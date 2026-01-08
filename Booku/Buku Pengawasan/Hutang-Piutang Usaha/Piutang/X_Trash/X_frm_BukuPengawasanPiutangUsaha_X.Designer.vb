<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_BukuPengawasanPiutangUsaha_X
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As DataGridViewCellStyle = New DataGridViewCellStyle()
        cmb_Customer = New ComboBox()
        lbl_Supplier = New Label()
        Label15 = New Label()
        cmb_JenisProduk_Induk = New ComboBox()
        Label1 = New Label()
        cmb_JatuhTempo = New ComboBox()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_BPPU = New DataGridViewTextBoxColumn()
        Nomor_Penjualan = New DataGridViewTextBoxColumn()
        Jenis_Invoice = New DataGridViewTextBoxColumn()
        Jenis_Produk = New DataGridViewTextBoxColumn()
        Angka_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Masa_Jatuh_Tempo = New DataGridViewTextBoxColumn()
        Nomor_SJ_BAST = New DataGridViewTextBoxColumn()
        Tanggal_SJ_BAST = New DataGridViewTextBoxColumn()
        Nomor_PO = New DataGridViewTextBoxColumn()
        Tanggal_PO = New DataGridViewTextBoxColumn()
        Kode_Project = New DataGridViewTextBoxColumn()
        Kode_Customer = New DataGridViewTextBoxColumn()
        Nama_Customer = New DataGridViewTextBoxColumn()
        Jumlah_Harga = New DataGridViewTextBoxColumn()
        Diskon_Rp = New DataGridViewTextBoxColumn()
        Dasar_Pengenaan_Pajak = New DataGridViewTextBoxColumn()
        Jenis_PPN = New DataGridViewTextBoxColumn()
        PPN_ = New DataGridViewTextBoxColumn()
        Tagihan_Bruto = New DataGridViewTextBoxColumn()
        Retur_ = New DataGridViewTextBoxColumn()
        Jumlah_Piutang_Usaha = New DataGridViewTextBoxColumn()
        Sisa_Piutang_Usaha = New DataGridViewTextBoxColumn()
        Jenis_PPh = New DataGridViewTextBoxColumn()
        PPh_Terutang = New DataGridViewTextBoxColumn()
        PPh_Ditanggung = New DataGridViewTextBoxColumn()
        PPh_Dipotong = New DataGridViewTextBoxColumn()
        Tagihan_Netto = New DataGridViewTextBoxColumn()
        Keterangan_Jatuh_Tempo = New DataGridViewTextBoxColumn()
        Jumlah_Bayar = New DataGridViewTextBoxColumn()
        Sisa_Tagihan = New DataGridViewTextBoxColumn()
        Biaya_Administrasi_Bank = New DataGridViewTextBoxColumn()
        Catatan_ = New DataGridViewTextBoxColumn()
        Nomor_JV_Penjualan = New DataGridViewTextBoxColumn()
        lbl_JudulForm = New Label()
        grb_Pencairan = New GroupBox()
        btn_HapusPencairan = New Button()
        btn_EditPencairan = New Button()
        btn_InputPencairan = New Button()
        dgv_DetailBayar = New DataGridView()
        Nomor_ID_Bayar = New DataGridViewTextBoxColumn()
        Tanggal_Bayar = New DataGridViewTextBoxColumn()
        Referensi_ = New DataGridViewTextBoxColumn()
        Nominal_Bayar = New DataGridViewTextBoxColumn()
        Potongan_PPh = New DataGridViewTextBoxColumn()
        Keterangan_Bayar = New DataGridViewTextBoxColumn()
        Nomor_JV_Bayar = New DataGridViewTextBoxColumn()
        btn_LihatJurnal = New Button()
        Label2 = New Label()
        cmb_JenisRelasi = New ComboBox()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        grb_Pencairan.SuspendLayout()
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' cmb_Customer
        ' 
        cmb_Customer.Font = New Font("Microsoft Sans Serif", 8F)
        cmb_Customer.FormattingEnabled = True
        cmb_Customer.ItemHeight = 13
        cmb_Customer.Location = New Point(438, 106)
        cmb_Customer.Margin = New Padding(4, 3, 4, 3)
        cmb_Customer.Name = "cmb_Customer"
        cmb_Customer.Size = New Size(200, 21)
        cmb_Customer.TabIndex = 10198
        ' 
        ' lbl_Supplier
        ' 
        lbl_Supplier.AutoSize = True
        lbl_Supplier.Location = New Point(434, 85)
        lbl_Supplier.Margin = New Padding(4, 0, 4, 0)
        lbl_Supplier.Name = "lbl_Supplier"
        lbl_Supplier.Size = New Size(65, 15)
        lbl_Supplier.TabIndex = 10199
        lbl_Supplier.Text = "Customer :"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(286, 85)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(74, 13)
        Label15.TabIndex = 10197
        Label15.Text = "Jenis Produk :"
        ' 
        ' cmb_JenisProduk_Induk
        ' 
        cmb_JenisProduk_Induk.FormattingEnabled = True
        cmb_JenisProduk_Induk.Location = New Point(289, 106)
        cmb_JenisProduk_Induk.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisProduk_Induk.Name = "cmb_JenisProduk_Induk"
        cmb_JenisProduk_Induk.Size = New Size(125, 23)
        cmb_JenisProduk_Induk.TabIndex = 10196
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(658, 85)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(75, 13)
        Label1.TabIndex = 10201
        Label1.Text = "Jatuh Tempo :"
        ' 
        ' cmb_JatuhTempo
        ' 
        cmb_JatuhTempo.FormattingEnabled = True
        cmb_JatuhTempo.Location = New Point(662, 106)
        cmb_JatuhTempo.Margin = New Padding(4, 3, 4, 3)
        cmb_JatuhTempo.Name = "cmb_JatuhTempo"
        cmb_JatuhTempo.Size = New Size(83, 23)
        cmb_JatuhTempo.TabIndex = 10200
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10191
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_BPPU, Nomor_Penjualan, Jenis_Invoice, Jenis_Produk, Angka_Invoice, Nomor_Invoice, Nomor_Faktur_Pajak, Tanggal_Invoice, Masa_Jatuh_Tempo, Nomor_SJ_BAST, Tanggal_SJ_BAST, Nomor_PO, Tanggal_PO, Kode_Project, Kode_Customer, Nama_Customer, Jumlah_Harga, Diskon_Rp, Dasar_Pengenaan_Pajak, Jenis_PPN, PPN_, Tagihan_Bruto, Retur_, Jumlah_Piutang_Usaha, Sisa_Piutang_Usaha, Jenis_PPh, PPh_Terutang, PPh_Ditanggung, PPh_Dipotong, Tagihan_Netto, Keterangan_Jatuh_Tempo, Jumlah_Bayar, Sisa_Tagihan, Biaya_Administrasi_Bank, Catatan_, Nomor_JV_Penjualan})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 493)
        DataTabelUtama.TabIndex = 10192
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.Padding = New Padding(0, 0, 3, 0)
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_Urut.HeaderText = "No."
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 45
        ' 
        ' Nomor_BPPU
        ' 
        Nomor_BPPU.HeaderText = "Nomor BPPU"
        Nomor_BPPU.Name = "Nomor_BPPU"
        Nomor_BPPU.ReadOnly = True
        Nomor_BPPU.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPPU.Visible = False
        ' 
        ' Nomor_Penjualan
        ' 
        Nomor_Penjualan.HeaderText = "Nomor Penjualan"
        Nomor_Penjualan.Name = "Nomor_Penjualan"
        Nomor_Penjualan.ReadOnly = True
        Nomor_Penjualan.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Penjualan.Visible = False
        ' 
        ' Jenis_Invoice
        ' 
        Jenis_Invoice.HeaderText = "Jenis Invoice"
        Jenis_Invoice.Name = "Jenis_Invoice"
        Jenis_Invoice.ReadOnly = True
        Jenis_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Invoice.Visible = False
        Jenis_Invoice.Width = 72
        ' 
        ' Jenis_Produk
        ' 
        Jenis_Produk.HeaderText = "Jenis Produk"
        Jenis_Produk.Name = "Jenis_Produk"
        Jenis_Produk.ReadOnly = True
        Jenis_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Produk.Visible = False
        Jenis_Produk.Width = 99
        ' 
        ' Angka_Invoice
        ' 
        Angka_Invoice.HeaderText = "Angka Invoice"
        Angka_Invoice.Name = "Angka_Invoice"
        Angka_Invoice.ReadOnly = True
        Angka_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Angka_Invoice.Visible = False
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.ReadOnly = True
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 81
        ' 
        ' Nomor_Faktur_Pajak
        ' 
        Nomor_Faktur_Pajak.HeaderText = "Nomor FP"
        Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Nomor_Faktur_Pajak.ReadOnly = True
        Nomor_Faktur_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.ReadOnly = True
        Tanggal_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Invoice.Width = 63
        ' 
        ' Masa_Jatuh_Tempo
        ' 
        Masa_Jatuh_Tempo.HeaderText = "Tgl/Masa Jatuh Tempo"
        Masa_Jatuh_Tempo.Name = "Masa_Jatuh_Tempo"
        Masa_Jatuh_Tempo.ReadOnly = True
        Masa_Jatuh_Tempo.SortMode = DataGridViewColumnSortMode.NotSortable
        Masa_Jatuh_Tempo.Width = 63
        ' 
        ' Nomor_SJ_BAST
        ' 
        Nomor_SJ_BAST.HeaderText = "Nomor SJ/BAST"
        Nomor_SJ_BAST.Name = "Nomor_SJ_BAST"
        Nomor_SJ_BAST.ReadOnly = True
        Nomor_SJ_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_SJ_BAST.Width = 99
        ' 
        ' Tanggal_SJ_BAST
        ' 
        Tanggal_SJ_BAST.HeaderText = "Tanggal SJ/BAST"
        Tanggal_SJ_BAST.Name = "Tanggal_SJ_BAST"
        Tanggal_SJ_BAST.ReadOnly = True
        Tanggal_SJ_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_SJ_BAST.Width = 99
        ' 
        ' Nomor_PO
        ' 
        Nomor_PO.HeaderText = "Nomor PO"
        Nomor_PO.Name = "Nomor_PO"
        Nomor_PO.ReadOnly = True
        Nomor_PO.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_PO.Visible = False
        Nomor_PO.Width = 99
        ' 
        ' Tanggal_PO
        ' 
        Tanggal_PO.HeaderText = "Tanggal PO"
        Tanggal_PO.Name = "Tanggal_PO"
        Tanggal_PO.ReadOnly = True
        Tanggal_PO.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_PO.Visible = False
        Tanggal_PO.Width = 99
        ' 
        ' Kode_Project
        ' 
        Kode_Project.HeaderText = "Kode Project"
        Kode_Project.Name = "Kode_Project"
        Kode_Project.ReadOnly = True
        Kode_Project.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Project.Width = 99
        ' 
        ' Kode_Customer
        ' 
        Kode_Customer.HeaderText = "Kode Customer"
        Kode_Customer.Name = "Kode_Customer"
        Kode_Customer.ReadOnly = True
        Kode_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Customer.Visible = False
        ' 
        ' Nama_Customer
        ' 
        Nama_Customer.HeaderText = "Nama Customer"
        Nama_Customer.Name = "Nama_Customer"
        Nama_Customer.ReadOnly = True
        Nama_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Customer.Width = 150
        ' 
        ' Jumlah_Harga
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Jumlah_Harga.DefaultCellStyle = DataGridViewCellStyle2
        Jumlah_Harga.HeaderText = "Jumlah Harga"
        Jumlah_Harga.Name = "Jumlah_Harga"
        Jumlah_Harga.ReadOnly = True
        Jumlah_Harga.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Harga.Width = 81
        ' 
        ' Diskon_Rp
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Diskon_Rp.DefaultCellStyle = DataGridViewCellStyle3
        Diskon_Rp.HeaderText = "Diskon"
        Diskon_Rp.Name = "Diskon_Rp"
        Diskon_Rp.ReadOnly = True
        Diskon_Rp.SortMode = DataGridViewColumnSortMode.NotSortable
        Diskon_Rp.Width = 81
        ' 
        ' Dasar_Pengenaan_Pajak
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Dasar_Pengenaan_Pajak.DefaultCellStyle = DataGridViewCellStyle4
        Dasar_Pengenaan_Pajak.HeaderText = "DPP"
        Dasar_Pengenaan_Pajak.Name = "Dasar_Pengenaan_Pajak"
        Dasar_Pengenaan_Pajak.ReadOnly = True
        Dasar_Pengenaan_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Dasar_Pengenaan_Pajak.Width = 81
        ' 
        ' Jenis_PPN
        ' 
        Jenis_PPN.HeaderText = "Jenis PPN"
        Jenis_PPN.Name = "Jenis_PPN"
        Jenis_PPN.ReadOnly = True
        Jenis_PPN.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_PPN.Visible = False
        ' 
        ' PPN_
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        PPN_.DefaultCellStyle = DataGridViewCellStyle5
        PPN_.HeaderText = "PPN"
        PPN_.Name = "PPN_"
        PPN_.ReadOnly = True
        PPN_.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Tagihan_Bruto
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Tagihan_Bruto.DefaultCellStyle = DataGridViewCellStyle6
        Tagihan_Bruto.HeaderText = "Tagihan Bruto"
        Tagihan_Bruto.Name = "Tagihan_Bruto"
        Tagihan_Bruto.ReadOnly = True
        Tagihan_Bruto.SortMode = DataGridViewColumnSortMode.NotSortable
        Tagihan_Bruto.Visible = False
        Tagihan_Bruto.Width = 81
        ' 
        ' Retur_
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Retur_.DefaultCellStyle = DataGridViewCellStyle7
        Retur_.HeaderText = "Retur"
        Retur_.Name = "Retur_"
        Retur_.ReadOnly = True
        Retur_.SortMode = DataGridViewColumnSortMode.NotSortable
        Retur_.Width = 81
        ' 
        ' Jumlah_Piutang_Usaha
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Jumlah_Piutang_Usaha.DefaultCellStyle = DataGridViewCellStyle8
        Jumlah_Piutang_Usaha.HeaderText = "Jumlah Piutang Usaha"
        Jumlah_Piutang_Usaha.Name = "Jumlah_Piutang_Usaha"
        Jumlah_Piutang_Usaha.ReadOnly = True
        Jumlah_Piutang_Usaha.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Piutang_Usaha.Width = 81
        ' 
        ' Sisa_Piutang_Usaha
        ' 
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        Sisa_Piutang_Usaha.DefaultCellStyle = DataGridViewCellStyle9
        Sisa_Piutang_Usaha.HeaderText = "Sisa Piutang Usaha"
        Sisa_Piutang_Usaha.Name = "Sisa_Piutang_Usaha"
        Sisa_Piutang_Usaha.ReadOnly = True
        Sisa_Piutang_Usaha.SortMode = DataGridViewColumnSortMode.NotSortable
        Sisa_Piutang_Usaha.Width = 81
        ' 
        ' Jenis_PPh
        ' 
        Jenis_PPh.HeaderText = "Jenis PPh"
        Jenis_PPh.Name = "Jenis_PPh"
        Jenis_PPh.ReadOnly = True
        Jenis_PPh.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_PPh.Visible = False
        ' 
        ' PPh_Terutang
        ' 
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        PPh_Terutang.DefaultCellStyle = DataGridViewCellStyle10
        PPh_Terutang.HeaderText = "PPh Terutang"
        PPh_Terutang.Name = "PPh_Terutang"
        PPh_Terutang.ReadOnly = True
        PPh_Terutang.SortMode = DataGridViewColumnSortMode.NotSortable
        PPh_Terutang.Visible = False
        PPh_Terutang.Width = 81
        ' 
        ' PPh_Ditanggung
        ' 
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        PPh_Ditanggung.DefaultCellStyle = DataGridViewCellStyle11
        PPh_Ditanggung.HeaderText = "PPh Ditanggung"
        PPh_Ditanggung.Name = "PPh_Ditanggung"
        PPh_Ditanggung.ReadOnly = True
        PPh_Ditanggung.Width = 81
        ' 
        ' PPh_Dipotong
        ' 
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N0"
        PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle12
        PPh_Dipotong.HeaderText = "PPh Dipotong"
        PPh_Dipotong.Name = "PPh_Dipotong"
        PPh_Dipotong.ReadOnly = True
        PPh_Dipotong.SortMode = DataGridViewColumnSortMode.NotSortable
        PPh_Dipotong.Width = 81
        ' 
        ' Tagihan_Netto
        ' 
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N0"
        Tagihan_Netto.DefaultCellStyle = DataGridViewCellStyle13
        Tagihan_Netto.HeaderText = "Total Tagihan"
        Tagihan_Netto.Name = "Tagihan_Netto"
        Tagihan_Netto.ReadOnly = True
        Tagihan_Netto.SortMode = DataGridViewColumnSortMode.NotSortable
        Tagihan_Netto.Visible = False
        Tagihan_Netto.Width = 81
        ' 
        ' Keterangan_Jatuh_Tempo
        ' 
        DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleCenter
        Keterangan_Jatuh_Tempo.DefaultCellStyle = DataGridViewCellStyle14
        Keterangan_Jatuh_Tempo.HeaderText = "Jatuh Tempo"
        Keterangan_Jatuh_Tempo.Name = "Keterangan_Jatuh_Tempo"
        Keterangan_Jatuh_Tempo.ReadOnly = True
        Keterangan_Jatuh_Tempo.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_Jatuh_Tempo.Width = 45
        ' 
        ' Jumlah_Bayar
        ' 
        DataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "N0"
        Jumlah_Bayar.DefaultCellStyle = DataGridViewCellStyle15
        Jumlah_Bayar.HeaderText = "Jumlah Bayar"
        Jumlah_Bayar.Name = "Jumlah_Bayar"
        Jumlah_Bayar.ReadOnly = True
        Jumlah_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar.Width = 81
        ' 
        ' Sisa_Tagihan
        ' 
        DataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Format = "N0"
        Sisa_Tagihan.DefaultCellStyle = DataGridViewCellStyle16
        Sisa_Tagihan.HeaderText = "Sisa Tagihan"
        Sisa_Tagihan.Name = "Sisa_Tagihan"
        Sisa_Tagihan.ReadOnly = True
        Sisa_Tagihan.SortMode = DataGridViewColumnSortMode.NotSortable
        Sisa_Tagihan.Width = 81
        ' 
        ' Biaya_Administrasi_Bank
        ' 
        DataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "N0"
        Biaya_Administrasi_Bank.DefaultCellStyle = DataGridViewCellStyle17
        Biaya_Administrasi_Bank.HeaderText = "Biaya Adm Bank"
        Biaya_Administrasi_Bank.Name = "Biaya_Administrasi_Bank"
        Biaya_Administrasi_Bank.ReadOnly = True
        Biaya_Administrasi_Bank.SortMode = DataGridViewColumnSortMode.NotSortable
        Biaya_Administrasi_Bank.Width = 63
        ' 
        ' Catatan_
        ' 
        Catatan_.HeaderText = "Catatan"
        Catatan_.Name = "Catatan_"
        Catatan_.ReadOnly = True
        Catatan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Catatan_.Width = 210
        ' 
        ' Nomor_JV_Penjualan
        ' 
        Nomor_JV_Penjualan.HeaderText = "Nomor JV"
        Nomor_JV_Penjualan.Name = "Nomor_JV_Penjualan"
        Nomor_JV_Penjualan.ReadOnly = True
        Nomor_JV_Penjualan.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV_Penjualan.Visible = False
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(474, 32)
        lbl_JudulForm.TabIndex = 10193
        lbl_JudulForm.Text = "Buku Pengawasan Piutang Usaha"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' grb_Pencairan
        ' 
        grb_Pencairan.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_Pencairan.Controls.Add(btn_HapusPencairan)
        grb_Pencairan.Controls.Add(btn_EditPencairan)
        grb_Pencairan.Controls.Add(btn_InputPencairan)
        grb_Pencairan.Controls.Add(dgv_DetailBayar)
        grb_Pencairan.Location = New Point(1015, 5)
        grb_Pencairan.Margin = New Padding(4, 3, 4, 3)
        grb_Pencairan.Name = "grb_Pencairan"
        grb_Pencairan.Padding = New Padding(4, 3, 4, 3)
        grb_Pencairan.Size = New Size(492, 134)
        grb_Pencairan.TabIndex = 10202
        grb_Pencairan.TabStop = False
        grb_Pencairan.Text = "Pencairan"
        ' 
        ' btn_HapusPencairan
        ' 
        btn_HapusPencairan.Enabled = False
        btn_HapusPencairan.Location = New Point(13, 93)
        btn_HapusPencairan.Margin = New Padding(4, 3, 4, 3)
        btn_HapusPencairan.Name = "btn_HapusPencairan"
        btn_HapusPencairan.Size = New Size(94, 32)
        btn_HapusPencairan.TabIndex = 10062
        btn_HapusPencairan.Text = "Hapus"
        btn_HapusPencairan.UseVisualStyleBackColor = True
        ' 
        ' btn_EditPencairan
        ' 
        btn_EditPencairan.Enabled = False
        btn_EditPencairan.Location = New Point(13, 58)
        btn_EditPencairan.Margin = New Padding(4, 3, 4, 3)
        btn_EditPencairan.Name = "btn_EditPencairan"
        btn_EditPencairan.Size = New Size(94, 32)
        btn_EditPencairan.TabIndex = 10061
        btn_EditPencairan.Text = "Edit"
        btn_EditPencairan.UseVisualStyleBackColor = True
        ' 
        ' btn_InputPencairan
        ' 
        btn_InputPencairan.Location = New Point(13, 22)
        btn_InputPencairan.Margin = New Padding(4, 3, 4, 3)
        btn_InputPencairan.Name = "btn_InputPencairan"
        btn_InputPencairan.Size = New Size(94, 32)
        btn_InputPencairan.TabIndex = 10048
        btn_InputPencairan.Text = "Cair"
        btn_InputPencairan.UseVisualStyleBackColor = True
        ' 
        ' dgv_DetailBayar
        ' 
        dgv_DetailBayar.AllowUserToAddRows = False
        dgv_DetailBayar.AllowUserToDeleteRows = False
        dgv_DetailBayar.AllowUserToResizeRows = False
        dgv_DetailBayar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        dgv_DetailBayar.BorderStyle = BorderStyle.None
        dgv_DetailBayar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_DetailBayar.Columns.AddRange(New DataGridViewColumn() {Nomor_ID_Bayar, Tanggal_Bayar, Referensi_, Nominal_Bayar, Potongan_PPh, Keterangan_Bayar, Nomor_JV_Bayar})
        dgv_DetailBayar.Location = New Point(119, 22)
        dgv_DetailBayar.Margin = New Padding(4, 3, 4, 3)
        dgv_DetailBayar.MultiSelect = False
        dgv_DetailBayar.Name = "dgv_DetailBayar"
        dgv_DetailBayar.ReadOnly = True
        dgv_DetailBayar.RowHeadersVisible = False
        dgv_DetailBayar.RowHeadersWidth = 33
        dgv_DetailBayar.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_DetailBayar.Size = New Size(359, 104)
        dgv_DetailBayar.TabIndex = 10040
        ' 
        ' Nomor_ID_Bayar
        ' 
        DataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "N0"
        Nomor_ID_Bayar.DefaultCellStyle = DataGridViewCellStyle18
        Nomor_ID_Bayar.HeaderText = "Nomor ID Bayar"
        Nomor_ID_Bayar.Name = "Nomor_ID_Bayar"
        Nomor_ID_Bayar.ReadOnly = True
        Nomor_ID_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID_Bayar.Visible = False
        Nomor_ID_Bayar.Width = 81
        ' 
        ' Tanggal_Bayar
        ' 
        DataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Bayar.DefaultCellStyle = DataGridViewCellStyle19
        Tanggal_Bayar.HeaderText = "Tgl. Bayar"
        Tanggal_Bayar.Name = "Tanggal_Bayar"
        Tanggal_Bayar.ReadOnly = True
        Tanggal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Bayar.Width = 81
        ' 
        ' Referensi_
        ' 
        DataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleLeft
        Referensi_.DefaultCellStyle = DataGridViewCellStyle20
        Referensi_.HeaderText = "Referensi"
        Referensi_.Name = "Referensi_"
        Referensi_.ReadOnly = True
        Referensi_.SortMode = DataGridViewColumnSortMode.NotSortable
        Referensi_.Width = 123
        ' 
        ' Nominal_Bayar
        ' 
        DataGridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle21.Format = "N0"
        DataGridViewCellStyle21.NullValue = Nothing
        Nominal_Bayar.DefaultCellStyle = DataGridViewCellStyle21
        Nominal_Bayar.HeaderText = "Jumlah Bayar"
        Nominal_Bayar.Name = "Nominal_Bayar"
        Nominal_Bayar.ReadOnly = True
        Nominal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Nominal_Bayar.Width = 99
        ' 
        ' Potongan_PPh
        ' 
        DataGridViewCellStyle22.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle22.Format = "N0"
        Potongan_PPh.DefaultCellStyle = DataGridViewCellStyle22
        Potongan_PPh.HeaderText = "Potongan PPh"
        Potongan_PPh.Name = "Potongan_PPh"
        Potongan_PPh.ReadOnly = True
        Potongan_PPh.SortMode = DataGridViewColumnSortMode.NotSortable
        Potongan_PPh.Width = 81
        ' 
        ' Keterangan_Bayar
        ' 
        Keterangan_Bayar.HeaderText = "Keterangan Bayar"
        Keterangan_Bayar.Name = "Keterangan_Bayar"
        Keterangan_Bayar.ReadOnly = True
        Keterangan_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_Bayar.Visible = False
        Keterangan_Bayar.Width = 33
        ' 
        ' Nomor_JV_Bayar
        ' 
        Nomor_JV_Bayar.HeaderText = "Nomor JV Bayar"
        Nomor_JV_Bayar.Name = "Nomor_JV_Bayar"
        Nomor_JV_Bayar.ReadOnly = True
        Nomor_JV_Bayar.Visible = False
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(847, 12)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 39)
        btn_LihatJurnal.TabIndex = 10203
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(136, 85)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(63, 13)
        Label2.TabIndex = 10230
        Label2.Text = "Jenis Relasi"
        ' 
        ' cmb_JenisRelasi
        ' 
        cmb_JenisRelasi.FormattingEnabled = True
        cmb_JenisRelasi.Location = New Point(140, 106)
        cmb_JenisRelasi.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisRelasi.Name = "cmb_JenisRelasi"
        cmb_JenisRelasi.Size = New Size(125, 23)
        cmb_JenisRelasi.TabIndex = 10229
        ' 
        ' btn_Export
        ' 
        btn_Export.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Export.Location = New Point(773, 77)
        btn_Export.Margin = New Padding(4, 3, 4, 3)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(115, 52)
        btn_Export.TabIndex = 10231
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' X_frm_BukuPengawasanPiutangUsaha_X
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 682)
        Controls.Add(btn_Export)
        Controls.Add(Label2)
        Controls.Add(cmb_JenisRelasi)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(grb_Pencairan)
        Controls.Add(cmb_Customer)
        Controls.Add(lbl_Supplier)
        Controls.Add(Label15)
        Controls.Add(cmb_JenisProduk_Induk)
        Controls.Add(Label1)
        Controls.Add(cmb_JatuhTempo)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(lbl_JudulForm)
        Margin = New Padding(4, 3, 4, 3)
        Name = "X_frm_BukuPengawasanPiutangUsaha_X"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Piutang Usaha"
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        grb_Pencairan.ResumeLayout(False)
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents cmb_Customer As ComboBox
    Friend WithEvents lbl_Supplier As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cmb_JenisProduk_Induk As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_JatuhTempo As ComboBox
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents grb_Pencairan As GroupBox
    Friend WithEvents btn_HapusPencairan As Button
    Friend WithEvents btn_EditPencairan As Button
    Friend WithEvents btn_InputPencairan As Button
    Friend WithEvents dgv_DetailBayar As DataGridView
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cmb_JenisRelasi As ComboBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPPU As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Penjualan As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Angka_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Masa_Jatuh_Tempo As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_PO As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Harga As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Rp As DataGridViewTextBoxColumn
    Friend WithEvents Dasar_Pengenaan_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_PPN As DataGridViewTextBoxColumn
    Friend WithEvents PPN_ As DataGridViewTextBoxColumn
    Friend WithEvents Tagihan_Bruto As DataGridViewTextBoxColumn
    Friend WithEvents Retur_ As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Piutang_Usaha As DataGridViewTextBoxColumn
    Friend WithEvents Sisa_Piutang_Usaha As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_PPh As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Terutang As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Ditanggung As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Dipotong As DataGridViewTextBoxColumn
    Friend WithEvents Tagihan_Netto As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_Jatuh_Tempo As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Sisa_Tagihan As DataGridViewTextBoxColumn
    Friend WithEvents Biaya_Administrasi_Bank As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Penjualan As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Referensi_ As DataGridViewTextBoxColumn
    Friend WithEvents Nominal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Potongan_PPh As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents btn_Export As Button
End Class
