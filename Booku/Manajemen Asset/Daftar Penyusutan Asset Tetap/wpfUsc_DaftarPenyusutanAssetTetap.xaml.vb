Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports bcomm
Imports ClosedXML

Public Class wpfUsc_DaftarPenyusutanAssetTetap

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public JalurMasuk

    Dim JudulForm
    Public JenisTampilan
    Public JenisTampilan_GLOBAL_Rinci = "GLOBAL - Rinci"
    Public JenisTampilan_GLOBAL_Rekap = "GLOBAL - Rekap"
    Public JenisTampilan_DETAIL_Rinci = "DETAIL -Rinci"
    Public JenisTampilan_DETAIL_Rekap = "DETAIL -Rekap"
    Dim TahunLaporan
    Dim TahunLaporanSebelumnya

    Dim FilterAkun As String
    Dim FilterKelompokHarta As String
    Dim FilterTahunPerolehan As String

    'Tabel Temporary
    Dim DataTabelSementara As New DataTable

    'Variabel untuk Data Terseleksi :
    Dim NomorID_Terseleksi
    Dim Id_Terindeks
    Dim KodeAsset_Terseleksi
    Dim NomorPembelian_Terseleksi
    Dim NamaAktiva_Terseleksi
    Dim COAAsset_Terseleksi
    Dim NamaAkunAsset_Terseleksi
    Dim COABiayaPenyusutan_Terseleksi
    Dim NamaAkunBiayaPenyusutan_Terseleksi
    Dim COAAkumulasiPenyusutan_Terseleksi
    Dim KelompokHarta_Terseleksi
    Dim Divisi_Terseleksi
    Dim TanggalPerolehan_Terseleksi
    Dim HargaPerolehan_Terseleksi
    Dim HargaPerolehanAwal_Terseleksi
    Dim Penambahan_Pengurangan_Terseleksi
    Dim HargaPerolehanAkhir_Terseleksi
    Dim PenyusutanTahunLaporanYangSudahDijurnal_Terseleksi
    Dim NSB_BerdasarkanJurnalTerakhir_Terseleksi
    Dim AkumulasiPenyusutanSampaiTahunSebelumnya_Terseleksi
    Dim AkumulasiPenyusutanSampaiDenganJurnalTerakhir_Terseleksi
    Dim Keterangan_Terseleksi
    Dim KodeClosing_Terseleksi
    Dim AssetSudahClosing_Terseleksi
    Dim TahunTransaksi_Terseleksi As Integer

    'Variabel untuk Kebutuhan Posting Jurnal :
    Dim TanggalTransaksi_Simpan
    Dim Bundelan
    Dim KodeLawanTransaksi, NamaLawanTransaksi
    Dim UraianTransaksi

    'Variabel Koneksi Database :
    Dim cmdCEKJURNAL As OdbcCommand
    Dim drCEKJURNAL As OdbcDataReader

    Public KesesuaianJurnal As Boolean

    Dim AssetSudahClosing As Boolean
    Dim AssetSudahClosingDiTahunIni As Boolean

    Dim JumlahAsset As Int64

    Dim teks_Jumlah = "         Jumlah"

    Dim ProsesPostingJurnal As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        btn_Posting.Visibility = Visibility.Collapsed           'kalau sistem sudah berjalan normal, tombol ini nanti dihapus saja
        btn_LihatJurnal.Visibility = Visibility.Collapsed       'kalau sistem sudah berjalan normal, tombol ini nanti dihapus saja
        ProsesPostingJurnal = False
        Terabas()

        ProsesLoadingForm = True

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            VisibilitasTombolImport(False)
        Else
            VisibilitasTombolImport(True)
        End If

        Select Case JalurMasuk
            Case Halaman_MENUUTAMA
                RefreshTampilanData()
            Case Halaman_LAPORANNERACALAJUR
                RefreshTampilanData()
                JenisTampilan = JenisTampilan_DETAIL_Rekap
                EksekusiTampilanData = True
                TampilkanData()
        End Select

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub ResetFilter()
        EksekusiTampilanData = False
        KontenComboFilterCOA()
        KontenComboFilterKelompokHarta()
        KontenComboFilterTahunPerolehan()
    End Sub


    Sub RefreshTampilanData()
        ResetFilter()
        KontenComboTahunLaporan()
        JenisTampilan = JenisTampilan_GLOBAL_Rinci
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Public EksekusiTampilanData As Boolean
    Sub TampilkanData()

        If Not EksekusiTampilanData Then Return

        KetersediaanMenuHalaman(pnl_Halaman, False)

        JumlahAsset = 0

        Select Case JenisTampilan
            Case JenisTampilan_GLOBAL_Rinci
                JudulForm = "Daftar Penyusutan Asset Tetap [Global]"
                TampilkanData_Global_Rinci()
            Case JenisTampilan_GLOBAL_Rekap
                JudulForm = "Daftar Penyusutan Asset Tetap [Global]"
                TampilkanData_Global_Rekap()
            Case JenisTampilan_DETAIL_Rinci
                JudulForm = "Daftar Penyusutan Asset Tetap - Tahun " & TahunLaporan
                TampilkanData_Detail_Rinci()
            Case JenisTampilan_DETAIL_Rekap
                JudulForm = "Daftar Penyusutan Asset Tetap - Tahun " & TahunLaporan
                TampilkanData_Detail_Rekap()
        End Select

        BersihkanSeleksi()

    End Sub


    Sub TampilkanDataRinci()

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Data Tabel : 
        Dim TambahBaris As Boolean
        Dim NomorUrut = 0
        Dim IdAsset
        Dim KodeAsset
        Dim NomorPembelian
        Dim NamaAktiva
        Dim KodeAkun_Asset
        Dim NamaAkun_Asset
        Dim KodeAkun_BiayaPenyusutan
        Dim NamaAkun_BiayaPenyusutan
        Dim MasaManfaat
        Dim KelompokHarta
        Dim TarifPenyusutan
        Dim Divisi
        Dim KodeDivisi
        Dim TanggalPerolehan
        Dim BulanPerolehan
        Dim TahunPerolehan
        Dim BulanDimulaiPenyusutan
        Dim BulanTerakhirPenyusutan
        Dim TahunDimulaiPenyusutan
        Dim HargaPerolehan As Int64
        Dim HargaPerolehanAwal As Int64
        Dim HargaPerolehanAkhir As Int64
        Dim PenyusutanPerbulan As Long 'Jangan dirubah menjadi integer. Sudah benar long.
        Dim PenyusutanPertahun As Long 'Idem
        Dim PenyusutanBulanTerakhir As Long 'Idem
        Dim AkumulasiPenyusutanSampaiTahunSebelumnya As Int64
        Dim NilaiSisaBukuAwal As Int64
        Dim PenambahanPengurangan As Int64
        Dim SaldoAwalSiapUntukDisusutkan As Int64
        Dim Januari, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, Nopember, Desember
        Dim AkumulasiPenyusutanTahunLaporan As Int64
        Dim AkumulasiPenyusutanSampaiDengan As Int64
        Dim NilaiSisaBukuAkhirTahun As Int64
        Dim PenyusutanTahunLaporanYangSudahDijurnal As Int64
        Dim NilaiSisaBuku_BerdasarkanJurnalTerakhir As Int64 'Untuk kepentingan saat Asset Dijual/Disposal.
        Dim HargaJual As Int64
        Dim TanggalInvoice
        Dim NomorInvoice
        Dim Keterangan
        Dim KodeClosing
        AssetSudahClosing = False
        AssetSudahClosingDiTahunIni = False
        Dim TanggalClosing
        Dim BulanClosing
        Dim TahunClosing

        Dim JumlahBulanPenyusutanSampaiTahunSebelumnya As Integer
        Dim SisaBulanPenyusutan As Long '(Sudah benar pakai As Long. Jangan Dirubah ke Integer/Int. Supaya angkanya bulat, tidak berkoma)
        Dim JumlahMaksimalBulanPenyusutan As Int64
        Dim ToleransiSelisihSaldo = 1200

        Dim Jml_Januari, Jml_Februari, Jml_Maret, Jml_April, Jml_Mei, Jml_Juni, Jml_Juli, Jml_Agustus, Jml_September, Jml_Oktober, Jml_Nopember, Jml_Desember
        Dim Jml_NilaiSisaBukuAkhirTahun As Int64
        Dim Jml_AkumulasiPenyusutanSampaiDengan As Int64
        Dim Jml_HargaPerolehan As Int64
        Dim Jml_HargaPerolehanAwal As Int64
        Dim Jml_PenambahanPengurangan As Int64
        Dim Jml_HargaPerolehanAkhir As Int64
        Dim Jml_AkumulasiPenyusutanSampaiTahunSebelumnya As Int64
        Dim Jml_NilaiSisaBukuAwal As Int64
        Dim Jml_AkumulasiSatutahun As Int64
        Dim Jml_SaldoAwalSiapUntukDisusutkan As Int64
        Dim SelisihSaldoAkhir As Integer = 0

        Jml_Januari = 0
        Jml_Februari = 0
        Jml_Maret = 0
        Jml_April = 0
        Jml_Mei = 0
        Jml_Juni = 0
        Jml_Juli = 0
        Jml_Agustus = 0
        Jml_September = 0
        Jml_Oktober = 0
        Jml_Nopember = 0
        Jml_Desember = 0

        Jml_SaldoAwalSiapUntukDisusutkan = 0
        Jml_PenambahanPengurangan = 0
        Jml_NilaiSisaBukuAkhirTahun = 0
        Jml_AkumulasiPenyusutanSampaiDengan = 0
        Jml_HargaPerolehan = 0
        Jml_AkumulasiPenyusutanSampaiTahunSebelumnya = 0
        Jml_NilaiSisaBukuAwal = 0
        Jml_AkumulasiSatutahun = 0

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_DataAsset ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader

        Do While dr.Read

            IdAsset = dr.Item("Nomor_ID")
            KodeAsset = dr.Item("Kode_Asset")
            NomorPembelian = dr.Item("Nomor_Pembelian")
            NamaAktiva = dr.Item("Nama_Aktiva")
            KodeAkun_Asset = dr.Item("COA_Asset")
            NamaAkun_Asset = AmbilValue_NamaAkun(KodeAkun_Asset)
            KodeAkun_BiayaPenyusutan = dr.Item("COA_Biaya_Penyusutan")
            NamaAkun_BiayaPenyusutan = AmbilValue_NamaAkun(KodeAkun_BiayaPenyusutan)
            MasaManfaat = dr.Item("Masa_Manfaat") & " Tahun"

            Select Case dr.Item("Kelompok_Harta")
                Case 1
                    KelompokHarta = KelompokHarta_1
                    JumlahMaksimalBulanPenyusutan = 4 * 12
                Case 2
                    KelompokHarta = KelompokHarta_2
                    JumlahMaksimalBulanPenyusutan = 8 * 12
                Case 3
                    KelompokHarta = KelompokHarta_3
                    JumlahMaksimalBulanPenyusutan = 16 * 12
                Case 4
                    KelompokHarta = KelompokHarta_4
                    JumlahMaksimalBulanPenyusutan = 20 * 12
                Case 5
                    KelompokHarta = KelompokHarta_BangunanPermanen
                    JumlahMaksimalBulanPenyusutan = 20 * 12
                Case 6
                    KelompokHarta = KelompokHarta_BangunanTidakPermanen
                    JumlahMaksimalBulanPenyusutan = 10 * 12
                Case 7
                    KelompokHarta = KelompokHarta_Tanah
                    JumlahMaksimalBulanPenyusutan = 999999999999999999
                    MasaManfaat = StripKosong
                Case Else
                    KelompokHarta = Kosongan 'Ini untuk mengisi kekosongan value. Jangan dihapus.
            End Select

            If KelompokHarta = KelompokHarta_Tanah Then
                TarifPenyusutan = "0 %"
            Else
                TarifPenyusutan = (100 / AmbilAngka(MasaManfaat)).ToString & " %"
            End If

            Divisi = dr.Item("Divisi")
            KodeDivisi = dr.Item("Kode_Divisi")

            TanggalPerolehan = TanggalFormatTampilan(dr.Item("Tanggal_Perolehan"))
            BulanPerolehan = AmbilAngka(Format(dr.Item("Tanggal_Perolehan"), "MM"))
            TahunPerolehan = AmbilAngka(Format(dr.Item("Tanggal_Perolehan"), "yyyy"))

            If FilterAkun = KodeAkun_Asset Or FilterAkun = Pilihan_Semua Then
                TambahBaris = True
                If FilterKelompokHarta = KelompokHarta Or FilterKelompokHarta = Pilihan_Semua Then
                    TambahBaris = True
                    If FilterTahunPerolehan = TahunPerolehan.ToString Or FilterTahunPerolehan = Pilihan_Semua Then
                        TambahBaris = True
                    Else
                        TambahBaris = False
                    End If
                Else
                    TambahBaris = False
                End If
            Else
                TambahBaris = False
            End If

            If TambahBaris = True Then

                BulanDimulaiPenyusutan = BulanPerolehan
                TahunDimulaiPenyusutan = TahunPerolehan

                JumlahBulanPenyusutanSampaiTahunSebelumnya = 12 - BulanDimulaiPenyusutan + 1 + (12 * (TahunLaporanSebelumnya - TahunDimulaiPenyusutan))
                If JumlahBulanPenyusutanSampaiTahunSebelumnya < 0 Then JumlahBulanPenyusutanSampaiTahunSebelumnya = 0
                If JumlahBulanPenyusutanSampaiTahunSebelumnya > JumlahMaksimalBulanPenyusutan Then JumlahBulanPenyusutanSampaiTahunSebelumnya = JumlahMaksimalBulanPenyusutan

                HargaPerolehan = dr.Item("Harga_Perolehan")

                If KelompokHarta = KelompokHarta_Tanah Then
                    PenyusutanPerbulan = 0
                    PenyusutanPertahun = 0
                Else
                    PenyusutanPerbulan = HargaPerolehan / (AmbilAngka(MasaManfaat)) / 12
                    PenyusutanPertahun = HargaPerolehan / (AmbilAngka(MasaManfaat))
                End If

                AkumulasiPenyusutanSampaiTahunSebelumnya = PenyusutanPerbulan * JumlahBulanPenyusutanSampaiTahunSebelumnya
                SaldoAwalSiapUntukDisusutkan = HargaPerolehan - AkumulasiPenyusutanSampaiTahunSebelumnya
                NilaiSisaBukuAwal = SaldoAwalSiapUntukDisusutkan
                If SaldoAwalSiapUntukDisusutkan < ToleransiSelisihSaldo Then
                    NilaiSisaBukuAwal = 0
                    SaldoAwalSiapUntukDisusutkan = 0
                End If

                'PesanUntukProgrammer("Saldo Awal Siap Untuk Disusutkan : " & SaldoAwalSiapUntukDisusutkan & Enter2Baris &
                '                     "Penyusutan Perbulan : " & PenyusutanPerbulan)

                If KelompokHarta = KelompokHarta_Tanah Then
                    SisaBulanPenyusutan = 999999999999999999 '(Ilok segini masih kurang???)
                Else
                    SisaBulanPenyusutan = SaldoAwalSiapUntukDisusutkan / PenyusutanPerbulan
                End If

                Januari = PenyusutanPerbulan
                Februari = PenyusutanPerbulan
                Maret = PenyusutanPerbulan
                April = PenyusutanPerbulan
                Mei = PenyusutanPerbulan
                Juni = PenyusutanPerbulan
                Juli = PenyusutanPerbulan
                Agustus = PenyusutanPerbulan
                September = PenyusutanPerbulan
                Oktober = PenyusutanPerbulan
                Nopember = PenyusutanPerbulan
                Desember = PenyusutanPerbulan

                'Jika (Tahun Laporan = Tahun Dimulai Penyusutan) dan (Dimulai pada bulan tertentu), maka bulan sebelumnya harus dibikin nol (0).
                If TahunLaporan = TahunDimulaiPenyusutan Then
                    'Sudah benar pakai If pada masinng-masing bulan. Jangan pakai 'Select Case'
                    NilaiSisaBukuAwal = 0
                    If BulanDimulaiPenyusutan > 1 Then Januari = 0
                    If BulanDimulaiPenyusutan > 2 Then Februari = 0
                    If BulanDimulaiPenyusutan > 3 Then Maret = 0
                    If BulanDimulaiPenyusutan > 4 Then April = 0
                    If BulanDimulaiPenyusutan > 5 Then Mei = 0
                    If BulanDimulaiPenyusutan > 6 Then Juni = 0
                    If BulanDimulaiPenyusutan > 7 Then Juli = 0
                    If BulanDimulaiPenyusutan > 8 Then Agustus = 0
                    If BulanDimulaiPenyusutan > 9 Then September = 0
                    If BulanDimulaiPenyusutan > 10 Then Oktober = 0
                    If BulanDimulaiPenyusutan > 11 Then Nopember = 0
                    'Kenapa tidak ada logika untuk me-nol-kan (0) Bulan Desember..?
                    'Ga usah bingung..!
                    'Kalau Desember di-nol-kan (0), berarti dari Januari sampai Desember nol (0) semua.
                    'Lah, itu sudah tidak termasuk dalam logika ini. Dalam Arti Tahun Laporan Asset tidak sama dengan Tahun Perolehan.
                End If

                'Jika (Tahun Laporan adalah sebelum Tahun Dimulai Penyusutan), maka semua bulan harus dibikin nol (0).
                If TahunLaporan < TahunDimulaiPenyusutan Then
                    NilaiSisaBukuAwal = 0
                    SaldoAwalSiapUntukDisusutkan = 0
                    Januari = 0
                    Februari = 0
                    Maret = 0
                    April = 0
                    Mei = 0
                    Juni = 0
                    Juli = 0
                    Agustus = 0
                    September = 0
                    Oktober = 0
                    Nopember = 0
                    Desember = 0
                End If

                'Ini Logika untuk Tahun Laporan = Tahun Terakhir Penyusutan
                If SisaBulanPenyusutan <= 12 Then
                    BulanTerakhirPenyusutan = SisaBulanPenyusutan
                    SelisihSaldoAkhir = HargaPerolehan - AkumulasiPenyusutanSampaiTahunSebelumnya - (PenyusutanPerbulan * SisaBulanPenyusutan)
                    If (SelisihSaldoAkhir > ToleransiSelisihSaldo) Or (SelisihSaldoAkhir < (-ToleransiSelisihSaldo)) Then SelisihSaldoAkhir = 0
                    PenyusutanBulanTerakhir = PenyusutanPerbulan + SelisihSaldoAkhir
                    If SelisihSaldoAkhir < ToleransiSelisihSaldo Then
                        If BulanTerakhirPenyusutan = 1 Then Januari = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 2 Then Februari = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 3 Then Maret = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 4 Then April = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 5 Then Mei = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 6 Then Juni = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 7 Then Juli = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 8 Then Agustus = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 9 Then September = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 10 Then Oktober = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 11 Then Nopember = PenyusutanBulanTerakhir
                        If BulanTerakhirPenyusutan = 12 Then Desember = PenyusutanBulanTerakhir
                        '----------------------------------------------------------------------
                        If SaldoAwalSiapUntukDisusutkan = 0 Then AkumulasiPenyusutanSampaiTahunSebelumnya += SelisihSaldoAkhir
                    End If
                    'Jika sudah habis Masa Penyusutan, maka bulan setelahnya harus dibikin nol (0).
                    If BulanTerakhirPenyusutan < 1 Then Januari = 0
                    If BulanTerakhirPenyusutan < 2 Then Februari = 0
                    If BulanTerakhirPenyusutan < 3 Then Maret = 0
                    If BulanTerakhirPenyusutan < 4 Then April = 0
                    If BulanTerakhirPenyusutan < 5 Then Mei = 0
                    If BulanTerakhirPenyusutan < 6 Then Juni = 0
                    If BulanTerakhirPenyusutan < 7 Then Juli = 0
                    If BulanTerakhirPenyusutan < 8 Then Agustus = 0
                    If BulanTerakhirPenyusutan < 9 Then September = 0
                    If BulanTerakhirPenyusutan < 10 Then Oktober = 0
                    If BulanTerakhirPenyusutan < 11 Then Nopember = 0
                    If BulanTerakhirPenyusutan < 12 Then Desember = 0
                    '------------------------------------------------
                End If

                'Ini logika untuk meng-nol-kan angka penyusutan pada bulan ini dan seterusnya, karena belum waktunya untuk penjurnalan.
                If TahunLaporan = TahunIni Then
                    If AmbilAngka(BulanIni) = 1 Then Januari = 0
                    If AmbilAngka(BulanIni) <= 2 Then Februari = 0
                    If AmbilAngka(BulanIni) <= 3 Then Maret = 0
                    If AmbilAngka(BulanIni) <= 4 Then April = 0
                    If AmbilAngka(BulanIni) <= 5 Then Mei = 0
                    If AmbilAngka(BulanIni) <= 6 Then Juni = 0
                    If AmbilAngka(BulanIni) <= 7 Then Juli = 0
                    If AmbilAngka(BulanIni) <= 8 Then Agustus = 0
                    If AmbilAngka(BulanIni) <= 9 Then September = 0
                    If AmbilAngka(BulanIni) <= 10 Then Oktober = 0
                    If AmbilAngka(BulanIni) <= 11 Then Nopember = 0
                    If AmbilAngka(BulanIni) <= 12 Then Desember = 0
                    'Sudah benar pakai If pada masinng-masing bulan. Jangan pakai 'Select Case'
                End If

                If TahunLaporan = TahunPerolehan Then
                    HargaPerolehanAwal = 0
                    PenambahanPengurangan = HargaPerolehan
                Else
                    HargaPerolehanAwal = HargaPerolehan
                    PenambahanPengurangan = 0
                End If

                TanggalClosing = TanggalFormatTampilan(dr.Item("Tanggal_Closing"))
                If TanggalClosing <> TanggalKosong Then
                    AssetSudahClosing = True
                    BulanClosing = AmbilAngka(Format(dr.Item("Tanggal_Closing"), "MM"))
                    TahunClosing = AmbilAngka(Format(dr.Item("Tanggal_Closing"), "yyyy"))
                    HargaJual = dr.Item("Harga_Jual")
                    If TahunLaporan >= TahunClosing Then PenambahanPengurangan -= HargaPerolehan
                Else
                    AssetSudahClosing = False
                    TahunClosing = 0
                    BulanClosing = 0
                End If

                HargaPerolehanAkhir = HargaPerolehanAwal + PenambahanPengurangan

                If AssetSudahClosing = True Then
                    If TahunClosing <= TahunLaporan Then AssetSudahClosingDiTahunIni = True
                    If TahunClosing = TahunLaporan Then
                        If BulanClosing = 1 Then Januari = 0
                        If BulanClosing <= 2 Then Februari = 0
                        If BulanClosing <= 3 Then Maret = 0
                        If BulanClosing <= 4 Then April = 0
                        If BulanClosing <= 5 Then Mei = 0
                        If BulanClosing <= 6 Then Juni = 0
                        If BulanClosing <= 7 Then Juli = 0
                        If BulanClosing <= 8 Then Agustus = 0
                        If BulanClosing <= 9 Then September = 0
                        If BulanClosing <= 10 Then Oktober = 0
                        If BulanClosing <= 11 Then Nopember = 0
                        If BulanClosing <= 12 Then Desember = 0
                    End If
                    If TahunClosing < TahunLaporan Then
                        Januari = 0
                        Februari = 0
                        Maret = 0
                        April = 0
                        Mei = 0
                        Juni = 0
                        Juli = 0
                        Agustus = 0
                        September = 0
                        Oktober = 0
                        Nopember = 0
                        Desember = 0
                    End If
                End If

                Jml_Januari += Januari
                Jml_Februari += Februari
                Jml_Maret += Maret
                Jml_April += April
                Jml_Mei += Mei
                Jml_Juni += Juni
                Jml_Juli += Juli
                Jml_Agustus += Agustus
                Jml_September += September
                Jml_Oktober += Oktober
                Jml_Nopember += Nopember
                Jml_Desember += Desember

                AkumulasiPenyusutanTahunLaporan = Januari + Februari + Maret + April + Mei + Juni + Juli + Agustus + September + Oktober + Nopember + Desember
                AkumulasiPenyusutanSampaiDengan = AkumulasiPenyusutanSampaiTahunSebelumnya + AkumulasiPenyusutanTahunLaporan
                If AssetSudahClosingDiTahunIni = True Then
                    NilaiSisaBukuAkhirTahun = 0
                Else
                    NilaiSisaBukuAkhirTahun = HargaPerolehan - AkumulasiPenyusutanSampaiDengan
                End If
                If NilaiSisaBukuAkhirTahun < ToleransiSelisihSaldo Then
                    NilaiSisaBukuAkhirTahun = 0
                    AkumulasiPenyusutanSampaiDengan = HargaPerolehan
                End If
                If TahunLaporan < TahunDimulaiPenyusutan Then NilaiSisaBukuAkhirTahun = 0

                TanggalInvoice = AmbilValue_TanggalInvoiceBerdasarkanNomorPembelian(NomorPembelian)
                NomorInvoice = AmbilValue_NomorInvoiceBerdasarkanNomorPembelian(NomorPembelian)
                Keterangan = PenghapusEnter(dr.Item("Keterangan"))
                KodeClosing = dr.Item("Kode_Closing")

                If TahunPerolehan <= TahunLaporan Then

                    NomorUrut += 1

                    Jml_HargaPerolehan += HargaPerolehan
                    Jml_HargaPerolehanAwal += HargaPerolehanAwal
                    Jml_HargaPerolehanAkhir += HargaPerolehanAkhir
                    Jml_AkumulasiPenyusutanSampaiTahunSebelumnya += AkumulasiPenyusutanSampaiTahunSebelumnya
                    Jml_NilaiSisaBukuAwal += NilaiSisaBukuAwal
                    Jml_PenambahanPengurangan += PenambahanPengurangan
                    Jml_SaldoAwalSiapUntukDisusutkan += SaldoAwalSiapUntukDisusutkan
                    Jml_AkumulasiSatutahun += AkumulasiPenyusutanTahunLaporan
                    Jml_AkumulasiPenyusutanSampaiDengan += AkumulasiPenyusutanSampaiDengan
                    Jml_NilaiSisaBukuAkhirTahun += NilaiSisaBukuAkhirTahun

                    'Cek Data Jurnal (Untuk keperluan pengambilan value 'NilaiSisaBuku_BerdasarkanJurnalTerakhir') :
                    AksesDatabase_Transaksi(Buka)
                    cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                                   " WHERE  Valid    <> '" & _X_ & "' " &
                                                   " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                                   " AND Bundelan LIKE '%" & KodeAsset & "%' " &
                                                   " AND COA = '" & KodeAkun_BiayaPenyusutan & "' ", KoneksiDatabaseTransaksi)
                    drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
                    PenyusutanTahunLaporanYangSudahDijurnal = 0
                    Do While drCEKJURNAL.Read
                        Dim TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
                        Dim BulanJurnalPenyusutan = AmbilAngka(Format(TanggalJurnalPenyusutan, "MM"))
                        If AmbilAngka(BulanJurnalPenyusutan) = 1 Then PenyusutanTahunLaporanYangSudahDijurnal += Januari
                        If AmbilAngka(BulanJurnalPenyusutan) = 2 Then PenyusutanTahunLaporanYangSudahDijurnal += Februari
                        If AmbilAngka(BulanJurnalPenyusutan) = 3 Then PenyusutanTahunLaporanYangSudahDijurnal += Maret
                        If AmbilAngka(BulanJurnalPenyusutan) = 4 Then PenyusutanTahunLaporanYangSudahDijurnal += April
                        If AmbilAngka(BulanJurnalPenyusutan) = 5 Then PenyusutanTahunLaporanYangSudahDijurnal += Mei
                        If AmbilAngka(BulanJurnalPenyusutan) = 6 Then PenyusutanTahunLaporanYangSudahDijurnal += Juni
                        If AmbilAngka(BulanJurnalPenyusutan) = 7 Then PenyusutanTahunLaporanYangSudahDijurnal += Juli
                        If AmbilAngka(BulanJurnalPenyusutan) = 8 Then PenyusutanTahunLaporanYangSudahDijurnal += Agustus
                        If AmbilAngka(BulanJurnalPenyusutan) = 9 Then PenyusutanTahunLaporanYangSudahDijurnal += September
                        If AmbilAngka(BulanJurnalPenyusutan) = 10 Then PenyusutanTahunLaporanYangSudahDijurnal += Oktober
                        If AmbilAngka(BulanJurnalPenyusutan) = 11 Then PenyusutanTahunLaporanYangSudahDijurnal += Nopember
                        If AmbilAngka(BulanJurnalPenyusutan) = 12 Then PenyusutanTahunLaporanYangSudahDijurnal += Desember
                    Loop
                    AksesDatabase_Transaksi(Tutup)
                    NilaiSisaBuku_BerdasarkanJurnalTerakhir = SaldoAwalSiapUntukDisusutkan - PenyusutanTahunLaporanYangSudahDijurnal

                    datatabelUtama.Rows.Add(NomorUrut, IdAsset, KodeAsset, NomorPembelian, NamaAktiva,
                                            KodeAkun_Asset, NamaAkun_Asset, KodeAkun_BiayaPenyusutan, NamaAkun_BiayaPenyusutan,
                                            MasaManfaat, KelompokHarta, TarifPenyusutan, Divisi, KodeDivisi,
                                            TanggalPerolehan, HargaPerolehan, HargaPerolehanAwal, PenambahanPengurangan, HargaPerolehanAkhir,
                                            PenyusutanPerbulan, PenyusutanPertahun, JumlahBulanPenyusutanSampaiTahunSebelumnya,
                                            AkumulasiPenyusutanSampaiTahunSebelumnya, NilaiSisaBukuAwal, SaldoAwalSiapUntukDisusutkan,
                                            AkumulasiPenyusutanTahunLaporan, AkumulasiPenyusutanSampaiDengan, NilaiSisaBukuAkhirTahun,
                                            Januari, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, Nopember, Desember,
                                            PenyusutanTahunLaporanYangSudahDijurnal, NilaiSisaBuku_BerdasarkanJurnalTerakhir, TanggalInvoice, NomorInvoice, Keterangan, KodeClosing)

                    Dim IndexBaris = NomorUrut - 1

                End If

            End If

            Terabas()

        Loop 'Ujung Loop Tampilan

        AksesDatabase_General(Tutup)

        JumlahBaris = datatabelUtama.Rows.Count

        'Dim rowIndex As Integer = 0
        'For Each item In datagridUtama.Items
        '    Dim dgRow As DataGridRow = TryCast(datagridUtama.ItemContainerGenerator.ContainerFromItem(item), DataGridRow)
        '    If dgRow IsNot Nothing Then
        '        If rowIndex = 2 Then ' Index basis 0, 2 adalah baris ketiga
        '            dgRow.Foreground = New SolidColorBrush(Colors.Red)
        '        End If
        '        rowIndex += 1
        '    End If
        'Next

        If JumlahBaris > 0 Then
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, teks_Jumlah, Kosongan, teks_Jumlah,
                                  Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                  Kosongan, Jml_HargaPerolehan, Jml_HargaPerolehanAwal, Jml_PenambahanPengurangan, Jml_HargaPerolehanAkhir,
                                  0, 0, 0,
                                  Jml_AkumulasiPenyusutanSampaiTahunSebelumnya, Jml_NilaiSisaBukuAwal, Jml_SaldoAwalSiapUntukDisusutkan,
                                  Jml_AkumulasiSatutahun, Jml_AkumulasiPenyusutanSampaiDengan, Jml_NilaiSisaBukuAkhirTahun,
                                  Jml_Januari, Jml_Februari, Jml_Maret, Jml_April, Jml_Mei, Jml_Juni,
                                  Jml_Juli, Jml_Agustus, Jml_September, Jml_Oktober, Jml_Nopember, Jml_Desember)
        End If

        JumlahAsset = NomorUrut
        VisibilitasJumlahAsset(True)

    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        KetersediaanTombolUpdate(False)
        KetersediaanTombolLihatJurnal(False)
        KetersediaanTombolLihatInvoice(False)
        KetersediaanTombolPosting(False)
        KetersediaanTombolJualAsset(False)
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub



    Sub VisibilitasTombolImport(Visibilitas As Boolean)
        btn_Import.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                btn_Import.Visibility = Visibility.Visible
            End If
        Else
            btn_Import.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTombolCRUD(Visibilitas As Boolean)
        pnl_CRUD.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                pnl_CRUD.Visibility = Visibility.Visible
            End If
        Else
            pnl_CRUD.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTombolKodeDivisi(Visibilitas As Boolean)
        brd_KodeDivisi.Visibility = Visibility.Collapsed
        btn_KodeDivisi.Visibility = Visibility.Collapsed
        If Visibilitas Then
            brd_KodeDivisi.Visibility = Visibility.Visible
            btn_KodeDivisi.Visibility = Visibility.Visible
        Else
            brd_KodeDivisi.Visibility = Visibility.Collapsed
            btn_KodeDivisi.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        pnl_Jurnal.Visibility = Visibility.Collapsed
        If Visibilitas Then
            pnl_Jurnal.Visibility = Visibility.Visible
        Else
            pnl_Jurnal.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTombolJualBeliAsset(Visibilitas As Boolean)
        pnl_JualBeliAsset.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                pnl_JualBeliAsset.Visibility = Visibility.Visible
            End If
        Else
            pnl_JualBeliAsset.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasFilterData(Visibilitas As Boolean)
        pnl_FilterData.Visibility = Visibility.Collapsed
        If Visibilitas Then
            pnl_FilterData.Visibility = Visibility.Visible
        Else
            pnl_FilterData.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasJumlahAsset(Visibilitas As Boolean)
        pnl_JumlahAsset.Visibility = Visibility.Collapsed
        If Visibilitas Then
            txt_JumlahAsset.Text = JumlahAsset
            pnl_JumlahAsset.Visibility = Visibility.Visible
        Else
            pnl_JumlahAsset.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KetersediaanTombolUpdate(Tersedia As Boolean)
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        If Tersedia Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
        End If
    End Sub


    Sub KetersediaanTombolPosting(Tersedia As Boolean)
        btn_Posting.IsEnabled = False
        If Tersedia Then
            btn_Posting.IsEnabled = True
        Else
            btn_Posting.IsEnabled = False
        End If
    End Sub


    Sub KetersediaanTombolLihatJurnal(Tersedia As Boolean)
        btn_LihatJurnal.IsEnabled = False
        If Tersedia Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If
    End Sub


    Sub KetersediaanTombolLihatInvoice(Tersedia As Boolean)
        btn_LihatInvoice.IsEnabled = False
        If Tersedia Then
            btn_LihatInvoice.IsEnabled = True
        Else
            btn_LihatInvoice.IsEnabled = False
        End If
    End Sub


    Sub KetersediaanTombolJualAsset(Tersedia As Boolean)
        btn_JualAsset.IsEnabled = False
        btn_DisposalAsset.IsEnabled = False
        If Tersedia Then
            btn_JualAsset.IsEnabled = True
            btn_DisposalAsset.IsEnabled = True
        Else
            btn_DisposalAsset.IsEnabled = False
            btn_JualAsset.IsEnabled = False
        End If
    End Sub




    Sub KontenComboTahunLaporan()
        EksekusiTampilanData = False
        cmb_TahunLaporan.Items.Clear()
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 10)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 9)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 8)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 7)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 6)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 5)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 4)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 3)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 2)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif - 1)
        cmb_TahunLaporan.Items.Add(TahunBukuAktif)
        cmb_TahunLaporan.SelectedValue = TahunBukuAktif
    End Sub



    Sub KontenComboFilterCOA()
        cmb_FilterCOA.Items.Clear()
        cmb_FilterCOA.Items.Add(Pilihan_Semua)
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterListCOA_AssetTetap, KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Dim KodeAkun
        Dim NamaAkun
        Do While dr.Read
            KodeAkun = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            cmb_FilterCOA.Items.Add(KodeAkun & " : " & NamaAkun)
        Loop
        AksesDatabase_General(Tutup)
        cmb_FilterCOA.Text = Pilihan_Semua
    End Sub

    Sub KontenComboFilterKelompokHarta()
        cmb_FilterKelompokHarta.Items.Clear()
        cmb_FilterKelompokHarta.Items.Add(Pilihan_Semua)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_1)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_2)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_3)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_4)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_Tanah)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_BangunanPermanen)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_BangunanTidakPermanen)
        cmb_FilterKelompokHarta.Text = Pilihan_Semua
    End Sub

    Sub KontenComboFilterTahunPerolehan()
        cmb_FilterTahunPerolehan.Items.Clear()
        cmb_FilterTahunPerolehan.Items.Add(Pilihan_Semua)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 10)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 9)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 8)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 7)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 6)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 5)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 4)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 3)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 2)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif - 1)
        cmb_FilterTahunPerolehan.Items.Add(TahunBukuAktif)
        cmb_FilterTahunPerolehan.SelectedValue = Pilihan_Semua
    End Sub


    Sub TampilkanData_Global_Rinci()

        lbl_JudulForm.Text = JudulForm
        btn_DetailGlobal.Content = teks_DetailPerbulan
        btn_Rekap.Content = teks_RekapGlobal

        'Visibilitas Elemen :
        VisibilitasTombolKodeDivisi(True)
        VisibilitasTombolJualBeliAsset(True)
        VisibilitasTombolCRUD(True)
        'VisibilitasTombolJurnal(False)
        VisibilitasFilterData(True)

        'Sembunyikan Semua Kolom :
        SembunyikanSemuaKolomTabel()

        'Kolom-kolom yang Ditampilkan :
        If Not ProsesPostingJurnal Then
            Nomor_Urut.Visibility = Visibility.Visible
            Nama_Aktiva.Visibility = Visibility.Visible
            COA_Asset.Visibility = Visibility.Visible
            Nama_Akun_Asset.Visibility = Visibility.Visible
            'Masa_Manfaat.Visibility = Visibility.Visible
            'Kelompok_Harta.Visibility = Visibility.Visible
            Tarif_Penyusutan.Visibility = Visibility.Visible
            'Divisi_.Visibility = Visibility.Visible
            'Kode_Divisi.Visibility = Visibility.Visible
            Tanggal_Perolehan.Visibility = Visibility.Visible
            Harga_Perolehan_Awal.Visibility = Visibility.Visible
            Penambahan_Pengurangan.Visibility = Visibility.Visible
            Harga_Perolehan_Akhir.Visibility = Visibility.Visible
            Nilai_Sisa_Buku_Awal.Visibility = Visibility.Visible
            Saldo_Awal_Siap_Untuk_Disusutkan.Visibility = Visibility.Visible
            Akumulasi_Tahun_Laporan.Visibility = Visibility.Visible
            Akumulasi_Penyusutan_Sampai_Dengan.Visibility = Visibility.Visible
            Nilai_Sisa_Buku_Akhir_Tahun.Visibility = Visibility.Visible
            Tanggal_Invoice.Visibility = Visibility.Visible
            Nomor_Invoice.Visibility = Visibility.Visible
            Keterangan_.Visibility = Visibility.Visible
        End If

        TampilkanDataRinci()

    End Sub

    Sub TampilkanData_Global_Rekap()

        lbl_JudulForm.Text = JudulForm
        btn_DetailGlobal.Content = teks_DetailPerbulan
        btn_Rekap.Content = teks_Kembali

        'Visibilitas Elemen :
        VisibilitasTombolKodeDivisi(True)
        VisibilitasTombolJualBeliAsset(False)
        VisibilitasTombolCRUD(False)
        'VisibilitasTombolJurnal(False)
        VisibilitasFilterData(False)
        VisibilitasJumlahAsset(False)
        'Sembunyikan Semua Kolom :
        SembunyikanSemuaKolomTabel()

        'Kolom-kolom yang Ditampilkan :
        Nomor_Urut.Visibility = Visibility.Visible
        Nama_Akun_Asset.Visibility = Visibility.Visible
        Harga_Perolehan.Visibility = Visibility.Visible
        'Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya.Visibility = Visibility.Visible
        Nilai_Sisa_Buku_Awal.Visibility = Visibility.Visible
        Akumulasi_Tahun_Laporan.Visibility = Visibility.Visible
        Akumulasi_Penyusutan_Sampai_Dengan.Visibility = Visibility.Visible
        Nilai_Sisa_Buku_Akhir_Tahun.Visibility = Visibility.Visible

        'Bikin Tabel Temporary :
        DataTabelSementara.Rows.Clear()
        DataTabelSementara.Columns.Clear()
        DataTabelSementara.Columns.Add("Nomor_Urut")
        DataTabelSementara.Columns.Add("Nama_Akun_Asset")
        DataTabelSementara.Columns.Add("Harga_Perolehan")
        DataTabelSementara.Columns.Add("Akumulasi_Penyusutan_Awal")
        DataTabelSementara.Columns.Add("Nilai_Sisa_Buku_Awal")
        DataTabelSementara.Columns.Add("Penyusutan_Tahun_Laporan")
        DataTabelSementara.Columns.Add("Akumulasi_Penyusutan_Akhir")
        DataTabelSementara.Columns.Add("Nilai_Sisa_Buku_Akhir")

        Dim NomorUrut = 0
        Dim NamaAkun_Asset = Kosongan
        Dim HargaPerolehan As Int64
        Dim AkumulasiPenyusutanAwal As Int64
        Dim NilaiSisaBukuAwal As Int64
        Dim PenyusutanTahunLaporan As Int64
        Dim AkumulasiPenyusutanAkhir As Int64
        Dim NilaiSisaBukuAkhir As Int64
        Dim Rekap_HargaPerolehan As Int64
        Dim Rekap_AkumulasiPenyusutanAwal As Int64
        Dim Rekap_NilaiSisaBukuAwal As Int64
        Dim Rekap_PenyusutanTahunLaporan As Int64
        Dim Rekap_AkumulasiPenyusutanAkhir As Int64
        Dim Rekap_NilaiSisaBukuAkhir As Int64
        Dim Total_HargaPerolehan As Int64 = 0
        Dim Total_AkumulasiPenyusutanAwal As Int64 = 0
        Dim Total_NilaiSisaBukuAwal As Int64
        Dim Total_PenyusutanTahunLaporan As Int64 = 0
        Dim Total_AkumulasiPenyusutanAkhir As Int64 = 0
        Dim Total_NilaiSisaBukuAkhir As Int64 = 0
        Dim COAAsset_Terindeks = Kosongan

        Dim SusurAkun = AwalAkunAssetTetap

        Do While SusurAkun <= AkhirAkunAssetTetap

            Rekap_HargaPerolehan = 0
            Rekap_AkumulasiPenyusutanAwal = 0
            Rekap_NilaiSisaBukuAwal = 0
            Rekap_PenyusutanTahunLaporan = 0
            Rekap_AkumulasiPenyusutanAkhir = 0
            Rekap_NilaiSisaBukuAkhir = 0


            For Each row As DataRow In datatabelUtama.Rows

                COAAsset_Terindeks = AmbilValueCellTeksBerpotensiDBNull_Row(row, "COA_Asset")

                If AmbilAngka(COAAsset_Terindeks) = SusurAkun Then
                    NamaAkun_Asset = row("Nama_Akun_Asset")
                    HargaPerolehan = AmbilAngka(row("Harga_Perolehan"))
                    AkumulasiPenyusutanAwal = AmbilAngka(row("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya"))
                    NilaiSisaBukuAwal = AmbilAngka(row("Nilai_Sisa_Buku_Awal"))
                    PenyusutanTahunLaporan = AmbilAngka(row("Akumulasi_Tahun_Laporan"))
                    AkumulasiPenyusutanAkhir = AmbilAngka(row("Akumulasi_Penyusutan_Sampai_Dengan"))
                    NilaiSisaBukuAkhir = AmbilAngka(row("Nilai_Sisa_Buku_Akhir_Tahun"))
                    Rekap_HargaPerolehan += HargaPerolehan
                    Rekap_AkumulasiPenyusutanAwal += AkumulasiPenyusutanAwal
                    Rekap_NilaiSisaBukuAwal += NilaiSisaBukuAwal
                    Rekap_PenyusutanTahunLaporan += PenyusutanTahunLaporan
                    Rekap_AkumulasiPenyusutanAkhir += AkumulasiPenyusutanAkhir
                    Rekap_NilaiSisaBukuAkhir += NilaiSisaBukuAkhir
                    Total_HargaPerolehan += HargaPerolehan
                    Total_AkumulasiPenyusutanAwal += AkumulasiPenyusutanAwal
                    Total_NilaiSisaBukuAwal += NilaiSisaBukuAwal
                    Total_PenyusutanTahunLaporan += PenyusutanTahunLaporan
                    Total_AkumulasiPenyusutanAkhir += AkumulasiPenyusutanAkhir
                    Total_NilaiSisaBukuAkhir += NilaiSisaBukuAkhir
                End If

            Next

            If Rekap_HargaPerolehan > 0 Then
                NomorUrut += 1
                DataTabelSementara.Rows.Add(NomorUrut,
                                            NamaAkun_Asset,
                                            Rekap_HargaPerolehan,
                                            Rekap_AkumulasiPenyusutanAwal,
                                            Rekap_NilaiSisaBukuAwal,
                                            Rekap_PenyusutanTahunLaporan,
                                            Rekap_AkumulasiPenyusutanAkhir,
                                            Rekap_NilaiSisaBukuAkhir)
            End If

            SusurAkun += 1

        Loop

        datatabelUtama.Rows.Clear()

        NomorUrut = 0

        For Each rowBundelAkun As DataRow In DataTabelSementara.Rows
            NomorUrut += 1
            NamaAkun_Asset = rowBundelAkun.Item("Nama_Akun_Asset")
            Rekap_HargaPerolehan = rowBundelAkun.Item("Harga_Perolehan")
            Rekap_AkumulasiPenyusutanAwal = rowBundelAkun.Item("Akumulasi_Penyusutan_Awal")
            Rekap_NilaiSisaBukuAwal = rowBundelAkun.Item("Nilai_Sisa_Buku_Awal")
            Rekap_PenyusutanTahunLaporan = rowBundelAkun.Item("Penyusutan_Tahun_Laporan")
            Rekap_AkumulasiPenyusutanAkhir = rowBundelAkun.Item("Akumulasi_Penyusutan_Akhir")
            Rekap_NilaiSisaBukuAkhir = rowBundelAkun.Item("Nilai_Sisa_Buku_Akhir")
            datatabelUtama.Rows.Add(NomorUrut, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, NamaAkun_Asset, Kosongan, Kosongan,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                    Kosongan, Rekap_HargaPerolehan, 0, 0, 0,
                                    0, 0, Kosongan,
                                    Rekap_AkumulasiPenyusutanAwal, Rekap_NilaiSisaBukuAwal, 0,
                                    Rekap_PenyusutanTahunLaporan, Rekap_AkumulasiPenyusutanAkhir, Rekap_NilaiSisaBukuAkhir)
        Next

        JumlahBaris = datatabelUtama.Rows.Count

        datatabelUtama.Rows.Add()
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, teks_Jumlah, Kosongan, teks_Jumlah,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Total_HargaPerolehan, 0, 0, 0,
                                0, 0, Kosongan,
                                Total_AkumulasiPenyusutanAwal, Total_NilaiSisaBukuAwal, 0,
                                Total_PenyusutanTahunLaporan, Total_AkumulasiPenyusutanAkhir, Total_NilaiSisaBukuAkhir)

    End Sub

    Sub TampilkanData_Detail_Rinci()

        If Not ProsesPostingJurnal Then
            lbl_JudulForm.Text = JudulForm
            btn_DetailGlobal.Content = teks_Global
            btn_Rekap.Content = teks_RekapPenyusutan
            PewarnaanKolomBulan()
            'Visibilitas Elemen :
            VisibilitasTombolKodeDivisi(True)
            VisibilitasTombolJualBeliAsset(False)
            VisibilitasTombolCRUD(False)
            'VisibilitasTombolJurnal(False)
            VisibilitasFilterData(True)
        End If

        'Sembunyikan Semua Kolom :
        SembunyikanSemuaKolomTabel()

        'Kolom-kolom yang Ditampilkan :
        If Not ProsesPostingJurnal Then
            Nomor_Urut.Visibility = Visibility.Visible
            Nama_Aktiva.Visibility = Visibility.Visible
            Nama_Akun_Asset.Visibility = Visibility.Visible
            'Masa_Manfaat.Visibility = Visibility.Visible
            'Kelompok_Harta.Visibility = Visibility.Visible
            Tarif_Penyusutan.Visibility = Visibility.Visible
            'Divisi_.Visibility = Visibility.Visible
            Tanggal_Perolehan.Visibility = Visibility.Visible
            Harga_Perolehan_Awal.Visibility = Visibility.Visible
            Penambahan_Pengurangan.Visibility = Visibility.Visible
            Harga_Perolehan_Akhir.Visibility = Visibility.Visible
            Nilai_Sisa_Buku_Awal.Visibility = Visibility.Visible
            Saldo_Awal_Siap_Untuk_Disusutkan.Visibility = Visibility.Visible
            Akumulasi_Tahun_Laporan.Visibility = Visibility.Visible
            Akumulasi_Penyusutan_Sampai_Dengan.Visibility = Visibility.Visible
            Nilai_Sisa_Buku_Akhir_Tahun.Visibility = Visibility.Visible
            Januari_.Visibility = Visibility.Visible
            Februari_.Visibility = Visibility.Visible
            Maret_.Visibility = Visibility.Visible
            April_.Visibility = Visibility.Visible
            Mei_.Visibility = Visibility.Visible
            Juni_.Visibility = Visibility.Visible
            Juli_.Visibility = Visibility.Visible
            Agustus_.Visibility = Visibility.Visible
            September_.Visibility = Visibility.Visible
            Oktober_.Visibility = Visibility.Visible
            Nopember_.Visibility = Visibility.Visible
            Desember_.Visibility = Visibility.Visible
            Tanggal_Invoice.Visibility = Visibility.Visible
            Nomor_Invoice.Visibility = Visibility.Visible
            Keterangan_.Visibility = Visibility.Visible
        End If

        TampilkanDataRinci()

    End Sub

    Sub TampilkanData_Detail_Rekap()

        KesesuaianJurnal = True

        lbl_JudulForm.Text = JudulForm
        btn_DetailGlobal.Content = teks_Global
        btn_Rekap.Content = teks_Kembali
        PewarnaanKolomBulan()

        'Visibilitas Elemen :
        VisibilitasTombolKodeDivisi(True)
        VisibilitasTombolJualBeliAsset(False)
        VisibilitasTombolCRUD(False)
        'VisibilitasTombolJurnal(True)
        VisibilitasFilterData(False)
        VisibilitasJumlahAsset(False)

        'Sembunyikan Semua Kolom :
        SembunyikanSemuaKolomTabel()

        'Kolom-kolom yang Ditampilkan :
        Nomor_Urut.Visibility = Visibility.Visible
        COA_Biaya_Penyusutan.Visibility = Visibility.Visible
        Nama_Akun_Biaya_Penyusutan.Visibility = Visibility.Visible
        Akumulasi_Tahun_Laporan.Visibility = Visibility.Visible
        Januari_.Visibility = Visibility.Visible
        Februari_.Visibility = Visibility.Visible
        Maret_.Visibility = Visibility.Visible
        April_.Visibility = Visibility.Visible
        Mei_.Visibility = Visibility.Visible
        Juni_.Visibility = Visibility.Visible
        Juli_.Visibility = Visibility.Visible
        Agustus_.Visibility = Visibility.Visible
        September_.Visibility = Visibility.Visible
        Oktober_.Visibility = Visibility.Visible
        Nopember_.Visibility = Visibility.Visible
        Desember_.Visibility = Visibility.Visible

        'bikin tabel temporary :
        DataTabelSementara.Rows.Clear()
        DataTabelSementara.Columns.Clear()
        DataTabelSementara.Columns.Add("Nomor_Urut")
        DataTabelSementara.Columns.Add("COA_Asset")
        DataTabelSementara.Columns.Add("COA_Biaya_Penyusutan")
        DataTabelSementara.Columns.Add("Nama_Akun_Biaya_Penyusutan")
        DataTabelSementara.Columns.Add("Akumulasi_Tahun_Laporan")
        DataTabelSementara.Columns.Add("Januari_")
        DataTabelSementara.Columns.Add("Februari_")
        DataTabelSementara.Columns.Add("Maret_")
        DataTabelSementara.Columns.Add("April_")
        DataTabelSementara.Columns.Add("Mei_")
        DataTabelSementara.Columns.Add("Juni_")
        DataTabelSementara.Columns.Add("Juli_")
        DataTabelSementara.Columns.Add("Agustus_")
        DataTabelSementara.Columns.Add("September_")
        DataTabelSementara.Columns.Add("Oktober_")
        DataTabelSementara.Columns.Add("Nopember_")
        DataTabelSementara.Columns.Add("Desember_")

        Dim NomorUrut = 0
        Dim KodeAkun_Asset = Kosongan
        Dim NamaAkun_BiayaPenyusutan = Kosongan
        Dim KodeAkun_BiayaPenyusutan = Kosongan
        Dim AkumulasiPenyusutanTahunLaporan As Int64
        Dim Januari As Int64
        Dim Februari As Int64
        Dim Maret As Int64
        Dim April As Int64
        Dim Mei As Int64
        Dim Juni As Int64
        Dim Juli As Int64
        Dim Agustus As Int64
        Dim September As Int64
        Dim Oktober As Int64
        Dim Nopember As Int64
        Dim Desember As Int64
        Dim Rekap_AkumulasiPenyusutanTahunLaporan As Int64
        Dim Rekap_Januari As Int64
        Dim Rekap_Februari As Int64
        Dim Rekap_Maret As Int64
        Dim Rekap_April As Int64
        Dim Rekap_Mei As Int64
        Dim Rekap_Juni As Int64
        Dim Rekap_Juli As Int64
        Dim Rekap_Agustus As Int64
        Dim Rekap_September As Int64
        Dim Rekap_Oktober As Int64
        Dim Rekap_Nopember As Int64
        Dim Rekap_Desember As Int64
        Dim Total_Januari As Int64 = 0
        Dim Total_Februari As Int64 = 0
        Dim Total_Maret As Int64 = 0
        Dim Total_April As Int64 = 0
        Dim Total_Mei As Int64 = 0
        Dim Total_Juni As Int64 = 0
        Dim Total_Juli As Int64 = 0
        Dim Total_Agustus As Int64 = 0
        Dim Total_September As Int64 = 0
        Dim Total_Oktober As Int64 = 0
        Dim Total_Nopember As Int64 = 0
        Dim Total_Desember As Int64 = 0
        Dim COAAsset_Terindeks = Kosongan
        Dim JumlahBarisAssetTerbundel

        Dim SusurAkun = AwalAkunAssetTetap
        Dim TambahBaris As Boolean = False

        Do While SusurAkun <= AkhirAkunAssetTetap

            TambahBaris = False
            JumlahBarisAssetTerbundel = 0
            Rekap_AkumulasiPenyusutanTahunLaporan = 0
            Rekap_Januari = 0
            Rekap_Februari = 0
            Rekap_Maret = 0
            Rekap_April = 0
            Rekap_Mei = 0
            Rekap_Juni = 0
            Rekap_Juli = 0
            Rekap_Agustus = 0
            Rekap_September = 0
            Rekap_Oktober = 0
            Rekap_Nopember = 0
            Rekap_Desember = 0

            For Each row As DataRow In datatabelUtama.Rows

                COAAsset_Terindeks = AmbilValueCellTeksBerpotensiDBNull_Row(row, "COA_Asset")

                If AmbilAngka(COAAsset_Terindeks) = SusurAkun Then
                    TambahBaris = True
                    JumlahBarisAssetTerbundel += 1
                    KodeAkun_Asset = row("COA_Asset")
                    KodeAkun_BiayaPenyusutan = row("COA_Biaya_Penyusutan")
                    If KodeAkun_BiayaPenyusutan = Kosongan Then TambahBaris = False
                    NamaAkun_BiayaPenyusutan = row("Nama_Akun_Biaya_Penyusutan")
                    AkumulasiPenyusutanTahunLaporan = row("Akumulasi_Tahun_Laporan")
                    Januari = AmbilAngka(row("Januari_"))
                    Februari = AmbilAngka(row("Februari_"))
                    Maret = AmbilAngka(row("Maret_"))
                    April = AmbilAngka(row("April_"))
                    Mei = AmbilAngka(row("Mei_"))
                    Juni = AmbilAngka(row("Juni_"))
                    Juli = AmbilAngka(row("Juli_"))
                    Agustus = AmbilAngka(row("Agustus_"))
                    September = AmbilAngka(row("September_"))
                    Oktober = AmbilAngka(row("Oktober_"))
                    Nopember = AmbilAngka(row("Nopember_"))
                    Desember = AmbilAngka(row("Desember_"))
                    Rekap_AkumulasiPenyusutanTahunLaporan += AkumulasiPenyusutanTahunLaporan
                    Rekap_Januari += Januari
                    Rekap_Februari += Februari
                    Rekap_Maret += Maret
                    Rekap_April += April
                    Rekap_Mei += Mei
                    Rekap_Juni += Juni
                    Rekap_Juli += Juli
                    Rekap_Agustus += Agustus
                    Rekap_September += September
                    Rekap_Oktober += Oktober
                    Rekap_Nopember += Nopember
                    Rekap_Desember += Desember
                    Total_Januari += Januari
                    Total_Februari += Februari
                    Total_Maret += Maret
                    Total_April += April
                    Total_Mei += Mei
                    Total_Juni += Juni
                    Total_Juli += Juli
                    Total_Agustus += Agustus
                    Total_September += September
                    Total_Oktober += Oktober
                    Total_Nopember += Nopember
                    Total_Desember += Desember
                End If

            Next

            If TambahBaris = True Then
                NomorUrut += 1
                DataTabelSementara.Rows.Add(NomorUrut, KodeAkun_Asset, KodeAkun_BiayaPenyusutan, NamaAkun_BiayaPenyusutan, Rekap_AkumulasiPenyusutanTahunLaporan, Rekap_Januari, Rekap_Februari, Rekap_Maret, Rekap_April, Rekap_Mei, Rekap_Juni,
                                            Rekap_Juli, Rekap_Agustus, Rekap_September, Rekap_Oktober, Rekap_Nopember, Rekap_Desember)
            End If

            SusurAkun += 1

        Loop

        datatabelUtama.Rows.Clear()

        NomorUrut = 0

        For Each rowBundelAkun As DataRow In DataTabelSementara.Rows
            NomorUrut += 1
            KodeAkun_Asset = rowBundelAkun.Item("COA_Asset")
            KodeAkun_BiayaPenyusutan = rowBundelAkun.Item("COA_Biaya_Penyusutan")
            NamaAkun_BiayaPenyusutan = rowBundelAkun.Item("Nama_Akun_Biaya_Penyusutan")
            Rekap_AkumulasiPenyusutanTahunLaporan = rowBundelAkun.Item("Akumulasi_Tahun_Laporan")
            Rekap_Januari = AmbilAngka(rowBundelAkun.Item("Januari_"))
            Rekap_Februari = AmbilAngka(rowBundelAkun.Item("Februari_"))
            Rekap_Maret = AmbilAngka(rowBundelAkun.Item("Maret_"))
            Rekap_April = AmbilAngka(rowBundelAkun.Item("April_"))
            Rekap_Mei = AmbilAngka(rowBundelAkun.Item("Mei_"))
            Rekap_Juni = AmbilAngka(rowBundelAkun.Item("Juni_"))
            Rekap_Juli = AmbilAngka(rowBundelAkun.Item("Juli_"))
            Rekap_Agustus = AmbilAngka(rowBundelAkun.Item("Agustus_"))
            Rekap_September = AmbilAngka(rowBundelAkun.Item("September_"))
            Rekap_Oktober = AmbilAngka(rowBundelAkun.Item("Oktober_"))
            Rekap_Nopember = AmbilAngka(rowBundelAkun.Item("Nopember_"))
            Rekap_Desember = AmbilAngka(rowBundelAkun.Item("Desember_"))

            datatabelUtama.Rows.Add(NomorUrut, Kosongan, Kosongan, Kosongan, Kosongan, KodeAkun_Asset, Kosongan, KodeAkun_BiayaPenyusutan, NamaAkun_BiayaPenyusutan,
                                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                    Kosongan, 0, 0, 0, 0,
                                    0, 0, Kosongan,
                                    0, 0, 0,
                                    Rekap_AkumulasiPenyusutanTahunLaporan, 0, 0,
                                    Rekap_Januari, Rekap_Februari, Rekap_Maret, Rekap_April, Rekap_Mei, Rekap_Juni,
                                    Rekap_Juli, Rekap_Agustus, Rekap_September, Rekap_Oktober, Rekap_Nopember, Rekap_Desember)

            'Cek Data Jurnal :
            If TahunLaporan = TahunBukuAktif Then
                Dim IndexBaris = NomorUrut - 1
                Dim AkumulasiPenyusutanTahunLaporan_BerdasarkanJurnal As Int64 = 0
                AksesDatabase_Transaksi(Buka)
                cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                               " WHERE  Valid    <> '" & _X_ & "' " &
                                               " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                               " AND COA = '" & KodeAkun_BiayaPenyusutan & "' ", KoneksiDatabaseTransaksi)
                'Kenapa Query Di sini tidak menggunakan kriteria BUNDELAN..? Karena ini tampilan Detail Rekap..!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
                Do While drCEKJURNAL.Read
                    AkumulasiPenyusutanTahunLaporan_BerdasarkanJurnal += AmbilAngka(drCEKJURNAL.Item("Jumlah_Debet"))
                Loop
                AksesDatabase_Transaksi(Tutup)
                If Rekap_AkumulasiPenyusutanTahunLaporan <> AkumulasiPenyusutanTahunLaporan_BerdasarkanJurnal Then KesesuaianJurnal = False
            End If
        Next

        JumlahBaris = datatabelUtama.Rows.Count

        datatabelUtama.Rows.Add()
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, teks_Jumlah, Kosongan, teks_Jumlah,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, 0, 0, 0, 0,
                                0, 0, Kosongan,
                                0, 0, 0,
                                0, 0, 0,
                                Total_Januari, Total_Februari, Total_Maret, Total_April, Total_Mei, Total_Juni,
                                Total_Juli, Total_Agustus, Total_September, Total_Oktober, Total_Nopember, Total_Desember)

    End Sub


    Sub PewarnaanKolomBulan()
        If TahunLaporan = TahunBukuAktif Then
            Januari_.Foreground = WarnaPudar_WPF
            Februari_.Foreground = WarnaPudar_WPF
            Maret_.Foreground = WarnaPudar_WPF
            April_.Foreground = WarnaPudar_WPF
            Mei_.Foreground = WarnaPudar_WPF
            Juni_.Foreground = WarnaPudar_WPF
            Juli_.Foreground = WarnaPudar_WPF
            Agustus_.Foreground = WarnaPudar_WPF
            September_.Foreground = WarnaPudar_WPF
            Oktober_.Foreground = WarnaPudar_WPF
            Nopember_.Foreground = WarnaPudar_WPF
            Desember_.Foreground = WarnaPudar_WPF
        Else
            Januari_.Foreground = WarnaTeksStandar_WPF
            Februari_.Foreground = WarnaTeksStandar_WPF
            Maret_.Foreground = WarnaTeksStandar_WPF
            April_.Foreground = WarnaTeksStandar_WPF
            Mei_.Foreground = WarnaTeksStandar_WPF
            Juni_.Foreground = WarnaTeksStandar_WPF
            Juli_.Foreground = WarnaTeksStandar_WPF
            Agustus_.Foreground = WarnaTeksStandar_WPF
            September_.Foreground = WarnaTeksStandar_WPF
            Oktober_.Foreground = WarnaTeksStandar_WPF
            Nopember_.Foreground = WarnaTeksStandar_WPF
            Desember_.Foreground = WarnaTeksStandar_WPF
        End If
    End Sub


    Sub SembunyikanSemuaKolomTabel()
        Nomor_Urut.Visibility = Visibility.Collapsed
        Nomor_ID.Visibility = Visibility.Collapsed
        Nama_Aktiva.Visibility = Visibility.Collapsed
        COA_Asset.Visibility = Visibility.Collapsed
        Nama_Akun_Asset.Visibility = Visibility.Collapsed
        COA_Biaya_Penyusutan.Visibility = Visibility.Collapsed
        Nama_Akun_Biaya_Penyusutan.Visibility = Visibility.Collapsed
        Masa_Manfaat.Visibility = Visibility.Collapsed
        Kelompok_Harta.Visibility = Visibility.Collapsed
        Tarif_Penyusutan.Visibility = Visibility.Collapsed
        Divisi_.Visibility = Visibility.Collapsed
        Kode_Divisi.Visibility = Visibility.Collapsed
        Tanggal_Perolehan.Visibility = Visibility.Collapsed
        Harga_Perolehan.Visibility = Visibility.Collapsed
        Harga_Perolehan_Awal.Visibility = Visibility.Collapsed
        Penambahan_Pengurangan.Visibility = Visibility.Collapsed
        Harga_Perolehan_Akhir.Visibility = Visibility.Collapsed
        Penyusutan_Perbulan.Visibility = Visibility.Collapsed
        Penyusutan_Pertahun.Visibility = Visibility.Collapsed
        Temp_.Visibility = Visibility.Collapsed
        Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya.Visibility = Visibility.Collapsed
        Nilai_Sisa_Buku_Awal.Visibility = Visibility.Collapsed
        Saldo_Awal_Siap_Untuk_Disusutkan.Visibility = Visibility.Collapsed
        Akumulasi_Tahun_Laporan.Visibility = Visibility.Collapsed
        Akumulasi_Penyusutan_Sampai_Dengan.Visibility = Visibility.Collapsed
        Nilai_Sisa_Buku_Akhir_Tahun.Visibility = Visibility.Collapsed
        Januari_.Visibility = Visibility.Collapsed
        Februari_.Visibility = Visibility.Collapsed
        Maret_.Visibility = Visibility.Collapsed
        April_.Visibility = Visibility.Collapsed
        Mei_.Visibility = Visibility.Collapsed
        Juni_.Visibility = Visibility.Collapsed
        Juli_.Visibility = Visibility.Collapsed
        Agustus_.Visibility = Visibility.Collapsed
        September_.Visibility = Visibility.Collapsed
        Oktober_.Visibility = Visibility.Collapsed
        Nopember_.Visibility = Visibility.Collapsed
        Desember_.Visibility = Visibility.Collapsed
        Tanggal_Invoice.Visibility = Visibility.Collapsed
        Nomor_Invoice.Visibility = Visibility.Collapsed
        Keterangan_.Visibility = Visibility.Collapsed
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            Kode_Asset.Visibility = Visibility.Collapsed
            Kode_Closing.Visibility = Visibility.Collapsed
        End If
    End Sub




    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_DetailGlobal_Click(sender As Object, e As RoutedEventArgs) Handles btn_DetailGlobal.Click
        ResetFilter()
        Select Case btn_DetailGlobal.Content
            Case teks_Global
                JenisTampilan = JenisTampilan_GLOBAL_Rinci
            Case teks_DetailPerbulan
                JenisTampilan = JenisTampilan_DETAIL_Rinci
        End Select
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Private Sub btn_Rekap_Click(sender As Object, e As RoutedEventArgs) Handles btn_Rekap.Click
        ResetFilter()
        Select Case btn_DetailGlobal.Content
            Case teks_Global
                Select Case btn_Rekap.Content
                    Case teks_Kembali
                        JenisTampilan = JenisTampilan_DETAIL_Rinci
                    Case teks_RekapPenyusutan
                        JenisTampilan = JenisTampilan_DETAIL_Rekap
                End Select
            Case teks_DetailPerbulan
                Select Case btn_Rekap.Content
                    Case teks_Kembali
                        JenisTampilan = JenisTampilan_GLOBAL_Rinci
                    Case teks_RekapGlobal
                        JenisTampilan = JenisTampilan_GLOBAL_Rekap
                End Select
        End Select
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Private Sub btn_KodeDivisi_Click(sender As Object, e As RoutedEventArgs) Handles btn_KodeDivisi.Click
        frm_KodeDivisi.ShowDialog()
    End Sub


    Private Sub btn_JualAsset_Click(sender As Object, e As RoutedEventArgs) Handles btn_JualAsset.Click

        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE  Valid    <> '" & _X_ & "' " &
                                       " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                       " AND Bundelan LIKE '%" & KodeAsset_Terseleksi & "%' " &
                                       " AND COA = '" & COABiayaPenyusutan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Dim TanggalJurnalPenyusutan
        Dim BulanJurnalPenyusutan = 0
        Do While drCEKJURNAL.Read
            TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
            BulanJurnalPenyusutan = AmbilAngka(Format(TanggalJurnalPenyusutan, "MM"))
        Loop
        AksesDatabase_Transaksi(Tutup)

        If BulanJurnalPenyusutan = 12 Then
            Pesan_Peringatan("'Jurnal Penyusutan' terkait dengan asset ini sudah diposting sampai Bulan Desember." & Enter2Baris &
                   "Jika ingin posting data penjutalan terkait asset ini, silakan hapus terlebih dahulu postingan jurnalnya, " &
                   "mulai dari bulan saat transaksi penjualan asset sampai seterusnya.")
            Return
        End If

        Dim BulanPerolehan = AmbilBulanAngka_DariTanggal(TanggalPerolehan_Terseleksi)
        Dim TahunPerolehan = AmbilTahun_DariTanggal(TanggalPerolehan_Terseleksi)
        Dim BulanDijual_Angka
        If BulanJurnalPenyusutan = 0 And TahunPerolehan = TahunBukuAktif And BulanPerolehan > 0 Then
            BulanDijual_Angka = BulanPerolehan
        Else
            BulanDijual_Angka = BulanJurnalPenyusutan + 1
        End If

        win_JualAsset = New wpfWin_JualAsset
        win_JualAsset.BulanDijual_Angka = BulanDijual_Angka  'Variabel 'BulanPengunci' sengaja lebih didahulukan dari ResetForm
        win_JualAsset.ResetForm()
        win_JualAsset.JenisProduk_Induk = JenisProduk_Barang
        win_JualAsset.JenisProduk_PerItem = JenisProduk_Barang
        win_JualAsset.KodeAsset = KodeAsset_Terseleksi
        win_JualAsset.txt_NamaAktiva.Text = NamaAktiva_Terseleksi
        win_JualAsset.KelompokHarta = KelompokHarta_Terseleksi
        win_JualAsset.dtp_TanggalPerolehan.SelectedDate = TanggalFormatWPF(TanggalPerolehan_Terseleksi)
        win_JualAsset.txt_NilaiSisaBuku.Text = NSB_BerdasarkanJurnalTerakhir_Terseleksi
        win_JualAsset.txt_HargaPerolehan.Text = HargaPerolehan_Terseleksi
        If KelompokHarta_Terseleksi = KelompokHarta_Tanah Then
            win_JualAsset.txt_COA_AkumulasiPenyusutan.Text = Kosongan
        Else
            win_JualAsset.txt_COA_AkumulasiPenyusutan.Text = AmbilValue_COAAkumulasiPenyusutan_DariDataAsset(KodeAsset_Terseleksi)
        End If
        win_JualAsset.txt_AkumulasiPenyusutan.Text = AkumulasiPenyusutanSampaiDenganJurnalTerakhir_Terseleksi
        If NSB_BerdasarkanJurnalTerakhir_Terseleksi = 0 _
            Or KelompokHarta_Terseleksi = KelompokHarta_Tanah _
            Then
            win_JualAsset.lbl_PerTanggal_1.Visibility = Visibility.Collapsed
            win_JualAsset.lbl_PerTanggal_2.Visibility = Visibility.Collapsed
        Else
            win_JualAsset.lbl_PerTanggal_1.Visibility = Visibility.Visible
            win_JualAsset.lbl_PerTanggal_2.Visibility = Visibility.Visible
        End If
        If KelompokHarta_Terseleksi = KelompokHarta_Tanah _
            Or KelompokHarta_Terseleksi = KelompokHarta_BangunanPermanen _
            Or KelompokHarta_Terseleksi = KelompokHarta_BangunanTidakPermanen _
            Then
            win_JualAsset.AdaPPh = True
        Else
            win_JualAsset.AdaPPh = False
        End If
        win_JualAsset.COA_AssetDijual = COAAsset_Terseleksi
        win_JualAsset.ShowDialog()

    End Sub


    Private Sub btn_DisposalAsset_Click(sender As Object, e As RoutedEventArgs) Handles btn_DisposalAsset.Click

        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE  Valid    <> '" & _X_ & "' " &
                                       " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                       " AND Bundelan LIKE '%" & KodeAsset_Terseleksi & "%' " &
                                       " AND COA = '" & COABiayaPenyusutan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Dim TanggalJurnalPenyusutan
        Dim BulanJurnalPenyusutan = 0
        Do While drCEKJURNAL.Read
            TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
            BulanJurnalPenyusutan = AmbilAngka(Format(TanggalJurnalPenyusutan, "MM"))
        Loop
        AksesDatabase_Transaksi(Tutup)

        If BulanJurnalPenyusutan = 12 Then
            Pesan_Peringatan("'Jurnal Penyusutan' terkait dengan asset ini sudah diposting sampai Bulan Desember." & Enter2Baris &
                   "Jika ingin posting data disposal terkait asset ini, silakan hapus terlebih dahulu postingan jurnalnya, " &
                   "mulai dari bulan saat Disposal Asset sampai seterusnya.")
            Return
        End If

        win_InputDisposalAssetTetap = New wpfWin_InputDisposalAssetTetap
        win_InputDisposalAssetTetap.BulanDisposal_Angka = BulanJurnalPenyusutan + 1 'Variabel 'BulanPengunci' sengaja lebih didahulukan dari ResetForm
        win_InputDisposalAssetTetap.ResetForm()
        win_InputDisposalAssetTetap.KodeAsset = KodeAsset_Terseleksi
        win_InputDisposalAssetTetap.txt_NamaAktiva.Text = NamaAktiva_Terseleksi
        win_InputDisposalAssetTetap.KelompokHarta = KelompokHarta_Terseleksi
        win_InputDisposalAssetTetap.dtp_TanggalPerolehan.SelectedDate = TanggalFormatWPF(TanggalPerolehan_Terseleksi)
        win_InputDisposalAssetTetap.txt_NilaiSisaBuku.Text = NSB_BerdasarkanJurnalTerakhir_Terseleksi
        win_InputDisposalAssetTetap.txt_HargaPerolehan.Text = HargaPerolehan_Terseleksi
        If KelompokHarta_Terseleksi = KelompokHarta_Tanah Then
            win_InputDisposalAssetTetap.txt_COA_AkumulasiPenyusutan.Text = Kosongan
        Else
            win_InputDisposalAssetTetap.txt_COA_AkumulasiPenyusutan.Text = PenentuanCOA_AkumulasiPenyusutan(COAAsset_Terseleksi)
        End If
        win_InputDisposalAssetTetap.txt_AkumulasiPenyusutan.Text = AkumulasiPenyusutanSampaiDenganJurnalTerakhir_Terseleksi
        If NSB_BerdasarkanJurnalTerakhir_Terseleksi = 0 _
            Or KelompokHarta_Terseleksi = KelompokHarta_Tanah _
            Then
            win_InputDisposalAssetTetap.lbl_PerTanggal_1.Visibility = Visibility.Collapsed
            win_InputDisposalAssetTetap.lbl_PerTanggal_2.Visibility = Visibility.Collapsed
        Else
            win_InputDisposalAssetTetap.lbl_PerTanggal_1.Visibility = Visibility.Visible
            win_InputDisposalAssetTetap.lbl_PerTanggal_2.Visibility = Visibility.Visible
        End If
        win_InputDisposalAssetTetap.COA_AssetDisposal = COAAsset_Terseleksi
        win_InputDisposalAssetTetap.ShowDialog()

    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputDataAsset = New wpfWin_InputDataAsset
        win_InputDataAsset.ResetForm()
        win_InputDataAsset.FungsiForm = FungsiForm_TAMBAH
        win_InputDataAsset.JalurMasuk = Halaman_DATAASSETTETAP
        win_InputDataAsset.ShowDialog()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputDataAsset = New wpfWin_InputDataAsset
        win_InputDataAsset.ResetForm()
        win_InputDataAsset.FungsiForm = FungsiForm_EDIT
        win_InputDataAsset.JalurMasuk = Halaman_DATAASSETTETAP
        win_InputDataAsset.IdAsset = NomorID_Terseleksi
        win_InputDataAsset.cmb_KelompokHarta.SelectedValue = KelompokHarta_Terseleksi
        win_InputDataAsset.txt_KodeAsset.Text = KodeAsset_Terseleksi
        win_InputDataAsset.KodeAsset_SebelumDiedit = KodeAsset_Terseleksi
        win_InputDataAsset.txt_NamaAktiva.Text = NamaAktiva_Terseleksi
        win_InputDataAsset.txt_COA_Asset.Text = COAAsset_Terseleksi
        win_InputDataAsset.txt_NamaAkun_Asset.Text = NamaAkunAsset_Terseleksi
        win_InputDataAsset.txt_COA_BiayaPenyusutan.Text = COABiayaPenyusutan_Terseleksi
        win_InputDataAsset.txt_NamaAkun_BiayaPenyusutan.Text = NamaAkunBiayaPenyusutan_Terseleksi
        win_InputDataAsset.cmb_Divisi.SelectedValue = Divisi_Terseleksi
        win_InputDataAsset.dtp_TanggalPerolehan.SelectedDate = TanggalFormatWPF(TanggalPerolehan_Terseleksi)
        win_InputDataAsset.txt_HargaPerolehan.Text = HargaPerolehan_Terseleksi
        IsiValueElemenRichTextBox(win_InputDataAsset.txt_Keterangan, Keterangan_Terseleksi)
        win_InputDataAsset.ShowDialog()
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then Return '(Untuk jaga-jaga saja...!)

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        HapusDataAsset_BerdasarkanNomorID(NomorID_Terseleksi)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub


    Private Sub btn_Posting_Click(sender As Object, e As RoutedEventArgs) Handles btn_Posting.Click

        win_CeklisBulan = New wpfWin_CeklisBulan

        'Non-Aktif-kan Ceklis Bulan yang sudah dijurnal :
        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE  Valid    <> '" & _X_ & "' " &
                                       " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                       " AND COA = '" & COABiayaPenyusutan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Do While drCEKJURNAL.Read
            Dim TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
            Dim BulanJurnalPenyusutan = AmbilBulanAngka_DariTanggal(TanggalJurnalPenyusutan)
            If AmbilAngka(BulanJurnalPenyusutan) = 1 Then win_CeklisBulan.chk_Januari.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 2 Then win_CeklisBulan.chk_Februari.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 3 Then win_CeklisBulan.chk_Maret.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 4 Then win_CeklisBulan.chk_April.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 5 Then win_CeklisBulan.chk_Mei.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 6 Then win_CeklisBulan.chk_Juni.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 7 Then win_CeklisBulan.chk_Juli.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 8 Then win_CeklisBulan.chk_Agustus.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 9 Then win_CeklisBulan.chk_September.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 10 Then win_CeklisBulan.chk_Oktober.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 11 Then win_CeklisBulan.chk_Nopember.IsEnabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 12 Then win_CeklisBulan.chk_Desember.IsEnabled = False
        Loop
        AksesDatabase_Transaksi(Tutup)

        'Non-Aktif-kan Ceklis Bulan yang belum waktunya untuk dijurnal :
        If TahunBukuAktif = TahunIni Then
            If AmbilAngka(BulanIni) <= 1 Then
                If AmbilAngka(BulanIni) = 1 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Januari.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Januari.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 2 Then
                If AmbilAngka(BulanIni) = 2 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Februari.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Februari.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 3 Then
                If AmbilAngka(BulanIni) = 3 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Maret.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Maret.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 4 Then
                If AmbilAngka(BulanIni) = 4 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_April.IsEnabled = True
                Else
                    win_CeklisBulan.chk_April.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 5 Then
                If AmbilAngka(BulanIni) = 5 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Mei.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Mei.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 6 Then
                If AmbilAngka(BulanIni) = 6 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Juni.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Juni.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 7 Then
                If AmbilAngka(BulanIni) = 7 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Juli.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Juli.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 8 Then
                If AmbilAngka(BulanIni) = 8 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Agustus.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Agustus.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 9 Then
                If AmbilAngka(BulanIni) = 9 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_September.IsEnabled = True
                Else
                    win_CeklisBulan.chk_September.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 10 Then
                If AmbilAngka(BulanIni) = 10 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Oktober.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Oktober.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 11 Then
                If AmbilAngka(BulanIni) = 11 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Nopember.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Nopember.IsEnabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 12 Then
                If AmbilAngka(BulanIni) = 12 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    win_CeklisBulan.chk_Desember.IsEnabled = True
                Else
                    win_CeklisBulan.chk_Desember.IsEnabled = False
                End If
            End If
        End If

        'Non-Aktif-kan Ceklis Bulan yang tidak ada angka Jurnal-nya, alias 0 (nol) :
        If AmbilAngka(rowviewUtama("Januari_")) = 0 Then win_CeklisBulan.chk_Januari.IsEnabled = False
        If AmbilAngka(rowviewUtama("Februari_")) = 0 Then win_CeklisBulan.chk_Februari.IsEnabled = False
        If AmbilAngka(rowviewUtama("Maret_")) = 0 Then win_CeklisBulan.chk_Maret.IsEnabled = False
        If AmbilAngka(rowviewUtama("April_")) = 0 Then win_CeklisBulan.chk_April.IsEnabled = False
        If AmbilAngka(rowviewUtama("Mei_")) = 0 Then win_CeklisBulan.chk_Mei.IsEnabled = False
        If AmbilAngka(rowviewUtama("Juni_")) = 0 Then win_CeklisBulan.chk_Juni.IsEnabled = False
        If AmbilAngka(rowviewUtama("Juli_")) = 0 Then win_CeklisBulan.chk_Juli.IsEnabled = False
        If AmbilAngka(rowviewUtama("Agustus_")) = 0 Then win_CeklisBulan.chk_Agustus.IsEnabled = False
        If AmbilAngka(rowviewUtama("September_")) = 0 Then win_CeklisBulan.chk_September.IsEnabled = False
        If AmbilAngka(rowviewUtama("Oktober_")) = 0 Then win_CeklisBulan.chk_Oktober.IsEnabled = False
        If AmbilAngka(rowviewUtama("Nopember_")) = 0 Then win_CeklisBulan.chk_Nopember.IsEnabled = False
        If AmbilAngka(rowviewUtama("Desember_")) = 0 Then win_CeklisBulan.chk_Desember.IsEnabled = False

        'Non-Aktif-kan Ceklis Bulan yang belum waktunya :
        If BulanTerakhirDitutup = 0 Then win_CeklisBulan.chk_Februari.IsEnabled = False
        If BulanTerakhirDitutup <= 1 Then win_CeklisBulan.chk_Maret.IsEnabled = False
        If BulanTerakhirDitutup <= 2 Then win_CeklisBulan.chk_April.IsEnabled = False
        If BulanTerakhirDitutup <= 3 Then win_CeklisBulan.chk_Mei.IsEnabled = False
        If BulanTerakhirDitutup <= 4 Then win_CeklisBulan.chk_Juni.IsEnabled = False
        If BulanTerakhirDitutup <= 5 Then win_CeklisBulan.chk_Juli.IsEnabled = False
        If BulanTerakhirDitutup <= 6 Then win_CeklisBulan.chk_Agustus.IsEnabled = False
        If BulanTerakhirDitutup <= 7 Then win_CeklisBulan.chk_September.IsEnabled = False
        If BulanTerakhirDitutup <= 8 Then win_CeklisBulan.chk_Oktober.IsEnabled = False
        If BulanTerakhirDitutup <= 9 Then win_CeklisBulan.chk_Nopember.IsEnabled = False
        If BulanTerakhirDitutup <= 10 Then win_CeklisBulan.chk_Desember.IsEnabled = False

        win_CeklisBulan.ShowDialog()
        If win_CeklisBulan.LanjutkanProses = False Then Return

        Dim BulanTerceklis_Awal = win_CeklisBulan.BulanTerceklis_Awal
        Dim BulanTerceklis_Akhir = win_CeklisBulan.BulanTerceklis_Akhir
        Dim BulanAngka_Awal = KonversiBulanKeAngka(BulanTerceklis_Awal)
        Dim BulanAngka_Akhir = KonversiBulanKeAngka(BulanTerceklis_Akhir)

        Dim Pesan As String =
            "Pastikan data penjualan asset (jika ada) sudah terposting seluruhnya sampai bulan " & BulanTerceklis_Akhir & " " &
            "sebelum posting jurnal...!!!" & Enter2Baris &
            "Lanjutkan posting..?"
        Pilihan = MessageBox.Show(Pesan, "PERHATIAN..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        Dim JenisTampilanAsal = JenisTampilan
        ProsesPostingJurnal = True
        SembunyikanSemuaKolomTabel()
        datagridUtama.Visibility = Visibility.Collapsed
        lbl_NotifikasiProses.Visibility = Visibility.Visible
        JenisTampilan = JenisTampilan_DETAIL_Rinci
        TampilkanData()

        Dim JumlahJurnalTerposting = 0
        Dim TotalPenyusutan As Int64
        Dim JumlahBarisAssetTerbundel

        Dim KodeAsset_Terindeks = Kosongan
        Dim COAAsset_Terindeks = Kosongan
        Dim NamaAkunAsset_Terindeks = Kosongan
        Dim COABiayaPenyusutan_Terindeks = Kosongan

        Dim JurnalSudahAda As Boolean
        Dim BulanAngka_Telusur = BulanAngka_Awal
        Dim NamaBulan = Kosongan
        Dim JumlahPenyusutan As Int64

        Do While BulanAngka_Telusur <= BulanAngka_Akhir

            jur_StatusPenyimpananJurnal_PerBaris = True 'Ini penting, Jangan dihapus.

            TotalPenyusutan = 0
            JumlahBarisAssetTerbundel = 0
            Bundelan = Kosongan

            For Each row As DataRow In datatabelUtama.Rows

                COAAsset_Terindeks = AmbilValueCellTeksBerpotensiDBNull_Row(row, "COA_Asset")

                If AmbilAngka(COAAsset_Terindeks) = AmbilAngka(COAAsset_Terseleksi) Then

                    Id_Terindeks = row("Nomor_ID")
                    KodeAsset_Terindeks = row("Kode_Asset")
                    NamaAkunAsset_Terindeks = row("Nama_Akun_Asset")
                    COABiayaPenyusutan_Terindeks = row("COA_Biaya_Penyusutan")

                    Select Case BulanAngka_Telusur
                        Case 1
                            NamaBulan = "Januari"
                            JumlahPenyusutan = AmbilAngka(row("Januari_"))
                        Case 2
                            NamaBulan = "Februari"
                            JumlahPenyusutan = AmbilAngka(row("Februari_"))
                        Case 3
                            NamaBulan = "Maret"
                            JumlahPenyusutan = AmbilAngka(row("Maret_"))
                        Case 4
                            NamaBulan = "April"
                            JumlahPenyusutan = AmbilAngka(row("April_"))
                        Case 5
                            NamaBulan = "Mei"
                            JumlahPenyusutan = AmbilAngka(row("Mei_"))
                        Case 6
                            NamaBulan = "Juni"
                            JumlahPenyusutan = AmbilAngka(row("Juni_"))
                        Case 7
                            NamaBulan = "Juli"
                            JumlahPenyusutan = AmbilAngka(row("Juli_"))
                        Case 8
                            NamaBulan = "Agustus"
                            JumlahPenyusutan = AmbilAngka(row("Agustus_"))
                        Case 9
                            NamaBulan = "September"
                            JumlahPenyusutan = AmbilAngka(row("September_"))
                        Case 10
                            NamaBulan = "Oktober"
                            JumlahPenyusutan = AmbilAngka(row("Oktober_"))
                        Case 11
                            NamaBulan = "Nopember"
                            JumlahPenyusutan = AmbilAngka(row("Nopember_"))
                        Case 12
                            NamaBulan = "Desember"
                            JumlahPenyusutan = AmbilAngka(row("Desember_"))
                    End Select

                    TanggalTransaksi_Simpan = TanggalFormatSimpan(AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka_Telusur, TahunBukuAktif))

                    AksesDatabase_Transaksi(Buka)
                    cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                                   " WHERE  Valid    <> '" & _X_ & "' " &
                                                   " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                                   " AND Bundelan LIKE '%" & KodeAsset_Terindeks & "%' " &
                                                   " AND Tanggal_Transaksi = '" & TanggalTransaksi_Simpan & "' " &
                                                   " AND COA = '" & COABiayaPenyusutan_Terindeks & "' ", KoneksiDatabaseTransaksi)
                    drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
                    If drCEKJURNAL.HasRows Then
                        JurnalSudahAda = True
                    Else
                        JurnalSudahAda = False
                    End If
                    AksesDatabase_Transaksi(Tutup)

                    If JumlahPenyusutan > 0 And JurnalSudahAda = False Then
                        JumlahBarisAssetTerbundel += 1
                        TotalPenyusutan += JumlahPenyusutan
                        If JumlahBarisAssetTerbundel <= 1 Then
                            Bundelan = KodeAsset_Terindeks
                        Else
                            Bundelan = Bundelan & SlashGanda_Pemisah & KodeAsset_Terindeks
                        End If
                    End If

                End If

            Next

            If JumlahBarisAssetTerbundel >= 1 Then
                UraianTransaksi = "Penyusutan " & NamaAkunAsset_Terindeks & " Bulan " & NamaBulan & "."
                SistemPenomoranOtomatis_NomorJV()
                ResetValueJurnal()
                jur_JenisJurnal = JenisJurnal_Penyusutan
                jur_TanggalTransaksi = TanggalTransaksi_Simpan
                jur_Bundelan = Bundelan
                jur_UraianTransaksi = UraianTransaksi
                jur_Direct = 0

                'Simpan Jurnal :
                ___jurDebet(COABiayaPenyusutan_Terseleksi, TotalPenyusutan)
                _______jurKredit(COAAkumulasiPenyusutan_Terseleksi, TotalPenyusutan)

                If jur_StatusPenyimpananJurnal_Lengkap = True Then
                    JumlahJurnalTerposting += 1
                Else
                    Pesan_Gagal("Ups... Terjadi kesalahan pada proses penyimpanan..!")
                    Exit Do
                End If
            End If

            BulanAngka_Telusur += 1

            'Syntax di bawah ini berfungsi untuk mencegah penjurnalan pada Bulan Berjalan di Tahun Berjalan
            'Sementara untuk bulan yang sama di tahun yang berbeda (sebelumnya) tetap dijurnal.
            'Dengan kata lain, pada tahun sebelum tahun berjalan, penjurnalan dilakukan secara lengkap (12 Bulan)
            If TahunBukuAktif = TahunIni And BulanAngka_Telusur = BulanIni Then Exit Do
            If jur_StatusPenyimpananJurnal_PerBaris = False Then Exit Do

        Loop '<--- Ujung Loop : Susur Nomor Bulan

        ProsesPostingJurnal = False
        datagridUtama.Visibility = Visibility.Visible
        lbl_NotifikasiProses.Visibility = Visibility.Collapsed
        JenisTampilan = JenisTampilanAsal
        TampilkanData()

        If jur_StatusPenyimpananJurnal_PerBaris = True Then
            If JumlahJurnalTerposting = 1 Then
                Pesan_Sukses("'Jurnal Penyusutan' Akun '" & NamaAkunBiayaPenyusutan_Terseleksi &
                       "' Bulan " & BulanTerceklis_Awal & " BERHASIL diposting.")
            Else
                Pesan_Sukses("'Jurnal Penyusutan' Akun '" & NamaAkunBiayaPenyusutan_Terseleksi &
                       "' BERHASIL diposting untuk Bulan " & BulanTerceklis_Awal & " - " & BulanTerceklis_Akhir & ".")
            End If
        Else
            If JumlahJurnalTerposting > 0 Then
                Pesan_Peringatan("'Jurnal Penyusutan' Akun '" & NamaAkunBiayaPenyusutan_Terseleksi & "' hanya terposting sebagian." & Enter2Baris & teks_SilakanUlangiLagi_Database)
            Else
                Pesan_Gagal("'Jurnal Penyusutan' Akun '" & NamaAkunBiayaPenyusutan_Terseleksi & "' GAGAL diposting." & Enter2Baris & teks_SilakanUlangiLagi_Database)
            End If
        End If

    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        win_PilihJurnal_DataAsset = New wpfWin_PilihJurnal_DataAsset
        win_PilihJurnal_DataAsset.ResetForm()
        Dim AdaJurnal = 0
        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE  Valid    <> '" & _X_ & "' " &
                                       " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                       " AND COA = '" & COABiayaPenyusutan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Do While drCEKJURNAL.Read
            Dim TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
            Dim BulanJurnalPenyusutan = Format(TanggalJurnalPenyusutan, "MM")
            If AmbilAngka(BulanJurnalPenyusutan) = 1 Then
                win_PilihJurnal_DataAsset.rdb_Januari.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Januari = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 2 Then
                win_PilihJurnal_DataAsset.rdb_Februari.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Februari = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 3 Then
                win_PilihJurnal_DataAsset.rdb_Maret.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Maret = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 4 Then
                win_PilihJurnal_DataAsset.rdb_April.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_April = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 5 Then
                win_PilihJurnal_DataAsset.rdb_Mei.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Mei = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 6 Then
                win_PilihJurnal_DataAsset.rdb_Juni.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Juni = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 7 Then
                win_PilihJurnal_DataAsset.rdb_Juli.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Juli = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 8 Then
                win_PilihJurnal_DataAsset.rdb_Agustus.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Agustus = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 9 Then
                win_PilihJurnal_DataAsset.rdb_September.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_September = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 10 Then
                win_PilihJurnal_DataAsset.rdb_Oktober.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Oktober = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 11 Then
                win_PilihJurnal_DataAsset.rdb_Nopember.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Nopember = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 12 Then
                win_PilihJurnal_DataAsset.rdb_Desember.IsEnabled = True
                win_PilihJurnal_DataAsset.AngkaNomorJV_Desember = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)
        If AdaJurnal > 0 Then
            win_PilihJurnal_DataAsset.ShowDialog()
        Else
            Pesan_Informasi("Tidak/Belum ada Jurnal pada Tahun Buku ini untuk data terpilih.")
        End If
    End Sub

    Private Sub btn_Adjusment_Click(sender As Object, e As RoutedEventArgs) Handles btn_Adjusment.Click
        If ModusAplikasi = "CLASSIC" Then
            win_BOOKU.BukaHalamanAdjusmentPenyusutanAsset()
        Else
            win_BOOKU.BukaHalamanAdjusmentPenyusutanAsset()
        End If
    End Sub


    Private Sub btn_LihatInvoice_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatInvoice.Click
        Cetak(JenisFormCetak_Invoice, KonversiNomorPenjualanKeNomorInvoice(KodeClosing_Terseleksi), True, False)
    End Sub


    Private Sub btn_Import_Click(sender As Object, e As RoutedEventArgs) Handles btn_Import.Click

        'If Not LevelUserAktif = LevelUser_99_AppDeveloper Then
        '    FiturDalamPerbaikan()
        '    Return
        'End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            Pilihan = MessageBox.Show("Penggunaan fitur ini akan mengakibatkan Data Jurnal yang telah diposting berkaitan dengan Data Asset yang ada di tabel ini akan dihapus seluruhnya." & Enter2Baris &
                                  "Tidak perlu khawatir, karena Anda bisa mempostingnya lagi di lain kesempatan." & Enter2Baris &
                                  "Lanjutkan import..?",
                                  "PERHATIAN..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then Return
        End If

        'Hapus Data Jurnal Terlebih Dahulu Sebeleum Import :
        Dim ProsesHapus As Boolean = True 'Sudah benar True, jangan diganti False
        Dim JumlahJurnalDihapus = 0
        Dim cmdSUSURJURNAL As OdbcCommand
        Dim drSUSURJURNAL As OdbcDataReader
        Dim cmdHAPUSJURNAL As OdbcCommand
        Dim NomorJV_HarusDihapus = Kosongan
        Dim KodeAkun_BiayaPenyusutan = Kosongan
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA " &
                              " WHERE (COA BETWEEN 62000 AND 62299) OR (COA BETWEEN 52200 AND 52900) ",
                              KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        AksesDatabase_Transaksi(Buka)
        Do While dr.Read
            KodeAkun_BiayaPenyusutan = dr.Item("COA")
            cmdSUSURJURNAL = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                                             " WHERE  Valid    <> '" & _X_ & "' " &
                                             " AND COA = '" & KodeAkun_BiayaPenyusutan & "' ",
                                             KoneksiDatabaseTransaksi)
            drSUSURJURNAL = cmdSUSURJURNAL.ExecuteReader
            Do While drSUSURJURNAL.Read
                NomorJV_HarusDihapus = drSUSURJURNAL.Item("Nomor_JV")
                cmdHAPUSJURNAL = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                                 " WHERE Nomor_JV = '" & NomorJV_HarusDihapus & "' ",
                                                 KoneksiDatabaseTransaksi)
                Try
                    cmdHAPUSJURNAL.ExecuteNonQuery()
                    ProsesHapus = True
                    JumlahJurnalDihapus += 1
                Catch ex As Exception
                    ProsesHapus = False
                End Try
            Loop
        Loop
        AksesDatabase_Transaksi(Tutup)
        AksesDatabase_General(Tutup)

        If ProsesHapus = True Then
            If JumlahJurnalDihapus > 1 Then
                Pilihan = MessageBox.Show("Seluruh data Jurnal terkait telah berhasil DIHAPUS." & Enter2Baris &
                                          "Lanjutkan import..?",
                                          "PERHATIAN..!", MessageBoxButtons.YesNo)
                If Pilihan = vbNo Then Return
            Else
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    Pesan_Informasi("Belum ada Jurnal yang perlu dihapus pada event ini." _
                       & Enter2Baris & "Silakan lanjurkan proses import.")
                End If
            End If
        Else
            Pesan_Gagal("Import dibatalakan..!!!" & Enter2Baris & teks_SilakanCobaLagi_Database)
            Return
        End If

        win_ProgressImportDataAsset = New wpfWin_ProgressImportDataAsset
        win_ProgressImportDataAsset.ShowDialog()
        If StatusPosting = Status_BATAL Then
            Pesan_Informasi("Proses posting telah dibatalkan seluruhnya pada event ini.")
        End If

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub



    Private Sub cmb_TahunLaporan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_TahunLaporan.SelectionChanged
        TahunLaporan = AmbilAngka(cmb_TahunLaporan.SelectedValue)
        TahunLaporanSebelumnya = TahunLaporan - 1
        Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya.Header = "Akumulasi" & Enter1Baris & "Penyusutan" & Enter1Baris & "s.d. " & TahunLaporanSebelumnya
        Akumulasi_Tahun_Laporan.Header = "Penyusutan" & Enter1Baris & "Tahun " & TahunLaporan
        Akumulasi_Penyusutan_Sampai_Dengan.Header = "Akumulasi" & Enter1Baris & "Penyusutan" & Enter1Baris & "s.d. " & TahunLaporan
        Nilai_Sisa_Buku_Akhir_Tahun.Header = "Nilai Sisa Buku" & Enter1Baris & "Akhir" & Enter1Baris & "Tahun " & TahunLaporan
        TampilkanData()
    End Sub


    Private Sub cmb_FilterCOA_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_FilterCOA.SelectionChanged
        FilterAkun = Left(cmb_FilterCOA.SelectedValue, JumlahDigitCOA)
        TampilkanData()
    End Sub


    Private Sub cmb_FilterKelompokHarta_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_FilterKelompokHarta.SelectionChanged
        FilterKelompokHarta = cmb_FilterKelompokHarta.SelectedValue
        TampilkanData()
    End Sub


    Private Sub cmb_FilterTahunPerolehan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_FilterTahunPerolehan.SelectionChanged
        FilterTahunPerolehan = cmb_FilterTahunPerolehan.SelectedValue
        TampilkanData()
    End Sub


    Private Sub btn_Filter_Click(sender As Object, e As RoutedEventArgs) Handles btn_Filter.Click
        TampilkanData()
    End Sub




    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        NomorID_Terseleksi = rowviewUtama("Nomor_ID")
        KodeAsset_Terseleksi = rowviewUtama("Kode_Asset")
        NomorPembelian_Terseleksi = rowviewUtama("Nomor_Pembelian")
        NamaAktiva_Terseleksi = rowviewUtama("Nama_Aktiva")
        COAAsset_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Asset")
        NamaAkunAsset_Terseleksi = rowviewUtama("Nama_Akun_Asset")
        COABiayaPenyusutan_Terseleksi = rowviewUtama("COA_Biaya_Penyusutan")
        NamaAkunBiayaPenyusutan_Terseleksi = rowviewUtama("Nama_Akun_Biaya_Penyusutan")
        COAAkumulasiPenyusutan_Terseleksi = PenentuanCOA_AkumulasiPenyusutan(COAAsset_Terseleksi)
        KelompokHarta_Terseleksi = rowviewUtama("Kelompok_Harta")
        Divisi_Terseleksi = rowviewUtama("Kode_Divisi") & " - " & rowviewUtama("Divisi_")
        TanggalPerolehan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Perolehan")
        HargaPerolehan_Terseleksi = AmbilAngka(rowviewUtama("Harga_Perolehan"))
        HargaPerolehanAwal_Terseleksi = AmbilAngka(rowviewUtama("Harga_Perolehan_Awal"))
        Penambahan_Pengurangan_Terseleksi = AmbilAngka(rowviewUtama("Penambahan_Pengurangan"))
        HargaPerolehanAkhir_Terseleksi = AmbilAngka(rowviewUtama("Harga_Perolehan_Akhir"))
        AkumulasiPenyusutanSampaiTahunSebelumnya_Terseleksi = AmbilAngka(rowviewUtama("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya"))
        PenyusutanTahunLaporanYangSudahDijurnal_Terseleksi = AmbilAngka(rowviewUtama("Penyusutan_Tahun_Laporan_Yang_Sudah_Dijurnal"))
        NSB_BerdasarkanJurnalTerakhir_Terseleksi = AmbilAngka(rowviewUtama("NSB_Berdasarkan_Jurnal_Terakhir"))
        AkumulasiPenyusutanSampaiDenganJurnalTerakhir_Terseleksi _
            = AkumulasiPenyusutanSampaiTahunSebelumnya_Terseleksi _
            + PenyusutanTahunLaporanYangSudahDijurnal_Terseleksi
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        TahunTransaksi_Terseleksi = AmbilAngka(AmbilTeksKanan(TanggalPerolehan_Terseleksi, 4))
        KodeClosing_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Closing")
        If KodeClosing_Terseleksi <> Kosongan Then AssetSudahClosing_Terseleksi = True
        If KodeClosing_Terseleksi = Kosongan Then AssetSudahClosing_Terseleksi = False

        Select Case JenisTahunBuku
            Case JenisTahunBuku_NORMAL
                If TahunTransaksi_Terseleksi = TahunBukuAktif Then
                    btn_Edit.IsEnabled = True
                    btn_Hapus.IsEnabled = True
                Else
                    btn_Edit.IsEnabled = False
                    btn_Hapus.IsEnabled = False
                End If
            Case JenisTahunBuku_LAMPAU
                If TahunTransaksi_Terseleksi <= TahunBukuAktif Then
                    btn_Edit.IsEnabled = True
                    btn_Hapus.IsEnabled = True
                Else
                    btn_Edit.IsEnabled = False
                    btn_Hapus.IsEnabled = False
                End If
        End Select

        If TahunLaporan = TahunBukuAktif And JenisTahunBuku = JenisTahunBuku_NORMAL Then
            btn_LihatJurnal.IsEnabled = True
            btn_Posting.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
            btn_Posting.IsEnabled = False
        End If

        If BarisTerseleksi > (JumlahBaris - 1) Then
            BersihkanSeleksi()
        End If

        If BarisTerseleksi >= 0 Then
            If AssetSudahClosing_Terseleksi = False Then
                btn_JualAsset.IsEnabled = True
                btn_DisposalAsset.IsEnabled = True
            Else
                btn_JualAsset.IsEnabled = False
                btn_DisposalAsset.IsEnabled = False
            End If
        Else
            btn_JualAsset.IsEnabled = False
            btn_DisposalAsset.IsEnabled = False
        End If

        If btn_JualAsset.IsEnabled = False Then
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
        End If

        If AmbilTeksKiri(KodeClosing_Terseleksi, PanjangTeks_AwalanPENJ) = AwalanPENJ Then
            btn_LihatInvoice.IsEnabled = True
        Else
            btn_LihatInvoice.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If Not ProsesPostingJurnal Then
            Dim KodeClosing = e.Row.Item("Kode_Closing")
            Dim KodeAsset = e.Row.Item("Kode_Asset")
            Dim KodeAkun_BiayaPenyusutan = e.Row.Item("COA_Biaya_Penyusutan")
            Dim TanggalJurnalPenyusutan
            Dim BulanJurnalPenyusutan
            Dim cmdKhusus As OdbcCommand
            Dim drKhusus As OdbcDataReader
            Dim Warna As SolidColorBrush
            If LevelUserAktif >= LevelUser_99_AppDeveloper Then
                Warna = WarnaMerahSolid_WPF
            Else
                Warna = WarnaPudar_WPF
            End If
            If JenisTampilan = JenisTampilan_DETAIL_Rinci Then
                BukaDatabaseTransaksi_Kondisional()
                cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                            " WHERE  Valid    <> '" & _X_ & "' " &
                                            " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                            " AND Bundelan LIKE '%" & KodeAsset & "%' " &
                                            " AND COA = '" & KodeAkun_BiayaPenyusutan & "' ", KoneksiDatabaseTransaksi)
                drKhusus = cmdKhusus.ExecuteReader
                Do While drKhusus.Read
                    TanggalJurnalPenyusutan = drKhusus.Item("Tanggal_Transaksi")
                    BulanJurnalPenyusutan = AmbilAngka(Format(TanggalJurnalPenyusutan, "MM"))
                    If BulanJurnalPenyusutan = 1 Then PewarnaanCellFormatTeks(Januari_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 2 Then PewarnaanCellFormatTeks(Februari_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 3 Then PewarnaanCellFormatTeks(Maret_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 4 Then PewarnaanCellFormatTeks(April_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 5 Then PewarnaanCellFormatTeks(Mei_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 6 Then PewarnaanCellFormatTeks(Juni_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 7 Then PewarnaanCellFormatTeks(Juli_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 8 Then PewarnaanCellFormatTeks(Agustus_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 9 Then PewarnaanCellFormatTeks(September_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 10 Then PewarnaanCellFormatTeks(Oktober_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 11 Then PewarnaanCellFormatTeks(Nopember_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 12 Then PewarnaanCellFormatTeks(Desember_, e.Row, Warna)
                Loop
                TutupDatabaseTransaksi_Kondisional()
            End If
            If JenisTampilan = JenisTampilan_DETAIL_Rekap Then
                BukaDatabaseTransaksi_Kondisional()
                cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                            " WHERE  Valid    <> '" & _X_ & "' " &
                                            " AND Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                            " AND COA = '" & KodeAkun_BiayaPenyusutan & "' ", KoneksiDatabaseTransaksi)
                drKhusus = cmdKhusus.ExecuteReader
                Do While drKhusus.Read
                    TanggalJurnalPenyusutan = drKhusus.Item("Tanggal_Transaksi")
                    BulanJurnalPenyusutan = AmbilAngka(Format(TanggalJurnalPenyusutan, "MM"))
                    If BulanJurnalPenyusutan = 1 Then PewarnaanCellFormatTeks(Januari_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 2 Then PewarnaanCellFormatTeks(Februari_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 3 Then PewarnaanCellFormatTeks(Maret_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 4 Then PewarnaanCellFormatTeks(April_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 5 Then PewarnaanCellFormatTeks(Mei_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 6 Then PewarnaanCellFormatTeks(Juni_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 7 Then PewarnaanCellFormatTeks(Juli_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 8 Then PewarnaanCellFormatTeks(Agustus_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 9 Then PewarnaanCellFormatTeks(September_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 10 Then PewarnaanCellFormatTeks(Oktober_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 11 Then PewarnaanCellFormatTeks(Nopember_, e.Row, Warna)
                    If BulanJurnalPenyusutan = 12 Then PewarnaanCellFormatTeks(Desember_, e.Row, Warna)
                Loop
                TutupDatabaseTransaksi_Kondisional()
            End If
            If JenisTampilan = JenisTampilan_GLOBAL_Rinci Then
                If Not IsDBNull(KodeClosing) Then
                    If KodeClosing <> Kosongan Then e.Row.Foreground = WarnaPudar_WPF
                End If
            End If
        End If
    End Sub



    Private Sub txt_JumlahAsset_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahAsset.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahAsset)
    End Sub




    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        cmb_TahunLaporan.IsReadOnly = True
        cmb_FilterCOA.IsReadOnly = True
        cmb_FilterKelompokHarta.IsReadOnly = True
        cmb_FilterTahunPerolehan.IsReadOnly = True
        brd_TombolFilter.Visibility = Visibility.Collapsed
        pnl_TombolFilter.Visibility = Visibility.Collapsed
        txt_JumlahAsset.IsReadOnly = True
        lbl_NotifikasiProses.Foreground = WarnaTeksStandar_WPF
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

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Nomor_ID As New DataGridTextColumn
    Dim Kode_Asset As New DataGridTextColumn
    Dim Nomor_Pembelian As New DataGridTextColumn
    Dim Nama_Aktiva As New DataGridTextColumn
    Dim COA_Asset As New DataGridTextColumn
    Dim Nama_Akun_Asset As New DataGridTextColumn
    Dim COA_Biaya_Penyusutan As New DataGridTextColumn
    Dim Nama_Akun_Biaya_Penyusutan As New DataGridTextColumn
    Dim Masa_Manfaat As New DataGridTextColumn
    Dim Kelompok_Harta As New DataGridTextColumn
    Dim Tarif_Penyusutan As New DataGridTextColumn
    Dim Divisi_ As New DataGridTextColumn
    Dim Kode_Divisi As New DataGridTextColumn
    Dim Tanggal_Perolehan As New DataGridTextColumn
    Dim Harga_Perolehan As New DataGridTextColumn
    Dim Harga_Perolehan_Awal As New DataGridTextColumn
    Dim Penambahan_Pengurangan As New DataGridTextColumn
    Dim Harga_Perolehan_Akhir As New DataGridTextColumn
    Dim Penyusutan_Perbulan As New DataGridTextColumn
    Dim Penyusutan_Pertahun As New DataGridTextColumn
    Dim Temp_ As New DataGridTextColumn
    Dim Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya As New DataGridTextColumn
    Dim Nilai_Sisa_Buku_Awal As New DataGridTextColumn
    Dim Saldo_Awal_Siap_Untuk_Disusutkan As New DataGridTextColumn
    Dim Akumulasi_Tahun_Laporan As New DataGridTextColumn
    Dim Akumulasi_Penyusutan_Sampai_Dengan As New DataGridTextColumn
    Dim Nilai_Sisa_Buku_Akhir_Tahun As New DataGridTextColumn
    Dim Januari_ As New DataGridTextColumn
    Dim Februari_ As New DataGridTextColumn
    Dim Maret_ As New DataGridTextColumn
    Dim April_ As New DataGridTextColumn
    Dim Mei_ As New DataGridTextColumn
    Dim Juni_ As New DataGridTextColumn
    Dim Juli_ As New DataGridTextColumn
    Dim Agustus_ As New DataGridTextColumn
    Dim September_ As New DataGridTextColumn
    Dim Oktober_ As New DataGridTextColumn
    Dim Nopember_ As New DataGridTextColumn
    Dim Desember_ As New DataGridTextColumn
    Dim Penyusutan_Tahun_Laporan_Yang_Sudah_Dijurnal As New DataGridTextColumn
    Dim NSB_Berdasarkan_Jurnal_Terakhir As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Kode_Closing As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Kode_Asset")
        datatabelUtama.Columns.Add("Nomor_Pembelian")
        datatabelUtama.Columns.Add("Nama_Aktiva")
        datatabelUtama.Columns.Add("COA_Asset")
        datatabelUtama.Columns.Add("Nama_Akun_Asset")
        datatabelUtama.Columns.Add("COA_Biaya_Penyusutan")
        datatabelUtama.Columns.Add("Nama_Akun_Biaya_Penyusutan")
        datatabelUtama.Columns.Add("Masa_Manfaat")
        datatabelUtama.Columns.Add("Kelompok_Harta")
        datatabelUtama.Columns.Add("Tarif_Penyusutan")
        datatabelUtama.Columns.Add("Divisi_")
        datatabelUtama.Columns.Add("Kode_Divisi")
        datatabelUtama.Columns.Add("Tanggal_Perolehan")
        datatabelUtama.Columns.Add("Harga_Perolehan", GetType(Int64))
        datatabelUtama.Columns.Add("Harga_Perolehan_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Penambahan_Pengurangan", GetType(Int64))
        datatabelUtama.Columns.Add("Harga_Perolehan_Akhir", GetType(Int64))
        datatabelUtama.Columns.Add("Penyusutan_Perbulan", GetType(Int64))
        datatabelUtama.Columns.Add("Penyusutan_Pertahun", GetType(Int64))
        datatabelUtama.Columns.Add("Temp_")
        datatabelUtama.Columns.Add("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya", GetType(Int64))
        datatabelUtama.Columns.Add("Nilai_Sisa_Buku_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Awal_Siap_Untuk_Disusutkan", GetType(Int64))
        datatabelUtama.Columns.Add("Akumulasi_Tahun_Laporan", GetType(Int64))
        datatabelUtama.Columns.Add("Akumulasi_Penyusutan_Sampai_Dengan", GetType(Int64))
        datatabelUtama.Columns.Add("Nilai_Sisa_Buku_Akhir_Tahun", GetType(Int64))
        datatabelUtama.Columns.Add("Januari_", GetType(Int64))
        datatabelUtama.Columns.Add("Februari_", GetType(Int64))
        datatabelUtama.Columns.Add("Maret_", GetType(Int64))
        datatabelUtama.Columns.Add("April_", GetType(Int64))
        datatabelUtama.Columns.Add("Mei_", GetType(Int64))
        datatabelUtama.Columns.Add("Juni_", GetType(Int64))
        datatabelUtama.Columns.Add("Juli_", GetType(Int64))
        datatabelUtama.Columns.Add("Agustus_", GetType(Int64))
        datatabelUtama.Columns.Add("September_", GetType(Int64))
        datatabelUtama.Columns.Add("Oktober_", GetType(Int64))
        datatabelUtama.Columns.Add("Nopember_", GetType(Int64))
        datatabelUtama.Columns.Add("Desember_", GetType(Int64))
        datatabelUtama.Columns.Add("Penyusutan_Tahun_Laporan_Yang_Sudah_Dijurnal", GetType(Int64))
        datatabelUtama.Columns.Add("NSB_Berdasarkan_Jurnal_Terakhir", GetType(Int64))
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Kode_Closing")


        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor_ID", 75, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Asset, "Kode_Asset", "Kode Asset", 123, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Pembelian, "Nomor_Pembelian", "Nomor Pembelian", 75, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Aktiva, "Nama_Aktiva", "Nama Aktiva", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Asset, "COA_Asset", "Kode" & Enter1Baris & "Akun", 45, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun_Asset, "Nama_Akun_Asset", "Nama Akun", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Biaya_Penyusutan, "COA_Biaya_Penyusutan", "COA Biaya Penyusutan", 75, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun_Biaya_Penyusutan, "Nama_Akun_Biaya_Penyusutan", "Nama Akun" & Enter1Baris & "Penyusutan", 270, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Masa_Manfaat, "Masa_Manfaat", "Masa Manfaat", 75, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kelompok_Harta, "Kelompok_Harta", "Kelompok Harta", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tarif_Penyusutan, "Tarif_Penyusutan", "Tarif" & Enter1Baris & "Penyusutan", 63, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Divisi_, "Divisi_", "Divisi", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Divisi, "Kode_Divisi", "Kode Divisi", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Perolehan, "Tanggal_Perolehan", "Tanggal Perolehan", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Perolehan, "Harga_Perolehan", "Harga Perolehan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Perolehan_Awal, "Harga_Perolehan_Awal", "Harga Perolehan" & Enter1Baris & "Awal", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Penambahan_Pengurangan, "Penambahan_Pengurangan", "Penambahan/" & Enter1Baris & "Pengurangan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Perolehan_Akhir, "Harga_Perolehan_Akhir", "Harga Perolehan" & Enter1Baris & "Akhir", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Penyusutan_Perbulan, "Penyusutan_Perbulan", "Penyusutan Perbulan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Penyusutan_Pertahun, "Penyusutan_Pertahun", "Penyusutan Pertahun", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Temp_, "Temp_", "Temp", 75, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya, "Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya", "1", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nilai_Sisa_Buku_Awal, "Nilai_Sisa_Buku_Awal", "Nilai Sisa Buku Awal", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal_Siap_Untuk_Disusutkan, "Saldo_Awal_Siap_Untuk_Disusutkan", "Saldo Awal" & Enter1Baris & "Siap untuk" & Enter1Baris & "Disusutkan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Akumulasi_Tahun_Laporan, "Akumulasi_Tahun_Laporan", "2", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Akumulasi_Penyusutan_Sampai_Dengan, "Akumulasi_Penyusutan_Sampai_Dengan", "3", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nilai_Sisa_Buku_Akhir_Tahun, "Nilai_Sisa_Buku_Akhir_Tahun", "4", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Januari_, "Januari_", Bulan_Januari, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Februari_, "Februari_", Bulan_Februari, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Maret_, "Maret_", Bulan_Maret, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, April_, "April_", Bulan_April, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mei_, "Mei_", Bulan_Mei, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Juni_, "Juni_", Bulan_Juni, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Juli_, "Juli_", Bulan_Juli, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Agustus_, "Agustus_", Bulan_Agustus, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, September_, "September_", Bulan_September, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Oktober_, "Oktober_", Bulan_Oktober, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nopember_, "Nopember_", Bulan_Nopember, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Desember_, "Desember_", Bulan_Desember, 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Penyusutan_Tahun_Laporan_Yang_Sudah_Dijurnal, "Penyusutan_Tahun_Laporan_Yang_Sudah_Dijurnal", "", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NSB_Berdasarkan_Jurnal_Terakhir, "NSB_Berdasarkan_Jurnal_Terakhir", "", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Closing, "Kode_Closing", "Kode Closing", 57, FormatString, KiriTengah, KunciUrut, TerlihatKhususProgrammer)

        datagridUtama.MinWidth = 33

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
        pnl_SidebarKiri.Height = TinggiKonten
        pnl_SidebarKanan.Height = TinggiKonten
        pnl_Footer.Width = LebarKonten
        cmb_TahunLaporan.IsReadOnly = True
        cmb_FilterCOA.IsReadOnly = True
        cmb_FilterKelompokHarta.IsReadOnly = True
        cmb_FilterTahunPerolehan.IsReadOnly = True
        txt_JumlahAsset.IsReadOnly = True
        lbl_NotifikasiProses.Visibility = Visibility.Collapsed
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub


End Class
