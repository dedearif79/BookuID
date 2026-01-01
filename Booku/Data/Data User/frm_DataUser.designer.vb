<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DataUser
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btn_Tambah = New System.Windows.Forms.Button()
        Me.btn_Edit = New System.Windows.Forms.Button()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.Username_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Password_User = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Level_User = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_User = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Jabatan_User = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cluster_User = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cluster_Finance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cluster_Accounting = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status_Aktif = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_Blokir = New System.Windows.Forms.Button()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Tambah
        '
        Me.btn_Tambah.Location = New System.Drawing.Point(542, 85)
        Me.btn_Tambah.Name = "btn_Tambah"
        Me.btn_Tambah.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tambah.TabIndex = 351
        Me.btn_Tambah.Text = "Tambah"
        Me.btn_Tambah.UseVisualStyleBackColor = True
        '
        'btn_Edit
        '
        Me.btn_Edit.Enabled = False
        Me.btn_Edit.Location = New System.Drawing.Point(631, 85)
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
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Username_, Me.Password_User, Me.Level_User, Me.Nama_User, Me.Jabatan_User, Me.Cluster_User, Me.Cluster_Finance, Me.Cluster_Accounting, Me.Status_Aktif})
        Me.DataGridView.Location = New System.Drawing.Point(12, 126)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(791, 330)
        Me.DataGridView.TabIndex = 10018
        '
        'Username_
        '
        Me.Username_.HeaderText = "Username"
        Me.Username_.Name = "Username_"
        Me.Username_.ReadOnly = True
        Me.Username_.Width = 150
        '
        'Password_User
        '
        Me.Password_User.HeaderText = "Password"
        Me.Password_User.Name = "Password_User"
        Me.Password_User.ReadOnly = True
        Me.Password_User.Visible = False
        '
        'Level_User
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Level_User.DefaultCellStyle = DataGridViewCellStyle2
        Me.Level_User.HeaderText = "Level"
        Me.Level_User.Name = "Level_User"
        Me.Level_User.ReadOnly = True
        Me.Level_User.Visible = False
        Me.Level_User.Width = 54
        '
        'Nama_User
        '
        Me.Nama_User.HeaderText = "Nama"
        Me.Nama_User.Name = "Nama_User"
        Me.Nama_User.ReadOnly = True
        Me.Nama_User.Width = 240
        '
        'Jabatan_User
        '
        Me.Jabatan_User.HeaderText = "Jabatan"
        Me.Jabatan_User.Name = "Jabatan_User"
        Me.Jabatan_User.ReadOnly = True
        Me.Jabatan_User.Width = 180
        '
        'Cluster_User
        '
        Me.Cluster_User.HeaderText = "Cluster"
        Me.Cluster_User.Name = "Cluster_User"
        Me.Cluster_User.ReadOnly = True
        Me.Cluster_User.Width = 150
        '
        'Cluster_Finance
        '
        Me.Cluster_Finance.HeaderText = "Fnc"
        Me.Cluster_Finance.Name = "Cluster_Finance"
        Me.Cluster_Finance.ReadOnly = True
        Me.Cluster_Finance.Visible = False
        Me.Cluster_Finance.Width = 33
        '
        'Cluster_Accounting
        '
        Me.Cluster_Accounting.HeaderText = "Acc"
        Me.Cluster_Accounting.Name = "Cluster_Accounting"
        Me.Cluster_Accounting.ReadOnly = True
        Me.Cluster_Accounting.Visible = False
        Me.Cluster_Accounting.Width = 33
        '
        'Status_Aktif
        '
        Me.Status_Aktif.HeaderText = "Status Aktif"
        Me.Status_Aktif.Name = "Status_Aktif"
        Me.Status_Aktif.ReadOnly = True
        Me.Status_Aktif.Width = 63
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Location = New System.Drawing.Point(12, 55)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(99, 65)
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
        Me.Label6.Size = New System.Drawing.Size(150, 32)
        Me.Label6.TabIndex = 10030
        Me.Label6.Text = "Data User"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btn_Blokir
        '
        Me.btn_Blokir.Enabled = False
        Me.btn_Blokir.Location = New System.Drawing.Point(720, 85)
        Me.btn_Blokir.Name = "btn_Blokir"
        Me.btn_Blokir.Size = New System.Drawing.Size(83, 35)
        Me.btn_Blokir.TabIndex = 10031
        Me.btn_Blokir.Text = "Blokir"
        Me.btn_Blokir.UseVisualStyleBackColor = True
        '
        'frm_DataUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 472)
        Me.Controls.Add(Me.btn_Blokir)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_Refresh)
        Me.Controls.Add(Me.DataGridView)
        Me.Controls.Add(Me.btn_Edit)
        Me.Controls.Add(Me.btn_Tambah)
        Me.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.Name = "frm_DataUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Data User"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Tambah As System.Windows.Forms.Button
    Friend WithEvents btn_Edit As System.Windows.Forms.Button
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_Blokir As System.Windows.Forms.Button
    Friend WithEvents Username_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Password_User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Level_User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nama_User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jabatan_User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cluster_User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cluster_Finance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cluster_Accounting As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status_Aktif As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
