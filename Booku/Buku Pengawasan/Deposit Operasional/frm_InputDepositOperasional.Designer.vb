<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputDepositOperasional
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
        Me.txt_NomorBPDO = New System.Windows.Forms.TextBox()
        Me.btn_PilihLawanTransaksi = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_JumlahTransaksi = New System.Windows.Forms.TextBox()
        Me.txt_NamaLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.lbl_NamaLawanTransaksi = New System.Windows.Forms.Label()
        Me.lbl_KodeLawanTransaksi = New System.Windows.Forms.Label()
        Me.txt_KodeLawanTransaksi = New System.Windows.Forms.TextBox()
        Me.dtp_TanggalBukti = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_NomorFakturPajak = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_NomorBukti = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_UraianTransaksi = New System.Windows.Forms.RichTextBox()
        Me.btn_PilihCustomer = New System.Windows.Forms.Button()
        Me.txt_NamaCustomer = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_KodeCustomer = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(254, 486)
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
        Me.btn_Batal.Location = New System.Drawing.Point(165, 486)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10327
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'lbl_NomorBPBG
        '
        Me.lbl_NomorBPBG.AutoSize = True
        Me.lbl_NomorBPBG.Location = New System.Drawing.Point(27, 37)
        Me.lbl_NomorBPBG.Name = "lbl_NomorBPBG"
        Me.lbl_NomorBPBG.Size = New System.Drawing.Size(71, 13)
        Me.lbl_NomorBPBG.TabIndex = 10420
        Me.lbl_NomorBPBG.Text = "Nomor BPDO"
        '
        'txt_NomorBPDO
        '
        Me.txt_NomorBPDO.Location = New System.Drawing.Point(153, 34)
        Me.txt_NomorBPDO.MaxLength = 99
        Me.txt_NomorBPDO.Name = "txt_NomorBPDO"
        Me.txt_NomorBPDO.Size = New System.Drawing.Size(101, 20)
        Me.txt_NomorBPDO.TabIndex = 10
        '
        'btn_PilihLawanTransaksi
        '
        Me.btn_PilihLawanTransaksi.Location = New System.Drawing.Point(256, 136)
        Me.btn_PilihLawanTransaksi.Name = "btn_PilihLawanTransaksi"
        Me.btn_PilihLawanTransaksi.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihLawanTransaksi.TabIndex = 60
        Me.btn_PilihLawanTransaksi.Text = "Pilih"
        Me.btn_PilihLawanTransaksi.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 311)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 10419
        Me.Label1.Text = "Jumlah Transaksi"
        '
        'txt_JumlahTransaksi
        '
        Me.txt_JumlahTransaksi.Location = New System.Drawing.Point(153, 308)
        Me.txt_JumlahTransaksi.MaxLength = 99
        Me.txt_JumlahTransaksi.Name = "txt_JumlahTransaksi"
        Me.txt_JumlahTransaksi.Size = New System.Drawing.Size(101, 20)
        Me.txt_JumlahTransaksi.TabIndex = 120
        Me.txt_JumlahTransaksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NamaLawanTransaksi
        '
        Me.txt_NamaLawanTransaksi.Location = New System.Drawing.Point(153, 164)
        Me.txt_NamaLawanTransaksi.MaxLength = 99
        Me.txt_NamaLawanTransaksi.Name = "txt_NamaLawanTransaksi"
        Me.txt_NamaLawanTransaksi.Size = New System.Drawing.Size(183, 20)
        Me.txt_NamaLawanTransaksi.TabIndex = 70
        '
        'lbl_NamaLawanTransaksi
        '
        Me.lbl_NamaLawanTransaksi.AutoSize = True
        Me.lbl_NamaLawanTransaksi.Location = New System.Drawing.Point(27, 167)
        Me.lbl_NamaLawanTransaksi.Name = "lbl_NamaLawanTransaksi"
        Me.lbl_NamaLawanTransaksi.Size = New System.Drawing.Size(119, 13)
        Me.lbl_NamaLawanTransaksi.TabIndex = 10418
        Me.lbl_NamaLawanTransaksi.Text = "Nama Lawan Transaksi"
        '
        'lbl_KodeLawanTransaksi
        '
        Me.lbl_KodeLawanTransaksi.AutoSize = True
        Me.lbl_KodeLawanTransaksi.Location = New System.Drawing.Point(27, 141)
        Me.lbl_KodeLawanTransaksi.Name = "lbl_KodeLawanTransaksi"
        Me.lbl_KodeLawanTransaksi.Size = New System.Drawing.Size(116, 13)
        Me.lbl_KodeLawanTransaksi.TabIndex = 10417
        Me.lbl_KodeLawanTransaksi.Text = "Kode Lawan Transaksi"
        '
        'txt_KodeLawanTransaksi
        '
        Me.txt_KodeLawanTransaksi.Location = New System.Drawing.Point(153, 138)
        Me.txt_KodeLawanTransaksi.MaxLength = 99
        Me.txt_KodeLawanTransaksi.Name = "txt_KodeLawanTransaksi"
        Me.txt_KodeLawanTransaksi.Size = New System.Drawing.Size(101, 20)
        Me.txt_KodeLawanTransaksi.TabIndex = 50
        '
        'dtp_TanggalBukti
        '
        Me.dtp_TanggalBukti.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalBukti.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalBukti.Location = New System.Drawing.Point(153, 86)
        Me.dtp_TanggalBukti.Name = "dtp_TanggalBukti"
        Me.dtp_TanggalBukti.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalBukti.TabIndex = 30
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 10416
        Me.Label4.Text = "Tanggal Bukti"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 115)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 10424
        Me.Label2.Text = "Nomor Faktur Pajak"
        '
        'txt_NomorFakturPajak
        '
        Me.txt_NomorFakturPajak.Location = New System.Drawing.Point(153, 112)
        Me.txt_NomorFakturPajak.MaxLength = 99
        Me.txt_NomorFakturPajak.Name = "txt_NomorFakturPajak"
        Me.txt_NomorFakturPajak.Size = New System.Drawing.Size(183, 20)
        Me.txt_NomorFakturPajak.TabIndex = 40
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 357)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 10432
        Me.Label7.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Keterangan.Location = New System.Drawing.Point(30, 376)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(306, 84)
        Me.txt_Keterangan.TabIndex = 300
        Me.txt_Keterangan.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 10434
        Me.Label6.Text = "Nomor Bukti"
        '
        'txt_NomorBukti
        '
        Me.txt_NomorBukti.Location = New System.Drawing.Point(153, 60)
        Me.txt_NomorBukti.MaxLength = 99
        Me.txt_NomorBukti.Name = "txt_NomorBukti"
        Me.txt_NomorBukti.Size = New System.Drawing.Size(183, 20)
        Me.txt_NomorBukti.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(27, 196)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 10436
        Me.Label8.Text = "Uraian Transaksi"
        '
        'txt_UraianTransaksi
        '
        Me.txt_UraianTransaksi.Location = New System.Drawing.Point(153, 193)
        Me.txt_UraianTransaksi.Name = "txt_UraianTransaksi"
        Me.txt_UraianTransaksi.Size = New System.Drawing.Size(183, 54)
        Me.txt_UraianTransaksi.TabIndex = 80
        Me.txt_UraianTransaksi.Text = ""
        '
        'btn_PilihCustomer
        '
        Me.btn_PilihCustomer.Location = New System.Drawing.Point(256, 254)
        Me.btn_PilihCustomer.Name = "btn_PilihCustomer"
        Me.btn_PilihCustomer.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihCustomer.TabIndex = 100
        Me.btn_PilihCustomer.Text = "Pilih"
        Me.btn_PilihCustomer.UseVisualStyleBackColor = True
        '
        'txt_NamaCustomer
        '
        Me.txt_NamaCustomer.Location = New System.Drawing.Point(153, 282)
        Me.txt_NamaCustomer.MaxLength = 99
        Me.txt_NamaCustomer.Name = "txt_NamaCustomer"
        Me.txt_NamaCustomer.Size = New System.Drawing.Size(183, 20)
        Me.txt_NamaCustomer.TabIndex = 110
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(27, 285)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 10441
        Me.Label9.Text = "Nama Customer"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(27, 259)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 13)
        Me.Label10.TabIndex = 10440
        Me.Label10.Text = "Kode Customer"
        '
        'txt_KodeCustomer
        '
        Me.txt_KodeCustomer.Location = New System.Drawing.Point(153, 256)
        Me.txt_KodeCustomer.MaxLength = 99
        Me.txt_KodeCustomer.Name = "txt_KodeCustomer"
        Me.txt_KodeCustomer.Size = New System.Drawing.Size(101, 20)
        Me.txt_KodeCustomer.TabIndex = 90
        '
        'frm_InputDepositOperasional
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(367, 542)
        Me.Controls.Add(Me.btn_PilihCustomer)
        Me.Controls.Add(Me.txt_NamaCustomer)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_KodeCustomer)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_UraianTransaksi)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_NomorBukti)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_NomorFakturPajak)
        Me.Controls.Add(Me.lbl_NomorBPBG)
        Me.Controls.Add(Me.txt_NomorBPDO)
        Me.Controls.Add(Me.btn_PilihLawanTransaksi)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_JumlahTransaksi)
        Me.Controls.Add(Me.txt_NamaLawanTransaksi)
        Me.Controls.Add(Me.lbl_NamaLawanTransaksi)
        Me.Controls.Add(Me.lbl_KodeLawanTransaksi)
        Me.Controls.Add(Me.txt_KodeLawanTransaksi)
        Me.Controls.Add(Me.dtp_TanggalBukti)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputDepositOperasional"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Bank Garansi"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents lbl_NomorBPBG As Label
    Friend WithEvents txt_NomorBPDO As TextBox
    Friend WithEvents btn_PilihLawanTransaksi As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_JumlahTransaksi As TextBox
    Friend WithEvents txt_NamaLawanTransaksi As TextBox
    Friend WithEvents lbl_NamaLawanTransaksi As Label
    Friend WithEvents lbl_KodeLawanTransaksi As Label
    Friend WithEvents txt_KodeLawanTransaksi As TextBox
    Friend WithEvents dtp_TanggalBukti As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NomorFakturPajak As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_NomorBukti As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_UraianTransaksi As RichTextBox
    Friend WithEvents btn_PilihCustomer As Button
    Friend WithEvents txt_NamaCustomer As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_KodeCustomer As TextBox
End Class
