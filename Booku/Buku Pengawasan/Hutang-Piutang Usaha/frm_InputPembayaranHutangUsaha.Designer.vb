<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputPembayaranHutangUsaha
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
        Me.btn_Lanjutkan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.lbl_NomorBPHU = New System.Windows.Forms.Label()
        Me.txt_NomorBPHU = New System.Windows.Forms.TextBox()
        Me.txt_NomorPembelian = New System.Windows.Forms.TextBox()
        Me.lbl_NomorPembelian = New System.Windows.Forms.Label()
        Me.txt_JumlahBayarSekarang = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahBayar = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtp_TanggalBayar = New System.Windows.Forms.DateTimePicker()
        Me.txt_JumlahTerutang = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahTerutang = New System.Windows.Forms.Label()
        Me.txt_JumlahTelahDibayar = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahTelahDibayar = New System.Windows.Forms.Label()
        Me.txt_SisaHutang = New System.Windows.Forms.TextBox()
        Me.lbl_SisaHutang = New System.Windows.Forms.Label()
        Me.cmb_SaranaPembayaran = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lbl_PembayaranKe = New System.Windows.Forms.Label()
        Me.txt_JumlahBankCash = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahBankCash = New System.Windows.Forms.Label()
        Me.txt_JumlahPPhDipotong = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahPPhDipotong = New System.Windows.Forms.Label()
        Me.txt_BiayaAdministrasiBank = New System.Windows.Forms.TextBox()
        Me.lbl_BiayaAdministrasiBank = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Lanjutkan
        '
        Me.btn_Lanjutkan.Location = New System.Drawing.Point(405, 248)
        Me.btn_Lanjutkan.Name = "btn_Lanjutkan"
        Me.btn_Lanjutkan.Size = New System.Drawing.Size(149, 35)
        Me.btn_Lanjutkan.TabIndex = 9931
        Me.btn_Lanjutkan.Text = "Lanjutkan >>"
        Me.btn_Lanjutkan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(316, 248)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9933
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'lbl_NomorBPHU
        '
        Me.lbl_NomorBPHU.AutoSize = True
        Me.lbl_NomorBPHU.Location = New System.Drawing.Point(21, 58)
        Me.lbl_NomorBPHU.Name = "lbl_NomorBPHU"
        Me.lbl_NomorBPHU.Size = New System.Drawing.Size(71, 13)
        Me.lbl_NomorBPHU.TabIndex = 9934
        Me.lbl_NomorBPHU.Text = "Nomor BPHU"
        '
        'txt_NomorBPHU
        '
        Me.txt_NomorBPHU.Enabled = False
        Me.txt_NomorBPHU.Location = New System.Drawing.Point(150, 55)
        Me.txt_NomorBPHU.MaxLength = 2
        Me.txt_NomorBPHU.Name = "txt_NomorBPHU"
        Me.txt_NomorBPHU.Size = New System.Drawing.Size(132, 20)
        Me.txt_NomorBPHU.TabIndex = 10
        '
        'txt_NomorPembelian
        '
        Me.txt_NomorPembelian.Enabled = False
        Me.txt_NomorPembelian.Location = New System.Drawing.Point(150, 81)
        Me.txt_NomorPembelian.Name = "txt_NomorPembelian"
        Me.txt_NomorPembelian.Size = New System.Drawing.Size(132, 20)
        Me.txt_NomorPembelian.TabIndex = 20
        '
        'lbl_NomorPembelian
        '
        Me.lbl_NomorPembelian.AutoSize = True
        Me.lbl_NomorPembelian.Location = New System.Drawing.Point(21, 84)
        Me.lbl_NomorPembelian.Name = "lbl_NomorPembelian"
        Me.lbl_NomorPembelian.Size = New System.Drawing.Size(90, 13)
        Me.lbl_NomorPembelian.TabIndex = 9936
        Me.lbl_NomorPembelian.Text = "Nomor Pembelian"
        '
        'txt_JumlahBayarSekarang
        '
        Me.txt_JumlahBayarSekarang.Location = New System.Drawing.Point(150, 185)
        Me.txt_JumlahBayarSekarang.Name = "txt_JumlahBayarSekarang"
        Me.txt_JumlahBayarSekarang.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahBayarSekarang.TabIndex = 60
        Me.txt_JumlahBayarSekarang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahBayar
        '
        Me.lbl_JumlahBayar.AutoSize = True
        Me.lbl_JumlahBayar.Location = New System.Drawing.Point(21, 188)
        Me.lbl_JumlahBayar.Name = "lbl_JumlahBayar"
        Me.lbl_JumlahBayar.Size = New System.Drawing.Size(119, 13)
        Me.lbl_JumlahBayar.TabIndex = 9945
        Me.lbl_JumlahBayar.Text = "Jumlah Bayar Sekarang"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(316, 159)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(238, 83)
        Me.txt_Keterangan.TabIndex = 900
        Me.txt_Keterangan.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(313, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 9947
        Me.Label3.Text = "Keterangan :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 266)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 9949
        Me.Label4.Text = "Tanggal Bayar"
        '
        'dtp_TanggalBayar
        '
        Me.dtp_TanggalBayar.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalBayar.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalBayar.Location = New System.Drawing.Point(150, 263)
        Me.dtp_TanggalBayar.Name = "dtp_TanggalBayar"
        Me.dtp_TanggalBayar.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalBayar.TabIndex = 90
        '
        'txt_JumlahTerutang
        '
        Me.txt_JumlahTerutang.Enabled = False
        Me.txt_JumlahTerutang.Location = New System.Drawing.Point(150, 107)
        Me.txt_JumlahTerutang.Name = "txt_JumlahTerutang"
        Me.txt_JumlahTerutang.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahTerutang.TabIndex = 30
        Me.txt_JumlahTerutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahTerutang
        '
        Me.lbl_JumlahTerutang.AutoSize = True
        Me.lbl_JumlahTerutang.Location = New System.Drawing.Point(21, 110)
        Me.lbl_JumlahTerutang.Name = "lbl_JumlahTerutang"
        Me.lbl_JumlahTerutang.Size = New System.Drawing.Size(86, 13)
        Me.lbl_JumlahTerutang.TabIndex = 10060
        Me.lbl_JumlahTerutang.Text = "Jumlah Terutang"
        '
        'txt_JumlahTelahDibayar
        '
        Me.txt_JumlahTelahDibayar.Enabled = False
        Me.txt_JumlahTelahDibayar.Location = New System.Drawing.Point(150, 133)
        Me.txt_JumlahTelahDibayar.Name = "txt_JumlahTelahDibayar"
        Me.txt_JumlahTelahDibayar.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahTelahDibayar.TabIndex = 40
        Me.txt_JumlahTelahDibayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahTelahDibayar
        '
        Me.lbl_JumlahTelahDibayar.AutoSize = True
        Me.lbl_JumlahTelahDibayar.Location = New System.Drawing.Point(21, 136)
        Me.lbl_JumlahTelahDibayar.Name = "lbl_JumlahTelahDibayar"
        Me.lbl_JumlahTelahDibayar.Size = New System.Drawing.Size(109, 13)
        Me.lbl_JumlahTelahDibayar.TabIndex = 10062
        Me.lbl_JumlahTelahDibayar.Text = "Jumlah Telah Dibayar"
        '
        'txt_SisaHutang
        '
        Me.txt_SisaHutang.Enabled = False
        Me.txt_SisaHutang.Location = New System.Drawing.Point(150, 159)
        Me.txt_SisaHutang.Name = "txt_SisaHutang"
        Me.txt_SisaHutang.Size = New System.Drawing.Size(132, 20)
        Me.txt_SisaHutang.TabIndex = 50
        Me.txt_SisaHutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_SisaHutang
        '
        Me.lbl_SisaHutang.AutoSize = True
        Me.lbl_SisaHutang.Location = New System.Drawing.Point(21, 162)
        Me.lbl_SisaHutang.Name = "lbl_SisaHutang"
        Me.lbl_SisaHutang.Size = New System.Drawing.Size(65, 13)
        Me.lbl_SisaHutang.TabIndex = 10064
        Me.lbl_SisaHutang.Text = "Sisa Hutang"
        '
        'cmb_SaranaPembayaran
        '
        Me.cmb_SaranaPembayaran.FormattingEnabled = True
        Me.cmb_SaranaPembayaran.Location = New System.Drawing.Point(316, 81)
        Me.cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        Me.cmb_SaranaPembayaran.Size = New System.Drawing.Size(238, 21)
        Me.cmb_SaranaPembayaran.TabIndex = 140
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(313, 58)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(109, 13)
        Me.Label17.TabIndex = 10080
        Me.Label17.Text = "Sarana Pembayaran :"
        '
        'lbl_PembayaranKe
        '
        Me.lbl_PembayaranKe.AutoSize = True
        Me.lbl_PembayaranKe.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PembayaranKe.Location = New System.Drawing.Point(21, 18)
        Me.lbl_PembayaranKe.Name = "lbl_PembayaranKe"
        Me.lbl_PembayaranKe.Size = New System.Drawing.Size(99, 15)
        Me.lbl_PembayaranKe.TabIndex = 10081
        Me.lbl_PembayaranKe.Text = "Pembayaran Ke-"
        '
        'txt_JumlahBankCash
        '
        Me.txt_JumlahBankCash.Enabled = False
        Me.txt_JumlahBankCash.Location = New System.Drawing.Point(150, 237)
        Me.txt_JumlahBankCash.Name = "txt_JumlahBankCash"
        Me.txt_JumlahBankCash.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahBankCash.TabIndex = 80
        Me.txt_JumlahBankCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahBankCash
        '
        Me.lbl_JumlahBankCash.AutoSize = True
        Me.lbl_JumlahBankCash.Location = New System.Drawing.Point(21, 240)
        Me.lbl_JumlahBankCash.Name = "lbl_JumlahBankCash"
        Me.lbl_JumlahBankCash.Size = New System.Drawing.Size(97, 13)
        Me.lbl_JumlahBankCash.TabIndex = 10083
        Me.lbl_JumlahBankCash.Text = "Jumlah Bank/Cash"
        '
        'txt_JumlahPPhDipotong
        '
        Me.txt_JumlahPPhDipotong.Enabled = False
        Me.txt_JumlahPPhDipotong.Location = New System.Drawing.Point(150, 211)
        Me.txt_JumlahPPhDipotong.Name = "txt_JumlahPPhDipotong"
        Me.txt_JumlahPPhDipotong.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahPPhDipotong.TabIndex = 70
        Me.txt_JumlahPPhDipotong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahPPhDipotong
        '
        Me.lbl_JumlahPPhDipotong.AutoSize = True
        Me.lbl_JumlahPPhDipotong.Location = New System.Drawing.Point(21, 214)
        Me.lbl_JumlahPPhDipotong.Name = "lbl_JumlahPPhDipotong"
        Me.lbl_JumlahPPhDipotong.Size = New System.Drawing.Size(109, 13)
        Me.lbl_JumlahPPhDipotong.TabIndex = 10085
        Me.lbl_JumlahPPhDipotong.Text = "Jumlah PPh Dipotong"
        '
        'txt_BiayaAdministrasiBank
        '
        Me.txt_BiayaAdministrasiBank.Location = New System.Drawing.Point(440, 108)
        Me.txt_BiayaAdministrasiBank.Name = "txt_BiayaAdministrasiBank"
        Me.txt_BiayaAdministrasiBank.Size = New System.Drawing.Size(114, 20)
        Me.txt_BiayaAdministrasiBank.TabIndex = 145
        Me.txt_BiayaAdministrasiBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_BiayaAdministrasiBank
        '
        Me.lbl_BiayaAdministrasiBank.AutoSize = True
        Me.lbl_BiayaAdministrasiBank.Location = New System.Drawing.Point(313, 111)
        Me.lbl_BiayaAdministrasiBank.Name = "lbl_BiayaAdministrasiBank"
        Me.lbl_BiayaAdministrasiBank.Size = New System.Drawing.Size(91, 13)
        Me.lbl_BiayaAdministrasiBank.TabIndex = 10087
        Me.lbl_BiayaAdministrasiBank.Text = "Biaya Adm Bank :"
        Me.lbl_BiayaAdministrasiBank.Visible = False
        '
        'frm_InputPembayaranHutangUsaha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 312)
        Me.Controls.Add(Me.lbl_BiayaAdministrasiBank)
        Me.Controls.Add(Me.txt_BiayaAdministrasiBank)
        Me.Controls.Add(Me.txt_JumlahPPhDipotong)
        Me.Controls.Add(Me.lbl_JumlahPPhDipotong)
        Me.Controls.Add(Me.txt_JumlahBankCash)
        Me.Controls.Add(Me.lbl_JumlahBankCash)
        Me.Controls.Add(Me.lbl_PembayaranKe)
        Me.Controls.Add(Me.cmb_SaranaPembayaran)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txt_SisaHutang)
        Me.Controls.Add(Me.lbl_SisaHutang)
        Me.Controls.Add(Me.txt_JumlahTelahDibayar)
        Me.Controls.Add(Me.lbl_JumlahTelahDibayar)
        Me.Controls.Add(Me.txt_JumlahTerutang)
        Me.Controls.Add(Me.lbl_JumlahTerutang)
        Me.Controls.Add(Me.dtp_TanggalBayar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_JumlahBayarSekarang)
        Me.Controls.Add(Me.lbl_JumlahBayar)
        Me.Controls.Add(Me.txt_NomorPembelian)
        Me.Controls.Add(Me.lbl_NomorPembelian)
        Me.Controls.Add(Me.txt_NomorBPHU)
        Me.Controls.Add(Me.lbl_NomorBPHU)
        Me.Controls.Add(Me.btn_Lanjutkan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputPembayaranHutangUsaha"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Pembayaran Hutang/Piutang Usaha"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Lanjutkan As System.Windows.Forms.Button
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents lbl_NomorBPHU As System.Windows.Forms.Label
    Friend WithEvents txt_NomorBPHU As System.Windows.Forms.TextBox
    Friend WithEvents txt_NomorPembelian As System.Windows.Forms.TextBox
    Friend WithEvents lbl_NomorPembelian As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahBayarSekarang As System.Windows.Forms.TextBox
    Friend WithEvents lbl_JumlahBayar As System.Windows.Forms.Label
    Friend WithEvents txt_Keterangan As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtp_TanggalBayar As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_JumlahTerutang As System.Windows.Forms.TextBox
    Friend WithEvents lbl_JumlahTerutang As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahTelahDibayar As System.Windows.Forms.TextBox
    Friend WithEvents lbl_JumlahTelahDibayar As System.Windows.Forms.Label
    Friend WithEvents txt_SisaHutang As System.Windows.Forms.TextBox
    Friend WithEvents lbl_SisaHutang As System.Windows.Forms.Label
    Friend WithEvents cmb_SaranaPembayaran As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lbl_PembayaranKe As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahBankCash As System.Windows.Forms.TextBox
    Friend WithEvents lbl_JumlahBankCash As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahPPhDipotong As System.Windows.Forms.TextBox
    Friend WithEvents lbl_JumlahPPhDipotong As System.Windows.Forms.Label
    Friend WithEvents txt_BiayaAdministrasiBank As TextBox
    Friend WithEvents lbl_BiayaAdministrasiBank As Label
End Class
