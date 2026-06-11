Public Enum VALUEBLOCKOPERATION
    STORE = 0
    INCREMENT = 1
    DECREMENT = 2
End Enum

Public Class MifareClassic

    Private _pcscConnection As PscsReader
    Private apdu As Apdu

    Public Sub New()
    End Sub

    Public Sub New(ByVal readerName As String)
        _pcscConnection = New PscsReader(readerName)
    End Sub

    Public Sub New(ByVal pcsc As PscsReader)
        _pcscConnection = pcsc
    End Sub

    Public Property pcscConnection() As PscsReader
        Get
            Return _pcscConnection
        End Get
        Set(ByVal value As PscsReader)
            _pcscConnection = value
        End Set
    End Property

    Private Function getErrorMessage(ByVal sw1sw2 As Byte()) As String
        If (sw1sw2.Length < 2) Then
            Return "Unknown Status Word (" + Helper.byteAsString(sw1sw2, False) + ")"

        ElseIf (sw1sw2(0) = &H63 And sw1sw2(1) = &H0) Then
            Return "Command failed"
        Else
            Return "Unknown Status Word (" + Helper.byteAsString(sw1sw2, False) + ")"
        End If
    End Function

    Public Function isMifareClassic(ByVal atr As Byte()) As Boolean
        If (Not (atr Is Nothing) And atr.Length > 8 And Helper.byteArrayIsEqual(atr.Skip(4).Take(3).ToArray(), New Byte() {&H80, &H4F, &HC})) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub valueBlock(ByVal blockNumber As Byte, ByVal transType As VALUEBLOCKOPERATION, ByVal amount As Integer)
        Dim apdu As Apdu
        apdu = New Apdu()
        apdu.setCommand(New Byte() {&HFF, &HD7, &H0, blockNumber, &H5})

        apdu.data = New Byte(4) {}
        apdu.data(0) = CByte(transType)
        Array.Copy(Helper.intToByte(amount), 0, apdu.data, 1, 4)

        pcscConnection.sendCommand(apdu)

        If Not apdu.statusWordEqualTo(New Byte() {&H90, &H0}) Then
            Throw New CardException("Value block operation failed", apdu.statusWord)
        End If
    End Sub

    Public Sub store(ByVal blockNumber As Byte, ByVal amount As Int32)
        valueBlock(blockNumber, VALUEBLOCKOPERATION.STORE, amount)
    End Sub

    Public Sub decrement(ByVal blockNumber As Byte, ByVal amount As Int32)
        valueBlock(blockNumber, VALUEBLOCKOPERATION.DECREMENT, amount)
    End Sub

    Public Sub increment(ByVal blockNumber As Byte, ByVal amount As Int32)
        valueBlock(blockNumber, VALUEBLOCKOPERATION.INCREMENT, amount)
    End Sub

    Public Function inquireAmount(ByVal blockNumber As Byte) As Int32
        Dim apdu As Apdu

        apdu = New Apdu()
        apdu.setCommand(New Byte() {&HFF, &HB1, &H0, blockNumber, &H4})
        apdu.data = Nothing
        apdu.lengthExpected = 4

        pcscConnection.sendCommand(apdu)

        If apdu.statusWord(0) <> &H90 Then
            Throw New CardException("Read value failed", apdu.statusWord)
        End If


        Return Helper.byteToInt(apdu.response)
    End Function

    Public Sub restoreAmount(ByVal sourceBlock As Byte, ByVal targetBlock As Byte)
        Dim apdu As Apdu

        apdu = New Apdu()
        apdu.lengthExpected = 2

        apdu.setCommand(New Byte() {&HFF, &HD7, &H0, sourceBlock, &H2})

        apdu.data = New Byte(1) {}
        apdu.data(0) = &H3
        apdu.data(1) = targetBlock

        pcscConnection.sendCommand(apdu)

        If apdu.statusWord(0) <> &H90 Then
            Throw New CardException("Restore value failed", apdu.statusWord)
        End If

    End Sub

    Public Function readBinary(ByVal blockNumber As Byte, ByVal length As Byte) As Byte()

        apdu = New Apdu()

        apdu.setCommand(New Byte() {&HFF, &HB0, &H0, blockNumber, length})
        apdu.data = New Byte() {}
        apdu.lengthExpected = 16

        pcscConnection.sendCommand(apdu)
        If (apdu.statusWord(0) <> &H90) Then
            Throw New CardException("Read failed", apdu.statusWord)
        End If

        Return apdu.response.Take(16).ToArray()

    End Function

    Public Sub updateBinary(ByVal blockNumber As Byte, ByRef data() As Byte, ByVal length As Byte)

        apdu = New Apdu()

        If (data.Length > 48) Then
            Throw New Exception("Data has invalid length")
        End If

        'If (data.Length <> 16) Then
        '    Array.Resize(data, 16)
        'End If

        If (length Mod 16 <> 0) Then
            Throw New Exception("Data length must be multiple of 16")
        End If

        apdu.setCommand(New Byte() {&HFF, &HD6, &H0, blockNumber, length})
        'apdu.data = New Byte(15) {}
        apdu.lengthExpected = 16
        Array.Copy(data, apdu.data, length)

        pcscConnection.sendCommand(apdu)
        If (apdu.statusWord(0) <> &H90) Then
            Throw New CardException("Update failed", apdu.statusWord)
        End If

    End Sub

End Class
