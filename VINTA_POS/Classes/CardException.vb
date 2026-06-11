'===========================================================================================
' * 
' *  Copyright (C)   : Advanced Card System Ltd
' * 
' *  File            : CardException.cs
' * 
' *  Description     : Contains Methods and Properties for smart card related exceptions
' * 
' *  Author          : Arturo Salvamante
' *  
' *  Date            : June 03, 2011
' * 
' *  Revision Traile : [Author] / [Date if modification] / [Details of Modifications done] 
' * 
' * =========================================================================================


Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class CardException

    Inherits Exception
    Protected _statusWord As Byte()
    Public ReadOnly Property statusWord() As Byte()
        Get
            Return _statusWord
        End Get
    End Property

    Protected _message As String
    Public Overrides ReadOnly Property Message() As String
        Get
            Return _message
        End Get
    End Property

    Public Sub New(ByVal message As String, ByVal sw As Byte())
        _message = message
        _statusWord = sw
    End Sub
End Class
