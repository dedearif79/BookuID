<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_VerifikasiDataJurnal
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
        Me.lbl_Judul = New System.Windows.Forms.Label()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.lbl_StatusBalance = New System.Windows.Forms.Label()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.lbl_BarisTabelExcel = New System.Windows.Forms.Label()
        Me.btn_BuangJurnal = New System.Windows.Forms.Button()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_K = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Debet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_Judul
        '
        Me.lbl_Judul.AutoSize = True
        Me.lbl_Judul.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Judul.Location = New System.Drawing.Point(12, 11)
        Me.lbl_Judul.Name = "lbl_Judul"
        Me.lbl_Judul.Size = New System.Drawing.Size(196, 25)
        Me.lbl_Judul.TabIndex = 10024
        Me.lbl_Judul.Text = "Verifikasi Data Jurnal"
        '
        'btn_OK
        '
        Me.btn_OK.Location = New System.Drawing.Point(242, 387)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(84, 36)
        Me.btn_OK.TabIndex = 20
        Me.btn_OK.Text = "OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToResizeRows = False
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Kode_Akun, Me.Nama_Akun, Me.D_K, Me.Debet, Me.Kredit, Me.Nomor_ID})
        Me.DataGridView.Location = New System.Drawing.Point(12, 79)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(550, 293)
        Me.DataGridView.TabIndex = 10
        '
        'lbl_StatusBalance
        '
        Me.lbl_StatusBalance.AutoSize = True
        Me.lbl_StatusBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_StatusBalance.Location = New System.Drawing.Point(12, 387)
        Me.lbl_StatusBalance.Name = "lbl_StatusBalance"
        Me.lbl_StatusBalance.Size = New System.Drawing.Size(144, 25)
        Me.lbl_StatusBalance.TabIndex = 100000033
        Me.lbl_StatusBalance.Text = "Status Balance"
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(437, 387)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(125, 36)
        Me.btn_Batal.TabIndex = 40
        Me.btn_Batal.Text = "Batalkan Proses"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'lbl_BarisTabelExcel
        '
        Me.lbl_BarisTabelExcel.AutoSize = True
        Me.lbl_BarisTabelExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BarisTabelExcel.Location = New System.Drawing.Point(14, 44)
        Me.lbl_BarisTabelExcel.Name = "lbl_BarisTabelExcel"
        Me.lbl_BarisTabelExcel.Size = New System.Drawing.Size(117, 17)
        Me.lbl_BarisTabelExcel.TabIndex = 100000035
        Me.lbl_BarisTabelExcel.Text = "Baris Tabel Excel"
        '
        'btn_BuangJurnal
        '
        Me.btn_BuangJurnal.Location = New System.Drawing.Point(332, 387)
        Me.btn_BuangJurnal.Name = "btn_BuangJurnal"
        Me.btn_BuangJurnal.Size = New System.Drawing.Size(99, 36)
        Me.btn_BuangJurnal.TabIndex = 30
        Me.btn_BuangJurnal.Text = "Buang Jurnal"
        Me.btn_BuangJurnal.UseVisualStyleBackColor = True
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(9, 0, 9, 0)
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 42
        '
        'Kode_Akun
        '
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Kode_Akun.DefaultCellStyle = DataGridViewCellStyle2
        Me.Kode_Akun.HeaderText = "Kode Akun"
        Me.Kode_Akun.Name = "Kode_Akun"
        Me.Kode_Akun.ReadOnly = True
        Me.Kode_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Akun.Width = 54
        '
        'Nama_Akun
        '
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nama_Akun.DefaultCellStyle = DataGridViewCellStyle3
        Me.Nama_Akun.HeaderText = "Nama Akun"
        Me.Nama_Akun.Name = "Nama_Akun"
        Me.Nama_Akun.ReadOnly = True
        Me.Nama_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Akun.Width = 180
        '
        'D_K
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.D_K.DefaultCellStyle = DataGridViewCellStyle4
        Me.D_K.HeaderText = "D/K"
        Me.D_K.Name = "D_K"
        Me.D_K.ReadOnly = True
        Me.D_K.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.D_K.Width = 45
        '
        'Debet
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        DataGridViewCellStyle5.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Debet.DefaultCellStyle = DataGridViewCellStyle5
        Me.Debet.HeaderText = "Debet"
        Me.Debet.Name = "Debet"
        Me.Debet.ReadOnly = True
        Me.Debet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Debet.Width = 111
        '
        'Kredit
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Kredit.DefaultCellStyle = DataGridViewCellStyle6
        Me.Kredit.HeaderText = "Kredit"
        Me.Kredit.Name = "Kredit"
        Me.Kredit.ReadOnly = True
        Me.Kredit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kredit.Width = 111
        '
        'Nomor_ID
        '
        Me.Nomor_ID.HeaderText = "Nomor ID"
        Me.Nomor_ID.Name = "Nomor_ID"
        Me.Nomor_ID.ReadOnly = True
        Me.Nomor_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_ID.Visible = False
        '
        'frm_VerifikasiDataJurnal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 434)
        Me.ControlBox = False
        Me.Controls.Add(Me.btn_BuangJurnal)
        Me.Controls.Add(Me.lbl_BarisTabelExcel)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.lbl_StatusBalance)
        Me.Controls.Add(Me.DataGridView)
        Me.Controls.Add(Me.btn_OK)
        Me.Controls.Add(Me.lbl_Judul)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_VerifikasiDataJurnal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Verifikasi Data Jurnal"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_Judul As System.Windows.Forms.Label
    Friend WithEvents btn_OK As System.Windows.Forms.Button
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_StatusBalance As System.Windows.Forms.Label
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents lbl_BarisTabelExcel As System.Windows.Forms.Label
    Friend WithEvents btn_BuangJurnal As System.Windows.Forms.Button
    Friend WithEvents Nomor_Urut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kode_Akun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_K As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Debet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Kredit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
