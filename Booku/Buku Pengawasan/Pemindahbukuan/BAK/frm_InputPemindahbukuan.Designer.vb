<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputPemindahbukuan
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
        Me.txt_NomorBPPB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_DariBuku = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmb_KeBuku = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Penanggungjawab = New System.Windows.Forms.TextBox()
        Me.lbl_Penanggungjawab = New System.Windows.Forms.Label()
        Me.txt_JumlahTransaksi = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txt_UraianTransaksi = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_TanggalBPPB = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(254, 313)
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
        Me.btn_Batal.Location = New System.Drawing.Point(165, 313)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10327
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_NomorBPPB
        '
        Me.txt_NomorBPPB.Enabled = False
        Me.txt_NomorBPPB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NomorBPPB.Location = New System.Drawing.Point(142, 27)
        Me.txt_NomorBPPB.Name = "txt_NomorBPPB"
        Me.txt_NomorBPPB.Size = New System.Drawing.Size(120, 20)
        Me.txt_NomorBPPB.TabIndex = 10328
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(32, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 10329
        Me.Label3.Text = "Nomor Pengajuan"
        '
        'cmb_DariBuku
        '
        Me.cmb_DariBuku.FormattingEnabled = True
        Me.cmb_DariBuku.Location = New System.Drawing.Point(142, 79)
        Me.cmb_DariBuku.Name = "cmb_DariBuku"
        Me.cmb_DariBuku.Size = New System.Drawing.Size(195, 21)
        Me.cmb_DariBuku.TabIndex = 10330
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(32, 82)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 10331
        Me.Label7.Text = "Dari Buku"
        '
        'cmb_KeBuku
        '
        Me.cmb_KeBuku.FormattingEnabled = True
        Me.cmb_KeBuku.Location = New System.Drawing.Point(142, 106)
        Me.cmb_KeBuku.Name = "cmb_KeBuku"
        Me.cmb_KeBuku.Size = New System.Drawing.Size(195, 21)
        Me.cmb_KeBuku.TabIndex = 10332
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(32, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 10333
        Me.Label2.Text = "Ke Buku"
        '
        'txt_Penanggungjawab
        '
        Me.txt_Penanggungjawab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Penanggungjawab.Location = New System.Drawing.Point(142, 133)
        Me.txt_Penanggungjawab.Name = "txt_Penanggungjawab"
        Me.txt_Penanggungjawab.Size = New System.Drawing.Size(195, 20)
        Me.txt_Penanggungjawab.TabIndex = 100000020
        '
        'lbl_Penanggungjawab
        '
        Me.lbl_Penanggungjawab.AutoSize = True
        Me.lbl_Penanggungjawab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Penanggungjawab.Location = New System.Drawing.Point(32, 136)
        Me.lbl_Penanggungjawab.Name = "lbl_Penanggungjawab"
        Me.lbl_Penanggungjawab.Size = New System.Drawing.Size(96, 13)
        Me.lbl_Penanggungjawab.TabIndex = 100000023
        Me.lbl_Penanggungjawab.Text = "Penanggungjawab"
        '
        'txt_JumlahTransaksi
        '
        Me.txt_JumlahTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JumlahTransaksi.Location = New System.Drawing.Point(142, 159)
        Me.txt_JumlahTransaksi.Name = "txt_JumlahTransaksi"
        Me.txt_JumlahTransaksi.Size = New System.Drawing.Size(120, 20)
        Me.txt_JumlahTransaksi.TabIndex = 100000021
        Me.txt_JumlahTransaksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(32, 162)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 100000022
        Me.Label11.Text = "Jumlah Transaksi"
        '
        'txt_UraianTransaksi
        '
        Me.txt_UraianTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_UraianTransaksi.Location = New System.Drawing.Point(35, 213)
        Me.txt_UraianTransaksi.Multiline = True
        Me.txt_UraianTransaksi.Name = "txt_UraianTransaksi"
        Me.txt_UraianTransaksi.Size = New System.Drawing.Size(302, 74)
        Me.txt_UraianTransaksi.TabIndex = 100000024
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 194)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 100000025
        Me.Label1.Text = "Uraian Transaksi :"
        '
        'dtp_TanggalBPPB
        '
        Me.dtp_TanggalBPPB.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalBPPB.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalBPPB.Location = New System.Drawing.Point(142, 53)
        Me.dtp_TanggalBPPB.Name = "dtp_TanggalBPPB"
        Me.dtp_TanggalBPPB.Size = New System.Drawing.Size(81, 20)
        Me.dtp_TanggalBPPB.TabIndex = 100000026
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(32, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 100000027
        Me.Label4.Text = "Tanggal Pengajuan"
        '
        'frm_InputPemindahbukuan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(371, 374)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtp_TanggalBPPB)
        Me.Controls.Add(Me.txt_UraianTransaksi)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_Penanggungjawab)
        Me.Controls.Add(Me.lbl_Penanggungjawab)
        Me.Controls.Add(Me.txt_JumlahTransaksi)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmb_KeBuku)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmb_DariBuku)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_NomorBPPB)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputPemindahbukuan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Pemindahbukuan"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_NomorBPPB As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmb_DariBuku As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cmb_KeBuku As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_Penanggungjawab As TextBox
    Friend WithEvents lbl_Penanggungjawab As Label
    Friend WithEvents txt_JumlahTransaksi As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txt_UraianTransaksi As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtp_TanggalBPPB As DateTimePicker
    Friend WithEvents Label4 As Label
End Class
