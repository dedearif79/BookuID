<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_BukuPengawasanKetetapanPajak_X
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
        lbl_JudulForm = New Label()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_BPHP = New DataGridViewTextBoxColumn()
        Kode_Akun_Pokok_Pajak = New DataGridViewTextBoxColumn()
        Nomor_ = New DataGridViewTextBoxColumn()
        Kode_Jenis_Pajak = New DataGridViewTextBoxColumn()
        Jenis_Pajak = New DataGridViewTextBoxColumn()
        Masa_Pajak_Awal = New DataGridViewTextBoxColumn()
        Masa_Pajak_Akhir = New DataGridViewTextBoxColumn()
        Masa_Pajak = New DataGridViewTextBoxColumn()
        Tahun_Pajak = New DataGridViewTextBoxColumn()
        Nomor_Ketetapan = New DataGridViewTextBoxColumn()
        Tanggal_Ketetapan = New DataGridViewTextBoxColumn()
        Pokok_Pajak = New DataGridViewTextBoxColumn()
        Sanksi_ = New DataGridViewTextBoxColumn()
        Jumlah_Ketetapan = New DataGridViewTextBoxColumn()
        Jumlah_Bayar_Pokok = New DataGridViewTextBoxColumn()
        Jumlah_Bayar_Sanksi = New DataGridViewTextBoxColumn()
        Jumlah_Bayar = New DataGridViewTextBoxColumn()
        Sisa_Tagihan = New DataGridViewTextBoxColumn()
        Status_Lunas = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        btn_Edit = New Button()
        btn_Hapus = New Button()
        btn_Input = New Button()
        grb_Pembayaran = New GroupBox()
        rdb_BayarSanksi = New RadioButton()
        rdb_BayarPokok = New RadioButton()
        btn_HapusPembayaran = New Button()
        dgv_DetailBayar = New DataGridView()
        Nomor_ID_Bayar = New DataGridViewTextBoxColumn()
        Tanggal_Bayar = New DataGridViewTextBoxColumn()
        Referensi_ = New DataGridViewTextBoxColumn()
        Nominal_Bayar = New DataGridViewTextBoxColumn()
        Keterangan_Bayar = New DataGridViewTextBoxColumn()
        Nomor_JV_Bayar = New DataGridViewTextBoxColumn()
        btn_EditPembayaran = New Button()
        btn_InputPembayaran = New Button()
        pnl_CRUD = New Panel()
        btn_DetailPembayaran = New Button()
        btn_LihatJurnal = New Button()
        grb_InfoSaldo = New GroupBox()
        Label1 = New Label()
        txt_SelisihSaldo = New TextBox()
        lbl_SaldoBerdasarkanCOA_PlusAJP = New Label()
        btn_Sesuaikan = New Button()
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian = New TextBox()
        txt_SaldoBerdasarkanList = New TextBox()
        lbl_SaldoBerdasarkanList = New Label()
        lbl_AJP = New Label()
        txt_AJP = New TextBox()
        txt_SaldoAwalBerdasarkanCOA = New TextBox()
        lbl_SaldoAwalBerdasarkanCOA = New Label()
        lbl_TahunPajak = New Label()
        cmb_TahunPajak = New ComboBox()
        Label2 = New Label()
        cmb_PilihanJenisPajak = New ComboBox()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        cmb_PilihanStatusLunas = New ComboBox()
        Label7 = New Label()
        Label8 = New Label()
        cmb_TahunPenerbitan = New ComboBox()
        lbl_Warning_3 = New Label()
        lbl_Warning_2 = New Label()
        lbl_Warning_1 = New Label()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        grb_Pembayaran.SuspendLayout()
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).BeginInit()
        pnl_CRUD.SuspendLayout()
        grb_InfoSaldo.SuspendLayout()
        SuspendLayout()
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(499, 32)
        lbl_JudulForm.TabIndex = 10036
        lbl_JudulForm.Text = "Buku Pengawasan Ketetapan Pajak"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10031
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPHP, Kode_Akun_Pokok_Pajak, Nomor_, Kode_Jenis_Pajak, Jenis_Pajak, Masa_Pajak_Awal, Masa_Pajak_Akhir, Masa_Pajak, Tahun_Pajak, Nomor_Ketetapan, Tanggal_Ketetapan, Pokok_Pajak, Sanksi_, Jumlah_Ketetapan, Jumlah_Bayar_Pokok, Jumlah_Bayar_Sanksi, Jumlah_Bayar, Sisa_Tagihan, Status_Lunas, Keterangan_, Nomor_JV})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 543)
        DataTabelUtama.TabIndex = 10035
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_Urut.HeaderText = "No"
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 45
        ' 
        ' Nomor_ID
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_ID.DefaultCellStyle = DataGridViewCellStyle2
        Nomor_ID.HeaderText = "Nomor ID"
        Nomor_ID.Name = "Nomor_ID"
        Nomor_ID.ReadOnly = True
        Nomor_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID.Visible = False
        Nomor_ID.Width = 45
        ' 
        ' Nomor_BPHP
        ' 
        Nomor_BPHP.HeaderText = "Nomor BPHP"
        Nomor_BPHP.Name = "Nomor_BPHP"
        Nomor_BPHP.ReadOnly = True
        Nomor_BPHP.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPHP.Visible = False
        Nomor_BPHP.Width = 123
        ' 
        ' Kode_Akun_Pokok_Pajak
        ' 
        Kode_Akun_Pokok_Pajak.HeaderText = "Kode Akun Pokok Pajak"
        Kode_Akun_Pokok_Pajak.Name = "Kode_Akun_Pokok_Pajak"
        Kode_Akun_Pokok_Pajak.ReadOnly = True
        Kode_Akun_Pokok_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Akun_Pokok_Pajak.Visible = False
        Kode_Akun_Pokok_Pajak.Width = 63
        ' 
        ' Nomor_
        ' 
        Nomor_.HeaderText = "Nomor"
        Nomor_.Name = "Nomor_"
        Nomor_.ReadOnly = True
        Nomor_.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_.Visible = False
        ' 
        ' Kode_Jenis_Pajak
        ' 
        Kode_Jenis_Pajak.HeaderText = "Kode Jenis Pajak"
        Kode_Jenis_Pajak.Name = "Kode_Jenis_Pajak"
        Kode_Jenis_Pajak.ReadOnly = True
        Kode_Jenis_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Jenis_Pajak.Visible = False
        ' 
        ' Jenis_Pajak
        ' 
        Jenis_Pajak.HeaderText = "Jenis Pajak"
        Jenis_Pajak.Name = "Jenis_Pajak"
        Jenis_Pajak.ReadOnly = True
        Jenis_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Pajak.Width = 81
        ' 
        ' Masa_Pajak_Awal
        ' 
        Masa_Pajak_Awal.HeaderText = "Masa Pajak Awal"
        Masa_Pajak_Awal.Name = "Masa_Pajak_Awal"
        Masa_Pajak_Awal.ReadOnly = True
        Masa_Pajak_Awal.SortMode = DataGridViewColumnSortMode.NotSortable
        Masa_Pajak_Awal.Visible = False
        Masa_Pajak_Awal.Width = 72
        ' 
        ' Masa_Pajak_Akhir
        ' 
        Masa_Pajak_Akhir.HeaderText = "Masa Pajak Akhir"
        Masa_Pajak_Akhir.Name = "Masa_Pajak_Akhir"
        Masa_Pajak_Akhir.ReadOnly = True
        Masa_Pajak_Akhir.SortMode = DataGridViewColumnSortMode.NotSortable
        Masa_Pajak_Akhir.Visible = False
        Masa_Pajak_Akhir.Width = 72
        ' 
        ' Masa_Pajak
        ' 
        Masa_Pajak.HeaderText = "Masa Pajak"
        Masa_Pajak.Name = "Masa_Pajak"
        Masa_Pajak.ReadOnly = True
        Masa_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Masa_Pajak.Width = 123
        ' 
        ' Tahun_Pajak
        ' 
        Tahun_Pajak.HeaderText = "Tahun Pajak"
        Tahun_Pajak.Name = "Tahun_Pajak"
        Tahun_Pajak.ReadOnly = True
        Tahun_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Tahun_Pajak.Visible = False
        ' 
        ' Nomor_Ketetapan
        ' 
        Nomor_Ketetapan.HeaderText = "Nomor Ketetapan"
        Nomor_Ketetapan.Name = "Nomor_Ketetapan"
        Nomor_Ketetapan.ReadOnly = True
        Nomor_Ketetapan.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Ketetapan.Width = 150
        ' 
        ' Tanggal_Ketetapan
        ' 
        Tanggal_Ketetapan.HeaderText = "Tanggal Ketetapan"
        Tanggal_Ketetapan.Name = "Tanggal_Ketetapan"
        Tanggal_Ketetapan.ReadOnly = True
        Tanggal_Ketetapan.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Ketetapan.Width = 63
        ' 
        ' Pokok_Pajak
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Pokok_Pajak.DefaultCellStyle = DataGridViewCellStyle3
        Pokok_Pajak.HeaderText = "Pokok Pajak"
        Pokok_Pajak.Name = "Pokok_Pajak"
        Pokok_Pajak.ReadOnly = True
        Pokok_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Pokok_Pajak.Width = 81
        ' 
        ' Sanksi_
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Sanksi_.DefaultCellStyle = DataGridViewCellStyle4
        Sanksi_.HeaderText = "Sanksi"
        Sanksi_.Name = "Sanksi_"
        Sanksi_.ReadOnly = True
        Sanksi_.SortMode = DataGridViewColumnSortMode.NotSortable
        Sanksi_.Width = 81
        ' 
        ' Jumlah_Ketetapan
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Jumlah_Ketetapan.DefaultCellStyle = DataGridViewCellStyle5
        Jumlah_Ketetapan.HeaderText = "Jumlah Ketetapan"
        Jumlah_Ketetapan.Name = "Jumlah_Ketetapan"
        Jumlah_Ketetapan.ReadOnly = True
        Jumlah_Ketetapan.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Ketetapan.Width = 81
        ' 
        ' Jumlah_Bayar_Pokok
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Jumlah_Bayar_Pokok.DefaultCellStyle = DataGridViewCellStyle6
        Jumlah_Bayar_Pokok.HeaderText = "Jumlah Bayar Pokok"
        Jumlah_Bayar_Pokok.Name = "Jumlah_Bayar_Pokok"
        Jumlah_Bayar_Pokok.ReadOnly = True
        Jumlah_Bayar_Pokok.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar_Pokok.Width = 81
        ' 
        ' Jumlah_Bayar_Sanksi
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Jumlah_Bayar_Sanksi.DefaultCellStyle = DataGridViewCellStyle7
        Jumlah_Bayar_Sanksi.HeaderText = "Jumlah Bayar Sanksi"
        Jumlah_Bayar_Sanksi.Name = "Jumlah_Bayar_Sanksi"
        Jumlah_Bayar_Sanksi.ReadOnly = True
        Jumlah_Bayar_Sanksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar_Sanksi.Width = 81
        ' 
        ' Jumlah_Bayar
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Jumlah_Bayar.DefaultCellStyle = DataGridViewCellStyle8
        Jumlah_Bayar.HeaderText = "Jumlah Bayar"
        Jumlah_Bayar.Name = "Jumlah_Bayar"
        Jumlah_Bayar.ReadOnly = True
        Jumlah_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar.Width = 81
        ' 
        ' Sisa_Tagihan
        ' 
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        Sisa_Tagihan.DefaultCellStyle = DataGridViewCellStyle9
        Sisa_Tagihan.HeaderText = "Sisa Tagihan"
        Sisa_Tagihan.Name = "Sisa_Tagihan"
        Sisa_Tagihan.ReadOnly = True
        Sisa_Tagihan.SortMode = DataGridViewColumnSortMode.NotSortable
        Sisa_Tagihan.Width = 81
        ' 
        ' Status_Lunas
        ' 
        Status_Lunas.HeaderText = "Status"
        Status_Lunas.Name = "Status_Lunas"
        Status_Lunas.ReadOnly = True
        Status_Lunas.SortMode = DataGridViewColumnSortMode.NotSortable
        Status_Lunas.Width = 63
        ' 
        ' Keterangan_
        ' 
        Keterangan_.HeaderText = "Keterangan"
        Keterangan_.Name = "Keterangan_"
        Keterangan_.ReadOnly = True
        Keterangan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_.Width = 180
        ' 
        ' Nomor_JV
        ' 
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_JV.DefaultCellStyle = DataGridViewCellStyle10
        Nomor_JV.HeaderText = "Nomor JV"
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV.Visible = False
        Nomor_JV.Width = 45
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Edit.Location = New Point(105, 5)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10034
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Hapus.Location = New Point(209, 5)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10033
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Input
        ' 
        btn_Input.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Input.Location = New Point(1, 5)
        btn_Input.Margin = New Padding(4, 3, 4, 3)
        btn_Input.Name = "btn_Input"
        btn_Input.Size = New Size(97, 40)
        btn_Input.TabIndex = 10032
        btn_Input.Text = "Input"
        btn_Input.UseVisualStyleBackColor = True
        ' 
        ' grb_Pembayaran
        ' 
        grb_Pembayaran.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_Pembayaran.Controls.Add(rdb_BayarSanksi)
        grb_Pembayaran.Controls.Add(rdb_BayarPokok)
        grb_Pembayaran.Controls.Add(btn_HapusPembayaran)
        grb_Pembayaran.Controls.Add(dgv_DetailBayar)
        grb_Pembayaran.Controls.Add(btn_EditPembayaran)
        grb_Pembayaran.Controls.Add(btn_InputPembayaran)
        grb_Pembayaran.Location = New Point(1015, 5)
        grb_Pembayaran.Margin = New Padding(4, 3, 4, 3)
        grb_Pembayaran.Name = "grb_Pembayaran"
        grb_Pembayaran.Padding = New Padding(4, 3, 4, 3)
        grb_Pembayaran.Size = New Size(492, 134)
        grb_Pembayaran.TabIndex = 10082
        grb_Pembayaran.TabStop = False
        grb_Pembayaran.Text = "Pembayaran"
        grb_Pembayaran.Visible = False
        ' 
        ' rdb_BayarSanksi
        ' 
        rdb_BayarSanksi.AutoSize = True
        rdb_BayarSanksi.Location = New Point(211, 13)
        rdb_BayarSanksi.Margin = New Padding(4, 3, 4, 3)
        rdb_BayarSanksi.Name = "rdb_BayarSanksi"
        rdb_BayarSanksi.Size = New Size(58, 19)
        rdb_BayarSanksi.TabIndex = 10106
        rdb_BayarSanksi.TabStop = True
        rdb_BayarSanksi.Text = "Sanksi"
        rdb_BayarSanksi.UseVisualStyleBackColor = True
        ' 
        ' rdb_BayarPokok
        ' 
        rdb_BayarPokok.AutoSize = True
        rdb_BayarPokok.Location = New Point(121, 13)
        rdb_BayarPokok.Margin = New Padding(4, 3, 4, 3)
        rdb_BayarPokok.Name = "rdb_BayarPokok"
        rdb_BayarPokok.Size = New Size(58, 19)
        rdb_BayarPokok.TabIndex = 10105
        rdb_BayarPokok.TabStop = True
        rdb_BayarPokok.Text = "Pokok"
        rdb_BayarPokok.UseVisualStyleBackColor = True
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
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Bayar.DefaultCellStyle = DataGridViewCellStyle11
        Tanggal_Bayar.HeaderText = "Tgl. Bayar"
        Tanggal_Bayar.MinimumWidth = 6
        Tanggal_Bayar.Name = "Tanggal_Bayar"
        Tanggal_Bayar.ReadOnly = True
        Tanggal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Bayar.Width = 81
        ' 
        ' Referensi_
        ' 
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft
        Referensi_.DefaultCellStyle = DataGridViewCellStyle12
        Referensi_.HeaderText = "Referensi"
        Referensi_.MinimumWidth = 6
        Referensi_.Name = "Referensi_"
        Referensi_.ReadOnly = True
        Referensi_.SortMode = DataGridViewColumnSortMode.NotSortable
        Referensi_.Width = 123
        ' 
        ' Nominal_Bayar
        ' 
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N0"
        DataGridViewCellStyle13.NullValue = Nothing
        Nominal_Bayar.DefaultCellStyle = DataGridViewCellStyle13
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
        ' pnl_CRUD
        ' 
        pnl_CRUD.Controls.Add(btn_Input)
        pnl_CRUD.Controls.Add(btn_Hapus)
        pnl_CRUD.Controls.Add(btn_Edit)
        pnl_CRUD.Location = New Point(701, 90)
        pnl_CRUD.Margin = New Padding(2)
        pnl_CRUD.Name = "pnl_CRUD"
        pnl_CRUD.Size = New Size(307, 50)
        pnl_CRUD.TabIndex = 10095
        ' 
        ' btn_DetailPembayaran
        ' 
        btn_DetailPembayaran.Location = New Point(807, 17)
        btn_DetailPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_DetailPembayaran.Name = "btn_DetailPembayaran"
        btn_DetailPembayaran.Size = New Size(97, 52)
        btn_DetailPembayaran.TabIndex = 10097
        btn_DetailPembayaran.Text = "Detail Pembayaran"
        btn_DetailPembayaran.UseVisualStyleBackColor = True
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(911, 17)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 52)
        btn_LihatJurnal.TabIndex = 10096
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
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
        grb_InfoSaldo.Location = New Point(1086, 557)
        grb_InfoSaldo.Margin = New Padding(4, 3, 4, 3)
        grb_InfoSaldo.Name = "grb_InfoSaldo"
        grb_InfoSaldo.Padding = New Padding(4, 3, 4, 3)
        grb_InfoSaldo.Size = New Size(408, 118)
        grb_InfoSaldo.TabIndex = 10098
        grb_InfoSaldo.TabStop = False
        grb_InfoSaldo.Text = "Saldo :"
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
        lbl_AJP.Location = New Point(410, 760)
        lbl_AJP.Margin = New Padding(4, 0, 4, 0)
        lbl_AJP.Name = "lbl_AJP"
        lbl_AJP.Size = New Size(33, 15)
        lbl_AJP.TabIndex = 10102
        lbl_AJP.Text = "AJP :"
        ' 
        ' txt_AJP
        ' 
        txt_AJP.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_AJP.Location = New Point(458, 757)
        txt_AJP.Margin = New Padding(4, 3, 4, 3)
        txt_AJP.Name = "txt_AJP"
        txt_AJP.Size = New Size(125, 23)
        txt_AJP.TabIndex = 10101
        txt_AJP.TextAlign = HorizontalAlignment.Right
        ' 
        ' txt_SaldoAwalBerdasarkanCOA
        ' 
        txt_SaldoAwalBerdasarkanCOA.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_SaldoAwalBerdasarkanCOA.Location = New Point(253, 757)
        txt_SaldoAwalBerdasarkanCOA.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoAwalBerdasarkanCOA.Name = "txt_SaldoAwalBerdasarkanCOA"
        txt_SaldoAwalBerdasarkanCOA.Size = New Size(125, 23)
        txt_SaldoAwalBerdasarkanCOA.TabIndex = 10099
        txt_SaldoAwalBerdasarkanCOA.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_SaldoAwalBerdasarkanCOA
        ' 
        lbl_SaldoAwalBerdasarkanCOA.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lbl_SaldoAwalBerdasarkanCOA.AutoSize = True
        lbl_SaldoAwalBerdasarkanCOA.Location = New Point(15, 760)
        lbl_SaldoAwalBerdasarkanCOA.Margin = New Padding(4, 0, 4, 0)
        lbl_SaldoAwalBerdasarkanCOA.Name = "lbl_SaldoAwalBerdasarkanCOA"
        lbl_SaldoAwalBerdasarkanCOA.Size = New Size(211, 15)
        lbl_SaldoAwalBerdasarkanCOA.TabIndex = 10100
        lbl_SaldoAwalBerdasarkanCOA.Text = "Saldo Awal COA Hutang PPh Pasal 23 :"
        ' 
        ' lbl_TahunPajak
        ' 
        lbl_TahunPajak.AutoSize = True
        lbl_TahunPajak.Location = New Point(131, 77)
        lbl_TahunPajak.Margin = New Padding(4, 0, 4, 0)
        lbl_TahunPajak.Name = "lbl_TahunPajak"
        lbl_TahunPajak.Size = New Size(70, 15)
        lbl_TahunPajak.TabIndex = 10104
        lbl_TahunPajak.Text = "Tahun Pajak"
        ' 
        ' cmb_TahunPajak
        ' 
        cmb_TahunPajak.Enabled = False
        cmb_TahunPajak.FormattingEnabled = True
        cmb_TahunPajak.Location = New Point(251, 74)
        cmb_TahunPajak.Margin = New Padding(4, 3, 4, 3)
        cmb_TahunPajak.Name = "cmb_TahunPajak"
        cmb_TahunPajak.Size = New Size(67, 23)
        cmb_TahunPajak.TabIndex = 10103
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(358, 77)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(63, 15)
        Label2.TabIndex = 10106
        Label2.Text = "Jenis Pajak"
        ' 
        ' cmb_PilihanJenisPajak
        ' 
        cmb_PilihanJenisPajak.FormattingEnabled = True
        cmb_PilihanJenisPajak.Location = New Point(443, 74)
        cmb_PilihanJenisPajak.Margin = New Padding(4, 3, 4, 3)
        cmb_PilihanJenisPajak.Name = "cmb_PilihanJenisPajak"
        cmb_PilihanJenisPajak.Size = New Size(115, 23)
        cmb_PilihanJenisPajak.TabIndex = 10105
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(236, 77)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(10, 15)
        Label3.TabIndex = 10107
        Label3.Text = ":"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(428, 77)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(10, 15)
        Label4.TabIndex = 10108
        Label4.Text = ":"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(428, 113)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(10, 15)
        Label5.TabIndex = 10111
        Label5.Text = ":"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(358, 113)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(39, 15)
        Label6.TabIndex = 10110
        Label6.Text = "Status"
        ' 
        ' cmb_PilihanStatusLunas
        ' 
        cmb_PilihanStatusLunas.FormattingEnabled = True
        cmb_PilihanStatusLunas.Location = New Point(443, 110)
        cmb_PilihanStatusLunas.Margin = New Padding(4, 3, 4, 3)
        cmb_PilihanStatusLunas.Name = "cmb_PilihanStatusLunas"
        cmb_PilihanStatusLunas.Size = New Size(115, 23)
        cmb_PilihanStatusLunas.TabIndex = 10109
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(236, 113)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(10, 15)
        Label7.TabIndex = 10114
        Label7.Text = ":"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(131, 113)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(99, 15)
        Label8.TabIndex = 10113
        Label8.Text = "Tahun Penerbitan"
        ' 
        ' cmb_TahunPenerbitan
        ' 
        cmb_TahunPenerbitan.Enabled = False
        cmb_TahunPenerbitan.FormattingEnabled = True
        cmb_TahunPenerbitan.Location = New Point(251, 110)
        cmb_TahunPenerbitan.Margin = New Padding(4, 3, 4, 3)
        cmb_TahunPenerbitan.Name = "cmb_TahunPenerbitan"
        cmb_TahunPenerbitan.Size = New Size(67, 23)
        cmb_TahunPenerbitan.TabIndex = 10112
        ' 
        ' lbl_Warning_3
        ' 
        lbl_Warning_3.AutoSize = True
        lbl_Warning_3.ForeColor = Color.Red
        lbl_Warning_3.Location = New Point(699, 756)
        lbl_Warning_3.Margin = New Padding(4, 0, 4, 0)
        lbl_Warning_3.Name = "lbl_Warning_3"
        lbl_Warning_3.Size = New Size(61, 15)
        lbl_Warning_3.TabIndex = 10117
        lbl_Warning_3.Text = "Warning 3"
        ' 
        ' lbl_Warning_2
        ' 
        lbl_Warning_2.AutoSize = True
        lbl_Warning_2.ForeColor = Color.Red
        lbl_Warning_2.Location = New Point(699, 734)
        lbl_Warning_2.Margin = New Padding(4, 0, 4, 0)
        lbl_Warning_2.Name = "lbl_Warning_2"
        lbl_Warning_2.Size = New Size(276, 15)
        lbl_Warning_2.TabIndex = 10116
        lbl_Warning_2.Text = "Untuk Sanksi, jenis pajaknya mengikuti Kode Pajak."
        ' 
        ' lbl_Warning_1
        ' 
        lbl_Warning_1.AutoSize = True
        lbl_Warning_1.ForeColor = Color.Red
        lbl_Warning_1.Location = New Point(699, 714)
        lbl_Warning_1.Margin = New Padding(4, 0, 4, 0)
        lbl_Warning_1.Name = "lbl_Warning_1"
        lbl_Warning_1.Size = New Size(259, 15)
        lbl_Warning_1.TabIndex = 10115
        lbl_Warning_1.Text = "Masih ada masalah terkait Algoritma Jenis Pajak"
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(644, 18)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10118
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuPengawasanKetetapanPajak
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Export)
        Controls.Add(lbl_Warning_3)
        Controls.Add(lbl_Warning_2)
        Controls.Add(lbl_Warning_1)
        Controls.Add(Label7)
        Controls.Add(Label8)
        Controls.Add(cmb_TahunPenerbitan)
        Controls.Add(Label5)
        Controls.Add(Label6)
        Controls.Add(cmb_PilihanStatusLunas)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(cmb_PilihanJenisPajak)
        Controls.Add(lbl_TahunPajak)
        Controls.Add(cmb_TahunPajak)
        Controls.Add(lbl_AJP)
        Controls.Add(txt_AJP)
        Controls.Add(txt_SaldoAwalBerdasarkanCOA)
        Controls.Add(lbl_SaldoAwalBerdasarkanCOA)
        Controls.Add(grb_InfoSaldo)
        Controls.Add(btn_DetailPembayaran)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(pnl_CRUD)
        Controls.Add(grb_Pembayaran)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanKetetapanPajak"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Ketetapan Pajak"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        grb_Pembayaran.ResumeLayout(False)
        grb_Pembayaran.PerformLayout()
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).EndInit()
        pnl_CRUD.ResumeLayout(False)
        grb_InfoSaldo.ResumeLayout(False)
        grb_InfoSaldo.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Input As Button
    Friend WithEvents grb_Pembayaran As GroupBox
    Friend WithEvents btn_HapusPembayaran As Button
    Friend WithEvents dgv_DetailBayar As DataGridView
    Friend WithEvents btn_EditPembayaran As Button
    Friend WithEvents btn_InputPembayaran As Button
    Friend WithEvents pnl_CRUD As Panel
    Friend WithEvents btn_DetailPembayaran As Button
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents grb_InfoSaldo As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_SelisihSaldo As TextBox
    Friend WithEvents lbl_SaldoBerdasarkanCOA_PlusAJP As Label
    Friend WithEvents btn_Sesuaikan As Button
    Friend WithEvents txt_SaldoBerdasarkanCOA_PlusPenyesuaian As TextBox
    Friend WithEvents txt_SaldoBerdasarkanList As TextBox
    Friend WithEvents lbl_SaldoBerdasarkanList As Label
    Friend WithEvents lbl_AJP As Label
    Friend WithEvents txt_AJP As TextBox
    Friend WithEvents txt_SaldoAwalBerdasarkanCOA As TextBox
    Friend WithEvents lbl_SaldoAwalBerdasarkanCOA As Label
    Friend WithEvents lbl_TahunPajak As Label
    Friend WithEvents cmb_TahunPajak As ComboBox
    Friend WithEvents rdb_BayarPokok As RadioButton
    Friend WithEvents rdb_BayarSanksi As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents cmb_PilihanJenisPajak As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cmb_PilihanStatusLunas As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cmb_TahunPenerbitan As ComboBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPHP As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Akun_Pokok_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Jenis_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Masa_Pajak_Awal As DataGridViewTextBoxColumn
    Friend WithEvents Masa_Pajak_Akhir As DataGridViewTextBoxColumn
    Friend WithEvents Masa_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Tahun_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Ketetapan As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Ketetapan As DataGridViewTextBoxColumn
    Friend WithEvents Pokok_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Sanksi_ As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Ketetapan As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar_Pokok As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar_Sanksi As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Sisa_Tagihan As DataGridViewTextBoxColumn
    Friend WithEvents Status_Lunas As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents lbl_Warning_3 As Label
    Friend WithEvents lbl_Warning_2 As Label
    Friend WithEvents lbl_Warning_1 As Label
    Friend WithEvents Nomor_ID_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Referensi_ As DataGridViewTextBoxColumn
    Friend WithEvents Nominal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents btn_Export As Button
End Class
