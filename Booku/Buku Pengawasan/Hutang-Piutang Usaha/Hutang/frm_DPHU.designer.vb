<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DPHU
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
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo_Awal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DPP_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PPN_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_PPh_Dipotong = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Tagihan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Bayar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sisa_Hutang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_BBHU = New System.Windows.Forms.Button()
        Me.cmb_Supplier = New System.Windows.Forms.ComboBox()
        Me.lbl_Supplier = New System.Windows.Forms.Label()
        Me.btn_DetailBayar = New System.Windows.Forms.Button()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataTabelUtama.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Kode_Supplier, Me.Nama_Supplier, Me.Saldo_Awal, Me.Jumlah_Invoice, Me.DPP_, Me.PPN_, Me.Jumlah_PPh_Dipotong, Me.Jumlah_Tagihan, Me.Jumlah_Bayar, Me.Sisa_Hutang})
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataTabelUtama.DefaultCellStyle = DataGridViewCellStyle26
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 126)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(1102, 453)
        Me.DataTabelUtama.TabIndex = 10018
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle15
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 45
        '
        'Kode_Supplier
        '
        DataGridViewCellStyle16.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.Kode_Supplier.DefaultCellStyle = DataGridViewCellStyle16
        Me.Kode_Supplier.HeaderText = "Kode Supplier"
        Me.Kode_Supplier.Name = "Kode_Supplier"
        Me.Kode_Supplier.ReadOnly = True
        Me.Kode_Supplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Supplier.Width = 81
        '
        'Nama_Supplier
        '
        DataGridViewCellStyle17.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nama_Supplier.DefaultCellStyle = DataGridViewCellStyle17
        Me.Nama_Supplier.HeaderText = "Nama Suppleir"
        Me.Nama_Supplier.Name = "Nama_Supplier"
        Me.Nama_Supplier.ReadOnly = True
        Me.Nama_Supplier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Supplier.Width = 210
        '
        'Saldo_Awal
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle18.Format = "N0"
        DataGridViewCellStyle18.NullValue = Nothing
        Me.Saldo_Awal.DefaultCellStyle = DataGridViewCellStyle18
        Me.Saldo_Awal.HeaderText = "Saldo Awal"
        Me.Saldo_Awal.Name = "Saldo_Awal"
        Me.Saldo_Awal.ReadOnly = True
        Me.Saldo_Awal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Jumlah_Invoice
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Jumlah_Invoice.DefaultCellStyle = DataGridViewCellStyle19
        Me.Jumlah_Invoice.HeaderText = "Jumlah Invoice"
        Me.Jumlah_Invoice.Name = "Jumlah_Invoice"
        Me.Jumlah_Invoice.ReadOnly = True
        Me.Jumlah_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Invoice.Width = 63
        '
        'DPP_
        '
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle20.Format = "N0"
        DataGridViewCellStyle20.NullValue = Nothing
        DataGridViewCellStyle20.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.DPP_.DefaultCellStyle = DataGridViewCellStyle20
        Me.DPP_.HeaderText = "DPP"
        Me.DPP_.Name = "DPP_"
        Me.DPP_.ReadOnly = True
        Me.DPP_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DPP_.Width = 99
        '
        'PPN_
        '
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle21.Format = "N0"
        DataGridViewCellStyle21.NullValue = Nothing
        DataGridViewCellStyle21.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.PPN_.DefaultCellStyle = DataGridViewCellStyle21
        Me.PPN_.HeaderText = "PPN"
        Me.PPN_.Name = "PPN_"
        Me.PPN_.ReadOnly = True
        Me.PPN_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PPN_.Width = 99
        '
        'Jumlah_PPh_Dipotong
        '
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle22.Format = "N0"
        DataGridViewCellStyle22.NullValue = Nothing
        DataGridViewCellStyle22.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Jumlah_PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle22
        Me.Jumlah_PPh_Dipotong.HeaderText = "PPh Dipotong"
        Me.Jumlah_PPh_Dipotong.Name = "Jumlah_PPh_Dipotong"
        Me.Jumlah_PPh_Dipotong.ReadOnly = True
        Me.Jumlah_PPh_Dipotong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_PPh_Dipotong.Width = 99
        '
        'Jumlah_Tagihan
        '
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle23.Format = "N0"
        DataGridViewCellStyle23.NullValue = Nothing
        Me.Jumlah_Tagihan.DefaultCellStyle = DataGridViewCellStyle23
        Me.Jumlah_Tagihan.HeaderText = "Jumlah Tagihan"
        Me.Jumlah_Tagihan.Name = "Jumlah_Tagihan"
        Me.Jumlah_Tagihan.ReadOnly = True
        Me.Jumlah_Tagihan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Jumlah_Bayar
        '
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle24.Format = "N0"
        DataGridViewCellStyle24.NullValue = Nothing
        Me.Jumlah_Bayar.DefaultCellStyle = DataGridViewCellStyle24
        Me.Jumlah_Bayar.HeaderText = "Jumlah Bayar"
        Me.Jumlah_Bayar.Name = "Jumlah_Bayar"
        Me.Jumlah_Bayar.ReadOnly = True
        Me.Jumlah_Bayar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Sisa_Hutang
        '
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle25.Format = "N0"
        DataGridViewCellStyle25.NullValue = Nothing
        DataGridViewCellStyle25.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Sisa_Hutang.DefaultCellStyle = DataGridViewCellStyle25
        Me.Sisa_Hutang.HeaderText = "Sisa Hutang"
        Me.Sisa_Hutang.Name = "Sisa_Hutang"
        Me.Sisa_Hutang.ReadOnly = True
        Me.Sisa_Hutang.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Sisa_Hutang.Width = 99
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(377, 29)
        Me.Label6.TabIndex = 10030
        Me.Label6.Text = "Daftar Pengawasan Hutang Usaha"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn_BBHU
        '
        Me.btn_BBHU.Enabled = False
        Me.btn_BBHU.Location = New System.Drawing.Point(1015, 55)
        Me.btn_BBHU.Name = "btn_BBHU"
        Me.btn_BBHU.Size = New System.Drawing.Size(99, 65)
        Me.btn_BBHU.TabIndex = 10054
        Me.btn_BBHU.Text = "BBHU"
        Me.btn_BBHU.UseVisualStyleBackColor = True
        '
        'cmb_Supplier
        '
        Me.cmb_Supplier.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_Supplier.FormattingEnabled = True
        Me.cmb_Supplier.ItemHeight = 13
        Me.cmb_Supplier.Location = New System.Drawing.Point(15, 86)
        Me.cmb_Supplier.Name = "cmb_Supplier"
        Me.cmb_Supplier.Size = New System.Drawing.Size(231, 21)
        Me.cmb_Supplier.TabIndex = 10026
        '
        'lbl_Supplier
        '
        Me.lbl_Supplier.AutoSize = True
        Me.lbl_Supplier.Location = New System.Drawing.Point(12, 67)
        Me.lbl_Supplier.Name = "lbl_Supplier"
        Me.lbl_Supplier.Size = New System.Drawing.Size(51, 13)
        Me.lbl_Supplier.TabIndex = 10034
        Me.lbl_Supplier.Text = "Supplier :"
        '
        'btn_DetailBayar
        '
        Me.btn_DetailBayar.Enabled = False
        Me.btn_DetailBayar.Location = New System.Drawing.Point(910, 55)
        Me.btn_DetailBayar.Name = "btn_DetailBayar"
        Me.btn_DetailBayar.Size = New System.Drawing.Size(99, 65)
        Me.btn_DetailBayar.TabIndex = 10070
        Me.btn_DetailBayar.Text = "Detail Bayar"
        Me.btn_DetailBayar.UseVisualStyleBackColor = True
        Me.btn_DetailBayar.Visible = False
        '
        'frm_DPHU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1128, 591)
        Me.Controls.Add(Me.cmb_Supplier)
        Me.Controls.Add(Me.lbl_Supplier)
        Me.Controls.Add(Me.btn_DetailBayar)
        Me.Controls.Add(Me.btn_BBHU)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_DPHU"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Daftar Pengawasan Hutang Usaha"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_BBHU As System.Windows.Forms.Button
    Friend WithEvents cmb_Supplier As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_Supplier As System.Windows.Forms.Label
    Friend WithEvents btn_DetailBayar As System.Windows.Forms.Button
    Friend WithEvents PPh_Dipotong As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Urut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kode_Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Awal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Invoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DPP_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PPN_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_PPh_Dipotong As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Tagihan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sisa_Hutang As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
