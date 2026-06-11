Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class PscsException
    Inherits System.Exception

    Private _errorCode As Integer

    Public ReadOnly Property errorCode() As Integer
        Get
            Return _errorCode
        End Get
    End Property

    Private _message As String

    Public Overrides ReadOnly Property Message() As String
        Get
            Return _message
        End Get
    End Property

    Public Sub New(ByVal errCode As Integer)
        MyBase.New()
        _errorCode = errCode
        _message = PcscProvider.GetScardErrMsg(errCode)
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New()
        _message = message
    End Sub
End Class