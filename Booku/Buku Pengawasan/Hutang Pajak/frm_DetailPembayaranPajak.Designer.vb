<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DetailPembayaranPajak
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
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.btn_LihatJurnal = New System.Windows.Forms.Button()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Masa_Pajak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jenis_Pajak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Ketetapan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Setoran = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Bayar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Bayar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_NTPN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TW_TL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_JV = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Masa_Pajak, Me.Jenis_Pajak, Me.Nomor_Ketetapan, Me.Kode_Setoran, Me.Tanggal_Bayar, Me.Jumlah_Bayar, Me.Kode_NTPN, Me.TW_TL, Me.Nomor_JV})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 55)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(775, 505)
        Me.DataTabelUtama.TabIndex = 151
        '
        'btn_LihatJurnal
        '
        Me.btn_LihatJurnal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_LihatJurnal.Enabled = False
        Me.btn_LihatJurnal.Location = New System.Drawing.Point(704, 11)
        Me.btn_LihatJurnal.Name = "btn_LihatJurnal"
        Me.btn_LihatJurnal.Size = New System.Drawing.Size(83, 38)
        Me.btn_LihatJurnal.TabIndex = 10065
        Me.btn_LihatJurnal.Text = "Lihat Jurnal"
        Me.btn_LihatJurnal.UseVisualStyleBackColor = True
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.MinimumWidth = 6
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 33
        '
        'Masa_Pajak
        '
        Me.Masa_Pajak.HeaderText = "Masa Pajak"
        Me.Masa_Pajak.MinimumWidth = 6
        Me.Masa_Pajak.Name = "Masa_Pajak"
        Me.Masa_Pajak.ReadOnly = True
        Me.Masa_Pajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Masa_Pajak.Width = 99
        '
        'Jenis_Pajak
        '
        Me.Jenis_Pajak.HeaderText = "Jenis Pajak"
        Me.Jenis_Pajak.Name = "Jenis_Pajak"
        Me.Jenis_Pajak.ReadOnly = True
        Me.Jenis_Pajak.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jenis_Pajak.Width = 99
        '
        'Nomor_Ketetapan
        '
        Me.Nomor_Ketetapan.HeaderText = "Nomor Ketetapan"
        Me.Nomor_Ketetapan.Name = "Nomor_Ketetapan"
        Me.Nomor_Ketetapan.ReadOnly = True
        Me.Nomor_Ketetapan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Ketetapan.Width = 123
        '
        'Kode_Setoran
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Kode_Setoran.DefaultCellStyle = DataGridViewCellStyle2
        Me.Kode_Setoran.HeaderText = "Kode"
        Me.Kode_Setoran.MinimumWidth = 6
        Me.Kode_Setoran.Name = "Kode_Setoran"
        Me.Kode_Setoran.ReadOnly = True
        Me.Kode_Setoran.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Setoran.Width = 81
        '
        'Tanggal_Bayar
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Tanggal_Bayar.DefaultCellStyle = DataGridViewCellStyle3
        Me.Tanggal_Bayar.HeaderText = "Tanggal Bayar"
        Me.Tanggal_Bayar.MinimumWidth = 6
        Me.Tanggal_Bayar.Name = "Tanggal_Bayar"
        Me.Tanggal_Bayar.ReadOnly = True
        Me.Tanggal_Bayar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Bayar.Width = 63
        '
        'Jumlah_Bayar
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Me.Jumlah_Bayar.DefaultCellStyle = DataGridViewCellStyle4
        Me.Jumlah_Bayar.HeaderText = "Jumlah Bayar"
        Me.Jumlah_Bayar.MinimumWidth = 6
        Me.Jumlah_Bayar.Name = "Jumlah_Bayar"
        Me.Jumlah_Bayar.ReadOnly = True
        Me.Jumlah_Bayar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Bayar.Width = 81
        '
        'Kode_NTPN
        '
        Me.Kode_NTPN.HeaderText = "Kode NTPN"
        Me.Kode_NTPN.MinimumWidth = 6
        Me.Kode_NTPN.Name = "Kode_NTPN"
        Me.Kode_NTPN.ReadOnly = True
        Me.Kode_NTPN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_NTPN.Width = 159
        '
        'TW_TL
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.TW_TL.DefaultCellStyle = DataGridViewCellStyle5
        Me.TW_TL.HeaderText = "TW TL"
        Me.TW_TL.MinimumWidth = 6
        Me.TW_TL.Name = "TW_TL"
        Me.TW_TL.ReadOnly = True
        Me.TW_TL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TW_TL.Width = 33
        '
        'Nomor_JV
        '
        Me.Nomor_JV.HeaderText = "Nomor JV"
        Me.Nomor_JV.MinimumWidth = 6
        Me.Nomor_JV.Name = "Nomor_JV"
        Me.Nomor_JV.ReadOnly = True
        Me.Nomor_JV.Visible = False
        Me.Nomor_JV.Width = 63
        '
        'frm_DetailPembayaranPajak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 572)
        Me.Controls.Add(Me.btn_LihatJurnal)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frm_DetailPembayaranPajak"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detail Pembayaran Pajak"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_LihatJurnal As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Masa_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Ketetapan As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Setoran As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Bayar As DataGridViewTextBoxColumn
    Friend WithEvents Kode_NTPN As DataGridViewTextBoxColumn
    Friend WithEvents TW_TL As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
End Class
