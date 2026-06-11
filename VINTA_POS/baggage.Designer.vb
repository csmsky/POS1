<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class baggage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(baggage))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.LabelBaggageWeight = New System.Windows.Forms.Label()
        Me.ComboBoxBaggageWeight = New System.Windows.Forms.ComboBox()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(511, 26)
        Me.Panel1.TabIndex = 214
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Location = New System.Drawing.Point(0, 205)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(511, 26)
        Me.Panel2.TabIndex = 215
        '
        'ButtonOK
        '
        Me.ButtonOK.BackColor = System.Drawing.Color.RoyalBlue
        Me.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOK.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonOK.Location = New System.Drawing.Point(264, 131)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(217, 50)
        Me.ButtonOK.TabIndex = 236
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = False
        '
        'LabelBaggageWeight
        '
        Me.LabelBaggageWeight.AutoSize = True
        Me.LabelBaggageWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelBaggageWeight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelBaggageWeight.Location = New System.Drawing.Point(27, 47)
        Me.LabelBaggageWeight.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBaggageWeight.Name = "LabelBaggageWeight"
        Me.LabelBaggageWeight.Size = New System.Drawing.Size(179, 25)
        Me.LabelBaggageWeight.TabIndex = 258
        Me.LabelBaggageWeight.Text = "Baggage Weight:"
        '
        'ComboBoxBaggageWeight
        '
        Me.ComboBoxBaggageWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxBaggageWeight.FormattingEnabled = True
        Me.ComboBoxBaggageWeight.Items.AddRange(New Object() {"1 - 10 KG", "11 - 20 KG", "21 - 30 KG", "31 - 40 KG", "41 - 50 KG"})
        Me.ComboBoxBaggageWeight.Location = New System.Drawing.Point(32, 75)
        Me.ComboBoxBaggageWeight.Name = "ComboBoxBaggageWeight"
        Me.ComboBoxBaggageWeight.Size = New System.Drawing.Size(449, 37)
        Me.ComboBoxBaggageWeight.TabIndex = 259
        Me.ComboBoxBaggageWeight.Text = "-- Please Choose Baggage Weight --"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.BackColor = System.Drawing.Color.RoyalBlue
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCancel.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonCancel.Location = New System.Drawing.Point(32, 131)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(217, 50)
        Me.ButtonCancel.TabIndex = 260
        Me.ButtonCancel.Text = "CANCEL"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'baggage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(509, 229)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ComboBoxBaggageWeight)
        Me.Controls.Add(Me.LabelBaggageWeight)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "baggage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "  "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ButtonOK As Button
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents LabelBaggageWeight As Label
    Friend WithEvents ComboBoxBaggageWeight As ComboBox
    Friend WithEvents ButtonCancel As Button
End Class
