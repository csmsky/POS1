Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass

Public Class VIPReload
    Dim strConn As String = FDEandD()
    Dim isEnteringPaidAmount As Boolean = False
    Dim storedTopUpAmount As Decimal = 0



    Private Sub VIPReload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearFields()
        ComboBoxPaymentMethod.Items.Clear()
        Try
            Using dbConn As New MySqlConnection(strConn)
                dbConn.Open()
                Dim query As String = "SELECT payment_method FROM payment_method_tbl"
                Using cmd As New MySqlCommand(query, dbConn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            ComboBoxPaymentMethod.Items.Add(reader("payment_method").ToString())
                        End While
                    End Using
                End Using
            End Using
            ' Select the first one by default if there are any
            If ComboBoxPaymentMethod.Items.Count > 0 Then
                ComboBoxPaymentMethod.SelectedIndex = 0
            End If
        Catch ex As Exception
            MsgBox("Error loading payment methods: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try
    End Sub

    Private Sub ClearFields()
        TextBox1.Clear() ' Card Number
        TextBox2.Clear() ' Customer No
        TextBox3.Clear() ' First Name
        TextBox4.Clear() ' Last Name
        TextBox5.Clear() ' Top-Up Amount

        Label5.Text = "0.00" ' Current Balance
        Label8.Text = "0.00" ' New Balance
        Label12.Text = "P0.00" ' Total
        Label13.Text = "P0.00" ' Paid
        Label14.Text = "P0.00" ' Change

        ' Reset custom variables
        isEnteringPaidAmount = False
        storedTopUpAmount = 0

        mainform.lblApprovedCode.Text = ""
    End Sub

    ' 1. SEARCH VIP CARD (Press ENTER in TextBox1)
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SearchVIPCard(TextBox1.Text.Trim())
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub SearchVIPCard(cardNumber As String)
        If String.IsNullOrWhiteSpace(cardNumber) Then Return

        Try
            Using dbConn As New MySqlConnection(strConn)
                dbConn.Open()
                ' We decrypt the first and last name securely
                Dim query As String = "SELECT customer_no, balance, CAST(AES_DECRYPT(first_name, 'strdjnltmyp') AS CHAR) AS first_name, CAST(AES_DECRYPT(last_name, 'strdjnltmyp') AS CHAR) AS last_name FROM vip_card_tbl WHERE card_number = @card_number"
                Using cmd As New MySqlCommand(query, dbConn)
                    cmd.Parameters.AddWithValue("@card_number", cardNumber)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            TextBox2.Text = reader("customer_no").ToString()
                            TextBox3.Text = reader("first_name").ToString()
                            TextBox4.Text = reader("last_name").ToString()

                            Dim currentBalance As Decimal = Convert.ToDecimal(reader("balance"))
                            Label5.Text = currentBalance.ToString("0.00")
                            CalculateNewBalance()
                        Else
                            MsgBox("VIP Card not found in the system!", MsgBoxStyle.Exclamation, "Not Found")
                            ClearFields()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Database Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Numpad_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click, Button11.Click, Button12.Click, Button13.Click, Button14.Click, Button15.Click, Button16.Click, Button10.Click
        Dim btn As Button = CType(sender, Button)

        If btn.Text = "100" Or btn.Text = "200" Or btn.Text = "500" Or btn.Text = "1000" Then
            Dim currentVal As Decimal = 0
            Decimal.TryParse(TextBox5.Text, currentVal)
            TextBox5.Text = (currentVal + Convert.ToDecimal(btn.Text)).ToString("0.00")
        Else
            TextBox5.Text &= btn.Text
        End If

        If isEnteringPaidAmount Then
            UpdatePaymentLabels()
        Else
            CalculateNewBalance()
        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        ' DEL Button
        If TextBox5.Text.Length > 0 Then
            TextBox5.Text = TextBox5.Text.Substring(0, TextBox5.Text.Length - 1)
        End If

        If isEnteringPaidAmount Then
            UpdatePaymentLabels()
        Else
            CalculateNewBalance()
        End If
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        ' CLR Button
        TextBox5.Clear()

        If isEnteringPaidAmount Then
            UpdatePaymentLabels()
        Else
            CalculateNewBalance()
        End If
    End Sub


    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If isEnteringPaidAmount Then
            UpdatePaymentLabels()
        Else
            CalculateNewBalance()
        End If
    End Sub

    Private Sub UpdatePaymentLabels()
        Dim paid As Decimal = 0
        Decimal.TryParse(TextBox5.Text, paid) ' <-- Now reads directly from the box!

        If ComboBoxPaymentMethod.Text.ToUpper() = "CASH" Then
            Label13.Text = "P" & paid.ToString("0.00") ' Paid
            Dim change As Decimal = paid - storedTopUpAmount
            If change < 0 Then change = 0
            Label14.Text = "P" & change.ToString("0.00") ' Change
        End If
    End Sub


    Private Sub CalculateNewBalance()
        Dim currentBal As Decimal = 0
        Dim topUp As Decimal = 0

        Decimal.TryParse(Label5.Text, currentBal)
        Decimal.TryParse(TextBox5.Text, topUp)

        ' This line makes the New Balance update automatically!
        Label8.Text = (currentBal + topUp).ToString("0.00")

        Label12.Text = "P" & topUp.ToString("0.00") ' Total
        Label14.Text = "P0.00" ' Change
    End Sub


    Private Sub ComboBoxPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPaymentMethod.SelectedIndexChanged
        isEnteringPaidAmount = False
        storedTopUpAmount = 0
        TextBox5.Clear()
        Label13.Text = "P0.00"
        Label14.Text = "P0.00"
    End Sub


    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MsgBox("Please search for a valid VIP Card first!", MsgBoxStyle.Exclamation, "Wait")
            Return
        End If

        ' Use .ToUpper() to safely match the database
        If ComboBoxPaymentMethod.Text.ToUpper() = "CASH" Then
            If Not isEnteringPaidAmount Then
                ' FIRST PRESS: Save Top-Up Amount
                Dim topUp As Decimal = 0
                If Not Decimal.TryParse(TextBox5.Text, topUp) OrElse topUp <= 0 Then
                    MsgBox("Please enter a valid Top-Up Amount!", MsgBoxStyle.Exclamation, "Wait")
                    Return
                End If

                storedTopUpAmount = topUp
                isEnteringPaidAmount = True

                ' Clear the box so they can type the Paid Amount
                TextBox5.Clear()

                MsgBox("Top-Up Amount Confirmed!" & vbCrLf & "Enter Paid Amount.", MsgBoxStyle.Information, "Enter Paid Amount")
                Return
            Else
                ' SECOND PRESS: Validate Paid Amount
                Dim paid As Decimal = 0
                Decimal.TryParse(TextBox5.Text, paid)
                If paid < storedTopUpAmount Then
                    MsgBox("Customer's paid amount is less than the Top-Up Amount!", MsgBoxStyle.Exclamation, "Insufficient Cash")
                    Return
                End If
            End If
        Else
            ' For GCash/Credit Card, just read the Top-Up Amount and proceed
            If Not Decimal.TryParse(TextBox5.Text, storedTopUpAmount) OrElse storedTopUpAmount <= 0 Then
                MsgBox("Please enter a valid Top-Up Amount!", MsgBoxStyle.Exclamation, "Wait")
                Return
            End If

            mainform.lblApprovedCode.Text = "" ' Clear any old codes

            ' === CHECK WHICH POPUP TO SHOW (EXACTLY LIKE MAINFORM) ===
            If ComboBoxPaymentMethod.SelectedIndex >= 3 Then
                ' INDEX 3+: All Cards (Visa, Mastercard, Debit, etc.)
                CardDetails.lblPayment.Text = ComboBoxPaymentMethod.Text
                ApprovedCode.lblPayment_Type.Text = CardDetails.lblPayment.Text

                CardDetails.ShowDialog()

                ' CardDetails automatically opens ApprovedCode when it finishes.
                ' We must pause VIPReload until the ApprovedCode window is closed!
                While Application.OpenForms.OfType(Of ApprovedCode)().Any()
                    Application.DoEvents()
                End While

            ElseIf ComboBoxPaymentMethod.SelectedIndex = 1 Or ComboBoxPaymentMethod.SelectedIndex = 2 Then
                ' INDEX 1 or 2: GCash / E-Wallets
                ApprovedCode.lblPayment_Type.Text = ComboBoxPaymentMethod.Text
                ApprovedCode.ShowDialog()
            End If
            ' =========================================================

            ' If they closed the popup without entering a code, cancel the transaction
            If mainform.lblApprovedCode.Text = "" Or mainform.lblApprovedCode.Text = "lblApprovedCode" Then
                MsgBox("Transaction Cancelled: You must complete the Payment Details!", MsgBoxStyle.Exclamation, "Cancelled")
                Return
            End If
        End If


        ' Proceed with transaction
        Dim result As DialogResult = MessageBox.Show($"Are you sure you want to load ₱{storedTopUpAmount.ToString("0.00")} into Card {TextBox1.Text}?", "Confirm Top-Up", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ExecuteTopUpTransaction(storedTopUpAmount)
        End If
    End Sub




    Private Sub ExecuteTopUpTransaction(amount As Decimal)
        Try
            Using dbConn As New MySqlConnection(strConn)
                dbConn.Open()
                Using tx = dbConn.BeginTransaction()
                    Try
                        ' 1. Update the Card Balance
                        Dim updateQuery As String = "UPDATE vip_card_tbl SET balance = balance + @amount WHERE card_number = @card_number"
                        Using cmdUpdate As New MySqlCommand(updateQuery, dbConn, tx)
                            cmdUpdate.Parameters.AddWithValue("@amount", amount)
                            cmdUpdate.Parameters.AddWithValue("@card_number", TextBox1.Text.Trim())
                            cmdUpdate.ExecuteNonQuery()
                        End Using

                        ' 2. Log into audit_trail_tbl (So the Manager has a record)
                        Using cmdLog As New MySqlCommand("insertingAuditTrail", dbConn, tx)
                            cmdLog.CommandType = CommandType.StoredProcedure
                            cmdLog.Parameters.AddWithValue("p_pos_id", mainform.LabelPOSno.Text)
                            cmdLog.Parameters.AddWithValue("p_userid", mainform.LabelCashierID.Text)
                            cmdLog.Parameters.AddWithValue("p_username", mainform.LabelCashierName.Text)
                            cmdLog.Parameters.AddWithValue("p_approvedby", "Manager")
                            cmdLog.Parameters.AddWithValue("p_activity_performed", "VIP Top-Up")
                            cmdLog.Parameters.AddWithValue("p_module", "VIP Reload")
                            cmdLog.Parameters.AddWithValue("p_reference_id", TextBox1.Text.Trim())
                            cmdLog.Parameters.AddWithValue("p_remarks", $"Added ₱{amount.ToString("0.00")} via {ComboBoxPaymentMethod.Text} (Ref: {mainform.lblApprovedCode.Text})")
                            cmdLog.ExecuteNonQuery()
                        End Using

                        ' 3. Save Receipt to reload_or_tbl
                        Dim reloadOR As String = GenerateReloadOR()

                        Dim paidAmount As Decimal = 0
                        Decimal.TryParse(Label13.Text.Replace("P", ""), paidAmount)
                        Dim changeAmount As Decimal = 0
                        Decimal.TryParse(Label14.Text.Replace("P", ""), changeAmount)

                        ' If it is GCash or Credit Card, the paid amount is strictly the top-up amount
                        If ComboBoxPaymentMethod.Text.ToUpper() <> "CASH" Then
                            paidAmount = amount
                            changeAmount = 0
                        End If

                        Dim insertOR As String = "INSERT INTO reload_or_tbl (or_no, card_number, payment_date, payment_time, pos_id, cashier_id, username, top_up_amount, payment_method, cash_tendered, change_amount, approved_code, upload) " &
                                                 "VALUES (@or_no, @card_number, CURDATE(), CURTIME(), @pos_id, @cashier_id, @username, @top_up_amount, @payment_method, @cash_tendered, @change_amount, @approved_code, 'no')"

                        Using cmdOR As New MySqlCommand(insertOR, dbConn, tx)
                            cmdOR.Parameters.AddWithValue("@or_no", reloadOR)
                            cmdOR.Parameters.AddWithValue("@card_number", TextBox1.Text.Trim())
                            cmdOR.Parameters.AddWithValue("@pos_id", mainform.LabelPOSno.Text)
                            cmdOR.Parameters.AddWithValue("@cashier_id", mainform.LabelCashierID.Text)
                            cmdOR.Parameters.AddWithValue("@username", mainform.LabelCashierName.Text)
                            cmdOR.Parameters.AddWithValue("@top_up_amount", amount)
                            cmdOR.Parameters.AddWithValue("@payment_method", ComboBoxPaymentMethod.Text)
                            cmdOR.Parameters.AddWithValue("@cash_tendered", paidAmount)
                            cmdOR.Parameters.AddWithValue("@change_amount", changeAmount)
                            cmdOR.Parameters.AddWithValue("@approved_code", mainform.lblApprovedCode.Text)

                            cmdOR.ExecuteNonQuery()
                        End Using

                        ' 4. Save to Reload Electronic Journal (reload_ejournal_tbl)
                        Dim insertEJ As String = "INSERT INTO reload_ejournal_tbl(pos_id, cashier_name, payment_date, payment_time, or_no, total_amount, payment_method, cash, change_amount, type_of_transaction) " &
                                                 "VALUES (@pos_id, @cashier_name, CURDATE(), CURTIME(), @or_no, @total_amount, @payment_method, @cash, @change_amount, 'VIP Reload')"

                        Using cmdEJ As New MySqlCommand(insertEJ, dbConn, tx)
                            cmdEJ.Parameters.AddWithValue("@pos_id", mainform.LabelPOSno.Text)
                            cmdEJ.Parameters.AddWithValue("@cashier_name", mainform.LabelCashierName.Text)
                            cmdEJ.Parameters.AddWithValue("@or_no", reloadOR)
                            cmdEJ.Parameters.AddWithValue("@total_amount", amount)
                            cmdEJ.Parameters.AddWithValue("@payment_method", ComboBoxPaymentMethod.Text)
                            cmdEJ.Parameters.AddWithValue("@cash", paidAmount)
                            cmdEJ.Parameters.AddWithValue("@change_amount", changeAmount)
                            cmdEJ.ExecuteNonQuery()
                        End Using


                        ' 5. Create the physical Text File E-Journal in a separate folder
                        Dim folderPath As String = "C:\EJournal_Reload\" & DateTime.Now.ToString("yyyy-MM")
                        Dim textFilePath As String = folderPath & "\" & mainform.LabelPOSno.Text & "_" & DateTime.Now.ToString("yyyyMMdd") & "_Reload.txt"

                        Receipt_OR_Printed.AppendReloadEJournalReceipt(
                            textFilePath,
                            mainform.LabelPOSno.Text,
                            mainform.LabelCashierName.Text,
                            reloadOR,
                            TextBox1.Text.Trim(),
                            amount.ToString("0.00"),
                            ComboBoxPaymentMethod.Text,
                            mainform.lblApprovedCode.Text,
                            paidAmount.ToString("0.00"),
                            changeAmount.ToString("0.00")
                        )

                        tx.Commit()
                        MsgBox($"Successfully reloaded ₱{amount.ToString("0.00")} into Card {TextBox1.Text}!", MsgBoxStyle.Information, "Success")

                        ' Reset everything for the next customer
                        ClearFields()
                        TextBox1.Focus()

                    Catch ex As Exception
                        tx.Rollback()
                        MsgBox("Transaction Failed: " & ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Connection Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Function GenerateReloadOR() As String
        Dim newOR As String = ""
        Try
            Using dbConn As New MySqlConnection(strConn)
                dbConn.Open()
                ' Count how many reloads this POS has done today to get the next sequence number
                Dim query As String = "SELECT COUNT(*) FROM reload_or_tbl WHERE pos_id = @pos_id AND payment_date = CURDATE()"
                Using cmd As New MySqlCommand(query, dbConn)
                    cmd.Parameters.AddWithValue("@pos_id", mainform.LabelPOSno.Text)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    count += 1

                    ' Format: REL-[POS ID]-[DATE]-[SEQUENCE] --> Example: REL-1-20260615-0001
                    newOR = $"REL-{mainform.LabelPOSno.Text}-{DateTime.Now.ToString("yyyyMMdd")}-{count.ToString("D4")}"
                End Using
            End Using
        Catch ex As Exception
            ' Fallback if sequence generation fails
            newOR = $"REL-{DateTime.Now.ToString("yyyyMMddHHmmss")}"
        End Try
        Return newOR
    End Function


End Class