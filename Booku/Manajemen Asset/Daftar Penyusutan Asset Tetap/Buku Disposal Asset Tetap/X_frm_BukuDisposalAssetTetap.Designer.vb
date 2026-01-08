<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_BukuDisposalAssetTetap
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lbl_JudulForm = New System.Windows.Forms.Label()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Berita_Acara = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Berita_Acara = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Asset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Asset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Asset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Perolehan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Harga_Perolehan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Akumulasi_Penyusutan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Laba_Rugi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Keterangan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_JV_Closing = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_LihatJurnal = New System.Windows.Forms.Button()
        Me.btn_Export = New System.Windows.Forms.Button()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_JudulForm
        '
        Me.lbl_JudulForm.AutoSize = True
        Me.lbl_JudulForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JudulForm.Location = New System.Drawing.Point(10, 12)
        Me.lbl_JudulForm.Name = "lbl_JudulForm"
        Me.lbl_JudulForm.Size = New System.Drawing.Size(379, 32)
        Me.lbl_JudulForm.TabIndex = 10039
        Me.lbl_JudulForm.Text = "Buku Disposal Asset Tetap"
        Me.lbl_JudulForm.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Location = New System.Drawing.Point(12, 57)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(81, 65)
        Me.btn_Refresh.TabIndex = 10034
        Me.btn_Refresh.Text = "Refresh"
        Me.btn_Refresh.UseVisualStyleBackColor = True
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Nomor_Berita_Acara, Me.Tanggal_Berita_Acara, Me.Kode_Asset, Me.Nama_Asset, Me.Jumlah_Asset, Me.Tanggal_Perolehan, Me.Harga_Perolehan, Me.Akumulasi_Penyusutan, Me.Laba_Rugi, Me.Keterangan_, Me.Nomor_JV_Closing})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 128)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(1280, 471)
        Me.DataTabelUtama.TabIndex = 10038
        '
        'Nomor_Urut
        '
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 33
        '
        'Nomor_Berita_Acara
        '
        Me.Nomor_Berita_Acara.HeaderText = "Nomor Berita Acara"
        Me.Nomor_Berita_Acara.Name = "Nomor_Berita_Acara"
        Me.Nomor_Berita_Acara.ReadOnly = True
        Me.Nomor_Berita_Acara.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Berita_Acara.Width = 99
        '
        'Tanggal_Berita_Acara
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Tanggal_Berita_Acara.DefaultCellStyle = DataGridViewCellStyle1
        Me.Tanggal_Berita_Acara.HeaderText = "Tanggal Berita Acara"
        Me.Tanggal_Berita_Acara.Name = "Tanggal_Berita_Acara"
        Me.Tanggal_Berita_Acara.ReadOnly = True
        Me.Tanggal_Berita_Acara.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Berita_Acara.Width = 69
        '
        'Kode_Asset
        '
        Me.Kode_Asset.HeaderText = "Kode Asset"
        Me.Kode_Asset.Name = "Kode_Asset"
        Me.Kode_Asset.ReadOnly = True
        Me.Kode_Asset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Asset.Width = 150
        '
        'Nama_Asset
        '
        Me.Nama_Asset.HeaderText = "Nama Asset"
        Me.Nama_Asset.Name = "Nama_Asset"
        Me.Nama_Asset.ReadOnly = True
        Me.Nama_Asset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Asset.Width = 210
        '
        'Jumlah_Asset
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Me.Jumlah_Asset.DefaultCellStyle = DataGridViewCellStyle2
        Me.Jumlah_Asset.HeaderText = "Jumlah"
        Me.Jumlah_Asset.Name = "Jumlah_Asset"
        Me.Jumlah_Asset.ReadOnly = True
        Me.Jumlah_Asset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Asset.Width = 45
        '
        'Tanggal_Perolehan
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Tanggal_Perolehan.DefaultCellStyle = DataGridViewCellStyle3
        Me.Tanggal_Perolehan.HeaderText = "Tanggal Perolehan"
        Me.Tanggal_Perolehan.Name = "Tanggal_Perolehan"
        Me.Tanggal_Perolehan.ReadOnly = True
        Me.Tanggal_Perolehan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Perolehan.Width = 69
        '
        'Harga_Perolehan
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Me.Harga_Perolehan.DefaultCellStyle = DataGridViewCellStyle4
        Me.Harga_Perolehan.HeaderText = "Harga Perolehan"
        Me.Harga_Perolehan.Name = "Harga_Perolehan"
        Me.Harga_Perolehan.ReadOnly = True
        Me.Harga_Perolehan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Harga_Perolehan.Width = 81
        '
        'Akumulasi_Penyusutan
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        Me.Akumulasi_Penyusutan.DefaultCellStyle = DataGridViewCellStyle5
        Me.Akumulasi_Penyusutan.HeaderText = "Akumulasi Penyusutan"
        Me.Akumulasi_Penyusutan.Name = "Akumulasi_Penyusutan"
        Me.Akumulasi_Penyusutan.ReadOnly = True
        Me.Akumulasi_Penyusutan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Akumulasi_Penyusutan.Width = 81
        '
        'Laba_Rugi
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        Me.Laba_Rugi.DefaultCellStyle = DataGridViewCellStyle6
        Me.Laba_Rugi.HeaderText = "Laba/Rugi Disposal"
        Me.Laba_Rugi.Name = "Laba_Rugi"
        Me.Laba_Rugi.ReadOnly = True
        Me.Laba_Rugi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Laba_Rugi.Width = 81
        '
        'Keterangan_
        '
        Me.Keterangan_.HeaderText = "Keterangan"
        Me.Keterangan_.Name = "Keterangan_"
        Me.Keterangan_.ReadOnly = True
        Me.Keterangan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Keterangan_.Width = 270
        '
        'Nomor_JV_Closing
        '
        Me.Nomor_JV_Closing.HeaderText = "Nomor JV"
        Me.Nomor_JV_Closing.Name = "Nomor_JV_Closing"
        Me.Nomor_JV_Closing.ReadOnly = True
        Me.Nomor_JV_Closing.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_JV_Closing.Visible = False
        '
        'btn_LihatJurnal
        '
        Me.btn_LihatJurnal.Location = New System.Drawing.Point(743, 72)
        Me.btn_LihatJurnal.Name = "btn_LihatJurnal"
        Me.btn_LihatJurnal.Size = New System.Drawing.Size(96, 35)
        Me.btn_LihatJurnal.TabIndex = 10079
        Me.btn_LihatJurnal.Text = "Lihat Jurnal"
        Me.btn_LihatJurnal.UseVisualStyleBackColor = True
        '
        'btn_Export
        '
        Me.btn_Export.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Export.Location = New System.Drawing.Point(1193, 12)
        Me.btn_Export.Name = "btn_Export"
        Me.btn_Export.Size = New System.Drawing.Size(99, 45)
        Me.btn_Export.TabIndex = 10080
        Me.btn_Export.Text = "Export"
        Me.btn_Export.UseVisualStyleBackColor = True
        '
        'frm_BukuDisposalAssetTetap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 681)
        Me.Controls.Add(Me.btn_Export)
        Me.Controls.Add(Me.btn_LihatJurnal)
        Me.Controls.Add(Me.lbl_JudulForm)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Name = "X_frm_BukuDisposalAssetTetap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Buku Disposal Asset Tetap"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents btn_Export As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Berita_Acara As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Berita_Acara As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Asset As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Asset As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Asset As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Perolehan As DataGridViewTextBoxColumn
    Friend WithEvents Harga_Perolehan As DataGridViewTextBoxColumn
    Friend WithEvents Akumulasi_Penyusutan As DataGridViewTextBoxColumn
    Friend WithEvents Laba_Rugi As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV_Closing As DataGridViewTextBoxColumn
End Class
