<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class X_frm_GantiPassword
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
        Me.btn_Ganti = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_PasswordLama = New System.Windows.Forms.TextBox()
        Me.txt_PasswordBaru = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_Ganti
        '
        Me.btn_Ganti.Location = New System.Drawing.Point(117, 137)
        Me.btn_Ganti.Name = "btn_Ganti"
        Me.btn_Ganti.Size = New System.Drawing.Size(83, 35)
        Me.btn_Ganti.TabIndex = 9933
        Me.btn_Ganti.Text = "Ganti"
        Me.btn_Ganti.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 9935
        Me.Label1.Text = "Password Lama :"
        '
        'txt_PasswordLama
        '
        Me.txt_PasswordLama.Location = New System.Drawing.Point(129, 34)
        Me.txt_PasswordLama.Name = "txt_PasswordLama"
        Me.txt_PasswordLama.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_PasswordLama.Size = New System.Drawing.Size(151, 20)
        Me.txt_PasswordLama.TabIndex = 9936
        '
        'txt_PasswordBaru
        '
        Me.txt_PasswordBaru.Location = New System.Drawing.Point(129, 76)
        Me.txt_PasswordBaru.Name = "txt_PasswordBaru"
        Me.txt_PasswordBaru.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txt_PasswordBaru.Size = New System.Drawing.Size(151, 20)
        Me.txt_PasswordBaru.TabIndex = 9938
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(39, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 9937
        Me.Label2.Text = "Password Baru :"
        '
        'frm_GantiPassword
        '
        Me.AcceptButton = Me.btn_Ganti
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 204)
        Me.Controls.Add(Me.txt_PasswordBaru)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_PasswordLama)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_Ganti)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_GantiPassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ganti Password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Ganti As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_PasswordLama As System.Windows.Forms.TextBox
    Friend WithEvents txt_PasswordBaru As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
