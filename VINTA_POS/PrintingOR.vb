
Imports System.Drawing
Imports System.Drawing.Printing
Public Class PrintingOR

    Dim Company_Name As String = "METTA CITY"
    Dim Company_Address As String = "San Juan City"
    Dim Companny_TIN As String = "VAT REG TIN: XXX-XXX-XXX-XXX"
    Dim Companny_Min As String = "XXXXXXXX"

    Dim cashierName As String = mainform.LabelCashierName.Text
    Dim saleInovice As String = mainform.TextBoxBarcode.Text
    Dim totalDue As String = mainform.LabelTotal.Text
    Dim paymentMethod As String = mainform.ComboBoxPaymentMethod.Text

    ' Declare the PrintDocument object
    Private WithEvents printInvoice As New PrintDocument()

    ' Method to trigger the print process
    Public Sub PrintReceipt()
        ' Set the printer settings (optional)
        printInvoice.PrinterSettings.PrinterName = "Posiflex PP9000 Printer" ' Optional, specify printer name if needed

        ' Trigger the print job
        printInvoice.Print()
    End Sub

    ' The PrintPage event is where you define what gets printed
    Private Sub printInvoice_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printInvoice.PrintPage
        ' Set up the graphics object to draw text
        Dim graphics As Graphics = e.Graphics
        Dim font As New Font("Arial", 10)
        Dim brush As New SolidBrush(Color.Black)

        ' Starting Y position for printing
        Dim yPos As Integer = 100

        ' Print Receipt Header
        graphics.DrawString("              " & Company_Name, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("           " & Company_Name, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("          " & Company_Name, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("     " & Company_Name, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("              " & Company_Name, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("      VAT REG TIN: " & Company_Name, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("      MACHINE:" & Company_Name & "-SN: " & Company_Name, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("            MIN: " & Company_Name, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("----------------------------------------", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("Cashier: " & cashierName, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("========================================", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("            SERVICE INVOICE ", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("========================================", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("Date: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("SI #: " & saleInovice, font, brush, 100, yPos) 'added
        yPos += 20                                                          'added
        graphics.DrawString("Reset Counter: " & mainform.rstCnt.ToString(), font, brush, 100, yPos)  'added
        yPos += 20
        graphics.DrawString("========================================", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("Description                               ", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("            Qty     U.Price     Amount", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("========================================", font, brush, 100, yPos)
        yPos += 20

        ' Loop through ListView and print items
        For Each li As ListViewItem In mainform.ListViewCashier.Items
            Dim itemName = li.SubItems(0).Text
            Dim qty = li.SubItems(1).Text
            Dim price = li.SubItems(2).Text
            Dim amount = li.SubItems(3).Text
            Dim amountDiscount = li.SubItems(4).Text
            Dim typeOfDiscount = li.SubItems(9).Text

            ' Print item details
            graphics.DrawString(itemName.PadRight(40) & qty.PadLeft(5) & " x " & "₱ " & price.PadLeft(8) & " ₱ " & amount.PadLeft(10), font, brush, 100, yPos)
            yPos += 20

            If typeOfDiscount <> "Regular" Then
                graphics.DrawString("         " & typeOfDiscount & ": ₱(" & "-" & amountDiscount & ")", font, brush, 100, yPos)
                yPos += 20
            End If
        Next

        ' Print total amount
        graphics.DrawString("========================================", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("TOTAL AMOUNT:             ₱ " & totalDue, font, brush, 100, yPos)
        yPos += 20

        ' Payment method and change
        If paymentMethod = "CASH" Then
            graphics.DrawString("PAYMENT:                  ₱ " & totalDue, font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("  " & paymentMethod, font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("CHANGE:                   ₱ " & totalDue, font, brush, 100, yPos)
        Else
            graphics.DrawString("PAYMENT:                  ₱ " & totalDue, font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("  " & paymentMethod, font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("CHANGE:                   ₱ " & "0.00", font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("APPROVED CODE:  " & paymentMethod, font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("  " & totalDue, font, brush, 100, yPos)
        End If

        ' Print VAT information
        graphics.DrawString("========================================", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("             VAT INFORMATION", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("(V) VATable Sales:        ₱ " & totalDue, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("(VAT 12%):                ₱ " & totalDue, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("(X) VAT-Exempt Sales:     ₱ " & totalDue, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("(Z) Zero-Rated Sales:     ₱ " & totalDue, font, brush, 100, yPos)
        yPos += 20

        ' Print discount details if applicable
        If totalDue <> "Regular" Then
            graphics.DrawString("========================================", font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("         DISCOUNT DETAILS", font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("Name: " & totalDue, font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("ID No.: " & totalDue, font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("Address.: " & totalDue, font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString("___________________", font, brush, 100, yPos)
            yPos += 20
            graphics.DrawString(" " & totalDue & " Signature", font, brush, 100, yPos)
            yPos += 20
        End If

        ' Footer
        graphics.DrawString("========================================", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("   THIS SERVES AS YOUR SERVICE INVOICE", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("========================================", font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("          PTU No.: " & Companny_Min, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("        Date Issued: " & Today.ToString("yyyy/MM/dd"), font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("        Valid Until: " & Companny_Min, font, brush, 100, yPos)
        yPos += 20
        graphics.DrawString("     --- END OF TRANSACTION ---", font, brush, 100, yPos)
        yPos += 20

    End Sub
    Private Sub CenterText(e As Printing.PrintPageEventArgs, text As String, font As Font, y As Integer)
        Dim x = (e.PageBounds.Width - e.Graphics.MeasureString(text, font).Width) / 2
        e.Graphics.DrawString(text, font, Brushes.Black, x, y)
    End Sub
End Class
