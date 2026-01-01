<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Input_POPembelian
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgv_Invoice = New System.Windows.Forms.DataGridView()
        Me.Nomor_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_SJ_BAST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_SJ_BAST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgv_SJBAST = New System.Windows.Forms.DataGridView()
        Me.lbl_SJBAST = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmb_Kontrol = New System.Windows.Forms.ComboBox()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.txt_KodeSupplier = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txt_NamaSupplier = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_DPPJasa = New System.Windows.Forms.Label()
        Me.txt_DPPBarang = New System.Windows.Forms.TextBox()
        Me.lbl_DPPBarang = New System.Windows.Forms.Label()
        Me.txt_PPhDitanggung = New System.Windows.Forms.TextBox()
        Me.lbl_PPhDitanggung = New System.Windows.Forms.Label()
        Me.txt_PPhDipotong = New System.Windows.Forms.TextBox()
        Me.lbl_PPhDipotong = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txt_TotalTagihan_Kotor = New System.Windows.Forms.TextBox()
        Me.lbl_PersenPPh = New System.Windows.Forms.Label()
        Me.txt_TarifPPh = New System.Windows.Forms.TextBox()
        Me.cmb_JenisPPh = New System.Windows.Forms.ComboBox()
        Me.txt_PPhTerutang = New System.Windows.Forms.TextBox()
        Me.lbl_PerlakuanPPN = New System.Windows.Forms.Label()
        Me.cmb_PerlakuanPPN = New System.Windows.Forms.ComboBox()
        Me.lbl_Invoice = New System.Windows.Forms.Label()
        Me.txt_DPPJasa = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_TotalTagihan = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_PPN = New System.Windows.Forms.TextBox()
        Me.lbl_PPN = New System.Windows.Forms.Label()
        Me.txt_DasarPengenaanPajak = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.btn_Tambah = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_Diskon_Persen = New System.Windows.Forms.TextBox()
        Me.lbl_JenisPPN = New System.Windows.Forms.Label()
        Me.cmb_JenisPPN = New System.Windows.Forms.ComboBox()
        Me.txt_KeteranganToP = New System.Windows.Forms.TextBox()
        Me.txt_AlamatSupplier = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grb_Produk = New System.Windows.Forms.GroupBox()
        Me.txt_Diskon_Rp = New System.Windows.Forms.TextBox()
        Me.lbl_Diskon = New System.Windows.Forms.Label()
        Me.txt_JumlahHargaKeseluruhan = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Catatan = New System.Windows.Forms.RichTextBox()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_TermOfPayment = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtp_TanggalPO = New System.Windows.Forms.DateTimePicker()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txt_NomorPO = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.txt_PembuatPO = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btn_Pratinjau = New System.Windows.Forms.Button()
        Me.btn_Cetak = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_Attention = New System.Windows.Forms.TextBox()
        Me.rdb_TanggalJangkaWaktuPenyelesaian = New System.Windows.Forms.RadioButton()
        Me.rdb_JumlahHariJangkaWaktuPenyelesaian = New System.Windows.Forms.RadioButton()
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian = New System.Windows.Forms.Label()
        Me.txt_JumlahHariJangkaWaktuPenyelesaian = New System.Windows.Forms.TextBox()
        Me.dtp_TanggalJangkaWaktuPenyelesaian = New System.Windows.Forms.DateTimePicker()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txt_BiayaTransportasiPembelian = New System.Windows.Forms.TextBox()
        Me.cmb_JenisJasa = New System.Windows.Forms.ComboBox()
        Me.lbl_JenisJasa = New System.Windows.Forms.Label()
        Me.cmb_KodeSetoran = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_TarifPPN = New System.Windows.Forms.TextBox()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_Produk_Per_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        CType(Me.dgv_Invoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_SJBAST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grb_Produk.SuspendLayout()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgv_Invoice
        '
        Me.dgv_Invoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgv_Invoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Invoice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Invoice, Me.Tanggal_Invoice})
        Me.dgv_Invoice.Location = New System.Drawing.Point(446, 395)
        Me.dgv_Invoice.Name = "dgv_Invoice"
        Me.dgv_Invoice.RowHeadersVisible = False
        Me.dgv_Invoice.Size = New System.Drawing.Size(169, 149)
        Me.dgv_Invoice.TabIndex = 10345
        '
        'Nomor_Invoice
        '
        Me.Nomor_Invoice.HeaderText = "Nomor Invoice"
        Me.Nomor_Invoice.Name = "Nomor_Invoice"
        Me.Nomor_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Invoice.Width = 90
        '
        'Tanggal_Invoice
        '
        Me.Tanggal_Invoice.HeaderText = "Tanggal"
        Me.Tanggal_Invoice.Name = "Tanggal_Invoice"
        Me.Tanggal_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Invoice.Width = 72
        '
        'Tanggal_SJ_BAST
        '
        Me.Tanggal_SJ_BAST.HeaderText = "Tanggal"
        Me.Tanggal_SJ_BAST.Name = "Tanggal_SJ_BAST"
        Me.Tanggal_SJ_BAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_SJ_BAST.Width = 72
        '
        'Nomor_SJ_BAST
        '
        Me.Nomor_SJ_BAST.HeaderText = "No. SJ/BAST"
        Me.Nomor_SJ_BAST.Name = "Nomor_SJ_BAST"
        Me.Nomor_SJ_BAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_SJ_BAST.Width = 91
        '
        'dgv_SJBAST
        '
        Me.dgv_SJBAST.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgv_SJBAST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_SJBAST.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_SJ_BAST, Me.Tanggal_SJ_BAST})
        Me.dgv_SJBAST.Location = New System.Drawing.Point(264, 395)
        Me.dgv_SJBAST.Name = "dgv_SJBAST"
        Me.dgv_SJBAST.RowHeadersVisible = False
        Me.dgv_SJBAST.Size = New System.Drawing.Size(169, 149)
        Me.dgv_SJBAST.TabIndex = 10344
        '
        'lbl_SJBAST
        '
        Me.lbl_SJBAST.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_SJBAST.AutoSize = True
        Me.lbl_SJBAST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SJBAST.Location = New System.Drawing.Point(264, 375)
        Me.lbl_SJBAST.Name = "lbl_SJBAST"
        Me.lbl_SJBAST.Size = New System.Drawing.Size(105, 13)
        Me.lbl_SJBAST.TabIndex = 10343
        Me.lbl_SJBAST.Text = "Surat Jalan / BAST :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(461, 136)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(46, 13)
        Me.Label15.TabIndex = 10342
        Me.Label15.Text = "Kontrol :"
        '
        'cmb_Kontrol
        '
        Me.cmb_Kontrol.FormattingEnabled = True
        Me.cmb_Kontrol.Location = New System.Drawing.Point(464, 154)
        Me.cmb_Kontrol.Name = "cmb_Kontrol"
        Me.cmb_Kontrol.Size = New System.Drawing.Size(75, 21)
        Me.cmb_Kontrol.TabIndex = 10308
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(490, 18)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 10304
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'txt_KodeSupplier
        '
        Me.txt_KodeSupplier.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_KodeSupplier.Location = New System.Drawing.Point(403, 19)
        Me.txt_KodeSupplier.MaxLength = 3
        Me.txt_KodeSupplier.Name = "txt_KodeSupplier"
        Me.txt_KodeSupplier.Size = New System.Drawing.Size(81, 20)
        Me.txt_KodeSupplier.TabIndex = 10303
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(307, 22)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(73, 13)
        Me.Label24.TabIndex = 10326
        Me.Label24.Text = "Kode Supplier"
        '
        'txt_NamaSupplier
        '
        Me.txt_NamaSupplier.Location = New System.Drawing.Point(403, 45)
        Me.txt_NamaSupplier.Name = "txt_NamaSupplier"
        Me.txt_NamaSupplier.Size = New System.Drawing.Size(193, 20)
        Me.txt_NamaSupplier.TabIndex = 10305
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(307, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 10327
        Me.Label2.Text = "Nama Supplier"
        '
        'lbl_DPPJasa
        '
        Me.lbl_DPPJasa.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_DPPJasa.AutoSize = True
        Me.lbl_DPPJasa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DPPJasa.Location = New System.Drawing.Point(391, 586)
        Me.lbl_DPPJasa.Name = "lbl_DPPJasa"
        Me.lbl_DPPJasa.Size = New System.Drawing.Size(54, 13)
        Me.lbl_DPPJasa.TabIndex = 10357
        Me.lbl_DPPJasa.Text = "DPP Jasa"
        '
        'txt_DPPBarang
        '
        Me.txt_DPPBarang.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DPPBarang.Location = New System.Drawing.Point(474, 557)
        Me.txt_DPPBarang.Name = "txt_DPPBarang"
        Me.txt_DPPBarang.Size = New System.Drawing.Size(92, 20)
        Me.txt_DPPBarang.TabIndex = 10354
        Me.txt_DPPBarang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_DPPBarang
        '
        Me.lbl_DPPBarang.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_DPPBarang.AutoSize = True
        Me.lbl_DPPBarang.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DPPBarang.Location = New System.Drawing.Point(391, 560)
        Me.lbl_DPPBarang.Name = "lbl_DPPBarang"
        Me.lbl_DPPBarang.Size = New System.Drawing.Size(66, 13)
        Me.lbl_DPPBarang.TabIndex = 10355
        Me.lbl_DPPBarang.Text = "DPP Barang"
        '
        'txt_PPhDitanggung
        '
        Me.txt_PPhDitanggung.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPhDitanggung.Location = New System.Drawing.Point(803, 528)
        Me.txt_PPhDitanggung.Name = "txt_PPhDitanggung"
        Me.txt_PPhDitanggung.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPhDitanggung.TabIndex = 10320
        Me.txt_PPhDitanggung.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PPhDitanggung
        '
        Me.lbl_PPhDitanggung.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PPhDitanggung.AutoSize = True
        Me.lbl_PPhDitanggung.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPhDitanggung.Location = New System.Drawing.Point(628, 531)
        Me.lbl_PPhDitanggung.Name = "lbl_PPhDitanggung"
        Me.lbl_PPhDitanggung.Size = New System.Drawing.Size(85, 13)
        Me.lbl_PPhDitanggung.TabIndex = 10353
        Me.lbl_PPhDitanggung.Text = "PPh Ditanggung"
        '
        'txt_PPhDipotong
        '
        Me.txt_PPhDipotong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPhDipotong.Location = New System.Drawing.Point(803, 554)
        Me.txt_PPhDipotong.Name = "txt_PPhDipotong"
        Me.txt_PPhDipotong.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPhDipotong.TabIndex = 10321
        Me.txt_PPhDipotong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PPhDipotong
        '
        Me.lbl_PPhDipotong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PPhDipotong.AutoSize = True
        Me.lbl_PPhDipotong.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPhDipotong.Location = New System.Drawing.Point(628, 557)
        Me.lbl_PPhDipotong.Name = "lbl_PPhDipotong"
        Me.lbl_PPhDipotong.Size = New System.Drawing.Size(73, 13)
        Me.lbl_PPhDipotong.TabIndex = 10352
        Me.lbl_PPhDipotong.Text = "PPh Dipotong"
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(628, 479)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(31, 13)
        Me.Label16.TabIndex = 10351
        Me.Label16.Text = "Total"
        '
        'txt_TotalTagihan_Kotor
        '
        Me.txt_TotalTagihan_Kotor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TotalTagihan_Kotor.Location = New System.Drawing.Point(803, 476)
        Me.txt_TotalTagihan_Kotor.Name = "txt_TotalTagihan_Kotor"
        Me.txt_TotalTagihan_Kotor.Size = New System.Drawing.Size(92, 20)
        Me.txt_TotalTagihan_Kotor.TabIndex = 10316
        Me.txt_TotalTagihan_Kotor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PersenPPh
        '
        Me.lbl_PersenPPh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PersenPPh.AutoSize = True
        Me.lbl_PersenPPh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PersenPPh.Location = New System.Drawing.Point(788, 505)
        Me.lbl_PersenPPh.Name = "lbl_PersenPPh"
        Me.lbl_PersenPPh.Size = New System.Drawing.Size(15, 13)
        Me.lbl_PersenPPh.TabIndex = 10349
        Me.lbl_PersenPPh.Text = "%"
        '
        'txt_TarifPPh
        '
        Me.txt_TarifPPh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TarifPPh.Location = New System.Drawing.Point(760, 502)
        Me.txt_TarifPPh.MaxLength = 5
        Me.txt_TarifPPh.Name = "txt_TarifPPh"
        Me.txt_TarifPPh.Size = New System.Drawing.Size(30, 20)
        Me.txt_TarifPPh.TabIndex = 10318
        Me.txt_TarifPPh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmb_JenisPPh
        '
        Me.cmb_JenisPPh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_JenisPPh.FormattingEnabled = True
        Me.cmb_JenisPPh.Location = New System.Drawing.Point(631, 502)
        Me.cmb_JenisPPh.Name = "cmb_JenisPPh"
        Me.cmb_JenisPPh.Size = New System.Drawing.Size(75, 21)
        Me.cmb_JenisPPh.TabIndex = 10317
        '
        'txt_PPhTerutang
        '
        Me.txt_PPhTerutang.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPhTerutang.Location = New System.Drawing.Point(803, 502)
        Me.txt_PPhTerutang.Name = "txt_PPhTerutang"
        Me.txt_PPhTerutang.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPhTerutang.TabIndex = 10319
        Me.txt_PPhTerutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PerlakuanPPN
        '
        Me.lbl_PerlakuanPPN.AutoSize = True
        Me.lbl_PerlakuanPPN.Enabled = False
        Me.lbl_PerlakuanPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PerlakuanPPN.Location = New System.Drawing.Point(628, 47)
        Me.lbl_PerlakuanPPN.Name = "lbl_PerlakuanPPN"
        Me.lbl_PerlakuanPPN.Size = New System.Drawing.Size(86, 13)
        Me.lbl_PerlakuanPPN.TabIndex = 10348
        Me.lbl_PerlakuanPPN.Text = "Perlakuan PPN :"
        Me.lbl_PerlakuanPPN.Visible = False
        '
        'cmb_PerlakuanPPN
        '
        Me.cmb_PerlakuanPPN.Enabled = False
        Me.cmb_PerlakuanPPN.FormattingEnabled = True
        Me.cmb_PerlakuanPPN.Location = New System.Drawing.Point(631, 66)
        Me.cmb_PerlakuanPPN.Name = "cmb_PerlakuanPPN"
        Me.cmb_PerlakuanPPN.Size = New System.Drawing.Size(97, 21)
        Me.cmb_PerlakuanPPN.TabIndex = 10347
        Me.cmb_PerlakuanPPN.Visible = False
        '
        'lbl_Invoice
        '
        Me.lbl_Invoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_Invoice.AutoSize = True
        Me.lbl_Invoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Invoice.Location = New System.Drawing.Point(448, 375)
        Me.lbl_Invoice.Name = "lbl_Invoice"
        Me.lbl_Invoice.Size = New System.Drawing.Size(48, 13)
        Me.lbl_Invoice.TabIndex = 10346
        Me.lbl_Invoice.Text = "Invoice :"
        '
        'txt_DPPJasa
        '
        Me.txt_DPPJasa.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DPPJasa.Location = New System.Drawing.Point(474, 583)
        Me.txt_DPPJasa.Name = "txt_DPPJasa"
        Me.txt_DPPJasa.Size = New System.Drawing.Size(92, 20)
        Me.txt_DPPJasa.TabIndex = 10356
        Me.txt_DPPJasa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(164, 74)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 13)
        Me.Label12.TabIndex = 10338
        Me.Label12.Text = "Hari"
        '
        'txt_TotalTagihan
        '
        Me.txt_TotalTagihan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TotalTagihan.Location = New System.Drawing.Point(803, 580)
        Me.txt_TotalTagihan.Name = "txt_TotalTagihan"
        Me.txt_TotalTagihan.Size = New System.Drawing.Size(92, 20)
        Me.txt_TotalTagihan.TabIndex = 10323
        Me.txt_TotalTagihan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(628, 583)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 10337
        Me.Label10.Text = "Total Tagihan"
        '
        'txt_PPN
        '
        Me.txt_PPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPN.Location = New System.Drawing.Point(803, 450)
        Me.txt_PPN.Name = "txt_PPN"
        Me.txt_PPN.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPN.TabIndex = 10315
        Me.txt_PPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PPN
        '
        Me.lbl_PPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PPN.AutoSize = True
        Me.lbl_PPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPN.Location = New System.Drawing.Point(628, 453)
        Me.lbl_PPN.Name = "lbl_PPN"
        Me.lbl_PPN.Size = New System.Drawing.Size(29, 13)
        Me.lbl_PPN.TabIndex = 10336
        Me.lbl_PPN.Text = "PPN"
        '
        'txt_DasarPengenaanPajak
        '
        Me.txt_DasarPengenaanPajak.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DasarPengenaanPajak.Location = New System.Drawing.Point(803, 424)
        Me.txt_DasarPengenaanPajak.Name = "txt_DasarPengenaanPajak"
        Me.txt_DasarPengenaanPajak.Size = New System.Drawing.Size(92, 20)
        Me.txt_DasarPengenaanPajak.TabIndex = 10314
        Me.txt_DasarPengenaanPajak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(628, 428)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(123, 13)
        Me.Label9.TabIndex = 10335
        Me.Label9.Text = "Dasar Pengenaan Pajak"
        '
        'btn_Hapus
        '
        Me.btn_Hapus.Location = New System.Drawing.Point(156, 16)
        Me.btn_Hapus.Name = "btn_Hapus"
        Me.btn_Hapus.Size = New System.Drawing.Size(68, 30)
        Me.btn_Hapus.TabIndex = 230
        Me.btn_Hapus.Text = "Hapus"
        Me.btn_Hapus.UseVisualStyleBackColor = True
        '
        'btn_Edit
        '
        Me.btn_Edit.Location = New System.Drawing.Point(82, 16)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(68, 30)
        Me.btn_Edit.TabIndex = 220
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'btn_Tambah
        '
        Me.btn_Tambah.Location = New System.Drawing.Point(8, 16)
        Me.btn_Tambah.Name = "btn_Tambah"
        Me.btn_Tambah.Size = New System.Drawing.Size(68, 30)
        Me.btn_Tambah.TabIndex = 210
        Me.btn_Tambah.Text = "Tambah"
        Me.btn_Tambah.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(787, 402)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(15, 13)
        Me.Label14.TabIndex = 10341
        Me.Label14.Text = "%"
        Me.Label14.Visible = False
        '
        'txt_Diskon_Persen
        '
        Me.txt_Diskon_Persen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Diskon_Persen.Location = New System.Drawing.Point(755, 398)
        Me.txt_Diskon_Persen.MaxLength = 5
        Me.txt_Diskon_Persen.Name = "txt_Diskon_Persen"
        Me.txt_Diskon_Persen.Size = New System.Drawing.Size(33, 20)
        Me.txt_Diskon_Persen.TabIndex = 10312
        Me.txt_Diskon_Persen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_Diskon_Persen.Visible = False
        '
        'lbl_JenisPPN
        '
        Me.lbl_JenisPPN.AutoSize = True
        Me.lbl_JenisPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JenisPPN.Location = New System.Drawing.Point(307, 135)
        Me.lbl_JenisPPN.Name = "lbl_JenisPPN"
        Me.lbl_JenisPPN.Size = New System.Drawing.Size(62, 13)
        Me.lbl_JenisPPN.TabIndex = 10340
        Me.lbl_JenisPPN.Text = "Jenis PPN :"
        '
        'cmb_JenisPPN
        '
        Me.cmb_JenisPPN.FormattingEnabled = True
        Me.cmb_JenisPPN.Location = New System.Drawing.Point(310, 154)
        Me.cmb_JenisPPN.Name = "cmb_JenisPPN"
        Me.cmb_JenisPPN.Size = New System.Drawing.Size(92, 21)
        Me.cmb_JenisPPN.TabIndex = 10307
        '
        'txt_KeteranganToP
        '
        Me.txt_KeteranganToP.Location = New System.Drawing.Point(118, 97)
        Me.txt_KeteranganToP.Multiline = True
        Me.txt_KeteranganToP.Name = "txt_KeteranganToP"
        Me.txt_KeteranganToP.Size = New System.Drawing.Size(163, 51)
        Me.txt_KeteranganToP.TabIndex = 10301
        '
        'txt_AlamatSupplier
        '
        Me.txt_AlamatSupplier.Location = New System.Drawing.Point(134, 636)
        Me.txt_AlamatSupplier.Multiline = True
        Me.txt_AlamatSupplier.Name = "txt_AlamatSupplier"
        Me.txt_AlamatSupplier.Size = New System.Drawing.Size(193, 20)
        Me.txt_AlamatSupplier.TabIndex = 999999999
        Me.txt_AlamatSupplier.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(38, 639)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 13)
        Me.Label11.TabIndex = 10339
        Me.Label11.Text = "Alamat Supplier"
        Me.Label11.Visible = False
        '
        'grb_Produk
        '
        Me.grb_Produk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grb_Produk.Controls.Add(Me.btn_Hapus)
        Me.grb_Produk.Controls.Add(Me.btn_Edit)
        Me.grb_Produk.Controls.Add(Me.btn_Tambah)
        Me.grb_Produk.Location = New System.Drawing.Point(663, 124)
        Me.grb_Produk.Name = "grb_Produk"
        Me.grb_Produk.Size = New System.Drawing.Size(232, 55)
        Me.grb_Produk.TabIndex = 10309
        Me.grb_Produk.TabStop = False
        Me.grb_Produk.Text = "Barang/Jasa :"
        '
        'txt_Diskon_Rp
        '
        Me.txt_Diskon_Rp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Diskon_Rp.Location = New System.Drawing.Point(803, 398)
        Me.txt_Diskon_Rp.Name = "txt_Diskon_Rp"
        Me.txt_Diskon_Rp.Size = New System.Drawing.Size(92, 20)
        Me.txt_Diskon_Rp.TabIndex = 10313
        Me.txt_Diskon_Rp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Diskon
        '
        Me.lbl_Diskon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Diskon.AutoSize = True
        Me.lbl_Diskon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diskon.Location = New System.Drawing.Point(628, 402)
        Me.lbl_Diskon.Name = "lbl_Diskon"
        Me.lbl_Diskon.Size = New System.Drawing.Size(49, 13)
        Me.lbl_Diskon.TabIndex = 10334
        Me.lbl_Diskon.Text = "Discount"
        '
        'txt_JumlahHargaKeseluruhan
        '
        Me.txt_JumlahHargaKeseluruhan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_JumlahHargaKeseluruhan.Location = New System.Drawing.Point(803, 372)
        Me.txt_JumlahHargaKeseluruhan.Name = "txt_JumlahHargaKeseluruhan"
        Me.txt_JumlahHargaKeseluruhan.Size = New System.Drawing.Size(92, 20)
        Me.txt_JumlahHargaKeseluruhan.TabIndex = 10311
        Me.txt_JumlahHargaKeseluruhan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(628, 375)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 10333
        Me.Label6.Text = "Jumlah"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 375)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 10332
        Me.Label5.Text = "Catatan :"
        '
        'txt_Catatan
        '
        Me.txt_Catatan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Catatan.Location = New System.Drawing.Point(18, 395)
        Me.txt_Catatan.Name = "txt_Catatan"
        Me.txt_Catatan.Size = New System.Drawing.Size(232, 105)
        Me.txt_Catatan.TabIndex = 10322
        Me.txt_Catatan.Text = ""
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(812, 616)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 10324
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(723, 616)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10325
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_TermOfPayment
        '
        Me.txt_TermOfPayment.Location = New System.Drawing.Point(118, 71)
        Me.txt_TermOfPayment.Name = "txt_TermOfPayment"
        Me.txt_TermOfPayment.Size = New System.Drawing.Size(44, 20)
        Me.txt_TermOfPayment.TabIndex = 10300
        Me.txt_TermOfPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 10330
        Me.Label3.Text = "Term of Payment"
        '
        'dtp_TanggalPO
        '
        Me.dtp_TanggalPO.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalPO.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalPO.Location = New System.Drawing.Point(118, 45)
        Me.dtp_TanggalPO.Name = "dtp_TanggalPO"
        Me.dtp_TanggalPO.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalPO.TabIndex = 10299
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(16, 49)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(46, 13)
        Me.Label50.TabIndex = 10329
        Me.Label50.Text = "Tanggal"
        '
        'txt_NomorPO
        '
        Me.txt_NomorPO.Location = New System.Drawing.Point(118, 19)
        Me.txt_NomorPO.Name = "txt_NomorPO"
        Me.txt_NomorPO.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorPO.TabIndex = 10298
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 10328
        Me.Label1.Text = "Nomor"
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Jenis_Produk_Per_Item, Me.Nama_Produk, Me.Deskripsi_Produk, Me.Jumlah_Produk, Me.Satuan_Produk, Me.Harga_Satuan, Me.Jumlah_Harga_Per_Item, Me.Diskon_Per_Item_Persen, Me.Diskon_Per_Item_Rp, Me.Total_Harga, Me.Kode_Project_Produk})
        Me.DataTabelUtama.Location = New System.Drawing.Point(18, 185)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(877, 177)
        Me.DataTabelUtama.TabIndex = 10310
        '
        'txt_PembuatPO
        '
        Me.txt_PembuatPO.Location = New System.Drawing.Point(118, 154)
        Me.txt_PembuatPO.Name = "txt_PembuatPO"
        Me.txt_PembuatPO.Size = New System.Drawing.Size(163, 20)
        Me.txt_PembuatPO.TabIndex = 10358
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 157)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 10359
        Me.Label7.Text = "PembuatPO"
        '
        'btn_Pratinjau
        '
        Me.btn_Pratinjau.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pratinjau.Location = New System.Drawing.Point(803, 12)
        Me.btn_Pratinjau.Name = "btn_Pratinjau"
        Me.btn_Pratinjau.Size = New System.Drawing.Size(92, 46)
        Me.btn_Pratinjau.TabIndex = 10361
        Me.btn_Pratinjau.Text = "Pratinjau"
        Me.btn_Pratinjau.UseVisualStyleBackColor = True
        '
        'btn_Cetak
        '
        Me.btn_Cetak.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cetak.Location = New System.Drawing.Point(803, 64)
        Me.btn_Cetak.Name = "btn_Cetak"
        Me.btn_Cetak.Size = New System.Drawing.Size(92, 46)
        Me.btn_Cetak.TabIndex = 10360
        Me.btn_Cetak.Text = "Cetak"
        Me.btn_Cetak.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(307, 74)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 13)
        Me.Label13.TabIndex = 10363
        Me.Label13.Text = "Attention"
        '
        'txt_Attention
        '
        Me.txt_Attention.Location = New System.Drawing.Point(403, 71)
        Me.txt_Attention.Name = "txt_Attention"
        Me.txt_Attention.Size = New System.Drawing.Size(193, 20)
        Me.txt_Attention.TabIndex = 10306
        '
        'rdb_TanggalJangkaWaktuPenyelesaian
        '
        Me.rdb_TanggalJangkaWaktuPenyelesaian.AutoSize = True
        Me.rdb_TanggalJangkaWaktuPenyelesaian.Location = New System.Drawing.Point(19, 609)
        Me.rdb_TanggalJangkaWaktuPenyelesaian.Name = "rdb_TanggalJangkaWaktuPenyelesaian"
        Me.rdb_TanggalJangkaWaktuPenyelesaian.Size = New System.Drawing.Size(14, 13)
        Me.rdb_TanggalJangkaWaktuPenyelesaian.TabIndex = 1000000002
        Me.rdb_TanggalJangkaWaktuPenyelesaian.TabStop = True
        Me.rdb_TanggalJangkaWaktuPenyelesaian.UseVisualStyleBackColor = True
        '
        'rdb_JumlahHariJangkaWaktuPenyelesaian
        '
        Me.rdb_JumlahHariJangkaWaktuPenyelesaian.AutoSize = True
        Me.rdb_JumlahHariJangkaWaktuPenyelesaian.Location = New System.Drawing.Point(19, 583)
        Me.rdb_JumlahHariJangkaWaktuPenyelesaian.Name = "rdb_JumlahHariJangkaWaktuPenyelesaian"
        Me.rdb_JumlahHariJangkaWaktuPenyelesaian.Size = New System.Drawing.Size(14, 13)
        Me.rdb_JumlahHariJangkaWaktuPenyelesaian.TabIndex = 1000000000
        Me.rdb_JumlahHariJangkaWaktuPenyelesaian.TabStop = True
        Me.rdb_JumlahHariJangkaWaktuPenyelesaian.UseVisualStyleBackColor = True
        '
        'lbl_JumlahHariAngkaWaktuPenyelesaian
        '
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian.AutoSize = True
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian.Enabled = False
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian.Location = New System.Drawing.Point(87, 583)
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian.Name = "lbl_JumlahHariAngkaWaktuPenyelesaian"
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian.Size = New System.Drawing.Size(26, 13)
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian.TabIndex = 1000000005
        Me.lbl_JumlahHariAngkaWaktuPenyelesaian.Text = "Hari"
        '
        'txt_JumlahHariJangkaWaktuPenyelesaian
        '
        Me.txt_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
        Me.txt_JumlahHariJangkaWaktuPenyelesaian.Location = New System.Drawing.Point(38, 580)
        Me.txt_JumlahHariJangkaWaktuPenyelesaian.Name = "txt_JumlahHariJangkaWaktuPenyelesaian"
        Me.txt_JumlahHariJangkaWaktuPenyelesaian.Size = New System.Drawing.Size(43, 20)
        Me.txt_JumlahHariJangkaWaktuPenyelesaian.TabIndex = 1000000001
        '
        'dtp_TanggalJangkaWaktuPenyelesaian
        '
        Me.dtp_TanggalJangkaWaktuPenyelesaian.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalJangkaWaktuPenyelesaian.Enabled = False
        Me.dtp_TanggalJangkaWaktuPenyelesaian.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalJangkaWaktuPenyelesaian.Location = New System.Drawing.Point(38, 606)
        Me.dtp_TanggalJangkaWaktuPenyelesaian.Name = "dtp_TanggalJangkaWaktuPenyelesaian"
        Me.dtp_TanggalJangkaWaktuPenyelesaian.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalJangkaWaktuPenyelesaian.TabIndex = 1000000003
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(17, 560)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(143, 13)
        Me.Label17.TabIndex = 1000000004
        Me.Label17.Text = "Jangka Waktu Penyelesaian"
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(391, 635)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(69, 13)
        Me.Label18.TabIndex = 1000000007
        Me.Label18.Text = "Ongkos Kirim"
        '
        'txt_BiayaTransportasiPembelian
        '
        Me.txt_BiayaTransportasiPembelian.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_BiayaTransportasiPembelian.Location = New System.Drawing.Point(474, 632)
        Me.txt_BiayaTransportasiPembelian.Name = "txt_BiayaTransportasiPembelian"
        Me.txt_BiayaTransportasiPembelian.Size = New System.Drawing.Size(92, 20)
        Me.txt_BiayaTransportasiPembelian.TabIndex = 1000000006
        Me.txt_BiayaTransportasiPembelian.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmb_JenisJasa
        '
        Me.cmb_JenisJasa.Enabled = False
        Me.cmb_JenisJasa.FormattingEnabled = True
        Me.cmb_JenisJasa.Location = New System.Drawing.Point(403, 97)
        Me.cmb_JenisJasa.Name = "cmb_JenisJasa"
        Me.cmb_JenisJasa.Size = New System.Drawing.Size(194, 21)
        Me.cmb_JenisJasa.TabIndex = 1000000008
        '
        'lbl_JenisJasa
        '
        Me.lbl_JenisJasa.AutoSize = True
        Me.lbl_JenisJasa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JenisJasa.Location = New System.Drawing.Point(309, 100)
        Me.lbl_JenisJasa.Name = "lbl_JenisJasa"
        Me.lbl_JenisJasa.Size = New System.Drawing.Size(56, 13)
        Me.lbl_JenisJasa.TabIndex = 1000000009
        Me.lbl_JenisJasa.Text = "Jenis Jasa"
        '
        'cmb_KodeSetoran
        '
        Me.cmb_KodeSetoran.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_KodeSetoran.FormattingEnabled = True
        Me.cmb_KodeSetoran.Location = New System.Drawing.Point(712, 502)
        Me.cmb_KodeSetoran.Name = "cmb_KodeSetoran"
        Me.cmb_KodeSetoran.Size = New System.Drawing.Size(42, 21)
        Me.cmb_KodeSetoran.TabIndex = 1000000010
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(787, 453)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 13)
        Me.Label8.TabIndex = 1000000027
        Me.Label8.Text = "%"
        '
        'txt_TarifPPN
        '
        Me.txt_TarifPPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TarifPPN.Location = New System.Drawing.Point(755, 450)
        Me.txt_TarifPPN.MaxLength = 2
        Me.txt_TarifPPN.Name = "txt_TarifPPN"
        Me.txt_TarifPPN.Size = New System.Drawing.Size(33, 20)
        Me.txt_TarifPPN.TabIndex = 1000000026
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
        'Nama_Produk
        '
        Me.Nama_Produk.HeaderText = "Nama Barang/Jasa"
        Me.Nama_Produk.Name = "Nama_Produk"
        Me.Nama_Produk.ReadOnly = True
        Me.Nama_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Produk.Width = 160
        '
        'Deskripsi_Produk
        '
        Me.Deskripsi_Produk.HeaderText = "Deskripsi"
        Me.Deskripsi_Produk.Name = "Deskripsi_Produk"
        Me.Deskripsi_Produk.ReadOnly = True
        Me.Deskripsi_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Deskripsi_Produk.Width = 162
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
        'frm_Input_POPembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(912, 668)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_TarifPPN)
        Me.Controls.Add(Me.cmb_KodeSetoran)
        Me.Controls.Add(Me.cmb_JenisJasa)
        Me.Controls.Add(Me.lbl_JenisJasa)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txt_BiayaTransportasiPembelian)
        Me.Controls.Add(Me.rdb_TanggalJangkaWaktuPenyelesaian)
        Me.Controls.Add(Me.rdb_JumlahHariJangkaWaktuPenyelesaian)
        Me.Controls.Add(Me.lbl_JumlahHariAngkaWaktuPenyelesaian)
        Me.Controls.Add(Me.txt_JumlahHariJangkaWaktuPenyelesaian)
        Me.Controls.Add(Me.dtp_TanggalJangkaWaktuPenyelesaian)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txt_Attention)
        Me.Controls.Add(Me.btn_Pratinjau)
        Me.Controls.Add(Me.btn_Cetak)
        Me.Controls.Add(Me.txt_PembuatPO)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dgv_Invoice)
        Me.Controls.Add(Me.dgv_SJBAST)
        Me.Controls.Add(Me.lbl_SJBAST)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cmb_Kontrol)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_KodeSupplier)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txt_NamaSupplier)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbl_DPPJasa)
        Me.Controls.Add(Me.txt_DPPBarang)
        Me.Controls.Add(Me.lbl_DPPBarang)
        Me.Controls.Add(Me.txt_PPhDitanggung)
        Me.Controls.Add(Me.lbl_PPhDitanggung)
        Me.Controls.Add(Me.txt_PPhDipotong)
        Me.Controls.Add(Me.lbl_PPhDipotong)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txt_TotalTagihan_Kotor)
        Me.Controls.Add(Me.lbl_PersenPPh)
        Me.Controls.Add(Me.txt_TarifPPh)
        Me.Controls.Add(Me.cmb_JenisPPh)
        Me.Controls.Add(Me.txt_PPhTerutang)
        Me.Controls.Add(Me.lbl_PerlakuanPPN)
        Me.Controls.Add(Me.cmb_PerlakuanPPN)
        Me.Controls.Add(Me.lbl_Invoice)
        Me.Controls.Add(Me.txt_DPPJasa)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txt_TotalTagihan)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_PPN)
        Me.Controls.Add(Me.lbl_PPN)
        Me.Controls.Add(Me.txt_DasarPengenaanPajak)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txt_Diskon_Persen)
        Me.Controls.Add(Me.lbl_JenisPPN)
        Me.Controls.Add(Me.cmb_JenisPPN)
        Me.Controls.Add(Me.txt_KeteranganToP)
        Me.Controls.Add(Me.txt_AlamatSupplier)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.grb_Produk)
        Me.Controls.Add(Me.txt_Diskon_Rp)
        Me.Controls.Add(Me.lbl_Diskon)
        Me.Controls.Add(Me.txt_JumlahHargaKeseluruhan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_Catatan)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.txt_TermOfPayment)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtp_TanggalPO)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.txt_NomorPO)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_Input_POPembelian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input PO Pembelian"
        CType(Me.dgv_Invoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_SJBAST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grb_Produk.ResumeLayout(False)
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgv_Invoice As DataGridView
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents dgv_SJBAST As DataGridView
    Friend WithEvents lbl_SJBAST As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cmb_Kontrol As ComboBox
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents txt_KodeSupplier As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_NamaSupplier As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbl_DPPJasa As Label
    Friend WithEvents txt_DPPBarang As TextBox
    Friend WithEvents lbl_DPPBarang As Label
    Friend WithEvents txt_PPhDitanggung As TextBox
    Friend WithEvents lbl_PPhDitanggung As Label
    Friend WithEvents txt_PPhDipotong As TextBox
    Friend WithEvents lbl_PPhDipotong As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents txt_TotalTagihan_Kotor As TextBox
    Friend WithEvents lbl_PersenPPh As Label
    Friend WithEvents txt_TarifPPh As TextBox
    Friend WithEvents cmb_JenisPPh As ComboBox
    Friend WithEvents txt_PPhTerutang As TextBox
    Friend WithEvents lbl_PerlakuanPPN As Label
    Friend WithEvents cmb_PerlakuanPPN As ComboBox
    Friend WithEvents lbl_Invoice As Label
    Friend WithEvents txt_DPPJasa As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txt_TotalTagihan As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_PPN As TextBox
    Friend WithEvents lbl_PPN As Label
    Friend WithEvents txt_DasarPengenaanPajak As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents txt_Diskon_Persen As TextBox
    Friend WithEvents lbl_JenisPPN As Label
    Friend WithEvents cmb_JenisPPN As ComboBox
    Friend WithEvents txt_KeteranganToP As TextBox
    Friend WithEvents txt_AlamatSupplier As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents grb_Produk As GroupBox
    Friend WithEvents txt_Diskon_Rp As TextBox
    Friend WithEvents lbl_Diskon As Label
    Friend WithEvents txt_JumlahHargaKeseluruhan As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Catatan As RichTextBox
    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_TermOfPayment As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dtp_TanggalPO As DateTimePicker
    Friend WithEvents Label50 As Label
    Friend WithEvents txt_NomorPO As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents txt_PembuatPO As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btn_Pratinjau As Button
    Friend WithEvents btn_Cetak As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents txt_Attention As TextBox
    Friend WithEvents rdb_TanggalJangkaWaktuPenyelesaian As RadioButton
    Friend WithEvents rdb_JumlahHariJangkaWaktuPenyelesaian As RadioButton
    Friend WithEvents lbl_JumlahHariAngkaWaktuPenyelesaian As Label
    Friend WithEvents txt_JumlahHariJangkaWaktuPenyelesaian As TextBox
    Friend WithEvents dtp_TanggalJangkaWaktuPenyelesaian As DateTimePicker
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents txt_BiayaTransportasiPembelian As TextBox
    Friend WithEvents cmb_JenisJasa As ComboBox
    Friend WithEvents lbl_JenisJasa As Label
    Friend WithEvents cmb_KodeSetoran As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_TarifPPN As TextBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk_Per_Item As DataGridViewTextBoxColumn
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
