Imports Org.BouncyCastle.Asn1.Cmp
Imports System.IO
Imports bcomm

Public Module mdl_InstallXAMPP


    Private xamppPath As String = "C:\BookuID\BookuXAMPP"
    Private zipFilePath As String = "C:\BookuID\TempInstaller\xampp82120.zip" ' Sesuaikan lokasi file ZIP XAMPP


    Async Function InstallXAMPP(pgb_Progress As ProgressBar, lbl_Progress As TextBlock) As Task

        If Not File.Exists(zipFilePath) Then
            MessageBox.Show("File XAMPP tidak ditemukan! Pastikan ada di: " & zipFilePath)
            Exit Function
        End If
        lbl_Progress.Text = "Menginstal XAMPP, harap tunggu..."

        Await EkstrakFile(zipFilePath, xamppPath, pgb_Progress, lbl_Progress)

        ConfigureApache()
        ConfigureMySQL()
        ConfigureFileZilla()

        'StartXampp()

        lbl_Progress.Text = "Instalasi selesai!"
        MsgBox("XAMPP berhasil diinstal di: " & xamppPath)

    End Function


    Private Sub ConfigureApache()
        Dim httpdConfPath As String = Path.Combine(xamppPath, "apache", "conf", "httpd.conf")
        Dim httpdSslConfPath As String = Path.Combine(xamppPath, "apache", "conf", "extra", "httpd-ssl.conf")

        If File.Exists(httpdConfPath) Then
            Dim content As String = File.ReadAllText(httpdConfPath)
            content = content.Replace("Listen 80", "Listen 87")
            content = content.Replace("ServerName localhost:80", "ServerName localhost:87")
            File.WriteAllText(httpdConfPath, content)
        End If

        If File.Exists(httpdSslConfPath) Then
            Dim content As String = File.ReadAllText(httpdSslConfPath)
            content = content.Replace("Listen 443", "Listen 999")
            File.WriteAllText(httpdSslConfPath, content)
        End If
    End Sub

    Private Sub ConfigureMySQL()
        Dim myIniPath As String = Path.Combine(xamppPath, "mysql", "bin", "my.ini")

        If File.Exists(myIniPath) Then
            Dim content As String = File.ReadAllText(myIniPath)
            content = content.Replace("port=3306", "port=3399")
            File.WriteAllText(myIniPath, content)
        End If
    End Sub

    Private Sub ConfigureFileZilla()
        Dim fileZillaConfigPath As String = Path.Combine(xamppPath, "FileZillaFTP", "FileZilla Server.xml")

        If File.Exists(fileZillaConfigPath) Then
            Dim content As String = File.ReadAllText(fileZillaConfigPath)
            content = content.Replace("<Port>21</Port>", "<Port>27</Port>")
            File.WriteAllText(fileZillaConfigPath, content)
        End If
    End Sub

    Private Sub StartXampp()
        Try
            Process.Start(Path.Combine(xamppPath, "apache_start.bat"))
            Process.Start(Path.Combine(xamppPath, "mysql_start.bat"))
        Catch ex As Exception
            MessageBox.Show("Gagal menjalankan XAMPP: " & ex.Message)
        End Try
    End Sub

End Module
