Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Delegate Sub TransmitApduDelegate(ByVal sender As Object, ByVal e As TransmitApduEventArg)

Public Class TransmitApduEventArg
    Inherits EventArgs
    Private data_ As Byte()
    Public Property data() As Byte()
        Get
            Return data_
        End Get
        Set(ByVal value As Byte())
            data_ = value
        End Set
    End Property

    Public Sub New(ByVal data As Byte())
        data_ = data
    End Sub

    Public Function GetAsString(ByVal spaceinBetween As Boolean) As String
        If data_ Is Nothing Then
            Return ""
        End If

        Dim tmpStr As String = String.Empty
        For i As Integer = 0 To data_.Length - 1
            tmpStr += String.Format("{0:X2}", data_(i))

            If spaceinBetween Then
                tmpStr += " "
            End If
        Next
        Return tmpStr
    End Function
End Class

Public Class PscsReader
    Private hCard_ As New IntPtr()
    Private hContext_ As New IntPtr()
    Private pProtocol_ As Integer = PcscProvider.SCARD_PROTOCOL_T0 Or PcscProvider.SCARD_PROTOCOL_T1
    Private pdwActiveProtocol_ As Integer
    Private shareMode_ As Integer = PcscProvider.SCARD_SHARE_SHARED
    Private _operationControlCode As UInteger = 0
    Private _readerName As String = ""
    Private lastError As Integer = 0
    Private _apduCommand As New Apdu()

    Public Event OnSendCommand As TransmitApduDelegate
    Public Event OnReceivedCommand As TransmitApduDelegate

    Public Sub New()
        establishContext()
    End Sub

    Public Sub New(ByVal readerName As String)
        _readerName = readerName
        establishContext()
    End Sub

    Public Property cardHandle() As IntPtr
        Get
            Return hCard_
        End Get
        Set(ByVal value As IntPtr)
            hCard_ = value
        End Set
    End Property

    Public Property resourceMngrContext() As IntPtr
        Get
            Return hContext_
        End Get
        Set(ByVal value As IntPtr)
            hContext_ = value
        End Set
    End Property

    Public Property preferedProtocol() As Integer
        Get
            Return pProtocol_
        End Get
        Set(ByVal value As Integer)
            pProtocol_ = value
        End Set
    End Property

    Public Property activeProtocol() As Integer
        Get
            Return pdwActiveProtocol_
        End Get
        Set(ByVal value As Integer)
            pdwActiveProtocol_ = value
        End Set
    End Property

    Public Property shareMode() As Integer
        Get
            Return shareMode_
        End Get
        Set(ByVal value As Integer)
            shareMode_ = value
        End Set
    End Property

    Public Property readerName() As String
        Get
            Return _readerName
        End Get
        Set(ByVal value As String)
            _readerName = value
        End Set
    End Property

    Public Property apduCommand() As Apdu
        Get
            Return _apduCommand
        End Get
        Set(ByVal value As Apdu)
            _apduCommand = value
        End Set
    End Property

    Public ReadOnly Property pcscConnection() As PscsReader
        Get
            Return Me
        End Get
    End Property

    Public Property operationControlCode() As UInteger
        Get
            Return _operationControlCode
        End Get
        Set(ByVal value As UInteger)
            _operationControlCode = value
        End Set
    End Property

#Region "protected Methods"

    Public Sub establishContext()
        Dim retCode As Integer
        retCode = PcscProvider.SCardEstablishContext(PcscProvider.SCARD_SCOPE_USER, 0, 0, hContext_)
        If retCode <> PcscProvider.SCARD_S_SUCCESS Then
            Throw New Exception("Unable to establish context - " & PcscProvider.GetScardErrMsg(retCode))
        End If
    End Sub

    Public Sub releaseContext()
        Dim retCode As Integer = PcscProvider.SCardReleaseContext(hContext_)
        If retCode <> PcscProvider.SCARD_S_SUCCESS Then
            Throw New PscsException(retCode)
        End If
    End Sub

    Protected Sub resetContext()
        releaseContext()
        establishContext()
    End Sub

#End Region

    Public Sub connect()
        If _readerName.Trim() = "" Then
            Throw New Exception("Smartacard reader is not specified")
        End If

        connect(_readerName, pProtocol_, shareMode_)
    End Sub

    Public Sub connect(ByVal readerName As String)
        _readerName = readerName
        connect(_readerName, pProtocol_, shareMode_)
    End Sub

    Public Sub connect(ByVal readerName As String, ByVal preferedProtocol As Integer, ByVal shareMode As Integer)
        Dim returnCode As Integer

        returnCode = PcscProvider.SCardConnect(hContext_, readerName, shareMode, preferedProtocol, hCard_, pdwActiveProtocol_)
        If returnCode <> PcscProvider.SCARD_S_SUCCESS Then
            lastError = returnCode
            ''Throw New PscsException(returnCode)

        End If

        shareMode_ = shareMode
        pProtocol_ = preferedProtocol
        _readerName = readerName
    End Sub

    Public Sub connectDirect()
        connect(readerName, PcscProvider.SCARD_PROTOCOL_UNDEFINED, PcscProvider.SCARD_SHARE_DIRECT)
    End Sub

    Public Function getReaderList() As String()
        Dim returnData As Byte()
        Dim sReaderGroup As Byte() = Nothing
        Dim readerList As String() = New String(-1) {}
        Dim readerString As String = String.Empty
        Dim returnCode As Integer
        Dim hContext As New IntPtr()
        Dim readerCount As Integer = 255

        returnCode = PcscProvider.SCardEstablishContext(PcscProvider.SCARD_SCOPE_USER, 0, 0, hContext)
        If returnCode <> PcscProvider.SCARD_S_SUCCESS Then
            lastError = returnCode
            Return readerList
        End If

        returnCode = PcscProvider.SCardListReaders(hContext_, Nothing, Nothing, readerCount)
        If returnCode <> PcscProvider.SCARD_S_SUCCESS Then
            lastError = returnCode
            Return readerList
        End If

        returnData = New Byte(readerCount - 1) {}

        returnCode = PcscProvider.SCardListReaders(hContext_, sReaderGroup, returnData, readerCount)
        If returnCode <> PcscProvider.SCARD_S_SUCCESS Then
            Return readerList
        End If


        readerString = System.Text.ASCIIEncoding.ASCII.GetString(returnData).Trim(ControlChars.NullChar)
        readerList = readerString.Split(ControlChars.NullChar)

        Return readerList
    End Function

    Public Sub disconnect()
        Dim returnValue As Integer = PcscProvider.SCardDisconnect(hCard_, PcscProvider.SCARD_UNPOWER_CARD)
        If returnValue <> PcscProvider.SCARD_S_SUCCESS Then
            Throw New PscsException(returnValue)
        End If
    End Sub

    Public Sub sendCommand(ByRef apdu As Apdu)
        apduCommand = apdu
        sendCommand()
        apdu = apduCommand
    End Sub

    Public Sub beginTransaction()
        Dim returnValue As Integer = PcscProvider.SCardBeginTransaction(hCard_)
        If returnValue <> PcscProvider.SCARD_S_SUCCESS Then
            '  Throw New PscsException(returnValue)
        End If
    End Sub

    Public Sub endTransaction()
        Dim returnValue As Integer = PcscProvider.SCardEndTransaction(hCard_, PcscProvider.SCARD_LEAVE_CARD)
        If returnValue <> PcscProvider.SCARD_S_SUCCESS Then
            'Throw New PscsException(returnValue)
        End If
    End Sub

    Public Sub scardStatus()
        Dim readerNameLen As Integer = 0
        Dim state As Integer = 0
        Dim atrLen As Integer = 33
        Dim atr As Byte() = Nothing

        Dim returnValue As Integer = PcscProvider.SCardStatus(hCard_, readerName, readerNameLen, state, pProtocol_, atr,
         atrLen)

        If returnValue <> PcscProvider.SCARD_S_SUCCESS Then
            Throw New PscsException(returnValue)
        End If
    End Sub

    Public Sub sendCommand()
        Dim sendBuff As Byte(), recvBuff As Byte()
        Dim sendLen As Integer, recvLen As Integer, returnCode As Integer
        Dim ioRequest As PcscProvider.SCARD_IO_REQUEST

        ioRequest.dwProtocol = pdwActiveProtocol_
        ioRequest.cbPciLength = 8

        If apduCommand.data Is Nothing Then
            sendBuff = New Byte(4) {}
        Else
            sendBuff = New Byte(5 + (apduCommand.data.Length - 1)) {}
        End If


        recvLen = apduCommand.lengthExpected + 2


        Array.Copy(New Byte() {apduCommand.instructionClass, apduCommand.instructionCode, apduCommand.parameter1, apduCommand.parameter2, apduCommand.parameter3}, sendBuff, 5)

        If apduCommand.data IsNot Nothing Then
            Array.Copy(apduCommand.data, 0, sendBuff, 5, apduCommand.data.Length)
        End If

        sendLen = sendBuff.Length

        apduCommand.statusWord = New Byte(1) {}
        recvBuff = New Byte(recvLen - 1) {}

        sendCommandTriggerEvent(New TransmitApduEventArg(sendBuff))
        returnCode = PcscProvider.SCardTransmit(hCard_, ioRequest, sendBuff, sendLen, ioRequest, recvBuff,
         recvLen)
        If returnCode = 0 Then
            receivedCommandTriggerEvent(New TransmitApduEventArg(recvBuff.Take(recvLen).ToArray()))
            If recvLen > 1 Then
                Array.Copy(recvBuff, recvLen - 2, apduCommand.statusWord, 0, 2)
            End If

            If recvLen > 2 Then
                apduCommand.response = New Byte(recvLen - 3) {}
                Array.Copy(recvBuff, 0, apduCommand.response, 0, recvLen - 2)
            End If
        Else

            'Throw New PscsException(returnCode)
        End If
    End Sub

    Public Sub sendCardControl(ByRef apdu As Apdu, ByVal controlCode As UInteger)
        apduCommand = apdu
        operationControlCode = controlCode
        sendCardControl()
        apdu = apduCommand
    End Sub

    Public Sub sendCardControl()
        Dim sendBuff As Byte(), recvbuff As Byte()
        Dim sendLen As Integer, recvLen As Integer, returnCode As Integer, actualLength As Integer = 0
        Dim ioRequest As PcscProvider.SCARD_IO_REQUEST

        ioRequest.dwProtocol = pdwActiveProtocol_
        ioRequest.cbPciLength = 8

        If apduCommand.data Is Nothing Then
            Throw New Exception("No data specified")
        End If

        sendBuff = New Byte(apduCommand.data.Length - 1) {}
        recvLen = apduCommand.lengthExpected

        Array.Copy(apduCommand.data, 0, sendBuff, 0, apduCommand.data.Length)

        sendLen = sendBuff.Length

        apduCommand.statusWord = New Byte(1) {}
        recvbuff = New Byte(recvLen - 1) {}

        sendCommandTriggerEvent(New TransmitApduEventArg(sendBuff))
        returnCode = PcscProvider.SCardControl(hCard_, operationControlCode, sendBuff, sendLen, recvbuff, recvbuff.Length,
         actualLength)

        If returnCode = 0 Then
            apduCommand.actualLengthReceived = actualLength

            receivedCommandTriggerEvent(New TransmitApduEventArg(recvbuff.Take(actualLength).ToArray()))

            apduCommand.response = New Byte(actualLength - 1) {}
            Array.Copy(recvbuff, 0, apduCommand.response, 0, actualLength)

            If actualLength > 1 Then
                Array.Copy(recvbuff, actualLength - 2, apduCommand.statusWord, 0, 2)

                'if (apdu.actualLengthReceived >= 2)
                '{
                '    apdu.receiveData = new byte[actualLength - 2];
                '    Array.Copy(recvbuff, 0, apdu.receiveData, 0, actualLength - 2);
                '}
            End If
        Else
            Throw New PscsException(returnCode)
        End If

    End Sub

    Public Overridable Function getAtr() As Byte()
        Dim rdrLen As Integer = 0, retCode As Integer, protocol As Integer = activeProtocol
        Dim pdwSate As Integer = 0, atrLen As Integer = 33
        Dim atr As Byte() = New Byte(99) {}


        retCode = PcscProvider.SCardStatus(cardHandle, readerName, rdrLen, pdwSate, protocol, atr,
         atrLen)
        If retCode <> 0 Then
            Throw New PscsException(retCode)
        End If

        Return atr.Take(atrLen).ToArray()
    End Function

    Public Overridable Function getFirmwareVersion() As Byte()
        Throw New NotImplementedException()
    End Function

    Private Sub sendCommandTriggerEvent(ByVal e As TransmitApduEventArg)
        RaiseEvent OnSendCommand(Me, e)
    End Sub

    Private Sub receivedCommandTriggerEvent(ByVal e As TransmitApduEventArg)
        RaiseEvent OnReceivedCommand(Me, e)
    End Sub
End Class
