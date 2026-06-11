<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreen))
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelCopyright = New System.Windows.Forms.Label()
        Me.LabelManufacturer = New System.Windows.Forms.Label()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.LabelNoCopyright = New System.Windows.Forms.Label()
        Me.LabelSoftwareName = New System.Windows.Forms.Label()
        Me.LabelVersionNo = New System.Windows.Forms.Label()
        Me.LabelPOSno = New System.Windows.Forms.Label()
        Me.LabelPOS = New System.Windows.Forms.Label()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Location = New System.Drawing.Point(80, 28)
        Me.PictureBoxLogo.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(397, 329)
        Me.PictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxLogo.TabIndex = 0
        Me.PictureBoxLogo.TabStop = False
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(3, 462)
        Me.ProgressBar.Margin = New System.Windows.Forms.Padding(4)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(551, 21)
        Me.ProgressBar.TabIndex = 1
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 70
        '
        'LabelCopyright
        '
        Me.LabelCopyright.AutoSize = True
        Me.LabelCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCopyright.Location = New System.Drawing.Point(-1, 426)
        Me.LabelCopyright.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCopyright.Name = "LabelCopyright"
        Me.LabelCopyright.Size = New System.Drawing.Size(72, 17)
        Me.LabelCopyright.TabIndex = 2
        Me.LabelCopyright.Text = "Copyright:"
        '
        'LabelManufacturer
        '
        Me.LabelManufacturer.AutoSize = True
        Me.LabelManufacturer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelManufacturer.Location = New System.Drawing.Point(-1, 442)
        Me.LabelManufacturer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelManufacturer.Name = "LabelManufacturer"
        Me.LabelManufacturer.Size = New System.Drawing.Size(96, 17)
        Me.LabelManufacturer.TabIndex = 3
        Me.LabelManufacturer.Text = "Manufacturer:"
        '
        'LabelName
        '
        Me.LabelName.AutoSize = True
        Me.LabelName.Location = New System.Drawing.Point(104, 442)
        Me.LabelName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelName.Name = "LabelName"
        Me.LabelName.Size = New System.Drawing.Size(85, 17)
        Me.LabelName.TabIndex = 4
        Me.LabelName.Text = "VINTATECH"
        '
        'LabelNoCopyright
        '
        Me.LabelNoCopyright.AutoSize = True
        Me.LabelNoCopyright.Location = New System.Drawing.Point(104, 426)
        Me.LabelNoCopyright.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelNoCopyright.Name = "LabelNoCopyright"
        Me.LabelNoCopyright.Size = New System.Drawing.Size(88, 17)
        Me.LabelNoCopyright.TabIndex = 5
        Me.LabelNoCopyright.Text = "O2018-2409"
        '
        'LabelSoftwareName
        '
        Me.LabelSoftwareName.AutoSize = True
        Me.LabelSoftwareName.Location = New System.Drawing.Point(7, 410)
        Me.LabelSoftwareName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSoftwareName.Name = "LabelSoftwareName"
        Me.LabelSoftwareName.Size = New System.Drawing.Size(0, 17)
        Me.LabelSoftwareName.TabIndex = 6
        '
        'LabelVersionNo
        '
        Me.LabelVersionNo.AutoSize = True
        Me.LabelVersionNo.Location = New System.Drawing.Point(112, 410)
        Me.LabelVersionNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVersionNo.Name = "LabelVersionNo"
        Me.LabelVersionNo.Size = New System.Drawing.Size(0, 17)
        Me.LabelVersionNo.TabIndex = 7
        '
        'LabelPOSno
        '
        Me.LabelPOSno.AutoSize = True
        Me.LabelPOSno.Location = New System.Drawing.Point(525, 442)
        Me.LabelPOSno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPOSno.Name = "LabelPOSno"
        Me.LabelPOSno.Size = New System.Drawing.Size(0, 17)
        Me.LabelPOSno.TabIndex = 9
        '
        'LabelPOS
        '
        Me.LabelPOS.AutoSize = True
        Me.LabelPOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPOS.Location = New System.Drawing.Point(475, 442)
        Me.LabelPOS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPOS.Name = "LabelPOS"
        Me.LabelPOS.Size = New System.Drawing.Size(41, 17)
        Me.LabelPOS.TabIndex = 8
        Me.LabelPOS.Text = "POS:"
        '
        'SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(556, 486)
        Me.Controls.Add(Me.LabelPOSno)
        Me.Controls.Add(Me.LabelPOS)
        Me.Controls.Add(Me.LabelVersionNo)
        Me.Controls.Add(Me.LabelSoftwareName)
        Me.Controls.Add(Me.LabelNoCopyright)
        Me.Controls.Add(Me.LabelName)
        Me.Controls.Add(Me.LabelManufacturer)
        Me.Controls.Add(Me.LabelCopyright)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SplashScreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBoxLogo As PictureBox
    Friend WithEvents ProgressBar As ProgressBar
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelCopyright As Label
    Friend WithEvents LabelManufacturer As Label
    Friend WithEvents LabelName As Label
    Friend WithEvents LabelNoCopyright As Label
    Friend WithEvents LabelSoftwareName As Label
    Friend WithEvents LabelVersionNo As Label
    Friend WithEvents LabelPOSno As Label
    Friend WithEvents LabelPOS As Label
End Class
