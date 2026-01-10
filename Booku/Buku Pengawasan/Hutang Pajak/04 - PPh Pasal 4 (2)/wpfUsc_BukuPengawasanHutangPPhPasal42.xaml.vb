Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_BukuPengawasanHutangPPhPasal42

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
    Public COAHutangPajak_402 As String
    Public COAHutangPajak_403 As String
    Public COAHutangPajak_409 As String
    Public COAHutangPajak_419 As String

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
    Dim DPP_Terseleksi As Int64
    Dim PPh_Terseleksi As Int64
    Dim PPh402_Terseleksi As Int64
    Dim PPh403_Terseleksi As Int64
    Dim PPh409_Terseleksi As Int64
    Dim PPh419_Terseleksi As Int64
    Dim JumlahTagihan_Terseleksi As Int64
    Dim JumlahBayar402_Terseleksi As Int64
    Dim JumlahBayar403_Terseleksi As Int64
    Dim JumlahBayar409_Terseleksi As Int64
    Dim JumlahBayar419_Terseleksi As Int64
    Dim JumlahBayar_Terseleksi As Int64
    Dim SisaHutang402_Terseleksi As Int64
    Dim SisaHutang403_Terseleksi As Int64
    Dim SisaHutang409_Terseleksi As Int64
    Dim SisaHutang419_Terseleksi As Int64
    Dim SisaHutang_Terseleksi As Int64
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


    Dim SisaHutang_SaatCutOff_402 As Int64
    Dim SisaHutang_SaatCutOff_403 As Int64
    Dim SisaHutang_SaatCutOff_409 As Int64
    Dim SisaHutang_SaatCutOff_419 As Int64

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
    Dim DPP402 As Int64
    Dim DPP403 As Int64
    Dim DPP409 As Int64
    Dim DPP419 As Int64
    Dim DPP As Int64
    Dim RekapPerBulanDPP402 As Int64
    Dim RekapPerBulanDPP403 As Int64
    Dim RekapPerBulanDPP409 As Int64
    Dim RekapPerBulanDPP419 As Int64
    Dim RekapPerBulanDPP As Int64
    Dim PPh402 As Int64
    Dim PPh403 As Int64
    Dim PPh409 As Int64
    Dim PPh419 As Int64
    Dim PPh As Int64
    Dim RekapPerBulanPPh402 As Int64
    Dim RekapPerBulanPPh403 As Int64
    Dim RekapPerBulanPPh409 As Int64
    Dim RekapPerBulanPPh419 As Int64
    Dim RekapPerBulanPPh As Int64
    Dim JumlahTagihan As Int64
    Dim TanggalTransaksi
    Dim JumlahBayar402 As Int64
    Dim JumlahBayar403 As Int64
    Dim JumlahBayar409 As Int64
    Dim JumlahBayar419 As Int64
    Dim JumlahBayar As Int64
    Dim SisaHutang As Int64
    Dim JenisKodeSetoran
    Dim Keterangan

    Dim TotalDPP402 As Int64
    Dim TotalDPP403 As Int64
    Dim TotalDPP409 As Int64
    Dim TotalDPP419 As Int64
    Dim TotalDPP As Int64
    Dim TotalTagihan402 As Int64
    Dim TotalTagihan403 As Int64
    Dim TotalTagihan409 As Int64
    Dim TotalTagihan419 As Int64
    Dim TotalTagihan As Int64
    Dim TotalBayar402 As Int64
    Dim TotalBayar403 As Int64
    Dim TotalBayar409 As Int64
    Dim TotalBayar419 As Int64
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

        lbl_JudulForm.Text = frm_BukuPengawasanHutangPPhPasal23.JudulForm
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
        cmb_MasaPajak.Items.Add(MasaPajak_All)
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
        frm_BukuPengawasanHutangPPhPasal23.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        If MasaPajak = Kosongan Then Return

        KesesuaianJurnal_402 = True
        KesesuaianJurnal_403 = True
        KesesuaianJurnal_409 = True
        KesesuaianJurnal_419 = True

        'Style Tabel :
        datatabelUtama.Rows.Clear()
        DPP_402.Visibility = Visibility.Collapsed
        DPP_403.Visibility = Visibility.Collapsed
        DPP_409.Visibility = Visibility.Collapsed
        DPP_419.Visibility = Visibility.Collapsed
        PPh_402.Visibility = Visibility.Collapsed
        PPh_403.Visibility = Visibility.Collapsed
        PPh_409.Visibility = Visibility.Collapsed
        PPh_419.Visibility = Visibility.Collapsed
        Jumlah_Bayar_402.Visibility = Visibility.Collapsed
        Jumlah_Bayar_403.Visibility = Visibility.Collapsed
        Jumlah_Bayar_409.Visibility = Visibility.Collapsed
        Jumlah_Bayar_419.Visibility = Visibility.Collapsed

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

        TotalDPP402 = 0
        TotalDPP403 = 0
        TotalDPP409 = 0
        TotalDPP419 = 0
        TotalDPP = 0

        TotalTagihan402 = 0
        TotalTagihan403 = 0
        TotalTagihan409 = 0
        TotalTagihan419 = 0
        TotalTagihan = 0

        TotalBayar402 = 0
        TotalBayar403 = 0
        TotalBayar409 = 0
        TotalBayar419 = 0
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

                RekapPerBulanDPP402 = 0
                RekapPerBulanDPP403 = 0
                RekapPerBulanDPP409 = 0
                RekapPerBulanDPP419 = 0
                RekapPerBulanDPP = 0

                RekapPerBulanPPh402 = 0
                RekapPerBulanPPh403 = 0
                RekapPerBulanPPh409 = 0
                RekapPerBulanPPh419 = 0
                RekapPerBulanPPh = 0

                JumlahBayar402 = 0
                JumlahBayar403 = 0
                JumlahBayar409 = 0
                JumlahBayar419 = 0
                JumlahBayar = 0
                SisaHutang = 0

                NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

                'Jika [ Tahun Buku LAMPAU ] :
                If JenisTahunBukuPajak = JenisTahunBuku_LAMPAU Then

                    BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)

                    SumberData = SumberData_SisaHutangPajak
                    cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                          " WHERE Jenis_Pajak                           = '" & JenisPajak & "' " &
                                          " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        DPP = dr.Item("DPP")
                        PPh = dr.Item("Jumlah_Hutang")
                        AmbilValue_PerKodeSetoran()
                    Loop

                    TutupDatabaseTransaksi_Alternatif()

                End If

                'Jika [ Tahun Buku NORMAL ]   :
                If JenisTahunBukuPajak = JenisTahunBuku_NORMAL Then

                    BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)

                    cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                  " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                  " AND Kode_Setoran                       <> '" & KodeSetoran_419 & "' " & '(Dividen jangan dimasukkan).
                                  " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                  " AND Jumlah_Bayar                        > 0 " &
                                  " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                  KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        PPh = dr.Item("PPh_Terutang")
                        If PPh > 0 Then
                            SumberData = dr.Item("Peruntukan")
                            Dim NomorPembelian = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                            Dim cmdPEMB = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                                  " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                  " AND Jenis_Produk_Per_Item LIKE '%" & JenisProduk_Jasa & "%' ",
                                                  KoneksiDatabaseTransaksi_Alternatif)
                            Dim drPEMB = cmdPEMB.ExecuteReader
                            drPEMB.Read()
                            If drPEMB.HasRows Then
                                DPP = PPh / (drPEMB.Item("Tarif_PPh") / 100)
                            Else
                                DPP = dr.Item("Bagi_Hasil")
                            End If
                            AmbilValue_PerKodeSetoran()
                        End If
                    Loop

                    'Dividen :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangDividen " &
                                  " WHERE Jenis_PPh                                 = '" & KonversiJenisPajakKeJenisPPh(JenisPajak) & "' " &
                                  " AND DATE_FORMAT(Tanggal_Akta_Notaris, '%Y-%m')  = '" & TahunPajak & "-" & NomorBulan & "' ",
                                  KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        PPh = dr.Item("PPh_Terutang")
                        If PPh > 0 Then
                            DPP = dr.Item("Jumlah_Dividen")
                            SumberData = JenisJasa_Dividen
                            AmbilValue_PerKodeSetoran()
                        End If
                    Loop

                    TutupDatabaseTransaksi_Alternatif()

                End If

                TotalDPP402 += RekapPerBulanDPP402
                TotalDPP403 += RekapPerBulanDPP403
                TotalDPP409 += RekapPerBulanDPP409
                TotalDPP419 += RekapPerBulanDPP419
                TotalDPP += RekapPerBulanDPP

                TotalTagihan402 += RekapPerBulanPPh402
                TotalTagihan403 += RekapPerBulanPPh403
                TotalTagihan409 += RekapPerBulanPPh409
                TotalTagihan419 += RekapPerBulanPPh419
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
                                    Case KodeSetoran_402
                                        JumlahBayar402 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_403
                                        JumlahBayar403 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_409
                                        JumlahBayar409 += dr.Item("Jumlah_Bayar")
                                    Case KodeSetoran_419
                                        JumlahBayar419 += dr.Item("Jumlah_Bayar")
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

                TotalBayar402 += JumlahBayar402
                TotalBayar403 += JumlahBayar403
                TotalBayar409 += JumlahBayar409
                TotalBayar419 += JumlahBayar419
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
                TotalDPP402, TotalDPP403, TotalDPP409, TotalDPP419, TotalDPP,
                TotalTagihan402, TotalTagihan403, TotalTagihan409, TotalTagihan419, TotalTagihan,
                TotalBayar402, TotalBayar403, TotalBayar409, TotalBayar419, TotalBayar, TotalSisaHutang,
                KodeSetoran_UntukTabel, Kosongan, Kosongan)

            If TotalDPP402 > 0 Then DPP_402.Visibility = Visibility.Visible
            If TotalDPP403 > 0 Then DPP_403.Visibility = Visibility.Visible
            If TotalDPP409 > 0 Then DPP_409.Visibility = Visibility.Visible
            If TotalDPP419 > 0 Then DPP_419.Visibility = Visibility.Visible
            If TotalDPP > 0 Then DPP_.Visibility = Visibility.Visible

            If TotalTagihan402 > 0 Then PPh_402.Visibility = Visibility.Visible
            If TotalTagihan403 > 0 Then PPh_403.Visibility = Visibility.Visible
            If TotalTagihan409 > 0 Then PPh_409.Visibility = Visibility.Visible
            If TotalTagihan419 > 0 Then PPh_419.Visibility = Visibility.Visible

            If TotalBayar402 > 0 Then Jumlah_Bayar_402.Visibility = Visibility.Visible
            If TotalBayar403 > 0 Then Jumlah_Bayar_403.Visibility = Visibility.Visible
            If TotalBayar409 > 0 Then Jumlah_Bayar_409.Visibility = Visibility.Visible
            If TotalBayar419 > 0 Then Jumlah_Bayar_419.Visibility = Visibility.Visible

            VisibilitasInfoSaldo(True)

        End If



        'TAMPILAN DETAIL : -----------------------------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_DETAIL Then

            SumberData = Kosongan

            NomorBulan = KonversiBulanKeNomor_String(MasaPajak)
            Bulan = MasaPajak

            RekapPerBulanDPP402 = 0
            RekapPerBulanDPP403 = 0
            RekapPerBulanDPP409 = 0
            RekapPerBulanDPP419 = 0
            RekapPerBulanDPP = 0

            RekapPerBulanPPh402 = 0
            RekapPerBulanPPh403 = 0
            RekapPerBulanPPh409 = 0
            RekapPerBulanPPh419 = 0
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

                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                      " AND Kode_Setoran                       <> '" & KodeSetoran_419 & "' " & '(Dividen jangan dimasukkan).
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
                        Else
                            NomorFakturPajak = Kosongan
                            NamaJasa = Kosongan
                            DPP = dr.Item("Bagi_Hasil")
                        End If
                        AmbilValue_PerKodeSetoran()
                        TambahBaris()
                    End If
                Loop

                'Dividen :
                cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangDividen " &
                                      " WHERE Jenis_PPh                                 = '" & KonversiJenisPajakKeJenisPPh(JenisPajak) & "' " &
                                      " AND DATE_FORMAT(Tanggal_Akta_Notaris, '%Y-%m')  = '" & TahunPajak & "-" & NomorBulan & "' ",
                                      KoneksiDatabaseTransaksi_Alternatif)
                dr_ExecuteReader()
                Do While dr.Read
                    PPh = dr.Item("PPh_Terutang")
                    If PPh > 0 Then
                        SumberData = JenisJasa_Dividen
                        TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                        TanggalInvoice = TanggalTransaksi
                        NomorInvoice = dr.Item("Nomor_Akta_Notaris")
                        KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                        NamaSupplier = AmbilValue_NamaPemegangSaham(KodeSupplier)
                        NPWP = AmbilValue_NPWPPemegangSaham(KodeSupplier)
                        Keterangan = PenghapusEnter(dr.Item("Keterangan"))
                        NamaJasa = JenisJasa_Dividen
                        DPP = dr.Item("Jumlah_Dividen")
                        AmbilValue_PerKodeSetoran()
                        TambahBaris()
                    End If
                Loop

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
                    RekapPerBulanPPh402, RekapPerBulanPPh403, RekapPerBulanPPh409, RekapPerBulanPPh419, RekapPerBulanPPh)
            End If


        End If



        'JENIS TAMPILAN ALL : --------------------------------------------------------------------------------------------
        If JenisTampilan = JenisTampilan_ALL Then

            SumberData = Kosongan

            NomorBulan = 0

            BukaDatabaseTransaksi_Alternatif(TahunBukuSumberData)
            If StatusKoneksiDatabaseTransaksi_Alternatif = False Then Return

            Do While AmbilAngka(NomorBulan) < 12

                JumlahBarisDalamSatuBulan = 0
                NomorBulan = AmbilAngka(NomorBulan) + 1
                Bulan = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBP & TahunPajak & "-" & NomorBulan.ToString

                RekapPerBulanDPP402 = 0
                RekapPerBulanDPP403 = 0
                RekapPerBulanDPP409 = 0
                RekapPerBulanDPP419 = 0
                RekapPerBulanDPP = 0

                RekapPerBulanPPh402 = 0
                RekapPerBulanPPh403 = 0
                RekapPerBulanPPh409 = 0
                RekapPerBulanPPh419 = 0
                RekapPerBulanPPh = 0

                NomorBulan = KonversiAngkaKeStringDuaDigit(NomorBulan)

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

                    cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                          " WHERE Jenis_Pajak                       = '" & JenisPajak & "' " &
                                          " AND Kode_Setoran                       <> '" & KodeSetoran_419 & "' " & '(Dividen jangan dimasukkan).
                                          " AND Status_Invoice                      = '" & Status_Dibayar & "' " &
                                          " AND Jumlah_Bayar                        > 0 " &
                                          " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        PPh = dr.Item("PPh_Terutang")
                        If PPh > 0 Then
                            SumberData = dr.Item("Peruntukan")
                            KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                            AmbilValue_NamaDanNPWPSupplier()
                            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                            NomorInvoice = dr.Item("Nomor_Invoice")
                            Dim NomorPembelian = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                            Dim cmdPEMB = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                                          " WHERE Nomor_Pembelian = '" & NomorPembelian & "' " &
                                                          " AND Jenis_Produk_Per_Item LIKE '%" & JenisProduk_Jasa & "%' ",
                                                          KoneksiDatabaseTransaksi_Alternatif)
                            Dim drPEMB = cmdPEMB.ExecuteReader
                            drPEMB.Read()
                            If drPEMB.HasRows Then
                                NomorFakturPajak = drPEMB.Item("Nomor_Faktur_Pajak")
                                NamaJasa = drPEMB.Item("Nama_Produk")
                                DPP = PPh / (drPEMB.Item("Tarif_PPh") / 100)
                            Else
                                NomorFakturPajak = Kosongan
                                NamaJasa = Kosongan
                                DPP = dr.Item("Bagi_Hasil")
                            End If
                            AmbilValue_PerKodeSetoran()
                            Keterangan = PenghapusEnter(dr.Item("Catatan"))
                            TambahBaris()
                        End If
                    Loop

                    'Dividen :
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangDividen " &
                                          " WHERE Jenis_PPh                                 = '" & KonversiJenisPajakKeJenisPPh(JenisPajak) & "' " &
                                          " AND DATE_FORMAT(Tanggal_Akta_Notaris, '%Y-%m')  = '" & TahunPajak & "-" & NomorBulan & "' ",
                                          KoneksiDatabaseTransaksi_Alternatif)
                    dr_ExecuteReader()
                    Do While dr.Read
                        PPh = dr.Item("PPh_Terutang")
                        If PPh > 0 Then
                            SumberData = JenisJasa_Dividen
                            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                            TanggalInvoice = TanggalTransaksi
                            NomorInvoice = dr.Item("Nomor_Akta_Notaris")
                            KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                            NamaSupplier = AmbilValue_NamaPemegangSaham(KodeSupplier)
                            NPWP = AmbilValue_NPWPPemegangSaham(KodeSupplier)
                            Keterangan = PenghapusEnter(dr.Item("Keterangan"))
                            NamaJasa = JenisJasa_Dividen
                            DPP = dr.Item("Jumlah_Dividen")
                            AmbilValue_PerKodeSetoran()
                            TambahBaris()
                        End If
                    Loop

                End If

                TotalTagihan += RekapPerBulanPPh

                If JumlahBarisDalamSatuBulan > 0 Then
                    Dim newRow As DataRow = datatabelUtama.NewRow()
                    newRow("Tanggal_Transaksi") = Bulan
                    datatabelUtama.Rows.InsertAt(newRow, datatabelUtama.Rows.Count - JumlahBarisDalamSatuBulan)

                    'Baris REKAP PERBULAN untuk Jenis Tampilan ALL :
                    datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan,
                                            Kosongan, Kosongan, Kosongan, Kosongan,
                                            Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, "Total " & Bulan,
                                            0, 0, 0, 0, RekapPerBulanDPP,
                                            RekapPerBulanPPh402, RekapPerBulanPPh403, RekapPerBulanPPh409, RekapPerBulanPPh419, RekapPerBulanPPh)
                    datatabelUtama.Rows.Add()
                End If

            Loop

            TutupDatabaseTransaksi_Alternatif()

            If datatabelUtama.Rows.Count > 0 Then
                'Baris TOTAL untuk Jenis Tampilan ALL :
                datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan,
                                        Kosongan, Kosongan, Kosongan, Kosongan,
                                        Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, teks_TOTAL_,
                                        0, 0, 0, 0, TotalDPP,
                                        TotalTagihan402, TotalTagihan403, TotalTagihan409, TotalTagihan419, TotalTagihan)
            End If

        End If


        If JenisTampilan = JenisTampilan_ALL Or JenisTampilan = JenisTampilan_REKAP Then
            'Hitung Saldo Akhir Saat Cut Off :
            SisaHutangPajak_SaatCutOff_Public(SisaHutang_SaatCutOff_402, AwalanBP, JenisPajak, KodeSetoran_402)
            SisaHutangPajak_SaatCutOff_Public(SisaHutang_SaatCutOff_403, AwalanBP, JenisPajak, KodeSetoran_403)
            SisaHutangPajak_SaatCutOff_Public(SisaHutang_SaatCutOff_409, AwalanBP, JenisPajak, KodeSetoran_409)
            SisaHutangPajak_SaatCutOff_Public(SisaHutang_SaatCutOff_419, AwalanBP, JenisPajak, KodeSetoran_419)
        End If

        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                'Kode Setoran : 402
                SaldoAkhir_BerdasarkanList_402 = SisaHutang_SaatCutOff_402
                txt_SaldoBerdasarkanList_402.Text = SaldoAkhir_BerdasarkanList_402
                AmbilValue_SaldoAkhirBerdasarkanCOA_402()
                CekKesesuaianSaldoAkhir_402()
                txt_SelisihSaldo_402.Text = SaldoAkhir_BerdasarkanList_402 - SaldoAkhir_BerdasarkanCOA_402
                'Kode Setoran : 403
                SaldoAkhir_BerdasarkanList_403 = SisaHutang_SaatCutOff_403
                txt_SaldoBerdasarkanList_403.Text = SaldoAkhir_BerdasarkanList_403
                AmbilValue_SaldoAkhirBerdasarkanCOA_403()
                CekKesesuaianSaldoAkhir_403()
                txt_SelisihSaldo_403.Text = SaldoAkhir_BerdasarkanList_403 - SaldoAkhir_BerdasarkanCOA_403
                'Kode Setoran : 409
                SaldoAkhir_BerdasarkanList_409 = SisaHutang_SaatCutOff_409
                txt_SaldoBerdasarkanList_409.Text = SaldoAkhir_BerdasarkanList_409
                AmbilValue_SaldoAkhirBerdasarkanCOA_409()
                CekKesesuaianSaldoAkhir_409()
                txt_SelisihSaldo_409.Text = SaldoAkhir_BerdasarkanList_409 - SaldoAkhir_BerdasarkanCOA_409
                'Kode Setoran : 419
                SaldoAkhir_BerdasarkanList_419 = SisaHutang_SaatCutOff_419
                txt_SaldoBerdasarkanList_419.Text = SaldoAkhir_BerdasarkanList_419
                AmbilValue_SaldoAkhirBerdasarkanCOA_419()
                CekKesesuaianSaldoAkhir_419()
                txt_SelisihSaldo_419.Text = SaldoAkhir_BerdasarkanList_419 - SaldoAkhir_BerdasarkanCOA_419
            Case JenisTahunBuku_NORMAL
                'Penjelasan : Variabel-variabel di bawah ini untuk mendapatkan jumlah bayar atas hutang pajak tahun sebelum TahunBukuAktif,
                'tapi dibayarkan pada tahun ini (TahunBukuAktif).
                Dim TotalBayar_UntukHutangPajakTahunSebelumIni_402 As Int64 = 0
                Dim TotalBayar_UntukHutangPajakTahunSebelumIni_403 As Int64 = 0
                Dim TotalBayar_UntukHutangPajakTahunSebelumIni_409 As Int64 = 0
                Dim TotalBayar_UntukHutangPajakTahunSebelumIni_419 As Int64 = 0
                AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_402, TotalBayar_UntukHutangPajakTahunSebelumIni_402)
                AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_403, TotalBayar_UntukHutangPajakTahunSebelumIni_403)
                AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_409, TotalBayar_UntukHutangPajakTahunSebelumIni_409)
                AmbilValue_JumlahBayarUntukHutangPajakTahunKemarin_Public(AwalanBP, KodeSetoran_419, TotalBayar_UntukHutangPajakTahunSebelumIni_419)
                If Not TahunBukuSudahStabil(TahunBukuAktif) Then
                    'Kode Setoran : 402
                    AmbilValue_SaldoAwalBerdasarkanList_402()
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_402()
                    CekKesesuaianSaldoAwal_402()
                    txt_SelisihSaldo_402.Text = SaldoAwal_BerdasarkanList_402 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_402
                    txt_TotalTabel_402.Text = SaldoAwal_BerdasarkanList_402 + TotalTagihan402 - (TotalBayar402 + TotalBayar_UntukHutangPajakTahunSebelumIni_402)
                    'Kode Setoran : 403
                    AmbilValue_SaldoAwalBerdasarkanList_403()
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_403()
                    CekKesesuaianSaldoAwal_403()
                    txt_SelisihSaldo_403.Text = SaldoAwal_BerdasarkanList_403 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_403
                    txt_TotalTabel_403.Text = SaldoAwal_BerdasarkanList_403 + TotalTagihan403 - (TotalBayar403 + TotalBayar_UntukHutangPajakTahunSebelumIni_403)
                    'Kode Setoran : 409
                    AmbilValue_SaldoAwalBerdasarkanList_409()
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_409()
                    CekKesesuaianSaldoAwal_409()
                    txt_SelisihSaldo_409.Text = SaldoAwal_BerdasarkanList_409 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_409
                    txt_TotalTabel_409.Text = SaldoAwal_BerdasarkanList_409 + TotalTagihan409 - (TotalBayar409 + TotalBayar_UntukHutangPajakTahunSebelumIni_409)
                    'Kode Setoran : 419
                    AmbilValue_SaldoAwalBerdasarkanList_419()
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_419()
                    CekKesesuaianSaldoAwal_419()
                    txt_SelisihSaldo_419.Text = SaldoAwal_BerdasarkanList_419 - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_419
                    txt_TotalTabel_419.Text = SaldoAwal_BerdasarkanList_419 + TotalTagihan419 - (TotalBayar419 + TotalBayar_UntukHutangPajakTahunSebelumIni_419)
                Else
                    txt_TotalTabel_402.Text = SaldoAwal_BerdasarkanCOA_402 + TotalTagihan402 - (TotalBayar402 + TotalBayar_UntukHutangPajakTahunSebelumIni_402)
                    txt_TotalTabel_403.Text = SaldoAwal_BerdasarkanCOA_403 + TotalTagihan403 - (TotalBayar403 + TotalBayar_UntukHutangPajakTahunSebelumIni_403)
                    txt_TotalTabel_409.Text = SaldoAwal_BerdasarkanCOA_409 + TotalTagihan409 - (TotalBayar409 + TotalBayar_UntukHutangPajakTahunSebelumIni_409)
                    txt_TotalTabel_419.Text = SaldoAwal_BerdasarkanCOA_419 + TotalTagihan419 - (TotalBayar419 + TotalBayar_UntukHutangPajakTahunSebelumIni_419)
                End If
                txt_TotalTabel_Total.Text _
                    = AmbilAngka(txt_TotalTabel_402.Text) _
                    + AmbilAngka(txt_TotalTabel_403.Text) _
                    + AmbilAngka(txt_TotalTabel_409.Text) _
                    + AmbilAngka(txt_TotalTabel_419.Text)
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
                                RekapPerBulanDPP402, RekapPerBulanDPP403, RekapPerBulanDPP409, RekapPerBulanDPP419, DPP,
                                RekapPerBulanPPh402, RekapPerBulanPPh403, RekapPerBulanPPh409, RekapPerBulanPPh419, JumlahTagihan,
                                JumlahBayar402, JumlahBayar403, JumlahBayar409, JumlahBayar419, JumlahBayar, SisaHutang,
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
            Case KodeSetoran_402 'Pengalihan Tanah dan/atau Bangunan
                DPP402 = DPP
                PPh402 = PPh
                RekapPerBulanDPP402 += DPP
                RekapPerBulanPPh402 += PPh
            Case KodeSetoran_403 'Sewa Tanah dan/atau Bangunan
                DPP403 = DPP
                PPh403 = PPh
                RekapPerBulanDPP403 += DPP
                RekapPerBulanPPh403 += PPh
            Case KodeSetoran_409 'Jasa Konstruksi
                DPP403 = DPP
                PPh409 = PPh
                RekapPerBulanDPP409 += DPP
                RekapPerBulanPPh409 += PPh
            Case KodeSetoran_419 'Dividen
                DPP419 = DPP
                PPh419 = PPh
                RekapPerBulanDPP419 += DPP
                RekapPerBulanPPh419 += PPh
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
                                0, 0, 0, 0, JumlahBayar_KetetapanPajak, SisaHutang_KetetapanPajak)

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
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
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

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        'TrialBalance_Mentahkan()

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
            Pesan_Informasi("Hutang Pajak Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim JumlahTagihan As Int64 = 0
        Dim SisaHutang As Int64 = 0
        Dim JumlahBayar
        Dim KodeSetoran = Kosongan
        JumlahBayar = 0
        Select Case True
            Case rdb_KodeSetoran_402.IsChecked
                If SisaHutang402_Terseleksi <= 0 Then
                    Pesan_Informasi("Hutang " & JenisPajak & " Kode-402 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPh402_Terseleksi
                SisaHutang = SisaHutang402_Terseleksi
                JumlahBayar = JumlahBayar402_Terseleksi
                KodeSetoran = KodeSetoran_402
            Case rdb_KodeSetoran_403.IsChecked
                If SisaHutang403_Terseleksi <= 0 Then
                    Pesan_Informasi("Hutang " & JenisPajak & " Kode-403 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPh403_Terseleksi
                SisaHutang = SisaHutang403_Terseleksi
                JumlahBayar = JumlahBayar403_Terseleksi
                KodeSetoran = KodeSetoran_403
            Case rdb_KodeSetoran_409.IsChecked
                If SisaHutang409_Terseleksi <= 0 Then
                    Pesan_Informasi("Hutang " & JenisPajak & " Kode-409 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPh409_Terseleksi
                SisaHutang = SisaHutang409_Terseleksi
                JumlahBayar = JumlahBayar409_Terseleksi
                KodeSetoran = KodeSetoran_409
            Case rdb_KodeSetoran_419.IsChecked
                If SisaHutang419_Terseleksi <= 0 Then
                    Pesan_Informasi("Hutang " & JenisPajak & " Kode-419 Bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
                    Return
                End If
                JumlahTagihan = PPh419_Terseleksi
                SisaHutang = SisaHutang419_Terseleksi
                JumlahBayar = JumlahBayar419_Terseleksi
                KodeSetoran = KodeSetoran_419
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
        DPP_Terseleksi = AmbilAngka(rowviewUtama("DPP_"))
        PPh402_Terseleksi = AmbilAngka(rowviewUtama("PPh_402"))
        PPh403_Terseleksi = AmbilAngka(rowviewUtama("PPh_403"))
        PPh409_Terseleksi = AmbilAngka(rowviewUtama("PPh_409"))
        PPh419_Terseleksi = AmbilAngka(rowviewUtama("PPh_419"))
        PPh_Terseleksi = AmbilAngka(rowviewUtama("PPh_"))
        JumlahTagihan_Terseleksi = AmbilAngka(rowviewUtama("PPh_"))
        JumlahBayar402_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_402"))
        JumlahBayar403_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_403"))
        JumlahBayar409_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_409"))
        JumlahBayar419_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_419"))
        JumlahBayar_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar_Pajak"))
        SisaHutang402_Terseleksi = PPh402_Terseleksi - JumlahBayar402_Terseleksi
        SisaHutang403_Terseleksi = PPh403_Terseleksi - JumlahBayar403_Terseleksi
        SisaHutang409_Terseleksi = PPh409_Terseleksi - JumlahBayar409_Terseleksi
        SisaHutang419_Terseleksi = PPh419_Terseleksi - JumlahBayar419_Terseleksi
        SisaHutang_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Hutang_Pajak"))
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
                rdb_KodeSetoran_402.IsEnabled = False
                rdb_KodeSetoran_403.IsEnabled = False
                rdb_KodeSetoran_409.IsEnabled = False
                rdb_KodeSetoran_419.IsEnabled = False
                If PPh402_Terseleksi > 0 Then rdb_KodeSetoran_402.IsEnabled = True
                If PPh403_Terseleksi > 0 Then rdb_KodeSetoran_403.IsEnabled = True
                If PPh409_Terseleksi > 0 Then rdb_KodeSetoran_409.IsEnabled = True
                If PPh419_Terseleksi > 0 Then rdb_KodeSetoran_419.IsEnabled = True
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


    Private Sub rdb_KodeSetoran_402_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_402.Checked
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_403_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_403.Checked
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_409_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_409.Checked
        LogikaKodeSetoran()
    End Sub
    Private Sub rdb_KodeSetoran_419_Checked(sender As Object, e As RoutedEventArgs) Handles rdb_KodeSetoran_419.Checked
        LogikaKodeSetoran()
    End Sub
    Sub LogikaKodeSetoran()
        If rdb_KodeSetoran_402.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_402
        If rdb_KodeSetoran_403.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_403
        If rdb_KodeSetoran_409.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_409
        If rdb_KodeSetoran_419.IsChecked = True Then KodeSetoran_UntukBayar = KodeSetoran_419
        If rdb_KodeSetoran_402.IsChecked = True _
            Or rdb_KodeSetoran_403.IsChecked = True _
            Or rdb_KodeSetoran_409.IsChecked = True _
            Or rdb_KodeSetoran_419.IsChecked = True _
            Then
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

        If Not TanyaKonfirmasi("Yakin ingin menghapus laporan terpilih?" & Enter2Baris &
                                  "Catatan :" & Enter1Baris &
                                  "Data invoice tidak akan terhapus pada event ini.") Then Return

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
        rdb_KodeSetoran_402.IsChecked = False
        rdb_KodeSetoran_403.IsChecked = False
        rdb_KodeSetoran_409.IsChecked = False
        rdb_KodeSetoran_419.IsChecked = False
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
    Dim DPP_402 As New DataGridTextColumn
    Dim DPP_403 As New DataGridTextColumn
    Dim DPP_409 As New DataGridTextColumn
    Dim DPP_419 As New DataGridTextColumn
    Dim DPP_ As New DataGridTextColumn
    Dim PPh_402 As New DataGridTextColumn
    Dim PPh_403 As New DataGridTextColumn
    Dim PPh_409 As New DataGridTextColumn
    Dim PPh_419 As New DataGridTextColumn
    Dim PPh_ As New DataGridTextColumn
    Dim Jumlah_Bayar_402 As New DataGridTextColumn
    Dim Jumlah_Bayar_403 As New DataGridTextColumn
    Dim Jumlah_Bayar_409 As New DataGridTextColumn
    Dim Jumlah_Bayar_419 As New DataGridTextColumn
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
        datatabelUtama.Columns.Add("DPP_402", GetType(Int64))
        datatabelUtama.Columns.Add("DPP_403", GetType(Int64))
        datatabelUtama.Columns.Add("DPP_409", GetType(Int64))
        datatabelUtama.Columns.Add("DPP_419", GetType(Int64))
        datatabelUtama.Columns.Add("DPP_", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_402", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_403", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_409", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_419", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar_402", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar_403", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar_409", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar_419", GetType(Int64))
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
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_402, "DPP_402", "DPP-402", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_403, "DPP_403", "DPP-403", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_409, "DPP_409", "DPP-409", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_419, "DPP_419", "DPP-419", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_, "DPP_", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_402, "PPh_402", "PPh-402", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_403, "PPh_403", "PPh-403", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_409, "PPh_409", "PPh-409", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_419, "PPh_419", "PPh-419", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_, "PPh_", "PPh", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar_402, "Jumlah_Bayar_402", "Jumlah Bayar" & Enter1Baris & "402", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar_403, "Jumlah_Bayar_403", "Jumlah Bayar" & Enter1Baris & "403", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar_409, "Jumlah_Bayar_409", "Jumlah Bayar" & Enter1Baris & "409", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar_419, "Jumlah_Bayar_419", "Jumlah Bayar" & Enter1Baris & "419", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
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
        '402
        txt_SaldoBerdasarkanList_402.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_402.IsReadOnly = True
        txt_SelisihSaldo_402.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_402.IsReadOnly = True
        txt_AJP_402.IsReadOnly = True
        txt_TotalTabel_402.IsReadOnly = True
        '403
        txt_SaldoBerdasarkanList_403.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_403.IsReadOnly = True
        txt_SelisihSaldo_403.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_403.IsReadOnly = True
        txt_AJP_403.IsReadOnly = True
        txt_TotalTabel_403.IsReadOnly = True
        '409
        txt_SaldoBerdasarkanList_409.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_409.IsReadOnly = True
        txt_SelisihSaldo_409.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_409.IsReadOnly = True
        txt_AJP_409.IsReadOnly = True
        txt_TotalTabel_409.IsReadOnly = True
        '419
        txt_SaldoBerdasarkanList_419.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA_419.IsReadOnly = True
        txt_SelisihSaldo_419.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian_419.IsReadOnly = True
        txt_AJP_419.IsReadOnly = True
        txt_TotalTabel_419.IsReadOnly = True
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
    'Kode Setoran : 402

    Public KesesuaianSaldoAwal_402 As Boolean
    Public KesesuaianSaldoAkhir_402 As Boolean
    Public KesesuaianJurnal_402 As Boolean

    Dim SaldoAwal_BerdasarkanList_402
    Dim SaldoAwal_BerdasarkanCOA_402
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_402
    Dim SaldoAkhir_BerdasarkanList_402
    Dim SaldoAkhir_BerdasarkanCOA_402
    Dim JumlahPenyesuaianSaldo_402

    Sub AmbilValue_SaldoAwalBerdasarkanList_402()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_402, SaldoAwal_BerdasarkanList_402, txt_SaldoBerdasarkanList_402)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_402()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutangPajak_402, SaldoAwal_BerdasarkanCOA_402, JumlahPenyesuaianSaldo_402, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_402,
                                                                  txt_SaldoAwalBerdasarkanCOA_402, txt_AJP_402, txt_saldoBerdasarkanCOA_PlusPenyesuaian_402)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_402()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutangPajak_402, SaldoAkhir_BerdasarkanCOA_402, txt_saldoBerdasarkanCOA_PlusPenyesuaian_402)
    End Sub

    Sub CekKesesuaianSaldoAwal_402()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_402, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_402, KesesuaianSaldoAwal_402,
                                      btn_Sesuaikan_402, txt_SaldoBerdasarkanList_402, txt_saldoBerdasarkanCOA_PlusPenyesuaian_402, txt_SelisihSaldo_402)
    End Sub

    Sub CekKesesuaianSaldoAkhir_402()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_402, SaldoAkhir_BerdasarkanCOA_402, KesesuaianSaldoAkhir_402,
                                      btn_Sesuaikan_402, txt_SaldoBerdasarkanList_402, txt_saldoBerdasarkanCOA_PlusPenyesuaian_402, txt_SelisihSaldo_402)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_402_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_402.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_402)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_402_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_402.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_402)
    End Sub

    Private Sub txt_SelisihSaldo_402_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_402.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_402)
    End Sub

    Private Sub btn_Sesuaikan_402_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_402.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutangPajak_402, SaldoAwal_BerdasarkanList_402, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_402)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_402_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_402.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_402)
    End Sub

    Private Sub txt_AJP_402_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_402.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_402)
    End Sub

    Private Sub txt_TotalTabel_402_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_402.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_402)
    End Sub

    '=======================================================================================================================================



    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================
    'Kode Setoran : 403

    Public KesesuaianSaldoAwal_403 As Boolean
    Public KesesuaianSaldoAkhir_403 As Boolean
    Public KesesuaianJurnal_403 As Boolean

    Dim SaldoAwal_BerdasarkanList_403
    Dim SaldoAwal_BerdasarkanCOA_403
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_403
    Dim SaldoAkhir_BerdasarkanList_403
    Dim SaldoAkhir_BerdasarkanCOA_403
    Dim JumlahPenyesuaianSaldo_403

    Sub AmbilValue_SaldoAwalBerdasarkanList_403()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_403, SaldoAwal_BerdasarkanList_403, txt_SaldoBerdasarkanList_403)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_403()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutangPajak_403, SaldoAwal_BerdasarkanCOA_403, JumlahPenyesuaianSaldo_403, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_403,
                                                                  txt_SaldoAwalBerdasarkanCOA_403, txt_AJP_403, txt_saldoBerdasarkanCOA_PlusPenyesuaian_403)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_403()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutangPajak_403, SaldoAkhir_BerdasarkanCOA_403, txt_saldoBerdasarkanCOA_PlusPenyesuaian_403)
    End Sub

    Sub CekKesesuaianSaldoAwal_403()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_403, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_403, KesesuaianSaldoAwal_403,
                                      btn_Sesuaikan_403, txt_SaldoBerdasarkanList_403, txt_saldoBerdasarkanCOA_PlusPenyesuaian_403, txt_SelisihSaldo_403)
    End Sub

    Sub CekKesesuaianSaldoAkhir_403()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_403, SaldoAkhir_BerdasarkanCOA_403, KesesuaianSaldoAkhir_403,
                                      btn_Sesuaikan_403, txt_SaldoBerdasarkanList_403, txt_saldoBerdasarkanCOA_PlusPenyesuaian_403, txt_SelisihSaldo_403)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_403_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_403.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_403)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_403_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_403.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_403)
    End Sub

    Private Sub txt_SelisihSaldo_403_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_403.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_403)
    End Sub

    Private Sub btn_Sesuaikan_403_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_403.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutangPajak_403, SaldoAwal_BerdasarkanList_403, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_403)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_403_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_403.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_403)
    End Sub

    Private Sub txt_AJP_403_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_403.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_403)
    End Sub

    Private Sub txt_TotalTabel_403_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_403.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_403)
    End Sub

    '=======================================================================================================================================




    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================
    'Kode Setoran : 409

    Public KesesuaianSaldoAwal_409 As Boolean
    Public KesesuaianSaldoAkhir_409 As Boolean
    Public KesesuaianJurnal_409 As Boolean

    Dim SaldoAwal_BerdasarkanList_409
    Dim SaldoAwal_BerdasarkanCOA_409
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_409
    Dim SaldoAkhir_BerdasarkanList_409
    Dim SaldoAkhir_BerdasarkanCOA_409
    Dim JumlahPenyesuaianSaldo_409

    Sub AmbilValue_SaldoAwalBerdasarkanList_409()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_409, SaldoAwal_BerdasarkanList_409, txt_SaldoBerdasarkanList_409)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_409()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutangPajak_409, SaldoAwal_BerdasarkanCOA_409, JumlahPenyesuaianSaldo_409, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_409,
                                                                  txt_SaldoAwalBerdasarkanCOA_409, txt_AJP_409, txt_saldoBerdasarkanCOA_PlusPenyesuaian_409)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_409()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutangPajak_409, SaldoAkhir_BerdasarkanCOA_409, txt_saldoBerdasarkanCOA_PlusPenyesuaian_409)
    End Sub

    Sub CekKesesuaianSaldoAwal_409()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_409, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_409, KesesuaianSaldoAwal_409,
                                      btn_Sesuaikan_409, txt_SaldoBerdasarkanList_409, txt_saldoBerdasarkanCOA_PlusPenyesuaian_409, txt_SelisihSaldo_409)
    End Sub

    Sub CekKesesuaianSaldoAkhir_409()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_409, SaldoAkhir_BerdasarkanCOA_409, KesesuaianSaldoAkhir_409,
                                      btn_Sesuaikan_409, txt_SaldoBerdasarkanList_409, txt_saldoBerdasarkanCOA_PlusPenyesuaian_409, txt_SelisihSaldo_409)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_409_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_409.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_409)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_409_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_409.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_409)
    End Sub

    Private Sub txt_SelisihSaldo_409_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_409.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_409)
    End Sub

    Private Sub btn_Sesuaikan_409_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_409.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutangPajak_409, SaldoAwal_BerdasarkanList_409, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_409)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_409_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_409.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_409)
    End Sub

    Private Sub txt_AJP_409_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_409.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_409)
    End Sub

    Private Sub txt_TotalTabel_409_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_409.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_409)
    End Sub

    '=======================================================================================================================================




    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================
    'Kode Setoran : 419

    Public KesesuaianSaldoAwal_419 As Boolean
    Public KesesuaianSaldoAkhir_419 As Boolean
    Public KesesuaianJurnal_419 As Boolean

    Dim SaldoAwal_BerdasarkanList_419
    Dim SaldoAwal_BerdasarkanCOA_419
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_419
    Dim SaldoAkhir_BerdasarkanList_419
    Dim SaldoAkhir_BerdasarkanCOA_419
    Dim JumlahPenyesuaianSaldo_419

    Sub AmbilValue_SaldoAwalBerdasarkanList_419()
        AmbilValue_SaldoAwalBerdasarkanList(KodeSetoran_419, SaldoAwal_BerdasarkanList_419, txt_SaldoBerdasarkanList_419)
    End Sub

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_419()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutangPajak_419, SaldoAwal_BerdasarkanCOA_419, JumlahPenyesuaianSaldo_419, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_419,
                                                                  txt_SaldoAwalBerdasarkanCOA_419, txt_AJP_419, txt_saldoBerdasarkanCOA_PlusPenyesuaian_419)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA_419()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutangPajak_419, SaldoAkhir_BerdasarkanCOA_419, txt_saldoBerdasarkanCOA_PlusPenyesuaian_419)
    End Sub

    Sub CekKesesuaianSaldoAwal_419()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList_419, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_419, KesesuaianSaldoAwal_419,
                                      btn_Sesuaikan_419, txt_SaldoBerdasarkanList_419, txt_saldoBerdasarkanCOA_PlusPenyesuaian_419, txt_SelisihSaldo_419)
    End Sub

    Sub CekKesesuaianSaldoAkhir_419()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList_419, SaldoAkhir_BerdasarkanCOA_419, KesesuaianSaldoAkhir_419,
                                      btn_Sesuaikan_419, txt_SaldoBerdasarkanList_419, txt_saldoBerdasarkanCOA_PlusPenyesuaian_419, txt_SelisihSaldo_419)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_419_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList_419.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList_419)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_419_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian_419.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian_419)
    End Sub

    Private Sub txt_SelisihSaldo_419_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo_419.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo_419)
    End Sub

    Private Sub btn_Sesuaikan_419_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan_419.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutangPajak_419, SaldoAwal_BerdasarkanList_419, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian_419)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_419_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA_419.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA_419)
    End Sub

    Private Sub txt_AJP_419_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP_419.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP_419)
    End Sub

    Private Sub txt_TotalTabel_419_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_419.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_419)
    End Sub

    '=======================================================================================================================================


    Private Sub txt_TotalTabel_Total_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel_Total.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel_Total)
    End Sub


End Class
