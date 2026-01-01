<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_TautanCOA
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.Nomor_Urut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tautan_COA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kode_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Akun = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status_Tautan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_Tutup = New System.Windows.Forms.Button()
        Me.btn_EditTautan = New System.Windows.Forms.Button()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_JumlahTautan = New System.Windows.Forms.Label()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToResizeRows = False
        Me.DataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nomor_Urut, Me.Tautan_COA, Me.Kode_Akun, Me.Nama_Akun, Me.Status_Tautan})
        Me.DataGridView.Location = New System.Drawing.Point(12, 128)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(3)
        Me.DataGridView.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView.RowTemplate.Height = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(786, 445)
        Me.DataGridView.TabIndex = 10019
        '
        'Nomor_Urut
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Nomor_Urut.DefaultCellStyle = DataGridViewCellStyle1
        Me.Nomor_Urut.HeaderText = "No."
        Me.Nomor_Urut.Name = "Nomor_Urut"
        Me.Nomor_Urut.ReadOnly = True
        Me.Nomor_Urut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nomor_Urut.Width = 45
        '
        'Tautan_COA
        '
        Me.Tautan_COA.HeaderText = "Tautan COA"
        Me.Tautan_COA.Name = "Tautan_COA"
        Me.Tautan_COA.ReadOnly = True
        Me.Tautan_COA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Tautan_COA.Width = 270
        '
        'Kode_Akun
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Kode_Akun.DefaultCellStyle = DataGridViewCellStyle2
        Me.Kode_Akun.HeaderText = "Kode Akun"
        Me.Kode_Akun.Name = "Kode_Akun"
        Me.Kode_Akun.ReadOnly = True
        Me.Kode_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Kode_Akun.Width = 63
        '
        'Nama_Akun
        '
        Me.Nama_Akun.HeaderText = "Nama Akun"
        Me.Nama_Akun.Name = "Nama_Akun"
        Me.Nama_Akun.ReadOnly = True
        Me.Nama_Akun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Akun.Width = 270
        '
        'Status_Tautan
        '
        Me.Status_Tautan.HeaderText = "Status"
        Me.Status_Tautan.Name = "Status_Tautan"
        Me.Status_Tautan.ReadOnly = True
        Me.Status_Tautan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Status_Tautan.Width = 120
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 25)
        Me.Label1.TabIndex = 10023
        Me.Label1.Text = "Tautan COA"
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Tutup.Location = New System.Drawing.Point(715, 584)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tutup.TabIndex = 10027
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'btn_EditTautan
        '
        Me.btn_EditTautan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_EditTautan.Location = New System.Drawing.Point(703, 80)
        Me.btn_EditTautan.Name = "btn_EditTautan"
        Me.btn_EditTautan.Size = New System.Drawing.Size(95, 42)
        Me.btn_EditTautan.TabIndex = 10028
        Me.btn_EditTautan.Text = "Edit Tautan"
        Me.btn_EditTautan.UseVisualStyleBackColor = True
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Location = New System.Drawing.Point(12, 57)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(81, 65)
        Me.btn_Refresh.TabIndex = 10039
        Me.btn_Refresh.Text = "Refresh"
        Me.btn_Refresh.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 584)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 10040
        Me.Label2.Text = "Jumlah Tautan :"
        '
        'lbl_JumlahTautan
        '
        Me.lbl_JumlahTautan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_JumlahTautan.AutoSize = True
        Me.lbl_JumlahTautan.Location = New System.Drawing.Point(98, 584)
        Me.lbl_JumlahTautan.Name = "lbl_JumlahTautan"
        Me.lbl_JumlahTautan.Size = New System.Drawing.Size(13, 13)
        Me.lbl_JumlahTautan.TabIndex = 10041
        Me.lbl_JumlahTautan.Text = "0"
        '
        'frm_TautanCOA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 631)
        Me.Controls.Add(Me.lbl_JumlahTautan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.btn_EditTautan)
        Me.Controls.Add(Me.btn_Tutup)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_TautanCOA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Tautan COA"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents btn_EditTautan As System.Windows.Forms.Button
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents Nomor_Urut As DataGridViewTextBoxColumn
    Friend WithEvents Tautan_COA As DataGridViewTextBoxColumn
    Friend WithEvents Kode_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Akun As DataGridViewTextBoxColumn
    Friend WithEvents Status_Tautan As DataGridViewTextBoxColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents lbl_JumlahTautan As Label
End Class
