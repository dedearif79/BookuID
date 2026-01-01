<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_InputMitra
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
        Me.btn_Reset = New System.Windows.Forms.Button()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Tutup = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_KodeMitra = New System.Windows.Forms.TextBox()
        Me.txt_NamaMitra = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Alamat = New System.Windows.Forms.TextBox()
        Me.chk_Supplier = New System.Windows.Forms.CheckBox()
        Me.chk_Customer = New System.Windows.Forms.CheckBox()
        Me.txt_NPWP = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_Email = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_PIC = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_RekeningBank = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_AtasNama = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmb_JenisWP = New System.Windows.Forms.ComboBox()
        Me.cmb_LokasiWP = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chk_Keuangan = New System.Windows.Forms.CheckBox()
        Me.chk_Afiliasi = New System.Windows.Forms.CheckBox()
        Me.chk_PJK = New System.Windows.Forms.CheckBox()
        Me.chk_PKP = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btn_Reset
        '
        Me.btn_Reset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Reset.Location = New System.Drawing.Point(88, 482)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(83, 35)
        Me.btn_Reset.TabIndex = 9100
        Me.btn_Reset.Text = "Reset"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(177, 482)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9000
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Tutup.Location = New System.Drawing.Point(266, 482)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tutup.TabIndex = 9200
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 9934
        Me.Label1.Text = "Kode Mitra"
        '
        'txt_KodeMitra
        '
        Me.txt_KodeMitra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_KodeMitra.Location = New System.Drawing.Point(104, 19)
        Me.txt_KodeMitra.MaxLength = 9
        Me.txt_KodeMitra.Name = "txt_KodeMitra"
        Me.txt_KodeMitra.Size = New System.Drawing.Size(95, 20)
        Me.txt_KodeMitra.TabIndex = 10
        '
        'txt_NamaMitra
        '
        Me.txt_NamaMitra.Location = New System.Drawing.Point(104, 45)
        Me.txt_NamaMitra.Name = "txt_NamaMitra"
        Me.txt_NamaMitra.Size = New System.Drawing.Size(245, 20)
        Me.txt_NamaMitra.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 9936
        Me.Label2.Text = "Nama Mitra"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 9938
        Me.Label3.Text = "Kategori"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 254)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 9947
        Me.Label7.Text = "Alamat"
        '
        'txt_Alamat
        '
        Me.txt_Alamat.Location = New System.Drawing.Point(104, 251)
        Me.txt_Alamat.MaxLength = 99
        Me.txt_Alamat.Multiline = True
        Me.txt_Alamat.Name = "txt_Alamat"
        Me.txt_Alamat.Size = New System.Drawing.Size(245, 46)
        Me.txt_Alamat.TabIndex = 80
        '
        'chk_Supplier
        '
        Me.chk_Supplier.AutoSize = True
        Me.chk_Supplier.Location = New System.Drawing.Point(104, 77)
        Me.chk_Supplier.Name = "chk_Supplier"
        Me.chk_Supplier.Size = New System.Drawing.Size(64, 17)
        Me.chk_Supplier.TabIndex = 30
        Me.chk_Supplier.Text = "Supplier"
        Me.chk_Supplier.UseVisualStyleBackColor = True
        '
        'chk_Customer
        '
        Me.chk_Customer.AutoSize = True
        Me.chk_Customer.Location = New System.Drawing.Point(190, 77)
        Me.chk_Customer.Name = "chk_Customer"
        Me.chk_Customer.Size = New System.Drawing.Size(70, 17)
        Me.chk_Customer.TabIndex = 33
        Me.chk_Customer.Text = "Customer"
        Me.chk_Customer.UseVisualStyleBackColor = True
        '
        'txt_NPWP
        '
        Me.txt_NPWP.Location = New System.Drawing.Point(104, 172)
        Me.txt_NPWP.Name = "txt_NPWP"
        Me.txt_NPWP.Size = New System.Drawing.Size(147, 20)
        Me.txt_NPWP.TabIndex = 50
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 175)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 9951
        Me.Label4.Text = "NPWP"
        '
        'txt_Email
        '
        Me.txt_Email.Location = New System.Drawing.Point(104, 303)
        Me.txt_Email.Name = "txt_Email"
        Me.txt_Email.Size = New System.Drawing.Size(147, 20)
        Me.txt_Email.TabIndex = 90
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 306)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 9953
        Me.Label5.Text = "Email"
        '
        'txt_PIC
        '
        Me.txt_PIC.Location = New System.Drawing.Point(104, 329)
        Me.txt_PIC.Name = "txt_PIC"
        Me.txt_PIC.Size = New System.Drawing.Size(147, 20)
        Me.txt_PIC.TabIndex = 100
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 332)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 13)
        Me.Label6.TabIndex = 9955
        Me.Label6.Text = "PIC"
        '
        'txt_RekeningBank
        '
        Me.txt_RekeningBank.Location = New System.Drawing.Point(104, 355)
        Me.txt_RekeningBank.Name = "txt_RekeningBank"
        Me.txt_RekeningBank.Size = New System.Drawing.Size(147, 20)
        Me.txt_RekeningBank.TabIndex = 110
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 358)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 9957
        Me.Label8.Text = "Rekening Bank"
        '
        'txt_AtasNama
        '
        Me.txt_AtasNama.Location = New System.Drawing.Point(104, 381)
        Me.txt_AtasNama.Name = "txt_AtasNama"
        Me.txt_AtasNama.Size = New System.Drawing.Size(147, 20)
        Me.txt_AtasNama.TabIndex = 120
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 384)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 13)
        Me.Label9.TabIndex = 9959
        Me.Label9.Text = "Atas Nama"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 201)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 13)
        Me.Label10.TabIndex = 9961
        Me.Label10.Text = "Jenis WP"
        '
        'cmb_JenisWP
        '
        Me.cmb_JenisWP.FormattingEnabled = True
        Me.cmb_JenisWP.Location = New System.Drawing.Point(104, 197)
        Me.cmb_JenisWP.Name = "cmb_JenisWP"
        Me.cmb_JenisWP.Size = New System.Drawing.Size(121, 21)
        Me.cmb_JenisWP.TabIndex = 60
        '
        'cmb_LokasiWP
        '
        Me.cmb_LokasiWP.FormattingEnabled = True
        Me.cmb_LokasiWP.Location = New System.Drawing.Point(104, 224)
        Me.cmb_LokasiWP.Name = "cmb_LokasiWP"
        Me.cmb_LokasiWP.Size = New System.Drawing.Size(121, 21)
        Me.cmb_LokasiWP.TabIndex = 70
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 228)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 13)
        Me.Label11.TabIndex = 9963
        Me.Label11.Text = "Lokasi WP"
        '
        'chk_Keuangan
        '
        Me.chk_Keuangan.AutoSize = True
        Me.chk_Keuangan.Location = New System.Drawing.Point(190, 122)
        Me.chk_Keuangan.Name = "chk_Keuangan"
        Me.chk_Keuangan.Size = New System.Drawing.Size(122, 17)
        Me.chk_Keuangan.TabIndex = 36
        Me.chk_Keuangan.Text = "Lembaga Keuangan"
        Me.chk_Keuangan.UseVisualStyleBackColor = True
        '
        'chk_Afiliasi
        '
        Me.chk_Afiliasi.AutoSize = True
        Me.chk_Afiliasi.Location = New System.Drawing.Point(104, 122)
        Me.chk_Afiliasi.Name = "chk_Afiliasi"
        Me.chk_Afiliasi.Size = New System.Drawing.Size(55, 17)
        Me.chk_Afiliasi.TabIndex = 9964
        Me.chk_Afiliasi.Text = "Afiliasi"
        Me.chk_Afiliasi.UseVisualStyleBackColor = True
        '
        'chk_PJK
        '
        Me.chk_PJK.AutoSize = True
        Me.chk_PJK.Location = New System.Drawing.Point(190, 99)
        Me.chk_PJK.Name = "chk_PJK"
        Me.chk_PJK.Size = New System.Drawing.Size(160, 17)
        Me.chk_PJK.TabIndex = 9965
        Me.chk_PJK.Text = "Perusahaan Jasa Konstruksi"
        Me.chk_PJK.UseVisualStyleBackColor = True
        '
        'chk_PKP
        '
        Me.chk_PKP.AutoSize = True
        Me.chk_PKP.Location = New System.Drawing.Point(104, 99)
        Me.chk_PKP.Name = "chk_PKP"
        Me.chk_PKP.Size = New System.Drawing.Size(47, 17)
        Me.chk_PKP.TabIndex = 9966
        Me.chk_PKP.Text = "PKP"
        Me.chk_PKP.UseVisualStyleBackColor = True
        '
        'frm_InputMitra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(365, 531)
        Me.Controls.Add(Me.chk_PKP)
        Me.Controls.Add(Me.chk_PJK)
        Me.Controls.Add(Me.chk_Afiliasi)
        Me.Controls.Add(Me.chk_Keuangan)
        Me.Controls.Add(Me.cmb_LokasiWP)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmb_JenisWP)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_AtasNama)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_RekeningBank)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_PIC)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_Email)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_NPWP)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chk_Customer)
        Me.Controls.Add(Me.chk_Supplier)
        Me.Controls.Add(Me.txt_Alamat)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_NamaMitra)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_KodeMitra)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Reset)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Tutup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputMitra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Data Mitra"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_KodeMitra As System.Windows.Forms.TextBox
    Friend WithEvents txt_NamaMitra As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_Alamat As System.Windows.Forms.TextBox
    Friend WithEvents chk_Supplier As System.Windows.Forms.CheckBox
    Friend WithEvents chk_Customer As System.Windows.Forms.CheckBox
    Friend WithEvents txt_NPWP As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_Email As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_PIC As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_RekeningBank As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_AtasNama As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmb_JenisWP As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_LokasiWP As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chk_Keuangan As CheckBox
    Friend WithEvents chk_Afiliasi As CheckBox
    Friend WithEvents chk_PJK As CheckBox
    Friend WithEvents chk_PKP As CheckBox
End Class
