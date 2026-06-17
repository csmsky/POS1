Public Class GiftCardRegister
    Private Sub ButtonPrepaid_Click(sender As Object, e As EventArgs) Handles ButtonPrepaid.Click
        ' this will open the Prepaid Form 
        ' PrepaidForm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub ButtonVIP_Click(sender As Object, e As EventArgs) Handles ButtonVIP.Click
        ' Eventually, this will open the VIP Form:
        ' VIPForm.ShowDialog() 
        Me.Close()
    End Sub

End Class