<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_BukuPengawasanPelaporanPPN
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
        Dim DataGridViewCellStyle19 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As DataGridViewCellStyle = New DataGridViewCellStyle()
        btn_EditPembayaran = New Button()
        btn_InputPembayaran = New Button()
        lbl_MasaPajak = New Label()
        cmb_MasaPajak = New ComboBox()
        lbl_TahunPajak = New Label()
        cmb_TahunPajak = New ComboBox()
        btn_LihatJurnal = New Button()
        btn_TambahSPT = New Button()
        btn_EditSPT = New Button()
        btn_HapusSPT = New Button()
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
        Tanggal_Lapor = New DataGridViewTextBoxColumn()
        Nomor_ID_Lapor = New DataGridViewTextBoxColumn()
        TW_TL_Lapor = New DataGridViewTextBoxColumn()
        N_P_Lapor = New DataGridViewTextBoxColumn()
        Pajak_Keluaran_Dibayar = New DataGridViewTextBoxColumn()
        Pajak_Keluaran_Dipungut = New DataGridViewTextBoxColumn()
        Pajak_Keluaran_Tidak_Dipungut = New DataGridViewTextBoxColumn()
        Pajak_Keluaran_Retur = New DataGridViewTextBoxColumn()
        Pajak_Keluaran_Jumlah = New DataGridViewTextBoxColumn()
        Pajak_Masukan_Impor = New DataGridViewTextBoxColumn()
        Pajak_Masukan_Dalam_Negeri = New DataGridViewTextBoxColumn()
        Pajak_Masukan_Retur = New DataGridViewTextBoxColumn()
        Pajak_Masukan_Kompensasi_Sebelumnya = New DataGridViewTextBoxColumn()
        Pajak_Masukan_Kompensasi_Pembetulan = New DataGridViewTextBoxColumn()
        Pajak_Masukan_Jumlah = New DataGridViewTextBoxColumn()
        PPN_NKL = New DataGridViewTextBoxColumn()
        Selisih_Pembetulan = New DataGridViewTextBoxColumn()
        Kompensasi_Ke = New DataGridViewTextBoxColumn()
        Jumlah_Bayar = New DataGridViewTextBoxColumn()
        Sisa_Hutang = New DataGridViewTextBoxColumn()
        PPN_Tidak_Dapat_Dikreditkan = New DataGridViewTextBoxColumn()
        Peredaran_Usaha_Lokal = New DataGridViewTextBoxColumn()
        Peredaran_Usaha_Ekspor = New DataGridViewTextBoxColumn()
        Retur_Penjualan = New DataGridViewTextBoxColumn()
        Peredaran_Usaha_Jumlah = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        grb_Pembayaran = New GroupBox()
        btn_HapusPembayaran = New Button()
        dgv_DetailBayar = New DataGridView()
        Nomor_ID_Bayar = New DataGridViewTextBoxColumn()
        Tanggal_Bayar = New DataGridViewTextBoxColumn()
        Referensi_ = New DataGridViewTextBoxColumn()
        Nominal_Bayar = New DataGridViewTextBoxColumn()
        TW_TL_Bayar = New DataGridViewTextBoxColumn()
        Keterangan_Bayar = New DataGridViewTextBoxColumn()
        Nomor_JV_Bayar = New DataGridViewTextBoxColumn()
        lbl_JudulForm = New Label()
        btn_Refresh = New Button()
        txt_SaldoAwalBerdasarkanCOA = New TextBox()
        btn_HasilAkhir = New Button()
        grb_LaporSPT = New GroupBox()
        btn_Export = New Button()
        grb_InfoSaldo.SuspendLayout()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        grb_Pembayaran.SuspendLayout()
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).BeginInit()
        grb_LaporSPT.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_EditPembayaran
        ' 
        btn_EditPembayaran.Enabled = False
        btn_EditPembayaran.Location = New Point(13, 58)
        btn_EditPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_EditPembayaran.Name = "btn_EditPembayaran"
        btn_EditPembayaran.Size = New Size(94, 32)
        btn_EditPembayaran.TabIndex = 10061
        btn_EditPembayaran.Text = "Edit"
        btn_EditPembayaran.UseVisualStyleBackColor = True
        ' 
        ' btn_InputPembayaran
        ' 
        btn_InputPembayaran.Location = New Point(13, 22)
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
        lbl_MasaPajak.Enabled = False
        lbl_MasaPajak.Location = New Point(197, 62)
        lbl_MasaPajak.Margin = New Padding(4, 0, 4, 0)
        lbl_MasaPajak.Name = "lbl_MasaPajak"
        lbl_MasaPajak.Size = New Size(72, 15)
        lbl_MasaPajak.TabIndex = 10086
        lbl_MasaPajak.Text = "Masa Pajak :"
        ' 
        ' cmb_MasaPajak
        ' 
        cmb_MasaPajak.Enabled = False
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
        lbl_TahunPajak.Enabled = False
        lbl_TahunPajak.Location = New Point(16, 62)
        lbl_TahunPajak.Margin = New Padding(4, 0, 4, 0)
        lbl_TahunPajak.Name = "lbl_TahunPajak"
        lbl_TahunPajak.Size = New Size(45, 15)
        lbl_TahunPajak.TabIndex = 10084
        lbl_TahunPajak.Text = "Tahun :"
        ' 
        ' cmb_TahunPajak
        ' 
        cmb_TahunPajak.Enabled = False
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
        btn_LihatJurnal.Size = New Size(97, 40)
        btn_LihatJurnal.TabIndex = 10082
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_TambahSPT.Location = New Point(7, 20)
        btn_TambahSPT.Margin = New Padding(4, 3, 4, 3)
        btn_TambahSPT.Name = "btn_Tambah"
        btn_TambahSPT.Size = New Size(97, 42)
        btn_TambahSPT.TabIndex = 10069
        btn_TambahSPT.Text = "Input"
        btn_TambahSPT.UseVisualStyleBackColor = True
        ' 
        ' btn_Edit
        ' 
        btn_EditSPT.Location = New Point(111, 20)
        btn_EditSPT.Margin = New Padding(4, 3, 4, 3)
        btn_EditSPT.Name = "btn_Edit"
        btn_EditSPT.Size = New Size(97, 42)
        btn_EditSPT.TabIndex = 10071
        btn_EditSPT.Text = "Edit"
        btn_EditSPT.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_HapusSPT.Location = New Point(215, 20)
        btn_HapusSPT.Margin = New Padding(4, 3, 4, 3)
        btn_HapusSPT.Name = "btn_Hapus"
        btn_HapusSPT.Size = New Size(97, 42)
        btn_HapusSPT.TabIndex = 10070
        btn_HapusSPT.Text = "Hapus"
        btn_HapusSPT.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(14, 85)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(40, 15)
        Label1.TabIndex = 10044
        Label1.Text = "Selisih"
        ' 
        ' txt_SelisihSaldo
        ' 
        txt_SelisihSaldo.Location = New Point(166, 82)
        txt_SelisihSaldo.Margin = New Padding(4, 3, 4, 3)
        txt_SelisihSaldo.Name = "txt_SelisihSaldo"
        txt_SelisihSaldo.Size = New Size(125, 23)
        txt_SelisihSaldo.TabIndex = 10045
        txt_SelisihSaldo.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_SaldoBerdasarkanCOA_PlusAJP
        ' 
        lbl_SaldoBerdasarkanCOA_PlusAJP.AutoSize = True
        lbl_SaldoBerdasarkanCOA_PlusAJP.Location = New Point(14, 55)
        lbl_SaldoBerdasarkanCOA_PlusAJP.Margin = New Padding(4, 0, 4, 0)
        lbl_SaldoBerdasarkanCOA_PlusAJP.Name = "lbl_SaldoBerdasarkanCOA_PlusAJP"
        lbl_SaldoBerdasarkanCOA_PlusAJP.Size = New Size(133, 15)
        lbl_SaldoBerdasarkanCOA_PlusAJP.TabIndex = 10042
        lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        ' 
        ' btn_Sesuaikan
        ' 
        btn_Sesuaikan.Location = New Point(302, 20)
        btn_Sesuaikan.Margin = New Padding(4, 3, 4, 3)
        btn_Sesuaikan.Name = "btn_Sesuaikan"
        btn_Sesuaikan.Size = New Size(94, 87)
        btn_Sesuaikan.TabIndex = 10040
        btn_Sesuaikan.Text = "Sesuaikan"
        btn_Sesuaikan.UseVisualStyleBackColor = True
        ' 
        ' txt_SaldoBerdasarkanCOA_PlusPenyesuaian
        ' 
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Location = New Point(166, 52)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Name = "txt_SaldoBerdasarkanCOA_PlusPenyesuaian"
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Size = New Size(125, 23)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TabIndex = 10043
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TextAlign = HorizontalAlignment.Right
        ' 
        ' txt_SaldoBerdasarkanList
        ' 
        txt_SaldoBerdasarkanList.Location = New Point(166, 22)
        txt_SaldoBerdasarkanList.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoBerdasarkanList.Name = "txt_SaldoBerdasarkanList"
        txt_SaldoBerdasarkanList.Size = New Size(125, 23)
        txt_SaldoBerdasarkanList.TabIndex = 10040
        txt_SaldoBerdasarkanList.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_SaldoBerdasarkanList
        ' 
        lbl_SaldoBerdasarkanList.AutoSize = True
        lbl_SaldoBerdasarkanList.Location = New Point(14, 25)
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
        lbl_SaldoAwalBerdasarkanCOA.Size = New Size(168, 15)
        lbl_SaldoAwalBerdasarkanCOA.TabIndex = 10090
        lbl_SaldoAwalBerdasarkanCOA.Text = "Saldo Awal COA Hutang PPN :"
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPHP, Bulan_, Tanggal_Lapor, Nomor_ID_Lapor, TW_TL_Lapor, N_P_Lapor, Pajak_Keluaran_Dibayar, Pajak_Keluaran_Dipungut, Pajak_Keluaran_Tidak_Dipungut, Pajak_Keluaran_Retur, Pajak_Keluaran_Jumlah, Pajak_Masukan_Impor, Pajak_Masukan_Dalam_Negeri, Pajak_Masukan_Retur, Pajak_Masukan_Kompensasi_Sebelumnya, Pajak_Masukan_Kompensasi_Pembetulan, Pajak_Masukan_Jumlah, PPN_NKL, Selisih_Pembetulan, Kompensasi_Ke, Jumlah_Bayar, Sisa_Hutang, PPN_Tidak_Dapat_Dikreditkan, Peredaran_Usaha_Lokal, Peredaran_Usaha_Ekspor, Retur_Penjualan, Peredaran_Usaha_Jumlah, Keterangan_})
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
        Nomor_BPHP.Visible = False
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
        ' Tanggal_Lapor
        ' 
        Tanggal_Lapor.HeaderText = "Lapor"
        Tanggal_Lapor.Name = "Tanggal_Lapor"
        Tanggal_Lapor.ReadOnly = True
        Tanggal_Lapor.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Lapor.Width = 63
        ' 
        ' Nomor_ID_Lapor
        ' 
        Nomor_ID_Lapor.HeaderText = "Nomor ID Lapor"
        Nomor_ID_Lapor.Name = "Nomor_ID_Lapor"
        Nomor_ID_Lapor.ReadOnly = True
        Nomor_ID_Lapor.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID_Lapor.Visible = False
        ' 
        ' TW_TL_Lapor
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        TW_TL_Lapor.DefaultCellStyle = DataGridViewCellStyle2
        TW_TL_Lapor.HeaderText = "TW TL"
        TW_TL_Lapor.Name = "TW_TL_Lapor"
        TW_TL_Lapor.ReadOnly = True
        TW_TL_Lapor.SortMode = DataGridViewColumnSortMode.NotSortable
        TW_TL_Lapor.Width = 33
        ' 
        ' N_P_Lapor
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        N_P_Lapor.DefaultCellStyle = DataGridViewCellStyle3
        N_P_Lapor.HeaderText = "N/P"
        N_P_Lapor.Name = "N_P_Lapor"
        N_P_Lapor.ReadOnly = True
        N_P_Lapor.SortMode = DataGridViewColumnSortMode.NotSortable
        N_P_Lapor.Width = 33
        ' 
        ' Pajak_Keluaran_Dibayar
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Pajak_Keluaran_Dibayar.DefaultCellStyle = DataGridViewCellStyle4
        Pajak_Keluaran_Dibayar.HeaderText = "Dibayar"
        Pajak_Keluaran_Dibayar.Name = "Pajak_Keluaran_Dibayar"
        Pajak_Keluaran_Dibayar.ReadOnly = True
        Pajak_Keluaran_Dibayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Keluaran_Dibayar.Width = 81
        ' 
        ' Pajak_Keluaran_Dipungut
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Pajak_Keluaran_Dipungut.DefaultCellStyle = DataGridViewCellStyle5
        Pajak_Keluaran_Dipungut.HeaderText = "Dipungut"
        Pajak_Keluaran_Dipungut.Name = "Pajak_Keluaran_Dipungut"
        Pajak_Keluaran_Dipungut.ReadOnly = True
        Pajak_Keluaran_Dipungut.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Keluaran_Dipungut.Width = 81
        ' 
        ' Pajak_Keluaran_Tidak_Dipungut
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Pajak_Keluaran_Tidak_Dipungut.DefaultCellStyle = DataGridViewCellStyle6
        Pajak_Keluaran_Tidak_Dipungut.HeaderText = "Tidak Dipungut"
        Pajak_Keluaran_Tidak_Dipungut.Name = "Pajak_Keluaran_Tidak_Dipungut"
        Pajak_Keluaran_Tidak_Dipungut.ReadOnly = True
        Pajak_Keluaran_Tidak_Dipungut.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Keluaran_Tidak_Dipungut.Width = 81
        ' 
        ' Pajak_Keluaran_Retur
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Pajak_Keluaran_Retur.DefaultCellStyle = DataGridViewCellStyle7
        Pajak_Keluaran_Retur.HeaderText = "Retur Keluaran"
        Pajak_Keluaran_Retur.Name = "Pajak_Keluaran_Retur"
        Pajak_Keluaran_Retur.ReadOnly = True
        Pajak_Keluaran_Retur.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Keluaran_Retur.Width = 81
        ' 
        ' Pajak_Keluaran_Jumlah
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Pajak_Keluaran_Jumlah.DefaultCellStyle = DataGridViewCellStyle8
        Pajak_Keluaran_Jumlah.HeaderText = "Jumlah Keluaran"
        Pajak_Keluaran_Jumlah.Name = "Pajak_Keluaran_Jumlah"
        Pajak_Keluaran_Jumlah.ReadOnly = True
        Pajak_Keluaran_Jumlah.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Keluaran_Jumlah.Width = 81
        ' 
        ' Pajak_Masukan_Impor
        ' 
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        Pajak_Masukan_Impor.DefaultCellStyle = DataGridViewCellStyle9
        Pajak_Masukan_Impor.HeaderText = "Impor"
        Pajak_Masukan_Impor.Name = "Pajak_Masukan_Impor"
        Pajak_Masukan_Impor.ReadOnly = True
        Pajak_Masukan_Impor.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Masukan_Impor.Width = 81
        ' 
        ' Pajak_Masukan_Dalam_Negeri
        ' 
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        Pajak_Masukan_Dalam_Negeri.DefaultCellStyle = DataGridViewCellStyle10
        Pajak_Masukan_Dalam_Negeri.HeaderText = "Dalam Negeri"
        Pajak_Masukan_Dalam_Negeri.Name = "Pajak_Masukan_Dalam_Negeri"
        Pajak_Masukan_Dalam_Negeri.ReadOnly = True
        Pajak_Masukan_Dalam_Negeri.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Masukan_Dalam_Negeri.Width = 81
        ' 
        ' Pajak_Masukan_Retur
        ' 
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        Pajak_Masukan_Retur.DefaultCellStyle = DataGridViewCellStyle11
        Pajak_Masukan_Retur.HeaderText = "Retur Masukan"
        Pajak_Masukan_Retur.Name = "Pajak_Masukan_Retur"
        Pajak_Masukan_Retur.ReadOnly = True
        Pajak_Masukan_Retur.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Masukan_Retur.Width = 81
        ' 
        ' Pajak_Masukan_Kompensasi_Sebelumnya
        ' 
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N0"
        Pajak_Masukan_Kompensasi_Sebelumnya.DefaultCellStyle = DataGridViewCellStyle12
        Pajak_Masukan_Kompensasi_Sebelumnya.HeaderText = "Kompensasi Sebelumnya"
        Pajak_Masukan_Kompensasi_Sebelumnya.Name = "Pajak_Masukan_Kompensasi_Sebelumnya"
        Pajak_Masukan_Kompensasi_Sebelumnya.ReadOnly = True
        Pajak_Masukan_Kompensasi_Sebelumnya.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Masukan_Kompensasi_Sebelumnya.Width = 81
        ' 
        ' Pajak_Masukan_Kompensasi_Pembetulan
        ' 
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N0"
        Pajak_Masukan_Kompensasi_Pembetulan.DefaultCellStyle = DataGridViewCellStyle13
        Pajak_Masukan_Kompensasi_Pembetulan.HeaderText = "Kompensasi Pembetulan"
        Pajak_Masukan_Kompensasi_Pembetulan.Name = "Pajak_Masukan_Kompensasi_Pembetulan"
        Pajak_Masukan_Kompensasi_Pembetulan.ReadOnly = True
        Pajak_Masukan_Kompensasi_Pembetulan.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Masukan_Kompensasi_Pembetulan.Width = 81
        ' 
        ' Pajak_Masukan_Jumlah
        ' 
        DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N0"
        Pajak_Masukan_Jumlah.DefaultCellStyle = DataGridViewCellStyle14
        Pajak_Masukan_Jumlah.HeaderText = "Jumlah Masukan"
        Pajak_Masukan_Jumlah.Name = "Pajak_Masukan_Jumlah"
        Pajak_Masukan_Jumlah.ReadOnly = True
        Pajak_Masukan_Jumlah.SortMode = DataGridViewColumnSortMode.NotSortable
        Pajak_Masukan_Jumlah.Width = 81
        ' 
        ' PPN_NKL
        ' 
        DataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Format = "N0"
        PPN_NKL.DefaultCellStyle = DataGridViewCellStyle15
        PPN_NKL.HeaderText = "PPN NKL"
        PPN_NKL.Name = "PPN_NKL"
        PPN_NKL.ReadOnly = True
        PPN_NKL.SortMode = DataGridViewColumnSortMode.NotSortable
        PPN_NKL.Width = 81
        ' 
        ' Selisih_Pembetulan
        ' 
        DataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.Format = "N0"
        Selisih_Pembetulan.DefaultCellStyle = DataGridViewCellStyle16
        Selisih_Pembetulan.HeaderText = "Selisih Pembetulan"
        Selisih_Pembetulan.Name = "Selisih_Pembetulan"
        Selisih_Pembetulan.ReadOnly = True
        Selisih_Pembetulan.SortMode = DataGridViewColumnSortMode.NotSortable
        Selisih_Pembetulan.Width = 99
        ' 
        ' Kompensasi_Ke
        ' 
        Kompensasi_Ke.HeaderText = "Kompensasi Ke"
        Kompensasi_Ke.Name = "Kompensasi_Ke"
        Kompensasi_Ke.ReadOnly = True
        Kompensasi_Ke.SortMode = DataGridViewColumnSortMode.NotSortable
        Kompensasi_Ke.Width = 63
        ' 
        ' Jumlah_Bayar
        ' 
        DataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "N0"
        Jumlah_Bayar.DefaultCellStyle = DataGridViewCellStyle17
        Jumlah_Bayar.HeaderText = "Jumlah Bayar"
        Jumlah_Bayar.Name = "Jumlah_Bayar"
        Jumlah_Bayar.ReadOnly = True
        Jumlah_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar.Width = 81
        ' 
        ' Sisa_Hutang
        ' 
        DataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "N0"
        Sisa_Hutang.DefaultCellStyle = DataGridViewCellStyle18
        Sisa_Hutang.HeaderText = "Sisa Hutang"
        Sisa_Hutang.Name = "Sisa_Hutang"
        Sisa_Hutang.ReadOnly = True
        Sisa_Hutang.SortMode = DataGridViewColumnSortMode.NotSortable
        Sisa_Hutang.Width = 81
        ' 
        ' PPN_Tidak_Dapat_Dikreditkan
        ' 
        DataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle19.Format = "N0"
        PPN_Tidak_Dapat_Dikreditkan.DefaultCellStyle = DataGridViewCellStyle19
        PPN_Tidak_Dapat_Dikreditkan.HeaderText = "PPN Tidak Dapat Dikreditkan"
        PPN_Tidak_Dapat_Dikreditkan.Name = "PPN_Tidak_Dapat_Dikreditkan"
        PPN_Tidak_Dapat_Dikreditkan.ReadOnly = True
        PPN_Tidak_Dapat_Dikreditkan.SortMode = DataGridViewColumnSortMode.NotSortable
        PPN_Tidak_Dapat_Dikreditkan.Width = 81
        ' 
        ' Peredaran_Usaha_Lokal
        ' 
        DataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle20.Format = "N0"
        Peredaran_Usaha_Lokal.DefaultCellStyle = DataGridViewCellStyle20
        Peredaran_Usaha_Lokal.HeaderText = "Peredaran Usaha Lokal"
        Peredaran_Usaha_Lokal.Name = "Peredaran_Usaha_Lokal"
        Peredaran_Usaha_Lokal.ReadOnly = True
        Peredaran_Usaha_Lokal.SortMode = DataGridViewColumnSortMode.NotSortable
        Peredaran_Usaha_Lokal.Width = 81
        ' 
        ' Peredaran_Usaha_Ekspor
        ' 
        DataGridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle21.Format = "N0"
        Peredaran_Usaha_Ekspor.DefaultCellStyle = DataGridViewCellStyle21
        Peredaran_Usaha_Ekspor.HeaderText = "Peredaran Usaha Ekspor"
        Peredaran_Usaha_Ekspor.Name = "Peredaran_Usaha_Ekspor"
        Peredaran_Usaha_Ekspor.ReadOnly = True
        Peredaran_Usaha_Ekspor.SortMode = DataGridViewColumnSortMode.NotSortable
        Peredaran_Usaha_Ekspor.Width = 81
        ' 
        ' Retur_Penjualan
        ' 
        DataGridViewCellStyle22.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle22.Format = "N0"
        Retur_Penjualan.DefaultCellStyle = DataGridViewCellStyle22
        Retur_Penjualan.HeaderText = "Retur Penjualan"
        Retur_Penjualan.Name = "Retur_Penjualan"
        Retur_Penjualan.ReadOnly = True
        Retur_Penjualan.SortMode = DataGridViewColumnSortMode.NotSortable
        Retur_Penjualan.Width = 81
        ' 
        ' Peredaran_Usaha_Jumlah
        ' 
        DataGridViewCellStyle23.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle23.Format = "N0"
        Peredaran_Usaha_Jumlah.DefaultCellStyle = DataGridViewCellStyle23
        Peredaran_Usaha_Jumlah.HeaderText = "Jumlah Peredaran Usaha"
        Peredaran_Usaha_Jumlah.Name = "Peredaran_Usaha_Jumlah"
        Peredaran_Usaha_Jumlah.ReadOnly = True
        Peredaran_Usaha_Jumlah.SortMode = DataGridViewColumnSortMode.NotSortable
        Peredaran_Usaha_Jumlah.Width = 81
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
        ' btn_HapusPembayaran
        ' 
        btn_HapusPembayaran.Enabled = False
        btn_HapusPembayaran.Location = New Point(13, 93)
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
        dgv_DetailBayar.Columns.AddRange(New DataGridViewColumn() {Nomor_ID_Bayar, Tanggal_Bayar, Referensi_, Nominal_Bayar, TW_TL_Bayar, Keterangan_Bayar, Nomor_JV_Bayar})
        dgv_DetailBayar.Location = New Point(120, 22)
        dgv_DetailBayar.Margin = New Padding(4, 3, 4, 3)
        dgv_DetailBayar.MultiSelect = False
        dgv_DetailBayar.Name = "dgv_DetailBayar"
        dgv_DetailBayar.ReadOnly = True
        dgv_DetailBayar.RowHeadersVisible = False
        dgv_DetailBayar.RowHeadersWidth = 33
        dgv_DetailBayar.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_DetailBayar.Size = New Size(359, 102)
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
        DataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Bayar.DefaultCellStyle = DataGridViewCellStyle24
        Tanggal_Bayar.HeaderText = "Tgl. Bayar"
        Tanggal_Bayar.MinimumWidth = 6
        Tanggal_Bayar.Name = "Tanggal_Bayar"
        Tanggal_Bayar.ReadOnly = True
        Tanggal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Bayar.Width = 63
        ' 
        ' Referensi_
        ' 
        DataGridViewCellStyle25.Alignment = DataGridViewContentAlignment.MiddleLeft
        Referensi_.DefaultCellStyle = DataGridViewCellStyle25
        Referensi_.HeaderText = "Referensi"
        Referensi_.MinimumWidth = 6
        Referensi_.Name = "Referensi_"
        Referensi_.ReadOnly = True
        Referensi_.SortMode = DataGridViewColumnSortMode.NotSortable
        Referensi_.Width = 93
        ' 
        ' Nominal_Bayar
        ' 
        DataGridViewCellStyle26.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle26.Format = "N0"
        DataGridViewCellStyle26.NullValue = Nothing
        Nominal_Bayar.DefaultCellStyle = DataGridViewCellStyle26
        Nominal_Bayar.HeaderText = "Jumlah Bayar"
        Nominal_Bayar.MinimumWidth = 6
        Nominal_Bayar.Name = "Nominal_Bayar"
        Nominal_Bayar.ReadOnly = True
        Nominal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Nominal_Bayar.Width = 99
        ' 
        ' TW_TL_Bayar
        ' 
        DataGridViewCellStyle27.Alignment = DataGridViewContentAlignment.MiddleCenter
        TW_TL_Bayar.DefaultCellStyle = DataGridViewCellStyle27
        TW_TL_Bayar.HeaderText = "TW/TL"
        TW_TL_Bayar.Name = "TW_TL_Bayar"
        TW_TL_Bayar.ReadOnly = True
        TW_TL_Bayar.Width = 48
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
        lbl_JudulForm.Size = New Size(483, 32)
        lbl_JudulForm.TabIndex = 10080
        lbl_JudulForm.Text = "Buku Pengawasan Pelaporan PPN"
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
        ' btn_HasilAkhir
        ' 
        btn_HasilAkhir.Location = New Point(115, 90)
        btn_HasilAkhir.Margin = New Padding(4, 3, 4, 3)
        btn_HasilAkhir.Name = "btn_HasilAkhir"
        btn_HasilAkhir.Size = New Size(94, 48)
        btn_HasilAkhir.TabIndex = 10093
        btn_HasilAkhir.Text = "Hasil Akhir"
        btn_HasilAkhir.UseVisualStyleBackColor = True
        ' 
        ' grb_LaporSPT
        ' 
        grb_LaporSPT.Controls.Add(btn_TambahSPT)
        grb_LaporSPT.Controls.Add(btn_EditSPT)
        grb_LaporSPT.Controls.Add(btn_HapusSPT)
        grb_LaporSPT.Location = New Point(691, 68)
        grb_LaporSPT.Margin = New Padding(4, 3, 4, 3)
        grb_LaporSPT.Name = "grb_LaporSPT"
        grb_LaporSPT.Padding = New Padding(4, 3, 4, 3)
        grb_LaporSPT.Size = New Size(317, 70)
        grb_LaporSPT.TabIndex = 10094
        grb_LaporSPT.TabStop = False
        grb_LaporSPT.Text = "Lapor SPT :"
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(540, 86)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10095
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuPengawasanPelaporanPPN
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 682)
        Controls.Add(btn_Export)
        Controls.Add(grb_LaporSPT)
        Controls.Add(btn_HasilAkhir)
        Controls.Add(lbl_MasaPajak)
        Controls.Add(cmb_MasaPajak)
        Controls.Add(lbl_TahunPajak)
        Controls.Add(cmb_TahunPajak)
        Controls.Add(btn_LihatJurnal)
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
        Name = "frm_BukuPengawasanPelaporanPPN"
        Text = "Buku Pengawasan Pelaporan PPN"
        grb_InfoSaldo.ResumeLayout(False)
        grb_InfoSaldo.PerformLayout()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        grb_Pembayaran.ResumeLayout(False)
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).EndInit()
        grb_LaporSPT.ResumeLayout(False)
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
    Friend WithEvents btn_TambahSPT As Button
    Friend WithEvents btn_EditSPT As Button
    Friend WithEvents btn_HapusSPT As Button
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
    Friend WithEvents btn_HasilAkhir As Button
    Friend WithEvents grb_LaporSPT As GroupBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPHP As DataGridViewTextBoxColumn
    Friend WithEvents Bulan_ As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Lapor As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID_Lapor As DataGridViewTextBoxColumn
    Friend WithEvents TW_TL_Lapor As DataGridViewTextBoxColumn
    Friend WithEvents N_P_Lapor As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Keluaran_Dibayar As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Keluaran_Dipungut As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Keluaran_Tidak_Dipungut As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Keluaran_Retur As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Keluaran_Jumlah As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Masukan_Impor As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Masukan_Dalam_Negeri As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Masukan_Retur As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Masukan_Kompensasi_Sebelumnya As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Masukan_Kompensasi_Pembetulan As DataGridViewTextBoxColumn
    Friend WithEvents Pajak_Masukan_Jumlah As DataGridViewTextBoxColumn
    Friend WithEvents PPN_NKL As DataGridViewTextBoxColumn
    Friend WithEvents Selisih_Pembetulan As DataGridViewTextBoxColumn
    Friend WithEvents Kompensasi_Ke As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Sisa_Hutang As DataGridViewTextBoxColumn
    Friend WithEvents PPN_Tidak_Dapat_Dikreditkan As DataGridViewTextBoxColumn
    Friend WithEvents Peredaran_Usaha_Lokal As DataGridViewTextBoxColumn
    Friend WithEvents Peredaran_Usaha_Ekspor As DataGridViewTextBoxColumn
    Friend WithEvents Retur_Penjualan As DataGridViewTextBoxColumn
    Friend WithEvents Peredaran_Usaha_Jumlah As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Referensi_ As DataGridViewTextBoxColumn
    Friend WithEvents Nominal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents TW_TL_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents btn_Export As Button
End Class
