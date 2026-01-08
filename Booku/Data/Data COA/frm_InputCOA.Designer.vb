<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputCOA
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
        Me.txt_COA = New System.Windows.Forms.TextBox()
        Me.txt_NamaAkun = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmb_DebetKredit = New System.Windows.Forms.ComboBox()
        Me.txt_SaldoAwal = New System.Windows.Forms.TextBox()
        Me.lbl_SaldoAwal = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_Uraian = New System.Windows.Forms.TextBox()
        Me.cmb_Visibilitas = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Reset
        '
        Me.btn_Reset.Location = New System.Drawing.Point(111, 252)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(83, 35)
        Me.btn_Reset.TabIndex = 9932
        Me.btn_Reset.Text = "Reset"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'btn_Simpan
        '
        Me.btn_Simpan.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btn_Simpan.Location = New System.Drawing.Point(200, 252)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9931
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Location = New System.Drawing.Point(289, 252)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tutup.TabIndex = 9933
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 9934
        Me.Label1.Text = "Kode Akun"
        '
        'txt_COA
        '
        Me.txt_COA.Location = New System.Drawing.Point(104, 19)
        Me.txt_COA.Name = "txt_COA"
        Me.txt_COA.Size = New System.Drawing.Size(83, 20)
        Me.txt_COA.TabIndex = 9935
        '
        'txt_NamaAkun
        '
        Me.txt_NamaAkun.Location = New System.Drawing.Point(104, 45)
        Me.txt_NamaAkun.Name = "txt_NamaAkun"
        Me.txt_NamaAkun.Size = New System.Drawing.Size(268, 20)
        Me.txt_NamaAkun.TabIndex = 9937
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 9936
        Me.Label2.Text = "Nama Akun"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 9942
        Me.Label5.Text = "Debet/Kredit"
        '
        'cmb_DebetKredit
        '
        Me.cmb_DebetKredit.Enabled = False
        Me.cmb_DebetKredit.FormattingEnabled = True
        Me.cmb_DebetKredit.Location = New System.Drawing.Point(104, 71)
        Me.cmb_DebetKredit.Name = "cmb_DebetKredit"
        Me.cmb_DebetKredit.Size = New System.Drawing.Size(83, 21)
        Me.cmb_DebetKredit.TabIndex = 9944
        '
        'txt_SaldoAwal
        '
        Me.txt_SaldoAwal.Enabled = False
        Me.txt_SaldoAwal.Location = New System.Drawing.Point(104, 98)
        Me.txt_SaldoAwal.Name = "txt_SaldoAwal"
        Me.txt_SaldoAwal.Size = New System.Drawing.Size(151, 20)
        Me.txt_SaldoAwal.TabIndex = 9946
        Me.txt_SaldoAwal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_SaldoAwal
        '
        Me.lbl_SaldoAwal.AutoSize = True
        Me.lbl_SaldoAwal.Location = New System.Drawing.Point(12, 101)
        Me.lbl_SaldoAwal.Name = "lbl_SaldoAwal"
        Me.lbl_SaldoAwal.Size = New System.Drawing.Size(60, 13)
        Me.lbl_SaldoAwal.TabIndex = 9945
        Me.lbl_SaldoAwal.Text = "Saldo Awal"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 127)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 9947
        Me.Label7.Text = "Uraian"
        '
        'txt_Uraian
        '
        Me.txt_Uraian.Location = New System.Drawing.Point(104, 124)
        Me.txt_Uraian.Multiline = True
        Me.txt_Uraian.Name = "txt_Uraian"
        Me.txt_Uraian.Size = New System.Drawing.Size(268, 57)
        Me.txt_Uraian.TabIndex = 9948
        '
        'cmb_Visibilitas
        '
        Me.cmb_Visibilitas.FormattingEnabled = True
        Me.cmb_Visibilitas.Location = New System.Drawing.Point(104, 187)
        Me.cmb_Visibilitas.Name = "cmb_Visibilitas"
        Me.cmb_Visibilitas.Size = New System.Drawing.Size(58, 21)
        Me.cmb_Visibilitas.TabIndex = 9950
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 190)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 9949
        Me.Label8.Text = "Visibilitas"
        '
        'frm_InputCOA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 299)
        Me.Controls.Add(Me.cmb_Visibilitas)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt_Uraian)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_SaldoAwal)
        Me.Controls.Add(Me.lbl_SaldoAwal)
        Me.Controls.Add(Me.cmb_DebetKredit)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_NamaAkun)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_COA)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Reset)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Tutup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputCOA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Data COA"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_COA As System.Windows.Forms.TextBox
    Friend WithEvents txt_NamaAkun As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_DebetKredit As System.Windows.Forms.ComboBox
    Friend WithEvents txt_SaldoAwal As System.Windows.Forms.TextBox
    Friend WithEvents lbl_SaldoAwal As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_Uraian As System.Windows.Forms.TextBox
    Friend WithEvents cmb_Visibilitas As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
