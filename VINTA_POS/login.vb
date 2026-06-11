Imports System.Data
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass
Imports System.Security.AccessControl

' ===== Fingerprint =====
Imports DPUruNet
Imports DPUruNet.Constants

Public Class login
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps

    ' ===========================
    ' CARD READER (DISABLED FOR NOW)
    ' ===========================
    Private _pcscReader As PscsReader
    Private _cardPolling As CardPolling

    ' ===========================
    ' FINGERPRINT (AUTO READ)
    ' ===========================
    Private _fpReader As Reader = Nothing
    Private _fpResolution As Integer = 500
    Private _fpBusy As Boolean = False
    Private _fpLastMatchAt As DateTime = DateTime.MinValue

    Private Const FP_THRESHOLD As Integer = 30000          ' lower=stricter
    Private Const FP_MIN_INTERVAL_MS As Integer = 1500     ' prevent repeated triggers
    Private Const FP_POLL_INTERVAL_MS As Integer = 500     ' scan loop interval

    Private WithEvents fpTimer As New Timer()

    Dim apdu As Apdu = New Apdu
    Dim command() As Byte

    Public Sub OnThreadException(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        MessageBox.Show(e.Exception.Message)
    End Sub

    ' ===========================
    ' CARD EVENTS (kept, but not used)
    ' ===========================
    Private Sub cardPolling_OnCardFound(ByVal sender As Object, ByVal e As CardPollingEventArg)
        ' DISABLED - fingerprint will be used instead
        ' (kept here as reference)
    End Sub

    Private Sub cardPolling_OnError(ByVal sender As Object, ByVal e As CardPollingErrorEventArg)
        MessageBox.Show("Invalid")
    End Sub

    Private Sub cardPolling_OnCardRemoved(ByVal sender As Object, ByVal e As CardPollingEventArg)
        'clearing fields when card is removed
        TextBoxUserID.Clear()
        LabelName.Text = ""
        LabelPosition.Text = ""
        PictureBoxFace.Image = Nothing
        ButtonLogin.Enabled = False
    End Sub
    ' ============================================
    ' AUTO LOGIN HELPER
    ' ============================================
    Private Sub AutoLogin()
        ' Make sure user data is already loaded
        If String.IsNullOrWhiteSpace(TextBoxUserID.Text) Then Exit Sub
        If String.IsNullOrWhiteSpace(LabelPosition.Text) Then Exit Sub
        If Not ButtonLogin.Enabled Then Exit Sub

        ' Small delay para sure tapos na UI updates
        Application.DoEvents()
        Threading.Thread.Sleep(200)

        ' Trigger existing login logic
        ButtonLogin.PerformClick()
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'for offline/online indicator
            conn.Open()

            If conn.State = ConnectionState.Open Then

                LabelHello.Hide()
                LabelPOSno.Text = pos

                LabelSoftwareName.Text = Application.ProductName
                LabelVersionNo.Text = Application.ProductVersion

                ComboReaderNames.Hide() ' not used now
                Timer1.Enabled = True
                ButtonLogin.Enabled = False

                ComboReaderNames.Enabled = False

                LabelIndicator.Text = "ONLINE"
                LabelIndicator.ForeColor = Color.Green

                ' ===========================
                ' CARD READER INIT (DISABLED)
                ' ===========================
                'Dim readerList() As String
                'Try
                '    _pcscReader = New PscsReader
                '    _cardPolling = New CardPolling
                '    AddHandler _cardPolling.OnCardFound, AddressOf cardPolling_OnCardFound
                '    AddHandler _cardPolling.OnCardRemoved, AddressOf cardPolling_OnCardRemoved
                '    AddHandler _cardPolling.OnError, AddressOf cardPolling_OnError
                '    _cardPolling.StopPolling()
                '    readerList = _pcscReader.getReaderList
                '    ComboReaderNames.Items.Clear()
                '    If readerList.Length > 0 Then
                '        ComboReaderNames.Items.AddRange(readerList)
                '        ComboReaderNames.SelectedIndex = 0
                '        For i As Integer = 0 To ComboReaderNames.Items.Count - 1
                '            _cardPolling.add(ComboReaderNames.Items(i).ToString)
                '        Next
                '    Else
                '        MessageBox.Show("No readers found.", "List Readers", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    End If
                'Catch pcscException As PscsException
                '    MessageBox.Show("PCSC Exception: " + pcscException.Message)
                'Catch generalException As Exception
                '    MessageBox.Show(generalException.Message)
                'End Try
                'If _cardPolling Is Nothing Then Return
                'If _cardPolling.isBusy Then
                '    _cardPolling.StopPolling()
                'Else
                '    _cardPolling.start()
                'End If

                ' ===========================
                ' Fingerprint UI (HIDE)
                ' ===========================
                ' ✅ you asked to keep old UI; so we just hide fingerprint controls if they exist
                If pbFingerprint IsNot Nothing Then
                    pbFingerprint.Visible = False
                    pbFingerprint.Enabled = False
                End If
                If lblFpStatus IsNot Nothing Then
                    lblFpStatus.Visible = False
                End If
                ' ✅ combobox should not be there
                If cmbFinger IsNot Nothing Then
                    cmbFinger.Visible = False
                    cmbFinger.Enabled = False
                End If
                If btnScanFinger IsNot Nothing Then
                    btnScanFinger.Visible = False
                    btnScanFinger.Enabled = False
                End If

                ' ===========================
                ' Fingerprint AUTO start
                ' ===========================
                InitFingerprintReader()
                StartAutoFingerprint()

            Else
                LabelHello.Hide()
                LabelPOSno.Text = pos
                LabelSoftwareName.Text = Application.ProductName
                LabelVersionNo.Text = Application.ProductVersion
                ComboReaderNames.Hide()
                Timer1.Enabled = True
                ButtonLogin.Enabled = True

                LabelIndicator.Text = "OFFLINE"
                LabelIndicator.ForeColor = Color.Red
            End If

        Catch
            LabelPOSno.Text = pos
            LabelSoftwareName.Text = Application.ProductName
            LabelVersionNo.Text = Application.ProductVersion
            ComboReaderNames.Hide()
            Timer1.Enabled = True
            ButtonLogin.Enabled = False
            LabelIndicator.Text = "OFFLINE"
        Finally
            Try
                If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
            Catch
            End Try
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'for displaying current date and time
        LabelDateTime.Text = DateTime.Now.ToString("ddd MM/dd/yyyy HH:mm:ss")
    End Sub

    ' ============================================================
    ' FINGERPRINT INIT + AUTO LOOP (NO BUTTON, ALWAYS READING)
    ' ============================================================
    Private Sub InitFingerprintReader()
        Try
            Dim rc = ReaderCollection.GetReaders()
            If rc Is Nothing OrElse rc.Count = 0 Then
                ' No fingerprint reader detected.
                Exit Sub
            End If

            _fpReader = rc(0)
            _fpReader.Open(CapturePriority.DP_PRIORITY_COOPERATIVE)

            _fpResolution = 500
            If _fpReader.Capabilities IsNot Nothing AndAlso
               _fpReader.Capabilities.Resolutions IsNot Nothing AndAlso
               _fpReader.Capabilities.Resolutions.Length > 0 Then
                _fpResolution = _fpReader.Capabilities.Resolutions(0)
            End If

        Catch
            _fpReader = Nothing
        End Try
    End Sub

    Private Sub StartAutoFingerprint()
        fpTimer.Interval = FP_POLL_INTERVAL_MS
        fpTimer.Enabled = True
        fpTimer.Start()
    End Sub

    Private Sub StopAutoFingerprint()
        fpTimer.Stop()
        fpTimer.Enabled = False
    End Sub

    Private Sub fpTimer_Tick(sender As Object, e As EventArgs) Handles fpTimer.Tick
        If _fpBusy Then Return
        If _fpReader Is Nothing Then Return

        If (DateTime.Now - _fpLastMatchAt).TotalMilliseconds < FP_MIN_INTERVAL_MS Then Return

        _fpBusy = True
        Try
            AutoScanOnce()
        Catch
            ' ignore scan errors/timeouts
        Finally
            _fpBusy = False
        End Try
    End Sub

    Private Sub AutoScanOnce()
        If DateTime.Now.Subtract(_fpLastMatchAt).TotalMilliseconds < FP_MIN_INTERVAL_MS Then Return

        Dim capRes As CaptureResult =
            _fpReader.Capture(Formats.Fid.ANSI, CaptureProcessing.DP_IMG_PROC_DEFAULT, 1200, _fpResolution)

        If capRes Is Nothing OrElse capRes.ResultCode <> ResultCode.DP_SUCCESS Then
            Return ' no finger / timeout / fail
        End If

        Dim fmdRes As DataResult(Of Fmd) =
            FeatureExtraction.CreateFmdFromFid(capRes.Data, Formats.Fmd.ANSI)

        If fmdRes Is Nothing OrElse fmdRes.ResultCode <> ResultCode.DP_SUCCESS OrElse fmdRes.Data Is Nothing Then
            Return
        End If

        Dim capturedFmd As Fmd = fmdRes.Data

        ' Match against DB templates (ALL fingers, since combobox is removed)
        Dim bestEmployee As String = FindEmployeeIdByFingerprint(capturedFmd)

        If String.IsNullOrWhiteSpace(bestEmployee) Then
            Return
        End If

        _fpLastMatchAt = DateTime.Now

        ' ✅ IMPORTANT: triggers your existing TextBoxUserID_TextChanged logic
        If TextBoxUserID.Text <> bestEmployee Then
            TextBoxUserID.Text = bestEmployee

            ' ✅ AUTO LOGIN
            AutoLogin()
            _fpLastMatchAt = DateTime.Now

        End If
    End Sub

    ' ============================================================
    ' MATCH (1:N) against BLOB templates in employee_fingerprints
    ' ============================================================
    Private Function FindEmployeeIdByFingerprint(capturedFmd As Fmd) As String
        Dim bestEmp As String = ""
        Dim bestScore As Integer = Integer.MaxValue

        Using c As New MySqlConnection(strConn)
            c.Open()

            ' ✅ No finger filter (since combobox is removed)
            Dim sql As String =
                "SELECT employee_id, finger_name, fmd_blob " &
                "FROM employee_fingerprints " &
                "WHERE fmd_format='ANSI' AND fmd_blob IS NOT NULL"

            Using cmd As New MySqlCommand(sql, c)
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        Dim blob As Byte() = TryCast(rdr("fmd_blob"), Byte())
                        If blob Is Nothing OrElse blob.Length = 0 Then Continue While

                        Dim imp = Importer.ImportFmd(blob, Formats.Fmd.ANSI, Formats.Fmd.ANSI)
                        If imp Is Nothing OrElse imp.ResultCode <> ResultCode.DP_SUCCESS OrElse imp.Data Is Nothing Then
                            Continue While
                        End If

                        Dim storedFmd As Fmd = imp.Data
                        Dim cr As CompareResult = Comparison.Compare(capturedFmd, 0, storedFmd, 0)

                        If cr Is Nothing OrElse cr.ResultCode <> ResultCode.DP_SUCCESS Then Continue While

                        If cr.Score < bestScore Then
                            bestScore = cr.Score
                            bestEmp = rdr("employee_id").ToString()
                        End If
                    End While
                End Using
            End Using
        End Using

        If bestEmp <> "" AndAlso bestScore < FP_THRESHOLD Then
            Return bestEmp
        End If

        Return ""
    End Function

    ' ============================================================
    ' YOUR EXISTING USER LOOKUP (UNCHANGED)
    ' ============================================================
    Private Sub TextBoxCashierID_TextChanged(sender As Object, e As EventArgs) Handles TextBoxUserID.TextChanged
        'for displaying cashier's info
        'Dim cmd As New MySqlCommand
        'Dim dt As New DataTable()
        'Dim adapter As New MySqlDataAdapter

        Try
            If conn.State <> ConnectionState.Open Then conn.Open()
            Dim cmd As New MySqlCommand
            Dim dt As New DataTable()
            Dim adapter As New MySqlDataAdapter

            cmd.CommandText = "SELECT CAST(AES_DECRYPT(first_name, 'strdjnltmyp') AS CHAR(50)), CAST(AES_DECRYPT(position, 'strdjnltmyp') AS CHAR(50)), picture FROM user_registration_tbl WHERE (user_id = '" & TextBoxUserID.Text & "')"
            'cmd.CommandText = "SELECT CAST(AES_DECRYPT(first_name, 'strdjnltmyp') AS CHAR(50)), CAST(AES_DECRYPT(position, 'strdjnltmyp') AS CHAR(50)) FROM user_registration_tbl WHERE (user_id = '" & TextBoxUserID.Text & "')"

            adapter.SelectCommand = cmd
            adapter.SelectCommand.Connection = conn
            adapter.Fill(dt)

            If dt.Rows.Count > 0 Then

                LabelName.Text = dt.Rows(0)(0).ToString()
                LabelPosition.Text = dt.Rows(0)(1).ToString()

                Dim img() As Byte = DirectCast(dt.Rows(0)(2), Byte())
                Dim ms As New MemoryStream(img)
                PictureBoxFace.Image = Image.FromStream(ms)

                LabelHello.Show()

                Try

                    If LabelPosition.Text = "Cashier" Then
                        Dim cmmd As New MySqlCommand
                        Dim dtt As New DataTable()
                        Dim adapterr As New MySqlDataAdapter

                        cmmd.CommandText = "SELECT * FROM accumulated_amount_tbl WHERE printed = @printed AND payment_date = @payment_date"
                        cmmd.CommandType = CommandType.Text
                        cmmd.Parameters.AddWithValue("@user_id", TextBoxUserID.Text)
                        cmmd.Parameters.AddWithValue("@printed", "yes")
                        cmmd.Parameters.AddWithValue("@payment_date", Today.ToString("yyyy-MM-dd"))

                        adapterr.SelectCommand = cmmd
                        adapterr.SelectCommand.Connection = conn
                        adapterr.Fill(dtt)

                        If dtt.Rows.Count > 0 Then
                            MsgBox("Cannot transact anymore!", MessageBoxIcon.Warning)
                            ButtonLogin.Enabled = False
                        Else
                            ButtonLogin.Enabled = True
                        End If

                    ElseIf LabelPosition.Text = "Manager" Then
                        ButtonLogin.Enabled = True
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try

            Else
                LabelName.Text = ""
                LabelPosition.Text = ""
                PictureBoxFace.Image = Nothing
                LabelHello.Hide()
                ButtonLogin.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            Try
                If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
            Catch
            End Try
        End Try
    End Sub

    Private Sub ButtonLogin_Click(sender As Object, e As EventArgs) Handles ButtonLogin.Click

        Try
            If LabelPosition.Text = "Manager" Then
                Me.Hide()
                management.Show()
            ElseIf LabelPosition.Text = "Cashier" Then
                Me.Hide()
                mainform.Show()
                secondscreen.Show()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'for saving user's login
        conn.Open()
        Using cmd As New MySqlCommand("INSERT INTO userlog_tbl(user_id, pos_id, date_login, time_login, position, upload) values (@user_id, @pos_id, @date_login, @time_login, @position, @upload)", conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@user_id", TextBoxUserID.Text)
            cmd.Parameters.AddWithValue("@pos_id", LabelPOSno.Text)
            cmd.Parameters.AddWithValue("@date_login", Today.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@time_login", Now.ToString("HH:mm:ss"))
            cmd.Parameters.AddWithValue("@position", LabelPosition.Text)
            cmd.Parameters.AddWithValue("@upload", "no")
            Try
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                conn.Close()
            End Try
        End Using

        'for userlog id
        conn.Open()
        Dim qd As String = "Select MAX(userlog_id) AS id FROM userlog_tbl"
        Dim cmdd As New MySqlCommand(qd) With {.Connection = conn}
        Dim rdr As MySqlDataReader = cmdd.ExecuteReader()
        While rdr.Read
            If LabelPosition.Text = "Manager" Then
                management.LabelLoginID.Text = Val(rdr.Item("id").ToString)
            ElseIf LabelPosition.Text = "Cashier" Then
                mainform.LabelLoginID.Text = Val(rdr.Item("id").ToString)
            End If
        End While

        If LabelPosition.Text = "Manager" Then
            management.LabelUserName.Text = LabelName.Text
            management.LabelUserID.Text = TextBoxUserID.Text
        ElseIf LabelPosition.Text = "Cashier" Then
            mainform.LabelCashierName.Text = LabelName.Text
            mainform.LabelCashierID.Text = TextBoxUserID.Text
        End If
        conn.Close()

        Try
            ' audit_trail_tbl
            conn.Open() ' Make sure the connection is open before executing the procedure
            Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

            Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("p_pos_id", LabelPOSno.Text)
                cmd.Parameters.AddWithValue("p_userid", TextBoxUserID.Text)
                cmd.Parameters.AddWithValue("p_username", LabelName.Text)
                cmd.Parameters.AddWithValue("p_approvedby", "")
                cmd.Parameters.AddWithValue("p_activity_performed", "ACCESS SYSTEM")
                cmd.Parameters.AddWithValue("p_module", "LOGIN")
                cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "-LOGIN SESSION")
                cmd.Parameters.AddWithValue("p_remarks", "ACCESS SYSTEM")
                cmd.ExecuteNonQuery()
            End Using
        Catch

        End Try

        Try
            'TRY UNLOCK EJOURNAL
            Dim folderPath As String = "C:\E-Journal"
            UnlockFolder(folderPath)
        Catch
            MsgBox("Error Unlock Folder!")
        End Try

    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Application.Exit()
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)

        Try
            StopAutoFingerprint()
        Catch
        End Try

        Try
            If _fpReader IsNot Nothing Then
                _fpReader.Dispose()
                _fpReader = Nothing
            End If
        Catch
        End Try
    End Sub

    ' ============================================================
    ' Fingerprint image helpers (kept, but hidden in UI)
    ' ============================================================
    Private Sub ClearFingerprintPicture()
        If pbFingerprint Is Nothing Then Return
        If pbFingerprint.Image IsNot Nothing Then
            Try : pbFingerprint.Image.Dispose() : Catch : End Try
            pbFingerprint.Image = Nothing
        End If
    End Sub

    Private Sub ShowFingerprintImage(fid As Fid)
        If pbFingerprint Is Nothing Then Return
        If fid Is Nothing OrElse fid.Views Is Nothing OrElse fid.Views.Count = 0 Then Return

        Dim v As Fid.Fiv = fid.Views(0)
        Dim bmp As Bitmap = CreateBitmap24(v.RawImage, v.Width, v.Height)

        If pbFingerprint.InvokeRequired Then
            pbFingerprint.BeginInvoke(CType(Sub() SetPicture(bmp), Action))
        Else
            SetPicture(bmp)
        End If
    End Sub

    Private Sub SetPicture(bmp As Bitmap)
        Dim old = pbFingerprint.Image
        pbFingerprint.Image = bmp
        If old IsNot Nothing Then
            Try : old.Dispose() : Catch : End Try
        End If
    End Sub

    Private Function CreateBitmap24(raw As Byte(), w As Integer, h As Integer) As Bitmap
        Dim rgb(raw.Length * 3 - 1) As Byte
        For i As Integer = 0 To raw.Length - 1
            Dim j = i * 3
            rgb(j) = raw(i)
            rgb(j + 1) = raw(i)
            rgb(j + 2) = raw(i)
        Next

        Dim bmp As New Bitmap(w, h, PixelFormat.Format24bppRgb)
        Dim bd As BitmapData = bmp.LockBits(New Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, bmp.PixelFormat)

        For y As Integer = 0 To h - 1
            Dim dest As IntPtr = bd.Scan0 + y * bd.Stride
            Marshal.Copy(rgb, y * w * 3, dest, w * 3)
        Next

        bmp.UnlockBits(bd)
        Return bmp
    End Function

    Public Sub UnlockFolder(folderPath As String)
        Dim dirInfo As New DirectoryInfo(folderPath)
        Dim dirSecurity As New DirectorySecurity()

        ' Restore inheritance
        dirSecurity.SetAccessRuleProtection(False, True)
        dirInfo.SetAccessControl(dirSecurity)
    End Sub


End Class
