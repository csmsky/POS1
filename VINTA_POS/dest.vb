Imports System.IO.Ports
Imports WindowsApplication1.ConfigClass
Public Class dest
    'Dim strConn As String = "server=172.20.10.8;port=9999;userid=root;password=superferry@securepasswordreplica;database=superferry_replica"
    'Dim strConn As String = "server=192.168.1.27;port=9999;userid=root;password=superferry@securepasswordreplica;database=superferry_replica"
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    Dim displayHome As Byte() = New Byte(0) {&H1}
    Dim displayClear As Byte() = New Byte(0) {&HC}
    Dim displayNewLine As Byte() = New Byte(0) {&HD}
    Dim displayLineFd As Byte() = New Byte(0) {&HA}

    Dim scheduleID As String
    Dim vesselID As String
    Dim seatNum As String
    Dim origStation As String
    Dim destStation As String
    Dim vesselSched As String
    Dim split As String
    Dim split2 As String
    Dim combo
    Dim sched_id As String

    Dim MyButton = New Button
    Private Sub dest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                MyButton.Font = New Font("Microsoft Sans Serif", 14)
                MyButton.Width = 130
                MyButton.Height = 60
                'MyButton.BackColor = Color.Transparent
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
        Dim MyButton As Button
        MyButton = CType(sender, Button)
        destination.ButtonDestination.Text = MyButton.Text

        'If MyButton.Text = "Plaza Mexico" Then
        '    destination.DestinationID.Text = "PLAMEX"
        '    destination.DestinationNo.Text = 1
        'ElseIf MyButton.Text = "Mall Of Asia" Then
        '    destination.DestinationID.Text = "MOA"
        '    destination.DestinationNo.Text = 2
        'ElseIf MyButton.Text = "Noveleta" Then
        '    destination.DestinationID.Text = "NVLT"
        '    destination.DestinationNo.Text = 3
        'End If
        Dim cmd As New MySqlCommand
        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter
        cmd.CommandText = "SELECT station_id FROM station_tbl where station_name = '" & MyButton.Text & "'"
        conn.Open()
        adapter.SelectCommand = cmd
        adapter.SelectCommand.Connection = conn
        adapter.Fill(dt)

        If dt.Rows.Count > 0 Then
            destination.DestinationID.Text = dt.Rows(0)(0).ToString()
        End If

        Dim ccmd As New MySqlCommand("select sched_id, concat(vessel_id,'-', departure_time) AS trip from vesselsched_tbl where departure_station = @departure_station AND arrival_station = @arrival_station order by departure_time ASC", conn)
        ccmd.CommandType = CommandType.Text
        ccmd.Parameters.AddWithValue("@departure_station", destination.OriginID.Text)
        ccmd.Parameters.AddWithValue("@arrival_station", destination.DestinationID.Text)
        Dim sda As New MySqlDataAdapter(ccmd)
        Dim ddt As New DataTable

        sda.Fill(ddt)

        destination.ComboBox1.DataSource = ddt

        destination.ComboBox1.DisplayMember = "trip"
        destination.ComboBox1.ValueMember = "sched_id"

        origStation = destination.ButtonOrigin.Text
        destStation = destination.ButtonDestination.Text

        Try
            vesselID = destination.ComboBox1.DisplayMember.ToString()
            scheduleID = destination.ComboBox1.SelectedValue.ToString()
            combo = destination.ComboBox1.Text
        Catch
            MsgBox("Invalid", vbOKOnly, "Error!")
        End Try

        Dim cmdd As New MySqlCommand("select fare from fare_tbl where station_origin = @station_origin and station_dest = @station_dest", conn)
        cmdd.Parameters.AddWithValue("@station_origin", destination.OriginID.Text)
        cmdd.Parameters.AddWithValue("@station_dest", destination.DestinationID.Text)

        Dim dtt As New DataTable()
        Dim adapterr As New MySqlDataAdapter(cmdd)

        adapterr.Fill(dtt)
        Try

            destination.TextBoxPayment.Text = dtt.Rows(0)(0).ToString()
            destination.LabelFare.Text = dtt.Rows(0)(0).ToString()

        Catch ex As Exception
            MsgBox("Invalid", vbOKOnly, "Error!")
        End Try

        destination.LabelVATable.Text = Val(destination.TextBoxPayment.Text) / "1.12"
        destination.LabelVAT.Text = Val(destination.TextBoxPayment.Text) - Val(destination.LabelVATable.Text)

        Try
            With destination.SerialPort1
                .PortName = "COM2"
                .BaudRate = 9600
                .DataBits = 8
                .Parity = Parity.None
                .StopBits = StopBits.One
                .Handshake = Handshake.None
            End With

            If Not (destination.SerialPort1.IsOpen = True) Then
                destination.SerialPort1.Open()
            End If

            destination.SerialPort1.DiscardOutBuffer()

            destination.SerialPort1.Write(displayClear, 0, 1)

            destination.SerialPort1.Write(displayHome, 0, 1)
            '.Write(Text.PadRight(15))
            'destination.SerialPort1.Write("Amount Due:")
            'destination.SerialPort1.Write((destination.TextBoxPayment.Text.PadLeft(11) + ".00"))
            destination.SerialPort1.Write("Amount Due:" & vbCrLf & destination.TextBoxPayment.Text + ".00")
            destination.SerialPort1.Close()
            'MsgBox("Data sent")

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        Me.Close()
    End Sub
End Class