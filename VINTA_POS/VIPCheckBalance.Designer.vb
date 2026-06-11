<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VIPCheckBalance
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCardNumber = New System.Windows.Forms.TextBox()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.LabelBalance = New System.Windows.Forms.Label()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.LabelCustomerNo = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(207, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "VIP Card Number:"
        '
        'TextBoxCardNumber
        '
        Me.TextBoxCardNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCardNumber.Location = New System.Drawing.Point(242, 58)
        Me.TextBoxCardNumber.Name = "TextBoxCardNumber"
        Me.TextBoxCardNumber.Size = New System.Drawing.Size(332, 45)
        Me.TextBoxCardNumber.TabIndex = 1
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSearch.Location = New System.Drawing.Point(594, 63)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(131, 40)
        Me.ButtonSearch.TabIndex = 2
        Me.ButtonSearch.Text = "Check"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'LabelBalance
        '
        Me.LabelBalance.AutoSize = True
        Me.LabelBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelBalance.Location = New System.Drawing.Point(245, 261)
        Me.LabelBalance.Name = "LabelBalance"
        Me.LabelBalance.Size = New System.Drawing.Size(136, 52)
        Me.LabelBalance.TabIndex = 3
        Me.LabelBalance.Text = "₱0.00"
        '
        'ButtonClose
        '
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.Location = New System.Drawing.Point(658, 320)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(98, 54)
        Me.ButtonClose.TabIndex = 4
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Location = New System.Drawing.Point(429, 179)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(0, 17)
        Me.LabelStatus.TabIndex = 5
        '
        'LabelName
        '
        Me.LabelName.AutoSize = True
        Me.LabelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelName.Location = New System.Drawing.Point(245, 118)
        Me.LabelName.Name = "LabelName"
        Me.LabelName.Size = New System.Drawing.Size(153, 52)
        Me.LabelName.TabIndex = 6
        Me.LabelName.Text = "Label2"
        '
        'LabelCustomerNo
        '
        Me.LabelCustomerNo.AutoSize = True
        Me.LabelCustomerNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCustomerNo.Location = New System.Drawing.Point(245, 192)
        Me.LabelCustomerNo.Name = "LabelCustomerNo"
        Me.LabelCustomerNo.Size = New System.Drawing.Size(153, 52)
        Me.LabelCustomerNo.TabIndex = 7
        Me.LabelCustomerNo.Text = "Label3"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.LabelBalance)
        Me.GroupBox1.Controls.Add(Me.LabelCustomerNo)
        Me.GroupBox1.Controls.Add(Me.ButtonClose)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.LabelName)
        Me.GroupBox1.Controls.Add(Me.ButtonSearch)
        Me.GroupBox1.Controls.Add(Me.TextBoxCardNumber)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(762, 386)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Check Balance"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(110, 279)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 29)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Balance:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(49, 210)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(167, 29)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Customer No.:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(132, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 29)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Name:"
        '
        'VIPCheckBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(782, 406)
        Me.Controls.Add(Me.LabelStatus)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "VIPCheckBalance"
        Me.Text = "VIPCheckBalance"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxCardNumber As TextBox
    Friend WithEvents ButtonSearch As Button
    Friend WithEvents LabelBalance As Label
    Friend WithEvents ButtonClose As Button
    Friend WithEvents LabelStatus As Label
    Friend WithEvents LabelName As Label
    Friend WithEvents LabelCustomerNo As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
End Class
