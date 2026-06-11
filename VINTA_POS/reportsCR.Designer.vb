<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class reportsCR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(reportsCR))
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.PictureBoxVINTALogo = New System.Windows.Forms.PictureBox()
        Me.PictureBoxCompanyLogo = New System.Windows.Forms.PictureBox()
        Me.LabelManagement = New System.Windows.Forms.Label()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.LabelPOSno = New System.Windows.Forms.Label()
        Me.LabelUser = New System.Windows.Forms.Label()
        Me.LabelUserID = New System.Windows.Forms.Label()
        Me.LabelUserName = New System.Windows.Forms.Label()
        Me.LabelPOS = New System.Windows.Forms.Label()
        Me.LabelDateTime = New System.Windows.Forms.Label()
        Me.ButtonShowAll = New System.Windows.Forms.Button()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.LabelTO = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.LabelTO2 = New System.Windows.Forms.Label()
        Me.DateTimePickerEnd2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerStart2 = New System.Windows.Forms.DateTimePicker()
        Me.ButtonSearch2 = New System.Windows.Forms.Button()
        Me.ButtonShowAll2 = New System.Windows.Forms.Button()
        Me.LabelSales = New System.Windows.Forms.Label()
        Me.LabelEJournal = New System.Windows.Forms.Label()
        Me.CrystalReportViewer2 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.PanelHeader.SuspendLayout()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PanelHeader.Controls.Add(Me.PictureBoxVINTALogo)
        Me.PanelHeader.Controls.Add(Me.PictureBoxCompanyLogo)
        Me.PanelHeader.Controls.Add(Me.LabelManagement)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(1365, 97)
        Me.PanelHeader.TabIndex = 46
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
        'PictureBoxCompanyLogo
        '
        Me.PictureBoxCompanyLogo.Location = New System.Drawing.Point(-8, -43)
        Me.PictureBoxCompanyLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxCompanyLogo.Name = "PictureBoxCompanyLogo"
        Me.PictureBoxCompanyLogo.Size = New System.Drawing.Size(251, 180)
        Me.PictureBoxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxCompanyLogo.TabIndex = 5
        Me.PictureBoxCompanyLogo.TabStop = False
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
        Me.PanelFooter.TabIndex = 47
        '
        'LabelPOSno
        '
        Me.LabelPOSno.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPOSno.AutoSize = True
        Me.LabelPOSno.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelPOSno.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPOSno.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelPOSno.Location = New System.Drawing.Point(700, 12)
        Me.LabelPOSno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPOSno.Name = "LabelPOSno"
        Me.LabelPOSno.Size = New System.Drawing.Size(0, 29)
        Me.LabelPOSno.TabIndex = 87
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
        Me.LabelPOS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.LabelDateTime.Location = New System.Drawing.Point(1013, 12)
        Me.LabelDateTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelDateTime.Name = "LabelDateTime"
        Me.LabelDateTime.Size = New System.Drawing.Size(0, 29)
        Me.LabelDateTime.TabIndex = 80
        '
        'ButtonShowAll
        '
        Me.ButtonShowAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonShowAll.Location = New System.Drawing.Point(16, 103)
        Me.ButtonShowAll.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonShowAll.Name = "ButtonShowAll"
        Me.ButtonShowAll.Size = New System.Drawing.Size(115, 36)
        Me.ButtonShowAll.TabIndex = 49
        Me.ButtonShowAll.Text = "Show All"
        Me.ButtonShowAll.UseVisualStyleBackColor = True
        '
        'ButtonClose
        '
        Me.ButtonClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClose.Location = New System.Drawing.Point(1235, 105)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(115, 36)
        Me.ButtonClose.TabIndex = 54
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'LabelTO
        '
        Me.LabelTO.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTO.AutoSize = True
        Me.LabelTO.Location = New System.Drawing.Point(844, 114)
        Me.LabelTO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTO.Name = "LabelTO"
        Me.LabelTO.Size = New System.Drawing.Size(28, 17)
        Me.LabelTO.TabIndex = 53
        Me.LabelTO.Text = "TO"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(881, 105)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(160, 34)
        Me.DateTimePicker2.TabIndex = 52
        Me.DateTimePicker2.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(675, 105)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(160, 34)
        Me.DateTimePicker1.TabIndex = 51
        Me.DateTimePicker1.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSearch.Location = New System.Drawing.Point(1051, 105)
        Me.ButtonSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(115, 36)
        Me.ButtonSearch.TabIndex = 50
        Me.ButtonSearch.Text = "Search"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'LabelTO2
        '
        Me.LabelTO2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTO2.AutoSize = True
        Me.LabelTO2.Location = New System.Drawing.Point(844, 527)
        Me.LabelTO2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTO2.Name = "LabelTO2"
        Me.LabelTO2.Size = New System.Drawing.Size(28, 17)
        Me.LabelTO2.TabIndex = 60
        Me.LabelTO2.Text = "TO"
        '
        'DateTimePickerEnd2
        '
        Me.DateTimePickerEnd2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerEnd2.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerEnd2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerEnd2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerEnd2.Location = New System.Drawing.Point(881, 517)
        Me.DateTimePickerEnd2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerEnd2.Name = "DateTimePickerEnd2"
        Me.DateTimePickerEnd2.Size = New System.Drawing.Size(160, 34)
        Me.DateTimePickerEnd2.TabIndex = 59
        Me.DateTimePickerEnd2.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'DateTimePickerStart2
        '
        Me.DateTimePickerStart2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerStart2.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePickerStart2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerStart2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerStart2.Location = New System.Drawing.Point(675, 517)
        Me.DateTimePickerStart2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerStart2.Name = "DateTimePickerStart2"
        Me.DateTimePickerStart2.Size = New System.Drawing.Size(160, 34)
        Me.DateTimePickerStart2.TabIndex = 58
        Me.DateTimePickerStart2.Value = New Date(2018, 8, 7, 0, 0, 0, 0)
        '
        'ButtonSearch2
        '
        Me.ButtonSearch2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSearch2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSearch2.Location = New System.Drawing.Point(1051, 517)
        Me.ButtonSearch2.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSearch2.Name = "ButtonSearch2"
        Me.ButtonSearch2.Size = New System.Drawing.Size(115, 36)
        Me.ButtonSearch2.TabIndex = 57
        Me.ButtonSearch2.Text = "Search"
        Me.ButtonSearch2.UseVisualStyleBackColor = True
        '
        'ButtonShowAll2
        '
        Me.ButtonShowAll2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonShowAll2.Location = New System.Drawing.Point(16, 517)
        Me.ButtonShowAll2.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonShowAll2.Name = "ButtonShowAll2"
        Me.ButtonShowAll2.Size = New System.Drawing.Size(115, 36)
        Me.ButtonShowAll2.TabIndex = 56
        Me.ButtonShowAll2.Text = "Show All"
        Me.ButtonShowAll2.UseVisualStyleBackColor = True
        '
        'LabelSales
        '
        Me.LabelSales.AutoSize = True
        Me.LabelSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSales.Location = New System.Drawing.Point(201, 108)
        Me.LabelSales.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSales.Name = "LabelSales"
        Me.LabelSales.Size = New System.Drawing.Size(313, 31)
        Me.LabelSales.TabIndex = 61
        Me.LabelSales.Text = "Sales Summary Report"
        '
        'LabelEJournal
        '
        Me.LabelEJournal.AutoSize = True
        Me.LabelEJournal.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEJournal.Location = New System.Drawing.Point(201, 521)
        Me.LabelEJournal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelEJournal.Name = "LabelEJournal"
        Me.LabelEJournal.Size = New System.Drawing.Size(139, 31)
        Me.LabelEJournal.TabIndex = 62
        Me.LabelEJournal.Text = "E-Journal"
        '
        'CrystalReportViewer2
        '
        Me.CrystalReportViewer2.ActiveViewIndex = -1
        Me.CrystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer2.Location = New System.Drawing.Point(16, 560)
        Me.CrystalReportViewer2.Margin = New System.Windows.Forms.Padding(4)
        Me.CrystalReportViewer2.Name = "CrystalReportViewer2"
        Me.CrystalReportViewer2.Size = New System.Drawing.Size(1333, 326)
        Me.CrystalReportViewer2.TabIndex = 55
        Me.CrystalReportViewer2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(16, 148)
        Me.CrystalReportViewer1.Margin = New System.Windows.Forms.Padding(4)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1333, 348)
        Me.CrystalReportViewer1.TabIndex = 48
        Me.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'reportsCR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1365, 945)
        Me.Controls.Add(Me.LabelEJournal)
        Me.Controls.Add(Me.LabelSales)
        Me.Controls.Add(Me.LabelTO2)
        Me.Controls.Add(Me.DateTimePickerEnd2)
        Me.Controls.Add(Me.DateTimePickerStart2)
        Me.Controls.Add(Me.ButtonSearch2)
        Me.Controls.Add(Me.ButtonShowAll2)
        Me.Controls.Add(Me.CrystalReportViewer2)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.LabelTO)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.ButtonShowAll)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.PanelHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "reportsCR"
        Me.Text = " "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFooter.ResumeLayout(False)
        Me.PanelFooter.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelHeader As Panel
    Friend WithEvents PictureBoxVINTALogo As PictureBox
    Friend WithEvents PictureBoxCompanyLogo As PictureBox
    Friend WithEvents LabelManagement As Label
    Friend WithEvents PanelFooter As Panel
    Friend WithEvents LabelUser As Label
    Friend WithEvents LabelUserID As Label
    Friend WithEvents LabelUserName As Label
    Friend WithEvents LabelPOS As Label
    Friend WithEvents LabelDateTime As Label
    'Friend WithEvents CrystalReportOR1 As CrystalReportOR
    Friend WithEvents ButtonShowAll As Button
    Friend WithEvents ButtonClose As Button
    Friend WithEvents LabelTO As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents ButtonSearch As Button
    Friend WithEvents LabelTO2 As Label
    Friend WithEvents DateTimePickerEnd2 As DateTimePicker
    Friend WithEvents DateTimePickerStart2 As DateTimePicker
    Friend WithEvents ButtonSearch2 As Button
    Friend WithEvents ButtonShowAll2 As Button
    'Friend WithEvents CrystalReportEJ1 As CrystalReportEJ
    Friend WithEvents LabelSales As Label
    Friend WithEvents LabelEJournal As Label
    Friend WithEvents LabelPOSno As Label
    Private WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Private WithEvents CrystalReportViewer2 As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
