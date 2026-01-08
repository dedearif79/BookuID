<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_ListSJBAST
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btn_Pilih = New System.Windows.Forms.Button()
        Me.lbl_CariInvoice = New System.Windows.Forms.Label()
        Me.txt_CariSJBAST = New System.Windows.Forms.TextBox()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.cmb_Mitra = New System.Windows.Forms.ComboBox()
        Me.lbl_FilterMitra = New System.Windows.Forms.Label()
        Me.Nomor_SJ_BAST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_SJ_BAST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_Surat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Diterima = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Alamat_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Biaya_Transportasi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Pilih
        '
        Me.btn_Pilih.Enabled = False
        Me.btn_Pilih.Location = New System.Drawing.Point(292, 411)
        Me.btn_Pilih.Name = "btn_Pilih"
        Me.btn_Pilih.Size = New System.Drawing.Size(70, 46)
        Me.btn_Pilih.TabIndex = 10053
        Me.btn_Pilih.Text = "Pilih"
        Me.btn_Pilih.UseVisualStyleBackColor = True
        '
        'lbl_CariInvoice
        '
        Me.lbl_CariInvoice.AutoSize = True
        Me.lbl_CariInvoice.Location = New System.Drawing.Point(12, 11)
        Me.lbl_CariInvoice.Name = "lbl_CariInvoice"
        Me.lbl_CariInvoice.Size = New System.Drawing.Size(126, 13)
        Me.lbl_CariInvoice.TabIndex = 10052
        Me.lbl_CariInvoice.Text = "Cari Surat Jalan / BAST :"
        '
        'txt_CariSJBAST
        '
        Me.txt_CariSJBAST.Location = New System.Drawing.Point(12, 30)
        Me.txt_CariSJBAST.Name = "txt_CariSJBAST"
        Me.txt_CariSJBAST.Size = New System.Drawing.Size(132, 20)
        Me.txt_CariSJBAST.TabIndex = 10050
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_SJ_BAST, Me.Tanggal_SJ_BAST, Me.Jenis_Surat, Me.Tanggal_Diterima, Me.Kode_Mitra, Me.Nama_Mitra, Me.Alamat_Mitra, Me.Nomor_PO, Me.Biaya_Transportasi})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 59)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(350, 346)
        Me.DataTabelUtama.TabIndex = 10051
        '
        'cmb_Mitra
        '
        Me.cmb_Mitra.FormattingEnabled = True
        Me.cmb_Mitra.Location = New System.Drawing.Point(175, 30)
        Me.cmb_Mitra.Name = "cmb_Mitra"
        Me.cmb_Mitra.Size = New System.Drawing.Size(187, 21)
        Me.cmb_Mitra.TabIndex = 10054
        '
        'lbl_FilterMitra
        '
        Me.lbl_FilterMitra.AutoSize = True
        Me.lbl_FilterMitra.Location = New System.Drawing.Point(172, 11)
        Me.lbl_FilterMitra.Name = "lbl_FilterMitra"
        Me.lbl_FilterMitra.Size = New System.Drawing.Size(61, 13)
        Me.lbl_FilterMitra.TabIndex = 10055
        Me.lbl_FilterMitra.Text = "Filter Mitra :"
        '
        'Nomor_SJ_BAST
        '
        Me.Nomor_SJ_BAST.HeaderText = "Nomor SJ/BAST"
        Me.Nomor_SJ_BAST.Name = "Nomor_SJ_BAST"
        Me.Nomor_SJ_BAST.ReadOnly = True
        Me.Nomor_SJ_BAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_SJ_BAST.Width = 132
        '
        'Tanggal_SJ_BAST
        '
        Me.Tanggal_SJ_BAST.HeaderText = "Tanggal SJ/BAST"
        Me.Tanggal_SJ_BAST.Name = "Tanggal_SJ_BAST"
        Me.Tanggal_SJ_BAST.ReadOnly = True
        Me.Tanggal_SJ_BAST.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_SJ_BAST.Visible = False
        Me.Tanggal_SJ_BAST.Width = 63
        '
        'Jenis_Surat
        '
        Me.Jenis_Surat.HeaderText = "Jenis Surat"
        Me.Jenis_Surat.Name = "Jenis_Surat"
        Me.Jenis_Surat.ReadOnly = True
        Me.Jenis_Surat.Visible = False
        '
        'Tanggal_Diterima
        '
        Me.Tanggal_Diterima.HeaderText = "Tanggal Diterima"
        Me.Tanggal_Diterima.Name = "Tanggal_Diterima"
        Me.Tanggal_Diterima.ReadOnly = True
        Me.Tanggal_Diterima.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Diterima.Width = 63
        '
        'Kode_Mitra
        '
        Me.Kode_Mitra.HeaderText = "Kode Mitra"
        Me.Kode_Mitra.Name = "Kode_Mitra"
        Me.Kode_Mitra.ReadOnly = True
        Me.Kode_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Mitra.Visible = False
        '
        'Nama_Mitra
        '
        Me.Nama_Mitra.HeaderText = "Mitra"
        Me.Nama_Mitra.Name = "Nama_Mitra"
        Me.Nama_Mitra.ReadOnly = True
        Me.Nama_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Mitra.Visible = False
        Me.Nama_Mitra.Width = 180
        '
        'Alamat_Mitra
        '
        Me.Alamat_Mitra.HeaderText = "Alamat Mitra"
        Me.Alamat_Mitra.Name = "Alamat_Mitra"
        Me.Alamat_Mitra.ReadOnly = True
        Me.Alamat_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Alamat_Mitra.Visible = False
        '
        'Nomor_PO
        '
        Me.Nomor_PO.HeaderText = "Nomor PO"
        Me.Nomor_PO.Name = "Nomor_PO"
        Me.Nomor_PO.ReadOnly = True
        Me.Nomor_PO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_PO.Width = 132
        '
        'Biaya_Transportasi
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Biaya_Transportasi.DefaultCellStyle = DataGridViewCellStyle1
        Me.Biaya_Transportasi.HeaderText = "Biaya Transportasi"
        Me.Biaya_Transportasi.Name = "Biaya_Transportasi"
        Me.Biaya_Transportasi.ReadOnly = True
        Me.Biaya_Transportasi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Biaya_Transportasi.Visible = False
        '
        'frm_ListSJBAST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 469)
        Me.Controls.Add(Me.btn_Pilih)
        Me.Controls.Add(Me.lbl_CariInvoice)
        Me.Controls.Add(Me.txt_CariSJBAST)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.cmb_Mitra)
        Me.Controls.Add(Me.lbl_FilterMitra)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "X_frm_ListSJBAST"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daftar Surat Jalan / BAST"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Pilih As Button
    Friend WithEvents lbl_CariInvoice As Label
    Friend WithEvents txt_CariSJBAST As TextBox
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents cmb_Mitra As ComboBox
    Friend WithEvents lbl_FilterMitra As Label
    Friend WithEvents Nomor_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_SJ_BAST As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Surat As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Diterima As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Alamat_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Biaya_Transportasi As DataGridViewTextBoxColumn
End Class
