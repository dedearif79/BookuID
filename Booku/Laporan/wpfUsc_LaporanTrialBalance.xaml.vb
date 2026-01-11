Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfUsc_LaporanTrialBalance

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Public JalurMasuk

    Dim QueryTampilan

    Dim TrialBalanceDone As Boolean
    Dim LoopingTrialBalance As Boolean
    Dim JedaPerBarisCOA = 0 'milidetik
    Dim JedaPerKelompokCOA = 3 'milidetik
    Dim JumlahBukuYangDicek As Integer

    Dim KesesuaianData_TrialBalance As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        If JalurMasuk = Halaman_MENUUTAMA Then Me.Visibility = Visibility.Visible
        If JalurMasuk <> Halaman_MENUUTAMA Then Me.Visibility = Visibility.Collapsed

        lbl_JudulForm.Text = frm_LaporanTrialBalance.JudulForm

        pnl_Progress.Visibility = Visibility.Collapsed

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub btn_Proses_Click(sender As Object, e As RoutedEventArgs) Handles btn_Proses.Click

        If JalurMasuk <> Halaman_MENUUTAMA Then Me.Visibility = Visibility.Collapsed  'Ini Penting. Jangan dihapus..! Untuk jaga-jaga aja.

        'CEK KESESUAIAN DATA :
        KesesuaianData_TrialBalance = True
        TahunPajak = TahunBukuAktif 'Ini penting untuk ditentukan di awal. Untuk kepentingan Kesesuaian Saldo Awal Semua Jenis PPh.

        CekKesesuaianData()
        pnl_Progress.Visibility = Visibility.Collapsed
        Terabas()

        If KesesuaianData_TrialBalance Then
            Jeda(3333)
            RefreshTampilanData()
        End If


    End Sub
    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Dim pesan_DataTidakSesuai As String
    Sub CekKesesuaianData()

        pnl_DataGridUtama.Visibility = Visibility.Collapsed

        JumlahBukuYangDicek = 37 'Angka ini bisa berubah-ubah, tergantung berapa jumlah Data yang harus dicek (disesuaikan).
        ProgressMaximum = JumlahBukuYangDicek - 1
        ProgressInfo = "Mohon tunggu..! Sistem sedang mengecek kesesuaian Saldo Awal"
        StartProgress()

        If JenisTahunBuku <> JenisTahunBuku_LAMPAU Then

            pesan_DataTidakSesuai = "Proses 'Trial Balance' mengalami kendala, karena :"


            '1. Cek Kesesuaian Jurnal Pada Data Amortisasi Biaya :
            frm_DaftarAmortisasiBiaya.CekKesesuaianData()
            If usc_DaftarAmortisasiBiaya.KesesuaianJurnal = False Then
                KesesuaianData_TrialBalance = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Ada Data Amortisasi Biaya yang belum didorong ke Jurnal"
            End If
            ProgressUp(pgb_Progress)

            '2. Cek Kesesuaian Jurnal Pada Data Penyusutan Asset Tetap :
            usc_DaftarPenyusutanAssetTetap.ResetFilter()
            usc_DaftarPenyusutanAssetTetap.TampilkanData_Detail_Rekap() '(Untuk menampilkan Jurnal)
            If usc_DaftarPenyusutanAssetTetap.KesesuaianJurnal = False Then
                KesesuaianData_TrialBalance = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Ada Data Penyusutan Asset Tetap yang belum didorong ke Jurnal"
            End If
            ProgressUp(pgb_Progress)

            frm_BukuPengawasanHutangUsaha_NonAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangUsaha_Afiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangUsaha_Impor_USD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangUsaha_Impor_AUD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangUsaha_Impor_JPY.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangUsaha_Impor_CNY.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangUsaha_Impor_EUR.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangUsaha_Impor_SGD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangUsaha_NonAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangUsaha_Afiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangUsaha_Ekspor_USD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangUsaha_Ekspor_AUD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangUsaha_Ekspor_JPY.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangUsaha_Ekspor_CNY.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangUsaha_Ekspor_EUR.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangUsaha_Ekspor_SGD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangPPhPasal21.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangPPhPasal23.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangPPhPasal25.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangPPhPasal26.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangPPhPasal42.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangKaryawan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangKaryawan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangPemegangSaham.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangPemegangSaham.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangBPJSKesehatan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangBPJSKetenagakerjaan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangKoperasiKaryawan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangSerikat.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangBank.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangLeasing.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangPihakKetiga.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangPihakKetiga.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanHutangAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            frm_BukuPengawasanPiutangAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)

            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_NonAfiliasi.KesesuaianSaldoAwal, "Hutang Usaha Non-Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Afiliasi.KesesuaianSaldoAwal, "Hutang Usaha Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_USD.KesesuaianSaldoAwal, "Hutang Usaha (USD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_AUD.KesesuaianSaldoAwal, "Hutang Usaha (AUD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_JPY.KesesuaianSaldoAwal, "Hutang Usaha (JPY)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_CNY.KesesuaianSaldoAwal, "Hutang Usaha (CNY)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_EUR.KesesuaianSaldoAwal, "Hutang Usaha (EUR)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_SGD.KesesuaianSaldoAwal, "Hutang Usaha (SGD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_NonAfiliasi.KesesuaianSaldoAwal, "Piutang Usaha Non-Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Afiliasi.KesesuaianSaldoAwal, "Piutang Usaha Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_USD.KesesuaianSaldoAwal, "Piutang Usaha (USD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_AUD.KesesuaianSaldoAwal, "Piutang Usaha (AUD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.KesesuaianSaldoAwal, "Piutang Usaha (JPY)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_CNY.KesesuaianSaldoAwal, "Piutang Usaha (CNY)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.KesesuaianSaldoAwal, "Piutang Usaha (EUR)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_SGD.KesesuaianSaldoAwal, "Piutang Usaha (SGD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAwal_100, "Hutang PPh Pasal 21 - 100")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAwal_401, "Hutang PPh Pasal 21 - 401")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_100, "Hutang PPh Pasal 23 - 100")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_101, "Hutang PPh Pasal 23 - 101")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_102, "Hutang PPh Pasal 23 - 102")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_103, "Hutang PPh Pasal 23 - 103")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_104, "Hutang PPh Pasal 23 - 104")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal25.KesesuaianSaldoAwal, "Hutang PPh Pasal 25")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_100, "Hutang PPh Pasal 26 - 100")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_101, "Hutang PPh Pasal 26 - 101")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_102, "Hutang PPh Pasal 26 - 102")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_103, "Hutang PPh Pasal 26 - 103")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_104, "Hutang PPh Pasal 26 - 104")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_105, "Hutang PPh Pasal 26 - 105")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_402, "Hutang PPh Pasal 42 - 402")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_403, "Hutang PPh Pasal 42 - 403")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_409, "Hutang PPh Pasal 42 - 409")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_419, "Hutang PPh Pasal 42 - 419")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangKaryawan.KesesuaianSaldoAwal, "Hutang Karyawan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangKaryawan.KesesuaianSaldoAwal, "Piutang Karyawan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPemegangSaham.KesesuaianSaldoAwal, "Hutang Pemegang Saham")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangPemegangSaham.KesesuaianSaldoAwal, "Piutang Pemegang Saham")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangBPJSKesehatan.KesesuaianSaldoAwal, "Hutang BPJS Kesehatan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangBPJSKetenagakerjaan.KesesuaianSaldoAwal, "Hutang BPJS Ketenagakerjaan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangKoperasiKaryawan.KesesuaianSaldoAwal, "Hutang Koperasi Karyawan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangSerikat.KesesuaianSaldoAwal, "Hutang Serikat")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangBank.KesesuaianSaldoAwal, "Hutang Bank")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangLeasing.KesesuaianSaldoAwal, "Hutang Leasing")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPihakKetiga.KesesuaianSaldoAwal, "Hutang Pihak Ketiga")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangPihakKetiga.KesesuaianSaldoAwal, "Piutang Pihak Ketiga")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangAfiliasi.KesesuaianSaldoAwal, "Hutang Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangAfiliasi.KesesuaianSaldoAwal, "Piutang Afiliasi")

            '=========================================== SETELAH PENGECEKAN : ===========================================
            KetersediaanMenuHalaman(pnl_Halaman, True) 'Jaga-jaga ada proses yang tidak selesai. Ketersediaan UI harus dipulihkan
            If KesesuaianData_TrialBalance = False Then
                pgb_Progress.Foreground = WarnaPeringatan_WPF
                pesan_DataTidakSesuai &= "." & Enter2Baris &
                    "Silakan perbaiki dan sesuaikan semua data tersebut agar proses 'Trial Balance' berjalan dengan baik." & Enter2Baris &
                    "Atau silakan klik tombol 'Yes' untuk melanjutkan 'Trial Balance'."
                If JalurMasuk <> Halaman_MENUUTAMA Then TutupHalaman()
                Pilihan = MessageBox.Show(pesan_DataTidakSesuai, "Perhatian..!", MessageBoxButtons.YesNo)
                If Pilihan = vbNo Then Return
                If Pilihan = vbYes Then
                    KetersediaanMenuHalaman(pnl_Halaman, False)
                    KesesuaianData_TrialBalance = True
                End If
            End If
            '============================================================================================================
        End If

    End Sub
    Sub CekKesesuaianSaldoAwal(KesesuaianSaldoAwal As Boolean, NamaBP As String)
        If KesesuaianSaldoAwal = False Then
            KesesuaianData_TrialBalance = False
            pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Data Saldo Awal " & NamaBP & " tidak sesuai"
        End If
    End Sub


    Sub RefreshTampilanData()

        Proses = True
        TrialBalanceDone = False
        LoopingTrialBalance = True
        'TrialBalance_Mentahkan() 'Mentahkan dulu data, karena khawatir proses berhenti di tengah jalan.
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabaseGeneral = False Then Return
        Try
            cmd = New OdbcCommand(" SELECT COA FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Proses = True
        Catch ex As Exception
            Proses = False
            pesan_AdaMasalahDenganKoneksiDatabase()
            Return
        End Try
        Dim JumlahBarisCOA = 0
        Do While dr.Read
            JumlahBarisCOA += 1
        Loop
        AksesDatabase_General(Tutup)
        ProgressMaximum = JumlahBarisCOA
        ProgressInfo = "Mohon tunggu..! Sistem sedang merekap seluruh COA."
        StartProgress()
        TampilkanData()
        If Proses = False Then TrialBalanceDone = False
        If TrialBalanceDone = True Then
            TrialBalance_Matangkan()
            PesanSukses("Trial Balance BERHASIL.")
            pnl_Progress.Visibility = Visibility.Collapsed
            Select Case JalurMasuk
                Case Halaman_MENUUTAMA
                Case Halaman_TUTUPBUKU
                    If usc_TutupBuku.StatusAktif Then usc_TutupBuku.TampilkanData()
                    MsgBox("Silakan periksa data dengan teliti sebelum melakukan 'Tutup Buku'")
                    TutupHalaman()
                Case Halaman_LAPORANLABARUGI
                    'X_frm_Laporan_LabaRugi_X.TampilkanData()
                    TutupHalaman()
                Case Halaman_LAPORANNERACA
                    'X_frm_Laporan_Neraca_X.TampilkanData()
                    TutupHalaman()
                Case Else
                    TutupHalaman()
            End Select
        Else
            LoopingTrialBalance = False 'Keluar dari looping dan BackgroundWorker
            datatabelUtama.Rows.Clear()
            MsgBox("Trial Balance GAGAL." & Enter2Baris & teks_SilakanCobaLagi_Database)
            datatabelUtama.Rows.Clear() 'Kenapa coding ini harus dua kali..? Karena kita bekerja di BackgroundWorker. Jadi, yang terakhir ini untuk menyapu sisa-sisa baris yang masih ada. Intinya : BARIS INI JANGAN DIHAPUS...!!!
            If JalurMasuk <> Halaman_MENUUTAMA Then TutupHalaman()
        End If
        Proses = False

    End Sub

    Sub TampilkanData()

        KetersediaanMenuHalaman(pnl_Halaman, False)
        pnl_DataGridUtama.Visibility = Visibility.Visible

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        QueryTampilan = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' "
        DataPerKategoriCOA()

        BersihkanSeleksi()

        TrialBalanceDone = True

        If usc_TutupBuku.StatusAktif Then usc_TutupBuku.TampilkanData() 'Ini jangan dihapus. Ini dibutuhkan agar data pada Form Tutup Buku ter-refresh. Khawatir form ini sedang dibuka oleh user pada saat Trial Balance.

        KetersediaanMenuHalaman(pnl_Halaman, True)

    End Sub


    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
    End Sub


    Dim TahunTrialBalance
    Dim MutasiDebetJanuari As Decimal
    Dim MutasiKreditJanuari As Decimal
    Dim SaldoAkhirJanuari As Decimal
    Dim MutasiDebetFebruari As Decimal
    Dim MutasiKreditFebruari As Decimal
    Dim SaldoAkhirFebruari As Decimal
    Dim MutasiDebetMaret As Decimal
    Dim MutasiKreditMaret As Decimal
    Dim SaldoAkhirMaret As Decimal
    Dim MutasiDebetApril As Decimal
    Dim MutasiKreditApril As Decimal
    Dim SaldoAkhirApril As Decimal
    Dim MutasiDebetMei As Decimal
    Dim MutasiKreditMei As Decimal
    Dim SaldoAkhirMei As Decimal
    Dim MutasiDebetJuni As Decimal
    Dim MutasiKreditJuni As Decimal
    Dim SaldoAkhirJuni As Decimal
    Dim MutasiDebetJuli As Decimal
    Dim MutasiKreditJuli As Decimal
    Dim SaldoAkhirJuli As Decimal
    Dim MutasiDebetAgustus As Decimal
    Dim MutasiKreditAgustus As Decimal
    Dim SaldoAkhirAgustus As Decimal
    Dim MutasiDebetSeptember As Decimal
    Dim MutasiKreditSeptember As Decimal
    Dim SaldoAkhirSeptember As Decimal
    Dim MutasiDebetOktober As Decimal
    Dim MutasiKreditOktober As Decimal
    Dim SaldoAkhirOktober As Decimal
    Dim MutasiDebetNopember As Decimal
    Dim MutasiKreditNopember As Decimal
    Dim SaldoAkhirNopember As Decimal
    Dim MutasiDebetDesember As Decimal
    Dim MutasiKreditDesember As Decimal
    Dim SaldoAkhirDesember As Decimal

    Sub DataPerKategoriCOA()

        TahunTrialBalance = TahunBukuAktif

        'Data Tabel
        Dim COA
        Dim NamaAkun
        Dim DebetKreditCOA
        Dim SaldoAwal As Decimal
        Dim KodeMataUang As String
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabaseGeneral = False Then
            Proses = False
            Return
        End If
        AksesDatabase_Transaksi(Buka)
        Try
            cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Proses = True
        Catch ex As Exception
            Proses = False
            Return
        End Try
        Do While dr.Read
            Try
                If LoopingTrialBalance = False Then Exit Do
                COA = dr.Item("COA")
                NamaAkun = dr.Item("Nama_Akun")
                DebetKreditCOA = dr.Item("D_K")
                SaldoAwal = dr.Item("Saldo_Awal")
                KodeMataUang = dr.Item("Kode_Mata_Uang")
                MutasiPerBulan(BulanAngka_Januari, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAwal, MutasiDebetJanuari, MutasiKreditJanuari, SaldoAkhirJanuari)
                MutasiPerBulan(BulanAngka_Februari, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirJanuari, MutasiDebetFebruari, MutasiKreditFebruari, SaldoAkhirFebruari)
                MutasiPerBulan(BulanAngka_Maret, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirFebruari, MutasiDebetMaret, MutasiKreditMaret, SaldoAkhirMaret)
                MutasiPerBulan(BulanAngka_April, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirMaret, MutasiDebetApril, MutasiKreditApril, SaldoAkhirApril)
                MutasiPerBulan(BulanAngka_Mei, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirApril, MutasiDebetMei, MutasiKreditMei, SaldoAkhirMei)
                MutasiPerBulan(BulanAngka_Juni, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirMei, MutasiDebetJuni, MutasiKreditJuni, SaldoAkhirJuni)
                MutasiPerBulan(BulanAngka_Juli, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirJuni, MutasiDebetJuli, MutasiKreditJuli, SaldoAkhirJuli)
                MutasiPerBulan(BulanAngka_Agustus, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirJuli, MutasiDebetAgustus, MutasiKreditAgustus, SaldoAkhirAgustus)
                MutasiPerBulan(BulanAngka_September, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirAgustus, MutasiDebetSeptember, MutasiKreditSeptember, SaldoAkhirSeptember)
                MutasiPerBulan(BulanAngka_Oktober, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirSeptember, MutasiDebetOktober, MutasiKreditOktober, SaldoAkhirOktober)
                MutasiPerBulan(BulanAngka_Nopember, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirOktober, MutasiDebetNopember, MutasiKreditNopember, SaldoAkhirNopember)
                MutasiPerBulan(BulanAngka_Desember, COA, NamaAkun, KodeMataUang, DebetKreditCOA, SaldoAkhirNopember, MutasiDebetDesember, MutasiKreditDesember, SaldoAkhirDesember)
                datatabelUtama.Rows.Add(
                    NamaAkun, COA, SaldoAwal,
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Januari), MutasiDebetJanuari), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Januari), MutasiKreditJanuari), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Januari), SaldoAkhirJanuari),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Februari), MutasiDebetFebruari), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Februari), MutasiKreditFebruari), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Februari), SaldoAkhirFebruari),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Maret), MutasiDebetMaret), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Maret), MutasiKreditMaret), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Maret), SaldoAkhirMaret),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_April), MutasiDebetApril), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_April), MutasiKreditApril), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_April), SaldoAkhirApril),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Mei), MutasiDebetMei), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Mei), MutasiKreditMei), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Mei), SaldoAkhirMei),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Juni), MutasiDebetJuni), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Juni), MutasiKreditJuni), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Juni), SaldoAkhirJuni),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Juli), MutasiDebetJuli), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Juli), MutasiKreditJuli), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Juli), SaldoAkhirJuli),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Agustus), MutasiDebetAgustus), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Agustus), MutasiKreditAgustus), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Agustus), SaldoAkhirAgustus),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_September), MutasiDebetSeptember), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_September), MutasiKreditSeptember), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_September), SaldoAkhirSeptember),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Oktober), MutasiDebetOktober), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Oktober), MutasiKreditOktober), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Oktober), SaldoAkhirOktober),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Nopember), MutasiDebetNopember), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Nopember), MutasiKreditNopember), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Nopember), SaldoAkhirNopember),
                    AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Desember), MutasiDebetDesember), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Desember), MutasiKreditDesember), AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, BulanAngka_Desember), SaldoAkhirDesember))
                ProgressUp(pgb_Progress)
                Dim QuerySimpanSaldo = " UPDATE tbl_COA SET " &
                                       " Debet_Januari      = '" & DesimalFormatSimpan(MutasiDebetJanuari) & "', " &
                                       " Kredit_Januari     = '" & DesimalFormatSimpan(MutasiKreditJanuari) & "', " &
                                       " Saldo_Januari      = '" & DesimalFormatSimpan(SaldoAkhirJanuari) & "', " &
                                       " Debet_Februari     = '" & DesimalFormatSimpan(MutasiDebetFebruari) & "', " &
                                       " Kredit_Februari    = '" & DesimalFormatSimpan(MutasiKreditFebruari) & "', " &
                                       " Saldo_Februari     = '" & DesimalFormatSimpan(SaldoAkhirFebruari) & "', " &
                                       " Debet_Maret        = '" & DesimalFormatSimpan(MutasiDebetMaret) & "', " &
                                       " Kredit_Maret       = '" & DesimalFormatSimpan(MutasiKreditMaret) & "', " &
                                       " Saldo_Maret        = '" & DesimalFormatSimpan(SaldoAkhirMaret) & "', " &
                                       " Debet_April        = '" & DesimalFormatSimpan(MutasiDebetApril) & "', " &
                                       " Kredit_April       = '" & DesimalFormatSimpan(MutasiKreditApril) & "', " &
                                       " Saldo_April        = '" & DesimalFormatSimpan(SaldoAkhirApril) & "', " &
                                       " Debet_Mei          = '" & DesimalFormatSimpan(MutasiDebetMei) & "', " &
                                       " Kredit_Mei         = '" & DesimalFormatSimpan(MutasiKreditMei) & "', " &
                                       " Saldo_Mei          = '" & DesimalFormatSimpan(SaldoAkhirMei) & "', " &
                                       " Debet_Juni         = '" & DesimalFormatSimpan(MutasiDebetJuni) & "', " &
                                       " Kredit_Juni        = '" & DesimalFormatSimpan(MutasiKreditJuni) & "', " &
                                       " Saldo_Juni         = '" & DesimalFormatSimpan(SaldoAkhirJuni) & "', " &
                                       " Debet_Juli         = '" & DesimalFormatSimpan(MutasiDebetJuli) & "', " &
                                       " Kredit_Juli        = '" & DesimalFormatSimpan(MutasiKreditJuli) & "', " &
                                       " Saldo_Juli         = '" & DesimalFormatSimpan(SaldoAkhirJuli) & "', " &
                                       " Debet_Agustus      = '" & DesimalFormatSimpan(MutasiDebetAgustus) & "', " &
                                       " Kredit_Agustus     = '" & DesimalFormatSimpan(MutasiKreditAgustus) & "', " &
                                       " Saldo_Agustus      = '" & DesimalFormatSimpan(SaldoAkhirAgustus) & "', " &
                                       " Debet_September    = '" & DesimalFormatSimpan(MutasiDebetSeptember) & "', " &
                                       " Kredit_September   = '" & DesimalFormatSimpan(MutasiKreditSeptember) & "', " &
                                       " Saldo_September    = '" & DesimalFormatSimpan(SaldoAkhirSeptember) & "', " &
                                       " Debet_Oktober      = '" & DesimalFormatSimpan(MutasiDebetOktober) & "', " &
                                       " Kredit_Oktober     = '" & DesimalFormatSimpan(MutasiKreditOktober) & "', " &
                                       " Saldo_Oktober      = '" & DesimalFormatSimpan(SaldoAkhirOktober) & "', " &
                                       " Debet_Nopember     = '" & DesimalFormatSimpan(MutasiDebetNopember) & "', " &
                                       " Kredit_Nopember    = '" & DesimalFormatSimpan(MutasiKreditNopember) & "', " &
                                       " Saldo_Nopember     = '" & DesimalFormatSimpan(SaldoAkhirNopember) & "', " &
                                       " Debet_Desember     = '" & DesimalFormatSimpan(MutasiDebetDesember) & "', " &
                                       " Kredit_Desember    = '" & DesimalFormatSimpan(MutasiKreditDesember) & "', " &
                                       " Saldo_Desember     = '" & DesimalFormatSimpan(SaldoAkhirDesember) & "' " &
                                       " WHERE COA          = '" & COA & "' "
                Dim cmdSimpanSaldo = New OdbcCommand(QuerySimpanSaldo, KoneksiDatabaseGeneral)
                cmdSimpanSaldo.ExecuteNonQuery()
                Jeda(JedaPerBarisCOA)
                Proses = True
            Catch ex As Exception
                Proses = False
                Exit Do
            End Try
        Loop
        AksesDatabase_Transaksi(Tutup)
        AksesDatabase_General(Tutup)
    End Sub

    Sub MutasiPerBulan(ByVal BulanAngka As String,
                       ByVal COA As String,
                       ByVal NamaAkun As String,
                       ByVal KodeMataUang As String,
                       ByVal DebetKreditCOA As String,
                       ByVal SaldoAwalBulan As Decimal,
                       ByRef MutasiDebetBulan As Decimal,
                       ByRef MutasiKreditBulan As Decimal,
                       ByRef SaldoAkhirBulan As Decimal)
        Dim QueryBulan = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE Valid <> '" & _X_ & "' " &
            " AND COA = '" & COA & "'" &
            " AND Status_Approve = 1 " &
            " AND Kode_Mata_Uang = '" & KodeMataUang & "' " &
            " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunTrialBalance & "-" & KonversiAngkaKeStringDuaDigit(BulanAngka) & "' "
        Dim cmdBulan = New OdbcCommand(QueryBulan, KoneksiDatabaseTransaksi)
        Dim drBulan = cmdBulan.ExecuteReader
        MutasiDebetBulan = 0
        MutasiKreditBulan = 0
        Do While drBulan.Read
            MutasiDebetBulan += drBulan.Item("Jumlah_Debet")
            MutasiKreditBulan += drBulan.Item("Jumlah_Kredit")
        Loop
        If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
            If DebetKreditCOA = dk_DEBET_ Then SaldoAkhirBulan = MutasiDebetBulan - MutasiKreditBulan
            If DebetKreditCOA = dk_KREDIT_ Then SaldoAkhirBulan = MutasiKreditBulan - MutasiDebetBulan
        Else
            If DebetKreditCOA = dk_DEBET_ Then SaldoAkhirBulan = SaldoAwalBulan + MutasiDebetBulan - MutasiKreditBulan
            If DebetKreditCOA = dk_KREDIT_ Then SaldoAkhirBulan = SaldoAwalBulan + MutasiKreditBulan - MutasiDebetBulan
        End If
    End Sub




    Private Sub cmb_TahunTrialBalance_SelectedIndexChanged(sender As Object, e As EventArgs)
        'TampilkanData()
    End Sub
    Private Sub cmb_TahunTrialBalance_TextChanged(sender As Object, e As EventArgs)
        'TampilkanData()
    End Sub
    Private Sub cmb_TahunTrialBalance_KeyPress(sender As Object, e As KeyPressEventArgs)
        KunciTotalInputan(sender, e)
    End Sub







    Sub TutupHalaman()
        frm_LaporanTrialBalance.Close()
    End Sub


    'Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nama_Akun As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Saldo_Awal As New DataGridTextColumn
    Dim Mutasi_Debet_Januari As New DataGridTextColumn
    Dim Mutasi_Kredit_Januari As New DataGridTextColumn
    Dim Saldo_Januari As New DataGridTextColumn
    Dim Mutasi_Debet_Februari As New DataGridTextColumn
    Dim Mutasi_Kredit_Februari As New DataGridTextColumn
    Dim Saldo_Februari As New DataGridTextColumn
    Dim Mutasi_Debet_Maret As New DataGridTextColumn
    Dim Mutasi_Kredit_Maret As New DataGridTextColumn
    Dim Saldo_Maret As New DataGridTextColumn
    Dim Mutasi_Debet_April As New DataGridTextColumn
    Dim Mutasi_Kredit_April As New DataGridTextColumn
    Dim Saldo_April As New DataGridTextColumn
    Dim Mutasi_Debet_Mei As New DataGridTextColumn
    Dim Mutasi_Kredit_Mei As New DataGridTextColumn
    Dim Saldo_Mei As New DataGridTextColumn
    Dim Mutasi_Debet_Juni As New DataGridTextColumn
    Dim Mutasi_Kredit_Juni As New DataGridTextColumn
    Dim Saldo_Juni As New DataGridTextColumn
    Dim Mutasi_Debet_Juli As New DataGridTextColumn
    Dim Mutasi_Kredit_Juli As New DataGridTextColumn
    Dim Saldo_Juli As New DataGridTextColumn
    Dim Mutasi_Debet_Agustus As New DataGridTextColumn
    Dim Mutasi_Kredit_Agustus As New DataGridTextColumn
    Dim Saldo_Agustus As New DataGridTextColumn
    Dim Mutasi_Debet_September As New DataGridTextColumn
    Dim Mutasi_Kredit_September As New DataGridTextColumn
    Dim Saldo_September As New DataGridTextColumn
    Dim Mutasi_Debet_Oktober As New DataGridTextColumn
    Dim Mutasi_Kredit_Oktober As New DataGridTextColumn
    Dim Saldo_Oktober As New DataGridTextColumn
    Dim Mutasi_Debet_Nopember As New DataGridTextColumn
    Dim Mutasi_Kredit_Nopember As New DataGridTextColumn
    Dim Saldo_Nopember As New DataGridTextColumn
    Dim Mutasi_Debet_Desember As New DataGridTextColumn
    Dim Mutasi_Kredit_Desember As New DataGridTextColumn
    Dim Saldo_Desember As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Saldo_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Januari", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Januari", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Januari", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Februari", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Februari", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Februari", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Maret", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Maret", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Maret", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_April", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_April", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_April", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Mei", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Mei", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Mei", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Juni", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Juni", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Juni", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Juli", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Juli", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Juli", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Agustus", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Agustus", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Agustus", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_September", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_September", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_September", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Oktober", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Oktober", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Oktober", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Nopember", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Nopember", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Nopember", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet_Desember", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit_Desember", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Desember", GetType(Int64))


        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 330, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal, "Saldo_Awal", "Saldo Awal", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Januari, "Mutasi_Debet_Januari", "Debet" & Enter1Baris & "Januari", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Januari, "Mutasi_Kredit_Januari", "Kredit" & Enter1Baris & "Januari", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Januari, "Saldo_Januari", "Saldo" & Enter1Baris & "Januari", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Februari, "Mutasi_Debet_Februari", "Debet" & Enter1Baris & "Februari", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Februari, "Mutasi_Kredit_Februari", "Kredit" & Enter1Baris & "Februari", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Februari, "Saldo_Februari", "Saldo" & Enter1Baris & "Februari", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Maret, "Mutasi_Debet_Maret", "Debet" & Enter1Baris & "Maret", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Maret, "Mutasi_Kredit_Maret", "Kredit" & Enter1Baris & "Maret", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Maret, "Saldo_Maret", "Saldo" & Enter1Baris & "Maret", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_April, "Mutasi_Debet_April", "Debet" & Enter1Baris & "April", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_April, "Mutasi_Kredit_April", "Kredit" & Enter1Baris & "April", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_April, "Saldo_April", "Saldo" & Enter1Baris & "April", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Mei, "Mutasi_Debet_Mei", "Debet" & Enter1Baris & "Mei", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Mei, "Mutasi_Kredit_Mei", "Kredit" & Enter1Baris & "Mei", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Mei, "Saldo_Mei", "Saldo" & Enter1Baris & "Mei", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Juni, "Mutasi_Debet_Juni", "Debet" & Enter1Baris & "Juni", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Juni, "Mutasi_Kredit_Juni", "Kredit" & Enter1Baris & "Juni", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Juni, "Saldo_Juni", "Saldo" & Enter1Baris & "Juni", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Juli, "Mutasi_Debet_Juli", "Debet" & Enter1Baris & "Juli", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Juli, "Mutasi_Kredit_Juli", "Kredit" & Enter1Baris & "Juli", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Juli, "Saldo_Juli", "Saldo" & Enter1Baris & "Juli", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Agustus, "Mutasi_Debet_Agustus", "Debet" & Enter1Baris & "Agustus", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Agustus, "Mutasi_Kredit_Agustus", "Kredit" & Enter1Baris & "Agustus", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Agustus, "Saldo_Agustus", "Saldo" & Enter1Baris & "Agustus", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_September, "Mutasi_Debet_September", "Debet" & Enter1Baris & "September", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_September, "Mutasi_Kredit_September", "Kredit" & Enter1Baris & "September", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_September, "Saldo_September", "Saldo" & Enter1Baris & "September", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Oktober, "Mutasi_Debet_Oktober", "Debet" & Enter1Baris & "Oktober", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Oktober, "Mutasi_Kredit_Oktober", "Kredit" & Enter1Baris & "Oktober", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Oktober, "Saldo_Oktober", "Saldo" & Enter1Baris & "Oktober", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Nopember, "Mutasi_Debet_Nopember", "Debet" & Enter1Baris & "Nopember", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Nopember, "Mutasi_Kredit_Nopember", "Kredit" & Enter1Baris & "Nopember", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Nopember, "Saldo_Nopember", "Saldo" & Enter1Baris & "Nopember", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet_Desember, "Mutasi_Debet_Desember", "Debet" & Enter1Baris & "Desember", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit_Desember, "Mutasi_Kredit_Desember", "Kredit" & Enter1Baris & "Desember", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Desember, "Saldo_Desember", "Saldo" & Enter1Baris & "Desember", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_DataGridUtama.Visibility = Visibility.Collapsed
    End Sub

    Sub StartProgress()
        pgb_Progress.Foreground = WarnaHijauSolid_WPF
        pnl_Progress.Visibility = Visibility.Visible
        pgb_Progress.Minimum = 0
        pgb_Progress.Maximum = ProgressMaximum
        pgb_Progress.Value = 0
        lbl_ProgressInfo.Text = ProgressInfo
    End Sub

    Sub datagridUtama_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
        KetentuanUkuran()
    End Sub
    Sub pnl_Konten_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles pnl_Konten.SizeChanged
        KetentuanUkuran()
    End Sub
    Dim LebarKonten As Integer
    Dim TinggiKonten As Integer
    Sub KetentuanUkuran()
        LebarKonten = pnl_Konten.ActualWidth
        TinggiKonten = pnl_Konten.ActualHeight
        datagridUtama.MaxHeight = TinggiKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
