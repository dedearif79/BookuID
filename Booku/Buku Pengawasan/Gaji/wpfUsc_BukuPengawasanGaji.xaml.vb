Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfUsc_BukuPengawasanGaji

    Public StatusAktif As Boolean = False

    Public JudulForm = Kosongan
    Public NamaHalaman
    Public COAHutang
    Dim Total_SisaHutang As Int64
    Dim TotalTabel As Int64


    Public JenisTampilan
    Public JenisTampilan_DETAIL = "DETAIL"
    Public JenisTampilan_REKAP = "REKAP"
    Public JenisTampilan_ALL = "ALL"
    Public Bulan_ALL = JenisTampilan_ALL
    Dim Bulan_REKAP = JenisTampilan_REKAP

    Dim Bulan
    Dim NomorBulan
    Dim JumlahBarisDalamSatuBulan

    Dim NomorUrut = 0
    Dim MaxLooping
    Dim NomorID
    Dim NomorBPHG
    Dim TanggalTransaksi
    Dim GajiBagianProduksi As Int64
    Dim GajiBagianProduksi2 As Int64
    Dim GajiBagianProduksi3 As Int64
    Dim GajiBagianProduksi4 As Int64
    Dim ThrBonusProduksi As Int64
    Dim TunjanganPPh21Produksi As Int64
    Dim BpjsTkJkkJkmProduksi As Int64
    Dim BpjsTkJhtIpProduksi As Int64
    Dim BpjsKesehatanProduksi As Int64
    Dim AsuransiKaryawanProduksi As Int64
    Dim PesangonKaryawanProduksi As Int64
    Dim JumlahGajiBagianProduksi As Int64
    Dim BpjsTkJhtIpProduksiDibayarKaryawan As Int64
    Dim BpjsKesehatanProduksiDibayarKaryawan As Int64
    Dim GajiBagianAdministrasi As Int64
    Dim GajiBagianAdministrasi2 As Int64
    Dim GajiBagianAdministrasi3 As Int64
    Dim GajiBagianAdministrasi4 As Int64
    Dim ThrBonusAdministrasi As Int64
    Dim TunjanganPPh21Administrasi As Int64
    Dim BpjsTkJkkJkmAdministrasi As Int64
    Dim BpjsTkJhtIpAdministrasi As Int64
    Dim BpjsKesehatanAdministrasi As Int64
    Dim AsuransiKaryawanAdministrasi As Int64
    Dim PesangonKaryawanAdministrasi As Int64
    Dim JumlahGajiBagianAdministrasi As Int64
    Dim BpjsTkJhtIpAdministrasiDibayarKaryawan As Int64
    Dim BpjsKesehatanAdministrasiDibayarKaryawan As Int64
    'Dim TunjanganPPhPasal21 As int64
    'Dim BiayaBpjsKesehatan As int64
    'Dim BiayaBpjsKetenagakerjaan As int64
    Dim JumlahGajiKotor As Int64
    Dim PotonganHutangBpjsKesehatan As Int64
    Dim PotonganHutangBpjsKetenagakerjaan As Int64
    Dim PotonganHutangKoperasi As Int64
    Dim PotonganHutangPPhPasal21Rutin As Int64
    Dim PotonganHutangPPhPasal21Pesangon As Int64
    Dim PotonganHutangSerikat As Int64
    Dim PotonganKasbonKaryawan As Int64
    Dim PotonganLainnya As Int64
    Dim JumlahPotongan As Int64
    Dim JumlahGajiDibayarkan As Int64
    Dim PPhDitanggungRutin As Int64
    Dim PPhDitanggungPesangon As Int64
    Dim JumlahPembayaran As Int64
    Dim SisaPembayaran As Int64
    Dim NomorJV_Gaji As Int64
    Dim Keterangan

    Dim RekapPerBulan_GajiBagianProduksi As Int64
    Dim RekapPerBulan_GajiBagianProduksi2 As Int64
    Dim RekapPerBulan_GajiBagianProduksi3 As Int64
    Dim RekapPerBulan_GajiBagianProduksi4 As Int64
    Dim RekapPerBulan_ThrBonusProduksi As Int64
    Dim RekapPerBulan_TunjanganPPh21Produksi As Int64
    Dim RekapPerBulan_BpjsTkJkkJkmProduksi As Int64
    Dim RekapPerBulan_BpjsTkJhtIpProduksi As Int64
    Dim RekapPerBulan_BpjsKesehatanProduksi As Int64
    Dim RekapPerBulan_AsuransiKaryawanProduksi As Int64
    Dim RekapPerBulan_PesangonKaryawanProduksi As Int64
    Dim RekapPerBulan_JumlahGajiBagianProduksi As Int64
    Dim RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan As Int64
    Dim RekapPerBulan_BpjsKesehatanProduksiDibayarKaryawan As Int64
    Dim RekapPerBulan_GajiBagianAdministrasi As Int64
    Dim RekapPerBulan_GajiBagianAdministrasi2 As Int64
    Dim RekapPerBulan_GajiBagianAdministrasi3 As Int64
    Dim RekapPerBulan_GajiBagianAdministrasi4 As Int64
    Dim RekapPerBulan_ThrBonusAdministrasi As Int64
    Dim RekapPerBulan_TunjanganPPh21Administrasi As Int64
    Dim RekapPerBulan_BpjsTkJkkJkmAdministrasi As Int64
    Dim RekapPerBulan_BpjsTkJhtIpAdministrasi As Int64
    Dim RekapPerBulan_BpjsKesehatanAdministrasi As Int64
    Dim RekapPerBulan_AsuransiKaryawanAdministrasi As Int64
    Dim RekapPerBulan_PesangonKaryawanAdministrasi As Int64
    Dim RekapPerBulan_JumlahGajiBagianAdministrasi As Int64
    Dim RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan As Int64
    Dim RekapPerBulan_BpjsKesehatanAdministrasiDibayarKaryawan As Int64
    'Dim RekapPerBulan_TunjanganPPhPasal21 As int64
    'Dim RekapPerBulan_BiayaBpjsKesehatan As int64
    'Dim RekapPerBulan_BiayaBpjsKetenagakerjaan As int64
    Dim RekapPerBulan_JumlahGajiKotor As Int64
    Dim RekapPerBulan_PotonganHutangBpjsKesehatan As Int64
    Dim RekapPerBulan_PotonganHutangBpjsKetenagakerjaan As Int64
    Dim RekapPerBulan_PotonganHutangKoperasi As Int64
    Dim RekapPerBulan_PotonganHutangPPhPasal21Rutin As Int64
    Dim RekapPerBulan_PotonganHutangPPhPasal21Pesangon As Int64
    Dim RekapPerBulan_PotonganHutangSerikat As Int64
    Dim RekapPerBulan_PotonganKasbonKaryawan As Int64
    Dim RekapPerBulan_PotonganLainnya As Int64
    Dim RekapPerBulan_JumlahPotongan As Int64
    Dim RekapPerBulan_JumlahGajiDibayarkan As Int64
    Dim RekapPerBulan_PPhDitanggungRutin As Int64
    Dim RekapPerBulan_PPhDitanggungPesangon As Int64

    Dim RekapTotal_GajiBagianProduksi As Int64
    Dim RekapTotal_GajiBagianProduksi2 As Int64
    Dim RekapTotal_GajiBagianProduksi3 As Int64
    Dim RekapTotal_GajiBagianProduksi4 As Int64
    Dim RekapTotal_ThrBonusProduksi As Int64
    Dim RekapTotal_TunjanganPPh21Produksi As Int64
    Dim RekapTotal_BpjsTkJkkJkmProduksi As Int64
    Dim RekapTotal_BpjsTkJhtIpProduksi As Int64
    Dim RekapTotal_BpjsKesehatanProduksi As Int64
    Dim RekapTotal_AsuransiKaryawanProduksi As Int64
    Dim RekapTotal_PesangonKaryawanProduksi As Int64
    Dim RekapTotal_JumlahGajiBagianProduksi As Int64
    Dim RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan As Int64
    Dim RekapTotal_BpjsKesehatanProduksiDibayarKaryawan As Int64
    Dim RekapTotal_GajiBagianAdministrasi As Int64
    Dim RekapTotal_GajiBagianAdministrasi2 As Int64
    Dim RekapTotal_GajiBagianAdministrasi3 As Int64
    Dim RekapTotal_GajiBagianAdministrasi4 As Int64
    Dim RekapTotal_ThrBonusAdministrasi As Int64
    Dim RekapTotal_TunjanganPPh21Administrasi As Int64
    Dim RekapTotal_BpjsTkJkkJkmAdministrasi As Int64
    Dim RekapTotal_BpjsTkJhtIpAdministrasi As Int64
    Dim RekapTotal_BpjsKesehatanAdministrasi As Int64
    Dim RekapTotal_AsuransiKaryawanAdministrasi As Int64
    Dim RekapTotal_PesangonKaryawanAdministrasi As Int64
    Dim RekapTotal_JumlahGajiBagianAdministrasi As Int64
    Dim RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan As Int64
    Dim RekapTotal_BpjsKesehatanAdministrasiDibayarKaryawan As Int64
    'Dim RekapTotal_TunjanganPPhPasal21 As int64
    'Dim RekapTotal_BiayaBpjsKesehatan As int64
    'Dim RekapTotal_BiayaBpjsKetenagakerjaan As int64
    Dim RekapTotal_JumlahGajiKotor As Int64
    Dim RekapTotal_PotonganHutangBpjsKesehatan As Int64
    Dim RekapTotal_PotonganHutangBpjsKetenagakerjaan As Int64
    Dim RekapTotal_PotonganHutangKoperasi As Int64
    Dim RekapTotal_PotonganHutangPPhPasal21Rutin As Int64
    Dim RekapTotal_PotonganHutangPPhPasal21Pesangon As Int64
    Dim RekapTotal_PotonganHutangSerikat As Int64
    Dim RekapTotal_PotonganKasbonKaryawan As Int64
    Dim RekapTotal_PotonganLainnya As Int64
    Dim RekapTotal_JumlahPotongan As Int64
    Dim RekapTotal_JumlahGajiDibayarkan As Int64
    Dim RekapTotal_PPhDitanggungRutin As Int64
    Dim RekapTotal_PPhDitanggungPesangon As Int64
    Dim RekapTotal_JumlahPembayaran As Int64
    Dim RekapTotal_SisaPembayaran As Int64

    Dim NomorID_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim Bulan_Terseleksi
    Dim NomorBPHG_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim GajiBagianProduksi_Terseleksi As Int64
    Dim GajiBagianProduksi2_Terseleksi As Int64
    Dim GajiBagianProduksi3_Terseleksi As Int64
    Dim GajiBagianProduksi4_Terseleksi As Int64
    Dim ThrBonusProduksi_Terseleksi As Int64
    Dim TunjanganPPh21Produksi_Terseleksi As Int64
    Dim BpjsTkJkkJkmProduksi_Terseleksi As Int64
    Dim BpjsTkJhtIpProduksi_Terseleksi As Int64
    Dim BpjsKesehatanProduksi_Terseleksi As Int64
    Dim AsuransiKaryawanProduksi_Terseleksi As Int64
    Dim PesangonKaryawanProduksi_Terseleksi As Int64
    Dim JumlahGajiBagianProduksi_Terseleksi As Int64
    Dim BpjsTkJhtIpProduksiDibayarKaryawan_Terseleksi As Int64
    Dim BpjsKesehatanProduksiDibayarKaryawan_Terseleksi As Int64
    Dim GajiBagianAdministrasi_Terseleksi As Int64
    Dim GajiBagianAdministrasi2_Terseleksi As Int64
    Dim GajiBagianAdministrasi3_Terseleksi As Int64
    Dim GajiBagianAdministrasi4_Terseleksi As Int64
    Dim ThrBonusAdministrasi_Terseleksi As Int64
    Dim TunjanganPPh21Administrasi_Terseleksi As Int64
    Dim BpjsTkJkkJkmAdministrasi_Terseleksi As Int64
    Dim BpjsTkJhtIpAdministrasi_Terseleksi As Int64
    Dim BpjsKesehatanAdministrasi_Terseleksi As Int64
    Dim AsuransiKaryawanAdministrasi_Terseleksi As Int64
    Dim PesangonKaryawanAdministrasi_Terseleksi As Int64
    Dim JumlahGajiBagianAdministrasi_Terseleksi As Int64
    Dim BpjsTkJhtIpAdministrasiDibayarKaryawan_Terseleksi As Int64
    Dim BpjsKesehatanAdministrasiDibayarKaryawan_Terseleksi As Int64
    'Dim TunjanganPPhPasal21_Terseleksi As Int64
    'Dim BiayaBpjsKesehatan_Terseleksi As Int64
    'Dim BiayaBpjsKetenagakerjaan_Terseleksi As Int64
    Dim JumlahGajiKotor_Terseleksi As Int64
    Dim PotonganHutangBpjsKesehatan_Terseleksi As Int64
    Dim PotonganHutangBpjsKetenagakerjaan_Terseleksi As Int64
    Dim PotonganHutangKoperasi_Terseleksi As Int64
    Dim PotonganHutangPPhPasal21Rutin_Terseleksi As Int64
    Dim PotonganHutangPPhPasal21Pesangon_Terseleksi As Int64
    Dim PotonganHutangSerikat_Terseleksi As Int64
    Dim PotonganKasbonKaryawan_Terseleksi As Int64
    Dim PotonganLainnya_Terseleksi As Int64
    Dim JumlahPotongan_Terseleksi As Int64
    Dim JumlahGajiDibayarkan_Terseleksi As Int64
    Dim JumlahTagihan_Terseleksi As Int64
    Dim PPhDitanggungRutin_Terseleksi As Int64
    Dim PPhDitanggungPesangon_Terseleksi As Int64
    Dim JumlahPembayaran_Terseleksi As Int64
    Dim SisaPembayaran_Terseleksi As Int64
    Dim NomorJV_Gaji_Terseleksi As Int64
    Dim Keterangan_Terseleksi

    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi

    Public TahunTelusurData As Integer
    Public TahunTelusurDataTerlama As Integer
    Public TahunTelusurDataSebelumnya As Integer

    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Dim AwalanBPHG_PlusTahunBukuTampilan

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        lbl_JudulForm.Text = JudulForm
        Terabas()
        StatusAktif = True

        ProsesLoadingForm = True

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            grb_InfoSaldo.Header = "Saldo Akhir :"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            grb_InfoSaldo.Header = "Saldo Awal :"
        End If

        VisibilitasTombolTambahDanHapusTahun()

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub



    Sub RefreshTampilanData()
        KontenComboTahunTelusurData(False)
        KontenCombo_Bulan(False)
        Sub_JenisTampilan_REKAP()
    End Sub



    Dim EksekusiTampilanData As Boolean
    Sub TampilkanData()

        If EksekusiTampilanData = False Then Return

        'Judul Halaman :
        frm_BukuPengawasanGaji.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        VisibilitasKolomKolomTabel()

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Data Tabel :

        Dim Bulan_Tabel = Kosongan
        NomorUrut = 0
        MaxLooping = 0

        RekapTotal_GajiBagianProduksi = 0
        RekapTotal_GajiBagianProduksi2 = 0
        RekapTotal_GajiBagianProduksi3 = 0
        RekapTotal_GajiBagianProduksi4 = 0
        RekapTotal_ThrBonusProduksi = 0
        RekapTotal_TunjanganPPh21Produksi = 0
        RekapTotal_BpjsTkJkkJkmProduksi = 0
        RekapTotal_BpjsTkJhtIpProduksi = 0
        RekapTotal_BpjsKesehatanProduksi = 0
        RekapTotal_AsuransiKaryawanProduksi = 0
        RekapTotal_PesangonKaryawanProduksi = 0
        RekapTotal_JumlahGajiBagianProduksi = 0
        RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan = 0
        RekapTotal_BpjsKesehatanProduksiDibayarKaryawan = 0

        RekapTotal_GajiBagianAdministrasi = 0
        RekapTotal_GajiBagianAdministrasi2 = 0
        RekapTotal_GajiBagianAdministrasi3 = 0
        RekapTotal_GajiBagianAdministrasi4 = 0
        RekapTotal_ThrBonusAdministrasi = 0
        RekapTotal_TunjanganPPh21Administrasi = 0
        RekapTotal_BpjsTkJkkJkmAdministrasi = 0
        RekapTotal_BpjsTkJhtIpAdministrasi = 0
        RekapTotal_BpjsKesehatanAdministrasi = 0
        RekapTotal_AsuransiKaryawanAdministrasi = 0
        RekapTotal_PesangonKaryawanAdministrasi = 0
        RekapTotal_JumlahGajiBagianAdministrasi = 0
        RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan = 0
        RekapTotal_BpjsKesehatanAdministrasiDibayarKaryawan = 0

        RekapTotal_JumlahGajiKotor = 0
        RekapTotal_PotonganHutangBpjsKesehatan = 0
        RekapTotal_PotonganHutangBpjsKetenagakerjaan = 0
        RekapTotal_PotonganHutangKoperasi = 0
        RekapTotal_PotonganHutangPPhPasal21Rutin = 0
        RekapTotal_PotonganHutangPPhPasal21Pesangon = 0
        RekapTotal_PotonganHutangSerikat = 0
        RekapTotal_PotonganKasbonKaryawan = 0
        RekapTotal_PotonganLainnya = 0
        RekapTotal_JumlahPotongan = 0
        RekapTotal_JumlahGajiDibayarkan = 0
        RekapTotal_PPhDitanggungRutin = 0
        RekapTotal_PPhDitanggungPesangon = 0
        RekapTotal_JumlahPembayaran = 0
        RekapTotal_SisaPembayaran = 0

        If JenisTampilan = JenisTampilan_REKAP Or JenisTampilan = JenisTampilan_ALL Then
            NomorBulan = 1
            MaxLooping = 12
        End If

        If JenisTampilan = JenisTampilan_DETAIL Then
            NomorBulan = KonversiBulanKeAngka(Bulan)
            MaxLooping = NomorBulan
        End If

        AksesDatabase_Transaksi(Buka)

        Do While NomorBulan <= MaxLooping

            JumlahBarisDalamSatuBulan = 0
            Bulan_Tabel = BulanTerbilang(NomorBulan)
            NomorBPHG = AwalanBPHG_PlusTahunBukuTampilan & NomorBulan
            RekapPerBulan_GajiBagianProduksi = 0
            RekapPerBulan_GajiBagianProduksi2 = 0
            RekapPerBulan_GajiBagianProduksi3 = 0
            RekapPerBulan_GajiBagianProduksi4 = 0
            RekapPerBulan_ThrBonusProduksi = 0
            RekapPerBulan_TunjanganPPh21Produksi = 0
            RekapPerBulan_BpjsTkJkkJkmProduksi = 0
            RekapPerBulan_BpjsTkJhtIpProduksi = 0
            RekapPerBulan_BpjsKesehatanProduksi = 0
            RekapPerBulan_AsuransiKaryawanProduksi = 0
            RekapPerBulan_PesangonKaryawanProduksi = 0
            RekapPerBulan_JumlahGajiBagianProduksi = 0
            RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan = 0
            RekapPerBulan_BpjskesehatanProduksiDibayarKaryawan = 0

            RekapPerBulan_GajiBagianAdministrasi = 0
            RekapPerBulan_GajiBagianAdministrasi2 = 0
            RekapPerBulan_GajiBagianAdministrasi3 = 0
            RekapPerBulan_GajiBagianAdministrasi4 = 0
            RekapPerBulan_ThrBonusAdministrasi = 0
            RekapPerBulan_TunjanganPPh21Administrasi = 0
            RekapPerBulan_BpjsTkJkkJkmAdministrasi = 0
            RekapPerBulan_BpjsTkJhtIpAdministrasi = 0
            RekapPerBulan_BpjsKesehatanAdministrasi = 0
            RekapPerBulan_AsuransiKaryawanAdministrasi = 0
            RekapPerBulan_PesangonKaryawanAdministrasi = 0
            RekapPerBulan_JumlahGajiBagianAdministrasi = 0
            RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan = 0
            RekapPerBulan_BpjsKesehatanAdministrasiDibayarKaryawan = 0

            RekapPerBulan_JumlahGajiKotor = 0
            RekapPerBulan_PotonganHutangBpjsKesehatan = 0
            RekapPerBulan_PotonganHutangBpjsKetenagakerjaan = 0
            RekapPerBulan_PotonganHutangKoperasi = 0
            RekapPerBulan_PotonganHutangPPhPasal21Rutin = 0
            RekapPerBulan_PotonganHutangPPhPasal21Pesangon = 0
            RekapPerBulan_PotonganHutangSerikat = 0
            RekapPerBulan_PotonganKasbonKaryawan = 0
            RekapPerBulan_PotonganLainnya = 0
            RekapPerBulan_JumlahPotongan = 0
            RekapPerBulan_JumlahGajiDibayarkan = 0
            RekapPerBulan_PPhDitanggungRutin = 0
            RekapPerBulan_PPhDitanggungPesangon = 0
            JumlahPembayaran = 0
            SisaPembayaran = 0

            cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanGaji WHERE Bulan = '" & Bulan_Tabel & "'", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader

            Do While dr.Read

                NomorID = dr.Item("Nomor_ID")
                TanggalTransaksi = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Transaksi"), 10)
                GajiBagianProduksi = dr.Item("Gaji_Bagian_Produksi")
                GajiBagianProduksi2 = dr.Item("Gaji_Bagian_Produksi_2")
                GajiBagianProduksi3 = dr.Item("Gaji_Bagian_Produksi_3")
                GajiBagianProduksi4 = dr.Item("Gaji_Bagian_Produksi_4")
                ThrBonusProduksi = dr.Item("THR_Bonus_Produksi")
                TunjanganPPh21Produksi = dr.Item("Tunjangan_PPh_21_Produksi")
                BpjsTkJkkJkmProduksi = dr.Item("BPJS_TK_JKK_JKM_Produksi")
                BpjsTkJhtIpProduksi = dr.Item("BPJS_TK_JHT_IP_Produksi")
                BpjsKesehatanProduksi = dr.Item("BPJS_Kesehatan_Produksi")
                AsuransiKaryawanProduksi = dr.Item("Asuransi_Karyawan_Produksi")
                PesangonKaryawanProduksi = dr.Item("Pesangon_Karyawan_Produksi")
                JumlahGajiBagianProduksi = dr.Item("Jumlah_Gaji_Bagian_Produksi")
                BpjsTkJhtIpProduksiDibayarKaryawan = dr.Item("BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan")
                BpjsKesehatanProduksiDibayarKaryawan = dr.Item("BPJS_Kesehatan_Produksi_Dibayar_Karyawan")

                GajiBagianAdministrasi = dr.Item("Gaji_Bagian_Administrasi")
                GajiBagianAdministrasi2 = dr.Item("Gaji_Bagian_Administrasi_2")
                GajiBagianAdministrasi3 = dr.Item("Gaji_Bagian_Administrasi_3")
                GajiBagianAdministrasi4 = dr.Item("Gaji_Bagian_Administrasi_4")
                ThrBonusAdministrasi = dr.Item("THR_Bonus_Administrasi")
                TunjanganPPh21Administrasi = dr.Item("Tunjangan_PPh_21_Administrasi")
                BpjsTkJkkJkmAdministrasi = dr.Item("BPJS_TK_JKK_JKM_Administrasi")
                BpjsTkJhtIpAdministrasi = dr.Item("BPJS_TK_JHT_IP_Administrasi")
                BpjsKesehatanAdministrasi = dr.Item("BPJS_Kesehatan_Administrasi")
                AsuransiKaryawanAdministrasi = dr.Item("Asuransi_Karyawan_Administrasi")
                PesangonKaryawanAdministrasi = dr.Item("Pesangon_Karyawan_Administrasi")
                JumlahGajiBagianAdministrasi = dr.Item("Jumlah_Gaji_Bagian_Administrasi")
                BpjsTkJhtIpAdministrasiDibayarKaryawan = dr.Item("BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan")
                BpjsKesehatanAdministrasiDibayarKaryawan = dr.Item("BPJS_Kesehatan_Administrasi_Dibayar_Karyawan")

                JumlahGajiKotor = dr.Item("Jumlah_Gaji_Kotor")
                PotonganHutangBpjsKesehatan = dr.Item("Potongan_Hutang_BPJS_Kesehatan")
                PotonganHutangBpjsKetenagakerjaan = dr.Item("Potongan_Hutang_BPJS_Ketenagakerjaan")
                PotonganHutangKoperasi = dr.Item("Potongan_Hutang_Koperasi")
                PotonganHutangPPhPasal21Rutin = dr.Item("Potongan_Hutang_PPh_Pasal_21_Rutin")
                PotonganHutangPPhPasal21Pesangon = dr.Item("Potongan_Hutang_PPh_Pasal_21_Pesangon")
                PotonganHutangSerikat = dr.Item("Potongan_Hutang_Serikat")
                PotonganKasbonKaryawan = dr.Item("Potongan_Kasbon_Karyawan")
                PotonganLainnya = dr.Item("Potongan_Lainnya")
                JumlahPotongan = dr.Item("Jumlah_Potongan")
                JumlahGajiDibayarkan = dr.Item("Jumlah_Gaji_Dibayarkan")
                PPhDitanggungRutin = dr.Item("PPh_Ditanggung_Rutin")
                PPhDitanggungPesangon = dr.Item("PPh_Ditanggung_Pesangon")
                NomorJV_Gaji = dr.Item("Nomor_JV")
                Keterangan = PenghapusEnter(dr.Item("Keterangan"))

                RekapPerBulan_GajiBagianProduksi += GajiBagianProduksi
                RekapPerBulan_GajiBagianProduksi2 += GajiBagianProduksi2
                RekapPerBulan_GajiBagianProduksi3 += GajiBagianProduksi3
                RekapPerBulan_GajiBagianProduksi4 += GajiBagianProduksi4
                RekapPerBulan_ThrBonusProduksi += ThrBonusProduksi
                RekapPerBulan_TunjanganPPh21Produksi += TunjanganPPh21Produksi
                RekapPerBulan_BpjsTkJkkJkmProduksi += BpjsTkJkkJkmProduksi
                RekapPerBulan_BpjsTkJhtIpProduksi += BpjsTkJhtIpProduksi
                RekapPerBulan_BpjsKesehatanProduksi += BpjsKesehatanProduksi
                RekapPerBulan_AsuransiKaryawanProduksi += AsuransiKaryawanProduksi
                RekapPerBulan_PesangonKaryawanProduksi += PesangonKaryawanProduksi
                RekapPerBulan_JumlahGajiBagianProduksi += JumlahGajiBagianProduksi
                RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan += BpjsTkJhtIpProduksiDibayarKaryawan
                RekapPerBulan_BpjsKesehatanProduksiDibayarKaryawan += BpjsKesehatanProduksiDibayarKaryawan

                RekapPerBulan_GajiBagianAdministrasi += GajiBagianAdministrasi
                RekapPerBulan_GajiBagianAdministrasi2 += GajiBagianAdministrasi2
                RekapPerBulan_GajiBagianAdministrasi3 += GajiBagianAdministrasi3
                RekapPerBulan_GajiBagianAdministrasi4 += GajiBagianAdministrasi4
                RekapPerBulan_ThrBonusAdministrasi += ThrBonusAdministrasi
                RekapPerBulan_TunjanganPPh21Administrasi += TunjanganPPh21Administrasi
                RekapPerBulan_BpjsTkJkkJkmAdministrasi += BpjsTkJkkJkmAdministrasi
                RekapPerBulan_BpjsTkJhtIpAdministrasi += BpjsTkJhtIpAdministrasi
                RekapPerBulan_BpjsKesehatanAdministrasi += BpjsKesehatanAdministrasi
                RekapPerBulan_AsuransiKaryawanAdministrasi += AsuransiKaryawanAdministrasi
                RekapPerBulan_PesangonKaryawanAdministrasi += PesangonKaryawanAdministrasi
                RekapPerBulan_JumlahGajiBagianAdministrasi += JumlahGajiBagianAdministrasi
                RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan += BpjsTkJhtIpAdministrasiDibayarKaryawan
                RekapPerBulan_BpjsKesehatanAdministrasiDibayarKaryawan += BpjsKesehatanAdministrasiDibayarKaryawan

                RekapPerBulan_JumlahGajiKotor += JumlahGajiKotor
                RekapPerBulan_PotonganHutangBpjsKesehatan += PotonganHutangBpjsKesehatan
                RekapPerBulan_PotonganHutangBpjsKetenagakerjaan += PotonganHutangBpjsKetenagakerjaan
                RekapPerBulan_PotonganHutangKoperasi += PotonganHutangKoperasi
                RekapPerBulan_PotonganHutangPPhPasal21Rutin += PotonganHutangPPhPasal21Rutin
                RekapPerBulan_PotonganHutangPPhPasal21Pesangon += PotonganHutangPPhPasal21Pesangon
                RekapPerBulan_PotonganHutangSerikat += PotonganHutangSerikat
                RekapPerBulan_PotonganKasbonKaryawan += PotonganKasbonKaryawan
                RekapPerBulan_PotonganLainnya += PotonganLainnya
                RekapPerBulan_JumlahPotongan += JumlahPotongan
                RekapPerBulan_JumlahGajiDibayarkan += JumlahGajiDibayarkan
                RekapPerBulan_PPhDitanggungRutin += PPhDitanggungRutin
                RekapPerBulan_PPhDitanggungPesangon += PPhDitanggungPesangon

                RekapTotal_GajiBagianProduksi += GajiBagianProduksi
                RekapTotal_GajiBagianProduksi2 += GajiBagianProduksi2
                RekapTotal_GajiBagianProduksi3 += GajiBagianProduksi3
                RekapTotal_GajiBagianProduksi4 += GajiBagianProduksi4
                RekapTotal_ThrBonusProduksi += ThrBonusProduksi
                RekapTotal_TunjanganPPh21Produksi += TunjanganPPh21Produksi
                RekapTotal_BpjsTkJkkJkmProduksi += BpjsTkJkkJkmProduksi
                RekapTotal_BpjsTkJhtIpProduksi += BpjsTkJhtIpProduksi
                RekapTotal_BpjsKesehatanProduksi += BpjsKesehatanProduksi
                RekapTotal_AsuransiKaryawanProduksi += AsuransiKaryawanProduksi
                RekapTotal_PesangonKaryawanProduksi += PesangonKaryawanProduksi
                RekapTotal_JumlahGajiBagianProduksi += JumlahGajiBagianProduksi
                RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan += BpjsTkJhtIpProduksiDibayarKaryawan
                RekapTotal_BpjsKesehatanProduksiDibayarKaryawan += BpjsKesehatanProduksiDibayarKaryawan

                RekapTotal_GajiBagianAdministrasi += GajiBagianAdministrasi
                RekapTotal_GajiBagianAdministrasi2 += GajiBagianAdministrasi2
                RekapTotal_GajiBagianAdministrasi3 += GajiBagianAdministrasi3
                RekapTotal_GajiBagianAdministrasi4 += GajiBagianAdministrasi4
                RekapTotal_ThrBonusAdministrasi += ThrBonusAdministrasi
                RekapTotal_TunjanganPPh21Administrasi += TunjanganPPh21Administrasi
                RekapTotal_BpjsTkJkkJkmAdministrasi += BpjsTkJkkJkmAdministrasi
                RekapTotal_BpjsTkJhtIpAdministrasi += BpjsTkJhtIpAdministrasi
                RekapTotal_BpjsKesehatanAdministrasi += BpjsKesehatanAdministrasi
                RekapTotal_AsuransiKaryawanAdministrasi += AsuransiKaryawanAdministrasi
                RekapTotal_PesangonKaryawanAdministrasi += PesangonKaryawanAdministrasi
                RekapTotal_JumlahGajiBagianAdministrasi += JumlahGajiBagianAdministrasi
                RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan += BpjsTkJhtIpAdministrasiDibayarKaryawan
                RekapTotal_BpjsKesehatanAdministrasiDibayarKaryawan += BpjsKesehatanAdministrasiDibayarKaryawan

                RekapTotal_JumlahGajiKotor += JumlahGajiKotor
                RekapTotal_PotonganHutangBpjsKesehatan += PotonganHutangBpjsKesehatan
                RekapTotal_PotonganHutangBpjsKetenagakerjaan += PotonganHutangBpjsKetenagakerjaan
                RekapTotal_PotonganHutangKoperasi += PotonganHutangKoperasi
                RekapTotal_PotonganHutangPPhPasal21Rutin += PotonganHutangPPhPasal21Rutin
                RekapTotal_PotonganHutangPPhPasal21Pesangon += PotonganHutangPPhPasal21Pesangon
                RekapTotal_PotonganHutangSerikat += PotonganHutangSerikat
                RekapTotal_PotonganKasbonKaryawan += PotonganKasbonKaryawan
                RekapTotal_PotonganLainnya += PotonganLainnya
                RekapTotal_JumlahPotongan += JumlahPotongan
                RekapTotal_JumlahGajiDibayarkan += JumlahGajiDibayarkan
                RekapTotal_PPhDitanggungRutin += PPhDitanggungRutin
                RekapTotal_PPhDitanggungPesangon += PPhDitanggungPesangon

                If JenisTampilan = JenisTampilan_ALL Or JenisTampilan = JenisTampilan_DETAIL Then

                    NomorUrut += 1
                    JumlahBarisDalamSatuBulan += 1
                    datatabelUtama.Rows.Add(NomorUrut, NomorID, Bulan_Tabel, NomorBPHG, TanggalTransaksi,
                                            GajiBagianProduksi, GajiBagianProduksi2, GajiBagianProduksi3, GajiBagianProduksi4, ThrBonusProduksi,
                                            TunjanganPPh21Produksi, BpjsTkJkkJkmProduksi, BpjsTkJhtIpProduksi, BpjsKesehatanProduksi, AsuransiKaryawanProduksi, PesangonKaryawanProduksi,
                                            JumlahGajiBagianProduksi, BpjsTkJhtIpProduksiDibayarKaryawan, BpjsKesehatanProduksiDibayarKaryawan,
                                            GajiBagianAdministrasi, GajiBagianAdministrasi2, GajiBagianAdministrasi3, GajiBagianAdministrasi4, ThrBonusAdministrasi,
                                            TunjanganPPh21Administrasi, BpjsTkJkkJkmAdministrasi, BpjsTkJhtIpAdministrasi, BpjsKesehatanAdministrasi, AsuransiKaryawanAdministrasi, PesangonKaryawanAdministrasi,
                                            JumlahGajiBagianAdministrasi, BpjsTkJhtIpAdministrasiDibayarKaryawan, BpjsKesehatanAdministrasiDibayarKaryawan,
                                            JumlahGajiKotor,
                                            PotonganHutangBpjsKesehatan, PotonganHutangBpjsKetenagakerjaan, PotonganHutangKoperasi,
                                            PotonganHutangPPhPasal21Rutin, PotonganHutangPPhPasal21Pesangon, PotonganHutangSerikat, PotonganKasbonKaryawan, PotonganLainnya, JumlahPotongan,
                                            JumlahGajiDibayarkan, PPhDitanggungRutin, PPhDitanggungPesangon, 0, 0, NomorJV_Gaji, Keterangan)
                End If
            Loop

            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP     = '" & NomorBPHG & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahPembayaran += drBAYAR.Item("Jumlah_Bayar")
            Loop
            SisaPembayaran = RekapPerBulan_JumlahGajiDibayarkan - JumlahPembayaran

            If JenisTampilan = JenisTampilan_REKAP Or JenisTampilan = JenisTampilan_ALL Then

                RekapTotal_JumlahPembayaran = AmbilAngka(RekapTotal_JumlahPembayaran) + AmbilAngka(JumlahPembayaran)
                RekapTotal_SisaPembayaran = AmbilAngka(RekapTotal_SisaPembayaran) + AmbilAngka(SisaPembayaran)

                If JenisTampilan = JenisTampilan_REKAP Then
                    datatabelUtama.Rows.Add(NomorBulan, Kosongan, Bulan_Tabel, NomorBPHG, Kosongan,
                                        RekapPerBulan_GajiBagianProduksi,
                                        RekapPerBulan_GajiBagianProduksi2,
                                        RekapPerBulan_GajiBagianProduksi3,
                                        RekapPerBulan_GajiBagianProduksi4,
                                        RekapPerBulan_ThrBonusProduksi,
                                        RekapPerBulan_TunjanganPPh21Produksi,
                                        RekapPerBulan_BpjsTkJkkJkmProduksi,
                                        RekapPerBulan_BpjsTkJhtIpProduksi,
                                        RekapPerBulan_BpjsKesehatanProduksi,
                                        RekapPerBulan_AsuransiKaryawanProduksi,
                                        RekapPerBulan_PesangonKaryawanProduksi,
                                        RekapPerBulan_JumlahGajiBagianProduksi,
                                        RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan,
                                        RekapPerBulan_BpjsKesehatanProduksiDibayarKaryawan,
                                        RekapPerBulan_GajiBagianAdministrasi,
                                        RekapPerBulan_GajiBagianAdministrasi2,
                                        RekapPerBulan_GajiBagianAdministrasi3,
                                        RekapPerBulan_GajiBagianAdministrasi4,
                                        RekapPerBulan_ThrBonusAdministrasi,
                                        RekapPerBulan_TunjanganPPh21Administrasi,
                                        RekapPerBulan_BpjsTkJkkJkmAdministrasi,
                                        RekapPerBulan_BpjsTkJhtIpAdministrasi,
                                        RekapPerBulan_BpjsKesehatanAdministrasi,
                                        RekapPerBulan_AsuransiKaryawanAdministrasi,
                                        RekapPerBulan_PesangonKaryawanAdministrasi,
                                        RekapPerBulan_JumlahGajiBagianAdministrasi,
                                        RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan,
                                        RekapPerBulan_BpjsKesehatanAdministrasiDibayarKaryawan,
                                        RekapPerBulan_JumlahGajiKotor,
                                        RekapPerBulan_PotonganHutangBpjsKesehatan,
                                        RekapPerBulan_PotonganHutangBpjsKetenagakerjaan,
                                        RekapPerBulan_PotonganHutangKoperasi,
                                        RekapPerBulan_PotonganHutangPPhPasal21Rutin,
                                        RekapPerBulan_PotonganHutangPPhPasal21Pesangon,
                                        RekapPerBulan_PotonganHutangSerikat,
                                        RekapPerBulan_PotonganKasbonKaryawan,
                                        RekapPerBulan_PotonganLainnya,
                                        RekapPerBulan_JumlahPotongan,
                                        RekapPerBulan_JumlahGajiDibayarkan,
                                        RekapPerBulan_PPhDitanggungRutin,
                                        RekapPerBulan_PPhDitanggungPesangon,
                                        JumlahPembayaran,
                                        SisaPembayaran, 0, Kosongan)
                    'If AmbilAngka(RekapPerBulan_JumlahGajiDibayarkan) > 0 Then datatabelUtama.Rows(NomorBulan - 1).DefaultCellStyle.ForeColor = WarnaTegas
                    'If AmbilAngka(RekapPerBulan_JumlahGajiDibayarkan) = 0 Then datatabelUtama.Rows(NomorBulan - 1).DefaultCellStyle.ForeColor = WarnaPudar
                End If

                If JenisTampilan = JenisTampilan_ALL And JumlahBarisDalamSatuBulan > 0 Then
                    'datatabelUtama.Rows.Insert(datatabelUtama.RowCount - JumlahBarisDalamSatuBulan, Kosongan, Kosongan, Bulan_Tabel, Bulan_Tabel)
                    'Dim newRow As DataRow = datatabelUtama.NewRow()
                    newRow = datatabelUtama.NewRow
                    'newRow("Tanggal_Transaksi") = Bulan
                    datatabelUtama.Rows.InsertAt(newRow, datatabelUtama.Rows.Count - JumlahBarisDalamSatuBulan)
                    datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, NomorBPHG, "Total",
                                        RekapPerBulan_GajiBagianProduksi,
                                        RekapPerBulan_GajiBagianProduksi2,
                                        RekapPerBulan_GajiBagianProduksi3,
                                        RekapPerBulan_GajiBagianProduksi4,
                                        RekapPerBulan_ThrBonusProduksi,
                                        RekapPerBulan_TunjanganPPh21Produksi,
                                        RekapPerBulan_BpjsTkJkkJkmProduksi,
                                        RekapPerBulan_BpjsTkJhtIpProduksi,
                                        RekapPerBulan_BpjsKesehatanProduksi,
                                        RekapPerBulan_AsuransiKaryawanProduksi,
                                        RekapPerBulan_PesangonKaryawanProduksi,
                                        RekapPerBulan_JumlahGajiBagianProduksi,
                                        RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan,
                                        RekapPerBulan_BpjsKesehatanProduksiDibayarKaryawan,
                                        RekapPerBulan_GajiBagianAdministrasi,
                                        RekapPerBulan_GajiBagianAdministrasi2,
                                        RekapPerBulan_GajiBagianAdministrasi3,
                                        RekapPerBulan_GajiBagianAdministrasi4,
                                        RekapPerBulan_ThrBonusAdministrasi,
                                        RekapPerBulan_TunjanganPPh21Administrasi,
                                        RekapPerBulan_BpjsTkJkkJkmAdministrasi,
                                        RekapPerBulan_BpjsTkJhtIpAdministrasi,
                                        RekapPerBulan_BpjsKesehatanAdministrasi,
                                        RekapPerBulan_AsuransiKaryawanAdministrasi,
                                        RekapPerBulan_PesangonKaryawanAdministrasi,
                                        RekapPerBulan_JumlahGajiBagianAdministrasi,
                                        RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan,
                                        RekapPerBulan_BpjsKesehatanAdministrasiDibayarKaryawan,
                                        RekapPerBulan_JumlahGajiKotor,
                                        RekapPerBulan_PotonganHutangBpjsKesehatan,
                                        RekapPerBulan_PotonganHutangBpjsKetenagakerjaan,
                                        RekapPerBulan_PotonganHutangKoperasi,
                                        RekapPerBulan_PotonganHutangPPhPasal21Rutin,
                                        RekapPerBulan_PotonganHutangPPhPasal21Pesangon,
                                        RekapPerBulan_PotonganHutangSerikat,
                                        RekapPerBulan_PotonganKasbonKaryawan,
                                        RekapPerBulan_PotonganLainnya,
                                        RekapPerBulan_JumlahPotongan,
                                        RekapPerBulan_JumlahGajiDibayarkan,
                                        RekapPerBulan_PPhDitanggungRutin,
                                        RekapPerBulan_PPhDitanggungPesangon,
                                        JumlahPembayaran,
                                        SisaPembayaran, 0, Kosongan)
                    datatabelUtama.Rows.Add()
                End If
            End If
            NomorBulan += 1
        Loop
        NomorBulan -= 1

        AksesDatabase_Transaksi(Tutup)

        If datatabelUtama.Rows.Count > 0 Then
            If JenisTampilan <> JenisTampilan_ALL Then datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(Kosongan, Kosongan, "T O T A L", Kosongan, "T O T A L",
                                    RekapTotal_GajiBagianProduksi,
                                    RekapTotal_GajiBagianProduksi2,
                                    RekapTotal_GajiBagianProduksi3,
                                    RekapTotal_GajiBagianProduksi4,
                                    RekapTotal_ThrBonusProduksi,
                                    RekapTotal_TunjanganPPh21Produksi,
                                    RekapTotal_BpjsTkJkkJkmProduksi,
                                    RekapTotal_BpjsTkJhtIpProduksi,
                                    RekapTotal_BpjsKesehatanProduksi,
                                    RekapTotal_AsuransiKaryawanProduksi,
                                    RekapTotal_PesangonKaryawanProduksi,
                                    RekapTotal_JumlahGajiBagianProduksi,
                                    RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan,
                                    RekapTotal_BpjsKesehatanProduksiDibayarKaryawan,
                                    RekapTotal_GajiBagianAdministrasi,
                                    RekapTotal_GajiBagianAdministrasi2,
                                    RekapTotal_GajiBagianAdministrasi3,
                                    RekapTotal_GajiBagianAdministrasi4,
                                    RekapTotal_ThrBonusAdministrasi,
                                    RekapTotal_TunjanganPPh21Administrasi,
                                    RekapTotal_BpjsTkJkkJkmAdministrasi,
                                    RekapTotal_BpjsTkJhtIpAdministrasi,
                                    RekapTotal_BpjsKesehatanAdministrasi,
                                    RekapTotal_AsuransiKaryawanAdministrasi,
                                    RekapTotal_PesangonKaryawanAdministrasi,
                                    RekapTotal_JumlahGajiBagianAdministrasi,
                                    RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan,
                                    RekapTotal_BpjsKesehatanAdministrasiDibayarKaryawan,
                                    RekapTotal_JumlahGajiKotor,
                                    RekapTotal_PotonganHutangBpjsKesehatan,
                                    RekapTotal_PotonganHutangBpjsKetenagakerjaan,
                                    RekapTotal_PotonganHutangKoperasi,
                                    RekapTotal_PotonganHutangPPhPasal21Rutin,
                                    RekapTotal_PotonganHutangPPhPasal21Pesangon,
                                    RekapTotal_PotonganHutangSerikat,
                                    RekapTotal_PotonganKasbonKaryawan,
                                    RekapTotal_PotonganLainnya,
                                    RekapTotal_JumlahPotongan,
                                    RekapTotal_JumlahGajiDibayarkan,
                                    RekapTotal_PPhDitanggungRutin,
                                    RekapTotal_PPhDitanggungPesangon,
                                    RekapTotal_JumlahPembayaran,
                                    RekapTotal_SisaPembayaran, 0, Kosongan)
        End If
        BersihkanSeleksi()

    End Sub



    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        KetersediaanTombolJurnal(False)
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        NomorJV_Pembayaran_Terseleksi = 0
        'VisibilitasInfoSaldo(True)
        BersihkanSeleksiTabelPembayaran()
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub


    Sub Sub_JenisTampilan_DETAIL()
        JenisTampilan = JenisTampilan_DETAIL
        JudulForm = "Buku Pengawasan Gaji"
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub


    Sub Sub_JenisTampilan_REKAP()
        JenisTampilan = JenisTampilan_REKAP
        JudulForm = "Buku Pengawasan Gaji - Rekap"
        VisibilitasObjek_REKAP()
        TampilkanData()
    End Sub


    Sub Sub_JenisTampilan_ALL()
        JenisTampilan = JenisTampilan_ALL
        JudulForm = "Buku Pengawasan Gaji"
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub


    Sub VisibilitasObjek_DETAIL()
        Bulan_.Visibility = Visibility.Collapsed
        Tanggal_Transaksi.Visibility = Visibility.Visible
        Jumlah_Pembayaran.Visibility = Visibility.Collapsed
        Sisa_Pembayaran.Visibility = Visibility.Collapsed
        Keterangan_.Visibility = Visibility.Visible
    End Sub


    Sub VisibilitasObjek_REKAP()
        Bulan_.Visibility = Visibility.Visible
        Tanggal_Transaksi.Visibility = Visibility.Collapsed
        Jumlah_Pembayaran.Visibility = Visibility.Visible
        Sisa_Pembayaran.Visibility = Visibility.Visible
        Keterangan_.Visibility = Visibility.Collapsed
    End Sub


    Sub KontenComboTahunTelusurData(TampilkanData As Boolean)

        TahunTelusurDataTerlama = AmbilTahunTerlama_BerdasarkanKolomTanggal(TahunCutOff, "tbl_PengawasanGaji", "Tanggal_Transaksi")
        Dim ListTahunTelusurData = TahunBukuAktif

        EksekusiTampilanData = False

        cmb_TahunTelusurData.Items.Clear()
        TahunTelusurData = TahunBukuAktif
        Do While ListTahunTelusurData >= TahunTelusurDataTerlama
            cmb_TahunTelusurData.Items.Add(ListTahunTelusurData)
            ListTahunTelusurData -= 1
        Loop

        If TampilkanData Then
            EksekusiTampilanData = True
        Else
            EksekusiTampilanData = False
        End If

        cmb_TahunTelusurData.SelectedValue = TahunBukuAktif

        EksekusiTampilanData = True

    End Sub


    Sub KontenCombo_Bulan(TampilkanData As Boolean)

        EksekusiTampilanData = False

        cmb_Bulan.Items.Clear()
        cmb_Bulan.Items.Add(Bulan_ALL)
        cmb_Bulan.Items.Add(Bulan_REKAP)
        cmb_Bulan.Items.Add(Bulan_Januari)
        cmb_Bulan.Items.Add(Bulan_Februari)
        cmb_Bulan.Items.Add(Bulan_Maret)
        cmb_Bulan.Items.Add(Bulan_April)
        cmb_Bulan.Items.Add(Bulan_Mei)
        cmb_Bulan.Items.Add(Bulan_Juni)
        cmb_Bulan.Items.Add(Bulan_Juli)
        cmb_Bulan.Items.Add(Bulan_Agustus)
        cmb_Bulan.Items.Add(Bulan_September)
        cmb_Bulan.Items.Add(Bulan_Oktober)
        cmb_Bulan.Items.Add(Bulan_Nopember)
        cmb_Bulan.Items.Add(Bulan_Desember)

        If TampilkanData Then
            EksekusiTampilanData = True
        Else
            EksekusiTampilanData = False
        End If

        cmb_Bulan.SelectedValue = Bulan_REKAP

        EksekusiTampilanData = True

    End Sub


    Sub VisibilitasTombolTambahDanHapusTahun()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            btn_TambahTahun.Visibility = Visibility.Visible
            btn_HapusTahun.Visibility = Visibility.Visible
        Else
            btn_TambahTahun.Visibility = Visibility.Collapsed
            btn_HapusTahun.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasInfoSaldo(Visibilitas As Boolean)
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_TotalTabel.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If TahunTelusurData = TahunBukuAktif Then
                grb_InfoSaldo.Visibility = Visibility.Visible
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    pnl_SaldoAwalPlusAJP.Visibility = Visibility.Visible
                    pnl_TotalTabel.Visibility = Visibility.Visible
                End If
            End If
        Else
            grb_InfoSaldo.Visibility = Visibility.Collapsed
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
            pnl_TotalTabel.Visibility = Visibility.Collapsed
        End If
        If JenisTahunBuku = JenisTahunBuku_LAMPAU And TahunTelusurData = TahunBukuAktif Then
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
            pnl_TotalTabel.Visibility = Visibility.Collapsed
        Else
            pnl_TotalTabel.Visibility = Visibility.Visible
        End If
    End Sub


    Sub VisibilitasTabelPembayaran()
        If JumlahBarisBayar > 0 Then
            datagridBayar.Visibility = Visibility.Visible
        Else
            datagridBayar.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KetersediaanTombolJurnal(Tersedia As Boolean)
        btn_LihatJurnal.IsEnabled = False
        If Tersedia Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If
    End Sub


    Sub VisibilitasKolomTabelBerdasarkanCOA(Kolom As DataGridTextColumn, COA As String)

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = Pilihan_Ya Then Kolom.Visibility = Visibility.Visible
            If dr.Item("Visibilitas") = Pilihan_Tidak Then Kolom.Visibility = Visibility.Collapsed
            Kolom.Header = dr.Item("Nama_Akun")
        Else
            Kolom.Visibility = Visibility.Collapsed
        End If

        Dim headerTextBlock As New TextBlock()
        Dim IndeksKolom As Integer = datagridUtama.Columns.IndexOf(Kolom)
        headerTextBlock.Text = Kolom.Header
        headerTextBlock.TextWrapping = TextWrapping.Wrap
        headerTextBlock.TextAlignment = TextAlignment.Center
        headerTextBlock.Padding = New Thickness(0)
        datagridUtama.Columns(IndeksKolom).Header = headerTextBlock

    End Sub


    Sub VisibilitasKolomKolomTabel()

        datagridUtama.Visibility = Visibility.Collapsed

        AksesDatabase_General(Buka)

        'Produksi :
        VisibilitasKolomTabelBerdasarkanCOA(Gaji_Bagian_Produksi, KodeTautanCOA_BiayaGajiProduksi)
        VisibilitasKolomTabelBerdasarkanCOA(Gaji_Bagian_Produksi_2, KodeTautanCOA_BiayaGajiProduksi2)
        VisibilitasKolomTabelBerdasarkanCOA(Gaji_Bagian_Produksi_3, KodeTautanCOA_BiayaGajiProduksi3)
        VisibilitasKolomTabelBerdasarkanCOA(Gaji_Bagian_Produksi_4, KodeTautanCOA_BiayaGajiProduksi4)
        VisibilitasKolomTabelBerdasarkanCOA(THR_Bonus_Produksi, KodeTautanCOA_BiayaThrBonusProduksi)
        VisibilitasKolomTabelBerdasarkanCOA(Tunjangan_PPh_21_Produksi, KodeTautanCOA_BiayaTunjanganPPh21Produksi)
        VisibilitasKolomTabelBerdasarkanCOA(BPJS_TK_JKK_JKM_Produksi, KodeTautanCOA_BiayaBpjsTkJkkJkmProduksi)
        VisibilitasKolomTabelBerdasarkanCOA(BPJS_TK_JHT_IP_Produksi, KodeTautanCOA_BiayaBpjsTkJhtIpProduksi)
        VisibilitasKolomTabelBerdasarkanCOA(BPJS_Kesehatan_Produksi, KodeTautanCOA_BiayaBpjsKesehatanProduksi)
        VisibilitasKolomTabelBerdasarkanCOA(Asuransi_Karyawan_Produksi, KodeTautanCOA_BiayaAsuransiKaryawanProduksi)
        VisibilitasKolomTabelBerdasarkanCOA(Pesangon_Karyawan_Produksi, KodeTautanCOA_BiayaPesangonKaryawanProduksi)

        'Administrasi
        VisibilitasKolomTabelBerdasarkanCOA(Gaji_Bagian_Administrasi, KodeTautanCOA_BiayaGajiAdministrasi)
        VisibilitasKolomTabelBerdasarkanCOA(Gaji_Bagian_Administrasi_2, KodeTautanCOA_BiayaGajiAdministrasi2)
        VisibilitasKolomTabelBerdasarkanCOA(Gaji_Bagian_Administrasi_3, KodeTautanCOA_BiayaGajiAdministrasi3)
        VisibilitasKolomTabelBerdasarkanCOA(Gaji_Bagian_Administrasi_4, KodeTautanCOA_BiayaGajiAdministrasi4)
        VisibilitasKolomTabelBerdasarkanCOA(THR_Bonus_Administrasi, KodeTautanCOA_BiayaThrBonusAdministrasi)
        VisibilitasKolomTabelBerdasarkanCOA(Tunjangan_PPh_21_Administrasi, KodeTautanCOA_BiayaTunjanganPPh21Administrasi)
        VisibilitasKolomTabelBerdasarkanCOA(BPJS_TK_JKK_JKM_Administrasi, KodeTautanCOA_BiayaBpjsTkJkkJkmAdministrasi)
        VisibilitasKolomTabelBerdasarkanCOA(BPJS_TK_JHT_IP_Administrasi, KodeTautanCOA_BiayaBpjsTkJhtIpAdministrasi)
        VisibilitasKolomTabelBerdasarkanCOA(BPJS_Kesehatan_Administrasi, KodeTautanCOA_BiayaBpjsKesehatanAdministrasi)
        VisibilitasKolomTabelBerdasarkanCOA(Asuransi_Karyawan_Administrasi, KodeTautanCOA_BiayaAsuransiKaryawanAdministrasi)
        VisibilitasKolomTabelBerdasarkanCOA(Pesangon_Karyawan_Administrasi, KodeTautanCOA_BiayaPesangonKaryawanAdministrasi)

        VisibilitasKolomTabelBerdasarkanCOA(Potongan_Hutang_BPJS_Kesehatan, KodeTautanCOA_HutangBpjsKesehatan)
        VisibilitasKolomTabelBerdasarkanCOA(Potongan_Hutang_BPJS_Ketenagakerjaan, KodeTautanCOA_HutangBpjsKetenagakerjaan)
        VisibilitasKolomTabelBerdasarkanCOA(Potongan_Hutang_Koperasi, KodeTautanCOA_HutangKoperasiKaryawan)
        VisibilitasKolomTabelBerdasarkanCOA(Potongan_Hutang_PPh_Pasal_21_Rutin, KodeTautanCOA_HutangPPhPasal21)
        VisibilitasKolomTabelBerdasarkanCOA(Potongan_Hutang_PPh_Pasal_21_Pesangon, KodeTautanCOA_HutangPPhPasal21_401)
        VisibilitasKolomTabelBerdasarkanCOA(Potongan_Hutang_Serikat, KodeTautanCOA_HutangSerikat)
        VisibilitasKolomTabelBerdasarkanCOA(Potongan_Kasbon_Karyawan, KodeTautanCOA_PiutangKaryawan)
        VisibilitasKolomTabelBerdasarkanCOA(Potongan_Lainnya, KodeTautanCOA_HutangLancarLainnya)

        AksesDatabase_General(Tutup)

        datagridUtama.Visibility = Visibility.Visible

    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_InputHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputHutang.Click
        win_InputGaji = New wpfWin_InputGaji
        win_InputGaji.ResetForm()
        win_InputGaji.JalurMasuk = Halaman_BUKUPENGAWASANGAJI
        win_InputGaji.FungsiForm = FungsiForm_TAMBAH
        win_InputGaji.ShowDialog()
    End Sub


    Private Sub btn_EditHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditHutang.Click

        Dim AdaPengajuan As Boolean = CekDataPengajuanBerdasarkanNomorBP(NomorBPHG_Terseleksi)

        If AdaPengajuan Then
            PesanPeringatan("Sudah ada data pengajuan/pembayaran untuk Gaji Bulan " & cmb_Bulan.SelectedValue & "." & Enter2Baris &
                            "Data tidak dapat dihapus.")
            Return
        End If

        If NomorID_Terseleksi = 0 Then Return

        win_InputGaji = New wpfWin_InputGaji
        win_InputGaji.ResetForm()

        If PotonganHutangPPhPasal21Rutin_Terseleksi > 0 Then win_InputGaji.PilihanPPh_Rutin = win_InputGaji.PilihanPPh_PPhDipotongDitunjang
        If PPhDitanggungRutin_Terseleksi > 0 Then win_InputGaji.PilihanPPh_Rutin = win_InputGaji.PilihanPPh_PPhDitanggung

        If PotonganHutangPPhPasal21Pesangon_Terseleksi > 0 Then win_InputGaji.PilihanPPh_Pesangon = win_InputGaji.PilihanPPh_PPhDipotongDitunjang
        If PPhDitanggungPesangon_Terseleksi > 0 Then win_InputGaji.PilihanPPh_Pesangon = win_InputGaji.PilihanPPh_PPhDitanggung

        If PotonganHutangPPhPasal21Pesangon_Terseleksi > 0 Or PPhDitanggungPesangon_Terseleksi > 0 Then
            win_InputGaji.AdaPPhPesangon = True
        Else
            win_InputGaji.AdaPPhPesangon = False
        End If

        win_InputGaji.FungsiForm = FungsiForm_EDIT
        win_InputGaji.JalurMasuk = Halaman_BUKUPENGAWASANGAJI
        win_InputGaji.NomorID = NomorID_Terseleksi
        IsiValueComboBypassTerkunci(win_InputGaji.cmb_Bulan, Bulan_Terseleksi) '(Tidak ada pilihan, selain bulan bersangkutan).
        win_InputGaji.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(TanggalTransaksi_Terseleksi)

        win_InputGaji.txt_GajiBagianProduksi.Text = GajiBagianProduksi_Terseleksi
        win_InputGaji.txt_GajiBagianProduksi2.Text = GajiBagianProduksi2_Terseleksi
        win_InputGaji.txt_GajiBagianProduksi3.Text = GajiBagianProduksi3_Terseleksi
        win_InputGaji.txt_GajiBagianProduksi4.Text = GajiBagianProduksi4_Terseleksi
        win_InputGaji.txt_ThrBonusProduksi.Text = ThrBonusProduksi_Terseleksi
        win_InputGaji.txt_TunjanganPPh21Produksi.Text = TunjanganPPh21Produksi_Terseleksi
        win_InputGaji.txt_BpjsTkJkkJkmProduksi.Text = BpjsTkJkkJkmProduksi_Terseleksi
        win_InputGaji.txt_BpjsTkJhtIpProduksi.Text = BpjsTkJhtIpProduksi_Terseleksi
        win_InputGaji.txt_BpjsKesehatanProduksi.Text = BpjsKesehatanProduksi_Terseleksi
        win_InputGaji.txt_AsuransiKaryawanProduksi.Text = AsuransiKaryawanProduksi_Terseleksi
        win_InputGaji.txt_PesangonKaryawanProduksi.Text = PesangonKaryawanProduksi_Terseleksi
        'win_InputGaji.txt_JumlahGajiBagianProduksi.Text = JumlahGajiBagianProduksi_Terseleksi
        win_InputGaji.txt_BpjsTkJhtIpProduksiDibayarKaryawan.Text = BpjsTkJhtIpProduksiDibayarKaryawan_Terseleksi
        win_InputGaji.txt_BpjsKesehatanProduksiDibayarKaryawan.Text = BpjsKesehatanProduksiDibayarKaryawan_Terseleksi

        win_InputGaji.txt_GajiBagianAdministrasi.Text = GajiBagianAdministrasi_Terseleksi
        win_InputGaji.txt_GajiBagianAdministrasi2.Text = GajiBagianAdministrasi2_Terseleksi
        win_InputGaji.txt_GajiBagianAdministrasi3.Text = GajiBagianAdministrasi3_Terseleksi
        win_InputGaji.txt_GajiBagianAdministrasi4.Text = GajiBagianAdministrasi4_Terseleksi
        win_InputGaji.txt_ThrBonusAdministrasi.Text = ThrBonusAdministrasi_Terseleksi
        win_InputGaji.txt_TunjanganPPh21Administrasi.Text = TunjanganPPh21Administrasi_Terseleksi
        win_InputGaji.txt_BpjsTkJkkJkmAdministrasi.Text = BpjsTkJkkJkmAdministrasi_Terseleksi
        win_InputGaji.txt_BpjsTkJhtIpAdministrasi.Text = BpjsTkJhtIpAdministrasi_Terseleksi
        win_InputGaji.txt_BpjsKesehatanAdministrasi.Text = BpjsKesehatanAdministrasi_Terseleksi
        win_InputGaji.txt_AsuransiKaryawanAdministrasi.Text = AsuransiKaryawanAdministrasi_Terseleksi
        win_InputGaji.txt_PesangonKaryawanAdministrasi.Text = PesangonKaryawanAdministrasi_Terseleksi
        'win_InputGaji.txt_JumlahGajiBagianAdministrasi.Text = JumlahGajiBagianAdministrasi_Terseleksi
        win_InputGaji.txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.Text = BpjsTkJhtIpAdministrasiDibayarKaryawan_Terseleksi
        win_InputGaji.txt_BpjsKesehatanAdministrasiDibayarKaryawan.Text = BpjsKesehatanAdministrasiDibayarKaryawan_Terseleksi

        'win_InputGaji.txt_JumlahGajiKotor.Text = JumlahGajiKotor_Terseleksi
        win_InputGaji.txt_PotonganHutangBpjsKesehatan.Text = PotonganHutangBpjsKesehatan_Terseleksi
        win_InputGaji.txt_PotonganHutangBpjsKetenagakerjaan.Text = PotonganHutangBpjsKetenagakerjaan_Terseleksi
        win_InputGaji.txt_PotonganHutangKoperasi.Text = PotonganHutangKoperasi_Terseleksi
        win_InputGaji.txt_PotonganHutangPPhPasal21Rutin.Text = PotonganHutangPPhPasal21Rutin_Terseleksi
        win_InputGaji.txt_PotonganHutangPPhPasal21Pesangon.Text = PotonganHutangPPhPasal21Pesangon_Terseleksi
        win_InputGaji.txt_PotonganHutangSerikat.Text = PotonganHutangSerikat_Terseleksi
        win_InputGaji.txt_PotonganKasbonKaryawan.Text = PotonganKasbonKaryawan_Terseleksi
        win_InputGaji.txt_PotonganLainnya.Text = PotonganLainnya_Terseleksi
        'win_InputGaji.txt_JumlahPotongan.Text = JumlahPotongan_Terseleksi
        'win_InputGaji.txt_JumlahGajiDibayarkan.Text = JumlahGajiDibayarkan_Terseleksi
        win_InputGaji.txt_PPhDitanggungRutin.Text = PPhDitanggungRutin_Terseleksi
        win_InputGaji.txt_PPhDitanggungPesangon.Text = PPhDitanggungPesangon_Terseleksi
        IsiValueElemenRichTextBox(win_InputGaji.txt_Keterangan, Keterangan_Terseleksi)
        win_InputGaji.NomorJVGaji = NomorJV_Gaji_Terseleksi
        win_InputGaji.ShowDialog()

        If JenisTampilan = JenisTampilan_ALL Then
            Sub_JenisTampilan_ALL()
        Else
            Sub_JenisTampilan_DETAIL()
        End If

    End Sub


    Private Sub btn_HapusHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusHutang.Click

        Dim AdaPengajuan As Boolean = CekDataPengajuanBerdasarkanNomorBP(NomorBPHG_Terseleksi)

        If AdaPengajuan Then
            PesanPeringatan("Sudah ada data pengajuan/pembayaran untuk Gaji Bulan " & cmb_Bulan.SelectedValue & "." & Enter2Baris &
                            "Data tidak dapat dihapus.")
            Return
        End If

        If Not TanyaKonfirmasi("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris & "Yakin ingin menghapus?") Then Return

        'TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PengawasanGaji :
        cmd = New OdbcCommand(" DELETE FROM tbl_PengawasanGaji WHERE Nomor_ID = '" & NomorID_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        'Hapus Data di tbl_Transaksi (Jurnal) :
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_Gaji_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        TampilkanData()
        If usc_BukuPengawasanHutangPPhPasal21.StatusAktif Then usc_BukuPengawasanHutangPPhPasal21.TampilkanData()

        Pesan_Sukses("Data terpilih berhasil dihapus.")

    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Gaji_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Gaji_Terseleksi)
        ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
            Return
        End If
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub cmb_TahunTelusurData_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_TahunTelusurData.SelectionChanged
        TahunTelusurData = cmb_TahunTelusurData.SelectedValue
        If TahunTelusurData = 0 Then
            cmb_TahunTelusurData.Text = TahunTelusurDataTerlama
            TahunTelusurData = TahunTelusurDataTerlama
        End If
        AwalanBPHG_PlusTahunBukuTampilan = AwalanBPHG & TahunTelusurData & "-"
        PerubahanTahunTelusurData()
    End Sub
    Sub GantiTahunTelusurData_ManualPaksa(TahunPengganti As Integer)
        If TahunPengganti < TahunTelusurDataTerlama Then TahunPengganti = TahunTelusurDataTerlama
        cmb_TahunTelusurData.Text = TahunPengganti
        TahunTelusurData = TahunPengganti
        PerubahanTahunTelusurData()
    End Sub
    Sub PerubahanTahunTelusurData()
        'TahunTelusurDataSebelumnya = TahunTelusurData - 1
        'AwalanBPH_PlusTahunBuku = AwalanBPH & TahunTelusurData & "-"
        'If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
        '    VisibilitasTombolTagihan(True)
        'Else
        '    If TahunTelusurData = TahunBukuAktif Then
        '        VisibilitasTombolTagihan(True)
        '    Else
        '        VisibilitasTombolTagihan(False)
        '    End If
        'End If
        'VisibilitasKolomTabel()
        'TampilkanData()
    End Sub

    Private Sub btn_TambahTahun_Click(sender As Object, e As RoutedEventArgs) Handles btn_TambahTahun.Click

        'Dim TahunTelusurDataBaru = TahunTelusurDataTerlama - 1

        'Dim Pesan As String =
        '    "Anda akan menambahkan Tabel Tagihan " & TahunTelusurDataBaru & "." & Enter1Baris &
        '    "Lanjutkan proses..?"
        'Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        'If Pilihan = vbNo Then Return

        'Dim NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelPengawasan)
        'Dim NomorUrut = 0
        'Dim Bulan = Kosongan
        'AksesDatabase_Transaksi(Buka)
        'Do While NomorUrut < 12
        '    NomorID += 1
        '    NomorUrut += 1
        '    Bulan = BulanTerbilang(NomorUrut)
        '    cmd = New OdbcCommand(
        '        " INSERT INTO " & TabelPengawasan & " VALUES ( " &
        '        " '" & NomorID & "', " &
        '        " '" & NomorUrut & "', " &
        '        " '" & Bulan & "', " &
        '        " '" & TahunTelusurDataBaru & "', " &
        '        " '" & 0 & "', " &
        '        " '" & 0 & "', " &
        '        " '" & Kosongan & "' " &
        '        " )",
        '        KoneksiDatabaseTransaksi)
        '    cmd_ExecuteNonQuery()
        'Loop
        'AksesDatabase_Transaksi(Tutup)

        'If StatusSuntingDatabase = True Then
        '    KontenComboTahunTelusurData(False)
        '    GantiTahunTelusurData_ManualPaksa(TahunTelusurDataTerlama)
        '    Terabas()
        '    PesanPemberitahuan("Tabel Tagihan Tahun " & TahunTelusurDataBaru & " BERHASIL ditambahkan.")
        'Else
        '    PesanPeringatan("Tabel Tagihan Tahun " & TahunTelusurDataBaru & " GAGAL ditambahkan." & Enter2Baris &
        '    teks_SilakanCobaLagi_Database)
        'End If

    End Sub


    Private Sub btn_HapusTahun_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusTahun.Click

        'If TahunTelusurDataTerlama = TahunBukuAktif Then
        '    PesanPeringatan("Tabel Tagihan Tahun " & TahunTelusurDataTerlama & " tidak dapat dihapus." & Enter1Baris &
        '                    "Namun Anda dapat mengosongkannya.")
        '    Return
        'End If

        'Dim TahunTelusurDataAsal = TahunTelusurData
        'Dim TahunTelusurDataYangDihapus = TahunTelusurDataTerlama

        'Dim Pesan As String =
        '    "Anda akan menghapus tabel dan seluruh data Tagihan Tahun " & TahunTelusurDataYangDihapus & "." & Enter1Baris &
        '    "Lanjutkan proses..?"
        'Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        'If Pilihan = vbNo Then Return

        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(
        '    " DELETE FROM " & TabelPengawasan &
        '    " WHERE Tahun = '" & TahunTelusurDataYangDihapus & "' ",
        '    KoneksiDatabaseTransaksi)
        'cmd_ExecuteNonQuery()
        'AksesDatabase_Transaksi(Tutup)

        'If StatusSuntingDatabase = True Then
        '    KontenComboTahunTelusurData(False)
        '    GantiTahunTelusurData_ManualPaksa(TahunTelusurDataAsal)
        '    Terabas()
        '    PesanPemberitahuan("Tabel Tagihan Tahun " & TahunTelusurDataYangDihapus & " BERHASIL dihapus.")
        'Else
        '    PesanPeringatan("Tabel Tagihan Tahun " & TahunTelusurDataYangDihapus & " GAGAL dihapus." & Enter2Baris &
        '    teks_SilakanCobaLagi_Database)
        'End If

    End Sub


    Private Sub cmb_Bulan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Bulan.SelectionChanged
        Bulan = cmb_Bulan.SelectedValue
        NomorBulan = KonversiBulanKeAngka(Bulan)
        If ProsesLoadingForm = False Then
            Select Case Bulan
                Case Bulan_ALL
                    Sub_JenisTampilan_ALL()
                Case Bulan_REKAP
                    Sub_JenisTampilan_REKAP()
                Case Else
                    Sub_JenisTampilan_DETAIL()
            End Select
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

        NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID"))
        Bulan_Terseleksi = rowviewUtama("Bulan_")
        NomorBPHG_Terseleksi = rowviewUtama("Nomor_BPHG")
        TanggalTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Transaksi")
        GajiBagianProduksi_Terseleksi = AmbilAngka(rowviewUtama("Gaji_Bagian_Produksi"))
        GajiBagianProduksi2_Terseleksi = AmbilAngka(rowviewUtama("Gaji_Bagian_Produksi_2"))
        GajiBagianProduksi3_Terseleksi = AmbilAngka(rowviewUtama("Gaji_Bagian_Produksi_3"))
        GajiBagianProduksi4_Terseleksi = AmbilAngka(rowviewUtama("Gaji_Bagian_Produksi_4"))
        ThrBonusProduksi_Terseleksi = AmbilAngka(rowviewUtama("THR_Bonus_Produksi"))
        TunjanganPPh21Produksi_Terseleksi = AmbilAngka(rowviewUtama("Tunjangan_PPh_21_Produksi"))
        BpjsTkJkkJkmProduksi_Terseleksi = AmbilAngka(rowviewUtama("BPJS_TK_JKK_JKM_Produksi"))
        BpjsTkJhtIpProduksi_Terseleksi = AmbilAngka(rowviewUtama("BPJS_TK_JHT_IP_Produksi"))
        BpjsKesehatanProduksi_Terseleksi = AmbilAngka(rowviewUtama("BPJS_Kesehatan_Produksi"))
        AsuransiKaryawanProduksi_Terseleksi = AmbilAngka(rowviewUtama("Asuransi_Karyawan_Produksi"))
        PesangonKaryawanProduksi_Terseleksi = AmbilAngka(rowviewUtama("Pesangon_Karyawan_Produksi"))
        JumlahGajiBagianProduksi_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Gaji_Bagian_Produksi"))
        BpjsTkJhtIpProduksiDibayarKaryawan_Terseleksi = AmbilAngka(rowviewUtama("BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan"))
        BpjsKesehatanProduksiDibayarKaryawan_Terseleksi = AmbilAngka(rowviewUtama("BPJS_Kesehatan_Produksi_Dibayar_Karyawan"))

        GajiBagianAdministrasi_Terseleksi = AmbilAngka(rowviewUtama("Gaji_Bagian_Administrasi"))
        GajiBagianAdministrasi2_Terseleksi = AmbilAngka(rowviewUtama("Gaji_Bagian_Administrasi_2"))
        GajiBagianAdministrasi3_Terseleksi = AmbilAngka(rowviewUtama("Gaji_Bagian_Administrasi_3"))
        GajiBagianAdministrasi4_Terseleksi = AmbilAngka(rowviewUtama("Gaji_Bagian_Administrasi_4"))
        ThrBonusAdministrasi_Terseleksi = AmbilAngka(rowviewUtama("THR_Bonus_Administrasi"))
        TunjanganPPh21Administrasi_Terseleksi = AmbilAngka(rowviewUtama("Tunjangan_PPh_21_Administrasi"))
        BpjsTkJkkJkmAdministrasi_Terseleksi = AmbilAngka(rowviewUtama("BPJS_TK_JKK_JKM_Administrasi"))
        BpjsTkJhtIpAdministrasi_Terseleksi = AmbilAngka(rowviewUtama("BPJS_TK_JHT_IP_Administrasi"))
        BpjsKesehatanAdministrasi_Terseleksi = AmbilAngka(rowviewUtama("BPJS_Kesehatan_Administrasi"))
        AsuransiKaryawanAdministrasi_Terseleksi = AmbilAngka(rowviewUtama("Asuransi_Karyawan_Administrasi"))
        PesangonKaryawanAdministrasi_Terseleksi = AmbilAngka(rowviewUtama("Pesangon_Karyawan_Administrasi"))
        JumlahGajiBagianAdministrasi_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Gaji_Bagian_Administrasi"))
        BpjsTkJhtIpAdministrasiDibayarKaryawan_Terseleksi = AmbilAngka(rowviewUtama("BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan"))
        BpjsKesehatanAdministrasiDibayarKaryawan_Terseleksi = AmbilAngka(rowviewUtama("BPJS_Kesehatan_Administrasi_Dibayar_Karyawan"))

        JumlahGajiKotor_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Gaji_Kotor"))
        PotonganHutangBpjsKesehatan_Terseleksi = AmbilAngka(rowviewUtama("Potongan_Hutang_BPJS_Kesehatan"))
        PotonganHutangBpjsKetenagakerjaan_Terseleksi = AmbilAngka(rowviewUtama("Potongan_Hutang_BPJS_Ketenagakerjaan"))
        PotonganHutangKoperasi_Terseleksi = AmbilAngka(rowviewUtama("Potongan_Hutang_Koperasi"))
        PotonganHutangPPhPasal21Rutin_Terseleksi = AmbilAngka(rowviewUtama("Potongan_Hutang_PPh_Pasal_21_Rutin"))
        PotonganHutangPPhPasal21Pesangon_Terseleksi = AmbilAngka(rowviewUtama("Potongan_Hutang_PPh_Pasal_21_Pesangon"))
        PotonganHutangSerikat_Terseleksi = AmbilAngka(rowviewUtama("Potongan_Hutang_Serikat"))
        PotonganKasbonKaryawan_Terseleksi = AmbilAngka(rowviewUtama("Potongan_Kasbon_Karyawan"))
        PotonganLainnya_Terseleksi = AmbilAngka(rowviewUtama("Potongan_Lainnya"))
        JumlahPotongan_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Potongan"))
        JumlahGajiDibayarkan_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Gaji_Dibayarkan"))
        JumlahTagihan_Terseleksi = JumlahGajiDibayarkan_Terseleksi
        PPhDitanggungRutin_Terseleksi = AmbilAngka(rowviewUtama("PPh_Ditanggung_Rutin"))
        PPhDitanggungPesangon_Terseleksi = AmbilAngka(rowviewUtama("PPh_Ditanggung_Pesangon"))
        NomorJV_Gaji_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")

        If NomorID_Terseleksi > 0 Then
            btn_EditHutang.IsEnabled = True
            btn_HapusHutang.IsEnabled = True
        Else
            btn_EditHutang.IsEnabled = False
            btn_HapusHutang.IsEnabled = False
        End If

        If NomorJV_Gaji_Terseleksi > 0 Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If

        If JenisTampilan = JenisTampilan_REKAP Then

            TermasukHutangTahunIni_Terseleksi = True 'Ini nanti harus dilogikakan, jika ada kemungkinan pembayaran gaji untuk tahun sebelumnya.
            JumlahPembayaran_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Pembayaran"))
            SisaPembayaran_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Pembayaran"))

            TampilkanDataPembayaran()

            btn_EditHutang.IsEnabled = False

            If BarisTerseleksi >= 12 Then BersihkanSeleksi()
            If JumlahGajiDibayarkan_Terseleksi = 0 Then BersihkanSeleksi()

        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If JenisTampilan = JenisTampilan_DETAIL Or JenisTampilan = JenisTampilan_ALL Then
            btn_EditHutang_Click(sender, e)
        End If
        If JenisTampilan = JenisTampilan_REKAP And cmb_Bulan.IsEnabled = True Then
            cmb_Bulan.SelectedValue = rowviewUtama("Bulan_")
        End If
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        'If AmbilAngka(e.Row.Item("Jumlah_Potongan")) > 0 Then e.Row.Foreground = WarnaTegas_WPF
        'If AmbilAngka(e.Row.Item("Jumlah_Potongan")) = 0 Then e.Row.Foreground = WarnaPudar_WPF
        'If AmbilAngka(e.Row.Item("Selisih_")) = 0 Then
        '    e.Row.Foreground = WarnaTegas_WPF
        'Else
        '    e.Row.Foreground = WarnaPeringatan_WPF
        'End If
    End Sub



    Sub TampilkanDataPembayaran()

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then pnl_SidebarKanan.Visibility = Visibility.Visible
        datatabelBayar.Rows.Clear()

        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar As Int64 = 0
        Dim TotalBayar As Int64 = 0
        Dim KeteranganBayar
        Dim NomorJVBayar
        Dim TahunSumberDataPembayaran = 0

        Dim TahunTelusurPembayaran = TahunTelusurData
        Dim PencegahLoopingTahunTelusurDataLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunSumberDataPembayaran = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunSumberDataPembayaran = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunTelusurDataLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunSumberDataPembayaran)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP      = '" & NomorBPHG_Terseleksi & "' " &
                              " AND Status_Invoice  = '" & Status_Dibayar & "' " &
                              " ORDER BY Nomor_ID", KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    NomorIdBayar = dr.Item("Nomor_ID")
                    TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                    Referensi = dr.Item("Nomor_KK")
                    JumlahBayar = dr.Item("Jumlah_Bayar")
                    TotalBayar += JumlahBayar
                    KeteranganBayar = dr.Item("Catatan")
                    If TahunTelusurPembayaran = TahunBukuAktif Then
                        NomorJVBayar = dr.Item("Nomor_JV")
                    Else
                        NomorJVBayar = 0
                    End If
                    datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJVBayar)
                    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                        If TahunTelusurPembayaran = TahunBukuAktif Then
                            'datatabelBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaTegas
                        Else
                            'datatabelBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaPudar
                        End If
                    End If
                    If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
                    Index_BarisTabelPembayaran += 1
                Loop
                TutupDatabaseTransaksi_Alternatif()
            End If
            If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
            PencegahLoopingTahunTelusurDataLampau += 1
            TahunTelusurPembayaran += 1
        Loop

        BersihkanSeleksiTabelPembayaran()

        txt_SaldoAwalPerBaris.Text = JumlahTagihan_Terseleksi
        txt_JumlahBayarPerBaris.Text = TotalBayar                           '(Sebaiknya tidak menggunakan variabel TotalBayar_Terseleksi. Agar lebih update).
        txt_SaldoAkhirPerbaris.Text = JumlahTagihan_Terseleksi - TotalBayar '(Sebaiknya tidak menggunakan variabel SisaHutang_Terseleksi. Agar lebih update).

        If SisaPembayaran_Terseleksi > 0 Then
            lbl_SaldoAkhirPerBaris.Visibility = Visibility.Visible
            txt_SaldoAkhirPerbaris.Visibility = Visibility.Visible
            txt_SaldoAkhirPerbaris.Foreground = WarnaPeringatan_WPF
            lbl_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Text = StatusLunas_BelumLunas
            txt_KeteranganLunas.Foreground = WarnaPeringatan_WPF
        Else
            lbl_SaldoAkhirPerBaris.Visibility = Visibility.Collapsed
            txt_SaldoAkhirPerbaris.Visibility = Visibility.Collapsed
            txt_SaldoAkhirPerbaris.Foreground = WarnaTeksStandar_WPF
            lbl_KeteranganLunas.Visibility = Visibility.Visible
            txt_KeteranganLunas.Visibility = Visibility.Visible
            txt_KeteranganLunas.Text = StatusLunas_Lunas
            txt_KeteranganLunas.Foreground = WarnaTeksStandar_WPF
        End If

    End Sub


    Sub BersihkanSeleksiTabelPembayaran()
        BarisBayar_Terseleksi = -1
        datagridBayar.SelectedIndex = -1
        datagridBayar.SelectedItem = Nothing
        datagridBayar.SelectedCells.Clear()
        JumlahBarisBayar = datatabelBayar.Rows.Count
        VisibilitasTabelPembayaran()
        KetersediaanTombolJurnal(False)
        btn_EditBayar.IsEnabled = False
        btn_HapusBayar.IsEnabled = False
        NomorJV_Pembayaran_Terseleksi = 0
    End Sub


    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        If SisaPembayaran_Terseleksi <= 0 Then
            Pesan_Informasi("Gaji Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangGaji
        win_InputBuktiPengeluaran.NomorBP = NomorBPHG_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Karyawan
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, Kosongan, Kosongan, "Pembayaran Gaji " & Bulan_Terseleksi, NomorBPHG_Terseleksi,
                                JumlahGajiDibayarkan_Terseleksi, 0, 0, 0, JumlahPembayaran_Terseleksi, SisaPembayaran_Terseleksi,
                                Kosongan, Kosongan, 0, 0, 0,
                                SisaPembayaran_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub


    Private Sub btn_EditBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub


    Private Sub btn_HapusBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub


    Private Sub txt_SaldoAwalPerBaris_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalPerBaris.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalPerBaris)
    End Sub

    Private Sub txt_JumlahBayarPerBaris_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBayarPerBaris.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahBayarPerBaris)
    End Sub

    Private Sub txt_SaldoAkhirPerbaris_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAkhirPerbaris.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAkhirPerbaris)
    End Sub

    Private Sub txt_KeteranganLunas_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KeteranganLunas.TextChanged

    End Sub



    Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    End Sub
    Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
        HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomBayar IsNot Nothing Then
            BersihkanSeleksiTabelPembayaran()
        End If
    End Sub
    Private Sub datagridBayar_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBayar.SelectedCellsChanged

        KolomTerseleksiBayar = datagridBayar.CurrentColumn
        BarisBayar_Terseleksi = datagridBayar.SelectedIndex
        If BarisBayar_Terseleksi < 0 Then Return
        rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
        If Not rowviewBayar IsNot Nothing Then Return

        NomorIdPembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_ID_Bayar").ToString)
        NomorJV_Pembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_JV_Bayar").ToString)
        Referensi_Terseleksi = rowviewBayar("Referensi_")
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditBayar.IsEnabled = True
            btn_HapusBayar.IsEnabled = True
        Else
            BersihkanSeleksiTabelPembayaran()
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then KetersediaanTombolJurnal(False)


    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
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
    Dim Bulan_ As New DataGridTextColumn
    Dim Nomor_BPHG As New DataGridTextColumn
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim Gaji_Bagian_Produksi As New DataGridTextColumn
    Dim Gaji_Bagian_Produksi_2 As New DataGridTextColumn
    Dim Gaji_Bagian_Produksi_3 As New DataGridTextColumn
    Dim Gaji_Bagian_Produksi_4 As New DataGridTextColumn
    Dim THR_Bonus_Produksi As New DataGridTextColumn
    Dim Tunjangan_PPh_21_Produksi As New DataGridTextColumn
    Dim BPJS_TK_JKK_JKM_Produksi As New DataGridTextColumn
    Dim BPJS_TK_JHT_IP_Produksi As New DataGridTextColumn
    Dim BPJS_Kesehatan_Produksi As New DataGridTextColumn
    Dim Asuransi_Karyawan_Produksi As New DataGridTextColumn
    Dim Pesangon_Karyawan_Produksi As New DataGridTextColumn
    Dim Jumlah_Gaji_Bagian_Produksi As New DataGridTextColumn
    Dim BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan As New DataGridTextColumn
    Dim BPJS_Kesehatan_Produksi_Dibayar_Karyawan As New DataGridTextColumn
    Dim Gaji_Bagian_Administrasi As New DataGridTextColumn
    Dim Gaji_Bagian_Administrasi_2 As New DataGridTextColumn
    Dim Gaji_Bagian_Administrasi_3 As New DataGridTextColumn
    Dim Gaji_Bagian_Administrasi_4 As New DataGridTextColumn
    Dim THR_Bonus_Administrasi As New DataGridTextColumn
    Dim Tunjangan_PPh_21_Administrasi As New DataGridTextColumn
    Dim BPJS_TK_JKK_JKM_Administrasi As New DataGridTextColumn
    Dim BPJS_TK_JHT_IP_Administrasi As New DataGridTextColumn
    Dim BPJS_Kesehatan_Administrasi As New DataGridTextColumn
    Dim Asuransi_Karyawan_Administrasi As New DataGridTextColumn
    Dim Pesangon_Karyawan_Administrasi As New DataGridTextColumn
    Dim Jumlah_Gaji_Bagian_Administrasi As New DataGridTextColumn
    Dim BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan As New DataGridTextColumn
    Dim BPJS_Kesehatan_Administrasi_Dibayar_Karyawan As New DataGridTextColumn
    Dim Jumlah_Gaji_Kotor As New DataGridTextColumn
    Dim Potongan_Hutang_BPJS_Kesehatan As New DataGridTextColumn
    Dim Potongan_Hutang_BPJS_Ketenagakerjaan As New DataGridTextColumn
    Dim Potongan_Hutang_Koperasi As New DataGridTextColumn
    Dim Potongan_Hutang_PPh_Pasal_21_Rutin As New DataGridTextColumn
    Dim Potongan_Hutang_PPh_Pasal_21_Pesangon As New DataGridTextColumn
    Dim Potongan_Hutang_Serikat As New DataGridTextColumn
    Dim Potongan_Kasbon_Karyawan As New DataGridTextColumn
    Dim Potongan_Lainnya As New DataGridTextColumn
    Dim Jumlah_Potongan As New DataGridTextColumn
    Dim Jumlah_Gaji_Dibayarkan As New DataGridTextColumn
    Dim PPh_Ditanggung_Rutin As New DataGridTextColumn
    Dim PPh_Ditanggung_Pesangon As New DataGridTextColumn
    Dim Jumlah_Pembayaran As New DataGridTextColumn
    Dim Sisa_Pembayaran As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn



    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Bulan_")
        datatabelUtama.Columns.Add("Nomor_BPHG")
        datatabelUtama.Columns.Add("Tanggal_Transaksi")
        datatabelUtama.Columns.Add("Gaji_Bagian_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("Gaji_Bagian_Produksi_2", GetType(Int64))
        datatabelUtama.Columns.Add("Gaji_Bagian_Produksi_3", GetType(Int64))
        datatabelUtama.Columns.Add("Gaji_Bagian_Produksi_4", GetType(Int64))
        datatabelUtama.Columns.Add("THR_Bonus_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("Tunjangan_PPh_21_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_TK_JKK_JKM_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_TK_JHT_IP_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_Kesehatan_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("Asuransi_Karyawan_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("Pesangon_Karyawan_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Gaji_Bagian_Produksi", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_Kesehatan_Produksi_Dibayar_Karyawan", GetType(Int64))
        datatabelUtama.Columns.Add("Gaji_Bagian_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("Gaji_Bagian_Administrasi_2", GetType(Int64))
        datatabelUtama.Columns.Add("Gaji_Bagian_Administrasi_3", GetType(Int64))
        datatabelUtama.Columns.Add("Gaji_Bagian_Administrasi_4", GetType(Int64))
        datatabelUtama.Columns.Add("THR_Bonus_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("Tunjangan_PPh_21_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_TK_JKK_JKM_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_TK_JHT_IP_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_Kesehatan_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("Asuransi_Karyawan_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("Pesangon_Karyawan_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Gaji_Bagian_Administrasi", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan", GetType(Int64))
        datatabelUtama.Columns.Add("BPJS_Kesehatan_Administrasi_Dibayar_Karyawan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Gaji_Kotor", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Hutang_BPJS_Kesehatan", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Hutang_BPJS_Ketenagakerjaan", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Hutang_Koperasi", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Hutang_PPh_Pasal_21_Rutin", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Hutang_PPh_Pasal_21_Pesangon", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Hutang_Serikat", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Kasbon_Karyawan", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Lainnya", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Potongan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Gaji_Dibayarkan", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Ditanggung_Rutin", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Ditanggung_Pesangon", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Pembayaran", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Pembayaran", GetType(Int64))
        datatabelUtama.Columns.Add("Nomor_JV", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")


        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Bulan_, "Bulan_", "Bulan", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPHG, "Nomor_BPHG", "Nomor BPHG", 99, FormatString, KiriTengah, KunciUrut, TerlihatKhususProgrammer)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Transaksi, "Tanggal_Transaksi", "Tanggal" & Enter1Baris & "Transaksi", 81, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Gaji_Bagian_Produksi, "Gaji_Bagian_Produksi", "1", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Gaji_Bagian_Produksi_2, "Gaji_Bagian_Produksi_2", "2", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Gaji_Bagian_Produksi_3, "Gaji_Bagian_Produksi_3", "3", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Gaji_Bagian_Produksi_4, "Gaji_Bagian_Produksi_4", "4", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, THR_Bonus_Produksi, "THR_Bonus_Produksi", "5", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tunjangan_PPh_21_Produksi, "Tunjangan_PPh_21_Produksi", "6", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_TK_JKK_JKM_Produksi, "BPJS_TK_JKK_JKM_Produksi", "7", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_TK_JHT_IP_Produksi, "BPJS_TK_JHT_IP_Produksi", "8", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_Kesehatan_Produksi, "BPJS_Kesehatan_Produksi", "9", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Asuransi_Karyawan_Produksi, "Asuransi_Karyawan_Produksi", "10", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pesangon_Karyawan_Produksi, "Pesangon_Karyawan_Produksi", "11", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Gaji_Bagian_Produksi, "Jumlah_Gaji_Bagian_Produksi", "Jumlah Gaji Produksi", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan, "BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan", "BPJS TK-JHT/IP Produksi Dibayar Karyawan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_Kesehatan_Produksi_Dibayar_Karyawan, "BPJS_Kesehatan_Produksi_Dibayar_Karyawan", "BPJS Kesehatan Produksi Dibayar Karyawan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Gaji_Bagian_Administrasi, "Gaji_Bagian_Administrasi", "1", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Gaji_Bagian_Administrasi_2, "Gaji_Bagian_Administrasi_2", "2", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Gaji_Bagian_Administrasi_3, "Gaji_Bagian_Administrasi_3", "3", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Gaji_Bagian_Administrasi_4, "Gaji_Bagian_Administrasi_4", "4", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, THR_Bonus_Administrasi, "THR_Bonus_Administrasi", "5", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tunjangan_PPh_21_Administrasi, "Tunjangan_PPh_21_Administrasi", "6", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_TK_JKK_JKM_Administrasi, "BPJS_TK_JKK_JKM_Administrasi", "7", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_TK_JHT_IP_Administrasi, "BPJS_TK_JHT_IP_Administrasi", "8", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_Kesehatan_Administrasi, "BPJS_Kesehatan_Administrasi", "9", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Asuransi_Karyawan_Administrasi, "Asuransi_Karyawan_Administrasi", "10", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Pesangon_Karyawan_Administrasi, "Pesangon_Karyawan_Administrasi", "11", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Gaji_Bagian_Administrasi, "Jumlah_Gaji_Bagian_Administrasi", "Jumlah Gaji Administrasi", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan, "BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan", "BPJS TK-JHT/IP Administrasi Dibayar Karyawan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, BPJS_Kesehatan_Administrasi_Dibayar_Karyawan, "BPJS_Kesehatan_Administrasi_Dibayar_Karyawan", "BPJS Kesehatan Administrasi Dibayar Karyawan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Gaji_Kotor, "Jumlah_Gaji_Kotor", "Jumlah" & Enter1Baris & "Gaji" & Enter1Baris & "Kotor", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Hutang_BPJS_Kesehatan, "Potongan_Hutang_BPJS_Kesehatan", "X", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Hutang_BPJS_Ketenagakerjaan, "Potongan_Hutang_BPJS_Ketenagakerjaan", "X", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Hutang_Koperasi, "Potongan_Hutang_Koperasi", "Potongan" & Enter1Baris & "X", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Hutang_PPh_Pasal_21_Rutin, "Potongan_Hutang_PPh_Pasal_21_Rutin", "X", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Hutang_PPh_Pasal_21_Pesangon, "Potongan_Hutang_PPh_Pasal_21_Pesangon", "X", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Hutang_Serikat, "Potongan_Hutang_Serikat", "X", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Kasbon_Karyawan, "Potongan_Kasbon_Karyawan", "X", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Lainnya, "Potongan_Lainnya", "X", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Potongan, "Jumlah_Potongan", "Jumlah" & Enter1Baris & "Potongan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Gaji_Dibayarkan, "Jumlah_Gaji_Dibayarkan", "Jumlah" & Enter1Baris & "Gaji" & Enter1Baris & "Dibayarkan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Rutin, "PPh_Ditanggung_Rutin", "PPh" & Enter1Baris & "Ditanggung" & Enter1Baris & "Rutin", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung_Pesangon, "PPh_Ditanggung_Pesangon", "PPh" & Enter1Baris & "Ditanggung" & Enter1Baris & "Pesangon", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Pembayaran, "Jumlah_Pembayaran", "Jumlah" & Enter1Baris & "Pembayaran", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Pembayaran, "Sisa_Pembayaran", "Sisa" & Enter1Baris & "Pembayaran", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 63, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 270, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub

    'Tabel Bayar :
    Public datatabelBayar As DataTable
    Public dataviewBayar As DataView
    Public rowviewBayar As DataRowView
    Public newRowBayar As DataRow
    Public HeaderKolomBayar As DataGridColumnHeader
    Public KolomTerseleksiBayar As DataGridColumn
    Public BarisBayar_Terseleksi As Integer
    Public JumlahBarisBayar As Integer

    Dim Nomor_ID_Bayar As New DataGridTextColumn
    Dim Tanggal_Bayar As New DataGridTextColumn
    Dim Referensi_ As New DataGridTextColumn
    Dim Nominal_Bayar As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar()

        datatabelBayar = New DataTable
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_, "Referensi_", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        Buat_DataTabelBayar()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_SaldoBerdasarkanList.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.IsReadOnly = True
        txt_SelisihSaldo.IsReadOnly = True
        txt_SaldoAwalPerBaris.IsReadOnly = True
        txt_JumlahBayarPerBaris.IsReadOnly = True
        txt_SaldoAkhirPerbaris.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_AJP.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
        lbl_KeteranganLunas.Visibility = Visibility.Collapsed
        txt_KeteranganLunas.Visibility = Visibility.Collapsed
        VisibilitasInfoSaldo(False)
        lbl_SaldoAwalBerdasarkanCOA.Visibility = Visibility.Collapsed
        txt_SaldoAwalBerdasarkanCOA.Visibility = Visibility.Collapsed
        lbl_AJP.Visibility = Visibility.Collapsed
        txt_AJP.Visibility = Visibility.Collapsed
        lbl_TotalTabel.Visibility = Visibility.Collapsed
        txt_TotalTabel.Visibility = Visibility.Collapsed
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
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        StatusAktif = False
    End Sub




    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean
    Public KesesuaianJurnal As Boolean

    Dim SaldoAwal_BerdasarkanList As Int64
    Dim SaldoAwal_BerdasarkanCOA As Int64
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian As Int64
    Dim SaldoAkhir_BerdasarkanList As Int64
    Dim SaldoAkhir_BerdasarkanCOA As Int64
    Dim JumlahPenyesuaianSaldo As Int64

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutang, SaldoAwal_BerdasarkanCOA, JumlahPenyesuaianSaldo, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian,
                                                                  txt_SaldoAwalBerdasarkanCOA, txt_AJP, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutang, SaldoAkhir_BerdasarkanCOA, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub CekKesesuaianSaldoAwal()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian, KesesuaianSaldoAwal,
                                      btn_Sesuaikan, txt_SaldoBerdasarkanList, txt_saldoBerdasarkanCOA_PlusPenyesuaian, txt_SelisihSaldo)
    End Sub

    Sub CekKesesuaianSaldoAkhir()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList, SaldoAkhir_BerdasarkanCOA, KesesuaianSaldoAkhir,
                                      btn_Sesuaikan, txt_SaldoBerdasarkanList, txt_saldoBerdasarkanCOA_PlusPenyesuaian, txt_SelisihSaldo)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Private Sub txt_SelisihSaldo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo)
    End Sub

    Private Sub btn_Sesuaikan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutang, SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA)
    End Sub

    Private Sub txt_AJP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP)
    End Sub

    Private Sub txt_TotalTabel_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel)
    End Sub



    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================

End Class
