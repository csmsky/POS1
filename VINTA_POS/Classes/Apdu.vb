Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Public Class Apdu
    Private _instructionClass As Byte
    Private _instructionCode As Byte
    Private _parameter1 As Byte
    Private _parameter2 As Byte
    Private _parameter3 As Byte
    Private _lengthExpected As Integer
    Private _actualLengthReceived As Integer

    Private _data As Byte()
    Private _response As Byte()
    Private _statusWord As Byte()

    Public Sub New()

    End Sub
    Public Sub New(ByVal instructionClass As Byte, ByVal instructionCode As Byte, ByVal param1 As Byte, ByVal param2 As Byte, ByVal param3 As Byte)
        Me.setCommand(New Byte() {instructionClass, instructionCode, param1, param2, param3})
    End Sub

    Public Sub New(ByVal cmd As Byte())
        If cmd.Length <> 5 Then
            Throw New Exception("Invalid command")
        End If

        Me.setCommand(New Byte() {cmd(0), cmd(1), cmd(2), cmd(3), cmd(4)})
    End Sub

    ''The T=0 instruction class
    Public Property instructionClass() As Byte
        Get
            Return _instructionClass
        End Get
        Set(ByVal value As Byte)
            _instructionClass = value
        End Set
    End Property

    ''An instruction code in the T=0 instruction class
    Public Property instructionCode() As Byte
        Get
            Return _instructionCode
        End Get
        Set(ByVal value As Byte)
            _instructionCode = value
        End Set
    End Property

    ''References codes that complete the instruction code
    Public Property parameter1() As Byte
        Get
            Return _parameter1
        End Get
        Set(ByVal value As Byte)
            _parameter1 = value
        End Set
    End Property

    ''Reference codes that complete the instruction code
    Public Property parameter2() As Byte
        Get
            Return _parameter2
        End Get
        Set(ByVal value As Byte)
            _parameter2 = value
        End Set
    End Property

    ''the number of data bytes to be transmitted during the command, per ISO 7816-4,Section 8.2.1.
    Public Property parameter3() As Byte
        Get
            Return _parameter3
        End Get
        Set(ByVal value As Byte)
            _parameter3 = value
        End Set
    End Property

    ''Length of data expected from the card
    Public Property lengthExpected() As Integer
        Get
            Return _lengthExpected
        End Get
        Set(ByVal value As Integer)
            _lengthExpected = value
        End Set
    End Property

    Public Property actualLengthReceived() As Integer
        Get
            Return _actualLengthReceived
        End Get
        Set(ByVal value As Integer)
            _actualLengthReceived = value
        End Set
    End Property

    Public Property data() As Byte()
        Get
            Return _data
        End Get
        Set(ByVal value As Byte())
            _data = value
        End Set
    End Property

    Public Property response() As Byte()
        Get
            Return _response
        End Get
        Set(ByVal value As Byte())
            _response = value
        End Set
    End Property

    Public Property statusWord() As Byte()
        Get
            Return _statusWord
        End Get
        Set(ByVal value As Byte())
            _statusWord = value
        End Set
    End Property

    Public Sub setCommand(ByVal cmd As Byte())
        If cmd.Length <> 5 Then
            Throw New Exception("Invalid Command")
        End If

        instructionClass = cmd(0)
        instructionCode = cmd(1)
        parameter1 = cmd(2)
        parameter2 = cmd(3)
        parameter3 = cmd(4)

        data = New Byte((parameter3) - 1) {}

    End Sub

    Public Function statusWordEqualTo(ByVal data As Byte()) As Boolean
        If statusWord Is Nothing Then
            Return False
        End If

        For i As Integer = 0 To i < statusWord.Length - 1
            If statusWord(i) <> data(i) Then
                Return False
            End If
        Next

        Return True
    End Function


End Class
