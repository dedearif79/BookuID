<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DataMitra
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
        btn_Tambah = New Button()
        btn_Hapus = New Button()
        btn_Edit = New Button()
        DataTabelUtama = New DataGridView()
        Kode_Mitra = New DataGridViewTextBoxColumn()
        Nama_Mitra = New DataGridViewTextBoxColumn()
        Supplier_ = New DataGridViewTextBoxColumn()
        Customer_ = New DataGridViewTextBoxColumn()
        Pemegang_Saham = New DataGridViewCheckBoxColumn()
        Afiliasi_ = New DataGridViewCheckBoxColumn()
        Supplier_Customer = New DataGridViewTextBoxColumn()
        Lembaga_Keuangan = New DataGridViewCheckBoxColumn()
        PKP_ = New DataGridViewCheckBoxColumn()
        PJK_ = New DataGridViewCheckBoxColumn()
        NPWP_ = New DataGridViewTextBoxColumn()
        Jenis_WP = New DataGridViewTextBoxColumn()
        Lokasi_WP = New DataGridViewTextBoxColumn()
        Alamat_ = New DataGridViewTextBoxColumn()
        Email_ = New DataGridViewTextBoxColumn()
        PIC_ = New DataGridViewTextBoxColumn()
        Rekening_Bank = New DataGridViewTextBoxColumn()
        Atas_Nama = New DataGridViewTextBoxColumn()
        btn_Refresh = New Button()
        Label6 = New Label()
        CType(DataTabelUtama, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btn_Tambah
        ' 
        btn_Tambah.Location = New Point(1203, 90)
        btn_Tambah.Margin = New Padding(4, 3, 4, 3)
        btn_Tambah.Name = "btn_Tambah"
        btn_Tambah.Size = New Size(97, 40)
        btn_Tambah.TabIndex = 351
        btn_Tambah.Text = "Tambah"
        btn_Tambah.UseVisualStyleBackColor = True
        ' 
        ' btn_Hapus
        ' 
        btn_Hapus.Enabled = False
        btn_Hapus.Location = New Point(1410, 90)
        btn_Hapus.Margin = New Padding(4, 3, 4, 3)
        btn_Hapus.Name = "btn_Hapus"
        btn_Hapus.Size = New Size(97, 40)
        btn_Hapus.TabIndex = 352
        btn_Hapus.Text = "Hapus"
        btn_Hapus.UseVisualStyleBackColor = True
        ' 
        ' btn_Edit
        ' 
        btn_Edit.Location = New Point(1307, 90)
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
        DataTabelUtama.Columns.AddRange(New DataGridViewColumn() {Kode_Mitra, Nama_Mitra, Supplier_, Customer_, Pemegang_Saham, Afiliasi_, Supplier_Customer, Lembaga_Keuangan, PKP_, PJK_, NPWP_, Jenis_WP, Lokasi_WP, Alamat_, Email_, PIC_, Rekening_Bank, Atas_Nama})
        DataTabelUtama.Location = New Point(14, 145)
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
        ' Kode_Mitra
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        Kode_Mitra.DefaultCellStyle = DataGridViewCellStyle1
        Kode_Mitra.HeaderText = "Kode Mitra"
        Kode_Mitra.Name = "Kode_Mitra"
        Kode_Mitra.ReadOnly = True
        Kode_Mitra.SortMode = DataGridViewColumnSortMode.NotSortable
        Kode_Mitra.Width = 81
        ' 
        ' Nama_Mitra
        ' 
        Nama_Mitra.HeaderText = "Nama Mitra"
        Nama_Mitra.Name = "Nama_Mitra"
        Nama_Mitra.ReadOnly = True
        Nama_Mitra.SortMode = DataGridViewColumnSortMode.NotSortable
        Nama_Mitra.Width = 180
        ' 
        ' Supplier_
        ' 
        Supplier_.HeaderText = "Supplier"
        Supplier_.Name = "Supplier_"
        Supplier_.ReadOnly = True
        Supplier_.SortMode = DataGridViewColumnSortMode.NotSortable
        Supplier_.Visible = False
        ' 
        ' Customer_
        ' 
        Customer_.HeaderText = "Customer"
        Customer_.Name = "Customer_"
        Customer_.ReadOnly = True
        Customer_.SortMode = DataGridViewColumnSortMode.NotSortable
        Customer_.Visible = False
        ' 
        ' Pemegang_Saham
        ' 
        Pemegang_Saham.HeaderText = "Pemegang Saham"
        Pemegang_Saham.Name = "Pemegang_Saham"
        Pemegang_Saham.ReadOnly = True
        Pemegang_Saham.Width = 72
        ' 
        ' Afiliasi_
        ' 
        Afiliasi_.HeaderText = "Afiliasi"
        Afiliasi_.Name = "Afiliasi_"
        Afiliasi_.ReadOnly = True
        Afiliasi_.Resizable = DataGridViewTriState.True
        Afiliasi_.Width = 72
        ' 
        ' Supplier_Customer
        ' 
        Supplier_Customer.HeaderText = "Supplier / Customer"
        Supplier_Customer.Name = "Supplier_Customer"
        Supplier_Customer.ReadOnly = True
        Supplier_Customer.Resizable = DataGridViewTriState.True
        Supplier_Customer.SortMode = DataGridViewColumnSortMode.NotSortable
        Supplier_Customer.Width = 111
        ' 
        ' Lembaga_Keuangan
        ' 
        Lembaga_Keuangan.HeaderText = "Lembaga Keuangan"
        Lembaga_Keuangan.Name = "Lembaga_Keuangan"
        Lembaga_Keuangan.ReadOnly = True
        Lembaga_Keuangan.Resizable = DataGridViewTriState.True
        Lembaga_Keuangan.Width = 72
        ' 
        ' PKP_
        ' 
        PKP_.HeaderText = "PKP"
        PKP_.Name = "PKP_"
        PKP_.ReadOnly = True
        PKP_.Resizable = DataGridViewTriState.True
        PKP_.Width = 72
        ' 
        ' PJK_
        ' 
        PJK_.HeaderText = "Perusahaan Jasa Konstruksi"
        PJK_.Name = "PJK_"
        PJK_.ReadOnly = True
        PJK_.Resizable = DataGridViewTriState.True
        PJK_.Width = 72
        ' 
        ' NPWP_
        ' 
        NPWP_.HeaderText = "NPWP"
        NPWP_.Name = "NPWP_"
        NPWP_.ReadOnly = True
        NPWP_.SortMode = DataGridViewColumnSortMode.NotSortable
        NPWP_.Width = 123
        ' 
        ' Jenis_WP
        ' 
        Jenis_WP.HeaderText = "Jenis WP"
        Jenis_WP.Name = "Jenis_WP"
        Jenis_WP.ReadOnly = True
        Jenis_WP.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Lokasi_WP
        ' 
        Lokasi_WP.HeaderText = "Lokasi WP"
        Lokasi_WP.Name = "Lokasi_WP"
        Lokasi_WP.ReadOnly = True
        Lokasi_WP.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Alamat_
        ' 
        Alamat_.HeaderText = "Alamat"
        Alamat_.Name = "Alamat_"
        Alamat_.ReadOnly = True
        Alamat_.SortMode = DataGridViewColumnSortMode.NotSortable
        Alamat_.Width = 261
        ' 
        ' Email_
        ' 
        Email_.HeaderText = "Email"
        Email_.Name = "Email_"
        Email_.ReadOnly = True
        Email_.SortMode = DataGridViewColumnSortMode.NotSortable
        Email_.Width = 147
        ' 
        ' PIC_
        ' 
        PIC_.HeaderText = "PIC"
        PIC_.Name = "PIC_"
        PIC_.ReadOnly = True
        PIC_.SortMode = DataGridViewColumnSortMode.NotSortable
        PIC_.Width = 120
        ' 
        ' Rekening_Bank
        ' 
        Rekening_Bank.HeaderText = "Rekening Bank"
        Rekening_Bank.Name = "Rekening_Bank"
        Rekening_Bank.ReadOnly = True
        Rekening_Bank.SortMode = DataGridViewColumnSortMode.NotSortable
        Rekening_Bank.Width = 145
        ' 
        ' Atas_Nama
        ' 
        Atas_Nama.HeaderText = "Atas Nama"
        Atas_Nama.Name = "Atas_Nama"
        Atas_Nama.ReadOnly = True
        Atas_Nama.SortMode = DataGridViewColumnSortMode.NotSortable
        Atas_Nama.Width = 151
        ' 
        ' btn_Refresh
        ' 
        btn_Refresh.Location = New Point(14, 63)
        btn_Refresh.Margin = New Padding(4, 3, 4, 3)
        btn_Refresh.Name = "btn_Refresh"
        btn_Refresh.Size = New Size(115, 75)
        btn_Refresh.TabIndex = 10
        btn_Refresh.Text = "Refresh"
        btn_Refresh.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Microsoft Sans Serif", 21F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(12, 12)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(154, 32)
        Label6.TabIndex = 10030
        Label6.Text = "Data Mitra"
        Label6.TextAlign = ContentAlignment.TopCenter
        ' 
        ' frm_DataMitra
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1521, 797)
        Controls.Add(Label6)
        Controls.Add(btn_Refresh)
        Controls.Add(DataTabelUtama)
        Controls.Add(btn_Edit)
        Controls.Add(btn_Hapus)
        Controls.Add(btn_Tambah)
        ImeMode = ImeMode.Disable
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_DataMitra"
        StartPosition = FormStartPosition.CenterParent
        Text = "Data Mitra"
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
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Kode_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Supplier_ As DataGridViewTextBoxColumn
    Friend WithEvents Customer_ As DataGridViewTextBoxColumn
    Friend WithEvents Pemegang_Saham As DataGridViewCheckBoxColumn
    Friend WithEvents Afiliasi_ As DataGridViewCheckBoxColumn
    Friend WithEvents Supplier_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Lembaga_Keuangan As DataGridViewCheckBoxColumn
    Friend WithEvents PKP_ As DataGridViewCheckBoxColumn
    Friend WithEvents PJK_ As DataGridViewCheckBoxColumn
    Friend WithEvents NPWP_ As DataGridViewTextBoxColumn
    Friend WithEvents Jenis_WP As DataGridViewTextBoxColumn
    Friend WithEvents Lokasi_WP As DataGridViewTextBoxColumn
    Friend WithEvents Alamat_ As DataGridViewTextBoxColumn
    Friend WithEvents Email_ As DataGridViewTextBoxColumn
    Friend WithEvents PIC_ As DataGridViewTextBoxColumn
    Friend WithEvents Rekening_Bank As DataGridViewTextBoxColumn
    Friend WithEvents Atas_Nama As DataGridViewTextBoxColumn
End Class
