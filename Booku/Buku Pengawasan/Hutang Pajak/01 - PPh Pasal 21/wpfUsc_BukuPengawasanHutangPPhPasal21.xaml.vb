Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_BukuPengawasanHutangPPhPasal21

    Public StatusAktif As Boolean = False

    Public JudulForm
    Public JenisPajak
    Public JenisTampilan
    Public JenisTampilan_ALL = "ALL"
    Public JenisTampilan_REKAP = "REKAP"
    Public JenisTampilan_DETAIL = "DETAIL"
    Public MasaPajak_All = "ALL"
    Public MasaPajak_Rekap = "REKAP"
    Public MasaPajak_Angka As Integer


    Dim JenisTahunBukuPajak
    Dim TahunHutangPajakTerlama
    Public TahunBukuSumberData

    Public NamaHalaman As String
    Public AwalanBP As String
    Public COAHutangPajak_100 As String
    Public COAHutangPajak_401 As String

    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim Bulan_Terseleksi
    Dim NomorBPHP_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim NamaJasa_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim NPWP_Terseleksi
    Dim DPP_Terseleksi
    Dim PPh_Terseleksi
    Dim PPh100_Terseleksi
    Dim PPh401_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahBayar100_Terseleksi
    Dim JumlahBayar401_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim SisaHutang100_Terseleksi
    Dim SisaHutang401_Terseleksi
    Dim SisaHutang_Terseleksi
    Dim KodeSetoran_Terseleksi
    Dim Keterangan_Terseleksi

    'Kolom Lapor :
    Dim TanggalLapor
    Dim NomorIDLapor
    Dim TWTL_Lapor
    Dim NP_Lapor
    Dim TanggalLapor_Terseleksi
    Dim NomorIDLapor_Terseleksi
    Dim NP_Lapor_Terseleksi


    Dim SisaHutang_SaatCutOff_100
    Dim SisaHutang_SaatCutOff_401

    Dim MasaPajak = Kosongan
    Dim TahunPajakSebelumIni

    'Variabel Tabel :
    Dim n_LoopingTampilan
    Dim Index_BarisTabel
    Dim NomorUrut
    Dim NomorID
    Dim NomorBulan
    Dim NomorBPHP
    Dim Bulan = Kosongan
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim JenisPembelian
    Dim NomorFakturPajak
    Dim NamaJasa
    Dim NPWP
    Dim KodeSupplier
    Dim NamaSupplier
    Dim DPP100JasaOP As Int64
    Dim DPP100Gaji As Int64
    Dim DPP100 As Int64
    Dim DPP401 As Int64
    Dim DPP As Int64
    Dim RekapPerBulanDPP100JasaOP As Int64
    Dim RekapPerBulanDPP100Gaji As Int64
    Dim RekapPerBulanDPP100 As Int64
    Dim RekapPerBulanDPP401 As Int64
    Dim RekapPerBulanDPP As Int64
    Dim PPh100JasaOP As Int64
    Dim PPh100Gaji As Int64
    Dim PPh100 As Int64
    Dim PPh401 As Int64
    Dim PPh As Int64
    Dim RekapPerBulanPPh100JasaOP As Int64
    Dim RekapPerBulanPPh100Gaji As Int64
    Dim RekapPerBulanPPh100 As Int64
    Dim RekapPerBulanPPh401 As Int64
    Dim RekapPerBulanPPh As Int64
    Dim JumlahTagihan As Int64
    Dim TanggalTransaksi
    Dim JumlahBayar100 As Int64
    Dim JumlahBayar401 As Int64
    Dim JumlahBayar As Int64
    Dim SisaHutang As Int64
    Dim JenisKodeSetoran
    Dim Keterangan

    Dim TotalDPP100JasaOP As Int64
    Dim TotalDPP100Gaji As Int64
    Dim TotalDPP100 As Int64
    Dim TotalDPP401 As Int64
    Dim TotalDPP As Int64
    Dim TotalTagihan100JasaOP As Int64
    Dim TotalTagihan100Gaji As Int64
    Dim TotalTagihan100 As Int64
    Dim TotalTagihan401 As Int64
    Dim TotalTagihan As Int64
    Dim TotalBayar100 As Int64
    Dim TotalBayar401 As Int64
    Dim TotalBayar As Int64
    Dim TotalSisaHutang As Int64

    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi
    Dim TahunPembayaran_Terseleksi

    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Dim NomorInvoice_Sebelumnya

    Dim KodeSetoran_UntukBayar
    Dim KodeSetoran_UntukTabel

    Dim SumberData
    Dim SumberData_SisaHutangPajak = "Sisa Hutang Pajak"

    Dim JumlahBarisDalamSatuBulan = 0


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StatusAktif = True

        lbl_JudulForm.Text = frm_BukuPengawasanHutangPPhPasal21.JudulForm
        PPh_.Header = "Jumlah" & Enter1Baris & JenisPajak
        Jumlah_Bayar_Pajak.Header = "Jumlah Bayar" & Enter1Baris & JenisPajak

        ProsesLoadingForm = True

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            VisibilitasTombolJurnal(False)
            VisibilitasTombolCRUD(True)
            VisibilitasTombolUpdateBayar(True)
            grb_InfoSaldo.Header = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            VisibilitasTombolJurnal(True)
            VisibilitasTombolCRUD(False)
            VisibilitasTombolUpdateBayar(False)
            grb_InfoSaldo.Header = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        End If

        KontenCombo_TahunPajak()
        KontenCombo_MasaPajak()

        Sub_JenisTampilan_REKAP()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_TahunPajak() 'Sengaja pakai Sub KontenCombo, untuk me-refresh List Tahun Pajak, barangkali ada update data untuk Tahun Pajak Terlama
        cmb_MasaPajak.SelectedValue = MasaPajak_Rekap
        EksekusiKode = True
        TampilkanData()
    End Sub


    Sub KontenCombo_MasaPajak()
        cmb_MasaPajak.Items.Clear()
        'cmb_MasaPajak.Items.Add(MasaPajak_All)     'Tidak ada Jenis Tampilan ALL pada BPH PPh Pasal 21, karena ada unsur Gaji, yang beda format tabelnya.
        cmb_MasaPajak.Items.Add(MasaPajak_Rekap)
        cmb_MasaPajak.Items.Add(Bulan_Januari)
        cmb_MasaPajak.Items.Add(Bulan_Februari)
        cmb_MasaPajak.Items.Add(Bulan_Maret)
        cmb_MasaPajak.Items.Add(Bulan_April)
        cmb_MasaPajak.Items.Add(Bulan_Mei)
        cmb_MasaPajak.Items.Add(Bulan_Juni)
        cmb_MasaPajak.Items.Add(Bulan_Juli)
        cmb_MasaPajak.Items.Add(Bulan_Agustus)
        cmb_MasaPajak.Items.Add(Bulan_September)
        cmb_MasaPajak.Items.Add(Bulan_Oktober)
        cmb_MasaPajak.Items.Add(Bulan_Nopember)
        cmb_MasaPajak.Items.Add(Bulan_Desember)
        cmb_MasaPajak.SelectedValue = MasaPajak_Rekap
    End Sub

    Sub KontenCombo_TahunPajak()

        TahunHutangPajakTerlama = AmbilTahunTerlama_SisaHutangPajak(JenisPajak)
        Dim ListTahunPajak = TahunBukuAktif

        cmb_TahunPajak.Items.Clear()
        TahunPajak = TahunBukuAktif
        Do While ListTahunPajak >= TahunHutangPajakTerlama
            cmb_TahunPajak.Items.Add(ListTahunPajak)
            ListTahunPajak -= 1
        Loop
        cmb_TahunPajak.SelectedValue = TahunPajak

    End Sub



    Sub TampilkanData()

        If EksekusiKode = False Then Return

        KetersediaanMenuHalaman(pnl_Halaman, False)
        VisibilitasInfoSaldo(False)

        'Judul Halaman :
        frm_BukuPengawasanHutangPPhPasal21.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        If MasaPajak = Kosongan Then Return

        KesesuaianJurnal_100 = True
        KesesuaianJurnal_401 = True

        'Style Tabel :
        datatabelUtama.Rows.Clear()
        DPP_100_Jasa_OP.Visibility = Visibility.Collapsed
        DPP_100_Gaji.Visibility = Visibility.Collapsed
        DPP_100.Visibility = Visibility.Collapsed
        DPP_401.Visibility = Visibility.Collapsed
        PPh_100_Jasa_OP.Visibility = Visibility.Collapsed
        PPh_100_Gaji.Visibility = Visibility.Collapsed
        PPh_100.Visibility = Visibility.Collapsed
        PPh_401.Visibility = Visibility.Collapsed
        Jumlah_Bayar_100.Visibility = Visibility.Collapsed
        Jumlah_Bayar_401.Visibility = Visibility.Collapsed

        'Data Tabel :
        NomorUrut = 0
        NomorID = 0 'Ini Jangan Dihapus. Ada kepentingan di balik ini.
        TanggalTransaksi = Kosongan
        NomorInvoice = Kosongan
        NomorFakturPajak = Kosongan
        NamaJasa = Kosongan
        NPWP = Kosongan
        KodeSupplier = Kosongan
        NamaSupplier = Kosongan
        DPP = 0
        Keterangan = Kosongan

        TotalDPP100JasaOP = 0
        TotalDPP100Gaji = 0
        TotalDPP100 = 0
        TotalDPP401 = 0
        TotalDPP = 0

        TotalTagihan100JasaOP = 0
        TotalTagihan100Gaji = 0
        TotalTagihan100 = 0
        TotalTagihan401 = 0
        TotalTagihan = 0

        TotalBayar100 = 0
        TotalBayar401 = 0
        TotalBayar = 0

        TotalSisaHutang = 0

        'TAMPILAN REKAP : ------------------------------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_REKAP Then

            SumberData = Kosongan

            Index_BarisTabel = 0
            NomorBulan = 0

            AksesDatabase_Transaksi(Buka)

            If StatusKoneksiDatabaseTransaksi = False Then Return

            Do While AmbilAngka(NomorBulan) < 12

                NomorBulan = AmbilAngka(NomorBulan) + 1
                Bulan = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBP & TahunPajak & "-" & NomorBulan.ToString

                RekapPerBulanDPP100JasaOP = 0
                RekapPerBulanDPP100Gaji = 0
                RekapPerBulanDPP100 = 0
                RekapPerBulanDPP401 = 0
                RekapPerBulanDPP = 0

                RekapPerBulanPPh100JasaOP = 0
                RekapPerBulanPPh100Gaji = 0
                RekapPerBulanPPh100 = 0
                RekapPerBulanPPh401 = 0
                RekapPerBulanPPh = 0

                JumlahBayar100 = 0
                JumlahBayar401 = 0
                JumlahBayar = 0
                SisaHutang = 0

                NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

                'Jika [ Tahun Buku LAMPAU ] :
                If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then

                    BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)

                    SumberData = SumberData_SisaHutangPajak

                    'PPh Pasal 21 dari Jasa OP :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                          " WHERE Jenis_Pajak                           = '" & JenisPajak & "' " &
                                          " AND Nama_Jasa                              <> '" & teks_Gaji & "' " &
                                          " AND Nama_Jasa                              <> '" & teks_Pesangon & "' " &
                                          " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        RekapPerBulanDPP100JasaOP += dr.Item("DPP")
                        RekapPerBulanPPh100JasaOP += dr.Item("Jumlah_Hutang")
                    Loop

                    'PPh Pasal 21 dari Pembayaran Gaji :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                          " WHERE Jenis_Pajak                           = '" & JenisPajak & "' " &
                                          " AND Nama_Jasa                               = '" & teks_Gaji & "' " &
                                          " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        RekapPerBulanDPP100Gaji += dr.Item("DPP")
                        RekapPerBulanPPh100Gaji += dr.Item("Jumlah_Hutang")
                    Loop

                    'PPh Pasal 21 dari Pembayaran Pesangon :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                          " WHERE Jenis_Pajak                           = '" & JenisPajak & "' " &
                                          " AND Nama_Jasa                               = '" & teks_Pesangon & "' " &
                                          " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        RekapPerBulanDPP401 += dr.Item("DPP")
                        RekapPerBulanPPh401 += dr.Item("Jumlah_Hutang")
                    Loop

                    TutupDatabaseTransaksi_Alternatif()

                End If

                'Jika [ Tahun Buku NORMAL ]   :
                If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then

                    BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)

                    'PPh Pasal 21 dari Pengluaran Bank-Cash, Baik Transaksi Tunai maupun Pembayaran Hutang :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                          " WHERE Jenis_Pajak       = '" & JenisPajak & "' " &
                                          " AND Status_Invoice      = '" & Status_Dibayar & "' " &
                                          " AND Jumlah_Bayar        > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        PPh100JasaOP = dr.Item("PPh_Terutang")
                        If PPh100JasaOP > 0 Then
                            SumberData = dr.Item("Peruntukan")
                            Dim NomorPembelian = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                            Dim cmdPEMB = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                                  " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                  " AND Jenis_Produk_Per_Item LIKE '%" & JenisProduk_Jasa & "%' ",
                                                  KoneksiDatabaseTransaksi_Alternatif)
                            Dim drPEMB = cmdPEMB.ExecuteReader
                            drPEMB.Read()
                            If drPEMB.HasRows Then
                                DPP = PPh100JasaOP / (drPEMB.Item("Tarif_PPh") / 100)
                                RekapPerBulanDPP100JasaOP += DPP
                                RekapPerBulanPPh100JasaOP += PPh100JasaOP
                            End If
                        End If
                    Loop

                    'PPh Pasal 21 dari Pengawasan Hutang Bank :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangBank " &
                                          " WHERE Biaya_PPh > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Pencairan, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        DPP = dr.Item("Jumlah_Pinjaman")
                        PPh = dr.Item("Biaya_PPh")
                        RekapPerBulanDPP100JasaOP += dr.Item("Jumlah_Pinjaman")
                        RekapPerBulanPPh100JasaOP += dr.Item("Biaya_PPh")
                    Loop

                    'PPh Pasal 21 dari Pengawasan Hutang Leasing :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangLeasing " &
                                          " WHERE Biaya_PPh > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Pencairan, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        DPP = dr.Item("Jumlah_Pinjaman")
                        PPh = dr.Item("Biaya_PPh")
                        RekapPerBulanDPP100JasaOP += dr.Item("Jumlah_Pinjaman")
                        RekapPerBulanPPh100JasaOP += dr.Item("Biaya_PPh")
                    Loop

                    'PPh Pasal 21 dari Pembayaran Gaji 
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanGaji " &
                                          " WHERE DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        RekapPerBulanPPh100Gaji += dr.Item("Potongan_Hutang_PPh_Pasal_21_Rutin")
                        RekapPerBulanPPh100Gaji += dr.Item("PPh_Ditanggung_Rutin")
                        RekapPerBulanPPh401 += dr.Item("Potongan_Hutang_PPh_Pasal_21_Pesangon")
                        RekapPerBulanPPh401 += dr.Item("PPh_Ditanggung_Pesangon")
                        RekapPerBulanDPP100Gaji += dr.Item("Jumlah_Gaji_Kotor")
                        RekapPerBulanDPP401 += (dr.Item("Pesangon_Karyawan_Produksi") + dr.Item("Pesangon_Karyawan_Administrasi"))
                    Loop

                    TutupDatabaseTransaksi_Alternatif()

                End If

                RekapPerBulanDPP100 = RekapPerBulanDPP100JasaOP + RekapPerBulanDPP100Gaji
                RekapPerBulanPPh100 = RekapPerBulanPPh100JasaOP + RekapPerBulanPPh100Gaji

                RekapPerBulanDPP401 = RekapPerBulanDPP401   '<-- Ini sebetulnya tidak perlu. Tapi ga apa-apa, untuk ketertiban coding
                RekapPerBulanPPh401 = RekapPerBulanPPh401   '<-- Ini sebetulnya tidak perlu. Tapi ga apa-apa, untuk ketertiban coding

                RekapPerBulanDPP = RekapPerBulanDPP100 + RekapPerBulanDPP401
                RekapPerBulanPPh = RekapPerBulanPPh100 + RekapPerBulanPPh401

                TotalDPP100JasaOP += RekapPerBulanDPP100JasaOP
                TotalDPP100Gaji += RekapPerBulanDPP100Gaji
                TotalDPP100 += RekapPerBulanDPP100
                TotalDPP401 += RekapPerBulanDPP401
                TotalDPP += RekapPerBulanDPP

                TotalTagihan100JasaOP += RekapPerBulanPPh100JasaOP
                TotalTagihan100Gaji += RekapPerBulanPPh100Gaji
                TotalTagihan100 += RekapPerBulanPPh100
                TotalTagihan401 += RekapPerBulanPPh401
                TotalTagihan += RekapPerBulanPPh

                'Data Pembayaran : ---------------------------------------------------------------------------------------
                If RekapPerBulanPPh > 0 Then
                    Dim TahunTelusurPembayaran = TahunPajak
                    Dim TahunSumberDataPembayaran = 0
                    Dim PencegahLoopingTahunPajakLampau = 0
                    Do While TahunTelusurPembayaran <= TahunBukuAktif
                        If TahunTelusurPembayaran <= TahunCutOff Then TahunSumberDataPembayaran = TahunCutOff
                        If TahunTelusurPembayaran > TahunCutOff Then TahunSumberDataPembayaran = TahunTelusurPembayaran
                        If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                            BukaDatabaseTransaksi_Alternatif(TahunSumberDataPembayaran)
                            cmd = New OdbcCommand(" SELECT Jumlah_Bayar, Kode_Setoran FROM tbl_BuktiPengeluaran " &
                                                  " WHERE Nomor_BP          = '" & NomorBPHP & "' " &
                                                  " AND Status_Invoice      = '" & Status_Dibayar & "' ",
                                                  KoneksiDatabaseTransaksi_Alternatif)
                            dr_ExecuteReader()
                            Do While dr.Read
                                JumlahBayar += dr.Item("Jumlah_Bayar")
                                Select Case dr.Item("Kode_Setoran")
                                    Case KodeSetoran_100
                                        JumlahBayar100 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_401
                                        JumlahBayar401 += dr.Item("Jumlah_Bayar")
                                End Select
                                If JumlahBayar >= RekapPerBulanPPh Then Exit Do
                            Loop
                            TutupDatabaseTransaksi_Alternatif()
                        End If
                        If JumlahBayar >= RekapPerBulanPPh Then Exit Do
                        PencegahLoopingTahunPajakLampau += 1
                        TahunTelusurPembayaran += 1
                    Loop
                End If
                'Penjelasan :
                'Algoritma ini berfungsi untuk menelusur jumlah pembayaran atas PPh bulan tertentu (nomor bulan)
                'dari suatu tahun pajak yang sedang ditampilkan, berdasarkan Kode Setoran masing-masing.
                'Data pembayaran yang ditampilkan adalah dimulai dari TahunPajak itu sendiri sampai TahunBukuAktif.
                'Misalkan, Tahun Buku Aktif-nya adalah 2023, sementara Data Pajak yang ditampilkan (TahunPajak) adalah 2022,
                'maka data pembayarannya ditelusuri dari tahun 2022 sampai tahun 2023.
                'Ini pentiung untuk mengetahui, berapa sisa hutang pajak pada suatu bulan di tahun tertentu.
                '---------------------------------------------------------------------------------------------------------

                TotalBayar100 += JumlahBayar100
                TotalBayar401 += JumlahBayar401
                TotalBayar += JumlahBayar

                SisaHutang = RekapPerBulanPPh - JumlahBayar
                TotalSisaHutang += SisaHutang

                TambahBaris()

            Loop

            AksesDatabase_Transaksi(Tutup)

            Baris_KetetapanPajak()

            'Baris TOTAL untuk Jenis Tampilan REKAP :
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(
                Kosongan, Kosongan, Kosongan, teks_TOTAL_,
                Kosongan, Kosongan, Kosongan, Kosongan,
                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                TotalDPP100JasaOP, TotalDPP100Gaji, TotalDPP100, TotalDPP401, TotalDPP,
                TotalTagihan100JasaOP, TotalTagihan100Gaji, TotalTagihan100, TotalTagihan401, TotalTagihan,
                TotalBayar100, TotalBayar401, TotalBayar, TotalSisaHutang,
                KodeSetoran_UntukTabel, Kosongan, Kosongan)

            If TotalDPP100JasaOP > 0 Then DPP_100_Jasa_OP.Visibility = Visibility.Visible
            If TotalDPP100Gaji > 0 Then DPP_100_Gaji.Visibility = Visibility.Visible
            If TotalDPP100 > 0 Then DPP_100.Visibility = Visibility.Visible
            If TotalDPP401 > 0 Then DPP_401.Visibility = Visibility.Visible
            If TotalDPP > 0 Then DPP_.Visibility = Visibility.Visible

            If TotalTagihan100JasaOP > 0 Then PPh_100_Jasa_OP.Visibility = Visibility.Visible
            If TotalTagihan100Gaji > 0 Then PPh_100_Gaji.Visibility = Visibility.Visible
            If TotalTagihan100 > 0 Then PPh_100.Visibility = Visibility.Visible
            If TotalTagihan401 > 0 Then PPh_401.Visibility = Visibility.Visible

            If TotalBayar100 > 0 Then Jumlah_Bayar_100.Visibility = Visibility.Visible
            If TotalBayar401 > 0 Then Jumlah_Bayar_401.Visibility = Visibility.Visible

            VisibilitasInfoSaldo(True)

        End If



        'TAMPILAN DETAIL : -----------------------------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_DETAIL Then

            SumberData = Kosongan

            NomorBulan = KonversiBulanKeNomor_String(MasaPajak)
            Bulan = MasaPajak

            RekapPerBulanDPP100JasaOP = 0
            RekapPerBulanDPP100Gaji = 0
            RekapPerBulanDPP100 = 0
            RekapPerBulanDPP401 = 0
            RekapPerBulanDPP = 0

            RekapPerBulanPPh100JasaOP = 0
            RekapPerBulanPPh100Gaji = 0
            RekapPerBulanPPh100 = 0
            RekapPerBulanPPh401 = 0
            RekapPerBulanPPh = 0

            BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)
            If StatusKoneksiDatabaseTransaksi_Alternatif = False Then Return

            'Jika Tahun Pajak = Tahun Buku LAMPAU :
            If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then
                SumberData = SumberData_SisaHutangPajak
                cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                      " WHERE Jenis_Pajak = '" & JenisPajak & "' AND " &
                                      " DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    NomorID = dr.Item("Nomor_ID")
                    TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
                    TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    NomorInvoice = dr.Item("Nomor_Invoice")
                    NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
                    NamaJasa = dr.Item("Nama_Jasa")
                    KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                    AmbilValue_NamaDanNPWPSupplier()
                    DPP = dr.Item("DPP")
                    PPh = dr.Item("Jumlah_Hutang")
                    AmbilValue_PerKodeSetoran()
                    Keterangan = PenghapusEnter(dr.Item("Keterangan"))
                    TambahBaris()
                Loop
            End If

            'Jika Tahun Pajak = Tahun Buku NORMAL :
            If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then

                'PPh Pasal 21 dari Pengluaran Bank-Cash, Baik Transaksi Tunai maupun Pembayaran Hutang :
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                      " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                      " AND Jumlah_Bayar                        > 0 " &
                                      " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    PPh = dr.Item("PPh_Terutang")
                    If PPh > 0 Then
                        SumberData = dr.Item("Peruntukan")
                        TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                        TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                        NomorInvoice = dr.Item("Nomor_Invoice")
                        KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                        AmbilValue_NamaDanNPWPSupplier()
                        Keterangan = PenghapusEnter(dr.Item("Catatan"))
                        Dim NomorPembelian = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                        Dim cmdPEMB = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                      " AND Jenis_Produk_Per_Item LIKE '%" & JenisProduk_Jasa & "%' ",
                                                      KoneksiDatabaseTransaksi_Alternatif)
                        Dim drPEMB = cmdPEMB.ExecuteReader
                        drPEMB.Read()
                        If drPEMB.HasRows Then
                            TanggalInvoice = TanggalFormatTampilan(drPEMB.Item("Tanggal_Invoice"))
                            NomorInvoice = drPEMB.Item("Nomor_Invoice")
                            NomorFakturPajak = drPEMB.Item("Nomor_Faktur_Pajak")
                            NamaJasa = drPEMB.Item("Nama_Produk")
                            DPP = PPh / (drPEMB.Item("Tarif_PPh") / 100)
                        End If
                        AmbilValue_PerKodeSetoran()
                        TambahBaris()
                    End If
                Loop

                'PPh Pasal 21 dari Pengawasan Hutang Bank :
                cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangBank " &
                                      " WHERE Biaya_PPh > 0 " &
                                      " AND DATE_FORMAT(Tanggal_Pencairan, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    NamaJasa = StripKosong
                    TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
                    TanggalInvoice = TanggalTransaksi
                    NomorInvoice = dr.Item("Nomor_Kontrak")
                    NomorFakturPajak = Kosongan
                    KodeSupplier = dr.Item("Kode_Kreditur")
                    AmbilValue_NamaDanNPWPSupplier()
                    RekapPerBulanDPP100JasaOP += dr.Item("Jumlah_Pinjaman")
                    RekapPerBulanPPh100JasaOP += dr.Item("Biaya_PPh")
                    KodeSetoran_UntukTabel = KodeSetoran_100
                    Keterangan = dr.Item("Keterangan")
                    TambahBaris()
                Loop

                'PPh Pasal 21 dari Pengawasan Hutang Leasing :
                cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangLeasing " &
                                      " WHERE Biaya_PPh > 0 " &
                                      " AND DATE_FORMAT(Tanggal_Pencairan, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    NamaJasa = StripKosong
                    TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
                    TanggalInvoice = TanggalTransaksi
                    NomorInvoice = dr.Item("Nomor_Kontrak")
                    NomorFakturPajak = Kosongan
                    KodeSupplier = dr.Item("Kode_Kreditur")
                    AmbilValue_NamaDanNPWPSupplier()
                    RekapPerBulanDPP100JasaOP += dr.Item("Jumlah_Pinjaman")
                    RekapPerBulanPPh100JasaOP += dr.Item("Biaya_PPh")
                    KodeSetoran_UntukTabel = KodeSetoran_100
                    Keterangan = dr.Item("Keterangan")
                    TambahBaris()
                Loop

                'PPh Pasal 21 dari Gaji tidak ditampilkan di sini. Jika User ingin melihat detail, ada tombol yang mengarahkan ke Buku Pengawasan Gaji.

            End If

            TutupDatabaseTransaksi_Alternatif()

            ''Urutkan berdasarkan Tanggal :
            'dataviewUtama.Sort = "Tanggal_Transaksi" '(Ini penting karena pengambilan Data bersumber pada 2 tabel (Data Pembelian Tunai, dan Data Pembayaran Hutang Usaha)

            'NomorUrut = 0
            'For Each row As DataRow In datatabelUtama.Rows
            '    NomorUrut += 1
            '    row("Nomor_Urut") = NomorUrut
            'Next

            'Baris TOTAL untuk Jenis Tampilan DETAIL :
            If datatabelUtama.Rows.Count > 0 Then
                datatabelUtama.Rows.Add()
                datatabelUtama.Rows.Add(
                    Kosongan, Kosongan, Kosongan, Kosongan,
                    Kosongan, Kosongan, Kosongan, Kosongan,
                    Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, teks_TOTAL_,
                    0, 0, 0, 0, RekapPerBulanDPP,
                    0, 0, 0, 0, RekapPerBulanPPh)
            End If

        End If


        'Kenapa Tidak ada Jenis Tampilan ALL..?
        'Karena konsep tampilan data PPh Pasal 21 berbeda dengan yang lainnya.
        'Dia ada sumber dari Pengawasan Gaji, yang struktur tabelnya tidak match dengan yang disini
        'Di Tampilan DETAIL pun dia tidak tampil. Tapi ada tombol untuk nge-link ke Buku Pengawasan Gaji.
        'Sementara untuk di tampilan REKAP, data Gaji tetap masuk.


        If JenisTampilan = JenisTampilan_REKAP Then
            'Hitung Saldo Akhir Saat Cut Off :
            SisaHutangPajak_SaatCutOff_Public(SisaHutang_SaatCutOff_100, AwalanBP, JenisPajak, KodeSetoran_100)
            SisaHutangPajak_SaatCutOff_Public(SisaHutang_SaatCutOff_401, AwalanBP, JenisPajak, KodeSetoran_401)
        End If

        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                'Kode Setoran : 100
                SaldoAkhir_BerdasarkanList_100 = SisaHutang_SaatCutOff_100
                txt_SaldoBerdasarkanList_100.Text = SaldoAkhir_BerdasarkanList_100
                AmbilValue_SaldoAkhirBerdasarkanCOA_100()
                CekKesesuaianSaldoAkhir_100()
                txt_SelisihSaldo_100.Text = SaldoAkhir_BerdasarkanList_100 - SaldoAkhir_BerdasarkanCOA_100
                'Kode Setoran : 401
                SaldoAkhir_BerdasarkanList_401 = SisaHutang_SaatCutOff_401
                txt_SaldoBerdasarkanList_401.Text = SaldoAkhir_BerdasarkanList_401
                AmbilValue_SaldoAkhirBerdasarkanCOA_401()
                CekKesesuaianSaldoAkhir_401()
                txt_SelisihSaldo_401.Text = SaldoAkhir_BerdasarkanList_401 - SaldoAkhir_BerdasarkanCOA_401
            Case JenisTahunBuku_NORMAL
                'Penjelasan : Variabel-variabel di bawah ini untuk mendapatkan jumlah bayar atas hutang pajak tahun sebelum TahunBukuAktif,
                'tapi dibayarkan pada tahun ini (TahunBukuAktif).
                Dim TotalBayar_UntukHutangPajakTahunSebelumIni_100 As Int64 = 0
                Dim TotalBayar_UntukHutangPajakTahunSebelumIni_401 As Int64 = 0
                AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_100, TotalBayar_UntukHutangPajakTahunSebelumIni_100)
                AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_401, TotalBayar_UntukHutangPajakTahunSebelumIni_401)
                If Not TahunBukuSudahStabil(TahunBukuAktif) Then
                    'Kode Setoran : 100
                    AmbilValue_SaldoAwalBerdasarkanList_100()
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_100()
                    CekKesesuaianSaldoAwal_100()
                    txt_SelisihSaldo_100.Text = SaldoAwal_BerdasarkanList_100 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100
                    txt_TotalTabel_100.Text = SaldoAwal_BerdasarkanList_100 + TotalTagihan100 - (TotalBayar100 + TotalBayar_UntukHutangPajakTahunSebelumIni_100)
                    'Kode Setoran : 401
                    AmbilValue_SaldoAwalBerdasarkanList_401()
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_401()
                    CekKesesuaianSaldoAwal_401()
                    txt_SelisihSaldo_401.Text = SaldoAwal_BerdasarkanList_401 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_401
                    txt_TotalTabel_401.Text = SaldoAwal_BerdasarkanList_401 + TotalTagihan401 - (TotalBayar401 + TotalBayar_UntukHutangPajakTahunSebelumIni_401)
                Else
                    txt_TotalTabel_100.Text = SaldoAwal_BerdasarkanCOA_100 + TotalTagihan100 - (TotalBayar100 + TotalBayar_UntukHutangPajakTahunSebelumIni_100)
                    txt_TotalTabel_401.Text = SaldoAwal_BerdasarkanCOA_401 + TotalTagihan401 - (TotalBayar401 + TotalBayar_UntukHutangPajakTahunSebelumIni_401)
                End If
                txt_TotalTabel_Total.Text _
                    = AmbilAngka(txt_TotalTabel_100.Text) _
                    + AmbilAngka(txt_TotalTabel_401.Text)
        End Select

        lbl_TotalTabel.Text = "Saldo Akhir " & TahunPajak & " : "

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()

        If JenisTampilan <> JenisTampilan_REKAP And PPh = 0 Then Return

        NomorUrut += 1
        JumlahBarisDalamSatuBulan += 1

        If JenisTampilan = JenisTampilan_DETAIL Then JumlahTagihan = PPh
        If JenisTampilan = JenisTampilan_ALL Then JumlahTagihan = PPh
        If JenisTampilan = JenisTampilan_REKAP Then
            IsiValue_DataPelaporan()
            DPP = RekapPerBulanDPP
            JumlahTagihan = RekapPerBulanPPh
        End If

        datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPHP, Bulan,
                                TanggalLapor, NomorIDLapor, TWTL_Lapor, NP_Lapor,
                                TanggalTransaksi, TanggalInvoice, NomorInvoice, NomorFakturPajak, NamaJasa, NPWP, KodeSupplier, NamaSupplier,
                                RekapPerBulanDPP100JasaOP, RekapPerBulanDPP100Gaji, RekapPerBulanDPP100, RekapPerBulanDPP401, DPP,
                                RekapPerBulanPPh100JasaOP, RekapPerBulanPPh100Gaji, RekapPerBulanPPh100, RekapPerBulanPPh401, JumlahTagihan,
                                JumlahBayar100, JumlahBayar401, JumlahBayar, SisaHutang,
                                KodeSetoran_UntukTabel, JenisKodeSetoran, Keterangan)

        Index_BarisTabel += 1
        Terabas()

    End Sub

    Sub IsiValue_DataPelaporan()

        BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)
        cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_PengawasanPelaporanPajak " &
                                      " WHERE Bulan     = '" & NomorBulan & " ' " &
                                      " AND Jenis_Pajak = '" & JenisPajak & " ' ", KoneksiDatabaseTransaksi_Alternatif)
        drTELUSUR2_ExecuteReader()
        drTELUSUR2.Read()
        If drTELUSUR2.HasRows Then
            TanggalLapor = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_Lapor"))
            NomorIDLapor = drTELUSUR2.Item("Nomor_ID")
            NP_Lapor = drTELUSUR2.Item("N_P")
            If NP_Lapor <> "N" Then
                'KompensasiKe = drTELUSUR2.Item("Kompensasi_Ke_Tahun") & "-" & KonversiBulanKeAngka(drTELUSUR2.Item("Kompensasi_Ke_Bulan"))
            Else
                'KompensasiKe = Kosongan
            End If
        Else
            TanggalLapor = Kosongan
            NomorIDLapor = 0
            NP_Lapor = Kosongan
            'KompensasiKe = Kosongan
        End If

        'Logika Tepat Waktu :
        If TanggalLapor = Kosongan Or NP_Lapor <> "N" Then
            TWTL_Lapor = Kosongan
        Else
            Dim TanggalLapor_Date As Date = TanggalLapor
            Dim BulanDeadlineLapor = NomorBulan + 1
            Dim TahunDeadlineLapor = TahunPajak
            If BulanDeadlineLapor = 13 Then
                BulanDeadlineLapor = 1
                TahunDeadlineLapor = TahunPajak + 1
            End If
            Dim TanggalDeadlineLapor As Date = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanDeadlineLapor, TahunDeadlineLapor)
            If TanggalLapor_Date <= TanggalDeadlineLapor Then
                TWTL_Lapor = TWTL_TepatWaktu
            Else
                TWTL_Lapor = TWTL_Terlambat
            End If
        End If
        TutupDatabaseTransaksi_Alternatif()

    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub

    Sub AmbilValue_NamaDanNPWPSupplier()
        AksesDatabase_General(Buka)
        cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                      " WHERE Kode_Mitra = '" & KodeSupplier & "' ", KoneksiDatabaseGeneral)
        drTELUSUR2_ExecuteReader()
        drTELUSUR2.Read()
        If drTELUSUR2.HasRows Then
            NamaSupplier = drTELUSUR2.Item("Nama_Mitra")
            NPWP = drTELUSUR2.Item("NPWP")
        End If
        AksesDatabase_General(Tutup)
    End Sub


    Sub AmbilValue_PerKodeSetoran()

        KodeSetoran_UntukTabel = dr.Item("Kode_Setoran")
        JenisKodeSetoran = PenentuanJenisKodeSetoran(JenisPajak, KodeSetoran_UntukTabel)

        Select Case KodeSetoran_UntukTabel
            Case KodeSetoran_100 'Setoran Masa
                DPP100 = DPP
                PPh100 = PPh
                RekapPerBulanDPP100 += DPP
                RekapPerBulanPPh100 += PPh
            Case KodeSetoran_401 'Pesangon
                DPP401 = DPP
                PPh401 = PPh
                RekapPerBulanDPP401 += DPP
                RekapPerBulanPPh401 += PPh
        End Select

        RekapPerBulanDPP += DPP
        RekapPerBulanPPh += PPh

    End Sub


    Sub Baris_KetetapanPajak()

        Dim JenisPajak_YangDitelusuri = JenisPajak
        Dim NomorBPHP_KetetapanPajak = Kosongan
        Dim JumlahTagihan_KetetapanPajak
        Dim JumlahBayar_KetetapanPajak
        Dim SisaHutang_KetetapanPajak

        BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)
        cmd = New OdbcCommand(" SELECT * FROM tbl_KetetapanPajak " &
                              " WHERE Jenis_Pajak = '" & JenisPajak_YangDitelusuri & "' ",
                              KoneksiDatabaseTransaksi_Alternatif)
        dr_ExecuteReader()
        JumlahTagihan_KetetapanPajak = 0
        Do While dr.Read
            NomorBPHP_KetetapanPajak = dr.Item("Nomor_BPHP")
            JumlahTagihan_KetetapanPajak += dr.Item("Pokok_Pajak")
        Loop

        'Data Pembayaran Pokok Pajak :
        JumlahBayar_KetetapanPajak = 0
        cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar, Kode_Setoran FROM tbl_BuktiPengeluaran " &
                                   " WHERE Nomor_BP     LIKE '%" & AwalanBPKP & "%' " &
                                   " AND Jenis_Pajak    = '" & JenisPajak_YangDitelusuri & "' " &
                                   " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        drBAYAR_ExecuteReader()
        Do While drBAYAR.Read
            JumlahBayar_KetetapanPajak += drBAYAR.Item("Jumlah_Bayar")
        Loop

        TutupDatabaseTransaksi_Alternatif()

        SisaHutang_KetetapanPajak = JumlahTagihan_KetetapanPajak - JumlahBayar_KetetapanPajak

        datatabelUtama.Rows.Add()
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, "Ketetapan Pajak",
                                Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                0, 0, 0, 0, 0,
                                0, 0, 0, 0, JumlahTagihan_KetetapanPajak,
                                0, 0, JumlahBayar_KetetapanPajak, SisaHutang_KetetapanPajak)

        TotalTagihan += JumlahTagihan_KetetapanPajak
        TotalBayar += JumlahBayar_KetetapanPajak
        TotalSisaHutang += SisaHutang_KetetapanPajak

    End Sub

    Private Sub cmb_TahunPajak_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_TahunPajak.SelectionChanged

        TahunPajak = AmbilAngka(cmb_TahunPajak.SelectedValue)
        TahunPajakSebelumIni = TahunPajak - 1

        If TahunPajak > TahunCutOff Then
            JenisTahunBukuPajak = JenisTahunBuku_NORMAL
            TahunBukuSumberData = TahunPajak
            VisibilitasTombolDetailPembayaran(True)
        Else
            JenisTahunBukuPajak = JenisTahunBuku_LAMPAU
            TahunBukuSumberData = TahunCutOff
            VisibilitasTombolDetailPembayaran(False)
        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            lbl_MasaPajak.IsEnabled = True
            cmb_MasaPajak.IsEnabled = True
        End If

        If TahunPajak = TahunBukuAktif Then
            TahunPajakSamaDenganTahunBukuAktif = True
        Else
            TahunPajakSamaDenganTahunBukuAktif = False
        End If

        If ProsesLoadingForm = False Then
            If MasaPajak = MasaPajak_Rekap Then Sub_JenisTampilan_REKAP()
            If MasaPajak <> MasaPajak_Rekap Then cmb_MasaPajak.SelectedValue = MasaPajak_Rekap
        End If

    End Sub

    Private Sub cmb_MasaPajak_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_MasaPajak.SelectionChanged

        MasaPajak = cmb_MasaPajak.SelectedValue
        MasaPajak_Angka = KonversiBulanKeAngka(MasaPajak)

        If ProsesLoadingForm = False Then
            Select Case MasaPajak
                Case MasaPajak_All
                    Sub_JenisTampilan_ALL()
                Case MasaPajak_Rekap
                    Sub_JenisTampilan_REKAP()
                Case Else
                    Sub_JenisTampilan_DETAIL()
            End Select
        End If

    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_DetailGajiDanPesangon_Click(sender As Object, e As RoutedEventArgs) Handles btn_DetailGajiDanPesangon.Click

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            win_InputHutangPPhPasal21_GajiDanPesangon = New wpfWin_InputHutangPPhPasal21_GajiDanPesangon
            win_InputHutangPPhPasal21_GajiDanPesangon.ResetForm()
            Dim BulanTerIndex
            For Each row As DataRow In datatabelUtama.Rows
                BulanTerIndex = row("Bulan_")
                If Not IsDBNull(BulanTerIndex) Then
                    Select Case BulanTerIndex
                        Case Bulan_Januari
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Januari.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Januari.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Januari.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Januari.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Februari
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Februari.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Februari.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Februari.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Februari.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Maret
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Maret.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Maret.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Maret.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Maret.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_April
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_April.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_April.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_April.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_April.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Mei
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Mei.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Mei.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Mei.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Mei.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Juni
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Juni.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Juni.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Juni.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Juni.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Juli
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Juli.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Juli.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Juli.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Juli.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Agustus
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Agustus.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Agustus.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Agustus.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Agustus.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_September
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_September.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_September.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_September.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_September.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Oktober
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Oktober.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Oktober.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Oktober.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Oktober.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Nopember
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Nopember.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Nopember.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Nopember.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Nopember.Text = AmbilAngka(row("PPh_401"))
                        Case Bulan_Desember
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPGaji_Desember.Text = AmbilAngka(row("DPP_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhGaji_Desember.Text = AmbilAngka(row("PPh_100_Gaji"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_DPPPesangon_Desember.Text = AmbilAngka(row("DPP_401"))
                            win_InputHutangPPhPasal21_GajiDanPesangon.txt_PPhPesangon_Desember.Text = AmbilAngka(row("PPh_401"))
                    End Select
                End If
            Next
            win_InputHutangPPhPasal21_GajiDanPesangon.ShowDialog()
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            EksekusiKode = False
            frm_BOOKU.BukaModul_BukuPengawasanGaji()
            EksekusiKode = True
            If MasaPajak = MasaPajak_Rekap Then
                usc_BukuPengawasanGaji.cmb_Bulan.SelectedValue = usc_BukuPengawasanGaji.Bulan_ALL
            Else
                usc_BukuPengawasanGaji.cmb_Bulan.SelectedValue = MasaPajak
            End If
        End If

    End Sub

    Sub Sub_JenisTampilan_ALL()
        JenisTampilan = JenisTampilan_ALL
        JudulForm = "Daftar Transaksi " & JenisPajak
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub

    Sub Sub_JenisTampilan_REKAP()
        JenisTampilan = JenisTampilan_REKAP
        JudulForm = "Buku Pengawasan Hutang " & JenisPajak
        VisibilitasObjek_REKAP()
        TampilkanData()
    End Sub

    Sub Sub_JenisTampilan_DETAIL()
        JenisTampilan = JenisTampilan_DETAIL
        JudulForm = "Daftar Transaksi " & JenisPajak
        VisibilitasObjek_DETAIL()
        TampilkanData()
    End Sub

    Sub VisibilitasObjek_REKAP()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        Nomor_BPHP.Visibility = Visibility.Collapsed
        Bulan_.Visibility = Visibility.Visible
        Bulan_.Header = "Masa Pajak"
        Tanggal_Transaksi.Visibility = Visibility.Collapsed
        Tanggal_Invoice.Visibility = Visibility.Collapsed
        Nomor_Invoice.Visibility = Visibility.Collapsed
        Nomor_Faktur_Pajak.Visibility = Visibility.Collapsed
        Nama_Jasa.Visibility = Visibility.Collapsed
        NPWP_.Visibility = Visibility.Collapsed
        Nama_Supplier.Visibility = Visibility.Collapsed
        DPP_.Header = "Jumlah" & Enter1Baris & "DPP"
        Jumlah_Bayar_Pajak.Visibility = Visibility.Visible
        Sisa_Hutang_Pajak.Visibility = Visibility.Visible
        Kode_Setoran.Visibility = Visibility.Collapsed
        Jenis_Kode_Setoran.Visibility = Visibility.Collapsed
        Keterangan_.Visibility = Visibility.Collapsed
    End Sub

    Sub VisibilitasObjek_DETAIL()
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        Nomor_BPHP.Visibility = Visibility.Collapsed
        Bulan_.Visibility = Visibility.Collapsed
        Tanggal_Transaksi.Visibility = Visibility.Visible
        Tanggal_Invoice.Visibility = Visibility.Visible
        Nomor_Invoice.Visibility = Visibility.Visible
        Nomor_Faktur_Pajak.Visibility = Visibility.Visible
        Nama_Jasa.Visibility = Visibility.Visible
        NPWP_.Visibility = Visibility.Visible
        Nama_Supplier.Visibility = Visibility.Visible
        DPP_.Visibility = Visibility.Visible
        DPP_.Header = "DPP"
        Jumlah_Bayar_Pajak.Visibility = Visibility.Collapsed
        Sisa_Hutang_Pajak.Visibility = Visibility.Collapsed
        Kode_Setoran.Visibility = Visibility.Visible
        Jenis_Kode_Setoran.Visibility = Visibility.Visible
        Keterangan_.Visibility = Visibility.Visible
    End Sub

    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        If Visibilitas Then
            brd_LihatJurnal.Visibility = Visibility.Visible
            btn_LihatJurnal.Visibility = Visibility.Visible
        Else
            brd_LihatJurnal.Visibility = Visibility.Collapsed
            btn_LihatJurnal.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolCRUD(Visibilitas As Boolean)
        If Visibilitas Then
            pnl_CRUD.Visibility = Visibility.Visible
        Else
            pnl_CRUD.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolDetailPembayaran(Visibilitas As Boolean)
        brd_DetailPembayaran.Visibility = Visibility.Collapsed
        btn_DetailPembayaran.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                brd_DetailPembayaran.Visibility = Visibility.Visible
                btn_DetailPembayaran.Visibility = Visibility.Visible
            End If
        Else
            brd_DetailPembayaran.Visibility = Visibility.Collapsed
            btn_DetailPembayaran.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolUpdateBayar(Visibilitas As Boolean)
        If Visibilitas Then
            btn_EditBayar.Visibility = Visibility.Visible
            btn_HapusBayar.Visibility = Visibility.Visible
        Else
            btn_EditBayar.Visibility = Visibility.Collapsed
            btn_HapusBayar.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasInfoSaldo(Visibilitas As Boolean)
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_TotalTabel.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If TahunPajakSamaDenganTahunBukuAktif And JenisTampilan = JenisTampilan_REKAP Then
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
    End Sub

    Sub VisibilitasTabelPembayaran()
        If JumlahBarisBayar > 0 Then
            datagridBayar.Visibility = Visibility.Visible
        Else
            datagridBayar.Visibility = Visibility.Collapsed
        End If
    End Sub



    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            MsgBox("Data terpilih BELUM masuk JURNAL.")
            Return
        End If
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputHutangPajak = New wpfWin_InputHutangPajak
        win_InputHutangPajak.ResetForm()
        win_InputHutangPajak.FungsiForm = FungsiForm_TAMBAH
        IsiValueComboBypassTerkunci(win_InputHutangPajak.cmb_JenisPajak, JenisPajak)
        win_InputHutangPajak.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        win_InputHutangPajak = New wpfWin_InputHutangPajak
        win_InputHutangPajak.ResetForm()
        win_InputHutangPajak.FungsiForm = FungsiForm_EDIT
        win_InputHutangPajak.BulanTransaksi = MasaPajak_Angka
        ProsesLoadingForm = True
        win_InputHutangPajak.NomorId = NomorID_Terseleksi
        win_InputHutangPajak.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(TanggalTransaksi_Terseleksi)
        win_InputHutangPajak.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice_Terseleksi)
        win_InputHutangPajak.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        win_InputHutangPajak.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        win_InputHutangPajak.txt_NamaJasa.Text = NamaJasa_Terseleksi
        win_InputHutangPajak.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        win_InputHutangPajak.txt_DPP.Text = DPP_Terseleksi
        IsiValueComboBypassTerkunci(win_InputHutangPajak.cmb_JenisPajak, JenisPajak)
        win_InputHutangPajak.cmb_KodeSetoran.SelectedValue = KodeSetoran_Terseleksi
        win_InputHutangPajak.txt_JumlahHutang.Text = PPh_Terseleksi
        IsiValueElemenRichTextBox(win_InputHutangPajak.txt_Keterangan, Keterangan_Terseleksi)
        ProsesLoadingForm = False
        win_InputHutangPajak.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_HutangPajak " &
                              " WHERE Nomor_ID = '" & NomorID_Terseleksi & "'",
                              KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        If StatusSuntingDatabase = True Then
            TampilkanData()
            pesan_DataTerpilihBerhasilDihapus()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

        AksesDatabase_Transaksi(Tutup)

    End Sub



    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        If SisaHutang_Terseleksi <= 0 Then
            MsgBox("Hutang Pajak Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim JumlahTagihan As Int64 = 0
        Dim SisaHutang As Int64 = 0
        Dim JumlahBayar
        Dim KodeSetoran = Kosongan
        JumlahBayar = 0
        Select Case True
            Case rdb_KodeSetoran_100.IsChecked
                If SisaHutang100_Terseleksi <= 0 Then
                    MsgBox("Hutang " & JenisPajak & " Kode-100 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPh100_Terseleksi
                SisaHutang = SisaHutang100_Terseleksi
                JumlahBayar = JumlahBayar100_Terseleksi
                KodeSetoran = KodeSetoran_100
            Case rdb_KodeSetoran_401.IsChecked
                If SisaHutang401_Terseleksi <= 0 Then
                    MsgBox("Hutang " & JenisPajak & " Kode-401 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPh401_Terseleksi
                SisaHutang = SisaHutang401_Terseleksi
                JumlahBayar = JumlahBayar401_Terseleksi
                KodeSetoran = KodeSetoran_401
        End Select

        Dim TanggalTagihanPajak = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(Bulan_Terseleksi, TahunPajak)
        '(Variabel ini tidak ada kaitannya dengan Invoice dari DJP. Ini hanya untuk kepentingan algoritma.)

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.JenisPajak = JenisPajak
        win_InputBuktiPengeluaran.KodeSetoran = KodeSetoran
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangPajak
        win_InputBuktiPengeluaran.NomorBP = NomorBPHP_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_DJP
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, Kosongan, TanggalTagihanPajak, "Pembayaran " & JenisPajak & " - " & KodeSetoran & " - " & Bulan_Terseleksi, NomorBPHP_Terseleksi,
                                JumlahTagihan, 0, 0, 0, JumlahBayar, SisaHutang,
                                JenisPajak, KodeSetoran, 0, 0, 0,
                                SisaHutang, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Private Sub btn_DetailPembayaran_Click(sender As Object, e As RoutedEventArgs) Handles btn_DetailPembayaran.Click
        frm_DetailPembayaranPajak.ResetForm()
        frm_DetailPembayaranPajak.JenisPajak = JenisPajak
        frm_DetailPembayaranPajak.ShowDialog()
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
        NomorID_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_ID"))
        Bulan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Bulan_")
        If Bulan_Terseleksi = Kosongan Or Bulan_Terseleksi = teks_TOTAL_ Then
            BersihkanSeleksi()
            Return
        End If
        NomorBPHP_Terseleksi = rowviewUtama("Nomor_BPHP")
        TanggalTransaksi_Terseleksi = rowviewUtama("Tanggal_Transaksi")
        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice")
        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice")
        NomorFakturPajak_Terseleksi = rowviewUtama("Nomor_Faktur_Pajak")
        NamaJasa_Terseleksi = rowviewUtama("Nama_Jasa")
        KodeSupplier_Terseleksi = rowviewUtama("Kode_Supplier")
        NamaSupplier_Terseleksi = rowviewUtama("Nama_Supplier")
        NPWP_Terseleksi = rowviewUtama("NPWP_")
        DPP_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "DPP_"))
        PPh100_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "PPh_100"))
        PPh401_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "PPh_401"))
        PPh_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "PPh_"))
        JumlahTagihan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "PPh_"))
        JumlahBayar100_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Bayar_100"))
        JumlahBayar401_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Bayar_401"))
        JumlahBayar_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Bayar_Pajak"))
        SisaHutang100_Terseleksi = PPh100_Terseleksi - JumlahBayar100_Terseleksi
        SisaHutang401_Terseleksi = PPh401_Terseleksi - JumlahBayar401_Terseleksi
        SisaHutang_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Sisa_Hutang_Pajak"))
        KodeSetoran_Terseleksi = rowviewUtama("Kode_Setoran")
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")

        'Kolom Lapor :
        TanggalLapor_Terseleksi = rowviewUtama("Tanggal_Lapor")
        NomorIDLapor_Terseleksi = rowviewUtama("Nomor_ID_Lapor")
        NP_Lapor_Terseleksi = rowviewUtama("N_P_Lapor")


        If JenisTampilan = JenisTampilan_DETAIL Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU And NomorID_Terseleksi = 0 Then BersihkanSeleksi()
        End If

        If JenisTampilan = JenisTampilan_REKAP Then
            If JumlahTagihan_Terseleksi > 0 Then
                ResetTampilanDataPembayaran()
                rdb_KodeSetoran_100.IsEnabled = False
                rdb_KodeSetoran_401.IsEnabled = False
                If PPh100_Terseleksi > 0 Then rdb_KodeSetoran_100.IsEnabled = True
                If PPh401_Terseleksi > 0 Then rdb_KodeSetoran_401.IsEnabled = True
            Else
                pnl_SidebarKanan.Visibility = Visibility.Collapsed
                'If Bulan_Terseleksi <> JenisPajak_KetetapanPajak Then BersihkanSeleksi()
            End If
        End If

        If JenisTampilan = JenisTampilan_DETAIL Then
            If NomorUrut_Terseleksi > 0 Then
                btn_Edit.IsEnabled = True
                btn_Hapus.IsEnabled = True
            Else
                btn_Edit.IsEnabled = False
                btn_Hapus.IsEnabled = False
            End If
        End If

        If JenisTampilan = JenisTampilan_REKAP Then
            If JumlahBayar_Terseleksi > 0 And SisaHutang_Terseleksi <= 0 Then
                grb_LaporSPT.IsEnabled = True
                If TanggalLapor_Terseleksi = Kosongan Then
                    btn_InputSPT.IsEnabled = True
                    btn_EditSPT.IsEnabled = False
                    btn_HapusSPT.IsEnabled = False
                Else
                    btn_InputSPT.IsEnabled = False
                    btn_EditSPT.IsEnabled = True
                    btn_HapusSPT.IsEnabled = True
                End If
            Else
                grb_LaporSPT.IsEnabled = False
            End If
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        If JenisTampilan = JenisTampilan_DETAIL Or JenisTampilan = JenisTampilan_ALL Then
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                    If NomorID_Terseleksi = 0 Then
                        btn_Input_Click(sender, e)
                    Else
                        btn_Edit_Click(sender, e)
                    End If
                End If
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    'belum ada codingnya.
                End If
            End If
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                'belum ada codingnya.
            End If
        End If
        If JenisTampilan = JenisTampilan_REKAP And cmb_MasaPajak.IsEnabled = True Then
            If NomorUrut_Terseleksi <> 0 Then cmb_MasaPajak.SelectedValue = datatabelUtama.Rows(BarisTerseleksi)("Bulan_")
        End If
        If Bulan_Terseleksi = JenisPajak_KetetapanPajak Then
            frm_BukuPengawasanKetetapanPajak.MdiParent = frm_BOOKU
            frm_BukuPengawasanKetetapanPajak.Show()
            frm_BukuPengawasanKetetapanPajak.Focus()
            usc_BukuPengawasanKetetapanPajak.cmb_PilihanJenisPajak.SelectedValue = JenisPajak
        End If
    End Sub


    Private Sub rdb_KodeSetoran_100_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_100.Checked
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_401_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_401.Checked
        LogikaKodeSetoran()
    End Sub
    Sub LogikaKodeSetoran()
        If rdb_KodeSetoran_100.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_100
        If rdb_KodeSetoran_401.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_401
        If rdb_KodeSetoran_100.IsChecked = True Or rdb_KodeSetoran_401.IsChecked = True Then
            TampilkanDataPembayaran()
        Else
            KodeSetoran_UntukBayar = KodeSetoran_Non
        End If
    End Sub



    Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    End Sub
    Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
        HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomBayar IsNot Nothing Then
            btn_LihatJurnal.IsEnabled = False
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
        NomorJV_Pembayaran_Terseleksi = rowviewBayar("Nomor_JV_Bayar")
        Referensi_Terseleksi = rowviewBayar("Referensi_")
        TahunPembayaran_Terseleksi = AmbilAngka(Microsoft.VisualBasic.Left(AmbilAngka(Referensi_Terseleksi), 4))
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditBayar.IsEnabled = True
            btn_HapusBayar.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
            btn_EditBayar.IsEnabled = False
            btn_HapusBayar.IsEnabled = False
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.IsEnabled = False
        If TahunPembayaran_Terseleksi <> TahunBukuAktif Then
            'btn_EditPembayaran.isenabled = False
            'btn_HapusPembayaran.isenabled = False
        End If
    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
    End Sub




    Private Sub btn_InputSPT_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputSPT.Click
        win_InputLaporPajak = New wpfWin_InputLaporPajak
        win_InputLaporPajak.ResetForm()
        win_InputLaporPajak.FungsiForm = FungsiForm_TAMBAH
        win_InputLaporPajak.JenisPajak = JenisPajak_PPN
        BukaFormInputLaporPajak()
    End Sub

    Private Sub btn_EditSPT_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditSPT.Click
        win_InputLaporPajak = New wpfWin_InputLaporPajak
        win_InputLaporPajak.ResetForm()
        win_InputLaporPajak.FungsiForm = FungsiForm_EDIT
        win_InputLaporPajak.JenisPajak = JenisPajak_PPN
        win_InputLaporPajak.NomorID = NomorIDLapor_Terseleksi
        win_InputLaporPajak.cmb_NP.SelectedValue = NP_Lapor_Terseleksi
        win_InputLaporPajak.dtp_TanggalLapor.SelectedDate = TanggalFormatWPF(TanggalLapor_Terseleksi)
        BukaFormInputLaporPajak()
    End Sub

    Sub BukaFormInputLaporPajak()
        ProsesIsiValueForm = True
        win_InputLaporPajak.JenisPajak = JenisPajak
        win_InputLaporPajak.txt_MasaPajak.Text = Bulan_Terseleksi
        win_InputLaporPajak.NP = NP_Lapor_Terseleksi
        win_InputLaporPajak.txt_JumlahLebihBayar.Text = 0 'Untuk PPh, sementara di-nol-kan (0) dulu.
        ProsesIsiValueForm = False
        win_InputLaporPajak.ShowDialog()
    End Sub

    Private Sub btn_HapusSPT_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusSPT.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus laporan terpilih..?" & Enter2Baris &
                                  "Catatan :" & Enter1Baris &
                                  "Data invoice tidak akan terhapus pada event ini.",
                                  "Perhatian..!", MessageBoxButtons.YesNo)

        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_PengawasanPelaporanPajak " &
                              " WHERE Nomor_ID = '" & NomorIDLapor_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        PesanUntukProgrammer("Hapus juga data 'Tanggal Lapor' di masing-masing INVOICE terkait...!!! ")

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub




    Sub ResetTampilanDataPembayaran()
        pnl_SidebarKanan.Visibility = Visibility.Visible
        pnl_DataGridBayar.Visibility = Visibility.Collapsed
        datatabelBayar.Rows.Clear()
        KodeSetoran_UntukBayar = KodeSetoran_Non
        rdb_KodeSetoran_100.IsChecked = False
        rdb_KodeSetoran_401.IsChecked = False
    End Sub

    Sub TampilkanDataPembayaran()

        pnl_DataGridBayar.Visibility = Visibility.Visible

        datatabelBayar.Rows.Clear()
        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar As Int64 = 0
        Dim TWTLBayar = Kosongan
        Dim TotalBayar As Int64 = 0
        Dim KeteranganBayar
        Dim NomorJV_Pembayaran
        Dim TahunSumberDataPembayaran = 0

        Dim TahunTelusurPembayaran = TahunPajak
        Dim PencegahLoopingTahunPajakLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunSumberDataPembayaran = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunSumberDataPembayaran = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunPajakLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunSumberDataPembayaran)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Nomor_BP          = '" & NomorBPHP_Terseleksi & "' " &
                                      " AND Kode_Setoran        = '" & KodeSetoran_UntukBayar & "' " &
                                      " AND Status_Invoice      = '" & Status_Dibayar & "' " &
                                      " ORDER BY Nomor_ID ", KoneksiDatabaseTransaksi_Alternatif)
                dr = cmd.ExecuteReader
                Do While dr.Read
                    NomorIdBayar = dr.Item("Nomor_ID")
                    TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                    Referensi = dr.Item("Nomor_KK")
                    JumlahBayar = dr.Item("Jumlah_Bayar")
                    TotalBayar += JumlahBayar
                    KeteranganBayar = dr.Item("Catatan")
                    If TahunTelusurPembayaran = TahunBukuAktif Then
                        NomorJV_Pembayaran = dr.Item("Nomor_JV")
                    Else
                        NomorJV_Pembayaran = 0
                    End If
                    datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, TWTLBayar, KeteranganBayar, NomorJV_Pembayaran)
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
            PencegahLoopingTahunPajakLampau += 1
            TahunTelusurPembayaran += 1
        Loop

        BersihkanSeleksiTabelPembayaran()

    End Sub

    Sub BersihkanSeleksiTabelPembayaran()
        datagridBayar.SelectedIndex = -1
        datagridBayar.SelectedItem = Nothing
        datagridBayar.SelectedCells.Clear()
        BarisBayar_Terseleksi = -1
        JumlahBarisBayar = datatabelBayar.Rows.Count
        btn_EditBayar.IsEnabled = False
        btn_HapusBayar.IsEnabled = False
        NomorJV_Pembayaran_Terseleksi = 0
        VisibilitasTabelPembayaran()
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
    Dim Nomor_BPHP As New DataGridTextColumn
    Dim Bulan_ As New DataGridTextColumn
    Dim Tanggal_Lapor As New DataGridTextColumn
    Dim Nomor_ID_Lapor As New DataGridTextColumn
    Dim TW_TL_Lapor As New DataGridTextColumn
    Dim N_P_Lapor As New DataGridTextColumn
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Nama_Jasa As New DataGridTextColumn
    Dim NPWP_ As New DataGridTextColumn
    Dim Kode_Supplier As New DataGridTextColumn
    Dim Nama_Supplier As New DataGridTextColumn
    Dim DPP_100_Jasa_OP As New DataGridTextColumn
    Dim DPP_100_Gaji As New DataGridTextColumn
    Dim DPP_100 As New DataGridTextColumn
    Dim DPP_401 As New DataGridTextColumn
    Dim DPP_ As New DataGridTextColumn
    Dim PPh_100_Jasa_OP As New DataGridTextColumn
    Dim PPh_100_Gaji As New DataGridTextColumn
    Dim PPh_100 As New DataGridTextColumn
    Dim PPh_401 As New DataGridTextColumn
    Dim PPh_ As New DataGridTextColumn
    Dim Jumlah_Bayar_100 As New DataGridTextColumn
    Dim Jumlah_Bayar_401 As New DataGridTextColumn
    Dim Jumlah_Bayar_Pajak As New DataGridTextColumn
    Dim Sisa_Hutang_Pajak As New DataGridTextColumn
    Dim Kode_Setoran As New DataGridTextColumn
    Dim Jenis_Kode_Setoran As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPHP")
        datatabelUtama.Columns.Add("Bulan_")
        datatabelUtama.Columns.Add("Tanggal_Lapor")
        datatabelUtama.Columns.Add("Nomor_ID_Lapor")
        datatabelUtama.Columns.Add("TW_TL_Lapor")
        datatabelUtama.Columns.Add("N_P_Lapor")
        datatabelUtama.Columns.Add("Tanggal_Transaksi")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Nama_Jasa")
        datatabelUtama.Columns.Add("NPWP_")
        datatabelUtama.Columns.Add("Kode_Supplier")
        datatabelUtama.Columns.Add("Nama_Supplier")
        datatabelUtama.Columns.Add("DPP_100_Jasa_OP", GetType(Int64))
        datatabelUtama.Columns.Add("DPP_100_Gaji", GetType(Int64))
        datatabelUtama.Columns.Add("DPP_100", GetType(Int64))
        datatabelUtama.Columns.Add("DPP_401", GetType(Int64))
        datatabelUtama.Columns.Add("DPP_", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_100_Jasa_OP", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_100_Gaji", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_100", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_401", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar_100", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar_401", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar_Pajak", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Hutang_Pajak", GetType(Int64))
        datatabelUtama.Columns.Add("Kode_Setoran")
        datatabelUtama.Columns.Add("Jenis_Kode_Setoran")
        datatabelUtama.Columns.Add("Keterangan_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPHP, "Nomor_BPHP", "Nomor BPHP", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Bulan_, "Bulan_", "Bulan", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Lapor, "Tanggal_Lapor", "Tanggal Lapor", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID_Lapor, "Nomor_ID_Lapor", "Nomor ID Lapor", 33, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, TW_TL_Lapor, "TW_TL_Lapor", "TW" & Enter1Baris & "TL", 33, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, N_P_Lapor, "N_P_Lapor", "N/P", 33, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Transaksi, "Tanggal_Transaksi", "Tanggal Transaksi", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Jasa, "Nama_Jasa", "Nama Jasa", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NPWP_, "NPWP_", "NPWP", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Supplier, "Kode_Supplier", "Kode Supplier", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Supplier, "Nama_Supplier", "Nama Supplier", 153, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_100_Jasa_OP, "DPP_100_Jasa_OP", "DPP-100" & Enter1Baris & "(Jasa OP)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_100_Gaji, "DPP_100_Gaji ", "DPP-100" & Enter1Baris & "(Gaji)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_100, "DPP_100", "DPP-100", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_401, "DPP_401", "DPP-401" & Enter1Baris & "(Pesangon)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_, "DPP_", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_100_Jasa_OP, "PPh_100_Jasa_OP", "PPh-100" & Enter1Baris & "(Jasa OP)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_100_Gaji, "PPh_100_Gaji ", "PPh-100" & Enter1Baris & "(Gaji)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_100, "PPh_100", "PPh-100", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_401, "PPh_401", "PPh-401", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_, "PPh_", "PPh", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar_100, "Jumlah_Bayar_100", "Jumlah Bayar" & Enter1Baris & "100", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar_401, "Jumlah_Bayar_401", "Jumlah Bayar" & Enter1Baris & "401", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar_Pajak, "Jumlah_Bayar_Pajak", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Hutang_Pajak, "Sisa_Hutang_Pajak", "Sisa Hutang", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Setoran, "Kode_Setoran", "Kode Setoran", 57, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Kode_Setoran, "Jenis_Kode_Setoran", "Jenis Kode Setoran", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan_", 123, FormatString, KiriTengah, KunciUrut, Terlihat)

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
    Dim TW_TL_Bayar As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar()

        datatabelBayar = New DataTable
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("TW_TL_Bayar")
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_, "Referensi_", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, TW_TL_Bayar, "TW_TL_Bayar", "TW/TL", 45, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        Buat_DataTabelBayar()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        cmb_TahunPajak.IsReadOnly = True
        cmb_MasaPajak.IsReadOnly = True
        '100
        txt_SaldoBerdasarkanList_100.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_100.IsReadOnly = True
        txt_SelisihSaldo_100.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_100.IsReadOnly = True
        txt_AJP_100.IsReadOnly = True
        txt_TotalTabel_100.IsReadOnly = True
        '401
        txt_SaldoBerdasarkanList_401.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_401.IsReadOnly = True
        txt_SelisihSaldo_401.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_401.IsReadOnly = True
        txt_AJP_401.IsReadOnly = True
        txt_TotalTabel_401.IsReadOnly = True
        'Total
        txt_TotalTabel_Total.IsReadOnly = True
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


    Sub AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran As String, ByRef SaldoAwal_BerdasarkanList As Int64, txt_SaldoBerdasarkanList As TextBox)
        If TahunPajak = TahunBukuAktif Then
            Dim JumlahTagihan_SA As Int64
            Dim JumlahBayar_SA As Int64
            Dim TahunTelusur_SA = TahunBukuAktif - 1
            If TahunTelusur_SA = TahunCutOff Then
                JumlahTagihan_SA = 0
                BukaDatabaseTransaksi_Alternatif(TahunTelusur_SA)
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                         " WHERE Jenis_Pajak    = '" & JenisPajak & "' " &
                                         " AND Kode_Setoran     = '" & KodeSetoran & "' ",
                                         KoneksiDatabaseTransaksi_Alternatif)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    JumlahTagihan_SA += drTAGIHAN.Item("Jumlah_Hutang")
                Loop
                TutupDatabaseTransaksi_Alternatif()
            Else
                'Ini tidak diperlukan, karena ketika TahunBukuAktif sudah 2 tahun dari TahunCutOff, maka tidak perlu ada lagi pengecekan keseimbangan,
                'karena sudah dipastikan akan sesuai antara data finance dengan data akuntansi.
            End If
            AmbilValue_JumlahBayarPajakTahunBukuKemarin_Public(AwalanBP, KodeSetoran, JumlahBayar_SA)
            SaldoAwal_BerdasarkanList = JumlahTagihan_SA - JumlahBayar_SA
            txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
        End If
    End Sub







    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================
    'Kode Setoran : 100

    Public KesesuaianSaldoAwal_100 As Boolean
    Public KesesuaianSaldoAkhir_100 As Boolean
    Public KesesuaianJurnal_100 As Boolean

    Dim SaldoAwal_BerdasarkanList_100
    Dim SaldoAwal_BerdasarkanCOA_100
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100
    Dim SaldoAkhir_BerdasarkanList_100
    Dim SaldoAkhir_BerdasarkanCOA_100
    Dim JumlahPenyesuaianSaldo_100

    Sub AmbilValue_SaldoAwalBerdasarkanList_100()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_100, SaldoAwal_BerdasarkanList_100, txt_SaldoBerdasarkanList_100)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_100()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutangPajak_100, SaldoAwal_BerdasarkanCOA_100, JumlahPenyesuaianSaldo_100, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100,
                                                                  txt_SaldoAwalBerdasarkanCOA_100, txt_AJP_100, txt_saldoBerdasarkanCOA_PlusPenyesuaian_100)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_100()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutangPajak_100, SaldoAkhir_BerdasarkanCOA_100, txt_saldoBerdasarkanCOA_PlusPenyesuaian_100)
    End Sub

    Sub CekKesesuaianSaldoAwal_100()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_100, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100, KesesuaianSaldoAwal_100,
                                      btn_Sesuaikan_100, txt_SaldoBerdasarkanList_100, txt_saldoBerdasarkanCOA_PlusPenyesuaian_100, txt_SelisihSaldo_100)
    End Sub

    Sub CekKesesuaianSaldoAkhir_100()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_100, SaldoAkhir_BerdasarkanCOA_100, KesesuaianSaldoAkhir_100,
                                      btn_Sesuaikan_100, txt_SaldoBerdasarkanList_100, txt_saldoBerdasarkanCOA_PlusPenyesuaian_100, txt_SelisihSaldo_100)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_100)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_100)
    End Sub

    Private Sub txt_SelisihSaldo_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_100)
    End Sub

    Private Sub btn_Sesuaikan_100_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_100.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutangPajak_100, SaldoAwal_BerdasarkanList_100, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_100)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_100)
    End Sub

    Private Sub txt_AJP_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_100)
    End Sub

    Private Sub txt_TotalTabel_100_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_100.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_100)
    End Sub

    '=======================================================================================================================================



    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================
    'Kode Setoran : 401

    Public KesesuaianSaldoAwal_401 As Boolean
    Public KesesuaianSaldoAkhir_401 As Boolean
    Public KesesuaianJurnal_401 As Boolean

    Dim SaldoAwal_BerdasarkanList_401
    Dim SaldoAwal_BerdasarkanCOA_401
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_401
    Dim SaldoAkhir_BerdasarkanList_401
    Dim SaldoAkhir_BerdasarkanCOA_401
    Dim JumlahPenyesuaianSaldo_401

    Sub AmbilValue_SaldoAwalBerdasarkanList_401()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_401, SaldoAwal_BerdasarkanList_401, txt_SaldoBerdasarkanList_401)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_401()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutangPajak_401, SaldoAwal_BerdasarkanCOA_401, JumlahPenyesuaianSaldo_401, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_401,
                                                                  txt_SaldoAwalBerdasarkanCOA_401, txt_AJP_401, txt_saldoBerdasarkanCOA_PlusPenyesuaian_401)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_401()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutangPajak_401, SaldoAkhir_BerdasarkanCOA_401, txt_saldoBerdasarkanCOA_PlusPenyesuaian_401)
    End Sub

    Sub CekKesesuaianSaldoAwal_401()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_401, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_401, KesesuaianSaldoAwal_401,
                                      btn_Sesuaikan_401, txt_SaldoBerdasarkanList_401, txt_saldoBerdasarkanCOA_PlusPenyesuaian_401, txt_SelisihSaldo_401)
    End Sub

    Sub CekKesesuaianSaldoAkhir_401()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_401, SaldoAkhir_BerdasarkanCOA_401, KesesuaianSaldoAkhir_401,
                                      btn_Sesuaikan_401, txt_SaldoBerdasarkanList_401, txt_saldoBerdasarkanCOA_PlusPenyesuaian_401, txt_SelisihSaldo_401)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_401_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_401.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_401)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_401_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_401.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_401)
    End Sub

    Private Sub txt_SelisihSaldo_401_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_401.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_401)
    End Sub

    Private Sub btn_Sesuaikan_401_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_401.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutangPajak_401, SaldoAwal_BerdasarkanList_401, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_401)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_401_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_401.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_401)
    End Sub

    Private Sub txt_AJP_401_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_401.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_401)
    End Sub

    Private Sub txt_TotalTabel_401_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_401.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_401)
    End Sub

    '=======================================================================================================================================


    Private Sub txt_TotalTabel_Total_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_Total.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_Total)
    End Sub

End Class
