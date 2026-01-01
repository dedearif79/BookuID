<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputHutangPPhPasal42
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
        Me.txt_DPP = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.txt_KodeSupplier = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txt_NamaSupplier = New System.Windows.Forms.TextBox()
        Me.txt_NPWP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_NamaJasa = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_NomorFakturPajak = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtp_TanggalInvoice = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtp_TanggalTransaksi = New System.Windows.Forms.DateTimePicker()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.txt_PPhPasal42 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_NomorInvoice = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txt_DPP
        '
        Me.txt_DPP.Location = New System.Drawing.Point(158, 222)
        Me.txt_DPP.Name = "txt_DPP"
        Me.txt_DPP.Size = New System.Drawing.Size(98, 20)
        Me.txt_DPP.TabIndex = 10113
        Me.txt_DPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(22, 224)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 13)
        Me.Label10.TabIndex = 10128
        Me.Label10.Text = "DPP"
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(245, 145)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 10110
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'txt_KodeSupplier
        '
        Me.txt_KodeSupplier.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_KodeSupplier.Location = New System.Drawing.Point(158, 146)
        Me.txt_KodeSupplier.MaxLength = 3
        Me.txt_KodeSupplier.Name = "txt_KodeSupplier"
        Me.txt_KodeSupplier.Size = New System.Drawing.Size(81, 20)
        Me.txt_KodeSupplier.TabIndex = 10109
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(22, 149)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(73, 13)
        Me.Label24.TabIndex = 10125
        Me.Label24.Text = "Kode Supplier"
        '
        'txt_NamaSupplier
        '
        Me.txt_NamaSupplier.Location = New System.Drawing.Point(158, 171)
        Me.txt_NamaSupplier.Name = "txt_NamaSupplier"
        Me.txt_NamaSupplier.Size = New System.Drawing.Size(163, 20)
        Me.txt_NamaSupplier.TabIndex = 10111
        '
        'txt_NPWP
        '
        Me.txt_NPWP.Location = New System.Drawing.Point(158, 197)
        Me.txt_NPWP.Name = "txt_NPWP"
        Me.txt_NPWP.Size = New System.Drawing.Size(163, 20)
        Me.txt_NPWP.TabIndex = 10112
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 176)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 10126
        Me.Label2.Text = "Nama Supplier"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(22, 200)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 13)
        Me.Label9.TabIndex = 10127
        Me.Label9.Text = "NPWP"
        '
        'txt_NamaJasa
        '
        Me.txt_NamaJasa.Location = New System.Drawing.Point(158, 122)
        Me.txt_NamaJasa.MaxLength = 99
        Me.txt_NamaJasa.Name = "txt_NamaJasa"
        Me.txt_NamaJasa.Size = New System.Drawing.Size(163, 20)
        Me.txt_NamaJasa.TabIndex = 10108
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(22, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(92, 13)
        Me.Label8.TabIndex = 10124
        Me.Label8.Text = "Nama Jasa/Sewa"
        '
        'txt_NomorFakturPajak
        '
        Me.txt_NomorFakturPajak.Location = New System.Drawing.Point(158, 98)
        Me.txt_NomorFakturPajak.MaxLength = 99
        Me.txt_NomorFakturPajak.Name = "txt_NomorFakturPajak"
        Me.txt_NomorFakturPajak.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorFakturPajak.TabIndex = 10107
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(22, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 13)
        Me.Label7.TabIndex = 10123
        Me.Label7.Text = "Nomor Faktur Pajak"
        '
        'dtp_TanggalInvoice
        '
        Me.dtp_TanggalInvoice.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalInvoice.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalInvoice.Location = New System.Drawing.Point(158, 49)
        Me.dtp_TanggalInvoice.Name = "dtp_TanggalInvoice"
        Me.dtp_TanggalInvoice.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalInvoice.TabIndex = 10105
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(22, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 10122
        Me.Label5.Text = "Tanggal Invoice"
        '
        'dtp_TanggalTransaksi
        '
        Me.dtp_TanggalTransaksi.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalTransaksi.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalTransaksi.Location = New System.Drawing.Point(158, 24)
        Me.dtp_TanggalTransaksi.Name = "dtp_TanggalTransaksi"
        Me.dtp_TanggalTransaksi.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalTransaksi.TabIndex = 10104
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(22, 28)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(95, 13)
        Me.Label50.TabIndex = 10121
        Me.Label50.Text = "Tanggal Transaksi"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 273)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 10120
        Me.Label3.Text = "Keterangan"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(22, 292)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(299, 92)
        Me.txt_Keterangan.TabIndex = 10115
        Me.txt_Keterangan.Text = ""
        '
        'txt_PPhPasal42
        '
        Me.txt_PPhPasal42.Location = New System.Drawing.Point(158, 246)
        Me.txt_PPhPasal42.Name = "txt_PPhPasal42"
        Me.txt_PPhPasal42.Size = New System.Drawing.Size(98, 20)
        Me.txt_PPhPasal42.TabIndex = 10114
        Me.txt_PPhPasal42.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(22, 249)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 13)
        Me.Label6.TabIndex = 10119
        Me.Label6.Text = "PPh Pasal 4 (2)"
        '
        'txt_NomorInvoice
        '
        Me.txt_NomorInvoice.Location = New System.Drawing.Point(158, 73)
        Me.txt_NomorInvoice.MaxLength = 99
        Me.txt_NomorInvoice.Name = "txt_NomorInvoice"
        Me.txt_NomorInvoice.Size = New System.Drawing.Size(163, 20)
        Me.txt_NomorInvoice.TabIndex = 10106
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 10118
        Me.Label1.Text = "Nomor Invoice"
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(237, 403)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 10116
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(148, 403)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10117
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'frm_InputHutangPPhPasal42
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 458)
        Me.Controls.Add(Me.txt_DPP)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_KodeSupplier)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txt_NamaSupplier)
        Me.Controls.Add(Me.txt_NPWP)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_NamaJasa)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_NomorFakturPajak)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtp_TanggalInvoice)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtp_TanggalTransaksi)
        Me.Controls.Add(Me.Label50)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_PPhPasal42)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_NomorInvoice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputHutangPPhPasal42"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Hutang PPh Pasal 4 (2)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_DPP As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents txt_KodeSupplier As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_NamaSupplier As TextBox
    Friend WithEvents txt_NPWP As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_NamaJasa As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_NomorFakturPajak As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents dtp_TanggalInvoice As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents dtp_TanggalTransaksi As DateTimePicker
    Friend WithEvents Label50 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents txt_PPhPasal42 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_NomorInvoice As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
End Class
