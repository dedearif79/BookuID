<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_BASTPembelian_X
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
        lbl_JudulForm = New Label()
        Label15 = New Label()
        btn_Edit = New Button()
        btn_Hapus = New Button()
        btn_Tambah = New Button()
        btn_Refresh = New Button()
        DataTabelUtama = New DataGridView()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Angka_BAST = New DataGridViewTextBoxColumn()
        Nomor_BAST = New DataGridViewTextBoxColumn()
        Tanggal_BAST = New DataGridViewTextBoxColumn()
        Nomor_PO = New DataGridViewTextBoxColumn()
        Tanggal_PO = New DataGridViewTextBoxColumn()
        Yang_Menyerahkan = New DataGridViewTextBoxColumn()
        Kode_Project = New DataGridViewTextBoxColumn()
        Kode_Supplier = New DataGridViewTextBoxColumn()
        Nama_Supplier = New DataGridViewTextBoxColumn()
        Catatan_ = New DataGridViewTextBoxColumn()
        Kontrol_ = New DataGridViewTextBoxColumn()
        Yang_Menerima = New DataGridViewTextBoxColumn()
        Tanggal_Diterima = New DataGridViewTextBoxColumn()
        cmb_Kontrol = New ComboBox()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(11, 11)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(625, 32)
        lbl_JudulForm.TabIndex = 10202
        lbl_JudulForm.Text = "Berita Acara Serah Terima Pekerjaan (BAST)"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(307, 93)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(46, 13)
        Label15.TabIndex = 10206
        Label15.Text = "Kontrol :"
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Location = New Point(840, 97)
        btn_Edit.Margin = New Padding(4)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10200
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Location = New Point(944, 97)
        btn_Hapus.Margin = New Padding(4)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10199
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(737, 97)
        btn_Tambah.Margin = New Padding(4)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 10198
        btn_Tambah.Text = "Input"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 64)
        btn_Refresh.Margin = New Padding(4)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10197
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Angka_BAST, Nomor_BAST, Tanggal_BAST, Nomor_PO, Tanggal_PO, Yang_Menyerahkan, Kode_Project, Kode_Supplier, Nama_Supplier, Catatan_, Kontrol_, Yang_Menerima, Tanggal_Diterima})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1494, 493)
        DataTabelUtama.TabIndex = 10201
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
        ' Angka_BAST
        ' 
        Angka_BAST.HeaderText = "Angka BAST"
        Angka_BAST.Name = "Angka_BAST"
        Angka_BAST.ReadOnly = True
        Angka_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Angka_BAST.Visible = False
        ' 
        ' Nomor_BAST
        ' 
        Nomor_BAST.HeaderText = "Nomor BAST"
        Nomor_BAST.Name = "Nomor_BAST"
        Nomor_BAST.ReadOnly = True
        Nomor_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_BAST.Width = 87
        ' 
        ' Tanggal_BAST
        ' 
        Tanggal_BAST.HeaderText = "Tanggal BAST"
        Tanggal_BAST.Name = "Tanggal_BAST"
        Tanggal_BAST.ReadOnly = True
        Tanggal_BAST.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_BAST.Width = 63
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
        ' Yang_Menyerahkan
        ' 
        Yang_Menyerahkan.HeaderText = "Yang Menyerahkan"
        Yang_Menyerahkan.Name = "Yang_Menyerahkan"
        Yang_Menyerahkan.ReadOnly = True
        Yang_Menyerahkan.SortMode = DataGridViewColumnSortMode.NotSortable
        Yang_Menyerahkan.Width = 123
        ' 
        ' Kode_Project
        ' 
        Kode_Project.HeaderText = "Kode Project"
        Kode_Project.Name = "Kode_Project"
        Kode_Project.ReadOnly = True
        Kode_Project.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Project.Width = 99
        ' 
        ' Kode_Supplier
        ' 
        Kode_Supplier.HeaderText = "Kode Supplier"
        Kode_Supplier.Name = "Kode_Supplier"
        Kode_Supplier.ReadOnly = True
        Kode_Supplier.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Supplier.Visible = False
        ' 
        ' Nama_Supplier
        ' 
        Nama_Supplier.HeaderText = "Nama Supplier"
        Nama_Supplier.Name = "Nama_Supplier"
        Nama_Supplier.ReadOnly = True
        Nama_Supplier.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Supplier.Width = 150
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
        ' Yang_Menerima
        ' 
        Yang_Menerima.HeaderText = "Yang Menerima"
        Yang_Menerima.Name = "Yang_Menerima"
        Yang_Menerima.ReadOnly = True
        Yang_Menerima.SortMode = DataGridViewColumnSortMode.NotSortable
        Yang_Menerima.Width = 123
        ' 
        ' Tanggal_Diterima
        ' 
        Tanggal_Diterima.HeaderText = "Tanggal Diterima"
        Tanggal_Diterima.Name = "Tanggal_Diterima"
        Tanggal_Diterima.ReadOnly = True
        Tanggal_Diterima.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Diterima.Width = 63
        ' 
        ' cmb_Kontrol
        ' 
        cmb_Kontrol.FormattingEnabled = True
        cmb_Kontrol.Location = New Point(311, 114)
        cmb_Kontrol.Margin = New Padding(4)
        cmb_Kontrol.Name = "cmb_Kontrol"
        cmb_Kontrol.Size = New Size(87, 23)
        cmb_Kontrol.TabIndex = 10205
        ' 
        ' frm_BASTPembelian
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1522, 682)
        Controls.Add(lbl_JudulForm)
        Controls.Add(Label15)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(cmb_Kontrol)
        Margin = New Padding(4)
        Name = "frm_BASTPembelian"
        StartPosition = FormStartPosition.CenterParent
        Text = "Berita Acara Serah Terima Pekerjaan (BAST)"
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents cmb_Kontrol As ComboBox
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Angka_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_PO As DataGridViewTextBoxColumn
    Friend WithEvents Yang_Menyerahkan As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Supplier As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Supplier As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Kontrol_ As DataGridViewTextBoxColumn
    Friend WithEvents Yang_Menerima As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Diterima As DataGridViewTextBoxColumn
End Class
