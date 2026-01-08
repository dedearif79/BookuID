<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputPembayaranHutangPajak
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_MasaPajak = New System.Windows.Forms.TextBox()
        Me.txt_JumlahBayar = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtp_TanggalBayar = New System.Windows.Forms.DateTimePicker()
        Me.txt_JumlahTerutang = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_JumlahDibayar = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_SisaHutang = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmb_SaranaPembayaran = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lbl_PembayaranKe = New System.Windows.Forms.Label()
        Me.txt_NomorBPHP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Lanjutkan
        '
        Me.btn_Lanjutkan.Location = New System.Drawing.Point(385, 203)
        Me.btn_Lanjutkan.Name = "btn_Lanjutkan"
        Me.btn_Lanjutkan.Size = New System.Drawing.Size(149, 35)
        Me.btn_Lanjutkan.TabIndex = 9931
        Me.btn_Lanjutkan.Text = "Lanjutkan >>"
        Me.btn_Lanjutkan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(296, 203)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9933
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 9934
        Me.Label1.Text = "Masa Pajak"
        '
        'txt_MasaPajak
        '
        Me.txt_MasaPajak.Enabled = False
        Me.txt_MasaPajak.Location = New System.Drawing.Point(130, 81)
        Me.txt_MasaPajak.MaxLength = 2
        Me.txt_MasaPajak.Name = "txt_MasaPajak"
        Me.txt_MasaPajak.Size = New System.Drawing.Size(132, 20)
        Me.txt_MasaPajak.TabIndex = 20
        '
        'txt_JumlahBayar
        '
        Me.txt_JumlahBayar.Location = New System.Drawing.Point(130, 185)
        Me.txt_JumlahBayar.Name = "txt_JumlahBayar"
        Me.txt_JumlahBayar.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahBayar.TabIndex = 100
        Me.txt_JumlahBayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(21, 188)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 9945
        Me.Label6.Text = "Jumlah Bayar"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(296, 134)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(238, 53)
        Me.txt_Keterangan.TabIndex = 900
        Me.txt_Keterangan.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(293, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 9947
        Me.Label3.Text = "Keterangan :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 214)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 9949
        Me.Label4.Text = "Tanggal Bayar"
        '
        'dtp_TanggalBayar
        '
        Me.dtp_TanggalBayar.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalBayar.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalBayar.Location = New System.Drawing.Point(130, 211)
        Me.dtp_TanggalBayar.Name = "dtp_TanggalBayar"
        Me.dtp_TanggalBayar.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalBayar.TabIndex = 110
        '
        'txt_JumlahTerutang
        '
        Me.txt_JumlahTerutang.Enabled = False
        Me.txt_JumlahTerutang.Location = New System.Drawing.Point(130, 107)
        Me.txt_JumlahTerutang.Name = "txt_JumlahTerutang"
        Me.txt_JumlahTerutang.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahTerutang.TabIndex = 30
        Me.txt_JumlahTerutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 10060
        Me.Label5.Text = "Jumlah Terutang"
        '
        'txt_JumlahDibayar
        '
        Me.txt_JumlahDibayar.Enabled = False
        Me.txt_JumlahDibayar.Location = New System.Drawing.Point(130, 133)
        Me.txt_JumlahDibayar.Name = "txt_JumlahDibayar"
        Me.txt_JumlahDibayar.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahDibayar.TabIndex = 80
        Me.txt_JumlahDibayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(21, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 10062
        Me.Label7.Text = "Jumlah Dibayar"
        '
        'txt_SisaHutang
        '
        Me.txt_SisaHutang.Enabled = False
        Me.txt_SisaHutang.Location = New System.Drawing.Point(130, 159)
        Me.txt_SisaHutang.Name = "txt_SisaHutang"
        Me.txt_SisaHutang.Size = New System.Drawing.Size(132, 20)
        Me.txt_SisaHutang.TabIndex = 90
        Me.txt_SisaHutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(21, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 13)
        Me.Label8.TabIndex = 10064
        Me.Label8.Text = "Sisa Hutang"
        '
        'cmb_SaranaPembayaran
        '
        Me.cmb_SaranaPembayaran.FormattingEnabled = True
        Me.cmb_SaranaPembayaran.Location = New System.Drawing.Point(296, 81)
        Me.cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        Me.cmb_SaranaPembayaran.Size = New System.Drawing.Size(238, 21)
        Me.cmb_SaranaPembayaran.TabIndex = 120
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(293, 58)
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
        'txt_NomorBPHP
        '
        Me.txt_NomorBPHP.Enabled = False
        Me.txt_NomorBPHP.Location = New System.Drawing.Point(130, 55)
        Me.txt_NomorBPHP.MaxLength = 2
        Me.txt_NomorBPHP.Name = "txt_NomorBPHP"
        Me.txt_NomorBPHP.Size = New System.Drawing.Size(132, 20)
        Me.txt_NomorBPHP.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 10083
        Me.Label2.Text = "Nomor BPHP"
        '
        'frm_InputPembayaranHutangPajak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 256)
        Me.Controls.Add(Me.txt_NomorBPHP)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbl_PembayaranKe)
        Me.Controls.Add(Me.cmb_SaranaPembayaran)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txt_SisaHutang)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_JumlahDibayar)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_JumlahTerutang)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtp_TanggalBayar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_JumlahBayar)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_MasaPajak)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Lanjutkan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputPembayaranHutangPajak"
        Me.Opacity = 0.96R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Pembayaran Hutang Pajak"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Lanjutkan As System.Windows.Forms.Button
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_MasaPajak As System.Windows.Forms.TextBox
    Friend WithEvents txt_JumlahBayar As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_Keterangan As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtp_TanggalBayar As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_JumlahTerutang As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahDibayar As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_SisaHutang As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmb_SaranaPembayaran As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lbl_PembayaranKe As System.Windows.Forms.Label
    Friend WithEvents txt_NomorBPHP As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
