<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class reports
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
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(reports))
        Me.or_tblBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.or_tblDataSet = New WindowsApplication1.or_tblDataSet()
        Me.void_tblBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.void_tblDataSet = New WindowsApplication1.void_tblDataSet()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.LabelStationID = New System.Windows.Forms.Label()
        Me.LabelStation = New System.Windows.Forms.Label()
        Me.PictureBoxCompanyLogo = New System.Windows.Forms.PictureBox()
        Me.PictureBoxVINTALogo = New System.Windows.Forms.PictureBox()
        Me.LabelManagement = New System.Windows.Forms.Label()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.LabelPOSno = New System.Windows.Forms.Label()
        Me.LabelUser = New System.Windows.Forms.Label()
        Me.LabelUserID = New System.Windows.Forms.Label()
        Me.LabelUserName = New System.Windows.Forms.Label()
        Me.LabelPOS = New System.Windows.Forms.Label()
        Me.LabelDateTime = New System.Windows.Forms.Label()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ReportViewerSales = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.LabelSales = New System.Windows.Forms.Label()
        Me.LabelTO = New System.Windows.Forms.Label()
        Me.DateTimePicker2Sales = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1Sales = New System.Windows.Forms.DateTimePicker()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.ButtonShowAll = New System.Windows.Forms.Button()
        Me.LabelVoid = New System.Windows.Forms.Label()
        Me.LabelTO2 = New System.Windows.Forms.Label()
        Me.DateTimePicker2Void = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1Void = New System.Windows.Forms.DateTimePicker()
        Me.ButtonSearchVoid = New System.Windows.Forms.Button()
        Me.ButtonShowAllVoid = New System.Windows.Forms.Button()
        Me.ReportViewerVoid = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ComboBoxSalesCashier = New System.Windows.Forms.ComboBox()
        Me.ComboBoxVoidCashier = New System.Windows.Forms.ComboBox()
        Me.void_tblTableAdapter = New WindowsApplication1.void_tblDataSetTableAdapters.void_tblTableAdapter()
        Me.VoidtblBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ComboBoxSalesType = New System.Windows.Forms.ComboBox()
        Me.or_tblTableAdapter = New WindowsApplication1.or_tblDataSetTableAdapters.or_tblTableAdapter()
        Me.OrtblDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ComboBoxVoidType = New System.Windows.Forms.ComboBox()
        Me.ButtonSalesSummary = New System.Windows.Forms.Button()
        CType(Me.or_tblBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.or_tblDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.void_tblBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.void_tblDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelHeader.SuspendLayout()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFooter.SuspendLayout()
        CType(Me.VoidtblBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OrtblDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'or_tblBindingSource
        '
        Me.or_tblBindingSource.DataMember = "or_tbl"
        Me.or_tblBindingSource.DataSource = Me.or_tblDataSet
        '
        'or_tblDataSet
        '
        Me.or_tblDataSet.DataSetName = "or_tblDataSet"
        Me.or_tblDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'void_tblBindingSource
        '
        Me.void_tblBindingSource.DataMember = "void_tbl"
        Me.void_tblBindingSource.DataSource = Me.void_tblDataSet
        '
        'void_tblDataSet
        '
        Me.void_tblDataSet.DataSetName = "void_tblDataSet"
        Me.void_tblDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PanelHeader.Controls.Add(Me.LabelStationID)
        Me.PanelHeader.Controls.Add(Me.LabelStation)
        Me.PanelHeader.Controls.Add(Me.PictureBoxCompanyLogo)
        Me.PanelHeader.Controls.Add(Me.PictureBoxVINTALogo)
        Me.PanelHeader.Controls.Add(Me.LabelManagement)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(1365, 97)
        Me.PanelHeader.TabIndex = 47
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
        Me.LabelStationID.TabIndex = 94
        '
        'LabelStation
        '
        Me.LabelStation.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelStation.AutoSize = True
        Me.LabelStation.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelStation.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelStation.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelStation.Location = New System.Drawing.Point(161, 9)
        Me.LabelStation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelStation.Name = "LabelStation"
        Me.LabelStation.Size = New System.Drawing.Size(101, 29)
        Me.LabelStation.TabIndex = 93
        Me.LabelStation.Text = "Station:"
        '
        'PictureBoxCompanyLogo
        '
        Me.PictureBoxCompanyLogo.Location = New System.Drawing.Point(12, -2)
        Me.PictureBoxCompanyLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxCompanyLogo.Name = "PictureBoxCompanyLogo"
        Me.PictureBoxCompanyLogo.Size = New System.Drawing.Size(142, 100)
        Me.PictureBoxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxCompanyLogo.TabIndex = 88
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
        'LabelManagement
        '
        Me.LabelManagement.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LabelManagement.AutoSize = True
        Me.LabelManagement.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelManagement.ForeColor = System.Drawing.Color.Black
        Me.LabelManagement.Location = New System.Drawing.Point(579, 23)
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
        Me.PanelFooter.Location = New System.Drawing.Point(0, 892)
        Me.PanelFooter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Size = New System.Drawing.Size(1365, 53)
        Me.PanelFooter.TabIndex = 48
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
        Me.LabelPOS.Location = New System.Drawing.Point(629, 12)
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
        Me.LabelDateTime.Location = New System.Drawing.Point(1013, 12)
        Me.LabelDateTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelDateTime.Name = "LabelDateTime"
        Me.LabelDateTime.Size = New System.Drawing.Size(0, 29)
        Me.LabelDateTime.TabIndex = 80
        '
        'ButtonClose
        '
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.Location = New System.Drawing.Point(1235, 103)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(115, 36)
        Me.ButtonClose.TabIndex = 55
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ReportViewerSales
        '
        ReportDataSource1.Name = "or_tblDataSet"
        ReportDataSource1.Value = Me.or_tblBindingSource
        Me.ReportViewerSales.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewerSales.LocalReport.ReportEmbeddedResource = "WindowsApplication1.ReportSales.rdlc"
        Me.ReportViewerSales.Location = New System.Drawing.Point(16, 184)
        Me.ReportViewerSales.Margin = New System.Windows.Forms.Padding(4)
        Me.ReportViewerSales.Name = "ReportViewerSales"
        Me.ReportViewerSales.Size = New System.Drawing.Size(1333, 330)
        Me.ReportViewerSales.TabIndex = 56
        '
        'LabelSales
        '
        Me.LabelSales.AutoSize = True
        Me.LabelSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSales.Location = New System.Drawing.Point(139, 106)
        Me.LabelSales.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSales.Name = "LabelSales"
        Me.LabelSales.Size = New System.Drawing.Size(183, 31)
        Me.LabelSales.TabIndex = 67
        Me.LabelSales.Text = "Sales Report"
        '
        'LabelTO
        '
        Me.LabelTO.AutoSize = True
        Me.LabelTO.Location = New System.Drawing.Point(764, 114)
        Me.LabelTO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTO.Name = "LabelTO"
        Me.LabelTO.Size = New System.Drawing.Size(28, 17)
        Me.LabelTO.TabIndex = 66
        Me.LabelTO.Text = "TO"
        '
        'DateTimePicker2Sales
        '
        Me.DateTimePicker2Sales.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker2Sales.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2Sales.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2Sales.Location = New System.Drawing.Point(800, 103)
        Me.DateTimePicker2Sales.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2Sales.Name = "DateTimePicker2Sales"
        Me.DateTimePicker2Sales.Size = New System.Drawing.Size(304, 34)
        Me.DateTimePicker2Sales.TabIndex = 65
        Me.DateTimePicker2Sales.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'DateTimePicker1Sales
        '
        Me.DateTimePicker1Sales.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker1Sales.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1Sales.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1Sales.Location = New System.Drawing.Point(452, 103)
        Me.DateTimePicker1Sales.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1Sales.Name = "DateTimePicker1Sales"
        Me.DateTimePicker1Sales.Size = New System.Drawing.Size(304, 34)
        Me.DateTimePicker1Sales.TabIndex = 64
        Me.DateTimePicker1Sales.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSearch.Location = New System.Drawing.Point(1112, 103)
        Me.ButtonSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(115, 36)
        Me.ButtonSearch.TabIndex = 63
        Me.ButtonSearch.Text = "Search"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'ButtonShowAll
        '
        Me.ButtonShowAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonShowAll.Location = New System.Drawing.Point(16, 103)
        Me.ButtonShowAll.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonShowAll.Name = "ButtonShowAll"
        Me.ButtonShowAll.Size = New System.Drawing.Size(115, 36)
        Me.ButtonShowAll.TabIndex = 62
        Me.ButtonShowAll.Text = "Show All"
        Me.ButtonShowAll.UseVisualStyleBackColor = True
        '
        'LabelVoid
        '
        Me.LabelVoid.AutoSize = True
        Me.LabelVoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelVoid.Location = New System.Drawing.Point(139, 527)
        Me.LabelVoid.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVoid.Name = "LabelVoid"
        Me.LabelVoid.Size = New System.Drawing.Size(234, 31)
        Me.LabelVoid.TabIndex = 75
        Me.LabelVoid.Text = "Void Transaction"
        '
        'LabelTO2
        '
        Me.LabelTO2.AutoSize = True
        Me.LabelTO2.Location = New System.Drawing.Point(764, 533)
        Me.LabelTO2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTO2.Name = "LabelTO2"
        Me.LabelTO2.Size = New System.Drawing.Size(28, 17)
        Me.LabelTO2.TabIndex = 74
        Me.LabelTO2.Text = "TO"
        '
        'DateTimePicker2Void
        '
        Me.DateTimePicker2Void.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker2Void.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2Void.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2Void.Location = New System.Drawing.Point(800, 522)
        Me.DateTimePicker2Void.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2Void.Name = "DateTimePicker2Void"
        Me.DateTimePicker2Void.Size = New System.Drawing.Size(304, 34)
        Me.DateTimePicker2Void.TabIndex = 73
        Me.DateTimePicker2Void.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'DateTimePicker1Void
        '
        Me.DateTimePicker1Void.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker1Void.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1Void.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1Void.Location = New System.Drawing.Point(452, 523)
        Me.DateTimePicker1Void.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1Void.Name = "DateTimePicker1Void"
        Me.DateTimePicker1Void.Size = New System.Drawing.Size(304, 34)
        Me.DateTimePicker1Void.TabIndex = 72
        Me.DateTimePicker1Void.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'ButtonSearchVoid
        '
        Me.ButtonSearchVoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSearchVoid.Location = New System.Drawing.Point(1112, 522)
        Me.ButtonSearchVoid.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSearchVoid.Name = "ButtonSearchVoid"
        Me.ButtonSearchVoid.Size = New System.Drawing.Size(115, 36)
        Me.ButtonSearchVoid.TabIndex = 71
        Me.ButtonSearchVoid.Text = "Search"
        Me.ButtonSearchVoid.UseVisualStyleBackColor = True
        '
        'ButtonShowAllVoid
        '
        Me.ButtonShowAllVoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonShowAllVoid.Location = New System.Drawing.Point(16, 522)
        Me.ButtonShowAllVoid.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonShowAllVoid.Name = "ButtonShowAllVoid"
        Me.ButtonShowAllVoid.Size = New System.Drawing.Size(115, 36)
        Me.ButtonShowAllVoid.TabIndex = 70
        Me.ButtonShowAllVoid.Text = "Show All"
        Me.ButtonShowAllVoid.UseVisualStyleBackColor = True
        '
        'ReportViewerVoid
        '
        ReportDataSource2.Name = "void_tblDataSet"
        ReportDataSource2.Value = Me.void_tblBindingSource
        Me.ReportViewerVoid.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewerVoid.LocalReport.ReportEmbeddedResource = "WindowsApplication1.ReportVoid.rdlc"
        Me.ReportViewerVoid.Location = New System.Drawing.Point(16, 607)
        Me.ReportViewerVoid.Margin = New System.Windows.Forms.Padding(4)
        Me.ReportViewerVoid.Name = "ReportViewerVoid"
        Me.ReportViewerVoid.Size = New System.Drawing.Size(1333, 272)
        Me.ReportViewerVoid.TabIndex = 69
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'ComboBoxSalesCashier
        '
        Me.ComboBoxSalesCashier.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxSalesCashier.FormattingEnabled = True
        Me.ComboBoxSalesCashier.Location = New System.Drawing.Point(452, 144)
        Me.ComboBoxSalesCashier.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxSalesCashier.Name = "ComboBoxSalesCashier"
        Me.ComboBoxSalesCashier.Size = New System.Drawing.Size(304, 34)
        Me.ComboBoxSalesCashier.TabIndex = 76
        '
        'ComboBoxVoidCashier
        '
        Me.ComboBoxVoidCashier.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxVoidCashier.FormattingEnabled = True
        Me.ComboBoxVoidCashier.Location = New System.Drawing.Point(452, 565)
        Me.ComboBoxVoidCashier.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxVoidCashier.Name = "ComboBoxVoidCashier"
        Me.ComboBoxVoidCashier.Size = New System.Drawing.Size(304, 34)
        Me.ComboBoxVoidCashier.TabIndex = 77
        '
        'void_tblTableAdapter
        '
        Me.void_tblTableAdapter.ClearBeforeFill = True
        '
        'VoidtblBindingSource
        '
        Me.VoidtblBindingSource.DataMember = "void_tbl"
        Me.VoidtblBindingSource.DataSource = Me.void_tblDataSet
        '
        'ComboBoxSalesType
        '
        Me.ComboBoxSalesType.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxSalesType.FormattingEnabled = True
        Me.ComboBoxSalesType.Items.AddRange(New Object() {"Regular", "Student", "Senior", "PWD", "Zero Rated", "Employee"})
        Me.ComboBoxSalesType.Location = New System.Drawing.Point(800, 144)
        Me.ComboBoxSalesType.Name = "ComboBoxSalesType"
        Me.ComboBoxSalesType.Size = New System.Drawing.Size(304, 33)
        Me.ComboBoxSalesType.TabIndex = 78
        '
        'or_tblTableAdapter
        '
        Me.or_tblTableAdapter.ClearBeforeFill = True
        '
        'OrtblDataSetBindingSource
        '
        Me.OrtblDataSetBindingSource.DataSource = Me.or_tblDataSet
        Me.OrtblDataSetBindingSource.Position = 0
        '
        'ComboBoxVoidType
        '
        Me.ComboBoxVoidType.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxVoidType.FormattingEnabled = True
        Me.ComboBoxVoidType.Items.AddRange(New Object() {"Regular", "Student", "Senior", "PWD", "Zero Rated", "Employee"})
        Me.ComboBoxVoidType.Location = New System.Drawing.Point(800, 566)
        Me.ComboBoxVoidType.Name = "ComboBoxVoidType"
        Me.ComboBoxVoidType.Size = New System.Drawing.Size(304, 33)
        Me.ComboBoxVoidType.TabIndex = 79
        '
        'ButtonSalesSummary
        '
        Me.ButtonSalesSummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSalesSummary.Location = New System.Drawing.Point(1112, 144)
        Me.ButtonSalesSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSalesSummary.Name = "ButtonSalesSummary"
        Me.ButtonSalesSummary.Size = New System.Drawing.Size(238, 34)
        Me.ButtonSalesSummary.TabIndex = 80
        Me.ButtonSalesSummary.Text = "Sales Summary Report"
        Me.ButtonSalesSummary.UseVisualStyleBackColor = True
        '
        'reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1365, 945)
        Me.Controls.Add(Me.ButtonSalesSummary)
        Me.Controls.Add(Me.ComboBoxVoidType)
        Me.Controls.Add(Me.ComboBoxSalesType)
        Me.Controls.Add(Me.ComboBoxVoidCashier)
        Me.Controls.Add(Me.ComboBoxSalesCashier)
        Me.Controls.Add(Me.LabelVoid)
        Me.Controls.Add(Me.LabelTO2)
        Me.Controls.Add(Me.DateTimePicker2Void)
        Me.Controls.Add(Me.DateTimePicker1Void)
        Me.Controls.Add(Me.ButtonSearchVoid)
        Me.Controls.Add(Me.ButtonShowAllVoid)
        Me.Controls.Add(Me.ReportViewerVoid)
        Me.Controls.Add(Me.LabelSales)
        Me.Controls.Add(Me.LabelTO)
        Me.Controls.Add(Me.DateTimePicker2Sales)
        Me.Controls.Add(Me.DateTimePicker1Sales)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.ButtonShowAll)
        Me.Controls.Add(Me.ReportViewerSales)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "reports"
        Me.Text = "reports"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.or_tblBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.or_tblDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.void_tblBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.void_tblDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFooter.ResumeLayout(False)
        Me.PanelFooter.PerformLayout()
        CType(Me.VoidtblBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OrtblDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ButtonClose As Button
    Friend WithEvents ReportViewerSales As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents or_tblDataSet As or_tblDataSet
    Friend WithEvents or_tblTableAdapter As or_tblDataSetTableAdapters.or_tblTableAdapter
    Friend WithEvents LabelSales As Label
    Friend WithEvents LabelTO As Label
    Friend WithEvents DateTimePicker2Sales As DateTimePicker
    Friend WithEvents DateTimePicker1Sales As DateTimePicker
    Friend WithEvents ButtonSearch As Button
    Friend WithEvents ButtonShowAll As Button
    Friend WithEvents OrtblDataSetBindingSource As BindingSource
    Friend WithEvents or_tblBindingSource As BindingSource
    Friend WithEvents LabelPOSno As Label
    Friend WithEvents LabelVoid As Label
    Friend WithEvents LabelTO2 As Label
    Friend WithEvents DateTimePicker2Void As DateTimePicker
    Friend WithEvents DateTimePicker1Void As DateTimePicker
    Friend WithEvents ButtonSearchVoid As Button
    Friend WithEvents ButtonShowAllVoid As Button
    Friend WithEvents ReportViewerVoid As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents void_tblBindingSource As BindingSource
    Friend WithEvents void_tblDataSet As void_tblDataSet
    Friend WithEvents void_tblTableAdapter As void_tblDataSetTableAdapters.void_tblTableAdapter
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ComboBoxSalesCashier As ComboBox
    Friend WithEvents VoidtblBindingSource As BindingSource
    Friend WithEvents ComboBoxVoidCashier As ComboBox
    Friend WithEvents PictureBoxCompanyLogo As PictureBox
    Friend WithEvents LabelStationID As Label
    Friend WithEvents LabelStation As Label
    Friend WithEvents ComboBoxSalesType As ComboBox
    Friend WithEvents ComboBoxVoidType As ComboBox
    Friend WithEvents ButtonSalesSummary As Button
End Class
