<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Input_ReturPembelian
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_Diskon_Persen = New System.Windows.Forms.TextBox()
        Me.lbl_JenisPPN = New System.Windows.Forms.Label()
        Me.cmb_JenisPPN = New System.Windows.Forms.ComboBox()
        Me.grb_Produk = New System.Windows.Forms.GroupBox()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.txt_TotalRetur = New System.Windows.Forms.TextBox()
        Me.lbl_PPN = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_PPN = New System.Windows.Forms.TextBox()
        Me.txt_DasarPengenaanPajak = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_Diskon_Rp = New System.Windows.Forms.TextBox()
        Me.lbl_Diskon = New System.Windows.Forms.Label()
        Me.txt_JumlahHargaKeseluruhan = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Catatan = New System.Windows.Forms.RichTextBox()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.dtp_TanggalRetur = New System.Windows.Forms.DateTimePicker()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txt_NomorRetur = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.txt_KodeSupplier = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txt_NamaSupplier = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_PerlakuanPPN = New System.Windows.Forms.Label()
        Me.cmb_PerlakuanPPN = New System.Windows.Forms.ComboBox()
        Me.btn_SingkirkanInvoice = New System.Windows.Forms.Button()
        Me.btn_TambahInvoice = New System.Windows.Forms.Button()
        Me.dgv_Invoice = New System.Windows.Forms.DataGridView()
        Me.Nomor_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Project = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_Produk_Per_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Invoice_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Invoice_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COA_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Deskripsi_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Satuan_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Harga_Satuan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Harga_Per_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diskon_Per_Item_Persen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Diskon_Per_Item_Rp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_Harga = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Project_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grb_Produk.SuspendLayout()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv_Invoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(819, 404)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(15, 13)
        Me.Label14.TabIndex = 10222
        Me.Label14.Text = "%"
        Me.Label14.Visible = False
        '
        'txt_Diskon_Persen
        '
        Me.txt_Diskon_Persen.Location = New System.Drawing.Point(785, 401)
        Me.txt_Diskon_Persen.MaxLength = 5
        Me.txt_Diskon_Persen.Name = "txt_Diskon_Persen"
        Me.txt_Diskon_Persen.Size = New System.Drawing.Size(33, 20)
        Me.txt_Diskon_Persen.TabIndex = 10200
        Me.txt_Diskon_Persen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_Diskon_Persen.Visible = False
        '
        'lbl_JenisPPN
        '
        Me.lbl_JenisPPN.AutoSize = True
        Me.lbl_JenisPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JenisPPN.Location = New System.Drawing.Point(16, 495)
        Me.lbl_JenisPPN.Name = "lbl_JenisPPN"
        Me.lbl_JenisPPN.Size = New System.Drawing.Size(62, 13)
        Me.lbl_JenisPPN.TabIndex = 10221
        Me.lbl_JenisPPN.Text = "Jenis PPN :"
        Me.lbl_JenisPPN.Visible = False
        '
        'cmb_JenisPPN
        '
        Me.cmb_JenisPPN.FormattingEnabled = True
        Me.cmb_JenisPPN.Location = New System.Drawing.Point(19, 514)
        Me.cmb_JenisPPN.Name = "cmb_JenisPPN"
        Me.cmb_JenisPPN.Size = New System.Drawing.Size(92, 21)
        Me.cmb_JenisPPN.TabIndex = 10195
        Me.cmb_JenisPPN.Visible = False
        '
        'grb_Produk
        '
        Me.grb_Produk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grb_Produk.Controls.Add(Me.btn_Hapus)
        Me.grb_Produk.Controls.Add(Me.btn_Edit)
        Me.grb_Produk.Location = New System.Drawing.Point(774, 14)
        Me.grb_Produk.Name = "grb_Produk"
        Me.grb_Produk.Size = New System.Drawing.Size(158, 55)
        Me.grb_Produk.TabIndex = 10196
        Me.grb_Produk.TabStop = False
        Me.grb_Produk.Text = "Barang/Jasa :"
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
        'txt_TotalRetur
        '
        Me.txt_TotalRetur.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TotalRetur.Location = New System.Drawing.Point(840, 479)
        Me.txt_TotalRetur.Name = "txt_TotalRetur"
        Me.txt_TotalRetur.Size = New System.Drawing.Size(92, 20)
        Me.txt_TotalRetur.TabIndex = 10204
        Me.txt_TotalRetur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PPN
        '
        Me.lbl_PPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_PPN.AutoSize = True
        Me.lbl_PPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PPN.Location = New System.Drawing.Point(706, 456)
        Me.lbl_PPN.Name = "lbl_PPN"
        Me.lbl_PPN.Size = New System.Drawing.Size(29, 13)
        Me.lbl_PPN.TabIndex = 10217
        Me.lbl_PPN.Text = "PPN"
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(706, 482)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 10218
        Me.Label10.Text = "Total Retur"
        '
        'txt_PPN
        '
        Me.txt_PPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PPN.Location = New System.Drawing.Point(840, 453)
        Me.txt_PPN.Name = "txt_PPN"
        Me.txt_PPN.Size = New System.Drawing.Size(92, 20)
        Me.txt_PPN.TabIndex = 10203
        Me.txt_PPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_DasarPengenaanPajak
        '
        Me.txt_DasarPengenaanPajak.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DasarPengenaanPajak.Location = New System.Drawing.Point(840, 427)
        Me.txt_DasarPengenaanPajak.Name = "txt_DasarPengenaanPajak"
        Me.txt_DasarPengenaanPajak.Size = New System.Drawing.Size(92, 20)
        Me.txt_DasarPengenaanPajak.TabIndex = 10202
        Me.txt_DasarPengenaanPajak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(706, 430)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(123, 13)
        Me.Label9.TabIndex = 10216
        Me.Label9.Text = "Dasar Pengenaan Pajak"
        '
        'txt_Diskon_Rp
        '
        Me.txt_Diskon_Rp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Diskon_Rp.Location = New System.Drawing.Point(840, 401)
        Me.txt_Diskon_Rp.Name = "txt_Diskon_Rp"
        Me.txt_Diskon_Rp.Size = New System.Drawing.Size(92, 20)
        Me.txt_Diskon_Rp.TabIndex = 10201
        Me.txt_Diskon_Rp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Diskon
        '
        Me.lbl_Diskon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Diskon.AutoSize = True
        Me.lbl_Diskon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diskon.Location = New System.Drawing.Point(706, 404)
        Me.lbl_Diskon.Name = "lbl_Diskon"
        Me.lbl_Diskon.Size = New System.Drawing.Size(49, 13)
        Me.lbl_Diskon.TabIndex = 10215
        Me.lbl_Diskon.Text = "Discount"
        '
        'txt_JumlahHargaKeseluruhan
        '
        Me.txt_JumlahHargaKeseluruhan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_JumlahHargaKeseluruhan.Location = New System.Drawing.Point(840, 375)
        Me.txt_JumlahHargaKeseluruhan.Name = "txt_JumlahHargaKeseluruhan"
        Me.txt_JumlahHargaKeseluruhan.Size = New System.Drawing.Size(92, 20)
        Me.txt_JumlahHargaKeseluruhan.TabIndex = 10199
        Me.txt_JumlahHargaKeseluruhan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(706, 378)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 10214
        Me.Label6.Text = "Jumlah"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 382)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 10213
        Me.Label5.Text = "Catatan :"
        '
        'txt_Catatan
        '
        Me.txt_Catatan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Catatan.Location = New System.Drawing.Point(16, 398)
        Me.txt_Catatan.Name = "txt_Catatan"
        Me.txt_Catatan.Size = New System.Drawing.Size(240, 77)
        Me.txt_Catatan.TabIndex = 10198
        Me.txt_Catatan.Text = ""
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(849, 517)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 10205
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(760, 517)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10206
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'dtp_TanggalRetur
        '
        Me.dtp_TanggalRetur.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalRetur.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalRetur.Location = New System.Drawing.Point(118, 45)
        Me.dtp_TanggalRetur.Name = "dtp_TanggalRetur"
        Me.dtp_TanggalRetur.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalRetur.TabIndex = 10186
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(16, 49)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(75, 13)
        Me.Label50.TabIndex = 10211
        Me.Label50.Text = "Tanggal Retur"
        '
        'txt_NomorRetur
        '
        Me.txt_NomorRetur.Location = New System.Drawing.Point(118, 19)
        Me.txt_NomorRetur.Name = "txt_NomorRetur"
        Me.txt_NomorRetur.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorRetur.TabIndex = 10185
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 10210
        Me.Label1.Text = "Nomor Retur"
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Jenis_Produk_Per_Item, Me.Nomor_Invoice_Produk, Me.Tanggal_Invoice_Produk, Me.COA_Produk, Me.Nama_Produk, Me.Deskripsi_Produk, Me.Jumlah_Produk, Me.Satuan_Produk, Me.Harga_Satuan, Me.Jumlah_Harga_Per_Item, Me.Diskon_Per_Item_Persen, Me.Diskon_Per_Item_Rp, Me.Total_Harga, Me.Kode_Project_Produk})
        Me.DataTabelUtama.Location = New System.Drawing.Point(18, 88)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(914, 272)
        Me.DataTabelUtama.TabIndex = 10197
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(497, 18)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 10192
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
        Me.txt_KodeSupplier.TabIndex = 10191
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(314, 22)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(73, 13)
        Me.Label24.TabIndex = 10208
        Me.Label24.Text = "Kode Supplier"
        '
        'txt_NamaSupplier
        '
        Me.txt_NamaSupplier.Location = New System.Drawing.Point(410, 45)
        Me.txt_NamaSupplier.Name = "txt_NamaSupplier"
        Me.txt_NamaSupplier.Size = New System.Drawing.Size(193, 20)
        Me.txt_NamaSupplier.TabIndex = 10193
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(314, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 10209
        Me.Label2.Text = "Nama Supplier"
        '
        'lbl_PerlakuanPPN
        '
        Me.lbl_PerlakuanPPN.AutoSize = True
        Me.lbl_PerlakuanPPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PerlakuanPPN.Location = New System.Drawing.Point(139, 495)
        Me.lbl_PerlakuanPPN.Name = "lbl_PerlakuanPPN"
        Me.lbl_PerlakuanPPN.Size = New System.Drawing.Size(86, 13)
        Me.lbl_PerlakuanPPN.TabIndex = 10314
        Me.lbl_PerlakuanPPN.Text = "Perlakuan PPN :"
        Me.lbl_PerlakuanPPN.Visible = False
        '
        'cmb_PerlakuanPPN
        '
        Me.cmb_PerlakuanPPN.FormattingEnabled = True
        Me.cmb_PerlakuanPPN.Location = New System.Drawing.Point(142, 514)
        Me.cmb_PerlakuanPPN.Name = "cmb_PerlakuanPPN"
        Me.cmb_PerlakuanPPN.Size = New System.Drawing.Size(97, 21)
        Me.cmb_PerlakuanPPN.TabIndex = 10313
        Me.cmb_PerlakuanPPN.Visible = False
        '
        'btn_SingkirkanInvoice
        '
        Me.btn_SingkirkanInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_SingkirkanInvoice.Enabled = False
        Me.btn_SingkirkanInvoice.Location = New System.Drawing.Point(623, 371)
        Me.btn_SingkirkanInvoice.Name = "btn_SingkirkanInvoice"
        Me.btn_SingkirkanInvoice.Size = New System.Drawing.Size(24, 24)
        Me.btn_SingkirkanInvoice.TabIndex = 10318
        Me.btn_SingkirkanInvoice.Text = "-"
        Me.btn_SingkirkanInvoice.UseVisualStyleBackColor = True
        '
        'btn_TambahInvoice
        '
        Me.btn_TambahInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_TambahInvoice.Enabled = False
        Me.btn_TambahInvoice.Location = New System.Drawing.Point(596, 371)
        Me.btn_TambahInvoice.Name = "btn_TambahInvoice"
        Me.btn_TambahInvoice.Size = New System.Drawing.Size(24, 24)
        Me.btn_TambahInvoice.TabIndex = 10317
        Me.btn_TambahInvoice.Text = "+"
        Me.btn_TambahInvoice.UseVisualStyleBackColor = True
        '
        'dgv_Invoice
        '
        Me.dgv_Invoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dgv_Invoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Invoice.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Invoice, Me.Tanggal_Invoice, Me.Kode_Project})
        Me.dgv_Invoice.Location = New System.Drawing.Point(308, 398)
        Me.dgv_Invoice.Name = "dgv_Invoice"
        Me.dgv_Invoice.RowHeadersVisible = False
        Me.dgv_Invoice.Size = New System.Drawing.Size(339, 137)
        Me.dgv_Invoice.TabIndex = 10316
        '
        'Nomor_Invoice
        '
        Me.Nomor_Invoice.HeaderText = "Nomor Invoice"
        Me.Nomor_Invoice.Name = "Nomor_Invoice"
        Me.Nomor_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Invoice.Width = 99
        '
        'Tanggal_Invoice
        '
        Me.Tanggal_Invoice.HeaderText = "Tanggal"
        Me.Tanggal_Invoice.Name = "Tanggal_Invoice"
        Me.Tanggal_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Invoice.Width = 63
        '
        'Kode_Project
        '
        Me.Kode_Project.HeaderText = "Kode Proyek"
        Me.Kode_Project.Name = "Kode_Project"
        Me.Kode_Project.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Project.Width = 172
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(305, 378)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 13)
        Me.Label17.TabIndex = 10315
        Me.Label17.Text = "Invoice :"
        '
        'Nomor_Urut
        '
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 33
        '
        'Jenis_Produk_Per_Item
        '
        Me.Jenis_Produk_Per_Item.HeaderText = "Jenis Produk"
        Me.Jenis_Produk_Per_Item.Name = "Jenis_Produk_Per_Item"
        Me.Jenis_Produk_Per_Item.ReadOnly = True
        Me.Jenis_Produk_Per_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jenis_Produk_Per_Item.Visible = False
        '
        'Nomor_Invoice_Produk
        '
        Me.Nomor_Invoice_Produk.HeaderText = "Nomor Invoice"
        Me.Nomor_Invoice_Produk.Name = "Nomor_Invoice_Produk"
        Me.Nomor_Invoice_Produk.ReadOnly = True
        Me.Nomor_Invoice_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Invoice_Produk.Width = 81
        '
        'Tanggal_Invoice_Produk
        '
        Me.Tanggal_Invoice_Produk.HeaderText = "Tanggal Invoice"
        Me.Tanggal_Invoice_Produk.Name = "Tanggal_Invoice_Produk"
        Me.Tanggal_Invoice_Produk.ReadOnly = True
        Me.Tanggal_Invoice_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Invoice_Produk.Visible = False
        '
        'COA_Produk
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.COA_Produk.DefaultCellStyle = DataGridViewCellStyle1
        Me.COA_Produk.HeaderText = "COA"
        Me.COA_Produk.Name = "COA_Produk"
        Me.COA_Produk.ReadOnly = True
        Me.COA_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.COA_Produk.Width = 45
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
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Me.Jumlah_Produk.DefaultCellStyle = DataGridViewCellStyle2
        Me.Jumlah_Produk.HeaderText = "Jumlah"
        Me.Jumlah_Produk.Name = "Jumlah_Produk"
        Me.Jumlah_Produk.ReadOnly = True
        Me.Jumlah_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Produk.Width = 63
        '
        'Satuan_Produk
        '
        Me.Satuan_Produk.HeaderText = "Satuan"
        Me.Satuan_Produk.Name = "Satuan_Produk"
        Me.Satuan_Produk.ReadOnly = True
        Me.Satuan_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Satuan_Produk.Width = 63
        '
        'Harga_Satuan
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Me.Harga_Satuan.DefaultCellStyle = DataGridViewCellStyle3
        Me.Harga_Satuan.HeaderText = "Harga Satuan"
        Me.Harga_Satuan.Name = "Harga_Satuan"
        Me.Harga_Satuan.ReadOnly = True
        Me.Harga_Satuan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Harga_Satuan.Width = 81
        '
        'Jumlah_Harga_Per_Item
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Me.Jumlah_Harga_Per_Item.DefaultCellStyle = DataGridViewCellStyle4
        Me.Jumlah_Harga_Per_Item.HeaderText = "Jumlah Harga"
        Me.Jumlah_Harga_Per_Item.Name = "Jumlah_Harga_Per_Item"
        Me.Jumlah_Harga_Per_Item.ReadOnly = True
        Me.Jumlah_Harga_Per_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Diskon_Per_Item_Persen
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Diskon_Per_Item_Persen.DefaultCellStyle = DataGridViewCellStyle5
        Me.Diskon_Per_Item_Persen.HeaderText = "Diskon (%)"
        Me.Diskon_Per_Item_Persen.Name = "Diskon_Per_Item_Persen"
        Me.Diskon_Per_Item_Persen.ReadOnly = True
        Me.Diskon_Per_Item_Persen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Diskon_Per_Item_Persen.Width = 45
        '
        'Diskon_Per_Item_Rp
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Me.Diskon_Per_Item_Rp.DefaultCellStyle = DataGridViewCellStyle6
        Me.Diskon_Per_Item_Rp.HeaderText = "Diskon (Rp)"
        Me.Diskon_Per_Item_Rp.Name = "Diskon_Per_Item_Rp"
        Me.Diskon_Per_Item_Rp.ReadOnly = True
        Me.Diskon_Per_Item_Rp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Diskon_Per_Item_Rp.Width = 81
        '
        'Total_Harga
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Me.Total_Harga.DefaultCellStyle = DataGridViewCellStyle7
        Me.Total_Harga.HeaderText = "Total"
        Me.Total_Harga.Name = "Total_Harga"
        Me.Total_Harga.ReadOnly = True
        Me.Total_Harga.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Total_Harga.Width = 81
        '
        'Kode_Project_Produk
        '
        Me.Kode_Project_Produk.HeaderText = "Kode Proyek"
        Me.Kode_Project_Produk.Name = "Kode_Project_Produk"
        Me.Kode_Project_Produk.ReadOnly = True
        Me.Kode_Project_Produk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Project_Produk.Visible = False
        '
        'frm_Input_ReturPembelian
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(949, 568)
        Me.Controls.Add(Me.btn_SingkirkanInvoice)
        Me.Controls.Add(Me.btn_TambahInvoice)
        Me.Controls.Add(Me.dgv_Invoice)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.lbl_PerlakuanPPN)
        Me.Controls.Add(Me.cmb_PerlakuanPPN)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txt_Diskon_Persen)
        Me.Controls.Add(Me.lbl_JenisPPN)
        Me.Controls.Add(Me.cmb_JenisPPN)
        Me.Controls.Add(Me.grb_Produk)
        Me.Controls.Add(Me.txt_TotalRetur)
        Me.Controls.Add(Me.lbl_PPN)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_PPN)
        Me.Controls.Add(Me.txt_DasarPengenaanPajak)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_Diskon_Rp)
        Me.Controls.Add(Me.lbl_Diskon)
        Me.Controls.Add(Me.txt_JumlahHargaKeseluruhan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_Catatan)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.dtp_TanggalRetur)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.txt_NomorRetur)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_KodeSupplier)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txt_NamaSupplier)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_Input_ReturPembelian"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Retur Pembelian"
        Me.grb_Produk.ResumeLayout(False)
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv_Invoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label14 As Label
    Friend WithEvents txt_Diskon_Persen As TextBox
    Friend WithEvents lbl_JenisPPN As Label
    Friend WithEvents cmb_JenisPPN As ComboBox
    Friend WithEvents grb_Produk As GroupBox
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Edit As Button
    Friend WithEvents txt_TotalRetur As TextBox
    Friend WithEvents lbl_PPN As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_PPN As TextBox
    Friend WithEvents txt_DasarPengenaanPajak As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_Diskon_Rp As TextBox
    Friend WithEvents lbl_Diskon As Label
    Friend WithEvents txt_JumlahHargaKeseluruhan As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Catatan As RichTextBox
    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents dtp_TanggalRetur As DateTimePicker
    Friend WithEvents Label50 As Label
    Friend WithEvents txt_NomorRetur As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents txt_KodeSupplier As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_NamaSupplier As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbl_PerlakuanPPN As Label
    Friend WithEvents cmb_PerlakuanPPN As ComboBox
    Friend WithEvents btn_SingkirkanInvoice As Button
    Friend WithEvents btn_TambahInvoice As Button
    Friend WithEvents dgv_Invoice As DataGridView
    Friend WithEvents Label17 As Label
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk_Per_Item As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice_Produk As DataGridViewTextBoxColumn
    Friend WithEvents COA_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Deskripsi_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Satuan_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Harga_Satuan As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Harga_Per_Item As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Per_Item_Persen As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Per_Item_Rp As DataGridViewTextBoxColumn
    Friend WithEvents Total_Harga As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project_Produk As DataGridViewTextBoxColumn
End Class
