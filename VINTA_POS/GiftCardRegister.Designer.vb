<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GiftCardRegister
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
        Me.ButtonPrepaid = New System.Windows.Forms.Button()
        Me.ButtonVIP = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonPrepaid
        '
        Me.ButtonPrepaid.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPrepaid.Location = New System.Drawing.Point(25, 36)
        Me.ButtonPrepaid.Name = "ButtonPrepaid"
        Me.ButtonPrepaid.Size = New System.Drawing.Size(167, 116)
        Me.ButtonPrepaid.TabIndex = 0
        Me.ButtonPrepaid.Text = "Prepaid"
        Me.ButtonPrepaid.UseVisualStyleBackColor = True
        '
        'ButtonVIP
        '
        Me.ButtonVIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonVIP.Location = New System.Drawing.Point(213, 36)
        Me.ButtonVIP.Name = "ButtonVIP"
        Me.ButtonVIP.Size = New System.Drawing.Size(170, 116)
        Me.ButtonVIP.TabIndex = 1
        Me.ButtonVIP.Text = "VIP"
        Me.ButtonVIP.UseVisualStyleBackColor = True
        '
        'GiftCardRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 192)
        Me.Controls.Add(Me.ButtonVIP)
        Me.Controls.Add(Me.ButtonPrepaid)
        Me.Name = "GiftCardRegister"
        Me.Text = "GiftCardRegister"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonPrepaid As Button
    Friend WithEvents ButtonVIP As Button
End Class
