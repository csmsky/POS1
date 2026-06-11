
Imports System.Drawing.Printing
Imports WindowsApplication1.ConfigClass
Public Class Form2

    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps

    'Cash Count
    Dim PrintCashCountReading As PrintDocument = New PrintDocument
    Private WithEvents PrintDocument3 As New PrintDocument

    Private Sub TextBoxCC1000_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCC1000.TextChanged
        Try
            Dim amount1000 As Integer = 1000
            LabelTotal1000.Text = amount1000 * TextBoxCC1000.Text
        Catch
            MsgBox("Please Number or make it 0!")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label21.Text = (Convert.ToDecimal(LabelTotal1000.Text) +
                Convert.ToDecimal(LabelTotal500.Text) +
                Convert.ToDecimal(LabelTotal200.Text) +
                Convert.ToDecimal(LabelTotal100.Text) +
                Convert.ToDecimal(LabelTotal50.Text) +
                Convert.ToDecimal(Label1TotalCoins.Text)).ToString("N2")
    End Sub

    Private Sub TextBoxCC500_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCC500.TextChanged
        Try
            Dim amount500 As Integer = 500
            LabelTotal500.Text = amount500 * TextBoxCC500.Text
        Catch
            MsgBox("Please Number or make it 0!")
        End Try
    End Sub

    Private Sub TextBoxCC200_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCC200.TextChanged
        Try
            Dim amount200 As Integer = 200
            LabelTotal200.Text = amount200 * TextBoxCC200.Text
        Catch
            MsgBox("Please Number or make it 0!")
        End Try
    End Sub

    Private Sub TextBoxCC100_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCC100.TextChanged
        Try
            Dim amount100 As Integer = 100
            LabelTotal100.Text = amount100 * TextBoxCC100.Text
        Catch

        End Try
    End Sub

    Private Sub TextBoxCC50_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCC50.TextChanged
        Try
            Dim amount50 As Integer = 50
            LabelTotal50.Text = amount50 * TextBoxCC50.Text
        Catch
            MsgBox("Please Number or make it 0!")
        End Try
    End Sub

    Private Sub TextBoxTotalCoins_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTotalCoins.TextChanged
        Try
            Label1TotalCoins.Text = TextBoxTotalCoins.Text
        Catch
            MsgBox("Please Number or make it 0!")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            ' Set printer name dynamically here (or you can hardcode it)
            PrintDocument3.PrinterSettings.PrinterName = "Posiflex PP9000 Printer"  ' Replace with your printer name

            Dim margins As New Margins(0, 0, 0, 0)
            PrintCashCountReading.DefaultPageSettings.Margins = margins

            ''THERMAL
            'Dim papersize As New PaperSize("Custom", 280, 0)
            Dim papersize As New PaperSize("Custom", 280, 800)

            PrintCashCountReading.DefaultPageSettings.PaperSize = papersize

            Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
            PrintPreviewDialog1.Document = PrintCashCountReading

            ' Check if the printer is valid
            If Not PrintDocument3.PrinterSettings.IsValid Then

                AddHandler PrintCashCountReading.PrintPage, AddressOf Me.PrintDocument3_PrintPage
                PrintPreviewDialog1.ShowDialog()

                MsgBox("Printer is not valid!")
                Return
            Else
                ''show preview
                'AddHandler PrintXReading.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                'PrintPreviewDialog1.ShowDialog()

                'TO PRINT IMMEDIATELY
                PrintDocument3.Print()
            End If
        Catch ex As Exception
            MsgBox("Encounter Error in Printing")
        End Try
    End Sub

    Private Sub PrintDocument3_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument3.PrintPage
        Try
            ' Initial debugging to verify code execution
            ' Declare variables
            Dim cash_float, cash_drop As String

            ' Format today's date as yyyy-MM-dd
            Dim datetoday As String = DateTime.Now.ToString("yyyy-MM-dd")

            ' Format time (HH:mm)
            Dim timetoday As String = DateTime.Now.ToString("HH:mm:ss")

            ' MySQL connection
            Using conn As New MySqlConnection(strConn)
                conn.Open()

                ' ---------------------------
                ' 1) GET ACCUMULATED TOTALS
                ' ---------------------------

                ' SQL query to get accumulated totals
                Dim xSql As String = "SELECT cash_float, cash_drop FROM accumulated_amount_tbl " &
                             "WHERE user_id = @uid AND pos_id = @pos AND DATE(payment_date) = @pdate;"

                Dim xdt As New DataTable()

                ' SQL Command to fetch data
                Using xCmd As New MySqlCommand(xSql, conn)
                    xCmd.CommandType = CommandType.Text
                    xCmd.CommandTimeout = 0

                    ' Add parameters to the query
                    xCmd.Parameters.AddWithValue("@uid", login.TextBoxUserID.Text)
                    xCmd.Parameters.AddWithValue("@pos", mainform.LabelPOSno.Text)
                    xCmd.Parameters.AddWithValue("@pdate", datetoday)

                    ' Fetch data into DataTable
                    Using xda As New MySqlDataAdapter(xCmd)
                        xda.Fill(xdt)
                    End Using
                End Using

                ' If no rows are returned, show a message and exit
                If xdt.Rows.Count = 0 Then
                    ' MsgBox("No rows returned") ' Debugging message
                    MessageBox.Show("No accumulated record found for the selected date.", "X-READING")
                    Exit Sub
                End If

                ' If data exists, process it
                ' Access columns by name instead of index
                If Not IsDBNull(xdt.Rows(0)("cash_float")) Then
                    cash_float = FormatMoney(xdt.Rows(0)("cash_float"))
                Else
                    cash_float = "0.00"
                End If

                If Not IsDBNull(xdt.Rows(0)("cash_drop")) Then
                    cash_drop = FormatMoney(xdt.Rows(0)("cash_drop"))
                Else
                    cash_drop = "0.00"
                End If

                ' ---------------------------
                ' 2) PAYMENT METHOD BREAKDOWN
                ' ---------------------------
                Dim payTotals As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)

                Dim paySql As String = "SELECT payment_method, SUM(total_amount) AS total_amount " &
                               "FROM or_tbl " &
                               "WHERE cashier_id = @uid " &
                               "AND pos_id = @pos " &
                               "AND payment_date = @pdate " &
                               "AND (void_status IS NULL OR void_status <> 'yes') " &
                               "GROUP BY payment_method " &
                               "ORDER BY payment_method;"

                ' Debugging: Check parameters for payment method breakdown
                ' MsgBox("Payment Query for Date: " & datetoday)

                Dim payDt As New DataTable()

                ' Fetch payment data
                Using payCmd As New MySqlCommand(paySql, conn)
                    payCmd.CommandType = CommandType.Text
                    payCmd.CommandTimeout = 0
                    payCmd.Parameters.AddWithValue("@uid", login.TextBoxUserID.Text)
                    payCmd.Parameters.AddWithValue("@pos", mainform.LabelPOSno.Text)
                    payCmd.Parameters.AddWithValue("@pdate", datetoday)

                    Using payDa As New MySqlDataAdapter(payCmd)
                        payDa.Fill(payDt)
                    End Using
                End Using

                ' Process payment method totals
                For Each r As DataRow In payDt.Rows
                    Dim method As String = If(r("payment_method"), "").ToString().Trim()
                    Dim amt As Decimal = 0D
                    If Not IsDBNull(r("total_amount")) Then amt = Convert.ToDecimal(r("total_amount"))
                    If method <> "" Then payTotals(method) = amt
                Next

                ' ---------------------------
                ' 3) PRINT LAYOUT SETTINGS
                ' ---------------------------
                Dim leftMargin As Integer = 5
                Dim rightMargin As Integer = 5
                Dim thermalPaperWidth As Integer = 300  '300
                Dim maxWidth As Integer = thermalPaperWidth - leftMargin - rightMargin

                Dim pos = AppConfigReader.ps
                Dim serial = AppConfigReader.srl

                ' Graphics setup for printing
                Dim graphics As Graphics = e.Graphics
                Dim brush As New SolidBrush(Color.Black)

                Dim centerFormat As New StringFormat() With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }

                Dim N As New Font("Merchant Copy", 9)
                Dim H As New Font("Merchant Copy", 10)
                Dim B As New Font("Merchant Copy", 11, FontStyle.Bold)
                Dim font As New Font("Merchant Copy", 9)

                Dim yPos As Integer = 5

                ' ---------------------------
                ' 4) PRINT HEADER
                ' ---------------------------
                graphics.DrawString(CompName, B, brush, New RectangleF(leftMargin, yPos, maxWidth, 100), centerFormat)
                yPos += 80

                CenterText(e, "CASH COUNT REPORT", B, yPos) : yPos += 20
                CenterText(e, "Date/Time : " & datetoday & " " & timetoday, H, yPos) : yPos += 20
                CenterText(e, "=================================", H, yPos) : yPos += 20
                CenterText(e, "Cashier: " & mainform.LabelCashierName.Text, N, yPos) : yPos += 15
                CenterText(e, "POS " & mainform.LabelPOSno.Text, N, yPos) : yPos += 15
                CenterText(e, "=================================", H, yPos) : yPos += 20

                ' ---------------------------
                ' 6) NET COLLECTIONS
                ' ---------------------------
                yPos += 20
                graphics.DrawString("CASH EXPECTED:", font, brush, leftMargin, yPos)
                yPos += 20

                ' Display payment method totals
                Dim orderedMethods As String() = {"CASH"}
                Dim value As Decimal

                For Each m As String In orderedMethods
                    Dim amt As Decimal = 0D
                    If payTotals.ContainsKey(m) Then amt = payTotals(m)

                    graphics.DrawString(m & " Sales:", font, brush, leftMargin + 40, yPos)
                    graphics.DrawString(amt.ToString("N2"), N, brush, maxWidth - 145, yPos)
                    yPos += 20
                    value = Decimal.Parse(amt.ToString("N2"), Globalization.NumberStyles.Any)
                Next

                'Dim totalPaymentMethod As Decimal = payTotals.Values.Sum()

                Dim TotalCashOnHand As Decimal = CDec(value) + CDec(cash_float) - CDec(cash_drop)

                yPos += 20
                DrawLine(graphics, "    Cash In: ", cash_float, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                DrawLine(graphics, "    Cash Out: ", cash_drop, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                DrawLine(graphics, "    Cash in Drawer: ", TotalCashOnHand, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20

                CenterText(e, "=================================", H, yPos) : yPos += 20
                graphics.DrawString("ACTUAL CASH COUNT:", font, brush, leftMargin, yPos)
                yPos += 20

                DrawLine(graphics, "      1000    X  ", TextBoxCC1000.Text & "     = " & LabelTotal1000.Text, font, N, brush, leftMargin, maxWidth - 20, yPos) : yPos += 20
                DrawLine(graphics, "      500     X  ", TextBoxCC500.Text & "      = " & LabelTotal500.Text, font, N, brush, leftMargin, maxWidth - 20, yPos) : yPos += 20
                DrawLine(graphics, "      200     X  ", TextBoxCC200.Text & "      = " & LabelTotal200.Text, font, N, brush, leftMargin, maxWidth - 20, yPos) : yPos += 20
                DrawLine(graphics, "      100     X  ", TextBoxCC100.Text & "      = " & LabelTotal100.Text, font, N, brush, leftMargin, maxWidth - 20, yPos) : yPos += 20
                DrawLine(graphics, "      50      X  ", TextBoxCC50.Text & "       = " & LabelTotal50.Text, font, N, brush, leftMargin, maxWidth - 20, yPos) : yPos += 20
                DrawLine(graphics, "      Coins   X  ", TextBoxTotalCoins.Text & "    = " & Label1TotalCoins.Text, font, N, brush, leftMargin, maxWidth - 20, yPos) : yPos += 30
                DrawLine(graphics, "Total Counted Cash:  ", Label21.Text, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                CenterText(e, "=================================", H, yPos) : yPos += 30
                DrawLine(graphics, "Cash Expected Amount:      ", "   " & TotalCashOnHand, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                DrawLine(graphics, "Actual Expected Amount:    ", "   " & Label21.Text, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 30

                Dim AmountDifference As Decimal = CDec(Label21.Text) - CDec(TotalCashOnHand)
                DrawLine(graphics, "Difference:     ", AmountDifference, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20

                If (AmountDifference < 0) Then
                    DrawLine(graphics, "Status: ", "Short", font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                ElseIf (AmountDifference > 0) Then
                    DrawLine(graphics, "Status: ", "Over", font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                Else
                    DrawLine(graphics, "Status: ", "Balanced", font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                End If

            End Using
        Catch ex As Exception
            ' Catch any exceptions and display the error message
            'MsgBox("Error: " & ex.Message)
            MsgBox("Something Issue!")
        End Try
    End Sub
    Private Function FormatMoney(val As Object) As String
        If val Is Nothing OrElse IsDBNull(val) Then Return "0.00"
        Dim d As Decimal = 0D
        Decimal.TryParse(val.ToString(), d)
        Return d.ToString("N2")
    End Function

    Private Sub CenterText(e As Printing.PrintPageEventArgs, text As String, font As Font, y As Integer)
        Dim x = (e.PageBounds.Width - e.Graphics.MeasureString(text, font).Width) / 2
        e.Graphics.DrawString(text, font, Brushes.Black, x, y)
    End Sub
    Private Sub DrawLine(g As Graphics, label As String, value As String,
                 fLabel As Font, fValue As Font, br As Brush,
                 left As Integer, maxWidth As Integer, y As Integer)

        g.DrawString(label, fLabel, br, left, y)
        g.DrawString(value, fValue, br, maxWidth - 145, y)
    End Sub
End Class