<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ListDataProject
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
        Me.btn_Pilih = New System.Windows.Forms.Button()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Kode_Project = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nomor_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Customer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Customer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nilai_Project = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmb_Customer = New System.Windows.Forms.ComboBox()
        Me.lbl_FilterCustomer = New System.Windows.Forms.Label()
        Me.lbl_CariInvoice = New System.Windows.Forms.Label()
        Me.txt_CariKodeProject = New System.Windows.Forms.TextBox()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Pilih
        '
        Me.btn_Pilih.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pilih.Enabled = False
        Me.btn_Pilih.Location = New System.Drawing.Point(473, 431)
        Me.btn_Pilih.Name = "btn_Pilih"
        Me.btn_Pilih.Size = New System.Drawing.Size(70, 46)
        Me.btn_Pilih.TabIndex = 10049
        Me.btn_Pilih.Text = "Pilih"
        Me.btn_Pilih.UseVisualStyleBackColor = True
        '
        'DataTabelUtama
        '
        Me.DataTabelUtama.AllowUserToAddRows = False
        Me.DataTabelUtama.AllowUserToDeleteRows = False
        Me.DataTabelUtama.AllowUserToResizeRows = False
        Me.DataTabelUtama.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataTabelUtama.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataTabelUtama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataTabelUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Kode_Project, Me.Nomor_PO, Me.Kode_Customer, Me.Nama_Customer, Me.Nilai_Project})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 54)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(531, 360)
        Me.DataTabelUtama.TabIndex = 10048
        '
        'Kode_Project
        '
        Me.Kode_Project.HeaderText = "Kode Project"
        Me.Kode_Project.Name = "Kode_Project"
        Me.Kode_Project.ReadOnly = True
        Me.Kode_Project.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Project.Width = 132
        '
        'Nomor_PO
        '
        Me.Nomor_PO.HeaderText = "Nomor PO"
        Me.Nomor_PO.Name = "Nomor_PO"
        Me.Nomor_PO.ReadOnly = True
        Me.Nomor_PO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_PO.Width = 132
        '
        'Kode_Customer
        '
        Me.Kode_Customer.HeaderText = "Kode Customer"
        Me.Kode_Customer.Name = "Kode_Customer"
        Me.Kode_Customer.ReadOnly = True
        Me.Kode_Customer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Customer.Visible = False
        '
        'Nama_Customer
        '
        Me.Nama_Customer.HeaderText = "Customer"
        Me.Nama_Customer.Name = "Nama_Customer"
        Me.Nama_Customer.ReadOnly = True
        Me.Nama_Customer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Customer.Width = 180
        '
        'Nilai_Project
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N0"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Nilai_Project.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nilai_Project.HeaderText = "Nilai Project"
        Me.Nilai_Project.Name = "Nilai_Project"
        Me.Nilai_Project.ReadOnly = True
        Me.Nilai_Project.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nilai_Project.Width = 81
        '
        'cmb_Customer
        '
        Me.cmb_Customer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Customer.FormattingEnabled = True
        Me.cmb_Customer.Location = New System.Drawing.Point(356, 27)
        Me.cmb_Customer.Name = "cmb_Customer"
        Me.cmb_Customer.Size = New System.Drawing.Size(187, 21)
        Me.cmb_Customer.TabIndex = 10050
        '
        'lbl_FilterCustomer
        '
        Me.lbl_FilterCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_FilterCustomer.AutoSize = True
        Me.lbl_FilterCustomer.Location = New System.Drawing.Point(353, 8)
        Me.lbl_FilterCustomer.Name = "lbl_FilterCustomer"
        Me.lbl_FilterCustomer.Size = New System.Drawing.Size(82, 13)
        Me.lbl_FilterCustomer.TabIndex = 10051
        Me.lbl_FilterCustomer.Text = "Filter Customer :"
        '
        'lbl_CariInvoice
        '
        Me.lbl_CariInvoice.AutoSize = True
        Me.lbl_CariInvoice.Location = New System.Drawing.Point(12, 9)
        Me.lbl_CariInvoice.Name = "lbl_CariInvoice"
        Me.lbl_CariInvoice.Size = New System.Drawing.Size(95, 13)
        Me.lbl_CariInvoice.TabIndex = 10053
        Me.lbl_CariInvoice.Text = "Cari Kode Project :"
        '
        'txt_CariKodeProject
        '
        Me.txt_CariKodeProject.Location = New System.Drawing.Point(12, 28)
        Me.txt_CariKodeProject.Name = "txt_CariKodeProject"
        Me.txt_CariKodeProject.Size = New System.Drawing.Size(132, 20)
        Me.txt_CariKodeProject.TabIndex = 10052
        '
        'frm_ListDataProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 492)
        Me.Controls.Add(Me.lbl_CariInvoice)
        Me.Controls.Add(Me.txt_CariKodeProject)
        Me.Controls.Add(Me.cmb_Customer)
        Me.Controls.Add(Me.lbl_FilterCustomer)
        Me.Controls.Add(Me.btn_Pilih)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_ListDataProject"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Data Project"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Pilih As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents cmb_Customer As ComboBox
    Friend WithEvents lbl_FilterCustomer As Label
    Friend WithEvents lbl_CariInvoice As Label
    Friend WithEvents txt_CariKodeProject As TextBox
    Friend WithEvents Kode_Project As DataGridViewTextBoxColumn
    Friend WithEvents Nomor_PO As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Customer As DataGridViewTextBoxColumn
    Friend WithEvents Nilai_Project As DataGridViewTextBoxColumn
End Class
