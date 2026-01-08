<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_BukuBankGaransi
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
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_BPBG = New DataGridViewTextBoxColumn()
        Nomor_Kontrak = New DataGridViewTextBoxColumn()
        Tanggal_Transaksi = New DataGridViewTextBoxColumn()
        Nama_Bank = New DataGridViewTextBoxColumn()
        Keperluan_ = New DataGridViewTextBoxColumn()
        Kode_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Nama_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Jumlah_Transaksi = New DataGridViewTextBoxColumn()
        Biaya_Provisi = New DataGridViewTextBoxColumn()
        Tanggal_Pencairan = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        Nomor_JV_Transaksi = New DataGridViewTextBoxColumn()
        Nomor_JV_Pencairan = New DataGridViewTextBoxColumn()
        User_ = New DataGridViewTextBoxColumn()
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        btn_LihatJurnalTransaksi = New Button()
        btn_LihatJurnalPencairan = New Button()
        btn_Cairkan = New Button()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Tambah.Location = New Point(1099, 92)
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
        btn_Edit.Location = New Point(1203, 92)
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPBG, Nomor_Kontrak, Tanggal_Transaksi, Nama_Bank, Keperluan_, Kode_Lawan_Transaksi, Nama_Lawan_Transaksi, Jumlah_Transaksi, Biaya_Provisi, Tanggal_Pencairan, Keterangan_, Nomor_JV_Transaksi, Nomor_JV_Pencairan, User_})
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
        ' Nomor_BPBG
        ' 
        Nomor_BPBG.HeaderText = "Nomor BPBG"
        Nomor_BPBG.Name = "Nomor_BPBG"
        Nomor_BPBG.ReadOnly = True
        Nomor_BPBG.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPBG.Visible = False
        Nomor_BPBG.Width = 99
        ' 
        ' Nomor_Kontrak
        ' 
        Nomor_Kontrak.HeaderText = "Nomor Kontrak"
        Nomor_Kontrak.Name = "Nomor_Kontrak"
        Nomor_Kontrak.ReadOnly = True
        Nomor_Kontrak.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Kontrak.Width = 123
        ' 
        ' Tanggal_Transaksi
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Transaksi.DefaultCellStyle = DataGridViewCellStyle2
        Tanggal_Transaksi.HeaderText = "Tanggal Transaksi"
        Tanggal_Transaksi.Name = "Tanggal_Transaksi"
        Tanggal_Transaksi.ReadOnly = True
        Tanggal_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Transaksi.Width = 63
        ' 
        ' Nama_Bank
        ' 
        Nama_Bank.HeaderText = "Nama Bank"
        Nama_Bank.Name = "Nama_Bank"
        Nama_Bank.ReadOnly = True
        Nama_Bank.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Bank.Width = 150
        ' 
        ' Keperluan_
        ' 
        Keperluan_.HeaderText = "Keperluan"
        Keperluan_.Name = "Keperluan_"
        Keperluan_.ReadOnly = True
        Keperluan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Keperluan_.Width = 150
        ' 
        ' Kode_Lawan_Transaksi
        ' 
        Kode_Lawan_Transaksi.HeaderText = "Kode Lawan Transaksi"
        Kode_Lawan_Transaksi.Name = "Kode_Lawan_Transaksi"
        Kode_Lawan_Transaksi.ReadOnly = True
        Kode_Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Lawan_Transaksi.Visible = False
        Kode_Lawan_Transaksi.Width = 99
        ' 
        ' Nama_Lawan_Transaksi
        ' 
        Nama_Lawan_Transaksi.HeaderText = "Nama Lawan Transaksi"
        Nama_Lawan_Transaksi.Name = "Nama_Lawan_Transaksi"
        Nama_Lawan_Transaksi.ReadOnly = True
        Nama_Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Lawan_Transaksi.Width = 180
        ' 
        ' Jumlah_Transaksi
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Jumlah_Transaksi.DefaultCellStyle = DataGridViewCellStyle3
        Jumlah_Transaksi.HeaderText = "Jumlah Transaksi"
        Jumlah_Transaksi.Name = "Jumlah_Transaksi"
        Jumlah_Transaksi.ReadOnly = True
        Jumlah_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Transaksi.Width = 81
        ' 
        ' Biaya_Provisi
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Biaya_Provisi.DefaultCellStyle = DataGridViewCellStyle4
        Biaya_Provisi.HeaderText = "Biaya Provisi"
        Biaya_Provisi.Name = "Biaya_Provisi"
        Biaya_Provisi.ReadOnly = True
        Biaya_Provisi.SortMode = DataGridViewColumnSortMode.NotSortable
        Biaya_Provisi.Visible = False
        Biaya_Provisi.Width = 81
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
        ' Nomor_JV_Transaksi
        ' 
        Nomor_JV_Transaksi.HeaderText = "Nomor JV Transaksi"
        Nomor_JV_Transaksi.Name = "Nomor_JV_Transaksi"
        Nomor_JV_Transaksi.ReadOnly = True
        Nomor_JV_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV_Transaksi.Visible = False
        ' 
        ' Nomor_JV_Pencairan
        ' 
        Nomor_JV_Pencairan.HeaderText = "Nomor JV Pencairan"
        Nomor_JV_Pencairan.Name = "Nomor_JV_Pencairan"
        Nomor_JV_Pencairan.ReadOnly = True
        Nomor_JV_Pencairan.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV_Pencairan.Visible = False
        ' 
        ' User_
        ' 
        User_.HeaderText = "User"
        User_.Name = "User_"
        User_.ReadOnly = True
        User_.SortMode = DataGridViewColumnSortMode.NotSortable
        User_.Visible = False
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
        lbl_JudulForm.Size = New Size(275, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Buku Bank Garansi"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_LihatJurnalTransaksi
        ' 
        btn_LihatJurnalTransaksi.Location = New Point(232, 100)
        btn_LihatJurnalTransaksi.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnalTransaksi.Name = "btn_LihatJurnalTransaksi"
        btn_LihatJurnalTransaksi.Size = New Size(169, 40)
        btn_LihatJurnalTransaksi.TabIndex = 10031
        btn_LihatJurnalTransaksi.Text = "Lihat Jurnal Transaksi"
        btn_LihatJurnalTransaksi.UseVisualStyleBackColor = True
        ' 
        ' btn_LihatJurnalPencairan
        ' 
        btn_LihatJurnalPencairan.Location = New Point(408, 100)
        btn_LihatJurnalPencairan.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnalPencairan.Name = "btn_LihatJurnalPencairan"
        btn_LihatJurnalPencairan.Size = New Size(169, 40)
        btn_LihatJurnalPencairan.TabIndex = 10032
        btn_LihatJurnalPencairan.Text = "Lihat Jurnal Pencairan"
        btn_LihatJurnalPencairan.UseVisualStyleBackColor = True
        ' 
        ' btn_Cairkan
        ' 
        btn_Cairkan.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Cairkan.Location = New Point(1307, 92)
        btn_Cairkan.Margin = New Padding(4, 3, 4, 3)
        btn_Cairkan.Name = "btn_Cairkan"
        btn_Cairkan.Size = New Size(97, 40)
        btn_Cairkan.TabIndex = 10033
        btn_Cairkan.Text = "Cairkan"
        btn_Cairkan.UseVisualStyleBackColor = True
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(766, 86)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10073
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuBankGaransi
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Export)
        Controls.Add(btn_Cairkan)
        Controls.Add(btn_LihatJurnalPencairan)
        Controls.Add(btn_LihatJurnalTransaksi)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuBankGaransi"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Bank Garansi"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents btn_LihatJurnalTransaksi As Button
    Friend WithEvents btn_LihatJurnalPencairan As Button
    Friend WithEvents btn_Cairkan As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPBG As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Kontrak As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Bank As DataGridViewTextBoxColumn
    Friend WithEvents Keperluan_ As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Biaya_Provisi As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Pencairan As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Pencairan As DataGridViewTextBoxColumn
    Friend WithEvents User_ As DataGridViewTextBoxColumn
    Friend WithEvents btn_Export As Button
End Class
