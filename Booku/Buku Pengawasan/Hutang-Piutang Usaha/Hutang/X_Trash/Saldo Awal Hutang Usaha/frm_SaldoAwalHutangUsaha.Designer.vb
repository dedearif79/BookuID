<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_SaldoAwalHutangUsaha
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
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Pembelian = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referensi_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Faktur_Pajak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Faktur_Pajak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Barang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Jasa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DPP_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PPN_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PPh_Dipotong = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Tagihan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Bayar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sisa_Hutang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Due_Date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Keterangan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_JudulForm = New System.Windows.Forms.Label()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Tambah = New System.Windows.Forms.Button()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.txt_Pencarian = New System.Windows.Forms.TextBox()
        Me.lbl_Pencarian = New System.Windows.Forms.Label()
        Me.lbl_TotalSaldo = New System.Windows.Forms.Label()
        Me.btn_Sesuaikan = New System.Windows.Forms.Button()
        Me.grb_1 = New System.Windows.Forms.GroupBox()
        Me.lbl_SaldoCOA_PlusAJP = New System.Windows.Forms.Label()
        Me.txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian = New System.Windows.Forms.TextBox()
        Me.txt_TotalSaldoAwalHutangUsaha = New System.Windows.Forms.TextBox()
        Me.txt_SaldoAwalCOAHutangUsaha = New System.Windows.Forms.TextBox()
        Me.lbl_SaldoAwalCOA = New System.Windows.Forms.Label()
        Me.txt_JumlahPenyesuaianHutangUsaha = New System.Windows.Forms.TextBox()
        Me.lbl_AJP = New System.Windows.Forms.Label()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grb_1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToResizeRows = False
        Me.DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Nomor_Pembelian, Me.Referensi_, Me.Tanggal_Invoice, Me.Nomor_Invoice, Me.Tanggal_Faktur_Pajak, Me.Nomor_Faktur_Pajak, Me.Kode_Supplier, Me.Nama_Supplier, Me.Nama_Barang, Me.Nama_Jasa, Me.DPP_, Me.PPN_, Me.PPh_Dipotong, Me.Jumlah_Tagihan, Me.Jumlah_Bayar, Me.Sisa_Hutang, Me.Due_Date, Me.Keterangan_})
        Me.DataGridView.Location = New System.Drawing.Point(12, 120)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(1204, 450)
        Me.DataGridView.TabIndex = 10020
        '
        'Nomor_Urut
        '
        Me.Nomor_Urut.Frozen = True
        Me.Nomor_Urut.HeaderText = "Nomor Urut"
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 45
        '
        'Nomor_Pembelian
        '
        Me.Nomor_Pembelian.Frozen = True
        Me.Nomor_Pembelian.HeaderText = "Nomor Pembelian"
        Me.Nomor_Pembelian.Name = "Nomor_Pembelian"
        Me.Nomor_Pembelian.ReadOnly = True
        Me.Nomor_Pembelian.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Referensi_
        '
        Me.Referensi_.Frozen = True
        Me.Referensi_.HeaderText = "Referensi"
        Me.Referensi_.Name = "Referensi_"
        Me.Referensi_.ReadOnly = True
        Me.Referensi_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Tanggal_Invoice
        '
        Me.Tanggal_Invoice.Frozen = True
        Me.Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Me.Tanggal_Invoice.Name = "Tanggal_Invoice"
        Me.Tanggal_Invoice.ReadOnly = True
        Me.Tanggal_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Invoice.Width = 72
        '
        'Nomor_Invoice
        '
        Me.Nomor_Invoice.HeaderText = "Nomor Invoice"
        Me.Nomor_Invoice.Name = "Nomor_Invoice"
        Me.Nomor_Invoice.ReadOnly = True
        Me.Nomor_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Tanggal_Faktur_Pajak
        '
        Me.Tanggal_Faktur_Pajak.HeaderText = "Tanggal Faktur Pajak"
        Me.Tanggal_Faktur_Pajak.Name = "Tanggal_Faktur_Pajak"
        Me.Tanggal_Faktur_Pajak.ReadOnly = True
        Me.Tanggal_Faktur_Pajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Faktur_Pajak.Width = 72
        '
        'Nomor_Faktur_Pajak
        '
        Me.Nomor_Faktur_Pajak.HeaderText = "Nomor Faktur Pajak"
        Me.Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Me.Nomor_Faktur_Pajak.ReadOnly = True
        Me.Nomor_Faktur_Pajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Kode_Supplier
        '
        Me.Kode_Supplier.HeaderText = "Kode Supplier"
        Me.Kode_Supplier.Name = "Kode_Supplier"
        Me.Kode_Supplier.ReadOnly = True
        Me.Kode_Supplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Supplier.Width = 72
        '
        'Nama_Supplier
        '
        Me.Nama_Supplier.HeaderText = "Nama Supplier"
        Me.Nama_Supplier.Name = "Nama_Supplier"
        Me.Nama_Supplier.ReadOnly = True
        Me.Nama_Supplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Supplier.Width = 123
        '
        'Nama_Barang
        '
        Me.Nama_Barang.HeaderText = "Nama Barang"
        Me.Nama_Barang.Name = "Nama_Barang"
        Me.Nama_Barang.ReadOnly = True
        Me.Nama_Barang.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Nama_Jasa
        '
        Me.Nama_Jasa.HeaderText = "Nama Jasa"
        Me.Nama_Jasa.Name = "Nama_Jasa"
        Me.Nama_Jasa.ReadOnly = True
        Me.Nama_Jasa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DPP_
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.DPP_.DefaultCellStyle = DataGridViewCellStyle1
        Me.DPP_.HeaderText = "DPP"
        Me.DPP_.Name = "DPP_"
        Me.DPP_.ReadOnly = True
        Me.DPP_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PPN_
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.PPN_.DefaultCellStyle = DataGridViewCellStyle2
        Me.PPN_.HeaderText = "PPN"
        Me.PPN_.Name = "PPN_"
        Me.PPN_.ReadOnly = True
        Me.PPN_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PPh_Dipotong
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle3
        Me.PPh_Dipotong.HeaderText = "PPh Dipotong"
        Me.PPh_Dipotong.Name = "PPh_Dipotong"
        Me.PPh_Dipotong.ReadOnly = True
        Me.PPh_Dipotong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Jumlah_Tagihan
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Jumlah_Tagihan.DefaultCellStyle = DataGridViewCellStyle4
        Me.Jumlah_Tagihan.HeaderText = "Jumlah Tagihan"
        Me.Jumlah_Tagihan.Name = "Jumlah_Tagihan"
        Me.Jumlah_Tagihan.ReadOnly = True
        Me.Jumlah_Tagihan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Jumlah_Bayar
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Jumlah_Bayar.DefaultCellStyle = DataGridViewCellStyle5
        Me.Jumlah_Bayar.HeaderText = "Jumlah Bayar"
        Me.Jumlah_Bayar.Name = "Jumlah_Bayar"
        Me.Jumlah_Bayar.ReadOnly = True
        Me.Jumlah_Bayar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Sisa_Hutang
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.Sisa_Hutang.DefaultCellStyle = DataGridViewCellStyle6
        Me.Sisa_Hutang.HeaderText = "Sisa Hutang"
        Me.Sisa_Hutang.Name = "Sisa_Hutang"
        Me.Sisa_Hutang.ReadOnly = True
        Me.Sisa_Hutang.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Due_Date
        '
        Me.Due_Date.HeaderText = "Due Date"
        Me.Due_Date.Name = "Due_Date"
        Me.Due_Date.ReadOnly = True
        Me.Due_Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Due_Date.Width = 72
        '
        'Keterangan_
        '
        Me.Keterangan_.HeaderText = "Keterangan"
        Me.Keterangan_.Name = "Keterangan_"
        Me.Keterangan_.ReadOnly = True
        Me.Keterangan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Keterangan_.Width = 162
        '
        'lbl_JudulForm
        '
        Me.lbl_JudulForm.AutoSize = True
        Me.lbl_JudulForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JudulForm.Location = New System.Drawing.Point(10, 10)
        Me.lbl_JudulForm.Name = "lbl_JudulForm"
        Me.lbl_JudulForm.Size = New System.Drawing.Size(369, 32)
        Me.lbl_JudulForm.TabIndex = 10031
        Me.lbl_JudulForm.Text = "Saldo Awal Hutang Usaha"
        Me.lbl_JudulForm.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn_Edit
        '
        Me.btn_Edit.Location = New System.Drawing.Point(1044, 79)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(83, 35)
        Me.btn_Edit.TabIndex = 10036
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'btn_Hapus
        '
        Me.btn_Hapus.Location = New System.Drawing.Point(1133, 79)
        Me.btn_Hapus.Name = "btn_Hapus"
        Me.btn_Hapus.Size = New System.Drawing.Size(83, 35)
        Me.btn_Hapus.TabIndex = 10035
        Me.btn_Hapus.Text = "Hapus"
        Me.btn_Hapus.UseVisualStyleBackColor = True
        '
        'btn_Tambah
        '
        Me.btn_Tambah.Location = New System.Drawing.Point(955, 79)
        Me.btn_Tambah.Name = "btn_Tambah"
        Me.btn_Tambah.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tambah.TabIndex = 10034
        Me.btn_Tambah.Text = "Tambah"
        Me.btn_Tambah.UseVisualStyleBackColor = True
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Location = New System.Drawing.Point(12, 49)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(81, 65)
        Me.btn_Refresh.TabIndex = 10038
        Me.btn_Refresh.Text = "Refresh"
        Me.btn_Refresh.UseVisualStyleBackColor = True
        '
        'txt_Pencarian
        '
        Me.txt_Pencarian.Location = New System.Drawing.Point(214, 87)
        Me.txt_Pencarian.Name = "txt_Pencarian"
        Me.txt_Pencarian.Size = New System.Drawing.Size(174, 20)
        Me.txt_Pencarian.TabIndex = 10032
        '
        'lbl_Pencarian
        '
        Me.lbl_Pencarian.AutoSize = True
        Me.lbl_Pencarian.Location = New System.Drawing.Point(149, 90)
        Me.lbl_Pencarian.Name = "lbl_Pencarian"
        Me.lbl_Pencarian.Size = New System.Drawing.Size(61, 13)
        Me.lbl_Pencarian.TabIndex = 10033
        Me.lbl_Pencarian.Text = "Pencarian :"
        '
        'lbl_TotalSaldo
        '
        Me.lbl_TotalSaldo.AutoSize = True
        Me.lbl_TotalSaldo.Location = New System.Drawing.Point(10, 22)
        Me.lbl_TotalSaldo.Name = "lbl_TotalSaldo"
        Me.lbl_TotalSaldo.Size = New System.Drawing.Size(159, 13)
        Me.lbl_TotalSaldo.TabIndex = 10039
        Me.lbl_TotalSaldo.Text = "Total Saldo Awal Hutang Usaha"
        '
        'btn_Sesuaikan
        '
        Me.btn_Sesuaikan.Location = New System.Drawing.Point(329, 17)
        Me.btn_Sesuaikan.Name = "btn_Sesuaikan"
        Me.btn_Sesuaikan.Size = New System.Drawing.Size(81, 50)
        Me.btn_Sesuaikan.TabIndex = 10040
        Me.btn_Sesuaikan.Text = "Sesuaikan"
        Me.btn_Sesuaikan.UseVisualStyleBackColor = True
        '
        'grb_1
        '
        Me.grb_1.Controls.Add(Me.lbl_SaldoCOA_PlusAJP)
        Me.grb_1.Controls.Add(Me.btn_Sesuaikan)
        Me.grb_1.Controls.Add(Me.txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian)
        Me.grb_1.Controls.Add(Me.txt_TotalSaldoAwalHutangUsaha)
        Me.grb_1.Controls.Add(Me.lbl_TotalSaldo)
        Me.grb_1.Location = New System.Drawing.Point(466, 37)
        Me.grb_1.Name = "grb_1"
        Me.grb_1.Size = New System.Drawing.Size(420, 77)
        Me.grb_1.TabIndex = 10041
        Me.grb_1.TabStop = False
        '
        'lbl_SaldoCOA_PlusAJP
        '
        Me.lbl_SaldoCOA_PlusAJP.AutoSize = True
        Me.lbl_SaldoCOA_PlusAJP.Location = New System.Drawing.Point(12, 48)
        Me.lbl_SaldoCOA_PlusAJP.Name = "lbl_SaldoCOA_PlusAJP"
        Me.lbl_SaldoCOA_PlusAJP.Size = New System.Drawing.Size(157, 13)
        Me.lbl_SaldoCOA_PlusAJP.TabIndex = 10042
        Me.lbl_SaldoCOA_PlusAJP.Text = "Saldo Awal COA Hutang Usaha"
        '
        'txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian
        '
        Me.txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Location = New System.Drawing.Point(212, 45)
        Me.txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Name = "txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian"
        Me.txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Size = New System.Drawing.Size(108, 20)
        Me.txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.TabIndex = 10043
        Me.txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_TotalSaldoAwalHutangUsaha
        '
        Me.txt_TotalSaldoAwalHutangUsaha.Location = New System.Drawing.Point(212, 19)
        Me.txt_TotalSaldoAwalHutangUsaha.Name = "txt_TotalSaldoAwalHutangUsaha"
        Me.txt_TotalSaldoAwalHutangUsaha.Size = New System.Drawing.Size(108, 20)
        Me.txt_TotalSaldoAwalHutangUsaha.TabIndex = 10040
        Me.txt_TotalSaldoAwalHutangUsaha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SaldoAwalCOAHutangUsaha
        '
        Me.txt_SaldoAwalCOAHutangUsaha.Location = New System.Drawing.Point(180, 579)
        Me.txt_SaldoAwalCOAHutangUsaha.Name = "txt_SaldoAwalCOAHutangUsaha"
        Me.txt_SaldoAwalCOAHutangUsaha.Size = New System.Drawing.Size(108, 20)
        Me.txt_SaldoAwalCOAHutangUsaha.TabIndex = 10041
        Me.txt_SaldoAwalCOAHutangUsaha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_SaldoAwalCOA
        '
        Me.lbl_SaldoAwalCOA.AutoSize = True
        Me.lbl_SaldoAwalCOA.Location = New System.Drawing.Point(13, 582)
        Me.lbl_SaldoAwalCOA.Name = "lbl_SaldoAwalCOA"
        Me.lbl_SaldoAwalCOA.Size = New System.Drawing.Size(163, 13)
        Me.lbl_SaldoAwalCOA.TabIndex = 10043
        Me.lbl_SaldoAwalCOA.Text = "Saldo Awal COA Hutang Usaha :"
        '
        'txt_JumlahPenyesuaianHutangUsaha
        '
        Me.txt_JumlahPenyesuaianHutangUsaha.Location = New System.Drawing.Point(356, 579)
        Me.txt_JumlahPenyesuaianHutangUsaha.Name = "txt_JumlahPenyesuaianHutangUsaha"
        Me.txt_JumlahPenyesuaianHutangUsaha.Size = New System.Drawing.Size(108, 20)
        Me.txt_JumlahPenyesuaianHutangUsaha.TabIndex = 10044
        Me.txt_JumlahPenyesuaianHutangUsaha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_AJP
        '
        Me.lbl_AJP.AutoSize = True
        Me.lbl_AJP.Location = New System.Drawing.Point(314, 582)
        Me.lbl_AJP.Name = "lbl_AJP"
        Me.lbl_AJP.Size = New System.Drawing.Size(32, 13)
        Me.lbl_AJP.TabIndex = 10045
        Me.lbl_AJP.Text = "AJP :"
        '
        'frm_SaldoAwalHutangUsaha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1228, 636)
        Me.Controls.Add(Me.lbl_AJP)
        Me.Controls.Add(Me.txt_JumlahPenyesuaianHutangUsaha)
        Me.Controls.Add(Me.txt_SaldoAwalCOAHutangUsaha)
        Me.Controls.Add(Me.lbl_SaldoAwalCOA)
        Me.Controls.Add(Me.grb_1)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.btn_Edit)
        Me.Controls.Add(Me.btn_Hapus)
        Me.Controls.Add(Me.btn_Tambah)
        Me.Controls.Add(Me.lbl_Pencarian)
        Me.Controls.Add(Me.txt_Pencarian)
        Me.Controls.Add(Me.lbl_JudulForm)
        Me.Controls.Add(Me.DataGridView)
        Me.Name = "frm_SaldoAwalHutangUsaha"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Saldo Awal Hutang Usaha"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grb_1.ResumeLayout(False)
        Me.grb_1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents txt_Pencarian As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Pencarian As System.Windows.Forms.Label
    Friend WithEvents lbl_TotalSaldo As System.Windows.Forms.Label
    Friend WithEvents btn_Sesuaikan As System.Windows.Forms.Button
    Friend WithEvents grb_1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_SaldoCOA_PlusAJP As System.Windows.Forms.Label
    Friend WithEvents txt_SaldoAwalCOAHutangUsaha As System.Windows.Forms.TextBox
    Friend WithEvents txt_TotalSaldoAwalHutangUsaha As System.Windows.Forms.TextBox
    Friend WithEvents txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian As System.Windows.Forms.TextBox
    Friend WithEvents lbl_SaldoAwalCOA As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahPenyesuaianHutangUsaha As System.Windows.Forms.TextBox
    Friend WithEvents lbl_AJP As System.Windows.Forms.Label
    Friend WithEvents Nomor_Urut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Pembelian As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Referensi_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Faktur_Pajak As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kode_Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_Barang As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_Jasa As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DPP_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PPN_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PPh_Dipotong As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Tagihan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sisa_Hutang As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Due_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As System.Windows.Forms.DataGridViewTextBoxColumn
    'Friend WithEvents Potongan_ As System.Windows.Forms.DataGridViewTextBoxColumn (Ini bisa dihapus. Tapi nanti saja).
End Class
