Imports WindowsApplication1.ConfigClass
Public Class promo

    'NOTE: If you can't connect to the database, check the connection in "App.config", replace the connection if needed. Make sure to insert the encrypted data.

    'for calling connection in App.config
    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    Dim MyButton = New Button
    Private Sub promo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            'Dim Query = "SELECT station_name FROM station_tbl"
            Dim Query = "SELECT type, label_code FROM promo_tbl"
            Dim da As New MySqlDataAdapter(Query, conn)
            Dim ds As New DataSet
            ds.Clear()
            da.Fill(ds, "type")

            ReDim MyButton(ds.Tables(0).Rows.Count)

            Dim one As Integer = 1
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Dim MyButton As New Button

                MyButton.Text = ds.Tables(0).Rows.Item(i).Item(0).ToString

                'Hidden value to pass
                MyButton.Tag = ds.Tables(0).Rows(i)("label_code").ToString()

                MyButton.Name = "type" & one.ToString
                MyButton.Font = New Font("Tahoma", 12)
                MyButton.Width = 130
                MyButton.Height = 80
                PointButton.Y = 3
                If one = 1 Then
                    PointButton.X = PointButton.X
                Else
                    PointButton.X = PointButton.X + MyButton.Width + 20
                End If
                MyButton.Location = PointButton

                DirectCast(FlowLayoutPanel1, Panel).Controls.Add(MyButton)
                one += 1
                AddHandler MyButton.Click, AddressOf ClickMe
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Uknown Databases")
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
              cmd As New MySqlCommand("SELECT introductory FROM ride_pricing_tbl WHERE id = @id", conn)
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