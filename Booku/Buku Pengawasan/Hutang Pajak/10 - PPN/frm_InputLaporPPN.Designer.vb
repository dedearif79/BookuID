<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputLaporPPN
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_TanggalLapor = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_MasaPajak = New System.Windows.Forms.TextBox()
        Me.lbl_KompensasiKe = New System.Windows.Forms.Label()
        Me.cmb_KompensasiKe_Bulan = New System.Windows.Forms.ComboBox()
        Me.cmb_KompensasiKe_Tahun = New System.Windows.Forms.ComboBox()
        Me.txt_JumlahLebihBayar = New System.Windows.Forms.TextBox()
        Me.lbl_JumlahLebihBayar = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(199, 179)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(100, 41)
        Me.btn_Simpan.TabIndex = 900
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(110, 179)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 41)
        Me.btn_Batal.TabIndex = 999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(40, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 10216
        Me.Label1.Text = "Tanggal Lapor"
        '
        'dtp_TanggalLapor
        '
        Me.dtp_TanggalLapor.CustomFormat = "dd/MM/yyyy"
        Me.dtp_TanggalLapor.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_TanggalLapor.Location = New System.Drawing.Point(128, 51)
        Me.dtp_TanggalLapor.Name = "dtp_TanggalLapor"
        Me.dtp_TanggalLapor.Size = New System.Drawing.Size(95, 20)
        Me.dtp_TanggalLapor.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(40, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 10218
        Me.Label2.Text = "Masa Pajak"
        '
        'txt_MasaPajak
        '
        Me.txt_MasaPajak.Location = New System.Drawing.Point(128, 25)
        Me.txt_MasaPajak.Name = "txt_MasaPajak"
        Me.txt_MasaPajak.Size = New System.Drawing.Size(100, 20)
        Me.txt_MasaPajak.TabIndex = 10
        '
        'lbl_KompensasiKe
        '
        Me.lbl_KompensasiKe.AutoSize = True
        Me.lbl_KompensasiKe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_KompensasiKe.Location = New System.Drawing.Point(40, 106)
        Me.lbl_KompensasiKe.Name = "lbl_KompensasiKe"
        Me.lbl_KompensasiKe.Size = New System.Drawing.Size(80, 13)
        Me.lbl_KompensasiKe.TabIndex = 10220
        Me.lbl_KompensasiKe.Text = "Kompensasi Ke"
        '
        'cmb_KompensasiKe_Bulan
        '
        Me.cmb_KompensasiKe_Bulan.FormattingEnabled = True
        Me.cmb_KompensasiKe_Bulan.Location = New System.Drawing.Point(128, 103)
        Me.cmb_KompensasiKe_Bulan.Name = "cmb_KompensasiKe_Bulan"
        Me.cmb_KompensasiKe_Bulan.Size = New System.Drawing.Size(100, 21)
        Me.cmb_KompensasiKe_Bulan.TabIndex = 40
        '
        'cmb_KompensasiKe_Tahun
        '
        Me.cmb_KompensasiKe_Tahun.FormattingEnabled = True
        Me.cmb_KompensasiKe_Tahun.Location = New System.Drawing.Point(234, 103)
        Me.cmb_KompensasiKe_Tahun.Name = "cmb_KompensasiKe_Tahun"
        Me.cmb_KompensasiKe_Tahun.Size = New System.Drawing.Size(65, 21)
        Me.cmb_KompensasiKe_Tahun.TabIndex = 50
        '
        'txt_JumlahLebihBayar
        '
        Me.txt_JumlahLebihBayar.Location = New System.Drawing.Point(128, 77)
        Me.txt_JumlahLebihBayar.Name = "txt_JumlahLebihBayar"
        Me.txt_JumlahLebihBayar.Size = New System.Drawing.Size(100, 20)
        Me.txt_JumlahLebihBayar.TabIndex = 30
        '
        'lbl_JumlahLebihBayar
        '
        Me.lbl_JumlahLebihBayar.AutoSize = True
        Me.lbl_JumlahLebihBayar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_JumlahLebihBayar.Location = New System.Drawing.Point(40, 80)
        Me.lbl_JumlahLebihBayar.Name = "lbl_JumlahLebihBayar"
        Me.lbl_JumlahLebihBayar.Size = New System.Drawing.Size(56, 13)
        Me.lbl_JumlahLebihBayar.TabIndex = 10222
        Me.lbl_JumlahLebihBayar.Text = "Jumlah LB"
        '
        'frm_InputLaporPPN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(341, 246)
        Me.Controls.Add(Me.txt_JumlahLebihBayar)
        Me.Controls.Add(Me.lbl_JumlahLebihBayar)
        Me.Controls.Add(Me.cmb_KompensasiKe_Tahun)
        Me.Controls.Add(Me.cmb_KompensasiKe_Bulan)
        Me.Controls.Add(Me.lbl_KompensasiKe)
        Me.Controls.Add(Me.txt_MasaPajak)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtp_TanggalLapor)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputLaporPPN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Lapor PPN"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents dtp_TanggalLapor As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_MasaPajak As TextBox
    Friend WithEvents lbl_KompensasiKe As Label
    Friend WithEvents cmb_KompensasiKe_Bulan As ComboBox
    Friend WithEvents cmb_KompensasiKe_Tahun As ComboBox
    Friend WithEvents txt_JumlahLebihBayar As TextBox
    Friend WithEvents lbl_JumlahLebihBayar As Label
End Class
