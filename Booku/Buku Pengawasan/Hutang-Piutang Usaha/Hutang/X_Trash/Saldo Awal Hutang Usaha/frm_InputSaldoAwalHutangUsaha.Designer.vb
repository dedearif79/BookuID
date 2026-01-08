<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputSaldoAwalHutangUsaha
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_NomorPembelian = New System.Windows.Forms.TextBox()
        Me.txt_Referensi = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.txt_NomorInvoice = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtp_TanggalInvoice = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtp_TanggalFakturPajak = New System.Windows.Forms.DateTimePicker()
        Me.txt_NomorFakturPajak = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_KodeSupplier = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_NamaSupplier = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_NamaBarang = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_NamaJasa = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txt_DPP = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_PPN = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_JumlahTagihan = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_PPhDipotong = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dtp_DueDate = New System.Windows.Forms.DateTimePicker()
        Me.txt_JumlahBayar = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txt_SisaHutang = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_Reset
        '
        Me.btn_Reset.Location = New System.Drawing.Point(361, 298)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(83, 35)
        Me.btn_Reset.TabIndex = 9900
        Me.btn_Reset.Text = "Reset"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(450, 298)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9910
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Location = New System.Drawing.Point(539, 298)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tutup.TabIndex = 9999
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 9934
        Me.Label1.Text = "Nomor Pembelian"
        '
        'txt_NomorPembelian
        '
        Me.txt_NomorPembelian.Location = New System.Drawing.Point(142, 19)
        Me.txt_NomorPembelian.Name = "txt_NomorPembelian"
        Me.txt_NomorPembelian.Size = New System.Drawing.Size(172, 20)
        Me.txt_NomorPembelian.TabIndex = 10
        '
        'txt_Referensi
        '
        Me.txt_Referensi.Location = New System.Drawing.Point(142, 45)
        Me.txt_Referensi.Name = "txt_Referensi"
        Me.txt_Referensi.Size = New System.Drawing.Size(172, 20)
        Me.txt_Referensi.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 13)
        Me.Label2.TabIndex = 9936
        Me.Label2.Text = "Referensi/BPHU"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(383, 204)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 9947
        Me.Label7.Text = "Keterangan"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(471, 201)
        Me.txt_Keterangan.Multiline = True
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(151, 57)
        Me.txt_Keterangan.TabIndex = 180
        '
        'txt_NomorInvoice
        '
        Me.txt_NomorInvoice.Location = New System.Drawing.Point(142, 97)
        Me.txt_NomorInvoice.Name = "txt_NomorInvoice"
        Me.txt_NomorInvoice.Size = New System.Drawing.Size(172, 20)
        Me.txt_NomorInvoice.TabIndex = 40
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 9949
        Me.Label3.Text = "Nomor Invoice"
        '
        'dtp_TanggalInvoice
        '
        Me.dtp_TanggalInvoice.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalInvoice.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalInvoice.Location = New System.Drawing.Point(142, 71)
        Me.dtp_TanggalInvoice.Name = "dtp_TanggalInvoice"
        Me.dtp_TanggalInvoice.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalInvoice.TabIndex = 30
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 9952
        Me.Label4.Text = "Tanggal Invoice"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 13)
        Me.Label5.TabIndex = 9956
        Me.Label5.Text = "Tanggal Faktuf Pajak"
        '
        'dtp_TanggalFakturPajak
        '
        Me.dtp_TanggalFakturPajak.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalFakturPajak.Enabled = False
        Me.dtp_TanggalFakturPajak.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalFakturPajak.Location = New System.Drawing.Point(142, 123)
        Me.dtp_TanggalFakturPajak.Name = "dtp_TanggalFakturPajak"
        Me.dtp_TanggalFakturPajak.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalFakturPajak.TabIndex = 50
        '
        'txt_NomorFakturPajak
        '
        Me.txt_NomorFakturPajak.Location = New System.Drawing.Point(142, 149)
        Me.txt_NomorFakturPajak.Name = "txt_NomorFakturPajak"
        Me.txt_NomorFakturPajak.Size = New System.Drawing.Size(172, 20)
        Me.txt_NomorFakturPajak.TabIndex = 60
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 13)
        Me.Label6.TabIndex = 9953
        Me.Label6.Text = "Nomor Faktur Pajak"
        '
        'txt_KodeSupplier
        '
        Me.txt_KodeSupplier.Location = New System.Drawing.Point(142, 175)
        Me.txt_KodeSupplier.Name = "txt_KodeSupplier"
        Me.txt_KodeSupplier.Size = New System.Drawing.Size(105, 20)
        Me.txt_KodeSupplier.TabIndex = 70
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 178)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 13)
        Me.Label8.TabIndex = 9957
        Me.Label8.Text = "Kode Supplier"
        '
        'txt_NamaSupplier
        '
        Me.txt_NamaSupplier.Enabled = False
        Me.txt_NamaSupplier.Location = New System.Drawing.Point(142, 201)
        Me.txt_NamaSupplier.Name = "txt_NamaSupplier"
        Me.txt_NamaSupplier.Size = New System.Drawing.Size(172, 20)
        Me.txt_NamaSupplier.TabIndex = 80
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 204)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 13)
        Me.Label9.TabIndex = 9959
        Me.Label9.Text = "Nama Supplier"
        '
        'txt_NamaBarang
        '
        Me.txt_NamaBarang.Location = New System.Drawing.Point(142, 227)
        Me.txt_NamaBarang.Multiline = True
        Me.txt_NamaBarang.Name = "txt_NamaBarang"
        Me.txt_NamaBarang.Size = New System.Drawing.Size(172, 50)
        Me.txt_NamaBarang.TabIndex = 90
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 230)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 13)
        Me.Label10.TabIndex = 9961
        Me.Label10.Text = "Nama Barang"
        '
        'txt_NamaJasa
        '
        Me.txt_NamaJasa.Location = New System.Drawing.Point(142, 283)
        Me.txt_NamaJasa.Multiline = True
        Me.txt_NamaJasa.Name = "txt_NamaJasa"
        Me.txt_NamaJasa.Size = New System.Drawing.Size(172, 50)
        Me.txt_NamaJasa.TabIndex = 100
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 286)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 9963
        Me.Label11.Text = "Nama Jasa"
        '
        'txt_DPP
        '
        Me.txt_DPP.Location = New System.Drawing.Point(471, 19)
        Me.txt_DPP.Name = "txt_DPP"
        Me.txt_DPP.Size = New System.Drawing.Size(151, 20)
        Me.txt_DPP.TabIndex = 110
        Me.txt_DPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(383, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 13)
        Me.Label12.TabIndex = 9965
        Me.Label12.Text = "DPP"
        '
        'txt_PPN
        '
        Me.txt_PPN.Enabled = False
        Me.txt_PPN.Location = New System.Drawing.Point(471, 45)
        Me.txt_PPN.Name = "txt_PPN"
        Me.txt_PPN.Size = New System.Drawing.Size(151, 20)
        Me.txt_PPN.TabIndex = 120
        Me.txt_PPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(383, 48)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(29, 13)
        Me.Label13.TabIndex = 9967
        Me.Label13.Text = "PPN"
        '
        'txt_JumlahTagihan
        '
        Me.txt_JumlahTagihan.Enabled = False
        Me.txt_JumlahTagihan.Location = New System.Drawing.Point(471, 97)
        Me.txt_JumlahTagihan.Name = "txt_JumlahTagihan"
        Me.txt_JumlahTagihan.Size = New System.Drawing.Size(151, 20)
        Me.txt_JumlahTagihan.TabIndex = 140
        Me.txt_JumlahTagihan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(383, 100)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 9971
        Me.Label14.Text = "Jumlah Tagihan"
        '
        'txt_PPhDipotong
        '
        Me.txt_PPhDipotong.Location = New System.Drawing.Point(471, 71)
        Me.txt_PPhDipotong.Name = "txt_PPhDipotong"
        Me.txt_PPhDipotong.Size = New System.Drawing.Size(151, 20)
        Me.txt_PPhDipotong.TabIndex = 130
        Me.txt_PPhDipotong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(383, 74)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(73, 13)
        Me.Label15.TabIndex = 9969
        Me.Label15.Text = "PPh Dipotong"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(383, 178)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 13)
        Me.Label16.TabIndex = 9974
        Me.Label16.Text = "Due Date"
        '
        'dtp_DueDate
        '
        Me.dtp_DueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtp_DueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_DueDate.Location = New System.Drawing.Point(471, 175)
        Me.dtp_DueDate.Name = "dtp_DueDate"
        Me.dtp_DueDate.Size = New System.Drawing.Size(95, 20)
        Me.dtp_DueDate.TabIndex = 170
        '
        'txt_JumlahBayar
        '
        Me.txt_JumlahBayar.Location = New System.Drawing.Point(471, 123)
        Me.txt_JumlahBayar.Name = "txt_JumlahBayar"
        Me.txt_JumlahBayar.Size = New System.Drawing.Size(151, 20)
        Me.txt_JumlahBayar.TabIndex = 150
        Me.txt_JumlahBayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(383, 126)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(70, 13)
        Me.Label18.TabIndex = 9977
        Me.Label18.Text = "Jumlah Bayar"
        '
        'txt_SisaHutang
        '
        Me.txt_SisaHutang.Enabled = False
        Me.txt_SisaHutang.Location = New System.Drawing.Point(471, 149)
        Me.txt_SisaHutang.Name = "txt_SisaHutang"
        Me.txt_SisaHutang.Size = New System.Drawing.Size(151, 20)
        Me.txt_SisaHutang.TabIndex = 160
        Me.txt_SisaHutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(383, 152)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(65, 13)
        Me.Label19.TabIndex = 9979
        Me.Label19.Text = "Sisa Hutang"
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(253, 173)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 10000
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'frm_InputSaldoAwalHutangUsaha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 350)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_SisaHutang)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txt_JumlahBayar)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.dtp_DueDate)
        Me.Controls.Add(Me.txt_JumlahTagihan)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txt_PPhDipotong)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txt_PPN)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txt_DPP)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txt_NamaJasa)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt_NamaBarang)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_NamaSupplier)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txt_KodeSupplier)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtp_TanggalFakturPajak)
        Me.Controls.Add(Me.txt_NomorFakturPajak)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtp_TanggalInvoice)
        Me.Controls.Add(Me.txt_NomorInvoice)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_Referensi)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_NomorPembelian)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Reset)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Tutup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputSaldoAwalHutangUsaha"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Saldo Hutang Usaha"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_NomorPembelian As System.Windows.Forms.TextBox
    Friend WithEvents txt_Referensi As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_Keterangan As System.Windows.Forms.TextBox
    Friend WithEvents txt_NomorInvoice As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtp_TanggalInvoice As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtp_TanggalFakturPajak As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_NomorFakturPajak As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_KodeSupplier As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_NamaSupplier As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_NamaBarang As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_NamaJasa As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txt_DPP As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txt_PPN As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahTagihan As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txt_PPhDipotong As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dtp_DueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_JumlahBayar As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txt_SisaHutang As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btn_PilihMitra As System.Windows.Forms.Button
End Class
