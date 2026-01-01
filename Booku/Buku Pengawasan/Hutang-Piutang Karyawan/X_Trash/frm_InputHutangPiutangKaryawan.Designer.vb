<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputHutangPiutangKaryawan
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
        Label6 = New Label()
        txt_NamaKaryawan = New TextBox()
        Label3 = New Label()
        Label2 = New Label()
        txt_NomorIDKaryawan = New TextBox()
        Label5 = New Label()
        txt_Keterangan = New RichTextBox()
        dtp_TanggalPinjam = New DateTimePicker()
        Label4 = New Label()
        Label1 = New Label()
        txt_JumlahPinjaman = New TextBox()
        lbl_SaldoAwal = New Label()
        txt_SaldoAwal = New TextBox()
        btn_PilihIDKaryawan = New Button()
        lbl_NomorBP = New Label()
        txt_NomorBP = New TextBox()
        txt_Jabatan = New TextBox()
        grb_Bank = New GroupBox()
        Label54 = New Label()
        Label55 = New Label()
        txt_BiayaAdministrasiBank = New TextBox()
        cmb_DitanggungOleh = New ComboBox()
        txt_JumlahTransfer = New TextBox()
        Label12 = New Label()
        cmb_SaranaPembayaran = New ComboBox()
        lbl_SaranaPembayaran = New Label()
        Label7 = New Label()
        txt_NomorDokumen = New TextBox()
        grb_Bank.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_Simpan
        ' 
        btn_Simpan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Simpan.Location = New Point(272, 615)
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
        btn_Batal.Location = New Point(168, 615)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 9999
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(24, 183)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(47, 15)
        Label6.TabIndex = 10353
        Label6.Text = "Jabatan"
        ' 
        ' txt_NamaKaryawan
        ' 
        txt_NamaKaryawan.Enabled = False
        txt_NamaKaryawan.Location = New Point(155, 150)
        txt_NamaKaryawan.Margin = New Padding(4, 3, 4, 3)
        txt_NamaKaryawan.MaxLength = 99
        txt_NamaKaryawan.Name = "txt_NamaKaryawan"
        txt_NamaKaryawan.Size = New Size(213, 23)
        txt_NamaKaryawan.TabIndex = 50
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(24, 153)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(93, 15)
        Label3.TabIndex = 10352
        Label3.Text = "Nama Karyawan"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(24, 123)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(113, 15)
        Label2.TabIndex = 10351
        Label2.Text = "Nomor ID Karyawan"
        ' 
        ' txt_NomorIDKaryawan
        ' 
        txt_NomorIDKaryawan.Location = New Point(155, 120)
        txt_NomorIDKaryawan.Margin = New Padding(4, 3, 4, 3)
        txt_NomorIDKaryawan.MaxLength = 99
        txt_NomorIDKaryawan.Name = "txt_NomorIDKaryawan"
        txt_NomorIDKaryawan.Size = New Size(117, 23)
        txt_NomorIDKaryawan.TabIndex = 30
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(24, 472)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(73, 15)
        Label5.TabIndex = 10350
        Label5.Text = "Keterangan :"
        ' 
        ' txt_Keterangan
        ' 
        txt_Keterangan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_Keterangan.Location = New Point(28, 494)
        txt_Keterangan.Margin = New Padding(4, 3, 4, 3)
        txt_Keterangan.Name = "txt_Keterangan"
        txt_Keterangan.Size = New Size(340, 96)
        txt_Keterangan.TabIndex = 900
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
        Label4.TabIndex = 10348
        Label4.Text = "Tanggal Pinjam"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(24, 213)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(98, 15)
        Label1.TabIndex = 10355
        Label1.Text = "Jumlah Pinjaman"
        ' 
        ' txt_JumlahPinjaman
        ' 
        txt_JumlahPinjaman.Location = New Point(155, 210)
        txt_JumlahPinjaman.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahPinjaman.MaxLength = 99
        txt_JumlahPinjaman.Name = "txt_JumlahPinjaman"
        txt_JumlahPinjaman.Size = New Size(117, 23)
        txt_JumlahPinjaman.TabIndex = 70
        txt_JumlahPinjaman.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_SaldoAwal
        ' 
        lbl_SaldoAwal.AutoSize = True
        lbl_SaldoAwal.Location = New Point(24, 243)
        lbl_SaldoAwal.Margin = New Padding(4, 0, 4, 0)
        lbl_SaldoAwal.Name = "lbl_SaldoAwal"
        lbl_SaldoAwal.Size = New Size(65, 15)
        lbl_SaldoAwal.TabIndex = 10357
        lbl_SaldoAwal.Text = "Saldo Awal"
        ' 
        ' txt_SaldoAwal
        ' 
        txt_SaldoAwal.Location = New Point(155, 240)
        txt_SaldoAwal.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoAwal.MaxLength = 99
        txt_SaldoAwal.Name = "txt_SaldoAwal"
        txt_SaldoAwal.Size = New Size(117, 23)
        txt_SaldoAwal.TabIndex = 80
        txt_SaldoAwal.TextAlign = HorizontalAlignment.Right
        ' 
        ' btn_PilihIDKaryawan
        ' 
        btn_PilihIDKaryawan.Location = New Point(275, 118)
        btn_PilihIDKaryawan.Margin = New Padding(4, 3, 4, 3)
        btn_PilihIDKaryawan.Name = "btn_PilihIDKaryawan"
        btn_PilihIDKaryawan.Size = New Size(47, 27)
        btn_PilihIDKaryawan.TabIndex = 40
        btn_PilihIDKaryawan.Text = "Pilih"
        btn_PilihIDKaryawan.UseVisualStyleBackColor = True
        ' 
        ' lbl_NomorBP
        ' 
        lbl_NomorBP.AutoSize = True
        lbl_NomorBP.Location = New Point(24, 63)
        lbl_NomorBP.Margin = New Padding(4, 0, 4, 0)
        lbl_NomorBP.Name = "lbl_NomorBP"
        lbl_NomorBP.Size = New Size(62, 15)
        lbl_NomorBP.TabIndex = 10360
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
        ' txt_Jabatan
        ' 
        txt_Jabatan.Enabled = False
        txt_Jabatan.Location = New Point(155, 180)
        txt_Jabatan.Margin = New Padding(4, 3, 4, 3)
        txt_Jabatan.MaxLength = 99
        txt_Jabatan.Name = "txt_Jabatan"
        txt_Jabatan.Size = New Size(213, 23)
        txt_Jabatan.TabIndex = 60
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
        grb_Bank.Location = New Point(28, 320)
        grb_Bank.Margin = New Padding(4, 3, 4, 3)
        grb_Bank.Name = "grb_Bank"
        grb_Bank.Padding = New Padding(4, 3, 4, 3)
        grb_Bank.Size = New Size(341, 123)
        grb_Bank.TabIndex = 10391
        grb_Bank.TabStop = False
        grb_Bank.Text = "Bank :"
        grb_Bank.Visible = False
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
        txt_BiayaAdministrasiBank.TabIndex = 490
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
        cmb_DitanggungOleh.TabIndex = 500
        ' 
        ' txt_JumlahTransfer
        ' 
        txt_JumlahTransfer.Location = New Point(144, 85)
        txt_JumlahTransfer.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahTransfer.Name = "txt_JumlahTransfer"
        txt_JumlahTransfer.Size = New Size(146, 23)
        txt_JumlahTransfer.TabIndex = 510
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
        cmb_SaranaPembayaran.Location = New Point(155, 273)
        cmb_SaranaPembayaran.Margin = New Padding(4, 3, 4, 3)
        cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        cmb_SaranaPembayaran.Size = New Size(213, 23)
        cmb_SaranaPembayaran.TabIndex = 10390
        cmb_SaranaPembayaran.Visible = False
        ' 
        ' lbl_SaranaPembayaran
        ' 
        lbl_SaranaPembayaran.AutoSize = True
        lbl_SaranaPembayaran.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_SaranaPembayaran.Location = New Point(24, 277)
        lbl_SaranaPembayaran.Margin = New Padding(4, 0, 4, 0)
        lbl_SaranaPembayaran.Name = "lbl_SaranaPembayaran"
        lbl_SaranaPembayaran.Size = New Size(103, 13)
        lbl_SaranaPembayaran.TabIndex = 10392
        lbl_SaranaPembayaran.Text = "Sarana Pembayaran"
        lbl_SaranaPembayaran.Visible = False
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(24, 93)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(100, 15)
        Label7.TabIndex = 10416
        Label7.Text = "Nomor Dokumen"
        ' 
        ' txt_NomorDokumen
        ' 
        txt_NomorDokumen.Location = New Point(155, 90)
        txt_NomorDokumen.Margin = New Padding(4, 3, 4, 3)
        txt_NomorDokumen.MaxLength = 99
        txt_NomorDokumen.Name = "txt_NomorDokumen"
        txt_NomorDokumen.Size = New Size(213, 23)
        txt_NomorDokumen.TabIndex = 10415
        ' 
        ' frm_InputHutangPiutangKaryawan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(401, 684)
        Controls.Add(Label7)
        Controls.Add(txt_NomorDokumen)
        Controls.Add(grb_Bank)
        Controls.Add(cmb_SaranaPembayaran)
        Controls.Add(lbl_SaranaPembayaran)
        Controls.Add(txt_Jabatan)
        Controls.Add(lbl_NomorBP)
        Controls.Add(txt_NomorBP)
        Controls.Add(btn_PilihIDKaryawan)
        Controls.Add(lbl_SaldoAwal)
        Controls.Add(txt_SaldoAwal)
        Controls.Add(Label1)
        Controls.Add(txt_JumlahPinjaman)
        Controls.Add(Label6)
        Controls.Add(txt_NamaKaryawan)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(txt_NomorIDKaryawan)
        Controls.Add(Label5)
        Controls.Add(txt_Keterangan)
        Controls.Add(dtp_TanggalPinjam)
        Controls.Add(Label4)
        Controls.Add(btn_Simpan)
        Controls.Add(btn_Batal)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_InputHutangPiutangKaryawan"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input Hutang/Piutang Karyawan"
        grb_Bank.ResumeLayout(False)
        grb_Bank.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_NamaKaryawan As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NomorIDKaryawan As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents dtp_TanggalPinjam As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_JumlahPinjaman As TextBox
    Friend WithEvents lbl_SaldoAwal As Label
    Friend WithEvents txt_SaldoAwal As TextBox
    Friend WithEvents btn_PilihIDKaryawan As Button
    Friend WithEvents lbl_NomorBP As Label
    Friend WithEvents txt_NomorBP As TextBox
    Friend WithEvents txt_Jabatan As TextBox
    Friend WithEvents grb_Bank As GroupBox
    Friend WithEvents Label54 As Label
    Friend WithEvents Label55 As Label
    Friend WithEvents txt_BiayaAdministrasiBank As TextBox
    Friend WithEvents cmb_DitanggungOleh As ComboBox
    Friend WithEvents txt_JumlahTransfer As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cmb_SaranaPembayaran As ComboBox
    Friend WithEvents lbl_SaranaPembayaran As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_NomorDokumen As TextBox
End Class
