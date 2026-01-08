<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputHutangBankLeasing
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
        btn_Simpan = New Button()
        btn_Batal = New Button()
        txt_JumlahPinjaman = New TextBox()
        lbl_JumlahPinjaman = New Label()
        dtp_TanggalJatuhTempo = New DateTimePicker()
        Label4 = New Label()
        btn_PilihMitra = New Button()
        txt_KodeKreditur = New TextBox()
        Label24 = New Label()
        txt_NamaKreditur = New TextBox()
        Label5 = New Label()
        Label6 = New Label()
        txt_Keterangan = New RichTextBox()
        dtp_TanggalPersetujuan = New DateTimePicker()
        lbl_TanggalPersetujuan = New Label()
        txt_NomorKontrak = New TextBox()
        Label2 = New Label()
        grb_Persetujuan = New GroupBox()
        dtp_TanggalPencairan = New DateTimePicker()
        lbl_TanggalPencairan = New Label()
        lbl_BiayaAdministrasi = New Label()
        txt_BiayaAdministrasiKontrak = New TextBox()
        lbl_BiayaNotaris = New Label()
        txt_BiayaNotaris = New TextBox()
        lbl_BiayaPPh = New Label()
        txt_BiayaPPh = New TextBox()
        cmb_BankPencairan = New ComboBox()
        lbl_BankPencairan = New Label()
        grb_Pencairan = New GroupBox()
        chk_Pencairan = New CheckBox()
        grb_Persetujuan.SuspendLayout()
        grb_Pencairan.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_Simpan
        ' 
        btn_Simpan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Simpan.Location = New Point(315, 605)
        btn_Simpan.Margin = New Padding(4, 3, 4, 3)
        btn_Simpan.Name = "btn_Simpan"
        btn_Simpan.Size = New Size(97, 40)
        btn_Simpan.TabIndex = 9000
        btn_Simpan.Text = "Simpan"
        btn_Simpan.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Batal.DialogResult = DialogResult.Cancel
        btn_Batal.Location = New Point(211, 605)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 9999
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' txt_JumlahPinjaman
        ' 
        txt_JumlahPinjaman.Location = New Point(142, 119)
        txt_JumlahPinjaman.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahPinjaman.MaxLength = 2
        txt_JumlahPinjaman.Name = "txt_JumlahPinjaman"
        txt_JumlahPinjaman.Size = New Size(136, 23)
        txt_JumlahPinjaman.TabIndex = 150
        txt_JumlahPinjaman.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_JumlahPinjaman
        ' 
        lbl_JumlahPinjaman.AutoSize = True
        lbl_JumlahPinjaman.Location = New Point(12, 122)
        lbl_JumlahPinjaman.Margin = New Padding(4, 0, 4, 0)
        lbl_JumlahPinjaman.Name = "lbl_JumlahPinjaman"
        lbl_JumlahPinjaman.Size = New Size(98, 15)
        lbl_JumlahPinjaman.TabIndex = 10331
        lbl_JumlahPinjaman.Text = "Jumlah Pinjaman"
        ' 
        ' dtp_TanggalJatuhTempo
        ' 
        dtp_TanggalJatuhTempo.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalJatuhTempo.Format = DateTimePickerFormat.Custom
        dtp_TanggalJatuhTempo.Location = New Point(142, 149)
        dtp_TanggalJatuhTempo.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalJatuhTempo.Name = "dtp_TanggalJatuhTempo"
        dtp_TanggalJatuhTempo.Size = New Size(94, 23)
        dtp_TanggalJatuhTempo.TabIndex = 160
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 153)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(74, 15)
        Label4.TabIndex = 10333
        Label4.Text = "Jatuh Tempo"
        ' 
        ' btn_PilihMitra
        ' 
        btn_PilihMitra.Location = New Point(244, 58)
        btn_PilihMitra.Margin = New Padding(4, 3, 4, 3)
        btn_PilihMitra.Name = "btn_PilihMitra"
        btn_PilihMitra.Size = New Size(47, 27)
        btn_PilihMitra.TabIndex = 130
        btn_PilihMitra.Text = "Pilih"
        btn_PilihMitra.UseVisualStyleBackColor = True
        ' 
        ' txt_KodeKreditur
        ' 
        txt_KodeKreditur.CharacterCasing = CharacterCasing.Upper
        txt_KodeKreditur.Location = New Point(142, 59)
        txt_KodeKreditur.Margin = New Padding(4, 3, 4, 3)
        txt_KodeKreditur.MaxLength = 3
        txt_KodeKreditur.Name = "txt_KodeKreditur"
        txt_KodeKreditur.Size = New Size(94, 23)
        txt_KodeKreditur.TabIndex = 120
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label24.Location = New Point(12, 62)
        Label24.Margin = New Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(71, 13)
        Label24.TabIndex = 10346
        Label24.Text = "Kode Kreditur"
        ' 
        ' txt_NamaKreditur
        ' 
        txt_NamaKreditur.Location = New Point(142, 89)
        txt_NamaKreditur.Margin = New Padding(4, 3, 4, 3)
        txt_NamaKreditur.Name = "txt_NamaKreditur"
        txt_NamaKreditur.Size = New Size(224, 23)
        txt_NamaKreditur.TabIndex = 140
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(12, 92)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(74, 13)
        Label5.TabIndex = 10347
        Label5.Text = "Nama Kreditur"
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label6.AutoSize = True
        Label6.Location = New Point(29, 502)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(73, 15)
        Label6.TabIndex = 10349
        Label6.Text = "Keterangan :"
        ' 
        ' txt_Keterangan
        ' 
        txt_Keterangan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_Keterangan.Location = New Point(33, 521)
        txt_Keterangan.Margin = New Padding(4, 3, 4, 3)
        txt_Keterangan.Name = "txt_Keterangan"
        txt_Keterangan.Size = New Size(378, 63)
        txt_Keterangan.TabIndex = 700
        txt_Keterangan.Text = ""
        ' 
        ' dtp_TanggalPersetujuan
        ' 
        dtp_TanggalPersetujuan.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalPersetujuan.Format = DateTimePickerFormat.Custom
        dtp_TanggalPersetujuan.Location = New Point(142, 29)
        dtp_TanggalPersetujuan.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalPersetujuan.Name = "dtp_TanggalPersetujuan"
        dtp_TanggalPersetujuan.Size = New Size(94, 23)
        dtp_TanggalPersetujuan.TabIndex = 110
        ' 
        ' lbl_TanggalPersetujuan
        ' 
        lbl_TanggalPersetujuan.AutoSize = True
        lbl_TanggalPersetujuan.Location = New Point(12, 33)
        lbl_TanggalPersetujuan.Margin = New Padding(4, 0, 4, 0)
        lbl_TanggalPersetujuan.Name = "lbl_TanggalPersetujuan"
        lbl_TanggalPersetujuan.Size = New Size(113, 15)
        lbl_TanggalPersetujuan.TabIndex = 10351
        lbl_TanggalPersetujuan.Text = "Tanggal Persetujuan"
        ' 
        ' txt_NomorKontrak
        ' 
        txt_NomorKontrak.Location = New Point(142, 179)
        txt_NomorKontrak.Margin = New Padding(4, 3, 4, 3)
        txt_NomorKontrak.MaxLength = 99
        txt_NomorKontrak.Name = "txt_NomorKontrak"
        txt_NomorKontrak.Size = New Size(224, 23)
        txt_NomorKontrak.TabIndex = 170
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(12, 182)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(78, 13)
        Label2.TabIndex = 10353
        Label2.Text = "Nomor Kontrak"
        ' 
        ' grb_Persetujuan
        ' 
        grb_Persetujuan.Controls.Add(lbl_TanggalPersetujuan)
        grb_Persetujuan.Controls.Add(Label5)
        grb_Persetujuan.Controls.Add(txt_NamaKreditur)
        grb_Persetujuan.Controls.Add(txt_NomorKontrak)
        grb_Persetujuan.Controls.Add(Label24)
        grb_Persetujuan.Controls.Add(Label2)
        grb_Persetujuan.Controls.Add(txt_KodeKreditur)
        grb_Persetujuan.Controls.Add(dtp_TanggalPersetujuan)
        grb_Persetujuan.Controls.Add(btn_PilihMitra)
        grb_Persetujuan.Controls.Add(dtp_TanggalJatuhTempo)
        grb_Persetujuan.Controls.Add(txt_JumlahPinjaman)
        grb_Persetujuan.Controls.Add(Label4)
        grb_Persetujuan.Controls.Add(lbl_JumlahPinjaman)
        grb_Persetujuan.Location = New Point(29, 20)
        grb_Persetujuan.Margin = New Padding(4, 3, 4, 3)
        grb_Persetujuan.Name = "grb_Persetujuan"
        grb_Persetujuan.Padding = New Padding(4, 3, 4, 3)
        grb_Persetujuan.Size = New Size(383, 223)
        grb_Persetujuan.TabIndex = 100
        grb_Persetujuan.TabStop = False
        grb_Persetujuan.Text = "Persetujuan :"
        ' 
        ' dtp_TanggalPencairan
        ' 
        dtp_TanggalPencairan.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalPencairan.Format = DateTimePickerFormat.Custom
        dtp_TanggalPencairan.Location = New Point(142, 22)
        dtp_TanggalPencairan.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalPencairan.Name = "dtp_TanggalPencairan"
        dtp_TanggalPencairan.Size = New Size(94, 23)
        dtp_TanggalPencairan.TabIndex = 320
        ' 
        ' lbl_TanggalPencairan
        ' 
        lbl_TanggalPencairan.AutoSize = True
        lbl_TanggalPencairan.Location = New Point(12, 27)
        lbl_TanggalPencairan.Margin = New Padding(4, 0, 4, 0)
        lbl_TanggalPencairan.Name = "lbl_TanggalPencairan"
        lbl_TanggalPencairan.Size = New Size(103, 15)
        lbl_TanggalPencairan.TabIndex = 10355
        lbl_TanggalPencairan.Text = "Tanggal Pencairan"
        ' 
        ' lbl_BiayaAdministrasi
        ' 
        lbl_BiayaAdministrasi.AutoSize = True
        lbl_BiayaAdministrasi.Location = New Point(12, 88)
        lbl_BiayaAdministrasi.Margin = New Padding(4, 0, 4, 0)
        lbl_BiayaAdministrasi.Name = "lbl_BiayaAdministrasi"
        lbl_BiayaAdministrasi.Size = New Size(104, 15)
        lbl_BiayaAdministrasi.TabIndex = 10355
        lbl_BiayaAdministrasi.Text = "Biaya Administrasi"
        ' 
        ' txt_BiayaAdministrasiKontrak
        ' 
        txt_BiayaAdministrasiKontrak.Location = New Point(142, 84)
        txt_BiayaAdministrasiKontrak.Margin = New Padding(4, 3, 4, 3)
        txt_BiayaAdministrasiKontrak.MaxLength = 2
        txt_BiayaAdministrasiKontrak.Name = "txt_BiayaAdministrasiKontrak"
        txt_BiayaAdministrasiKontrak.Size = New Size(136, 23)
        txt_BiayaAdministrasiKontrak.TabIndex = 340
        txt_BiayaAdministrasiKontrak.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_BiayaNotaris
        ' 
        lbl_BiayaNotaris.AutoSize = True
        lbl_BiayaNotaris.Location = New Point(12, 118)
        lbl_BiayaNotaris.Margin = New Padding(4, 0, 4, 0)
        lbl_BiayaNotaris.Name = "lbl_BiayaNotaris"
        lbl_BiayaNotaris.Size = New Size(76, 15)
        lbl_BiayaNotaris.TabIndex = 10357
        lbl_BiayaNotaris.Text = "Biaya Notaris"
        ' 
        ' txt_BiayaNotaris
        ' 
        txt_BiayaNotaris.Location = New Point(142, 114)
        txt_BiayaNotaris.Margin = New Padding(4, 3, 4, 3)
        txt_BiayaNotaris.MaxLength = 2
        txt_BiayaNotaris.Name = "txt_BiayaNotaris"
        txt_BiayaNotaris.Size = New Size(136, 23)
        txt_BiayaNotaris.TabIndex = 350
        txt_BiayaNotaris.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_BiayaPPh
        ' 
        lbl_BiayaPPh.AutoSize = True
        lbl_BiayaPPh.Location = New Point(12, 148)
        lbl_BiayaPPh.Margin = New Padding(4, 0, 4, 0)
        lbl_BiayaPPh.Name = "lbl_BiayaPPh"
        lbl_BiayaPPh.Size = New Size(59, 15)
        lbl_BiayaPPh.TabIndex = 10359
        lbl_BiayaPPh.Text = "Biaya PPh"
        ' 
        ' txt_BiayaPPh
        ' 
        txt_BiayaPPh.Location = New Point(142, 144)
        txt_BiayaPPh.Margin = New Padding(4, 3, 4, 3)
        txt_BiayaPPh.MaxLength = 2
        txt_BiayaPPh.Name = "txt_BiayaPPh"
        txt_BiayaPPh.Size = New Size(136, 23)
        txt_BiayaPPh.TabIndex = 360
        txt_BiayaPPh.TextAlign = HorizontalAlignment.Right
        ' 
        ' cmb_BankPencairan
        ' 
        cmb_BankPencairan.FormattingEnabled = True
        cmb_BankPencairan.Location = New Point(142, 53)
        cmb_BankPencairan.Margin = New Padding(4, 3, 4, 3)
        cmb_BankPencairan.Name = "cmb_BankPencairan"
        cmb_BankPencairan.Size = New Size(224, 23)
        cmb_BankPencairan.TabIndex = 330
        ' 
        ' lbl_BankPencairan
        ' 
        lbl_BankPencairan.AutoSize = True
        lbl_BankPencairan.Location = New Point(12, 57)
        lbl_BankPencairan.Margin = New Padding(4, 0, 4, 0)
        lbl_BankPencairan.Name = "lbl_BankPencairan"
        lbl_BankPencairan.Size = New Size(88, 15)
        lbl_BankPencairan.TabIndex = 10361
        lbl_BankPencairan.Text = "Bank Pencairan"
        ' 
        ' grb_Pencairan
        ' 
        grb_Pencairan.Controls.Add(lbl_BankPencairan)
        grb_Pencairan.Controls.Add(cmb_BankPencairan)
        grb_Pencairan.Controls.Add(txt_BiayaPPh)
        grb_Pencairan.Controls.Add(lbl_BiayaPPh)
        grb_Pencairan.Controls.Add(txt_BiayaNotaris)
        grb_Pencairan.Controls.Add(lbl_BiayaNotaris)
        grb_Pencairan.Controls.Add(txt_BiayaAdministrasiKontrak)
        grb_Pencairan.Controls.Add(lbl_BiayaAdministrasi)
        grb_Pencairan.Controls.Add(lbl_TanggalPencairan)
        grb_Pencairan.Controls.Add(dtp_TanggalPencairan)
        grb_Pencairan.Enabled = False
        grb_Pencairan.Location = New Point(29, 281)
        grb_Pencairan.Margin = New Padding(4, 3, 4, 3)
        grb_Pencairan.Name = "grb_Pencairan"
        grb_Pencairan.Padding = New Padding(4, 3, 4, 3)
        grb_Pencairan.Size = New Size(383, 186)
        grb_Pencairan.TabIndex = 310
        grb_Pencairan.TabStop = False
        grb_Pencairan.Visible = False
        ' 
        ' chk_Pencairan
        ' 
        chk_Pencairan.AutoSize = True
        chk_Pencairan.Location = New Point(34, 264)
        chk_Pencairan.Margin = New Padding(4, 3, 4, 3)
        chk_Pencairan.Name = "chk_Pencairan"
        chk_Pencairan.Size = New Size(84, 19)
        chk_Pencairan.TabIndex = 300
        chk_Pencairan.Text = "Pencairan :"
        chk_Pencairan.UseVisualStyleBackColor = True
        chk_Pencairan.Visible = False
        ' 
        ' frm_InputHutangBankLeasing
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(442, 669)
        Controls.Add(grb_Persetujuan)
        Controls.Add(chk_Pencairan)
        Controls.Add(grb_Pencairan)
        Controls.Add(Label6)
        Controls.Add(txt_Keterangan)
        Controls.Add(btn_Simpan)
        Controls.Add(btn_Batal)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_InputHutangBankLeasing"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input Hutang Bank"
        grb_Persetujuan.ResumeLayout(False)
        grb_Persetujuan.PerformLayout()
        grb_Pencairan.ResumeLayout(False)
        grb_Pencairan.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_JumlahPinjaman As TextBox
    Friend WithEvents lbl_JumlahPinjaman As Label
    Friend WithEvents dtp_TanggalJatuhTempo As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents txt_KodeKreditur As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_NamaKreditur As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents dtp_TanggalPersetujuan As DateTimePicker
    Friend WithEvents lbl_TanggalPersetujuan As Label
    Friend WithEvents txt_NomorKontrak As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents grb_Persetujuan As GroupBox
    Friend WithEvents dtp_TanggalPencairan As DateTimePicker
    Friend WithEvents lbl_TanggalPencairan As Label
    Friend WithEvents lbl_BiayaAdministrasi As Label
    Friend WithEvents txt_BiayaAdministrasiKontrak As TextBox
    Friend WithEvents lbl_BiayaNotaris As Label
    Friend WithEvents txt_BiayaNotaris As TextBox
    Friend WithEvents lbl_BiayaPPh As Label
    Friend WithEvents txt_BiayaPPh As TextBox
    Friend WithEvents cmb_BankPencairan As ComboBox
    Friend WithEvents lbl_BankPencairan As Label
    Friend WithEvents grb_Pencairan As GroupBox
    Friend WithEvents chk_Pencairan As CheckBox
End Class
