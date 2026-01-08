<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputHutangPPhPasal25
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
        Me.txt_NomorBPHP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_JumlahTerutang = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_MasaPajak = New System.Windows.Forms.ComboBox()
        Me.cmb_Tahun = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(160, 278)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9931
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(71, 278)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9933
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 9934
        Me.Label1.Text = "Nomor BPHP"
        '
        'txt_NomorBPHP
        '
        Me.txt_NomorBPHP.Enabled = False
        Me.txt_NomorBPHP.Location = New System.Drawing.Point(132, 89)
        Me.txt_NomorBPHP.MaxLength = 2
        Me.txt_NomorBPHP.Name = "txt_NomorBPHP"
        Me.txt_NomorBPHP.Size = New System.Drawing.Size(111, 20)
        Me.txt_NomorBPHP.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 9936
        Me.Label2.Text = "Masa Pajak"
        '
        'txt_JumlahTerutang
        '
        Me.txt_JumlahTerutang.Location = New System.Drawing.Point(132, 115)
        Me.txt_JumlahTerutang.Name = "txt_JumlahTerutang"
        Me.txt_JumlahTerutang.Size = New System.Drawing.Size(111, 20)
        Me.txt_JumlahTerutang.TabIndex = 30
        Me.txt_JumlahTerutang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 118)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 13)
        Me.Label6.TabIndex = 9945
        Me.Label6.Text = "Jumlah Terutang"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(26, 169)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(217, 92)
        Me.txt_Keterangan.TabIndex = 90
        Me.txt_Keterangan.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 149)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 9947
        Me.Label3.Text = "Keterangan :"
        '
        'cmb_MasaPajak
        '
        Me.cmb_MasaPajak.FormattingEnabled = True
        Me.cmb_MasaPajak.Location = New System.Drawing.Point(132, 62)
        Me.cmb_MasaPajak.Name = "cmb_MasaPajak"
        Me.cmb_MasaPajak.Size = New System.Drawing.Size(111, 21)
        Me.cmb_MasaPajak.TabIndex = 20
        '
        'cmb_Tahun
        '
        Me.cmb_Tahun.FormattingEnabled = True
        Me.cmb_Tahun.Location = New System.Drawing.Point(132, 35)
        Me.cmb_Tahun.Name = "cmb_Tahun"
        Me.cmb_Tahun.Size = New System.Drawing.Size(59, 21)
        Me.cmb_Tahun.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 9951
        Me.Label5.Text = "Tahun"
        '
        'frm_InputHutangPPhPasal25
        '
        Me.AcceptButton = Me.btn_Simpan
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Batal
        Me.ClientSize = New System.Drawing.Size(270, 334)
        Me.Controls.Add(Me.cmb_Tahun)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmb_MasaPajak)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_JumlahTerutang)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_NomorBPHP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputHutangPPhPasal25"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Hutang PPh Pasal 25"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_NomorBPHP As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahTerutang As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_Keterangan As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmb_MasaPajak As ComboBox
    Friend WithEvents cmb_Tahun As ComboBox
    Friend WithEvents Label5 As Label
End Class
