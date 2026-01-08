<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_KodeDivisi
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.Kode_Divisi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Divisi_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_KodeDivisi = New System.Windows.Forms.TextBox()
        Me.txt_Divisi = New System.Windows.Forms.TextBox()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AllowUserToResizeRows = False
        Me.DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Kode_Divisi, Me.Divisi_})
        Me.DataGridView.Location = New System.Drawing.Point(12, 109)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowHeadersWidth = 33
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView.Size = New System.Drawing.Size(400, 360)
        Me.DataGridView.TabIndex = 31
        '
        'Kode_Divisi
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Kode_Divisi.DefaultCellStyle = DataGridViewCellStyle3
        Me.Kode_Divisi.HeaderText = "Kode Divisi"
        Me.Kode_Divisi.Name = "Kode_Divisi"
        Me.Kode_Divisi.ReadOnly = True
        Me.Kode_Divisi.Width = 63
        '
        'Divisi_
        '
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Divisi_.DefaultCellStyle = DataGridViewCellStyle4
        Me.Divisi_.HeaderText = "Divisi"
        Me.Divisi_.Name = "Divisi_"
        Me.Divisi_.ReadOnly = True
        Me.Divisi_.Width = 330
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Kode Divisi"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Divisi"
        '
        'txt_KodeDivisi
        '
        Me.txt_KodeDivisi.Enabled = False
        Me.txt_KodeDivisi.Location = New System.Drawing.Point(78, 37)
        Me.txt_KodeDivisi.MaxLength = 3
        Me.txt_KodeDivisi.Name = "txt_KodeDivisi"
        Me.txt_KodeDivisi.Size = New System.Drawing.Size(67, 20)
        Me.txt_KodeDivisi.TabIndex = 34
        Me.txt_KodeDivisi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_Divisi
        '
        Me.txt_Divisi.Location = New System.Drawing.Point(78, 63)
        Me.txt_Divisi.Name = "txt_Divisi"
        Me.txt_Divisi.Size = New System.Drawing.Size(229, 20)
        Me.txt_Divisi.TabIndex = 35
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Enabled = False
        Me.btn_Simpan.Location = New System.Drawing.Point(329, 48)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 352
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'frm_KodeDivisi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(430, 481)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.txt_Divisi)
        Me.Controls.Add(Me.txt_KodeDivisi)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_KodeDivisi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kode Divisi"
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Kode_Divisi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Divisi_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_KodeDivisi As System.Windows.Forms.TextBox
    Friend WithEvents txt_Divisi As System.Windows.Forms.TextBox
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
End Class
