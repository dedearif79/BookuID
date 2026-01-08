<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_BukuPengawasanBuktiPotongPPh_Prepaid
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
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        btn_Input = New Button()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Sumber_Data = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nomor_Patokan = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Kode_Customer = New DataGridViewTextBoxColumn()
        Nama_Customer = New DataGridViewTextBoxColumn()
        DPP_ = New DataGridViewTextBoxColumn()
        Jenis_PPh = New DataGridViewTextBoxColumn()
        Jumlah_PPh = New DataGridViewTextBoxColumn()
        Tanggal_Dipotong = New DataGridViewTextBoxColumn()
        Nomor_Bukti_Potong = New DataGridViewTextBoxColumn()
        Tanggal_Bukti_Potong = New DataGridViewTextBoxColumn()
        Keterangan_ = New DataGridViewTextBoxColumn()
        Nomor_JV = New DataGridViewTextBoxColumn()
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        btn_LihatJurnal = New Button()
        Label15 = New Label()
        cmb_JenisPPh = New ComboBox()
        Label1 = New Label()
        cmb_SumberData = New ComboBox()
        Label2 = New Label()
        cmb_BuktiPotong = New ComboBox()
        btn_Export = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Input
        ' 
        btn_Input.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Input.Location = New Point(1203, 92)
        btn_Input.Margin = New Padding(4, 3, 4, 3)
        btn_Input.Name = "btn_Input"
        btn_Input.Size = New Size(97, 40)
        btn_Input.TabIndex = 351
        btn_Input.Text = "Input"
        btn_Input.UseVisualStyleBackColor = True
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Sumber_Data, Nomor_ID, Nomor_Patokan, Nomor_Invoice, Tanggal_Invoice, Nomor_Faktur_Pajak, Kode_Customer, Nama_Customer, DPP_, Jenis_PPh, Jumlah_PPh, Tanggal_Dipotong, Nomor_Bukti_Potong, Tanggal_Bukti_Potong, Keterangan_, Nomor_JV})
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
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle4
        Nomor_Urut.HeaderText = "Nomor Urut"
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 45
        ' 
        ' Sumber_Data
        ' 
        Sumber_Data.HeaderText = "Sumber Data"
        Sumber_Data.Name = "Sumber_Data"
        Sumber_Data.ReadOnly = True
        Sumber_Data.SortMode = DataGridViewColumnSortMode.NotSortable
        Sumber_Data.Width = 111
        ' 
        ' Nomor_ID
        ' 
        Nomor_ID.HeaderText = "Nomor ID"
        Nomor_ID.Name = "Nomor_ID"
        Nomor_ID.ReadOnly = True
        Nomor_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID.Visible = False
        Nomor_ID.Width = 45
        ' 
        ' Nomor_Patokan
        ' 
        Nomor_Patokan.HeaderText = "Nomor Patokan"
        Nomor_Patokan.Name = "Nomor_Patokan"
        Nomor_Patokan.ReadOnly = True
        Nomor_Patokan.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Patokan.Visible = False
        Nomor_Patokan.Width = 123
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.ReadOnly = True
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 111
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
        Nomor_Faktur_Pajak.Width = 111
        ' 
        ' Kode_Customer
        ' 
        Kode_Customer.HeaderText = "Kode Customer"
        Kode_Customer.Name = "Kode_Customer"
        Kode_Customer.ReadOnly = True
        Kode_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Customer.Visible = False
        Kode_Customer.Width = 72
        ' 
        ' Nama_Customer
        ' 
        Nama_Customer.HeaderText = "Nama Customer"
        Nama_Customer.Name = "Nama_Customer"
        Nama_Customer.ReadOnly = True
        Nama_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Customer.Width = 171
        ' 
        ' DPP_
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DPP_.DefaultCellStyle = DataGridViewCellStyle5
        DPP_.HeaderText = "DPP"
        DPP_.Name = "DPP_"
        DPP_.ReadOnly = True
        DPP_.SortMode = DataGridViewColumnSortMode.NotSortable
        DPP_.Width = 81
        ' 
        ' Jenis_PPh
        ' 
        Jenis_PPh.HeaderText = "Jenis PPh"
        Jenis_PPh.Name = "Jenis_PPh"
        Jenis_PPh.ReadOnly = True
        Jenis_PPh.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_PPh.Width = 63
        ' 
        ' Jumlah_PPh
        ' 
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Jumlah_PPh.DefaultCellStyle = DataGridViewCellStyle6
        Jumlah_PPh.HeaderText = "Jumlah PPh"
        Jumlah_PPh.Name = "Jumlah_PPh"
        Jumlah_PPh.ReadOnly = True
        Jumlah_PPh.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_PPh.Width = 81
        ' 
        ' Tanggal_Dipotong
        ' 
        Tanggal_Dipotong.HeaderText = "Tanggal Dipotong"
        Tanggal_Dipotong.Name = "Tanggal_Dipotong"
        Tanggal_Dipotong.ReadOnly = True
        Tanggal_Dipotong.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Dipotong.Width = 63
        ' 
        ' Nomor_Bukti_Potong
        ' 
        Nomor_Bukti_Potong.HeaderText = "Nomor Bukti Potong"
        Nomor_Bukti_Potong.Name = "Nomor_Bukti_Potong"
        Nomor_Bukti_Potong.ReadOnly = True
        Nomor_Bukti_Potong.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Bukti_Potong.Width = 123
        ' 
        ' Tanggal_Bukti_Potong
        ' 
        Tanggal_Bukti_Potong.HeaderText = "Tanggal Bukti Potong"
        Tanggal_Bukti_Potong.Name = "Tanggal_Bukti_Potong"
        Tanggal_Bukti_Potong.ReadOnly = True
        Tanggal_Bukti_Potong.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Bukti_Potong.Width = 63
        ' 
        ' Keterangan_
        ' 
        Keterangan_.HeaderText = "Keterangan"
        Keterangan_.Name = "Keterangan_"
        Keterangan_.ReadOnly = True
        Keterangan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Keterangan_.Width = 252
        ' 
        ' Nomor_JV
        ' 
        Nomor_JV.HeaderText = "Nomor JV"
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV.Visible = False
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
        lbl_JudulForm.Size = New Size(647, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Buku Pengawasan Bukti Potong PPh (Prepaid)"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Enabled = False
        btn_LihatJurnal.Location = New Point(814, 93)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(97, 39)
        btn_LihatJurnal.TabIndex = 10206
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(345, 96)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(60, 13)
        Label15.TabIndex = 10208
        Label15.Text = "Jenis PPh :"
        ' 
        ' cmb_JenisPPh
        ' 
        cmb_JenisPPh.FormattingEnabled = True
        cmb_JenisPPh.Location = New Point(349, 117)
        cmb_JenisPPh.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisPPh.Name = "cmb_JenisPPh"
        cmb_JenisPPh.Size = New Size(89, 23)
        cmb_JenisPPh.TabIndex = 10207
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(172, 96)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(75, 13)
        Label1.TabIndex = 10210
        Label1.Text = "Sumber Data :"
        ' 
        ' cmb_SumberData
        ' 
        cmb_SumberData.FormattingEnabled = True
        cmb_SumberData.Location = New Point(175, 117)
        cmb_SumberData.Margin = New Padding(4, 3, 4, 3)
        cmb_SumberData.Name = "cmb_SumberData"
        cmb_SumberData.Size = New Size(143, 23)
        cmb_SumberData.TabIndex = 10209
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(463, 96)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(74, 13)
        Label2.TabIndex = 10212
        Label2.Text = "Bukti Potong :"
        ' 
        ' cmb_BuktiPotong
        ' 
        cmb_BuktiPotong.FormattingEnabled = True
        cmb_BuktiPotong.Location = New Point(467, 117)
        cmb_BuktiPotong.Margin = New Padding(4, 3, 4, 3)
        cmb_BuktiPotong.Name = "cmb_BuktiPotong"
        cmb_BuktiPotong.Size = New Size(112, 23)
        cmb_BuktiPotong.TabIndex = 10211
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(993, 80)
        btn_Export.Margin = New Padding(4)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(116, 52)
        btn_Export.TabIndex = 10213
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' frm_BukuPengawasanBuktiPotongPPh_Prepaid
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Export)
        Controls.Add(Label2)
        Controls.Add(cmb_BuktiPotong)
        Controls.Add(Label1)
        Controls.Add(cmb_SumberData)
        Controls.Add(Label15)
        Controls.Add(cmb_JenisPPh)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Input)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_BukuPengawasanBuktiPotongPPh_Prepaid"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Pengawasan Bukti Potong PPh (Prepaid)"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Input As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents cmb_JenisPPh As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmb_SumberData As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmb_BuktiPotong As ComboBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Sumber_Data As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Patokan As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Customer As DataGridViewTextBoxColumn
    Friend WithEvents DPP_ As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_PPh As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_PPh As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Dipotong As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Bukti_Potong As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bukti_Potong As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents btn_Export As Button
End Class
