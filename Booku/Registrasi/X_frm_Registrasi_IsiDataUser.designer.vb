<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Registrasi_IsiDataUser
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
        Label6 = New Label()
        grb_DataUserPerdana = New GroupBox()
        txt_NamaUser = New TextBox()
        Label9 = New Label()
        txt_Password = New TextBox()
        Label8 = New Label()
        txt_Username = New TextBox()
        Label5 = New Label()
        Label3 = New Label()
        btn_Lanjutkan = New Button()
        grb_DataUserPerdana.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(147, 21)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(94, 15)
        Label6.TabIndex = 10003
        Label6.Text = "Langkah Ketiga :"
        ' 
        ' grb_DataUserPerdana
        ' 
        grb_DataUserPerdana.Controls.Add(txt_NamaUser)
        grb_DataUserPerdana.Controls.Add(Label9)
        grb_DataUserPerdana.Controls.Add(txt_Password)
        grb_DataUserPerdana.Controls.Add(Label8)
        grb_DataUserPerdana.Controls.Add(txt_Username)
        grb_DataUserPerdana.Controls.Add(Label5)
        grb_DataUserPerdana.Location = New Point(29, 96)
        grb_DataUserPerdana.Margin = New Padding(4, 3, 4, 3)
        grb_DataUserPerdana.Name = "grb_DataUserPerdana"
        grb_DataUserPerdana.Padding = New Padding(4, 3, 4, 3)
        grb_DataUserPerdana.Size = New Size(340, 143)
        grb_DataUserPerdana.TabIndex = 20
        grb_DataUserPerdana.TabStop = False
        grb_DataUserPerdana.Text = "Data User Perdana :"
        ' 
        ' txt_NamaUser
        ' 
        txt_NamaUser.Location = New Point(124, 96)
        txt_NamaUser.Margin = New Padding(4, 3, 4, 3)
        txt_NamaUser.MaxLength = 63
        txt_NamaUser.Name = "txt_NamaUser"
        txt_NamaUser.Size = New Size(194, 23)
        txt_NamaUser.TabIndex = 50
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(20, 99)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(87, 15)
        Label9.TabIndex = 10015
        Label9.Text = "Nama Lengkap"
        ' 
        ' txt_Password
        ' 
        txt_Password.Location = New Point(124, 66)
        txt_Password.Margin = New Padding(4, 3, 4, 3)
        txt_Password.Name = "txt_Password"
        txt_Password.PasswordChar = "*"c
        txt_Password.Size = New Size(194, 23)
        txt_Password.TabIndex = 40
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(20, 69)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(57, 15)
        Label8.TabIndex = 10013
        Label8.Text = "Password"
        ' 
        ' txt_Username
        ' 
        txt_Username.Location = New Point(124, 36)
        txt_Username.Margin = New Padding(4, 3, 4, 3)
        txt_Username.MaxLength = 63
        txt_Username.Name = "txt_Username"
        txt_Username.Size = New Size(194, 23)
        txt_Username.TabIndex = 30
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(20, 39)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(60, 15)
        Label5.TabIndex = 10011
        Label5.Text = "Username"
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(40, 46)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(318, 43)
        Label3.TabIndex = 10005
        Label3.Text = "Isi Form di bawah untuk mendaftarkan user perdana, sebagi Super User :"
        Label3.TextAlign = ContentAlignment.TopCenter
        ' 
        ' btn_Lanjutkan
        ' 
        btn_Lanjutkan.Location = New Point(135, 258)
        btn_Lanjutkan.Margin = New Padding(4, 3, 4, 3)
        btn_Lanjutkan.Name = "btn_Lanjutkan"
        btn_Lanjutkan.Size = New Size(126, 40)
        btn_Lanjutkan.TabIndex = 10008
        btn_Lanjutkan.Text = "Lanjutkan"
        btn_Lanjutkan.UseVisualStyleBackColor = True
        ' 
        ' frm_Registrasi_IsiDataUser
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(398, 349)
        ControlBox = False
        Controls.Add(btn_Lanjutkan)
        Controls.Add(Label3)
        Controls.Add(grb_DataUserPerdana)
        Controls.Add(Label6)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frm_Registrasi_IsiDataUser"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Buat Database General - Langkah Ketiga"
        grb_DataUserPerdana.ResumeLayout(False)
        grb_DataUserPerdana.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grb_DataUserPerdana As System.Windows.Forms.GroupBox
    Friend WithEvents txt_NamaUser As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_Password As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_Username As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btn_Lanjutkan As System.Windows.Forms.Button
End Class
