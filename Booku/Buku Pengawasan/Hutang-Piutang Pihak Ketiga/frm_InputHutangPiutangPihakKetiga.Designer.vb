<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputHutangPiutangPihakKetiga
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
        grb_Bank = New GroupBox()
        Label54 = New Label()
        Label55 = New Label()
        txt_BiayaAdministrasiBank = New TextBox()
        cmb_DitanggungOleh = New ComboBox()
        txt_JumlahTransfer = New TextBox()
        Label12 = New Label()
        cmb_SaranaPembayaran = New ComboBox()
        lbl_SaranaPembayaran = New Label()
        lbl_NomorBP = New Label()
        txt_NomorBP = New TextBox()
        btn_PilihMitra = New Button()
        lbl_SaldoAwal = New Label()
        txt_SaldoAwal = New TextBox()
        Label1 = New Label()
        txt_JumlahPinjaman = New TextBox()
        txt_NamaLawanTransaksi = New TextBox()
        lbl_NamaLawanTransaksi = New Label()
        lbl_KodeLawanTransaksi = New Label()
        txt_KodeLawanTransaksi = New TextBox()
        Label5 = New Label()
        txt_Keterangan = New RichTextBox()
        dtp_TanggalPinjam = New DateTimePicker()
        Label4 = New Label()
        chk_JatuhTempo = New CheckBox()
        txt_NomorKontrak = New TextBox()
        Label2 = New Label()
        dtp_TanggalJatuhTempo = New DateTimePicker()
        Label3 = New Label()
        grb_Bank.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_Simpan
        ' 
        btn_Simpan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Simpan.Location = New Point(272, 616)
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
        btn_Batal.Location = New Point(168, 616)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 9999
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' grb_Bank
        ' 
        grb_Bank.Controls.Add(Label54)
        grb_Bank.Controls.Add(Label55)
        grb_Bank.Controls.Add(txt_BiayaAdministrasiBank)
        grb_Bank.Controls.Add(cmb_DitanggungOleh)
        grb_Bank.Controls.Add(txt_JumlahTransfer)
        grb_Bank.Controls.Add(Label12)
        grb_Bank.Enabled = False
        grb_Bank.Location = New Point(28, 318)
        grb_Bank.Margin = New Padding(4, 3, 4, 3)
        grb_Bank.Name = "grb_Bank"
        grb_Bank.Padding = New Padding(4, 3, 4, 3)
        grb_Bank.Size = New Size(341, 123)
        grb_Bank.TabIndex = 200
        grb_Bank.TabStop = False
        grb_Bank.Text = "Bank :"
        ' 
        ' Label54
        ' 
        Label54.AutoSize = True
        Label54.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label54.Location = New Point(12, 58)
        Label54.Margin = New Padding(4, 0, 4, 0)
        Label54.Name = "Label54"
        Label54.Size = New Size(87, 13)
        Label54.TabIndex = 10053
        Label54.Text = "Ditanggung Oleh"
        ' 
        ' Label55
        ' 
        Label55.AutoSize = True
        Label55.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label55.Location = New Point(12, 28)
        Label55.Margin = New Padding(4, 0, 4, 0)
        Label55.Name = "Label55"
        Label55.Size = New Size(91, 13)
        Label55.TabIndex = 10051
        Label55.Text = "Biaya Administrasi"
        ' 
        ' txt_BiayaAdministrasiBank
        ' 
        txt_BiayaAdministrasiBank.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_BiayaAdministrasiBank.Location = New Point(144, 24)
        txt_BiayaAdministrasiBank.Margin = New Padding(4, 3, 4, 3)
        txt_BiayaAdministrasiBank.Name = "txt_BiayaAdministrasiBank"
        txt_BiayaAdministrasiBank.Size = New Size(146, 20)
        txt_BiayaAdministrasiBank.TabIndex = 210
        txt_BiayaAdministrasiBank.TextAlign = HorizontalAlignment.Right
        ' 
        ' cmb_DitanggungOleh
        ' 
        cmb_DitanggungOleh.Enabled = False
        cmb_DitanggungOleh.FormattingEnabled = True
        cmb_DitanggungOleh.Location = New Point(142, 54)
        cmb_DitanggungOleh.Margin = New Padding(4, 3, 4, 3)
        cmb_DitanggungOleh.Name = "cmb_DitanggungOleh"
        cmb_DitanggungOleh.Size = New Size(148, 23)
        cmb_DitanggungOleh.TabIndex = 220
        ' 
        ' txt_JumlahTransfer
        ' 
        txt_JumlahTransfer.Location = New Point(144, 85)
        txt_JumlahTransfer.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahTransfer.Name = "txt_JumlahTransfer"
        txt_JumlahTransfer.Size = New Size(146, 23)
        txt_JumlahTransfer.TabIndex = 230
        txt_JumlahTransfer.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.Location = New Point(12, 89)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(82, 13)
        Label12.TabIndex = 10391
        Label12.Text = "Jumlah Transfer"
        ' 
        ' cmb_SaranaPembayaran
        ' 
        cmb_SaranaPembayaran.FormattingEnabled = True
        cmb_SaranaPembayaran.Location = New Point(155, 272)
        cmb_SaranaPembayaran.Margin = New Padding(4, 3, 4, 3)
        cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        cmb_SaranaPembayaran.Size = New Size(213, 23)
        cmb_SaranaPembayaran.TabIndex = 110
        ' 
        ' lbl_SaranaPembayaran
        ' 
        lbl_SaranaPembayaran.AutoSize = True
        lbl_SaranaPembayaran.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_SaranaPembayaran.Location = New Point(24, 276)
        lbl_SaranaPembayaran.Margin = New Padding(4, 0, 4, 0)
        lbl_SaranaPembayaran.Name = "lbl_SaranaPembayaran"
        lbl_SaranaPembayaran.Size = New Size(103, 13)
        lbl_SaranaPembayaran.TabIndex = 10412
        lbl_SaranaPembayaran.Text = "Sarana Pembayaran"
        ' 
        ' lbl_NomorBP
        ' 
        lbl_NomorBP.AutoSize = True
        lbl_NomorBP.Location = New Point(24, 63)
        lbl_NomorBP.Margin = New Padding(4, 0, 4, 0)
        lbl_NomorBP.Name = "lbl_NomorBP"
        lbl_NomorBP.Size = New Size(62, 15)
        lbl_NomorBP.TabIndex = 10409
        lbl_NomorBP.Text = "Nomor BP"
        ' 
        ' txt_NomorBP
        ' 
        txt_NomorBP.Location = New Point(155, 60)
        txt_NomorBP.Margin = New Padding(4, 3, 4, 3)
        txt_NomorBP.MaxLength = 99
        txt_NomorBP.Name = "txt_NomorBP"
        txt_NomorBP.Size = New Size(117, 23)
        txt_NomorBP.TabIndex = 20
        ' 
        ' btn_PilihMitra
        ' 
        btn_PilihMitra.Location = New Point(275, 88)
        btn_PilihMitra.Margin = New Padding(4, 3, 4, 3)
        btn_PilihMitra.Name = "btn_PilihMitra"
        btn_PilihMitra.Size = New Size(47, 27)
        btn_PilihMitra.TabIndex = 40
        btn_PilihMitra.Text = "Pilih"
        btn_PilihMitra.UseVisualStyleBackColor = True
        ' 
        ' lbl_SaldoAwal
        ' 
        lbl_SaldoAwal.AutoSize = True
        lbl_SaldoAwal.Location = New Point(24, 183)
        lbl_SaldoAwal.Margin = New Padding(4, 0, 4, 0)
        lbl_SaldoAwal.Name = "lbl_SaldoAwal"
        lbl_SaldoAwal.Size = New Size(65, 15)
        lbl_SaldoAwal.TabIndex = 10408
        lbl_SaldoAwal.Text = "Saldo Awal"
        ' 
        ' txt_SaldoAwal
        ' 
        txt_SaldoAwal.Location = New Point(155, 180)
        txt_SaldoAwal.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoAwal.MaxLength = 99
        txt_SaldoAwal.Name = "txt_SaldoAwal"
        txt_SaldoAwal.Size = New Size(117, 23)
        txt_SaldoAwal.TabIndex = 70
        txt_SaldoAwal.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(24, 153)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(98, 15)
        Label1.TabIndex = 10407
        Label1.Text = "Jumlah Pinjaman"
        ' 
        ' txt_JumlahPinjaman
        ' 
        txt_JumlahPinjaman.Location = New Point(155, 150)
        txt_JumlahPinjaman.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahPinjaman.MaxLength = 99
        txt_JumlahPinjaman.Name = "txt_JumlahPinjaman"
        txt_JumlahPinjaman.Size = New Size(117, 23)
        txt_JumlahPinjaman.TabIndex = 60
        txt_JumlahPinjaman.TextAlign = HorizontalAlignment.Right
        ' 
        ' txt_NamaLawanTransaksi
        ' 
        txt_NamaLawanTransaksi.Enabled = False
        txt_NamaLawanTransaksi.Location = New Point(155, 120)
        txt_NamaLawanTransaksi.Margin = New Padding(4, 3, 4, 3)
        txt_NamaLawanTransaksi.MaxLength = 99
        txt_NamaLawanTransaksi.Name = "txt_NamaLawanTransaksi"
        txt_NamaLawanTransaksi.Size = New Size(213, 23)
        txt_NamaLawanTransaksi.TabIndex = 50
        ' 
        ' lbl_NamaLawanTransaksi
        ' 
        lbl_NamaLawanTransaksi.AutoSize = True
        lbl_NamaLawanTransaksi.Location = New Point(24, 123)
        lbl_NamaLawanTransaksi.Margin = New Padding(4, 0, 4, 0)
        lbl_NamaLawanTransaksi.Name = "lbl_NamaLawanTransaksi"
        lbl_NamaLawanTransaksi.Size = New Size(94, 15)
        lbl_NamaLawanTransaksi.TabIndex = 10405
        lbl_NamaLawanTransaksi.Text = "Nama Lawan Trx"
        ' 
        ' lbl_KodeLawanTransaksi
        ' 
        lbl_KodeLawanTransaksi.AutoSize = True
        lbl_KodeLawanTransaksi.Location = New Point(24, 93)
        lbl_KodeLawanTransaksi.Margin = New Padding(4, 0, 4, 0)
        lbl_KodeLawanTransaksi.Name = "lbl_KodeLawanTransaksi"
        lbl_KodeLawanTransaksi.Size = New Size(89, 15)
        lbl_KodeLawanTransaksi.TabIndex = 10404
        lbl_KodeLawanTransaksi.Text = "Kode Lawan Trx"
        ' 
        ' txt_KodeLawanTransaksi
        ' 
        txt_KodeLawanTransaksi.Location = New Point(155, 90)
        txt_KodeLawanTransaksi.Margin = New Padding(4, 3, 4, 3)
        txt_KodeLawanTransaksi.MaxLength = 99
        txt_KodeLawanTransaksi.Name = "txt_KodeLawanTransaksi"
        txt_KodeLawanTransaksi.Size = New Size(117, 23)
        txt_KodeLawanTransaksi.TabIndex = 30
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(24, 473)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(73, 15)
        Label5.TabIndex = 10403
        Label5.Text = "Keterangan :"
        ' 
        ' txt_Keterangan
        ' 
        txt_Keterangan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_Keterangan.Location = New Point(28, 495)
        txt_Keterangan.Margin = New Padding(4, 3, 4, 3)
        txt_Keterangan.Name = "txt_Keterangan"
        txt_Keterangan.Size = New Size(340, 96)
        txt_Keterangan.TabIndex = 300
        txt_Keterangan.Text = ""
        ' 
        ' dtp_TanggalPinjam
        ' 
        dtp_TanggalPinjam.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalPinjam.Format = DateTimePickerFormat.Custom
        dtp_TanggalPinjam.Location = New Point(155, 30)
        dtp_TanggalPinjam.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalPinjam.Name = "dtp_TanggalPinjam"
        dtp_TanggalPinjam.Size = New Size(94, 23)
        dtp_TanggalPinjam.TabIndex = 10
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(24, 33)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(88, 15)
        Label4.TabIndex = 10402
        Label4.Text = "Tanggal Pinjam"
        ' 
        ' chk_JatuhTempo
        ' 
        chk_JatuhTempo.AutoSize = True
        chk_JatuhTempo.Location = New Point(155, 216)
        chk_JatuhTempo.Margin = New Padding(4, 3, 4, 3)
        chk_JatuhTempo.Name = "chk_JatuhTempo"
        chk_JatuhTempo.Size = New Size(15, 14)
        chk_JatuhTempo.TabIndex = 80
        chk_JatuhTempo.UseVisualStyleBackColor = True
        ' 
        ' txt_NomorKontrak
        ' 
        txt_NomorKontrak.Location = New Point(155, 242)
        txt_NomorKontrak.Margin = New Padding(4, 3, 4, 3)
        txt_NomorKontrak.MaxLength = 99
        txt_NomorKontrak.Name = "txt_NomorKontrak"
        txt_NomorKontrak.Size = New Size(213, 23)
        txt_NomorKontrak.TabIndex = 100
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(24, 246)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(78, 13)
        Label2.TabIndex = 10417
        Label2.Text = "Nomor Kontrak"
        ' 
        ' dtp_TanggalJatuhTempo
        ' 
        dtp_TanggalJatuhTempo.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalJatuhTempo.Format = DateTimePickerFormat.Custom
        dtp_TanggalJatuhTempo.Location = New Point(180, 212)
        dtp_TanggalJatuhTempo.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalJatuhTempo.Name = "dtp_TanggalJatuhTempo"
        dtp_TanggalJatuhTempo.Size = New Size(94, 23)
        dtp_TanggalJatuhTempo.TabIndex = 90
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(24, 216)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(74, 15)
        Label3.TabIndex = 10416
        Label3.Text = "Jatuh Tempo"
        ' 
        ' frm_InputHutangPiutangPihakKetiga
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(401, 685)
        Controls.Add(chk_JatuhTempo)
        Controls.Add(txt_NomorKontrak)
        Controls.Add(Label2)
        Controls.Add(dtp_TanggalJatuhTempo)
        Controls.Add(Label3)
        Controls.Add(grb_Bank)
        Controls.Add(cmb_SaranaPembayaran)
        Controls.Add(lbl_SaranaPembayaran)
        Controls.Add(lbl_NomorBP)
        Controls.Add(txt_NomorBP)
        Controls.Add(btn_PilihMitra)
        Controls.Add(lbl_SaldoAwal)
        Controls.Add(txt_SaldoAwal)
        Controls.Add(Label1)
        Controls.Add(txt_JumlahPinjaman)
        Controls.Add(txt_NamaLawanTransaksi)
        Controls.Add(lbl_NamaLawanTransaksi)
        Controls.Add(lbl_KodeLawanTransaksi)
        Controls.Add(txt_KodeLawanTransaksi)
        Controls.Add(Label5)
        Controls.Add(txt_Keterangan)
        Controls.Add(dtp_TanggalPinjam)
        Controls.Add(Label4)
        Controls.Add(btn_Simpan)
        Controls.Add(btn_Batal)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_InputHutangPiutangPihakKetiga"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input Hutang/Piutang Pihak Ketiga"
        grb_Bank.ResumeLayout(False)
        grb_Bank.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents grb_Bank As GroupBox
    Friend WithEvents Label54 As Label
    Friend WithEvents Label55 As Label
    Friend WithEvents txt_BiayaAdministrasiBank As TextBox
    Friend WithEvents cmb_DitanggungOleh As ComboBox
    Friend WithEvents txt_JumlahTransfer As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cmb_SaranaPembayaran As ComboBox
    Friend WithEvents lbl_SaranaPembayaran As Label
    Friend WithEvents lbl_NomorBP As Label
    Friend WithEvents txt_NomorBP As TextBox
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents lbl_SaldoAwal As Label
    Friend WithEvents txt_SaldoAwal As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_JumlahPinjaman As TextBox
    Friend WithEvents txt_NamaLawanTransaksi As TextBox
    Friend WithEvents lbl_NamaLawanTransaksi As Label
    Friend WithEvents lbl_KodeLawanTransaksi As Label
    Friend WithEvents txt_KodeLawanTransaksi As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents dtp_TanggalPinjam As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents chk_JatuhTempo As CheckBox
    Friend WithEvents txt_NomorKontrak As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents dtp_TanggalJatuhTempo As DateTimePicker
    Friend WithEvents Label3 As Label
End Class
