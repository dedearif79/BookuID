Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Public Class wpfWin_InputCOA

    Public FungsiForm
    Public JalurMasuk
    Public JumlahDigitCOA = 5
    Public ProsesSuntingDatabase As Boolean

    Dim COA
    Dim NamaAkun
    Dim DK
    Dim KodeMataUang
    Dim Kurs As Decimal
    Dim SaldoAwal As Decimal
    Dim SaldoAwal_IDR As Int64
    Dim Uraian
    Dim Visibilitas

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            Title = "Input Data COA"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            Title = "Edit Data COA"
            txt_COA.IsEnabled = False
            btn_Batal.Content = teks_Batal
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then txt_SaldoAwal.IsEnabled = False
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        'Kelompok coding ini nanti harus dihapus.
        If JalurMasuk = Halaman_TUTUPBUKU _
            Or JalurMasuk = Halaman_SALDOAWALHUTANGUSAHA _
            Or JalurMasuk = Halaman_BUKUPENGAWASANHUTANGPPHPASAL25 _
            Then
            txt_COA.IsEnabled = False
            txt_NamaAkun.IsEnabled = False
            txt_SaldoAwal.IsEnabled = True
            txt_Uraian.IsEnabled = False
            cmb_Visibilitas.IsEnabled = False
        End If

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        txt_COA.IsEnabled = True
        txt_NamaAkun.IsEnabled = True
        cmb_DebetKredit.IsEnabled = False
        txt_SaldoAwal.IsEnabled = False 'Sejatinya Saldo Awal itu tidak bisa diedit. Dia adalah konsekuensi dari Saldo Akhir Tahun sebelumnya. Saldo Awal hanya bisa diedit pada Jenis Tahun Buku Backup.
        txt_Uraian.IsEnabled = True
        cmb_Visibilitas.IsEnabled = True

        KontenComboDebetKredit()
        KontenCombo_KodeMataUang_Public(cmb_KodeMataUang)
        KontenComboVisibilitas()
        txt_COA.Text = Kosongan
        txt_NamaAkun.Text = Kosongan
        txt_Kurs.Text = Kosongan
        txt_SaldoAwal.Text = Kosongan
        txt_SaldoAwal_IDR.Text = Kosongan
        txt_Uraian.Text = Kosongan
        btn_Batal.Content = teks_Tutup

        ProsesSuntingDatabase = False

        ProsesResetForm = False

    End Sub



    Sub KontenComboDebetKredit()
        cmb_DebetKredit.Items.Clear()
        cmb_DebetKredit.Items.Add(dk_DEBET_)
        cmb_DebetKredit.Items.Add(dk_KREDIT_)
        cmb_DebetKredit.Text = Kosongan
    End Sub

    Sub KontenComboVisibilitas()
        cmb_Visibilitas.Items.Clear()
        cmb_Visibilitas.Items.Add(Pilihan_Ya)
        cmb_Visibilitas.Items.Add(Pilihan_Tidak)
        cmb_Visibilitas.SelectedValue = Pilihan_Ya
    End Sub


    Sub LogikaKodeMataUang()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            lbl_SaldoAwal.Text = "Saldo Akhir (" & KodeMataUang & ")"
            lbl_SaldoAwal_IDR.Text = "Saldo Akhir (IDR)"
        Else
            lbl_SaldoAwal.Text = "Saldo Awal (" & KodeMataUang & ")"
            lbl_SaldoAwal_IDR.Text = "Saldo Awal (IDR)"
        End If
        If KodeMataUang = KodeMataUang_IDR Then
            txt_Kurs.Text = 1
            txt_SaldoAwal.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparate), Style)
            lbl_Kurs.Visibility = Visibility.Collapsed
            txt_Kurs.Visibility = Visibility.Collapsed
            lbl_SaldoAwal_IDR.Visibility = Visibility.Collapsed
            txt_SaldoAwal_IDR.Visibility = Visibility.Collapsed
        Else
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then txt_Kurs.Text = KursTengahBI_AkhirTahunIni(KodeMataUang)
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then txt_Kurs.Text = KursTengahBI_AkhirTahunLalu(KodeMataUang)
            txt_SaldoAwal.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsing), Style)
            lbl_Kurs.Visibility = Visibility.Visible
            txt_Kurs.Visibility = Visibility.Visible
            lbl_SaldoAwal_IDR.Visibility = Visibility.Visible
            txt_SaldoAwal_IDR.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub txt_COA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COA.TextChanged
        COA = txt_COA.Text
        cmb_DebetKredit.SelectedValue = PenentuanDEBETKREDIT_COA(COA)
        If Len(COA) = 5 Then
            If FungsiForm = FungsiForm_TAMBAH Then
                Dim KodeCOATersedia As Boolean = True
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT COA FROM tbl_COA WHERE COA = '" & COA & " ' ", KoneksiDatabaseGeneral)
                dr_ExecuteReader()
                dr.Read()
                If dr.HasRows Then KodeCOATersedia = False
                AksesDatabase_General(Tutup)
                If KodeCOATersedia = False Then
                    Pesan_Peringatan("Kode COA " & COA & " sudah ada." & Enter2Baris &
                       "Silakan masukkan 'Kode COA' yang lain.")
                    txt_COA.Text = Kosongan
                    txt_COA.Focus()
                    Return
                End If
            End If
            If AmbilAngka(COA) >= AwalAkunBiaya Then
                txt_SaldoAwal.IsEnabled = False
                txt_SaldoAwal.Text = Kosongan
            Else
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                    txt_SaldoAwal.IsEnabled = True
                Else
                    txt_SaldoAwal.IsEnabled = False
                End If
            End If
        End If
    End Sub


    Private Sub txt_NamaAkun_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaAkun.TextChanged
        NamaAkun = txt_NamaAkun.Text
    End Sub


    Private Sub cmb_DebetKredit_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DebetKredit.SelectionChanged
        DK = cmb_DebetKredit.SelectedValue
    End Sub

    Private Sub cmb_KodeMataUang_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KodeMataUang.SelectionChanged
        txt_SaldoAwal.Text = Kosongan 'Supaya lebih aman, sebaiknya direset lagi.
        KodeMataUang = cmb_KodeMataUang.SelectedValue
        LogikaKodeMataUang()
    End Sub

    Private Sub txt_Kurs_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Kurs.TextChanged
        Kurs = AmbilAngka_Desimal(txt_Kurs.Text)
        PerhitunganKurs()
    End Sub

    Dim LoopingKeyDown As Integer
    Private Sub txt_SaldoAwal_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_SaldoAwal.KeyDown
        LoopingKeyDown = 0
    End Sub
    Private Sub txt_SaldoAwal_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwal.TextChanged
        LoopingKeyDown += 1
        SaldoAwal = AmbilAngka_Desimal(txt_SaldoAwal.Text)
        If ProsesAmbilAngkaDesimal = False Then
            If LoopingKeyDown <= 1 Then PesanPeringatan("Silakan isi kolom angka dengan benar!")
            txt_SaldoAwal.Text = Kosongan
            txt_SaldoAwal.Focus()
            Return
        End If
        PerhitunganKurs()
        If SaldoAwal > 0 Then
            cmb_Visibilitas.SelectedValue = Pilihan_Ya
            cmb_Visibilitas.IsEnabled = False
        Else
            cmb_Visibilitas.IsEnabled = True
        End If
    End Sub

    Sub PerhitunganKurs()
        txt_SaldoAwal_IDR.Text = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, SaldoAwal)
    End Sub

    Private Sub txt_SaldoAwal_IDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwal_IDR.TextChanged
        SaldoAwal_IDR = AmbilAngka(txt_SaldoAwal_IDR.Text)
    End Sub


    Private Sub txt_Uraian_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Uraian.TextChanged
        Uraian = txt_Uraian.Text
    End Sub


    Private Sub cmb_Visibilitas_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Visibilitas.SelectionChanged
        Visibilitas = cmb_Visibilitas.SelectedValue
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Validasi Form :
        If COA = Kosongan Then
            Pesan_Peringatan("Silakan isi kolom 'Kode Akun'.")
            txt_COA.Focus()
            Return
        End If

        If PanjangTeks(COA) <> JumlahDigitCOA Then
            Pesan_Peringatan("Jumlah Digit COA harus 5.")
            txt_COA.Focus()
            Return
        End If

        If AmbilAngka(COA) >= 90000 Then
            Pesan_Peringatan("Angka 9 tidak diperkenankan sebagai digit awal COA.")
            txt_COA.Focus()
            Return
        End If

        If NamaAkun = Kosongan Then
            Pesan_Peringatan("Silakan isi kolom 'Nama Akun'.")
            txt_NamaAkun.Focus()
            Return
        End If

        If DK = Kosongan Then
            Pesan_Peringatan("Silakan pilih 'Debet/Kredit'.")
            cmb_DebetKredit.Focus()
            Return
        End If

        If KodeMataUang = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_KodeMataUang, "Kode Mata Uang")
            Return
        End If

        TrialBalance_Mentahkan() 'Mentahkan data dari awal lebih baik (Sebelum proses tambah COA maupun proses edit COA)

        'Jika Bermaksud Menambah Data COA Baru :
        If FungsiForm = FungsiForm_TAMBAH Then

            'Simpan Data ke Tabel COA
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" INSERT INTO tbl_COA VALUES (" &
                                  " '" & COA & "', " &
                                  " '" & NamaAkun & "', " &
                                  " '" & KodeMataUang & "', " &
                                  " '" & DK & "', " &
                                  " '" & DesimalFormatSimpan(SaldoAwal) & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & Uraian & "', " &
                                  " '" & Visibilitas & "' " &
                                  ") ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
                AksesDatabase_General(Tutup)
            End Try
            AksesDatabase_General(Tutup)

            'Penyimpanan Data Saldo Awal COA ke seluruh Database Tahun Buku (Saldo Awal dibikin 0 semua dulu) :
            If ProsesSuntingDatabase = True Then
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ", KoneksiDatabaseGeneral)
                dr = cmd.ExecuteReader
                Dim cmdSimpanCOA As New OdbcCommand
                Do While dr.Read
                    BukaDatabaseTransaksi_Alternatif(dr.Item("Tahun_Buku"))
                    cmdSimpanCOA = New OdbcCommand(" INSERT INTO tbl_SaldoAwalCOA (COA, Saldo_Awal) VALUES ('" & COA & "', 0) ", KoneksiDatabaseTransaksi_Alternatif)
                    Try
                        cmdSimpanCOA.ExecuteNonQuery()
                        ProsesSuntingDatabase = True
                    Catch ex As Exception
                        ProsesSuntingDatabase = False
                        AksesDatabase_Transaksi(Tutup)
                        Exit Do
                    End Try
                    TutupDatabaseTransaksi_Alternatif()
                Loop
                AksesDatabase_General(Tutup)
            End If

        End If

        'Jika Bermaksud Edit Data COA yang sudah ada :
        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_COA SET " &
                                  " Nama_Akun       = '" & NamaAkun & "', " &
                                  " D_K             = '" & DK & "', " &
                                  " Kode_Mata_Uang  = '" & KodeMataUang & "', " &
                                  " Saldo_Awal      = '" & DesimalFormatSimpan(SaldoAwal) & "', " &
                                  " Uraian          = '" & Uraian & "', " &
                                  " Visibilitas     = '" & Visibilitas & "' " &
                                  " WHERE COA       = '" & COA & "' ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)
        End If

        'Simpan Saldo Awal pada Tahun Buku Aktif (Kode ini berlaku untuk PENYIMPANAN maupun EDIT COA), karena posisinya di luar IF Tambah atau Edit)
        If ProsesSuntingDatabase = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_SaldoAwalCOA " &
                                  " SET Saldo_Awal  = '" & DesimalFormatSimpan(SaldoAwal) & "' " &
                                  " WHERE COA       = '" & COA & "' ",
                                  KoneksiDatabaseTransaksi)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
                AksesDatabase_Transaksi(Tutup)
            End Try
            AksesDatabase_Transaksi(Tutup)
        End If
        If ProsesSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then
                Pesan_Sukses("Data COA berhasil disimpan.")
                ResetForm()
                FungsiForm = FungsiForm_TAMBAH
            End If
            If FungsiForm = FungsiForm_EDIT Then
                Pesan_Sukses("Data COA berhasil diedit.")
                Select Case JalurMasuk
                    Case Halaman_DATACOA
                        usc_DataCOA.rowviewUtama("COA_") = COA
                        usc_DataCOA.rowviewUtama("Nama_Akun") = NamaAkun
                        usc_DataCOA.rowviewUtama("D_K") = DK
                        usc_DataCOA.rowviewUtama("Kode_Mata_Uang") = KodeMataUang
                        usc_DataCOA.rowviewUtama("Saldo_Awal") = SaldoAwal_IDR
                        usc_DataCOA.rowviewUtama("Uraian_") = Uraian
                        usc_DataCOA.rowviewUtama("Visibilitas_") = Visibilitas
                    Case Halaman_SALDOAWALHUTANGUSAHA
                        frm_SaldoAwalHutangUsaha.RefreshTampilanData()
                    Case Halaman_TUTUPBUKU
                        If usc_TutupBuku.StatusAktif Then usc_TutupBuku.TampilkanData()
                End Select
                Me.Close()
            End If
            If usc_DataCOA.StatusAktif Then usc_DataCOA.TampilkanData()
            If usc_TutupBuku.StatusAktif Then usc_TutupBuku.TampilkanData()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" DELETE FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
                cmd.ExecuteNonQuery()
                AksesDatabase_General(Tutup)
                Pesan_Gagal("Data COA gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
            If FungsiForm = FungsiForm_EDIT Then
                Pesan_Gagal("Data COA gagal diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
                'Jika penyimpanan gagal, tidak perlu revisi value Saldo Awal di tbl_COA, karena dengan sendirinya value tersebut akan ter-update sesuai value dari tbl_SaldoAwalCOA di databse transaksi secara otomatis saat masuk Tahun Buku.
            End If
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub



    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        cmb_DebetKredit.IsReadOnly = True
        cmb_Visibilitas.IsReadOnly = True
        txt_COA.MaxLength = JumlahDigitCOA
        cmb_KodeMataUang.IsReadOnly = True
        txt_Kurs.IsReadOnly = True
        txt_SaldoAwal.IsReadOnly = False
        txt_SaldoAwal_IDR.IsReadOnly = True
    End Sub

End Class
