<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EnableManagerActivity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnableManagerActivity))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnMAVoidItem = New System.Windows.Forms.Button()
        Me.btnCancelFp = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.managerUserID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFpStatus = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(179, 50)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(135, 90)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Discounts"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnMAVoidItem
        '
        Me.btnMAVoidItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMAVoidItem.Location = New System.Drawing.Point(38, 50)
        Me.btnMAVoidItem.Name = "btnMAVoidItem"
        Me.btnMAVoidItem.Size = New System.Drawing.Size(135, 90)
        Me.btnMAVoidItem.TabIndex = 2
        Me.btnMAVoidItem.Text = "Void Item"
        Me.btnMAVoidItem.UseVisualStyleBackColor = True
        '
        'btnCancelFp
        '
        Me.btnCancelFp.Location = New System.Drawing.Point(25, 12)
        Me.btnCancelFp.Name = "btnCancelFp"
        Me.btnCancelFp.Size = New System.Drawing.Size(89, 26)
        Me.btnCancelFp.TabIndex = 3
        Me.btnCancelFp.Text = "btnCancelFp"
        Me.btnCancelFp.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnMAVoidItem)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(25, 122)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(347, 159)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Activation:"
        '
        'managerUserID
        '
        Me.managerUserID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.managerUserID.Location = New System.Drawing.Point(156, 34)
        Me.managerUserID.Multiline = True
        Me.managerUserID.Name = "managerUserID"
        Me.managerUserID.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.managerUserID.Size = New System.Drawing.Size(216, 42)
        Me.managerUserID.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 20)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Manager ID:"
        '
        'lblFpStatus
        '
        Me.lblFpStatus.AutoSize = True
        Me.lblFpStatus.Location = New System.Drawing.Point(60, 87)
        Me.lblFpStatus.Name = "lblFpStatus"
        Me.lblFpStatus.Size = New System.Drawing.Size(78, 17)
        Me.lblFpStatus.TabIndex = 7
        Me.lblFpStatus.Text = "lblFpStatus"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(378, 122)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(347, 159)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Activation: Introductory Ride"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(38, 50)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(135, 90)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Active"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(179, 50)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(135, 90)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "InActive"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Button4)
        Me.GroupBox3.Controls.Add(Me.Button5)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(378, 287)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(347, 167)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Activation: Regular Rides"
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(38, 50)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(135, 90)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "Active"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(179, 50)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(135, 90)
        Me.Button5.TabIndex = 0
        Me.Button5.Text = "InActive"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(38, 34)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(135, 90)
        Me.Button6.TabIndex = 3
        Me.Button6.Text = "Cash Count Report"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button7)
        Me.GroupBox4.Controls.Add(Me.Button6)
        Me.GroupBox4.Location = New System.Drawing.Point(25, 287)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(347, 167)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(179, 34)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(135, 90)
        Me.Button7.TabIndex = 4
        Me.Button7.Text = "Open Cash Drawer"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'EnableManagerActivity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 477)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lblFpStatus)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.managerUserID)
        Me.Controls.Add(Me.btnCancelFp)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EnableManagerActivity"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manager Access"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents btnMAVoidItem As Button
    Friend WithEvents btnCancelFp As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents managerUserID As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblFpStatus As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Button7 As Button
End Class
