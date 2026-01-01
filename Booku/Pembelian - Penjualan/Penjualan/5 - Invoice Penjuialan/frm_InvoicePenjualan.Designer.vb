<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_InvoicePenjualan
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
        lbl_JudulForm = New Label()
        btn_Edit = New Button()
        btn_Hapus = New Button()
        btn_Tambah = New Button()
        btn_Cetak = New Button()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Jenis_Invoice = New DataGridViewTextBoxColumn()
        Jenis_Produk = New DataGridViewTextBoxColumn()
        Angka_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Penjualan = New DataGridViewTextBoxColumn()
        N_P = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Tanggal_Pembetulan = New DataGridViewTextBoxColumn()
        Tanggal_Lapor = New DataGridViewTextBoxColumn()
        Jatuh_Tempo = New DataGridViewTextBoxColumn()
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
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Jenis_PPN = New DataGridViewTextBoxColumn()
        PPN_ = New DataGridViewTextBoxColumn()
        PPh_Dipotong = New DataGridViewTextBoxColumn()
        Tagihan_Kotor = New DataGridViewTextBoxColumn()
        Retur_DPP = New DataGridViewTextBoxColumn()
        Retur_PPN = New DataGridViewTextBoxColumn()
        Retur_ = New DataGridViewTextBoxColumn()
        Asset_ = New DataGridViewTextBoxColumn()
        Catatan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        btn_Pratinjau = New Button()
        btn_LihatJurnal = New Button()
        cmb_Customer = New ComboBox()
        lbl_Supplier = New Label()
        btn_Pembetulan = New Button()
        cmb_JenisTampilan = New ComboBox()
        Label1 = New Label()
        btn_LihatInvoice = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(257, 32)
        lbl_JudulForm.TabIndex = 10055
        lbl_JudulForm.Text = "Invoice Penjualan"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Edit.Location = New Point(805, 98)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10053
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Hapus.Location = New Point(909, 98)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10052
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Tambah.Location = New Point(701, 98)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 10051
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Cetak
        ' 
        btn_Cetak.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Cetak.Location = New Point(995, 12)
        btn_Cetak.Margin = New Padding(4, 3, 4, 3)
        btn_Cetak.Name = "btn_Cetak"
        btn_Cetak.Size = New Size(97, 58)
        btn_Cetak.TabIndex = 10056
        btn_Cetak.Text = "Cetak"
        btn_Cetak.UseVisualStyleBackColor = True
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10050
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Jenis_Invoice, Jenis_Produk, Angka_Invoice, Nomor_Invoice, Nomor_Penjualan, N_P, Tanggal_Invoice, Tanggal_Pembetulan, Tanggal_Lapor, Jatuh_Tempo, Nomor_SJ_BAST, Tanggal_SJ_BAST, Nomor_PO, Tanggal_PO, Kode_Project, Kode_Customer, Nama_Customer, Jumlah_Harga, Diskon_Rp, Dasar_Pengenaan_Pajak, Nomor_Faktur_Pajak, Jenis_PPN, PPN_, PPh_Dipotong, Tagihan_Kotor, Retur_DPP, Retur_PPN, Retur_, Asset_, Catatan_, Nomor_JV})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 493)
        DataTabelUtama.TabIndex = 10054
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        DataGridViewCellStyle11.Padding = New Padding(0, 0, 3, 0)
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle11
        Nomor_Urut.HeaderText = "No."
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 45
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
        Nomor_Invoice.Width = 99
        ' 
        ' Nomor_Penjualan
        ' 
        Nomor_Penjualan.HeaderText = "Nomor Penjualan"
        Nomor_Penjualan.Name = "Nomor_Penjualan"
        Nomor_Penjualan.ReadOnly = True
        Nomor_Penjualan.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Penjualan.Visible = False
        ' 
        ' N_P
        ' 
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter
        N_P.DefaultCellStyle = DataGridViewCellStyle12
        N_P.HeaderText = "N/P"
        N_P.Name = "N_P"
        N_P.ReadOnly = True
        N_P.SortMode = DataGridViewColumnSortMode.NotSortable
        N_P.Visible = False
        N_P.Width = 33
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.ReadOnly = True
        Tanggal_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Invoice.Width = 63
        ' 
        ' Tanggal_Pembetulan
        ' 
        Tanggal_Pembetulan.HeaderText = "Tanggal Pembetulan"
        Tanggal_Pembetulan.Name = "Tanggal_Pembetulan"
        Tanggal_Pembetulan.ReadOnly = True
        Tanggal_Pembetulan.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Pembetulan.Width = 63
        ' 
        ' Tanggal_Lapor
        ' 
        Tanggal_Lapor.HeaderText = "Tanggal Lapor"
        Tanggal_Lapor.Name = "Tanggal_Lapor"
        Tanggal_Lapor.ReadOnly = True
        Tanggal_Lapor.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Lapor.Visible = False
        Tanggal_Lapor.Width = 63
        ' 
        ' Jatuh_Tempo
        ' 
        Jatuh_Tempo.HeaderText = "Jatuh Tempo"
        Jatuh_Tempo.Name = "Jatuh_Tempo"
        Jatuh_Tempo.ReadOnly = True
        Jatuh_Tempo.SortMode = DataGridViewColumnSortMode.NotSortable
        Jatuh_Tempo.Width = 63
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
        Nomor_PO.Width = 99
        ' 
        ' Tanggal_PO
        ' 
        Tanggal_PO.HeaderText = "Tanggal PO"
        Tanggal_PO.Name = "Tanggal_PO"
        Tanggal_PO.ReadOnly = True
        Tanggal_PO.SortMode = DataGridViewColumnSortMode.NotSortable
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
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N0"
        Jumlah_Harga.DefaultCellStyle = DataGridViewCellStyle13
        Jumlah_Harga.HeaderText = "Jumlah Harga"
        Jumlah_Harga.Name = "Jumlah_Harga"
        Jumlah_Harga.ReadOnly = True
        Jumlah_Harga.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Harga.Width = 81
        ' 
        ' Diskon_Rp
        ' 
        DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N0"
        Diskon_Rp.DefaultCellStyle = DataGridViewCellStyle14
        Diskon_Rp.HeaderText = "Diskon"
        Diskon_Rp.Name = "Diskon_Rp"
        Diskon_Rp.ReadOnly = True
        Diskon_Rp.SortMode = DataGridViewColumnSortMode.NotSortable
        Diskon_Rp.Width = 81
        ' 
        ' Dasar_Pengenaan_Pajak
        ' 
        DataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "N0"
        Dasar_Pengenaan_Pajak.DefaultCellStyle = DataGridViewCellStyle15
        Dasar_Pengenaan_Pajak.HeaderText = "Dasar Pengenaan Pajak"
        Dasar_Pengenaan_Pajak.Name = "Dasar_Pengenaan_Pajak"
        Dasar_Pengenaan_Pajak.ReadOnly = True
        Dasar_Pengenaan_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Dasar_Pengenaan_Pajak.Width = 81
        ' 
        ' Nomor_Faktur_Pajak
        ' 
        Nomor_Faktur_Pajak.HeaderText = "Nomor Faktur Pajak"
        Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Nomor_Faktur_Pajak.ReadOnly = True
        Nomor_Faktur_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
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
        DataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Format = "N0"
        PPN_.DefaultCellStyle = DataGridViewCellStyle16
        PPN_.HeaderText = "PPN"
        PPN_.Name = "PPN_"
        PPN_.ReadOnly = True
        PPN_.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PPh_Dipotong
        ' 
        DataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "N0"
        PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle17
        PPh_Dipotong.HeaderText = "PPh Dipotong"
        PPh_Dipotong.Name = "PPh_Dipotong"
        PPh_Dipotong.ReadOnly = True
        PPh_Dipotong.SortMode = DataGridViewColumnSortMode.NotSortable
        PPh_Dipotong.Width = 81
        ' 
        ' Tagihan_Kotor
        ' 
        DataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "N0"
        Tagihan_Kotor.DefaultCellStyle = DataGridViewCellStyle18
        Tagihan_Kotor.HeaderText = "Jumlah Tagihan"
        Tagihan_Kotor.Name = "Tagihan_Kotor"
        Tagihan_Kotor.ReadOnly = True
        Tagihan_Kotor.SortMode = DataGridViewColumnSortMode.NotSortable
        Tagihan_Kotor.Width = 81
        ' 
        ' Retur_DPP
        ' 
        Retur_DPP.HeaderText = "Retur DPP"
        Retur_DPP.Name = "Retur_DPP"
        Retur_DPP.ReadOnly = True
        Retur_DPP.SortMode = DataGridViewColumnSortMode.NotSortable
        Retur_DPP.Visible = False
        ' 
        ' Retur_PPN
        ' 
        Retur_PPN.HeaderText = "Retur PPN"
        Retur_PPN.Name = "Retur_PPN"
        Retur_PPN.ReadOnly = True
        Retur_PPN.SortMode = DataGridViewColumnSortMode.NotSortable
        Retur_PPN.Visible = False
        ' 
        ' Retur_
        ' 
        DataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle19.Format = "N0"
        Retur_.DefaultCellStyle = DataGridViewCellStyle19
        Retur_.HeaderText = "Retur"
        Retur_.Name = "Retur_"
        Retur_.ReadOnly = True
        Retur_.SortMode = DataGridViewColumnSortMode.NotSortable
        Retur_.Visible = False
        ' 
        ' Asset_
        ' 
        Asset_.HeaderText = "Asset"
        Asset_.Name = "Asset_"
        Asset_.ReadOnly = True
        Asset_.SortMode = DataGridViewColumnSortMode.NotSortable
        Asset_.Visible = False
        Asset_.Width = 33
        ' 
        ' Catatan_
        ' 
        DataGridViewCellStyle20.NullValue = Nothing
        Catatan_.DefaultCellStyle = DataGridViewCellStyle20
        Catatan_.HeaderText = "Catatan"
        Catatan_.Name = "Catatan_"
        Catatan_.ReadOnly = True
        Catatan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Catatan_.Width = 210
        ' 
        ' Nomor_JV
        ' 
        Nomor_JV.HeaderText = "Nomor JV"
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV.Visible = False
        ' 
        ' btn_Pratinjau
        ' 
        btn_Pratinjau.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Pratinjau.Location = New Point(892, 12)
        btn_Pratinjau.Margin = New Padding(4, 3, 4, 3)
        btn_Pratinjau.Name = "btn_Pratinjau"
        btn_Pratinjau.Size = New Size(97, 58)
        btn_Pratinjau.TabIndex = 10057
        btn_Pratinjau.Text = "Pratinjau"
        btn_Pratinjau.UseVisualStyleBackColor = True
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(776, 12)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(108, 58)
        btn_LihatJurnal.TabIndex = 10083
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' cmb_Customer
        ' 
        cmb_Customer.Font = New Font("Microsoft Sans Serif", 8F)
        cmb_Customer.FormattingEnabled = True
        cmb_Customer.ItemHeight = 13
        cmb_Customer.Location = New Point(303, 109)
        cmb_Customer.Margin = New Padding(4, 3, 4, 3)
        cmb_Customer.Name = "cmb_Customer"
        cmb_Customer.Size = New Size(200, 21)
        cmb_Customer.TabIndex = 10189
        ' 
        ' lbl_Supplier
        ' 
        lbl_Supplier.AutoSize = True
        lbl_Supplier.Location = New Point(299, 89)
        lbl_Supplier.Margin = New Padding(4, 0, 4, 0)
        lbl_Supplier.Name = "lbl_Supplier"
        lbl_Supplier.Size = New Size(65, 15)
        lbl_Supplier.TabIndex = 10190
        lbl_Supplier.Text = "Customer :"
        ' 
        ' btn_Pembetulan
        ' 
        btn_Pembetulan.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Pembetulan.Location = New Point(1012, 98)
        btn_Pembetulan.Margin = New Padding(4, 3, 4, 3)
        btn_Pembetulan.Name = "btn_Pembetulan"
        btn_Pembetulan.Size = New Size(97, 40)
        btn_Pembetulan.TabIndex = 10191
        btn_Pembetulan.Text = "Pembetulan"
        btn_Pembetulan.UseVisualStyleBackColor = True
        ' 
        ' cmb_JenisTampilan
        ' 
        cmb_JenisTampilan.Font = New Font("Microsoft Sans Serif", 8F)
        cmb_JenisTampilan.FormattingEnabled = True
        cmb_JenisTampilan.ItemHeight = 13
        cmb_JenisTampilan.Location = New Point(150, 109)
        cmb_JenisTampilan.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisTampilan.Name = "cmb_JenisTampilan"
        cmb_JenisTampilan.Size = New Size(100, 21)
        cmb_JenisTampilan.TabIndex = 10192
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(147, 89)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(62, 15)
        Label1.TabIndex = 10193
        Label1.Text = "Tampilan :"
        ' 
        ' btn_LihatInvoice
        ' 
        btn_LihatInvoice.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_LihatInvoice.Location = New Point(548, 98)
        btn_LihatInvoice.Margin = New Padding(4, 3, 4, 3)
        btn_LihatInvoice.Name = "btn_LihatInvoice"
        btn_LihatInvoice.Size = New Size(120, 40)
        btn_LihatInvoice.TabIndex = 10194
        btn_LihatInvoice.Text = "Lihat Invoice"
        btn_LihatInvoice.UseVisualStyleBackColor = True
        ' 
        ' frm_InvoicePenjualan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 682)
        Controls.Add(btn_LihatInvoice)
        Controls.Add(cmb_JenisTampilan)
        Controls.Add(Label1)
        Controls.Add(btn_Pembetulan)
        Controls.Add(cmb_Customer)
        Controls.Add(lbl_Supplier)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        Controls.Add(btn_Cetak)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(btn_Pratinjau)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_InvoicePenjualan"
        StartPosition = FormStartPosition.CenterParent
        Text = "Invoice Penjualan"
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents btn_Cetak As Button
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_Pratinjau As Button
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents cmb_Customer As ComboBox
    Friend WithEvents lbl_Supplier As Label
    Friend WithEvents btn_Pembetulan As Button
    Friend WithEvents cmb_JenisTampilan As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Total_Tagihan As DataGridViewTextBoxColumn
    Friend WithEvents btn_LihatInvoice As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Angka_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Penjualan As DataGridViewTextBoxColumn
    Friend WithEvents N_P As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Pembetulan As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Lapor As DataGridViewTextBoxColumn
    Friend WithEvents Jatuh_Tempo As DataGridViewTextBoxColumn
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
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_PPN As DataGridViewTextBoxColumn
    Friend WithEvents PPN_ As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Dipotong As DataGridViewTextBoxColumn
    Friend WithEvents Tagihan_Kotor As DataGridViewTextBoxColumn
    Friend WithEvents Retur_DPP As DataGridViewTextBoxColumn
    Friend WithEvents Retur_PPN As DataGridViewTextBoxColumn
    Friend WithEvents Retur_ As DataGridViewTextBoxColumn
    Friend WithEvents Asset_ As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
End Class
