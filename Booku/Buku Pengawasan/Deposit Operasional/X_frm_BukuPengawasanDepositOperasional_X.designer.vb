<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_BukuPengawasanDepositOperasional_X
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
        btn_Tambah = New Button()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        DataTabelUtama = New DataGridView()
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        btn_LihatJurnal = New Button()
        btn_Export = New Button()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_BPDO = New DataGridViewTextBoxColumn()
        Nomor_Bukti = New DataGridViewTextBoxColumn()
        Tanggal_Bukti = New DataGridViewTextBoxColumn()
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Kode_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Nama_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Kode_Customer = New DataGridViewTextBoxColumn()
        Nama_Customer = New DataGridViewTextBoxColumn()
        Jumlah_Transaksi = New DataGridViewTextBoxColumn()
        Jumlah_Talangan = New DataGridViewTextBoxColumn()
        Sisa_Talangan = New DataGridViewTextBoxColumn()
        Jumlah_Reimburse = New DataGridViewTextBoxColumn()
        Potongan_Reimburse = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        User_ = New DataGridViewTextBoxColumn()
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
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nomor_BPDO, Nomor_Bukti, Tanggal_Bukti, Nomor_Faktur_Pajak, Kode_Lawan_Transaksi, Nama_Lawan_Transaksi, Kode_Customer, Nama_Customer, Jumlah_Transaksi, Jumlah_Talangan, Sisa_Talangan, Jumlah_Reimburse, Potongan_Reimburse, Keterangan_, Nomor_JV, User_})
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
        lbl_JudulForm.Size = New Size(552, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Buku Pengawasan Deposit Operasional"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(695, 93)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 39)
        btn_LihatJurnal.TabIndex = 10215
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(918, 77)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10216
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
        ' Nomor_BPDO
        ' 
        Nomor_BPDO.HeaderText = "Nomor BPDO"
        Nomor_BPDO.Name = "Nomor_BPDO"
        Nomor_BPDO.ReadOnly = True
        Nomor_BPDO.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BPDO.Visible = False
        Nomor_BPDO.Width = 99
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
        ' Nomor_Faktur_Pajak
        ' 
        Nomor_Faktur_Pajak.HeaderText = "Nomor Faktur Pajak"
        Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Nomor_Faktur_Pajak.ReadOnly = True
        Nomor_Faktur_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Faktur_Pajak.Width = 123
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
        ' Kode_Customer
        ' 
        Kode_Customer.HeaderText = "Kode Customer"
        Kode_Customer.Name = "Kode_Customer"
        Kode_Customer.ReadOnly = True
        Kode_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Customer.Visible = False
        Kode_Customer.Width = 33
        ' 
        ' Nama_Customer
        ' 
        Nama_Customer.HeaderText = "Nama Customer"
        Nama_Customer.Name = "Nama_Customer"
        Nama_Customer.ReadOnly = True
        Nama_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Customer.Width = 150
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
        ' Jumlah_Talangan
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Jumlah_Talangan.DefaultCellStyle = DataGridViewCellStyle4
        Jumlah_Talangan.HeaderText = "Jumlah Talangan"
        Jumlah_Talangan.Name = "Jumlah_Talangan"
        Jumlah_Talangan.ReadOnly = True
        Jumlah_Talangan.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Talangan.Width = 81
        ' 
        ' Sisa_Talangan
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Sisa_Talangan.DefaultCellStyle = DataGridViewCellStyle5
        Sisa_Talangan.HeaderText = "Sisa Talangan"
        Sisa_Talangan.Name = "Sisa_Talangan"
        Sisa_Talangan.ReadOnly = True
        Sisa_Talangan.SortMode = DataGridViewColumnSortMode.NotSortable
        Sisa_Talangan.Width = 81
        ' 
        ' Jumlah_Reimburse
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Jumlah_Reimburse.DefaultCellStyle = DataGridViewCellStyle6
        Jumlah_Reimburse.HeaderText = "Jumlah Reimburse"
        Jumlah_Reimburse.Name = "Jumlah_Reimburse"
        Jumlah_Reimburse.ReadOnly = True
        Jumlah_Reimburse.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Reimburse.Width = 81
        ' 
        ' Potongan_Reimburse
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Potongan_Reimburse.DefaultCellStyle = DataGridViewCellStyle7
        Potongan_Reimburse.HeaderText = "Potongan Reimburse"
        Potongan_Reimburse.Name = "Potongan_Reimburse"
        Potongan_Reimburse.ReadOnly = True
        Potongan_Reimburse.SortMode = DataGridViewColumnSortMode.NotSortable
        Potongan_Reimburse.Width = 81
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
        ' frm_BukuPengawasanDepositOperasional
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Export)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanDepositOperasional"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Deposit Operasional"
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
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents btn_Export As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BPDO As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Bukti As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bukti As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Talangan As DataGridViewTextBoxColumn
    Friend WithEvents Sisa_Talangan As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Reimburse As DataGridViewTextBoxColumn
    Friend WithEvents Potongan_Reimburse As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents User_ As DataGridViewTextBoxColumn
End Class
