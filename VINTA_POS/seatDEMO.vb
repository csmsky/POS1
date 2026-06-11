Imports System.Data
Imports System.IO
Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass

Public Class seatDEMO
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    'Dim strConn As String = FDEandD()
    'Dim conn As New MySqlConnection(strConn)

    Dim str, str1, str2, str3, str4, str5
    Private Sub Button1A_Click(sender As Object, e As EventArgs) Handles Button1A.Click
        mainformDEMO.TextBoxSeat.Text = Button1A.Text

        If Button1A.BackColor = Color.Snow Then
            Button1A.BackColor = Color.LawnGreen


        ElseIf Button1A.BackColor = Color.LawnGreen Then
            Button1A.BackColor = Color.Snow
        End If
    End Sub

    Private Sub ButtonDone_Click(sender As Object, e As EventArgs) Handles ButtonDone.Click
        Me.Hide()
        If LabelID.Text = Nothing Then
            secondscreen.Seat.Text = Nothing
        Else
            secondscreen.Seat.Text = mainformDEMO.TextBoxSeat.Text
        End If
        mainformDEMO.Show()
    End Sub

    Private Sub Button1B_Click(sender As Object, e As EventArgs) Handles Button1B.Click
        If Button1B.BackColor = Color.Snow Then
            Button1B.BackColor = Color.LawnGreen
        ElseIf Button1B.BackColor = Color.LawnGreen Then
            Button1B.BackColor = Color.Snow
        End If

        mainformDEMO.TextBoxSeat.Text = Button1B.Text
    End Sub

    Private Sub Button1C_Click(sender As Object, e As EventArgs) Handles Button1C.Click
        If Button1C.BackColor = Color.Snow Then
            Button1C.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button1C.Text
        ElseIf Button1C.BackColor = Color.LawnGreen Then
            Button1C.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button1D_Click(sender As Object, e As EventArgs) Handles Button1D.Click
        If Button1D.BackColor = Color.Snow Then
            Button1D.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button1D.Text
        ElseIf Button1D.BackColor = Color.LawnGreen Then
            Button1D.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button1E_Click(sender As Object, e As EventArgs) Handles Button1E.Click
        If Button1E.BackColor = Color.Snow Then
            Button1E.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button1E.Text
        ElseIf Button1E.BackColor = Color.LawnGreen Then
            Button1E.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button1F_Click(sender As Object, e As EventArgs) Handles Button1F.Click
        If Button1F.BackColor = Color.Snow Then
            Button1F.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button1F.Text
        ElseIf Button1F.BackColor = Color.LawnGreen Then
            Button1F.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button1G_Click(sender As Object, e As EventArgs) Handles Button1G.Click
        If Button1G.BackColor = Color.Snow Then
            Button1G.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button1G.Text
        ElseIf Button1G.BackColor = Color.LawnGreen Then
            Button1G.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button1H_Click(sender As Object, e As EventArgs) Handles Button1H.Click
        If Button1H.BackColor = Color.Snow Then
            Button1H.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button1H.Text
        ElseIf Button1H.BackColor = Color.LawnGreen Then
            Button1H.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button2A_Click(sender As Object, e As EventArgs) Handles Button2A.Click
        If Button2A.BackColor = Color.Snow Then
            Button2A.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button2A.Text
        ElseIf Button2A.BackColor = Color.LawnGreen Then
            Button2A.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button2B_Click(sender As Object, e As EventArgs) Handles Button2B.Click
        If Button2B.BackColor = Color.Snow Then
            Button2B.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button2B.Text
        ElseIf Button2B.BackColor = Color.LawnGreen Then
            Button2B.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button2C_Click(sender As Object, e As EventArgs) Handles Button2C.Click
        If Button2C.BackColor = Color.Snow Then
            Button2C.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button2C.Text
        ElseIf Button2C.BackColor = Color.LawnGreen Then
            Button2C.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button2D_Click(sender As Object, e As EventArgs) Handles Button2D.Click
        If Button2D.BackColor = Color.Snow Then
            Button2D.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button2D.Text
        ElseIf Button2D.BackColor = Color.LawnGreen Then
            Button2D.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button2E_Click(sender As Object, e As EventArgs) Handles Button2E.Click
        If Button2E.BackColor = Color.Snow Then
            Button2E.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button2E.Text
        ElseIf Button2E.BackColor = Color.LawnGreen Then
            Button2E.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button2F_Click(sender As Object, e As EventArgs) Handles Button2F.Click
        If Button2F.BackColor = Color.Snow Then
            Button2F.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button2F.Text
        ElseIf Button2F.BackColor = Color.LawnGreen Then
            Button2F.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button2G_Click(sender As Object, e As EventArgs) Handles Button2G.Click
        If Button2G.BackColor = Color.Snow Then
            Button2G.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button2G.Text
        ElseIf Button2G.BackColor = Color.LawnGreen Then
            Button2G.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button2H_Click(sender As Object, e As EventArgs) Handles Button2H.Click
        If Button2H.BackColor = Color.Snow Then
            Button2H.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button2H.Text
        ElseIf Button2H.BackColor = Color.LawnGreen Then
            Button2H.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button3A_Click(sender As Object, e As EventArgs) Handles Button3A.Click
        If Button3A.BackColor = Color.Snow Then
            Button3A.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button3A.Text
        ElseIf Button3A.BackColor = Color.LawnGreen Then
            Button3A.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button3B_Click(sender As Object, e As EventArgs) Handles Button3B.Click
        If Button3B.BackColor = Color.Snow Then
            Button3B.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button3B.Text
        ElseIf Button3B.BackColor = Color.LawnGreen Then
            Button3B.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button3C_Click(sender As Object, e As EventArgs) Handles Button3C.Click
        If Button3C.BackColor = Color.Snow Then
            Button3C.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button3C.Text
        ElseIf Button3C.BackColor = Color.LawnGreen Then
            Button3C.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button3D_Click(sender As Object, e As EventArgs) Handles Button3D.Click
        If Button3D.BackColor = Color.Snow Then
            Button3D.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button3D.Text
        ElseIf Button3D.BackColor = Color.LawnGreen Then
            Button3D.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button4A_Click(sender As Object, e As EventArgs) Handles Button4A.Click
        If Button4A.BackColor = Color.Snow Then
            Button4A.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button4A.Text
        ElseIf Button4A.BackColor = Color.LawnGreen Then
            Button4A.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button4B_Click(sender As Object, e As EventArgs) Handles Button4B.Click
        If Button4B.BackColor = Color.Snow Then
            Button4B.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button4B.Text
        ElseIf Button4B.BackColor = Color.LawnGreen Then
            Button4B.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button4C_Click(sender As Object, e As EventArgs) Handles Button4C.Click
        If Button4C.BackColor = Color.Snow Then
            Button4C.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button4C.Text
        ElseIf Button4C.BackColor = Color.LawnGreen Then
            Button4C.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button4D_Click(sender As Object, e As EventArgs) Handles Button4D.Click
        If Button4D.BackColor = Color.Snow Then
            Button4D.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button4D.Text
        ElseIf Button4D.BackColor = Color.LawnGreen Then
            Button4D.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button4E_Click(sender As Object, e As EventArgs) Handles Button4E.Click
        If Button4E.BackColor = Color.Snow Then
            Button4E.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button4E.Text
        ElseIf Button4E.BackColor = Color.LawnGreen Then
            Button4E.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button4F_Click(sender As Object, e As EventArgs) Handles Button4F.Click
        If Button4F.BackColor = Color.Snow Then
            Button4F.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button4F.Text
        ElseIf Button4F.BackColor = Color.LawnGreen Then
            Button4F.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button4G_Click(sender As Object, e As EventArgs) Handles Button4G.Click
        If Button4G.BackColor = Color.Snow Then
            Button4G.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button4G.Text
        ElseIf Button4G.BackColor = Color.LawnGreen Then
            Button4G.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button4H_Click(sender As Object, e As EventArgs) Handles Button4H.Click
        If Button4H.BackColor = Color.Snow Then
            Button4H.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button4H.Text
        ElseIf Button4H.BackColor = Color.LawnGreen Then
            Button4H.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button5A_Click(sender As Object, e As EventArgs) Handles Button5A.Click
        If Button5A.BackColor = Color.Snow Then
            Button5A.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button5A.Text
        ElseIf Button5A.BackColor = Color.LawnGreen Then
            Button5A.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button5B_Click(sender As Object, e As EventArgs) Handles Button5B.Click
        If Button5B.BackColor = Color.Snow Then
            Button5B.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button5B.Text
        ElseIf Button5B.BackColor = Color.LawnGreen Then
            Button5B.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button5C_Click(sender As Object, e As EventArgs) Handles Button5C.Click
        If Button5C.BackColor = Color.Snow Then
            Button5C.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button5C.Text
        ElseIf Button5C.BackColor = Color.LawnGreen Then
            Button5C.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button5D_Click(sender As Object, e As EventArgs) Handles Button5D.Click
        If Button5D.BackColor = Color.Snow Then
            Button5D.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button5D.Text
        ElseIf Button5D.BackColor = Color.LawnGreen Then
            Button5D.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button5E_Click(sender As Object, e As EventArgs) Handles Button5E.Click
        If Button5E.BackColor = Color.Snow Then
            Button5E.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button5E.Text
        ElseIf Button5E.BackColor = Color.LawnGreen Then
            Button5E.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button5F_Click(sender As Object, e As EventArgs) Handles Button5F.Click
        If Button5F.BackColor = Color.Snow Then
            Button5F.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button5F.Text
        ElseIf Button5F.BackColor = Color.LawnGreen Then
            Button5F.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button5G_Click(sender As Object, e As EventArgs) Handles Button5G.Click
        If Button5G.BackColor = Color.Snow Then
            Button5G.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button5G.Text
        ElseIf Button5G.BackColor = Color.LawnGreen Then
            Button5G.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button5H_Click(sender As Object, e As EventArgs) Handles Button5H.Click
        If Button5H.BackColor = Color.Snow Then
            Button5H.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button5H.Text
        ElseIf Button5H.BackColor = Color.LawnGreen Then
            Button5H.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button6A_Click(sender As Object, e As EventArgs) Handles Button6A.Click
        If Button6A.BackColor = Color.Snow Then
            Button6A.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button6A.Text
        ElseIf Button6A.BackColor = Color.LawnGreen Then
            Button6A.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button6B_Click(sender As Object, e As EventArgs) Handles Button6B.Click
        If Button6B.BackColor = Color.Snow Then
            Button6B.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button6B.Text
        ElseIf Button6B.BackColor = Color.LawnGreen Then
            Button6B.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button6C_Click(sender As Object, e As EventArgs) Handles Button6C.Click
        If Button6C.BackColor = Color.Snow Then
            Button6C.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button6C.Text
        ElseIf Button6C.BackColor = Color.LawnGreen Then
            Button6C.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button6D_Click(sender As Object, e As EventArgs) Handles Button6D.Click
        If Button6D.BackColor = Color.Snow Then
            Button6D.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button6D.Text
        ElseIf Button6D.BackColor = Color.LawnGreen Then
            Button6D.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button6E_Click(sender As Object, e As EventArgs) Handles Button6E.Click
        If Button6E.BackColor = Color.Snow Then
            Button6E.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button6E.Text
        ElseIf Button6E.BackColor = Color.LawnGreen Then
            Button6E.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button6F_Click(sender As Object, e As EventArgs) Handles Button6F.Click
        If Button6F.BackColor = Color.Snow Then
            Button6F.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button6F.Text
        ElseIf Button6F.BackColor = Color.LawnGreen Then
            Button6F.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button6G_Click(sender As Object, e As EventArgs) Handles Button6G.Click
        If Button6G.BackColor = Color.Snow Then
            Button6G.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button6G.Text
        ElseIf Button6G.BackColor = Color.LawnGreen Then
            Button6G.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button6H_Click(sender As Object, e As EventArgs) Handles Button6H.Click
        If Button6H.BackColor = Color.Snow Then
            Button6H.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button6H.Text
        ElseIf Button6H.BackColor = Color.LawnGreen Then
            Button6H.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button7A_Click(sender As Object, e As EventArgs) Handles Button7A.Click
        If Button7A.BackColor = Color.Snow Then
            Button7A.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button7A.Text
        ElseIf Button7A.BackColor = Color.LawnGreen Then
            Button7A.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button7B_Click(sender As Object, e As EventArgs) Handles Button7B.Click
        If Button7B.BackColor = Color.Snow Then
            Button7B.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button7B.Text
        ElseIf Button7B.BackColor = Color.LawnGreen Then
            Button7B.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button7C_Click(sender As Object, e As EventArgs) Handles Button7C.Click
        If Button7C.BackColor = Color.Snow Then
            Button7C.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button7C.Text
        ElseIf Button7C.BackColor = Color.LawnGreen Then
            Button7C.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button7D_Click(sender As Object, e As EventArgs) Handles Button7D.Click
        If Button7D.BackColor = Color.Snow Then
            Button7D.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button7D.Text
        ElseIf Button7D.BackColor = Color.LawnGreen Then
            Button7D.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button7E_Click(sender As Object, e As EventArgs) Handles Button7E.Click
        If Button7E.BackColor = Color.Snow Then
            Button7E.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button7E.Text
        ElseIf Button7E.BackColor = Color.LawnGreen Then
            Button7E.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button7F_Click(sender As Object, e As EventArgs) Handles Button7F.Click
        If Button7F.BackColor = Color.Snow Then
            Button7F.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button7F.Text
        ElseIf Button7F.BackColor = Color.LawnGreen Then
            Button7F.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button7G_Click(sender As Object, e As EventArgs) Handles Button7G.Click
        If Button7G.BackColor = Color.Snow Then
            Button7G.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button7G.Text
        ElseIf Button7G.BackColor = Color.LawnGreen Then
            Button7G.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button7H_Click(sender As Object, e As EventArgs) Handles Button7H.Click
        If Button7H.BackColor = Color.Snow Then
            Button7H.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button7H.Text
        ElseIf Button7H.BackColor = Color.LawnGreen Then
            Button7H.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button3E_Click(sender As Object, e As EventArgs) Handles Button3E.Click
        If Button3E.BackColor = Color.Snow Then
            Button3E.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button3E.Text
        ElseIf Button3E.BackColor = Color.LawnGreen Then
            Button3E.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button3F_Click(sender As Object, e As EventArgs) Handles Button3F.Click
        If Button3F.BackColor = Color.Snow Then
            Button3F.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button3F.Text
        ElseIf Button3F.BackColor = Color.LawnGreen Then
            Button3F.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button3G_Click(sender As Object, e As EventArgs) Handles Button3G.Click
        If Button3G.BackColor = Color.Snow Then
            Button3G.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button3G.Text
        ElseIf Button3G.BackColor = Color.LawnGreen Then
            Button3G.BackColor = Color.Snow
        End If
    End Sub

    Private Sub Button3H_Click(sender As Object, e As EventArgs) Handles Button3H.Click
        If Button3H.BackColor = Color.Snow Then
            Button3H.BackColor = Color.LawnGreen

            mainformDEMO.TextBoxSeat.Text = Button3H.Text
        ElseIf Button3H.BackColor = Color.LawnGreen Then
            Button3H.BackColor = Color.Snow
        End If
    End Sub

    Private Sub seat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA

        LabelID.Hide()
    End Sub
End Class