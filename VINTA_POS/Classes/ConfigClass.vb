Public Class ConfigClass
    Public Shared Function Dataconn()

        Dim localhost As String = AppConfigReader.lclhst
        Dim port As String = AppConfigReader.prt
        Dim username As String = AppConfigReader.usrnm
        Dim password As String = AppConfigReader.psswrd
        Dim database As String = AppConfigReader.dtbs
        Dim connection As String = "server=" & localhost & ";" & "port=" & port & ";" & "username=" & username & ";" & "password=" & password & ";" & "database=" & database
        Return connection

    End Function

    Public Shared Function encrypt(s As String, v As Long) As String
        Dim total As String
        Dim tmp As String
        For i = 1 To Len(s)
            tmp = Mid(s, i, 1)
            tmp = Asc(tmp) + v
            tmp = Chr(tmp)
            total = total & tmp
        Next i
        encrypt = total
    End Function


    Public Shared Function decrypt(s As String, v As Long) As String
        Dim total As String
        Dim tmp As String
        For i = 1 To Len(s)
            tmp = Mid(s, i, 1)
            tmp = Asc(tmp) - v
            tmp = Chr(tmp)
            total = total & tmp
        Next i
        decrypt = total
    End Function

    Public Shared Function FDEandD()
        Dim lclhost As String = decrypt(AppConfigReader.lclhst, 2.5)
        Dim prt As String = decrypt(AppConfigReader.prt, 2.5)
        Dim usrnm As String = decrypt(AppConfigReader.usrnm, 2.5)
        Dim psswrd As String = decrypt(AppConfigReader.psswrd, 2.5)
        Dim db As String = decrypt(AppConfigReader.dtbs, 2.5)
        Dim connection As String = "server=" & lclhost & ";" & "port=" & prt & ";" & "username=" & usrnm & ";" & "password=" & psswrd & ";" & "database=" & db
        Return connection
    End Function

End Class
