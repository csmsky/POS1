Imports WindowsApplication1.ConfigClass
Public Class originDEMO
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Private Sub origin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'for making dynamic buttons of origin station
        Dim array(2) As String
        array(0) = "Origin 1"
        array(1) = "Origin 2"
        array(2) = "Origin 3"
        For Each element As String In array
            Dim btn As New Button
            AddHandler btn.Click, AddressOf ClickMe
            btn.Width = 130
            btn.Height = 60
            btn.Font = New Font("Microsoft Sans Serif", 14)

            btn.Text = element
            btn.Visible = True
            FlowLayoutPanel1.Controls.Add(btn)
        Next
    End Sub

    Private Sub ClickMe(ByVal sender As Object, e As EventArgs)
        'for assigning button's function
        Dim btn As Button
        btn = CType(sender, Button)
        mainformDEMO.ButtonOrigin.Text = btn.Text

        If btn.Text = "Origin 1" Then
            mainformDEMO.OriginID.Text = "Origin 1"
        ElseIf btn.Text = "Origin 2" Then
            mainformDEMO.OriginID.Text = "Origin 2"
        ElseIf btn.Text = "Origin 3" Then
            mainformDEMO.OriginID.Text = "Origin 3"
        End If
        Me.Close()
    End Sub
End Class