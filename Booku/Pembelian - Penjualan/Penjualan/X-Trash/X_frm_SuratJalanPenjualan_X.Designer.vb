<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_SuratJalanPenjualan_X
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
        btn_Edit = New Button()
        btn_Hapus = New Button()
        btn_Tambah = New Button()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Angka_SJ = New DataGridViewTextBoxColumn()
        Nomor_SJ = New DataGridViewTextBoxColumn()
        Tanggal_SJ = New DataGridViewTextBoxColumn()
        Nomor_PO = New DataGridViewTextBoxColumn()
        Tanggal_PO = New DataGridViewTextBoxColumn()
        Plat_Nomor = New DataGridViewTextBoxColumn()
        Nama_Supir = New DataGridViewTextBoxColumn()
        Nama_Pengirim = New DataGridViewTextBoxColumn()
        Kode_Project = New DataGridViewTextBoxColumn()
        Kode_Customer = New DataGridViewTextBoxColumn()
        Nama_Customer = New DataGridViewTextBoxColumn()
        Catatan_ = New DataGridViewTextBoxColumn()
        Kontrol_ = New DataGridViewTextBoxColumn()
        Nama_Penerima = New DataGridViewTextBoxColumn()
        Tanggal_Diterima = New DataGridViewTextBoxColumn()
        lbl_JudulForm = New Label()
        btn_Cetak = New Button()
        btn_Pratinjau = New Button()
        Label15 = New Label()
        cmb_Kontrol = New ComboBox()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Location = New Point(713, 91)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10057
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Location = New Point(816, 91)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10056
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(609, 91)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 10055
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10054
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Angka_SJ, Nomor_SJ, Tanggal_SJ, Nomor_PO, Tanggal_PO, Plat_Nomor, Nama_Supir, Nama_Pengirim, Kode_Project, Kode_Customer, Nama_Customer, Catatan_, Kontrol_, Nama_Penerima, Tanggal_Diterima})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1316, 493)
        DataTabelUtama.TabIndex = 10058
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
        ' Angka_SJ
        ' 
        Angka_SJ.HeaderText = "Angka SJ"
        Angka_SJ.Name = "Angka_SJ"
        Angka_SJ.ReadOnly = True
        Angka_SJ.SortMode = DataGridViewColumnSortMode.NotSortable
        Angka_SJ.Visible = False
        ' 
        ' Nomor_SJ
        ' 
        Nomor_SJ.HeaderText = "Nomor Surat Jalan"
        Nomor_SJ.Name = "Nomor_SJ"
        Nomor_SJ.ReadOnly = True
        Nomor_SJ.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_SJ.Width = 87
        ' 
        ' Tanggal_SJ
        ' 
        Tanggal_SJ.HeaderText = "Tanggal Surat Jalan"
        Tanggal_SJ.Name = "Tanggal_SJ"
        Tanggal_SJ.ReadOnly = True
        Tanggal_SJ.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_SJ.Width = 63
        ' 
        ' Nomor_PO
        ' 
        Nomor_PO.HeaderText = "Nomor PO"
        Nomor_PO.Name = "Nomor_PO"
        Nomor_PO.ReadOnly = True
        Nomor_PO.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_PO.Width = 123
        ' 
        ' Tanggal_PO
        ' 
        Tanggal_PO.HeaderText = "Tanggal PO"
        Tanggal_PO.Name = "Tanggal_PO"
        Tanggal_PO.ReadOnly = True
        Tanggal_PO.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_PO.Width = 123
        ' 
        ' Plat_Nomor
        ' 
        Plat_Nomor.HeaderText = "Plat Nomor"
        Plat_Nomor.Name = "Plat_Nomor"
        Plat_Nomor.ReadOnly = True
        Plat_Nomor.SortMode = DataGridViewColumnSortMode.NotSortable
        Plat_Nomor.Width = 72
        ' 
        ' Nama_Supir
        ' 
        Nama_Supir.HeaderText = "Nama Supir"
        Nama_Supir.Name = "Nama_Supir"
        Nama_Supir.ReadOnly = True
        Nama_Supir.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Supir.Width = 123
        ' 
        ' Nama_Pengirim
        ' 
        Nama_Pengirim.HeaderText = "Nama Pengirim"
        Nama_Pengirim.Name = "Nama_Pengirim"
        Nama_Pengirim.ReadOnly = True
        Nama_Pengirim.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Pengirim.Width = 123
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
        ' Catatan_
        ' 
        Catatan_.HeaderText = "Catatan"
        Catatan_.Name = "Catatan_"
        Catatan_.ReadOnly = True
        Catatan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Catatan_.Width = 330
        ' 
        ' Kontrol_
        ' 
        Kontrol_.HeaderText = "Kontrol"
        Kontrol_.Name = "Kontrol_"
        Kontrol_.ReadOnly = True
        Kontrol_.SortMode = DataGridViewColumnSortMode.NotSortable
        Kontrol_.Width = 63
        ' 
        ' Nama_Penerima
        ' 
        Nama_Penerima.HeaderText = "Nama Penerima"
        Nama_Penerima.Name = "Nama_Penerima"
        Nama_Penerima.ReadOnly = True
        Nama_Penerima.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Penerima.Width = 123
        ' 
        ' Tanggal_Diterima
        ' 
        Tanggal_Diterima.HeaderText = "Tanggal Diterima"
        Tanggal_Diterima.Name = "Tanggal_Diterima"
        Tanggal_Diterima.ReadOnly = True
        Tanggal_Diterima.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Diterima.Width = 63
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 12)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(169, 32)
        lbl_JudulForm.TabIndex = 10059
        lbl_JudulForm.Text = "Surat Jalan"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Cetak
        ' 
        btn_Cetak.Location = New Point(816, 12)
        btn_Cetak.Margin = New Padding(4, 3, 4, 3)
        btn_Cetak.Name = "btn_Cetak"
        btn_Cetak.Size = New Size(97, 58)
        btn_Cetak.TabIndex = 10060
        btn_Cetak.Text = "Cetak"
        btn_Cetak.UseVisualStyleBackColor = True
        ' 
        ' btn_Pratinjau
        ' 
        btn_Pratinjau.Location = New Point(713, 12)
        btn_Pratinjau.Margin = New Padding(4, 3, 4, 3)
        btn_Pratinjau.Name = "btn_Pratinjau"
        btn_Pratinjau.Size = New Size(97, 58)
        btn_Pratinjau.TabIndex = 10061
        btn_Pratinjau.Text = "Pratinjau"
        btn_Pratinjau.UseVisualStyleBackColor = True
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(264, 88)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(46, 13)
        Label15.TabIndex = 10186
        Label15.Text = "Kontrol :"
        ' 
        ' cmb_Kontrol
        ' 
        cmb_Kontrol.FormattingEnabled = True
        cmb_Kontrol.Location = New Point(268, 108)
        cmb_Kontrol.Margin = New Padding(4, 3, 4, 3)
        cmb_Kontrol.Name = "cmb_Kontrol"
        cmb_Kontrol.Size = New Size(87, 23)
        cmb_Kontrol.TabIndex = 10185
        ' 
        ' frm_SuratJalanPenjualan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1344, 682)
        Controls.Add(Label15)
        Controls.Add(cmb_Kontrol)
        Controls.Add(btn_Cetak)
        Controls.Add(btn_Pratinjau)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(lbl_JudulForm)
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_SuratJalanPenjualan"
        StartPosition = FormStartPosition.CenterParent
        Text = "Surat Jalan"
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
    Friend WithEvents btn_Cetak As Button
    Friend WithEvents btn_Pratinjau As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Angka_SJ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_SJ As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_PO As DataGridViewTextBoxColumn
    Friend WithEvents Plat_Nomor As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Supir As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Pengirim As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Kontrol_ As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Penerima As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Diterima As DataGridViewTextBoxColumn
    Friend WithEvents Label15 As Label
    Friend WithEvents cmb_Kontrol As ComboBox
End Class
