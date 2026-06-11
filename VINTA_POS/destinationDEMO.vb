Imports System.IO.Ports
Public Class destinationDEMO

    'for vfd
    Dim displayHome As Byte() = New Byte(0) {&H1}
    Dim displayClear As Byte() = New Byte(0) {&HC}

    Private Sub dest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'for making dynamic buttons of destination station
        Dim array(2) As String
        array(0) = "Destination 1"
        array(1) = "Destination 2"
        array(2) = "Destination 3"
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
        mainformDEMO.ButtonDestination.Text = btn.Text

        If btn.Text = "Destination 1" Then
            mainformDEMO.DestinationID.Text = "Destination 1"

            mainformDEMO.TextBoxTripFare.Text = "30.00"
            mainformDEMO.LabelFare.Text = "30.00"
        ElseIf btn.Text = "Destination 2" Then
            mainformDEMO.DestinationID.Text = "Destination 2"

            mainformDEMO.TextBoxTripFare.Text = "20.00"
            mainformDEMO.LabelFare.Text = "20.00"
        ElseIf btn.Text = "Destination 3" Then
            mainformDEMO.DestinationID.Text = "Destination 3"

            mainformDEMO.TextBoxTripFare.Text = "20.00"
            mainformDEMO.LabelFare.Text = "20.00"
        End If

        Dim sumpayment As String = Convert.ToDecimal(Val(mainformDEMO.TextBoxTripFare.Text) + Val(mainformDEMO.TextBoxBaggageCost.Text) + Val(mainformDEMO.TextBoxCardAmount.Text)).ToString("0.00")
        mainformDEMO.LabelTotal.Text = sumpayment
        secondscreen.PaymentDue.Text = sumpayment

        Try
            mainformDEMO.LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
            mainformDEMO.LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(mainformDEMO.LabelVATable.Text)), 2))
            '--------- FOR MMDA
            'mainform.LabelVATable.Text = "0.00"
            'mainform.LabelVAT.Text = "0.00"
            '--------- FOR MMDA
            mainformDEMO.LabelVATExempt.Text = "0.00"
            mainformDEMO.LabelDiscount.Text = "0.00"
            'mainform.LabelPercent.Text = "0.00"
            mainformDEMO.LabelZeroRated.Text = "0.00"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'for displaying amount in vfd
        Try
            With mainformDEMO.SerialPort1
                .PortName = "COM2"
                .BaudRate = 9600
                .DataBits = 8
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None
            End With

            If Not (mainformDEMO.SerialPort1.IsOpen = True) Then
                mainformDEMO.SerialPort1.Open()
            End If

            mainformDEMO.SerialPort1.DiscardOutBuffer()

            mainformDEMO.SerialPort1.Write(displayClear, 0, 1)

            mainformDEMO.SerialPort1.Write(displayHome, 0, 1)
            mainformDEMO.SerialPort1.Write("Amount Due:" & vbCrLf & Convert.ToDecimal(mainformDEMO.LabelTotal.Text).ToString("0.00"))
            mainformDEMO.SerialPort1.Close()

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        Me.Close()
    End Sub
End Class