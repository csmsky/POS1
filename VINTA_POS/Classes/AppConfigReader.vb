Public Class AppConfigReader

    Private Shared aSettingsReader As New System.Configuration.AppSettingsReader
    Private Shared localhost As String = aSettingsReader.GetValue("localhost", GetType(String))
    Private Shared port As String = aSettingsReader.GetValue("port", GetType(String))
    Private Shared username As String = aSettingsReader.GetValue("username", GetType(String))
    Private Shared password As String = aSettingsReader.GetValue("password", GetType(String))
    Private Shared database As String = aSettingsReader.GetValue("database", GetType(String))
    Private Shared pos As String = aSettingsReader.GetValue("pos", GetType(String))
    Private Shared station As String = aSettingsReader.GetValue("station", GetType(String))
    Private Shared serial As String = aSettingsReader.GetValue("serial", GetType(String))

    Public Shared ReadOnly Property lclhst() As String
        Get
            Return localhost
        End Get
    End Property

    Public Shared ReadOnly Property prt() As String
        Get
            Return port
        End Get
    End Property

    Public Shared ReadOnly Property usrnm() As String
        Get
            Return username
        End Get
    End Property

    Public Shared ReadOnly Property psswrd() As String
        Get
            Return password
        End Get
    End Property
    Public Shared ReadOnly Property dtbs() As String
        Get
            Return database
        End Get
    End Property

    Public Shared ReadOnly Property ps() As String
        Get
            Return pos
        End Get
    End Property

    Public Shared ReadOnly Property sttn() As String
        Get
            Return station
        End Get
    End Property

    Public Shared ReadOnly Property srl() As String
        Get
            Return serial
        End Get
    End Property
End Class
