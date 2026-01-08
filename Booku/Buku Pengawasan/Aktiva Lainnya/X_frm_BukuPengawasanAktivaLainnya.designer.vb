<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_BukuPengawasanAktivaLainnya
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
        btn_Tambah = New Button()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_BPAL = New DataGridViewTextBoxColumn()
        Nomor_Bukti = New DataGridViewTextBoxColumn()
        Tanggal_Bukti = New DataGridViewTextBoxColumn()
        Kode_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Nama_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Uraian_Transaksi = New DataGridViewTextBoxColumn()
        COA_Debet = New DataGridViewTextBoxColumn()
        COA_Kredit = New DataGridViewTextBoxColumn()
        Nama_Akun = New DataGridViewTextBoxColumn()
        Jumlah_Transaksi = New DataGridViewTextBoxColumn()
        Tanggal_Pencairan = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        User_ = New DataGridViewTextBoxColumn()
        btn_LihatJurnal = New Button()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Tambah.Location = New Point(1203, 92)
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
        btn_Hapus.Location = New Point(1410, 92)
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
        btn_Edit.Location = New Point(1307, 92)
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
        lbl_JudulForm.Size = New Size(485, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Buku Pengawasan Aktiva Lain-lain"
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPAL, Nomor_Bukti, Tanggal_Bukti, Kode_Lawan_Transaksi, Nama_Lawan_Transaksi, Uraian_Transaksi, COA_Debet, COA_Kredit, Nama_Akun, Jumlah_Transaksi, Tanggal_Pencairan, Keterangan_, Nomor_JV, User_})
        DataTabelUtama.Location = New Point(14, 148)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 543)
        DataTabelUtama.TabIndex = 10031
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_Urut.HeaderText = "Nomor Urut"
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 45
        ' 
        ' Nomor_ID
        ' 
        Nomor_ID.HeaderText = "Nomor ID"
        Nomor_ID.Name = "Nomor_ID"
        Nomor_ID.ReadOnly = True
        Nomor_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID.Visible = False
        ' 
        ' Nomor_BPAL
        ' 
        Nomor_BPAL.HeaderText = "Nomor BPAL"
        Nomor_BPAL.Name = "Nomor_BPAL"
        Nomor_BPAL.ReadOnly = True
        Nomor_BPAL.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPAL.Visible = False
        Nomor_BPAL.Width = 99
        ' 
        ' Nomor_Bukti
        ' 
        Nomor_Bukti.HeaderText = "Nomor Bukti"
        Nomor_Bukti.Name = "Nomor_Bukti"
        Nomor_Bukti.ReadOnly = True
        Nomor_Bukti.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Bukti.Width = 123
        ' 
        ' Tanggal_Bukti
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        Tanggal_Bukti.DefaultCellStyle = DataGridViewCellStyle2
        Tanggal_Bukti.HeaderText = "Tanggal Bukti"
        Tanggal_Bukti.Name = "Tanggal_Bukti"
        Tanggal_Bukti.ReadOnly = True
        Tanggal_Bukti.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Bukti.Width = 63
        ' 
        ' Kode_Lawan_Transaksi
        ' 
        Kode_Lawan_Transaksi.HeaderText = "Kode Lawan Transaksi"
        Kode_Lawan_Transaksi.Name = "Kode_Lawan_Transaksi"
        Kode_Lawan_Transaksi.ReadOnly = True
        Kode_Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Lawan_Transaksi.Visible = False
        Kode_Lawan_Transaksi.Width = 63
        ' 
        ' Nama_Lawan_Transaksi
        ' 
        Nama_Lawan_Transaksi.HeaderText = "Nama Lawan Transaksi"
        Nama_Lawan_Transaksi.Name = "Nama_Lawan_Transaksi"
        Nama_Lawan_Transaksi.ReadOnly = True
        Nama_Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Lawan_Transaksi.Width = 150
        ' 
        ' Uraian_Transaksi
        ' 
        Uraian_Transaksi.HeaderText = "Uraian Transaksi"
        Uraian_Transaksi.Name = "Uraian_Transaksi"
        Uraian_Transaksi.ReadOnly = True
        Uraian_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Uraian_Transaksi.Width = 210
        ' 
        ' COA_Debet
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        COA_Debet.DefaultCellStyle = DataGridViewCellStyle3
        COA_Debet.HeaderText = "Kode Akun"
        COA_Debet.Name = "COA_Debet"
        COA_Debet.ReadOnly = True
        COA_Debet.SortMode = DataGridViewColumnSortMode.NotSortable
        COA_Debet.Width = 45
        ' 
        ' COA_Kredit
        ' 
        COA_Kredit.HeaderText = "COA Kredit"
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
        Nama_Akun.Width = 150
        ' 
        ' Jumlah_Transaksi
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Jumlah_Transaksi.DefaultCellStyle = DataGridViewCellStyle4
        Jumlah_Transaksi.HeaderText = "Jumlah Transaksi"
        Jumlah_Transaksi.Name = "Jumlah_Transaksi"
        Jumlah_Transaksi.ReadOnly = True
        Jumlah_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Transaksi.Width = 81
        ' 
        ' Tanggal_Pencairan
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Pencairan.DefaultCellStyle = DataGridViewCellStyle5
        Tanggal_Pencairan.HeaderText = "Tanggal Pencairan"
        Tanggal_Pencairan.Name = "Tanggal_Pencairan"
        Tanggal_Pencairan.ReadOnly = True
        Tanggal_Pencairan.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Pencairan.Width = 63
        ' 
        ' Keterangan_
        ' 
        Keterangan_.HeaderText = "Keterangan"
        Keterangan_.Name = "Keterangan_"
        Keterangan_.ReadOnly = True
        Keterangan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_.Width = 210
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
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(715, 92)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 39)
        btn_LihatJurnal.TabIndex = 10216
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' btn_Export
        ' 
        btn_Export.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Export.Location = New Point(966, 79)
        btn_Export.Margin = New Padding(4, 3, 4, 3)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(115, 52)
        btn_Export.TabIndex = 10233
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuPengawasanAktivaLainnya
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Export)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(DataTabelUtama)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanAktivaLainnya"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Aktiva Lain-lain"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPAL As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Bukti As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bukti As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Uraian_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents COA_Debet As DataGridViewTextBoxColumn
    Friend WithEvents COA_Kredit As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Pencairan As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents User_ As DataGridViewTextBoxColumn
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents btn_Export As Button
End Class
