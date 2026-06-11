Imports System.Drawing.Printing
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass
Imports System.Security.AccessControl
Imports System.Security.Principal

Imports System.Diagnostics
Imports System.Text

Public Class management
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps
    Dim station = AppConfigReader.sttn
    Dim serial = AppConfigReader.srl

    'X and Z Report
    Dim PrintXReading As PrintDocument = New PrintDocument
    Dim PrintZReading As PrintDocument = New PrintDocument

    Dim cashier_id As String

    'TRY LOCK EJOURNAL
    Public status As String
    Private arr As String() = New String(5) {}
    Dim ejdir As String

    Private WithEvents previewZReading As New PrintPreviewDialog
    Private receiptBuilder As New System.Text.StringBuilder()

    Dim Company_Name As String = "METTA CITY"
    Dim Company_Address As String = "San Juan City"
    Dim Companny_TIN As String = "VAT REG TIN: XXX-XXX-XXX-XXX"
    Dim Companny_Min As String = "XXXXXXXX"

    Private zr As ZReading
    Private prevGrand As Decimal


    'TRY LOCK EJOURNAL
    Private Sub management_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'audit_trail_tblDataSet.audit_trail_tbl' table. You can move, or remove it, as needed.
        ' Me.audit_trail_tblTableAdapter.Fill(Me.audit_trail_tblDataSet.audit_trail_tbl)
        'Added

        My.Computer.FileSystem.CreateDirectory("C:\E-Journal")
        LabelSerial.Text = serial

        'Software Versioning
        LabelSoftwareName.Text = Application.ProductName
        LabelVersionNo.Text = Application.ProductVersion

        'display current datetime in datetimepicker
        DateTimePicker1AuditTrail.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        DateTimePicker2AuditTrail.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

        'X & Z Report
        DateTimePickerXReport.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        DateTimePickerZStart.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        DateTimePickerEJ1.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        DateTimePickerEJ2.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

        'for taking the company logo from resources
        'PictureBoxCompanyLogo.Image = WindowsApplication1.My.Resources.Logo
        PictureBoxLogoReceipt.Image = WindowsApplication1.My.Resources.LogoReceipt
        PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA
        LabelPOSno.Text = pos
        LabelStationID.Text = station

        ComboBoxXCashier.Text = "-- Please Choose Cashier --"
        PictureBoxLogoReceipt.Hide()
        LabelLoginID.Hide()
        LabelUserID.Hide()
        LabelSerial.Hide()
        ButtonPrintZReport.Hide()
        ReportViewerAuditTrail.Hide()
        ButtonShowAll.Hide()
        LabelUserActivityLog.Hide()
        DateTimePicker1AuditTrail.Hide()
        LabelTO2.Hide()
        DateTimePicker2AuditTrail.Hide()
        ButtonSearchVoid.Hide()
        ButtonReports.Hide()
        LabelStation.Hide()
        LabelStationID.Hide()
        ButtonDownload.Hide()
        DateTimePickerEJ2.Hide()
        ButtonSouvenir.Hide()

        'TRY LOCK EJOURNAL
        status = ""
        arr(0) = ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}"
        arr(1) = ".{21EC2020-3AEA-1069-A2DD-08002B30309D}"
        arr(2) = ".{2559a1f4-21d7-11d4-bdaf-00c04f60b9f0}"
        arr(3) = ".{645FF040-5081-101B-9F08-00AA002F954E}"
        arr(4) = ".{2559a1f1-21d7-11d4-bdaf-00c04f60b9f0}"
        arr(5) = ".{7007ACC7-3202-11D1-AAD2-00805FC1270E}"

    End Sub

    '---------- X Report ----------'
    Private Sub DateTimePickerXReport_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DateTimePickerXReport.ValueChanged
        'Try
        '    Dim adapter As New MySqlDataAdapter("SELECT DISTINCT(CONCAT(CAST(AES_DECRYPT(first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(last_name, 'strdjnltmyp') AS CHAR(50)))) AS fullname, u.user_id AS cashier FROM user_registration_tbl u
        '                                    INNER JOIN accumulated_amount_tbl a ON u.user_id = a.user_id WHERE payment_date = '" & DateTimePickerXReport.Text & "'", conn)
        '    Dim table As New DataTable()

        '    adapter.Fill(table)

        '    'cashier_id = table.Rows(0)(1).ToString()

        '    ComboBoxXCashier.DataSource = table
        '    ComboBoxXCashier.DisplayMember = "fullname"
        '    'ComboBoxXCashier.SelectedItem.ValueMember = "cashier"
        '    'Label1.Text = ComboBoxXCashier.SelectedItem.ValueMember
        'Catch ex As Exception
        '    ComboBoxXCashier.DataSource = Nothing
        '    MsgBox(ex.Message)
        'End Try

        'List of cashier
        Try
            Dim adapter As New MySqlDataAdapter("SELECT DISTINCT(CAST(AES_DECRYPT(first_name, 'strdjnltmyp') AS CHAR(50))) AS fullname, u.user_id AS cashier FROM user_registration_tbl u
                                            INNER JOIN accumulated_amount_tbl a ON u.user_id = a.user_id WHERE payment_date = '" & DateTimePickerXReport.Text & "' AND pos_id = '" & LabelPOSno.Text & "'", conn)
            Dim table As New DataTable()

            adapter.Fill(table)
            ComboBoxXCashier.DataSource = table
            ComboBoxXCashier.Text = "-- Please Choose Cashier --"
            ComboBoxXCashier.ValueMember = "cashier"
            ComboBoxXCashier.DisplayMember = "fullname"
        Catch ex As Exception
            MsgBox(ex.Message)
            ComboBoxXCashier.DataSource = Nothing
        End Try

        ' add the handler after populating the ComboBox to avoid unwanted firing of the event...
        AddHandler ComboBoxXCashier.SelectedIndexChanged, AddressOf ComboBoxXCashier_SelectedIndexChanged
    End Sub

    Private Sub ComboBoxXCashier_SelectedIndexChanged(sender As Object, e As EventArgs)
        'get cashier id on select
        Dim cb = DirectCast(sender, ComboBox)
        ' SelectedValue is an Object - you can get the name of its actual type with .SelectedValue.GetType().Name
        cashier_id = (cb.SelectedValue).ToString
    End Sub

    Private Sub DateTimePickerXReport_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles DateTimePickerXReport.DropDown
        RemoveHandler DateTimePickerXReport.ValueChanged, AddressOf DateTimePickerXReport_ValueChanged
    End Sub

    Private Sub DateTimePickerXReport_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerXReport.CloseUp
        AddHandler DateTimePickerXReport.ValueChanged, AddressOf DateTimePickerXReport_ValueChanged
        Call DateTimePickerXReport_ValueChanged(sender, EventArgs.Empty)
    End Sub

    Private Sub ButtonPrintXReport_Click(sender As Object, e As EventArgs) Handles ButtonPrintXReport.Click
        Try

            If ComboBoxXCashier.Text = "-- Please Choose Cashier --" Then
                MsgBox("Please Choose Cashier!", MessageBoxIcon.Exclamation)
            Else
                ' Set printer name dynamically here (or you can hardcode it)
                PrintDocument1.PrinterSettings.PrinterName = "Posiflex PP9000 Printer"  ' Replace with your printer name

                Dim margins As New Margins(0, 0, 0, 0)
                PrintXReading.DefaultPageSettings.Margins = margins

                ''THERMAL
                'Dim papersize As New PaperSize("Custom", 280, 0)
                Dim papersize As New PaperSize("Custom", 280, 1500)

                PrintXReading.DefaultPageSettings.PaperSize = papersize

                Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                PrintPreviewDialog1.Document = PrintXReading

                ' Check if the printer is valid
                If Not PrintDocument1.PrinterSettings.IsValid Then
                    AddHandler PrintXReading.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                    PrintPreviewDialog1.ShowDialog()

                    'MsgBox("Printer is not valid!")
                    'Return
                Else
                    'TO PRINT IMMEDIATELY
                    PrintDocument1.Print()
                End If

                ' audit_trail_tbl
                'Dim BranchCode As String = "BR01"
                Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

                Using conn As New MySqlConnection(strConn)
                    conn.Open()

                    Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                        cmd.Parameters.AddWithValue("p_userid", mainform.LabelCashierID.Text)
                        cmd.Parameters.AddWithValue("p_username", mainform.LabelCashierName.Text)
                        cmd.Parameters.AddWithValue("p_approvedby", login.TextBoxUserID.Text)
                        cmd.Parameters.AddWithValue("p_activity_performed", "Generate X-Reading")
                        cmd.Parameters.AddWithValue("p_module", "Management")
                        cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-Report")
                        cmd.Parameters.AddWithValue("p_remarks", "Cashier Report")
                        cmd.ExecuteNonQuery()

                    End Using
                End Using

            End If

        Catch ex As Exception
            MsgBox("Encounter Error in X-reading")
        End Try
    End Sub
    Private Function FormatMoney(val As Object) As String
        If val Is Nothing OrElse IsDBNull(val) Then Return "0.00"
        Dim d As Decimal = 0D
        Decimal.TryParse(val.ToString(), d)
        Return d.ToString("N2")
    End Function

    Private Sub DrawLine(g As Graphics, label As String, value As String,
                     fLabel As Font, fValue As Font, br As Brush,
                     left As Integer, maxWidth As Integer, y As Integer)

        g.DrawString(label, fLabel, br, left, y)
        g.DrawString(value, fValue, br, maxWidth - 145, y)
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'X-Report
        Try

            Dim no_of_transaction, x_begin_or_no, x_end_or_no As String
            Dim total_sales, discount, vatable_sale, vat_exempt, vat, less_vat, zero_rated_sale As String
            Dim no_of_void, void_amount, cash_float, cash_drop As String

            Dim datetoday As String = String.Format("{0:dd, yyyy}", DateTime.Now)
            Dim timetoday As DateTime = String.Format("{0:HH:mm:ss}", DateTime.Now)

            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Far

            Using conn As New MySqlConnection(strConn)
                conn.Open()

                ' ---------------------------
                ' 1) GET ACCUMULATED TOTALS
                ' ---------------------------

                Dim xSql As String =
                    "SELECT no_of_transaction, begin_or_no, end_or_no, total_sales, discount, vatable, vat_exempt, vat, less_vat, zero_rated, no_of_void, void_amount, cash_float, cash_drop " &
                    "FROM accumulated_amount_tbl " &
                    "WHERE user_id = @uid AND pos_id = @pos AND payment_date = @pdate;"

                Dim xdt As New DataTable()

                Using xCmd As New MySqlCommand(xSql, conn)
                    xCmd.CommandType = CommandType.Text
                    xCmd.CommandTimeout = 0

                    xCmd.Parameters.AddWithValue("@uid", cashier_id)
                    xCmd.Parameters.AddWithValue("@pos", LabelPOSno.Text)
                    ' IMPORTANT: always yyyy-MM-dd for MySQL DATE columns
                    xCmd.Parameters.AddWithValue("@pdate", DateTimePickerXReport.Value.ToString("yyyy-MM-dd"))

                    Using xda As New MySqlDataAdapter(xCmd)
                        xda.Fill(xdt)
                    End Using
                End Using

                If xdt.Rows.Count = 0 Then
                    MessageBox.Show("No accumulated record found for the selected date.", "X-READING")
                    Exit Sub
                End If

                no_of_transaction = xdt.Rows(0)(0).ToString()
                x_begin_or_no = xdt.Rows(0)(1).ToString()
                x_end_or_no = xdt.Rows(0)(2).ToString()
                total_sales = FormatMoney(xdt.Rows(0)(3))
                discount = FormatMoney(xdt.Rows(0)(4))
                vatable_sale = FormatMoney(xdt.Rows(0)(5))
                vat_exempt = FormatMoney(xdt.Rows(0)(6))
                vat = FormatMoney(xdt.Rows(0)(7))
                less_vat = FormatMoney(xdt.Rows(0)(8))
                zero_rated_sale = FormatMoney(xdt.Rows(0)(9))
                no_of_void = FormatMoney(xdt.Rows(0)(10))
                void_amount = FormatMoney(xdt.Rows(0)(11))
                cash_float = FormatMoney(xdt.Rows(0)(12))
                cash_drop = FormatMoney(xdt.Rows(0)(13))

                ' ---------------------------
                ' 2) PAYMENT METHOD BREAKDOWN
                ' ---------------------------
                Dim payTotals As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)

                Dim paySql As String =
                    "SELECT payment_method, SUM(total_amount) AS total_amount " &
                    "FROM or_tbl " &
                    "WHERE cashier_id = @uid " &
                    "AND pos_id = @pos " &
                    "AND payment_date = @pdate " &
                    "AND (void_status IS NULL OR void_status <> 'yes') " &
                    "GROUP BY payment_method " &
                    "ORDER BY payment_method;"

                Dim payDt As New DataTable()

                Using payCmd As New MySqlCommand(paySql, conn)
                    payCmd.CommandType = CommandType.Text
                    payCmd.CommandTimeout = 0
                    payCmd.Parameters.AddWithValue("@uid", cashier_id)
                    payCmd.Parameters.AddWithValue("@pos", LabelPOSno.Text)
                    payCmd.Parameters.AddWithValue("@pdate", DateTimePickerXReport.Value.ToString("yyyy-MM-dd"))

                    Using payDa As New MySqlDataAdapter(payCmd)
                        payDa.Fill(payDt)
                    End Using
                End Using

                For Each r As DataRow In payDt.Rows
                    Dim method As String = If(r("payment_method"), "").ToString().Trim()
                    Dim amt As Decimal = 0D
                    If Not IsDBNull(r("total_amount")) Then amt = Convert.ToDecimal(r("total_amount"))
                    If method <> "" Then payTotals(method) = amt
                Next

                ' ---------------------------
                ' 2.1) DISCOUNT BREAKDOWN
                ' ---------------------------

                Dim discountBreakdown As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)

                Dim discountSql As String =
                        "SELECT oi.type_transaction, SUM(oi.discount) AS discount " &
                        "FROM or_items_tbl oi " &
                        "INNER JOIN or_tbl o ON oi.or_no = o.or_no " &
                        "WHERE DATE(oi.last_updated) = @pdate " &
                        "AND o.cashier_id = @uid " &
                        "AND (oi.void_status IS NULL OR oi.void_status <> 'yes') " &
                        "GROUP BY oi.type_transaction " &
                        "ORDER BY oi.type_transaction;"

                'Dim discountSql As String =
                '"SELECT type_transaction, SUM(discount) AS discount " &
                '"FROM or_items_tbl " &
                '"WHERE DATE(last_updated) = @pdate " &
                '" And (void_status Is NULL Or void_status <> 'yes')" &
                '"GROUP BY type_transaction " &
                '"ORDER BY type_transaction;"

                Dim discountDt As New DataTable()

                Using discountCmd As New MySqlCommand(discountSql, conn)
                    discountCmd.CommandType = CommandType.Text
                    discountCmd.CommandTimeout = 0
                    discountCmd.Parameters.AddWithValue("@uid", cashier_id)
                    discountCmd.Parameters.AddWithValue("@pos", LabelPOSno.Text)
                    discountCmd.Parameters.AddWithValue("@pdate", DateTimePickerXReport.Value.ToString("yyyy-MM-dd"))

                    Using discountDa As New MySqlDataAdapter(discountCmd)
                        discountDa.Fill(discountDt)
                    End Using
                End Using

                For Each dis As DataRow In discountDt.Rows
                    Dim discountmethod As String = If(dis("type_transaction"), "").ToString().Trim()
                    Dim discountAmt As Decimal = 0D
                    If Not IsDBNull(dis("discount")) Then discountAmt = Convert.ToDecimal(dis("discount"))
                    If discountmethod <> "" Then discountBreakdown(discountmethod) = discountAmt
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
                CenterText(e, CompNameB, N, yPos) : yPos += 15
                        CenterText(e, AddressA, N, yPos) : yPos += 15
                        CenterText(e, AddressB, N, yPos) : yPos += 15
                CenterText(e, AddressC, N, yPos) : yPos += 15
                CenterText(e, AddressD, N, yPos) : yPos += 15
                CenterText(e, VATTIN, N, yPos) : yPos += 15
                CenterText(e, "POS" & pos & " S/N: " & serial, N, yPos) : yPos += 15
                        CenterText(e, MIN_No, N, yPos) : yPos += 15
                        CenterText(e, "=================================", H, yPos) : yPos += 20
                CenterText(e, "X-READING", B, yPos) : yPos += 20
                        CenterText(e, "Date of: " & DateTimePickerXReport.Text, H, yPos) : yPos += 20
                        CenterText(e, "=================================", H, yPos) : yPos += 20
                CenterText(e, "Cashier: " & ComboBoxXCashier.Text, N, yPos) : yPos += 20
                CenterText(e, "POS " & LabelPOSno.Text, N, yPos) : yPos += 15
                        CenterText(e, "=================================", H, yPos) : yPos += 20

                        ' ---------------------------
                        ' 5) PRINT TOTALS
                        ' ---------------------------
                        DrawLine(graphics, "Total No. of Transaction:         ", no_of_transaction, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        DrawLine(graphics, "Beginning SI No.:", x_begin_or_no, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        DrawLine(graphics, "Ending SI No.:", x_end_or_no, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        CenterText(e, "=================================", H, yPos) : yPos += 20
                        yPos += 10

                Dim netSales As Decimal = Convert.ToDecimal(total_sales)
                Dim totalDiscount As Decimal = Convert.ToDecimal(discount)
                Dim cvoid_amount As Decimal = Convert.ToDecimal(void_amount)

                Dim grossSales As Decimal = netSales + totalDiscount
                Dim totalGrossLessVoid As Decimal = grossSales + Math.Abs(cvoid_amount)

                DrawLine(graphics, "Total Count of Void SI: ", no_of_void, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        yPos += 15
                        DrawLine(graphics, "Gross Sales: ", grossSales, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                DrawLine(graphics, "LESS: Void Sales ", void_amount, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                DrawLine(graphics, "TOTAL: ", totalGrossLessVoid, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                yPos += 10
                DrawLine(graphics, "LESS: Discounts ", discount, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        DrawLine(graphics, "Net Sales:", total_sales, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20

                        CenterText(e, "=================================", H, yPos) : yPos += 20
                        ' ---------------------------
                        ' 6) NET COLLECTIONS
                        ' ---------------------------
                        yPos += 10
                        graphics.DrawString("PAYMENTS:", font, brush, leftMargin, yPos)
                        yPos += 20

                'Dim orderedMethods As String() = {"CASH", "GCASH", "QR PH", "DEBIT CARD", "CREDIT CARD"}
                ' Assuming comboBoxPaymentMethod is your ComboBox
                ' Dim orderedMethods As New List(Of String)()

                For Each kvp In payTotals
                    Dim method As String = kvp.Key
                    Dim amt As Decimal = kvp.Value

                    graphics.DrawString(method & " :", font, brush, leftMargin + 40, yPos)
                    graphics.DrawString(amt.ToString("N2"), N, brush, maxWidth - 145, yPos)

                    yPos += 20
                Next

                Dim totalPaymentMethod As Decimal = payTotals.Values.Sum()

                DrawLine(graphics, "TOTAL:", totalPaymentMethod, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20

                        CenterText(e, "=================================", H, yPos) : yPos += 20
                        yPos += 10

                        Dim Tvatable_sale As Decimal = Convert.ToDecimal(vatable_sale)
                        Dim Tvat As Decimal = Convert.ToDecimal(vat)
                Dim Tvat_exempt As Decimal = Convert.ToDecimal(vat_exempt)
                Dim Tzero_rated_sale As Decimal = Convert.ToDecimal(zero_rated_sale)
                Dim Tlessvat As Decimal = Convert.ToDecimal(less_vat)

                Dim totalVatComputation As Decimal = Tvatable_sale + Tvat + Tvat_exempt + Tzero_rated_sale + Tlessvat

                DrawLine(graphics, "Less VAT(12%)", less_vat, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                yPos += 10
                DrawLine(graphics, "VATable:", vatable_sale, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                DrawLine(graphics, "VAT Amount:", vat, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                DrawLine(graphics, "VAT-Exempt:", vat_exempt, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                DrawLine(graphics, "Zero Rated Sale:", zero_rated_sale, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        DrawLine(graphics, "TOTAL:", totalVatComputation, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        CenterText(e, "=================================", H, yPos) : yPos += 20
                yPos += 10
                graphics.DrawString("DISCOUNTS SUMMARY:", font, brush, leftMargin, yPos)
                yPos += 20

                ' ---------------------------
                ' 6) DISCOUNT COLLECTIONS
                ' ---------------------------

                Dim orderedDiscounts As String() = {"Student", "Senior(20%)", "Solo Parent(10%)", "PWD(20%)", "NAAC(10%)", "BDAY Celeb(50%)", "S.Disc: 10%", "S.Disc: 25%", "S.Disc: 30%"}
                For Each od As String In orderedDiscounts
                    Dim disAmt As Decimal = 0D
                    If discountBreakdown.ContainsKey(od) Then disAmt = discountBreakdown(od)

                    graphics.DrawString(od & ":", font, brush, leftMargin + 40, yPos)
                    graphics.DrawString(disAmt.ToString("N2"), N, brush, maxWidth - 135, yPos)
                    yPos += 20
                Next
                Dim totalDiscounted As Decimal = discountBreakdown.Values.Sum()

                DrawLine(graphics, "TOTAL:", totalDiscounted, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20

                CenterText(e, "=================================", H, yPos) : yPos += 20
                        yPos += 10
                        DrawLine(graphics, "Cash In: ", cash_float, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        DrawLine(graphics, "Cash Out: ", cash_drop, font, N, brush, leftMargin, maxWidth, yPos) : yPos += 20
                        CenterText(e, "=================================", H, yPos) : yPos += 40

                        ' ---------------------------
                        ' 7) FOOTER
                        ' ---------------------------
                        CenterText(e, "Printed By: " & LabelUserName.Text, N, yPos) : yPos += 20
                        CenterText(e, "Printed on " & MonthName(Now.Month) & " " & datetoday, N, yPos) : yPos += 20
                        CenterText(e, "at " & timetoday, N, yPos) : yPos += 20

                'End Using
                '--------------------------FOR updating and inserting ejournal----------------------------
                Try
                    Dim filepath As String = "C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"
                    'Dim filepath As String = "C:\E-Journal\X_Reading\" & ComboBoxXCashier.Text & "_" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"
                    '' Dim filepath As String = "C:\E-Journal\" & "2019-11-14" & ".txt"
                    'MsgBox("Checking for Saving EJournal X Reading")
                    Dim sw As StreamWriter

                    If (Not File.Exists(filepath)) Then

                        sw = File.CreateText(filepath)
                        sw.WriteLine("   " & CompName)
                        sw.WriteLine("       " & CompNameB)
                        sw.WriteLine("         " & AddressA)
                        sw.WriteLine("         " & AddressB)
                        sw.WriteLine("         " & AddressC)
                        sw.WriteLine("         " & AddressD)
                        sw.WriteLine("         " & VATTIN)
                        sw.WriteLine("    POS" & LabelPOSno.Text & " S/N: " & serial)
                        sw.WriteLine("     " & MIN_No)
                        sw.WriteLine("=================================")
                        sw.WriteLine("              X-READING")
                        sw.WriteLine("           Date of: " & DateTimePickerXReport.Text)
                        sw.WriteLine("=================================")
                        sw.WriteLine("       Cashier:  " & ComboBoxXCashier.Text)
                        sw.WriteLine("            POS ID: " & LabelPOSno.Text)
                        sw.WriteLine("")
                        sw.WriteLine("Total No. of Transaction: " & no_of_transaction)
                        sw.WriteLine("")
                        sw.WriteLine("Beginning SI No.:       " & x_begin_or_no)
                        sw.WriteLine("Ending SI No.   :       " & x_end_or_no)
                        sw.WriteLine("=================================")
                        sw.WriteLine("")
                        sw.WriteLine("Total Count of Void SI:  " & no_of_void)
                        sw.WriteLine("")

                        sw.WriteLine("Gross Sales       :  " & grossSales)
                        sw.WriteLine("LESS: Void Sale   :  " & void_amount)
                        sw.WriteLine("TOTAL             :  " & totalGrossLessVoid)
                        sw.WriteLine("")
                        sw.WriteLine("LESS: Discount       " & discount)
                        sw.WriteLine("Net Sales         :  " & total_sales)
                        sw.WriteLine("=================================")
                        sw.WriteLine("")
                        sw.WriteLine("PAYMENTS :         ")

                        For Each kvp In payTotals
                            Dim method As String = kvp.Key
                            Dim amt As Decimal = kvp.Value

                            sw.WriteLine("      " & method & "       : " & amt)
                            yPos += 20
                        Next

                        sw.WriteLine("TOTAL             :  " & totalPaymentMethod)

                        sw.WriteLine("")
                        sw.WriteLine("Less VAT(12%)  : " & less_vat)
                        sw.WriteLine("")
                        sw.WriteLine("Vatable        : " & vatable_sale)
                        sw.WriteLine("VAT(12%)       : " & vat)
                        sw.WriteLine("VAT-Exempt     : " & vat_exempt)
                        sw.WriteLine("Zero Rated     : " & zero_rated_sale)
                        sw.WriteLine("TOTAL          : " & totalVatComputation)
                        sw.WriteLine("")
                        sw.WriteLine("=================================")
                        sw.WriteLine("")
                        sw.WriteLine("DISCOUNT SUMMARY:")
                        sw.WriteLine("")

                        For Each od As String In orderedDiscounts
                            Dim disAmt As Decimal = 0D
                            If discountBreakdown.ContainsKey(od) Then disAmt = discountBreakdown(od)
                            sw.WriteLine("      " & od & "        : " & disAmt)
                        Next
                        sw.WriteLine("TOTAL             :  " & totalDiscounted)

                        sw.WriteLine("=================================")
                        sw.WriteLine("Cash In :     " & cash_float)
                        sw.WriteLine("Cash Out :    " & cash_drop)
                        sw.WriteLine("")
                        sw.WriteLine("=================================")
                        sw.WriteLine("        Printed By: " & LabelUserName.Text)
                        sw.WriteLine("")
                        sw.WriteLine("    Printed on : " & MonthName(Now.Month) & " " & datetoday)
                        sw.WriteLine("          at: " & timetoday)
                        sw.WriteLine("")
                        sw.WriteLine("")
                        sw.WriteLine("")

                    Else
                        sw = File.CreateText(filepath)
                        sw.WriteLine("   " & CompName)
                        sw.WriteLine("       " & CompNameB)
                        sw.WriteLine("         " & AddressA)
                        sw.WriteLine("         " & AddressB)
                        sw.WriteLine("         " & AddressC)
                        sw.WriteLine("         " & AddressD)
                        sw.WriteLine("         " & VATTIN)
                        sw.WriteLine("    POS" & LabelPOSno.Text & " S/N: " & serial)
                        sw.WriteLine("     " & MIN_No)
                        sw.WriteLine("=================================")
                        sw.WriteLine("              X-READING")
                        sw.WriteLine("           Date of: " & DateTimePickerXReport.Text)
                        sw.WriteLine("=================================")
                        sw.WriteLine("       Cashier:  " & ComboBoxXCashier.Text)
                        sw.WriteLine("            POS ID: " & LabelPOSno.Text)
                        sw.WriteLine("")
                        sw.WriteLine("Total No. of Transaction: " & no_of_transaction)
                        sw.WriteLine("")
                        sw.WriteLine("Beginning SI No.:       " & x_begin_or_no)
                        sw.WriteLine("Ending SI No.   :       " & x_end_or_no)
                        sw.WriteLine("=================================")
                        sw.WriteLine("")
                        sw.WriteLine("Total Count of Void SI:  " & no_of_void)
                        sw.WriteLine("")

                        sw.WriteLine("Gross Sales       :  " & grossSales)
                        sw.WriteLine("LESS: Void Sale   :  " & void_amount)
                        sw.WriteLine("TOTAL             :  " & totalGrossLessVoid)
                        sw.WriteLine("")
                        sw.WriteLine("LESS: Discount       " & discount)
                        sw.WriteLine("Net Sales         :  " & total_sales)
                        sw.WriteLine("=================================")
                        sw.WriteLine("")
                        sw.WriteLine("PAYMENTS :         ")

                        For Each kvp In payTotals
                            Dim method As String = kvp.Key
                            Dim amt As Decimal = kvp.Value

                            sw.WriteLine("      " & method & "       : " & amt)
                            yPos += 20
                        Next

                        sw.WriteLine("TOTAL             :  " & totalPaymentMethod)

                        sw.WriteLine("")
                        sw.WriteLine("Less VAT(12%)  :  " & less_vat)
                        sw.WriteLine("")
                        sw.WriteLine("VATable        :  " & vatable_sale)
                        sw.WriteLine("VAT(12%)       :  " & vat)
                        sw.WriteLine("VAT-Exempt     :  " & vat_exempt)
                        sw.WriteLine("Zero Rated     :  " & zero_rated_sale)
                        sw.WriteLine("TOTAL          :  " & totalVatComputation)
                        sw.WriteLine("")
                        sw.WriteLine("=================================")
                        sw.WriteLine("")
                        sw.WriteLine("DISCOUNT SUMMARY:")
                        sw.WriteLine("")

                        For Each od As String In orderedDiscounts
                            Dim disAmt As Decimal = 0D
                            If discountBreakdown.ContainsKey(od) Then disAmt = discountBreakdown(od)
                            sw.WriteLine("      " & od & "        : " & disAmt)
                        Next
                        sw.WriteLine("TOTAL             :  " & totalDiscounted)

                        sw.WriteLine("=================================")
                        sw.WriteLine("Cash In :     " & cash_float)
                        sw.WriteLine("Cash Out :    " & cash_drop)
                        sw.WriteLine("")
                        sw.WriteLine("=================================")
                        sw.WriteLine("        Printed By: " & LabelUserName.Text)
                        sw.WriteLine("")
                        sw.WriteLine("    Printed on : " & MonthName(Now.Month) & " " & datetoday)
                        sw.WriteLine("          at: " & timetoday)
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
                    End Using
                    sf.Dispose()
            ComboBoxXCashier.Text = "-- Please Choose Cashier --"
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Sub
    '---------- Z Report ----------'

    Private Sub ButtonPrintZReport_Click(sender As Object, e As EventArgs) Handles ButtonPrintZReport.Click
        conn.Open()
        Dim z_counter As String
        Dim show As New MySqlCommand("SELECT z_counter FROM accumulated_amount_tbl WHERE payment_date = '" & DateTimePickerZStart.Text & "' AND pos_id = '" & LabelPOSno.Text & "'", conn)
        show.CommandType = CommandType.Text
        Dim da As New MySqlDataAdapter(show)
        Dim dtt As New DataTable
        show.CommandTimeout = 0

        Try
            da.Fill(dtt)

            z_counter = dtt.Rows(0)(0).ToString

            Try
                Dim cdd As New MySqlCommand("Update accumulated_amount_tbl set z_counter = @z_counter, printed = @printed, updated = @updated WHERE payment_date = '" & DateTimePickerZStart.Text & "' AND pos_id = '" & LabelPOSno.Text & "'", conn)
                cdd.CommandType = CommandType.Text
                cdd.Parameters.AddWithValue("@z_counter", z_counter + 1)
                cdd.Parameters.AddWithValue("@printed", "yes")
                cdd.Parameters.AddWithValue("@updated", "yes")

                Dim readerr As MySqlDataReader

                readerr = cdd.ExecuteReader
                readerr.Close()
                conn.Close()

                'To print
                Dim margins As New Margins(0, 0, 0, 0)
                PrintZReading.DefaultPageSettings.Margins = margins

                ''THERMAL
                Dim papersize As New PaperSize("Custom", 280, 6100)

                ''for MMDA
                'Dim papersize As New PaperSize("Custom", 280, 500)

                PrintZReading.DefaultPageSettings.PaperSize = papersize

                'Dim PrintPreviewDialog2 As PrintPreviewDialog = New PrintPreviewDialog
                'PrintPreviewDialog2.Document = PrintZReading

                'AddHandler PrintZReading.PrintPage, AddressOf Me.PrintDocument2_PrintPage
                'PrintPreviewDialog2.ShowDialog()

                'TO PRINT IMMEDIATELY
                PrintDocument2.Print()
            Catch ex As Exception
                MsgBox("NO TRANSACTION ON THIS DAY" & ex.Message, MessageBoxIcon.Warning)
                conn.Close()
            End Try
        Catch ex As Exception
            MsgBox("No transaction on this day!", MessageBoxIcon.Warning)
            conn.Close()
        End Try

        '---------------INSERTING TO TEXTFILE TABLE-----------------
        Dim ccmd As New MySqlCommand
        Dim ddt As New DataTable()
        Dim adapter As New MySqlDataAdapter
        Try
            ccmd.CommandText = "SELECT * FROM textfile_tbl WHERE payment_date = '" & DateTime.Today.ToString("yyyy-MM-dd") & "' AND pos_id = '" & LabelPOSno.Text & "'"
            adapter.SelectCommand = ccmd
            adapter.SelectCommand.Connection = conn
            adapter.Fill(ddt)

            If ddt.Rows.Count > 0 Then
                'for deleting old textfile for appending
                My.Computer.FileSystem.DeleteFile("C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt")
            Else
                Try

                    Dim rawData() As Byte = IO.File.ReadAllBytes("C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt")
                    'Dim rawData() As Byte = IO.File.ReadAllBytes("C:\E-Journal\" & "2019-11-14" & ".txt")
                    conn.Open()
                    Using cmd As New MySqlCommand("INSERT INTO textfile_tbl (payment_date,text_file, pos_id) values (@payment_date, aes_encrypt(@text_file, 'strdjnltmyp'), @pos_id)", conn)
                        cmd.CommandType = CommandType.Text
                        cmd.Parameters.AddWithValue("@payment_date", DateTime.Today.ToString("yyyy-MM-dd"))
                        cmd.Parameters.AddWithValue("@text_file", rawData)
                        cmd.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                        Try
                            cmd.ExecuteNonQuery()
                            conn.Close()

                            'for deleting old textfile for appending
                            My.Computer.FileSystem.DeleteFile("C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt")
                        Catch ex As Exception
                            MsgBox(ex.Message)
                            conn.Close()
                        End Try
                    End Using
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage

        'Z-Report

        Try
            ' ---------------------------
            ' 0) LOAD Z DATA (make sure conn is open before calling)
            ' ---------------------------
            Dim datetoday As String = String.Format("{0:dd, yyyy}", DateTime.Now)


            ' IMPORTANT: make sure you already have a connection object "conn" OPEN
            ' If you don't, use Using conn As New MySqlConnection(strConn) here and open it.
            zr = ZReading.Load(LabelPOSno.Text, DateTimePickerZStart.Value.ToString("yyyy-MM-dd"), conn)

            If zr Is Nothing Then
                MessageBox.Show("No Z-Reading data on this date.")
                e.HasMorePages = False
                Exit Sub
            End If

            Dim grossSales As Decimal = CDec(zr.total_sales) + CDec(zr.discount)
            Dim combiGrossVoid As Decimal = grossSales + Math.Abs(CDec(zr.void_amount))
            'Dim vatSummary As Decimal = CDec(zr.vatable_sale) + CDec(zr.vat) + CDec(zr.vat_exempt) + CDec(zr.zero_rated_sale) + CDec(zr.less_vat)
            Dim vatSummary As Decimal = Val(zr.vatable_sale) + Val(zr.vat) + Val(zr.vat_exempt) + Val(zr.zero_rated_sale) + Val(zr.less_vat)
            Dim newGrandTotal As Decimal = CDec(prevGrand) + CDec(zr.total_sales)

            ' ---------------------------
            ' PRINT SETTINGS
            ' ---------------------------
            Dim leftMargin As Integer = 5
            Dim rightMargin As Integer = 5
            Dim thermalPaperWidth As Integer = 300
            Dim maxWidth As Integer = thermalPaperWidth - leftMargin - rightMargin

            Dim graphics As Graphics = e.Graphics
            Dim brush As New SolidBrush(Color.Black)

            Dim centerFormat As New StringFormat() With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }

            Dim fontN As New Font("Tahoma", 9)
            Dim fontNB As New Font("Tahoma", 9, FontStyle.Bold)
            Dim fontH As New Font("Tahoma", 11)
            Dim fontB As New Font("Tahoma", 13, FontStyle.Bold)

            Dim yPos As Integer = 5

            ' ---------------------------
            ' 2) PAYMENT METHOD BREAKDOWN
            ' ---------------------------
            Dim payTotals As New Dictionary(Of String, Decimal)

            Dim paySql As String =
                    "SELECT payment_method, SUM(total_amount) AS total_amount " &
                    "FROM or_tbl " &
                    "WHERE payment_date = @pdate " &
                    "AND (void_status IS NULL OR void_status <> 'yes') " &
                    "GROUP BY payment_method " &
                    "ORDER BY payment_method;"

            Dim payDt As New DataTable()

            Using payCmd As New MySqlCommand(paySql, conn)
                payCmd.CommandType = CommandType.Text
                payCmd.CommandTimeout = 0
                payCmd.Parameters.AddWithValue("@pdate", DateTimePickerZStart.Value.ToString("yyyy-MM-dd"))

                Using payDa As New MySqlDataAdapter(payCmd)
                    payDa.Fill(payDt)

                End Using

                For Each r As DataRow In payDt.Rows

                    Dim method As String = r("payment_method").ToString().Trim()
                    Dim amt As Decimal = 0D

                    If Not IsDBNull(r("total_amount")) Then
                        amt = Convert.ToDecimal(r("total_amount"))
                    End If

                    If method <> "" Then
                        If payTotals.ContainsKey(method) Then
                            payTotals(method) += amt
                        Else
                            payTotals(method) = amt
                        End If
                    End If

                Next
            End Using

            ' ---------------------------
            ' 2.1) DISCOUNT BREAKDOWN
            ' ---------------------------

            Dim discountBreakdown As New Dictionary(Of String, Decimal)(StringComparer.OrdinalIgnoreCase)

            'Dim discountSql As String =
            '            "SELECT oi.type_transaction, SUM(oi.discount) AS discount " &
            '            "FROM or_items_tbl oi " &
            '            "INNER JOIN or_tbl o ON oi.or_no = o.or_no " &
            '            "WHERE DATE(oi.last_updated) = @pdate " &
            '            "AND o.cashier_id = @uid " &
            '            "AND (oi.void_status IS NULL OR oi.void_status <> 'yes') " &
            '            "GROUP BY oi.type_transaction " &
            '            "ORDER BY oi.type_transaction;"

            Dim discountSql As String =
            "SELECT type_transaction, SUM(discount) AS discount " &
            "FROM or_items_tbl " &
            "WHERE DATE(last_updated) = @pdate" &
            " And (void_status Is NULL Or void_status <> 'yes')" &
            "GROUP BY type_transaction " &
            "ORDER BY type_transaction;"

            Dim discountDt As New DataTable()

            Using discountCmd As New MySqlCommand(discountSql, conn)
                discountCmd.CommandType = CommandType.Text
                discountCmd.CommandTimeout = 0
                'discountCmd.Parameters.AddWithValue("@uid", cashier_id)
                discountCmd.Parameters.AddWithValue("@pos", LabelPOSno.Text)
                discountCmd.Parameters.AddWithValue("@pdate", DateTimePickerXReport.Value.ToString("yyyy-MM-dd"))

                Using discountDa As New MySqlDataAdapter(discountCmd)
                    discountDa.Fill(discountDt)
                End Using
            End Using

            For Each dis As DataRow In discountDt.Rows
                Dim discountmethod As String = If(dis("type_transaction"), "").ToString().Trim()
                Dim discountAmt As Decimal = 0D
                If Not IsDBNull(dis("discount")) Then discountAmt = Convert.ToDecimal(dis("discount"))
                If discountmethod <> "" Then discountBreakdown(discountmethod) = discountAmt
            Next

            ' ---------------------------
            ' 2) PRINT HEADER
            ' ---------------------------
            graphics.DrawString(CompName, fontB, brush, New RectangleF(leftMargin, yPos, maxWidth, 100), centerFormat)
            yPos += 80
            CenterText(e, CompNameB, fontN, yPos) : yPos += 15
            CenterText(e, AddressA, fontN, yPos) : yPos += 15
            CenterText(e, AddressB, fontN, yPos) : yPos += 15
            CenterText(e, AddressC, fontN, yPos) : yPos += 15
            CenterText(e, AddressD, fontN, yPos) : yPos += 15
            CenterText(e, VATTIN, fontN, yPos) : yPos += 15
            CenterText(e, "POS" & pos & " S/N: " & serial, fontN, yPos) : yPos += 15
            CenterText(e, MIN_No, fontN, yPos) : yPos += 15
            CenterText(e, "--------------------------------------------", fontB, yPos) : yPos += 20
            CenterText(e, "Z-READING", fontB, yPos) : yPos += 20
            CenterText(e, "Date of: " & DateTimePickerZStart.Value.ToString("yyyy-MM-dd"), fontH, yPos) : yPos += 20
            CenterText(e, "POS " & POS_ID, fontNB, yPos) : yPos += 20
            CenterText(e, "--------------------------------------------", fontB, yPos) : yPos += 20

            ' ---------------------------
            ' 3) SI SUMMARY
            ' ---------------------------

            graphics.DrawString("SI SUMMARY:", fontH, Brushes.Black, 10, yPos) : yPos += 20
            DrawKV(graphics, "Beginning SI:", zr.begin_or_no, fontN, brush, 12, 150, yPos) : yPos += 20
            DrawKV(graphics, "Ending SI:", zr.end_or_no, fontN, brush, 12, 150, yPos) : yPos += 20
            DrawKV(graphics, "No. Of Transaction:", zr.no_of_transaction, fontN, brush, 15, 150, yPos) : yPos += 20
            DrawKV(graphics, "No. Of Void:", zr.no_of_void, fontN, brush, 15, 150, yPos) : yPos += 20
            DrawKV(graphics, "Beginning Void No.:", zr.begin_void_no, fontN, brush, 15, 150, yPos) : yPos += 20
            DrawKV(graphics, "Ending Void No.:", zr.end_void_no, fontN, brush, 15, 150, yPos) : yPos += 20

            CenterText(e, "--------------------------------------------", fontB, yPos) : yPos += 20

            ' ---------------------------
            ' 4) SALES SUMMARY
            ' ---------------------------
            graphics.DrawString("SALES SUMMARY:", fontH, Brushes.Black, 10, yPos) : yPos += 20
            DrawKV(graphics, "Gross Sales:", grossSales.ToString("N2"), fontN, brush, 15, 170, yPos) : yPos += 20
            DrawKV(graphics, "LESS: Void Sales", SafeMoney(zr.void_amount), fontN, brush, 15, 170, yPos) : yPos += 20
            DrawKV(graphics, "TOTAL:", combiGrossVoid.ToString("N2"), fontN, brush, 15, 170, yPos) : yPos += 20
            yPos += 10
            DrawKV(graphics, "LESS: Discount", SafeMoney(zr.discount), fontN, brush, 15, 170, yPos) : yPos += 20
            DrawKV(graphics, "Net Sales:", SafeMoney(zr.total_sales), fontN, brush, 15, 170, yPos) : yPos += 20
            CenterText(e, "--------------------------------------------", fontB, yPos) : yPos += 20

            ' ---------------------------
            ' 5) VAT COMPUTATION
            ' ---------------------------
            graphics.DrawString("VAT COMPUTATION:", fontH, Brushes.Black, 10, yPos) : yPos += 20
            DrawKV(graphics, "Less Vat(12%):", SafeMoney(zr.less_vat), fontN, brush, 15, 170, yPos) : yPos += 20
            yPos += 10
            DrawKV(graphics, "Vatable Sales:", SafeMoney(zr.vatable_sale), fontN, brush, 15, 170, yPos) : yPos += 20
            DrawKV(graphics, "VAT(12%):", SafeMoney(zr.vat), fontN, brush, 15, 170, yPos) : yPos += 20
            DrawKV(graphics, "VAT-Exempt Sales:", SafeMoney(zr.vat_exempt), fontN, brush, 15, 170, yPos) : yPos += 20
            DrawKV(graphics, "Zero-Rated Sales:", SafeMoney(zr.zero_rated_sale), fontN, brush, 15, 170, yPos) : yPos += 20
            DrawKV(graphics, "TOTAL:", vatSummary.ToString("N2"), fontN, brush, 15, 170, yPos) : yPos += 20

            CenterText(e, "--------------------------------------------", fontB, yPos) : yPos += 20

            ' ---------------------------
            ' 6) NET COLLECTIONS
            ' ---------------------------
            yPos += 10
            graphics.DrawString("PAYMENTS:", fontH, Brushes.Black, 10, yPos) : yPos += 20

            For Each kvp In payTotals
                Dim method As String = kvp.Key
                Dim amt As Decimal = kvp.Value

                graphics.DrawString(method & " :", fontN, brush, leftMargin + 40, yPos)
                graphics.DrawString(amt.ToString("N2"), fontN, brush, maxWidth - 145, yPos)

                yPos += 20
            Next

            Dim totalPaymentMethod As Decimal = payTotals.Values.Sum()

            DrawKV(graphics, "TOTAL:", totalPaymentMethod.ToString("N2"), fontN, brush, 15, 170, yPos) : yPos += 20
            CenterText(e, "--------------------------------------------", fontB, yPos) : yPos += 20

            ' ---------------------------
            ' 7) Breakdown Discounts
            ' ---------------------------
            graphics.DrawString("DISCOUNT SUMMARY:", fontH, Brushes.Black, 10, yPos) : yPos += 20
            Dim orderedDiscounts As String() = {"Student", "Senior(20%)", "Solo Parent(10%)", "PWD(20%)", "NAAC(10%)", "BDAY Celeb(50%)", "S.Disc: 10%", "S.Disc: 25%", "S.Disc: 30%"}

            For Each od As String In orderedDiscounts
                Dim disAmt As Decimal = 0D
                If discountBreakdown.ContainsKey(od) Then disAmt = discountBreakdown(od)

                graphics.DrawString(od & ":", Font, brush, leftMargin + 40, yPos)
                graphics.DrawString(disAmt.ToString("N2"), fontN, brush, maxWidth - 145, yPos)

                yPos += 20
            Next

            Dim totalDiscounted As Decimal = discountBreakdown.Values.Sum()
            DrawKV(graphics, "TOTAL:", totalDiscounted.ToString("N2"), fontN, brush, 15, 170, yPos) : yPos += 20

            ' ---------------------------
            ' 7) ACCUMULATED GRAND TOTAL
            ' ---------------------------
            'graphics.DrawString("ACCUMULATED SALES:", fontH, Brushes.Black, 10, yPos) : yPos += 20
            'DrawKV(graphics, "Previous End Of Day:", SafeMoney(prevGrand), fontN, brush, 15, 170, yPos) : yPos += 20
            'DrawKV(graphics, "Today End Of Day:", SafeMoney(zr.total_sales), fontN, brush, 15, 170, yPos) : yPos += 20

            CenterText(e, "--------------------------------------------", fontB, yPos) : yPos += 20

            ' ---------------------------
            ' 8) FOOTER
            ' ---------------------------
            graphics.DrawString("Z-COUNTER:", fontH, Brushes.Black, 10, yPos)
            graphics.DrawString(zr.z_counter, fontN, Brushes.Black, 170, yPos) : yPos += 20

            CenterText(e, "--------------------------------------------", fontB, yPos) : yPos += 20
            CenterText(e, "Printed By: " & LabelUserName.Text, fontH, yPos) : yPos += 20
            CenterText(e, "Printed on: " & MonthName(Now.Month) & " " & datetoday, fontH, yPos) : yPos += 20
            CenterText(e, "  --- END OF DAY ---  ", fontB, yPos) : yPos += 20

            e.HasMorePages = False

        Catch ex As Exception
            MessageBox.Show("Something Issues in Generating Report!")
            'MessageBox.Show(ex.Message, "Z-READING ERROR")
        End Try

    End Sub

    ' =========================
    ' HELPERS
    ' =========================
    Private Sub DrawKV(g As Graphics, label As String, value As String, f As Font, br As Brush, x1 As Integer, x2 As Integer, y As Integer)
        g.DrawString(label, f, br, x1, y)
        g.DrawString(value, f, br, x2, y)
    End Sub

    Private Function SafeMoney(val As Object) As String
        If val Is Nothing OrElse IsDBNull(val) Then Return "0.00"
        Dim d As Decimal = 0D
        Decimal.TryParse(val.ToString(), d)
        Return d.ToString("N2")
    End Function

    Private Sub ButtonLogout_Click(sender As Object, e As EventArgs) Handles ButtonLogout.Click
        'for saving cashier logout
        conn.Open()

        Dim cdd As New MySqlCommand("Update userlog_tbl set date_logout = @date_logout, time_logout = @time_logout, updated = @updated WHERE userlog_id = @userlog_id and user_id = @user_id AND pos_id = @pos_id", conn)
        cdd.CommandType = CommandType.Text
        cdd.Parameters.AddWithValue("@date_logout", Today.ToString("yyyy-MM-dd"))
        cdd.Parameters.AddWithValue("@time_logout", Now.ToString("HH:mm:ss"))
        cdd.Parameters.AddWithValue("@updated", "yes")
        cdd.Parameters.AddWithValue("@userlog_id", LabelLoginID.Text)
        cdd.Parameters.AddWithValue("@user_id", LabelUserID.Text)
        cdd.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
        Dim reader As MySqlDataReader

        reader = cdd.ExecuteReader

        conn.Close()

        Try
            Me.Close()
            login.TextBoxUserID.Text = ""
            login.LabelName.Text = ""
            login.LabelPosition.Text = ""
            login.PictureBoxFace.Image = Nothing
            login.LabelHello.Hide()
            login.Show()

            'LOCK FOLDER
            Dim folderPath As String = "C:\E-Journal"
            LockFolder(folderPath)

        Catch ex As Exception
            MsgBox("Secured E-Journal Files")
        End Try
    End Sub

    Private Sub ButtonReports_Click(sender As Object, e As EventArgs) Handles ButtonReports.Click
        reports.LabelUserName.Text = LabelUserName.Text
        reports.LabelUserID.Text = LabelUserID.Text

        reports.Show()
    End Sub

    Private Sub ButtonDownload_Click(sender As Object, e As EventArgs)
        Try
            Dim fs As FileStream
            Dim bw As BinaryWriter
            Dim retval As Long
            Dim startIndex As Long = 0
            Dim bs As Integer = 100
            Dim outbyte(bs - 1) As Byte

            conn.Open()
            Dim text_file, payment_date As String
            Dim cmd As New MySqlCommand("SELECT text_file, or_no, DATE_FORMAT(payment_date, '%M %d, %Y') FROM ejournal_tbl WHERE payment_date = '" & DateTimePickerEJ1.Text & "' AND pos_id = '" & LabelPOSno.Text & "'", conn)
            cmd.CommandType = CommandType.Text
            Dim da As New MySqlDataAdapter(cmd)
            Dim dtt As New DataTable
            da.Fill(dtt)
            Dim dr As MySqlDataReader = cmd.ExecuteReader

            Do While dr.Read
                payment_date = dtt.Rows(0)(2).ToString
                'Added
                My.Computer.FileSystem.CreateDirectory("C:\E-Journal\" & payment_date)

                text_file = Val(dr.Item("or_no").ToString)
                Dim strtmp As String = dr.GetString(0)
                Dim fileloc As String = "C:\E-Journal\" & payment_date & "\" & text_file & ".txt"
                fs = New FileStream(fileloc, FileMode.OpenOrCreate, FileAccess.Write)
                bw = New BinaryWriter(fs)
                startIndex = 0
                retval = dr.GetBytes(0, startIndex, outbyte, 0, bs)

                Do While retval = bs
                    bw.Write(outbyte)
                    bw.Flush()
                    startIndex += bs
                    retval = dr.GetBytes(0, startIndex, outbyte, 0, bs)
                Loop

                Try
                    bw.Write(outbyte, 0, retval - 1)

                Catch ex As Exception
                    MessageBox.Show("There was an error: " & ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                bw.Flush()
                bw.Close()
                fs.Close()
            Loop
            dr.Close()
            conn.Close()

            MsgBox("File Downloaded!")
        Catch ex As Exception
            MessageBox.Show("There was an error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            conn.Close()
        End Try

        'Try
        '    conn.Open()
        '    Dim min_or, max_or As String
        '    Dim data As New MySqlCommand("SELECT MIN(or_no), MAX(or_no) FROM ejournal_tbl WHERE payment_date = '" & DateTimePickerEJ1.Text & "'", conn)
        '    data.CommandType = CommandType.Text
        '    Dim daa As New MySqlDataAdapter(data)
        '    Dim dt As New DataTable
        '    daa.Fill(dt)

        '    min_or = dt.Rows(0)(0).ToString
        '    max_or = dt.Rows(0)(1).ToString

        '    If dt.Rows.Count > 0 Then
        '        'MsgBox("File Downloaded!")
        '        My.Computer.FileSystem.CreateDirectory("C:\E-Journal\" & DateTimePickerEJ1.Text)
        '        'Dim file = "C:/E-Journal/" & DateTimePickerEJ1.Text & "/" & min_or & " - " & max_or
        '        'Dim cmd As New MySqlCommand("Select text_file INTO OUTFILE '" & file & ".txt' FIELDS TERMINATED BY '***' OPTIONALLY ENCLOSED BY '\n' LINES TERMINATED BY '' FROM ejournal_tbl WHERE payment_date = '" & DateTimePickerEJ1.Text & "'", conn)
        '        Dim cmd As New MySqlCommand("Select text_file INTO OUTFILE 'C:/E-Journal/" & DateTimePickerEJ1.Text & "/" & min_or & " - " & max_or & ".txt' FIELDS TERMINATED BY '***' OPTIONALLY ENCLOSED BY '\n' LINES TERMINATED BY '' FROM ejournal_tbl WHERE payment_date = '" & DateTimePickerEJ1.Text & "'", conn)
        '        cmd.CommandType = CommandType.Text
        '        Dim da As New MySqlDataAdapter(cmd)
        '        Dim dtt As New DataTable
        '        da.Fill(dtt)
        '        'MsgBox("Fill")
        '        'Dim dr As MySqlDataReader = cmd.ExecuteReader

        '        MsgBox("File Downloaded!")
        '        conn.Close()
        '    Else
        '        conn.Close()
        '        MsgBox("No Transaction for this day.", MessageBoxIcon.Warning)
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("There was an error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    conn.Close()
        'End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'for displaying current datetime
        LabelDateTime.Text = DateTime.Now.ToString("ddd MM/dd/yyyy HH:mm:ss")
    End Sub

    Private Sub ButtonShowAll_Click(sender As Object, e As EventArgs) Handles ButtonShowAll.Click
        'show all transactions made
        Try
            Dim audit As String = "SELECT or_no, date, time, cashier, manager, activity_performed, vatable, vat, vat_exempt, zero_rated, discount, total_amount FROM audit_trail_tbl"
            Dim adp As New MySqlDataAdapter(audit, conn)
            Dim audit_trail_tblDataSet As New DataSet
            adp.Fill(audit_trail_tblDataSet, "audit_trail_tblDataSet")
            Me.ReportViewerAuditTrail.LocalReport.ReportPath = ""
            Me.ReportViewerAuditTrail.LocalReport.DataSources.Clear()
            Me.ReportViewerAuditTrail.LocalReport.DataSources.Add(New ReportDataSource("audit_trail_tblDataSet", audit_trail_tblDataSet.Tables("audit_trail_tblDataSet")))
            Me.ReportViewerAuditTrail.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonSearchVoid_Click(sender As Object, e As EventArgs) Handles ButtonSearchVoid.Click
        'show filtered transactions
        Try
            Dim audit As String = "SELECT or_no, date, time, cashier, manager, activity_performed, vatable, vat, vat_exempt, zero_rated, discount, total_amount FROM audit_trail_tbl WHERE date BETWEEN '" & DateTimePicker1AuditTrail.Text & "' AND '" & DateTimePicker2AuditTrail.Text & "'"
            Dim adp As New MySqlDataAdapter(audit, conn)
            Dim audit_trail_tblDataSet As New DataSet
            adp.Fill(audit_trail_tblDataSet, "audit_trail_tblDataSet")
            Me.ReportViewerAuditTrail.LocalReport.ReportPath = ""
            Me.ReportViewerAuditTrail.LocalReport.DataSources.Clear()
            Me.ReportViewerAuditTrail.LocalReport.DataSources.Add(New ReportDataSource("audit_trail_tblDataSet", audit_trail_tblDataSet.Tables("audit_trail_tblDataSet")))
            Me.ReportViewerAuditTrail.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonDownload_Click_1(sender As Object, e As EventArgs) Handles ButtonDownload.Click
        '-----------------------------------CREATING FILE------------------------------------
        Dim fileloc As String = "C:\E-Journal\Downloads\" & DateTimePickerEJ1.Text & " to " & DateTimePickerEJ2.Text
        My.Computer.FileSystem.CreateDirectory(fileloc)
        Dim Status          'Check the Status of Function
        Try
            Dim fs As FileStream
            Dim bw As BinaryWriter
            Dim retval As Long
            Dim startIndex As Long = 0
            Dim bs As Integer = 100
            Dim outbyte(bs - 1) As Byte

            conn.Open()
            Dim text_file As String
            'Dim cmd As New MySqlCommand("SELECT text_file, payment_date FROM textfile_tbl WHERE payment_date BETWEEN '" & DateTimePickerEJ1.Text & "' AND '" & DateTimePickerEJ2.Text & "'", conn)
            Dim cmd As New MySqlCommand("SELECT aes_decrypt(text_file,'strdjnltmyp'), DATE_FORMAT(payment_date, '%M %d, %Y') AS date FROM textfile_tbl WHERE payment_date BETWEEN '" & DateTimePickerEJ1.Text & "' AND '" & DateTimePickerEJ2.Text & "'", conn)
            'Dim cmd As New MySqlCommand("SELECT text_file, DATE_FORMAT(payment_date, '%Y %m %d') FROM textfile_tbl WHERE payment_date BETWEEN '" & DateTimePickerEJ1.Text & "' AND '" & DateTimePickerEJ2.Text & "'", conn)

            Dim dr As MySqlDataReader = cmd.ExecuteReader

            Do While dr.Read
                '  text_file = Val(dr.Item("payment_date").ToString())
                text_file = dr.Item(1).ToString
                Dim strtmp As String = dr.GetString(0)

                fs = New FileStream(fileloc & "\" & text_file & ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite)
                'fs.Lock(0, 100)
                bw = New BinaryWriter(fs)
                'text_file.Password = "dsaad"
                startIndex = 0
                retval = dr.GetBytes(0, startIndex, outbyte, 0, bs)

                Do While retval = bs
                    bw.Write(outbyte)
                    bw.Flush()
                    startIndex += bs
                    retval = dr.GetBytes(0, startIndex, outbyte, 0, bs)
                Loop

                Try
                    bw.Write(outbyte, 0, retval - 1)

                Catch ex As Exception
                    MessageBox.Show("There was an error: " & ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                bw.Flush()
                bw.Close()
                fs.Close()

            Loop
            dr.Close()
            conn.Close()

            Dim dialog As DialogResult

            dialog = MessageBox.Show("Selected date data downloaded successfully.", "Success", MessageBoxButtons.OK)

            If dialog = DialogResult.OK Then

                Process.Start(fileloc)
            End If

            Status = "Download Files"

        Catch ex As Exception
            Status = "Failed Download Files"
            'MessageBox.Show("There was an error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MsgBox("There was an error in file downloading!")
            conn.Close()

        End Try

        Try
            ' audit_trail_tbl
            'Dim BranchCode As String = "BR01"
            Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

            Using conn As New MySqlConnection(strConn)
                conn.Open()

                Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                    cmd.Parameters.AddWithValue("p_userid", mainform.LabelCashierID.Text)
                    cmd.Parameters.AddWithValue("p_username", mainform.LabelCashierName.Text)
                    cmd.Parameters.AddWithValue("p_approvedby", login.TextBoxUserID.Text)
                    cmd.Parameters.AddWithValue("p_activity_performed", "Download Files")
                    cmd.Parameters.AddWithValue("p_module", "Management")
                    cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-" & Status)
                    cmd.Parameters.AddWithValue("p_remarks", "Generate Report")
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error: Inserting Status in Audit Trail")
        End Try
    End Sub
    Private Sub CenterText(e As Printing.PrintPageEventArgs, text As String, font As Font, y As Integer)
        Dim x = (e.PageBounds.Width - e.Graphics.MeasureString(text, font).Width) / 2
        e.Graphics.DrawString(text, font, Brushes.Black, x, y)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Private prevGrand As Decimal
        Try

            PrintDocument2.PrinterSettings.PrinterName = "Posiflex PP9000 Printer"  ' Replace with your printer name

            '======== CALL Z-READING STORED PROCEDURE ========
            conn.Open()
            Dim cmd As New MySqlCommand("updateZReading", conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@p_payment_date", DateTimePickerZStart.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@p_pos_id", LabelPOSno.Text)

            Dim outZ As New MySqlParameter("@p_new_zcounter", MySqlDbType.Int32)
            outZ.Direction = ParameterDirection.Output
            cmd.Parameters.Add(outZ)

            Dim outPrev As New MySqlParameter("@p_prev_grand_total", MySqlDbType.Decimal)
            outPrev.Direction = ParameterDirection.Output
            cmd.Parameters.Add(outPrev)

            cmd.ExecuteNonQuery()
            conn.Close()

            Dim newZ As Integer = outZ.Value
            If newZ = -1 Then
                MsgBox("No transactions found.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            '======== PRINT PREVIEW ========

            Dim margins As New Margins(0, 0, 0, 0)
            PrintZReading.DefaultPageSettings.Margins = margins

            ''THERMAL
            'Dim papersize As New PaperSize("Custom", 280, 0)
            Dim papersize As New PaperSize("Custom", 280, 1500)

            PrintZReading.DefaultPageSettings.PaperSize = papersize

            Dim PrintPreviewDialog2 As PrintPreviewDialog = New PrintPreviewDialog
            PrintPreviewDialog2.Document = PrintZReading

            ' Check if the printer is valid
            If Not PrintDocument2.PrinterSettings.IsValid Then
                AddHandler PrintZReading.PrintPage, AddressOf Me.PrintDocument2_PrintPage
                PrintPreviewDialog2.ShowDialog()

                'MsgBox("Printer is not valid!")
                'Return
            Else
                'TO PRINT IMMEDIATELY
                PrintDocument2.Print()

            End If

            Dim folderPath As String = "C:\E-Journal\"
            Dim filePath As String = folderPath & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"

            'MsgBox(filePath)
            Try
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If
                'MsgBox("Looking for file: " & filePath)

                If Not File.Exists(filePath) Then
                    MsgBox("File not found: " & filePath, MsgBoxStyle.Critical)
                    ' MsgBox("File not found")
                    Exit Sub
                End If

                Dim raw As Byte() = File.ReadAllBytes(filePath)

                conn.Open()
                Using ej As New MySqlCommand("saveEjournal", conn)
                    ej.CommandType = CommandType.StoredProcedure
                    ej.Parameters.AddWithValue("@p_payment_date", DateTime.Today.ToString("yyyy-MM-dd"))
                    ej.Parameters.AddWithValue("@p_pos_id", LabelPOSno.Text)
                    ej.Parameters.AddWithValue("@p_text", raw)
                    ej.ExecuteNonQuery()
                End Using
                conn.Close()
                'MsgBox("E-Journal successfully saved.", MsgBoxStyle.Information)
                File.Delete(filePath)

            Catch ex As MySqlException
                'MsgBox("DATABASE ERROR:" & vbCrLf & ex.Message, MsgBoxStyle.Critical)
            End Try

        Catch ex As Exception
            MsgBox("Z-Reading Error...")
            'MsgBox("Z-Reading Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
        Try
            ' audit_trail_tbl
            'Dim BranchCode As String = "BR01"
            Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

            Using conn As New MySqlConnection(strConn)
                conn.Open()

                Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                    cmd.Parameters.AddWithValue("p_userid", mainform.LabelCashierID.Text)
                    cmd.Parameters.AddWithValue("p_username", mainform.LabelCashierName.Text)
                    cmd.Parameters.AddWithValue("p_approvedby", login.TextBoxUserID.Text)
                    cmd.Parameters.AddWithValue("p_activity_performed", "Generate Z-Reading")
                    cmd.Parameters.AddWithValue("p_module", "Management")
                    cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-Report")
                    cmd.Parameters.AddWithValue("p_remarks", "Cashier Report")
                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error: Inserting Status in Audit Trail")
        End Try
    End Sub

    'LOCK FOLDER
    Public Sub LockFolder(folderPath As String)
        Dim dirInfo As New DirectoryInfo(folderPath)
        Dim dirSecurity As DirectorySecurity = dirInfo.GetAccessControl()

        ' Stop inheriting permissions
        dirSecurity.SetAccessRuleProtection(True, False)

        ' Deny everyone
        Dim everyone As New SecurityIdentifier(WellKnownSidType.WorldSid, Nothing)
        dirSecurity.AddAccessRule(New FileSystemAccessRule(
            everyone,
            FileSystemRights.FullControl,
            AccessControlType.Deny))

        ' Allow administrators
        Dim admin As New SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, Nothing)
        dirSecurity.AddAccessRule(New FileSystemAccessRule(
            admin,
            FileSystemRights.FullControl,
            AccessControlType.Allow))

        dirInfo.SetAccessControl(dirSecurity)
    End Sub

    'UNLOCK FOLDER
    Public Sub UnlockFolder(folderPath As String)
        Dim dirInfo As New DirectoryInfo(folderPath)
        Dim dirSecurity As New DirectorySecurity()

        ' Restore inheritance
        dirSecurity.SetAccessRuleProtection(False, True)
        dirInfo.SetAccessControl(dirSecurity)
    End Sub

    Private Function SaveToTextFile(content As String) As String

        Dim folderPath As String = "C:\E-Journal\"

        If Not IO.Directory.Exists(folderPath) Then
            IO.Directory.CreateDirectory(folderPath)
        End If

        Dim filePath As String = folderPath & "XREADING_" & ComboBoxXCashier.Text &
                                 DateTime.Now.ToString("yyyy-MM-dd") & ".txt"

        IO.File.WriteAllText(filePath, content)
        Return filePath

    End Function

    Private Sub SaveToDatabase(filePath As String)

        Dim raw As Byte() = IO.File.ReadAllBytes(filePath)

        Using conn As New MySqlConnection(strConn)
            conn.Open()

            Using cmd As New MySqlCommand("saveEjournal", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@p_payment_date",
                                            DateTime.Today.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@p_pos_id", LabelPOSno.Text)
                cmd.Parameters.AddWithValue("@p_text", raw)
                cmd.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim fs As FileStream
            Dim bw As BinaryWriter
            Dim retval As Long
            Dim startIndex As Long = 0
            Dim bs As Integer = 100
            Dim outbyte(bs - 1) As Byte

            conn.Open()
            Dim text_file, payment_date As String
            'Dim cmd As New MySqlCommand("SELECT text_file, or_no, DATE_FORMAT(payment_date, '%M %d, %Y') FROM e_journal_tbl WHERE payment_date = '" & DateTimePickerEJ1.Text & "' AND payment_date = '" & DateTimePickerEJ2.Text & "' AND POS_ID = '" & LabelPOSno.Text & "'", conn)
            Dim cmd As New MySqlCommand("SELECT text_file, or_no, DATE_FORMAT(payment_date, '%M %d, %Y') FROM e_journal_tbl WHERE payment_date = '" & DateTimePickerEJ1.Text & "' AND POS_ID = '" & LabelPOSno.Text & "'", conn)
            cmd.CommandType = CommandType.Text
            Dim da As New MySqlDataAdapter(cmd)
            Dim dtt As New DataTable
            da.Fill(dtt)
            Dim dr As MySqlDataReader = cmd.ExecuteReader

            Do While dr.Read
                payment_date = dtt.Rows(0)(2).ToString
                'Added
                My.Computer.FileSystem.CreateDirectory("C:\E-Journal\" & payment_date)

                text_file = Val(dr.Item("or_no").ToString)
                Dim strtmp As String = dr.GetString(0)
                Dim fileloc As String = "C:\E-Journal\" & payment_date & "\" & text_file & ".txt"
                fs = New FileStream(fileloc, FileMode.OpenOrCreate, FileAccess.Write)
                bw = New BinaryWriter(fs)
                startIndex = 0
                retval = dr.GetBytes(0, startIndex, outbyte, 0, bs)

                Do While retval = bs
                    bw.Write(outbyte)
                    bw.Flush()
                    startIndex += bs
                    retval = dr.GetBytes(0, startIndex, outbyte, 0, bs)
                Loop

                Try
                    bw.Write(outbyte, 0, retval - 1)

                Catch ex As Exception
                    MessageBox.Show("There was an error: " & ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                bw.Flush()
                bw.Close()
                fs.Close()
            Loop
            dr.Close()
            conn.Close()

            MsgBox("File Downloaded!")

        Catch ex As Exception
            ' MessageBox.Show("There was an error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            MsgBox("File Already Downloaded!")
            conn.Close()
        End Try

        Try
            ' audit_trail_tbl
            'Dim BranchCode As String = "BR01"
            Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

            Using conn As New MySqlConnection(strConn)
                conn.Open()

                Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                    cmd.Parameters.AddWithValue("p_userid", mainform.LabelCashierID.Text)
                    cmd.Parameters.AddWithValue("p_username", mainform.LabelCashierName.Text)
                    cmd.Parameters.AddWithValue("p_approvedby", login.TextBoxUserID.Text)
                    cmd.Parameters.AddWithValue("p_activity_performed", "Download E-Journal")
                    cmd.Parameters.AddWithValue("p_module", "Management")
                    cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-Download Report")
                    cmd.Parameters.AddWithValue("p_remarks", "Cashier Report")
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error: Inserting Status in Audit Trail")
        End Try

    End Sub

    Private Sub ButtonSouvenir_Click(sender As Object, e As EventArgs) Handles ButtonSouvenir.Click
        Souvenir.Show()
    End Sub

    Private Sub ButtonPMethod_Click(sender As Object, e As EventArgs) Handles ButtonPMethod.Click
        PaymentMethod.Show()
    End Sub
End Class
