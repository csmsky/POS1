<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cashierreport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(cashierreport))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DateTimePickerXReport = New System.Windows.Forms.DateTimePicker()
        Me.ButtonPrintXReport = New System.Windows.Forms.Button()
        Me.LabelXREADING = New System.Windows.Forms.Label()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(509, 26)
        Me.Panel1.TabIndex = 213
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Location = New System.Drawing.Point(0, 352)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(509, 26)
        Me.Panel2.TabIndex = 214
        '
        'DateTimePickerXReport
        '
        Me.DateTimePickerXReport.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerXReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerXReport.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerXReport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DateTimePickerXReport.Location = New System.Drawing.Point(95, 116)
        Me.DateTimePickerXReport.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerXReport.Name = "DateTimePickerXReport"
        Me.DateTimePickerXReport.Size = New System.Drawing.Size(311, 38)
        Me.DateTimePickerXReport.TabIndex = 216
        Me.DateTimePickerXReport.Value = New Date(2018, 12, 2, 0, 0, 0, 0)
        '
        'ButtonPrintXReport
        '
        Me.ButtonPrintXReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPrintXReport.Location = New System.Drawing.Point(95, 192)
        Me.ButtonPrintXReport.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonPrintXReport.Name = "ButtonPrintXReport"
        Me.ButtonPrintXReport.Size = New System.Drawing.Size(311, 55)
        Me.ButtonPrintXReport.TabIndex = 215
        Me.ButtonPrintXReport.Text = "Print X-Report"
        Me.ButtonPrintXReport.UseVisualStyleBackColor = True
        '
        'LabelXREADING
        '
        Me.LabelXREADING.AutoSize = True
        Me.LabelXREADING.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelXREADING.Location = New System.Drawing.Point(153, 50)
        Me.LabelXREADING.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelXREADING.Name = "LabelXREADING"
        Me.LabelXREADING.Size = New System.Drawing.Size(192, 36)
        Me.LabelXREADING.TabIndex = 217
        Me.LabelXREADING.Text = "X-READING"
        '
        'PrintDocument1
        '
        '
        'ButtonClose
        '
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.Location = New System.Drawing.Point(95, 255)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(311, 55)
        Me.ButtonClose.TabIndex = 218
        Me.ButtonClose.Text = "CLOSE"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'cashierreport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 377)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.LabelXREADING)
        Me.Controls.Add(Me.DateTimePickerXReport)
        Me.Controls.Add(Me.ButtonPrintXReport)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "cashierreport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "cashierreport"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DateTimePickerXReport As DateTimePicker
    Friend WithEvents ButtonPrintXReport As Button
    Friend WithEvents LabelXREADING As Label
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents ButtonClose As Button
End Class
