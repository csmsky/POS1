Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass ' Assuming this is where FDEandD() is
Public Class Souvenir
    Dim strConn As String = FDEandD()
    Private dtSouvenirs As New DataTable()
    Private Sub Souvenir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSouvenirs()
    End Sub
    Private Sub LoadSouvenirs()
        Try
            Using connection As New MySqlConnection(strConn)
                Dim query As String = "SELECT code As Code, type As Type, regular As Regular_Price, introductory As Intro_Price, label_code As Label FROM souvenir_tbl"

                Using cmd As New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dtSouvenirs)
                    End Using
                End Using
            End Using

            dgvSouvenirs.DataSource = dtSouvenirs.DefaultView
            dgvSouvenirs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception
            'MsgBox("Error loading souvenirs: " & ex.Message, MessageBoxIcon.Error)
            MsgBox("Error Loading Souvenirs Data")
        End Try
    End Sub

    Private Sub dgvSouvenirs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSouvenirs.CellClick
        ' Make sure the user clicked a valid row (not the header)
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvSouvenirs.Rows(e.RowIndex)

            txtCode.Text = row.Cells("Code").Value.ToString()
            txtType.Text = row.Cells("Type").Value.ToString()
            txtRegular.Text = row.Cells("Regular_Price").Value.ToString()
            txtIntro.Text = row.Cells("Intro_Price").Value.ToString()
            txtLabel.Text = row.Cells("Label").Value.ToString()
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrWhiteSpace(txtRegular.Text) OrElse String.IsNullOrWhiteSpace(txtType.Text) Then
            MsgBox("Please fill in the required fields (Type and Price).", MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Using connection As New MySqlConnection(strConn)
                Dim query As String = "INSERT INTO souvenir_tbl (code, type, regular, introductory, label_code) " &
                                      "VALUES (@code, @type, @regular, @introductory, @label_code)"
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@code", txtCode.Text)
                    cmd.Parameters.AddWithValue("@type", txtType.Text)
                    cmd.Parameters.AddWithValue("@regular", Convert.ToInt32(Val(txtRegular.Text)))
                    cmd.Parameters.AddWithValue("@introductory", Convert.ToInt32(Val(txtIntro.Text)))
                    cmd.Parameters.AddWithValue("@label_code", txtLabel.Text)

                    connection.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox("Souvenir added successfully!", MessageBoxIcon.Information)
            ClearFields()

        Catch ex As Exception
            'MsgBox("Error adding souvenir: " & ex.Message, MessageBoxIcon.Error)
            MsgBox("Error Adding Souvenir")
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If String.IsNullOrWhiteSpace(txtCode.Text) Then
            MsgBox("Please select a record from the table to update.", MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Using connection As New MySqlConnection(strConn)
                Dim query As String = "UPDATE souvenir_tbl SET type=@type, regular=@regular, " &
                                      "introductory=@introductory, label_code=@label_code WHERE code=@code"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@code", txtCode.Text)
                    cmd.Parameters.AddWithValue("@type", txtType.Text)
                    cmd.Parameters.AddWithValue("@regular", Convert.ToInt32(Val(txtRegular.Text)))
                    cmd.Parameters.AddWithValue("@introductory", Convert.ToInt32(Val(txtIntro.Text)))
                    cmd.Parameters.AddWithValue("@label_code", txtLabel.Text)

                    connection.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox("Souvenir updated successfully!", MessageBoxIcon.Information)
            ClearFields()

        Catch ex As Exception
            'MsgBox("Error updating souvenir: " & ex.Message, MessageBoxIcon.Error)
            MsgBox("Error Updating Souvenir")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If String.IsNullOrWhiteSpace(txtCode.Text) Then
            MsgBox("Please select a record from the table to delete.", MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MsgBox("Are you sure you want to delete this souvenir?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirm Delete") = MsgBoxResult.Yes Then
            Try
                Using connection As New MySqlConnection(strConn)
                    Dim query As String = "DELETE FROM souvenir_tbl WHERE code=@code"
                    Using cmd As New MySqlCommand(query, connection)
                        cmd.Parameters.AddWithValue("@code", txtCode.Text)
                        connection.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MsgBox("Souvenir deleted successfully!", MessageBoxIcon.Information)
                ClearFields()

            Catch ex As Exception
                MsgBox("Error deleting souvenir: " & ex.Message, MessageBoxIcon.Error)
                'MsgBox("Error Deleting Souvenir")
            End Try
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            ' Escape single quotes to prevent crashes if the user types an apostrophe
            Dim safeSearchText As String = txtSearch.Text.Replace("'", "''")

            ' Check if the search box is empty
            If String.IsNullOrWhiteSpace(safeSearchText) Then
                ' If empty, show all rows
                dtSouvenirs.DefaultView.RowFilter = ""
            Else
                ' If typing, filter by Code OR Type
                ' You can add more columns here if you want to search by Label too!
                dtSouvenirs.DefaultView.RowFilter = String.Format("Code LIKE '%{0}%' OR Type LIKE '%{0}%' OR Label LIKE '%{0}%'", safeSearchText)
            End If
        Catch ex As Exception
            ' Silently handle errors if the user types strange characters that confuse the filter
        End Try
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtCode.Clear()
        txtType.Clear()
        txtRegular.Clear()
        txtIntro.Clear()
        txtLabel.Clear()
    End Sub
End Class