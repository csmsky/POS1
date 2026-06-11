Imports System.IO

Public Class SplashScreen
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.
    Dim pos = AppConfigReader.ps
    Private Sub SplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'for taking the company logo from resources
        'PictureBoxLogo.Image = WindowsApplication1.My.Resources.Logo
        PictureBoxLogo.Image = WindowsApplication1.My.Resources.VINTA
        LabelPOSno.Text = pos

        'Versioning
        LabelSoftwareName.Text = Application.ProductName
        LabelVersionNo.Text = Application.ProductVersion

        'LabelVersionNo.Text = "v1.4.3"

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'for loading progress bar
        ProgressBar.Increment(2)
        If ProgressBar.Value = 70 Then
            Me.Hide()
            login.Show()
        End If
    End Sub
End Class