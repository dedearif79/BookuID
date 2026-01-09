Imports System.Data.Odbc
Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Windows
Imports bcomm

Public Class frm_BOOKU

    Private Sub frm_BOOKU_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Parameter Awal :
        FilePathDataKoneksi = Path.Combine(FolderNotesApp, NamaFileDataKoneksi)
        FilePathRegistrasiPerangkat = Path.Combine(FolderNotesApp, NamaFileRegistrasiPerangkat)
        FilePathRegistrasiPerangkat_Backup = Path.Combine(FolderNotesApp, NamaFileRegistrasiPerangkat_Backup)
        FilePathVersiDanApdetAplikasi = Path.Combine(FolderNotesApp, NamaFileVersiDanApdetAplikasi)

        'Standarisasi Settingan :
        StandarisasiSetinganAplikasi()

        'Start Up
        win_Startup = New wpfWin_StartUp
        win_Startup.ShowDialog()

        pnl_Notifikasi.Visible = False
        VisibilitasNotifikasi = False

        'Lakukan Update Value tbl_infoaplikasi yang ada di lokal, dengan value-value yang ada di server
        UpdateInfoAplikasi()


        'Pengisian Value dari Variabel-variabel Penting di Awal :
        DataAwalLoadingAplikasi()


        'Posisi Default Hak Akses User :
        StatusMenuPosisiLogout()


        'Cek Versi dan Apdet Aplikasi :
        CekVersiDanApdetAplikasi()


        'Cek Status Registrasi Perangkat :
        CekStatusRegistrasiPerangkat()

        Style_HalamanModul(Me)

    End Sub
    Public Sub New()
        InitializeComponent()

        '' WinForms global exception handlers
        'System.Windows.Forms.Application.SetUnhandledExceptionMode(
        '    System.Windows.Forms.UnhandledExceptionMode.CatchException)

        AddHandler System.Windows.Forms.Application.ThreadException, AddressOf WinForms_ThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf Domain_UnhandledException
        AddHandler System.Threading.Tasks.TaskScheduler.UnobservedTaskException, AddressOf Task_UnobservedTaskException

        ' WPF dispatcher (berguna untuk exception di WPF UserControl)
        AddHandler System.Windows.Threading.Dispatcher.CurrentDispatcher.UnhandledException,
            AddressOf Wpf_DispatcherUnhandledException
    End Sub

    Private Sub WinForms_ThreadException(sender As Object, e As System.Threading.ThreadExceptionEventArgs)
        HandleCrash(e.Exception, "WinForms ThreadException")
    End Sub

    Private Sub Domain_UnhandledException(sender As Object, e As System.UnhandledExceptionEventArgs)
        HandleCrash(TryCast(e.ExceptionObject, Exception), "AppDomain UnhandledException")
    End Sub

    Private Sub Task_UnobservedTaskException(sender As Object, e As System.Threading.Tasks.UnobservedTaskExceptionEventArgs)
        HandleCrash(e.Exception, "Task UnobservedTaskException")
        e.SetObserved()
    End Sub

    Private Sub Wpf_DispatcherUnhandledException(sender As Object,
        e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs)

        HandleCrash(e.Exception, "WPF DispatcherUnhandledException")
        e.Handled = True
    End Sub

    Private Sub HandleCrash(ex As Exception, source As String)
        Dim logPath As String = ""

        Try
            logPath = mdl_Logger.WriteException(ex, source)
        Catch
        End Try

        Try
            System.Windows.Forms.MessageBox.Show(
                "Terjadi kesalahan pada aplikasi BOOKU." & vbCrLf &
                "Silakan kirim file log berikut ke admin:" & vbCrLf &
                logPath,
                "BOOKU - Error",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error
            )
        Catch
        End Try

        Environment.Exit(1)
    End Sub


    Sub UpdateInfoAplikasi()

        If AdaInfoUdate = True Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_infoaplikasi SET " &
                                  " Nama_Aplikasi = '" & NamaAplikasi & "', " &
                                  " Nomor_Hotline = '" & NomorHotLine & "', " &
                                  " Website = '" & WebsiteAplikasi & "', " &
                                  " Email = '" & EmailAplikasi & "' " _
                                  , KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            AksesDatabase_General(Tutup)
        Else
            AksesDatabase_General(Buka)
            Try
                cmd = New OdbcCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabaseGeneral)
                dr = cmd.ExecuteReader
                dr.Read()
                NamaAplikasi = dr.Item("Nama_Aplikasi")
                EmailAplikasi = dr.Item("Email")
                NomorHotLine = dr.Item("Nomor_Hotline")
                VersiBooku_SisiDatabase = DekripsiAngkaAES1(dr.Item("Versi_App"))
                ApdetBooku_SisiDatabase = DekripsiAngkaAES1(dr.Item("Apdet_App"))
                'Value dari variabel WebsiteAplikasi jangan ngambil dari sini. Sudah langsung ditanam sejak awal saat deklarasi variabel.
            Catch ex As Exception
            End Try
            AksesDatabase_General(Tutup)
        End If

    End Sub

    Sub CekVersiDanApdetAplikasi()

        If StatusKoneksiDatabasePublic = True Then
            '---------------------------------------
            'Cek Versi dan Apdet, dari SISI PUBLIC :
            BukaDatabasePublic()
            Try
                cmdPublic = New MySqlCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabasePublic)
                drPublic = cmdPublic.ExecuteReader
                drPublic.Read()
                VersiBooku_SisiPublic = drPublic.Item("Versi_App")
                ApdetBooku_SisiPublic = drPublic.Item("Apdet_App")
                urlPaketInstallerPublic = drPublic.Item("URL_Paket_Installer")
                urlPaketUpdaterPublic = drPublic.Item("URL_Paket_Updater")
                FolderTempInstaller = Path.Combine(FolderRootBookuID, drPublic.Item("Folder_Temp_Paket_Installer"))
                FolderTempUpdater = Path.Combine(FolderRootBookuID, drPublic.Item("Folder_Temp_Paket_Updater"))
                FilePathInstallerLokal = Path.Combine(FolderTempInstaller, drPublic.Item("File_Installer"))
                FilePathUpdaterLokal = Path.Combine(FolderTempUpdater, drPublic.Item("File_Updater"))
                FilePathPaketInstallerLokal = Path.Combine(FolderTempInstaller, drPublic.Item("File_Paket_Installer"))
                FilePathPaketUpdaterLokal = Path.Combine(FolderTempUpdater, drPublic.Item("File_Paket_Updater"))
                AdaInfoUdate = True
            Catch ex As Exception
                AdaInfoUdate = False
            End Try
            TutupDatabasePublic()
        End If


        HapusFolder(FolderTempUpdater)

        '--------------------------------------
        'Cek Versi dan Apdet, dari SISI LOKAL :
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        VersiBooku_SisiDatabase = DekripsiAngkaAES1(dr.Item("Versi_App"))
        ApdetBooku_SisiDatabase = DekripsiAngkaAES1(dr.Item("Apdet_App"))
        AksesDatabase_General(Tutup)

        '--------------------------------------
        'Kirim Info ke Public :
        BukaDatabasePublic()
        If StatusKoneksiDatabasePublic = True Then
            cmdPublic = New MySqlCommand(" UPDATE tbl_customer SET Apdet_App = '" & ApdetBooku_SisiDatabase & "' " &
                                     " WHERE ID_Customer = '" & ID_Customer & "' ", KoneksiDatabasePublic)
            cmdPublic_ExecuteNonQuery()
            TutupDatabasePublic()
        End If

        '------------------------------------------
        'Cek Versi dan Apdet, dari SISI APLIKASI :
        Try
            DataVersiDanApdetAplikasi = File.ReadLines(FilePathVersiDanApdetAplikasi)
        Catch ex As Exception
            Pesan_Gagal("Sistem terkunci." & Enter2Baris &
                   "File " & NamaFileVersiDanApdetAplikasi & " rusak." & Enter2Baris &
                   "Silakan hubungi Developer untuk mengatasi masalah ini.")
            End
        End Try
        Dim i = 0
        For Each Baris In DataVersiDanApdetAplikasi
            i += 1
            If i = 5 Then VersiBooku_SisiAplikasi = DekripsiTeksAES1(Baris)
            If i = 8 Then ApdetBooku_SisiAplikasi = DekripsiTeksAES1(Baris)
        Next

        If VersiBooku_SisiAplikasi = teks_DekripsiTeksGagal _
            Or ApdetBooku_SisiAplikasi = teks_DekripsiTeksGagal Then
            Pesan_Gagal("Sistem terkunci." & Enter2Baris &
                   "File " & NamaFileVersiDanApdetAplikasi & " rusak." & Enter2Baris &
                   "Silakan hubungi Developer untuk mengatasi masalah ini.")
            End
        End If

        'BypassUpdater() 'Ini Sub untuk mem-byPass Logika Updater...!

        If AdaInfoUdate = True Then
            If ApdetBooku_SisiAplikasi <> ApdetBooku_SisiPublic Then
                win_Updater = New wpfWin_Updater
                win_Updater.ShowDialog()
                If ProsesUpdate_Aplikasi = False Then
                    Pesan_Peringatan("Proses update gagal." & Enter2Baris &
                                    "Aplikasi tetap dijalankan dengan versi yang belum diperbarui.")
                    HapusFolder(FolderTempUpdater)
                End If
            End If
        End If

        'Jika sisi Database belum cocok dengan Update Sisi Aplikasi :
        If ApdetBooku_SisiDatabase <> ApdetBooku_SisiAplikasi Then UpdateDatabase()

    End Sub

    Sub CekStatusRegistrasiPerangkat()

        'Cek Status Registrasi (Verifikasi Tahap 1 : Dari sisi Perangkat )
        StatusRegistrasiPerangkat = False
        Try
            DataRegistrasiPerangkat = IO.File.ReadLines(Path.Combine(FilePathRegistrasiPerangkat))
            FileEksis = True
        Catch ex As Exception
            My.Computer.FileSystem.WriteAllText(Path.Combine(FilePathRegistrasiPerangkat), "", False)
            FileEksis = False
        End Try
        Dim Terdaftar = Nothing
        Dim ID_CPU_Tercatat = Nothing
        If FileEksis = True Then
            Dim i = 0
            For Each Baris In DataRegistrasiPerangkat
                i += 1
                If i = 9 Then Terdaftar = DekripsiTeks(Baris)
                If i = 17 Then ID_CPU_Tercatat = DekripsiTeks(Baris)
            Next
        End If
        If Terdaftar = "TERDAFTAR" And ID_CPU = ID_CPU_Tercatat Then
            StatusRegistrasiPerangkat = True
        Else
            StatusRegistrasiPerangkat = False
        End If

        'Cek Status Registrasi (Verifikasi Tahap 2 : Dari sisi Database General / tbl_perangkat )
        If StatusRegistrasiPerangkat = True Then
            StatusRegistrasiPerangkat = False 'Sengaja dibikin false lagi, untuk verifikasi tahap 2
            Dim List_ID_Computer = Nothing
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_perangkat ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Do While dr.Read
                List_ID_Computer = DekripsiTeks(dr.Item("Kode_Khusus")) 'Ini Sebenarnya untuk mengambil value ID_Komputer
                If ID_CPU = List_ID_Computer Then
                    StatusRegistrasiPerangkat = True
                    Exit Do
                End If
            Loop
            AksesDatabase_General(Tutup)
        End If

        If StatusRegistrasiPerangkat = True Then
            BukaFormLogin()
        Else
            'Halaman Registrasi
            Pesan_Informasi("Perangkat Anda belum terdaftar untuk menggunakan aplikasi ini." & Enter2Baris & "Silakan mendaftar terlebih dahulu.")
            BukaDatabasePublic()
            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_customer WHERE Nomor_Seri_Produk = '" & NomorSeriProduk & "' ", KoneksiDatabasePublic)
            drPublic = cmdPublic.ExecuteReader
            drPublic.Read()
            Dim JumlahPerangkat = 0
            If drPublic.HasRows Then
                Try
                    ID_Customer = drPublic.Item("ID_Customer")
                    JumlahPerangkat = drPublic.Item("Jumlah_Perangkat")
                    ProsesRegistrasiPerangkat = True
                Catch ex As Exception
                    ProsesRegistrasiPerangkat = False
                    Pesan_Gagal("Registrasi perangkat gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
                End Try
            End If
            TutupDatabasePublic()
            If ProsesRegistrasiPerangkat = True Then
                frm_RegistrasiPerangkat.ResetForm()
                frm_RegistrasiPerangkat.txt_NomorSeriProduk.Text = NomorSeriProduk
                frm_RegistrasiPerangkat.txt_IDCustomer.Text = ID_Customer
                frm_RegistrasiPerangkat.txt_JumlahPerangkat.Text = JumlahPerangkat
                frm_RegistrasiPerangkat.txt_IDKomputer.Text = ID_CPU
                frm_RegistrasiPerangkat.ShowDialog()
            End If
            If ProsesRegistrasiPerangkat = True Then
                Pesan_Sukses("Proses registrasi perangkat berhasil.")
                BukaFormLogin()
            Else
                End
            End If
        End If

    End Sub

    Sub BukaFormLogin()

        win_Login = New wpfWin_Login
        win_Login.ResetForm()
        win_Login.ShowDialog()

        If StatusLogin = True Then

            'Form Masuk Tahun Buku :
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabaseGeneral = True Then
                If TahunBukuAktif = Nothing Then
                    cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ORDER BY Tahun_Buku ", KoneksiDatabaseGeneral)
                    dr = cmd.ExecuteReader
                    Do While dr.Read 'Loop ini untuk mengambil value Tahun Buku yang paling akhir.
                        TahunBukuBaru = dr.Item("Tahun_Buku")
                    Loop
                    If dr.HasRows Then
                        AksesDatabase_General(Tutup)
                        If LevelUserAktif >= LevelUser_81_TimIT Then 'Hanya User Level IT ke atas yang bisa gonta-ganti Tahun Buku.
                            win_GantiTahunBuku = New wpfWin_GantiTahunBuku
                            win_GantiTahunBuku.FungsiForm = FungsiForm_MASUKTAHUNBUKU
                            win_GantiTahunBuku.ShowDialog()
                        Else
                            win_GantiTahunBuku = New wpfWin_GantiTahunBuku
                            win_GantiTahunBuku.FungsiForm = FungsiForm_EksekusiSub_PROSESGANTITAHUNBUKU
                            BeginInvoke(Sub() Pesan_Informasi("Anda memasuki Tahun Buku " & TahunBukuAktif & "."))
                            win_GantiTahunBuku.ShowDialog()
                        End If
                    Else
                        Pesan_Informasi("Anda belum memiliki Database Transaksi untuk dikelola." _
                               & Enter2Baris & "Silakan buat database terlebih dahulu yang akan dipandu oleh aplikasi ini.")
                        win_BuatDatabaseBukuBaru = New wpfWin_BuatDatabaseBukuBaru
                        win_BuatDatabaseBukuBaru.ResetForm()
                        win_BuatDatabaseBukuBaru.txt_TahunBuku.IsEnabled = False
                        win_BuatDatabaseBukuBaru.cmb_JenisTahunBuku.IsEnabled = False
                        win_BuatDatabaseBukuBaru.txt_TahunBuku.Text = TahunCutOff
                        win_BuatDatabaseBukuBaru.cmb_JenisTahunBuku.SelectedValue = JenisTahunBuku_LAMPAU
                        win_BuatDatabaseBukuBaru.ShowDialog()
                        AksesDatabase_General(Tutup)
                    End If
                End If
                If TahunBukuAktif = Nothing Then
                    LoginGagal() 'Ini penting, untuk mencegah user masuk log/applikasi tanpa memilih Tahun Buku.
                    Pesan_Peringatan("Login dibatalkan karena Anda tidak memilih Tahun Buku untuk dikelola.")
                Else
                    Me.Text = JudulAplikasi
                End If
            Else
                LoginGagal()
            End If
            AksesDatabase_General(Tutup)
        Else
            LoginGagal()
        End If

    End Sub

    Sub MenuIniMasihDalamPengembangan()
        Pesan_Informasi("Menu ini masih dalam pengembangan.")
    End Sub




    '==============================================================================================================================
    '============================================== D A F T A R   M E N U  ========================================================
    '==============================================================================================================================



    'KELOMPOK MENU : FILE =========================================================================================================


    'SUB MENU : PENGATURAN ------------------------------------------------------------------------
    Private Sub mnu_Pengaturan_Click(sender As Object, e As EventArgs) Handles mnu_Pengaturan.Click
        win_Pengaturan = New wpfWin_Pengaturan
        win_Pengaturan.FungsiForm = "PENGATURAN"
        win_Pengaturan.tab_Pengaturan.SelectedIndex = 0
        win_Pengaturan.ShowDialog()
    End Sub


    'SUB MENU : BACKUP DATABASE ---------------------------------------------------------------------------

    'Cadangkan Database :
    Private Sub mnu_Database_Cadangkan_Click(sender As Object, e As EventArgs) Handles mnu_Database_Cadangkan.Click
        Dim PesanPertanyaan As String = "Anda akan melakukan pencadangan database."
        If Not TanyaKonfirmasi(PesanPertanyaan & Enter2Baris & "Lanjutkan?") Then Return
        win_BackupData = New wpfWin_BackupData
        win_BackupData.ResetForm()
        win_BackupData.ShowDialog()
    End Sub

    'Pulihkan Database :
    Private Sub mnu_Database_Pulihkan_Click(sender As Object, e As EventArgs) Handles mnu_Database_Pulihkan.Click
        win_PulihkanData = New wpfWin_PulihkanData
        win_PulihkanData.ResetForm()
        win_PulihkanData.ShowDialog()
    End Sub

    'Kloning Database :
    Private Sub mnu_Database_Kloning_Click(sender As Object, e As EventArgs) Handles mnu_Database_Kloning.Click
        win_KloningData = New wpfWin_KloningData
        win_KloningData.ResetForm()
        win_KloningData.ShowDialog()
        If win_KloningData.KloningDatabaseBerhasil Then
            PaksaKeluarAplikasi = True
            Close()
        End If
    End Sub


    'SUB MENU : KELUAR --------------------------------------------------------------------
    Sub Mnu_Keluar_Click(sender As Object, e As EventArgs) Handles mnu_Keluar.Click
        Close()
    End Sub




    'KELOMPOK MENU : DATA =========================================================================================================

    'SUB MENU : COMPANY PROFILE ---------------------------------------------------------------------------
    Private Sub mnu_CompanyProfile_Click(sender As Object, e As EventArgs) Handles mnu_CompanyProfile.Click
        BukaPengaturanCompanyProfile()
    End Sub

    'SUB MENU : DATA AWAL  ---------------------------------------------------------------------------

    'DATA AWAL - Hutang :

    'DATA AWAL - Hutang - Hutang Usaha :
    Private Sub mnu_DataAwal_HutangUsaha_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha.Click
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_NonAfiliasi_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Afiliasi_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Impor_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Impor.Click
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Impor_USD_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_USD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Impor_AUD_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_AUD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_AUD()
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Impor_JPY_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_JPY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_JPY()
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Impor_CNY_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_CNY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_CNY()
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Impor_EUR_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_EUR.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_EUR()
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Impor_SGD_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_SGD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_SGD()
    End Sub
    Private Sub mnu_DataAwal_HutangUsaha_Impor_GBP_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_GBP.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_GBP()
    End Sub


    Private Sub mnu_DataAwal_HutangUsaha_BAK_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangUsaha_BAK.Click
        X_BukuPengawasanHutangUsaha_BAK.JalurMasuk = Halaman_MENUUTAMA
        X_BukuPengawasanHutangUsaha_BAK.MdiParent = Me
        X_BukuPengawasanHutangUsaha_BAK.Show()
        X_BukuPengawasanHutangUsaha_BAK.Focus()
    End Sub

    'DATA AWAL - Hutang - Hutang Bank :
    Private Sub mnu_DataAwal_HutangBank_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangBank.Click
        BukaModul_BukuPengawasanHutangBank()
    End Sub

    'DATA AWAL - Hutang - Hutang Leasing :
    Private Sub mnu_DataAwal_HutangLeasing_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangLeasing.Click
        BukaModul_BukuPengawasanHutangLeasing()
    End Sub

    'DATA AWAL - Hutang - Hutang Pihak Ketiga :
    Private Sub mnu_DataAwal_HutangPihakKetiga_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPihakKetiga.Click
        BukaModul_BukuPengawasanHutangPihakKetiga()
    End Sub

    'DATA AWAL - Hutang - Hutang Afiliasi :
    Private Sub mnu_DataAwal_HutangAfiliasi_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangAfiliasi.Click
        BukaModul_BukuPengawasanHutangAfiliasi()
    End Sub

    'DATA AWAL - Hutang - Hutang Pihak Ketiga :
    Private Sub mnu_DataAwal_HutangKaryawan_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangKaryawan.Click
        BukaModul_BukuPengawasanHutangKaryawan()
    End Sub

    'DATA AWAL - Hutang - Hutang Pemegang Saham :
    Private Sub mnu_DataAwal_HutangPemegangSaham_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPemegangSaham.Click
        BukaModul_BukuPengawasanHutangPemegangSaham()
    End Sub

    'DATA AWAL - Hutang PPh Pasal 21 :
    Private Sub mnu_DataAwal_HutangPPhPasal21_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPPhPasal21.Click
        BukaModul_BukuPengawasanHutangPPhPasal21()
    End Sub

    'DATA AWAL - Hutang PPh Pasal 23 :
    Private Sub mnu_DataAwal_HutangPPhPasal23_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPPhPasal23.Click
        BukaModul_BukuPengawasanHutangPPhPasal23()
    End Sub

    'DATA AWAL - Hutang PPh Pasal 4 (2) :
    Private Sub mnu_DataAwal_HutangPPhPasal42_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPPhPasal42.Click
        BukaModul_BukuPengawasanHutangPPhPasal42()
    End Sub

    'DATA AWAL - Hutang PPh Pasal 25 :
    Private Sub mnu_DataAwal_HutangPPhPasal25_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPPhPasal25.Click
        BukaModul_BukuPengawasanHutangPPhPasal25()
    End Sub

    'DATA AWAL - Hutang PPh Pasal 26 :
    Private Sub mnu_DataAwal_HutangPPhPasal26_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPPhPasal26.Click
        BukaModul_BukuPengawasanHutangPPhPasal26()
    End Sub

    'DATA AWAL - Hutang PPh Pasal 29 :
    Private Sub mnu_DataAwal_HutangPPhPasal29_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPPhPasal29.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    'DATA AWAL - Hutang PPN :
    Private Sub mnu_DataAwal_HutangPPN_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangPPN.Click
        BukaModul_BukuPengawasanPelaporanPPN()
    End Sub

    'DATA AWAL - Hutang Ketetapan Pajak :
    Private Sub mnu_DataAwal_HutangKetetapanPajak_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangKetetapanPajak.Click
        BukaModul_BukuPengawasanKetetapanPajak()
    End Sub


    'DATA AWAL - Hutang Gaji :
    Private Sub mnu_DataAwal_HutangGaji_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangGaji.Click
        BukaModul_BukuPengawasanGaji()
    End Sub

    Private Sub mnu_DataAwal_HutangBPJSKesehatan_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangBPJSKesehatan.Click
        BukaModul_BukuPengawasanHutangBPJSKesehatan()
    End Sub

    Private Sub mnu_DataAwal_HutangBPJSKetenagakerjaan_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangBPJSKetenagakerjaan.Click
        BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
    End Sub

    Private Sub mnu_DataAwal_HutangKoperasiKaryawan_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangKoperasiKaryawan.Click
        BukaModul_BukuPengawasanHutangKoperasiKaryawan()
    End Sub

    Private Sub mnu_DataAwal_HutangSerikat_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_HutangSerikat.Click
        BukaModul_BukuPengawasanHutangSerikat()
    End Sub

    'DATA AWAL >> Piutang >> Piutang Usaha :
    Private Sub mnu_DataAwal_PiutangUsaha_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha.Click
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_NonAfiliasi_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_NonAfiliasi()
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Afiliasi_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor.Click
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_USD_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_USD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_AUD_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_AUD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_AUD()
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_JPY_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_JPY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_JPY()
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_CNY_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_CNY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_CNY()
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_EUR_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_EUR.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_EUR()
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_SGD_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_SGD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_SGD()
    End Sub
    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_GBP_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_GBP.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_GBP()
    End Sub

    'DATA AWAL >> Piutang >> Piutang Pihak Ketiga :
    Private Sub mnu_DataAwal_PiutangPihakKetiga_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangPihakKetiga.Click
        BukaModul_BukuPengawasanPiutangPihakKetiga()
    End Sub

    'DATA AWAL >> Piutang >> Piutang Afiliasi :
    Private Sub mnu_DataAwal_PiutangAfiliasi_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangAfiliasi.Click
        BukaModul_BukuPengawasanPiutangAfiliasi()
    End Sub

    'DATA AWAL >> Piutang >> Piutang Karyawan :
    Private Sub mnu_DataAwal_PiutangKaryawan_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangKaryawan.Click
        BukaModul_BukuPengawasanPiutangKaryawan()
    End Sub

    'DATA AWAL >> Piutang >> Piutang Pemegang Saham :
    Private Sub mnu_DataAwal_PiutangPemegangSaham_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_PiutangPemegangSaham.Click
        BukaModul_BukuPengawasanPiutangPemegangSaham()
    End Sub

    Private Sub mnu_DataAwal_DepositOperasional_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_DepositOperasional.Click
        BukaModul_BukuPengawasanDepositOperasional()
    End Sub


    'DATA AWAL >> Asset >> Amortisasi Biaya :
    Private Sub mnu_DataAwal_AmortisasiBiaya_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_AmortisasiBiaya.Click
        BukaModul_ManajemenAmortisasiBiaya()
    End Sub

    'DATA AWAL >> Asset >> Asset Tetap :
    Private Sub mnu_DataAwal_AssetTetap_Click(sender As Object, e As EventArgs) Handles mnu_DataAwal_AssetTetap.Click
        BukaModul_DaftarPenyusutanAssetTetap()
    End Sub


    'SUB MENU : DATA USER ---------------------------------------------------------------------
    Private Sub mnu_DataUser_Click(sender As Object, e As EventArgs) Handles mnu_DataUser.Click
        frm_DataUser.MdiParent = Me
        frm_DataUser.Show()
        frm_DataUser.Focus()
    End Sub

    'SUB MENU : DATA COA --------------------------------------------------------------------
    Private Sub mnu_DataCOA_Click(sender As Object, e As EventArgs) Handles mnu_DataCOA.Click
        frm_DataCOA.MdiParent = Me
        frm_DataCOA.Show()
        frm_DataCOA.Focus()
    End Sub

    'SUB MENU : DATA MITRA ----------------------------------------------------------------------
    Private Sub mnu_DataMitra_Click(sender As Object, e As EventArgs) Handles mnu_DataMitra.Click
        frm_DataMitra.MdiParent = Me
        frm_DataMitra.Show()
        frm_DataMitra.Focus()
    End Sub

    'SUB MENU : DATA LAWAN TRANSAKSI ------------------------------------------------------------------------------
    Private Sub mnu_DataLawanTransaksi_Click(sender As Object, e As EventArgs) Handles mnu_DataLawanTransaksi.Click
        frm_DataLawanTransaksi.MdiParent = Me
        frm_DataLawanTransaksi.Show()
        frm_DataLawanTransaksi.Focus()
    End Sub

    'SUB MENU : DATA KARYAWAN ----------------------------------------------------------------------
    Private Sub mnu_DataKaryawan_Click(sender As Object, e As EventArgs) Handles mnu_DataKaryawan.Click
        frm_DataKaryawan.MdiParent = Me
        frm_DataKaryawan.Show()
        frm_DataKaryawan.Focus()
    End Sub


    'SUB MENU : DAFTAR PEMEGANG SAHAM --------------------------------------------------------------
    Private Sub mnu_DaftarPemegangSaham_Click(sender As Object, e As EventArgs) Handles mnu_DaftarPemegangSaham.Click
        frm_DaftarPemegangSaham.MdiParent = Me
        frm_DaftarPemegangSaham.Show()
        frm_DaftarPemegangSaham.Focus()
    End Sub


    'SUB MENU : DATA PEMEGANG SAHAM ---------------------------------------------------------------- (Ini nanti hapus saja...!!!)
    Private Sub mnu_DataPemegangSaham_Click(sender As Object, e As EventArgs) Handles mnu_DataPemegangSaham.Click
        frm_DataPemegangSaham.MdiParent = Me
        frm_DataPemegangSaham.Show()
        frm_DataPemegangSaham.Focus()
    End Sub

    'SUB MENU : DATA PROJECT ----------------------------------------------------------------------
    Private Sub mnu_DataProject_Click(sender As Object, e As EventArgs) Handles mnu_DataProject.Click
        frm_DataProject.MdiParent = Me
        frm_DataProject.Show()
        frm_DataProject.Focus()
    End Sub

    'SUB MENU : TAHUN BUKU ------------------------------------------------------------------------

    'Buat Buku Baru:
    Private Sub mnu_BuatBukuBaru_Click(sender As Object, e As EventArgs) Handles mnu_BuatBukuBaru.Click
        win_BuatDatabaseBukuBaru = New wpfWin_BuatDatabaseBukuBaru
        win_BuatDatabaseBukuBaru.ResetForm()
        win_BuatDatabaseBukuBaru.ShowDialog()
    End Sub

    'Ganti Tahun Buku :
    Private Sub mnu_GantiTahunBuku_Click(sender As Object, e As EventArgs) Handles mnu_GantiTahunBuku.Click
        win_GantiTahunBuku = New wpfWin_GantiTahunBuku
        win_GantiTahunBuku.FungsiForm = FungsiForm_GANTITAHUNBUKU
        win_GantiTahunBuku.ShowDialog()
    End Sub

    'Tutup Buku :
    Private Sub mnu_TutupBuku_Click(sender As Object, e As EventArgs) Handles mnu_TutupBuku.Click
        'If LevelUserAktif < LevelUser_99_AppDeveloper Then
        '    PesanPemberitahuan("Mohon maaf... " & Enter2Baris &
        '                   "Fitur TUTUP BUKU sedang diperbaiki.")
        '    Return
        'End If
        usc_TutupBuku.ResetForm()
        frm_TutupBuku.MdiParent = Me
        frm_TutupBuku.Show()
        frm_TutupBuku.Focus()
    End Sub








    'KELOMPOK MENU : TRANSAKSI ====================================================================================================

    'KELOMPON SUB MENU : PEREKAMAN DATA -------------------------------------------------------------------------------------------

    'SUB MENU : PEMBELIAN ---------------------------------------------------------------------------------------------------------
    Private Sub mnu_InputPembelian_Click(sender As Object, e As EventArgs) Handles mnu_InputPembelian.Click
        frm_InputPembelian.ResetForm()
        frm_InputPembelian.FungsiForm = FungsiForm_TAMBAH
        frm_InputPembelian.ShowDialog()
    End Sub

    'SUB MENU : PENJUALAN ---------------------------------------------------------------------------------------------------------
    Private Sub mnu_InputPenjualan_Click(sender As Object, e As EventArgs) Handles mnu_InputPenjualan.Click
        FiturDalamPengembangan()
    End Sub

    'SUB MENU : Transaksi IN ----------------------------------------------------------------------------------------------------------------
    Private Sub mnu_TransaksiIN_Click(sender As Object, e As EventArgs) Handles mnu_TransaksiIN.Click
        win_InputTransaksi = New wpfWin_InputTransaksi
        win_InputTransaksi.ResetForm()
        win_InputTransaksi.FungsiForm = FungsiForm_TAMBAH
        win_InputTransaksi.JalurMasuk = Halaman_MENUUTAMA
        IsiValueComboBypassTerkunci(win_InputTransaksi.cmb_AlurTransaksi, AlurTransaksi_IN)
        win_InputTransaksi.ShowDialog()
    End Sub


    'SUB MENU : Transaksi OUT ---------------------------------------------------------------------------------------------------------------
    Private Sub mnu_TransaksiOUT_Click(sender As Object, e As EventArgs) Handles mnu_TransaksiOUT.Click
        win_InputTransaksi = New wpfWin_InputTransaksi
        win_InputTransaksi.ResetForm()
        win_InputTransaksi.FungsiForm = FungsiForm_TAMBAH
        win_InputTransaksi.JalurMasuk = Halaman_MENUUTAMA
        IsiValueComboBypassTerkunci(win_InputTransaksi.cmb_AlurTransaksi, AlurTransaksi_OUT)
        win_InputTransaksi.ShowDialog()
    End Sub


    'SUB MENU : ADJUSMENT ---------------------------------------------------------------------------------------------------------
    Private Sub mnu_Adjusment_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment.Click
        'Tidak ada Coding di Sini..! Adanya di masing-masing sub menu..!
    End Sub

    Private Sub mnu_Adjusment_BiayaPenyusutanAsset_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_BiayaPenyusutanAsset.Click

    End Sub

    Private Sub mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka.Click

    End Sub

    Private Sub mnu_Adjusment_PenghapusanPiutang_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_PenghapusanPiutang.Click

    End Sub

    Private Sub mnu_Adjusment_SelisihKurs_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_SelisihKurs.Click

    End Sub

    Private Sub mnu_Adjusment_SelisihPencatatan_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_SelisihPencatatan.Click

    End Sub

    Private Sub mnu_Adjusment_HPP_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP.Click
        'Tidak ada Coding di Sini..! Adanya di masing-masing sub menu..!
    End Sub
    Public JumlahMutasiAdjusmentHPP As Int64
    Public NomorUrutJurnalAdjusmentHPP
    Sub SusurTransaksiCOA_UntukBahanAdjusmentHPP(BulanAdjusment_Angka As Integer, COA_Awal As Integer, COA_Akhir As Integer)
        JumlahMutasiAdjusmentHPP = 0
        Dim JumlahKredit As Int64 = 0
        AksesDatabase_General(Buka)
        Dim COA As Integer
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_COA " &
                                     " WHERE COA BETWEEN " & COA_Awal &
                                     " AND " & COA_Akhir &
                                     " ORDER BY COA ", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            COA = drTELUSUR.Item("COA")
            JumlahKredit _
                = TotalDebetCOA_BulanTertentu(COA, BulanAdjusment_Angka) _
                - TotalKreditCOA_BulanTertentu(COA, BulanAdjusment_Angka)
            If JumlahKredit <> 0 Then
                NomorUrutJurnalAdjusmentHPP += 1
                TambahBarisKredit_BahanJurnal(COA, JumlahKredit)
                JumlahMutasiAdjusmentHPP += JumlahKredit
            End If
        Loop
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub mnu_Adjusment_HPP_PemakaianBahanPenolong_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP_PemakaianBahanPenolong.Click
        BukaModul_StockOpname_BahanPenolong()
    End Sub

    Private Sub mnu_Adjusment_HPP_PemakaianBahanBaku_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP_PemakaianBahanBaku.Click
        BukaModul_StockOpname_BahanBaku()
    End Sub

    Private Sub mnu_Adjusment_HPP_BiayaBahanBaku_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP_BiayaBahanBaku.Click
        AdjusmentHPP_BiayaBahanBaku()
    End Sub
    Sub AdjusmentHPP_BiayaBahanBaku()

        Dim BulanTerakhirJurnalAdjusment_BiayaBahanBaku = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaBahanBaku & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanTerakhirJurnalAdjusment_BiayaBahanBaku = 12 Then
            PesanPemberitahuan("Jurnal Adjusment HPP untuk Biaya Bahan Baku sudah lengkap sampai Desember " & TahunBukuAktif & ".")
            Return
        End If

        Dim BulanAdjusment_Angka As Integer = BulanTerakhirJurnalAdjusment_BiayaBahanBaku + 1

        Dim BulanTerakhirJurnalAdjusment_PersediaanBahanBaku = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_PersediaanBahanBaku_Lokal & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanAdjusment_Angka > BulanTerakhirJurnalAdjusment_PersediaanBahanBaku Then
            BeginInvoke(Sub() PesanPeringatan("Silakan dorong terlebih dahulu Jurnal Persediaan Bahan Baku untuk Bulan '" &
                                              KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "' sebelum melakukan Adjusment."))
            BukaModul_StockOpname_BahanBaku()
            Return
        End If

        Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif))
        TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaBahanBaku, 0) '(Angka 0 ini nanti akan dikoreksi dengan Jumlah Mutasi Debet yang sesungguhnya, nanti di akhir)
        SusurTransaksiCOA_UntukBahanAdjusmentHPP(BulanAdjusment_Angka, KodeTautanCOA_BiayaTransportasiPembelianBb_Lokal, KodeTautanCOA_BiayaPemakaianBahanBaku_Import)

        Dim PesanPertanyaan As String
        If JumlahMutasiAdjusmentHPP = 0 Then
            NomorUrutJurnalAdjusmentHPP += 1
            TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaBahanBaku, 0)
            PesanPertanyaan = "Tidak ada transaksi 'Biaya Bahan Baku' selama Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        Else
            PesanPertanyaan = "Anda akan melakukan Adjusment HPP Biaya Bahan Baku untuk Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        End If
        Pilihan = MessageBox.Show(PesanPertanyaan & Enter2Baris & "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        win_InputJurnal.datatabelUtama.Rows(0)("Jumlah_Debet") = JumlahMutasiAdjusmentHPP    '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TotalDebetBahanJurnal = JumlahMutasiAdjusmentHPP                                        '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TampilkanFormInputJurnal()

    End Sub

    Private Sub mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung.Click
        AdjusmentHPP_BiayaTenagaKerjaLangsung()
    End Sub

    Sub AdjusmentHPP_BiayaTenagaKerjaLangsung()

        Dim BulanTerakhirJurnalAdjusment_BiayaTenagaKerjaLangsung = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaTenagaKerjaLangsung & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanTerakhirJurnalAdjusment_BiayaTenagaKerjaLangsung = 12 Then
            PesanPemberitahuan("Jurnal Adjusment HPP untuk Biaya Tenaga Kerja Langsung sudah lengkap sampai Desember " & TahunBukuAktif & ".")
            Return
        End If

        Dim BulanAdjusment_Angka As Integer = BulanTerakhirJurnalAdjusment_BiayaTenagaKerjaLangsung + 1
        Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif))
        TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaTenagaKerjaLangsung, 0) '(Angka 0 ini nanti akan dikoreksi dengan Jumlah Mutasi Debet yang sesungguhnya, nanti di akhir)
        SusurTransaksiCOA_UntukBahanAdjusmentHPP(BulanAdjusment_Angka, KodeTautanCOA_BiayaGajiProduksi, 52199)

        If JumlahMutasiAdjusmentHPP = 0 Then
            BeginInvoke(Sub() IsiValueComboBypassTerkunci(usc_BukuPengawasanGaji.cmb_TahunTelusurData, TahunBukuAktif))
            BeginInvoke(Sub() IsiValueComboBypassTerkunci(usc_BukuPengawasanGaji.cmb_Bulan, KonversiAngkaKeBulanString(BulanAdjusment_Angka)))
            BeginInvoke(Sub() PesanPemberitahuan("Silakan dorong terlebih dahulu Jurnal Gaji untuk Bulan '" &
                                                 KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "' sebelum melakukan Adjusment."))
            BukaModul_BukuPengawasanGaji()
            Return
        End If

        Pilihan = MessageBox.Show("Anda akan melakukan Adjusment HPP Biaya Tenaga Kerja Langsung untuk Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'." & Enter2Baris &
                                  "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        win_InputJurnal.datatabelUtama.Rows(0)("Jumlah_Debet") = JumlahMutasiAdjusmentHPP    '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TotalDebetBahanJurnal = JumlahMutasiAdjusmentHPP                                        '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TampilkanFormInputJurnal()

    End Sub

    Private Sub mnu_Adjusment_HPP_BiayaOverheadPabrik_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP_BiayaOverheadPabrik.Click
        AdjusmentHPP_BiayaOverheadPabrik()
    End Sub
    Sub AdjusmentHPP_BiayaOverheadPabrik()

        Dim BulanTerakhirJurnalAdjusment_BiayaOverheadPabrik = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaOverheadPabrik & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanTerakhirJurnalAdjusment_BiayaOverheadPabrik = 12 Then
            PesanPemberitahuan("Jurnal Adjusment HPP untuk Biaya Overhead Pabrik sudah lengkap sampai Desember " & TahunBukuAktif & ".")
            Return
        End If

        Dim BulanAdjusment_Angka As Integer = BulanTerakhirJurnalAdjusment_BiayaOverheadPabrik + 1

        Dim BulanTerakhirJurnalAdjusment_PersediaanBahanPenolong = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_PersediaanBahanPenolong & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanAdjusment_Angka > BulanTerakhirJurnalAdjusment_PersediaanBahanPenolong Then
            BeginInvoke(Sub() PesanPeringatan("Silakan dorong terlebih dahulu Jurnal Persediaan Bahan Penolong untuk Bulan '" &
                                              KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "' sebelum melakukan Adjusment."))
            BukaModul_StockOpname_BahanPenolong()
            Return
        End If

        Dim COA_BiayaOverhead_Awal = 53102
        Dim COA_BiayaOverhead_Akhir = 53999

        Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif))
        TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaOverheadPabrik, 0) '(Angka 0 ini nanti akan dikoreksi dengan Jumlah Mutasi Debet yang sesungguhnya, nanti di akhir)
        SusurTransaksiCOA_UntukBahanAdjusmentHPP(BulanAdjusment_Angka, COA_BiayaOverhead_Awal, COA_BiayaOverhead_Akhir)

        Dim PesanPertanyaan As String
        If JumlahMutasiAdjusmentHPP = 0 Then
            NomorUrutJurnalAdjusmentHPP += 1
            TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaOverheadPabrik, 0)
            PesanPertanyaan = "Tidak ada transaksi 'Biaya Overhead Pabrik' selama Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        Else
            PesanPertanyaan = "Anda akan melakukan Adjusment HPP Biaya Overhead Pabrik untuk Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'." & Enter2Baris
        End If

        Pilihan = MessageBox.Show(PesanPertanyaan & Enter2Baris & "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        win_InputJurnal.datatabelUtama.Rows(0)("Jumlah_Debet") = JumlahMutasiAdjusmentHPP   '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TotalDebetBahanJurnal = JumlahMutasiAdjusmentHPP                                    '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TampilkanFormInputJurnal()

    End Sub

    Private Sub mnu_Adjusment_HPP_BiayaProduksi_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP_BiayaProduksi.Click
        AdjusmentHPP_BiayaProduksi()
    End Sub
    Sub AdjusmentHPP_BiayaProduksi()

        Dim BulanTerakhirJurnalAdjusment_BiayaProduksi = AmbilValue_BulanTertuaAngka(
            " tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaProduksi & "' " &
            " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' ", "Tanggal_Transaksi")

        If BulanTerakhirJurnalAdjusment_BiayaProduksi = 12 Then
            PesanPemberitahuan("Jurnal Adjusment HPP untuk Biaya Produksi sudah lengkap sampai Desember " & TahunBukuAktif & ".")
            Return
        End If

        Dim BulanAdjusment_Angka As Integer = BulanTerakhirJurnalAdjusment_BiayaProduksi + 1
        Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif))
        TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaProduksi, 0) '(Angka 0 ini nanti akan dikoreksi dengan Jumlah Mutasi Debet yang sesungguhnya, nanti di akhir)
        JumlahMutasiAdjusmentHPP = 0
        TambahBarisKredit_JurnalAdjusmentHpp_BiayaProduksi(KodeTautanCOA_BiayaBahanBaku, BulanAdjusment_Angka)
        TambahBarisKredit_JurnalAdjusmentHpp_BiayaProduksi(KodeTautanCOA_BiayaTenagaKerjaLangsung, BulanAdjusment_Angka)
        TambahBarisKredit_JurnalAdjusmentHpp_BiayaProduksi(KodeTautanCOA_BiayaOverheadPabrik, BulanAdjusment_Angka)

        Dim PesanPertanyaan As String
        If JumlahMutasiAdjusmentHPP = 0 Then
            NomorUrutJurnalAdjusmentHPP += 1
            TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaProduksi, 0)
            PesanPertanyaan = "Tidak ada transaksi 'Biaya Produksi' selama Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        Else
            PesanPertanyaan = "Anda akan melakukan Adjusment HPP Biaya Produksi untuk Bulan '" & KonversiAngkaKeBulanString(BulanAdjusment_Angka) & "'."
        End If
        Pilihan = MessageBox.Show(PesanPertanyaan & Enter2Baris & "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        win_InputJurnal.datatabelUtama.Rows(0)("Jumlah_Debet") = JumlahMutasiAdjusmentHPP   '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TotalDebetBahanJurnal = JumlahMutasiAdjusmentHPP                                    '(Koreksi Jumlah Mutasi Debet. Kenapa belakangan? Karena harus dijumlah dulu.)
        TampilkanFormInputJurnal()

    End Sub
    Sub TambahBarisKredit_JurnalAdjusmentHpp_BiayaProduksi(COA, BulanAdjusment_Angka)
        Dim JumlahKredit _
            = TotalDebetCOA_BulanTertentu(COA, BulanAdjusment_Angka) _
            - TotalKreditCOA_BulanTertentu(COA, BulanAdjusment_Angka)
        If JumlahKredit <> 0 Then
            NomorUrutJurnalAdjusmentHPP += 1
            TambahBarisKredit_BahanJurnal(COA, JumlahKredit)
            JumlahMutasiAdjusmentHPP += JumlahKredit
        End If
        PesanUntukProgrammer("COA : " & COA & Enter2Baris &
                             "Nama Akun : " & AmbilValue_NamaAkun(COA) & Enter2Baris &
                             "Total Debet : " & TotalDebetCOA_BulanTertentu(COA, BulanAdjusment_Angka) & Enter2Baris &
                             "Total Kredit : " & TotalKreditCOA_BulanTertentu(COA, BulanAdjusment_Angka) & Enter2Baris &
                             "Jumlah Kredit : " & JumlahKredit & Enter2Baris &
                             "")
    End Sub

    Private Sub mnu_Adjusment_HPP_HargaPokokProduksi_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP_HargaPokokProduksi.Click
        BukaModul_StockOpname_BarangDalamProses_CekFisik()
    End Sub

    Private Sub mnu_Adjusment_HPP_HargaPokokPenjualan_Click(sender As Object, e As EventArgs) Handles mnu_Adjusment_HPP_HargaPokokPenjualan.Click
        BukaModul_StockOpname_BarangJadi()
    End Sub

    Private Sub mnu_AdjusmentLainnya_Click(sender As Object, e As EventArgs) Handles mnu_AdjusmentLainnya.Click

    End Sub

    'Pemindahbukuan :
    Private Sub mnu_Pemindahbukuan_Click(sender As Object, e As EventArgs) Handles mnu_Pemindahbukuan.Click
        frm_InputPemindahbukuan.ResetForm()
        frm_InputPemindahbukuan.FungsiForm = FungsiForm_TAMBAH
        frm_InputPemindahbukuan.ShowDialog()
    End Sub





    'KELOMPOK MENU : BUKU PEMBELIAN ===============================================================================================

    'SUB MENU : PO ----------------------------------------------------------------------------------------------------------------
    Private Sub mnu_PO_Pembelian_Click(sender As Object, e As EventArgs) Handles mnu_PO_Pembelian.Click
    End Sub

    Private Sub mnu_PO_Pembelian_Lokal_Click(sender As Object, e As EventArgs) Handles mnu_PO_Pembelian_Lokal.Click
    End Sub

    Private Sub mnu_POPembelian_Lokal_Barang_Click(sender As Object, e As EventArgs) Handles mnu_POPembelian_Lokal_Barang.Click
        frm_POPembelian_Lokal_Barang.MdiParent = Me
        frm_POPembelian_Lokal_Barang.Show()
        frm_POPembelian_Lokal_Barang.Focus()
    End Sub

    Private Sub mnu_POPembelian_Lokal_Jasa_Click(sender As Object, e As EventArgs) Handles mnu_POPembelian_Lokal_Jasa.Click
        frm_POPembelian_Lokal_Jasa.MdiParent = Me
        frm_POPembelian_Lokal_Jasa.Show()
        frm_POPembelian_Lokal_Jasa.Focus()
    End Sub

    Private Sub mnu_POPembelian_Lokal_BarangDanJasa_Click(sender As Object, e As EventArgs) Handles mnu_POPembelian_Lokal_BarangDanJasa.Click
        frm_POPembelian_Lokal_BarangDanJasa.MdiParent = Me
        frm_POPembelian_Lokal_BarangDanJasa.Show()
        frm_POPembelian_Lokal_BarangDanJasa.Focus()
    End Sub

    Private Sub mnu_POPembelian_Lokal_JasaKonstruksi_Click(sender As Object, e As EventArgs) Handles mnu_POPembelian_Lokal_JasaKonstruksi.Click
        frm_POPembelian_Lokal_JasaKonstruksi.MdiParent = Me
        frm_POPembelian_Lokal_JasaKonstruksi.Show()
        frm_POPembelian_Lokal_JasaKonstruksi.Focus()
    End Sub

    Private Sub mnu_POPembelian_Semua_Click(sender As Object, e As EventArgs) Handles mnu_POPembelian_Semua.Click
        frm_POPembelian_Lokal_Semua.MdiParent = Me
        frm_POPembelian_Lokal_Semua.Show()
        frm_POPembelian_Lokal_Semua.Focus()
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Click(sender As Object, e As EventArgs) Handles mnu_PO_Pembelian_Impor.Click
    End Sub


    Private Sub mnu_PO_Pembelian_Impor_Barang_Click(sender As Object, e As EventArgs) Handles mnu_PO_Pembelian_Impor_Barang.Click
        frm_POPembelian_Impor_Barang.MdiParent = Me
        frm_POPembelian_Impor_Barang.Show()
        frm_POPembelian_Impor_Barang.Focus()
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Jasa_Click(sender As Object, e As EventArgs) Handles mnu_PO_Pembelian_Impor_Jasa.Click
        frm_POPembelian_Impor_Jasa.MdiParent = Me
        frm_POPembelian_Impor_Jasa.Show()
        frm_POPembelian_Impor_Jasa.Focus()
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Semua_Click(sender As Object, e As EventArgs) Handles mnu_PO_Pembelian_Impor_Semua.Click
        frm_POPembelian_Impor_Semua.MdiParent = Me
        frm_POPembelian_Impor_Semua.Show()
        frm_POPembelian_Impor_Semua.Focus()
    End Sub




    'SUB MENU : SURAT JALAN -------------------------------------------------------------------------------------------------------
    Private Sub mnu_SuratJalanPembelian_Click(sender As Object, e As EventArgs) Handles mnu_SuratJalanPembelian.Click
        frm_SuratJalanPembelian.MdiParent = Me
        frm_SuratJalanPembelian.Show()
        frm_SuratJalanPembelian.Focus()
    End Sub

    'SUB MENU : BUKU BAST ---------------------------------------------------------------------------------------------------------
    Private Sub mnu_BASTPembelian_Click(sender As Object, e As EventArgs) Handles mnu_BASTPembelian.Click
        frm_BASTPembelian.MdiParent = Me
        frm_BASTPembelian.Show()
        frm_BASTPembelian.Focus()
    End Sub

    'SUB MENU : INVOICE PEMBELIAN -------------------------------------------------------------------------------------------------
    Private Sub mnu_InvoicePembelian_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian.Click
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_DenganPO.Click
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal.Click
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Rutin_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal_Rutin.Click
        frm_InvoicePembelian_DenganPO_Lokal_Rutin.MdiParent = Me
        frm_InvoicePembelian_DenganPO_Lokal_Rutin.Show()
        frm_InvoicePembelian_DenganPO_Lokal_Rutin.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Termin_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal_Termin.Click
        frm_InvoicePembelian_DenganPO_Lokal_Termin.MdiParent = Me
        frm_InvoicePembelian_DenganPO_Lokal_Termin.Show()
        frm_InvoicePembelian_DenganPO_Lokal_Termin.Focus()
    End Sub


    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor.Click
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Rutin_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor_Rutin.Click
        frm_InvoicePembelian_DenganPO_Impor_Rutin.MdiParent = Me
        frm_InvoicePembelian_DenganPO_Impor_Rutin.Show()
        frm_InvoicePembelian_DenganPO_Impor_Rutin.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Termin_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor_Termin.Click
        frm_InvoicePembelian_DenganPO_Impor_Termin.MdiParent = Me
        frm_InvoicePembelian_DenganPO_Impor_Termin.Show()
        frm_InvoicePembelian_DenganPO_Impor_Termin.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal.Click
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Barang_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_Barang.Click
        frm_InvoicePembelian_TanpaPO_Lokal_Barang.MdiParent = Me
        frm_InvoicePembelian_TanpaPO_Lokal_Barang.Show()
        frm_InvoicePembelian_TanpaPO_Lokal_Barang.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Jasa_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_Jasa.Click
        frm_InvoicePembelian_TanpaPO_Lokal_Jasa.MdiParent = Me
        frm_InvoicePembelian_TanpaPO_Lokal_Jasa.Show()
        frm_InvoicePembelian_TanpaPO_Lokal_Jasa.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Click
        frm_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.MdiParent = Me
        frm_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Show()
        frm_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Click
        frm_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.MdiParent = Me
        frm_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Show()
        frm_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor.Click
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Barang_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor_Barang.Click
        frm_InvoicePembelian_TanpaPO_Impor_Barang.MdiParent = Me
        frm_InvoicePembelian_TanpaPO_Impor_Barang.Show()
        frm_InvoicePembelian_TanpaPO_Impor_Barang.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Jasa_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor_Jasa.Click
        frm_InvoicePembelian_TanpaPO_Impor_Jasa.MdiParent = Me
        frm_InvoicePembelian_TanpaPO_Impor_Jasa.Show()
        frm_InvoicePembelian_TanpaPO_Impor_Jasa.Focus()
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_LokalMUA_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePembelian_TanpaPO_LokalMUA.Click

    End Sub




    'SUB MENU : BUKU PEMBELIAN ----------------------------------------------------------------------------------------------------
    Private Sub mnu_BukuPembelian_Click(sender As Object, e As EventArgs) Handles mnu_BukuPembelian.Click
    End Sub

    Private Sub mnu_BukuPembelian_Lokal_Click(sender As Object, e As EventArgs) Handles mnu_BukuPembelian_Lokal.Click
        frm_BukuPembelian_lokal.MdiParent = Me
        frm_BukuPembelian_lokal.Show()
        frm_BukuPembelian_lokal.Focus()
    End Sub

    Private Sub mnu_BukuPembelian_Impor_Click(sender As Object, e As EventArgs) Handles mnu_BukuPembelian_Impor.Click
        frm_BukuPembelian_Impor.MdiParent = Me
        frm_BukuPembelian_Impor.Show()
        frm_BukuPembelian_Impor.Focus()
    End Sub


    'SUB MENU : RETUR PEMBELIAN ---------------------------------------------------------------------------------------------------
    Private Sub mnu_ReturPembelian_Click(sender As Object, e As EventArgs) Handles mnu_ReturPembelian.Click
        frm_ReturPembelian.MdiParent = Me
        frm_ReturPembelian.Show()
        frm_ReturPembelian.Focus()
    End Sub








    'KELOMPOK MENU : BUKU PENJUALAN ===============================================================================================

    'SUB MENU : BUKU PENGAWASAN PO PENJUALAN --------------------------------------------------------------------------------------
    Private Sub mnu_BukuPengawasanPOPenjualan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPOPenjualan.Click
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Barang_Click(sender As Object, e As EventArgs) Handles mnu_POPenjualan_Lokal_Barang.Click
        frm_POPenjualan_Lokal_Barang.MdiParent = Me
        frm_POPenjualan_Lokal_Barang.Show()
        frm_POPenjualan_Lokal_Barang.Focus()
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Jasa_Click(sender As Object, e As EventArgs) Handles mnu_POPenjualan_Lokal_Jasa.Click
        frm_POPenjualan_Lokal_Jasa.MdiParent = Me
        frm_POPenjualan_Lokal_Jasa.Show()
        frm_POPenjualan_Lokal_Jasa.Focus()
    End Sub

    Private Sub mnu_POPenjualan_Lokal_BarangDanJasa_Click(sender As Object, e As EventArgs) Handles mnu_POPenjualan_Lokal_BarangDanJasa.Click
        frm_POPenjualan_Lokal_BarangDanJasa.MdiParent = Me
        frm_POPenjualan_Lokal_BarangDanJasa.Show()
        frm_POPenjualan_Lokal_BarangDanJasa.Focus()
    End Sub

    Private Sub mnu_POPenjualan_Lokal_JasaKonstruksi_Click(sender As Object, e As EventArgs) Handles mnu_POPenjualan_Lokal_JasaKonstruksi.Click
        frm_POPenjualan_Lokal_JasaKonstruksi.MdiParent = Me
        frm_POPenjualan_Lokal_JasaKonstruksi.Show()
        frm_POPenjualan_Lokal_JasaKonstruksi.Focus()
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Semua_Click(sender As Object, e As EventArgs) Handles mnu_POPenjualan_Lokal_Semua.Click
        frm_POPenjualan_Lokal_Semua.MdiParent = Me
        frm_POPenjualan_Lokal_Semua.Show()
        frm_POPenjualan_Lokal_Semua.Focus()
    End Sub

    Private Sub mnu_POPenjualan_Ekspor_Click(sender As Object, e As EventArgs) Handles mnu_POPenjualan_Ekspor.Click
        frm_POPenjualan_Ekspor.MdiParent = Me
        frm_POPenjualan_Ekspor.Show()
        frm_POPenjualan_Ekspor.Focus()
    End Sub


    Private Sub mnu_SuratJalanPenjualan_Click(sender As Object, e As EventArgs) Handles mnu_SuratJalanPenjualan.Click
        frm_SuratJalanPenjualan.MdiParent = Me
        frm_SuratJalanPenjualan.Show()
        frm_SuratJalanPenjualan.Focus()
    End Sub

    Private Sub mnu_BASTPenjualan_Click(sender As Object, e As EventArgs) Handles mnu_BASTPenjualan.Click
        frm_BASTPenjualan.MdiParent = Me
        frm_BASTPenjualan.Show()
        frm_BASTPenjualan.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Lokal_Rutin_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_DenganPO_Lokal_Rutin.Click
        frm_InvoicePenjualan_DenganPO_Lokal_Rutin.MdiParent = Me
        frm_InvoicePenjualan_DenganPO_Lokal_Rutin.Show()
        frm_InvoicePenjualan_DenganPO_Lokal_Rutin.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Lokal_Termin_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_DenganPO_Lokal_Termin.Click
        frm_InvoicePenjualan_DenganPO_Lokal_Termin.MdiParent = Me
        frm_InvoicePenjualan_DenganPO_Lokal_Termin.Show()
        frm_InvoicePenjualan_DenganPO_Lokal_Termin.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin.Click
        frm_InvoicePenjualan_DenganPO_Ekspor_Rutin.MdiParent = Me
        frm_InvoicePenjualan_DenganPO_Ekspor_Rutin.Show()
        frm_InvoicePenjualan_DenganPO_Ekspor_Rutin.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Ekspor_Termin_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_DenganPO_Ekspor_Termin.Click
        frm_InvoicePenjualan_DenganPO_Ekspor_Termin.MdiParent = Me
        frm_InvoicePenjualan_DenganPO_Ekspor_Termin.Show()
        frm_InvoicePenjualan_DenganPO_Ekspor_Termin.Focus()
    End Sub


    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_Barang_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_Barang.Click
        frm_InvoicePenjualan_TanpaPO_Lokal_Barang.MdiParent = Me
        frm_InvoicePenjualan_TanpaPO_Lokal_Barang.Show()
        frm_InvoicePenjualan_TanpaPO_Lokal_Barang.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa.Click
        frm_InvoicePenjualan_TanpaPO_Lokal_Jasa.MdiParent = Me
        frm_InvoicePenjualan_TanpaPO_Lokal_Jasa.Show()
        frm_InvoicePenjualan_TanpaPO_Lokal_Jasa.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.Click
        frm_InvoicePenjualan_TanpaPO_Lokal_BarangdanJasa.MdiParent = Me
        frm_InvoicePenjualan_TanpaPO_Lokal_BarangdanJasa.Show()
        frm_InvoicePenjualan_TanpaPO_Lokal_BarangdanJasa.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Click
        frm_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.MdiParent = Me
        frm_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Show()
        frm_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Ekspor_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Ekspor.Click
        frm_InvoicePenjualan_TanpaPO_Ekspor.MdiParent = Me
        frm_InvoicePenjualan_TanpaPO_Ekspor.Show()
        frm_InvoicePenjualan_TanpaPO_Ekspor.Focus()
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Asset_Click(sender As Object, e As EventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Asset.Click
        mnu_DaftarPenyusutanAssetTetap_Click(sender, e)
    End Sub




    Private Sub mnu_BukuPenjualan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPenjualan.Click
    End Sub

    Private Sub mnu_BukuPenjualan_Lokal_Click(sender As Object, e As EventArgs) Handles mnu_BukuPenjualan_Lokal.Click
        frm_BukuPenjualan_Lokal.MdiParent = Me
        frm_BukuPenjualan_Lokal.Show()
        frm_BukuPenjualan_Lokal.Focus()
    End Sub

    Private Sub mnu_BukuPenjualan_Ekspor_Click(sender As Object, e As EventArgs) Handles mnu_BukuPenjualan_Ekspor.Click
        frm_BukuPenjualan_Ekspor.MdiParent = Me
        frm_BukuPenjualan_Ekspor.Show()
        frm_BukuPenjualan_Ekspor.Focus()
    End Sub


    Private Sub mnu_BukuPenjualanEceran_Click(sender As Object, e As EventArgs) Handles mnu_BukuPenjualanEceran.Click
        frm_BukuPenjualanEceran.MdiParent = Me
        frm_BukuPenjualanEceran.Show()
        frm_BukuPenjualanEceran.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanReturPenjualan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanReturPenjualan.Click
        frm_ReturPenjualan.MdiParent = Me
        frm_ReturPenjualan.Show()
        frm_ReturPenjualan.Focus()
    End Sub







    'KELOMPOK MENU : BUKU PENGAWASAN ==============================================================================================

    'KELOMPOK SUB MENU : BUKU BANK CASH -------------------------------------------------------------------------------------------

    'Buku Bank :
    Private Sub mnu_BukuBank_Click(sender As Object, e As EventArgs) Handles mnu_BukuBank.Click
        frm_BukuBank.MdiParent = Me
        frm_BukuBank.Show()
        frm_BukuBank.Focus()
    End Sub

    'Buku Kas :
    Private Sub mnu_BukuKas_Click(sender As Object, e As EventArgs) Handles mnu_BukuKas.Click
        frm_BukuKas.MdiParent = Me
        frm_BukuKas.Show()
        frm_BukuKas.Focus()
    End Sub

    'Buku Petty Cash :
    Private Sub mnu_BukuPettyCash_Click(sender As Object, e As EventArgs) Handles mnu_BukuPettyCash.Click
        frm_BukuPettyCash.MdiParent = Me
        frm_BukuPettyCash.Show()
        frm_BukuPettyCash.Focus()
    End Sub

    'Buku Cash Advance :
    Private Sub mnu_BukuCashAdvance_Click(sender As Object, e As EventArgs) Handles mnu_BukuCashAdvance.Click
        frm_BukuCashAdvance.MdiParent = Me
        frm_BukuCashAdvance.Show()
        frm_BukuCashAdvance.Focus()
    End Sub

    'Buku Bank Garansi :
    Private Sub mnu_BukuBankGaransi_Click(sender As Object, e As EventArgs) Handles mnu_BukuBankGaransi.Click
        frm_BukuBankGaransi.MdiParent = Me
        frm_BukuBankGaransi.Show()
        frm_BukuBankGaransi.Focus()
    End Sub


    'SUB MENU : BUKU PENGAWASAN GAJI ----------------------------------------------------------------------------------------------

    'Buku Pengawasan Gaji :
    Public Sub mnu_BukuPengawasanGaji_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanGaji.Click
        BukaModul_BukuPengawasanGaji()
    End Sub
    Sub BukaModul_BukuPengawasanGaji()
        frm_BukuPengawasanGaji.MdiParent = Me
        frm_BukuPengawasanGaji.Show()
        frm_BukuPengawasanGaji.Focus()
    End Sub

    'Buku Pengawasan Hutang BPJS Kesehatan :
    Private Sub mnu_BukuPengawasanHutangBPJSKesehatan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangBPJSKesehatan.Click
        BukaModul_BukuPengawasanHutangBPJSKesehatan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBPJSKesehatan()
        frm_BukuPengawasanHutangBPJSKesehatan.MdiParent = Me
        frm_BukuPengawasanHutangBPJSKesehatan.Show()
        frm_BukuPengawasanHutangBPJSKesehatan.Focus()
    End Sub


    'Buku Pengawasan Hutang BPJS Ketenagakerjaan :
    Private Sub mnu_BukuPengawasanHutangBPJSKetenagakerjaan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangBPJSKetenagakerjaan.Click
        BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
        frm_BukuPengawasanHutangBPJSKetenagakerjaan.MdiParent = Me
        frm_BukuPengawasanHutangBPJSKetenagakerjaan.Show()
        frm_BukuPengawasanHutangBPJSKetenagakerjaan.Focus()
    End Sub


    'Buku Pengawasan Hutang Koperasi :
    Private Sub mnu_BukuPengawasanHutangKoperasiKaryawan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangKoperasiKaryawan.Click
        BukaModul_BukuPengawasanHutangKoperasiKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangKoperasiKaryawan()
        frm_BukuPengawasanHutangKoperasiKaryawan.MdiParent = Me
        frm_BukuPengawasanHutangKoperasiKaryawan.Show()
        frm_BukuPengawasanHutangKoperasiKaryawan.Focus()
    End Sub


    'Buku Pengawasan Hutang Serikat :
    Private Sub mnu_BukuPengawasanHutangSerikat_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangSerikat.Click
        BukaModul_BukuPengawasanHutangSerikat()
    End Sub
    Sub BukaModul_BukuPengawasanHutangSerikat()
        frm_BukuPengawasanHutangSerikat.MdiParent = Me
        frm_BukuPengawasanHutangSerikat.Show()
        frm_BukuPengawasanHutangSerikat.Focus()
    End Sub



    'KELOMPOK SUB MENU : BUKU PENGAWASAN HUTANG -----------------------------------------------------------------------------------

    Private Sub mnu_BukuPengawasanHutangUsaha_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha.Click
    End Sub
    Private Sub mnu_BukuPengawasanHutangUsaha_NonAfiliasi_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
        frm_BukuPengawasanHutangUsaha_NonAfiliasi.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_NonAfiliasi.Show()
        frm_BukuPengawasanHutangUsaha_NonAfiliasi.Focus()
    End Sub
    Private Sub mnu_BukuPengawasanHutangUsaha_Afiliasi_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
        frm_BukuPengawasanHutangUsaha_Afiliasi.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_Afiliasi.Show()
        frm_BukuPengawasanHutangUsaha_Afiliasi.Focus()
    End Sub
    Private Sub mnu_BukuPengawasanHutangUsaha_Semua_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Semua.Click
        frm_BukuPengawasanHutangUsaha.MdiParent = Me
        frm_BukuPengawasanHutangUsaha.Show()
        frm_BukuPengawasanHutangUsaha.Focus()
    End Sub


    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_USD_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_USD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
        frm_BukuPengawasanHutangUsaha_Impor_USD.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_Impor_USD.Show()
        frm_BukuPengawasanHutangUsaha_Impor_USD.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_AUD_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_AUD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_AUD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_AUD()
        frm_BukuPengawasanHutangUsaha_Impor_AUD.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_Impor_AUD.Show()
        frm_BukuPengawasanHutangUsaha_Impor_AUD.Focus()
    End Sub


    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_JPY_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_JPY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_JPY()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_JPY()
        frm_BukuPengawasanHutangUsaha_Impor_JPY.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_Impor_JPY.Show()
        frm_BukuPengawasanHutangUsaha_Impor_JPY.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_CNY_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_CNY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_CNY()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_CNY()
        frm_BukuPengawasanHutangUsaha_Impor_CNY.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_Impor_CNY.Show()
        frm_BukuPengawasanHutangUsaha_Impor_CNY.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_EUR_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_EUR.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_EUR()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_EUR()
        frm_BukuPengawasanHutangUsaha_Impor_EUR.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_Impor_EUR.Show()
        frm_BukuPengawasanHutangUsaha_Impor_EUR.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_SGD_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_SGD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_SGD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_SGD()
        frm_BukuPengawasanHutangUsaha_Impor_SGD.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_Impor_SGD.Show()
        frm_BukuPengawasanHutangUsaha_Impor_SGD.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_GBP_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_GBP.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_GBP()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_GBP()
        frm_BukuPengawasanHutangUsaha_Impor_GBP.MdiParent = Me
        frm_BukuPengawasanHutangUsaha_Impor_GBP.Show()
        frm_BukuPengawasanHutangUsaha_Impor_GBP.Focus()
    End Sub


    Private Sub mnu_BukuPengawasanHutangBank_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangBank.Click
        BukaModul_BukuPengawasanHutangBank()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBank()
        frm_BukuPengawasanHutangBank.MdiParent = Me
        frm_BukuPengawasanHutangBank.Show()
        frm_BukuPengawasanHutangBank.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangLeasing_X_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangLeasing.Click
        BukaModul_BukuPengawasanHutangLeasing()
    End Sub
    Sub BukaModul_BukuPengawasanHutangLeasing()
        frm_BukuPengawasanHutangLeasing.MdiParent = Me
        frm_BukuPengawasanHutangLeasing.Show()
        frm_BukuPengawasanHutangLeasing.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangPihakKetiga_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPihakKetiga.Click
        BukaModul_BukuPengawasanHutangPihakKetiga()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPihakKetiga()
        frm_BukuPengawasanHutangPihakKetiga.MdiParent = Me
        frm_BukuPengawasanHutangPihakKetiga.Show()
        frm_BukuPengawasanHutangPihakKetiga.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangAfiliasi_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangAfiliasi.Click
        BukaModul_BukuPengawasanHutangAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangAfiliasi()
        frm_BukuPengawasanHutangAfiliasi.MdiParent = Me
        frm_BukuPengawasanHutangAfiliasi.Show()
        frm_BukuPengawasanHutangAfiliasi.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangKaryawan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangKaryawan.Click
        BukaModul_BukuPengawasanHutangKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangKaryawan()
        frm_BukuPengawasanHutangKaryawan.MdiParent = Me
        frm_BukuPengawasanHutangKaryawan.Show()
        frm_BukuPengawasanHutangKaryawan.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangPemegangSaham_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPemegangSaham.Click
        BukaModul_BukuPengawasanHutangPemegangSaham()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPemegangSaham()
        frm_BukuPengawasanHutangPemegangSaham.MdiParent = Me
        frm_BukuPengawasanHutangPemegangSaham.Show()
        frm_BukuPengawasanHutangPemegangSaham.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangDividen_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangDividen.Click
        BukaModul_BukuPengawasanHutangDividen()
    End Sub
    Sub BukaModul_BukuPengawasanHutangDividen()
        frm_BukuPengawasanHutangDividen.MdiParent = Me
        frm_BukuPengawasanHutangDividen.Show()
        frm_BukuPengawasanHutangDividen.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanHutangLainnya_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangLainnya.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    'KELOMPOK SUB MENU : BUKU PENGAWASAN PIUTANG ----------------------------------------------------------------------------------

    Private Sub mnu_BukuPengawasanPiutangUsaha_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha.Click
    End Sub
    Private Sub mnu_BukuPengawasanPiutangUsaha_NonAfiliasi_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_NonAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_NonAfiliasi()
        frm_BukuPengawasanPiutangUsaha_NonAfiliasi.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_NonAfiliasi.Show()
        frm_BukuPengawasanPiutangUsaha_NonAfiliasi.Focus()
    End Sub
    Private Sub mnu_BukuPengawasanPiutangUsaha_Afiliasi_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
        frm_BukuPengawasanPiutangUsaha_Afiliasi.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_Afiliasi.Show()
        frm_BukuPengawasanPiutangUsaha_Afiliasi.Focus()
    End Sub
    Private Sub mnu_BukuPengawasanPiutangUsaha_Semua_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Semua.Click
        frm_BukuPengawasanPiutangUsaha.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha.Show()
        frm_BukuPengawasanPiutangUsaha.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_USD_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_USD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
        frm_BukuPengawasanPiutangUsaha_Ekspor_USD.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_Ekspor_USD.Show()
        frm_BukuPengawasanPiutangUsaha_Ekspor_USD.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_AUD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_AUD()
        frm_BukuPengawasanPiutangUsaha_Ekspor_AUD.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_Ekspor_AUD.Show()
        frm_BukuPengawasanPiutangUsaha_Ekspor_AUD.Focus()
    End Sub


    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_JPY()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_JPY()
        frm_BukuPengawasanPiutangUsaha_Ekspor_JPY.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_Ekspor_JPY.Show()
        frm_BukuPengawasanPiutangUsaha_Ekspor_JPY.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_CNY()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_CNY()
        frm_BukuPengawasanPiutangUsaha_Ekspor_CNY.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_Ekspor_CNY.Show()
        frm_BukuPengawasanPiutangUsaha_Ekspor_CNY.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_EUR()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_EUR()
        frm_BukuPengawasanPiutangUsaha_Ekspor_EUR.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_Ekspor_EUR.Show()
        frm_BukuPengawasanPiutangUsaha_Ekspor_EUR.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_SGD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_SGD()
        frm_BukuPengawasanPiutangUsaha_Ekspor_SGD.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_Ekspor_SGD.Show()
        frm_BukuPengawasanPiutangUsaha_Ekspor_SGD.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_GBP()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_GBP()
        frm_BukuPengawasanPiutangUsaha_Ekspor_GBP.MdiParent = Me
        frm_BukuPengawasanPiutangUsaha_Ekspor_GBP.Show()
        frm_BukuPengawasanPiutangUsaha_Ekspor_GBP.Focus()
    End Sub


    Private Sub mnu_BukuPengawasanPiutangPihakKetiga_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangPihakKetiga.Click
        BukaModul_BukuPengawasanPiutangPihakKetiga()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangPihakKetiga()
        frm_BukuPengawasanPiutangPihakKetiga.MdiParent = Me
        frm_BukuPengawasanPiutangPihakKetiga.Show()
        frm_BukuPengawasanPiutangPihakKetiga.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangAfiliasi_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangAfiliasi.Click
        BukaModul_BukuPengawasanPiutangAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangAfiliasi()
        frm_BukuPengawasanPiutangAfiliasi.MdiParent = Me
        frm_BukuPengawasanPiutangAfiliasi.Show()
        frm_BukuPengawasanPiutangAfiliasi.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangKaryawan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangKaryawan.Click
        BukaModul_BukuPengawasanPiutangKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangKaryawan()
        frm_BukuPengawasanPiutangKaryawan.MdiParent = Me
        frm_BukuPengawasanPiutangKaryawan.Show()
        frm_BukuPengawasanPiutangKaryawan.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangPemegangSaham_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangPemegangSaham.Click
        BukaModul_BukuPengawasanPiutangPemegangSaham()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangPemegangSaham()
        frm_BukuPengawasanPiutangPemegangSaham.MdiParent = Me
        frm_BukuPengawasanPiutangPemegangSaham.Show()
        frm_BukuPengawasanPiutangPemegangSaham.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanDepositOperasional_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanDepositOperasional.Click
        BukaModul_BukuPengawasanDepositOperasional()
    End Sub
    Sub BukaModul_BukuPengawasanDepositOperasional()
        frm_BukuPengawasanDepositOperasional.MdiParent = Me
        frm_BukuPengawasanDepositOperasional.Show()
        frm_BukuPengawasanDepositOperasional.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangDividen_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangDividen.Click
        BukaModul_BukuPengawasanPiutangDividen()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangDividen()
        frm_BukuPengawasanPiutangDividen.MdiParent = Me
        frm_BukuPengawasanPiutangDividen.Show()
        frm_BukuPengawasanPiutangDividen.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanPiutangLainnya_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPiutangLainnya.Click
        MenuIniMasihDalamPengembangan()
    End Sub



    'KELOMPOK SUB MENU : BUKU PENGAWASAN BANK-CASH ---------------------------------------------------------------

    Private Sub mnu_BukuPengawasanBuktiPenerimaanBankCash_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanBuktiPenerimaanBankCash.Click
        BukaModul_BukuPengawasanBuktiPenerimaanBankCash()
    End Sub
    Sub BukaModul_BukuPengawasanBuktiPenerimaanBankCash()
        frm_BukuPengawasanBuktiPenerimaanBankCash.MdiParent = Me
        frm_BukuPengawasanBuktiPenerimaanBankCash.Show()
        frm_BukuPengawasanBuktiPenerimaanBankCash.Focus()
    End Sub


    Private Sub mnu_BukuPengawasanBuktiPengeluaranBankCash_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanBuktiPengeluaranBankCash.Click
        BukaModul_BukuPengawasanBuktiPengeluaranBankCash()
    End Sub
    Sub BukaModul_BukuPengawasanBuktiPengeluaranBankCash()
        frm_BukuPengawasanBuktiPengeluaranBankCash.MdiParent = Me
        frm_BukuPengawasanBuktiPengeluaranBankCash.Show()
        frm_BukuPengawasanBuktiPengeluaranBankCash.Focus()
    End Sub



    'KELOMPOK SUB MENU : BUKU PENGAWASAN PEMINDAHBUKUAN ---------------------------------------------------------------------------

    Private Sub mnu_BukuPengawasanPemindabukuan_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanPemindabukuan.Click
        frm_BukuPengawasanPemindahbukuan.MdiParent = Me
        frm_BukuPengawasanPemindahbukuan.Show()
        frm_BukuPengawasanPemindahbukuan.Focus()
    End Sub


    'KELOMPOK SUB MENU : BUKU PENGAWASAN AKTIVA LAINNYA ---------------------------------------------------------------------------

    Private Sub mnu_BukuPengawasanAktivaLainnya_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanAktivaLainnya.Click
        frm_BukuPengawasanAktivaLainnya.MdiParent = Me
        frm_BukuPengawasanAktivaLainnya.Show()
        frm_BukuPengawasanAktivaLainnya.Focus()
    End Sub




    'MENU : PAJAK =================================================================================================================

    'SUB MENU : BUKU PENGAWASAN HUTANG PPH ----------------------------------------------------------------------------------------

    'Buku Pengawasan Hutang PPh Pasal 21
    Private Sub mnu_BukuPengawasanHutangPPhPasal21_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal21.Click
        BukaModul_BukuPengawasanHutangPPhPasal21()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal21()
        frm_BukuPengawasanHutangPPhPasal21.MdiParent = Me
        frm_BukuPengawasanHutangPPhPasal21.Show()
        frm_BukuPengawasanHutangPPhPasal21.Focus()
    End Sub

    'Buku Pengawasan Hutang PPh Pasal 22
    Private Sub mnu_BukuPengawasanHutangPPhPasal22_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22.Click
    End Sub
    Private Sub mnu_BukuPengawasanHutangPPhPasal22_LokalClick(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22_Lokal.Click
        BukaModul_BukuPengawasanHutangPPhPasal22_Lokal()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal22_Lokal()
        frm_BukuPengawasanHutangPPhPasal22_Lokal.MdiParent = Me
        frm_BukuPengawasanHutangPPhPasal22_Lokal.Show()
        frm_BukuPengawasanHutangPPhPasal22_Lokal.Focus()
    End Sub
    Private Sub mnu_BukuPengawasanHutangPPhPasal22_ImporClick(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22_Impor.Click
        BukaModul_BukuPengawasanHutangPPhPasal22_Impor()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal22_Impor()
        frm_BukuPengawasanHutangPPhPasal22_Impor.MdiParent = Me
        frm_BukuPengawasanHutangPPhPasal22_Impor.Show()
        frm_BukuPengawasanHutangPPhPasal22_Impor.Focus()
    End Sub

    'Buku Pengawasan Hutang PPh Pasal 23
    Private Sub mnu_BukuPengawasanHutangPPhPasal23_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal23.Click
        BukaModul_BukuPengawasanHutangPPhPasal23()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal23()
        frm_BukuPengawasanHutangPPhPasal23.MdiParent = Me
        frm_BukuPengawasanHutangPPhPasal23.Show()
        frm_BukuPengawasanHutangPPhPasal23.Focus()
    End Sub

    'Buku Pengawasan Hutang PPh Pasal 4 (2)
    Private Sub mnu_BukuPengawasanHutangPPhPasal42_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal42.Click
        BukaModul_BukuPengawasanHutangPPhPasal42()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal42()
        frm_BukuPengawasanHutangPPhPasal42.MdiParent = Me
        frm_BukuPengawasanHutangPPhPasal42.Show()
        frm_BukuPengawasanHutangPPhPasal42.Focus()
    End Sub

    'Buku Pengawasan Hutang PPh Pasal 25
    Private Sub mnu_BukuPengawasanHutangPPhPasal25_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal25.Click
        BukaModul_BukuPengawasanHutangPPhPasal25()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal25()
        frm_BukuPengawasanHutangPPhPasal25.MdiParent = Me
        frm_BukuPengawasanHutangPPhPasal25.Show()
        frm_BukuPengawasanHutangPPhPasal25.Focus()
        'X_frm_BukuPengawasanHutangPPhPasal25_X.MdiParent = Me
        'X_frm_BukuPengawasanHutangPPhPasal25_X.Show()
        'X_frm_BukuPengawasanHutangPPhPasal25_X.Focus()
    End Sub

    'Buku Pengawasan Hutang PPh Pasal 26
    Private Sub mnu_BukuPengawasanHutangPPhPasal26_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal26.Click
        BukaModul_BukuPengawasanHutangPPhPasal26()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal26()
        frm_BukuPengawasanHutangPPhPasal26.MdiParent = Me
        frm_BukuPengawasanHutangPPhPasal26.Show()
        frm_BukuPengawasanHutangPPhPasal26.Focus()
    End Sub

    'Buku Pengawasan Hutang PPh Pasal 29
    Private Sub mnu_BukuPengawasanHutangPPhPasal29_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanHutangPPhPasal29.Click
        'Belum Ada Halaman Target
    End Sub

    Private Sub mnu_PPN_Click(sender As Object, e As EventArgs) Handles mnu_PPN.Click
        BukaModul_BukuPengawasanPelaporanPPN()
    End Sub
    Sub BukaModul_BukuPengawasanPelaporanPPN()
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            MenuDalamPerbaikan()
            Return
        End If
        frm_BukuPengawasanPelaporanPPN.MdiParent = Me
        frm_BukuPengawasanPelaporanPPN.Show()
        frm_BukuPengawasanPelaporanPPN.Focus()
    End Sub


    Private Sub mnu_KetetapanPajak_Click(sender As Object, e As EventArgs) Handles mnu_KetetapanPajak.Click
        BukaModul_BukuPengawasanKetetapanPajak()
    End Sub
    Sub BukaModul_BukuPengawasanKetetapanPajak()
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            MenuDalamPerbaikan()
            Return
        End If
        frm_BukuPengawasanKetetapanPajak.MdiParent = Me
        frm_BukuPengawasanKetetapanPajak.Show()
        frm_BukuPengawasanKetetapanPajak.Focus()
    End Sub


    Private Sub mnu_PajakImpor_Click(sender As Object, e As EventArgs) Handles mnu_PajakImpor.Click
        BukaModul_BukuPengawasanPajakImpor()
    End Sub
    Sub BukaModul_BukuPengawasanPajakImpor()
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            MenuIniMasihDalamPengembangan()
            Return
        End If
        frm_BukuPengawasanPajakImpor.MdiParent = Me
        frm_BukuPengawasanPajakImpor.Show()
        frm_BukuPengawasanPajakImpor.Focus()
    End Sub


    Private Sub mnu_ProfilPajakPerusahaan_Click(sender As Object, e As EventArgs) Handles mnu_ProfilPajakPerusahaan.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_InputBuktiPBk_Click(sender As Object, e As EventArgs) Handles mnu_InputBuktiPBk.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_InputKetetapanPajak_Click(sender As Object, e As EventArgs) Handles mnu_InputKetetapanPajak.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_PerhitunganEqualisasiPajakPajakTahunan_Click(sender As Object, e As EventArgs) Handles mnu_PerhitunganEqualisasiPajakPajakTahunan.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPotongPPh_Paid_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanBuktiPotongPPh_Paid.Click
        frm_BukuPengawasanBuktiPotongPPh_Paid.MdiParent = Me
        frm_BukuPengawasanBuktiPotongPPh_Paid.Show()
        frm_BukuPengawasanBuktiPotongPPh_Paid.Focus()
        'X_frm_BukuPengawasanBuktiPotongPPh_Paid.MdiParent = Me
        'X_frm_BukuPengawasanBuktiPotongPPh_Paid.Show()
        'X_frm_BukuPengawasanBuktiPotongPPh_Paid.Focus()
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPotongPPh_Prepaid_Click(sender As Object, e As EventArgs) Handles mnu_BukuPengawasanBuktiPotongPPh_Prepaid.Click
        frm_BukuPengawasanBuktiPotongPPh_Prepaid.MdiParent = Me
        frm_BukuPengawasanBuktiPotongPPh_Prepaid.Show()
        frm_BukuPengawasanBuktiPotongPPh_Prepaid.Focus()
        'X_frm_BukuPengawasanBuktiPotongPPh_Prepaid.MdiParent = Me
        'X_frm_BukuPengawasanBuktiPotongPPh_Prepaid.Show()
        'X_frm_BukuPengawasanBuktiPotongPPh_Prepaid.Focus()
    End Sub



    'MENU : STOCK OPNAME ==========================================================================================================
    Private Sub mnu_StockOpname_Click(sender As Object, e As EventArgs) Handles mnu_StockOpname.Click
    End Sub
    Sub BukaModul_StockOpname()
        If frm_StockOpname.JenisStok_Menu = Kosongan Then
            PesanUntukProgrammer("Tentukan Dulu Jenis Stoknya")
            Return
        End If
        frm_StockOpname.MdiParent = Me
        frm_StockOpname.Show()
        frm_StockOpname.Focus()
    End Sub

    Private Sub mnu_StockOpname_BahanPenolong_Click(sender As Object, e As EventArgs) Handles mnu_StockOpname_BahanPenolong.Click
        BukaModul_StockOpname_BahanPenolong()
    End Sub
    Sub BukaModul_StockOpname_BahanPenolong()
        frm_BahanPenolong.Close()
        frm_BahanPenolong.MdiParent = Me
        frm_BahanPenolong.Show()
        frm_BahanPenolong.Focus()
    End Sub

    Private Sub mnu_StockOpname_BahanBaku_Click(sender As Object, e As EventArgs) Handles mnu_StockOpname_BahanBaku.Click
        BukaModul_StockOpname_BahanBaku()
    End Sub
    Sub BukaModul_StockOpname_BahanBaku()
        frm_BahanBaku.Close()
        frm_BahanBaku.MdiParent = Me
        frm_BahanBaku.Show()
        frm_BahanBaku.Focus()
    End Sub
    Private Sub mnu_StockOpname_BarangDalamProses_Click(sender As Object, e As EventArgs) Handles mnu_StockOpname_BarangDalamProses.Click
    End Sub

    Private Sub mnu_StockOpname_BarangDalamProses_CekFisik_Click(sender As Object, e As EventArgs) Handles mnu_StockOpname_BarangDalamProses_CekFisik.Click
        BukaModul_StockOpname_BarangDalamProses_CekFisik()
    End Sub
    Sub BukaModul_StockOpname_BarangDalamProses_CekFisik()
        frm_BarangDalamProses_CekFisik.Close()
        frm_BarangDalamProses_CekFisik.MdiParent = Me
        frm_BarangDalamProses_CekFisik.Show()
        frm_BarangDalamProses_CekFisik.Focus()
    End Sub

    Private Sub mnu_StockOpname_BarangDalamProses_TarikanData_Click(sender As Object, e As EventArgs) Handles mnu_StockOpname_BarangDalamProses_TarikanData.Click
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            MenuIniMasihDalamPengembangan()
            Return
        End If
        BukaModul_StockOpnameBarangDalamProses_TarikanData()
    End Sub
    Sub BukaModul_StockOpnameBarangDalamProses_TarikanData()
        frm_BarangDalamProses_TarikanData.MdiParent = Me
        frm_BarangDalamProses_TarikanData.Show()
        frm_BarangDalamProses_TarikanData.Focus()
    End Sub
    Private Sub mnu_StockOpname_BarangJadi_Click(sender As Object, e As EventArgs) Handles mnu_StockOpname_BarangJadi.Click
        BukaModul_StockOpname_BarangJadi()
    End Sub
    Sub BukaModul_StockOpname_BarangJadi()
        frm_BarangJadi.Close()
        frm_BarangJadi.MdiParent = Me
        frm_BarangJadi.Show()
        frm_BarangJadi.Focus()
    End Sub



    'MENU : AKUNTANSI =============================================================================================================

    Private Sub mnu_Jurnal_Click(sender As Object, e As EventArgs) Handles mnu_Jurnal.Click
    End Sub

    Private Sub mnu_JurnalUmum_Click(sender As Object, e As EventArgs) Handles mnu_JurnalUmum.Click
        frm_JurnalUmum.MdiParent = Me
        frm_JurnalUmum.Show()
        frm_JurnalUmum.Focus()
    End Sub


    Private Sub mnu_JurnalAdjusment_Click(sender As Object, e As EventArgs) Handles mnu_JurnalAdjusment.Click
    End Sub

    Private Sub mnu_JurnalAdjusment_Penyusutan_Click(sender As Object, e As EventArgs) Handles mnu_JurnalAdjusment_Penyusutan.Click
        BukaHalamanAdjusmentPenyusutanAsset()
    End Sub
    Sub BukaHalamanAdjusmentPenyusutanAsset()
        frm_Adjusment_PenyusutanAsset.MdiParent = Me
        frm_Adjusment_PenyusutanAsset.Show()
        frm_Adjusment_PenyusutanAsset.Focus()
    End Sub

    Private Sub mnu_JurnalAdjusment_Amortisasi_Click(sender As Object, e As EventArgs) Handles mnu_JurnalAdjusment_Amortisasi.Click
        BukaHalamanAdjusmentAmortisasi()
    End Sub
    Sub BukaHalamanAdjusmentAmortisasi()
        frm_Adjusment_Amortisasi.MdiParent = Me
        frm_Adjusment_Amortisasi.Show()
        frm_Adjusment_Amortisasi.Focus()
    End Sub

    Private Sub mnu_JurnalAdjusment_Forex_Click(sender As Object, e As EventArgs) Handles mnu_JurnalAdjusment_Forex.Click
        frm_Adjusment_Forex.MdiParent = Me
        frm_Adjusment_Forex.Show()
        frm_Adjusment_Forex.Focus()
    End Sub

    Private Sub mnu_JurnalAdjusment_HPP_Click(sender As Object, e As EventArgs) Handles mnu_JurnalAdjusment_HPP.Click
        'Cek Dulu Kelengkapan Adjusment Penyusutan Asset :
        frm_Adjusment_PenyusutanAsset.CekAdjusment()
        If Not usc_Adjusment_PenyusutanAsset.AdjusmentBulanBukuAktifSudahLengkap Then
            PesanPemberitahuan("Silakan lengkapi dulu Adjusment Penyusutan Asset untuk Bulan " & KonversiAngkaKeBulanString(BulanBukuAktif) & ", baru masuk ke menu ini.")
            Return
        End If
        'Cek Dulu Kelengkapan Adjusment Amortisasi :
        frm_Adjusment_Amortisasi.CekAdjusment()
        If Not usc_Adjusment_Amortisasi.AdjusmentBulanBukuAktifSudahLengkap Then
            PesanPemberitahuan("Silakan lengkapi dulu Adjusment Amortisasi untuk Bulan " & KonversiAngkaKeBulanString(BulanBukuAktif) & ", baru masuk ke menu ini.")
            Return
        End If
        'Cek Dulu Kelengkapan Adjusment Forex :
        frm_Adjusment_Forex.CekAdjusment()
        If Not usc_Adjusment_Forex.AdjusmentBulanBukuAktifSudahLengkap Then
            PesanPemberitahuan("Silakan tuntaskan dulu Adjusment Forex untuk Bulan " & KonversiAngkaKeBulanString(BulanBukuAktif) & ", baru masuk ke menu ini.")
            Return
        End If
        frm_Adjusment_HPP.MdiParent = Me
        frm_Adjusment_HPP.Show()
        frm_Adjusment_HPP.Focus()
    End Sub


    'Sub Menu : BUKU BESAR --------------------------------------------------------------------------------------------------------
    Private Sub mnu_BukuBesar_Click(sender As Object, e As EventArgs) Handles mnu_BukuBesar.Click
        BukaModul_BukuBesar(Kosongan)
    End Sub
    Sub BukaModul_BukuBesar(AkunTerpilih)
        frm_BukuBesar.Close() 'Ini jangan dihapus. Ini dibutuhkan untuk mereset tampilan Buku Besar.
        frm_BukuBesar.MdiParent = Me
        frm_BukuBesar.AkunTerpilih = AkunTerpilih
        frm_BukuBesar.Show()
        frm_BukuBesar.Focus()
    End Sub

    Private Sub mnu_Laporan_Click(sender As Object, e As EventArgs) Handles mnu_Laporan.Click
    End Sub

    'Sub Menu : LAPORAN DATA TRANSAKSI --------------------------------------------------------------------------------------------
    Private Sub mnu_LaporanAktivitasTransaksi_Click(sender As Object, e As EventArgs) Handles mnu_LaporanAktivitasTransaksi.Click
        frm_LaporanAktivitasTransaksi.MdiParent = Me
        frm_LaporanAktivitasTransaksi.Show()
        frm_LaporanAktivitasTransaksi.Focus()
    End Sub


    'Sub Menu : LAPORAN -----------------------------------------------------------------------------------------------------------
    'Sub Menu : LAPORAN >> Trial Balance ------------------------------------------------------------------------------------------
    Private Sub mnu_TrialBalance_Click_1(sender As Object, e As EventArgs) Handles mnu_TrialBalance.Click
        BukaModul_LaporanTrialBalance()
    End Sub
    Sub BukaModul_LaporanTrialBalance()
        frm_LaporanTrialBalance.MdiParent = Me
        frm_LaporanTrialBalance.JalurMasuk = Halaman_MENUUTAMA
        frm_LaporanTrialBalance.Show()
        frm_LaporanTrialBalance.Focus()
    End Sub

    'Sub Menu : LAPORAN >> Neraca Lajur -------------------------------------------------------------------------------------------
    Private Sub mnu_NeracaLajur_Click(sender As Object, e As EventArgs) Handles mnu_NeracaLajur.Click
        frm_LaporanNeracaLajur.MdiParent = Me
        frm_LaporanNeracaLajur.Show()
        frm_LaporanNeracaLajur.Focus()
    End Sub

    Private Sub mnu_LaporanKeuangan_Click(sender As Object, e As EventArgs) Handles mnu_LaporanKeuangan.Click
    End Sub

    'Sub Menu : LAPORAN >> HPP ----------------------------------------------------------------------------------------------------
    Private Sub mnu_LaporanHPP_Click(sender As Object, e As EventArgs) Handles mnu_LaporanHPP.Click
        BukaHalaman_LaporanHPP()
    End Sub
    Sub BukaHalaman_LaporanHPP()
        frm_LaporanHPP.MdiParent = Me
        frm_LaporanHPP.Show()
        frm_LaporanHPP.Focus()
    End Sub

    'Sub Menu : LAPORAN >> Neraca -------------------------------------------------------------------------------------------------
    Private Sub mnu_Neraca_Click_1(sender As Object, e As EventArgs) Handles mnu_Neraca.Click
    End Sub
    Private Sub mnu_Neraca_Bulanan_Click(sender As Object, e As EventArgs) Handles mnu_Neraca_Bulanan.Click
        BukaHalaman_LaporanNeraca_Bulanan()
    End Sub
    Sub BukaHalaman_LaporanNeraca_Bulanan()
        frm_LaporanNeraca_Bulanan.MdiParent = Me
        frm_LaporanNeraca_Bulanan.Show()
        frm_LaporanNeraca_Bulanan.Focus()
    End Sub
    Private Sub mnu_Neraca_Tahunan_Click(sender As Object, e As EventArgs) Handles mnu_Neraca_Tahunan.Click
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            MenuIniMasihDalamPengembangan()
            Return
        End If
        frm_LaporanNeraca_Tahunan.MdiParent = Me
        frm_LaporanNeraca_Tahunan.Show()
        frm_LaporanNeraca_Tahunan.Focus()
    End Sub

    'Sub Menu : LAPORAN >> Laba/Rugi ----------------------------------------------------------------------------------------------
    Private Sub mnu_LabaRugi_Click_1(sender As Object, e As EventArgs) Handles mnu_LabaRugi.Click
    End Sub
    Private Sub mnu_LabaRugi_Bulanan_Click(sender As Object, e As EventArgs) Handles mnu_LabaRugi_Bulanan.Click
        BukaHalaman_LaporanLabaRugi_Bulanan()
    End Sub
    Sub BukaHalaman_LaporanLabaRugi_Bulanan()
        frm_LaporanLabaRugi_Bulanan.MdiParent = Me
        frm_LaporanLabaRugi_Bulanan.Show()
        frm_LaporanLabaRugi_Bulanan.Focus()
    End Sub
    Private Sub mnu_LabaRugi_Tahunan_Click(sender As Object, e As EventArgs) Handles mnu_LabaRugi_Tahunan.Click
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            MenuIniMasihDalamPengembangan()
            Return
        End If
        frm_LaporanLabaRugi_Tahunan.MdiParent = Me
        frm_LaporanLabaRugi_Tahunan.Show()
        frm_LaporanLabaRugi_Tahunan.Focus()
    End Sub


    'Sub Menu : LAPORAN >> Aktivitas Transaksi ------------------------------------------------------------------------------------
    Private Sub mnu_LaporanAktivitasTransaksi_Click_1(sender As Object, e As EventArgs) Handles mnu_LaporanAktivitasTransaksi.Click
        frm_JurnalUmum.MdiParent = Me
        frm_JurnalUmum.Show()
        frm_JurnalUmum.Focus()
    End Sub


    'KELOMPOK MENU : MANAJEMEN ASSET ==============================================================================================

    'Sub Menu : Manemen Amortisasi :

    'Sub Menu : Manemen Amortisasi >> Amortisasi Biaya :
    Private Sub mnu_ManajemenAmortisasiBiaya_Click(sender As Object, e As EventArgs) Handles mnu_ManajemenAmortisasiBiaya.Click
        BukaModul_ManajemenAmortisasiBiaya()
    End Sub
    Sub BukaModul_ManajemenAmortisasiBiaya()
        frm_DaftarAmortisasiBiaya.MdiParent = Me
        frm_DaftarAmortisasiBiaya.Show()
        frm_DaftarAmortisasiBiaya.Focus()
    End Sub
    'Sub Menu : Manemen Amortisasi >> Amortisasi AssetTidakBerwujud :
    Private Sub mnu_ManajemenAmortisasiAssetTidakBerwujud_Click(sender As Object, e As EventArgs) Handles mnu_ManajemenAmortisasiAssetTidakBerwujud.Click
        FiturDalamPengembangan()
    End Sub

    'Sub Menu : Manemen Asset Tetap :

    'Sub Menu : Manemen Asset Tetap >> Daftar Penyusutan Asset Tetap :
    Private Sub mnu_DaftarPenyusutanAssetTetap_Click(sender As Object, e As EventArgs) Handles mnu_DaftarPenyusutanAssetTetap.Click
        BukaModul_DaftarPenyusutanAssetTetap()
    End Sub
    Sub BukaModul_DaftarPenyusutanAssetTetap()
        frm_DaftarPenyusutanAssetTetap.JalurMasuk = Halaman_MENUUTAMA
        frm_DaftarPenyusutanAssetTetap.MdiParent = Me
        frm_DaftarPenyusutanAssetTetap.Show()
        frm_DaftarPenyusutanAssetTetap.Focus()
    End Sub
    'Sub Menu : Manemen Asset Tetap >> Buku Penjualan Asset Tetap :
    Private Sub mnu_BukuPenjualanAssetTetap_Click(sender As Object, e As EventArgs) Handles mnu_BukuPenjualanAssetTetap.Click
        BukaModul_BukuPenjualanAsset()
    End Sub
    Public Sub BukaModul_BukuPenjualanAsset()
        frm_BukuPenjualanAsset.Close()
        frm_BukuPenjualanAsset.MdiParent = Me
        frm_BukuPenjualanAsset.Show()
        frm_BukuPenjualanAsset.Focus()
    End Sub

    'Sub Menu : Manemen Asset Tetap >> Buku Disposal Asset Tetap :
    Private Sub mnu_BukuDisposalAssetTetap_Click(sender As Object, e As EventArgs) Handles mnu_BukuDisposalAssetTetap.Click
        frm_BukuDisposalAssetTetap.MdiParent = Me
        frm_BukuDisposalAssetTetap.Show()
        frm_BukuDisposalAssetTetap.Focus()
    End Sub


    'KELOMPOK MENU : USER =========================================================================================================

    'Switch User
    Private Sub mnu_SwitchUser_Click(sender As Object, e As EventArgs) Handles mnu_SwitchUser.Click
        StatusMenuPosisiLogout()
        KeluarDariSemuaModul()
        BukaFormLogin()
        If LevelUserAktif < LevelUser_81_TimIT Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ORDER BY Tahun_Buku ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Do While dr.Read 'Loop ini untuk mengambil value Tahun Buku yang paling akhir.
                TahunBukuBaru = dr.Item("Tahun_Buku")
            Loop
            AksesDatabase_General(Tutup)
            If TahunBukuBaru <> TahunBukuAktif Then
                win_GantiTahunBuku = New wpfWin_GantiTahunBuku
                win_GantiTahunBuku.ProsesGantiTahunBuku()
            End If
        End If
    End Sub

    'Ganti Password
    Private Sub mnu_GantiPassword_Click(sender As Object, e As EventArgs) Handles mnu_GantiPassword.Click
        win_GantiPassword = New wpfWin_GantiPassword
        win_GantiPassword.ResetForm()
        win_GantiPassword.ShowDialog()
    End Sub

    'Ganti Peran : Operator
    Private Sub mnu_PeranOperator_Click(sender As Object, e As EventArgs) Handles mnu_PeranOperator.Click
        KeluarDariSemuaModul()
        StatusMenuLevel_1_Operator()
        LevelUserAktif = LevelUser_01_Operator
        tls_UserAktif.Text = "User  :  " & NamaUserAktif & "  |  " & JabatanUserAktif & "  -->  " & JabatanUser_Operator & "  |  " & KeteranganCluster
        MsgBox("Anda sekarang berperan sebagai Operator.")
        mnu_PeranOperator.Enabled = False
        mnu_PeranManager.Enabled = True
        mnu_PeranDirektur.Enabled = True
        mnu_PeranTimIT.Enabled = True
        mnu_PeranAppDeveloper.Enabled = True
    End Sub

    'Ganti Peran : Manager
    Private Sub mnu_PeranManager_Click(sender As Object, e As EventArgs) Handles mnu_PeranManager.Click
        KeluarDariSemuaModul()
        StatusMenuLevel_2_Manager()
        LevelUserAktif = LevelUser_02_Manager
        tls_UserAktif.Text = "User  :  " & NamaUserAktif & "  |  " & JabatanUserAktif & "  -->  " & JabatanUser_Manager & "  |  " & KeteranganCluster
        MsgBox("Anda sekarang berperan sebagai Manager.")
        mnu_PeranOperator.Enabled = True
        mnu_PeranManager.Enabled = False
        mnu_PeranDirektur.Enabled = True
        mnu_PeranTimIT.Enabled = True
        mnu_PeranAppDeveloper.Enabled = True
    End Sub

    'Ganti Peran : Direktur
    Private Sub mnu_PeranDirektur_Click(sender As Object, e As EventArgs) Handles mnu_PeranDirektur.Click
        KeluarDariSemuaModul()
        StatusMenuLevel_3_Direktur()
        LevelUserAktif = LevelUser_03_Direktur
        tls_UserAktif.Text = "User  :  " & NamaUserAktif & "  |  " & JabatanUserAktif & "  -->  " & JabatanUser_Direktur & "  |  " & KeteranganCluster
        MsgBox("Anda sekarang berperan sebagai Direktur.")
        mnu_PeranOperator.Enabled = True
        mnu_PeranManager.Enabled = True
        mnu_PeranDirektur.Enabled = False
        mnu_PeranTimIT.Enabled = True
        mnu_PeranAppDeveloper.Enabled = True
    End Sub

    'Ganti Peran : Tim IT
    Private Sub mnu_PeranTimIT_Click(sender As Object, e As EventArgs) Handles mnu_PeranTimIT.Click
        KeluarDariSemuaModul()
        StatusMenuLevel_81_TimIT()
        LevelUserAktif = LevelUser_81_TimIT '(81)
        tls_UserAktif.Text = "User  :  " & NamaUserAktif & "  |  " & JabatanUserAktif & "  -->  " & JabatanUser_TimIT & "  |  " & KeteranganCluster
        MsgBox("Anda sekarang berperan sebagai Tim IT Developer.")
        mnu_PeranOperator.Enabled = True
        mnu_PeranManager.Enabled = True
        mnu_PeranDirektur.Enabled = True
        mnu_PeranTimIT.Enabled = False
        mnu_PeranAppDeveloper.Enabled = True
    End Sub

    'Ganti Peran : App Developer
    Private Sub mnu_PeranAppDeveloper_Click(sender As Object, e As EventArgs) Handles mnu_PeranAppDeveloper.Click
        KeluarDariSemuaModul()
        StatusMenuLevel_99_AppDeveloper()
        LevelUserAktif = LevelUser_99_AppDeveloper '(99)
        tls_UserAktif.Text = "User  :  " & NamaUserAktif & "  |  " & JabatanUserAktif & "  -->  " & JabatanUser_AppDeveloper & "  |  " & KeteranganCluster
        MsgBox("Anda sekarang berperan sebagai Developer.")
        mnu_PeranOperator.Enabled = True
        mnu_PeranManager.Enabled = True
        mnu_PeranDirektur.Enabled = True
        mnu_PeranTimIT.Enabled = True
        mnu_PeranAppDeveloper.Enabled = False
    End Sub

    'Login/Logout
    Private Sub mnu_Log_Click(sender As Object, e As EventArgs) Handles mnu_Log.Click
        If mnu_Log.Text = "Login" Then
            BukaFormLogin()
        Else 'Jika teks Menu "Logout" maka
            StatusMenuPosisiLogout()
            KeluarDariSemuaModul()
            MsgBox("Anda telah LOGOUT dari sistem.")
        End If
    End Sub





    'KELOMPOK MENU : JENDELA ======================================================================================================

    Private Sub mnu_Jendela_Click(sender As Object, e As EventArgs) Handles mnu_Jendela.Click

    End Sub


    '--------------------------------------------------------------------------------------------------------------
    'Sub Menu : Tutup Semua Jendela
    Private Sub mnu_Jendela_TutupSemua_Click(sender As Object, e As EventArgs) Handles mnu_Jendela_TutupSemua.Click
        KeluarDariSemuaModul()
    End Sub





    'MENU : TENTANG ===============================================================================================================
    Private Sub mnu_Tentang_Click(sender As Object, e As EventArgs) Handles mnu_Tentang.Click
        frm_Tentang.ShowDialog()
    End Sub







    'MENU : HELP ==================================================================================================================
    Private Sub mnu_Help_Click(sender As Object, e As EventArgs) Handles mnu_Help.Click
        frm_Help.ShowDialog()
    End Sub



    'MENU : TECHNICAL SUPPORT =====================================================================================================
    Private Sub mnu_PhpMyAdmin_Click(sender As Object, e As EventArgs) Handles mnu_PhpMyAdmin.Click
        frm_phpMyAdmin.Show()
        frm_phpMyAdmin.Focus()
    End Sub



    'MENU : APP DEVELOPER =========================================================================================================
    Private Sub mnu_ManajemenAplikasi_Click(sender As Object, e As EventArgs) Handles mnu_ManajemenAplikasi.Click
        frm_ManajemenAplikasi.MdiParent = Me
        frm_ManajemenAplikasi.Show()
        frm_ManajemenAplikasi.Focus()
    End Sub


    Private Sub mnu_ManajemenClient_Click(sender As Object, e As EventArgs) Handles mnu_ManajemenClient.Click
        frm_ManajemenClient.MdiParent = Me
        frm_ManajemenClient.Show()
        frm_ManajemenClient.Focus()
    End Sub

    Private Sub mnu_ManajemenKurs_Click(sender As Object, e As EventArgs) Handles mnu_ManajemenKurs.Click
        frm_ManajemenKurs.MdiParent = Me
        frm_ManajemenKurs.Show()
        frm_ManajemenKurs.Focus()
    End Sub


    Private Sub mnu_DataProduk_Click(sender As Object, e As EventArgs) Handles mnu_DataProduk.Click
        frm_DataProdukApp.MdiParent = Me
        frm_DataProdukApp.Show()
        frm_DataProdukApp.Focus()
    End Sub

    Private Sub mnu_DataPerangkat_Click(sender As Object, e As EventArgs) Handles mnu_DataPerangkat.Click
        frm_DataPerangkatApp.MdiParent = Me
        frm_DataPerangkatApp.Show()
        frm_DataPerangkatApp.Focus()
    End Sub

    Private Sub mnu_DataVoucher_Click(sender As Object, e As EventArgs) Handles mnu_DataVoucher.Click
        frm_DataVoucherApp.MdiParent = Me
        frm_DataVoucherApp.Show()
        frm_DataVoucherApp.Focus()
    End Sub

    Private Sub mnu_TabPokok_Click(sender As Object, e As EventArgs) Handles mnu_TabPokok.Click
        frm_TabPokok.Show()
        frm_TabPokok.Focus()
    End Sub

    Private Sub mnu_TryApp_Click(sender As Object, e As EventArgs) Handles mnu_TryApp.Click
        frm_TryApp.ShowDialog()
    End Sub




    '------------------------------------------------------------------------------------------------------------------------------------------------
    '||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '=============================================================== BATAS AKHIR MENU ===============================================================
    '||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '------------------------------------------------------------------------------------------------------------------------------------------------




    Sub SembunyikanSemuaMenu()
        mnu_File.Visible = False
        mnu_Data.Visible = False
        mnu_Transaksi.Visible = False
        mnu_Pembelian.Visible = False
        mnu_Penjualan.Visible = False
        mnu_BukuPengawasan.Visible = False
        mnu_Pengajuan.Visible = False
        mnu_StockOpname.Visible = False
        mnu_Akuntansi.Visible = False
        mnu_ManajemenAsset.Visible = False
        mnu_Pajak.Visible = False
        mnu_User.Visible = False
        mnu_Help.Visible = False
        mnu_Jendela.Visible = False
        mnu_Tentang.Visible = False
        mnu_Registrasi.Visible = False
        mnu_TechnicalSupport.Visible = False
        mnu_AppDeveloper.Visible = False
        mnu_Notifikasi.Visible = False
    End Sub

    Sub TampilkanSemuaMenu()
        mnu_File.Visible = True
        mnu_Data.Visible = True
        mnu_ManajemenAsset.Visible = True
        mnu_Pajak.Visible = True
        mnu_Jendela.Visible = True
        mnu_Tentang.Visible = True
        mnu_User.Visible = True
        mnu_Help.Visible = True
        mnu_AppDeveloper.Visible = True
        mnu_TechnicalSupport.Visible = True
        mnu_Registrasi.Visible = True
        mnu_DataCOA.Enabled = False
        mnu_DataMitra.Enabled = False
        mnu_TahunBuku.Visible = False
        mnu_StockOpname.Visible = True
        If ClusterFinance = 1 Then
            mnu_Transaksi.Visible = True
            mnu_Pembelian.Visible = True
            mnu_Penjualan.Visible = True
            mnu_BukuPengawasan.Visible = True
            If SistemApprovalPerusahaan = True Then mnu_Pengajuan.Visible = True
            mnu_BukuPengawasanGaji_Induk.Visible = True
            mnu_DataMitra.Enabled = True
        End If
        If ClusterAccounting = 1 Then
            mnu_Akuntansi.Visible = True
            mnu_DataCOA.Enabled = True
            mnu_TahunBuku.Visible = True
        End If
        mnu_Notifikasi.Visible = True
    End Sub

    Sub StatusMenuPosisiLogin()
        SembunyikanSemuaMenu()
        TampilkanSemuaMenu()
        mnu_Log.Text = "Logout"
        mnu_SwitchUser.Enabled = True
        mnu_GantiPassword.Enabled = True
    End Sub

    Sub StatusMenuPosisiLogout()
        SembunyikanSemuaMenu()
        mnu_File.Visible = True
        mnu_Tentang.Visible = True
        mnu_User.Visible = True
        mnu_Log.Text = "Login"
        mnu_SwitchUser.Enabled = False
        mnu_GantiPassword.Enabled = False
        mnu_GantiPeran.Visible = False
        mnu_Help.Visible = True
        Me.Text = NamaAplikasi
        tls_UserAktif.Text = "User :"
    End Sub

    Sub StatusMenuLevel_1_Operator() 'Operator
        StatusMenuLevel_2_Manager()
        If ClusterFinance = 1 Then mnu_Transaksi.Visible = True
        mnu_Akuntansi.Visible = False
        mnu_Pengajuan.Text = "Pengajuan"
        mnu_PengajuanPembayaranPembelianTunai.Text = "Pengajuan Pembayaran Pembelian Tunai"
        mnu_PengajuanPembayaranHutangUsaha.Text = "Pengajuan Pembayaran Hutang Usaha"
        mnu_PengajuanPembayaranHutangPajak.Text = "Pengajuan Pembayaran Hutang Pajak"
        mnu_PengajuanPembayaranHutangBank.Text = "Pengajuan Pembayaran Hutang Bank"
        mnu_PengajuanPembayaranHutangAfiliasi.Text = "Pengajuan Pembayaran Hutang Afiliasi"
        mnu_PengajuanPembayaranHutangLainnya.Text = "Pengajuan Pembayaran Hutang Lainnya"
        mnu_PengajuanPembayaranKasbon.Text = "Pengajuan Pembayaran Kasbon"
        mnu_PengajuanPembayaranInvestasi.Text = "Pengajuan Pembayaran Investasi"
        mnu_PengajuanPemindahbukuan.Text = "Pengajuan Pemindahbukuan"
        mnu_PengajuanLainnya.Text = "Pengajuan Lainnya"
        mnu_PengajuanPO.Text = "Pengajuan PO"
    End Sub

    Sub StatusMenuLevel_2_Manager() 'Manager
        StatusMenuLevel_3_Direktur()
    End Sub

    Sub StatusMenuLevel_3_Direktur() 'Direktur
        StatusMenuLevel_4_GeneralUser()
    End Sub

    Sub StatusMenuLevel_4_GeneralUser() 'User Umum
        StatusMenuLevel_9_SuperUser()
    End Sub

    Sub StatusMenuLevel_9_SuperUser() 'Super User
        StatusMenuLevel_81_TimIT()
        mnu_Pengaturan.Enabled = False
        mnu_TechnicalSupport.Visible = False
        mnu_Registrasi.Visible = False
        mnu_BuatBukuBaru.Enabled = False
        mnu_GantiTahunBuku.Enabled = False
    End Sub

    Sub StatusMenuLevel_81_TimIT() 'Tim IT
        StatusMenuLevel_99_AppDeveloper()
        mnu_AppDeveloper.Visible = False
        mnu_TrialBalance.Visible = False
    End Sub

    Sub StatusMenuLevel_99_AppDeveloper() 'Master Developer
        StatusMenuPosisiLogin()
        mnu_Pengaturan.Enabled = True
        If SistemApprovalPerusahaan = True Then
            mnu_Transaksi.Visible = False
        Else
            mnu_Transaksi.Visible = True
        End If
        mnu_Pengajuan.Text = "Persetujuan"
        mnu_PengajuanPembayaranPembelianTunai.Text = "Persetujuan Pembayaran Pembelian Tunai"
        mnu_PengajuanPembayaranHutangUsaha.Text = "Persetujuan Pembayaran Hutang Usaha"
        mnu_PengajuanPembayaranHutangPajak.Text = "Persetujuan Pembayaran Hutang Pajak"
        mnu_PengajuanPembayaranHutangBank.Text = "Persetujuan Pembayaran Hutang Bank"
        mnu_PengajuanPembayaranHutangAfiliasi.Text = "Persetujuan Pembayaran Hutang Afiliasi"
        mnu_PengajuanPembayaranHutangLainnya.Text = "Persetujuan Pembayaran Hutang Lainnya"
        mnu_PengajuanPembayaranKasbon.Text = "Persetujuan Pembayaran Kasbon"
        mnu_PengajuanPembayaranInvestasi.Text = "Persetujuan Pembayaran Investasi"
        mnu_PengajuanPemindahbukuan.Text = "Persetujuan Pemindahbukuan"
        mnu_PengajuanLainnya.Text = "Persetujuan Lainnya"
        mnu_PengajuanPO.Text = "Persetujuan PO"
        mnu_BuatBukuBaru.Enabled = True
        mnu_GantiTahunBuku.Enabled = True
    End Sub

    Private Sub mnu_Registrasi_Click(sender As Object, e As EventArgs) Handles mnu_Registrasi.Click
        If VersiBooku_SisiAplikasi <> VersiBooku_SisiPublic Or ApdetBooku_SisiAplikasi <> ApdetBooku_SisiPublic Then
            PesanPeringatan("Registrasi hanya bisa dilakukan jika aplikasi sudah sesuai dengan Update terbaru." & Enter2Baris &
                            "Silakan lakukan update terlebih dahulu.")
            Return
        End If
        win_Registrasi = New wpfWin_Registrasi
        win_Registrasi.ResetForm()
        win_Registrasi.RegistrasiTambahan = True
        win_Registrasi.ShowDialog()
    End Sub

    Public PaksaKeluarAplikasi As Boolean = False
    Private Sub KeluarAplikasi(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If PaksaKeluarAplikasi Then
            e.Cancel = False
        Else
            Pilihan = MessageBox.Show("Yakin akan keluar dari aplikasi..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then e.Cancel = True
            If Pilihan = vbYes Then e.Cancel = False
        End If
    End Sub

    Private Sub mnu_Notifikasi_Click(sender As Object, e As EventArgs) Handles mnu_Notifikasi.Click
        Select Case VisibilitasNotifikasi
            Case True
                TutupPanelNotifikasi()
            Case False
                TampilkanPanelNotifikasi()
        End Select
    End Sub

    Public Sub IsiKontenNotifikasi()
        dgv_Notifikasi.Rows.Clear()
        dgv_Notifikasi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Notifikasi " &
                              " WHERE Status_Dibaca = 0 " &
                              " OR Status_Dieksekusi = 0", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            Dim NomorID
            Dim JenisNotifikasi
            Dim WaktuNotifikasi
            Dim Notifikasi
            Dim HalamanTarget
            Dim Pesan
            Dim StatusDibaca
            Dim StatusDieksekusi
            NomorID = dr.Item("Nomor_ID")
            JenisNotifikasi = dr.Item("Jenis_Notifikasi")
            WaktuNotifikasi = dr.Item("Waktu")
            Notifikasi = dr.Item("Notifikasi")
            HalamanTarget = dr.Item("Halaman_Target")
            Pesan = dr.Item("Pesan")
            StatusDibaca = dr.Item("Status_Dibaca")
            StatusDieksekusi = dr.Item("Status_Dieksekusi")
            dgv_Notifikasi.Rows.Add(NomorID, JenisNotifikasi, WaktuNotifikasi, WaktuNotifikasi & " :" & Enter1Baris & Notifikasi,
                                    HalamanTarget, Pesan, StatusDibaca, StatusDieksekusi)
        Loop
        AksesDatabase_Transaksi(Tutup)
        BeginInvoke(Sub() dgv_Notifikasi.ClearSelection())
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        KunciUkuranForm(Me, StandarLebarLayar, StandarTinggiLayar)
    End Sub

    Public BarisNotifikasi_Terseleksi
    Public NomorIDNotifikasi_Terseleksi
    Public KontenNotifikasi_Terseleksi
    Public HalamanTarget_Terseleksi
    Public PesanEksekusi_Terseleksi
    Public StatusDibacaNotifikasi_Terseleksi
    Public StatusDieksekusiNotifikasi_Terseleksi
    Private Sub dgv_Notifikasi_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Notifikasi.CellContentClick
    End Sub
    Private Sub dgv_Notifikasi_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_Notifikasi.CellClick
        BarisNotifikasi_Terseleksi = dgv_Notifikasi.CurrentRow.Index
        NomorIDNotifikasi_Terseleksi = dgv_Notifikasi.Item("Nomor_ID", BarisNotifikasi_Terseleksi).Value
        KontenNotifikasi_Terseleksi = dgv_Notifikasi.Item("Konten_", BarisNotifikasi_Terseleksi).Value
        HalamanTarget_Terseleksi = dgv_Notifikasi.Item("Halaman_Target", BarisNotifikasi_Terseleksi).Value
        PesanEksekusi_Terseleksi = dgv_Notifikasi.Item("Pesan_Eksekusi", BarisNotifikasi_Terseleksi).Value
        StatusDibacaNotifikasi_Terseleksi = dgv_Notifikasi.Item("Status_Dibaca", BarisNotifikasi_Terseleksi).Value
        StatusDieksekusiNotifikasi_Terseleksi = dgv_Notifikasi.Item("Status_Dieksekusi", BarisNotifikasi_Terseleksi).Value
        KeluarDariSemuaModul()
        Select Case HalamanTarget_Terseleksi
            Case Halaman_DATACOA
                mnu_DataCOA_Click(sender, e)
            Case Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI
                BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
                If usc_BukuPengawasanHutangUsaha.JudulForm = usc_BukuPengawasanHutangUsaha.JudulForm_BukuPengawasanHutangUsaha Then
                    usc_BukuPengawasanHutangUsaha.btn_SaldoAwalHutangUsaha_Click(sender, e)
                End If
                usc_BukuPengawasanHutangUsaha.TampilkanData()
                If usc_BukuPengawasanHutangUsaha.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI)
            Case Halaman_BUKUPENGAWASANHUTANGUSAHAAFILIASI
                BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
                If usc_BukuPengawasanHutangUsaha.JudulForm = usc_BukuPengawasanHutangUsaha.JudulForm_BukuPengawasanHutangUsaha Then
                    usc_BukuPengawasanHutangUsaha.btn_SaldoAwalHutangUsaha_Click(sender, e)
                End If
                usc_BukuPengawasanHutangUsaha.TampilkanData()
                If usc_BukuPengawasanHutangUsaha.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGUSAHAAFILIASI)
            Case Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_USD
                BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
                If usc_BukuPengawasanHutangUsaha.JudulForm = usc_BukuPengawasanHutangUsaha.JudulForm_BukuPengawasanHutangUsaha Then
                    usc_BukuPengawasanHutangUsaha.btn_SaldoAwalHutangUsaha_Click(sender, e)
                End If
                usc_BukuPengawasanHutangUsaha.TampilkanData()
                If usc_BukuPengawasanHutangUsaha.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_USD)
            Case Halaman_BUKUPENGAWASANPIUTANGUSAHANONAFILIASI
                BukaModul_BukuPengawasanPiutangUsaha_NonAfiliasi()
                If usc_BukuPengawasanPiutangUsaha.JudulForm = usc_BukuPengawasanPiutangUsaha.JudulForm_BukuPengawasanPiutangUsaha Then
                    usc_BukuPengawasanPiutangUsaha.btn_SaldoAwalPiutangUsaha_Click(sender, e)
                End If
                usc_BukuPengawasanPiutangUsaha.TampilkanData()
                If usc_BukuPengawasanPiutangUsaha.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANPIUTANGUSAHANONAFILIASI)
            Case Halaman_BUKUPENGAWASANPIUTANGUSAHAAFILIASI
                BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
                If usc_BukuPengawasanPiutangUsaha.JudulForm = usc_BukuPengawasanPiutangUsaha.JudulForm_BukuPengawasanPiutangUsaha Then
                    usc_BukuPengawasanPiutangUsaha.btn_SaldoAwalPiutangUsaha_Click(sender, e)
                End If
                usc_BukuPengawasanPiutangUsaha.TampilkanData()
                If usc_BukuPengawasanPiutangUsaha.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANPIUTANGUSAHAAFILIASI)
            Case Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_USD
                BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
                If usc_BukuPengawasanPiutangUsaha.JudulForm = usc_BukuPengawasanPiutangUsaha.JudulForm_BukuPengawasanPiutangUsaha Then
                    usc_BukuPengawasanPiutangUsaha.btn_SaldoAwalPiutangUsaha_Click(sender, e)
                End If
                usc_BukuPengawasanPiutangUsaha.TampilkanData()
                If usc_BukuPengawasanPiutangUsaha.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_USD)
            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL21
                mnu_BukuPengawasanHutangPPhPasal21_Click(sender, e)
            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL23
                mnu_BukuPengawasanHutangPPhPasal23_Click(sender, e)
                usc_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
                If usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_100 = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGPPHPASAL23)
            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL25
                mnu_BukuPengawasanHutangPPhPasal25_Click(sender, e)
            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL26
                mnu_BukuPengawasanHutangPPhPasal26_Click(sender, e)
            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL29
                mnu_BukuPengawasanHutangPPhPasal29_Click(sender, e)
            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL42
                mnu_BukuPengawasanHutangPPhPasal42_Click(sender, e)
                usc_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
                If usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_402 = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGPPHPASAL42)
            Case Halaman_BUKUPENGAWASANHUTANGKARYAWAN
                BukaModul_BukuPengawasanHutangKaryawan()
                usc_BukuPengawasanHutangKaryawan.TampilkanData()
                If usc_BukuPengawasanHutangKaryawan.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGKARYAWAN)
            Case Halaman_BUKUPENGAWASANPIUTANGKARYAWAN
                BukaModul_BukuPengawasanPiutangKaryawan()
                usc_BukuPengawasanPiutangKaryawan.TampilkanData()
                If usc_BukuPengawasanPiutangKaryawan.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANPIUTANGKARYAWAN)
            Case Halaman_BUKUPENGAWASANHUTANGPEMEGANGSAHAM
                BukaModul_BukuPengawasanHutangPemegangSaham()
                usc_BukuPengawasanHutangPemegangSaham.TampilkanData()
                If usc_BukuPengawasanHutangPemegangSaham.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGPEMEGANGSAHAM)
            Case Halaman_BUKUPENGAWASANPIUTANGPEMEGANGSAHAM
                BukaModul_BukuPengawasanPiutangPemegangSaham()
                usc_BukuPengawasanPiutangPemegangSaham.TampilkanData()
                If usc_BukuPengawasanPiutangPemegangSaham.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANPIUTANGPEMEGANGSAHAM)
            Case Halaman_BUKUPENGAWASANHUTANGBPJSKESEHATAN
                BukaModul_BukuPengawasanHutangBPJSKesehatan()
                usc_BukuPengawasanHutangBPJSKesehatan.TampilkanData()
                If usc_BukuPengawasanHutangBPJSKesehatan.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGBPJSKESEHATAN)
            Case Halaman_BUKUPENGAWASANHUTANGBPJSKETENAGAKERJAAN
                BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
                usc_BukuPengawasanHutangBPJSKetenagakerjaan.TampilkanData()
                If usc_BukuPengawasanHutangBPJSKetenagakerjaan.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGBPJSKETENAGAKERJAAN)
            Case Halaman_BUKUPENGAWASANHUTANGKOPERASIKARYAWAN
                BukaModul_BukuPengawasanHutangKoperasiKaryawan()
                usc_BukuPengawasanHutangKoperasiKaryawan.TampilkanData()
                If usc_BukuPengawasanHutangKoperasiKaryawan.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGKOPERASIKARYAWAN)
            Case Halaman_BUKUPENGAWASANHUTANGSERIKAT
                BukaModul_BukuPengawasanHutangSerikat()
                usc_BukuPengawasanHutangSerikat.TampilkanData()
                If usc_BukuPengawasanHutangSerikat.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGSERIKAT)
            Case Halaman_BUKUPENGAWASANHUTANGBANK
                BukaModul_BukuPengawasanHutangBank()
                usc_BukuPengawasanHutangBank.TampilkanData()
                If usc_BukuPengawasanHutangBank.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGBANK)
            Case Halaman_BUKUPENGAWASANHUTANGLEASING
                BukaModul_BukuPengawasanHutangLeasing()
                usc_BukuPengawasanHutangLeasing.TampilkanData()
                If usc_BukuPengawasanHutangLeasing.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGLEASING)
            Case Halaman_BUKUPENGAWASANHUTANGPIHAKKETIGA
                BukaModul_BukuPengawasanHutangPihakKetiga()
                usc_BukuPengawasanHutangPihakKetiga.TampilkanData()
                If usc_BukuPengawasanHutangPihakKetiga.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGPIHAKKETIGA)
            Case Halaman_BUKUPENGAWASANPIUTANGPIHAKKETIGA
                BukaModul_BukuPengawasanPiutangPihakKetiga()
                usc_BukuPengawasanPiutangPihakKetiga.TampilkanData()
                If usc_BukuPengawasanPiutangPihakKetiga.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANPIUTANGPIHAKKETIGA)
            Case Halaman_BUKUPENGAWASANHUTANGAFILIASI
                BukaModul_BukuPengawasanHutangAfiliasi()
                usc_BukuPengawasanHutangAfiliasi.TampilkanData()
                If usc_BukuPengawasanHutangAfiliasi.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANHUTANGAFILIASI)
            Case Halaman_BUKUPENGAWASANPIUTANGAFILIASI
                BukaModul_BukuPengawasanPiutangAfiliasi()
                usc_BukuPengawasanPiutangAfiliasi.TampilkanData()
                If usc_BukuPengawasanPiutangAfiliasi.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANPIUTANGAFILIASI)
            Case Halaman_BUKUPENGAWASANDEPOSITOPERASIONAL
                BukaModul_BukuPengawasanDepositOperasional()
                usc_BukuPengawasanDepositOperasional.TampilkanData()
                If usc_BukuPengawasanDepositOperasional.KesesuaianSaldoAwal = True Then UpdateNotifikasi_Eksekusi(Halaman_BUKUPENGAWASANDEPOSITOPERASIONAL)
            Case Kosongan
                'Tidak ada Eksekusi Apa-apa
            Case Else
                PesanUntukProgrammer("Halaman Target Belum Ditentukan..!!!!")
        End Select
        If PesanEksekusi_Terseleksi <> Nothing Then BeginInvoke(Sub() MsgBox(PesanEksekusi_Terseleksi))
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Notifikasi SET " &
                              " Status_Dibaca = 1 " &
                              " WHERE Nomor_ID = '" & NomorIDNotifikasi_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
        IsiKontenNotifikasi()

    End Sub

    Private Sub KursToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KursToolStripMenuItem.Click
        frm_Kurs.MdiParent = Me
        frm_Kurs.Show()
        frm_Kurs.Focus()
    End Sub

    Private Sub mnu_TechnicalSupport_Click(sender As Object, e As EventArgs) Handles mnu_TechnicalSupport.Click

    End Sub













    'Masalah UI :
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' WS_EX_COMPOSITED
            Return cp
        End Get
    End Property


End Class