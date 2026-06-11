Imports System.Drawing.Printing
Imports WindowsApplication1.ConfigClass
Public Class baggage
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        If mainform.Visible Then

            Me.Close()
            'Dim sumpayment As String = Convert.ToDecimal(Val(mainform.TextBoxTripFare.Text) + Val(mainform.TextBoxBaggageCost.Text)).ToString("0.00")
            Dim sumpayment As String = Convert.ToDecimal(Val(mainform.TextBoxTripFare.Text)).ToString("0.00")

            mainform.TextBoxMoney.Text = ""
            mainform.TextBoxChange.Text = ""

            mainform.LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
            mainform.LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(mainform.LabelVATable.Text)), 2))
            mainform.LabelVATExempt.Text = "0.00"
            mainform.LabelZeroRated.Text = "0.00"

            '------ FOR MMDA
            'LabelVATable.Text = "0.00"
            'LabelVAT.Text = "0.00"
            'LabelVATExempt.Text = "0.00"
            'LabelZeroRated.Text = "0.00"
            '------ FOR MMDA

            mainform.LabelDiscount1.Text = "0.00"
            mainform.LabelTotal.Text = sumpayment
            secondscreen.PaymentDue.Text = mainform.LabelTotal.Text
            'LabelPercent.Text = "0.00"

        ElseIf mainformDEMO.Visible Then

            If ComboBoxBaggageWeight.Text = "1 - 10 KG" Then
                mainformDEMO.TextBoxBaggageCost.Text = "10.00"
            ElseIf ComboBoxBaggageWeight.Text = "11 - 20 KG" Then
                mainformDEMO.TextBoxBaggageCost.Text = "20.00"
            ElseIf ComboBoxBaggageWeight.Text = "21 - 30 KG" Then
                mainformDEMO.TextBoxBaggageCost.Text = "30.00"
            ElseIf ComboBoxBaggageWeight.Text = "31 - 40 KG" Then
                mainformDEMO.TextBoxBaggageCost.Text = "40.00"
            ElseIf ComboBoxBaggageWeight.Text = "41 - 50 KG" Then
                mainformDEMO.TextBoxBaggageCost.Text = "50.00"
            Else
                MsgBox("Please select 'Baggage Weight' first.", MessageBoxIcon.Warning)
            End If

            Dim sumpayment As String = Convert.ToDecimal(Val(mainformDEMO.TextBoxTripFare.Text) + Val(mainformDEMO.TextBoxBaggageCost.Text)).ToString("0.00")
            mainformDEMO.TextBoxMoney.Text = ""
            mainformDEMO.TextBoxChange.Text = ""

            mainformDEMO.LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
            mainformDEMO.LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(mainformDEMO.LabelVATable.Text)), 2))
            mainformDEMO.LabelVATExempt.Text = "0.00"
            mainformDEMO.LabelZeroRated.Text = "0.00"

            mainformDEMO.LabelDiscount.Text = "0.00"
            mainformDEMO.LabelTotal.Text = sumpayment
            secondscreen.PaymentDue.Text = mainformDEMO.LabelTotal.Text

            Me.Close()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxBaggageWeight.SelectedIndexChanged
        'If ComboBoxBaggageWeight.Text = "1 - 10 KG" Then
        '    TextBoxAmount.Text = "200.00"
        'ElseIf ComboBoxBaggageWeight.Text = "10 - 20 KG" Then
        '    TextBoxAmount.Text = "300.00"
        'ElseIf ComboBoxBaggageWeight.Text = "20 - 30 KG" Then
        '    TextBoxAmount.Text = "400.00"
        'ElseIf ComboBoxBaggageWeight.Text = "30 - 40 KG" Then
        '    TextBoxAmount.Text = "500.00"
        'ElseIf ComboBoxBaggageWeight.Text = "40 - 50 KG" Then
        '    TextBoxAmount.Text = "600.00"
        'Else
        '    MsgBox("Please select 'Baggage Weight' first.", MessageBoxIcon.Warning)
        'End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click

        'mainform.LabelTotal.Text = Convert.ToDecimal(Val(mainform.TextBoxTripFare.Text) + Val(mainform.TextBoxBaggageCost.Text)).ToString("0.00")
        mainform.LabelTotal.Text = Convert.ToDecimal(Val(mainform.TextBoxTripFare.Text)).ToString("0.00")

        mainform.TextBoxMoney.Text = ""
        mainform.TextBoxChange.Text = ""

        'Dim sumpayment As String = Convert.ToDecimal(Val(mainform.LabelFare.Text) + Val(mainform.TextBoxBaggageCost.Text) + Val(mainform.TextBoxCardAmount.Text)).ToString("0.00")
        Dim sumpayment As String = Convert.ToDecimal(Val(mainform.LabelFare.Text)).ToString("0.00")

        mainform.LabelTotal.Text = sumpayment

        'for computing VAT
        Try
            mainform.LabelVATable.Text = (Math.Round(Val((mainform.TextBoxTripFare.Text / "1.12")), 2))
            mainform.LabelVAT.Text = (Math.Round(Val((mainform.TextBoxTripFare.Text) - Val(mainform.LabelVATable.Text)), 2))
            '--------- FOR MMDA
            'mainform.LabelVATable.Text = "0.00"
            'mainform.LabelVAT.Text = "0.00"
            '--------- FOR MMDA
            mainform.LabelVATExempt.Text = "0.00"
            mainform.LabelDiscount1.Text = "0.00"
            'mainform.LabelPercent.Text = "0.00"
            mainform.LabelZeroRated.Text = "0.00"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.Close()
    End Sub
End Class