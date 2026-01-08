<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_BukuPengawasanHutangPemegangSaham_X
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
        btn_Tambah = New Button()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_BPHPS = New DataGridViewTextBoxColumn()
        NIK_ = New DataGridViewTextBoxColumn()
        Nama_Pemegang_Saham = New DataGridViewTextBoxColumn()
        Tanggal_Pinjam = New DataGridViewTextBoxColumn()
        Jumlah_Hutang = New DataGridViewTextBoxColumn()
        Saldo_Awal = New DataGridViewTextBoxColumn()
        Jumlah_Angsuran = New DataGridViewTextBoxColumn()
        Saldo_Akhir = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        btn_BukuPembantu = New Button()
        btn_LihatJurnal = New Button()
        grb_Pembayaran = New GroupBox()
        btn_HapusPembayaran = New Button()
        btn_EditPembayaran = New Button()
        btn_InputPembayaran = New Button()
        dgv_DetailBayar = New DataGridView()
        Nomor_ID_Bayar = New DataGridViewTextBoxColumn()
        Tanggal_Bayar = New DataGridViewTextBoxColumn()
        Referensi_ = New DataGridViewTextBoxColumn()
        Nominal_Bayar = New DataGridViewTextBoxColumn()
        Keterangan_Bayar = New DataGridViewTextBoxColumn()
        Nomor_JV_Bayar = New DataGridViewTextBoxColumn()
        btn_Posting = New Button()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        grb_Pembayaran.SuspendLayout()
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Tambah.Location = New Point(372, 100)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 351
        btn_Tambah.Text = "Tambah"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Hapus.Location = New Point(580, 100)
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
        btn_Edit.Location = New Point(476, 100)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 353
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
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
        lbl_JudulForm.Size = New Size(629, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Buku Pengawasan Hutang Pemegang Saham"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPHPS, NIK_, Nama_Pemegang_Saham, Tanggal_Pinjam, Jumlah_Hutang, Saldo_Awal, Jumlah_Angsuran, Saldo_Akhir, Keterangan_, Nomor_JV})
        DataTabelUtama.Location = New Point(14, 148)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 543)
        DataTabelUtama.TabIndex = 10039
        ' 
        ' Nomor_Urut
        ' 
        Nomor_Urut.HeaderText = "No."
        Nomor_Urut.MinimumWidth = 6
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 33
        ' 
        ' Nomor_ID
        ' 
        Nomor_ID.HeaderText = "Nomor ID"
        Nomor_ID.Name = "Nomor_ID"
        Nomor_ID.ReadOnly = True
        Nomor_ID.Visible = False
        ' 
        ' Nomor_BPHPS
        ' 
        Nomor_BPHPS.HeaderText = "Nomor BPHPS"
        Nomor_BPHPS.MinimumWidth = 6
        Nomor_BPHPS.Name = "Nomor_BPHPS"
        Nomor_BPHPS.ReadOnly = True
        Nomor_BPHPS.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPHPS.Width = 99
        ' 
        ' NIK_
        ' 
        NIK_.HeaderText = "NIK"
        NIK_.MinimumWidth = 6
        NIK_.Name = "NIK_"
        NIK_.ReadOnly = True
        NIK_.SortMode = DataGridViewColumnSortMode.NotSortable
        NIK_.Visible = False
        NIK_.Width = 99
        ' 
        ' Nama_Pemegang_Saham
        ' 
        Nama_Pemegang_Saham.HeaderText = "Nama Pemegang Saham"
        Nama_Pemegang_Saham.MinimumWidth = 6
        Nama_Pemegang_Saham.Name = "Nama_Pemegang_Saham"
        Nama_Pemegang_Saham.ReadOnly = True
        Nama_Pemegang_Saham.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Pemegang_Saham.Width = 170
        ' 
        ' Tanggal_Pinjam
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Pinjam.DefaultCellStyle = DataGridViewCellStyle1
        Tanggal_Pinjam.HeaderText = "Tanggal Pinjam"
        Tanggal_Pinjam.MinimumWidth = 6
        Tanggal_Pinjam.Name = "Tanggal_Pinjam"
        Tanggal_Pinjam.ReadOnly = True
        Tanggal_Pinjam.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Pinjam.Width = 69
        ' 
        ' Jumlah_Hutang
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Jumlah_Hutang.DefaultCellStyle = DataGridViewCellStyle2
        Jumlah_Hutang.HeaderText = "Jumlah Hutang"
        Jumlah_Hutang.MinimumWidth = 6
        Jumlah_Hutang.Name = "Jumlah_Hutang"
        Jumlah_Hutang.ReadOnly = True
        Jumlah_Hutang.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Hutang.Width = 81
        ' 
        ' Saldo_Awal
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Saldo_Awal.DefaultCellStyle = DataGridViewCellStyle3
        Saldo_Awal.HeaderText = "Saldo Awal"
        Saldo_Awal.MinimumWidth = 6
        Saldo_Awal.Name = "Saldo_Awal"
        Saldo_Awal.ReadOnly = True
        Saldo_Awal.SortMode = DataGridViewColumnSortMode.NotSortable
        Saldo_Awal.Width = 81
        ' 
        ' Jumlah_Angsuran
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Jumlah_Angsuran.DefaultCellStyle = DataGridViewCellStyle4
        Jumlah_Angsuran.HeaderText = "Jumlah Angsuran"
        Jumlah_Angsuran.MinimumWidth = 6
        Jumlah_Angsuran.Name = "Jumlah_Angsuran"
        Jumlah_Angsuran.ReadOnly = True
        Jumlah_Angsuran.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Angsuran.Width = 81
        ' 
        ' Saldo_Akhir
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Saldo_Akhir.DefaultCellStyle = DataGridViewCellStyle5
        Saldo_Akhir.HeaderText = "Saldo Akhir"
        Saldo_Akhir.MinimumWidth = 6
        Saldo_Akhir.Name = "Saldo_Akhir"
        Saldo_Akhir.ReadOnly = True
        Saldo_Akhir.SortMode = DataGridViewColumnSortMode.NotSortable
        Saldo_Akhir.Width = 81
        ' 
        ' Keterangan_
        ' 
        Keterangan_.HeaderText = "Keterangan"
        Keterangan_.MinimumWidth = 6
        Keterangan_.Name = "Keterangan_"
        Keterangan_.ReadOnly = True
        Keterangan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_.Width = 210
        ' 
        ' Nomor_JV
        ' 
        Nomor_JV.HeaderText = "Nomor JV"
        Nomor_JV.MinimumWidth = 6
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV.Visible = False
        Nomor_JV.Width = 125
        ' 
        ' btn_BukuPembantu
        ' 
        btn_BukuPembantu.Location = New Point(115, 66)
        btn_BukuPembantu.Margin = New Padding(4, 3, 4, 3)
        btn_BukuPembantu.Name = "btn_BukuPembantu"
        btn_BukuPembantu.Size = New Size(94, 75)
        btn_BukuPembantu.TabIndex = 10207
        btn_BukuPembantu.Text = "Buku Pembantu"
        btn_BukuPembantu.UseVisualStyleBackColor = True
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(847, 14)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 39)
        btn_LihatJurnal.TabIndex = 10209
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' grb_Pembayaran
        ' 
        grb_Pembayaran.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        grb_Pembayaran.Controls.Add(btn_HapusPembayaran)
        grb_Pembayaran.Controls.Add(btn_EditPembayaran)
        grb_Pembayaran.Controls.Add(btn_InputPembayaran)
        grb_Pembayaran.Controls.Add(dgv_DetailBayar)
        grb_Pembayaran.Location = New Point(1015, 7)
        grb_Pembayaran.Margin = New Padding(4, 3, 4, 3)
        grb_Pembayaran.Name = "grb_Pembayaran"
        grb_Pembayaran.Padding = New Padding(4, 3, 4, 3)
        grb_Pembayaran.Size = New Size(492, 134)
        grb_Pembayaran.TabIndex = 10208
        grb_Pembayaran.TabStop = False
        grb_Pembayaran.Text = "Pembayaran :"
        ' 
        ' btn_HapusPembayaran
        ' 
        btn_HapusPembayaran.Enabled = False
        btn_HapusPembayaran.Location = New Point(13, 93)
        btn_HapusPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_HapusPembayaran.Name = "btn_HapusPembayaran"
        btn_HapusPembayaran.Size = New Size(94, 32)
        btn_HapusPembayaran.TabIndex = 10062
        btn_HapusPembayaran.Text = "Hapus"
        btn_HapusPembayaran.UseVisualStyleBackColor = True
        ' 
        ' btn_EditPembayaran
        ' 
        btn_EditPembayaran.Enabled = False
        btn_EditPembayaran.Location = New Point(13, 58)
        btn_EditPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_EditPembayaran.Name = "btn_EditPembayaran"
        btn_EditPembayaran.Size = New Size(94, 32)
        btn_EditPembayaran.TabIndex = 10061
        btn_EditPembayaran.Text = "Edit"
        btn_EditPembayaran.UseVisualStyleBackColor = True
        ' 
        ' btn_InputPembayaran
        ' 
        btn_InputPembayaran.Location = New Point(13, 22)
        btn_InputPembayaran.Margin = New Padding(4, 3, 4, 3)
        btn_InputPembayaran.Name = "btn_InputPembayaran"
        btn_InputPembayaran.Size = New Size(94, 32)
        btn_InputPembayaran.TabIndex = 10048
        btn_InputPembayaran.Text = "Bayar"
        btn_InputPembayaran.UseVisualStyleBackColor = True
        ' 
        ' dgv_DetailBayar
        ' 
        dgv_DetailBayar.AllowUserToAddRows = False
        dgv_DetailBayar.AllowUserToDeleteRows = False
        dgv_DetailBayar.AllowUserToResizeRows = False
        dgv_DetailBayar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        dgv_DetailBayar.BorderStyle = BorderStyle.None
        dgv_DetailBayar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_DetailBayar.Columns.AddRange(New DataGridViewColumn() {Nomor_ID_Bayar, Tanggal_Bayar, Referensi_, Nominal_Bayar, Keterangan_Bayar, Nomor_JV_Bayar})
        dgv_DetailBayar.Location = New Point(119, 22)
        dgv_DetailBayar.Margin = New Padding(4, 3, 4, 3)
        dgv_DetailBayar.MultiSelect = False
        dgv_DetailBayar.Name = "dgv_DetailBayar"
        dgv_DetailBayar.ReadOnly = True
        dgv_DetailBayar.RowHeadersVisible = False
        dgv_DetailBayar.RowHeadersWidth = 33
        dgv_DetailBayar.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_DetailBayar.Size = New Size(359, 104)
        dgv_DetailBayar.TabIndex = 10040
        ' 
        ' Nomor_ID_Bayar
        ' 
        Nomor_ID_Bayar.HeaderText = "Nomor ID Bayar"
        Nomor_ID_Bayar.MinimumWidth = 6
        Nomor_ID_Bayar.Name = "Nomor_ID_Bayar"
        Nomor_ID_Bayar.ReadOnly = True
        Nomor_ID_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID_Bayar.Visible = False
        Nomor_ID_Bayar.Width = 125
        ' 
        ' Tanggal_Bayar
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Bayar.DefaultCellStyle = DataGridViewCellStyle6
        Tanggal_Bayar.HeaderText = "Tgl. Bayar"
        Tanggal_Bayar.MinimumWidth = 6
        Tanggal_Bayar.Name = "Tanggal_Bayar"
        Tanggal_Bayar.ReadOnly = True
        Tanggal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Bayar.Width = 81
        ' 
        ' Referensi_
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft
        Referensi_.DefaultCellStyle = DataGridViewCellStyle7
        Referensi_.HeaderText = "Referensi"
        Referensi_.MinimumWidth = 6
        Referensi_.Name = "Referensi_"
        Referensi_.ReadOnly = True
        Referensi_.SortMode = DataGridViewColumnSortMode.NotSortable
        Referensi_.Width = 123
        ' 
        ' Nominal_Bayar
        ' 
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N0"
        DataGridViewCellStyle8.NullValue = Nothing
        Nominal_Bayar.DefaultCellStyle = DataGridViewCellStyle8
        Nominal_Bayar.HeaderText = "Jumlah Bayar"
        Nominal_Bayar.MinimumWidth = 6
        Nominal_Bayar.Name = "Nominal_Bayar"
        Nominal_Bayar.ReadOnly = True
        Nominal_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Nominal_Bayar.Width = 99
        ' 
        ' Keterangan_Bayar
        ' 
        Keterangan_Bayar.HeaderText = "Keterangan Bayar"
        Keterangan_Bayar.MinimumWidth = 6
        Keterangan_Bayar.Name = "Keterangan_Bayar"
        Keterangan_Bayar.ReadOnly = True
        Keterangan_Bayar.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_Bayar.Visible = False
        Keterangan_Bayar.Width = 33
        ' 
        ' Nomor_JV_Bayar
        ' 
        Nomor_JV_Bayar.HeaderText = "Nomor JV Bayar"
        Nomor_JV_Bayar.MinimumWidth = 6
        Nomor_JV_Bayar.Name = "Nomor_JV_Bayar"
        Nomor_JV_Bayar.ReadOnly = True
        Nomor_JV_Bayar.Visible = False
        Nomor_JV_Bayar.Width = 125
        ' 
        ' btn_Posting
        ' 
        btn_Posting.Enabled = False
        btn_Posting.Location = New Point(742, 13)
        btn_Posting.Margin = New Padding(4, 3, 4, 3)
        btn_Posting.Name = "btn_Posting"
        btn_Posting.Size = New Size(97, 40)
        btn_Posting.TabIndex = 10210
        btn_Posting.Text = "Posting"
        btn_Posting.UseVisualStyleBackColor = True
        ' 
        ' btn_Export
        ' 
        btn_Export.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Export.Location = New Point(847, 96)
        btn_Export.Margin = New Padding(4, 3, 4, 3)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(115, 40)
        btn_Export.TabIndex = 10235
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuPengawasanHutangPemegangSaham
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Export)
        Controls.Add(btn_Posting)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(grb_Pembayaran)
        Controls.Add(btn_BukuPembantu)
        Controls.Add(DataTabelUtama)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanHutangPemegangSaham"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Hutang Pemegang Saham"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        grb_Pembayaran.ResumeLayout(False)
        CType(dgv_DetailBayar, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_BukuPembantu As Button
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents grb_Pembayaran As GroupBox
    Friend WithEvents btn_HapusPembayaran As Button
    Friend WithEvents btn_EditPembayaran As Button
    Friend WithEvents btn_InputPembayaran As Button
    Friend WithEvents dgv_DetailBayar As DataGridView
    Friend WithEvents Nomor_ID_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Referensi_ As DataGridViewTextBoxColumn
    Friend WithEvents Nominal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPHPS As DataGridViewTextBoxColumn
    Friend WithEvents NIK_ As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Pemegang_Saham As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Pinjam As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Hutang As DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Awal As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Angsuran As DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Akhir As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents btn_Posting As Button
    Friend WithEvents btn_Export As Button
End Class
