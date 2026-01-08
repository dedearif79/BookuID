<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class X_frm_InputAktivaLainnya
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
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.lbl_NomorBPAL = New System.Windows.Forms.Label()
        Me.txt_NomorBPAL = New System.Windows.Forms.TextBox()
        Me.dtp_TanggalBukti = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_NomorBukti = New System.Windows.Forms.TextBox()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_JumlahTransaksi = New System.Windows.Forms.TextBox()
        Me.txt_NamaLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.lbl_NamaLawanTransaksi = New System.Windows.Forms.Label()
        Me.lbl_KodeLawanTransaksi = New System.Windows.Forms.Label()
        Me.txt_KodeLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_UraianTransaksi = New System.Windows.Forms.RichTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_NamaAkun = New System.Windows.Forms.TextBox()
        Me.txt_COADebet = New System.Windows.Forms.TextBox()
        Me.btn_PilihCOADebet = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(254, 436)
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
        Me.btn_Batal.Location = New System.Drawing.Point(165, 436)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10327
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'lbl_NomorBPAL
        '
        Me.lbl_NomorBPAL.AutoSize = True
        Me.lbl_NomorBPAL.Location = New System.Drawing.Point(28, 35)
        Me.lbl_NomorBPAL.Name = "lbl_NomorBPAL"
        Me.lbl_NomorBPAL.Size = New System.Drawing.Size(68, 13)
        Me.lbl_NomorBPAL.TabIndex = 10424
        Me.lbl_NomorBPAL.Text = "Nomor BPAL"
        '
        'txt_NomorBPAL
        '
        Me.txt_NomorBPAL.Location = New System.Drawing.Point(154, 32)
        Me.txt_NomorBPAL.MaxLength = 99
        Me.txt_NomorBPAL.Name = "txt_NomorBPAL"
        Me.txt_NomorBPAL.Size = New System.Drawing.Size(101, 20)
        Me.txt_NomorBPAL.TabIndex = 10
        '
        'dtp_TanggalBukti
        '
        Me.dtp_TanggalBukti.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalBukti.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalBukti.Location = New System.Drawing.Point(154, 84)
        Me.dtp_TanggalBukti.Name = "dtp_TanggalBukti"
        Me.dtp_TanggalBukti.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalBukti.TabIndex = 30
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 10423
        Me.Label4.Text = "Tanggal Bukti"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 10426
        Me.Label1.Text = "Nomor Bukti"
        '
        'txt_NomorBukti
        '
        Me.txt_NomorBukti.Location = New System.Drawing.Point(154, 58)
        Me.txt_NomorBukti.MaxLength = 99
        Me.txt_NomorBukti.Name = "txt_NomorBukti"
        Me.txt_NomorBukti.Size = New System.Drawing.Size(101, 20)
        Me.txt_NomorBukti.TabIndex = 20
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(257, 108)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 50
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 283)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 10433
        Me.Label2.Text = "Jumlah Transaksi"
        '
        'txt_JumlahTransaksi
        '
        Me.txt_JumlahTransaksi.Location = New System.Drawing.Point(154, 280)
        Me.txt_JumlahTransaksi.MaxLength = 99
        Me.txt_JumlahTransaksi.Name = "txt_JumlahTransaksi"
        Me.txt_JumlahTransaksi.Size = New System.Drawing.Size(101, 20)
        Me.txt_JumlahTransaksi.TabIndex = 110
        Me.txt_JumlahTransaksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NamaLawanTransaksi
        '
        Me.txt_NamaLawanTransaksi.Location = New System.Drawing.Point(154, 136)
        Me.txt_NamaLawanTransaksi.MaxLength = 99
        Me.txt_NamaLawanTransaksi.Name = "txt_NamaLawanTransaksi"
        Me.txt_NamaLawanTransaksi.Size = New System.Drawing.Size(183, 20)
        Me.txt_NamaLawanTransaksi.TabIndex = 60
        '
        'lbl_NamaLawanTransaksi
        '
        Me.lbl_NamaLawanTransaksi.AutoSize = True
        Me.lbl_NamaLawanTransaksi.Location = New System.Drawing.Point(28, 139)
        Me.lbl_NamaLawanTransaksi.Name = "lbl_NamaLawanTransaksi"
        Me.lbl_NamaLawanTransaksi.Size = New System.Drawing.Size(119, 13)
        Me.lbl_NamaLawanTransaksi.TabIndex = 10432
        Me.lbl_NamaLawanTransaksi.Text = "Nama Lawan Transaksi"
        '
        'lbl_KodeLawanTransaksi
        '
        Me.lbl_KodeLawanTransaksi.AutoSize = True
        Me.lbl_KodeLawanTransaksi.Location = New System.Drawing.Point(28, 113)
        Me.lbl_KodeLawanTransaksi.Name = "lbl_KodeLawanTransaksi"
        Me.lbl_KodeLawanTransaksi.Size = New System.Drawing.Size(116, 13)
        Me.lbl_KodeLawanTransaksi.TabIndex = 10431
        Me.lbl_KodeLawanTransaksi.Text = "Kode Lawan Transaksi"
        '
        'txt_KodeLawanTransaksi
        '
        Me.txt_KodeLawanTransaksi.Location = New System.Drawing.Point(154, 110)
        Me.txt_KodeLawanTransaksi.MaxLength = 99
        Me.txt_KodeLawanTransaksi.Name = "txt_KodeLawanTransaksi"
        Me.txt_KodeLawanTransaksi.Size = New System.Drawing.Size(101, 20)
        Me.txt_KodeLawanTransaksi.TabIndex = 40
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 315)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 10435
        Me.Label7.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Keterangan.Location = New System.Drawing.Point(31, 334)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(306, 84)
        Me.txt_Keterangan.TabIndex = 300
        Me.txt_Keterangan.Text = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(28, 168)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 10438
        Me.Label8.Text = "Uraian Transaksi"
        '
        'txt_UraianTransaksi
        '
        Me.txt_UraianTransaksi.Location = New System.Drawing.Point(154, 165)
        Me.txt_UraianTransaksi.Name = "txt_UraianTransaksi"
        Me.txt_UraianTransaksi.Size = New System.Drawing.Size(183, 54)
        Me.txt_UraianTransaksi.TabIndex = 70
        Me.txt_UraianTransaksi.Text = ""
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(28, 231)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 10443
        Me.Label11.Text = "Kode Akun"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 257)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 10442
        Me.Label3.Text = "Nama Akun"
        '
        'txt_NamaAkun
        '
        Me.txt_NamaAkun.Location = New System.Drawing.Point(154, 254)
        Me.txt_NamaAkun.Name = "txt_NamaAkun"
        Me.txt_NamaAkun.Size = New System.Drawing.Size(183, 20)
        Me.txt_NamaAkun.TabIndex = 100
        '
        'txt_COADebet
        '
        Me.txt_COADebet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_COADebet.Location = New System.Drawing.Point(154, 228)
        Me.txt_COADebet.MaxLength = 9
        Me.txt_COADebet.Name = "txt_COADebet"
        Me.txt_COADebet.Size = New System.Drawing.Size(69, 20)
        Me.txt_COADebet.TabIndex = 80
        '
        'btn_PilihCOADebet
        '
        Me.btn_PilihCOADebet.Location = New System.Drawing.Point(230, 226)
        Me.btn_PilihCOADebet.Name = "btn_PilihCOADebet"
        Me.btn_PilihCOADebet.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihCOADebet.TabIndex = 90
        Me.btn_PilihCOADebet.Text = "Pilih"
        Me.btn_PilihCOADebet.UseVisualStyleBackColor = True
        '
        'frm_InputAktivaLainnya
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(363, 488)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_NamaAkun)
        Me.Controls.Add(Me.txt_COADebet)
        Me.Controls.Add(Me.btn_PilihCOADebet)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_UraianTransaksi)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_JumlahTransaksi)
        Me.Controls.Add(Me.txt_NamaLawanTransaksi)
        Me.Controls.Add(Me.lbl_NamaLawanTransaksi)
        Me.Controls.Add(Me.lbl_KodeLawanTransaksi)
        Me.Controls.Add(Me.txt_KodeLawanTransaksi)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_NomorBukti)
        Me.Controls.Add(Me.lbl_NomorBPAL)
        Me.Controls.Add(Me.txt_NomorBPAL)
        Me.Controls.Add(Me.dtp_TanggalBukti)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputAktivaLainnya"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Data Investasi"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents lbl_NomorBPAL As Label
    Friend WithEvents txt_NomorBPAL As TextBox
    Friend WithEvents dtp_TanggalBukti As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_NomorBukti As TextBox
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_JumlahTransaksi As TextBox
    Friend WithEvents txt_NamaLawanTransaksi As TextBox
    Friend WithEvents lbl_NamaLawanTransaksi As Label
    Friend WithEvents lbl_KodeLawanTransaksi As Label
    Friend WithEvents txt_KodeLawanTransaksi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_UraianTransaksi As RichTextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_NamaAkun As TextBox
    Friend WithEvents txt_COADebet As TextBox
    Friend WithEvents btn_PilihCOADebet As Button
End Class
