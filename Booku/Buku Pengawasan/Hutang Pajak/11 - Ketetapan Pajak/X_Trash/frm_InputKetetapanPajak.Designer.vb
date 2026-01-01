<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputKetetapanPajak
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.txt_JumlahKetetapan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_NomorKetetapan = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtp_TanggalKetetapan = New System.Windows.Forms.DateTimePicker()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txt_PokokPajak = New System.Windows.Forms.TextBox()
        Me.txt_Sanksi = New System.Windows.Forms.TextBox()
        Me.cmb_MasaPajak_Awal = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Nomor = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_KodeJenisPajak = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmb_MasaPajak_Akhir = New System.Windows.Forms.ComboBox()
        Me.txt_TahunPajak_Inputan = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_JenisPajak = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_KodeAkun_PokokPajak = New System.Windows.Forms.TextBox()
        Me.txt_NamaAkun_PokokPajak = New System.Windows.Forms.TextBox()
        Me.lbl_COA_PokokPajak = New System.Windows.Forms.Label()
        Me.lbl_COA_Sanksi = New System.Windows.Forms.Label()
        Me.txt_KodeAkun_Sanksi = New System.Windows.Forms.TextBox()
        Me.txt_NamaAkun_Sanksi = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(259, 525)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9900
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(170, 525)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 419)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 10365
        Me.Label5.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(29, 440)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(313, 55)
        Me.txt_Keterangan.TabIndex = 500
        Me.txt_Keterangan.Text = ""
        '
        'txt_JumlahKetetapan
        '
        Me.txt_JumlahKetetapan.Location = New System.Drawing.Point(137, 383)
        Me.txt_JumlahKetetapan.MaxLength = 21
        Me.txt_JumlahKetetapan.Name = "txt_JumlahKetetapan"
        Me.txt_JumlahKetetapan.Size = New System.Drawing.Size(95, 20)
        Me.txt_JumlahKetetapan.TabIndex = 400
        Me.txt_JumlahKetetapan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 386)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 10363
        Me.Label3.Text = "Jumlah Ketetapan"
        '
        'txt_NomorKetetapan
        '
        Me.txt_NomorKetetapan.Location = New System.Drawing.Point(137, 143)
        Me.txt_NomorKetetapan.MaxLength = 99
        Me.txt_NomorKetetapan.Name = "txt_NomorKetetapan"
        Me.txt_NomorKetetapan.Size = New System.Drawing.Size(205, 20)
        Me.txt_NomorKetetapan.TabIndex = 80
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(26, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 10362
        Me.Label2.Text = "Nomor Ketetapan"
        '
        'dtp_TanggalKetetapan
        '
        Me.dtp_TanggalKetetapan.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalKetetapan.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalKetetapan.Location = New System.Drawing.Point(137, 38)
        Me.dtp_TanggalKetetapan.Name = "dtp_TanggalKetetapan"
        Me.dtp_TanggalKetetapan.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalKetetapan.TabIndex = 10
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(26, 41)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(98, 13)
        Me.Label50.TabIndex = 10368
        Me.Label50.Text = "Tanggal Ketetapan"
        '
        'txt_PokokPajak
        '
        Me.txt_PokokPajak.Location = New System.Drawing.Point(53, 28)
        Me.txt_PokokPajak.MaxLength = 21
        Me.txt_PokokPajak.Name = "txt_PokokPajak"
        Me.txt_PokokPajak.Size = New System.Drawing.Size(95, 20)
        Me.txt_PokokPajak.TabIndex = 210
        Me.txt_PokokPajak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Sanksi
        '
        Me.txt_Sanksi.Location = New System.Drawing.Point(53, 28)
        Me.txt_Sanksi.MaxLength = 21
        Me.txt_Sanksi.Name = "txt_Sanksi"
        Me.txt_Sanksi.Size = New System.Drawing.Size(95, 20)
        Me.txt_Sanksi.TabIndex = 310
        Me.txt_Sanksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmb_MasaPajak_Awal
        '
        Me.cmb_MasaPajak_Awal.FormattingEnabled = True
        Me.cmb_MasaPajak_Awal.Location = New System.Drawing.Point(137, 90)
        Me.cmb_MasaPajak_Awal.Name = "cmb_MasaPajak_Awal"
        Me.cmb_MasaPajak_Awal.Size = New System.Drawing.Size(81, 21)
        Me.cmb_MasaPajak_Awal.TabIndex = 50
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(26, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 13)
        Me.Label7.TabIndex = 10374
        Me.Label7.Text = "Masa Pajak"
        '
        'txt_Nomor
        '
        Me.txt_Nomor.Location = New System.Drawing.Point(290, 38)
        Me.txt_Nomor.MaxLength = 99
        Me.txt_Nomor.Name = "txt_Nomor"
        Me.txt_Nomor.Size = New System.Drawing.Size(52, 20)
        Me.txt_Nomor.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(246, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 10376
        Me.Label8.Text = "Nomor"
        '
        'txt_KodeJenisPajak
        '
        Me.txt_KodeJenisPajak.Location = New System.Drawing.Point(137, 64)
        Me.txt_KodeJenisPajak.MaxLength = 3
        Me.txt_KodeJenisPajak.Name = "txt_KodeJenisPajak"
        Me.txt_KodeJenisPajak.Size = New System.Drawing.Size(52, 20)
        Me.txt_KodeJenisPajak.TabIndex = 30
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(26, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 10378
        Me.Label9.Text = "Kode Jenis Pajak"
        '
        'cmb_MasaPajak_Akhir
        '
        Me.cmb_MasaPajak_Akhir.FormattingEnabled = True
        Me.cmb_MasaPajak_Akhir.Location = New System.Drawing.Point(261, 90)
        Me.cmb_MasaPajak_Akhir.Name = "cmb_MasaPajak_Akhir"
        Me.cmb_MasaPajak_Akhir.Size = New System.Drawing.Size(81, 21)
        Me.cmb_MasaPajak_Akhir.TabIndex = 60
        '
        'txt_TahunPajak_Inputan
        '
        Me.txt_TahunPajak_Inputan.Location = New System.Drawing.Point(137, 117)
        Me.txt_TahunPajak_Inputan.MaxLength = 4
        Me.txt_TahunPajak_Inputan.Name = "txt_TahunPajak_Inputan"
        Me.txt_TahunPajak_Inputan.Size = New System.Drawing.Size(52, 20)
        Me.txt_TahunPajak_Inputan.TabIndex = 70
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(26, 120)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 10381
        Me.Label10.Text = "Tahun Pajak"
        '
        'txt_JenisPajak
        '
        Me.txt_JenisPajak.Location = New System.Drawing.Point(195, 64)
        Me.txt_JenisPajak.MaxLength = 99
        Me.txt_JenisPajak.Name = "txt_JenisPajak"
        Me.txt_JenisPajak.Size = New System.Drawing.Size(147, 20)
        Me.txt_JenisPajak.TabIndex = 40
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(227, 93)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 13)
        Me.Label12.TabIndex = 10384
        Me.Label12.Text = "s.d."
        '
        'txt_KodeAkun_PokokPajak
        '
        Me.txt_KodeAkun_PokokPajak.Location = New System.Drawing.Point(53, 54)
        Me.txt_KodeAkun_PokokPajak.MaxLength = 3
        Me.txt_KodeAkun_PokokPajak.Name = "txt_KodeAkun_PokokPajak"
        Me.txt_KodeAkun_PokokPajak.Size = New System.Drawing.Size(52, 20)
        Me.txt_KodeAkun_PokokPajak.TabIndex = 220
        '
        'txt_NamaAkun_PokokPajak
        '
        Me.txt_NamaAkun_PokokPajak.Location = New System.Drawing.Point(111, 54)
        Me.txt_NamaAkun_PokokPajak.MaxLength = 99
        Me.txt_NamaAkun_PokokPajak.Name = "txt_NamaAkun_PokokPajak"
        Me.txt_NamaAkun_PokokPajak.Size = New System.Drawing.Size(211, 20)
        Me.txt_NamaAkun_PokokPajak.TabIndex = 230
        '
        'lbl_COA_PokokPajak
        '
        Me.lbl_COA_PokokPajak.AutoSize = True
        Me.lbl_COA_PokokPajak.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_COA_PokokPajak.Location = New System.Drawing.Point(7, 57)
        Me.lbl_COA_PokokPajak.Name = "lbl_COA_PokokPajak"
        Me.lbl_COA_PokokPajak.Size = New System.Drawing.Size(29, 13)
        Me.lbl_COA_PokokPajak.TabIndex = 10389
        Me.lbl_COA_PokokPajak.Text = "COA"
        '
        'lbl_COA_Sanksi
        '
        Me.lbl_COA_Sanksi.AutoSize = True
        Me.lbl_COA_Sanksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_COA_Sanksi.Location = New System.Drawing.Point(7, 57)
        Me.lbl_COA_Sanksi.Name = "lbl_COA_Sanksi"
        Me.lbl_COA_Sanksi.Size = New System.Drawing.Size(29, 13)
        Me.lbl_COA_Sanksi.TabIndex = 10391
        Me.lbl_COA_Sanksi.Text = "COA"
        '
        'txt_KodeAkun_Sanksi
        '
        Me.txt_KodeAkun_Sanksi.Location = New System.Drawing.Point(53, 54)
        Me.txt_KodeAkun_Sanksi.MaxLength = 3
        Me.txt_KodeAkun_Sanksi.Name = "txt_KodeAkun_Sanksi"
        Me.txt_KodeAkun_Sanksi.Size = New System.Drawing.Size(52, 20)
        Me.txt_KodeAkun_Sanksi.TabIndex = 320
        '
        'txt_NamaAkun_Sanksi
        '
        Me.txt_NamaAkun_Sanksi.Location = New System.Drawing.Point(111, 54)
        Me.txt_NamaAkun_Sanksi.MaxLength = 99
        Me.txt_NamaAkun_Sanksi.Name = "txt_NamaAkun_Sanksi"
        Me.txt_NamaAkun_Sanksi.Size = New System.Drawing.Size(211, 20)
        Me.txt_NamaAkun_Sanksi.TabIndex = 330
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txt_PokokPajak)
        Me.GroupBox1.Controls.Add(Me.txt_KodeAkun_PokokPajak)
        Me.GroupBox1.Controls.Add(Me.lbl_COA_PokokPajak)
        Me.GroupBox1.Controls.Add(Me.txt_NamaAkun_PokokPajak)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 174)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(330, 94)
        Me.GroupBox1.TabIndex = 200
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pokok Pajak :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 31)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 10393
        Me.Label6.Text = "Jumlah"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txt_Sanksi)
        Me.GroupBox2.Controls.Add(Me.txt_NamaAkun_Sanksi)
        Me.GroupBox2.Controls.Add(Me.txt_KodeAkun_Sanksi)
        Me.GroupBox2.Controls.Add(Me.lbl_COA_Sanksi)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 277)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(330, 94)
        Me.GroupBox2.TabIndex = 300
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sanksi :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 10394
        Me.Label1.Text = "Jumlah"
        '
        'frm_InputKetetapanPajak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(371, 588)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txt_JenisPajak)
        Me.Controls.Add(Me.txt_TahunPajak_Inputan)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmb_MasaPajak_Akhir)
        Me.Controls.Add(Me.txt_KodeJenisPajak)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_Nomor)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmb_MasaPajak_Awal)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtp_TanggalKetetapan)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_JumlahKetetapan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_NomorKetetapan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputKetetapanPajak"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Ketetapan Pajak"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents txt_JumlahKetetapan As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_NomorKetetapan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents dtp_TanggalKetetapan As DateTimePicker
    Friend WithEvents Label50 As Label
    Friend WithEvents txt_PokokPajak As TextBox
    Friend WithEvents txt_Sanksi As TextBox
    Friend WithEvents cmb_MasaPajak_Awal As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_Nomor As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_KodeJenisPajak As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents cmb_MasaPajak_Akhir As ComboBox
    Friend WithEvents txt_TahunPajak_Inputan As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_JenisPajak As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txt_KodeAkun_PokokPajak As TextBox
    Friend WithEvents txt_NamaAkun_PokokPajak As TextBox
    Friend WithEvents lbl_COA_PokokPajak As Label
    Friend WithEvents lbl_COA_Sanksi As Label
    Friend WithEvents txt_KodeAkun_Sanksi As TextBox
    Friend WithEvents txt_NamaAkun_Sanksi As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label1 As Label
End Class
