Imports System.Data
Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports WindowsApplication1.ConfigClass
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks


Public Class voidtransaction
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    'Fingerprint Reader   
    Private _fpAuth As FingerprintManagerAuth
    Private _fpCts As Threading.CancellationTokenSource

    'printing
    Dim WithEvents Void As PrintDocument = New PrintDocument

    Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
    Dim barcode As BusinessRefinery.Barcode.PDF417 = New BusinessRefinery.Barcode.PDF417()

    Private _pcscReader As PscsReader
    Private _cardPolling As CardPolling
    Dim apdu As Apdu = New Apdu
    Dim command() As Byte

    Dim todaysdate As String = String.Format("{0:yyyy-MM-dd}", DateTime.Now)

    Dim no_of_void, begin_void_no, end_void_no, total_amount, vatable, vat_exempt, zero_rated, vat, discount As String

    ' ===========================
    ' UI STATUS HELPER (optional label: lblFpStatus)
    ' ===========================
    Private Sub SetFpStatus(msg As String)
        Try
            If lblFpStatus Is Nothing Then Return
            lblFpStatus.Text = msg
            lblFpStatus.Refresh()
        Catch
        End Try
    End Sub

    Public Sub OnThreadException(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        MessageBox.Show(e.Exception.Message)
    End Sub

    Private Sub cardPolling_OnCardFound(ByVal sender As Object, ByVal e As CardPollingEventArg)
        Try
            Dim data() As Byte
            Dim indx As Integer
            Dim tmpStr As String

            'card establish context
            _pcscReader.establishContext()

            'card connect 
            _pcscReader.connect(ComboReaderNames.Text)

            'card begin transaction
            _pcscReader.beginTransaction()

            'set status LED to green
            command = Helper.getBytes("FF 00 40 20 04 01 01 01 01", " ")

            If command.Length > 5 Then
                apdu.setCommand(command.Skip(0).Take(5).ToArray)
                apdu.data = New Byte(command(4) - 1) {}
                Array.Copy(command, 5, apdu.data, 0, command.Length - 5)

            Else
                apdu.setCommand(command)

                If command(4) <> 0 Then
                    apdu.data = Nothing
                    apdu.lengthExpected = command(4)

                End If
            End If

            _pcscReader.sendCommand(apdu)

            'load authentication key
            command = Helper.getBytes("FF 82 00 00 06 FF FF FF FF FF FF", " ")

            If command.Length > 5 Then
                apdu.setCommand(command.Skip(0).Take(5).ToArray)
                apdu.data = New Byte(command(4) - 1) {}
                Array.Copy(command, 5, apdu.data, 0, command.Length - 5)

            Else
                apdu.setCommand(command)

                If command(4) <> 0 Then
                    apdu.data = Nothing
                    apdu.lengthExpected = command(4)

                End If
            End If

            _pcscReader.sendCommand(apdu)

            'authenticate block 01 
            command = Helper.getBytes("FF 86 00 00 05 01 00 01 60 00", " ")

            If command.Length > 5 Then
                apdu.setCommand(command.Skip(0).Take(5).ToArray)
                apdu.data = New Byte(command(4) - 1) {}
                Array.Copy(command, 5, apdu.data, 0, command.Length - 5)

            Else
                apdu.setCommand(command)

                If command(4) <> 0 Then
                    apdu.data = Nothing
                    apdu.lengthExpected = command(4)

                End If
            End If

            _pcscReader.sendCommand(apdu)

            'read block 01 
            command = Helper.getBytes("FF B0 00 01 10", " ")

            If command.Length > 5 Then
                apdu.setCommand(command.Skip(0).Take(5).ToArray)
                apdu.data = New Byte(command(4) - 1) {}
                Array.Copy(command, 5, apdu.data, 0, command.Length - 5)

            Else
                apdu.setCommand(command)

                If command(4) <> 0 Then
                    apdu.data = Nothing
                    apdu.lengthExpected = command(4)

                End If
            End If

            _pcscReader.sendCommand(apdu)

            'get response
            data = apdu.response.Take(16).ToArray

            tmpStr = ""

            For indx = 0 To data.Length - 1
                tmpStr = tmpStr + Chr(data(indx))
            Next indx

            TextBoxUserID.Text = tmpStr

            _pcscReader.endTransaction()

        Catch ex As Exception

        End Try

    End Sub
    Dim fileModifier As New ModifyTextFile()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            auto()

            Dim dialog As DialogResult
            dialog = MessageBox.Show("Are you sure you want to void this ticket?",
                             "Confirm Void",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question)

            If dialog <> DialogResult.Yes Then
                MsgBox("Operation cancelled.", MsgBoxStyle.Information)
                Return
            End If

            If String.IsNullOrWhiteSpace(TextBoxORNumber.Text) Then
                MsgBox("Please enter SI number.", MsgBoxStyle.Exclamation)
                Return
            End If

            Dim posId As Integer
            If Not Integer.TryParse(mainform.LabelPOSno.Text.Trim(), posId) Then
                MsgBox("Invalid POS number.", MsgBoxStyle.Exclamation)
                Return
            End If

            Dim result As Integer = -1
            Dim msg As String = ""

            Using conn As New MySqlConnection(strConn)
                Using cmd As New MySqlCommand("voidTransaction", conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.Add("@p_or_no", MySqlDbType.VarChar).Value = TextBoxORNumber.Text.Trim()
                    cmd.Parameters.Add("@p_or_rst_cnt", MySqlDbType.Int32).Value = Convert.ToInt32(TextBoxResetCounter.Text.Trim())
                    cmd.Parameters.Add("@p_pos_id", MySqlDbType.Int32).Value = posId
                    cmd.Parameters.Add("@p_payment_date", MySqlDbType.Date).Value = Date.Today
                    cmd.Parameters.Add("@p_void_no", MySqlDbType.VarChar).Value = LabelVoidNo.Text.Trim()
                    cmd.Parameters.Add("@p_rst_cnt_v", MySqlDbType.Int32).Value = mainform.voidRstCnt
                    cmd.Parameters.Add("@p_manager_id", MySqlDbType.VarChar).Value = TextBoxUserID.Text.Trim()
                    cmd.Parameters.Add("@p_cashier_id", MySqlDbType.VarChar).Value = mainform.LabelCashierID.Text.Trim()

                    cmd.Parameters.Add("@p_result", MySqlDbType.Int32).Direction = ParameterDirection.Output
                    cmd.Parameters.Add("@p_message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output

                    conn.Open()
                    cmd.ExecuteNonQuery()

                    result = Convert.ToInt32(cmd.Parameters("@p_result").Value)
                    msg = Convert.ToString(cmd.Parameters("@p_message").Value)
                End Using
            End Using

            If result = 1 Then
                MsgBox("Transaction Voided!", MsgBoxStyle.Information)

                ' INSERT AUDIT TRAIL ONLY IF SUCCESS
                Try
                    Dim BranchCode As String = "BR01"
                    Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

                    Using conn As New MySqlConnection(strConn)
                        conn.Open()

                        Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                            cmd.CommandType = CommandType.StoredProcedure

                            cmd.Parameters.AddWithValue("p_pos_id", mainform.LabelPOSno.Text)
                            cmd.Parameters.AddWithValue("p_userid", mainform.LabelCashierID.Text)
                            cmd.Parameters.AddWithValue("p_username", mainform.LabelCashierName.Text)
                            cmd.Parameters.AddWithValue("p_approvedby", TextBoxUserID.Text)
                            cmd.Parameters.AddWithValue("p_activity_performed", "Void Transaction")
                            cmd.Parameters.AddWithValue("p_module", "Sales")
                            cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-VT-" & LabelVoidNo.Text)
                            cmd.Parameters.AddWithValue("p_remarks", "Voided Sales")

                            cmd.ExecuteNonQuery()
                        End Using
                    End Using

                Catch ex As Exception
                    'MsgBox("Cannot store audit trail: " & ex.Message, MsgBoxStyle.Critical)
                    MsgBox("Cannot store audit trail")
                End Try

            ElseIf result = 0 Then
                'MsgBox(msg, MsgBoxStyle.Exclamation)
                MsgBox("No data recorded!")
            Else
                'MsgBox("Voiding Service Invoice not successful! " & msg, MsgBoxStyle.Critical)
                MsgBox("Voiding Service Invoice not successful! Error: " & msg)
            End If

        Catch ex As Exception
            'MsgBox("Error voiding transaction: " & ex.Message, MsgBoxStyle.Critical)
            'MsgBox("Voiding Transaction NOT Successfully!")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Example values to pass to the UpdateTextFile method
            Dim or_no As String = voidedOR.Text        ' Example order number (or_no)
            Dim saleInvoice As String = "SALES INVOICE"  ' The text to replace
            Dim voidedInvoice As String = "VOIDED INVOICE"   ' The new text to insert

            'MsgBox("Updated E-Journal in Database")
            TextBoxUserID.Text = ""

            ' Call the UpdateTextFile method to modify the content
            fileModifier.UpdateTextFile(or_no, saleInvoice, voidedInvoice)

            'Dim updatedContent As String = "This is the updated receipt content to be printed."
            Dim updatedContent As String = GetUpdatedContentFromDatabase(or_no)
            ' Call the PrintUpdatedContent method with the content to print
            'PrintUpdatedContent(updatedContent)
            PrintMultipleTables(or_no)

        Catch ex As Exception
            MsgBox("Changes in SALES INVOICE didn't apply!")
        End Try
    End Sub

    Private Sub PrintMultipleTablesFromTextBox()
        ' Get the or_no from the TextBox
        Dim or_no As String = TextBoxORNumber.Text.Trim()

        ' Check if the or_no is empty
        If String.IsNullOrEmpty(or_no) Then
            MsgBox("Please enter a valid OR No.")
            Return
        End If

        ' Proceed to fetch and print the data using the or_no
        PrintMultipleTables(or_no)
    End Sub

    ' This method handles printing the data fetched from multiple tables
    Private Sub CenterText(e As Printing.PrintPageEventArgs, text As String, font As Font, y As Integer)
        Dim x = (e.PageBounds.Width - e.Graphics.MeasureString(text, font).Width) / 2
        e.Graphics.DrawString(text, font, Brushes.Black, x, y)
    End Sub

    ' This method handles printing the data fetched from multiple tables
    Private Sub PrintMultipleTables(or_no As String)
        ' Create a PrintDocument instance
        Dim printDocument As New PrintDocument()

        ' Set thermal paper size: 3 inches x 80 mm (approx. 300 x 800 pixels)
        Dim thermalPaperWidth As Integer = 300  ' 3 inches in 1/100ths of an inch
        Dim thermalPaperHeight As Integer = 0 ' 8 inches (about 80mm) in 1/100ths of an inch
        'Dim thermalPaperHeight As Integer = 1300 ' 8 inches (about 80mm) in 1/100ths of an inch
        Dim paperSize As New PaperSize("Thermal Paper", thermalPaperWidth, thermalPaperHeight)
        printDocument.DefaultPageSettings.PaperSize = paperSize

        ' Set margins
        Dim leftMargin As Integer = 5
        Dim rightMargin As Integer = 5
        Dim topMargin As Integer = 5

        ' Calculate the available width for printing (total width minus left and right margins)
        Dim maxWidth As Integer = thermalPaperWidth - leftMargin - rightMargin

        ' Fetch data from multiple tables (e.g., sales_table, customer_table) using or_no
        Dim or_items_tbl As DataTable = GetDataFromTable("or_items_tbl", or_no)
        Dim or_tbl As DataTable = GetDataFromTable("or_tbl", or_no)
        Dim card_details_tbl As DataTable = GetDataFromTable("card_details_tbl", or_no)
        Dim customer_tbl As DataTable = GetDataFromTable("customer_tbl", or_no)

        ' Set printer name dynamically here (or you can hardcode it)
        printDocument.PrinterSettings.PrinterName = "Posiflex PP9000 Printer"  ' Replace with your printer name

        ' Check if the printer is valid
        If Not printDocument.PrinterSettings.IsValid Then
            MsgBox("Printer is not valid!")
            Return
        End If

        ' Handler for the PrintPage event
        AddHandler printDocument.PrintPage, Sub(sender As Object, e As PrintPageEventArgs)
                                                Dim pos = AppConfigReader.ps
                                                Dim serial = AppConfigReader.srl
                                                Dim font As New Font("Merchant Copy", 10)
                                                Dim graphics As Graphics = e.Graphics
                                                Dim brush As New SolidBrush(Color.Black)

                                                Dim centerFormat As New StringFormat()
                                                centerFormat.Alignment = StringAlignment.Center
                                                centerFormat.LineAlignment = StringAlignment.Center

                                                Dim N As New Font("Merchant Copy", 10)
                                                Dim NB As New Font("Merchant Copy", 10, FontStyle.Bold)
                                                Dim S As New Font("Merchant Copy", 8)
                                                Dim M As New Font("Merchant Copy", 6.5, FontStyle.Bold)
                                                Dim H As New Font("Merchant Copy", 13)
                                                Dim B As New Font("Merchant Copy", 15, FontStyle.Bold)

                                                '0000000000000060

                                                Dim yPos As Integer = 5

                                                'HEADER

                                                graphics.DrawString(CompName, B, brush, New RectangleF(leftMargin, yPos, maxWidth, 100), centerFormat)
                                                yPos += 80
                                                CenterText(e, CompNameB, N, yPos)
                                                yPos += 15
                                                CenterText(e, AddressA, N, yPos)
                                                yPos += 15
                                                CenterText(e, AddressB, N, yPos)
                                                yPos += 15
                                                CenterText(e, AddressC, N, yPos)
                                                yPos += 15
                                                CenterText(e, AddressD, N, yPos)
                                                yPos += 15
                                                CenterText(e, VATTIN, N, yPos)
                                                yPos += 15
                                                CenterText(e, "POS" & pos & "S/N: " & serial, N, yPos)
                                                yPos += 15
                                                CenterText(e, MIN_No, N, yPos)
                                                yPos += 15
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20
                                                CenterText(e, "VOIDED INVOICE", B, yPos)
                                                yPos += 20
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 15
                                                graphics.DrawString("Cashier Name:", N, brush, leftMargin, yPos)
                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString(row("username").ToString(), N, brush, 155, yPos)
                                                Next
                                                yPos += 20

                                                graphics.DrawString("SI #: ", NB, brush, leftMargin, yPos)
                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString(Terminal_ID & row("or_no").ToString(), NB, brush, maxWidth - 180, yPos)
                                                Next
                                                yPos += 20
                                                graphics.DrawString("Date/Time: ", font, brush, leftMargin, yPos)
                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString(CDate(row("payment_date")).ToString("MM/dd/yyyy"), N, brush, maxWidth - 130, yPos)
                                                    graphics.DrawString(row("payment_time").ToString(), N, brush, maxWidth - 60, yPos)
                                                Next
                                                yPos += 30
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 15
                                                graphics.DrawString("Description", font, brush, leftMargin, yPos)
                                                yPos += 20
                                                graphics.DrawString("Qty", font, brush, maxWidth - 200, yPos) ' Right-align quantity
                                                graphics.DrawString("U.Price", font, brush, maxWidth - 160, yPos) ' Right-align unit price
                                                graphics.DrawString("Amount", font, brush, maxWidth - 90, yPos) ' Right-align amount
                                                yPos += 20

                                                ' Loop through the rows of the sales table and print each row
                                                For Each row As DataRow In or_items_tbl.Rows
                                                    graphics.DrawString(row("ride_type").ToString(), S, brush, New RectangleF(leftMargin, yPos, maxWidth, 200))
                                                    yPos += 35
                                                    graphics.DrawString(row("qty").ToString() & " x ", font, brush, maxWidth - 200, yPos)
                                                    graphics.DrawString("₱ " & Convert.ToDecimal(row("base_price")).ToString(), font, brush, maxWidth - 160, yPos)
                                                    graphics.DrawString("₱ " & Convert.ToDecimal(row("total_amount")).ToString(), NB, brush, maxWidth - 90, yPos)
                                                    yPos += 25

                                                    If Not IsDBNull(row("discount")) AndAlso Convert.ToDecimal(row("discount")) > 0 Then
                                                        graphics.DrawString(row("type_transaction").ToString() & " : ", font, brush, leftMargin + 20, yPos)
                                                        graphics.DrawString(" ₱(" & "-" & Convert.ToDecimal(row("discount")).ToString() & ")", font, brush, leftMargin + 100, yPos)
                                                        yPos += 25
                                                    Else
                                                        yPos += 25
                                                    End If
                                                Next
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 25
                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString("**" & row("qty").ToString() & " Item(s)**", font, brush, maxWidth - 237, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("AMOUNT DUE:", NB, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("total_amount")).ToString(), NB, brush, 170, yPos)
                                                    yPos += 20
                                                    If row("payment_method").ToString().ToUpper() = "CASH" Then
                                                        graphics.DrawString("PAYMENT:", font, brush, leftMargin, yPos)
                                                        graphics.DrawString(" ₱ " & Convert.ToDecimal(row("cash_tendered")), font, brush, 170, yPos)
                                                        yPos += 20
                                                        graphics.DrawString(row("payment_method").ToString(), font, brush, maxWidth - 237, yPos)   'need to change
                                                        yPos += 20
                                                        graphics.DrawString("CHANGE:", font, brush, leftMargin, yPos)  'need to change
                                                        graphics.DrawString(" ₱ " & Convert.ToDecimal(row("change_amount")), font, brush, 170, yPos)
                                                    ElseIf row("payment_method").ToString().ToUpper() = "GCASH" Or row("payment_method").ToString().ToUpper() = "QR PH" Then
                                                        graphics.DrawString("PAYMENT:", font, brush, leftMargin, yPos)
                                                        graphics.DrawString(" ₱ " & Convert.ToDecimal(row("total_amount")), font, brush, 170, yPos)
                                                        yPos += 20
                                                        graphics.DrawString(row("payment_method").ToString(), font, brush, maxWidth - 237, yPos)
                                                        yPos += 20
                                                        graphics.DrawString("APPROVED CODE: ", NB, brush, leftMargin, yPos)
                                                        graphics.DrawString(row("approved_code").ToString(), NB, brush, 170, yPos)
                                                    Else
                                                        graphics.DrawString("PAYMENT:", font, brush, leftMargin, yPos)
                                                        graphics.DrawString(" ₱ " & Convert.ToDecimal(row("total_amount")), font, brush, 170, yPos)
                                                        yPos += 20
                                                        graphics.DrawString(row("payment_method").ToString(), font, brush, maxWidth - 237, yPos)
                                                        yPos += 20
                                                        For Each card_row As DataRow In card_details_tbl.Rows
                                                            graphics.DrawString("CARD NAME: ", S, brush, leftMargin, yPos)
                                                            graphics.DrawString(card_row("card_name").ToString(), S, brush, 170, yPos)
                                                            yPos += 20
                                                            graphics.DrawString("CARD NO.: ", S, brush, leftMargin, yPos)
                                                            graphics.DrawString(card_row("card_number").ToString(), S, brush, 170, yPos)
                                                            yPos += 30
                                                        Next
                                                        graphics.DrawString("APPROVED CODE: ", NB, brush, leftMargin, yPos)
                                                        graphics.DrawString(row("approved_code").ToString(), NB, brush, 170, yPos)
                                                    End If
                                                Next
                                                yPos += 20
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20
                                                graphics.DrawString("VAT INFORMATION", font, brush, (thermalPaperWidth - graphics.MeasureString("VAT INFORMATION", font).Width) / 2, yPos)
                                                yPos += 15
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20

                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString("VATable:", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("vatable")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("VAT(12%):", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("vat")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("VAT-Exempt:", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("vat_exempt")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("Zero-Rated:", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("zero_rated")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("Less Vat(12%)", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("less_vat")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                Next

                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20
                                                Dim titleText As String = "CUSTOMER DETAILS"
                                                graphics.DrawString(titleText, font, brush, (thermalPaperWidth - graphics.MeasureString(titleText, font).Width) / 2, yPos)
                                                yPos += 15
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20

                                                Dim hasDbDiscount As Boolean = False
                                                For Each row As DataRow In or_tbl.Rows
                                                    If Not IsDBNull(row("discount")) AndAlso Convert.ToDecimal(row("discount")) > 0 Then
                                                        hasDbDiscount = True
                                                        Exit For
                                                    End If
                                                Next

                                                Dim hasCustomerDetails As Boolean = (customer_tbl IsNot Nothing AndAlso customer_tbl.Rows.Count > 0)

                                                If hasDbDiscount OrElse hasCustomerDetails Then

                                                    If hasCustomerDetails Then

                                                        For Each custRow As DataRow In customer_tbl.Rows


                                                            graphics.DrawString("Name: " & custRow("name").ToString(), font, brush, leftMargin, yPos)
                                                            yPos += 15

                                                            graphics.DrawString("ID: " & custRow("id_no").ToString(), font, brush, leftMargin, yPos)
                                                            yPos += 15

                                                            graphics.DrawString("TIN #: " & custRow("tin_no").ToString(), font, brush, leftMargin, yPos)
                                                            yPos += 15

                                                            Dim custAddress As String = custRow("address").ToString()
                                                            If Not String.IsNullOrEmpty(custAddress) Then
                                                                graphics.DrawString("Address: " & custAddress, font, brush, leftMargin, yPos)
                                                                yPos += 15
                                                            End If

                                                            graphics.DrawString("Signature:_________________", font, brush, leftMargin, yPos)
                                                            yPos += 30
                                                        Next

                                                    Else

                                                        graphics.DrawString("Name:_________________________", font, brush, leftMargin, yPos)
                                                        yPos += 20

                                                        graphics.DrawString("ID: ______________________", font, brush, leftMargin, yPos)
                                                        yPos += 20

                                                        graphics.DrawString("TIN #:________________________", font, brush, leftMargin, yPos)
                                                        yPos += 20

                                                        graphics.DrawString("Address:______________________", font, brush, leftMargin, yPos)
                                                        yPos += 20

                                                        graphics.DrawString("Signature:____________________", font, brush, leftMargin, yPos)
                                                        yPos += 30
                                                    End If
                                                Else
                                                    graphics.DrawString("Name:_________________________", font, brush, leftMargin, yPos)
                                                    yPos += 20

                                                    graphics.DrawString("ID: ______________________", font, brush, leftMargin, yPos)
                                                    yPos += 20

                                                    graphics.DrawString("TIN #:________________________", font, brush, leftMargin, yPos)
                                                    yPos += 20

                                                    graphics.DrawString("Address:______________________", font, brush, leftMargin, yPos)
                                                    yPos += 20

                                                    graphics.DrawString("Signature:____________________", font, brush, leftMargin, yPos)
                                                    yPos += 30
                                                End If

                                                yPos += 15
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 30
                                                CenterText(e, "THIS DOCUMENT IS NOT VALID FOR CLAIM OF INPUT TAX", M, yPos)
                                                yPos += 30
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 30
                                                'CenterText(e, V_Name, N, yPos)
                                                'yPos += 15
                                                'CenterText(e, V_VatReg, N, yPos)
                                                'yPos += 15
                                                'CenterText(e, V_Accreditation_No, N, yPos)
                                                'yPos += 15
                                                CenterText(e, V_Date_Issued, N, yPos)
                                                yPos += 15
                                                CenterText(e, V_PTU_No, N, yPos)
                                                yPos += 15
                                                CenterText(e, "Thank you! Please come again!", N, yPos)
                                                yPos += 50
                                                CenterText(e, "--- END OF TRANSACTION ---", H, yPos)

                                                e.HasMorePages = False
                                            End Sub
        ' Create the PrintPreviewDialog
        'Dim printPreview As New PrintPreviewDialog()
        'printPreview.Document = printDocument
        'printPreview.ShowDialog()  ' Show the print preview dialog

        printDocument.Print()

    End Sub

    ' Method to fetch data from a specific table filtered by or_no
    Private Function GetDataFromTable(tableName As String, or_no As String) As DataTable

        ' Create a DataTable to hold the fetched data
        Dim table As New DataTable()

        ' MySQL Connection string
        Using conn As New MySqlConnection(strConn)
            Try
                conn.Open()

                ' Query to fetch data from the table based on or_no
                Dim query As String = "SELECT * FROM " & tableName & " WHERE or_no = @or_no"
                If tableName = "customer_tbl" Then
                    query = "SELECT or_no, CAST(AES_DECRYPT(name, 'strdjnltmyp') AS CHAR) AS name, id_no, tin_no, address FROM customer_tbl WHERE or_no = @or_no"
                End If
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@or_no", or_no)

                ' Execute the query and fill the DataTable with the result
                Dim adapter As New MySqlDataAdapter(cmd)
                adapter.Fill(table)
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End Using

        Return table
    End Function
    ' Method to print the updated content for VOID
    Private Sub PrintUpdatedContent(updatedContent As String)
        ' Create a PrintDocument instance
        Dim printDocument As New PrintDocument()

        ' Set thermal paper size: 3 inches x 80 mm (approx. 300 x 800 pixels)
        Dim thermalPaperWidth As Integer = 300  ' 3 inches in 1/100ths of an inch
        Dim thermalPaperHeight As Integer = 800 ' 8 inches (about 80mm) in 1/100ths of an inch
        Dim paperSize As New PaperSize("Thermal Paper", thermalPaperWidth, thermalPaperHeight)
        printDocument.DefaultPageSettings.PaperSize = paperSize

        ' Set margins
        Dim leftMargin As Integer = 20 ' Left margin
        Dim rightMargin As Integer = 30 ' Right margin
        Dim topMargin As Integer = 50   ' Top margin

        ' Calculate the available width for printing (total width minus left and right margins)
        Dim maxWidth As Integer = thermalPaperWidth - leftMargin - rightMargin

        ' Handler for the PrintPage event
        AddHandler printDocument.PrintPage, Sub(sender As Object, e As PrintPageEventArgs)
                                                Dim font As New Font("Merchant", 8)
                                                Dim brush As Brush = Brushes.Black

                                                ' Print the updated content directly with the calculated max width
                                                e.Graphics.DrawString(updatedContent, font, brush, leftMargin, topMargin)

                                                ' Indicate that the printing is done
                                                e.HasMorePages = False
                                            End Sub

        ' Create the PrintPreviewDialog
        Dim printPreview As New PrintPreviewDialog()
        printPreview.Document = printDocument
        printPreview.ShowDialog()  ' Show the print preview dialog
    End Sub

    ' Method to wrap text that exceeds the page width
    Private Function WrapText(text As String, font As Font, graphics As Graphics, maxWidth As Integer) As String
        Dim sb As New StringBuilder() ' StringBuilder used here to accumulate lines
        Dim words As String() = text.Split(" "c)
        Dim line As String = String.Empty

        For Each word As String In words
            ' Measure the string width with the current line and word added
            If graphics.MeasureString(line & word, font).Width < maxWidth Then
                line &= word & " "  ' Add the word to the current line
            Else
                sb.AppendLine(line.Trim())  ' Add the current line to the StringBuilder
                line = word & " "  ' Start a new line with the current word
            End If
        Next

        ' Add the last line if necessary
        If line.Length > 0 Then sb.AppendLine(line.Trim())

        ' Return the wrapped text
        Return sb.ToString()
    End Function

    ' Method to fetch the updated content (text_file) from the database
    Private Function GetUpdatedContentFromDatabase(or_no As String) As String
        Dim query As String = "SELECT text_file FROM e_journal_tbl WHERE or_no = @orNo"
        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@orNo", or_no)
        Dim content As String = String.Empty

        Try
            conn.Open()
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()  ' Read the first row
                ' Convert the BLOB data to a string
                content = System.Text.Encoding.UTF8.GetString(CType(reader("text_file"), Byte()))
            End If
        Catch ex As Exception
            MsgBox("Error fetching updated content: " & ex.Message)
        Finally
            conn.Close()
        End Try

        Return content
    End Function

    Private Sub TextBoxORNumber_TextChanged(sender As Object, e As EventArgs) Handles TextBoxORNumber.TextChanged
        voidedOR.Text = TextBoxORNumber.Text
    End Sub

    Private Sub cardPolling_OnError(ByVal sender As Object, ByVal e As CardPollingErrorEventArg)
        MessageBox.Show("Invalid")
    End Sub

    Private Sub cardPolling_OnCardRemoved(ByVal sender As Object, ByVal e As CardPollingEventArg)
        'clearing fields when card is removed
        TextBoxUserID.Clear()

        ButtonVoid.Enabled = False
    End Sub

    Private Sub TextBoxUserID_TextChanged(sender As Object, e As EventArgs) Handles TextBoxUserID.TextChanged
        If String.IsNullOrWhiteSpace(TextBoxUserID.Text) Then
            Button1.Enabled = False
            Return
        End If

        Try
            Using c As New MySqlConnection(strConn)
                c.Open()

                Dim sql As String =
                "SELECT COUNT(*) FROM user_registration_tbl " &
                "WHERE user_id=@id AND CAST(AES_DECRYPT(position,'strdjnltmyp') AS CHAR(50))='Manager'"

                Using cmd As New MySqlCommand(sql, c)
                    cmd.Parameters.AddWithValue("@id", TextBoxUserID.Text.Trim())
                    Dim n As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    Button1.Enabled = (n > 0)
                End Using
            End Using

            If Button1.Enabled Then
                SetFpStatus("Manager authorized ✅ You may now reprint.")
            Else
                SetFpStatus("Matched user is not Manager.")
            End If

        Catch ex As Exception
            Button1.Enabled = False
            SetFpStatus("DB Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Try
            If _fpCts IsNot Nothing Then _fpCts.Cancel()
            If _fpAuth IsNot Nothing Then _fpAuth.CancelAndCleanup()
        Catch
        End Try

        Me.Close()
        mainform.Show()
    End Sub

    Private Sub ButtonVoid_Click(sender As Object, e As EventArgs) Handles ButtonVoid.Click
        Dim dialog As DialogResult

        auto()

        dialog = MessageBox.Show("Are you sure you want to void transaction?", "Warning!", MessageBoxButtons.YesNo)
        Try
            conn.Open()
            If dialog = DialogResult.Yes Then
                'to check if the ticket was already been voided
                Dim voidCheck As String = "SELECT * FROM or_tbl WHERE or_no = '" & TextBoxORNumber.Text & "' AND void_status = 'no' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "'"
                Dim cmdVoidCheck As New MySqlCommand(voidCheck) With {.Connection = conn}
                'cmdVoidCheck.CommandType = CommandType.Text
                'cmdVoidCheck.Parameters.AddWithValue("@or_no", TextBoxORNumber.Text)

                Using readerVoid As MySqlDataReader = cmdVoidCheck.ExecuteReader()
                    Try
                        'update void_status to yes, update seat_status
                        If readerVoid.HasRows Then
                            readerVoid.Close()
                            conn.Close()
                            conn.Open()
                            Dim ccd As New MySqlCommand("UPDATE or_tbl SET qty = (qty * -1),
                                                         base_price = (base_price * -1),
                                                         vatable = (vatable * -1),
                                                         vat_exempt = (vat_exempt * -1),
                                                         zero_rated = (zero_rated * -1),
                                                         vat = (vat * -1),
                                                         discount = (discount * -1),
                                                         total_amount = (total_amount * -1),
                                                         void_status = 'yes', updated = 'yes' WHERE or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "';
                                                         
                                                         UPDATE e_journal_tbl SET qty = (qty * -1),
                                                         base_price = (base_price * -1),
                                                         vatable = (vatable * -1),
                                                         vat_exempt = (vat_exempt * -1),
                                                         zero_rated = (zero_rated * -1),
                                                         vat = (vat * -1),
                                                         discount = (discount * -1),
                                                         total_amount = (total_amount * -1),
                                                         void_no = '" & LabelVoidNo.Text & "', void_status = 'yes', updated = 'yes' WHERE or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "'", conn)
                            Dim reader As MySqlDataReader
                            reader = ccd.ExecuteReader

                            reader.Close()

                            MsgBox("Transaction Voided!", MessageBoxIcon.Information)
                            conn.Close()

                            ''INSERT into void_tbl
                            Dim AA As String = "INSERT INTO void_tbl (void_no, rst_cnt_v, or_no, payment_date, payment_time, pos_id, user_id, base_price, vatable, vat_exempt, zero_rated, vat, discount, total_amount, void_type, void_by, upload)
                            SELECT '" & LabelVoidNo.Text & "', " & mainform.voidRstCnt & ", or_no, payment_date, payment_time, pos_id, user_id, base_price, vatable, vat_exempt, zero_rated, vat, discount, total_amount, void_type, '" & TextBoxUserID.Text & "', 'no' FROM or_tbl WHERE or_no = '" & TextBoxORNumber.Text & "'"


                            Dim cmmd As New MySqlCommand(AA) With {.Connection = conn}
                            MsgBox("AFFTER INSERTING TO VOID TABLE")
                            Try
                                'conn.Open()
                                'cmmd.ExecuteReader()
                                'conn.Close()

                                'Dim BranchCode As String = "BR01"
                                'Dim DateNow = DateTime.Now.ToString("yyyyMMdd")
                                'Dim reference_id As String = BranchCode & "-" & DateNow & "-SI-" & TextBoxORNumber.Text

                                'insert into audit_trail_tbl
                                'Dim audit_trail As String = "INSERT INTO audit_trail_tbl (pos_id, or_no, userid, username, approveby, activity_performed, module, reference_id, remarks)
                                'Select Case'" & mainform.LabelPOSno.Text & "', or_no, '" & Today.ToString("yyyy-MM-dd") & "', '" & Now.ToString("HH:mm:ss") & "', user_id, '" & TextBoxUserID.Text & "', 'Void Transaction', 'Sales', reference_id ,Void Transaction, 'no' FROM or_tbl WHERE or_no = '" & TextBoxORNumber.Text & "'"
                                'Dim cmdd As New MySqlCommand(audit_trail) With {.Connection = conn}

                                'Dim audit_trail As String = "INSERT INTO audit_trail_tbl (pos_id, or_no, userid, username, approveby, activity_performed, module, reference_id, remarks) " &
                                '"SELECT '" & mainform.LabelPOSno.Text & "', or_no, '" & Today.ToString("yyyy-MM-dd") & "', '" & Now.ToString("HH:mm:ss") & "', cashier_id, '" & TextBoxUserID.Text & "', 'Void Transaction', 'Sales', reference_id, 'Void Transaction', 'no' " &
                                '"FROM or_tbl WHERE or_no = '" & TextBoxORNumber.Text & "'"
                                'Dim cmdd As New MySqlCommand(audit_trail) With {.Connection = conn}

                                Dim BranchCode As String = "BR01"
                                Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

                                Using conn As New MySqlConnection(strConn)
                                    conn.Open()

                                    Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                                        cmd.CommandType = CommandType.StoredProcedure

                                        cmd.Parameters.AddWithValue("p_pos_id", mainform.LabelPOSno.Text)
                                        cmd.Parameters.AddWithValue("p_userid", mainform.LabelCashierID.Text)
                                        cmd.Parameters.AddWithValue("p_username", mainform.LabelCashierName.Text)
                                        cmd.Parameters.AddWithValue("p_approvedby", "")
                                        cmd.Parameters.AddWithValue("p_activity_performed", "Void Transaction")
                                        cmd.Parameters.AddWithValue("p_module", "Sales")
                                        cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-VT-" & TextBoxORNumber.Text)
                                        cmd.Parameters.AddWithValue("p_remarks", "New Sales")

                                        cmd.ExecuteNonQuery()

                                    End Using
                                End Using

                                Try
                                    'conn.Open()
                                    'cmdd.ExecuteReader()
                                    'conn.Close()

                                    conn.Open()

                                    'select data from void_tbl
                                    'Dim sum As New MySqlCommand("Select (SELECT vatable + vat_exempt, zero_rated + vat + total_amount FROM void_tbl where or_no = '" & TextBoxORNumber.Text & "')", conn)
                                    'sum.CommandType = CommandType.Text
                                    'Dim da As New MySqlDataAdapter(sum)
                                    'Dim dtt As New DataTable
                                    'sum.CommandTimeout = 0

                                    'Try
                                    '    da.Fill(dtt)

                                    '    total_amount = dtt.Rows(0)(0).ToString

                                    'sales, vatable, vat_exempt, zero_rated, vat, discount, void

                                    'select data from accumulated_amount_tbl
                                    Dim total_sales, vatable, vat_exempt, zero_rated, vat, discount, void_amount As String

                                    Dim disc As New MySqlCommand("SELECT (SELECT SUM(vatable + vat_exempt + zero_rated + vat + total_amount) FROM void_tbl where or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') + (SELECT total_sales FROM accumulated_amount_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') AS total_sales,
                                                                 (SELECT vatable FROM void_tbl where or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') + (SELECT vatable FROM accumulated_amount_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') AS vatable,
                                                                 (SELECT vat_exempt FROM void_tbl where or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') + (SELECT vat_exempt FROM accumulated_amount_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') AS vat_exempt,
                                                                 (SELECT zero_rated FROM void_tbl where or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') + (SELECT zero_rated FROM accumulated_amount_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') AS zero_rated,
                                                                 (SELECT vat FROM void_tbl where or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') + (SELECT vat FROM accumulated_amount_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') AS vat,
                                                                 (SELECT discount FROM void_tbl where or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') + (SELECT discount FROM accumulated_amount_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') AS discount,
                                                                 (SELECT SUM(vatable + vat_exempt + zero_rated + vat + total_amount) FROM void_tbl where or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') + (SELECT void FROM accumulated_amount_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "') AS void", conn)
                                    disc.CommandType = CommandType.Text
                                    Dim dda As New MySqlDataAdapter(disc)
                                    Dim ddt As New DataTable
                                    disc.CommandTimeout = 0

                                    Try
                                        dda.Fill(ddt)

                                        total_sales = ddt.Rows(0)(0).ToString
                                        vatable = ddt.Rows(0)(1).ToString
                                        vat_exempt = ddt.Rows(0)(2).ToString
                                        zero_rated = ddt.Rows(0)(3).ToString
                                        vat = ddt.Rows(0)(4).ToString
                                        discount = ddt.Rows(0)(5).ToString
                                        void_amount = ddt.Rows(0)(6).ToString

                                        'Try
                                        '    Dim sum_no_of_void, sum_total_amount, sum_vatable, sum_vat_exempt, sum_zero_rated, sum_vat, sum_discount, sum_void As String

                                        '    sum_no_of_void = no_of_void
                                        '    sum_total_amount = a_total_amount + total_amount
                                        '    sum_vatable = a_vatable + vatable
                                        '    sum_vat_exempt = a_vat_exempt + vat_exempt
                                        '    sum_zero_rated = a_zero_rated + zero_rated
                                        '    sum_vat = a_vat + vat
                                        '    sum_discount = a_discount + discount
                                        '    sum_void = Val(a_void) + Val(total_amount)

                                        '' TO EDIT
                                        Dim cdd As New MySqlCommand("UPDATE accumulated_amount_tbl SET no_of_void = (SELECT count(*) FROM void_tbl WHERE user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "'),
                                                                                 begin_void_no = (SELECT MIN(void_no) FROM void_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "'),
                                                                                 end_void_no = (SELECT MAX(void_no) FROM void_tbl where user_id = '" & mainform.LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "'), 
                                                                                 void_amount = @void, 
                                                                                 total_sales = @sales, 
                                                                                 vatable = @vatable, 
                                                                                 vat_exempt = @vat_exempt, 
                                                                                 zero_rated = @zero_rated, 
                                                                                 vat = @vat, 
                                                                                 discount = @discount,
                                                                                 updated = @updated
                                                                                 WHERE user_id = @user_id AND payment_date = '" & todaysdate & "' AND pos_id = '" & mainform.LabelPOSno.Text & "'", conn)

                                        cdd.CommandType = CommandType.Text
                                        cdd.Parameters.AddWithValue("@void", void_amount)
                                        cdd.Parameters.AddWithValue("@sales", total_sales)
                                        cdd.Parameters.AddWithValue("@vatable", vatable)
                                        cdd.Parameters.AddWithValue("@vat_exempt", vat_exempt)
                                        cdd.Parameters.AddWithValue("@zero_rated", zero_rated)
                                        cdd.Parameters.AddWithValue("@vat", vat)
                                        cdd.Parameters.AddWithValue("@discount", discount)
                                        cdd.Parameters.AddWithValue("@updated", "yes")
                                        cdd.Parameters.AddWithValue("@user_id", mainform.LabelCashierID.Text)

                                        'for printing
                                        Dim margins As New Margins(0, 0, 0, 0)
                                        Void.DefaultPageSettings.Margins = margins

                                        ''THERMAL
                                        Dim papersize As New PaperSize("Custom", 280, 1000)
                                        '' ------------- For MMDA
                                        'Dim papersize As New PaperSize("Custom", 280, 470)

                                        Void.DefaultPageSettings.PaperSize = papersize

                                        'PrintPreviewDialog1.Document = Void

                                        'AddHandler Void.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                                        'PrintPreviewDialog1.ShowDialog()

                                        'TO PRINT IMMEDIATELY
                                        PrintDocument1.Print()

                                        Dim readerr As MySqlDataReader
                                        TextBoxORNumber.Text = Nothing
                                        readerr = cdd.ExecuteReader
                                        readerr.Close()
                                        conn.Close()
                                        'Catch ex As Exception
                                        '    MsgBox("UPDATE:" & ex.Message, MessageBoxIcon.Warning)
                                        '    conn.Close()
                                        'End Try
                                        'Catch ex As Exception
                                        '    MsgBox("SELECT accu:" & ex.Message, MessageBoxIcon.Warning)
                                        '    conn.Close()
                                        'End Try
                                    Catch ex As Exception
                                        MsgBox("SELECT void:" & ex.Message, MessageBoxIcon.Warning)
                                        conn.Close()
                                    End Try

                                    conn.Close()
                                Catch ex As Exception
                                    MsgBox("INSERT audit" & ex.Message, MessageBoxIcon.Warning)
                                    conn.Close()
                                End Try
                                conn.Close()

                            Catch ex As Exception
                                MsgBox("INSERT" & ex.Message, MessageBoxIcon.Warning)
                                conn.Close()
                            End Try

                        Else

                            readerVoid.Close()
                            MsgBox("Ticket is already voided or expired!", MessageBoxIcon.Warning)
                            TextBoxORNumber.Text = Nothing
                            conn.Close()
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message & "Ticket already voided!", MessageBoxIcon.Warning)
                        readerVoid.Close()
                    End Try
                End Using
            End If

            'conn.Close()

            If dialog = DialogResult.No Then
                conn.Close()
            End If

        Catch ex As Exception
            MsgBox("LAST" & ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub auto()
        Dim seq As Long = 0
        mainform.voidRstCnt = 0

        conn.Open()
        Dim qd As String = "SELECT void_no, rst_cnt_v FROM void_tbl WHERE pos_id = '" & mainform.LabelPOSno.Text & "' ORDER BY payment_date DESC, payment_time DESC LIMIT 1"
        Dim cmd As New MySqlCommand(qd) With {.Connection = conn}
        Dim rdr As MySqlDataReader = cmd.ExecuteReader()
        If rdr.Read() Then
            If Not IsDBNull(rdr("void_no")) Then
                Dim voidNoStr As String = rdr("void_no").ToString()
                If Long.TryParse(voidNoStr, seq) Then
                    ' Successfully parsed exact Long
                Else
                    Dim parsedVal As Double = 0
                    If Double.TryParse(voidNoStr, parsedVal) Then
                        seq = Convert.ToInt64(parsedVal)
                    End If
                End If
            End If
            If Not IsDBNull(rdr("rst_cnt_v")) Then
                mainform.voidRstCnt = Convert.ToInt32(rdr("rst_cnt_v").ToString())
            End If
        End If
        rdr.Close()

        ' Check for 10-digit maximum
        If seq >= 9999999999 Then
            seq = 1
            mainform.voidRstCnt += 1
        Else
            seq += 1
        End If

        ' Format to exactly 10 digits
        LabelVoidNo.Text = seq.ToString("D10")

        conn.Close()
    End Sub


    Private Async Sub voidtransaction_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        hideFieldsInVoidForm()
        Button1.Enabled = False
        btnCancelFp.Visible = False

        _fpAuth = New FingerprintManagerAuth(strConn, 30000)

        AddHandler _fpAuth.StatusChanged,
        Sub(msg)
            If Me.IsHandleCreated Then
                Me.BeginInvoke(New Action(Sub() SetFpStatus(msg)))
            End If
        End Sub

        ' Start scanning until manager is detected
        _fpCts = New Threading.CancellationTokenSource()

        Await StartManagerScanLoopAsync()   ' ✅ now this works

        ' your other UI setup
        voidedOR.Hide()
        LabelVoidNo.Hide()
        ButtonVoid.Hide()
        Button1.Enabled = False

        PictureBoxBarcode.Hide()
        PictureBoxLogoReceipt.Hide()
        ComboReaderNames.Hide()
        ComboReaderNames.Enabled = False

    End Sub
    Private Async Function StartManagerScanLoopAsync() As Task
        While Not _fpCts.IsCancellationRequested AndAlso Not Me.IsDisposed
            Try
                SetFpStatus("Place MANAGER finger to authorize REPRINT...")

                Dim managerId As String = Await _fpAuth.AuthorizeOnceAsync(_fpCts.Token)

                If Not String.IsNullOrWhiteSpace(managerId) Then
                    If Me.IsHandleCreated Then
                        Me.BeginInvoke(New Action(Sub() TextBoxUserID.Text = managerId))
                    End If
                    Exit While
                End If

            Catch ex As TaskCanceledException
                Exit While
            Catch ex As Exception
                SetFpStatus("FP Error: " & ex.Message)
            End Try

            Await Task.Delay(500)
        End While
    End Function

    Private Sub btnCancelFp_Click(sender As Object, e As EventArgs) Handles btnCancelFp.Click
        Try
            If _fpCts IsNot Nothing Then _fpCts.Cancel()
            If _fpAuth IsNot Nothing Then _fpAuth.CancelAndCleanup()
            SetFpStatus("Cancelling...")
        Catch
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim id, pos_id, cashier_name, station, payment_date, payment_time, or_no, passenger_name, vessel_id, schedule, seat_no As String
            Dim origin, destination, trip_fare, baggage_cost, card_amount, total_amount, cash, change_amount, vatable, vat_exempt, zero_rated, vat, type_of_transaction As String
            Dim void_no As String

            'select data from ejournal_tbl to issue void ticket
            Dim valueEJ As New MySqlCommand("SELECT id, pos_id, cashier_name, station, DATE_FORMAT(payment_date, '%Y/%m/%d') AS payment_date, TIME_FORMAT(payment_time, '%H:%i') AS payment_time, or_no, passenger_name, vessel_id, schedule, seat_no,
            origin, destination, trip_fare, baggage_cost, card_amount, total_amount, cash, change_amount, discount, vatable, vat_exempt, zero_rated, vat, type_of_transaction FROM ejournal_tbl WHERE or_no = '" & TextBoxORNumber.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "'", conn)
            valueEJ.CommandType = CommandType.Text
            Dim valueSDAEJ As New MySqlDataAdapter(valueEJ)
            Dim valueDTEJ As New DataTable
            valueEJ.CommandTimeout = 0

            valueSDAEJ.Fill(valueDTEJ)

            id = valueDTEJ.Rows(0)(0).ToString
            pos_id = valueDTEJ.Rows(0)(1).ToString
            cashier_name = valueDTEJ.Rows(0)(2).ToString
            station = valueDTEJ.Rows(0)(3).ToString
            payment_date = valueDTEJ.Rows(0)(4)
            payment_time = valueDTEJ.Rows(0)(5).ToString
            or_no = valueDTEJ.Rows(0)(6).ToString
            passenger_name = valueDTEJ.Rows(0)(7).ToString
            vessel_id = valueDTEJ.Rows(0)(8).ToString
            schedule = valueDTEJ.Rows(0)(9).ToString
            seat_no = valueDTEJ.Rows(0)(10).ToString
            origin = valueDTEJ.Rows(0)(11).ToString
            destination = valueDTEJ.Rows(0)(12).ToString
            trip_fare = valueDTEJ.Rows(0)(13).ToString
            baggage_cost = valueDTEJ.Rows(0)(14).ToString
            card_amount = valueDTEJ.Rows(0)(15).ToString
            total_amount = valueDTEJ.Rows(0)(16).ToString
            cash = valueDTEJ.Rows(0)(17).ToString
            change_amount = valueDTEJ.Rows(0)(18).ToString
            discount = valueDTEJ.Rows(0)(19).ToString
            vatable = valueDTEJ.Rows(0)(20).ToString
            vat_exempt = valueDTEJ.Rows(0)(21).ToString
            zero_rated = valueDTEJ.Rows(0)(22).ToString
            vat = valueDTEJ.Rows(0)(23).ToString
            type_of_transaction = valueDTEJ.Rows(0)(24).ToString

            barcode.Code = TextBoxORNumber.Text

            Dim sumpayment = Convert.ToDecimal(Val(trip_fare) + Val(baggage_cost) + Val(card_amount)).ToString("0.00")

            Dim valueVID As New MySqlCommand("SELECT void_no FROM void_tbl WHERE or_no = '" & TextBoxORNumber.Text & "'", conn)
            valueVID.CommandType = CommandType.Text
            Dim valueSDA As New MySqlDataAdapter(valueVID)
            Dim valueDTVID As New DataTable
            valueVID.CommandTimeout = 0

            valueSDA.Fill(valueDTVID)

            void_no = valueDTVID.Rows(0)(0).ToString
            ''for THERMAL
            Dim TextToPrint As String = ""
            Dim N As New Font("Merchant Copy", 13)
            Dim S As New Font("Merchant Copy", 11)
            Dim H As New Font("Merchant Copy", 15)
            Dim B As New Font("Merchant Copy", 15, FontStyle.Bold)

            Dim sngCenterPage As Single

            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Far

            ''for centering company logo
            'get the printer resolution
            'Dim thisDpiX As Integer = e.Graphics.DpiX
            'Dim r As Single = thisDpiX / 96
            'Dim targetwidth As Integer = 235 * r

            ''get the size of the margins in pixels
            ''convert from inches to pixels
            ''your printer and windows may be set on units other than inches
            ''so you need to look at the values using debug
            'Dim marginwidthpixels = e.MarginBounds.Width * thisDpiX / 100
            ''determine the center of the margins which is half the margin size from the left margin
            'Dim c As Single = (marginwidthpixels / 2) + (e.MarginBounds.X * thisDpiX / 100)
            ''determine the left edge of the image from the center
            ''subtract half the drawing size from the center in pixels
            'Dim l As Integer = c - (targetwidth / 2)
            ''determine the top of the margins in pixels
            'Dim t As Integer = e.MarginBounds.Y * thisDpiX / 100

            TextToPrint &= Environment.NewLine
            e.Graphics.DrawImage(PictureBoxLogoReceipt.Image, 92, 0)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("CAVITE SUPERFERRY", N).Width / 2)
            e.Graphics.DrawString("CAVITE SUPERFERRY", N, Brushes.Black, sngCenterPage, 70)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PINOY CATAMARAN", N).Width / 2)
            e.Graphics.DrawString("PINOY CATAMARAN", N, Brushes.Black, sngCenterPage, 82)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("#17 SILAHIS STREET,", N).Width / 2)
            e.Graphics.DrawString("#17 SILAHIS STREET,", N, Brushes.Black, sngCenterPage, 94)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BARANGAY TANZA, NAVOTAS CITY", N).Width / 2)
            e.Graphics.DrawString("BARANGAY TANZA, NAVOTAS CITY", N, Brushes.Black, sngCenterPage, 106)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PHILIPPINES", N).Width / 2)
            e.Graphics.DrawString("PHILIPPINES", N, Brushes.Black, sngCenterPage, 118)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
            e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 130)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS" & pos_id & "-SN: " & mainform.LabelSerial.Text, N).Width / 2)
            e.Graphics.DrawString("POS" & pos_id & "-SN: " & mainform.LabelSerial.Text, N, Brushes.Black, sngCenterPage, 142)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("MIN: XXXXXXXXXX", N).Width / 2)
            e.Graphics.DrawString("MIN: XXXXXXXXXX", N, Brushes.Black, sngCenterPage, 154)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 165)

            e.Graphics.DrawString("Cashier: " & cashier_name & ControlChars.NewLine & "Station ID: " & station, N, Brushes.Black, 4, 175)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VOID", B).Width / 2)
            e.Graphics.DrawString("VOID", B, Brushes.Black, sngCenterPage, 200)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 210)

            e.Graphics.DrawString(payment_date & " " & payment_time, N, Brushes.Black, New RectangleF(4, 220, 0, 0))
            e.Graphics.DrawString("VOID #: " & void_no, N, Brushes.Black, New RectangleF(80, 220, 195, 195), sf)

            e.Graphics.DrawString("OR #: " & or_no, B, Brushes.Black, New RectangleF(4, 232, 0, 0))
            e.Graphics.DrawString("PHP", B, Brushes.Black, New RectangleF(80, 232, 195, 195), sf)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 241)

            e.Graphics.DrawString("NAME" & ControlChars.NewLine &
                                          "VESSEL ID" & ControlChars.NewLine &
                                          "SCHEDULE" & ControlChars.NewLine &
                                          "SEAT NO." & ControlChars.NewLine &
                                          "PT. OF ORIGIN" & ControlChars.NewLine &
                                          "PT. OF DESTINATION" & ControlChars.NewLine &
                                          "TRIP FARE" & ControlChars.NewLine &
                                          "BAGGAGE COST" & ControlChars.NewLine &
                                          "CARD AMOUNT", N, Brushes.Black, New RectangleF(4, 250, 0, 0))
            e.Graphics.DrawString(passenger_name & ControlChars.NewLine &
                                          vessel_id & ControlChars.NewLine &
                                          schedule & ControlChars.NewLine &
                                          seat_no & ControlChars.NewLine &
                                          origin & ControlChars.NewLine &
                                          destination & ControlChars.NewLine &
                                          trip_fare & ControlChars.NewLine &
                                          baggage_cost & ControlChars.NewLine &
                                          card_amount, N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 358)

            barcode.Resolution = 155

            barcode.Format = System.Drawing.Imaging.ImageFormat.Gif
            PictureBoxBarcode.Image = barcode.drawBarcodeOnBitmap()

            e.Graphics.DrawImage(PictureBoxBarcode.Image, 6, 367)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(or_no, N).Width / 2)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 411)

            e.Graphics.DrawString("TOTAL AMOUNT" & ControlChars.NewLine & "CASH" & ControlChars.NewLine & "CHANGE", N, Brushes.Black, New RectangleF(4, 420, 0, 0))
            e.Graphics.DrawString(total_amount & ControlChars.NewLine & cash & ControlChars.NewLine & change_amount, N, Brushes.Black, New RectangleF(80, 420, 195, 195), sf)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 456)

            If zero_rated = "" Or zero_rated = "0.00" Then

                e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine &
                                  "VAT Amount" & ControlChars.NewLine &
                                  "(X) VAT-Exempt Sales" & ControlChars.NewLine &
                                  "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                e.Graphics.DrawString(vatable & ControlChars.NewLine &
                                      vat & ControlChars.NewLine &
                                      vat_exempt & ControlChars.NewLine &
                                      zero_rated & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

            Else

                e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine &
                                  "VAT Amount" & ControlChars.NewLine &
                                  "(X) VAT-Exempt Sales" & ControlChars.NewLine &
                                  "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                e.Graphics.DrawString(vatable & ControlChars.NewLine &
                                      vat & ControlChars.NewLine &
                                      vat_exempt & ControlChars.NewLine &
                                      "('" & sumpayment & "' - VAT)" & zero_rated & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

            End If

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 517)

            e.Graphics.DrawString("Discount", N, Brushes.Black, New RectangleF(4, 528, 0, 0))
            e.Graphics.DrawString(discount & ControlChars.NewLine & type_of_transaction, N, Brushes.Black, New RectangleF(80, 528, 195, 195), sf)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 547)

            e.Graphics.DrawString("THIS DOCUMENT IS NOT VALID FOR CLAIM OF INPUT TAX", S, Brushes.Black, 1, 558)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 568)

            e.Graphics.DrawString("Name: _________________________________" & ControlChars.NewLine &
                                      "Address: ______________________________" & ControlChars.NewLine &
                                      "_______________________________________" & ControlChars.NewLine &
                                      "ID #: _________________________________" & ControlChars.NewLine &
                                      "OSCA/SC ID #: _________________________" & ControlChars.NewLine &
                                      "PWD ID #: _____________________________" & ControlChars.NewLine &
                                      "TIN #: ________________________________" & ControlChars.NewLine &
                                      "Bus Style: ____________________________" & ControlChars.NewLine &
                                      "Signature: ____________________________", N, Brushes.Black, New RectangleF(4, 578, 0, 0))

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 690)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VINTATECH", N).Width / 2)
            e.Graphics.DrawString("VINTATECH", N, Brushes.Black, sngCenterPage, 700)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Unit 507 F&L Bldg. 2211", N).Width / 2)
            e.Graphics.DrawString("Unit 507 F&L Bldg. 2211", N, Brushes.Black, sngCenterPage, 712)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Commonwealth, Holy Spirit 1127", N).Width / 2)
            e.Graphics.DrawString("Commonwealth, Holy Spirit 1127", N, Brushes.Black, sngCenterPage, 724)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Quezon City", N).Width / 2)
            e.Graphics.DrawString("Quezon City", N, Brushes.Black, sngCenterPage, 736)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
            e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 748)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Accred. No.: XXXXXXXXXXX", N).Width / 2)
            e.Graphics.DrawString("Accred. No.: XXXXXXXXXXX", N, Brushes.Black, sngCenterPage, 760)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Date Issued: XX/XX/XXXX", N).Width / 2)
            e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 772)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
            e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 784)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PTU No.: XXXXXXXXX", N).Width / 2)
            e.Graphics.DrawString("PTU No.: XXXXXXXXX", N, Brushes.Black, sngCenterPage, 796)
            e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 808)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
            e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 820)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 830)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS DOCUMENT SHALL BE VALID FOR", N).Width / 2)
            e.Graphics.DrawString("THIS DOCUMENT SHALL BE VALID FOR", N, Brushes.Black, sngCenterPage, 840)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("FIVE (5) YEARS FROM THE DATE", N).Width / 2)
            e.Graphics.DrawString("FIVE (5) YEARS FROM THE DATE", N, Brushes.Black, sngCenterPage, 852)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OF THE PERMIT TO USE.", N).Width / 2)
            e.Graphics.DrawString("OF THE PERMIT TO USE.", N, Brushes.Black, sngCenterPage, 864)

            '==================================APPENDED AND INSERTING TO E JOURNAL===============================

            Try

                Dim filepath As String = "C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"
                '' Dim filepath As String = "C:\E-Journal\" & "2019-11-14" & ".txt"

                Dim sw As StreamWriter

                If (Not File.Exists(filepath)) Then

                    sw = File.CreateText(filepath)
                    sw.WriteLine("           CAVITE SUPERFERRY")
                    sw.WriteLine("            PINOY CATAMARAN")
                    sw.WriteLine("          #17 SILAHIS STREET,")
                    sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                    sw.WriteLine("              PHILIPPINES")
                    sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                    sw.WriteLine("       POS" & pos_id & "-SN: " & mainform.LabelSerial.Text)
                    sw.WriteLine("            MIN: XXXXXXXXXX")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Cashier: " & cashier_name)
                    sw.WriteLine("Station ID: " & station)
                    sw.WriteLine("                 VOID")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine(payment_date & " " & payment_time & " VOID #:" & void_no)
                    sw.WriteLine("OR #: " & or_no & "                  PHP")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("NAME                                " & passenger_name)
                    sw.WriteLine("VESSEL ID                           " & vessel_id)
                    sw.WriteLine("SCHEDULE                            " & schedule)
                    sw.WriteLine("SEAT NO.                            " & seat_no)
                    sw.WriteLine("PT. OF ORIGIN                       " & origin)
                    sw.WriteLine("PT. OF DESTINATION                  " & destination)
                    sw.WriteLine("TRIP FARE                           " & trip_fare)
                    sw.WriteLine("BAGGAGE COST                        " & baggage_cost)
                    sw.WriteLine("CARD AMOUNT                         " & card_amount)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("TOTAL AMOUNT                        " & total_amount)
                    sw.WriteLine("CASH                                " & cash)
                    sw.WriteLine("CHANGE                              " & change_amount)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("(V) VATable Sales                   " & vatable)
                    sw.WriteLine("VAT Amount                          " & vat)
                    sw.WriteLine("(X) VAT-Exempt Sales                " & vat_exempt)
                    If zero_rated = "" Or zero_rated = "0.00" Then
                        sw.WriteLine("(Z) Zero-Rated Sales                " & zero_rated)

                    Else
                        sw.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & zero_rated)
                    End If
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Discount                            " & discount)
                    sw.WriteLine("                                    " & type_of_transaction)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("THIS DOCUMENT IS NOT VALID FOR CLAIM OF INPUT TAX")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Name: _________________________________")
                    sw.WriteLine("Address: ______________________________")
                    sw.WriteLine("_______________________________________")
                    sw.WriteLine("TIN #: ________________________________")
                    sw.WriteLine("ID #: _________________________________")
                    sw.WriteLine("Bus Style: ____________________________")
                    sw.WriteLine("Signature: ____________________________")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("               VINTATECH")
                    sw.WriteLine("        Unit 507 F&L Bldg. 2211")
                    sw.WriteLine("     Commonwealth, Holy Spirit 1127")
                    sw.WriteLine("              Quezon City")
                    sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                    sw.WriteLine("        Accred. No.: XXXXXXXXXXX")
                    sw.WriteLine("        Date Issued: XX/XX/XXXX")
                    sw.WriteLine("        Valid Until: XX/XX/XXXX")
                    sw.WriteLine("          PTU No.: XXXXXXXXX")
                    sw.WriteLine("        Date Issued: XX/XX/XXXX")
                    sw.WriteLine("        Valid Until: XX/XX/XXXX")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("   THIS DOCUMENT SHALL BE VALID FOR")
                    sw.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                    sw.WriteLine("         OF THE PERMIT TO USE.")
                    sw.WriteLine("")
                    sw.WriteLine("")
                    sw.WriteLine("")

                Else
                    sw = File.AppendText(filepath)

                    sw.WriteLine("           CAVITE SUPERFERRY")
                    sw.WriteLine("            PINOY CATAMARAN")
                    sw.WriteLine("          #17 SILAHIS STREET,")
                    sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                    sw.WriteLine("              PHILIPPINES")
                    sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                    sw.WriteLine("       POS" & pos_id & "-SN: " & mainform.LabelSerial.Text)
                    sw.WriteLine("            MIN: XXXXXXXXXX")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Cashier: " & cashier_name)
                    sw.WriteLine("Station ID: " & station)
                    sw.WriteLine("                 VOID")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine(payment_date & " " & payment_time & " VOID #:" & void_no)
                    sw.WriteLine("OR #: " & or_no & "                  PHP")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("NAME                                " & passenger_name)
                    sw.WriteLine("VESSEL ID                           " & vessel_id)
                    sw.WriteLine("SCHEDULE                            " & schedule)
                    sw.WriteLine("SEAT NO.                            " & seat_no)
                    sw.WriteLine("PT. OF ORIGIN                       " & origin)
                    sw.WriteLine("PT. OF DESTINATION                  " & destination)
                    sw.WriteLine("TRIP FARE                           " & trip_fare)
                    sw.WriteLine("BAGGAGE COST                        " & baggage_cost)
                    sw.WriteLine("CARD AMOUNT                         " & card_amount)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("TOTAL AMOUNT                        " & total_amount)
                    sw.WriteLine("CASH                                " & cash)
                    sw.WriteLine("CHANGE                              " & change_amount)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("(V) VATable Sales                   " & vatable)
                    sw.WriteLine("VAT Amount                          " & vat)
                    sw.WriteLine("(X) VAT-Exempt Sales                " & vat_exempt)
                    If zero_rated = "" Or zero_rated = "0.00" Then
                        sw.WriteLine("(Z) Zero-Rated Sales                " & zero_rated)

                    Else
                        sw.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & zero_rated)
                    End If
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Discount                            " & discount)
                    sw.WriteLine("                                    " & type_of_transaction)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("THIS DOCUMENT IS NOT VALID FOR CLAIM OF INPUT TAX")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Name: _________________________________")
                    sw.WriteLine("Address: ______________________________")
                    sw.WriteLine("_______________________________________")
                    sw.WriteLine("TIN #: ________________________________")
                    sw.WriteLine("ID #: _________________________________")
                    sw.WriteLine("Bus Style: ____________________________")
                    sw.WriteLine("Signature: ____________________________")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("               VINTATECH")
                    sw.WriteLine("        Unit 507 F&L Bldg. 2211")
                    sw.WriteLine("     Commonwealth, Holy Spirit 1127")
                    sw.WriteLine("              Quezon City")
                    sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                    sw.WriteLine("        Accred. No.: XXXXXXXXXXX")
                    sw.WriteLine("        Date Issued: XX/XX/XXXX")
                    sw.WriteLine("        Valid Until: XX/XX/XXXX")
                    sw.WriteLine("          PTU No.: XXXXXXXXX")
                    sw.WriteLine("        Date Issued: XX/XX/XXXX")
                    sw.WriteLine("        Valid Until: XX/XX/XXXX")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("   THIS DOCUMENT SHALL BE VALID FOR")
                    sw.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                    sw.WriteLine("         OF THE PERMIT TO USE.")
                    sw.WriteLine("")
                    sw.WriteLine("")
                    sw.WriteLine("")

                End If


                sw.Close()

                Dim fillInfo As FileInfo

                fillInfo = New FileInfo(filepath)
                If fillInfo.Exists = True Then
                    fillInfo.Attributes = FileAttributes.Encrypted
                End If

            Catch ex As Exception
                MsgBox(ex.Message, "Error writing to log file.")
            End Try

            Dim files As System.IO.StreamWriter


            files = My.Computer.FileSystem.OpenTextFileWriter("C: \E-Journal\" & void_no & ".txt", False)


            files.WriteLine("           CAVITE SUPERFERRY")
            files.WriteLine("            PINOY CATAMARAN")
            files.WriteLine("          #17 SILAHIS STREET,")
            files.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
            files.WriteLine("              PHILIPPINES")
            files.WriteLine("      VAT REG TIN: 745-993-747-000")
            files.WriteLine("       POS" & pos_id & "-SN: " & mainform.LabelSerial.Text)
            files.WriteLine("            MIN: XXXXXXXXXX")
            files.WriteLine("----------------------------------------")
            files.WriteLine("Cashier: " & cashier_name)
            files.WriteLine("Station ID: " & station)
            files.WriteLine("                 VOID")
            files.WriteLine("----------------------------------------")
            files.WriteLine(payment_date & " " & payment_time & " VOID #:" & void_no)
            files.WriteLine("OR #: " & or_no & "                  PHP")
            files.WriteLine("----------------------------------------")
            files.WriteLine("NAME                                " & passenger_name)
            files.WriteLine("VESSEL ID                           " & vessel_id)
            files.WriteLine("SCHEDULE                            " & schedule)
            files.WriteLine("SEAT NO.                            " & seat_no)
            files.WriteLine("PT. OF ORIGIN                       " & origin)
            files.WriteLine("PT. OF DESTINATION                  " & destination)
            files.WriteLine("TRIP FARE                           " & trip_fare)
            files.WriteLine("BAGGAGE COST                        " & baggage_cost)
            files.WriteLine("CARD AMOUNT                         " & card_amount)
            files.WriteLine("----------------------------------------")
            files.WriteLine("TOTAL AMOUNT                        " & total_amount)
            files.WriteLine("CASH                                " & cash)
            files.WriteLine("CHANGE                              " & change_amount)
            files.WriteLine("----------------------------------------")
            files.WriteLine("(V) VATable Sales                   " & vatable)
            files.WriteLine("VAT Amount                          " & vat)
            files.WriteLine("(X) VAT-Exempt Sales                " & vat_exempt)
            If zero_rated = "" Or zero_rated = "0.00" Then
                files.WriteLine("(Z) Zero-Rated Sales                " & zero_rated)

            Else
                files.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & zero_rated)
            End If
            files.WriteLine("----------------------------------------")
            files.WriteLine("Discount                            " & discount)
            files.WriteLine("                                    " & type_of_transaction)
            files.WriteLine("----------------------------------------")
            files.WriteLine("THIS DOCUMENT IS NOT VALID FOR CLAIM OF INPUT TAX")
            files.WriteLine("----------------------------------------")
            files.WriteLine("Name: _________________________________")
            files.WriteLine("Address: ______________________________")
            files.WriteLine("_______________________________________")
            files.WriteLine("TIN #: ________________________________")
            files.WriteLine("ID #: _________________________________")
            files.WriteLine("Bus Style: ____________________________")
            files.WriteLine("Signature: ____________________________")
            files.WriteLine("----------------------------------------")
            files.WriteLine("               VINTATECH")
            files.WriteLine("        Unit 507 F&L Bldg. 2211")
            files.WriteLine("     Commonwealth, Holy Spirit 1127")
            files.WriteLine("              Quezon City")
            files.WriteLine("      VAT REG TIN: 745-993-747-000")
            files.WriteLine("        Accred. No.: XXXXXXXXXXX")
            files.WriteLine("        Date Issued: XX/XX/XXXX")
            files.WriteLine("        Valid Until: XX/XX/XXXX")
            files.WriteLine("          PTU No.: XXXXXXXXX")
            files.WriteLine("        Date Issued: XX/XX/XXXX")
            files.WriteLine("        Valid Until: XX/XX/XXXX")
            files.WriteLine("----------------------------------------")
            files.WriteLine("   THIS DOCUMENT SHALL BE VALID FOR")
            files.WriteLine("     FIVE (5) YEARS FROM THE DATE")
            files.WriteLine("         OF THE PERMIT TO USE.")

            files.Close()

            Dim rawData() As Byte = IO.File.ReadAllBytes("C:\E-Journal\" & void_no & ".txt")

            Try

                Try
                    Dim cdd As New MySqlCommand("UPDATE ejournal_tbl SET text_file = @text_file WHERE or_no = @or_no", conn)
                    cdd.CommandType = CommandType.Text
                    cdd.Parameters.AddWithValue("@text_file", rawData)
                    cdd.Parameters.AddWithValue("@or_no", or_no)
                    Dim reader As MySqlDataReader

                    reader = cdd.ExecuteReader
                    reader.Close()

                    'conn.Close()


                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try



            My.Computer.FileSystem.DeleteFile("C:\E-Journal\" & void_no & ".txt")

            '==================================APPENDED AND INSERTING TO E JOURNAL===============================

            sf.Dispose()
            ''End of THERMAL

        Catch ex As Exception
            PrintPreviewDialog1.Close()
            MsgBox(ex.Message, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub hideFieldsInVoidForm()
        btnCancelFp.Hide()
        ButtonVoid.Hide()
        ComboReaderNames.Hide()
        PictureBoxLogoReceipt.Hide()
        PictureBoxBarcode.Hide()
        voidedOR.Hide()
        LabelVoidNo.Hide()
    End Sub
End Class