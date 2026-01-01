Imports bcomm
Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

Public Class frm_RegistrasiPerangkat

    Private Sub frm_RegistrasiPerangkat_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Public Sub ResetForm()
        ProsesRegistrasiPerangkat = False 'Dibikin false dulu. Jika registrasi gagal, maka ini defaultnya.
        txt_NomorSeriProduk.Text = Nothing
        txt_IDCustomer.Text = Nothing
        txt_JumlahPerangkat.Text = Nothing
        txt_IDKomputer.Text = Nothing
        txt_NomorSeriProduk.Enabled = False
        btn_Kirim.Enabled = True
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ProsesRegistrasiPerangkat = False
        Me.Close()
    End Sub

    Private Sub btn_Kirim_Click(sender As Object, e As EventArgs) Handles btn_Kirim.Click
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
                MsgBox("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
                ProsesRegistrasiPerangkat = False
            End Try
            If ProsesRegistrasiPerangkat = True Then
                If JumlahPerangkatTerdaftar >= txt_JumlahPerangkat.Text Then
                    ProsesRegistrasiPerangkat = False
                    MsgBox("Registrasi Perangkat Gagal." &
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
                        MsgBox("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
                    End Try
                    TutupDatabasePublic()
                    If ProsesRegistrasiPerangkat = True Then
                        BukaDatabasePublic()
                        cmdPublic = New MySqlCommand(" INSERT INTO tbl_perangkat VALUES ('" & _
                                                    Nomor_ID & "', '" & _
                                                    txt_IDKomputer.Text & "', '" & _
                                                    txt_NomorSeriProduk.Text & "') ", KoneksiDatabasePublic)
                        Try
                            cmdPublic.ExecuteNonQuery()
                            ProsesRegistrasiPerangkat = True
                        Catch ex As Exception
                            ProsesRegistrasiPerangkat = False
                            MsgBox("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
                        End Try
                        TutupDatabasePublic()
                    End If
                End If
            End If
        Else
            ProsesRegistrasiPerangkat = False
            MsgBox("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
        End If
        TutupDatabasePublic()

        'Daftarkan Perangkat ke tbl_perangkat di Database General di Serverl Lokal :
        If ProsesRegistrasiPerangkat = True Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_perangkat VALUES ( '" & txt_IDKomputer.Text & "', '" & EnkripsiTeks(txt_IDKomputer.Text) & "') ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesRegistrasiPerangkat = True
            Catch ex As Exception
                ProsesRegistrasiPerangkat = False
                MsgBox("Registrasi Perangkat Gagal." & Enter2Baris & teks_SilakanCobaLagi_Internet)
            End Try
            TutupDatabasePublic()
        End If
        If ProsesRegistrasiPerangkat = True Then BeriKeteranganKomputerTerdaftar() '  || Sudah benar begini formatnya
        If ProsesRegistrasiPerangkat = False Then                              '      || Jangan pakai Else-Else-an. Nanti beda hasilnya.
            'Hapus kembali Data Perangkat di server Public
            BukaDatabasePublic()
            cmdPublic = New MySqlCommand(" DELETE FROM tbl_perangkat " & _
                                         " WHERE Nomor_Seri_Produk = '" & txt_NomorSeriProduk.Text & "' " & _
                                         " AND ID_Komputer = '" & ID_CPU & "' " _
                                         , KoneksiDatabasePublic)
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