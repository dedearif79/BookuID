<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_BukuPembantuPiutangKaryawan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Angsuran_Ke = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Angsuran = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Angsuran = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Keterangan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lbl_TanggalPinjaman = New System.Windows.Forms.Label()
        Me.lbl_Jabatan = New System.Windows.Forms.Label()
        Me.lbl_NamaKaryawan = New System.Windows.Forms.Label()
        Me.txt_SaldoAwal = New System.Windows.Forms.TextBox()
        Me.txt_JumlahPinjaman = New System.Windows.Forms.TextBox()
        Me.txt_SaldoAkhir = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Angsuran_Ke, Me.Tanggal_Angsuran, Me.Jumlah_Angsuran, Me.Saldo_, Me.Keterangan_})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 131)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(568, 440)
        Me.DataTabelUtama.TabIndex = 10061
        '
        'Angsuran_Ke
        '
        Me.Angsuran_Ke.HeaderText = "Angsuran Ke"
        Me.Angsuran_Ke.Name = "Angsuran_Ke"
        Me.Angsuran_Ke.ReadOnly = True
        Me.Angsuran_Ke.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Angsuran_Ke.Width = 63
        '
        'Tanggal_Angsuran
        '
        Me.Tanggal_Angsuran.HeaderText = "Tanggal Angsuran"
        Me.Tanggal_Angsuran.Name = "Tanggal_Angsuran"
        Me.Tanggal_Angsuran.ReadOnly = True
        Me.Tanggal_Angsuran.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Angsuran.Width = 69
        '
        'Jumlah_Angsuran
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        Me.Jumlah_Angsuran.DefaultCellStyle = DataGridViewCellStyle1
        Me.Jumlah_Angsuran.HeaderText = "Jumlah Angsuran"
        Me.Jumlah_Angsuran.Name = "Jumlah_Angsuran"
        Me.Jumlah_Angsuran.ReadOnly = True
        Me.Jumlah_Angsuran.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jumlah_Angsuran.Width = 81
        '
        'Saldo_
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Me.Saldo_.DefaultCellStyle = DataGridViewCellStyle2
        Me.Saldo_.HeaderText = "Saldo"
        Me.Saldo_.Name = "Saldo_"
        Me.Saldo_.ReadOnly = True
        Me.Saldo_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Saldo_.Width = 81
        '
        'Keterangan_
        '
        Me.Keterangan_.HeaderText = "Keterangan"
        Me.Keterangan_.Name = "Keterangan_"
        Me.Keterangan_.ReadOnly = True
        Me.Keterangan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Keterangan_.Width = 270
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(434, 25)
        Me.Label6.TabIndex = 10062
        Me.Label6.Text = "Buku Pembantu Pengawasan Piutang Karyawan"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 10063
        Me.Label1.Text = "Nama Karyawan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 10064
        Me.Label2.Text = "Jabatan"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(306, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 10065
        Me.Label3.Text = "Saldo Awal"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(397, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 13)
        Me.Label4.TabIndex = 10068
        Me.Label4.Text = ":"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(105, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 13)
        Me.Label5.TabIndex = 10067
        Me.Label5.Text = ":"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(105, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 13)
        Me.Label7.TabIndex = 10066
        Me.Label7.Text = ":"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(397, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 13)
        Me.Label9.TabIndex = 10073
        Me.Label9.Text = ":"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(397, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 13)
        Me.Label10.TabIndex = 10072
        Me.Label10.Text = ":"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(306, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(86, 13)
        Me.Label12.TabIndex = 10070
        Me.Label12.Text = "Jumlah Pinjaman"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(306, 50)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 13)
        Me.Label13.TabIndex = 10069
        Me.Label13.Text = "Tanggal Pinjaman"
        '
        'lbl_TanggalPinjaman
        '
        Me.lbl_TanggalPinjaman.AutoSize = True
        Me.lbl_TanggalPinjaman.Location = New System.Drawing.Point(413, 50)
        Me.lbl_TanggalPinjaman.Name = "lbl_TanggalPinjaman"
        Me.lbl_TanggalPinjaman.Size = New System.Drawing.Size(92, 13)
        Me.lbl_TanggalPinjaman.TabIndex = 10076
        Me.lbl_TanggalPinjaman.Text = "Tanggal Pinjaman"
        '
        'lbl_Jabatan
        '
        Me.lbl_Jabatan.AutoSize = True
        Me.lbl_Jabatan.Location = New System.Drawing.Point(121, 72)
        Me.lbl_Jabatan.Name = "lbl_Jabatan"
        Me.lbl_Jabatan.Size = New System.Drawing.Size(45, 13)
        Me.lbl_Jabatan.TabIndex = 10075
        Me.lbl_Jabatan.Text = "Jabatan"
        '
        'lbl_NamaKaryawan
        '
        Me.lbl_NamaKaryawan.AutoSize = True
        Me.lbl_NamaKaryawan.Location = New System.Drawing.Point(121, 50)
        Me.lbl_NamaKaryawan.Name = "lbl_NamaKaryawan"
        Me.lbl_NamaKaryawan.Size = New System.Drawing.Size(85, 13)
        Me.lbl_NamaKaryawan.TabIndex = 10074
        Me.lbl_NamaKaryawan.Text = "Nama Karyawan"
        '
        'txt_SaldoAwal
        '
        Me.txt_SaldoAwal.Location = New System.Drawing.Point(416, 95)
        Me.txt_SaldoAwal.Name = "txt_SaldoAwal"
        Me.txt_SaldoAwal.Size = New System.Drawing.Size(100, 20)
        Me.txt_SaldoAwal.TabIndex = 10078
        Me.txt_SaldoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_JumlahPinjaman
        '
        Me.txt_JumlahPinjaman.Location = New System.Drawing.Point(416, 69)
        Me.txt_JumlahPinjaman.Name = "txt_JumlahPinjaman"
        Me.txt_JumlahPinjaman.Size = New System.Drawing.Size(100, 20)
        Me.txt_JumlahPinjaman.TabIndex = 10079
        Me.txt_JumlahPinjaman.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SaldoAkhir
        '
        Me.txt_SaldoAkhir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_SaldoAkhir.Location = New System.Drawing.Point(416, 586)
        Me.txt_SaldoAkhir.Name = "txt_SaldoAkhir"
        Me.txt_SaldoAkhir.Size = New System.Drawing.Size(100, 20)
        Me.txt_SaldoAkhir.TabIndex = 10082
        Me.txt_SaldoAkhir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(400, 589)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 13)
        Me.Label8.TabIndex = 10081
        Me.Label8.Text = ":"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(309, 589)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(61, 13)
        Me.Label11.TabIndex = 10080
        Me.Label11.Text = "Saldo Akhir"
        '
        'frm_BukuPembantuPiutangKaryawan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 618)
        Me.Controls.Add(Me.txt_SaldoAkhir)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt_JumlahPinjaman)
        Me.Controls.Add(Me.txt_SaldoAwal)
        Me.Controls.Add(Me.lbl_TanggalPinjaman)
        Me.Controls.Add(Me.lbl_Jabatan)
        Me.Controls.Add(Me.lbl_NamaKaryawan)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frm_BukuPembantuPiutangKaryawan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buku Pembantu Pengawasan Piutang Karyawan"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lbl_TanggalPinjaman As Label
    Friend WithEvents lbl_Jabatan As Label
    Friend WithEvents lbl_NamaKaryawan As Label
    Friend WithEvents txt_SaldoAwal As TextBox
    Friend WithEvents txt_JumlahPinjaman As TextBox
    Friend WithEvents txt_SaldoAkhir As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Angsuran_Ke As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Angsuran As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Angsuran As DataGridViewTextBoxColumn
    Friend WithEvents Saldo_ As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
End Class
