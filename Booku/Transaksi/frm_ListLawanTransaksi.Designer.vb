<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ListLawanTransaksi
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_CariMitra = New System.Windows.Forms.TextBox()
        Me.lbl_CariMitra = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_Kategori = New System.Windows.Forms.ComboBox()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Pilih = New System.Windows.Forms.Button()
        Me.Kode_Lawan_Transaksi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Lawan_Transaksi = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Kode_Lawan_Transaksi, Me.Nama_Lawan_Transaksi})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 64)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(340, 346)
        Me.DataTabelUtama.TabIndex = 30
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
        Me.lbl_CariMitra.Size = New System.Drawing.Size(115, 13)
        Me.lbl_CariMitra.TabIndex = 10034
        Me.lbl_CariMitra.Text = "Cari Lawan Transaksi :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(258, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 10036
        Me.Label1.Text = "Filter :"
        '
        'cmb_Kategori
        '
        Me.cmb_Kategori.FormattingEnabled = True
        Me.cmb_Kategori.Location = New System.Drawing.Point(261, 35)
        Me.cmb_Kategori.Name = "cmb_Kategori"
        Me.cmb_Kategori.Size = New System.Drawing.Size(91, 21)
        Me.cmb_Kategori.TabIndex = 20
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
        Me.btn_Pilih.Location = New System.Drawing.Point(282, 416)
        Me.btn_Pilih.Name = "btn_Pilih"
        Me.btn_Pilih.Size = New System.Drawing.Size(70, 46)
        Me.btn_Pilih.TabIndex = 10037
        Me.btn_Pilih.Text = "Pilih"
        Me.btn_Pilih.UseVisualStyleBackColor = True
        '
        'Kode_Lawan_Transaksi
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Kode_Lawan_Transaksi.DefaultCellStyle = DataGridViewCellStyle1
        Me.Kode_Lawan_Transaksi.HeaderText = "Kode"
        Me.Kode_Lawan_Transaksi.Name = "Kode_Lawan_Transaksi"
        Me.Kode_Lawan_Transaksi.ReadOnly = True
        Me.Kode_Lawan_Transaksi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Lawan_Transaksi.Width = 63
        '
        'Nama_Lawan_Transaksi
        '
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nama_Lawan_Transaksi.DefaultCellStyle = DataGridViewCellStyle2
        Me.Nama_Lawan_Transaksi.HeaderText = "Nama Lawan Transaksi"
        Me.Nama_Lawan_Transaksi.Name = "Nama_Lawan_Transaksi"
        Me.Nama_Lawan_Transaksi.ReadOnly = True
        Me.Nama_Lawan_Transaksi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Lawan_Transaksi.Width = 270
        '
        'frm_ListLawanTransaksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Batal
        Me.ClientSize = New System.Drawing.Size(366, 469)
        Me.Controls.Add(Me.btn_Pilih)
        Me.Controls.Add(Me.btn_OK)
        Me.Controls.Add(Me.cmb_Kategori)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbl_CariMitra)
        Me.Controls.Add(Me.txt_CariMitra)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_ListLawanTransaksi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daftar Lawan Transaksi"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents txt_CariMitra As System.Windows.Forms.TextBox
    Friend WithEvents lbl_CariMitra As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_Kategori As System.Windows.Forms.ComboBox
    Friend WithEvents btn_OK As System.Windows.Forms.Button
    Friend WithEvents btn_Pilih As System.Windows.Forms.Button
    Friend WithEvents Kode_Lawan_Transaksi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_Lawan_Transaksi As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
