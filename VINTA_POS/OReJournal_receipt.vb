Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient
Public Class Receipt_OR_Printed
    'Company Details
    Public Shared CompName As String = "METTACITY"
    Public Shared CompNameB As String = "Glory International Technology Inc."
    Public Shared AddressA As String = "CINEMA Level 4, Glorietta 4"
    Public Shared AddressB As String = "Ayala Center, San Lorenzo"
    Public Shared AddressC As String = "City of Makati, NCR, 4th District"
    Public Shared AddressD As String = "Metro Manila, PHILIPPINES"
    Public Shared VATTIN As String = "VAT REG TIN: 671-476-518-00001"

    Public Shared MIN_No As String = "MIN: 26020709360088690"      'POS 1
    'Public Shared MIN_No As String = "MIN: 26020709360088691"     'POS 2
    'Public Shared MIN_No As String = "MIN: 26020709360088692"     'POS 3

    Public Shared POS_ID_Number As String = " S/N: XTNB0545001"   'POS 1
    'Public Shared POS_ID_Number As String = " S/N: XTNB0699001"   'POS 2
    'Public Shared POS_ID_Number As String = " S/N:  XTNB0312001"   'POS 3

    Public Shared V_PTU_No As String = "PTU No.: FP022026-047-0582979-00001"     'POS 1
    'Public Shared V_PTU_No As String = "PTU No.: FP022026-047-0582980-00001"     'POS 2
    'Public Shared V_PTU_No As String = "PTU No.: FP022026-047-0582981-00001"      'POS 3

    'System Vendor Details
    Public Shared V_Name As String = "VINTA INTELLIGENT TECHNOLOGIES CORPORATION"
    Public Shared V_VatReg As String = "VAT REG. TIN#: 745-993-747-000"
    Public Shared V_Accreditation_No As String = "Accred No.: XXXXXX"
    Public Shared V_Date_Issued As String = "Date Issued: 02/09/2026"

    Dim cashierName As String = mainform.LabelCashierName.Text
    Dim saleInovice As String = mainform.TextBoxBarcode.Text
    Dim totalDue As String = mainform.LabelTotal.Text
    Dim paymentMethod As String = mainform.ComboBoxPaymentMethod.Text

    ' Declare the PrintDocument object
    Private WithEvents printInvoice As New PrintDocument()
    Public Shared Sub PrintReceipt(filepath As String,
                                  pos As String,
                                  serial As String,
                                  cashier As String,
                                  orNo As String,
                                  itemName As String,
                                  qty As String,
                                  price As String,
                                  amount As String,
                                  amountDiscount As String,
                                  typeOfDiscount As String,
                                  total As String,
                                  paymentMethod As String,
                                  approvedCode As String,
                                  money As String,
                                  changeVal As String,
                                  guestName As String,
                                  guestId As String,
                                  guestTin As String,
                                  guestAddress As String,
                                  vatable As String,
                                  vat As String,
                                  vatExempt As String,
                                  zeroRated As String,
                                  discount As String,
                                  transType As String,
                                  lessvat As String)

        Using sw As StreamWriter = File.CreateText(filepath)

            sw.WriteLine("              " & CompName)
            sw.WriteLine("           " & CompNameB)
            sw.WriteLine("          " & AddressA)
            sw.WriteLine("     " & AddressB)
            sw.WriteLine("              " & AddressC)
            sw.WriteLine("          " & AddressD)
            sw.WriteLine("         " & VATTIN)
            sw.WriteLine("         POS" & POS_ID & " " & POS_ID_Number)
            sw.WriteLine("         " & MIN_No)
            sw.WriteLine("========================================")
            sw.WriteLine("            SALES INVOICE ")
            sw.WriteLine("========================================")
            sw.WriteLine("Cashier: " & cashier)
            sw.WriteLine("SI #: " & Terminal_ID & orNo)
            sw.WriteLine("Reset Counter: " & mainform.rstCnt.ToString())
            sw.WriteLine("Date/Time:" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
            sw.WriteLine("========================================")
            sw.WriteLine("Description                               ")
            sw.WriteLine("            Qty     U.Price     Amount")
            sw.WriteLine("========================================")

            Dim totalQty As Integer = 0

            For Each li As ListViewItem In mainform.ListViewCashier.Items

                itemName = li.SubItems(0).Text
                qty = li.SubItems(1).Text
                price = li.SubItems(2).Text
                amount = li.SubItems(3).Text
                amountDiscount = li.SubItems(4).Text
                typeOfDiscount = li.SubItems(10).Text

                totalQty += qty

                sw.WriteLine(itemName)
                sw.WriteLine("             " & qty & "  x   " & "₱ " & price & "  ₱ " & amount)
                If typeOfDiscount = "Regular" Then

                Else
                    sw.WriteLine("         " & typeOfDiscount & ": ₱(" & "-" & amountDiscount & ")")

                End If
            Next
            sw.WriteLine("")
            sw.WriteLine("========================================")
            sw.WriteLine("**" & totalQty & " Item(s)**")
            sw.WriteLine("AMOUNT DUE :             ₱ " & total)

            If (paymentMethod = "CASH") Then
                sw.WriteLine("PAYMENT:                  ₱ " & money)
                sw.WriteLine("  " & paymentMethod)
                sw.WriteLine("CHANGE:                   ₱ " & changeVal)
            ElseIf (paymentMethod = "DEBIT CARD") Or (paymentMethod = "CREDIT CARD") Then
                sw.WriteLine("PAYMENT:                  ₱ " & money)
                sw.WriteLine("  " & paymentMethod)
                sw.WriteLine("CARD NAME:                   ₱ " & mainform.lblCname.Text)
                sw.WriteLine("CARD NO.:                   ₱ " & mainform.lblCno.Text)
                sw.WriteLine("APPROVED CODE:  " & approvedCode)
            Else
                sw.WriteLine("PAYMENT:                  ₱ " & total)
                sw.WriteLine("  " & paymentMethod)
                sw.WriteLine("APPROVED CODE:  " & approvedCode)
            End If
            sw.WriteLine("")
            sw.WriteLine("========================================")
            sw.WriteLine("             VAT INFORMATION")
            sw.WriteLine("========================================")
            sw.WriteLine("VATable:              ₱ " & vatable)
            sw.WriteLine("VAT(12%)              ₱ " & vat)
            sw.WriteLine("VAT-Exempt:           ₱ " & vatExempt)
            sw.WriteLine("Zero-Rated:           ₱ " & zeroRated)
            sw.WriteLine("Less Vat(12%):        ₱ " & lessvat)
            sw.WriteLine("")

            If (typeOfDiscount = "Regular") Then
                sw.WriteLine("========================================")
                sw.WriteLine("         CUSTOMER DETAILS")
                sw.WriteLine("========================================")
                sw.WriteLine("")
                sw.WriteLine("Name :" & guestName)
                sw.WriteLine("")
                sw.WriteLine("ID:" & guestId)
                sw.WriteLine("")
                sw.WriteLine("TIN No. :" & guestTin)
                sw.WriteLine("")
                sw.WriteLine("Address :" & guestAddress)
                sw.WriteLine("")
                sw.WriteLine("")
                sw.WriteLine("Signature: ____________________________")
                sw.WriteLine("")
            Else
                sw.WriteLine("========================================")
                sw.WriteLine("         CUSTOMER DETAILS")
                sw.WriteLine("========================================")
                sw.WriteLine("")
                sw.WriteLine("Name: " & guestName)
                sw.WriteLine("ID: " & guestId)
                sw.WriteLine("TIN No.:" & guestTin)
                sw.WriteLine("Address.: " & guestAddress)
                sw.WriteLine("")
                sw.WriteLine("___________________")
                sw.WriteLine(" " & typeOfDiscount & " Signature")

                sw.WriteLine("")
            End If
            sw.WriteLine("========================================")
            sw.WriteLine("   THIS SERVES AS YOUR SALES INVOICE")
            sw.WriteLine("========================================")
            sw.WriteLine("")
            'sw.WriteLine("    " & V_Name)
            'sw.WriteLine("         " & V_VatReg)
            'sw.WriteLine("       " & V_Accreditation_No)
            sw.WriteLine("      " & V_Date_Issued)
            sw.WriteLine("        " & V_PTU_No)
            sw.WriteLine("")
            sw.WriteLine("    Thank you! Please come again!")
            sw.WriteLine("")
            sw.WriteLine("")
            sw.WriteLine("     --- END OF TRANSACTION ---")
            sw.WriteLine("")
            sw.WriteLine("")

            'sw.WriteLine("        Date Issued: " & Today.ToString("yyyy/MM/dd"))

        End Using
    End Sub

    Public Shared Sub AppendEJournalReceipt(filepath As String,
                                  pos As String,
                                  serial As String,
                                  cashier As String,
                                  orNo As String,
                                  itemName As String,
                                  qty As String,
                                  price As String,
                                  amount As String,
                                  amountDiscount As String,
                                  typeOfDiscount As String,
                                  total As String,
                                  paymentMethod As String,
                                  approvedCode As String,
                                  money As String,
                                  changeVal As String,
                                  guestName As String,
                                  guestId As String,
                                  guestTin As String,
                                  guestAddress As String,
                                  vatable As String,
                                  vat As String,
                                  vatExempt As String,
                                  zeroRated As String,
                                  discount As String,
                                  transType As String,
                                  lessvat As String)

        Try

            Dim dir As String = Path.GetDirectoryName(filepath)
            If Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            Dim totalQty As Integer = 0

            Using sw As StreamWriter = File.AppendText(filepath)

                sw.WriteLine("              " & CompName)
                sw.WriteLine("           " & CompNameB)
                sw.WriteLine("          " & AddressA)
                sw.WriteLine("     " & AddressB)
                sw.WriteLine("              " & AddressC)
                sw.WriteLine("          " & AddressD)
                sw.WriteLine("         " & VATTIN)
                sw.WriteLine("         POS" & POS_ID & " " & POS_ID_Number)
                sw.WriteLine("         " & MIN_No)
                sw.WriteLine("========================================")
                sw.WriteLine("            SALES INVOICE ")
                sw.WriteLine("========================================")
                sw.WriteLine("Cashier: " & cashier)
                sw.WriteLine("SI #: " & Terminal_ID & orNo)
                sw.WriteLine("Reset Counter: " & mainform.rstCnt.ToString())
                sw.WriteLine("Date/Time:" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                sw.WriteLine("========================================")
                sw.WriteLine("Description                               ")
                sw.WriteLine("            Qty     U.Price     Amount")
                sw.WriteLine("========================================")

                For Each li As ListViewItem In mainform.ListViewCashier.Items

                    itemName = li.SubItems(0).Text
                    qty = li.SubItems(1).Text
                    price = li.SubItems(2).Text
                    amount = li.SubItems(3).Text
                    amountDiscount = li.SubItems(4).Text
                    typeOfDiscount = li.SubItems(10).Text

                    totalQty += qty

                    sw.WriteLine(itemName)
                    sw.WriteLine("             " & qty & "  x   " & "₱ " & price & "  ₱ " & amount)
                    If typeOfDiscount = "Regular" Then

                    Else
                        sw.WriteLine("         " & typeOfDiscount & ": ₱(" & "-" & amountDiscount & ")")

                    End If

                Next
                sw.WriteLine("")
                sw.WriteLine("========================================")
                sw.WriteLine("**" & totalQty & " Item(s)**")
                sw.WriteLine("AMOUNT DUE:             ₱ " & total)

                If (paymentMethod = "CASH") Then
                    sw.WriteLine("PAYMENT:                  ₱ " & money)
                    sw.WriteLine("  " & paymentMethod)
                    sw.WriteLine("CHANGE:                   ₱ " & changeVal)
                ElseIf (paymentMethod = "DEBIT CARD") Or (paymentMethod = "CREDIT CARD") Then
                    sw.WriteLine("PAYMENT:                  ₱ " & money)
                    sw.WriteLine("  " & paymentMethod)
                    sw.WriteLine("CARD NAME:                   ₱ " & mainform.lblCname.Text)
                    sw.WriteLine("CARD NO.:                   ₱ " & mainform.lblCno.Text)
                    sw.WriteLine("APPROVED CODE:  " & approvedCode)
                Else
                    sw.WriteLine("PAYMENT:                  ₱ " & total)
                    sw.WriteLine("  " & paymentMethod)
                    sw.WriteLine("APPROVED CODE:  " & approvedCode)
                End If

                sw.WriteLine("")
                sw.WriteLine("========================================")
                sw.WriteLine("             VAT INFORMATION")
                sw.WriteLine("========================================")
                sw.WriteLine("VATable:              ₱ " & vatable)
                sw.WriteLine("VAT(12%)              ₱ " & vat)
                sw.WriteLine("VAT-Exempt:           ₱ " & vatExempt)
                sw.WriteLine("Zero-Rated:           ₱ " & zeroRated)
                sw.WriteLine("Less Vat(12%):        ₱ " & lessvat)
                sw.WriteLine("")

                If (typeOfDiscount = "Regular") Then
                    sw.WriteLine("========================================")
                    sw.WriteLine("         CUSTOMER DETAILS")
                    sw.WriteLine("========================================")
                    sw.WriteLine("")
                    sw.WriteLine("Name :" & guestName)
                    sw.WriteLine("")
                    sw.WriteLine("ID:" & guestId)
                    sw.WriteLine("")
                    sw.WriteLine("TIN No. :" & guestTin)
                    sw.WriteLine("")
                    sw.WriteLine("Address :" & guestAddress)
                    sw.WriteLine("")
                    sw.WriteLine("")
                    sw.WriteLine("Signature: ____________________________")
                    sw.WriteLine("")
                Else
                    sw.WriteLine("========================================")
                    sw.WriteLine("         CUSTOMER DETAILS")
                    sw.WriteLine("========================================")
                    sw.WriteLine("")
                    sw.WriteLine("Name: " & guestName)
                    sw.WriteLine("ID: " & guestId)
                    sw.WriteLine("TIN No.: " & guestTin)
                    sw.WriteLine("Address.: " & guestAddress)
                    sw.WriteLine("")
                    sw.WriteLine("___________________")
                    sw.WriteLine(" " & typeOfDiscount & " Signature")

                    sw.WriteLine("")
                End If
                sw.WriteLine("========================================")
                sw.WriteLine("   THIS SERVES AS YOUR SALES INVOICE")
                sw.WriteLine("========================================")
                sw.WriteLine("")
                'sw.WriteLine("    " & V_Name)
                'sw.WriteLine("         " & V_VatReg)
                'sw.WriteLine("       " & V_Accreditation_No)
                sw.WriteLine("      " & V_Date_Issued)
                sw.WriteLine("        " & V_PTU_No)
                sw.WriteLine("")
                sw.WriteLine("    Thank you! Please come again!")
                sw.WriteLine("")
                sw.WriteLine("")
                sw.WriteLine("     --- END OF TRANSACTION ---")
                sw.WriteLine("")

                sw.Close()
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub MakeTextswEJournal(filepath As String,
                                  pos As String,
                                  serial As String,
                                  cashier As String,
                                  orNo As String,
                                  itemName As String,
                                  qty As String,
                                  price As String,
                                  amount As String,
                                  amountDiscount As String,
                                  typeOfDiscount As String,
                                  total As String,
                                  paymentMethod As String,
                                  approvedCode As String,
                                  money As String,
                                  changeVal As String,
                                  guestName As String,
                                  guestId As String,
                                  guestTin As String,
                                  guestAddress As String,
                                  vatable As String,
                                  vat As String,
                                  vatExempt As String,
                                  zeroRated As String,
                                  discount As String,
                                  transType As String,
                                  lessvat As String)

        Using file As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(filepath, False)

            file.WriteLine("              " & CompName)
            file.WriteLine("           " & CompNameB)
            file.WriteLine("          " & AddressA)
            file.WriteLine("     " & AddressB)
            file.WriteLine("              " & AddressC)
            file.WriteLine("          " & AddressD)
            file.WriteLine("         " & VATTIN)
            file.WriteLine("         POS" & POS_ID & " " & POS_ID_Number)
            file.WriteLine("         " & MIN_No)
            file.WriteLine("========================================")
            file.WriteLine("            SALES INVOICE ")
            file.WriteLine("========================================")
            file.WriteLine("Cashier: " & cashier)
            file.WriteLine("SI #: " & Terminal_ID & orNo)
            file.WriteLine("Reset Counter: " & mainform.rstCnt.ToString())
            file.WriteLine("Date/Time:" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
            file.WriteLine("========================================")
            file.WriteLine("Description                               ")
            file.WriteLine("            Qty     U.Price     Amount")
            file.WriteLine("========================================")

            Dim totalQty As Integer = 0

            For Each li As ListViewItem In mainform.ListViewCashier.Items

                itemName = li.SubItems(0).Text
                qty = li.SubItems(1).Text
                price = li.SubItems(2).Text
                amount = li.SubItems(3).Text
                amountDiscount = li.SubItems(4).Text
                typeOfDiscount = li.SubItems(10).Text

                totalQty += qty

                file.WriteLine(itemName)
                file.WriteLine("             " & qty & "  x   " & "₱ " & price & "  ₱ " & amount)
                If typeOfDiscount = "Regular" Then

                Else
                    file.WriteLine("         " & typeOfDiscount & ": ₱(" & "-" & amountDiscount & ")")

                End If

            Next
            file.WriteLine("")
            file.WriteLine("========================================")
            file.WriteLine("**" & totalQty & " Item(s)**")
            file.WriteLine("AMOUNT DUE:             ₱ " & total)

            If (paymentMethod = "CASH") Then
                file.WriteLine("PAYMENT:                  ₱ " & money)
                file.WriteLine("  " & paymentMethod)
                file.WriteLine("CHANGE:                   ₱ " & changeVal)
            ElseIf (paymentMethod = "DEBIT CARD") Or (paymentMethod = "CREDIT CARD") Then
                file.WriteLine("PAYMENT:                  ₱ " & money)
                file.WriteLine("  " & paymentMethod)
                file.WriteLine("CARD NAME:                   ₱ " & mainform.lblCname.Text)
                file.WriteLine("CARD NO.:                   ₱ " & mainform.lblCno.Text)
                file.WriteLine("APPROVED CODE:  " & approvedCode)
            Else
                file.WriteLine("PAYMENT:                  ₱ " & total)
                file.WriteLine("  " & paymentMethod)
                file.WriteLine("APPROVED CODE:  " & approvedCode)
            End If

            file.WriteLine("")
            file.WriteLine("========================================")
            file.WriteLine("             VAT INFORMATION")
            file.WriteLine("========================================")
            file.WriteLine("VATable:              ₱ " & vatable)
            file.WriteLine("VAT(12%)              ₱ " & vat)
            file.WriteLine("VAT-Exempt:           ₱ " & vatExempt)
            file.WriteLine("Zero-Rated:           ₱ " & zeroRated)
            file.WriteLine("Less Vat(12%):        ₱ " & lessvat)
            file.WriteLine("")

            If (typeOfDiscount = "Regular") Then
                file.WriteLine("========================================")
                file.WriteLine("         CUSTOMER DETAILS")
                file.WriteLine("========================================")

                file.WriteLine("Name :" & guestName)
                file.WriteLine("")
                file.WriteLine("ID:" & guestId)
                file.WriteLine("")
                file.WriteLine("TIN No. :" & guestTin)
                file.WriteLine("")
                file.WriteLine("Address :" & guestAddress)
                file.WriteLine("")
                file.WriteLine("")
                file.WriteLine("Signature: ____________________________")
                file.WriteLine("")
            Else
                file.WriteLine("========================================")
                file.WriteLine("         CUSTOMER DETAILS")
                file.WriteLine("========================================")
                file.WriteLine("")
                file.WriteLine("Name: " & guestName)
                file.WriteLine("")
                file.WriteLine("ID: " & guestId)
                file.WriteLine("")
                file.WriteLine("TIN No.: " & guestTin)
                file.WriteLine("")
                file.WriteLine("Address.: " & guestAddress)
                file.WriteLine("")
                file.WriteLine("")
                file.WriteLine("___________________")
                file.WriteLine(" " & typeOfDiscount & " Signature")

                file.WriteLine("")
            End If
            file.WriteLine("========================================")
            file.WriteLine("   THIS SERVES AS YOUR SALES INVOICE")
            file.WriteLine("========================================")
            file.WriteLine("")
            'file.WriteLine("    " & V_Name)
            'file.WriteLine("         " & V_VatReg)
            'file.WriteLine("       " & V_Accreditation_No)
            file.WriteLine("      " & V_Date_Issued)
            file.WriteLine("        " & V_PTU_No)
            file.WriteLine("")
            file.WriteLine("    Thank you! Please come again!")
            file.WriteLine("")
            file.WriteLine("")
            file.WriteLine("     --- END OF TRANSACTION ---")
            file.WriteLine("")
        End Using
    End Sub
End Class
