<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_BukuPembelian
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
        btn_Jurnal = New Button()
        Label1 = New Label()
        cmb_JatuhTempo = New ComboBox()
        cmb_Supplier = New ComboBox()
        lbl_Supplier = New Label()
        Label15 = New Label()
        cmb_JenisProduk_Induk = New ComboBox()
        lbl_JudulForm = New Label()
        Label2 = New Label()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_Pembelian = New DataGridViewTextBoxColumn()
        Jenis_Invoice = New DataGridViewTextBoxColumn()
        Jenis_Produk = New DataGridViewTextBoxColumn()
        Angka_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        N_P = New DataGridViewTextBoxColumn()
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Tanggal_Pembetulan = New DataGridViewTextBoxColumn()
        Kode_Supplier = New DataGridViewTextBoxColumn()
        Nama_Supplier = New DataGridViewTextBoxColumn()
        Jumlah_Harga = New DataGridViewTextBoxColumn()
        Diskon_Rp = New DataGridViewTextBoxColumn()
        Dasar_Pengenaan_Pajak = New DataGridViewTextBoxColumn()
        Jenis_PPN = New DataGridViewTextBoxColumn()
        Perlakuan_PPN = New DataGridViewTextBoxColumn()
        PPN_ = New DataGridViewTextBoxColumn()
        PPh_Dipotong = New DataGridViewTextBoxColumn()
        Tagihan_Bruto = New DataGridViewTextBoxColumn()
        Retur_ = New DataGridViewTextBoxColumn()
        Tagihan_Netto = New DataGridViewTextBoxColumn()
        Nomor_SJ_BAST = New DataGridViewTextBoxColumn()
        Tanggal_SJ_BAST = New DataGridViewTextBoxColumn()
        Nomor_PO = New DataGridViewTextBoxColumn()
        Tanggal_PO = New DataGridViewTextBoxColumn()
        Kode_Project = New DataGridViewTextBoxColumn()
        Masa_Jatuh_Tempo = New DataGridViewTextBoxColumn()
        Keterangan_Jatuh_Tempo = New DataGridViewTextBoxColumn()
        Catatan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        cmb_JenisTampilan = New ComboBox()
        Label3 = New Label()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Jurnal
        ' 
        btn_Jurnal.Location = New Point(952, 7)
        btn_Jurnal.Margin = New Padding(4, 3, 4, 3)
        btn_Jurnal.Name = "btn_Jurnal"
        btn_Jurnal.Size = New Size(141, 58)
        btn_Jurnal.TabIndex = 10207
        btn_Jurnal.Text = "Jurnal"
        btn_Jurnal.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(821, 85)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(75, 13)
        Label1.TabIndex = 10206
        Label1.Text = "Jatuh Tempo :"
        ' 
        ' cmb_JatuhTempo
        ' 
        cmb_JatuhTempo.FormattingEnabled = True
        cmb_JatuhTempo.Location = New Point(825, 106)
        cmb_JatuhTempo.Margin = New Padding(4, 3, 4, 3)
        cmb_JatuhTempo.Name = "cmb_JatuhTempo"
        cmb_JatuhTempo.Size = New Size(83, 23)
        cmb_JatuhTempo.TabIndex = 10205
        ' 
        ' cmb_Supplier
        ' 
        cmb_Supplier.Font = New Font("Microsoft Sans Serif", 8F)
        cmb_Supplier.FormattingEnabled = True
        cmb_Supplier.ItemHeight = 13
        cmb_Supplier.Location = New Point(593, 106)
        cmb_Supplier.Margin = New Padding(4, 3, 4, 3)
        cmb_Supplier.Name = "cmb_Supplier"
        cmb_Supplier.Size = New Size(200, 21)
        cmb_Supplier.TabIndex = 10203
        ' 
        ' lbl_Supplier
        ' 
        lbl_Supplier.AutoSize = True
        lbl_Supplier.Location = New Point(589, 85)
        lbl_Supplier.Margin = New Padding(4, 0, 4, 0)
        lbl_Supplier.Name = "lbl_Supplier"
        lbl_Supplier.Size = New Size(56, 15)
        lbl_Supplier.TabIndex = 10204
        lbl_Supplier.Text = "Supplier :"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(430, 85)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(74, 13)
        Label15.TabIndex = 10202
        Label15.Text = "Jenis Produk :"
        ' 
        ' cmb_JenisProduk_Induk
        ' 
        cmb_JenisProduk_Induk.FormattingEnabled = True
        cmb_JenisProduk_Induk.Location = New Point(434, 106)
        cmb_JenisProduk_Induk.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisProduk_Induk.Name = "cmb_JenisProduk_Induk"
        cmb_JenisProduk_Induk.Size = New Size(125, 23)
        cmb_JenisProduk_Induk.TabIndex = 10201
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(236, 32)
        lbl_JudulForm.TabIndex = 10198
        lbl_JudulForm.Text = "Buku Pembelian"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(293, 85)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(62, 15)
        Label2.TabIndex = 10209
        Label2.Text = "Tampilan :"
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10196
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_Pembelian, Jenis_Invoice, Jenis_Produk, Angka_Invoice, Nomor_Invoice, N_P, Nomor_Faktur_Pajak, Tanggal_Invoice, Tanggal_Pembetulan, Kode_Supplier, Nama_Supplier, Jumlah_Harga, Diskon_Rp, Dasar_Pengenaan_Pajak, Jenis_PPN, Perlakuan_PPN, PPN_, PPh_Dipotong, Tagihan_Bruto, Retur_, Tagihan_Netto, Nomor_SJ_BAST, Tanggal_SJ_BAST, Nomor_PO, Tanggal_PO, Kode_Project, Masa_Jatuh_Tempo, Keterangan_Jatuh_Tempo, Catatan_, Nomor_JV})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 493)
        DataTabelUtama.TabIndex = 10197
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
        ' Nomor_Pembelian
        ' 
        Nomor_Pembelian.HeaderText = "Nomor Pembelian"
        Nomor_Pembelian.Name = "Nomor_Pembelian"
        Nomor_Pembelian.ReadOnly = True
        Nomor_Pembelian.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Pembelian.Width = 99
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
        ' N_P
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        N_P.DefaultCellStyle = DataGridViewCellStyle2
        N_P.HeaderText = "N/P"
        N_P.Name = "N_P"
        N_P.ReadOnly = True
        N_P.SortMode = DataGridViewColumnSortMode.NotSortable
        N_P.Visible = False
        N_P.Width = 33
        ' 
        ' Nomor_Faktur_Pajak
        ' 
        Nomor_Faktur_Pajak.HeaderText = "Nomor FP"
        Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Nomor_Faktur_Pajak.ReadOnly = True
        Nomor_Faktur_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Faktur_Pajak.Width = 99
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
        Tanggal_Pembetulan.Width = 63
        ' 
        ' Kode_Supplier
        ' 
        Kode_Supplier.HeaderText = "Kode Supplier"
        Kode_Supplier.Name = "Kode_Supplier"
        Kode_Supplier.ReadOnly = True
        Kode_Supplier.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Supplier.Visible = False
        ' 
        ' Nama_Supplier
        ' 
        Nama_Supplier.HeaderText = "Nama Supplier"
        Nama_Supplier.Name = "Nama_Supplier"
        Nama_Supplier.ReadOnly = True
        Nama_Supplier.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Supplier.Width = 150
        ' 
        ' Jumlah_Harga
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Jumlah_Harga.DefaultCellStyle = DataGridViewCellStyle3
        Jumlah_Harga.HeaderText = "Jumlah Harga"
        Jumlah_Harga.Name = "Jumlah_Harga"
        Jumlah_Harga.ReadOnly = True
        Jumlah_Harga.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Harga.Width = 81
        ' 
        ' Diskon_Rp
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Diskon_Rp.DefaultCellStyle = DataGridViewCellStyle4
        Diskon_Rp.HeaderText = "Diskon"
        Diskon_Rp.Name = "Diskon_Rp"
        Diskon_Rp.ReadOnly = True
        Diskon_Rp.SortMode = DataGridViewColumnSortMode.NotSortable
        Diskon_Rp.Width = 81
        ' 
        ' Dasar_Pengenaan_Pajak
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Dasar_Pengenaan_Pajak.DefaultCellStyle = DataGridViewCellStyle5
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
        ' Perlakuan_PPN
        ' 
        Perlakuan_PPN.HeaderText = "Perlakuan PPN"
        Perlakuan_PPN.Name = "Perlakuan_PPN"
        Perlakuan_PPN.ReadOnly = True
        Perlakuan_PPN.SortMode = DataGridViewColumnSortMode.NotSortable
        Perlakuan_PPN.Visible = False
        ' 
        ' PPN_
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        PPN_.DefaultCellStyle = DataGridViewCellStyle6
        PPN_.HeaderText = "PPN"
        PPN_.Name = "PPN_"
        PPN_.ReadOnly = True
        PPN_.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PPh_Dipotong
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle7
        PPh_Dipotong.HeaderText = "PPh Dipotong"
        PPh_Dipotong.Name = "PPh_Dipotong"
        PPh_Dipotong.ReadOnly = True
        PPh_Dipotong.SortMode = DataGridViewColumnSortMode.NotSortable
        PPh_Dipotong.Width = 81
        ' 
        ' Tagihan_Bruto
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Tagihan_Bruto.DefaultCellStyle = DataGridViewCellStyle8
        Tagihan_Bruto.HeaderText = "Tagihan Bruto"
        Tagihan_Bruto.Name = "Tagihan_Bruto"
        Tagihan_Bruto.ReadOnly = True
        Tagihan_Bruto.SortMode = DataGridViewColumnSortMode.NotSortable
        Tagihan_Bruto.Width = 81
        ' 
        ' Retur_
        ' 
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        Retur_.DefaultCellStyle = DataGridViewCellStyle9
        Retur_.HeaderText = "Retur"
        Retur_.Name = "Retur_"
        Retur_.ReadOnly = True
        Retur_.SortMode = DataGridViewColumnSortMode.NotSortable
        Retur_.Width = 81
        ' 
        ' Tagihan_Netto
        ' 
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        Tagihan_Netto.DefaultCellStyle = DataGridViewCellStyle10
        Tagihan_Netto.HeaderText = "Tagihan Netto"
        Tagihan_Netto.Name = "Tagihan_Netto"
        Tagihan_Netto.ReadOnly = True
        Tagihan_Netto.SortMode = DataGridViewColumnSortMode.NotSortable
        Tagihan_Netto.Width = 81
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
        Tanggal_SJ_BAST.Visible = False
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
        ' Masa_Jatuh_Tempo
        ' 
        Masa_Jatuh_Tempo.HeaderText = "Tgl/Masa Jatuh Tempo"
        Masa_Jatuh_Tempo.Name = "Masa_Jatuh_Tempo"
        Masa_Jatuh_Tempo.ReadOnly = True
        Masa_Jatuh_Tempo.SortMode = DataGridViewColumnSortMode.NotSortable
        Masa_Jatuh_Tempo.Width = 63
        ' 
        ' Keterangan_Jatuh_Tempo
        ' 
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter
        Keterangan_Jatuh_Tempo.DefaultCellStyle = DataGridViewCellStyle11
        Keterangan_Jatuh_Tempo.HeaderText = "Jatuh Tempo"
        Keterangan_Jatuh_Tempo.Name = "Keterangan_Jatuh_Tempo"
        Keterangan_Jatuh_Tempo.ReadOnly = True
        Keterangan_Jatuh_Tempo.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_Jatuh_Tempo.Width = 45
        ' 
        ' Catatan_
        ' 
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
        ' cmb_JenisTampilan
        ' 
        cmb_JenisTampilan.Font = New Font("Microsoft Sans Serif", 8F)
        cmb_JenisTampilan.FormattingEnabled = True
        cmb_JenisTampilan.ItemHeight = 13
        cmb_JenisTampilan.Location = New Point(296, 106)
        cmb_JenisTampilan.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisTampilan.Name = "cmb_JenisTampilan"
        cmb_JenisTampilan.Size = New Size(100, 21)
        cmb_JenisTampilan.TabIndex = 10208
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ForeColor = Color.Red
        Label3.Location = New Point(430, 29)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(263, 15)
        Label3.TabIndex = 10210
        Label3.Text = "Invoice Tahun Lalu jangan ditampilkan di sini...!!!"
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(977, 77)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10211
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuPembelian
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 682)
        Controls.Add(btn_Export)
        Controls.Add(Label3)
        Controls.Add(btn_Jurnal)
        Controls.Add(Label1)
        Controls.Add(cmb_JatuhTempo)
        Controls.Add(cmb_Supplier)
        Controls.Add(lbl_Supplier)
        Controls.Add(Label15)
        Controls.Add(cmb_JenisProduk_Induk)
        Controls.Add(lbl_JudulForm)
        Controls.Add(Label2)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(cmb_JenisTampilan)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPembelian"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pembelian"
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Jurnal As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_JatuhTempo As ComboBox
    Friend WithEvents cmb_Supplier As ComboBox
    Friend WithEvents lbl_Supplier As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cmb_JenisProduk_Induk As ComboBox
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents cmb_JenisTampilan As ComboBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Pembelian As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Angka_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents N_P As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Pembetulan As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Supplier As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Supplier As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Harga As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Rp As DataGridViewTextBoxColumn
    Friend WithEvents Dasar_Pengenaan_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_PPN As DataGridViewTextBoxColumn
    Friend WithEvents Perlakuan_PPN As DataGridViewTextBoxColumn
    Friend WithEvents PPN_ As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Dipotong As DataGridViewTextBoxColumn
    Friend WithEvents Tagihan_Bruto As DataGridViewTextBoxColumn
    Friend WithEvents Retur_ As DataGridViewTextBoxColumn
    Friend WithEvents Tagihan_Netto As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_PO As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Masa_Jatuh_Tempo As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_Jatuh_Tempo As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents Label3 As Label
    Friend WithEvents btn_Export As Button
End Class
