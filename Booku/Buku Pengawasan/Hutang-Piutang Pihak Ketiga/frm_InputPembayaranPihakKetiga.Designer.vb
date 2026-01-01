<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_InputPembayaranPihakKetiga
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lbl_BiayaAdministrasiBank = New System.Windows.Forms.Label()
        Me.txt_BiayaAdministrasiBank = New System.Windows.Forms.TextBox()
        Me.txt_JumlahDibayarkan = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahPencairan = New System.Windows.Forms.Label()
        Me.lbl_AngsuranKe = New System.Windows.Forms.Label()
        Me.cmb_SaranaPembayaran = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txt_BagiHasil = New System.Windows.Forms.TextBox()
        Me.lbl_BagiHasil = New System.Windows.Forms.Label()
        Me.txt_Pokok = New System.Windows.Forms.TextBox()
        Me.lbl_Pokok = New System.Windows.Forms.Label()
        Me.txt_Denda = New System.Windows.Forms.TextBox()
        Me.lbl_Denda = New System.Windows.Forms.Label()
        Me.dtp_TanggalBayar = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.txt_PPhDipotong = New System.Windows.Forms.TextBox()
        Me.lbl_PPh = New System.Windows.Forms.Label()
        Me.txt_NomorBP = New System.Windows.Forms.TextBox()
        Me.lbl_NomorBP = New System.Windows.Forms.Label()
        Me.btn_Lanjutkan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_PPhDitanggung = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbl_BiayaAdministrasiBank
        '
        Me.lbl_BiayaAdministrasiBank.AutoSize = True
        Me.lbl_BiayaAdministrasiBank.Location = New System.Drawing.Point(313, 111)
        Me.lbl_BiayaAdministrasiBank.Name = "lbl_BiayaAdministrasiBank"
        Me.lbl_BiayaAdministrasiBank.Size = New System.Drawing.Size(91, 13)
        Me.lbl_BiayaAdministrasiBank.TabIndex = 10114
        Me.lbl_BiayaAdministrasiBank.Text = "Biaya Adm Bank :"
        Me.lbl_BiayaAdministrasiBank.Visible = False
        '
        'txt_BiayaAdministrasiBank
        '
        Me.txt_BiayaAdministrasiBank.Location = New System.Drawing.Point(440, 108)
        Me.txt_BiayaAdministrasiBank.Name = "txt_BiayaAdministrasiBank"
        Me.txt_BiayaAdministrasiBank.Size = New System.Drawing.Size(114, 20)
        Me.txt_BiayaAdministrasiBank.TabIndex = 250
        Me.txt_BiayaAdministrasiBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_JumlahDibayarkan
        '
        Me.txt_JumlahDibayarkan.Enabled = False
        Me.txt_JumlahDibayarkan.Location = New System.Drawing.Point(150, 237)
        Me.txt_JumlahDibayarkan.Name = "txt_JumlahDibayarkan"
        Me.txt_JumlahDibayarkan.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahDibayarkan.TabIndex = 100
        Me.txt_JumlahDibayarkan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahPencairan
        '
        Me.lbl_JumlahPencairan.AutoSize = True
        Me.lbl_JumlahPencairan.Location = New System.Drawing.Point(21, 240)
        Me.lbl_JumlahPencairan.Name = "lbl_JumlahPencairan"
        Me.lbl_JumlahPencairan.Size = New System.Drawing.Size(97, 13)
        Me.lbl_JumlahPencairan.TabIndex = 10112
        Me.lbl_JumlahPencairan.Text = "Jumlah Dibayarkan"
        '
        'lbl_AngsuranKe
        '
        Me.lbl_AngsuranKe.AutoSize = True
        Me.lbl_AngsuranKe.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_AngsuranKe.Location = New System.Drawing.Point(21, 18)
        Me.lbl_AngsuranKe.Name = "lbl_AngsuranKe"
        Me.lbl_AngsuranKe.Size = New System.Drawing.Size(83, 15)
        Me.lbl_AngsuranKe.TabIndex = 10111
        Me.lbl_AngsuranKe.Text = "Angsuran Ke :"
        '
        'cmb_SaranaPembayaran
        '
        Me.cmb_SaranaPembayaran.FormattingEnabled = True
        Me.cmb_SaranaPembayaran.Location = New System.Drawing.Point(316, 81)
        Me.cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        Me.cmb_SaranaPembayaran.Size = New System.Drawing.Size(238, 21)
        Me.cmb_SaranaPembayaran.TabIndex = 200
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(313, 58)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(109, 13)
        Me.Label17.TabIndex = 10110
        Me.Label17.Text = "Sarana Pembayaran :"
        '
        'txt_BagiHasil
        '
        Me.txt_BagiHasil.Enabled = False
        Me.txt_BagiHasil.Location = New System.Drawing.Point(150, 133)
        Me.txt_BagiHasil.Name = "txt_BagiHasil"
        Me.txt_BagiHasil.Size = New System.Drawing.Size(132, 20)
        Me.txt_BagiHasil.TabIndex = 60
        Me.txt_BagiHasil.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_BagiHasil
        '
        Me.lbl_BagiHasil.AutoSize = True
        Me.lbl_BagiHasil.Location = New System.Drawing.Point(21, 136)
        Me.lbl_BagiHasil.Name = "lbl_BagiHasil"
        Me.lbl_BagiHasil.Size = New System.Drawing.Size(90, 13)
        Me.lbl_BagiHasil.TabIndex = 10109
        Me.lbl_BagiHasil.Text = "Bunga/Bagi Hasil"
        '
        'txt_Pokok
        '
        Me.txt_Pokok.Enabled = False
        Me.txt_Pokok.Location = New System.Drawing.Point(150, 107)
        Me.txt_Pokok.Name = "txt_Pokok"
        Me.txt_Pokok.Size = New System.Drawing.Size(132, 20)
        Me.txt_Pokok.TabIndex = 50
        Me.txt_Pokok.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Pokok
        '
        Me.lbl_Pokok.AutoSize = True
        Me.lbl_Pokok.Location = New System.Drawing.Point(21, 110)
        Me.lbl_Pokok.Name = "lbl_Pokok"
        Me.lbl_Pokok.Size = New System.Drawing.Size(38, 13)
        Me.lbl_Pokok.TabIndex = 10108
        Me.lbl_Pokok.Text = "Pokok"
        '
        'txt_Denda
        '
        Me.txt_Denda.Location = New System.Drawing.Point(150, 211)
        Me.txt_Denda.Name = "txt_Denda"
        Me.txt_Denda.Size = New System.Drawing.Size(132, 20)
        Me.txt_Denda.TabIndex = 90
        Me.txt_Denda.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Denda
        '
        Me.lbl_Denda.AutoSize = True
        Me.lbl_Denda.Location = New System.Drawing.Point(21, 214)
        Me.lbl_Denda.Name = "lbl_Denda"
        Me.lbl_Denda.Size = New System.Drawing.Size(39, 13)
        Me.lbl_Denda.TabIndex = 10107
        Me.lbl_Denda.Text = "Denda"
        '
        'dtp_TanggalBayar
        '
        Me.dtp_TanggalBayar.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalBayar.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalBayar.Location = New System.Drawing.Point(150, 263)
        Me.dtp_TanggalBayar.Name = "dtp_TanggalBayar"
        Me.dtp_TanggalBayar.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalBayar.TabIndex = 110
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 266)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 10106
        Me.Label4.Text = "Tanggal Bayar"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(313, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 10105
        Me.Label3.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(316, 159)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(238, 83)
        Me.txt_Keterangan.TabIndex = 500
        Me.txt_Keterangan.Text = ""
        '
        'txt_PPhDipotong
        '
        Me.txt_PPhDipotong.Enabled = False
        Me.txt_PPhDipotong.Location = New System.Drawing.Point(150, 185)
        Me.txt_PPhDipotong.Name = "txt_PPhDipotong"
        Me.txt_PPhDipotong.Size = New System.Drawing.Size(132, 20)
        Me.txt_PPhDipotong.TabIndex = 80
        Me.txt_PPhDipotong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PPh
        '
        Me.lbl_PPh.AutoSize = True
        Me.lbl_PPh.Location = New System.Drawing.Point(21, 188)
        Me.lbl_PPh.Name = "lbl_PPh"
        Me.lbl_PPh.Size = New System.Drawing.Size(73, 13)
        Me.lbl_PPh.TabIndex = 10104
        Me.lbl_PPh.Text = "PPh Dipotong"
        '
        'txt_NomorBP
        '
        Me.txt_NomorBP.Enabled = False
        Me.txt_NomorBP.Location = New System.Drawing.Point(150, 58)
        Me.txt_NomorBP.MaxLength = 2
        Me.txt_NomorBP.Name = "txt_NomorBP"
        Me.txt_NomorBP.Size = New System.Drawing.Size(132, 20)
        Me.txt_NomorBP.TabIndex = 10
        '
        'lbl_NomorBP
        '
        Me.lbl_NomorBP.AutoSize = True
        Me.lbl_NomorBP.Location = New System.Drawing.Point(21, 61)
        Me.lbl_NomorBP.Name = "lbl_NomorBP"
        Me.lbl_NomorBP.Size = New System.Drawing.Size(55, 13)
        Me.lbl_NomorBP.TabIndex = 10102
        Me.lbl_NomorBP.Text = "Nomor BP"
        '
        'btn_Lanjutkan
        '
        Me.btn_Lanjutkan.Location = New System.Drawing.Point(405, 248)
        Me.btn_Lanjutkan.Name = "btn_Lanjutkan"
        Me.btn_Lanjutkan.Size = New System.Drawing.Size(149, 35)
        Me.btn_Lanjutkan.TabIndex = 9000
        Me.btn_Lanjutkan.Text = "Lanjutkan >>"
        Me.btn_Lanjutkan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(316, 248)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_PPhDitanggung
        '
        Me.txt_PPhDitanggung.Enabled = False
        Me.txt_PPhDitanggung.Location = New System.Drawing.Point(150, 159)
        Me.txt_PPhDitanggung.Name = "txt_PPhDitanggung"
        Me.txt_PPhDitanggung.Size = New System.Drawing.Size(132, 20)
        Me.txt_PPhDitanggung.TabIndex = 70
        Me.txt_PPhDitanggung.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 162)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 10116
        Me.Label1.Text = "PPh Ditanggung"
        '
        'frm_InputPembayaranHutangPihakKetiga
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 312)
        Me.Controls.Add(Me.txt_PPhDitanggung)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbl_BiayaAdministrasiBank)
        Me.Controls.Add(Me.txt_BiayaAdministrasiBank)
        Me.Controls.Add(Me.txt_JumlahDibayarkan)
        Me.Controls.Add(Me.lbl_JumlahPencairan)
        Me.Controls.Add(Me.lbl_AngsuranKe)
        Me.Controls.Add(Me.cmb_SaranaPembayaran)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txt_BagiHasil)
        Me.Controls.Add(Me.lbl_BagiHasil)
        Me.Controls.Add(Me.txt_Pokok)
        Me.Controls.Add(Me.lbl_Pokok)
        Me.Controls.Add(Me.txt_Denda)
        Me.Controls.Add(Me.lbl_Denda)
        Me.Controls.Add(Me.dtp_TanggalBayar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_PPhDipotong)
        Me.Controls.Add(Me.lbl_PPh)
        Me.Controls.Add(Me.txt_NomorBP)
        Me.Controls.Add(Me.lbl_NomorBP)
        Me.Controls.Add(Me.btn_Lanjutkan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputPembayaranHutangPihakKetiga"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Pembayaran Hutang Pihak Ketiga"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbl_BiayaAdministrasiBank As Label
    Friend WithEvents txt_BiayaAdministrasiBank As TextBox
    Friend WithEvents txt_JumlahDibayarkan As TextBox
    Friend WithEvents lbl_JumlahPencairan As Label
    Friend WithEvents lbl_AngsuranKe As Label
    Friend WithEvents cmb_SaranaPembayaran As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txt_BagiHasil As TextBox
    Friend WithEvents lbl_BagiHasil As Label
    Friend WithEvents txt_Pokok As TextBox
    Friend WithEvents lbl_Pokok As Label
    Friend WithEvents txt_Denda As TextBox
    Friend WithEvents lbl_Denda As Label
    Friend WithEvents dtp_TanggalBayar As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents txt_PPhDipotong As TextBox
    Friend WithEvents lbl_PPh As Label
    Friend WithEvents txt_NomorBP As TextBox
    Friend WithEvents lbl_NomorBP As Label
    Friend WithEvents btn_Lanjutkan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_PPhDitanggung As TextBox
    Friend WithEvents Label1 As Label
End Class
