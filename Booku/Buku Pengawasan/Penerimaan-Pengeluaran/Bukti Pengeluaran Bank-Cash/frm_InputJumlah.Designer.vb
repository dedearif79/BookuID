<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputJumlah
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
        btn_OK = New Button()
        btn_Batal = New Button()
        txt_Jumlah = New TextBox()
        txt_DPP = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        txt_PPN = New TextBox()
        lbl_PPhTerutang = New Label()
        txt_PPhTerutang = New TextBox()
        Label4 = New Label()
        txt_PPhDitanggung = New TextBox()
        Label5 = New Label()
        txt_PPhDipotong = New TextBox()
        Label6 = New Label()
        txt_BiayaTransportasi = New TextBox()
        Label7 = New Label()
        txt_BiayaMaterai = New TextBox()
        Label8 = New Label()
        txt_Retur = New TextBox()
        Label9 = New Label()
        txt_JumlahTagihan = New TextBox()
        Label10 = New Label()
        txt_SudahDibayar = New TextBox()
        Label11 = New Label()
        txt_SisaTagihan = New TextBox()
        lbl_Jumlah = New Label()
        pnl_DPP = New Panel()
        pnl_PPN = New Panel()
        pnl_PPhTerutang = New Panel()
        pnl_PPhDitanggung = New Panel()
        pnl_PPhDipotong = New Panel()
        pnl_BiayaTransportasi = New Panel()
        pnl_BiayaMaterai = New Panel()
        pnl_Retur = New Panel()
        pnl_JumlahTagihan = New Panel()
        pnl_SudahDibayar = New Panel()
        pnl_SisaTagihan = New Panel()
        pnl_JumlahInputan = New Panel()
        pnl_OK = New Panel()
        pnl_Atas = New Panel()
        pnl_DPP.SuspendLayout()
        pnl_PPN.SuspendLayout()
        pnl_PPhTerutang.SuspendLayout()
        pnl_PPhDitanggung.SuspendLayout()
        pnl_PPhDipotong.SuspendLayout()
        pnl_BiayaTransportasi.SuspendLayout()
        pnl_BiayaMaterai.SuspendLayout()
        pnl_Retur.SuspendLayout()
        pnl_JumlahTagihan.SuspendLayout()
        pnl_SudahDibayar.SuspendLayout()
        pnl_SisaTagihan.SuspendLayout()
        pnl_JumlahInputan.SuspendLayout()
        pnl_OK.SuspendLayout()
        SuspendLayout()
        ' 
        ' btn_OK
        ' 
        btn_OK.DialogResult = DialogResult.OK
        btn_OK.Location = New Point(205, 33)
        btn_OK.Margin = New Padding(4, 3, 4, 3)
        btn_OK.Name = "btn_OK"
        btn_OK.Size = New Size(97, 40)
        btn_OK.TabIndex = 10326
        btn_OK.Text = "OK"
        btn_OK.UseVisualStyleBackColor = True
        ' 
        ' btn_Batal
        ' 
        btn_Batal.DialogResult = DialogResult.Cancel
        btn_Batal.Location = New Point(102, 33)
        btn_Batal.Margin = New Padding(4, 3, 4, 3)
        btn_Batal.Name = "btn_Batal"
        btn_Batal.Size = New Size(97, 40)
        btn_Batal.TabIndex = 10327
        btn_Batal.Text = "Batal"
        btn_Batal.UseVisualStyleBackColor = True
        ' 
        ' txt_Jumlah
        ' 
        txt_Jumlah.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Jumlah.Location = New Point(166, 3)
        txt_Jumlah.Margin = New Padding(4, 3, 4, 3)
        txt_Jumlah.Name = "txt_Jumlah"
        txt_Jumlah.Size = New Size(136, 20)
        txt_Jumlah.TabIndex = 10328
        txt_Jumlah.TextAlign = HorizontalAlignment.Right
        ' 
        ' txt_DPP
        ' 
        txt_DPP.Enabled = False
        txt_DPP.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_DPP.Location = New Point(166, 3)
        txt_DPP.Margin = New Padding(4, 3, 4, 3)
        txt_DPP.Name = "txt_DPP"
        txt_DPP.Size = New Size(136, 20)
        txt_DPP.TabIndex = 10329
        txt_DPP.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(30, 7)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(29, 15)
        Label1.TabIndex = 10330
        Label1.Text = "DPP"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(30, 7)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(30, 15)
        Label2.TabIndex = 10332
        Label2.Text = "PPN"
        ' 
        ' txt_PPN
        ' 
        txt_PPN.Enabled = False
        txt_PPN.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_PPN.Location = New Point(166, 3)
        txt_PPN.Margin = New Padding(4, 3, 4, 3)
        txt_PPN.Name = "txt_PPN"
        txt_PPN.Size = New Size(136, 20)
        txt_PPN.TabIndex = 10331
        txt_PPN.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_PPhTerutang
        ' 
        lbl_PPhTerutang.AutoSize = True
        lbl_PPhTerutang.Location = New Point(30, 7)
        lbl_PPhTerutang.Margin = New Padding(4, 0, 4, 0)
        lbl_PPhTerutang.Name = "lbl_PPhTerutang"
        lbl_PPhTerutang.Size = New Size(78, 15)
        lbl_PPhTerutang.TabIndex = 10334
        lbl_PPhTerutang.Text = "PPh Terutang"
        ' 
        ' txt_PPhTerutang
        ' 
        txt_PPhTerutang.Enabled = False
        txt_PPhTerutang.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_PPhTerutang.Location = New Point(166, 3)
        txt_PPhTerutang.Margin = New Padding(4, 3, 4, 3)
        txt_PPhTerutang.Name = "txt_PPhTerutang"
        txt_PPhTerutang.Size = New Size(136, 20)
        txt_PPhTerutang.TabIndex = 10333
        txt_PPhTerutang.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(30, 7)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(94, 15)
        Label4.TabIndex = 10336
        Label4.Text = "PPh Ditanggung"
        ' 
        ' txt_PPhDitanggung
        ' 
        txt_PPhDitanggung.Enabled = False
        txt_PPhDitanggung.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_PPhDitanggung.Location = New Point(166, 3)
        txt_PPhDitanggung.Margin = New Padding(4, 3, 4, 3)
        txt_PPhDitanggung.Name = "txt_PPhDitanggung"
        txt_PPhDitanggung.Size = New Size(136, 20)
        txt_PPhDitanggung.TabIndex = 10335
        txt_PPhDitanggung.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(30, 7)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(81, 15)
        Label5.TabIndex = 10338
        Label5.Text = "PPh Dipotong"
        ' 
        ' txt_PPhDipotong
        ' 
        txt_PPhDipotong.Enabled = False
        txt_PPhDipotong.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_PPhDipotong.Location = New Point(166, 3)
        txt_PPhDipotong.Margin = New Padding(4, 3, 4, 3)
        txt_PPhDipotong.Name = "txt_PPhDipotong"
        txt_PPhDipotong.Size = New Size(136, 20)
        txt_PPhDipotong.TabIndex = 10337
        txt_PPhDipotong.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(30, 7)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(102, 15)
        Label6.TabIndex = 10340
        Label6.Text = "Biaya Transportasi"
        ' 
        ' txt_BiayaTransportasi
        ' 
        txt_BiayaTransportasi.Enabled = False
        txt_BiayaTransportasi.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_BiayaTransportasi.Location = New Point(166, 3)
        txt_BiayaTransportasi.Margin = New Padding(4, 3, 4, 3)
        txt_BiayaTransportasi.Name = "txt_BiayaTransportasi"
        txt_BiayaTransportasi.Size = New Size(136, 20)
        txt_BiayaTransportasi.TabIndex = 10339
        txt_BiayaTransportasi.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(30, 7)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(78, 15)
        Label7.TabIndex = 10342
        Label7.Text = "Biaya Materai"
        ' 
        ' txt_BiayaMaterai
        ' 
        txt_BiayaMaterai.Enabled = False
        txt_BiayaMaterai.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_BiayaMaterai.Location = New Point(166, 3)
        txt_BiayaMaterai.Margin = New Padding(4, 3, 4, 3)
        txt_BiayaMaterai.Name = "txt_BiayaMaterai"
        txt_BiayaMaterai.Size = New Size(136, 20)
        txt_BiayaMaterai.TabIndex = 10341
        txt_BiayaMaterai.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(30, 7)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(35, 15)
        Label8.TabIndex = 10344
        Label8.Text = "Retur"
        ' 
        ' txt_Retur
        ' 
        txt_Retur.Enabled = False
        txt_Retur.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_Retur.Location = New Point(166, 3)
        txt_Retur.Margin = New Padding(4, 3, 4, 3)
        txt_Retur.Name = "txt_Retur"
        txt_Retur.Size = New Size(136, 20)
        txt_Retur.TabIndex = 10343
        txt_Retur.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(30, 7)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(90, 15)
        Label9.TabIndex = 10346
        Label9.Text = "Jumlah Tagihan"
        ' 
        ' txt_JumlahTagihan
        ' 
        txt_JumlahTagihan.Enabled = False
        txt_JumlahTagihan.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_JumlahTagihan.Location = New Point(166, 3)
        txt_JumlahTagihan.Margin = New Padding(4, 3, 4, 3)
        txt_JumlahTagihan.Name = "txt_JumlahTagihan"
        txt_JumlahTagihan.Size = New Size(136, 20)
        txt_JumlahTagihan.TabIndex = 10345
        txt_JumlahTagihan.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(30, 7)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(83, 15)
        Label10.TabIndex = 10348
        Label10.Text = "Sudah Dibayar"
        ' 
        ' txt_SudahDibayar
        ' 
        txt_SudahDibayar.Enabled = False
        txt_SudahDibayar.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_SudahDibayar.Location = New Point(166, 3)
        txt_SudahDibayar.Margin = New Padding(4, 3, 4, 3)
        txt_SudahDibayar.Name = "txt_SudahDibayar"
        txt_SudahDibayar.Size = New Size(136, 20)
        txt_SudahDibayar.TabIndex = 10347
        txt_SudahDibayar.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(30, 7)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(72, 15)
        Label11.TabIndex = 10350
        Label11.Text = "Sisa Tagihan"
        ' 
        ' txt_SisaTagihan
        ' 
        txt_SisaTagihan.Enabled = False
        txt_SisaTagihan.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_SisaTagihan.Location = New Point(166, 3)
        txt_SisaTagihan.Margin = New Padding(4, 3, 4, 3)
        txt_SisaTagihan.Name = "txt_SisaTagihan"
        txt_SisaTagihan.Size = New Size(136, 20)
        txt_SisaTagihan.TabIndex = 10349
        txt_SisaTagihan.TextAlign = HorizontalAlignment.Right
        ' 
        ' lbl_Jumlah
        ' 
        lbl_Jumlah.AutoSize = True
        lbl_Jumlah.Location = New Point(30, 7)
        lbl_Jumlah.Margin = New Padding(4, 0, 4, 0)
        lbl_Jumlah.Name = "lbl_Jumlah"
        lbl_Jumlah.Size = New Size(45, 15)
        lbl_Jumlah.TabIndex = 10351
        lbl_Jumlah.Text = "Jumlah"
        ' 
        ' pnl_DPP
        ' 
        pnl_DPP.Controls.Add(txt_DPP)
        pnl_DPP.Controls.Add(Label1)
        pnl_DPP.Dock = DockStyle.Top
        pnl_DPP.Location = New Point(0, 30)
        pnl_DPP.Margin = New Padding(4, 3, 4, 3)
        pnl_DPP.Name = "pnl_DPP"
        pnl_DPP.Size = New Size(337, 30)
        pnl_DPP.TabIndex = 10352
        ' 
        ' pnl_PPN
        ' 
        pnl_PPN.Controls.Add(txt_PPN)
        pnl_PPN.Controls.Add(Label2)
        pnl_PPN.Dock = DockStyle.Top
        pnl_PPN.Location = New Point(0, 60)
        pnl_PPN.Margin = New Padding(4, 3, 4, 3)
        pnl_PPN.Name = "pnl_PPN"
        pnl_PPN.Size = New Size(337, 30)
        pnl_PPN.TabIndex = 10353
        ' 
        ' pnl_PPhTerutang
        ' 
        pnl_PPhTerutang.Controls.Add(txt_PPhTerutang)
        pnl_PPhTerutang.Controls.Add(lbl_PPhTerutang)
        pnl_PPhTerutang.Dock = DockStyle.Top
        pnl_PPhTerutang.Location = New Point(0, 90)
        pnl_PPhTerutang.Margin = New Padding(4, 3, 4, 3)
        pnl_PPhTerutang.Name = "pnl_PPhTerutang"
        pnl_PPhTerutang.Size = New Size(337, 30)
        pnl_PPhTerutang.TabIndex = 10354
        ' 
        ' pnl_PPhDitanggung
        ' 
        pnl_PPhDitanggung.Controls.Add(txt_PPhDitanggung)
        pnl_PPhDitanggung.Controls.Add(Label4)
        pnl_PPhDitanggung.Dock = DockStyle.Top
        pnl_PPhDitanggung.Location = New Point(0, 120)
        pnl_PPhDitanggung.Margin = New Padding(4, 3, 4, 3)
        pnl_PPhDitanggung.Name = "pnl_PPhDitanggung"
        pnl_PPhDitanggung.Size = New Size(337, 30)
        pnl_PPhDitanggung.TabIndex = 10355
        ' 
        ' pnl_PPhDipotong
        ' 
        pnl_PPhDipotong.Controls.Add(txt_PPhDipotong)
        pnl_PPhDipotong.Controls.Add(Label5)
        pnl_PPhDipotong.Dock = DockStyle.Top
        pnl_PPhDipotong.Location = New Point(0, 150)
        pnl_PPhDipotong.Margin = New Padding(4, 3, 4, 3)
        pnl_PPhDipotong.Name = "pnl_PPhDipotong"
        pnl_PPhDipotong.Size = New Size(337, 30)
        pnl_PPhDipotong.TabIndex = 10356
        ' 
        ' pnl_BiayaTransportasi
        ' 
        pnl_BiayaTransportasi.Controls.Add(txt_BiayaTransportasi)
        pnl_BiayaTransportasi.Controls.Add(Label6)
        pnl_BiayaTransportasi.Dock = DockStyle.Top
        pnl_BiayaTransportasi.Location = New Point(0, 180)
        pnl_BiayaTransportasi.Margin = New Padding(4, 3, 4, 3)
        pnl_BiayaTransportasi.Name = "pnl_BiayaTransportasi"
        pnl_BiayaTransportasi.Size = New Size(337, 30)
        pnl_BiayaTransportasi.TabIndex = 10355
        ' 
        ' pnl_BiayaMaterai
        ' 
        pnl_BiayaMaterai.Controls.Add(txt_BiayaMaterai)
        pnl_BiayaMaterai.Controls.Add(Label7)
        pnl_BiayaMaterai.Dock = DockStyle.Top
        pnl_BiayaMaterai.Location = New Point(0, 210)
        pnl_BiayaMaterai.Margin = New Padding(4, 3, 4, 3)
        pnl_BiayaMaterai.Name = "pnl_BiayaMaterai"
        pnl_BiayaMaterai.Size = New Size(337, 30)
        pnl_BiayaMaterai.TabIndex = 10357
        ' 
        ' pnl_Retur
        ' 
        pnl_Retur.Controls.Add(txt_Retur)
        pnl_Retur.Controls.Add(Label8)
        pnl_Retur.Dock = DockStyle.Top
        pnl_Retur.Location = New Point(0, 240)
        pnl_Retur.Margin = New Padding(4, 3, 4, 3)
        pnl_Retur.Name = "pnl_Retur"
        pnl_Retur.Size = New Size(337, 30)
        pnl_Retur.TabIndex = 10358
        ' 
        ' pnl_JumlahTagihan
        ' 
        pnl_JumlahTagihan.Controls.Add(txt_JumlahTagihan)
        pnl_JumlahTagihan.Controls.Add(Label9)
        pnl_JumlahTagihan.Dock = DockStyle.Top
        pnl_JumlahTagihan.Location = New Point(0, 270)
        pnl_JumlahTagihan.Margin = New Padding(4, 3, 4, 3)
        pnl_JumlahTagihan.Name = "pnl_JumlahTagihan"
        pnl_JumlahTagihan.Size = New Size(337, 30)
        pnl_JumlahTagihan.TabIndex = 10359
        ' 
        ' pnl_SudahDibayar
        ' 
        pnl_SudahDibayar.Controls.Add(txt_SudahDibayar)
        pnl_SudahDibayar.Controls.Add(Label10)
        pnl_SudahDibayar.Dock = DockStyle.Top
        pnl_SudahDibayar.Location = New Point(0, 300)
        pnl_SudahDibayar.Margin = New Padding(4, 3, 4, 3)
        pnl_SudahDibayar.Name = "pnl_SudahDibayar"
        pnl_SudahDibayar.Size = New Size(337, 30)
        pnl_SudahDibayar.TabIndex = 10360
        ' 
        ' pnl_SisaTagihan
        ' 
        pnl_SisaTagihan.Controls.Add(txt_SisaTagihan)
        pnl_SisaTagihan.Controls.Add(Label11)
        pnl_SisaTagihan.Dock = DockStyle.Top
        pnl_SisaTagihan.Location = New Point(0, 330)
        pnl_SisaTagihan.Margin = New Padding(4, 3, 4, 3)
        pnl_SisaTagihan.Name = "pnl_SisaTagihan"
        pnl_SisaTagihan.Size = New Size(337, 30)
        pnl_SisaTagihan.TabIndex = 10361
        ' 
        ' pnl_JumlahInputan
        ' 
        pnl_JumlahInputan.Controls.Add(txt_Jumlah)
        pnl_JumlahInputan.Controls.Add(lbl_Jumlah)
        pnl_JumlahInputan.Dock = DockStyle.Top
        pnl_JumlahInputan.Location = New Point(0, 360)
        pnl_JumlahInputan.Margin = New Padding(4, 3, 4, 3)
        pnl_JumlahInputan.Name = "pnl_JumlahInputan"
        pnl_JumlahInputan.Size = New Size(337, 30)
        pnl_JumlahInputan.TabIndex = 10362
        ' 
        ' pnl_OK
        ' 
        pnl_OK.Controls.Add(btn_OK)
        pnl_OK.Controls.Add(btn_Batal)
        pnl_OK.Dock = DockStyle.Top
        pnl_OK.Location = New Point(0, 390)
        pnl_OK.Margin = New Padding(4, 3, 4, 3)
        pnl_OK.Name = "pnl_OK"
        pnl_OK.Size = New Size(337, 92)
        pnl_OK.TabIndex = 10363
        ' 
        ' pnl_Atas
        ' 
        pnl_Atas.Dock = DockStyle.Top
        pnl_Atas.Location = New Point(0, 0)
        pnl_Atas.Margin = New Padding(4, 3, 4, 3)
        pnl_Atas.Name = "pnl_Atas"
        pnl_Atas.Size = New Size(337, 30)
        pnl_Atas.TabIndex = 1
        ' 
        ' frm_InputJumlah
        ' 
        AcceptButton = btn_OK
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btn_Batal
        ClientSize = New Size(337, 484)
        Controls.Add(pnl_OK)
        Controls.Add(pnl_JumlahInputan)
        Controls.Add(pnl_SisaTagihan)
        Controls.Add(pnl_SudahDibayar)
        Controls.Add(pnl_JumlahTagihan)
        Controls.Add(pnl_Retur)
        Controls.Add(pnl_BiayaMaterai)
        Controls.Add(pnl_BiayaTransportasi)
        Controls.Add(pnl_PPhDipotong)
        Controls.Add(pnl_PPhDitanggung)
        Controls.Add(pnl_PPhTerutang)
        Controls.Add(pnl_PPN)
        Controls.Add(pnl_DPP)
        Controls.Add(pnl_Atas)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_InputJumlah"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Jumlah Bayar"
        pnl_DPP.ResumeLayout(False)
        pnl_DPP.PerformLayout()
        pnl_PPN.ResumeLayout(False)
        pnl_PPN.PerformLayout()
        pnl_PPhTerutang.ResumeLayout(False)
        pnl_PPhTerutang.PerformLayout()
        pnl_PPhDitanggung.ResumeLayout(False)
        pnl_PPhDitanggung.PerformLayout()
        pnl_PPhDipotong.ResumeLayout(False)
        pnl_PPhDipotong.PerformLayout()
        pnl_BiayaTransportasi.ResumeLayout(False)
        pnl_BiayaTransportasi.PerformLayout()
        pnl_BiayaMaterai.ResumeLayout(False)
        pnl_BiayaMaterai.PerformLayout()
        pnl_Retur.ResumeLayout(False)
        pnl_Retur.PerformLayout()
        pnl_JumlahTagihan.ResumeLayout(False)
        pnl_JumlahTagihan.PerformLayout()
        pnl_SudahDibayar.ResumeLayout(False)
        pnl_SudahDibayar.PerformLayout()
        pnl_SisaTagihan.ResumeLayout(False)
        pnl_SisaTagihan.PerformLayout()
        pnl_JumlahInputan.ResumeLayout(False)
        pnl_JumlahInputan.PerformLayout()
        pnl_OK.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_Jumlah As TextBox
    Friend WithEvents txt_DPP As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_PPN As TextBox
    Friend WithEvents lbl_PPhTerutang As Label
    Friend WithEvents txt_PPhTerutang As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_PPhDitanggung As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_PPhDipotong As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_BiayaTransportasi As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_BiayaMaterai As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_Retur As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_JumlahTagihan As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_SudahDibayar As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txt_SisaTagihan As TextBox
    Friend WithEvents lbl_Jumlah As Label
    Friend WithEvents pnl_DPP As Panel
    Friend WithEvents pnl_PPN As Panel
    Friend WithEvents pnl_PPhTerutang As Panel
    Friend WithEvents pnl_PPhDitanggung As Panel
    Friend WithEvents pnl_PPhDipotong As Panel
    Friend WithEvents pnl_BiayaTransportasi As Panel
    Friend WithEvents pnl_BiayaMaterai As Panel
    Friend WithEvents pnl_Retur As Panel
    Friend WithEvents pnl_JumlahTagihan As Panel
    Friend WithEvents pnl_SudahDibayar As Panel
    Friend WithEvents pnl_SisaTagihan As Panel
    Friend WithEvents pnl_JumlahInputan As Panel
    Friend WithEvents pnl_OK As Panel
    Friend WithEvents pnl_Atas As Panel
End Class
