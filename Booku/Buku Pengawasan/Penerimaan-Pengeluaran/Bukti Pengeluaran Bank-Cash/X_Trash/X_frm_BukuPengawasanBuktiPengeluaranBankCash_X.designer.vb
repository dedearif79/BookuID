<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_BukuPengawasanBuktiPengeluaranBankCash_X
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
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        grb_Pengajuan = New GroupBox()
        grb_TindakLanjut = New GroupBox()
        btn_Cetak = New Button()
        btn_Posting = New Button()
        btn_LihatBundelan = New Button()
        btn_LihatJurnal = New Button()
        btn_Bundelan = New Button()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Angka_KK = New DataGridViewTextBoxColumn()
        Nomor_KK = New DataGridViewTextBoxColumn()
        Tanggal_KK = New DataGridViewTextBoxColumn()
        Kode_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Nama_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Jumlah_Invoice = New DataGridViewTextBoxColumn()
        Sarana_Pembayaran = New DataGridViewTextBoxColumn()
        Penerima_Pembayaran = New DataGridViewTextBoxColumn()
        Nomor_BP = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Jumlah_Tagihan = New DataGridViewTextBoxColumn()
        Biaya_Administrasi_Bank = New DataGridViewTextBoxColumn()
        Jumlah_Bayar = New DataGridViewTextBoxColumn()
        Tanggal_Bayar = New DataGridViewTextBoxColumn()
        COA_Kredit = New DataGridViewTextBoxColumn()
        Nama_Akun = New DataGridViewTextBoxColumn()
        Status_ = New DataGridViewTextBoxColumn()
        Uraian_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        User_ = New DataGridViewTextBoxColumn()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        grb_Pengajuan.SuspendLayout()
        grb_TindakLanjut.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(10, 22)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 351
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Location = New Point(218, 22)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 352
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Location = New Point(114, 22)
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Angka_KK, Nomor_KK, Tanggal_KK, Kode_Lawan_Transaksi, Nama_Lawan_Transaksi, Jumlah_Invoice, Sarana_Pembayaran, Penerima_Pembayaran, Nomor_BP, Nomor_Invoice, Tanggal_Invoice, Jumlah_Tagihan, Biaya_Administrasi_Bank, Jumlah_Bayar, Tanggal_Bayar, COA_Kredit, Nama_Akun, Status_, Uraian_, Nomor_JV, User_})
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
        lbl_JudulForm.Size = New Size(682, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Buku Pengawasan Bukti Pengeluaran Bank-Cash"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' grb_Pengajuan
        ' 
        grb_Pengajuan.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_Pengajuan.Controls.Add(btn_Tambah)
        grb_Pengajuan.Controls.Add(btn_Hapus)
        grb_Pengajuan.Controls.Add(btn_Edit)
        grb_Pengajuan.Location = New Point(955, 68)
        grb_Pengajuan.Margin = New Padding(4, 3, 4, 3)
        grb_Pengajuan.Name = "grb_Pengajuan"
        grb_Pengajuan.Padding = New Padding(4, 3, 4, 3)
        grb_Pengajuan.Size = New Size(326, 74)
        grb_Pengajuan.TabIndex = 10031
        grb_Pengajuan.TabStop = False
        grb_Pengajuan.Text = "Pengajuan :"
        ' 
        ' grb_TindakLanjut
        ' 
        grb_TindakLanjut.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_TindakLanjut.Controls.Add(btn_Cetak)
        grb_TindakLanjut.Controls.Add(btn_Posting)
        grb_TindakLanjut.Location = New Point(1289, 68)
        grb_TindakLanjut.Margin = New Padding(4, 3, 4, 3)
        grb_TindakLanjut.Name = "grb_TindakLanjut"
        grb_TindakLanjut.Padding = New Padding(4, 3, 4, 3)
        grb_TindakLanjut.Size = New Size(219, 74)
        grb_TindakLanjut.TabIndex = 10032
        grb_TindakLanjut.TabStop = False
        grb_TindakLanjut.Text = "Tindak Lanjut :"
        ' 
        ' btn_Cetak
        ' 
        btn_Cetak.Location = New Point(8, 22)
        btn_Cetak.Margin = New Padding(4, 3, 4, 3)
        btn_Cetak.Name = "btn_Cetak"
        btn_Cetak.Size = New Size(97, 40)
        btn_Cetak.TabIndex = 356
        btn_Cetak.Text = "Cetak"
        btn_Cetak.UseVisualStyleBackColor = True
        ' 
        ' btn_Posting
        ' 
        btn_Posting.Location = New Point(113, 22)
        btn_Posting.Margin = New Padding(4, 3, 4, 3)
        btn_Posting.Name = "btn_Posting"
        btn_Posting.Size = New Size(97, 40)
        btn_Posting.TabIndex = 354
        btn_Posting.Text = "Posting"
        btn_Posting.UseVisualStyleBackColor = True
        ' 
        ' btn_LihatBundelan
        ' 
        btn_LihatBundelan.Location = New Point(830, 90)
        btn_LihatBundelan.Margin = New Padding(4, 3, 4, 3)
        btn_LihatBundelan.Name = "btn_LihatBundelan"
        btn_LihatBundelan.Size = New Size(118, 40)
        btn_LihatBundelan.TabIndex = 354
        btn_LihatBundelan.Text = "Lihat Bundelan"
        btn_LihatBundelan.UseVisualStyleBackColor = True
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Location = New Point(726, 90)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 40)
        btn_LihatJurnal.TabIndex = 354
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' btn_Bundelan
        ' 
        btn_Bundelan.Location = New Point(116, 66)
        btn_Bundelan.Margin = New Padding(4, 3, 4, 3)
        btn_Bundelan.Name = "btn_Bundelan"
        btn_Bundelan.Size = New Size(94, 75)
        btn_Bundelan.TabIndex = 10033
        btn_Bundelan.Text = "Bundelan"
        btn_Bundelan.UseVisualStyleBackColor = True
        ' 
        ' Nomor_Urut
        ' 
        Nomor_Urut.HeaderText = "No."
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 33
        ' 
        ' Angka_KK
        ' 
        Angka_KK.HeaderText = "Angka KK"
        Angka_KK.Name = "Angka_KK"
        Angka_KK.ReadOnly = True
        Angka_KK.SortMode = DataGridViewColumnSortMode.NotSortable
        Angka_KK.Visible = False
        Angka_KK.Width = 63
        ' 
        ' Nomor_KK
        ' 
        Nomor_KK.HeaderText = "Nomor Bukti"
        Nomor_KK.Name = "Nomor_KK"
        Nomor_KK.ReadOnly = True
        Nomor_KK.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_KK.Width = 99
        ' 
        ' Tanggal_KK
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_KK.DefaultCellStyle = DataGridViewCellStyle1
        Tanggal_KK.HeaderText = "Tanggal"
        Tanggal_KK.Name = "Tanggal_KK"
        Tanggal_KK.ReadOnly = True
        Tanggal_KK.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_KK.Width = 72
        ' 
        ' Kode_Lawan_Transaksi
        ' 
        Kode_Lawan_Transaksi.HeaderText = "Kode Lawan Transaksi"
        Kode_Lawan_Transaksi.Name = "Kode_Lawan_Transaksi"
        Kode_Lawan_Transaksi.ReadOnly = True
        Kode_Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Lawan_Transaksi.Visible = False
        Kode_Lawan_Transaksi.Width = 72
        ' 
        ' Nama_Lawan_Transaksi
        ' 
        Nama_Lawan_Transaksi.HeaderText = "Lawan Transaksi"
        Nama_Lawan_Transaksi.Name = "Nama_Lawan_Transaksi"
        Nama_Lawan_Transaksi.ReadOnly = True
        Nama_Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Lawan_Transaksi.Width = 150
        ' 
        ' Jumlah_Invoice
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Jumlah_Invoice.DefaultCellStyle = DataGridViewCellStyle2
        Jumlah_Invoice.HeaderText = "Jumlah Berkas"
        Jumlah_Invoice.Name = "Jumlah_Invoice"
        Jumlah_Invoice.ReadOnly = True
        Jumlah_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Invoice.Width = 63
        ' 
        ' Sarana_Pembayaran
        ' 
        Sarana_Pembayaran.HeaderText = "Sarana Pembayaran"
        Sarana_Pembayaran.Name = "Sarana_Pembayaran"
        Sarana_Pembayaran.ReadOnly = True
        Sarana_Pembayaran.SortMode = DataGridViewColumnSortMode.NotSortable
        Sarana_Pembayaran.Width = 123
        ' 
        ' Penerima_Pembayaran
        ' 
        Penerima_Pembayaran.HeaderText = "Penerima Pembayaran"
        Penerima_Pembayaran.Name = "Penerima_Pembayaran"
        Penerima_Pembayaran.ReadOnly = True
        Penerima_Pembayaran.SortMode = DataGridViewColumnSortMode.NotSortable
        Penerima_Pembayaran.Width = 123
        ' 
        ' Nomor_BP
        ' 
        Nomor_BP.HeaderText = "Nomor BP"
        Nomor_BP.Name = "Nomor_BP"
        Nomor_BP.ReadOnly = True
        Nomor_BP.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BP.Visible = False
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.ReadOnly = True
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 150
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.ReadOnly = True
        Tanggal_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Invoice.Width = 150
        ' 
        ' Jumlah_Tagihan
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Jumlah_Tagihan.DefaultCellStyle = DataGridViewCellStyle3
        Jumlah_Tagihan.HeaderText = "Jumlah Tagihan"
        Jumlah_Tagihan.Name = "Jumlah_Tagihan"
        Jumlah_Tagihan.ReadOnly = True
        Jumlah_Tagihan.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Tagihan.Visible = False
        Jumlah_Tagihan.Width = 81
        ' 
        ' Biaya_Administrasi_Bank
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Biaya_Administrasi_Bank.DefaultCellStyle = DataGridViewCellStyle4
        Biaya_Administrasi_Bank.HeaderText = "Biaya Adm Bank"
        Biaya_Administrasi_Bank.Name = "Biaya_Administrasi_Bank"
        Biaya_Administrasi_Bank.ReadOnly = True
        Biaya_Administrasi_Bank.SortMode = DataGridViewColumnSortMode.NotSortable
        Biaya_Administrasi_Bank.Visible = False
        Biaya_Administrasi_Bank.Width = 81
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
        ' Tanggal_Bayar
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Bayar.DefaultCellStyle = DataGridViewCellStyle6
        Tanggal_Bayar.HeaderText = "Tanggal Bayar"
        Tanggal_Bayar.Name = "Tanggal_Bayar"
        Tanggal_Bayar.ReadOnly = True
        Tanggal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Bayar.Width = 72
        ' 
        ' COA_Kredit
        ' 
        COA_Kredit.HeaderText = "Kode Akun"
        COA_Kredit.Name = "COA_Kredit"
        COA_Kredit.ReadOnly = True
        COA_Kredit.SortMode = DataGridViewColumnSortMode.NotSortable
        COA_Kredit.Visible = False
        COA_Kredit.Width = 45
        ' 
        ' Nama_Akun
        ' 
        Nama_Akun.HeaderText = "Nama Akun"
        Nama_Akun.Name = "Nama_Akun"
        Nama_Akun.ReadOnly = True
        Nama_Akun.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Akun.Width = 123
        ' 
        ' Status_
        ' 
        Status_.HeaderText = "Status"
        Status_.Name = "Status_"
        Status_.ReadOnly = True
        Status_.SortMode = DataGridViewColumnSortMode.NotSortable
        Status_.Width = 63
        ' 
        ' Uraian_
        ' 
        Uraian_.HeaderText = "Uraian"
        Uraian_.Name = "Uraian_"
        Uraian_.ReadOnly = True
        Uraian_.SortMode = DataGridViewColumnSortMode.NotSortable
        Uraian_.Width = 210
        ' 
        ' Nomor_JV
        ' 
        Nomor_JV.HeaderText = "Nomor JV"
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV.Visible = False
        ' 
        ' User_
        ' 
        User_.HeaderText = "User"
        User_.Name = "User_"
        User_.ReadOnly = True
        User_.SortMode = DataGridViewColumnSortMode.NotSortable
        User_.Visible = False
        ' 
        ' frm_BukuPengawasanBuktiPengeluaranBankCash
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Bundelan)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(btn_LihatBundelan)
        Controls.Add(grb_TindakLanjut)
        Controls.Add(grb_Pengajuan)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanBuktiPengeluaranBankCash"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Bukti Pengeluaran Bank-Cash"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        grb_Pengajuan.ResumeLayout(False)
        grb_TindakLanjut.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents grb_Pengajuan As GroupBox
    Friend WithEvents grb_TindakLanjut As GroupBox
    Friend WithEvents btn_Posting As Button
    Friend WithEvents btn_LihatBundelan As Button
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents btn_Bundelan As Button
    Friend WithEvents btn_Cetak As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Angka_KK As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_KK As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_KK As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Sarana_Pembayaran As DataGridViewTextBoxColumn
    Friend WithEvents Penerima_Pembayaran As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BP As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Tagihan As DataGridViewTextBoxColumn
    Friend WithEvents Biaya_Administrasi_Bank As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents COA_Kredit As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Status_ As DataGridViewTextBoxColumn
    Friend WithEvents Uraian_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents User_ As DataGridViewTextBoxColumn
End Class
