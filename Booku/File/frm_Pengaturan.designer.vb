<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Pengaturan
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
        Label1 = New Label()
        txt_LokasiServer = New TextBox()
        btn_SimpanPerubahanKoneksiDatabase = New Button()
        txt_UserDatabase = New TextBox()
        Label2 = New Label()
        txt_PasswordDatabase = New TextBox()
        Label3 = New Label()
        txt_PortServer = New TextBox()
        Label4 = New Label()
        btn_TesKoneksi = New Button()
        btn_BukaFolder = New Button()
        txt_FolderXAMPP = New TextBox()
        Label7 = New Label()
        fbd_Backup = New FolderBrowserDialog()
        fbd_FolderXAMPP = New FolderBrowserDialog()
        tab_Pengaturan = New TabControl()
        tab_Umum = New TabPage()
        tab_CompanyProfile = New TabPage()
        tab_Database = New TabPage()
        btn_StartdbEngine = New Button()
        btn_StopdbEngine = New Button()
        btn_RepairMySQL = New Button()
        tab_Pengaturan.SuspendLayout()
        tab_Database.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(29, 68)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(75, 15)
        Label1.TabIndex = 0
        Label1.Text = "Lokasi Server"
        ' 
        ' txt_LokasiServer
        ' 
        txt_LokasiServer.Location = New Point(121, 65)
        txt_LokasiServer.Margin = New Padding(4, 3, 4, 3)
        txt_LokasiServer.Name = "txt_LokasiServer"
        txt_LokasiServer.Size = New Size(148, 23)
        txt_LokasiServer.TabIndex = 20
        ' 
        ' btn_SimpanPerubahanKoneksiDatabase
        ' 
        btn_SimpanPerubahanKoneksiDatabase.Location = New Point(75, 242)
        btn_SimpanPerubahanKoneksiDatabase.Margin = New Padding(4, 3, 4, 3)
        btn_SimpanPerubahanKoneksiDatabase.Name = "btn_SimpanPerubahanKoneksiDatabase"
        btn_SimpanPerubahanKoneksiDatabase.Size = New Size(147, 40)
        btn_SimpanPerubahanKoneksiDatabase.TabIndex = 10000
        btn_SimpanPerubahanKoneksiDatabase.Text = "Simpan Perubahan"
        btn_SimpanPerubahanKoneksiDatabase.UseVisualStyleBackColor = True
        ' 
        ' txt_UserDatabase
        ' 
        txt_UserDatabase.Location = New Point(121, 125)
        txt_UserDatabase.Margin = New Padding(4, 3, 4, 3)
        txt_UserDatabase.Name = "txt_UserDatabase"
        txt_UserDatabase.Size = New Size(148, 23)
        txt_UserDatabase.TabIndex = 30
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(29, 128)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(30, 15)
        Label2.TabIndex = 9002
        Label2.Text = "User"
        ' 
        ' txt_PasswordDatabase
        ' 
        txt_PasswordDatabase.Location = New Point(121, 155)
        txt_PasswordDatabase.Margin = New Padding(4, 3, 4, 3)
        txt_PasswordDatabase.Name = "txt_PasswordDatabase"
        txt_PasswordDatabase.PasswordChar = "*"c
        txt_PasswordDatabase.Size = New Size(148, 23)
        txt_PasswordDatabase.TabIndex = 40
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(29, 158)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(57, 15)
        Label3.TabIndex = 9004
        Label3.Text = "Password"
        ' 
        ' txt_PortServer
        ' 
        txt_PortServer.Location = New Point(121, 95)
        txt_PortServer.Margin = New Padding(4, 3, 4, 3)
        txt_PortServer.Name = "txt_PortServer"
        txt_PortServer.Size = New Size(148, 23)
        txt_PortServer.TabIndex = 25
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(29, 98)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(29, 15)
        Label4.TabIndex = 9006
        Label4.Text = "Port"
        ' 
        ' btn_TesKoneksi
        ' 
        btn_TesKoneksi.Location = New Point(75, 195)
        btn_TesKoneksi.Margin = New Padding(4, 3, 4, 3)
        btn_TesKoneksi.Name = "btn_TesKoneksi"
        btn_TesKoneksi.Size = New Size(147, 40)
        btn_TesKoneksi.TabIndex = 900
        btn_TesKoneksi.Text = "Tes Koneksi"
        btn_TesKoneksi.UseVisualStyleBackColor = True
        ' 
        ' btn_BukaFolder
        ' 
        btn_BukaFolder.Location = New Point(229, 32)
        btn_BukaFolder.Margin = New Padding(4, 3, 4, 3)
        btn_BukaFolder.Name = "btn_BukaFolder"
        btn_BukaFolder.Size = New Size(41, 27)
        btn_BukaFolder.TabIndex = 10002
        btn_BukaFolder.Text = "..."
        btn_BukaFolder.UseVisualStyleBackColor = True
        ' 
        ' txt_FolderXAMPP
        ' 
        txt_FolderXAMPP.Location = New Point(121, 35)
        txt_FolderXAMPP.Margin = New Padding(4, 3, 4, 3)
        txt_FolderXAMPP.Name = "txt_FolderXAMPP"
        txt_FolderXAMPP.Size = New Size(100, 23)
        txt_FolderXAMPP.TabIndex = 10
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(29, 38)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(80, 15)
        Label7.TabIndex = 10001
        Label7.Text = "FolderXAMPP"
        ' 
        ' tab_Pengaturan
        ' 
        tab_Pengaturan.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tab_Pengaturan.Controls.Add(tab_Umum)
        tab_Pengaturan.Controls.Add(tab_CompanyProfile)
        tab_Pengaturan.Controls.Add(tab_Database)
        tab_Pengaturan.Location = New Point(14, 13)
        tab_Pengaturan.Margin = New Padding(4, 3, 4, 3)
        tab_Pengaturan.Name = "tab_Pengaturan"
        tab_Pengaturan.SelectedIndex = 0
        tab_Pengaturan.Size = New Size(818, 631)
        tab_Pengaturan.TabIndex = 10003
        ' 
        ' tab_Umum
        ' 
        tab_Umum.Location = New Point(4, 24)
        tab_Umum.Margin = New Padding(4, 3, 4, 3)
        tab_Umum.Name = "tab_Umum"
        tab_Umum.Size = New Size(810, 603)
        tab_Umum.TabIndex = 2
        tab_Umum.Text = "Umum"
        tab_Umum.UseVisualStyleBackColor = True
        ' 
        ' tab_CompanyProfile
        ' 
        tab_CompanyProfile.Location = New Point(4, 24)
        tab_CompanyProfile.Name = "tab_CompanyProfile"
        tab_CompanyProfile.Padding = New Padding(3)
        tab_CompanyProfile.Size = New Size(810, 644)
        tab_CompanyProfile.TabIndex = 3
        tab_CompanyProfile.Text = "Company Profile"
        tab_CompanyProfile.UseVisualStyleBackColor = True
        ' 
        ' tab_Database
        ' 
        tab_Database.Controls.Add(btn_StartdbEngine)
        tab_Database.Controls.Add(btn_StopdbEngine)
        tab_Database.Controls.Add(btn_RepairMySQL)
        tab_Database.Controls.Add(btn_BukaFolder)
        tab_Database.Controls.Add(txt_FolderXAMPP)
        tab_Database.Controls.Add(Label7)
        tab_Database.Controls.Add(Label3)
        tab_Database.Controls.Add(txt_LokasiServer)
        tab_Database.Controls.Add(txt_UserDatabase)
        tab_Database.Controls.Add(btn_TesKoneksi)
        tab_Database.Controls.Add(txt_PasswordDatabase)
        tab_Database.Controls.Add(Label1)
        tab_Database.Controls.Add(Label2)
        tab_Database.Controls.Add(txt_PortServer)
        tab_Database.Controls.Add(Label4)
        tab_Database.Controls.Add(btn_SimpanPerubahanKoneksiDatabase)
        tab_Database.Location = New Point(4, 24)
        tab_Database.Margin = New Padding(4, 3, 4, 3)
        tab_Database.Name = "tab_Database"
        tab_Database.Padding = New Padding(4, 3, 4, 3)
        tab_Database.Size = New Size(810, 644)
        tab_Database.TabIndex = 1
        tab_Database.Text = "Database"
        tab_Database.UseVisualStyleBackColor = True
        ' 
        ' btn_StartdbEngine
        ' 
        btn_StartdbEngine.Location = New Point(655, 6)
        btn_StartdbEngine.Margin = New Padding(4, 3, 4, 3)
        btn_StartdbEngine.Name = "btn_StartdbEngine"
        btn_StartdbEngine.Size = New Size(147, 40)
        btn_StartdbEngine.TabIndex = 10005
        btn_StartdbEngine.Text = "&Start dbEngine"
        btn_StartdbEngine.UseVisualStyleBackColor = True
        ' 
        ' btn_StopdbEngine
        ' 
        btn_StopdbEngine.Location = New Point(655, 52)
        btn_StopdbEngine.Margin = New Padding(4, 3, 4, 3)
        btn_StopdbEngine.Name = "btn_StopdbEngine"
        btn_StopdbEngine.Size = New Size(147, 40)
        btn_StopdbEngine.TabIndex = 10004
        btn_StopdbEngine.Text = "Sto&p dbEngine"
        btn_StopdbEngine.UseVisualStyleBackColor = True
        ' 
        ' btn_RepairMySQL
        ' 
        btn_RepairMySQL.Location = New Point(655, 98)
        btn_RepairMySQL.Margin = New Padding(4, 3, 4, 3)
        btn_RepairMySQL.Name = "btn_RepairMySQL"
        btn_RepairMySQL.Size = New Size(147, 40)
        btn_RepairMySQL.TabIndex = 10003
        btn_RepairMySQL.Text = "&Repair MySQL"
        btn_RepairMySQL.UseVisualStyleBackColor = True
        ' 
        ' frm_Pengaturan
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(846, 656)
        Controls.Add(tab_Pengaturan)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(4, 3, 4, 3)
        Name = "frm_Pengaturan"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Pengaturan"
        tab_Pengaturan.ResumeLayout(False)
        tab_Database.ResumeLayout(False)
        tab_Database.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_LokasiServer As System.Windows.Forms.TextBox
    Friend WithEvents btn_SimpanPerubahanKoneksiDatabase As System.Windows.Forms.Button
    Friend WithEvents txt_UserDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_PasswordDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_PortServer As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btn_TesKoneksi As System.Windows.Forms.Button
    Friend WithEvents fbd_Backup As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txt_FolderXAMPP As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents fbd_FolderXAMPP As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btn_BukaFolder As System.Windows.Forms.Button
    Friend WithEvents tab_Pengaturan As TabControl
    Friend WithEvents tab_Database As TabPage
    Friend WithEvents tab_Umum As TabPage
    Friend WithEvents tab_CompanyProfile As TabPage
    Friend WithEvents btn_RepairMySQL As Button
    Friend WithEvents btn_StartdbEngine As Button
    Friend WithEvents btn_StopdbEngine As Button
End Class
