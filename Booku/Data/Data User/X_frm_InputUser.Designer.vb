<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputUser
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
        Me.btn_Reset = New System.Windows.Forms.Button()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Tutup = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Username = New System.Windows.Forms.TextBox()
        Me.txt_NamaLengkap = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmb_Jabatan = New System.Windows.Forms.ComboBox()
        Me.txt_Password = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chk_ClusterFinance = New System.Windows.Forms.CheckBox()
        Me.chk_ClusterAccounting = New System.Windows.Forms.CheckBox()
        Me.cmb_StatusAktif = New System.Windows.Forms.ComboBox()
        Me.lbl_StatusAktif = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Reset
        '
        Me.btn_Reset.Location = New System.Drawing.Point(31, 212)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(83, 35)
        Me.btn_Reset.TabIndex = 9932
        Me.btn_Reset.Text = "Reset"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(120, 212)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9931
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Location = New System.Drawing.Point(209, 212)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tutup.TabIndex = 9933
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 9934
        Me.Label1.Text = "Username"
        '
        'txt_Username
        '
        Me.txt_Username.Location = New System.Drawing.Point(120, 28)
        Me.txt_Username.Name = "txt_Username"
        Me.txt_Username.Size = New System.Drawing.Size(172, 20)
        Me.txt_Username.TabIndex = 10
        '
        'txt_NamaLengkap
        '
        Me.txt_NamaLengkap.Location = New System.Drawing.Point(120, 80)
        Me.txt_NamaLengkap.Name = "txt_NamaLengkap"
        Me.txt_NamaLengkap.Size = New System.Drawing.Size(172, 20)
        Me.txt_NamaLengkap.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 9936
        Me.Label2.Text = "Nama Lengkap"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 9942
        Me.Label5.Text = "Jabatan"
        '
        'cmb_Jabatan
        '
        Me.cmb_Jabatan.FormattingEnabled = True
        Me.cmb_Jabatan.Location = New System.Drawing.Point(120, 106)
        Me.cmb_Jabatan.Name = "cmb_Jabatan"
        Me.cmb_Jabatan.Size = New System.Drawing.Size(96, 21)
        Me.cmb_Jabatan.TabIndex = 40
        '
        'txt_Password
        '
        Me.txt_Password.Location = New System.Drawing.Point(120, 54)
        Me.txt_Password.Name = "txt_Password"
        Me.txt_Password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_Password.Size = New System.Drawing.Size(172, 20)
        Me.txt_Password.TabIndex = 20
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 9947
        Me.Label3.Text = "Password"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 9949
        Me.Label4.Text = "Cluster"
        '
        'chk_ClusterFinance
        '
        Me.chk_ClusterFinance.AutoSize = True
        Me.chk_ClusterFinance.Location = New System.Drawing.Point(120, 136)
        Me.chk_ClusterFinance.Name = "chk_ClusterFinance"
        Me.chk_ClusterFinance.Size = New System.Drawing.Size(64, 17)
        Me.chk_ClusterFinance.TabIndex = 50
        Me.chk_ClusterFinance.Text = "Finance"
        Me.chk_ClusterFinance.UseVisualStyleBackColor = True
        '
        'chk_ClusterAccounting
        '
        Me.chk_ClusterAccounting.AutoSize = True
        Me.chk_ClusterAccounting.Location = New System.Drawing.Point(206, 136)
        Me.chk_ClusterAccounting.Name = "chk_ClusterAccounting"
        Me.chk_ClusterAccounting.Size = New System.Drawing.Size(80, 17)
        Me.chk_ClusterAccounting.TabIndex = 60
        Me.chk_ClusterAccounting.Text = "Accounting"
        Me.chk_ClusterAccounting.UseVisualStyleBackColor = True
        '
        'cmb_StatusAktif
        '
        Me.cmb_StatusAktif.FormattingEnabled = True
        Me.cmb_StatusAktif.Location = New System.Drawing.Point(120, 159)
        Me.cmb_StatusAktif.Name = "cmb_StatusAktif"
        Me.cmb_StatusAktif.Size = New System.Drawing.Size(64, 21)
        Me.cmb_StatusAktif.TabIndex = 70
        '
        'lbl_StatusAktif
        '
        Me.lbl_StatusAktif.AutoSize = True
        Me.lbl_StatusAktif.Location = New System.Drawing.Point(28, 162)
        Me.lbl_StatusAktif.Name = "lbl_StatusAktif"
        Me.lbl_StatusAktif.Size = New System.Drawing.Size(61, 13)
        Me.lbl_StatusAktif.TabIndex = 9953
        Me.lbl_StatusAktif.Text = "Status Aktif"
        '
        'frm_InputUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 270)
        Me.Controls.Add(Me.cmb_StatusAktif)
        Me.Controls.Add(Me.lbl_StatusAktif)
        Me.Controls.Add(Me.chk_ClusterAccounting)
        Me.Controls.Add(Me.chk_ClusterFinance)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_Password)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmb_Jabatan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_NamaLengkap)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_Username)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Reset)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Tutup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Data User"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_Username As System.Windows.Forms.TextBox
    Friend WithEvents txt_NamaLengkap As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_Jabatan As System.Windows.Forms.ComboBox
    Friend WithEvents txt_Password As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chk_ClusterFinance As System.Windows.Forms.CheckBox
    Friend WithEvents chk_ClusterAccounting As System.Windows.Forms.CheckBox
    Friend WithEvents cmb_StatusAktif As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_StatusAktif As System.Windows.Forms.Label
End Class
