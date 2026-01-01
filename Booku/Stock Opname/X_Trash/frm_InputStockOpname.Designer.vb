<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputStockOpname
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
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_NamaBarang = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txt_JumlahBarang = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Satuan = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_HargaSatuan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_JumlahHarga = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_Lokasi = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.dtp_TanggalPengecekan = New System.Windows.Forms.DateTimePicker()
        Me.lbl_Cp_TanggalTerdaftar = New System.Windows.Forms.Label()
        Me.lbl_Asal = New System.Windows.Forms.Label()
        Me.cmb_Asal = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(283, 379)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9000
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(194, 379)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_NamaBarang
        '
        Me.txt_NamaBarang.Location = New System.Drawing.Point(145, 48)
        Me.txt_NamaBarang.Name = "txt_NamaBarang"
        Me.txt_NamaBarang.Size = New System.Drawing.Size(221, 20)
        Me.txt_NamaBarang.TabIndex = 10
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(25, 51)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 13)
        Me.Label19.TabIndex = 10329
        Me.Label19.Text = "Nama Barang"
        '
        'txt_JumlahBarang
        '
        Me.txt_JumlahBarang.Location = New System.Drawing.Point(145, 74)
        Me.txt_JumlahBarang.Name = "txt_JumlahBarang"
        Me.txt_JumlahBarang.Size = New System.Drawing.Size(59, 20)
        Me.txt_JumlahBarang.TabIndex = 20
        Me.txt_JumlahBarang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 10331
        Me.Label1.Text = "Jumlah Barang"
        '
        'txt_Satuan
        '
        Me.txt_Satuan.Location = New System.Drawing.Point(145, 100)
        Me.txt_Satuan.Name = "txt_Satuan"
        Me.txt_Satuan.Size = New System.Drawing.Size(93, 20)
        Me.txt_Satuan.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 10333
        Me.Label2.Text = "Satuan"
        '
        'txt_HargaSatuan
        '
        Me.txt_HargaSatuan.Location = New System.Drawing.Point(145, 126)
        Me.txt_HargaSatuan.Name = "txt_HargaSatuan"
        Me.txt_HargaSatuan.Size = New System.Drawing.Size(93, 20)
        Me.txt_HargaSatuan.TabIndex = 40
        Me.txt_HargaSatuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 10335
        Me.Label3.Text = "Harga Satuan"
        '
        'txt_JumlahHarga
        '
        Me.txt_JumlahHarga.Location = New System.Drawing.Point(145, 152)
        Me.txt_JumlahHarga.Name = "txt_JumlahHarga"
        Me.txt_JumlahHarga.Size = New System.Drawing.Size(93, 20)
        Me.txt_JumlahHarga.TabIndex = 50
        Me.txt_JumlahHarga.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 10337
        Me.Label4.Text = "Jumlah Harga"
        '
        'txt_Lokasi
        '
        Me.txt_Lokasi.Location = New System.Drawing.Point(145, 205)
        Me.txt_Lokasi.Name = "txt_Lokasi"
        Me.txt_Lokasi.Size = New System.Drawing.Size(221, 20)
        Me.txt_Lokasi.TabIndex = 60
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 208)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 10339
        Me.Label5.Text = "Lokasi"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(25, 253)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 10405
        Me.Label7.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Keterangan.Location = New System.Drawing.Point(28, 272)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(338, 84)
        Me.txt_Keterangan.TabIndex = 900
        Me.txt_Keterangan.Text = ""
        '
        'dtp_TanggalPengecekan
        '
        Me.dtp_TanggalPengecekan.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalPengecekan.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalPengecekan.Location = New System.Drawing.Point(145, 22)
        Me.dtp_TanggalPengecekan.Name = "dtp_TanggalPengecekan"
        Me.dtp_TanggalPengecekan.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalPengecekan.TabIndex = 5
        '
        'lbl_Cp_TanggalTerdaftar
        '
        Me.lbl_Cp_TanggalTerdaftar.AutoSize = True
        Me.lbl_Cp_TanggalTerdaftar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Cp_TanggalTerdaftar.Location = New System.Drawing.Point(25, 26)
        Me.lbl_Cp_TanggalTerdaftar.Name = "lbl_Cp_TanggalTerdaftar"
        Me.lbl_Cp_TanggalTerdaftar.Size = New System.Drawing.Size(110, 13)
        Me.lbl_Cp_TanggalTerdaftar.TabIndex = 1000000006
        Me.lbl_Cp_TanggalTerdaftar.Text = "Tanggal Pengecekan"
        '
        'lbl_Asal
        '
        Me.lbl_Asal.AutoSize = True
        Me.lbl_Asal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Asal.Location = New System.Drawing.Point(25, 181)
        Me.lbl_Asal.Name = "lbl_Asal"
        Me.lbl_Asal.Size = New System.Drawing.Size(27, 13)
        Me.lbl_Asal.TabIndex = 1000000008
        Me.lbl_Asal.Text = "Asal"
        '
        'cmb_Asal
        '
        Me.cmb_Asal.FormattingEnabled = True
        Me.cmb_Asal.Location = New System.Drawing.Point(145, 178)
        Me.cmb_Asal.Name = "cmb_Asal"
        Me.cmb_Asal.Size = New System.Drawing.Size(95, 21)
        Me.cmb_Asal.TabIndex = 55
        '
        'frm_InputStockOpname
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 445)
        Me.Controls.Add(Me.lbl_Asal)
        Me.Controls.Add(Me.cmb_Asal)
        Me.Controls.Add(Me.dtp_TanggalPengecekan)
        Me.Controls.Add(Me.lbl_Cp_TanggalTerdaftar)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_Lokasi)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_JumlahHarga)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_HargaSatuan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Satuan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_JumlahBarang)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_NamaBarang)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputStockOpname"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Stock Opname"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_NamaBarang As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txt_JumlahBarang As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_Satuan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_HargaSatuan As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_JumlahHarga As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_Lokasi As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents dtp_TanggalPengecekan As DateTimePicker
    Friend WithEvents lbl_Cp_TanggalTerdaftar As Label
    Friend WithEvents lbl_Asal As Label
    Friend WithEvents cmb_Asal As ComboBox
End Class
