<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PilihJenisProdukInduk
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_Lanjutkan = New System.Windows.Forms.Button()
        Me.rdb_Barang = New System.Windows.Forms.RadioButton()
        Me.rdb_Jasa = New System.Windows.Forms.RadioButton()
        Me.rdb_BarangDanJasa = New System.Windows.Forms.RadioButton()
        Me.rdb_JasaKonstruksi = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Silakan Pilih Jenis Produk :"
        '
        'btn_Lanjutkan
        '
        Me.btn_Lanjutkan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Lanjutkan.Location = New System.Drawing.Point(98, 171)
        Me.btn_Lanjutkan.Name = "btn_Lanjutkan"
        Me.btn_Lanjutkan.Size = New System.Drawing.Size(99, 35)
        Me.btn_Lanjutkan.TabIndex = 901
        Me.btn_Lanjutkan.Text = "Lanjutkan >>"
        Me.btn_Lanjutkan.UseVisualStyleBackColor = True
        '
        'rdb_Barang
        '
        Me.rdb_Barang.AutoSize = True
        Me.rdb_Barang.Location = New System.Drawing.Point(26, 55)
        Me.rdb_Barang.Name = "rdb_Barang"
        Me.rdb_Barang.Size = New System.Drawing.Size(59, 17)
        Me.rdb_Barang.TabIndex = 10
        Me.rdb_Barang.TabStop = True
        Me.rdb_Barang.Text = "Barang"
        Me.rdb_Barang.UseVisualStyleBackColor = True
        '
        'rdb_Jasa
        '
        Me.rdb_Jasa.AutoSize = True
        Me.rdb_Jasa.Location = New System.Drawing.Point(26, 78)
        Me.rdb_Jasa.Name = "rdb_Jasa"
        Me.rdb_Jasa.Size = New System.Drawing.Size(47, 17)
        Me.rdb_Jasa.TabIndex = 20
        Me.rdb_Jasa.TabStop = True
        Me.rdb_Jasa.Text = "Jasa"
        Me.rdb_Jasa.UseVisualStyleBackColor = True
        '
        'rdb_BarangDanJasa
        '
        Me.rdb_BarangDanJasa.AutoSize = True
        Me.rdb_BarangDanJasa.Location = New System.Drawing.Point(26, 101)
        Me.rdb_BarangDanJasa.Name = "rdb_BarangDanJasa"
        Me.rdb_BarangDanJasa.Size = New System.Drawing.Size(105, 17)
        Me.rdb_BarangDanJasa.TabIndex = 30
        Me.rdb_BarangDanJasa.TabStop = True
        Me.rdb_BarangDanJasa.Text = "Barang dan Jasa"
        Me.rdb_BarangDanJasa.UseVisualStyleBackColor = True
        '
        'rdb_JasaKonstruksi
        '
        Me.rdb_JasaKonstruksi.AutoSize = True
        Me.rdb_JasaKonstruksi.Location = New System.Drawing.Point(26, 124)
        Me.rdb_JasaKonstruksi.Name = "rdb_JasaKonstruksi"
        Me.rdb_JasaKonstruksi.Size = New System.Drawing.Size(99, 17)
        Me.rdb_JasaKonstruksi.TabIndex = 40
        Me.rdb_JasaKonstruksi.TabStop = True
        Me.rdb_JasaKonstruksi.Text = "Jasa Konstruksi"
        Me.rdb_JasaKonstruksi.UseVisualStyleBackColor = True
        '
        'frm_PilihJenisProdukInduk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(209, 218)
        Me.Controls.Add(Me.rdb_JasaKonstruksi)
        Me.Controls.Add(Me.rdb_BarangDanJasa)
        Me.Controls.Add(Me.rdb_Jasa)
        Me.Controls.Add(Me.rdb_Barang)
        Me.Controls.Add(Me.btn_Lanjutkan)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_PilihJenisProdukInduk"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pilih Jenis Produk"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btn_Lanjutkan As Button
    Friend WithEvents rdb_Barang As RadioButton
    Friend WithEvents rdb_Jasa As RadioButton
    Friend WithEvents rdb_BarangDanJasa As RadioButton
    Friend WithEvents rdb_JasaKonstruksi As RadioButton
End Class
