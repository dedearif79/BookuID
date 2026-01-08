<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_DataProdukApp
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
        Me.btn_Tambah = New System.Windows.Forms.Button()
        Me.btn_Hapus = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.Nomor_Seri_Produk = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID_Customer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jumlah_Perangkat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status_Terpakai = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Tambah
        '
        Me.btn_Tambah.Location = New System.Drawing.Point(193, 85)
        Me.btn_Tambah.Name = "btn_Tambah"
        Me.btn_Tambah.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tambah.TabIndex = 351
        Me.btn_Tambah.Text = "Tambah"
        Me.btn_Tambah.UseVisualStyleBackColor = True
        '
        'btn_Hapus
        '
        Me.btn_Hapus.Location = New System.Drawing.Point(371, 85)
        Me.btn_Hapus.Name = "btn_Hapus"
        Me.btn_Hapus.Size = New System.Drawing.Size(83, 35)
        Me.btn_Hapus.TabIndex = 352
        Me.btn_Hapus.Text = "Hapus"
        Me.btn_Hapus.UseVisualStyleBackColor = True
        '
        'btn_Edit
        '
        Me.btn_Edit.Location = New System.Drawing.Point(282, 85)
        Me.btn_Edit.Name = "btn_Edit"
        Me.btn_Edit.Size = New System.Drawing.Size(83, 35)
        Me.btn_Edit.TabIndex = 353
        Me.btn_Edit.Text = "Edit"
        Me.btn_Edit.UseVisualStyleBackColor = True
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToResizeRows = False
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Seri_Produk, Me.ID_Customer, Me.Jumlah_Perangkat, Me.Status_Terpakai})
        Me.DataGridView.Location = New System.Drawing.Point(12, 126)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(442, 471)
        Me.DataGridView.TabIndex = 10018
        '
        'Nomor_Seri_Produk
        '
        Me.Nomor_Seri_Produk.HeaderText = "Nomor Seri Produk"
        Me.Nomor_Seri_Produk.Name = "Nomor_Seri_Produk"
        Me.Nomor_Seri_Produk.ReadOnly = True
        Me.Nomor_Seri_Produk.Width = 210
        '
        'ID_Customer
        '
        Me.ID_Customer.HeaderText = "ID Customer"
        Me.ID_Customer.Name = "ID_Customer"
        Me.ID_Customer.ReadOnly = True
        Me.ID_Customer.Width = 72
        '
        'Jumlah_Perangkat
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Jumlah_Perangkat.DefaultCellStyle = DataGridViewCellStyle1
        Me.Jumlah_Perangkat.HeaderText = "Jumlah Perangkat"
        Me.Jumlah_Perangkat.Name = "Jumlah_Perangkat"
        Me.Jumlah_Perangkat.ReadOnly = True
        Me.Jumlah_Perangkat.Width = 63
        '
        'Status_Terpakai
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Status_Terpakai.DefaultCellStyle = DataGridViewCellStyle2
        Me.Status_Terpakai.HeaderText = "Status Terpakai"
        Me.Status_Terpakai.Name = "Status_Terpakai"
        Me.Status_Terpakai.ReadOnly = True
        Me.Status_Terpakai.Width = 63
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Location = New System.Drawing.Point(12, 55)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(81, 65)
        Me.btn_Refresh.TabIndex = 10
        Me.btn_Refresh.Text = "Refresh"
        Me.btn_Refresh.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(299, 32)
        Me.Label6.TabIndex = 10030
        Me.Label6.Text = "Data Produk Aplikasi"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frm_DataProdukApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 691)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.DataGridView)
        Me.Controls.Add(Me.btn_Edit)
        Me.Controls.Add(Me.btn_Hapus)
        Me.Controls.Add(Me.btn_Tambah)
        Me.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.Name = "frm_DataProdukApp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Data Produk Aplikasi"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Hapus As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Nomor_Seri_Produk As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ID_Customer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jumlah_Perangkat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status_Terpakai As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
