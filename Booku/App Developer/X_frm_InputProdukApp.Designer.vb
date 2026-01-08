<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_InputProdukApp
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
        Me.txt_NomorSeriProduk = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_IDCustomer = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_JumlahPerangkat = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_StatusTerpakai = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Reset
        '
        Me.btn_Reset.Location = New System.Drawing.Point(84, 180)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(83, 35)
        Me.btn_Reset.TabIndex = 9000
        Me.btn_Reset.Text = "Reset"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'btn_Simpan
        '
        Me.btn_Simpan.Location = New System.Drawing.Point(173, 180)
        Me.btn_Simpan.Name = "btn_Simpan"
        Me.btn_Simpan.Size = New System.Drawing.Size(83, 35)
        Me.btn_Simpan.TabIndex = 9100
        Me.btn_Simpan.Text = "Simpan"
        Me.btn_Simpan.UseVisualStyleBackColor = True
        '
        'btn_Tutup
        '
        Me.btn_Tutup.Location = New System.Drawing.Point(262, 180)
        Me.btn_Tutup.Name = "btn_Tutup"
        Me.btn_Tutup.Size = New System.Drawing.Size(83, 35)
        Me.btn_Tutup.TabIndex = 9999
        Me.btn_Tutup.Text = "Tutup"
        Me.btn_Tutup.UseVisualStyleBackColor = True
        '
        'txt_NomorSeriProduk
        '
        Me.txt_NomorSeriProduk.Location = New System.Drawing.Point(134, 26)
        Me.txt_NomorSeriProduk.Name = "txt_NomorSeriProduk"
        Me.txt_NomorSeriProduk.Size = New System.Drawing.Size(266, 20)
        Me.txt_NomorSeriProduk.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 9936
        Me.Label2.Text = "Nomor Seri Produk"
        '
        'txt_IDCustomer
        '
        Me.txt_IDCustomer.Location = New System.Drawing.Point(134, 52)
        Me.txt_IDCustomer.MaxLength = 8
        Me.txt_IDCustomer.Name = "txt_IDCustomer"
        Me.txt_IDCustomer.Size = New System.Drawing.Size(103, 20)
        Me.txt_IDCustomer.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(32, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 9945
        Me.Label6.Text = "ID Customer"
        '
        'txt_JumlahPerangkat
        '
        Me.txt_JumlahPerangkat.Location = New System.Drawing.Point(134, 78)
        Me.txt_JumlahPerangkat.Name = "txt_JumlahPerangkat"
        Me.txt_JumlahPerangkat.Size = New System.Drawing.Size(51, 20)
        Me.txt_JumlahPerangkat.TabIndex = 30
        Me.txt_JumlahPerangkat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 9947
        Me.Label1.Text = "Jumlah Perangkat"
        '
        'txt_StatusTerpakai
        '
        Me.txt_StatusTerpakai.Location = New System.Drawing.Point(134, 104)
        Me.txt_StatusTerpakai.Name = "txt_StatusTerpakai"
        Me.txt_StatusTerpakai.Size = New System.Drawing.Size(51, 20)
        Me.txt_StatusTerpakai.TabIndex = 40
        Me.txt_StatusTerpakai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 9950
        Me.Label3.Text = "Status Terpakai"
        '
        'frm_InputProdukApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(425, 239)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_StatusTerpakai)
        Me.Controls.Add(Me.txt_JumlahPerangkat)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_IDCustomer)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_NomorSeriProduk)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btn_Reset)
        Me.Controls.Add(Me.btn_Simpan)
        Me.Controls.Add(Me.btn_Tutup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_InputProdukApp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Produk App"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents btn_Simpan As System.Windows.Forms.Button
    Friend WithEvents btn_Tutup As System.Windows.Forms.Button
    Friend WithEvents txt_NomorSeriProduk As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_IDCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahPerangkat As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_StatusTerpakai As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
