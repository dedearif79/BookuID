<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_JurnalUmum_X
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
        DataTabelUtama = New DataGridView()
        Pilih_ = New DataGridViewCheckBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        Tanggal_Jurnal = New DataGridViewTextBoxColumn()
        Jenis_Jurnal = New DataGridViewTextBoxColumn()
        Kode_Dokumen = New DataGridViewTextBoxColumn()
        Nomor_PO = New DataGridViewTextBoxColumn()
        Kode_Project = New DataGridViewTextBoxColumn()
        Nama_Produk = New DataGridViewTextBoxColumn()
        Referensi = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Nama_Akun = New DataGridViewTextBoxColumn()
        Kode_Akun = New DataGridViewTextBoxColumn()
        D_K = New DataGridViewTextBoxColumn()
        Debet = New DataGridViewTextBoxColumn()
        Kredit = New DataGridViewTextBoxColumn()
        Uraian_Transaksi = New DataGridViewTextBoxColumn()
        Direct_ = New DataGridViewTextBoxColumn()
        Status_Approve = New DataGridViewTextBoxColumn()
        Ceklis_Approve = New DataGridViewCheckBoxColumn()
        btn_Refresh = New Button()
        Label6 = New Label()
        btn_JurnalVoucher = New Button()
        cmb_StatusApprove = New ComboBox()
        Label2 = New Label()
        Label3 = New Label()
        dtp_SampaiTanggal = New DateTimePicker()
        dtp_DariTanggal = New DateTimePicker()
        lbl_Status = New Label()
        cmb_UrutBerdasarkan = New ComboBox()
        cmb_ASC = New ComboBox()
        Label4 = New Label()
        grb_Filter = New GroupBox()
        lbl_Direct = New Label()
        cmb_Direct = New ComboBox()
        Label8 = New Label()
        txt_COA = New TextBox()
        Label7 = New Label()
        txt_Cari = New TextBox()
        btn_Filter = New Button()
        Label5 = New Label()
        txt_SampaiNomorJV = New TextBox()
        txt_DariNomorJV = New TextBox()
        Label1 = New Label()
        btn_Export = New Button()
        btn_Import = New Button()
        btn_Tambah = New Button()
        btn_Setujui = New Button()
        lbl_JumlahListJV = New Label()
        btn_Edit = New Button()
        btn_Hapus = New Button()
        cmb_JenisJurnal = New ComboBox()
        Label9 = New Label()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        grb_Filter.SuspendLayout()
        SuspendLayout()
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Pilih_, Nomor_JV, Tanggal_Jurnal, Jenis_Jurnal, Kode_Dokumen, Nomor_PO, Kode_Project, Nama_Produk, Referensi, Tanggal_Invoice, Nomor_Invoice, Nomor_Faktur_Pajak, Lawan_Transaksi, Nama_Akun, Kode_Akun, D_K, Debet, Kredit, Uraian_Transaksi, Direct_, Status_Approve, Ceklis_Approve})
        DataTabelUtama.Location = New Point(12, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 602)
        DataTabelUtama.TabIndex = 10018
        ' 
        ' Pilih_
        ' 
        Pilih_.Frozen = True
        Pilih_.HeaderText = ""
        Pilih_.Name = "Pilih_"
        Pilih_.Resizable = DataGridViewTriState.True
        Pilih_.Width = 33
        ' 
        ' Nomor_JV
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Padding = New Padding(3, 0, 3, 0)
        Nomor_JV.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_JV.Frozen = True
        Nomor_JV.HeaderText = "No. JV"
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV.Width = 45
        ' 
        ' Tanggal_Jurnal
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Jurnal.DefaultCellStyle = DataGridViewCellStyle2
        Tanggal_Jurnal.Frozen = True
        Tanggal_Jurnal.HeaderText = "Tanggal Jurnal"
        Tanggal_Jurnal.Name = "Tanggal_Jurnal"
        Tanggal_Jurnal.ReadOnly = True
        Tanggal_Jurnal.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Jurnal.Width = 72
        ' 
        ' Jenis_Jurnal
        ' 
        Jenis_Jurnal.HeaderText = "Jenis Jurnal"
        Jenis_Jurnal.Name = "Jenis_Jurnal"
        Jenis_Jurnal.ReadOnly = True
        Jenis_Jurnal.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Jurnal.Visible = False
        ' 
        ' Kode_Dokumen
        ' 
        Kode_Dokumen.HeaderText = "Kode Dokumen"
        Kode_Dokumen.Name = "Kode_Dokumen"
        Kode_Dokumen.ReadOnly = True
        Kode_Dokumen.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Dokumen.Visible = False
        ' 
        ' Nomor_PO
        ' 
        Nomor_PO.HeaderText = "Nomor PO"
        Nomor_PO.Name = "Nomor_PO"
        Nomor_PO.ReadOnly = True
        Nomor_PO.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_PO.Visible = False
        ' 
        ' Kode_Project
        ' 
        Kode_Project.HeaderText = "Kode Project"
        Kode_Project.Name = "Kode_Project"
        Kode_Project.ReadOnly = True
        Kode_Project.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Project.Visible = False
        ' 
        ' Nama_Produk
        ' 
        Nama_Produk.HeaderText = "Nama Barang/Jasa"
        Nama_Produk.Name = "Nama_Produk"
        Nama_Produk.ReadOnly = True
        Nama_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Produk.Width = 99
        ' 
        ' Referensi
        ' 
        Referensi.HeaderText = "Referensi"
        Referensi.Name = "Referensi"
        Referensi.ReadOnly = True
        Referensi.SortMode = DataGridViewColumnSortMode.NotSortable
        Referensi.Visible = False
        Referensi.Width = 63
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.ReadOnly = True
        Tanggal_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Invoice.Width = 72
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.ReadOnly = True
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 99
        ' 
        ' Nomor_Faktur_Pajak
        ' 
        Nomor_Faktur_Pajak.HeaderText = "Nomor Faktur Pajak"
        Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Nomor_Faktur_Pajak.ReadOnly = True
        Nomor_Faktur_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Faktur_Pajak.Width = 99
        ' 
        ' Lawan_Transaksi
        ' 
        Lawan_Transaksi.HeaderText = "Lawan Transaksi"
        Lawan_Transaksi.Name = "Lawan_Transaksi"
        Lawan_Transaksi.ReadOnly = True
        Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Lawan_Transaksi.Width = 141
        ' 
        ' Nama_Akun
        ' 
        DataGridViewCellStyle3.Padding = New Padding(3, 0, 3, 0)
        Nama_Akun.DefaultCellStyle = DataGridViewCellStyle3
        Nama_Akun.HeaderText = "Nama Akun"
        Nama_Akun.Name = "Nama_Akun"
        Nama_Akun.ReadOnly = True
        Nama_Akun.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Akun.Width = 180
        ' 
        ' Kode_Akun
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Padding = New Padding(3, 0, 3, 0)
        Kode_Akun.DefaultCellStyle = DataGridViewCellStyle4
        Kode_Akun.HeaderText = "Kode Akun"
        Kode_Akun.Name = "Kode_Akun"
        Kode_Akun.ReadOnly = True
        Kode_Akun.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Akun.Width = 54
        ' 
        ' D_K
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        D_K.DefaultCellStyle = DataGridViewCellStyle5
        D_K.HeaderText = "D/K"
        D_K.Name = "D_K"
        D_K.ReadOnly = True
        D_K.SortMode = DataGridViewColumnSortMode.NotSortable
        D_K.Width = 36
        ' 
        ' Debet
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        DataGridViewCellStyle6.Padding = New Padding(3, 0, 3, 0)
        Debet.DefaultCellStyle = DataGridViewCellStyle6
        Debet.HeaderText = "Debet"
        Debet.Name = "Debet"
        Debet.ReadOnly = True
        Debet.SortMode = DataGridViewColumnSortMode.NotSortable
        Debet.Width = 99
        ' 
        ' Kredit
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        DataGridViewCellStyle7.NullValue = Nothing
        DataGridViewCellStyle7.Padding = New Padding(3, 0, 3, 0)
        Kredit.DefaultCellStyle = DataGridViewCellStyle7
        Kredit.HeaderText = "Kredit"
        Kredit.Name = "Kredit"
        Kredit.ReadOnly = True
        Kredit.SortMode = DataGridViewColumnSortMode.NotSortable
        Kredit.Width = 99
        ' 
        ' Uraian_Transaksi
        ' 
        DataGridViewCellStyle8.Padding = New Padding(3, 0, 3, 0)
        Uraian_Transaksi.DefaultCellStyle = DataGridViewCellStyle8
        Uraian_Transaksi.HeaderText = "Uraian Transaksi"
        Uraian_Transaksi.Name = "Uraian_Transaksi"
        Uraian_Transaksi.ReadOnly = True
        Uraian_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Uraian_Transaksi.Width = 216
        ' 
        ' Direct_
        ' 
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter
        Direct_.DefaultCellStyle = DataGridViewCellStyle9
        Direct_.HeaderText = "Direct"
        Direct_.Name = "Direct_"
        Direct_.ReadOnly = True
        Direct_.SortMode = DataGridViewColumnSortMode.NotSortable
        Direct_.Width = 45
        ' 
        ' Status_Approve
        ' 
        Status_Approve.HeaderText = "Status Approve"
        Status_Approve.Name = "Status_Approve"
        Status_Approve.ReadOnly = True
        Status_Approve.SortMode = DataGridViewColumnSortMode.NotSortable
        Status_Approve.Visible = False
        ' 
        ' Ceklis_Approve
        ' 
        Ceklis_Approve.HeaderText = "Ceklis Approve"
        Ceklis_Approve.Name = "Ceklis_Approve"
        Ceklis_Approve.Visible = False
        Ceklis_Approve.Width = 54
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(12, 12)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(192, 32)
        Label6.TabIndex = 10030
        Label6.Text = "Jurnal Umum"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_JurnalVoucher
        ' 
        btn_JurnalVoucher.Enabled = False
        btn_JurnalVoucher.Location = New Point(115, 63)
        btn_JurnalVoucher.Margin = New Padding(4, 3, 4, 3)
        btn_JurnalVoucher.Name = "btn_JurnalVoucher"
        btn_JurnalVoucher.Size = New Size(94, 75)
        btn_JurnalVoucher.TabIndex = 10031
        btn_JurnalVoucher.Text = "Jurnal Voucher"
        btn_JurnalVoucher.UseVisualStyleBackColor = True
        ' 
        ' cmb_StatusApprove
        ' 
        cmb_StatusApprove.FormattingEnabled = True
        cmb_StatusApprove.Location = New Point(390, 44)
        cmb_StatusApprove.Margin = New Padding(4, 3, 4, 3)
        cmb_StatusApprove.Name = "cmb_StatusApprove"
        cmb_StatusApprove.Size = New Size(87, 23)
        cmb_StatusApprove.TabIndex = 5050
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(96, 20)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(53, 15)
        Label2.TabIndex = 10062
        Label2.Text = "Periode :"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(108, 47)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(22, 15)
        Label3.TabIndex = 10059
        Label3.Text = "s.d"
        ' 
        ' dtp_SampaiTanggal
        ' 
        dtp_SampaiTanggal.CustomFormat = "dd/MM/yyyy"
        dtp_SampaiTanggal.Format = DateTimePickerFormat.Custom
        dtp_SampaiTanggal.Location = New Point(134, 44)
        dtp_SampaiTanggal.Margin = New Padding(4, 3, 4, 3)
        dtp_SampaiTanggal.Name = "dtp_SampaiTanggal"
        dtp_SampaiTanggal.Size = New Size(94, 23)
        dtp_SampaiTanggal.TabIndex = 5020
        ' 
        ' dtp_DariTanggal
        ' 
        dtp_DariTanggal.CustomFormat = "dd/MM/yyyy"
        dtp_DariTanggal.Format = DateTimePickerFormat.Custom
        dtp_DariTanggal.Location = New Point(13, 44)
        dtp_DariTanggal.Margin = New Padding(4, 3, 4, 3)
        dtp_DariTanggal.Name = "dtp_DariTanggal"
        dtp_DariTanggal.Size = New Size(94, 23)
        dtp_DariTanggal.TabIndex = 5010
        ' 
        ' lbl_Status
        ' 
        lbl_Status.AutoSize = True
        lbl_Status.Location = New Point(407, 20)
        lbl_Status.Margin = New Padding(4, 0, 4, 0)
        lbl_Status.Name = "lbl_Status"
        lbl_Status.Size = New Size(45, 15)
        lbl_Status.TabIndex = 10063
        lbl_Status.Text = "Status :"
        ' 
        ' cmb_UrutBerdasarkan
        ' 
        cmb_UrutBerdasarkan.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        cmb_UrutBerdasarkan.FormattingEnabled = True
        cmb_UrutBerdasarkan.Location = New Point(1284, 111)
        cmb_UrutBerdasarkan.Margin = New Padding(4, 3, 4, 3)
        cmb_UrutBerdasarkan.Name = "cmb_UrutBerdasarkan"
        cmb_UrutBerdasarkan.Size = New Size(145, 23)
        cmb_UrutBerdasarkan.TabIndex = 10064
        ' 
        ' cmb_ASC
        ' 
        cmb_ASC.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        cmb_ASC.FormattingEnabled = True
        cmb_ASC.Location = New Point(1437, 111)
        cmb_ASC.Margin = New Padding(4, 3, 4, 3)
        cmb_ASC.Name = "cmb_ASC"
        cmb_ASC.Size = New Size(69, 23)
        cmb_ASC.TabIndex = 10065
        ' 
        ' Label4
        ' 
        Label4.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label4.AutoSize = True
        Label4.Location = New Point(1284, 87)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(103, 15)
        Label4.TabIndex = 10066
        Label4.Text = "Urut Berdasarkan :"
        ' 
        ' grb_Filter
        ' 
        grb_Filter.Controls.Add(lbl_Direct)
        grb_Filter.Controls.Add(cmb_Direct)
        grb_Filter.Controls.Add(Label8)
        grb_Filter.Controls.Add(txt_COA)
        grb_Filter.Controls.Add(Label7)
        grb_Filter.Controls.Add(txt_Cari)
        grb_Filter.Controls.Add(btn_Filter)
        grb_Filter.Controls.Add(Label5)
        grb_Filter.Controls.Add(txt_SampaiNomorJV)
        grb_Filter.Controls.Add(txt_DariNomorJV)
        grb_Filter.Controls.Add(Label1)
        grb_Filter.Controls.Add(dtp_DariTanggal)
        grb_Filter.Controls.Add(cmb_StatusApprove)
        grb_Filter.Controls.Add(dtp_SampaiTanggal)
        grb_Filter.Controls.Add(Label3)
        grb_Filter.Controls.Add(lbl_Status)
        grb_Filter.Controls.Add(Label2)
        grb_Filter.Location = New Point(318, 58)
        grb_Filter.Margin = New Padding(4, 3, 4, 3)
        grb_Filter.Name = "grb_Filter"
        grb_Filter.Padding = New Padding(4, 3, 4, 3)
        grb_Filter.Size = New Size(916, 81)
        grb_Filter.TabIndex = 10067
        grb_Filter.TabStop = False
        grb_Filter.Text = "Filter :"
        ' 
        ' lbl_Direct
        ' 
        lbl_Direct.AutoSize = True
        lbl_Direct.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_Direct.Location = New Point(743, 20)
        lbl_Direct.Margin = New Padding(4, 0, 4, 0)
        lbl_Direct.Name = "lbl_Direct"
        lbl_Direct.Size = New Size(41, 13)
        lbl_Direct.TabIndex = 10079
        lbl_Direct.Text = "Direct :"
        ' 
        ' cmb_Direct
        ' 
        cmb_Direct.FormattingEnabled = True
        cmb_Direct.Location = New Point(733, 44)
        cmb_Direct.Margin = New Padding(4, 3, 4, 3)
        cmb_Direct.Name = "cmb_Direct"
        cmb_Direct.Size = New Size(73, 23)
        cmb_Direct.TabIndex = 10078
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(518, 20)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(41, 15)
        Label8.TabIndex = 10076
        Label8.Text = "Akun :"
        ' 
        ' txt_COA
        ' 
        txt_COA.Location = New Point(499, 44)
        txt_COA.Margin = New Padding(4, 3, 4, 3)
        txt_COA.Name = "txt_COA"
        txt_COA.Size = New Size(74, 23)
        txt_COA.TabIndex = 5060
        txt_COA.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(630, 20)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(34, 15)
        Label7.TabIndex = 10074
        Label7.Text = "Cari :"
        ' 
        ' txt_Cari
        ' 
        txt_Cari.Location = New Point(597, 44)
        txt_Cari.Margin = New Padding(4, 3, 4, 3)
        txt_Cari.Name = "txt_Cari"
        txt_Cari.Size = New Size(115, 23)
        txt_Cari.TabIndex = 5070
        ' 
        ' btn_Filter
        ' 
        btn_Filter.Location = New Point(827, 20)
        btn_Filter.Margin = New Padding(4, 3, 4, 3)
        btn_Filter.Name = "btn_Filter"
        btn_Filter.Size = New Size(72, 52)
        btn_Filter.TabIndex = 5099
        btn_Filter.Text = "Filter"
        btn_Filter.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(298, 47)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(22, 15)
        Label5.TabIndex = 10067
        Label5.Text = "s.d"
        ' 
        ' txt_SampaiNomorJV
        ' 
        txt_SampaiNomorJV.Location = New Point(322, 44)
        txt_SampaiNomorJV.Margin = New Padding(4, 3, 4, 3)
        txt_SampaiNomorJV.Name = "txt_SampaiNomorJV"
        txt_SampaiNomorJV.Size = New Size(48, 23)
        txt_SampaiNomorJV.TabIndex = 5040
        ' 
        ' txt_DariNomorJV
        ' 
        txt_DariNomorJV.Location = New Point(247, 44)
        txt_DariNomorJV.Margin = New Padding(4, 3, 4, 3)
        txt_DariNomorJV.Name = "txt_DariNomorJV"
        txt_DariNomorJV.Size = New Size(48, 23)
        txt_DariNomorJV.TabIndex = 5030
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(278, 20)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(65, 15)
        Label1.TabIndex = 10064
        Label1.Text = "Nomor JV :"
        ' 
        ' btn_Export
        ' 
        btn_Export.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Export.Location = New Point(1392, 14)
        btn_Export.Margin = New Padding(4, 3, 4, 3)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(115, 52)
        btn_Export.TabIndex = 10070
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' btn_Import
        ' 
        btn_Import.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Import.Location = New Point(1269, 14)
        btn_Import.Margin = New Padding(4, 3, 4, 3)
        btn_Import.Name = "btn_Import"
        btn_Import.Size = New Size(115, 52)
        btn_Import.TabIndex = 10071
        btn_Import.Text = "Import"
        btn_Import.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(930, 16)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 10072
        btn_Tambah.Text = "Input Jurnal"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Setujui
        ' 
        btn_Setujui.Location = New Point(217, 63)
        btn_Setujui.Margin = New Padding(4, 3, 4, 3)
        btn_Setujui.Name = "btn_Setujui"
        btn_Setujui.Size = New Size(94, 75)
        btn_Setujui.TabIndex = 10073
        btn_Setujui.Text = "Setujui"
        btn_Setujui.UseVisualStyleBackColor = True
        ' 
        ' lbl_JumlahListJV
        ' 
        lbl_JumlahListJV.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lbl_JumlahListJV.AutoSize = True
        lbl_JumlahListJV.Location = New Point(14, 760)
        lbl_JumlahListJV.Margin = New Padding(4, 0, 4, 0)
        lbl_JumlahListJV.Name = "lbl_JumlahListJV"
        lbl_JumlahListJV.Size = New Size(86, 15)
        lbl_JumlahListJV.TabIndex = 10074
        lbl_JumlahListJV.Text = "Jumlah List JV :"
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Enabled = False
        btn_Edit.Location = New Point(1034, 16)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10081
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Enabled = False
        btn_Hapus.Location = New Point(1138, 16)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10080
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' cmb_JenisJurnal
        ' 
        cmb_JenisJurnal.FormattingEnabled = True
        cmb_JenisJurnal.Location = New Point(479, 22)
        cmb_JenisJurnal.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisJurnal.Name = "cmb_JenisJurnal"
        cmb_JenisJurnal.Size = New Size(209, 23)
        cmb_JenisJurnal.TabIndex = 10080
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(393, 25)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(72, 15)
        Label9.TabIndex = 10081
        Label9.Text = "Jenis Jurnal :"
        ' 
        ' X_frm_JurnalUmum_X
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(cmb_JenisJurnal)
        Controls.Add(Label9)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(lbl_JumlahListJV)
        Controls.Add(btn_Setujui)
        Controls.Add(btn_Tambah)
        Controls.Add(btn_Import)
        Controls.Add(btn_Export)
        Controls.Add(grb_Filter)
        Controls.Add(Label4)
        Controls.Add(cmb_ASC)
        Controls.Add(cmb_UrutBerdasarkan)
        Controls.Add(btn_JurnalVoucher)
        Controls.Add(Label6)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "X_frm_JurnalUmum_X"
        StartPosition = FormStartPosition.CenterParent
        Text = "Jurnal Umum"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        grb_Filter.ResumeLayout(False)
        grb_Filter.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_JurnalVoucher As System.Windows.Forms.Button
    Friend WithEvents cmb_StatusApprove As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtp_SampaiTanggal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_DariTanggal As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_Status As System.Windows.Forms.Label
    Friend WithEvents cmb_UrutBerdasarkan As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_ASC As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grb_Filter As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_SampaiNomorJV As System.Windows.Forms.TextBox
    Friend WithEvents txt_DariNomorJV As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_Export As System.Windows.Forms.Button
    Friend WithEvents btn_Import As System.Windows.Forms.Button
    Friend WithEvents btn_Filter As System.Windows.Forms.Button
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents txt_Cari As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_COA As System.Windows.Forms.TextBox
    Friend WithEvents btn_Setujui As System.Windows.Forms.Button
    Friend WithEvents lbl_JumlahListJV As System.Windows.Forms.Label
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents lbl_Direct As System.Windows.Forms.Label
    Friend WithEvents cmb_Direct As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_JenisJurnal As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Pilih_ As DataGridViewCheckBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Jurnal As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Jurnal As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Dokumen As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Referensi As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Akun As DataGridViewTextBoxColumn
    Friend WithEvents D_K As DataGridViewTextBoxColumn
    Friend WithEvents Debet As DataGridViewTextBoxColumn
    Friend WithEvents Kredit As DataGridViewTextBoxColumn
    Friend WithEvents Uraian_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Direct_ As DataGridViewTextBoxColumn
    Friend WithEvents Status_Approve As DataGridViewTextBoxColumn
    Friend WithEvents Ceklis_Approve As DataGridViewCheckBoxColumn
End Class
