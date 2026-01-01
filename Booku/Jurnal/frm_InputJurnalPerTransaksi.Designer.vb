<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputJurnalPerTransaksi
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
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_PilihCOA = New System.Windows.Forms.Button()
        Me.txt_COA = New System.Windows.Forms.TextBox()
        Me.lbl_COA = New System.Windows.Forms.Label()
        Me.txt_JumlahTransaksi = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txt_NamaAkun = New System.Windows.Forms.TextBox()
        Me.lbl_NamaAkun = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_DK = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btn_Batal
        '
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(99, 164)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(84, 36)
        Me.btn_Batal.TabIndex = 999
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'btn_OK
        '
        Me.btn_OK.Location = New System.Drawing.Point(189, 164)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(84, 36)
        Me.btn_OK.TabIndex = 900
        Me.btn_OK.Text = "OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_PilihCOA
        '
        Me.btn_PilihCOA.Location = New System.Drawing.Point(201, 24)
        Me.btn_PilihCOA.Name = "btn_PilihCOA"
        Me.btn_PilihCOA.Size = New System.Drawing.Size(72, 23)
        Me.btn_PilihCOA.TabIndex = 20
        Me.btn_PilihCOA.Text = "Pilih"
        Me.btn_PilihCOA.UseVisualStyleBackColor = True
        '
        'txt_COA
        '
        Me.txt_COA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_COA.Location = New System.Drawing.Point(118, 26)
        Me.txt_COA.Name = "txt_COA"
        Me.txt_COA.Size = New System.Drawing.Size(77, 20)
        Me.txt_COA.TabIndex = 10
        '
        'lbl_COA
        '
        Me.lbl_COA.AutoSize = True
        Me.lbl_COA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_COA.Location = New System.Drawing.Point(21, 29)
        Me.lbl_COA.Name = "lbl_COA"
        Me.lbl_COA.Size = New System.Drawing.Size(60, 13)
        Me.lbl_COA.TabIndex = 100000012
        Me.lbl_COA.Text = "Kode Akun"
        '
        'txt_JumlahTransaksi
        '
        Me.txt_JumlahTransaksi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_JumlahTransaksi.Location = New System.Drawing.Point(118, 107)
        Me.txt_JumlahTransaksi.Name = "txt_JumlahTransaksi"
        Me.txt_JumlahTransaksi.Size = New System.Drawing.Size(120, 20)
        Me.txt_JumlahTransaksi.TabIndex = 50
        Me.txt_JumlahTransaksi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(21, 110)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 100000011
        Me.Label11.Text = "Jumlah Transaksi"
        '
        'txt_NamaAkun
        '
        Me.txt_NamaAkun.Enabled = False
        Me.txt_NamaAkun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_NamaAkun.Location = New System.Drawing.Point(118, 54)
        Me.txt_NamaAkun.Name = "txt_NamaAkun"
        Me.txt_NamaAkun.Size = New System.Drawing.Size(156, 20)
        Me.txt_NamaAkun.TabIndex = 30
        '
        'lbl_NamaAkun
        '
        Me.lbl_NamaAkun.AutoSize = True
        Me.lbl_NamaAkun.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NamaAkun.Location = New System.Drawing.Point(21, 57)
        Me.lbl_NamaAkun.Name = "lbl_NamaAkun"
        Me.lbl_NamaAkun.Size = New System.Drawing.Size(63, 13)
        Me.lbl_NamaAkun.TabIndex = 100000010
        Me.lbl_NamaAkun.Text = "Nama Akun"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 100000014
        Me.Label1.Text = "Debet/Kredit"
        '
        'cmb_DK
        '
        Me.cmb_DK.FormattingEnabled = True
        Me.cmb_DK.Location = New System.Drawing.Point(118, 80)
        Me.cmb_DK.Name = "cmb_DK"
        Me.cmb_DK.Size = New System.Drawing.Size(121, 21)
        Me.cmb_DK.TabIndex = 40
        '
        'frm_InputJurnalPerTransaksi
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Batal
        Me.ClientSize = New System.Drawing.Size(300, 222)
        Me.Controls.Add(Me.cmb_DK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_PilihCOA)
        Me.Controls.Add(Me.txt_COA)
        Me.Controls.Add(Me.lbl_COA)
        Me.Controls.Add(Me.txt_JumlahTransaksi)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt_NamaAkun)
        Me.Controls.Add(Me.lbl_NamaAkun)
        Me.Controls.Add(Me.btn_OK)
        Me.Controls.Add(Me.btn_Batal)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputJurnalPerTransaksi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Item Jurnal"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents btn_OK As System.Windows.Forms.Button
    Friend WithEvents btn_PilihCOA As System.Windows.Forms.Button
    Friend WithEvents txt_COA As System.Windows.Forms.TextBox
    Friend WithEvents lbl_COA As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahTransaksi As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txt_NamaAkun As System.Windows.Forms.TextBox
    Friend WithEvents lbl_NamaAkun As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_DK As System.Windows.Forms.ComboBox
End Class
