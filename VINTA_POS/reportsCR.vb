Imports WindowsApplication1.ConfigClass
Public Class reportsCR
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps

    Private Sub ButtonShowAll_Click(sender As Object, e As EventArgs) Handles ButtonShowAll.Click
        Dim cmd As MySqlCommand
        Dim adp As New MySqlDataAdapter
        Dim dt As New DataSet
        Dim sql = "Select or_no, payment_date, station_id, pos_id, user_id, base_fare, vat_exempt, vatable, discount, vat, payment, void_status from or_tbl"
        Try
            conn.Open()
            cmd = New MySqlCommand(sql, conn)
            adp.SelectCommand = cmd
            adp.Fill(dt, "or_tbl")
            'Dim report As New CrystalReportOR
            'report.SetDataSource(dt)
            'CrystalReportViewer1.ReportSource = report
            CrystalReportViewer1.Refresh()
            cmd.Dispose()
            adp.Dispose()
            dt.Dispose()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Dim cmd As MySqlCommand
        Dim adp As New MySqlDataAdapter
        Dim dt As New DataSet
        Dim sql = "Select or_no, payment_date, station_id, pos_id, user_id, base_fare, vat_exempt, vatable, discount, vat, payment, void_status from or_tbl WHERE payment_date BETWEEN '" & DateTimePicker1.Text & "' AND '" & DateTimePicker2.Text & "'"
        Try
            conn.Open()
            cmd = New MySqlCommand(sql, conn)
            adp.SelectCommand = cmd
            adp.Fill(dt, "or_tbl")
            'Dim report As New CrystalReportOR
            'report.SetDataSource(dt)
            'CrystalReportViewer1.ReportSource = report
            CrystalReportViewer1.Refresh()
            cmd.Dispose()
            adp.Dispose()
            dt.Dispose()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub ButtonShowAll2_Click(sender As Object, e As EventArgs) Handles ButtonShowAll2.Click
        Dim cmd As MySqlCommand
        Dim adp As New MySqlDataAdapter
        Dim dt As New DataSet
        Dim sql = "Select pos_id, cashier_name, station, payment_date, payment_time, or_no, passenger_name, vessel_id, schedule, seat_no, origin, destination, trip_fare, total_amount, cash, change_amount, vatable, vat_exempt, zero_rated, vat from ejournal_tbl"
        Try
            conn.Open()
            cmd = New MySqlCommand(sql, conn)
            adp.SelectCommand = cmd
            adp.Fill(dt, "ejournal_tbl")
            'Dim report As New CrystalReportEJ
            'report.SetDataSource(dt)
            'CrystalReportViewer2.ReportSource = report
            CrystalReportViewer2.Refresh()
            cmd.Dispose()
            adp.Dispose()
            dt.Dispose()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub ButtonSearch2_Click(sender As Object, e As EventArgs) Handles ButtonSearch2.Click
        Dim cmd As MySqlCommand
        Dim adp As New MySqlDataAdapter
        Dim dt As New DataSet
        Dim sql = "Select pos_id, cashier_name, station, payment_date, payment_time, or_no, passenger_name, vessel_id, schedule, seat_no, origin, destination, trip_fare, total_amount, cash, change_amount, vatable, vat_exempt, zero_rated, vat from ejournal_tbl WHERE payment_date BETWEEN '" & DateTimePickerStart2.Text & "' AND '" & DateTimePickerEnd2.Text & "'"
        Try
            conn.Open()
            cmd = New MySqlCommand(sql, conn)
            adp.SelectCommand = cmd
            adp.Fill(dt, "ejournal_tbl")
            'Dim report As New CrystalReportEJ
            'report.SetDataSource(dt)
            'CrystalReportViewer2.ReportSource = report
            CrystalReportViewer2.Refresh()
            cmd.Dispose()
            adp.Dispose()
            dt.Dispose()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try
        conn.Close()
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
        management.Show()
    End Sub

    Private Sub reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA
        LabelPOSno.Text = pos

        LabelUserID.Hide()
    End Sub
End Class