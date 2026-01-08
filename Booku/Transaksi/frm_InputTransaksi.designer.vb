<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputTransaksi
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
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_UraianTransaksi = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_NamaLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_KodeLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_NomorFakturPajak = New System.Windows.Forms.TextBox()
        Me.lbl_NomorFakturPajak = New System.Windows.Forms.Label()
        Me.lbl_NomorInvoice = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_NamaAkun = New System.Windows.Forms.TextBox()
        Me.lbl_NamaAkun = New System.Windows.Forms.Label()
        Me.txt_JumlahTransaksi = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lbl_COA = New System.Windows.Forms.Label()
        Me.btn_Reset = New System.Windows.Forms.Button()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Tutup = New System.Windows.Forms.Button()
        Me.cmb_JenisTransaksi = New System.Windows.Forms.ComboBox()
        Me.txt_Referensi = New System.Windows.Forms.TextBox()
        Me.txt_NomorInvoice = New System.Windows.Forms.TextBox()
        Me.btn_PilihReferensi = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dtp_TanggalTransaksi = New System.Windows.Forms.DateTimePicker()
        Me.txt_COA = New System.Windows.Forms.TextBox()
        Me.cmb_SaranaPembayaran = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbl_JudulForm = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_TanggalInvoice = New System.Windows.Forms.TextBox()
        Me.cmb_DitanggungOleh = New System.Windows.Forms.ComboBox()
        Me.grb_Bank = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txt_TotalBank = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahTransfer = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_BiayaAdministrasiBank = New System.Windows.Forms.TextBox()
        Me.txt_JumlahTransfer = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmb_JenisJurnal = New System.Windows.Forms.ComboBox()
        Me.grb_Bank.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(25, 64)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 10014
        Me.Label10.Text = "Jenis Transaksi"
        '
        'txt_UraianTransaksi
        '
        Me.txt_UraianTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_UraianTransaksi.Location = New System.Drawing.Point(381, 218)
        Me.txt_UraianTransaksi.Multiline = True
        Me.txt_UraianTransaksi.Name = "txt_UraianTransaksi"
        Me.txt_UraianTransaksi.Size = New System.Drawing.Size(261, 71)
        Me.txt_UraianTransaksi.TabIndex = 210
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(379, 199)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 10016
        Me.Label1.Text = "Uraian Transaksi :"
        '
        'txt_NamaLawanTransaksi
        '
        Me.txt_NamaLawanTransaksi.Enabled = False
        Me.txt_NamaLawanTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NamaLawanTransaksi.Location = New System.Drawing.Point(147, 299)
        Me.txt_NamaLawanTransaksi.Name = "txt_NamaLawanTransaksi"
        Me.txt_NamaLawanTransaksi.Size = New System.Drawing.Size(195, 20)
        Me.txt_NamaLawanTransaksi.TabIndex = 120
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 302)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 13)
        Me.Label2.TabIndex = 10018
        Me.Label2.Text = "Nama Lawan Transaksi"
        '
        'txt_KodeLawanTransaksi
        '
        Me.txt_KodeLawanTransaksi.Enabled = False
        Me.txt_KodeLawanTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_KodeLawanTransaksi.Location = New System.Drawing.Point(147, 273)
        Me.txt_KodeLawanTransaksi.Name = "txt_KodeLawanTransaksi"
        Me.txt_KodeLawanTransaksi.Size = New System.Drawing.Size(95, 20)
        Me.txt_KodeLawanTransaksi.TabIndex = 110
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(25, 276)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 13)
        Me.Label3.TabIndex = 10020
        Me.Label3.Text = "Kode Lawan Transaksi"
        '
        'txt_NomorFakturPajak
        '
        Me.txt_NomorFakturPajak.Enabled = False
        Me.txt_NomorFakturPajak.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NomorFakturPajak.Location = New System.Drawing.Point(147, 247)
        Me.txt_NomorFakturPajak.Name = "txt_NomorFakturPajak"
        Me.txt_NomorFakturPajak.Size = New System.Drawing.Size(195, 20)
        Me.txt_NomorFakturPajak.TabIndex = 100
        '
        'lbl_NomorFakturPajak
        '
        Me.lbl_NomorFakturPajak.AutoSize = True
        Me.lbl_NomorFakturPajak.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorFakturPajak.Location = New System.Drawing.Point(25, 250)
        Me.lbl_NomorFakturPajak.Name = "lbl_NomorFakturPajak"
        Me.lbl_NomorFakturPajak.Size = New System.Drawing.Size(101, 13)
        Me.lbl_NomorFakturPajak.TabIndex = 10022
        Me.lbl_NomorFakturPajak.Text = "Nomor Faktur Pajak"
        '
        'lbl_NomorInvoice
        '
        Me.lbl_NomorInvoice.AutoSize = True
        Me.lbl_NomorInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorInvoice.Location = New System.Drawing.Point(24, 224)
        Me.lbl_NomorInvoice.Name = "lbl_NomorInvoice"
        Me.lbl_NomorInvoice.Size = New System.Drawing.Size(104, 13)
        Me.lbl_NomorInvoice.TabIndex = 10024
        Me.lbl_NomorInvoice.Text = "Nomor Invoice/Nota"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(25, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 10026
        Me.Label6.Text = "Referensi"
        '
        'txt_NamaAkun
        '
        Me.txt_NamaAkun.Enabled = False
        Me.txt_NamaAkun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NamaAkun.Location = New System.Drawing.Point(147, 169)
        Me.txt_NamaAkun.Name = "txt_NamaAkun"
        Me.txt_NamaAkun.Size = New System.Drawing.Size(195, 20)
        Me.txt_NamaAkun.TabIndex = 70
        '
        'lbl_NamaAkun
        '
        Me.lbl_NamaAkun.AutoSize = True
        Me.lbl_NamaAkun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaAkun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl_NamaAkun.Location = New System.Drawing.Point(25, 172)
        Me.lbl_NamaAkun.Name = "lbl_NamaAkun"
        Me.lbl_NamaAkun.Size = New System.Drawing.Size(63, 13)
        Me.lbl_NamaAkun.TabIndex = 10030
        Me.lbl_NamaAkun.Text = "Nama Akun"
        '
        'txt_JumlahTransaksi
        '
        Me.txt_JumlahTransaksi.Enabled = False
        Me.txt_JumlahTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JumlahTransaksi.Location = New System.Drawing.Point(147, 325)
        Me.txt_JumlahTransaksi.Name = "txt_JumlahTransaksi"
        Me.txt_JumlahTransaksi.Size = New System.Drawing.Size(120, 20)
        Me.txt_JumlahTransaksi.TabIndex = 140
        Me.txt_JumlahTransaksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(25, 328)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 10034
        Me.Label11.Text = "Jumlah Transaksi"
        '
        'lbl_COA
        '
        Me.lbl_COA.AutoSize = True
        Me.lbl_COA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_COA.Location = New System.Drawing.Point(25, 144)
        Me.lbl_COA.Name = "lbl_COA"
        Me.lbl_COA.Size = New System.Drawing.Size(60, 13)
        Me.lbl_COA.TabIndex = 10037
        Me.lbl_COA.Text = "Kode Akun"
        '
        'btn_Reset
        '
        Me.btn_Reset.Location = New System.Drawing.Point(470, 343)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(83, 35)
        Me.btn_Reset.TabIndex = 9920
        Me.btn_Reset.Text = "Reset"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(559, 343)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9900
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Location = New System.Drawing.Point(382, 343)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tutup.TabIndex = 9930
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'cmb_JenisTransaksi
        '
        Me.cmb_JenisTransaksi.FormattingEnabled = True
        Me.cmb_JenisTransaksi.Location = New System.Drawing.Point(147, 61)
        Me.cmb_JenisTransaksi.Name = "cmb_JenisTransaksi"
        Me.cmb_JenisTransaksi.Size = New System.Drawing.Size(196, 21)
        Me.cmb_JenisTransaksi.TabIndex = 10
        '
        'txt_Referensi
        '
        Me.txt_Referensi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Referensi.Location = New System.Drawing.Point(147, 88)
        Me.txt_Referensi.Name = "txt_Referensi"
        Me.txt_Referensi.Size = New System.Drawing.Size(118, 20)
        Me.txt_Referensi.TabIndex = 20
        '
        'txt_NomorInvoice
        '
        Me.txt_NomorInvoice.Enabled = False
        Me.txt_NomorInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NomorInvoice.Location = New System.Drawing.Point(147, 221)
        Me.txt_NomorInvoice.Name = "txt_NomorInvoice"
        Me.txt_NomorInvoice.Size = New System.Drawing.Size(195, 20)
        Me.txt_NomorInvoice.TabIndex = 90
        '
        'btn_PilihReferensi
        '
        Me.btn_PilihReferensi.Location = New System.Drawing.Point(271, 86)
        Me.btn_PilihReferensi.Name = "btn_PilihReferensi"
        Me.btn_PilihReferensi.Size = New System.Drawing.Size(72, 23)
        Me.btn_PilihReferensi.TabIndex = 30
        Me.btn_PilihReferensi.Text = "Pilih"
        Me.btn_PilihReferensi.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(25, 354)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(95, 13)
        Me.Label13.TabIndex = 10040
        Me.Label13.Text = "Tanggal Transaksi"
        '
        'dtp_TanggalTransaksi
        '
        Me.dtp_TanggalTransaksi.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalTransaksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalTransaksi.Location = New System.Drawing.Point(147, 351)
        Me.dtp_TanggalTransaksi.Name = "dtp_TanggalTransaksi"
        Me.dtp_TanggalTransaksi.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalTransaksi.TabIndex = 150
        '
        'txt_COA
        '
        Me.txt_COA.Enabled = False
        Me.txt_COA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_COA.Location = New System.Drawing.Point(147, 141)
        Me.txt_COA.Name = "txt_COA"
        Me.txt_COA.Size = New System.Drawing.Size(95, 20)
        Me.txt_COA.TabIndex = 50
        '
        'cmb_SaranaPembayaran
        '
        Me.cmb_SaranaPembayaran.FormattingEnabled = True
        Me.cmb_SaranaPembayaran.Location = New System.Drawing.Point(147, 114)
        Me.cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        Me.cmb_SaranaPembayaran.Size = New System.Drawing.Size(195, 21)
        Me.cmb_SaranaPembayaran.TabIndex = 40
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(25, 117)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 13)
        Me.Label7.TabIndex = 10045
        Me.Label7.Text = "Sarana Pembayaran"
        '
        'lbl_JudulForm
        '
        Me.lbl_JudulForm.AutoSize = True
        Me.lbl_JudulForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JudulForm.Location = New System.Drawing.Point(24, 22)
        Me.lbl_JudulForm.Name = "lbl_JudulForm"
        Me.lbl_JudulForm.Size = New System.Drawing.Size(77, 20)
        Me.lbl_JudulForm.TabIndex = 10046
        Me.lbl_JudulForm.Text = "Transaksi"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(25, 198)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 13)
        Me.Label9.TabIndex = 10048
        Me.Label9.Text = "Tanggal Invoice"
        '
        'txt_TanggalInvoice
        '
        Me.txt_TanggalInvoice.Enabled = False
        Me.txt_TanggalInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TanggalInvoice.Location = New System.Drawing.Point(147, 195)
        Me.txt_TanggalInvoice.Name = "txt_TanggalInvoice"
        Me.txt_TanggalInvoice.Size = New System.Drawing.Size(195, 20)
        Me.txt_TanggalInvoice.TabIndex = 80
        '
        'cmb_DitanggungOleh
        '
        Me.cmb_DitanggungOleh.Enabled = False
        Me.cmb_DitanggungOleh.FormattingEnabled = True
        Me.cmb_DitanggungOleh.Location = New System.Drawing.Point(119, 45)
        Me.cmb_DitanggungOleh.Name = "cmb_DitanggungOleh"
        Me.cmb_DitanggungOleh.Size = New System.Drawing.Size(120, 21)
        Me.cmb_DitanggungOleh.TabIndex = 180
        '
        'grb_Bank
        '
        Me.grb_Bank.Controls.Add(Me.Label16)
        Me.grb_Bank.Controls.Add(Me.txt_TotalBank)
        Me.grb_Bank.Controls.Add(Me.lbl_JumlahTransfer)
        Me.grb_Bank.Controls.Add(Me.Label14)
        Me.grb_Bank.Controls.Add(Me.Label12)
        Me.grb_Bank.Controls.Add(Me.txt_BiayaAdministrasiBank)
        Me.grb_Bank.Controls.Add(Me.txt_JumlahTransfer)
        Me.grb_Bank.Controls.Add(Me.cmb_DitanggungOleh)
        Me.grb_Bank.Enabled = False
        Me.grb_Bank.Location = New System.Drawing.Point(381, 56)
        Me.grb_Bank.Name = "grb_Bank"
        Me.grb_Bank.Size = New System.Drawing.Size(261, 133)
        Me.grb_Bank.TabIndex = 160
        Me.grb_Bank.TabStop = False
        Me.grb_Bank.Text = "Bank :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(19, 101)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(31, 13)
        Me.Label16.TabIndex = 10056
        Me.Label16.Text = "Total"
        '
        'txt_TotalBank
        '
        Me.txt_TotalBank.Enabled = False
        Me.txt_TotalBank.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_TotalBank.Location = New System.Drawing.Point(119, 98)
        Me.txt_TotalBank.Name = "txt_TotalBank"
        Me.txt_TotalBank.Size = New System.Drawing.Size(120, 20)
        Me.txt_TotalBank.TabIndex = 200
        Me.txt_TotalBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahTransfer
        '
        Me.lbl_JumlahTransfer.AutoSize = True
        Me.lbl_JumlahTransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JumlahTransfer.Location = New System.Drawing.Point(20, 75)
        Me.lbl_JumlahTransfer.Name = "lbl_JumlahTransfer"
        Me.lbl_JumlahTransfer.Size = New System.Drawing.Size(82, 13)
        Me.lbl_JumlahTransfer.TabIndex = 10054
        Me.lbl_JumlahTransfer.Text = "Jumlah Transfer"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(20, 48)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(87, 13)
        Me.Label14.TabIndex = 10053
        Me.Label14.Text = "Ditanggung Oleh"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(20, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 13)
        Me.Label12.TabIndex = 10051
        Me.Label12.Text = "Biaya Administrasi"
        '
        'txt_BiayaAdministrasiBank
        '
        Me.txt_BiayaAdministrasiBank.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BiayaAdministrasiBank.Location = New System.Drawing.Point(119, 19)
        Me.txt_BiayaAdministrasiBank.Name = "txt_BiayaAdministrasiBank"
        Me.txt_BiayaAdministrasiBank.Size = New System.Drawing.Size(120, 20)
        Me.txt_BiayaAdministrasiBank.TabIndex = 170
        Me.txt_BiayaAdministrasiBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_JumlahTransfer
        '
        Me.txt_JumlahTransfer.Enabled = False
        Me.txt_JumlahTransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JumlahTransfer.Location = New System.Drawing.Point(119, 72)
        Me.txt_JumlahTransfer.Name = "txt_JumlahTransfer"
        Me.txt_JumlahTransfer.Size = New System.Drawing.Size(120, 20)
        Me.txt_JumlahTransfer.TabIndex = 190
        Me.txt_JumlahTransfer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(379, 304)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 10058
        Me.Label4.Text = "Jenis Jurnal"
        '
        'cmb_JenisJurnal
        '
        Me.cmb_JenisJurnal.FormattingEnabled = True
        Me.cmb_JenisJurnal.Location = New System.Drawing.Point(447, 301)
        Me.cmb_JenisJurnal.Name = "cmb_JenisJurnal"
        Me.cmb_JenisJurnal.Size = New System.Drawing.Size(195, 21)
        Me.cmb_JenisJurnal.TabIndex = 220
        '
        'frm_InputTransaksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 402)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.grb_Bank)
        Me.Controls.Add(Me.cmb_JenisJurnal)
        Me.Controls.Add(Me.txt_TanggalInvoice)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lbl_JudulForm)
        Me.Controls.Add(Me.cmb_SaranaPembayaran)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_COA)
        Me.Controls.Add(Me.dtp_TanggalTransaksi)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btn_PilihReferensi)
        Me.Controls.Add(Me.txt_NomorInvoice)
        Me.Controls.Add(Me.txt_Referensi)
        Me.Controls.Add(Me.cmb_JenisTransaksi)
        Me.Controls.Add(Me.btn_Reset)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Tutup)
        Me.Controls.Add(Me.lbl_COA)
        Me.Controls.Add(Me.txt_JumlahTransaksi)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt_NamaAkun)
        Me.Controls.Add(Me.lbl_NamaAkun)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lbl_NomorInvoice)
        Me.Controls.Add(Me.txt_NomorFakturPajak)
        Me.Controls.Add(Me.lbl_NomorFakturPajak)
        Me.Controls.Add(Me.txt_KodeLawanTransaksi)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_NamaLawanTransaksi)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_UraianTransaksi)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label10)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputTransaksi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Transaksi"
        Me.grb_Bank.ResumeLayout(False)
        Me.grb_Bank.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_UraianTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_NamaLawanTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_KodeLawanTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_NomorFakturPajak As System.Windows.Forms.TextBox
    Friend WithEvents lbl_NomorFakturPajak As System.Windows.Forms.Label
    Friend WithEvents lbl_NomorInvoice As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_NamaAkun As System.Windows.Forms.TextBox
    Friend WithEvents lbl_NamaAkun As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lbl_COA As System.Windows.Forms.Label
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents cmb_JenisTransaksi As System.Windows.Forms.ComboBox
    Friend WithEvents txt_Referensi As System.Windows.Forms.TextBox
    Friend WithEvents txt_NomorInvoice As System.Windows.Forms.TextBox
    Friend WithEvents btn_PilihReferensi As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtp_TanggalTransaksi As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_COA As System.Windows.Forms.TextBox
    Friend WithEvents cmb_SaranaPembayaran As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_TanggalInvoice As System.Windows.Forms.TextBox
    Friend WithEvents cmb_DitanggungOleh As System.Windows.Forms.ComboBox
    Friend WithEvents grb_Bank As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_JumlahTransfer As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txt_BiayaAdministrasiBank As System.Windows.Forms.TextBox
    Friend WithEvents txt_JumlahTransfer As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txt_TotalBank As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmb_JenisJurnal As System.Windows.Forms.ComboBox
End Class
