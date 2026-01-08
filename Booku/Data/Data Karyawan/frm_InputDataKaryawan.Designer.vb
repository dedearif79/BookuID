<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputDataKaryawan
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
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_NIK = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_TanggalRegistrasi = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Catatan = New System.Windows.Forms.RichTextBox()
        Me.cmb_Jabatan = New System.Windows.Forms.ComboBox()
        Me.txt_NomorIDKaryawan = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_NamaKaryawan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chk_StatusAktif = New System.Windows.Forms.CheckBox()
        Me.txt_AtasNama = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_RekeningBank = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(235, 399)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9000
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(146, 399)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_NIK
        '
        Me.txt_NIK.Location = New System.Drawing.Point(135, 84)
        Me.txt_NIK.MaxLength = 99
        Me.txt_NIK.Name = "txt_NIK"
        Me.txt_NIK.Size = New System.Drawing.Size(183, 20)
        Me.txt_NIK.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 10333
        Me.Label1.Text = "NIK"
        '
        'dtp_TanggalRegistrasi
        '
        Me.dtp_TanggalRegistrasi.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalRegistrasi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalRegistrasi.Location = New System.Drawing.Point(135, 32)
        Me.dtp_TanggalRegistrasi.Name = "dtp_TanggalRegistrasi"
        Me.dtp_TanggalRegistrasi.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalRegistrasi.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(30, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 10332
        Me.Label4.Text = "Tanggal Registrasi"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 238)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 10335
        Me.Label5.Text = "Catatan :"
        '
        'txt_Catatan
        '
        Me.txt_Catatan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Catatan.Location = New System.Drawing.Point(33, 257)
        Me.txt_Catatan.Name = "txt_Catatan"
        Me.txt_Catatan.Size = New System.Drawing.Size(285, 84)
        Me.txt_Catatan.TabIndex = 900
        Me.txt_Catatan.Text = ""
        '
        'cmb_Jabatan
        '
        Me.cmb_Jabatan.FormattingEnabled = True
        Me.cmb_Jabatan.Location = New System.Drawing.Point(135, 136)
        Me.cmb_Jabatan.Name = "cmb_Jabatan"
        Me.cmb_Jabatan.Size = New System.Drawing.Size(183, 21)
        Me.cmb_Jabatan.TabIndex = 50
        '
        'txt_NomorIDKaryawan
        '
        Me.txt_NomorIDKaryawan.Location = New System.Drawing.Point(135, 58)
        Me.txt_NomorIDKaryawan.MaxLength = 99
        Me.txt_NomorIDKaryawan.Name = "txt_NomorIDKaryawan"
        Me.txt_NomorIDKaryawan.Size = New System.Drawing.Size(183, 20)
        Me.txt_NomorIDKaryawan.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 10338
        Me.Label2.Text = "Nomor ID"
        '
        'txt_NamaKaryawan
        '
        Me.txt_NamaKaryawan.Location = New System.Drawing.Point(135, 110)
        Me.txt_NamaKaryawan.MaxLength = 99
        Me.txt_NamaKaryawan.Name = "txt_NamaKaryawan"
        Me.txt_NamaKaryawan.Size = New System.Drawing.Size(183, 20)
        Me.txt_NamaKaryawan.TabIndex = 40
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 10340
        Me.Label3.Text = "Nama Karyawan"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 139)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 10341
        Me.Label6.Text = "Jabatan"
        '
        'chk_StatusAktif
        '
        Me.chk_StatusAktif.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chk_StatusAktif.AutoSize = True
        Me.chk_StatusAktif.Location = New System.Drawing.Point(33, 357)
        Me.chk_StatusAktif.Name = "chk_StatusAktif"
        Me.chk_StatusAktif.Size = New System.Drawing.Size(80, 17)
        Me.chk_StatusAktif.TabIndex = 990
        Me.chk_StatusAktif.Text = "Status Aktif"
        Me.chk_StatusAktif.UseVisualStyleBackColor = True
        '
        'txt_AtasNama
        '
        Me.txt_AtasNama.Location = New System.Drawing.Point(135, 189)
        Me.txt_AtasNama.Name = "txt_AtasNama"
        Me.txt_AtasNama.Size = New System.Drawing.Size(183, 20)
        Me.txt_AtasNama.TabIndex = 10381
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(30, 192)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 13)
        Me.Label9.TabIndex = 10383
        Me.Label9.Text = "Atas Nama"
        '
        'txt_RekeningBank
        '
        Me.txt_RekeningBank.Location = New System.Drawing.Point(135, 163)
        Me.txt_RekeningBank.Name = "txt_RekeningBank"
        Me.txt_RekeningBank.Size = New System.Drawing.Size(183, 20)
        Me.txt_RekeningBank.TabIndex = 10380
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(30, 166)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 13)
        Me.Label10.TabIndex = 10382
        Me.Label10.Text = "Rekening Bank"
        '
        'frm_InputDataKaryawan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 458)
        Me.Controls.Add(Me.txt_AtasNama)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_RekeningBank)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.chk_StatusAktif)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_NamaKaryawan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_NomorIDKaryawan)
        Me.Controls.Add(Me.cmb_Jabatan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_Catatan)
        Me.Controls.Add(Me.txt_NIK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtp_TanggalRegistrasi)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputDataKaryawan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Data Karyawan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_NIK As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtp_TanggalRegistrasi As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Catatan As RichTextBox
    Friend WithEvents cmb_Jabatan As ComboBox
    Friend WithEvents txt_NomorIDKaryawan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NamaKaryawan As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents chk_StatusAktif As CheckBox
    Friend WithEvents txt_AtasNama As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_RekeningBank As TextBox
    Friend WithEvents Label10 As Label
End Class
