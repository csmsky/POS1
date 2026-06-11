Imports MySql.Data.MySqlClient

Public Class ZReading

    Public begin_or_no As String
    Public end_or_no As String
    Public begin_balance As String
    Public end_balance As String
    Public no_of_transaction As String
    Public total_sales As String
    Public discount As String
    Public vatable_sale As String
    Public vat As String
    Public less_vat As String
    Public vat_exempt As String
    Public zero_rated_sale As String
    Public z_counter As String
    Public no_of_void As String
    Public begin_void_no As String
    Public end_void_no As String
    Public void_amount As String
    Public grand_total As String

    Public Shared Function Load(pos_id As Integer, payment_date As String, conn As MySqlConnection) As ZReading
        Dim zr As New ZReading()
        Dim dt As New DataTable()

        Using cmd As New MySqlCommand("getZReadingSummary", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@p_pos_id", pos_id)
            cmd.Parameters.AddWithValue("@p_payment_date", payment_date)

            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then
            Return Nothing
        End If

        Dim r = dt.Rows(0)

        zr.begin_or_no = r("begin_or_no").ToString()
        zr.end_or_no = r("end_or_no").ToString()
        zr.begin_balance = r("begin_balance").ToString()
        zr.end_balance = r("end_balance").ToString()
        zr.no_of_transaction = r("no_of_transaction").ToString()
        zr.total_sales = r("total_sales").ToString()
        zr.discount = r("discount").ToString()
        zr.vatable_sale = r("vatable_sale").ToString()
        zr.vat = r("vat").ToString()
        zr.less_vat = r("less_vat").ToString()
        zr.vat_exempt = r("vat_exempt").ToString()
        zr.zero_rated_sale = r("zero_rated_sale").ToString()
        zr.z_counter = r("z_counter").ToString()
        zr.no_of_void = r("no_of_void").ToString()
        zr.begin_void_no = r("begin_void_no").ToString()
        zr.end_void_no = r("end_void_no").ToString()
        zr.void_amount = r("void_amount").ToString()
        zr.grand_total = r("grand_total").ToString()

        Return zr
    End Function
End Class