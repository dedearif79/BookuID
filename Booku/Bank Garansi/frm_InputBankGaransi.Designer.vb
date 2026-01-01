<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputBankGaransi
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
        Me.lbl_NomorBPBG = New System.Windows.Forms.Label()
        Me.txt_NomorBPBG = New System.Windows.Forms.TextBox()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_JumlahTransaksi = New System.Windows.Forms.TextBox()
        Me.txt_NamaLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.lbl_NamaLawanTransaksi = New System.Windows.Forms.Label()
        Me.lbl_KodeLawanTransaksi = New System.Windows.Forms.Label()
        Me.txt_KodeLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.dtp_TanggalTransaksi = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_NamaBank = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Keperluan = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_BiayaProvisi = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_NomorKontrak = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(254, 417)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 10326
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(165, 417)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10327
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'lbl_NomorBPBG
        '
        Me.lbl_NomorBPBG.AutoSize = True
        Me.lbl_NomorBPBG.Location = New System.Drawing.Point(27, 66)
        Me.lbl_NomorBPBG.Name = "lbl_NomorBPBG"
        Me.lbl_NomorBPBG.Size = New System.Drawing.Size(70, 13)
        Me.lbl_NomorBPBG.TabIndex = 10420
        Me.lbl_NomorBPBG.Text = "Nomor BPBG"
        '
        'txt_NomorBPBG
        '
        Me.txt_NomorBPBG.Location = New System.Drawing.Point(153, 63)
        Me.txt_NomorBPBG.MaxLength = 99
        Me.txt_NomorBPBG.Name = "txt_NomorBPBG"
        Me.txt_NomorBPBG.Size = New System.Drawing.Size(101, 20)
        Me.txt_NomorBPBG.TabIndex = 20
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(256, 165)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 60
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 222)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 10419
        Me.Label1.Text = "Jumlah Transaksi"
        '
        'txt_JumlahTransaksi
        '
        Me.txt_JumlahTransaksi.Location = New System.Drawing.Point(153, 219)
        Me.txt_JumlahTransaksi.MaxLength = 99
        Me.txt_JumlahTransaksi.Name = "txt_JumlahTransaksi"
        Me.txt_JumlahTransaksi.Size = New System.Drawing.Size(101, 20)
        Me.txt_JumlahTransaksi.TabIndex = 80
        Me.txt_JumlahTransaksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NamaLawanTransaksi
        '
        Me.txt_NamaLawanTransaksi.Location = New System.Drawing.Point(153, 193)
        Me.txt_NamaLawanTransaksi.MaxLength = 99
        Me.txt_NamaLawanTransaksi.Name = "txt_NamaLawanTransaksi"
        Me.txt_NamaLawanTransaksi.Size = New System.Drawing.Size(183, 20)
        Me.txt_NamaLawanTransaksi.TabIndex = 70
        '
        'lbl_NamaLawanTransaksi
        '
        Me.lbl_NamaLawanTransaksi.AutoSize = True
        Me.lbl_NamaLawanTransaksi.Location = New System.Drawing.Point(27, 196)
        Me.lbl_NamaLawanTransaksi.Name = "lbl_NamaLawanTransaksi"
        Me.lbl_NamaLawanTransaksi.Size = New System.Drawing.Size(119, 13)
        Me.lbl_NamaLawanTransaksi.TabIndex = 10418
        Me.lbl_NamaLawanTransaksi.Text = "Nama Lawan Transaksi"
        '
        'lbl_KodeLawanTransaksi
        '
        Me.lbl_KodeLawanTransaksi.AutoSize = True
        Me.lbl_KodeLawanTransaksi.Location = New System.Drawing.Point(27, 170)
        Me.lbl_KodeLawanTransaksi.Name = "lbl_KodeLawanTransaksi"
        Me.lbl_KodeLawanTransaksi.Size = New System.Drawing.Size(116, 13)
        Me.lbl_KodeLawanTransaksi.TabIndex = 10417
        Me.lbl_KodeLawanTransaksi.Text = "Kode Lawan Transaksi"
        '
        'txt_KodeLawanTransaksi
        '
        Me.txt_KodeLawanTransaksi.Location = New System.Drawing.Point(153, 167)
        Me.txt_KodeLawanTransaksi.MaxLength = 99
        Me.txt_KodeLawanTransaksi.Name = "txt_KodeLawanTransaksi"
        Me.txt_KodeLawanTransaksi.Size = New System.Drawing.Size(101, 20)
        Me.txt_KodeLawanTransaksi.TabIndex = 50
        '
        'dtp_TanggalTransaksi
        '
        Me.dtp_TanggalTransaksi.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalTransaksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalTransaksi.Location = New System.Drawing.Point(153, 37)
        Me.dtp_TanggalTransaksi.Name = "dtp_TanggalTransaksi"
        Me.dtp_TanggalTransaksi.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalTransaksi.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 10416
        Me.Label4.Text = "Tanggal Transaksi"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 10424
        Me.Label2.Text = "Nama Bank"
        '
        'txt_NamaBank
        '
        Me.txt_NamaBank.Location = New System.Drawing.Point(153, 115)
        Me.txt_NamaBank.MaxLength = 99
        Me.txt_NamaBank.Name = "txt_NamaBank"
        Me.txt_NamaBank.Size = New System.Drawing.Size(183, 20)
        Me.txt_NamaBank.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 10426
        Me.Label3.Text = "Keperluan"
        '
        'txt_Keperluan
        '
        Me.txt_Keperluan.Location = New System.Drawing.Point(153, 141)
        Me.txt_Keperluan.MaxLength = 99
        Me.txt_Keperluan.Name = "txt_Keperluan"
        Me.txt_Keperluan.Size = New System.Drawing.Size(183, 20)
        Me.txt_Keperluan.TabIndex = 40
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 248)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 10428
        Me.Label5.Text = "Biaya Provisi"
        '
        'txt_BiayaProvisi
        '
        Me.txt_BiayaProvisi.Location = New System.Drawing.Point(153, 245)
        Me.txt_BiayaProvisi.MaxLength = 99
        Me.txt_BiayaProvisi.Name = "txt_BiayaProvisi"
        Me.txt_BiayaProvisi.Size = New System.Drawing.Size(101, 20)
        Me.txt_BiayaProvisi.TabIndex = 90
        Me.txt_BiayaProvisi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 288)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 10432
        Me.Label7.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Keterangan.Location = New System.Drawing.Point(30, 307)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(306, 84)
        Me.txt_Keterangan.TabIndex = 300
        Me.txt_Keterangan.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 10434
        Me.Label6.Text = "Nomor Kontrak"
        '
        'txt_NomorKontrak
        '
        Me.txt_NomorKontrak.Location = New System.Drawing.Point(153, 89)
        Me.txt_NomorKontrak.MaxLength = 99
        Me.txt_NomorKontrak.Name = "txt_NomorKontrak"
        Me.txt_NomorKontrak.Size = New System.Drawing.Size(183, 20)
        Me.txt_NomorKontrak.TabIndex = 25
        '
        'frm_InputBankGaransi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(367, 473)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_NomorKontrak)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_BiayaProvisi)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keperluan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_NamaBank)
        Me.Controls.Add(Me.lbl_NomorBPBG)
        Me.Controls.Add(Me.txt_NomorBPBG)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_JumlahTransaksi)
        Me.Controls.Add(Me.txt_NamaLawanTransaksi)
        Me.Controls.Add(Me.lbl_NamaLawanTransaksi)
        Me.Controls.Add(Me.lbl_KodeLawanTransaksi)
        Me.Controls.Add(Me.txt_KodeLawanTransaksi)
        Me.Controls.Add(Me.dtp_TanggalTransaksi)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputBankGaransi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Bank Garansi"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents lbl_NomorBPBG As Label
    Friend WithEvents txt_NomorBPBG As TextBox
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_JumlahTransaksi As TextBox
    Friend WithEvents txt_NamaLawanTransaksi As TextBox
    Friend WithEvents lbl_NamaLawanTransaksi As Label
    Friend WithEvents lbl_KodeLawanTransaksi As Label
    Friend WithEvents txt_KodeLawanTransaksi As TextBox
    Friend WithEvents dtp_TanggalTransaksi As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NamaBank As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_Keperluan As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_BiayaProvisi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_NomorKontrak As TextBox
End Class
