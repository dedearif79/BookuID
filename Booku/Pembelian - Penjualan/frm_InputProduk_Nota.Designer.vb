<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputProduk_Nota
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
        Me.txt_NomorUrut = New System.Windows.Forms.TextBox()
        Me.lbl_NomorUrut = New System.Windows.Forms.Label()
        Me.txt_NamaProduk = New System.Windows.Forms.TextBox()
        Me.lbl_NamaProduk = New System.Windows.Forms.Label()
        Me.txt_DeskripsiProduk = New System.Windows.Forms.TextBox()
        Me.lbl_Deskripsi = New System.Windows.Forms.Label()
        Me.txt_JumlahProduk = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahProduk = New System.Windows.Forms.Label()
        Me.txt_Satuan = New System.Windows.Forms.TextBox()
        Me.lbl_Satuan = New System.Windows.Forms.Label()
        Me.txt_HargaSatuan = New System.Windows.Forms.TextBox()
        Me.lbl_HargaSatuan = New System.Windows.Forms.Label()
        Me.txt_TotalHarga = New System.Windows.Forms.TextBox()
        Me.lbl_TotalHarga = New System.Windows.Forms.Label()
        Me.btn_Tambahkan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.lbl_keteranganSatuan = New System.Windows.Forms.Label()
        Me.txt_DiskonPerItem_Rp = New System.Windows.Forms.TextBox()
        Me.lbl_Diskon = New System.Windows.Forms.Label()
        Me.txt_DiskonPerItem_Persen = New System.Windows.Forms.TextBox()
        Me.lbl_Persen = New System.Windows.Forms.Label()
        Me.txt_JumlahHarga = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahHarga = New System.Windows.Forms.Label()
        Me.cmb_JenisProduk = New System.Windows.Forms.ComboBox()
        Me.cmb_Peruntukan = New System.Windows.Forms.ComboBox()
        Me.lbl_Peruntukan = New System.Windows.Forms.Label()
        Me.btn_PilihKodeProject = New System.Windows.Forms.Button()
        Me.txt_KodeProject = New System.Windows.Forms.TextBox()
        Me.lbl_KodeProject = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txt_NomorUrut
        '
        Me.txt_NomorUrut.Location = New System.Drawing.Point(142, 23)
        Me.txt_NomorUrut.Name = "txt_NomorUrut"
        Me.txt_NomorUrut.Size = New System.Drawing.Size(52, 20)
        Me.txt_NomorUrut.TabIndex = 10
        Me.txt_NomorUrut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_NomorUrut
        '
        Me.lbl_NomorUrut.AutoSize = True
        Me.lbl_NomorUrut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomorUrut.Location = New System.Drawing.Point(20, 26)
        Me.lbl_NomorUrut.Name = "lbl_NomorUrut"
        Me.lbl_NomorUrut.Size = New System.Drawing.Size(61, 13)
        Me.lbl_NomorUrut.TabIndex = 10113
        Me.lbl_NomorUrut.Text = "Nomor Urut"
        '
        'txt_NamaProduk
        '
        Me.txt_NamaProduk.Location = New System.Drawing.Point(142, 49)
        Me.txt_NamaProduk.Name = "txt_NamaProduk"
        Me.txt_NamaProduk.Size = New System.Drawing.Size(188, 20)
        Me.txt_NamaProduk.TabIndex = 20
        '
        'lbl_NamaProduk
        '
        Me.lbl_NamaProduk.AutoSize = True
        Me.lbl_NamaProduk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaProduk.Location = New System.Drawing.Point(20, 52)
        Me.lbl_NamaProduk.Name = "lbl_NamaProduk"
        Me.lbl_NamaProduk.Size = New System.Drawing.Size(99, 13)
        Me.lbl_NamaProduk.TabIndex = 10115
        Me.lbl_NamaProduk.Text = "Nama Barang/Jasa"
        '
        'txt_DeskripsiProduk
        '
        Me.txt_DeskripsiProduk.Location = New System.Drawing.Point(142, 75)
        Me.txt_DeskripsiProduk.Name = "txt_DeskripsiProduk"
        Me.txt_DeskripsiProduk.Size = New System.Drawing.Size(188, 20)
        Me.txt_DeskripsiProduk.TabIndex = 30
        '
        'lbl_Deskripsi
        '
        Me.lbl_Deskripsi.AutoSize = True
        Me.lbl_Deskripsi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Deskripsi.Location = New System.Drawing.Point(20, 78)
        Me.lbl_Deskripsi.Name = "lbl_Deskripsi"
        Me.lbl_Deskripsi.Size = New System.Drawing.Size(50, 13)
        Me.lbl_Deskripsi.TabIndex = 10117
        Me.lbl_Deskripsi.Text = "Deskripsi"
        '
        'txt_JumlahProduk
        '
        Me.txt_JumlahProduk.Location = New System.Drawing.Point(142, 101)
        Me.txt_JumlahProduk.Name = "txt_JumlahProduk"
        Me.txt_JumlahProduk.Size = New System.Drawing.Size(106, 20)
        Me.txt_JumlahProduk.TabIndex = 40
        Me.txt_JumlahProduk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahProduk
        '
        Me.lbl_JumlahProduk.AutoSize = True
        Me.lbl_JumlahProduk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JumlahProduk.Location = New System.Drawing.Point(20, 104)
        Me.lbl_JumlahProduk.Name = "lbl_JumlahProduk"
        Me.lbl_JumlahProduk.Size = New System.Drawing.Size(40, 13)
        Me.lbl_JumlahProduk.TabIndex = 10119
        Me.lbl_JumlahProduk.Text = "Jumlah"
        '
        'txt_Satuan
        '
        Me.txt_Satuan.Location = New System.Drawing.Point(142, 127)
        Me.txt_Satuan.Name = "txt_Satuan"
        Me.txt_Satuan.Size = New System.Drawing.Size(64, 20)
        Me.txt_Satuan.TabIndex = 50
        '
        'lbl_Satuan
        '
        Me.lbl_Satuan.AutoSize = True
        Me.lbl_Satuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Satuan.Location = New System.Drawing.Point(20, 130)
        Me.lbl_Satuan.Name = "lbl_Satuan"
        Me.lbl_Satuan.Size = New System.Drawing.Size(41, 13)
        Me.lbl_Satuan.TabIndex = 10121
        Me.lbl_Satuan.Text = "Satuan"
        '
        'txt_HargaSatuan
        '
        Me.txt_HargaSatuan.Location = New System.Drawing.Point(142, 153)
        Me.txt_HargaSatuan.Name = "txt_HargaSatuan"
        Me.txt_HargaSatuan.Size = New System.Drawing.Size(106, 20)
        Me.txt_HargaSatuan.TabIndex = 60
        Me.txt_HargaSatuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_HargaSatuan
        '
        Me.lbl_HargaSatuan.AutoSize = True
        Me.lbl_HargaSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_HargaSatuan.Location = New System.Drawing.Point(20, 156)
        Me.lbl_HargaSatuan.Name = "lbl_HargaSatuan"
        Me.lbl_HargaSatuan.Size = New System.Drawing.Size(73, 13)
        Me.lbl_HargaSatuan.TabIndex = 10123
        Me.lbl_HargaSatuan.Text = "Harga Satuan"
        '
        'txt_TotalHarga
        '
        Me.txt_TotalHarga.Location = New System.Drawing.Point(142, 231)
        Me.txt_TotalHarga.Name = "txt_TotalHarga"
        Me.txt_TotalHarga.Size = New System.Drawing.Size(106, 20)
        Me.txt_TotalHarga.TabIndex = 80
        Me.txt_TotalHarga.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Total
        '
        Me.lbl_TotalHarga.AutoSize = True
        Me.lbl_TotalHarga.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TotalHarga.Location = New System.Drawing.Point(20, 234)
        Me.lbl_TotalHarga.Name = "lbl_Total"
        Me.lbl_TotalHarga.Size = New System.Drawing.Size(31, 13)
        Me.lbl_TotalHarga.TabIndex = 10125
        Me.lbl_TotalHarga.Text = "Total"
        '
        'btn_Tambahkan
        '
        Me.btn_Tambahkan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Tambahkan.Location = New System.Drawing.Point(231, 355)
        Me.btn_Tambahkan.Name = "btn_Tambahkan"
        Me.btn_Tambahkan.Size = New System.Drawing.Size(99, 35)
        Me.btn_Tambahkan.TabIndex = 900
        Me.btn_Tambahkan.Text = "Tambahkan"
        Me.btn_Tambahkan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(142, 355)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'lbl_keteranganSatuan
        '
        Me.lbl_keteranganSatuan.AutoSize = True
        Me.lbl_keteranganSatuan.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_keteranganSatuan.Location = New System.Drawing.Point(208, 130)
        Me.lbl_keteranganSatuan.Name = "lbl_keteranganSatuan"
        Me.lbl_keteranganSatuan.Size = New System.Drawing.Size(94, 13)
        Me.lbl_keteranganSatuan.TabIndex = 10126
        Me.lbl_keteranganSatuan.Text = "(pcs, kg, meter, dll)"
        '
        'txt_DiskonPerItem_Rp
        '
        Me.txt_DiskonPerItem_Rp.Location = New System.Drawing.Point(142, 205)
        Me.txt_DiskonPerItem_Rp.Name = "txt_DiskonPerItem_Rp"
        Me.txt_DiskonPerItem_Rp.Size = New System.Drawing.Size(106, 20)
        Me.txt_DiskonPerItem_Rp.TabIndex = 75
        Me.txt_DiskonPerItem_Rp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Diskon
        '
        Me.lbl_Diskon.AutoSize = True
        Me.lbl_Diskon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diskon.Location = New System.Drawing.Point(20, 208)
        Me.lbl_Diskon.Name = "lbl_Diskon"
        Me.lbl_Diskon.Size = New System.Drawing.Size(40, 13)
        Me.lbl_Diskon.TabIndex = 10128
        Me.lbl_Diskon.Text = "Diskon"
        '
        'txt_DiskonPerItem_Persen
        '
        Me.txt_DiskonPerItem_Persen.Location = New System.Drawing.Point(69, 205)
        Me.txt_DiskonPerItem_Persen.MaxLength = 5
        Me.txt_DiskonPerItem_Persen.Name = "txt_DiskonPerItem_Persen"
        Me.txt_DiskonPerItem_Persen.Size = New System.Drawing.Size(33, 20)
        Me.txt_DiskonPerItem_Persen.TabIndex = 70
        Me.txt_DiskonPerItem_Persen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Persen
        '
        Me.lbl_Persen.AutoSize = True
        Me.lbl_Persen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Persen.Location = New System.Drawing.Point(103, 208)
        Me.lbl_Persen.Name = "lbl_Persen"
        Me.lbl_Persen.Size = New System.Drawing.Size(15, 13)
        Me.lbl_Persen.TabIndex = 10130
        Me.lbl_Persen.Text = "%"
        '
        'txt_JumlahHarga
        '
        Me.txt_JumlahHarga.Location = New System.Drawing.Point(142, 179)
        Me.txt_JumlahHarga.Name = "txt_JumlahHarga"
        Me.txt_JumlahHarga.Size = New System.Drawing.Size(106, 20)
        Me.txt_JumlahHarga.TabIndex = 65
        Me.txt_JumlahHarga.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahHarga
        '
        Me.lbl_JumlahHarga.AutoSize = True
        Me.lbl_JumlahHarga.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JumlahHarga.Location = New System.Drawing.Point(20, 182)
        Me.lbl_JumlahHarga.Name = "lbl_JumlahHarga"
        Me.lbl_JumlahHarga.Size = New System.Drawing.Size(72, 13)
        Me.lbl_JumlahHarga.TabIndex = 10132
        Me.lbl_JumlahHarga.Text = "Jumlah Harga"
        '
        'cmb_JenisProduk
        '
        Me.cmb_JenisProduk.FormattingEnabled = True
        Me.cmb_JenisProduk.Location = New System.Drawing.Point(211, 23)
        Me.cmb_JenisProduk.Name = "cmb_JenisProduk"
        Me.cmb_JenisProduk.Size = New System.Drawing.Size(119, 21)
        Me.cmb_JenisProduk.TabIndex = 15
        '
        'cmb_Peruntukan
        '
        Me.cmb_Peruntukan.FormattingEnabled = True
        Me.cmb_Peruntukan.Location = New System.Drawing.Point(142, 257)
        Me.cmb_Peruntukan.Name = "cmb_Peruntukan"
        Me.cmb_Peruntukan.Size = New System.Drawing.Size(119, 21)
        Me.cmb_Peruntukan.TabIndex = 90
        '
        'lbl_Peruntukan
        '
        Me.lbl_Peruntukan.AutoSize = True
        Me.lbl_Peruntukan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Peruntukan.Location = New System.Drawing.Point(20, 260)
        Me.lbl_Peruntukan.Name = "lbl_Peruntukan"
        Me.lbl_Peruntukan.Size = New System.Drawing.Size(62, 13)
        Me.lbl_Peruntukan.TabIndex = 10134
        Me.lbl_Peruntukan.Text = "Peruntukan"
        '
        'btn_PilihKodeProject
        '
        Me.btn_PilihKodeProject.Location = New System.Drawing.Point(287, 282)
        Me.btn_PilihKodeProject.Name = "btn_PilihKodeProject"
        Me.btn_PilihKodeProject.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihKodeProject.TabIndex = 110
        Me.btn_PilihKodeProject.Text = "Pilih"
        Me.btn_PilihKodeProject.UseVisualStyleBackColor = True
        '
        'txt_KodeProject
        '
        Me.txt_KodeProject.Location = New System.Drawing.Point(142, 284)
        Me.txt_KodeProject.Name = "txt_KodeProject"
        Me.txt_KodeProject.Size = New System.Drawing.Size(139, 20)
        Me.txt_KodeProject.TabIndex = 100
        '
        'lbl_KodeProject
        '
        Me.lbl_KodeProject.AutoSize = True
        Me.lbl_KodeProject.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_KodeProject.Location = New System.Drawing.Point(20, 287)
        Me.lbl_KodeProject.Name = "lbl_KodeProject"
        Me.lbl_KodeProject.Size = New System.Drawing.Size(68, 13)
        Me.lbl_KodeProject.TabIndex = 10358
        Me.lbl_KodeProject.Text = "Kode Project"
        '
        'frm_InputProduk_Nota
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 412)
        Me.Controls.Add(Me.btn_PilihKodeProject)
        Me.Controls.Add(Me.txt_KodeProject)
        Me.Controls.Add(Me.lbl_KodeProject)
        Me.Controls.Add(Me.lbl_Peruntukan)
        Me.Controls.Add(Me.cmb_Peruntukan)
        Me.Controls.Add(Me.cmb_JenisProduk)
        Me.Controls.Add(Me.lbl_JumlahHarga)
        Me.Controls.Add(Me.txt_JumlahHarga)
        Me.Controls.Add(Me.lbl_Persen)
        Me.Controls.Add(Me.txt_DiskonPerItem_Persen)
        Me.Controls.Add(Me.txt_DiskonPerItem_Rp)
        Me.Controls.Add(Me.lbl_Diskon)
        Me.Controls.Add(Me.lbl_keteranganSatuan)
        Me.Controls.Add(Me.btn_Tambahkan)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.txt_TotalHarga)
        Me.Controls.Add(Me.lbl_TotalHarga)
        Me.Controls.Add(Me.txt_HargaSatuan)
        Me.Controls.Add(Me.lbl_HargaSatuan)
        Me.Controls.Add(Me.txt_Satuan)
        Me.Controls.Add(Me.lbl_Satuan)
        Me.Controls.Add(Me.txt_JumlahProduk)
        Me.Controls.Add(Me.lbl_JumlahProduk)
        Me.Controls.Add(Me.txt_DeskripsiProduk)
        Me.Controls.Add(Me.lbl_Deskripsi)
        Me.Controls.Add(Me.txt_NamaProduk)
        Me.Controls.Add(Me.lbl_NamaProduk)
        Me.Controls.Add(Me.txt_NomorUrut)
        Me.Controls.Add(Me.lbl_NomorUrut)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputProduk_Nota"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Barang/Jasa"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_NomorUrut As TextBox
    Friend WithEvents lbl_NomorUrut As Label
    Friend WithEvents txt_NamaProduk As TextBox
    Friend WithEvents lbl_NamaProduk As Label
    Friend WithEvents txt_DeskripsiProduk As TextBox
    Friend WithEvents lbl_Deskripsi As Label
    Friend WithEvents txt_JumlahProduk As TextBox
    Friend WithEvents lbl_JumlahProduk As Label
    Friend WithEvents txt_Satuan As TextBox
    Friend WithEvents lbl_Satuan As Label
    Friend WithEvents txt_HargaSatuan As TextBox
    Friend WithEvents lbl_HargaSatuan As Label
    Friend WithEvents txt_TotalHarga As TextBox
    Friend WithEvents lbl_TotalHarga As Label
    Friend WithEvents btn_Tambahkan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents lbl_keteranganSatuan As Label
    Friend WithEvents txt_DiskonPerItem_Rp As TextBox
    Friend WithEvents lbl_Diskon As Label
    Friend WithEvents txt_DiskonPerItem_Persen As TextBox
    Friend WithEvents lbl_Persen As Label
    Friend WithEvents txt_JumlahHarga As TextBox
    Friend WithEvents lbl_JumlahHarga As Label
    Friend WithEvents cmb_JenisProduk As ComboBox
    Friend WithEvents cmb_Peruntukan As ComboBox
    Friend WithEvents lbl_Peruntukan As Label
    Friend WithEvents btn_PilihKodeProject As Button
    Friend WithEvents txt_KodeProject As TextBox
    Friend WithEvents lbl_KodeProject As Label
End Class
