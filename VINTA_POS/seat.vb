Imports System.Data
Imports System.IO
Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass

Public Class seat
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    Dim str, str1, str2, str3, str4, str5
    Private Sub Button1A_Click(sender As Object, e As EventArgs) Handles Button1A.Click

        ' str = mainform.ComboBoxTime.Text.Split("-")
        str1 = str(0).ToString & "-1A"

        If Button1A.BackColor = Color.Snow Then
            Button1A.BackColor = Color.LawnGreen

            Dim conn As New MySqlConnection(strConn)
            Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@seat_id", str1)

            Dim dt As New DataTable()
            Dim adapter As New MySqlDataAdapter(cmd)

            adapter.Fill(dt)
            Try

                LabelID.Text = dt.Rows(0)(0).ToString()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        ElseIf Button1A.BackColor = Color.LawnGreen Then
            Button1A.BackColor = Color.Snow

            LabelID.Text = Nothing
        End If
    End Sub

    Private Sub ButtonDone_Click(sender As Object, e As EventArgs) Handles ButtonDone.Click
        'for throwing data to the mainform
        Try
            Dim con As New MySqlConnection(strConn)
            Dim cmdd As New MySqlCommand("select seat_no from seat_tbl where seat_id = @seat_id", con)
            cmdd.Parameters.AddWithValue("@seat_id", LabelID.Text)

            Dim dtt As New DataTable()
            Dim adapterr As New MySqlDataAdapter(cmdd)

            adapterr.Fill(dtt)
            Try

                ' mainform.TextBoxSeat.Text = dtt.Rows(0)(0).ToString()
                secondscreen.Seat.Text = dtt.Rows(0)(0).ToString()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            If LabelID.Text = Nothing Then
                'mainform.TextBoxSeat.Clear()
                secondscreen.Seat.Text = Nothing
            Else
                conn.Open()
                Dim cmd As New MySqlCommand("select seat_no from seat_tbl where seat_id = @seat_id", conn)

                Dim dt As New DataTable()
                Dim adapter As New MySqlDataAdapter(cmd)

                adapterr.Fill(dt)
                Try

                    'mainform.TextBoxSeat.Text = dt.Rows(0)(0).ToString()
                    secondscreen.Seat.Text = dt.Rows(0)(0).ToString()
                Finally

                End Try
                conn.Close()
            End If
        Catch ex As Exception
        End Try

        Me.Hide()
    End Sub

    Private Sub Button1B_Click(sender As Object, e As EventArgs) Handles Button1B.Click
        ' str = mainform.ComboBoxTime.Text.Split("-")
        str2 = str(0).ToString & "-1B"

        If Button1B.BackColor = Color.Snow Then
            Button1B.BackColor = Color.LawnGreen

            Dim conn As New MySqlConnection(strConn)
            Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@seat_id", str2)

            Dim dt As New DataTable()
            Dim adapter As New MySqlDataAdapter(cmd)

            adapter.Fill(dt)
            Try

                LabelID.Text = dt.Rows(0)(0).ToString()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        ElseIf Button1B.BackColor = Color.LawnGreen Then
            Button1B.BackColor = Color.Snow

            LabelID.Text = Nothing
        End If


    End Sub

    Private Sub Button1C_Click(sender As Object, e As EventArgs) Handles Button1C.Click
        'str = mainform.ComboBoxTime.Text.Split("-")
        str3 = str(0).ToString & "-1C"

        If Button1C.BackColor = Color.Snow Then
            Button1C.BackColor = Color.LawnGreen

            Dim conn As New MySqlConnection(strConn)
            Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@seat_id", str3)

            Dim dt As New DataTable()
            Dim adapter As New MySqlDataAdapter(cmd)

            adapter.Fill(dt)
            Try

                LabelID.Text = dt.Rows(0)(0).ToString()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        ElseIf Button1C.BackColor = Color.LawnGreen Then
            Button1C.BackColor = Color.Snow

            LabelID.Text = Nothing
        End If
    End Sub

    Private Sub Button1D_Click(sender As Object, e As EventArgs) Handles Button1D.Click
        'str = mainform.ComboBoxTime.Text.Split("-")
        str4 = str(0).ToString & "-1D"

        If Button1D.BackColor = Color.Snow Then
            Button1D.BackColor = Color.LawnGreen

            Dim conn As New MySqlConnection(strConn)
            Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@seat_id", str4)

            Dim dt As New DataTable()
            Dim adapter As New MySqlDataAdapter(cmd)

            adapter.Fill(dt)
            Try

                LabelID.Text = dt.Rows(0)(0).ToString()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        ElseIf Button1D.BackColor = Color.LawnGreen Then
            Button1D.BackColor = Color.Snow

            LabelID.Text = Nothing
        End If
    End Sub

    Private Sub Button1E_Click(sender As Object, e As EventArgs) Handles Button1E.Click
        'str = mainform.ComboBoxTime.Text.Split("-")
        str5 = str(0).ToString & "-1E"

        If Button1E.BackColor = Color.Snow Then
            Button1E.BackColor = Color.LawnGreen

            Dim conn As New MySqlConnection(strConn)
            Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@seat_id", str5)

            Dim dt As New DataTable()
            Dim adapter As New MySqlDataAdapter(cmd)

            adapter.Fill(dt)
            Try

                LabelID.Text = dt.Rows(0)(0).ToString()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        ElseIf Button1E.BackColor = Color.LawnGreen Then
            Button1E.BackColor = Color.Snow

            LabelID.Text = Nothing
        End If
    End Sub

    Private Sub Button1F_Click(sender As Object, e As EventArgs) Handles Button1F.Click
        If Button1F.BackColor = Color.Snow Then
            Button1F.BackColor = Color.LawnGreen
        ElseIf Button1F.BackColor = Color.LawnGreen Then
            Button1F.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 6)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1G_Click(sender As Object, e As EventArgs) Handles Button1G.Click
        If Button1G.BackColor = Color.Snow Then
            Button1G.BackColor = Color.LawnGreen
        ElseIf Button1G.BackColor = Color.LawnGreen Then
            Button1G.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 7)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1H_Click(sender As Object, e As EventArgs) Handles Button1H.Click
        If Button1H.BackColor = Color.Snow Then
            Button1H.BackColor = Color.LawnGreen
        ElseIf Button1H.BackColor = Color.LawnGreen Then
            Button1H.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 8)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2A_Click(sender As Object, e As EventArgs) Handles Button2A.Click
        If Button2A.BackColor = Color.Snow Then
            Button2A.BackColor = Color.LawnGreen
        ElseIf Button2A.BackColor = Color.LawnGreen Then
            Button2A.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 9)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2B_Click(sender As Object, e As EventArgs) Handles Button2B.Click
        If Button2B.BackColor = Color.Snow Then
            Button2B.BackColor = Color.LawnGreen
        ElseIf Button2B.BackColor = Color.LawnGreen Then
            Button2B.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 10)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2C_Click(sender As Object, e As EventArgs) Handles Button2C.Click
        If Button2C.BackColor = Color.Snow Then
            Button2C.BackColor = Color.LawnGreen
        ElseIf Button2C.BackColor = Color.LawnGreen Then
            Button2C.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 11)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2D_Click(sender As Object, e As EventArgs) Handles Button2D.Click
        If Button2D.BackColor = Color.Snow Then
            Button2D.BackColor = Color.LawnGreen
        ElseIf Button2D.BackColor = Color.LawnGreen Then
            Button2D.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 12)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2E_Click(sender As Object, e As EventArgs) Handles Button2E.Click
        If Button2E.BackColor = Color.Snow Then
            Button2E.BackColor = Color.LawnGreen
        ElseIf Button2E.BackColor = Color.LawnGreen Then
            Button2E.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 13)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2F_Click(sender As Object, e As EventArgs) Handles Button2F.Click
        If Button2F.BackColor = Color.Snow Then
            Button2F.BackColor = Color.LawnGreen
        ElseIf Button2F.BackColor = Color.LawnGreen Then
            Button2F.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 14)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2G_Click(sender As Object, e As EventArgs) Handles Button2G.Click
        If Button2G.BackColor = Color.Snow Then
            Button2G.BackColor = Color.LawnGreen
        ElseIf Button2G.BackColor = Color.LawnGreen Then
            Button2G.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 15)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2H_Click(sender As Object, e As EventArgs) Handles Button2H.Click
        If Button2H.BackColor = Color.Snow Then
            Button2H.BackColor = Color.LawnGreen
        ElseIf Button2H.BackColor = Color.LawnGreen Then
            Button2H.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 16)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3A_Click(sender As Object, e As EventArgs) Handles Button3A.Click
        If Button3A.BackColor = Color.Snow Then
            Button3A.BackColor = Color.LawnGreen
        ElseIf Button3A.BackColor = Color.LawnGreen Then
            Button3A.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 17)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3B_Click(sender As Object, e As EventArgs) Handles Button3B.Click
        If Button3B.BackColor = Color.Snow Then
            Button3B.BackColor = Color.LawnGreen
        ElseIf Button3B.BackColor = Color.LawnGreen Then
            Button3B.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("seat_id", 18)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3C_Click(sender As Object, e As EventArgs) Handles Button3C.Click
        If Button3C.BackColor = Color.Snow Then
            Button3C.BackColor = Color.LawnGreen
        ElseIf Button3C.BackColor = Color.LawnGreen Then
            Button3C.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 19)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3D_Click(sender As Object, e As EventArgs) Handles Button3D.Click
        If Button3D.BackColor = Color.Snow Then
            Button3D.BackColor = Color.LawnGreen
        ElseIf Button3D.BackColor = Color.LawnGreen Then
            Button3D.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 20)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3E_Click(sender As Object, e As EventArgs) Handles Button3E.Click
        If Button3E.BackColor = Color.Snow Then
            Button3E.BackColor = Color.LawnGreen
        ElseIf Button3E.BackColor = Color.LawnGreen Then
            Button3E.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 21)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3F_Click(sender As Object, e As EventArgs) Handles Button3F.Click
        If Button3F.BackColor = Color.Snow Then
            Button3F.BackColor = Color.LawnGreen
        ElseIf Button3F.BackColor = Color.LawnGreen Then
            Button3F.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 22)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3G_Click(sender As Object, e As EventArgs) Handles Button3G.Click
        If Button3G.BackColor = Color.Snow Then
            Button3G.BackColor = Color.LawnGreen
        ElseIf Button3G.BackColor = Color.LawnGreen Then
            Button3G.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 23)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3H_Click(sender As Object, e As EventArgs) Handles Button3H.Click
        If Button3H.BackColor = Color.Snow Then
            Button3H.BackColor = Color.LawnGreen
        ElseIf Button3H.BackColor = Color.LawnGreen Then
            Button3H.BackColor = Color.Snow
        End If

        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand("select seat_id from seat_tbl where seat_id = @seat_id", conn)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@seat_id", 24)

        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter(cmd)

        adapter.Fill(dt)
        Try

            LabelID.Text = dt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub seat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelID.Hide()

        'for taking the company logo from resources
        'PictureBoxCompanyLogo.Image = WindowsApplication1.My.Resources.Logo
        PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA
    End Sub
End Class