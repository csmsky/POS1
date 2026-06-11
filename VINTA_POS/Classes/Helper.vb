Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography

Public Class Helper
    Public Shared Function getBytes(ByVal stringBytes As String, ByVal delimeter As Char) As Byte()
        Dim arrayString() As String = stringBytes.Split(delimeter)
        Dim bytesResult() As Byte = New Byte((arrayString.Length) - 1) {}
        Dim tmpByte As Byte
        Dim counter As Integer = 0
        For Each str As String In arrayString
            If Byte.TryParse(str, System.Globalization.NumberStyles.HexNumber, Nothing, tmpByte) Then
                bytesResult(counter) = tmpByte
                counter = (counter + 1)
            Else
                Return Nothing
            End If
        Next
        Return bytesResult
    End Function

    Public Shared Function getBytes(ByVal stringBytes As String) As Byte()
        Dim fString As String = ""
        Dim counter As Integer = 0

        If stringBytes.Trim() = "" Then
            Return Nothing
        End If

        For i As Integer = 0 To stringBytes.Length - 1
            If stringBytes(i) = " "c Then
                Continue For
            End If

            If (counter > 0) Then
                If (counter Mod 2 = 0) Then
                    fString = (fString + " ")
                End If
            End If

            fString = (fString + stringBytes(i).ToString)
            counter = (counter + 1)
            counter += 1
        Next
        Return getBytes(fString, " "c)
    End Function

    Public Shared Function stringToByteArray(ByVal stringByte As String) As Byte()
        Dim tmpArray As Byte() = New Byte(stringByte.Length - 1) {}
        Dim str As String = ""

        For indx As Integer = 0 To stringByte.Length - 1
            ' Left half of Debit key
            str = stringByte.Substring(indx, 1)

            If str.Length = 1 Then
                Dim asciiEncoding As New System.Text.ASCIIEncoding()
                Dim intAsciiCode As Integer = CInt(asciiEncoding.GetBytes(str)(0))


                tmpArray(indx) = CByte(intAsciiCode)
            Else
                Throw New Exception("Character is not valid.")
            End If
        Next

        Return tmpArray
    End Function

    Public Shared Function byteToInt(ByVal data() As Byte, ByVal isLittleEndian As Boolean) As Int32
        Dim tmpArry As Byte() = New Byte((data.Length) - 1) {}
        Array.Copy(data, tmpArry, tmpArry.Length)
        If (tmpArry.Length <> 4) Then
            If isLittleEndian Then
                Array.Resize(tmpArry, 4)
            Else
                Array.Reverse(tmpArry)
                Array.Resize(tmpArry, 4)
                Array.Reverse(tmpArry)
            End If
        End If
        If isLittleEndian Then
            Return (tmpArry(3) << 24) + (tmpArry(2) << 16) + (tmpArry(1) << 8) + tmpArry(0)
        Else
            Return (tmpArry(3) + (tmpArry(2) * 256) + (tmpArry(1) * 256 * 256) + (tmpArry(0) * 256 * 256 * 256))
        End If
    End Function

    Public Shared Function byteToInt(ByVal data() As Byte) As Integer
        Return byteToInt(data, False)
    End Function

    Public Shared Function intToByte(ByVal nummber As Integer) As Byte()
        Dim tmpByte() As Byte = New Byte((4) - 1) {}

        tmpByte(0) = CByte((nummber >> 24) And &HFF)
        tmpByte(1) = CByte((nummber >> 16) And &HFF)
        tmpByte(2) = CByte((nummber >> 8) And &HFF)
        tmpByte(3) = CByte(nummber And &HFF)

        Return tmpByte
    End Function

    Public Shared Function intToByte(ByVal number As UInt32) As Byte()
        Dim tmpByte() As Byte = New Byte((3) - 1) {}

        tmpByte(0) = CByte((number >> 24) And &HFF)
        tmpByte(1) = CByte((number >> 16) And &HFF)
        tmpByte(2) = CByte((number >> 8) And &HFF)
        tmpByte(3) = CByte(number And &HFF)

        Return tmpByte
    End Function

    Public Shared Function byteAsString(ByVal bytes() As Byte, ByVal startIndex As Integer, ByVal length As Integer, ByVal spaceInBetween As Boolean) As String
        Dim newByte() As Byte

        If (bytes.Length < (startIndex + length)) Then
            Array.Resize(bytes, (startIndex + length))
        End If

        newByte = New Byte((length) - 1) {}
        Array.Copy(bytes, startIndex, newByte, 0, length)

        Return byteAsString(newByte, spaceInBetween)
    End Function

    Public Shared Function byteAsString(ByVal tmpbytes() As Byte, ByVal spaceInBetween As Boolean) As String
        Dim tmpStr As String = String.Empty
        If (tmpbytes Is Nothing) Then
            Return ""
        End If
        Dim i As Integer = 0
        Do While (i < tmpbytes.Length)
            tmpStr = (tmpStr + String.Format("{0:X2}", tmpbytes(i)))
            If spaceInBetween Then
                tmpStr = (tmpStr + " ")
            End If
            i = (i + 1)
        Loop
        Return tmpStr
    End Function

    Public Shared Function byteArrayIsEqual(ByVal array1() As Byte, ByVal array2() As Byte, ByVal lenght As Integer) As Boolean
        If (array1.Length < lenght) Then
            Return False
        End If
        If (array2.Length < lenght) Then
            Return False
        End If
        Dim i As Integer = 0
        Do While (i < lenght)
            If (array1(i) <> array2(i)) Then
                Return False
            End If
            i = (i + 1)
        Loop
        Return True
    End Function

    Public Shared Function byteArrayIsEqual(ByVal array1() As Byte, ByVal array2() As Byte) As Boolean
        Return byteArrayIsEqual(array1, array2, array2.Length)
    End Function

    Public Shared Function appendArrays(ByVal array1() As Byte, ByVal array2() As Byte) As Byte()
        Dim c() As Byte = New Byte((array1.Length + array2.Length) - 1) {}
        Buffer.BlockCopy(array1, 0, c, 0, array1.Length)
        Buffer.BlockCopy(array2, 0, c, array1.Length, array2.Length)
        Return c
    End Function

    Public Shared Function appendArrays(ByVal array1() As Byte, ByVal array2 As Byte) As Byte()
        Dim c() As Byte = New Byte((1 + array1.Length) - 1) {}
        Buffer.BlockCopy(array1, 0, c, 0, array1.Length)
        c(array1.Length) = array2
        Return c
    End Function
End Class
