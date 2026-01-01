<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_ReturPenjualan
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
        lbl_JudulForm = New Label()
        btn_Hapus = New Button()
        btn_Tambah = New Button()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Angka_Retur = New DataGridViewTextBoxColumn()
        Nomor_Retur = New DataGridViewTextBoxColumn()
        Tanggal_Retur = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Kode_Project = New DataGridViewTextBoxColumn()
        Kode_Customer = New DataGridViewTextBoxColumn()
        Nama_Customer = New DataGridViewTextBoxColumn()
        Jumlah_Harga = New DataGridViewTextBoxColumn()
        Diskon_Persen = New DataGridViewTextBoxColumn()
        Diskon_Rp = New DataGridViewTextBoxColumn()
        Dasar_Pengenaan_Pajak = New DataGridViewTextBoxColumn()
        Jenis_PPN = New DataGridViewTextBoxColumn()
        PPN_ = New DataGridViewTextBoxColumn()
        Total_Retur = New DataGridViewTextBoxColumn()
        Catatan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        btn_Edit = New Button()
        btn_LihatJurnal = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(494, 32)
        lbl_JudulForm.TabIndex = 10053
        lbl_JudulForm.Text = "Buku Pengawasan Retur Penjualan"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Location = New Point(746, 98)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10050
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(539, 98)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 10049
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10048
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Angka_Retur, Nomor_Retur, Tanggal_Retur, Nomor_Invoice, Tanggal_Invoice, Kode_Project, Kode_Customer, Nama_Customer, Jumlah_Harga, Diskon_Persen, Diskon_Rp, Dasar_Pengenaan_Pajak, Jenis_PPN, PPN_, Total_Retur, Catatan_, Nomor_JV})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 493)
        DataTabelUtama.TabIndex = 10052
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
        ' Angka_Retur
        ' 
        Angka_Retur.HeaderText = "Angka Retur"
        Angka_Retur.Name = "Angka_Retur"
        Angka_Retur.ReadOnly = True
        Angka_Retur.SortMode = DataGridViewColumnSortMode.NotSortable
        Angka_Retur.Visible = False
        ' 
        ' Nomor_Retur
        ' 
        Nomor_Retur.HeaderText = "Nomor Retur"
        Nomor_Retur.Name = "Nomor_Retur"
        Nomor_Retur.ReadOnly = True
        Nomor_Retur.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Retur.Width = 87
        ' 
        ' Tanggal_Retur
        ' 
        Tanggal_Retur.HeaderText = "Tanggal Retur"
        Tanggal_Retur.Name = "Tanggal_Retur"
        Tanggal_Retur.ReadOnly = True
        Tanggal_Retur.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Retur.Width = 63
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
        ' Kode_Project
        ' 
        Kode_Project.HeaderText = "Kode Project"
        Kode_Project.Name = "Kode_Project"
        Kode_Project.ReadOnly = True
        Kode_Project.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Project.Width = 99
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
        ' Diskon_Persen
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        Diskon_Persen.DefaultCellStyle = DataGridViewCellStyle3
        Diskon_Persen.HeaderText = "Diskon (%)"
        Diskon_Persen.Name = "Diskon_Persen"
        Diskon_Persen.ReadOnly = True
        Diskon_Persen.SortMode = DataGridViewColumnSortMode.NotSortable
        Diskon_Persen.Visible = False
        Diskon_Persen.Width = 45
        ' 
        ' Diskon_Rp
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Diskon_Rp.DefaultCellStyle = DataGridViewCellStyle4
        Diskon_Rp.HeaderText = "Diskon (Rp)"
        Diskon_Rp.Name = "Diskon_Rp"
        Diskon_Rp.ReadOnly = True
        Diskon_Rp.SortMode = DataGridViewColumnSortMode.NotSortable
        Diskon_Rp.Width = 81
        ' 
        ' Dasar_Pengenaan_Pajak
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Dasar_Pengenaan_Pajak.DefaultCellStyle = DataGridViewCellStyle5
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
        ' PPN_
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        PPN_.DefaultCellStyle = DataGridViewCellStyle6
        PPN_.HeaderText = "PPN"
        PPN_.Name = "PPN_"
        PPN_.ReadOnly = True
        PPN_.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Total_Retur
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        Total_Retur.DefaultCellStyle = DataGridViewCellStyle7
        Total_Retur.HeaderText = "Total Retur"
        Total_Retur.Name = "Total_Retur"
        Total_Retur.ReadOnly = True
        Total_Retur.SortMode = DataGridViewColumnSortMode.NotSortable
        Total_Retur.Width = 81
        ' 
        ' Catatan_
        ' 
        Catatan_.HeaderText = "Catatan"
        Catatan_.Name = "Catatan_"
        Catatan_.ReadOnly = True
        Catatan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Catatan_.Width = 210
        ' 
        ' Nomor_JV
        ' 
        Nomor_JV.HeaderText = "Nomor JV"
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.Visible = False
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Location = New Point(643, 98)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10051
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(746, 12)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 58)
        btn_LihatJurnal.TabIndex = 10084
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' frm_ReturPenjualan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 682)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(btn_Edit)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_ReturPenjualan"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Retur Penjualan"
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Angka_Retur As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Retur As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Retur As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Harga As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Persen As DataGridViewTextBoxColumn
    Friend WithEvents Diskon_Rp As DataGridViewTextBoxColumn
    Friend WithEvents Dasar_Pengenaan_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_PPN As DataGridViewTextBoxColumn
    Friend WithEvents PPN_ As DataGridViewTextBoxColumn
    Friend WithEvents Total_Retur As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
End Class
