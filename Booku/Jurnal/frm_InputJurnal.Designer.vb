<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputJurnal
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
        Me.lbl_JudulForm = New System.Windows.Forms.Label()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_K = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Debet_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kredit_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_StatusBalance = New System.Windows.Forms.Label()
        Me.lbl_NomorJV = New System.Windows.Forms.Label()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.btn_Reset = New System.Windows.Forms.Button()
        Me.btn_TambahTransaksi = New System.Windows.Forms.Button()
        Me.grb_Transaksi = New System.Windows.Forms.GroupBox()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.lbl_Referensi = New System.Windows.Forms.Label()
        Me.txt_KodeDokumen = New System.Windows.Forms.TextBox()
        Me.dtp_TanggalJurnal = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Bundelan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_NomorInvoice = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_NomorFakturPajak = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_NamaLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_UraianTransaksi = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.txt_KodeLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_TanggalInvoice = New System.Windows.Forms.TextBox()
        Me.cmb_JenisJurnal = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grb_Transaksi.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbl_JudulForm
        '
        Me.lbl_JudulForm.AutoSize = True
        Me.lbl_JudulForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JudulForm.Location = New System.Drawing.Point(25, 21)
        Me.lbl_JudulForm.Name = "lbl_JudulForm"
        Me.lbl_JudulForm.Size = New System.Drawing.Size(114, 25)
        Me.lbl_JudulForm.TabIndex = 10024
        Me.lbl_JudulForm.Text = "Input Jurnal"
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(558, 603)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.btn_Simpan.TabIndex = 999
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Kode_Akun, Me.Nama_Akun, Me.D_K, Me.Debet_, Me.Kredit_, Me.Nomor_ID})
        Me.DataTabelUtama.Location = New System.Drawing.Point(25, 301)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(617, 291)
        Me.DataTabelUtama.TabIndex = 333
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 42
        '
        'Kode_Akun
        '
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Kode_Akun.DefaultCellStyle = DataGridViewCellStyle2
        Me.Kode_Akun.HeaderText = "Kode Akun"
        Me.Kode_Akun.Name = "Kode_Akun"
        Me.Kode_Akun.ReadOnly = True
        Me.Kode_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Akun.Width = 54
        '
        'Nama_Akun
        '
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nama_Akun.DefaultCellStyle = DataGridViewCellStyle3
        Me.Nama_Akun.HeaderText = "Nama Akun"
        Me.Nama_Akun.Name = "Nama_Akun"
        Me.Nama_Akun.ReadOnly = True
        Me.Nama_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Akun.Width = 250
        '
        'D_K
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.D_K.DefaultCellStyle = DataGridViewCellStyle4
        Me.D_K.HeaderText = "D/K"
        Me.D_K.Name = "D_K"
        Me.D_K.ReadOnly = True
        Me.D_K.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.D_K.Width = 45
        '
        'Debet_
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        DataGridViewCellStyle5.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Debet_.DefaultCellStyle = DataGridViewCellStyle5
        Me.Debet_.HeaderText = "Debet"
        Me.Debet_.Name = "Debet_"
        Me.Debet_.ReadOnly = True
        Me.Debet_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Debet_.Width = 111
        '
        'Kredit_
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Kredit_.DefaultCellStyle = DataGridViewCellStyle6
        Me.Kredit_.HeaderText = "Kredit"
        Me.Kredit_.Name = "Kredit_"
        Me.Kredit_.ReadOnly = True
        Me.Kredit_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kredit_.Width = 111
        '
        'Nomor_ID
        '
        Me.Nomor_ID.HeaderText = "Nomor ID"
        Me.Nomor_ID.Name = "Nomor_ID"
        Me.Nomor_ID.ReadOnly = True
        Me.Nomor_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_ID.Visible = False
        '
        'lbl_StatusBalance
        '
        Me.lbl_StatusBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_StatusBalance.AutoSize = True
        Me.lbl_StatusBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_StatusBalance.Location = New System.Drawing.Point(25, 604)
        Me.lbl_StatusBalance.Name = "lbl_StatusBalance"
        Me.lbl_StatusBalance.Size = New System.Drawing.Size(144, 25)
        Me.lbl_StatusBalance.TabIndex = 100000033
        Me.lbl_StatusBalance.Text = "Status Balance"
        '
        'lbl_NomorJV
        '
        Me.lbl_NomorJV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_NomorJV.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorJV.Location = New System.Drawing.Point(468, 21)
        Me.lbl_NomorJV.Name = "lbl_NomorJV"
        Me.lbl_NomorJV.Size = New System.Drawing.Size(174, 27)
        Me.lbl_NomorJV.TabIndex = 100000035
        Me.lbl_NomorJV.Text = "Nomor JV"
        Me.lbl_NomorJV.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.Location = New System.Drawing.Point(468, 603)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(84, 36)
        Me.btn_Batal.TabIndex = 930
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'btn_Reset
        '
        Me.btn_Reset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Reset.Location = New System.Drawing.Point(378, 603)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(84, 36)
        Me.btn_Reset.TabIndex = 900
        Me.btn_Reset.Text = "Reset"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'btn_TambahTransaksi
        '
        Me.btn_TambahTransaksi.Location = New System.Drawing.Point(8, 19)
        Me.btn_TambahTransaksi.Name = "btn_TambahTransaksi"
        Me.btn_TambahTransaksi.Size = New System.Drawing.Size(81, 37)
        Me.btn_TambahTransaksi.TabIndex = 110
        Me.btn_TambahTransaksi.Text = "Tambah"
        Me.btn_TambahTransaksi.UseVisualStyleBackColor = True
        '
        'grb_Transaksi
        '
        Me.grb_Transaksi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grb_Transaksi.Controls.Add(Me.btn_Hapus)
        Me.grb_Transaksi.Controls.Add(Me.btn_Edit)
        Me.grb_Transaksi.Controls.Add(Me.btn_TambahTransaksi)
        Me.grb_Transaksi.Location = New System.Drawing.Point(371, 221)
        Me.grb_Transaksi.Name = "grb_Transaksi"
        Me.grb_Transaksi.Size = New System.Drawing.Size(271, 69)
        Me.grb_Transaksi.TabIndex = 100
        Me.grb_Transaksi.TabStop = False
        Me.grb_Transaksi.Text = "Baris Transaksi :"
        '
        'btn_Hapus
        '
        Me.btn_Hapus.Enabled = False
        Me.btn_Hapus.Location = New System.Drawing.Point(182, 19)
        Me.btn_Hapus.Name = "btn_Hapus"
        Me.btn_Hapus.Size = New System.Drawing.Size(81, 37)
        Me.btn_Hapus.TabIndex = 130
        Me.btn_Hapus.Text = "Hapus"
        Me.btn_Hapus.UseVisualStyleBackColor = True
        '
        'btn_Edit
        '
        Me.btn_Edit.Enabled = False
        Me.btn_Edit.Location = New System.Drawing.Point(95, 19)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(81, 37)
        Me.btn_Edit.TabIndex = 120
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'lbl_Referensi
        '
        Me.lbl_Referensi.AutoSize = True
        Me.lbl_Referensi.ForeColor = System.Drawing.Color.Black
        Me.lbl_Referensi.Location = New System.Drawing.Point(31, 117)
        Me.lbl_Referensi.Name = "lbl_Referensi"
        Me.lbl_Referensi.Size = New System.Drawing.Size(81, 13)
        Me.lbl_Referensi.TabIndex = 100000039
        Me.lbl_Referensi.Text = "Kode Dokumen"
        '
        'txt_KodeDokumen
        '
        Me.txt_KodeDokumen.Location = New System.Drawing.Point(129, 114)
        Me.txt_KodeDokumen.Name = "txt_KodeDokumen"
        Me.txt_KodeDokumen.Size = New System.Drawing.Size(163, 20)
        Me.txt_KodeDokumen.TabIndex = 20
        '
        'dtp_TanggalJurnal
        '
        Me.dtp_TanggalJurnal.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalJurnal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalJurnal.Location = New System.Drawing.Point(129, 58)
        Me.dtp_TanggalJurnal.Name = "dtp_TanggalJurnal"
        Me.dtp_TanggalJurnal.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalJurnal.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(31, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 100000042
        Me.Label2.Text = "Tanggal Jurnal"
        '
        'txt_Bundelan
        '
        Me.txt_Bundelan.Location = New System.Drawing.Point(129, 140)
        Me.txt_Bundelan.Name = "txt_Bundelan"
        Me.txt_Bundelan.Size = New System.Drawing.Size(163, 20)
        Me.txt_Bundelan.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(31, 143)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 100000045
        Me.Label3.Text = "Bundelan"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(31, 169)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 100000048
        Me.Label4.Text = "Tanggal Invoice"
        '
        'txt_NomorInvoice
        '
        Me.txt_NomorInvoice.Location = New System.Drawing.Point(129, 192)
        Me.txt_NomorInvoice.Name = "txt_NomorInvoice"
        Me.txt_NomorInvoice.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorInvoice.TabIndex = 50
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(31, 195)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 100000049
        Me.Label5.Text = "Nomor Invoice"
        '
        'txt_NomorFakturPajak
        '
        Me.txt_NomorFakturPajak.Location = New System.Drawing.Point(129, 218)
        Me.txt_NomorFakturPajak.Name = "txt_NomorFakturPajak"
        Me.txt_NomorFakturPajak.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorFakturPajak.TabIndex = 60
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(31, 221)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 100000051
        Me.Label6.Text = "No. Faktur Pajak"
        '
        'txt_NamaLawanTransaksi
        '
        Me.txt_NamaLawanTransaksi.Location = New System.Drawing.Point(156, 270)
        Me.txt_NamaLawanTransaksi.Name = "txt_NamaLawanTransaksi"
        Me.txt_NamaLawanTransaksi.Size = New System.Drawing.Size(136, 20)
        Me.txt_NamaLawanTransaksi.TabIndex = 80
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 273)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 13)
        Me.Label7.TabIndex = 100000053
        Me.Label7.Text = "Nama Lawan Transaksi"
        '
        'txt_UraianTransaksi
        '
        Me.txt_UraianTransaksi.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_UraianTransaksi.Location = New System.Drawing.Point(317, 85)
        Me.txt_UraianTransaksi.Multiline = True
        Me.txt_UraianTransaksi.Name = "txt_UraianTransaksi"
        Me.txt_UraianTransaksi.Size = New System.Drawing.Size(325, 101)
        Me.txt_UraianTransaksi.TabIndex = 90
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(314, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 13)
        Me.Label8.TabIndex = 100000055
        Me.Label8.Text = "Uraian Transaksi :"
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(252, 242)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 75
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'txt_KodeLawanTransaksi
        '
        Me.txt_KodeLawanTransaksi.Location = New System.Drawing.Point(156, 244)
        Me.txt_KodeLawanTransaksi.Name = "txt_KodeLawanTransaksi"
        Me.txt_KodeLawanTransaksi.Size = New System.Drawing.Size(90, 20)
        Me.txt_KodeLawanTransaksi.TabIndex = 70
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 247)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 13)
        Me.Label1.TabIndex = 100000062
        Me.Label1.Text = "Kode Lawan Transaksi"
        '
        'txt_TanggalInvoice
        '
        Me.txt_TanggalInvoice.Location = New System.Drawing.Point(129, 166)
        Me.txt_TanggalInvoice.Name = "txt_TanggalInvoice"
        Me.txt_TanggalInvoice.Size = New System.Drawing.Size(163, 20)
        Me.txt_TanggalInvoice.TabIndex = 40
        '
        'cmb_JenisJurnal
        '
        Me.cmb_JenisJurnal.FormattingEnabled = True
        Me.cmb_JenisJurnal.Location = New System.Drawing.Point(129, 87)
        Me.cmb_JenisJurnal.Name = "cmb_JenisJurnal"
        Me.cmb_JenisJurnal.Size = New System.Drawing.Size(163, 21)
        Me.cmb_JenisJurnal.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(31, 90)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 13)
        Me.Label10.TabIndex = 100000064
        Me.Label10.Text = "Jenis Jurnal"
        '
        'frm_InputJurnal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 675)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmb_JenisJurnal)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_TanggalInvoice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_KodeLawanTransaksi)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_UraianTransaksi)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_NamaLawanTransaksi)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_NomorFakturPajak)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_NomorInvoice)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_Bundelan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtp_TanggalJurnal)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_KodeDokumen)
        Me.Controls.Add(Me.lbl_Referensi)
        Me.Controls.Add(Me.grb_Transaksi)
        Me.Controls.Add(Me.btn_Reset)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.lbl_NomorJV)
        Me.Controls.Add(Me.lbl_StatusBalance)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.lbl_JudulForm)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputJurnal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Input Jurnal"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grb_Transaksi.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_StatusBalance As System.Windows.Forms.Label
    Friend WithEvents lbl_NomorJV As System.Windows.Forms.Label
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents btn_TambahTransaksi As System.Windows.Forms.Button
    Friend WithEvents grb_Transaksi As System.Windows.Forms.GroupBox
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents lbl_Referensi As System.Windows.Forms.Label
    Friend WithEvents txt_KodeDokumen As System.Windows.Forms.TextBox
    Friend WithEvents dtp_TanggalJurnal As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_Bundelan As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_NomorInvoice As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_NomorFakturPajak As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_NamaLawanTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_UraianTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btn_PilihMitra As System.Windows.Forms.Button
    Friend WithEvents txt_KodeLawanTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_TanggalInvoice As System.Windows.Forms.TextBox
    Friend WithEvents cmb_JenisJurnal As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents D_K As DataGridViewTextBoxColumn
    Friend WithEvents Debet_ As DataGridViewTextBoxColumn
    Friend WithEvents Kredit_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
End Class
