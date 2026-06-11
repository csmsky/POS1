Imports MySql.Data.MySqlClient
Imports WindowsApplication1.ConfigClass

Public Class InputCustomerDetails

    Dim strConn As String = FDEandD()
    Dim conn As New MySqlConnection(strConn)

    ' Variables to remember context
    Private _transType As String = ""
    Private _currentOR As String = "" '<-- Variable to hold the OR number
    Private _currentQty As String = ""

    ' In-memory pending list (NOT yet saved to DB)
    Private _pendingCustomers As New List(Of CustomerInfo)

    ' Track which row is being edited (-1 means adding new)
    Private _editingIndex As Integer = -1

    ' New controls added via code
    Private WithEvents DataGridViewCustomers As New DataGridView()
    Private WithEvents ButtonEdit As New Button()
    Private WithEvents ButtonDelete As New Button()
    Private WithEvents ButtonConfirm As New Button()

    ' Call this from your Main POS Form to pass the Type AND the OR Number
    Public Sub SetTransactionDetails(type As String, orNo As String, Optional qty As String = "")
        _transType = type
        _currentOR = orNo
        _currentQty = qty

        ' This will change the window title to clearly show the ride info!
        Me.Text = "Registering " & qty & "x " & type & " Rides"
    End Sub


    Private Sub InputCustomerDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load all previous customer names into the Search Bar memory
        Try
            Dim nameCollection As New AutoCompleteStringCollection()
            Using dbConn As New MySqlConnection(strConn)
                dbConn.Open()
                Using cmd As New MySqlCommand("SELECT DISTINCT CAST(AES_DECRYPT(name, 'strdjnltmyp') AS CHAR) AS name FROM customer_tbl WHERE name IS NOT NULL", dbConn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            nameCollection.Add(reader("name").ToString())
                        End While
                    End Using
                End Using
            End Using
            TextBoxGuestName.AutoCompleteCustomSource = nameCollection
        Catch ex As Exception
        End Try

        ' --- Make the form taller to fit the new controls ---
        Me.ClientSize = New System.Drawing.Size(580, 510)

        ' --- Move the existing "(+)Add" button up a bit to make room ---
        Button1.Location = New System.Drawing.Point(280, 210)
        Button1.Size = New System.Drawing.Size(114, 40)

        ' --- DataGridView to show pending entries ---
        DataGridViewCustomers.Location = New System.Drawing.Point(8, 300)
        DataGridViewCustomers.Size = New System.Drawing.Size(544, 150)
        DataGridViewCustomers.AllowUserToAddRows = False
        DataGridViewCustomers.AllowUserToDeleteRows = False
        DataGridViewCustomers.ReadOnly = True
        DataGridViewCustomers.MultiSelect = False
        DataGridViewCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridViewCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCustomers.RowHeadersWidth = 30
        Me.Controls.Add(DataGridViewCustomers)

        ' --- Edit Button ---
        ButtonEdit.Text = "Edit"
        ButtonEdit.Location = New System.Drawing.Point(8, 460)
        ButtonEdit.Size = New System.Drawing.Size(114, 40)
        Me.Controls.Add(ButtonEdit)

        ' --- Delete Button ---
        ButtonDelete.Text = "Delete"
        ButtonDelete.Location = New System.Drawing.Point(130, 460)
        ButtonDelete.Size = New System.Drawing.Size(114, 40)
        Me.Controls.Add(ButtonDelete)

        ' --- Confirm & Save Button ---
        ButtonConfirm.Text = "Confirm && Save"
        ButtonConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold)
        ButtonConfirm.Location = New System.Drawing.Point(368, 460)
        ButtonConfirm.Size = New System.Drawing.Size(184, 40)
        Me.Controls.Add(ButtonConfirm)

        RefreshGrid()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Validation: Ensure Name, ID, and OR Number (Barcode) are filled out
        If String.IsNullOrWhiteSpace(TextBoxGuestName.Text) OrElse String.IsNullOrWhiteSpace(TextBoxGuestID.Text) Then
            MessageBox.Show("Please enter Name and ID.", "Missing Info")
            Exit Sub
        End If

        ' Clean up the inputs
        Dim cleanTIN As String = TextBoxGuestTIN.Text.Replace("-", "").Trim()

        ' 1. Save to Memory ONLY (Database upload happens during Confirm & Save)
        Dim newCustomer As New CustomerInfo With {
            .Name = TextBoxGuestName.Text.Trim(),
            .ID = TextBoxGuestID.Text.Trim(),
            .TIN = cleanTIN,
            .Address = TextBoxGuestAddress.Text.Trim(),
            .TransType = _transType
        }

        ' 1. Delete any old verification rows first
        For i As Integer = secondscreen.ListViewCustomerView.Items.Count - 1 To 0 Step -1
            If secondscreen.ListViewCustomerView.Items(i).Tag IsNot Nothing AndAlso secondscreen.ListViewCustomerView.Items(i).Tag.ToString() = "TEMP_NAME" Then
                secondscreen.ListViewCustomerView.Items.RemoveAt(i)
            End If
        Next

        ' 2. Blast the new name directly into the wide Ride Description column
        Dim tempItem As New ListViewItem("") ' Qty column
        tempItem.SubItems.Add("Name Verification: " & newCustomer.Name)
        tempItem.SubItems.Add("") ' Price
        tempItem.SubItems.Add("") ' Total
        tempItem.SubItems.Add("") ' Discount
        tempItem.SubItems.Add("") ' Amount

        tempItem.Tag = "TEMP_NAME"
        secondscreen.ListViewCustomerView.Items.Add(tempItem)



        If _editingIndex >= 0 AndAlso _editingIndex <_pendingCustomers.Count Then
            ' Update existing entry
        _pendingCustomers(_editingIndex) = newCustomer
            _editingIndex = -1
            Button1.Text = "(+)Add"
        Else
            ' Add to your global/shared list (in memory)
            _pendingCustomers.Add(newCustomer)
        End If

        ' Notify the user it was added to the current transaction queue
        MessageBox.Show($"{_transType} details added to the current transaction!", "Success")

        ' Clear text boxes for the next entry
        TextBoxGuestName.Clear()
        TextBoxGuestID.Clear()
        TextBoxGuestTIN.Clear()
        TextBoxGuestAddress.Clear()

        RefreshGrid()
    End Sub

    Private Sub InputCustomerDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Clear the text fields when the form is closed
        TextBoxGuestName.Clear()
        TextBoxGuestID.Clear()
        TextBoxGuestTIN.Clear()
        TextBoxGuestAddress.Clear()

        ' Reset the Add/Update button state
        Button1.Text = "(+)Add"
        _editingIndex = -1
    End Sub

    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click
        If DataGridViewCustomers.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a row to edit.", "No Selection")
            Exit Sub
        End If

        _editingIndex = DataGridViewCustomers.SelectedRows(0).Index

        If _editingIndex < 0 OrElse _editingIndex >= _pendingCustomers.Count Then Exit Sub

        Dim entry As CustomerInfo = _pendingCustomers(_editingIndex)
        TextBoxGuestName.Text = entry.Name
        TextBoxGuestID.Text = entry.ID
        TextBoxGuestTIN.Text = entry.TIN
        TextBoxGuestAddress.Text = entry.Address

        Button1.Text = "Update"
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        ' Delete the verification row from the 2nd screen if the cashier hits Delete
        For i As Integer = secondscreen.ListViewCustomerView.Items.Count - 1 To 0 Step -1
            If secondscreen.ListViewCustomerView.Items(i).Tag IsNot Nothing AndAlso secondscreen.ListViewCustomerView.Items(i).Tag.ToString() = "TEMP_NAME" Then
                secondscreen.ListViewCustomerView.Items.RemoveAt(i)
            End If
        Next


        If DataGridViewCustomers.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a row to delete.", "No Selection")
            Exit Sub
        End If

        Dim idx As Integer = DataGridViewCustomers.SelectedRows(0).Index

        If idx < 0 OrElse idx >= _pendingCustomers.Count Then Exit Sub

        Dim result = MessageBox.Show("Remove this entry?", "Confirm Delete", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            _pendingCustomers.RemoveAt(idx)
            _editingIndex = -1
            Button1.Text = "(+)Add"
            TextBoxGuestName.Clear()
            TextBoxGuestID.Clear()
            TextBoxGuestTIN.Clear()
            TextBoxGuestAddress.Clear()
            RefreshGrid()
        End If
    End Sub

    Private Sub ButtonConfirm_Click(sender As Object, e As EventArgs) Handles ButtonConfirm.Click
        If _pendingCustomers.Count = 0 Then
            MessageBox.Show("No customer details to save. Please add at least one entry.", "Empty List")
            Exit Sub
        End If

        auto()

        Using dbConn As New MySqlConnection(strConn)
            Try
                dbConn.Open()

                For Each cust As CustomerInfo In _pendingCustomers
                    Using cmd As New MySqlCommand("insert_customer_procedure", dbConn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@p_or_no", mainform.TextBoxBarcode.Text)
                        cmd.Parameters.AddWithValue("@p_name", cust.Name)
                        cmd.Parameters.AddWithValue("@p_id_no", cust.ID)
                        cmd.Parameters.AddWithValue("@p_tin_no", cust.TIN)
                        cmd.Parameters.AddWithValue("@p_address", cust.Address)
                        cmd.Parameters.AddWithValue("@p_transType", cust.TransType)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Add to your global/shared list
                    cust.ORNumber = mainform.TextBoxBarcode.Text
                    eJournalCustomerData.CustomerList.Add(cust)
                Next

                MessageBox.Show("Customer saved to database!", "Success")

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
                Exit Sub
            End Try
        End Using
        ' Close the form and return to POS
        mainform.CurrentCustomerName = _pendingCustomers(0).Name
        Me.DialogResult = DialogResult.OK
        _pendingCustomers.Clear()
        Me.Close()

    End Sub

    Private Sub RefreshGrid()
        Dim dt As New DataTable()
        dt.Columns.Add("Name", GetType(String))
        dt.Columns.Add("ID No.", GetType(String))
        dt.Columns.Add("TIN", GetType(String))
        dt.Columns.Add("Address", GetType(String))

        For Each c As CustomerInfo In _pendingCustomers
            dt.Rows.Add(c.Name, c.ID, c.TIN, c.Address)
        Next

        DataGridViewCustomers.DataSource = dt
    End Sub

    Private Sub auto()
        Dim seq As Long = 0
        mainform.rstCnt = 0

        conn.Open()
        Dim qd As String = "SELECT or_no, rst_cnt FROM or_tbl WHERE pos_id = '" & mainform.LabelPOSno.Text & "' ORDER BY id DESC LIMIT 1"
        Dim cmd As New MySqlCommand(qd) With {.Connection = conn}
        Dim rdr As MySqlDataReader = cmd.ExecuteReader()
        If rdr.Read() Then
            If Not IsDBNull(rdr("or_no")) Then
                Dim orNoStr As String = rdr("or_no").ToString()
                If Long.TryParse(orNoStr, seq) Then
                    ' Successfully parsed exact Long, preventing precision loss
                Else
                    Dim parsedVal As Double = 0
                    If Double.TryParse(orNoStr, parsedVal) Then
                        seq = Convert.ToInt64(parsedVal)
                    End If
                End If
            End If
            If Not IsDBNull(rdr("rst_cnt")) Then
                mainform.rstCnt = Convert.ToInt32(rdr("rst_cnt").ToString())
            End If
        End If
        rdr.Close()

        If seq >= 9999999999999999 Then
            seq = 1
            mainform.rstCnt += 1
        Else
            seq += 1
        End If

        mainform.TextBoxBarcode.Text = seq.ToString("D16")

        Dim lblReset As Control = mainform.Controls.Find("LabelReset", True).FirstOrDefault()
        If lblReset IsNot Nothing Then
            lblReset.Text = mainform.rstCnt.ToString()
        End If

        conn.Close()

    End Sub


    Private Sub TextBoxGuestName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxGuestName.TextChanged

    End Sub

    Private Sub TextBoxGuestName_Leave(sender As Object, e As EventArgs) Handles TextBoxGuestName.Leave
        ' If the name box is blank, don't do anything
        If String.IsNullOrWhiteSpace(TextBoxGuestName.Text) Then Exit Sub

        ' Search the database for this name to see if they are a returning customer!
        Try
            Using dbConn As New MySqlConnection(strConn)
                dbConn.Open()
                Dim query As String = "SELECT id_no, tin_no, address FROM customer_tbl WHERE name = AES_ENCRYPT(@name, 'strdjnltmyp') LIMIT 1"

                Using cmd As New MySqlCommand(query, dbConn)
                    cmd.Parameters.AddWithValue("@name", TextBoxGuestName.Text)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' BINGO! We found a match. Auto-fill the rest of the boxes!
                            TextBoxGuestID.Text = reader("id_no").ToString()
                            TextBoxGuestTIN.Text = reader("tin_no").ToString()
                            TextBoxGuestAddress.Text = reader("address").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' If the database doesn't connect, just fail silently so it doesn't interrupt the cashier
        End Try

    End Sub

    Private Sub GroupBoxGuestDetails_Enter(sender As Object, e As EventArgs) Handles GroupBoxGuestDetails.Enter

    End Sub
End Class


Public Class CustomerInfo
    Public Property ORNumber As String
    Public Property Name As String
    Public Property ID As String
    Public Property TIN As String
    Public Property Address As String
    Public Property TransType As String
End Class
