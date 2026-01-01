<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ListCOA
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
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_CariAkun = New System.Windows.Forms.TextBox()
        Me.lbl_CariCOA = New System.Windows.Forms.Label()
        Me.btn_Pilih = New System.Windows.Forms.Button()
        Me.btn_Tutup = New System.Windows.Forms.Button()
        Me.chk_TampilkanYangTersembunyi = New System.Windows.Forms.CheckBox()
        Me.COA_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Visibilitas_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.COA_, Me.Nama_Akun, Me.Visibilitas_})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 64)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(398, 341)
        Me.DataTabelUtama.TabIndex = 20
        '
        'btn_Batal
        '
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(39, 534)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(88, 33)
        Me.btn_Batal.TabIndex = 10032
        Me.btn_Batal.Text = "! Jangan Dihapus !"
        Me.btn_Batal.UseVisualStyleBackColor = True
        Me.btn_Batal.Visible = False
        '
        'txt_CariAkun
        '
        Me.txt_CariAkun.Location = New System.Drawing.Point(12, 35)
        Me.txt_CariAkun.Name = "txt_CariAkun"
        Me.txt_CariAkun.Size = New System.Drawing.Size(174, 20)
        Me.txt_CariAkun.TabIndex = 10
        '
        'lbl_CariCOA
        '
        Me.lbl_CariCOA.AutoSize = True
        Me.lbl_CariCOA.Location = New System.Drawing.Point(12, 16)
        Me.lbl_CariCOA.Name = "lbl_CariCOA"
        Me.lbl_CariCOA.Size = New System.Drawing.Size(56, 13)
        Me.lbl_CariCOA.TabIndex = 10034
        Me.lbl_CariCOA.Text = "Cari COA :"
        '
        'btn_Pilih
        '
        Me.btn_Pilih.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pilih.Location = New System.Drawing.Point(340, 416)
        Me.btn_Pilih.Name = "btn_Pilih"
        Me.btn_Pilih.Size = New System.Drawing.Size(70, 46)
        Me.btn_Pilih.TabIndex = 10035
        Me.btn_Pilih.Text = "Pilih"
        Me.btn_Pilih.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Tutup.Location = New System.Drawing.Point(254, 416)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(80, 46)
        Me.btn_Tutup.TabIndex = 10037
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'chk_TampilkanYangTersembunyi
        '
        Me.chk_TampilkanYangTersembunyi.AutoSize = True
        Me.chk_TampilkanYangTersembunyi.Location = New System.Drawing.Point(12, 411)
        Me.chk_TampilkanYangTersembunyi.Name = "chk_TampilkanYangTersembunyi"
        Me.chk_TampilkanYangTersembunyi.Size = New System.Drawing.Size(160, 17)
        Me.chk_TampilkanYangTersembunyi.TabIndex = 10039
        Me.chk_TampilkanYangTersembunyi.Text = "Tampilkan yang tersembunyi"
        Me.chk_TampilkanYangTersembunyi.UseVisualStyleBackColor = True
        '
        'COA_
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.COA_.DefaultCellStyle = DataGridViewCellStyle1
        Me.COA_.HeaderText = "COA"
        Me.COA_.Name = "COA_"
        Me.COA_.ReadOnly = True
        Me.COA_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.COA_.Width = 63
        '
        'Nama_Akun
        '
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Nama_Akun.DefaultCellStyle = DataGridViewCellStyle2
        Me.Nama_Akun.HeaderText = "Nama Akun"
        Me.Nama_Akun.Name = "Nama_Akun"
        Me.Nama_Akun.ReadOnly = True
        Me.Nama_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Akun.Width = 330
        '
        'Visibilitas_
        '
        Me.Visibilitas_.HeaderText = "Visibilitas"
        Me.Visibilitas_.Name = "Visibilitas_"
        Me.Visibilitas_.ReadOnly = True
        Me.Visibilitas_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Visibilitas_.Visible = False
        Me.Visibilitas_.Width = 72
        '
        'frm_ListCOA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Batal
        Me.ClientSize = New System.Drawing.Size(422, 476)
        Me.Controls.Add(Me.chk_TampilkanYangTersembunyi)
        Me.Controls.Add(Me.btn_Tutup)
        Me.Controls.Add(Me.btn_Pilih)
        Me.Controls.Add(Me.lbl_CariCOA)
        Me.Controls.Add(Me.txt_CariAkun)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_ListCOA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List COA"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataTabelUtama As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents txt_CariAkun As System.Windows.Forms.TextBox
    Friend WithEvents lbl_CariCOA As System.Windows.Forms.Label
    Friend WithEvents btn_Pilih As System.Windows.Forms.Button
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents chk_TampilkanYangTersembunyi As CheckBox
    Friend WithEvents COA_ As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Visibilitas_ As DataGridViewTextBoxColumn
End Class
