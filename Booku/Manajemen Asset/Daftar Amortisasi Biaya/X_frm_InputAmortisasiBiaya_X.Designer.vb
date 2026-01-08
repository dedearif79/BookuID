<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_InputAmortisasiBiaya_X
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
        Me.btn_Reset = New System.Windows.Forms.Button()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Tutup = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.txt_NamaAkun_Amortisasi = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_MasaAmortisasi = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_COA_Amortisasi = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btn_PilihCOA_Amortisasi = New System.Windows.Forms.Button()
        Me.dtp_TanggalTransaksi = New System.Windows.Forms.DateTimePicker()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txt_JumlahTransaksi = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.grb_AkunAmortisasi = New System.Windows.Forms.GroupBox()
        Me.grb_AkunBiaya = New System.Windows.Forms.GroupBox()
        Me.txt_COA_Biaya = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_NamaAkun_Biaya = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_PilihCOA_Biaya = New System.Windows.Forms.Button()
        Me.txt_KodeAsset = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.grb_AkunAmortisasi.SuspendLayout()
        Me.grb_AkunBiaya.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_Reset
        '
        Me.btn_Reset.Location = New System.Drawing.Point(104, 507)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(83, 35)
        Me.btn_Reset.TabIndex = 9100
        Me.btn_Reset.Text = "Reset"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(277, 507)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9999
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Location = New System.Drawing.Point(191, 507)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tutup.TabIndex = 9300
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 381)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 9947
        Me.Label7.Text = "Keterangan"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(115, 378)
        Me.txt_Keterangan.MaxLength = 99
        Me.txt_Keterangan.Multiline = True
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(245, 98)
        Me.txt_Keterangan.TabIndex = 200
        '
        'txt_NamaAkun_Amortisasi
        '
        Me.txt_NamaAkun_Amortisasi.Enabled = False
        Me.txt_NamaAkun_Amortisasi.Location = New System.Drawing.Point(103, 55)
        Me.txt_NamaAkun_Amortisasi.Name = "txt_NamaAkun_Amortisasi"
        Me.txt_NamaAkun_Amortisasi.Size = New System.Drawing.Size(245, 20)
        Me.txt_NamaAkun_Amortisasi.TabIndex = 40
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 9961
        Me.Label3.Text = "Nama Akun"
        '
        'txt_MasaAmortisasi
        '
        Me.txt_MasaAmortisasi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_MasaAmortisasi.Location = New System.Drawing.Point(115, 282)
        Me.txt_MasaAmortisasi.MaxLength = 9
        Me.txt_MasaAmortisasi.Name = "txt_MasaAmortisasi"
        Me.txt_MasaAmortisasi.Size = New System.Drawing.Size(36, 20)
        Me.txt_MasaAmortisasi.TabIndex = 90
        Me.txt_MasaAmortisasi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 285)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 9963
        Me.Label4.Text = "Masa Amortisasi"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(153, 285)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 9964
        Me.Label10.Text = "Bulan"
        '
        'txt_COA_Amortisasi
        '
        Me.txt_COA_Amortisasi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_COA_Amortisasi.Location = New System.Drawing.Point(103, 29)
        Me.txt_COA_Amortisasi.MaxLength = 9
        Me.txt_COA_Amortisasi.Name = "txt_COA_Amortisasi"
        Me.txt_COA_Amortisasi.Size = New System.Drawing.Size(69, 20)
        Me.txt_COA_Amortisasi.TabIndex = 20
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 9966
        Me.Label11.Text = "Kode Akun"
        '
        'btn_PilihCOA_Amortisasi
        '
        Me.btn_PilihCOA_Amortisasi.Location = New System.Drawing.Point(179, 27)
        Me.btn_PilihCOA_Amortisasi.Name = "btn_PilihCOA_Amortisasi"
        Me.btn_PilihCOA_Amortisasi.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihCOA_Amortisasi.TabIndex = 30
        Me.btn_PilihCOA_Amortisasi.Text = "Pilih"
        Me.btn_PilihCOA_Amortisasi.UseVisualStyleBackColor = True
        '
        'dtp_TanggalTransaksi
        '
        Me.dtp_TanggalTransaksi.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalTransaksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalTransaksi.Location = New System.Drawing.Point(115, 314)
        Me.dtp_TanggalTransaksi.Name = "dtp_TanggalTransaksi"
        Me.dtp_TanggalTransaksi.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalTransaksi.TabIndex = 110
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(18, 317)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(74, 13)
        Me.Label50.TabIndex = 10086
        Me.Label50.Text = "Tgl. Transaksi"
        '
        'txt_JumlahTransaksi
        '
        Me.txt_JumlahTransaksi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_JumlahTransaksi.Location = New System.Drawing.Point(115, 346)
        Me.txt_JumlahTransaksi.MaxLength = 24
        Me.txt_JumlahTransaksi.Name = "txt_JumlahTransaksi"
        Me.txt_JumlahTransaksi.Size = New System.Drawing.Size(141, 20)
        Me.txt_JumlahTransaksi.TabIndex = 120
        Me.txt_JumlahTransaksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(18, 349)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 13)
        Me.Label14.TabIndex = 10088
        Me.Label14.Text = "Jml. Transaksi"
        '
        'grb_AkunAmortisasi
        '
        Me.grb_AkunAmortisasi.Controls.Add(Me.txt_COA_Amortisasi)
        Me.grb_AkunAmortisasi.Controls.Add(Me.Label3)
        Me.grb_AkunAmortisasi.Controls.Add(Me.txt_NamaAkun_Amortisasi)
        Me.grb_AkunAmortisasi.Controls.Add(Me.Label11)
        Me.grb_AkunAmortisasi.Controls.Add(Me.btn_PilihCOA_Amortisasi)
        Me.grb_AkunAmortisasi.Location = New System.Drawing.Point(12, 66)
        Me.grb_AkunAmortisasi.Name = "grb_AkunAmortisasi"
        Me.grb_AkunAmortisasi.Size = New System.Drawing.Size(366, 93)
        Me.grb_AkunAmortisasi.TabIndex = 10
        Me.grb_AkunAmortisasi.TabStop = False
        Me.grb_AkunAmortisasi.Text = "Akun Amortisasi :"
        '
        'grb_AkunBiaya
        '
        Me.grb_AkunBiaya.Controls.Add(Me.txt_COA_Biaya)
        Me.grb_AkunBiaya.Controls.Add(Me.Label1)
        Me.grb_AkunBiaya.Controls.Add(Me.txt_NamaAkun_Biaya)
        Me.grb_AkunBiaya.Controls.Add(Me.Label2)
        Me.grb_AkunBiaya.Controls.Add(Me.btn_PilihCOA_Biaya)
        Me.grb_AkunBiaya.Location = New System.Drawing.Point(12, 173)
        Me.grb_AkunBiaya.Name = "grb_AkunBiaya"
        Me.grb_AkunBiaya.Size = New System.Drawing.Size(366, 93)
        Me.grb_AkunBiaya.TabIndex = 50
        Me.grb_AkunBiaya.TabStop = False
        Me.grb_AkunBiaya.Text = "Akun Biaya :"
        '
        'txt_COA_Biaya
        '
        Me.txt_COA_Biaya.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_COA_Biaya.Location = New System.Drawing.Point(103, 29)
        Me.txt_COA_Biaya.MaxLength = 9
        Me.txt_COA_Biaya.Name = "txt_COA_Biaya"
        Me.txt_COA_Biaya.Size = New System.Drawing.Size(69, 20)
        Me.txt_COA_Biaya.TabIndex = 60
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 9961
        Me.Label1.Text = "Nama Akun"
        '
        'txt_NamaAkun_Biaya
        '
        Me.txt_NamaAkun_Biaya.Enabled = False
        Me.txt_NamaAkun_Biaya.Location = New System.Drawing.Point(103, 55)
        Me.txt_NamaAkun_Biaya.Name = "txt_NamaAkun_Biaya"
        Me.txt_NamaAkun_Biaya.Size = New System.Drawing.Size(245, 20)
        Me.txt_NamaAkun_Biaya.TabIndex = 80
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 9966
        Me.Label2.Text = "Kode Akun"
        '
        'btn_PilihCOA_Biaya
        '
        Me.btn_PilihCOA_Biaya.Location = New System.Drawing.Point(179, 27)
        Me.btn_PilihCOA_Biaya.Name = "btn_PilihCOA_Biaya"
        Me.btn_PilihCOA_Biaya.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihCOA_Biaya.TabIndex = 70
        Me.btn_PilihCOA_Biaya.Text = "Pilih"
        Me.btn_PilihCOA_Biaya.UseVisualStyleBackColor = True
        '
        'txt_KodeAsset
        '
        Me.txt_KodeAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_KodeAsset.Enabled = False
        Me.txt_KodeAsset.Location = New System.Drawing.Point(115, 31)
        Me.txt_KodeAsset.MaxLength = 333
        Me.txt_KodeAsset.Name = "txt_KodeAsset"
        Me.txt_KodeAsset.Size = New System.Drawing.Size(141, 20)
        Me.txt_KodeAsset.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 10090
        Me.Label5.Text = "Kode Asset"
        '
        'frm_InputAmortisasiBiaya
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 558)
        Me.Controls.Add(Me.txt_KodeAsset)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.grb_AkunBiaya)
        Me.Controls.Add(Me.grb_AkunAmortisasi)
        Me.Controls.Add(Me.txt_JumlahTransaksi)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.dtp_TanggalTransaksi)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_MasaAmortisasi)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btn_Reset)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Tutup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputAmortisasiBiaya"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Amortisasi Biaya"
        Me.grb_AkunAmortisasi.ResumeLayout(False)
        Me.grb_AkunAmortisasi.PerformLayout()
        Me.grb_AkunBiaya.ResumeLayout(False)
        Me.grb_AkunBiaya.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_Keterangan As System.Windows.Forms.TextBox
    Friend WithEvents txt_NamaAkun_Amortisasi As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_MasaAmortisasi As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_COA_Amortisasi As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btn_PilihCOA_Amortisasi As System.Windows.Forms.Button
    Friend WithEvents dtp_TanggalTransaksi As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents grb_AkunAmortisasi As System.Windows.Forms.GroupBox
    Friend WithEvents grb_AkunBiaya As System.Windows.Forms.GroupBox
    Friend WithEvents txt_COA_Biaya As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_NamaAkun_Biaya As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btn_PilihCOA_Biaya As System.Windows.Forms.Button
    Friend WithEvents txt_KodeAsset As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
