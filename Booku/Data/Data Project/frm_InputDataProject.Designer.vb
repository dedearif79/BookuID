<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputDataProject
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
        Me.txt_KodeProject = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_NilaiProject = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Keterangan = New System.Windows.Forms.RichTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmb_Status = New System.Windows.Forms.ComboBox()
        Me.txt_NamaProject = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_NamaCustomer = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txt_KodeCustomer = New System.Windows.Forms.TextBox()
        Me.btn_PilihMitra = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_NomorPO = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Simpan.Location = New System.Drawing.Point(166, 357)
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
        Me.btn_Batal.Location = New System.Drawing.Point(77, 357)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10327
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_KodeProject
        '
        Me.txt_KodeProject.Location = New System.Drawing.Point(122, 26)
        Me.txt_KodeProject.MaxLength = 99
        Me.txt_KodeProject.Name = "txt_KodeProject"
        Me.txt_KodeProject.Size = New System.Drawing.Size(127, 20)
        Me.txt_KodeProject.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 10340
        Me.Label2.Text = "Kode Project"
        '
        'txt_NilaiProject
        '
        Me.txt_NilaiProject.Location = New System.Drawing.Point(122, 157)
        Me.txt_NilaiProject.MaxLength = 21
        Me.txt_NilaiProject.Name = "txt_NilaiProject"
        Me.txt_NilaiProject.Size = New System.Drawing.Size(127, 20)
        Me.txt_NilaiProject.TabIndex = 60
        Me.txt_NilaiProject.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 10342
        Me.Label3.Text = "Nilai Project"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 195)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 10347
        Me.Label5.Text = "Keterangan :"
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(23, 216)
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(226, 55)
        Me.txt_Keterangan.TabIndex = 90
        Me.txt_Keterangan.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 292)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 10348
        Me.Label6.Text = "Status"
        '
        'cmb_Status
        '
        Me.cmb_Status.FormattingEnabled = True
        Me.cmb_Status.Location = New System.Drawing.Point(122, 289)
        Me.cmb_Status.Name = "cmb_Status"
        Me.cmb_Status.Size = New System.Drawing.Size(81, 21)
        Me.cmb_Status.TabIndex = 100
        '
        'txt_NamaProject
        '
        Me.txt_NamaProject.Location = New System.Drawing.Point(122, 52)
        Me.txt_NamaProject.MaxLength = 99
        Me.txt_NamaProject.Name = "txt_NamaProject"
        Me.txt_NamaProject.Size = New System.Drawing.Size(127, 20)
        Me.txt_NamaProject.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(20, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 10350
        Me.Label7.Text = "Nama Project"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 134)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 10338
        Me.Label1.Text = "Nama Customer"
        '
        'txt_NamaCustomer
        '
        Me.txt_NamaCustomer.Location = New System.Drawing.Point(122, 131)
        Me.txt_NamaCustomer.Name = "txt_NamaCustomer"
        Me.txt_NamaCustomer.Size = New System.Drawing.Size(127, 20)
        Me.txt_NamaCustomer.TabIndex = 50
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(20, 108)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(79, 13)
        Me.Label24.TabIndex = 10337
        Me.Label24.Text = "Kode Customer"
        '
        'txt_KodeCustomer
        '
        Me.txt_KodeCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_KodeCustomer.Location = New System.Drawing.Point(122, 105)
        Me.txt_KodeCustomer.MaxLength = 3
        Me.txt_KodeCustomer.Name = "txt_KodeCustomer"
        Me.txt_KodeCustomer.Size = New System.Drawing.Size(81, 20)
        Me.txt_KodeCustomer.TabIndex = 40
        '
        'btn_PilihMitra
        '
        Me.btn_PilihMitra.Location = New System.Drawing.Point(209, 104)
        Me.btn_PilihMitra.Name = "btn_PilihMitra"
        Me.btn_PilihMitra.Size = New System.Drawing.Size(40, 23)
        Me.btn_PilihMitra.TabIndex = 45
        Me.btn_PilihMitra.Text = "Pilih"
        Me.btn_PilihMitra.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 13)
        Me.Label4.TabIndex = 10344
        Me.Label4.Text = "Nomor PO / SPK"
        '
        'txt_NomorPO
        '
        Me.txt_NomorPO.Location = New System.Drawing.Point(122, 78)
        Me.txt_NomorPO.MaxLength = 99
        Me.txt_NomorPO.Name = "txt_NomorPO"
        Me.txt_NomorPO.Size = New System.Drawing.Size(127, 20)
        Me.txt_NomorPO.TabIndex = 30
        '
        'frm_InputDataProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(275, 418)
        Me.Controls.Add(Me.txt_NamaProject)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmb_Status)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.txt_NomorPO)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_NilaiProject)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_KodeProject)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btn_PilihMitra)
        Me.Controls.Add(Me.txt_KodeCustomer)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txt_NamaCustomer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputDataProject"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Data Project"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Simpan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_KodeProject As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NilaiProject As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_Keterangan As RichTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmb_Status As ComboBox
    Friend WithEvents txt_NamaProject As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_NamaCustomer As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txt_KodeCustomer As TextBox
    Friend WithEvents btn_PilihMitra As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_NomorPO As TextBox
End Class
