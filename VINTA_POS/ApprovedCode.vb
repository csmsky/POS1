Imports System.Text
Imports System.Data
Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass
Public Class ApprovedCode

    Dim strConn As String = FDEandD()

    Private Sub ButtonCardDetails_Click(sender As Object, e As EventArgs) Handles ButtonCardApprovedCode.Click
        Try
            If TextboxApprovedCode.Text = "0" Or TextboxApprovedCode.Text = "" Or TextboxApprovedCode.Text = "lblApprovedCode" Then
                MsgBox("Please Input Approval Code or Invoice# from Metrobank Terminal!")
            Else
                InsertCardData()
                mainform.Button12.Enabled = True
                Me.Close()
            End If
        Catch

        End Try
    End Sub
    Private Sub TextBoxApprovedCode_TextChanged(sender As Object, e As EventArgs)
        If TextboxApprovedCode.Text = "" Then
            MsgBox("Please Fill-in Approved Code!!")
            ButtonCardApprovedCode.Enabled = False
        Else
            'mainform.lblApprovedCode.Text = TextboxApprovedCode.Text
            ButtonCardApprovedCode.Enabled = True
        End If
    End Sub

    Private Sub InsertCardData()

        Try
            Using conn As New MySqlConnection(strConn)
                conn.Open()

                ' Stored Procedure: insertCardDetails
                Using cmd As New MySqlCommand("insertCardDetails", conn)
                    cmd.CommandType = CommandType.StoredProcedure

                    ' Parameters MUST match your stored procedure parameter names
                    cmd.Parameters.AddWithValue("p_or_no", mainform.TextBoxBarcode.Text)
                    cmd.Parameters.AddWithValue("p_card_number", labelCardNumber.Text.Trim())
                    cmd.Parameters.AddWithValue("p_card_name", labelCardName.Text.Trim())
                    cmd.Parameters.AddWithValue("p_expiry_date", "")
                    cmd.Parameters.AddWithValue("p_approved_code", mainform.lblApprovedCode.Text)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            'MessageBox.Show("Card data inserted successfully.")

        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub TextboxApprovedCode_TextChanged_1(sender As Object, e As EventArgs) Handles TextboxApprovedCode.TextChanged

        mainform.lblApprovedCode.Text = TextboxApprovedCode.Text

        If TextboxApprovedCode.Text = "" Then
            ButtonCardApprovedCode.Enabled = False
        Else
            ButtonCardApprovedCode.Enabled = True
        End If
    End Sub

    Private Sub ApprovedCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ButtonCardApprovedCode.Enabled = False
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
End Class