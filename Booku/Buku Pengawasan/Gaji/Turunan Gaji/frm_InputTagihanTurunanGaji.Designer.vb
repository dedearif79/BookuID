<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputTagihanTurunanGaji
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
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.btn_Simpan = New System.Windows.Forms.Button()
        Me.txt_JumlahTagihan = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txt_Bulan = New System.Windows.Forms.TextBox()
        Me.lbl_NamaAkun = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_JumlahPotongan = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'btn_Batal
        '
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(64, 251)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(84, 36)
        Me.btn_Batal.TabIndex = 999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(154, 251)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(84, 36)
        Me.btn_Simpan.TabIndex = 900
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'txt_JumlahTagihan
        '
        Me.txt_JumlahTagihan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JumlahTagihan.Location = New System.Drawing.Point(118, 60)
        Me.txt_JumlahTagihan.Name = "txt_JumlahTagihan"
        Me.txt_JumlahTagihan.Size = New System.Drawing.Size(120, 20)
        Me.txt_JumlahTagihan.TabIndex = 20
        Me.txt_JumlahTagihan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(21, 63)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 13)
        Me.Label11.TabIndex = 100000011
        Me.Label11.Text = "Jumlah Tagihan"
        '
        'txt_Bulan
        '
        Me.txt_Bulan.Enabled = False
        Me.txt_Bulan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Bulan.Location = New System.Drawing.Point(118, 27)
        Me.txt_Bulan.Name = "txt_Bulan"
        Me.txt_Bulan.Size = New System.Drawing.Size(120, 20)
        Me.txt_Bulan.TabIndex = 10
        '
        'lbl_NamaAkun
        '
        Me.lbl_NamaAkun.AutoSize = True
        Me.lbl_NamaAkun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaAkun.Location = New System.Drawing.Point(20, 30)
        Me.lbl_NamaAkun.Name = "lbl_NamaAkun"
        Me.lbl_NamaAkun.Size = New System.Drawing.Size(34, 13)
        Me.lbl_NamaAkun.TabIndex = 100000010
        Me.lbl_NamaAkun.Text = "Bulan"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 100000013
        Me.Label1.Text = "Jumlah Potongan"
        '
        'txt_JumlahPotongan
        '
        Me.txt_JumlahPotongan.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JumlahPotongan.Location = New System.Drawing.Point(118, 93)
        Me.txt_JumlahPotongan.Name = "txt_JumlahPotongan"
        Me.txt_JumlahPotongan.Size = New System.Drawing.Size(120, 20)
        Me.txt_JumlahPotongan.TabIndex = 30
        Me.txt_JumlahPotongan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 100000015
        Me.Label3.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(23, 147)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(215, 81)
        Me.txt_Keterangan.TabIndex = 100000014
        Me.txt_Keterangan.Text = ""
        '
        'frm_InputTagihanBpjs
        '
        Me.AcceptButton = Me.btn_Simpan
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Batal
        Me.ClientSize = New System.Drawing.Size(260, 310)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_JumlahPotongan)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_JumlahTagihan)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt_Bulan)
        Me.Controls.Add(Me.lbl_NamaAkun)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputTagihanBpjs"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Input Tagihan BPJS"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents txt_JumlahTagihan As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txt_Bulan As System.Windows.Forms.TextBox
    Friend WithEvents lbl_NamaAkun As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahPotongan As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_Keterangan As System.Windows.Forms.RichTextBox
End Class
