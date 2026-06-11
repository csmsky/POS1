Imports System.IO
Imports System.Drawing.Printing
Imports System.IO.Ports
Imports WindowsApplication1.ConfigClass
Imports QRCoder
Imports System.Security.Cryptography
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports System.Net.Http
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Principal

Imports System.Diagnostics



Public Class mainform
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.
    'Dim connString As String = "Server=localhost;Database=amusement;Uid=root;Pwd=;"
    Inherits System.Windows.Forms.Form
    Public CurrentCustomerName As String = ""
    Dim total As Decimal = 0

    'for or_tbl id
    Dim randomString As String

    'for calling connection in App.config
    Dim strConn As String = FDEandD()


    'Get the CurrentTime and Date
    Dim dateAndTime As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

    Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps
    Dim station = AppConfigReader.sttn
    Dim serial = AppConfigReader.srl

    Dim barcode As BusinessRefinery.Barcode.PDF417 = New BusinessRefinery.Barcode.PDF417()
    Dim barcodeticket As BusinessRefinery.Barcode.PDF417 = New BusinessRefinery.Barcode.PDF417()

    'Printing OR and Preview
    Private WithEvents printInvoice As New PrintDocument()
    Private printPreviewDialog As New PrintPreviewDialog()

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

    'Dim todaysdate As String = Today.ToString("yyyy-MM-dd") modified
    ReadOnly Property todaysdate As String
        Get
            Return DateTime.Today.ToString("yyyy-MM-dd")
        End Get
    End Property


    Private _counter As SaleInvoiceCounter

    Dim cost As String

    Private _detailsForm As New InputCustomerDetails()
    Public rstCnt As Integer = 0
    Public voidRstCnt As Integer = 0

    'Public Sub OnThreadException(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
    '    MessageBox.Show(e.Exception.Message)
    'End Sub

    'Private Sub cardPolling_OnError(ByVal sender As Object, ByVal e As CardPollingErrorEventArg)
    '    MessageBox.Show(e.errorMessage)
    'End Sub

    Private Sub cardPolling_OnCardRemoved(ByVal sender As Object, ByVal e As CardPollingEventArg)
        'for clearing fields when card is removed
        'for clearing fields for another transaction

        LabelFare.Text = Nothing
        TextBoxTripFare.Text = "0.00"

        TextBoxChange.Clear()
        TextBoxMoney.Clear()

        ButtonRegularRide.Text = "Origin"
        'ButtonDestination.Text = "Destination"
        ButtonStudent.BackColor = Color.Gainsboro
        ButtonPWD.BackColor = Color.Gainsboro
        ButtonSenior.BackColor = Color.Gainsboro
        ButtonZeroRated.BackColor = Color.Gainsboro

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

            'card end transaction
            _pcscReader.endTransaction() 'dff

        Catch ex As Exception
        End Try
    End Sub
    Private Sub destination_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxUserID.Hide()

        'for making an E-journal Folder
        My.Computer.FileSystem.CreateDirectory("C:\E-Journal")

        'for taking the company logo from resources
        'PictureBoxCompanyLogo.Image = WindowsApplication1.My.Resources.Logo
        PictureBoxLogoReceipt.Image = WindowsApplication1.My.Resources.CompLogo
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
        PrintDocument1.DefaultPageSettings.Margins = margins

        ''THERMAL
        Dim papersize As New PaperSize("Custom", 280, 1000)
        '' ------------- For MMDA
        'Dim papersize As New PaperSize("Custom", 280, 470)

        ''DOT MATRIX
        'Dim papersize As New PaperSize("Custom", 255, 670)

        PrintDocument1.DefaultPageSettings.PaperSize = papersize


        Dim readerList() As String

        Try
            _pcscReader = New PscsReader
            _cardPolling = New CardPolling

            'register to event on card found
            AddHandler _cardPolling.OnCardFound, AddressOf cardPolling_OnCardFound

            'register to event on card remove
            AddHandler _cardPolling.OnCardRemoved, AddressOf cardPolling_OnCardRemoved

            'register to event on error
            'AddHandler _cardPolling.OnError, AddressOf cardPolling_OnError

            _cardPolling.StopPolling()

            'get all smart card reader connected to computer
            readerList = _pcscReader.getReaderList

            'ComboReaderNames.Items.Clear() //Experiment

            'If readerList.Length > 0 Then
            'ComboReaderNames.Items.AddRange(readerList)

            'ComboReaderNames.SelectedIndex = 0

            'Get the reader name for contactless (picc) and contact reader
            'For i As Integer = 0 To ComboReaderNames.Items.Count - 1
            '_cardPolling.add(ComboReaderNames.Items(i).ToString)
            'Next

            ' ButtonListReaders.Enabled = False

            'Else
            ' MessageBox.Show("No readers found.", "List Readers", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'End If

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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBoxMoney.AppendText("9")
    End Sub

    Private Sub Button100_Click(sender As Object, e As EventArgs) Handles Button100.Click
        TextBoxMoney.AppendText("100")
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

        EnableKeypadNumber()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim totalAmount As Decimal
        Dim moneyInput As Decimal

        If TextBoxMoney.Text = "" Then
            'MsgBox("Input Money or Payment Method.", MessageBoxIcon.Warning)
            MsgBox("Input Money or Payment Method.")
            EnableKeypadNumber()

        ElseIf Decimal.TryParse(LabelTotal.Text, totalAmount) AndAlso Decimal.TryParse(TextBoxMoney.Text, moneyInput) Then
            If totalAmount > moneyInput Then
                'MsgBox("Insufficient amount.", MsgBoxStyle.Exclamation)
                MsgBox("Insufficient amount.")
                TextBoxMoney.Clear()
                TextBoxChange.Clear()

                EnableKeypadNumber()

                Exit Sub
            Else
                'for computing change
                If ButtonZeroRated.BackColor = Color.Gainsboro Then
                    'TextBoxChange.Text = Convert.ToDecimal(Val(TextBoxMoney.Text) - Val(LabelTotal.Text)).ToString("0.00")
                    TextBoxChange.Text = (TextBoxMoney.Text - LabelTotal.Text).ToString("0.00")
                    Dim combination As String = TextBoxMoney.Text & ".00"
                    'secondscreen.LabelCashTendered.Text = TextBoxMoney.Text
                    secondscreen.LabelCashTendered.Text = combination
                    secondscreen.LabelChangeCustomerChange.Text = TextBoxChange.Text

                ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                    TextBoxChange.Text = Convert.ToDecimal(Val(TextBoxMoney.Text) - "0.00").ToString("0.00")
                    Dim combination As String = TextBoxMoney.Text & ".00"
                    'secondscreen.LabelCashTendered.Text = TextBoxMoney.Text
                    secondscreen.LabelCashTendered.Text = combination
                    secondscreen.LabelChangeCustomerChange.Text = TextBoxChange.Text

                End If

                Button12.Enabled = True

                disableKeypadNumber()

            End If
        End If

    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        'for deleting one character at a time
        If Len(TextBoxMoney.Text) > 1 Then
            TextBoxMoney.Text = Mid(TextBoxMoney.Text, 1, Len(TextBoxMoney.Text) - 1)
        Else
            TextBoxMoney.Text = ""
            EnableKeypadNumber()

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
        Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text)).ToString("0.00")

        '============================== EMPLOYEE ===================================================

        'for employee
        If ButtonEmployee.BackColor = Color.Gray Then

            auto()

            'for inserting transaction
            Dim qd As String = "INSERT INTO or_tbl(id, ticket_no, passenger_id, payment_date, payment_time, station_id, pos_id, user_id, type_of_transaction, upload) VALUES 
                                                  (@id, @ticket_no, @passenger_id, @payment_date, @payment_time, @station_id, @pos_id, @user_id, @type_of_transaction, @upload)"
            Dim cmd As New MySqlCommand(qd) With {.Connection = conn}

            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@id", randomString)
            cmd.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)

            cmd.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@payment_time", Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@station_id", LabelStationID.Text)
            cmd.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
            cmd.Parameters.AddWithValue("@user_id", LabelCashierID.Text)
            cmd.Parameters.AddWithValue("@type_of_transaction", LabelType.Text)
            cmd.Parameters.AddWithValue("@upload", "no")

            Try
                conn.Open()
                cmd.ExecuteReader()
                conn.Close()

                Try

                    conn.Close()

                    'inserting in audit_trail_tbl
                    Dim audit As String = "INSERT INTO audit_trail_tbl(pos_id, ticket_no, date, time, cashier, activity_performed, vatable, vat, vat_exempt, zero_rated, discount, total_amount, upload) VALUES 
                                                                          (@pos_id, @ticket_no, @date, @time, @cashier, @activity_performed, @vatable, @vat, @vat_exempt, @zero_rated, @discount, @total_amount, @upload)"
                    Dim cmdaudit As New MySqlCommand(audit) With {.Connection = conn}

                    cmdaudit.CommandType = CommandType.Text
                    cmdaudit.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                    cmdaudit.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
                    cmdaudit.Parameters.AddWithValue("@date", Today.ToString("yyyy-MM-dd"))
                    cmdaudit.Parameters.AddWithValue("@time", Now.ToString("HH:mm"))
                    cmdaudit.Parameters.AddWithValue("@cashier", LabelCashierID.Text)
                    cmdaudit.Parameters.AddWithValue("@activity_performed", "Employee Ticket Issued")
                    cmdaudit.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                    cmdaudit.Parameters.AddWithValue("@vat", LabelVAT.Text)
                    cmdaudit.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                    cmdaudit.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                    cmdaudit.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                    cmdaudit.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                    cmdaudit.Parameters.AddWithValue("@upload", "no")

                    Try
                        conn.Open()
                        cmdaudit.ExecuteReader()

                        conn.Close()

                        If (page_number > 1) Then page_number = 0

                        ''PRINT set print dialog something
                        ''Set print dialog
                        'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                        'PrintPreviewDialog1.Document = PrintDocument1

                        'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                        'PrintPreviewDialog1.ShowDialog()

                        ''TO PRINT IMMEDIATELY
                        PrintDocument1.Print()


                    Catch ex As Exception
                        MsgBox(ex.Message & "PRINTING", MessageBoxIcon.Warning)
                        conn.Close()
                    End Try

                    conn.Close()

                Catch ex As Exception
                    MsgBox(ex.Message & "AUDIT", MessageBoxIcon.Warning)
                    conn.Close()
                End Try

                conn.Close()

            Catch ex As Exception
                MsgBox(ex.Message & "try insert into or_tbl", MessageBoxIcon.Warning)
                conn.Close()
            End Try


            conn.Close()

            '============================== EMPLOYEE ===================================================

            '============================== OR ===================================================

            'ElseIf ButtonCard.BackColor = Color.Gainsboro And
            '  ButtonEmployee.BackColor = Color.Gainsboro Then

            'for calling auto (payment id)
            auto()

            'for combining vessel id and time
            Dim sched_ID As String
            conn.Open()
            'combo = ComboBoxTime.Text.Split("-")
            Dim comb1 = combo(1).ToString
            Dim comb = combo(0).ToString

            'for selecting sched id
            Dim cd As New MySqlCommand("SELECT sched_id FROM vesselsched_tbl WHERE @vessel_id = vessel_id and @departure_time = departure_time", conn)
            cd.CommandType = CommandType.Text
            cd.Parameters.AddWithValue("@vessel_id", comb)
            cd.Parameters.AddWithValue("@departure_time", comb1)
            Dim sda As New MySqlDataAdapter(cd)
            Dim dt As New DataTable
            cd.CommandTimeout = 0

            Try
                sda.Fill(dt)

                sched_ID = dt.Rows(0)(0).ToString
                conn.Close()

                'for inserting transaction
                'Dim qd As String = "INSERT INTO or_tbl(id, ticket_no, or_no, passenger_id, sched_id, seat_id, payment_date, payment_time, station_id, dest_id, pos_id, user_id, trip_fare, baggage_cost, card_amount, vatable, vat_exempt, zero_rated, vat, discount, total_amount, type_of_transaction, void_status, upload) VALUES 
                ' (@id, @ticket_no, @or_no, @passenger_id, @sched_id, (SELECT seat_id FROM seat_tbl WHERE vessel_id = '" & comb & "' AND seat_no = '" & "TextBoxSeat.Text" & "'), @payment_date, @payment_time, @station_id, @dest_id, @pos_id, @user_id, @trip_fare, @baggage_cost, @card_amount, @vatable, @vat_exempt, @zero_rated, @vat, @discount, @total_amount, @type_of_transaction, @void_status, @upload)"
                'Dim cmd As New MySqlCommand(qd) With {.Connection = conn}

                ' cmd.CommandType = CommandType.Text
                'cmd.Parameters.AddWithValue("@id", randomString)
                'cmd.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
                'cmd.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)

                'cmd.Parameters.AddWithValue("@sched_id", sched_ID)
                'cmd.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))
                'cmd.Parameters.AddWithValue("@payment_time", Now.ToString("HH:mm"))
                'cmd.Parameters.AddWithValue("@station_id", OriginID.Text)
                'cmd.Parameters.AddWithValue("@dest_id", DestinationID.Text)
                'cmd.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                'cmd.Parameters.AddWithValue("@user_id", LabelCashierID.Text)
                'cmd.Parameters.AddWithValue("@trip_fare", TextBoxTripFare.Text)

                'cmd.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                'cmd.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                'cmd.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                'cmd.Parameters.AddWithValue("@vat", LabelVAT.Text)
                'cmd.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                'cmd.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                'cmd.Parameters.AddWithValue("@type_of_transaction", LabelType.Text)
                'cmd.Parameters.AddWithValue("@void_status", "no")
                'cmd.Parameters.AddWithValue("@upload", "no")

                Try
                    conn.Open()
                    'cmd.ExecuteReader()
                    conn.Close()
                    'for updating seat
                    Try
                        conn.Open()
                        Dim cdd As New MySqlCommand("Update seat_tbl set seat_status = @seat_status,  updated = @updated WHERE vessel_id = @vesssel_id AND seat_no = @seat_no", conn)
                        cdd.CommandType = CommandType.Text
                        cdd.Parameters.AddWithValue("@seat_status", 1)
                        cdd.Parameters.AddWithValue("@updated", "yes")
                        cdd.Parameters.AddWithValue("@vesssel_id", comb)

                        Dim reader As MySqlDataReader

                        reader = cdd.ExecuteReader
                        conn.Close()

                        '==================================================APPENDED OR=============================================

                        Try

                            Dim filepath As String = "C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"
                            '' Dim filepath As String = "C:\E-Journal\" & "2019-11-14" & ".txt"


                            Dim sw As StreamWriter
                            '  Dim fs As FileStream


                            ' Dim fs As FileStream


                            If (Not File.Exists(filepath)) Then

                                sw = File.CreateText(filepath)
                                sw.WriteLine("           CAVITE SUPERFERRY")
                                sw.WriteLine("            PINOY CATAMARAN")
                                sw.WriteLine("          #17 SILAHIS STREET,")
                                sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                                sw.WriteLine("              PHILIPPINES")
                                sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                                sw.WriteLine("      POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                                sw.WriteLine("            MIN: XXXXXXXXXX")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Cashier: " & LabelCashierName.Text)
                                sw.WriteLine("Station ID: " & OriginID.Text)
                                sw.WriteLine("            OFFICIAL RECEIPT")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                                sw.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                                sw.WriteLine("----------------------------------------")

                                sw.WriteLine("VESSEL ID                           " & comb)
                                sw.WriteLine("SCHEDULE                            " & comb1)

                                sw.WriteLine("PT. OF ORIGIN                       " & ButtonRegularRide.Text)
                                sw.WriteLine("TRIP FARE                           " & TextBoxTripFare.Text)

                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("TOTAL AMOUNT                        " & LabelTotal.Text)
                                sw.WriteLine("CASH                                " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                                sw.WriteLine("CHANGE                              " & TextBoxChange.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("(V) VATable Sales                   " & LabelVATable.Text)
                                sw.WriteLine("VAT Amount                          " & LabelVAT.Text)
                                sw.WriteLine("(X) VAT-Exempt Sales                " & LabelVATExempt.Text)
                                If ButtonZeroRated.BackColor = Color.Gainsboro Then
                                    sw.WriteLine("(Z) Zero-Rated Sales                " & LabelZeroRated.Text)

                                ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                                    sw.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                                End If
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Discount                            " & LabelDiscount.Text)
                                sw.WriteLine("                                    " & LabelType.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Name: _________________________________")
                                sw.WriteLine("Address: ______________________________")
                                sw.WriteLine("_______________________________________")
                                sw.WriteLine("ID #: _________________________________")
                                sw.WriteLine("OSCA/SC ID #: _________________________")
                                sw.WriteLine("PWD ID #: _____________________________")
                                sw.WriteLine("TIN #: ________________________________")
                                sw.WriteLine("Bus Style: ____________________________")
                                sw.WriteLine("Signature: ____________________________")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("               VINTATECH")
                                sw.WriteLine("        Unit 507 F&L Bldg. 2211")
                                sw.WriteLine("     Commonwealth, Holy Spirit 1127")
                                sw.WriteLine("              Quezon City")
                                sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                                sw.WriteLine("        Accred. No.: XXXXXXXXXXX")
                                'sw.WriteLine("        Date Issued: XX/XX/XXXX") MODIFIED
                                sw.WriteLine("        Date Issued: " & DateTime.Today.ToString("MM/dd/yyyy"))

                                sw.WriteLine("        Valid Until: XX/XX/XXXX")
                                sw.WriteLine("          PTU No.: XXXXXXXXX")
                                'sw.WriteLine("        Date Issued: XX/XX/XXXX") MODIFIED
                                sw.WriteLine("        Date Issued: " & DateTime.Today.ToString("MM/dd/yyyy"))

                                sw.WriteLine("        Valid Until: XX/XX/XXXX")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                                sw.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                                sw.WriteLine("         OF THE PERMIT TO USE.")
                                sw.WriteLine("")
                                sw.WriteLine("")
                                sw.WriteLine("")

                            Else
                                sw = File.AppendText(filepath)
                                sw.WriteLine("           CAVITE SUPERFERRY")
                                sw.WriteLine("            PINOY CATAMARAN")
                                sw.WriteLine("          #17 SILAHIS STREET,")
                                sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                                sw.WriteLine("              PHILIPPINES")
                                sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                                sw.WriteLine("      POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                                sw.WriteLine("            MIN: XXXXXXXXXX")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Cashier: " & LabelCashierName.Text)
                                sw.WriteLine("Station ID: " & OriginID.Text)
                                sw.WriteLine("            OFFICIAL RECEIPT")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                                sw.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("VESSEL ID                           " & comb)
                                sw.WriteLine("SCHEDULE                            " & comb1)

                                sw.WriteLine("PT. OF ORIGIN                       " & ButtonRegularRide.Text)
                                'sw.WriteLine("PT. OF DESTINATION                  " & ButtonDestination.Text)
                                sw.WriteLine("TRIP FARE                           " & TextBoxTripFare.Text)

                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("TOTAL AMOUNT                        " & LabelTotal.Text)
                                sw.WriteLine("CASH                                " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                                sw.WriteLine("CHANGE                              " & TextBoxChange.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("(V) VATable Sales                   " & LabelVATable.Text)
                                sw.WriteLine("VAT Amount                          " & LabelVAT.Text)
                                sw.WriteLine("(X) VAT-Exempt Sales                " & LabelVATExempt.Text)
                                If ButtonZeroRated.BackColor = Color.Gainsboro Then
                                    sw.WriteLine("(Z) Zero-Rated Sales                " & LabelZeroRated.Text)

                                ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                                    sw.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                                End If
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Discount                            " & LabelDiscount.Text)
                                sw.WriteLine("                                    " & LabelType.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Name: _________________________________")
                                sw.WriteLine("Address: ______________________________")
                                sw.WriteLine("_______________________________________")
                                sw.WriteLine("ID #: _________________________________")
                                sw.WriteLine("OSCA/SC ID #: _________________________")
                                sw.WriteLine("PWD ID #: _____________________________")
                                sw.WriteLine("TIN #: ________________________________")
                                sw.WriteLine("Bus Style: ____________________________")
                                sw.WriteLine("Signature: ____________________________")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("               VINTATECH")
                                sw.WriteLine("        Unit 507 F&L Bldg. 2211")
                                sw.WriteLine("     Commonwealth, Holy Spirit 1127")
                                sw.WriteLine("              Quezon City")
                                sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                                sw.WriteLine("        Accred. No.: XXXXXXXXXXX")
                                'sw.WriteLine("        Date Issued: XX/XX/XXXX") MODIFIED
                                sw.WriteLine("        Date Issued: " & DateTime.Today.ToString("MM/dd/yyyy"))

                                sw.WriteLine("        Valid Until: XX/XX/XXXX")
                                sw.WriteLine("          PTU No.: XXXXXXXXX")
                                'sw.WriteLine("        Date Issued: XX/XX/XXXX") MODIFIED
                                sw.WriteLine("        Date Issued: " & DateTime.Today.ToString("MM/dd/yyyy"))

                                sw.WriteLine("        Valid Until: XX/XX/XXXX")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                                sw.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                                sw.WriteLine("         OF THE PERMIT TO USE.")
                                sw.WriteLine("")
                                sw.WriteLine("")
                                sw.WriteLine("")

                            End If


                            sw.Close()

                            Dim fillInfo As FileInfo

                            fillInfo = New FileInfo(filepath)
                            If fillInfo.Exists = True Then
                                fillInfo.Attributes = FileAttributes.Encrypted
                            End If

                        Catch ex As Exception
                            MsgBox(ex.Message, "Error writing to log file.")
                        End Try

                        '==================================================APPENDED OR=============================================

                        'For inserting on ejournal_tbl
                        Try
                            'For creating a text file
                            Dim file As System.IO.StreamWriter

                            file = My.Computer.FileSystem.OpenTextFileWriter("C: \E-Journal\" & TextBoxBarcode.Text & ".txt", False)

                            file.WriteLine("           CAVITE SUPERFERRY")
                            file.WriteLine("            PINOY CATAMARAN")
                            file.WriteLine("          #17 SILAHIS STREET,")
                            file.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                            file.WriteLine("              PHILIPPINES")
                            file.WriteLine("      VAT REG TIN: 745-993-747-000")
                            file.WriteLine("      POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                            file.WriteLine("            MIN: XXXXXXXXXX")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("Cashier: " & LabelCashierName.Text)
                            file.WriteLine("Station ID: " & OriginID.Text)
                            file.WriteLine("            OFFICIAL RECEIPT")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                            file.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                            file.WriteLine("Reset Counter: " & rstCnt.ToString())
                            file.WriteLine("----------------------------------------")

                            file.WriteLine("VESSEL ID                           " & comb)
                            file.WriteLine("SCHEDULE                            " & comb1)

                            file.WriteLine("PT. OF ORIGIN                       " & ButtonRegularRide.Text)

                            file.WriteLine("TRIP FARE                           " & TextBoxTripFare.Text)

                            file.WriteLine("----------------------------------------")
                            file.WriteLine("TOTAL AMOUNT                        " & LabelTotal.Text)
                            file.WriteLine("CASH                                " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                            file.WriteLine("CHANGE                              " & TextBoxChange.Text)
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("(V) VATable Sales                   " & LabelVATable.Text)
                            file.WriteLine("VAT Amount                          " & LabelVAT.Text)
                            file.WriteLine("(X) VAT-Exempt Sales                " & LabelVATExempt.Text)
                            If ButtonZeroRated.BackColor = Color.Gainsboro Then
                                file.WriteLine("(Z) Zero-Rated Sales                " & LabelZeroRated.Text)

                            ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                                file.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                            End If
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("Discount                            " & LabelDiscount.Text)
                            file.WriteLine("                                    " & LabelType.Text)
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("Name: _________________________________")
                            file.WriteLine("Address: ______________________________")
                            file.WriteLine("_______________________________________")
                            file.WriteLine("ID #: _________________________________")
                            file.WriteLine("OSCA/SC ID #: _________________________")
                            file.WriteLine("PWD ID #: _____________________________")
                            file.WriteLine("TIN #: ________________________________")
                            file.WriteLine("Bus Style: ____________________________")
                            file.WriteLine("Signature: ____________________________")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("               VINTATECH")
                            file.WriteLine("        Unit 507 F&L Bldg. 2211")
                            file.WriteLine("     Commonwealth, Holy Spirit 1127")
                            file.WriteLine("              Quezon City")
                            file.WriteLine("      VAT REG TIN: 745-993-747-000")
                            file.WriteLine("        Accred. No.: XXXXXXXXXXX")
                            'file.WriteLine("        Date Issued: XX/XX/XXXX") MODIFIED
                            file.WriteLine("        Date Issued: " & DateTime.Today.ToString("MM/dd/yyyy"))

                            file.WriteLine("        Valid Until: XX/XX/XXXX")
                            file.WriteLine("          PTU No.: XXXXXXXXX")
                            'file.WriteLine("        Date Issued: XX/XX/XXXX") MODIFIED
                            file.WriteLine("        Date Issued: " & DateTime.Today.ToString("MM/dd/yyyy"))

                            file.WriteLine("        Valid Until: XX/XX/XXXX")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                            file.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                            file.WriteLine("         OF THE PERMIT TO USE.")

                            '----------- FOR MMDA
                            'file.WriteLine("METROPOLITAN MANILA DEVELOPMENT AUTHORITY")
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine("Cashier: " & LabelCashierName.Text)
                            'file.WriteLine("Station ID: " & OriginID.Text)
                            'file.WriteLine("               DEMO TICKET")
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                            'file.WriteLine("OR #: " & TextBoxBarcode.Text)
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine("NAME                " & fname.Text & " " & lname.Text)
                            'file.WriteLine("PT. OF ORIGIN              " & ButtonOrigin.Text)
                            'file.WriteLine("PT. OF DESTINATION     " & ButtonDestination.Text)
                            'file.WriteLine("TRIP FARE                          " & LabelFare.Text)
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine("TOTAL AMOUNT                       " & TextBoxPayment.Text)
                            'file.WriteLine("CASH                               " & TextBoxMoney.Text & ".00")
                            'file.WriteLine("CHANGE                             " & TextBoxChange.Text)
                            'file.WriteLine("Discount                           " & LabelDiscount.Text)
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine("           A DEMO PROJECT BY")
                            'file.WriteLine("               VINTATECH")
                            '----------- FOR MMDA
                            file.Close()

                            'For uploading a text file

                            Dim rawData() As Byte = IO.File.ReadAllBytes("C:\E-Journal\" & TextBoxBarcode.Text & ".txt")

                            Dim qdEJ As String = "INSERT INTO ejournal_tbl(id, pos_id, cashier_name, station, payment_date, payment_time, ticket_no, or_no, passenger_name, vessel_id, schedule, seat_no, origin, destination, trip_fare, baggage_cost, card_amount, total_amount, cash, change_amount, discount, vatable, vat_exempt, zero_rated, vat, type_of_transaction, void_status, text_file, upload)
                                  values (@id, @pos_id, @cashier_name, @station, @payment_date, @payment_time, @ticket_no, @or_no, @passenger_name, @vessel_id, @schedule, @seat_no, @origin, @destination, @trip_fare, @baggage_cost, @card_amount, @total_amount, @cash, @change_amount, @discount, @vatable, @vat_exempt, @zero_rated, @vat, @type_of_transaction, @void_status, @text_file, @upload)"
                            Dim cmdEJ As New MySqlCommand(qdEJ) With {.Connection = conn}

                            cmdEJ.CommandType = CommandType.Text
                            cmdEJ.Parameters.AddWithValue("@id", TextBoxBarcode.Text)
                            cmdEJ.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                            cmdEJ.Parameters.AddWithValue("@cashier_name", LabelCashierName.Text)
                            cmdEJ.Parameters.AddWithValue("@station", OriginID.Text)
                            cmdEJ.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))
                            cmdEJ.Parameters.AddWithValue("@payment_time", Now.ToString("HH:mm"))
                            cmdEJ.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
                            cmdEJ.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)

                            cmdEJ.Parameters.AddWithValue("@vessel_id", comb)
                            cmdEJ.Parameters.AddWithValue("@schedule", comb1)

                            cmdEJ.Parameters.AddWithValue("@origin", ButtonRegularRide.Text)

                            cmdEJ.Parameters.AddWithValue("@trip_fare", TextBoxTripFare.Text)

                            cmdEJ.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                            cmdEJ.Parameters.AddWithValue("@cash", Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                            cmdEJ.Parameters.AddWithValue("@change_amount", TextBoxChange.Text)
                            cmdEJ.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                            cmdEJ.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                            cmdEJ.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                            cmdEJ.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                            cmdEJ.Parameters.AddWithValue("@vat", LabelVAT.Text)
                            cmdEJ.Parameters.AddWithValue("@type_of_transaction", LabelType.Text)
                            cmdEJ.Parameters.AddWithValue("@void_status", "no")
                            cmdEJ.Parameters.AddWithValue("@text_file", rawData)
                            cmdEJ.Parameters.AddWithValue("@upload", "no")

                            Try
                                conn.Open()
                                cmdEJ.ExecuteReader()

                                conn.Close()

                                'inserting in audit_trail_tbl
                                Dim audit As String = "INSERT INTO audit_trail_tbl(pos_id, ticket_no, or_no, date, time, cashier, activity_performed, vatable, vat, vat_exempt, zero_rated, discount, total_amount, upload) VALUES 
                                                      (@pos_id, @ticket_no, @or_no, @date, @time, @cashier, @activity_performed, @vatable, @vat, @vat_exempt, @zero_rated, @discount, @total_amount, @upload)"
                                Dim cmdaudit As New MySqlCommand(audit) With {.Connection = conn}

                                cmdaudit.CommandType = CommandType.Text
                                cmdaudit.Parameters.AddWithValue("@pos_id", TextBoxBarcode.Text)
                                cmdaudit.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
                                cmdaudit.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)
                                cmdaudit.Parameters.AddWithValue("@date", Today.ToString("yyyy-MM-dd"))
                                cmdaudit.Parameters.AddWithValue("@time", Now.ToString("HH:mm"))
                                cmdaudit.Parameters.AddWithValue("@cashier", LabelCashierID.Text)
                                cmdaudit.Parameters.AddWithValue("@activity_performed", "Fare Sales Transaction")
                                cmdaudit.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                                cmdaudit.Parameters.AddWithValue("@vat", LabelVAT.Text)
                                cmdaudit.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                                cmdaudit.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                                cmdaudit.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                                cmdaudit.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                                cmdaudit.Parameters.AddWithValue("@upload", "no")


                                Try
                                    conn.Open()
                                    cmdaudit.ExecuteReader()

                                    conn.Close()

                                    'ACCUMULATED AMOUNT TBL
                                    'for fetching value
                                    Dim ccmd As New MySqlCommand
                                    Dim ddt As New DataTable()
                                    Dim adapter As New MySqlDataAdapter

                                    'select if cashier and date is already inserted in accumulated_amount_tbl
                                    Try
                                        ccmd.CommandText = "SELECT * FROM accumulated_amount_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'"
                                        adapter.SelectCommand = ccmd
                                        adapter.SelectCommand.Connection = conn
                                        adapter.Fill(ddt)

                                        If ddt.Rows.Count > 0 Then
                                            conn.Open()

                                            Try
                                                'for updating accumulated_amount_tbl with new inserted data
                                                Dim ccdd As New MySqlCommand("UPDATE accumulated_amount_tbl SET pos_id = '" & LabelPOSno.Text & "', 
                                                        end_or_no = '" & TextBoxBarcode.Text & "',  
                                                        no_of_transaction = (SELECT COUNT(or_no) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'), 
                                                        sales = (SELECT SUM(vatable + vat_exempt + zero_rated + vat + total_amount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        discount = (SELECT SUM(discount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vatable = (SELECT SUM(vatable) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vat_exempt = (SELECT SUM(vat_exempt) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vat = (SELECT SUM(vat) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        zero_rated = (SELECT SUM(zero_rated) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        grand_total = (SELECT (SELECT SUM(vatable + vat_exempt + zero_rated + vat + total_amount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no') - (SELECT IFNULL(SUM(NULLIF(vatable + vat_exempt + zero_rated + vat + total_amount, 0)), 0) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'yes')),
                                                        updated = 'yes'
                                                        WHERE user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'", conn)

                                                Dim readerr As MySqlDataReader

                                                readerr = ccdd.ExecuteReader
                                                readerr.Close()
                                                conn.Close()

                                                'for dual printing
                                                If (page_number > 1) Then page_number = 0

                                                ''PRINT set print dialog something
                                                ''Set print dialog
                                                'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                                                'PrintPreviewDialog1.Document = PrintDocument1

                                                'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                                                'PrintPreviewDialog1.ShowDialog()

                                                ''TO PRINT IMMEDIATELY
                                                PrintDocument1.Print()


                                            Catch ex As Exception
                                                MsgBox(ex.Message & "UPDATE", MessageBoxIcon.Warning)
                                                conn.Close()
                                            End Try
                                        Else

                                            conn.Close()

                                            'for inserting accumulated amounts
                                            ''TO EDIT
                                            Dim AA As String = "INSERT INTO accumulated_amount_tbl (user_id, 
                                                                                                        pos_id, 
                                                                                                        payment_date, 
                                                                                                        begin_or_no, 
                                                                                                        end_or_no, 
                                                                                                        no_of_transaction, 
                                                                                                        no_of_void, 
                                                                                                        sales, 
                                                                                                        discount, 
                                                                                                        vatable, 
                                                                                                        vat_exempt, 
                                                                                                        vat, 
                                                                                                        zero_rated, 
                                                                                                        z_counter, 
                                                                                                        begin_void_no, 
                                                                                                        end_void_no, 
                                                                                                        void, 
                                                                                                        grand_total, 
                                                                                                        printed, 
                                                                                                        upload)
                                                                                                Select '" & LabelCashierID.Text & "', 
                                                                                                       '" & LabelPOSno.Text & "', 
                                                                                                       '" & todaysdate & "', 
                                                                                                       MIN(or_no), 
                                                                                                       MAX(or_no), 
                                                                                                       COUNT(or_no), 
                                                                                                       0, 
                                                                                                       SUM(vatable + vat_exempt + zero_rated + vat + total_amount), 
                                                                                                       SUM(discount), 
                                                                                                       SUM(vatable), 
                                                                                                       SUM(vat_exempt), 
                                                                                                       SUM(vat), 
                                                                                                       SUM(zero_rated), 
                                                                                                       0, 
                                                                                                       0, 
                                                                                                       0, 
                                                                                                       '0.00', 
                                                                                                       SUM(vatable + vat_exempt + zero_rated + vat + total_amount), 
                                                                                                       'no', 
                                                                                                       'no' 
                                                                                                       FROM or_tbl WHERE user_id = '" & LabelCashierID.Text & "' and payment_date = '" & todaysdate & "'"
                                            Dim cmmd As New MySqlCommand(AA) With {.Connection = conn}

                                            Try
                                                conn.Open()
                                                cmmd.ExecuteReader()

                                                'for dual printing
                                                If (page_number > 1) Then page_number = 0

                                                ''PRINT set print dialog something
                                                ''Set print dialog
                                                'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                                                'PrintPreviewDialog1.Document = PrintDocument1

                                                'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                                                'PrintPreviewDialog1.ShowDialog()

                                                ''TO PRINT IMMEDIATELY
                                                PrintDocument1.Print()


                                            Catch ex As Exception
                                                MsgBox(ex.Message & "insert accumulated", MessageBoxIcon.Warning)
                                                conn.Close()
                                            End Try

                                        End If

                                    Catch ex As Exception
                                        MsgBox(ex.Message & "LAST", MessageBoxIcon.Warning)
                                        conn.Close()
                                    End Try

                                    conn.Close()

                                Catch ex As Exception
                                    MsgBox(ex.Message & "AUDIT", MessageBoxIcon.Warning)
                                    conn.Close()
                                End Try

                            Catch ex As Exception
                                MsgBox(ex.Message & "try ejournal insert", MessageBoxIcon.Warning)
                                conn.Close()
                            End Try

                            conn.Close()

                            'MessageBox.Show("File Inserted into database successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                            'JIM ----DELETED TEMPORARY FILE AFTER SAVING IT TO DATABASE
                            My.Computer.FileSystem.DeleteFile("C:\E-Journal\" & TextBoxBarcode.Text & ".txt")

                        Catch ex As Exception
                            MsgBox(ex.Message & "try seat update", MessageBoxIcon.Warning)
                            conn.Close()
                        End Try

                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & "try updating seat", MessageBoxIcon.Warning)
                        conn.Close()
                    End Try

                Catch ex As Exception
                    MsgBox(ex.Message & "try insert into or_tbl", MessageBoxIcon.Warning)
                    conn.Close()
                End Try
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message & "sched_id", MessageBoxIcon.Warning)
            End Try
            conn.Close()
        End If


    End Sub

    Private Sub auto()
        Dim seq As Long = 0
        rstCnt = 0

        conn.Open()
        Dim qd As String = "SELECT or_no, rst_cnt FROM or_tbl WHERE pos_id = '" & LabelPOSno.Text & "' ORDER BY id DESC LIMIT 1"
        Dim cmd As New MySqlCommand(qd) With {.Connection = conn}
        Dim rdr As MySqlDataReader = cmd.ExecuteReader()
        If rdr.Read() Then
            If Not IsDBNull(rdr("or_no")) Then
                Dim orNoStr As String = rdr("or_no").ToString()
                If Long.TryParse(orNoStr, seq) Then
                    ' Successfully parsed exact Long, preventing precision loss
                Else
                    ' Fallback for scientific notation corrupted strings like "000000000001E+16"
                    Dim parsedVal As Double = 0
                    If Double.TryParse(orNoStr, parsedVal) Then
                        seq = Convert.ToInt64(parsedVal)
                    End If
                End If
            End If
            If Not IsDBNull(rdr("rst_cnt")) Then
                rstCnt = Convert.ToInt32(rdr("rst_cnt").ToString())
            End If
        End If
        rdr.Close()

        If seq >= 9999999999999999 Then
            seq = 1
            rstCnt += 1
        Else
            seq += 1
        End If

        TextBoxBarcode.Text = seq.ToString("D16")

        Dim lblReset As Control = Me.Controls.Find("LabelReset", True).FirstOrDefault()
        If lblReset IsNot Nothing Then
            lblReset.Text = rstCnt.ToString()
        End If

        conn.Close()

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
        'for displaying current day, datetime
        LabelDateTime.Text = DateTime.Now.ToString("ddd MM/dd/yyyy HH:mm:ss")
    End Sub

    Private Sub TextBoxUserID_TextChanged(sender As Object, e As EventArgs) Handles TextBoxUserID.TextChanged
        'for Manager Access
        Dim cmd As New MySqlCommand
        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter

        Try
            conn.ConnectionString = (strConn)
            conn.Open()
            cmd.CommandText = "SELECT user_id FROM user_registration_tbl WHERE (user_id = '" & TextBoxUserID.Text & "') AND (CAST(AES_DECRYPT(position, 'strdjnltmyp') AS CHAR(50)) = 'Manager')"

            adapter.SelectCommand = cmd
            adapter.SelectCommand.Connection = conn
            adapter.Fill(dt)

            If dt.Rows.Count > 0 Then
                ButtonReprintTicket.Enabled = True
                ButtonVoid.Enabled = True
                ButtonStudent.Enabled = True
                ButtonPWD.Enabled = True
                ButtonZeroRated.Enabled = True
                conn.Close()
            Else
                ButtonReprintTicket.Enabled = False
                ButtonVoid.Enabled = False
                ButtonStudent.Enabled = False
                ButtonPWD.Enabled = False
                ButtonZeroRated.Enabled = False
                conn.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub ButtonBaggage_Click(sender As Object, e As EventArgs)
        baggage.Show()

        'List of cashier
        Try
            Dim adapter As New MySqlDataAdapter("SELECT baggage_weight AS weight, baggage_cost AS cost FROM baggage_tbl", conn)
            Dim table As New DataTable()

            adapter.Fill(table)
            baggage.ComboBoxBaggageWeight.DataSource = table
            baggage.ComboBoxBaggageWeight.Text = "-- Please Choose Baggage Weight --"
            baggage.ComboBoxBaggageWeight.ValueMember = "cost"
            baggage.ComboBoxBaggageWeight.DisplayMember = "weight"
        Catch ex As Exception
            MsgBox(ex.Message)
            baggage.ComboBoxBaggageWeight.DataSource = Nothing
        End Try

        baggage.ComboBoxBaggageWeight.Text = "-- Please Choose Baggage Weight --"

        ' add the handler after populating the ComboBox to avoid unwanted firing of the event...
        'AddHandler baggage.ComboBoxBaggageWeight.SelectedIndexChanged, AddressOf ComboBoxBaggageWeight_SelectedIndexChanged
    End Sub

    Private Sub ButtonVoid_Click(sender As Object, e As EventArgs) Handles ButtonVoid.Click
        voidtransaction.Show()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ButtonLogout_Click(sender As Object, e As EventArgs) Handles ButtonLogout.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Do you want to LOGOUT?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If result = DialogResult.OK Then
                conn.Open()

                Dim DateNow = DateTime.Now.ToString("yyyyMMdd")
                Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                    cmd.Parameters.AddWithValue("p_userid", TextBoxUserID.Text)
                    cmd.Parameters.AddWithValue("p_username", LabelCashierName.Text)
                    cmd.Parameters.AddWithValue("p_approvedby", "")
                    cmd.Parameters.AddWithValue("p_activity_performed", "LOGOUT")
                    cmd.Parameters.AddWithValue("p_module", "Ticket Sales")
                    cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-LOGOUT SESSION")
                    cmd.Parameters.AddWithValue("p_remarks", "LOGOUT SESSION")
                    cmd.ExecuteNonQuery()
                End Using

                Dim cdd As New MySqlCommand("Update userlog_tbl set date_logout = '" & Today.ToString("yyyy-MM-dd") & "', time_logout = '" & Now.ToString("HH:mm:ss") & "', updated = 'yes' WHERE userlog_id = '" & LabelLoginID.Text & "' and user_id = '" & LabelCashierID.Text & "'", conn)
                Dim reader As MySqlDataReader

                reader = cdd.ExecuteReader
                conn.Close()

                Try
                    Me.Close()
                    reprint.Close()
                    voidtransaction.Close()
                    baggage.Close()
                    voidtransaction.Close()
                    login.TextBoxUserID.Text = ""
                    login.LabelName.Text = ""
                    login.LabelPosition.Text = ""
                    login.PictureBoxFace.Image = Nothing
                    login.LabelHello.Hide()
                    login.Show()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            ElseIf result = DialogResult.Cancel Then
            End If

            'LOCK FOLDER
            Dim folderPath As String = "C:\E-Journal"
            LockFolder(folderPath)

        Catch ex As Exception
            MsgBox("Logout Error: " & ex.Message, MsgBoxStyle.Critical, "System Error")
            'MsgBox("Something Error in LOGOUT!")
        End Try
    End Sub

    Private Sub ButtonStudent_Click(sender As Object, e As EventArgs) Handles ButtonStudent.Click
        Dim or_no As String = TextBoxBarcode.Text
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

            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            If ButtonStudent.BackColor = Color.Gainsboro Then
                ButtonStudent.BackColor = Color.Cyan
                LabelType.Text = "Student"

                Dim PercentDiscount = 0.5D
                Dim introDiscount = 100

                'For Percent Discount
                'Dim introDiscountAmount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                'Dim amountDue As Decimal = Math.Round(sumpayment * PercentDiscount, 2)

                'For Less 100php
                Dim introDiscountAmount As Decimal = CDec(TextBoxQty.Text) * introDiscount
                Dim amountDue As Decimal = Math.Round(sumpayment - introDiscountAmount, 2)
                Dim vatable As Decimal = Math.Round(amountDue / 1.12D, 2)
                Dim vat As Decimal = Math.Round(amountDue - vatable, 2)

                'The BIR COMPLIANCE COMPUTATION
                'Dim Bvatable As Decimal = Math.Round(sumpayment / 1.12D, 2)
                'Dim Bvat As Decimal = Math.Round(sumpayment - Bvatable, 2)
                'Dim BintroDiscountAmount As Decimal = CDec(TextBoxQty.Text) * introDiscount
                'Dim BamountDue As Decimal = Math.Round(sumpayment - introDiscountAmount, 2)

                'BIR INCLUSIVE VAT
                'LabelVATable.Text = Bvatable.ToString("0.00")
                'LabelVAT.Text = Bvat.ToString("0.00")

                'For total Due
                LabelQtyPrice.Text = sumpayment.ToString("0.00")
                LabelTotal.Text = amountDue.ToString("0.00")

                'For Discount
                LabelDiscount.Text = introDiscountAmount.ToString("0.00")

                'Existing Exlusive VAT
                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")

                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                ' For Customer View
                secondscreen.PaymentDue.Text = LabelTotal.Text

            ElseIf ButtonStudent.BackColor = Color.Cyan Then
                ButtonStudent.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))

                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                LabelDiscount.Text = "0.00"
                LabelQtyPrice.Text = "0.00"

                LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text
                TextBoxPercentDiscount.Text = ""
            End If

            If ButtonStudent.BackColor = Color.Cyan Then
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonSoloParent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonNAAC.BackColor = Color.Gainsboro
                ButtonDiplomat.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Student"

            End If

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Ride!", MessageBoxIcon.Information)
            ButtonStudent.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"

        End Try
        _detailsForm.SetTransactionDetails("Student", or_no)
        ' Open popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True
        End If

    End Sub

    Private Sub ButtonPWD_Click(sender As Object, e As EventArgs) Handles ButtonPWD.Click
        Dim or_no As String = TextBoxBarcode.Text
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

            Dim PercentDiscount = 0.2D
            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            If ButtonPWD.BackColor = Color.Gainsboro Then
                ButtonPWD.BackColor = Color.Cyan
                LabelType.Text = "PWD(20%)"

                'Dim discount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                'Dim amountDue As Decimal = Math.Round(sumpayment - discount, 2)
                'Dim vatExempt1 As Decimal = Math.Round(amountDue / 1.12D, 2)
                'Dim comp As Decimal = Math.Round(amountDue - vatExempt1, 2)
                'Dim vatExempt As Decimal = Math.Round(vatExempt1 + comp, 2)

                'EXISTING COMPUTATION
                ''TextBoxPercentDiscount.Text = PercentDiscount
                'LabelDiscount.Text = discount.ToString("0.00")
                'LabelTotal.Text = amountDue.ToString("0.00")
                'secondscreen.PaymentDue.Text = LabelTotal.Text

                'LabelLessVat.Text = "0.00"
                'LabelVATExempt.Text = vatExempt.ToString("0.00")

                'BIR COMPLIANCE INCLUSIVE VAT
                Dim BvatExempt As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim Bdiscount As Decimal = Math.Round(BvatExempt * PercentDiscount, 2)
                Dim BamountDue As Decimal = Math.Round(BvatExempt - Bdiscount, 2)
                Dim Blessvat As Decimal = Math.Round(sumpayment - BvatExempt, 2)

                'BIR COMPLIANCE
                LabelDiscount.Text = Bdiscount.ToString("0.00")
                LabelTotal.Text = BamountDue.ToString("0.00")
                LabelVATExempt.Text = BvatExempt.ToString("0.00")
                LabelLessVat.Text = Blessvat.ToString("0.00")

                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                LabelZeroRated.Text = "0.00"

            ElseIf ButtonPWD.BackColor = Color.Cyan Then
                ButtonPWD.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                Dim vatable As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim vat As Decimal = Math.Round(sumpayment - vatable, 2)

                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")

                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                LabelDiscount.Text = "0.00"

                LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text
                TextBoxPercentDiscount.Text = ""
            End If

            If ButtonPWD.BackColor = Color.Cyan Then
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonSoloParent.BackColor = Color.Gainsboro
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonNAAC.BackColor = Color.Gainsboro
                ButtonDiplomat.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "PWD(20%)"

            End If

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Ride!", MessageBoxIcon.Information)
            ButtonPWD.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
        _detailsForm.SetTransactionDetails("PWD(20%)", or_no)
        ' Open popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True
        End If
    End Sub

    Private Sub ButtonSenior_Click(sender As Object, e As EventArgs) Handles ButtonSenior.Click
        Dim or_no As String = TextBoxBarcode.Text
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

            Dim PercentDiscount = 0.2D
            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            If ButtonSenior.BackColor = Color.Gainsboro Then
                ButtonSenior.BackColor = Color.Cyan
                LabelType.Text = "Senior(20%)"

                'EXISTING COMPUTATION EXCLUSIVE VAT
                'Dim discount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                'Dim amountDue As Decimal = Math.Round(sumpayment - discount, 2)
                'Dim vatExempt As Decimal = Math.Round(amountDue / 1.12D, 2)
                'Dim lessvat As Decimal = Math.Round(amountDue - vatExempt, 2)

                'BIR COMPLIANCE INCLUSIVE VAT
                Dim BvatExempt As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim Bdiscount As Decimal = Math.Round(BvatExempt * PercentDiscount, 2)
                Dim BamountDue As Decimal = Math.Round(BvatExempt - Bdiscount, 2)
                Dim Blessvat As Decimal = Math.Round(sumpayment - BvatExempt, 2)

                'BIR COMPLIANCE
                LabelDiscount.Text = Bdiscount.ToString("0.00")
                LabelTotal.Text = BamountDue.ToString("0.00")
                LabelVATExempt.Text = BvatExempt.ToString("0.00")
                LabelLessVat.Text = Blessvat.ToString("0.00")

                'EXISTING IMPLEMENTATION
                ''TextBoxPercentDiscount.Text = PercentDiscount
                'LabelDiscount.Text = discount.ToString("0.00")
                'LabelTotal.Text = amountDue.ToString("0.00")
                'LabelLessVat.Text = "0.00"
                'LabelVATExempt.Text = vatExempt.ToString("0.00")

                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                'secondscreen.PaymentDue.Text = LabelTotal.Text

            ElseIf ButtonSenior.BackColor = Color.Cyan Then
                ButtonSenior.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                Dim vatable As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim vat As Decimal = Math.Round(sumpayment - vatable, 2)

                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")

                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                LabelDiscount.Text = "0.00"

                LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text
                TextBoxPercentDiscount.Text = ""
            End If

            If ButtonSenior.BackColor = Color.Cyan Then
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonSoloParent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonNAAC.BackColor = Color.Gainsboro
                ButtonDiplomat.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Senior(20%)"

            End If

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Ride!", MessageBoxIcon.Information)
            ButtonSenior.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
        _detailsForm.SetTransactionDetails("Senior(20%)", or_no)
        ' Open popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True
        End If

    End Sub

    Private Sub ButtonZeroRated_Click(sender As Object, e As EventArgs) Handles ButtonZeroRated.Click
        Dim or_no As String = TextBoxBarcode.Text
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

            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            If ButtonZeroRated.BackColor = Color.Gainsboro Then
                ButtonZeroRated.BackColor = Color.Cyan
                LabelType.Text = "Zero Rated"
                ' The original math: only strips the 12% VAT
                Dim zero_rated As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim lessvat As Decimal = Math.Round(sumpayment - zero_rated, 2)

                LabelTotal.Text = sumpayment.ToString("0.00")
                'LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text

                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                LabelLessVat.Text = "0.00"
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = sumpayment.ToString("0.00")

            ElseIf ButtonZeroRated.BackColor = Color.Cyan Then
                ButtonZeroRated.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                Dim vatable As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim vat As Decimal = Math.Round(sumpayment - vatable, 2)

                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")

                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                LabelDiscount.Text = "0.00"

                LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text
                TextBoxPercentDiscount.Text = ""
            End If

            If ButtonZeroRated.BackColor = Color.Cyan Then
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonSoloParent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonNAAC.BackColor = Color.Gainsboro
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonDiplomat.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Zero Rated"

            End If

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Ride!", MessageBoxIcon.Information)
            ButtonZeroRated.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
        _detailsForm.SetTransactionDetails("Zero Rated", or_no)
        ' Open popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True
        End If
    End Sub

    Private Sub ButtonEmployee_Click(sender As Object, e As EventArgs) Handles ButtonEmployee.Click
        Dim or_no As String = TextBoxBarcode.Text
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

            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            If ButtonEmployee.BackColor = Color.Gainsboro Then
                ButtonEmployee.BackColor = Color.Cyan
                LabelType.Text = "BDAY Celeb(50%)"

                Dim PercentDiscount = 0.5D      '0.3D
                Dim introDiscount = 100

                'Dim introDiscountAmount As Decimal = CDec(TextBoxQty.Text) * introDiscount
                'Dim amountDue As Decimal = Math.Round(sumpayment - introDiscountAmount, 2)

                'EXISTING COMPUTATION 
                Dim introDiscountAmount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                Dim amountDue As Decimal = Math.Round(sumpayment - introDiscountAmount, 2)
                Dim vatable As Decimal = Math.Round(amountDue / 1.12D, 2)
                Dim vat As Decimal = Math.Round(amountDue - vatable, 2)

                'For Amount Due
                LabelQtyPrice.Text = sumpayment.ToString("0.00")
                LabelTotal.Text = amountDue.ToString("0.00")

                'For Discount
                LabelDiscount.Text = introDiscountAmount.ToString("0.00")

                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")

                LabelLessVat.Text = "0.00"
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

                ' For Customer View
                secondscreen.PaymentDue.Text = LabelTotal.Text

            ElseIf ButtonEmployee.BackColor = Color.Cyan Then
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))

                LabelQtyPrice.Text = "0.00"
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                LabelDiscount.Text = "0.00"

                LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text
                TextBoxPercentDiscount.Text = ""
            End If

            If ButtonEmployee.BackColor = Color.Cyan Then
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonSoloParent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonNAAC.BackColor = Color.Gainsboro
                ButtonDiplomat.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonStudent.BackColor = Color.Gainsboro
                LabelType.Text = "BDAY Celeb(50%)"

            End If

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Ride!", MessageBoxIcon.Information)
            ButtonEmployee.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"

        End Try
        _detailsForm.SetTransactionDetails("BDAY Celeb(50%)", or_no)
        ' Open popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True
        End If
    End Sub

    Private Sub ButtonOrigin_Click(sender As Object, e As EventArgs) Handles ButtonRegularRide.Click
        ButtonAddItem.BackColor = Color.Gainsboro
        Dim RideSelectionForm As New origin
        RideSelectionForm.Show()
        TextBoxQty.Text = "0"
        Button14.Enabled = True
        ButtonAddItem.Enabled = True
        CurrentCustomerName = "Guest"

    End Sub

    Private Sub ButtonTrainingMode_Click(sender As Object, e As EventArgs) Handles ButtonTrainingMode.Click

        EnableManagerActivity.Show()

    End Sub

    Private Sub ButtonReprintTicket_Click(sender As Object, e As EventArgs) Handles ButtonReprintTicket.Click
        'show reprint form
        reprint.Show()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try

            'Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) + Val(TextBoxBaggageCost.Text) + Val(TextBoxCardAmount.Text)).ToString("0.00")
            Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text)).ToString("0.00")

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

                    'combo = ComboBoxTime.Text.Split("-")

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
                                e.Graphics.DrawString(comb & ControlChars.NewLine & comb1 & ControlChars.NewLine & "TextBoxSeat.Text" & ControlChars.NewLine & ButtonRegularRide.Text & ControlChars.NewLine & "DES", N, Brushes.Black, New RectangleF(100, 130, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("-- Please keep this ticket for checking --", N).Width / 2)
                                e.Graphics.DrawString("-- Please keep this ticket for checking --", N, Brushes.Black, sngCenterPage, 200)

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
                                e.Graphics.DrawString("OFFICIAL RECEIPT", N, Brushes.Black, 160, 200)

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
                                e.Graphics.DrawString("sample 1" & " " & "sample 1" & ControlChars.NewLine &
                                              comb & ControlChars.NewLine &
                                              comb1 & ControlChars.NewLine &
                                              ButtonRegularRide.Text & ControlChars.NewLine &
                                              "DESt" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              "Sample 3" & ControlChars.NewLine &
                                              "Sample 4", N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS SERVES AS AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS SERVES AS AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 830)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N).Width / 2)
                                e.Graphics.DrawString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N, Brushes.Black, sngCenterPage, 840)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("FIVE (5) YEARS FROM THE DATE", N).Width / 2)
                                e.Graphics.DrawString("FIVE (5) YEARS FROM THE DATE", N, Brushes.Black, sngCenterPage, 852)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OF THE PERMIT TO USE.", N).Width / 2)
                                e.Graphics.DrawString("OF THE PERMIT TO USE.", N, Brushes.Black, sngCenterPage, 864)

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
                                e.Graphics.DrawString("OFFICIAL RECEIPT", N, Brushes.Black, 160, 200)

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
                                e.Graphics.DrawString("Sample 5" & " " & "Sample 6" & ControlChars.NewLine &
                                              comb & ControlChars.NewLine &
                                              comb1 & ControlChars.NewLine &
                                              ButtonRegularRide.Text & ControlChars.NewLine &
                                              "dest" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              "Sample 7" & ControlChars.NewLine &
                                              "Sample 8", N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS SERVES AS AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS SERVES AS AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 830)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N).Width / 2)
                                e.Graphics.DrawString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N, Brushes.Black, sngCenterPage, 840)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("FIVE (5) YEARS FROM THE DATE", N).Width / 2)
                                e.Graphics.DrawString("FIVE (5) YEARS FROM THE DATE", N, Brushes.Black, sngCenterPage, 852)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OF THE PERMIT TO USE.", N).Width / 2)
                                e.Graphics.DrawString("OF THE PERMIT TO USE.", N, Brushes.Black, sngCenterPage, 864)

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
                                e.Graphics.DrawString(comb & ControlChars.NewLine & comb1 & ControlChars.NewLine & "TextBoxSeat.Text" & ControlChars.NewLine & ButtonRegularRide.Text & ControlChars.NewLine & "Desi", N, Brushes.Black, New RectangleF(100, 130, 175, 175), sf)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("-- Please keep this ticket for checking --", N).Width / 2)
                                e.Graphics.DrawString("-- Please keep this ticket for checking --", N, Brushes.Black, sngCenterPage, 200)

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
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 200)

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
                                e.Graphics.DrawString("Sample 91" & " " & "Sample 92" & ControlChars.NewLine &
                                              comb & ControlChars.NewLine &
                                              comb1 & ControlChars.NewLine &
                                              ButtonRegularRide.Text & ControlChars.NewLine &
                                              "SSSSSSs" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                             "Sample 93" & ControlChars.NewLine &
                                              "Sample 94", N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS SERVES AS AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS SERVES AS AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 830)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N).Width / 2)
                                e.Graphics.DrawString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N, Brushes.Black, sngCenterPage, 840)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("FIVE (5) YEARS FROM THE DATE", N).Width / 2)
                                e.Graphics.DrawString("FIVE (5) YEARS FROM THE DATE", N, Brushes.Black, sngCenterPage, 852)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OF THE PERMIT TO USE.", N).Width / 2)
                                e.Graphics.DrawString("OF THE PERMIT TO USE.", N, Brushes.Black, sngCenterPage, 864)

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
                                e.Graphics.DrawString("OFFICIAL RECEIPT", N, Brushes.Black, 160, 200)

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
                                e.Graphics.DrawString("Sample 96" & " " & "Sample 96" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              "SAple", N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS SERVES AS AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS SERVES AS AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 830)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N).Width / 2)
                                e.Graphics.DrawString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N, Brushes.Black, sngCenterPage, 840)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("FIVE (5) YEARS FROM THE DATE", N).Width / 2)
                                e.Graphics.DrawString("FIVE (5) YEARS FROM THE DATE", N, Brushes.Black, sngCenterPage, 852)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OF THE PERMIT TO USE.", N).Width / 2)
                                e.Graphics.DrawString("OF THE PERMIT TO USE.", N, Brushes.Black, sngCenterPage, 864)

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
                                e.Graphics.DrawString("OFFICIAL RECEIPT", N, Brushes.Black, 160, 200)

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
                                e.Graphics.DrawString("Sample 11111" & " " & "Sample_2222" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              "SAAAAAAAAAAa", N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS SERVES AS AN OFFICIAL RECEIPT", N).Width / 2)
                                e.Graphics.DrawString("THIS SERVES AS AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

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

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                                e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 830)

                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N).Width / 2)
                                e.Graphics.DrawString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N, Brushes.Black, sngCenterPage, 840)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("FIVE (5) YEARS FROM THE DATE", N).Width / 2)
                                e.Graphics.DrawString("FIVE (5) YEARS FROM THE DATE", N, Brushes.Black, sngCenterPage, 852)
                                sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OF THE PERMIT TO USE.", N).Width / 2)
                                e.Graphics.DrawString("OF THE PERMIT TO USE.", N, Brushes.Black, sngCenterPage, 864)

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
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OFFICIAL RECEIPT", N).Width / 2)
                        e.Graphics.DrawString("OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 200)

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
                        e.Graphics.DrawString("SAmple 111" & " " & "SAmple 111" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              "" & ControlChars.NewLine &
                                              TextBoxTripFare.Text & ControlChars.NewLine &
                                              "SASSS" & ControlChars.NewLine &
                                              "SSSSSS", N, Brushes.Black, New RectangleF(100, 250, 175, 175), sf)

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

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS SERVES AS AN OFFICIAL RECEIPT", N).Width / 2)
                        e.Graphics.DrawString("THIS SERVES AS AN OFFICIAL RECEIPT", N, Brushes.Black, sngCenterPage, 558)

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

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("----------------------------------------", N).Width / 2)
                        e.Graphics.DrawString("----------------------------------------", N, Brushes.Black, sngCenterPage, 830)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N).Width / 2)
                        e.Graphics.DrawString("THIS INVOICE/RECEIPT SHALL BE VALID FOR", N, Brushes.Black, sngCenterPage, 840)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("FIVE (5) YEARS FROM THE DATE", N).Width / 2)
                        e.Graphics.DrawString("FIVE (5) YEARS FROM THE DATE", N, Brushes.Black, sngCenterPage, 852)
                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("OF THE PERMIT TO USE.", N).Width / 2)
                        e.Graphics.DrawString("OF THE PERMIT TO USE.", N, Brushes.Black, sngCenterPage, 864)

                        ''End of THERMAL
                    End If

                End If


            ElseIf ButtonEmployee.BackColor = Color.Gray Then

                'combo = ComboBoxTime.Text.Split("-")

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
                        e.Graphics.DrawString(comb & ControlChars.NewLine & comb1 & ControlChars.NewLine & "TextBoxSeat.Text" & ControlChars.NewLine & ButtonRegularRide.Text & ControlChars.NewLine & "DEsts", N, Brushes.Black, New RectangleF(100, 130, 175, 175), sf)

                        sngCenterPage = Convert.ToSingle(e.PageBounds.Width / 2 - e.Graphics.MeasureString("-- Please keep this ticket for checking --", N).Width / 2)
                        e.Graphics.DrawString("-- Please keep this ticket for checking --", N, Brushes.Black, sngCenterPage, 200)

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

    Private Sub CheckBoxPercentDiscount_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPercentDiscount.CheckedChanged
        If CheckBoxPercentDiscount.Checked = True Then
            ComboBoxDiscount.SelectedIndex = -1
            TextBoxDiscountPromo.Text = ""
            ComboBoxDiscount.Enabled = True
            TextBoxDiscountPromo.Enabled = True

        Else
            'ComboBoxDiscount.Text = ""
            ComboBoxDiscount.SelectedIndex = -1
            TextBoxDiscountPromo.Text = ""
            ComboBoxDiscount.Enabled = False
            TextBoxDiscountPromo.Enabled = False

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

    Private Sub ButtonReport_Click(sender As Object, e As EventArgs) Handles ButtonReport.Click
        ListOfReport.Show()
    End Sub

    Private Sub ButtonQty_Click(sender As Object, e As EventArgs) Handles ButtonQty.Click

        TextBoxQty.Text = ""
        LabelTotal.Text = "0.00"
        ButtonStudent.BackColor = Color.Gainsboro
        ButtonSoloParent.BackColor = Color.Gainsboro
        ButtonSenior.BackColor = Color.Gainsboro
        ButtonPWD.BackColor = Color.Gainsboro
        ButtonEmployee.BackColor = Color.Gainsboro
        ButtonZeroRated.BackColor = Color.Gainsboro
        ButtonQty.BackColor = Color.Cyan
        TableLayoutPanel10.Enabled = True

        Dim QtyForm As New Qty
        QtyForm.Show()

    End Sub

    Private Sub ButtonSoloParent_Click(sender As Object, e As EventArgs) Handles ButtonSoloParent.Click
        Dim or_no As String = TextBoxBarcode.Text
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

            Dim PercentDiscount = 0.1D
            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            If ButtonSoloParent.BackColor = Color.Gainsboro Then
                ButtonSoloParent.BackColor = Color.Cyan
                LabelType.Text = "Solo Parent(10%)"

                'EXISTING IMPLEMENTION
                'Dim discount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                'Dim amountDue As Decimal = Math.Round(sumpayment - discount, 2)
                'Dim vatExempt As Decimal = Math.Round(amountDue / 1.12D, 2)
                'Dim lessvat As Decimal = Math.Round(amountDue - vatExempt, 2)

                'TextBoxPercentDiscount.Text = PercentDiscount
                'LabelDiscount.Text = discount.ToString("0.00")
                'LabelTotal.Text = amountDue.ToString("0.00")

                'LabelVAT.Text = "0.00"
                'LabelLessVat.Text = lessvat.ToString("0.00")
                'LabelVATExempt.Text = vatExempt.ToString("0.00")

                'BIR COMPLIANCE INCLUSIVE VAT
                Dim BvatExempt As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim Bvat As Decimal = Math.Round(sumpayment - BvatExempt, 2)
                Dim Bdiscount As Decimal = Math.Round(BvatExempt * PercentDiscount, 2)
                Dim withoutVat As Decimal = Math.Round(BvatExempt - Bdiscount, 2)
                'Dim BamountDue As Decimal = Math.Round(withoutVat + Bvat, 2)
                Dim BamountDue As Decimal = Math.Round(sumpayment - Bdiscount, 2)


                'BIR COMPLIANCE
                LabelDiscount.Text = Bdiscount.ToString("0.00")
                LabelTotal.Text = BamountDue.ToString("0.00")
                LabelVATExempt.Text = "0.00"
                LabelVAT.Text = Bvat.ToString("0.00")

                'secondscreen.PaymentDue.Text = LabelTotal.Text
                LabelLessVat.Text = "0.00"
                LabelVATable.Text = BvatExempt.ToString("0.00")
                LabelZeroRated.Text = "0.00"

            ElseIf ButtonSoloParent.BackColor = Color.Cyan Then
                ButtonSoloParent.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))

                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                LabelDiscount.Text = "0.00"

                LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text
                TextBoxPercentDiscount.Text = ""
            End If

            If ButtonSoloParent.BackColor = Color.Cyan Then
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonNAAC.BackColor = Color.Gainsboro
                ButtonDiplomat.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Solo Parent(10%)"

            End If

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Ride!", MessageBoxIcon.Information)
            ButtonSoloParent.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
        _detailsForm.SetTransactionDetails("Solo Parent(10%)", or_no)
        ' Open popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True
        End If
    End Sub

    Private Sub TextBoxQty_TextChanged(sender As Object, e As EventArgs) Handles TextBoxQty.TextChanged

        'Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text).ToString("0.00") * Val(TextBoxQty.Text)).ToString("0.00")
        'Dim pay As Decimal = 0D
        'Decimal.TryParse(sumpayment, pay)

        'Dim vatable As Decimal = Math.Round(pay / 1.12D, 2)

        'LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
        'LabelVATable.Text = (Math.Round(Val((sumpayment) / 1.12), 2))
        'LabelVATable.Text = vatable.ToString("0.00")

        'LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))

        'LabelTotal.Text = (Math.Round(Val((LabelVAT.Text) + Val(LabelVATable.Text)), 2))

        Dim rideFare As Decimal
        Decimal.TryParse(TextBoxTripFare.Text, rideFare)

        Dim qty As Decimal
        Decimal.TryParse(TextBoxQty.Text, qty)

        Dim total As Decimal = rideFare * qty
        LabelTotal.Text = total.ToString("0.00")
        LabelQtyPrice.Text = total.ToString("0.00")

        Dim vatable As Decimal = Math.Round(total / 1.12D, 2)
        LabelVATable.Text = vatable.ToString("0.00")

        Dim vat As Decimal = Math.Round(total - vatable, 2)
        LabelVAT.Text = vat.ToString("0.00")

        LabelLessVat.Text = "0.00"
        LabelVATExempt.Text = "0.00"
        LabelZeroRated.Text = "0.00"
        LabelDiscount.Text = "0.00"
        LabelType.Text = "Regular"

        secondscreen.PaymentDue.Text = LabelTotal.Text
    End Sub

    Private Sub mainform_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Software Versioning
        LabelSoftwareName.Text = Application.ProductName
        LabelVersionNo.Text = Application.ProductVersion

        ServicePointManager.SecurityProtocol = CType(3072 Or 768, SecurityProtocolType)

        ServicePointManager.ServerCertificateValidationCallback =
        Function(sender2, cert, chain, errors) True

        disableFunctionForAuthorizedPersonnelOnly()
        hideListFunction()
        disableKeypadNumber()

        Try
            secondscreen.Show()

            ' Start Sales Invoice # this:
            _counter = New SaleInvoiceCounter("00000-0000-00001")

            'Check if there's promo or introductory price
            'CheckColumnData()

            TextBoxQty.Enabled = False

            ' Set ListView properties For ListviewCashier
            ListViewCashier.View = View.Details
            ListViewCashier.FullRowSelect = True
            ListViewCashier.GridLines = True

            ' Add column headers
            ListViewCashier.Columns.Add("Item", 100)
            ListViewCashier.Columns.Add("Qty", 30)
            ListViewCashier.Columns.Add("Price", 70)
            ListViewCashier.Columns.Add("Total", 70)
            ListViewCashier.Columns.Add("Disc.", 70)
            ListViewCashier.Columns.Add("Net", 70)
            ListViewCashier.Columns.Add("VAT-S", 70)
            ListViewCashier.Columns.Add("VAT(12%)", 70)
            ListViewCashier.Columns.Add("VAT-ES", 70)
            ListViewCashier.Columns.Add("VAT ZR", 70)
            ListViewCashier.Columns.Add("TYPE", 70)
            ListViewCashier.Columns.Add("Item_Code", 70)
            ListViewCashier.Columns.Add("Less_Vat", 70)
            ListViewCashier.Columns.Add("Customer Name", 150)
            ListViewCashier.Columns(ListViewCashier.Columns.Count - 1).DisplayIndex = 0

            ' Set ListView properties For ListviewCashier
            ListViewWristband.View = View.Details
            ListViewWristband.FullRowSelect = True
            ListViewWristband.GridLines = True

            ' Add column headers
            ListViewWristband.Columns.Add("Guest_Id", 150)
            ListViewWristband.Columns.Add("Ride_Code", 150)
            ListViewWristband.Columns.Add("Type Of Transaction", 150)

            ' Set ListView properties For ListviewCustomer
            secondscreen.ListViewCustomerView.View = View.Details
            secondscreen.ListViewCustomerView.FullRowSelect = True
            secondscreen.ListViewCustomerView.GridLines = False

            ' Add column header
            ' Add this Clear() line to fix the repeating Qty and Ride columns!
            secondscreen.ListViewCustomerView.Columns.Clear()
            secondscreen.ListViewCustomerView.Columns.Add("Qty", 50)
            secondscreen.ListViewCustomerView.Columns.Add("Ride Description", 321)
            secondscreen.ListViewCustomerView.Columns.Add("Price", 85)
            secondscreen.ListViewCustomerView.Columns.Add("Total", 85)
            secondscreen.ListViewCustomerView.Columns.Add("Discount", 85)
            secondscreen.ListViewCustomerView.Columns.Add("Amount", 85)


            ' =========================================================
            ' LOAD PAYMENT METHODS FROM DATABASE (AS IS)
            ' =========================================================
            Try
                Using conn As New MySqlConnection(strConn)

                    Dim query As String = "SELECT payment_method FROM payment_method_tbl"
                    Dim cmd As New MySqlCommand(query, conn)
                    Dim da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    da.Fill(dt)

                    ComboBoxPaymentMethod.DataSource = dt
                    ComboBoxPaymentMethod.DisplayMember = "payment_method"
                    ComboBoxPaymentMethod.ValueMember = "payment_method"

                End Using

            Catch ex As Exception
                MsgBox("Error loading payment methods: " & ex.Message)
            End Try

            ' =========================================================

        Catch ex As Exception
            MsgBox("Please try the Transaction again!")
        End Try

    End Sub
    Private Sub ButtonAddItem_Click(sender As Object, e As EventArgs) Handles ButtonAddItem.Click

        ' Delete the verification row right before adding the real final row
        For i As Integer = secondscreen.ListViewCustomerView.Items.Count - 1 To 0 Step -1
            If secondscreen.ListViewCustomerView.Items(i).Tag IsNot Nothing AndAlso secondscreen.ListViewCustomerView.Items(i).Tag.ToString() = "TEMP_NAME" Then
                secondscreen.ListViewCustomerView.Items.RemoveAt(i)
            End If
        Next

        'Generate OR_No
        auto()
        'MsgBox(TextBoxBarcode.Text)

        Try
            If (TextBoxQty.Text = "0" Or TextBoxQty.Text = "" Or TextBoxTripFare.Text = "" Or LabelAvailed.Text = "" Or String.IsNullOrWhiteSpace(LabelQtyPrice.Text) Or String.IsNullOrWhiteSpace(LabelTotal.Text)) Then
                MsgBox("Please! Select Quantity or Rides")

            ElseIf CheckBoxPercentDiscount.Checked Then

                ListViewCashier.View = View.Details

                Dim item As New ListViewItem(LabelAvailed.Text)
                item.SubItems.Add(TextBoxQty.Text)
                item.SubItems.Add(TextBoxTripFare.Text)
                item.SubItems.Add(LabelQtyPrice.Text)
                item.SubItems.Add(LabelDiscount.Text)
                item.SubItems.Add(LabelTotal.Text)
                item.SubItems.Add(LabelVATable.Text)
                item.SubItems.Add(LabelVAT.Text)
                item.SubItems.Add(LabelVATExempt.Text)
                item.SubItems.Add(LabelZeroRated.Text)
                item.SubItems.Add(LabelType.Text)
                item.SubItems.Add(label_code.Text)
                item.SubItems.Add(LabelLessVat.Text)

                ' ADDED CUSTOMER NAME HERE
                item.SubItems.Add(CurrentCustomerName)

                item.SubItems(4).ForeColor = Color.Red

                ListViewCashier.Items.Add(item)
                item.SubItems(5).ForeColor = Color.Red

                CopyListviewCustomerView()

                TextBoxTripFare.Text = ""
                TextBoxQty.Text = "0"
                ButtonRegularRide.Text = "Regular"
                ButtonPromo.Text = "Introductory"
                ButtonQty.BackColor = Color.Gainsboro
                TextBoxPercentDiscount.Text = ""
                LabelQtyPrice.Text = ""
                LabelTotal.Text = ""
                LabelDiscount.Text = ""
                LabelVATable.Text = ""
                LabelVAT.Text = ""
                LabelZeroRated.Text = ""
                LabelType.Text = ""
                label_code.Text = ""
                LabelLessVat.Text = ""
                ComboBoxDiscount.Text = ""
                TextBoxDiscountPromo.Text = ""
                LabelAvailed.Text = ""
                CheckBoxPercentDiscount.Checked = False

            Else
                ListViewCashier.View = View.Details

                Dim item As New ListViewItem(LabelAvailed.Text)
                item.SubItems.Add(TextBoxQty.Text)
                item.SubItems.Add(TextBoxTripFare.Text)
                item.SubItems.Add(LabelQtyPrice.Text)
                item.SubItems.Add(LabelDiscount.Text)
                item.SubItems.Add(LabelTotal.Text)
                item.SubItems.Add(LabelVATable.Text)
                item.SubItems.Add(LabelVAT.Text)
                item.SubItems.Add(LabelVATExempt.Text)
                item.SubItems.Add(LabelZeroRated.Text)
                item.SubItems.Add(LabelType.Text)
                item.SubItems.Add(label_code.Text)
                item.SubItems.Add(LabelLessVat.Text)

                ' ADDED CUSTOMER NAME HERE
                item.SubItems.Add(CurrentCustomerName)

                item.SubItems(4).ForeColor = Color.Red
                item.SubItems(5).ForeColor = Color.Red

                ListViewCashier.Items.Add(item)

                CopyListviewCustomerView()

                TextBoxTripFare.Text = ""
                TextBoxQty.Text = "0"
                ButtonRegularRide.Text = "Regular"
                ButtonPromo.Text = "Introductory"
                LabelQtyPrice.Text = ""
                LabelTotal.Text = ""
                LabelDiscount.Text = ""
                LabelVATable.Text = ""
                LabelVAT.Text = ""
                LabelLessVat.Text = ""
                LabelZeroRated.Text = ""
                LabelType.Text = ""
                label_code.Text = ""
                ComboBoxDiscount.Text = ""
                LabelAvailed.Text = ""
                TextBoxDiscountPromo.Text = ""

                ButtonAddItem.BackColor = Color.Cyan

                ButtonQty.BackColor = Color.Gainsboro
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonSoloParent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonNAAC.BackColor = Color.Gainsboro
                ButtonDiplomat.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                TextBoxPercentDiscount.Text = ""
                CheckBoxPercentDiscount.Checked = False

            End If

            ' --- UPDATED BUTTON LOCK/UNLOCK LOGIC HERE ---
            ButtonComputeTotal.Enabled = True
            ButtonAddItem.Enabled = False
            Button14.Enabled = False

            ' Save Guest to Database Customer
            If CurrentCustomerName = "Guest" Then
                Try
                    Using dbConn As New MySqlConnection(strConn)
                        dbConn.Open()
                        Using cmd As New MySqlCommand("insert_customer_procedure", dbConn)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@p_or_no", TextBoxBarcode.Text)
                            cmd.Parameters.AddWithValue("@p_name", "Guest")
                            cmd.Parameters.AddWithValue("@p_id_no", "N/A")
                            cmd.Parameters.AddWithValue("@p_tin_no", "N/A")
                            cmd.Parameters.AddWithValue("@p_address", "N/A")
                            cmd.Parameters.AddWithValue("@p_transType", LabelType.Text)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using
                Catch ex As Exception
                End Try
            End If

            ' Clear out the name for the next item!
            CurrentCustomerName = ""

            ' Clear out the name for the next item!
            CurrentCustomerName = ""

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub


    Private Sub CheckColumnData()
        Try
            conn.Open()

            ' Example: Check if "name" column in "users" table has any non-empty values
            Dim query As String = "SELECT COUNT(*) FROM ride_pricing_tbl WHERE introductory IS NOT NULL AND introductory <> ''"
            Dim cmd As New MySqlCommand(query, conn)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            ' Enable button if data exists, otherwise disable
            If count > 0 Then

                ButtonPromo.Enabled = True
            Else
                ButtonPromo.Enabled = False

            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub ButtonPromo_Click(sender As Object, e As EventArgs) Handles ButtonPromo.Click
        ButtonAddItem.BackColor = Color.Gainsboro
        Dim RideIntroductorySelectionForm As New IntroductoryForm
        RideIntroductorySelectionForm.Show()
        TextBoxQty.Text = "0"
    End Sub

    Private Function ComputeColumnTotal(lv As ListView, columnIndex As Integer) As Decimal
        Dim total As Decimal = 0

        For Each item As ListViewItem In lv.Items
            Dim cellValue As String = item.SubItems(columnIndex).Text.Trim()
            Dim value As Decimal

            ' Make sure it’s a number before adding
            If Decimal.TryParse(cellValue, value) Then
                total += value
            End If
        Next

        Return total
    End Function

    Private Sub ShowColumnTotal()
        ' Remove existing total row if any
        If ListViewCashier.Items.Count > 0 Then
            Dim last = ListViewCashier.Items(ListViewCashier.Items.Count - 1)
            If last.SubItems(1).Text = "TOTAL:" Then
                ListViewCashier.Items.Remove(last)
            End If
        End If

        ' Compute total
        Dim qty As Decimal = ComputeColumnTotal(ListViewCashier, 1)
        Dim base_price As Decimal = ComputeColumnTotal(ListViewCashier, 2)
        Dim total As Decimal = ComputeColumnTotal(ListViewCashier, 3)
        Dim discount As Decimal = ComputeColumnTotal(ListViewCashier, 4)
        Dim net As Decimal = ComputeColumnTotal(ListViewCashier, 5)
        Dim vatableSale As Decimal = ComputeColumnTotal(ListViewCashier, 6)
        Dim vat12 As Decimal = ComputeColumnTotal(ListViewCashier, 7)
        Dim vatableExemptSale As Decimal = ComputeColumnTotal(ListViewCashier, 8)
        Dim vatZeroRated As Decimal = ComputeColumnTotal(ListViewCashier, 9)
        Dim lessVat As Decimal = ComputeColumnTotal(ListViewCashier, 12)

    End Sub

    Private Sub ButtonComputeTotal_Click(sender As Object, e As EventArgs) Handles ButtonComputeTotal.Click

        Try
            If TextBoxQty.Text = "" Then
                MsgBox("Select Rides or Qty")
            Else
                'Store data to or_items_tbl

                ItemInsertIntoTicketTracsaction()

                Dim qtyTotal As Decimal = ComputeColumnTotal(ListViewCashier, 1)
                LabelQtyTotal.Text = qtyTotal.ToString("N2")

                Dim basepriceTotal As Decimal = ComputeColumnTotal(ListViewCashier, 2)
                Label_BasePrice.Text = basepriceTotal.ToString("N2")

                Dim qtyPrice As Decimal = ComputeColumnTotal(ListViewCashier, 3)
                LabelQtyPrice.Text = qtyPrice.ToString("N2")

                Dim totalDiscount As Decimal = ComputeColumnTotal(ListViewCashier, 4)
                LabelDiscount.Text = totalDiscount.ToString("N2")

                Dim totalPrice As Decimal = ComputeColumnTotal(ListViewCashier, 5)
                LabelTotal.Text = totalPrice.ToString("N2")

                Dim totalVatable As Decimal = ComputeColumnTotal(ListViewCashier, 6)
                LabelVATable.Text = totalVatable.ToString("N2")

                Dim totalVat As Decimal = ComputeColumnTotal(ListViewCashier, 7)
                LabelVAT.Text = totalVat.ToString("N2")

                Dim totalVatExempt As Decimal = ComputeColumnTotal(ListViewCashier, 8)
                LabelVATExempt.Text = totalVatExempt.ToString("N2")

                Dim totalZeroRated As Decimal = ComputeColumnTotal(ListViewCashier, 9)
                LabelZeroRated.Text = totalZeroRated.ToString("N2")

                Dim totalLessVat As Decimal = ComputeColumnTotal(ListViewCashier, 12)
                LabelLessVat.Text = totalLessVat.ToString("N2")

                ButtonQty.BackColor = Color.Gainsboro
                ButtonAddItem.BackColor = Color.Gainsboro
                'ButtonComputeTotal.BackColor = Color.Cyan
                'ButtonComputeTotal.ForeColor = Color.Gainsboro
                LabelTotal.ForeColor = Color.Red

                secondscreen.LabelTotalCustomerView.Text = LabelTotal.Text
                secondscreen.LabelChangeCustomerChange.Text = TextBoxChange.Text
                ComboBoxPaymentMethod.Enabled = True

                ButtonPromo.Text = "Introductory"
                ButtonRegularRide.Text = "Regular"
                TextBoxQty.Text = "0"
                TextBoxTripFare.Text = ""

            End If
        Catch

        End Try
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        Dim sumpayment As String = Convert.ToDecimal(Val(LabelTotal.Text)).ToString("0.00")

        '============================== CARD ===================================================
        If ButtonCard.BackColor = Color.Gray Then

            'If Passenger_idTextBox.Text = "Jose" 
            ' Then
            'fname.Text = "" Or
            'lname.Text = "" Or
            'LabelTotal.Text = "" Or
            'TextBoxMoney.Text = "" Or
            'TextBoxChange.Text = ""

            MsgBox("Can't Issue Ticket. Please make sure all fields are filled up!", MessageBoxIcon.Error)

            ' Else

            'for calling auto (payment id)
            auto()

            'for combining vessel id and time
            'Dim sched_ID As String
            'conn.Open()
            'combo = ComboBoxTime.Text.Split("-")
            'Dim comb1 = combo(1).ToString
            'Dim comb = combo(0).ToString

            'conn.Close()

            'for inserting transaction
            'Dim qd As String = "INSERT INTO or_tbl(id, or_no, passenger_id, payment_date, payment_time, station_id, pos_id, user_id, trip_fare, baggage_cost, card_amount, vatable, vat_exempt, zero_rated, vat, discount, total_amount, type_of_transaction, void_status, upload) VALUES 
            '                                     (@id, @or_no, @passenger_id, @payment_date, @payment_time, @station_id, @pos_id, @user_id, @trip_fare, @baggage_cost, @card_amount, @vatable, @vat_exempt, @zero_rated, @vat, @discount, @total_amount, @type_of_transaction, @void_status, @upload)"


            Dim qd As String = "INSERT INTO or_tbl(id, or_no, payment_date, payment_time, pos_id, user_id, vatable, vat_exempt, zero_rated, vat, discount, total_amount, payment_method, type_of_transaction, void_status, upload) VALUES 
                                                      (@id, @or_no, @payment_date, @payment_time, @pos_id, @user_id, @vatable, @vat_exempt, @zero_rated, @vat, @discount, @total_amount, @type_of_transaction, @void_status, @upload)"

            Dim cmd As New MySqlCommand(qd) With {.Connection = conn}

            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@id", randomString)
            cmd.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)
            'cmd.Parameters.AddWithValue("@passenger_id", Passenger_idTextBox.Text)
            cmd.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@payment_time", Now.ToString("HH:mm"))
            'cmd.Parameters.AddWithValue("@station_id", LabelStationID.Text)
            cmd.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
            cmd.Parameters.AddWithValue("@user_id", LabelCashierID.Text)
            'cmd.Parameters.AddWithValue("@trip_fare", TextBoxTripFare.Text)
            'cmd.Parameters.AddWithValue("@baggage_cost", TextBoxBaggageCost.Text)
            'cmd.Parameters.AddWithValue("@card_amount", TextBoxCardAmount.Text)
            cmd.Parameters.AddWithValue("@vatable", LabelVATable.Text)
            cmd.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
            cmd.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
            cmd.Parameters.AddWithValue("@vat", LabelVAT.Text)
            cmd.Parameters.AddWithValue("@discount", LabelDiscount.Text)
            cmd.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
            cmd.Parameters.AddWithValue("@payment_method", ComboBoxPaymentMethod.Text)
            cmd.Parameters.AddWithValue("@type_of_transaction", LabelType.Text)
            cmd.Parameters.AddWithValue("@void_status", "no")
            cmd.Parameters.AddWithValue("@upload", "no")

            Try
                conn.Open()
                cmd.ExecuteReader()

                'Try
                '    conn.Open()
                '    Dim cdd As New MySqlCommand("Update seat_tbl set seat_status = @seat_status,  updated = @updated WHERE vessel_id = @vesssel_id AND seat_no = @seat_no", conn)
                '    cdd.CommandType = CommandType.Text
                '    cdd.Parameters.AddWithValue("@seat_status", 1)
                '    cdd.Parameters.AddWithValue("@updated", "yes")
                '    cdd.Parameters.AddWithValue("@vesssel_id", comb)
                '    cdd.Parameters.AddWithValue("@seat_no", TextBoxSeat.Text)
                '    Dim reader As MySqlDataReader

                '    reader = cdd.ExecuteReader
                conn.Close()

                '============APPENDED===============
                Try

                    Dim filepath As String = "C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"
                    '' Dim filepath As String = "C:\E-Journal\" & "2019-11-14" & ".txt"


                    Dim sw As StreamWriter
                    '  Dim fs As FileStream


                    ' Dim fs As FileStream

                    If (Not File.Exists(filepath)) Then

                        sw = File.CreateText(filepath)
                        sw.WriteLine("           CAVITE SUPERFERRY")
                        sw.WriteLine("            PINOY CATAMARAN")
                        sw.WriteLine("          #17 SILAHIS STREET,")
                        sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                        sw.WriteLine("              PHILIPPINES")
                        sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                        sw.WriteLine("       POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                        sw.WriteLine("            MIN: XXXXXXXXXX")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("Cashier: " & LabelCashierName.Text)
                        'sw.WriteLine("Station ID: " & LabelStationID.Text)
                        sw.WriteLine("            OFFICIAL RECEIPT")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                        sw.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                        sw.WriteLine("----------------------------------------")
                        'sw.WriteLine("NAME                               " & fname.Text & " " & lname.Text)
                        'sw.WriteLine("VESSEL ID                          ")
                        'sw.WriteLine("SCHEDULE                           ")
                        'sw.WriteLine("SEAT NO.                           ")
                        'sw.WriteLine("PT. OF ORIGIN                      ")
                        'sw.WriteLine("PT. OF DESTINATION                 ")
                        'sw.WriteLine("TRIP FARE                          " & TextBoxTripFare.Text)
                        'sw.WriteLine("BAGGAGE COST                       " & TextBoxBaggageCost.Text)
                        'sw.WriteLine("CARD AMOUNT                        " & TextBoxCardAmount.Text)
                        'sw.WriteLine("----------------------------------------")
                        sw.WriteLine("TOTAL AMOUNT                       " & LabelTotal.Text)
                        sw.WriteLine("PAYMENT: " & ComboBoxPaymentMethod.Text & "     " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                        sw.WriteLine("CHANGE                             " & TextBoxChange.Text)
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("(V) VATable Sales                  " & LabelVATable.Text)
                        sw.WriteLine("VAT Amount                         " & LabelVAT.Text)
                        sw.WriteLine("(X) VAT-Exempt Sales               " & LabelVATExempt.Text)
                        If ButtonZeroRated.BackColor = Color.Gainsboro Then
                            sw.WriteLine("(Z) Zero-Rated Sales               " & LabelZeroRated.Text)

                        ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                            sw.WriteLine("(Z) Zero-Rated Sales               " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                        End If
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("Discount                           " & LabelDiscount.Text)
                        sw.WriteLine("                                   " & LabelType.Text)
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("Name: _________________________________")
                        sw.WriteLine("Address: ______________________________")
                        sw.WriteLine("_______________________________________")
                        sw.WriteLine("ID #: _________________________________")
                        sw.WriteLine("OSCA/SC ID #: _________________________")
                        sw.WriteLine("PWD ID #: _____________________________")
                        sw.WriteLine("TIN #: ________________________________")
                        sw.WriteLine("Bus Style: ____________________________")
                        sw.WriteLine("Signature: ____________________________")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("               VINTATECH")
                        sw.WriteLine("        Unit 507 F&L Bldg. 2211")
                        sw.WriteLine("     Commonwealth, Holy Spirit 1127")
                        sw.WriteLine("              Quezon City")
                        sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                        sw.WriteLine("        Accred. No.: XXXXXXXXXXX")
                        sw.WriteLine("        Date Issued: XX/XX/XXXX")
                        sw.WriteLine("        Valid Until: XX/XX/XXXX")
                        sw.WriteLine("          PTU No.: XXXXXXXXX")
                        sw.WriteLine("        Date Issued: XX/XX/XXXX")
                        sw.WriteLine("        Valid Until: XX/XX/XXXX")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                        sw.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                        sw.WriteLine("         OF THE PERMIT TO USE.")
                        sw.WriteLine("")
                        sw.WriteLine("")
                        sw.WriteLine("")

                    Else

                        sw = File.AppendText(filepath)
                        sw.WriteLine("           CAVITE SUPERFERRY")
                        sw.WriteLine("            PINOY CATAMARAN")
                        sw.WriteLine("          #17 SILAHIS STREET,")
                        sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                        sw.WriteLine("              PHILIPPINES")
                        sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                        sw.WriteLine("       POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                        sw.WriteLine("            MIN: XXXXXXXXXX")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("Cashier: " & LabelCashierName.Text)
                        'sw.WriteLine("Station ID: " & LabelStationID.Text)
                        sw.WriteLine("            OFFICIAL RECEIPT")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                        sw.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                        sw.WriteLine("----------------------------------------")
                        'sw.WriteLine("NAME                               " & fname.Text & " " & lname.Text)
                        'sw.WriteLine("VESSEL ID                          ")
                        'sw.WriteLine("SCHEDULE                           ")
                        'sw.WriteLine("SEAT NO.                           ")
                        'sw.WriteLine("PT. OF ORIGIN                      ")
                        'sw.WriteLine("PT. OF DESTINATION                 ")
                        'sw.WriteLine("TRIP FARE                          " & TextBoxTripFare.Text)
                        'sw.WriteLine("BAGGAGE COST                       " & TextBoxBaggageCost.Text)
                        'sw.WriteLine("CARD AMOUNT                        " & TextBoxCardAmount.Text)
                        'sw.WriteLine("----------------------------------------")
                        sw.WriteLine("TOTAL AMOUNT                       " & LabelTotal.Text)
                        sw.WriteLine("PAYMENT: " & ComboBoxPaymentMethod.Text & "     " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                        sw.WriteLine("CHANGE                             " & TextBoxChange.Text)
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("(V) VATable Sales                  " & LabelVATable.Text)
                        sw.WriteLine("VAT Amount                         " & LabelVAT.Text)
                        sw.WriteLine("(X) VAT-Exempt Sales               " & LabelVATExempt.Text)
                        If ButtonZeroRated.BackColor = Color.Gainsboro Then
                            sw.WriteLine("(Z) Zero-Rated Sales               " & LabelZeroRated.Text)

                        ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                            sw.WriteLine("(Z) Zero-Rated Sales               " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                        End If
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("Discount                           " & LabelDiscount.Text)
                        sw.WriteLine("                                   " & LabelType.Text)
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("Name: _________________________________")
                        sw.WriteLine("Address: ______________________________")
                        sw.WriteLine("_______________________________________")
                        sw.WriteLine("ID #: _________________________________")
                        sw.WriteLine("OSCA/SC ID #: _________________________")
                        sw.WriteLine("PWD ID #: _____________________________")
                        sw.WriteLine("TIN #: ________________________________")
                        sw.WriteLine("Bus Style: ____________________________")
                        sw.WriteLine("Signature: ____________________________")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("               VINTATECH")
                        sw.WriteLine("        Unit 507 F&L Bldg. 2211")
                        sw.WriteLine("     Commonwealth, Holy Spirit 1127")
                        sw.WriteLine("              Quezon City")
                        sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                        sw.WriteLine("        Accred. No.: XXXXXXXXXXX")
                        sw.WriteLine("        Date Issued: XX/XX/XXXX")
                        sw.WriteLine("        Valid Until: XX/XX/XXXX")
                        sw.WriteLine("          PTU No.: XXXXXXXXX")
                        sw.WriteLine("        Date Issued: XX/XX/XXXX")
                        sw.WriteLine("        Valid Until: XX/XX/XXXX")
                        sw.WriteLine("----------------------------------------")
                        sw.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                        sw.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                        sw.WriteLine("         OF THE PERMIT TO USE.")
                        sw.WriteLine("")
                        sw.WriteLine("")
                        sw.WriteLine("")
                    End If


                    sw.Close()

                    Dim fillInfo As FileInfo

                    fillInfo = New FileInfo(filepath)

                    If fillInfo.Exists = True Then
                        fillInfo.Attributes = FileAttributes.Encrypted
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message, "Error writing to log file.")
                End Try

                '============APPENDED===============

                'For inserting on ejournal_tbl
                Try
                    'For creating a text file
                    Dim file As System.IO.StreamWriter

                    file = My.Computer.FileSystem.OpenTextFileWriter("C: \E-Journal\" & TextBoxBarcode.Text & ".txt", False)

                    file.WriteLine("           CAVITE SUPERFERRY")
                    file.WriteLine("            PINOY CATAMARAN")
                    file.WriteLine("          #17 SILAHIS STREET,")
                    file.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                    file.WriteLine("              PHILIPPINES")
                    file.WriteLine("      VAT REG TIN: 745-993-747-000")
                    file.WriteLine("       POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                    file.WriteLine("            MIN: XXXXXXXXXX")
                    file.WriteLine("----------------------------------------")
                    file.WriteLine("Cashier: " & LabelCashierName.Text)
                    'file.WriteLine("Station ID: " & LabelStationID.Text)
                    file.WriteLine("            OFFICIAL RECEIPT")
                    file.WriteLine("----------------------------------------")
                    file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                    file.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                    file.WriteLine("Reset Counter: " & rstCnt.ToString())
                    file.WriteLine("----------------------------------------")
                    'file.WriteLine("NAME                               " & fname.Text & " " & lname.Text)
                    'file.WriteLine("VESSEL ID                          ")
                    'file.WriteLine("SCHEDULE                           ")
                    'file.WriteLine("SEAT NO.                           ")
                    'file.WriteLine("PT. OF ORIGIN                      ")
                    'file.WriteLine("PT. OF DESTINATION                 ")
                    'file.WriteLine("TRIP FARE                          " & TextBoxTripFare.Text)
                    'file.WriteLine("BAGGAGE COST                       " & TextBoxBaggageCost.Text)
                    'file.WriteLine("CARD AMOUNT                        " & TextBoxCardAmount.Text)
                    'file.WriteLine("----------------------------------------")
                    file.WriteLine("TOTAL AMOUNT                       " & LabelTotal.Text)
                    file.WriteLine("PAYMENT: " & ComboBoxPaymentMethod.Text & "     " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                    file.WriteLine("CHANGE                             " & TextBoxChange.Text)
                    file.WriteLine("----------------------------------------")
                    file.WriteLine("(V) VATable Sales                  " & LabelVATable.Text)
                    file.WriteLine("VAT Amount                         " & LabelVAT.Text)
                    file.WriteLine("(X) VAT-Exempt Sales               " & LabelVATExempt.Text)
                    If ButtonZeroRated.BackColor = Color.Gainsboro Then
                        file.WriteLine("(Z) Zero-Rated Sales               " & LabelZeroRated.Text)

                    ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                        file.WriteLine("(Z) Zero-Rated Sales               " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                    End If
                    file.WriteLine("----------------------------------------")
                    file.WriteLine("Discount                           " & LabelDiscount.Text)
                    file.WriteLine("                                   " & LabelType.Text)
                    file.WriteLine("----------------------------------------")
                    file.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                    file.WriteLine("----------------------------------------")
                    file.WriteLine("Name: _________________________________")
                    file.WriteLine("Address: ______________________________")
                    file.WriteLine("_______________________________________")
                    file.WriteLine("ID #: _________________________________")
                    file.WriteLine("OSCA/SC ID #: _________________________")
                    file.WriteLine("PWD ID #: _____________________________")
                    file.WriteLine("TIN #: ________________________________")
                    file.WriteLine("Bus Style: ____________________________")
                    file.WriteLine("Signature: ____________________________")
                    file.WriteLine("----------------------------------------")
                    file.WriteLine("               VINTATECH")
                    file.WriteLine("        Unit 507 F&L Bldg. 2211")
                    file.WriteLine("     Commonwealth, Holy Spirit 1127")
                    file.WriteLine("              Quezon City")
                    file.WriteLine("      VAT REG TIN: 745-993-747-000")
                    file.WriteLine("        Accred. No.: XXXXXXXXXXX")
                    file.WriteLine("        Date Issued: XX/XX/XXXX")
                    file.WriteLine("        Valid Until: XX/XX/XXXX")
                    file.WriteLine("          PTU No.: XXXXXXXXX")
                    file.WriteLine("        Date Issued: XX/XX/XXXX")
                    file.WriteLine("        Valid Until: XX/XX/XXXX")
                    file.WriteLine("----------------------------------------")
                    file.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                    file.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                    file.WriteLine("         OF THE PERMIT TO USE.")

                    '----------- FOR MMDA
                    'file.WriteLine("METROPOLITAN MANILA DEVELOPMENT AUTHORITY")
                    'file.WriteLine("----------------------------------------")

                    'file.WriteLine("Cashier: " & LabelCashierName.Text)
                    'file.WriteLine("Station ID: " & OriginID.Text)
                    'file.WriteLine("               DEMO TICKET")
                    'file.WriteLine("----------------------------------------")

                    'file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                    'file.WriteLine("OR #: " & TextBoxBarcode.Text)
                    'file.WriteLine("----------------------------------------")

                    'file.WriteLine("NAME                " & fname.Text & " " & lname.Text)
                    'file.WriteLine("PT. OF ORIGIN              " & ButtonOrigin.Text)
                    'file.WriteLine("PT. OF DESTINATION     " & ButtonDestination.Text)
                    'file.WriteLine("TRIP FARE                          " & LabelFare.Text)
                    'file.WriteLine("----------------------------------------")

                    'file.WriteLine("TOTAL AMOUNT                       " & TextBoxPayment.Text)
                    'file.WriteLine("CASH                               " & TextBoxMoney.Text & ".00")
                    'file.WriteLine("CHANGE                             " & TextBoxChange.Text)
                    'file.WriteLine("Discount                           " & LabelDiscount.Text)
                    'file.WriteLine("----------------------------------------")

                    'file.WriteLine("           A DEMO PROJECT BY")
                    'file.WriteLine("               VINTATECH")
                    '----------- FOR MMDA
                    file.Close()

                    'For uploading a text file

                    Dim rawData() As Byte = IO.File.ReadAllBytes("C:\E-Journal\" & TextBoxBarcode.Text & ".txt")

                    'Dim qdEJ As String = "INSERT INTO ejournal_tbl(id, pos_id, cashier_name, station, payment_date, payment_time, or_no, passenger_name, trip_fare, baggage_cost, card_amount, total_amount, cash, change_amount, discount, vatable, vat_exempt, zero_rated, vat, type_of_transaction, void_status, text_file, upload)
                    'values (@id, @pos_id, @cashier_name, @station, @payment_date, @payment_time, @or_no, @passenger_name, @trip_fare, @baggage_cost, @card_amount, @total_amount, @cash, @change_amount, @discount, @vatable, @vat_exempt, @zero_rated, @vat, @type_of_transaction, @void_status, @text_file, @upload)"


                    'THIS LAST TOUCH SHOULD CHANGE THE TABLE INDEX such: or_tbl, e-journal_tbl, audit trail'
                    Dim qdEJ As String = "INSERT INTO ejournal_tbl(id, pos_id, cashier_name, payment_date, payment_time, or_no, total_amount, payment_method, cash, change_amount, discount, vatable, vat_exempt, zero_rated, vat, type_of_transaction, void_status, text_file, upload)
                                                              values (@id, @pos_id, @cashier_name, @payment_date, @payment_time, @or_no, @total_amount, @payment_method, @cash, @change_amount, @discount, @vatable, @vat_exempt, @zero_rated, @vat, @type_of_transaction, @void_status, @text_file, @upload)"

                    Dim cmdEJ As New MySqlCommand(qdEJ) With {.Connection = conn}

                    cmdEJ.CommandType = CommandType.Text
                    cmdEJ.Parameters.AddWithValue("@id", TextBoxBarcode.Text)
                    cmdEJ.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                    cmdEJ.Parameters.AddWithValue("@cashier_name", LabelCashierName.Text)
                    'cmdEJ.Parameters.AddWithValue("@station", LabelStationID.Text)
                    cmdEJ.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))
                    cmdEJ.Parameters.AddWithValue("@payment_time", Now.ToString("HH:mm"))
                    cmdEJ.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)
                    'cmdEJ.Parameters.AddWithValue("@passenger_name", fname.Text & " " & lname.Text)
                    'cmdEJ.Parameters.AddWithValue("@trip_fare", TextBoxTripFare.Text)
                    'cmdEJ.Parameters.AddWithValue("@baggage_cost", TextBoxBaggageCost.Text)
                    'cmdEJ.Parameters.AddWithValue("@card_amount", TextBoxCardAmount.Text)
                    cmdEJ.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                    cmdEJ.Parameters.AddWithValue("@payment_method", ComboBoxPaymentMethod.Text)
                    cmdEJ.Parameters.AddWithValue("@cash", Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                    cmdEJ.Parameters.AddWithValue("@change_amount", TextBoxChange.Text)
                    cmdEJ.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                    cmdEJ.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                    cmdEJ.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                    cmdEJ.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                    cmdEJ.Parameters.AddWithValue("@vat", LabelVAT.Text)
                    cmdEJ.Parameters.AddWithValue("@type_of_transaction", LabelType.Text)
                    cmdEJ.Parameters.AddWithValue("@void_status", "no")
                    cmdEJ.Parameters.AddWithValue("@text_file", rawData)
                    cmdEJ.Parameters.AddWithValue("@upload", "no")

                    Try
                        conn.Open()
                        cmdEJ.ExecuteReader()

                        conn.Close()

                        'inserting in audit_trail_tbl
                        Dim audit As String = "INSERT INTO audit_trail_tbl(pos_id, or_no, date, time, cashier, activity_performed, vatable, vat, vat_exempt, zero_rated, discount, total_amount, upload) VALUES 
                                                      (@pos_id, @or_no, @date, @time, @cashier, @activity_performed, @vatable, @vat, @vat_exempt, @zero_rated, @discount, @total_amount, @upload)"
                        Dim cmdaudit As New MySqlCommand(audit) With {.Connection = conn}

                        cmdaudit.CommandType = CommandType.Text
                        cmdaudit.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                        cmdaudit.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)
                        cmdaudit.Parameters.AddWithValue("@date", Today.ToString("yyyy-MM-dd"))
                        cmdaudit.Parameters.AddWithValue("@time", Now.ToString("HH:mm"))
                        cmdaudit.Parameters.AddWithValue("@cashier", LabelCashierID.Text)
                        cmdaudit.Parameters.AddWithValue("@activity_performed", "Card Sales Transaction")
                        cmdaudit.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                        cmdaudit.Parameters.AddWithValue("@vat", LabelVAT.Text)
                        cmdaudit.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                        cmdaudit.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                        cmdaudit.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                        cmdaudit.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                        cmdaudit.Parameters.AddWithValue("@upload", "no")


                        Try
                            conn.Open()
                            cmdaudit.ExecuteReader()

                            conn.Close()

                            'ACCUMULATED AMOUNT TBL
                            'for fetching value
                            Dim ccmd As New MySqlCommand
                            Dim ddt As New DataTable()
                            Dim adapter As New MySqlDataAdapter

                            'select if cashier and date is already inserted in accumulated_amount_tbl
                            Try
                                ccmd.CommandText = "SELECT * FROM accumulated_amount_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'"
                                adapter.SelectCommand = ccmd
                                adapter.SelectCommand.Connection = conn
                                adapter.Fill(ddt)

                                If ddt.Rows.Count > 0 Then
                                    conn.Open()

                                    Try
                                        'for updating accumulated_amount_tbl with new inserted data
                                        Dim ccdd As New MySqlCommand("UPDATE accumulated_amount_tbl SET pos_id = '" & LabelPOSno.Text & "', 
                                                        end_or_no = '" & TextBoxBarcode.Text & "',  
                                                        no_of_transaction = (SELECT COUNT(or_no) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'), 
                                                        sales = (SELECT SUM(vatable + vat_exempt + zero_rated + vat + total_amount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        discount = (SELECT SUM(discount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vatable = (SELECT SUM(vatable) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vat_exempt = (SELECT SUM(vat_exempt) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vat = (SELECT SUM(vat) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        zero_rated = (SELECT SUM(zero_rated) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        grand_total = (SELECT (SELECT SUM(vatable + vat_exempt + zero_rated + vat + total_amount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no') - (SELECT IFNULL(SUM(NULLIF(vatable + vat_exempt + zero_rated + vat + total_amount, 0)), 0) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'yes')),
                                                        updated = 'yes'
                                                        WHERE user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'", conn)

                                        Dim readerr As MySqlDataReader

                                        readerr = ccdd.ExecuteReader
                                        readerr.Close()
                                        conn.Close()

                                        'for dual printing
                                        If (page_number > 1) Then page_number = 0

                                        ''PRINT set print dialog something
                                        ''Set print dialog
                                        'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                                        'PrintPreviewDialog1.Document = PrintDocument1

                                        'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                                        'PrintPreviewDialog1.ShowDialog()

                                        ''TO PRINT IMMEDIATELY
                                        PrintDocument1.Print()


                                    Catch ex As Exception
                                        MsgBox(ex.Message & "UPDATE", MessageBoxIcon.Warning)
                                        conn.Close()
                                    End Try
                                Else

                                    conn.Close()

                                    'for inserting accumulated amounts
                                    ''TO EDIT
                                    Dim AA As String = "INSERT INTO accumulated_amount_tbl (user_id, 
                                                                                                pos_id, 
                                                                                                payment_date, 
                                                                                                begin_or_no, 
                                                                                                end_or_no, 
                                                                                                no_of_transaction, 
                                                                                                no_of_void, 
                                                                                                sales, 
                                                                                                discount, 
                                                                                                vatable, 
                                                                                                vat_exempt, 
                                                                                                vat, 
                                                                                                zero_rated, 
                                                                                                z_counter, 
                                                                                                begin_void_no, 
                                                                                                end_void_no, 
                                                                                                void, 
                                                                                                grand_total, 
                                                                                                printed, 
                                                                                                upload)
                                                                                        Select '" & LabelCashierID.Text & "', 
                                                                                               '" & LabelPOSno.Text & "', 
                                                                                               '" & todaysdate & "', 
                                                                                               MIN(or_no), 
                                                                                               MAX(or_no), 
                                                                                               COUNT(or_no), 
                                                                                               0, 
                                                                                               SUM(vatable + vat_exempt + zero_rated + vat + total_amount), 
                                                                                               SUM(discount), 
                                                                                               SUM(vatable), 
                                                                                               SUM(vat_exempt), 
                                                                                               SUM(vat), 
                                                                                               SUM(zero_rated), 
                                                                                               0, 
                                                                                               0, 
                                                                                               0, 
                                                                                               '0.00', 
                                                                                               SUM(vatable + vat_exempt + zero_rated + vat + total_amount), 
                                                                                               'no', 
                                                                                               'no' 
                                                                                               FROM or_tbl WHERE user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'"
                                    Dim cmmd As New MySqlCommand(AA) With {.Connection = conn}

                                    Try
                                        conn.Open()
                                        cmmd.ExecuteReader()

                                        'for dual printing
                                        If (page_number > 1) Then page_number = 0

                                        ''PRINT set print dialog something
                                        ''Set print dialog
                                        'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                                        'PrintPreviewDialog1.Document = PrintDocument1

                                        'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                                        'PrintPreviewDialog1.ShowDialog()

                                        ''TO PRINT IMMEDIATELY
                                        PrintDocument1.Print()


                                    Catch ex As Exception
                                        MsgBox(ex.Message & "insert accumulated", MessageBoxIcon.Warning)
                                        conn.Close()
                                    End Try

                                End If

                            Catch ex As Exception
                                MsgBox(ex.Message & "LAST", MessageBoxIcon.Warning)
                                conn.Close()
                            End Try

                            conn.Close()

                        Catch ex As Exception
                            MsgBox(ex.Message & "AUDIT", MessageBoxIcon.Warning)
                            conn.Close()
                        End Try

                    Catch ex As Exception
                        MsgBox(ex.Message & "try ejournal insert", MessageBoxIcon.Warning)
                        conn.Close()
                    End Try     'END TRY PROCESS OF AUDIT TRAIL INSERT INTO TABLE



                    conn.Close()

                    'MessageBox.Show("File Inserted into database successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    'DELETED TEMPORARY FILE AFTER SAVING IT TO DATABASE
                    My.Computer.FileSystem.DeleteFile("C:\E-Journal\" & TextBoxBarcode.Text & ".txt")

                    'Catch ex As Exception
                    '    MsgBox(ex.Message & "try seat update", MessageBoxIcon.Warning)
                    '    conn.Close()
                    'End Try

                    conn.Close()

                Catch ex As Exception
                    MsgBox(ex.Message & "try insert into or_tbl", MessageBoxIcon.Warning)
                    conn.Close()
                End Try                     'END TRY PROCESS OF EJOURNAL TABLE

                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message & "sched_id", MessageBoxIcon.Warning)
            End Try
            conn.Close()

            'End If ' End of If Condition


            '============================== CARD ===================================================


            '============================== EMPLOYEE ===================================================

            'for employee
        ElseIf ButtonEmployee.BackColor = Color.Gray Then

            auto()

            'for inserting transaction
            Dim qd As String = "INSERT INTO or_tbl(id, ticket_no, passenger_id, payment_date, payment_time, station_id, pos_id, user_id, type_of_transaction, upload) VALUES 
                                                  (@id, @ticket_no, @passenger_id, @payment_date, @payment_time, @station_id, @pos_id, @user_id, @type_of_transaction, @upload)"
            Dim cmd As New MySqlCommand(qd) With {.Connection = conn}

            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@id", randomString)
            cmd.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
            cmd.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@payment_time", Now.ToString("HH:mm"))
            cmd.Parameters.AddWithValue("@station_id", LabelStationID.Text)
            cmd.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
            cmd.Parameters.AddWithValue("@user_id", LabelCashierID.Text)
            cmd.Parameters.AddWithValue("@type_of_transaction", LabelType.Text)
            cmd.Parameters.AddWithValue("@upload", "no")

            Try
                conn.Open()
                cmd.ExecuteReader()
                conn.Close()

                Try

                    conn.Close()

                    'inserting in audit_trail_tbl
                    Dim audit As String = "INSERT INTO audit_trail_tbl(pos_id, ticket_no, date, time, cashier, activity_performed, vatable, vat, vat_exempt, zero_rated, discount, total_amount, upload) VALUES 
                                                                          (@pos_id, @ticket_no, @date, @time, @cashier, @activity_performed, @vatable, @vat, @vat_exempt, @zero_rated, @discount, @total_amount, @upload)"
                    Dim cmdaudit As New MySqlCommand(audit) With {.Connection = conn}

                    cmdaudit.CommandType = CommandType.Text
                    cmdaudit.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                    cmdaudit.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
                    cmdaudit.Parameters.AddWithValue("@date", Today.ToString("yyyy-MM-dd"))
                    cmdaudit.Parameters.AddWithValue("@time", Now.ToString("HH:mm"))
                    cmdaudit.Parameters.AddWithValue("@cashier", LabelCashierID.Text)
                    cmdaudit.Parameters.AddWithValue("@activity_performed", "Employee Ticket Issued")
                    cmdaudit.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                    cmdaudit.Parameters.AddWithValue("@vat", LabelVAT.Text)
                    cmdaudit.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                    cmdaudit.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                    cmdaudit.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                    cmdaudit.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                    cmdaudit.Parameters.AddWithValue("@upload", "no")


                    Try
                        conn.Open()
                        cmdaudit.ExecuteReader()

                        conn.Close()

                        If (page_number > 1) Then page_number = 0

                        ''PRINT set print dialog something
                        ''Set print dialog
                        'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                        'PrintPreviewDialog1.Document = PrintDocument1

                        'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                        'PrintPreviewDialog1.ShowDialog()

                        ''TO PRINT IMMEDIATELY
                        PrintDocument1.Print()


                    Catch ex As Exception
                        MsgBox(ex.Message & "PRINTING", MessageBoxIcon.Warning)
                        conn.Close()
                    End Try

                    conn.Close()

                Catch ex As Exception
                    MsgBox(ex.Message & "AUDIT", MessageBoxIcon.Warning)
                    conn.Close()
                End Try

                conn.Close()

            Catch ex As Exception
                MsgBox(ex.Message & "try insert into or_tbl", MessageBoxIcon.Warning)
                conn.Close()
            End Try


            conn.Close()

            '============================== EMPLOYEE ===================================================

            '============================== OFFICIAL RECEIPT =========================================================

        ElseIf ButtonCard.BackColor = Color.Gainsboro And
            ButtonEmployee.BackColor = Color.Gainsboro Then

            'for calling auto (payment id)
            auto()

            'for combining vessel id and time
            'Dim sched_ID As String
            'conn.Open()
            'combo = ComboBoxTime.Text.Split("-")
            'Dim comb1 = combo(1).ToString
            'Dim comb = combo(0).ToString

            'for selecting sched id
            'Dim cd As New MySqlCommand("SELECT sched_id FROM vesselsched_tbl WHERE @vessel_id = vessel_id and @departure_time = departure_time", conn)
            'cd.CommandType = CommandType.Text
            'cd.Parameters.AddWithValue("@vessel_id", comb)
            'cd.Parameters.AddWithValue("@departure_time", comb1)
            'Dim sda As New MySqlDataAdapter(cd)
            'Dim dt As New DataTable
            'cd.CommandTimeout = 0

            Try
                'sda.Fill(dt)

                'sched_ID = dt.Rows(0)(0).ToString
                'conn.Close()

                'for inserting transaction
                'Dim qd As String = "INSERT INTO or_tbl(id, ticket_no, or_no, passenger_id, sched_id, seat_id, payment_date, payment_time, station_id, dest_id, pos_id, user_id, trip_fare, baggage_cost, card_amount, vatable, vat_exempt, zero_rated, vat, discount, total_amount, type_of_transaction, void_status, upload) VALUES 
                '                                     (@id, @ticket_no, @or_no, @passenger_id, @sched_id, (SELECT seat_id FROM seat_tbl WHERE vessel_id = '" & comb & "' AND seat_no = '" & TextBoxSeat.Text & "'), @payment_date, @payment_time, @station_id, @dest_id, @pos_id, @user_id, @trip_fare, @baggage_cost, @card_amount, @vatable, @vat_exempt, @zero_rated, @vat, @discount, @total_amount, @type_of_transaction, @void_status, @upload)"

                Dim qd As String = "INSERT INTO or_tbl(id, or_no, payment_date, payment_time, pos_id, trip_fare, vatable, vat_exempt, zero_rated, vat, discount, total_amount, payment_method, type_of_transaction, void_status, upload) VALUES 
                                                          (@id, @or_no, @payment_date, @payment_time, @pos_id, @trip_fare, @vatable, @vat_exempt, @zero_rated, @vat, @discount, @total_amount, @payment_method, @type_of_transaction, @void_status, @upload)"


                Dim cmd As New MySqlCommand(qd) With {.Connection = conn}

                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@id", randomString)
                'cmd.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
                cmd.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)
                'cmd.Parameters.AddWithValue("@passenger_id", Passenger_idTextBox.Text)
                'cmd.Parameters.AddWithValue("@sched_id", sched_ID)
                cmd.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@payment_time", Now.ToString("HH:mm"))
                'cmd.Parameters.AddWithValue("@station_id", OriginID.Text)
                'cmd.Parameters.AddWithValue("@dest_id", DestinationID.Text)
                cmd.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                'cmd.Parameters.AddWithValue("@user_id", LabelCashierID.Text)
                cmd.Parameters.AddWithValue("@trip_fare", TextBoxTripFare.Text)
                'cmd.Parameters.AddWithValue("@baggage_cost", TextBoxBaggageCost.Text)
                'cmd.Parameters.AddWithValue("@card_amount", TextBoxCardAmount.Text)
                cmd.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                cmd.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                cmd.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                cmd.Parameters.AddWithValue("@vat", LabelVAT.Text)
                cmd.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                cmd.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                cmd.Parameters.AddWithValue("@payment_method", LabelTotal.Text)
                cmd.Parameters.AddWithValue("@type_of_transaction", LabelType.Text)
                cmd.Parameters.AddWithValue("@void_status", "no")
                cmd.Parameters.AddWithValue("@upload", "no")

                Try
                    conn.Open()
                    cmd.ExecuteReader()
                    conn.Close()
                    'for updating seat
                    Try
                        'conn.Open()
                        'Dim cdd As New MySqlCommand("Update seat_tbl set seat_status = @seat_status,  updated = @updated WHERE vessel_id = @vesssel_id AND seat_no = @seat_no", conn)
                        'cdd.CommandType = CommandType.Text
                        'cdd.Parameters.AddWithValue("@seat_status", 1)
                        'cdd.Parameters.AddWithValue("@updated", "yes")
                        'cdd.Parameters.AddWithValue("@vesssel_id", comb)
                        'cdd.Parameters.AddWithValue("@seat_no", TextBoxSeat.Text)
                        'Dim reader As MySqlDataReader

                        'reader = cdd.ExecuteReader
                        'conn.Close()

                        '==================================================APPENDED OR=============================================

                        Try

                            Dim filepath As String = "C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"
                            '' Dim filepath As String = "C:\E-Journal\" & "2019-11-14" & ".txt"


                            Dim sw As StreamWriter
                            '  Dim fs As FileStream


                            ' Dim fs As FileStream


                            If (Not File.Exists(filepath)) Then

                                sw = File.CreateText(filepath)
                                sw.WriteLine("           CAVITE SUPERFERRY")
                                sw.WriteLine("            PINOY CATAMARAN")
                                sw.WriteLine("          #17 SILAHIS STREET,")
                                sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                                sw.WriteLine("              PHILIPPINES")
                                sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                                sw.WriteLine("      POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                                sw.WriteLine("            MIN: XXXXXXXXXX")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Cashier: " & LabelCashierName.Text)
                                'sw.WriteLine("Station ID: " & OriginID.Text)
                                sw.WriteLine("            OFFICIAL RECEIPT")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                                sw.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                                sw.WriteLine("----------------------------------------")
                                'sw.WriteLine("NAME                                " & fname.Text & " " & lname.Text)
                                'sw.WriteLine("VESSEL ID                           " & comb)
                                'sw.WriteLine("SCHEDULE                            " & comb1)
                                'sw.WriteLine("SEAT NO.                            " & TextBoxSeat.Text)
                                'sw.WriteLine("PT. OF ORIGIN                       " & ButtonOrigin.Text)
                                'sw.WriteLine("PT. OF DESTINATION                  " & ButtonDestination.Text)
                                sw.WriteLine("TRIP FARE                           " & TextBoxTripFare.Text)
                                'sw.WriteLine("BAGGAGE COST                        " & TextBoxBaggageCost.Text)
                                'sw.WriteLine("CARD AMOUNT                         " & TextBoxCardAmount.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("TOTAL AMOUNT                        " & LabelTotal.Text)
                                sw.WriteLine("Payment Method                      " & ComboBoxPaymentMethod.Text)
                                sw.WriteLine("CASH                                " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                                sw.WriteLine("CHANGE                              " & TextBoxChange.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("(V) VATable Sales                   " & LabelVATable.Text)
                                sw.WriteLine("VAT Amount                          " & LabelVAT.Text)
                                sw.WriteLine("(X) VAT-Exempt Sales                " & LabelVATExempt.Text)
                                If ButtonZeroRated.BackColor = Color.Gainsboro Then
                                    sw.WriteLine("(Z) Zero-Rated Sales                " & LabelZeroRated.Text)

                                ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                                    sw.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                                End If
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Discount                            " & LabelDiscount.Text)
                                sw.WriteLine("                                    " & LabelType.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Name: _________________________________")
                                sw.WriteLine("Address: ______________________________")
                                sw.WriteLine("_______________________________________")
                                sw.WriteLine("ID #: _________________________________")
                                sw.WriteLine("OSCA/SC ID #: _________________________")
                                sw.WriteLine("PWD ID #: _____________________________")
                                sw.WriteLine("TIN #: ________________________________")
                                sw.WriteLine("Bus Style: ____________________________")
                                sw.WriteLine("Signature: ____________________________")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("               VINTATECH")
                                sw.WriteLine("        Unit 507 F&L Bldg. 2211")
                                sw.WriteLine("     Commonwealth, Holy Spirit 1127")
                                sw.WriteLine("              Quezon City")
                                sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                                sw.WriteLine("        Accred. No.: XXXXXXXXXXX")
                                sw.WriteLine("        Date Issued: XX/XX/XXXX")
                                sw.WriteLine("        Valid Until: XX/XX/XXXX")
                                sw.WriteLine("          PTU No.: XXXXXXXXX")
                                sw.WriteLine("        Date Issued: XX/XX/XXXX")
                                sw.WriteLine("        Valid Until: XX/XX/XXXX")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                                sw.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                                sw.WriteLine("         OF THE PERMIT TO USE.")
                                sw.WriteLine("")
                                sw.WriteLine("")
                                sw.WriteLine("")

                            Else
                                sw = File.AppendText(filepath)
                                sw.WriteLine("           CAVITE SUPERFERRY")
                                sw.WriteLine("            PINOY CATAMARAN")
                                sw.WriteLine("          #17 SILAHIS STREET,")
                                sw.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                                sw.WriteLine("              PHILIPPINES")
                                sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                                sw.WriteLine("      POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                                sw.WriteLine("            MIN: XXXXXXXXXX")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Cashier: " & LabelCashierName.Text)
                                'sw.WriteLine("Station ID: " & OriginID.Text)
                                sw.WriteLine("            OFFICIAL RECEIPT")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                                sw.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                                sw.WriteLine("----------------------------------------")
                                'sw.WriteLine("NAME                                " & fname.Text & " " & lname.Text)
                                'sw.WriteLine("VESSEL ID                           " & comb)
                                'sw.WriteLine("SCHEDULE                            " & comb1)
                                'sw.WriteLine("SEAT NO.                            " & TextBoxSeat.Text)
                                'sw.WriteLine("PT. OF ORIGIN                       " & ButtonOrigin.Text)
                                'sw.WriteLine("PT. OF DESTINATION                  " & ButtonDestination.Text)
                                sw.WriteLine("TRIP FARE                           " & TextBoxTripFare.Text)
                                'sw.WriteLine("BAGGAGE COST                        " & TextBoxBaggageCost.Text)
                                'sw.WriteLine("CARD AMOUNT                         " & TextBoxCardAmount.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("TOTAL AMOUNT                        " & LabelTotal.Text)
                                sw.WriteLine("Payment Method                        " & ComboBoxPaymentMethod.Text)
                                sw.WriteLine("CASH                                " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                                sw.WriteLine("CHANGE                              " & TextBoxChange.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("(V) VATable Sales                   " & LabelVATable.Text)
                                sw.WriteLine("VAT Amount                          " & LabelVAT.Text)
                                sw.WriteLine("(X) VAT-Exempt Sales                " & LabelVATExempt.Text)
                                If ButtonZeroRated.BackColor = Color.Gainsboro Then
                                    sw.WriteLine("(Z) Zero-Rated Sales                " & LabelZeroRated.Text)

                                ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                                    sw.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                                End If
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Discount                            " & LabelDiscount.Text)
                                sw.WriteLine("                                    " & LabelType.Text)
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("Name: _________________________________")
                                sw.WriteLine("Address: ______________________________")
                                sw.WriteLine("_______________________________________")
                                sw.WriteLine("ID #: _________________________________")
                                sw.WriteLine("OSCA/SC ID #: _________________________")
                                sw.WriteLine("PWD ID #: _____________________________")
                                sw.WriteLine("TIN #: ________________________________")
                                sw.WriteLine("Bus Style: ____________________________")
                                sw.WriteLine("Signature: ____________________________")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("               VINTATECH")
                                sw.WriteLine("        Unit 507 F&L Bldg. 2211")
                                sw.WriteLine("     Commonwealth, Holy Spirit 1127")
                                sw.WriteLine("              Quezon City")
                                sw.WriteLine("      VAT REG TIN: 745-993-747-000")
                                sw.WriteLine("        Accred. No.: XXXXXXXXXXX")
                                sw.WriteLine("        Date Issued: XX/XX/XXXX")
                                sw.WriteLine("        Valid Until: XX/XX/XXXX")
                                sw.WriteLine("          PTU No.: XXXXXXXXX")
                                sw.WriteLine("        Date Issued: XX/XX/XXXX")
                                sw.WriteLine("        Valid Until: XX/XX/XXXX")
                                sw.WriteLine("----------------------------------------")
                                sw.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                                sw.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                                sw.WriteLine("         OF THE PERMIT TO USE.")
                                sw.WriteLine("")
                                sw.WriteLine("")
                                sw.WriteLine("")

                            End If


                            sw.Close()

                            Dim fillInfo As FileInfo

                            fillInfo = New FileInfo(filepath)
                            If fillInfo.Exists = True Then
                                fillInfo.Attributes = FileAttributes.Encrypted
                            End If

                        Catch ex As Exception
                            MsgBox(ex.Message, "Error writing to log file.")
                        End Try

                        '==================================================APPENDED OR=============================================

                        'For inserting on ejournal_tbl
                        Try
                            'For creating a text file
                            Dim file As System.IO.StreamWriter

                            file = My.Computer.FileSystem.OpenTextFileWriter("C: \E-Journal\" & TextBoxBarcode.Text & ".txt", False)

                            file.WriteLine("           CAVITE SUPERFERRY")
                            file.WriteLine("            PINOY CATAMARAN")
                            file.WriteLine("          #17 SILAHIS STREET,")
                            file.WriteLine("     BARANGAY TANZA, NAVOTAS CITY")
                            file.WriteLine("              PHILIPPINES")
                            file.WriteLine("      VAT REG TIN: 745-993-747-000")
                            file.WriteLine("      POS" & LabelPOSno.Text & "-SN: " & LabelSerial.Text)
                            file.WriteLine("            MIN: XXXXXXXXXX")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("Cashier: " & LabelCashierName.Text)
                            'file.WriteLine("Station ID: " & OriginID.Text)
                            file.WriteLine("            OFFICIAL RECEIPT")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                            file.WriteLine("OR #: " & TextBoxBarcode.Text & "    PHP")
                            file.WriteLine("----------------------------------------")
                            'file.WriteLine("NAME                                " & fname.Text & " " & lname.Text)
                            'file.WriteLine("VESSEL ID                           " & comb)
                            'file.WriteLine("SCHEDULE                            " & comb1)
                            'file.WriteLine("SEAT NO.                            " & TextBoxSeat.Text)
                            'file.WriteLine("PT. OF ORIGIN                       " & ButtonOrigin.Text)
                            'file.WriteLine("PT. OF DESTINATION                  " & ButtonDestination.Text)
                            file.WriteLine("TRIP FARE                           " & TextBoxTripFare.Text)
                            'file.WriteLine("BAGGAGE COST                        " & TextBoxBaggageCost.Text)
                            'file.WriteLine("CARD AMOUNT                         " & TextBoxCardAmount.Text)
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("TOTAL AMOUNT                        " & LabelTotal.Text)
                            file.WriteLine("Payment Method                        " & ComboBoxPaymentMethod.Text)
                            file.WriteLine("CASH                                " & Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                            file.WriteLine("CHANGE                              " & TextBoxChange.Text)
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("(V) VATable Sales                   " & LabelVATable.Text)
                            file.WriteLine("VAT Amount                          " & LabelVAT.Text)
                            file.WriteLine("(X) VAT-Exempt Sales                " & LabelVATExempt.Text)
                            If ButtonZeroRated.BackColor = Color.Gainsboro Then
                                file.WriteLine("(Z) Zero-Rated Sales                " & LabelZeroRated.Text)

                            ElseIf ButtonZeroRated.BackColor = Color.Gray Then
                                file.WriteLine("(Z) Zero-Rated Sales                " & "('" & sumpayment & "' - VAT)" & LabelZeroRated.Text)
                            End If
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("Discount                            " & LabelDiscount.Text)
                            file.WriteLine("                                    " & LabelType.Text)
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("   THIS SERVES AS AN OFFICIAL RECEIPT")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("Name: _________________________________")
                            file.WriteLine("Address: ______________________________")
                            file.WriteLine("_______________________________________")
                            file.WriteLine("ID #: _________________________________")
                            file.WriteLine("OSCA/SC ID #: _________________________")
                            file.WriteLine("PWD ID #: _____________________________")
                            file.WriteLine("TIN #: ________________________________")
                            file.WriteLine("Bus Style: ____________________________")
                            file.WriteLine("Signature: ____________________________")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("               VINTATECH")
                            file.WriteLine("        Unit 507 F&L Bldg. 2211")
                            file.WriteLine("     Commonwealth, Holy Spirit 1127")
                            file.WriteLine("              Quezon City")
                            file.WriteLine("      VAT REG TIN: 745-993-747-000")
                            file.WriteLine("        Accred. No.: XXXXXXXXXXX")
                            file.WriteLine("        Date Issued: XX/XX/XXXX")
                            file.WriteLine("        Valid Until: XX/XX/XXXX")
                            file.WriteLine("          PTU No.: XXXXXXXXX")
                            file.WriteLine("        Date Issued: XX/XX/XXXX")
                            file.WriteLine("        Valid Until: XX/XX/XXXX")
                            file.WriteLine("----------------------------------------")
                            file.WriteLine("THIS INVOICE/RECEIPT SHALL BE VALID FOR")
                            file.WriteLine("     FIVE (5) YEARS FROM THE DATE")
                            file.WriteLine("         OF THE PERMIT TO USE.")

                            '----------- FOR MMDA
                            'file.WriteLine("METROPOLITAN MANILA DEVELOPMENT AUTHORITY")
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine("Cashier: " & LabelCashierName.Text)
                            'file.WriteLine("Station ID: " & OriginID.Text)
                            'file.WriteLine("               DEMO TICKET")
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
                            'file.WriteLine("OR #: " & TextBoxBarcode.Text)
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine("NAME                " & fname.Text & " " & lname.Text)
                            'file.WriteLine("PT. OF ORIGIN              " & ButtonOrigin.Text)
                            'file.WriteLine("PT. OF DESTINATION     " & ButtonDestination.Text)
                            'file.WriteLine("TRIP FARE                          " & LabelFare.Text)
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine("TOTAL AMOUNT                       " & TextBoxPayment.Text)
                            'file.WriteLine("CASH                               " & TextBoxMoney.Text & ".00")
                            'file.WriteLine("CHANGE                             " & TextBoxChange.Text)
                            'file.WriteLine("Discount                           " & LabelDiscount.Text)
                            'file.WriteLine("----------------------------------------")

                            'file.WriteLine("           A DEMO PROJECT BY")
                            'file.WriteLine("               VINTATECH")
                            '----------- FOR MMDA
                            file.Close()

                            'For uploading a text file

                            Dim rawData() As Byte = IO.File.ReadAllBytes("C:\E-Journal\" & TextBoxBarcode.Text & ".txt")

                            'Dim qdEJ As String = "INSERT INTO ejournal_tbl(id, pos_id, cashier_name, station, payment_date, payment_time, ticket_no, or_no, passenger_name, vessel_id, schedule, seat_no, origin, destination, trip_fare, baggage_cost, card_amount, total_amount, cash, change_amount, discount, vatable, vat_exempt, zero_rated, vat, type_of_transaction, void_status, text_file, upload)
                            'values (@id, @pos_id, @cashier_name, @station, @payment_date, @payment_time, @ticket_no, @or_no, @passenger_name, @vessel_id, @schedule, @seat_no, @origin, @destination, @trip_fare, @baggage_cost, @card_amount, @total_amount, @cash, @change_amount, @discount, @vatable, @vat_exempt, @zero_rated, @vat, @type_of_transaction, @void_status, @text_file, @upload)"

                            Dim qdEJ As String = "INSERT INTO ejournal_tbl(id, pos_id, cashier_name, payment_date, payment_time, or_no, trip_fare, total_amount, payment_method, cash, change_amount, discount, vatable, vat_exempt, zero_rated, vat, type_of_transaction, void_status, text_file, upload)
                                  values (@id, @pos_id, @cashier_name, @payment_date, @payment_time, @or_no, @trip_fare, @total_amount, @payment_method, @cash, @change_amount, @discount, @vatable, @vat_exempt, @zero_rated, @vat, @type_of_transaction, @void_status, @text_file, @upload)"


                            Dim cmdEJ As New MySqlCommand(qdEJ) With {.Connection = conn}

                            cmdEJ.CommandType = CommandType.Text
                            cmdEJ.Parameters.AddWithValue("@id", TextBoxBarcode.Text)
                            cmdEJ.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
                            cmdEJ.Parameters.AddWithValue("@cashier_name", LabelCashierName.Text)
                            'cmdEJ.Parameters.AddWithValue("@station", OriginID.Text)
                            cmdEJ.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))
                            cmdEJ.Parameters.AddWithValue("@payment_time", Now.ToString("HH:mm"))
                            'cmdEJ.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
                            cmdEJ.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)
                            'cmdEJ.Parameters.AddWithValue("@passenger_name", fname.Text & " " & lname.Text)
                            'cmdEJ.Parameters.AddWithValue("@vessel_id", comb)
                            'cmdEJ.Parameters.AddWithValue("@schedule", comb1)
                            'cmdEJ.Parameters.AddWithValue("@seat_no", TextBoxSeat.Text)
                            'cmdEJ.Parameters.AddWithValue("@origin", ButtonOrigin.Text)
                            'cmdEJ.Parameters.AddWithValue("@destination", ButtonDestination.Text)
                            cmdEJ.Parameters.AddWithValue("@trip_fare", TextBoxTripFare.Text)
                            'cmdEJ.Parameters.AddWithValue("@baggage_cost", TextBoxBaggageCost.Text)
                            'cmdEJ.Parameters.AddWithValue("@card_amount", TextBoxCardAmount.Text)
                            cmdEJ.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                            cmdEJ.Parameters.AddWithValue("@payment", ComboBoxPaymentMethod.Text)
                            cmdEJ.Parameters.AddWithValue("@cash", Convert.ToDecimal(Val(TextBoxMoney.Text).ToString("0.00")))
                            cmdEJ.Parameters.AddWithValue("@change_amount", TextBoxChange.Text)
                            cmdEJ.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                            cmdEJ.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                            cmdEJ.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                            cmdEJ.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                            cmdEJ.Parameters.AddWithValue("@vat", LabelVAT.Text)
                            cmdEJ.Parameters.AddWithValue("@type_of_transaction", LabelType.Text)
                            cmdEJ.Parameters.AddWithValue("@void_status", "no")
                            cmdEJ.Parameters.AddWithValue("@text_file", rawData)
                            cmdEJ.Parameters.AddWithValue("@upload", "no")

                            Try
                                conn.Open()
                                cmdEJ.ExecuteReader()

                                conn.Close()

                                'inserting in audit_trail_tbl
                                Dim audit As String = "INSERT INTO audit_trail_tbl(pos_id, or_no, date, time, cashier, activity_performed, vatable, vat, vat_exempt, zero_rated, discount, total_amount, upload) VALUES 
                                                      (@pos_id, @or_no, @date, @time, @cashier, @activity_performed, @vatable, @vat, @vat_exempt, @zero_rated, @discount, @total_amount, @upload)"
                                Dim cmdaudit As New MySqlCommand(audit) With {.Connection = conn}

                                cmdaudit.CommandType = CommandType.Text
                                cmdaudit.Parameters.AddWithValue("@pos_id", TextBoxBarcode.Text)
                                'cmdaudit.Parameters.AddWithValue("@ticket_no", TextBoxBarcodeTicket.Text)
                                cmdaudit.Parameters.AddWithValue("@or_no", TextBoxBarcode.Text)
                                cmdaudit.Parameters.AddWithValue("@date", Today.ToString("yyyy-MM-dd"))
                                cmdaudit.Parameters.AddWithValue("@time", Now.ToString("HH:mm"))
                                cmdaudit.Parameters.AddWithValue("@cashier", LabelCashierID.Text)
                                cmdaudit.Parameters.AddWithValue("@activity_performed", "Fare Sales Transaction")
                                cmdaudit.Parameters.AddWithValue("@vatable", LabelVATable.Text)
                                cmdaudit.Parameters.AddWithValue("@vat", LabelVAT.Text)
                                cmdaudit.Parameters.AddWithValue("@vat_exempt", LabelVATExempt.Text)
                                cmdaudit.Parameters.AddWithValue("@zero_rated", LabelZeroRated.Text)
                                cmdaudit.Parameters.AddWithValue("@discount", LabelDiscount.Text)
                                cmdaudit.Parameters.AddWithValue("@total_amount", LabelTotal.Text)
                                cmdaudit.Parameters.AddWithValue("@upload", "no")


                                Try
                                    conn.Open()
                                    cmdaudit.ExecuteReader()

                                    conn.Close()

                                    'ACCUMULATED AMOUNT TBL
                                    'for fetching value
                                    Dim ccmd As New MySqlCommand
                                    Dim ddt As New DataTable()
                                    Dim adapter As New MySqlDataAdapter

                                    'select if cashier and date is already inserted in accumulated_amount_tbl
                                    Try
                                        ccmd.CommandText = "SELECT * FROM accumulated_amount_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'"
                                        adapter.SelectCommand = ccmd
                                        adapter.SelectCommand.Connection = conn
                                        adapter.Fill(ddt)

                                        If ddt.Rows.Count > 0 Then
                                            conn.Open()

                                            Try
                                                'for updating accumulated_amount_tbl with new inserted data
                                                Dim ccdd As New MySqlCommand("UPDATE accumulated_amount_tbl SET pos_id = '" & LabelPOSno.Text & "', 
                                                        end_or_no = '" & TextBoxBarcode.Text & "',  
                                                        no_of_transaction = (SELECT COUNT(or_no) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'), 
                                                        sales = (SELECT SUM(vatable + vat_exempt + zero_rated + vat + total_amount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        discount = (SELECT SUM(discount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vatable = (SELECT SUM(vatable) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vat_exempt = (SELECT SUM(vat_exempt) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        vat = (SELECT SUM(vat) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        zero_rated = (SELECT SUM(zero_rated) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no'), 
                                                        grand_total = (SELECT (SELECT SUM(vatable + vat_exempt + zero_rated + vat + total_amount) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'no') - (SELECT IFNULL(SUM(NULLIF(vatable + vat_exempt + zero_rated + vat + total_amount, 0)), 0) FROM or_tbl where user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "' AND void_status = 'yes')),
                                                        updated = 'yes'
                                                        WHERE user_id = '" & LabelCashierID.Text & "' AND payment_date = '" & todaysdate & "'", conn)

                                                Dim readerr As MySqlDataReader

                                                readerr = ccdd.ExecuteReader
                                                readerr.Close()
                                                conn.Close()

                                                'for dual printing
                                                If (page_number > 1) Then page_number = 0

                                                ''PRINT set print dialog something
                                                ''Set print dialog
                                                'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                                                'PrintPreviewDialog1.Document = PrintDocument1

                                                'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                                                'PrintPreviewDialog1.ShowDialog()

                                                ''TO PRINT IMMEDIATELY
                                                PrintDocument1.Print()


                                            Catch ex As Exception
                                                MsgBox(ex.Message & "UPDATE", MessageBoxIcon.Warning)
                                                conn.Close()
                                            End Try
                                        Else

                                            conn.Close()

                                            'for inserting accumulated amounts
                                            ''TO EDIT
                                            Dim AA As String = "INSERT INTO accumulated_amount_tbl (user_id, 
                                                                                                        pos_id, 
                                                                                                        payment_date, 
                                                                                                        begin_or_no, 
                                                                                                        end_or_no, 
                                                                                                        no_of_transaction, 
                                                                                                        no_of_void, 
                                                                                                        sales, 
                                                                                                        discount, 
                                                                                                        vatable, 
                                                                                                        vat_exempt, 
                                                                                                        vat, 
                                                                                                        zero_rated, 
                                                                                                        z_counter, 
                                                                                                        begin_void_no, 
                                                                                                        end_void_no, 
                                                                                                        void, 
                                                                                                        grand_total, 
                                                                                                        printed, 
                                                                                                        upload)
                                                                                                Select '" & LabelCashierID.Text & "', 
                                                                                                       '" & LabelPOSno.Text & "', 
                                                                                                       '" & todaysdate & "', 
                                                                                                       MIN(or_no), 
                                                                                                       MAX(or_no), 
                                                                                                       COUNT(or_no), 
                                                                                                       0, 
                                                                                                       SUM(vatable + vat_exempt + zero_rated + vat + total_amount), 
                                                                                                       SUM(discount), 
                                                                                                       SUM(vatable), 
                                                                                                       SUM(vat_exempt), 
                                                                                                       SUM(vat), 
                                                                                                       SUM(zero_rated), 
                                                                                                       0, 
                                                                                                       0, 
                                                                                                       0, 
                                                                                                       '0.00', 
                                                                                                       SUM(vatable + vat_exempt + zero_rated + vat + total_amount), 
                                                                                                       'no', 
                                                                                                       'no' 
                                                                                                       FROM or_tbl WHERE user_id = '" & LabelCashierID.Text & "' and payment_date = '" & todaysdate & "'"
                                            Dim cmmd As New MySqlCommand(AA) With {.Connection = conn}

                                            Try
                                                conn.Open()
                                                cmmd.ExecuteReader()

                                                'for dual printing
                                                If (page_number > 1) Then page_number = 0

                                                ''PRINT set print dialog something
                                                ''Set print dialog
                                                'Dim PrintPreviewDialog1 As PrintPreviewDialog = New PrintPreviewDialog
                                                'PrintPreviewDialog1.Document = PrintDocument1

                                                'AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintDocument1_PrintPage
                                                'PrintPreviewDialog1.ShowDialog()

                                                ''TO PRINT IMMEDIATELY
                                                PrintDocument1.Print()


                                            Catch ex As Exception
                                                MsgBox(ex.Message & "insert accumulated", MessageBoxIcon.Warning)
                                                conn.Close()
                                            End Try

                                        End If

                                    Catch ex As Exception
                                        MsgBox(ex.Message & "LAST", MessageBoxIcon.Warning)
                                        conn.Close()
                                    End Try

                                    conn.Close()

                                Catch ex As Exception
                                    MsgBox(ex.Message & "AUDIT", MessageBoxIcon.Warning)
                                    conn.Close()
                                End Try

                            Catch ex As Exception
                                MsgBox(ex.Message & "try ejournal insert", MessageBoxIcon.Warning)
                                conn.Close()
                            End Try

                            conn.Close()

                            'MessageBox.Show("File Inserted into database successfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                            'JIM ----DELETED TEMPORARY FILE AFTER SAVING IT TO DATABASE
                            My.Computer.FileSystem.DeleteFile("C:\E-Journal\" & TextBoxBarcode.Text & ".txt")

                        Catch ex As Exception
                            MsgBox(ex.Message & "try seat update", MessageBoxIcon.Warning)
                            conn.Close()
                        End Try

                        conn.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message & "try updating seat", MessageBoxIcon.Warning)
                        conn.Close()
                    End Try

                Catch ex As Exception
                    MsgBox(ex.Message & "try insert into or_tbl", MessageBoxIcon.Warning)
                    conn.Close()
                End Try
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message & "sched_id", MessageBoxIcon.Warning)
            End Try
            conn.Close()
        End If
    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPaymentMethod.SelectedIndexChanged
    ' Label1.Text = ComboBoxPaymentMethod.Text
    'End Sub
    Private Function GetNextUniqueTranNo(strConn As String,
                                     Optional currentValue As String = Nothing) As String

        ' If no current value provided, get last value from DB
        If String.IsNullOrEmpty(currentValue) Then
            Using conn As New MySqlConnection(strConn),
              cmd As New MySqlCommand("
                    SELECT or_no
                    FROM or_tbl
                    ORDER BY or_no DESC
                    LIMIT 1;", conn)

                conn.Open()
                Dim result = cmd.ExecuteScalar()
                If result Is Nothing OrElse result Is DBNull.Value Then
                    currentValue = "000000-00-000000"
                Else
                    currentValue = CStr(result)
                End If
            End Using
        End If

        Dim counter As New SaleInvoiceCounter(currentValue)

        Using conn As New MySqlConnection(strConn)
            conn.Open()

            Dim exists As Boolean = True

            While exists
                ' Check if this TranNo already exists
                Dim sql As String = "SELECT COUNT(*) FROM or_tbl WHERE or_no = @or_no"
                Using cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@or_no", counter.CounterValue)
                    Dim cnt As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    exists = (cnt > 0)
                End Using

                ' If it exists, increment and check again
                If exists Then
                    counter.Increment()
                End If
            End While
        End Using

        Return counter.CounterValue
    End Function

    ' Inserting data in different table
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        Dim or_no As String = TextBoxBarcode.Text

        Try
            If (TextBoxQty.Text = "" Or ButtonRegularRide.Text = "" Or ButtonPromo.Text = "" Or ComboBoxPaymentMethod.Text = "") Then
                MsgBox("Please! Fill In the Field")
                Exit Sub
            End If

            ' 1) Prepare E-Journal file + receiptText FIRST (or you can do after commit)

            Dim filepath As String = "C:\E-Journal\" & DateTime.Today.ToString("yyyy-MM-dd") & ".txt"
            Dim receiptText As String = ""

            Try
                ' create/append file
                Dim printName As String = ""
                Dim printID As String = ""
                Dim printTIN As String = ""
                Dim printAddress As String = ""

                ' 2. Check if the cashier actually entered any customer details
                If eJournalCustomerData.CustomerList.Count > 0 Then

                    ' They did! Grab the first customer in the list to overwrite the empty strings
                    Dim currentCustomer As CustomerInfo = eJournalCustomerData.CustomerList(0)
                    printName = currentCustomer.Name
                    printID = currentCustomer.ID
                    printTIN = currentCustomer.TIN
                    printAddress = currentCustomer.Address
                End If

                ' 3. Call your print method ONCE using the variables. 
                ' If no details were entered, it safely passes the empty strings!
                If Not File.Exists(filepath) Then
                    Receipt_OR_Printed.PrintReceipt(filepath, LabelPOSno.Text, LabelSerial.Text, LabelCashierName.Text,
            TextBoxBarcode.Text, "", "", "", "", "", "", LabelTotal.Text, ComboBoxPaymentMethod.Text,
            lblApprovedCode.Text, Convert.ToDecimal(Val(TextBoxMoney.Text)).ToString("0.00"),
            TextBoxChange.Text, printName, printID, printTIN,
            printAddress, LabelVATable.Text, LabelVAT.Text, LabelVATExempt.Text,
            LabelZeroRated.Text, LabelDiscount.Text, LabelType.Text, LabelLessVat.Text)
                Else
                    Receipt_OR_Printed.AppendEJournalReceipt(filepath, LabelPOSno.Text, LabelSerial.Text, LabelCashierName.Text,
            TextBoxBarcode.Text, "", "", "", "", "", "", LabelTotal.Text, ComboBoxPaymentMethod.Text,
            lblApprovedCode.Text, Convert.ToDecimal(Val(TextBoxMoney.Text)).ToString("0.00"),
            TextBoxChange.Text, printName, printID, printTIN,
            printAddress, LabelVATable.Text, LabelVAT.Text, LabelVATExempt.Text,
            LabelZeroRated.Text, LabelDiscount.Text, LabelType.Text, LabelLessVat.Text)
                End If

                ' extract receiptText from file (your existing logic)
                Dim lines() As String = IO.File.ReadAllLines(filepath)

                Dim siIndex As Integer = -1
                For i As Integer = 0 To lines.Length - 1
                    If lines(i).Trim().Contains("SI #:") AndAlso lines(i).Trim().Contains(or_no) Then
                        siIndex = i : Exit For
                    End If
                Next
                If siIndex = -1 Then Throw New Exception("SI # not found in text file.")

                Dim startIndex As Integer = -1
                For i As Integer = siIndex To 0 Step -1
                    If lines(i).Trim().Contains("METTACITY") Then
                        startIndex = i : Exit For
                    End If
                Next
                If startIndex = -1 Then Throw New Exception("Receipt header not found in text file.")

                Dim endIndex As Integer = -1
                For j As Integer = siIndex To lines.Length - 1
                    If lines(j).Contains("     --- END OF TRANSACTION ---") Then
                        endIndex = j : Exit For
                    End If
                Next
                If endIndex = -1 Then Throw New Exception("END OF TRANSACTION not found in text file.")

                receiptText = String.Join(Environment.NewLine, lines.Skip(startIndex).Take(endIndex - startIndex + 1))

            Catch ex As Exception
                'MsgBox("E-Journal file error: " & ex.Message, MsgBoxStyle.Critical)
                'MsgBox("Creating textfile has Issue!")
                Exit Sub
            End Try

            ' 2) NOW DO ALL DATABASE WORK IN ONE TRANSACTION (ONE CONNECTION)
            Using conn As New MySqlConnection(strConn)
                conn.Open()

                Using tx = conn.BeginTransaction()
                    Try
                        ' OR_tbl
                        Using cmd As New MySqlCommand("newORTransaction", conn, tx)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("p_id", randomString)
                            cmd.Parameters.AddWithValue("p_or_no", TextBoxBarcode.Text)
                            cmd.Parameters.AddWithValue("p_payment_date", Today.ToString("yyyy-MM-dd"))
                            cmd.Parameters.AddWithValue("p_payment_time", Now.ToString("HH:mm:ss"))
                            cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                            cmd.Parameters.AddWithValue("p_cashier_id", LabelCashierID.Text)
                            cmd.Parameters.AddWithValue("p_username", LabelCashierName.Text)
                            cmd.Parameters.AddWithValue("p_qty", LabelQtyTotal.Text)
                            cmd.Parameters.AddWithValue("p_base_price", Decimal.Parse(LabelTotal.Text))
                            cmd.Parameters.AddWithValue("p_discount", Decimal.Parse(LabelDiscount.Text))
                            cmd.Parameters.AddWithValue("p_vatable", Decimal.Parse(LabelVATable.Text))
                            cmd.Parameters.AddWithValue("p_vat", Decimal.Parse(LabelVAT.Text))
                            cmd.Parameters.AddWithValue("p_lessvat", Decimal.Parse(LabelLessVat.Text))
                            cmd.Parameters.AddWithValue("p_vat_exempt", Decimal.Parse(LabelVATExempt.Text))
                            cmd.Parameters.AddWithValue("p_zero_rated", Decimal.Parse(LabelZeroRated.Text))
                            cmd.Parameters.AddWithValue("p_total_amount", Decimal.Parse(LabelTotal.Text))
                            cmd.Parameters.AddWithValue("p_payment_method", ComboBoxPaymentMethod.Text)
                            cmd.Parameters.AddWithValue("p_cash_tendered", TextBoxMoney.Text)
                            cmd.Parameters.AddWithValue("p_change_amount", TextBoxChange.Text)
                            cmd.Parameters.AddWithValue("p_approved_code", lblApprovedCode.Text)
                            cmd.Parameters.AddWithValue("p_void_status", "no")
                            cmd.Parameters.AddWithValue("p_void_by", "")
                            cmd.Parameters.AddWithValue("p_created_by", "")
                            cmd.Parameters.AddWithValue("p_last_updated", "")
                            cmd.Parameters.AddWithValue("p_upload", "no")
                            cmd.Parameters.AddWithValue("p_updated", "no")
                            cmd.ExecuteNonQuery()

                        End Using

                        Using cmdRst As New MySqlCommand("UPDATE or_tbl SET rst_cnt = @rst_cnt WHERE id = LAST_INSERT_ID()", conn, tx)
                            cmdRst.Parameters.AddWithValue("@rst_cnt", rstCnt)
                            cmdRst.ExecuteNonQuery()
                        End Using

                        ' e_journal_tbl
                        Using cmd As New MySqlCommand("insertingEJournal", conn, tx)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("p_id", randomString)
                            cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                            cmd.Parameters.AddWithValue("p_cashier_name", LabelCashierID.Text)
                            cmd.Parameters.AddWithValue("p_payment_date", Today.ToString("yyyy-MM-dd"))
                            cmd.Parameters.AddWithValue("p_payment_time", Now.ToString("HH:mm:ss"))
                            cmd.Parameters.AddWithValue("p_or_no", TextBoxBarcode.Text)
                            cmd.Parameters.AddWithValue("p_void_no", "")
                            cmd.Parameters.AddWithValue("p_qty", LabelQtyTotal.Text)
                            cmd.Parameters.AddWithValue("p_base_price", Decimal.Parse(LabelTotal.Text))
                            cmd.Parameters.AddWithValue("p_total_amount", Decimal.Parse(LabelTotal.Text))

                            If ComboBoxPaymentMethod.Text = "CASH" Then
                                cmd.Parameters.AddWithValue("p_payment_method", ComboBoxPaymentMethod.Text)
                                cmd.Parameters.AddWithValue("p_cash_tendered", Decimal.Parse(TextBoxMoney.Text))
                                cmd.Parameters.AddWithValue("p_change_amount", Decimal.Parse(TextBoxChange.Text))
                            Else
                                cmd.Parameters.AddWithValue("p_payment_method", ComboBoxPaymentMethod.Text)
                                cmd.Parameters.AddWithValue("p_cash_tendered", 0D)
                                cmd.Parameters.AddWithValue("p_change_amount", 0D)
                            End If

                            cmd.Parameters.AddWithValue("p_discount", Decimal.Parse(LabelDiscount.Text))
                            cmd.Parameters.AddWithValue("p_vatable", Decimal.Parse(LabelVATable.Text))
                            cmd.Parameters.AddWithValue("p_vat", Decimal.Parse(LabelVAT.Text))
                            cmd.Parameters.AddWithValue("p_lessvat", Decimal.Parse(LabelLessVat.Text))
                            cmd.Parameters.AddWithValue("p_vat_exempt", Decimal.Parse(LabelVATExempt.Text))
                            cmd.Parameters.AddWithValue("p_zero_rated", Decimal.Parse(LabelZeroRated.Text))
                            cmd.Parameters.AddWithValue("p_void_status", "no")
                            cmd.Parameters.AddWithValue("p_text_file", receiptText)
                            cmd.Parameters.AddWithValue("p_upload", "no")
                            cmd.Parameters.AddWithValue("p_updated", "no")
                            cmd.ExecuteNonQuery()

                        End Using

                        ' audit_trail_tbl
                        'Dim BranchCode As String = "BR01"
                        Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

                        Using cmd As New MySqlCommand("insertingAuditTrail", conn, tx)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                            cmd.Parameters.AddWithValue("p_userid", LabelCashierID.Text)
                            cmd.Parameters.AddWithValue("p_username", LabelCashierName.Text)
                            cmd.Parameters.AddWithValue("p_approvedby", "")
                            cmd.Parameters.AddWithValue("p_activity_performed", TypeTransaction)
                            cmd.Parameters.AddWithValue("p_module", "Sales")
                            cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-SI-" & TextBoxBarcode.Text)
                            cmd.Parameters.AddWithValue("p_remarks", "New Sales")
                            cmd.ExecuteNonQuery()

                        End Using

                        ' accumulated_amount_tbl
                        Using cmd As New MySqlCommand("upsertAccumulatedAmount", conn, tx)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@p_user_id", LabelCashierID.Text)
                            cmd.Parameters.AddWithValue("@p_payment_date", todaysdate)
                            cmd.Parameters.AddWithValue("@p_pos_id", LabelPOSno.Text)
                            cmd.Parameters.AddWithValue("@p_end_or_no", TextBoxBarcode.Text)
                            cmd.ExecuteNonQuery()

                        End Using

                        ' ✅ all ok
                        tx.Commit()

                    Catch ex As Exception
                        Try : tx.Rollback() : Catch : End Try
                        'MsgBox("Transaction failed. Nothing was saved." & vbCrLf & ex.Message, MsgBoxStyle.Critical)
                        MsgBox("Transaction failed. Nothing was saved!")
                        Exit Sub
                    End Try
                End Using
            End Using

            If ComboBoxPaymentMethod.Text = "CASH" Then
                PrintMultipleTables(or_no, eJournalCustomerData.CustomerList)
                OpenTerminalDrawer()
                'PrintMultipleTables(or_no, _discountForm.DiscountedGuests)
            Else
                PrintMultipleTables(or_no, eJournalCustomerData.CustomerList)
                PrintMultipleTables(or_no, eJournalCustomerData.CustomerList)
            End If

            'For Ticket Sales
            CopyListviewCashierDataToGenerateWristband()

            'for GiftShop Sales
            'ListViewCashier.Items.Clear()
            'secondscreen.ListViewCustomerView.Clear()
            'TableLayoutPanel4.Enabled = False
            'Button13.Enabled = False

            '--------------------------------
            Button16.Enabled = True     'Enable Button for Printing Sticker
            Button12.Enabled = False    'Disable Button for Printing Receipt

            ButtonStudent.BackColor = Color.Gainsboro
            ButtonNAAC.BackColor = Color.Gainsboro
            ButtonSenior.BackColor = Color.Gainsboro
            ButtonDiplomat.BackColor = Color.Gainsboro
            ButtonSoloParent.BackColor = Color.Gainsboro
            ButtonZeroRated.BackColor = Color.Gainsboro
            ButtonPWD.BackColor = Color.Gainsboro
            ButtonEmployee.BackColor = Color.Gainsboro

            ButtonComputeTotal.BackColor = Color.Gainsboro
            secondscreen.LabelTotalCustomerView.Text = "0.00"
            secondscreen.LabelCashTendered.Text = "0.00"
            secondscreen.LabelChangeCustomerChange.Text = "0.00"
            'secondscreen.ListViewCustomerView.Clear()
            secondscreen.ListViewCustomerView.Items.Clear()
            LabelAvailed.Text = ""
            origin.Close()

            LabelTotal.Text = "0.00"
            TextBoxMoney.Text = "0.00"
            TextBoxChange.Text = "0.00"
            ComboBoxPaymentMethod.Text = ""
            TextBoxPercentDiscount.Text = ""
            lblApprovedCode.Text = ""
            TableLayoutPanel13.Enabled = False
            ButtonComputeTotal.Enabled = False

            'ExportDatabase()
            disableFunctionForAuthorizedPersonnelOnly()
        Catch ex As Exception
            MsgBox("Transaction failed. Nothing was saved!", ex.Message)
        End Try
    End Sub


    'For printing Sale Invoice Receipt 
    Private Function GetDataFromTable(tableName As String, or_no As String) As DataTable

        ' Create a DataTable to hold the fetched data
        Dim table As New DataTable()

        ' MySQL Connection string
        Using conn As New MySqlConnection(strConn)
            Try
                conn.Open()

                ' Query to fetch data from the table based on or_no
                Dim query As String = "SELECT * FROM " & tableName & " WHERE or_no = @or_no"
                If tableName = "customer_tbl" Then
                    query = "SELECT or_no, CAST(AES_DECRYPT(name, 'strdjnltmyp') AS CHAR) AS name, id_no, tin_no, address FROM customer_tbl WHERE or_no = @or_no"
                End If
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@or_no", or_no)

                ' Execute the query and fill the DataTable with the result
                Dim adapter As New MySqlDataAdapter(cmd)
                adapter.Fill(table)
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            End Try
        End Using

        Return table
    End Function

    Private Sub PrintMultipleTablesFromTextBox()
        ' Get the or_no from the TextBox
        Dim or_no As String = TextBoxBarcode.Text.Trim()

        ' Check if the or_no is empty
        If String.IsNullOrEmpty(or_no) Then
            MsgBox("Please enter a valid OR No.")
            Return
        End If

        ' Proceed to fetch and print the data using the or_no
        PrintMultipleTables(or_no, eJournalCustomerData.CustomerList)
    End Sub

    ' This method handles printing the data fetched from multiple tables
    Private Sub CenterText(e As Printing.PrintPageEventArgs, text As String, font As Font, y As Integer)
        Dim x = (e.PageBounds.Width - e.Graphics.MeasureString(text, font).Width) / 2
        e.Graphics.DrawString(text, font, Brushes.Black, x, y)
    End Sub

    ' This method handles printing the data fetched from multiple tables
    Private Sub PrintMultipleTables(or_no As String, CustomerList As List(Of CustomerInfo))
        ' Create a PrintDocument instance
        Dim printDocument As New PrintDocument()

        ' Set thermal paper size: 3 inches x 80 mm (approx. 300 x 800 pixels)
        Dim thermalPaperWidth As Integer = 300  ' 3 inches in 1/100ths of an inch
        'Dim thermalPaperHeight As Integer = 0 ' 8 inches (about 80mm) in 1/100ths of an inch
        Dim thermalPaperHeight As Integer = 1800 ' 8 inches (about 80mm) in 1/100ths of an inch
        Dim paperSize As New PaperSize("Thermal Paper", thermalPaperWidth, thermalPaperHeight)
        printDocument.DefaultPageSettings.PaperSize = paperSize

        ' Set margins
        Dim leftMargin As Integer = 5
        Dim rightMargin As Integer = 5
        Dim topMargin As Integer = 5

        ' Calculate the available width for printing (total width minus left and right margins)
        Dim maxWidth As Integer = thermalPaperWidth - leftMargin - rightMargin

        ' Fetch data from multiple tables (e.g., sales_table, customer_table) using or_no
        Dim or_items_tbl As DataTable = GetDataFromTable("or_items_tbl", or_no)
        Dim or_tbl As DataTable = GetDataFromTable("or_tbl", or_no)
        Dim card_details_tbl As DataTable = GetDataFromTable("card_details_tbl", or_no)
        Dim customer_tbl As DataTable = GetDataFromTable("customer_tbl", or_no)

        ' Set printer name dynamically here (or you can hardcode it)
        printDocument.PrinterSettings.PrinterName = "Posiflex PP9000 Printer"  ' Replace with your printer name

        ' Check if the printer is valid
        If Not printDocument.PrinterSettings.IsValid Then
            MsgBox("Printer is not valid!")
            Return
        End If

        ' Handler for the PrintPage event
        AddHandler printDocument.PrintPage, Sub(sender As Object, e As PrintPageEventArgs)
                                                Dim pos = AppConfigReader.ps
                                                Dim serial = AppConfigReader.srl
                                                Dim font As New Font("Tahoma", 9)
                                                Dim graphics As Graphics = e.Graphics
                                                Dim brush As New SolidBrush(Color.Black)

                                                Dim centerFormat As New StringFormat()
                                                centerFormat.Alignment = StringAlignment.Center
                                                centerFormat.LineAlignment = StringAlignment.Center

                                                Dim N As New Font("Tahoma", 9)
                                                Dim NB As New Font("Tahoma", 9, FontStyle.Bold)
                                                Dim S As New Font("Tahoma", 8)
                                                Dim M As New Font("Tahoma", 8, FontStyle.Bold)
                                                Dim H As New Font("Tahoma", 11)
                                                Dim B As New Font("Tahoma", 13, FontStyle.Bold)

                                                Dim yPos As Integer = 5

                                                'HEADER

                                                graphics.DrawString(CompName, B, brush, New RectangleF(leftMargin, yPos, maxWidth, 100), centerFormat)
                                                yPos += 80
                                                CenterText(e, CompNameB, N, yPos)
                                                yPos += 15
                                                CenterText(e, AddressA, N, yPos)
                                                yPos += 15
                                                CenterText(e, AddressB, N, yPos)
                                                yPos += 15
                                                CenterText(e, AddressC, N, yPos)
                                                yPos += 15
                                                CenterText(e, AddressD, N, yPos)
                                                yPos += 15
                                                CenterText(e, VATTIN, N, yPos)
                                                yPos += 15
                                                CenterText(e, "POS " & pos & " S/N: " & serial, N, yPos)
                                                yPos += 15
                                                CenterText(e, MIN_No, N, yPos)
                                                yPos += 15
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20
                                                CenterText(e, "SALES INVOICE", B, yPos)
                                                yPos += 20
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 15

                                                graphics.DrawString("Cashier Name:", N, brush, leftMargin, yPos)
                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString(row("username").ToString(), N, brush, 155, yPos)
                                                Next
                                                yPos += 20

                                                graphics.DrawString("SI #: ", NB, brush, leftMargin, yPos)
                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString(Terminal_ID & row("or_no").ToString(), NB, brush, maxWidth - 180, yPos)
                                                Next
                                                yPos += 20
                                                graphics.DrawString("Date/Time: ", font, brush, leftMargin, yPos)
                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString(CDate(row("payment_date")).ToString("MM/dd/yyyy"), N, brush, maxWidth - 130, yPos)
                                                    graphics.DrawString(row("payment_time").ToString(), N, brush, maxWidth - 60, yPos)
                                                Next
                                                yPos += 30
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 15
                                                graphics.DrawString("Description", font, brush, leftMargin, yPos)
                                                yPos += 20
                                                graphics.DrawString("Qty", font, brush, maxWidth - 200, yPos) ' Right-align quantity
                                                graphics.DrawString("U.Price", font, brush, maxWidth - 160, yPos) ' Right-align unit price
                                                graphics.DrawString("Amount", font, brush, maxWidth - 90, yPos) ' Right-align amount
                                                yPos += 20

                                                ' Loop through the rows of the sales table and print each row
                                                For Each row As DataRow In or_items_tbl.Rows
                                                    graphics.DrawString(row("ride_type").ToString(), S, brush, New RectangleF(leftMargin, yPos, maxWidth, 200))
                                                    yPos += 35
                                                    graphics.DrawString(row("qty").ToString() & " x ", font, brush, maxWidth - 200, yPos)
                                                    graphics.DrawString("₱ " & Convert.ToDecimal(row("base_price")).ToString(), font, brush, maxWidth - 160, yPos)
                                                    graphics.DrawString("₱ " & Convert.ToDecimal(row("total_amount")).ToString(), NB, brush, maxWidth - 90, yPos)
                                                    yPos += 25

                                                    If Not IsDBNull(row("discount")) AndAlso Convert.ToDecimal(row("discount")) > 0 Then
                                                        graphics.DrawString(row("type_transaction").ToString() & " : ", font, brush, leftMargin + 10, yPos)
                                                        graphics.DrawString(" ₱(" & "-" & Convert.ToDecimal(row("discount")).ToString() & ")", font, brush, leftMargin + 100, yPos)
                                                        yPos += 25
                                                    ElseIf Not IsDBNull(row("type_transaction")) AndAlso (row("type_transaction").ToString().Trim().ToLower() = "Diplomat" Or row("type_transaction").ToString().Trim().ToLower() = "Zero Rated") Then
                                                        graphics.DrawString(row("type_transaction").ToString(), font, brush, leftMargin + 10, yPos)
                                                        yPos += 25
                                                    Else
                                                        yPos += 25
                                                    End If
                                                Next
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 25
                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString("**" & row("qty").ToString() & " Item(s)**", font, brush, maxWidth - 237, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("AMOUNT DUE :", H, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("total_amount")).ToString(), H, brush, 170, yPos)
                                                    yPos += 20
                                                    If row("payment_method").ToString().ToUpper() = "CASH" Then
                                                        graphics.DrawString("PAYMENT:", font, brush, leftMargin, yPos)
                                                        graphics.DrawString(" ₱ " & Convert.ToDecimal(row("cash_tendered")), font, brush, 170, yPos)
                                                        yPos += 20
                                                        graphics.DrawString(row("payment_method").ToString(), font, brush, maxWidth - 237, yPos)   'need to change
                                                        yPos += 20
                                                        graphics.DrawString("CHANGE:", font, brush, leftMargin, yPos)  'need to change
                                                        graphics.DrawString(" ₱ " & Convert.ToDecimal(row("change_amount")), font, brush, 170, yPos)
                                                    ElseIf row("payment_method").ToString().ToUpper() = "GCASH" Or row("payment_method").ToString().ToUpper() = "QR PH" Then
                                                        graphics.DrawString("PAYMENT:", font, brush, leftMargin, yPos)
                                                        graphics.DrawString(" ₱ " & Convert.ToDecimal(row("total_amount")), font, brush, 170, yPos)
                                                        yPos += 20
                                                        graphics.DrawString(row("payment_method").ToString(), font, brush, maxWidth - 237, yPos)
                                                        yPos += 20
                                                        graphics.DrawString("APPROVED CODE: ", NB, brush, leftMargin, yPos)
                                                        graphics.DrawString(row("approved_code").ToString(), NB, brush, 170, yPos)
                                                    Else
                                                        graphics.DrawString("PAYMENT:", font, brush, leftMargin, yPos)
                                                        graphics.DrawString(" ₱ " & Convert.ToDecimal(row("total_amount")), font, brush, 170, yPos)
                                                        yPos += 20
                                                        graphics.DrawString(row("payment_method").ToString(), font, brush, maxWidth - 237, yPos)
                                                        yPos += 20
                                                        For Each card_row As DataRow In card_details_tbl.Rows
                                                            graphics.DrawString("CARD NAME: ", S, brush, leftMargin, yPos)
                                                            graphics.DrawString(card_row("card_name").ToString(), S, brush, 170, yPos)
                                                            yPos += 20
                                                            graphics.DrawString("CARD NO.: ", S, brush, leftMargin, yPos)
                                                            graphics.DrawString(card_row("card_number").ToString(), S, brush, 170, yPos)
                                                            yPos += 30
                                                        Next
                                                        graphics.DrawString("APPROVED CODE : ", NB, brush, leftMargin, yPos)
                                                        graphics.DrawString(row("approved_code").ToString(), NB, brush, 170, yPos)
                                                    End If
                                                Next
                                                yPos += 20
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20
                                                graphics.DrawString("VAT INFORMATION", font, brush, (thermalPaperWidth - graphics.MeasureString("VAT INFORMATION", font).Width) / 2, yPos)
                                                yPos += 15
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 20

                                                For Each row As DataRow In or_tbl.Rows
                                                    graphics.DrawString("VATable:", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("vatable")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("VAT(12%):", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("vat")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("VAT-Exempt:", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("vat_exempt")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("Zero-Rated:", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("zero_rated")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("Less Vat(12%):", font, brush, leftMargin, yPos)
                                                    graphics.DrawString(" ₱ " & Convert.ToDecimal(row("less_vat")).ToString(), font, brush, 170, yPos)
                                                    yPos += 20
                                                Next
                                                'If Customer has a discount 
                                                Dim hasDbDiscount As Boolean = False
                                                For Each row As DataRow In or_tbl.Rows
                                                    If Not IsDBNull(row("discount")) AndAlso Convert.ToDecimal(row("discount")) > 0 Then
                                                        hasDbDiscount = True
                                                    End If
                                                Next

                                                ' Create a simple boolean to make our IF statements easier to read
                                                Dim hasManualDetails As Boolean = (CustomerList IsNot Nothing AndAlso CustomerList.Count > 0)
                                                Dim titleText As String = "CUSTOMER DETAILS"
                                                ' Print the section IF there is a discount OR IF details were added manually
                                                If hasDbDiscount OrElse hasManualDetails Then

                                                    ' Print Header
                                                    CenterText(e, "=================================", H, yPos)
                                                    yPos += 20
                                                    graphics.DrawString(titleText, font, brush, (thermalPaperWidth - graphics.MeasureString(titleText, font).Width) / 2, yPos)
                                                    yPos += 15
                                                    CenterText(e, "=================================", H, yPos)
                                                    yPos += 20

                                                    For Each guest As CustomerInfo In CustomerList
                                                        graphics.DrawString("Name: " & guest.Name, font, brush, leftMargin, yPos)
                                                        yPos += 15

                                                        graphics.DrawString("ID: " & guest.ID, font, brush, leftMargin, yPos)
                                                        yPos += 15

                                                        graphics.DrawString("TIN #: " & guest.TIN, font, brush, leftMargin, yPos)
                                                        yPos += 15

                                                        ' Only print address if it's not empty (saves paper)
                                                        If Not String.IsNullOrEmpty(guest.Address) Then
                                                            graphics.DrawString("Address: " & guest.Address, font, brush, leftMargin, yPos)
                                                            yPos += 15 ' I changed this from 50 to 15 so it doesn't leave a massive gap
                                                        End If

                                                        graphics.DrawString("Signature:_________________", font, brush, leftMargin, yPos)
                                                        yPos += 30 ' Generous space between multiple guests
                                                    Next
                                                Else
                                                    CenterText(e, "=================================", H, yPos)
                                                    yPos += 20
                                                    graphics.DrawString(titleText, font, brush, (thermalPaperWidth - graphics.MeasureString(titleText, font).Width) / 2, yPos)
                                                    yPos += 15
                                                    CenterText(e, "=================================", H, yPos)
                                                    yPos += 20
                                                    graphics.DrawString("Name:_________________________", font, brush, leftMargin, yPos)
                                                    yPos += 20

                                                    graphics.DrawString("ID: __________________________", font, brush, leftMargin, yPos)
                                                    yPos += 20

                                                    graphics.DrawString("TIN #:________________________", font, brush, leftMargin, yPos)
                                                    yPos += 20

                                                    graphics.DrawString("Address:______________________", font, brush, leftMargin, yPos)
                                                    yPos += 20

                                                    graphics.DrawString("Signature:____________________", font, brush, leftMargin, yPos)
                                                    yPos += 30
                                                End If


                                                yPos += 30
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 30
                                                CenterText(e, "THIS SERVES AS YOUR SALES INVOICE", M, yPos)
                                                yPos += 30
                                                CenterText(e, "=================================", H, yPos)
                                                yPos += 30
                                                'CenterText(e, V_Name, N, yPos)
                                                'yPos += 15
                                                'CenterText(e, V_VatReg, N, yPos)
                                                'yPos += 15
                                                'CenterText(e, V_Accreditation_No, N, yPos)
                                                'yPos += 15
                                                CenterText(e, V_Date_Issued, N, yPos)
                                                yPos += 15
                                                CenterText(e, V_PTU_No, N, yPos)
                                                yPos += 50
                                                CenterText(e, "Thank you! Please come again!", N, yPos)
                                                yPos += 50
                                                CenterText(e, "--- END OF TRANSACTION ---", H, yPos)

                                                e.HasMorePages = False
                                            End Sub
        ' Create the PrintPreviewDialog
        Dim printPreview As New PrintPreviewDialog()
        printPreview.Document = printDocument
        printPreview.ShowDialog()  ' Show the print preview dialog

        'Directly send to printer
        printDocument.Print()

        If CustomerList IsNot Nothing AndAlso CustomerList.Count > 0 Then
            Try
                ' Get your connection string (Make sure FDEandD() is accessible here)
                Dim strConn As String = FDEandD()

                Using conn As New MySqlConnection(strConn)
                    conn.Open()
                    Dim query As String = "INSERT INTO customer_tbl (or_no, name, id_no, tin_no, address) VALUES (@or_no, AES_ENCRYPT(@name, 'strdjnltmyp'), @id_no, @tin_no, @address)"

                    For Each guest As CustomerInfo In CustomerList
                        Using cmd As New MySqlCommand(query, conn)
                            ' We are finally using the or_no passed into the printing function!
                            cmd.Parameters.AddWithValue("@or_no", or_no)
                            cmd.Parameters.AddWithValue("@name", guest.Name)
                            cmd.Parameters.AddWithValue("@id_no", guest.ID)

                            ' Handle empty TIN gracefully
                            If String.IsNullOrWhiteSpace(guest.TIN) Then
                                cmd.Parameters.AddWithValue("@tin_no", DBNull.Value)
                            Else
                                cmd.Parameters.AddWithValue("@tin_no", guest.TIN)
                            End If

                            ' Handle empty address gracefully
                            If String.IsNullOrWhiteSpace(guest.Address) Then
                                cmd.Parameters.AddWithValue("@address", DBNull.Value)
                            Else
                                cmd.Parameters.AddWithValue("@address", guest.Address)
                            End If

                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                End Using
            Catch ex As Exception
                ' MsgBox("Failed to upload customer details to database: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
            End Try

            ' FINALLY: Clear the memory list now that it is safely uploaded and printed
            CustomerList.Clear()
            InputCustomerDetails.Close()
        End If

        Try
            ExportDatabase()
        Catch
            MsgBox("Error Exporting Backup")
        End Try

    End Sub
    Private Sub CopyListviewCashierDataToGenerateWristband()

        Dim id As Integer = 0

        For Each item As ListViewItem In ListViewCashier.Items

            ' Get quantity from ListView
            Dim qty As Integer = Convert.ToInt32(item.SubItems(1).Text)
            Dim itemCode As String = item.SubItems(11).Text   ' 
            Dim typeOfTransaction As String = item.SubItems(10).Text

            ' Create ONE wristband PER quantity
            For i As Integer = 1 To qty
                id += 1

                Dim guestId As String = TextBoxBarcode.Text & id

                Dim lvItem As New ListViewItem(guestId)
                lvItem.SubItems.Add(itemCode)
                lvItem.SubItems.Add(typeOfTransaction)

                ListViewWristband.Items.Add(lvItem)
            Next
        Next

        ListViewCashier.Items.Clear()

    End Sub

    Private Sub CopyListviewCustomerView()

        secondscreen.ListViewCustomerView.View = View.Details

        Dim item As New ListViewItem(TextBoxQty.Text)
        item.SubItems.Add(LabelAvailed.Text)
        item.SubItems.Add(TextBoxTripFare.Text)
        item.SubItems.Add(LabelQtyPrice.Text)
        item.SubItems.Add(LabelDiscount.Text)
        item.SubItems.Add(LabelTotal.Text)

        secondscreen.ListViewCustomerView.Items.Add(item)
    End Sub


    Private Sub ItemInsertIntoTicketTracsaction()
        Try
            auto()
            Label1.Text = TextBoxBarcode.Text

            If ListViewCashier.Items.Count = 0 Then
                'MessageBox.Show("No items found in the list.", MessageBoxIcon.Warning)
                Exit Sub
            End If

            Using conn As New MySqlConnection(strConn)
                conn.Open()

                Dim tran As MySqlTransaction = conn.BeginTransaction()

                Try
                    Dim posNo As String = LabelPOSno.Text.Trim()
                    Dim orNo As String = TextBoxBarcode.Text.Trim()

                    ' =========================
                    ' 1) GET EXISTING DB RECORDS
                    ' =========================
                    Dim dbData As New List(Of String)
                    Dim selectSql As String =
                "SELECT qty, ride_type, base_price, total_amount, discount, vatable, vat, vat_exempt, zero_rated, type_transaction, less_vat " &
                "FROM or_items_tbl " &
                "WHERE pos_no=@pos_no AND or_no=@or_no " &
                "ORDER BY id ASC"

                    Using cmdSelect As New MySqlCommand(selectSql, conn, tran)
                        cmdSelect.Parameters.AddWithValue("@pos_no", posNo)
                        cmdSelect.Parameters.AddWithValue("@or_no", orNo)

                        Using reader As MySqlDataReader = cmdSelect.ExecuteReader()
                            While reader.Read()
                                Dim rowString As String =
                            reader("ride_type").ToString() & "|" &
                            reader("qty").ToString() & "|" &
                            Convert.ToDecimal(reader("base_price")).ToString("0.00") & "|" &
                            Convert.ToDecimal(reader("total_amount")).ToString("0.00") & "|" &
                            Convert.ToDecimal(reader("discount")).ToString("0.00") & "|" &
                            Convert.ToDecimal(reader("vatable")).ToString("0.00") & "|" &
                            Convert.ToDecimal(reader("vat")).ToString("0.00") & "|" &
                            Convert.ToDecimal(reader("vat_exempt")).ToString("0.00") & "|" &
                            Convert.ToDecimal(reader("zero_rated")).ToString("0.00") & "|" &
                            reader("type_transaction").ToString() & "|" &
                            Convert.ToDecimal(reader("less_vat")).ToString("0.00")

                                dbData.Add(rowString)
                            End While
                        End Using
                    End Using

                    ' =========================
                    ' 2) GET CURRENT LISTVIEW DATA
                    ' =========================
                    Dim listViewData As New List(Of String)

                    For Each item As ListViewItem In ListViewCashier.Items
                        Dim rideType As String = item.SubItems(0).Text.Trim()
                        Dim qty As Integer = Val(item.SubItems(1).Text)
                        Dim basePrice As Decimal = Val(item.SubItems(2).Text)
                        Dim totalAmount As Decimal = Val(item.SubItems(3).Text)
                        Dim discountAmount As Decimal = Val(item.SubItems(4).Text)
                        Dim vatable As Decimal = Val(item.SubItems(6).Text)
                        Dim vat As Decimal = Val(item.SubItems(7).Text)
                        Dim vatExempt As Decimal = Val(item.SubItems(8).Text)
                        Dim zeroRated As Decimal = Val(item.SubItems(9).Text)
                        Dim typeTransaction As String = item.SubItems(10).Text.Trim()
                        Dim lessVat As Decimal = Val(item.SubItems(12).Text)

                        Dim rowString As String =
                    rideType & "|" &
                    qty.ToString() & "|" &
                    basePrice.ToString("0.00") & "|" &
                    totalAmount.ToString("0.00") & "|" &
                    discountAmount.ToString("0.00") & "|" &
                    vatable.ToString("0.00") & "|" &
                    vat.ToString("0.00") & "|" &
                    vatExempt.ToString("0.00") & "|" &
                    zeroRated.ToString("0.00") & "|" &
                    typeTransaction & "|" &
                    lessVat.ToString("0.00")

                        listViewData.Add(rowString)
                    Next

                    ' =========================
                    ' 3) CHECK IF THERE ARE CHANGES
                    ' =========================
                    If dbData.Count > 0 AndAlso dbData.SequenceEqual(listViewData) Then
                        tran.Rollback()
                        'MessageBox.Show("No changes detected.", MessageBoxIcon.Information)
                        'MessageBox.Show("No changes detected!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    ' =========================
                    ' 4) DELETE OLD ROWS IF EXIST
                    ' =========================
                    Dim delSql As String =
                "DELETE FROM or_items_tbl WHERE pos_no=@pos_no AND or_no=@or_no"

                    Using cmdDel As New MySqlCommand(delSql, conn, tran)
                        cmdDel.Parameters.AddWithValue("@pos_no", posNo)
                        cmdDel.Parameters.AddWithValue("@or_no", orNo)
                        cmdDel.ExecuteNonQuery()
                    End Using

                    ' =========================
                    ' 5) INSERT NEW ROWS
                    ' =========================
                    Dim insSql As String =
                "INSERT INTO or_items_tbl " &
                "(pos_no, or_no, qty, ride_type, base_price, total_amount, discount, vatable, vat, less_vat, vat_exempt, zero_rated, type_transaction, void_status) " &
                "VALUES (@pos_no, @or_no, @qty, @ride_type, @base_price, @total_amount, @discount, @vatable, @vat, @lessvat, @vat_exempt, @zero_rated, @type_transaction, @void_status)"

                    Using cmdIns As New MySqlCommand(insSql, conn, tran)

                        cmdIns.Parameters.Add("@pos_no", MySqlDbType.VarChar)
                        cmdIns.Parameters.Add("@or_no", MySqlDbType.VarChar)
                        cmdIns.Parameters.Add("@qty", MySqlDbType.Int32)
                        cmdIns.Parameters.Add("@ride_type", MySqlDbType.VarChar)
                        cmdIns.Parameters.Add("@base_price", MySqlDbType.Decimal)
                        cmdIns.Parameters.Add("@total_amount", MySqlDbType.Decimal)
                        cmdIns.Parameters.Add("@discount", MySqlDbType.Decimal)
                        cmdIns.Parameters.Add("@vatable", MySqlDbType.Decimal)
                        cmdIns.Parameters.Add("@vat", MySqlDbType.Decimal)
                        cmdIns.Parameters.Add("@lessvat", MySqlDbType.Decimal)
                        cmdIns.Parameters.Add("@vat_exempt", MySqlDbType.Decimal)
                        cmdIns.Parameters.Add("@zero_rated", MySqlDbType.Decimal)
                        cmdIns.Parameters.Add("@type_transaction", MySqlDbType.VarChar)
                        cmdIns.Parameters.Add("@void_status", MySqlDbType.VarChar)

                        For Each item As ListViewItem In ListViewCashier.Items

                            Dim rideType As String = item.SubItems(0).Text.Trim()
                            Dim qty As Integer = Val(item.SubItems(1).Text)
                            Dim basePrice As Decimal = Val(item.SubItems(2).Text)
                            Dim totalAmount As Decimal = Val(item.SubItems(3).Text)
                            Dim discountAmount As Decimal = Val(item.SubItems(4).Text)
                            Dim vatable As Decimal = Val(item.SubItems(6).Text)
                            Dim vat As Decimal = Val(item.SubItems(7).Text)
                            Dim vatExempt As Decimal = Val(item.SubItems(8).Text)
                            Dim zeroRated As Decimal = Val(item.SubItems(9).Text)
                            Dim typeTransaction As String = item.SubItems(10).Text.Trim()
                            Dim lessVat As Decimal = Val(item.SubItems(12).Text)

                            cmdIns.Parameters("@pos_no").Value = posNo
                            cmdIns.Parameters("@or_no").Value = orNo
                            cmdIns.Parameters("@qty").Value = qty
                            cmdIns.Parameters("@ride_type").Value = rideType
                            cmdIns.Parameters("@base_price").Value = basePrice
                            cmdIns.Parameters("@total_amount").Value = totalAmount
                            cmdIns.Parameters("@discount").Value = discountAmount
                            cmdIns.Parameters("@vatable").Value = vatable
                            cmdIns.Parameters("@vat").Value = vat
                            cmdIns.Parameters("@lessvat").Value = lessVat
                            cmdIns.Parameters("@vat_exempt").Value = vatExempt
                            cmdIns.Parameters("@zero_rated").Value = zeroRated
                            cmdIns.Parameters("@type_transaction").Value = typeTransaction
                            cmdIns.Parameters("@void_status").Value = "no"

                            cmdIns.ExecuteNonQuery()
                        Next
                    End Using

                    tran.Commit()
                    'MessageBox.Show("Data successfully stored in database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    Try
                        tran.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using

        Catch ex As Exception
            'MessageBox.Show("Unsuccessfully Store Data in Database! " & ex.Message)
            MessageBox.Show("Unsuccessfully Store Data in Database!")
        End Try
    End Sub

    'Check the last sales invoice number in the database
    Private Function GetLastTranNo() As String

        Using conn As New MySqlConnection(strConn),
        cmd As New MySqlCommand("
            SELECT or_no 
            FROM or_tbl 
            ORDER BY or_no DESC 
            LIMIT 1;", conn)

            conn.Open()
            Dim result = cmd.ExecuteScalar()

            If result Is Nothing OrElse result Is DBNull.Value Then
                Return Nothing
            End If

            Return CStr(result)
        End Using
    End Function

    Private Sub ButtonCustInfo_Click(sender As Object, e As EventArgs) Handles ButtonCustInfo.Click
        'InputDiscountDetails.Show()
        '_discountForm.SetDiscountType("Special Discount")
        '_discountForm.ShowDialog()
        'ComboBoxDiscount.Text = ""
        LabelAvailed.Text = ""
        ComboBoxDiscount.SelectedIndex = -1
        ComboBoxPaymentMethod.SelectedIndex = -1
        LabelTotal.Text = ""
        TextBoxTripFare.Text = ""
        ComboBoxDiscount.Text = ""
        TextBoxDiscountPromo.Text = ""
        ButtonPromo.Text = "Introductory"
        ButtonRegularRide.Text = "Regular"
        TextBoxQty.Text = "0"
    End Sub

    Private Sub ButtonNAAC_Click(sender As Object, e As EventArgs) Handles ButtonNAAC.Click
        Dim or_no As String = TextBoxBarcode.Text
        Try
            TextBoxTripFare.Text = LabelFare.Text

            ' --- Standard Button Enable Logic ---
            Button1.Enabled = True : Button2.Enabled = True : Button3.Enabled = True
            Button4.Enabled = True : Button5.Enabled = True : Button6.Enabled = True
            Button7.Enabled = True : Button8.Enabled = True : Button9.Enabled = True
            Button100.Enabled = True : Button200.Enabled = True : Button500.Enabled = True
            Button1000.Enabled = True : ButtonDot.Enabled = True : Button0.Enabled = True
            Button00.Enabled = True : ButtonDel.Enabled = True : ButtonClear.Enabled = True
            ButtonOk.Enabled = True

            ' 20% Discount for NAAC
            Dim PercentDiscount = 0.2D
            ' sumpayment is Gross (Price * Qty)
            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            If ButtonNAAC.BackColor = Color.Gainsboro Then
                ButtonNAAC.BackColor = Color.Cyan
                LabelType.Text = "NAAC(20%)"

                'EXISTING COMPUTATION
                'Dim discount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                'Dim amountDue As Decimal = Math.Round(sumpayment - discount, 2)
                'Dim vatable As Decimal = Math.Round(amountDue / 1.12D, 2)
                'Dim vat As Decimal = Math.Round(amountDue - vatable, 2)

                'For total Due
                'LabelQtyPrice.Text = sumpayment.ToString("0.00")
                'LabelTotal.Text = amountDue.ToString("0.00")
                'For Discount
                'LabelDiscount.Text = discount.ToString("0.00")

                'BIR COMPLIANCE INCLUSIVE VAT
                Dim Bvatable As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim Bvat As Decimal = Math.Round(sumpayment - Bvatable, 2)
                Dim Bdiscount As Decimal = Math.Round(Bvatable * PercentDiscount, 2)
                Dim withoutVat As Decimal = Math.Round(Bvatable - Bdiscount, 2)
                Dim BamountDue As Decimal = Math.Round(withoutVat + Bvat, 2)

                'BIR COMPLIANCE
                LabelDiscount.Text = Bdiscount.ToString("0.00")
                LabelTotal.Text = BamountDue.ToString("0.00")
                LabelVATable.Text = Bvatable.ToString("0.00")
                LabelVAT.Text = Bvat.ToString("0.00")

                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

            ElseIf ButtonNAAC.BackColor = Color.Cyan Then
                ButtonNAAC.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                ' Re-trigger regular calculation to reset labels
                TextBoxQty_TextChanged(Nothing, Nothing)
                TextBoxPercentDiscount.Text = ""
            End If

            ' --- Standard Toggle Logic ---
            If ButtonNAAC.BackColor = Color.Cyan Then
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonSoloParent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonDiplomat.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
            End If

            ' Clear payment fields for the new calculation
            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Ride!", MessageBoxIcon.Information)
            ButtonNAAC.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
        _detailsForm.SetTransactionDetails("NAAC(20%)", or_no)
        ' Open popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True
        End If
    End Sub

    Private Sub ButtonDiplomat_Click(sender As Object, e As EventArgs) Handles ButtonDiplomat.Click
        Dim or_no As String = TextBoxBarcode.Text
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

            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            If ButtonDiplomat.BackColor = Color.Gainsboro Then
                ButtonDiplomat.BackColor = Color.Cyan
                LabelType.Text = "Diplomat"

                ' The original math: only strips the 12% VAT
                Dim diplomat As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim lessvat As Decimal = Math.Round(sumpayment - diplomat, 2)
                Dim amountDue As Decimal = Math.Round(sumpayment - lessvat, 2)

                'LabelTotal.Text = "0.00"
                LabelTotal.Text = amountDue.ToString("0.00")
                'LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text

                LabelVATExempt.Text = diplomat.ToString("0.00")
                LabelLessVat.Text = lessvat.ToString("0.00")
                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                'LabelZeroRated.Text = zero_rated.ToString("0.00")

            ElseIf ButtonDiplomat.BackColor = Color.Cyan Then
                ButtonDiplomat.BackColor = Color.Gainsboro
                LabelType.Text = "Regular"

                Dim vatable As Decimal = Math.Round(sumpayment / 1.12D, 2)
                Dim vat As Decimal = Math.Round(sumpayment - vatable, 2)

                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")

                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"
                LabelDiscount.Text = "0.00"

                LabelTotal.Text = sumpayment.ToString("0.00")
                secondscreen.PaymentDue.Text = LabelTotal.Text
                TextBoxPercentDiscount.Text = ""
            End If

            If ButtonDiplomat.BackColor = Color.Cyan Then
                ButtonStudent.BackColor = Color.Gainsboro
                ButtonSoloParent.BackColor = Color.Gainsboro
                ButtonPWD.BackColor = Color.Gainsboro
                ButtonNAAC.BackColor = Color.Gainsboro
                ButtonSenior.BackColor = Color.Gainsboro
                ButtonZeroRated.BackColor = Color.Gainsboro
                ButtonEmployee.BackColor = Color.Gainsboro
                LabelType.Text = "Diplomat"
            End If

            TextBoxMoney.Clear()
            TextBoxChange.Clear()
            secondscreen.Change.Text = Nothing

        Catch ex As Exception
            MsgBox("Please input Ride!", MessageBoxIcon.Information)
            ButtonDiplomat.BackColor = Color.Gainsboro
            LabelType.Text = "Regular"
        End Try
        _detailsForm.SetTransactionDetails("Diplomat", or_no)
        ' Open popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True
        End If
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click

        Dim printerName As String = "ZDesigner ZD230-203dpi ZPL" ' change to your installed printer name

        Try
            Button12.BackColor = Color.Gainsboro
            Button12.Enabled = False

            Dim printDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim logoPath As String = IO.Path.Combine(Application.StartupPath, "Images", "CompLogo.png") 'Logo of Company

            Dim secretKey As String = "3HqjgEa8eZrKkdiyDbnjIdfMlaayo9dFvd2rFTgbjgc="   ' your secretkey
            Dim zplHeader As String = "^XA" & vbCrLf

            Dim logoZpl As String = ""
            If IO.File.Exists(logoPath) Then
                Using logoBmp As New Bitmap(logoPath)
                    Dim resizedLogo As Bitmap = ResizeBitmap(logoBmp, 120, 120) ' adjust size
                    logoZpl = BitmapToZPL_At(resizedLogo, 40, 1200)               ' position
                    resizedLogo.Dispose()
                End Using
            End If

            For Each item As ListViewItem In ListViewWristband.Items

                Dim guestId As String = item.SubItems(0).Text
                Dim rideCode As String = If(item.SubItems.Count > 1, item.SubItems(1).Text, "")
                Dim typeOfTransaction As String = If(item.SubItems.Count > 2, item.SubItems(2).Text, "")
                Dim timeStamp As String = DateTime.Now.ToString("yyyyMMddHHmmss")
                Dim timeStampID As String = DateTime.Now.ToString("HHmmss")

                'The original code running to mobile scanner
                'Dim payload As String = $"G={guestId}|R={rideDescription}|T={timeStamp}"
                'Dim encrypted As String = EncryptWithSecret(payload, secretKey)

                Dim payload As String = POS_ID & guestId
                'MsgBox(payload)

                Dim encrypted As String = EncryptWithSecret(payload, secretKey)

                InsertQRRecord(payload, rideCode, typeOfTransaction, timeStamp, encrypted)

                Dim qrGen As New QRCodeGenerator()

                'Original Code with encryption
                'Dim qrData = qrGen.CreateQrCode(encrypted, QRCodeGenerator.ECCLevel.Q)
                Dim qrData = qrGen.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q)

                Dim qrCode As New QRCode(qrData)
                Dim qrImage As Bitmap = qrCode.GetGraphic(5)

                'For Testing Purposes: Visualization of QR Code
                ' SAVE QR IMAGE
                Dim savePath As String = IO.Path.Combine(Application.StartupPath, "QR", payload & ".png")
                If Not IO.Directory.Exists(IO.Path.GetDirectoryName(savePath)) Then
                    IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(savePath))
                End If
                qrImage.Save(savePath, Imaging.ImageFormat.Png)

                ' QR bitmap -> ZPL at specific position
                Dim zplImage As String = BitmapToZPL_At(qrImage, 50, 15) ' position QR lower
                ' Text positions

                Dim guestTextZpl As String =
                         "^FO230,50^A0N,20,20^FDID : " & EscapeZpl(guestId) & "^FS" & vbCrLf &
                        "^FO230,90^A0N,20,20^FDD/T : " & EscapeZpl(printDate) & "^FS" & vbCrLf &
                        "^FO230,120^A0N,20,20^FDCODE : " & EscapeZpl(rideCode) & "^FS" & vbCrLf &
                        "^FO230,150^A0N,20,20^FDTYPE : " & EscapeZpl(typeOfTransaction) & "^FS"

                Dim finalZPL As String =
                  "^XA" & vbCrLf &
                  "^PW600" & vbCrLf &
                   "^LL400" & vbCrLf &
                    zplImage & vbCrLf &
                    guestTextZpl & vbCrLf &
                   "^XZ"

                'zplImage & vbCrLf &

                Dim ok = ZebraRawPrint.SendZplToPrinter(printerName, finalZPL)
                If Not ok Then
                    MessageBox.Show("Print failed. Check printer name / driver.")
                End If

                'For Registration for Guest in Hikvision API
                Dim visitorName As String = guestId
                Dim certificateNo As String = payload
                Dim customId As String = guestId
                'Dim customId As String = DateTime.Now.ToString("yyyyMMddHHmmss")
                Dim customFieldName As String = guestId

                Try
                    Dim codeReservation As String = HikOpenApiClient.AddReservation(visitorName, certificateNo, customId, customFieldName)
                    'MessageBox.Show(codeReservation)

                Catch ex As Exception
                    'MessageBox.Show(ex.ToString())
                    MsgBox("Error Connection in Hik Server!")
                End Try
            Next

            MsgBox("Done Printing Sticker Ticket.......")

            LabelTotal.Text = "0.00"
            TextBoxMoney.Text = "0.00"
            TextBoxChange.Text = "0.00"
            ComboBoxPaymentMethod.Text = ""
            ButtonComputeTotal.BackColor = Color.Gainsboro


            ListViewWristband.Items.Clear()

            Button12.Enabled = False
            Button16.Enabled = False

        Catch ex As Exception
            MsgBox("Error During Printing: " & ex.Message)
        End Try
    End Sub

    'For QR Code Encyption
    Private Function EncryptWithSecret(plainText As String, secretKey As String) As String
        ' Derive 32-byte key from your secret string
        Dim keyBytes As Byte()
        Using sha As SHA256 = SHA256.Create()
            keyBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(secretKey))
        End Using

        ' 16-byte IV (here just zeros; you can change to something fixed/shared)
        Dim ivBytes(15) As Byte

        Using aes As Aes = Aes.Create()
            aes.Key = keyBytes
            aes.IV = ivBytes
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7

            Dim encryptor = aes.CreateEncryptor(aes.Key, aes.IV)
            Dim plainBytes = Encoding.UTF8.GetBytes(plainText)
            Dim cipherBytes As Byte()

            Using ms As New IO.MemoryStream()
                Using cs As New CryptoStream(ms, encryptor, CryptoStreamMode.Write)
                    cs.Write(plainBytes, 0, plainBytes.Length)
                    cs.FlushFinalBlock()
                    cipherBytes = ms.ToArray()
                End Using
            End Using

            ' Base64 so it is safe to put inside QR
            Return Convert.ToBase64String(cipherBytes)
        End Using
    End Function

    'Initial for Setup for QR Code Table
    'Private Sub InsertQRRecord(guestId As String, rideId As String, descText As String, timeStamp As String, encryptedQR As String)
    Private Sub InsertQRRecord(guestId As String, rideDescription As String, typeOfTransaction As String, timeStamp As String, encryptedQR As String)

        conn.Open()

        Dim query As String =
            "INSERT INTO wristband_qr_tbl (guestID, rideDescription, typeOfTransaction, timeStamp, encryptedQR) " &
            "VALUES (@GuestID, @RideDescription, @TypeOfTransaction, @TimeStamp, @EncryptedQR)"

        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@GuestID", guestId)
            cmd.Parameters.AddWithValue("@RideDescription", rideDescription)
            cmd.Parameters.AddWithValue("@TypeOfTransaction", typeOfTransaction)
            cmd.Parameters.AddWithValue("@TimeStamp", timeStamp)
            cmd.Parameters.AddWithValue("@EncryptedQR", encryptedQR)

            cmd.ExecuteNonQuery()
        End Using

        conn.Close()

    End Sub

    ' Convertion of File of for Zebra Printing "ZD510"
    Public Function BitmapToZPL(bmp As Bitmap) As String
        ' Resize image for wristband clarity
        Using resized As New Bitmap(bmp, New Size(200, 200))

            Dim width As Integer = resized.Width
            Dim height As Integer = resized.Height

            Dim bytesPerRow As Integer = CInt(Math.Ceiling(width / 8.0))
            Dim totalBytes As Integer = bytesPerRow * height

            Dim data(totalBytes - 1) As Byte
            Dim byteIndex As Integer = 0

            For y As Integer = 0 To height - 1
                Dim bitCount As Integer = 0
                Dim currentByte As Byte = 0

                For x As Integer = 0 To width - 1
                    Dim pixel As Color = resized.GetPixel(x, y)

                    ' anything not white = black
                    Dim isBlack As Boolean = (pixel.R < 128 OrElse pixel.G < 128 OrElse pixel.B < 128)

                    currentByte = CByte(currentByte << 1)
                    If isBlack Then currentByte = CByte(currentByte Or 1)

                    bitCount += 1

                    If bitCount = 8 Then
                        data(byteIndex) = currentByte
                        byteIndex += 1
                        bitCount = 0
                        currentByte = 0
                    End If
                Next

                If bitCount > 0 Then
                    currentByte = CByte(currentByte << (8 - bitCount))
                    data(byteIndex) = currentByte
                    byteIndex += 1
                End If
            Next

            Dim hex As String = BitConverter.ToString(data).Replace("-", "")

            ' ✅ RETURN ONLY ^GFA (NO ^FO, NO ^FS)
            Return $"^GFA,{totalBytes},{totalBytes},{bytesPerRow},{hex}"
        End Using
    End Function

    Private Function BitmapToZPL_At(bmp As Bitmap, x As Integer, y As Integer) As String
        Dim gfa As String = BitmapToZPL(bmp)   ' now it's ONLY ^GFA...
        Return $"^FO{x},{y}" & vbCrLf & gfa & vbCrLf & "^FS"
    End Function
    Private Function EscapeZpl(s As String) As String
        If s Is Nothing Then Return ""
        ' ZPL uses ^ and \ as control in some contexts, keep it simple:
        Return s.Replace("^", " ").Replace("~", " ")
    End Function

    Private Function ResizeBitmap(src As Bitmap, w As Integer, h As Integer) As Bitmap
        Dim dst As New Bitmap(w, h)
        Using g = Graphics.FromImage(dst)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(src, 0, 0, w, h)
        End Using
        Return dst
    End Function

    Public Sub SendZPLToPrinter(zpl As String)
        Dim printerName As String = "ZDesigner ZD510-300dpi ZPL"

        Dim rawData As Byte() = System.Text.Encoding.ASCII.GetBytes(zpl)
        RawPrinterHelper.SendBytesToPrinter(printerName, rawData)
    End Sub

    Public Class RawPrinterHelper
        <StructLayout(LayoutKind.Sequential)>
        Public Structure DOCINFOA
            <MarshalAs(UnmanagedType.LPStr)> Public pDocName As String
            <MarshalAs(UnmanagedType.LPStr)> Public pOutputFile As String
            <MarshalAs(UnmanagedType.LPStr)> Public pDatatype As String
        End Structure

        <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", SetLastError:=True)>
        Public Shared Function OpenPrinter(szPrinter As String, ByRef hPrinter As IntPtr, pd As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", CharSet:=CharSet.Ansi, SetLastError:=True)>
        Public Shared Function StartDocPrinter(hPrinter As IntPtr, level As Integer, ByRef di As DOCINFOA) As Boolean
        End Function

        <DllImport("winspool.Drv", SetLastError:=True)>
        Public Shared Function StartPagePrinter(hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", SetLastError:=True)>
        Public Shared Function WritePrinter(hPrinter As IntPtr, pBytes As Byte(), dwCount As Integer, ByRef dwWritten As Integer) As Boolean
        End Function

        <DllImport("winspool.Drv", SetLastError:=True)>
        Public Shared Function EndPagePrinter(hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", SetLastError:=True)>
        Public Shared Function EndDocPrinter(hPrinter As IntPtr) As Boolean
        End Function

        <DllImport("winspool.Drv", SetLastError:=True)>
        Public Shared Function ClosePrinter(hPrinter As IntPtr) As Boolean
        End Function

        Public Shared Function SendBytesToPrinter(szPrinterName As String, pBytes As Byte()) As Boolean
            Dim di As New DOCINFOA()
            di.pDocName = "ZPL Print"
            di.pDatatype = "RAW"

            Dim hPrinter As IntPtr = IntPtr.Zero
            Dim dwWritten As Integer = 0

            If OpenPrinter(szPrinterName, hPrinter, IntPtr.Zero) Then
                If StartDocPrinter(hPrinter, 1, di) Then
                    If StartPagePrinter(hPrinter) Then
                        WritePrinter(hPrinter, pBytes, pBytes.Length, dwWritten)
                        EndPagePrinter(hPrinter)
                    End If
                    EndDocPrinter(hPrinter)
                End If
                ClosePrinter(hPrinter)
            End If

            Return dwWritten > 0
        End Function
    End Class

    ' To show the second form to the extended monitor
    Public Sub ShowFormOnScreen(targetForm As Form, screenIndex As Integer)
        ' Safety check: if requested screen doesn't exist, just show on primary
        If screenIndex < 0 OrElse screenIndex >= Screen.AllScreens.Length Then
            screenIndex = 0 ' primary screen
        End If

        Dim scr As Screen = Screen.AllScreens(screenIndex)

        ' Important: manual position
        targetForm.StartPosition = FormStartPosition.Manual

        ' Position the form at the top-left of the target screen
        targetForm.Location = scr.Bounds.Location

        ' Optional: make it full screen on that monitor
        targetForm.WindowState = FormWindowState.Maximized

        targetForm.Show()
    End Sub

    Private Sub ButtonCard_Click(sender As Object, e As EventArgs) Handles ButtonCard.Click

    End Sub
    Private Sub ButtonVoidItem_Click(sender As Object, e As EventArgs) Handles ButtonVoidItem.Click

        ButtonComputeTotal.BackColor = Color.Gainsboro

        autovoidItem()

        If ListViewCashier.SelectedItems.Count > 0 Then

            Dim sel As ListViewItem = ListViewCashier.SelectedItems(0)

            ' Example: columns: [0]=id, [1]=remarks, [2]=amount
            Dim p_ride_type As String = sel.SubItems(0).Text
            Dim p_qty As Integer = Integer.Parse(sel.SubItems(1).Text)
            Dim p_base_price As Decimal = Decimal.Parse(sel.SubItems(2).Text)
            Dim p_total_amount As Decimal = Decimal.Parse(sel.SubItems(3).Text)
            Dim p_discount As Decimal = Decimal.Parse(sel.SubItems(4).Text)
            Dim p_vatable As Decimal = Decimal.Parse(sel.SubItems(5).Text)
            Dim p_vat As Decimal = Decimal.Parse(sel.SubItems(6).Text)
            Dim p_vat_exempt As Decimal = Decimal.Parse(sel.SubItems(7).Text)
            Dim p_vat_zero_rated As Decimal = Decimal.Parse(sel.SubItems(8).Text)
            Dim p_type As String = sel.SubItems(9).Text

            Try
                Try
                    Dim BranchCode As String = "BR01"
                    Dim DateNow As String = DateTime.Now.ToString("yyyyMMdd")
                    Dim voidID As String = lblvoiditem.Text

                    Dim refIdCO As String = $"{BranchCode}-{DateNow}-VI-{voidID}"
                    Dim remarks As String = $"ride:{p_ride_type}-qty:{p_qty}-price:{p_base_price}-amount:{p_total_amount}-disc:{p_discount}"

                    Using c As New MySqlConnection(strConn)
                        c.Open()
                        Using cmd As New MySqlCommand("insertingAuditTrail", c)
                            cmd.CommandType = CommandType.StoredProcedure

                            cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                            cmd.Parameters.AddWithValue("p_userid", LabelCashierID.Text)
                            cmd.Parameters.AddWithValue("p_username", LabelCashierName.Text)
                            cmd.Parameters.AddWithValue("p_approvedby", "")

                            cmd.Parameters.AddWithValue("p_activity_performed", "Void Item")
                            cmd.Parameters.AddWithValue("p_module", "Sales")
                            cmd.Parameters.AddWithValue("p_reference_id", refIdCO)
                            cmd.Parameters.AddWithValue("p_remarks", remarks)

                            cmd.ExecuteNonQuery()
                        End Using

                        For Each item As ListViewItem In ListViewCashier.SelectedItems
                            storedVoidItem(p_ride_type, p_qty, p_base_price, p_total_amount, p_discount, p_vatable, p_vat, p_vat_exempt, p_vat_zero_rated, p_type)

                            Dim rideType As String = item.SubItems(0).Text
                            Dim rideQty As String = item.SubItems(1).Text
                            ListViewCashier.Items.Remove(item)

                            ' find and remove in ListView2
                            For Each lv2Item As ListViewItem In secondscreen.ListViewCustomerView.Items
                                If lv2Item.SubItems(1).Text = rideType And lv2Item.SubItems(0).Text = rideQty Then
                                    secondscreen.ListViewCustomerView.Items.Remove(lv2Item)
                                    Exit For
                                End If
                            Next
                            MessageBox.Show("Successfully Deleted Item!")
                            LabelTotal.Text = ""
                        Next
                    End Using

                Catch ex As Exception
                    MessageBox.Show("Save failed Audit Trail: " & ex.Message)
                End Try

            Catch ex As Exception
                MessageBox.Show("Save failed: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Item is DELETED, Please Recompute the Total by clicking TOTAL BUTTON!")
        End If
    End Sub

    Private Function storedVoidItem(ride_type As String, qty As Integer, base_price As Decimal, total_amount As Decimal, discount As Decimal, vatable As Decimal, vat As Decimal, vat_exempt As Decimal, vat_zero_rated As Decimal, type As String) As String

        Try
            ' Need to Continue....... Void Item table
            Using con As New MySqlConnection(strConn)
                Using cmd As New MySqlCommand("void_item", con)
                    cmd.CommandType = CommandType.StoredProcedure

                    ' IMPORTANT: parameter names must match your SP
                    cmd.Parameters.AddWithValue("@p_void_no", lblvoiditem.Text)
                    cmd.Parameters.AddWithValue("@p_or_no", TextBoxBarcode.Text)
                    cmd.Parameters.AddWithValue("@p_pos_id", LabelPOSno.Text)
                    cmd.Parameters.AddWithValue("@p_ride_type", ride_type)
                    cmd.Parameters.AddWithValue("@p_qty", qty)
                    cmd.Parameters.AddWithValue("@p_user_id", LabelCashierID.Text)
                    cmd.Parameters.AddWithValue("@p_base_price", base_price)
                    cmd.Parameters.AddWithValue("@p_vatable", vatable)
                    cmd.Parameters.AddWithValue("@p_vat_exempt", vat_exempt)
                    cmd.Parameters.AddWithValue("@p_zero_rated", vat_zero_rated)
                    cmd.Parameters.AddWithValue("@p_vat", vat)
                    cmd.Parameters.AddWithValue("@p_discount", discount)
                    cmd.Parameters.AddWithValue("@p_total_amount", total_amount)
                    cmd.Parameters.AddWithValue("@p_void_type", type)
                    cmd.Parameters.AddWithValue("@p_void_by", "")
                    cmd.Parameters.AddWithValue("@p_upload", "no")

                    Dim pMsg As New MySqlParameter("@p_message", MySqlDbType.VarChar, 255)
                    pMsg.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(pMsg)

                    con.Open()
                    cmd.ExecuteNonQuery()

                    Return If(pMsg.Value IsNot Nothing, pMsg.Value.ToString(), "Done.")

                End Using
            End Using

        Catch ex As Exception
            MsgBox("Unsuccessfully Void Item!")
        End Try

    End Function

    Private Sub autovoidItem()
        'for generating Void No.
        Dim seq As Single
        conn.Open()
        Dim qd As String = "Select count(*) AS void FROM void_item_tbl WHERE pos_id = '" & LabelPOSno.Text & "'"
        Dim cmd As New MySqlCommand(qd) With {.Connection = conn}
        Dim rdr As MySqlDataReader = cmd.ExecuteReader()
        While rdr.Read
            seq = Val(rdr.Item("void").ToString) + 1
        End While
        Select Case Len(Trim(seq))
            Case 1 : lblvoiditem.Text = "000000000" + Trim(Str(seq))
            Case 2 : lblvoiditem.Text = "00000000" + Trim(Str(seq))
            Case 3 : lblvoiditem.Text = "0000000" + Trim(Str(seq))
            Case 4 : lblvoiditem.Text = "000000" + Trim(Str(seq))
            Case 5 : lblvoiditem.Text = "00000" + Trim(Str(seq))
            Case 6 : lblvoiditem.Text = "0000" + Trim(Str(seq))
            Case 7 : lblvoiditem.Text = "000" + Trim(Str(seq))
            Case 8 : lblvoiditem.Text = "00" + Trim(Str(seq))
            Case 9 : lblvoiditem.Text = "0" + Trim(Str(seq))
        End Select
        conn.Close()
    End Sub

    Private Sub ComboBoxPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPaymentMethod.SelectedIndexChanged
        Try
            If LabelTotal.Text = "" Then
                MsgBox("Please Total the Amount!")
            Else
                If ComboBoxPaymentMethod.SelectedIndex = 1 Or ComboBoxPaymentMethod.SelectedIndex = 2 Then
                    TableLayoutPanel13.Enabled = False
                    TextBoxMoney.Text = ""
                    TextBoxChange.Text = ""
                    ApprovedCode.lblPayment_Type.Text = ComboBoxPaymentMethod.Text
                    ApprovedCode.Show()
                    Button12.ForeColor = Color.Black

                    'ElseIf ComboBoxPaymentMethod.SelectedIndex = 3 Or ComboBoxPaymentMethod.SelectedIndex = 4 Or ComboBoxPaymentMethod.SelectedIndex = 5 Or ComboBoxPaymentMethod.SelectedIndex = 6 Or ComboBoxPaymentMethod.SelectedIndex >= 7 Then
                ElseIf ComboBoxPaymentMethod.SelectedIndex >= 3 Then

                    TableLayoutPanel13.Enabled = False
                    TextBoxMoney.Text = ""
                    TextBoxChange.Text = ""
                    CardDetails.lblPayment.Text = ComboBoxPaymentMethod.Text
                    ApprovedCode.lblPayment_Type.Text = CardDetails.lblPayment.Text
                    CardDetails.Show()
                    Button12.ForeColor = Color.Black

                ElseIf ComboBoxPaymentMethod.SelectedIndex = 0 Then
                    TableLayoutPanel13.Enabled = True
                    Button12.ForeColor = Color.Black
                    EnableKeypadNumber()
                End If
            End If

        Catch

        End Try
    End Sub

    Private Sub TextBoxPercentDiscount_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPercentDiscount.TextChanged

        Dim discount As Integer = TextBoxPercentDiscount.Text
        Dim sumpayment As String = Convert.ToDecimal(Val(TextBoxTripFare.Text) * Val(TextBoxQty.Text)).ToString("0.00")

        Dim amount As Integer = sumpayment * discount
        LabelTotal.Text = amount.ToString

        LabelDiscount.Text = (Math.Round(Val((LabelVATable.Text * Val(TextBoxPercentDiscount.Text))), 2))
        'LabelDiscount.Text = (Math.Round(Val((LabelVATable.Text * Val(TextBoxPercentDiscount.Text))), 2))

        LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
        LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(LabelVATable.Text)), 2))
        LabelVATExempt.Text = "0.00"
        LabelZeroRated.Text = "0.00"

        ' For Total Due
        'LabelTotal.Text = (Math.Round(Val(LabelVAT.Text) + (Val((LabelVATable.Text - LabelDiscount.Text))), 2))

    End Sub

    Private Sub disableFunctionForAuthorizedPersonnelOnly()
        ButtonStudent.Enabled = False
        ButtonSenior.Enabled = False
        ButtonSoloParent.Enabled = False
        ButtonPWD.Enabled = False
        ButtonNAAC.Enabled = False
        ButtonDiplomat.Enabled = False
        ButtonZeroRated.Enabled = False
        ButtonEmployee.Enabled = False
        ButtonVoidItem.Enabled = False
        'ButtonCustInfo.Enabled = False
        ComboBoxPaymentMethod.Enabled = False
        ComboBoxDiscount.Enabled = False
        CheckBoxPercentDiscount.Enabled = False
        TextBoxDiscountPromo.Enabled = False
        ComboBoxDiscount.SelectedIndex = -1
        ComboBoxPaymentMethod.SelectedIndex = -1
    End Sub

    Private Sub hideListFunction()
        ButtonRegularRide.Enabled = True
        ButtonPromo.Enabled = False
        TextBoxUserID.Hide()
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
        TextBoxPercentDiscount.Hide()

        ' The the module is dependent these function
        LabelQtyTotal.Hide()
        LabelQtyPrice.Hide()
        Label_BasePrice.Hide()
        ComboReaderNames.Hide()
        ButtonCard.Hide()
        Button11.Hide()
        LabelStation.Hide()
        LabelStationID.Hide()
        Button12.Enabled = False
        Button16.Enabled = False
        lblvoiditem.Hide()
        ComboBoxPaymentMethod.Enabled = False
        TableLayoutPanel13.Enabled = False
        LabelDiscount1.Hide()
        lblApprovedCode.Hide()
        label_code.Hide()
        lblCname.Hide()
        lblCno.Hide()
        'Label1.Hide()
        LabelLessVat.Hide()

        'for GiftShop function
        'TableLayoutPanel4.Enabled = True
        'Button13.Enabled = True

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        ReprintWristbandSticker.Show()
    End Sub

    'CASH DRAWER FUNCTION
    Private Sub OpenTerminalDrawer()
        Dim drawerPortName As String = "COM1"

        Using sp As New SerialPort("COM1", 9600, Parity.None, 8, StopBits.One)
            Try
                sp.Open()

                ' \007 is ASCII 7 (The "Bell" character)
                ' We send it as a Byte array to ensure it is transmitted purely
                Dim triggerCode As Byte() = {7}

                ' Write the byte to the port
                sp.Write(triggerCode, 0, 1)

                ' Close immediately
                sp.Close()

            Catch ex As UnauthorizedAccessException
                ' CRITICAL: This error means the Posiflex Demo (or another app) is still open.
                MessageBox.Show("Port COM1 is busy!")
            Catch ex As Exception
                'MessageBox.Show("Drawer Error: " & ex.Message)
                MessageBox.Show("Error Opening Cash Drawer, Kindly manually use Key!")
            End Try
        End Using
    End Sub

    'LOCK FOLDER
    Public Sub LockFolder(folderPath As String)
        Try
            Dim dirInfo As New DirectoryInfo(folderPath)
            Dim dirSecurity As DirectorySecurity = dirInfo.GetAccessControl()

            ' Stop inheriting permissions
            dirSecurity.SetAccessRuleProtection(True, False)

            ' Deny everyone
            Dim everyone As New SecurityIdentifier(WellKnownSidType.WorldSid, Nothing)
            dirSecurity.AddAccessRule(New FileSystemAccessRule(
                everyone,
                FileSystemRights.FullControl,
                AccessControlType.Deny))

            ' Allow administrators
            Dim admin As New SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, Nothing)
            dirSecurity.AddAccessRule(New FileSystemAccessRule(
                admin,
                FileSystemRights.FullControl,
                AccessControlType.Allow))

            dirInfo.SetAccessControl(dirSecurity)
        Catch

        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDiscount.SelectedIndexChanged
        Try
            If ComboBoxDiscount.Text = "buy 5 Ticket get 1free" Then

                LabelQtyPrice.Text = "0.00"
                LabelTotal.Text = "0.00"
                LabelDiscount.Text = "0.00"
                LabelType.Text = "S.Disc: Free Ticket"

                LabelVATable.Text = "0.00"
                LabelVAT.Text = "0.00"
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

            ElseIf ComboBoxDiscount.Text = "0.10" Then

                Dim PercentDiscount = ComboBoxDiscount.Text
                Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

                Dim discount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                Dim amountDue As Decimal = Math.Round(sumpayment - discount, 2)

                Dim vatable As Decimal = Math.Round(amountDue / 1.12D, 2)
                Dim vat As Decimal = Math.Round(amountDue - vatable, 2)

                'For total Due
                LabelQtyPrice.Text = sumpayment.ToString("0.00")
                LabelTotal.Text = amountDue.ToString("0.00")

                'For Discount
                LabelDiscount.Text = discount.ToString("0.00")
                LabelType.Text = "S.Disc: 10%"

                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

            ElseIf ComboBoxDiscount.Text = "0.25" Then

                Dim PercentDiscount = ComboBoxDiscount.Text
                Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

                Dim discount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                Dim amountDue As Decimal = Math.Round(sumpayment - discount, 2)

                Dim vatable As Decimal = Math.Round(amountDue / 1.12D, 2)
                Dim vat As Decimal = Math.Round(amountDue - vatable, 2)

                'For total Due
                LabelQtyPrice.Text = sumpayment.ToString("0.00")
                LabelTotal.Text = amountDue.ToString("0.00")

                'For Discount
                LabelDiscount.Text = discount.ToString("0.00")
                LabelType.Text = "S.Disc: 25%"

                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"

            ElseIf ComboBoxDiscount.Text = "0.30" Then

                Dim PercentDiscount = ComboBoxDiscount.Text
                Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

                Dim discount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
                Dim amountDue As Decimal = Math.Round(sumpayment - discount, 2)

                Dim vatable As Decimal = Math.Round(amountDue / 1.12D, 2)
                Dim vat As Decimal = Math.Round(amountDue - vatable, 2)

                'For total Due
                LabelQtyPrice.Text = sumpayment.ToString("0.00")
                LabelTotal.Text = amountDue.ToString("0.00")


                'For Discount
                LabelDiscount.Text = discount.ToString("0.00")
                LabelType.Text = "S.Disc: 30%"

                LabelVATable.Text = vatable.ToString("0.00")
                LabelVAT.Text = vat.ToString("0.00")
                LabelVATExempt.Text = "0.00"
                LabelZeroRated.Text = "0.00"
            End If
        Catch
            'MsgBox("Select Discount Amount!")
        End Try
    End Sub

    Private Sub TextBoxDiscountPromo_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDiscountPromo.TextChanged
        Try

            ' 20% Discount for NAAC
            Dim PercentDiscount = TextBoxDiscountPromo.Text
            ' sumpayment is Gross (Price * Qty)
            Dim sumpayment As Decimal = CDec(TextBoxTripFare.Text) * CDec(TextBoxQty.Text)

            'Dim discount As Decimal = Math.Round(sumpayment * PercentDiscount, 2)
            Dim discount As Decimal = Math.Round(sumpayment - PercentDiscount, 2)
            Dim amountDue As Decimal = Math.Round(sumpayment - discount, 2)

            Dim vatable As Decimal = Math.Round(discount / 1.12D, 2)
            Dim vat As Decimal = Math.Round(discount - vatable, 2)

            'For total Due
            LabelQtyPrice.Text = sumpayment.ToString("0.00")
            LabelTotal.Text = discount.ToString("0.00")
            'LabelTotal.Text = amountDue.ToString("0.00")

            'For Discount
            'LabelDiscount.Text = discount.ToString("0.00")
            LabelDiscount.Text = PercentDiscount.ToString
            LabelType.Text = "Special Discount"

            LabelVATable.Text = vatable.ToString("0.00")
            LabelVAT.Text = vat.ToString("0.00")
            LabelVATExempt.Text = "0.00"
            LabelZeroRated.Text = "0.00"
        Catch
            'MsgBox("Input Discount Amount!")
        End Try
    End Sub
    Public Sub ExportDatabase()

        Try
            Dim dbName As String = "amusement_db"
            Dim dbUser As String = "root"
            Dim dbPassword As String = "" ' if blank, remove -p totally (see below)

            Dim mysqldumpPath As String = "C:\xampp\mysql\bin\mysqldump.exe"
            Dim backupFolder As String = "C:\DB_Backup\"

            If Not File.Exists(mysqldumpPath) Then
                MessageBox.Show("mysqldump not found: " & mysqldumpPath)
                Return
            End If

            If Not Directory.Exists(backupFolder) Then
                Directory.CreateDirectory(backupFolder)
            End If

            'Dim fileName As String = dbName & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".sql"
            Dim fileName As String = "POS" & POS_ID & "_" & dbName & "_" & DateTime.Now.ToString("yyyyMMdd") & ".sql"
            Dim fullPath As String = Path.Combine(backupFolder, fileName)

            Dim args As String

            ' ✅ IMPORTANT:
            ' If password is blank, do NOT include -p (it can still cause issues)
            If String.IsNullOrEmpty(dbPassword) Then
                args = $"--default-character-set=utf8mb4 -u {dbUser} {dbName}"
            Else
                args = $"--default-character-set=utf8mb4 -u {dbUser} -p{dbPassword} {dbName}"
            End If

            Dim psi As New ProcessStartInfo()
            psi.FileName = mysqldumpPath
            psi.Arguments = args
            psi.UseShellExecute = False
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True
            psi.CreateNoWindow = True
            psi.StandardOutputEncoding = Encoding.UTF8
            psi.StandardErrorEncoding = Encoding.UTF8

            Dim stdout As String = ""
            Dim stderr As String = ""

            Using p As Process = Process.Start(psi)
                stdout = p.StandardOutput.ReadToEnd()
                stderr = p.StandardError.ReadToEnd()
                p.WaitForExit()

                If p.ExitCode <> 0 Then
                    'MessageBox.Show("Backup FAILED!" & vbCrLf & stderr)
                    Return
                End If
            End Using

            ' ✅ Write the dump content to file
            File.WriteAllText(fullPath, stdout, Encoding.UTF8)

            ' ✅ Verify file size
            Dim fi As New FileInfo(fullPath)
            If fi.Length = 0 Then
                'MessageBox.Show("Backup created but file is 0KB. Error: " & vbCrLf & stderr)
                Return
            End If

            'MessageBox.Show("Database exported successfully!" & vbCrLf & fullPath)
            'MessageBox.Show("Created database backup successfully!")

        Catch ex As Exception
            MessageBox.Show("Error: Creating database backup")
            'MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        ' 1. Check if Quantity is missing
        If String.IsNullOrWhiteSpace(TextBoxQty.Text) Then
            MessageBox.Show("Please enter a Quantity and select a Ride Type first!", "Missing Info")
            Exit Sub
        End If

        ' 2. Pass the data to the popup
        Dim or_no As String = TextBoxBarcode.Text
        _detailsForm.SetTransactionDetails("Regular", or_no, TextBoxQty.Text)

        ' 3. Show popup. If they click confirm, unlock the Add Item button!
        If _detailsForm.ShowDialog() = DialogResult.OK Then
            ButtonAddItem.Enabled = True ' (Change ButtonAddItem to your actual button name if it's different)
        End If
    End Sub


    Public Sub EnableKeypadNumber()
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True
        Button10.Enabled = True
        Button2.Enabled = True
        ButtonDot.Enabled = True
        Button0.Enabled = True
        Button00.Enabled = True
        Button100.Enabled = True
        Button200.Enabled = True
        Button500.Enabled = True
        Button1000.Enabled = True
    End Sub

    Public Sub disableKeypadNumber()
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False
        Button10.Enabled = False
        Button2.Enabled = False
        ButtonDot.Enabled = False
        Button0.Enabled = False
        Button00.Enabled = False
        Button100.Enabled = False
        Button200.Enabled = False
        Button500.Enabled = False
        Button1000.Enabled = False
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub ListViewCashier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewCashier.SelectedIndexChanged

    End Sub
End Class
