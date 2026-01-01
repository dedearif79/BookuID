<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DetailBayar
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
        Me.DataGridView = New System.Windows.Forms.DataGridView()
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
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Transaksi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Penambahan_Hutang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bayar_Hutang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo_Hutang = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sarana_Pembayaran = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grb_RentangWaktu.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToResizeRows = False
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Tanggal_Transaksi, Me.Nomor_Invoice, Me.Penambahan_Hutang, Me.Bayar_Hutang, Me.Saldo_Hutang, Me.Sarana_Pembayaran})
        Me.DataGridView.Location = New System.Drawing.Point(12, 126)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(641, 453)
        Me.DataGridView.TabIndex = 10018
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(142, 29)
        Me.Label6.TabIndex = 10030
        Me.Label6.Text = "Detail Bayar"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'grb_RentangWaktu
        '
        Me.grb_RentangWaktu.Controls.Add(Me.cmb_Periode)
        Me.grb_RentangWaktu.Controls.Add(Me.Label2)
        Me.grb_RentangWaktu.Controls.Add(Me.dtp_SampaiTanggal)
        Me.grb_RentangWaktu.Controls.Add(Me.dtp_DariTanggal)
        Me.grb_RentangWaktu.Location = New System.Drawing.Point(12, 51)
        Me.grb_RentangWaktu.Name = "grb_RentangWaktu"
        Me.grb_RentangWaktu.Size = New System.Drawing.Size(251, 69)
        Me.grb_RentangWaktu.TabIndex = 10054
        Me.grb_RentangWaktu.TabStop = False
        Me.grb_RentangWaktu.Text = "Rentang Waktu :"
        Me.grb_RentangWaktu.Visible = False
        '
        'cmb_Periode
        '
        Me.cmb_Periode.FormattingEnabled = True
        Me.cmb_Periode.Location = New System.Drawing.Point(9, 16)
        Me.cmb_Periode.Name = "cmb_Periode"
        Me.cmb_Periode.Size = New System.Drawing.Size(95, 21)
        Me.cmb_Periode.TabIndex = 10032
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(113, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 10028
        Me.Label2.Text = "s.d"
        '
        'dtp_SampaiTanggal
        '
        Me.dtp_SampaiTanggal.CustomFormat = "dd/MM/yyyy"
        Me.dtp_SampaiTanggal.Enabled = False
        Me.dtp_SampaiTanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_SampaiTanggal.Location = New System.Drawing.Point(142, 40)
        Me.dtp_SampaiTanggal.Name = "dtp_SampaiTanggal"
        Me.dtp_SampaiTanggal.Size = New System.Drawing.Size(95, 20)
        Me.dtp_SampaiTanggal.TabIndex = 10029
        '
        'dtp_DariTanggal
        '
        Me.dtp_DariTanggal.CustomFormat = "dd/MM/yyyy"
        Me.dtp_DariTanggal.Enabled = False
        Me.dtp_DariTanggal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_DariTanggal.Location = New System.Drawing.Point(9, 40)
        Me.dtp_DariTanggal.Name = "dtp_DariTanggal"
        Me.dtp_DariTanggal.Size = New System.Drawing.Size(95, 20)
        Me.dtp_DariTanggal.TabIndex = 10028
        '
        'lbl_NamaSupplier
        '
        Me.lbl_NamaSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaSupplier.Location = New System.Drawing.Point(209, 17)
        Me.lbl_NamaSupplier.Name = "lbl_NamaSupplier"
        Me.lbl_NamaSupplier.Size = New System.Drawing.Size(444, 19)
        Me.lbl_NamaSupplier.TabIndex = 10055
        Me.lbl_NamaSupplier.Text = "Nama Supplier"
        Me.lbl_NamaSupplier.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txt_SaldoAkhir
        '
        Me.txt_SaldoAkhir.Location = New System.Drawing.Point(553, 94)
        Me.txt_SaldoAkhir.Name = "txt_SaldoAkhir"
        Me.txt_SaldoAkhir.Size = New System.Drawing.Size(100, 20)
        Me.txt_SaldoAkhir.TabIndex = 10059
        Me.txt_SaldoAkhir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_SaldoAkhir.Visible = False
        '
        'txt_SaldoAwal
        '
        Me.txt_SaldoAwal.Location = New System.Drawing.Point(553, 70)
        Me.txt_SaldoAwal.Name = "txt_SaldoAwal"
        Me.txt_SaldoAwal.Size = New System.Drawing.Size(100, 20)
        Me.txt_SaldoAwal.TabIndex = 10058
        Me.txt_SaldoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_SaldoAwal.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(465, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 10057
        Me.Label1.Text = "Saldo Akhir"
        Me.Label1.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(467, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 10056
        Me.Label3.Text = "Saldo Awal"
        Me.Label3.Visible = False
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(0, 0, 6, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.Width = 45
        '
        'Tanggal_Transaksi
        '
        Me.Tanggal_Transaksi.HeaderText = "Tanggal Transaksi"
        Me.Tanggal_Transaksi.Name = "Tanggal_Transaksi"
        Me.Tanggal_Transaksi.ReadOnly = True
        Me.Tanggal_Transaksi.Width = 72
        '
        'Nomor_Invoice
        '
        Me.Nomor_Invoice.HeaderText = "Nomor Invoice"
        Me.Nomor_Invoice.Name = "Nomor_Invoice"
        Me.Nomor_Invoice.ReadOnly = True
        Me.Nomor_Invoice.Width = 123
        '
        'Penambahan_Hutang
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.Penambahan_Hutang.DefaultCellStyle = DataGridViewCellStyle2
        Me.Penambahan_Hutang.HeaderText = "Penambahan"
        Me.Penambahan_Hutang.Name = "Penambahan_Hutang"
        Me.Penambahan_Hutang.ReadOnly = True
        Me.Penambahan_Hutang.Width = 99
        '
        'Bayar_Hutang
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Bayar_Hutang.DefaultCellStyle = DataGridViewCellStyle3
        Me.Bayar_Hutang.HeaderText = "Bayar"
        Me.Bayar_Hutang.Name = "Bayar_Hutang"
        Me.Bayar_Hutang.ReadOnly = True
        Me.Bayar_Hutang.Width = 99
        '
        'Saldo_Hutang
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Saldo_Hutang.DefaultCellStyle = DataGridViewCellStyle4
        Me.Saldo_Hutang.HeaderText = "Saldo"
        Me.Saldo_Hutang.Name = "Saldo_Hutang"
        Me.Saldo_Hutang.ReadOnly = True
        Me.Saldo_Hutang.Width = 99
        '
        'Sarana_Pembayaran
        '
        Me.Sarana_Pembayaran.HeaderText = "Sarana Pembayaran"
        Me.Sarana_Pembayaran.Name = "Sarana_Pembayaran"
        Me.Sarana_Pembayaran.ReadOnly = True
        Me.Sarana_Pembayaran.Width = 99
        '
        'frm_DetailBayar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 591)
        Me.Controls.Add(Me.txt_SaldoAkhir)
        Me.Controls.Add(Me.txt_SaldoAwal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbl_NamaSupplier)
        Me.Controls.Add(Me.grb_RentangWaktu)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DataGridView)
        Me.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_DetailBayar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Detail Bayar"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grb_RentangWaktu.ResumeLayout(False)
        Me.grb_RentangWaktu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
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
    Friend WithEvents Nomor_Urut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Transaksi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Penambahan_Hutang As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bayar_Hutang As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Hutang As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sarana_Pembayaran As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
