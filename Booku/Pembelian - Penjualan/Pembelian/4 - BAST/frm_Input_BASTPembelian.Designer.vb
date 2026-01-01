<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Input_BASTPembelian
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txt_Catatan = New System.Windows.Forms.RichTextBox()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.dtp_TanggalBAST = New System.Windows.Forms.DateTimePicker()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txt_NomorBAST = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.txt_NamaSupplier = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.txt_KodeSupplier = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.cmb_PerlakuanPPN = New System.Windows.Forms.ComboBox()
        Me.cmb_JenisPPN = New System.Windows.Forms.ComboBox()
        Me.Nomor_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_JenisPPN = New System.Windows.Forms.Label()
        Me.Tanggal_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Project = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_PerlakuanPPN = New System.Windows.Forms.Label()
        Me.dtp_TanggalDiterima = New System.Windows.Forms.DateTimePicker()
        Me.lbl_TanggalDiterima = New System.Windows.Forms.Label()
        Me.txt_YangMenerima = New System.Windows.Forms.TextBox()
        Me.lbl_NamaPenerima = New System.Windows.Forms.Label()
        Me.btn_SingkirkanPO = New System.Windows.Forms.Button()
        Me.btn_TambahPO = New System.Windows.Forms.Button()
        Me.dgv_PO = New System.Windows.Forms.DataGridView()
        Me.txt_YangMenyerahkan = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_AlamatSupplier = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grb_Produk = New System.Windows.Forms.GroupBox()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_PO_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_PO_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Deskripsi_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Produk_Maksimal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Satuan_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Project_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Keterangan_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_PO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grb_Produk.SuspendLayout()
        Me.SuspendLayout()
        '
        'txt_Catatan
        '
        Me.txt_Catatan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Catatan.Location = New System.Drawing.Point(16, 465)
        Me.txt_Catatan.Name = "txt_Catatan"
        Me.txt_Catatan.Size = New System.Drawing.Size(259, 111)
        Me.txt_Catatan.TabIndex = 10365
        Me.txt_Catatan.Text = ""
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(769, 584)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 10370
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(680, 584)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10371
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'dtp_TanggalBAST
        '
        Me.dtp_TanggalBAST.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalBAST.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalBAST.Location = New System.Drawing.Point(118, 45)
        Me.dtp_TanggalBAST.Name = "dtp_TanggalBAST"
        Me.dtp_TanggalBAST.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalBAST.TabIndex = 10356
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(16, 49)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(77, 13)
        Me.Label50.TabIndex = 10375
        Me.Label50.Text = "Tanggal BAST"
        '
        'txt_NomorBAST
        '
        Me.txt_NomorBAST.Location = New System.Drawing.Point(118, 19)
        Me.txt_NomorBAST.Name = "txt_NomorBAST"
        Me.txt_NomorBAST.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorBAST.TabIndex = 10355
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 10374
        Me.Label1.Text = "Nomor BAST"
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Nomor_PO_Produk, Me.Tanggal_PO_Produk, Me.Nama_Produk, Me.Deskripsi_Produk, Me.Jumlah_Produk, Me.Jumlah_Produk_Maksimal, Me.Satuan_Produk, Me.Kode_Project_Produk, Me.Keterangan_Produk})
        Me.DataTabelUtama.Location = New System.Drawing.Point(18, 192)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(834, 243)
        Me.DataTabelUtama.TabIndex = 10369
        '
        'txt_NamaSupplier
        '
        Me.txt_NamaSupplier.Location = New System.Drawing.Point(410, 45)
        Me.txt_NamaSupplier.Name = "txt_NamaSupplier"
        Me.txt_NamaSupplier.Size = New System.Drawing.Size(193, 20)
        Me.txt_NamaSupplier.TabIndex = 10363
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 449)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 10376
        Me.Label5.Text = "Catatan :"
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(497, 18)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 10361
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'txt_KodeSupplier
        '
        Me.txt_KodeSupplier.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_KodeSupplier.Location = New System.Drawing.Point(410, 19)
        Me.txt_KodeSupplier.MaxLength = 3
        Me.txt_KodeSupplier.Name = "txt_KodeSupplier"
        Me.txt_KodeSupplier.Size = New System.Drawing.Size(81, 20)
        Me.txt_KodeSupplier.TabIndex = 10360
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(314, 22)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(73, 13)
        Me.Label24.TabIndex = 10372
        Me.Label24.Text = "Kode Supplier"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(314, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 10373
        Me.Label2.Text = "Nama Supplier"
        '
        'btn_Hapus
        '
        Me.btn_Hapus.Location = New System.Drawing.Point(82, 16)
        Me.btn_Hapus.Name = "btn_Hapus"
        Me.btn_Hapus.Size = New System.Drawing.Size(68, 30)
        Me.btn_Hapus.TabIndex = 110
        Me.btn_Hapus.Text = "Hapus"
        Me.btn_Hapus.UseVisualStyleBackColor = True
        '
        'cmb_PerlakuanPPN
        '
        Me.cmb_PerlakuanPPN.FormattingEnabled = True
        Me.cmb_PerlakuanPPN.Location = New System.Drawing.Point(506, 158)
        Me.cmb_PerlakuanPPN.Name = "cmb_PerlakuanPPN"
        Me.cmb_PerlakuanPPN.Size = New System.Drawing.Size(97, 21)
        Me.cmb_PerlakuanPPN.TabIndex = 10386
        '
        'cmb_JenisPPN
        '
        Me.cmb_JenisPPN.FormattingEnabled = True
        Me.cmb_JenisPPN.Location = New System.Drawing.Point(403, 158)
        Me.cmb_JenisPPN.Name = "cmb_JenisPPN"
        Me.cmb_JenisPPN.Size = New System.Drawing.Size(92, 21)
        Me.cmb_JenisPPN.TabIndex = 10385
        '
        'Nomor_PO
        '
        Me.Nomor_PO.HeaderText = "Nomor PO"
        Me.Nomor_PO.Name = "Nomor_PO"
        Me.Nomor_PO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_PO.Width = 123
        '
        'lbl_JenisPPN
        '
        Me.lbl_JenisPPN.AutoSize = True
        Me.lbl_JenisPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JenisPPN.Location = New System.Drawing.Point(400, 139)
        Me.lbl_JenisPPN.Name = "lbl_JenisPPN"
        Me.lbl_JenisPPN.Size = New System.Drawing.Size(62, 13)
        Me.lbl_JenisPPN.TabIndex = 10387
        Me.lbl_JenisPPN.Text = "Jenis PPN :"
        '
        'Tanggal_PO
        '
        Me.Tanggal_PO.HeaderText = "Tanggal PO"
        Me.Tanggal_PO.Name = "Tanggal_PO"
        Me.Tanggal_PO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_PO.Width = 99
        '
        'Kode_Project
        '
        Me.Kode_Project.HeaderText = "Kode Proyek"
        Me.Kode_Project.Name = "Kode_Project"
        Me.Kode_Project.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Project.Width = 123
        '
        'lbl_PerlakuanPPN
        '
        Me.lbl_PerlakuanPPN.AutoSize = True
        Me.lbl_PerlakuanPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PerlakuanPPN.Location = New System.Drawing.Point(503, 139)
        Me.lbl_PerlakuanPPN.Name = "lbl_PerlakuanPPN"
        Me.lbl_PerlakuanPPN.Size = New System.Drawing.Size(86, 13)
        Me.lbl_PerlakuanPPN.TabIndex = 10388
        Me.lbl_PerlakuanPPN.Text = "Perlakuan PPN :"
        '
        'dtp_TanggalDiterima
        '
        Me.dtp_TanggalDiterima.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalDiterima.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalDiterima.Location = New System.Drawing.Point(118, 123)
        Me.dtp_TanggalDiterima.Name = "dtp_TanggalDiterima"
        Me.dtp_TanggalDiterima.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalDiterima.TabIndex = 10359
        '
        'lbl_TanggalDiterima
        '
        Me.lbl_TanggalDiterima.AutoSize = True
        Me.lbl_TanggalDiterima.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TanggalDiterima.Location = New System.Drawing.Point(16, 127)
        Me.lbl_TanggalDiterima.Name = "lbl_TanggalDiterima"
        Me.lbl_TanggalDiterima.Size = New System.Drawing.Size(66, 13)
        Me.lbl_TanggalDiterima.TabIndex = 10384
        Me.lbl_TanggalDiterima.Text = "Tgl. Diterima"
        '
        'txt_YangMenerima
        '
        Me.txt_YangMenerima.Location = New System.Drawing.Point(118, 97)
        Me.txt_YangMenerima.Name = "txt_YangMenerima"
        Me.txt_YangMenerima.Size = New System.Drawing.Size(163, 20)
        Me.txt_YangMenerima.TabIndex = 10358
        '
        'lbl_NamaPenerima
        '
        Me.lbl_NamaPenerima.AutoSize = True
        Me.lbl_NamaPenerima.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaPenerima.Location = New System.Drawing.Point(16, 100)
        Me.lbl_NamaPenerima.Name = "lbl_NamaPenerima"
        Me.lbl_NamaPenerima.Size = New System.Drawing.Size(81, 13)
        Me.lbl_NamaPenerima.TabIndex = 10383
        Me.lbl_NamaPenerima.Text = "Yang Menerima"
        '
        'btn_SingkirkanPO
        '
        Me.btn_SingkirkanPO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_SingkirkanPO.Enabled = False
        Me.btn_SingkirkanPO.Location = New System.Drawing.Point(664, 495)
        Me.btn_SingkirkanPO.Name = "btn_SingkirkanPO"
        Me.btn_SingkirkanPO.Size = New System.Drawing.Size(24, 24)
        Me.btn_SingkirkanPO.TabIndex = 10367
        Me.btn_SingkirkanPO.Text = "-"
        Me.btn_SingkirkanPO.UseVisualStyleBackColor = True
        '
        'btn_TambahPO
        '
        Me.btn_TambahPO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_TambahPO.Enabled = False
        Me.btn_TambahPO.Location = New System.Drawing.Point(664, 465)
        Me.btn_TambahPO.Name = "btn_TambahPO"
        Me.btn_TambahPO.Size = New System.Drawing.Size(24, 24)
        Me.btn_TambahPO.TabIndex = 10366
        Me.btn_TambahPO.Text = "+"
        Me.btn_TambahPO.UseVisualStyleBackColor = True
        '
        'dgv_PO
        '
        Me.dgv_PO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgv_PO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_PO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_PO, Me.Tanggal_PO, Me.Kode_Project})
        Me.dgv_PO.Location = New System.Drawing.Point(299, 465)
        Me.dgv_PO.Name = "dgv_PO"
        Me.dgv_PO.RowHeadersVisible = False
        Me.dgv_PO.Size = New System.Drawing.Size(359, 111)
        Me.dgv_PO.TabIndex = 10382
        '
        'txt_YangMenyerahkan
        '
        Me.txt_YangMenyerahkan.Location = New System.Drawing.Point(118, 71)
        Me.txt_YangMenyerahkan.Name = "txt_YangMenyerahkan"
        Me.txt_YangMenyerahkan.Size = New System.Drawing.Size(163, 20)
        Me.txt_YangMenyerahkan.TabIndex = 10357
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 13)
        Me.Label6.TabIndex = 10381
        Me.Label6.Text = "Yang Menyerahkan"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(296, 449)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(28, 13)
        Me.Label12.TabIndex = 10378
        Me.Label12.Text = "PO :"
        '
        'txt_AlamatSupplier
        '
        Me.txt_AlamatSupplier.Location = New System.Drawing.Point(410, 71)
        Me.txt_AlamatSupplier.Multiline = True
        Me.txt_AlamatSupplier.Name = "txt_AlamatSupplier"
        Me.txt_AlamatSupplier.Size = New System.Drawing.Size(193, 45)
        Me.txt_AlamatSupplier.TabIndex = 10364
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(314, 74)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 13)
        Me.Label11.TabIndex = 10377
        Me.Label11.Text = "Alamat Supplier"
        '
        'grb_Produk
        '
        Me.grb_Produk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grb_Produk.Controls.Add(Me.btn_Hapus)
        Me.grb_Produk.Controls.Add(Me.btn_Edit)
        Me.grb_Produk.Location = New System.Drawing.Point(694, 124)
        Me.grb_Produk.Name = "grb_Produk"
        Me.grb_Produk.Size = New System.Drawing.Size(158, 55)
        Me.grb_Produk.TabIndex = 10368
        Me.grb_Produk.TabStop = False
        Me.grb_Produk.Text = "Barang/Jasa :"
        '
        'btn_Edit
        '
        Me.btn_Edit.Location = New System.Drawing.Point(8, 16)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(68, 30)
        Me.btn_Edit.TabIndex = 100
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'Nomor_Urut
        '
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 33
        '
        'Nomor_PO_Produk
        '
        Me.Nomor_PO_Produk.HeaderText = "Nomor PO"
        Me.Nomor_PO_Produk.Name = "Nomor_PO_Produk"
        Me.Nomor_PO_Produk.ReadOnly = True
        Me.Nomor_PO_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_PO_Produk.Visible = False
        Me.Nomor_PO_Produk.Width = 99
        '
        'Tanggal_PO_Produk
        '
        Me.Tanggal_PO_Produk.HeaderText = "Tanggal PO"
        Me.Tanggal_PO_Produk.Name = "Tanggal_PO_Produk"
        Me.Tanggal_PO_Produk.ReadOnly = True
        Me.Tanggal_PO_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_PO_Produk.Visible = False
        Me.Tanggal_PO_Produk.Width = 99
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
        Me.Deskripsi_Produk.Width = 120
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
        'Jumlah_Produk_Maksimal
        '
        Me.Jumlah_Produk_Maksimal.HeaderText = "Jumlah Produk Maksimal"
        Me.Jumlah_Produk_Maksimal.Name = "Jumlah_Produk_Maksimal"
        Me.Jumlah_Produk_Maksimal.ReadOnly = True
        Me.Jumlah_Produk_Maksimal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Produk_Maksimal.Visible = False
        '
        'Satuan_Produk
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Satuan_Produk.DefaultCellStyle = DataGridViewCellStyle2
        Me.Satuan_Produk.HeaderText = "Satuan"
        Me.Satuan_Produk.Name = "Satuan_Produk"
        Me.Satuan_Produk.ReadOnly = True
        Me.Satuan_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Satuan_Produk.Width = 63
        '
        'Kode_Project_Produk
        '
        Me.Kode_Project_Produk.HeaderText = "Kode Project"
        Me.Kode_Project_Produk.Name = "Kode_Project_Produk"
        Me.Kode_Project_Produk.ReadOnly = True
        Me.Kode_Project_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Project_Produk.Width = 99
        '
        'Keterangan_Produk
        '
        Me.Keterangan_Produk.HeaderText = "Keterangan"
        Me.Keterangan_Produk.Name = "Keterangan_Produk"
        Me.Keterangan_Produk.ReadOnly = True
        Me.Keterangan_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Keterangan_Produk.Width = 381
        '
        'frm_Input_BASTPembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 635)
        Me.Controls.Add(Me.txt_Catatan)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.dtp_TanggalBAST)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.txt_NomorBAST)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.txt_NamaSupplier)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_KodeSupplier)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmb_PerlakuanPPN)
        Me.Controls.Add(Me.cmb_JenisPPN)
        Me.Controls.Add(Me.lbl_JenisPPN)
        Me.Controls.Add(Me.lbl_PerlakuanPPN)
        Me.Controls.Add(Me.dtp_TanggalDiterima)
        Me.Controls.Add(Me.lbl_TanggalDiterima)
        Me.Controls.Add(Me.txt_YangMenerima)
        Me.Controls.Add(Me.lbl_NamaPenerima)
        Me.Controls.Add(Me.btn_SingkirkanPO)
        Me.Controls.Add(Me.btn_TambahPO)
        Me.Controls.Add(Me.dgv_PO)
        Me.Controls.Add(Me.txt_YangMenyerahkan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txt_AlamatSupplier)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.grb_Produk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_Input_BASTPembelian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Berita Acara Serah Terima (BAST)"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_PO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grb_Produk.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_Catatan As RichTextBox
    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents dtp_TanggalBAST As DateTimePicker
    Friend WithEvents Label50 As Label
    Friend WithEvents txt_NomorBAST As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents txt_NamaSupplier As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents txt_KodeSupplier As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents cmb_PerlakuanPPN As ComboBox
    Friend WithEvents cmb_JenisPPN As ComboBox
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents lbl_JenisPPN As Label
    Friend WithEvents Tanggal_PO As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents lbl_PerlakuanPPN As Label
    Friend WithEvents dtp_TanggalDiterima As DateTimePicker
    Friend WithEvents lbl_TanggalDiterima As Label
    Friend WithEvents txt_YangMenerima As TextBox
    Friend WithEvents lbl_NamaPenerima As Label
    Friend WithEvents btn_SingkirkanPO As Button
    Friend WithEvents btn_TambahPO As Button
    Friend WithEvents dgv_PO As DataGridView
    Friend WithEvents txt_YangMenyerahkan As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txt_AlamatSupplier As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents grb_Produk As GroupBox
    Friend WithEvents btn_Edit As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_PO_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Deskripsi_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Produk_Maksimal As DataGridViewTextBoxColumn
    Friend WithEvents Satuan_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_Produk As DataGridViewTextBoxColumn
End Class
