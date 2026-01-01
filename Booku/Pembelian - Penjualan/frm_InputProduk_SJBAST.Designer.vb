<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_InputProduk_SJBAST
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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btn_Tambahkan = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.txt_Keterangan = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_Satuan = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_JumlahProduk = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_DeskripsiProduk = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_NamaProduk = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_NomorUrut = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(208, 130)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 13)
        Me.Label8.TabIndex = 10152
        Me.Label8.Text = "(pcs, kg, meter, dll)"
        '
        'btn_Tambahkan
        '
        Me.btn_Tambahkan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Tambahkan.Location = New System.Drawing.Point(231, 228)
        Me.btn_Tambahkan.Name = "btn_Tambahkan"
        Me.btn_Tambahkan.Size = New System.Drawing.Size(99, 35)
        Me.btn_Tambahkan.TabIndex = 10143
        Me.btn_Tambahkan.Text = "Tambahkan"
        Me.btn_Tambahkan.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Batal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Batal.Location = New System.Drawing.Point(142, 228)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(83, 35)
        Me.btn_Batal.TabIndex = 10144
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'txt_Keterangan
        '
        Me.txt_Keterangan.Location = New System.Drawing.Point(142, 153)
        Me.txt_Keterangan.Multiline = True
        Me.txt_Keterangan.Name = "txt_Keterangan"
        Me.txt_Keterangan.Size = New System.Drawing.Size(188, 47)
        Me.txt_Keterangan.TabIndex = 10138
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 156)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 10150
        Me.Label6.Text = "Keterangan"
        '
        'txt_Satuan
        '
        Me.txt_Satuan.Location = New System.Drawing.Point(142, 127)
        Me.txt_Satuan.Name = "txt_Satuan"
        Me.txt_Satuan.Size = New System.Drawing.Size(64, 20)
        Me.txt_Satuan.TabIndex = 10137
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(20, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 10149
        Me.Label5.Text = "Satuan"
        '
        'txt_JumlahProduk
        '
        Me.txt_JumlahProduk.Location = New System.Drawing.Point(142, 101)
        Me.txt_JumlahProduk.Name = "txt_JumlahProduk"
        Me.txt_JumlahProduk.Size = New System.Drawing.Size(106, 20)
        Me.txt_JumlahProduk.TabIndex = 10136
        Me.txt_JumlahProduk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 10148
        Me.Label4.Text = "Jumlah"
        '
        'txt_DeskripsiProduk
        '
        Me.txt_DeskripsiProduk.Location = New System.Drawing.Point(142, 75)
        Me.txt_DeskripsiProduk.Name = "txt_DeskripsiProduk"
        Me.txt_DeskripsiProduk.Size = New System.Drawing.Size(188, 20)
        Me.txt_DeskripsiProduk.TabIndex = 10135
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 10147
        Me.Label2.Text = "Deskripsi"
        '
        'txt_NamaProduk
        '
        Me.txt_NamaProduk.Location = New System.Drawing.Point(142, 49)
        Me.txt_NamaProduk.Name = "txt_NamaProduk"
        Me.txt_NamaProduk.Size = New System.Drawing.Size(188, 20)
        Me.txt_NamaProduk.TabIndex = 10134
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 10146
        Me.Label1.Text = "Nama Barang/Jasa"
        '
        'txt_NomorUrut
        '
        Me.txt_NomorUrut.Location = New System.Drawing.Point(142, 23)
        Me.txt_NomorUrut.Name = "txt_NomorUrut"
        Me.txt_NomorUrut.Size = New System.Drawing.Size(52, 20)
        Me.txt_NomorUrut.TabIndex = 10133
        Me.txt_NomorUrut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 10145
        Me.Label3.Text = "Nomor Urut"
        '
        'frm_InputProduk_SJBAST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 285)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btn_Tambahkan)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.txt_Keterangan)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_Satuan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_JumlahProduk)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_DeskripsiProduk)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_NamaProduk)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_NomorUrut)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_InputProduk_SJBAST"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input Barang/Jasa"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label8 As Label
    Friend WithEvents btn_Tambahkan As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents txt_Keterangan As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txt_Satuan As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_JumlahProduk As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_DeskripsiProduk As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_NamaProduk As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_NomorUrut As TextBox
    Friend WithEvents Label3 As Label
End Class
