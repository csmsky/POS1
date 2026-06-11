Imports System.IO
Imports WindowsApplication1.ConfigClass
Public Class testseat
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps

    Dim MyButton = New Button
    Private Sub testseat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'for taking the company logo from resources
        'PictureBoxCompanyLogo.Image = WindowsApplication1.My.Resources.Logo
        PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA
        LabelPOSno.Text = pos

        LabelCashierID.Hide()

        Dim cmd As New MySqlCommand
        Dim dt As New DataTable()
        Dim adapter As New MySqlDataAdapter

        'for displaying passenger's info
        Try
            cmd.CommandText = "SELECT seat_layout from vessel_tbl where vessel_id = '" & Label1.Text & "'"
            conn.Open()
            adapter.SelectCommand = cmd
            adapter.SelectCommand.Connection = conn
            adapter.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim img() As Byte
                img = dt.Rows(0)(0)
                Dim ms As New MemoryStream(img)
                PictureBox1.Image = Image.FromStream(ms)

            Else
                PictureBox1.Image = Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim PointButton As Point = New Point
        Try
            Dim Query = "SELECT seat_no, seat_id FROM seat_tbl WHERE vessel_id = '" & Label1.Text & "'"
            'AND seat_status = 0"
            Dim da As New MySqlDataAdapter(Query, conn)
            Dim ds As New DataSet
            ds.Clear()
            da.Fill(ds, "seat_no")

            If ds.Tables(0).Rows.Count < 1 Then
                Label1.Text = "NO AVAILABLE SEATS"
            End If

            ReDim MyButton(ds.Tables(0).Rows.Count)

            Dim one As Integer = 1
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Dim MyButton As New Button
                MyButton.Text = ds.Tables(0).Rows.Item(i).Item(0).ToString
                MyButton.Name = "seat_no" & one.ToString
                MyButton.Font = New Font("Microsoft Sans Serif", 14)
                MyButton.Width = 130
                MyButton.Height = 60
                MyButton.BackColor = Color.WhiteSmoke
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

    Private Sub ClickMe(sender As Object, e As EventArgs)
        Dim MyButton As Button
        MyButton = CType(sender, Button)

        Dim dialog As DialogResult

        dialog = MessageBox.Show("Are you sure you want to choose seat, " & MyButton.Text & " ?", "WARNING!", MessageBoxButtons.YesNo)

        If dialog = DialogResult.Yes Then
            ' mainform.TextBoxSeat.Text = MyButton.Text
            secondscreen.Seat.Text = MyButton.Text
            Me.Close()
        End If
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'for displaying current day, datetime
        LabelDateTime.Text = DateTime.Now.ToString("ddd MM/dd/yyyy HH:mm:ss")
    End Sub
End Class