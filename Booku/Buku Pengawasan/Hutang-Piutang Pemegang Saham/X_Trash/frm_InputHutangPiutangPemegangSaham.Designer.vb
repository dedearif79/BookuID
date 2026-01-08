<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputHutangPiutangPemegangSaham
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
        btn_PilihNIK = New Button()
        lbl_SaldoAwal = New Label()
        txt_SaldoAwal = New TextBox()
        Label1 = New Label()
        txt_JumlahPinjaman = New TextBox()
        txt_NamaPemegangSaham = New TextBox()
        Label3 = New Label()
        Label2 = New Label()
        txt_NIK = New TextBox()
        Label5 = New Label()
        txt_Keterangan = New RichTextBox()
        dtp_TanggalPinjam = New DateTimePicker()
        Label4 = New Label()
        Label6 = New Label()
        txt_NomorDokumen = New TextBox()
        grb_Bank.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_Simpan
        ' 
        btn_Simpan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Simpan.Location = New Point(272, 598)
        btn_Simpan.Margin = New Padding(4, 3, 4, 3)
        btn_Simpan.Name = "btn_Simpan"
        btn_Simpan.Size = New Size(97, 40)
        btn_Simpan.TabIndex = 10326
        btn_Simpan.Text = "Simpan"
        btn_Simpan.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Batal.DialogResult = DialogResult.Cancel
        btn_Batal.Location = New Point(168, 598)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 10327
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
        grb_Bank.Location = New Point(28, 290)
        grb_Bank.Margin = New Padding(4, 3, 4, 3)
        grb_Bank.Name = "grb_Bank"
        grb_Bank.Padding = New Padding(4, 3, 4, 3)
        grb_Bank.Size = New Size(341, 123)
        grb_Bank.TabIndex = 10411
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
        cmb_SaranaPembayaran.Location = New Point(155, 243)
        cmb_SaranaPembayaran.Margin = New Padding(4, 3, 4, 3)
        cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        cmb_SaranaPembayaran.Size = New Size(213, 23)
        cmb_SaranaPembayaran.TabIndex = 10410
        ' 
        ' lbl_SaranaPembayaran
        ' 
        lbl_SaranaPembayaran.AutoSize = True
        lbl_SaranaPembayaran.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_SaranaPembayaran.Location = New Point(24, 247)
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
        txt_NomorBP.TabIndex = 10394
        ' 
        ' btn_PilihNIK
        ' 
        btn_PilihNIK.Location = New Point(275, 118)
        btn_PilihNIK.Margin = New Padding(4, 3, 4, 3)
        btn_PilihNIK.Name = "btn_PilihNIK"
        btn_PilihNIK.Size = New Size(47, 27)
        btn_PilihNIK.TabIndex = 10396
        btn_PilihNIK.Text = "Pilih"
        btn_PilihNIK.UseVisualStyleBackColor = True
        ' 
        ' lbl_SaldoAwal
        ' 
        lbl_SaldoAwal.AutoSize = True
        lbl_SaldoAwal.Location = New Point(24, 213)
        lbl_SaldoAwal.Margin = New Padding(4, 0, 4, 0)
        lbl_SaldoAwal.Name = "lbl_SaldoAwal"
        lbl_SaldoAwal.Size = New Size(65, 15)
        lbl_SaldoAwal.TabIndex = 10408
        lbl_SaldoAwal.Text = "Saldo Awal"
        ' 
        ' txt_SaldoAwal
        ' 
        txt_SaldoAwal.Location = New Point(155, 210)
        txt_SaldoAwal.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoAwal.MaxLength = 99
        txt_SaldoAwal.Name = "txt_SaldoAwal"
        txt_SaldoAwal.Size = New Size(117, 23)
        txt_SaldoAwal.TabIndex = 10400
        txt_SaldoAwal.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(24, 183)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(98, 15)
        Label1.TabIndex = 10407
        Label1.Text = "Jumlah Pinjaman"
        ' 
        ' txt_JumlahPinjaman
        ' 
        txt_JumlahPinjaman.Location = New Point(155, 180)
        txt_JumlahPinjaman.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahPinjaman.MaxLength = 99
        txt_JumlahPinjaman.Name = "txt_JumlahPinjaman"
        txt_JumlahPinjaman.Size = New Size(117, 23)
        txt_JumlahPinjaman.TabIndex = 10399
        txt_JumlahPinjaman.TextAlign = HorizontalAlignment.Right
        ' 
        ' txt_NamaPemegangSaham
        ' 
        txt_NamaPemegangSaham.Enabled = False
        txt_NamaPemegangSaham.Location = New Point(155, 150)
        txt_NamaPemegangSaham.Margin = New Padding(4, 3, 4, 3)
        txt_NamaPemegangSaham.MaxLength = 99
        txt_NamaPemegangSaham.Name = "txt_NamaPemegangSaham"
        txt_NamaPemegangSaham.Size = New Size(213, 23)
        txt_NamaPemegangSaham.TabIndex = 10397
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(24, 153)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(39, 15)
        Label3.TabIndex = 10405
        Label3.Text = "Nama"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(24, 123)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(26, 15)
        Label2.TabIndex = 10404
        Label2.Text = "NIK"
        ' 
        ' txt_NIK
        ' 
        txt_NIK.Location = New Point(155, 120)
        txt_NIK.Margin = New Padding(4, 3, 4, 3)
        txt_NIK.MaxLength = 99
        txt_NIK.Name = "txt_NIK"
        txt_NIK.Size = New Size(117, 23)
        txt_NIK.TabIndex = 10395
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(24, 455)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(73, 15)
        Label5.TabIndex = 10403
        Label5.Text = "Keterangan :"
        ' 
        ' txt_Keterangan
        ' 
        txt_Keterangan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_Keterangan.Location = New Point(28, 477)
        txt_Keterangan.Margin = New Padding(4, 3, 4, 3)
        txt_Keterangan.Name = "txt_Keterangan"
        txt_Keterangan.Size = New Size(340, 96)
        txt_Keterangan.TabIndex = 10401
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
        dtp_TanggalPinjam.TabIndex = 10393
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
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(24, 93)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(100, 15)
        Label6.TabIndex = 10414
        Label6.Text = "Nomor Dokumen"
        ' 
        ' txt_NomorDokumen
        ' 
        txt_NomorDokumen.Location = New Point(155, 90)
        txt_NomorDokumen.Margin = New Padding(4, 3, 4, 3)
        txt_NomorDokumen.MaxLength = 99
        txt_NomorDokumen.Name = "txt_NomorDokumen"
        txt_NomorDokumen.Size = New Size(213, 23)
        txt_NomorDokumen.TabIndex = 10413
        ' 
        ' frm_InputHutangPiutangPemegangSaham
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(401, 667)
        Controls.Add(Label6)
        Controls.Add(txt_NomorDokumen)
        Controls.Add(grb_Bank)
        Controls.Add(cmb_SaranaPembayaran)
        Controls.Add(lbl_SaranaPembayaran)
        Controls.Add(lbl_NomorBP)
        Controls.Add(txt_NomorBP)
        Controls.Add(btn_PilihNIK)
        Controls.Add(lbl_SaldoAwal)
        Controls.Add(txt_SaldoAwal)
        Controls.Add(Label1)
        Controls.Add(txt_JumlahPinjaman)
        Controls.Add(txt_NamaPemegangSaham)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(txt_NIK)
        Controls.Add(Label5)
        Controls.Add(txt_Keterangan)
        Controls.Add(dtp_TanggalPinjam)
        Controls.Add(Label4)
        Controls.Add(btn_Simpan)
        Controls.Add(btn_Batal)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_InputHutangPiutangPemegangSaham"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input Hutang Pemegang Saham"
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
    Friend WithEvents btn_PilihNIK As Button
    Friend WithEvents lbl_SaldoAwal As Label
    Friend WithEvents txt_SaldoAwal As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_JumlahPinjaman As TextBox
    Friend WithEvents txt_NamaPemegangSaham As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NIK As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents dtp_TanggalPinjam As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_NomorDokumen As TextBox
End Class
