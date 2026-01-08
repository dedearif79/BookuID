Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_DaftarAmortisasiBiaya_X

    Public FungsiForm
    Dim JudulForm
    Dim TahunLaporan
    Dim TahunLaporanSebelumnya
    Dim TahunLaporanTerakhir
    Dim JumlahBaris

    'Variabel untuk Data Terseleksi :
    Dim BarisTerseleksi
    Dim Id_Terseleksi
    Dim KodeAsset_Terseleksi
    Dim COAAmortisasi_Terseleksi
    Dim NamaAkunAmortisasi_Terseleksi
    Dim COABiaya_Terseleksi
    Dim NamaAkunBiaya_Terseleksi
    Dim MasaAmortisasi_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim Keterangan_Terseleksi
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

    Private Sub frm_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            pnl_CRUD.Enabled = True
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            pnl_CRUD.Enabled = False
        End If

        RefreshTampilanData()

        BeginInvoke(Sub() BersihkanSeleksi())

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        KontenComboTahunLaporan()
    End Sub

    Sub TampilkanData()

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
        Dim NomorUrut = 0
        Dim IdAmortisasi
        Dim KodeAsset
        Dim MasaAmortisasi
        Dim KodeAkun_Amortisasi
        Dim NamaAkun_Amortisasi
        Dim KodeAkun_Biaya
        Dim NamaAkun_BIaya
        Dim TanggalTransaksi
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
        Dim SelisihSaldoAkhir As Integer = 0
        Dim Januari, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, Nopember, Desember
        Dim AkumulasiAmortisasiTahunLaporan As Int64
        Dim AkumulasiAmortisasiSampaiDengan As Int64
        Dim Keterangan

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
            KodeAkun_Biaya = dr.Item("COA_Biaya")
            NamaAkun_BIaya = dr.Item("Nama_Akun_Biaya")
            MasaAmortisasi = dr.Item("Masa_Amortisasi") & " Bulan"
            JumlahMaksimalBulanPenyusutan = AmbilAngka(MasaAmortisasi)
            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            HariTransaksi = AmbilAngka(Format(dr.Item("Tanggal_Transaksi"), "dd"))
            BulanTransaksi = AmbilAngka(Format(dr.Item("Tanggal_Transaksi"), "MM"))
            TahunTransaksi = AmbilAngka(Format(dr.Item("Tanggal_Transaksi"), "yyyy"))
            JumlahTransaksi = dr.Item("Jumlah_Transaksi")

            BulanDimulaiAmortisasi = BulanTransaksi
            TahunDimulaiAmortisasi = TahunTransaksi
            TahunSelisih = TahunLaporanSebelumnya

            'Jika Transaksi terjadi diatas tanggal 15, maka Penyusutan dimulai pada bulan berikutnya.
            If HariTransaksi > 15 Then
                BulanDimulaiAmortisasi = BulanTransaksi + 1
                'Logika ini akan mengakibatkan value menjadi 13 jika tanggal transaksi di atas tanggal 15-Des.
                'Value 13 akan dirubah menjadi 1 pada tahap berikutnya.
                If BulanDimulaiAmortisasi > 12 Then
                    TahunDimulaiAmortisasi = TahunTransaksi + 1
                    TahunSelisih = TahunLaporanSebelumnya + 1
                End If
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
                If BulanDimulaiAmortisasi > 12 Then Desember = 0
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

            Jml_Januari = Jml_Januari + Januari
            Jml_Februari = Jml_Februari + Februari
            Jml_Maret = Jml_Maret + Maret
            Jml_April = Jml_April + April
            Jml_Mei = Jml_Mei + Mei
            Jml_Juni = Jml_Juni + Juni
            Jml_Juli = Jml_Juli + Juli
            Jml_Agustus = Jml_Agustus + Agustus
            Jml_September = Jml_September + September
            Jml_Oktober = Jml_Oktober + Oktober
            Jml_Nopember = Jml_Nopember + Nopember
            Jml_Desember = Jml_Desember + Desember

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

            Keterangan = dr.Item("Keterangan")

            If AkumulasiAmortisasiTahunLaporan > 0 Then

                NomorUrut = NomorUrut + 1
                Jml_JumlahTransaksi = Jml_JumlahTransaksi + JumlahTransaksi
                Jml_AkumulasiAmortisasiSampaiTahunSebelumnya = Jml_AkumulasiAmortisasiSampaiTahunSebelumnya + AkumulasiPenyusutanSampaiTahunSebelumnya
                Jml_SaldoAwal = Jml_SaldoAwal + SaldoAwal
                Jml_PenambahanPengurangan = Jml_PenambahanPengurangan + PenambahanPengurangan
                Jml_SaldoAwalSiapUntukDiamortisasi = Jml_SaldoAwalSiapUntukDiamortisasi + SaldoAwalSiapUntukDiamortisasi
                Jml_AkumulasiAmortisasiTahunLaporan = Jml_AkumulasiAmortisasiTahunLaporan + AkumulasiAmortisasiTahunLaporan
                Jml_AkumulasiAmortisasiSampaiDengan = Jml_AkumulasiAmortisasiSampaiDengan + AkumulasiAmortisasiSampaiDengan
                Jml_SaldoAkhir = Jml_SaldoAkhir + SaldoAkhir

                DataTabelUtama.Rows.Add(NomorUrut, IdAmortisasi, KodeAsset, KodeAkun_Amortisasi, NamaAkun_Amortisasi, KodeAkun_Biaya, NamaAkun_BIaya, TanggalTransaksi,
                                      JumlahTransaksi, MasaAmortisasi, JumlahBulanPenyusutanSampaiTahunSebelumnya, SisaBulanPenyusutan, BulanTerakhirAmortisasi,
                                      AkumulasiPenyusutanSampaiTahunSebelumnya, SaldoAwal, PenambahanPengurangan, SaldoAwalSiapUntukDiamortisasi,
                                      AkumulasiAmortisasiTahunLaporan, AkumulasiAmortisasiSampaiDengan, SaldoAkhir, SelisihSaldoAkhir,
                                      Januari, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, Nopember, Desember,
                                      Keterangan)

                'Ini untuk membiaskan warna huruf yang value-nya 0
                Dim IndexBaris = NomorUrut - 1
                If AmbilAngka(DataTabelUtama.Item("Januari_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("Januari_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Februari_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("Februari_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Maret_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("Maret_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("April_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("April_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Mei_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("Mei_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Juni_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("Juni_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Juli_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("Juli_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Agustus_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("Agustus_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("September_", IndexBaris).Value) = 0 Then DataTabelUtama.Item("September_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Oktober_", IndexBaris).Value = 0) Then DataTabelUtama.Item("Oktober_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Nopember_", IndexBaris).Value = 0) Then DataTabelUtama.Item("Nopember_", IndexBaris).Style.ForeColor = WarnaPudar
                If AmbilAngka(DataTabelUtama.Item("Desember_", IndexBaris).Value = 0) Then DataTabelUtama.Item("Desember_", IndexBaris).Style.ForeColor = WarnaPudar

                'Cek Data Jurnal :
                If TahunLaporan = TahunBukuAktif Then
                    Dim AkumulasiAmortisasiTahunLaporan_BerdasarkanJurnal As Int64 = 0
                    AksesDatabase_Transaksi(Buka)
                    cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                                   " WHERE Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
                                                   " AND Bundelan = '" & KodeAsset & "' " &
                                                   " AND COA = '" & KodeAkun_Biaya & "' ", KoneksiDatabaseTransaksi)
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
                        AkumulasiAmortisasiTahunLaporan_BerdasarkanJurnal += AmbilAngka(drCEKJURNAL.Item("Jumlah_Debet"))
                    Loop
                    AksesDatabase_Transaksi(Tutup)
                    Dim AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan As Int64 = AkumulasiAmortisasiTahunLaporan
                    If TahunLaporan = TahunIni Then
                        If AmbilAngka(BulanIni) = 1 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Januari - Februari - Maret - April - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 2 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Februari - Maret - April - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 3 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Maret - April - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 4 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - April - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 5 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Mei - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 6 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Juni - Juli - Agustus - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 7 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Juli - Agustus - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 8 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Agustus - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 9 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - September - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 10 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Oktober - Nopember - Desember
                        If AmbilAngka(BulanIni) = 11 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Nopember - Desember
                        If AmbilAngka(BulanIni) = 12 Then AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan = AkumulasiAmortisasiTahunLaporan - Desember
                    End If
                    If AkumulasiAmortisasiTahunLaporan_SampaiBulanBerjalan <> AkumulasiAmortisasiTahunLaporan_BerdasarkanJurnal Then KesesuaianJurnal = False
                End If

            End If

        Loop 'Ujung Loop Tampilan

        AksesDatabase_General(Tutup)

        JumlahBaris = DataTabelUtama.RowCount

        If JumlahBaris > 0 Then
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "      J U M L A H", Nothing,
                                  Jml_JumlahTransaksi, Nothing, Nothing, Nothing, Nothing,
                                  Nothing, Jml_SaldoAwal, Jml_PenambahanPengurangan, Jml_SaldoAwalSiapUntukDiamortisasi,
                                  Jml_AkumulasiAmortisasiTahunLaporan, Jml_AkumulasiAmortisasiSampaiDengan, Jml_SaldoAkhir, Nothing,
                                  Jml_Januari, Jml_Februari, Jml_Maret, Jml_April, Jml_Mei, Jml_Juni, Jml_Juli, Jml_Agustus, Jml_September, Jml_Oktober, Jml_Nopember, Jml_Desember)
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
        btn_Posting.Enabled = False
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

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_TahunLaporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TahunLaporan.SelectedIndexChanged
        cmb_TahunLaporan_TextChanged(sender, e)
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
        TampilkanData()
        JudulForm = "Daftar Amortisasi Biaya - Tahun " & TahunLaporan
        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If TahunLaporan = TahunBukuAktif Then
                btn_Tambah.Enabled = True
            Else
                btn_Tambah.Enabled = False
            End If
        Else
            If TahunLaporan <= TahunCutOff Then
                btn_Tambah.Enabled = True
            Else
                btn_Tambah.Enabled = False
            End If
        End If
    End Sub

    Private Sub btn_Import_Click(sender As Object, e As EventArgs) Handles btn_Import.Click
        FiturBelumBisaDigunakan()
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        'frm_InputAmortisasiBiaya.ResetForm()
        'frm_InputAmortisasiBiaya.JalurMasuk = Halaman_AMORTISASIBIAYA
        'frm_InputAmortisasiBiaya.FungsiForm = FungsiForm_TAMBAH
        'frm_InputAmortisasiBiaya.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        'frm_InputAmortisasiBiaya.ResetForm()
        'frm_InputAmortisasiBiaya.JalurMasuk = Halaman_AMORTISASIBIAYA
        'frm_InputAmortisasiBiaya.FungsiForm = FungsiForm_EDIT
        'frm_InputAmortisasiBiaya.IdAmortisasi = Id_Terseleksi
        'frm_InputAmortisasiBiaya.txt_KodeAsset.Text = KodeAsset_Terseleksi
        'frm_InputAmortisasiBiaya.KodeAsset_SebelumDiedit = KodeAsset_Terseleksi
        'frm_InputAmortisasiBiaya.txt_COA_Amortisasi.Text = COAAmortisasi_Terseleksi
        'frm_InputAmortisasiBiaya.txt_NamaAkun_Amortisasi.Text = NamaAkunAmortisasi_Terseleksi
        'frm_InputAmortisasiBiaya.txt_COA_Biaya.Text = COABiaya_Terseleksi
        'frm_InputAmortisasiBiaya.txt_NamaAkun_Biaya.Text = NamaAkunBiaya_Terseleksi
        'frm_InputAmortisasiBiaya.txt_MasaAmortisasi.Text = MasaAmortisasi_Terseleksi
        'frm_InputAmortisasiBiaya.dtp_TanggalTransaksi.Text = TanggalTransaksi_Terseleksi
        'frm_InputAmortisasiBiaya.txt_JumlahTransaksi.Text = JumlahTransaksi_Terseleksi
        'frm_InputAmortisasiBiaya.txt_Keterangan.Text = Keterangan_Terseleksi
        'frm_InputAmortisasiBiaya.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        'Hapus Data Amortisasi Biaya :
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" DELETE from tbl_AmortisasiBiaya " &
                              " WHERE Kode_Asset = '" & KodeAsset_Terseleksi & "' ",
                              KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_General(Tutup)

        'Hapus Data Jurnal :
        If StatusSuntingDatabase = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" DELETE from tbl_Transaksi " &
                                  " WHERE Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
                                  " AND Bundelan = '" & KodeAsset_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        If StatusSuntingDatabase = True Then
            MsgBox("Data terpilih BERHASIL dihapus.")
            TampilkanData()
        Else
            MsgBox("Data terpilih GAGAL dihapus." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        Id_Terseleksi = DataTabelUtama.Item("Nomor_ID", BarisTerseleksi).Value
        KodeAsset_Terseleksi = DataTabelUtama.Item("Kode_Asset", BarisTerseleksi).Value
        COAAmortisasi_Terseleksi = DataTabelUtama.Item("COA_Amortisasi", BarisTerseleksi).Value
        NamaAkunAmortisasi_Terseleksi = DataTabelUtama.Item("Nama_Akun_Amortisasi", BarisTerseleksi).Value
        COABiaya_Terseleksi = DataTabelUtama.Item("COA_Biaya", BarisTerseleksi).Value
        NamaAkunBiaya_Terseleksi = DataTabelUtama.Item("Nama_Akun_Biaya", BarisTerseleksi).Value
        MasaAmortisasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Masa_Amortisasi", BarisTerseleksi).Value)
        TanggalTransaksi_Terseleksi = DataTabelUtama.Item("Tanggal_Transaksi", BarisTerseleksi).Value
        JumlahTransaksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Transaksi", BarisTerseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", BarisTerseleksi).Value
        TahunTransaksi_Terseleksi = AmbilAngka(Microsoft.VisualBasic.Right(DataTabelUtama("Tanggal_Transaksi", BarisTerseleksi).Value, 4))

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

    End Sub

    Private Sub DataTabelUtama_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles DataTabelUtama.CellFormatting
        If e.ColumnIndex = 0 Then
            e.CellStyle.ForeColor = WarnaPudar
        End If
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        ops_PilihJurnal_DataAsset.ResetForm()
        Dim AdaJurnal = 0
        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
                                       " AND Bundelan = '" & KodeAsset_Terseleksi & "' " &
                                       " AND COA = '" & COABiaya_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Do While drCEKJURNAL.Read
            Dim TanggalJurnalPenyusutan = drCEKJURNAL.Item("Tanggal_Transaksi")
            Dim BulanJurnalPenyusutan = Format(TanggalJurnalPenyusutan, "MM")
            If AmbilAngka(BulanJurnalPenyusutan) = 1 Then
                ops_PilihJurnal_DataAsset.rdb_Januari.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Januari = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 2 Then
                ops_PilihJurnal_DataAsset.rdb_Februari.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Februari = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 3 Then
                ops_PilihJurnal_DataAsset.rdb_Maret.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Maret = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 4 Then
                ops_PilihJurnal_DataAsset.rdb_April.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_April = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 5 Then
                ops_PilihJurnal_DataAsset.rdb_Mei.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Mei = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 6 Then
                ops_PilihJurnal_DataAsset.rdb_Juni.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Juni = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 7 Then
                ops_PilihJurnal_DataAsset.rdb_Juli.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Juli = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 8 Then
                ops_PilihJurnal_DataAsset.rdb_Agustus.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Agustus = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 9 Then
                ops_PilihJurnal_DataAsset.rdb_September.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_September = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 10 Then
                ops_PilihJurnal_DataAsset.rdb_Oktober.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Oktober = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 11 Then
                ops_PilihJurnal_DataAsset.rdb_Nopember.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Nopember = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
            If AmbilAngka(BulanJurnalPenyusutan) = 12 Then
                ops_PilihJurnal_DataAsset.rdb_Desember.Enabled = True
                ops_PilihJurnal_DataAsset.AngkaNomorJV_Desember = drCEKJURNAL.Item("Nomor_JV")
                AdaJurnal = AdaJurnal + 1
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)
        If AdaJurnal > 0 Then
            ops_PilihJurnal_DataAsset.ShowDialog()
        Else
            MsgBox("Tidak/Belum ada Jurnal pada Tahun Buku ini untuk data terpilih.")
        End If
    End Sub

    Dim Id_Terindeks
    Private Sub btn_Posting_Click(sender As Object, e As EventArgs) Handles btn_Posting.Click

        ''Cek Apakah Data Sudah di-Jurnal lengkap :
        'Dim BulanPenjurnalan
        'Dim BahanKeterangan = Nothing
        'Dim PesanKhusus = "Data terpilih sudah diposting ke Jurnal"
        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
        '                      " WHERE Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
        '                      " AND Bundelan = '" & KodeAsset_Terseleksi & "' ",
        '                      KoneksiDatabaseTransaksi)
        'dr = cmd.ExecuteReader
        'Do While dr.Read
        '    BulanPenjurnalan = Format(dr.Item("Tanggal_Transaksi"), "MM")
        '    BahanKeterangan = dr.Item("Uraian_Transaksi")
        '    If TahunBukuAktif = TahunIni Then
        '        If AmbilAngka(BulanPenjurnalan) = (AmbilAngka(BulanIni) - 1) Then
        '            MsgBox(PesanKhusus)
        '            Return
        '        End If
        '    Else
        '        If AmbilAngka(BulanPenjurnalan) = 12 Then
        '            MsgBox(PesanKhusus)
        '            Return
        '        End If
        '    End If
        'Loop
        'AksesDatabase_Transaksi(Tutup)

        ''Buka Form untuk mengisi Uraian/Keterangan Jurnal :
        'frm_PostingJurnal_DataAsset.ResetForm()
        'frm_PostingJurnal_DataAsset.txt_Keterangan.Text = BahanKeterangan
        'frm_PostingJurnal_DataAsset.ShowDialog()

        'If frm_PostingJurnal_DataAsset.LanjutkanPosting = False Then Return

        'Bundelan = KodeAsset_Terseleksi
        'UraianTransaksi = frm_PostingJurnal_DataAsset.txt_Keterangan.Text
        'Dim JurnalSudahAda As Boolean
        'Dim NomorBulan = 1
        'Dim NamaBulan = Nothing
        'Dim JumlahPenyusutan As Int64
        'Dim JumlahJurnalTerposting = 0

        'Do While NomorBulan <= 12

        '    jur_StatusPenyimpananJurnal_PerBaris = True 'Ini penting, Jangan dihapus.

        '    Select Case NomorBulan
        '        Case 1
        '            NamaBulan = "Januari"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Januari_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "01" & "-" & "31"
        '        Case 2
        '            NamaBulan = "Februari"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Februari_", BarisTerseleksi).Value)
        '            If TahunKabisat_TahunBukuAktif = False Then TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "02" & "-" & "28"
        '            If TahunKabisat_TahunBukuAktif = True Then TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "02" & "-" & "29"
        '        Case 3
        '            NamaBulan = "Maret"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Maret_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "03" & "-" & "31"
        '        Case 4
        '            NamaBulan = "April"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("April_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "04" & "-" & "30"
        '        Case 5
        '            NamaBulan = "Mei"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Mei_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "05" & "-" & "31"
        '        Case 6
        '            NamaBulan = "Juni"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Juni_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "06" & "-" & "30"
        '        Case 7
        '            NamaBulan = "Juli"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Juli_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "07" & "-" & "31"
        '        Case 8
        '            NamaBulan = "Agustus"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Agustus_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "08" & "-" & "31"
        '        Case 9
        '            NamaBulan = "September"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("September_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "09" & "-" & "30"
        '        Case 10
        '            NamaBulan = "Oktober"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Oktober_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "10" & "-" & "31"
        '        Case 11
        '            NamaBulan = "Nopember"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Nopember_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "11" & "-" & "30"
        '        Case 12
        '            NamaBulan = "Desember"
        '            JumlahPenyusutan = AmbilAngka(DataTabelUtama.Item("Desember_", BarisTerseleksi).Value)
        '            TanggalTransaksi_Simpan = TahunBukuAktif & "-" & "12" & "-" & "31"
        '    End Select

        '    AksesDatabase_Transaksi(Buka)
        '    cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " & _
        '                          " WHERE Tanggal_Transaksi = '" & TanggalTransaksi_Simpan & "' " & _
        '                          " AND Bundelan = '" & KodeAsset_Terseleksi & "' ", _
        '                          KoneksiDatabaseTransaksi)
        '    dr = cmd.ExecuteReader
        '    If dr.HasRows Then
        '        JurnalSudahAda = True
        '    Else
        '        JurnalSudahAda = False
        '    End If
        '    AksesDatabase_Transaksi(Tutup)

        '    If JumlahPenyusutan > 0 And JurnalSudahAda = False Then
        '        SistemPenomoranOtomatis_NomorJV()
        '        ResetValueJurnal()
        '        jur_JenisJurnal = JenisJurnal_Amortisasi
        '        jur_TanggalTransaksi = TanggalTransaksi_Simpan
        '        jur_Bundelan = Bundelan
        '        jur_UraianTransaksi = UraianTransaksi
        '        jur_Direct = 0

        '        'Simpan Jurnal :
        '        ___jurDebet(COABiaya_Terseleksi, JumlahPenyusutan)
        '        _______jurKredit(COAAmortisasi_Terseleksi, JumlahPenyusutan)

        '        If jur_StatusPenyimpananJurnal_Lengkap = True Then
        '            JumlahJurnalTerposting = JumlahJurnalTerposting + 1
        '        Else
        '            If JumlahJurnalTerposting = 0 Then MsgBox("'Data Penyusutan' gagal diposting ke Jurnal." & Enter2Baris & teks_SilakanCobaLagi_Database)
        '            If JumlahJurnalTerposting > 0 Then
        '                MsgBox("'Data Penyusutan' hanya terposting sebagian." & Enter2Baris & teks_SilakanUlangiLagi_Database)
        '                TampilkanData()
        '            End If
        '            Exit Do
        '        End If
        '    End If

        '    NomorBulan = NomorBulan + 1

        '    'Syntax di bawah ini berfungsi untuk mencegah penjurnalan pada Bulan Berjalan di Tahun Berjalan
        '    'Sementara untuk bulan yang sama di tahun yang berbeda (sebelumnya) tetap dijurnal.
        '    'Dengan kata lain, pada tahun sebelum tahun berjalan, penjurnalan dilakukan secara lengkap (12 Bulan)
        '    If TahunBukuAktif = TahunIni And NomorBulan = BulanIni Then Exit Do

        'Loop

        'If jur_StatusPenyimpananJurnal_PerBaris = True Then
        '    If JumlahJurnalTerposting = 0 Then MsgBox("Tidak ada 'Data Penyusutan' yang terposting ke Jurnal pada Event ini.")
        '    If JumlahJurnalTerposting > 0 Then
        '        MsgBox("'Data Penyusutan' berhasil diposting ke Jurnal sampai bulan " & NamaBulan & " " & TahunBukuAktif & ".")
        '        TampilkanData()
        '    End If
        'End If


        frm_CeklisBulan.ResetForm()

        'Non-Aktif-kan Ceklis Bulan yang sudah dijurnal :
        AksesDatabase_Transaksi(Buka)
        cmdCEKJURNAL = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                       " WHERE COA = '" & COABiaya_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        drCEKJURNAL = cmdCEKJURNAL.ExecuteReader
        Do While drCEKJURNAL.Read
            Dim TanggalJurnalAmortisasi = drCEKJURNAL.Item("Tanggal_Transaksi")
            Dim BulanJurnalAmortisasi = AmbilBulanAngka_DariTanggal(TanggalJurnalAmortisasi)
            If AmbilAngka(BulanJurnalAmortisasi) = 1 Then frm_CeklisBulan.chk_Januari.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 2 Then frm_CeklisBulan.chk_Februari.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 3 Then frm_CeklisBulan.chk_Maret.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 4 Then frm_CeklisBulan.chk_April.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 5 Then frm_CeklisBulan.chk_Mei.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 6 Then frm_CeklisBulan.chk_Juni.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 7 Then frm_CeklisBulan.chk_Juli.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 8 Then frm_CeklisBulan.chk_Agustus.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 9 Then frm_CeklisBulan.chk_September.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 10 Then frm_CeklisBulan.chk_Oktober.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 11 Then frm_CeklisBulan.chk_Nopember.Enabled = False
            If AmbilAngka(BulanJurnalAmortisasi) = 12 Then frm_CeklisBulan.chk_Desember.Enabled = False
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

        Pilihan = MessageBox.Show("Pastikan data amortisasi (jika ada) sudah terposting seluruhnya sampai bulan " & BulanTerceklis_Akhir & " " &
                                  "sebelum posting jurnal...!!!" & Enter2Baris &
                                  "Lanjutkan posting..?",
                                  "PERHATIAN..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        'SembunyikanSemuaKolomTabel()
        TampilkanData()

        Dim JumlahJurnalTerposting = 0
        Dim TotalPenyusutan As Int64
        Dim JumlahBarisAmortisasiTerbundel

        Dim KodeAsset_Terindeks = Nothing
        Dim COAAmortisasi_Terindeks = Nothing
        Dim NamaAkunAmortisasi_Terindeks = Nothing
        Dim COABiaya_Terindeks = Nothing

        Dim JurnalSudahAda As Boolean
        Dim BulanAngka_Telusur = BulanAngka_Awal
        Dim NamaBulan = Kosongan
        Dim JumlahPenyusutan As Int64

        Do While BulanAngka_Telusur <= BulanAngka_Akhir

            jur_StatusPenyimpananJurnal_PerBaris = True 'Ini penting, Jangan dihapus.

            TotalPenyusutan = 0
            JumlahBarisAmortisasiTerbundel = 0
            Bundelan = Nothing

            For Each row As DataGridViewRow In DataTabelUtama.Rows

                COAAmortisasi_Terindeks = row.Cells("COA_Amortisasi").Value

                If AmbilAngka(COAAmortisasi_Terindeks) = AmbilAngka(COAAmortisasi_Terseleksi) Then

                    Id_Terindeks = row.Cells("Nomor_ID").Value
                    KodeAsset_Terindeks = row.Cells("Kode_Asset").Value
                    NamaAkunAmortisasi_Terindeks = row.Cells("Nama_Akun_Amortisasi").Value
                    COABiaya_Terindeks = row.Cells("COA_Biaya").Value

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
                                                   " WHERE Jenis_Jurnal = '" & JenisJurnal_Amortisasi & "' " &
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
                        JumlahBarisAmortisasiTerbundel = JumlahBarisAmortisasiTerbundel + 1
                        TotalPenyusutan = TotalPenyusutan + JumlahPenyusutan
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

        'Sub_FungsiForm_Detail_Rekap()
        TampilkanData()

        If jur_StatusPenyimpananJurnal_PerBaris = True Then
            If JumlahJurnalTerposting = 1 Then
                MsgBox("'Jurnal Amortisasi' Akun '" & NamaAkunBiaya_Terseleksi &
                       "' Bulan " & BulanTerceklis_Awal & " BERHASIL diposting.")
            Else
                MsgBox("'Jurnal Amortisasi' Akun '" & NamaAkunBiaya_Terseleksi &
                       "' BERHASIL diposting untuk Bulan " & BulanTerceklis_Awal & " - " & BulanTerceklis_Akhir & ".")
            End If
        Else
            If JumlahJurnalTerposting > 0 Then
                MsgBox("'Jurnal Amortisasi' Akun '" & NamaAkunBiaya_Terseleksi & "' hanya terposting sebagian." & Enter2Baris & teks_SilakanUlangiLagi_Database)
            Else
                MsgBox("'Jurnal Amortisasi' Akun '" & NamaAkunBiaya_Terseleksi & "' GAGAL diposting." & Enter2Baris & teks_SilakanUlangiLagi_Database)
            End If
        End If


    End Sub

End Class