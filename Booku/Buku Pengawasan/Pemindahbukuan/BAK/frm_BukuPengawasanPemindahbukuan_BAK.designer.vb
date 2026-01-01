<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_BukuPengawasanPemindahbukuan_BAK
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        btn_Tambah = New Button()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        DataTabelUtama = New DataGridView()
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        cmb_DariBuku = New ComboBox()
        cmb_KeBuku = New ComboBox()
        Label1 = New Label()
        Label2 = New Label()
        txt_TotalTransaksiPBk = New TextBox()
        Label3 = New Label()
        btn_LihatJurnal = New Button()
        btn_Ajukan = New Button()
        btn_Export = New Button()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_BPPB = New DataGridViewTextBoxColumn()
        Tanggal_BPPB = New DataGridViewTextBoxColumn()
        Nomor_KK = New DataGridViewTextBoxColumn()
        COA_Kredit = New DataGridViewTextBoxColumn()
        COA_Debet = New DataGridViewTextBoxColumn()
        Dari_Buku = New DataGridViewTextBoxColumn()
        Ke_Buku = New DataGridViewTextBoxColumn()
        Penanggungjawab_ = New DataGridViewTextBoxColumn()
        Tanggal_Transaksi = New DataGridViewTextBoxColumn()
        Jumlah_Transaksi = New DataGridViewTextBoxColumn()
        Uraian_Transaksi = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        User_ = New DataGridViewTextBoxColumn()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(812, 98)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 351
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Enabled = False
        btn_Hapus.Location = New Point(1019, 98)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 352
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Enabled = False
        btn_Edit.Location = New Point(916, 98)
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPPB, Tanggal_BPPB, Nomor_KK, COA_Kredit, COA_Debet, Dari_Buku, Ke_Buku, Penanggungjawab_, Tanggal_Transaksi, Jumlah_Transaksi, Uraian_Transaksi, Nomor_JV, User_})
        DataTabelUtama.Location = New Point(14, 145)
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
        btn_Refresh.Location = New Point(14, 63)
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
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(511, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Buku Pengawasan Pemindahbukuan"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' cmb_DariBuku
        ' 
        cmb_DariBuku.FormattingEnabled = True
        cmb_DariBuku.Location = New Point(134, 112)
        cmb_DariBuku.Margin = New Padding(4, 3, 4, 3)
        cmb_DariBuku.Name = "cmb_DariBuku"
        cmb_DariBuku.Size = New Size(226, 23)
        cmb_DariBuku.TabIndex = 10031
        ' 
        ' cmb_KeBuku
        ' 
        cmb_KeBuku.FormattingEnabled = True
        cmb_KeBuku.Location = New Point(381, 112)
        cmb_KeBuku.Margin = New Padding(4, 3, 4, 3)
        cmb_KeBuku.Name = "cmb_KeBuku"
        cmb_KeBuku.Size = New Size(226, 23)
        cmb_KeBuku.TabIndex = 10032
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(131, 93)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(64, 15)
        Label1.TabIndex = 10033
        Label1.Text = "Dari Buku :"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(377, 93)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(56, 15)
        Label2.TabIndex = 10034
        Label2.Text = "Ke Buku :"
        ' 
        ' txt_TotalTransaksiPBk
        ' 
        txt_TotalTransaksiPBk.Location = New Point(966, 35)
        txt_TotalTransaksiPBk.Margin = New Padding(4, 3, 4, 3)
        txt_TotalTransaksiPBk.Name = "txt_TotalTransaksiPBk"
        txt_TotalTransaksiPBk.Size = New Size(139, 23)
        txt_TotalTransaksiPBk.TabIndex = 10035
        txt_TotalTransaksiPBk.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(962, 13)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(90, 15)
        Label3.TabIndex = 10036
        Label3.Text = "Total Transaksi :"
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Location = New Point(630, 18)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 40)
        btn_LihatJurnal.TabIndex = 10037
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' btn_Ajukan
        ' 
        btn_Ajukan.Location = New Point(653, 98)
        btn_Ajukan.Margin = New Padding(4, 3, 4, 3)
        btn_Ajukan.Name = "btn_Ajukan"
        btn_Ajukan.Size = New Size(97, 40)
        btn_Ajukan.TabIndex = 10038
        btn_Ajukan.Text = "Ajukan"
        btn_Ajukan.UseVisualStyleBackColor = True
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(744, 18)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 40)
        btn_Export.TabIndex = 10073
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_Urut.HeaderText = "Nomor Urut"
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Visible = False
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
        ' Nomor_BPPB
        ' 
        Nomor_BPPB.HeaderText = "Nomor Pemindahbukuan"
        Nomor_BPPB.Name = "Nomor_BPPB"
        Nomor_BPPB.ReadOnly = True
        Nomor_BPPB.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPPB.Width = 123
        ' 
        ' Tanggal_BPPB
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_BPPB.DefaultCellStyle = DataGridViewCellStyle2
        Tanggal_BPPB.HeaderText = "Tanggal Pengajuan"
        Tanggal_BPPB.Name = "Tanggal_BPPB"
        Tanggal_BPPB.ReadOnly = True
        Tanggal_BPPB.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_BPPB.Width = 63
        ' 
        ' Nomor_KK
        ' 
        Nomor_KK.HeaderText = "Nomor KK"
        Nomor_KK.Name = "Nomor_KK"
        Nomor_KK.ReadOnly = True
        Nomor_KK.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' COA_Kredit
        ' 
        COA_Kredit.HeaderText = "COA_Kredit"
        COA_Kredit.Name = "COA_Kredit"
        COA_Kredit.ReadOnly = True
        COA_Kredit.SortMode = DataGridViewColumnSortMode.NotSortable
        COA_Kredit.Visible = False
        ' 
        ' COA_Debet
        ' 
        COA_Debet.HeaderText = "COA Debet"
        COA_Debet.Name = "COA_Debet"
        COA_Debet.ReadOnly = True
        COA_Debet.SortMode = DataGridViewColumnSortMode.NotSortable
        COA_Debet.Visible = False
        ' 
        ' Dari_Buku
        ' 
        Dari_Buku.HeaderText = "Dari Buku"
        Dari_Buku.Name = "Dari_Buku"
        Dari_Buku.ReadOnly = True
        Dari_Buku.SortMode = DataGridViewColumnSortMode.NotSortable
        Dari_Buku.Width = 210
        ' 
        ' Ke_Buku
        ' 
        Ke_Buku.HeaderText = "Ke Buku"
        Ke_Buku.Name = "Ke_Buku"
        Ke_Buku.ReadOnly = True
        Ke_Buku.SortMode = DataGridViewColumnSortMode.NotSortable
        Ke_Buku.Width = 210
        ' 
        ' Penanggungjawab_
        ' 
        Penanggungjawab_.HeaderText = "Penanggungjawab"
        Penanggungjawab_.Name = "Penanggungjawab_"
        Penanggungjawab_.ReadOnly = True
        Penanggungjawab_.SortMode = DataGridViewColumnSortMode.NotSortable
        Penanggungjawab_.Visible = False
        ' 
        ' Tanggal_Transaksi
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        Tanggal_Transaksi.DefaultCellStyle = DataGridViewCellStyle3
        Tanggal_Transaksi.HeaderText = "Tanggal Transaksi"
        Tanggal_Transaksi.Name = "Tanggal_Transaksi"
        Tanggal_Transaksi.ReadOnly = True
        Tanggal_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Transaksi.Width = 72
        ' 
        ' Jumlah_Transaksi
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Jumlah_Transaksi.DefaultCellStyle = DataGridViewCellStyle4
        Jumlah_Transaksi.HeaderText = "Jumlah Transaksi"
        Jumlah_Transaksi.Name = "Jumlah_Transaksi"
        Jumlah_Transaksi.ReadOnly = True
        Jumlah_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Transaksi.Width = 81
        ' 
        ' Uraian_Transaksi
        ' 
        Uraian_Transaksi.HeaderText = "Uraian"
        Uraian_Transaksi.Name = "Uraian_Transaksi"
        Uraian_Transaksi.ReadOnly = True
        Uraian_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Uraian_Transaksi.Width = 402
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
        ' frm_BukuPengawasanPemindahbukuan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 797)
        Controls.Add(btn_Export)
        Controls.Add(btn_Ajukan)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(Label3)
        Controls.Add(txt_TotalTransaksiPBk)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(cmb_KeBuku)
        Controls.Add(cmb_DariBuku)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanPemindahbukuan"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Pemindahbukuan"
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
    Friend WithEvents cmb_DariBuku As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_KeBuku As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_TotalTransaksiPBk As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btn_LihatJurnal As System.Windows.Forms.Button
    Friend WithEvents btn_Ajukan As Button
    Friend WithEvents btn_Export As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPPB As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_BPPB As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_KK As DataGridViewTextBoxColumn
    Friend WithEvents COA_Kredit As DataGridViewTextBoxColumn
    Friend WithEvents COA_Debet As DataGridViewTextBoxColumn
    Friend WithEvents Dari_Buku As DataGridViewTextBoxColumn
    Friend WithEvents Ke_Buku As DataGridViewTextBoxColumn
    Friend WithEvents Penanggungjawab_ As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Uraian_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents User_ As DataGridViewTextBoxColumn
End Class
