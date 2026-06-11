Imports System.Text
Public Class CardDetails

    ' Collects raw swipe characters until Enter
    Private swipeBuffer As New StringBuilder()

    Private Sub CardDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtTrack1.Hide()
        Label5.Hide()
        Label6.Hide()

        ButtonCardDetails.Enabled = False

        Me.KeyPreview = True

    End Sub

    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        ' End of swipe (usually Enter or Carriage Return)
        If e.KeyChar = ChrW(13) OrElse e.KeyChar = ChrW(10) Then
            Dim rawData As String = swipeBuffer.ToString()
            swipeBuffer.Clear()

            If Not String.IsNullOrWhiteSpace(rawData) Then
                ProcessMagStripe(rawData)
            End If
        Else
            swipeBuffer.Append(e.KeyChar)
        End If
    End Sub

    Private Sub ProcessMagStripe(rawData As String)
        Dim track1 As String = Nothing
        Dim track2 As String = Nothing

        Dim parts = rawData.Split({"?"c}, StringSplitOptions.RemoveEmptyEntries)

        For Each p In parts
            If p.StartsWith("&"c) Then
                track1 = p
            ElseIf p.StartsWith(";"c) Then
                track2 = p
            End If
        Next

        ' ===== TRACK 1 PARSE =====
        If track1 IsNot Nothing Then

            track1 = track1.TrimStart("%"c)

            ' Expected: B<card>^LAST/FIRST^EXP...
            Dim t1 = track1.Split("^"c)

            If t1.Length >= 3 Then
                Dim cardNumber As String = t1(0).Substring(1)   ' remove leading B
                Dim fullName As String = t1(1)
                Dim exp As String = t1(2).Substring(0, 4)       ' YYMM

                txtTrack1.Text = track1
                TextBoxCardNo.Text = MaskCardNumber(cardNumber) ' 🔐 MASKED
                TextBoxCardName.Text = fullName
                TextBoxExpiry.Text = exp
            End If
        End If

        Label5.Text = TextBoxCardNo.Text
        Label6.Text = TextBoxCardName.Text

    End Sub
    Private Function MaskCardNumber(cardNumber As String) As String
        If String.IsNullOrWhiteSpace(cardNumber) Then Return ""

        ' Remove spaces just in case
        Dim cleanCard As String = cardNumber.Replace(" ", "").Trim()

        If cleanCard.Length <= 4 Then
            Return cleanCard
        End If

        Dim last4 As String = cleanCard.Substring(cleanCard.Length - 4)
        Dim masked As String = New String("*"c, cleanCard.Length - 4)

        Return masked & last4
    End Function

    Private Sub ButtonCardDetails_Click(sender As Object, e As EventArgs) Handles ButtonCardDetails.Click

        ApprovedCode.labelCardNumber.Text = Label5.Text
        ApprovedCode.labelCardName.Text = Label6.Text

        Me.Close()
        ApprovedCode.Show()
    End Sub

    Private Sub TextBoxCardNo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCardNo.TextChanged
        If TextBoxCardNo.Text = "" Then

        Else
            ButtonCardDetails.Enabled = True
        End If
    End Sub
End Class