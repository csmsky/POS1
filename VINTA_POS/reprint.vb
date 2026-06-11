Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass

Public Class reprint
    ' ===========================
    ' DB + PRINT
    ' ===========================
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    Dim WithEvents Reprint As PrintDocument = New PrintDocument
    Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
    Dim barcode As BusinessRefinery.Barcode.PDF417 = New BusinessRefinery.Barcode.PDF417()
    Dim barcodeticket As BusinessRefinery.Barcode.PDF417 = New BusinessRefinery.Barcode.PDF417()
    Dim index As Integer

    Private _fpAuth As FingerprintManagerAuth
    Private _fpCts As Threading.CancellationTokenSource

    ' ===========================
    ' CARD READER (COMMENTED / DISABLED)
    ' ===========================
    'Private _pcscReader As PscsReader
    'Private _cardPolling As CardPolling
    'Dim apdu As Apdu = New Apdu
    'Dim command() As Byte

    'for printing multiple pages
    Dim page_number As Byte

    Public Sub OnThreadException(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        MessageBox.Show(e.Exception.Message)
    End Sub

    ' ===========================
    ' CARD EVENTS (DISABLED)
    ' ===========================
    Private Sub cardPolling_OnCardFound(ByVal sender As Object, ByVal e As CardPollingEventArg)
        ' DISABLED - fingerprint will be used instead
    End Sub

    Private Sub cardPolling_OnError(ByVal sender As Object, ByVal e As CardPollingErrorEventArg)
        MessageBox.Show("Invalid")
    End Sub

    Private Sub cardPolling_OnCardRemoved(ByVal sender As Object, ByVal e As CardPollingEventArg)
        TextBoxUserID.Clear()
        ButtonReprint.Enabled = False
    End Sub

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

    Private Async Sub reprint_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Always start disabled
        btnCancelFp.Visible = False
        ButtonReprint.Enabled = False
        TextBoxUserID.Clear()

        _fpAuth = New FingerprintManagerAuth(strConn, 30000)

        AddHandler _fpAuth.StatusChanged, Sub(msg)
                                              If Me.IsHandleCreated Then
                                                  Me.BeginInvoke(New Action(Sub() SetFpStatus(msg)))
                                              End If
                                          End Sub

        PictureBoxLogoReceipt.Image = WindowsApplication1.My.Resources.LogoReceipt

        loaddata()
        datagrid_design()

        PictureBoxBarcode.Hide()
        PictureBoxBarcodeTicket.Hide()
        PictureBoxLogoReceipt.Hide()

        ComboReaderNames.Hide()

        ' Start scanning until manager is detected
        _fpCts = New Threading.CancellationTokenSource()
        Await StartManagerScanLoopAsync()

    End Sub

    Private Async Function StartManagerScanLoopAsync() As Threading.Tasks.Task
        While Not _fpCts.IsCancellationRequested AndAlso Not Me.IsDisposed

            Try
                SetFpStatus("Place MANAGER finger to authorize REPRINT...")

                Dim managerId As String = Await _fpAuth.AuthorizeOnceAsync(_fpCts.Token)

                If Not String.IsNullOrWhiteSpace(managerId) Then
                    ' Put manager id in textbox (this triggers TextChanged)
                    If Me.IsHandleCreated Then
                        Me.BeginInvoke(New Action(Sub()
                                                      TextBoxUserID.Text = managerId
                                                  End Sub))
                    End If

                    Exit While ' stop loop after success
                End If

            Catch ex As Threading.Tasks.TaskCanceledException
                Exit While
            Catch ex As Exception
                SetFpStatus("FP Error: " & ex.Message)
            End Try

            ' small delay to avoid rapid loop
            Await Threading.Tasks.Task.Delay(500)

        End While
    End Function

    Private Sub loaddata()
        'show data from ejournal to reprint
        Try
            Dim cmd As New MySqlCommand
            Dim ds As New DataSet()
            Dim adapter As New MySqlDataAdapter

            cmd.CommandText = "SELECT or_no, qty, total_amount, payment_method, payment_date, payment_time, void_status FROM e_journal_tbl WHERE pos_id = '" & mainform.LabelPOSno.Text & "' ORDER BY or_no DESC"

            adapter.SelectCommand = cmd
            adapter.SelectCommand.Connection = conn

            adapter.Fill(ds, "or_tbl")
            DataGridView1.DataSource = ds.Tables(0)
            DataGridView1.Refresh()

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub datagrid_design()
        'design of datagrid
        DataGridView1.BorderStyle = BorderStyle.None
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        DataGridView1.BackgroundColor = Color.White

        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewAdvancedCellBorderStyle.None
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            ' 1) Ignore header clicks
            If e.RowIndex < 0 Then Exit Sub

            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' 2) Ignore the new row (blank row at bottom)
            If selectedRow.IsNewRow Then Exit Sub

            ' 3) Use correct column indexes based on your grid:
            ' 0 or_no
            ' 1 qty
            ' 2 total_amount
            ' 3 payment_method
            ' 4 payment_date
            ' 5 payment_time
            ' 6 void_status

            LabelORValue.Text = SafeCellText(selectedRow.Cells(0))
            LabelCashierValue.Text = SafeCellText(selectedRow.Cells(3))   ' (example: payment_method)
            LabelDateValue.Text = SafeCellText(selectedRow.Cells(4))
            LabelTimeValue.Text = SafeCellText(selectedRow.Cells(5))

            loaddata()

        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try
    End Sub

    Private Function SafeCellText(cell As DataGridViewCell) As String
        If cell Is Nothing OrElse cell.Value Is Nothing OrElse IsDBNull(cell.Value) Then
            Return ""
        End If
        Return cell.Value.ToString()
    End Function
    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        'search passenger
        Try
            FilterData(TextBoxSearch.Text)
        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("No Recorded Data!")
        End Try
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        FilterData("")
    End Sub

    Public Sub FilterData(valueToSearch As String)

        Dim searchQuery As String = "SELECT or_no, qty, total_amount, payment_method, username, DATE_FORMAT(payment_date, '%d/%m/%Y') AS payment_date, TIME_FORMAT(payment_time, '%H:%i:%s') AS payment_time, void_status FROM or_tbl WHERE CONCAT(or_no) like '%" & valueToSearch & "%' AND pos_id = '" & mainform.LabelPOSno.Text & "'"
        Dim command As New MySqlCommand(searchQuery, conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable
        adapter.Fill(table)
        DataGridView1.DataSource = table
    End Sub

    Private Sub ButtonReprint_Click(sender As Object, e As EventArgs) Handles ButtonReprint.Click

        Dim or_no As String = LabelORValue.Text

        If LabelDateValue.Text = "" OrElse LabelTimeValue.Text = "" Then
            MsgBox("Please select data to reprint!", MessageBoxIcon.Warning)
            'Return
        Else
            PrintMultipleTables(or_no)
        End If
    End Sub

    'For printing Sale Invoice Receipt 
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
    Private Sub PrintMultipleTablesFromTextBox()
        ' Get the or_no from the TextBox
        Dim or_no As String = LabelORValue.Text.Trim()

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
                                                Dim font As New Font("Tahoma", 9)
                                                Dim graphics As Graphics = e.Graphics
                                                Dim brush As New SolidBrush(Color.Black)

                                                Dim centerFormat As New StringFormat()
                                                centerFormat.Alignment = StringAlignment.Center
                                                centerFormat.LineAlignment = StringAlignment.Center

                                                Dim N As New Font("Tahoma", 9)
                                                Dim NB As New Font("Tahoma", 9, FontStyle.Bold)
                                                Dim S As New Font("Tahoma", 8)
                                                Dim M As New Font("Tahoma", 8, FontStyle.Bold)
                                                Dim H As New Font("Tahoma", 11)
                                                Dim B As New Font("Tahoma", 13, FontStyle.Bold)

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
                                                CenterText(e, "POS" & pos & " S/N: " & serial, N, yPos)
                                                yPos += 15
                                                CenterText(e, MIN_No, N, yPos)
                                                yPos += 15
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20
                                                CenterText(e, "SALES INVOICE", B, yPos)
                                                yPos += 20
                                                CenterText(e, "(Re-print)", NB, yPos)
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
                                                    graphics.DrawString("AMOUNT DUE :", NB, brush, leftMargin, yPos)
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
                                                    graphics.DrawString("VAT(12%)", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("vat")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("VAT-Exempt:", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("vat_exempt")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("Zero-Rated:", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("zero_rated")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("Less Vat(12%):", font, brush, leftMargin, yPos)
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


                                                yPos += 30
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 30
                                                CenterText(e, "THIS SERVES AS YOUR SALES INVOICE", M, yPos)
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

        ' Directly send to printer
        printDocument.Print()

    End Sub

    Private Sub TextBoxUserID_TextChanged(sender As Object, e As EventArgs) Handles TextBoxUserID.TextChanged

        If String.IsNullOrWhiteSpace(TextBoxUserID.Text) Then
            ButtonReprint.Enabled = False
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
                    ButtonReprint.Enabled = (n > 0)
                End Using
            End Using

            If ButtonReprint.Enabled Then
                SetFpStatus("Manager authorized ✅ You may now reprint.")
            Else
                SetFpStatus("Matched user is not Manager.")
            End If

        Catch ex As Exception
            ButtonReprint.Enabled = False
            SetFpStatus("DB Error: " & ex.Message)
        End Try

    End Sub

    Private Sub btnCancelFp_Click(sender As Object, e As EventArgs) Handles btnCancelFp.Click
        Try
            If _fpCts IsNot Nothing Then _fpCts.Cancel()
            If _fpAuth IsNot Nothing Then _fpAuth.CancelAndCleanup()
            SetFpStatus("Cancelling...")
        Catch
        End Try
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Try
            If _fpCts IsNot Nothing Then _fpCts.Cancel()
            If _fpAuth IsNot Nothing Then _fpAuth.CancelAndCleanup()
        Catch
        End Try

        Me.Close()
        mainform.Show()
    End Sub
End Class