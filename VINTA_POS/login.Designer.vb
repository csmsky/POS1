<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class login
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(login))
        Me.ButtonLogin = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.PictureBoxCompanyLogo = New System.Windows.Forms.PictureBox()
        Me.PictureBoxVINTALogo = New System.Windows.Forms.PictureBox()
        Me.LabelLogin = New System.Windows.Forms.Label()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.LabelPOSno = New System.Windows.Forms.Label()
        Me.LabelPOS = New System.Windows.Forms.Label()
        Me.LabelDateTime = New System.Windows.Forms.Label()
        Me.TextBoxUserID = New System.Windows.Forms.TextBox()
        Me.ComboReaderNames = New System.Windows.Forms.ComboBox()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.LabelPosition = New System.Windows.Forms.Label()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.LabelVersionNo = New System.Windows.Forms.Label()
        Me.LabelSoftwareName = New System.Windows.Forms.Label()
        Me.LabelHello = New System.Windows.Forms.Label()
        Me.PictureBoxFace = New System.Windows.Forms.PictureBox()
        Me.LabelIndicator = New System.Windows.Forms.Label()
        Me.pbFingerprint = New System.Windows.Forms.PictureBox()
        Me.cmbFinger = New System.Windows.Forms.ComboBox()
        Me.lblFpStatus = New System.Windows.Forms.Label()
        Me.btnScanFinger = New System.Windows.Forms.Button()
        Me.PanelHeader.SuspendLayout()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFooter.SuspendLayout()
        CType(Me.PictureBoxFace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFingerprint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonLogin
        '
        Me.ButtonLogin.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonLogin.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonLogin.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonLogin.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonLogin.Location = New System.Drawing.Point(515, 732)
        Me.ButtonLogin.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonLogin.Name = "ButtonLogin"
        Me.ButtonLogin.Size = New System.Drawing.Size(360, 58)
        Me.ButtonLogin.TabIndex = 6
        Me.ButtonLogin.Text = "Login"
        Me.ButtonLogin.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PanelHeader.Controls.Add(Me.PictureBoxCompanyLogo)
        Me.PanelHeader.Controls.Add(Me.PictureBoxVINTALogo)
        Me.PanelHeader.Controls.Add(Me.LabelLogin)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(1365, 97)
        Me.PanelHeader.TabIndex = 45
        '
        'PictureBoxCompanyLogo
        '
        Me.PictureBoxCompanyLogo.Location = New System.Drawing.Point(12, 0)
        Me.PictureBoxCompanyLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxCompanyLogo.Name = "PictureBoxCompanyLogo"
        Me.PictureBoxCompanyLogo.Size = New System.Drawing.Size(142, 98)
        Me.PictureBoxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxCompanyLogo.TabIndex = 87
        Me.PictureBoxCompanyLogo.TabStop = False
        '
        'PictureBoxVINTALogo
        '
        Me.PictureBoxVINTALogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxVINTALogo.Location = New System.Drawing.Point(1201, -2)
        Me.PictureBoxVINTALogo.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxVINTALogo.Name = "PictureBoxVINTALogo"
        Me.PictureBoxVINTALogo.Size = New System.Drawing.Size(164, 100)
        Me.PictureBoxVINTALogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxVINTALogo.TabIndex = 86
        Me.PictureBoxVINTALogo.TabStop = False
        '
        'LabelLogin
        '
        Me.LabelLogin.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LabelLogin.AutoSize = True
        Me.LabelLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelLogin.Location = New System.Drawing.Point(627, 33)
        Me.LabelLogin.Name = "LabelLogin"
        Me.LabelLogin.Size = New System.Drawing.Size(118, 44)
        Me.LabelLogin.TabIndex = 0
        Me.LabelLogin.Text = "Login"
        '
        'PanelFooter
        '
        Me.PanelFooter.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PanelFooter.Controls.Add(Me.LabelPOSno)
        Me.PanelFooter.Controls.Add(Me.LabelPOS)
        Me.PanelFooter.Controls.Add(Me.LabelDateTime)
        Me.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelFooter.Location = New System.Drawing.Point(0, 892)
        Me.PanelFooter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Size = New System.Drawing.Size(1365, 53)
        Me.PanelFooter.TabIndex = 46
        '
        'LabelPOSno
        '
        Me.LabelPOSno.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPOSno.AutoSize = True
        Me.LabelPOSno.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelPOSno.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPOSno.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelPOSno.Location = New System.Drawing.Point(700, 12)
        Me.LabelPOSno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPOSno.Name = "LabelPOSno"
        Me.LabelPOSno.Size = New System.Drawing.Size(0, 29)
        Me.LabelPOSno.TabIndex = 88
        '
        'LabelPOS
        '
        Me.LabelPOS.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPOS.AutoSize = True
        Me.LabelPOS.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelPOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPOS.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelPOS.Location = New System.Drawing.Point(629, 12)
        Me.LabelPOS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPOS.Name = "LabelPOS"
        Me.LabelPOS.Size = New System.Drawing.Size(74, 29)
        Me.LabelPOS.TabIndex = 81
        Me.LabelPOS.Text = "POS:"
        '
        'LabelDateTime
        '
        Me.LabelDateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDateTime.AutoSize = True
        Me.LabelDateTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDateTime.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelDateTime.Location = New System.Drawing.Point(1013, 12)
        Me.LabelDateTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelDateTime.Name = "LabelDateTime"
        Me.LabelDateTime.Size = New System.Drawing.Size(0, 29)
        Me.LabelDateTime.TabIndex = 80
        '
        'TextBoxUserID
        '
        Me.TextBoxUserID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxUserID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxUserID.Location = New System.Drawing.Point(567, 164)
        Me.TextBoxUserID.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxUserID.Name = "TextBoxUserID"
        Me.TextBoxUserID.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxUserID.Size = New System.Drawing.Size(257, 30)
        Me.TextBoxUserID.TabIndex = 1
        Me.TextBoxUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ComboReaderNames
        '
        Me.ComboReaderNames.FormattingEnabled = True
        Me.ComboReaderNames.Location = New System.Drawing.Point(567, 130)
        Me.ComboReaderNames.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboReaderNames.Name = "ComboReaderNames"
        Me.ComboReaderNames.Size = New System.Drawing.Size(257, 24)
        Me.ComboReaderNames.TabIndex = 48
        Me.ComboReaderNames.Visible = False
        '
        'LabelName
        '
        Me.LabelName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelName.AutoSize = True
        Me.LabelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelName.Location = New System.Drawing.Point(605, 512)
        Me.LabelName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelName.Name = "LabelName"
        Me.LabelName.Size = New System.Drawing.Size(0, 25)
        Me.LabelName.TabIndex = 50
        '
        'LabelPosition
        '
        Me.LabelPosition.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelPosition.AutoSize = True
        Me.LabelPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPosition.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelPosition.Location = New System.Drawing.Point(605, 551)
        Me.LabelPosition.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPosition.Name = "LabelPosition"
        Me.LabelPosition.Size = New System.Drawing.Size(0, 25)
        Me.LabelPosition.TabIndex = 51
        '
        'ButtonClose
        '
        Me.ButtonClose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonClose.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonClose.Location = New System.Drawing.Point(515, 798)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(360, 58)
        Me.ButtonClose.TabIndex = 54
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = False
        '
        'LabelVersionNo
        '
        Me.LabelVersionNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelVersionNo.AutoSize = True
        Me.LabelVersionNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelVersionNo.Location = New System.Drawing.Point(69, 871)
        Me.LabelVersionNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVersionNo.Name = "LabelVersionNo"
        Me.LabelVersionNo.Size = New System.Drawing.Size(0, 17)
        Me.LabelVersionNo.TabIndex = 88
        '
        'LabelSoftwareName
        '
        Me.LabelSoftwareName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSoftwareName.AutoSize = True
        Me.LabelSoftwareName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelSoftwareName.Location = New System.Drawing.Point(9, 871)
        Me.LabelSoftwareName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSoftwareName.Name = "LabelSoftwareName"
        Me.LabelSoftwareName.Size = New System.Drawing.Size(48, 17)
        Me.LabelSoftwareName.TabIndex = 87
        Me.LabelSoftwareName.Text = "VINTA"
        '
        'LabelHello
        '
        Me.LabelHello.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelHello.AutoSize = True
        Me.LabelHello.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelHello.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelHello.Location = New System.Drawing.Point(527, 512)
        Me.LabelHello.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelHello.Name = "LabelHello"
        Me.LabelHello.Size = New System.Drawing.Size(61, 25)
        Me.LabelHello.TabIndex = 89
        Me.LabelHello.Text = "Hello"
        Me.LabelHello.Visible = False
        '
        'PictureBoxFace
        '
        Me.PictureBoxFace.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBoxFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBoxFace.Location = New System.Drawing.Point(515, 241)
        Me.PictureBoxFace.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxFace.Name = "PictureBoxFace"
        Me.PictureBoxFace.Size = New System.Drawing.Size(360, 242)
        Me.PictureBoxFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxFace.TabIndex = 47
        Me.PictureBoxFace.TabStop = False
        '
        'LabelIndicator
        '
        Me.LabelIndicator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelIndicator.AutoSize = True
        Me.LabelIndicator.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelIndicator.Location = New System.Drawing.Point(1251, 863)
        Me.LabelIndicator.Name = "LabelIndicator"
        Me.LabelIndicator.Size = New System.Drawing.Size(0, 25)
        Me.LabelIndicator.TabIndex = 90
        '
        'pbFingerprint
        '
        Me.pbFingerprint.Location = New System.Drawing.Point(1238, 832)
        Me.pbFingerprint.Name = "pbFingerprint"
        Me.pbFingerprint.Size = New System.Drawing.Size(43, 28)
        Me.pbFingerprint.TabIndex = 91
        Me.pbFingerprint.TabStop = False
        '
        'cmbFinger
        '
        Me.cmbFinger.FormattingEnabled = True
        Me.cmbFinger.Location = New System.Drawing.Point(1238, 862)
        Me.cmbFinger.Name = "cmbFinger"
        Me.cmbFinger.Size = New System.Drawing.Size(46, 24)
        Me.cmbFinger.TabIndex = 92
        '
        'lblFpStatus
        '
        Me.lblFpStatus.AutoSize = True
        Me.lblFpStatus.Location = New System.Drawing.Point(1287, 839)
        Me.lblFpStatus.Name = "lblFpStatus"
        Me.lblFpStatus.Size = New System.Drawing.Size(51, 17)
        Me.lblFpStatus.TabIndex = 93
        Me.lblFpStatus.Text = "Label1"
        '
        'btnScanFinger
        '
        Me.btnScanFinger.Location = New System.Drawing.Point(1290, 863)
        Me.btnScanFinger.Name = "btnScanFinger"
        Me.btnScanFinger.Size = New System.Drawing.Size(48, 23)
        Me.btnScanFinger.TabIndex = 94
        Me.btnScanFinger.Text = "Button1"
        Me.btnScanFinger.UseVisualStyleBackColor = True
        '
        'login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(1365, 945)
        Me.Controls.Add(Me.btnScanFinger)
        Me.Controls.Add(Me.lblFpStatus)
        Me.Controls.Add(Me.cmbFinger)
        Me.Controls.Add(Me.pbFingerprint)
        Me.Controls.Add(Me.LabelIndicator)
        Me.Controls.Add(Me.LabelHello)
        Me.Controls.Add(Me.LabelVersionNo)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.LabelSoftwareName)
        Me.Controls.Add(Me.LabelPosition)
        Me.Controls.Add(Me.LabelName)
        Me.Controls.Add(Me.ComboReaderNames)
        Me.Controls.Add(Me.TextBoxUserID)
        Me.Controls.Add(Me.PictureBoxFace)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelHeader)
        Me.Controls.Add(Me.ButtonLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Tag = "destination"
        Me.Text = "login"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFooter.ResumeLayout(False)
        Me.PanelFooter.PerformLayout()
        CType(Me.PictureBoxFace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFingerprint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonLogin As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PanelHeader As Panel
    Friend WithEvents LabelLogin As Label
    Friend WithEvents PanelFooter As Panel
    Friend WithEvents LabelDateTime As Label
    Friend WithEvents LabelPOS As Label
    Friend WithEvents PictureBoxFace As PictureBox
    Friend WithEvents TextBoxUserID As TextBox
    Friend WithEvents ComboReaderNames As ComboBox
    Friend WithEvents LabelName As Label
    Friend WithEvents LabelPosition As Label
    Friend WithEvents ButtonClose As Button
    Friend WithEvents PictureBoxVINTALogo As PictureBox
    Friend WithEvents LabelVersionNo As Label
    Friend WithEvents LabelSoftwareName As Label
    Friend WithEvents LabelPOSno As Label
    Friend WithEvents LabelHello As Label
    Friend WithEvents PictureBoxCompanyLogo As PictureBox
    Friend WithEvents LabelIndicator As Label
    Friend WithEvents pbFingerprint As PictureBox
    Friend WithEvents cmbFinger As ComboBox
    Friend WithEvents lblFpStatus As Label
    Friend WithEvents btnScanFinger As Button
End Class
