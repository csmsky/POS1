<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class secondscreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(secondscreen))
        Me.LabelPaymentDue = New System.Windows.Forms.Label()
        Me.LabelChange = New System.Windows.Forms.Label()
        Me.Seat = New System.Windows.Forms.Label()
        Me.PaymentDue = New System.Windows.Forms.Label()
        Me.Change = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelTotalCustomerView = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelCashTendered = New System.Windows.Forms.Label()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelChangeCustomerChange = New System.Windows.Forms.Label()
        Me.ListViewCustomerView = New System.Windows.Forms.ListView()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.pbSide = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.Accumulated_amount_tblDataSet1 = New WindowsApplication1.accumulated_amount_tblDataSet()
        Me.AxVideoHeader = New AxWMPLib.AxWindowsMediaPlayer()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        CType(Me.pbSide, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel8.SuspendLayout()
        CType(Me.Accumulated_amount_tblDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxVideoHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelPaymentDue
        '
        Me.LabelPaymentDue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelPaymentDue.AutoSize = True
        Me.LabelPaymentDue.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPaymentDue.Location = New System.Drawing.Point(4, 4)
        Me.LabelPaymentDue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelPaymentDue.Name = "LabelPaymentDue"
        Me.LabelPaymentDue.Size = New System.Drawing.Size(118, 20)
        Me.LabelPaymentDue.TabIndex = 0
        Me.LabelPaymentDue.Text = "Amount Due:"
        '
        'LabelChange
        '
        Me.LabelChange.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelChange.AutoSize = True
        Me.LabelChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelChange.Location = New System.Drawing.Point(4, 10)
        Me.LabelChange.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelChange.Name = "LabelChange"
        Me.LabelChange.Size = New System.Drawing.Size(78, 20)
        Me.LabelChange.TabIndex = 1
        Me.LabelChange.Text = "Change:"
        '
        'Seat
        '
        Me.Seat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Seat.AutoSize = True
        Me.Seat.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Seat.Location = New System.Drawing.Point(549, 11)
        Me.Seat.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Seat.Name = "Seat"
        Me.Seat.Size = New System.Drawing.Size(0, 31)
        Me.Seat.TabIndex = 3
        '
        'PaymentDue
        '
        Me.PaymentDue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PaymentDue.AutoSize = True
        Me.PaymentDue.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PaymentDue.Location = New System.Drawing.Point(549, 54)
        Me.PaymentDue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PaymentDue.Name = "PaymentDue"
        Me.PaymentDue.Size = New System.Drawing.Size(0, 31)
        Me.PaymentDue.TabIndex = 4
        '
        'Change
        '
        Me.Change.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Change.AutoSize = True
        Me.Change.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Change.Location = New System.Drawing.Point(549, 96)
        Me.Change.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Change.Name = "Change"
        Me.Change.Size = New System.Drawing.Size(0, 31)
        Me.Change.TabIndex = 5
        '
        'Timer1
        '
        Me.Timer1.Interval = 2000
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ListViewCustomerView, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(353, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.63636!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.36364!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(604, 375)
        Me.TableLayoutPanel1.TabIndex = 9
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(6, 241)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.14815!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.85185!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(592, 128)
        Me.TableLayoutPanel2.TabIndex = 8
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.LabelPaymentDue, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LabelTotalCustomerView, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(6, 6)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(580, 28)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'LabelTotalCustomerView
        '
        Me.LabelTotalCustomerView.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelTotalCustomerView.AutoSize = True
        Me.LabelTotalCustomerView.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTotalCustomerView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LabelTotalCustomerView.Location = New System.Drawing.Point(293, 4)
        Me.LabelTotalCustomerView.Name = "LabelTotalCustomerView"
        Me.LabelTotalCustomerView.Size = New System.Drawing.Size(44, 20)
        Me.LabelTotalCustomerView.TabIndex = 1
        Me.LabelTotalCustomerView.Text = "0.00"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LabelCashTendered, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(6, 43)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(580, 30)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cash Tendered:"
        '
        'LabelCashTendered
        '
        Me.LabelCashTendered.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelCashTendered.AutoSize = True
        Me.LabelCashTendered.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCashTendered.Location = New System.Drawing.Point(293, 5)
        Me.LabelCashTendered.Name = "LabelCashTendered"
        Me.LabelCashTendered.Size = New System.Drawing.Size(44, 20)
        Me.LabelCashTendered.TabIndex = 1
        Me.LabelCashTendered.Text = "0.00"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.LabelChangeCustomerChange, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.LabelChange, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(6, 82)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(580, 40)
        Me.TableLayoutPanel7.TabIndex = 2
        '
        'LabelChangeCustomerChange
        '
        Me.LabelChangeCustomerChange.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelChangeCustomerChange.AutoSize = True
        Me.LabelChangeCustomerChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelChangeCustomerChange.ForeColor = System.Drawing.Color.Black
        Me.LabelChangeCustomerChange.Location = New System.Drawing.Point(293, 10)
        Me.LabelChangeCustomerChange.Name = "LabelChangeCustomerChange"
        Me.LabelChangeCustomerChange.Size = New System.Drawing.Size(44, 20)
        Me.LabelChangeCustomerChange.TabIndex = 2
        Me.LabelChangeCustomerChange.Text = "0.00"
        Me.LabelChangeCustomerChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ListViewCustomerView
        '
        Me.ListViewCustomerView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewCustomerView.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewCustomerView.HideSelection = False
        Me.ListViewCustomerView.Location = New System.Drawing.Point(6, 6)
        Me.ListViewCustomerView.Name = "ListViewCustomerView"
        Me.ListViewCustomerView.Size = New System.Drawing.Size(592, 226)
        Me.ListViewCustomerView.TabIndex = 9
        Me.ListViewCustomerView.UseCompatibleStateImageBehavior = False
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.71429!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.28571!))
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel6, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel8, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.11864!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.88136!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(972, 539)
        Me.TableLayoutPanel5.TabIndex = 10
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.52695!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.47305!))
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel1, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.pbSide, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(6, 152)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(960, 381)
        Me.TableLayoutPanel6.TabIndex = 10
        '
        'pbSide
        '
        Me.pbSide.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbSide.Location = New System.Drawing.Point(3, 3)
        Me.pbSide.Name = "pbSide"
        Me.pbSide.Size = New System.Drawing.Size(344, 375)
        Me.pbSide.TabIndex = 10
        Me.pbSide.TabStop = False
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.AxVideoHeader, 0, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(6, 6)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(960, 137)
        Me.TableLayoutPanel8.TabIndex = 11
        '
        'Accumulated_amount_tblDataSet1
        '
        Me.Accumulated_amount_tblDataSet1.DataSetName = "accumulated_amount_tblDataSet"
        Me.Accumulated_amount_tblDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'AxVideoHeader
        '
        Me.AxVideoHeader.Enabled = True
        Me.AxVideoHeader.Location = New System.Drawing.Point(3, 3)
        Me.AxVideoHeader.Name = "AxVideoHeader"
        Me.AxVideoHeader.OcxState = CType(resources.GetObject("AxVideoHeader.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxVideoHeader.Size = New System.Drawing.Size(954, 130)
        Me.AxVideoHeader.TabIndex = 0
        '
        'secondscreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(972, 539)
        Me.Controls.Add(Me.TableLayoutPanel5)
        Me.Controls.Add(Me.Change)
        Me.Controls.Add(Me.PaymentDue)
        Me.Controls.Add(Me.Seat)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "secondscreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        CType(Me.pbSide, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel8.ResumeLayout(False)
        CType(Me.Accumulated_amount_tblDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxVideoHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelPaymentDue As Label
    Friend WithEvents LabelChange As Label
    Friend WithEvents Seat As Label
    Friend WithEvents PaymentDue As Label
    Friend WithEvents Change As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents LabelTotalCustomerView As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents LabelChangeCustomerChange As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LabelCashTendered As Label
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents Accumulated_amount_tblDataSet1 As accumulated_amount_tblDataSet
    Friend WithEvents ListViewCustomerView As ListView
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents AxVideoHeader As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents pbSide As PictureBox
End Class
