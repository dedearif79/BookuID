<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_InputDisposalAssetTetap
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
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_NamaAkun_AkumulasiPenyusutan = New System.Windows.Forms.TextBox()
        Me.btn_PilihCOA_Asset = New System.Windows.Forms.Button()
        Me.grb_AkumulasiPenyusutan = New System.Windows.Forms.GroupBox()
        Me.txt_COA_AkumulasiPenyusutan = New System.Windows.Forms.TextBox()
        Me.txt_AkumulasiPenyusutan = New System.Windows.Forms.TextBox()
        Me.lbl_PerTanggal_1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lbl_PerTanggal_2 = New System.Windows.Forms.Label()
        Me.txt_HargaPerolehan = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtp_TanggalPerolehan = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.txt_NilaiSisaBuku = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_NomorBeritaAcaraDisposal = New System.Windows.Forms.TextBox()
        Me.lbl_Pokok = New System.Windows.Forms.Label()
        Me.txt_NamaAktiva = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_TanggalDisposal = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.btn_Lanjutkan = New System.Windows.Forms.Button()
        Me.txt_NomorFakturPajak = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grb_AkumulasiPenyusutan.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 10226
        Me.Label11.Text = "Kode Akun"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 13)
        Me.Label9.TabIndex = 10225
        Me.Label9.Text = "Nama Akun"
        '
        'txt_NamaAkun_AkumulasiPenyusutan
        '
        Me.txt_NamaAkun_AkumulasiPenyusutan.Location = New System.Drawing.Point(114, 71)
        Me.txt_NamaAkun_AkumulasiPenyusutan.Name = "txt_NamaAkun_AkumulasiPenyusutan"
        Me.txt_NamaAkun_AkumulasiPenyusutan.Size = New System.Drawing.Size(217, 20)
        Me.txt_NamaAkun_AkumulasiPenyusutan.TabIndex = 140
        '
        'btn_PilihCOA_Asset
        '
        Me.btn_PilihCOA_Asset.Location = New System.Drawing.Point(169, 43)
        Me.btn_PilihCOA_Asset.Name = "btn_PilihCOA_Asset"
        Me.btn_PilihCOA_Asset.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihCOA_Asset.TabIndex = 130
        Me.btn_PilihCOA_Asset.Text = "Pilih"
        Me.btn_PilihCOA_Asset.UseVisualStyleBackColor = True
        '
        'grb_AkumulasiPenyusutan
        '
        Me.grb_AkumulasiPenyusutan.Controls.Add(Me.Label11)
        Me.grb_AkumulasiPenyusutan.Controls.Add(Me.Label9)
        Me.grb_AkumulasiPenyusutan.Controls.Add(Me.txt_NamaAkun_AkumulasiPenyusutan)
        Me.grb_AkumulasiPenyusutan.Controls.Add(Me.txt_COA_AkumulasiPenyusutan)
        Me.grb_AkumulasiPenyusutan.Controls.Add(Me.btn_PilihCOA_Asset)
        Me.grb_AkumulasiPenyusutan.Controls.Add(Me.txt_AkumulasiPenyusutan)
        Me.grb_AkumulasiPenyusutan.Controls.Add(Me.lbl_PerTanggal_1)
        Me.grb_AkumulasiPenyusutan.Controls.Add(Me.Label8)
        Me.grb_AkumulasiPenyusutan.Location = New System.Drawing.Point(24, 157)
        Me.grb_AkumulasiPenyusutan.Name = "grb_AkumulasiPenyusutan"
        Me.grb_AkumulasiPenyusutan.Size = New System.Drawing.Size(342, 104)
        Me.grb_AkumulasiPenyusutan.TabIndex = 100
        Me.grb_AkumulasiPenyusutan.TabStop = False
        Me.grb_AkumulasiPenyusutan.Text = "Akumulasi Penyusutan :"
        '
        'txt_COA_AkumulasiPenyusutan
        '
        Me.txt_COA_AkumulasiPenyusutan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_COA_AkumulasiPenyusutan.Location = New System.Drawing.Point(114, 45)
        Me.txt_COA_AkumulasiPenyusutan.MaxLength = 9
        Me.txt_COA_AkumulasiPenyusutan.Name = "txt_COA_AkumulasiPenyusutan"
        Me.txt_COA_AkumulasiPenyusutan.Size = New System.Drawing.Size(49, 20)
        Me.txt_COA_AkumulasiPenyusutan.TabIndex = 120
        '
        'txt_AkumulasiPenyusutan
        '
        Me.txt_AkumulasiPenyusutan.Location = New System.Drawing.Point(114, 19)
        Me.txt_AkumulasiPenyusutan.Name = "txt_AkumulasiPenyusutan"
        Me.txt_AkumulasiPenyusutan.Size = New System.Drawing.Size(95, 20)
        Me.txt_AkumulasiPenyusutan.TabIndex = 110
        Me.txt_AkumulasiPenyusutan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_PerTanggal_1
        '
        Me.lbl_PerTanggal_1.AutoSize = True
        Me.lbl_PerTanggal_1.Location = New System.Drawing.Point(215, 22)
        Me.lbl_PerTanggal_1.Name = "lbl_PerTanggal_1"
        Me.lbl_PerTanggal_1.Size = New System.Drawing.Size(49, 13)
        Me.lbl_PerTanggal_1.TabIndex = 10124
        Me.lbl_PerTanggal_1.Text = "( per .... )"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 13)
        Me.Label8.TabIndex = 10221
        Me.Label8.Text = "Jumlah"
        '
        'lbl_PerTanggal_2
        '
        Me.lbl_PerTanggal_2.AutoSize = True
        Me.lbl_PerTanggal_2.Location = New System.Drawing.Point(239, 277)
        Me.lbl_PerTanggal_2.Name = "lbl_PerTanggal_2"
        Me.lbl_PerTanggal_2.Size = New System.Drawing.Size(49, 13)
        Me.lbl_PerTanggal_2.TabIndex = 10246
        Me.lbl_PerTanggal_2.Text = "( per .... )"
        '
        'txt_HargaPerolehan
        '
        Me.txt_HargaPerolehan.Location = New System.Drawing.Point(138, 126)
        Me.txt_HargaPerolehan.Name = "txt_HargaPerolehan"
        Me.txt_HargaPerolehan.Size = New System.Drawing.Size(95, 20)
        Me.txt_HargaPerolehan.TabIndex = 60
        Me.txt_HargaPerolehan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(33, 129)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 13)
        Me.Label7.TabIndex = 10245
        Me.Label7.Text = "Harga Perolehan"
        '
        'dtp_TanggalPerolehan
        '
        Me.dtp_TanggalPerolehan.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalPerolehan.Enabled = False
        Me.dtp_TanggalPerolehan.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalPerolehan.Location = New System.Drawing.Point(138, 100)
        Me.dtp_TanggalPerolehan.Name = "dtp_TanggalPerolehan"
        Me.dtp_TanggalPerolehan.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalPerolehan.TabIndex = 50
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(33, 103)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 13)
        Me.Label6.TabIndex = 10244
        Me.Label6.Text = "Tanggal Perolehan"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(33, 338)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 10243
        Me.Label5.Text = "Keterangan"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txt_Keterangan.Location = New System.Drawing.Point(36, 357)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(319, 84)
        Me.txt_Keterangan.TabIndex = 900
        Me.txt_Keterangan.Text = ""
        '
        'txt_NilaiSisaBuku
        '
        Me.txt_NilaiSisaBuku.Location = New System.Drawing.Point(138, 274)
        Me.txt_NilaiSisaBuku.Name = "txt_NilaiSisaBuku"
        Me.txt_NilaiSisaBuku.Size = New System.Drawing.Size(95, 20)
        Me.txt_NilaiSisaBuku.TabIndex = 300
        Me.txt_NilaiSisaBuku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 277)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 10241
        Me.Label2.Text = "Nilai Sisa Buku"
        '
        'txt_NomorBeritaAcaraDisposal
        '
        Me.txt_NomorBeritaAcaraDisposal.Location = New System.Drawing.Point(138, 48)
        Me.txt_NomorBeritaAcaraDisposal.MaxLength = 99
        Me.txt_NomorBeritaAcaraDisposal.Name = "txt_NomorBeritaAcaraDisposal"
        Me.txt_NomorBeritaAcaraDisposal.Size = New System.Drawing.Size(217, 20)
        Me.txt_NomorBeritaAcaraDisposal.TabIndex = 20
        '
        'lbl_Pokok
        '
        Me.lbl_Pokok.AutoSize = True
        Me.lbl_Pokok.Location = New System.Drawing.Point(33, 51)
        Me.lbl_Pokok.Name = "lbl_Pokok"
        Me.lbl_Pokok.Size = New System.Drawing.Size(99, 13)
        Me.lbl_Pokok.TabIndex = 10239
        Me.lbl_Pokok.Text = "Nomor Berita Acara"
        '
        'txt_NamaAktiva
        '
        Me.txt_NamaAktiva.Location = New System.Drawing.Point(138, 74)
        Me.txt_NamaAktiva.MaxLength = 99
        Me.txt_NamaAktiva.Name = "txt_NamaAktiva"
        Me.txt_NamaAktiva.Size = New System.Drawing.Size(217, 20)
        Me.txt_NamaAktiva.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 10238
        Me.Label1.Text = "Nama Aktiva"
        '
        'dtp_TanggalDisposal
        '
        Me.dtp_TanggalDisposal.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalDisposal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalDisposal.Location = New System.Drawing.Point(138, 22)
        Me.dtp_TanggalDisposal.Name = "dtp_TanggalDisposal"
        Me.dtp_TanggalDisposal.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalDisposal.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(33, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 10236
        Me.Label4.Text = "Tanggal Disposal"
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.Location = New System.Drawing.Point(117, 459)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'btn_Lanjutkan
        '
        Me.btn_Lanjutkan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Lanjutkan.Location = New System.Drawing.Point(206, 459)
        Me.btn_Lanjutkan.Name = "btn_Lanjutkan"
        Me.btn_Lanjutkan.Size = New System.Drawing.Size(149, 35)
        Me.btn_Lanjutkan.TabIndex = 9000
        Me.btn_Lanjutkan.Text = "Lanjutkan >>"
        Me.btn_Lanjutkan.UseVisualStyleBackColor = True
        '
        'txt_NomorFakturPajak
        '
        Me.txt_NomorFakturPajak.Location = New System.Drawing.Point(138, 300)
        Me.txt_NomorFakturPajak.MaxLength = 99
        Me.txt_NomorFakturPajak.Name = "txt_NomorFakturPajak"
        Me.txt_NomorFakturPajak.Size = New System.Drawing.Size(217, 20)
        Me.txt_NomorFakturPajak.TabIndex = 10247
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 303)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 13)
        Me.Label3.TabIndex = 10248
        Me.Label3.Text = "Nomor Faktur Pajak"
        '
        'frm_InputDisposalAssetTetap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 516)
        Me.Controls.Add(Me.txt_NomorFakturPajak)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.grb_AkumulasiPenyusutan)
        Me.Controls.Add(Me.lbl_PerTanggal_2)
        Me.Controls.Add(Me.txt_HargaPerolehan)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtp_TanggalPerolehan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_NilaiSisaBuku)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_NomorBeritaAcaraDisposal)
        Me.Controls.Add(Me.lbl_Pokok)
        Me.Controls.Add(Me.txt_NamaAktiva)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtp_TanggalDisposal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.btn_Lanjutkan)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputDisposalAssetTetap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Disposal Asset Tetap"
        Me.grb_AkumulasiPenyusutan.ResumeLayout(False)
        Me.grb_AkumulasiPenyusutan.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_NamaAkun_AkumulasiPenyusutan As TextBox
    Friend WithEvents btn_PilihCOA_Asset As Button
    Friend WithEvents grb_AkumulasiPenyusutan As GroupBox
    Friend WithEvents txt_COA_AkumulasiPenyusutan As TextBox
    Friend WithEvents txt_AkumulasiPenyusutan As TextBox
    Friend WithEvents lbl_PerTanggal_1 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lbl_PerTanggal_2 As Label
    Friend WithEvents txt_HargaPerolehan As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents dtp_TanggalPerolehan As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents txt_NilaiSisaBuku As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NomorBeritaAcaraDisposal As TextBox
    Friend WithEvents lbl_Pokok As Label
    Friend WithEvents txt_NamaAktiva As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtp_TanggalDisposal As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_Batal As Button
    Friend WithEvents btn_Lanjutkan As Button
    Friend WithEvents txt_NomorFakturPajak As TextBox
    Friend WithEvents Label3 As Label
End Class
