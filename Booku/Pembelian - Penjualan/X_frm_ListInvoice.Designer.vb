<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ListInvoice
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
        Me.btn_Pilih = New System.Windows.Forms.Button()
        Me.lbl_CariInvoice = New System.Windows.Forms.Label()
        Me.txt_CariInvoice = New System.Windows.Forms.TextBox()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.cmb_Mitra = New System.Windows.Forms.ComboBox()
        Me.lbl_FilterMitra = New System.Windows.Forms.Label()
        Me.Nomor_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tanggal_Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Alamat_Mitra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Project = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Pilih
        '
        Me.btn_Pilih.Enabled = False
        Me.btn_Pilih.Location = New System.Drawing.Point(292, 411)
        Me.btn_Pilih.Name = "btn_Pilih"
        Me.btn_Pilih.Size = New System.Drawing.Size(70, 46)
        Me.btn_Pilih.TabIndex = 10041
        Me.btn_Pilih.Text = "Pilih"
        Me.btn_Pilih.UseVisualStyleBackColor = True
        '
        'lbl_CariInvoice
        '
        Me.lbl_CariInvoice.AutoSize = True
        Me.lbl_CariInvoice.Location = New System.Drawing.Point(12, 11)
        Me.lbl_CariInvoice.Name = "lbl_CariInvoice"
        Me.lbl_CariInvoice.Size = New System.Drawing.Size(69, 13)
        Me.lbl_CariInvoice.TabIndex = 10040
        Me.lbl_CariInvoice.Text = "Cari Invoice :"
        '
        'txt_CariInvoice
        '
        Me.txt_CariInvoice.Location = New System.Drawing.Point(12, 30)
        Me.txt_CariInvoice.Name = "txt_CariInvoice"
        Me.txt_CariInvoice.Size = New System.Drawing.Size(132, 20)
        Me.txt_CariInvoice.TabIndex = 10038
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Invoice, Me.Tanggal_Invoice, Me.Kode_Mitra, Me.Nama_Mitra, Me.Alamat_Mitra, Me.Kode_Project})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 59)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(350, 346)
        Me.DataTabelUtama.TabIndex = 10039
        '
        'cmb_Mitra
        '
        Me.cmb_Mitra.FormattingEnabled = True
        Me.cmb_Mitra.Location = New System.Drawing.Point(175, 30)
        Me.cmb_Mitra.Name = "cmb_Mitra"
        Me.cmb_Mitra.Size = New System.Drawing.Size(187, 21)
        Me.cmb_Mitra.TabIndex = 10042
        '
        'lbl_FilterMitra
        '
        Me.lbl_FilterMitra.AutoSize = True
        Me.lbl_FilterMitra.Location = New System.Drawing.Point(172, 11)
        Me.lbl_FilterMitra.Name = "lbl_FilterMitra"
        Me.lbl_FilterMitra.Size = New System.Drawing.Size(61, 13)
        Me.lbl_FilterMitra.TabIndex = 10043
        Me.lbl_FilterMitra.Text = "Filter Mitra :"
        '
        'Nomor_Invoice
        '
        Me.Nomor_Invoice.HeaderText = "Nomor Invoice"
        Me.Nomor_Invoice.Name = "Nomor_Invoice"
        Me.Nomor_Invoice.ReadOnly = True
        Me.Nomor_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Invoice.Width = 99
        '
        'Tanggal_Invoice
        '
        Me.Tanggal_Invoice.HeaderText = "Tanggal Invoice"
        Me.Tanggal_Invoice.Name = "Tanggal_Invoice"
        Me.Tanggal_Invoice.ReadOnly = True
        Me.Tanggal_Invoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tanggal_Invoice.Width = 63
        '
        'Kode_Mitra
        '
        Me.Kode_Mitra.HeaderText = "Kode Mitra"
        Me.Kode_Mitra.Name = "Kode_Mitra"
        Me.Kode_Mitra.ReadOnly = True
        Me.Kode_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Mitra.Visible = False
        '
        'Nama_Mitra
        '
        Me.Nama_Mitra.HeaderText = "Mitra"
        Me.Nama_Mitra.Name = "Nama_Mitra"
        Me.Nama_Mitra.ReadOnly = True
        Me.Nama_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Mitra.Width = 180
        '
        'Alamat_Mitra
        '
        Me.Alamat_Mitra.HeaderText = "Alamat Mitra"
        Me.Alamat_Mitra.Name = "Alamat_Mitra"
        Me.Alamat_Mitra.ReadOnly = True
        Me.Alamat_Mitra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Alamat_Mitra.Visible = False
        '
        'Kode_Project
        '
        Me.Kode_Project.HeaderText = "Kode Project"
        Me.Kode_Project.Name = "Kode_Project"
        Me.Kode_Project.ReadOnly = True
        Me.Kode_Project.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Project.Visible = False
        Me.Kode_Project.Width = 180
        '
        'frm_ListInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 469)
        Me.Controls.Add(Me.cmb_Mitra)
        Me.Controls.Add(Me.lbl_FilterMitra)
        Me.Controls.Add(Me.btn_Pilih)
        Me.Controls.Add(Me.lbl_CariInvoice)
        Me.Controls.Add(Me.txt_CariInvoice)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_ListInvoice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daftar Invoice"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Pilih As Button
    Friend WithEvents lbl_CariInvoice As Label
    Friend WithEvents txt_CariInvoice As TextBox
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents cmb_Mitra As ComboBox
    Friend WithEvents lbl_FilterMitra As Label
    Friend WithEvents Nomor_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Tanggal_Invoice As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Alamat_Mitra As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
End Class
