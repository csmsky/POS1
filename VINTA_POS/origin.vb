Imports WindowsApplication1.ConfigClass
Public Class origin
    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    Dim MyButton = New Button
    Private Sub origin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        mainform.TextBoxQty.Text = ""
        mainform.LabelTotal.Text = "0.00"
        mainform.ButtonStudent.BackColor = Color.Gainsboro
        mainform.ButtonSoloParent.BackColor = Color.Gainsboro
        mainform.ButtonSenior.BackColor = Color.Gainsboro
        mainform.ButtonPWD.BackColor = Color.Gainsboro
        mainform.ButtonEmployee.BackColor = Color.Gainsboro
        mainform.ButtonZeroRated.BackColor = Color.Gainsboro
        mainform.ButtonRegularRide.BackColor = Color.Cyan

        'for making dynamic buttons of origin station
        Dim PointButton As Point = New Point
        Try
            Dim Query = "SELECT type, label_code FROM ride_pricing_tbl"
            Dim da As New MySqlDataAdapter(Query, conn)
            Dim ds As New DataSet
            ds.Clear()
            da.Fill(ds, "type")

            FlowLayoutPanel1.Controls.Clear()

            ' Calculate width for 3 buttons per row
            Dim btnWidth As Integer = (FlowLayoutPanel1.ClientSize.Width - 40) \ 3

            For i = 0 To ds.Tables(0).Rows.Count - 1

                Dim btn As New Button

                btn.Text = ds.Tables(0).Rows(i)("type").ToString()
                btn.Tag = ds.Tables(0).Rows(i)("label_code").ToString()

                btn.Font = New Font("Tahoma", 12)
                btn.Width = btnWidth
                btn.Height = 80
                btn.Margin = New Padding(5)

                AddHandler btn.Click, AddressOf ClickMe

                FlowLayoutPanel1.Controls.Add(btn)

            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Unknown Database")
        End Try
    End Sub

    Private Sub ClickMe(ByVal sender As Object, e As EventArgs)

        ' 1) Identify the clicked button
        Dim btn = TryCast(sender, Button)
        If btn Is Nothing Then Exit Sub

        ' 2) Set origin text and reset money-related fields once
        mainform.ButtonRegularRide.Text = btn.Text
        ResetMoneyFields()

        mainform.label_code.Text = btn.Tag.ToString()

        ' 3) Get fare ID for this type
        Dim fareId As Integer
        Using conn As New MySqlConnection(strConn),
              cmd As New MySqlCommand("SELECT id FROM ride_pricing_tbl WHERE type = @type", conn)
            cmd.Parameters.Add("@type", MySqlDbType.VarChar).Value = btn.Text
            mainform.LabelAvailed.Text = btn.Text
            conn.Open()
            Dim result = cmd.ExecuteScalar()                  ' <- returns first column of first row
            If result Is Nothing OrElse result Is DBNull.Value Then
                MessageBox.Show($"No fare found for type: {btn.Text}", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            fareId = Convert.ToInt32(result)
        End Using
        mainform.OriginID.Text = fareId.ToString()

        ' 4) Get amount for this fare ID
        Dim amount As Decimal = 0D
        Using conn As New MySqlConnection(strConn),
              cmd As New MySqlCommand("SELECT regular FROM ride_pricing_tbl WHERE id = @id", conn)
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = fareId
            conn.Open()
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                amount = Convert.ToDecimal(result)
            End If

        End Using

        ' 5) Fill UI
        mainform.TextBoxTripFare.Text = amount.ToString("0.00")
        mainform.LabelFare.Text = mainform.TextBoxTripFare.Text

        Me.Close()

    End Sub
    Private Sub ResetMoneyFields()
        mainform.TextBoxTripFare.Text = "0.00"
        mainform.TextBoxMoney.Clear()
        mainform.TextBoxChange.Clear()
    End Sub
End Class