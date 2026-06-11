Imports WindowsApplication1.ConfigClass
Imports System.IO
Imports System.IO.Ports
' ===== Fingerprint =====
Imports DPUruNet
Imports DPUruNet.Constants
Public Class EnableManagerActivity
    Dim strConn As String = FDEandD()

    'Fingerprint Reader   
    Private _fpAuth As FingerprintManagerAuth
    Private _fpCts As Threading.CancellationTokenSource

    ' ===========================
    ' UI STATUS HELPER (optional label: lblFpStatus)
    ' ===========================
    Private Sub SetFpStatus(msg As String)
        Try
            If lblFpStatus Is Nothing Then Return
            lblFpStatus.Text = msg
            lblFpStatus.Refresh()
        Catch
        End Try
    End Sub

    Private Async Sub EnableManagerActivity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnCancelFp.Hide()
            btnCancelFp.Visible = False

            disabledFunction()

            _fpAuth = New FingerprintManagerAuth(strConn, 30000)
            '_fpAuth = New FingerprintManagerAuth(strConn)

            AddHandler _fpAuth.StatusChanged,
            Sub(msg)
                If Me.IsHandleCreated Then
                    Me.BeginInvoke(New Action(Sub() SetFpStatus(msg)))
                End If
            End Sub

            ' Start scanning until manager is detected
            _fpCts = New Threading.CancellationTokenSource()

            Await StartManagerScanLoopAsync()   ' ✅ now this works
        Catch
            MsgBox("Error Initializing Manager Credentials")
        End Try
    End Sub

    Private Sub btnMAVoidItem_Click(sender As Object, e As EventArgs) Handles btnMAVoidItem.Click

        btnMAVoidItem.BackColor = Color.Cyan
        Button1.BackColor = Color.Gainsboro

        mainform.ButtonVoidItem.Enabled = True
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            btnMAVoidItem.BackColor = Color.Gainsboro
            Button1.BackColor = Color.Cyan

            mainform.ButtonStudent.Enabled = True
            mainform.ButtonSenior.Enabled = True
            mainform.ButtonSoloParent.Enabled = True
            mainform.ButtonPWD.Enabled = True
            mainform.ButtonNAAC.Enabled = True
            mainform.ButtonDiplomat.Enabled = True
            mainform.ButtonZeroRated.Enabled = True
            mainform.ButtonEmployee.Enabled = True
            mainform.ButtonCustInfo.Enabled = True
            mainform.CheckBoxPercentDiscount.Enabled = True

            ' audit_trail_tbl
            'Dim BranchCode As String = "BR01"
            Dim DateNow = DateTime.Now.ToString("yyyyMMdd")

            Using conn As New MySqlConnection(strConn)
                conn.Open()

                Using cmd As New MySqlCommand("insertingAuditTrail", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("p_pos_id", mainform.LabelPOSno.Text)
                    cmd.Parameters.AddWithValue("p_userid", mainform.LabelCashierID.Text)
                    cmd.Parameters.AddWithValue("p_username", mainform.LabelCashierName.Text)
                    cmd.Parameters.AddWithValue("p_approvedby", managerUserID.Text)
                    cmd.Parameters.AddWithValue("p_activity_performed", TypeTransaction)
                    cmd.Parameters.AddWithValue("p_module", "Sales")
                    cmd.Parameters.AddWithValue("p_reference_id", BranchCode & "-" & DateNow & "DC-Apply Discount")
                    cmd.Parameters.AddWithValue("p_remarks", "New Sales")
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Me.Close()
        Catch
            MsgBox("Didn't Record the Activity!")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        btnMAVoidItem.BackColor = Color.Gainsboro
        Button1.BackColor = Color.Gainsboro

    End Sub

    Private Async Function StartManagerScanLoopAsync() As Task
        Try
            While Not _fpCts.IsCancellationRequested AndAlso Not Me.IsDisposed
                Try
                    SetFpStatus("Place MANAGER finger to authorize REPRINT...")

                    Dim managerId As String = Await _fpAuth.AuthorizeOnceAsync(_fpCts.Token)

                    If Not String.IsNullOrWhiteSpace(managerId) Then
                        If Me.IsHandleCreated Then
                            Me.BeginInvoke(New Action(Sub() managerUserID.Text = managerId))
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
        Catch
            MsgBox("Error Scanning of Fingerprint")
        End Try
    End Function

    Private Sub btnCancelFp_Click(sender As Object, e As EventArgs) Handles btnCancelFp.Click
        Try
            If _fpCts IsNot Nothing Then _fpCts.Cancel()
            If _fpAuth IsNot Nothing Then _fpAuth.CancelAndCleanup()
            SetFpStatus("Cancelling...")
        Catch
        End Try
    End Sub

    Private Sub managerUserID_TextChanged(sender As Object, e As EventArgs) Handles managerUserID.TextChanged

        Try
                Using c As New MySqlConnection(strConn)
                    c.Open()

                    Dim sql As String =
                    "SELECT COUNT(*) FROM user_registration_tbl " &
                    "WHERE user_id=@id AND CAST(AES_DECRYPT(position,'strdjnltmyp') AS CHAR(50))='Manager'"

                    Using cmd As New MySqlCommand(sql, c)
                        cmd.Parameters.AddWithValue("@id", managerUserID.Text.Trim())
                        Dim n As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                        btnMAVoidItem.Enabled = (n > 0)
                    End Using
                End Using

                If btnMAVoidItem.Enabled Then
                    Button1.Enabled = True
                    Button2.Enabled = True
                    Button3.Enabled = True
                    Button4.Enabled = True
                    Button5.Enabled = True
                    Button6.Enabled = True
                    Button7.Enabled = True
                    SetFpStatus("Manager authorized ✅ You may now reprint.")
                Else
                    SetFpStatus("Matched user is not Manager.")
                End If

            Catch ex As Exception
                btnMAVoidItem.Enabled = False
                Button1.Enabled = False
                Button2.Enabled = False
                Button3.Enabled = False
                Button4.Enabled = False
                Button5.Enabled = False
                Button6.Enabled = False
                Button7.Enabled = False
                ' SetFpStatus("DB Error: " & ex.Message)
            End Try

        'Try
        'If managerUserID.Text = "" Then
        'btnMAVoidItem.Enabled = False
        'Button1.Enabled = False
        'Button2.Enabled = False
        'Button3.Enabled = False
        'Button4.Enabled = False
        'Button5.Enabled = False
        'Button6.Enabled = False
        'Button7.Enabled = False
        'Else
        'btnMAVoidItem.Enabled = True
        'Button1.Enabled = True
        'Button2.Enabled = True
        'Button3.Enabled = True
        'Button4.Enabled = True
        'Button5.Enabled = True
        'Button6.Enabled = True
        'Button7.Enabled = True

        'btnMAVoidItem.BackColor = Color.Gainsboro
        'Button1.BackColor = Color.Gainsboro
        'Button2.BackColor = Color.Gainsboro
        'Button3.BackColor = Color.Gainsboro
        'Button4.BackColor = Color.Gainsboro
        'Button5.BackColor = Color.Gainsboro
        'Button6.BackColor = Color.Gainsboro
        'Button7.BackColor = Color.Gainsboro
        'End If
        'Catch
        'MsgBox("Input Manager ID")
        'End Try
        ' End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        mainform.ButtonPromo.Enabled = True

        Button2.BackColor = Color.Cyan
        Button3.BackColor = Color.Gainsboro

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        mainform.ButtonPromo.Enabled = False
        Button3.BackColor = Color.Cyan
        Button2.BackColor = Color.Gainsboro
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        mainform.ButtonRegularRide.Enabled = True
        Button4.BackColor = Color.Cyan
        Button5.BackColor = Color.Gainsboro
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        mainform.ButtonRegularRide.Enabled = False
        Button5.BackColor = Color.Cyan
        Button4.BackColor = Color.Gainsboro
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        OpenTerminalDrawer()
        Me.Close()
    End Sub

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

    Private Sub CopyListviewCustomerView()

        secondscreen.ListViewCustomerView.View = View.Details

        Dim item As New ListViewItem(mainform.TextBoxQty.Text)
        item.SubItems.Add(mainform.LabelAvailed.Text)
        item.SubItems.Add(mainform.TextBoxTripFare.Text)
        item.SubItems.Add(mainform.LabelDiscount.Text)
        item.SubItems.Add(mainform.LabelTotal.Text)

        secondscreen.ListViewCustomerView.Items.Add(item)
    End Sub

    Private Sub disabledFunction()
        btnMAVoidItem.Enabled = False
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = False
    End Sub
End Class