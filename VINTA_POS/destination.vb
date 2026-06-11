Imports System.IO.Ports
Imports WindowsApplication1.ConfigClass
Public Class destination
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    'for vfd
    Dim displayHome As Byte() = New Byte(0) {&H1}
    Dim displayClear As Byte() = New Byte(0) {&HC}

    Dim scheduleID As String
    Dim vesselID As String
    Dim origStation As String
    Dim destStation As String
    Dim combo

    Dim MyButton = New Button
    Private Sub dest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'for making dynamic buttons of destination station
        Dim PointButton As Point = New Point
        Try
            Dim Query = "SELECT station_name FROM station_tbl"
            Dim da As New MySqlDataAdapter(Query, conn)
            Dim ds As New DataSet
            ds.Clear()
            da.Fill(ds, "station_name")

            ReDim MyButton(ds.Tables(0).Rows.Count)

            Dim one As Integer = 1
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Dim MyButton As New Button
                MyButton.Text = ds.Tables(0).Rows.Item(i).Item(0).ToString
                MyButton.Name = "station_name" & one.ToString
                MyButton.Font = New Font("Tahoma", 12)
                MyButton.Width = 130
                MyButton.Height = 80
                PointButton.Y = 3
                If one = 1 Then
                    PointButton.X = PointButton.X
                Else
                    PointButton.X = PointButton.X + MyButton.Width + 10
                End If
                MyButton.Location = PointButton

                DirectCast(FlowLayoutPanel1, Panel).Controls.Add(MyButton)
                one += 1
                AddHandler MyButton.Click, AddressOf ClickMe
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Uknown Databases")
        End Try
    End Sub

    Private Sub ClickMe(ByVal sender As Object, e As EventArgs)
        If mainform.ButtonRegularRide.Text = "Origin" Then
            MsgBox("Please choose ORIGIN first.", MessageBoxIcon.Warning)
        Else
            'for assigning button's function
            Dim MyButton As Button
            MyButton = CType(sender, Button)
            ' mainform.ButtonDestination.Text = MyButton.Text

            Dim cmd As New MySqlCommand
            Dim dt As New DataTable()
            Dim adapter As New MySqlDataAdapter
            cmd.CommandText = "SELECT station_id FROM station_tbl where station_name = '" & MyButton.Text & "'"
            conn.Open()
            adapter.SelectCommand = cmd
            adapter.SelectCommand.Connection = conn
            adapter.Fill(dt)

            If dt.Rows.Count > 0 Then
                mainform.DestinationID.Text = dt.Rows(0)(0).ToString()
            End If

            'select queries
            Dim ccmd As New MySqlCommand("select sched_id, concat(vessel_id,'-', departure_time) AS trip from vesselsched_tbl where departure_station = @departure_station AND arrival_station = @arrival_station order by departure_time ASC", conn)
            ccmd.CommandType = CommandType.Text
            ccmd.Parameters.AddWithValue("@departure_station", mainform.OriginID.Text)
            ccmd.Parameters.AddWithValue("@arrival_station", mainform.DestinationID.Text)
            Dim sda As New MySqlDataAdapter(ccmd)
            Dim ddt As New DataTable

            sda.Fill(ddt)

            'mainform.ComboBoxTime.DataSource = ddt

            'mainform.ComboBoxTime.DisplayMember = "trip"
            'mainform.ComboBoxTime.ValueMember = "sched_id"

            origStation = mainform.ButtonRegularRide.Text
            'destStation = mainform.ButtonDestination.Text

            Try
                'vesselID = mainform.ComboBoxTime.DisplayMember.ToString()
                'scheduleID = mainform.ComboBoxTime.SelectedValue.ToString()
                'combo = mainform.ComboBoxTime.Text

                'fare
                Dim cmdd As New MySqlCommand("select fare from fare_tbl where station_origin = @station_origin and station_dest = @station_dest", conn)
                cmdd.Parameters.AddWithValue("@station_origin", mainform.OriginID.Text)
                cmdd.Parameters.AddWithValue("@station_dest", mainform.DestinationID.Text)

                Dim dtt As New DataTable()
                Dim adapterr As New MySqlDataAdapter(cmdd)

                adapterr.Fill(dtt)
                Try

                    mainform.TextBoxTripFare.Text = dtt.Rows(0)(0).ToString()
                    mainform.LabelFare.Text = dtt.Rows(0)(0).ToString()

                    Dim sumpayment As String = Convert.ToDecimal(Val(mainform.TextBoxTripFare.Text)).ToString("0.00")
                    mainform.LabelTotal.Text = sumpayment
                    secondscreen.PaymentDue.Text = sumpayment


                    'for computing VAT
                    Try
                        mainform.LabelVATable.Text = (Math.Round(Val((sumpayment / "1.12")), 2))
                        mainform.LabelVAT.Text = (Math.Round(Val((sumpayment) - Val(mainform.LabelVATable.Text)), 2))
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

                Catch ex As Exception
                    MsgBox("Invalid", vbOKOnly, "Error!")
                End Try

            Catch
                MsgBox("Invalid, same destination and origin!", MessageBoxIcon.Error)
                'mainform.ButtonDestination.Text = "Destination"
            End Try

            'for displaying amount in vfd
            Try
                With mainform.SerialPort1
                    .PortName = "COM2"
                    .BaudRate = 9600
                    .DataBits = 8
                    .Parity = Parity.None
                    .StopBits = StopBits.One
                    .Handshake = Handshake.None
                End With

                If Not (mainform.SerialPort1.IsOpen = True) Then
                    mainform.SerialPort1.Open()
                End If

                mainform.SerialPort1.DiscardOutBuffer()

                mainform.SerialPort1.Write(displayClear, 0, 1)

                mainform.SerialPort1.Write(displayHome, 0, 1)
                mainform.SerialPort1.Write("Amount Due:" & vbCrLf & Convert.ToDecimal(mainform.TextBoxTripFare.Text).ToString("0.00"))
                mainform.SerialPort1.Close()

            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try

            Me.Close()

        End If
    End Sub
End Class