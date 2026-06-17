<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrepaidForm
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
        Me.TextBoxCode = New System.Windows.Forms.TextBox()
        Me.ComboBoxPackage = New System.Windows.Forms.ComboBox()
        Me.TextBoxRideType = New System.Windows.Forms.TextBox()
        Me.TextBoxQuantity = New System.Windows.Forms.TextBox()
        Me.TextBoxAmount = New System.Windows.Forms.TextBox()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextBoxCode
        '
        Me.TextBoxCode.Location = New System.Drawing.Point(299, 40)
        Me.TextBoxCode.Name = "TextBoxCode"
        Me.TextBoxCode.ReadOnly = True
        Me.TextBoxCode.Size = New System.Drawing.Size(473, 22)
        Me.TextBoxCode.TabIndex = 0
        '
        'ComboBoxPackage
        '
        Me.ComboBoxPackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPackage.FormattingEnabled = True
        Me.ComboBoxPackage.Location = New System.Drawing.Point(299, 148)
        Me.ComboBoxPackage.Name = "ComboBoxPackage"
        Me.ComboBoxPackage.Size = New System.Drawing.Size(473, 24)
        Me.ComboBoxPackage.TabIndex = 1
        '
        'TextBoxRideType
        '
        Me.TextBoxRideType.Location = New System.Drawing.Point(299, 93)
        Me.TextBoxRideType.Name = "TextBoxRideType"
        Me.TextBoxRideType.ReadOnly = True
        Me.TextBoxRideType.Size = New System.Drawing.Size(473, 22)
        Me.TextBoxRideType.TabIndex = 2
        '
        'TextBoxQuantity
        '
        Me.TextBoxQuantity.Location = New System.Drawing.Point(299, 208)
        Me.TextBoxQuantity.Name = "TextBoxQuantity"
        Me.TextBoxQuantity.ReadOnly = True
        Me.TextBoxQuantity.Size = New System.Drawing.Size(473, 22)
        Me.TextBoxQuantity.TabIndex = 3
        '
        'TextBoxAmount
        '
        Me.TextBoxAmount.Location = New System.Drawing.Point(299, 274)
        Me.TextBoxAmount.Name = "TextBoxAmount"
        Me.TextBoxAmount.ReadOnly = True
        Me.TextBoxAmount.Size = New System.Drawing.Size(473, 22)
        Me.TextBoxAmount.TabIndex = 4
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(333, 337)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(420, 86)
        Me.ButtonSave.TabIndex = 5
        Me.ButtonSave.Text = "Register Prepaid Card"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(54, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "CARD CODE:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(54, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "STATUS:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(54, 155)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "PACKAGE:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(54, 211)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 17)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "QUANTITY:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(54, 279)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "AMOUNT:"
        '
        'PrepaidForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 450)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.TextBoxAmount)
        Me.Controls.Add(Me.TextBoxQuantity)
        Me.Controls.Add(Me.TextBoxRideType)
        Me.Controls.Add(Me.ComboBoxPackage)
        Me.Controls.Add(Me.TextBoxCode)
        Me.Name = "PrepaidForm"
        Me.Text = "PrepaidForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxCode As TextBox
    Friend WithEvents ComboBoxPackage As ComboBox
    Friend WithEvents TextBoxRideType As TextBox
    Friend WithEvents TextBoxQuantity As TextBox
    Friend WithEvents TextBoxAmount As TextBox
    Friend WithEvents ButtonSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
