<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CardDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CardDetails))
        Me.TextBoxCardNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonCardDetails = New System.Windows.Forms.Button()
        Me.TextBoxCardName = New System.Windows.Forms.TextBox()
        Me.TextBoxExpiry = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTrack1 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPayment = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxCardNo
        '
        Me.TextBoxCardNo.Location = New System.Drawing.Point(179, 80)
        Me.TextBoxCardNo.Multiline = True
        Me.TextBoxCardNo.Name = "TextBoxCardNo"
        Me.TextBoxCardNo.Size = New System.Drawing.Size(329, 38)
        Me.TextBoxCardNo.TabIndex = 0
        Me.TextBoxCardNo.UseWaitCursor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(31, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Card No.:"
        Me.Label1.UseWaitCursor = True
        '
        'ButtonCardDetails
        '
        Me.ButtonCardDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCardDetails.Location = New System.Drawing.Point(358, 187)
        Me.ButtonCardDetails.Name = "ButtonCardDetails"
        Me.ButtonCardDetails.Size = New System.Drawing.Size(150, 72)
        Me.ButtonCardDetails.TabIndex = 2
        Me.ButtonCardDetails.Text = "Apply Details"
        Me.ButtonCardDetails.UseVisualStyleBackColor = True
        Me.ButtonCardDetails.UseWaitCursor = True
        '
        'TextBoxCardName
        '
        Me.TextBoxCardName.Location = New System.Drawing.Point(179, 131)
        Me.TextBoxCardName.Multiline = True
        Me.TextBoxCardName.Name = "TextBoxCardName"
        Me.TextBoxCardName.Size = New System.Drawing.Size(329, 38)
        Me.TextBoxCardName.TabIndex = 3
        Me.TextBoxCardName.UseWaitCursor = True
        '
        'TextBoxExpiry
        '
        Me.TextBoxExpiry.Location = New System.Drawing.Point(179, 187)
        Me.TextBoxExpiry.Multiline = True
        Me.TextBoxExpiry.Name = "TextBoxExpiry"
        Me.TextBoxExpiry.Size = New System.Drawing.Size(150, 38)
        Me.TextBoxExpiry.TabIndex = 4
        Me.TextBoxExpiry.UseWaitCursor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblPayment)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ButtonCardDetails)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBoxCardNo)
        Me.GroupBox1.Controls.Add(Me.TextBoxExpiry)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBoxCardName)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(23, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(538, 294)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Card Details: "
        Me.GroupBox1.UseWaitCursor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(34, 195)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Expiry Date:"
        Me.Label3.UseWaitCursor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(31, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Card Name:"
        Me.Label2.UseWaitCursor = True
        '
        'txtTrack1
        '
        Me.txtTrack1.Location = New System.Drawing.Point(33, 4)
        Me.txtTrack1.Multiline = True
        Me.txtTrack1.Name = "txtTrack1"
        Me.txtTrack1.Size = New System.Drawing.Size(69, 38)
        Me.txtTrack1.TabIndex = 7
        Me.txtTrack1.UseWaitCursor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(108, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 17)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(165, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 17)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Label6"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(34, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Payment Type:"
        Me.Label4.UseWaitCursor = True
        '
        'lblPayment
        '
        Me.lblPayment.AutoSize = True
        Me.lblPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayment.Location = New System.Drawing.Point(175, 38)
        Me.lblPayment.Name = "lblPayment"
        Me.lblPayment.Size = New System.Drawing.Size(131, 20)
        Me.lblPayment.TabIndex = 8
        Me.lblPayment.Text = "Payment_Type"
        Me.lblPayment.UseWaitCursor = True
        '
        'CardDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 354)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtTrack1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CardDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxCardNo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ButtonCardDetails As Button
    Friend WithEvents TextBoxCardName As TextBox
    Friend WithEvents TextBoxExpiry As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtTrack1 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblPayment As Label
    Friend WithEvents Label4 As Label
End Class
