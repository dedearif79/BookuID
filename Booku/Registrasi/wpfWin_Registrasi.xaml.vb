Imports System.Data.Odbc
Imports MySql.Data.MySqlClient
Imports bcomm
Imports System.Windows


Public Class wpfWin_Registrasi

    Dim PengulanganRegistrasi
    Public RegistrasiTambahan As Boolean
    Public ID_Customer_Reg
    Dim NamaPerusahaan_Reg
    Dim JumlahPerangkat_Reg
    Dim StatusTerpakai_Reg
    Dim TanggalInstall
    Dim ExpireSaatRegistrasi
    Dim RegistrasiOnline As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        txt_IDKomputer.Text = ID_CPU

    End Sub

    Public Sub ResetForm()
        ResetFormDataProduk()
        btn_Kirim.IsEnabled = False
        btn_CekKetersediaan.IsEnabled = False
        btn_Batal.IsEnabled = True
        btn_Batal.Content = "Batal"
        btn_CekKetersediaan.Visibility = Visibility.Visible
        txt_NomorSeriProduk.IsEnabled = True
        txt_NomorSeriProduk.Text = Nothing
        PengulanganRegistrasi = 0
    End Sub


    Public Sub ResetFormDataProduk()
        txt_IDCustomer.Text = Nothing
        txt_JumlahPerangkat.Text = Nothing
    End Sub

    Private Sub txt_NomorSeriProduk_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_NomorSeriProduk.TextChanged
        btn_Kirim.IsEnabled = False
        ResetFormDataProduk()
        If Microsoft.VisualBasic.Len(txt_NomorSeriProduk.Text) = 27 Then
            btn_CekKetersediaan.IsEnabled = True
        Else
            btn_CekKetersediaan.IsEnabled = False
        End If
    End Sub

    Private Sub btn_CekKetersediaan_Click(sender As Object, e As RoutedEventArgs) Handles btn_CekKetersediaan.Click

        BukaDatabasePublic()
        If StatusKoneksiDatabasePublic = True Then
            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_produk WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' ", KoneksiDatabasePublic)
            drPublic = cmdPublic.ExecuteReader
            drPublic.Read()
            If drPublic.HasRows Then
                ID_Customer_Reg = drPublic.Item("ID_Customer")
                JumlahPerangkat_Reg = drPublic.Item("Jumlah_Perangkat")
                StatusTerpakai_Reg = drPublic.Item("Status_Terpakai")
                If StatusTerpakai_Reg <> 1 Then
                    txt_IDCustomer.Text = ID_Customer_Reg
                    txt_JumlahPerangkat.Text = JumlahPerangkat_Reg
                    btn_CekKetersediaan.IsEnabled = False
                    RegistrasiOnline = True
                    Pesan_Sukses("Nomor Seri Produk tersedia." & Enter2Baris & "Silakan lengkapi data perusahaan.")
                    win_Registrasi_IsiDataCompany = New wpfWin_Registrasi_IsiDataCompany
                    win_Registrasi_IsiDataCompany.ShowDialog()
                    win_Registrasi_IsiDataUser = New wpfWin_Registrasi_IsiDataUser
                    win_Registrasi_IsiDataUser.ShowDialog()
                    btn_Kirim.IsEnabled = True
                    btn_Kirim.Focus()
                Else
                    Pesan_Peringatan("Nomor Seri Produk tidak tersedia.")
                    btn_Kirim.IsEnabled = False
                End If
            Else
                Pesan_Peringatan("Nomor Seri Produk tidak tersedia.")
                btn_Kirim.IsEnabled = False
            End If
        Else
            RegistrasiOffline()
        End If

        TutupDatabasePublic()

    End Sub

    Sub RegistrasiOffline()
        Dim Pesan As String = "Aplikasi tidak dapat terhubung ke server. Akan tetapi Anda masih bisa melakukan registrasi secara offline." & Enter2Baris &
            "Registrasi online dapat dilakukan menyusul setelah aplikasi dapat terhubung kembali ke server." & Enter2Baris &
            "Lanjutkan registrasi offline?"
        If Not TanyaKonfirmasi(Pesan) Then Return
        Pesan_Informasi("Fitur registrasi offline belum tersedia.")
        Return
        txt_NomorSeriProduk.Text = NomorSeriProduk_Kosongan
        ID_Customer_Reg = ID_Customer_Kosongan
        txt_IDCustomer.Text = ID_Customer_Kosongan
        txt_JumlahPerangkat.Text = 1
        btn_CekKetersediaan.IsEnabled = False
        RegistrasiOnline = False
        win_Registrasi_IsiDataCompany = New wpfWin_Registrasi_IsiDataCompany
        win_Registrasi_IsiDataCompany.ShowDialog()
        win_Registrasi_IsiDataUser = New wpfWin_Registrasi_IsiDataUser
        win_Registrasi_IsiDataUser.ShowDialog()
        btn_Kirim.IsEnabled = True
        btn_Kirim.Focus()
    End Sub

    Private Sub btn_Kirim_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kirim.Click

        PesanUntukProgrammer("Nanti harus dibikin sistem, bahwa untuk ID Customer sifatnya customize, dengan ketentuan huruf kecil semua, dan harus 8 digit...!")

        ProsesRegistrasiPerusahaan = True
        PengulanganRegistrasi += 1
        If PengulanganRegistrasi >= 3 Then
            PengulanganRegistrasi = 0
            Pesan_Peringatan("Coba atur ulang konfigurasi koneksi database." & Enter2Baris & "Jika Anda mengalami kesulitan, silakan hubungi Tim IT.")
            PengaturanKoneksi()
            If StatusKoneksiTesKoneksiDbSAT = False Then
                Return
            End If
        End If

        'Buat Database Dasar
        BuatDatabaseDasar()

        'Buat Database General : db_bookuid_booku_idcustomer_gen
        HasilPembuatanDatabaseGeneral = False
        NamaPerusahaan_Reg = win_Registrasi_IsiDataCompany.txt_NamaPerusahaan.Text
        Dim ID_Customer_Asli = ID_Customer
        ID_Customer = ID_Customer_Reg
        BuatDatabaseGeneral()
        If RegistrasiTambahan Then ID_Customer = ID_Customer_Asli


        'Isi Data Perusahaan
        If HasilPembuatanDatabaseGeneral = True Then
            Pesan_Sukses("Pembuatan kerangka database berhasil.")
            AksesDatabase_General(Buka)
            Dim QueryIsiTabel
            ExpireSaatRegistrasi = Microsoft.VisualBasic.Left(Today, 10)
            QueryIsiTabel = " INSERT INTO tbl_Company VALUES ('" &
                txt_NomorSeriProduk.Text & "', '" &
                txt_IDCustomer.Text & "', '" &
                win_Registrasi_IsiDataCompany.txt_NamaPerusahaan.Text & "', '" &
                Kosongan & "', '" & 'Tagline
                win_Registrasi_IsiDataCompany.cmb_JenisUsahaPerusahaan.SelectedValue & "', '" &
                win_Registrasi_IsiDataCompany.cmb_JenisWPPerusahaan.SelectedValue & "', '" &
                win_Registrasi_IsiDataCompany.txt_NPWP.Text & "', '" &
                win_Registrasi_IsiDataCompany.txt_NamaDirektur.Text & "', '" &
                win_Registrasi_IsiDataCompany.txt_AlamatPerusahaan.Text & "', '" &
                win_Registrasi_IsiDataCompany.txt_EmailPerusahaan.Text & "', '" &
                win_Registrasi_IsiDataCompany.txt_PIC.Text & "', '" &
                win_Registrasi_IsiDataCompany.txt_NomorSKT.Text & "', '" &
                TanggalFormatSimpan(win_Registrasi_IsiDataCompany.dtp_TanggalSKT.SelectedDate) & "', '" &
                Kosongan & "', '" &
                Kosongan & "', '" &
                Kosongan & "', '" &
                Kosongan & "', '" &
                TanggalKosongSimpan & "', '" &
                TanggalKosongSimpan & "', '" &
                1 & "', '" &
                TanggalTakTerbatasSimpan & "', '" &
                Kosongan & "', '" &
                Kosongan & "', '" &
                Kosongan & "', '" &
                0 & "', '" &
                TanggalTakTerbatasSimpan & "' ) "
            cmd = New OdbcCommand(QueryIsiTabel, KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                HasilPembuatanDatabaseGeneral = True
                Pesan_Sukses("Pengisian data profil perusahaan berhasil.")
            Catch ex As Exception
                HasilPembuatanDatabaseGeneral = False
                Pesan_Gagal("Pengisian data profil perusahaan gagal.")
            End Try
            AksesDatabase_General(Tutup)
        Else
            Pesan_Gagal("Pembuatan database perusahaan gagal.")
        End If

        'Isi Data SC (Kode Khusus)
        If HasilPembuatanDatabaseGeneral = True Then
            AksesDatabase_General(Buka)
            Dim QueryIsiTabel
            Dim IdSC = 1
            TahunBukuTerakhirDibuka = 0
            TanggalInstall = TanggalFormatTampilan(Today)
            ExpireSaatRegistrasi = TanggalInstall
            QueryIsiTabel = " INSERT INTO tbl_SC VALUES ('" &
                IdSC & "', '" &
                TahunBukuTerakhirDibuka & "', '" &
                EnkripsiAngka(txt_JumlahPerangkat.Text) & "', '" &
                EnkripsiTeks(win_Registrasi_IsiDataCompany.cmb_SistemApproval.SelectedValue) & "', '" &
                EnkripsiAngka(win_Registrasi_IsiDataCompany.cmb_TahunCutOff.SelectedValue) & "', '" &
                EnkripsiTeks(win_Registrasi_IsiDataCompany.cmb_SistemCOA.SelectedValue) & "', '" &
                EnkripsiTanggal(TanggalInstall) & "', '" &
                EnkripsiTanggal(ExpireSaatRegistrasi) & "' ) "
            cmd = New OdbcCommand(QueryIsiTabel, KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                HasilPembuatanDatabaseGeneral = True
                Pesan_Sukses("Pengisian data pendukung berhasil.")
            Catch ex As Exception
                HasilPembuatanDatabaseGeneral = False
                Pesan_Gagal("Pengisian data pendukung gagal.")
            End Try
            AksesDatabase_General(Tutup)
        Else
            Pesan_Gagal("Pembuatan database perusahaan gagal.")
        End If

        'Isi Data User Perdana (Super User)
        If HasilPembuatanDatabaseGeneral = True Then
            AksesDatabase_General(Buka)
            Dim QueryIsiTabel
            QueryIsiTabel = " INSERT INTO tbl_User VALUES ('" &
                win_Registrasi_IsiDataUser.txt_Username.Text & "', '" &
                EnkripsiTeks(win_Registrasi_IsiDataUser.txt_Password.Password) & "', '9', '" &
                win_Registrasi_IsiDataUser.txt_NamaUser.Text & "', '" & JabatanUser_SuperUser & "', '1' , '1', '1' ) "
            cmd = New OdbcCommand(QueryIsiTabel, KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                HasilPembuatanDatabaseGeneral = True
                Pesan_Sukses("Pengisian data user perdana berhasil.")
            Catch ex As Exception
                HasilPembuatanDatabaseGeneral = False
                Pesan_Gagal("Pengisian data user perdana gagal.")
            End Try
            AksesDatabase_General(Tutup)
        End If

        'Isi Data Tabel COA
        If HasilPembuatanDatabaseGeneral = True Then IsiTabelCOA()

        'Isi Data Tabel Tautan COA
        If HasilPembuatanDatabaseGeneral = True Then
            IsiTabelTautanCOA(win_Registrasi_IsiDataCompany.cmb_SistemCOA.SelectedValue)
        End If


        'Isi Data InfoAplikasi
        If HasilPembuatanDatabaseGeneral = True Then
            If RegistrasiOnline Then
                BukaDatabasePublic()
                cmdPublic = New MySqlCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabasePublic)
                drPublic = cmdPublic.ExecuteReader
                drPublic.Read()
                VersiBooku_SisiPublic = drPublic.Item("Versi_App")
                ApdetBooku_SisiPublic = drPublic.Item("Apdet_App")
                TutupDatabasePublic()
            Else
                VersiBooku_SisiPublic = 0
                ApdetBooku_SisiPublic = 0
            End If
            AksesDatabase_General(Buka)
            Try
                cmd = New OdbcCommand(" INSERT INTO tbl_infoaplikasi VALUES ('" &
                                      NamaAplikasi & "', '" &
                                      EnkripsiAngkaAES1(VersiBooku_SisiPublic) & "', '" &
                                      EnkripsiAngkaAES1(ApdetBooku_SisiPublic) & "', '" &
                                      NomorHotLine & "', '" &
                                      WebsiteAplikasi & "', '" &
                                      EmailAplikasi & "') ",
                                      KoneksiDatabaseGeneral)
                cmd.ExecuteNonQuery()
                VersiBooku_SisiDatabase = VersiBooku_SisiPublic
                ApdetBooku_SisiDatabase = ApdetBooku_SisiPublic
                HasilPembuatanDatabaseGeneral = True
                Pesan_Sukses("Pengisian data info aplikasi berhasil.")
            Catch ex As Exception
                HasilPembuatanDatabaseGeneral = False
                Pesan_Gagal("Pengisian data info aplikasi gagal.")
            End Try
            AksesDatabase_General(Tutup)
        End If

        'Tes Kesuksesan Pembuatan Database General :
        If HasilPembuatanDatabaseGeneral = True Then
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabaseGeneral = True Then
                ProsesRegistrasiPerusahaan = True
                AksesDatabase_General(Tutup)
            Else
                ProsesRegistrasiPerusahaan = False
            End If
            AksesDatabase_General(Tutup)
        Else
            ProsesRegistrasiPerusahaan = False
        End If

        'Daftarkan Data Customer, termasuk ID Customer dan Nomor Seri Produk
        If ProsesRegistrasiPerusahaan = True Then
            If RegistrasiOnline Then
                BukaDatabasePublic()
                cmdPublic = New MySqlCommand(" INSERT INTO tbl_customer VALUES ('" &
                                             txt_NomorSeriProduk.Text & "', '" &
                                             txt_IDCustomer.Text & "', '" &
                                             VersiBooku_SisiPublic & "', '" &
                                             ApdetBooku_SisiPublic & "', '" &
                                             win_Registrasi_IsiDataCompany.txt_NamaPerusahaan.Text & "', '" &
                                             win_Registrasi_IsiDataCompany.txt_NamaDirektur.Text & "', '" &
                                             win_Registrasi_IsiDataCompany.txt_NPWP.Text & "', '" &
                                             win_Registrasi_IsiDataCompany.cmb_JenisUsahaPerusahaan.SelectedValue & "', '" &
                                             win_Registrasi_IsiDataCompany.txt_AlamatPerusahaan.Text & "', '" &
                                             win_Registrasi_IsiDataCompany.txt_EmailPerusahaan.Text & "', '" &
                                             win_Registrasi_IsiDataCompany.txt_PIC.Text & "', '" &
                                             txt_JumlahPerangkat.Text & "', '" &
                                             win_Registrasi_IsiDataCompany.cmb_SistemApproval.SelectedValue & "', '" &
                                             win_Registrasi_IsiDataCompany.cmb_TahunCutOff.SelectedValue & "', '" &
                                             win_Registrasi_IsiDataCompany.cmb_SistemCOA.SelectedValue & "', '" &
                                             TanggalFormatSimpan(ExpireSaatRegistrasi) & "') ",
                                             KoneksiDatabasePublic)
                Try
                    cmdPublic.ExecuteNonQuery()
                    ProsesRegistrasiPerusahaan = True
                    Pesan_Sukses("Pengisian data client berhasil.")
                Catch ex As Exception
                    ProsesRegistrasiPerusahaan = False
                    Pesan_Gagal("Pengisian data client gagal.")
                End Try
                TutupDatabasePublic()
            Else
            End If
        End If

        'Daftarkan Perangkat Komputer
        Dim Nomor_ID = 0
        If ProsesRegistrasiPerusahaan = True Then
            If RegistrasiOnline Then
                BukaDatabasePublic()
                cmdPublic = New MySqlCommand("SELECT * FROM tbl_perangkat " &
                                         " WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_perangkat)", KoneksiDatabasePublic)
                Try
                    drPublic = cmdPublic.ExecuteReader
                    drPublic.Read()
                    If Not drPublic.HasRows Then
                        Nomor_ID = 1
                    Else
                        Nomor_ID = drPublic.Item("Nomor_ID") + 1
                    End If
                    ProsesRegistrasiPerusahaan = True
                Catch ex As Exception
                    ProsesRegistrasiPerusahaan = False
                End Try
                TutupDatabasePublic()
            Else
            End If
        End If
        If ProsesRegistrasiPerusahaan = True Then
            If RegistrasiOnline Then
                BukaDatabasePublic()
                cmdPublic = New MySqlCommand(" INSERT INTO tbl_perangkat VALUES ('" &
                                        Nomor_ID & "', '" &
                                        txt_IDKomputer.Text & "', '" &
                                        txt_NomorSeriProduk.Text & "') ", KoneksiDatabasePublic)
                Try
                    cmdPublic.ExecuteNonQuery()
                    ProsesRegistrasiPerusahaan = True
                    Pesan_Sukses("ID komputer berhasil didaftarkan.")
                Catch ex As Exception
                    ProsesRegistrasiPerusahaan = False
                    Pesan_Gagal("ID komputer gagal didaftarkan.")
                End Try
                TutupDatabasePublic()
            Else
            End If
        End If

        'Daftarkan Perangkat ke tbl_perangkat di Database General di Serverl Lokal :
        If ProsesRegistrasiPerusahaan = True Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_perangkat VALUES ( '" & txt_IDKomputer.Text & "', '" & EnkripsiTeks(txt_IDKomputer.Text) & "') ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesRegistrasiPerusahaan = True
            Catch ex As Exception
                ProsesRegistrasiPerusahaan = False
            End Try
            TutupDatabasePublic()
        End If

        'Beri Keterangan Bahwa Komputer Terdaftar
        If ProsesRegistrasiPerusahaan = True Then
            BeriKeteranganKomputerTerdaftar()
        End If

        'Update Data Ketersediaan Nomor Seri Produk menjadiTerpakai (1)
        If ProsesRegistrasiPerusahaan = True Then
            If RegistrasiOnline Then
                BukaDatabasePublic()
                cmdPublic = New MySqlCommand(" UPDATE tbl_produk SET Status_Terpakai = 1 " &
                                         " WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' ", KoneksiDatabasePublic)
                Try
                    cmdPublic.ExecuteNonQuery()
                    ProsesRegistrasiPerusahaan = True
                Catch ex As Exception
                    ProsesRegistrasiPerusahaan = False
                End Try
                TutupDatabasePublic()
            Else
            End If
        End If

        'Daftarkan ID_Customer_Reg di Database Dasar
        If ProsesRegistrasiPerusahaan = True Then
            BukaDatabaseDasar()
            cmdDasar = New MySqlCommand(" INSERT INTO tbl_ListCompany VALUES ( '" & ID_Customer_Reg & "', '" & NamaPerusahaan_Reg & "' )", KoneksiDatabaseDasar)
            Try
                cmdDasar.ExecuteNonQuery()
                ProsesRegistrasiPerusahaan = True
            Catch ex As Exception
                ProsesRegistrasiPerusahaan = False
            End Try
            TutupDatabaseDasar()
        End If

        'Akhir Proses Registrasi:
        If ProsesRegistrasiPerusahaan = True Then

            If RegistrasiTambahan = True Then
                MsgBox("Selamat...! Registrasi berhasil." &
                       Enter2Baris & "Anda masih berada di Zona ." & NamaPerusahaan & "." &
                       Enter2Baris & "Untuk masuk ke Zona ." & NamaPerusahaan_Reg & ", Anda harus keluar aplikasi terlebih dahulu kemudian lakukan LOGIN seperti biasa.")
                Me.Close()
            Else
                MsgBox("Selamat...! Registrasi berhasil.")
                btn_Kirim.IsEnabled = False
                btn_Batal.Content = "Tutup"
                Me.Close()
            End If

        Else

            'Hapus kembali Data Customer di server Public
            BukaDatabasePublic()
            cmdPublic = New MySqlCommand(" DELETE FROM tbl_customer WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' ", KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            TutupDatabasePublic()

            'Hapus kembali Data Perangkat di server Public
            BukaDatabasePublic()
            cmdPublic = New MySqlCommand(" DELETE FROM tbl_perangkat WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' ", KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            TutupDatabasePublic()

            'Hapus kembali database General
            Dim KoneksiHapusDb = New MySqlConnection("Data Source =" & LokasiServerDatabase & ";username=" & UserDatabase & ";password=" & PasswordDatabase & ";SSL Mode=None")
            Dim cmdHapusDb As MySqlCommand
            Dim strHapusDb As String
            cmdHapusDb = KoneksiHapusDb.CreateCommand
            strHapusDb = " DROP DATABASE " & NamaDatabaseGeneral
            cmdHapusDb.CommandText = strHapusDb
            Try
                KoneksiHapusDb.Open()
                cmdHapusDb.ExecuteNonQuery()
                KoneksiHapusDb.Close()
            Catch ex As Exception
            End Try

            'Update Data Ketersediaan Nomor Seri Produk menjadi Tidak Terpakai (0)
            BukaDatabasePublic()
            cmdPublic = New MySqlCommand(" UPDATE tbl_produk SET Status_Terpakai = 0 WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' ", KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            TutupDatabasePublic()

            'Hapus kembali ID_Customer_Reg di Database Dasar
            BukaDatabaseDasar()
            cmdDasar = New MySqlCommand(" DELETE FROM tbl_ListCompany WHERE ID_Customer = '" & ID_Customer_Reg & "' ", KoneksiDatabaseDasar)
            Try
                cmdDasar.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            TutupDatabaseDasar()

            'Batalkan Keterangan, bahwa Komputer Terdaftar (File : 0002.conf)
            If RegistrasiTambahan = False Then
                BeriKeteranganKomputerTidakTerdaftar()
            End If

            'Pesan Gagal
            Pesan_Gagal("Registrasi gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
            btn_CekKetersediaan.IsEnabled = True
            btn_Batal.Content = "Batal"

        End If

        'Pulihkan Data Dsn General
        BuatDsnGeneral()

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        SizeToContent = SizeToContent.Height
    End Sub

End Class
