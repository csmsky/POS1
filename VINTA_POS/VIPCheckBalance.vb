Imports MySql.Data.MySqlClient

Public Class VIPCheckBalance
    Dim strConn As String = WindowsApplication1.ConfigClass.FDEandD()

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        ' Clear the dynamic labels on the right for a fresh search
        LabelName.Text = "---"
        LabelCustomerNo.Text = "---"
        LabelBalance.Text = "₱0.00"

        If String.IsNullOrWhiteSpace(TextBoxCardNumber.Text) Then
            MsgBox("Please enter a VIP Card Number.", MsgBoxStyle.Exclamation, "Missing Info")
            Return
        End If

        Try
            Using dbConn As New MySqlConnection(strConn)
                dbConn.Open()
                ' Fetch everything including the decrypted names
                Dim query As String = "SELECT card_number, customer_no, balance, CAST(AES_DECRYPT(first_name, 'strdjnltmyp') AS CHAR) AS first_name, CAST(AES_DECRYPT(last_name, 'strdjnltmyp') AS CHAR) AS last_name FROM vip_card_tbl WHERE card_number = @card_number"

                Using cmd As New MySqlCommand(query, dbConn)
                    cmd.Parameters.AddWithValue("@card_number", TextBoxCardNumber.Text.Trim())

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' Grab the data
                            Dim customerNo As String = reader("customer_no").ToString()
                            Dim lastName As String = reader("last_name").ToString()
                            Dim firstName As String = reader("first_name").ToString()
                            Dim currentBalance As Decimal = Convert.ToDecimal(reader("balance"))

                            ' Push the raw data into the labels on the right side
                            LabelName.Text = firstName & " " & lastName
                            LabelCustomerNo.Text = customerNo
                            LabelBalance.Text = "₱" & currentBalance.ToString("N2")

                            secondscreen.ListViewCustomerView.Items.Clear()
                            Dim item As New ListViewItem("-") ' Qty
                            item.SubItems.Add("Current VIP Balance") ' Description
                            item.SubItems.Add("") ' Price
                            item.SubItems.Add(currentBalance.ToString("0.00")) ' Total
                            item.SubItems.Add("") ' Discount
                            item.SubItems.Add(currentBalance.ToString("0.00")) ' Amount
                            secondscreen.ListViewCustomerView.Items.Add(item)
                        Else
                            ' Customer Not Found!
                            MsgBox("VIP Card not found in the system.", MsgBoxStyle.Information, "Not Found")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Database Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' This automatically resets the screen when it opens
    Private Sub VIPCheckBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxCardNumber.Text = ""
        LabelName.Text = "---"
        LabelCustomerNo.Text = "---"
        LabelBalance.Text = "₱0.00"

        secondscreen.LabelTotalCustomerView.Text = "0.00"
        secondscreen.LabelCashTendered.Text = "0.00"
        secondscreen.LabelChangeCustomerChange.Text = "0.00"
        secondscreen.ListViewCustomerView.Items.Clear()
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        ' Hide the balance from the 2nd screen when leaving
        secondscreen.ListViewCustomerView.Items.Clear()

        Me.Close()
    End Sub
End Class
