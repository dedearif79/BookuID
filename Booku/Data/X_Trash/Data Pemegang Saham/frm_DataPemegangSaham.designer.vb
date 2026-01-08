<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_DataPemegangSaham
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        btn_Tambah = New Button()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        DataTabelUtama = New DataGridView()
        btn_Refresh = New Button()
        lbl_JudulForm = New Label()
        Nomor_Urut = New DataGridViewTextBoxColumn()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Nama_ = New DataGridViewTextBoxColumn()
        NIK_ = New DataGridViewTextBoxColumn()
        NPWP_ = New DataGridViewTextBoxColumn()
        Alamat_ = New DataGridViewTextBoxColumn()
        Jenis_PS = New DataGridViewTextBoxColumn()
        Lokasi_PS = New DataGridViewTextBoxColumn()
        Jumlah_Lembar = New DataGridViewTextBoxColumn()
        Harga_Per_Lembar = New DataGridViewTextBoxColumn()
        Jumlah_Saham = New DataGridViewTextBoxColumn()
        Rekening_Bank = New DataGridViewTextBoxColumn()
        Atas_Nama = New DataGridViewTextBoxColumn()
        Catatan_ = New DataGridViewTextBoxColumn()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Tambah.Location = New Point(1203, 92)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 351
        btn_Tambah.Text = "Tambah"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Hapus.Location = New Point(1410, 92)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 352
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btn_Edit.Location = New Point(1307, 92)
        btn_Edit.Margin = New Padding(4, 3, 4, 3)
        btn_Edit.Name = "btn_Edit"
        btn_Edit.Size = New Size(97, 40)
        btn_Edit.TabIndex = 353
        btn_Edit.Text = "Edit"
        btn_Edit.UseVisualStyleBackColor = True
        ' 
        ' DataTabelUtama
        ' 
        DataTabelUtama.AllowUserToAddRows = False
        DataTabelUtama.AllowUserToDeleteRows = False
        DataTabelUtama.AllowUserToResizeRows = False
        DataTabelUtama.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataTabelUtama.BorderStyle = BorderStyle.Fixed3D
        DataTabelUtama.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Nomor_Urut, Nomor_ID, Nama_, NIK_, NPWP_, Alamat_, Jenis_PS, Lokasi_PS, Jumlah_Lembar, Harga_Per_Lembar, Jumlah_Saham, Rekening_Bank, Atas_Nama, Catatan_})
        DataTabelUtama.Location = New Point(14, 148)
        DataTabelUtama.Margin = New Padding(4, 3, 4, 3)
        DataTabelUtama.MultiSelect = False
        DataTabelUtama.Name = "DataTabelUtama"
        DataTabelUtama.ReadOnly = True
        DataTabelUtama.RowHeadersVisible = False
        DataTabelUtama.RowHeadersWidth = 33
        DataTabelUtama.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataTabelUtama.Size = New Size(1493, 543)
        DataTabelUtama.TabIndex = 10018
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 66)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(94, 75)
        btn_Refresh.TabIndex = 10
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' lbl_JudulForm
        ' 
        lbl_JudulForm.AutoSize = True
        lbl_JudulForm.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lbl_JudulForm.Location = New Point(12, 14)
        lbl_JudulForm.Margin = New Padding(4, 0, 4, 0)
        lbl_JudulForm.Name = "lbl_JudulForm"
        lbl_JudulForm.Size = New Size(335, 32)
        lbl_JudulForm.TabIndex = 10030
        lbl_JudulForm.Text = "Data Pemegang Saham"
        lbl_JudulForm.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Nomor_Urut
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight
        Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Nomor_Urut.HeaderText = "No."
        Nomor_Urut.Name = "Nomor_Urut"
        Nomor_Urut.ReadOnly = True
        Nomor_Urut.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_Urut.Width = 33
        ' 
        ' Nomor_ID
        ' 
        Nomor_ID.HeaderText = "Nomor ID"
        Nomor_ID.Name = "Nomor_ID"
        Nomor_ID.ReadOnly = True
        Nomor_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID.Visible = False
        ' 
        ' Nama_
        ' 
        Nama_.HeaderText = "Nama"
        Nama_.Name = "Nama_"
        Nama_.ReadOnly = True
        Nama_.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_.Width = 180
        ' 
        ' NIK_
        ' 
        NIK_.HeaderText = "NIK"
        NIK_.Name = "NIK_"
        NIK_.ReadOnly = True
        NIK_.SortMode = DataGridViewColumnSortMode.NotSortable
        NIK_.Width = 117
        ' 
        ' NPWP_
        ' 
        NPWP_.HeaderText = "NPWP"
        NPWP_.Name = "NPWP_"
        NPWP_.ReadOnly = True
        NPWP_.SortMode = DataGridViewColumnSortMode.NotSortable
        NPWP_.Width = 117
        ' 
        ' Alamat_
        ' 
        Alamat_.HeaderText = "Alamat"
        Alamat_.Name = "Alamat_"
        Alamat_.ReadOnly = True
        Alamat_.SortMode = DataGridViewColumnSortMode.NotSortable
        Alamat_.Width = 180
        ' 
        ' Jenis_PS
        ' 
        Jenis_PS.HeaderText = "Jenis"
        Jenis_PS.Name = "Jenis_PS"
        Jenis_PS.ReadOnly = True
        Jenis_PS.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_PS.Width = 99
        ' 
        ' Lokasi_PS
        ' 
        Lokasi_PS.HeaderText = "Lokasi"
        Lokasi_PS.Name = "Lokasi_PS"
        Lokasi_PS.ReadOnly = True
        Lokasi_PS.SortMode = DataGridViewColumnSortMode.NotSortable
        Lokasi_PS.Width = 99
        ' 
        ' Jumlah_Lembar
        ' 
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N0"
        Jumlah_Lembar.DefaultCellStyle = DataGridViewCellStyle2
        Jumlah_Lembar.HeaderText = "Jumlah Lembar"
        Jumlah_Lembar.Name = "Jumlah_Lembar"
        Jumlah_Lembar.ReadOnly = True
        Jumlah_Lembar.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Lembar.Width = 69
        ' 
        ' Harga_Per_Lembar
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        Harga_Per_Lembar.DefaultCellStyle = DataGridViewCellStyle3
        Harga_Per_Lembar.HeaderText = "Harga Perlembar"
        Harga_Per_Lembar.Name = "Harga_Per_Lembar"
        Harga_Per_Lembar.ReadOnly = True
        Harga_Per_Lembar.SortMode = DataGridViewColumnSortMode.NotSortable
        Harga_Per_Lembar.Width = 81
        ' 
        ' Jumlah_Saham
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        Jumlah_Saham.DefaultCellStyle = DataGridViewCellStyle4
        Jumlah_Saham.HeaderText = "Jumlah Saham"
        Jumlah_Saham.Name = "Jumlah_Saham"
        Jumlah_Saham.ReadOnly = True
        Jumlah_Saham.SortMode = DataGridViewColumnSortMode.NotSortable
        Jumlah_Saham.Width = 81
        ' 
        ' Rekening_Bank
        ' 
        Rekening_Bank.HeaderText = "Rekening Bank"
        Rekening_Bank.Name = "Rekening_Bank"
        Rekening_Bank.ReadOnly = True
        Rekening_Bank.SortMode = DataGridViewColumnSortMode.NotSortable
        Rekening_Bank.Width = 111
        ' 
        ' Atas_Nama
        ' 
        Atas_Nama.HeaderText = "Atas Nama"
        Atas_Nama.Name = "Atas_Nama"
        Atas_Nama.ReadOnly = True
        Atas_Nama.SortMode = DataGridViewColumnSortMode.NotSortable
        Atas_Nama.Width = 111
        ' 
        ' Catatan_
        ' 
        Catatan_.HeaderText = "Catatan"
        Catatan_.Name = "Catatan_"
        Catatan_.ReadOnly = True
        Catatan_.SortMode = DataGridViewColumnSortMode.NotSortable
        Catatan_.Width = 330
        ' 
        ' frm_DataPemegangSaham
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 786)
        Controls.Add(lbl_JudulForm)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_DataPemegangSaham"
        StartPosition = FormStartPosition.CenterParent
        Text = "Data Pemegang Saham"
        WindowState = FormWindowState.Maximized
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents lbl_JudulForm As System.Windows.Forms.Label
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_ID As DataGridViewTextBoxColumn
    Friend WithEvents Nama_ As DataGridViewTextBoxColumn
    Friend WithEvents NIK_ As DataGridViewTextBoxColumn
    Friend WithEvents NPWP_ As DataGridViewTextBoxColumn
    Friend WithEvents Alamat_ As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_PS As DataGridViewTextBoxColumn
    Friend WithEvents Lokasi_PS As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Lembar As DataGridViewTextBoxColumn
    Friend WithEvents Harga_Per_Lembar As DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Saham As DataGridViewTextBoxColumn
    Friend WithEvents Rekening_Bank As DataGridViewTextBoxColumn
    Friend WithEvents Atas_Nama As DataGridViewTextBoxColumn
    Friend WithEvents Catatan_ As DataGridViewTextBoxColumn
End Class
