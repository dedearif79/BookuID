<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ListDataKaryawan
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
        Me.lbl_CariInvoice = New System.Windows.Forms.Label()
        Me.txt_CariKaryawan = New System.Windows.Forms.TextBox()
        Me.btn_Pilih = New System.Windows.Forms.Button()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.Nomor_ID_Karyawan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NIK_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Karyawan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jabatan_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_CariInvoice
        '
        Me.lbl_CariInvoice.AutoSize = True
        Me.lbl_CariInvoice.Location = New System.Drawing.Point(12, 9)
        Me.lbl_CariInvoice.Name = "lbl_CariInvoice"
        Me.lbl_CariInvoice.Size = New System.Drawing.Size(81, 13)
        Me.lbl_CariInvoice.TabIndex = 10059
        Me.lbl_CariInvoice.Text = "Cari Karyawan :"
        '
        'txt_CariKaryawan
        '
        Me.txt_CariKaryawan.Location = New System.Drawing.Point(12, 28)
        Me.txt_CariKaryawan.Name = "txt_CariKaryawan"
        Me.txt_CariKaryawan.Size = New System.Drawing.Size(132, 20)
        Me.txt_CariKaryawan.TabIndex = 10058
        '
        'btn_Pilih
        '
        Me.btn_Pilih.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pilih.Enabled = False
        Me.btn_Pilih.Location = New System.Drawing.Point(306, 431)
        Me.btn_Pilih.Name = "btn_Pilih"
        Me.btn_Pilih.Size = New System.Drawing.Size(70, 46)
        Me.btn_Pilih.TabIndex = 10055
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_ID_Karyawan, Me.NIK_, Me.Nama_Karyawan, Me.Jabatan_})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 54)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(364, 360)
        Me.DataTabelUtama.TabIndex = 10054
        '
        'Nomor_ID_Karyawan
        '
        Me.Nomor_ID_Karyawan.HeaderText = "Nomor ID"
        Me.Nomor_ID_Karyawan.Name = "Nomor_ID_Karyawan"
        Me.Nomor_ID_Karyawan.ReadOnly = True
        Me.Nomor_ID_Karyawan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_ID_Karyawan.Width = 99
        '
        'NIK_
        '
        Me.NIK_.HeaderText = "NIK"
        Me.NIK_.Name = "NIK_"
        Me.NIK_.ReadOnly = True
        Me.NIK_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NIK_.Visible = False
        Me.NIK_.Width = 117
        '
        'Nama_Karyawan
        '
        Me.Nama_Karyawan.HeaderText = "Nama Karyawan"
        Me.Nama_Karyawan.Name = "Nama_Karyawan"
        Me.Nama_Karyawan.ReadOnly = True
        Me.Nama_Karyawan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Karyawan.Width = 150
        '
        'Jabatan_
        '
        Me.Jabatan_.HeaderText = "Jabatan"
        Me.Jabatan_.Name = "Jabatan_"
        Me.Jabatan_.ReadOnly = True
        Me.Jabatan_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Jabatan_.Width = 105
        '
        'frm_ListDataKaryawan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 492)
        Me.Controls.Add(Me.lbl_CariInvoice)
        Me.Controls.Add(Me.txt_CariKaryawan)
        Me.Controls.Add(Me.btn_Pilih)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_ListDataKaryawan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Data Karyawan"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbl_CariInvoice As Label
    Friend WithEvents txt_CariKaryawan As TextBox
    Friend WithEvents btn_Pilih As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents Nomor_ID_Karyawan As DataGridViewTextBoxColumn
    Friend WithEvents NIK_ As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Karyawan As DataGridViewTextBoxColumn
    Friend WithEvents Jabatan_ As DataGridViewTextBoxColumn
End Class
