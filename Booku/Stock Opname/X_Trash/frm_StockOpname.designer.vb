<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_StockOpname
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
        btn_Tambah = New Button()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Jenis_Stok = New DataGridViewTextBoxColumn()
        Tanggal_Pengecekan = New DataGridViewTextBoxColumn()
        Nama_Barang = New DataGridViewTextBoxColumn()
        Jumlah_Barang = New DataGridViewTextBoxColumn()
        Satuan_ = New DataGridViewTextBoxColumn()
        Harga_Satuan = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Nama_Supplier = New DataGridViewTextBoxColumn()
        Jumlah_Harga = New DataGridViewTextBoxColumn()
        Asal_ = New DataGridViewTextBoxColumn()
        Lokasi_ = New DataGridViewTextBoxColumn()
        Kode_Project = New DataGridViewTextBoxColumn()
        Kode_Akun = New DataGridViewTextBoxColumn()
        Nama_Akun = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        Jenis_Data = New DataGridViewTextBoxColumn()
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        btn_Jurnal = New Button()
        cmb_Bulan = New ComboBox()
        Label1 = New Label()
        pnl_CRUD = New Panel()
        lbl_bantuan = New LinkLabel()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        pnl_CRUD.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Tambah.Location = New Point(0, 0)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 351
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Hapus.Location = New Point(208, 0)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 352
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Edit.Location = New Point(104, 0)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 353
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Jenis_Stok, Tanggal_Pengecekan, Nama_Barang, Jumlah_Barang, Satuan_, Harga_Satuan, Nomor_Invoice, Tanggal_Invoice, Nomor_Faktur_Pajak, Nama_Supplier, Jumlah_Harga, Asal_, Lokasi_, Kode_Project, Kode_Akun, Nama_Akun, Keterangan_, Jenis_Data})
        DataTabelUtama.Location = New Point(14, 148)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 543)
        DataTabelUtama.TabIndex = 10018
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
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_ID.DefaultCellStyle = DataGridViewCellStyle2
        Nomor_ID.HeaderText = "Nomor ID"
        Nomor_ID.Name = "Nomor_ID"
        Nomor_ID.ReadOnly = True
        Nomor_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID.Visible = False
        Nomor_ID.Width = 45
        ' 
        ' Jenis_Stok
        ' 
        Jenis_Stok.HeaderText = "Jenis Stok"
        Jenis_Stok.Name = "Jenis_Stok"
        Jenis_Stok.ReadOnly = True
        Jenis_Stok.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Stok.Visible = False
        Jenis_Stok.Width = 120
        ' 
        ' Tanggal_Pengecekan
        ' 
        Tanggal_Pengecekan.HeaderText = "Tanggal Pengecekan"
        Tanggal_Pengecekan.Name = "Tanggal_Pengecekan"
        Tanggal_Pengecekan.ReadOnly = True
        Tanggal_Pengecekan.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Pengecekan.Visible = False
        Tanggal_Pengecekan.Width = 63
        ' 
        ' Nama_Barang
        ' 
        Nama_Barang.HeaderText = "Nama Barang"
        Nama_Barang.Name = "Nama_Barang"
        Nama_Barang.ReadOnly = True
        Nama_Barang.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Barang.Width = 210
        ' 
        ' Jumlah_Barang
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Jumlah_Barang.DefaultCellStyle = DataGridViewCellStyle3
        Jumlah_Barang.HeaderText = "Jumlah"
        Jumlah_Barang.Name = "Jumlah_Barang"
        Jumlah_Barang.ReadOnly = True
        Jumlah_Barang.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Barang.Width = 45
        ' 
        ' Satuan_
        ' 
        Satuan_.HeaderText = "Satuan"
        Satuan_.Name = "Satuan_"
        Satuan_.ReadOnly = True
        Satuan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Satuan_.Width = 72
        ' 
        ' Harga_Satuan
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Harga_Satuan.DefaultCellStyle = DataGridViewCellStyle4
        Harga_Satuan.HeaderText = "Harga Satuan"
        Harga_Satuan.Name = "Harga_Satuan"
        Harga_Satuan.ReadOnly = True
        Harga_Satuan.SortMode = DataGridViewColumnSortMode.NotSortable
        Harga_Satuan.Width = 81
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.ReadOnly = True
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 99
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.ReadOnly = True
        Tanggal_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Invoice.Width = 63
        ' 
        ' Nomor_Faktur_Pajak
        ' 
        Nomor_Faktur_Pajak.HeaderText = "Nomor Faktur Pajak"
        Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Nomor_Faktur_Pajak.ReadOnly = True
        Nomor_Faktur_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Faktur_Pajak.Width = 99
        ' 
        ' Nama_Supplier
        ' 
        Nama_Supplier.HeaderText = "Nama Supplier"
        Nama_Supplier.Name = "Nama_Supplier"
        Nama_Supplier.ReadOnly = True
        Nama_Supplier.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Supplier.Width = 132
        ' 
        ' Jumlah_Harga
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Jumlah_Harga.DefaultCellStyle = DataGridViewCellStyle5
        Jumlah_Harga.HeaderText = "Jumlah Harga"
        Jumlah_Harga.Name = "Jumlah_Harga"
        Jumlah_Harga.ReadOnly = True
        Jumlah_Harga.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Harga.Width = 81
        ' 
        ' Asal_
        ' 
        Asal_.HeaderText = "Asal"
        Asal_.Name = "Asal_"
        Asal_.ReadOnly = True
        Asal_.SortMode = DataGridViewColumnSortMode.NotSortable
        Asal_.Width = 63
        ' 
        ' Lokasi_
        ' 
        Lokasi_.HeaderText = "Lokasi"
        Lokasi_.Name = "Lokasi_"
        Lokasi_.ReadOnly = True
        Lokasi_.SortMode = DataGridViewColumnSortMode.NotSortable
        Lokasi_.Width = 150
        ' 
        ' Kode_Project
        ' 
        Kode_Project.HeaderText = "Kode Project"
        Kode_Project.Name = "Kode_Project"
        Kode_Project.ReadOnly = True
        Kode_Project.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Project.Width = 99
        ' 
        ' Kode_Akun
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        Kode_Akun.DefaultCellStyle = DataGridViewCellStyle6
        Kode_Akun.HeaderText = "Kode Akun"
        Kode_Akun.Name = "Kode_Akun"
        Kode_Akun.ReadOnly = True
        Kode_Akun.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Akun.Width = 45
        ' 
        ' Nama_Akun
        ' 
        Nama_Akun.HeaderText = "Nama Akun"
        Nama_Akun.Name = "Nama_Akun"
        Nama_Akun.ReadOnly = True
        Nama_Akun.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Akun.Width = 210
        ' 
        ' Keterangan_
        ' 
        Keterangan_.HeaderText = "Keterangan"
        Keterangan_.Name = "Keterangan_"
        Keterangan_.ReadOnly = True
        Keterangan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_.Width = 390
        ' 
        ' Jenis_Data
        ' 
        Jenis_Data.HeaderText = "Jenis Data"
        Jenis_Data.Name = "Jenis_Data"
        Jenis_Data.ReadOnly = True
        Jenis_Data.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Data.Visible = False
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 66)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 14)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(213, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Stock Opname"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Jurnal
        ' 
        btn_Jurnal.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Jurnal.Location = New Point(610, 72)
        btn_Jurnal.Margin = New Padding(4, 3, 4, 3)
        btn_Jurnal.Name = "btn_Jurnal"
        btn_Jurnal.Size = New Size(141, 58)
        btn_Jurnal.TabIndex = 10192
        btn_Jurnal.Text = "Jurnal"
        btn_Jurnal.UseVisualStyleBackColor = True
        ' 
        ' cmb_Bulan
        ' 
        cmb_Bulan.FormattingEnabled = True
        cmb_Bulan.Location = New Point(187, 108)
        cmb_Bulan.Margin = New Padding(4, 3, 4, 3)
        cmb_Bulan.Name = "cmb_Bulan"
        cmb_Bulan.Size = New Size(96, 23)
        cmb_Bulan.TabIndex = 10193
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(133, 114)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(43, 15)
        Label1.TabIndex = 10194
        Label1.Text = "Bulan :"
        ' 
        ' pnl_CRUD
        ' 
        pnl_CRUD.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnl_CRUD.Controls.Add(btn_Tambah)
        pnl_CRUD.Controls.Add(btn_Hapus)
        pnl_CRUD.Controls.Add(btn_Edit)
        pnl_CRUD.Location = New Point(1203, 89)
        pnl_CRUD.Margin = New Padding(4, 3, 4, 3)
        pnl_CRUD.Name = "pnl_CRUD"
        pnl_CRUD.Size = New Size(304, 40)
        pnl_CRUD.TabIndex = 10195
        ' 
        ' lbl_bantuan
        ' 
        lbl_bantuan.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lbl_bantuan.AutoSize = True
        lbl_bantuan.Location = New Point(1452, 10)
        lbl_bantuan.Margin = New Padding(4, 0, 4, 0)
        lbl_bantuan.Name = "lbl_bantuan"
        lbl_bantuan.Size = New Size(51, 15)
        lbl_bantuan.TabIndex = 10197
        lbl_bantuan.TabStop = True
        lbl_bantuan.Text = "Bantuan"
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(866, 75)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10198
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_StockOpname
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Export)
        Controls.Add(lbl_bantuan)
        Controls.Add(pnl_CRUD)
        Controls.Add(Label1)
        Controls.Add(cmb_Bulan)
        Controls.Add(btn_Jurnal)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_StockOpname"
        StartPosition = FormStartPosition.CenterParent
        Text = "Stock Opname"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        pnl_CRUD.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents btn_Jurnal As Button
    Friend WithEvents cmb_Bulan As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents pnl_CRUD As Panel
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Stok As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Pengecekan As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Barang As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Barang As DataGridViewTextBoxColumn
    Friend WithEvents Satuan_ As DataGridViewTextBoxColumn
    Friend WithEvents Harga_Satuan As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Supplier As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Harga As DataGridViewTextBoxColumn
    Friend WithEvents Asal_ As DataGridViewTextBoxColumn
    Friend WithEvents Lokasi_ As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Data As DataGridViewTextBoxColumn
    Friend WithEvents lbl_bantuan As LinkLabel
    Friend WithEvents btn_Export As Button
End Class
