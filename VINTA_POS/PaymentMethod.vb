Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass ' Assuming this is where FDEandD() is
Public Class PaymentMethod
    Dim strConn As String = FDEandD()
    Private dtPaymentMethod As New DataTable()
    Private selectedId As Integer = 0

    Private Sub PaymentMethod_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPaymentMethod()
    End Sub

    Private Sub LoadPaymentMethod()
        Try
            Using connection As New MySqlConnection(strConn)
                Dim query As String = "SELECT payment_method As paymentMethod FROM payment_method_tbl"
                Using cmd As New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dtPaymentMethod)
                    End Using
                End Using
            End Using

            dgvPaymentMethod.DataSource = dtPaymentMethod.DefaultView
            dgvPaymentMethod.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception
            MsgBox("Error loading souvenirs: " & ex.Message, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub dgvPaymentMethod_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPaymentMethod.CellClick
        ' Make sure the user clicked a valid row (not the header)
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = dgvPaymentMethod.Rows(e.RowIndex)
                txtPaymentMethod.Text = row.Cells("paymentMethod").Value.ToString()
            End If
        Catch

        End Try

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrWhiteSpace(txtPaymentMethod.Text) Then
            MsgBox("Please select a record from the table to update.", MessageBoxIcon.Warning)
            Exit Sub
        End If
        Try
            Using connection As New MySqlConnection(strConn)
                Dim query As String = "INSERT INTO payment_method_tbl (payment_method) " &
                                      "VALUES (@payment_method)"
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@payment_method", txtPaymentMethod.Text)
                    connection.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox("Payment Method Added Successfully!", MessageBoxIcon.Information)

            txtPaymentMethod.Text = ""

        Catch ex As Exception
            'MsgBox("Error adding payment method: " & ex.Message, MessageBoxIcon.Error)
            MsgBox("Error Adding Payment Method")
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtPaymentMethod.Text) Then
            MsgBox("Please select a record from the table to delete.", MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this payment method?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirm Delete") = MsgBoxResult.Yes Then
            Try
                Using connection As New MySqlConnection(strConn)
                    Dim query As String = "DELETE FROM payment_method_tbl WHERE payment_method=@paymentMethod"
                    Using cmd As New MySqlCommand(query, connection)
                        cmd.Parameters.AddWithValue("@paymentMethod", txtPaymentMethod.Text)
                        connection.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MsgBox("Payment Method Deleted Successfully!", MessageBoxIcon.Information)

                txtPaymentMethod.Text = ""

            Catch ex As Exception
                'MsgBox("Error deleting souvenir: " & ex.Message, MessageBoxIcon.Error)
                MsgBox("Error Deleting Payment Method")
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtPaymentMethod.Text = ""
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            ' Escape single quotes to prevent crashes if the user types an apostrophe
            Dim safeSearchText As String = txtSearch.Text.Replace("'", "''")

            ' Check if the search box is empty
            If String.IsNullOrWhiteSpace(safeSearchText) Then
                ' If empty, show all rows
                dtPaymentMethod.DefaultView.RowFilter = ""
            Else
                ' If typing, filter by Code OR Type
                ' You can add more columns here if you want to search by Label too!
                dtPaymentMethod.DefaultView.RowFilter = String.Format("paymentMethod LIKE '%{0}%'", safeSearchText)
            End If
        Catch ex As Exception
            ' Silently handle errors if the user types strange characters that confuse the filter
        End Try
    End Sub
End Class