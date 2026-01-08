<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_BukuPengawasanHutangPPhPasal21_X
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
        btn_EditPembayaran = New Button()
        btn_InputPembayaran = New Button()
        lbl_MasaPajak = New Label()
        cmb_MasaPajak = New ComboBox()
        lbl_TahunPajak = New Label()
        cmb_TahunPajak = New ComboBox()
        btn_LihatJurnal = New Button()
        pnl_CRUD = New Panel()
        btn_Tambah = New Button()
        btn_Edit = New Button()
        btn_Hapus = New Button()
        Label1 = New Label()
        txt_SelisihSaldo = New TextBox()
        lbl_SaldoBerdasarkanCOA_PlusAJP = New Label()
        btn_Sesuaikan = New Button()
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian = New TextBox()
        txt_SaldoBerdasarkanList = New TextBox()
        lbl_SaldoBerdasarkanList = New Label()
        lbl_AJP = New Label()
        txt_AJP = New TextBox()
        lbl_SaldoAwalBerdasarkanCOA = New Label()
        grb_InfoSaldo = New GroupBox()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_BPHP = New DataGridViewTextBoxColumn()
        Bulan_ = New DataGridViewTextBoxColumn()
        Tanggal_Transaksi = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Nama_Jasa = New DataGridViewTextBoxColumn()
        NPWP_ = New DataGridViewTextBoxColumn()
        Kode_Supplier = New DataGridViewTextBoxColumn()
        Nama_Supplier = New DataGridViewTextBoxColumn()
        DPP_ = New DataGridViewTextBoxColumn()
        Bruto_Jasa_OP = New DataGridViewTextBoxColumn()
        Bruto_Gaji = New DataGridViewTextBoxColumn()
        Bruto_Pesangon = New DataGridViewTextBoxColumn()
        Bruto_ = New DataGridViewTextBoxColumn()
        Jasa_OP = New DataGridViewTextBoxColumn()
        Gaji_ = New DataGridViewTextBoxColumn()
        PPh_Pasal_21_100 = New DataGridViewTextBoxColumn()
        PPh_Pasal_21_401_Pesangon = New DataGridViewTextBoxColumn()
        PPh_Pasal_21 = New DataGridViewTextBoxColumn()
        Jumlah_Bayar_100 = New DataGridViewTextBoxColumn()
        Jumlah_Bayar_401 = New DataGridViewTextBoxColumn()
        Jumlah_Bayar_Pajak = New DataGridViewTextBoxColumn()
        Sisa_Hutang_Pajak = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        grb_Pembayaran = New GroupBox()
        rdb_KodeSetoran_401 = New RadioButton()
        rdb_KodeSetoran_100 = New RadioButton()
        btn_HapusPembayaran = New Button()
        dgv_DetailBayar = New DataGridView()
        Nomor_ID_Bayar = New DataGridViewTextBoxColumn()
        Tanggal_Bayar = New DataGridViewTextBoxColumn()
        Referensi_ = New DataGridViewTextBoxColumn()
        Nominal_Bayar = New DataGridViewTextBoxColumn()
        Keterangan_Bayar = New DataGridViewTextBoxColumn()
        Nomor_JV_Bayar = New DataGridViewTextBoxColumn()
        lbl_JudulForm = New Label()
        btn_Refresh = New Button()
        txt_SaldoAwalBerdasarkanCOA = New TextBox()
        btn_DetailGajiDanPesangon = New Button()
        btn_DetailPembayaran = New Button()
        btn_Export = New Button()
        pnl_CRUD.SuspendLayout()
        grb_InfoSaldo.SuspendLayout()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        grb_Pembayaran.SuspendLayout()
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_EditPembayaran
        ' 
        btn_EditPembayaran.Enabled = False
        btn_EditPembayaran.Location = New Point(10, 59)
        btn_EditPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_EditPembayaran.Name = "btn_EditPembayaran"
        btn_EditPembayaran.Size = New Size(94, 32)
        btn_EditPembayaran.TabIndex = 10061
        btn_EditPembayaran.Text = "Edit"
        btn_EditPembayaran.UseVisualStyleBackColor = True
        ' 
        ' btn_InputPembayaran
        ' 
        btn_InputPembayaran.Location = New Point(10, 23)
        btn_InputPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_InputPembayaran.Name = "btn_InputPembayaran"
        btn_InputPembayaran.Size = New Size(94, 32)
        btn_InputPembayaran.TabIndex = 10048
        btn_InputPembayaran.Text = "Bayar"
        btn_InputPembayaran.UseVisualStyleBackColor = True
        ' 
        ' lbl_MasaPajak
        ' 
        lbl_MasaPajak.AutoSize = True
        lbl_MasaPajak.Location = New Point(197, 62)
        lbl_MasaPajak.Margin = New Padding(4, 0, 4, 0)
        lbl_MasaPajak.Name = "lbl_MasaPajak"
        lbl_MasaPajak.Size = New Size(72, 15)
        lbl_MasaPajak.TabIndex = 10086
        lbl_MasaPajak.Text = "Masa Pajak :"
        ' 
        ' cmb_MasaPajak
        ' 
        cmb_MasaPajak.FormattingEnabled = True
        cmb_MasaPajak.Location = New Point(285, 59)
        cmb_MasaPajak.Margin = New Padding(4, 3, 4, 3)
        cmb_MasaPajak.Name = "cmb_MasaPajak"
        cmb_MasaPajak.Size = New Size(96, 23)
        cmb_MasaPajak.TabIndex = 10085
        ' 
        ' lbl_TahunPajak
        ' 
        lbl_TahunPajak.AutoSize = True
        lbl_TahunPajak.Location = New Point(16, 62)
        lbl_TahunPajak.Margin = New Padding(4, 0, 4, 0)
        lbl_TahunPajak.Name = "lbl_TahunPajak"
        lbl_TahunPajak.Size = New Size(45, 15)
        lbl_TahunPajak.TabIndex = 10084
        lbl_TahunPajak.Text = "Tahun :"
        ' 
        ' cmb_TahunPajak
        ' 
        cmb_TahunPajak.FormattingEnabled = True
        cmb_TahunPajak.Location = New Point(79, 59)
        cmb_TahunPajak.Margin = New Padding(4, 3, 4, 3)
        cmb_TahunPajak.Name = "cmb_TahunPajak"
        cmb_TahunPajak.Size = New Size(67, 23)
        cmb_TahunPajak.TabIndex = 10083
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(911, 17)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 52)
        btn_LihatJurnal.TabIndex = 10082
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' pnl_CRUD
        ' 
        pnl_CRUD.Controls.Add(btn_Tambah)
        pnl_CRUD.Controls.Add(btn_Edit)
        pnl_CRUD.Controls.Add(btn_Hapus)
        pnl_CRUD.Location = New Point(701, 90)
        pnl_CRUD.Margin = New Padding(2)
        pnl_CRUD.Name = "pnl_CRUD"
        pnl_CRUD.Size = New Size(307, 50)
        pnl_CRUD.TabIndex = 10087
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(1, 5)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 42)
        btn_Tambah.TabIndex = 10069
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Location = New Point(105, 5)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 42)
        btn_Edit.TabIndex = 10071
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Location = New Point(209, 5)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 42)
        btn_Hapus.TabIndex = 10070
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 87)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(40, 15)
        Label1.TabIndex = 10044
        Label1.Text = "Selisih"
        ' 
        ' txt_SelisihSaldo
        ' 
        txt_SelisihSaldo.Location = New Point(163, 83)
        txt_SelisihSaldo.Margin = New Padding(4, 3, 4, 3)
        txt_SelisihSaldo.Name = "txt_SelisihSaldo"
        txt_SelisihSaldo.Size = New Size(125, 23)
        txt_SelisihSaldo.TabIndex = 10045
        txt_SelisihSaldo.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_SaldoBerdasarkanCOA_PlusAJP
        ' 
        lbl_SaldoBerdasarkanCOA_PlusAJP.AutoSize = True
        lbl_SaldoBerdasarkanCOA_PlusAJP.Location = New Point(12, 57)
        lbl_SaldoBerdasarkanCOA_PlusAJP.Margin = New Padding(4, 0, 4, 0)
        lbl_SaldoBerdasarkanCOA_PlusAJP.Name = "lbl_SaldoBerdasarkanCOA_PlusAJP"
        lbl_SaldoBerdasarkanCOA_PlusAJP.Size = New Size(133, 15)
        lbl_SaldoBerdasarkanCOA_PlusAJP.TabIndex = 10042
        lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        ' 
        ' btn_Sesuaikan
        ' 
        btn_Sesuaikan.Location = New Point(300, 21)
        btn_Sesuaikan.Margin = New Padding(4, 3, 4, 3)
        btn_Sesuaikan.Name = "btn_Sesuaikan"
        btn_Sesuaikan.Size = New Size(94, 87)
        btn_Sesuaikan.TabIndex = 10040
        btn_Sesuaikan.Text = "Sesuaikan"
        btn_Sesuaikan.UseVisualStyleBackColor = True
        ' 
        ' txt_SaldoBerdasarkanCOA_PlusPenyesuaian
        ' 
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Location = New Point(163, 53)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Name = "txt_SaldoBerdasarkanCOA_PlusPenyesuaian"
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Size = New Size(125, 23)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TabIndex = 10043
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TextAlign = HorizontalAlignment.Right
        ' 
        ' txt_SaldoBerdasarkanList
        ' 
        txt_SaldoBerdasarkanList.Location = New Point(163, 23)
        txt_SaldoBerdasarkanList.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoBerdasarkanList.Name = "txt_SaldoBerdasarkanList"
        txt_SaldoBerdasarkanList.Size = New Size(125, 23)
        txt_SaldoBerdasarkanList.TabIndex = 10040
        txt_SaldoBerdasarkanList.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_SaldoBerdasarkanList
        ' 
        lbl_SaldoBerdasarkanList.AutoSize = True
        lbl_SaldoBerdasarkanList.Location = New Point(12, 27)
        lbl_SaldoBerdasarkanList.Margin = New Padding(4, 0, 4, 0)
        lbl_SaldoBerdasarkanList.Name = "lbl_SaldoBerdasarkanList"
        lbl_SaldoBerdasarkanList.Size = New Size(92, 15)
        lbl_SaldoBerdasarkanList.TabIndex = 10039
        lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
        ' 
        ' lbl_AJP
        ' 
        lbl_AJP.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lbl_AJP.AutoSize = True
        lbl_AJP.Location = New Point(411, 648)
        lbl_AJP.Margin = New Padding(4, 0, 4, 0)
        lbl_AJP.Name = "lbl_AJP"
        lbl_AJP.Size = New Size(33, 15)
        lbl_AJP.TabIndex = 10092
        lbl_AJP.Text = "AJP :"
        ' 
        ' txt_AJP
        ' 
        txt_AJP.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_AJP.Location = New Point(460, 645)
        txt_AJP.Margin = New Padding(4, 3, 4, 3)
        txt_AJP.Name = "txt_AJP"
        txt_AJP.Size = New Size(125, 23)
        txt_AJP.TabIndex = 10091
        txt_AJP.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_SaldoAwalBerdasarkanCOA
        ' 
        lbl_SaldoAwalBerdasarkanCOA.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lbl_SaldoAwalBerdasarkanCOA.AutoSize = True
        lbl_SaldoAwalBerdasarkanCOA.Location = New Point(16, 648)
        lbl_SaldoAwalBerdasarkanCOA.Margin = New Padding(4, 0, 4, 0)
        lbl_SaldoAwalBerdasarkanCOA.Name = "lbl_SaldoAwalBerdasarkanCOA"
        lbl_SaldoAwalBerdasarkanCOA.Size = New Size(211, 15)
        lbl_SaldoAwalBerdasarkanCOA.TabIndex = 10090
        lbl_SaldoAwalBerdasarkanCOA.Text = "Saldo Awal COA Hutang PPh Pasal 23 :"
        ' 
        ' grb_InfoSaldo
        ' 
        grb_InfoSaldo.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_InfoSaldo.Controls.Add(Label1)
        grb_InfoSaldo.Controls.Add(txt_SelisihSaldo)
        grb_InfoSaldo.Controls.Add(lbl_SaldoBerdasarkanCOA_PlusAJP)
        grb_InfoSaldo.Controls.Add(btn_Sesuaikan)
        grb_InfoSaldo.Controls.Add(txt_SaldoBerdasarkanCOA_PlusPenyesuaian)
        grb_InfoSaldo.Controls.Add(txt_SaldoBerdasarkanList)
        grb_InfoSaldo.Controls.Add(lbl_SaldoBerdasarkanList)
        grb_InfoSaldo.Location = New Point(1086, 425)
        grb_InfoSaldo.Margin = New Padding(4, 3, 4, 3)
        grb_InfoSaldo.Name = "grb_InfoSaldo"
        grb_InfoSaldo.Padding = New Padding(4, 3, 4, 3)
        grb_InfoSaldo.Size = New Size(408, 118)
        grb_InfoSaldo.TabIndex = 10088
        grb_InfoSaldo.TabStop = False
        grb_InfoSaldo.Text = "Saldo :"
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPHP, Bulan_, Tanggal_Transaksi, Tanggal_Invoice, Nomor_Invoice, Nomor_Faktur_Pajak, Nama_Jasa, NPWP_, Kode_Supplier, Nama_Supplier, DPP_, Bruto_Jasa_OP, Bruto_Gaji, Bruto_Pesangon, Bruto_, Jasa_OP, Gaji_, PPh_Pasal_21_100, PPh_Pasal_21_401_Pesangon, PPh_Pasal_21, Jumlah_Bayar_100, Jumlah_Bayar_401, Jumlah_Bayar_Pajak, Sisa_Hutang_Pajak, Keterangan_})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 493)
        DataTabelUtama.TabIndex = 10079
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Padding = New Padding(0, 0, 3, 0)
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_Urut.HeaderText = "No."
        Nomor_Urut.MinimumWidth = 6
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 33
        ' 
        ' Nomor_ID
        ' 
        Nomor_ID.HeaderText = "Nomor ID"
        Nomor_ID.Name = "Nomor_ID"
        Nomor_ID.ReadOnly = True
        Nomor_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID.Visible = False
        ' 
        ' Nomor_BPHP
        ' 
        Nomor_BPHP.HeaderText = "Nomor BPHP"
        Nomor_BPHP.MinimumWidth = 6
        Nomor_BPHP.Name = "Nomor_BPHP"
        Nomor_BPHP.ReadOnly = True
        Nomor_BPHP.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPHP.Width = 99
        ' 
        ' Bulan_
        ' 
        Bulan_.HeaderText = "Bulan"
        Bulan_.MinimumWidth = 6
        Bulan_.Name = "Bulan_"
        Bulan_.ReadOnly = True
        Bulan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Bulan_.Width = 99
        ' 
        ' Tanggal_Transaksi
        ' 
        Tanggal_Transaksi.HeaderText = "Tanggal Transaksi"
        Tanggal_Transaksi.MinimumWidth = 6
        Tanggal_Transaksi.Name = "Tanggal_Transaksi"
        Tanggal_Transaksi.ReadOnly = True
        Tanggal_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Transaksi.Width = 63
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Tanggal_Invoice.MinimumWidth = 6
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.ReadOnly = True
        Tanggal_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Invoice.Width = 63
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.MinimumWidth = 6
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.ReadOnly = True
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 150
        ' 
        ' Nomor_Faktur_Pajak
        ' 
        Nomor_Faktur_Pajak.HeaderText = "Nomor Faktur Pajak"
        Nomor_Faktur_Pajak.MinimumWidth = 6
        Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Nomor_Faktur_Pajak.ReadOnly = True
        Nomor_Faktur_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Faktur_Pajak.Width = 126
        ' 
        ' Nama_Jasa
        ' 
        Nama_Jasa.HeaderText = "Nama Jasa/Sewa"
        Nama_Jasa.MinimumWidth = 6
        Nama_Jasa.Name = "Nama_Jasa"
        Nama_Jasa.ReadOnly = True
        Nama_Jasa.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Jasa.Width = 150
        ' 
        ' NPWP_
        ' 
        NPWP_.HeaderText = "NPWP"
        NPWP_.MinimumWidth = 6
        NPWP_.Name = "NPWP_"
        NPWP_.ReadOnly = True
        NPWP_.SortMode = DataGridViewColumnSortMode.NotSortable
        NPWP_.Width = 99
        ' 
        ' Kode_Supplier
        ' 
        Kode_Supplier.HeaderText = "Kode Supplier"
        Kode_Supplier.MinimumWidth = 6
        Kode_Supplier.Name = "Kode_Supplier"
        Kode_Supplier.ReadOnly = True
        Kode_Supplier.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Supplier.Visible = False
        Kode_Supplier.Width = 63
        ' 
        ' Nama_Supplier
        ' 
        Nama_Supplier.HeaderText = "Nama Supplier"
        Nama_Supplier.MinimumWidth = 6
        Nama_Supplier.Name = "Nama_Supplier"
        Nama_Supplier.ReadOnly = True
        Nama_Supplier.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Supplier.Width = 180
        ' 
        ' DPP_
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        DPP_.DefaultCellStyle = DataGridViewCellStyle2
        DPP_.HeaderText = "DPP"
        DPP_.MinimumWidth = 6
        DPP_.Name = "DPP_"
        DPP_.ReadOnly = True
        DPP_.SortMode = DataGridViewColumnSortMode.NotSortable
        DPP_.Width = 81
        ' 
        ' Bruto_Jasa_OP
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Bruto_Jasa_OP.DefaultCellStyle = DataGridViewCellStyle3
        Bruto_Jasa_OP.HeaderText = "Bruto Jasa OP"
        Bruto_Jasa_OP.Name = "Bruto_Jasa_OP"
        Bruto_Jasa_OP.ReadOnly = True
        Bruto_Jasa_OP.SortMode = DataGridViewColumnSortMode.NotSortable
        Bruto_Jasa_OP.Width = 81
        ' 
        ' Bruto_Gaji
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Bruto_Gaji.DefaultCellStyle = DataGridViewCellStyle4
        Bruto_Gaji.HeaderText = "Bruto Gaji"
        Bruto_Gaji.Name = "Bruto_Gaji"
        Bruto_Gaji.ReadOnly = True
        Bruto_Gaji.SortMode = DataGridViewColumnSortMode.NotSortable
        Bruto_Gaji.Width = 81
        ' 
        ' Bruto_Pesangon
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Bruto_Pesangon.DefaultCellStyle = DataGridViewCellStyle5
        Bruto_Pesangon.HeaderText = "Bruto Pesangon"
        Bruto_Pesangon.Name = "Bruto_Pesangon"
        Bruto_Pesangon.ReadOnly = True
        Bruto_Pesangon.SortMode = DataGridViewColumnSortMode.NotSortable
        Bruto_Pesangon.Width = 81
        ' 
        ' Bruto_
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Bruto_.DefaultCellStyle = DataGridViewCellStyle6
        Bruto_.HeaderText = "Bruto"
        Bruto_.Name = "Bruto_"
        Bruto_.ReadOnly = True
        Bruto_.SortMode = DataGridViewColumnSortMode.NotSortable
        Bruto_.Width = 81
        ' 
        ' Jasa_OP
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Jasa_OP.DefaultCellStyle = DataGridViewCellStyle7
        Jasa_OP.HeaderText = "PPh 21 Jasa OP"
        Jasa_OP.Name = "Jasa_OP"
        Jasa_OP.ReadOnly = True
        Jasa_OP.SortMode = DataGridViewColumnSortMode.NotSortable
        Jasa_OP.Width = 81
        ' 
        ' Gaji_
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Gaji_.DefaultCellStyle = DataGridViewCellStyle8
        Gaji_.HeaderText = "PPh 21 Gaji"
        Gaji_.Name = "Gaji_"
        Gaji_.ReadOnly = True
        Gaji_.SortMode = DataGridViewColumnSortMode.NotSortable
        Gaji_.Width = 81
        ' 
        ' PPh_Pasal_21_100
        ' 
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        PPh_Pasal_21_100.DefaultCellStyle = DataGridViewCellStyle9
        PPh_Pasal_21_100.HeaderText = "Kode-100"
        PPh_Pasal_21_100.Name = "PPh_Pasal_21_100"
        PPh_Pasal_21_100.ReadOnly = True
        PPh_Pasal_21_100.SortMode = DataGridViewColumnSortMode.NotSortable
        PPh_Pasal_21_100.Width = 81
        ' 
        ' PPh_Pasal_21_401_Pesangon
        ' 
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        PPh_Pasal_21_401_Pesangon.DefaultCellStyle = DataGridViewCellStyle10
        PPh_Pasal_21_401_Pesangon.HeaderText = "Kode-401"
        PPh_Pasal_21_401_Pesangon.Name = "PPh_Pasal_21_401_Pesangon"
        PPh_Pasal_21_401_Pesangon.ReadOnly = True
        PPh_Pasal_21_401_Pesangon.SortMode = DataGridViewColumnSortMode.NotSortable
        PPh_Pasal_21_401_Pesangon.Width = 81
        ' 
        ' PPh_Pasal_21
        ' 
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        PPh_Pasal_21.DefaultCellStyle = DataGridViewCellStyle11
        PPh_Pasal_21.HeaderText = "Jumlah PPh"
        PPh_Pasal_21.MinimumWidth = 6
        PPh_Pasal_21.Name = "PPh_Pasal_21"
        PPh_Pasal_21.ReadOnly = True
        PPh_Pasal_21.SortMode = DataGridViewColumnSortMode.NotSortable
        PPh_Pasal_21.Width = 81
        ' 
        ' Jumlah_Bayar_100
        ' 
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N0"
        Jumlah_Bayar_100.DefaultCellStyle = DataGridViewCellStyle12
        Jumlah_Bayar_100.HeaderText = "Bayar Kode-100"
        Jumlah_Bayar_100.Name = "Jumlah_Bayar_100"
        Jumlah_Bayar_100.ReadOnly = True
        Jumlah_Bayar_100.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar_100.Width = 81
        ' 
        ' Jumlah_Bayar_401
        ' 
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N0"
        Jumlah_Bayar_401.DefaultCellStyle = DataGridViewCellStyle13
        Jumlah_Bayar_401.HeaderText = "Bayar Kode-401"
        Jumlah_Bayar_401.Name = "Jumlah_Bayar_401"
        Jumlah_Bayar_401.ReadOnly = True
        Jumlah_Bayar_401.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar_401.Width = 81
        ' 
        ' Jumlah_Bayar_Pajak
        ' 
        DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N0"
        Jumlah_Bayar_Pajak.DefaultCellStyle = DataGridViewCellStyle14
        Jumlah_Bayar_Pajak.HeaderText = "Jumlah Bayar"
        Jumlah_Bayar_Pajak.MinimumWidth = 6
        Jumlah_Bayar_Pajak.Name = "Jumlah_Bayar_Pajak"
        Jumlah_Bayar_Pajak.ReadOnly = True
        Jumlah_Bayar_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar_Pajak.Width = 81
        ' 
        ' Sisa_Hutang_Pajak
        ' 
        DataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "N0"
        Sisa_Hutang_Pajak.DefaultCellStyle = DataGridViewCellStyle15
        Sisa_Hutang_Pajak.HeaderText = "Sisa Hutang"
        Sisa_Hutang_Pajak.MinimumWidth = 6
        Sisa_Hutang_Pajak.Name = "Sisa_Hutang_Pajak"
        Sisa_Hutang_Pajak.ReadOnly = True
        Sisa_Hutang_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Sisa_Hutang_Pajak.Width = 81
        ' 
        ' Keterangan_
        ' 
        Keterangan_.HeaderText = "Keterangan"
        Keterangan_.Name = "Keterangan_"
        Keterangan_.ReadOnly = True
        Keterangan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_.Width = 180
        ' 
        ' grb_Pembayaran
        ' 
        grb_Pembayaran.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_Pembayaran.Controls.Add(rdb_KodeSetoran_401)
        grb_Pembayaran.Controls.Add(rdb_KodeSetoran_100)
        grb_Pembayaran.Controls.Add(btn_HapusPembayaran)
        grb_Pembayaran.Controls.Add(dgv_DetailBayar)
        grb_Pembayaran.Controls.Add(btn_EditPembayaran)
        grb_Pembayaran.Controls.Add(btn_InputPembayaran)
        grb_Pembayaran.Location = New Point(1015, 5)
        grb_Pembayaran.Margin = New Padding(4, 3, 4, 3)
        grb_Pembayaran.Name = "grb_Pembayaran"
        grb_Pembayaran.Padding = New Padding(4, 3, 4, 3)
        grb_Pembayaran.Size = New Size(492, 134)
        grb_Pembayaran.TabIndex = 10081
        grb_Pembayaran.TabStop = False
        grb_Pembayaran.Text = "Pembayaran"
        grb_Pembayaran.Visible = False
        ' 
        ' rdb_KodeSetoran_401
        ' 
        rdb_KodeSetoran_401.AutoSize = True
        rdb_KodeSetoran_401.Location = New Point(190, 13)
        rdb_KodeSetoran_401.Margin = New Padding(4, 3, 4, 3)
        rdb_KodeSetoran_401.Name = "rdb_KodeSetoran_401"
        rdb_KodeSetoran_401.Size = New Size(43, 19)
        rdb_KodeSetoran_401.TabIndex = 10064
        rdb_KodeSetoran_401.TabStop = True
        rdb_KodeSetoran_401.Text = "401"
        rdb_KodeSetoran_401.UseVisualStyleBackColor = True
        ' 
        ' rdb_KodeSetoran_100
        ' 
        rdb_KodeSetoran_100.AutoSize = True
        rdb_KodeSetoran_100.Location = New Point(121, 13)
        rdb_KodeSetoran_100.Margin = New Padding(4, 3, 4, 3)
        rdb_KodeSetoran_100.Name = "rdb_KodeSetoran_100"
        rdb_KodeSetoran_100.Size = New Size(43, 19)
        rdb_KodeSetoran_100.TabIndex = 10063
        rdb_KodeSetoran_100.TabStop = True
        rdb_KodeSetoran_100.Text = "100"
        rdb_KodeSetoran_100.UseVisualStyleBackColor = True
        ' 
        ' btn_HapusPembayaran
        ' 
        btn_HapusPembayaran.Enabled = False
        btn_HapusPembayaran.Location = New Point(10, 95)
        btn_HapusPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_HapusPembayaran.Name = "btn_HapusPembayaran"
        btn_HapusPembayaran.Size = New Size(94, 32)
        btn_HapusPembayaran.TabIndex = 10062
        btn_HapusPembayaran.Text = "Hapus"
        btn_HapusPembayaran.UseVisualStyleBackColor = True
        ' 
        ' dgv_DetailBayar
        ' 
        dgv_DetailBayar.AllowUserToAddRows = False
        dgv_DetailBayar.AllowUserToDeleteRows = False
        dgv_DetailBayar.AllowUserToResizeRows = False
        dgv_DetailBayar.BorderStyle = BorderStyle.None
        dgv_DetailBayar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_DetailBayar.Columns.AddRange(New DataGridViewColumn() {Nomor_ID_Bayar, Tanggal_Bayar, Referensi_, Nominal_Bayar, Keterangan_Bayar, Nomor_JV_Bayar})
        dgv_DetailBayar.Location = New Point(118, 39)
        dgv_DetailBayar.Margin = New Padding(4, 3, 4, 3)
        dgv_DetailBayar.MultiSelect = False
        dgv_DetailBayar.Name = "dgv_DetailBayar"
        dgv_DetailBayar.ReadOnly = True
        dgv_DetailBayar.RowHeadersVisible = False
        dgv_DetailBayar.RowHeadersWidth = 33
        dgv_DetailBayar.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_DetailBayar.Size = New Size(359, 85)
        dgv_DetailBayar.TabIndex = 10049
        ' 
        ' Nomor_ID_Bayar
        ' 
        Nomor_ID_Bayar.HeaderText = "Nomor ID Bayar"
        Nomor_ID_Bayar.MinimumWidth = 6
        Nomor_ID_Bayar.Name = "Nomor_ID_Bayar"
        Nomor_ID_Bayar.ReadOnly = True
        Nomor_ID_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID_Bayar.Visible = False
        Nomor_ID_Bayar.Width = 125
        ' 
        ' Tanggal_Bayar
        ' 
        DataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Bayar.DefaultCellStyle = DataGridViewCellStyle16
        Tanggal_Bayar.HeaderText = "Tgl. Bayar"
        Tanggal_Bayar.MinimumWidth = 6
        Tanggal_Bayar.Name = "Tanggal_Bayar"
        Tanggal_Bayar.ReadOnly = True
        Tanggal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Bayar.Width = 81
        ' 
        ' Referensi_
        ' 
        DataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleLeft
        Referensi_.DefaultCellStyle = DataGridViewCellStyle17
        Referensi_.HeaderText = "Referensi"
        Referensi_.MinimumWidth = 6
        Referensi_.Name = "Referensi_"
        Referensi_.ReadOnly = True
        Referensi_.SortMode = DataGridViewColumnSortMode.NotSortable
        Referensi_.Width = 123
        ' 
        ' Nominal_Bayar
        ' 
        DataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "N0"
        DataGridViewCellStyle18.NullValue = Nothing
        Nominal_Bayar.DefaultCellStyle = DataGridViewCellStyle18
        Nominal_Bayar.HeaderText = "Jumlah Bayar"
        Nominal_Bayar.MinimumWidth = 6
        Nominal_Bayar.Name = "Nominal_Bayar"
        Nominal_Bayar.ReadOnly = True
        Nominal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Nominal_Bayar.Width = 99
        ' 
        ' Keterangan_Bayar
        ' 
        Keterangan_Bayar.HeaderText = "Keterangan Bayar"
        Keterangan_Bayar.MinimumWidth = 6
        Keterangan_Bayar.Name = "Keterangan_Bayar"
        Keterangan_Bayar.ReadOnly = True
        Keterangan_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_Bayar.Visible = False
        Keterangan_Bayar.Width = 33
        ' 
        ' Nomor_JV_Bayar
        ' 
        Nomor_JV_Bayar.HeaderText = "Nomor JV Bayar"
        Nomor_JV_Bayar.MinimumWidth = 6
        Nomor_JV_Bayar.Name = "Nomor_JV_Bayar"
        Nomor_JV_Bayar.ReadOnly = True
        Nomor_JV_Bayar.Visible = False
        Nomor_JV_Bayar.Width = 125
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(564, 32)
        lbl_JudulForm.TabIndex = 10080
        lbl_JudulForm.Text = "Buku Pengawasan Hutang PPh Pasal 21"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 90)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 48)
        btn_Refresh.TabIndex = 10078
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' txt_SaldoAwalBerdasarkanCOA
        ' 
        txt_SaldoAwalBerdasarkanCOA.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_SaldoAwalBerdasarkanCOA.Location = New Point(254, 645)
        txt_SaldoAwalBerdasarkanCOA.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoAwalBerdasarkanCOA.Name = "txt_SaldoAwalBerdasarkanCOA"
        txt_SaldoAwalBerdasarkanCOA.Size = New Size(125, 23)
        txt_SaldoAwalBerdasarkanCOA.TabIndex = 10089
        txt_SaldoAwalBerdasarkanCOA.TextAlign = HorizontalAlignment.Right
        ' 
        ' btn_DetailGajiDanPesangon
        ' 
        btn_DetailGajiDanPesangon.Location = New Point(115, 90)
        btn_DetailGajiDanPesangon.Margin = New Padding(4, 3, 4, 3)
        btn_DetailGajiDanPesangon.Name = "btn_DetailGajiDanPesangon"
        btn_DetailGajiDanPesangon.Size = New Size(265, 48)
        btn_DetailGajiDanPesangon.TabIndex = 10093
        btn_DetailGajiDanPesangon.Text = "Detail Gaji dan Pesangon"
        btn_DetailGajiDanPesangon.UseVisualStyleBackColor = True
        ' 
        ' btn_DetailPembayaran
        ' 
        btn_DetailPembayaran.Location = New Point(807, 17)
        btn_DetailPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_DetailPembayaran.Name = "btn_DetailPembayaran"
        btn_DetailPembayaran.Size = New Size(97, 52)
        btn_DetailPembayaran.TabIndex = 10094
        btn_DetailPembayaran.Text = "Detail Pembayaran"
        btn_DetailPembayaran.UseVisualStyleBackColor = True
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(510, 86)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10095
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuPengawasanHutangPPhPasal21
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 682)
        Controls.Add(btn_Export)
        Controls.Add(btn_DetailPembayaran)
        Controls.Add(btn_DetailGajiDanPesangon)
        Controls.Add(lbl_MasaPajak)
        Controls.Add(cmb_MasaPajak)
        Controls.Add(lbl_TahunPajak)
        Controls.Add(cmb_TahunPajak)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(pnl_CRUD)
        Controls.Add(lbl_AJP)
        Controls.Add(txt_AJP)
        Controls.Add(lbl_SaldoAwalBerdasarkanCOA)
        Controls.Add(grb_InfoSaldo)
        Controls.Add(DataTabelUtama)
        Controls.Add(grb_Pembayaran)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(txt_SaldoAwalBerdasarkanCOA)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanHutangPPhPasal21"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Hutang PPh Pasal 21"
        WindowState = FormWindowState.Maximized
        pnl_CRUD.ResumeLayout(False)
        grb_InfoSaldo.ResumeLayout(False)
        grb_InfoSaldo.PerformLayout()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        grb_Pembayaran.ResumeLayout(False)
        grb_Pembayaran.PerformLayout()
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_EditPembayaran As Button
    Friend WithEvents btn_InputPembayaran As Button
    Friend WithEvents lbl_MasaPajak As Label
    Friend WithEvents cmb_MasaPajak As ComboBox
    Friend WithEvents lbl_TahunPajak As Label
    Friend WithEvents cmb_TahunPajak As ComboBox
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents pnl_CRUD As Panel
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_SelisihSaldo As TextBox
    Friend WithEvents lbl_SaldoBerdasarkanCOA_PlusAJP As Label
    Friend WithEvents btn_Sesuaikan As Button
    Friend WithEvents txt_SaldoBerdasarkanCOA_PlusPenyesuaian As TextBox
    Friend WithEvents txt_SaldoBerdasarkanList As TextBox
    Friend WithEvents lbl_SaldoBerdasarkanList As Label
    Friend WithEvents lbl_AJP As Label
    Friend WithEvents txt_AJP As TextBox
    Friend WithEvents lbl_SaldoAwalBerdasarkanCOA As Label
    Friend WithEvents grb_InfoSaldo As GroupBox
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents grb_Pembayaran As GroupBox
    Friend WithEvents btn_HapusPembayaran As Button
    Friend WithEvents dgv_DetailBayar As DataGridView
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents txt_SaldoAwalBerdasarkanCOA As TextBox
    Friend WithEvents btn_DetailGajiDanPesangon As Button
    Friend WithEvents rdb_KodeSetoran_401 As RadioButton
    Friend WithEvents rdb_KodeSetoran_100 As RadioButton
    Friend WithEvents btn_DetailPembayaran As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPHP As DataGridViewTextBoxColumn
    Friend WithEvents Bulan_ As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Jasa As DataGridViewTextBoxColumn
    Friend WithEvents NPWP_ As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Supplier As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Supplier As DataGridViewTextBoxColumn
    Friend WithEvents DPP_ As DataGridViewTextBoxColumn
    Friend WithEvents Bruto_Jasa_OP As DataGridViewTextBoxColumn
    Friend WithEvents Bruto_Gaji As DataGridViewTextBoxColumn
    Friend WithEvents Bruto_Pesangon As DataGridViewTextBoxColumn
    Friend WithEvents Bruto_ As DataGridViewTextBoxColumn
    Friend WithEvents Jasa_OP As DataGridViewTextBoxColumn
    Friend WithEvents Gaji_ As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Pasal_21_100 As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Pasal_21_401_Pesangon As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Pasal_21 As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar_100 As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar_401 As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Sisa_Hutang_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Referensi_ As DataGridViewTextBoxColumn
    Friend WithEvents Nominal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents btn_Export As Button
End Class
