<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ListMitra
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Kode_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NPWP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_WP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Customer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Alamat_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_CariMitra = New System.Windows.Forms.TextBox()
        Me.lbl_CariMitra = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_JenisLawanTransaksi = New System.Windows.Forms.ComboBox()
        Me.btn_TambahDataMitra = New System.Windows.Forms.Button()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Pilih = New System.Windows.Forms.Button()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Kode_Mitra, Me.Nama_Mitra, Me.NPWP, Me.Jenis_WP, Me.Supplier, Me.Customer, Me.Alamat_Mitra})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 64)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(490, 346)
        Me.DataTabelUtama.TabIndex = 30
        '
        'Kode_Mitra
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Kode_Mitra.DefaultCellStyle = DataGridViewCellStyle3
        Me.Kode_Mitra.HeaderText = "Kode Mitra"
        Me.Kode_Mitra.Name = "Kode_Mitra"
        Me.Kode_Mitra.ReadOnly = True
        Me.Kode_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Mitra.Width = 63
        '
        'Nama_Mitra
        '
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nama_Mitra.DefaultCellStyle = DataGridViewCellStyle4
        Me.Nama_Mitra.HeaderText = "Nama Mitra"
        Me.Nama_Mitra.Name = "Nama_Mitra"
        Me.Nama_Mitra.ReadOnly = True
        Me.Nama_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Mitra.Width = 270
        '
        'NPWP
        '
        Me.NPWP.HeaderText = "NPWP"
        Me.NPWP.Name = "NPWP"
        Me.NPWP.ReadOnly = True
        Me.NPWP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NPWP.Width = 150
        '
        'Jenis_WP
        '
        Me.Jenis_WP.HeaderText = "Jenis WP"
        Me.Jenis_WP.Name = "Jenis_WP"
        Me.Jenis_WP.ReadOnly = True
        Me.Jenis_WP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jenis_WP.Visible = False
        '
        'Supplier
        '
        Me.Supplier.HeaderText = "Supplier"
        Me.Supplier.Name = "Supplier"
        Me.Supplier.ReadOnly = True
        Me.Supplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Supplier.Visible = False
        '
        'Customer
        '
        Me.Customer.HeaderText = "Customer"
        Me.Customer.Name = "Customer"
        Me.Customer.ReadOnly = True
        Me.Customer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Customer.Visible = False
        '
        'Alamat_Mitra
        '
        Me.Alamat_Mitra.HeaderText = "Alamat"
        Me.Alamat_Mitra.Name = "Alamat_Mitra"
        Me.Alamat_Mitra.ReadOnly = True
        Me.Alamat_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Alamat_Mitra.Visible = False
        '
        'btn_Batal
        '
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(419, 481)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 60
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_CariMitra
        '
        Me.txt_CariMitra.Location = New System.Drawing.Point(12, 35)
        Me.txt_CariMitra.Name = "txt_CariMitra"
        Me.txt_CariMitra.Size = New System.Drawing.Size(182, 20)
        Me.txt_CariMitra.TabIndex = 10
        '
        'lbl_CariMitra
        '
        Me.lbl_CariMitra.AutoSize = True
        Me.lbl_CariMitra.Location = New System.Drawing.Point(12, 16)
        Me.lbl_CariMitra.Name = "lbl_CariMitra"
        Me.lbl_CariMitra.Size = New System.Drawing.Size(57, 13)
        Me.lbl_CariMitra.TabIndex = 10034
        Me.lbl_CariMitra.Text = "Cari Mitra :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(226, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 10036
        Me.Label1.Text = "Filter :"
        '
        'cmb_JenisLawanTransaksi
        '
        Me.cmb_JenisLawanTransaksi.FormattingEnabled = True
        Me.cmb_JenisLawanTransaksi.Location = New System.Drawing.Point(229, 35)
        Me.cmb_JenisLawanTransaksi.Name = "cmb_JenisLawanTransaksi"
        Me.cmb_JenisLawanTransaksi.Size = New System.Drawing.Size(91, 21)
        Me.cmb_JenisLawanTransaksi.TabIndex = 20
        '
        'btn_TambahDataMitra
        '
        Me.btn_TambahDataMitra.Location = New System.Drawing.Point(412, 12)
        Me.btn_TambahDataMitra.Name = "btn_TambahDataMitra"
        Me.btn_TambahDataMitra.Size = New System.Drawing.Size(90, 46)
        Me.btn_TambahDataMitra.TabIndex = 40
        Me.btn_TambahDataMitra.Text = "Tambah"
        Me.btn_TambahDataMitra.UseVisualStyleBackColor = True
        '
        'btn_OK
        '
        Me.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btn_OK.Location = New System.Drawing.Point(330, 481)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(83, 35)
        Me.btn_OK.TabIndex = 50
        Me.btn_OK.Text = "OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Pilih
        '
        Me.btn_Pilih.Enabled = False
        Me.btn_Pilih.Location = New System.Drawing.Point(433, 416)
        Me.btn_Pilih.Name = "btn_Pilih"
        Me.btn_Pilih.Size = New System.Drawing.Size(70, 46)
        Me.btn_Pilih.TabIndex = 10037
        Me.btn_Pilih.Text = "Pilih"
        Me.btn_Pilih.UseVisualStyleBackColor = True
        '
        'frm_ListMitra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Batal
        Me.ClientSize = New System.Drawing.Size(515, 469)
        Me.Controls.Add(Me.btn_Pilih)
        Me.Controls.Add(Me.btn_OK)
        Me.Controls.Add(Me.btn_TambahDataMitra)
        Me.Controls.Add(Me.cmb_JenisLawanTransaksi)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbl_CariMitra)
        Me.Controls.Add(Me.txt_CariMitra)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_ListMitra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daftar Mitra"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents txt_CariMitra As System.Windows.Forms.TextBox
    Friend WithEvents lbl_CariMitra As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_JenisLawanTransaksi As System.Windows.Forms.ComboBox
    Friend WithEvents btn_TambahDataMitra As System.Windows.Forms.Button
    Friend WithEvents btn_OK As System.Windows.Forms.Button
    Friend WithEvents btn_Pilih As System.Windows.Forms.Button
    Friend WithEvents Kode_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents NPWP As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_WP As DataGridViewTextBoxColumn
    Friend WithEvents Supplier As DataGridViewTextBoxColumn
    Friend WithEvents Customer As DataGridViewTextBoxColumn
    Friend WithEvents Alamat_Mitra As DataGridViewTextBoxColumn
End Class
