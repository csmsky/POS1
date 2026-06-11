Public Class Qty
    Private Sub Button_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click, ButtonZero.Click
        Dim btn As Button = CType(sender, Button)
        mainform.TextBoxQty.Text &= btn.Text & "" ' append button text separated by space
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        mainform.TextBoxQty.Text = ""
    End Sub
End Class