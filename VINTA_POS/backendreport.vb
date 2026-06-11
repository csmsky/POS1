Imports WindowsApplication1.ConfigClass
Imports Microsoft.Reporting.WinForms
Public Class backendreport
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps
    Dim station = AppConfigReader.sttn

    Private Sub backendreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ' make TableAdapter use your runtime connection string
            Me.accumulated_amount_tblTableAdapter.Connection = New MySqlConnection(strConn)

            Me.accumulated_amount_tblTableAdapter.Fill(Me.accumulated_amount_tblDataSet.accumulated_amount_tbl)

            PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA
            LabelPOSno.Text = pos
            LabelStationID.Text = station
            LabelUserID.Hide()

            'display current datetime in datetimepicker
            DateTimePicker1Sales.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            DateTimePicker2Sales.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show("Cannot connect to MySQL: " & ex.Message,
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' optional: continue loading form with empty dataset
        End Try

        'Original Code 
        'TODO: This line of code loads data into the 'accumulated_amount_tblDataSet.accumulated_amount_tbl' table. You can move, or remove it, as needed.
        'Me.accumulated_amount_tblTableAdapter.Fill(Me.accumulated_amount_tblDataSet.accumulated_amount_tbl)

        'Not included in Original Code
        'TODO: This line of code loads data into the 'accumulated_amount_tblDataSet.accumulated_amount_tbl' table. You can move, or remove it, as needed.
        ''Me.accumulated_amount_tblTableAdapter.Fill(Me.accumulated_amount_tblDataSet.accumulated_amount_tbl)
        ''for taking the company logo from resources
        ''PictureBoxCompanyLogo.Image = WindowsApplication1.My.Resources.Logo

        'Original Code
        'PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA
        'LabelPOSno.Text = pos
        'LabelStationID.Text = station
        'LabelUserID.Hide()

        'display current datetime in datetimepicker
        'DateTimePicker1Sales.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        'DateTimePicker2Sales.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

    End Sub

    Private Sub ButtonShowAll_Click(sender As Object, e As EventArgs) Handles ButtonShowAll.Click
        'show all transactions made
        Try
            Dim salessummary As String = "SELECT DISTINCT payment_date, 
                                            MIN(begin_or_no) AS Expr1, 
                                            MAX(end_or_no) AS Expr2, 
                                            SUM(no_of_transaction) AS Expr3, 
                                            SUM(sales) AS Expr4, SUM(discount) AS Expr5, 
                                            SUM(vatable) AS Expr6, SUM(vat) AS Expr7, 
                                            SUM(vat_exempt) AS Expr8, 
                                            SUM(zero_rated) AS Expr9, 
                                            SUM(no_of_void) AS Expr10, 
                                            IFNULL(MIN(NULLIF(begin_void_no, 0)), 0) AS Expr11, 
                                            MAX(end_void_no) AS Expr12, 
                                            SUM(void) AS Expr13, 
                                            SUM(grand_total) AS Expr14
                                            FROM accumulated_amount_tbl
                                            GROUP BY payment_date"
            Dim adp As New MySqlDataAdapter(salessummary, conn)
            Dim accumulated_amount_tblDataSet As New DataSet
            adp.Fill(accumulated_amount_tblDataSet, "accumulated_amount_tblDataSet")
            Me.ReportViewerBackEnd.LocalReport.ReportPath = ""
            Me.ReportViewerBackEnd.LocalReport.DataSources.Clear()
            Me.ReportViewerBackEnd.LocalReport.DataSources.Add(New ReportDataSource("accumulated_amount_tblDataSet", accumulated_amount_tblDataSet.Tables("accumulated_amount_tblDataSet")))
            Me.ReportViewerBackEnd.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        'show filtered transactions made
        Try
            Dim salessummary As String = "SELECT DISTINCT payment_date, 
                                            MIN(begin_or_no) AS Expr1, 
                                            MAX(end_or_no) AS Expr2, 
                                            SUM(no_of_transaction) AS Expr3, 
                                            SUM(sales) AS Expr4, SUM(discount) AS Expr5, 
                                            SUM(vatable) AS Expr6, SUM(vat) AS Expr7, 
                                            SUM(vat_exempt) AS Expr8, 
                                            SUM(zero_rated) AS Expr9, 
                                            SUM(no_of_void) AS Expr10, 
                                            IFNULL(MIN(NULLIF(begin_void_no, 0)), 0) AS Expr11, 
                                            MAX(end_void_no) AS Expr12, 
                                            SUM(void) AS Expr13, SUM(grand_total) AS Expr14
                                            FROM accumulated_amount_tbl
                                            WHERE payment_date BETWEEN '" & DateTimePicker1Sales.Text & "' AND '" & DateTimePicker2Sales.Text & "' GROUP BY payment_date"
            Dim adp As New MySqlDataAdapter(salessummary, conn)
            Dim accumulated_amount_tblDataSet As New DataSet
            adp.Fill(accumulated_amount_tblDataSet, "accumulated_amount_tblDataSet")
            Me.ReportViewerBackEnd.LocalReport.ReportPath = ""
            Me.ReportViewerBackEnd.LocalReport.DataSources.Clear()
            Me.ReportViewerBackEnd.LocalReport.DataSources.Add(New ReportDataSource("accumulated_amount_tblDataSet", accumulated_amount_tblDataSet.Tables("accumulated_amount_tblDataSet")))
            Me.ReportViewerBackEnd.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        reports.LabelUserName.Text = LabelUserName.Text
        reports.LabelUserID.Text = LabelUserID.Text

        Me.Close()
        reports.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'for displaying current datetime
        LabelDateTime.Text = DateTime.Now.ToString("ddd MM/dd/yyyy HH:mm:ss")
    End Sub
End Class