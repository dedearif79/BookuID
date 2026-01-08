<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_ListDataPemegangSaham_X
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
        Me.txt_CariPemegangSaham = New System.Windows.Forms.TextBox()
        Me.btn_Pilih = New System.Windows.Forms.Button()
        Me.DataTabelUtama = New System.Windows.Forms.DataGridView()
        Me.NIK_ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nama_Pemegang_Saham = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_CariInvoice
        '
        Me.lbl_CariInvoice.AutoSize = True
        Me.lbl_CariInvoice.Location = New System.Drawing.Point(12, 9)
        Me.lbl_CariInvoice.Name = "lbl_CariInvoice"
        Me.lbl_CariInvoice.Size = New System.Drawing.Size(121, 13)
        Me.lbl_CariInvoice.TabIndex = 10059
        Me.lbl_CariInvoice.Text = "Cari Pemegang Saham :"
        '
        'txt_CariPemegangSaham
        '
        Me.txt_CariPemegangSaham.Location = New System.Drawing.Point(12, 28)
        Me.txt_CariPemegangSaham.Name = "txt_CariPemegangSaham"
        Me.txt_CariPemegangSaham.Size = New System.Drawing.Size(132, 20)
        Me.txt_CariPemegangSaham.TabIndex = 10058
        '
        'btn_Pilih
        '
        Me.btn_Pilih.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Pilih.Enabled = False
        Me.btn_Pilih.Location = New System.Drawing.Point(275, 431)
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
        Me.DataTabelUtama.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NIK_, Me.Nama_Pemegang_Saham})
        Me.DataTabelUtama.Location = New System.Drawing.Point(12, 54)
        Me.DataTabelUtama.MultiSelect = False
        Me.DataTabelUtama.Name = "DataTabelUtama"
        Me.DataTabelUtama.ReadOnly = True
        Me.DataTabelUtama.RowHeadersVisible = False
        Me.DataTabelUtama.RowHeadersWidth = 33
        Me.DataTabelUtama.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataTabelUtama.Size = New System.Drawing.Size(333, 360)
        Me.DataTabelUtama.TabIndex = 10054
        '
        'NIK_
        '
        Me.NIK_.HeaderText = "NIK"
        Me.NIK_.Name = "NIK_"
        Me.NIK_.ReadOnly = True
        Me.NIK_.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NIK_.Width = 117
        '
        'Nama_Pemegang_Saham
        '
        Me.Nama_Pemegang_Saham.HeaderText = "Nama Pemegang Saham"
        Me.Nama_Pemegang_Saham.Name = "Nama_Pemegang_Saham"
        Me.Nama_Pemegang_Saham.ReadOnly = True
        Me.Nama_Pemegang_Saham.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nama_Pemegang_Saham.Width = 210
        '
        'frm_ListDataPemegangSaham
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 492)
        Me.Controls.Add(Me.lbl_CariInvoice)
        Me.Controls.Add(Me.txt_CariPemegangSaham)
        Me.Controls.Add(Me.btn_Pilih)
        Me.Controls.Add(Me.DataTabelUtama)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_ListDataPemegangSaham"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Data Pemegang Saham"
        CType(Me.DataTabelUtama, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbl_CariInvoice As Label
    Friend WithEvents txt_CariPemegangSaham As TextBox
    Friend WithEvents btn_Pilih As Button
    Friend WithEvents DataTabelUtama As DataGridView
    Friend WithEvents NIK_ As DataGridViewTextBoxColumn
    Friend WithEvents Nama_Pemegang_Saham As DataGridViewTextBoxColumn
End Class
