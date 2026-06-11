Imports System.Data
Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.IO.Ports
Imports WindowsApplication1.ConfigClass

Public Class mainformDEMO
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    'Dim strConn As String = FDEandD()
    'Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps
    Dim station = AppConfigReader.sttn
    Dim serial = AppConfigReader.srl

    Dim barcode As BusinessRefinery.Barcode.PDF417 = New BusinessRefinery.Barcode.PDF417()
    Dim barcodeticket As BusinessRefinery.Barcode.PDF417 = New BusinessRefinery.Barcode.PDF417()
    'for printing multiple pages
    Dim page_number As Byte

    'for VFD Reader
    Dim displayHome As Byte() = New Byte(0) {&H1}
    Dim displayClear As Byte() = New Byte(0) {&HC}
    Dim displayNewLine As Byte() = New Byte(0) {&HD}
    Dim displayLineFd As Byte() = New Byte(0) {&HA}

    'for NFC Card
    Private _pcscReader As PscsReader
    Private _cardPolling As CardPolling
    Dim apdu As Apdu = New Apdu
    Dim command() As Byte
    Dim combo

    Dim todaysdate As String = Today.ToString("yyyy-MM-dd")

    Dim cost As String

    Public Sub OnThreadException(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        MessageBox.Show(e.Exception.Message)
    End Sub

    Private Sub cardPolling_OnError(ByVal sender As Object, ByVal e As CardPollingErrorEventArg)
        MessageBox.Show(e.errorMessage)
    End Sub

    Private Sub cardPolling_OnCardRemoved(ByVal sender As Object, ByVal e As CardPollingEventArg)
        'for clearing fields when card is removed
        'for clearing fields for another transaction
        Passenger_idTextBox.Text = ""
        lname.Text = ""
        fname.Text = ""
        gender.Text = ""
        profession.Text = ""
        idtype.Text = ""
        cardno.Text = ""
        PictureBoxFace.Hide()
        LabelFare.Text = Nothing
        TextBoxTripFare.Text = "0.00"
        TextBoxBaggageCost.Text = "0.00"
        TextBoxCardAmount.Text = "0.00"
        TextBoxChange.Clear()
        TextBoxMoney.Clear()
        TextBoxSeat.Clear()
        ButtonOrigin.Text = "Origin"
        ButtonDestination.Text = "Destination"
        ButtonStudent.BackColor = Color.Gainsboro
        ButtonPWD.BackColor = Color.Gainsboro
        ButtonSenior.BackColor = Color.Gainsboro
        ButtonZeroRated.BackColor = Color.Gainsboro
        ComboBoxTime.Text = Nothing
        LabelType.Text = "Regular"
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True
        Button100.Enabled = True
        Button200.Enabled = True
        Button500.Enabled = True
        Button1000.Enabled = True
        ButtonDot.Enabled = True
        Button0.Enabled = True
        Button00.Enabled = True
        ButtonDel.Enabled = True
        ButtonClear.Enabled = True
        ButtonOk.Enabled = True

        ButtonBaggage.Enabled = True
    End Sub

    Private Sub cardPolling_OnCardFound(ByVal sender As Object, ByVal e As CardPollingEventArg)
        Try
            Dim data() As Byte
            Dim indx As Integer
            Dim tmpStr As String

            'card establish context
            _pcscReader.establishContext()

            'card connect 
            _pcscReader.connect(ComboReaderNames.Text)

            'card begin transaction
            _pcscReader.beginTransaction()

            'set status LED to green
            command = Helper.getBytes("FF 00 40 20 04 01 01 01 01", " ")

            If command.Length > 5 Then
                apdu.setCommand(command.Skip(0).Take(5).ToArray)
                apdu.data = New Byte(command(4) - 1) {}
                Array.Copy(command, 5, apdu.data, 0, command.Length - 5)

            Else
                apdu.setCommand(command)

                If command(4) <> 0 Then
                    apdu.data = Nothing
                    apdu.lengthExpected = command(4)

                End If
            End If

            _pcscReader.sendCommand(apdu)

            'load authentication key
            command = Helper.getBytes("FF 82 00 00 06 FF FF FF FF FF FF", " ")

            If command.Length > 5 Then
                apdu.setCommand(command.Skip(0).Take(5).ToArray)
                apdu.data = New Byte(command(4) - 1) {}
                Array.Copy(command, 5, apdu.data, 0, command.Length - 5)

            Else
                apdu.setCommand(command)

                If command(4) <> 0 Then
                    apdu.data = Nothing
                    apdu.lengthExpected = command(4)

                End If
            End If

            _pcscReader.sendCommand(apdu)

            'authenticate block 01 
            command = Helper.getBytes("FF 86 00 00 05 01 00 01 60 00", " ")

            If command.Length > 5 Then
                apdu.setCommand(command.Skip(0).Take(5).ToArray)
                apdu.data = New Byte(command(4) - 1) {}
                Array.Copy(command, 5, apdu.data, 0, command.Length - 5)

            Else
                apdu.setCommand(command)

                If command(4) <> 0 Then
                    apdu.data = Nothing
                    apdu.lengthExpected = command(4)

                End If
            End If

            _pcscReader.sendCommand(apdu)

            'read block 01 
            command = Helper.getBytes("FF B0 00 01 10", " ")

            If command.Length > 5 Then
                apdu.setCommand(command.Skip(0).Take(5).ToArray)
                apdu.data = New Byte(command(4) - 1) {}
                Array.Copy(command, 5, apdu.data, 0, command.Length - 5)

            Else
                apdu.setCommand(command)

                If command(4) <> 0 Then
                    apdu.data = Nothing
                    apdu.lengthExpected = command(4)

                End If
            End If

            _pcscReader.sendCommand(apdu)

            'get response
            data = apdu.response.Take(16).ToArray

            tmpStr = ""

            For indx = 0 To data.Length - 1
                tmpStr = tmpStr + Chr(data(indx))
            Next indx

            Passenger_idTextBox.Text = tmpStr

            'card end transaction
            _pcscReader.endTransaction() 'dff

        Catch ex As Exception
        End Try
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBoxMoney.AppendText("9")
    End Sub

    Private Sub Button100_Click(sender As Object, e As EventArgs) Handles Button100.Click
        TextBoxMoney.AppendText("100")
    End Sub

    Private Sub destination_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxUserID.Hide()

        'for making an E-journal Folder
        My.Computer.FileSystem.CreateDirectory("C:\E-Journal")

        'for taking the company logo from resources
        'PictureBoxCompanyLogo.Image = WindowsApplication1.My.Resources.Logo
        PictureBoxLogoReceipt.Image = WindowsApplication1.My.Resources.LogoReceipt
        PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA
        LabelPOSno.Text = pos
        LabelStationID.Text = station
        LabelSerial.Text = serial

        'for disabling discount button when there is no manager access
        'ButtonReprintTicket.Enabled = False
        'ButtonVoid.Enabled = False
        'ButtonStudent.Enabled = False
        'ButtonPWDSenior.Enabled = False
        'ButtonZeroRated.Enabled = False

        'Software Versioning
        LabelSoftwareName.Text = Application.ProductName
        LabelVersionNo.Text = Application.ProductVersion

        'Hides important datas that is used
        PictureBoxFace.Hide()
        LabelOR.Hide()
        LabelTicket.Hide()
        TextBoxPercentDiscount.Enabled = False
        LabelSerial.Hide()
        LabelLoginID.Hide()
        LabelCashierID.Hide()
        LabelFare.Hide()
        LabelVAT.Hide()
        LabelVATExempt.Hide()
        LabelVATable.Hide()
        LabelDiscount.Hide()
        LabelZeroRated.Hide()
        LabelType.Hide()
        Timer1.Enabled = True
        ComboReaderNames.Hide()
        OriginID.Hide()
        DestinationID.Hide()
        TextBoxBarcode.Hide()
        PictureBoxBarcode.Hide()
        TextBoxBarcodeTicket.Hide()
        PictureBoxBarcodeTicket.Hide()
        PictureBoxLogoReceipt.Hide()

        ''PRINT dim margins something
        Dim margins As New Margins(0, 0, 0, 0)
        PrintDocument2.DefaultPageSettings.Margins = margins

        ''THERMAL
        Dim papersize As New PaperSize("Custom", 280, 1000)
        '' ------------- For MMDA
        'Dim papersize As New PaperSize("Custom", 280, 470)

        ''DOT MATRIX
        'Dim papersize As New PaperSize("Custom", 255, 670)

        PrintDocument2.DefaultPageSettings.PaperSize = papersize


        Dim readerList() As String

        Try
            _pcscReader = New PscsReader
            _cardPolling = New CardPolling

            'register to event on card found
            AddHandler _cardPolling.OnCardFound, AddressOf cardPolling_OnCardFound

            'register to event on card remove
            AddHandler _cardPolling.OnCardRemoved, AddressOf cardPolling_OnCardRemoved

            'register to event on error
            AddHandler _cardPolling.OnError, AddressOf cardPolling_OnError

            _cardPolling.StopPolling()

            'get all smart card reader connected to computer
            readerList = _pcscReader.getReaderList

            'ComboReaderNames.Items.Clear() //Experiment

            If readerList.Length > 0 Then
                ComboReaderNames.Items.AddRange(readerList)

                ComboReaderNames.SelectedIndex = 0

                'Get the reader name for contactless (picc) and contact reader
                For i As Integer = 0 To ComboReaderNames.Items.Count - 1
                    _cardPolling.add(ComboReaderNames.Items(i).ToString)
                Next

                ' ButtonListReaders.Enabled = False

            Else
                MessageBox.Show("No readers found.", "List Readers", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch pcscException As PscsException
            MessageBox.Show("PCSC Exception: " + pcscException.Message)

        Catch generalException As Exception
            MessageBox.Show(generalException.Message)

        End Try

        '---start pooling
        If _cardPolling Is Nothing Then
            Return
        End If

        If _cardPolling.isBusy Then
            _cardPolling.StopPolling()
        Else
            _cardPolling.start()
        End If

        ComboReaderNames.Enabled = False
        Me.Refresh()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        LabelDateTime.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBoxMoney.AppendText("1")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBoxMoney.AppendText("2")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBoxMoney.AppendText("3")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBoxMoney.AppendText("4")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBoxMoney.AppendText("5")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBoxMoney.AppendText("6")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBoxMoney.AppendText("7")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TextBoxMoney.AppendText("8")
    End Sub

    Private Sub ButtonDot_Click(sender As Object, e As EventArgs) Handles ButtonDot.Click
        TextBoxMoney.AppendText(".")
    End Sub

    Private Sub Button0_Click(sender As Object, e As EventArgs) Handles Button0.Click
        TextBoxMoney.AppendText("0")
    End Sub

    Private Sub Button200_Click(sender As Object, e As EventArgs) Handles Button200.Click
        TextBoxMoney.AppendText("200")
    End Sub

    Private Sub Button500_Click(sender As Object, e As EventArgs) Handles Button500.Click
        TextBoxMoney.AppendText("500")
    End Sub

    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        TextBoxMoney.Text = ""
        TextBoxChange.Clear()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If TextBoxTripFare.Text = Nothing Or
            TextBoxMoney.Text = Nothing Then
            MsgBox("Input payment/money.", MessageBoxIcon.Warning)
        Else

            'for computing change
            'TextBoxChange.Text = Convert.ToDecimal(Val(TextBoxMoney.Text) - (Val(TextBoxTripFare.Text).ToString("0.00") + Val(TextBoxBaggageCost.Text).ToString("0.00") + Val(TextBoxCardAmount.Text).ToString("0.00")))
            If ButtonZeroRated.BackColor = Color.Gainsboro Then
                TextBoxChange.Text = Convert.ToDecimal(Val(TextBoxMoney.Text) - Val(LabelTotal.Text)).ToString("0.00")
                secondscreen.Change.Text = TextBoxChange.Text

            ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                TextBoxChange.Text = Convert.ToDecimal(Val(TextBoxMoney.Text) - "0.00").ToString("0.00")
                secondscreen.Change.Text = TextBoxChange.Text
            End If
        End If
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        'for deleting one character at a time
        If Len(TextBoxMoney.Text) > 1 Then
            TextBoxMoney.Text = Mid(TextBoxMoney.Text, 1, Len(TextBoxMoney.Text) - 1)
        Else
            TextBoxMoney.Text = ""
        End If
        TextBoxChange.Clear()
    End Sub

    Private Sub Button1000_Click(sender As Object, e As EventArgs) Handles Button1000.Click
        TextBoxMoney.AppendText("1000")
    End Sub

    Private Sub Button00_Click(sender As Object, e As EventArgs) Handles Button00.Click
        TextBoxMoney.AppendText("00")
    End Sub

    Private Sub ButtonIssueTicket_Click(sender As Object, e As EventArgs) Handles ButtonIssueTicket.Click
        Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")
        '============================== CARD ===================================================
        If ButtonCard.BackColor = Color.Gray Then

            If Passenger_idTextBox.Text = "" Or
                fname.Text = "" Or
                lname.Text = "" Or
                LabelTotal.Text = "" Or
                TextBoxMoney.Text = "" Or
                TextBoxChange.Text = "" Then

                MsgBox("Can't Issue Ticket. Please make sure all fields are filled up.", MessageBoxIcon.Error)

            Else

                'for calling auto (payment id)
                auto()

                'for dual printing
                If (page_number > 1) Then page_number = 0

                ''PRINT set print dialog something
                ''Set print dialog
                'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                'PrintPreviewDialog1.Document = PrintDocument1

                'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                'PrintPreviewDialog1.ShowDialog()

                'TO PRINT IMMEDIATELY
                PrintDocument2.Print()


                If seatDEMO.Button1A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1A.BackColor = Color.Red
                    seatDEMO.Button1A.Enabled = False
                End If
                If seatDEMO.Button1B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1B.BackColor = Color.Red
                    seatDEMO.Button1B.Enabled = False
                End If
                If seatDEMO.Button1C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1C.BackColor = Color.Red
                    seatDEMO.Button1C.Enabled = False
                End If
                If seatDEMO.Button1D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1D.BackColor = Color.Red
                    seatDEMO.Button1D.Enabled = False
                End If
                If seatDEMO.Button1E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1E.BackColor = Color.Red
                    seatDEMO.Button1E.Enabled = False
                End If
                If seatDEMO.Button1F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1F.BackColor = Color.Red
                    seatDEMO.Button1F.Enabled = False
                End If
                If seatDEMO.Button1G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1G.BackColor = Color.Red
                    seatDEMO.Button1G.Enabled = False
                End If
                If seatDEMO.Button1H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1H.BackColor = Color.Red
                    seatDEMO.Button1H.Enabled = False
                End If

                If seatDEMO.Button2A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2A.BackColor = Color.Red
                    seatDEMO.Button2A.Enabled = False
                End If
                If seatDEMO.Button2B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2B.BackColor = Color.Red
                    seatDEMO.Button2B.Enabled = False
                End If
                If seatDEMO.Button2C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2C.BackColor = Color.Red
                    seatDEMO.Button2C.Enabled = False
                End If
                If seatDEMO.Button2D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2D.BackColor = Color.Red
                    seatDEMO.Button2D.Enabled = False
                End If
                If seatDEMO.Button2E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2E.BackColor = Color.Red
                    seatDEMO.Button2E.Enabled = False
                End If
                If seatDEMO.Button2F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2F.BackColor = Color.Red
                    seatDEMO.Button2F.Enabled = False
                End If
                If seatDEMO.Button2G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2G.BackColor = Color.Red
                    seatDEMO.Button2G.Enabled = False
                End If
                If seatDEMO.Button2H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2H.BackColor = Color.Red
                    seatDEMO.Button2H.Enabled = False
                End If

                If seatDEMO.Button3A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3A.BackColor = Color.Red
                    seatDEMO.Button3A.Enabled = False
                End If
                If seatDEMO.Button3B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3B.BackColor = Color.Red
                    seatDEMO.Button3B.Enabled = False
                End If
                If seatDEMO.Button3C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3C.BackColor = Color.Red
                    seatDEMO.Button3C.Enabled = False
                End If
                If seatDEMO.Button3D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3D.BackColor = Color.Red
                    seatDEMO.Button3D.Enabled = False
                End If
                If seatDEMO.Button3E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3E.BackColor = Color.Red
                    seatDEMO.Button3E.Enabled = False
                End If
                If seatDEMO.Button3F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3F.BackColor = Color.Red
                    seatDEMO.Button3F.Enabled = False
                End If
                If seatDEMO.Button3G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3G.BackColor = Color.Red
                    seatDEMO.Button3G.Enabled = False
                End If
                If seatDEMO.Button3H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3H.BackColor = Color.Red
                    seatDEMO.Button3H.Enabled = False
                End If

                If seatDEMO.Button4A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4A.BackColor = Color.Red
                    seatDEMO.Button4A.Enabled = False
                End If
                If seatDEMO.Button4B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4B.BackColor = Color.Red
                    seatDEMO.Button4B.Enabled = False
                End If
                If seatDEMO.Button4C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4C.BackColor = Color.Red
                    seatDEMO.Button4C.Enabled = False
                End If
                If seatDEMO.Button4D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4D.BackColor = Color.Red
                    seatDEMO.Button4D.Enabled = False
                End If
                If seatDEMO.Button4E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4E.BackColor = Color.Red
                    seatDEMO.Button4E.Enabled = False
                End If
                If seatDEMO.Button4F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4F.BackColor = Color.Red
                    seatDEMO.Button4F.Enabled = False
                End If
                If seatDEMO.Button4G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4G.BackColor = Color.Red
                    seatDEMO.Button4G.Enabled = False
                End If
                If seatDEMO.Button4H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4H.BackColor = Color.Red
                    seatDEMO.Button4H.Enabled = False
                End If

                If seatDEMO.Button5A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5A.BackColor = Color.Red
                    seatDEMO.Button5A.Enabled = False
                End If
                If seatDEMO.Button5B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5B.BackColor = Color.Red
                    seatDEMO.Button5B.Enabled = False
                End If
                If seatDEMO.Button5C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5C.BackColor = Color.Red
                    seatDEMO.Button5C.Enabled = False
                End If
                If seatDEMO.Button5D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5D.BackColor = Color.Red
                    seatDEMO.Button5D.Enabled = False
                End If
                If seatDEMO.Button5E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5E.BackColor = Color.Red
                    seatDEMO.Button5E.Enabled = False
                End If
                If seatDEMO.Button5F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5F.BackColor = Color.Red
                    seatDEMO.Button5F.Enabled = False
                End If
                If seatDEMO.Button5G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5G.BackColor = Color.Red
                    seatDEMO.Button5G.Enabled = False
                End If
                If seatDEMO.Button5H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5H.BackColor = Color.Red
                    seatDEMO.Button5H.Enabled = False
                End If

                If seatDEMO.Button6A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6A.BackColor = Color.Red
                    seatDEMO.Button6A.Enabled = False
                End If
                If seatDEMO.Button6B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6B.BackColor = Color.Red
                    seatDEMO.Button6B.Enabled = False
                End If
                If seatDEMO.Button6C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6C.BackColor = Color.Red
                    seatDEMO.Button6C.Enabled = False
                End If
                If seatDEMO.Button6D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6D.BackColor = Color.Red
                    seatDEMO.Button6D.Enabled = False
                End If
                If seatDEMO.Button6E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6E.BackColor = Color.Red
                    seatDEMO.Button6E.Enabled = False
                End If
                If seatDEMO.Button6F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6F.BackColor = Color.Red
                    seatDEMO.Button6F.Enabled = False
                End If
                If seatDEMO.Button6G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6G.BackColor = Color.Red
                    seatDEMO.Button6G.Enabled = False
                End If
                If seatDEMO.Button6H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6H.BackColor = Color.Red
                    seatDEMO.Button6H.Enabled = False
                End If

                If seatDEMO.Button7A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7A.BackColor = Color.Red
                    seatDEMO.Button7A.Enabled = False
                End If
                If seatDEMO.Button7B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7B.BackColor = Color.Red
                    seatDEMO.Button7B.Enabled = False
                End If
                If seatDEMO.Button7C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7C.BackColor = Color.Red
                    seatDEMO.Button7C.Enabled = False
                End If
                If seatDEMO.Button7D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7D.BackColor = Color.Red
                    seatDEMO.Button7D.Enabled = False
                End If
                If seatDEMO.Button7E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7E.BackColor = Color.Red
                    seatDEMO.Button7E.Enabled = False
                End If
                If seatDEMO.Button7F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7F.BackColor = Color.Red
                    seatDEMO.Button7F.Enabled = False
                End If
                If seatDEMO.Button7G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7G.BackColor = Color.Red
                    seatDEMO.Button7G.Enabled = False
                End If
                If seatDEMO.Button7H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7H.BackColor = Color.Red
                    seatDEMO.Button7H.Enabled = False
                End If

            End If

            '============================== CARD ===================================================


            '============================== EMPLOYEE ===================================================

            'for employee
        ElseIf ButtonEmployee.BackColor = Color.Gray Then
            If Passenger_idTextBox.Text = "" Or
                    fname.Text = "" Or
                    lname.Text = "" Or
                    TextBoxSeat.Text = "" Or
                    ComboBoxTime.Text = "" Then

                MsgBox("Can't Issue Ticket. Please make sure all fields are filled up.", MessageBoxIcon.Error)

            Else

                'for calling auto (payment id)
                auto()

                'for dual printing
                If (page_number > 1) Then page_number = 0

                ''PRINT set print dialog something
                ''Set print dialog
                'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                'PrintPreviewDialog1.Document = PrintDocument1

                'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                'PrintPreviewDialog1.ShowDialog()

                'TO PRINT IMMEDIATELY
                PrintDocument2.Print()


                If seatDEMO.Button1A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1A.BackColor = Color.Red
                    seatDEMO.Button1A.Enabled = False
                End If
                If seatDEMO.Button1B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1B.BackColor = Color.Red
                    seatDEMO.Button1B.Enabled = False
                End If
                If seatDEMO.Button1C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1C.BackColor = Color.Red
                    seatDEMO.Button1C.Enabled = False
                End If
                If seatDEMO.Button1D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1D.BackColor = Color.Red
                    seatDEMO.Button1D.Enabled = False
                End If
                If seatDEMO.Button1E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1E.BackColor = Color.Red
                    seatDEMO.Button1E.Enabled = False
                End If
                If seatDEMO.Button1F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1F.BackColor = Color.Red
                    seatDEMO.Button1F.Enabled = False
                End If
                If seatDEMO.Button1G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1G.BackColor = Color.Red
                    seatDEMO.Button1G.Enabled = False
                End If
                If seatDEMO.Button1H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1H.BackColor = Color.Red
                    seatDEMO.Button1H.Enabled = False
                End If

                If seatDEMO.Button2A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2A.BackColor = Color.Red
                    seatDEMO.Button2A.Enabled = False
                End If
                If seatDEMO.Button2B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2B.BackColor = Color.Red
                    seatDEMO.Button2B.Enabled = False
                End If
                If seatDEMO.Button2C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2C.BackColor = Color.Red
                    seatDEMO.Button2C.Enabled = False
                End If
                If seatDEMO.Button2D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2D.BackColor = Color.Red
                    seatDEMO.Button2D.Enabled = False
                End If
                If seatDEMO.Button2E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2E.BackColor = Color.Red
                    seatDEMO.Button2E.Enabled = False
                End If
                If seatDEMO.Button2F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2F.BackColor = Color.Red
                    seatDEMO.Button2F.Enabled = False
                End If
                If seatDEMO.Button2G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2G.BackColor = Color.Red
                    seatDEMO.Button2G.Enabled = False
                End If
                If seatDEMO.Button2H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2H.BackColor = Color.Red
                    seatDEMO.Button2H.Enabled = False
                End If

                If seatDEMO.Button3A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3A.BackColor = Color.Red
                    seatDEMO.Button3A.Enabled = False
                End If
                If seatDEMO.Button3B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3B.BackColor = Color.Red
                    seatDEMO.Button3B.Enabled = False
                End If
                If seatDEMO.Button3C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3C.BackColor = Color.Red
                    seatDEMO.Button3C.Enabled = False
                End If
                If seatDEMO.Button3D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3D.BackColor = Color.Red
                    seatDEMO.Button3D.Enabled = False
                End If
                If seatDEMO.Button3E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3E.BackColor = Color.Red
                    seatDEMO.Button3E.Enabled = False
                End If
                If seatDEMO.Button3F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3F.BackColor = Color.Red
                    seatDEMO.Button3F.Enabled = False
                End If
                If seatDEMO.Button3G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3G.BackColor = Color.Red
                    seatDEMO.Button3G.Enabled = False
                End If
                If seatDEMO.Button3H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3H.BackColor = Color.Red
                    seatDEMO.Button3H.Enabled = False
                End If

                If seatDEMO.Button4A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4A.BackColor = Color.Red
                    seatDEMO.Button4A.Enabled = False
                End If
                If seatDEMO.Button4B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4B.BackColor = Color.Red
                    seatDEMO.Button4B.Enabled = False
                End If
                If seatDEMO.Button4C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4C.BackColor = Color.Red
                    seatDEMO.Button4C.Enabled = False
                End If
                If seatDEMO.Button4D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4D.BackColor = Color.Red
                    seatDEMO.Button4D.Enabled = False
                End If
                If seatDEMO.Button4E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4E.BackColor = Color.Red
                    seatDEMO.Button4E.Enabled = False
                End If
                If seatDEMO.Button4F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4F.BackColor = Color.Red
                    seatDEMO.Button4F.Enabled = False
                End If
                If seatDEMO.Button4G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4G.BackColor = Color.Red
                    seatDEMO.Button4G.Enabled = False
                End If
                If seatDEMO.Button4H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4H.BackColor = Color.Red
                    seatDEMO.Button4H.Enabled = False
                End If

                If seatDEMO.Button5A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5A.BackColor = Color.Red
                    seatDEMO.Button5A.Enabled = False
                End If
                If seatDEMO.Button5B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5B.BackColor = Color.Red
                    seatDEMO.Button5B.Enabled = False
                End If
                If seatDEMO.Button5C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5C.BackColor = Color.Red
                    seatDEMO.Button5C.Enabled = False
                End If
                If seatDEMO.Button5D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5D.BackColor = Color.Red
                    seatDEMO.Button5D.Enabled = False
                End If
                If seatDEMO.Button5E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5E.BackColor = Color.Red
                    seatDEMO.Button5E.Enabled = False
                End If
                If seatDEMO.Button5F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5F.BackColor = Color.Red
                    seatDEMO.Button5F.Enabled = False
                End If
                If seatDEMO.Button5G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5G.BackColor = Color.Red
                    seatDEMO.Button5G.Enabled = False
                End If
                If seatDEMO.Button5H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5H.BackColor = Color.Red
                    seatDEMO.Button5H.Enabled = False
                End If

                If seatDEMO.Button6A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6A.BackColor = Color.Red
                    seatDEMO.Button6A.Enabled = False
                End If
                If seatDEMO.Button6B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6B.BackColor = Color.Red
                    seatDEMO.Button6B.Enabled = False
                End If
                If seatDEMO.Button6C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6C.BackColor = Color.Red
                    seatDEMO.Button6C.Enabled = False
                End If
                If seatDEMO.Button6D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6D.BackColor = Color.Red
                    seatDEMO.Button6D.Enabled = False
                End If
                If seatDEMO.Button6E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6E.BackColor = Color.Red
                    seatDEMO.Button6E.Enabled = False
                End If
                If seatDEMO.Button6F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6F.BackColor = Color.Red
                    seatDEMO.Button6F.Enabled = False
                End If
                If seatDEMO.Button6G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6G.BackColor = Color.Red
                    seatDEMO.Button6G.Enabled = False
                End If
                If seatDEMO.Button6H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6H.BackColor = Color.Red
                    seatDEMO.Button6H.Enabled = False
                End If

                If seatDEMO.Button7A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7A.BackColor = Color.Red
                    seatDEMO.Button7A.Enabled = False
                End If
                If seatDEMO.Button7B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7B.BackColor = Color.Red
                    seatDEMO.Button7B.Enabled = False
                End If
                If seatDEMO.Button7C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7C.BackColor = Color.Red
                    seatDEMO.Button7C.Enabled = False
                End If
                If seatDEMO.Button7D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7D.BackColor = Color.Red
                    seatDEMO.Button7D.Enabled = False
                End If
                If seatDEMO.Button7E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7E.BackColor = Color.Red
                    seatDEMO.Button7E.Enabled = False
                End If
                If seatDEMO.Button7F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7F.BackColor = Color.Red
                    seatDEMO.Button7F.Enabled = False
                End If
                If seatDEMO.Button7G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7G.BackColor = Color.Red
                    seatDEMO.Button7G.Enabled = False
                End If
                If seatDEMO.Button7H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7H.BackColor = Color.Red
                    seatDEMO.Button7H.Enabled = False
                End If
            End If

            '============================== EMPLOYEE ===================================================

            '============================== OR ===================================================

        ElseIf ButtonCard.BackColor = Color.Gainsboro And
                ButtonEmployee.BackColor = Color.Gainsboro Then

            If Passenger_idTextBox.Text = "" Or
                        fname.Text = "" Or
                        lname.Text = "" Or
                        TextBoxTripFare.Text = "" Or
                        TextBoxSeat.Text = "" Or
                        ComboBoxTime.Text = "" Or
                        TextBoxMoney.Text = "" Or
                        TextBoxChange.Text = "" Then

                MsgBox("Can't Issue Ticket. Please make sure all fields are filled up.", MessageBoxIcon.Error)
            Else
                'for calling auto (payment id)
                auto()

                'for dual printing
                If (page_number > 1) Then page_number = 0

                ''PRINT set print dialog something
                ''Set print dialog
                'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                'PrintPreviewDialog1.Document = PrintDocument1

                'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                'PrintPreviewDialog1.ShowDialog()

                'TO PRINT IMMEDIATELY
                PrintDocument2.Print()


                If seatDEMO.Button1A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1A.BackColor = Color.Red
                    seatDEMO.Button1A.Enabled = False
                End If
                If seatDEMO.Button1B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1B.BackColor = Color.Red
                    seatDEMO.Button1B.Enabled = False
                End If
                If seatDEMO.Button1C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1C.BackColor = Color.Red
                    seatDEMO.Button1C.Enabled = False
                End If
                If seatDEMO.Button1D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1D.BackColor = Color.Red
                    seatDEMO.Button1D.Enabled = False
                End If
                If seatDEMO.Button1E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1E.BackColor = Color.Red
                    seatDEMO.Button1E.Enabled = False
                End If
                If seatDEMO.Button1F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1F.BackColor = Color.Red
                    seatDEMO.Button1F.Enabled = False
                End If
                If seatDEMO.Button1G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1G.BackColor = Color.Red
                    seatDEMO.Button1G.Enabled = False
                End If
                If seatDEMO.Button1H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button1H.BackColor = Color.Red
                    seatDEMO.Button1H.Enabled = False
                End If

                If seatDEMO.Button2A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2A.BackColor = Color.Red
                    seatDEMO.Button2A.Enabled = False
                End If
                If seatDEMO.Button2B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2B.BackColor = Color.Red
                    seatDEMO.Button2B.Enabled = False
                End If
                If seatDEMO.Button2C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2C.BackColor = Color.Red
                    seatDEMO.Button2C.Enabled = False
                End If
                If seatDEMO.Button2D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2D.BackColor = Color.Red
                    seatDEMO.Button2D.Enabled = False
                End If
                If seatDEMO.Button2E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2E.BackColor = Color.Red
                    seatDEMO.Button2E.Enabled = False
                End If
                If seatDEMO.Button2F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2F.BackColor = Color.Red
                    seatDEMO.Button2F.Enabled = False
                End If
                If seatDEMO.Button2G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2G.BackColor = Color.Red
                    seatDEMO.Button2G.Enabled = False
                End If
                If seatDEMO.Button2H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button2H.BackColor = Color.Red
                    seatDEMO.Button2H.Enabled = False
                End If

                If seatDEMO.Button3A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3A.BackColor = Color.Red
                    seatDEMO.Button3A.Enabled = False
                End If
                If seatDEMO.Button3B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3B.BackColor = Color.Red
                    seatDEMO.Button3B.Enabled = False
                End If
                If seatDEMO.Button3C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3C.BackColor = Color.Red
                    seatDEMO.Button3C.Enabled = False
                End If
                If seatDEMO.Button3D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3D.BackColor = Color.Red
                    seatDEMO.Button3D.Enabled = False
                End If
                If seatDEMO.Button3E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3E.BackColor = Color.Red
                    seatDEMO.Button3E.Enabled = False
                End If
                If seatDEMO.Button3F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3F.BackColor = Color.Red
                    seatDEMO.Button3F.Enabled = False
                End If
                If seatDEMO.Button3G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3G.BackColor = Color.Red
                    seatDEMO.Button3G.Enabled = False
                End If
                If seatDEMO.Button3H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button3H.BackColor = Color.Red
                    seatDEMO.Button3H.Enabled = False
                End If

                If seatDEMO.Button4A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4A.BackColor = Color.Red
                    seatDEMO.Button4A.Enabled = False
                End If
                If seatDEMO.Button4B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4B.BackColor = Color.Red
                    seatDEMO.Button4B.Enabled = False
                End If
                If seatDEMO.Button4C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4C.BackColor = Color.Red
                    seatDEMO.Button4C.Enabled = False
                End If
                If seatDEMO.Button4D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4D.BackColor = Color.Red
                    seatDEMO.Button4D.Enabled = False
                End If
                If seatDEMO.Button4E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4E.BackColor = Color.Red
                    seatDEMO.Button4E.Enabled = False
                End If
                If seatDEMO.Button4F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4F.BackColor = Color.Red
                    seatDEMO.Button4F.Enabled = False
                End If
                If seatDEMO.Button4G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4G.BackColor = Color.Red
                    seatDEMO.Button4G.Enabled = False
                End If
                If seatDEMO.Button4H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button4H.BackColor = Color.Red
                    seatDEMO.Button4H.Enabled = False
                End If

                If seatDEMO.Button5A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5A.BackColor = Color.Red
                    seatDEMO.Button5A.Enabled = False
                End If
                If seatDEMO.Button5B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5B.BackColor = Color.Red
                    seatDEMO.Button5B.Enabled = False
                End If
                If seatDEMO.Button5C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5C.BackColor = Color.Red
                    seatDEMO.Button5C.Enabled = False
                End If
                If seatDEMO.Button5D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5D.BackColor = Color.Red
                    seatDEMO.Button5D.Enabled = False
                End If
                If seatDEMO.Button5E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5E.BackColor = Color.Red
                    seatDEMO.Button5E.Enabled = False
                End If
                If seatDEMO.Button5F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5F.BackColor = Color.Red
                    seatDEMO.Button5F.Enabled = False
                End If
                If seatDEMO.Button5G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5G.BackColor = Color.Red
                    seatDEMO.Button5G.Enabled = False
                End If
                If seatDEMO.Button5H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button5H.BackColor = Color.Red
                    seatDEMO.Button5H.Enabled = False
                End If

                If seatDEMO.Button6A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6A.BackColor = Color.Red
                    seatDEMO.Button6A.Enabled = False
                End If
                If seatDEMO.Button6B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6B.BackColor = Color.Red
                    seatDEMO.Button6B.Enabled = False
                End If
                If seatDEMO.Button6C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6C.BackColor = Color.Red
                    seatDEMO.Button6C.Enabled = False
                End If
                If seatDEMO.Button6D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6D.BackColor = Color.Red
                    seatDEMO.Button6D.Enabled = False
                End If
                If seatDEMO.Button6E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6E.BackColor = Color.Red
                    seatDEMO.Button6E.Enabled = False
                End If
                If seatDEMO.Button6F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6F.BackColor = Color.Red
                    seatDEMO.Button6F.Enabled = False
                End If
                If seatDEMO.Button6G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6G.BackColor = Color.Red
                    seatDEMO.Button6G.Enabled = False
                End If
                If seatDEMO.Button6H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button6H.BackColor = Color.Red
                    seatDEMO.Button6H.Enabled = False
                End If

                If seatDEMO.Button7A.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7A.BackColor = Color.Red
                    seatDEMO.Button7A.Enabled = False
                End If
                If seatDEMO.Button7B.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7B.BackColor = Color.Red
                    seatDEMO.Button7B.Enabled = False
                End If
                If seatDEMO.Button7C.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7C.BackColor = Color.Red
                    seatDEMO.Button7C.Enabled = False
                End If
                If seatDEMO.Button7D.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7D.BackColor = Color.Red
                    seatDEMO.Button7D.Enabled = False
                End If
                If seatDEMO.Button7E.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7E.BackColor = Color.Red
                    seatDEMO.Button7E.Enabled = False
                End If
                If seatDEMO.Button7F.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7F.BackColor = Color.Red
                    seatDEMO.Button7F.Enabled = False
                End If
                If seatDEMO.Button7G.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7G.BackColor = Color.Red
                    seatDEMO.Button7G.Enabled = False
                End If
                If seatDEMO.Button7H.BackColor = Color.LawnGreen Then
                    seatDEMO.Button7H.BackColor = Color.Red
                    seatDEMO.Button7H.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub auto()
        TextBoxBarcode.Text = LabelOR.Text
        TextBoxBarcodeTicket.Text = LabelTicket.Text
    End Sub

    Private Sub PrintDocument2_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        Try

            Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")
            'text font style for receipt
            Dim TextToPrint As String = ""
            Dim N As New Font("Merchant Copy", 13)
            Dim H As New Font("Merchant Copy", 15)
            Dim B As New Font("Merchant Copy", 15, FontStyle.Bold)

            Dim sngCenterPage As Single

            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Far


            ''UNCOMMENT
            If ButtonEmployee.BackColor = Color.Gainsboro Then

                If ButtonCard.BackColor = Color.Gainsboro Then

                    combo = ComboBoxTime.Text.Split("-")

                    Dim comb1 = combo(1).ToString
                    Dim comb = combo(0).ToString

                    If ButtonPWD.BackColor = Color.Gray Or ButtonSenior.BackColor = Color.Gray Then

                        ''for THERMAL
                        Select Case page_number

                            Case 0

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BOARDING AND ALIGHTING PASS", B).Width / 2)
                                e.Graphics.DrawString("BOARDING AND ALIGHTING PASS", B, Brushes.Black, sngCenterPage, 2)

                                e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 25)
                                e.Graphics.DrawString("TICKET #: " & TextBoxBarcodeTicket.Text, B, Brushes.Black, New RectangleF(4, 40, 0, 0))

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 60)

                                barcodeticket.Resolution = 155

                                barcodeticket.Format = System.Drawing.Imaging.ImageFormat.Gif
                                PictureBoxBarcodeTicket.Image = barcodeticket.drawBarcodeOnBitmap()

                                e.Graphics.DrawImage(PictureBoxBarcodeTicket.Image, 6, 70)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcodeTicket.Text, N).Width / 2)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 115)

                                e.Graphics.DrawString("VESSEL ID" & ControlChars.NewLine & "SCHEDULE" & ControlChars.NewLine & "SEAT NO." & ControlChars.NewLine & "PT. OF ORIGIN" & ControlChars.NewLine & "PT. OF DESTINATION", N, Brushes.Black, New RectangleF(4, 130, 0, 0))
                                e.Graphics.DrawString(comb & ControlChars.NewLine & comb1 & ControlChars.NewLine & TextBoxSeat.Text & ControlChars.NewLine & ButtonOrigin.Text & ControlChars.NewLine & ButtonDestination.Text, N, Brushes.Black, New RectangleF(100, 130, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("-- This is a sample ticket only --", N).Width / 2)
                                e.Graphics.DrawString("-- This is a sample ticket only --", N, Brushes.Black, sngCenterPage, 200)

                                sf.Dispose()
                                e.HasMorePages = True


                            Case 1

                                ''for centering company logo
                                'get the printer resolution
                                'Dim thisDpiX As Integer = e.Graphics.DpiX
                                'Dim r As Single = thisDpiX / 96
                                'Dim targetwidth As Integer = 235 * r

                                ''get the size of the margins in pixels
                                ''convert from inches to pixels
                                ''your printer and windows may be set on units other than inches
                                ''so you need to look at the values using debug
                                'Dim marginwidthpixels = e.MarginBounds.Width * thisDpiX / 100
                                ''determine the center of the margins which is half the margin size from the left margin
                                'Dim c As Single = (marginwidthpixels / 2) + (e.MarginBounds.X * thisDpiX / 100)
                                ''determine the left edge of the image from the center
                                ''subtract half the drawing size from the center in pixels
                                'Dim l As Integer = c - (targetwidth / 2)
                                ''determine the top of the margins in pixels
                                'Dim t As Integer = e.MarginBounds.Y * thisDpiX / 100

                                TextToPrint &= Environment.NewLine
                                e.Graphics.DrawImage(PictureBoxLogoReceipt.Image, 92, 0)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("CAVITE SUPERFERRY", N).Width / 2)
                                e.Graphics.DrawString("CAVITE SUPERFERRY", N, Brushes.Black, sngCenterPage, 70)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PINOY CATAMARAN", N).Width / 2)
                                e.Graphics.DrawString("PINOY CATAMARAN", N, Brushes.Black, sngCenterPage, 82)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("#17 SILAHIS STREET,", N).Width / 2)
                                e.Graphics.DrawString("#17 SILAHIS STREET,", N, Brushes.Black, sngCenterPage, 94)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BARANGAY TANZA, NAVOTAS CITY", N).Width / 2)
                                e.Graphics.DrawString("BARANGAY TANZA, NAVOTAS CITY", N, Brushes.Black, sngCenterPage, 106)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PHILIPPINES", N).Width / 2)
                                e.Graphics.DrawString("PHILIPPINES", N, Brushes.Black, sngCenterPage, 118)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 130)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N).Width / 2)
                                e.Graphics.DrawString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N, Brushes.Black, sngCenterPage, 142)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("MIN: XXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("MIN: XXXXXXXXXX", N, Brushes.Black, sngCenterPage, 154)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 165)

                                e.Graphics.DrawString("Cashier: " & LabelCashierName.Text & ControlChars.NewLine & "Station ID: " & OriginID.Text, N, Brushes.Black, 4, 175)
                                e.Graphics.DrawString("Customer Copy", N, Brushes.Black, 4, 200)
                                e.Graphics.DrawString("TRAINING MODE", N, Brushes.Black, 160, 200)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 210)

                                e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 220)
                                e.Graphics.DrawString("OR #: " & TextBoxBarcode.Text, B, Brushes.Black, New RectangleF(4, 232, 0, 0))
                                e.Graphics.DrawString("PHP", B, Brushes.Black, New RectangleF(80, 232, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 241)

                                e.Graphics.DrawString("NAME" & ControlChars.NewLine &
                                              "VESSEL ID" & ControlChars.NewLine &
                                              "SCHEDULE" & ControlChars.NewLine &
                                              "SEAT NO." & ControlChars.NewLine &
                                              "PT. OF ORIGIN" & ControlChars.NewLine &
                                              "PT. OF DESTINATION" & ControlChars.NewLine &
                                              "TRIP FARE" & ControlChars.NewLine &
                                              "BAGGAGE COST" & ControlChars.NewLine &
                                              "CARD AMOUNT", N, Brushes.Black, New RectangleF(4, 250, 0, 0))
                                e.Graphics.DrawString(fname.Text & " " & lname.Text & ControlChars.NewLine &
                                              comb & ControlChars.NewLine &
                                              comb1 & ControlChars.NewLine &
                                              TextBoxSeat.Text & ControlChars.NewLine &
                                              ButtonOrigin.Text & ControlChars.NewLine &
                                              ButtonDestination.Text & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              TextBoxBaggageCost.Text & ControlChars.NewLine &
                                              TextBoxCardAmount.Text, N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 358)

                                barcode.Resolution = 155

                                barcode.Format = System.Drawing.Imaging.ImageFormat.Gif
                                PictureBoxBarcode.Image = barcode.drawBarcodeOnBitmap()

                                e.Graphics.DrawImage(PictureBoxBarcode.Image, 6, 367)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcode.Text, N).Width / 2)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 411)

                                e.Graphics.DrawString("TOTAL AMOUNT" & ControlChars.NewLine &
                                                      "CASH" & ControlChars.NewLine &
                                                      "CHANGE", N, Brushes.Black, New RectangleF(4, 420, 0, 0))
                                e.Graphics.DrawString(LabelTotal.Text & ControlChars.NewLine &
                                                      Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")) & ControlChars.NewLine &
                                                      TextBoxChange.Text, N, Brushes.Black, New RectangleF(80, 420, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 456)

                                e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine & "VAT Amount" & ControlChars.NewLine & "(X) VAT-Exempt Sales" & ControlChars.NewLine & "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                                e.Graphics.DrawString(LabelVATable.Text & ControlChars.NewLine & LabelVAT.Text & ControlChars.NewLine & LabelVATExempt.Text & ControlChars.NewLine & LabelZeroRated.Text & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 517)

                                e.Graphics.DrawString("Discount", N, Brushes.Black, New RectangleF(4, 528, 0, 0))
                                e.Graphics.DrawString(LabelDiscount.Text & ControlChars.NewLine & LabelType.Text, N, Brushes.Black, New RectangleF(80, 528, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 547)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS IS NOT AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS IS NOT AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 568)

                                e.Graphics.DrawString("Name: _________________________________" & ControlChars.NewLine &
                                          "Address: ______________________________" & ControlChars.NewLine &
                                          "_______________________________________" & ControlChars.NewLine &
                                          "ID #: _________________________________" & ControlChars.NewLine &
                                          "OSCA/SC ID #: _________________________" & ControlChars.NewLine &
                                          "PWD ID #: _____________________________" & ControlChars.NewLine &
                                          "TIN #: ________________________________" & ControlChars.NewLine &
                                          "Bus Style: ____________________________" & ControlChars.NewLine &
                                          "Signature: ____________________________", N, Brushes.Black, New RectangleF(4, 578, 0, 0))

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 690)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VINTATECH", N).Width / 2)
                                e.Graphics.DrawString("VINTATECH", N, Brushes.Black, sngCenterPage, 700)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Unit 507 F&L Bldg. 2211", N).Width / 2)
                                e.Graphics.DrawString("Unit 507 F&L Bldg. 2211", N, Brushes.Black, sngCenterPage, 712)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Commonwealth, Holy Spirit 1127", N).Width / 2)
                                e.Graphics.DrawString("Commonwealth, Holy Spirit 1127", N, Brushes.Black, sngCenterPage, 724)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Quezon City", N).Width / 2)
                                e.Graphics.DrawString("Quezon City", N, Brushes.Black, sngCenterPage, 736)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 748)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Accred. No.: XXXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("Accred. No.: XXXXXXXXXXX", N, Brushes.Black, sngCenterPage, 760)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Date Issued: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 772)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 784)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PTU No.: XXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("PTU No.: XXXXXXXXX", N, Brushes.Black, sngCenterPage, 796)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 808)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 820)

                                sf.Dispose()
                                e.HasMorePages = True

                            Case 2

                                ''for centering company logo
                                'get the printer resolution
                                'Dim thisDpiX As Integer = e.Graphics.DpiX
                                'Dim r As Single = thisDpiX / 96
                                'Dim targetwidth As Integer = 235 * r

                                ''get the size of the margins in pixels
                                ''convert from inches to pixels
                                ''your printer and windows may be set on units other than inches
                                ''so you need to look at the values using debug
                                'Dim marginwidthpixels = e.MarginBounds.Width * thisDpiX / 100
                                ''determine the center of the margins which is half the margin size from the left margin
                                'Dim c As Single = (marginwidthpixels / 2) + (e.MarginBounds.X * thisDpiX / 100)
                                ''determine the left edge of the image from the center
                                ''subtract half the drawing size from the center in pixels
                                'Dim l As Integer = c - (targetwidth / 2)
                                ''determine the top of the margins in pixels
                                'Dim t As Integer = e.MarginBounds.Y * thisDpiX / 100

                                TextToPrint &= Environment.NewLine
                                e.Graphics.DrawImage(PictureBoxLogoReceipt.Image, 92, 0)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("CAVITE SUPERFERRY", N).Width / 2)
                                e.Graphics.DrawString("CAVITE SUPERFERRY", N, Brushes.Black, sngCenterPage, 70)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PINOY CATAMARAN", N).Width / 2)
                                e.Graphics.DrawString("PINOY CATAMARAN", N, Brushes.Black, sngCenterPage, 82)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("#17 SILAHIS STREET,", N).Width / 2)
                                e.Graphics.DrawString("#17 SILAHIS STREET,", N, Brushes.Black, sngCenterPage, 94)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BARANGAY TANZA, NAVOTAS CITY", N).Width / 2)
                                e.Graphics.DrawString("BARANGAY TANZA, NAVOTAS CITY", N, Brushes.Black, sngCenterPage, 106)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PHILIPPINES", N).Width / 2)
                                e.Graphics.DrawString("PHILIPPINES", N, Brushes.Black, sngCenterPage, 118)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 130)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N).Width / 2)
                                e.Graphics.DrawString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N, Brushes.Black, sngCenterPage, 142)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("MIN: XXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("MIN: XXXXXXXXXX", N, Brushes.Black, sngCenterPage, 154)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 165)

                                e.Graphics.DrawString("Cashier: " & LabelCashierName.Text & ControlChars.NewLine & "Station ID: " & OriginID.Text, N, Brushes.Black, 4, 175)
                                e.Graphics.DrawString("Store Copy", N, Brushes.Black, 4, 200)
                                e.Graphics.DrawString("TRAINING MODE", N, Brushes.Black, 160, 200)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 210)

                                e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 220)
                                e.Graphics.DrawString("OR #: " & TextBoxBarcode.Text, B, Brushes.Black, New RectangleF(4, 232, 0, 0))
                                e.Graphics.DrawString("PHP", B, Brushes.Black, New RectangleF(80, 232, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 241)

                                e.Graphics.DrawString("NAME" & ControlChars.NewLine &
                                              "VESSEL ID" & ControlChars.NewLine &
                                              "SCHEDULE" & ControlChars.NewLine &
                                              "SEAT NO." & ControlChars.NewLine &
                                              "PT. OF ORIGIN" & ControlChars.NewLine &
                                              "PT. OF DESTINATION" & ControlChars.NewLine &
                                              "TRIP FARE" & ControlChars.NewLine &
                                              "BAGGAGE COST" & ControlChars.NewLine &
                                              "CARD AMOUNT", N, Brushes.Black, New RectangleF(4, 250, 0, 0))
                                e.Graphics.DrawString(fname.Text & " " & lname.Text & ControlChars.NewLine &
                                              comb & ControlChars.NewLine &
                                              comb1 & ControlChars.NewLine &
                                              TextBoxSeat.Text & ControlChars.NewLine &
                                              ButtonOrigin.Text & ControlChars.NewLine &
                                              ButtonDestination.Text & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              TextBoxBaggageCost.Text & ControlChars.NewLine &
                                              TextBoxCardAmount.Text, N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 358)

                                barcode.Resolution = 155

                                barcode.Format = System.Drawing.Imaging.ImageFormat.Gif
                                PictureBoxBarcode.Image = barcode.drawBarcodeOnBitmap()

                                e.Graphics.DrawImage(PictureBoxBarcode.Image, 6, 367)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcode.Text, N).Width / 2)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 411)

                                e.Graphics.DrawString("TOTAL AMOUNT" & ControlChars.NewLine & "CASH" & ControlChars.NewLine & "CHANGE", N, Brushes.Black, New RectangleF(4, 420, 0, 0))
                                e.Graphics.DrawString(LabelTotal.Text & ControlChars.NewLine & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")) & ControlChars.NewLine & TextBoxChange.Text, N, Brushes.Black, New RectangleF(80, 420, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 456)

                                e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine & "VAT Amount" & ControlChars.NewLine & "(X) VAT-Exempt Sales" & ControlChars.NewLine & "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                                e.Graphics.DrawString(LabelVATable.Text & ControlChars.NewLine & LabelVAT.Text & ControlChars.NewLine & LabelVATExempt.Text & ControlChars.NewLine & LabelZeroRated.Text & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 517)

                                e.Graphics.DrawString("Discount", N, Brushes.Black, New RectangleF(4, 528, 0, 0))
                                e.Graphics.DrawString(LabelDiscount.Text & ControlChars.NewLine & LabelType.Text, N, Brushes.Black, New RectangleF(80, 528, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 547)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS IS NOT AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS IS NOT AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 568)

                                e.Graphics.DrawString("Name: _________________________________" & ControlChars.NewLine &
                                          "Address: ______________________________" & ControlChars.NewLine &
                                          "_______________________________________" & ControlChars.NewLine &
                                          "ID #: _________________________________" & ControlChars.NewLine &
                                          "OSCA/SC ID #: _________________________" & ControlChars.NewLine &
                                          "PWD ID #: _____________________________" & ControlChars.NewLine &
                                          "TIN #: ________________________________" & ControlChars.NewLine &
                                          "Bus Style: ____________________________" & ControlChars.NewLine &
                                          "Signature: ____________________________", N, Brushes.Black, New RectangleF(4, 578, 0, 0))

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 690)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VINTATECH", N).Width / 2)
                                e.Graphics.DrawString("VINTATECH", N, Brushes.Black, sngCenterPage, 700)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Unit 507 F&L Bldg. 2211", N).Width / 2)
                                e.Graphics.DrawString("Unit 507 F&L Bldg. 2211", N, Brushes.Black, sngCenterPage, 712)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Commonwealth, Holy Spirit 1127", N).Width / 2)
                                e.Graphics.DrawString("Commonwealth, Holy Spirit 1127", N, Brushes.Black, sngCenterPage, 724)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Quezon City", N).Width / 2)
                                e.Graphics.DrawString("Quezon City", N, Brushes.Black, sngCenterPage, 736)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 748)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Accred. No.: XXXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("Accred. No.: XXXXXXXXXXX", N, Brushes.Black, sngCenterPage, 760)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Date Issued: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 772)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 784)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PTU No.: XXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("PTU No.: XXXXXXXXX", N, Brushes.Black, sngCenterPage, 796)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 808)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 820)

                                e.HasMorePages = False
                        End Select
                        page_number += 1


                        'Regular Transaction
                    Else

                        Select Case page_number
                            Case 0

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BOARDING AND ALIGHTING PASS", B).Width / 2)
                                e.Graphics.DrawString("BOARDING AND ALIGHTING PASS", B, Brushes.Black, sngCenterPage, 2)

                                e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 25)
                                e.Graphics.DrawString("TICKET #: " & TextBoxBarcodeTicket.Text, B, Brushes.Black, New RectangleF(4, 40, 0, 0))

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 60)

                                barcodeticket.Resolution = 155

                                barcodeticket.Format = System.Drawing.Imaging.ImageFormat.Gif
                                PictureBoxBarcodeTicket.Image = barcodeticket.drawBarcodeOnBitmap()

                                e.Graphics.DrawImage(PictureBoxBarcodeTicket.Image, 6, 70)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcodeTicket.Text, N).Width / 2)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 115)

                                e.Graphics.DrawString("VESSEL ID" & ControlChars.NewLine & "SCHEDULE" & ControlChars.NewLine & "SEAT NO." & ControlChars.NewLine & "PT. OF ORIGIN" & ControlChars.NewLine & "PT. OF DESTINATION", N, Brushes.Black, New RectangleF(4, 130, 0, 0))
                                e.Graphics.DrawString(comb & ControlChars.NewLine & comb1 & ControlChars.NewLine & TextBoxSeat.Text & ControlChars.NewLine & ButtonOrigin.Text & ControlChars.NewLine & ButtonDestination.Text, N, Brushes.Black, New RectangleF(100, 130, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("-- This is a sample ticket only --", N).Width / 2)
                                e.Graphics.DrawString("-- This is a sample ticket only --", N, Brushes.Black, sngCenterPage, 200)

                                sf.Dispose()
                                e.HasMorePages = True

                            Case 1

                                TextToPrint &= Environment.NewLine
                                e.Graphics.DrawImage(PictureBoxLogoReceipt.Image, 92, 0)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("CAVITE SUPERFERRY", N).Width / 2)
                                e.Graphics.DrawString("CAVITE SUPERFERRY", N, Brushes.Black, sngCenterPage, 70)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PINOY CATAMARAN", N).Width / 2)
                                e.Graphics.DrawString("PINOY CATAMARAN", N, Brushes.Black, sngCenterPage, 82)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("#17 SILAHIS STREET,", N).Width / 2)
                                e.Graphics.DrawString("#17 SILAHIS STREET,", N, Brushes.Black, sngCenterPage, 94)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BARANGAY TANZA, NAVOTAS CITY", N).Width / 2)
                                e.Graphics.DrawString("BARANGAY TANZA, NAVOTAS CITY", N, Brushes.Black, sngCenterPage, 106)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PHILIPPINES", N).Width / 2)
                                e.Graphics.DrawString("PHILIPPINES", N, Brushes.Black, sngCenterPage, 118)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 130)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N).Width / 2)
                                e.Graphics.DrawString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N, Brushes.Black, sngCenterPage, 142)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("MIN: XXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("MIN: XXXXXXXXXX", N, Brushes.Black, sngCenterPage, 154)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 165)

                                e.Graphics.DrawString("Cashier: " & LabelCashierName.Text & ControlChars.NewLine & "Station ID: " & OriginID.Text, N, Brushes.Black, 4, 175)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("TRAINING MODE", N).Width / 2)
                                e.Graphics.DrawString("TRAINING MODE", N, Brushes.Black, sngCenterPage, 200)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 210)

                                e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 220)
                                e.Graphics.DrawString("OR #: " & TextBoxBarcode.Text, B, Brushes.Black, New RectangleF(4, 232, 0, 0))
                                e.Graphics.DrawString("PHP", B, Brushes.Black, New RectangleF(80, 232, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 241)

                                e.Graphics.DrawString("NAME" & ControlChars.NewLine &
                                              "VESSEL ID" & ControlChars.NewLine &
                                              "SCHEDULE" & ControlChars.NewLine &
                                              "SEAT NO." & ControlChars.NewLine &
                                              "PT. OF ORIGIN" & ControlChars.NewLine &
                                              "PT. OF DESTINATION" & ControlChars.NewLine &
                                              "TRIP FARE" & ControlChars.NewLine &
                                              "BAGGAGE COST" & ControlChars.NewLine &
                                              "CARD AMOUNT", N, Brushes.Black, New RectangleF(4, 250, 0, 0))
                                e.Graphics.DrawString(fname.Text & " " & lname.Text & ControlChars.NewLine &
                                              comb & ControlChars.NewLine &
                                              comb1 & ControlChars.NewLine &
                                              TextBoxSeat.Text & ControlChars.NewLine &
                                              ButtonOrigin.Text & ControlChars.NewLine &
                                              ButtonDestination.Text & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              TextBoxBaggageCost.Text & ControlChars.NewLine &
                                              TextBoxCardAmount.Text, N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 358)

                                barcode.Resolution = 155

                                barcode.Format = System.Drawing.Imaging.ImageFormat.Gif
                                PictureBoxBarcode.Image = barcode.drawBarcodeOnBitmap()

                                e.Graphics.DrawImage(PictureBoxBarcode.Image, 6, 367)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcode.Text, N).Width / 2)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 411)

                                e.Graphics.DrawString("TOTAL AMOUNT" & ControlChars.NewLine & "CASH" & ControlChars.NewLine & "CHANGE", N, Brushes.Black, New RectangleF(4, 420, 0, 0))
                                e.Graphics.DrawString(LabelTotal.Text & ControlChars.NewLine & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")) & ControlChars.NewLine & TextBoxChange.Text, N, Brushes.Black, New RectangleF(80, 420, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 456)

                                If ButtonZeroRated.BackColor = Color.Gainsboro Then
                                    e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine & "VAT Amount" & ControlChars.NewLine & "(X) VAT-Exempt Sales" & ControlChars.NewLine & "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                                    e.Graphics.DrawString(LabelVATable.Text & ControlChars.NewLine & LabelVAT.Text & ControlChars.NewLine & LabelVATExempt.Text & ControlChars.NewLine & LabelZeroRated.Text & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

                                ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                                    e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine & "VAT Amount" & ControlChars.NewLine & "(X) VAT-Exempt Sales" & ControlChars.NewLine & "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                                    e.Graphics.DrawString(LabelVATable.Text & ControlChars.NewLine & LabelVAT.Text & ControlChars.NewLine & LabelVATExempt.Text & ControlChars.NewLine & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

                                End If

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 517)

                                e.Graphics.DrawString("Discount", N, Brushes.Black, New RectangleF(4, 528, 0, 0))
                                e.Graphics.DrawString(LabelDiscount.Text & ControlChars.NewLine & LabelType.Text, N, Brushes.Black, New RectangleF(80, 528, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 547)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS IS NOT AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS IS NOT AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 568)

                                e.Graphics.DrawString("Name: _________________________________" & ControlChars.NewLine &
                                          "Address: ______________________________" & ControlChars.NewLine &
                                          "_______________________________________" & ControlChars.NewLine &
                                          "ID #: _________________________________" & ControlChars.NewLine &
                                          "OSCA/SC ID #: _________________________" & ControlChars.NewLine &
                                          "PWD ID #: _____________________________" & ControlChars.NewLine &
                                          "TIN #: ________________________________" & ControlChars.NewLine &
                                          "Bus Style: ____________________________" & ControlChars.NewLine &
                                          "Signature: ____________________________", N, Brushes.Black, New RectangleF(4, 578, 0, 0))

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 690)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VINTATECH", N).Width / 2)
                                e.Graphics.DrawString("VINTATECH", N, Brushes.Black, sngCenterPage, 700)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Unit 507 F&L Bldg. 2211", N).Width / 2)
                                e.Graphics.DrawString("Unit 507 F&L Bldg. 2211", N, Brushes.Black, sngCenterPage, 712)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Commonwealth, Holy Spirit 1127", N).Width / 2)
                                e.Graphics.DrawString("Commonwealth, Holy Spirit 1127", N, Brushes.Black, sngCenterPage, 724)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Quezon City", N).Width / 2)
                                e.Graphics.DrawString("Quezon City", N, Brushes.Black, sngCenterPage, 736)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 748)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Accred. No.: XXXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("Accred. No.: XXXXXXXXXXX", N, Brushes.Black, sngCenterPage, 760)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Date Issued: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 772)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 784)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PTU No.: XXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("PTU No.: XXXXXXXXX", N, Brushes.Black, sngCenterPage, 796)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 808)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 820)

                                e.HasMorePages = False
                        End Select
                        page_number += 1
                        ''End of THERMAL
                    End If

                ElseIf ButtonCard.BackColor = Color.Gray Then

                    If ButtonPWD.BackColor = Color.Gray Or ButtonSenior.BackColor = Color.Gray Then

                        ''for THERMAL
                        Select Case page_number

                            Case 0

                                ''for centering company logo
                                'get the printer resolution
                                'Dim thisDpiX As Integer = e.Graphics.DpiX
                                'Dim r As Single = thisDpiX / 96
                                'Dim targetwidth As Integer = 235 * r

                                ''get the size of the margins in pixels
                                ''convert from inches to pixels
                                ''your printer and windows may be set on units other than inches
                                ''so you need to look at the values using debug
                                'Dim marginwidthpixels = e.MarginBounds.Width * thisDpiX / 100
                                ''determine the center of the margins which is half the margin size from the left margin
                                'Dim c As Single = (marginwidthpixels / 2) + (e.MarginBounds.X * thisDpiX / 100)
                                ''determine the left edge of the image from the center
                                ''subtract half the drawing size from the center in pixels
                                'Dim l As Integer = c - (targetwidth / 2)
                                ''determine the top of the margins in pixels
                                'Dim t As Integer = e.MarginBounds.Y * thisDpiX / 100

                                TextToPrint &= Environment.NewLine
                                e.Graphics.DrawImage(PictureBoxLogoReceipt.Image, 92, 0)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("CAVITE SUPERFERRY", N).Width / 2)
                                e.Graphics.DrawString("CAVITE SUPERFERRY", N, Brushes.Black, sngCenterPage, 70)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PINOY CATAMARAN", N).Width / 2)
                                e.Graphics.DrawString("PINOY CATAMARAN", N, Brushes.Black, sngCenterPage, 82)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("#17 SILAHIS STREET,", N).Width / 2)
                                e.Graphics.DrawString("#17 SILAHIS STREET,", N, Brushes.Black, sngCenterPage, 94)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BARANGAY TANZA, NAVOTAS CITY", N).Width / 2)
                                e.Graphics.DrawString("BARANGAY TANZA, NAVOTAS CITY", N, Brushes.Black, sngCenterPage, 106)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PHILIPPINES", N).Width / 2)
                                e.Graphics.DrawString("PHILIPPINES", N, Brushes.Black, sngCenterPage, 118)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 130)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N).Width / 2)
                                e.Graphics.DrawString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N, Brushes.Black, sngCenterPage, 142)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("MIN: XXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("MIN: XXXXXXXXXX", N, Brushes.Black, sngCenterPage, 154)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 165)

                                e.Graphics.DrawString("Cashier: " & LabelCashierName.Text & ControlChars.NewLine & "Station ID: " & OriginID.Text, N, Brushes.Black, 4, 175)
                                e.Graphics.DrawString("Customer Copy", N, Brushes.Black, 4, 200)
                                e.Graphics.DrawString("TRAINING MODE", N, Brushes.Black, 160, 200)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 210)

                                e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 220)
                                e.Graphics.DrawString("OR #: " & TextBoxBarcode.Text, B, Brushes.Black, New RectangleF(4, 232, 0, 0))
                                e.Graphics.DrawString("PHP", B, Brushes.Black, New RectangleF(80, 232, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 241)

                                e.Graphics.DrawString("NAME" & ControlChars.NewLine &
                                              "VESSEL ID" & ControlChars.NewLine &
                                              "SCHEDULE" & ControlChars.NewLine &
                                              "SEAT NO." & ControlChars.NewLine &
                                              "PT. OF ORIGIN" & ControlChars.NewLine &
                                              "PT. OF DESTINATION" & ControlChars.NewLine &
                                              "TRIP FARE" & ControlChars.NewLine &
                                              "BAGGAGE COST" & ControlChars.NewLine &
                                              "CARD AMOUNT", N, Brushes.Black, New RectangleF(4, 250, 0, 0))
                                e.Graphics.DrawString(fname.Text & " " & lname.Text & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              TextBoxSeat.Text & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              TextBoxBaggageCost.Text & ControlChars.NewLine &
                                              TextBoxCardAmount.Text, N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 358)

                                barcode.Resolution = 155

                                barcode.Format = System.Drawing.Imaging.ImageFormat.Gif
                                PictureBoxBarcode.Image = barcode.drawBarcodeOnBitmap()

                                e.Graphics.DrawImage(PictureBoxBarcode.Image, 6, 367)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcode.Text, N).Width / 2)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 411)

                                e.Graphics.DrawString("TOTAL AMOUNT" & ControlChars.NewLine & "CASH" & ControlChars.NewLine & "CHANGE", N, Brushes.Black, New RectangleF(4, 420, 0, 0))
                                e.Graphics.DrawString(LabelTotal.Text & ControlChars.NewLine & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")) & ControlChars.NewLine & TextBoxChange.Text, N, Brushes.Black, New RectangleF(80, 420, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 456)

                                e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine & "VAT Amount" & ControlChars.NewLine & "(X) VAT-Exempt Sales" & ControlChars.NewLine & "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                                e.Graphics.DrawString(LabelVATable.Text & ControlChars.NewLine & LabelVAT.Text & ControlChars.NewLine & LabelVATExempt.Text & ControlChars.NewLine & LabelZeroRated.Text & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 517)

                                e.Graphics.DrawString("Discount", N, Brushes.Black, New RectangleF(4, 528, 0, 0))
                                e.Graphics.DrawString(LabelDiscount.Text & ControlChars.NewLine & LabelType.Text, N, Brushes.Black, New RectangleF(80, 528, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 547)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS IS NOT AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS IS NOT AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 568)

                                e.Graphics.DrawString("Name: _________________________________" & ControlChars.NewLine &
                                          "Address: ______________________________" & ControlChars.NewLine &
                                          "_______________________________________" & ControlChars.NewLine &
                                          "ID #: _________________________________" & ControlChars.NewLine &
                                          "OSCA/SC ID #: _________________________" & ControlChars.NewLine &
                                          "PWD ID #: _____________________________" & ControlChars.NewLine &
                                          "TIN #: ________________________________" & ControlChars.NewLine &
                                          "Bus Style: ____________________________" & ControlChars.NewLine &
                                          "Signature: ____________________________", N, Brushes.Black, New RectangleF(4, 578, 0, 0))

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 690)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VINTATECH", N).Width / 2)
                                e.Graphics.DrawString("VINTATECH", N, Brushes.Black, sngCenterPage, 700)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Unit 507 F&L Bldg. 2211", N).Width / 2)
                                e.Graphics.DrawString("Unit 507 F&L Bldg. 2211", N, Brushes.Black, sngCenterPage, 712)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Commonwealth, Holy Spirit 1127", N).Width / 2)
                                e.Graphics.DrawString("Commonwealth, Holy Spirit 1127", N, Brushes.Black, sngCenterPage, 724)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Quezon City", N).Width / 2)
                                e.Graphics.DrawString("Quezon City", N, Brushes.Black, sngCenterPage, 736)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 748)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Accred. No.: XXXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("Accred. No.: XXXXXXXXXXX", N, Brushes.Black, sngCenterPage, 760)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Date Issued: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 772)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 784)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PTU No.: XXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("PTU No.: XXXXXXXXX", N, Brushes.Black, sngCenterPage, 796)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 808)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 820)

                                sf.Dispose()
                                e.HasMorePages = True

                            Case 1

                                ''for centering company logo
                                'get the printer resolution
                                'Dim thisDpiX As Integer = e.Graphics.DpiX
                                'Dim r As Single = thisDpiX / 96
                                'Dim targetwidth As Integer = 235 * r

                                ''get the size of the margins in pixels
                                ''convert from inches to pixels
                                ''your printer and windows may be set on units other than inches
                                ''so you need to look at the values using debug
                                'Dim marginwidthpixels = e.MarginBounds.Width * thisDpiX / 100
                                ''determine the center of the margins which is half the margin size from the left margin
                                'Dim c As Single = (marginwidthpixels / 2) + (e.MarginBounds.X * thisDpiX / 100)
                                ''determine the left edge of the image from the center
                                ''subtract half the drawing size from the center in pixels
                                'Dim l As Integer = c - (targetwidth / 2)
                                ''determine the top of the margins in pixels
                                'Dim t As Integer = e.MarginBounds.Y * thisDpiX / 100

                                TextToPrint &= Environment.NewLine
                                e.Graphics.DrawImage(PictureBoxLogoReceipt.Image, 92, 0)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("CAVITE SUPERFERRY", N).Width / 2)
                                e.Graphics.DrawString("CAVITE SUPERFERRY", N, Brushes.Black, sngCenterPage, 70)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PINOY CATAMARAN", N).Width / 2)
                                e.Graphics.DrawString("PINOY CATAMARAN", N, Brushes.Black, sngCenterPage, 82)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("#17 SILAHIS STREET,", N).Width / 2)
                                e.Graphics.DrawString("#17 SILAHIS STREET,", N, Brushes.Black, sngCenterPage, 94)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BARANGAY TANZA, NAVOTAS CITY", N).Width / 2)
                                e.Graphics.DrawString("BARANGAY TANZA, NAVOTAS CITY", N, Brushes.Black, sngCenterPage, 106)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PHILIPPINES", N).Width / 2)
                                e.Graphics.DrawString("PHILIPPINES", N, Brushes.Black, sngCenterPage, 118)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 130)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N).Width / 2)
                                e.Graphics.DrawString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N, Brushes.Black, sngCenterPage, 142)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("MIN: XXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("MIN: XXXXXXXXXX", N, Brushes.Black, sngCenterPage, 154)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 165)

                                e.Graphics.DrawString("Cashier: " & LabelCashierName.Text & ControlChars.NewLine & "Station ID: " & OriginID.Text, N, Brushes.Black, 4, 175)
                                e.Graphics.DrawString("Store Copy", N, Brushes.Black, 4, 200)
                                e.Graphics.DrawString("TRAINING MODE", N, Brushes.Black, 160, 200)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 210)

                                e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 220)
                                e.Graphics.DrawString("OR #: " & TextBoxBarcode.Text, B, Brushes.Black, New RectangleF(4, 232, 0, 0))
                                e.Graphics.DrawString("PHP", B, Brushes.Black, New RectangleF(80, 232, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 241)

                                e.Graphics.DrawString("NAME" & ControlChars.NewLine &
                                              "VESSEL ID" & ControlChars.NewLine &
                                              "SCHEDULE" & ControlChars.NewLine &
                                              "SEAT NO." & ControlChars.NewLine &
                                              "PT. OF ORIGIN" & ControlChars.NewLine &
                                              "PT. OF DESTINATION" & ControlChars.NewLine &
                                              "TRIP FARE" & ControlChars.NewLine &
                                              "BAGGAGE COST" & ControlChars.NewLine &
                                              "CARD AMOUNT", N, Brushes.Black, New RectangleF(4, 250, 0, 0))
                                e.Graphics.DrawString(fname.Text & " " & lname.Text & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              TextBoxBaggageCost.Text & ControlChars.NewLine &
                                              TextBoxCardAmount.Text, N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 358)

                                barcode.Resolution = 155

                                barcode.Format = System.Drawing.Imaging.ImageFormat.Gif
                                PictureBoxBarcode.Image = barcode.drawBarcodeOnBitmap()

                                e.Graphics.DrawImage(PictureBoxBarcode.Image, 6, 367)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcode.Text, N).Width / 2)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 411)

                                e.Graphics.DrawString("TOTAL AMOUNT" & ControlChars.NewLine & "CASH" & ControlChars.NewLine & "CHANGE", N, Brushes.Black, New RectangleF(4, 420, 0, 0))
                                e.Graphics.DrawString(LabelTotal.Text & ControlChars.NewLine & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")) & ControlChars.NewLine & TextBoxChange.Text, N, Brushes.Black, New RectangleF(80, 420, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 456)

                                e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine & "VAT Amount" & ControlChars.NewLine & "(X) VAT-Exempt Sales" & ControlChars.NewLine & "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                                e.Graphics.DrawString(LabelVATable.Text & ControlChars.NewLine & LabelVAT.Text & ControlChars.NewLine & LabelVATExempt.Text & ControlChars.NewLine & LabelZeroRated.Text & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 517)

                                e.Graphics.DrawString("Discount", N, Brushes.Black, New RectangleF(4, 528, 0, 0))
                                e.Graphics.DrawString(LabelDiscount.Text & ControlChars.NewLine & LabelType.Text, N, Brushes.Black, New RectangleF(80, 528, 195, 195), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 547)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS IS NOT AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS IS NOT AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 568)

                                e.Graphics.DrawString("Name: _________________________________" & ControlChars.NewLine &
                                          "Address: ______________________________" & ControlChars.NewLine &
                                          "_______________________________________" & ControlChars.NewLine &
                                          "ID #: _________________________________" & ControlChars.NewLine &
                                          "OSCA/SC ID #: _________________________" & ControlChars.NewLine &
                                          "PWD ID #: _____________________________" & ControlChars.NewLine &
                                          "TIN #: ________________________________" & ControlChars.NewLine &
                                          "Bus Style: ____________________________" & ControlChars.NewLine &
                                          "Signature: ____________________________", N, Brushes.Black, New RectangleF(4, 578, 0, 0))

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 690)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VINTATECH", N).Width / 2)
                                e.Graphics.DrawString("VINTATECH", N, Brushes.Black, sngCenterPage, 700)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Unit 507 F&L Bldg. 2211", N).Width / 2)
                                e.Graphics.DrawString("Unit 507 F&L Bldg. 2211", N, Brushes.Black, sngCenterPage, 712)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Commonwealth, Holy Spirit 1127", N).Width / 2)
                                e.Graphics.DrawString("Commonwealth, Holy Spirit 1127", N, Brushes.Black, sngCenterPage, 724)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Quezon City", N).Width / 2)
                                e.Graphics.DrawString("Quezon City", N, Brushes.Black, sngCenterPage, 736)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                                e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 748)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Accred. No.: XXXXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("Accred. No.: XXXXXXXXXXX", N, Brushes.Black, sngCenterPage, 760)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Date Issued: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 772)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 784)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PTU No.: XXXXXXXXX", N).Width / 2)
                                e.Graphics.DrawString("PTU No.: XXXXXXXXX", N, Brushes.Black, sngCenterPage, 796)
                                e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 808)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                                e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 820)

                                e.HasMorePages = False
                        End Select
                        page_number += 1


                        'Regular Transaction
                    Else

                        TextToPrint &= Environment.NewLine
                        e.Graphics.DrawImage(PictureBoxLogoReceipt.Image, 92, 0)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("CAVITE SUPERFERRY", N).Width / 2)
                        e.Graphics.DrawString("CAVITE SUPERFERRY", N, Brushes.Black, sngCenterPage, 70)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PINOY CATAMARAN", N).Width / 2)
                        e.Graphics.DrawString("PINOY CATAMARAN", N, Brushes.Black, sngCenterPage, 82)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("#17 SILAHIS STREET,", N).Width / 2)
                        e.Graphics.DrawString("#17 SILAHIS STREET,", N, Brushes.Black, sngCenterPage, 94)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BARANGAY TANZA, NAVOTAS CITY", N).Width / 2)
                        e.Graphics.DrawString("BARANGAY TANZA, NAVOTAS CITY", N, Brushes.Black, sngCenterPage, 106)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PHILIPPINES", N).Width / 2)
                        e.Graphics.DrawString("PHILIPPINES", N, Brushes.Black, sngCenterPage, 118)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                        e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 130)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N).Width / 2)
                        e.Graphics.DrawString("POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text, N, Brushes.Black, sngCenterPage, 142)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("MIN: XXXXXXXXXX", N).Width / 2)
                        e.Graphics.DrawString("MIN: XXXXXXXXXX", N, Brushes.Black, sngCenterPage, 154)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 165)

                        e.Graphics.DrawString("Cashier: " & LabelCashierName.Text & ControlChars.NewLine & "Station ID: " & OriginID.Text, N, Brushes.Black, 4, 175)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("TRAINING MODE", N).Width / 2)
                        e.Graphics.DrawString("TRAINING MODE", N, Brushes.Black, sngCenterPage, 200)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 210)

                        e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 220)
                        e.Graphics.DrawString("OR #: " & TextBoxBarcode.Text, B, Brushes.Black, New RectangleF(4, 232, 0, 0))
                        e.Graphics.DrawString("PHP", B, Brushes.Black, New RectangleF(80, 232, 195, 195), sf)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 241)

                        e.Graphics.DrawString("NAME" & ControlChars.NewLine &
                                              "VESSEL ID" & ControlChars.NewLine &
                                              "SCHEDULE" & ControlChars.NewLine &
                                              "SEAT NO." & ControlChars.NewLine &
                                              "PT. OF ORIGIN" & ControlChars.NewLine &
                                              "PT. OF DESTINATION" & ControlChars.NewLine &
                                              "TRIP FARE" & ControlChars.NewLine &
                                              "BAGGAGE COST" & ControlChars.NewLine &
                                              "CARD AMOUNT", N, Brushes.Black, New RectangleF(4, 250, 0, 0))
                        e.Graphics.DrawString(fname.Text & " " & lname.Text & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              TextBoxBaggageCost.Text & ControlChars.NewLine &
                                              TextBoxCardAmount.Text, N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 358)

                        barcode.Resolution = 155

                        barcode.Format = System.Drawing.Imaging.ImageFormat.Gif
                        PictureBoxBarcode.Image = barcode.drawBarcodeOnBitmap()

                        e.Graphics.DrawImage(PictureBoxBarcode.Image, 6, 367)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcode.Text, N).Width / 2)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 411)

                        e.Graphics.DrawString("TOTAL AMOUNT" & ControlChars.NewLine & "CASH" & ControlChars.NewLine & "CHANGE", N, Brushes.Black, New RectangleF(4, 420, 0, 0))
                        e.Graphics.DrawString(LabelTotal.Text & ControlChars.NewLine & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")) & ControlChars.NewLine & TextBoxChange.Text, N, Brushes.Black, New RectangleF(80, 420, 195, 195), sf)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 456)

                        If ButtonZeroRated.BackColor = Color.Gainsboro Then
                            e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine & "VAT Amount" & ControlChars.NewLine & "(X) VAT-Exempt Sales" & ControlChars.NewLine & "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                            e.Graphics.DrawString(LabelVATable.Text & ControlChars.NewLine & LabelVAT.Text & ControlChars.NewLine & LabelVATExempt.Text & ControlChars.NewLine & LabelZeroRated.Text & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

                        ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                            e.Graphics.DrawString("(V) VATable Sales" & ControlChars.NewLine & "VAT Amount" & ControlChars.NewLine & "(X) VAT-Exempt Sales" & ControlChars.NewLine & "(Z) Zero-Rated Sales" & ControlChars.NewLine, N, Brushes.Black, New RectangleF(4, 467, 0, 0))
                            e.Graphics.DrawString(LabelVATable.Text & ControlChars.NewLine & LabelVAT.Text & ControlChars.NewLine & LabelVATExempt.Text & ControlChars.NewLine & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text & ControlChars.NewLine, N, Brushes.Black, New RectangleF(80, 467, 195, 195), sf)

                        End If

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 517)

                        e.Graphics.DrawString("Discount", N, Brushes.Black, New RectangleF(4, 528, 0, 0))
                        e.Graphics.DrawString(LabelDiscount.Text & ControlChars.NewLine & LabelType.Text, N, Brushes.Black, New RectangleF(80, 528, 195, 195), sf)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 547)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS IS NOT AN OFFICIAL RECEIPT", N).Width / 2)
                        e.Graphics.DrawString("THIS IS NOT AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 568)

                        e.Graphics.DrawString("Name: _________________________________" & ControlChars.NewLine &
                                          "Address: ______________________________" & ControlChars.NewLine &
                                          "_______________________________________" & ControlChars.NewLine &
                                          "ID #: _________________________________" & ControlChars.NewLine &
                                          "OSCA/SC ID #: _________________________" & ControlChars.NewLine &
                                          "PWD ID #: _____________________________" & ControlChars.NewLine &
                                          "TIN #: ________________________________" & ControlChars.NewLine &
                                          "Bus Style: ____________________________" & ControlChars.NewLine &
                                          "Signature: ____________________________", N, Brushes.Black, New RectangleF(4, 578, 0, 0))

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 690)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VINTATECH", N).Width / 2)
                        e.Graphics.DrawString("VINTATECH", N, Brushes.Black, sngCenterPage, 700)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Unit 507 F&L Bldg. 2211", N).Width / 2)
                        e.Graphics.DrawString("Unit 507 F&L Bldg. 2211", N, Brushes.Black, sngCenterPage, 712)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Commonwealth, Holy Spirit 1127", N).Width / 2)
                        e.Graphics.DrawString("Commonwealth, Holy Spirit 1127", N, Brushes.Black, sngCenterPage, 724)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Quezon City", N).Width / 2)
                        e.Graphics.DrawString("Quezon City", N, Brushes.Black, sngCenterPage, 736)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("VAT REG TIN: 745-993-747-000", N).Width / 2)
                        e.Graphics.DrawString("VAT REG TIN: 745-993-747-000", N, Brushes.Black, sngCenterPage, 748)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Accred. No.: XXXXXXXXXXX", N).Width / 2)
                        e.Graphics.DrawString("Accred. No.: XXXXXXXXXXX", N, Brushes.Black, sngCenterPage, 760)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Date Issued: XX/XX/XXXX", N).Width / 2)
                        e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 772)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                        e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 784)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("PTU No.: XXXXXXXXX", N).Width / 2)
                        e.Graphics.DrawString("PTU No.: XXXXXXXXX", N, Brushes.Black, sngCenterPage, 796)
                        e.Graphics.DrawString("Date Issued: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 808)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("Valid Until: XX/XX/XXXX", N).Width / 2)
                        e.Graphics.DrawString("Valid Until: XX/XX/XXXX", N, Brushes.Black, sngCenterPage, 820)

                        ''End of THERMAL
                    End If

                End If


            ElseIf ButtonEmployee.BackColor = Color.Gray Then

                combo = ComboBoxTime.Text.Split("-")

                Dim comb1 = combo(1).ToString
                Dim comb = combo(0).ToString

                Select Case page_number
                    Case 0

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("BOARDING AND ALIGHTING PASS", B).Width / 2)
                        e.Graphics.DrawString("BOARDING AND ALIGHTING PASS", B, Brushes.Black, sngCenterPage, 2)

                        e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm"), N, Brushes.Black, 4, 25)
                        e.Graphics.DrawString("TICKET #: " & TextBoxBarcodeTicket.Text, B, Brushes.Black, New RectangleF(4, 40, 0, 0))

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 60)

                        barcodeticket.Resolution = 155

                        barcodeticket.Format = System.Drawing.Imaging.ImageFormat.Gif
                        PictureBoxBarcodeTicket.Image = barcodeticket.drawBarcodeOnBitmap()

                        e.Graphics.DrawImage(PictureBoxBarcodeTicket.Image, 6, 70)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString(TextBoxBarcodeTicket.Text, N).Width / 2)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 115)

                        e.Graphics.DrawString("VESSEL ID" & ControlChars.NewLine & "SCHEDULE" & ControlChars.NewLine & "SEAT NO." & ControlChars.NewLine & "PT. OF ORIGIN" & ControlChars.NewLine & "PT. OF DESTINATION", N, Brushes.Black, New RectangleF(4, 130, 0, 0))
                        e.Graphics.DrawString(comb & ControlChars.NewLine & comb1 & ControlChars.NewLine & TextBoxSeat.Text & ControlChars.NewLine & ButtonOrigin.Text & ControlChars.NewLine & ButtonDestination.Text, N, Brushes.Black, New RectangleF(100, 130, 175, 175), sf)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("-- This is a sample ticket only --", N).Width / 2)
                        e.Graphics.DrawString("-- This is a sample ticket only --", N, Brushes.Black, sngCenterPage, 200)

                        sf.Dispose()
                        e.HasMorePages = True

                    Case 1

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("NOTE ", B).Width / 2)
                        e.Graphics.DrawString("NOTE ", B, Brushes.Black, sngCenterPage, 2)

                        e.HasMorePages = False

                End Select
                page_number += 1

            End If

            Try
                With SerialPort1
                    .PortName = "COM2"
                    .BaudRate = 9600
                    .DataBits = 8
                    .Parity = Parity.None
                    .StopBits = StopBits.One
                    .Handshake = Handshake.None
                End With

                If Not (SerialPort1.IsOpen = True) Then
                    SerialPort1.Open()
                End If

                SerialPort1.DiscardOutBuffer()

                SerialPort1.Write(displayClear, 0, 1)

                SerialPort1.Write(displayHome, 0, 1)
                SerialPort1.Write(("Welcome to").PadLeft(10) & vbCrLf & "FERRY STATION")

                SerialPort1.Close()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBoxBarcode_TextChanged(sender As Object, e As EventArgs) Handles TextBoxBarcode.TextChanged
        'assigning value of barcode
        Try
            barcode.Code = TextBoxBarcode.Text
        Catch ex As Exception
            MsgBox(ex.Message + "Barcode")
        End Try
    End Sub

    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick
        'for displaying current datetime
        LabelDateTime.Text = DateTime.Now.ToString("ddd MM/dd/yyyy HH:mm:ss")
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ButtonSeat_Click(sender As Object, e As EventArgs) Handles ButtonSeat.Click
        If ComboBoxTime.Text = "" Or
                ButtonOrigin.Text = "Origin" Or
                ButtonDestination.Text = "Destination" Then
            MsgBox("Please input Origin, Destination and Time first.", MessageBoxIcon.Warning)
        Else
            seatDEMO.Show()
        End If
    End Sub

    Private Sub ButtonOrigin_Click(sender As Object, e As EventArgs) Handles ButtonOrigin.Click
        originDEMO.Show()
    End Sub

    Private Sub ButtonDestination_Click(sender As Object, e As EventArgs) Handles ButtonDestination.Click
        destinationDEMO.Show()
    End Sub

    Private Sub ButtonActualMode_Click(sender As Object, e As EventArgs) Handles ButtonActualMode.Click
        mainform.LabelCashierName.Text = LabelCashierName.Text
        mainform.LabelCashierID.Text = LabelCashierID.Text

        Me.Close()
        mainform.Show()
    End Sub

    Private Sub Passenger_idTextBox_TextChanged(sender As Object, e As EventArgs) Handles Passenger_idTextBox.TextChanged
        Try
            'for displaying passenger's info
            If Passenger_idTextBox.Text = "AAA111" Then
                lname.Text = "Cruz"
                fname.Text = "Crisanto"
                gender.Text = "Male"
                profession.Text = "Student"
                idtype.Text = "Student ID"

                PictureBoxFace.Show()
            Else
                'conn.Close()
                lname.Text = ""
                fname.Text = ""
                gender.Text = ""
                profession.Text = ""
                idtype.Text = ""
                cardno.Text = ""
                PictureBoxFace.Hide()
                TextBoxTripFare.Text = "0.00"
                TextBoxBaggageCost.Text = "0.00"
                TextBoxTripFare.Text = "0.00"
                TextBoxChange.Clear()
                TextBoxMoney.Clear()
                seat.LabelID.Text = Nothing
                TextBoxSeat.Clear()
                LabelFare.Text = Nothing
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                ButtonOrigin.Text = "Origin"
                ButtonDestination.Text = "Destination"
                ButtonCard.BackColor = Color.Gainsboro
                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                ButtonOrigin.Enabled = True
                ButtonDestination.Enabled = True
                ComboBoxTime.Enabled = True
                ButtonSeat.Enabled = True

                TextBoxCardAmount.Text = "0.00"
                TextBoxTripFare.Text = "0.00"
                TextBoxBaggageCost.Text = "0.00"
                TextBoxMoney.Text = ""
                TextBoxChange.Text = ""
                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"

                LabelVATExempt.Text = "0.00"
                LabelDiscount.Text = "0.00"
                LabelTotal.Text = ""
                'mainform.LabelPercent.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                ComboBoxTime.Text = Nothing
                LabelType.Text = "Regular"

                Button1.Enabled = True
                Button2.Enabled = True
                Button3.Enabled = True
                Button4.Enabled = True
                Button5.Enabled = True
                Button6.Enabled = True
                Button7.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button100.Enabled = True
                Button200.Enabled = True
                Button500.Enabled = True
                Button1000.Enabled = True
                ButtonDot.Enabled = True
                Button0.Enabled = True
                Button00.Enabled = True
                ButtonDel.Enabled = True
                ButtonClear.Enabled = True
                ButtonOk.Enabled = True

                ButtonBaggage.Enabled = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MessageBoxIcon.Warning)
            'conn.Close()
        End Try
        'conn.Close()
    End Sub

    Private Sub ButtonStudent_Click(sender As Object, e As EventArgs) Handles ButtonStudent.Click
        Try

            TextBoxTripFare.Text = LabelFare.Text

            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Button5.Enabled = True
            Button6.Enabled = True
            Button7.Enabled = True
            Button8.Enabled = True
            Button9.Enabled = True
            Button100.Enabled = True
            Button200.Enabled = True
            Button500.Enabled = True
            Button1000.Enabled = True
            ButtonDot.Enabled = True
            Button0.Enabled = True
            Button00.Enabled = True
            ButtonDel.Enabled = True
            ButtonClear.Enabled = True
            ButtonOk.Enabled = True

            Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")

            If ButtonStudent.BackColor = Color.Gainsboro Then
                ButtonStudent.BackColor = Color.Gray
                LabelType.Text = "Student"

                LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                LabelDiscount.Text = (Math.Round(Val((LabelVATable.Text * TextBoxPercentDiscount.Text)), 2))
                'LabelTotal.Text = (Math.Round(Val((LabelVATable.Text - LabelDiscount.Text)), 2))
                LabelTotal.Text = (Math.Round(Val(LabelVAT.Text) + (Val((LabelVATable.Text - LabelDiscount.Text))), 2))
                secondscreen.PaymentDue.Text = LabelTotal.Text
                'LabelPercent.Text = "20%"

                '------ FOR MMDA
                'LabelVATable.Text = "0.00"
                'LabelVAT.Text = "0.00"
                'LabelVATExempt.Text = "0.00"
                'LabelZeroRated.Text = "0.00"

                'LabelDiscount.Text = FormatNumber(CDbl(Val(LabelFare.Text * "0.20")), 2)
                'TextBoxPayment.Text = FormatNumber(CDbl(Val(LabelFare.Text - LabelDiscount.Text)), 2)
                'secondscreen.PaymentDue.Text = TextBoxPayment.Text
                'LabelPercent.Text = "20%"
                '------ FOR MMDA

            ElseIf ButtonStudent.BackColor = Color.Gray Then
                ButtonStudent.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                '------ FOR MMDA
                'LabelVATable.Text = "0.00"
                'LabelVAT.Text = "0.00"
                'LabelVATExempt.Text = "0.00"
                'LabelZeroRated.Text = "0.00"
                '------ FOR MMDA

                LabelDiscount.Text = "0.00"
                LabelTotal.Text = sumpayment
                secondscreen.PaymentDue.Text = LabelTotal.Text
                'LabelPercent.Text = "0.00"
            End If

            If ButtonStudent.BackColor = Color.Gray Then
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Student"
            End If

            Try
                With SerialPort1
                    .PortName = "COM2"
                    .BaudRate = 9600
                    .DataBits = 8
                    .Parity = Parity.None
                    .StopBits = StopBits.One
                    .Handshake = Handshake.None
                End With

                If Not (SerialPort1.IsOpen = True) Then
                    SerialPort1.Open()
                End If

                SerialPort1.DiscardOutBuffer()

                SerialPort1.Write(displayClear, 0, 1)

                SerialPort1.Write(displayHome, 0, 1)
                SerialPort1.Write("Amount Due:" & vbCrLf & Convert.ToDecimal(LabelTotal.Text).ToString("0.00"))
                SerialPort1.Close()

            Catch ex As Exception
                'MsgBox(ex.Message, MessageBoxIcon.Warning)
            End Try

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Origin/Destination!", MessageBoxIcon.Information)
            ButtonStudent.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
    End Sub

    Private Sub ButtonPWD_Click(sender As Object, e As EventArgs) Handles ButtonPWD.Click
        Try

            TextBoxTripFare.Text = LabelFare.Text

            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Button5.Enabled = True
            Button6.Enabled = True
            Button7.Enabled = True
            Button8.Enabled = True
            Button9.Enabled = True
            Button100.Enabled = True
            Button200.Enabled = True
            Button500.Enabled = True
            Button1000.Enabled = True
            ButtonDot.Enabled = True
            Button0.Enabled = True
            Button00.Enabled = True
            ButtonDel.Enabled = True
            ButtonClear.Enabled = True
            ButtonOk.Enabled = True

            Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")

            If ButtonPWD.BackColor = Color.Gainsboro Then
                ButtonPWD.BackColor = Color.Gray
                LabelType.Text = "PWD"

                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                LabelVATExempt.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelZeroRated.Text = "0.00"

                LabelDiscount.Text = (Math.Round(Val((LabelVATExempt.Text * TextBoxPercentDiscount.Text)), 2))
                LabelTotal.Text = (Math.Round(Val((LabelVATExempt.Text - LabelDiscount.Text)), 2))
                secondscreen.PaymentDue.Text = LabelTotal.Text
                'LabelPercent.Text = "20%"

                '------ FOR MMDA
                'LabelVATable.Text = "0.00"
                'LabelVAT.Text = "0.00"
                'LabelVATExempt.Text = "0.00"
                'LabelZeroRated.Text = "0.00"

                'LabelDiscount.Text = FormatNumber(CDbl(Val(LabelFare.Text * "0.20")), 2)
                'TextBoxPayment.Text = FormatNumber(CDbl(Val(LabelFare.Text - LabelDiscount.Text)), 2)
                'secondscreen.PaymentDue.Text = TextBoxPayment.Text
                'LabelPercent.Text = "20%"
                '------ FOR MMDA

            ElseIf ButtonPWD.BackColor = Color.Gray Then
                ButtonPWD.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                '------ FOR MMDA
                'LabelVATable.Text = "0.00"
                'LabelVAT.Text = "0.00"
                'LabelVATExempt.Text = "0.00"
                'LabelZeroRated.Text = "0.00"
                '------ FOR MMDA

                LabelDiscount.Text = "0.00"
                LabelTotal.Text = sumpayment
                secondscreen.PaymentDue.Text = LabelTotal.Text
                'LabelPercent.Text = "0.00"
            End If

            If ButtonPWD.BackColor = Color.Gray Then
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "PWD"
            End If

            Try
                With SerialPort1
                    .PortName = "COM2"
                    .BaudRate = 9600
                    .DataBits = 8
                    .Parity = Parity.None
                    .StopBits = StopBits.One
                    .Handshake = Handshake.None
                End With

                If Not (SerialPort1.IsOpen = True) Then
                    SerialPort1.Open()
                End If

                SerialPort1.DiscardOutBuffer()

                SerialPort1.Write(displayClear, 0, 1)

                SerialPort1.Write(displayHome, 0, 1)
                SerialPort1.Write("Amount Due:" & vbCrLf & Convert.ToDecimal(LabelTotal.Text).ToString("0.00"))
                SerialPort1.Close()

            Catch ex As Exception
                'MsgBox(ex.Message, MessageBoxIcon.Warning)
            End Try

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Origin/Destination!", MessageBoxIcon.Information)
            ButtonPWD.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
    End Sub

    Private Sub ButtonSenior_Click(sender As Object, e As EventArgs) Handles ButtonSenior.Click
        Try

            TextBoxTripFare.Text = LabelFare.Text

            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Button5.Enabled = True
            Button6.Enabled = True
            Button7.Enabled = True
            Button8.Enabled = True
            Button9.Enabled = True
            Button100.Enabled = True
            Button200.Enabled = True
            Button500.Enabled = True
            Button1000.Enabled = True
            ButtonDot.Enabled = True
            Button0.Enabled = True
            Button00.Enabled = True
            ButtonDel.Enabled = True
            ButtonClear.Enabled = True
            ButtonOk.Enabled = True

            Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")

            If ButtonSenior.BackColor = Color.Gainsboro Then
                ButtonSenior.BackColor = Color.Gray
                LabelType.Text = "Senior"

                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                LabelVATExempt.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelZeroRated.Text = "0.00"

                'LabelDiscount.Text = (Math.Round(Val((LabelVATExempt.Text * TextBoxPercentDiscount.Text)), 2))
                LabelDiscount.Text = (Math.Round(Val((LabelVATExempt.Text * TextBoxPercentDiscount.Text)), 2))
                LabelTotal.Text = (Math.Round(Val((LabelVATExempt.Text - LabelDiscount.Text)), 2))
                secondscreen.PaymentDue.Text = LabelTotal.Text
                'LabelPercent.Text = "20%"

                '------ FOR MMDA
                'LabelVATable.Text = "0.00"
                'LabelVAT.Text = "0.00"
                'LabelVATExempt.Text = "0.00"
                'LabelZeroRated.Text = "0.00"

                'LabelDiscount.Text = FormatNumber(CDbl(Val(LabelFare.Text * "0.20")), 2)
                'TextBoxPayment.Text = FormatNumber(CDbl(Val(LabelFare.Text - LabelDiscount.Text)), 2)
                'secondscreen.PaymentDue.Text = TextBoxPayment.Text
                'LabelPercent.Text = "20%"
                '------ FOR MMDA

            ElseIf ButtonSenior.BackColor = Color.Gray Then
                ButtonSenior.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                '------ FOR MMDA
                'LabelVATable.Text = "0.00"
                'LabelVAT.Text = "0.00"
                'LabelVATExempt.Text = "0.00"
                'LabelZeroRated.Text = "0.00"
                '------ FOR MMDA

                LabelDiscount.Text = "0.00"
                LabelTotal.Text = sumpayment
                secondscreen.PaymentDue.Text = LabelTotal.Text
                'LabelPercent.Text = "0.00"
            End If

            If ButtonSenior.BackColor = Color.Gray Then
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Senior"
            End If

            Try
                With SerialPort1
                    .PortName = "COM2"
                    .BaudRate = 9600
                    .DataBits = 8
                    .Parity = Parity.None
                    .StopBits = StopBits.One
                    .Handshake = Handshake.None
                End With

                If Not (SerialPort1.IsOpen = True) Then
                    SerialPort1.Open()
                End If

                SerialPort1.DiscardOutBuffer()

                SerialPort1.Write(displayClear, 0, 1)

                SerialPort1.Write(displayHome, 0, 1)
                SerialPort1.Write("Amount Due:" & vbCrLf & Convert.ToDecimal(LabelTotal.Text).ToString("0.00"))
                SerialPort1.Close()

            Catch ex As Exception
                'MsgBox(ex.Message, MessageBoxIcon.Warning)
            End Try

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox(ex.Message & "Please input Origin/Destination!", MessageBoxIcon.Information)
            ButtonSenior.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
    End Sub

    Private Sub ButtonZeroRated_Click(sender As Object, e As EventArgs) Handles ButtonZeroRated.Click
        If ButtonCard.BackColor = Color.Gray Then

            Try

                TextBoxTripFare.Text = LabelFare.Text

                Button1.Enabled = True
                Button2.Enabled = True
                Button3.Enabled = True
                Button4.Enabled = True
                Button5.Enabled = True
                Button6.Enabled = True
                Button7.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button100.Enabled = True
                Button200.Enabled = True
                Button500.Enabled = True
                Button1000.Enabled = True
                ButtonDot.Enabled = True
                Button0.Enabled = True
                Button00.Enabled = True
                ButtonDel.Enabled = True
                ButtonClear.Enabled = True
                ButtonOk.Enabled = True

                Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")
                Dim VAT As String = (Math.Round(Val((sumpayment / "1.12")), 2))

                If ButtonZeroRated.BackColor = Color.Gainsboro Then
                    ButtonZeroRated.BackColor = Color.Gray
                    LabelType.Text = "Zero Rated"

                    LabelVATable.Text = "0.00"
                    LabelVAT.Text = "0.00"
                    LabelVATExempt.Text = "0.00"
                    LabelZeroRated.Text = VAT

                    LabelDiscount.Text = "0.00"
                    LabelTotal.Text = "0.00"
                    secondscreen.PaymentDue.Text = LabelTotal.Text
                    'LabelPercent.Text = "20%"

                ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                    ButtonZeroRated.BackColor = Color.Gainsboro
                    LabelType.Text = "Regular"

                    LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                    LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))
                    LabelVATExempt.Text = "0.00"
                    LabelZeroRated.Text = "0.00"

                    '------ FOR MMDA
                    'LabelVATable.Text = "0.00"
                    'LabelVAT.Text = "0.00"
                    'LabelVATExempt.Text = "0.00"
                    'LabelZeroRated.Text = "0.00"
                    '------ FOR MMDA

                    LabelDiscount.Text = "0.00"
                    LabelTotal.Text = sumpayment
                    secondscreen.PaymentDue.Text = LabelTotal.Text
                    'LabelPercent.Text = "0.00"
                End If

                If ButtonZeroRated.BackColor = Color.Gray Then
                    ButtonStudent.BackColor = Color.Gainsboro
                    ButtonPWD.BackColor = Color.Gainsboro
                    ButtonSenior.BackColor = Color.Gainsboro
                    ButtonEmployee.BackColor = Color.Gainsboro
                    LabelType.Text = "Zero Rated"
                End If

                Try
                    With SerialPort1
                        .PortName = "COM2"
                        .BaudRate = 9600
                        .DataBits = 8
                        .Parity = Parity.None
                        .StopBits = StopBits.One
                        .Handshake = Handshake.None
                    End With

                    If Not (SerialPort1.IsOpen = True) Then
                        SerialPort1.Open()
                    End If

                    SerialPort1.DiscardOutBuffer()

                    SerialPort1.Write(displayClear, 0, 1)

                    SerialPort1.Write(displayHome, 0, 1)
                    SerialPort1.Write("Amount Due:" & vbCrLf & Convert.ToDecimal(LabelTotal.Text).ToString("0.00"))
                    SerialPort1.Close()

                Catch ex As Exception
                    'MsgBox(ex.Message, MessageBoxIcon.Warning)
                End Try

                TextBoxMoney.Clear()
                TextBoxChange.Clear()
                secondscreen.Change.Text = Nothing

            Catch ex As Exception
                MsgBox("Please input Origin/Destination! OR Choose button CARD!", MessageBoxIcon.Information)
                ButtonZeroRated.BackColor = Color.Gainsboro
                TextBoxTripFare.Clear()
                LabelType.Text = "Regular"
            End Try

        ElseIf ButtonDestination.Text = "Destination" Then

            MsgBox("Please input Origin/Destination!", MessageBoxIcon.Information)
            ButtonZeroRated.BackColor = Color.Gainsboro
            TextBoxTripFare.Text = "0.00"
            TextBoxBaggageCost.Text = "0.00"
            TextBoxCardAmount.Text = "0.00"

        Else

            Try

                TextBoxTripFare.Text = LabelFare.Text

                Button1.Enabled = True
                Button2.Enabled = True
                Button3.Enabled = True
                Button4.Enabled = True
                Button5.Enabled = True
                Button6.Enabled = True
                Button7.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button100.Enabled = True
                Button200.Enabled = True
                Button500.Enabled = True
                Button1000.Enabled = True
                ButtonDot.Enabled = True
                Button0.Enabled = True
                Button00.Enabled = True
                ButtonDel.Enabled = True
                ButtonClear.Enabled = True
                ButtonOk.Enabled = True

                Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")
                Dim VAT As String = (Math.Round(Val((sumpayment / "1.12")), 2))

                If ButtonZeroRated.BackColor = Color.Gainsboro Then
                    ButtonZeroRated.BackColor = Color.Gray
                    LabelType.Text = "Zero Rated"

                    LabelVATable.Text = "0.00"
                    LabelVAT.Text = "0.00"
                    LabelVATExempt.Text = "0.00"
                    LabelZeroRated.Text = VAT

                    LabelDiscount.Text = "0.00"
                    LabelTotal.Text = "0.00"
                    secondscreen.PaymentDue.Text = LabelTotal.Text
                    'LabelPercent.Text = "20%"

                ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                    ButtonZeroRated.BackColor = Color.Gainsboro
                    LabelType.Text = "Regular"

                    LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                    LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))
                    LabelVATExempt.Text = "0.00"
                    LabelZeroRated.Text = "0.00"

                    '------ FOR MMDA
                    'LabelVATable.Text = "0.00"
                    'LabelVAT.Text = "0.00"
                    'LabelVATExempt.Text = "0.00"
                    'LabelZeroRated.Text = "0.00"
                    '------ FOR MMDA

                    LabelDiscount.Text = "0.00"
                    LabelTotal.Text = sumpayment
                    secondscreen.PaymentDue.Text = LabelTotal.Text
                    'LabelPercent.Text = "0.00"
                End If

                If ButtonZeroRated.BackColor = Color.Gray Then
                    ButtonStudent.BackColor = Color.Gainsboro
                    ButtonPWD.BackColor = Color.Gainsboro
                    ButtonSenior.BackColor = Color.Gainsboro
                    ButtonEmployee.BackColor = Color.Gainsboro
                    LabelType.Text = "Zero Rated"
                End If

                Try
                    With SerialPort1
                        .PortName = "COM2"
                        .BaudRate = 9600
                        .DataBits = 8
                        .Parity = Parity.None
                        .StopBits = StopBits.One
                        .Handshake = Handshake.None
                    End With

                    If Not (SerialPort1.IsOpen = True) Then
                        SerialPort1.Open()
                    End If

                    SerialPort1.DiscardOutBuffer()

                    SerialPort1.Write(displayClear, 0, 1)

                    SerialPort1.Write(displayHome, 0, 1)
                    SerialPort1.Write("Amount Due:" & vbCrLf & Convert.ToDecimal(LabelTotal.Text).ToString("0.00"))
                    SerialPort1.Close()

                Catch ex As Exception
                    'MsgBox(ex.Message, MessageBoxIcon.Warning)
                End Try

                TextBoxMoney.Clear()
                TextBoxChange.Clear()
                secondscreen.Change.Text = Nothing

            Catch ex As Exception
                MsgBox("Please input Origin/Destination!", MessageBoxIcon.Information)
                ButtonZeroRated.BackColor = Color.Gainsboro
                TextBoxTripFare.Clear()
                LabelType.Text = "Regular"
            End Try

        End If
    End Sub

    Private Sub ButtonEmployee_Click(sender As Object, e As EventArgs) Handles ButtonEmployee.Click
        Try

            Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")

            If ButtonEmployee.BackColor = Color.Gainsboro Then
                ButtonEmployee.BackColor = Color.Gray
                LabelType.Text = "Employee"

                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                LabelDiscount.Text = "0.00"
                'LabelTotal.Text = (Math.Round(Val((LabelVATable.Text - LabelDiscount.Text)), 2))
                LabelTotal.Text = "0.00"
                secondscreen.PaymentDue.Text = LabelTotal.Text
                'LabelPercent.Text = "20%"

                '------ FOR MMDA
                'LabelVATable.Text = "0.00"
                'LabelVAT.Text = "0.00"
                'LabelVATExempt.Text = "0.00"
                'LabelZeroRated.Text = "0.00"

                'LabelDiscount.Text = FormatNumber(CDbl(Val(LabelFare.Text * "0.20")), 2)
                'TextBoxPayment.Text = FormatNumber(CDbl(Val(LabelFare.Text - LabelDiscount.Text)), 2)
                'secondscreen.PaymentDue.Text = TextBoxPayment.Text
                'LabelPercent.Text = "20%"
                '------ FOR MMDA

                Button1.Enabled = False
                Button2.Enabled = False
                Button3.Enabled = False
                Button4.Enabled = False
                Button5.Enabled = False
                Button6.Enabled = False
                Button7.Enabled = False
                Button8.Enabled = False
                Button9.Enabled = False
                Button100.Enabled = False
                Button200.Enabled = False
                Button500.Enabled = False
                Button1000.Enabled = False
                ButtonDot.Enabled = False
                Button0.Enabled = False
                Button00.Enabled = False
                ButtonDel.Enabled = False
                ButtonClear.Enabled = False
                ButtonOk.Enabled = False

                TextBoxTripFare.Text = "0.00"
                TextBoxBaggageCost.Text = "0.00"
                TextBoxCardAmount.Text = "0.00"

                TextBoxMoney.Text = "0.00"
                TextBoxChange.Text = "0.00"

            ElseIf ButtonEmployee.BackColor = Color.Gray Then
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                TextBoxTripFare.Text = LabelFare.Text

                LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                '------ FOR MMDA
                'LabelVATable.Text = "0.00"
                'LabelVAT.Text = "0.00"
                'LabelVATExempt.Text = "0.00"
                'LabelZeroRated.Text = "0.00"
                '------ FOR MMDA

                LabelDiscount.Text = "0.00"
                LabelTotal.Text = sumpayment
                secondscreen.PaymentDue.Text = LabelTotal.Text
                'LabelPercent.Text = "0.00"

                Button1.Enabled = True
                Button2.Enabled = True
                Button3.Enabled = True
                Button4.Enabled = True
                Button5.Enabled = True
                Button6.Enabled = True
                Button7.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button100.Enabled = True
                Button200.Enabled = True
                Button500.Enabled = True
                Button1000.Enabled = True
                ButtonDot.Enabled = True
                Button0.Enabled = True
                Button00.Enabled = True
                ButtonDel.Enabled = True
                ButtonClear.Enabled = True
                ButtonOk.Enabled = True

                TextBoxMoney.Text = ""
                TextBoxChange.Text = ""

            End If

            If ButtonEmployee.BackColor = Color.Gray Then
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                LabelType.Text = "Employee"
            End If

            Try
                With SerialPort1
                    .PortName = "COM2"
                    .BaudRate = 9600
                    .DataBits = 8
                    .Parity = Parity.None
                    .StopBits = StopBits.One
                    .Handshake = Handshake.None
                End With

                If Not (SerialPort1.IsOpen = True) Then
                    SerialPort1.Open()
                End If

                SerialPort1.DiscardOutBuffer()

                SerialPort1.Write(displayClear, 0, 1)

                SerialPort1.Write(displayHome, 0, 1)
                SerialPort1.Write("Amount Due:" & vbCrLf & Convert.ToDecimal(LabelTotal.Text).ToString("0.00"))
                SerialPort1.Close()

            Catch ex As Exception
                'MsgBox(ex.Message, MessageBoxIcon.Warning)
            End Try

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Origin/Destination!", MessageBoxIcon.Information)
            ButtonEmployee.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
    End Sub

    Private Sub ButtonBaggage_Click(sender As Object, e As EventArgs) Handles ButtonBaggage.Click
        baggage.Show()

        baggage.ComboBoxBaggageWeight.Text = "-- Please Choose Baggage Weight --"
    End Sub

    'Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click
    '    If ButtonEdit.BackColor = Color.Gainsboro Then
    '        ButtonEdit.BackColor = Color.Gray
    '        TextBoxCardAmount.Enabled = True

    '    ElseIf ButtonEdit.BackColor = Color.Gray Then
    '        ButtonEdit.BackColor = Color.Gainsboro
    '        TextBoxCardAmount.Enabled = False

    '    End If
    'End Sub

    Private Sub CheckBoxPercentDiscount_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPercentDiscount.CheckedChanged
        If CheckBoxPercentDiscount.Checked = True Then
            TextBoxPercentDiscount.Enabled = True
        Else
            TextBoxPercentDiscount.Enabled = False
        End If
    End Sub

    Private Sub ButtonCard_Click(sender As Object, e As EventArgs) Handles ButtonCard.Click

        If ButtonCard.BackColor = Color.Gainsboro Then
            ButtonCard.BackColor = Color.Gray

            ButtonOrigin.Text = "Origin"
            ButtonOrigin.Enabled = False
            ButtonDestination.Text = "Destination"
            ButtonDestination.Enabled = False
            ComboBoxTime.Text = ""
            ComboBoxTime.Enabled = False
            TextBoxSeat.Text = ""
            ButtonSeat.Enabled = False
            ButtonBaggage.Enabled = False

            TextBoxCardAmount.Text = "20.00"
            LabelFare.Text = "0.00"
            TextBoxTripFare.Text = "0.00"
            TextBoxBaggageCost.Text = "0.00"
            TextBoxMoney.Text = ""
            TextBoxChange.Text = ""

            'for computing VAT
            Try
                LabelVATable.Text = (Math.Round(Val((TextBoxCardAmount.Text / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((TextBoxCardAmount.Text) - Val(LabelVATable.Text)), 2))

                LabelVATExempt.Text = "0.00"
                LabelDiscount.Text = "0.00"
                LabelTotal.Text = TextBoxCardAmount.Text
                'mainform.LabelPercent.Text = "0.00"
                LabelZeroRated.Text = "0.00"
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        ElseIf ButtonCard.BackColor = Color.Gray Then
            ButtonCard.BackColor = Color.Gainsboro

            ButtonOrigin.Enabled = True
            ButtonDestination.Enabled = True
            ComboBoxTime.Enabled = True
            ButtonSeat.Enabled = True
            ButtonBaggage.Enabled = True

            TextBoxCardAmount.Text = "0.00"
            TextBoxTripFare.Text = "0.00"
            TextBoxBaggageCost.Text = "0.00"
            TextBoxMoney.Text = ""
            TextBoxChange.Text = ""
            LabelVATable.Text = "0.00"
            LabelVAT.Text = "0.00"

            LabelVATExempt.Text = "0.00"
            LabelDiscount.Text = "0.00"
            LabelTotal.Text = ""
            'mainform.LabelPercent.Text = "0.00"
            LabelZeroRated.Text = "0.00"

        End If
    End Sub

    Private Sub TextBoxBarcodeTicket_TextChanged(sender As Object, e As EventArgs) Handles TextBoxBarcodeTicket.TextChanged
        'assigning value of barcode
        Try
            barcodeticket.Code = TextBoxBarcodeTicket.Text
        Catch ex As Exception
            MsgBox(ex.Message + "Barcode")
        End Try
    End Sub
End Class