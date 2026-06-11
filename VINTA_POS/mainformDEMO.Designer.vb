<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class mainformDEMO
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainformDEMO))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.ButtonEmployee = New System.Windows.Forms.Button()
        Me.TextBoxPercentDiscount = New System.Windows.Forms.TextBox()
        Me.LabelVersionNo = New System.Windows.Forms.Label()
        Me.LabelSoftwareName = New System.Windows.Forms.Label()
        Me.ButtonSenior = New System.Windows.Forms.Button()
        Me.LabelPOSno = New System.Windows.Forms.Label()
        Me.PanelFooter = New System.Windows.Forms.Panel()
        Me.LabelCashier = New System.Windows.Forms.Label()
        Me.LabelCashierID = New System.Windows.Forms.Label()
        Me.LabelCashierName = New System.Windows.Forms.Label()
        Me.LabelPOS = New System.Windows.Forms.Label()
        Me.LabelDateTime = New System.Windows.Forms.Label()
        Me.LabelDestination = New System.Windows.Forms.Label()
        Me.LabelOrigin = New System.Windows.Forms.Label()
        Me.ComboBoxTime = New System.Windows.Forms.ComboBox()
        Me.CheckBoxPercentDiscount = New System.Windows.Forms.CheckBox()
        Me.TextBoxSeat = New System.Windows.Forms.TextBox()
        Me.TextBoxBarcode = New System.Windows.Forms.TextBox()
        Me.LabelPayment = New System.Windows.Forms.Label()
        Me.ButtonSeat = New System.Windows.Forms.Button()
        Me.PictureBoxBarcode = New System.Windows.Forms.PictureBox()
        Me.LabelDiscount = New System.Windows.Forms.Label()
        Me.ButtonDestination = New System.Windows.Forms.Button()
        Me.LabelVAT = New System.Windows.Forms.Label()
        Me.DestinationID = New System.Windows.Forms.Label()
        Me.LabelTime = New System.Windows.Forms.Label()
        Me.LabelVATable = New System.Windows.Forms.Label()
        Me.OriginID = New System.Windows.Forms.Label()
        Me.ButtonVoid = New System.Windows.Forms.Button()
        Me.ButtonZeroRated = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.PictureBoxCompanyLogo = New System.Windows.Forms.PictureBox()
        Me.TextBoxBarcodeTicket = New System.Windows.Forms.TextBox()
        Me.PictureBoxBarcodeTicket = New System.Windows.Forms.PictureBox()
        Me.LabelTotal = New System.Windows.Forms.Label()
        Me.LabelTotalText = New System.Windows.Forms.Label()
        Me.TextBoxCardAmount = New System.Windows.Forms.TextBox()
        Me.LabelTrainingMode = New System.Windows.Forms.Label()
        Me.LabelStationID = New System.Windows.Forms.Label()
        Me.TextBoxUserID = New System.Windows.Forms.TextBox()
        Me.LabelStation = New System.Windows.Forms.Label()
        Me.ButtonActualMode = New System.Windows.Forms.Button()
        Me.LabelCardAmount = New System.Windows.Forms.Label()
        Me.PictureBoxVINTALogo = New System.Windows.Forms.PictureBox()
        Me.TextBoxBaggageCost = New System.Windows.Forms.TextBox()
        Me.PictureBoxLogoReceipt = New System.Windows.Forms.PictureBox()
        Me.ButtonCard = New System.Windows.Forms.Button()
        Me.LabelType = New System.Windows.Forms.Label()
        Me.ButtonOrigin = New System.Windows.Forms.Button()
        Me.LabelSerial = New System.Windows.Forms.Label()
        Me.ButtonStudent = New System.Windows.Forms.Button()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.LabelBaggageCost = New System.Windows.Forms.Label()
        Me.ButtonPWD = New System.Windows.Forms.Button()
        Me.LabelFare = New System.Windows.Forms.Label()
        Me.Passenger_idTextBox = New System.Windows.Forms.TextBox()
        Me.ComboReaderNames = New System.Windows.Forms.ComboBox()
        Me.LabelVATExempt = New System.Windows.Forms.Label()
        Me.LabelLoginID = New System.Windows.Forms.Label()
        Me.ButtonBaggage = New System.Windows.Forms.Button()
        Me.LabelZeroRated = New System.Windows.Forms.Label()
        Me.LabelMoney = New System.Windows.Forms.Label()
        Me.TextBoxTripFare = New System.Windows.Forms.TextBox()
        Me.pass_lbl = New System.Windows.Forms.Label()
        Me.LabelProfession = New System.Windows.Forms.Label()
        Me.GroupBoxPassengerInfo = New System.Windows.Forms.GroupBox()
        Me.PictureBoxFace = New System.Windows.Forms.PictureBox()
        Me.LabelTicket = New System.Windows.Forms.Label()
        Me.LabelOR = New System.Windows.Forms.Label()
        Me.LabelCardNo = New System.Windows.Forms.Label()
        Me.cardno = New System.Windows.Forms.Label()
        Me.LabelIDNo = New System.Windows.Forms.Label()
        Me.idtype = New System.Windows.Forms.Label()
        Me.pi_lbl = New System.Windows.Forms.Label()
        Me.lname = New System.Windows.Forms.Label()
        Me.LabelGender = New System.Windows.Forms.Label()
        Me.fname = New System.Windows.Forms.Label()
        Me.LabelFName = New System.Windows.Forms.Label()
        Me.gender = New System.Windows.Forms.Label()
        Me.LabelLName = New System.Windows.Forms.Label()
        Me.profession = New System.Windows.Forms.Label()
        Me.ButtonReprintTicket = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonClear = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.PanelRight = New System.Windows.Forms.Panel()
        Me.ButtonIssueTicket = New System.Windows.Forms.Button()
        Me.Button100 = New System.Windows.Forms.Button()
        Me.Button1000 = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Button00 = New System.Windows.Forms.Button()
        Me.Button500 = New System.Windows.Forms.Button()
        Me.Button200 = New System.Windows.Forms.Button()
        Me.ButtonDot = New System.Windows.Forms.Button()
        Me.Button0 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.LabelChange = New System.Windows.Forms.Label()
        Me.TextBoxChange = New System.Windows.Forms.TextBox()
        Me.TextBoxMoney = New System.Windows.Forms.TextBox()
        Me.LabelTripFare = New System.Windows.Forms.Label()
        Me.GroupBoxPassengerID = New System.Windows.Forms.GroupBox()
        Me.PanelFooter.SuspendLayout()
        CType(Me.PictureBoxBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxBarcodeTicket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLogoReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelHeader.SuspendLayout()
        Me.GroupBoxPassengerInfo.SuspendLayout()
        CType(Me.PictureBoxFace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelRight.SuspendLayout()
        Me.GroupBoxPassengerID.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'PrintDocument2
        '
        '
        'ButtonEmployee
        '
        Me.ButtonEmployee.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonEmployee.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonEmployee.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonEmployee.ForeColor = System.Drawing.Color.Black
        Me.ButtonEmployee.Location = New System.Drawing.Point(110, 818)
        Me.ButtonEmployee.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonEmployee.Name = "ButtonEmployee"
        Me.ButtonEmployee.Size = New System.Drawing.Size(168, 48)
        Me.ButtonEmployee.TabIndex = 366
        Me.ButtonEmployee.Text = "Employee"
        Me.ButtonEmployee.UseVisualStyleBackColor = False
        '
        'TextBoxPercentDiscount
        '
        Me.TextBoxPercentDiscount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPercentDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPercentDiscount.Location = New System.Drawing.Point(202, 669)
        Me.TextBoxPercentDiscount.Name = "TextBoxPercentDiscount"
        Me.TextBoxPercentDiscount.Size = New System.Drawing.Size(100, 30)
        Me.TextBoxPercentDiscount.TabIndex = 360
        Me.TextBoxPercentDiscount.Text = ".20"
        '
        'LabelVersionNo
        '
        Me.LabelVersionNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelVersionNo.AutoSize = True
        Me.LabelVersionNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelVersionNo.Location = New System.Drawing.Point(108, 870)
        Me.LabelVersionNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVersionNo.Name = "LabelVersionNo"
        Me.LabelVersionNo.Size = New System.Drawing.Size(0, 17)
        Me.LabelVersionNo.TabIndex = 359
        '
        'LabelSoftwareName
        '
        Me.LabelSoftwareName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelSoftwareName.AutoSize = True
        Me.LabelSoftwareName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelSoftwareName.Location = New System.Drawing.Point(3, 870)
        Me.LabelSoftwareName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSoftwareName.Name = "LabelSoftwareName"
        Me.LabelSoftwareName.Size = New System.Drawing.Size(60, 17)
        Me.LabelSoftwareName.TabIndex = 358
        Me.LabelSoftwareName.Text = "Version:"
        '
        'ButtonSenior
        '
        Me.ButtonSenior.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonSenior.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonSenior.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSenior.ForeColor = System.Drawing.Color.Black
        Me.ButtonSenior.Location = New System.Drawing.Point(202, 708)
        Me.ButtonSenior.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSenior.Name = "ButtonSenior"
        Me.ButtonSenior.Size = New System.Drawing.Size(168, 48)
        Me.ButtonSenior.TabIndex = 365
        Me.ButtonSenior.Text = "Senior"
        Me.ButtonSenior.UseVisualStyleBackColor = False
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
        'PanelFooter
        '
        Me.PanelFooter.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PanelFooter.Controls.Add(Me.LabelPOSno)
        Me.PanelFooter.Controls.Add(Me.LabelCashier)
        Me.PanelFooter.Controls.Add(Me.LabelCashierID)
        Me.PanelFooter.Controls.Add(Me.LabelCashierName)
        Me.PanelFooter.Controls.Add(Me.LabelPOS)
        Me.PanelFooter.Controls.Add(Me.LabelDateTime)
        Me.PanelFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelFooter.Location = New System.Drawing.Point(0, 892)
        Me.PanelFooter.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelFooter.Name = "PanelFooter"
        Me.PanelFooter.Size = New System.Drawing.Size(1365, 53)
        Me.PanelFooter.TabIndex = 356
        '
        'LabelCashier
        '
        Me.LabelCashier.AutoSize = True
        Me.LabelCashier.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCashier.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelCashier.Location = New System.Drawing.Point(11, 12)
        Me.LabelCashier.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCashier.Name = "LabelCashier"
        Me.LabelCashier.Size = New System.Drawing.Size(110, 29)
        Me.LabelCashier.TabIndex = 50
        Me.LabelCashier.Text = "Cashier:"
        '
        'LabelCashierID
        '
        Me.LabelCashierID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCashierID.AutoSize = True
        Me.LabelCashierID.BackColor = System.Drawing.Color.SteelBlue
        Me.LabelCashierID.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCashierID.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelCashierID.Location = New System.Drawing.Point(443, 12)
        Me.LabelCashierID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCashierID.Name = "LabelCashierID"
        Me.LabelCashierID.Size = New System.Drawing.Size(0, 29)
        Me.LabelCashierID.TabIndex = 86
        '
        'LabelCashierName
        '
        Me.LabelCashierName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCashierName.AutoSize = True
        Me.LabelCashierName.BackColor = System.Drawing.SystemColors.ControlDark
        Me.LabelCashierName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCashierName.ForeColor = System.Drawing.Color.AliceBlue
        Me.LabelCashierName.Location = New System.Drawing.Point(121, 12)
        Me.LabelCashierName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCashierName.Name = "LabelCashierName"
        Me.LabelCashierName.Size = New System.Drawing.Size(0, 29)
        Me.LabelCashierName.TabIndex = 83
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
        'LabelDestination
        '
        Me.LabelDestination.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDestination.AutoSize = True
        Me.LabelDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDestination.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelDestination.Location = New System.Drawing.Point(280, 124)
        Me.LabelDestination.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelDestination.Name = "LabelDestination"
        Me.LabelDestination.Size = New System.Drawing.Size(119, 24)
        Me.LabelDestination.TabIndex = 333
        Me.LabelDestination.Text = "Destination:"
        '
        'LabelOrigin
        '
        Me.LabelOrigin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelOrigin.AutoSize = True
        Me.LabelOrigin.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelOrigin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelOrigin.Location = New System.Drawing.Point(280, 13)
        Me.LabelOrigin.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelOrigin.Name = "LabelOrigin"
        Me.LabelOrigin.Size = New System.Drawing.Size(73, 24)
        Me.LabelOrigin.TabIndex = 332
        Me.LabelOrigin.Text = "Origin:"
        '
        'ComboBoxTime
        '
        Me.ComboBoxTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxTime.FormattingEnabled = True
        Me.ComboBoxTime.Items.AddRange(New Object() {"SAMP1-06:00:00", "SAMP2-06:30:00", "SAMP3-07:00:00", "SAMP4-07:30:00", "SAMP1-08:00:00", "SAMP2-08:30:00", "SAMP3-09:00:00", "SAMP4-09:30:00", "SAMP1-10:00:00", "SAMP2-10:30:00", "SAMP3-11:00:00", "SAMP4-11:30:00", "SAMP1-01:00:00", "SAMP2-01:30:00", "SAMP3-02:00:00", "SAMP4-02:30:00"})
        Me.ComboBoxTime.Location = New System.Drawing.Point(678, 81)
        Me.ComboBoxTime.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxTime.Name = "ComboBoxTime"
        Me.ComboBoxTime.Size = New System.Drawing.Size(261, 37)
        Me.ComboBoxTime.TabIndex = 331
        '
        'CheckBoxPercentDiscount
        '
        Me.CheckBoxPercentDiscount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxPercentDiscount.AutoSize = True
        Me.CheckBoxPercentDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxPercentDiscount.ForeColor = System.Drawing.Color.Black
        Me.CheckBoxPercentDiscount.Location = New System.Drawing.Point(84, 671)
        Me.CheckBoxPercentDiscount.Name = "CheckBoxPercentDiscount"
        Me.CheckBoxPercentDiscount.Size = New System.Drawing.Size(110, 29)
        Me.CheckBoxPercentDiscount.TabIndex = 361
        Me.CheckBoxPercentDiscount.Text = "Discount"
        Me.CheckBoxPercentDiscount.UseVisualStyleBackColor = True
        '
        'TextBoxSeat
        '
        Me.TextBoxSeat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSeat.Enabled = False
        Me.TextBoxSeat.Font = New System.Drawing.Font("Microsoft Sans Serif", 39.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxSeat.Location = New System.Drawing.Point(675, 148)
        Me.TextBoxSeat.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxSeat.Name = "TextBoxSeat"
        Me.TextBoxSeat.Size = New System.Drawing.Size(113, 82)
        Me.TextBoxSeat.TabIndex = 330
        Me.TextBoxSeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBoxBarcode
        '
        Me.TextBoxBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxBarcode.Location = New System.Drawing.Point(766, 42)
        Me.TextBoxBarcode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxBarcode.Name = "TextBoxBarcode"
        Me.TextBoxBarcode.Size = New System.Drawing.Size(24, 30)
        Me.TextBoxBarcode.TabIndex = 326
        '
        'LabelPayment
        '
        Me.LabelPayment.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LabelPayment.AutoSize = True
        Me.LabelPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 23.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPayment.ForeColor = System.Drawing.Color.Black
        Me.LabelPayment.Location = New System.Drawing.Point(691, 26)
        Me.LabelPayment.Name = "LabelPayment"
        Me.LabelPayment.Size = New System.Drawing.Size(183, 44)
        Me.LabelPayment.TabIndex = 0
        Me.LabelPayment.Text = "Payment"
        '
        'ButtonSeat
        '
        Me.ButtonSeat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSeat.BackColor = System.Drawing.Color.RoyalBlue
        Me.ButtonSeat.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonSeat.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSeat.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonSeat.Location = New System.Drawing.Point(826, 148)
        Me.ButtonSeat.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSeat.Name = "ButtonSeat"
        Me.ButtonSeat.Size = New System.Drawing.Size(115, 82)
        Me.ButtonSeat.TabIndex = 329
        Me.ButtonSeat.Text = "Seat"
        Me.ButtonSeat.UseVisualStyleBackColor = False
        '
        'PictureBoxBarcode
        '
        Me.PictureBoxBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxBarcode.Location = New System.Drawing.Point(796, 40)
        Me.PictureBoxBarcode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxBarcode.Name = "PictureBoxBarcode"
        Me.PictureBoxBarcode.Size = New System.Drawing.Size(25, 32)
        Me.PictureBoxBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxBarcode.TabIndex = 327
        Me.PictureBoxBarcode.TabStop = False
        '
        'LabelDiscount
        '
        Me.LabelDiscount.AutoSize = True
        Me.LabelDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDiscount.ForeColor = System.Drawing.Color.Black
        Me.LabelDiscount.Location = New System.Drawing.Point(4, 49)
        Me.LabelDiscount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelDiscount.Name = "LabelDiscount"
        Me.LabelDiscount.Size = New System.Drawing.Size(69, 17)
        Me.LabelDiscount.TabIndex = 309
        Me.LabelDiscount.Text = "discount"
        '
        'ButtonDestination
        '
        Me.ButtonDestination.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDestination.BackColor = System.Drawing.Color.RoyalBlue
        Me.ButtonDestination.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDestination.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonDestination.Location = New System.Drawing.Point(280, 148)
        Me.ButtonDestination.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonDestination.Name = "ButtonDestination"
        Me.ButtonDestination.Size = New System.Drawing.Size(290, 82)
        Me.ButtonDestination.TabIndex = 325
        Me.ButtonDestination.Text = "Destination"
        Me.ButtonDestination.UseVisualStyleBackColor = False
        '
        'LabelVAT
        '
        Me.LabelVAT.AutoSize = True
        Me.LabelVAT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelVAT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelVAT.Location = New System.Drawing.Point(146, 42)
        Me.LabelVAT.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVAT.Name = "LabelVAT"
        Me.LabelVAT.Size = New System.Drawing.Size(38, 18)
        Me.LabelVAT.TabIndex = 317
        Me.LabelVAT.Text = "VAT"
        '
        'DestinationID
        '
        Me.DestinationID.AutoSize = True
        Me.DestinationID.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DestinationID.ForeColor = System.Drawing.Color.Black
        Me.DestinationID.Location = New System.Drawing.Point(72, 9)
        Me.DestinationID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.DestinationID.Name = "DestinationID"
        Me.DestinationID.Size = New System.Drawing.Size(34, 17)
        Me.DestinationID.TabIndex = 298
        Me.DestinationID.Text = "DID"
        '
        'LabelTime
        '
        Me.LabelTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTime.AutoSize = True
        Me.LabelTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelTime.Location = New System.Drawing.Point(672, 55)
        Me.LabelTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTime.Name = "LabelTime"
        Me.LabelTime.Size = New System.Drawing.Size(67, 25)
        Me.LabelTime.TabIndex = 323
        Me.LabelTime.Text = "Time:"
        '
        'LabelVATable
        '
        Me.LabelVATable.AutoSize = True
        Me.LabelVATable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelVATable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelVATable.Location = New System.Drawing.Point(145, 5)
        Me.LabelVATable.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVATable.Name = "LabelVATable"
        Me.LabelVATable.Size = New System.Drawing.Size(69, 18)
        Me.LabelVATable.TabIndex = 316
        Me.LabelVATable.Text = "VATable"
        '
        'OriginID
        '
        Me.OriginID.AutoSize = True
        Me.OriginID.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OriginID.ForeColor = System.Drawing.Color.Black
        Me.OriginID.Location = New System.Drawing.Point(4, 9)
        Me.OriginID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.OriginID.Name = "OriginID"
        Me.OriginID.Size = New System.Drawing.Size(35, 17)
        Me.OriginID.TabIndex = 295
        Me.OriginID.Text = "OID"
        '
        'ButtonVoid
        '
        Me.ButtonVoid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonVoid.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonVoid.Enabled = False
        Me.ButtonVoid.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonVoid.ForeColor = System.Drawing.Color.Black
        Me.ButtonVoid.Location = New System.Drawing.Point(53, 573)
        Me.ButtonVoid.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonVoid.Name = "ButtonVoid"
        Me.ButtonVoid.Size = New System.Drawing.Size(161, 48)
        Me.ButtonVoid.TabIndex = 322
        Me.ButtonVoid.Text = "VOID"
        Me.ButtonVoid.UseVisualStyleBackColor = False
        '
        'ButtonZeroRated
        '
        Me.ButtonZeroRated.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonZeroRated.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonZeroRated.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonZeroRated.ForeColor = System.Drawing.Color.Black
        Me.ButtonZeroRated.Location = New System.Drawing.Point(202, 764)
        Me.ButtonZeroRated.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonZeroRated.Name = "ButtonZeroRated"
        Me.ButtonZeroRated.Size = New System.Drawing.Size(168, 48)
        Me.ButtonZeroRated.TabIndex = 364
        Me.ButtonZeroRated.Text = "Zero Rated"
        Me.ButtonZeroRated.UseVisualStyleBackColor = False
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(383, 110)
        Me.Button10.Margin = New System.Windows.Forms.Padding(4)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(100, 28)
        Me.Button10.TabIndex = 87
        Me.Button10.Text = "Button10"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'PictureBoxCompanyLogo
        '
        Me.PictureBoxCompanyLogo.Location = New System.Drawing.Point(12, -2)
        Me.PictureBoxCompanyLogo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxCompanyLogo.Name = "PictureBoxCompanyLogo"
        Me.PictureBoxCompanyLogo.Size = New System.Drawing.Size(142, 100)
        Me.PictureBoxCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBoxCompanyLogo.TabIndex = 5
        Me.PictureBoxCompanyLogo.TabStop = False
        '
        'TextBoxBarcodeTicket
        '
        Me.TextBoxBarcodeTicket.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBarcodeTicket.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxBarcodeTicket.Location = New System.Drawing.Point(670, 16)
        Me.TextBoxBarcodeTicket.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxBarcodeTicket.Name = "TextBoxBarcodeTicket"
        Me.TextBoxBarcodeTicket.Size = New System.Drawing.Size(24, 30)
        Me.TextBoxBarcodeTicket.TabIndex = 346
        '
        'PictureBoxBarcodeTicket
        '
        Me.PictureBoxBarcodeTicket.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxBarcodeTicket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxBarcodeTicket.Location = New System.Drawing.Point(700, 16)
        Me.PictureBoxBarcodeTicket.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxBarcodeTicket.Name = "PictureBoxBarcodeTicket"
        Me.PictureBoxBarcodeTicket.Size = New System.Drawing.Size(25, 32)
        Me.PictureBoxBarcodeTicket.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxBarcodeTicket.TabIndex = 347
        Me.PictureBoxBarcodeTicket.TabStop = False
        '
        'LabelTotal
        '
        Me.LabelTotal.AutoSize = True
        Me.LabelTotal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelTotal.Location = New System.Drawing.Point(51, 361)
        Me.LabelTotal.Name = "LabelTotal"
        Me.LabelTotal.Size = New System.Drawing.Size(0, 29)
        Me.LabelTotal.TabIndex = 345
        '
        'LabelTotalText
        '
        Me.LabelTotalText.AutoSize = True
        Me.LabelTotalText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTotalText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelTotalText.Location = New System.Drawing.Point(52, 336)
        Me.LabelTotalText.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTotalText.Name = "LabelTotalText"
        Me.LabelTotalText.Size = New System.Drawing.Size(68, 25)
        Me.LabelTotalText.TabIndex = 344
        Me.LabelTotalText.Text = "Total:"
        '
        'TextBoxCardAmount
        '
        Me.TextBoxCardAmount.Enabled = False
        Me.TextBoxCardAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCardAmount.Location = New System.Drawing.Point(52, 276)
        Me.TextBoxCardAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxCardAmount.Multiline = True
        Me.TextBoxCardAmount.Name = "TextBoxCardAmount"
        Me.TextBoxCardAmount.Size = New System.Drawing.Size(161, 41)
        Me.TextBoxCardAmount.TabIndex = 343
        Me.TextBoxCardAmount.Text = "0.00"
        '
        'LabelTrainingMode
        '
        Me.LabelTrainingMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTrainingMode.AutoSize = True
        Me.LabelTrainingMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTrainingMode.ForeColor = System.Drawing.Color.Firebrick
        Me.LabelTrainingMode.Location = New System.Drawing.Point(1047, 9)
        Me.LabelTrainingMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTrainingMode.Name = "LabelTrainingMode"
        Me.LabelTrainingMode.Size = New System.Drawing.Size(146, 24)
        Me.LabelTrainingMode.TabIndex = 314
        Me.LabelTrainingMode.Text = "Training Mode"
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
        Me.LabelStationID.TabIndex = 90
        '
        'TextBoxUserID
        '
        Me.TextBoxUserID.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBoxUserID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxUserID.Location = New System.Drawing.Point(161, 63)
        Me.TextBoxUserID.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxUserID.Name = "TextBoxUserID"
        Me.TextBoxUserID.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxUserID.Size = New System.Drawing.Size(161, 30)
        Me.TextBoxUserID.TabIndex = 313
        Me.TextBoxUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.LabelStation.TabIndex = 89
        Me.LabelStation.Text = "Station:"
        '
        'ButtonActualMode
        '
        Me.ButtonActualMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonActualMode.BackColor = System.Drawing.Color.RoyalBlue
        Me.ButtonActualMode.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonActualMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonActualMode.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonActualMode.Location = New System.Drawing.Point(1044, 58)
        Me.ButtonActualMode.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonActualMode.Name = "ButtonActualMode"
        Me.ButtonActualMode.Size = New System.Drawing.Size(149, 36)
        Me.ButtonActualMode.TabIndex = 88
        Me.ButtonActualMode.Text = "Actual Mode"
        Me.ButtonActualMode.UseVisualStyleBackColor = False
        '
        'LabelCardAmount
        '
        Me.LabelCardAmount.AutoSize = True
        Me.LabelCardAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCardAmount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelCardAmount.Location = New System.Drawing.Point(52, 249)
        Me.LabelCardAmount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCardAmount.Name = "LabelCardAmount"
        Me.LabelCardAmount.Size = New System.Drawing.Size(146, 25)
        Me.LabelCardAmount.TabIndex = 342
        Me.LabelCardAmount.Text = "Card Amount:"
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
        'TextBoxBaggageCost
        '
        Me.TextBoxBaggageCost.Enabled = False
        Me.TextBoxBaggageCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxBaggageCost.Location = New System.Drawing.Point(52, 186)
        Me.TextBoxBaggageCost.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxBaggageCost.Multiline = True
        Me.TextBoxBaggageCost.Name = "TextBoxBaggageCost"
        Me.TextBoxBaggageCost.Size = New System.Drawing.Size(161, 41)
        Me.TextBoxBaggageCost.TabIndex = 341
        Me.TextBoxBaggageCost.Text = "0.00"
        '
        'PictureBoxLogoReceipt
        '
        Me.PictureBoxLogoReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBoxLogoReceipt.Location = New System.Drawing.Point(413, 26)
        Me.PictureBoxLogoReceipt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBoxLogoReceipt.Name = "PictureBoxLogoReceipt"
        Me.PictureBoxLogoReceipt.Size = New System.Drawing.Size(85, 60)
        Me.PictureBoxLogoReceipt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxLogoReceipt.TabIndex = 302
        Me.PictureBoxLogoReceipt.TabStop = False
        '
        'ButtonCard
        '
        Me.ButtonCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCard.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonCard.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCard.ForeColor = System.Drawing.Color.Black
        Me.ButtonCard.Location = New System.Drawing.Point(53, 720)
        Me.ButtonCard.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonCard.Name = "ButtonCard"
        Me.ButtonCard.Size = New System.Drawing.Size(161, 48)
        Me.ButtonCard.TabIndex = 338
        Me.ButtonCard.Text = "CARD"
        Me.ButtonCard.UseVisualStyleBackColor = False
        '
        'LabelType
        '
        Me.LabelType.AutoSize = True
        Me.LabelType.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelType.ForeColor = System.Drawing.Color.Black
        Me.LabelType.Location = New System.Drawing.Point(72, 49)
        Me.LabelType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelType.Name = "LabelType"
        Me.LabelType.Size = New System.Drawing.Size(65, 17)
        Me.LabelType.TabIndex = 316
        Me.LabelType.Text = "Regular"
        '
        'ButtonOrigin
        '
        Me.ButtonOrigin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOrigin.BackColor = System.Drawing.Color.RoyalBlue
        Me.ButtonOrigin.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOrigin.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOrigin.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonOrigin.Location = New System.Drawing.Point(280, 38)
        Me.ButtonOrigin.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonOrigin.Name = "ButtonOrigin"
        Me.ButtonOrigin.Size = New System.Drawing.Size(290, 82)
        Me.ButtonOrigin.TabIndex = 324
        Me.ButtonOrigin.Text = "Origin"
        Me.ButtonOrigin.UseVisualStyleBackColor = False
        '
        'LabelSerial
        '
        Me.LabelSerial.AutoSize = True
        Me.LabelSerial.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSerial.ForeColor = System.Drawing.Color.Black
        Me.LabelSerial.Location = New System.Drawing.Point(4, 29)
        Me.LabelSerial.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelSerial.Name = "LabelSerial"
        Me.LabelSerial.Size = New System.Drawing.Size(38, 17)
        Me.LabelSerial.TabIndex = 315
        Me.LabelSerial.Text = "SRL"
        '
        'ButtonStudent
        '
        Me.ButtonStudent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonStudent.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonStudent.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonStudent.ForeColor = System.Drawing.Color.Black
        Me.ButtonStudent.Location = New System.Drawing.Point(25, 708)
        Me.ButtonStudent.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonStudent.Name = "ButtonStudent"
        Me.ButtonStudent.Size = New System.Drawing.Size(168, 48)
        Me.ButtonStudent.TabIndex = 362
        Me.ButtonStudent.Text = "Student"
        Me.ButtonStudent.UseVisualStyleBackColor = False
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.SystemColors.ControlDark
        Me.PanelHeader.Controls.Add(Me.LabelTrainingMode)
        Me.PanelHeader.Controls.Add(Me.LabelStationID)
        Me.PanelHeader.Controls.Add(Me.TextBoxUserID)
        Me.PanelHeader.Controls.Add(Me.LabelStation)
        Me.PanelHeader.Controls.Add(Me.ButtonActualMode)
        Me.PanelHeader.Controls.Add(Me.Button10)
        Me.PanelHeader.Controls.Add(Me.PictureBoxVINTALogo)
        Me.PanelHeader.Controls.Add(Me.PictureBoxCompanyLogo)
        Me.PanelHeader.Controls.Add(Me.LabelPayment)
        Me.PanelHeader.Controls.Add(Me.PictureBoxLogoReceipt)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(1365, 97)
        Me.PanelHeader.TabIndex = 355
        '
        'LabelBaggageCost
        '
        Me.LabelBaggageCost.AutoSize = True
        Me.LabelBaggageCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelBaggageCost.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelBaggageCost.Location = New System.Drawing.Point(51, 159)
        Me.LabelBaggageCost.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBaggageCost.Name = "LabelBaggageCost"
        Me.LabelBaggageCost.Size = New System.Drawing.Size(156, 25)
        Me.LabelBaggageCost.TabIndex = 340
        Me.LabelBaggageCost.Text = "Baggage Cost:"
        '
        'ButtonPWD
        '
        Me.ButtonPWD.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPWD.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonPWD.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPWD.ForeColor = System.Drawing.Color.Black
        Me.ButtonPWD.Location = New System.Drawing.Point(26, 764)
        Me.ButtonPWD.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonPWD.Name = "ButtonPWD"
        Me.ButtonPWD.Size = New System.Drawing.Size(168, 48)
        Me.ButtonPWD.TabIndex = 363
        Me.ButtonPWD.Text = "PWD"
        Me.ButtonPWD.UseVisualStyleBackColor = False
        '
        'LabelFare
        '
        Me.LabelFare.AutoSize = True
        Me.LabelFare.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelFare.ForeColor = System.Drawing.Color.Black
        Me.LabelFare.Location = New System.Drawing.Point(5, 67)
        Me.LabelFare.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelFare.Name = "LabelFare"
        Me.LabelFare.Size = New System.Drawing.Size(37, 17)
        Me.LabelFare.TabIndex = 307
        Me.LabelFare.Text = "fare"
        '
        'Passenger_idTextBox
        '
        Me.Passenger_idTextBox.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Passenger_idTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Passenger_idTextBox.Location = New System.Drawing.Point(16, 62)
        Me.Passenger_idTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.Passenger_idTextBox.Name = "Passenger_idTextBox"
        Me.Passenger_idTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Passenger_idTextBox.Size = New System.Drawing.Size(343, 34)
        Me.Passenger_idTextBox.TabIndex = 1
        '
        'ComboReaderNames
        '
        Me.ComboReaderNames.FormattingEnabled = True
        Me.ComboReaderNames.Location = New System.Drawing.Point(167, 33)
        Me.ComboReaderNames.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboReaderNames.Name = "ComboReaderNames"
        Me.ComboReaderNames.Size = New System.Drawing.Size(97, 24)
        Me.ComboReaderNames.TabIndex = 37
        '
        'LabelVATExempt
        '
        Me.LabelVATExempt.AutoSize = True
        Me.LabelVATExempt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelVATExempt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelVATExempt.Location = New System.Drawing.Point(145, 23)
        Me.LabelVATExempt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelVATExempt.Name = "LabelVATExempt"
        Me.LabelVATExempt.Size = New System.Drawing.Size(99, 18)
        Me.LabelVATExempt.TabIndex = 318
        Me.LabelVATExempt.Text = "VAT Exempt"
        '
        'LabelLoginID
        '
        Me.LabelLoginID.AutoSize = True
        Me.LabelLoginID.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelLoginID.ForeColor = System.Drawing.Color.Black
        Me.LabelLoginID.Location = New System.Drawing.Point(72, 29)
        Me.LabelLoginID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelLoginID.Name = "LabelLoginID"
        Me.LabelLoginID.Size = New System.Drawing.Size(32, 17)
        Me.LabelLoginID.TabIndex = 306
        Me.LabelLoginID.Text = "LID"
        '
        'ButtonBaggage
        '
        Me.ButtonBaggage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonBaggage.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonBaggage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonBaggage.ForeColor = System.Drawing.Color.Black
        Me.ButtonBaggage.Location = New System.Drawing.Point(53, 671)
        Me.ButtonBaggage.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonBaggage.Name = "ButtonBaggage"
        Me.ButtonBaggage.Size = New System.Drawing.Size(161, 48)
        Me.ButtonBaggage.TabIndex = 321
        Me.ButtonBaggage.Text = "BAGGAGE"
        Me.ButtonBaggage.UseVisualStyleBackColor = False
        '
        'LabelZeroRated
        '
        Me.LabelZeroRated.AutoSize = True
        Me.LabelZeroRated.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelZeroRated.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelZeroRated.Location = New System.Drawing.Point(145, 60)
        Me.LabelZeroRated.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelZeroRated.Name = "LabelZeroRated"
        Me.LabelZeroRated.Size = New System.Drawing.Size(87, 18)
        Me.LabelZeroRated.TabIndex = 319
        Me.LabelZeroRated.Text = "ZeroRated"
        '
        'LabelMoney
        '
        Me.LabelMoney.AutoSize = True
        Me.LabelMoney.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMoney.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelMoney.Location = New System.Drawing.Point(52, 402)
        Me.LabelMoney.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelMoney.Name = "LabelMoney"
        Me.LabelMoney.Size = New System.Drawing.Size(84, 25)
        Me.LabelMoney.TabIndex = 294
        Me.LabelMoney.Text = "Money:"
        '
        'TextBoxTripFare
        '
        Me.TextBoxTripFare.Enabled = False
        Me.TextBoxTripFare.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxTripFare.Location = New System.Drawing.Point(52, 99)
        Me.TextBoxTripFare.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTripFare.Multiline = True
        Me.TextBoxTripFare.Name = "TextBoxTripFare"
        Me.TextBoxTripFare.Size = New System.Drawing.Size(161, 41)
        Me.TextBoxTripFare.TabIndex = 311
        Me.TextBoxTripFare.Text = "0.00"
        '
        'pass_lbl
        '
        Me.pass_lbl.AutoSize = True
        Me.pass_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pass_lbl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pass_lbl.Location = New System.Drawing.Point(11, 27)
        Me.pass_lbl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pass_lbl.Name = "pass_lbl"
        Me.pass_lbl.Size = New System.Drawing.Size(149, 25)
        Me.pass_lbl.TabIndex = 33
        Me.pass_lbl.Text = "Passenger ID:"
        '
        'LabelProfession
        '
        Me.LabelProfession.AutoSize = True
        Me.LabelProfession.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelProfession.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelProfession.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelProfession.Location = New System.Drawing.Point(7, 123)
        Me.LabelProfession.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelProfession.Name = "LabelProfession"
        Me.LabelProfession.Size = New System.Drawing.Size(105, 20)
        Me.LabelProfession.TabIndex = 67
        Me.LabelProfession.Text = "Profession:"
        '
        'GroupBoxPassengerInfo
        '
        Me.GroupBoxPassengerInfo.Controls.Add(Me.PictureBoxFace)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.LabelTicket)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.LabelOR)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.LabelCardNo)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.cardno)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.LabelIDNo)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.idtype)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.pi_lbl)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.LabelProfession)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.lname)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.LabelGender)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.fname)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.LabelFName)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.gender)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.LabelLName)
        Me.GroupBoxPassengerInfo.Controls.Add(Me.profession)
        Me.GroupBoxPassengerInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.GroupBoxPassengerInfo.Location = New System.Drawing.Point(5, 217)
        Me.GroupBoxPassengerInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxPassengerInfo.Name = "GroupBoxPassengerInfo"
        Me.GroupBoxPassengerInfo.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBoxPassengerInfo.Size = New System.Drawing.Size(383, 445)
        Me.GroupBoxPassengerInfo.TabIndex = 357
        Me.GroupBoxPassengerInfo.TabStop = False
        Me.GroupBoxPassengerInfo.Text = " "
        '
        'PictureBoxFace
        '
        Me.PictureBoxFace.Image = CType(resources.GetObject("PictureBoxFace.Image"), System.Drawing.Image)
        Me.PictureBoxFace.Location = New System.Drawing.Point(11, 205)
        Me.PictureBoxFace.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBoxFace.Name = "PictureBoxFace"
        Me.PictureBoxFace.Size = New System.Drawing.Size(360, 225)
        Me.PictureBoxFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxFace.TabIndex = 350
        Me.PictureBoxFace.TabStop = False
        '
        'LabelTicket
        '
        Me.LabelTicket.AutoSize = True
        Me.LabelTicket.ForeColor = System.Drawing.Color.Black
        Me.LabelTicket.Location = New System.Drawing.Point(243, 123)
        Me.LabelTicket.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTicket.Name = "LabelTicket"
        Me.LabelTicket.Size = New System.Drawing.Size(72, 17)
        Me.LabelTicket.TabIndex = 349
        Me.LabelTicket.Text = "00000001"
        '
        'LabelOR
        '
        Me.LabelOR.AutoSize = True
        Me.LabelOR.ForeColor = System.Drawing.Color.Black
        Me.LabelOR.Location = New System.Drawing.Point(243, 101)
        Me.LabelOR.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelOR.Name = "LabelOR"
        Me.LabelOR.Size = New System.Drawing.Size(128, 17)
        Me.LabelOR.TabIndex = 348
        Me.LabelOR.Text = "000000000000001"
        '
        'LabelCardNo
        '
        Me.LabelCardNo.AutoSize = True
        Me.LabelCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelCardNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCardNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelCardNo.Location = New System.Drawing.Point(7, 174)
        Me.LabelCardNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCardNo.Name = "LabelCardNo"
        Me.LabelCardNo.Size = New System.Drawing.Size(84, 20)
        Me.LabelCardNo.TabIndex = 71
        Me.LabelCardNo.Text = "Card No:"
        '
        'cardno
        '
        Me.cardno.AutoSize = True
        Me.cardno.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cardno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cardno.Location = New System.Drawing.Point(137, 174)
        Me.cardno.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cardno.Name = "cardno"
        Me.cardno.Size = New System.Drawing.Size(0, 20)
        Me.cardno.TabIndex = 70
        '
        'LabelIDNo
        '
        Me.LabelIDNo.AutoSize = True
        Me.LabelIDNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelIDNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelIDNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelIDNo.Location = New System.Drawing.Point(7, 147)
        Me.LabelIDNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelIDNo.Name = "LabelIDNo"
        Me.LabelIDNo.Size = New System.Drawing.Size(68, 20)
        Me.LabelIDNo.TabIndex = 69
        Me.LabelIDNo.Text = "ID No.:"
        '
        'idtype
        '
        Me.idtype.AutoSize = True
        Me.idtype.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.idtype.ForeColor = System.Drawing.SystemColors.ControlText
        Me.idtype.Location = New System.Drawing.Point(137, 147)
        Me.idtype.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.idtype.Name = "idtype"
        Me.idtype.Size = New System.Drawing.Size(0, 20)
        Me.idtype.TabIndex = 68
        '
        'pi_lbl
        '
        Me.pi_lbl.AutoSize = True
        Me.pi_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pi_lbl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.pi_lbl.Location = New System.Drawing.Point(100, 16)
        Me.pi_lbl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pi_lbl.Name = "pi_lbl"
        Me.pi_lbl.Size = New System.Drawing.Size(164, 25)
        Me.pi_lbl.TabIndex = 40
        Me.pi_lbl.Text = "Passenger Info:"
        '
        'lname
        '
        Me.lname.AutoSize = True
        Me.lname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lname.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lname.Location = New System.Drawing.Point(137, 76)
        Me.lname.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lname.Name = "lname"
        Me.lname.Size = New System.Drawing.Size(0, 20)
        Me.lname.TabIndex = 41
        '
        'LabelGender
        '
        Me.LabelGender.AutoSize = True
        Me.LabelGender.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelGender.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelGender.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelGender.Location = New System.Drawing.Point(7, 98)
        Me.LabelGender.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelGender.Name = "LabelGender"
        Me.LabelGender.Size = New System.Drawing.Size(76, 20)
        Me.LabelGender.TabIndex = 66
        Me.LabelGender.Text = "Gender:"
        '
        'fname
        '
        Me.fname.AutoSize = True
        Me.fname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fname.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fname.Location = New System.Drawing.Point(137, 54)
        Me.fname.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.fname.Name = "fname"
        Me.fname.Size = New System.Drawing.Size(0, 20)
        Me.fname.TabIndex = 42
        '
        'LabelFName
        '
        Me.LabelFName.AutoSize = True
        Me.LabelFName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelFName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelFName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelFName.Location = New System.Drawing.Point(7, 54)
        Me.LabelFName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelFName.Name = "LabelFName"
        Me.LabelFName.Size = New System.Drawing.Size(108, 20)
        Me.LabelFName.TabIndex = 57
        Me.LabelFName.Text = "First Name:"
        '
        'gender
        '
        Me.gender.AutoSize = True
        Me.gender.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gender.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gender.Location = New System.Drawing.Point(137, 98)
        Me.gender.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.gender.Name = "gender"
        Me.gender.Size = New System.Drawing.Size(0, 20)
        Me.gender.TabIndex = 54
        '
        'LabelLName
        '
        Me.LabelLName.AutoSize = True
        Me.LabelLName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelLName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelLName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelLName.Location = New System.Drawing.Point(7, 76)
        Me.LabelLName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelLName.Name = "LabelLName"
        Me.LabelLName.Size = New System.Drawing.Size(106, 20)
        Me.LabelLName.TabIndex = 56
        Me.LabelLName.Text = "Last Name:"
        '
        'profession
        '
        Me.profession.AutoSize = True
        Me.profession.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.profession.ForeColor = System.Drawing.SystemColors.ControlText
        Me.profession.Location = New System.Drawing.Point(137, 123)
        Me.profession.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.profession.Name = "profession"
        Me.profession.Size = New System.Drawing.Size(0, 20)
        Me.profession.TabIndex = 55
        '
        'ButtonReprintTicket
        '
        Me.ButtonReprintTicket.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonReprintTicket.BackColor = System.Drawing.Color.Gainsboro
        Me.ButtonReprintTicket.Enabled = False
        Me.ButtonReprintTicket.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonReprintTicket.ForeColor = System.Drawing.Color.Black
        Me.ButtonReprintTicket.Location = New System.Drawing.Point(53, 622)
        Me.ButtonReprintTicket.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonReprintTicket.Name = "ButtonReprintTicket"
        Me.ButtonReprintTicket.Size = New System.Drawing.Size(161, 48)
        Me.ButtonReprintTicket.TabIndex = 320
        Me.ButtonReprintTicket.Text = "REPRINT"
        Me.ButtonReprintTicket.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button1.Location = New System.Drawing.Point(280, 239)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 111)
        Me.Button1.TabIndex = 315
        Me.Button1.Text = "1"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button2.Location = New System.Drawing.Point(412, 239)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(133, 111)
        Me.Button2.TabIndex = 296
        Me.Button2.Text = "2"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.Color.Firebrick
        Me.ButtonDel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonDel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDel.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonDel.Location = New System.Drawing.Point(808, 239)
        Me.ButtonDel.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(133, 111)
        Me.ButtonDel.TabIndex = 314
        Me.ButtonDel.Text = "DEL"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ButtonClear
        '
        Me.ButtonClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClear.BackColor = System.Drawing.Color.Firebrick
        Me.ButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClear.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonClear.Location = New System.Drawing.Point(808, 348)
        Me.ButtonClear.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(133, 111)
        Me.ButtonClear.TabIndex = 307
        Me.ButtonClear.Text = "CLEAR"
        Me.ButtonClear.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button3.Location = New System.Drawing.Point(544, 239)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(133, 111)
        Me.Button3.TabIndex = 297
        Me.Button3.Text = "3"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'PanelRight
        '
        Me.PanelRight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelRight.Controls.Add(Me.TextBoxBarcodeTicket)
        Me.PanelRight.Controls.Add(Me.PictureBoxBarcodeTicket)
        Me.PanelRight.Controls.Add(Me.LabelTotal)
        Me.PanelRight.Controls.Add(Me.LabelTotalText)
        Me.PanelRight.Controls.Add(Me.TextBoxCardAmount)
        Me.PanelRight.Controls.Add(Me.LabelCardAmount)
        Me.PanelRight.Controls.Add(Me.TextBoxBaggageCost)
        Me.PanelRight.Controls.Add(Me.LabelBaggageCost)
        Me.PanelRight.Controls.Add(Me.ButtonCard)
        Me.PanelRight.Controls.Add(Me.LabelType)
        Me.PanelRight.Controls.Add(Me.ButtonOrigin)
        Me.PanelRight.Controls.Add(Me.LabelSerial)
        Me.PanelRight.Controls.Add(Me.LabelDestination)
        Me.PanelRight.Controls.Add(Me.LabelOrigin)
        Me.PanelRight.Controls.Add(Me.ComboBoxTime)
        Me.PanelRight.Controls.Add(Me.TextBoxSeat)
        Me.PanelRight.Controls.Add(Me.ButtonSeat)
        Me.PanelRight.Controls.Add(Me.TextBoxBarcode)
        Me.PanelRight.Controls.Add(Me.PictureBoxBarcode)
        Me.PanelRight.Controls.Add(Me.LabelDiscount)
        Me.PanelRight.Controls.Add(Me.ButtonDestination)
        Me.PanelRight.Controls.Add(Me.LabelVAT)
        Me.PanelRight.Controls.Add(Me.DestinationID)
        Me.PanelRight.Controls.Add(Me.LabelTime)
        Me.PanelRight.Controls.Add(Me.LabelVATable)
        Me.PanelRight.Controls.Add(Me.OriginID)
        Me.PanelRight.Controls.Add(Me.ButtonVoid)
        Me.PanelRight.Controls.Add(Me.LabelFare)
        Me.PanelRight.Controls.Add(Me.LabelVATExempt)
        Me.PanelRight.Controls.Add(Me.LabelLoginID)
        Me.PanelRight.Controls.Add(Me.ButtonBaggage)
        Me.PanelRight.Controls.Add(Me.LabelZeroRated)
        Me.PanelRight.Controls.Add(Me.LabelMoney)
        Me.PanelRight.Controls.Add(Me.TextBoxTripFare)
        Me.PanelRight.Controls.Add(Me.ButtonReprintTicket)
        Me.PanelRight.Controls.Add(Me.Button1)
        Me.PanelRight.Controls.Add(Me.ButtonClear)
        Me.PanelRight.Controls.Add(Me.Button2)
        Me.PanelRight.Controls.Add(Me.ButtonDel)
        Me.PanelRight.Controls.Add(Me.Button3)
        Me.PanelRight.Controls.Add(Me.ButtonIssueTicket)
        Me.PanelRight.Controls.Add(Me.Button100)
        Me.PanelRight.Controls.Add(Me.Button1000)
        Me.PanelRight.Controls.Add(Me.ButtonOk)
        Me.PanelRight.Controls.Add(Me.Button00)
        Me.PanelRight.Controls.Add(Me.Button500)
        Me.PanelRight.Controls.Add(Me.Button200)
        Me.PanelRight.Controls.Add(Me.ButtonDot)
        Me.PanelRight.Controls.Add(Me.Button0)
        Me.PanelRight.Controls.Add(Me.Button9)
        Me.PanelRight.Controls.Add(Me.Button8)
        Me.PanelRight.Controls.Add(Me.Button7)
        Me.PanelRight.Controls.Add(Me.Button6)
        Me.PanelRight.Controls.Add(Me.Button5)
        Me.PanelRight.Controls.Add(Me.Button4)
        Me.PanelRight.Controls.Add(Me.LabelChange)
        Me.PanelRight.Controls.Add(Me.TextBoxChange)
        Me.PanelRight.Controls.Add(Me.TextBoxMoney)
        Me.PanelRight.Controls.Add(Me.LabelTripFare)
        Me.PanelRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelRight.Location = New System.Drawing.Point(403, 102)
        Me.PanelRight.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelRight.Name = "PanelRight"
        Me.PanelRight.Size = New System.Drawing.Size(957, 782)
        Me.PanelRight.TabIndex = 354
        '
        'ButtonIssueTicket
        '
        Me.ButtonIssueTicket.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonIssueTicket.BackColor = System.Drawing.Color.RoyalBlue
        Me.ButtonIssueTicket.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonIssueTicket.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonIssueTicket.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonIssueTicket.Location = New System.Drawing.Point(280, 686)
        Me.ButtonIssueTicket.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonIssueTicket.Name = "ButtonIssueTicket"
        Me.ButtonIssueTicket.Size = New System.Drawing.Size(661, 82)
        Me.ButtonIssueTicket.TabIndex = 290
        Me.ButtonIssueTicket.Text = "Issue Ticket"
        Me.ButtonIssueTicket.UseVisualStyleBackColor = False
        '
        'Button100
        '
        Me.Button100.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button100.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button100.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button100.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button100.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button100.Location = New System.Drawing.Point(676, 239)
        Me.Button100.Margin = New System.Windows.Forms.Padding(4)
        Me.Button100.Name = "Button100"
        Me.Button100.Size = New System.Drawing.Size(133, 111)
        Me.Button100.TabIndex = 306
        Me.Button100.Text = "100"
        Me.Button100.UseVisualStyleBackColor = False
        '
        'Button1000
        '
        Me.Button1000.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1000.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button1000.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1000.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1000.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button1000.Location = New System.Drawing.Point(676, 567)
        Me.Button1000.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1000.Name = "Button1000"
        Me.Button1000.Size = New System.Drawing.Size(133, 111)
        Me.Button1000.TabIndex = 313
        Me.Button1000.Text = "1000"
        Me.Button1000.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.Color.ForestGreen
        Me.ButtonOk.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOk.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonOk.Location = New System.Drawing.Point(808, 458)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(133, 220)
        Me.ButtonOk.TabIndex = 308
        Me.ButtonOk.Text = "OK"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Button00
        '
        Me.Button00.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button00.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button00.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button00.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button00.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button00.Location = New System.Drawing.Point(544, 567)
        Me.Button00.Margin = New System.Windows.Forms.Padding(4)
        Me.Button00.Name = "Button00"
        Me.Button00.Size = New System.Drawing.Size(133, 111)
        Me.Button00.TabIndex = 312
        Me.Button00.Text = "00"
        Me.Button00.UseVisualStyleBackColor = False
        '
        'Button500
        '
        Me.Button500.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button500.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button500.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button500.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button500.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button500.Location = New System.Drawing.Point(676, 458)
        Me.Button500.Margin = New System.Windows.Forms.Padding(4)
        Me.Button500.Name = "Button500"
        Me.Button500.Size = New System.Drawing.Size(133, 111)
        Me.Button500.TabIndex = 310
        Me.Button500.Text = "500"
        Me.Button500.UseVisualStyleBackColor = False
        '
        'Button200
        '
        Me.Button200.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button200.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button200.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button200.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button200.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button200.Location = New System.Drawing.Point(676, 348)
        Me.Button200.Margin = New System.Windows.Forms.Padding(4)
        Me.Button200.Name = "Button200"
        Me.Button200.Size = New System.Drawing.Size(133, 111)
        Me.Button200.TabIndex = 309
        Me.Button200.Text = "200"
        Me.Button200.UseVisualStyleBackColor = False
        '
        'ButtonDot
        '
        Me.ButtonDot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDot.BackColor = System.Drawing.Color.RoyalBlue
        Me.ButtonDot.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonDot.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDot.ForeColor = System.Drawing.Color.AliceBlue
        Me.ButtonDot.Location = New System.Drawing.Point(280, 567)
        Me.ButtonDot.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonDot.Name = "ButtonDot"
        Me.ButtonDot.Size = New System.Drawing.Size(133, 111)
        Me.ButtonDot.TabIndex = 305
        Me.ButtonDot.Text = "."
        Me.ButtonDot.UseVisualStyleBackColor = False
        '
        'Button0
        '
        Me.Button0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button0.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button0.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button0.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button0.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button0.Location = New System.Drawing.Point(412, 567)
        Me.Button0.Margin = New System.Windows.Forms.Padding(4)
        Me.Button0.Name = "Button0"
        Me.Button0.Size = New System.Drawing.Size(133, 111)
        Me.Button0.TabIndex = 304
        Me.Button0.Text = "0"
        Me.Button0.UseVisualStyleBackColor = False
        '
        'Button9
        '
        Me.Button9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button9.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button9.Location = New System.Drawing.Point(544, 458)
        Me.Button9.Margin = New System.Windows.Forms.Padding(4)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(133, 111)
        Me.Button9.TabIndex = 303
        Me.Button9.Text = "9"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button8.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button8.Location = New System.Drawing.Point(412, 458)
        Me.Button8.Margin = New System.Windows.Forms.Padding(4)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(133, 111)
        Me.Button8.TabIndex = 302
        Me.Button8.Text = "8"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button7.Location = New System.Drawing.Point(280, 458)
        Me.Button7.Margin = New System.Windows.Forms.Padding(4)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(133, 111)
        Me.Button7.TabIndex = 301
        Me.Button7.Text = "7"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button6.Location = New System.Drawing.Point(544, 348)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(133, 111)
        Me.Button6.TabIndex = 300
        Me.Button6.Text = "6"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button5.Location = New System.Drawing.Point(412, 348)
        Me.Button5.Margin = New System.Windows.Forms.Padding(4)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(133, 111)
        Me.Button5.TabIndex = 299
        Me.Button5.Text = "5"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackColor = System.Drawing.Color.RoyalBlue
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.AliceBlue
        Me.Button4.Location = New System.Drawing.Point(280, 348)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(133, 111)
        Me.Button4.TabIndex = 298
        Me.Button4.Text = "4"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'LabelChange
        '
        Me.LabelChange.AutoSize = True
        Me.LabelChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelChange.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelChange.Location = New System.Drawing.Point(51, 490)
        Me.LabelChange.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelChange.Name = "LabelChange"
        Me.LabelChange.Size = New System.Drawing.Size(95, 25)
        Me.LabelChange.TabIndex = 295
        Me.LabelChange.Text = "Change:"
        '
        'TextBoxChange
        '
        Me.TextBoxChange.Enabled = False
        Me.TextBoxChange.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxChange.ForeColor = System.Drawing.Color.Red
        Me.TextBoxChange.Location = New System.Drawing.Point(52, 517)
        Me.TextBoxChange.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxChange.Multiline = True
        Me.TextBoxChange.Name = "TextBoxChange"
        Me.TextBoxChange.Size = New System.Drawing.Size(161, 41)
        Me.TextBoxChange.TabIndex = 293
        '
        'TextBoxMoney
        '
        Me.TextBoxMoney.Enabled = False
        Me.TextBoxMoney.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxMoney.Location = New System.Drawing.Point(53, 430)
        Me.TextBoxMoney.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxMoney.Multiline = True
        Me.TextBoxMoney.Name = "TextBoxMoney"
        Me.TextBoxMoney.Size = New System.Drawing.Size(161, 41)
        Me.TextBoxMoney.TabIndex = 292
        '
        'LabelTripFare
        '
        Me.LabelTripFare.AutoSize = True
        Me.LabelTripFare.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTripFare.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LabelTripFare.Location = New System.Drawing.Point(52, 72)
        Me.LabelTripFare.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTripFare.Name = "LabelTripFare"
        Me.LabelTripFare.Size = New System.Drawing.Size(107, 25)
        Me.LabelTripFare.TabIndex = 291
        Me.LabelTripFare.Text = "Trip Fare:"
        '
        'GroupBoxPassengerID
        '
        Me.GroupBoxPassengerID.Controls.Add(Me.Passenger_idTextBox)
        Me.GroupBoxPassengerID.Controls.Add(Me.ComboReaderNames)
        Me.GroupBoxPassengerID.Controls.Add(Me.pass_lbl)
        Me.GroupBoxPassengerID.Location = New System.Drawing.Point(5, 97)
        Me.GroupBoxPassengerID.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxPassengerID.Name = "GroupBoxPassengerID"
        Me.GroupBoxPassengerID.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBoxPassengerID.Size = New System.Drawing.Size(383, 118)
        Me.GroupBoxPassengerID.TabIndex = 353
        Me.GroupBoxPassengerID.TabStop = False
        '
        'mainformDEMO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(1365, 945)
        Me.Controls.Add(Me.ButtonEmployee)
        Me.Controls.Add(Me.TextBoxPercentDiscount)
        Me.Controls.Add(Me.LabelVersionNo)
        Me.Controls.Add(Me.LabelSoftwareName)
        Me.Controls.Add(Me.ButtonSenior)
        Me.Controls.Add(Me.PanelFooter)
        Me.Controls.Add(Me.CheckBoxPercentDiscount)
        Me.Controls.Add(Me.ButtonZeroRated)
        Me.Controls.Add(Me.ButtonStudent)
        Me.Controls.Add(Me.PanelHeader)
        Me.Controls.Add(Me.ButtonPWD)
        Me.Controls.Add(Me.GroupBoxPassengerInfo)
        Me.Controls.Add(Me.PanelRight)
        Me.Controls.Add(Me.GroupBoxPassengerID)
        Me.ForeColor = System.Drawing.Color.AliceBlue
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "mainformDEMO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = " "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelFooter.ResumeLayout(False)
        Me.PanelFooter.PerformLayout()
        CType(Me.PictureBoxBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxCompanyLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxBarcodeTicket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxVINTALogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLogoReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelHeader.PerformLayout()
        Me.GroupBoxPassengerInfo.ResumeLayout(False)
        Me.GroupBoxPassengerInfo.PerformLayout()
        CType(Me.PictureBoxFace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelRight.ResumeLayout(False)
        Me.PanelRight.PerformLayout()
        Me.GroupBoxPassengerID.ResumeLayout(False)
        Me.GroupBoxPassengerID.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents PrintDocument2 As Printing.PrintDocument
    Friend WithEvents ButtonEmployee As Button
    Friend WithEvents TextBoxPercentDiscount As TextBox
    Friend WithEvents LabelVersionNo As Label
    Friend WithEvents LabelSoftwareName As Label
    Friend WithEvents ButtonSenior As Button
    Friend WithEvents LabelPOSno As Label
    Friend WithEvents PanelFooter As Panel
    Friend WithEvents LabelCashier As Label
    Friend WithEvents LabelCashierID As Label
    Friend WithEvents LabelCashierName As Label
    Friend WithEvents LabelPOS As Label
    Friend WithEvents LabelDateTime As Label
    Friend WithEvents LabelDestination As Label
    Friend WithEvents LabelOrigin As Label
    Friend WithEvents ComboBoxTime As ComboBox
    Friend WithEvents CheckBoxPercentDiscount As CheckBox
    Friend WithEvents TextBoxSeat As TextBox
    Friend WithEvents TextBoxBarcode As TextBox
    Friend WithEvents LabelPayment As Label
    Friend WithEvents ButtonSeat As Button
    Friend WithEvents PictureBoxBarcode As PictureBox
    Friend WithEvents LabelDiscount As Label
    Friend WithEvents ButtonDestination As Button
    Friend WithEvents LabelVAT As Label
    Friend WithEvents DestinationID As Label
    Friend WithEvents LabelTime As Label
    Friend WithEvents LabelVATable As Label
    Friend WithEvents OriginID As Label
    Friend WithEvents ButtonVoid As Button
    Friend WithEvents ButtonZeroRated As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents PictureBoxCompanyLogo As PictureBox
    Friend WithEvents TextBoxBarcodeTicket As TextBox
    Friend WithEvents PictureBoxBarcodeTicket As PictureBox
    Friend WithEvents LabelTotal As Label
    Friend WithEvents LabelTotalText As Label
    Friend WithEvents TextBoxCardAmount As TextBox
    Friend WithEvents LabelTrainingMode As Label
    Friend WithEvents LabelStationID As Label
    Friend WithEvents TextBoxUserID As TextBox
    Friend WithEvents LabelStation As Label
    Friend WithEvents ButtonActualMode As Button
    Friend WithEvents LabelCardAmount As Label
    Friend WithEvents PictureBoxVINTALogo As PictureBox
    Friend WithEvents TextBoxBaggageCost As TextBox
    Friend WithEvents PictureBoxLogoReceipt As PictureBox
    Friend WithEvents ButtonCard As Button
    Friend WithEvents LabelType As Label
    Friend WithEvents ButtonOrigin As Button
    Friend WithEvents LabelSerial As Label
    Friend WithEvents ButtonStudent As Button
    Friend WithEvents PanelHeader As Panel
    Friend WithEvents LabelBaggageCost As Label
    Friend WithEvents ButtonPWD As Button
    Friend WithEvents LabelFare As Label
    Friend WithEvents Passenger_idTextBox As TextBox
    Friend WithEvents ComboReaderNames As ComboBox
    Friend WithEvents LabelVATExempt As Label
    Friend WithEvents LabelLoginID As Label
    Friend WithEvents ButtonBaggage As Button
    Friend WithEvents LabelZeroRated As Label
    Friend WithEvents LabelMoney As Label
    Friend WithEvents TextBoxTripFare As TextBox
    Friend WithEvents pass_lbl As Label
    Friend WithEvents LabelProfession As Label
    Friend WithEvents GroupBoxPassengerInfo As GroupBox
    Friend WithEvents PictureBoxFace As PictureBox
    Friend WithEvents LabelTicket As Label
    Friend WithEvents LabelOR As Label
    Friend WithEvents LabelCardNo As Label
    Friend WithEvents cardno As Label
    Friend WithEvents LabelIDNo As Label
    Friend WithEvents idtype As Label
    Friend WithEvents pi_lbl As Label
    Friend WithEvents lname As Label
    Friend WithEvents LabelGender As Label
    Friend WithEvents fname As Label
    Friend WithEvents LabelFName As Label
    Friend WithEvents gender As Label
    Friend WithEvents LabelLName As Label
    Friend WithEvents profession As Label
    Friend WithEvents ButtonReprintTicket As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents ButtonClear As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents PanelRight As Panel
    Friend WithEvents ButtonIssueTicket As Button
    Friend WithEvents Button100 As Button
    Friend WithEvents Button1000 As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Button00 As Button
    Friend WithEvents Button500 As Button
    Friend WithEvents Button200 As Button
    Friend WithEvents ButtonDot As Button
    Friend WithEvents Button0 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents LabelChange As Label
    Friend WithEvents TextBoxChange As TextBox
    Friend WithEvents TextBoxMoney As TextBox
    Friend WithEvents LabelTripFare As Label
    Friend WithEvents GroupBoxPassengerID As GroupBox
End Class
