Imports System.Threading
Imports System.Threading.Tasks
Imports DPUruNet
Imports DPUruNet.Constants
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms ' Added to allow the InputBox popup to render

Public Class FingerprintManagerAuth

    Public Event StatusChanged(message As String)
    Public Event Authorized(managerId As String)
    Public Event Failed(message As String)

    Private _fpReader As Reader = Nothing
    Private _busy As Boolean = False

    Private ReadOnly _connStr As String
    Private ReadOnly _threshold As Integer

    Public Sub New(connStr As String, Optional threshold As Integer = 30000) '30000
        _connStr = connStr
        _threshold = threshold
    End Sub

    Private Sub SetStatus(msg As String)
        RaiseEvent StatusChanged(msg)
    End Sub

    ' ============================
    ' ASYNC AUTH (NON-BLOCKING UI)
    ' ============================
    Public Async Function AuthorizeOnceAsync(Optional ct As CancellationToken = Nothing) As Task(Of String)
        If _busy Then Return ""
        _busy = True

        Try
            SetStatus("Place MANAGER finger to authorize...")

            ' =========================================================================
            ' BYPASS INJECTED HERE (Original background scanner task commented out)
            ' =========================================================================
            ' Dim empId As String = Await Task.Run(Function()
            '                                          If ct.IsCancellationRequested Then Return ""
            '                                          Return ScanAndMatchManager(ct)
            '                                      End Function, ct)

            Dim inputId As String = InputBox("TEST MODE: Enter a Manager User ID to bypass fingerprint scan:", "Manager Authorization Bypass", "")

            If String.IsNullOrWhiteSpace(inputId) Then
                SetStatus("Cancelled.")
                RaiseEvent Failed("Cancelled.")
                Return ""
            End If

            Dim empId As String = ""
            Dim isMgr As Boolean = Await Task.Run(Function() IsManager(inputId), ct)

            If isMgr Then
                empId = inputId
            End If
            ' =========================================================================

            If Not String.IsNullOrWhiteSpace(empId) Then
                SetStatus("Authorized ✅ Manager: " & empId)
                RaiseEvent Authorized(empId)
                Return empId
            Else
                If ct.IsCancellationRequested Then
                    SetStatus("Cancelled.")
                    RaiseEvent Failed("Cancelled.")
                Else
                    SetStatus("Not authorized.")
                    RaiseEvent Failed("No match / not manager.")
                End If
                Return ""
            End If

        Catch ex As OperationCanceledException
            SetStatus("Cancelled.")
            RaiseEvent Failed("Cancelled.")
            Return ""
        Catch ex As Exception
            SetStatus("FP Error: " & ex.Message)
            RaiseEvent Failed(ex.Message)
            Return ""
        Finally
            _busy = False
            CleanupReader()
        End Try
    End Function

    ' ============================
    ' CAPTURE + MATCH (WORKER)
    ' ============================
    Private Function ScanAndMatchManager(ct As CancellationToken) As String
        Dim rc = ReaderCollection.GetReaders()
        If rc Is Nothing OrElse rc.Count = 0 Then
            SetStatus("No fingerprint reader detected.")
            Return ""
        End If

        ct.ThrowIfCancellationRequested()

        _fpReader = rc(0)
        _fpReader.Open(CapturePriority.DP_PRIORITY_COOPERATIVE)

        Dim resolution As Integer = 500
        If _fpReader.Capabilities IsNot Nothing AndAlso
               _fpReader.Capabilities.Resolutions IsNot Nothing AndAlso
               _fpReader.Capabilities.Resolutions.Length > 0 Then
            resolution = _fpReader.Capabilities.Resolutions(0)
        End If

        ct.ThrowIfCancellationRequested()

        ' Capture timeout (ms)
        Dim capRes As CaptureResult =
                _fpReader.Capture(Formats.Fid.ANSI, CaptureProcessing.DP_IMG_PROC_DEFAULT, 2000, resolution)

        If capRes Is Nothing Then
            SetStatus("Capture returned nothing.")
            Return ""
        End If

        If capRes.ResultCode <> ResultCode.DP_SUCCESS Then
            SetStatus("Capture: " & capRes.ResultCode.ToString())
            Return ""
        End If

        ct.ThrowIfCancellationRequested()

        Dim fmdRes As DataResult(Of Fmd) =
                FeatureExtraction.CreateFmdFromFid(capRes.Data, Formats.Fmd.ANSI)

        If fmdRes Is Nothing OrElse fmdRes.ResultCode <> ResultCode.DP_SUCCESS OrElse fmdRes.Data Is Nothing Then
            SetStatus("FMD error: " & If(fmdRes Is Nothing, "null", fmdRes.ResultCode.ToString()))
            Return ""
        End If

        ct.ThrowIfCancellationRequested()

        Dim empId As String = FindEmployeeIdByFingerprint(fmdRes.Data, ct)

        If String.IsNullOrWhiteSpace(empId) Then
            SetStatus("No match.")
        End If

        Return empId
    End Function

    Private Function FindEmployeeIdByFingerprint(capturedFmd As Fmd, ct As CancellationToken) As String
        Dim bestEmp As String = ""
        Dim bestScore As Integer = Integer.MaxValue

        Using c As New MySqlConnection(_connStr)
            c.Open()

            Dim sql As String =
                    "SELECT employee_id, fmd_blob " &
                    "FROM employee_fingerprints " &
                    "WHERE fmd_format='ANSI' AND fmd_blob IS NOT NULL"

            Using cmd As New MySqlCommand(sql, c)
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()

                        ct.ThrowIfCancellationRequested()

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

        If bestEmp <> "" AndAlso bestScore < _threshold Then
            If IsManager(bestEmp) Then
                Return bestEmp
            Else
                SetStatus("Matched but not Manager.")
            End If
        End If

        Return ""
    End Function

    Private Function IsManager(userId As String) As Boolean
        Using c As New MySqlConnection(_connStr)
            c.Open()

            Dim sql As String =
                    "SELECT COUNT(*) FROM user_registration_tbl " &
                    "WHERE user_id=@id AND CAST(AES_DECRYPT(position,'strdjnltmyp') AS CHAR(50))='Manager'"

            Using cmd As New MySqlCommand(sql, c)
                cmd.Parameters.AddWithValue("@id", userId)
                Dim n As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return n > 0
            End Using
        End Using
    End Function

    Public Sub CancelAndCleanup()
        CleanupReader()
    End Sub

    Private Sub CleanupReader()
        Try
            If _fpReader IsNot Nothing Then
                _fpReader.Dispose()
                _fpReader = Nothing
            End If
        Catch
        End Try
    End Sub

End Class