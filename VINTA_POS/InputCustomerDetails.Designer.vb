<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InputCustomerDetails
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
        Me.GroupBoxGuestDetails = New System.Windows.Forms.GroupBox()
        Me.TextBoxGuestTIN = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBoxGuestID = New System.Windows.Forms.TextBox()
        Me.TextBoxGuestAddress = New System.Windows.Forms.TextBox()
        Me.TextBoxGuestName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBoxGuestDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxGuestDetails
        '
        Me.GroupBoxGuestDetails.Controls.Add(Me.TextBoxGuestTIN)
        Me.GroupBoxGuestDetails.Controls.Add(Me.Label4)
        Me.GroupBoxGuestDetails.Controls.Add(Me.Button1)
        Me.GroupBoxGuestDetails.Controls.Add(Me.TextBoxGuestID)
        Me.GroupBoxGuestDetails.Controls.Add(Me.TextBoxGuestAddress)
        Me.GroupBoxGuestDetails.Controls.Add(Me.TextBoxGuestName)
        Me.GroupBoxGuestDetails.Controls.Add(Me.Label3)
        Me.GroupBoxGuestDetails.Controls.Add(Me.Label2)
        Me.GroupBoxGuestDetails.Controls.Add(Me.Label1)
        Me.GroupBoxGuestDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxGuestDetails.Location = New System.Drawing.Point(8, 8)
        Me.GroupBoxGuestDetails.Name = "GroupBoxGuestDetails"
        Me.GroupBoxGuestDetails.Size = New System.Drawing.Size(544, 320)
        Me.GroupBoxGuestDetails.TabIndex = 4
        Me.GroupBoxGuestDetails.TabStop = False
        Me.GroupBoxGuestDetails.Text = "Guest Details"
        '
        'TextBoxGuestTIN
        '
        Me.TextBoxGuestTIN.Location = New System.Drawing.Point(168, 144)
        Me.TextBoxGuestTIN.Name = "TextBoxGuestTIN"
        Me.TextBoxGuestTIN.Size = New System.Drawing.Size(314, 27)
        Me.TextBoxGuestTIN.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(32, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 20)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "TIN No.:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(368, 248)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(114, 64)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "(+)Add"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBoxGuestID
        '
        Me.TextBoxGuestID.Location = New System.Drawing.Point(168, 96)
        Me.TextBoxGuestID.Name = "TextBoxGuestID"
        Me.TextBoxGuestID.Size = New System.Drawing.Size(314, 27)
        Me.TextBoxGuestID.TabIndex = 5
        '
        'TextBoxGuestAddress
        '
        Me.TextBoxGuestAddress.Location = New System.Drawing.Point(168, 192)
        Me.TextBoxGuestAddress.Name = "TextBoxGuestAddress"
        Me.TextBoxGuestAddress.Size = New System.Drawing.Size(314, 27)
        Me.TextBoxGuestAddress.TabIndex = 4
        '
        'TextBoxGuestName
        '
        Me.TextBoxGuestName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.TextBoxGuestName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.TextBoxGuestName.Location = New System.Drawing.Point(168, 48)
        Me.TextBoxGuestName.Name = "TextBoxGuestName"
        Me.TextBoxGuestName.Size = New System.Drawing.Size(314, 27)
        Me.TextBoxGuestName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(32, 200)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Address:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(32, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "ID No.:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Guest Name:"
        '
        'InputCustomerDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(561, 335)
        Me.Controls.Add(Me.GroupBoxGuestDetails)
        Me.Name = "InputCustomerDetails"
        Me.Text = "InputCustomerDetails"
        Me.GroupBoxGuestDetails.ResumeLayout(False)
        Me.GroupBoxGuestDetails.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBoxGuestDetails As GroupBox
    Friend WithEvents TextBoxGuestTIN As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBoxGuestID As TextBox
    Friend WithEvents TextBoxGuestAddress As TextBox
    Friend WithEvents TextBoxGuestName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
