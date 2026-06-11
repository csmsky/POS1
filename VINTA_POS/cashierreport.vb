Imports System.Drawing.Printing
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass
Imports System.Security.AccessControl
Public Class cashierreport
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    Dim PrintXReading As PrintDocument = New PrintDocument
    Private Sub ButtonPrintXReport_Click(sender As Object, e As EventArgs) Handles ButtonPrintXReport.Click

        ' Set margins
        Dim margins As New Margins(0, 0, 0, 0)
        'PrintDocument1.DefaultPageSettings.Margins = margins

        With PrintDocument1.DefaultPageSettings
            .Margins = margins

            .Landscape = False

            .PaperSize = New PaperSize("Custom", 280, 1000)
        End With

        ' Create preview dialog
        Dim preview As New PrintPreviewDialog()
        preview.Document = PrintDocument1

        ' optional: start zoomed out to see the whole page
        DirectCast(preview.PrintPreviewControl, PrintPreviewControl).Zoom = 0.7

        ' Show preview window
        preview.ShowDialog()

        ' If you still want an option to print directly from code (without preview),
        'you can use a separate button that calls
        PrintDocument1.Print()

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage

        'X-Reading Report
        Try

            Dim cash_float, cash_drop, no_of_transaction, x_begin_or_no, x_end_or_no, cash_sales, online_payment_sales, total_sales, discount, vatable_sale, vat_exempt, vat, zero_rated_sale, no_of_void, void_amount, grand_total As String

            Dim xvalue As New MySqlCommand("SELECT cash_float, cash_drop, no_of_transaction, begin_or_no, end_or_no, cash_sales, online_payment_sales, total_sales, discount, vatable, vat_exempt, vat, zero_rated, no_of_void, void_amount, grand_total FROM accumulated_amount_tbl WHERE user_id = '" & mainform.LabelCashierID.Text & "' AND pos_id = '" & mainform.LabelPOSno.Text & "' AND payment_date = '" & DateTimePickerXReport.Text & "'", conn)
            xvalue.CommandType = CommandType.Text
            Dim xda As New MySqlDataAdapter(xvalue)
            Dim xdt As New DataTable
            xvalue.CommandTimeout = 0

            xda.Fill(xdt)

            cash_float = xdt.Rows(0)(0).ToString
            cash_drop = xdt.Rows(0)(1).ToString
            no_of_transaction = xdt.Rows(0)(2).ToString
            x_begin_or_no = xdt.Rows(0)(3).ToString
            x_end_or_no = xdt.Rows(0)(4).ToString
            cash_sales = xdt.Rows(0)(5).ToString
            online_payment_sales = xdt.Rows(0)(6).ToString
            total_sales = xdt.Rows(0)(7).ToString
            discount = xdt.Rows(0)(8).ToString
            vatable_sale = xdt.Rows(0)(9).ToString
            vat_exempt = xdt.Rows(0)(10).ToString
            vat = xdt.Rows(0)(11).ToString
            zero_rated_sale = xdt.Rows(0)(12).ToString
            no_of_void = xdt.Rows(0)(13).ToString
            void_amount = xdt.Rows(0)(14).ToString
            grand_total = xdt.Rows(0)(15).ToString

            'X-Report
            Dim TextToPrint As String = ""
            Dim N As New Font("Merchant Copy", 8)
            Dim H As New Font("Merchant Copy", 8)
            Dim B As New Font("Merchant Copy", 10, FontStyle.Bold)

            Dim datetoday As String = String.Format("{0:dd, yyyy}", DateTime.Now)
            Dim timetoday As DateTime = String.Format("{0:HH:mm:ss}", DateTime.Now)
            Dim convertedTimeToday As String = timetoday.ToString("hh:mm tt")

            Dim sngCenterPage As Single

            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Far

            TextToPrint &= Environment.NewLine
            e.Graphics.DrawImage(mainform.PictureBoxLogoReceipt.Image, 92, 0)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("METTA CITY", N).Width / 2)
            e.Graphics.DrawString("METTA CITY", N, Brushes.Black, sngCenterPage, 70)

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

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS" & mainform.LabelPOSno.Text & "-SN: " & mainform.LabelSerial.Text, N).Width / 2)
            e.Graphics.DrawString("MACHINE-SN: " & mainform.LabelSerial.Text, N, Brushes.Black, sngCenterPage, 142)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("MIN: XXXXXXXXXX", N).Width / 2)
            e.Graphics.DrawString("MIN: XXXXXXXXXX", N, Brushes.Black, sngCenterPage, 154)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 165)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("X-READING", B).Width / 2)
            e.Graphics.DrawString("X-READING", B, Brushes.Black, sngCenterPage, 173)
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Date of: " & DateTimePickerXReport.Text, N).Width / 2)
            e.Graphics.DrawString("Date of: " & DateTimePickerXReport.Text, N, Brushes.Black, sngCenterPage, 190)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 195)

            'Cashier Name
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Cashier: " & mainform.LabelCashierName.Text, N).Width / 2)
            e.Graphics.DrawString("Cashier: " & mainform.LabelCashierName.Text, N, Brushes.Black, sngCenterPage, 205)

            'POS ID
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS ID " & mainform.LabelPOSno.Text, B).Width / 2)
            e.Graphics.DrawString("POS ID: " & mainform.LabelPOSno.Text, N, Brushes.Black, sngCenterPage, 220)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("-----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("-----------------------------------------", N, Brushes.Black, sngCenterPage, 230)

            'SI SUMMARy
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(vbTab & "SALES INVOICE SUMMARY", B).Width / 2)
            e.Graphics.DrawString(vbTab & "SALES INVOICE SUMMARY", N, Brushes.Black, sngCenterPage, 240)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("-----------------------------------------", N, Brushes.Black, sngCenterPage, 250)

            'SI Details
            e.Graphics.DrawString("Beginning SI No.:" & ControlChars.NewLine &
                                  "Ending SI No.:" & ControlChars.NewLine & ControlChars.NewLine &
                                  "No. of Transation:" & ControlChars.NewLine &
                                  "No. of Void:" & ControlChars.NewLine &
                                  "", N, Brushes.Black, New RectangleF(4, 265, 0, 0))
            e.Graphics.DrawString(x_begin_or_no & ControlChars.NewLine &
                                  x_end_or_no & ControlChars.NewLine & ControlChars.NewLine &
                                  no_of_transaction & ControlChars.NewLine &
                                  no_of_void & ControlChars.NewLine &
                                  "", N, Brushes.Black, New RectangleF(100, 265, 175, 175), sf)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 325)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(vbTab & "SALES SUMMARY", B).Width / 2)
            e.Graphics.DrawString(vbTab & "SALES SUMMARY", N, Brushes.Black, sngCenterPage, 335)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 345)

            'SALE SUMMARY
            e.Graphics.DrawString("Gross Sales:" & ControlChars.NewLine &
                                  "   Vatable:" & ControlChars.NewLine &
                                  "   VAT(12%):" & ControlChars.NewLine &
                                  "   Vat-Exempt:" & ControlChars.NewLine &
                                  "   Zero-Rated:" & ControlChars.NewLine & ControlChars.NewLine &
                                  "   Total Gross Sales:" & ControlChars.NewLine &
                                  "   Total Discounts:" & ControlChars.NewLine & ControlChars.NewLine &
                                  "NET SALES:" & ControlChars.NewLine & ControlChars.NewLine &
                                  "LESS VOIDED SALES:" & ControlChars.NewLine & ControlChars.NewLine &
                                  "NET COLLECTION:", N, Brushes.Black, New RectangleF(4, 360, 0, 0))
            e.Graphics.DrawString("" & ControlChars.NewLine &
                                  vatable_sale & ControlChars.NewLine &
                                  vat & ControlChars.NewLine &
                                  vat_exempt & ControlChars.NewLine &
                                  zero_rated_sale & ControlChars.NewLine & ControlChars.NewLine &
                                  total_sales & ControlChars.NewLine &
                                  discount & ControlChars.NewLine & ControlChars.NewLine &
                                  total_sales & ControlChars.NewLine & ControlChars.NewLine &
                                  void_amount & ControlChars.NewLine & ControlChars.NewLine &
                                  grand_total, N, Brushes.Black, New RectangleF(100, 360, 175, 175), sf)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 550)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(vbTab & "CASH ACCOUNTABILITY", B).Width / 2)
            e.Graphics.DrawString(vbTab & "CASH ACCOUNTABILITY ", N, Brushes.Black, sngCenterPage, 560)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 570)

            'CASH IN and DROP and EXPECTED INCOME
            e.Graphics.DrawString("Cash Float:" & ControlChars.NewLine & ControlChars.NewLine &
                                  "Cash Drop:" & ControlChars.NewLine & ControlChars.NewLine &
                                  "Total Sales:", N, Brushes.Black, New RectangleF(4, 585, 0, 0))
            e.Graphics.DrawString(cash_float & ControlChars.NewLine & ControlChars.NewLine &
                                  cash_drop & ControlChars.NewLine & ControlChars.NewLine &
                                  grand_total, N, Brushes.Black, New RectangleF(100, 585, 175, 175), sf)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
            e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 650)

            'Printed by
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Printed By: " & mainform.LabelCashierName.Text, N).Width / 2)
            e.Graphics.DrawString("Printed By: " & mainform.LabelCashierName.Text, N, Brushes.Black, sngCenterPage, 670)

            'Printed date & time
            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Printed on" & MonthName(Now.Month) & " " & datetoday, N).Width / 2)
            e.Graphics.DrawString("Printed on " & MonthName(Now.Month) & " " & datetoday, N, Brushes.Black, sngCenterPage, 685)

            sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("at " & timetoday, N).Width / 2)
            e.Graphics.DrawString("at " & timetoday, N, Brushes.Black, sngCenterPage, 700)


            '----- INSERTING AND UPDATE E-Journa X-Report -------
            Try
                Dim filepath As String = "C:\E-Journal\X_Reading\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"

                Dim sw As StreamWriter

                Dim grossSales As Decimal
                grossSales = CDec(vatable_sale) + CDec(vat) + CDec(vat_exempt) + CDec(zero_rated_sale)

                If (Not File.Exists(filepath)) Then

                    sw = File.CreateText(filepath)
                    sw.WriteLine("           METTA CITY")
                    sw.WriteLine("            PINOY CATAMARAN")
                    sw.WriteLine("          #17 SILAHIS STREET,")
                    sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                    sw.WriteLine("              PHILIPPINES")
                    sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                    sw.WriteLine("      POS" & mainform.LabelPOSno.Text & "-SN: " & mainform.LabelSerial.Text)
                    sw.WriteLine("            MIN: XXXXXXXXXX")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("              X-READING")
                    sw.WriteLine("         Date of: " & DateTimePickerXReport.Text)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("       Cashier:  " & mainform.LabelCashierName.Text)
                    sw.WriteLine("            POS ID: " & mainform.LabelPOSno.Text)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("           SALES INVOICE SUMMARY")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Beginning OR No.:                 " & x_begin_or_no)
                    sw.WriteLine("Ending OR No.:                    " & x_end_or_no)
                    sw.WriteLine("")
                    sw.WriteLine("No. of Transaction                " & no_of_transaction)
                    sw.WriteLine("No. of Void:                      " & no_of_void)
                    sw.WriteLine("")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("              SALES SUMMARY")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Gross Sales:")
                    sw.WriteLine("  Vatable Sales:                  " & vatable_sale)
                    sw.WriteLine("  VAT(12%)                        " & vat)
                    sw.WriteLine("  VAT-Exempt:                     " & vat_exempt)
                    sw.WriteLine("  Zero-Rated:                     " & zero_rated_sale)
                    sw.WriteLine("")
                    sw.WriteLine("  Total Gross Sales:              " & grossSales)
                    sw.WriteLine("  Total Discounts:                " & discount)
                    sw.WriteLine("")
                    sw.WriteLine("NET SALES:                        " & total_sales)
                    sw.WriteLine("")
                    sw.WriteLine("LESS VOIDED SALES:                " & void_amount)
                    sw.WriteLine("")
                    sw.WriteLine("NET COLLECTION:                   " & grand_total)
                    sw.WriteLine("")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("          CASH ACCOUNTABILITY")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Cash Float:                    " & cash_float)
                    sw.WriteLine("Cash Drop:                     " & cash_drop)
                    sw.WriteLine("Total Sales:                   " & grand_total)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("")
                    sw.WriteLine("")
                    sw.WriteLine("        Printed By: " & mainform.LabelCashierName.Text)
                    sw.WriteLine("    Printed on : " & MonthName(Now.Month) & " " & datetoday)
                    sw.WriteLine("          at: " & timetoday)
                    sw.WriteLine("")
                Else
                    sw = File.AppendText(filepath)
                    sw.WriteLine("           METTA CITY")
                    sw.WriteLine("            PINOY CATAMARAN")
                    sw.WriteLine("          #17 SILAHIS STREET,")
                    sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                    sw.WriteLine("              PHILIPPINES")
                    sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                    sw.WriteLine("      POS" & mainform.LabelPOSno.Text & "-SN: " & mainform.LabelSerial.Text)
                    sw.WriteLine("            MIN: XXXXXXXXXX")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("              X-READING")
                    sw.WriteLine("         Date of: " & DateTimePickerXReport.Text)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("       Cashier:  " & mainform.LabelCashierName.Text)
                    sw.WriteLine("            POS ID: " & mainform.LabelPOSno.Text)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("           SALES INVOICE SUMMARY")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Beginning OR No.:                 " & x_begin_or_no)
                    sw.WriteLine("Ending OR No.:                    " & x_end_or_no)
                    sw.WriteLine("")
                    sw.WriteLine("No. of Transaction                " & no_of_transaction)
                    sw.WriteLine("No. of Void:                      " & no_of_void)
                    sw.WriteLine("")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("              SALES SUMMARY")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Gross Sales:")
                    sw.WriteLine("  Vatable Sales:                  " & vatable_sale)
                    sw.WriteLine("  VAT(12%)                        " & vat)
                    sw.WriteLine("  VAT-Exempt:                     " & vat_exempt)
                    sw.WriteLine("  Zero-Rated:                     " & zero_rated_sale)
                    sw.WriteLine("")
                    sw.WriteLine("  Total Gross Sales:              " & grossSales)
                    sw.WriteLine("  Total Discounts:                " & discount)
                    sw.WriteLine("")
                    sw.WriteLine("NET SALES:                        " & total_sales)
                    sw.WriteLine("")
                    sw.WriteLine("LESS VOIDED SALES:                " & void_amount)
                    sw.WriteLine("")
                    sw.WriteLine("NET COLLECTION:                   " & grand_total)
                    sw.WriteLine("")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("          CASH ACCOUNTABILITY")
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("Cash Float:                       " & cash_float)
                    sw.WriteLine("Cash Drop:                        " & cash_drop)
                    sw.WriteLine("Total Sales:                   " & grand_total)
                    sw.WriteLine("----------------------------------------")
                    sw.WriteLine("")
                    sw.WriteLine("")
                    sw.WriteLine("        Printed By: " & mainform.LabelCashierName.Text)
                    sw.WriteLine("    Printed on : " & MonthName(Now.Month) & " " & datetoday)
                    sw.WriteLine("          at: " & timetoday)
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

            sf.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Sub cashierreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePickerXReport.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
    End Sub
End Class