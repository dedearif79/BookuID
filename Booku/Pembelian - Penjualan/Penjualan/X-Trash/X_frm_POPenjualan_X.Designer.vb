<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_POPenjualan_X
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
        btn_Edit = New Button()
        btn_Hapus = New Button()
        btn_Tambah = New Button()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Angka_PO = New DataGridViewTextBoxColumn()
        Kode_Customer = New DataGridViewTextBoxColumn()
        Nama_Customer = New DataGridViewTextBoxColumn()
        Nomor_PO = New DataGridViewTextBoxColumn()
        Tanggal_PO = New DataGridViewTextBoxColumn()
        Jenis_Produk = New DataGridViewTextBoxColumn()
        Kode_Project = New DataGridViewTextBoxColumn()
        Jumlah_Harga = New DataGridViewTextBoxColumn()
        Diskon_Rp = New DataGridViewTextBoxColumn()
        Dasar_Pengenaan_Pajak = New DataGridViewTextBoxColumn()
        Jenis_PPN = New DataGridViewTextBoxColumn()
        Perlakuan_PPN = New DataGridViewTextBoxColumn()
        PPN_ = New DataGridViewTextBoxColumn()
        PPh_Dipotong = New DataGridViewTextBoxColumn()
        Total_Tagihan = New DataGridViewTextBoxColumn()
        Term_Of_Payment = New DataGridViewTextBoxColumn()
        Nomor_SJ_BAST = New DataGridViewTextBoxColumn()
        Tanggal_SJ_BAST = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Jangka_Waktu_Penyelesaian = New DataGridViewTextBoxColumn()
        Catatan_ = New DataGridViewTextBoxColumn()
        Kontrol_ = New DataGridViewTextBoxColumn()
        lbl_JudulForm = New Label()
        Label15 = New Label()
        cmb_Kontrol = New ComboBox()
        Label1 = New Label()
        cmb_JenisProduk_Induk = New ComboBox()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Location = New Point(834, 98)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10045
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Location = New Point(937, 98)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10044
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(730, 98)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 10043
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10042
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Angka_PO, Kode_Customer, Nama_Customer, Nomor_PO, Tanggal_PO, Jenis_Produk, Kode_Project, Jumlah_Harga, Diskon_Rp, Dasar_Pengenaan_Pajak, Jenis_PPN, Perlakuan_PPN, PPN_, PPh_Dipotong, Total_Tagihan, Term_Of_Payment, Nomor_SJ_BAST, Tanggal_SJ_BAST, Nomor_Invoice, Tanggal_Invoice, Jangka_Waktu_Penyelesaian, Catatan_, Kontrol_})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1306, 493)
        DataTabelUtama.TabIndex = 10046
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.Padding = New Padding(0, 0, 3, 0)
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_Urut.HeaderText = "No."
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 45
        ' 
        ' Angka_PO
        ' 
        Angka_PO.HeaderText = "Angka PO"
        Angka_PO.Name = "Angka_PO"
        Angka_PO.ReadOnly = True
        Angka_PO.SortMode = DataGridViewColumnSortMode.NotSortable
        Angka_PO.Visible = False
        ' 
        ' Kode_Customer
        ' 
        Kode_Customer.HeaderText = "Kode Customer"
        Kode_Customer.Name = "Kode_Customer"
        Kode_Customer.ReadOnly = True
        Kode_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Customer.Visible = False
        ' 
        ' Nama_Customer
        ' 
        Nama_Customer.HeaderText = "Nama Customer"
        Nama_Customer.Name = "Nama_Customer"
        Nama_Customer.ReadOnly = True
        Nama_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Customer.Width = 150
        ' 
        ' Nomor_PO
        ' 
        Nomor_PO.HeaderText = "Nomor PO"
        Nomor_PO.Name = "Nomor_PO"
        Nomor_PO.ReadOnly = True
        Nomor_PO.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_PO.Width = 87
        ' 
        ' Tanggal_PO
        ' 
        Tanggal_PO.HeaderText = "Tanggal PO"
        Tanggal_PO.Name = "Tanggal_PO"
        Tanggal_PO.ReadOnly = True
        Tanggal_PO.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_PO.Width = 63
        ' 
        ' Jenis_Produk
        ' 
        Jenis_Produk.HeaderText = "Jenis Produk"
        Jenis_Produk.Name = "Jenis_Produk"
        Jenis_Produk.ReadOnly = True
        Jenis_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Produk.Visible = False
        ' 
        ' Kode_Project
        ' 
        Kode_Project.HeaderText = "Kode Project"
        Kode_Project.Name = "Kode_Project"
        Kode_Project.ReadOnly = True
        Kode_Project.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Project.Width = 99
        ' 
        ' Jumlah_Harga
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Jumlah_Harga.DefaultCellStyle = DataGridViewCellStyle2
        Jumlah_Harga.HeaderText = "Jumlah Harga"
        Jumlah_Harga.Name = "Jumlah_Harga"
        Jumlah_Harga.ReadOnly = True
        Jumlah_Harga.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Harga.Width = 81
        ' 
        ' Diskon_Rp
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Diskon_Rp.DefaultCellStyle = DataGridViewCellStyle3
        Diskon_Rp.HeaderText = "Diskon"
        Diskon_Rp.Name = "Diskon_Rp"
        Diskon_Rp.ReadOnly = True
        Diskon_Rp.SortMode = DataGridViewColumnSortMode.NotSortable
        Diskon_Rp.Width = 81
        ' 
        ' Dasar_Pengenaan_Pajak
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Dasar_Pengenaan_Pajak.DefaultCellStyle = DataGridViewCellStyle4
        Dasar_Pengenaan_Pajak.HeaderText = "Dasar Pengenaan Pajak"
        Dasar_Pengenaan_Pajak.Name = "Dasar_Pengenaan_Pajak"
        Dasar_Pengenaan_Pajak.ReadOnly = True
        Dasar_Pengenaan_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Dasar_Pengenaan_Pajak.Width = 81
        ' 
        ' Jenis_PPN
        ' 
        Jenis_PPN.HeaderText = "Jenis PPN"
        Jenis_PPN.Name = "Jenis_PPN"
        Jenis_PPN.ReadOnly = True
        Jenis_PPN.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_PPN.Visible = False
        ' 
        ' Perlakuan_PPN
        ' 
        Perlakuan_PPN.HeaderText = "Perlakuan PPN"
        Perlakuan_PPN.Name = "Perlakuan_PPN"
        Perlakuan_PPN.ReadOnly = True
        Perlakuan_PPN.SortMode = DataGridViewColumnSortMode.NotSortable
        Perlakuan_PPN.Visible = False
        Perlakuan_PPN.Width = 99
        ' 
        ' PPN_
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        PPN_.DefaultCellStyle = DataGridViewCellStyle5
        PPN_.HeaderText = "PPN"
        PPN_.Name = "PPN_"
        PPN_.ReadOnly = True
        PPN_.SortMode = DataGridViewColumnSortMode.NotSortable
        PPN_.Width = 81
        ' 
        ' PPh_Dipotong
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle6
        PPh_Dipotong.HeaderText = "PPh Dipotong"
        PPh_Dipotong.Name = "PPh_Dipotong"
        PPh_Dipotong.ReadOnly = True
        PPh_Dipotong.SortMode = DataGridViewColumnSortMode.NotSortable
        PPh_Dipotong.Width = 81
        ' 
        ' Total_Tagihan
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Total_Tagihan.DefaultCellStyle = DataGridViewCellStyle7
        Total_Tagihan.HeaderText = "Total Tagihan"
        Total_Tagihan.Name = "Total_Tagihan"
        Total_Tagihan.ReadOnly = True
        Total_Tagihan.SortMode = DataGridViewColumnSortMode.NotSortable
        Total_Tagihan.Width = 81
        ' 
        ' Term_Of_Payment
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft
        Term_Of_Payment.DefaultCellStyle = DataGridViewCellStyle8
        Term_Of_Payment.HeaderText = "Term of Payment"
        Term_Of_Payment.Name = "Term_Of_Payment"
        Term_Of_Payment.ReadOnly = True
        Term_Of_Payment.SortMode = DataGridViewColumnSortMode.NotSortable
        Term_Of_Payment.Width = 123
        ' 
        ' Nomor_SJ_BAST
        ' 
        Nomor_SJ_BAST.HeaderText = "Nomor SJ/BAST"
        Nomor_SJ_BAST.Name = "Nomor_SJ_BAST"
        Nomor_SJ_BAST.ReadOnly = True
        Nomor_SJ_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_SJ_BAST.Width = 123
        ' 
        ' Tanggal_SJ_BAST
        ' 
        Tanggal_SJ_BAST.HeaderText = "Tanggal SJ/BAST"
        Tanggal_SJ_BAST.Name = "Tanggal_SJ_BAST"
        Tanggal_SJ_BAST.ReadOnly = True
        Tanggal_SJ_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_SJ_BAST.Width = 123
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.ReadOnly = True
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 123
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.ReadOnly = True
        Tanggal_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Invoice.Width = 123
        ' 
        ' Jangka_Waktu_Penyelesaian
        ' 
        Jangka_Waktu_Penyelesaian.HeaderText = "Jangka Waktu Pnylsaian"
        Jangka_Waktu_Penyelesaian.Name = "Jangka_Waktu_Penyelesaian"
        Jangka_Waktu_Penyelesaian.ReadOnly = True
        Jangka_Waktu_Penyelesaian.SortMode = DataGridViewColumnSortMode.NotSortable
        Jangka_Waktu_Penyelesaian.Width = 63
        ' 
        ' Catatan_
        ' 
        Catatan_.HeaderText = "Catatan"
        Catatan_.Name = "Catatan_"
        Catatan_.ReadOnly = True
        Catatan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Catatan_.Width = 210
        ' 
        ' Kontrol_
        ' 
        Kontrol_.HeaderText = "Kontrol"
        Kontrol_.Name = "Kontrol_"
        Kontrol_.ReadOnly = True
        Kontrol_.SortMode = DataGridViewColumnSortMode.NotSortable
        Kontrol_.Width = 57
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(463, 32)
        lbl_JudulForm.TabIndex = 10047
        lbl_JudulForm.Text = "Buku Pengawasan PO Penjualan"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(444, 87)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(46, 13)
        Label15.TabIndex = 10184
        Label15.Text = "Kontrol :"
        ' 
        ' cmb_Kontrol
        ' 
        cmb_Kontrol.FormattingEnabled = True
        cmb_Kontrol.Location = New Point(448, 107)
        cmb_Kontrol.Margin = New Padding(4, 3, 4, 3)
        cmb_Kontrol.Name = "cmb_Kontrol"
        cmb_Kontrol.Size = New Size(87, 23)
        cmb_Kontrol.TabIndex = 10183
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(286, 87)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(74, 13)
        Label1.TabIndex = 10188
        Label1.Text = "Jenis Produk :"
        ' 
        ' cmb_JenisProduk_Induk
        ' 
        cmb_JenisProduk_Induk.FormattingEnabled = True
        cmb_JenisProduk_Induk.Location = New Point(289, 107)
        cmb_JenisProduk_Induk.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisProduk_Induk.Name = "cmb_JenisProduk_Induk"
        cmb_JenisProduk_Induk.Size = New Size(125, 23)
        cmb_JenisProduk_Induk.TabIndex = 10187
        ' 
        ' frm_POPenjualan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1334, 682)
        Controls.Add(Label1)
        Controls.Add(cmb_JenisProduk_Induk)
        Controls.Add(Label15)
        Controls.Add(cmb_Kontrol)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(lbl_JudulForm)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_POPenjualan"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan PO Penjualan"
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cmb_Kontrol As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_JenisProduk_Induk As ComboBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Angka_PO As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_PO As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Harga As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Rp As DataGridViewTextBoxColumn
    Friend WithEvents Dasar_Pengenaan_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_PPN As DataGridViewTextBoxColumn
    Friend WithEvents Perlakuan_PPN As DataGridViewTextBoxColumn
    Friend WithEvents PPN_ As DataGridViewTextBoxColumn
    Friend WithEvents PPh_Dipotong As DataGridViewTextBoxColumn
    Friend WithEvents Total_Tagihan As DataGridViewTextBoxColumn
    Friend WithEvents Term_Of_Payment As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Jangka_Waktu_Penyelesaian As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Kontrol_ As DataGridViewTextBoxColumn
End Class
