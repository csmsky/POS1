<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class management
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
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(management))
        Me.audit_trail_tblBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.audit_trail_tblDataSet = New WindowsApplication1.audit_trail_tblDataSet()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.LabelSerial = New System.Windows.Forms.Label()
        Me.LabelStationID = New System.Windows.Forms.Label()
        Me.LabelStation = New System.Windows.Forms.Label()
        Me.PictureBoxCompanyLogo = New System.Windows.Forms.PictureBox()
        Me.LabelLoginID = New System.Windows.Forms.Label()
        Me.PictureBoxVINTALogo = New System.Windows.Forms.PictureBox()
        Me.LabelManagement = New System.Windows.Forms.Label()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.LabelPOSno = New System.Windows.Forms.Label()
        Me.LabelUser = New System.Windows.Forms.Label()
        Me.LabelUserID = New System.Windows.Forms.Label()
        Me.LabelUserName = New System.Windows.Forms.Label()
        Me.LabelPOS = New System.Windows.Forms.Label()
        Me.LabelDateTime = New System.Windows.Forms.Label()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.DateTimePickerXReport = New System.Windows.Forms.DateTimePicker()
        Me.ButtonPrintXReport = New System.Windows.Forms.Button()
        Me.ButtonLogout = New System.Windows.Forms.Button()
        Me.LabelUserActivityLog = New System.Windows.Forms.Label()
        Me.PanelX = New System.Windows.Forms.Panel()
        Me.ComboBoxXCashier = New System.Windows.Forms.ComboBox()
        Me.LabelXCashier = New System.Windows.Forms.Label()
        Me.ButtonPrintZReport = New System.Windows.Forms.Button()
        Me.DateTimePickerZStart = New System.Windows.Forms.DateTimePicker()
        Me.PanelZ = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ButtonReports = New System.Windows.Forms.Button()
        Me.PrintDocument3 = New System.Drawing.Printing.PrintDocument()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelVersionNo = New System.Windows.Forms.Label()
        Me.LabelSoftwareName = New System.Windows.Forms.Label()
        Me.ReportViewerAuditTrail = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.LabelTO2 = New System.Windows.Forms.Label()
        Me.DateTimePicker2AuditTrail = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1AuditTrail = New System.Windows.Forms.DateTimePicker()
        Me.ButtonSearchVoid = New System.Windows.Forms.Button()
        Me.ButtonShowAll = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePickerEJ2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerEJ1 = New System.Windows.Forms.DateTimePicker()
        Me.ButtonDownload = New System.Windows.Forms.Button()
        Me.audit_trail_tblTableAdapter = New WindowsApplication1.audit_trail_tblDataSetTableAdapters.audit_trail_tblTableAdapter()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ButtonSouvenir = New System.Windows.Forms.Button()
        Me.ButtonPMethod = New System.Windows.Forms.Button()
        Me.PictureBoxLogoReceipt = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.audit_trail_tblBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.audit_trail_tblDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelHeader.SuspendLayout()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFooter.SuspendLayout()
        Me.PanelX.SuspendLayout()
        Me.PanelZ.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxLogoReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'audit_trail_tblBindingSource
        '
        Me.audit_trail_tblBindingSource.DataMember = "audit_trail_tbl"
        Me.audit_trail_tblBindingSource.DataSource = Me.audit_trail_tblDataSet
        '
        'audit_trail_tblDataSet
        '
        Me.audit_trail_tblDataSet.DataSetName = "audit_trail_tblDataSet"
        Me.audit_trail_tblDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PanelHeader.Controls.Add(Me.LabelSerial)
        Me.PanelHeader.Controls.Add(Me.LabelStationID)
        Me.PanelHeader.Controls.Add(Me.LabelStation)
        Me.PanelHeader.Controls.Add(Me.PictureBoxCompanyLogo)
        Me.PanelHeader.Controls.Add(Me.LabelLoginID)
        Me.PanelHeader.Controls.Add(Me.PictureBoxVINTALogo)
        Me.PanelHeader.Controls.Add(Me.LabelManagement)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(1453, 97)
        Me.PanelHeader.TabIndex = 45
        '
        'LabelSerial
        '
        Me.LabelSerial.AutoSize = True
        Me.LabelSerial.ForeColor = System.Drawing.Color.Black
        Me.LabelSerial.Location = New System.Drawing.Point(301, 60)
        Me.LabelSerial.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSerial.Name = "LabelSerial"
        Me.LabelSerial.Size = New System.Drawing.Size(35, 17)
        Me.LabelSerial.TabIndex = 316
        Me.LabelSerial.Text = "SRL"
        '
        'LabelStationID
        '
        Me.LabelStationID.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelStationID.AutoSize = True
        Me.LabelStationID.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelStationID.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelStationID.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelStationID.Location = New System.Drawing.Point(262, 9)
        Me.LabelStationID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStationID.Name = "LabelStationID"
        Me.LabelStationID.Size = New System.Drawing.Size(0, 29)
        Me.LabelStationID.TabIndex = 92
        '
        'LabelStation
        '
        Me.LabelStation.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelStation.AutoSize = True
        Me.LabelStation.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelStation.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelStation.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelStation.Location = New System.Drawing.Point(159, 9)
        Me.LabelStation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStation.Name = "LabelStation"
        Me.LabelStation.Size = New System.Drawing.Size(101, 29)
        Me.LabelStation.TabIndex = 91
        Me.LabelStation.Text = "Station:"
        '
        'PictureBoxCompanyLogo
        '
        Me.PictureBoxCompanyLogo.Location = New System.Drawing.Point(12, 0)
        Me.PictureBoxCompanyLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxCompanyLogo.Name = "PictureBoxCompanyLogo"
        Me.PictureBoxCompanyLogo.Size = New System.Drawing.Size(142, 97)
        Me.PictureBoxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxCompanyLogo.TabIndex = 88
        Me.PictureBoxCompanyLogo.TabStop = False
        '
        'LabelLoginID
        '
        Me.LabelLoginID.AutoSize = True
        Me.LabelLoginID.Location = New System.Drawing.Point(255, 60)
        Me.LabelLoginID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelLoginID.Name = "LabelLoginID"
        Me.LabelLoginID.Size = New System.Drawing.Size(29, 17)
        Me.LabelLoginID.TabIndex = 87
        Me.LabelLoginID.Text = "LID"
        '
        'PictureBoxVINTALogo
        '
        Me.PictureBoxVINTALogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxVINTALogo.Location = New System.Drawing.Point(1289, -2)
        Me.PictureBoxVINTALogo.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxVINTALogo.Name = "PictureBoxVINTALogo"
        Me.PictureBoxVINTALogo.Size = New System.Drawing.Size(164, 100)
        Me.PictureBoxVINTALogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxVINTALogo.TabIndex = 86
        Me.PictureBoxVINTALogo.TabStop = False
        '
        'LabelManagement
        '
        Me.LabelManagement.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LabelManagement.AutoSize = True
        Me.LabelManagement.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelManagement.ForeColor = System.Drawing.Color.Black
        Me.LabelManagement.Location = New System.Drawing.Point(623, 23)
        Me.LabelManagement.Name = "LabelManagement"
        Me.LabelManagement.Size = New System.Drawing.Size(260, 44)
        Me.LabelManagement.TabIndex = 0
        Me.LabelManagement.Text = "Management"
        '
        'PanelFooter
        '
        Me.PanelFooter.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PanelFooter.Controls.Add(Me.LabelPOSno)
        Me.PanelFooter.Controls.Add(Me.LabelUser)
        Me.PanelFooter.Controls.Add(Me.LabelUserID)
        Me.PanelFooter.Controls.Add(Me.LabelUserName)
        Me.PanelFooter.Controls.Add(Me.LabelPOS)
        Me.PanelFooter.Controls.Add(Me.LabelDateTime)
        Me.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelFooter.Location = New System.Drawing.Point(0, 587)
        Me.PanelFooter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Size = New System.Drawing.Size(1453, 53)
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
        'LabelUser
        '
        Me.LabelUser.AutoSize = True
        Me.LabelUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelUser.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelUser.Location = New System.Drawing.Point(11, 12)
        Me.LabelUser.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelUser.Name = "LabelUser"
        Me.LabelUser.Size = New System.Drawing.Size(75, 29)
        Me.LabelUser.TabIndex = 50
        Me.LabelUser.Text = "User:"
        '
        'LabelUserID
        '
        Me.LabelUserID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelUserID.AutoSize = True
        Me.LabelUserID.BackColor = System.Drawing.Color.SteelBlue
        Me.LabelUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelUserID.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelUserID.Location = New System.Drawing.Point(581, 12)
        Me.LabelUserID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelUserID.Name = "LabelUserID"
        Me.LabelUserID.Size = New System.Drawing.Size(0, 29)
        Me.LabelUserID.TabIndex = 86
        '
        'LabelUserName
        '
        Me.LabelUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelUserName.AutoSize = True
        Me.LabelUserName.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelUserName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelUserName.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelUserName.Location = New System.Drawing.Point(95, 12)
        Me.LabelUserName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelUserName.Name = "LabelUserName"
        Me.LabelUserName.Size = New System.Drawing.Size(0, 29)
        Me.LabelUserName.TabIndex = 83
        '
        'LabelPOS
        '
        Me.LabelPOS.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPOS.AutoSize = True
        Me.LabelPOS.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelPOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPOS.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelPOS.Location = New System.Drawing.Point(616, 13)
        Me.LabelPOS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPOS.Name = "LabelPOS"
        Me.LabelPOS.Size = New System.Drawing.Size(74, 29)
        Me.LabelPOS.TabIndex = 82
        Me.LabelPOS.Text = "POS:"
        '
        'LabelDateTime
        '
        Me.LabelDateTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDateTime.AutoSize = True
        Me.LabelDateTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDateTime.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelDateTime.Location = New System.Drawing.Point(1101, 12)
        Me.LabelDateTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelDateTime.Name = "LabelDateTime"
        Me.LabelDateTime.Size = New System.Drawing.Size(0, 29)
        Me.LabelDateTime.TabIndex = 80
        '
        'PrintDocument1
        '
        '
        'PrintDocument2
        '
        '
        'DateTimePickerXReport
        '
        Me.DateTimePickerXReport.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerXReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerXReport.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerXReport.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DateTimePickerXReport.Location = New System.Drawing.Point(119, 30)
        Me.DateTimePickerXReport.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerXReport.Name = "DateTimePickerXReport"
        Me.DateTimePickerXReport.Size = New System.Drawing.Size(311, 34)
        Me.DateTimePickerXReport.TabIndex = 49
        Me.DateTimePickerXReport.Value = New Date(2018, 12, 2, 0, 0, 0, 0)
        '
        'ButtonPrintXReport
        '
        Me.ButtonPrintXReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPrintXReport.Location = New System.Drawing.Point(119, 185)
        Me.ButtonPrintXReport.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonPrintXReport.Name = "ButtonPrintXReport"
        Me.ButtonPrintXReport.Size = New System.Drawing.Size(312, 55)
        Me.ButtonPrintXReport.TabIndex = 47
        Me.ButtonPrintXReport.Text = "Print X-Report"
        Me.ButtonPrintXReport.UseVisualStyleBackColor = True
        '
        'ButtonLogout
        '
        Me.ButtonLogout.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonLogout.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ButtonLogout.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonLogout.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonLogout.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonLogout.Location = New System.Drawing.Point(1296, -47)
        Me.ButtonLogout.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonLogout.Name = "ButtonLogout"
        Me.ButtonLogout.Size = New System.Drawing.Size(97, 39)
        Me.ButtonLogout.TabIndex = 52
        Me.ButtonLogout.Text = "Logout"
        Me.ButtonLogout.UseVisualStyleBackColor = False
        '
        'LabelUserActivityLog
        '
        Me.LabelUserActivityLog.AutoSize = True
        Me.LabelUserActivityLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelUserActivityLog.Location = New System.Drawing.Point(132, 397)
        Me.LabelUserActivityLog.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelUserActivityLog.Name = "LabelUserActivityLog"
        Me.LabelUserActivityLog.Size = New System.Drawing.Size(180, 25)
        Me.LabelUserActivityLog.TabIndex = 88
        Me.LabelUserActivityLog.Text = "Audit Trail Report"
        '
        'PanelX
        '
        Me.PanelX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelX.Controls.Add(Me.ComboBoxXCashier)
        Me.PanelX.Controls.Add(Me.LabelXCashier)
        Me.PanelX.Controls.Add(Me.DateTimePickerXReport)
        Me.PanelX.Controls.Add(Me.ButtonPrintXReport)
        Me.PanelX.Location = New System.Drawing.Point(16, 105)
        Me.PanelX.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelX.Name = "PanelX"
        Me.PanelX.Size = New System.Drawing.Size(452, 283)
        Me.PanelX.TabIndex = 89
        '
        'ComboBoxXCashier
        '
        Me.ComboBoxXCashier.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxXCashier.FormattingEnabled = True
        Me.ComboBoxXCashier.Location = New System.Drawing.Point(119, 97)
        Me.ComboBoxXCashier.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxXCashier.Name = "ComboBoxXCashier"
        Me.ComboBoxXCashier.Size = New System.Drawing.Size(311, 34)
        Me.ComboBoxXCashier.TabIndex = 53
        '
        'LabelXCashier
        '
        Me.LabelXCashier.AutoSize = True
        Me.LabelXCashier.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelXCashier.Location = New System.Drawing.Point(19, 101)
        Me.LabelXCashier.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelXCashier.Name = "LabelXCashier"
        Me.LabelXCashier.Size = New System.Drawing.Size(86, 26)
        Me.LabelXCashier.TabIndex = 52
        Me.LabelXCashier.Text = "Cashier:"
        '
        'ButtonPrintZReport
        '
        Me.ButtonPrintZReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPrintZReport.Location = New System.Drawing.Point(918, 481)
        Me.ButtonPrintZReport.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonPrintZReport.Name = "ButtonPrintZReport"
        Me.ButtonPrintZReport.Size = New System.Drawing.Size(100, 31)
        Me.ButtonPrintZReport.TabIndex = 48
        Me.ButtonPrintZReport.Text = "Print Z-Report"
        Me.ButtonPrintZReport.UseVisualStyleBackColor = True
        '
        'DateTimePickerZStart
        '
        Me.DateTimePickerZStart.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerZStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerZStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerZStart.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DateTimePickerZStart.Location = New System.Drawing.Point(77, 30)
        Me.DateTimePickerZStart.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerZStart.Name = "DateTimePickerZStart"
        Me.DateTimePickerZStart.Size = New System.Drawing.Size(312, 34)
        Me.DateTimePickerZStart.TabIndex = 51
        Me.DateTimePickerZStart.Value = New Date(2018, 12, 2, 0, 0, 0, 0)
        '
        'PanelZ
        '
        Me.PanelZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelZ.Controls.Add(Me.Button2)
        Me.PanelZ.Controls.Add(Me.DateTimePickerZStart)
        Me.PanelZ.Location = New System.Drawing.Point(476, 105)
        Me.PanelZ.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelZ.Name = "PanelZ"
        Me.PanelZ.Size = New System.Drawing.Size(452, 283)
        Me.PanelZ.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(77, 185)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(311, 55)
        Me.Button2.TabIndex = 53
        Me.Button2.Text = "Print Z-Report"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ButtonReports
        '
        Me.ButtonReports.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonReports.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonReports.Location = New System.Drawing.Point(932, 446)
        Me.ButtonReports.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonReports.Name = "ButtonReports"
        Me.ButtonReports.Size = New System.Drawing.Size(97, 39)
        Me.ButtonReports.TabIndex = 90
        Me.ButtonReports.Text = "Reports"
        Me.ButtonReports.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'LabelVersionNo
        '
        Me.LabelVersionNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelVersionNo.AutoSize = True
        Me.LabelVersionNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelVersionNo.Location = New System.Drawing.Point(108, 565)
        Me.LabelVersionNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVersionNo.Name = "LabelVersionNo"
        Me.LabelVersionNo.Size = New System.Drawing.Size(0, 17)
        Me.LabelVersionNo.TabIndex = 96
        '
        'LabelSoftwareName
        '
        Me.LabelSoftwareName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSoftwareName.AutoSize = True
        Me.LabelSoftwareName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelSoftwareName.Location = New System.Drawing.Point(3, 565)
        Me.LabelSoftwareName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSoftwareName.Name = "LabelSoftwareName"
        Me.LabelSoftwareName.Size = New System.Drawing.Size(60, 17)
        Me.LabelSoftwareName.TabIndex = 95
        Me.LabelSoftwareName.Text = "Version:"
        '
        'ReportViewerAuditTrail
        '
        ReportDataSource1.Name = "audit_trail_tblDataSet"
        ReportDataSource1.Value = Me.audit_trail_tblBindingSource
        Me.ReportViewerAuditTrail.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewerAuditTrail.LocalReport.ReportEmbeddedResource = "WindowsApplication1.ReportAuditTrail.rdlc"
        Me.ReportViewerAuditTrail.Location = New System.Drawing.Point(12, 434)
        Me.ReportViewerAuditTrail.Name = "ReportViewerAuditTrail"
        Me.ReportViewerAuditTrail.ServerReport.BearerToken = Nothing
        Me.ReportViewerAuditTrail.Size = New System.Drawing.Size(899, 105)
        Me.ReportViewerAuditTrail.TabIndex = 97
        '
        'LabelTO2
        '
        Me.LabelTO2.AutoSize = True
        Me.LabelTO2.Location = New System.Drawing.Point(1054, 403)
        Me.LabelTO2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTO2.Name = "LabelTO2"
        Me.LabelTO2.Size = New System.Drawing.Size(28, 17)
        Me.LabelTO2.TabIndex = 101
        Me.LabelTO2.Text = "TO"
        '
        'DateTimePicker2AuditTrail
        '
        Me.DateTimePicker2AuditTrail.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2AuditTrail.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker2AuditTrail.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2AuditTrail.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2AuditTrail.Location = New System.Drawing.Point(1090, 394)
        Me.DateTimePicker2AuditTrail.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2AuditTrail.Name = "DateTimePicker2AuditTrail"
        Me.DateTimePicker2AuditTrail.Size = New System.Drawing.Size(160, 34)
        Me.DateTimePicker2AuditTrail.TabIndex = 100
        Me.DateTimePicker2AuditTrail.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'DateTimePicker1AuditTrail
        '
        Me.DateTimePicker1AuditTrail.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1AuditTrail.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker1AuditTrail.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1AuditTrail.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1AuditTrail.Location = New System.Drawing.Point(886, 394)
        Me.DateTimePicker1AuditTrail.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1AuditTrail.Name = "DateTimePicker1AuditTrail"
        Me.DateTimePicker1AuditTrail.Size = New System.Drawing.Size(160, 34)
        Me.DateTimePicker1AuditTrail.TabIndex = 99
        Me.DateTimePicker1AuditTrail.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'ButtonSearchVoid
        '
        Me.ButtonSearchVoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSearchVoid.Location = New System.Drawing.Point(1258, 394)
        Me.ButtonSearchVoid.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSearchVoid.Name = "ButtonSearchVoid"
        Me.ButtonSearchVoid.Size = New System.Drawing.Size(91, 34)
        Me.ButtonSearchVoid.TabIndex = 98
        Me.ButtonSearchVoid.Text = "Search"
        Me.ButtonSearchVoid.UseVisualStyleBackColor = True
        '
        'ButtonShowAll
        '
        Me.ButtonShowAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonShowAll.Location = New System.Drawing.Point(13, 393)
        Me.ButtonShowAll.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonShowAll.Name = "ButtonShowAll"
        Me.ButtonShowAll.Size = New System.Drawing.Size(115, 34)
        Me.ButtonShowAll.TabIndex = 102
        Me.ButtonShowAll.Text = "Show All"
        Me.ButtonShowAll.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.DateTimePickerEJ2)
        Me.Panel1.Controls.Add(Me.DateTimePickerEJ1)
        Me.Panel1.Location = New System.Drawing.Point(935, 105)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(310, 282)
        Me.Panel1.TabIndex = 103
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(42, 185)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(237, 55)
        Me.Button1.TabIndex = 98
        Me.Button1.Text = "Download E-Journal"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePickerEJ2
        '
        Me.DateTimePickerEJ2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DateTimePickerEJ2.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerEJ2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerEJ2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerEJ2.Location = New System.Drawing.Point(49, 72)
        Me.DateTimePickerEJ2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerEJ2.Name = "DateTimePickerEJ2"
        Me.DateTimePickerEJ2.Size = New System.Drawing.Size(209, 34)
        Me.DateTimePickerEJ2.TabIndex = 97
        Me.DateTimePickerEJ2.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'DateTimePickerEJ1
        '
        Me.DateTimePickerEJ1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DateTimePickerEJ1.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerEJ1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerEJ1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerEJ1.Location = New System.Drawing.Point(49, 30)
        Me.DateTimePickerEJ1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerEJ1.Name = "DateTimePickerEJ1"
        Me.DateTimePickerEJ1.Size = New System.Drawing.Size(209, 34)
        Me.DateTimePickerEJ1.TabIndex = 96
        Me.DateTimePickerEJ1.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'ButtonDownload
        '
        Me.ButtonDownload.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonDownload.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDownload.Location = New System.Drawing.Point(932, 532)
        Me.ButtonDownload.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonDownload.Name = "ButtonDownload"
        Me.ButtonDownload.Size = New System.Drawing.Size(100, 30)
        Me.ButtonDownload.TabIndex = 95
        Me.ButtonDownload.Text = "Download E-Journal"
        Me.ButtonDownload.UseVisualStyleBackColor = True
        '
        'audit_trail_tblTableAdapter
        '
        Me.audit_trail_tblTableAdapter.ClearBeforeFill = True
        '
        'ButtonSouvenir
        '
        Me.ButtonSouvenir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSouvenir.Location = New System.Drawing.Point(609, 60)
        Me.ButtonSouvenir.Name = "ButtonSouvenir"
        Me.ButtonSouvenir.Size = New System.Drawing.Size(146, 86)
        Me.ButtonSouvenir.TabIndex = 104
        Me.ButtonSouvenir.Text = "(+)Add Souvenir"
        Me.ButtonSouvenir.UseVisualStyleBackColor = True
        '
        'ButtonPMethod
        '
        Me.ButtonPMethod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPMethod.Location = New System.Drawing.Point(423, 60)
        Me.ButtonPMethod.Name = "ButtonPMethod"
        Me.ButtonPMethod.Size = New System.Drawing.Size(146, 86)
        Me.ButtonPMethod.TabIndex = 105
        Me.ButtonPMethod.Text = "(+)Add Payment Method"
        Me.ButtonPMethod.UseVisualStyleBackColor = True
        '
        'PictureBoxLogoReceipt
        '
        Me.PictureBoxLogoReceipt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBoxLogoReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxLogoReceipt.Location = New System.Drawing.Point(1039, 446)
        Me.PictureBoxLogoReceipt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxLogoReceipt.Name = "PictureBoxLogoReceipt"
        Me.PictureBoxLogoReceipt.Size = New System.Drawing.Size(91, 62)
        Me.PictureBoxLogoReceipt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxLogoReceipt.TabIndex = 91
        Me.PictureBoxLogoReceipt.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ButtonPMethod)
        Me.Panel2.Controls.Add(Me.ButtonSouvenir)
        Me.Panel2.Location = New System.Drawing.Point(12, 397)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1238, 185)
        Me.Panel2.TabIndex = 106
        '
        'management
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1453, 640)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ButtonPrintZReport)
        Me.Controls.Add(Me.ButtonDownload)
        Me.Controls.Add(Me.ButtonShowAll)
        Me.Controls.Add(Me.LabelTO2)
        Me.Controls.Add(Me.DateTimePicker2AuditTrail)
        Me.Controls.Add(Me.DateTimePicker1AuditTrail)
        Me.Controls.Add(Me.ButtonSearchVoid)
        Me.Controls.Add(Me.ReportViewerAuditTrail)
        Me.Controls.Add(Me.LabelVersionNo)
        Me.Controls.Add(Me.LabelSoftwareName)
        Me.Controls.Add(Me.PictureBoxLogoReceipt)
        Me.Controls.Add(Me.ButtonReports)
        Me.Controls.Add(Me.PanelZ)
        Me.Controls.Add(Me.PanelX)
        Me.Controls.Add(Me.LabelUserActivityLog)
        Me.Controls.Add(Me.ButtonLogout)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "management"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "management"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.audit_trail_tblBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.audit_trail_tblDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFooter.ResumeLayout(False)
        Me.PanelFooter.PerformLayout()
        Me.PanelX.ResumeLayout(False)
        Me.PanelX.PerformLayout()
        Me.PanelZ.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxLogoReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelHeader As Panel
    Friend WithEvents PictureBoxVINTALogo As PictureBox
    Friend WithEvents LabelManagement As Label
    Friend WithEvents PanelFooter As Panel
    Friend WithEvents LabelUser As Label
    Friend WithEvents LabelUserID As Label
    Friend WithEvents LabelUserName As Label
    Friend WithEvents LabelPOS As Label
    Friend WithEvents LabelDateTime As Label
    Friend WithEvents LabelLoginID As Label
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintDocument2 As Printing.PrintDocument
    Friend WithEvents DateTimePickerXReport As DateTimePicker
    Friend WithEvents ButtonPrintXReport As Button
    Friend WithEvents ButtonLogout As Button
    Friend WithEvents LabelUserActivityLog As Label
    Friend WithEvents PanelX As Panel
    Friend WithEvents ComboBoxXCashier As ComboBox
    Friend WithEvents LabelXCashier As Label
    'Friend WithEvents CrystalReportOR1 As CrystalReportOR
    Friend WithEvents ButtonPrintZReport As Button
    Friend WithEvents DateTimePickerZStart As DateTimePicker
    Friend WithEvents PanelZ As Panel
    Friend WithEvents ButtonReports As Button
    Friend WithEvents PictureBoxLogoReceipt As PictureBox
    Friend WithEvents LabelPOSno As Label
    Friend WithEvents PrintDocument3 As Printing.PrintDocument
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PictureBoxCompanyLogo As PictureBox
    Friend WithEvents LabelStationID As Label
    Friend WithEvents LabelStation As Label
    Friend WithEvents LabelSerial As Label
    Friend WithEvents LabelVersionNo As Label
    Friend WithEvents LabelSoftwareName As Label
    Friend WithEvents ReportViewerAuditTrail As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents LabelTO2 As Label
    Friend WithEvents DateTimePicker2AuditTrail As DateTimePicker
    Friend WithEvents DateTimePicker1AuditTrail As DateTimePicker
    Friend WithEvents ButtonSearchVoid As Button
    Friend WithEvents ButtonShowAll As Button
    Friend WithEvents audit_trail_tblBindingSource As BindingSource
    Friend WithEvents audit_trail_tblDataSet As audit_trail_tblDataSet
    Friend WithEvents audit_trail_tblTableAdapter As audit_trail_tblDataSetTableAdapters.audit_trail_tblTableAdapter
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DateTimePickerEJ2 As DateTimePicker
    Friend WithEvents ButtonDownload As Button
    Friend WithEvents DateTimePickerEJ1 As DateTimePicker
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ButtonSouvenir As Button
    Friend WithEvents ButtonPMethod As Button
    Friend WithEvents Panel2 As Panel
End Class
