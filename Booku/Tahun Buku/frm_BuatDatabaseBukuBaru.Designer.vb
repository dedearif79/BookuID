<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_BuatDatabaseBukuBaru
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
        Me.btn_BuatDatabase = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_TahunBuku = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_JenisTahunBuku = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btn_BuatDatabase
        '
        Me.btn_BuatDatabase.Location = New System.Drawing.Point(106, 172)
        Me.btn_BuatDatabase.Name = "btn_BuatDatabase"
        Me.btn_BuatDatabase.Size = New System.Drawing.Size(108, 35)
        Me.btn_BuatDatabase.TabIndex = 9933
        Me.btn_BuatDatabase.Text = "Buat Database"
        Me.btn_BuatDatabase.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(82, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 9935
        Me.Label1.Text = "Tahun Buku :"
        '
        'txt_TahunBuku
        '
        Me.txt_TahunBuku.Location = New System.Drawing.Point(160, 79)
        Me.txt_TahunBuku.Name = "txt_TahunBuku"
        Me.txt_TahunBuku.Size = New System.Drawing.Size(54, 20)
        Me.txt_TahunBuku.TabIndex = 9936
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(111, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 9937
        Me.Label2.Text = "BUAT DATABASE :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(82, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 9938
        Me.Label3.Text = "Jenis :"
        '
        'cmb_JenisTahunBuku
        '
        Me.cmb_JenisTahunBuku.FormattingEnabled = True
        Me.cmb_JenisTahunBuku.Location = New System.Drawing.Point(160, 118)
        Me.cmb_JenisTahunBuku.Name = "cmb_JenisTahunBuku"
        Me.cmb_JenisTahunBuku.Size = New System.Drawing.Size(88, 21)
        Me.cmb_JenisTahunBuku.TabIndex = 9939
        '
        'frm_BuatDatabaseBukuBaru
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 229)
        Me.Controls.Add(Me.cmb_JenisTahunBuku)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_TahunBuku)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_BuatDatabase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_BuatDatabaseBukuBaru"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buat Database Buku Baru"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_BuatDatabase As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_TahunBuku As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmb_JenisTahunBuku As System.Windows.Forms.ComboBox
End Class
