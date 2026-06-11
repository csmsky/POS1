Imports System.Runtime.InteropServices
Imports System.Text

Public Module ZebraRawPrint

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Private Structure DOC_INFO_1
        <MarshalAs(UnmanagedType.LPStr)> Public pDocName As String
        <MarshalAs(UnmanagedType.LPStr)> Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPStr)> Public pDataType As String
    End Structure

    <DllImport("winspool.Drv", SetLastError:=True, CharSet:=CharSet.Ansi)>
    Private Function OpenPrinter(pPrinterName As String, ByRef phPrinter As IntPtr, pDefault As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True, CharSet:=CharSet.Ansi)>
    Private Function ClosePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True, CharSet:=CharSet.Ansi)>
    Private Function StartDocPrinter(hPrinter As IntPtr, level As Integer, ByRef di As DOC_INFO_1) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Private Function EndDocPrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Private Function StartPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Private Function EndPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", SetLastError:=True)>
    Private Function WritePrinter(hPrinter As IntPtr, pBytes As IntPtr, dwCount As Integer, ByRef dwWritten As Integer) As Boolean
    End Function

    Public Function SendZplToPrinter(printerName As String, zpl As String) As Boolean
        Dim hPrinter As IntPtr = IntPtr.Zero

        If Not OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then
            Return False
        End If

        Try
            Dim di As New DOC_INFO_1 With {
                .pDocName = "ZPL Print",
                .pOutputFile = Nothing,
                .pDataType = "RAW"
            }

            If Not StartDocPrinter(hPrinter, 1, di) Then Return False
            If Not StartPagePrinter(hPrinter) Then Return False

            Dim bytes = Encoding.ASCII.GetBytes(zpl)
            Dim unmanagedBytes As IntPtr = Marshal.AllocHGlobal(bytes.Length)
            Marshal.Copy(bytes, 0, unmanagedBytes, bytes.Length)

            Dim written As Integer = 0
            Dim ok = WritePrinter(hPrinter, unmanagedBytes, bytes.Length, written)

            Marshal.FreeHGlobal(unmanagedBytes)

            EndPagePrinter(hPrinter)
            EndDocPrinter(hPrinter)

            Return ok AndAlso written = bytes.Length
        Finally
            ClosePrinter(hPrinter)
        End Try
    End Function

End Module
