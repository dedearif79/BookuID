Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Media
Imports bcomm

Public Class wpfUsc_Adjusment_Amortisasi

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Dim JudulForm
    Dim TahunLaporan
    Dim TahunLaporanSebelumnya
    Dim TahunLaporanTerakhir

    'Variabel untuk Data Terseleksi :
    Dim NomorID_Terseleksi
    Dim KodeAsset_Terseleksi
    Dim COAAmortisasi_Terseleksi
    Dim NamaAkunAmortisasi_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim COABiaya_Terseleksi
    Dim NamaAkunBiaya_Terseleksi
    Dim MasaAmortisasi_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim TanggalMulai_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim Keterangan_Terseleksi
    Dim TahunTransaksi_Terseleksi As Integer

    'Variabel untuk Kebutuhan Posting Jurnal :
    Dim TanggalTransaksi_Simpan
    Dim Bundelan
    Dim KodeLawanTransaksi, NamaLawanTransaksi
    Dim UraianTransaksi

    'Variabel Tabel :
    Dim NomorUrut
    Dim IdAmortisasi
    Dim KodeAsset
    Dim MasaAmortisasi
    Dim KodeAkun_Amortisasi
    Dim NamaAkun_Amortisasi
    Dim NamaProduk
    Dim KodeAkun_Biaya
    Dim NamaAkun_Biaya
    Dim TanggalTransaksi
    Dim TanggalMulaiAmortisasi
    Dim HariTransaksi
    Dim BulanTransaksi
    Dim TahunTransaksi
    Dim BulanDimulaiAmortisasi
    Dim BulanTerakhirAmortisasi
    Dim TahunDimulaiAmortisasi
    Dim JumlahTransaksi As Int64
    Dim AmortisasiPerbulan As Long 'Jangan dirubah menjadi integer. Sudah benar long.
    Dim AmortisasiPertahun As Long 'Idem
    Dim AmortisasiBulanTerakhir As Long 'Idem
    Dim AkumulasiPenyusutanSampaiTahunSebelumnya As Int64
    Dim PenambahanPengurangan As Int64
    Dim SaldoAwalSiapUntukDiamortisasi As Int64
    Dim SaldoAwal, SaldoAkhir As Int64
    Dim SelisihSaldoAkhir As Integer
    Dim Januari, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, Nopember, Desember As Int64
    Dim Rekap_Januari, Rekap_Februari, Rekap_Maret, Rekap_April, Rekap_Mei, Rekap_Juni,
        Rekap_Juli, Rekap_Agustus, Rekap_September, Rekap_Oktober, Rekap_Nopember, Rekap_Desember As Int64

    Dim KodeAkun_Biaya_Sebelumnya
    Dim NamaAkun_Biaya_Sebelumnya
    Dim KodeAkun_Amortisasi_Sebelumnya
    Dim NamaAkun_Amortisasi_Sebelumnya

    Dim AkumulasiAmortisasiTahunLaporan As Int64
    Dim AkumulasiAmortisasiSampaiDengan As Int64
    Dim Keterangan

    Dim AkumulasiAmortisasiTahunLaporan_BerdasarkanJurnal As Int64
    Dim AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan As Int64


    Dim TahunSelisih '(Masih bingung ngasih nama variabelnya. Belum ketemu istilah yang lebih pas. Ini hanya variabel pembantu saja).
    Dim JumlahBulanPenyusutanSampaiTahunSebelumnya As Integer
    Dim SisaBulanPenyusutan As Long '(Sudah benar pakai As Long. Jangan Dirubah ke Integer/Int. Supaya angkanya bulat, tidak berkoma)
    Dim JumlahMaksimalBulanPenyusutan As Integer
    Dim ToleransiSelisihSaldo = 1200

    Dim Jml_PenambahanPengurangan As Int64
    Dim Jml_SaldoAwalSiapUntukDiamortisasi As Int64
    Dim Jml_SaldoAwal, Jml_SaldoAkhir As Int64
    Dim Jml_Januari, Jml_Februari, Jml_Maret, Jml_April, Jml_Mei, Jml_Juni, Jml_Juli, Jml_Agustus, Jml_September, Jml_Oktober, Jml_Nopember, Jml_Desember
    Dim Jml_AkumulasiAmortisasiSampaiDengan As Int64
    Dim Jml_JumlahTransaksi As Int64
    Dim Jml_AkumulasiAmortisasiSampaiTahunSebelumnya As Int64
    Dim Jml_AkumulasiAmortisasiTahunLaporan As Int64

    'Variabel Koneksi Database :
    Dim cmdCEKJURNAL As OdbcCommand
    Dim drCEKJURNAL As OdbcDataReader

    Public KesesuaianJurnal As Boolean
    Dim JenisTampilan

    Public AdjusmentBulanBukuAktifSudahLengkap As Boolean
    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        lbl_JudulForm.Text = JudulForm
        Terabas()

        ProsesLoadingForm = True

        RefreshTampilanData()

        ProsesLoadingForm = False

        datagridUtama.SelectionUnit = DataGridSelectionUnit.FullRow 'Ini style khusus, karena ada masalah yang belum diketahui

        SudahDimuat = True

    End Sub


    Sub RefreshTampilanData()
        Kode_Asset.Visibility = Visibility.Collapsed
        Nama_Produk.Visibility = Visibility.Collapsed
        Tanggal_Transaksi.Visibility = Visibility.Collapsed
        Tanggal_Mulai.Visibility = Visibility.Collapsed
        Jumlah_Transaksi.Visibility = Visibility.Collapsed
        Masa_Amortisasi.Visibility = Visibility.Collapsed
        Saldo_Awal.Visibility = Visibility.Collapsed
        Penambahan_Pengurangan.Visibility = Visibility.Collapsed
        Saldo_Awal_Siap_Untuk_Diamortisasi.Visibility = Visibility.Collapsed
        Amortisasi_Tahun_Ini.Visibility = Visibility.Collapsed
        Akumulasi_Amortisasi_Sampai_Dengan_Tahun_Ini.Visibility = Visibility.Collapsed
        Saldo_Akhir.Visibility = Visibility.Collapsed
        Selisih_Saldo_Akhir.Visibility = Visibility.Collapsed
        Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya.Visibility = Visibility.Collapsed
        Keterangan_.Visibility = Visibility.Collapsed
        TahunLaporan = TahunBukuAktif
        TahunLaporanSebelumnya = TahunLaporan - 1
        EksekusiTampilanData = True
        JenisTampilan = JenisTampilan_Rekap
        TampilkanData()
    End Sub



    Dim EksekusiTampilanData As Boolean
    Sub TampilkanData()

        If EksekusiTampilanData = False Then Return

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Terabas()

        'Style Tabel :
        datatabelUtama.Rows.Clear()
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

        'Data Tabel : 
        NomorUrut = 0
        SelisihSaldoAkhir = 0

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

        Jml_PenambahanPengurangan = 0
        Jml_SaldoAwalSiapUntukDiamortisasi = 0
        Jml_SaldoAwal = 0
        Jml_SaldoAkhir = 0
        Jml_AkumulasiAmortisasiSampaiDengan = 0
        Jml_JumlahTransaksi = 0
        Jml_AkumulasiAmortisasiSampaiTahunSebelumnya = 0
        Jml_AkumulasiAmortisasiTahunLaporan = 0

        KodeAkun_Biaya_Sebelumnya = Kosongan
        NamaAkun_Biaya_Sebelumnya = Kosongan
        KodeAkun_Amortisasi_Sebelumnya = Kosongan
        NamaAkun_Amortisasi_Sebelumnya = Kosongan

        KesesuaianJurnal = True

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_AmortisasiBiaya ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader

        Do While dr.Read

            IdAmortisasi = dr.Item("Nomor_ID")
            KodeAsset = dr.Item("Kode_Asset")
            KodeAkun_Amortisasi = dr.Item("COA_Amortisasi")
            NamaAkun_Amortisasi = dr.Item("Nama_Akun_Amortisasi")
            NamaProduk = dr.Item("Nama_Produk")
            KodeAkun_Biaya = dr.Item("COA_Biaya")
            NamaAkun_Biaya = dr.Item("Nama_Akun_Biaya")
            MasaAmortisasi = dr.Item("Masa_Amortisasi") & " Bulan"
            JumlahMaksimalBulanPenyusutan = AmbilAngka(MasaAmortisasi)
            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            TanggalMulaiAmortisasi = TanggalFormatTampilan(dr.Item("Tanggal_Mulai"))
            HariTransaksi = AmbilAngka(Format(dr.Item("Tanggal_Transaksi"), "dd"))
            BulanTransaksi = AmbilAngka(Format(dr.Item("Tanggal_Transaksi"), "MM"))
            TahunTransaksi = AmbilAngka(Format(dr.Item("Tanggal_Transaksi"), "yyyy"))
            JumlahTransaksi = dr.Item("Jumlah_Transaksi")

            BulanDimulaiAmortisasi = AmbilBulanAngka_DariTanggal(TanggalMulaiAmortisasi)
            TahunDimulaiAmortisasi = AmbilTahun_DariTanggal(TanggalMulaiAmortisasi)
            TahunSelisih = TahunLaporanSebelumnya

            Dim SelisihTahun As Integer
            If TahunDimulaiAmortisasi > TahunTransaksi Then
                SelisihTahun = TahunDimulaiAmortisasi - TahunTransaksi
                TahunDimulaiAmortisasi = TahunTransaksi + SelisihTahun
                TahunSelisih = TahunLaporanSebelumnya + SelisihTahun
            End If

            JumlahBulanPenyusutanSampaiTahunSebelumnya = (12 - BulanDimulaiAmortisasi + 1) + (12 * (TahunSelisih - TahunDimulaiAmortisasi))
            If JumlahBulanPenyusutanSampaiTahunSebelumnya < 0 Then JumlahBulanPenyusutanSampaiTahunSebelumnya = 0
            If JumlahBulanPenyusutanSampaiTahunSebelumnya > JumlahMaksimalBulanPenyusutan Then JumlahBulanPenyusutanSampaiTahunSebelumnya = JumlahMaksimalBulanPenyusutan

            AmortisasiPerbulan = JumlahTransaksi / JumlahMaksimalBulanPenyusutan
            AmortisasiPertahun = JumlahTransaksi / JumlahMaksimalBulanPenyusutan * 12
            AkumulasiPenyusutanSampaiTahunSebelumnya = AmortisasiPerbulan * JumlahBulanPenyusutanSampaiTahunSebelumnya
            SaldoAwalSiapUntukDiamortisasi = JumlahTransaksi - AkumulasiPenyusutanSampaiTahunSebelumnya
            If TahunLaporan < TahunTransaksi Then SaldoAwalSiapUntukDiamortisasi = 0
            SaldoAwal = SaldoAwalSiapUntukDiamortisasi
            If SaldoAwalSiapUntukDiamortisasi < ToleransiSelisihSaldo Then
                SaldoAwal = 0
                SaldoAwalSiapUntukDiamortisasi = 0
            End If

            SisaBulanPenyusutan = SaldoAwalSiapUntukDiamortisasi / AmortisasiPerbulan
            BulanTerakhirAmortisasi = SisaBulanPenyusutan

            'Default Value :
            Januari = AmortisasiPerbulan
            Februari = AmortisasiPerbulan
            Maret = AmortisasiPerbulan
            April = AmortisasiPerbulan
            Mei = AmortisasiPerbulan
            Juni = AmortisasiPerbulan
            Juli = AmortisasiPerbulan
            Agustus = AmortisasiPerbulan
            September = AmortisasiPerbulan
            Oktober = AmortisasiPerbulan
            Nopember = AmortisasiPerbulan
            Desember = AmortisasiPerbulan

            'Pemulihan value 'BulanDimulaiAmortisasi', yang tadinya 13 menjadi 1.
            'Posisinya di sini, dan jangan dirubah.
            If BulanDimulaiAmortisasi > 12 Then BulanDimulaiAmortisasi = 1

            'Jika (Tahun Laporan = Tahun Dimulai Amortisasi) dan (Dimulai pada bulan tertentu), maka bulan sebelumnya harus dibikin nol (0).
            If TahunLaporan = TahunDimulaiAmortisasi Then
                If TahunLaporan = TahunTransaksi Then SaldoAwal = 0
                If BulanDimulaiAmortisasi > 1 Then Januari = 0
                If BulanDimulaiAmortisasi > 2 Then Februari = 0
                If BulanDimulaiAmortisasi > 3 Then Maret = 0
                If BulanDimulaiAmortisasi > 4 Then April = 0
                If BulanDimulaiAmortisasi > 5 Then Mei = 0
                If BulanDimulaiAmortisasi > 6 Then Juni = 0
                If BulanDimulaiAmortisasi > 7 Then Juli = 0
                If BulanDimulaiAmortisasi > 8 Then Agustus = 0
                If BulanDimulaiAmortisasi > 9 Then September = 0
                If BulanDimulaiAmortisasi > 10 Then Oktober = 0
                If BulanDimulaiAmortisasi > 11 Then Nopember = 0
                If BulanDimulaiAmortisasi > 12 Then Desember = 0    'Dengan adanya algoritma baru, baris ini sebetulnya sudah tidak dibutuhkan. Tapi ga apa-apa lah, buat dokumentasi.
            End If

            'Jika (Tahun Laporan adalah sebelum Tahun Dimulai Amortisasi), maka semua bulan harus dibikin nol (0).
            If TahunLaporan < TahunDimulaiAmortisasi Then
                SaldoAwal = 0
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

            'Ini Logika untuk Tahun Laporan = Tahun Terakhir Amortisasi.
            If SisaBulanPenyusutan <= 12 Then
                BulanTerakhirAmortisasi = SisaBulanPenyusutan
                SelisihSaldoAkhir = JumlahTransaksi - AkumulasiPenyusutanSampaiTahunSebelumnya - (AmortisasiPerbulan * SisaBulanPenyusutan)
                If (SelisihSaldoAkhir > ToleransiSelisihSaldo) Or (SelisihSaldoAkhir < (-ToleransiSelisihSaldo)) Then SelisihSaldoAkhir = 0
                AmortisasiBulanTerakhir = AmortisasiPerbulan + SelisihSaldoAkhir
                'Ini untuk kasus yang Masa Amortisasi-nya tidak sampai 12 bulan, dan 
                '(Kode ini tidak ada di halaman Daftar Penyusutan Asset,
                'karena disana minimal masa penyusutan-nya adalah 4 tahun, alias sudah pasti diatas 12 bulan)
                If JumlahMaksimalBulanPenyusutan < 12 Then
                    If SaldoAwalSiapUntukDiamortisasi = JumlahTransaksi Then BulanTerakhirAmortisasi = (BulanDimulaiAmortisasi - 1) + SisaBulanPenyusutan
                    If BulanTerakhirAmortisasi <= 12 Then
                        If BulanTerakhirAmortisasi > 1 Then Januari = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 2 Then Februari = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 3 Then Maret = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 4 Then April = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 5 Then Mei = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 6 Then Juni = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 7 Then Juli = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 8 Then Agustus = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 9 Then September = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 10 Then Oktober = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 11 Then Nopember = AmortisasiPerbulan
                        If BulanTerakhirAmortisasi > 12 Then Desember = AmortisasiPerbulan
                        '-----------------------------------------------------------------
                        If SaldoAwalSiapUntukDiamortisasi = JumlahTransaksi Then
                            If BulanDimulaiAmortisasi > 1 Then Januari = 0
                            If BulanDimulaiAmortisasi > 2 Then Februari = 0
                            If BulanDimulaiAmortisasi > 3 Then Maret = 0
                            If BulanDimulaiAmortisasi > 4 Then April = 0
                            If BulanDimulaiAmortisasi > 5 Then Mei = 0
                            If BulanDimulaiAmortisasi > 6 Then Juni = 0
                            If BulanDimulaiAmortisasi > 7 Then Juli = 0
                            If BulanDimulaiAmortisasi > 8 Then Agustus = 0
                            If BulanDimulaiAmortisasi > 9 Then September = 0
                            If BulanDimulaiAmortisasi > 10 Then Oktober = 0
                            If BulanDimulaiAmortisasi > 11 Then Nopember = 0
                        End If
                    End If
                End If '--------------------------------------------
                If SelisihSaldoAkhir < ToleransiSelisihSaldo Then
                    If BulanTerakhirAmortisasi = 1 Then Januari = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 2 Then Februari = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 3 Then Maret = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 4 Then April = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 5 Then Mei = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 6 Then Juni = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 7 Then Juli = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 8 Then Agustus = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 9 Then September = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 10 Then Oktober = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 11 Then Nopember = AmortisasiBulanTerakhir
                    If BulanTerakhirAmortisasi = 12 Then Desember = AmortisasiBulanTerakhir
                    '----------------------------------------------------------------------
                    If SaldoAwalSiapUntukDiamortisasi = 0 Then AkumulasiPenyusutanSampaiTahunSebelumnya = AkumulasiPenyusutanSampaiTahunSebelumnya + SelisihSaldoAkhir
                End If
                'Jika sudah habis Masa Amortisasi, maka bulan setelahnya harus dibikin nol (0).
                If BulanTerakhirAmortisasi < 1 Then Januari = 0
                If BulanTerakhirAmortisasi < 2 Then Februari = 0
                If BulanTerakhirAmortisasi < 3 Then Maret = 0
                If BulanTerakhirAmortisasi < 4 Then April = 0
                If BulanTerakhirAmortisasi < 5 Then Mei = 0
                If BulanTerakhirAmortisasi < 6 Then Juni = 0
                If BulanTerakhirAmortisasi < 7 Then Juli = 0
                If BulanTerakhirAmortisasi < 8 Then Agustus = 0
                If BulanTerakhirAmortisasi < 9 Then September = 0
                If BulanTerakhirAmortisasi < 10 Then Oktober = 0
                If BulanTerakhirAmortisasi < 11 Then Nopember = 0
                If BulanTerakhirAmortisasi < 12 Then Desember = 0
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

            AkumulasiAmortisasiTahunLaporan = Januari + Februari + Maret + April + Mei + Juni + Juli + Agustus + September + Oktober + Nopember + Desember
            If TahunLaporan = TahunTransaksi Then
                PenambahanPengurangan = JumlahTransaksi
            Else
                PenambahanPengurangan = 0
            End If
            AkumulasiAmortisasiSampaiDengan = AkumulasiPenyusutanSampaiTahunSebelumnya + AkumulasiAmortisasiTahunLaporan
            SaldoAkhir = JumlahTransaksi - AkumulasiAmortisasiSampaiDengan
            If SaldoAkhir < ToleransiSelisihSaldo Then
                SaldoAkhir = 0
                AkumulasiAmortisasiSampaiDengan = JumlahTransaksi
            End If
            If TahunLaporan < TahunDimulaiAmortisasi And TahunLaporan < TahunTransaksi Then SaldoAkhir = 0

            Keterangan = PenghapusEnter(dr.Item("Keterangan"))

            If AkumulasiAmortisasiTahunLaporan > 0 Then

                Jml_JumlahTransaksi += JumlahTransaksi
                Jml_AkumulasiAmortisasiSampaiTahunSebelumnya += AkumulasiPenyusutanSampaiTahunSebelumnya
                Jml_SaldoAwal += SaldoAwal
                Jml_PenambahanPengurangan += PenambahanPengurangan
                Jml_SaldoAwalSiapUntukDiamortisasi += SaldoAwalSiapUntukDiamortisasi
                Jml_AkumulasiAmortisasiTahunLaporan += AkumulasiAmortisasiTahunLaporan
                Jml_AkumulasiAmortisasiSampaiDengan += AkumulasiAmortisasiSampaiDengan
                Jml_SaldoAkhir += SaldoAkhir

                Select Case JenisTampilan
                    Case JenisTampilan_Detail
                        TambahBaris_Detail()
                    Case JenisTampilan_Rekap
                        If KodeAkun_Biaya_Sebelumnya <> Kosongan Then
                            If KodeAkun_Biaya_Sebelumnya <> KodeAkun_Biaya Then
                                TambahBaris_Rekap()
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
                            End If
                        End If
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
                End Select

                'Ini untuk membiaskan warna huruf yang value-nya 0

                'Cek Data Jurnal :
                If TahunLaporan = TahunBukuAktif Then
                    AkumulasiAmortisasiTahunLaporan_BerdasarkanJurnal = 0
                    AksesDatabase_Transaksi(Buka)
                    cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                                   " WHERE  Valid    <> '" & _X_ & "' " &
                                                   " AND Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
                                                   " AND Bundelan = '" & KodeAsset & "' " &
                                                   " AND COA = '" & KodeAkun_Biaya & "' ", KoneksiDatabaseTransaksi)
                    drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
                    Do While drCEKJURNAL.Read
                        AkumulasiAmortisasiTahunLaporan_BerdasarkanJurnal += AmbilAngka(drCEKJURNAL.Item("Jumlah_Debet"))
                    Loop
                    AksesDatabase_Transaksi(Tutup)
                    AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan
                    If TahunLaporan = TahunIni Then
                        Select Case AmbilAngka(BulanIni)
                            Case 1
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Januari - Februari - Maret - April - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                            Case 2
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Februari - Maret - April - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                            Case 3
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Maret - April - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                            Case 4
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - April - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                            Case 5
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                            Case 6
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                            Case 7
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Juli - Agustus - September - Oktober - Nopember - Desember
                            Case 8
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Agustus - September - Oktober - Nopember - Desember
                            Case 9
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - September - Oktober - Nopember - Desember
                            Case 10
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Oktober - Nopember - Desember
                            Case 11
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Nopember - Desember
                            Case 12
                                AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan _
                                    - Desember
                        End Select
                    End If
                    If AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan <> AkumulasiAmortisasiTahunLaporan_BerdasarkanJurnal Then KesesuaianJurnal = False
                End If

            End If

            KodeAkun_Biaya_Sebelumnya = KodeAkun_Biaya
            NamaAkun_Biaya_Sebelumnya = NamaAkun_Biaya
            KodeAkun_Amortisasi_Sebelumnya = KodeAkun_Amortisasi
            NamaAkun_Amortisasi_Sebelumnya = NamaAkun_Amortisasi

        Loop 'Ujung Loop Tampilan

        AksesDatabase_General(Tutup)

        If JenisTampilan = JenisTampilan_Rekap Then TambahBaris_Rekap()

        JumlahBaris = datatabelUtama.Rows.Count

        Dim TanggalJurnalPenyusutan
        Dim BulanJurnalPenyusutan
        Dim cmdKhusus As OdbcCommand
        Dim drKhusus As OdbcDataReader
        Dim JumlahJurnalAdjusment As Integer = 0
        Dim JumlahBarisBahanJurnal As Integer = JumlahBaris
        Dim BulanAngka As Integer = BulanBukuAktif      'Ini Penting...! Jangan dihapus
        If BulanBukuAktif > 12 Then BulanAngka = 12     'Ini Penting...! Jangan dihapus
        Dim KolomBulan As String = KonversiAngkaKeBulanString(BulanAngka) & "_"
        AksesDatabase_Transaksi(Buka)
        For Each row As DataRow In datatabelUtama.Rows
            KodeAkun_Biaya = row("COA_Biaya")
            If AmbilAngka(row(KolomBulan)) = 0 Then JumlahBarisBahanJurnal -= 1
            cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                        " WHERE  Valid    <> '" & _X_ & "' " &
                                        " AND Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
                                        " AND COA = '" & KodeAkun_Biaya & "' ", KoneksiDatabaseTransaksi)
            drKhusus = cmdKhusus.ExecuteReader
            Do While drKhusus.Read
                TanggalJurnalPenyusutan = drKhusus.Item("Tanggal_Transaksi")
                BulanJurnalPenyusutan = AmbilAngka(Format(TanggalJurnalPenyusutan, "MM"))
                If BulanJurnalPenyusutan = BulanBukuAktif Then JumlahJurnalAdjusment += 1
            Loop
            Jeda(111)
        Next
        AksesDatabase_Transaksi(Tutup)

        If JumlahJurnalAdjusment = JumlahBarisBahanJurnal Then
            AdjusmentBulanBukuAktifSudahLengkap = True
        Else
            AdjusmentBulanBukuAktifSudahLengkap = False
        End If

        'PesanUntukProgrammer(
        '    "Jumlah Baris Bahan Jurnal : " & JumlahBarisBahanJurnal & Enter2Baris &
        '    "Jumlah Jurnal Adjusment : " & JumlahJurnalAdjusment & Enter2Baris &
        '    "Adjusment sudah lengkap : " & AdjusmentBulanBukuAktifSudahLengkap & Enter2Baris &
        '    "")

        If JumlahBaris > 0 Then
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, "      J U M L A H", Kosongan, Kosongan,
                                    Jml_JumlahTransaksi, Kosongan, 0, 0, 0,
                                    0, Jml_SaldoAwal, Jml_PenambahanPengurangan, Jml_SaldoAwalSiapUntukDiamortisasi,
                                    Jml_AkumulasiAmortisasiTahunLaporan, Jml_AkumulasiAmortisasiSampaiDengan, Jml_SaldoAkhir, 0,
                                    Jml_Januari, Jml_Februari, Jml_Maret, Jml_April, Jml_Mei, Jml_Juni, Jml_Juli, Jml_Agustus, Jml_September, Jml_Oktober, Jml_Nopember, Jml_Desember)
        End If

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris_Detail()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, IdAmortisasi, KodeAsset, KodeAkun_Amortisasi, NamaAkun_Amortisasi, NamaProduk,
                                KodeAkun_Biaya, NamaAkun_Biaya, TanggalTransaksi, TanggalMulaiAmortisasi,
                                JumlahTransaksi, MasaAmortisasi, JumlahBulanPenyusutanSampaiTahunSebelumnya, SisaBulanPenyusutan, BulanTerakhirAmortisasi,
                                AkumulasiPenyusutanSampaiTahunSebelumnya, SaldoAwal, PenambahanPengurangan, SaldoAwalSiapUntukDiamortisasi,
                                AkumulasiAmortisasiTahunLaporan, AkumulasiAmortisasiSampaiDengan, SaldoAkhir, SelisihSaldoAkhir,
                                Januari, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, Nopember, Desember,
                                Keterangan)
        Terabas()
    End Sub

    Sub TambahBaris_Rekap()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, Kosongan, Kosongan, KodeAkun_Amortisasi_Sebelumnya, NamaAkun_Amortisasi_Sebelumnya, Kosongan,
                                KodeAkun_Biaya_Sebelumnya, NamaAkun_Biaya_Sebelumnya, Kosongan, Kosongan,
                                0, Kosongan, 0, 0, 0,
                                0, 0, 0, 0,
                                0, 0, 0, 0,
                                Rekap_Januari, Rekap_Februari, Rekap_Maret, Rekap_April, Rekap_Mei, Rekap_Juni,
                                Rekap_Juli, Rekap_Agustus, Rekap_September, Rekap_Oktober, Rekap_Nopember, Rekap_Desember)
        Terabas()
        Jeda(111)
    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        KetersediaanTombolLihatJurnal(False)
        KetersediaanTombolPosting(False)
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub



    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        pnl_Jurnal.Visibility = Visibility.Collapsed
        If Visibilitas Then
            pnl_Jurnal.Visibility = Visibility.Visible
        Else
            pnl_Jurnal.Visibility = Visibility.Collapsed
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


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub



    Dim Id_Terindeks
    Private Sub btn_Posting_Click(sender As Object, e As RoutedEventArgs) Handles btn_Posting.Click

        win_CeklisBulan = New wpfWin_CeklisBulan

        'Non-Aktif-kan Ceklis Bulan yang sudah dijurnal :
        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE  Valid          <> '" & _X_ & "' " &
                                       " AND    Jenis_Jurnal    = '" & JenisJurnal_Amortisasi & "' " &
                                       " AND    COA             = '" & COABiaya_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Do While drCEKJURNAL.Read
            Dim TanggalJurnalAmortisasi = drCEKJURNAL.Item("Tanggal_Transaksi")
            Dim BulanJurnalAmortisasi = AmbilBulanAngka_DariTanggal(TanggalJurnalAmortisasi)
            If AmbilAngka(BulanJurnalAmortisasi) = 1 Then win_CeklisBulan.chk_Januari.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 2 Then win_CeklisBulan.chk_Februari.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 3 Then win_CeklisBulan.chk_Maret.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 4 Then win_CeklisBulan.chk_April.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 5 Then win_CeklisBulan.chk_Mei.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 6 Then win_CeklisBulan.chk_Juni.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 7 Then win_CeklisBulan.chk_Juli.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 8 Then win_CeklisBulan.chk_Agustus.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 9 Then win_CeklisBulan.chk_September.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 10 Then win_CeklisBulan.chk_Oktober.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 11 Then win_CeklisBulan.chk_Nopember.IsEnabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 12 Then win_CeklisBulan.chk_Desember.IsEnabled = False
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

        Dim AmortisasiJanuari As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Januari_"))
        Dim AmortisasiFebruari As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Februari_"))
        Dim AmortisasiMaret As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Maret_"))
        Dim AmortisasiApril As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "April_"))
        Dim AmortisasiMei As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Mei_"))
        Dim AmortisasiJuni As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Juni_"))
        Dim AmortisasiJuli As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Juli_"))
        Dim AmortisasiAgustus As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Agustus_"))
        Dim AmortisasiSeptember As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "September_"))
        Dim AmortisasiOktober As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Oktober_"))
        Dim AmortisasiNopember As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nopember_"))
        Dim AmortisasiDesember As Int64 = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Desember_"))

        'Non-Aktif-kan Ceklis Bulan yang tidak ada angka Jurnal-nya, alias 0 (nol) :
        If AmortisasiJanuari = 0 Then win_CeklisBulan.chk_Januari.IsEnabled = False
        If AmortisasiFebruari = 0 Then win_CeklisBulan.chk_Februari.IsEnabled = False
        If AmortisasiMaret = 0 Then win_CeklisBulan.chk_Maret.IsEnabled = False
        If AmortisasiApril = 0 Then win_CeklisBulan.chk_April.IsEnabled = False
        If AmortisasiMei = 0 Then win_CeklisBulan.chk_Mei.IsEnabled = False
        If AmortisasiJuni = 0 Then win_CeklisBulan.chk_Juni.IsEnabled = False
        If AmortisasiJuli = 0 Then win_CeklisBulan.chk_Juli.IsEnabled = False
        If AmortisasiAgustus = 0 Then win_CeklisBulan.chk_Agustus.IsEnabled = False
        If AmortisasiSeptember = 0 Then win_CeklisBulan.chk_September.IsEnabled = False
        If AmortisasiOktober = 0 Then win_CeklisBulan.chk_Oktober.IsEnabled = False
        If AmortisasiNopember = 0 Then win_CeklisBulan.chk_Nopember.IsEnabled = False
        If AmortisasiDesember = 0 Then win_CeklisBulan.chk_Desember.IsEnabled = False

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
            "Pastikan data amortisasi (jika ada) sudah terposting seluruhnya sampai bulan " & BulanTerceklis_Akhir & " " &
            "sebelum posting jurnal." & Enter2Baris &
            "Lanjutkan posting?"
        If Not TanyaKonfirmasi(Pesan) Then Return

        'SembunyikanSemuaKolomTabel()
        TampilkanData()

        Dim JumlahJurnalTerposting = 0
        Dim TotalPenyusutan As Int64
        Dim JumlahBarisAmortisasiTerbundel

        Dim KodeAsset_Terindeks = Kosongan
        Dim COAAmortisasi_Terindeks = Kosongan
        Dim NamaAkunAmortisasi_Terindeks = Kosongan
        Dim COABiaya_Terindeks = Kosongan

        Dim JurnalSudahAda As Boolean
        Dim BulanAngka_Telusur = BulanAngka_Awal
        Dim NamaBulan = Kosongan
        Dim JumlahPenyusutan As Int64

        Do While BulanAngka_Telusur <= BulanAngka_Akhir

            jur_StatusPenyimpananJurnal_PerBaris = True 'Ini penting, Jangan dihapus.

            TotalPenyusutan = 0
            JumlahBarisAmortisasiTerbundel = 0
            Bundelan = Kosongan

            For Each row As DataRow In datatabelUtama.Rows

                COAAmortisasi_Terindeks = AmbilValueCellTeksBerpotensiDBNull_Row(row, "COA_Amortisasi")

                If AmbilAngka(COAAmortisasi_Terindeks) = AmbilAngka(COAAmortisasi_Terseleksi) Then

                    Id_Terindeks = row("Nomor_ID")
                    KodeAsset_Terindeks = row("Kode_Asset")
                    NamaAkunAmortisasi_Terindeks = row("Nama_Akun_Amortisasi")
                    COABiaya_Terindeks = row("COA_Biaya")
                    JumlahPenyusutan = 0

                    'PesanUntukProgrammer("Nomor Urut : " & row("Nomor_Urut") & Enter2Baris &
                    '                     "COA Biaya Terseleksi : " & COABiaya_Terseleksi & Enter2Baris &
                    '                     "COA Biaya Terindeks : " & COABiaya_Terindeks)

                    Select Case BulanAngka_Telusur
                        Case 1
                            NamaBulan = "Januari"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Januari_"))
                        Case 2
                            NamaBulan = "Februari"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Februari_"))
                        Case 3
                            NamaBulan = "Maret"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Maret_"))
                        Case 4
                            NamaBulan = "April"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("April_"))
                        Case 5
                            NamaBulan = "Mei"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Mei_"))
                        Case 6
                            NamaBulan = "Juni"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Juni_"))
                        Case 7
                            NamaBulan = "Juli"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Juli_"))
                        Case 8
                            NamaBulan = "Agustus"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Agustus_"))
                        Case 9
                            NamaBulan = "September"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("September_"))
                        Case 10
                            NamaBulan = "Oktober"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Oktober_"))
                        Case 11
                            NamaBulan = "Nopember"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Nopember_"))
                        Case 12
                            NamaBulan = "Desember"
                            If COABiaya_Terindeks = COABiaya_Terseleksi Then JumlahPenyusutan = AmbilAngka(row("Desember_"))
                    End Select

                    TanggalTransaksi_Simpan = TanggalFormatSimpan(AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka_Telusur, TahunBukuAktif))

                    AksesDatabase_Transaksi(Buka)
                    cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                                   " WHERE  Valid    <> '" & _X_ & "' " &
                                                   " AND Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
                                                   " AND Bundelan LIKE '%" & KodeAsset_Terindeks & "%' " &
                                                   " AND Tanggal_Transaksi = '" & TanggalTransaksi_Simpan & "' " &
                                                   " AND COA = '" & COABiaya_Terindeks & "' ", KoneksiDatabaseTransaksi)
                    drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
                    If drCEKJURNAL.HasRows Then
                        JurnalSudahAda = True
                    Else
                        JurnalSudahAda = False
                    End If
                    AksesDatabase_Transaksi(Tutup)

                    If JumlahPenyusutan > 0 And JurnalSudahAda = False Then
                        JumlahBarisAmortisasiTerbundel += 1
                        TotalPenyusutan += JumlahPenyusutan
                        If JumlahBarisAmortisasiTerbundel <= 1 Then
                            Bundelan = KodeAsset_Terindeks
                        Else
                            Bundelan = Bundelan & SlashGanda_Pemisah & KodeAsset_Terindeks
                        End If
                    End If

                End If

            Next

            If JumlahBarisAmortisasiTerbundel >= 1 Then
                UraianTransaksi = "Amortisasi " & NamaAkunAmortisasi_Terindeks & " Bulan " & NamaBulan & "."
                SistemPenomoranOtomatis_NomorJV()
                ResetValueJurnal()
                jur_JenisJurnal = JenisJurnal_Amortisasi
                jur_TanggalTransaksi = TanggalTransaksi_Simpan
                jur_Bundelan = Bundelan
                jur_UraianTransaksi = UraianTransaksi
                jur_Direct = 0

                'Simpan Jurnal :
                ___jurDebet(COABiaya_Terseleksi, TotalPenyusutan)
                _______jurKredit(COAAmortisasi_Terseleksi, TotalPenyusutan)


                If jur_StatusPenyimpananJurnal_Lengkap = True Then
                    JumlahJurnalTerposting = JumlahJurnalTerposting + 1
                Else
                    Pesan_Gagal("Terjadi kesalahan pada proses penyimpanan.")
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

        'Sub_FungsiForm_Detail_Rekap()
        TampilkanData()

        If jur_StatusPenyimpananJurnal_PerBaris = True Then
            If JumlahJurnalTerposting = 1 Then
                Pesan_Sukses("'Jurnal Amortisasi' Akun '" & NamaAkunBiaya_Terseleksi &
                       "' Bulan " & BulanTerceklis_Awal & " berhasil diposting.")
            Else
                Pesan_Sukses("'Jurnal Amortisasi' Akun '" & NamaAkunBiaya_Terseleksi &
                       "' berhasil diposting untuk Bulan " & BulanTerceklis_Awal & " - " & BulanTerceklis_Akhir & ".")
            End If
        Else
            If JumlahJurnalTerposting > 0 Then
                Pesan_Peringatan("'Jurnal Amortisasi' Akun '" & NamaAkunBiaya_Terseleksi & "' hanya terposting sebagian." & Enter2Baris & teks_SilakanUlangiLagi_Database)
            Else
                Pesan_Gagal("'Jurnal Amortisasi' Akun '" & NamaAkunBiaya_Terseleksi & "' gagal diposting." & Enter2Baris & teks_SilakanUlangiLagi_Database)
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
                                       " AND Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
                                       " AND Bundelan = '" & KodeAsset_Terseleksi & "' " &
                                       " AND COA = '" & COABiaya_Terseleksi & "' ", KoneksiDatabaseTransaksi)
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

        NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID"))
        KodeAsset_Terseleksi = rowviewUtama("Kode_Asset")
        COAAmortisasi_Terseleksi = rowviewUtama("COA_Amortisasi")
        NamaAkunAmortisasi_Terseleksi = rowviewUtama("Nama_Akun_Amortisasi")
        NamaProduk_Terseleksi = rowviewUtama("Nama_Produk")
        COABiaya_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Biaya")
        NamaAkunBiaya_Terseleksi = rowviewUtama("Nama_Akun_Biaya")
        MasaAmortisasi_Terseleksi = AmbilAngka(rowviewUtama("Masa_Amortisasi"))
        TanggalTransaksi_Terseleksi = rowviewUtama("Tanggal_Transaksi")
        TanggalMulai_Terseleksi = rowviewUtama("Tanggal_Mulai")
        JumlahTransaksi_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Transaksi"))
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        TahunTransaksi_Terseleksi = AmbilAngka(Right(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Transaksi"), 4))

        If TahunLaporan = TahunBukuAktif And JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If Not COABiaya_Terseleksi = Kosongan Then
                btn_LihatJurnal.IsEnabled = True
                KetersediaanTombolPosting(True)
            End If
        Else
            btn_LihatJurnal.IsEnabled = False
            btn_Posting.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If AmbilAngka(e.Row.Item("Januari_")) = 0 Then PewarnaanCellFormatTeks(Januari_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Februari_")) = 0 Then PewarnaanCellFormatTeks(Februari_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Maret_")) = 0 Then PewarnaanCellFormatTeks(Maret_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("April_")) = 0 Then PewarnaanCellFormatTeks(April_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Mei_")) = 0 Then PewarnaanCellFormatTeks(Mei_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Juni_")) = 0 Then PewarnaanCellFormatTeks(Juni_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Juli_")) = 0 Then PewarnaanCellFormatTeks(Juli_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Agustus_")) = 0 Then PewarnaanCellFormatTeks(Agustus_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("September_")) = 0 Then PewarnaanCellFormatTeks(September_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Oktober_")) = 0 Then PewarnaanCellFormatTeks(Oktober_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Nopember_")) = 0 Then PewarnaanCellFormatTeks(Nopember_, e.Row, WarnaPudar_WPF)
        If AmbilAngka(e.Row.Item("Desember_")) = 0 Then PewarnaanCellFormatTeks(Desember_, e.Row, WarnaPudar_WPF)
        Dim KodeAsset = e.Row.Item("Kode_Asset")
        Dim KodeAkun_Biaya = e.Row.Item("COA_Biaya")
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
        BukaDatabaseTransaksi_Kondisional()
        cmdKhusus = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                    " WHERE  Valid    <> '" & _X_ & "' " &
                                    " AND Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
                                    " AND Bundelan = '" & KodeAsset & "' " &
                                    " AND COA = '" & KodeAkun_Biaya & "' ", KoneksiDatabaseTransaksi)
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
        PewarnaanCellFormatTeks(Januari_, e.Row, Warna)
    End Sub





    Private Sub txt_JumlahAsset_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahAmortisasi.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahAmortisasi)
    End Sub





    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_JumlahAmortisasi.IsReadOnly = True
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
    Dim COA_Amortisasi As New DataGridTextColumn
    Dim Nama_Akun_Amortisasi As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim COA_Biaya As New DataGridTextColumn
    Dim Nama_Akun_Biaya As New DataGridTextColumn
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim Tanggal_Mulai As New DataGridTextColumn
    Dim Jumlah_Transaksi As New DataGridTextColumn
    Dim Masa_Amortisasi As New DataGridTextColumn
    Dim Jumlah_Bulan_Penyusutan_Sampai_Tahun_Sebelumnya As New DataGridTextColumn
    Dim Sisa_Bulan_Penyusutan As New DataGridTextColumn
    Dim Bulan_Terakhir_Penyusutan As New DataGridTextColumn
    Dim Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya As New DataGridTextColumn
    Dim Saldo_Awal As New DataGridTextColumn
    Dim Penambahan_Pengurangan As New DataGridTextColumn

    Dim Saldo_Awal_Siap_Untuk_Diamortisasi As New DataGridTextColumn
    Dim Amortisasi_Tahun_Ini As New DataGridTextColumn
    Dim Akumulasi_Amortisasi_Sampai_Dengan_Tahun_Ini As New DataGridTextColumn
    Dim Saldo_Akhir As New DataGridTextColumn
    Dim Selisih_Saldo_Akhir As New DataGridTextColumn
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
    Dim Keterangan_ As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Kode_Asset")
        datatabelUtama.Columns.Add("COA_Amortisasi")
        datatabelUtama.Columns.Add("Nama_Akun_Amortisasi")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("COA_Biaya")
        datatabelUtama.Columns.Add("Nama_Akun_Biaya")
        datatabelUtama.Columns.Add("Tanggal_Transaksi")
        datatabelUtama.Columns.Add("Tanggal_Mulai")
        datatabelUtama.Columns.Add("Jumlah_Transaksi", GetType(Int64))
        datatabelUtama.Columns.Add("Masa_Amortisasi")
        datatabelUtama.Columns.Add("Jumlah_Bulan_Penyusutan_Sampai_Tahun_Sebelumnya", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Bulan_Penyusutan", GetType(Int64))
        datatabelUtama.Columns.Add("Bulan_Terakhir_Penyusutan", GetType(Int64))
        datatabelUtama.Columns.Add("Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Penambahan_Pengurangan", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Awal_Siap_Untuk_Diamortisasi", GetType(Int64))
        datatabelUtama.Columns.Add("Amortisasi_Tahun_Ini", GetType(Int64))
        datatabelUtama.Columns.Add("Akumulasi_Amortisasi_Sampai_Dengan_Tahun_Ini", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Akhir", GetType(Int64))
        datatabelUtama.Columns.Add("Selisih_Saldo_Akhir", GetType(Int64))
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
        datatabelUtama.Columns.Add("Keterangan_")


        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor_ID", 75, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Asset, "Kode_Asset", "Kode Asset", 75, FormatString, KiriTengah, KunciUrut, TerlihatKhususProgrammer)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Amortisasi, "COA_Amortisasi", "Kode" & Enter1Baris & "Akun", 45, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun_Amortisasi, "Nama_Akun_Amortisasi", "Nama Akun Amortisasi", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Produk", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Biaya, "COA_Biaya", "COA Biaya Penyusutan", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun_Biaya, "Nama_Akun_Biaya", "Nama Akun" & Enter1Baris & "Penyusutan", 270, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Transaksi, "Tanggal_Transaksi", "Tanggal Transaksi", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Mulai, "Tanggal_Mulai", "Tanggal Mulai Amortisasi", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Transaksi, "Jumlah_Transaksi", "Jumlah" & Enter1Baris & "Transaksi", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Masa_Amortisasi, "Masa_Amortisasi", "Masa Amortisasi", 75, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bulan_Penyusutan_Sampai_Tahun_Sebelumnya, "Jumlah_Bulan_Penyusutan_Sampai_Tahun_Sebelumnya", "1", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Bulan_Penyusutan, "Sisa_Bulan_Penyusutan", "2", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Bulan_Terakhir_Penyusutan, "Bulan_Terakhir_Penyusutan", "3", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya, "Akumulasi_Penyusutan_Sampai_Tahun_Sebelumnya", "4", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal, "Saldo_Awal", "Saldo Awal", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Penambahan_Pengurangan, "Penambahan_Pengurangan", "Penambahan/" & Enter1Baris & "Pengurangan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal_Siap_Untuk_Diamortisasi, "Saldo_Awal_Siap_Untuk_Diamortisasi", "Saldo Awal" & Enter1Baris & "Siap untuk" & Enter1Baris & "Diamortisasi", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Amortisasi_Tahun_Ini, "Amortisasi_Tahun_Ini", "Amortisasi Tahun Ini", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Akumulasi_Amortisasi_Sampai_Dengan_Tahun_Ini, "Akumulasi_Amortisasi_Sampai_Dengan_Tahun_Ini", "Akumulasi Amortisasi" & Enter1Baris & "s.d. Tahun Ini", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Akhir, "Saldo_Akhir", "Saldo Akhir", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Selisih_Saldo_Akhir, "Selisih_Saldo_Akhir", "Selisih Saldo Akhir", 63, FormatAngka, KananTengah, KunciUrut, TerlihatKhususProgrammer)
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
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Terlihat)

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
        txt_JumlahAmortisasi.IsReadOnly = True
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
