Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text
Imports System.Drawing.Printing
Imports WindowsApplication1.ConfigClass

Public Class ModifyTextFile
    ' Your MySQL connection string
    Dim strConn As String = FDEandD()

    ' Method to find the text file, modify the content, and update it in the database
    Public Sub UpdateTextFile(or_no As String, saleInvoice As String, voidInvoice As String)
        ' Step 1: Find the text file based on or_no in the database
        Dim query As String = "SELECT text_file FROM e_journal_tbl WHERE or_no = @orNo"
        Dim conn As New MySqlConnection(strConn)
        Dim cmd As New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@orNo", or_no)

        Try
            conn.Open()

            ' Properly declare the DataReader
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()  ' Read the first (and ideally, only) row
                ' Convert the BLOB data to a string
                Dim fileContent As String = System.Text.Encoding.UTF8.GetString(CType(reader("text_file"), Byte()))

                ' Step 2: Modify the content (replace old text with new text)
                Dim sb As New StringBuilder()
                Dim lines As String() = fileContent.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

                ' Modify each line by replacing old text with new text
                For Each line As String In lines
                    sb.AppendLine(line.Replace(saleInvoice, voidInvoice))
                Next

                ' Step 3: Close the reader before executing the update query
                reader.Close()  ' Close the reader before using the same connection again

                ' Convert the modified content back to a byte array
                Dim newContentBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(sb.ToString())

                ' Step 4: Update the modified content back to the database
                Dim updateQuery As String = "UPDATE e_journal_tbl SET text_file = @newContent WHERE or_no = @orNo"
                Dim updateCmd As New MySqlCommand(updateQuery, conn)
                updateCmd.Parameters.AddWithValue("@newContent", newContentBytes)
                updateCmd.Parameters.AddWithValue("@orNo", or_no)
                updateCmd.ExecuteNonQuery()

                MsgBox("Text file updated successfully.")
            Else
                MsgBox("No record found for the given or_no.")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
End Class
