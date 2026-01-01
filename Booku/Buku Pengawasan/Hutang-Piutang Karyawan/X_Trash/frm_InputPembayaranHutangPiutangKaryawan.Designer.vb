<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputPembayaranHutangPiutangKaryawan
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
        Me.lbl_BiayaAdministrasiBank = New System.Windows.Forms.Label()
        Me.txt_BiayaAdministrasiBank = New System.Windows.Forms.TextBox()
        Me.lbl_PembayaranKe = New System.Windows.Forms.Label()
        Me.cmb_SaranaPembayaran = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txt_SaldoAkhir = New System.Windows.Forms.TextBox()
        Me.lbl_SaldoAkhir = New System.Windows.Forms.Label()
        Me.txt_JumlahDibayar = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahDibayar = New System.Windows.Forms.Label()
        Me.txt_SaldoAwal = New System.Windows.Forms.TextBox()
        Me.lbl_SaldoAwal = New System.Windows.Forms.Label()
        Me.dtp_TanggalBayar = New System.Windows.Forms.DateTimePicker()
        Me.lbl_TanggalBayar = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.txt_JumlahBayar = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahBayar = New System.Windows.Forms.Label()
        Me.txt_NomorBP = New System.Windows.Forms.TextBox()
        Me.lbl_NomorBP = New System.Windows.Forms.Label()
        Me.btn_Lanjutkan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lbl_BiayaAdministrasiBank
        '
        Me.lbl_BiayaAdministrasiBank.AutoSize = True
        Me.lbl_BiayaAdministrasiBank.Location = New System.Drawing.Point(313, 85)
        Me.lbl_BiayaAdministrasiBank.Name = "lbl_BiayaAdministrasiBank"
        Me.lbl_BiayaAdministrasiBank.Size = New System.Drawing.Size(91, 13)
        Me.lbl_BiayaAdministrasiBank.TabIndex = 10114
        Me.lbl_BiayaAdministrasiBank.Text = "Biaya Adm Bank :"
        Me.lbl_BiayaAdministrasiBank.Visible = False
        '
        'txt_BiayaAdministrasiBank
        '
        Me.txt_BiayaAdministrasiBank.Location = New System.Drawing.Point(440, 82)
        Me.txt_BiayaAdministrasiBank.Name = "txt_BiayaAdministrasiBank"
        Me.txt_BiayaAdministrasiBank.Size = New System.Drawing.Size(114, 20)
        Me.txt_BiayaAdministrasiBank.TabIndex = 10098
        Me.txt_BiayaAdministrasiBank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PembayaranKe
        '
        Me.lbl_PembayaranKe.AutoSize = True
        Me.lbl_PembayaranKe.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PembayaranKe.Location = New System.Drawing.Point(21, 18)
        Me.lbl_PembayaranKe.Name = "lbl_PembayaranKe"
        Me.lbl_PembayaranKe.Size = New System.Drawing.Size(99, 15)
        Me.lbl_PembayaranKe.TabIndex = 10111
        Me.lbl_PembayaranKe.Text = "Pembayaran Ke-"
        '
        'cmb_SaranaPembayaran
        '
        Me.cmb_SaranaPembayaran.FormattingEnabled = True
        Me.cmb_SaranaPembayaran.Location = New System.Drawing.Point(316, 55)
        Me.cmb_SaranaPembayaran.Name = "cmb_SaranaPembayaran"
        Me.cmb_SaranaPembayaran.Size = New System.Drawing.Size(238, 21)
        Me.cmb_SaranaPembayaran.TabIndex = 10097
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(313, 32)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(109, 13)
        Me.Label17.TabIndex = 10110
        Me.Label17.Text = "Sarana Pembayaran :"
        '
        'txt_SaldoAkhir
        '
        Me.txt_SaldoAkhir.Enabled = False
        Me.txt_SaldoAkhir.Location = New System.Drawing.Point(150, 133)
        Me.txt_SaldoAkhir.Name = "txt_SaldoAkhir"
        Me.txt_SaldoAkhir.Size = New System.Drawing.Size(132, 20)
        Me.txt_SaldoAkhir.TabIndex = 10092
        Me.txt_SaldoAkhir.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_SaldoAkhir
        '
        Me.lbl_SaldoAkhir.AutoSize = True
        Me.lbl_SaldoAkhir.Location = New System.Drawing.Point(21, 136)
        Me.lbl_SaldoAkhir.Name = "lbl_SaldoAkhir"
        Me.lbl_SaldoAkhir.Size = New System.Drawing.Size(61, 13)
        Me.lbl_SaldoAkhir.TabIndex = 10109
        Me.lbl_SaldoAkhir.Text = "Saldo Akhir"
        '
        'txt_JumlahDibayar
        '
        Me.txt_JumlahDibayar.Enabled = False
        Me.txt_JumlahDibayar.Location = New System.Drawing.Point(150, 107)
        Me.txt_JumlahDibayar.Name = "txt_JumlahDibayar"
        Me.txt_JumlahDibayar.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahDibayar.TabIndex = 10091
        Me.txt_JumlahDibayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahDibayar
        '
        Me.lbl_JumlahDibayar.AutoSize = True
        Me.lbl_JumlahDibayar.Location = New System.Drawing.Point(21, 110)
        Me.lbl_JumlahDibayar.Name = "lbl_JumlahDibayar"
        Me.lbl_JumlahDibayar.Size = New System.Drawing.Size(79, 13)
        Me.lbl_JumlahDibayar.TabIndex = 10108
        Me.lbl_JumlahDibayar.Text = "Total Angsuran"
        '
        'txt_SaldoAwal
        '
        Me.txt_SaldoAwal.Enabled = False
        Me.txt_SaldoAwal.Location = New System.Drawing.Point(150, 81)
        Me.txt_SaldoAwal.Name = "txt_SaldoAwal"
        Me.txt_SaldoAwal.Size = New System.Drawing.Size(132, 20)
        Me.txt_SaldoAwal.TabIndex = 10090
        Me.txt_SaldoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_SaldoAwal
        '
        Me.lbl_SaldoAwal.AutoSize = True
        Me.lbl_SaldoAwal.Location = New System.Drawing.Point(21, 84)
        Me.lbl_SaldoAwal.Name = "lbl_SaldoAwal"
        Me.lbl_SaldoAwal.Size = New System.Drawing.Size(60, 13)
        Me.lbl_SaldoAwal.TabIndex = 10107
        Me.lbl_SaldoAwal.Text = "Saldo Awal"
        '
        'dtp_TanggalBayar
        '
        Me.dtp_TanggalBayar.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalBayar.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalBayar.Location = New System.Drawing.Point(150, 185)
        Me.dtp_TanggalBayar.Name = "dtp_TanggalBayar"
        Me.dtp_TanggalBayar.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalBayar.TabIndex = 10096
        '
        'lbl_TanggalBayar
        '
        Me.lbl_TanggalBayar.AutoSize = True
        Me.lbl_TanggalBayar.Location = New System.Drawing.Point(21, 188)
        Me.lbl_TanggalBayar.Name = "lbl_TanggalBayar"
        Me.lbl_TanggalBayar.Size = New System.Drawing.Size(95, 13)
        Me.lbl_TanggalBayar.TabIndex = 10106
        Me.lbl_TanggalBayar.Text = "Tanggal Transaksi"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(313, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 10105
        Me.Label3.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(316, 133)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(238, 68)
        Me.txt_Keterangan.TabIndex = 10099
        Me.txt_Keterangan.Text = ""
        '
        'txt_JumlahBayar
        '
        Me.txt_JumlahBayar.Location = New System.Drawing.Point(150, 159)
        Me.txt_JumlahBayar.Name = "txt_JumlahBayar"
        Me.txt_JumlahBayar.Size = New System.Drawing.Size(132, 20)
        Me.txt_JumlahBayar.TabIndex = 10093
        Me.txt_JumlahBayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_JumlahBayar
        '
        Me.lbl_JumlahBayar.AutoSize = True
        Me.lbl_JumlahBayar.Location = New System.Drawing.Point(21, 162)
        Me.lbl_JumlahBayar.Name = "lbl_JumlahBayar"
        Me.lbl_JumlahBayar.Size = New System.Drawing.Size(70, 13)
        Me.lbl_JumlahBayar.TabIndex = 10104
        Me.lbl_JumlahBayar.Text = "Jumlah Bayar"
        '
        'txt_NomorBP
        '
        Me.txt_NomorBP.Enabled = False
        Me.txt_NomorBP.Location = New System.Drawing.Point(150, 55)
        Me.txt_NomorBP.MaxLength = 2
        Me.txt_NomorBP.Name = "txt_NomorBP"
        Me.txt_NomorBP.Size = New System.Drawing.Size(132, 20)
        Me.txt_NomorBP.TabIndex = 10088
        '
        'lbl_NomorBP
        '
        Me.lbl_NomorBP.AutoSize = True
        Me.lbl_NomorBP.Location = New System.Drawing.Point(21, 58)
        Me.lbl_NomorBP.Name = "lbl_NomorBP"
        Me.lbl_NomorBP.Size = New System.Drawing.Size(55, 13)
        Me.lbl_NomorBP.TabIndex = 10102
        Me.lbl_NomorBP.Text = "Nomor BP"
        '
        'btn_Lanjutkan
        '
        Me.btn_Lanjutkan.Location = New System.Drawing.Point(405, 223)
        Me.btn_Lanjutkan.Name = "btn_Lanjutkan"
        Me.btn_Lanjutkan.Size = New System.Drawing.Size(149, 35)
        Me.btn_Lanjutkan.TabIndex = 10100
        Me.btn_Lanjutkan.Text = "Lanjutkan >>"
        Me.btn_Lanjutkan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(316, 223)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10101
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'frm_InputPembayaranHutangPiutangKaryawan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 277)
        Me.Controls.Add(Me.lbl_BiayaAdministrasiBank)
        Me.Controls.Add(Me.txt_BiayaAdministrasiBank)
        Me.Controls.Add(Me.lbl_PembayaranKe)
        Me.Controls.Add(Me.cmb_SaranaPembayaran)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txt_SaldoAkhir)
        Me.Controls.Add(Me.lbl_SaldoAkhir)
        Me.Controls.Add(Me.txt_JumlahDibayar)
        Me.Controls.Add(Me.lbl_JumlahDibayar)
        Me.Controls.Add(Me.txt_SaldoAwal)
        Me.Controls.Add(Me.lbl_SaldoAwal)
        Me.Controls.Add(Me.dtp_TanggalBayar)
        Me.Controls.Add(Me.lbl_TanggalBayar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_JumlahBayar)
        Me.Controls.Add(Me.lbl_JumlahBayar)
        Me.Controls.Add(Me.txt_NomorBP)
        Me.Controls.Add(Me.lbl_NomorBP)
        Me.Controls.Add(Me.btn_Lanjutkan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputPembayaranHutangPiutangKaryawan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Pembayaran Hutang/Piutang Karyawan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbl_BiayaAdministrasiBank As Label
    Friend WithEvents txt_BiayaAdministrasiBank As TextBox
    Friend WithEvents lbl_PembayaranKe As Label
    Friend WithEvents cmb_SaranaPembayaran As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txt_SaldoAkhir As TextBox
    Friend WithEvents lbl_SaldoAkhir As Label
    Friend WithEvents txt_JumlahDibayar As TextBox
    Friend WithEvents lbl_JumlahDibayar As Label
    Friend WithEvents txt_SaldoAwal As TextBox
    Friend WithEvents lbl_SaldoAwal As Label
    Friend WithEvents dtp_TanggalBayar As DateTimePicker
    Friend WithEvents lbl_TanggalBayar As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents txt_JumlahBayar As TextBox
    Friend WithEvents lbl_JumlahBayar As Label
    Friend WithEvents txt_NomorBP As TextBox
    Friend WithEvents lbl_NomorBP As Label
    Friend WithEvents btn_Lanjutkan As Button
    Friend WithEvents btn_Batal As Button
End Class
