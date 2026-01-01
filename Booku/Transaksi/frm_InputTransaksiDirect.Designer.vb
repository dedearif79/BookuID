<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputTransaksiDirect
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
        btn_Lanjutkan = New Button()
        btn_Batal = New Button()
        Label1 = New Label()
        txt_NomorBukti = New TextBox()
        txt_NamaLawanTransaksi = New TextBox()
        txt_JumlahTransaksi = New TextBox()
        Label6 = New Label()
        txt_Uraian = New RichTextBox()
        Label3 = New Label()
        Label4 = New Label()
        dtp_TanggalTransaksi = New DateTimePicker()
        txt_NamaAkunLawan = New TextBox()
        cmb_AlurTransaksi = New ComboBox()
        Label17 = New Label()
        grb_LawanAkun = New GroupBox()
        btn_PilihCOA = New Button()
        txt_COALawan = New TextBox()
        grb_LawanTransaksi = New GroupBox()
        btn_PilihKodeLawanTransaksi = New Button()
        txt_KodeLawanTransaksi = New TextBox()
        btn_Simpan = New Button()
        txt_NamaProduk = New TextBox()
        Label2 = New Label()
        grb_LawanAkun.SuspendLayout()
        grb_LawanTransaksi.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_Lanjutkan
        ' 
        btn_Lanjutkan.Location = New Point(13, 626)
        btn_Lanjutkan.Margin = New Padding(4, 3, 4, 3)
        btn_Lanjutkan.Name = "btn_Lanjutkan"
        btn_Lanjutkan.Size = New Size(174, 40)
        btn_Lanjutkan.TabIndex = 9931
        btn_Lanjutkan.Text = "Lanjutkan >>"
        btn_Lanjutkan.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Location = New Point(131, 576)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 9933
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(24, 75)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(75, 15)
        Label1.TabIndex = 9934
        Label1.Text = "Nomor Bukti"
        ' 
        ' txt_NomorBukti
        ' 
        txt_NomorBukti.Location = New Point(152, 72)
        txt_NomorBukti.Margin = New Padding(4, 3, 4, 3)
        txt_NomorBukti.MaxLength = 999
        txt_NomorBukti.Name = "txt_NomorBukti"
        txt_NomorBukti.Size = New Size(181, 23)
        txt_NomorBukti.TabIndex = 20
        ' 
        ' txt_NamaLawanTransaksi
        ' 
        txt_NamaLawanTransaksi.Enabled = False
        txt_NamaLawanTransaksi.Location = New Point(10, 57)
        txt_NamaLawanTransaksi.Margin = New Padding(4, 3, 4, 3)
        txt_NamaLawanTransaksi.Name = "txt_NamaLawanTransaksi"
        txt_NamaLawanTransaksi.Size = New Size(305, 23)
        txt_NamaLawanTransaksi.TabIndex = 50
        txt_NamaLawanTransaksi.TextAlign = HorizontalAlignment.Center
        ' 
        ' txt_JumlahTransaksi
        ' 
        txt_JumlahTransaksi.Location = New Point(152, 345)
        txt_JumlahTransaksi.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahTransaksi.Name = "txt_JumlahTransaksi"
        txt_JumlahTransaksi.Size = New Size(181, 23)
        txt_JumlahTransaksi.TabIndex = 90
        txt_JumlahTransaksi.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(24, 348)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(95, 15)
        Label6.TabIndex = 9945
        Label6.Text = "Jumlah Transaksi"
        ' 
        ' txt_Uraian
        ' 
        txt_Uraian.Location = New Point(28, 483)
        txt_Uraian.Margin = New Padding(4, 3, 4, 3)
        txt_Uraian.Name = "txt_Uraian"
        txt_Uraian.Size = New Size(305, 74)
        txt_Uraian.TabIndex = 900
        txt_Uraian.Text = ""
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(24, 465)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(47, 15)
        Label3.TabIndex = 9947
        Label3.Text = "Uraian :"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(25, 423)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(98, 15)
        Label4.TabIndex = 9949
        Label4.Text = "Tanggal Transaksi"
        ' 
        ' dtp_TanggalTransaksi
        ' 
        dtp_TanggalTransaksi.CustomFormat = "dd/MM/yyyy"
        dtp_TanggalTransaksi.Format = DateTimePickerFormat.Custom
        dtp_TanggalTransaksi.Location = New Point(153, 419)
        dtp_TanggalTransaksi.Margin = New Padding(4, 3, 4, 3)
        dtp_TanggalTransaksi.Name = "dtp_TanggalTransaksi"
        dtp_TanggalTransaksi.Size = New Size(94, 23)
        dtp_TanggalTransaksi.TabIndex = 110
        ' 
        ' txt_NamaAkunLawan
        ' 
        txt_NamaAkunLawan.Enabled = False
        txt_NamaAkunLawan.Location = New Point(10, 55)
        txt_NamaAkunLawan.Margin = New Padding(4, 3, 4, 3)
        txt_NamaAkunLawan.Name = "txt_NamaAkunLawan"
        txt_NamaAkunLawan.Size = New Size(305, 23)
        txt_NamaAkunLawan.TabIndex = 80
        txt_NamaAkunLawan.TextAlign = HorizontalAlignment.Center
        ' 
        ' cmb_AlurTransaksi
        ' 
        cmb_AlurTransaksi.FormattingEnabled = True
        cmb_AlurTransaksi.Location = New Point(152, 32)
        cmb_AlurTransaksi.Margin = New Padding(4, 3, 4, 3)
        cmb_AlurTransaksi.Name = "cmb_AlurTransaksi"
        cmb_AlurTransaksi.Size = New Size(70, 23)
        cmb_AlurTransaksi.TabIndex = 10
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(24, 36)
        Label17.Margin = New Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(79, 15)
        Label17.TabIndex = 10080
        Label17.Text = "Alur Transaksi"
        ' 
        ' grb_LawanAkun
        ' 
        grb_LawanAkun.Controls.Add(btn_PilihCOA)
        grb_LawanAkun.Controls.Add(txt_COALawan)
        grb_LawanAkun.Controls.Add(txt_NamaAkunLawan)
        grb_LawanAkun.Location = New Point(18, 230)
        grb_LawanAkun.Margin = New Padding(4, 3, 4, 3)
        grb_LawanAkun.Name = "grb_LawanAkun"
        grb_LawanAkun.Padding = New Padding(4, 3, 4, 3)
        grb_LawanAkun.Size = New Size(329, 96)
        grb_LawanAkun.TabIndex = 55
        grb_LawanAkun.TabStop = False
        grb_LawanAkun.Text = "Lawan Akun : "
        ' 
        ' btn_PilihCOA
        ' 
        btn_PilihCOA.Location = New Point(135, 23)
        btn_PilihCOA.Margin = New Padding(4, 3, 4, 3)
        btn_PilihCOA.Name = "btn_PilihCOA"
        btn_PilihCOA.Size = New Size(70, 27)
        btn_PilihCOA.TabIndex = 70
        btn_PilihCOA.Text = "Pilih"
        btn_PilihCOA.UseVisualStyleBackColor = True
        ' 
        ' txt_COALawan
        ' 
        txt_COALawan.Location = New Point(10, 25)
        txt_COALawan.Margin = New Padding(4, 3, 4, 3)
        txt_COALawan.Name = "txt_COALawan"
        txt_COALawan.Size = New Size(117, 23)
        txt_COALawan.TabIndex = 60
        txt_COALawan.TextAlign = HorizontalAlignment.Center
        ' 
        ' grb_LawanTransaksi
        ' 
        grb_LawanTransaksi.Controls.Add(btn_PilihKodeLawanTransaksi)
        grb_LawanTransaksi.Controls.Add(txt_KodeLawanTransaksi)
        grb_LawanTransaksi.Controls.Add(txt_NamaLawanTransaksi)
        grb_LawanTransaksi.Location = New Point(18, 117)
        grb_LawanTransaksi.Margin = New Padding(4, 3, 4, 3)
        grb_LawanTransaksi.Name = "grb_LawanTransaksi"
        grb_LawanTransaksi.Padding = New Padding(4, 3, 4, 3)
        grb_LawanTransaksi.Size = New Size(329, 96)
        grb_LawanTransaksi.TabIndex = 25
        grb_LawanTransaksi.TabStop = False
        grb_LawanTransaksi.Text = "Lawan Transaksi : "
        ' 
        ' btn_PilihKodeLawanTransaksi
        ' 
        btn_PilihKodeLawanTransaksi.Location = New Point(135, 23)
        btn_PilihKodeLawanTransaksi.Margin = New Padding(4, 3, 4, 3)
        btn_PilihKodeLawanTransaksi.Name = "btn_PilihKodeLawanTransaksi"
        btn_PilihKodeLawanTransaksi.Size = New Size(70, 27)
        btn_PilihKodeLawanTransaksi.TabIndex = 40
        btn_PilihKodeLawanTransaksi.Text = "Pilih"
        btn_PilihKodeLawanTransaksi.UseVisualStyleBackColor = True
        ' 
        ' txt_KodeLawanTransaksi
        ' 
        txt_KodeLawanTransaksi.Location = New Point(10, 25)
        txt_KodeLawanTransaksi.Margin = New Padding(4, 3, 4, 3)
        txt_KodeLawanTransaksi.Name = "txt_KodeLawanTransaksi"
        txt_KodeLawanTransaksi.Size = New Size(117, 23)
        txt_KodeLawanTransaksi.TabIndex = 30
        txt_KodeLawanTransaksi.TextAlign = HorizontalAlignment.Center
        ' 
        ' btn_Simpan
        ' 
        btn_Simpan.Location = New Point(236, 576)
        btn_Simpan.Margin = New Padding(4, 3, 4, 3)
        btn_Simpan.Name = "btn_Simpan"
        btn_Simpan.Size = New Size(97, 40)
        btn_Simpan.TabIndex = 10081
        btn_Simpan.Text = "Simpan"
        btn_Simpan.UseVisualStyleBackColor = True
        ' 
        ' txt_NamaProduk
        ' 
        txt_NamaProduk.Location = New Point(152, 374)
        txt_NamaProduk.Margin = New Padding(4, 3, 4, 3)
        txt_NamaProduk.Name = "txt_NamaProduk"
        txt_NamaProduk.Size = New Size(181, 23)
        txt_NamaProduk.TabIndex = 10082
        txt_NamaProduk.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(24, 377)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(105, 15)
        Label2.TabIndex = 10083
        Label2.Text = "Nama Barang/Jasa"
        ' 
        ' frm_InputTransaksiDirect
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(364, 635)
        Controls.Add(txt_NamaProduk)
        Controls.Add(Label2)
        Controls.Add(btn_Simpan)
        Controls.Add(grb_LawanTransaksi)
        Controls.Add(grb_LawanAkun)
        Controls.Add(cmb_AlurTransaksi)
        Controls.Add(Label17)
        Controls.Add(dtp_TanggalTransaksi)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(txt_Uraian)
        Controls.Add(txt_JumlahTransaksi)
        Controls.Add(Label6)
        Controls.Add(txt_NomorBukti)
        Controls.Add(Label1)
        Controls.Add(btn_Lanjutkan)
        Controls.Add(btn_Batal)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frm_InputTransaksiDirect"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input Transaksi Kas"
        grb_LawanAkun.ResumeLayout(False)
        grb_LawanAkun.PerformLayout()
        grb_LawanTransaksi.ResumeLayout(False)
        grb_LawanTransaksi.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btn_Lanjutkan As System.Windows.Forms.Button
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_NomorBukti As System.Windows.Forms.TextBox
    Friend WithEvents txt_NamaLawanTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents txt_JumlahTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_Uraian As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtp_TanggalTransaksi As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_NamaAkunLawan As System.Windows.Forms.TextBox
    Friend WithEvents cmb_AlurTransaksi As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents grb_LawanAkun As System.Windows.Forms.GroupBox
    Friend WithEvents txt_COALawan As System.Windows.Forms.TextBox
    Friend WithEvents btn_PilihCOA As System.Windows.Forms.Button
    Friend WithEvents grb_LawanTransaksi As System.Windows.Forms.GroupBox
    Friend WithEvents btn_PilihKodeLawanTransaksi As System.Windows.Forms.Button
    Friend WithEvents txt_KodeLawanTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents btn_Simpan As Button
    Friend WithEvents txt_NamaProduk As TextBox
    Friend WithEvents Label2 As Label
End Class
