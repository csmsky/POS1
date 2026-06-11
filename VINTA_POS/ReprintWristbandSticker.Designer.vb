<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReprintWristbandSticker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReprintWristbandSticker))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.labelGuestID = New System.Windows.Forms.Label()
        Me.LabelDT = New System.Windows.Forms.Label()
        Me.LabelType = New System.Windows.Forms.Label()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonReprint = New System.Windows.Forms.Button()
        Me.LabelCode = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(17, 73)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(641, 222)
        Me.DataGridView1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(326, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Search:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(406, 21)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(251, 30)
        Me.TextBox1.TabIndex = 2
        '
        'labelGuestID
        '
        Me.labelGuestID.AutoSize = True
        Me.labelGuestID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelGuestID.Location = New System.Drawing.Point(107, 313)
        Me.labelGuestID.Name = "labelGuestID"
        Me.labelGuestID.Size = New System.Drawing.Size(66, 18)
        Me.labelGuestID.TabIndex = 3
        Me.labelGuestID.Text = "Guest ID"
        '
        'LabelDT
        '
        Me.LabelDT.AutoSize = True
        Me.LabelDT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDT.Location = New System.Drawing.Point(469, 350)
        Me.LabelDT.Name = "LabelDT"
        Me.LabelDT.Size = New System.Drawing.Size(76, 18)
        Me.LabelDT.TabIndex = 4
        Me.LabelDT.Text = "Date/Time"
        '
        'LabelType
        '
        Me.LabelType.AutoSize = True
        Me.LabelType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelType.Location = New System.Drawing.Point(103, 351)
        Me.LabelType.Name = "LabelType"
        Me.LabelType.Size = New System.Drawing.Size(142, 18)
        Me.LabelType.TabIndex = 5
        Me.LabelType.Text = "Type Of Transaction"
        '
        'ButtonClose
        '
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.Location = New System.Drawing.Point(399, 394)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(124, 51)
        Me.ButtonClose.TabIndex = 6
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ButtonReprint
        '
        Me.ButtonReprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonReprint.Location = New System.Drawing.Point(529, 394)
        Me.ButtonReprint.Name = "ButtonReprint"
        Me.ButtonReprint.Size = New System.Drawing.Size(124, 51)
        Me.ButtonReprint.TabIndex = 7
        Me.ButtonReprint.Text = "Reprint"
        Me.ButtonReprint.UseVisualStyleBackColor = True
        '
        'LabelCode
        '
        Me.LabelCode.AutoSize = True
        Me.LabelCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCode.Location = New System.Drawing.Point(467, 315)
        Me.LabelCode.Name = "LabelCode"
        Me.LabelCode.Size = New System.Drawing.Size(78, 18)
        Me.LabelCode.TabIndex = 8
        Me.LabelCode.Text = "Ride Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 349)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 18)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Type :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 312)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 18)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Guest ID :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(354, 313)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 18)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Ride Code :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(353, 350)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 18)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Date/Time :"
        '
        'ReprintWristbandSticker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 469)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LabelCode)
        Me.Controls.Add(Me.ButtonReprint)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.LabelType)
        Me.Controls.Add(Me.LabelDT)
        Me.Controls.Add(Me.labelGuestID)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ReprintWristbandSticker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reprint Sticker"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents labelGuestID As Label
    Friend WithEvents LabelDT As Label
    Friend WithEvents LabelType As Label
    Friend WithEvents ButtonClose As Button
    Friend WithEvents ButtonReprint As Button
    Friend WithEvents LabelCode As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
