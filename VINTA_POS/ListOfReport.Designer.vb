<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ListOfReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ListOfReport))
        Me.ButtonCashFloat = New System.Windows.Forms.Button()
        Me.ButtonCashdrop = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.btnCancelFp = New System.Windows.Forms.Button()
        Me.ButtonPrintCashFloatDrop = New System.Windows.Forms.Button()
        Me.lblFpStatus = New System.Windows.Forms.Label()
        Me.ComboBoxReason = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabelCashierID_Request = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxApprovedBy = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextboxCash = New System.Windows.Forms.TextBox()
        Me.PrintPreviewDialogCashINOUT = New System.Windows.Forms.PrintPreviewDialog()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonCashFloat
        '
        Me.ButtonCashFloat.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCashFloat.Location = New System.Drawing.Point(12, 37)
        Me.ButtonCashFloat.Name = "ButtonCashFloat"
        Me.ButtonCashFloat.Size = New System.Drawing.Size(189, 83)
        Me.ButtonCashFloat.TabIndex = 0
        Me.ButtonCashFloat.Text = "CASH FLOAT"
        Me.ButtonCashFloat.UseVisualStyleBackColor = True
        '
        'ButtonCashdrop
        '
        Me.ButtonCashdrop.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCashdrop.Location = New System.Drawing.Point(207, 37)
        Me.ButtonCashdrop.Name = "ButtonCashdrop"
        Me.ButtonCashdrop.Size = New System.Drawing.Size(189, 83)
        Me.ButtonCashdrop.TabIndex = 1
        Me.ButtonCashdrop.Text = "CASH DROP"
        Me.ButtonCashdrop.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(32, 269)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(71, 38)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "X Reading"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonClose)
        Me.GroupBox1.Controls.Add(Me.btnCancelFp)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.ButtonPrintCashFloatDrop)
        Me.GroupBox1.Controls.Add(Me.lblFpStatus)
        Me.GroupBox1.Controls.Add(Me.ComboBoxReason)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.LabelCashierID_Request)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBoxApprovedBy)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextboxCash)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 119)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(384, 357)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'ButtonClose
        '
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.Location = New System.Drawing.Point(119, 281)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(125, 59)
        Me.ButtonClose.TabIndex = 5
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'btnCancelFp
        '
        Me.btnCancelFp.Location = New System.Drawing.Point(32, 313)
        Me.btnCancelFp.Name = "btnCancelFp"
        Me.btnCancelFp.Size = New System.Drawing.Size(75, 38)
        Me.btnCancelFp.TabIndex = 6
        Me.btnCancelFp.Text = "Button1"
        Me.btnCancelFp.UseVisualStyleBackColor = True
        '
        'ButtonPrintCashFloatDrop
        '
        Me.ButtonPrintCashFloatDrop.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPrintCashFloatDrop.Location = New System.Drawing.Point(250, 281)
        Me.ButtonPrintCashFloatDrop.Name = "ButtonPrintCashFloatDrop"
        Me.ButtonPrintCashFloatDrop.Size = New System.Drawing.Size(125, 59)
        Me.ButtonPrintCashFloatDrop.TabIndex = 5
        Me.ButtonPrintCashFloatDrop.Text = "Print Receipt"
        Me.ButtonPrintCashFloatDrop.UseVisualStyleBackColor = True
        '
        'lblFpStatus
        '
        Me.lblFpStatus.AutoSize = True
        Me.lblFpStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFpStatus.Location = New System.Drawing.Point(21, 240)
        Me.lblFpStatus.Name = "lblFpStatus"
        Me.lblFpStatus.Size = New System.Drawing.Size(51, 18)
        Me.lblFpStatus.TabIndex = 5
        Me.lblFpStatus.Text = "Label4"
        '
        'ComboBoxReason
        '
        Me.ComboBoxReason.FormattingEnabled = True
        Me.ComboBoxReason.Location = New System.Drawing.Point(171, 133)
        Me.ComboBoxReason.Name = "ComboBoxReason"
        Me.ComboBoxReason.Size = New System.Drawing.Size(182, 24)
        Me.ComboBoxReason.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(19, 133)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 25)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Reason:"
        '
        'LabelCashierID_Request
        '
        Me.LabelCashierID_Request.AutoSize = True
        Me.LabelCashierID_Request.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCashierID_Request.Location = New System.Drawing.Point(168, 38)
        Me.LabelCashierID_Request.Name = "LabelCashierID_Request"
        Me.LabelCashierID_Request.Size = New System.Drawing.Size(81, 18)
        Me.LabelCashierID_Request.TabIndex = 9
        Me.LabelCashierID_Request.Text = "Cashier_ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 25)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Cashier ID:"
        '
        'TextBoxApprovedBy
        '
        Me.TextBoxApprovedBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxApprovedBy.Location = New System.Drawing.Point(171, 187)
        Me.TextBoxApprovedBy.Name = "TextBoxApprovedBy"
        Me.TextBoxApprovedBy.Size = New System.Drawing.Size(161, 30)
        Me.TextBoxApprovedBy.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 190)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 25)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Approved By:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 25)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Enter Amount:"
        '
        'TextboxCash
        '
        Me.TextboxCash.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextboxCash.Location = New System.Drawing.Point(171, 81)
        Me.TextboxCash.Name = "TextboxCash"
        Me.TextboxCash.Size = New System.Drawing.Size(122, 30)
        Me.TextboxCash.TabIndex = 0
        '
        'PrintPreviewDialogCashINOUT
        '
        Me.PrintPreviewDialogCashINOUT.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialogCashINOUT.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialogCashINOUT.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialogCashINOUT.Enabled = True
        Me.PrintPreviewDialogCashINOUT.Icon = CType(resources.GetObject("PrintPreviewDialogCashINOUT.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialogCashINOUT.Name = "PrintPreviewDialogCashINOUT"
        Me.PrintPreviewDialogCashINOUT.Visible = False
        '
        'ListOfReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(415, 489)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonCashdrop)
        Me.Controls.Add(Me.ButtonCashFloat)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ListOfReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cash Management"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonCashFloat As Button
    Friend WithEvents ButtonCashdrop As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextboxCash As TextBox
    Friend WithEvents TextBoxApprovedBy As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ButtonPrintCashFloatDrop As Button
    Friend WithEvents ComboBoxReason As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents LabelCashierID_Request As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents PrintPreviewDialogCashINOUT As PrintPreviewDialog
    Friend WithEvents btnCancelFp As Button
    Friend WithEvents lblFpStatus As Label
    Friend WithEvents ButtonClose As Button
End Class
