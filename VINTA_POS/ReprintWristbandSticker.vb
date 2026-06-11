Imports WindowsApplication1.ConfigClass
Imports MySql.Data.MySqlClient
Imports QRCoder

Public Class ReprintWristbandSticker

    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Dim index As Integer

    Private Sub ReprintWristbandSticker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        datagrid_design()
        loaddata()
        fit_grid() ' <-- call after data is loaded (important)
    End Sub

    Private Sub loaddata()
        Try
            Dim ds As New DataSet()
            Using cmd As New MySqlCommand("SELECT guestID, rideDescription, typeOfTransaction, last_updated FROM wristband_qr_tbl", conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(ds, "wristband_qr_tbl")
                End Using
            End Using

            DataGridView1.DataSource = ds.Tables(0)
            DataGridView1.Refresh()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub datagrid_design()
        DataGridView1.BorderStyle = BorderStyle.None
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        DataGridView1.BackgroundColor = Color.White

        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewAdvancedCellBorderStyle.None
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'display info on select
        Try
            Index = e.RowIndex
            Dim selectedRow As DataGridViewRow

            selectedRow = DataGridView1.Rows(Index)
            labelGuestID.Text = selectedRow.Cells(0).Value.ToString()
            LabelCode.Text = selectedRow.Cells(1).Value.ToString()
            LabelType.Text = selectedRow.Cells(2).Value
            LabelDT.Text = selectedRow.Cells(3).Value.ToString()

            loaddata()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub fit_grid()
        With DataGridView1
            ' make it resize / fit the space
            '.Dock = DockStyle.Top   ' or DockStyle.Fill if you want it fill whole form
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True

            ' FIT columns to the grid width
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            ' optional: nicer look
            .AllowUserToResizeRows = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        End With

        ' OPTIONAL: control proportions (so rideDescription is wider)
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("guestID").FillWeight = 25
            DataGridView1.Columns("rideDescription").FillWeight = 20
            DataGridView1.Columns("typeOfTransaction").FillWeight = 20
            DataGridView1.Columns("last_updated").FillWeight = 25

            DataGridView1.Columns("last_updated").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
        End If
    End Sub

    Public Sub FilterData(valueToSearch As String)
        ' Dim searchQuery As String = "SELECT guestID, rideDescription, typeOfTransaction, last_updated FROM wristband_qr_tbl WHERE CONCAT(guestID) like '%" & valueToSearch & "%' AND pos_id = '" & mainform.LabelPOSno.Text & "'"
        Dim searchQuery As String = "SELECT guestID, rideDescription, typeOfTransaction, last_updated FROM wristband_qr_tbl WHERE CONCAT(guestID) like '%" & valueToSearch & "%'"
        Dim command As New MySqlCommand(searchQuery, conn)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable
        adapter.Fill(table)
        DataGridView1.DataSource = table
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'search guestID
        Try
            FilterData(TextBox1.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        FilterData("")
    End Sub

    Private Sub ButtonReprint_Click(sender As Object, e As EventArgs) Handles ButtonReprint.Click
        Dim printerName As String = "ZDesigner ZD230-203dpi ZPL" ' change to your installed printer name

        Try
            Dim guestId As String = labelGuestID.Text
            Dim rideCode As String = LabelCode.Text
            Dim typeOfTransaction As String = LabelType.Text
            Dim timeStamp As String = LabelDT.Text

            Dim qrGen As New QRCodeGenerator()
            Dim qrData = qrGen.CreateQrCode(guestId, QRCodeGenerator.ECCLevel.Q)

            Dim qrCode As New QRCode(qrData)
            Dim qrImage As Bitmap = qrCode.GetGraphic(5)

            'For Wristband Printing
            'Dim zplImage As String = BitmapToZPL(qrImage)

            ' QR bitmap -> ZPL at specific position
            Dim zplImage As String = BitmapToZPL_At(qrImage, 50, 15) ' position QR lower
            ' Text positions

            '"^FO150,50^BQN,2,5^FDLA," & EscapeZpl(zplImage) & "^FS" & vbCrLf &

            Dim guestTextZpl As String =
                     "^FO230,50^A0N,20,20^FDID : " & EscapeZpl(guestId) & "^FS" & vbCrLf &
                    "^FO230,90^A0N,20,20^FDD/T : " & EscapeZpl(timeStamp) & "^FS" & vbCrLf &
                    "^FO230,120^A0N,20,20^FDCODE : " & EscapeZpl(rideCode) & "^FS" & vbCrLf &
                    "^FO230,150^A0N,20,20^FDTYPE : " & EscapeZpl(typeOfTransaction) & "^FS"

            Dim finalZPL As String =
              "^XA" & vbCrLf &
              "^PW600" & vbCrLf &
               "^LL400" & vbCrLf &
                zplImage & vbCrLf &
                guestTextZpl & vbCrLf &
               "^XZ"

            MsgBox("Reprinting Sticker.......")

            Dim ok = ZebraRawPrint.SendZplToPrinter(printerName, finalZPL)
            If Not ok Then
                MessageBox.Show("Print failed. Check printer name / driver.")
            End If

        Catch ex As Exception
            MsgBox("Error During Printing: " & ex.Message)
        End Try
    End Sub

    Private Function BitmapToZPL_At(bmp As Bitmap, x As Integer, y As Integer) As String
        Dim gfa As String = BitmapToZPL(bmp)   ' now it's ONLY ^GFA...
        Return $"^FO{x},{y}" & vbCrLf & gfa & vbCrLf & "^FS"
    End Function
    Private Function EscapeZpl(s As String) As String
        If s Is Nothing Then Return ""
        ' ZPL uses ^ and \ as control in some contexts, keep it simple:
        Return s.Replace("^", " ").Replace("~", " ")
    End Function
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
End Class