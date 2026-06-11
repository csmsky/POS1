Imports System.ComponentModel

Public Delegate Sub CardStatusChangeDelegate(ByVal sender As Object, ByVal e As CardPollingEventArg)
Public Delegate Sub CardPollingErrorDelegate(ByVal sender As Object, ByVal e As CardPollingErrorEventArg)

Public Enum CARD_STATUS
    UNKNOWN = 0
    CARD_FOUND = 1
    CARD_NOT_FOUND = 2
    [ERROR] = 3
End Enum

Public Class CardPollingEventArg
    Inherits EventArgs
    Public reader As String
    Public status As CARD_STATUS
    Public atr As Byte()
    Friend currentStatus As Integer
End Class

Public Class CardPollingErrorEventArg
    Inherits EventArgs
    Public reader As String
    Public errorMessage As String
    Public errorCode As Integer
End Class

Public Class CardPolling
    Private doCardPolling As Boolean = False
    Private threadpollStatusLock As New Object()
    Private _readers As New List(Of String)()
    Private threadPoll As Dictionary(Of String, BackgroundWorker) = Nothing
    Private threadPollCardStatus As Dictionary(Of String, CARD_STATUS) = Nothing

    Public Event OnCardFound As CardStatusChangeDelegate
    Public Event OnCardRemoved As CardStatusChangeDelegate
    Public Event OnError As CardPollingErrorDelegate


    Public Function isBusy() As Boolean
        If threadPoll Is Nothing Then
            Return False
        End If

        If threadPoll.Count < 1 Then
            Return False
        End If

        For Each key As String In threadPoll.Keys
            If threadPoll(key).IsBusy Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Function isBusy(ByVal readerName As String) As Boolean
        If threadPoll Is Nothing Then
            Return False
        End If

        If threadPoll.Count < 1 Then
            Return False
        End If

        For Each key As String In threadPoll.Keys
            If key.Trim() = readerName.Trim() Then
                If threadPoll(key).IsBusy Then
                    Return True
                Else
                    Return False
                End If
            End If
        Next


        Return False
    End Function

    Public Sub fillReader()
        Dim ReaderCount As Integer = 255
        Dim retData As Byte()
        Dim sReaderGroup As Byte() = Nothing
        Dim readerList As String()
        Dim readerStr As String = String.Empty
        readerList = Nothing
        Dim retCode As Integer
        Dim hContext As New IntPtr()

        If doCardPolling Then
            Throw New Exception("Card polling already started")
        End If

        retCode = PcscProvider.SCardEstablishContext(PcscProvider.SCARD_SCOPE_USER, 0, 0, hContext)
        If retCode <> PcscProvider.SCARD_S_SUCCESS Then
            Throw New Exception("Unable to establish context - " & PcscProvider.GetScardErrMsg(retCode))
        End If

        retCode = PcscProvider.SCardListReaders(hContext, Nothing, Nothing, ReaderCount)
        If retCode <> PcscProvider.SCARD_S_SUCCESS Then
            Throw New Exception("List Reader Failed - " & PcscProvider.GetScardErrMsg(retCode))
        End If

        retData = New Byte(ReaderCount - 1) {}

        retCode = PcscProvider.SCardListReaders(hContext, sReaderGroup, retData, ReaderCount)
        If retCode <> PcscProvider.SCARD_S_SUCCESS Then
            Throw New Exception("List Reader Failed - " & PcscProvider.GetScardErrMsg(retCode))
        End If

        'Convert retData(Hexadecimal) value to String 
        readerStr = System.Text.ASCIIEncoding.ASCII.GetString(retData).Trim(ControlChars.NullChar)
        readerList = readerStr.Split(ControlChars.NullChar)

        For Each rdr As String In readerList
            _readers.Add(rdr)
        Next
    End Sub

    Public Sub add(ByVal readerName As String)
        If doCardPolling Then
            Throw New Exception("Card polling already started")
        End If

        If readerName.Trim() = "" Then
            Return
        End If

        If Not _readers.Contains(readerName) Then
            _readers.Add(readerName)
        End If
    End Sub

    Public Sub addRange(ByVal readers As String())
        If doCardPolling Then
            Throw New Exception("Card polling already started")
        End If

        For Each rdr As String In _readers
            _readers.Add(rdr)
        Next
    End Sub

    Public Sub clear()
        If doCardPolling Then
            Throw New Exception("Card polling already started")
        End If

        _readers.Clear()
    End Sub

    Public Sub start()

        If doCardPolling Then
            Throw New Exception("Card polling already started")
        End If

        If _readers.Count > 1 Then
            Try

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

        doCardPolling = True

        threadPoll = New Dictionary(Of String, BackgroundWorker)()
        threadPollCardStatus = New Dictionary(Of String, CARD_STATUS)()
        For Each rdr As String In _readers
            Dim bw As New BackgroundWorker()
            bw.WorkerReportsProgress = True
            bw.WorkerSupportsCancellation = True

            AddHandler bw.ProgressChanged, AddressOf bw_ProgressChanged
            AddHandler bw.DoWork, AddressOf bw_DoWork

            bw.RunWorkerAsync(rdr)
            threadPoll.Add(rdr, bw)
            threadPollCardStatus.Add(rdr, CARD_STATUS.UNKNOWN)
        Next
    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        If e.ProgressPercentage >= 0 Then
            Dim arg As CardPollingEventArg = DirectCast(e.UserState, CardPollingEventArg)
            'lock (threadpollStatusLock)
            threadPollCardStatus(arg.reader) = arg.status
            Select Case arg.status
                Case CARD_STATUS.CARD_FOUND
                    CardFound(arg)
                    Exit Select
                Case CARD_STATUS.CARD_NOT_FOUND
                    CardRemove(arg)
                    Exit Select
            End Select
        ElseIf e.ProgressPercentage = -1 Then
            Dim arg As CardPollingErrorEventArg = DirectCast(e.UserState, CardPollingErrorEventArg)
            'lock (threadpollStatusLock)
            threadPollCardStatus(arg.reader) = CARD_STATUS.[ERROR]
            CardError(arg)
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Try
            Dim retCode As Integer
            Dim hContext As New IntPtr()
            Dim arg As New CardPollingEventArg()
            arg.reader = e.Argument.ToString()
            arg.status = CARD_STATUS.UNKNOWN

            retCode = PcscProvider.SCardEstablishContext(PcscProvider.SCARD_SCOPE_USER, 0, 0, hContext)
            If retCode <> PcscProvider.SCARD_S_SUCCESS Then
                Throw New Exception("Unable to establish context", New Exception(PcscProvider.GetScardErrMsg(retCode)))
            End If

            Dim bwOwner As BackgroundWorker = DirectCast(sender, BackgroundWorker)


            While Not bwOwner.CancellationPending

                Dim state As New PcscProvider.SCARD_READERSTATE()
                state.szReader = e.Argument.ToString()

                retCode = PcscProvider.SCardGetStatusChange(hContext, 3000, state, 1)
                If retCode <> 0 Then
                    If arg.status <> CARD_STATUS.[ERROR] Then
                        arg.status = CARD_STATUS.[ERROR]
                        Dim errorArg As New CardPollingErrorEventArg()
                        errorArg.errorCode = retCode
                        errorArg.errorMessage = PcscProvider.GetScardErrMsg(retCode)
                        errorArg.reader = e.Argument.ToString()
                        bwOwner.ReportProgress(-1, errorArg)
                    End If
                Else
                    'state.dwCurrentState >>= 32;                        
                    If (state.dwEventState And PcscProvider.SCARD_STATE_PRESENT) = PcscProvider.SCARD_STATE_PRESENT Then
                        If arg.status <> CARD_STATUS.CARD_FOUND Then
                            arg.status = CARD_STATUS.CARD_FOUND
                            arg.atr = state.rgbAtr.Take(state.cbAtr).ToArray()
                            bwOwner.ReportProgress(0, arg)
                        End If
                    Else
                        If arg.status <> CARD_STATUS.CARD_NOT_FOUND Then
                            arg.status = CARD_STATUS.CARD_NOT_FOUND
                            bwOwner.ReportProgress(0, arg)
                        End If
                    End If
                End If

                System.Threading.Thread.Sleep(500)
            End While

            PcscProvider.SCardReleaseContext(hContext)

        Catch ex As Exception
        End Try

    End Sub

    Public Sub StopPolling()
        If threadPoll Is Nothing Then
            Return
        End If

        For Each k As String In threadPoll.Keys
            threadPoll(k).CancelAsync()
        Next

        doCardPolling = False
    End Sub

    Public Function getCardStatus(ByVal readername As String) As CARD_STATUS
        Try
            'lock (threadpollStatusLock)
            '{
            If Not threadPollCardStatus.ContainsKey(readername) Then
                Throw New Exception("Reader not found")
            End If

            '}
            Return threadPollCardStatus(readername)
        Catch ex As Exception
            Return CARD_STATUS.UNKNOWN
        End Try
    End Function

    Private Sub CardFound(ByVal e As CardPollingEventArg)
        RaiseEvent OnCardFound(Me, e)
    End Sub

    Private Sub CardRemove(ByVal e As CardPollingEventArg)
        RaiseEvent OnCardRemoved(Me, e)
    End Sub

    Private Sub CardError(ByVal e As CardPollingErrorEventArg)
        RaiseEvent OnError(Me, e)
    End Sub

End Class
