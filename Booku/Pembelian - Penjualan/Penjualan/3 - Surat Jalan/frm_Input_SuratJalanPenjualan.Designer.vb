<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Input_SuratJalanPenjualan
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
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_AlamatCustomer = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grb_Produk = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Catatan = New System.Windows.Forms.RichTextBox()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.dtp_TanggalSJ = New System.Windows.Forms.DateTimePicker()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txt_NomorSJ = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.txt_KodeCustomer = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txt_NamaCustomer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_NamaSupir = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_PlatNomor = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btn_Pratinjau = New System.Windows.Forms.Button()
        Me.btn_Cetak = New System.Windows.Forms.Button()
        Me.txt_NamaPengirim = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgv_PO = New System.Windows.Forms.DataGridView()
        Me.Nomor_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Project = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_SingkirkanPO = New System.Windows.Forms.Button()
        Me.btn_TambahPO = New System.Windows.Forms.Button()
        Me.dtp_TanggalDiterima = New System.Windows.Forms.DateTimePicker()
        Me.lbl_TanggalDiterima = New System.Windows.Forms.Label()
        Me.txt_NamaPenerima = New System.Windows.Forms.TextBox()
        Me.lbl_NamaPenerima = New System.Windows.Forms.Label()
        Me.lbl_PerlakuanPPN = New System.Windows.Forms.Label()
        Me.cmb_PerlakuanPPN = New System.Windows.Forms.ComboBox()
        Me.lbl_JenisPPN = New System.Windows.Forms.Label()
        Me.cmb_JenisPPN = New System.Windows.Forms.ComboBox()
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
        Me.grb_Produk.SuspendLayout()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_PO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'btn_Edit
        '
        Me.btn_Edit.Location = New System.Drawing.Point(8, 16)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(68, 30)
        Me.btn_Edit.TabIndex = 100
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(296, 449)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(28, 13)
        Me.Label12.TabIndex = 10261
        Me.Label12.Text = "PO :"
        '
        'txt_AlamatCustomer
        '
        Me.txt_AlamatCustomer.Location = New System.Drawing.Point(410, 71)
        Me.txt_AlamatCustomer.Multiline = True
        Me.txt_AlamatCustomer.Name = "txt_AlamatCustomer"
        Me.txt_AlamatCustomer.Size = New System.Drawing.Size(193, 45)
        Me.txt_AlamatCustomer.TabIndex = 110
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(314, 74)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(86, 13)
        Me.Label11.TabIndex = 10258
        Me.Label11.Text = "Alamat Customer"
        '
        'grb_Produk
        '
        Me.grb_Produk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grb_Produk.Controls.Add(Me.btn_Hapus)
        Me.grb_Produk.Controls.Add(Me.btn_Edit)
        Me.grb_Produk.Location = New System.Drawing.Point(694, 154)
        Me.grb_Produk.Name = "grb_Produk"
        Me.grb_Produk.Size = New System.Drawing.Size(158, 55)
        Me.grb_Produk.TabIndex = 10236
        Me.grb_Produk.TabStop = False
        Me.grb_Produk.Text = "Barang/Jasa :"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 449)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 10252
        Me.Label5.Text = "Catatan :"
        '
        'txt_Catatan
        '
        Me.txt_Catatan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Catatan.Location = New System.Drawing.Point(16, 465)
        Me.txt_Catatan.Name = "txt_Catatan"
        Me.txt_Catatan.Size = New System.Drawing.Size(259, 111)
        Me.txt_Catatan.TabIndex = 200
        Me.txt_Catatan.Text = ""
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(769, 584)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 10245
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
        Me.btn_Batal.TabIndex = 10246
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'dtp_TanggalSJ
        '
        Me.dtp_TanggalSJ.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalSJ.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalSJ.Location = New System.Drawing.Point(118, 45)
        Me.dtp_TanggalSJ.Name = "dtp_TanggalSJ"
        Me.dtp_TanggalSJ.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalSJ.TabIndex = 20
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(16, 49)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(81, 13)
        Me.Label50.TabIndex = 10250
        Me.Label50.Text = "Tgl. Surat Jalan"
        '
        'txt_NomorSJ
        '
        Me.txt_NomorSJ.Location = New System.Drawing.Point(118, 19)
        Me.txt_NomorSJ.Name = "txt_NomorSJ"
        Me.txt_NomorSJ.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorSJ.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 10249
        Me.Label1.Text = "Nomor Surat Jalan"
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
        Me.DataTabelUtama.Location = New System.Drawing.Point(18, 218)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(834, 217)
        Me.DataTabelUtama.TabIndex = 10237
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(497, 18)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 90
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'txt_KodeCustomer
        '
        Me.txt_KodeCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_KodeCustomer.Location = New System.Drawing.Point(410, 19)
        Me.txt_KodeCustomer.MaxLength = 3
        Me.txt_KodeCustomer.Name = "txt_KodeCustomer"
        Me.txt_KodeCustomer.Size = New System.Drawing.Size(81, 20)
        Me.txt_KodeCustomer.TabIndex = 80
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(314, 22)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(79, 13)
        Me.Label24.TabIndex = 10247
        Me.Label24.Text = "Kode Customer"
        '
        'txt_NamaCustomer
        '
        Me.txt_NamaCustomer.Location = New System.Drawing.Point(410, 45)
        Me.txt_NamaCustomer.Name = "txt_NamaCustomer"
        Me.txt_NamaCustomer.Size = New System.Drawing.Size(193, 20)
        Me.txt_NamaCustomer.TabIndex = 100
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(314, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 10248
        Me.Label2.Text = "Nama Customer"
        '
        'txt_NamaSupir
        '
        Me.txt_NamaSupir.Location = New System.Drawing.Point(118, 98)
        Me.txt_NamaSupir.Name = "txt_NamaSupir"
        Me.txt_NamaSupir.Size = New System.Drawing.Size(163, 20)
        Me.txt_NamaSupir.TabIndex = 40
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(16, 101)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 13)
        Me.Label15.TabIndex = 10264
        Me.Label15.Text = "Nama Supir"
        '
        'txt_PlatNomor
        '
        Me.txt_PlatNomor.Location = New System.Drawing.Point(118, 71)
        Me.txt_PlatNomor.Name = "txt_PlatNomor"
        Me.txt_PlatNomor.Size = New System.Drawing.Size(163, 20)
        Me.txt_PlatNomor.TabIndex = 30
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(16, 74)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(59, 13)
        Me.Label16.TabIndex = 10266
        Me.Label16.Text = "Plat Nomor"
        '
        'btn_Pratinjau
        '
        Me.btn_Pratinjau.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pratinjau.Location = New System.Drawing.Point(760, 12)
        Me.btn_Pratinjau.Name = "btn_Pratinjau"
        Me.btn_Pratinjau.Size = New System.Drawing.Size(92, 46)
        Me.btn_Pratinjau.TabIndex = 10270
        Me.btn_Pratinjau.Text = "Pratinjau"
        Me.btn_Pratinjau.UseVisualStyleBackColor = True
        '
        'btn_Cetak
        '
        Me.btn_Cetak.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cetak.Location = New System.Drawing.Point(760, 64)
        Me.btn_Cetak.Name = "btn_Cetak"
        Me.btn_Cetak.Size = New System.Drawing.Size(92, 46)
        Me.btn_Cetak.TabIndex = 10269
        Me.btn_Cetak.Text = "Cetak"
        Me.btn_Cetak.UseVisualStyleBackColor = True
        '
        'txt_NamaPengirim
        '
        Me.txt_NamaPengirim.Location = New System.Drawing.Point(118, 127)
        Me.txt_NamaPengirim.Name = "txt_NamaPengirim"
        Me.txt_NamaPengirim.Size = New System.Drawing.Size(163, 20)
        Me.txt_NamaPengirim.TabIndex = 50
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 130)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 10272
        Me.Label6.Text = "Nama Pengirim"
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
        Me.dgv_PO.TabIndex = 10273
        '
        'Nomor_PO
        '
        Me.Nomor_PO.HeaderText = "Nomor PO"
        Me.Nomor_PO.Name = "Nomor_PO"
        Me.Nomor_PO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_PO.Width = 123
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
        'btn_SingkirkanPO
        '
        Me.btn_SingkirkanPO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_SingkirkanPO.Enabled = False
        Me.btn_SingkirkanPO.Location = New System.Drawing.Point(664, 495)
        Me.btn_SingkirkanPO.Name = "btn_SingkirkanPO"
        Me.btn_SingkirkanPO.Size = New System.Drawing.Size(24, 24)
        Me.btn_SingkirkanPO.TabIndex = 310
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
        Me.btn_TambahPO.TabIndex = 300
        Me.btn_TambahPO.Text = "+"
        Me.btn_TambahPO.UseVisualStyleBackColor = True
        '
        'dtp_TanggalDiterima
        '
        Me.dtp_TanggalDiterima.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalDiterima.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalDiterima.Location = New System.Drawing.Point(118, 179)
        Me.dtp_TanggalDiterima.Name = "dtp_TanggalDiterima"
        Me.dtp_TanggalDiterima.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalDiterima.TabIndex = 70
        '
        'lbl_TanggalDiterima
        '
        Me.lbl_TanggalDiterima.AutoSize = True
        Me.lbl_TanggalDiterima.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TanggalDiterima.Location = New System.Drawing.Point(16, 183)
        Me.lbl_TanggalDiterima.Name = "lbl_TanggalDiterima"
        Me.lbl_TanggalDiterima.Size = New System.Drawing.Size(66, 13)
        Me.lbl_TanggalDiterima.TabIndex = 10279
        Me.lbl_TanggalDiterima.Text = "Tgl. Diterima"
        '
        'txt_NamaPenerima
        '
        Me.txt_NamaPenerima.Location = New System.Drawing.Point(118, 153)
        Me.txt_NamaPenerima.Name = "txt_NamaPenerima"
        Me.txt_NamaPenerima.Size = New System.Drawing.Size(163, 20)
        Me.txt_NamaPenerima.TabIndex = 60
        '
        'lbl_NamaPenerima
        '
        Me.lbl_NamaPenerima.AutoSize = True
        Me.lbl_NamaPenerima.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaPenerima.Location = New System.Drawing.Point(16, 156)
        Me.lbl_NamaPenerima.Name = "lbl_NamaPenerima"
        Me.lbl_NamaPenerima.Size = New System.Drawing.Size(82, 13)
        Me.lbl_NamaPenerima.TabIndex = 10278
        Me.lbl_NamaPenerima.Text = "Nama Penerima"
        '
        'lbl_PerlakuanPPN
        '
        Me.lbl_PerlakuanPPN.AutoSize = True
        Me.lbl_PerlakuanPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PerlakuanPPN.Location = New System.Drawing.Point(503, 164)
        Me.lbl_PerlakuanPPN.Name = "lbl_PerlakuanPPN"
        Me.lbl_PerlakuanPPN.Size = New System.Drawing.Size(86, 13)
        Me.lbl_PerlakuanPPN.TabIndex = 10316
        Me.lbl_PerlakuanPPN.Text = "Perlakuan PPN :"
        Me.lbl_PerlakuanPPN.Visible = False
        '
        'cmb_PerlakuanPPN
        '
        Me.cmb_PerlakuanPPN.FormattingEnabled = True
        Me.cmb_PerlakuanPPN.Location = New System.Drawing.Point(506, 183)
        Me.cmb_PerlakuanPPN.Name = "cmb_PerlakuanPPN"
        Me.cmb_PerlakuanPPN.Size = New System.Drawing.Size(97, 21)
        Me.cmb_PerlakuanPPN.TabIndex = 10314
        Me.cmb_PerlakuanPPN.Visible = False
        '
        'lbl_JenisPPN
        '
        Me.lbl_JenisPPN.AutoSize = True
        Me.lbl_JenisPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JenisPPN.Location = New System.Drawing.Point(400, 164)
        Me.lbl_JenisPPN.Name = "lbl_JenisPPN"
        Me.lbl_JenisPPN.Size = New System.Drawing.Size(62, 13)
        Me.lbl_JenisPPN.TabIndex = 10315
        Me.lbl_JenisPPN.Text = "Jenis PPN :"
        Me.lbl_JenisPPN.Visible = False
        '
        'cmb_JenisPPN
        '
        Me.cmb_JenisPPN.FormattingEnabled = True
        Me.cmb_JenisPPN.Location = New System.Drawing.Point(403, 183)
        Me.cmb_JenisPPN.Name = "cmb_JenisPPN"
        Me.cmb_JenisPPN.Size = New System.Drawing.Size(92, 21)
        Me.cmb_JenisPPN.TabIndex = 10313
        Me.cmb_JenisPPN.Visible = False
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
        '
        'Keterangan_Produk
        '
        Me.Keterangan_Produk.HeaderText = "Keterangan"
        Me.Keterangan_Produk.Name = "Keterangan_Produk"
        Me.Keterangan_Produk.ReadOnly = True
        Me.Keterangan_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Keterangan_Produk.Width = 381
        '
        'frm_Input_SuratJalanPenjualan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 635)
        Me.Controls.Add(Me.lbl_PerlakuanPPN)
        Me.Controls.Add(Me.cmb_PerlakuanPPN)
        Me.Controls.Add(Me.lbl_JenisPPN)
        Me.Controls.Add(Me.cmb_JenisPPN)
        Me.Controls.Add(Me.dtp_TanggalDiterima)
        Me.Controls.Add(Me.lbl_TanggalDiterima)
        Me.Controls.Add(Me.txt_NamaPenerima)
        Me.Controls.Add(Me.lbl_NamaPenerima)
        Me.Controls.Add(Me.btn_SingkirkanPO)
        Me.Controls.Add(Me.btn_TambahPO)
        Me.Controls.Add(Me.dgv_PO)
        Me.Controls.Add(Me.txt_NamaPengirim)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_Pratinjau)
        Me.Controls.Add(Me.btn_Cetak)
        Me.Controls.Add(Me.txt_PlatNomor)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txt_NamaSupir)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txt_AlamatCustomer)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.grb_Produk)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_Catatan)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.dtp_TanggalSJ)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.txt_NomorSJ)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_KodeCustomer)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txt_NamaCustomer)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_Input_SuratJalanPenjualan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Surat Jalan"
        Me.grb_Produk.ResumeLayout(False)
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_PO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Edit As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents txt_AlamatCustomer As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents grb_Produk As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Catatan As RichTextBox
    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents dtp_TanggalSJ As DateTimePicker
    Friend WithEvents Label50 As Label
    Friend WithEvents txt_NomorSJ As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents txt_KodeCustomer As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_NamaCustomer As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NamaSupir As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txt_PlatNomor As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents btn_Pratinjau As Button
    Friend WithEvents btn_Cetak As Button
    Friend WithEvents txt_NamaPengirim As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents dgv_PO As DataGridView
    Friend WithEvents btn_SingkirkanPO As Button
    Friend WithEvents btn_TambahPO As Button
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_PO As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents dtp_TanggalDiterima As DateTimePicker
    Friend WithEvents lbl_TanggalDiterima As Label
    Friend WithEvents txt_NamaPenerima As TextBox
    Friend WithEvents lbl_NamaPenerima As Label
    Friend WithEvents lbl_PerlakuanPPN As Label
    Friend WithEvents cmb_PerlakuanPPN As ComboBox
    Friend WithEvents lbl_JenisPPN As Label
    Friend WithEvents cmb_JenisPPN As ComboBox
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
