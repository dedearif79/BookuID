<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_InvoicePembelian
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btn_Pembetulan = New System.Windows.Forms.Button()
        Me.cmb_Supplier = New System.Windows.Forms.ComboBox()
        Me.lbl_Supplier = New System.Windows.Forms.Label()
        Me.btn_LihatJurnal = New System.Windows.Forms.Button()
        Me.btn_Pratinjau = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_JudulForm = New System.Windows.Forms.Label()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Tambah = New System.Windows.Forms.Button()
        Me.btn_Cetak = New System.Windows.Forms.Button()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Angka_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Pembelian = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.N_P = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Pembetulan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Lapor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jatuh_Tempo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_SJ_BAST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_SJ_BAST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Project = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Harga = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diskon_Rp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dasar_Pengenaan_Pajak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Faktur_Pajak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_PPN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PPN_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PPh_Dipotong = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tagihan_Kotor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Retur_DPP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Retur_PPN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Retur_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Catatan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_JV = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmb_JenisTampilan = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_LihatInvoice = New System.Windows.Forms.Button()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Pembetulan
        '
        Me.btn_Pembetulan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pembetulan.Location = New System.Drawing.Point(1209, 85)
        Me.btn_Pembetulan.Name = "btn_Pembetulan"
        Me.btn_Pembetulan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Pembetulan.TabIndex = 10205
        Me.btn_Pembetulan.Text = "Pembetulan"
        Me.btn_Pembetulan.UseVisualStyleBackColor = True
        '
        'cmb_Supplier
        '
        Me.cmb_Supplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.cmb_Supplier.FormattingEnabled = True
        Me.cmb_Supplier.ItemHeight = 13
        Me.cmb_Supplier.Location = New System.Drawing.Point(538, 93)
        Me.cmb_Supplier.Name = "cmb_Supplier"
        Me.cmb_Supplier.Size = New System.Drawing.Size(172, 21)
        Me.cmb_Supplier.TabIndex = 10203
        '
        'lbl_Supplier
        '
        Me.lbl_Supplier.AutoSize = True
        Me.lbl_Supplier.Location = New System.Drawing.Point(535, 75)
        Me.lbl_Supplier.Name = "lbl_Supplier"
        Me.lbl_Supplier.Size = New System.Drawing.Size(51, 13)
        Me.lbl_Supplier.TabIndex = 10204
        Me.lbl_Supplier.Text = "Supplier :"
        '
        'btn_LihatJurnal
        '
        Me.btn_LihatJurnal.Enabled = False
        Me.btn_LihatJurnal.Location = New System.Drawing.Point(1021, 12)
        Me.btn_LihatJurnal.Name = "btn_LihatJurnal"
        Me.btn_LihatJurnal.Size = New System.Drawing.Size(93, 50)
        Me.btn_LihatJurnal.TabIndex = 10202
        Me.btn_LihatJurnal.Text = "Lihat Jurnal"
        Me.btn_LihatJurnal.UseVisualStyleBackColor = True
        '
        'btn_Pratinjau
        '
        Me.btn_Pratinjau.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pratinjau.Location = New System.Drawing.Point(1120, 12)
        Me.btn_Pratinjau.Name = "btn_Pratinjau"
        Me.btn_Pratinjau.Size = New System.Drawing.Size(83, 50)
        Me.btn_Pratinjau.TabIndex = 10201
        Me.btn_Pratinjau.Text = "Pratinjau"
        Me.btn_Pratinjau.UseVisualStyleBackColor = True
        Me.btn_Pratinjau.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(203, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 10207
        Me.Label1.Text = "Tampilan :"
        '
        'lbl_JudulForm
        '
        Me.lbl_JudulForm.AutoSize = True
        Me.lbl_JudulForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JudulForm.Location = New System.Drawing.Point(10, 10)
        Me.lbl_JudulForm.Name = "lbl_JudulForm"
        Me.lbl_JudulForm.Size = New System.Drawing.Size(264, 32)
        Me.lbl_JudulForm.TabIndex = 10199
        Me.lbl_JudulForm.Text = "Invoice Pembelian"
        Me.lbl_JudulForm.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn_Edit
        '
        Me.btn_Edit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Edit.Location = New System.Drawing.Point(1031, 85)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(83, 35)
        Me.btn_Edit.TabIndex = 10197
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'btn_Hapus
        '
        Me.btn_Hapus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Hapus.Location = New System.Drawing.Point(1120, 85)
        Me.btn_Hapus.Name = "btn_Hapus"
        Me.btn_Hapus.Size = New System.Drawing.Size(83, 35)
        Me.btn_Hapus.TabIndex = 10196
        Me.btn_Hapus.Text = "Hapus"
        Me.btn_Hapus.UseVisualStyleBackColor = True
        '
        'btn_Tambah
        '
        Me.btn_Tambah.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Tambah.Location = New System.Drawing.Point(942, 85)
        Me.btn_Tambah.Name = "btn_Tambah"
        Me.btn_Tambah.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tambah.TabIndex = 10195
        Me.btn_Tambah.Text = "Input"
        Me.btn_Tambah.UseVisualStyleBackColor = True
        '
        'btn_Cetak
        '
        Me.btn_Cetak.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cetak.Location = New System.Drawing.Point(1209, 12)
        Me.btn_Cetak.Name = "btn_Cetak"
        Me.btn_Cetak.Size = New System.Drawing.Size(83, 50)
        Me.btn_Cetak.TabIndex = 10200
        Me.btn_Cetak.Text = "Cetak"
        Me.btn_Cetak.UseVisualStyleBackColor = True
        Me.btn_Cetak.Visible = False
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Location = New System.Drawing.Point(12, 55)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(81, 65)
        Me.btn_Refresh.TabIndex = 10194
        Me.btn_Refresh.Text = "Refresh"
        Me.btn_Refresh.UseVisualStyleBackColor = True
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Jenis_Invoice, Me.Jenis_Produk, Me.Angka_Invoice, Me.Nomor_Invoice, Me.Nomor_Pembelian, Me.N_P, Me.Tanggal_Invoice, Me.Tanggal_Pembetulan, Me.Tanggal_Lapor, Me.Jatuh_Tempo, Me.Nomor_SJ_BAST, Me.Tanggal_SJ_BAST, Me.Nomor_PO, Me.Tanggal_PO, Me.Kode_Project, Me.Kode_Supplier, Me.Nama_Supplier, Me.Jumlah_Harga, Me.Diskon_Rp, Me.Dasar_Pengenaan_Pajak, Me.Nomor_Faktur_Pajak, Me.Jenis_PPN, Me.PPN_, Me.PPh_Dipotong, Me.Tagihan_Kotor, Me.Retur_DPP, Me.Retur_PPN, Me.Retur_, Me.Catatan_, Me.Nomor_JV})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 126)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(1280, 427)
        Me.DataTabelUtama.TabIndex = 10198
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 45
        '
        'Jenis_Invoice
        '
        Me.Jenis_Invoice.HeaderText = "Jenis Invoice"
        Me.Jenis_Invoice.Name = "Jenis_Invoice"
        Me.Jenis_Invoice.ReadOnly = True
        Me.Jenis_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jenis_Invoice.Visible = False
        Me.Jenis_Invoice.Width = 72
        '
        'Jenis_Produk
        '
        Me.Jenis_Produk.HeaderText = "Jenis Produk"
        Me.Jenis_Produk.Name = "Jenis_Produk"
        Me.Jenis_Produk.ReadOnly = True
        Me.Jenis_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jenis_Produk.Visible = False
        Me.Jenis_Produk.Width = 99
        '
        'Angka_Invoice
        '
        Me.Angka_Invoice.HeaderText = "Angka Invoice"
        Me.Angka_Invoice.Name = "Angka_Invoice"
        Me.Angka_Invoice.ReadOnly = True
        Me.Angka_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Angka_Invoice.Visible = False
        '
        'Nomor_Invoice
        '
        Me.Nomor_Invoice.HeaderText = "Nomor Invoice"
        Me.Nomor_Invoice.Name = "Nomor_Invoice"
        Me.Nomor_Invoice.ReadOnly = True
        Me.Nomor_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Invoice.Width = 99
        '
        'Nomor_Pembelian
        '
        Me.Nomor_Pembelian.HeaderText = "Nomor Pembelian"
        Me.Nomor_Pembelian.Name = "Nomor_Pembelian"
        Me.Nomor_Pembelian.ReadOnly = True
        Me.Nomor_Pembelian.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Pembelian.Visible = False
        '
        'N_P
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.N_P.DefaultCellStyle = DataGridViewCellStyle2
        Me.N_P.HeaderText = "N/P"
        Me.N_P.Name = "N_P"
        Me.N_P.ReadOnly = True
        Me.N_P.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.N_P.Visible = False
        Me.N_P.Width = 33
        '
        'Tanggal_Invoice
        '
        Me.Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Me.Tanggal_Invoice.Name = "Tanggal_Invoice"
        Me.Tanggal_Invoice.ReadOnly = True
        Me.Tanggal_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Invoice.Width = 63
        '
        'Tanggal_Pembetulan
        '
        Me.Tanggal_Pembetulan.HeaderText = "Tanggal Pembetulan"
        Me.Tanggal_Pembetulan.Name = "Tanggal_Pembetulan"
        Me.Tanggal_Pembetulan.ReadOnly = True
        Me.Tanggal_Pembetulan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Pembetulan.Width = 63
        '
        'Tanggal_Lapor
        '
        Me.Tanggal_Lapor.HeaderText = "Tanggal Lapor"
        Me.Tanggal_Lapor.Name = "Tanggal_Lapor"
        Me.Tanggal_Lapor.ReadOnly = True
        Me.Tanggal_Lapor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Lapor.Visible = False
        Me.Tanggal_Lapor.Width = 63
        '
        'Jatuh_Tempo
        '
        Me.Jatuh_Tempo.HeaderText = "Jatuh Tempo"
        Me.Jatuh_Tempo.Name = "Jatuh_Tempo"
        Me.Jatuh_Tempo.ReadOnly = True
        Me.Jatuh_Tempo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jatuh_Tempo.Width = 63
        '
        'Nomor_SJ_BAST
        '
        Me.Nomor_SJ_BAST.HeaderText = "Nomor SJ/BAST"
        Me.Nomor_SJ_BAST.Name = "Nomor_SJ_BAST"
        Me.Nomor_SJ_BAST.ReadOnly = True
        Me.Nomor_SJ_BAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_SJ_BAST.Width = 99
        '
        'Tanggal_SJ_BAST
        '
        Me.Tanggal_SJ_BAST.HeaderText = "Tanggal SJ/BAST"
        Me.Tanggal_SJ_BAST.Name = "Tanggal_SJ_BAST"
        Me.Tanggal_SJ_BAST.ReadOnly = True
        Me.Tanggal_SJ_BAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_SJ_BAST.Width = 99
        '
        'Nomor_PO
        '
        Me.Nomor_PO.HeaderText = "Nomor PO"
        Me.Nomor_PO.Name = "Nomor_PO"
        Me.Nomor_PO.ReadOnly = True
        Me.Nomor_PO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_PO.Width = 99
        '
        'Tanggal_PO
        '
        Me.Tanggal_PO.HeaderText = "Tanggal PO"
        Me.Tanggal_PO.Name = "Tanggal_PO"
        Me.Tanggal_PO.ReadOnly = True
        Me.Tanggal_PO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_PO.Width = 99
        '
        'Kode_Project
        '
        Me.Kode_Project.HeaderText = "Kode Project"
        Me.Kode_Project.Name = "Kode_Project"
        Me.Kode_Project.ReadOnly = True
        Me.Kode_Project.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Project.Width = 99
        '
        'Kode_Supplier
        '
        Me.Kode_Supplier.HeaderText = "Kode Supplier"
        Me.Kode_Supplier.Name = "Kode_Supplier"
        Me.Kode_Supplier.ReadOnly = True
        Me.Kode_Supplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Supplier.Visible = False
        '
        'Nama_Supplier
        '
        Me.Nama_Supplier.HeaderText = "Nama Supplier"
        Me.Nama_Supplier.Name = "Nama_Supplier"
        Me.Nama_Supplier.ReadOnly = True
        Me.Nama_Supplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Supplier.Width = 150
        '
        'Jumlah_Harga
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Me.Jumlah_Harga.DefaultCellStyle = DataGridViewCellStyle3
        Me.Jumlah_Harga.HeaderText = "Jumlah Harga"
        Me.Jumlah_Harga.Name = "Jumlah_Harga"
        Me.Jumlah_Harga.ReadOnly = True
        Me.Jumlah_Harga.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Harga.Width = 81
        '
        'Diskon_Rp
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Me.Diskon_Rp.DefaultCellStyle = DataGridViewCellStyle4
        Me.Diskon_Rp.HeaderText = "Diskon"
        Me.Diskon_Rp.Name = "Diskon_Rp"
        Me.Diskon_Rp.ReadOnly = True
        Me.Diskon_Rp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Diskon_Rp.Width = 81
        '
        'Dasar_Pengenaan_Pajak
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Me.Dasar_Pengenaan_Pajak.DefaultCellStyle = DataGridViewCellStyle5
        Me.Dasar_Pengenaan_Pajak.HeaderText = "Dasar Pengenaan Pajak"
        Me.Dasar_Pengenaan_Pajak.Name = "Dasar_Pengenaan_Pajak"
        Me.Dasar_Pengenaan_Pajak.ReadOnly = True
        Me.Dasar_Pengenaan_Pajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Dasar_Pengenaan_Pajak.Width = 81
        '
        'Nomor_Faktur_Pajak
        '
        Me.Nomor_Faktur_Pajak.HeaderText = "Nomor Faktur Pajak"
        Me.Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Me.Nomor_Faktur_Pajak.ReadOnly = True
        Me.Nomor_Faktur_Pajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Faktur_Pajak.Visible = False
        '
        'Jenis_PPN
        '
        Me.Jenis_PPN.HeaderText = "Jenis PPN"
        Me.Jenis_PPN.Name = "Jenis_PPN"
        Me.Jenis_PPN.ReadOnly = True
        Me.Jenis_PPN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jenis_PPN.Visible = False
        '
        'PPN_
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Me.PPN_.DefaultCellStyle = DataGridViewCellStyle6
        Me.PPN_.HeaderText = "PPN"
        Me.PPN_.Name = "PPN_"
        Me.PPN_.ReadOnly = True
        Me.PPN_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PPh_Dipotong
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Me.PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle7
        Me.PPh_Dipotong.HeaderText = "PPh Dipotong"
        Me.PPh_Dipotong.Name = "PPh_Dipotong"
        Me.PPh_Dipotong.ReadOnly = True
        Me.PPh_Dipotong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PPh_Dipotong.Width = 81
        '
        'Tagihan_Kotor
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Me.Tagihan_Kotor.DefaultCellStyle = DataGridViewCellStyle8
        Me.Tagihan_Kotor.HeaderText = "Jumlah Tagihan"
        Me.Tagihan_Kotor.Name = "Tagihan_Kotor"
        Me.Tagihan_Kotor.ReadOnly = True
        Me.Tagihan_Kotor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tagihan_Kotor.Width = 81
        '
        'Retur_DPP
        '
        Me.Retur_DPP.HeaderText = "Retur DPP"
        Me.Retur_DPP.Name = "Retur_DPP"
        Me.Retur_DPP.ReadOnly = True
        Me.Retur_DPP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Retur_DPP.Visible = False
        '
        'Retur_PPN
        '
        Me.Retur_PPN.HeaderText = "Retur PPN"
        Me.Retur_PPN.Name = "Retur_PPN"
        Me.Retur_PPN.ReadOnly = True
        Me.Retur_PPN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Retur_PPN.Visible = False
        '
        'Retur_
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        Me.Retur_.DefaultCellStyle = DataGridViewCellStyle9
        Me.Retur_.HeaderText = "Retur"
        Me.Retur_.Name = "Retur_"
        Me.Retur_.ReadOnly = True
        Me.Retur_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Retur_.Visible = False
        '
        'Catatan_
        '
        DataGridViewCellStyle10.NullValue = Nothing
        Me.Catatan_.DefaultCellStyle = DataGridViewCellStyle10
        Me.Catatan_.HeaderText = "Catatan"
        Me.Catatan_.Name = "Catatan_"
        Me.Catatan_.ReadOnly = True
        Me.Catatan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Catatan_.Width = 210
        '
        'Nomor_JV
        '
        Me.Nomor_JV.HeaderText = "Nomor JV"
        Me.Nomor_JV.Name = "Nomor_JV"
        Me.Nomor_JV.ReadOnly = True
        Me.Nomor_JV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_JV.Visible = False
        '
        'cmb_JenisTampilan
        '
        Me.cmb_JenisTampilan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.cmb_JenisTampilan.FormattingEnabled = True
        Me.cmb_JenisTampilan.ItemHeight = 13
        Me.cmb_JenisTampilan.Location = New System.Drawing.Point(206, 93)
        Me.cmb_JenisTampilan.Name = "cmb_JenisTampilan"
        Me.cmb_JenisTampilan.Size = New System.Drawing.Size(86, 21)
        Me.cmb_JenisTampilan.TabIndex = 10206
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(756, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(237, 13)
        Me.Label2.TabIndex = 10208
        Me.Label2.Text = "Invoice Tahun Lalu jangan ditampilkan di sini...!!!"
        '
        'btn_LihatInvoice
        '
        Me.btn_LihatInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_LihatInvoice.Location = New System.Drawing.Point(789, 85)
        Me.btn_LihatInvoice.Name = "btn_LihatInvoice"
        Me.btn_LihatInvoice.Size = New System.Drawing.Size(103, 35)
        Me.btn_LihatInvoice.TabIndex = 10209
        Me.btn_LihatInvoice.Text = "Lihat Invoice"
        Me.btn_LihatInvoice.UseVisualStyleBackColor = True
        '
        'frm_InvoicePembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 591)
        Me.Controls.Add(Me.btn_LihatInvoice)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btn_Pembetulan)
        Me.Controls.Add(Me.cmb_Supplier)
        Me.Controls.Add(Me.lbl_Supplier)
        Me.Controls.Add(Me.btn_LihatJurnal)
        Me.Controls.Add(Me.btn_Pratinjau)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbl_JudulForm)
        Me.Controls.Add(Me.btn_Edit)
        Me.Controls.Add(Me.btn_Hapus)
        Me.Controls.Add(Me.btn_Tambah)
        Me.Controls.Add(Me.btn_Cetak)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.cmb_JenisTampilan)
        Me.Name = "frm_InvoicePembelian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Invoice Pembelian"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Pembetulan As Button
    Friend WithEvents cmb_Supplier As ComboBox
    Friend WithEvents lbl_Supplier As Label
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents btn_Pratinjau As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents btn_Cetak As Button
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents cmb_JenisTampilan As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_LihatInvoice As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Angka_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Pembelian As DataGridViewTextBoxColumn
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
    Friend WithEvents Kode_Supplier As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Supplier As DataGridViewTextBoxColumn
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
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
End Class
