<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Input_InvoicePenjualan
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_Diskon_Persen = New System.Windows.Forms.TextBox()
        Me.txt_AlamatCustomer = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btn_Pratinjau = New System.Windows.Forms.Button()
        Me.grb_Produk = New System.Windows.Forms.GroupBox()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.btn_Tambah = New System.Windows.Forms.Button()
        Me.txt_TotalTagihan_Kotor = New System.Windows.Forms.TextBox()
        Me.lbl_PPN = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_PPN = New System.Windows.Forms.TextBox()
        Me.txt_DasarPengenaanPajak = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_Diskon_Rp = New System.Windows.Forms.TextBox()
        Me.lbl_Diskon = New System.Windows.Forms.Label()
        Me.txt_JumlahHargaKeseluruhan = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Catatan = New System.Windows.Forms.RichTextBox()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Cetak = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.dtp_TanggalInvoice = New System.Windows.Forms.DateTimePicker()
        Me.lbl_TanggalInvoice = New System.Windows.Forms.Label()
        Me.txt_NomorInvoice = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.txt_KodeCustomer = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txt_NamaCustomer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtp_TanggalJatuhTempo = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btn_SingkirkanSJBAST = New System.Windows.Forms.Button()
        Me.btn_TambahSJBAST = New System.Windows.Forms.Button()
        Me.dgv_SJBAST = New System.Windows.Forms.DataGridView()
        Me.Nomor_SJ_BAST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_SJ_BAST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Diterima = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Biaya_Transportasi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_SJBAST = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_JenisInvoice = New System.Windows.Forms.ComboBox()
        Me.txt_PPhDitanggung = New System.Windows.Forms.TextBox()
        Me.lbl_PPhDitanggung = New System.Windows.Forms.Label()
        Me.txt_PPhDipotong = New System.Windows.Forms.TextBox()
        Me.lbl_PPhDipotong = New System.Windows.Forms.Label()
        Me.lbl_PPh = New System.Windows.Forms.Label()
        Me.lbl_PersenPPh = New System.Windows.Forms.Label()
        Me.txt_TarifPPh = New System.Windows.Forms.TextBox()
        Me.cmb_JenisPPh = New System.Windows.Forms.ComboBox()
        Me.txt_PPhTerutang = New System.Windows.Forms.TextBox()
        Me.txt_TotalTagihan = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_DPPJasa = New System.Windows.Forms.TextBox()
        Me.lbl_DPPJasa = New System.Windows.Forms.Label()
        Me.txt_DPPBarang = New System.Windows.Forms.TextBox()
        Me.lbl_DPPBarang = New System.Windows.Forms.Label()
        Me.lbl_PerlakuanPPN = New System.Windows.Forms.Label()
        Me.cmb_PerlakuanPPN = New System.Windows.Forms.ComboBox()
        Me.lbl_JenisPPN = New System.Windows.Forms.Label()
        Me.cmb_JenisPPN = New System.Windows.Forms.ComboBox()
        Me.txt_JumlahHariJatuhTempo = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahHariJatuhTempo = New System.Windows.Forms.Label()
        Me.rdb_JumlahHariJatuhTempo = New System.Windows.Forms.RadioButton()
        Me.rdb_TanggalJatuhTempo = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_BiayaTransportasiPenjualan = New System.Windows.Forms.TextBox()
        Me.txt_Referensi = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.grb_Bank = New System.Windows.Forms.GroupBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txt_BiayaAdministrasiBank = New System.Windows.Forms.TextBox()
        Me.cmb_DitanggungOleh = New System.Windows.Forms.ComboBox()
        Me.txt_JumlahTransfer = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmb_SaranaPembayaran = New System.Windows.Forms.ComboBox()
        Me.lbl_SaranaPembayaran = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_TarifPPN = New System.Windows.Forms.TextBox()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_Produk_Per_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_SJ_BAST_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_SJ_BAST_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Diterima_SJ_BAST_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_PO_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Deskripsi_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Satuan_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Harga_Satuan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Harga_Per_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diskon_Per_Item_Persen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diskon_Per_Item_Rp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_Harga = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Project_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grb_Produk.SuspendLayout()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_SJBAST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grb_Bank.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(898, 413)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(15, 13)
        Me.Label14.TabIndex = 10222
        Me.Label14.Text = "%"
        Me.Label14.Visible = False
        '
        'txt_Diskon_Persen
        '
        Me.txt_Diskon_Persen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Diskon_Persen.Location = New System.Drawing.Point(866, 410)
        Me.txt_Diskon_Persen.MaxLength = 5
        Me.txt_Diskon_Persen.Name = "txt_Diskon_Persen"
        Me.txt_Diskon_Persen.Size = New System.Drawing.Size(33, 20)
        Me.txt_Diskon_Persen.TabIndex = 10200
        Me.txt_Diskon_Persen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_Diskon_Persen.Visible = False
        '
        'txt_AlamatCustomer
        '
        Me.txt_AlamatCustomer.Location = New System.Drawing.Point(118, 124)
        Me.txt_AlamatCustomer.Multiline = True
        Me.txt_AlamatCustomer.Name = "txt_AlamatCustomer"
        Me.txt_AlamatCustomer.Size = New System.Drawing.Size(193, 45)
        Me.txt_AlamatCustomer.TabIndex = 60
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 127)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(86, 13)
        Me.Label11.TabIndex = 10220
        Me.Label11.Text = "Alamat Customer"
        '
        'btn_Pratinjau
        '
        Me.btn_Pratinjau.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pratinjau.Location = New System.Drawing.Point(16, 619)
        Me.btn_Pratinjau.Name = "btn_Pratinjau"
        Me.btn_Pratinjau.Size = New System.Drawing.Size(92, 46)
        Me.btn_Pratinjau.TabIndex = 10219
        Me.btn_Pratinjau.Text = "Pratinjau"
        Me.btn_Pratinjau.UseVisualStyleBackColor = True
        '
        'grb_Produk
        '
        Me.grb_Produk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grb_Produk.Controls.Add(Me.btn_Hapus)
        Me.grb_Produk.Controls.Add(Me.btn_Edit)
        Me.grb_Produk.Controls.Add(Me.btn_Tambah)
        Me.grb_Produk.Location = New System.Drawing.Point(772, 122)
        Me.grb_Produk.Name = "grb_Produk"
        Me.grb_Produk.Size = New System.Drawing.Size(231, 55)
        Me.grb_Produk.TabIndex = 10196
        Me.grb_Produk.TabStop = False
        Me.grb_Produk.Text = "Barang/Jasa :"
        Me.grb_Produk.Visible = False
        '
        'btn_Hapus
        '
        Me.btn_Hapus.Location = New System.Drawing.Point(154, 17)
        Me.btn_Hapus.Name = "btn_Hapus"
        Me.btn_Hapus.Size = New System.Drawing.Size(68, 30)
        Me.btn_Hapus.TabIndex = 110
        Me.btn_Hapus.Text = "Hapus"
        Me.btn_Hapus.UseVisualStyleBackColor = True
        '
        'btn_Edit
        '
        Me.btn_Edit.Location = New System.Drawing.Point(80, 17)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(68, 30)
        Me.btn_Edit.TabIndex = 100
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'btn_Tambah
        '
        Me.btn_Tambah.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_Tambah.Location = New System.Drawing.Point(6, 17)
        Me.btn_Tambah.Name = "btn_Tambah"
        Me.btn_Tambah.Size = New System.Drawing.Size(68, 30)
        Me.btn_Tambah.TabIndex = 95
        Me.btn_Tambah.Text = "Tambah"
        Me.btn_Tambah.UseVisualStyleBackColor = True
        '
        'txt_TotalTagihan_Kotor
        '
        Me.txt_TotalTagihan_Kotor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TotalTagihan_Kotor.Location = New System.Drawing.Point(914, 488)
        Me.txt_TotalTagihan_Kotor.Name = "txt_TotalTagihan_Kotor"
        Me.txt_TotalTagihan_Kotor.Size = New System.Drawing.Size(92, 20)
        Me.txt_TotalTagihan_Kotor.TabIndex = 10204
        Me.txt_TotalTagihan_Kotor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PPN
        '
        Me.lbl_PPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PPN.AutoSize = True
        Me.lbl_PPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPN.Location = New System.Drawing.Point(739, 465)
        Me.lbl_PPN.Name = "lbl_PPN"
        Me.lbl_PPN.Size = New System.Drawing.Size(32, 13)
        Me.lbl_PPN.TabIndex = 10217
        Me.lbl_PPN.Text = "PPN "
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(739, 491)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 13)
        Me.Label10.TabIndex = 10218
        Me.Label10.Text = "Total Tagihan Kotor"
        '
        'txt_PPN
        '
        Me.txt_PPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPN.Location = New System.Drawing.Point(914, 462)
        Me.txt_PPN.Name = "txt_PPN"
        Me.txt_PPN.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPN.TabIndex = 10203
        Me.txt_PPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_DasarPengenaanPajak
        '
        Me.txt_DasarPengenaanPajak.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DasarPengenaanPajak.Location = New System.Drawing.Point(914, 436)
        Me.txt_DasarPengenaanPajak.Name = "txt_DasarPengenaanPajak"
        Me.txt_DasarPengenaanPajak.Size = New System.Drawing.Size(92, 20)
        Me.txt_DasarPengenaanPajak.TabIndex = 10202
        Me.txt_DasarPengenaanPajak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(739, 439)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(123, 13)
        Me.Label9.TabIndex = 10216
        Me.Label9.Text = "Dasar Pengenaan Pajak"
        '
        'txt_Diskon_Rp
        '
        Me.txt_Diskon_Rp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Diskon_Rp.Location = New System.Drawing.Point(914, 410)
        Me.txt_Diskon_Rp.Name = "txt_Diskon_Rp"
        Me.txt_Diskon_Rp.Size = New System.Drawing.Size(92, 20)
        Me.txt_Diskon_Rp.TabIndex = 10201
        Me.txt_Diskon_Rp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Diskon
        '
        Me.lbl_Diskon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Diskon.AutoSize = True
        Me.lbl_Diskon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diskon.Location = New System.Drawing.Point(739, 413)
        Me.lbl_Diskon.Name = "lbl_Diskon"
        Me.lbl_Diskon.Size = New System.Drawing.Size(49, 13)
        Me.lbl_Diskon.TabIndex = 10215
        Me.lbl_Diskon.Text = "Discount"
        '
        'txt_JumlahHargaKeseluruhan
        '
        Me.txt_JumlahHargaKeseluruhan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_JumlahHargaKeseluruhan.Location = New System.Drawing.Point(914, 384)
        Me.txt_JumlahHargaKeseluruhan.Name = "txt_JumlahHargaKeseluruhan"
        Me.txt_JumlahHargaKeseluruhan.Size = New System.Drawing.Size(92, 20)
        Me.txt_JumlahHargaKeseluruhan.TabIndex = 10199
        Me.txt_JumlahHargaKeseluruhan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(739, 387)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 10214
        Me.Label6.Text = "Jumlah"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 386)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 10213
        Me.Label5.Text = "Catatan :"
        '
        'txt_Catatan
        '
        Me.txt_Catatan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Catatan.Location = New System.Drawing.Point(16, 407)
        Me.txt_Catatan.Name = "txt_Catatan"
        Me.txt_Catatan.Size = New System.Drawing.Size(242, 137)
        Me.txt_Catatan.TabIndex = 10198
        Me.txt_Catatan.Text = ""
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(923, 630)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 10205
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Cetak
        '
        Me.btn_Cetak.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cetak.Location = New System.Drawing.Point(114, 619)
        Me.btn_Cetak.Name = "btn_Cetak"
        Me.btn_Cetak.Size = New System.Drawing.Size(92, 46)
        Me.btn_Cetak.TabIndex = 10207
        Me.btn_Cetak.Text = "Cetak"
        Me.btn_Cetak.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(834, 630)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10206
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'dtp_TanggalInvoice
        '
        Me.dtp_TanggalInvoice.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalInvoice.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalInvoice.Location = New System.Drawing.Point(459, 19)
        Me.dtp_TanggalInvoice.Name = "dtp_TanggalInvoice"
        Me.dtp_TanggalInvoice.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalInvoice.TabIndex = 70
        '
        'lbl_TanggalInvoice
        '
        Me.lbl_TanggalInvoice.AutoSize = True
        Me.lbl_TanggalInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TanggalInvoice.Location = New System.Drawing.Point(343, 22)
        Me.lbl_TanggalInvoice.Name = "lbl_TanggalInvoice"
        Me.lbl_TanggalInvoice.Size = New System.Drawing.Size(84, 13)
        Me.lbl_TanggalInvoice.TabIndex = 10211
        Me.lbl_TanggalInvoice.Text = "Tanggal Invoice"
        '
        'txt_NomorInvoice
        '
        Me.txt_NomorInvoice.Location = New System.Drawing.Point(118, 19)
        Me.txt_NomorInvoice.Name = "txt_NomorInvoice"
        Me.txt_NomorInvoice.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorInvoice.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 10210
        Me.Label1.Text = "Nomor Invoice"
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Jenis_Produk_Per_Item, Me.Nomor_SJ_BAST_Produk, Me.Tanggal_SJ_BAST_Produk, Me.Tanggal_Diterima_SJ_BAST_Produk, Me.Nomor_PO_Produk, Me.Nama_Produk, Me.Deskripsi_Produk, Me.Jumlah_Produk, Me.Satuan_Produk, Me.Harga_Satuan, Me.Jumlah_Harga_Per_Item, Me.Diskon_Per_Item_Persen, Me.Diskon_Per_Item_Rp, Me.Total_Harga, Me.Kode_Project_Produk})
        Me.DataTabelUtama.Location = New System.Drawing.Point(18, 183)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(988, 191)
        Me.DataTabelUtama.TabIndex = 10197
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(205, 71)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 40
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'txt_KodeCustomer
        '
        Me.txt_KodeCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_KodeCustomer.Location = New System.Drawing.Point(118, 72)
        Me.txt_KodeCustomer.MaxLength = 3
        Me.txt_KodeCustomer.Name = "txt_KodeCustomer"
        Me.txt_KodeCustomer.Size = New System.Drawing.Size(81, 20)
        Me.txt_KodeCustomer.TabIndex = 30
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(16, 75)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(79, 13)
        Me.Label24.TabIndex = 10208
        Me.Label24.Text = "Kode Customer"
        '
        'txt_NamaCustomer
        '
        Me.txt_NamaCustomer.Location = New System.Drawing.Point(118, 98)
        Me.txt_NamaCustomer.Name = "txt_NamaCustomer"
        Me.txt_NamaCustomer.Size = New System.Drawing.Size(193, 20)
        Me.txt_NamaCustomer.TabIndex = 50
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 10209
        Me.Label2.Text = "Nama Customer"
        '
        'dtp_TanggalJatuhTempo
        '
        Me.dtp_TanggalJatuhTempo.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalJatuhTempo.Enabled = False
        Me.dtp_TanggalJatuhTempo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalJatuhTempo.Location = New System.Drawing.Point(459, 74)
        Me.dtp_TanggalJatuhTempo.Name = "dtp_TanggalJatuhTempo"
        Me.dtp_TanggalJatuhTempo.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalJatuhTempo.TabIndex = 110
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(343, 51)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(69, 13)
        Me.Label15.TabIndex = 10226
        Me.Label15.Text = "Jatuh Tempo"
        '
        'btn_SingkirkanSJBAST
        '
        Me.btn_SingkirkanSJBAST.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_SingkirkanSJBAST.Enabled = False
        Me.btn_SingkirkanSJBAST.Location = New System.Drawing.Point(588, 380)
        Me.btn_SingkirkanSJBAST.Name = "btn_SingkirkanSJBAST"
        Me.btn_SingkirkanSJBAST.Size = New System.Drawing.Size(24, 24)
        Me.btn_SingkirkanSJBAST.TabIndex = 10279
        Me.btn_SingkirkanSJBAST.Text = "-"
        Me.btn_SingkirkanSJBAST.UseVisualStyleBackColor = True
        '
        'btn_TambahSJBAST
        '
        Me.btn_TambahSJBAST.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_TambahSJBAST.Enabled = False
        Me.btn_TambahSJBAST.Location = New System.Drawing.Point(561, 380)
        Me.btn_TambahSJBAST.Name = "btn_TambahSJBAST"
        Me.btn_TambahSJBAST.Size = New System.Drawing.Size(24, 24)
        Me.btn_TambahSJBAST.TabIndex = 10278
        Me.btn_TambahSJBAST.Text = "+"
        Me.btn_TambahSJBAST.UseVisualStyleBackColor = True
        '
        'dgv_SJBAST
        '
        Me.dgv_SJBAST.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgv_SJBAST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_SJBAST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_SJ_BAST, Me.Tanggal_SJ_BAST, Me.Tanggal_Diterima, Me.Nomor_PO, Me.Biaya_Transportasi})
        Me.dgv_SJBAST.Location = New System.Drawing.Point(273, 407)
        Me.dgv_SJBAST.Name = "dgv_SJBAST"
        Me.dgv_SJBAST.RowHeadersVisible = False
        Me.dgv_SJBAST.Size = New System.Drawing.Size(339, 137)
        Me.dgv_SJBAST.TabIndex = 10277
        '
        'Nomor_SJ_BAST
        '
        Me.Nomor_SJ_BAST.HeaderText = "Nomor SJ/BAST"
        Me.Nomor_SJ_BAST.Name = "Nomor_SJ_BAST"
        Me.Nomor_SJ_BAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_SJ_BAST.Width = 99
        '
        'Tanggal_SJ_BAST
        '
        Me.Tanggal_SJ_BAST.HeaderText = "Tanggal SJ/BAST"
        Me.Tanggal_SJ_BAST.Name = "Tanggal_SJ_BAST"
        Me.Tanggal_SJ_BAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_SJ_BAST.Visible = False
        Me.Tanggal_SJ_BAST.Width = 63
        '
        'Tanggal_Diterima
        '
        Me.Tanggal_Diterima.HeaderText = "Diterima"
        Me.Tanggal_Diterima.Name = "Tanggal_Diterima"
        Me.Tanggal_Diterima.ReadOnly = True
        Me.Tanggal_Diterima.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Diterima.Width = 63
        '
        'Nomor_PO
        '
        Me.Nomor_PO.HeaderText = "PO"
        Me.Nomor_PO.Name = "Nomor_PO"
        Me.Nomor_PO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_PO.Width = 172
        '
        'Biaya_Transportasi
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Me.Biaya_Transportasi.DefaultCellStyle = DataGridViewCellStyle7
        Me.Biaya_Transportasi.HeaderText = "Biaya Transportasi"
        Me.Biaya_Transportasi.Name = "Biaya_Transportasi"
        Me.Biaya_Transportasi.ReadOnly = True
        Me.Biaya_Transportasi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Biaya_Transportasi.Visible = False
        '
        'lbl_SJBAST
        '
        Me.lbl_SJBAST.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_SJBAST.AutoSize = True
        Me.lbl_SJBAST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SJBAST.Location = New System.Drawing.Point(270, 387)
        Me.lbl_SJBAST.Name = "lbl_SJBAST"
        Me.lbl_SJBAST.Size = New System.Drawing.Size(102, 13)
        Me.lbl_SJBAST.TabIndex = 10276
        Me.lbl_SJBAST.Text = "Surat Jalan / BAST:"
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(242, 652)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 13)
        Me.Label18.TabIndex = 10280
        Me.Label18.Text = "Catatan :"
        Me.Label18.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 10282
        Me.Label3.Text = "Jenis Invoice"
        '
        'cmb_JenisInvoice
        '
        Me.cmb_JenisInvoice.FormattingEnabled = True
        Me.cmb_JenisInvoice.Location = New System.Drawing.Point(118, 45)
        Me.cmb_JenisInvoice.Name = "cmb_JenisInvoice"
        Me.cmb_JenisInvoice.Size = New System.Drawing.Size(95, 21)
        Me.cmb_JenisInvoice.TabIndex = 20
        '
        'txt_PPhDitanggung
        '
        Me.txt_PPhDitanggung.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPhDitanggung.Location = New System.Drawing.Point(914, 540)
        Me.txt_PPhDitanggung.Name = "txt_PPhDitanggung"
        Me.txt_PPhDitanggung.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPhDitanggung.TabIndex = 10297
        Me.txt_PPhDitanggung.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PPhDitanggung
        '
        Me.lbl_PPhDitanggung.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PPhDitanggung.AutoSize = True
        Me.lbl_PPhDitanggung.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPhDitanggung.Location = New System.Drawing.Point(739, 543)
        Me.lbl_PPhDitanggung.Name = "lbl_PPhDitanggung"
        Me.lbl_PPhDitanggung.Size = New System.Drawing.Size(85, 13)
        Me.lbl_PPhDitanggung.TabIndex = 10304
        Me.lbl_PPhDitanggung.Text = "PPh Ditanggung"
        '
        'txt_PPhDipotong
        '
        Me.txt_PPhDipotong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPhDipotong.Location = New System.Drawing.Point(914, 566)
        Me.txt_PPhDipotong.Name = "txt_PPhDipotong"
        Me.txt_PPhDipotong.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPhDipotong.TabIndex = 10298
        Me.txt_PPhDipotong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PPhDipotong
        '
        Me.lbl_PPhDipotong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PPhDipotong.AutoSize = True
        Me.lbl_PPhDipotong.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPhDipotong.Location = New System.Drawing.Point(739, 569)
        Me.lbl_PPhDipotong.Name = "lbl_PPhDipotong"
        Me.lbl_PPhDipotong.Size = New System.Drawing.Size(73, 13)
        Me.lbl_PPhDipotong.TabIndex = 10303
        Me.lbl_PPhDipotong.Text = "PPh Dipotong"
        '
        'lbl_PPh
        '
        Me.lbl_PPh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PPh.AutoSize = True
        Me.lbl_PPh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPh.Location = New System.Drawing.Point(739, 517)
        Me.lbl_PPh.Name = "lbl_PPh"
        Me.lbl_PPh.Size = New System.Drawing.Size(27, 13)
        Me.lbl_PPh.TabIndex = 10302
        Me.lbl_PPh.Text = "PPh"
        '
        'lbl_PersenPPh
        '
        Me.lbl_PersenPPh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PersenPPh.AutoSize = True
        Me.lbl_PersenPPh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PersenPPh.Location = New System.Drawing.Point(898, 517)
        Me.lbl_PersenPPh.Name = "lbl_PersenPPh"
        Me.lbl_PersenPPh.Size = New System.Drawing.Size(15, 13)
        Me.lbl_PersenPPh.TabIndex = 10301
        Me.lbl_PersenPPh.Text = "%"
        '
        'txt_TarifPPh
        '
        Me.txt_TarifPPh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TarifPPh.Location = New System.Drawing.Point(866, 514)
        Me.txt_TarifPPh.MaxLength = 5
        Me.txt_TarifPPh.Name = "txt_TarifPPh"
        Me.txt_TarifPPh.Size = New System.Drawing.Size(33, 20)
        Me.txt_TarifPPh.TabIndex = 10295
        Me.txt_TarifPPh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmb_JenisPPh
        '
        Me.cmb_JenisPPh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_JenisPPh.FormattingEnabled = True
        Me.cmb_JenisPPh.Location = New System.Drawing.Point(772, 514)
        Me.cmb_JenisPPh.Name = "cmb_JenisPPh"
        Me.cmb_JenisPPh.Size = New System.Drawing.Size(81, 21)
        Me.cmb_JenisPPh.TabIndex = 10294
        '
        'txt_PPhTerutang
        '
        Me.txt_PPhTerutang.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPhTerutang.Location = New System.Drawing.Point(914, 514)
        Me.txt_PPhTerutang.Name = "txt_PPhTerutang"
        Me.txt_PPhTerutang.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPhTerutang.TabIndex = 10296
        Me.txt_PPhTerutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_TotalTagihan
        '
        Me.txt_TotalTagihan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TotalTagihan.Location = New System.Drawing.Point(914, 592)
        Me.txt_TotalTagihan.Name = "txt_TotalTagihan"
        Me.txt_TotalTagihan.Size = New System.Drawing.Size(92, 20)
        Me.txt_TotalTagihan.TabIndex = 10299
        Me.txt_TotalTagihan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(739, 595)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 10300
        Me.Label4.Text = "Total Tagihan"
        '
        'txt_DPPJasa
        '
        Me.txt_DPPJasa.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DPPJasa.Location = New System.Drawing.Point(575, 588)
        Me.txt_DPPJasa.Name = "txt_DPPJasa"
        Me.txt_DPPJasa.Size = New System.Drawing.Size(92, 20)
        Me.txt_DPPJasa.TabIndex = 10307
        Me.txt_DPPJasa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_DPPJasa
        '
        Me.lbl_DPPJasa.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_DPPJasa.AutoSize = True
        Me.lbl_DPPJasa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DPPJasa.Location = New System.Drawing.Point(492, 591)
        Me.lbl_DPPJasa.Name = "lbl_DPPJasa"
        Me.lbl_DPPJasa.Size = New System.Drawing.Size(54, 13)
        Me.lbl_DPPJasa.TabIndex = 10308
        Me.lbl_DPPJasa.Text = "DPP Jasa"
        '
        'txt_DPPBarang
        '
        Me.txt_DPPBarang.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DPPBarang.Location = New System.Drawing.Point(575, 562)
        Me.txt_DPPBarang.Name = "txt_DPPBarang"
        Me.txt_DPPBarang.Size = New System.Drawing.Size(92, 20)
        Me.txt_DPPBarang.TabIndex = 10305
        Me.txt_DPPBarang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_DPPBarang
        '
        Me.lbl_DPPBarang.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_DPPBarang.AutoSize = True
        Me.lbl_DPPBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DPPBarang.Location = New System.Drawing.Point(492, 565)
        Me.lbl_DPPBarang.Name = "lbl_DPPBarang"
        Me.lbl_DPPBarang.Size = New System.Drawing.Size(66, 13)
        Me.lbl_DPPBarang.TabIndex = 10306
        Me.lbl_DPPBarang.Text = "DPP Barang"
        '
        'lbl_PerlakuanPPN
        '
        Me.lbl_PerlakuanPPN.AutoSize = True
        Me.lbl_PerlakuanPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PerlakuanPPN.Location = New System.Drawing.Point(454, 129)
        Me.lbl_PerlakuanPPN.Name = "lbl_PerlakuanPPN"
        Me.lbl_PerlakuanPPN.Size = New System.Drawing.Size(86, 13)
        Me.lbl_PerlakuanPPN.TabIndex = 10312
        Me.lbl_PerlakuanPPN.Text = "Perlakuan PPN :"
        '
        'cmb_PerlakuanPPN
        '
        Me.cmb_PerlakuanPPN.FormattingEnabled = True
        Me.cmb_PerlakuanPPN.Location = New System.Drawing.Point(457, 148)
        Me.cmb_PerlakuanPPN.Name = "cmb_PerlakuanPPN"
        Me.cmb_PerlakuanPPN.Size = New System.Drawing.Size(97, 21)
        Me.cmb_PerlakuanPPN.TabIndex = 130
        '
        'lbl_JenisPPN
        '
        Me.lbl_JenisPPN.AutoSize = True
        Me.lbl_JenisPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JenisPPN.Location = New System.Drawing.Point(342, 129)
        Me.lbl_JenisPPN.Name = "lbl_JenisPPN"
        Me.lbl_JenisPPN.Size = New System.Drawing.Size(62, 13)
        Me.lbl_JenisPPN.TabIndex = 10310
        Me.lbl_JenisPPN.Text = "Jenis PPN :"
        '
        'cmb_JenisPPN
        '
        Me.cmb_JenisPPN.FormattingEnabled = True
        Me.cmb_JenisPPN.Location = New System.Drawing.Point(345, 148)
        Me.cmb_JenisPPN.Name = "cmb_JenisPPN"
        Me.cmb_JenisPPN.Size = New System.Drawing.Size(92, 21)
        Me.cmb_JenisPPN.TabIndex = 120
        '
        'txt_JumlahHariJatuhTempo
        '
        Me.txt_JumlahHariJatuhTempo.Enabled = False
        Me.txt_JumlahHariJatuhTempo.Location = New System.Drawing.Point(459, 48)
        Me.txt_JumlahHariJatuhTempo.Name = "txt_JumlahHariJatuhTempo"
        Me.txt_JumlahHariJatuhTempo.Size = New System.Drawing.Size(63, 20)
        Me.txt_JumlahHariJatuhTempo.TabIndex = 90
        '
        'lbl_JumlahHariJatuhTempo
        '
        Me.lbl_JumlahHariJatuhTempo.AutoSize = True
        Me.lbl_JumlahHariJatuhTempo.Enabled = False
        Me.lbl_JumlahHariJatuhTempo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JumlahHariJatuhTempo.Location = New System.Drawing.Point(528, 51)
        Me.lbl_JumlahHariJatuhTempo.Name = "lbl_JumlahHariJatuhTempo"
        Me.lbl_JumlahHariJatuhTempo.Size = New System.Drawing.Size(26, 13)
        Me.lbl_JumlahHariJatuhTempo.TabIndex = 10314
        Me.lbl_JumlahHariJatuhTempo.Text = "Hari"
        '
        'rdb_JumlahHariJatuhTempo
        '
        Me.rdb_JumlahHariJatuhTempo.AutoSize = True
        Me.rdb_JumlahHariJatuhTempo.Location = New System.Drawing.Point(440, 51)
        Me.rdb_JumlahHariJatuhTempo.Name = "rdb_JumlahHariJatuhTempo"
        Me.rdb_JumlahHariJatuhTempo.Size = New System.Drawing.Size(14, 13)
        Me.rdb_JumlahHariJatuhTempo.TabIndex = 80
        Me.rdb_JumlahHariJatuhTempo.TabStop = True
        Me.rdb_JumlahHariJatuhTempo.UseVisualStyleBackColor = True
        '
        'rdb_TanggalJatuhTempo
        '
        Me.rdb_TanggalJatuhTempo.AutoSize = True
        Me.rdb_TanggalJatuhTempo.Location = New System.Drawing.Point(440, 77)
        Me.rdb_TanggalJatuhTempo.Name = "rdb_TanggalJatuhTempo"
        Me.rdb_TanggalJatuhTempo.Size = New System.Drawing.Size(14, 13)
        Me.rdb_TanggalJatuhTempo.TabIndex = 100
        Me.rdb_TanggalJatuhTempo.TabStop = True
        Me.rdb_TanggalJatuhTempo.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(492, 641)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 1000000015
        Me.Label7.Text = "Ongkos Kirim"
        '
        'txt_BiayaTransportasiPenjualan
        '
        Me.txt_BiayaTransportasiPenjualan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_BiayaTransportasiPenjualan.Location = New System.Drawing.Point(575, 638)
        Me.txt_BiayaTransportasiPenjualan.Name = "txt_BiayaTransportasiPenjualan"
        Me.txt_BiayaTransportasiPenjualan.Size = New System.Drawing.Size(92, 20)
        Me.txt_BiayaTransportasiPenjualan.TabIndex = 1000000014
        Me.txt_BiayaTransportasiPenjualan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Referensi
        '
        Me.txt_Referensi.Location = New System.Drawing.Point(440, 100)
        Me.txt_Referensi.Name = "txt_Referensi"
        Me.txt_Referensi.Size = New System.Drawing.Size(114, 20)
        Me.txt_Referensi.TabIndex = 1000000016
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(343, 103)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 1000000017
        Me.Label13.Text = "Referensi"
        '
        'grb_Bank
        '
        Me.grb_Bank.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grb_Bank.Controls.Add(Me.Label54)
        Me.grb_Bank.Controls.Add(Me.Label55)
        Me.grb_Bank.Controls.Add(Me.txt_BiayaAdministrasiBank)
        Me.grb_Bank.Controls.Add(Me.cmb_DitanggungOleh)
        Me.grb_Bank.Controls.Add(Me.txt_JumlahTransfer)
        Me.grb_Bank.Controls.Add(Me.Label12)
        Me.grb_Bank.Enabled = False
        Me.grb_Bank.Location = New System.Drawing.Point(772, 7)
        Me.grb_Bank.Name = "grb_Bank"
        Me.grb_Bank.Size = New System.Drawing.Size(231, 107)
        Me.grb_Bank.TabIndex = 1000000019
        Me.grb_Bank.TabStop = False
        Me.grb_Bank.Text = "Bank :"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(10, 50)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(87, 13)
        Me.Label54.TabIndex = 10053
        Me.Label54.Text = "Ditanggung Oleh"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(10, 24)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(91, 13)
        Me.Label55.TabIndex = 10051
        Me.Label55.Text = "Biaya Administrasi"
        '
        'txt_BiayaAdministrasiBank
        '
        Me.txt_BiayaAdministrasiBank.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_BiayaAdministrasiBank.Location = New System.Drawing.Point(114, 21)
        Me.txt_BiayaAdministrasiBank.Name = "txt_BiayaAdministrasiBank"
        Me.txt_BiayaAdministrasiBank.Size = New System.Drawing.Size(104, 20)
        Me.txt_BiayaAdministrasiBank.TabIndex = 490
        Me.txt_BiayaAdministrasiBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmb_DitanggungOleh
        '
        Me.cmb_DitanggungOleh.FormattingEnabled = True
        Me.cmb_DitanggungOleh.Location = New System.Drawing.Point(114, 47)
        Me.cmb_DitanggungOleh.Name = "cmb_DitanggungOleh"
        Me.cmb_DitanggungOleh.Size = New System.Drawing.Size(104, 21)
        Me.cmb_DitanggungOleh.TabIndex = 500
        '
        'txt_JumlahTransfer
        '
        Me.txt_JumlahTransfer.Enabled = False
        Me.txt_JumlahTransfer.Location = New System.Drawing.Point(114, 74)
        Me.txt_JumlahTransfer.Name = "txt_JumlahTransfer"
        Me.txt_JumlahTransfer.Size = New System.Drawing.Size(104, 20)
        Me.txt_JumlahTransfer.TabIndex = 510
        Me.txt_JumlahTransfer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(10, 77)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 13)
        Me.Label12.TabIndex = 10391
        Me.Label12.Text = "Jumlah Transfer"
        '
        'cmb_SaranaPembayaran
        '
        Me.cmb_SaranaPembayaran.Enabled = False
        Me.cmb_SaranaPembayaran.FormattingEnabled = True
        Me.cmb_SaranaPembayaran.Location = New System.Drawing.Point(588, 40)
        Me.cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        Me.cmb_SaranaPembayaran.Size = New System.Drawing.Size(159, 21)
        Me.cmb_SaranaPembayaran.TabIndex = 1000000020
        '
        'lbl_SaranaPembayaran
        '
        Me.lbl_SaranaPembayaran.AutoSize = True
        Me.lbl_SaranaPembayaran.Enabled = False
        Me.lbl_SaranaPembayaran.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SaranaPembayaran.Location = New System.Drawing.Point(585, 21)
        Me.lbl_SaranaPembayaran.Name = "lbl_SaranaPembayaran"
        Me.lbl_SaranaPembayaran.Size = New System.Drawing.Size(109, 13)
        Me.lbl_SaranaPembayaran.TabIndex = 1000000021
        Me.lbl_SaranaPembayaran.Text = "Sarana Pembayaran :"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(898, 465)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 13)
        Me.Label8.TabIndex = 1000000023
        Me.Label8.Text = "%"
        '
        'txt_TarifPPN
        '
        Me.txt_TarifPPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TarifPPN.Location = New System.Drawing.Point(866, 462)
        Me.txt_TarifPPN.MaxLength = 2
        Me.txt_TarifPPN.Name = "txt_TarifPPN"
        Me.txt_TarifPPN.Size = New System.Drawing.Size(33, 20)
        Me.txt_TarifPPN.TabIndex = 1000000022
        Me.txt_TarifPPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Nomor_Urut
        '
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 33
        '
        'Jenis_Produk_Per_Item
        '
        Me.Jenis_Produk_Per_Item.HeaderText = "Jenis Produk"
        Me.Jenis_Produk_Per_Item.Name = "Jenis_Produk_Per_Item"
        Me.Jenis_Produk_Per_Item.ReadOnly = True
        Me.Jenis_Produk_Per_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jenis_Produk_Per_Item.Visible = False
        '
        'Nomor_SJ_BAST_Produk
        '
        Me.Nomor_SJ_BAST_Produk.HeaderText = "Nomor SJ/BAST"
        Me.Nomor_SJ_BAST_Produk.Name = "Nomor_SJ_BAST_Produk"
        Me.Nomor_SJ_BAST_Produk.ReadOnly = True
        Me.Nomor_SJ_BAST_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_SJ_BAST_Produk.Visible = False
        '
        'Tanggal_SJ_BAST_Produk
        '
        Me.Tanggal_SJ_BAST_Produk.HeaderText = "Tanggal SJ/BAST"
        Me.Tanggal_SJ_BAST_Produk.Name = "Tanggal_SJ_BAST_Produk"
        Me.Tanggal_SJ_BAST_Produk.ReadOnly = True
        Me.Tanggal_SJ_BAST_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_SJ_BAST_Produk.Visible = False
        '
        'Tanggal_Diterima_SJ_BAST_Produk
        '
        Me.Tanggal_Diterima_SJ_BAST_Produk.HeaderText = "Tanggal Diterima"
        Me.Tanggal_Diterima_SJ_BAST_Produk.Name = "Tanggal_Diterima_SJ_BAST_Produk"
        Me.Tanggal_Diterima_SJ_BAST_Produk.ReadOnly = True
        Me.Tanggal_Diterima_SJ_BAST_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Diterima_SJ_BAST_Produk.Visible = False
        Me.Tanggal_Diterima_SJ_BAST_Produk.Width = 63
        '
        'Nomor_PO_Produk
        '
        Me.Nomor_PO_Produk.HeaderText = "Nomor PO"
        Me.Nomor_PO_Produk.Name = "Nomor_PO_Produk"
        Me.Nomor_PO_Produk.ReadOnly = True
        Me.Nomor_PO_Produk.Visible = False
        '
        'Nama_Produk
        '
        Me.Nama_Produk.HeaderText = "Nama Barang/Jasa"
        Me.Nama_Produk.Name = "Nama_Produk"
        Me.Nama_Produk.ReadOnly = True
        Me.Nama_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Produk.Width = 210
        '
        'Deskripsi_Produk
        '
        Me.Deskripsi_Produk.HeaderText = "Deskripsi"
        Me.Deskripsi_Produk.Name = "Deskripsi_Produk"
        Me.Deskripsi_Produk.ReadOnly = True
        Me.Deskripsi_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Deskripsi_Produk.Width = 210
        '
        'Jumlah_Produk
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        Me.Jumlah_Produk.DefaultCellStyle = DataGridViewCellStyle1
        Me.Jumlah_Produk.HeaderText = "Jumlah"
        Me.Jumlah_Produk.Name = "Jumlah_Produk"
        Me.Jumlah_Produk.ReadOnly = True
        Me.Jumlah_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Produk.Width = 63
        '
        'Satuan_Produk
        '
        Me.Satuan_Produk.HeaderText = "Satuan"
        Me.Satuan_Produk.Name = "Satuan_Produk"
        Me.Satuan_Produk.ReadOnly = True
        Me.Satuan_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Satuan_Produk.Width = 63
        '
        'Harga_Satuan
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Me.Harga_Satuan.DefaultCellStyle = DataGridViewCellStyle2
        Me.Harga_Satuan.HeaderText = "Harga Satuan"
        Me.Harga_Satuan.Name = "Harga_Satuan"
        Me.Harga_Satuan.ReadOnly = True
        Me.Harga_Satuan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Harga_Satuan.Width = 81
        '
        'Jumlah_Harga_Per_Item
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Me.Jumlah_Harga_Per_Item.DefaultCellStyle = DataGridViewCellStyle3
        Me.Jumlah_Harga_Per_Item.HeaderText = "Jumlah Harga"
        Me.Jumlah_Harga_Per_Item.Name = "Jumlah_Harga_Per_Item"
        Me.Jumlah_Harga_Per_Item.ReadOnly = True
        Me.Jumlah_Harga_Per_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Diskon_Per_Item_Persen
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Diskon_Per_Item_Persen.DefaultCellStyle = DataGridViewCellStyle4
        Me.Diskon_Per_Item_Persen.HeaderText = "Diskon (%)"
        Me.Diskon_Per_Item_Persen.Name = "Diskon_Per_Item_Persen"
        Me.Diskon_Per_Item_Persen.ReadOnly = True
        Me.Diskon_Per_Item_Persen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Diskon_Per_Item_Persen.Width = 45
        '
        'Diskon_Per_Item_Rp
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Me.Diskon_Per_Item_Rp.DefaultCellStyle = DataGridViewCellStyle5
        Me.Diskon_Per_Item_Rp.HeaderText = "Diskon (Rp)"
        Me.Diskon_Per_Item_Rp.Name = "Diskon_Per_Item_Rp"
        Me.Diskon_Per_Item_Rp.ReadOnly = True
        Me.Diskon_Per_Item_Rp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Diskon_Per_Item_Rp.Width = 81
        '
        'Total_Harga
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Me.Total_Harga.DefaultCellStyle = DataGridViewCellStyle6
        Me.Total_Harga.HeaderText = "Total"
        Me.Total_Harga.Name = "Total_Harga"
        Me.Total_Harga.ReadOnly = True
        Me.Total_Harga.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Total_Harga.Width = 81
        '
        'Kode_Project_Produk
        '
        Me.Kode_Project_Produk.HeaderText = "Kode Project"
        Me.Kode_Project_Produk.Name = "Kode_Project_Produk"
        Me.Kode_Project_Produk.ReadOnly = True
        Me.Kode_Project_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Project_Produk.Width = 99
        '
        'frm_Input_InvoicePenjualan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 681)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_TarifPPN)
        Me.Controls.Add(Me.cmb_SaranaPembayaran)
        Me.Controls.Add(Me.lbl_SaranaPembayaran)
        Me.Controls.Add(Me.grb_Bank)
        Me.Controls.Add(Me.txt_Referensi)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_BiayaTransportasiPenjualan)
        Me.Controls.Add(Me.rdb_TanggalJatuhTempo)
        Me.Controls.Add(Me.rdb_JumlahHariJatuhTempo)
        Me.Controls.Add(Me.lbl_JumlahHariJatuhTempo)
        Me.Controls.Add(Me.txt_JumlahHariJatuhTempo)
        Me.Controls.Add(Me.lbl_PerlakuanPPN)
        Me.Controls.Add(Me.cmb_PerlakuanPPN)
        Me.Controls.Add(Me.lbl_JenisPPN)
        Me.Controls.Add(Me.cmb_JenisPPN)
        Me.Controls.Add(Me.txt_DPPJasa)
        Me.Controls.Add(Me.lbl_DPPJasa)
        Me.Controls.Add(Me.txt_DPPBarang)
        Me.Controls.Add(Me.lbl_DPPBarang)
        Me.Controls.Add(Me.txt_PPhDitanggung)
        Me.Controls.Add(Me.lbl_PPhDitanggung)
        Me.Controls.Add(Me.txt_PPhDipotong)
        Me.Controls.Add(Me.lbl_PPhDipotong)
        Me.Controls.Add(Me.lbl_PPh)
        Me.Controls.Add(Me.lbl_PersenPPh)
        Me.Controls.Add(Me.txt_TarifPPh)
        Me.Controls.Add(Me.cmb_JenisPPh)
        Me.Controls.Add(Me.txt_PPhTerutang)
        Me.Controls.Add(Me.txt_TotalTagihan)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmb_JenisInvoice)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.btn_SingkirkanSJBAST)
        Me.Controls.Add(Me.btn_TambahSJBAST)
        Me.Controls.Add(Me.dgv_SJBAST)
        Me.Controls.Add(Me.lbl_SJBAST)
        Me.Controls.Add(Me.dtp_TanggalJatuhTempo)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txt_Diskon_Persen)
        Me.Controls.Add(Me.txt_AlamatCustomer)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btn_Pratinjau)
        Me.Controls.Add(Me.grb_Produk)
        Me.Controls.Add(Me.txt_TotalTagihan_Kotor)
        Me.Controls.Add(Me.lbl_PPN)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_PPN)
        Me.Controls.Add(Me.txt_DasarPengenaanPajak)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_Diskon_Rp)
        Me.Controls.Add(Me.lbl_Diskon)
        Me.Controls.Add(Me.txt_JumlahHargaKeseluruhan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_Catatan)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Cetak)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.dtp_TanggalInvoice)
        Me.Controls.Add(Me.lbl_TanggalInvoice)
        Me.Controls.Add(Me.txt_NomorInvoice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_KodeCustomer)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txt_NamaCustomer)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_Input_InvoicePenjualan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Invoice Penjualan"
        Me.grb_Produk.ResumeLayout(False)
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_SJBAST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grb_Bank.ResumeLayout(False)
        Me.grb_Bank.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label14 As Label
    Friend WithEvents txt_Diskon_Persen As TextBox
    Friend WithEvents txt_AlamatCustomer As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents btn_Pratinjau As Button
    Friend WithEvents grb_Produk As GroupBox
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents txt_TotalTagihan_Kotor As TextBox
    Friend WithEvents lbl_PPN As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_PPN As TextBox
    Friend WithEvents txt_DasarPengenaanPajak As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_Diskon_Rp As TextBox
    Friend WithEvents lbl_Diskon As Label
    Friend WithEvents txt_JumlahHargaKeseluruhan As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Catatan As RichTextBox
    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Cetak As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents dtp_TanggalInvoice As DateTimePicker
    Friend WithEvents lbl_TanggalInvoice As Label
    Friend WithEvents txt_NomorInvoice As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents txt_KodeCustomer As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_NamaCustomer As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents dtp_TanggalJatuhTempo As DateTimePicker
    Friend WithEvents Label15 As Label
    Friend WithEvents btn_SingkirkanSJBAST As Button
    Friend WithEvents btn_TambahSJBAST As Button
    Friend WithEvents dgv_SJBAST As DataGridView
    Friend WithEvents lbl_SJBAST As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmb_JenisInvoice As ComboBox
    Friend WithEvents txt_PPhDitanggung As TextBox
    Friend WithEvents lbl_PPhDitanggung As Label
    Friend WithEvents txt_PPhDipotong As TextBox
    Friend WithEvents lbl_PPhDipotong As Label
    Friend WithEvents lbl_PPh As Label
    Friend WithEvents lbl_PersenPPh As Label
    Friend WithEvents txt_TarifPPh As TextBox
    Friend WithEvents cmb_JenisPPh As ComboBox
    Friend WithEvents txt_PPhTerutang As TextBox
    Friend WithEvents txt_TotalTagihan As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_DPPJasa As TextBox
    Friend WithEvents lbl_DPPJasa As Label
    Friend WithEvents txt_DPPBarang As TextBox
    Friend WithEvents lbl_DPPBarang As Label
    Friend WithEvents lbl_PerlakuanPPN As Label
    Friend WithEvents cmb_PerlakuanPPN As ComboBox
    Friend WithEvents lbl_JenisPPN As Label
    Friend WithEvents cmb_JenisPPN As ComboBox
    Friend WithEvents txt_JumlahHariJatuhTempo As TextBox
    Friend WithEvents lbl_JumlahHariJatuhTempo As Label
    Friend WithEvents rdb_JumlahHariJatuhTempo As RadioButton
    Friend WithEvents rdb_TanggalJatuhTempo As RadioButton
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_BiayaTransportasiPenjualan As TextBox
    Friend WithEvents Nomor_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Diterima As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Biaya_Transportasi As DataGridViewTextBoxColumn
    Friend WithEvents txt_Referensi As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents grb_Bank As GroupBox
    Friend WithEvents Label54 As Label
    Friend WithEvents Label55 As Label
    Friend WithEvents txt_BiayaAdministrasiBank As TextBox
    Friend WithEvents cmb_DitanggungOleh As ComboBox
    Friend WithEvents txt_JumlahTransfer As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cmb_SaranaPembayaran As ComboBox
    Friend WithEvents lbl_SaranaPembayaran As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_TarifPPN As TextBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk_Per_Item As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_SJ_BAST_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ_BAST_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Diterima_SJ_BAST_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Deskripsi_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Satuan_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Harga_Satuan As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Harga_Per_Item As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Per_Item_Persen As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Per_Item_Rp As DataGridViewTextBoxColumn
    Friend WithEvents Total_Harga As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project_Produk As DataGridViewTextBoxColumn
End Class
