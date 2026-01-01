<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_TutupBuku_X
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COA_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo_Awal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Debet_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kredit_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo_Akhir = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pemisah_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo_Awal_Tahun_Berikutnya = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_TrialBalance = New System.Windows.Forms.Button()
        Me.lbl_JudulForm = New System.Windows.Forms.Label()
        Me.btn_TransferSaldoDanTutupBuku = New System.Windows.Forms.Button()
        Me.btn_EditSaldo = New System.Windows.Forms.Button()
        Me.bgw_CekKesesuaianData = New System.ComponentModel.BackgroundWorker()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.COA_, Me.Nama_Akun, Me.Saldo_Awal, Me.Debet_, Me.Kredit_, Me.Saldo_Akhir, Me.Pemisah_, Me.Saldo_Awal_Tahun_Berikutnya})
        Me.DataTabelUtama.Location = New System.Drawing.Point(16, 155)
        Me.DataTabelUtama.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(1292, 645)
        Me.DataTabelUtama.TabIndex = 10018
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle8
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 45
        '
        'COA_
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.COA_.DefaultCellStyle = DataGridViewCellStyle9
        Me.COA_.HeaderText = "Kode Akun"
        Me.COA_.Name = "COA_"
        Me.COA_.ReadOnly = True
        Me.COA_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.COA_.Width = 63
        '
        'Nama_Akun
        '
        Me.Nama_Akun.HeaderText = "Nama Akun"
        Me.Nama_Akun.Name = "Nama_Akun"
        Me.Nama_Akun.ReadOnly = True
        Me.Nama_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Akun.Width = 210
        '
        'Saldo_Awal
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        Me.Saldo_Awal.DefaultCellStyle = DataGridViewCellStyle10
        Me.Saldo_Awal.HeaderText = "Saldo Awal"
        Me.Saldo_Awal.Name = "Saldo_Awal"
        Me.Saldo_Awal.ReadOnly = True
        Me.Saldo_Awal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Saldo_Awal.Width = 123
        '
        'Debet_
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "N0"
        Me.Debet_.DefaultCellStyle = DataGridViewCellStyle11
        Me.Debet_.HeaderText = "Debet"
        Me.Debet_.Name = "Debet_"
        Me.Debet_.ReadOnly = True
        Me.Debet_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Debet_.Width = 123
        '
        'Kredit_
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N0"
        Me.Kredit_.DefaultCellStyle = DataGridViewCellStyle12
        Me.Kredit_.HeaderText = "Kredit"
        Me.Kredit_.Name = "Kredit_"
        Me.Kredit_.ReadOnly = True
        Me.Kredit_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kredit_.Width = 123
        '
        'Saldo_Akhir
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N0"
        Me.Saldo_Akhir.DefaultCellStyle = DataGridViewCellStyle13
        Me.Saldo_Akhir.HeaderText = "Saldo Akhir"
        Me.Saldo_Akhir.Name = "Saldo_Akhir"
        Me.Saldo_Akhir.ReadOnly = True
        Me.Saldo_Akhir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Saldo_Akhir.Width = 123
        '
        'Pemisah_
        '
        Me.Pemisah_.HeaderText = ""
        Me.Pemisah_.Name = "Pemisah_"
        Me.Pemisah_.ReadOnly = True
        Me.Pemisah_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Pemisah_.Width = 15
        '
        'Saldo_Awal_Tahun_Berikutnya
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N0"
        Me.Saldo_Awal_Tahun_Berikutnya.DefaultCellStyle = DataGridViewCellStyle14
        Me.Saldo_Awal_Tahun_Berikutnya.HeaderText = "Saldo Awal Tahun Berikutnya"
        Me.Saldo_Awal_Tahun_Berikutnya.Name = "Saldo_Awal_Tahun_Berikutnya"
        Me.Saldo_Awal_Tahun_Berikutnya.ReadOnly = True
        Me.Saldo_Awal_Tahun_Berikutnya.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Saldo_Awal_Tahun_Berikutnya.Width = 123
        '
        'btn_TrialBalance
        '
        Me.btn_TrialBalance.Location = New System.Drawing.Point(16, 68)
        Me.btn_TrialBalance.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_TrialBalance.Name = "btn_TrialBalance"
        Me.btn_TrialBalance.Size = New System.Drawing.Size(125, 80)
        Me.btn_TrialBalance.TabIndex = 10
        Me.btn_TrialBalance.Text = "Trial Balance"
        Me.btn_TrialBalance.UseVisualStyleBackColor = True
        '
        'lbl_JudulForm
        '
        Me.lbl_JudulForm.AutoSize = True
        Me.lbl_JudulForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JudulForm.Location = New System.Drawing.Point(13, 12)
        Me.lbl_JudulForm.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_JudulForm.Name = "lbl_JudulForm"
        Me.lbl_JudulForm.Size = New System.Drawing.Size(169, 32)
        Me.lbl_JudulForm.TabIndex = 10030
        Me.lbl_JudulForm.Text = "Tutup Buku"
        Me.lbl_JudulForm.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn_TransferSaldoDanTutupBuku
        '
        Me.btn_TransferSaldoDanTutupBuku.Enabled = False
        Me.btn_TransferSaldoDanTutupBuku.Location = New System.Drawing.Point(299, 68)
        Me.btn_TransferSaldoDanTutupBuku.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_TransferSaldoDanTutupBuku.Name = "btn_TransferSaldoDanTutupBuku"
        Me.btn_TransferSaldoDanTutupBuku.Size = New System.Drawing.Size(300, 80)
        Me.btn_TransferSaldoDanTutupBuku.TabIndex = 10031
        Me.btn_TransferSaldoDanTutupBuku.Text = "Transfer Saldo dan Tutup Buku >>"
        Me.btn_TransferSaldoDanTutupBuku.UseVisualStyleBackColor = True
        '
        'btn_EditSaldo
        '
        Me.btn_EditSaldo.Enabled = False
        Me.btn_EditSaldo.Location = New System.Drawing.Point(149, 68)
        Me.btn_EditSaldo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_EditSaldo.Name = "btn_EditSaldo"
        Me.btn_EditSaldo.Size = New System.Drawing.Size(141, 80)
        Me.btn_EditSaldo.TabIndex = 10032
        Me.btn_EditSaldo.Text = "Edit Saldo"
        Me.btn_EditSaldo.UseVisualStyleBackColor = True
        '
        'bgw_CekKesesuaianData
        '
        '
        'frm_TutupBuku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1739, 850)
        Me.Controls.Add(Me.btn_EditSaldo)
        Me.Controls.Add(Me.btn_TransferSaldoDanTutupBuku)
        Me.Controls.Add(Me.lbl_JudulForm)
        Me.Controls.Add(Me.btn_TrialBalance)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frm_TutupBuku"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Tutup Buku"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_TrialBalance As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents btn_TransferSaldoDanTutupBuku As System.Windows.Forms.Button
    Friend WithEvents Nomor_Urut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COA_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Awal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Debet_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kredit_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Akhir As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pemisah_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Saldo_Awal_Tahun_Berikutnya As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_EditSaldo As System.Windows.Forms.Button
    Friend WithEvents bgw_CekKesesuaianData As System.ComponentModel.BackgroundWorker
End Class
