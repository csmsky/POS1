Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass
Imports System.IO
Public Class secondscreen

    Dim strConn As String = FDEandD()
    Private r As New Random()

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Dim i As Integer = +1
        'PictureBox1.Image = My.Resources.ResourceManager.GetObject("Image" & r.Next(i, 8))
    End Sub

    ' 1. DATABASE CONFIGURATION
    'Private ReadOnly connString As String = "server=localhost;user=root;password='';database=amusement_db"

    ' 2. SEAMLESS LOOP TIMER
    Private WithEvents tmrSeamlessLoop As New Timer()

    ' --- LOAD EVENT ---
    Private Sub secondscreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' A. MOVE TO SECOND MONITOR LOGIC
        Dim screens() As Screen = Screen.AllScreens

        If screens.Length > 1 Then
            Me.StartPosition = FormStartPosition.Manual
            Me.Location = screens(1).Bounds.Location
            Me.FormBorderStyle = FormBorderStyle.None
            Me.WindowState = FormWindowState.Normal
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Maximized
        End If

        ' B. SETUP SEAMLESS LOOP TIMER
        tmrSeamlessLoop.Interval = 20
        tmrSeamlessLoop.Start()

        ' C. LOAD INITIAL CONTENT
        RefreshMedia("header")
        RefreshMedia("side")
    End Sub

    ' --- RESIZE EVENT: MAINTAIN LAYOUT ---
    Private Sub secondscreen_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If AxVideoHeader IsNot Nothing AndAlso pbSide IsNot Nothing Then
            Dim headerHeight As Integer = CInt(Me.Height * 0.3)

            ' 1. Position Video Header
            AxVideoHeader.Top = 0
            AxVideoHeader.Left = 0
            AxVideoHeader.Width = Me.Width
            AxVideoHeader.Height = headerHeight

            ' 2. Position Side Image
            pbSide.Top = headerHeight
            pbSide.Left = 0
            pbSide.Width = Me.Width
            pbSide.Height = Me.Height - headerHeight
        End If
    End Sub

    ' --- TIMER TICK: SEAMLESS LOOP & AUTO-RESUME ---
    Private Sub tmrSeamlessLoop_Tick(sender As Object, e As EventArgs) Handles tmrSeamlessLoop.Tick
        Try
            If AxVideoHeader IsNot Nothing Then

                ' FIX: If the player is "Ready" or "Stopped" (common after a URL change), force Play
                If AxVideoHeader.playState = WMPLib.WMPPlayState.wmppsStopped OrElse
                   AxVideoHeader.playState = WMPLib.WMPPlayState.wmppsReady Then
                    AxVideoHeader.Ctlcontrols.play()
                End If

                ' SEAMLESS LOOP LOGIC
                If AxVideoHeader.playState = WMPLib.WMPPlayState.wmppsPlaying Then
                    ' Jump to start 100ms before the file actually ends to prevent the "black gap"
                    If AxVideoHeader.Ctlcontrols.currentPosition >= (AxVideoHeader.currentMedia.duration - 0.1) Then
                        AxVideoHeader.Ctlcontrols.currentPosition = 0
                    End If
                End If

            End If
        Catch ex As Exception
            ' Silent catch during transitions
        End Try
    End Sub

    ' --- DATABASE RETRIEVAL LOGIC ---
    Public Sub RefreshMedia(pos As String, Optional targetFilename As String = "")
        Try
            Using conn As New MySqlConnection(strConn)
                conn.Open()
                Dim query As String = ""

                If String.IsNullOrEmpty(targetFilename) Then
                    query = "SELECT base_directory, filename FROM assets_customer_view_tbl " &
                            "WHERE position = @pos ORDER BY created_at DESC LIMIT 1"
                Else
                    query = "SELECT base_directory, filename FROM assets_customer_view_tbl " &
                            "WHERE position = @pos AND filename = @targetName LIMIT 1"
                End If

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@pos", pos)
                    If Not String.IsNullOrEmpty(targetFilename) Then
                        cmd.Parameters.AddWithValue("@targetName", targetFilename)
                    End If

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim folder As String = reader("base_directory").ToString()
                            Dim fileName As String = reader("filename").ToString()
                            Dim fullPath As String = Path.Combine(folder, fileName)

                            If System.IO.File.Exists(fullPath) Then
                                UpdateDisplay(fullPath, pos)
                            Else
                                Console.WriteLine("File missing: " & fullPath)
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error refreshing " & pos & ": " & ex.Message)
        End Try
    End Sub

    ' --- DISPLAY UPDATE LOGIC ---
    Private Sub UpdateDisplay(filePath As String, pos As String)
        If pos.ToLower() = "header" Then
            If AxVideoHeader IsNot Nothing Then
                ' FIX: Stop existing playback before switching URLs to clear the buffer
                AxVideoHeader.Ctlcontrols.stop()

                AxVideoHeader.Visible = True
                AxVideoHeader.uiMode = "none"
                AxVideoHeader.stretchToFit = True

                ' Assign new media
                AxVideoHeader.URL = filePath

                ' Native loop OFF (Our timer handles it better)
                AxVideoHeader.settings.setMode("loop", False)
                AxVideoHeader.settings.mute = True

                ' Start playback
                AxVideoHeader.Ctlcontrols.play()
            End If

        ElseIf pos.ToLower() = "side" Then
            If pbSide IsNot Nothing Then
                pbSide.Visible = True
                ' FileStream prevents "file in use" errors if the image is being replaced
                Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                    pbSide.Image = Image.FromStream(fs)
                End Using
                pbSide.SizeMode = PictureBoxSizeMode.Zoom
            End If
        End If
    End Sub
End Class