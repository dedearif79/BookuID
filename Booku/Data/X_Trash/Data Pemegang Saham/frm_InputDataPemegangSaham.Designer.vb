<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputDataPemegangSaham
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
        txt_JumlahSaham = New TextBox()
        Label7 = New Label()
        txt_JumlahLembar = New TextBox()
        Label2 = New Label()
        txt_HargaPerlembar = New TextBox()
        Label6 = New Label()
        Label4 = New Label()
        txt_Alamat = New RichTextBox()
        txt_Nama = New TextBox()
        Label3 = New Label()
        Label5 = New Label()
        txt_Catatan = New RichTextBox()
        txt_NIK = New TextBox()
        Label1 = New Label()
        txt_NPWP = New TextBox()
        Label8 = New Label()
        txt_AtasNama = New TextBox()
        Label9 = New Label()
        txt_RekeningBank = New TextBox()
        Label10 = New Label()
        cmb_LokasiPS = New ComboBox()
        Label11 = New Label()
        cmb_JenisPS = New ComboBox()
        Label12 = New Label()
        SuspendLayout()
        ' 
        ' btn_Simpan
        ' 
        btn_Simpan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Simpan.Location = New Point(271, 572)
        btn_Simpan.Margin = New Padding(4, 3, 4, 3)
        btn_Simpan.Name = "btn_Simpan"
        btn_Simpan.Size = New Size(97, 40)
        btn_Simpan.TabIndex = 10326
        btn_Simpan.Text = "Simpan"
        btn_Simpan.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn_Batal.DialogResult = DialogResult.Cancel
        btn_Batal.Location = New Point(167, 572)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 10327
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' txt_JumlahSaham
        ' 
        txt_JumlahSaham.Location = New Point(154, 322)
        txt_JumlahSaham.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahSaham.MaxLength = 99
        txt_JumlahSaham.Name = "txt_JumlahSaham"
        txt_JumlahSaham.Size = New Size(126, 23)
        txt_JumlahSaham.TabIndex = 60
        txt_JumlahSaham.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(31, 325)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(84, 15)
        Label7.TabIndex = 10373
        Label7.Text = "Jumlah Saham"
        ' 
        ' txt_JumlahLembar
        ' 
        txt_JumlahLembar.Location = New Point(154, 262)
        txt_JumlahLembar.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahLembar.MaxLength = 99
        txt_JumlahLembar.Name = "txt_JumlahLembar"
        txt_JumlahLembar.Size = New Size(75, 23)
        txt_JumlahLembar.TabIndex = 40
        txt_JumlahLembar.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(31, 265)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(88, 15)
        Label2.TabIndex = 10371
        Label2.Text = "Jumlah Lembar"
        ' 
        ' txt_HargaPerlembar
        ' 
        txt_HargaPerlembar.Location = New Point(154, 292)
        txt_HargaPerlembar.Margin = New Padding(4, 3, 4, 3)
        txt_HargaPerlembar.MaxLength = 99
        txt_HargaPerlembar.Name = "txt_HargaPerlembar"
        txt_HargaPerlembar.Size = New Size(126, 23)
        txt_HargaPerlembar.TabIndex = 50
        txt_HargaPerlembar.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(31, 295)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(96, 15)
        Label6.TabIndex = 10370
        Label6.Text = "Harga Perlembar"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(31, 133)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(45, 15)
        Label4.TabIndex = 10367
        Label4.Text = "Alamat"
        ' 
        ' txt_Alamat
        ' 
        txt_Alamat.Location = New Point(154, 129)
        txt_Alamat.Margin = New Padding(4, 3, 4, 3)
        txt_Alamat.Name = "txt_Alamat"
        txt_Alamat.Size = New Size(213, 67)
        txt_Alamat.TabIndex = 30
        txt_Alamat.Text = ""
        ' 
        ' txt_Nama
        ' 
        txt_Nama.Location = New Point(154, 33)
        txt_Nama.Margin = New Padding(4, 3, 4, 3)
        txt_Nama.MaxLength = 99
        txt_Nama.Name = "txt_Nama"
        txt_Nama.Size = New Size(213, 23)
        txt_Nama.TabIndex = 10
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(31, 37)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(39, 15)
        Label3.TabIndex = 10365
        Label3.Text = "Nama"
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(31, 424)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(54, 15)
        Label5.TabIndex = 10364
        Label5.Text = "Catatan :"
        ' 
        ' txt_Catatan
        ' 
        txt_Catatan.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txt_Catatan.Location = New Point(35, 446)
        txt_Catatan.Margin = New Padding(4, 3, 4, 3)
        txt_Catatan.Name = "txt_Catatan"
        txt_Catatan.Size = New Size(332, 96)
        txt_Catatan.TabIndex = 70
        txt_Catatan.Text = ""
        ' 
        ' txt_NIK
        ' 
        txt_NIK.Location = New Point(154, 63)
        txt_NIK.Margin = New Padding(4, 3, 4, 3)
        txt_NIK.MaxLength = 99
        txt_NIK.Name = "txt_NIK"
        txt_NIK.Size = New Size(126, 23)
        txt_NIK.TabIndex = 20
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(31, 67)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(26, 15)
        Label1.TabIndex = 10363
        Label1.Text = "NIK"
        ' 
        ' txt_NPWP
        ' 
        txt_NPWP.Location = New Point(154, 96)
        txt_NPWP.Margin = New Padding(4, 3, 4, 3)
        txt_NPWP.MaxLength = 99
        txt_NPWP.Name = "txt_NPWP"
        txt_NPWP.Size = New Size(126, 23)
        txt_NPWP.TabIndex = 25
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(31, 99)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(41, 15)
        Label8.TabIndex = 10375
        Label8.Text = "NPWP"
        ' 
        ' txt_AtasNama
        ' 
        txt_AtasNama.Location = New Point(154, 382)
        txt_AtasNama.Margin = New Padding(4, 3, 4, 3)
        txt_AtasNama.Name = "txt_AtasNama"
        txt_AtasNama.Size = New Size(213, 23)
        txt_AtasNama.TabIndex = 10377
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(31, 385)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(65, 15)
        Label9.TabIndex = 10379
        Label9.Text = "Atas Nama"
        ' 
        ' txt_RekeningBank
        ' 
        txt_RekeningBank.Location = New Point(154, 352)
        txt_RekeningBank.Margin = New Padding(4, 3, 4, 3)
        txt_RekeningBank.Name = "txt_RekeningBank"
        txt_RekeningBank.Size = New Size(213, 23)
        txt_RekeningBank.TabIndex = 10376
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(31, 355)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(85, 15)
        Label10.TabIndex = 10378
        Label10.Text = "Rekening Bank"
        ' 
        ' cmb_LokasiPS
        ' 
        cmb_LokasiPS.FormattingEnabled = True
        cmb_LokasiPS.Location = New Point(154, 233)
        cmb_LokasiPS.Margin = New Padding(4, 3, 4, 3)
        cmb_LokasiPS.Name = "cmb_LokasiPS"
        cmb_LokasiPS.Size = New Size(140, 23)
        cmb_LokasiPS.TabIndex = 36
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(31, 236)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(40, 15)
        Label11.TabIndex = 10383
        Label11.Text = "Lokasi"
        ' 
        ' cmb_JenisPS
        ' 
        cmb_JenisPS.FormattingEnabled = True
        cmb_JenisPS.Location = New Point(154, 202)
        cmb_JenisPS.Margin = New Padding(4, 3, 4, 3)
        cmb_JenisPS.Name = "cmb_JenisPS"
        cmb_JenisPS.Size = New Size(140, 23)
        cmb_JenisPS.TabIndex = 33
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(31, 205)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(32, 15)
        Label12.TabIndex = 10382
        Label12.Text = "Jenis"
        ' 
        ' frm_InputDataPemegangSaham
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(400, 644)
        Controls.Add(cmb_LokasiPS)
        Controls.Add(Label11)
        Controls.Add(cmb_JenisPS)
        Controls.Add(Label12)
        Controls.Add(txt_AtasNama)
        Controls.Add(Label9)
        Controls.Add(txt_RekeningBank)
        Controls.Add(Label10)
        Controls.Add(txt_NPWP)
        Controls.Add(Label8)
        Controls.Add(txt_JumlahSaham)
        Controls.Add(Label7)
        Controls.Add(txt_JumlahLembar)
        Controls.Add(Label2)
        Controls.Add(txt_HargaPerlembar)
        Controls.Add(Label6)
        Controls.Add(Label4)
        Controls.Add(txt_Alamat)
        Controls.Add(txt_Nama)
        Controls.Add(Label3)
        Controls.Add(Label5)
        Controls.Add(txt_Catatan)
        Controls.Add(txt_NIK)
        Controls.Add(Label1)
        Controls.Add(btn_Simpan)
        Controls.Add(btn_Batal)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_InputDataPemegangSaham"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Input Data Pemegang Saham"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_JumlahSaham As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_JumlahLembar As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_HargaPerlembar As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_Alamat As RichTextBox
    Friend WithEvents txt_Nama As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Catatan As RichTextBox
    Friend WithEvents txt_NIK As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_NPWP As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_AtasNama As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_RekeningBank As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents cmb_LokasiPS As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents cmb_JenisPS As ComboBox
    Friend WithEvents Label12 As Label
End Class
