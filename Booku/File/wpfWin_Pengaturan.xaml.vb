Imports bcomm
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input


Public Class wpfWin_Pengaturan

    Public FungsiForm As String
    Public AdaPerubahanForm As Boolean

    Private fbd_FolderXAMPP As New System.Windows.Forms.FolderBrowserDialog


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        AdaPerubahanForm = False

        If LevelUserAktif >= LevelUser_81_TimIT Then
            tab_Database.Visibility = Visibility.Visible
        Else
            tab_Database.Visibility = Visibility.Collapsed
        End If

        If FungsiForm = "PENGATURAN" Then
            tab_Umum.IsEnabled = True
            tab_CompanyProfile.IsEnabled = True
            LoadingTab_Umum()
            LoadingTab_CompanyProfile()
        End If

        If FungsiForm = "TES KONEKSI" Then
            tab_Umum.IsEnabled = False
            tab_CompanyProfile.IsEnabled = False
            tab_Pengaturan.SelectedItem = tab_Database
        End If

    End Sub


    '=============================== TAB : UMUM ======================================================================================

    Sub LoadingTab_Umum()
        If grid_Umum.Children.Count = 0 Then
            usc_PengaturanUmum = New wpfUsc_PengaturanUmum
            grid_Umum.Children.Add(usc_PengaturanUmum)
        End If
    End Sub


    '=============================== TAB : COMPANY PROFILE ===========================================================================

    Sub LoadingTab_CompanyProfile()
        If grid_CompanyProfile.Children.Count = 0 Then
            usc_CompanyProfile = New wpfUsc_CompanyProfile
            grid_CompanyProfile.Children.Add(usc_CompanyProfile)
        End If
    End Sub

    Sub Tutup_CompanyProfile()
        AdaPerubahanForm = False
        grid_CompanyProfile.Children.Clear()
    End Sub

    Sub LogikaFormCompanyProfile()
        If AdaPerubahanForm Then
            Pilihan = MessageBox.Show("Ada perubahan data 'Company Profile' yang belum tersimpan..!" & Enter2Baris & "Lanjutkan keluar dari halaman ini..?", "Perhatian..!", MessageBoxButton.YesNo)
            If Pilihan = MessageBoxResult.No Then
                Dispatcher.BeginInvoke(Sub() tab_Pengaturan.SelectedItem = tab_CompanyProfile)
            End If
            If Pilihan = MessageBoxResult.Yes Then Tutup_CompanyProfile()
        Else
            Tutup_CompanyProfile()
        End If
    End Sub


    Private Sub tab_Pengaturan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tab_Pengaturan.SelectionChanged
        If Not e.Source Is tab_Pengaturan Then Return

        Dim TabTerpilih As TabItem = TryCast(tab_Pengaturan.SelectedItem, TabItem)
        If TabTerpilih Is Nothing Then Return

        Select Case TabTerpilih.Name
            Case "tab_Umum"
                LoadingTab_Umum()
            Case "tab_CompanyProfile"
                LoadingTab_CompanyProfile()
            Case "tab_Database"
                LoadingTab_Database()
        End Select
    End Sub


    '=============================== TAB : DATABASE ===========================================================================

    Sub ResetTab_KoneksiDatabase()
        txt_FolderXAMPP.Text = LokasiFolderXAMPP
        txt_LokasiServer.Text = Nothing
        txt_PortServer.Text = Nothing
        txt_UserDatabase.Text = Nothing
        txt_PasswordDatabase.Password = Nothing
        btn_SimpanPerubahanKoneksiDatabase.IsEnabled = False
    End Sub


    Sub LoadingTab_Database()

        If FungsiForm = "PENGATURAN" Then
            txt_FolderXAMPP.Text = LokasiFolderXAMPP
            txt_LokasiServer.Text = LokasiServerDatabase
            txt_PortServer.Text = PortDatabase
            txt_UserDatabase.Text = UserDatabase
            txt_PasswordDatabase.Password = PasswordDatabase
        End If

        If FungsiForm = "TES KONEKSI" Then
            ResetTab_KoneksiDatabase()
        End If

    End Sub


    Private Sub btn_BukaFolder_Click(sender As Object, e As RoutedEventArgs) Handles btn_BukaFolder.Click
        If fbd_FolderXAMPP.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            txt_FolderXAMPP.Text = fbd_FolderXAMPP.SelectedPath
        End If
    End Sub


    Private Sub btn_TesKoneksi_Click(sender As Object, e As RoutedEventArgs) Handles btn_TesKoneksi.Click

        LokasiFolderXAMPP = txt_FolderXAMPP.Text
        LokasiServerDatabaseTesKoneksiDbSAT = txt_LokasiServer.Text
        PortServerTesKoneksiDbSAT = txt_PortServer.Text
        UserDatabaseTesKoneksiDbSAT = txt_UserDatabase.Text
        PasswordDatabaseTesKoneksiDbSAT = txt_PasswordDatabase.Password
        Start_dbENGINE()
        TesKoneksiDbSAT()

        If StatusKoneksiTesKoneksiDbSAT = True Then
            btn_SimpanPerubahanKoneksiDatabase.IsEnabled = True
            Pesan_Sukses("Koneksi SUKSES tersambung." & Enter2Baris & "Silakan simpan perubahan konfigurasi.")
            StatusKoneksiTesKoneksiDbSAT = False 'StatusKoneksi dikembalikan lagi ke FALSE karena harus disimpan dulu. Setelah disimpan, baru benar-benar TRUE.
        Else
            btn_SimpanPerubahanKoneksiDatabase.IsEnabled = False
            Pesan_Gagal("Koneksi GAGAL.")
        End If

    End Sub


    Private Sub txt_LokasiServer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_LokasiServer.TextChanged
        btn_SimpanPerubahanKoneksiDatabase.IsEnabled = False
    End Sub

    Private Sub txt_PortServer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PortServer.TextChanged
        btn_SimpanPerubahanKoneksiDatabase.IsEnabled = False
    End Sub

    Private Sub txt_UserDatabase_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UserDatabase.TextChanged
        btn_SimpanPerubahanKoneksiDatabase.IsEnabled = False
    End Sub

    Private Sub txt_PasswordDatabase_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles txt_PasswordDatabase.PasswordChanged
        btn_SimpanPerubahanKoneksiDatabase.IsEnabled = False
    End Sub


    Private Sub btn_SimpanPerubahanKoneksiDatabase_Click(sender As Object, e As RoutedEventArgs) Handles btn_SimpanPerubahanKoneksiDatabase.Click

        'Simpan Perubahan Konfigurasi Koneksi, ke File 0001.conf :
        DataKoneksi = HeaderConfig &
            EnkripsiTeks(LokasiFolderXAMPP) & Enter1Baris &
            EnkripsiTeks(LokasiServerDatabaseTesKoneksiDbSAT) & Enter1Baris &
            EnkripsiTeks(PortServerTesKoneksiDbSAT) & Enter1Baris &
            EnkripsiTeks(UserDatabaseTesKoneksiDbSAT) & Enter1Baris &
            EnkripsiTeks(PasswordDatabaseTesKoneksiDbSAT) &
            FooterConfig
        Try
            IO.File.WriteAllText(FilePathDataKoneksi, DataKoneksi)
            Pesan_Sukses("Konfigurasi Koneksi BERHASIL disimpan.")
            StatusKoneksiTesKoneksiDbSAT = True
        Catch ex As Exception
            Pesan_Gagal("Konfigurasi Koneksi GAGAL disimpan..!!!")
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


    Private Sub btn_StartdbEngine_Click(sender As Object, e As RoutedEventArgs) Handles btn_StartdbEngine.Click
        Start_dbENGINE()
        If Start_dbEngineBerhasil Then
            PesanSukses("dbEngine berhasil dihidupkan..!")
        Else
            PesanError("dbEngine gagal dihidupkan..!")
        End If
    End Sub


    Private Sub btn_StopdbEngine_Click(sender As Object, e As RoutedEventArgs) Handles btn_StopdbEngine.Click
        Stop_dbENGINE()
        If Stop_dbEngineBerhasil Then
            PesanSukses("dbEngine berhasil dimatikan..!")
        Else
            PesanError("dbEngine gagal dimatikan..!")
        End If
    End Sub


    Private Sub btn_RepairMySQL_Click(sender As Object, e As RoutedEventArgs) Handles btn_RepairMySQL.Click

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
        End If

        'Salin folder backup --> data :
        If ProsesRepair Then
            SalinFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "backup"), Path.Combine(LokasiFolderXAMPP, "mysql", "data"))
            LogikaTahapandanProses(SalinFolderBerhasil, ProsesRepair)
            Jeda(333)
        End If

        'Hapus ibdata1 baru :
        If ProsesRepair Then
            HapusFile(Path.Combine(LokasiFolderXAMPP, "mysql", "data", "ibdata1"))
            LogikaTahapandanProses(HapusFileBerhasil, ProsesRepair)
            Jeda(333)
        End If

        'Pindahkan ibdata1 lama (dari folder lama) ke folder baru data :
        If ProsesRepair Then
            SalinFile(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old", "ibdata1"), Path.Combine(LokasiFolderXAMPP, "mysql", "data", "ibdata1"))
            LogikaTahapandanProses(SalinFileBerhasil, ProsesRepair)
            Jeda(333)
        End If

        'Backup Data Lama (data_old) --> data_bak :
        If ProsesRepair Then
            SalinFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old"), Path.Combine(LokasiFolderXAMPP, "mysql", "data_bak"))
            LogikaTahapandanProses(SalinFolderBerhasil, ProsesRepair)
            Jeda(333)
        End If

        'Hapus hanya file saja, di Folder data_old :
        If ProsesRepair Then
            HapusHanyaFileDalamFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old"))
            LogikaTahapandanProses(HapusHanyaFiledalamFolderBerhasil, ProsesRepair)
            Jeda(333)
        End If

        'Hapus folder test di folder data_old :
        If ProsesRepair Then
            HapusFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old", "test"))
            LogikaTahapandanProses(HapusFolderBerhasil, ProsesRepair)
            Jeda(333)
        End If

        'Hapus folder phpmyadmin di folder data_old :
        If ProsesRepair Then
            HapusFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old", "phpmyadmin"))
            LogikaTahapandanProses(HapusFolderBerhasil, ProsesRepair)
            Jeda(333)
        End If

        'Hapus folder performance_schema di folder data_old :
        If ProsesRepair Then
            HapusFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old", "performance_schema"))
            LogikaTahapandanProses(HapusFolderBerhasil, ProsesRepair)
            Jeda(333)
        End If

        'Salin folder data_old ke folder data :
        If ProsesRepair Then
            SalinFolder(Path.Combine(LokasiFolderXAMPP, "mysql", "data_old"), Path.Combine(LokasiFolderXAMPP, "mysql", "data"))
            LogikaTahapandanProses(SalinFolderBerhasil, ProsesRepair)
            Jeda(333)
        End If

        If ProsesRepair Then
            PesanSukses("Repair MySQL berhasil..!")
            Start_dbENGINE()
        Else
            PesanSukses("Repair MySQL gagal..!")
        End If

    End Sub


    Private Sub TutupForm(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        Pilihan = MessageBoxResult.Yes
        LogikaFormCompanyProfile()
        If Pilihan = MessageBoxResult.No Then e.Cancel = True
        If Pilihan = MessageBoxResult.Yes Then e.Cancel = False
    End Sub


    '=============================== WINDOW CONTROLS ===========================================================================

    Private Sub Header_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        Me.DragMove()
    End Sub


    Private Sub btn_Close_Click(sender As Object, e As RoutedEventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
    End Sub

End Class
