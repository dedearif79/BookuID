<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RegistrasiPerangkat
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_NomorSeriProduk = New System.Windows.Forms.TextBox()
        Me.txt_IDKomputer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_IDCustomer = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_JumlahPerangkat = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_Kirim = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_Batal
        '
        Me.btn_Batal.Location = New System.Drawing.Point(147, 154)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 9933
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 9934
        Me.Label1.Text = "Nomor Seri Produk"
        '
        'txt_NomorSeriProduk
        '
        Me.txt_NomorSeriProduk.Location = New System.Drawing.Point(124, 22)
        Me.txt_NomorSeriProduk.MaxLength = 27
        Me.txt_NomorSeriProduk.Name = "txt_NomorSeriProduk"
        Me.txt_NomorSeriProduk.Size = New System.Drawing.Size(195, 20)
        Me.txt_NomorSeriProduk.TabIndex = 20
        '
        'txt_IDKomputer
        '
        Me.txt_IDKomputer.Enabled = False
        Me.txt_IDKomputer.Location = New System.Drawing.Point(124, 100)
        Me.txt_IDKomputer.Name = "txt_IDKomputer"
        Me.txt_IDKomputer.Size = New System.Drawing.Size(132, 20)
        Me.txt_IDKomputer.TabIndex = 9937
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 9936
        Me.Label2.Text = "ID Komputer"
        '
        'txt_IDCustomer
        '
        Me.txt_IDCustomer.Enabled = False
        Me.txt_IDCustomer.Location = New System.Drawing.Point(124, 48)
        Me.txt_IDCustomer.Name = "txt_IDCustomer"
        Me.txt_IDCustomer.Size = New System.Drawing.Size(132, 20)
        Me.txt_IDCustomer.TabIndex = 30
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 9946
        Me.Label4.Text = "ID Customer"
        '
        'txt_JumlahPerangkat
        '
        Me.txt_JumlahPerangkat.Enabled = False
        Me.txt_JumlahPerangkat.Location = New System.Drawing.Point(124, 74)
        Me.txt_JumlahPerangkat.Name = "txt_JumlahPerangkat"
        Me.txt_JumlahPerangkat.Size = New System.Drawing.Size(40, 20)
        Me.txt_JumlahPerangkat.TabIndex = 40
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 13)
        Me.Label6.TabIndex = 9948
        Me.Label6.Text = "Jumlah Perangkat"
        '
        'btn_Kirim
        '
        Me.btn_Kirim.Enabled = False
        Me.btn_Kirim.Location = New System.Drawing.Point(236, 154)
        Me.btn_Kirim.Name = "btn_Kirim"
        Me.btn_Kirim.Size = New System.Drawing.Size(83, 35)
        Me.btn_Kirim.TabIndex = 9931
        Me.btn_Kirim.Text = "Kirim"
        Me.btn_Kirim.UseVisualStyleBackColor = True
        '
        'frm_RegistrasiPerangkat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(347, 211)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_NomorSeriProduk)
        Me.Controls.Add(Me.txt_JumlahPerangkat)
        Me.Controls.Add(Me.txt_IDKomputer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_Kirim)
        Me.Controls.Add(Me.txt_IDCustomer)
        Me.Controls.Add(Me.btn_Batal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_RegistrasiPerangkat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registrasi Perangkat"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Batal As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_NomorSeriProduk As System.Windows.Forms.TextBox
    Friend WithEvents txt_IDKomputer As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_IDCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_JumlahPerangkat As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_Kirim As System.Windows.Forms.Button
End Class
