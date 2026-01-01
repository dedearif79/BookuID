<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_BukuBesar_X
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
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        DataTabelUtama = New DataGridView()
        Nomor_JV = New DataGridViewTextBoxColumn()
        Tanggal_Transaksi = New DataGridViewTextBoxColumn()
        Nama_Produk = New DataGridViewTextBoxColumn()
        Tanggal_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Invoice = New DataGridViewTextBoxColumn()
        Nomor_Faktur_Pajak = New DataGridViewTextBoxColumn()
        Kode_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Nama_Lawan_Transaksi = New DataGridViewTextBoxColumn()
        Akun_Lawan = New DataGridViewTextBoxColumn()
        D_K = New DataGridViewTextBoxColumn()
        Debet = New DataGridViewTextBoxColumn()
        Kredit = New DataGridViewTextBoxColumn()
        Saldo = New DataGridViewTextBoxColumn()
        Uraian_Transaksi = New DataGridViewTextBoxColumn()
        Direct_ = New DataGridViewTextBoxColumn()
        lbl_BukuBesar = New Label()
        Label1 = New Label()
        Label2 = New Label()
        txt_SaldoAwal = New TextBox()
        txt_SaldoAkhir = New TextBox()
        btn_Refresh = New Button()
        txt_JumlahKredit = New TextBox()
        txt_JumlahDebet = New TextBox()
        Label3 = New Label()
        Label4 = New Label()
        txt_COA = New TextBox()
        lbl_COA = New Label()
        btn_PilihCOA = New Button()
        btn_Export = New Button()
        lbl_JumlahBaris = New Label()
        btn_BukuPembantu = New Button()
        btn_LihatJurnal = New Button()
        btn_InputTransaksi = New Button()
        cmb_Direct = New ComboBox()
        lbl_Direct = New Label()
        btn_Edit = New Button()
        btn_Hapus = New Button()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_JV, Tanggal_Transaksi, Nama_Produk, Tanggal_Invoice, Nomor_Invoice, Nomor_Faktur_Pajak, Kode_Lawan_Transaksi, Nama_Lawan_Transaksi, Akun_Lawan, D_K, Debet, Kredit, Saldo, Uraian_Transaksi, Direct_})
        DataTabelUtama.Location = New Point(14, 145)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 599)
        DataTabelUtama.TabIndex = 10019
        ' 
        ' Nomor_JV
        ' 
        Nomor_JV.HeaderText = "No. JV"
        Nomor_JV.Name = "Nomor_JV"
        Nomor_JV.ReadOnly = True
        Nomor_JV.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_JV.Width = 81
        ' 
        ' Tanggal_Transaksi
        ' 
        Tanggal_Transaksi.HeaderText = "Tanggal Transaksi"
        Tanggal_Transaksi.Name = "Tanggal_Transaksi"
        Tanggal_Transaksi.ReadOnly = True
        Tanggal_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Tanggal_Transaksi.Width = 72
        ' 
        ' Nama_Produk
        ' 
        Nama_Produk.HeaderText = "Nama Barang/Jasa"
        Nama_Produk.Name = "Nama_Produk"
        Nama_Produk.ReadOnly = True
        Nama_Produk.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Produk.Width = 99
        ' 
        ' Tanggal_Invoice
        ' 
        Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Tanggal_Invoice.Name = "Tanggal_Invoice"
        Tanggal_Invoice.ReadOnly = True
        Tanggal_Invoice.Width = 72
        ' 
        ' Nomor_Invoice
        ' 
        Nomor_Invoice.HeaderText = "Nomor Invoice"
        Nomor_Invoice.Name = "Nomor_Invoice"
        Nomor_Invoice.ReadOnly = True
        Nomor_Invoice.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Invoice.Width = 99
        ' 
        ' Nomor_Faktur_Pajak
        ' 
        Nomor_Faktur_Pajak.HeaderText = "Nomor Faktur Pajak"
        Nomor_Faktur_Pajak.Name = "Nomor_Faktur_Pajak"
        Nomor_Faktur_Pajak.ReadOnly = True
        Nomor_Faktur_Pajak.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Kode_Lawan_Transaksi
        ' 
        Kode_Lawan_Transaksi.HeaderText = "Kode Lawan Transaksi"
        Kode_Lawan_Transaksi.Name = "Kode_Lawan_Transaksi"
        Kode_Lawan_Transaksi.ReadOnly = True
        Kode_Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Lawan_Transaksi.Visible = False
        Kode_Lawan_Transaksi.Width = 72
        ' 
        ' Nama_Lawan_Transaksi
        ' 
        Nama_Lawan_Transaksi.HeaderText = "Lawan Transaksi"
        Nama_Lawan_Transaksi.Name = "Nama_Lawan_Transaksi"
        Nama_Lawan_Transaksi.ReadOnly = True
        Nama_Lawan_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Lawan_Transaksi.Width = 180
        ' 
        ' Akun_Lawan
        ' 
        Akun_Lawan.HeaderText = "Akun Lawan"
        Akun_Lawan.Name = "Akun_Lawan"
        Akun_Lawan.ReadOnly = True
        Akun_Lawan.SortMode = DataGridViewColumnSortMode.NotSortable
        Akun_Lawan.Visible = False
        Akun_Lawan.Width = 150
        ' 
        ' D_K
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        D_K.DefaultCellStyle = DataGridViewCellStyle1
        D_K.HeaderText = "D/K"
        D_K.Name = "D_K"
        D_K.ReadOnly = True
        D_K.SortMode = DataGridViewColumnSortMode.NotSortable
        D_K.Width = 45
        ' 
        ' Debet
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Debet.DefaultCellStyle = DataGridViewCellStyle2
        Debet.HeaderText = "Debet"
        Debet.Name = "Debet"
        Debet.ReadOnly = True
        Debet.SortMode = DataGridViewColumnSortMode.NotSortable
        Debet.Width = 99
        ' 
        ' Kredit
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Kredit.DefaultCellStyle = DataGridViewCellStyle3
        Kredit.HeaderText = "Kredit"
        Kredit.Name = "Kredit"
        Kredit.ReadOnly = True
        Kredit.SortMode = DataGridViewColumnSortMode.NotSortable
        Kredit.Width = 99
        ' 
        ' Saldo
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Saldo.DefaultCellStyle = DataGridViewCellStyle4
        Saldo.HeaderText = "Saldo"
        Saldo.Name = "Saldo"
        Saldo.ReadOnly = True
        Saldo.SortMode = DataGridViewColumnSortMode.NotSortable
        Saldo.Width = 99
        ' 
        ' Uraian_Transaksi
        ' 
        Uraian_Transaksi.HeaderText = "Uraian Transaksi"
        Uraian_Transaksi.Name = "Uraian_Transaksi"
        Uraian_Transaksi.ReadOnly = True
        Uraian_Transaksi.SortMode = DataGridViewColumnSortMode.NotSortable
        Uraian_Transaksi.Width = 180
        ' 
        ' Direct_
        ' 
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        Direct_.DefaultCellStyle = DataGridViewCellStyle5
        Direct_.HeaderText = "Direct"
        Direct_.Name = "Direct_"
        Direct_.ReadOnly = True
        Direct_.SortMode = DataGridViewColumnSortMode.NotSortable
        Direct_.Width = 45
        ' 
        ' lbl_BukuBesar
        ' 
        lbl_BukuBesar.AutoSize = True
        lbl_BukuBesar.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_BukuBesar.Location = New Point(14, 10)
        lbl_BukuBesar.Margin = New Padding(4, 0, 4, 0)
        lbl_BukuBesar.Name = "lbl_BukuBesar"
        lbl_BukuBesar.Size = New Size(170, 32)
        lbl_BukuBesar.TabIndex = 10033
        lbl_BukuBesar.Text = "Buku Besar"
        lbl_BukuBesar.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(1063, 90)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(65, 15)
        Label1.TabIndex = 10035
        Label1.Text = "Saldo Awal"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(1063, 118)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(67, 15)
        Label2.TabIndex = 10036
        Label2.Text = "Saldo Akhir"
        ' 
        ' txt_SaldoAwal
        ' 
        txt_SaldoAwal.Location = New Point(1163, 87)
        txt_SaldoAwal.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoAwal.Name = "txt_SaldoAwal"
        txt_SaldoAwal.Size = New Size(116, 23)
        txt_SaldoAwal.TabIndex = 10037
        txt_SaldoAwal.TextAlign = HorizontalAlignment.Right
        ' 
        ' txt_SaldoAkhir
        ' 
        txt_SaldoAkhir.Location = New Point(1163, 114)
        txt_SaldoAkhir.Margin = New Padding(4, 3, 4, 3)
        txt_SaldoAkhir.Name = "txt_SaldoAkhir"
        txt_SaldoAkhir.Size = New Size(116, 23)
        txt_SaldoAkhir.TabIndex = 10038
        txt_SaldoAkhir.TextAlign = HorizontalAlignment.Right
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10039
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' txt_JumlahKredit
        ' 
        txt_JumlahKredit.Location = New Point(916, 114)
        txt_JumlahKredit.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahKredit.Name = "txt_JumlahKredit"
        txt_JumlahKredit.Size = New Size(116, 23)
        txt_JumlahKredit.TabIndex = 10043
        txt_JumlahKredit.TextAlign = HorizontalAlignment.Right
        ' 
        ' txt_JumlahDebet
        ' 
        txt_JumlahDebet.Location = New Point(916, 87)
        txt_JumlahDebet.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahDebet.Name = "txt_JumlahDebet"
        txt_JumlahDebet.Size = New Size(116, 23)
        txt_JumlahDebet.TabIndex = 10042
        txt_JumlahDebet.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(764, 118)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(129, 15)
        Label3.TabIndex = 10041
        Label3.Text = "Jumlah Transaksi Kredit"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(764, 90)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(129, 15)
        Label4.TabIndex = 10040
        Label4.Text = "Jumlah Transaksi Debet"
        ' 
        ' txt_COA
        ' 
        txt_COA.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_COA.Location = New Point(398, 114)
        txt_COA.Margin = New Padding(4, 3, 4, 3)
        txt_COA.Name = "txt_COA"
        txt_COA.Size = New Size(76, 20)
        txt_COA.TabIndex = 10044
        txt_COA.TextAlign = HorizontalAlignment.Center
        ' 
        ' lbl_COA
        ' 
        lbl_COA.AutoSize = True
        lbl_COA.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_COA.Location = New Point(398, 93)
        lbl_COA.Margin = New Padding(4, 0, 4, 0)
        lbl_COA.Name = "lbl_COA"
        lbl_COA.Size = New Size(66, 13)
        lbl_COA.TabIndex = 10045
        lbl_COA.Text = "Kode Akun :"
        ' 
        ' btn_PilihCOA
        ' 
        btn_PilihCOA.Location = New Point(482, 112)
        btn_PilihCOA.Margin = New Padding(4, 3, 4, 3)
        btn_PilihCOA.Name = "btn_PilihCOA"
        btn_PilihCOA.Size = New Size(84, 27)
        btn_PilihCOA.TabIndex = 10046
        btn_PilihCOA.Text = "Pilih"
        btn_PilihCOA.UseVisualStyleBackColor = True
        ' 
        ' btn_Export
        ' 
        btn_Export.Location = New Point(1392, 14)
        btn_Export.Margin = New Padding(4, 3, 4, 3)
        btn_Export.Name = "btn_Export"
        btn_Export.Size = New Size(115, 52)
        btn_Export.TabIndex = 10071
        btn_Export.Text = "Export"
        btn_Export.UseVisualStyleBackColor = True
        ' 
        ' lbl_JumlahBaris
        ' 
        lbl_JumlahBaris.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lbl_JumlahBaris.AutoSize = True
        lbl_JumlahBaris.Location = New Point(10, 760)
        lbl_JumlahBaris.Margin = New Padding(4, 0, 4, 0)
        lbl_JumlahBaris.Name = "lbl_JumlahBaris"
        lbl_JumlahBaris.Size = New Size(79, 15)
        lbl_JumlahBaris.TabIndex = 10072
        lbl_JumlahBaris.Text = "Jumlah Baris :"
        ' 
        ' btn_BukuPembantu
        ' 
        btn_BukuPembantu.Location = New Point(1197, 14)
        btn_BukuPembantu.Margin = New Padding(4, 3, 4, 3)
        btn_BukuPembantu.Name = "btn_BukuPembantu"
        btn_BukuPembantu.Size = New Size(115, 52)
        btn_BukuPembantu.TabIndex = 10073
        btn_BukuPembantu.Text = "Buku Pembantu"
        btn_BukuPembantu.UseVisualStyleBackColor = True
        btn_BukuPembantu.Visible = False
        ' 
        ' btn_LihatJurnal
        ' 
        btn_LihatJurnal.Location = New Point(248, 96)
        btn_LihatJurnal.Margin = New Padding(4, 3, 4, 3)
        btn_LihatJurnal.Name = "btn_LihatJurnal"
        btn_LihatJurnal.Size = New Size(94, 43)
        btn_LihatJurnal.TabIndex = 10074
        btn_LihatJurnal.Text = "Lihat Jurnal"
        btn_LihatJurnal.UseVisualStyleBackColor = True
        ' 
        ' btn_InputTransaksi
        ' 
        btn_InputTransaksi.Location = New Point(115, 63)
        btn_InputTransaksi.Margin = New Padding(4, 3, 4, 3)
        btn_InputTransaksi.Name = "btn_InputTransaksi"
        btn_InputTransaksi.Size = New Size(94, 75)
        btn_InputTransaksi.TabIndex = 10075
        btn_InputTransaksi.Text = "Input Transaksi"
        btn_InputTransaksi.UseVisualStyleBackColor = True
        ' 
        ' cmb_Direct
        ' 
        cmb_Direct.FormattingEnabled = True
        cmb_Direct.Location = New Point(630, 114)
        cmb_Direct.Margin = New Padding(4, 3, 4, 3)
        cmb_Direct.Name = "cmb_Direct"
        cmb_Direct.Size = New Size(73, 23)
        cmb_Direct.TabIndex = 10076
        ' 
        ' lbl_Direct
        ' 
        lbl_Direct.AutoSize = True
        lbl_Direct.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lbl_Direct.Location = New Point(630, 93)
        lbl_Direct.Margin = New Padding(4, 0, 4, 0)
        lbl_Direct.Name = "lbl_Direct"
        lbl_Direct.Size = New Size(41, 13)
        lbl_Direct.TabIndex = 10077
        lbl_Direct.Text = "Direct :"
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Enabled = False
        btn_Edit.Location = New Point(1307, 98)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 10079
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Enabled = False
        btn_Hapus.Location = New Point(1410, 98)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 10078
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' X_frm_BukuBesar_X
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(lbl_Direct)
        Controls.Add(cmb_Direct)
        Controls.Add(btn_InputTransaksi)
        Controls.Add(btn_LihatJurnal)
        Controls.Add(btn_BukuPembantu)
        Controls.Add(lbl_JumlahBaris)
        Controls.Add(btn_Export)
        Controls.Add(btn_PilihCOA)
        Controls.Add(txt_COA)
        Controls.Add(lbl_COA)
        Controls.Add(txt_JumlahKredit)
        Controls.Add(txt_JumlahDebet)
        Controls.Add(Label3)
        Controls.Add(Label4)
        Controls.Add(btn_Refresh)
        Controls.Add(txt_SaldoAkhir)
        Controls.Add(txt_SaldoAwal)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(lbl_BukuBesar)
        Controls.Add(DataTabelUtama)
        Margin = New Padding(4, 3, 4, 3)
        Name = "X_frm_BukuBesar_X"
        StartPosition = FormStartPosition.CenterParent
        Text = "Buku Besar"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_BukuBesar As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_SaldoAwal As System.Windows.Forms.TextBox
    Friend WithEvents txt_SaldoAkhir As System.Windows.Forms.TextBox
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents txt_JumlahKredit As System.Windows.Forms.TextBox
    Friend WithEvents txt_JumlahDebet As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_COA As System.Windows.Forms.TextBox
    Friend WithEvents lbl_COA As System.Windows.Forms.Label
    Friend WithEvents btn_PilihCOA As System.Windows.Forms.Button
    Friend WithEvents btn_Export As System.Windows.Forms.Button
    Friend WithEvents lbl_JumlahBaris As System.Windows.Forms.Label
    Friend WithEvents btn_BukuPembantu As System.Windows.Forms.Button
    Friend WithEvents btn_LihatJurnal As System.Windows.Forms.Button
    Friend WithEvents btn_InputTransaksi As System.Windows.Forms.Button
    Friend WithEvents cmb_Direct As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_Direct As System.Windows.Forms.Label
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents Nomor_JV As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Produk As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_Faktur_Pajak As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Lawan_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Akun_Lawan As DataGridViewTextBoxColumn
    Friend WithEvents D_K As DataGridViewTextBoxColumn
    Friend WithEvents Debet As DataGridViewTextBoxColumn
    Friend WithEvents Kredit As DataGridViewTextBoxColumn
    Friend WithEvents Saldo As DataGridViewTextBoxColumn
    Friend WithEvents Uraian_Transaksi As DataGridViewTextBoxColumn
    Friend WithEvents Direct_ As DataGridViewTextBoxColumn
End Class
