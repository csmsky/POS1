Imports WindowsApplication1.ConfigClass
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports MySql.Data.MySqlClient
Imports System.IO.Ports

Public Class ListOfReport

    Private WithEvents PrintDocumentCashINOUT As New Printing.PrintDocument
    Dim strConn As String = FDEandD()

    Private _fpAuth As FingerprintManagerAuth
    Private _fpCts As CancellationTokenSource

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cashierreport.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ButtonCashFloat_Click(sender As Object, e As EventArgs) Handles ButtonCashFloat.Click
        Try
            ButtonCashFloat.BackColor = Color.Cyan
            ButtonCashdrop.BackColor = Color.Gainsboro

            ComboBoxReason.Items.Clear()
            ComboBoxReason.Items.Add("Opening cash float")
            ComboBoxReason.Items.Add("Additional cash float")
            ComboBoxReason.Items.Add("Change replenishment")
            ComboBoxReason.Items.Add("Emergency cash float")
            ComboBoxReason.Items.Add("System offline")
            ComboBoxReason.Items.Add("Adjustment")
            ComboBoxReason.Items.Add("End-day reconciliation")
            ComboBoxReason.SelectedIndex = 0
        Catch
        End Try
    End Sub

    Private Sub ButtonCashdrop_Click(sender As Object, e As EventArgs) Handles ButtonCashdrop.Click
        Try
            ButtonCashdrop.BackColor = Color.Cyan
            ButtonCashFloat.BackColor = Color.Gainsboro

            ComboBoxReason.Items.Clear()
            ComboBoxReason.Items.Add("Scheduled cash drop")
            ComboBoxReason.Items.Add("Excess cash drop")
            ComboBoxReason.Items.Add("End-of-Day cash drop")
            ComboBoxReason.Items.Add("security cash drop")
            ComboBoxReason.Items.Add("Cash drawer limit drop")
            ComboBoxReason.Items.Add("Change fund preservation drop")
            ComboBoxReason.Items.Add("Reconciliation cash drop")
            ComboBoxReason.Items.Add("Safe drop/Vault drop")
            ComboBoxReason.Items.Add("Manual/Manager request cash drop")
            ComboBoxReason.SelectedIndex = 0
        Catch
        End Try
    End Sub

    Private Function GetNextSeqSafe(conn As MySqlConnection, seqKey As String) As Integer
        ' Read the new value
        Using cmdRead As New MySqlCommand(
        "SELECT reference_id FROM audit_trail_tbl WHERE seq_key = @k;", conn)
            cmdRead.Parameters.AddWithValue("@k", seqKey)
            Return Convert.ToInt32(cmdRead.ExecuteScalar())
        End Using
    End Function
    Private Sub ButtonPrintCashFloatDrop_Click(sender As Object, e As EventArgs) Handles ButtonPrintCashFloatDrop.Click
        Try

            If ButtonCashFloat.BackColor = Color.Cyan Then

                Using conn As New MySqlConnection(strConn)
                    Using cmd As New MySqlCommand("insertCashFloat", conn)

                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Clear()

                        cmd.Parameters.AddWithValue("p_date_cashfloat", Date.Today)
                        cmd.Parameters.AddWithValue("p_time_cashfloat", TimeOfDay)
                        cmd.Parameters.AddWithValue("p_pos_id", CInt(mainform.LabelPOSno.Text))
                        cmd.Parameters.AddWithValue("p_cashier_id", mainform.LabelCashierID.Text)
                        cmd.Parameters.AddWithValue("p_cash_float", CDec(TextboxCash.Text))
                        cmd.Parameters.AddWithValue("p_approvedby", TextBoxApprovedBy.Text)
                        cmd.Parameters.AddWithValue("p_reason", ComboBoxReason.Text)

                        cmd.Parameters.Add("p_message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output

                        conn.Open()
                        cmd.ExecuteNonQuery()

                        Dim resultMsg As String = cmd.Parameters("p_message").Value.ToString()
                        MsgBox(resultMsg, MsgBoxStyle.Information, "Cash Float")
                    End Using
                End Using

            ElseIf ButtonCashdrop.BackColor = Color.Cyan Then

                Using conn As New MySqlConnection(strConn)
                    Using cmd As New MySqlCommand("insertCashDrop", conn)

                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Clear()

                        cmd.Parameters.AddWithValue("p_date_cashdrop", Date.Today)
                        cmd.Parameters.AddWithValue("p_time_cashdrop", TimeOfDay)
                        cmd.Parameters.AddWithValue("p_pos_id", CInt(mainform.LabelPOSno.Text))
                        cmd.Parameters.AddWithValue("p_cashier_id", mainform.LabelCashierID.Text)
                        cmd.Parameters.AddWithValue("p_cash_drop", CDec(TextboxCash.Text))
                        cmd.Parameters.AddWithValue("p_approvedby", TextBoxApprovedBy.Text)
                        cmd.Parameters.AddWithValue("p_reason", ComboBoxReason.Text)

                        cmd.Parameters.Add("p_message", MySqlDbType.VarChar, 255).Direction = ParameterDirection.Output

                        conn.Open()
                        cmd.ExecuteNonQuery()

                        Dim resultMsg As String = cmd.Parameters("p_message").Value.ToString()
                        MsgBox(resultMsg, MsgBoxStyle.Information, "Cash Drop")
                    End Using
                End Using

            End If

            With PrintDocumentCashINOUT.DefaultPageSettings
                .PaperSize = New Printing.PaperSize("Receipt", 300, 250)
                .Margins = New Printing.Margins(5, 5, 5, 5)
            End With

            ' Set printer name dynamically here (or you can hardcode it)
            PrintDocumentCashINOUT.PrinterSettings.PrinterName = "Posiflex PP9000 Printer"  ' Replace with your printer name

            ' Check if the printer is valid
            If Not PrintDocumentCashINOUT.PrinterSettings.IsValid Then
                MsgBox("Printer is not valid!")
                Return
            End If
            'PrintPreviewDialogCashINOUT.Document = PrintDocumentCashINOUT
            'PrintPreviewDialogCashINOUT.ShowDialog()

            PrintDocumentCashINOUT.Print()
            OpenTerminalDrawer()

            LabelCashierID_Request.Text = "Cashier_ID"
            TextboxCash.Text = ""
            ComboBoxReason.Text = ""
            TextBoxApprovedBy.Text = ""
            ButtonCashdrop.BackColor = Color.Gainsboro
            ButtonCashFloat.BackColor = Color.Gainsboro

        Catch ex As Exception
            MsgBox("Please.. Enter Amount & Reason", MsgBoxStyle.Exclamation)
        End Try
    End Sub

    ' ===========================
    ' LOAD + FP AUTH LOOP
    ' ===========================
    Private Async Sub ListOfReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LabelCashierID_Request.Text = mainform.LabelCashierName.Text

        Button3.Visible = False
        btnCancelFp.Visible = False
        btnCancelFp.Enabled = True

        ButtonPrintCashFloatDrop.Enabled = False
        TextBoxApprovedBy.Clear()

        _fpAuth = New FingerprintManagerAuth(strConn, 30000)

        ' IMPORTANT: use AddressOf to avoid lambda parsing issues
        AddHandler _fpAuth.StatusChanged, AddressOf OnFpStatusChanged

        _fpCts = New CancellationTokenSource()
        Await StartManagerScanLoopAsync()

    End Sub

    Private Sub OnFpStatusChanged(msg As String)
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New Action(Sub() SetFpStatus(msg)))
        End If
    End Sub

    Private Async Function StartManagerScanLoopAsync() As Task
        While _fpCts IsNot Nothing AndAlso Not _fpCts.IsCancellationRequested AndAlso Not Me.IsDisposed

            Try
                SetFpStatus("Place MANAGER finger to authorize...")

                Dim managerId As String = Await _fpAuth.AuthorizeOnceAsync(_fpCts.Token)

                If Not String.IsNullOrWhiteSpace(managerId) Then
                    If Me.IsHandleCreated Then
                        Me.BeginInvoke(New Action(Sub()
                                                      TextBoxApprovedBy.Text = managerId
                                                  End Sub))
                    End If
                    Exit While
                End If

            Catch ex As TaskCanceledException
                Exit While
            Catch ex As Exception
                SetFpStatus("FP Error: " & ex.Message)
            End Try

            Await Task.Delay(500)
        End While
    End Function

    ' ===========================
    ' PRINT PAGE
    ' ===========================
    Private Sub PrintDocumentCashINOUT_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocumentCashINOUT.PrintPage

        Dim cashAmount As Decimal = 0D
        Decimal.TryParse(TextboxCash.Text, cashAmount)

        Dim fHeader As New Font("Merchant Copy", 13, FontStyle.Bold)
        Dim fHeader2 As New Font("Merchant Copy", 11.5, FontStyle.Bold)
        Dim fBodyB As New Font("Merchant Copy", 10, FontStyle.Bold)
        Dim fBody As New Font("Merchant Copy", 10)
        Dim y As Integer = 10

        ' Set printer name dynamically here (or you can hardcode it)
        PrintDocumentCashINOUT.PrinterSettings.PrinterName = "Posiflex PP9000 Printer"  ' Replace with your printer name

        ' Check if the printer is valid
        If Not PrintDocumentCashINOUT.PrinterSettings.IsValid Then
            MsgBox("Printer is not valid!")
            Return
        End If

        DrawCenteredText(e.Graphics, "METTA CITY", fHeader, y, e.PageBounds.Width)
        y += 40

        If ButtonCashFloat.BackColor = Color.Cyan Then
            DrawCenteredText(e.Graphics, "CASH IN RECEIPT", fHeader2, y, e.PageBounds.Width)
            y += 30
        ElseIf ButtonCashdrop.BackColor = Color.Cyan Then ' ✅ FIX: ButtonCashdrop (not ButtonCashDrop)
            DrawCenteredText(e.Graphics, "CASH OUT RECEIPT", fHeader2, y, e.PageBounds.Width)
            y += 30
        End If

        e.Graphics.DrawString("Date : " & Date.Today.ToString("yyyy-MM-dd"), fBody, Brushes.Black, 10, y)
        y += 15
        e.Graphics.DrawString("Time : " & DateTime.Now.ToString("HH:mm:ss"), fBody, Brushes.Black, 10, y)
        y += 35

        e.Graphics.DrawString("POS_ID : " & mainform.LabelPOSno.Text, fBodyB, Brushes.Black, 10, y)
        y += 15

        e.Graphics.DrawString("Cashier Name : " & mainform.LabelCashierName.Text, fBodyB, Brushes.Black, 10, y)
        y += 35

        e.Graphics.DrawString("Amount  : ₱ " & cashAmount.ToString("N2"), fBodyB, Brushes.Black, 10, y)
        y += 15

        e.Graphics.DrawString("Reason  : " & ComboBoxReason.Text, fBody, Brushes.Black, 10, y)
        y += 35

        e.Graphics.DrawString("Approved By : " & TextBoxApprovedBy.Text, fBody, Brushes.Black, 10, y)
        y += 50

        DrawCenteredText(e.Graphics, "=====================", fHeader, y, e.PageBounds.Width)
        y += 50

        e.Graphics.DrawString("Signature (Cashier): ____________", fBody, Brushes.Black, 10, y)
        y += 30
        e.Graphics.DrawString("Signature (Manager): ____________", fBody, Brushes.Black, 10, y)

    End Sub

    Private Sub DrawCenteredText(g As Graphics, text As String, font As Font, y As Integer, pageWidth As Integer)
        Dim textSize As SizeF = g.MeasureString(text, font)
        Dim x As Single = (pageWidth - textSize.Width) / 2
        g.DrawString(text, font, Brushes.Black, x, y)
    End Sub

    ' ===========================
    ' FP STATUS + CANCEL + CLOSE
    ' ===========================
    Private Sub SetFpStatus(msg As String)
        Try
            If lblFpStatus Is Nothing Then Return
            lblFpStatus.Text = msg
            lblFpStatus.Refresh()
        Catch
        End Try
    End Sub

    Private Sub btnCancelFp_Click(sender As Object, e As EventArgs) Handles btnCancelFp.Click
        Try
            If _fpCts IsNot Nothing Then _fpCts.Cancel()
            If _fpAuth IsNot Nothing Then _fpAuth.CancelAndCleanup()
            SetFpStatus("Cancelling...")
        Catch
        End Try
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Try
            If _fpCts IsNot Nothing Then _fpCts.Cancel()
            If _fpAuth IsNot Nothing Then _fpAuth.CancelAndCleanup()
        Catch
        End Try

        Me.Close()
        mainform.Show()
    End Sub

    Private Sub TextBoxApprovedBy_TextChanged(sender As Object, e As EventArgs) Handles TextBoxApprovedBy.TextChanged
        If String.IsNullOrWhiteSpace(TextBoxApprovedBy.Text) Then
            ButtonPrintCashFloatDrop.Enabled = False
            Return
        End If

        Try
            Using c As New MySqlConnection(strConn)
                c.Open()

                Dim sql As String =
                    "SELECT COUNT(*) FROM user_registration_tbl " &
                    "WHERE user_id=@id AND CAST(AES_DECRYPT(position,'strdjnltmyp') AS CHAR(50))='Manager'"

                Using cmd As New MySqlCommand(sql, c)
                    cmd.Parameters.AddWithValue("@id", TextBoxApprovedBy.Text.Trim())
                    Dim n As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    ButtonPrintCashFloatDrop.Enabled = (n > 0)
                End Using
            End Using

            If ButtonPrintCashFloatDrop.Enabled Then
                SetFpStatus("Manager authorized ✅ You may now print.")
            Else
                SetFpStatus("Matched user is not Manager.")
            End If

        Catch ex As Exception
            ButtonPrintCashFloatDrop.Enabled = False
            SetFpStatus("DB Error: " & ex.Message)
        End Try
    End Sub

    'OPENING CASH DRAWER
    Private Sub OpenTerminalDrawer()
        Dim drawerPortName As String = "COM1"

        Using sp As New SerialPort("COM1", 9600, Parity.None, 8, StopBits.One)
            Try
                sp.Open()

                ' \007 is ASCII 7 (The "Bell" character)
                ' We send it as a Byte array to ensure it is transmitted purely
                Dim triggerCode As Byte() = {7}

                ' Write the byte to the port
                sp.Write(triggerCode, 0, 1)

                ' Close immediately
                sp.Close()

            Catch ex As UnauthorizedAccessException
                ' CRITICAL: This error means the Posiflex Demo (or another app) is still open.
                MessageBox.Show("Port COM1 is busy!")
            Catch ex As Exception
                'MessageBox.Show("Drawer Error: " & ex.Message)
                MessageBox.Show("Error Opening Cash Drawer, Kindly manually use Key!")
            End Try
        End Using
    End Sub
End Class
