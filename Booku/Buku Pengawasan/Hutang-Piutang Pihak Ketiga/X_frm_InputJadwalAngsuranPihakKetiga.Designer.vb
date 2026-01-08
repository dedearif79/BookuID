<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_InputJadwalAngsuranPihakKetiga
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
        Me.dtp_TanggalJatuhTempo = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_AngsuranKe = New System.Windows.Forms.TextBox()
        Me.txt_Pokok = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_BagiHasil = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_JumlahDibayarkan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_JumlahPPh = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_PPhDitanggung = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_PPhDipotong = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grb_BiayaPPh = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_TarifPPh = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grb_BiayaPPh.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtp_TanggalJatuhTempo
        '
        Me.dtp_TanggalJatuhTempo.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalJatuhTempo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalJatuhTempo.Location = New System.Drawing.Point(138, 58)
        Me.dtp_TanggalJatuhTempo.Name = "dtp_TanggalJatuhTempo"
        Me.dtp_TanggalJatuhTempo.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalJatuhTempo.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(30, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 10355
        Me.Label4.Text = "Jatuh Tempo"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(30, 35)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(68, 13)
        Me.label9.TabIndex = 10353
        Me.label9.Text = "Angsuran Ke"
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(180, 340)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 10350
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(91, 340)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10351
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_AngsuranKe
        '
        Me.txt_AngsuranKe.Location = New System.Drawing.Point(138, 32)
        Me.txt_AngsuranKe.MaxLength = 2
        Me.txt_AngsuranKe.Name = "txt_AngsuranKe"
        Me.txt_AngsuranKe.Size = New System.Drawing.Size(40, 20)
        Me.txt_AngsuranKe.TabIndex = 10
        Me.txt_AngsuranKe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Pokok
        '
        Me.txt_Pokok.Location = New System.Drawing.Point(138, 84)
        Me.txt_Pokok.MaxLength = 27
        Me.txt_Pokok.Name = "txt_Pokok"
        Me.txt_Pokok.Size = New System.Drawing.Size(105, 20)
        Me.txt_Pokok.TabIndex = 30
        Me.txt_Pokok.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 10364
        Me.Label1.Text = "Pokok"
        '
        'txt_BagiHasil
        '
        Me.txt_BagiHasil.Location = New System.Drawing.Point(138, 110)
        Me.txt_BagiHasil.MaxLength = 27
        Me.txt_BagiHasil.Name = "txt_BagiHasil"
        Me.txt_BagiHasil.Size = New System.Drawing.Size(105, 20)
        Me.txt_BagiHasil.TabIndex = 40
        Me.txt_BagiHasil.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 10366
        Me.Label2.Text = "Bunga/Bagi Hasil"
        '
        'txt_JumlahDibayarkan
        '
        Me.txt_JumlahDibayarkan.Location = New System.Drawing.Point(138, 295)
        Me.txt_JumlahDibayarkan.MaxLength = 27
        Me.txt_JumlahDibayarkan.Name = "txt_JumlahDibayarkan"
        Me.txt_JumlahDibayarkan.Size = New System.Drawing.Size(105, 20)
        Me.txt_JumlahDibayarkan.TabIndex = 500
        Me.txt_JumlahDibayarkan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 298)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 13)
        Me.Label3.TabIndex = 10368
        Me.Label3.Text = "Jumlah Dibayarkan"
        '
        'txt_JumlahPPh
        '
        Me.txt_JumlahPPh.Location = New System.Drawing.Point(114, 47)
        Me.txt_JumlahPPh.MaxLength = 27
        Me.txt_JumlahPPh.Name = "txt_JumlahPPh"
        Me.txt_JumlahPPh.Size = New System.Drawing.Size(105, 20)
        Me.txt_JumlahPPh.TabIndex = 220
        Me.txt_JumlahPPh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 10370
        Me.Label5.Text = "Jumlah PPh"
        '
        'txt_PPhDitanggung
        '
        Me.txt_PPhDitanggung.Location = New System.Drawing.Point(114, 73)
        Me.txt_PPhDitanggung.MaxLength = 27
        Me.txt_PPhDitanggung.Name = "txt_PPhDitanggung"
        Me.txt_PPhDitanggung.Size = New System.Drawing.Size(105, 20)
        Me.txt_PPhDitanggung.TabIndex = 230
        Me.txt_PPhDitanggung.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 10372
        Me.Label6.Text = "Ditanggung"
        '
        'txt_PPhDipotong
        '
        Me.txt_PPhDipotong.Location = New System.Drawing.Point(114, 99)
        Me.txt_PPhDipotong.MaxLength = 27
        Me.txt_PPhDipotong.Name = "txt_PPhDipotong"
        Me.txt_PPhDipotong.Size = New System.Drawing.Size(105, 20)
        Me.txt_PPhDipotong.TabIndex = 240
        Me.txt_PPhDipotong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 102)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 10374
        Me.Label7.Text = "Dipotong"
        '
        'grb_BiayaPPh
        '
        Me.grb_BiayaPPh.Controls.Add(Me.Label10)
        Me.grb_BiayaPPh.Controls.Add(Me.txt_TarifPPh)
        Me.grb_BiayaPPh.Controls.Add(Me.Label8)
        Me.grb_BiayaPPh.Controls.Add(Me.txt_JumlahPPh)
        Me.grb_BiayaPPh.Controls.Add(Me.txt_PPhDipotong)
        Me.grb_BiayaPPh.Controls.Add(Me.Label5)
        Me.grb_BiayaPPh.Controls.Add(Me.Label7)
        Me.grb_BiayaPPh.Controls.Add(Me.Label6)
        Me.grb_BiayaPPh.Controls.Add(Me.txt_PPhDitanggung)
        Me.grb_BiayaPPh.Location = New System.Drawing.Point(24, 144)
        Me.grb_BiayaPPh.Name = "grb_BiayaPPh"
        Me.grb_BiayaPPh.Size = New System.Drawing.Size(238, 133)
        Me.grb_BiayaPPh.TabIndex = 200
        Me.grb_BiayaPPh.TabStop = False
        Me.grb_BiayaPPh.Text = "Biaya PPh :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(153, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(15, 13)
        Me.Label10.TabIndex = 10377
        Me.Label10.Text = "%"
        '
        'txt_TarifPPh
        '
        Me.txt_TarifPPh.Location = New System.Drawing.Point(114, 21)
        Me.txt_TarifPPh.MaxLength = 27
        Me.txt_TarifPPh.Name = "txt_TarifPPh"
        Me.txt_TarifPPh.Size = New System.Drawing.Size(36, 20)
        Me.txt_TarifPPh.TabIndex = 210
        Me.txt_TarifPPh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 10376
        Me.Label8.Text = "Tarif PPh"
        '
        'frm_InputJadwalAngsuranHutangPihakKetiga
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(289, 401)
        Me.Controls.Add(Me.grb_BiayaPPh)
        Me.Controls.Add(Me.txt_JumlahDibayarkan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_BagiHasil)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_Pokok)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtp_TanggalJatuhTempo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_AngsuranKe)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputJadwalAngsuranHutangPihakKetiga"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Jadwal Angsuran Hutang Pihak Ketiga"
        Me.grb_BiayaPPh.ResumeLayout(False)
        Me.grb_BiayaPPh.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtp_TanggalJatuhTempo As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents label9 As Label
    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_AngsuranKe As TextBox
    Friend WithEvents txt_Pokok As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_BagiHasil As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_JumlahDibayarkan As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_JumlahPPh As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_PPhDitanggung As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_PPhDipotong As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents grb_BiayaPPh As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_TarifPPh As TextBox
    Friend WithEvents Label8 As Label
End Class
