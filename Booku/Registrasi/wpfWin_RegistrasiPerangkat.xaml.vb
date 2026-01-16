Imports System.Windows
Imports bcomm
Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

Public Class wpfWin_RegistrasiPerangkat

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub


    Public Sub ResetForm()
        ProsesRegistrasiPerangkat = False
        txt_NomorSeriProduk.Text = Kosongan
        txt_IDCustomer.Text = Kosongan
        txt_JumlahPerangkat.Text = Kosongan
        txt_IDKomputer.Text = Kosongan
        txt_NomorSeriProduk.IsEnabled = False
        btn_Kirim.IsEnabled = True
    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        ProsesRegistrasiPerangkat = False
        Me.Close()
    End Sub


    Private Sub btn_Kirim_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kirim.Click
        Dim JumlahPerangkatTerdaftar = 0
        BukaDatabasePublic()
        If StatusKoneksiDatabasePublic = True Then
            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_perangkat WHERE Nomor_Seri_Produk = '" & NomorSeriProduk & "'", KoneksiDatabasePublic)
            Try
                drPublic = cmdPublic.ExecuteReader
                Do While drPublic.Read
                    JumlahPerangkatTerdaftar = JumlahPerangkatTerdaftar + 1
                Loop
                ProsesRegistrasiPerangkat = True
            Catch ex As Exception
                Pesan_Gagal("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
                ProsesRegistrasiPerangkat = False
            End Try
            If ProsesRegistrasiPerangkat = True Then
                If JumlahPerangkatTerdaftar >= AmbilAngka(txt_JumlahPerangkat.Text) Then
                    ProsesRegistrasiPerangkat = False
                    Pesan_Gagal("Registrasi Perangkat Gagal." &
                           Enter2Baris & "Jumlah perangkat pada Nomor Seri Produk '" & NomorSeriProduk & "' sudah mencapai batas." &
                           Enter2Baris & "Silakan hubungi Developer jika ingin menambah jumlah perangkat.")
                Else
                    'Daftarkan Perangkat Komputer ke Database Public :
                    Dim Nomor_ID = 0
                    BukaDatabasePublic()
                    cmdPublic = New MySqlCommand("SELECT * FROM tbl_perangkat WHERE Nomor_ID IN (SELECT MAX(Nomor_ID) FROM tbl_perangkat)", KoneksiDatabasePublic)
                    Try
                        drPublic = cmdPublic.ExecuteReader
                        drPublic.Read()
                        If Not drPublic.HasRows Then
                            Nomor_ID = 1
                        Else
                            Nomor_ID = drPublic.Item("Nomor_ID") + 1
                        End If
                        ProsesRegistrasiPerangkat = True
                    Catch ex As Exception
                        ProsesRegistrasiPerangkat = False
                        Pesan_Gagal("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
                    End Try
                    TutupDatabasePublic()
                    If ProsesRegistrasiPerangkat = True Then
                        BukaDatabasePublic()
                        cmdPublic = New MySqlCommand(" INSERT INTO tbl_perangkat VALUES ('" &
                                                    Nomor_ID & "', '" &
                                                    txt_IDKomputer.Text & "', '" &
                                                    txt_NomorSeriProduk.Text & "') ", KoneksiDatabasePublic)
                        Try
                            cmdPublic.ExecuteNonQuery()
                            ProsesRegistrasiPerangkat = True
                        Catch ex As Exception
                            ProsesRegistrasiPerangkat = False
                            Pesan_Gagal("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
                        End Try
                        TutupDatabasePublic()
                    End If
                End If
            End If
        Else
            ProsesRegistrasiPerangkat = False
            Pesan_Gagal("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
        End If
        TutupDatabasePublic()

        'Daftarkan Perangkat ke tbl_perangkat di Database General di Server Lokal :
        If ProsesRegistrasiPerangkat = True Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_perangkat VALUES ( '" & txt_IDKomputer.Text & "', '" & EnkripsiTeks(txt_IDKomputer.Text) & "') ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesRegistrasiPerangkat = True
            Catch ex As Exception
                ProsesRegistrasiPerangkat = False
                Pesan_Gagal("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
            End Try
            TutupDatabasePublic()
        End If

        If ProsesRegistrasiPerangkat = True Then BeriKeteranganKomputerTerdaftar()
        If ProsesRegistrasiPerangkat = False Then
            'Hapus kembali Data Perangkat di server Public
            BukaDatabasePublic()
            cmdPublic = New MySqlCommand(" DELETE FROM tbl_perangkat " &
                                         " WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' " &
                                         " AND ID_Komputer = '" & ID_CPU & "' ",
                                         KoneksiDatabasePublic)
            Try
                cmdPublic.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            TutupDatabasePublic()
            'Bikin Keterangan Komputer Tidak Terdaftar
            BeriKeteranganKomputerTidakTerdaftar()
        End If
        Me.Close()
    End Sub

End Class
