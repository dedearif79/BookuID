Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_DaftarPenyusutanAssetTetap_X

    Dim JudulForm
    Public FungsiForm
    Public FungsiForm_GLOBAL_Rinci = "GLOBAL - Rinci"
    Public FungsiForm_GLOBAL_Rekap = "GLOBAL - Rekap"
    Public FungsiForm_DETAIL_Rinci = "DETAIL -Rinci"
    Public FungsiForm_DETAIL_Rekap = "DETAIL -Rekap"
    Dim TahunLaporan
    Dim TahunLaporanSebelumnya
    Dim TahunLaporanTerakhir
    Dim JumlahBaris

    Dim FilterAkun As String
    Dim FilterKelompokHarta As String
    Dim FilterTahunPerolehan As String

    'Tabel Temporary
    Dim DataTabelSementara As New DataTable

    'Variabel untuk Data Terseleksi :
    Dim BarisTerseleksi
    Dim Id_Terseleksi
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

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        KontenComboTahunLaporan()

        Select Case FungsiForm
            Case FungsiForm_GLOBAL_Rinci
                Sub_FungsiForm_Global_Rinci()
            Case FungsiForm_GLOBAL_Rekap
                Sub_FungsiForm_Global_Rekap()
            Case FungsiForm_DETAIL_Rinci
                Sub_FungsiForm_Detail_Rinci()
            Case FungsiForm_DETAIL_Rekap
                Sub_FungsiForm_Detail_Rekap()
        End Select

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            btn_JualAsset.Visible = True
            btn_DisposalAsset.Visible = True
            pnl_CRUD.Enabled = False
            btn_Import.Enabled = False
        Else
            btn_JualAsset.Visible = False
            btn_DisposalAsset.Visible = False
            pnl_CRUD.Enabled = True
            btn_Import.Enabled = True
        End If

        BeginInvoke(Sub() BersihkanSeleksi())

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()

        KontenComboTahunLaporan()

    End Sub

    Sub TampilkanData()

        Application.DoEvents()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)
        If TahunLaporan = TahunBukuAktif Then
            DataTabelUtama.Columns("Januari_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Februari_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Maret_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("April_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Mei_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Juni_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Juli_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Agustus_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("September_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Oktober_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Nopember_").DefaultCellStyle.ForeColor = WarnaPudar
            DataTabelUtama.Columns("Desember_").DefaultCellStyle.ForeColor = WarnaPudar
        Else
            DataTabelUtama.Columns("Januari_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Februari_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Maret_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("April_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Mei_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Juni_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Juli_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Agustus_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("September_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Oktober_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Nopember_").DefaultCellStyle.ForeColor = WarnaTegas
            DataTabelUtama.Columns("Desember_").DefaultCellStyle.ForeColor = WarnaTegas
        End If

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

        KesesuaianJurnal = True

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
                    KelompokHarta = Nothing 'Ini untuk mengisi kekosongan value. Jangan dihapus.
            End Select

            If KelompokHarta = KelompokHarta_Tanah Then
                TarifPenyusutan = "0 %"
            Else
                TarifPenyusutan = (100 / AmbilAngka(MasaManfaat)).ToString & " %"
            End If

            Divisi = dr.Item("Divisi")
            KodeDivisi = dr.Item("Kode_Divisi")

            TanggalPerolehan = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Perolehan"), 10)
            BulanPerolehan = AmbilAngka(Format(dr.Item("Tanggal_Perolehan"), "MM"))
            TahunPerolehan = AmbilAngka(Format(dr.Item("Tanggal_Perolehan"), "yyyy"))

            If FilterAkun = KodeAkun_Asset Or FilterAkun = "Semua" Then
                TambahBaris = True
                If FilterKelompokHarta = KelompokHarta Or FilterKelompokHarta = "Semua" Then
                    TambahBaris = True
                    If FilterTahunPerolehan = TahunPerolehan.ToString Or FilterTahunPerolehan = "Semua" Then
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

                If KelompokHarta = KelompokHarta_Tanah Then
                    SisaBulanPenyusutan = 999999999999999999
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

                Keterangan = dr.Item("Keterangan")
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
                                                   " WHERE Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
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

                    DataTabelUtama.Rows.Add(NomorUrut, IdAsset, KodeAsset, NomorPembelian, NamaAktiva, KodeAkun_Asset, NamaAkun_Asset, KodeAkun_BiayaPenyusutan, NamaAkun_BiayaPenyusutan,
                                            MasaManfaat, KelompokHarta, TarifPenyusutan, Divisi, KodeDivisi,
                                            TanggalPerolehan, HargaPerolehan, HargaPerolehanAwal, PenambahanPengurangan, HargaPerolehanAkhir,
                                            PenyusutanPerbulan, PenyusutanPertahun, JumlahBulanPenyusutanSampaiTahunSebelumnya,
                                            AkumulasiPenyusutanSampaiTahunSebelumnya, NilaiSisaBukuAwal, SaldoAwalSiapUntukDisusutkan,
                                            AkumulasiPenyusutanTahunLaporan, AkumulasiPenyusutanSampaiDengan, NilaiSisaBukuAkhirTahun,
                                            Januari, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, Nopember, Desember,
                                            PenyusutanTahunLaporanYangSudahDijurnal, NilaiSisaBuku_BerdasarkanJurnalTerakhir, Keterangan, KodeClosing)

                    Dim IndexBaris = NomorUrut - 1
                    If TahunLaporan = TahunIni Then
                        DataTabelUtama.Item("Januari_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Februari_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Maret_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("April_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Mei_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Juni_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Juli_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Agustus_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("September_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Oktober_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Nopember_", IndexBaris).Style.ForeColor = WarnaPudar
                        DataTabelUtama.Item("Desember_", IndexBaris).Style.ForeColor = WarnaPudar
                    End If

                    'Cek Data Jurnal (Untuk keperluan pewarnaan) :
                    AksesDatabase_Transaksi(Buka)
                    cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                                   " WHERE Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
                                                   " AND Bundelan LIKE '%" & KodeAsset & "%' " &
                                                   " AND COA = '" & KodeAkun_BiayaPenyusutan & "' ", KoneksiDatabaseTransaksi)
                    drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
                    Do While drCEKJURNAL.Read
                        Dim TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
                        Dim BulanJurnalPenyusutan = AmbilAngka(Format(TanggalJurnalPenyusutan, "MM"))
                        If AmbilAngka(BulanJurnalPenyusutan) = 1 Then DataTabelUtama.Item("Januari_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 2 Then DataTabelUtama.Item("Februari_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 3 Then DataTabelUtama.Item("Maret_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 4 Then DataTabelUtama.Item("April_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 5 Then DataTabelUtama.Item("Mei_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 6 Then DataTabelUtama.Item("Juni_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 7 Then DataTabelUtama.Item("Juli_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 8 Then DataTabelUtama.Item("Agustus_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 9 Then DataTabelUtama.Item("September_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 10 Then DataTabelUtama.Item("Oktober_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 11 Then DataTabelUtama.Item("Nopember_", IndexBaris).Style.ForeColor = WarnaTegas
                        If AmbilAngka(BulanJurnalPenyusutan) = 12 Then DataTabelUtama.Item("Desember_", IndexBaris).Style.ForeColor = WarnaTegas
                    Loop
                    AksesDatabase_Transaksi(Tutup)

                    If AssetSudahClosing = True Then DataTabelUtama.Rows(IndexBaris).DefaultCellStyle.ForeColor = WarnaPudar

                End If

            End If

            Application.DoEvents()

        Loop 'Ujung Loop Tampilan

        AksesDatabase_General(Tutup)

        JumlahBaris = DataTabelUtama.RowCount

        If JumlahBaris > 0 Then
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Nothing, Nothing, "      J U M L A H", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                  Nothing, Nothing, Nothing, Nothing, Nothing,
                                  Nothing, Jml_HargaPerolehan, Jml_HargaPerolehanAwal, Jml_PenambahanPengurangan, Jml_HargaPerolehanAkhir,
                                  Nothing, Nothing, Nothing,
                                  Jml_AkumulasiPenyusutanSampaiTahunSebelumnya, Jml_NilaiSisaBukuAwal, Jml_SaldoAwalSiapUntukDisusutkan,
                                  Jml_AkumulasiSatutahun, Jml_AkumulasiPenyusutanSampaiDengan, Jml_NilaiSisaBukuAkhirTahun,
                                  Jml_Januari, Jml_Februari, Jml_Maret, Jml_April, Jml_Mei, Jml_Juni,
                                  Jml_Juli, Jml_Agustus, Jml_September, Jml_Oktober, Jml_Nopember, Jml_Desember)
        End If

        'Styling Baris Jumlah :
        If TahunLaporan = TahunIni Then
            Dim IndexBarisPenjumlahan = JumlahBaris + 1
            If AmbilAngka(BulanIni) > 1 Then DataTabelUtama.Item("Januari_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 2 Then DataTabelUtama.Item("Februari_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 3 Then DataTabelUtama.Item("Maret_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 4 Then DataTabelUtama.Item("April_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 5 Then DataTabelUtama.Item("Mei_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 6 Then DataTabelUtama.Item("Juni_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 7 Then DataTabelUtama.Item("Juli_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 8 Then DataTabelUtama.Item("Agustus_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 9 Then DataTabelUtama.Item("September_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 10 Then DataTabelUtama.Item("Oktober_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            If AmbilAngka(BulanIni) > 11 Then DataTabelUtama.Item("Nopember_", IndexBarisPenjumlahan).Style.ForeColor = WarnaTegas
            'Desember ga perlu Styling.
        End If

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        btn_LihatJurnal.Enabled = False
        btn_LihatInvoice.Enabled = False
        btn_Posting.Enabled = False
        btn_JualAsset.Enabled = False
        btn_DisposalAsset.Enabled = False
    End Sub

    Sub KontenComboTahunLaporan()
        If AmbilAngka(BulanIni) > 1 Then
            TahunLaporanTerakhir = TahunIni
        Else
            TahunLaporanTerakhir = TahunIni - 1
        End If
        cmb_TahunLaporan.Items.Clear()
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 10)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 9)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 8)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 7)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 6)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 5)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 4)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 3)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 2)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir - 1)
        cmb_TahunLaporan.Items.Add(TahunLaporanTerakhir)
        cmb_TahunLaporan.Text = TahunBukuAktif
    End Sub

    Sub KontenComboFilterCOA()
        cmb_FilterCOA.Items.Clear()
        cmb_FilterCOA.Items.Add("Semua")
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
        cmb_FilterCOA.Text = "Semua"
    End Sub

    Sub KontenComboFilterKelompokHarta()
        cmb_FilterKelompokHarta.Items.Clear()
        cmb_FilterKelompokHarta.Items.Add("Semua")
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_1)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_2)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_3)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_4)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_Tanah)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_BangunanPermanen)
        cmb_FilterKelompokHarta.Items.Add(KelompokHarta_BangunanTidakPermanen)
        cmb_FilterKelompokHarta.Text = "Semua"
    End Sub

    Sub KontenComboFilterTahunPerolehan()
        If AmbilAngka(BulanIni) > 1 Then
            TahunLaporanTerakhir = TahunIni
        Else
            TahunLaporanTerakhir = TahunIni - 1
        End If
        cmb_FilterTahunPerolehan.Items.Clear()
        cmb_FilterTahunPerolehan.Items.Add("Semua")
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 10)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 9)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 8)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 7)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 6)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 5)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 4)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 3)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 2)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir - 1)
        cmb_FilterTahunPerolehan.Items.Add(TahunLaporanTerakhir)
        cmb_FilterTahunPerolehan.Text = "Semua"
    End Sub

    Sub ResetFilterData()
        KontenComboFilterCOA()
        KontenComboFilterKelompokHarta()
        KontenComboFilterTahunPerolehan()
        TampilkanData()
    End Sub


    Sub Sub_FungsiForm_Global_Rinci()

        'Fungsi Form dan Status Tombol
        FungsiForm = FungsiForm_GLOBAL_Rinci
        JudulForm = "Daftar Penyusutan Asset Tetap [Global]"
        ResetFilterData()
        cmb_TahunLaporan.Text = TahunBukuAktif
        btn_DetailGlobal.Text = "Detail"
        btn_Rekap.Text = "Rekap Total"
        btn_LihatJurnal.Visible = False

        'Tampilkan Beberapa Objek :
        grb_FilterData.Visible = True
        btn_KodeDivisi.Visible = True
        btn_Tambah.Visible = True
        btn_Edit.Visible = True
        btn_Hapus.Visible = True
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            btn_JualAsset.Visible = True
            btn_DisposalAsset.Visible = True
        End If

        'Sembunyikan Beberapa Objek :
        lbl_Tahun.Visible = False
        cmb_TahunLaporan.Visible = False
        btn_Posting.Visible = False

        'Sembunyikan Semua Kolom :
        SembunyikanSemuaKolomTabel()

        'Kolom-kolom yang Ditampilkan :
        DataTabelUtama.Columns("Nomor_Urut").Visible = True
        DataTabelUtama.Columns("Nama_Aktiva").Visible = True
        DataTabelUtama.Columns("COA_Asset").Visible = True
        DataTabelUtama.Columns("Nama_Akun_Asset").Visible = True
        DataTabelUtama.Columns("Masa_Manfaat").Visible = True
        DataTabelUtama.Columns("Kelompok_Harta").Visible = True
        DataTabelUtama.Columns("Tarif_Penyusutan").Visible = True
        DataTabelUtama.Columns("Divisi_").Visible = True
        DataTabelUtama.Columns("Kode_Divisi").Visible = True
        DataTabelUtama.Columns("Tanggal_Perolehan").Visible = True
        DataTabelUtama.Columns("Harga_Perolehan_Awal").Visible = True
        DataTabelUtama.Columns("Penambahan_Pengurangan").Visible = True
        DataTabelUtama.Columns("Harga_Perolehan_Akhir").Visible = True
        DataTabelUtama.Columns("Nilai_Sisa_Buku_Awal").Visible = True
        DataTabelUtama.Columns("Saldo_Awal_Siap_Untuk_Disusutkan").Visible = True
        DataTabelUtama.Columns("Akumulasi_Tahun_Laporan").Visible = True
        DataTabelUtama.Columns("Akumulasi_Penyusutan_Sampai_Dengan").Visible = True
        DataTabelUtama.Columns("Nilai_Sisa_Buku_Akhir_Tahun").Visible = True
        DataTabelUtama.Columns("Keterangan_").Visible = True

        BersihkanSeleksi()

    End Sub

    Sub Sub_FungsiForm_Global_Rekap()

        'Fungsi Form dan Status Tombol
        FungsiForm = FungsiForm_GLOBAL_Rekap
        btn_DetailGlobal.Text = "Detail"
        btn_Rekap.Text = "<< Kembali"

        'Tampilkan Beberapa Objek :
        btn_JualAsset.Visible = False
        btn_DisposalAsset.Visible = False

        'Sembunyikan Beberapa Objek :
        grb_FilterData.Visible = False

        'Sembunyikan Semua Kolom :
        SembunyikanSemuaKolomTabel()

        'Kolom-kolom yang Ditampilkan :
        DataTabelUtama.Columns("Nomor_Urut").Visible = True
        DataTabelUtama.Columns("Nama_Akun_Asset").Visible = True
        DataTabelUtama.Columns("Harga_Perolehan").Visible = True
        'DataTabelUtama.Columns("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya").Visible = True
        DataTabelUtama.Columns("Nilai_Sisa_Buku_Awal").Visible = True
        DataTabelUtama.Columns("Akumulasi_Tahun_Laporan").Visible = True
        DataTabelUtama.Columns("Akumulasi_Penyusutan_Sampai_Dengan").Visible = True
        DataTabelUtama.Columns("Nilai_Sisa_Buku_Akhir_Tahun").Visible = True

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
        Dim NamaAkun_Asset = Nothing
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
        Dim COAAsset_Terindeks = Nothing

        Dim SusurAkun = AwalAkunAssetTetap

        Do While SusurAkun <= AkhirAkunAssetTetap

            Rekap_HargaPerolehan = 0
            Rekap_AkumulasiPenyusutanAwal = 0
            Rekap_NilaiSisaBukuAwal = 0
            Rekap_PenyusutanTahunLaporan = 0
            Rekap_AkumulasiPenyusutanAkhir = 0
            Rekap_NilaiSisaBukuAkhir = 0


            For Each row As DataGridViewRow In DataTabelUtama.Rows

                COAAsset_Terindeks = row.Cells("COA_Asset").Value

                If AmbilAngka(COAAsset_Terindeks) = SusurAkun Then
                    NamaAkun_Asset = row.Cells("Nama_Akun_Asset").Value
                    HargaPerolehan = AmbilAngka(row.Cells("Harga_Perolehan").Value)
                    AkumulasiPenyusutanAwal = AmbilAngka(row.Cells("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya").Value)
                    NilaiSisaBukuAwal = AmbilAngka(row.Cells("Nilai_Sisa_Buku_Awal").Value)
                    PenyusutanTahunLaporan = AmbilAngka(row.Cells("Akumulasi_Tahun_Laporan").Value)
                    AkumulasiPenyusutanAkhir = AmbilAngka(row.Cells("Akumulasi_Penyusutan_Sampai_Dengan").Value)
                    NilaiSisaBukuAkhir = AmbilAngka(row.Cells("Nilai_Sisa_Buku_Akhir_Tahun").Value)
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

        DataTabelUtama.Rows.Clear()

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
            DataTabelUtama.Rows.Add(NomorUrut, Nothing, Nothing, Nothing, Nothing, Nothing, NamaAkun_Asset, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Rekap_HargaPerolehan, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing,
                                    Rekap_AkumulasiPenyusutanAwal, Rekap_NilaiSisaBukuAwal, Nothing,
                                    Rekap_PenyusutanTahunLaporan, Rekap_AkumulasiPenyusutanAkhir, Rekap_NilaiSisaBukuAkhir)
        Next

        JumlahBaris = DataTabelUtama.RowCount

        DataTabelUtama.Rows.Add()
        DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "      J U M L A H", Nothing, Nothing,
                                Nothing, Nothing, Nothing, Nothing, Nothing,
                                Nothing, Total_HargaPerolehan, Nothing, Nothing, Nothing,
                                Nothing, Nothing, Nothing,
                                Total_AkumulasiPenyusutanAwal, Total_NilaiSisaBukuAwal, Nothing,
                                Total_PenyusutanTahunLaporan, Total_AkumulasiPenyusutanAkhir, Total_NilaiSisaBukuAkhir)

        BersihkanSeleksi()

    End Sub

    Sub Sub_FungsiForm_Detail_Rinci()

        'Fungsi Form dan Status Tombol
        FungsiForm = FungsiForm_DETAIL_Rinci
        JudulForm = "Daftar Penyusutan Asset - Tahun " & TahunLaporan
        ResetFilterData()
        btn_DetailGlobal.Text = "Global"
        btn_Rekap.Text = "Rekap Penyusutan"
        lbl_Tahun.Enabled = True
        cmb_TahunLaporan.Enabled = True

        'Tampilan Beberapa Objek :
        cmb_TahunLaporan.Visible = True
        grb_FilterData.Visible = True
        lbl_Tahun.Visible = True

        'Sembunyikan Beberapa Objek :
        btn_JualAsset.Visible = False
        btn_DisposalAsset.Visible = False
        btn_LihatJurnal.Visible = False
        btn_Posting.Visible = False
        btn_KodeDivisi.Visible = False
        btn_Tambah.Visible = False
        btn_Edit.Visible = False
        btn_Hapus.Visible = False

        'Sembunyikan Semua Kolom :
        SembunyikanSemuaKolomTabel()

        'Kolom-kolom yang Ditampilkan :
        DataTabelUtama.Columns("Nomor_Urut").Visible = True
        DataTabelUtama.Columns("Nama_Aktiva").Visible = True
        DataTabelUtama.Columns("Nama_Akun_Asset").Visible = True
        DataTabelUtama.Columns("Masa_Manfaat").Visible = True
        DataTabelUtama.Columns("Kelompok_Harta").Visible = True
        DataTabelUtama.Columns("Tarif_Penyusutan").Visible = True
        DataTabelUtama.Columns("Divisi_").Visible = True
        DataTabelUtama.Columns("Tanggal_Perolehan").Visible = True
        DataTabelUtama.Columns("Harga_Perolehan_Awal").Visible = True
        DataTabelUtama.Columns("Penambahan_Pengurangan").Visible = True
        DataTabelUtama.Columns("Harga_Perolehan_Akhir").Visible = True
        DataTabelUtama.Columns("Nilai_Sisa_Buku_Awal").Visible = True
        DataTabelUtama.Columns("Saldo_Awal_Siap_Untuk_Disusutkan").Visible = True
        DataTabelUtama.Columns("Akumulasi_Tahun_Laporan").Visible = True
        DataTabelUtama.Columns("Akumulasi_Penyusutan_Sampai_Dengan").Visible = True
        DataTabelUtama.Columns("Nilai_Sisa_Buku_Akhir_Tahun").Visible = True
        DataTabelUtama.Columns("Januari_").Visible = True
        DataTabelUtama.Columns("Februari_").Visible = True
        DataTabelUtama.Columns("Maret_").Visible = True
        DataTabelUtama.Columns("April_").Visible = True
        DataTabelUtama.Columns("Mei_").Visible = True
        DataTabelUtama.Columns("Juni_").Visible = True
        DataTabelUtama.Columns("Juli_").Visible = True
        DataTabelUtama.Columns("Agustus_").Visible = True
        DataTabelUtama.Columns("September_").Visible = True
        DataTabelUtama.Columns("Oktober_").Visible = True
        DataTabelUtama.Columns("Nopember_").Visible = True
        DataTabelUtama.Columns("Desember_").Visible = True
        DataTabelUtama.Columns("Keterangan_").Visible = True

        BersihkanSeleksi()

    End Sub

    Sub Sub_FungsiForm_Detail_Rekap()

        'Fungsi Form dan Status Tombol
        FungsiForm = FungsiForm_DETAIL_Rekap
        btn_DetailGlobal.Text = "Global"
        btn_Rekap.Text = "<< Kembali"
        lbl_Tahun.Enabled = False
        cmb_TahunLaporan.Enabled = False

        'Tampilkan Beberapa Objek :
        btn_Posting.Visible = True
        btn_LihatJurnal.Visible = True

        'Sembunyikan beberapa Objek :
        grb_FilterData.Visible = False
        btn_JualAsset.Visible = False
        btn_DisposalAsset.Visible = False

        'Sembunyikan Semua Kolom :
        SembunyikanSemuaKolomTabel()

        'Kolom-kolom yang Ditampilkan :
        DataTabelUtama.Columns("Nomor_Urut").Visible = True
        DataTabelUtama.Columns("COA_Biaya_Penyusutan").Visible = True
        DataTabelUtama.Columns("Nama_Akun_Biaya_Penyusutan").Visible = True
        DataTabelUtama.Columns("Akumulasi_Tahun_Laporan").Visible = True
        DataTabelUtama.Columns("Januari_").Visible = True
        DataTabelUtama.Columns("Februari_").Visible = True
        DataTabelUtama.Columns("Maret_").Visible = True
        DataTabelUtama.Columns("April_").Visible = True
        DataTabelUtama.Columns("Mei_").Visible = True
        DataTabelUtama.Columns("Juni_").Visible = True
        DataTabelUtama.Columns("Juli_").Visible = True
        DataTabelUtama.Columns("Agustus_").Visible = True
        DataTabelUtama.Columns("September_").Visible = True
        DataTabelUtama.Columns("Oktober_").Visible = True
        DataTabelUtama.Columns("Nopember_").Visible = True
        DataTabelUtama.Columns("Desember_").Visible = True

        'Bikin Tabel Temporary :
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
        Dim KodeAkun_Asset = Nothing
        Dim NamaAkun_BiayaPenyusutan = Nothing
        Dim KodeAkun_BiayaPenyusutan = Nothing
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
        Dim COAAsset_Terindeks = Nothing
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

            For Each row As DataGridViewRow In DataTabelUtama.Rows

                COAAsset_Terindeks = row.Cells("COA_Asset").Value

                If AmbilAngka(COAAsset_Terindeks) = SusurAkun Then
                    TambahBaris = True
                    JumlahBarisAssetTerbundel += 1
                    KodeAkun_Asset = row.Cells("COA_Asset").Value
                    KodeAkun_BiayaPenyusutan = row.Cells("COA_Biaya_Penyusutan").Value
                    If KodeAkun_BiayaPenyusutan = Kosongan Then TambahBaris = False
                    NamaAkun_BiayaPenyusutan = row.Cells("Nama_Akun_Biaya_Penyusutan").Value
                    AkumulasiPenyusutanTahunLaporan = row.Cells("Akumulasi_Tahun_Laporan").Value
                    Januari = AmbilAngka(row.Cells("Januari_").Value)
                    Februari = AmbilAngka(row.Cells("Februari_").Value)
                    Maret = AmbilAngka(row.Cells("Maret_").Value)
                    April = AmbilAngka(row.Cells("April_").Value)
                    Mei = AmbilAngka(row.Cells("Mei_").Value)
                    Juni = AmbilAngka(row.Cells("Juni_").Value)
                    Juli = AmbilAngka(row.Cells("Juli_").Value)
                    Agustus = AmbilAngka(row.Cells("Agustus_").Value)
                    September = AmbilAngka(row.Cells("September_").Value)
                    Oktober = AmbilAngka(row.Cells("Oktober_").Value)
                    Nopember = AmbilAngka(row.Cells("Nopember_").Value)
                    Desember = AmbilAngka(row.Cells("Desember_").Value)
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

        DataTabelUtama.Rows.Clear()

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

            DataTabelUtama.Rows.Add(NomorUrut, Nothing, Nothing, Nothing, Nothing, KodeAkun_Asset, Nothing, KodeAkun_BiayaPenyusutan, NamaAkun_BiayaPenyusutan,
                                    Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing,
                                    Nothing, Nothing, Nothing,
                                    Rekap_AkumulasiPenyusutanTahunLaporan, Nothing, Nothing,
                                    Rekap_Januari, Rekap_Februari, Rekap_Maret, Rekap_April, Rekap_Mei, Rekap_Juni,
                                    Rekap_Juli, Rekap_Agustus, Rekap_September, Rekap_Oktober, Rekap_Nopember, Rekap_Desember)

            'Cek Data Jurnal :
            If TahunLaporan = TahunBukuAktif Then
                Dim IndexBaris = NomorUrut - 1
                Dim AkumulasiPenyusutanTahunLaporan_BerdasarkanJurnal As Int64 = 0
                AksesDatabase_Transaksi(Buka)
                cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                               " WHERE COA = '" & KodeAkun_BiayaPenyusutan & "' ", KoneksiDatabaseTransaksi)
                'Kenapa Query Di sini tidak menggunakan kriteria BUNDELAN..? Karena ini tampilan Detail Rekap..!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
                Do While drCEKJURNAL.Read
                    Dim TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
                    Dim BulanJurnalPenyusutan = Format(TanggalJurnalPenyusutan, "MM")
                    If AmbilAngka(BulanJurnalPenyusutan) = 1 Then DataTabelUtama.Item("Januari_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 2 Then DataTabelUtama.Item("Februari_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 3 Then DataTabelUtama.Item("Maret_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 4 Then DataTabelUtama.Item("April_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 5 Then DataTabelUtama.Item("Mei_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 6 Then DataTabelUtama.Item("Juni_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 7 Then DataTabelUtama.Item("Juli_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 8 Then DataTabelUtama.Item("Agustus_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 9 Then DataTabelUtama.Item("September_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 10 Then DataTabelUtama.Item("Oktober_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 11 Then DataTabelUtama.Item("Nopember_", IndexBaris).Style.ForeColor = WarnaTegas
                    If AmbilAngka(BulanJurnalPenyusutan) = 12 Then DataTabelUtama.Item("Desember_", IndexBaris).Style.ForeColor = WarnaTegas
                    AkumulasiPenyusutanTahunLaporan_BerdasarkanJurnal += AmbilAngka(drCEKJURNAL.Item("Jumlah_Debet"))
                Loop
                AksesDatabase_Transaksi(Tutup)
                If Rekap_AkumulasiPenyusutanTahunLaporan <> AkumulasiPenyusutanTahunLaporan_BerdasarkanJurnal Then KesesuaianJurnal = False
            End If
        Next

        JumlahBaris = DataTabelUtama.RowCount

        DataTabelUtama.Rows.Add()
        DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "      J U M L A H", Nothing, Nothing,
                                Nothing, Nothing, Nothing, Nothing, Nothing,
                                Nothing, Nothing, Nothing, Nothing, Nothing,
                                Nothing, Nothing, Nothing,
                                Nothing, Nothing, Nothing,
                                Nothing, Nothing, Nothing,
                                Total_Januari, Total_Februari, Total_Maret, Total_April, Total_Mei, Total_Juni,
                                Total_Juli, Total_Agustus, Total_September, Total_Oktober, Total_Nopember, Total_Desember)

        BersihkanSeleksi()

    End Sub

    Sub SembunyikanSemuaKolomTabel()

        DataTabelUtama.Columns("Nomor_Urut").Visible = False
        DataTabelUtama.Columns("ID_").Visible = False
        'DataTabelUtama.Columns("Kode_Asset").Visible = False
        DataTabelUtama.Columns("Nama_Aktiva").Visible = False
        DataTabelUtama.Columns("COA_Asset").Visible = False
        DataTabelUtama.Columns("Nama_Akun_Asset").Visible = False
        DataTabelUtama.Columns("COA_Biaya_Penyusutan").Visible = False
        DataTabelUtama.Columns("Nama_Akun_Biaya_Penyusutan").Visible = False
        DataTabelUtama.Columns("Masa_Manfaat").Visible = False
        DataTabelUtama.Columns("Kelompok_Harta").Visible = False
        DataTabelUtama.Columns("Tarif_Penyusutan").Visible = False
        DataTabelUtama.Columns("Divisi_").Visible = False
        DataTabelUtama.Columns("Kode_Divisi").Visible = False
        DataTabelUtama.Columns("Tanggal_Perolehan").Visible = False
        DataTabelUtama.Columns("Harga_Perolehan").Visible = False
        DataTabelUtama.Columns("Harga_Perolehan_Awal").Visible = False
        DataTabelUtama.Columns("Penambahan_Pengurangan").Visible = False
        DataTabelUtama.Columns("Harga_Perolehan_Akhir").Visible = False
        DataTabelUtama.Columns("Penyusutan_Perbulan").Visible = False
        DataTabelUtama.Columns("Penyusutan_Pertahun").Visible = False
        DataTabelUtama.Columns("Temp_").Visible = False
        DataTabelUtama.Columns("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya").Visible = False
        DataTabelUtama.Columns("Nilai_Sisa_Buku_Awal").Visible = False
        DataTabelUtama.Columns("Saldo_Awal_Siap_Untuk_Disusutkan").Visible = False
        DataTabelUtama.Columns("Akumulasi_Tahun_Laporan").Visible = False
        DataTabelUtama.Columns("Akumulasi_Penyusutan_Sampai_Dengan").Visible = False
        DataTabelUtama.Columns("Nilai_Sisa_Buku_Akhir_Tahun").Visible = False
        DataTabelUtama.Columns("Januari_").Visible = False
        DataTabelUtama.Columns("Februari_").Visible = False
        DataTabelUtama.Columns("Maret_").Visible = False
        DataTabelUtama.Columns("April_").Visible = False
        DataTabelUtama.Columns("Mei_").Visible = False
        DataTabelUtama.Columns("Juni_").Visible = False
        DataTabelUtama.Columns("Juli_").Visible = False
        DataTabelUtama.Columns("Agustus_").Visible = False
        DataTabelUtama.Columns("September_").Visible = False
        DataTabelUtama.Columns("Oktober_").Visible = False
        DataTabelUtama.Columns("Nopember_").Visible = False
        DataTabelUtama.Columns("Desember_").Visible = False
        DataTabelUtama.Columns("Keterangan_").Visible = False

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        Sub_FungsiForm_Global_Rinci()
        RefreshTampilanData()
    End Sub

    Private Sub btn_Rekap_Click(sender As Object, e As EventArgs) Handles btn_Rekap.Click
        Select Case FungsiForm
            Case FungsiForm_GLOBAL_Rinci
                Sub_FungsiForm_Global_Rekap()
            Case FungsiForm_GLOBAL_Rekap
                TampilkanData()
                Sub_FungsiForm_Global_Rinci()
            Case FungsiForm_DETAIL_Rinci
                Sub_FungsiForm_Detail_Rekap()
            Case FungsiForm_DETAIL_Rekap
                TampilkanData()
                Sub_FungsiForm_Detail_Rinci()
        End Select
        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm
    End Sub

    Private Sub btn_DetailGlobal_Click(sender As Object, e As EventArgs) Handles btn_DetailGlobal.Click
        cmb_FilterCOA.Text = "Semua"
        KontenComboFilterCOA()
        Select Case FungsiForm
            Case FungsiForm_GLOBAL_Rinci
                Sub_FungsiForm_Detail_Rinci()
            Case FungsiForm_GLOBAL_Rekap
                Sub_FungsiForm_Detail_Rinci()
            Case FungsiForm_DETAIL_Rinci
                Sub_FungsiForm_Global_Rinci()
            Case FungsiForm_DETAIL_Rekap
                Sub_FungsiForm_Global_Rinci()
        End Select
        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm
    End Sub

    Private Sub btn_KodeDivisi_Click(sender As Object, e As EventArgs) Handles btn_KodeDivisi.Click
        frm_KodeDivisi.ShowDialog()
    End Sub

    Private Sub cmb_TahunLaporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TahunLaporan.SelectedIndexChanged
        'cmb_TahunLaporan_TextChanged(sender, e)
    End Sub
    Private Sub cmb_TahunLaporan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_TahunLaporan.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub cmb_TahunLaporan_TextChanged(sender As Object, e As EventArgs) Handles cmb_TahunLaporan.TextChanged
        TahunLaporan = AmbilAngka(cmb_TahunLaporan.Text)
        If TahunLaporan < 1000 Then Return
        If TahunLaporan = Nothing Then Return
        TahunLaporanSebelumnya = TahunLaporan - 1
        If TahunLaporan > TahunLaporanTerakhir Then
            MsgBox("Belum ada laporan untuk tahun " & TahunLaporan & ".")
            cmb_TahunLaporan.Text = Nothing
            Return
        End If
        DataTabelUtama.Columns("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya").HeaderText = "Akumulasi Penyusutan s.d. " & TahunLaporanSebelumnya
        DataTabelUtama.Columns("Akumulasi_Tahun_Laporan").HeaderText = "Penyusutan Tahun " & TahunLaporan
        DataTabelUtama.Columns("Akumulasi_Penyusutan_Sampai_Dengan").HeaderText = "Akumulasi Penyusutan s.d. " & TahunLaporan
        ResetFilterData()
        If FungsiForm = FungsiForm_GLOBAL_Rinci Or FungsiForm = FungsiForm_GLOBAL_Rekap Then JudulForm = "Daftar Penyusutan Asset Tetap [Global]"
        If FungsiForm = FungsiForm_DETAIL_Rinci Or FungsiForm = FungsiForm_DETAIL_Rekap Then JudulForm = "Daftar Penyusutan Asset Tetap - Tahun " & TahunLaporan
        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm
    End Sub

    Private Sub cmb_FilterCOA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_FilterCOA.SelectedIndexChanged
        cmb_FilterCOA_TextChanged(sender, e)
    End Sub
    Private Sub cmb_FilterCOA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_FilterCOA.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_FilterCOA_TextChanged(sender As Object, e As EventArgs) Handles cmb_FilterCOA.TextChanged
        FilterAkun = Microsoft.VisualBasic.Left(cmb_FilterCOA.Text, JumlahDigitCOA)
    End Sub

    Private Sub cmb_FilterKelompokHarta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_FilterKelompokHarta.SelectedIndexChanged
        cmb_FilterKelompokHarta_TextChanged(sender, e)
    End Sub
    Private Sub cmb_FilterKelompokHarta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_FilterKelompokHarta.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_FilterKelompokHarta_TextChanged(sender As Object, e As EventArgs) Handles cmb_FilterKelompokHarta.TextChanged
        FilterKelompokHarta = cmb_FilterKelompokHarta.Text
    End Sub

    Private Sub cmb_FilterTahunPerolehan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_FilterTahunPerolehan.SelectedIndexChanged
        cmb_FilterTahunPerolehan_TextChanged(sender, e)
    End Sub
    Private Sub cmb_FilterTahunPerolehan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_FilterTahunPerolehan.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub cmb_FilterTahunPerolehan_TextChanged(sender As Object, e As EventArgs) Handles cmb_FilterTahunPerolehan.TextChanged
        FilterTahunPerolehan = cmb_FilterTahunPerolehan.Text.ToString
    End Sub

    Private Sub btn_ResetFilterData_Click(sender As Object, e As EventArgs) Handles btn_ResetFilterData.Click
        ResetFilterData()
    End Sub

    Private Sub btn_FilterData_Click(sender As Object, e As EventArgs) Handles btn_FilterData.Click
        TampilkanData()
    End Sub



    Private Sub btn_Import_Click(sender As Object, e As EventArgs) Handles btn_Import.Click

        Pilihan = MessageBox.Show("Penggunaan fitur ini akan mengakibatkan Data Jurnal yang telah diposting berkaitan dengan Data Asset yang ada di tabel ini akan dihapus seluruhnya." & Enter2Baris &
                                  "Tidak perlu khawatir, karena Anda bisa mempostingnya lagi." & Enter2Baris &
                                  "Lanjutkan import..?",
                                  "PERHATIAN..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        'Hapus Data Jurnal Terlebih Dahulu Sebeleum Import :
        Dim ProsesHapus As Boolean = True 'Sudah benar True, jangan diganti False
        Dim JumlahJurnalDihapus = 0
        Dim cmdSUSURJURNAL As OdbcCommand
        Dim drSUSURJURNAL As OdbcDataReader
        Dim cmdHAPUSJURNAL As OdbcCommand
        Dim NomorJV_HarusDihapus = Nothing
        Dim KodeAkun_BiayaPenyusutan = Nothing
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE (COA BETWEEN 62000 AND 62299) OR (COA BETWEEN 52200 AND 52900) ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        AksesDatabase_Transaksi(Buka)
        Do While dr.Read
            KodeAkun_BiayaPenyusutan = dr.Item("COA")
            cmdSUSURJURNAL = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                                             " WHERE COA = '" & KodeAkun_BiayaPenyusutan & "' ",
                                             KoneksiDatabaseTransaksi)
            drSUSURJURNAL = cmdSUSURJURNAL.ExecuteReader
            Do While drSUSURJURNAL.Read
                NomorJV_HarusDihapus = drSUSURJURNAL.Item("Nomor_JV")
                cmdHAPUSJURNAL = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_HarusDihapus & "' ", KoneksiDatabaseTransaksi)
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
                MsgBox("Belum ada Jurnal yang perlu dihapus pada event ini." _
                       & Enter2Baris & "Silakan lanjurkan proses import.")
            End If
        Else
            MsgBox("Import dibatalakan..!!!" & Enter2Baris & teks_SilakanCobaLagi_Database)
            Return
        End If

        X_frm_ProgressImportDataAsset.ShowDialog()
        If StatusPosting = Status_BATAL Then
            MsgBox("Proses posting telah dibatalkan seluruhnya pada event ini.")
        End If

    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub



    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        'frm_InputDataAsset.ResetForm()
        'frm_InputDataAsset.FungsiForm = FungsiForm_TAMBAH
        'frm_InputDataAsset.JalurMasuk = Halaman_DATAASSETTETAP
        'frm_InputDataAsset.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        'frm_InputDataAsset.ResetForm()
        'frm_InputDataAsset.FungsiForm = FungsiForm_EDIT
        'frm_InputDataAsset.JalurMasuk = Halaman_DATAASSETTETAP
        'frm_InputDataAsset.IdAsset = Id_Terseleksi
        'frm_InputDataAsset.cmb_KelompokHarta.Text = KelompokHarta_Terseleksi
        'frm_InputDataAsset.txt_KodeAsset.Text = KodeAsset_Terseleksi
        'frm_InputDataAsset.KodeAsset_SebelumDiedit = KodeAsset_Terseleksi
        'frm_InputDataAsset.txt_NamaAktiva.Text = NamaAktiva_Terseleksi
        'frm_InputDataAsset.txt_COA_Asset.Text = COAAsset_Terseleksi
        'frm_InputDataAsset.txt_NamaAkun_Asset.Text = NamaAkunAsset_Terseleksi
        'frm_InputDataAsset.txt_COA_BiayaPenyusutan.Text = COABiayaPenyusutan_Terseleksi
        'frm_InputDataAsset.txt_NamaAkun_BiayaPenyusutan.Text = NamaAkunBiayaPenyusutan_Terseleksi
        'frm_InputDataAsset.cmb_Divisi.Text = Divisi_Terseleksi
        'frm_InputDataAsset.dtp_TanggalPerolehan.Value = TanggalPerolehan_Terseleksi
        'frm_InputDataAsset.txt_HargaPerolehan.Text = HargaPerolehan_Terseleksi
        'frm_InputDataAsset.txt_Keterangan.Text = Keterangan_Terseleksi
        'frm_InputDataAsset.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click
        FiturBelumBisaDigunakan()
        Return
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        Id_Terseleksi = DataTabelUtama.Item("ID_", BarisTerseleksi).Value
        KodeAsset_Terseleksi = DataTabelUtama.Item("Kode_Asset", BarisTerseleksi).Value
        NomorPembelian_Terseleksi = DataTabelUtama.Item("Nomor_Pembelian", BarisTerseleksi).Value
        NamaAktiva_Terseleksi = DataTabelUtama.Item("Nama_Aktiva", BarisTerseleksi).Value
        COAAsset_Terseleksi = DataTabelUtama.Item("COA_Asset", BarisTerseleksi).Value
        NamaAkunAsset_Terseleksi = DataTabelUtama.Item("Nama_Akun_Asset", BarisTerseleksi).Value
        COABiayaPenyusutan_Terseleksi = DataTabelUtama.Item("COA_Biaya_Penyusutan", BarisTerseleksi).Value
        NamaAkunBiayaPenyusutan_Terseleksi = DataTabelUtama.Item("Nama_Akun_Biaya_Penyusutan", BarisTerseleksi).Value
        COAAkumulasiPenyusutan_Terseleksi = PenentuanCOA_AkumulasiPenyusutan(COAAsset_Terseleksi)
        KelompokHarta_Terseleksi = DataTabelUtama.Item("Kelompok_Harta", BarisTerseleksi).Value
        Divisi_Terseleksi = DataTabelUtama.Item("Kode_Divisi", BarisTerseleksi).Value & " - " & DataTabelUtama.Item("Divisi_", BarisTerseleksi).Value
        TanggalPerolehan_Terseleksi = DataTabelUtama.Item("Tanggal_Perolehan", BarisTerseleksi).Value
        HargaPerolehan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Harga_Perolehan", BarisTerseleksi).Value)
        HargaPerolehanAwal_Terseleksi = AmbilAngka(DataTabelUtama.Item("Harga_Perolehan_Awal", BarisTerseleksi).Value)
        Penambahan_Pengurangan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Penambahan_Pengurangan", BarisTerseleksi).Value)
        HargaPerolehanAkhir_Terseleksi = AmbilAngka(DataTabelUtama.Item("Harga_Perolehan_Akhir", BarisTerseleksi).Value)
        AkumulasiPenyusutanSampaiTahunSebelumnya_Terseleksi = AmbilAngka(DataTabelUtama.Item("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya", BarisTerseleksi).Value)
        PenyusutanTahunLaporanYangSudahDijurnal_Terseleksi = AmbilAngka(DataTabelUtama.Item("Penyusutan_Tahun_Laporan_Yang_Sudah_Dijurnal", BarisTerseleksi).Value)
        NSB_BerdasarkanJurnalTerakhir_Terseleksi = AmbilAngka(DataTabelUtama.Item("NSB_Berdasarkan_Jurnal_Terakhir", BarisTerseleksi).Value)
        AkumulasiPenyusutanSampaiDenganJurnalTerakhir_Terseleksi _
            = AkumulasiPenyusutanSampaiTahunSebelumnya_Terseleksi _
            + PenyusutanTahunLaporanYangSudahDijurnal_Terseleksi
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", BarisTerseleksi).Value
        TahunTransaksi_Terseleksi = AmbilAngka(AmbilTeksKanan(TanggalPerolehan_Terseleksi, 4))
        KodeClosing_Terseleksi = DataTabelUtama.Item("Kode_Closing", BarisTerseleksi).Value
        If KodeClosing_Terseleksi <> Kosongan Then AssetSudahClosing_Terseleksi = True
        If KodeClosing_Terseleksi = Kosongan Then AssetSudahClosing_Terseleksi = False

        Select Case JenisTahunBuku
            Case JenisTahunBuku_NORMAL
                If TahunTransaksi_Terseleksi = TahunBukuAktif Then
                    btn_Edit.Enabled = True
                    btn_Hapus.Enabled = True
                Else
                    btn_Edit.Enabled = False
                    btn_Hapus.Enabled = False
                End If
            Case JenisTahunBuku_LAMPAU
                If TahunTransaksi_Terseleksi <= TahunBukuAktif Then
                    btn_Edit.Enabled = True
                    btn_Hapus.Enabled = True
                Else
                    btn_Edit.Enabled = False
                    btn_Hapus.Enabled = False
                End If
        End Select

        If TahunLaporan = TahunBukuAktif And JenisTahunBuku = JenisTahunBuku_NORMAL Then
            btn_LihatJurnal.Enabled = True
            btn_Posting.Enabled = True
        Else
            btn_LihatJurnal.Enabled = False
            btn_Posting.Enabled = False
        End If

        If BarisTerseleksi > (JumlahBaris - 1) Then
            BersihkanSeleksi()
        End If

        If BarisTerseleksi >= 0 Then
            If AssetSudahClosing_Terseleksi = False Then
                btn_JualAsset.Enabled = True
                btn_DisposalAsset.Enabled = True
            Else
                btn_JualAsset.Enabled = False
                btn_DisposalAsset.Enabled = False
            End If
        Else
            btn_JualAsset.Enabled = False
            btn_DisposalAsset.Enabled = False
        End If

        If btn_JualAsset.Enabled = False Then
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If

        If AmbilTeksKiri(KodeClosing_Terseleksi, PanjangTeks_AwalanPENJ) = AwalanPENJ Then
            btn_LihatInvoice.Enabled = True
        Else
            btn_LihatInvoice.Enabled = False
        End If

    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        ops_PilihJurnal_DataAsset.ResetForm()
        Dim AdaJurnal = 0
        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE COA = '" & COABiayaPenyusutan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Do While drCEKJURNAL.Read
            Dim TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
            Dim BulanJurnalPenyusutan = Format(TanggalJurnalPenyusutan, "MM")
            If AmbilAngka(BulanJurnalPenyusutan) = 1 Then
                ops_PilihJurnal_DataAsset.rdb_Januari.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Januari = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 2 Then
                ops_PilihJurnal_DataAsset.rdb_Februari.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Februari = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 3 Then
                ops_PilihJurnal_DataAsset.rdb_Maret.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Maret = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 4 Then
                ops_PilihJurnal_DataAsset.rdb_April.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_April = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 5 Then
                ops_PilihJurnal_DataAsset.rdb_Mei.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Mei = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 6 Then
                ops_PilihJurnal_DataAsset.rdb_Juni.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Juni = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 7 Then
                ops_PilihJurnal_DataAsset.rdb_Juli.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Juli = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 8 Then
                ops_PilihJurnal_DataAsset.rdb_Agustus.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Agustus = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 9 Then
                ops_PilihJurnal_DataAsset.rdb_September.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_September = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 10 Then
                ops_PilihJurnal_DataAsset.rdb_Oktober.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Oktober = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 11 Then
                ops_PilihJurnal_DataAsset.rdb_Nopember.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Nopember = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 12 Then
                ops_PilihJurnal_DataAsset.rdb_Desember.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Desember = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal += 1
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)
        If AdaJurnal > 0 Then
            ops_PilihJurnal_DataAsset.ShowDialog()
        Else
            MsgBox("Tidak/Belum ada Jurnal pada Tahun Buku ini untuk data terpilih.")
        End If
    End Sub

    Private Sub btn_Posting_Click(sender As Object, e As EventArgs) Handles btn_Posting.Click

        frm_CeklisBulan.ResetForm()

        'Non-Aktif-kan Ceklis Bulan yang sudah dijurnal :
        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE COA = '" & COABiayaPenyusutan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Do While drCEKJURNAL.Read
            Dim TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
            Dim BulanJurnalPenyusutan = AmbilBulanAngka_DariTanggal(TanggalJurnalPenyusutan)
            If AmbilAngka(BulanJurnalPenyusutan) = 1 Then frm_CeklisBulan.chk_Januari.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 2 Then frm_CeklisBulan.chk_Februari.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 3 Then frm_CeklisBulan.chk_Maret.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 4 Then frm_CeklisBulan.chk_April.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 5 Then frm_CeklisBulan.chk_Mei.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 6 Then frm_CeklisBulan.chk_Juni.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 7 Then frm_CeklisBulan.chk_Juli.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 8 Then frm_CeklisBulan.chk_Agustus.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 9 Then frm_CeklisBulan.chk_September.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 10 Then frm_CeklisBulan.chk_Oktober.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 11 Then frm_CeklisBulan.chk_Nopember.Enabled = False
            If AmbilAngka(BulanJurnalPenyusutan) = 12 Then frm_CeklisBulan.chk_Desember.Enabled = False
        Loop
        AksesDatabase_Transaksi(Tutup)

        'Non-Aktif-kan Ceklis Bulan yang belum waktunya untuk dijurnal :
        If TahunBukuAktif = TahunIni Then
            If AmbilAngka(BulanIni) <= 1 Then
                If AmbilAngka(BulanIni) = 1 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Januari.Enabled = True
                Else
                    frm_CeklisBulan.chk_Januari.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 2 Then
                If AmbilAngka(BulanIni) = 2 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Februari.Enabled = True
                Else
                    frm_CeklisBulan.chk_Februari.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 3 Then
                If AmbilAngka(BulanIni) = 3 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Maret.Enabled = True
                Else
                    frm_CeklisBulan.chk_Maret.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 4 Then
                If AmbilAngka(BulanIni) = 4 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_April.Enabled = True
                Else
                    frm_CeklisBulan.chk_April.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 5 Then
                If AmbilAngka(BulanIni) = 5 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Mei.Enabled = True
                Else
                    frm_CeklisBulan.chk_Mei.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 6 Then
                If AmbilAngka(BulanIni) = 6 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Juni.Enabled = True
                Else
                    frm_CeklisBulan.chk_Juni.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 7 Then
                If AmbilAngka(BulanIni) = 7 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Juli.Enabled = True
                Else
                    frm_CeklisBulan.chk_Juli.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 8 Then
                If AmbilAngka(BulanIni) = 8 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Agustus.Enabled = True
                Else
                    frm_CeklisBulan.chk_Agustus.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 9 Then
                If AmbilAngka(BulanIni) = 9 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_September.Enabled = True
                Else
                    frm_CeklisBulan.chk_September.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 10 Then
                If AmbilAngka(BulanIni) = 10 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Oktober.Enabled = True
                Else
                    frm_CeklisBulan.chk_Oktober.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 11 Then
                If AmbilAngka(BulanIni) = 11 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Nopember.Enabled = True
                Else
                    frm_CeklisBulan.chk_Nopember.Enabled = False
                End If
            End If
            If AmbilAngka(BulanIni) <= 12 Then
                If AmbilAngka(BulanIni) = 12 And AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanIni, TahunBukuAktif) = TanggalIni Then
                    frm_CeklisBulan.chk_Desember.Enabled = True
                Else
                    frm_CeklisBulan.chk_Desember.Enabled = False
                End If
            End If
        End If

        'Non-Aktif-kan Ceklis Bulan yang tidak ada angka Jurnal-nya, alias 0 (nol) :
        If AmbilAngka(DataTabelUtama.Item("Januari_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Januari.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Februari_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Februari.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Maret_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Maret.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("April_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_April.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Mei_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Mei.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Juni_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Juni.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Juli_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Juli.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Agustus_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Agustus.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("September_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_September.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Oktober_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Oktober.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Nopember_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Nopember.Enabled = False
        If AmbilAngka(DataTabelUtama.Item("Desember_", BarisTerseleksi).Value) = 0 Then frm_CeklisBulan.chk_Desember.Enabled = False

        frm_CeklisBulan.ShowDialog()
        If frm_CeklisBulan.LanjutkanProses = False Then Return

        Dim BulanTerceklis_Awal = frm_CeklisBulan.BulanTerceklis_Awal
        Dim BulanTerceklis_Akhir = frm_CeklisBulan.BulanTerceklis_Akhir
        Dim BulanAngka_Awal = KonversiBulanKeAngka(BulanTerceklis_Awal)
        Dim BulanAngka_Akhir = KonversiBulanKeAngka(BulanTerceklis_Akhir)

        Pilihan = MessageBox.Show("Pastikan data penjualan asset (jika ada) sudah terposting seluruhnya sampai bulan " & BulanTerceklis_Akhir & " " &
                                  "sebelum posting jurnal...!!!" & Enter2Baris &
                                  "Lanjutkan posting..?",
                                  "PERHATIAN..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        SembunyikanSemuaKolomTabel()
        TampilkanData()

        Dim JumlahJurnalTerposting = 0
        Dim TotalPenyusutan As Int64
        Dim JumlahBarisAssetTerbundel

        Dim KodeAsset_Terindeks = Nothing
        Dim COAAsset_Terindeks = Nothing
        Dim NamaAkunAsset_Terindeks = Nothing
        Dim COABiayaPenyusutan_Terindeks = Nothing

        Dim JurnalSudahAda As Boolean
        Dim BulanAngka_Telusur = BulanAngka_Awal
        Dim NamaBulan = Kosongan
        Dim JumlahPenyusutan As Int64

        Do While BulanAngka_Telusur <= BulanAngka_Akhir

            jur_StatusPenyimpananJurnal_PerBaris = True 'Ini penting, Jangan dihapus.

            TotalPenyusutan = 0
            JumlahBarisAssetTerbundel = 0
            Bundelan = Nothing

            For Each row As DataGridViewRow In DataTabelUtama.Rows

                COAAsset_Terindeks = row.Cells("COA_Asset").Value

                If AmbilAngka(COAAsset_Terindeks) = AmbilAngka(COAAsset_Terseleksi) Then

                    Id_Terindeks = row.Cells("ID_").Value
                    KodeAsset_Terindeks = row.Cells("Kode_Asset").Value
                    NamaAkunAsset_Terindeks = row.Cells("Nama_Akun_Asset").Value
                    COABiayaPenyusutan_Terindeks = row.Cells("COA_Biaya_Penyusutan").Value

                    Select Case BulanAngka_Telusur
                        Case 1
                            NamaBulan = "Januari"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Januari_").Value)
                        Case 2
                            NamaBulan = "Februari"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Februari_").Value)
                        Case 3
                            NamaBulan = "Maret"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Maret_").Value)
                        Case 4
                            NamaBulan = "April"
                            JumlahPenyusutan = AmbilAngka(row.Cells("April_").Value)
                        Case 5
                            NamaBulan = "Mei"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Mei_").Value)
                        Case 6
                            NamaBulan = "Juni"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Juni_").Value)
                        Case 7
                            NamaBulan = "Juli"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Juli_").Value)
                        Case 8
                            NamaBulan = "Agustus"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Agustus_").Value)
                        Case 9
                            NamaBulan = "September"
                            JumlahPenyusutan = AmbilAngka(row.Cells("September_").Value)
                        Case 10
                            NamaBulan = "Oktober"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Oktober_").Value)
                        Case 11
                            NamaBulan = "Nopember"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Nopember_").Value)
                        Case 12
                            NamaBulan = "Desember"
                            JumlahPenyusutan = AmbilAngka(row.Cells("Desember_").Value)
                    End Select

                    TanggalTransaksi_Simpan = TanggalFormatSimpan(AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka_Telusur, TahunBukuAktif))

                    AksesDatabase_Transaksi(Buka)
                    cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                                   " WHERE Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
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
                        JumlahBarisAssetTerbundel = JumlahBarisAssetTerbundel + 1
                        TotalPenyusutan = TotalPenyusutan + JumlahPenyusutan
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
                    JumlahJurnalTerposting = JumlahJurnalTerposting + 1
                Else
                    MsgBox("Ups... Terjadi kesalahan pada proses penyimpanan..!")
                    Exit Do
                End If
            End If

            BulanAngka_Telusur = BulanAngka_Telusur + 1

            'Syntax di bawah ini berfungsi untuk mencegah penjurnalan pada Bulan Berjalan di Tahun Berjalan
            'Sementara untuk bulan yang sama di tahun yang berbeda (sebelumnya) tetap dijurnal.
            'Dengan kata lain, pada tahun sebelum tahun berjalan, penjurnalan dilakukan secara lengkap (12 Bulan)
            If TahunBukuAktif = TahunIni And BulanAngka_Telusur = BulanIni Then Exit Do
            If jur_StatusPenyimpananJurnal_PerBaris = False Then Exit Do

        Loop 'Susur Nomor Bulan

        Sub_FungsiForm_Detail_Rekap()

        If jur_StatusPenyimpananJurnal_PerBaris = True Then
            If JumlahJurnalTerposting = 1 Then
                MsgBox("'Jurnal Penyusutan' Akun '" & NamaAkunBiayaPenyusutan_Terseleksi &
                       "' Bulan " & BulanTerceklis_Awal & " BERHASIL diposting.")
            Else
                MsgBox("'Jurnal Penyusutan' Akun '" & NamaAkunBiayaPenyusutan_Terseleksi &
                       "' BERHASIL diposting untuk Bulan " & BulanTerceklis_Awal & " - " & BulanTerceklis_Akhir & ".")
            End If
        Else
            If JumlahJurnalTerposting > 0 Then
                MsgBox("'Jurnal Penyusutan' Akun '" & NamaAkunBiayaPenyusutan_Terseleksi & "' hanya terposting sebagian." & Enter2Baris & teks_SilakanUlangiLagi_Database)
            Else
                MsgBox("'Jurnal Penyusutan' Akun '" & NamaAkunBiayaPenyusutan_Terseleksi & "' GAGAL diposting." & Enter2Baris & teks_SilakanUlangiLagi_Database)
            End If
        End If

    End Sub

    Private Sub btn_LihatInvoice_Click(sender As Object, e As EventArgs) Handles btn_LihatInvoice.Click
        Dim InvoicePenjualanAsset = KonversiNomorPenjualanKeNomorInvoice(KodeClosing_Terseleksi)
        frm_Cetak.FungsiForm = FungsiForm_PRATINJAU
        NomorInvoicePenjualan_Cetak = InvoicePenjualanAsset
        TampilkanHalamanCetak_InvoicePenjualan()
    End Sub

    Private Sub btn_JualAsset_Click(sender As Object, e As EventArgs) Handles btn_JualAsset.Click

        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
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
            MsgBox("'Jurnal Penyusutan' terkait dengan asset ini sudah diposting sampai Bulan Desember." & Enter2Baris &
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

        X_frm_JualAsset.BulanDijual_Angka = BulanDijual_Angka  'Variabel 'BulanPengunci' sengaja lebih didahulukan dari ResetForm
        X_frm_JualAsset.ResetForm()
        X_frm_JualAsset.JenisProduk_Induk = JenisProduk_Barang
        X_frm_JualAsset.JenisProduk_PerItem = JenisProduk_Barang
        X_frm_JualAsset.KodeAsset = KodeAsset_Terseleksi
        X_frm_JualAsset.txt_NamaAktiva.Text = NamaAktiva_Terseleksi
        X_frm_JualAsset.KelompokHarta = KelompokHarta_Terseleksi
        X_frm_JualAsset.dtp_TanggalPerolehan.Value = TanggalPerolehan_Terseleksi
        X_frm_JualAsset.txt_NilaiSisaBuku.Text = NSB_BerdasarkanJurnalTerakhir_Terseleksi
        X_frm_JualAsset.txt_HargaPerolehan.Text = HargaPerolehan_Terseleksi
        If KelompokHarta_Terseleksi = KelompokHarta_Tanah Then
            X_frm_JualAsset.txt_COA_AkumulasiPenyusutan.Text = Kosongan
        Else
            X_frm_JualAsset.txt_COA_AkumulasiPenyusutan.Text = AmbilValue_COAAkumulasiPenyusutan_DariDataAsset(KodeAsset_Terseleksi)
        End If
        X_frm_JualAsset.txt_AkumulasiPenyusutan.Text = AkumulasiPenyusutanSampaiDenganJurnalTerakhir_Terseleksi
        If NSB_BerdasarkanJurnalTerakhir_Terseleksi = 0 _
            Or KelompokHarta_Terseleksi = KelompokHarta_Tanah _
            Then
            X_frm_JualAsset.lbl_PerTanggal_1.Visible = False
            X_frm_JualAsset.lbl_PerTanggal_2.Visible = False
        Else
            X_frm_JualAsset.lbl_PerTanggal_1.Visible = True
            X_frm_JualAsset.lbl_PerTanggal_2.Visible = True
        End If
        If KelompokHarta_Terseleksi = KelompokHarta_Tanah _
            Or KelompokHarta_Terseleksi = KelompokHarta_BangunanPermanen _
            Or KelompokHarta_Terseleksi = KelompokHarta_BangunanTidakPermanen _
            Then
            X_frm_JualAsset.AdaPPh = True
        Else
            X_frm_JualAsset.AdaPPh = False
        End If
        X_frm_JualAsset.COA_AssetDijual = COAAsset_Terseleksi
        X_frm_JualAsset.ShowDialog()

    End Sub

    Private Sub btn_DisposalAsset_Click(sender As Object, e As EventArgs) Handles btn_DisposalAsset.Click

        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE Jenis_Jurnal = '" & JenisJurnal_Penyusutan & "' " &
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
            MsgBox("'Jurnal Penyusutan' terkait dengan asset ini sudah diposting sampai Bulan Desember." & Enter2Baris &
                   "Jika ingin posting data disposal terkait asset ini, silakan hapus terlebih dahulu postingan jurnalnya, " &
                   "mulai dari bulan saat Disposal Asset sampai seterusnya.")
            Return
        End If

        X_frm_InputDisposalAssetTetap.BulanDisposal_Angka = BulanJurnalPenyusutan + 1 'Variabel 'BulanPengunci' sengaja lebih didahulukan dari ResetForm
        X_frm_InputDisposalAssetTetap.ResetForm()
        X_frm_InputDisposalAssetTetap.KodeAsset = KodeAsset_Terseleksi
        X_frm_InputDisposalAssetTetap.txt_NamaAktiva.Text = NamaAktiva_Terseleksi
        X_frm_InputDisposalAssetTetap.KelompokHarta = KelompokHarta_Terseleksi
        X_frm_InputDisposalAssetTetap.dtp_TanggalPerolehan.Value = TanggalPerolehan_Terseleksi
        X_frm_InputDisposalAssetTetap.txt_NilaiSisaBuku.Text = NSB_BerdasarkanJurnalTerakhir_Terseleksi
        X_frm_InputDisposalAssetTetap.txt_HargaPerolehan.Text = HargaPerolehan_Terseleksi
        If KelompokHarta_Terseleksi = KelompokHarta_Tanah Then
            X_frm_InputDisposalAssetTetap.txt_COA_AkumulasiPenyusutan.Text = Kosongan
        Else
            X_frm_InputDisposalAssetTetap.txt_COA_AkumulasiPenyusutan.Text = PenentuanCOA_AkumulasiPenyusutan(COAAsset_Terseleksi)
        End If
        X_frm_InputDisposalAssetTetap.txt_AkumulasiPenyusutan.Text = AkumulasiPenyusutanSampaiDenganJurnalTerakhir_Terseleksi
        If NSB_BerdasarkanJurnalTerakhir_Terseleksi = 0 _
            Or KelompokHarta_Terseleksi = KelompokHarta_Tanah _
            Then
            X_frm_InputDisposalAssetTetap.lbl_PerTanggal_1.Visible = False
            X_frm_InputDisposalAssetTetap.lbl_PerTanggal_2.Visible = False
        Else
            X_frm_InputDisposalAssetTetap.lbl_PerTanggal_1.Visible = True
            X_frm_InputDisposalAssetTetap.lbl_PerTanggal_2.Visible = True
        End If
        X_frm_InputDisposalAssetTetap.COA_AssetDisposal = COAAsset_Terseleksi
        X_frm_InputDisposalAssetTetap.ShowDialog()

    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class