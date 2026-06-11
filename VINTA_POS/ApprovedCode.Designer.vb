<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ApprovedCode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ApprovedCode))
        Me.TextboxApprovedCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonCardApprovedCode = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblPayment_Type = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.labelCardNumber = New System.Windows.Forms.Label()
        Me.labelCardName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextboxApprovedCode
        '
        Me.TextboxApprovedCode.Location = New System.Drawing.Point(180, 212)
        Me.TextboxApprovedCode.Multiline = True
        Me.TextboxApprovedCode.Name = "TextboxApprovedCode"
        Me.TextboxApprovedCode.Size = New System.Drawing.Size(226, 34)
        Me.TextboxApprovedCode.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 18)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Card No. :"
        '
        'ButtonCardApprovedCode
        '
        Me.ButtonCardApprovedCode.Location = New System.Drawing.Point(283, 262)
        Me.ButtonCardApprovedCode.Name = "ButtonCardApprovedCode"
        Me.ButtonCardApprovedCode.Size = New System.Drawing.Size(118, 55)
        Me.ButtonCardApprovedCode.TabIndex = 3
        Me.ButtonCardApprovedCode.Text = "Apply"
        Me.ButtonCardApprovedCode.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblPayment_Type)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ButtonCardApprovedCode)
        Me.GroupBox1.Controls.Add(Me.labelCardNumber)
        Me.GroupBox1.Controls.Add(Me.labelCardName)
        Me.GroupBox1.Controls.Add(Me.TextboxApprovedCode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(19, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(427, 335)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Invoice No.:"
        '
        'lblPayment_Type
        '
        Me.lblPayment_Type.AutoSize = True
        Me.lblPayment_Type.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayment_Type.Location = New System.Drawing.Point(181, 59)
        Me.lblPayment_Type.Name = "lblPayment_Type"
        Me.lblPayment_Type.Size = New System.Drawing.Size(114, 18)
        Me.lblPayment_Type.TabIndex = 7
        Me.lblPayment_Type.Text = "Payment Type"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(28, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Payment Type:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(28, 220)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "T.Invoice #:"
        '
        'labelCardNumber
        '
        Me.labelCardNumber.AutoSize = True
        Me.labelCardNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCardNumber.Location = New System.Drawing.Point(181, 101)
        Me.labelCardNumber.Name = "labelCardNumber"
        Me.labelCardNumber.Size = New System.Drawing.Size(63, 18)
        Me.labelCardNumber.TabIndex = 4
        Me.labelCardNumber.Text = "No Data"
        '
        'labelCardName
        '
        Me.labelCardName.AutoSize = True
        Me.labelCardName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCardName.Location = New System.Drawing.Point(181, 146)
        Me.labelCardName.Name = "labelCardName"
        Me.labelCardName.Size = New System.Drawing.Size(63, 18)
        Me.labelCardName.TabIndex = 3
        Me.labelCardName.Text = "No Data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 18)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Card Name :"
        '
        'ApprovedCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 391)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ApprovedCode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TextboxApprovedCode As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ButtonCardApprovedCode As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents labelCardNumber As Label
    Friend WithEvents labelCardName As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblPayment_Type As Label
    Friend WithEvents Label5 As Label
End Class
