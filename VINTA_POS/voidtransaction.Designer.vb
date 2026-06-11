<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class voidtransaction
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(voidtransaction))
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonVoid = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxORNumber = New System.Windows.Forms.TextBox()
        Me.LabelORNumber = New System.Windows.Forms.Label()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PictureBoxBarcode = New System.Windows.Forms.PictureBox()
        Me.PictureBoxLogoReceipt = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ComboReaderNames = New System.Windows.Forms.ComboBox()
        Me.TextBoxUserID = New System.Windows.Forms.TextBox()
        Me.LabelVoidNo = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.voidedOR = New System.Windows.Forms.Label()
        Me.lblFpStatus = New System.Windows.Forms.Label()
        Me.btnCancelFp = New System.Windows.Forms.Button()
        Me.TextBoxResetCounter = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PictureBoxBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLogoReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCancel.Location = New System.Drawing.Point(114, 248)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(156, 60)
        Me.ButtonCancel.TabIndex = 9
        Me.ButtonCancel.Text = "CANCEL"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonVoid
        '
        Me.ButtonVoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonVoid.Location = New System.Drawing.Point(474, 27)
        Me.ButtonVoid.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonVoid.Name = "ButtonVoid"
        Me.ButtonVoid.Size = New System.Drawing.Size(110, 60)
        Me.ButtonVoid.TabIndex = 8
        Me.ButtonVoid.Text = "VOID"
        Me.ButtonVoid.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(129, 42)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(326, 36)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "VOID TRANSACTION"
        '
        'TextBoxORNumber
        '
        Me.TextBoxORNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxORNumber.Location = New System.Drawing.Point(75, 206)
        Me.TextBoxORNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxORNumber.Name = "TextBoxORNumber"
        Me.TextBoxORNumber.Size = New System.Drawing.Size(422, 34)
        Me.TextBoxORNumber.TabIndex = 6
        '
        'LabelORNumber
        '
        Me.LabelORNumber.AutoSize = True
        Me.LabelORNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelORNumber.Location = New System.Drawing.Point(69, 178)
        Me.LabelORNumber.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelORNumber.Name = "LabelORNumber"
        Me.LabelORNumber.Size = New System.Drawing.Size(121, 25)
        Me.LabelORNumber.TabIndex = 5
        Me.LabelORNumber.Text = "SI Number:"
        '
        'PrintDocument1
        '
        '
        'PictureBoxBarcode
        '
        Me.PictureBoxBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxBarcode.Location = New System.Drawing.Point(12, 42)
        Me.PictureBoxBarcode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxBarcode.Name = "PictureBoxBarcode"
        Me.PictureBoxBarcode.Size = New System.Drawing.Size(30, 33)
        Me.PictureBoxBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxBarcode.TabIndex = 180
        Me.PictureBoxBarcode.TabStop = False
        Me.PictureBoxBarcode.Visible = False
        '
        'PictureBoxLogoReceipt
        '
        Me.PictureBoxLogoReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxLogoReceipt.Location = New System.Drawing.Point(61, 42)
        Me.PictureBoxLogoReceipt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxLogoReceipt.Name = "PictureBoxLogoReceipt"
        Me.PictureBoxLogoReceipt.Size = New System.Drawing.Size(41, 33)
        Me.PictureBoxLogoReceipt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxLogoReceipt.TabIndex = 179
        Me.PictureBoxLogoReceipt.TabStop = False
        Me.PictureBoxLogoReceipt.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(584, 26)
        Me.Panel1.TabIndex = 213
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Location = New System.Drawing.Point(0, 398)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(584, 26)
        Me.Panel2.TabIndex = 214
        '
        'ComboReaderNames
        '
        Me.ComboReaderNames.FormattingEnabled = True
        Me.ComboReaderNames.Location = New System.Drawing.Point(474, 95)
        Me.ComboReaderNames.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboReaderNames.Name = "ComboReaderNames"
        Me.ComboReaderNames.Size = New System.Drawing.Size(66, 24)
        Me.ComboReaderNames.TabIndex = 216
        '
        'TextBoxUserID
        '
        Me.TextBoxUserID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxUserID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxUserID.Location = New System.Drawing.Point(156, 100)
        Me.TextBoxUserID.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxUserID.Name = "TextBoxUserID"
        Me.TextBoxUserID.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxUserID.Size = New System.Drawing.Size(257, 30)
        Me.TextBoxUserID.TabIndex = 215
        Me.TextBoxUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LabelVoidNo
        '
        Me.LabelVoidNo.AutoSize = True
        Me.LabelVoidNo.Location = New System.Drawing.Point(12, 357)
        Me.LabelVoidNo.Name = "LabelVoidNo"
        Me.LabelVoidNo.Size = New System.Drawing.Size(54, 17)
        Me.LabelVoidNo.TabIndex = 217
        Me.LabelVoidNo.Text = "VoidNo"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(277, 248)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(171, 60)
        Me.Button1.TabIndex = 218
        Me.Button1.Text = "VOID"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(114, 315)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(334, 59)
        Me.Button2.TabIndex = 219
        Me.Button2.Text = "Print Voided Invoice"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'voidedOR
        '
        Me.voidedOR.AutoSize = True
        Me.voidedOR.Location = New System.Drawing.Point(12, 377)
        Me.voidedOR.Name = "voidedOR"
        Me.voidedOR.Size = New System.Drawing.Size(71, 17)
        Me.voidedOR.TabIndex = 220
        Me.voidedOR.Text = "voidedOR"
        '
        'lblFpStatus
        '
        Me.lblFpStatus.AutoSize = True
        Me.lblFpStatus.Location = New System.Drawing.Point(132, 146)
        Me.lblFpStatus.Name = "lblFpStatus"
        Me.lblFpStatus.Size = New System.Drawing.Size(78, 17)
        Me.lblFpStatus.TabIndex = 221
        Me.lblFpStatus.Text = "lblFpStatus"
        '
        'btnCancelFp
        '
        Me.btnCancelFp.Location = New System.Drawing.Point(8, 315)
        Me.btnCancelFp.Name = "btnCancelFp"
        Me.btnCancelFp.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelFp.TabIndex = 222
        Me.btnCancelFp.Text = "btnCancelFp"
        Me.btnCancelFp.UseVisualStyleBackColor = True
        '
        'TextBoxResetCounter
        '
        Me.TextBoxResetCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxResetCounter.Location = New System.Drawing.Point(499, 372)
        Me.TextBoxResetCounter.Name = "TextBoxResetCounter"
        Me.TextBoxResetCounter.Size = New System.Drawing.Size(72, 21)
        Me.TextBoxResetCounter.TabIndex = 223
        Me.TextBoxResetCounter.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 4.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(497, 357)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 12)
        Me.Label1.TabIndex = 224
        Me.Label1.Text = "Reset Count:"
        '
        'voidtransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 423)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxResetCounter)
        Me.Controls.Add(Me.btnCancelFp)
        Me.Controls.Add(Me.lblFpStatus)
        Me.Controls.Add(Me.voidedOR)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LabelVoidNo)
        Me.Controls.Add(Me.ComboReaderNames)
        Me.Controls.Add(Me.TextBoxUserID)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBoxBarcode)
        Me.Controls.Add(Me.PictureBoxLogoReceipt)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonVoid)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxORNumber)
        Me.Controls.Add(Me.LabelORNumber)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "voidtransaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "voidtransaction"
        CType(Me.PictureBoxBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLogoReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonVoid As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxORNumber As TextBox
    Friend WithEvents LabelORNumber As Label
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PictureBoxBarcode As PictureBox
    Friend WithEvents PictureBoxLogoReceipt As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ComboReaderNames As ComboBox
    Friend WithEvents TextBoxUserID As TextBox
    Friend WithEvents LabelVoidNo As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents voidedOR As Label
    Friend WithEvents lblFpStatus As Label
    Friend WithEvents btnCancelFp As Button
    Friend WithEvents TextBoxResetCounter As TextBox
    Friend WithEvents Label1 As Label
End Class
