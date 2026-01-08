<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_BukuPembantuPiutangPemegangSaham
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_SaldoAkhir = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txt_JumlahPinjaman = New System.Windows.Forms.TextBox()
        Me.txt_SaldoAwal = New System.Windows.Forms.TextBox()
        Me.lbl_TanggalPinjaman = New System.Windows.Forms.Label()
        Me.lbl_NIK = New System.Windows.Forms.Label()
        Me.lbl_NamaPemegangSaham = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Angsuran_Ke = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Angsuran = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Angsuran = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Keterangan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(509, 25)
        Me.Label6.TabIndex = 10062
        Me.Label6.Text = "Buku Pembantu Pengawasan Piutang Pemegang Saham"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txt_SaldoAkhir
        '
        Me.txt_SaldoAkhir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_SaldoAkhir.Location = New System.Drawing.Point(416, 586)
        Me.txt_SaldoAkhir.Name = "txt_SaldoAkhir"
        Me.txt_SaldoAkhir.Size = New System.Drawing.Size(100, 20)
        Me.txt_SaldoAkhir.TabIndex = 10101
        Me.txt_SaldoAkhir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(400, 589)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 13)
        Me.Label8.TabIndex = 10100
        Me.Label8.Text = ":"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(309, 589)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(61, 13)
        Me.Label11.TabIndex = 10099
        Me.Label11.Text = "Saldo Akhir"
        '
        'txt_JumlahPinjaman
        '
        Me.txt_JumlahPinjaman.Location = New System.Drawing.Point(416, 69)
        Me.txt_JumlahPinjaman.Name = "txt_JumlahPinjaman"
        Me.txt_JumlahPinjaman.Size = New System.Drawing.Size(100, 20)
        Me.txt_JumlahPinjaman.TabIndex = 10098
        Me.txt_JumlahPinjaman.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_SaldoAwal
        '
        Me.txt_SaldoAwal.Location = New System.Drawing.Point(416, 95)
        Me.txt_SaldoAwal.Name = "txt_SaldoAwal"
        Me.txt_SaldoAwal.Size = New System.Drawing.Size(100, 20)
        Me.txt_SaldoAwal.TabIndex = 10097
        Me.txt_SaldoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_TanggalPinjaman
        '
        Me.lbl_TanggalPinjaman.AutoSize = True
        Me.lbl_TanggalPinjaman.Location = New System.Drawing.Point(413, 50)
        Me.lbl_TanggalPinjaman.Name = "lbl_TanggalPinjaman"
        Me.lbl_TanggalPinjaman.Size = New System.Drawing.Size(92, 13)
        Me.lbl_TanggalPinjaman.TabIndex = 10096
        Me.lbl_TanggalPinjaman.Text = "Tanggal Pinjaman"
        '
        'lbl_NIK
        '
        Me.lbl_NIK.AutoSize = True
        Me.lbl_NIK.Location = New System.Drawing.Point(121, 72)
        Me.lbl_NIK.Name = "lbl_NIK"
        Me.lbl_NIK.Size = New System.Drawing.Size(25, 13)
        Me.lbl_NIK.TabIndex = 10095
        Me.lbl_NIK.Text = "NIK"
        '
        'lbl_NamaPemegangSaham
        '
        Me.lbl_NamaPemegangSaham.AutoSize = True
        Me.lbl_NamaPemegangSaham.Location = New System.Drawing.Point(121, 50)
        Me.lbl_NamaPemegangSaham.Name = "lbl_NamaPemegangSaham"
        Me.lbl_NamaPemegangSaham.Size = New System.Drawing.Size(125, 13)
        Me.lbl_NamaPemegangSaham.TabIndex = 10094
        Me.lbl_NamaPemegangSaham.Text = "Nama Pemegang Saham"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(397, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 13)
        Me.Label9.TabIndex = 10093
        Me.Label9.Text = ":"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(397, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 13)
        Me.Label10.TabIndex = 10092
        Me.Label10.Text = ":"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(306, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(86, 13)
        Me.Label12.TabIndex = 10091
        Me.Label12.Text = "Jumlah Pinjaman"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(306, 50)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 13)
        Me.Label13.TabIndex = 10090
        Me.Label13.Text = "Tanggal Pinjaman"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(397, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 13)
        Me.Label4.TabIndex = 10089
        Me.Label4.Text = ":"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(105, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 13)
        Me.Label5.TabIndex = 10088
        Me.Label5.Text = ":"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(105, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 13)
        Me.Label7.TabIndex = 10087
        Me.Label7.Text = ":"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(306, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 10086
        Me.Label3.Text = "Saldo Awal"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 10085
        Me.Label2.Text = "NPWP"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 10084
        Me.Label1.Text = "Nama"
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
        Me.DataTabelUtama.TabIndex = 10083
        '
        'Angsuran_Ke
        '
        Me.Angsuran_Ke.HeaderText = "Angsuran Ke"
        Me.Angsuran_Ke.MinimumWidth = 6
        Me.Angsuran_Ke.Name = "Angsuran_Ke"
        Me.Angsuran_Ke.ReadOnly = True
        Me.Angsuran_Ke.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Angsuran_Ke.Width = 63
        '
        'Tanggal_Angsuran
        '
        Me.Tanggal_Angsuran.HeaderText = "Tanggal Angsuran"
        Me.Tanggal_Angsuran.MinimumWidth = 6
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
        Me.Jumlah_Angsuran.MinimumWidth = 6
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
        Me.Saldo_.MinimumWidth = 6
        Me.Saldo_.Name = "Saldo_"
        Me.Saldo_.ReadOnly = True
        Me.Saldo_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Saldo_.Width = 81
        '
        'Keterangan_
        '
        Me.Keterangan_.HeaderText = "Keterangan"
        Me.Keterangan_.MinimumWidth = 6
        Me.Keterangan_.Name = "Keterangan_"
        Me.Keterangan_.ReadOnly = True
        Me.Keterangan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Keterangan_.Width = 270
        '
        'frm_BukuPembantuPiutangPemegangSaham
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
        Me.Controls.Add(Me.lbl_NIK)
        Me.Controls.Add(Me.lbl_NamaPemegangSaham)
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
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.Label6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_BukuPembantuPiutangPemegangSaham"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buku Pembantu Pengawasan Piutang Pemegang Saham"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_SaldoAkhir As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txt_JumlahPinjaman As TextBox
    Friend WithEvents txt_SaldoAwal As TextBox
    Friend WithEvents lbl_TanggalPinjaman As Label
    Friend WithEvents lbl_NIK As Label
    Friend WithEvents lbl_NamaPemegangSaham As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents Angsuran_Ke As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Angsuran As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Angsuran As DataGridViewTextBoxColumn
    Friend WithEvents Saldo_ As DataGridViewTextBoxColumn
    Friend WithEvents Keterangan_ As DataGridViewTextBoxColumn
End Class
