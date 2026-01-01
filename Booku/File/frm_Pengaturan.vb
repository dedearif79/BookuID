Imports bcomm
Imports System.IO
Imports System.Security.Policy
Imports System.Windows.Forms.Integration


Public Class frm_Pengaturan

    Public FungsiForm
    Public AdaPerubahanForm

    Private Sub frm_Pengaturan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AdaPerubahanForm = False

        If LevelUserAktif >= LevelUser_81_TimIT Then
            tab_Database.Visible = True
        Else
            tab_Database.Visible = False
        End If

        If FungsiForm = "PENGATURAN" Then
            tab_Umum.Enabled = True
            tab_CompanyProfile.Enabled = True
            LoadingTab_CompanyProfile()
        End If

        If FungsiForm = "TES KONEKSI" Then
            tab_Umum.Enabled = False
            tab_CompanyProfile.Enabled = False
            tab_Pengaturan.SelectedTab = tab_Database
        End If

    End Sub


    '=============================== TAB : UMUM ======================================================================================

    Private Sub tab_Umum_Click(sender As Object, e As EventArgs) Handles tab_Umum.Click
    End Sub
    Private Sub tab_Umum_Enter(sender As Object, e As EventArgs) Handles tab_Umum.Enter
        LoadingTab_Umum()
    End Sub

    Sub LoadingTab_Umum()

    End Sub

    '=============================== TAB : COMPANY PROFILE ===========================================================================

    Sub LoadingTab_CompanyProfile()
        usc_CompanyProfile = New wpfUsc_CompanyProfile
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_CompanyProfile
        tab_CompanyProfile.Controls.Add(host)
    End Sub

    Sub Tutup_CompanyProfile()
        AdaPerubahanForm = False
        tab_CompanyProfile.Controls.Clear()
    End Sub

    Sub LogikaFormCompanyProfile()
        If AdaPerubahanForm Then
            Pilihan = MessageBox.Show("Ada perubahan data 'Company Profile' yang belum tersimpan..!" & Enter2Baris & "Lanjutkan keluar dari halaman ini..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then BeginInvoke(Sub() tab_Pengaturan.SelectedTab = tab_CompanyProfile)
            If Pilihan = vbYes Then Tutup_CompanyProfile()
        Else
            Tutup_CompanyProfile()
        End If
    End Sub


    Private Sub tab_CompanyProfile_Click(sender As Object, e As EventArgs) Handles tab_CompanyProfile.Click
    End Sub
    Private Sub tab_CompanyProfile_Enter(sender As Object, e As EventArgs) Handles tab_CompanyProfile.Enter
        LoadingTab_CompanyProfile()
    End Sub
    Private Sub tab_CompanyProfile_Leave(sender As Object, e As EventArgs) Handles tab_CompanyProfile.Leave
        LogikaFormCompanyProfile()
    End Sub



    '=============================== TAB : DATABASE ===========================================================================

    Sub ResetTab_KoneksiDatabase()
        txt_FolderXAMPP.Text = LokasiFolderXAMPP
        txt_LokasiServer.Text = Nothing
        txt_PortServer.Text = Nothing
        txt_UserDatabase.Text = Nothing
        txt_PasswordDatabase.Text = Nothing
        btn_SimpanPerubahanKoneksiDatabase.Enabled = False
    End Sub


    Sub LoadingTab_Database()

        If FungsiForm = "PENGATURAN" Then
            txt_FolderXAMPP.Text = LokasiFolderXAMPP
            txt_LokasiServer.Text = LokasiServerDatabase
            txt_PortServer.Text = PortDatabase
            txt_UserDatabase.Text = UserDatabase
            txt_PasswordDatabase.Text = PasswordDatabase
        End If

        If FungsiForm = "TES KONEKSI" Then
            ResetTab_KoneksiDatabase()
        End If

    End Sub


    Private Sub tab_Database_Click(sender As Object, e As EventArgs) Handles tab_Database.Click
    End Sub
    Private Sub tab_Database_Enter(sender As Object, e As EventArgs) Handles tab_Database.Enter
        LoadingTab_Database()
    End Sub

    Private Sub btn_BukaFolder_Click(sender As Object, e As EventArgs) Handles btn_BukaFolder.Click
        If fbd_FolderXAMPP.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            txt_FolderXAMPP.Text = fbd_FolderXAMPP.SelectedPath
        End If
    End Sub


    Private Sub btn_TesKoneksi_Click(sender As Object, e As EventArgs) Handles btn_TesKoneksi.Click

        LokasiFolderXAMPP = txt_FolderXAMPP.Text
        LokasiServerDatabaseTesKoneksiDbSAT = txt_LokasiServer.Text
        PortServerTesKoneksiDbSAT = txt_PortServer.Text
        UserDatabaseTesKoneksiDbSAT = txt_UserDatabase.Text
        PasswordDatabaseTesKoneksiDbSAT = txt_PasswordDatabase.Text
        Start_dbENGINE()
        TesKoneksiDbSAT()

        If StatusKoneksiTesKoneksiDbSAT = True Then
            btn_SimpanPerubahanKoneksiDatabase.Enabled = True
            MsgBox("Koneksi SUKSES tersambung." & Enter2Baris & "Silakan simpan perubahan konfigurasi.")
            StatusKoneksiTesKoneksiDbSAT = False 'StatusKoneksi dikembalikan lagi ke FALSE karena harus disimpan dulu. Setelah disimpan, baru benar-benar TRUE.
        Else
            btn_SimpanPerubahanKoneksiDatabase.Enabled = False
            MsgBox("Koneksi GAGAL.")
        End If

    End Sub

    Private Sub txt_LokasiServer_TextChanged(sender As Object, e As EventArgs) Handles txt_LokasiServer.TextChanged
        btn_SimpanPerubahanKoneksiDatabase.Enabled = False
    End Sub

    Private Sub txt_PortServer_TextChanged(sender As Object, e As EventArgs) Handles txt_PortServer.TextChanged
        btn_SimpanPerubahanKoneksiDatabase.Enabled = False
    End Sub

    Private Sub txt_UserDatabase_TextChanged(sender As Object, e As EventArgs) Handles txt_UserDatabase.TextChanged
        btn_SimpanPerubahanKoneksiDatabase.Enabled = False
    End Sub

    Private Sub txt_PasswordDatabase_TextChanged(sender As Object, e As EventArgs) Handles txt_PasswordDatabase.TextChanged
        btn_SimpanPerubahanKoneksiDatabase.Enabled = False
    End Sub

    Private Sub btn_SimpanPerubahanKoneksiDatabase_Click(sender As Object, e As EventArgs) Handles btn_SimpanPerubahanKoneksiDatabase.Click

        'Simpan Perubahan Konfigurasi Koneksi, ke File 0001.conf :
        DataKoneksi = HeaderConfig &
            EnkripsiTeks(LokasiFolderXAMPP) & Enter1Baris &
            EnkripsiTeks(LokasiServerDatabaseTesKoneksiDbSAT) & Enter1Baris &
            EnkripsiTeks(PortServerTesKoneksiDbSAT) & Enter1Baris &
            EnkripsiTeks(UserDatabaseTesKoneksiDbSAT) & Enter1Baris &
            EnkripsiTeks(PasswordDatabaseTesKoneksiDbSAT) &
            FooterConfig
        Try
            My.Computer.FileSystem.WriteAllText(FilePathDataKoneksi, DataKoneksi, False)
            MsgBox("Konfigurasi Koneksi BERHASIL disimpan.")
            StatusKoneksiTesKoneksiDbSAT = True
        Catch ex As Exception
            MsgBox("Konfigurasi Koneksi GAGAL disimpan..!!!")
            StatusKoneksiTesKoneksiDbSAT = False
        End Try

        'Perbarui Value dari Variabel2 Database :
        LokasiServerDatabase = LokasiServerDatabaseTesKoneksiDbSAT
        PortDatabase = PortServerTesKoneksiDbSAT
        UserDatabase = UserDatabaseTesKoneksiDbSAT
        PasswordDatabase = PasswordDatabaseTesKoneksiDbSAT
        UserDatabaseTesKoneksiMySQL = UserDatabaseTesKoneksiDbSAT
        PasswordDatabaseTesKoneksiMySQL = PasswordDatabaseTesKoneksiDbSAT
        PerbaruiVariabelTerkaitServer()

        KonfigurasiKoneksiDatabaseSudahTersimpan = True

        'Konfigurasi ulang dsn Database General dan Database Transaksi :
        BuatDsnGeneral()
        If TahunBukuAktif <> Nothing Then BuatDsnTransaksi(TahunBukuAktif)

        If FungsiForm = "TES KONEKSI" Then
            Me.Close()
        Else
            AksesDatabase_General(Buka) 'Ini untuk tes koneksi
            AksesDatabase_General(Tutup)
        End If

    End Sub


    Private Sub btn_StartdbEngine_Click(sender As Object, e As EventArgs) Handles btn_StartdbEngine.Click
        Start_dbENGINE()
        If Start_dbEngineBerhasil Then
            PesanSukses("dbEngine berhasil dihidupkan..!")
        Else
            PesanError("dbEngine gagal dihidupkan..!")
        End If
    End Sub

    Private Sub btn_StopdbEngine_Click(sender As Object, e As EventArgs) Handles btn_StopdbEngine.Click
        Stop_dbENGINE()
        If Stop_dbEngineBerhasil Then
            PesanSukses("dbEngine berhasil dimatikan..!")
        Else
            PesanError("dbEngine gagal dimatikan..!")
        End If
    End Sub


    Private Sub btn_RepairMySQL_Click(sender As Object, e As EventArgs) Handles btn_RepairMySQL.Click

        Dim ProsesRepair As Boolean = True

        Stop_dbENGINE()
        Jeda(5555)
        LogikaTahapandanProses(Stop_dbEngineBerhasil, ProsesRepair)

        'Bersihkan dulu sisa folder backup :
        HapusFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old"))
        HapusFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_bak"))

        'Rename folder data --> data_old :
        If ProsesRepair Then
            RenameFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data"), Path.Combine(LokasiFolderXAMPP, "mysql", "data_old"))
            LogikaTahapandanProses(RenameFolderBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Rename folder data --> data_old : " & ProsesRepair)
        End If

        'Salin folder backup --> data :
        If ProsesRepair Then
            SalinFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "backup"), Path.Combine(LokasiFolderXAMPP, "mysql", "data"))
            LogikaTahapandanProses(SalinFolderBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Salin folder backup --> data : " & ProsesRepair)
        End If

        'Hapus ibdata1 baru :
        If ProsesRepair Then
            HapusFile(Path.Combine(LokasiFolderXAMPP, "mysql", "data", "ibdata1"))
            LogikaTahapandanProses(HapusFileBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Hapus ibdata1 baru : " & ProsesRepair)
        End If

        'Pindahkan ibdata1 lama (dari folder lama) ke folder baru data :
        If ProsesRepair Then
            SalinFile(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old", "ibdata1"), Path.Combine(LokasiFolderXAMPP, "mysql", "data", "ibdata1"))
            LogikaTahapandanProses(SalinFileBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Pindahkan ibdata1 lama (dari folder lama) ke folder baru data : " & ProsesRepair)
        End If

        'Backup Data Lama (data_old) --> data_bak :
        If ProsesRepair Then
            SalinFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old"), Path.Combine(LokasiFolderXAMPP, "mysql", "data_bak"))
            LogikaTahapandanProses(SalinFolderBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Backup Data Lama (data_old) --> data_bak : " & ProsesRepair)
        End If

        'Hapus hanya file saja, di Folder data_old :
        If ProsesRepair Then
            HapusHanyaFileDalamFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old"))
            LogikaTahapandanProses(HapusHanyaFiledalamFolderBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Hapus hanya file saja, di Folder data_old : " & ProsesRepair)
        End If

        'Hapus folder test di folder data_old :
        If ProsesRepair Then
            HapusFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old", "test"))
            LogikaTahapandanProses(HapusFolderBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Hapus folder test di folder data_old : " & ProsesRepair)
        End If

        'Hapus folder phpmyadmin di folder data_old :
        If ProsesRepair Then
            HapusFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old", "phpmyadmin"))
            LogikaTahapandanProses(HapusFolderBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Hapus folder phpmyadmin di folder data_old : " & ProsesRepair)
        End If

        'Hapus folder performance_schema di folder data_old :
        If ProsesRepair Then
            HapusFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old", "performance_schema"))
            LogikaTahapandanProses(HapusFolderBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Hapus folder performance_schema di folder data_old : " & ProsesRepair)
        End If

        'Salin folder data_old ke folder data :
        If ProsesRepair Then
            SalinFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old"), Path.Combine(LokasiFolderXAMPP, "mysql", "data"))
            LogikaTahapandanProses(SalinFolderBerhasil, ProsesRepair)
            Jeda(333)
            'PesanUntukProgrammer("Salin folder data_old ke folder data : " & ProsesRepair)
        End If

        If ProsesRepair Then
            PesanSukses("Repair MySQL berhasil..!")
            Start_dbENGINE()
        Else
            PesanSukses("Repair MySQL gagal..!")
        End If

    End Sub


    Private Sub TutupForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Pilihan = vbYes
        LogikaFormCompanyProfile()
        If Pilihan = vbNo Then e.Cancel = True
        If Pilihan = vbYes Then e.Cancel = False
    End Sub

End Class