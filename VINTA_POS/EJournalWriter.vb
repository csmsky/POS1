Imports System.IO
Module EJournalWriter
    Public Sub WriteEJournal(filepath As String,
                             pos As String,
                             serial As String,
                             cashier As String,
                             orNo As String,
                             total As String,
                             paymentMethod As String,
                             approvedCode As String,
                             money As String,
                             changeVal As String,
                             vatable As String,
                             vat As String,
                             vatExempt As String,
                             zeroRated As String,
                             guestName As String,
                             guestId As String,
                             guestAddress As String)

        ' Create directory if missing
        Dim dir = Path.GetDirectoryName(filepath)
        If Not Directory.Exists(dir) Then
            Directory.CreateDirectory(dir)
        End If

        Using file As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(filepath, True)

            file.WriteLine("           METTA CITY")
            file.WriteLine("         PINOY CATAMARAN")
            file.WriteLine("          #17 SILAHIS STREET,")
            file.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
            file.WriteLine("              PHILIPPINES")
            file.WriteLine("      VAT REG TIN: 745-993-747-000")
            file.WriteLine("       POS" & pos & "-SN: " & serial)
            file.WriteLine("            MIN: XXXXXXXXXX")
            file.WriteLine("----------------------------------------")
            file.WriteLine("Cashier: " & cashier)
            file.WriteLine("=========================================")
            file.WriteLine("            SALES INVOICE ")
            file.WriteLine("=========================================")
            file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
            file.WriteLine("SI #: " & orNo)
            file.WriteLine("Reset Counter: " & mainform.rstCnt.ToString())
            file.WriteLine("==========================================")
            file.WriteLine("Description   Qty     U.Price     Amount")
            file.WriteLine("==========================================")

            ' 🔥 Loop all ListView items in MAIN FORM
            For Each li As ListViewItem In mainform.ListViewCashier.Items
                Dim itemName = li.SubItems(0).Text
                Dim qty = li.SubItems(1).Text
                Dim price = li.SubItems(2).Text
                Dim amount = li.SubItems(3).Text
                Dim amountDiscount = li.SubItems(4).Text
                Dim typeOfDiscount = li.SubItems(9).Text

                file.WriteLine(itemName)
                file.WriteLine("         " & qty & " x ₱" & price & "    ₱" & amount)

                If typeOfDiscount <> "Regular" Then
                    file.WriteLine("  " & typeOfDiscount & ": ₱(" & amountDiscount & ")")
                End If
            Next

            file.WriteLine("")
            file.WriteLine("==========================================")
            If (paymentMethod = "CASH") Then
                file.WriteLine("TOTAL AMOUNT :                 ₱ " & total)
                file.WriteLine("PAYMENT " & paymentMethod & ":  ₱ " & money)
                file.WriteLine("CHANGE                        ₱ " & changeVal)
            Else
                file.WriteLine("PAYMENT:                  ₱ " & total)
                file.WriteLine("  " & paymentMethod)
                file.WriteLine("CHANGE:                   ₱ " & "0.00")
                file.WriteLine("APPROVED CODE:  " & approvedCode)
            End If
            file.WriteLine("")
            file.WriteLine("=========================================")
            file.WriteLine("             VAT INFORMATION")
            file.WriteLine("=========================================")
            file.WriteLine("(V) VATable Sales                  ₱ " & vatable)
            file.WriteLine("(VAT 12%)                          ₱ " & vat)
            file.WriteLine("(X) VAT-Exempt Sales               ₱ " & vatExempt)
            file.WriteLine("(Z) Zero-Rated Sales               ₱ " & zeroRated)
            file.WriteLine("")
            file.WriteLine("=========================================")
            file.WriteLine("   THIS SERVES AS YOUR SALES INVOICE")
            file.WriteLine("=========================================")
            file.WriteLine("")
        End Using
    End Sub
End Module
