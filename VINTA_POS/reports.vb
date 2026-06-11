Imports WindowsApplication1.ConfigClass
Imports Microsoft.Reporting.WinForms
Public Class reports
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)
    Dim pos = AppConfigReader.ps
    Dim station = AppConfigReader.sttn

    Dim cashier_id As String
    Dim cashiervoid_id As String
    Private Sub reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'for taking the company logo from resources
        'PictureBoxCompanyLogo.Image = WindowsApplication1.My.Resources.Logo
        PictureBoxVINTALogo.Image = WindowsApplication1.My.Resources.VINTA
        LabelPOSno.Text = pos
        LabelStationID.Text = station
        LabelUserID.Hide()

        'display current datetime in datetimepicker
        DateTimePicker1Sales.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        DateTimePicker2Sales.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        DateTimePicker1Void.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        DateTimePicker2Void.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)

        'display cashiers in combobox SALES
        Try
            Dim adapter As New MySqlDataAdapter("SELECT DISTINCT(CONCAT(CAST(AES_DECRYPT(first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(last_name, 'strdjnltmyp') AS CHAR(50)))) AS fullname, u.user_id AS cashier FROM user_registration_tbl u
                                            INNER JOIN or_tbl o ON u.user_id = o.user_id", conn)
            Dim table As New DataTable()

            adapter.Fill(table)

            'Sales Summary
            ComboBoxSalesCashier.DataSource = table
            ComboBoxSalesCashier.Text = "-- All Cashier --"
            ComboBoxSalesCashier.ValueMember = "cashier"
            ComboBoxSalesCashier.DisplayMember = "fullname"

        Catch ex As Exception
            MsgBox(ex.Message)
            ComboBoxSalesCashier.DataSource = Nothing
            ComboBoxVoidCashier.DataSource = Nothing
        End Try

        ' add the handler after populating the ComboBox to avoid unwanted firing of the event...
        AddHandler ComboBoxSalesCashier.SelectedIndexChanged, AddressOf ComboBoxSalesCashier_SelectedIndexChanged

        'display cashiers in combobox VOID
        Try
            Dim adaptervoid As New MySqlDataAdapter("SELECT DISTINCT(CONCAT(CAST(AES_DECRYPT(first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(last_name, 'strdjnltmyp') AS CHAR(50)))) AS fullname, u.user_id AS cashier FROM user_registration_tbl u
                                            INNER JOIN or_tbl o ON u.user_id = o.user_id", conn)
            Dim tablevoid As New DataTable()

            adaptervoid.Fill(tablevoid)

            'Void Transaction
            ComboBoxVoidCashier.DataSource = tablevoid
            ComboBoxVoidCashier.Text = "-- All Cashier --"
            ComboBoxVoidCashier.ValueMember = "cashier"
            ComboBoxVoidCashier.DisplayMember = "fullname"
        Catch ex As Exception
            MsgBox(ex.Message)
            ComboBoxSalesCashier.DataSource = Nothing
            ComboBoxVoidCashier.DataSource = Nothing
        End Try

        ComboBoxSalesCashier.Text = "-- All Cashier --"
        ComboBoxSalesType.Text = "-- Type of Transaction --"

        ComboBoxVoidCashier.Text = "-- All Cashier --"
        ComboBoxVoidType.Text = "-- Type of Transaction --"

        ' add the handler after populating the ComboBox to avoid unwanted firing of the event...
        AddHandler ComboBoxVoidCashier.SelectedIndexChanged, AddressOf ComboBoxSalesCashier_SelectedIndexChanged
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
        management.Show()
    End Sub

    Private Sub ButtonShowAll_Click(sender As Object, e As EventArgs) Handles ButtonShowAll.Click
        'show all transactions made
        Try
            Dim sales As String = "SELECT o.ticket_no, 
                                    o.or_no, 
                                    o.payment_date, 
                                    CONCAT(CAST(AES_DECRYPT(p.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(p.last_name, 'strdjnltmyp') AS CHAR(50))) AS Expr2,
                                    CAST(AES_DECRYPT(p.id_no, 'strdjnltmyp') AS CHAR(50)) AS id_no, 
                                    o.station_id, 
                                    o.pos_id, 
                                    CONCAT(CAST(AES_DECRYPT(u.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(u.last_name, 'strdjnltmyp') AS CHAR(50))) AS first_name, 
                                    o.trip_fare,
                                    o.baggage_cost,
                                    o.card_amount, 
                                    o.vatable, 
                                    o.vat, 
                                    o.vat_exempt, 
                                    o.zero_rated, 
                                    o.discount, 
                                    o.total_amount, 
                                    o.type_of_transaction, 
                                    o.void_status 
                                    FROM or_tbl o 
                                    INNER JOIN 
                                    user_registration_tbl u ON o.user_id = u.user_id
                                    INNER JOIN
                                    passenger_tbl p ON o.passenger_id = p.passenger_id
                                    WHERE o.station_id = '" & station & "' AND 
                                    o.payment_date BETWEEN '" & DateTimePicker1Sales.Text & "' AND '" & DateTimePicker2Sales.Text & "' AND
                                    o.pos_id = '" & LabelPOSno.Text & "'"

            Dim adp As New MySqlDataAdapter(sales, conn)
            Dim or_tblDataSet As New DataSet
            adp.Fill(or_tblDataSet, "or_tblDataSet")
            Me.ReportViewerSales.LocalReport.ReportPath = ""
            Me.ReportViewerSales.LocalReport.DataSources.Clear()
            Me.ReportViewerSales.LocalReport.DataSources.Add(New ReportDataSource("or_tblDataSet", or_tblDataSet.Tables("or_tblDataSet")))
            Me.ReportViewerSales.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        ComboBoxSalesCashier.Text = "-- All Cashier --"
        ComboBoxVoidCashier.Text = "-- All Cashier --"
        ComboBoxSalesType.Text = "-- Type of Transaction --"
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        'show filtered transactions
        Try
            If ComboBoxSalesCashier.Text = "-- All Cashier --" Then
                Dim sales As String = "SELECT o.ticket_no, 
                                    o.or_no, 
                                    o.payment_date, 
                                    CONCAT(CAST(AES_DECRYPT(p.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(p.last_name, 'strdjnltmyp') AS CHAR(50))) AS Expr2,
                                    CAST(AES_DECRYPT(p.id_no, 'strdjnltmyp') AS CHAR(50)) AS id_no, 
                                    o.station_id, 
                                    o.pos_id, 
                                    CONCAT(CAST(AES_DECRYPT(u.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(u.last_name, 'strdjnltmyp') AS CHAR(50))) AS first_name, 
                                    o.trip_fare,
                                    o.baggage_cost,
                                    o.card_amount, 
                                    o.vatable, 
                                    o.vat, 
                                    o.vat_exempt, 
                                    o.zero_rated, 
                                    o.discount, 
                                    o.total_amount, 
                                    o.type_of_transaction, 
                                    o.void_status 
                                    FROM or_tbl o 
                                    INNER JOIN 
                                    user_registration_tbl u ON o.user_id = u.user_id
                                    INNER JOIN
                                    passenger_tbl p ON o.passenger_id = p.passenger_id 
                                    WHERE o.payment_date BETWEEN '" & DateTimePicker1Sales.Text & "' AND '" & DateTimePicker2Sales.Text & "' AND
                                    o.type_of_transaction = '" & ComboBoxSalesType.Text & "' AND
                                    o.pos_id = '" & LabelPOSno.Text & "'"

                Dim adp As New MySqlDataAdapter(sales, conn)
                Dim or_tblDataSet As New DataSet
                adp.Fill(or_tblDataSet, "or_tblDataSet")
                Me.ReportViewerSales.LocalReport.ReportPath = ""
                Me.ReportViewerSales.LocalReport.DataSources.Clear()
                Me.ReportViewerSales.LocalReport.DataSources.Add(New ReportDataSource("or_tblDataSet", or_tblDataSet.Tables("or_tblDataSet")))
                Me.ReportViewerSales.RefreshReport()

            Else

                Dim sales As String = "SELECT o.ticket_no, 
                                    o.or_no, 
                                    o.payment_date, 
                                    CONCAT(CAST(AES_DECRYPT(p.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(p.last_name, 'strdjnltmyp') AS CHAR(50))) AS Expr2,
                                    CAST(AES_DECRYPT(p.id_no, 'strdjnltmyp') AS CHAR(50)) AS id_no, 
                                    o.station_id, 
                                    o.pos_id, 
                                    CONCAT(CAST(AES_DECRYPT(u.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(u.last_name, 'strdjnltmyp') AS CHAR(50))) AS first_name, 
                                    o.trip_fare,
                                    o.baggage_cost,
                                    o.card_amount, 
                                    o.vatable, 
                                    o.vat, 
                                    o.vat_exempt, 
                                    o.zero_rated, 
                                    o.discount, 
                                    o.total_amount, 
                                    o.type_of_transaction, 
                                    o.void_status 
                                    FROM or_tbl o 
                                    INNER JOIN 
                                    user_registration_tbl u ON o.user_id = u.user_id
                                    INNER JOIN
                                    passenger_tbl p ON o.passenger_id = p.passenger_id 
                                    WHERE o.payment_date BETWEEN '" & DateTimePicker1Sales.Text & "' AND '" & DateTimePicker2Sales.Text & "' AND 
                                    o.user_id = '" & cashier_id & "' AND
                                    o.type_of_transaction = '" & ComboBoxSalesType.Text & "' AND
                                    o.pos_id = '" & LabelPOSno.Text & "'"

                Dim adp As New MySqlDataAdapter(sales, conn)
                Dim or_tblDataSet As New DataSet
                adp.Fill(or_tblDataSet, "or_tblDataSet")
                Me.ReportViewerSales.LocalReport.ReportPath = ""
                Me.ReportViewerSales.LocalReport.DataSources.Clear()
                Me.ReportViewerSales.LocalReport.DataSources.Add(New ReportDataSource("or_tblDataSet", or_tblDataSet.Tables("or_tblDataSet")))
                Me.ReportViewerSales.RefreshReport()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        ComboBoxSalesCashier.Text = "-- All Cashier --"
        ComboBoxVoidCashier.Text = "-- All Cashier --"
        ComboBoxSalesType.Text = "-- Type of Transaction --"
    End Sub

    Private Sub ButtonShowAllVoid_Click(sender As Object, e As EventArgs) Handles ButtonShowAllVoid.Click
        'show all void transactions
        Try
            Dim void As String = "SELECT v.void_no,
                                v.or_no, 
                                v.payment_date, 
                                CONCAT(CAST(AES_DECRYPT(p.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(p.last_name, 'strdjnltmyp') AS CHAR(50))) AS Expr2,
                                CAST(AES_DECRYPT(p.id_no, 'strdjnltmyp') AS CHAR(50)) AS id_no, 
                                v.station_id, 
                                v.pos_id, 
                                CAST(AES_DECRYPT(ur.first_name, 'strdjnltmyp') AS CHAR(50)) Expr1,
                                CAST(AES_DECRYPT(u.first_name, 'strdjnltmyp') AS CHAR(50)) first_name, 
                                v.trip_fare,
                                v.baggage_cost,
                                v.card_amount, 
                                v.vatable, 
                                v.vat, 
                                v.vat_exempt, 
                                v.zero_rated, 
                                v.discount, 
                                v.total_amount, 
                                v.type_of_transaction
                                FROM void_tbl v 
                                INNER JOIN user_registration_tbl u ON v.user_id = u.user_id 
                                INNER JOIN user_registration_tbl ur ON v.void_by = ur.user_id
                                INNER JOIN passenger_tbl p ON v.passenger_id = p.passenger_id
                                WHERE v.payment_date BETWEEN '" & DateTimePicker1Void.Text & "' AND '" & DateTimePicker2Void.Text & "' AND
                                v.pos_id = '" & LabelPOSno.Text & "'"
            Dim adp As New MySqlDataAdapter(void, conn)
            Dim void_tblDataSet As New DataSet
            adp.Fill(void_tblDataSet, "void_tblDataSet")
            Me.ReportViewerVoid.LocalReport.ReportPath = ""
            Me.ReportViewerVoid.LocalReport.DataSources.Clear()
            Me.ReportViewerVoid.LocalReport.DataSources.Add(New ReportDataSource("void_tblDataSet", void_tblDataSet.Tables("void_tblDataSet")))
            Me.ReportViewerVoid.RefreshReport()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ComboBoxSalesCashier.Text = "-- All Cashier --"
        ComboBoxVoidCashier.Text = "-- All Cashier --"
        ComboBoxVoidType.Text = "-- Type of Transaction --"
    End Sub

    Private Sub ButtonSearchVoid_Click(sender As Object, e As EventArgs) Handles ButtonSearchVoid.Click
        'display fitered void transactions
        Try
            If ComboBoxVoidCashier.Text = "-- All Cashier --" Then
                Dim void As String = "SELECT v.void_no,
                                v.or_no, 
                                v.payment_date, 
                                CONCAT(CAST(AES_DECRYPT(p.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(p.last_name, 'strdjnltmyp') AS CHAR(50))) AS Expr2,
                                CAST(AES_DECRYPT(p.id_no, 'strdjnltmyp') AS CHAR(50)) AS id_no, 
                                v.station_id, 
                                v.pos_id, 
                                CAST(AES_DECRYPT(ur.first_name, 'strdjnltmyp') AS CHAR(50)) Expr1,
                                CAST(AES_DECRYPT(u.first_name, 'strdjnltmyp') AS CHAR(50)) first_name, 
                                v.trip_fare,
                                v.baggage_cost,
                                v.card_amount, 
                                v.vatable, 
                                v.vat, 
                                v.vat_exempt, 
                                v.zero_rated, 
                                v.discount, 
                                v.total_amount, 
                                v.type_of_transaction
                                FROM void_tbl v 
                                INNER JOIN user_registration_tbl u ON v.user_id = u.user_id 
                                INNER JOIN user_registration_tbl ur ON v.void_by = ur.user_id
                                INNER JOIN passenger_tbl p ON v.passenger_id = p.passenger_id 
                                WHERE v.payment_date BETWEEN '" & DateTimePicker1Void.Text & "' AND '" & DateTimePicker2Void.Text & "' AND
                                v.type_of_transaction = '" & ComboBoxVoidType.Text & "' AND
                                v.pos_id = '" & LabelPOSno.Text & "'"
                Dim adp As New MySqlDataAdapter(void, conn)
                Dim void_tblDataSet As New DataSet
                adp.Fill(void_tblDataSet, "void_tblDataSet")
                Me.ReportViewerVoid.LocalReport.ReportPath = ""
                Me.ReportViewerVoid.LocalReport.DataSources.Clear()
                Me.ReportViewerVoid.LocalReport.DataSources.Add(New ReportDataSource("void_tblDataSet", void_tblDataSet.Tables("void_tblDataSet")))
                Me.ReportViewerVoid.RefreshReport()
            Else
                Dim void As String = "SELECT v.void_no,
                                v.or_no, 
                                v.payment_date, 
                                CONCAT(CAST(AES_DECRYPT(p.first_name, 'strdjnltmyp') AS CHAR(50)), ' ', CAST(AES_DECRYPT(p.last_name, 'strdjnltmyp') AS CHAR(50))) AS Expr2,
                                CAST(AES_DECRYPT(p.id_no, 'strdjnltmyp') AS CHAR(50)) AS id_no, 
                                v.station_id, 
                                v.pos_id, 
                                CAST(AES_DECRYPT(ur.first_name, 'strdjnltmyp') AS CHAR(50)) Expr1,
                                CAST(AES_DECRYPT(u.first_name, 'strdjnltmyp') AS CHAR(50)) first_name, 
                                v.trip_fare,
                                v.baggage_cost,
                                v.card_amount, 
                                v.vatable, 
                                v.vat, 
                                v.vat_exempt, 
                                v.zero_rated, 
                                v.discount, 
                                v.total_amount, 
                                v.type_of_transaction
                                FROM void_tbl v 
                                INNER JOIN user_registration_tbl u ON v.user_id = u.user_id 
                                INNER JOIN user_registration_tbl ur ON v.void_by = ur.user_id
                                INNER JOIN passenger_tbl p ON v.passenger_id = p.passenger_id 
                                WHERE v.payment_date BETWEEN '" & DateTimePicker1Void.Text & "' AND '" & DateTimePicker2Void.Text & "' AND 
                                v.user_id = '" & cashier_id & "' AND
                                v.type_of_transaction = '" & ComboBoxVoidType.Text & "' AND
                                v.pos_id = '" & LabelPOSno.Text & "'"
                Dim adp As New MySqlDataAdapter(void, conn)
                Dim void_tblDataSet As New DataSet
                adp.Fill(void_tblDataSet, "void_tblDataSet")
                Me.ReportViewerVoid.LocalReport.ReportPath = ""
                Me.ReportViewerVoid.LocalReport.DataSources.Clear()
                Me.ReportViewerVoid.LocalReport.DataSources.Add(New ReportDataSource("void_tblDataSet", void_tblDataSet.Tables("void_tblDataSet")))
                Me.ReportViewerVoid.RefreshReport()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        ComboBoxSalesCashier.Text = "-- All Cashier --"
        ComboBoxVoidCashier.Text = "-- All Cashier --"
        ComboBoxVoidType.Text = "-- Type of Transaction --"
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'for displaying current datetime
        LabelDateTime.Text = DateTime.Now.ToString("ddd MM/dd/yyyy HH:mm:ss")
    End Sub

    Private Sub ComboBoxSalesCashier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSalesCashier.SelectedIndexChanged
        'get cashier id on select
        Dim cb = DirectCast(sender, ComboBox)
        ' SelectedValue is an Object - you can get the name of its actual type with .SelectedValue.GetType().Name
        cashier_id = (cb.SelectedValue).ToString
    End Sub

    Private Sub ComboBoxVoidCashier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxVoidCashier.SelectedIndexChanged
        'get cashier id on select
        Dim cb = DirectCast(sender, ComboBox)
        ' SelectedValue is an Object - you can get the name of its actual type with .SelectedValue.GetType().Name
        cashiervoid_id = (cb.SelectedValue).ToString
    End Sub

    Private Sub ButtonSalesSummary_Click(sender As Object, e As EventArgs) Handles ButtonSalesSummary.Click
        backendreport.LabelUserName.Text = LabelUserName.Text
        backendreport.LabelUserID.Text = LabelUserID.Text

        Me.Close()
        backendreport.Show()
    End Sub
End Class