Imports System.Data.Odbc
Imports bcomm
Imports System.Windows
Imports System.Windows.Controls


Public Class wpfWin_DorongInvoiceKeJurnal

    Dim NomorFakturPajak
    Dim NomorJV_Penjualan
    Dim NomorJV_Closing
    Dim AngkaInvoice
    Public NomorInvoice
    Public NP
    Dim TanggalInvoice
    Dim NomorPenjualan
    Dim JenisProduk_Induk
    Dim KodeCustomer
    Dim NamaCustomer
    Dim KodeMataUang As String
    Dim Kurs As Decimal
    Dim JumlahHargaKeseluruhan_MUA As Decimal
    Dim HargaJualEkspor_MUA As Decimal
    Dim TotalTagihan As Int64
    Dim TotalTagihan_MUA As Decimal
    Dim TotalTagihan_Asing As Decimal
    Dim JumlahPiutangUsaha As Int64
    Dim BiayaTransportasi As Int64
    Dim BiayaTransportasi_MUA As Decimal
    Dim Freight As Decimal
    Dim BiayaAsuransiPenjualan_MUA As Decimal
    Dim Insurance As Decimal
    Dim BiayaDibayarDimuka As Int64
    Dim BiayaDibayarDimuka_MUA As Decimal
    Dim DPPBarang As Int64
    Dim DPPJasa As Int64
    Dim DPP As Int64
    Dim JenisPPN
    Dim PerlakuanPPN
    Dim PPN
    Dim TarifPPN As Decimal
    Dim JenisPPh
    Dim PPhTerutang
    Dim PPhDipotong
    Dim KodeSetoran
    Dim Catatan

    Public JualAsset As Boolean
    Dim Asset As Integer
    Dim KelompokHarta

    Dim JenisPenjualan

    Dim PembayaranViaBank As Boolean
    Dim BiayaAdministrasiBank
    Dim DitanggungOleh
    Dim JumlahTransfer

    Dim JumlahDebet

    Dim COADebet
    Dim COAPenjualanBarangAtauAsset
    Dim COAJasa

    Dim TotalHarga As Int64

    'Variabel-variabel untuk Termin
    Dim MetodePembayaran
    Dim BasisPerhitunganTermin
    Dim TahapTermin
    Dim DPPBarangTermin As Int64
    Dim DPPJasaTermin As Int64
    Dim DPPTermin As Int64
    Dim TerminPersen As Decimal
    Dim DebetPelunasan As Int64
    Dim HargaJualEksporTermin_MUA As Decimal

    Dim DebetPelunasan_MUA As Decimal

    Dim MitraLuarNegeri As Boolean

    Dim DenganNomorFaktur As Boolean


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        LoadingForm()
        If MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then
            MitraLuarNegeri = True
            DenganNomorFaktur = True
            lbl_SilakanIsiNomorFaktur.Text = "Silakan isi Nomor PEB :"
            TotalTagihan_MUA = TotalTagihan_Asing
            BiayaTransportasi_MUA = Freight
            BiayaAsuransiPenjualan_MUA = Insurance
            BiayaDibayarDimuka_MUA = BiayaTransportasi_MUA + BiayaAsuransiPenjualan_MUA
            HargaJualEkspor_MUA = TotalTagihan_MUA - BiayaDibayarDimuka_MUA
            HargaJualEksporTermin_MUA = HargaJualEkspor_MUA
        Else
            MitraLuarNegeri = False
            If JenisPPN = JenisPPN_NonPPN Then
                DenganNomorFaktur = False
            Else
                DenganNomorFaktur = True
            End If
            BiayaDibayarDimuka = BiayaTransportasi
        End If

        If DenganNomorFaktur Then
            lbl_SilakanIsiNomorFaktur.IsEnabled = True
            txt_NomorFakturPajak.IsEnabled = True
        Else
            lbl_SilakanIsiNomorFaktur.IsEnabled = False
            txt_NomorFakturPajak.IsEnabled = False
        End If

        ProsesLoadingForm = False

    End Sub


    Sub LoadingForm()

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                   " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        DPPBarang = 0
        DPPJasa = 0
        Do While dr.Read()
            AngkaInvoice = dr.Item("Angka_Invoice")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            NomorPenjualan = dr.Item("Nomor_Penjualan")
            JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            TotalHarga = dr.Item("Jumlah_Harga_Keseluruhan") - dr.Item("Diskon")
            MetodePembayaran = dr.Item("Metode_Pembayaran")
            BasisPerhitunganTermin = dr.Item("Basis_Perhitungan_Termin")
            TahapTermin = dr.Item("Tahap_Termin")
            TerminPersen = dr.Item("Termin")
            If dr.Item("Jenis_Produk_Per_Item") = JenisProduk_Barang Then
                DPPBarang += dr.Item("Total_Harga_Per_Item")
            Else
                DPPJasa += dr.Item("Total_Harga_Per_Item")
            End If
            DPP = DPPBarang + DPPJasa
            DPPBarangTermin = dr.Item("DPP_Barang")
            DPPJasaTermin = dr.Item("DPP_Jasa")
            DPPTermin = dr.Item("Dasar_Pengenaan_Pajak")
            JumlahHargaKeseluruhan_MUA = dr.Item("Jumlah_Harga_Keseluruhan")
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
            TarifPPN = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPN"))
            PPN = dr.Item("PPN")
            If PerlakuanPPN <> PerlakuanPPN_Dibayar Then PPN = 0
            JenisPPh = dr.Item("Jenis_PPh")
            KodeSetoran = dr.Item("Kode_Setoran")
            PPhTerutang = dr.Item("PPh_Terutang")
            PPhDipotong = dr.Item("PPh_Dipotong")
            TotalTagihan = dr.Item("Total_Tagihan")
            JumlahPiutangUsaha = dr.Item("Jumlah_Piutang_Usaha")
            JenisPenjualan = dr.Item("Jenis_Penjualan")
            COADebet = dr.Item("COA_Debet")
            BiayaAdministrasiBank = dr.Item("Biaya_Administrasi_Bank")
            DitanggungOleh = dr.Item("Ditanggung_Oleh")
            BiayaTransportasi = dr.Item("Biaya_Transportasi")
            Catatan = dr.Item("Catatan")
            Asset = dr.Item("Asset")
            'Penjualan Ekspor :
            KodeMataUang = dr.Item("Kode_Mata_Uang")
            Kurs = dr.Item("Kurs")
            TotalTagihan_Asing = dr.Item("Total_Tagihan")
            Freight = dr.Item("Freight")
            Insurance = dr.Item("Insurance")
        Loop
        AksesDatabase_Transaksi(Tutup)

        If COATermasukBank(COADebet) Then
            PembayaranViaBank = True
            Perhitungan_ValueBank()
        Else
            PembayaranViaBank = False
        End If

        If Asset = 1 Then JualAsset = True
        If Asset = 0 Then JualAsset = False

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DataAsset WHERE Kode_Closing = '" & NomorPenjualan & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            KelompokHarta = KonversiAngkaKeKelompokHarta(dr.Item("Kelompok_Harta"))
        End If
        AksesDatabase_General(Tutup)

    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        MitraLuarNegeri = False
        DenganNomorFaktur = False
        txt_NomorFakturPajak.Text = Kosongan
        NomorJV_Penjualan = 0
        NomorInvoice = Kosongan
        MetodePembayaran = Kosongan
        BasisPerhitunganTermin = Kosongan
        JualAsset = False
        Asset = 0
        KelompokHarta = Kosongan
        COADebet = Kosongan
        TahapTermin = Kosongan
        DPPTermin = 0
        TerminPersen = 0
        PembayaranViaBank = False
        lbl_SilakanIsiNomorFaktur.Text = "Silakan isi Nomor Faktur Pajak :"
        Kurs = 1
        HargaJualEkspor_MUA = 0
        BiayaTransportasi = 0
        BiayaTransportasi_MUA = 0
        Freight = 0
        BiayaAsuransiPenjualan_MUA = 0
        Insurance = 0
        BiayaDibayarDimuka = 0
        BiayaDibayarDimuka_MUA = 0

        ProsesResetForm = False
    End Sub


    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub


    Sub Perhitungan_ValueBank()
        If PembayaranViaBank = True Then
            If DitanggungOleh = DitanggungOleh_LawanTransaksi Then
                JumlahTransfer = TotalTagihan
            End If
            If DitanggungOleh = DitanggungOleh_Perusahaan Then
                JumlahTransfer = TotalTagihan - BiayaAdministrasiBank
            End If
            If DitanggungOleh = Kosongan Then 'Ini akan terjadi jika SARANA PEMBAYARAN <> BANK
                JumlahTransfer = TotalTagihan
            End If
        End If
    End Sub


    Private Sub btn_DorongKeJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_DorongKeJurnal.Click
        DorongKeJurnal()
    End Sub


    Sub DorongKeJurnal()

        If DenganNomorFaktur Then

            If NomorFakturPajak = Kosongan Then
                PesanPeringatan("Silakan isi kolom 'Nomor Faktur Pajak'.")
                txt_NomorFakturPajak.Focus()
                Return
            End If

        End If


        '====================================================================================
        'PENYIMPANAN JURNAL : Hanya untuk Jenis Tahun Buku Normal.                          |
        '====================================================================================

        If PerlakuanPPN <> PerlakuanPPN_Dibayar Then PPN = 0

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then

            If JualAsset Then
                If KelompokHarta = KelompokHarta_Tanah Then
                    COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanAssetTanahBangunan
                Else
                    COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanAssetLainnya
                End If
            Else
                COAPenjualanBarangAtauAsset = KodeTautanCOA_PenjualanBarang_Manufaktur
                If JenisProduk_Induk = JenisProduk_JasaKonstruksi Then
                    COAJasa = KodeTautanCOA_PenjualanJasaKonstruksi
                Else
                    COAJasa = KodeTautanCOA_PenjualanJasa
                End If
            End If

            If JenisPPN = JenisPPN_Include Then
                Dim RasioDPPBarang As Decimal = DPPBarang / TotalHarga
                DPP = HitungDPPUntukPPNInclude(TotalHarga, TarifPPN)
                DPPBarang = DPP * RasioDPPBarang
                DPPJasa = DPP - DPPBarang
            End If

            Dim PiutangUsaha_Ekspor_MUA As Decimal
            If MetodePembayaran = MetodePembayaran_Termin Then
                If TahapTermin = TahapTermin_Pelunasan Then
                    DebetPelunasan = DPP - DPPTermin '(Ini belum dicoba untuk PPN Include!)
                    DPPTermin = 0
                    'Asing :
                    HargaJualEkspor_MUA = JumlahHargaKeseluruhan_MUA
                    DebetPelunasan_MUA = HargaJualEkspor_MUA - HargaJualEksporTermin_MUA
                    HargaJualEksporTermin_MUA = 0
                Else
                    DebetPelunasan = 0
                    DPPBarang = 0
                    DPPJasa = 0
                    'Asing :
                    DebetPelunasan_MUA = 0
                    HargaJualEkspor_MUA = 0
                End If
            Else
                DebetPelunasan = 0
                DPPTermin = 0
                'Asing
                DebetPelunasan_MUA = 0
                HargaJualEksporTermin_MUA = 0
            End If
            PiutangUsaha_Ekspor_MUA = TotalTagihan_MUA

            Dim COAPiutangUsahaEkspor As String = PenentuanCOA_PiutangUsahaEkspor_BerdasarkanKodeMataUang(KodeMataUang)
            Dim COA_BiayaDibayarDimuka_MUA As String = Kosongan
            Dim COA_UangMukaPenjualan_Ekspor_MUA = Kosongan

            Select Case KodeMataUang
                Case KodeMataUang_USD
                    COA_BiayaDibayarDimuka_MUA = KodeTautanCOA_BiayaDibayarDimuka_USD
                    COA_UangMukaPenjualan_Ekspor_MUA = KodeTautanCOA_UangMukaPenjualan_Ekspor_USD
                Case KodeMataUang_AUD
                    COA_BiayaDibayarDimuka_MUA = KodeTautanCOA_BiayaDibayarDimuka_AUD
                    COA_UangMukaPenjualan_Ekspor_MUA = KodeTautanCOA_UangMukaPenjualan_Ekspor_AUD
                Case KodeMataUang_JPY
                    COA_BiayaDibayarDimuka_MUA = KodeTautanCOA_BiayaDibayarDimuka_JPY
                    COA_UangMukaPenjualan_Ekspor_MUA = KodeTautanCOA_UangMukaPenjualan_Ekspor_JPY
                Case KodeMataUang_CNY
                    COA_BiayaDibayarDimuka_MUA = KodeTautanCOA_BiayaDibayarDimuka_CNY
                    COA_UangMukaPenjualan_Ekspor_MUA = KodeTautanCOA_UangMukaPenjualan_Ekspor_CNY
                Case KodeMataUang_EUR
                    COA_BiayaDibayarDimuka_MUA = KodeTautanCOA_BiayaDibayarDimuka_EUR
                    COA_UangMukaPenjualan_Ekspor_MUA = KodeTautanCOA_UangMukaPenjualan_Ekspor_EUR
                Case KodeMataUang_SGD
                    COA_BiayaDibayarDimuka_MUA = KodeTautanCOA_BiayaDibayarDimuka_SGD
                    COA_UangMukaPenjualan_Ekspor_MUA = KodeTautanCOA_UangMukaPenjualan_Ekspor_SGD
                Case KodeMataUang_GBP
                    COA_BiayaDibayarDimuka_MUA = KodeTautanCOA_BiayaDibayarDimuka_GBP
                    COA_UangMukaPenjualan_Ekspor_MUA = KodeTautanCOA_UangMukaPenjualan_Ekspor_GBP
            End Select

            If MitraLuarNegeri Then
                JurnalAdjusment_Forex(COAPiutangUsahaEkspor, TanggalInvoice)
                If BiayaDibayarDimuka_MUA > 0 Then JurnalAdjusment_Forex(COA_BiayaDibayarDimuka_MUA, TanggalInvoice)
                If DebetPelunasan_MUA > 0 Or HargaJualEksporTermin_MUA > 0 Then JurnalAdjusment_Forex(COA_UangMukaPenjualan_Ekspor_MUA, TanggalInvoice)
            End If

            ResetValueJurnal()
            SistemPenomoranOtomatis_NomorJV()
            NomorJV_Penjualan = jur_NomorJV
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoice)
            If JualAsset Then
                jur_JenisJurnal = JenisJurnal_Asset
            Else
                If MitraLuarNegeri Then
                    jur_JenisJurnal = JenisJurnal_PenjualanEkspor
                Else
                    jur_JenisJurnal = JenisJurnal_Penjualan
                End If
            End If
            jur_TanggalInvoice = TanggalInvoice 'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
            jur_NomorInvoice = NomorInvoice
            jur_NamaProduk = AmbilValue_ListProdukBerdasarkanInvoicePenjualan(NomorInvoice)
            jur_NomorFakturPajak = NomorFakturPajak
            jur_KodeLawanTransaksi = KodeCustomer
            jur_NamaLawanTransaksi = NamaCustomer
            jur_KodeMataUang = KodeMataUang
            jur_Kurs = Kurs
            jur_UraianTransaksi = Catatan
            jur_Direct = 0

            'Jika N/P = Normal :
            '--------------------
            If NP = "N" Then

                If JenisPenjualan = JenisPenjualan_Tunai Then
                    JumlahDebet = TotalTagihan
                    JumlahPiutangUsaha = 0
                Else
                    JumlahDebet = JumlahPiutangUsaha
                    PPhTerutang = 0
                    PPhDipotong = 0
                    'Penjelasan : PPh Terutang dan PPh Dipotong untuk transaksi Tempo (Non-Tunai)...
                    '...nanti dimunculkannya saat Jurnal Pencairan Piutang secara proporsional.
                End If

                'Eliminasi Baris Jurnal Tertentu :
                If JualAsset = False Then PPhTerutang = 0

                'Simpan Jurnal :
                If Not MitraLuarNegeri Then
                    ___jurDebetBankCashIN(DitanggungOleh, COADebet, JumlahDebet, JumlahTransfer, BiayaAdministrasiBank)
                    ___jurDebet(KodeTautanCOA_UangMukaPenjualan, DebetPelunasan)
                    ___jurDebet(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang)
                    ___jurDebet(PenentuanCOA_PPhDibayarDimuka_BP_BelumDiterima(JenisPPh), PPhDipotong) '(PPh Prepaid)
                    _______jurKredit(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang)
                    _______jurKredit(KodeTautanCOA_PPNKeluaran, PPN)
                    _______jurKredit(COAPenjualanBarangAtauAsset, DPPBarang)
                    _______jurKredit(COAJasa, DPPJasa)
                    _______jurKredit(KodeTautanCOA_UangMukaPenjualan, DPPTermin)
                    _______jurKredit(KodeTautanCOA_BiayaDibayarDimuka, BiayaDibayarDimuka)
                Else
                    ___jurDebet(COA_UangMukaPenjualan_Ekspor_MUA, DebetPelunasan_MUA)
                    ___jurDebet(COAPiutangUsahaEkspor, PiutangUsaha_Ekspor_MUA)
                    _______jurKredit(KodeTautanCOA_PenjualanEkspor, HargaJualEkspor_MUA)
                    _______jurKredit(COA_BiayaDibayarDimuka_MUA, BiayaDibayarDimuka_MUA)
                    _______jurKredit(COA_UangMukaPenjualan_Ekspor_MUA, HargaJualEksporTermin_MUA)
                    KoreksiSelisihJurnal(jur_NomorJV) 'Ini harus disimpan langsung di ujung penyimpanan Jurnal, tidak boleh diseling oleh baris kode yang lain
                End If

            End If

            'Jika N/P = Pembetulan :
            '-----------------------
            If NP <> "N" Then

                'PENYESUAIAN VALUE :
                Dim NomorInvoiceLama = Kosongan
                Dim DPPBarang_InvoiceLama
                Dim DPPJasa_InvoiceLama
                Dim PPN_InvoiceLama
                Dim PPhTerutang_InvoiceLama
                Dim BiayaTransportasiPenjualan_InvoiceLama
                Dim TotalTagihan_InvoiceLama
                Dim JumlahPiutangUsaha_InvoiceLama
                Dim DPPBarang_Selisih
                Dim DPPJasa_Selisih
                Dim PPN_Selisih
                Dim PPhTerutang_Selisih
                Dim BiayaTransportasiPenjualan_Selisih
                Dim TotalTagihan_Selisih
                Dim JumlahPiutangUsaha_Selisih

                If NP = "P1" Then
                    NomorInvoiceLama = NomorInvoice.Substring(0, NomorInvoice.Length - 3)
                Else
                    Dim PembetulanKe As Integer = AmbilAngka(NP)
                    Dim NPLama = "P" & (PembetulanKe - 1)
                    NomorInvoiceLama = Replace(NomorInvoice, NP, NPLama)
                End If

                'Ambil Value dari Data Lama :
                AksesDatabase_Transaksi(Buka)
                cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                           " WHERE Nomor_Invoice = '" & NomorInvoiceLama & "' ",
                                           KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                DPPBarang_InvoiceLama = 0
                DPPJasa_InvoiceLama = 0
                PPN_InvoiceLama = 0
                PPhTerutang_InvoiceLama = 0
                BiayaTransportasiPenjualan_InvoiceLama = 0
                TotalTagihan_InvoiceLama = 0
                JumlahPiutangUsaha_InvoiceLama = 0
                Do While dr.Read()
                    If dr.Item("Jenis_Produk_Per_Item") = JenisProduk_Barang Then
                        DPPBarang_InvoiceLama += dr.Item("Total_Harga_Per_Item")
                    Else
                        DPPJasa_InvoiceLama += dr.Item("Total_Harga_Per_Item")
                    End If
                    PPN_InvoiceLama = dr.Item("PPN")
                    PPhTerutang_InvoiceLama = dr.Item("PPh_Terutang")
                    BiayaTransportasiPenjualan_InvoiceLama = dr.Item("Biaya_Transportasi")
                    TotalTagihan_InvoiceLama = dr.Item("Jumlah_Tagihan")
                    JumlahPiutangUsaha_InvoiceLama = dr.Item("Jumlah_Piutang_Usaha")
                Loop
                AksesDatabase_Transaksi(Tutup)

                'Pengisian Value Selisih :
                DPPBarang_Selisih = DPPBarang_InvoiceLama - DPPBarangTermin
                DPPJasa_Selisih = DPPJasa_InvoiceLama - DPPJasaTermin
                PPN_Selisih = PPN_InvoiceLama - PPN
                PPhTerutang_Selisih = PPhTerutang_InvoiceLama - PPhTerutang
                BiayaTransportasiPenjualan_Selisih = BiayaTransportasiPenjualan_InvoiceLama - BiayaTransportasi
                TotalTagihan_Selisih = TotalTagihan_InvoiceLama - TotalTagihan
                JumlahPiutangUsaha_Selisih = JumlahPiutangUsaha_InvoiceLama - JumlahPiutangUsaha

                'Jika Value Baru Lebih Besar atau Sama dengan Value Lama : (Positif atau Nol)
                If DPPBarangTermin >= DPPBarang_InvoiceLama Then

                    'Pembalikan Value Minus (-) Menjadi Plus (+)
                    TotalTagihan_Selisih = 0 - TotalTagihan_Selisih
                    JumlahPiutangUsaha_Selisih = 0 - JumlahPiutangUsaha_Selisih
                    PPhTerutang_Selisih = 0 - PPhTerutang_Selisih
                    PPN_Selisih = 0 - PPN_Selisih
                    DPPBarang_Selisih = 0 - DPPBarang_Selisih
                    DPPJasa_Selisih = 0 - DPPJasa_Selisih
                    BiayaTransportasiPenjualan_Selisih = 0 - BiayaTransportasiPenjualan_Selisih

                    If JumlahPiutangUsaha = JumlahPiutangUsaha_InvoiceLama Then
                        PesanPemberitahuan("Data tetap didorong ke Jurnal meskipun nilainya 0 (Nol).")
                    End If

                    Dim JumlahPencairan_Selisih
                    If JenisPenjualan = JenisPenjualan_Tunai Then
                        JumlahPencairan_Selisih = TotalTagihan_Selisih
                        JumlahPiutangUsaha_Selisih = 0
                    Else
                        JumlahPencairan_Selisih = JumlahPiutangUsaha_Selisih
                        PPhTerutang = 0
                    End If

                    'Eliminasi Baris Jurnal Tertentu :
                    If Not (JualAsset = True And (PPhTerutang > 0 Or JumlahPiutangUsaha = JumlahPiutangUsaha_InvoiceLama)) Then PPhTerutang_Selisih = 0
                    If Not (PPN > 0 Or JumlahPiutangUsaha = JumlahPiutangUsaha_InvoiceLama) Then PPN_Selisih = 0
                    If Not (DPPBarangTermin > 0 Or JumlahPiutangUsaha = JumlahPiutangUsaha_InvoiceLama) Then DPPBarang_Selisih = 0
                    If Not (DPPJasaTermin > 0 Or JumlahPiutangUsaha = JumlahPiutangUsaha_InvoiceLama) Then DPPJasa_Selisih = 0

                    'Simpan Jurnal :
                    ___jurDebet(COADebet, JumlahPencairan_Selisih)
                    ___jurDebet(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang_Selisih)
                    _______jurKredit(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang_Selisih)
                    _______jurKredit(KodeTautanCOA_PPNKeluaran, PPN_Selisih)
                    _______jurKredit(COAPenjualanBarangAtauAsset, DPPBarang_Selisih)
                    _______jurKredit(COAJasa, DPPJasa_Selisih)
                    _______jurKredit(KodeTautanCOA_BiayaDibayarDimuka, BiayaTransportasiPenjualan_Selisih) '<---- Untuk lebih aman, nanti ini diganti dengan BiayaDibayarDimuka_Selisih..!!!

                End If

                'Jika Value Baru Lebih Kecil dari Value Lama : (Negatif)
                If DPPBarangTermin < DPPBarang_InvoiceLama Then

                    Dim JumlahPencairan_Selisih
                    If JenisPenjualan = JenisPenjualan_Tunai Then
                        JumlahPencairan_Selisih = TotalTagihan_Selisih
                        JumlahPiutangUsaha_Selisih = 0
                    Else
                        JumlahPencairan_Selisih = JumlahPiutangUsaha_Selisih
                        PPhTerutang = 0
                    End If

                    'Eliminasi Baris Jurnal Tertentu :
                    If Not (JualAsset = True) Then PPhTerutang_Selisih = 0

                    'Simpan Jurnal
                    ___jurDebet(COAPenjualanBarangAtauAsset, DPPBarang_Selisih)
                    ___jurDebet(COAJasa, DPPJasa_Selisih)
                    ___jurDebet(KodeTautanCOA_PPNKeluaran, PPN_Selisih)
                    ___jurDebet(KodeTautanCOA_BiayaDibayarDimuka, BiayaTransportasiPenjualan_Selisih) '<---- Untuk lebih aman, nanti ini diganti dengan BiayaDibayarDimuka_Selisih..!!!
                    ___jurDebet(PenentuanCOA_HutangPajak(JenisPPh, KodeSetoran), PPhTerutang_Selisih)
                    _______jurKredit(COADebet, JumlahPencairan_Selisih)
                    _______jurKredit(KodeTautanCOA_BiayaPPhPasal42_402, PPhTerutang_Selisih)

                End If

            End If

            '================================================================================================================
            '   JURNAL CLOSING PENJUALAN ASSET
            '================================================================================================================
            If JualAsset = True Then

                Dim COA_AssetDijual = Kosongan
                Dim COA_AkumulasiPenyusutan = Kosongan
                Dim HargaPerolehan As Int64 = 0
                Dim AkumulasiPenyusutan As Int64 = 0
                Dim HPP As Int64 = 0

                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT * FROM tbl_DataAsset " &
                                      " WHERE Kode_Closing = '" & NomorPenjualan & "' ",
                                      KoneksiDatabaseGeneral)
                dr_ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    KelompokHarta = KonversiAngkaKeKelompokHarta(dr.Item("Kelompok_Harta"))
                    COA_AssetDijual = dr.Item("COA_Asset")
                    HargaPerolehan = dr.Item("Harga_Perolehan")
                    AkumulasiPenyusutan = dr.Item("Akumulasi_Penyusutan")
                    HPP = HargaPerolehan - AkumulasiPenyusutan ' ( 'HPP' Sama dengan 'Nilai Sisa Buku' )
                    If KelompokHarta <> KelompokHarta_Tanah Then COA_AkumulasiPenyusutan = PenentuanCOA_AkumulasiPenyusutan(COA_AssetDijual)
                End If
                AksesDatabase_General(Tutup)

                ResetValueJurnal()
                SistemPenomoranOtomatis_NomorJV()
                NomorJV_Closing = jur_NomorJV
                jur_TanggalTransaksi = TanggalFormatSimpan(TanggalInvoice)
                jur_TanggalInvoice = TanggalInvoice 'Ini tidak menggunakan tanggal format simpan, karena kolomnya bukan format tanggal, melainkan Varchar.
                jur_NomorInvoice = NomorInvoice
                jur_JenisJurnal = JenisJurnal_Asset
                jur_UraianTransaksi = Catatan
                jur_Direct = 0

                'Eliminasi Baris Jurnal Tertentu :
                If Not (KelompokHarta <> KelompokHarta_Tanah) Then AkumulasiPenyusutan = 0

                'Simpan Jurnal :
                ___jurDebet(KodeTautanCOA_HPPPenjualanDisposalAsset, HPP)
                ___jurDebet(COA_AkumulasiPenyusutan, AkumulasiPenyusutan)
                _______jurKredit(COA_AssetDijual, HargaPerolehan)

                If jur_StatusPenyimpananJurnal_PerBaris = True Then
                    jur_StatusPenyimpananJurnal_Lengkap = True
                Else
                    jur_StatusPenyimpananJurnal_Lengkap = False
                End If

            End If

            If jur_StatusPenyimpananJurnal_PerBaris = True Then
                jur_StatusPenyimpananJurnal_Lengkap = True
            Else
                jur_StatusPenyimpananJurnal_Lengkap = False
            End If

            If jur_StatusPenyimpananJurnal_Lengkap = True Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_Penjualan_Invoice " &
                                      " SET Nomor_Faktur_Pajak = '" & NomorFakturPajak & "', " &
                                      " Nomor_JV = '" & NomorJV_Penjualan & "' " &
                                      " WHERE Nomor_Invoice = '" & NomorInvoice & "' ",
                                      KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
                AksesDatabase_General(Buka)
                cmd = New Odbc.OdbcCommand(" UPDATE tbl_DataAsset SET " &
                                           " Nomor_JV_Closing       = '" & NomorJV_Closing & "' " &
                                           " WHERE Kode_Closing     = '" & NomorPenjualan & "' ",
                                       KoneksiDatabaseGeneral)
                cmd_ExecuteNonQuery()
                'frm_BukuPenjualan.TampilkanData()
                RefreshTampilanInvoicePenjualan()
                pesan_DataBerhasilDikirimKeJurnal()
                If JualAsset = False Then
                    LihatJurnal(NomorJV_Penjualan)
                End If
                usc_BukuPenjualan_Ekspor.TampilkanData()
                Me.Close()
            Else
                pesan_DataGagalDikirimKeJurnal()
            End If

            ResetValueJurnal() 'Untuk Jaga-jaga, sebaiknya semua Value Jurnal di-reset lagi setelah proses penjurnalan selesai.

        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
