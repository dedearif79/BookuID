<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputBuktiPotongPPh
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
        btn_Simpan = New Button()
        btn_Batal = New Button()
        lbl_NomorBuktiPotong = New Label()
        txt_NomorBuktiPotong = New TextBox()
        lbl_DPP = New Label()
        txt_DPP = New TextBox()
        dtp_TanggalBuktiPotong = New DateTimePicker()
        Label4 = New Label()
        Label5 = New Label()
        txt_Keterangan = New RichTextBox()
        grb_DataTransaksi = New GroupBox()
        txt_JenisPPh = New TextBox()
        Label7 = New Label()
        txt_NamaLawanTransaksi = New TextBox()
        lbl_Mitra = New Label()
        txt_NomorFakturPajak = New TextBox()
        Label3 = New Label()
        dtp_TanggalInvoice = New DateTimePicker()
        Label2 = New Label()
        txt_NomorInvoice = New TextBox()
        Label1 = New Label()
        txt_PPhTerutang = New TextBox()
        lbl_PPhDipotong = New Label()
        dtp_TanggalDipotong = New DateTimePicker()
        Label8 = New Label()
        grb_DataTransaksi.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_Simpan
        ' 
        btn_Simpan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Simpan.Location = New Point(272, 603)
        btn_Simpan.Margin = New Padding(4, 3, 4, 3)
        btn_Simpan.Name = "btn_Simpan"
        btn_Simpan.Size = New Size(97, 40)
        btn_Simpan.TabIndex = 9000
        btn_Simpan.Text = "Simpan"
        btn_Simpan.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Batal.DialogResult = DialogResult.Cancel
        btn_Batal.Location = New Point(168, 603)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 9999
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' lbl_NomorBuktiPotong
        ' 
        lbl_NomorBuktiPotong.AutoSize = True
        lbl_NomorBuktiPotong.Location = New Point(24, 345)
        lbl_NomorBuktiPotong.Margin = New Padding(4, 0, 4, 0)
        lbl_NomorBuktiPotong.Name = "lbl_NomorBuktiPotong"
        lbl_NomorBuktiPotong.Size = New Size(117, 15)
        lbl_NomorBuktiPotong.TabIndex = 10366
        lbl_NomorBuktiPotong.Text = "Nomor Bukti Potong"
        ' 
        ' txt_NomorBuktiPotong
        ' 
        txt_NomorBuktiPotong.Location = New Point(168, 342)
        txt_NomorBuktiPotong.Margin = New Padding(4, 3, 4, 3)
        txt_NomorBuktiPotong.MaxLength = 99
        txt_NomorBuktiPotong.Name = "txt_NomorBuktiPotong"
        txt_NomorBuktiPotong.Size = New Size(200, 23)
        txt_NomorBuktiPotong.TabIndex = 200
        ' 
        ' lbl_DPP
        ' 
        lbl_DPP.AutoSize = True
        lbl_DPP.Location = New Point(9, 162)
        lbl_DPP.Margin = New Padding(4, 0, 4, 0)
        lbl_DPP.Name = "lbl_DPP"
        lbl_DPP.Size = New Size(29, 15)
        lbl_DPP.TabIndex = 10365
        lbl_DPP.Text = "DPP"
        ' 
        ' txt_DPP
        ' 
        txt_DPP.Location = New Point(140, 158)
        txt_DPP.Margin = New Padding(4, 3, 4, 3)
        txt_DPP.MaxLength = 99
        txt_DPP.Name = "txt_DPP"
        txt_DPP.Size = New Size(117, 23)
        txt_DPP.TabIndex = 10363
        txt_DPP.TextAlign = HorizontalAlignment.Right
        ' 
        ' dtp_TanggalBuktiPotong
        ' 
        dtp_TanggalBuktiPotong.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalBuktiPotong.Format = DateTimePickerFormat.Custom
        dtp_TanggalBuktiPotong.Location = New Point(168, 372)
        dtp_TanggalBuktiPotong.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalBuktiPotong.Name = "dtp_TanggalBuktiPotong"
        dtp_TanggalBuktiPotong.Size = New Size(94, 23)
        dtp_TanggalBuktiPotong.TabIndex = 300
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(24, 375)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(120, 15)
        Label4.TabIndex = 10364
        Label4.Text = "Tanggal Bukti Potong"
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(24, 460)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(73, 15)
        Label5.TabIndex = 10368
        Label5.Text = "Keterangan :"
        ' 
        ' txt_Keterangan
        ' 
        txt_Keterangan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_Keterangan.Location = New Point(28, 482)
        txt_Keterangan.Margin = New Padding(4, 3, 4, 3)
        txt_Keterangan.Name = "txt_Keterangan"
        txt_Keterangan.Size = New Size(340, 96)
        txt_Keterangan.TabIndex = 400
        txt_Keterangan.Text = ""
        ' 
        ' grb_DataTransaksi
        ' 
        grb_DataTransaksi.Controls.Add(txt_JenisPPh)
        grb_DataTransaksi.Controls.Add(Label7)
        grb_DataTransaksi.Controls.Add(txt_NamaLawanTransaksi)
        grb_DataTransaksi.Controls.Add(lbl_Mitra)
        grb_DataTransaksi.Controls.Add(txt_NomorFakturPajak)
        grb_DataTransaksi.Controls.Add(Label3)
        grb_DataTransaksi.Controls.Add(dtp_TanggalInvoice)
        grb_DataTransaksi.Controls.Add(Label2)
        grb_DataTransaksi.Controls.Add(txt_NomorInvoice)
        grb_DataTransaksi.Controls.Add(Label1)
        grb_DataTransaksi.Controls.Add(txt_PPhTerutang)
        grb_DataTransaksi.Controls.Add(lbl_PPhDipotong)
        grb_DataTransaksi.Controls.Add(txt_DPP)
        grb_DataTransaksi.Controls.Add(lbl_DPP)
        grb_DataTransaksi.Enabled = False
        grb_DataTransaksi.Location = New Point(28, 42)
        grb_DataTransaksi.Margin = New Padding(4, 3, 4, 3)
        grb_DataTransaksi.Name = "grb_DataTransaksi"
        grb_DataTransaksi.Padding = New Padding(4, 3, 4, 3)
        grb_DataTransaksi.Size = New Size(341, 260)
        grb_DataTransaksi.TabIndex = 100
        grb_DataTransaksi.TabStop = False
        grb_DataTransaksi.Text = "Data Transaksi :"
        ' 
        ' txt_JenisPPh
        ' 
        txt_JenisPPh.Location = New Point(140, 218)
        txt_JenisPPh.Margin = New Padding(4, 3, 4, 3)
        txt_JenisPPh.MaxLength = 99
        txt_JenisPPh.Name = "txt_JenisPPh"
        txt_JenisPPh.Size = New Size(117, 23)
        txt_JenisPPh.TabIndex = 10375
        txt_JenisPPh.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(9, 222)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(56, 15)
        Label7.TabIndex = 10376
        Label7.Text = "Jenis PPh"
        ' 
        ' txt_NamaLawanTransaksi
        ' 
        txt_NamaLawanTransaksi.Location = New Point(140, 128)
        txt_NamaLawanTransaksi.Margin = New Padding(4, 3, 4, 3)
        txt_NamaLawanTransaksi.MaxLength = 99
        txt_NamaLawanTransaksi.Name = "txt_NamaLawanTransaksi"
        txt_NamaLawanTransaksi.Size = New Size(176, 23)
        txt_NamaLawanTransaksi.TabIndex = 10373
        txt_NamaLawanTransaksi.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_Mitra
        ' 
        lbl_Mitra.AutoSize = True
        lbl_Mitra.Location = New Point(9, 132)
        lbl_Mitra.Margin = New Padding(4, 0, 4, 0)
        lbl_Mitra.Name = "lbl_Mitra"
        lbl_Mitra.Size = New Size(91, 15)
        lbl_Mitra.TabIndex = 10374
        lbl_Mitra.Text = "Lawan Transaksi"
        ' 
        ' txt_NomorFakturPajak
        ' 
        txt_NomorFakturPajak.Location = New Point(140, 98)
        txt_NomorFakturPajak.Margin = New Padding(4, 3, 4, 3)
        txt_NomorFakturPajak.MaxLength = 99
        txt_NomorFakturPajak.Name = "txt_NomorFakturPajak"
        txt_NomorFakturPajak.Size = New Size(176, 23)
        txt_NomorFakturPajak.TabIndex = 10371
        txt_NomorFakturPajak.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(9, 102)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(112, 15)
        Label3.TabIndex = 10372
        Label3.Text = "Nomor Faktur Pajak"
        ' 
        ' dtp_TanggalInvoice
        ' 
        dtp_TanggalInvoice.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalInvoice.Format = DateTimePickerFormat.Custom
        dtp_TanggalInvoice.Location = New Point(142, 68)
        dtp_TanggalInvoice.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalInvoice.Name = "dtp_TanggalInvoice"
        dtp_TanggalInvoice.Size = New Size(94, 23)
        dtp_TanggalInvoice.TabIndex = 10369
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(9, 72)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(89, 15)
        Label2.TabIndex = 10370
        Label2.Text = "Tanggal Invoice"
        ' 
        ' txt_NomorInvoice
        ' 
        txt_NomorInvoice.Location = New Point(140, 38)
        txt_NomorInvoice.Margin = New Padding(4, 3, 4, 3)
        txt_NomorInvoice.MaxLength = 99
        txt_NomorInvoice.Name = "txt_NomorInvoice"
        txt_NomorInvoice.Size = New Size(176, 23)
        txt_NomorInvoice.TabIndex = 10368
        txt_NomorInvoice.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(9, 42)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(86, 15)
        Label1.TabIndex = 10369
        Label1.Text = "Nomor Invoice"
        ' 
        ' txt_PPhTerutang
        ' 
        txt_PPhTerutang.Location = New Point(140, 188)
        txt_PPhTerutang.Margin = New Padding(4, 3, 4, 3)
        txt_PPhTerutang.MaxLength = 99
        txt_PPhTerutang.Name = "txt_PPhTerutang"
        txt_PPhTerutang.Size = New Size(117, 23)
        txt_PPhTerutang.TabIndex = 10366
        txt_PPhTerutang.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_PPhDipotong
        ' 
        lbl_PPhDipotong.AutoSize = True
        lbl_PPhDipotong.Location = New Point(9, 192)
        lbl_PPhDipotong.Margin = New Padding(4, 0, 4, 0)
        lbl_PPhDipotong.Name = "lbl_PPhDipotong"
        lbl_PPhDipotong.Size = New Size(77, 15)
        lbl_PPhDipotong.TabIndex = 10367
        lbl_PPhDipotong.Text = "PPh Terutang"
        ' 
        ' dtp_TanggalDipotong
        ' 
        dtp_TanggalDipotong.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalDipotong.Enabled = False
        dtp_TanggalDipotong.Format = DateTimePickerFormat.Custom
        dtp_TanggalDipotong.Location = New Point(168, 312)
        dtp_TanggalDipotong.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalDipotong.Name = "dtp_TanggalDipotong"
        dtp_TanggalDipotong.Size = New Size(94, 23)
        dtp_TanggalDipotong.TabIndex = 10369
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(24, 315)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(101, 15)
        Label8.TabIndex = 10370
        Label8.Text = "Tanggal Dipotong"
        ' 
        ' frm_InputBuktiPotongPPh
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(401, 673)
        Controls.Add(dtp_TanggalDipotong)
        Controls.Add(Label8)
        Controls.Add(grb_DataTransaksi)
        Controls.Add(Label5)
        Controls.Add(txt_Keterangan)
        Controls.Add(lbl_NomorBuktiPotong)
        Controls.Add(txt_NomorBuktiPotong)
        Controls.Add(dtp_TanggalBuktiPotong)
        Controls.Add(Label4)
        Controls.Add(btn_Simpan)
        Controls.Add(btn_Batal)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_InputBuktiPotongPPh"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input Bukti Potong PPh (Prepaid)"
        grb_DataTransaksi.ResumeLayout(False)
        grb_DataTransaksi.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents lbl_NomorBuktiPotong As Label
    Friend WithEvents txt_NomorBuktiPotong As TextBox
    Friend WithEvents lbl_DPP As Label
    Friend WithEvents txt_DPP As TextBox
    Friend WithEvents dtp_TanggalBuktiPotong As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents grb_DataTransaksi As GroupBox
    Friend WithEvents txt_PPhTerutang As TextBox
    Friend WithEvents lbl_PPhDipotong As Label
    Friend WithEvents txt_NamaLawanTransaksi As TextBox
    Friend WithEvents lbl_Mitra As Label
    Friend WithEvents txt_NomorFakturPajak As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dtp_TanggalInvoice As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NomorInvoice As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_JenisPPh As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents dtp_TanggalDipotong As DateTimePicker
    Friend WithEvents Label8 As Label
End Class
