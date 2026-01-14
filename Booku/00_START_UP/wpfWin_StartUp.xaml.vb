Imports System.Data.Odbc
Imports MySql.Data.MySqlClient
Imports System.Windows
Imports System.Windows.Threading
Imports System.Windows.Input
Imports bcomm
Imports System.IO

Public Class wpfWin_StartUp

    Dim JumlahCompany

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Terabas()

        Dispatcher.BeginInvoke(Sub() cmb_ListCompany.Focus())

        NamaAplikasi = "Booku - Sistem Akuntansi Terpadu"
        lbl_Baris_01.Text = NamaAplikasi
        lbl_Baris_02.Visibility = Visibility.Hidden
        lbl_ProgressReport.Text = Kosongan
        lbl_Company.Visibility = Visibility.Hidden
        cmb_ListCompany.Visibility = Visibility.Hidden
        btn_Lanjutkan.Visibility = Visibility.Hidden
        lbl_Identitas.Text = Kosongan
        lbl_Identitas.Visibility = Visibility.Collapsed

        EksekusiKode = True

        'Tes Konektivitas Database
        TesKoneksiDatabase()

        'Ambil value variabel ID CPU :
        ID_CPU = AmbilValue_ID_CPU()

        'List Company :
        KontenComboCompyany()

        'Loading....
        Terabas()
        Dispatcher.BeginInvoke(Sub() ProgressLoading())

        'Complete :
        If JumlahCompany > 0 Then
            If JumlahCompany = 1 Then
                lbl_ProgressReport.Text = "Klik 'Lanjutkan'."
            Else
                lbl_ProgressReport.Text = "Silakan pilih Company dan klik 'Lanjutkan'."
            End If
        Else
            Pesan_Peringatan("Anda belum melakukan Registrasi untuk pemakaian aplikasi ini." & Enter2Baris & "Silakan lakukan Registrasi terlebih dahulu.")
            win_Registrasi = New wpfWin_Registrasi
            win_Registrasi.ResetForm()
            win_Registrasi.RegistrasiTambahan = False
            win_Registrasi.ShowDialog()
            If ProsesRegistrasiPerusahaan = False Then
                End
            End If
            Me.Close()
        End If

    End Sub


    Dim Maximum = 333
    Sub ProgressLoading()
        StartProgress(pgb_Progress, 0, Maximum)
        Do While ProgressValue < 111
            ProgressUp(pgb_Progress)
            Terabas()
            Jeda(1)
        Loop
        If JumlahCompany > 1 Then
            lbl_Company.Visibility = Visibility.Visible
            cmb_ListCompany.Visibility = Visibility.Visible
        End If
        btn_Lanjutkan.Visibility = Visibility.Visible
    End Sub


    Sub KontenComboCompyany()
        Dim ListCompany = Kosongan
        cmb_ListCompany.Items.Clear()
        BukaDatabaseDasar()
        cmdDasar = New MySqlCommand(" SELECT * FROM tbl_ListCompany ", KoneksiDatabaseDasar)
        Try 'Kenapa harus pakai Try..? Karena sangat mungkin database dasar belum dibuat.
            drDasar = cmdDasar.ExecuteReader
            JumlahCompany = 0
            Do While drDasar.Read
                ListCompany = drDasar.Item("Nama_Perusahaan")
                cmb_ListCompany.Items.Add(ListCompany)
                JumlahCompany += 1
            Loop
        Catch ex As Exception
        End Try
        TutupDatabaseDasar()
        cmb_ListCompany.SelectedValue = ListCompany
    End Sub


    Sub TesKoneksiDatabase()

        Try
            DataKoneksi = IO.File.ReadLines(FilePathDataKoneksi)
            FileEksis = True
        Catch ex As Exception
            IO.File.WriteAllText(FilePathDataKoneksi, Kosongan)
            FileEksis = False
        End Try
        If FileEksis = True Then
            Dim i = 0
            For Each KontenPerBaris In DataKoneksi
                i += 1
                If i = 4 Then LokasiFolderXAMPP = DekripsiTeks(KontenPerBaris)
                If i = 5 Then
                    LokasiServerDatabase = DekripsiTeks(KontenPerBaris)
                    LokasiServerDatabaseTesKoneksiDbSAT = DekripsiTeks(KontenPerBaris)
                End If
                If i = 6 Then
                    PortDatabase = DekripsiTeks(KontenPerBaris)
                    PortServerTesKoneksiDbSAT = DekripsiTeks(KontenPerBaris)
                End If
                If i = 7 Then
                    UserDatabase = DekripsiTeks(KontenPerBaris)
                    UserDatabaseTesKoneksiMySQL = DekripsiTeks(KontenPerBaris)
                    UserDatabaseTesKoneksiDbSAT = DekripsiTeks(KontenPerBaris)
                End If
                If i = 8 Then
                    PasswordDatabase = DekripsiTeks(KontenPerBaris)
                    PasswordDatabaseTesKoneksiMySQL = DekripsiTeks(KontenPerBaris)
                    PasswordDatabaseTesKoneksiDbSAT = DekripsiTeks(KontenPerBaris)
                End If
            Next
        End If
        PerbaruiVariabelTerkaitServer()
        If Right(LokasiFolderXAMPP, 1) <> "\" Then
            LokasiFolderXAMPP &= "\"
        End If
        BuatDsnGeneral() 'Kenapa mesti selalu memanggil sub ini..? Untuk antisipasi kemungkinan perubahan lokasi server database..!

        'Tes Koneksi MySQL
        BukaTesKoneksiMySQL()
        If StatusKoneksiTesKoneksiMySQL = False Then
            Start_dbENGINE()
            BukaTesKoneksiMySQL()
            If StatusKoneksiTesKoneksiMySQL = False Then 'Trial untuk yang kedua kalinya.
                System.Threading.Thread.Sleep(3333)
                BukaTesKoneksiMySQL()
            End If
            If StatusKoneksiTesKoneksiMySQL = False Then
                Pesan_Peringatan("Ada masalah dengan koneksi Database." _
                       & Enter2Baris & "Silakan perbaiki koneksi, atau hubungi pihak server.")
                PengaturanKoneksi()
                If StatusKoneksiTesKoneksiMySQL = False Then
                    End '(Akhiri aplikasi)
                End If
            End If
        End If
        If StatusKoneksiTesKoneksiMySQL = True Then
            TutupTesKoneksiMySQL()
        End If

        'Tes Koneksi DbSAT (untuk koneksi Database General dan Database Transaksi) :
        TesKoneksiDbSAT()
        If StatusKoneksiTesKoneksiDbSAT = False Then
            Pesan_Peringatan("Ada masalah dengan koneksi Database." _
                   & Enter2Baris & "Silakan perbaiki koneksi, atau hubungi Tim IT untuk menangani masalah ini.")
            PengaturanKoneksi()
            If StatusKoneksiTesKoneksiDbSAT = False Then
                End '(Akhiri aplikasi)
            End If
        End If

    End Sub

    Sub MasaPakai()

        If StatusKoneksiDatabasePublic = True Then
            'Perbarui data masa pakai, berdasarkan data asli, yaitu dari server public ke lokal
            BukaDatabasePublic()
            If StatusKoneksiDatabasePublic = True Then
                cmdPublic = New MySqlCommand(" SELECT * FROM tbl_customer WHERE Nomor_Seri_Produk = '" & NomorSeriProduk & "' ", KoneksiDatabasePublic)
                Try
                    drPublic = cmdPublic.ExecuteReader
                    drPublic.Read()
                    If drPublic.HasRows Then
                        AppExpire = TanggalFormatWPF(drPublic.Item("Expire"))
                        'Lakukan Update Value AppExpire yang ada di lokal, dengan AppExpire yang ada di server :
                        AksesDatabase_General(Buka)
                        If StatusKoneksiDatabaseGeneral = True Then
                            cmd = New OdbcCommand(" UPDATE tbl_SC SET SC_99 = '" & EnkripsiTanggal(AppExpire) & "' ", KoneksiDatabaseGeneral)
                            Try
                                cmd.ExecuteNonQuery()
                            Catch ex As Exception
                            End Try
                        End If
                        AksesDatabase_General(Tutup)
                    End If
                Catch ex As Exception
                End Try
            End If
            TutupDatabasePublic()
        End If

        'Cek Masa Pakai Aplikasi di Sisi Server :
        BuatDsnGeneral()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SC ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            Try
                AppInstall = DekripsiTanggal(dr.Item("SC_90"))
                AppExpire = DekripsiTanggal(dr.Item("SC_99"))
            Catch ex As Exception
                ValidasiDekripsiTanggal = False
            End Try
        End If
        AksesDatabase_General(Tutup)
        If ValidasiDekripsiTanggal = False Then
            Pesan_Gagal(teks_SistemTerkunci_HubungiDeveloper)
            End
        End If
        If AppExpire < Today Then
            Pesan_Peringatan("Masa pakai aplikasi sudah kedaluwarsa." & Enter2Baris & "Silakan isi voucher atau hubungi Developer untuk memperpanjang masa pemakaian.")
            End
        End If
        If NomorSeriProduk = NomorSeriProduk_Kosongan Then
            Dim Pesan As String = "Aplikasi belum teregistrasi secara online." & Enter2Baris &
            "Lanjutkan registrasi online?"
            If TanyaKonfirmasi(Pesan) Then RegistrasiSusulan()
            If AppInstall = Today.AddDays(7) Then
                Pesan = "Batas waktu untuk registrasi online sudah habis." & Enter2Baris &
                    "Lanjutkan registrasi online untuk mengaktifkan aplikasi?"
                If TanyaKonfirmasi(Pesan) Then
                    RegistrasiSusulan()
                    End
                End If
            End If
        End If

    End Sub

    Sub RegistrasiSusulan()
        Pesan_Informasi("Sedang melakukan registrasi online.")
    End Sub

    Sub ProgressKoneksiDatabasePublic()

        AmbilValue_InfoAplikasi()

        'Complete :
        'Identitas Pemakai Aplikasi :
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabaseGeneral = True Then
            cmd = New OdbcCommand(" SELECT * FROM tbl_Company ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then NomorSeriProduk = dr.Item("Nomor_Seri_Produk")
            lbl_Identitas.Text = "ID CPU : " & ID_CPU & Enter1Baris & "Nomor Seri : " & NomorSeriProduk
        End If
        AksesDatabase_General(Tutup)

        If StatusKoneksiDatabasePublic = True Then
            lbl_Baris_01.Text = NamaAplikasi
            lbl_Baris_02.Visibility = Visibility.Visible
            lbl_Baris_02.Text = "Web : " & WebsiteAplikasi
            'Application.DoEvents()
            lbl_Identitas.Text = lbl_Identitas.Text
        End If

    End Sub
    Sub AmbilValue_InfoAplikasi()
        'Info Aplikasi :
        BukaDatabasePublic()
        'PesanPeringatan("Koneksi Public : " & StatusKoneksiDatabasePublic)
        If StatusKoneksiDatabasePublic = True Then
            Try
                cmdPublic = New MySqlCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabasePublic)
                drPublic = cmdPublic.ExecuteReader
                drPublic.Read()
                NamaAplikasi = drPublic.Item("Nama_Aplikasi")
                VersiBooku_SisiPublic = drPublic.Item("Versi_App")
                ApdetBooku_SisiPublic = drPublic.Item("Apdet_App")
                NomorHotLine = drPublic.Item("Nomor_Hotline")
                EmailAplikasi = drPublic.Item("Email")
                'Value dari variabel WebsiteAplikasi jangan ngambil dari sini. Sudah langsung ditanam sejak awal saat deklarasi variabel.
                AdaInfoUdate = True
            Catch ex As Exception
                AdaInfoUdate = False
            End Try
        End If
        TutupDatabasePublic()
    End Sub

    Sub Lanjutkan()

        lbl_ProgressReport.Text = "Harap tunggu..."
        lbl_Identitas.Visibility = Visibility.Visible
        Terabas()
        ProgressKoneksiDatabasePublic()

        Do While ProgressValue < Maximum
            ProgressUp(pgb_Progress)
            Terabas()
            Jeda(1)
        Loop

        Jeda(111)

        AmbilValueIDCustomer()

        'Cek Masa Pakai dan Keabsahan Pemakaian Applikasi :
        MasaPakai()

        Jeda(999)

        Me.Close()

    End Sub


    Private Sub cmb_ListCompany_SelectionChanged(sender As Object, e As Controls.SelectionChangedEventArgs) Handles cmb_ListCompany.SelectionChanged
        AmbilValueIDCustomer()
    End Sub
    Sub AmbilValueIDCustomer()
        BukaDatabaseDasar()
        cmdDasar = New MySqlCommand(" SELECT * FROM tbl_ListCompany WHERE Nama_Perusahaan = '" & cmb_ListCompany.SelectedValue & "' ", KoneksiDatabaseDasar)
        drDasar = cmdDasar.ExecuteReader
        drDasar.Read()
        ID_Customer = drDasar.Item("ID_Customer")
        TutupDatabaseDasar()
        dsn_DataGeneral = Awalan_dsn & ID_Customer & "_gen"
        FolderListClient = Path.Combine(FolderRootApp, "Client")
        FolderCompany = Path.Combine(FolderListClient, ID_Customer)
        FolderCompany_Image = Path.Combine(FolderCompany, "Img")
        FilePathLogoPerusahaan = Path.Combine(FolderCompany_Image, NamaFileLogoPerusahaan)
        FolderCompany_Backup = Path.Combine(FolderCompany, "Backup")
        FolderCompany_Backup_MySQL = Path.Combine(FolderCompany_Backup, "MySQL")
    End Sub


    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click

        lbl_Company.Visibility = Visibility.Hidden
        cmb_ListCompany.Visibility = Visibility.Hidden
        btn_Lanjutkan.Visibility = Visibility.Hidden

        lbl_Company.IsEnabled = False
        cmb_ListCompany.IsEnabled = False
        btn_Lanjutkan.IsEnabled = False

        Lanjutkan()

    End Sub


    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        WindowStyle = WindowStyle.None
        cmb_ListCompany.IsReadOnly = True
    End Sub

    Private Sub wpfWin_StartUp_LostMouseCapture(sender As Object, e As MouseEventArgs) Handles Me.LostMouseCapture

    End Sub
End Class
