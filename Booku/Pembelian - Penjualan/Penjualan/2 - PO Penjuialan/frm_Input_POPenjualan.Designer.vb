<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Input_POPenjualan
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
        Label14 = New Label()
        txt_Diskon_Persen = New TextBox()
        lbl_JenisPPN = New Label()
        cmb_JenisPPN = New ComboBox()
        txt_KeteranganToP = New TextBox()
        txt_AlamatCustomer = New TextBox()
        Label11 = New Label()
        grb_Produk = New GroupBox()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        btn_Tambah = New Button()
        Label12 = New Label()
        txt_TotalTagihan = New TextBox()
        Label10 = New Label()
        txt_PPN = New TextBox()
        Label8 = New Label()
        txt_DasarPengenaanPajak = New TextBox()
        Label9 = New Label()
        txt_Diskon_Rp = New TextBox()
        lbl_Diskon = New Label()
        txt_JumlahHargaKeseluruhan = New TextBox()
        Label6 = New Label()
        Label5 = New Label()
        txt_Catatan = New RichTextBox()
        btn_Simpan = New Button()
        btn_Batal = New Button()
        txt_KodeProject = New TextBox()
        txt_TermOfPayment = New TextBox()
        Label3 = New Label()
        dtp_TanggalPO = New DateTimePicker()
        Label50 = New Label()
        txt_NomorPO = New TextBox()
        Label1 = New Label()
        DataTabelUtama = New DataGridView()
        Label4 = New Label()
        btn_PilihMitra = New Button()
        txt_KodeCustomer = New TextBox()
        Label24 = New Label()
        txt_NamaCustomer = New TextBox()
        Label2 = New Label()
        Label15 = New Label()
        cmb_Kontrol = New ComboBox()
        dgv_SJBAST = New DataGridView()
        Nomor_SJ_BAST = New DataGridViewTextBoxColumn()
        Tanggal_SJ_BAST = New DataGridViewTextBoxColumn()
        lbl_SJBAST = New Label()
        dgv_Invoice = New DataGridView()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        lbl_Invoice = New Label()
        lbl_PerlakuanPPN = New Label()
        cmb_PerlakuanPPN = New ComboBox()
        txt_PPhTerutang = New TextBox()
        cmb_JenisPPh = New ComboBox()
        lbl_PersenPPh = New Label()
        txt_TarifPPh = New TextBox()
        lbl_PPh = New Label()
        txt_TotalTagihan_Kotor = New TextBox()
        Label16 = New Label()
        txt_PPhDipotong = New TextBox()
        lbl_PPhDipotong = New Label()
        txt_PPhDitanggung = New TextBox()
        lbl_PPhDitanggung = New Label()
        txt_DPPBarang = New TextBox()
        lbl_DPPBarang = New Label()
        txt_DPPJasa = New TextBox()
        lbl_DPPJasa = New Label()
        rdb_TanggalJangkaWaktuPenyelesaian = New RadioButton()
        rdb_JumlahHariJangkaWaktuPenyelesaian = New RadioButton()
        lbl_JumlahHariJangkaWaktuPenyelesaian = New Label()
        txt_JumlahHariJangkaWaktuPenyelesaian = New TextBox()
        dtp_TanggalJangkaWaktuPenyelesaian = New DateTimePicker()
        Label17 = New Label()
        Label18 = New Label()
        txt_BiayaTransportasiPenjualan = New TextBox()
        btn_PilihKodeProject = New Button()
        Label7 = New Label()
        txt_TarifPPN = New TextBox()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Jenis_Produk_Per_Item = New DataGridViewTextBoxColumn()
        Nama_Produk = New DataGridViewTextBoxColumn()
        Deskripsi_Produk = New DataGridViewTextBoxColumn()
        Jumlah_Produk = New DataGridViewTextBoxColumn()
        Satuan_Produk = New DataGridViewTextBoxColumn()
        Harga_Satuan = New DataGridViewTextBoxColumn()
        Jumlah_Harga_Per_Item = New DataGridViewTextBoxColumn()
        Diskon_Per_Item_Persen = New DataGridViewTextBoxColumn()
        Diskon_Per_Item_Rp = New DataGridViewTextBoxColumn()
        Total_Harga = New DataGridViewTextBoxColumn()
        Kode_Project_Produk = New DataGridViewTextBoxColumn()
        grb_Produk.SuspendLayout()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgv_SJBAST, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgv_Invoice, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label14
        ' 
        Label14.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label14.AutoSize = True
        Label14.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label14.Location = New Point(918, 464)
        Label14.Margin = New Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(15, 13)
        Label14.TabIndex = 10180
        Label14.Text = "%"
        Label14.Visible = False
        ' 
        ' txt_Diskon_Persen
        ' 
        txt_Diskon_Persen.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_Diskon_Persen.Location = New Point(881, 459)
        txt_Diskon_Persen.Margin = New Padding(4, 3, 4, 3)
        txt_Diskon_Persen.MaxLength = 5
        txt_Diskon_Persen.Name = "txt_Diskon_Persen"
        txt_Diskon_Persen.Size = New Size(38, 23)
        txt_Diskon_Persen.TabIndex = 410
        txt_Diskon_Persen.TextAlign = HorizontalAlignment.Right
        txt_Diskon_Persen.Visible = False
        ' 
        ' lbl_JenisPPN
        ' 
        lbl_JenisPPN.AutoSize = True
        lbl_JenisPPN.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_JenisPPN.Location = New Point(358, 156)
        lbl_JenisPPN.Margin = New Padding(4, 0, 4, 0)
        lbl_JenisPPN.Name = "lbl_JenisPPN"
        lbl_JenisPPN.Size = New Size(62, 13)
        lbl_JenisPPN.TabIndex = 10179
        lbl_JenisPPN.Text = "Jenis PPN :"
        ' 
        ' cmb_JenisPPN
        ' 
        cmb_JenisPPN.FormattingEnabled = True
        cmb_JenisPPN.Location = New Point(362, 178)
        cmb_JenisPPN.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisPPN.Name = "cmb_JenisPPN"
        cmb_JenisPPN.Size = New Size(107, 23)
        cmb_JenisPPN.TabIndex = 100
        ' 
        ' txt_KeteranganToP
        ' 
        txt_KeteranganToP.Location = New Point(138, 112)
        txt_KeteranganToP.Margin = New Padding(4, 3, 4, 3)
        txt_KeteranganToP.Multiline = True
        txt_KeteranganToP.Name = "txt_KeteranganToP"
        txt_KeteranganToP.Size = New Size(190, 58)
        txt_KeteranganToP.TabIndex = 40
        ' 
        ' txt_AlamatCustomer
        ' 
        txt_AlamatCustomer.Location = New Point(470, 82)
        txt_AlamatCustomer.Margin = New Padding(4, 3, 4, 3)
        txt_AlamatCustomer.Multiline = True
        txt_AlamatCustomer.Name = "txt_AlamatCustomer"
        txt_AlamatCustomer.Size = New Size(224, 51)
        txt_AlamatCustomer.TabIndex = 90
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.Location = New Point(358, 85)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(86, 13)
        Label11.TabIndex = 10178
        Label11.Text = "Alamat Customer"
        ' 
        ' grb_Produk
        ' 
        grb_Produk.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_Produk.Controls.Add(btn_Hapus)
        grb_Produk.Controls.Add(btn_Edit)
        grb_Produk.Controls.Add(btn_Tambah)
        grb_Produk.Location = New Point(774, 143)
        grb_Produk.Margin = New Padding(4, 3, 4, 3)
        grb_Produk.Name = "grb_Produk"
        grb_Produk.Padding = New Padding(4, 3, 4, 3)
        grb_Produk.Size = New Size(271, 63)
        grb_Produk.TabIndex = 200
        grb_Produk.TabStop = False
        grb_Produk.Text = "Barang/Jasa :"
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Location = New Point(182, 18)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(79, 35)
        btn_Hapus.TabIndex = 230
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Location = New Point(96, 18)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(79, 35)
        btn_Edit.TabIndex = 220
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(9, 18)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(79, 35)
        btn_Tambah.TabIndex = 210
        btn_Tambah.Text = "Tambah"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.Location = New Point(191, 85)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(26, 13)
        Label12.TabIndex = 10176
        Label12.Text = "Hari"
        ' 
        ' txt_TotalTagihan
        ' 
        txt_TotalTagihan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_TotalTagihan.Location = New Point(937, 669)
        txt_TotalTagihan.Margin = New Padding(4, 3, 4, 3)
        txt_TotalTagihan.Name = "txt_TotalTagihan"
        txt_TotalTagihan.Size = New Size(107, 23)
        txt_TotalTagihan.TabIndex = 510
        txt_TotalTagihan.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label10
        ' 
        Label10.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label10.AutoSize = True
        Label10.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(733, 673)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(73, 13)
        Label10.TabIndex = 10175
        Label10.Text = "Total Tagihan"
        ' 
        ' txt_PPN
        ' 
        txt_PPN.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_PPN.Location = New Point(937, 519)
        txt_PPN.Margin = New Padding(4, 3, 4, 3)
        txt_PPN.Name = "txt_PPN"
        txt_PPN.Size = New Size(107, 23)
        txt_PPN.TabIndex = 440
        txt_PPN.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label8
        ' 
        Label8.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label8.AutoSize = True
        Label8.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(733, 523)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(29, 13)
        Label8.TabIndex = 10174
        Label8.Text = "PPN"
        ' 
        ' txt_DasarPengenaanPajak
        ' 
        txt_DasarPengenaanPajak.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_DasarPengenaanPajak.Location = New Point(937, 489)
        txt_DasarPengenaanPajak.Margin = New Padding(4, 3, 4, 3)
        txt_DasarPengenaanPajak.Name = "txt_DasarPengenaanPajak"
        txt_DasarPengenaanPajak.Size = New Size(107, 23)
        txt_DasarPengenaanPajak.TabIndex = 430
        txt_DasarPengenaanPajak.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label9
        ' 
        Label9.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label9.AutoSize = True
        Label9.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(733, 494)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(123, 13)
        Label9.TabIndex = 10173
        Label9.Text = "Dasar Pengenaan Pajak"
        ' 
        ' txt_Diskon_Rp
        ' 
        txt_Diskon_Rp.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_Diskon_Rp.Location = New Point(937, 459)
        txt_Diskon_Rp.Margin = New Padding(4, 3, 4, 3)
        txt_Diskon_Rp.Name = "txt_Diskon_Rp"
        txt_Diskon_Rp.Size = New Size(107, 23)
        txt_Diskon_Rp.TabIndex = 420
        txt_Diskon_Rp.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_Diskon
        ' 
        lbl_Diskon.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lbl_Diskon.AutoSize = True
        lbl_Diskon.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_Diskon.Location = New Point(733, 464)
        lbl_Diskon.Margin = New Padding(4, 0, 4, 0)
        lbl_Diskon.Name = "lbl_Diskon"
        lbl_Diskon.Size = New Size(49, 13)
        lbl_Diskon.TabIndex = 10172
        lbl_Diskon.Text = "Discount"
        ' 
        ' txt_JumlahHargaKeseluruhan
        ' 
        txt_JumlahHargaKeseluruhan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_JumlahHargaKeseluruhan.Location = New Point(937, 429)
        txt_JumlahHargaKeseluruhan.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahHargaKeseluruhan.Name = "txt_JumlahHargaKeseluruhan"
        txt_JumlahHargaKeseluruhan.Size = New Size(107, 23)
        txt_JumlahHargaKeseluruhan.TabIndex = 400
        txt_JumlahHargaKeseluruhan.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label6.AutoSize = True
        Label6.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(733, 433)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(40, 13)
        Label6.TabIndex = 10171
        Label6.Text = "Jumlah"
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(19, 433)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(54, 15)
        Label5.TabIndex = 10170
        Label5.Text = "Catatan :"
        ' 
        ' txt_Catatan
        ' 
        txt_Catatan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_Catatan.Location = New Point(21, 456)
        txt_Catatan.Margin = New Padding(4, 3, 4, 3)
        txt_Catatan.Name = "txt_Catatan"
        txt_Catatan.Size = New Size(270, 121)
        txt_Catatan.TabIndex = 500
        txt_Catatan.Text = ""
        ' 
        ' btn_Simpan
        ' 
        btn_Simpan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Simpan.Location = New Point(947, 711)
        btn_Simpan.Margin = New Padding(4, 3, 4, 3)
        btn_Simpan.Name = "btn_Simpan"
        btn_Simpan.Size = New Size(97, 40)
        btn_Simpan.TabIndex = 10161
        btn_Simpan.Text = "Simpan"
        btn_Simpan.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Batal.DialogResult = DialogResult.Cancel
        btn_Batal.Location = New Point(844, 711)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 10162
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' txt_KodeProject
        ' 
        txt_KodeProject.Location = New Point(138, 178)
        txt_KodeProject.Margin = New Padding(4, 3, 4, 3)
        txt_KodeProject.Name = "txt_KodeProject"
        txt_KodeProject.Size = New Size(136, 23)
        txt_KodeProject.TabIndex = 50
        ' 
        ' txt_TermOfPayment
        ' 
        txt_TermOfPayment.Location = New Point(138, 82)
        txt_TermOfPayment.Margin = New Padding(4, 3, 4, 3)
        txt_TermOfPayment.Name = "txt_TermOfPayment"
        txt_TermOfPayment.Size = New Size(51, 23)
        txt_TermOfPayment.TabIndex = 30
        txt_TermOfPayment.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(19, 85)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(87, 13)
        Label3.TabIndex = 10168
        Label3.Text = "Term of Payment"
        ' 
        ' dtp_TanggalPO
        ' 
        dtp_TanggalPO.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalPO.Format = DateTimePickerFormat.Custom
        dtp_TanggalPO.Location = New Point(138, 52)
        dtp_TanggalPO.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalPO.Name = "dtp_TanggalPO"
        dtp_TanggalPO.Size = New Size(110, 23)
        dtp_TanggalPO.TabIndex = 20
        ' 
        ' Label50
        ' 
        Label50.AutoSize = True
        Label50.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label50.Location = New Point(19, 57)
        Label50.Margin = New Padding(4, 0, 4, 0)
        Label50.Name = "Label50"
        Label50.Size = New Size(46, 13)
        Label50.TabIndex = 10167
        Label50.Text = "Tanggal"
        ' 
        ' txt_NomorPO
        ' 
        txt_NomorPO.Location = New Point(138, 22)
        txt_NomorPO.Margin = New Padding(4, 3, 4, 3)
        txt_NomorPO.Name = "txt_NomorPO"
        txt_NomorPO.Size = New Size(190, 23)
        txt_NomorPO.TabIndex = 10
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(19, 25)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(38, 13)
        Label1.TabIndex = 10166
        Label1.Text = "Nomor"
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Jenis_Produk_Per_Item, Nama_Produk, Deskripsi_Produk, Jumlah_Produk, Satuan_Produk, Harga_Satuan, Jumlah_Harga_Per_Item, Diskon_Per_Item_Persen, Diskon_Per_Item_Rp, Total_Harga, Kode_Project_Produk})
        DataTabelUtama.Location = New Point(21, 213)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1023, 204)
        DataTabelUtama.TabIndex = 300
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(19, 181)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(68, 13)
        Label4.TabIndex = 10169
        Label4.Text = "Kode Project"
        ' 
        ' btn_PilihMitra
        ' 
        btn_PilihMitra.Location = New Point(572, 21)
        btn_PilihMitra.Margin = New Padding(4, 3, 4, 3)
        btn_PilihMitra.Name = "btn_PilihMitra"
        btn_PilihMitra.Size = New Size(47, 27)
        btn_PilihMitra.TabIndex = 70
        btn_PilihMitra.Text = "Pilih"
        btn_PilihMitra.UseVisualStyleBackColor = True
        ' 
        ' txt_KodeCustomer
        ' 
        txt_KodeCustomer.CharacterCasing = CharacterCasing.Upper
        txt_KodeCustomer.Location = New Point(470, 22)
        txt_KodeCustomer.Margin = New Padding(4, 3, 4, 3)
        txt_KodeCustomer.MaxLength = 3
        txt_KodeCustomer.Name = "txt_KodeCustomer"
        txt_KodeCustomer.Size = New Size(94, 23)
        txt_KodeCustomer.TabIndex = 60
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label24.Location = New Point(358, 25)
        Label24.Margin = New Padding(4, 0, 4, 0)
        Label24.Name = "Label24"
        Label24.Size = New Size(79, 13)
        Label24.TabIndex = 10164
        Label24.Text = "Kode Customer"
        ' 
        ' txt_NamaCustomer
        ' 
        txt_NamaCustomer.Location = New Point(470, 52)
        txt_NamaCustomer.Margin = New Padding(4, 3, 4, 3)
        txt_NamaCustomer.Name = "txt_NamaCustomer"
        txt_NamaCustomer.Size = New Size(224, 23)
        txt_NamaCustomer.TabIndex = 80
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(358, 55)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(82, 13)
        Label2.TabIndex = 10165
        Label2.Text = "Nama Customer"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(944, 25)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(46, 13)
        Label15.TabIndex = 10182
        Label15.Text = "Kontrol :"
        Label15.Visible = False
        ' 
        ' cmb_Kontrol
        ' 
        cmb_Kontrol.FormattingEnabled = True
        cmb_Kontrol.Location = New Point(947, 46)
        cmb_Kontrol.Margin = New Padding(4, 3, 4, 3)
        cmb_Kontrol.Name = "cmb_Kontrol"
        cmb_Kontrol.Size = New Size(87, 23)
        cmb_Kontrol.TabIndex = 110
        cmb_Kontrol.Visible = False
        ' 
        ' dgv_SJBAST
        ' 
        dgv_SJBAST.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        dgv_SJBAST.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_SJBAST.Columns.AddRange(New DataGridViewColumn() {Nomor_SJ_BAST, Tanggal_SJ_BAST})
        dgv_SJBAST.Location = New Point(308, 456)
        dgv_SJBAST.Margin = New Padding(4, 3, 4, 3)
        dgv_SJBAST.Name = "dgv_SJBAST"
        dgv_SJBAST.RowHeadersVisible = False
        dgv_SJBAST.Size = New Size(197, 172)
        dgv_SJBAST.TabIndex = 10279
        ' 
        ' Nomor_SJ_BAST
        ' 
        Nomor_SJ_BAST.HeaderText = "No. SJ/BAST"
        Nomor_SJ_BAST.Name = "Nomor_SJ_BAST"
        Nomor_SJ_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_SJ_BAST.Width = 91
        ' 
        ' Tanggal_SJ_BAST
        ' 
        Tanggal_SJ_BAST.HeaderText = "Tanggal"
        Tanggal_SJ_BAST.Name = "Tanggal_SJ_BAST"
        Tanggal_SJ_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_SJ_BAST.Width = 72
        ' 
        ' lbl_SJBAST
        ' 
        lbl_SJBAST.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lbl_SJBAST.AutoSize = True
        lbl_SJBAST.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_SJBAST.Location = New Point(308, 433)
        lbl_SJBAST.Margin = New Padding(4, 0, 4, 0)
        lbl_SJBAST.Name = "lbl_SJBAST"
        lbl_SJBAST.Size = New Size(105, 13)
        lbl_SJBAST.TabIndex = 10278
        lbl_SJBAST.Text = "Surat Jalan / BAST :"
        ' 
        ' dgv_Invoice
        ' 
        dgv_Invoice.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        dgv_Invoice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_Invoice.Columns.AddRange(New DataGridViewColumn() {Nomor_Invoice, Tanggal_Invoice})
        dgv_Invoice.Location = New Point(520, 456)
        dgv_Invoice.Margin = New Padding(4, 3, 4, 3)
        dgv_Invoice.Name = "dgv_Invoice"
        dgv_Invoice.RowHeadersVisible = False
        dgv_Invoice.Size = New Size(197, 172)
        dgv_Invoice.TabIndex = 10280
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 90
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal"
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Invoice.Width = 72
        ' 
        ' lbl_Invoice
        ' 
        lbl_Invoice.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lbl_Invoice.AutoSize = True
        lbl_Invoice.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_Invoice.Location = New Point(523, 433)
        lbl_Invoice.Margin = New Padding(4, 0, 4, 0)
        lbl_Invoice.Name = "lbl_Invoice"
        lbl_Invoice.Size = New Size(48, 13)
        lbl_Invoice.TabIndex = 10281
        lbl_Invoice.Text = "Invoice :"
        ' 
        ' lbl_PerlakuanPPN
        ' 
        lbl_PerlakuanPPN.AutoSize = True
        lbl_PerlakuanPPN.Enabled = False
        lbl_PerlakuanPPN.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_PerlakuanPPN.Location = New Point(478, 156)
        lbl_PerlakuanPPN.Margin = New Padding(4, 0, 4, 0)
        lbl_PerlakuanPPN.Name = "lbl_PerlakuanPPN"
        lbl_PerlakuanPPN.Size = New Size(86, 13)
        lbl_PerlakuanPPN.TabIndex = 10283
        lbl_PerlakuanPPN.Text = "Perlakuan PPN :"
        ' 
        ' cmb_PerlakuanPPN
        ' 
        cmb_PerlakuanPPN.Enabled = False
        cmb_PerlakuanPPN.FormattingEnabled = True
        cmb_PerlakuanPPN.Location = New Point(482, 178)
        cmb_PerlakuanPPN.Margin = New Padding(4, 3, 4, 3)
        cmb_PerlakuanPPN.Name = "cmb_PerlakuanPPN"
        cmb_PerlakuanPPN.Size = New Size(112, 23)
        cmb_PerlakuanPPN.TabIndex = 10282
        ' 
        ' txt_PPhTerutang
        ' 
        txt_PPhTerutang.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_PPhTerutang.Location = New Point(937, 579)
        txt_PPhTerutang.Margin = New Padding(4, 3, 4, 3)
        txt_PPhTerutang.Name = "txt_PPhTerutang"
        txt_PPhTerutang.Size = New Size(107, 23)
        txt_PPhTerutang.TabIndex = 480
        txt_PPhTerutang.TextAlign = HorizontalAlignment.Right
        ' 
        ' cmb_JenisPPh
        ' 
        cmb_JenisPPh.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmb_JenisPPh.FormattingEnabled = True
        cmb_JenisPPh.Location = New Point(771, 579)
        cmb_JenisPPh.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisPPh.Name = "cmb_JenisPPh"
        cmb_JenisPPh.Size = New Size(94, 23)
        cmb_JenisPPh.TabIndex = 460
        ' 
        ' lbl_PersenPPh
        ' 
        lbl_PersenPPh.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lbl_PersenPPh.AutoSize = True
        lbl_PersenPPh.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_PersenPPh.Location = New Point(918, 583)
        lbl_PersenPPh.Margin = New Padding(4, 0, 4, 0)
        lbl_PersenPPh.Name = "lbl_PersenPPh"
        lbl_PersenPPh.Size = New Size(15, 13)
        lbl_PersenPPh.TabIndex = 10286
        lbl_PersenPPh.Text = "%"
        ' 
        ' txt_TarifPPh
        ' 
        txt_TarifPPh.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_TarifPPh.Location = New Point(881, 579)
        txt_TarifPPh.Margin = New Padding(4, 3, 4, 3)
        txt_TarifPPh.MaxLength = 5
        txt_TarifPPh.Name = "txt_TarifPPh"
        txt_TarifPPh.Size = New Size(38, 23)
        txt_TarifPPh.TabIndex = 470
        txt_TarifPPh.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_PPh
        ' 
        lbl_PPh.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lbl_PPh.AutoSize = True
        lbl_PPh.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_PPh.Location = New Point(733, 583)
        lbl_PPh.Margin = New Padding(4, 0, 4, 0)
        lbl_PPh.Name = "lbl_PPh"
        lbl_PPh.Size = New Size(27, 13)
        lbl_PPh.TabIndex = 10287
        lbl_PPh.Text = "PPh"
        ' 
        ' txt_TotalTagihan_Kotor
        ' 
        txt_TotalTagihan_Kotor.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_TotalTagihan_Kotor.Location = New Point(937, 549)
        txt_TotalTagihan_Kotor.Margin = New Padding(4, 3, 4, 3)
        txt_TotalTagihan_Kotor.Name = "txt_TotalTagihan_Kotor"
        txt_TotalTagihan_Kotor.Size = New Size(107, 23)
        txt_TotalTagihan_Kotor.TabIndex = 450
        txt_TotalTagihan_Kotor.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label16
        ' 
        Label16.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label16.AutoSize = True
        Label16.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label16.Location = New Point(733, 553)
        Label16.Margin = New Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(31, 13)
        Label16.TabIndex = 10289
        Label16.Text = "Total"
        ' 
        ' txt_PPhDipotong
        ' 
        txt_PPhDipotong.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_PPhDipotong.Location = New Point(937, 639)
        txt_PPhDipotong.Margin = New Padding(4, 3, 4, 3)
        txt_PPhDipotong.Name = "txt_PPhDipotong"
        txt_PPhDipotong.Size = New Size(107, 23)
        txt_PPhDipotong.TabIndex = 500
        txt_PPhDipotong.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_PPhDipotong
        ' 
        lbl_PPhDipotong.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lbl_PPhDipotong.AutoSize = True
        lbl_PPhDipotong.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_PPhDipotong.Location = New Point(733, 643)
        lbl_PPhDipotong.Margin = New Padding(4, 0, 4, 0)
        lbl_PPhDipotong.Name = "lbl_PPhDipotong"
        lbl_PPhDipotong.Size = New Size(73, 13)
        lbl_PPhDipotong.TabIndex = 10291
        lbl_PPhDipotong.Text = "PPh Dipotong"
        ' 
        ' txt_PPhDitanggung
        ' 
        txt_PPhDitanggung.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_PPhDitanggung.Location = New Point(937, 609)
        txt_PPhDitanggung.Margin = New Padding(4, 3, 4, 3)
        txt_PPhDitanggung.Name = "txt_PPhDitanggung"
        txt_PPhDitanggung.Size = New Size(107, 23)
        txt_PPhDitanggung.TabIndex = 490
        txt_PPhDitanggung.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_PPhDitanggung
        ' 
        lbl_PPhDitanggung.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lbl_PPhDitanggung.AutoSize = True
        lbl_PPhDitanggung.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_PPhDitanggung.Location = New Point(733, 613)
        lbl_PPhDitanggung.Margin = New Padding(4, 0, 4, 0)
        lbl_PPhDitanggung.Name = "lbl_PPhDitanggung"
        lbl_PPhDitanggung.Size = New Size(85, 13)
        lbl_PPhDitanggung.TabIndex = 10293
        lbl_PPhDitanggung.Text = "PPh Ditanggung"
        ' 
        ' txt_DPPBarang
        ' 
        txt_DPPBarang.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_DPPBarang.Location = New Point(572, 643)
        txt_DPPBarang.Margin = New Padding(4, 3, 4, 3)
        txt_DPPBarang.Name = "txt_DPPBarang"
        txt_DPPBarang.Size = New Size(107, 23)
        txt_DPPBarang.TabIndex = 10294
        txt_DPPBarang.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_DPPBarang
        ' 
        lbl_DPPBarang.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lbl_DPPBarang.AutoSize = True
        lbl_DPPBarang.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_DPPBarang.Location = New Point(475, 646)
        lbl_DPPBarang.Margin = New Padding(4, 0, 4, 0)
        lbl_DPPBarang.Name = "lbl_DPPBarang"
        lbl_DPPBarang.Size = New Size(66, 13)
        lbl_DPPBarang.TabIndex = 10295
        lbl_DPPBarang.Text = "DPP Barang"
        ' 
        ' txt_DPPJasa
        ' 
        txt_DPPJasa.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_DPPJasa.Location = New Point(572, 673)
        txt_DPPJasa.Margin = New Padding(4, 3, 4, 3)
        txt_DPPJasa.Name = "txt_DPPJasa"
        txt_DPPJasa.Size = New Size(107, 23)
        txt_DPPJasa.TabIndex = 10296
        txt_DPPJasa.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_DPPJasa
        ' 
        lbl_DPPJasa.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lbl_DPPJasa.AutoSize = True
        lbl_DPPJasa.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_DPPJasa.Location = New Point(475, 676)
        lbl_DPPJasa.Margin = New Padding(4, 0, 4, 0)
        lbl_DPPJasa.Name = "lbl_DPPJasa"
        lbl_DPPJasa.Size = New Size(54, 13)
        lbl_DPPJasa.TabIndex = 10297
        lbl_DPPJasa.Text = "DPP Jasa"
        ' 
        ' rdb_TanggalJangkaWaktuPenyelesaian
        ' 
        rdb_TanggalJangkaWaktuPenyelesaian.AutoSize = True
        rdb_TanggalJangkaWaktuPenyelesaian.Location = New Point(26, 673)
        rdb_TanggalJangkaWaktuPenyelesaian.Margin = New Padding(4, 3, 4, 3)
        rdb_TanggalJangkaWaktuPenyelesaian.Name = "rdb_TanggalJangkaWaktuPenyelesaian"
        rdb_TanggalJangkaWaktuPenyelesaian.Size = New Size(14, 13)
        rdb_TanggalJangkaWaktuPenyelesaian.TabIndex = 1000000008
        rdb_TanggalJangkaWaktuPenyelesaian.TabStop = True
        rdb_TanggalJangkaWaktuPenyelesaian.UseVisualStyleBackColor = True
        ' 
        ' rdb_JumlahHariJangkaWaktuPenyelesaian
        ' 
        rdb_JumlahHariJangkaWaktuPenyelesaian.AutoSize = True
        rdb_JumlahHariJangkaWaktuPenyelesaian.Location = New Point(26, 643)
        rdb_JumlahHariJangkaWaktuPenyelesaian.Margin = New Padding(4, 3, 4, 3)
        rdb_JumlahHariJangkaWaktuPenyelesaian.Name = "rdb_JumlahHariJangkaWaktuPenyelesaian"
        rdb_JumlahHariJangkaWaktuPenyelesaian.Size = New Size(14, 13)
        rdb_JumlahHariJangkaWaktuPenyelesaian.TabIndex = 1000000006
        rdb_JumlahHariJangkaWaktuPenyelesaian.TabStop = True
        rdb_JumlahHariJangkaWaktuPenyelesaian.UseVisualStyleBackColor = True
        ' 
        ' lbl_JumlahHariJangkaWaktuPenyelesaian
        ' 
        lbl_JumlahHariJangkaWaktuPenyelesaian.AutoSize = True
        lbl_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
        lbl_JumlahHariJangkaWaktuPenyelesaian.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_JumlahHariJangkaWaktuPenyelesaian.Location = New Point(105, 643)
        lbl_JumlahHariJangkaWaktuPenyelesaian.Margin = New Padding(4, 0, 4, 0)
        lbl_JumlahHariJangkaWaktuPenyelesaian.Name = "lbl_JumlahHariJangkaWaktuPenyelesaian"
        lbl_JumlahHariJangkaWaktuPenyelesaian.Size = New Size(26, 13)
        lbl_JumlahHariJangkaWaktuPenyelesaian.TabIndex = 1000000011
        lbl_JumlahHariJangkaWaktuPenyelesaian.Text = "Hari"
        ' 
        ' txt_JumlahHariJangkaWaktuPenyelesaian
        ' 
        txt_JumlahHariJangkaWaktuPenyelesaian.Enabled = False
        txt_JumlahHariJangkaWaktuPenyelesaian.Location = New Point(48, 639)
        txt_JumlahHariJangkaWaktuPenyelesaian.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahHariJangkaWaktuPenyelesaian.Name = "txt_JumlahHariJangkaWaktuPenyelesaian"
        txt_JumlahHariJangkaWaktuPenyelesaian.Size = New Size(50, 23)
        txt_JumlahHariJangkaWaktuPenyelesaian.TabIndex = 1000000007
        ' 
        ' dtp_TanggalJangkaWaktuPenyelesaian
        ' 
        dtp_TanggalJangkaWaktuPenyelesaian.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalJangkaWaktuPenyelesaian.Enabled = False
        dtp_TanggalJangkaWaktuPenyelesaian.Format = DateTimePickerFormat.Custom
        dtp_TanggalJangkaWaktuPenyelesaian.Location = New Point(48, 669)
        dtp_TanggalJangkaWaktuPenyelesaian.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalJangkaWaktuPenyelesaian.Name = "dtp_TanggalJangkaWaktuPenyelesaian"
        dtp_TanggalJangkaWaktuPenyelesaian.Size = New Size(110, 23)
        dtp_TanggalJangkaWaktuPenyelesaian.TabIndex = 1000000009
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label17.Location = New Point(23, 616)
        Label17.Margin = New Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(135, 13)
        Label17.TabIndex = 1000000010
        Label17.Text = "Jatuh Tempo Penyelesaian"
        ' 
        ' Label18
        ' 
        Label18.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label18.AutoSize = True
        Label18.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label18.Location = New Point(475, 723)
        Label18.Margin = New Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(69, 13)
        Label18.TabIndex = 1000000013
        Label18.Text = "Ongkos Kirim"
        ' 
        ' txt_BiayaTransportasiPenjualan
        ' 
        txt_BiayaTransportasiPenjualan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_BiayaTransportasiPenjualan.Location = New Point(572, 720)
        txt_BiayaTransportasiPenjualan.Margin = New Padding(4, 3, 4, 3)
        txt_BiayaTransportasiPenjualan.Name = "txt_BiayaTransportasiPenjualan"
        txt_BiayaTransportasiPenjualan.Size = New Size(107, 23)
        txt_BiayaTransportasiPenjualan.TabIndex = 1000000012
        txt_BiayaTransportasiPenjualan.TextAlign = HorizontalAlignment.Right
        ' 
        ' btn_PilihKodeProject
        ' 
        btn_PilihKodeProject.Location = New Point(281, 177)
        btn_PilihKodeProject.Margin = New Padding(4, 3, 4, 3)
        btn_PilihKodeProject.Name = "btn_PilihKodeProject"
        btn_PilihKodeProject.Size = New Size(47, 27)
        btn_PilihKodeProject.TabIndex = 1000000015
        btn_PilihKodeProject.Text = "Pilih"
        btn_PilihKodeProject.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label7.AutoSize = True
        Label7.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(918, 523)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(15, 13)
        Label7.TabIndex = 1000000029
        Label7.Text = "%"
        ' 
        ' txt_TarifPPN
        ' 
        txt_TarifPPN.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txt_TarifPPN.Location = New Point(881, 519)
        txt_TarifPPN.Margin = New Padding(4, 3, 4, 3)
        txt_TarifPPN.MaxLength = 2
        txt_TarifPPN.Name = "txt_TarifPPN"
        txt_TarifPPN.Size = New Size(38, 23)
        txt_TarifPPN.TabIndex = 1000000028
        txt_TarifPPN.TextAlign = HorizontalAlignment.Right
        ' 
        ' Nomor_Urut
        ' 
        Nomor_Urut.HeaderText = "No."
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 33
        ' 
        ' Jenis_Produk_Per_Item
        ' 
        Jenis_Produk_Per_Item.HeaderText = "Jenis Produk"
        Jenis_Produk_Per_Item.Name = "Jenis_Produk_Per_Item"
        Jenis_Produk_Per_Item.ReadOnly = True
        Jenis_Produk_Per_Item.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Produk_Per_Item.Visible = False
        ' 
        ' Nama_Produk
        ' 
        Nama_Produk.HeaderText = "Nama Barang/Jasa"
        Nama_Produk.Name = "Nama_Produk"
        Nama_Produk.ReadOnly = True
        Nama_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Produk.Width = 160
        ' 
        ' Deskripsi_Produk
        ' 
        Deskripsi_Produk.HeaderText = "Deskripsi"
        Deskripsi_Produk.Name = "Deskripsi_Produk"
        Deskripsi_Produk.ReadOnly = True
        Deskripsi_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Deskripsi_Produk.Width = 162
        ' 
        ' Jumlah_Produk
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        Jumlah_Produk.DefaultCellStyle = DataGridViewCellStyle1
        Jumlah_Produk.HeaderText = "Jumlah"
        Jumlah_Produk.Name = "Jumlah_Produk"
        Jumlah_Produk.ReadOnly = True
        Jumlah_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Produk.Width = 63
        ' 
        ' Satuan_Produk
        ' 
        Satuan_Produk.HeaderText = "Satuan"
        Satuan_Produk.Name = "Satuan_Produk"
        Satuan_Produk.ReadOnly = True
        Satuan_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Satuan_Produk.Width = 63
        ' 
        ' Harga_Satuan
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Harga_Satuan.DefaultCellStyle = DataGridViewCellStyle2
        Harga_Satuan.HeaderText = "Harga Satuan"
        Harga_Satuan.Name = "Harga_Satuan"
        Harga_Satuan.ReadOnly = True
        Harga_Satuan.SortMode = DataGridViewColumnSortMode.NotSortable
        Harga_Satuan.Width = 81
        ' 
        ' Jumlah_Harga_Per_Item
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Jumlah_Harga_Per_Item.DefaultCellStyle = DataGridViewCellStyle3
        Jumlah_Harga_Per_Item.HeaderText = "Jumlah Harga"
        Jumlah_Harga_Per_Item.Name = "Jumlah_Harga_Per_Item"
        Jumlah_Harga_Per_Item.ReadOnly = True
        Jumlah_Harga_Per_Item.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Diskon_Per_Item_Persen
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        Diskon_Per_Item_Persen.DefaultCellStyle = DataGridViewCellStyle4
        Diskon_Per_Item_Persen.HeaderText = "Diskon (%)"
        Diskon_Per_Item_Persen.Name = "Diskon_Per_Item_Persen"
        Diskon_Per_Item_Persen.ReadOnly = True
        Diskon_Per_Item_Persen.SortMode = DataGridViewColumnSortMode.NotSortable
        Diskon_Per_Item_Persen.Width = 45
        ' 
        ' Diskon_Per_Item_Rp
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Diskon_Per_Item_Rp.DefaultCellStyle = DataGridViewCellStyle5
        Diskon_Per_Item_Rp.HeaderText = "Diskon (Rp)"
        Diskon_Per_Item_Rp.Name = "Diskon_Per_Item_Rp"
        Diskon_Per_Item_Rp.ReadOnly = True
        Diskon_Per_Item_Rp.SortMode = DataGridViewColumnSortMode.NotSortable
        Diskon_Per_Item_Rp.Width = 81
        ' 
        ' Total_Harga
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Total_Harga.DefaultCellStyle = DataGridViewCellStyle6
        Total_Harga.HeaderText = "Total"
        Total_Harga.Name = "Total_Harga"
        Total_Harga.ReadOnly = True
        Total_Harga.SortMode = DataGridViewColumnSortMode.NotSortable
        Total_Harga.Width = 81
        ' 
        ' Kode_Project_Produk
        ' 
        Kode_Project_Produk.HeaderText = "Kode Project"
        Kode_Project_Produk.Name = "Kode_Project_Produk"
        Kode_Project_Produk.ReadOnly = True
        Kode_Project_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Project_Produk.Width = 99
        ' 
        ' frm_Input_POPenjualan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1064, 771)
        Controls.Add(Label7)
        Controls.Add(txt_TarifPPN)
        Controls.Add(btn_PilihKodeProject)
        Controls.Add(Label18)
        Controls.Add(txt_BiayaTransportasiPenjualan)
        Controls.Add(rdb_TanggalJangkaWaktuPenyelesaian)
        Controls.Add(rdb_JumlahHariJangkaWaktuPenyelesaian)
        Controls.Add(lbl_JumlahHariJangkaWaktuPenyelesaian)
        Controls.Add(txt_JumlahHariJangkaWaktuPenyelesaian)
        Controls.Add(dtp_TanggalJangkaWaktuPenyelesaian)
        Controls.Add(Label17)
        Controls.Add(txt_DPPJasa)
        Controls.Add(lbl_DPPJasa)
        Controls.Add(txt_DPPBarang)
        Controls.Add(lbl_DPPBarang)
        Controls.Add(txt_PPhDitanggung)
        Controls.Add(lbl_PPhDitanggung)
        Controls.Add(txt_PPhDipotong)
        Controls.Add(lbl_PPhDipotong)
        Controls.Add(Label16)
        Controls.Add(txt_TotalTagihan_Kotor)
        Controls.Add(lbl_PPh)
        Controls.Add(lbl_PersenPPh)
        Controls.Add(txt_TarifPPh)
        Controls.Add(cmb_JenisPPh)
        Controls.Add(txt_PPhTerutang)
        Controls.Add(lbl_PerlakuanPPN)
        Controls.Add(cmb_PerlakuanPPN)
        Controls.Add(lbl_Invoice)
        Controls.Add(dgv_Invoice)
        Controls.Add(dgv_SJBAST)
        Controls.Add(lbl_SJBAST)
        Controls.Add(Label15)
        Controls.Add(cmb_Kontrol)
        Controls.Add(Label14)
        Controls.Add(txt_Diskon_Persen)
        Controls.Add(lbl_JenisPPN)
        Controls.Add(cmb_JenisPPN)
        Controls.Add(txt_KeteranganToP)
        Controls.Add(txt_AlamatCustomer)
        Controls.Add(Label11)
        Controls.Add(grb_Produk)
        Controls.Add(Label12)
        Controls.Add(txt_TotalTagihan)
        Controls.Add(Label10)
        Controls.Add(txt_PPN)
        Controls.Add(Label8)
        Controls.Add(txt_DasarPengenaanPajak)
        Controls.Add(Label9)
        Controls.Add(txt_Diskon_Rp)
        Controls.Add(lbl_Diskon)
        Controls.Add(txt_JumlahHargaKeseluruhan)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(txt_Catatan)
        Controls.Add(btn_Simpan)
        Controls.Add(btn_Batal)
        Controls.Add(txt_KodeProject)
        Controls.Add(txt_TermOfPayment)
        Controls.Add(Label3)
        Controls.Add(dtp_TanggalPO)
        Controls.Add(Label50)
        Controls.Add(txt_NomorPO)
        Controls.Add(Label1)
        Controls.Add(DataTabelUtama)
        Controls.Add(Label4)
        Controls.Add(btn_PilihMitra)
        Controls.Add(txt_KodeCustomer)
        Controls.Add(Label24)
        Controls.Add(txt_NamaCustomer)
        Controls.Add(Label2)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_Input_POPenjualan"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input PO Penjualan"
        grb_Produk.ResumeLayout(False)
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        CType(dgv_SJBAST, ComponentModel.ISupportInitialize).EndInit()
        CType(dgv_Invoice, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents Label14 As Label
    Friend WithEvents txt_Diskon_Persen As TextBox
    Friend WithEvents lbl_JenisPPN As Label
    Friend WithEvents cmb_JenisPPN As ComboBox
    Friend WithEvents txt_KeteranganToP As TextBox
    Friend WithEvents txt_AlamatCustomer As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents grb_Produk As GroupBox
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents txt_TotalTagihan As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_PPN As TextBox
    Friend WithEvents Label8 As Label
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
    Friend WithEvents txt_KodeProject As TextBox
    Friend WithEvents txt_TermOfPayment As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dtp_TanggalPO As DateTimePicker
    Friend WithEvents Label50 As Label
    Friend WithEvents txt_NomorPO As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents txt_KodeCustomer As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_NamaCustomer As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cmb_Kontrol As ComboBox
    Friend WithEvents dgv_SJBAST As DataGridView
    Friend WithEvents lbl_SJBAST As Label
    Friend WithEvents dgv_Invoice As DataGridView
    Friend WithEvents lbl_Invoice As Label
    Friend WithEvents Nomor_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents lbl_PerlakuanPPN As Label
    Friend WithEvents cmb_PerlakuanPPN As ComboBox
    Friend WithEvents txt_PPhTerutang As TextBox
    Friend WithEvents cmb_JenisPPh As ComboBox
    Friend WithEvents lbl_PersenPPh As Label
    Friend WithEvents txt_TarifPPh As TextBox
    Friend WithEvents lbl_PPh As Label
    Friend WithEvents txt_TotalTagihan_Kotor As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txt_PPhDipotong As TextBox
    Friend WithEvents lbl_PPhDipotong As Label
    Friend WithEvents txt_PPhDitanggung As TextBox
    Friend WithEvents lbl_PPhDitanggung As Label
    Friend WithEvents txt_DPPBarang As TextBox
    Friend WithEvents lbl_DPPBarang As Label
    Friend WithEvents txt_DPPJasa As TextBox
    Friend WithEvents lbl_DPPJasa As Label
    Friend WithEvents rdb_TanggalJangkaWaktuPenyelesaian As RadioButton
    Friend WithEvents rdb_JumlahHariJangkaWaktuPenyelesaian As RadioButton
    Friend WithEvents lbl_JumlahHariJangkaWaktuPenyelesaian As Label
    Friend WithEvents txt_JumlahHariJangkaWaktuPenyelesaian As TextBox
    Friend WithEvents dtp_TanggalJangkaWaktuPenyelesaian As DateTimePicker
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents txt_BiayaTransportasiPenjualan As TextBox
    Friend WithEvents btn_PilihKodeProject As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_TarifPPN As TextBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk_Per_Item As DataGridViewTextBoxColumn
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
