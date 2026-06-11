Imports System.Net
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Imports Newtonsoft.Json

Public Class HikOpenApiClient

    'Public Const HOST As String = "https://stph.ddns.net:8443"     'APP_KEY: 42112151          APP_SECRET: MO8G9nfRlp2h8KwuTSto    
    'Public Const HOST As String = "https://192.168.1.91:443"
    Public Const HOST As String = "https://192.168.10.181:443"     'APP_KEY: 60541179          APP_SECRET: OoPxUiTmgKL0GnwWZqeb   
    Public Const APP_KEY As String = "40055628"                 '60541179
    Public Const APP_SECRET As String = "3dmqw0AUcisHMxUQbtpX"  'OoPxUiTmgKL0GnwWZqeb
    Public Const USER_ID As String = "admin"

    Private Shared Function Md5Base64(jsonBody As String) As String
        Using md5 As MD5 = MD5.Create()
            Return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(jsonBody)))
        End Using
    End Function

    Private Shared Function HmacSha256Base64(secret As String, text As String) As String
        Using hmac As New HMACSHA256(Encoding.UTF8.GetBytes(secret))
            Return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(text)))
        End Using
    End Function

    Private Shared Function CallApi(path As String, bodyObj As Object) As String
        Dim url As String = HOST & path
        Dim bodyJson As String = JsonConvert.SerializeObject(bodyObj)

        Dim accept As String = "*/*" ' <<< fix
        Dim contentType As String = "application/json;charset=UTF-8"
        Dim contentMd5 As String = Md5Base64(bodyJson)
        Dim nonce As String = Guid.NewGuid().ToString("N")
        Dim ts As String = UnixTimeMilliseconds()

        Dim appKeyTrim = APP_KEY.Trim()

        Dim stringToSign =
        "POST" & vbLf &
        accept & vbLf &
        contentMd5 & vbLf &
        contentType & vbLf &
        "userid:" & USER_ID & vbLf &
        "x-ca-key:" & appKeyTrim & vbLf &
        "x-ca-nonce:" & nonce & vbLf &
        "x-ca-timestamp:" & ts & vbLf &
        path

        Dim signature = HmacSha256Base64(APP_SECRET, stringToSign)

        ServicePointManager.ServerCertificateValidationCallback = Function() True

        Dim req = CType(WebRequest.Create(url), HttpWebRequest)
        req.Method = "POST"
        req.Accept = accept
        req.ContentType = contentType

        req.Headers("userid") = USER_ID
        req.Headers("x-ca-key") = appKeyTrim
        req.Headers("x-ca-nonce") = nonce
        req.Headers("x-ca-timestamp") = ts
        req.Headers("x-ca-signature-headers") = "userid,x-ca-key,x-ca-nonce,x-ca-timestamp"
        req.Headers("x-ca-signature") = signature
        req.Headers("Content-MD5") = contentMd5

        Dim bodyBytes = Encoding.UTF8.GetBytes(bodyJson)
        Using s = req.GetRequestStream()
            s.Write(bodyBytes, 0, bodyBytes.Length)
        End Using

        Try
            Using resp = CType(req.GetResponse(), HttpWebResponse)
                Using r = New StreamReader(resp.GetResponseStream())
                    Return r.ReadToEnd()
                End Using
            End Using
        Catch ex As WebException
            ' IMPORTANT: show HikCentral error JSON
            If ex.Response IsNot Nothing Then
                Using errResp = CType(ex.Response, HttpWebResponse)
                    Using r = New StreamReader(errResp.GetResponseStream())
                        Return "HTTP " & CInt(errResp.StatusCode) & vbCrLf & r.ReadToEnd()
                    End Using
                End Using
            End If
            Return ex.ToString()
        End Try
    End Function
    Public Shared Function AddReservation(
                                         visitorName As String,
                                         certificateNo As String,
                                         customId As String,
                                         customFieldName As String)

        Dim startTime As DateTime = DateTime.UtcNow.AddHours(8)
        Dim endTime As DateTime = startTime.AddHours(7)

        Dim appointStartTime As String = startTime.ToString("yyyy-MM-ddTHH:mm:ss") & "+08:00"
        Dim appointEndTime As String = endTime.ToString("yyyy-MM-ddTHH:mm:ss") & "+08:00"

        Dim payload = New With {
            .appointStartTime = appointStartTime,
            .appointEndTime = appointEndTime,
            .visitReasonType = 4,
            .visitReasonDetail = "visitor",
            .visitorInfoList = New Object() {
                New With {
                    .VisitorInfo = New With {
                        .visitorGivenName = visitorName,
                        .visitorFamilyName = visitorName,
                        .visitorGroupName = "Walk-in",
                        .gender = 0,
                        .phoneNo = "09101111111",
                        .certificateType = 111,
                        .certificateNo = certificateNo,
                        .remark = "registered",
                        .accessInfo = New With {
                            .accessLevelList = New Object() {
                                New With {
                                    .accessLevel = New With {
                                        .id = 1
                                    }
                                }
                            }
                        },
                        .customField = New Object() {
                            New With {
                                .customID = customId,
                                .customFieldName = "visitor",
                                .customFieldType = 0,
                                .customFieldValue = customFieldName
                            }
                        }
                    }
                }
            }
        }

        Return CallApi("/artemis/api/visitor/v2/appointment", payload)

    End Function

    Private Shared Function UnixTimeMilliseconds() As String
        Dim epoch As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        Return CLng((DateTime.UtcNow - epoch).TotalMilliseconds).ToString()
    End Function
End Class
