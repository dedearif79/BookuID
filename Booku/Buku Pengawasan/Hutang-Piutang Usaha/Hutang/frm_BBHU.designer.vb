<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_BBHU
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.grb_RentangWaktu = New System.Windows.Forms.GroupBox()
        Me.cmb_Periode = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtp_SampaiTanggal = New System.Windows.Forms.DateTimePicker()
        Me.dtp_DariTanggal = New System.Windows.Forms.DateTimePicker()
        Me.lbl_NamaSupplier = New System.Windows.Forms.Label()
        Me.txt_SaldoAkhir = New System.Windows.Forms.TextBox()
        Me.txt_SaldoAwal = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referensi_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DPP_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PPN_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_PPh_Dipotong = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Debet_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kredit_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sarana_Pembayaran = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grb_RentangWaktu.SuspendLayout()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 12)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(338, 29)
        Me.Label6.TabIndex = 10030
        Me.Label6.Text = "Buku Pembantu Hutang Usaha"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'grb_RentangWaktu
        '
        Me.grb_RentangWaktu.Controls.Add(Me.cmb_Periode)
        Me.grb_RentangWaktu.Controls.Add(Me.Label2)
        Me.grb_RentangWaktu.Controls.Add(Me.dtp_SampaiTanggal)
        Me.grb_RentangWaktu.Controls.Add(Me.dtp_DariTanggal)
        Me.grb_RentangWaktu.Location = New System.Drawing.Point(16, 63)
        Me.grb_RentangWaktu.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grb_RentangWaktu.Name = "grb_RentangWaktu"
        Me.grb_RentangWaktu.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grb_RentangWaktu.Size = New System.Drawing.Size(335, 85)
        Me.grb_RentangWaktu.TabIndex = 10054
        Me.grb_RentangWaktu.TabStop = False
        Me.grb_RentangWaktu.Text = "Rentang Waktu :"
        Me.grb_RentangWaktu.Visible = False
        '
        'cmb_Periode
        '
        Me.cmb_Periode.FormattingEnabled = True
        Me.cmb_Periode.Location = New System.Drawing.Point(12, 20)
        Me.cmb_Periode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmb_Periode.Name = "cmb_Periode"
        Me.cmb_Periode.Size = New System.Drawing.Size(125, 24)
        Me.cmb_Periode.TabIndex = 10032
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(151, 53)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 16)
        Me.Label2.TabIndex = 10028
        Me.Label2.Text = "s.d"
        '
        'dtp_SampaiTanggal
        '
        Me.dtp_SampaiTanggal.CustomFormat = "dd/MM/yyyy"
        Me.dtp_SampaiTanggal.Enabled = False
        Me.dtp_SampaiTanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_SampaiTanggal.Location = New System.Drawing.Point(189, 49)
        Me.dtp_SampaiTanggal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtp_SampaiTanggal.Name = "dtp_SampaiTanggal"
        Me.dtp_SampaiTanggal.Size = New System.Drawing.Size(125, 22)
        Me.dtp_SampaiTanggal.TabIndex = 10029
        '
        'dtp_DariTanggal
        '
        Me.dtp_DariTanggal.CustomFormat = "dd/MM/yyyy"
        Me.dtp_DariTanggal.Enabled = False
        Me.dtp_DariTanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_DariTanggal.Location = New System.Drawing.Point(12, 49)
        Me.dtp_DariTanggal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtp_DariTanggal.Name = "dtp_DariTanggal"
        Me.dtp_DariTanggal.Size = New System.Drawing.Size(125, 22)
        Me.dtp_DariTanggal.TabIndex = 10028
        '
        'lbl_NamaSupplier
        '
        Me.lbl_NamaSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaSupplier.Location = New System.Drawing.Point(573, 21)
        Me.lbl_NamaSupplier.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_NamaSupplier.Name = "lbl_NamaSupplier"
        Me.lbl_NamaSupplier.Size = New System.Drawing.Size(703, 23)
        Me.lbl_NamaSupplier.TabIndex = 10055
        Me.lbl_NamaSupplier.Text = "Nama Supplier"
        Me.lbl_NamaSupplier.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txt_SaldoAkhir
        '
        Me.txt_SaldoAkhir.Location = New System.Drawing.Point(1143, 116)
        Me.txt_SaldoAkhir.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_SaldoAkhir.Name = "txt_SaldoAkhir"
        Me.txt_SaldoAkhir.Size = New System.Drawing.Size(132, 22)
        Me.txt_SaldoAkhir.TabIndex = 10059
        Me.txt_SaldoAkhir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SaldoAwal
        '
        Me.txt_SaldoAwal.Location = New System.Drawing.Point(1143, 86)
        Me.txt_SaldoAwal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_SaldoAwal.Name = "txt_SaldoAwal"
        Me.txt_SaldoAwal.Size = New System.Drawing.Size(132, 22)
        Me.txt_SaldoAwal.TabIndex = 10058
        Me.txt_SaldoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1028, 119)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 16)
        Me.Label1.TabIndex = 10057
        Me.Label1.Text = "Saldo Akhir"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1028, 90)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 16)
        Me.Label3.TabIndex = 10056
        Me.Label3.Text = "Saldo Awal"
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Tanggal_, Me.Referensi_, Me.DPP_, Me.PPN_, Me.Jumlah_PPh_Dipotong, Me.Debet_, Me.Kredit_, Me.Saldo_, Me.Sarana_Pembayaran})
        Me.DataTabelUtama.Location = New System.Drawing.Point(16, 155)
        Me.DataTabelUtama.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(1260, 558)
        Me.DataTabelUtama.TabIndex = 10060
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(0, 0, 6, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 45
        '
        'Tanggal_
        '
        Me.Tanggal_.HeaderText = "Tanggal"
        Me.Tanggal_.Name = "Tanggal_"
        Me.Tanggal_.ReadOnly = True
        Me.Tanggal_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_.Width = 72
        '
        'Referensi_
        '
        Me.Referensi_.HeaderText = "Referensi"
        Me.Referensi_.Name = "Referensi_"
        Me.Referensi_.ReadOnly = True
        Me.Referensi_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Referensi_.Width = 123
        '
        'DPP_
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.DPP_.DefaultCellStyle = DataGridViewCellStyle2
        Me.DPP_.HeaderText = "DPP"
        Me.DPP_.Name = "DPP_"
        Me.DPP_.ReadOnly = True
        Me.DPP_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PPN_
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.PPN_.DefaultCellStyle = DataGridViewCellStyle3
        Me.PPN_.HeaderText = "PPN"
        Me.PPN_.Name = "PPN_"
        Me.PPN_.ReadOnly = True
        Me.PPN_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Jumlah_PPh_Dipotong
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Jumlah_PPh_Dipotong.DefaultCellStyle = DataGridViewCellStyle4
        Me.Jumlah_PPh_Dipotong.HeaderText = "PPh Dipotong"
        Me.Jumlah_PPh_Dipotong.Name = "Jumlah_PPh_Dipotong"
        Me.Jumlah_PPh_Dipotong.ReadOnly = True
        Me.Jumlah_PPh_Dipotong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Debet_
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Debet_.DefaultCellStyle = DataGridViewCellStyle5
        Me.Debet_.HeaderText = "Debet"
        Me.Debet_.Name = "Debet_"
        Me.Debet_.ReadOnly = True
        Me.Debet_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Debet_.Width = 99
        '
        'Kredit_
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.Kredit_.DefaultCellStyle = DataGridViewCellStyle6
        Me.Kredit_.HeaderText = "Kredit"
        Me.Kredit_.Name = "Kredit_"
        Me.Kredit_.ReadOnly = True
        Me.Kredit_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kredit_.Width = 99
        '
        'Saldo_
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.Saldo_.DefaultCellStyle = DataGridViewCellStyle7
        Me.Saldo_.HeaderText = "Saldo"
        Me.Saldo_.Name = "Saldo_"
        Me.Saldo_.ReadOnly = True
        Me.Saldo_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Saldo_.Width = 99
        '
        'Sarana_Pembayaran
        '
        Me.Sarana_Pembayaran.HeaderText = "Sarana Pembayaran"
        Me.Sarana_Pembayaran.Name = "Sarana_Pembayaran"
        Me.Sarana_Pembayaran.ReadOnly = True
        Me.Sarana_Pembayaran.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Sarana_Pembayaran.Width = 99
        '
        'frm_BBHU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1292, 727)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.txt_SaldoAkhir)
        Me.Controls.Add(Me.txt_SaldoAwal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbl_NamaSupplier)
        Me.Controls.Add(Me.grb_RentangWaktu)
        Me.Controls.Add(Me.Label6)
        Me.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_BBHU"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Buku Pembantu Hutang Usaha"
        Me.grb_RentangWaktu.ResumeLayout(False)
        Me.grb_RentangWaktu.PerformLayout()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grb_RentangWaktu As System.Windows.Forms.GroupBox
    Friend WithEvents cmb_Periode As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtp_SampaiTanggal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp_DariTanggal As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_NamaSupplier As System.Windows.Forms.Label
    Friend WithEvents txt_SaldoAkhir As System.Windows.Forms.TextBox
    Friend WithEvents txt_SaldoAwal As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents PPh_Dipotong As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Urut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Referensi_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DPP_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PPN_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_PPh_Dipotong As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Debet_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kredit_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sarana_Pembayaran As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
