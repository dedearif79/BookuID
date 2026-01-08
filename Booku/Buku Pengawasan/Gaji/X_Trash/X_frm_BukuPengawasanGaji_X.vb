Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanGaji_X

    Public JudulForm
    Public JenisTampilan
    Public JenisTampilan_DETAIL = "DETAIL"
    Public JenisTampilan_REKAP = "REKAP"
    Public JenisTampilan_ALL = "ALL"
    Public Bulan_All = "ALL"
    Dim Bulan_Rekap = "REKAP"

    Dim Bulan_Combo
    Dim NomorBulan
    Dim JumlahBarisDalamSatuBulan

    Dim NomorUrut = 0
    Dim MaxLooping
    Dim NomorID
    Dim NomorBPHG
    Dim TanggalTransaksi
    Dim GajiBagianProduksi
    Dim GajiBagianProduksi2
    Dim GajiBagianProduksi3
    Dim GajiBagianProduksi4
    Dim ThrBonusProduksi
    Dim TunjanganPPh21Produksi
    Dim BpjsTkJkkJkmProduksi
    Dim BpjsTkJhtIpProduksi
    Dim BpjsKesehatanProduksi
    Dim AsuransiKaryawanProduksi
    Dim PesangonKaryawanProduksi
    Dim JumlahGajiBagianProduksi
    Dim BpjsTkJhtIpProduksiDibayarKaryawan
    Dim GajiBagianAdministrasi
    Dim GajiBagianAdministrasi2
    Dim GajiBagianAdministrasi3
    Dim GajiBagianAdministrasi4
    Dim ThrBonusAdministrasi
    Dim TunjanganPPh21Administrasi
    Dim BpjsTkJkkJkmAdministrasi
    Dim BpjsTkJhtIpAdministrasi
    Dim BpjsKesehatanAdministrasi
    Dim AsuransiKaryawanAdministrasi
    Dim PesangonKaryawanAdministrasi
    Dim JumlahGajiBagianAdministrasi
    Dim BpjsTkJhtIpAdministrasiDibayarKaryawan
    'Dim TunjanganPPhPasal21
    'Dim BiayaBpjsKesehatan
    'Dim BiayaBpjsKetenagakerjaan
    Dim JumlahGajiKotor
    Dim PotonganHutangBpjsKesehatan
    Dim PotonganHutangBpjsKetenagakerjaan
    Dim PotonganHutangKoperasi
    Dim PotonganHutangPPhPasal21Rutin
    Dim PotonganHutangPPhPasal21Pesangon
    Dim PotonganHutangSerikat
    Dim PotonganKasbonKaryawan
    Dim PotonganLainnya
    Dim JumlahPotongan
    Dim JumlahGajiDibayarkan
    Dim PPhDitanggungRutin
    Dim PPhDitanggungPesangon
    Dim JumlahPembayaran
    Dim SisaPembayaran
    Dim NomorJV_Gaji
    Dim Keterangan

    Dim RekapPerBulan_GajiBagianProduksi
    Dim RekapPerBulan_GajiBagianProduksi2
    Dim RekapPerBulan_GajiBagianProduksi3
    Dim RekapPerBulan_GajiBagianProduksi4
    Dim RekapPerBulan_ThrBonusProduksi
    Dim RekapPerBulan_TunjanganPPh21Produksi
    Dim RekapPerBulan_BpjsTkJkkJkmProduksi
    Dim RekapPerBulan_BpjsTkJhtIpProduksi
    Dim RekapPerBulan_BpjsKesehatanProduksi
    Dim RekapPerBulan_AsuransiKaryawanProduksi
    Dim RekapPerBulan_PesangonKaryawanProduksi
    Dim RekapPerBulan_JumlahGajiBagianProduksi
    Dim RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan
    Dim RekapPerBulan_GajiBagianAdministrasi
    Dim RekapPerBulan_GajiBagianAdministrasi2
    Dim RekapPerBulan_GajiBagianAdministrasi3
    Dim RekapPerBulan_GajiBagianAdministrasi4
    Dim RekapPerBulan_ThrBonusAdministrasi
    Dim RekapPerBulan_TunjanganPPh21Administrasi
    Dim RekapPerBulan_BpjsTkJkkJkmAdministrasi
    Dim RekapPerBulan_BpjsTkJhtIpAdministrasi
    Dim RekapPerBulan_BpjsKesehatanAdministrasi
    Dim RekapPerBulan_AsuransiKaryawanAdministrasi
    Dim RekapPerBulan_PesangonKaryawanAdministrasi
    Dim RekapPerBulan_JumlahGajiBagianAdministrasi
    Dim RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan
    'Dim RekapPerBulan_TunjanganPPhPasal21
    'Dim RekapPerBulan_BiayaBpjsKesehatan
    'Dim RekapPerBulan_BiayaBpjsKetenagakerjaan
    Dim RekapPerBulan_JumlahGajiKotor
    Dim RekapPerBulan_PotonganHutangBpjsKesehatan
    Dim RekapPerBulan_PotonganHutangBpjsKetenagakerjaan
    Dim RekapPerBulan_PotonganHutangKoperasi
    Dim RekapPerBulan_PotonganHutangPPhPasal21Rutin
    Dim RekapPerBulan_PotonganHutangPPhPasal21Pesangon
    Dim RekapPerBulan_PotonganHutangSerikat
    Dim RekapPerBulan_PotonganKasbonKaryawan
    Dim RekapPerBulan_PotonganLainnya
    Dim RekapPerBulan_JumlahPotongan
    Dim RekapPerBulan_JumlahGajiDibayarkan
    Dim RekapPerBulan_PPhDitanggungRutin
    Dim RekapPerBulan_PPhDitanggungPesangon

    Dim RekapTotal_GajiBagianProduksi
    Dim RekapTotal_GajiBagianProduksi2
    Dim RekapTotal_GajiBagianProduksi3
    Dim RekapTotal_GajiBagianProduksi4
    Dim RekapTotal_ThrBonusProduksi
    Dim RekapTotal_TunjanganPPh21Produksi
    Dim RekapTotal_BpjsTkJkkJkmProduksi
    Dim RekapTotal_BpjsTkJhtIpProduksi
    Dim RekapTotal_BpjsKesehatanProduksi
    Dim RekapTotal_AsuransiKaryawanProduksi
    Dim RekapTotal_PesangonKaryawanProduksi
    Dim RekapTotal_JumlahGajiBagianProduksi
    Dim RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan
    Dim RekapTotal_GajiBagianAdministrasi
    Dim RekapTotal_GajiBagianAdministrasi2
    Dim RekapTotal_GajiBagianAdministrasi3
    Dim RekapTotal_GajiBagianAdministrasi4
    Dim RekapTotal_ThrBonusAdministrasi
    Dim RekapTotal_TunjanganPPh21Administrasi
    Dim RekapTotal_BpjsTkJkkJkmAdministrasi
    Dim RekapTotal_BpjsTkJhtIpAdministrasi
    Dim RekapTotal_BpjsKesehatanAdministrasi
    Dim RekapTotal_AsuransiKaryawanAdministrasi
    Dim RekapTotal_PesangonKaryawanAdministrasi
    Dim RekapTotal_JumlahGajiBagianAdministrasi
    Dim RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan
    'Dim RekapTotal_TunjanganPPhPasal21
    'Dim RekapTotal_BiayaBpjsKesehatan
    'Dim RekapTotal_BiayaBpjsKetenagakerjaan
    Dim RekapTotal_JumlahGajiKotor
    Dim RekapTotal_PotonganHutangBpjsKesehatan
    Dim RekapTotal_PotonganHutangBpjsKetenagakerjaan
    Dim RekapTotal_PotonganHutangKoperasi
    Dim RekapTotal_PotonganHutangPPhPasal21Rutin
    Dim RekapTotal_PotonganHutangPPhPasal21Pesangon
    Dim RekapTotal_PotonganHutangSerikat
    Dim RekapTotal_PotonganKasbonKaryawan
    Dim RekapTotal_PotonganLainnya
    Dim RekapTotal_JumlahPotongan
    Dim RekapTotal_JumlahGajiDibayarkan
    Dim RekapTotal_PPhDitanggungRutin
    Dim RekapTotal_PPhDitanggungPesangon
    Dim RekapTotal_JumlahPembayaran
    Dim RekapTotal_SisaPembayaran

    Dim Baris_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim Bulan_Terseleksi
    Dim NomorBPHG_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim GajiBagianProduksi_Terseleksi
    Dim GajiBagianProduksi2_Terseleksi
    Dim GajiBagianProduksi3_Terseleksi
    Dim GajiBagianProduksi4_Terseleksi
    Dim ThrBonusProduksi_Terseleksi
    Dim TunjanganPPh21Produksi_Terseleksi
    Dim BpjsTkJkkJkmProduksi_Terseleksi
    Dim BpjsTkJhtIpProduksi_Terseleksi
    Dim BpjsKesehatanProduksi_Terseleksi
    Dim AsuransiKaryawanProduksi_Terseleksi
    Dim PesangonKaryawanProduksi_Terseleksi
    Dim JumlahGajiBagianProduksi_Terseleksi
    Dim BpjsTkJhtIpProduksi_DibayarKaryawan_Terseleksi
    Dim GajiBagianAdministrasi_Terseleksi
    Dim GajiBagianAdministrasi2_Terseleksi
    Dim GajiBagianAdministrasi3_Terseleksi
    Dim GajiBagianAdministrasi4_Terseleksi
    Dim ThrBonusAdministrasi_Terseleksi
    Dim TunjanganPPh21Administrasi_Terseleksi
    Dim BpjsTkJkkJkmAdministrasi_Terseleksi
    Dim BpjsTkJhtIpAdministrasi_Terseleksi
    Dim BpjsKesehatanAdministrasi_Terseleksi
    Dim AsuransiKaryawanAdministrasi_Terseleksi
    Dim PesangonKaryawanAdministrasi_Terseleksi
    Dim JumlahGajiBagianAdministrasi_Terseleksi
    Dim BpjsTkJhtIpAdministrasi_DibayarKaryawan_Terseleksi
    'Dim TunjanganPPhPasal21_Terseleksi
    'Dim BiayaBpjsKesehatan_Terseleksi
    'Dim BiayaBpjsKetenagakerjaan_Terseleksi
    Dim JumlahGajiKotor_Terseleksi
    Dim PotonganHutangBpjsKesehatan_Terseleksi
    Dim PotonganHutangBpjsKetenagakerjaan_Terseleksi
    Dim PotonganHutangKoperasi_Terseleksi
    Dim PotonganHutangPPhPasal21Rutin_Terseleksi
    Dim PotonganHutangPPhPasal21Pesangon_Terseleksi
    Dim PotonganHutangSerikat_Terseleksi
    Dim PotonganKasbonKaryawan_Terseleksi
    Dim PotonganLainnya_Terseleksi
    Dim JumlahPotongan_Terseleksi
    Dim JumlahGajiDibayarkan_Terseleksi
    Dim PPhDitanggungRutin_Terseleksi
    Dim PPhDitanggungPesangon_Terseleksi
    Dim JumlahPembayaran_Terseleksi
    Dim SisaPembayaran_Terseleksi
    Dim NomorJV_Gaji_Terseleksi
    Dim Keterangan_Terseleksi

    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi

    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer
    Dim TermasukHutangTahunIni_Terseleksi As Boolean


    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True
        Style_HalamanModul(Me)

        KontenCombo_Bulan()
        If EksekusiKode = True Then Sub_JenisTampilan_REKAP()

        ProsesLoadingForm = False

    End Sub

    Sub KontenCombo_Bulan()
        cmb_Bulan.Items.Clear()
        cmb_Bulan.Items.Add(Bulan_All)
        cmb_Bulan.Items.Add(Bulan_Rekap)
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
        If EksekusiKode = True Then cmb_Bulan.Text = Bulan_Rekap
    End Sub

    Sub RefreshTampilanData()
        cmb_Bulan.Text = Bulan_Rekap
    End Sub

    Sub TampilkanData()

        'Judul Halaman :
        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        VisibilitasKolom()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

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
            NomorBulan = KonversiBulanKeAngka(Bulan_Combo)
            MaxLooping = NomorBulan
        End If

        AksesDatabase_Transaksi(Buka)

        Do While NomorBulan <= MaxLooping

            JumlahBarisDalamSatuBulan = 0
            Bulan_Tabel = BulanTerbilang(NomorBulan)
            NomorBPHG = AwalanBPHG_PlusTahunBuku & NomorBulan
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
                Keterangan = dr.Item("Keterangan")

                RekapPerBulan_GajiBagianProduksi = AmbilAngka(RekapPerBulan_GajiBagianProduksi) + AmbilAngka(GajiBagianProduksi)
                RekapPerBulan_GajiBagianProduksi2 = AmbilAngka(RekapPerBulan_GajiBagianProduksi2) + AmbilAngka(GajiBagianProduksi2)
                RekapPerBulan_GajiBagianProduksi3 = AmbilAngka(RekapPerBulan_GajiBagianProduksi3) + AmbilAngka(GajiBagianProduksi3)
                RekapPerBulan_GajiBagianProduksi4 = AmbilAngka(RekapPerBulan_GajiBagianProduksi4) + AmbilAngka(GajiBagianProduksi4)
                RekapPerBulan_ThrBonusProduksi = AmbilAngka(RekapPerBulan_ThrBonusProduksi) + AmbilAngka(ThrBonusProduksi)
                RekapPerBulan_TunjanganPPh21Produksi = AmbilAngka(RekapPerBulan_TunjanganPPh21Produksi) + AmbilAngka(TunjanganPPh21Produksi)
                RekapPerBulan_BpjsTkJkkJkmProduksi = AmbilAngka(RekapPerBulan_BpjsTkJkkJkmProduksi) + AmbilAngka(BpjsTkJkkJkmProduksi)
                RekapPerBulan_BpjsTkJhtIpProduksi = AmbilAngka(RekapPerBulan_BpjsTkJhtIpProduksi) + AmbilAngka(BpjsTkJhtIpProduksi)
                RekapPerBulan_BpjsKesehatanProduksi = AmbilAngka(RekapPerBulan_BpjsKesehatanProduksi) + AmbilAngka(BpjsKesehatanProduksi)
                RekapPerBulan_AsuransiKaryawanProduksi = AmbilAngka(RekapPerBulan_AsuransiKaryawanProduksi) + AmbilAngka(AsuransiKaryawanProduksi)
                RekapPerBulan_PesangonKaryawanProduksi = AmbilAngka(RekapPerBulan_PesangonKaryawanProduksi) + AmbilAngka(PesangonKaryawanProduksi)
                RekapPerBulan_JumlahGajiBagianProduksi = AmbilAngka(RekapPerBulan_JumlahGajiBagianProduksi) + AmbilAngka(JumlahGajiBagianProduksi)
                RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan = AmbilAngka(RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan) + AmbilAngka(BpjsTkJhtIpProduksiDibayarKaryawan)

                RekapPerBulan_GajiBagianAdministrasi = AmbilAngka(RekapPerBulan_GajiBagianAdministrasi) + AmbilAngka(GajiBagianAdministrasi)
                RekapPerBulan_GajiBagianAdministrasi2 = AmbilAngka(RekapPerBulan_GajiBagianAdministrasi2) + AmbilAngka(GajiBagianAdministrasi2)
                RekapPerBulan_GajiBagianAdministrasi3 = AmbilAngka(RekapPerBulan_GajiBagianAdministrasi3) + AmbilAngka(GajiBagianAdministrasi3)
                RekapPerBulan_GajiBagianAdministrasi4 = AmbilAngka(RekapPerBulan_GajiBagianAdministrasi4) + AmbilAngka(GajiBagianAdministrasi4)
                RekapPerBulan_ThrBonusAdministrasi = AmbilAngka(RekapPerBulan_ThrBonusAdministrasi) + AmbilAngka(ThrBonusAdministrasi)
                RekapPerBulan_TunjanganPPh21Administrasi = AmbilAngka(RekapPerBulan_TunjanganPPh21Administrasi) + AmbilAngka(TunjanganPPh21Administrasi)
                RekapPerBulan_BpjsTkJkkJkmAdministrasi = AmbilAngka(RekapPerBulan_BpjsTkJkkJkmAdministrasi) + AmbilAngka(BpjsTkJkkJkmAdministrasi)
                RekapPerBulan_BpjsTkJhtIpAdministrasi = AmbilAngka(RekapPerBulan_BpjsTkJhtIpAdministrasi) + AmbilAngka(BpjsTkJhtIpAdministrasi)
                RekapPerBulan_BpjsKesehatanAdministrasi = AmbilAngka(RekapPerBulan_BpjsKesehatanAdministrasi) + AmbilAngka(BpjsKesehatanAdministrasi)
                RekapPerBulan_AsuransiKaryawanAdministrasi = AmbilAngka(RekapPerBulan_AsuransiKaryawanAdministrasi) + AmbilAngka(AsuransiKaryawanAdministrasi)
                RekapPerBulan_PesangonKaryawanAdministrasi = AmbilAngka(RekapPerBulan_PesangonKaryawanAdministrasi) + AmbilAngka(PesangonKaryawanAdministrasi)
                RekapPerBulan_JumlahGajiBagianAdministrasi = AmbilAngka(RekapPerBulan_JumlahGajiBagianAdministrasi) + AmbilAngka(JumlahGajiBagianAdministrasi)
                RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan = AmbilAngka(RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan) + AmbilAngka(BpjsTkJhtIpAdministrasiDibayarKaryawan)

                RekapPerBulan_JumlahGajiKotor = AmbilAngka(RekapPerBulan_JumlahGajiKotor) + AmbilAngka(JumlahGajiKotor)
                RekapPerBulan_PotonganHutangBpjsKesehatan = AmbilAngka(RekapPerBulan_PotonganHutangBpjsKesehatan) + AmbilAngka(PotonganHutangBpjsKesehatan)
                RekapPerBulan_PotonganHutangBpjsKetenagakerjaan = AmbilAngka(RekapPerBulan_PotonganHutangBpjsKetenagakerjaan) + AmbilAngka(PotonganHutangBpjsKetenagakerjaan)
                RekapPerBulan_PotonganHutangKoperasi = AmbilAngka(RekapPerBulan_PotonganHutangKoperasi) + AmbilAngka(PotonganHutangKoperasi)
                RekapPerBulan_PotonganHutangPPhPasal21Rutin = AmbilAngka(RekapPerBulan_PotonganHutangPPhPasal21Rutin) + AmbilAngka(PotonganHutangPPhPasal21Rutin)
                RekapPerBulan_PotonganHutangPPhPasal21Pesangon = AmbilAngka(RekapPerBulan_PotonganHutangPPhPasal21Pesangon) + AmbilAngka(PotonganHutangPPhPasal21Pesangon)
                RekapPerBulan_PotonganHutangSerikat = AmbilAngka(RekapPerBulan_PotonganHutangSerikat) + AmbilAngka(PotonganHutangSerikat)
                RekapPerBulan_PotonganKasbonKaryawan = AmbilAngka(RekapPerBulan_PotonganKasbonKaryawan) + AmbilAngka(PotonganKasbonKaryawan)
                RekapPerBulan_PotonganLainnya = AmbilAngka(RekapPerBulan_PotonganLainnya) + AmbilAngka(PotonganLainnya)
                RekapPerBulan_JumlahPotongan = AmbilAngka(RekapPerBulan_JumlahPotongan) + AmbilAngka(JumlahPotongan)
                RekapPerBulan_JumlahGajiDibayarkan = AmbilAngka(RekapPerBulan_JumlahGajiDibayarkan) + AmbilAngka(JumlahGajiDibayarkan)
                RekapPerBulan_PPhDitanggungRutin = AmbilAngka(RekapPerBulan_PPhDitanggungRutin) + AmbilAngka(PPhDitanggungRutin)
                RekapPerBulan_PPhDitanggungPesangon = AmbilAngka(RekapPerBulan_PPhDitanggungPesangon) + AmbilAngka(PPhDitanggungPesangon)

                RekapTotal_GajiBagianProduksi = AmbilAngka(RekapTotal_GajiBagianProduksi) + AmbilAngka(GajiBagianProduksi)
                RekapTotal_GajiBagianProduksi2 = AmbilAngka(RekapTotal_GajiBagianProduksi2) + AmbilAngka(GajiBagianProduksi2)
                RekapTotal_GajiBagianProduksi3 = AmbilAngka(RekapTotal_GajiBagianProduksi3) + AmbilAngka(GajiBagianProduksi3)
                RekapTotal_GajiBagianProduksi4 = AmbilAngka(RekapTotal_GajiBagianProduksi4) + AmbilAngka(GajiBagianProduksi4)
                RekapTotal_ThrBonusProduksi = AmbilAngka(RekapTotal_ThrBonusProduksi) + AmbilAngka(ThrBonusProduksi)
                RekapTotal_TunjanganPPh21Produksi = AmbilAngka(RekapTotal_TunjanganPPh21Produksi) + AmbilAngka(TunjanganPPh21Produksi)
                RekapTotal_BpjsTkJkkJkmProduksi = AmbilAngka(RekapTotal_BpjsTkJkkJkmProduksi) + AmbilAngka(BpjsTkJkkJkmProduksi)
                RekapTotal_BpjsTkJhtIpProduksi = AmbilAngka(RekapTotal_BpjsTkJhtIpProduksi) + AmbilAngka(BpjsTkJhtIpProduksi)
                RekapTotal_BpjsKesehatanProduksi = AmbilAngka(RekapTotal_BpjsKesehatanProduksi) + AmbilAngka(BpjsKesehatanProduksi)
                RekapTotal_AsuransiKaryawanProduksi = AmbilAngka(RekapTotal_AsuransiKaryawanProduksi) + AmbilAngka(AsuransiKaryawanProduksi)
                RekapTotal_PesangonKaryawanProduksi = AmbilAngka(RekapTotal_PesangonKaryawanProduksi) + AmbilAngka(PesangonKaryawanProduksi)
                RekapTotal_JumlahGajiBagianProduksi = AmbilAngka(RekapTotal_JumlahGajiBagianProduksi) + AmbilAngka(JumlahGajiBagianProduksi)
                RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan = AmbilAngka(RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan) + AmbilAngka(BpjsTkJhtIpProduksiDibayarKaryawan)

                RekapTotal_GajiBagianAdministrasi = AmbilAngka(RekapTotal_GajiBagianAdministrasi) + AmbilAngka(GajiBagianAdministrasi)
                RekapTotal_GajiBagianAdministrasi2 = AmbilAngka(RekapTotal_GajiBagianAdministrasi2) + AmbilAngka(GajiBagianAdministrasi2)
                RekapTotal_GajiBagianAdministrasi3 = AmbilAngka(RekapTotal_GajiBagianAdministrasi3) + AmbilAngka(GajiBagianAdministrasi3)
                RekapTotal_GajiBagianAdministrasi4 = AmbilAngka(RekapTotal_GajiBagianAdministrasi4) + AmbilAngka(GajiBagianAdministrasi4)
                RekapTotal_ThrBonusAdministrasi = AmbilAngka(RekapTotal_ThrBonusAdministrasi) + AmbilAngka(ThrBonusAdministrasi)
                RekapTotal_TunjanganPPh21Administrasi = AmbilAngka(RekapTotal_TunjanganPPh21Administrasi) + AmbilAngka(TunjanganPPh21Administrasi)
                RekapTotal_BpjsTkJkkJkmAdministrasi = AmbilAngka(RekapTotal_BpjsTkJkkJkmAdministrasi) + AmbilAngka(BpjsTkJkkJkmAdministrasi)
                RekapTotal_BpjsTkJhtIpAdministrasi = AmbilAngka(RekapTotal_BpjsTkJhtIpAdministrasi) + AmbilAngka(BpjsTkJhtIpAdministrasi)
                RekapTotal_BpjsKesehatanAdministrasi = AmbilAngka(RekapTotal_BpjsKesehatanAdministrasi) + AmbilAngka(BpjsKesehatanAdministrasi)
                RekapTotal_AsuransiKaryawanAdministrasi = AmbilAngka(RekapTotal_AsuransiKaryawanAdministrasi) + AmbilAngka(AsuransiKaryawanAdministrasi)
                RekapTotal_PesangonKaryawanAdministrasi = AmbilAngka(RekapTotal_PesangonKaryawanAdministrasi) + AmbilAngka(PesangonKaryawanAdministrasi)
                RekapTotal_JumlahGajiBagianAdministrasi = AmbilAngka(RekapTotal_JumlahGajiBagianAdministrasi) + AmbilAngka(JumlahGajiBagianAdministrasi)
                RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan = AmbilAngka(RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan) + AmbilAngka(BpjsTkJhtIpAdministrasiDibayarKaryawan)

                RekapTotal_JumlahGajiKotor = AmbilAngka(RekapTotal_JumlahGajiKotor) + AmbilAngka(JumlahGajiKotor)
                RekapTotal_PotonganHutangBpjsKesehatan = AmbilAngka(RekapTotal_PotonganHutangBpjsKesehatan) + AmbilAngka(PotonganHutangBpjsKesehatan)
                RekapTotal_PotonganHutangBpjsKetenagakerjaan = AmbilAngka(RekapTotal_PotonganHutangBpjsKetenagakerjaan) + AmbilAngka(PotonganHutangBpjsKetenagakerjaan)
                RekapTotal_PotonganHutangKoperasi = AmbilAngka(RekapTotal_PotonganHutangKoperasi) + AmbilAngka(PotonganHutangKoperasi)
                RekapTotal_PotonganHutangPPhPasal21Rutin = AmbilAngka(RekapTotal_PotonganHutangPPhPasal21Rutin) + AmbilAngka(PotonganHutangPPhPasal21Rutin)
                RekapTotal_PotonganHutangPPhPasal21Pesangon = AmbilAngka(RekapTotal_PotonganHutangPPhPasal21Pesangon) + AmbilAngka(PotonganHutangPPhPasal21Pesangon)
                RekapTotal_PotonganHutangSerikat = AmbilAngka(RekapTotal_PotonganHutangSerikat) + AmbilAngka(PotonganHutangSerikat)
                RekapTotal_PotonganKasbonKaryawan = AmbilAngka(RekapTotal_PotonganKasbonKaryawan) + AmbilAngka(PotonganKasbonKaryawan)
                RekapTotal_PotonganLainnya = AmbilAngka(RekapTotal_PotonganLainnya) + AmbilAngka(PotonganLainnya)
                RekapTotal_JumlahPotongan = AmbilAngka(RekapTotal_JumlahPotongan) + AmbilAngka(JumlahPotongan)
                RekapTotal_JumlahGajiDibayarkan = AmbilAngka(RekapTotal_JumlahGajiDibayarkan) + AmbilAngka(JumlahGajiDibayarkan)
                RekapTotal_PPhDitanggungRutin = AmbilAngka(RekapTotal_PPhDitanggungRutin) + AmbilAngka(PPhDitanggungRutin)
                RekapTotal_PPhDitanggungPesangon = AmbilAngka(RekapTotal_PPhDitanggungPesangon) + AmbilAngka(PPhDitanggungPesangon)

                If JenisTampilan = JenisTampilan_ALL Or JenisTampilan = JenisTampilan_DETAIL Then

                    If AmbilAngka(GajiBagianProduksi) = 0 Then GajiBagianProduksi = StripKosong
                    If AmbilAngka(GajiBagianProduksi2) = 0 Then GajiBagianProduksi2 = StripKosong
                    If AmbilAngka(GajiBagianProduksi3) = 0 Then GajiBagianProduksi3 = StripKosong
                    If AmbilAngka(GajiBagianProduksi4) = 0 Then GajiBagianProduksi4 = StripKosong
                    If AmbilAngka(ThrBonusProduksi) = 0 Then ThrBonusProduksi = StripKosong
                    If AmbilAngka(TunjanganPPh21Produksi) = 0 Then TunjanganPPh21Produksi = StripKosong
                    If AmbilAngka(BpjsTkJkkJkmProduksi) = 0 Then BpjsTkJkkJkmProduksi = StripKosong
                    If AmbilAngka(BpjsTkJhtIpProduksi) = 0 Then BpjsTkJhtIpProduksi = StripKosong
                    If AmbilAngka(BpjsKesehatanProduksi) = 0 Then BpjsKesehatanProduksi = StripKosong
                    If AmbilAngka(AsuransiKaryawanProduksi) = 0 Then AsuransiKaryawanProduksi = StripKosong
                    If AmbilAngka(PesangonKaryawanProduksi) = 0 Then PesangonKaryawanProduksi = StripKosong
                    If AmbilAngka(JumlahGajiBagianProduksi) = 0 Then JumlahGajiBagianProduksi = StripKosong
                    If AmbilAngka(BpjsTkJhtIpProduksiDibayarKaryawan) = 0 Then BpjsTkJhtIpProduksiDibayarKaryawan = StripKosong

                    If AmbilAngka(GajiBagianAdministrasi) = 0 Then GajiBagianAdministrasi = StripKosong
                    If AmbilAngka(GajiBagianAdministrasi2) = 0 Then GajiBagianAdministrasi2 = StripKosong
                    If AmbilAngka(GajiBagianAdministrasi3) = 0 Then GajiBagianAdministrasi3 = StripKosong
                    If AmbilAngka(GajiBagianAdministrasi4) = 0 Then GajiBagianAdministrasi4 = StripKosong
                    If AmbilAngka(ThrBonusAdministrasi) = 0 Then ThrBonusAdministrasi = StripKosong
                    If AmbilAngka(TunjanganPPh21Administrasi) = 0 Then TunjanganPPh21Administrasi = StripKosong
                    If AmbilAngka(BpjsTkJkkJkmAdministrasi) = 0 Then BpjsTkJkkJkmAdministrasi = StripKosong
                    If AmbilAngka(BpjsTkJhtIpAdministrasi) = 0 Then BpjsTkJhtIpAdministrasi = StripKosong
                    If AmbilAngka(BpjsKesehatanAdministrasi) = 0 Then BpjsKesehatanAdministrasi = StripKosong
                    If AmbilAngka(AsuransiKaryawanAdministrasi) = 0 Then AsuransiKaryawanAdministrasi = StripKosong
                    If AmbilAngka(PesangonKaryawanAdministrasi) = 0 Then PesangonKaryawanAdministrasi = StripKosong
                    If AmbilAngka(JumlahGajiBagianAdministrasi) = 0 Then JumlahGajiBagianAdministrasi = StripKosong
                    If AmbilAngka(BpjsTkJhtIpAdministrasiDibayarKaryawan) = 0 Then BpjsTkJhtIpAdministrasiDibayarKaryawan = StripKosong

                    If AmbilAngka(JumlahGajiKotor) = 0 Then JumlahGajiKotor = StripKosong
                    If AmbilAngka(PotonganHutangBpjsKesehatan) = 0 Then PotonganHutangBpjsKesehatan = StripKosong
                    If AmbilAngka(PotonganHutangBpjsKetenagakerjaan) = 0 Then PotonganHutangBpjsKetenagakerjaan = StripKosong
                    If AmbilAngka(PotonganHutangKoperasi) = 0 Then PotonganHutangKoperasi = StripKosong
                    If AmbilAngka(PotonganHutangPPhPasal21Rutin) = 0 Then PotonganHutangPPhPasal21Rutin = StripKosong
                    If AmbilAngka(PotonganHutangPPhPasal21Pesangon) = 0 Then PotonganHutangPPhPasal21Pesangon = StripKosong
                    If AmbilAngka(PotonganHutangSerikat) = 0 Then PotonganHutangSerikat = StripKosong
                    If AmbilAngka(PotonganKasbonKaryawan) = 0 Then PotonganKasbonKaryawan = StripKosong
                    If AmbilAngka(PotonganLainnya) = 0 Then PotonganLainnya = StripKosong
                    If AmbilAngka(JumlahPotongan) = 0 Then JumlahPotongan = StripKosong
                    If AmbilAngka(JumlahGajiDibayarkan) = 0 Then JumlahGajiDibayarkan = StripKosong
                    If AmbilAngka(PPhDitanggungRutin) = 0 Then PPhDitanggungRutin = StripKosong
                    If AmbilAngka(PPhDitanggungPesangon) = 0 Then PPhDitanggungPesangon = StripKosong

                    NomorUrut += 1
                    JumlahBarisDalamSatuBulan += 1
                    DataTabelUtama.Rows.Add(NomorUrut, NomorID, Bulan_Tabel, Kosongan, TanggalTransaksi,
                                            GajiBagianProduksi, GajiBagianProduksi2, GajiBagianProduksi3, GajiBagianProduksi4, ThrBonusProduksi,
                                            TunjanganPPh21Produksi, BpjsTkJkkJkmProduksi, BpjsTkJhtIpProduksi, BpjsKesehatanProduksi, AsuransiKaryawanProduksi, PesangonKaryawanProduksi,
                                            JumlahGajiBagianProduksi, BpjsTkJhtIpProduksiDibayarKaryawan,
                                            GajiBagianAdministrasi, GajiBagianAdministrasi2, GajiBagianAdministrasi3, GajiBagianAdministrasi4, ThrBonusAdministrasi,
                                            TunjanganPPh21Administrasi, BpjsTkJkkJkmAdministrasi, BpjsTkJhtIpAdministrasi, BpjsKesehatanAdministrasi, AsuransiKaryawanAdministrasi, PesangonKaryawanAdministrasi,
                                            JumlahGajiBagianAdministrasi, BpjsTkJhtIpAdministrasiDibayarKaryawan,
                                            JumlahGajiKotor,
                                            PotonganHutangBpjsKesehatan, PotonganHutangBpjsKetenagakerjaan, PotonganHutangKoperasi, PotonganHutangPPhPasal21Rutin, PotonganHutangPPhPasal21Pesangon, PotonganHutangSerikat, PotonganKasbonKaryawan, PotonganLainnya, JumlahPotongan,
                                            JumlahGajiDibayarkan, PPhDitanggungRutin, PPhDitanggungPesangon, Kosongan, Kosongan, NomorJV_Gaji, Keterangan)
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

                If AmbilAngka(RekapPerBulan_GajiBagianProduksi) = 0 Then RekapPerBulan_GajiBagianProduksi = StripKosong
                If AmbilAngka(RekapPerBulan_GajiBagianProduksi2) = 0 Then RekapPerBulan_GajiBagianProduksi2 = StripKosong
                If AmbilAngka(RekapPerBulan_GajiBagianProduksi3) = 0 Then RekapPerBulan_GajiBagianProduksi3 = StripKosong
                If AmbilAngka(RekapPerBulan_GajiBagianProduksi4) = 0 Then RekapPerBulan_GajiBagianProduksi4 = StripKosong
                If AmbilAngka(RekapPerBulan_ThrBonusProduksi) = 0 Then RekapPerBulan_ThrBonusProduksi = StripKosong
                If AmbilAngka(RekapPerBulan_TunjanganPPh21Produksi) = 0 Then RekapPerBulan_TunjanganPPh21Produksi = StripKosong
                If AmbilAngka(RekapPerBulan_BpjsTkJkkJkmProduksi) = 0 Then RekapPerBulan_BpjsTkJkkJkmProduksi = StripKosong
                If AmbilAngka(RekapPerBulan_BpjsTkJhtIpProduksi) = 0 Then RekapPerBulan_BpjsTkJhtIpProduksi = StripKosong
                If AmbilAngka(RekapPerBulan_BpjsKesehatanProduksi) = 0 Then RekapPerBulan_BpjsKesehatanProduksi = StripKosong
                If AmbilAngka(RekapPerBulan_AsuransiKaryawanProduksi) = 0 Then RekapPerBulan_AsuransiKaryawanProduksi = StripKosong
                If AmbilAngka(RekapPerBulan_PesangonKaryawanProduksi) = 0 Then RekapPerBulan_PesangonKaryawanProduksi = StripKosong
                If AmbilAngka(RekapPerBulan_JumlahGajiBagianProduksi) = 0 Then RekapPerBulan_JumlahGajiBagianProduksi = StripKosong
                If AmbilAngka(RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan) = 0 Then RekapPerBulan_BpjsTkJhtIpProduksiDibayarKaryawan = StripKosong

                If AmbilAngka(RekapPerBulan_GajiBagianAdministrasi) = 0 Then RekapPerBulan_GajiBagianAdministrasi = StripKosong
                If AmbilAngka(RekapPerBulan_GajiBagianAdministrasi2) = 0 Then RekapPerBulan_GajiBagianAdministrasi2 = StripKosong
                If AmbilAngka(RekapPerBulan_GajiBagianAdministrasi3) = 0 Then RekapPerBulan_GajiBagianAdministrasi3 = StripKosong
                If AmbilAngka(RekapPerBulan_GajiBagianAdministrasi4) = 0 Then RekapPerBulan_GajiBagianAdministrasi4 = StripKosong
                If AmbilAngka(RekapPerBulan_ThrBonusAdministrasi) = 0 Then RekapPerBulan_ThrBonusAdministrasi = StripKosong
                If AmbilAngka(RekapPerBulan_TunjanganPPh21Administrasi) = 0 Then RekapPerBulan_TunjanganPPh21Administrasi = StripKosong
                If AmbilAngka(RekapPerBulan_BpjsTkJkkJkmAdministrasi) = 0 Then RekapPerBulan_BpjsTkJkkJkmAdministrasi = StripKosong
                If AmbilAngka(RekapPerBulan_BpjsTkJhtIpAdministrasi) = 0 Then RekapPerBulan_BpjsTkJhtIpAdministrasi = StripKosong
                If AmbilAngka(RekapPerBulan_BpjsKesehatanAdministrasi) = 0 Then RekapPerBulan_BpjsKesehatanAdministrasi = StripKosong
                If AmbilAngka(RekapPerBulan_AsuransiKaryawanAdministrasi) = 0 Then RekapPerBulan_AsuransiKaryawanAdministrasi = StripKosong
                If AmbilAngka(RekapPerBulan_PesangonKaryawanAdministrasi) = 0 Then RekapPerBulan_PesangonKaryawanAdministrasi = StripKosong
                If AmbilAngka(RekapPerBulan_JumlahGajiBagianAdministrasi) = 0 Then RekapPerBulan_JumlahGajiBagianAdministrasi = StripKosong
                If AmbilAngka(RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan) = 0 Then RekapPerBulan_BpjsTkJhtIpAdministrasiDibayarKaryawan = StripKosong

                If AmbilAngka(RekapPerBulan_JumlahGajiKotor) = 0 Then RekapPerBulan_JumlahGajiKotor = StripKosong
                If AmbilAngka(RekapPerBulan_PotonganHutangBpjsKesehatan) = 0 Then RekapPerBulan_PotonganHutangBpjsKesehatan = StripKosong
                If AmbilAngka(RekapPerBulan_PotonganHutangBpjsKetenagakerjaan) = 0 Then RekapPerBulan_PotonganHutangBpjsKetenagakerjaan = StripKosong
                If AmbilAngka(RekapPerBulan_PotonganHutangKoperasi) = 0 Then RekapPerBulan_PotonganHutangKoperasi = StripKosong
                If AmbilAngka(RekapPerBulan_PotonganHutangPPhPasal21Rutin) = 0 Then RekapPerBulan_PotonganHutangPPhPasal21Rutin = StripKosong
                If AmbilAngka(RekapPerBulan_PotonganHutangPPhPasal21Pesangon) = 0 Then RekapPerBulan_PotonganHutangPPhPasal21Pesangon = StripKosong
                If AmbilAngka(RekapPerBulan_PotonganHutangSerikat) = 0 Then RekapPerBulan_PotonganHutangSerikat = StripKosong
                If AmbilAngka(RekapPerBulan_PotonganKasbonKaryawan) = 0 Then RekapPerBulan_PotonganKasbonKaryawan = StripKosong
                If AmbilAngka(RekapPerBulan_PotonganLainnya) = 0 Then RekapPerBulan_PotonganLainnya = StripKosong
                If AmbilAngka(RekapPerBulan_JumlahPotongan) = 0 Then RekapPerBulan_JumlahPotongan = StripKosong
                If AmbilAngka(RekapPerBulan_JumlahGajiDibayarkan) = 0 Then RekapPerBulan_JumlahGajiDibayarkan = StripKosong
                If AmbilAngka(RekapPerBulan_PPhDitanggungRutin) = 0 Then RekapPerBulan_PPhDitanggungRutin = StripKosong
                If AmbilAngka(RekapPerBulan_PPhDitanggungPesangon) = 0 Then RekapPerBulan_PPhDitanggungPesangon = StripKosong
                If AmbilAngka(JumlahPembayaran) = 0 Then JumlahPembayaran = StripKosong
                If AmbilAngka(SisaPembayaran) = 0 Then SisaPembayaran = StripKosong

                RekapTotal_JumlahPembayaran = AmbilAngka(RekapTotal_JumlahPembayaran) + AmbilAngka(JumlahPembayaran)
                RekapTotal_SisaPembayaran = AmbilAngka(RekapTotal_SisaPembayaran) + AmbilAngka(SisaPembayaran)

                If JenisTampilan = JenisTampilan_REKAP Then
                    DataTabelUtama.Rows.Add(NomorBulan, Kosongan, Bulan_Tabel, NomorBPHG, Kosongan,
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
                                        RekapPerBulan_JumlahGajiKotor,
                                        RekapPerBulan_PotonganHutangBpjsKesehatan, RekapPerBulan_PotonganHutangBpjsKetenagakerjaan, RekapPerBulan_PotonganHutangKoperasi, RekapPerBulan_PotonganHutangPPhPasal21Rutin, RekapPerBulan_PotonganHutangPPhPasal21Pesangon, RekapPerBulan_PotonganHutangSerikat, RekapPerBulan_PotonganKasbonKaryawan, RekapPerBulan_PotonganLainnya, RekapPerBulan_JumlahPotongan,
                                        RekapPerBulan_JumlahGajiDibayarkan, RekapPerBulan_PPhDitanggungRutin, RekapPerBulan_PPhDitanggungPesangon, JumlahPembayaran, SisaPembayaran, Kosongan, Kosongan)
                    If AmbilAngka(RekapPerBulan_JumlahGajiDibayarkan) > 0 Then DataTabelUtama.Rows(NomorBulan - 1).DefaultCellStyle.ForeColor = WarnaTegas
                    If AmbilAngka(RekapPerBulan_JumlahGajiDibayarkan) = 0 Then DataTabelUtama.Rows(NomorBulan - 1).DefaultCellStyle.ForeColor = WarnaPudar
                End If

                If JenisTampilan = JenisTampilan_ALL And JumlahBarisDalamSatuBulan > 0 Then
                    DataTabelUtama.Rows.Insert(DataTabelUtama.RowCount - JumlahBarisDalamSatuBulan, Kosongan, Kosongan, Bulan_Tabel, Bulan_Tabel)
                    DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, "Total",
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
                                        RekapPerBulan_JumlahGajiKotor,
                                        RekapPerBulan_PotonganHutangBpjsKesehatan, RekapPerBulan_PotonganHutangBpjsKetenagakerjaan, RekapPerBulan_PotonganHutangKoperasi, RekapPerBulan_PotonganHutangPPhPasal21Rutin, RekapPerBulan_PotonganHutangSerikat, RekapPerBulan_PotonganKasbonKaryawan, RekapPerBulan_PotonganLainnya, RekapPerBulan_JumlahPotongan,
                                        RekapPerBulan_JumlahGajiDibayarkan, RekapPerBulan_PPhDitanggungRutin, JumlahPembayaran, SisaPembayaran, Kosongan, Kosongan)
                    DataTabelUtama.Rows.Add()
                End If
            End If
            NomorBulan += 1
        Loop
        NomorBulan -= 1

        AksesDatabase_Transaksi(Tutup)

        If AmbilAngka(RekapTotal_GajiBagianProduksi) = 0 Then RekapTotal_GajiBagianProduksi = StripKosong
        If AmbilAngka(RekapTotal_GajiBagianProduksi2) = 0 Then RekapTotal_GajiBagianProduksi2 = StripKosong
        If AmbilAngka(RekapTotal_GajiBagianProduksi3) = 0 Then RekapTotal_GajiBagianProduksi3 = StripKosong
        If AmbilAngka(RekapTotal_GajiBagianProduksi4) = 0 Then RekapTotal_GajiBagianProduksi4 = StripKosong
        If AmbilAngka(RekapTotal_ThrBonusProduksi) = 0 Then RekapTotal_ThrBonusProduksi = StripKosong
        If AmbilAngka(RekapTotal_TunjanganPPh21Produksi) = 0 Then RekapTotal_TunjanganPPh21Produksi = StripKosong
        If AmbilAngka(RekapTotal_BpjsTkJkkJkmProduksi) = 0 Then RekapTotal_BpjsTkJkkJkmProduksi = StripKosong
        If AmbilAngka(RekapTotal_BpjsTkJhtIpProduksi) = 0 Then RekapTotal_BpjsTkJhtIpProduksi = StripKosong
        If AmbilAngka(RekapTotal_BpjsKesehatanProduksi) = 0 Then RekapTotal_BpjsKesehatanProduksi = StripKosong
        If AmbilAngka(RekapTotal_AsuransiKaryawanProduksi) = 0 Then RekapTotal_AsuransiKaryawanProduksi = StripKosong
        If AmbilAngka(RekapTotal_PesangonKaryawanProduksi) = 0 Then RekapTotal_PesangonKaryawanProduksi = StripKosong
        If AmbilAngka(RekapTotal_JumlahGajiBagianProduksi) = 0 Then RekapTotal_JumlahGajiBagianProduksi = StripKosong
        If AmbilAngka(RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan) = 0 Then RekapTotal_BpjsTkJhtIpProduksiDibayarKaryawan = StripKosong

        If AmbilAngka(RekapTotal_GajiBagianAdministrasi) = 0 Then RekapTotal_GajiBagianAdministrasi = StripKosong
        If AmbilAngka(RekapTotal_GajiBagianAdministrasi2) = 0 Then RekapTotal_GajiBagianAdministrasi2 = StripKosong
        If AmbilAngka(RekapTotal_GajiBagianAdministrasi3) = 0 Then RekapTotal_GajiBagianAdministrasi3 = StripKosong
        If AmbilAngka(RekapTotal_GajiBagianAdministrasi4) = 0 Then RekapTotal_GajiBagianAdministrasi4 = StripKosong
        If AmbilAngka(RekapTotal_ThrBonusAdministrasi) = 0 Then RekapTotal_ThrBonusAdministrasi = StripKosong
        If AmbilAngka(RekapTotal_TunjanganPPh21Administrasi) = 0 Then RekapTotal_TunjanganPPh21Administrasi = StripKosong
        If AmbilAngka(RekapTotal_BpjsTkJkkJkmAdministrasi) = 0 Then RekapTotal_BpjsTkJkkJkmAdministrasi = StripKosong
        If AmbilAngka(RekapTotal_BpjsTkJhtIpAdministrasi) = 0 Then RekapTotal_BpjsTkJhtIpAdministrasi = StripKosong
        If AmbilAngka(RekapTotal_BpjsKesehatanAdministrasi) = 0 Then RekapTotal_BpjsKesehatanAdministrasi = StripKosong
        If AmbilAngka(RekapTotal_AsuransiKaryawanAdministrasi) = 0 Then RekapTotal_AsuransiKaryawanAdministrasi = StripKosong
        If AmbilAngka(RekapTotal_PesangonKaryawanAdministrasi) = 0 Then RekapTotal_PesangonKaryawanAdministrasi = StripKosong
        If AmbilAngka(RekapTotal_JumlahGajiBagianAdministrasi) = 0 Then RekapTotal_JumlahGajiBagianAdministrasi = StripKosong
        If AmbilAngka(RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan) = 0 Then RekapTotal_BpjsTkJhtIpAdministrasiDibayarKaryawan = StripKosong

        If AmbilAngka(RekapTotal_JumlahGajiKotor) = 0 Then RekapTotal_JumlahGajiKotor = StripKosong
        If AmbilAngka(RekapTotal_PotonganHutangBpjsKesehatan) = 0 Then RekapTotal_PotonganHutangBpjsKesehatan = StripKosong
        If AmbilAngka(RekapTotal_PotonganHutangBpjsKetenagakerjaan) = 0 Then RekapTotal_PotonganHutangBpjsKetenagakerjaan = StripKosong
        If AmbilAngka(RekapTotal_PotonganHutangKoperasi) = 0 Then RekapTotal_PotonganHutangKoperasi = StripKosong
        If AmbilAngka(RekapTotal_PotonganHutangPPhPasal21Rutin) = 0 Then RekapTotal_PotonganHutangPPhPasal21Rutin = StripKosong
        If AmbilAngka(RekapTotal_PotonganHutangPPhPasal21Pesangon) = 0 Then RekapTotal_PotonganHutangPPhPasal21Pesangon = StripKosong
        If AmbilAngka(RekapTotal_PotonganHutangSerikat) = 0 Then RekapTotal_PotonganHutangSerikat = StripKosong
        If AmbilAngka(RekapTotal_PotonganKasbonKaryawan) = 0 Then RekapTotal_PotonganKasbonKaryawan = StripKosong
        If AmbilAngka(RekapTotal_PotonganLainnya) = 0 Then RekapTotal_PotonganLainnya = StripKosong
        If AmbilAngka(RekapTotal_JumlahPotongan) = 0 Then RekapTotal_JumlahPotongan = StripKosong
        If AmbilAngka(RekapTotal_JumlahGajiDibayarkan) = 0 Then RekapTotal_JumlahGajiDibayarkan = StripKosong
        If AmbilAngka(RekapTotal_PPhDitanggungRutin) = 0 Then RekapTotal_PPhDitanggungRutin = StripKosong
        If AmbilAngka(RekapTotal_PPhDitanggungPesangon) = 0 Then RekapTotal_PPhDitanggungPesangon = StripKosong
        If AmbilAngka(RekapTotal_JumlahPembayaran) = 0 Then RekapTotal_JumlahPembayaran = StripKosong
        If AmbilAngka(RekapTotal_SisaPembayaran) = 0 Then RekapTotal_SisaPembayaran = StripKosong

        If DataTabelUtama.RowCount > 0 Then
            If JenisTampilan <> JenisTampilan_ALL Then DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Kosongan, Kosongan, "T O T A L", Kosongan, "T O T A L",
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
                                    RekapTotal_JumlahGajiKotor,
                                    RekapTotal_PotonganHutangBpjsKesehatan, RekapTotal_PotonganHutangBpjsKetenagakerjaan, RekapTotal_PotonganHutangKoperasi, RekapTotal_PotonganHutangPPhPasal21Rutin, RekapTotal_PotonganHutangPPhPasal21Pesangon, RekapTotal_PotonganHutangSerikat, RekapTotal_PotonganKasbonKaryawan, RekapTotal_PotonganLainnya, RekapTotal_JumlahPotongan,
                                    RekapTotal_JumlahGajiDibayarkan, RekapTotal_PPhDitanggungRutin, RekapTotal_PPhDitanggungPesangon, RekapTotal_JumlahPembayaran, RekapTotal_SisaPembayaran, Kosongan, Kosongan)
        End If
        BersihkanSeleksi()
    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        btn_LihatJurnal.Enabled = False
        grb_Pembayaran.Visible = False
        dgv_DetailBayar.Rows.Clear()
        NomorJV_Pembayaran_Terseleksi = 0
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
        DataTabelUtama.Columns("Bulan_").Visible = False
        DataTabelUtama.Columns("Tanggal_Transaksi").Visible = True
        DataTabelUtama.Columns("Jumlah_Pembayaran").Visible = False
        DataTabelUtama.Columns("Sisa_Pembayaran").Visible = False
        DataTabelUtama.Columns("Keterangan_").Visible = True
    End Sub

    Sub VisibilitasObjek_REKAP()
        DataTabelUtama.Columns("Bulan_").Visible = True
        DataTabelUtama.Columns("Tanggal_Transaksi").Visible = False
        DataTabelUtama.Columns("Jumlah_Pembayaran").Visible = True
        DataTabelUtama.Columns("Sisa_Pembayaran").Visible = True
        DataTabelUtama.Columns("Keterangan_").Visible = False
    End Sub

    Sub VisibilitasKolom()

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Gaji_Bagian_Produksi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Gaji_Bagian_Produksi").Visible = False
            DataTabelUtama.Columns("Gaji_Bagian_Produksi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Gaji_Bagian_Produksi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiProduksi2 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Gaji_Bagian_Produksi_2").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Gaji_Bagian_Produksi_2").Visible = False
            DataTabelUtama.Columns("Gaji_Bagian_Produksi_2").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Gaji_Bagian_Produksi_2").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiProduksi3 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Gaji_Bagian_Produksi_3").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Gaji_Bagian_Produksi_3").Visible = False
            DataTabelUtama.Columns("Gaji_Bagian_Produksi_3").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Gaji_Bagian_Produksi_3").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiProduksi4 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Gaji_Bagian_Produksi_4").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Gaji_Bagian_Produksi_4").Visible = False
            DataTabelUtama.Columns("Gaji_Bagian_Produksi_4").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Gaji_Bagian_Produksi_4").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaThrBonusProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("THR_Bonus_Produksi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("THR_Bonus_Produksi").Visible = False
            DataTabelUtama.Columns("THR_Bonus_Produksi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("THR_Bonus_Produksi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaTunjanganPPh21Produksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Tunjangan_PPh_21_Produksi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Tunjangan_PPh_21_Produksi").Visible = False
            DataTabelUtama.Columns("Tunjangan_PPh_21_Produksi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Tunjangan_PPh_21_Produksi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsTkJkkJkmProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("BPJS_TK_JKK_JKM_Produksi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("BPJS_TK_JKK_JKM_Produksi").Visible = False
            DataTabelUtama.Columns("BPJS_TK_JKK_JKM_Produksi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("BPJS_TK_JKK_JKM_Produksi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsTkJhtIpProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("BPJS_TK_JHT_IP_Produksi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("BPJS_TK_JHT_IP_Produksi").Visible = False
            DataTabelUtama.Columns("BPJS_TK_JHT_IP_Produksi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("BPJS_TK_JHT_IP_Produksi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsKesehatanProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("BPJS_Kesehatan_Produksi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("BPJS_Kesehatan_Produksi").Visible = False
            DataTabelUtama.Columns("BPJS_Kesehatan_Produksi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("BPJS_Kesehatan_Produksi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaAsuransiKaryawanProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Asuransi_Karyawan_Produksi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Asuransi_Karyawan_Produksi").Visible = False
            DataTabelUtama.Columns("Asuransi_Karyawan_Produksi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Asuransi_Karyawan_Produksi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaPesangonKaryawanProduksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Pesangon_Karyawan_Produksi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Pesangon_Karyawan_Produksi").Visible = False
            DataTabelUtama.Columns("Pesangon_Karyawan_Produksi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Pesangon_Karyawan_Produksi").Visible = False
        End If

        ''Visibilitas Jumlah Gaji Bagian Produksi :
        'If DataTabelUtama.Columns("Gaji_Bagian_Produksi_2").Visible = True _
        '    And DataTabelUtama.Columns("Gaji_Bagian_Produksi_3").Visible = True _
        '    And DataTabelUtama.Columns("Gaji_Bagian_Produksi_4").Visible = True _
        '    And DataTabelUtama.Columns("THR_Bonus_Produksi").Visible = True _
        '    Then
        '    DataTabelUtama.Columns("Jumlah_Gaji_Bagian_Produksi").Visible = True
        'Else
        '    DataTabelUtama.Columns("Jumlah_Gaji_Bagian_Produksi").Visible = False
        'End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Gaji_Bagian_Administrasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Gaji_Bagian_Administrasi").Visible = False
            DataTabelUtama.Columns("Gaji_Bagian_Administrasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Gaji_Bagian_Administrasi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiAdministrasi2 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Gaji_Bagian_Administrasi_2").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Gaji_Bagian_Administrasi_2").Visible = False
            DataTabelUtama.Columns("Gaji_Bagian_Administrasi_2").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Gaji_Bagian_Administrasi_2").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiAdministrasi3 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Gaji_Bagian_Administrasi_3").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Gaji_Bagian_Administrasi_3").Visible = False
            DataTabelUtama.Columns("Gaji_Bagian_Administrasi_3").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Gaji_Bagian_Administrasi_3").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaGajiAdministrasi4 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Gaji_Bagian_Administrasi_4").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Gaji_Bagian_Administrasi_4").Visible = False
            DataTabelUtama.Columns("Gaji_Bagian_Administrasi_4").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Gaji_Bagian_Administrasi_4").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaThrBonusAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("THR_Bonus_Administrasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("THR_Bonus_Administrasi").Visible = False
            DataTabelUtama.Columns("THR_Bonus_Administrasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("THR_Bonus_Administrasi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaTunjanganPPh21Administrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Tunjangan_PPh_21_Administrasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Tunjangan_PPh_21_Administrasi").Visible = False
            DataTabelUtama.Columns("Tunjangan_PPh_21_Administrasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Tunjangan_PPh_21_Administrasi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsTkJkkJkmAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("BPJS_TK_JKK_JKM_Administrasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("BPJS_TK_JKK_JKM_Administrasi").Visible = False
            DataTabelUtama.Columns("BPJS_TK_JKK_JKM_Administrasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("BPJS_TK_JKK_JKM_Administrasi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsTkJhtIpAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("BPJS_TK_JHT_IP_Administrasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("BPJS_TK_JHT_IP_Administrasi").Visible = False
            DataTabelUtama.Columns("BPJS_TK_JHT_IP_Administrasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("BPJS_TK_JHT_IP_Administrasi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaBpjsKesehatanAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("BPJS_Kesehatan_Administrasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("BPJS_Kesehatan_Administrasi").Visible = False
            DataTabelUtama.Columns("BPJS_Kesehatan_Administrasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("BPJS_Kesehatan_Administrasi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaAsuransiKaryawanAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Asuransi_Karyawan_Administrasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Asuransi_Karyawan_Administrasi").Visible = False
            DataTabelUtama.Columns("Asuransi_Karyawan_Administrasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Asuransi_Karyawan_Administrasi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaPesangonKaryawanAdministrasi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Pesangon_Karyawan_Administrasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Pesangon_Karyawan_Administrasi").Visible = False
            DataTabelUtama.Columns("Pesangon_Karyawan_Administrasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Pesangon_Karyawan_Administrasi").Visible = False
        End If

        ''Visibilitas Jumlah Gaji Bagian Administrasi :
        'If DataTabelUtama.Columns("Gaji_Bagian_Administrasi_2").Visible = True _
        '    And DataTabelUtama.Columns("Gaji_Bagian_Administrasi_3").Visible = True _
        '    And DataTabelUtama.Columns("Gaji_Bagian_Administrasi_4").Visible = True _
        '    And DataTabelUtama.Columns("THR_Bonus_Administrasi").Visible = True _
        '    Then
        '    DataTabelUtama.Columns("Jumlah_Gaji_Bagian_Administrasi").Visible = True
        'Else
        '    DataTabelUtama.Columns("Jumlah_Gaji_Bagian_Administrasi").Visible = False
        'End If

        'cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_BiayaPPhPasal21 & "' ", KoneksiDatabaseGeneral)
        'dr = cmd.ExecuteReader
        'dr.Read()
        'If dr.HasRows Then
        '    If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Biaya_PPh_Pasal_21").Visible = True
        '    If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Biaya_PPh_Pasal_21").Visible = False
        '    DataTabelUtama.Columns("Biaya_PPh_Pasal_21").HeaderText = dr.Item("Nama_Akun")
        'Else
        '    DataTabelUtama.Columns("Biaya_PPh_Pasal_21").Visible = False
        'End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangBpjsKesehatan & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Potongan_Hutang_BPJS_Kesehatan").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Potongan_Hutang_BPJS_Kesehatan").Visible = False
            DataTabelUtama.Columns("Potongan_Hutang_BPJS_Kesehatan").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Potongan_Hutang_BPJS_Kesehatan").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangBpjsKetenagakerjaan & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Potongan_Hutang_BPJS_Ketenagakerjaan").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Potongan_Hutang_BPJS_Ketenagakerjaan").Visible = False
            DataTabelUtama.Columns("Potongan_Hutang_BPJS_Ketenagakerjaan").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Potongan_Hutang_BPJS_Ketenagakerjaan").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangKoperasiKaryawan & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Potongan_Hutang_Koperasi").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Potongan_Hutang_Koperasi").Visible = False
            DataTabelUtama.Columns("Potongan_Hutang_Koperasi").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Potongan_Hutang_Koperasi").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangPPhPasal21 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Potongan_Hutang_PPh_Pasal_21_Rutin").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Potongan_Hutang_PPh_Pasal_21_Rutin").Visible = False
            'DataTabelUtama.Columns("Potongan_Hutang_PPh_Pasal_21_Rutin").HeaderText = dr.Item("Nama_Akun") '(ga pake nama akun).
        Else
            DataTabelUtama.Columns("Potongan_Hutang_PPh_Pasal_21_Rutin").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangPPhPasal21_401 & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Potongan_Hutang_PPh_Pasal_21_Pesangon").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Potongan_Hutang_PPh_Pasal_21_Pesangon").Visible = False
            'DataTabelUtama.Columns("Potongan_Hutang_PPh_Pasal_21_Pesangon").HeaderText = dr.Item("Nama_Akun") '(ga pake nama akun).
        Else
            DataTabelUtama.Columns("Potongan_Hutang_PPh_Pasal_21_Pesangon").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangSerikat & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Potongan_Hutang_Serikat").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Potongan_Hutang_Serikat").Visible = False
            DataTabelUtama.Columns("Potongan_Hutang_Serikat").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Potongan_Hutang_Serikat").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_PiutangKaryawan & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Potongan_Kasbon_Karyawan").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Potongan_Kasbon_Karyawan").Visible = False
            DataTabelUtama.Columns("Potongan_Kasbon_Karyawan").HeaderText = dr.Item("Nama_Akun")
        Else
            DataTabelUtama.Columns("Potongan_Kasbon_Karyawan").Visible = False
        End If

        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangLancarLainnya & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Visibilitas") = "YA" Then DataTabelUtama.Columns("Potongan_Lainnya").Visible = True
            If dr.Item("Visibilitas") = "TIDAK" Then DataTabelUtama.Columns("Potongan_Lainnya").Visible = False
            DataTabelUtama.Columns("Potongan_Lainnya").HeaderText = "Potongan Lainnya" 'Tidak mengambil Nama Akun (Pengecualian).
        Else
            DataTabelUtama.Columns("Potongan_Lainnya").Visible = False
        End If

        AksesDatabase_General(Tutup)

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_Bulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Bulan.SelectedIndexChanged
    End Sub
    Private Sub cmb_Bulan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Bulan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_Bulan_TextChanged(sender As Object, e As EventArgs) Handles cmb_Bulan.TextChanged
        Bulan_Combo = cmb_Bulan.Text
        NomorBulan = KonversiBulanKeAngka(Bulan_Combo)

        If ProsesLoadingForm = False Then
            Select Case Bulan_Combo
                Case Bulan_All
                    Sub_JenisTampilan_ALL()
                Case Bulan_Rekap
                    Sub_JenisTampilan_REKAP()
                Case Else
                    Sub_JenisTampilan_DETAIL()
            End Select
        End If
    End Sub

    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        If SisaPembayaran_Terseleksi <= 0 Then
            MsgBox("Gaji Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
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

    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        frm_InputPembayaranGaji.ResetForm()
        frm_InputPembayaranGaji.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranGaji.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        frm_InputPembayaranGaji.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (BarisBayar_Terseleksi + 1).ToString
        frm_InputPembayaranGaji.NomorJVBayar = NomorJV_Pembayaran_Terseleksi
        frm_InputPembayaranGaji.NomorIdBayar = NomorIdPembayaran_Terseleksi
        frm_InputPembayaranGaji.NPPG = Referensi_Terseleksi
        IsiValueForm_InputPembayaranGaji()
        Dim BarisKe = 0
        Dim HitungBayar = 0
        Do While BarisKe < BarisBayar_Terseleksi
            HitungBayar += AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisKe).Value)
            BarisKe += 1
        Loop
        frm_InputPembayaranGaji.txt_JumlahGajiDibayar.Text = HitungBayar
        frm_InputPembayaranGaji.txt_SisaPembayaran.Text = JumlahGajiDibayarkan_Terseleksi - HitungBayar
        frm_InputPembayaranGaji.txt_JumlahBayar.Text = NominalBayar
        frm_InputPembayaranGaji.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        frm_InputPembayaranGaji.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        'Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        frm_InputPembayaranGaji.ShowDialog()

    End Sub

    Sub IsiValueForm_InputPembayaranGaji()

        frm_InputPembayaranGaji.txt_Bulan.Text = Bulan_Terseleksi
        frm_InputPembayaranGaji.txt_JumlahGaji.Text = JumlahGajiDibayarkan_Terseleksi

    End Sub

    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris &
                                  "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PembayaranGaji :
        cmd = New OdbcCommand(" DELETE FROM tbl_PembayaranGaji WHERE Nomor_ID = '" & NomorIdPembayaran_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        'Hapus Data di tbl_Transaksi (Jurnal) :
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_Pembayaran_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        TampilkanData()

        MsgBox("Data terpilih BERHASIL dihapus.")

    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Gaji_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Gaji_Terseleksi)
        ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            MsgBox("Data terpilih BELUM masuk JURNAL.")
            Return
        End If
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        frm_InputGaji.ResetForm()
        frm_InputGaji.FungsiForm = FungsiForm_TAMBAH
        frm_InputGaji.JalurMasuk = Halaman_BUKUPENGAWASANGAJI
        frm_InputGaji.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        If NomorID_Terseleksi = 0 Then Return

        frm_InputGaji.ResetForm()

        If PotonganHutangPPhPasal21Rutin_Terseleksi > 0 Then frm_InputGaji.PilihanPPh_Rutin = frm_InputGaji.PilihanPPh_PPhDipotongDitunjang
        If PPhDitanggungRutin_Terseleksi > 0 Then frm_InputGaji.PilihanPPh_Rutin = frm_InputGaji.PilihanPPh_PPhDitanggung

        If PotonganHutangPPhPasal21Pesangon_Terseleksi > 0 Then frm_InputGaji.PilihanPPh_Pesangon = frm_InputGaji.PilihanPPh_PPhDipotongDitunjang
        If PPhDitanggungPesangon_Terseleksi > 0 Then frm_InputGaji.PilihanPPh_Pesangon = frm_InputGaji.PilihanPPh_PPhDitanggung

        If PotonganHutangPPhPasal21Pesangon_Terseleksi > 0 Or PPhDitanggungPesangon_Terseleksi > 0 Then
            frm_InputGaji.AdaPPhPesangon = True
        Else
            frm_InputGaji.AdaPPhPesangon = False
        End If

        frm_InputGaji.FungsiForm = FungsiForm_EDIT
        frm_InputGaji.JalurMasuk = Halaman_BUKUPENGAWASANGAJI
        frm_InputGaji.NomorID = NomorID_Terseleksi
        frm_InputGaji.cmb_Bulan.Text = Bulan_Terseleksi
        frm_InputGaji.dtp_TanggalTransaksi.Value = TanggalTransaksi_Terseleksi

        frm_InputGaji.txt_GajiBagianProduksi.Text = GajiBagianProduksi_Terseleksi
        frm_InputGaji.txt_GajiBagianProduksi2.Text = GajiBagianProduksi2_Terseleksi
        frm_InputGaji.txt_GajiBagianProduksi3.Text = GajiBagianProduksi3_Terseleksi
        frm_InputGaji.txt_GajiBagianProduksi4.Text = GajiBagianProduksi4_Terseleksi
        frm_InputGaji.txt_ThrBonusProduksi.Text = ThrBonusProduksi_Terseleksi
        frm_InputGaji.txt_TunjanganPPh21Produksi.Text = TunjanganPPh21Produksi_Terseleksi
        frm_InputGaji.txt_BpjsTkJkkJkmProduksi.Text = BpjsTkJkkJkmProduksi_Terseleksi
        frm_InputGaji.txt_BpjsTkJhtIpProduksi.Text = BpjsTkJhtIpProduksi_Terseleksi
        frm_InputGaji.txt_BpjsKesehatanProduksi.Text = BpjsKesehatanProduksi_Terseleksi
        frm_InputGaji.txt_AsuransiKaryawanProduksi.Text = AsuransiKaryawanProduksi_Terseleksi
        frm_InputGaji.txt_PesangonKaryawanProduksi.Text = PesangonKaryawanProduksi_Terseleksi
        'frm_InputGaji.txt_JumlahGajiBagianProduksi.Text = JumlahGajiBagianProduksi_Terseleksi
        frm_InputGaji.txt_BpjsTkJhtIpProduksiDibayarKaryawan.Text = BpjsTkJhtIpProduksi_DibayarKaryawan_Terseleksi

        frm_InputGaji.txt_GajiBagianAdministrasi.Text = GajiBagianAdministrasi_Terseleksi
        frm_InputGaji.txt_GajiBagianAdministrasi2.Text = GajiBagianAdministrasi2_Terseleksi
        frm_InputGaji.txt_GajiBagianAdministrasi3.Text = GajiBagianAdministrasi3_Terseleksi
        frm_InputGaji.txt_GajiBagianAdministrasi4.Text = GajiBagianAdministrasi4_Terseleksi
        frm_InputGaji.txt_ThrBonusAdministrasi.Text = ThrBonusAdministrasi_Terseleksi
        frm_InputGaji.txt_TunjanganPPh21Administrasi.Text = TunjanganPPh21Administrasi_Terseleksi
        frm_InputGaji.txt_BpjsTkJkkJkmAdministrasi.Text = BpjsTkJkkJkmAdministrasi_Terseleksi
        frm_InputGaji.txt_BpjsTkJhtIpAdministrasi.Text = BpjsTkJhtIpAdministrasi_Terseleksi
        frm_InputGaji.txt_BPJSKesehatanAdministrasi.Text = BpjsKesehatanAdministrasi_Terseleksi
        frm_InputGaji.txt_AsuransiKaryawanAdministrasi.Text = AsuransiKaryawanAdministrasi_Terseleksi
        frm_InputGaji.txt_PesangonKaryawanAdministrasi.Text = PesangonKaryawanAdministrasi_Terseleksi
        'frm_InputGaji.txt_JumlahGajiBagianAdministrasi.Text = JumlahGajiBagianAdministrasi_Terseleksi
        frm_InputGaji.txt_BpjsTkJhtIpAdministrasiDibayarKaryawan.Text = BpjsTkJhtIpAdministrasi_DibayarKaryawan_Terseleksi

        'frm_InputGaji.txt_JumlahGajiKotor.Text = JumlahGajiKotor_Terseleksi
        frm_InputGaji.txt_PotonganHutangBpjsKesehatan.Text = PotonganHutangBpjsKesehatan_Terseleksi
        frm_InputGaji.txt_PotonganHutangBpjsKetenagakerjaan.Text = PotonganHutangBpjsKetenagakerjaan_Terseleksi
        frm_InputGaji.txt_PotonganHutangKoperasi.Text = PotonganHutangKoperasi_Terseleksi
        frm_InputGaji.txt_PotonganHutangPPhPasal21Rutin.Text = PotonganHutangPPhPasal21Rutin_Terseleksi
        frm_InputGaji.txt_PotonganHutangPPhPasal21Pesangon.Text = PotonganHutangPPhPasal21Pesangon_Terseleksi
        frm_InputGaji.txt_PotonganHutangSerikat.Text = PotonganHutangSerikat_Terseleksi
        frm_InputGaji.txt_PotonganKasbonKaryawan.Text = PotonganKasbonKaryawan_Terseleksi
        frm_InputGaji.txt_PotonganLainnya.Text = PotonganLainnya_Terseleksi
        'frm_InputGaji.txt_JumlahPotongan.Text = JumlahPotongan_Terseleksi
        'frm_InputGaji.txt_JumlahGajiDibayarkan.Text = JumlahGajiDibayarkan_Terseleksi
        frm_InputGaji.txt_PPhDitanggungRutin.Text = PPhDitanggungRutin_Terseleksi
        frm_InputGaji.txt_PPhDitanggungPesangon.Text = PPhDitanggungPesangon_Terseleksi
        frm_InputGaji.txt_Keterangan.Text = Keterangan_Terseleksi
        frm_InputGaji.NomorJVGaji = NomorJV_Gaji_Terseleksi
        frm_InputGaji.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris & "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PengawasanGaji :
        cmd = New OdbcCommand(" DELETE FROM tbl_PengawasanGaji WHERE Nomor_ID = '" & NomorID_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        'Hapus Data di tbl_PembayaranGaji :
        cmd = New OdbcCommand(" DELETE FROM tbl_PembayaranGaji WHERE Bulan = '" & Bulan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        'Hapus Data di tbl_Transaksi (Jurnal) :
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_Gaji_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        TampilkanData()
        If usc_BukuPengawasanHutangPPhPasal21.StatusAktif Then usc_BukuPengawasanHutangPPhPasal21.TampilkanData()

        MsgBox("Data terpilih BERHASIL dihapus.")

    End Sub

    Private Sub dgv_DetailBayar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellContentClick
    End Sub
    Private Sub dgv_DetailBayar_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_DetailBayar.ColumnHeaderMouseClick
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        btn_Edit.Enabled = False
        btn_HapusPembayaran.Enabled = False
        btn_EditPembayaran.Enabled = False
        btn_LihatJurnal.Enabled = False
        NomorJV_Gaji_Terseleksi = 0
    End Sub
    Private Sub dgv_DetailBayar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellClick
        Try
            BarisBayar_Terseleksi = dgv_DetailBayar.CurrentRow.Index
        Catch ex As Exception
            Return
        End Try
        NomorIdPembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_ID_Bayar", BarisBayar_Terseleksi).Value
        NomorJV_Pembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_JV_Bayar", BarisBayar_Terseleksi).Value
        Referensi_Terseleksi = dgv_DetailBayar.Item("Referensi_", BarisBayar_Terseleksi).Value
        NomorJV_Gaji_Terseleksi = 0
        If BarisBayar_Terseleksi >= 0 Then
            btn_HapusPembayaran.Enabled = True
            btn_EditPembayaran.Enabled = True
            btn_LihatJurnal.Enabled = True
        Else
            btn_HapusPembayaran.Enabled = False
            btn_EditPembayaran.Enabled = False
            btn_LihatJurnal.Enabled = False
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.Enabled = False
        If Referensi_Terseleksi = Kosongan Then
            btn_EditPembayaran.Enabled = False
            btn_HapusPembayaran.Enabled = False
        End If
    End Sub

    Sub TampilkanDataPembayaran()

        grb_Pembayaran.Visible = True

        dgv_DetailBayar.Rows.Clear()

        'AksesDatabase_Transaksi(Buka)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranGaji " &
        '                      " WHERE Bulan = '" & Bulan_Terseleksi & "' " &
        '                      " ORDER BY Nomor_ID", KoneksiDatabaseTransaksi)
        'dr = cmd.ExecuteReader
        'Do While dr.Read
        '    Dim NomorIdBayar = dr.Item("Nomor_ID")
        '    Dim TanggalBayar = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Bayar"), 10)
        '    Dim Referensi = dr.Item("NPPG")
        '    Dim JumlahBayar = dr.Item("Jumlah_Bayar")
        '    Dim KeteranganBayar = dr.Item("Keterangan")
        '    Dim NomorJV_Pembayaran = dr.Item("Nomor_JV")
        '    dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJV_Pembayaran)
        'Loop
        'AksesDatabase_Transaksi(Tutup)

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP      = '" & NomorBPHG_Terseleksi & "' " &
                              " AND Status_Invoice  = '" & Status_Dibayar & "' " &
                              " ORDER BY Nomor_ID", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            Dim Referensi = dr.Item("Nomor_KK")
            Dim JumlahBayar = dr.Item("Jumlah_Bayar")
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJV_Pembayaran = dr.Item("Nomor_JV")
            dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJV_Pembayaran)
        Loop
        AksesDatabase_Transaksi(Tutup)

        dgv_DetailBayar.ClearSelection()
        JumlahBarisBayar = dgv_DetailBayar.RowCount
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_ID", Baris_Terseleksi).Value)
        Bulan_Terseleksi = DataTabelUtama.Item("Bulan_", Baris_Terseleksi).Value
        'Dim SusurBaris = Baris_Terseleksi
        'Do While Bulan_Terseleksi = Kosongan
        '    SusurBaris -= 1
        '    If SusurBaris < 0 Then Exit Do
        '    Bulan_Terseleksi = DataTabelUtama.Item("Bulan_", SusurBaris).Value
        'Loop
        NomorBPHG_Terseleksi = DataTabelUtama.Item("Nomor_BPHG", Baris_Terseleksi).Value
        TanggalTransaksi_Terseleksi = Microsoft.VisualBasic.Left(DataTabelUtama.Item("Tanggal_Transaksi", Baris_Terseleksi).Value, 10)
        GajiBagianProduksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Gaji_Bagian_Produksi", Baris_Terseleksi).Value)
        GajiBagianProduksi2_Terseleksi = AmbilAngka(DataTabelUtama.Item("Gaji_Bagian_Produksi_2", Baris_Terseleksi).Value)
        GajiBagianProduksi3_Terseleksi = AmbilAngka(DataTabelUtama.Item("Gaji_Bagian_Produksi_3", Baris_Terseleksi).Value)
        GajiBagianProduksi4_Terseleksi = AmbilAngka(DataTabelUtama.Item("Gaji_Bagian_Produksi_4", Baris_Terseleksi).Value)
        ThrBonusProduksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("THR_Bonus_Produksi", Baris_Terseleksi).Value)
        TunjanganPPh21Produksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tunjangan_PPh_21_Produksi", Baris_Terseleksi).Value)
        BpjsTkJkkJkmProduksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("BPJS_TK_JKK_JKM_Produksi", Baris_Terseleksi).Value)
        BpjsTkJhtIpProduksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("BPJS_TK_JHT_IP_Produksi", Baris_Terseleksi).Value)
        BpjsKesehatanProduksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("BPJS_Kesehatan_Produksi", Baris_Terseleksi).Value)
        AsuransiKaryawanProduksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Asuransi_Karyawan_Produksi", Baris_Terseleksi).Value)
        PesangonKaryawanProduksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Pesangon_Karyawan_Produksi", Baris_Terseleksi).Value)
        JumlahGajiBagianProduksi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Gaji_Bagian_Produksi", Baris_Terseleksi).Value)
        BpjsTkJhtIpProduksi_DibayarKaryawan_Terseleksi = AmbilAngka(DataTabelUtama.Item("BPJS_TK_JHT_IP_Produksi_Dibayar_Karyawan", Baris_Terseleksi).Value)

        GajiBagianAdministrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Gaji_Bagian_Administrasi", Baris_Terseleksi).Value)
        GajiBagianAdministrasi2_Terseleksi = AmbilAngka(DataTabelUtama.Item("Gaji_Bagian_Administrasi_2", Baris_Terseleksi).Value)
        GajiBagianAdministrasi3_Terseleksi = AmbilAngka(DataTabelUtama.Item("Gaji_Bagian_Administrasi_3", Baris_Terseleksi).Value)
        GajiBagianAdministrasi4_Terseleksi = AmbilAngka(DataTabelUtama.Item("Gaji_Bagian_Administrasi_4", Baris_Terseleksi).Value)
        ThrBonusAdministrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("THR_Bonus_Administrasi", Baris_Terseleksi).Value)
        TunjanganPPh21Administrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tunjangan_PPh_21_Administrasi", Baris_Terseleksi).Value)
        BpjsTkJkkJkmAdministrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("BPJS_TK_JKK_JKM_Administrasi", Baris_Terseleksi).Value)
        BpjsTkJhtIpAdministrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("BPJS_TK_JHT_IP_Administrasi", Baris_Terseleksi).Value)
        BpjsKesehatanAdministrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("BPJS_Kesehatan_Administrasi", Baris_Terseleksi).Value)
        AsuransiKaryawanAdministrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Asuransi_Karyawan_Administrasi", Baris_Terseleksi).Value)
        PesangonKaryawanAdministrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Pesangon_Karyawan_Administrasi", Baris_Terseleksi).Value)
        JumlahGajiBagianAdministrasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Gaji_Bagian_Administrasi", Baris_Terseleksi).Value)
        BpjsTkJhtIpAdministrasi_DibayarKaryawan_Terseleksi = AmbilAngka(DataTabelUtama.Item("BPJS_TK_JHT_IP_Administrasi_Dibayar_Karyawan", Baris_Terseleksi).Value)

        JumlahGajiKotor_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Gaji_Kotor", Baris_Terseleksi).Value)
        PotonganHutangBpjsKesehatan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Potongan_Hutang_BPJS_Kesehatan", Baris_Terseleksi).Value)
        PotonganHutangBpjsKetenagakerjaan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Potongan_Hutang_BPJS_Ketenagakerjaan", Baris_Terseleksi).Value)
        PotonganHutangKoperasi_Terseleksi = AmbilAngka(DataTabelUtama.Item("Potongan_Hutang_Koperasi", Baris_Terseleksi).Value)
        PotonganHutangPPhPasal21Rutin_Terseleksi = AmbilAngka(DataTabelUtama.Item("Potongan_Hutang_PPh_Pasal_21_Rutin", Baris_Terseleksi).Value)
        PotonganHutangPPhPasal21Pesangon_Terseleksi = AmbilAngka(DataTabelUtama.Item("Potongan_Hutang_PPh_Pasal_21_Pesangon", Baris_Terseleksi).Value)
        PotonganHutangSerikat_Terseleksi = AmbilAngka(DataTabelUtama.Item("Potongan_Hutang_Serikat", Baris_Terseleksi).Value)
        PotonganKasbonKaryawan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Potongan_Kasbon_Karyawan", Baris_Terseleksi).Value)
        PotonganLainnya_Terseleksi = AmbilAngka(DataTabelUtama.Item("Potongan_Lainnya", Baris_Terseleksi).Value)
        JumlahPotongan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Potongan", Baris_Terseleksi).Value)
        JumlahGajiDibayarkan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Gaji_Dibayarkan", Baris_Terseleksi).Value)
        PPhDitanggungRutin_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Ditanggung_Rutin", Baris_Terseleksi).Value)
        PPhDitanggungPesangon_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Ditanggung_Pesangon", Baris_Terseleksi).Value)
        NomorJV_Gaji_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", Baris_Terseleksi).Value

        If NomorID_Terseleksi > 0 Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If

        If NomorJV_Gaji_Terseleksi > 0 Then
            btn_LihatJurnal.Enabled = True
        Else
            btn_LihatJurnal.Enabled = False
        End If

        If JenisTampilan = JenisTampilan_REKAP Then

            TermasukHutangTahunIni_Terseleksi = True 'Ini nanti harus dilogikakan, jika ada kemungkinan pembayaran gaji untuk tahun sebelumnya.
            JumlahPembayaran_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Pembayaran", Baris_Terseleksi).Value)
            SisaPembayaran_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Pembayaran", Baris_Terseleksi).Value)

            TampilkanDataPembayaran()

            btn_Edit.Enabled = False

            If Baris_Terseleksi >= 12 Then BersihkanSeleksi()
            If JumlahGajiDibayarkan_Terseleksi = 0 Then BersihkanSeleksi()

        End If

    End Sub

    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        If JenisTampilan = JenisTampilan_DETAIL Or JenisTampilan = JenisTampilan_ALL Then
            btn_Edit_Click(sender, e)
        End If
        If JenisTampilan = JenisTampilan_REKAP And cmb_Bulan.Enabled = True Then
            cmb_Bulan.Text = DataTabelUtama.Item("Bulan_", Baris_Terseleksi).Value
        End If
    End Sub

End Class