<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DataKaryawan
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
        Me.lbl_JudulForm = New System.Windows.Forms.Label()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Tambah = New System.Windows.Forms.Button()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Registrasi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_ID_Karyawan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NIK_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Karyawan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jabatan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Rekening_Bank = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Atas_Nama = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Catatan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status_Aktif = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_JudulForm
        '
        Me.lbl_JudulForm.AutoSize = True
        Me.lbl_JudulForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JudulForm.Location = New System.Drawing.Point(10, 12)
        Me.lbl_JudulForm.Name = "lbl_JudulForm"
        Me.lbl_JudulForm.Size = New System.Drawing.Size(220, 32)
        Me.lbl_JudulForm.TabIndex = 10039
        Me.lbl_JudulForm.Text = "Data Karyawan"
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Tanggal_Registrasi, Me.Nomor_ID_Karyawan, Me.NIK_, Me.Nama_Karyawan, Me.Jabatan_, Me.Rekening_Bank, Me.Atas_Nama, Me.Catatan_, Me.Status_Aktif})
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
        'btn_Edit
        '
        Me.btn_Edit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Edit.Location = New System.Drawing.Point(1120, 80)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(83, 35)
        Me.btn_Edit.TabIndex = 10037
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'btn_Hapus
        '
        Me.btn_Hapus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Hapus.Location = New System.Drawing.Point(1209, 80)
        Me.btn_Hapus.Name = "btn_Hapus"
        Me.btn_Hapus.Size = New System.Drawing.Size(83, 35)
        Me.btn_Hapus.TabIndex = 10036
        Me.btn_Hapus.Text = "Hapus"
        Me.btn_Hapus.UseVisualStyleBackColor = True
        '
        'btn_Tambah
        '
        Me.btn_Tambah.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Tambah.Location = New System.Drawing.Point(1031, 80)
        Me.btn_Tambah.Name = "btn_Tambah"
        Me.btn_Tambah.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tambah.TabIndex = 10035
        Me.btn_Tambah.Text = "Tambah"
        Me.btn_Tambah.UseVisualStyleBackColor = True
        '
        'Nomor_Urut
        '
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 33
        '
        'Tanggal_Registrasi
        '
        Me.Tanggal_Registrasi.HeaderText = "Tanggal Registrasi"
        Me.Tanggal_Registrasi.Name = "Tanggal_Registrasi"
        Me.Tanggal_Registrasi.ReadOnly = True
        Me.Tanggal_Registrasi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Registrasi.Width = 69
        '
        'Nomor_ID_Karyawan
        '
        Me.Nomor_ID_Karyawan.HeaderText = "Nomor ID"
        Me.Nomor_ID_Karyawan.Name = "Nomor_ID_Karyawan"
        Me.Nomor_ID_Karyawan.ReadOnly = True
        Me.Nomor_ID_Karyawan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_ID_Karyawan.Width = 117
        '
        'NIK_
        '
        Me.NIK_.HeaderText = "NIK"
        Me.NIK_.Name = "NIK_"
        Me.NIK_.ReadOnly = True
        Me.NIK_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NIK_.Width = 127
        '
        'Nama_Karyawan
        '
        Me.Nama_Karyawan.HeaderText = "Nama Karyawan"
        Me.Nama_Karyawan.Name = "Nama_Karyawan"
        Me.Nama_Karyawan.ReadOnly = True
        Me.Nama_Karyawan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Karyawan.Width = 180
        '
        'Jabatan_
        '
        Me.Jabatan_.HeaderText = "Jabatan"
        Me.Jabatan_.Name = "Jabatan_"
        Me.Jabatan_.ReadOnly = True
        Me.Jabatan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jabatan_.Width = 135
        '
        'Rekening_Bank
        '
        Me.Rekening_Bank.HeaderText = "Rekening Bank"
        Me.Rekening_Bank.Name = "Rekening_Bank"
        Me.Rekening_Bank.ReadOnly = True
        Me.Rekening_Bank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Rekening_Bank.Width = 111
        '
        'Atas_Nama
        '
        Me.Atas_Nama.HeaderText = "Atas Nama"
        Me.Atas_Nama.Name = "Atas_Nama"
        Me.Atas_Nama.ReadOnly = True
        Me.Atas_Nama.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Atas_Nama.Width = 111
        '
        'Catatan_
        '
        Me.Catatan_.HeaderText = "Catatan"
        Me.Catatan_.Name = "Catatan_"
        Me.Catatan_.ReadOnly = True
        Me.Catatan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Catatan_.Width = 333
        '
        'Status_Aktif
        '
        Me.Status_Aktif.HeaderText = "Status Aktif"
        Me.Status_Aktif.Name = "Status_Aktif"
        Me.Status_Aktif.ReadOnly = True
        Me.Status_Aktif.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Status_Aktif.Visible = False
        Me.Status_Aktif.Width = 45
        '
        'frm_DataKaryawan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 681)
        Me.Controls.Add(Me.lbl_JudulForm)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.Controls.Add(Me.btn_Edit)
        Me.Controls.Add(Me.btn_Hapus)
        Me.Controls.Add(Me.btn_Tambah)
        Me.Name = "frm_DataKaryawan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Data Karyawan"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_JudulForm As Label
    Friend WithEvents btn_Refresh As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents btn_Edit As Button
    Friend WithEvents btn_Hapus As Button
    Friend WithEvents btn_Tambah As Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Registrasi As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID_Karyawan As DataGridViewTextBoxColumn
    Friend WithEvents NIK_ As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Karyawan As DataGridViewTextBoxColumn
    Friend WithEvents Jabatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Rekening_Bank As DataGridViewTextBoxColumn
    Friend WithEvents Atas_Nama As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
    Friend WithEvents Status_Aktif As DataGridViewTextBoxColumn
End Class
