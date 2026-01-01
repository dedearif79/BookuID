<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_BukuPengawasanHutangAfiliasi_X
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
        lbl_JudulForm = New Label()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_BPHA = New DataGridViewTextBoxColumn()
        Kode_Kreditur = New DataGridViewTextBoxColumn()
        Nama_Kreditur = New DataGridViewTextBoxColumn()
        Tanggal_Pinjam = New DataGridViewTextBoxColumn()
        Jatuh_Tempo = New DataGridViewTextBoxColumn()
        Nomor_Kontrak = New DataGridViewTextBoxColumn()
        Saldo_Awal = New DataGridViewTextBoxColumn()
        Jumlah_Bayar = New DataGridViewTextBoxColumn()
        Saldo_Akhir = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        btn_EditHutang = New Button()
        btn_HapusHutang = New Button()
        btn_InputHutang = New Button()
        spc_Utama = New SplitContainer()
        grb_DaftarHutang = New GroupBox()
        btn_Posting = New Button()
        btn_LihatJurnal = New Button()
        grb_JadwalAngsuranHutang = New GroupBox()
        btn_LihatJurnalJadwal = New Button()
        btn_RefreshJadwal = New Button()
        btn_InputJadwal = New Button()
        dgv_JadwalAngsuran = New DataGridView()
        Jadwal_Nomor_Urut = New DataGridViewTextBoxColumn()
        Jadwal_Ceklis_ = New DataGridViewCheckBoxColumn()
        Jadwal_Nomor_ID = New DataGridViewTextBoxColumn()
        Jadwal_Jatuh_Tempo = New DataGridViewTextBoxColumn()
        Jadwal_Tanggal_Bayar = New DataGridViewTextBoxColumn()
        Jadwal_Pokok_ = New DataGridViewTextBoxColumn()
        Jadwal_Bagi_Hasil = New DataGridViewTextBoxColumn()
        Jadwal_PPh_Ditanggung = New DataGridViewTextBoxColumn()
        Jadwal_PPh_Dipotong = New DataGridViewTextBoxColumn()
        Jadwal_Jumlah_ = New DataGridViewTextBoxColumn()
        Jadwal_Saldo_Akhir = New DataGridViewTextBoxColumn()
        Jadwal_Nomor_JV = New DataGridViewTextBoxColumn()
        btn_HapusJadwal = New Button()
        btn_EditJadwal = New Button()
        btn_InputPembayaran = New Button()
        grb_Pembayaran = New GroupBox()
        btn_HapusPembayaran = New Button()
        btn_EditPembayaran = New Button()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        CType(spc_Utama, ComponentModel.ISupportInitialize).BeginInit()
        spc_Utama.Panel1.SuspendLayout()
        spc_Utama.Panel2.SuspendLayout()
        spc_Utama.SuspendLayout()
        grb_DaftarHutang.SuspendLayout()
        grb_JadwalAngsuranHutang.SuspendLayout()
        CType(dgv_JadwalAngsuran, ComponentModel.ISupportInitialize).BeginInit()
        grb_Pembayaran.SuspendLayout()
        SuspendLayout()
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 14)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(473, 32)
        lbl_JudulForm.TabIndex = 10039
        lbl_JudulForm.Text = "Buku Pengawasan Hutang Afiliasi"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(7, 22)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 40)
        btn_Refresh.TabIndex = 10034
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPHA, Kode_Kreditur, Nama_Kreditur, Tanggal_Pinjam, Jatuh_Tempo, Nomor_Kontrak, Saldo_Awal, Jumlah_Bayar, Saldo_Akhir, Keterangan_, Nomor_JV})
        DataTabelUtama.Location = New Point(7, 69)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(803, 586)
        DataTabelUtama.TabIndex = 10038
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_Urut.HeaderText = "No."
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
        ' Nomor_BPHA
        ' 
        Nomor_BPHA.HeaderText = "Nomor BPHA"
        Nomor_BPHA.Name = "Nomor_BPHA"
        Nomor_BPHA.ReadOnly = True
        Nomor_BPHA.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPHA.Visible = False
        Nomor_BPHA.Width = 99
        ' 
        ' Kode_Kreditur
        ' 
        Kode_Kreditur.HeaderText = "Kode Kreditur"
        Kode_Kreditur.Name = "Kode_Kreditur"
        Kode_Kreditur.ReadOnly = True
        Kode_Kreditur.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Kreditur.Visible = False
        Kode_Kreditur.Width = 72
        ' 
        ' Nama_Kreditur
        ' 
        Nama_Kreditur.HeaderText = "Nama Kreditur"
        Nama_Kreditur.Name = "Nama_Kreditur"
        Nama_Kreditur.ReadOnly = True
        Nama_Kreditur.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Kreditur.Width = 150
        ' 
        ' Tanggal_Pinjam
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Pinjam.DefaultCellStyle = DataGridViewCellStyle2
        Tanggal_Pinjam.HeaderText = "Tanggal Pinjam"
        Tanggal_Pinjam.Name = "Tanggal_Pinjam"
        Tanggal_Pinjam.ReadOnly = True
        Tanggal_Pinjam.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Pinjam.Width = 69
        ' 
        ' Jatuh_Tempo
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        Jatuh_Tempo.DefaultCellStyle = DataGridViewCellStyle3
        Jatuh_Tempo.HeaderText = "Jatuh Tempo"
        Jatuh_Tempo.Name = "Jatuh_Tempo"
        Jatuh_Tempo.ReadOnly = True
        Jatuh_Tempo.SortMode = DataGridViewColumnSortMode.NotSortable
        Jatuh_Tempo.Width = 69
        ' 
        ' Nomor_Kontrak
        ' 
        Nomor_Kontrak.HeaderText = "Nomor Kontrak"
        Nomor_Kontrak.Name = "Nomor_Kontrak"
        Nomor_Kontrak.ReadOnly = True
        Nomor_Kontrak.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Kontrak.Visible = False
        Nomor_Kontrak.Width = 99
        ' 
        ' Saldo_Awal
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Saldo_Awal.DefaultCellStyle = DataGridViewCellStyle4
        Saldo_Awal.HeaderText = "Saldo Awal"
        Saldo_Awal.Name = "Saldo_Awal"
        Saldo_Awal.ReadOnly = True
        Saldo_Awal.SortMode = DataGridViewColumnSortMode.NotSortable
        Saldo_Awal.Width = 81
        ' 
        ' Jumlah_Bayar
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Jumlah_Bayar.DefaultCellStyle = DataGridViewCellStyle5
        Jumlah_Bayar.HeaderText = "Jumlah Bayar"
        Jumlah_Bayar.Name = "Jumlah_Bayar"
        Jumlah_Bayar.ReadOnly = True
        Jumlah_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Bayar.Width = 81
        ' 
        ' Saldo_Akhir
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Saldo_Akhir.DefaultCellStyle = DataGridViewCellStyle6
        Saldo_Akhir.HeaderText = "Saldo Akhir"
        Saldo_Akhir.Name = "Saldo_Akhir"
        Saldo_Akhir.ReadOnly = True
        Saldo_Akhir.SortMode = DataGridViewColumnSortMode.NotSortable
        Saldo_Akhir.Width = 81
        ' 
        ' Keterangan_
        ' 
        Keterangan_.HeaderText = "Keterangan"
        Keterangan_.Name = "Keterangan_"
        Keterangan_.ReadOnly = True
        Keterangan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_.Width = 150
        ' 
        ' Nomor_JV
        ' 
        Nomor_JV.HeaderText = "Nomor JV"
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV.Visible = False
        Nomor_JV.Width = 81
        ' 
        ' btn_EditHutang
        ' 
        btn_EditHutang.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_EditHutang.Location = New Point(656, 22)
        btn_EditHutang.Margin = New Padding(4, 3, 4, 3)
        btn_EditHutang.Name = "btn_EditHutang"
        btn_EditHutang.Size = New Size(74, 40)
        btn_EditHutang.TabIndex = 10037
        btn_EditHutang.Text = "Edit"
        btn_EditHutang.UseVisualStyleBackColor = True
        ' 
        ' btn_HapusHutang
        ' 
        btn_HapusHutang.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_HapusHutang.Location = New Point(736, 22)
        btn_HapusHutang.Margin = New Padding(4, 3, 4, 3)
        btn_HapusHutang.Name = "btn_HapusHutang"
        btn_HapusHutang.Size = New Size(74, 40)
        btn_HapusHutang.TabIndex = 10036
        btn_HapusHutang.Text = "Hapus"
        btn_HapusHutang.UseVisualStyleBackColor = True
        ' 
        ' btn_InputHutang
        ' 
        btn_InputHutang.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_InputHutang.Location = New Point(575, 22)
        btn_InputHutang.Margin = New Padding(4, 3, 4, 3)
        btn_InputHutang.Name = "btn_InputHutang"
        btn_InputHutang.Size = New Size(74, 40)
        btn_InputHutang.TabIndex = 10035
        btn_InputHutang.Text = "Input"
        btn_InputHutang.UseVisualStyleBackColor = True
        ' 
        ' spc_Utama
        ' 
        spc_Utama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        spc_Utama.Location = New Point(1, 77)
        spc_Utama.Margin = New Padding(4, 3, 4, 3)
        spc_Utama.Name = "spc_Utama"
        ' 
        ' spc_Utama.Panel1
        ' 
        spc_Utama.Panel1.Controls.Add(grb_DaftarHutang)
        ' 
        ' spc_Utama.Panel2
        ' 
        spc_Utama.Panel2.Controls.Add(grb_JadwalAngsuranHutang)
        spc_Utama.Size = New Size(1517, 674)
        spc_Utama.SplitterDistance = 833
        spc_Utama.SplitterWidth = 5
        spc_Utama.TabIndex = 10040
        ' 
        ' grb_DaftarHutang
        ' 
        grb_DaftarHutang.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        grb_DaftarHutang.Controls.Add(btn_Export)
        grb_DaftarHutang.Controls.Add(btn_Posting)
        grb_DaftarHutang.Controls.Add(btn_LihatJurnal)
        grb_DaftarHutang.Controls.Add(DataTabelUtama)
        grb_DaftarHutang.Controls.Add(btn_InputHutang)
        grb_DaftarHutang.Controls.Add(btn_Refresh)
        grb_DaftarHutang.Controls.Add(btn_HapusHutang)
        grb_DaftarHutang.Controls.Add(btn_EditHutang)
        grb_DaftarHutang.Location = New Point(13, 8)
        grb_DaftarHutang.Margin = New Padding(4, 3, 4, 3)
        grb_DaftarHutang.Name = "grb_DaftarHutang"
        grb_DaftarHutang.Padding = New Padding(4, 3, 4, 3)
        grb_DaftarHutang.Size = New Size(817, 662)
        grb_DaftarHutang.TabIndex = 0
        grb_DaftarHutang.TabStop = False
        grb_DaftarHutang.Text = "Daftar Hutang :"
        ' 
        ' btn_Posting
        ' 
        btn_Posting.Enabled = False
        btn_Posting.Location = New Point(109, 22)
        btn_Posting.Margin = New Padding(4, 3, 4, 3)
        btn_Posting.Name = "btn_Posting"
        btn_Posting.Size = New Size(97, 40)
        btn_Posting.TabIndex = 10098
        btn_Posting.Text = "Posting"
        btn_Posting.UseVisualStyleBackColor = True
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(214, 22)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 40)
        btn_LihatJurnal.TabIndex = 10097
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' grb_JadwalAngsuranHutang
        ' 
        grb_JadwalAngsuranHutang.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        grb_JadwalAngsuranHutang.Controls.Add(btn_LihatJurnalJadwal)
        grb_JadwalAngsuranHutang.Controls.Add(btn_RefreshJadwal)
        grb_JadwalAngsuranHutang.Controls.Add(btn_InputJadwal)
        grb_JadwalAngsuranHutang.Controls.Add(dgv_JadwalAngsuran)
        grb_JadwalAngsuranHutang.Controls.Add(btn_HapusJadwal)
        grb_JadwalAngsuranHutang.Controls.Add(btn_EditJadwal)
        grb_JadwalAngsuranHutang.Location = New Point(4, 8)
        grb_JadwalAngsuranHutang.Margin = New Padding(4, 3, 4, 3)
        grb_JadwalAngsuranHutang.Name = "grb_JadwalAngsuranHutang"
        grb_JadwalAngsuranHutang.Padding = New Padding(4, 3, 4, 3)
        grb_JadwalAngsuranHutang.Size = New Size(664, 662)
        grb_JadwalAngsuranHutang.TabIndex = 1
        grb_JadwalAngsuranHutang.TabStop = False
        grb_JadwalAngsuranHutang.Text = "Jadwal Angsuran Hutang :"
        ' 
        ' btn_LihatJurnalJadwal
        ' 
        btn_LihatJurnalJadwal.Enabled = False
        btn_LihatJurnalJadwal.Location = New Point(108, 22)
        btn_LihatJurnalJadwal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnalJadwal.Name = "btn_LihatJurnalJadwal"
        btn_LihatJurnalJadwal.Size = New Size(97, 40)
        btn_LihatJurnalJadwal.TabIndex = 10098
        btn_LihatJurnalJadwal.Text = "Lihat Jurnal"
        btn_LihatJurnalJadwal.UseVisualStyleBackColor = True
        ' 
        ' btn_RefreshJadwal
        ' 
        btn_RefreshJadwal.Location = New Point(7, 22)
        btn_RefreshJadwal.Margin = New Padding(4, 3, 4, 3)
        btn_RefreshJadwal.Name = "btn_RefreshJadwal"
        btn_RefreshJadwal.Size = New Size(94, 40)
        btn_RefreshJadwal.TabIndex = 10039
        btn_RefreshJadwal.Text = "Refresh"
        btn_RefreshJadwal.UseVisualStyleBackColor = True
        ' 
        ' btn_InputJadwal
        ' 
        btn_InputJadwal.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_InputJadwal.Location = New Point(423, 22)
        btn_InputJadwal.Margin = New Padding(4, 3, 4, 3)
        btn_InputJadwal.Name = "btn_InputJadwal"
        btn_InputJadwal.Size = New Size(74, 40)
        btn_InputJadwal.TabIndex = 10039
        btn_InputJadwal.Text = "Input"
        btn_InputJadwal.UseVisualStyleBackColor = True
        ' 
        ' dgv_JadwalAngsuran
        ' 
        dgv_JadwalAngsuran.AllowUserToAddRows = False
        dgv_JadwalAngsuran.AllowUserToDeleteRows = False
        dgv_JadwalAngsuran.AllowUserToResizeRows = False
        dgv_JadwalAngsuran.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgv_JadwalAngsuran.BorderStyle = BorderStyle.Fixed3D
        dgv_JadwalAngsuran.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_JadwalAngsuran.Columns.AddRange(New DataGridViewColumn() {Jadwal_Nomor_Urut, Jadwal_Ceklis_, Jadwal_Nomor_ID, Jadwal_Jatuh_Tempo, Jadwal_Tanggal_Bayar, Jadwal_Pokok_, Jadwal_Bagi_Hasil, Jadwal_PPh_Ditanggung, Jadwal_PPh_Dipotong, Jadwal_Jumlah_, Jadwal_Saldo_Akhir, Jadwal_Nomor_JV})
        dgv_JadwalAngsuran.Location = New Point(7, 69)
        dgv_JadwalAngsuran.Margin = New Padding(4, 3, 4, 3)
        dgv_JadwalAngsuran.MultiSelect = False
        dgv_JadwalAngsuran.Name = "dgv_JadwalAngsuran"
        dgv_JadwalAngsuran.RowHeadersVisible = False
        dgv_JadwalAngsuran.RowHeadersWidth = 33
        dgv_JadwalAngsuran.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_JadwalAngsuran.Size = New Size(650, 586)
        dgv_JadwalAngsuran.TabIndex = 10039
        ' 
        ' Jadwal_Nomor_Urut
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        Jadwal_Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle7
        Jadwal_Nomor_Urut.HeaderText = "No."
        Jadwal_Nomor_Urut.Name = "Jadwal_Nomor_Urut"
        Jadwal_Nomor_Urut.ReadOnly = True
        Jadwal_Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Nomor_Urut.Width = 33
        ' 
        ' Jadwal_Ceklis_
        ' 
        Jadwal_Ceklis_.HeaderText = "Ceklis"
        Jadwal_Ceklis_.Name = "Jadwal_Ceklis_"
        Jadwal_Ceklis_.ReadOnly = True
        Jadwal_Ceklis_.Width = 42
        ' 
        ' Jadwal_Nomor_ID
        ' 
        Jadwal_Nomor_ID.HeaderText = "Nomor ID"
        Jadwal_Nomor_ID.Name = "Jadwal_Nomor_ID"
        Jadwal_Nomor_ID.ReadOnly = True
        Jadwal_Nomor_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Nomor_ID.Visible = False
        ' 
        ' Jadwal_Jatuh_Tempo
        ' 
        Jadwal_Jatuh_Tempo.HeaderText = "Jatuh Tempo"
        Jadwal_Jatuh_Tempo.Name = "Jadwal_Jatuh_Tempo"
        Jadwal_Jatuh_Tempo.ReadOnly = True
        Jadwal_Jatuh_Tempo.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Jatuh_Tempo.Width = 63
        ' 
        ' Jadwal_Tanggal_Bayar
        ' 
        Jadwal_Tanggal_Bayar.HeaderText = "Tanggal Bayar"
        Jadwal_Tanggal_Bayar.Name = "Jadwal_Tanggal_Bayar"
        Jadwal_Tanggal_Bayar.ReadOnly = True
        Jadwal_Tanggal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Tanggal_Bayar.Width = 63
        ' 
        ' Jadwal_Pokok_
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        Jadwal_Pokok_.DefaultCellStyle = DataGridViewCellStyle8
        Jadwal_Pokok_.HeaderText = "Pokok"
        Jadwal_Pokok_.Name = "Jadwal_Pokok_"
        Jadwal_Pokok_.ReadOnly = True
        Jadwal_Pokok_.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Pokok_.Width = 75
        ' 
        ' Jadwal_Bagi_Hasil
        ' 
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N0"
        Jadwal_Bagi_Hasil.DefaultCellStyle = DataGridViewCellStyle9
        Jadwal_Bagi_Hasil.HeaderText = "Bunga / Bagi Hasil"
        Jadwal_Bagi_Hasil.Name = "Jadwal_Bagi_Hasil"
        Jadwal_Bagi_Hasil.ReadOnly = True
        Jadwal_Bagi_Hasil.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Bagi_Hasil.Width = 69
        ' 
        ' Jadwal_PPh_Ditanggung
        ' 
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        Jadwal_PPh_Ditanggung.DefaultCellStyle = DataGridViewCellStyle10
        Jadwal_PPh_Ditanggung.HeaderText = "PPh Ditanggung"
        Jadwal_PPh_Ditanggung.Name = "Jadwal_PPh_Ditanggung"
        Jadwal_PPh_Ditanggung.ReadOnly = True
        Jadwal_PPh_Ditanggung.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_PPh_Ditanggung.Width = 69
        ' 
        ' Jadwal_PPh_Dipotong
        ' 
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        Jadwal_PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle11
        Jadwal_PPh_Dipotong.HeaderText = "PPh Dipotong"
        Jadwal_PPh_Dipotong.Name = "Jadwal_PPh_Dipotong"
        Jadwal_PPh_Dipotong.ReadOnly = True
        Jadwal_PPh_Dipotong.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_PPh_Dipotong.Width = 69
        ' 
        ' Jadwal_Jumlah_
        ' 
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N0"
        Jadwal_Jumlah_.DefaultCellStyle = DataGridViewCellStyle12
        Jadwal_Jumlah_.HeaderText = "Jumlah"
        Jadwal_Jumlah_.Name = "Jadwal_Jumlah_"
        Jadwal_Jumlah_.ReadOnly = True
        Jadwal_Jumlah_.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Jumlah_.Width = 75
        ' 
        ' Jadwal_Saldo_Akhir
        ' 
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N0"
        Jadwal_Saldo_Akhir.DefaultCellStyle = DataGridViewCellStyle13
        Jadwal_Saldo_Akhir.HeaderText = "Saldo Akhir"
        Jadwal_Saldo_Akhir.Name = "Jadwal_Saldo_Akhir"
        Jadwal_Saldo_Akhir.ReadOnly = True
        Jadwal_Saldo_Akhir.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Saldo_Akhir.Width = 81
        ' 
        ' Jadwal_Nomor_JV
        ' 
        Jadwal_Nomor_JV.HeaderText = "Nomor JV"
        Jadwal_Nomor_JV.Name = "Jadwal_Nomor_JV"
        Jadwal_Nomor_JV.ReadOnly = True
        Jadwal_Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Jadwal_Nomor_JV.Visible = False
        ' 
        ' btn_HapusJadwal
        ' 
        btn_HapusJadwal.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_HapusJadwal.Location = New Point(583, 22)
        btn_HapusJadwal.Margin = New Padding(4, 3, 4, 3)
        btn_HapusJadwal.Name = "btn_HapusJadwal"
        btn_HapusJadwal.Size = New Size(74, 40)
        btn_HapusJadwal.TabIndex = 10040
        btn_HapusJadwal.Text = "Hapus"
        btn_HapusJadwal.UseVisualStyleBackColor = True
        ' 
        ' btn_EditJadwal
        ' 
        btn_EditJadwal.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_EditJadwal.Location = New Point(503, 22)
        btn_EditJadwal.Margin = New Padding(4, 3, 4, 3)
        btn_EditJadwal.Name = "btn_EditJadwal"
        btn_EditJadwal.Size = New Size(74, 40)
        btn_EditJadwal.TabIndex = 10041
        btn_EditJadwal.Text = "Edit"
        btn_EditJadwal.UseVisualStyleBackColor = True
        ' 
        ' btn_InputPembayaran
        ' 
        btn_InputPembayaran.Location = New Point(7, 22)
        btn_InputPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_InputPembayaran.Name = "btn_InputPembayaran"
        btn_InputPembayaran.Size = New Size(75, 35)
        btn_InputPembayaran.TabIndex = 10048
        btn_InputPembayaran.Text = "Bayar"
        btn_InputPembayaran.UseVisualStyleBackColor = True
        ' 
        ' grb_Pembayaran
        ' 
        grb_Pembayaran.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_Pembayaran.Controls.Add(btn_HapusPembayaran)
        grb_Pembayaran.Controls.Add(btn_EditPembayaran)
        grb_Pembayaran.Controls.Add(btn_InputPembayaran)
        grb_Pembayaran.Location = New Point(1255, 14)
        grb_Pembayaran.Margin = New Padding(4, 3, 4, 3)
        grb_Pembayaran.Name = "grb_Pembayaran"
        grb_Pembayaran.Padding = New Padding(4, 3, 4, 3)
        grb_Pembayaran.Size = New Size(252, 65)
        grb_Pembayaran.TabIndex = 10041
        grb_Pembayaran.TabStop = False
        grb_Pembayaran.Text = "Pembayaran :"
        ' 
        ' btn_HapusPembayaran
        ' 
        btn_HapusPembayaran.Location = New Point(170, 22)
        btn_HapusPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_HapusPembayaran.Name = "btn_HapusPembayaran"
        btn_HapusPembayaran.Size = New Size(75, 35)
        btn_HapusPembayaran.TabIndex = 10050
        btn_HapusPembayaran.Text = "Hapus"
        btn_HapusPembayaran.UseVisualStyleBackColor = True
        ' 
        ' btn_EditPembayaran
        ' 
        btn_EditPembayaran.Location = New Point(89, 22)
        btn_EditPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_EditPembayaran.Name = "btn_EditPembayaran"
        btn_EditPembayaran.Size = New Size(75, 35)
        btn_EditPembayaran.TabIndex = 10049
        btn_EditPembayaran.Text = "Edit"
        btn_EditPembayaran.UseVisualStyleBackColor = True
        ' 
        ' btn_Export
        ' 
        btn_Export.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Export.Location = New Point(356, 22)
        btn_Export.Margin = New Padding(4, 3, 4, 3)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(115, 40)
        btn_Export.TabIndex = 10233
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuPengawasanHutangAfiliasi
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(grb_Pembayaran)
        Controls.Add(spc_Utama)
        Controls.Add(lbl_JudulForm)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanHutangAfiliasi"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Hutang Afiliasi"
        WindowState = FormWindowState.Minimized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        spc_Utama.Panel1.ResumeLayout(False)
        spc_Utama.Panel2.ResumeLayout(False)
        CType(spc_Utama, ComponentModel.ISupportInitialize).EndInit()
        spc_Utama.ResumeLayout(False)
        grb_DaftarHutang.ResumeLayout(False)
        grb_JadwalAngsuranHutang.ResumeLayout(False)
        CType(dgv_JadwalAngsuran, ComponentModel.ISupportInitialize).EndInit()
        grb_Pembayaran.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_EditHutang As Button
    Friend WithEvents btn_HapusHutang As Button
    Friend WithEvents btn_InputHutang As Button
    Friend WithEvents spc_Utama As SplitContainer
    Friend WithEvents grb_DaftarHutang As GroupBox
    Friend WithEvents grb_JadwalAngsuranHutang As GroupBox
    Friend WithEvents dgv_JadwalAngsuran As DataGridView
    Friend WithEvents btn_InputJadwal As Button
    Friend WithEvents btn_HapusJadwal As Button
    Friend WithEvents btn_EditJadwal As Button
    Friend WithEvents btn_RefreshJadwal As Button
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents btn_LihatJurnalJadwal As Button
    Friend WithEvents btn_InputPembayaran As Button
    Friend WithEvents grb_Pembayaran As GroupBox
    Friend WithEvents btn_HapusPembayaran As Button
    Friend WithEvents btn_EditPembayaran As Button
    Friend WithEvents Jadwal_Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_Ceklis_ As DataGridViewCheckBoxColumn
    Friend WithEvents Jadwal_Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_Jatuh_Tempo As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_Tanggal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_Pokok_ As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_Bagi_Hasil As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_PPh_Ditanggung As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_PPh_Dipotong As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_Jumlah_ As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_Saldo_Akhir As DataGridViewTextBoxColumn
    Friend WithEvents Jadwal_Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPHA As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Kreditur As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Kreditur As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Pinjam As DataGridViewTextBoxColumn
    Friend WithEvents Jatuh_Tempo As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Kontrak As DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Awal As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Akhir As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents btn_Posting As Button
    Friend WithEvents btn_Export As Button
End Class
