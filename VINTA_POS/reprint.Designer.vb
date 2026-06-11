<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class reprint
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim LabelTime As System.Windows.Forms.Label
        Dim LabelDate As System.Windows.Forms.Label
        Dim LabelCashierName As System.Windows.Forms.Label
        Dim LabelTicket As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(reprint))
        Me.LabelSearchPassenger = New System.Windows.Forms.Label()
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ButtonReprint = New System.Windows.Forms.Button()
        Me.PictureBoxBarcode = New System.Windows.Forms.PictureBox()
        Me.PictureBoxLogoReceipt = New System.Windows.Forms.PictureBox()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelTimeValue = New System.Windows.Forms.Label()
        Me.LabelDateValue = New System.Windows.Forms.Label()
        Me.LabelCashierValue = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ComboReaderNames = New System.Windows.Forms.ComboBox()
        Me.TextBoxUserID = New System.Windows.Forms.TextBox()
        Me.LabelORValue = New System.Windows.Forms.Label()
        Me.PictureBoxBarcodeTicket = New System.Windows.Forms.PictureBox()
        Me.lblFpStatus = New System.Windows.Forms.Label()
        Me.btnCancelFp = New System.Windows.Forms.Button()
        LabelTime = New System.Windows.Forms.Label()
        LabelDate = New System.Windows.Forms.Label()
        LabelCashierName = New System.Windows.Forms.Label()
        LabelTicket = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLogoReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxBarcodeTicket, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelTime
        '
        LabelTime.Anchor = System.Windows.Forms.AnchorStyles.None
        LabelTime.AutoSize = True
        LabelTime.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        LabelTime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        LabelTime.Location = New System.Drawing.Point(552, 437)
        LabelTime.Name = "LabelTime"
        LabelTime.Size = New System.Drawing.Size(65, 26)
        LabelTime.TabIndex = 215
        LabelTime.Text = "Time:"
        '
        'LabelDate
        '
        LabelDate.Anchor = System.Windows.Forms.AnchorStyles.None
        LabelDate.AutoSize = True
        LabelDate.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        LabelDate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        LabelDate.Location = New System.Drawing.Point(552, 410)
        LabelDate.Name = "LabelDate"
        LabelDate.Size = New System.Drawing.Size(63, 26)
        LabelDate.TabIndex = 214
        LabelDate.Text = "Date:"
        '
        'LabelCashierName
        '
        LabelCashierName.Anchor = System.Windows.Forms.AnchorStyles.None
        LabelCashierName.AutoSize = True
        LabelCashierName.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        LabelCashierName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        LabelCashierName.Location = New System.Drawing.Point(552, 383)
        LabelCashierName.Name = "LabelCashierName"
        LabelCashierName.Size = New System.Drawing.Size(149, 26)
        LabelCashierName.TabIndex = 213
        LabelCashierName.Text = "Cashier Name:"
        '
        'LabelTicket
        '
        LabelTicket.Anchor = System.Windows.Forms.AnchorStyles.None
        LabelTicket.AutoSize = True
        LabelTicket.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        LabelTicket.ImeMode = System.Windows.Forms.ImeMode.NoControl
        LabelTicket.Location = New System.Drawing.Point(12, 383)
        LabelTicket.Name = "LabelTicket"
        LabelTicket.Size = New System.Drawing.Size(159, 26)
        LabelTicket.TabIndex = 232
        LabelTicket.Text = "Sales Invoice #:"
        '
        'Label1
        '
        Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Label1.Location = New System.Drawing.Point(12, 427)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(227, 26)
        Label1.TabIndex = 350
        Label1.Text = "Authorized to Reprint:"
        '
        'LabelSearchPassenger
        '
        Me.LabelSearchPassenger.AutoSize = True
        Me.LabelSearchPassenger.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSearchPassenger.Location = New System.Drawing.Point(632, 33)
        Me.LabelSearchPassenger.Name = "LabelSearchPassenger"
        Me.LabelSearchPassenger.Size = New System.Drawing.Size(81, 25)
        Me.LabelSearchPassenger.TabIndex = 189
        Me.LabelSearchPassenger.Text = "Search:"
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Location = New System.Drawing.Point(735, 33)
        Me.TextBoxSearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(279, 22)
        Me.TextBoxSearch.TabIndex = 188
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(16, 62)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.Size = New System.Drawing.Size(999, 309)
        Me.DataGridView1.TabIndex = 180
        '
        'ButtonReprint
        '
        Me.ButtonReprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonReprint.Location = New System.Drawing.Point(797, 512)
        Me.ButtonReprint.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonReprint.Name = "ButtonReprint"
        Me.ButtonReprint.Size = New System.Drawing.Size(217, 55)
        Me.ButtonReprint.TabIndex = 179
        Me.ButtonReprint.Text = "Reprint"
        Me.ButtonReprint.UseVisualStyleBackColor = True
        '
        'PictureBoxBarcode
        '
        Me.PictureBoxBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxBarcode.Location = New System.Drawing.Point(17, 534)
        Me.PictureBoxBarcode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxBarcode.Name = "PictureBoxBarcode"
        Me.PictureBoxBarcode.Size = New System.Drawing.Size(25, 32)
        Me.PictureBoxBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxBarcode.TabIndex = 178
        Me.PictureBoxBarcode.TabStop = False
        Me.PictureBoxBarcode.Visible = False
        '
        'PictureBoxLogoReceipt
        '
        Me.PictureBoxLogoReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxLogoReceipt.Location = New System.Drawing.Point(48, 533)
        Me.PictureBoxLogoReceipt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxLogoReceipt.Name = "PictureBoxLogoReceipt"
        Me.PictureBoxLogoReceipt.Size = New System.Drawing.Size(26, 33)
        Me.PictureBoxLogoReceipt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxLogoReceipt.TabIndex = 177
        Me.PictureBoxLogoReceipt.TabStop = False
        Me.PictureBoxLogoReceipt.Visible = False
        '
        'ButtonClose
        '
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.Location = New System.Drawing.Point(572, 512)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(217, 55)
        Me.ButtonClose.TabIndex = 211
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1032, 26)
        Me.Panel1.TabIndex = 212
        '
        'LabelTimeValue
        '
        Me.LabelTimeValue.AutoSize = True
        Me.LabelTimeValue.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTimeValue.Location = New System.Drawing.Point(712, 437)
        Me.LabelTimeValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTimeValue.Name = "LabelTimeValue"
        Me.LabelTimeValue.Size = New System.Drawing.Size(0, 26)
        Me.LabelTimeValue.TabIndex = 226
        '
        'LabelDateValue
        '
        Me.LabelDateValue.AutoSize = True
        Me.LabelDateValue.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDateValue.Location = New System.Drawing.Point(712, 410)
        Me.LabelDateValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelDateValue.Name = "LabelDateValue"
        Me.LabelDateValue.Size = New System.Drawing.Size(0, 26)
        Me.LabelDateValue.TabIndex = 225
        '
        'LabelCashierValue
        '
        Me.LabelCashierValue.AutoSize = True
        Me.LabelCashierValue.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCashierValue.Location = New System.Drawing.Point(712, 383)
        Me.LabelCashierValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCashierValue.Name = "LabelCashierValue"
        Me.LabelCashierValue.Size = New System.Drawing.Size(0, 26)
        Me.LabelCashierValue.TabIndex = 224
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Location = New System.Drawing.Point(0, 575)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1032, 26)
        Me.Panel2.TabIndex = 227
        '
        'ComboReaderNames
        '
        Me.ComboReaderNames.FormattingEnabled = True
        Me.ComboReaderNames.Location = New System.Drawing.Point(80, 457)
        Me.ComboReaderNames.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboReaderNames.Name = "ComboReaderNames"
        Me.ComboReaderNames.Size = New System.Drawing.Size(60, 24)
        Me.ComboReaderNames.TabIndex = 231
        '
        'TextBoxUserID
        '
        Me.TextBoxUserID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxUserID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxUserID.Location = New System.Drawing.Point(145, 457)
        Me.TextBoxUserID.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxUserID.Name = "TextBoxUserID"
        Me.TextBoxUserID.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxUserID.Size = New System.Drawing.Size(257, 30)
        Me.TextBoxUserID.TabIndex = 230
        Me.TextBoxUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LabelORValue
        '
        Me.LabelORValue.AutoSize = True
        Me.LabelORValue.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelORValue.Location = New System.Drawing.Point(196, 383)
        Me.LabelORValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelORValue.Name = "LabelORValue"
        Me.LabelORValue.Size = New System.Drawing.Size(0, 26)
        Me.LabelORValue.TabIndex = 233
        '
        'PictureBoxBarcodeTicket
        '
        Me.PictureBoxBarcodeTicket.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxBarcodeTicket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxBarcodeTicket.Location = New System.Drawing.Point(80, 534)
        Me.PictureBoxBarcodeTicket.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxBarcodeTicket.Name = "PictureBoxBarcodeTicket"
        Me.PictureBoxBarcodeTicket.Size = New System.Drawing.Size(25, 32)
        Me.PictureBoxBarcodeTicket.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxBarcodeTicket.TabIndex = 349
        Me.PictureBoxBarcodeTicket.TabStop = False
        '
        'lblFpStatus
        '
        Me.lblFpStatus.AutoSize = True
        Me.lblFpStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFpStatus.Location = New System.Drawing.Point(142, 491)
        Me.lblFpStatus.Name = "lblFpStatus"
        Me.lblFpStatus.Size = New System.Drawing.Size(59, 20)
        Me.lblFpStatus.TabIndex = 352
        Me.lblFpStatus.Text = "Label2"
        '
        'btnCancelFp
        '
        Me.btnCancelFp.Location = New System.Drawing.Point(145, 524)
        Me.btnCancelFp.Name = "btnCancelFp"
        Me.btnCancelFp.Size = New System.Drawing.Size(128, 44)
        Me.btnCancelFp.TabIndex = 355
        Me.btnCancelFp.Text = "btnCancelFp"
        Me.btnCancelFp.UseVisualStyleBackColor = True
        '
        'reprint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1031, 601)
        Me.Controls.Add(Me.btnCancelFp)
        Me.Controls.Add(Me.lblFpStatus)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.PictureBoxBarcodeTicket)
        Me.Controls.Add(Me.LabelORValue)
        Me.Controls.Add(LabelTicket)
        Me.Controls.Add(Me.ComboReaderNames)
        Me.Controls.Add(Me.TextBoxUserID)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LabelTimeValue)
        Me.Controls.Add(Me.LabelDateValue)
        Me.Controls.Add(Me.LabelCashierValue)
        Me.Controls.Add(LabelTime)
        Me.Controls.Add(LabelDate)
        Me.Controls.Add(LabelCashierName)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.LabelSearchPassenger)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ButtonReprint)
        Me.Controls.Add(Me.PictureBoxBarcode)
        Me.Controls.Add(Me.PictureBoxLogoReceipt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "reprint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "reprint"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLogoReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxBarcodeTicket, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelSearchPassenger As Label
    Friend WithEvents TextBoxSearch As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ButtonReprint As Button
    Friend WithEvents PictureBoxBarcode As PictureBox
    Friend WithEvents PictureBoxLogoReceipt As PictureBox
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents ButtonClose As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LabelTimeValue As Label
    Friend WithEvents LabelDateValue As Label
    Friend WithEvents LabelCashierValue As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ComboReaderNames As ComboBox
    Friend WithEvents TextBoxUserID As TextBox
    Friend WithEvents LabelORValue As Label
    Friend WithEvents PictureBoxBarcodeTicket As PictureBox
    Friend WithEvents lblFpStatus As Label
    Friend WithEvents btnCancelFp As Button
End Class
