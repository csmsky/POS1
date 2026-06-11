<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class backendreport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(backendreport))
        Me.accumulated_amount_tblBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.accumulated_amount_tblDataSet = New WindowsApplication1.accumulated_amount_tblDataSet()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.LabelPOSno = New System.Windows.Forms.Label()
        Me.LabelUser = New System.Windows.Forms.Label()
        Me.LabelUserID = New System.Windows.Forms.Label()
        Me.LabelUserName = New System.Windows.Forms.Label()
        Me.LabelPOS = New System.Windows.Forms.Label()
        Me.LabelDateTime = New System.Windows.Forms.Label()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.LabelStationID = New System.Windows.Forms.Label()
        Me.LabelStation = New System.Windows.Forms.Label()
        Me.PictureBoxCompanyLogo = New System.Windows.Forms.PictureBox()
        Me.PictureBoxVINTALogo = New System.Windows.Forms.PictureBox()
        Me.LabelManagement = New System.Windows.Forms.Label()
        Me.LabelSales = New System.Windows.Forms.Label()
        Me.DateTimePicker2Sales = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1Sales = New System.Windows.Forms.DateTimePicker()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.ButtonShowAll = New System.Windows.Forms.Button()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.LabelTO = New System.Windows.Forms.Label()
        Me.ReportViewerBackEnd = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.accumulated_amount_tblTableAdapter = New WindowsApplication1.accumulated_amount_tblDataSetTableAdapters.accumulated_amount_tblTableAdapter()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.accumulated_amount_tblBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.accumulated_amount_tblDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFooter.SuspendLayout()
        Me.PanelHeader.SuspendLayout()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'accumulated_amount_tblBindingSource
        '
        Me.accumulated_amount_tblBindingSource.DataMember = "accumulated_amount_tbl"
        Me.accumulated_amount_tblBindingSource.DataSource = Me.accumulated_amount_tblDataSet
        '
        'accumulated_amount_tblDataSet
        '
        Me.accumulated_amount_tblDataSet.DataSetName = "accumulated_amount_tblDataSet"
        Me.accumulated_amount_tblDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.PanelFooter.TabIndex = 50
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
        Me.PanelHeader.TabIndex = 49
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
        'LabelSales
        '
        Me.LabelSales.AutoSize = True
        Me.LabelSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSales.Location = New System.Drawing.Point(136, 107)
        Me.LabelSales.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSales.Name = "LabelSales"
        Me.LabelSales.Size = New System.Drawing.Size(313, 31)
        Me.LabelSales.TabIndex = 83
        Me.LabelSales.Text = "Sales Summary Report"
        '
        'DateTimePicker2Sales
        '
        Me.DateTimePicker2Sales.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker2Sales.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2Sales.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2Sales.Location = New System.Drawing.Point(945, 103)
        Me.DateTimePicker2Sales.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2Sales.Name = "DateTimePicker2Sales"
        Me.DateTimePicker2Sales.Size = New System.Drawing.Size(160, 34)
        Me.DateTimePicker2Sales.TabIndex = 82
        Me.DateTimePicker2Sales.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'DateTimePicker1Sales
        '
        Me.DateTimePicker1Sales.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker1Sales.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1Sales.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1Sales.Location = New System.Drawing.Point(738, 103)
        Me.DateTimePicker1Sales.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1Sales.Name = "DateTimePicker1Sales"
        Me.DateTimePicker1Sales.Size = New System.Drawing.Size(160, 34)
        Me.DateTimePicker1Sales.TabIndex = 81
        Me.DateTimePicker1Sales.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSearch.Location = New System.Drawing.Point(1114, 103)
        Me.ButtonSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(115, 36)
        Me.ButtonSearch.TabIndex = 80
        Me.ButtonSearch.Text = "Search"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'ButtonShowAll
        '
        Me.ButtonShowAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonShowAll.Location = New System.Drawing.Point(13, 103)
        Me.ButtonShowAll.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonShowAll.Name = "ButtonShowAll"
        Me.ButtonShowAll.Size = New System.Drawing.Size(115, 36)
        Me.ButtonShowAll.TabIndex = 79
        Me.ButtonShowAll.Text = "Show All"
        Me.ButtonShowAll.UseVisualStyleBackColor = True
        '
        'ButtonClose
        '
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.Location = New System.Drawing.Point(1237, 103)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(115, 36)
        Me.ButtonClose.TabIndex = 77
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'LabelTO
        '
        Me.LabelTO.AutoSize = True
        Me.LabelTO.Location = New System.Drawing.Point(909, 114)
        Me.LabelTO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTO.Name = "LabelTO"
        Me.LabelTO.Size = New System.Drawing.Size(28, 17)
        Me.LabelTO.TabIndex = 84
        Me.LabelTO.Text = "TO"
        '
        'ReportViewerBackEnd
        '
        ReportDataSource1.Name = "accumulated_amount_tblDataSet"
        ReportDataSource1.Value = Me.accumulated_amount_tblBindingSource
        Me.ReportViewerBackEnd.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewerBackEnd.LocalReport.ReportEmbeddedResource = "WindowsApplication1.ReportBackEnd.rdlc"
        Me.ReportViewerBackEnd.Location = New System.Drawing.Point(13, 146)
        Me.ReportViewerBackEnd.Name = "ReportViewerBackEnd"
        Me.ReportViewerBackEnd.Size = New System.Drawing.Size(1339, 741)
        Me.ReportViewerBackEnd.TabIndex = 85
        '
        'accumulated_amount_tblTableAdapter
        '
        Me.accumulated_amount_tblTableAdapter.ClearBeforeFill = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'backendreport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1365, 945)
        Me.Controls.Add(Me.ReportViewerBackEnd)
        Me.Controls.Add(Me.LabelTO)
        Me.Controls.Add(Me.LabelSales)
        Me.Controls.Add(Me.DateTimePicker2Sales)
        Me.Controls.Add(Me.DateTimePicker1Sales)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.ButtonShowAll)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "backendreport"
        Me.Text = "backendreport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.accumulated_amount_tblBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.accumulated_amount_tblDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFooter.ResumeLayout(False)
        Me.PanelFooter.PerformLayout()
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelFooter As Panel
    Friend WithEvents LabelPOSno As Label
    Friend WithEvents LabelUser As Label
    Friend WithEvents LabelUserID As Label
    Friend WithEvents LabelUserName As Label
    Friend WithEvents LabelPOS As Label
    Friend WithEvents LabelDateTime As Label
    Friend WithEvents PanelHeader As Panel
    Friend WithEvents LabelStationID As Label
    Friend WithEvents LabelStation As Label
    Friend WithEvents PictureBoxCompanyLogo As PictureBox
    Friend WithEvents PictureBoxVINTALogo As PictureBox
    Friend WithEvents LabelManagement As Label
    Friend WithEvents LabelSales As Label
    Friend WithEvents DateTimePicker2Sales As DateTimePicker
    Friend WithEvents DateTimePicker1Sales As DateTimePicker
    Friend WithEvents ButtonSearch As Button
    Friend WithEvents ButtonShowAll As Button
    Friend WithEvents ButtonClose As Button
    Friend WithEvents LabelTO As Label
    Friend WithEvents ReportViewerBackEnd As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents accumulated_amount_tblBindingSource As BindingSource
    Friend WithEvents accumulated_amount_tblDataSet As accumulated_amount_tblDataSet
    Friend WithEvents accumulated_amount_tblTableAdapter As accumulated_amount_tblDataSetTableAdapters.accumulated_amount_tblTableAdapter
    Friend WithEvents Timer1 As Timer
End Class
