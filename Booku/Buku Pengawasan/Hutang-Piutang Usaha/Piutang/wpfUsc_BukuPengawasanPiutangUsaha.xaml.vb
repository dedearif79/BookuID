Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfUsc_BukuPengawasanPiutangUsaha

    Public StatusAktif As Boolean = False

    Public JudulForm
    Public JudulForm_SaldoAkhirPiutangUsaha = "Saldo Akhir Piutang Usaha"
    Public JudulForm_SaldoAwalPiutangUsaha = "Saldo Awal Piutang Usaha"
    Public JudulForm_BukuPengawasanPiutangUsaha = "Buku Pengawasan Piutang Usaha"

    Public JenisRelasi_Induk
    Public COAPiutang

    Dim QueryTampilan
    Dim QueryTampilanPiutangTahunLalu As String
    Dim QueryTampilanPiutangTahunAktif As String

    Dim BarisIndex

    'Variabel Tabel :
    Dim NomorUrut
    Dim JenisRelasi
    Dim NomorBPPU
    Dim NomorPenjualan
    Dim JenisInvoice
    Dim JenisProduk
    Dim AngkaInvoice
    Dim AngkaInvoice_Sebelumnya
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim TanggalInvoice
    Dim MasaJatuhTempo
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim TanggalDiterima
    Dim NomorPO
    Dim TanggalPO
    Dim KodeProject
    Dim NamaProduk
    Dim KodeCustomer
    Dim NamaCustomer
    Dim Kurs As Decimal
    Dim JumlahHarga
    Dim DiskonRp
    Dim DasarPengenaanPajak
    Dim JenisPPN
    Dim PPN
    Dim JenisPPh
    Dim PPhTerutang
    Dim PPhDitanggung
    Dim PPhDipotong
    Dim TagihanBruto
    Dim BiayaTransportasi
    Dim Retur
    Dim TagihanNetto
    Dim JumlahPiutangUsaha
    Dim SisaPiutangUsaha
    Dim KeteranganJatuhTempo
    Dim TanggalBayar_Arr
    Dim SisaTagihan
    Dim JumlahBayarTagihan
    Dim JumlahBayarTagihan_TahunLalu
    Dim JumlahBayarTagihan_TahunBukuAktif
    Dim JumlahBayarPiutangUsaha
    Dim JumlahBayarPiutangUsaha_TahunLalu
    Dim JumlahBayarPiutangUsaha_TahunBukuAktif
    Dim LOS
    Dim Referensi
    Dim Catatan
    Dim NomorJV_Penjualan

    Dim Total_SisaPiutangUsaha As Int64
    Dim TotalTabel As Int64

    'Asing
    Dim JumlahHarga_Asing As Decimal
    Dim DiskonAsing As Decimal
    Dim JumlahPiutang_Asing As Decimal
    Dim SisaPiutang_Asing As Decimal
    Dim SisaPiutang_Asing_IDR As Int64
    Dim JumlahBayar_Asing As Decimal
    Dim BiayaBiaya_Asing As Decimal

    'Variabel Data Terseleksi :
    Dim NomorUrut_Terseleksi
    Dim JenisInvoice_Terseleksi
    Dim JenisProduk_Terseleksi
    Public AngkaInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim MasaJatuhTempo_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim TanggalSJBAST_Terseleksi
    Dim NomorPO_Terseleksi
    Dim TanggalPO_Terseleksi
    Dim KodeProject_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim JumlahHarga_Terseleksi
    Dim DiskonRp_Terseleksi
    Dim DasarPengenaanPajak_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim JenisPPN_Terseleksi
    Dim PPN_Terseleksi
    Dim JenisPPh_Terseleksi
    Dim PPhTerutang_Terseleksi
    Dim PPhDitanggung_Terseleksi
    Dim PPhDipotong_Terseleksi
    Dim TagihanBruto_Terseleksi
    Dim Retur_Terseleksi
    Dim TagihanNetto_Terseleksi
    Dim JumlahPiutangUsaha_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim SisaPiutangUsaha_Terseleksi
    Dim SisaTagihan_Terseleksi
    Dim BiayaAdministrasiBank_Terseleksi
    Dim Catatan_Terseleksi


    Dim NomorJV_Penjualan_Terseleksi
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi
    Dim ReferensiBayar_Terseleksi
    Dim NomorPenjualan_Terseleksi As String
    Dim NomorBPPU_Terseleksi As String

    'Asing :
    Dim JumlahPiutang_Asing_Terseleksi As Decimal
    Dim JumlahBayar_Asing_Terseleksi As Decimal
    Dim SisaPiutang_Asing_Terseleksi As Decimal


    Dim TermasukPiutangTahunIni_Terseleksi As Boolean


    Dim InvoiceDenganPO As Boolean

    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Pilihan Filter :
    Dim Pilih_JenisRelasi
    Dim Pilih_JenisProduk_Induk
    Dim Pilih_KodeCustomer
    Dim Pilih_JatuhTempo
    Dim Pilih_LOS

    'Variabel Filter :
    Dim FilterLokasiWP As Boolean
    Dim FilterRelasi As Boolean
    Dim FilterJatuhTempo As Boolean

    Public DestinasiPenjualan
    Dim PenjualanLokal As Boolean
    Dim PenjualanEkspor As Boolean

    Public KodeMataUang As String
    Dim KursHariIni As Decimal

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Terabas()
        StatusAktif = True

        ProsesLoadingForm = True

        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            PenjualanLokal = True
            PenjualanEkspor = False
            txt_KursHariIni.Text = 1
        Else
            PenjualanLokal = False
            PenjualanEkspor = True
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then txt_KursHariIni.Text = KursTengahBI_AkhirTahunIni(KodeMataUang)
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then txt_KursHariIni.Text = AmbilValue_KursTengahBI(KodeMataUang, Today)
        End If

        LogikaDestinasiPenjualan()

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = JudulForm_SaldoAkhirPiutangUsaha
            VisibilitasTombolJurnal(False)
            VisibilitasTombolCRUD(True)
            grb_InfoSaldo.Header = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
            VisibilitasTombolSaldoAwal(False)
            VisibilitasTombolDPPU(False)
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Nomor_PO.Visibility = Visibility.Collapsed
            Tanggal_PO.Visibility = Visibility.Collapsed
            lbl_Kurs.Text = "Kurs Akhir Tahun : "
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            JudulForm = JudulForm_BukuPengawasanPiutangUsaha
            VisibilitasTombolJurnal(True)
            VisibilitasTombolCRUD(False)
            grb_InfoSaldo.Header = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
            VisibilitasTombolSaldoAwal(True)
            VisibilitasTombolDPPU(True)
            Nomor_SJ_BAST.Visibility = Visibility.Visible
            Tanggal_SJ_BAST.Visibility = Visibility.Visible
            Nomor_PO.Visibility = Visibility.Visible
            Tanggal_PO.Visibility = Visibility.Visible
            lbl_Kurs.Text = "Kurs Hari Ini : "
        End If

        LogikaJudulForm()

        VisibilitasFilterJenisRelasi(True)

        ProsesLoadingForm = False

        RefreshTampilanData()

    End Sub


    Sub KontenCombo_JenisRelasi()
        cmb_JenisRelasi.Items.Clear()
        cmb_JenisRelasi.Items.Add(Pilihan_Semua)
        cmb_JenisRelasi.Items.Add(JenisRelasi_Afiliasi)
        cmb_JenisRelasi.Items.Add(JenisRelasi_NonAfiliasi)
        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            Select Case JenisRelasi_Induk
                Case Pilihan_Semua
                    COAPiutang = Kosongan
                    cmb_JenisRelasi.SelectedValue = Pilihan_Semua
                Case JenisRelasi_Afiliasi
                    COAPiutang = KodeTautanCOA_PiutangUsaha_Afiliasi
                    IsiValueComboBypassTerkunci(cmb_JenisRelasi, JenisRelasi_Afiliasi)
                Case JenisRelasi_NonAfiliasi
                    COAPiutang = KodeTautanCOA_PiutangUsaha_NonAfiliasi
                    IsiValueComboBypassTerkunci(cmb_JenisRelasi, JenisRelasi_NonAfiliasi)
            End Select
        End If
        If DestinasiPenjualan = DestinasiPenjualan_Ekspor Then
            Select Case KodeMataUang
                Case KodeMataUang_USD
                    COAPiutang = KodeTautanCOA_PiutangUsaha_USD
                Case KodeMataUang_AUD
                    COAPiutang = KodeTautanCOA_PiutangUsaha_AUD
                Case KodeMataUang_JPY
                    COAPiutang = KodeTautanCOA_PiutangUsaha_JPY
                Case KodeMataUang_CNY
                    COAPiutang = KodeTautanCOA_PiutangUsaha_CNY
                Case KodeMataUang_EUR
                    COAPiutang = KodeTautanCOA_PiutangUsaha_EUR
                Case KodeMataUang_SGD
                    COAPiutang = KodeTautanCOA_PiutangUsaha_SGD
                Case KodeMataUang_GBP
                    COAPiutang = KodeTautanCOA_PiutangUsaha_GBP
            End Select
            IsiValueComboBypassTerkunci(cmb_JenisRelasi, Pilihan_Semua)
            VisibilitasFilterJenisRelasi(False)
        End If
    End Sub


    Sub KontenCombo_JenisProduk_Induk()
        cmb_JenisProduk_Induk.Items.Clear()
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Semua)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Barang)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Jasa)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_BarangDanJasa)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_JasaKonstruksi)
        cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
    End Sub


    Sub KontenCombo_Customer()
        Dim Afiliasi As Integer
        If JenisRelasi_Induk = JenisRelasi_Afiliasi Then Afiliasi = 1
        If JenisRelasi_Induk = JenisRelasi_NonAfiliasi Then Afiliasi = 0
        Dim FilterLokasiWP As String = Kosongan
        If PenjualanLokal Then FilterLokasiWP = " AND Lokasi_WP = '" & LokasiPS_DalamNegeri & "' "
        If PenjualanEkspor Then FilterLokasiWP = " AND Lokasi_WP = '" & LokasiPS_LuarNegeri & "' "
        cmb_Customer.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Customer = 1 " &
                              " AND Afiliasi = " & Afiliasi & FilterLokasiWP, KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        cmb_Customer.Items.Add(Pilihan_Semua)
        Do While dr.Read
            Dim NamaCustomer = dr.Item("Nama_Mitra")
            cmb_Customer.Items.Add(NamaCustomer)
        Loop
        cmb_Customer.SelectedValue = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub


    Sub LogikaDestinasiPenjualan()
        Buat_DataTabelBayar_Lokal()
        'Lokal :
        Nomor_Faktur_Pajak.Header = "Nomor Faktur Pajak"
        Jumlah_Harga.Visibility = Visibility.Visible
        Diskon_Rp.Visibility = Visibility.Visible
        Dasar_Pengenaan_Pajak.Visibility = Visibility.Visible
        PPN_.Visibility = Visibility.Visible
        Nomor_SJ_BAST.Visibility = Visibility.Visible
        Tanggal_SJ_BAST.Visibility = Visibility.Visible
        Biaya_Transportasi.Visibility = Visibility.Visible
        Jumlah_Piutang_Usaha.Visibility = Visibility.Visible
        Sisa_Piutang_Usaha.Visibility = Visibility.Visible
        Tagihan_Bruto.Visibility = Visibility.Visible
        Jenis_PPh.Visibility = Visibility.Visible
        PPh_Terutang.Visibility = Visibility.Visible
        PPh_Dipotong.Visibility = Visibility.Visible
        PPh_Ditanggung.Visibility = Visibility.Visible
        Tagihan_Netto.Visibility = Visibility.Visible
        Jumlah_Bayar.Visibility = Visibility.Visible
        Sisa_Tagihan.Visibility = Visibility.Visible
        Retur_.Visibility = Visibility.Visible
        Potongan_PPh.Visibility = Visibility.Visible
        'Asing :
        Jumlah_Harga_Asing.Visibility = Visibility.Collapsed
        Diskon_Asing.Visibility = Visibility.Collapsed
        Biaya_Biaya_Asing.Visibility = Visibility.Collapsed
        Jumlah_Piutang_Asing.Visibility = Visibility.Collapsed
        Jumlah_Bayar_Asing.Visibility = Visibility.Collapsed
        Sisa_Piutang_Asing.Visibility = Visibility.Collapsed
        Sisa_Piutang_Asing_IDR.Visibility = Visibility.Collapsed
        'Styling Kolom :
        txt_JumlahPiutangUsaha.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        txt_JumlahBayar.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        txt_SisaPiutang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        'Sembunyikan Elemen Kurs :
        brd_Kurs.Visibility = Visibility.Collapsed
        lbl_Kurs.Visibility = Visibility.Collapsed
        txt_KursHariIni.Visibility = Visibility.Collapsed
        btn_TerapkanKurs.Visibility = Visibility.Collapsed
        If PenjualanEkspor Then
            'Lokal :
            Buat_DataTabelBayar_Asing()
            Nomor_Faktur_Pajak.Header = "Nomor PEB"
            Jumlah_Harga.Visibility = Visibility.Collapsed
            Diskon_Rp.Visibility = Visibility.Collapsed
            Dasar_Pengenaan_Pajak.Visibility = Visibility.Collapsed
            PPN_.Visibility = Visibility.Collapsed
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Biaya_Transportasi.Visibility = Visibility.Collapsed
            Jumlah_Piutang_Usaha.Visibility = Visibility.Collapsed
            Sisa_Piutang_Usaha.Visibility = Visibility.Collapsed
            Tagihan_Bruto.Visibility = Visibility.Collapsed
            Jenis_PPh.Visibility = Visibility.Collapsed
            PPh_Terutang.Visibility = Visibility.Collapsed
            PPh_Dipotong.Visibility = Visibility.Collapsed
            PPh_Ditanggung.Visibility = Visibility.Collapsed
            Tagihan_Netto.Visibility = Visibility.Collapsed
            Jumlah_Bayar.Visibility = Visibility.Collapsed
            Sisa_Tagihan.Visibility = Visibility.Collapsed
            Retur_.Visibility = Visibility.Collapsed
            Potongan_PPh.Visibility = Visibility.Collapsed
            'Asing :
            Jumlah_Harga_Asing.Visibility = Visibility.Visible
            Diskon_Asing.Visibility = Visibility.Visible
            Biaya_Biaya_Asing.Visibility = Visibility.Visible
            Jumlah_Piutang_Asing.Visibility = Visibility.Visible
            Jumlah_Bayar_Asing.Visibility = Visibility.Visible
            Sisa_Piutang_Asing.Visibility = Visibility.Visible
            Sisa_Piutang_Asing_IDR.Visibility = Visibility.Visible
            'Styling Kolom :
            txt_JumlahPiutangUsaha.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_JumlahBayar.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_SisaPiutang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            'Tampilkan Elemen Kurs :
            brd_Kurs.Visibility = Visibility.Visible
            lbl_Kurs.Visibility = Visibility.Visible
            txt_KursHariIni.Visibility = Visibility.Visible
            btn_TerapkanKurs.Visibility = Visibility.Visible
        End If
    End Sub

    Sub KontenCombo_JatuhTempo()
        cmb_JatuhTempo.Items.Clear()
        cmb_JatuhTempo.Items.Add(JatuhTempo_Semua)
        cmb_JatuhTempo.Items.Add(JatuhTempo_Belum)
        cmb_JatuhTempo.Items.Add(JatuhTempo_JT)
        cmb_JatuhTempo.SelectedValue = JatuhTempo_Semua
    End Sub

    Sub KontenCombo_LOS()
        cmb_LOS.Items.Clear()
        cmb_LOS.Items.Add(Pilihan_Semua)
        cmb_LOS.Items.Add(los_L)
        cmb_LOS.Items.Add(los_OS)
        cmb_LOS.SelectedValue = Pilihan_Semua
    End Sub

    Sub RefreshTampilanData()
        EksekusiTampilan = False
        KontenCombo_JenisRelasi()
        KontenCombo_JenisProduk_Induk()
        KontenCombo_Customer()
        KontenCombo_JatuhTempo()
        KontenCombo_LOS()
        EksekusiTampilan = True
        TampilkanData()
    End Sub

    Public EksekusiTampilan As Boolean
    Sub TampilkanData()

        If Not EksekusiTampilan Then Return

        KetersediaanMenuHalaman(pnl_Halaman, False)

        KesesuaianJurnal = True

        'Style Tabel :
        Terabas()
        datatabelUtama.Rows.Clear()

        'Filter Jenis Produk Induk :
        Dim FilterJenisProduk_Induk = " "
        If cmb_JenisProduk_Induk.SelectedValue <> JenisProduk_Semua Then FilterJenisProduk_Induk = " AND Jenis_Produk_Induk = '" & Pilih_JenisProduk_Induk & "' "

        'Filter Customer :
        Dim FilterCustomer = " "
        If cmb_Customer.SelectedValue <> Pilihan_Semua Then FilterCustomer = " AND Kode_Customer = '" & Pilih_KodeCustomer & "' "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterCustomer

        'Query Tampilan :
        Dim SeleksiJurnal = Kosongan
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then SeleksiJurnal = "WHERE Nomor_JV >= 0 " 'Semua (tanpa seleksi jurnal)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then SeleksiJurnal = "WHERE Nomor_JV >  0 " 'Yang ditampilkan hanya yang sudah dijurnal

        QueryTampilanPiutangTahunLalu =
            " SELECT * FROM tbl_Penjualan_Invoice " &
            " WHERE Jenis_Penjualan = '" & JenisPenjualan_Tempo & "' " &
            " AND Kode_Mata_Uang = '" & KodeMataUang & "' " &
            " AND (Tanggal_Invoice < '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " & FilterData &
            " ORDER BY Tanggal_Invoice "
        QueryTampilanPiutangTahunAktif =
            " SELECT * FROM tbl_Penjualan_Invoice " & SeleksiJurnal &
            " AND Kode_Mata_Uang = '" & KodeMataUang & "' " &
            " AND Jenis_Penjualan = '" & JenisPenjualan_Tempo & "' " &
            " AND (Tanggal_Invoice >= '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " & FilterData &
            " ORDER BY Tanggal_Invoice "

        NomorUrut = 0
        BarisIndex = 0
        AngkaInvoice_Sebelumnya = 0
        SaldoAwal_BerdasarkanList = 0
        Total_SisaPiutangUsaha = 0

        AksesDatabase_Transaksi(Buka)

        '---------------------------------------------------------------
        'Data Tabel Sisa Piutang Usaha Tahun Lalu :
        '---------------------------------------------------------------
        QueryTampilan = QueryTampilanPiutangTahunLalu
        DataTabel()

        '---------------------------------------------------------------
        'Data Tabel BPPU Tahun Buku Aktif :
        '---------------------------------------------------------------
        If JudulForm <> JudulForm_SaldoAwalPiutangUsaha Then
            QueryTampilan = QueryTampilanPiutangTahunAktif
            DataTabel()
        End If

        AksesDatabase_Transaksi(Tutup)

        TotalTabel = Total_SisaPiutangUsaha

        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                SaldoAkhir_BerdasarkanList = Total_SisaPiutangUsaha
                txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                AmbilValue_SaldoAkhirBerdasarkanCOA()
                CekKesesuaianSaldoAkhir()
                txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
            Case JenisTahunBuku_NORMAL
                'PesanUntukProgrammer("Saldo Awal Berdasarkan List : " & SaldoAwal_BerdasarkanList & Enter2Baris &
                '                     "Total Sisa Piutang Usaha : ")
                txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
                AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
                CekKesesuaianSaldoAwal()
                txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
                txt_TotalTabel.Text = TotalTabel
        End Select

        BersihkanSeleksi()

    End Sub

    Sub DataTabel()

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            NomorSJBAST = Kosongan
            NomorSJBAST_Satuan = Kosongan
            NomorSJBAST_Sebelumnya = Kosongan
            TanggalSJBAST = Kosongan
            NomorPO = Kosongan
            TanggalPO = Kosongan
            AngkaInvoice = dr.Item("Angka_Invoice")
            JenisInvoice = dr.Item("Jenis_Invoice")
            JenisProduk = dr.Item("Jenis_Produk_Induk")
            NomorPenjualan = dr.Item("Nomor_Penjualan")
            NomorBPPU = AwalanBPPU & Mid(NomorPenjualan, PanjangTeks_AwalanBPPU_Plus1)
            NomorInvoice = dr.Item("Nomor_Invoice")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            MasaJatuhTempo = dr.Item("Jumlah_Hari_Jatuh_Tempo")
            If MasaJatuhTempo > 0 Then
                MasaJatuhTempo &= " hari"
            Else
                MasaJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                         " WHERE Angka_Invoice = '" & AngkaInvoice & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                KodeProject = Kosongan
                NomorSJBAST_Satuan = drTELUSUR.Item("Nomor_SJ_BAST_Produk")
                'Surat Jalan : ---------------------------------------------------
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_SJ " &
                                              " WHERE Nomor_SJ = '" & NomorSJBAST_Satuan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                drTELUSUR2.Read()
                If drTELUSUR2.HasRows Then
                    If NomorSJBAST_Satuan <> NomorSJBAST_Sebelumnya Then
                        If NomorSJBAST = Kosongan Then
                            NomorSJBAST = NomorSJBAST_Satuan
                            TanggalSJBAST = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_SJ"))
                            NomorPO = drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                          " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_SJ"))
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                          " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR3.Item("Kode_Project_Produk")
                        End If
                    End If
                End If
                'BAST : ------------------------------------------------------------
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                              " WHERE Nomor_BAST = '" & NomorSJBAST_Satuan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                drTELUSUR2.Read()
                If drTELUSUR2.HasRows Then
                    If NomorSJBAST_Satuan <> NomorSJBAST_Sebelumnya Then
                        If NomorSJBAST = Kosongan Then
                            NomorSJBAST = NomorSJBAST_Satuan
                            TanggalSJBAST = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
                            NomorPO = drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                          " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject = drTELUSUR3.Item("Kode_project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                          " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR3.Item("Kode_project_Produk")
                        End If
                    End If
                End If
                NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
            Loop
            If NomorSJBAST = Kosongan Then
                InvoiceDenganPO = False
            Else
                InvoiceDenganPO = True
            End If
            If PenjualanLokal Then
                If NomorPO = Kosongan Or dr.Item("Metode_Pembayaran") = MetodePembayaran_Termin Then
                    KodeProject = dr.Item("Kode_Project_Produk")
                    TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ_BAST_Produk"))
                    NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
                End If
            Else
                KodeProject = dr.Item("Kode_Project_Produk")
                TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ_BAST_Produk"))
                NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            End If
            NamaProduk = dr.Item("Nama_Produk")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            If dr.Item("COA_Debet") = KodeTautanCOA_PiutangUsaha_Afiliasi Then JenisRelasi = JenisRelasi_Afiliasi
            If dr.Item("COA_Debet") = KodeTautanCOA_PiutangUsaha_NonAfiliasi Then JenisRelasi = JenisRelasi_NonAfiliasi
            JenisPPN = dr.Item("Jenis_PPN")
            Kurs = 1 '(Kenapa dibuat 1..? Sebetulnya untuk saat ini variabel Kurs tidak diperlukan sih. Tapi ga apa-apa, untuk jaga-jaga aja).
            Dim Termin As Decimal = dr.Item("Termin")
            Dim TerminPersen As Decimal = Termin
            If PenjualanLokal Then
                JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan") * Persen(TerminPersen)
            Else
                JumlahHarga_Asing = dr.Item("Jumlah_Harga_Keseluruhan") * Persen(TerminPersen)
            End If
            If dr.Item("Basis_Perhitungan_Termin") = BasisPerhitunganTermin_Nominal Then
                If PenjualanLokal Then
                    JumlahHarga = FormatUlangInt64(Termin)
                    TerminPersen = JumlahHarga / dr.Item("Jumlah_Harga_Keseluruhan")
                Else
                    JumlahHarga_Asing = Termin
                    TerminPersen = JumlahHarga_Asing / dr.Item("Jumlah_Harga_Keseluruhan")
                End If
            End If
            DiskonRp = dr.Item("Diskon")
            DiskonAsing = AmbilValue_DiskonAsingBerdasarkanNomorInvoicePembelian(NomorInvoice) * Persen(TerminPersen)
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            PPN = dr.Item("PPN")
            JenisPPh = dr.Item("Jenis_PPh")
            If PenjualanLokal Then
                PPhTerutang = dr.Item("PPh_Terutang")
                PPhDitanggung = dr.Item("PPh_Ditanggung")
                PPhDipotong = dr.Item("PPh_Dipotong")
            Else
                PPhTerutang = 0
                PPhDitanggung = 0
                PPhDipotong = 0
            End If
            TagihanBruto = dr.Item("Total_Tagihan")
            BiayaTransportasi = dr.Item("Biaya_Transportasi")
            BiayaBiaya_Asing = dr.Item("Insurance") + dr.Item("Freight")
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            TagihanNetto = TagihanBruto - Retur
            Dim JumlahPiutangUsaha_Perhitungan As Int64
            If dr.Item("Perlakuan_PPN") = PerlakuanPPN_Dibayar Then
                JumlahPiutangUsaha_Perhitungan = DasarPengenaanPajak + BiayaTransportasi + PPN - Retur
            Else
                JumlahPiutangUsaha_Perhitungan = DasarPengenaanPajak + BiayaTransportasi - Retur
            End If
            Dim JumlahPiutangUsaha_Database = dr.Item("Jumlah_Piutang_Usaha")
            If JumlahPiutangUsaha_Perhitungan = JumlahPiutangUsaha_Database Then
                JumlahPiutangUsaha = JumlahPiutangUsaha_Database
            Else
                JumlahPiutangUsaha = JumlahPiutangUsaha_Perhitungan
            End If
            JumlahPiutang_Asing = JumlahHarga_Asing - DiskonAsing + BiayaBiaya_Asing
            Dim TanggalJatuhTempo_Date As Date
            Dim TanggalInvoice_Date As New Date(Right(TanggalInvoice, 4),
                     Mid(TanggalInvoice, 4, 2),
                     Left(TanggalInvoice, 2))
            If Right(MasaJatuhTempo, 2) = "ri" Then
                TanggalJatuhTempo_Date = TanggalInvoice_Date.AddDays(AmbilAngka(MasaJatuhTempo))
            Else
                TanggalJatuhTempo_Date = New Date(Right(MasaJatuhTempo, 4),
                     Mid(MasaJatuhTempo, 4, 2),
                     Left(MasaJatuhTempo, 2))
            End If
            If TanggalJatuhTempo_Date >= Today Then
                KeteranganJatuhTempo = JatuhTempo_Belum
            Else
                KeteranganJatuhTempo = JatuhTempo_JT
            End If

            TanggalBayar_Arr = Kosongan
            JumlahBayarTagihan_TahunLalu = 0
            JumlahBayarTagihan_TahunBukuAktif = 0
            JumlahBayarPiutangUsaha_TahunLalu = 0
            JumlahBayarPiutangUsaha_TahunBukuAktif = 0

            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP = '" & NomorBPPU & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Dim PPhTahunLalu = 0
            Dim PPh = 0
            Do While drBAYAR.Read
                If drBAYAR.Item("Tanggal_KM") < TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahBayarTagihan_TahunLalu += drBAYAR.Item("Jumlah_Bayar")
                    PPhTahunLalu += drBAYAR.Item("PPh_Dipotong")
                End If
                If drBAYAR.Item("Tanggal_KM") >= TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahBayarTagihan_TahunBukuAktif += drBAYAR.Item("Jumlah_Bayar")
                    If TanggalBayar_Arr = Kosongan Then
                        TanggalBayar_Arr &= TanggalFormatTampilan(drBAYAR.Item("Tanggal_KM"))
                    Else
                        TanggalBayar_Arr &= SlashGanda_Pemisah & TanggalFormatTampilan(drBAYAR.Item("Tanggal_KM"))
                    End If
                    PPh += drBAYAR.Item("PPh_Dipotong")
                End If
            Loop

            JumlahBayarPiutangUsaha_TahunLalu = JumlahBayarTagihan_TahunLalu + PPhTahunLalu
            JumlahBayarPiutangUsaha_TahunBukuAktif = JumlahBayarTagihan_TahunBukuAktif + PPh

            JumlahBayarTagihan = JumlahBayarTagihan_TahunLalu + JumlahBayarTagihan_TahunBukuAktif
            JumlahBayarPiutangUsaha = JumlahBayarTagihan + PPhTahunLalu + PPh

            SisaTagihan = TagihanNetto - JumlahBayarTagihan
            SisaPiutangUsaha = JumlahPiutangUsaha - JumlahBayarPiutangUsaha

            'Asing :
            If PenjualanEkspor Then
                JumlahBayar_Asing = JumlahBayarTagihan
                SisaPiutang_Asing = JumlahPiutang_Asing - JumlahBayar_Asing
                SisaPiutang_Asing_IDR = AmbilValue_NilaiMataUang(KodeMataUang, KursHariIni, SisaPiutang_Asing)
            Else
                JumlahBayar_Asing = 0
                SisaPiutang_Asing = 0
                SisaPiutang_Asing_IDR = 0
            End If

            If PenjualanLokal Then
                If SisaPiutangUsaha > 0 Then
                    LOS = los_OS
                Else
                    LOS = los_L
                End If
            Else
                If SisaPiutang_Asing > 0 Then
                    LOS = los_OS
                Else
                    LOS = los_L
                End If
            End If

            Referensi = dr.Item("Referensi")
            Catatan = PenghapusEnter(dr.Item("Catatan"))
            NomorJV_Penjualan = dr.Item("Nomor_JV")

            If AngkaInvoice <> AngkaInvoice_Sebelumnya Then
                ''Filter LokasiWP :
                'FilterLokasiWP = False
                'Select Case DestinasiPenjualan
                '    Case DestinasiPenjualan_Lokal
                '        If Not MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then FilterLokasiWP = True
                '    Case DestinasiPenjualan_Ekspor
                '        If MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then FilterLokasiWP = True
                'End Select
                'Filter Relasi :
                FilterRelasi = False
                Select Case Pilih_JenisRelasi
                    Case Pilihan_Semua
                        FilterRelasi = True
                    Case JenisRelasi_Afiliasi
                        If CustomerSebagaiAfiliasi(KodeCustomer) Then FilterRelasi = True
                    Case JenisRelasi_NonAfiliasi
                        If Not CustomerSebagaiAfiliasi(KodeCustomer) Then FilterRelasi = True
                End Select
                'Filter Jatuh Tempo :
                FilterJatuhTempo = False
                If Pilih_JatuhTempo = JatuhTempo_Semua Then
                    FilterJatuhTempo = True
                Else
                    If Pilih_JatuhTempo = KeteranganJatuhTempo Then FilterJatuhTempo = True
                End If
                'If FilterLokasiWP And FilterRelasi And FilterJatuhTempo Then TambahBaris()
                If FilterRelasi And FilterJatuhTempo Then TambahBaris()
            End If

            AngkaInvoice_Sebelumnya = AngkaInvoice

        Loop

    End Sub

    Sub TambahBaris()
        If Not Pilih_LOS = Pilihan_Semua Then
            Select Case LOS
                Case los_L
                    If Not Pilih_LOS = los_L Then Return
                Case los_OS
                    If Not Pilih_LOS = los_OS Then Return
            End Select
        End If
        If AmbilAngka(SisaTagihan) <= 0 Then KeteranganJatuhTempo = Kosongan
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, JenisRelasi, NomorBPPU, NomorPenjualan, JenisInvoice, JenisProduk,
                                AngkaInvoice, NomorInvoice, NomorFakturPajak, TanggalInvoice, MasaJatuhTempo,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject, NamaProduk, KodeCustomer, NamaCustomer,
                                JumlahHarga, JumlahHarga_Asing, DiskonRp, DiskonAsing, DasarPengenaanPajak, JenisPPN, PPN,
                                BiayaTransportasi, BiayaBiaya_Asing, TagihanBruto, JumlahPiutang_Asing, Retur,
                                JumlahPiutangUsaha, SisaPiutangUsaha, JenisPPh, PPhTerutang, PPhDitanggung, PPhDipotong, TagihanNetto,
                                KeteranganJatuhTempo, TanggalBayar_Arr, JumlahBayarTagihan, JumlahBayar_Asing,
                                SisaTagihan, SisaPiutang_Asing, SisaPiutang_Asing_IDR, LOS,
                                Referensi, Catatan, NomorJV_Penjualan)
        If PenjualanLokal Then Total_SisaPiutangUsaha += SisaPiutangUsaha
        If PenjualanEkspor Then Total_SisaPiutangUsaha += SisaPiutang_Asing_IDR
        If QueryTampilan = QueryTampilanPiutangTahunLalu Then
            SaldoAwal_BerdasarkanList += (JumlahPiutangUsaha - JumlahBayarPiutangUsaha_TahunLalu)
        End If
        BarisIndex += 1
        Terabas()
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_EditPiutang.IsEnabled = False
        btn_HapusPiutang.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_BukuPembantu.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        NomorJV_Pembayaran_Terseleksi = 0
        If Pilih_JenisProduk_Induk = Pilihan_Semua _
            And Pilih_KodeCustomer = Pilihan_Semua _
            And Pilih_JatuhTempo = Pilihan_Semua _
            Then
            If JenisRelasi_Induk = Pilihan_Semua Then
                lbl_TotalTabel.Text = "Sisa Piutang Usaha :"
            Else
                lbl_TotalTabel.Text = "Sisa Piutang Usaha" & StripKosong & JenisRelasi_Induk & " :"
            End If
        Else
            lbl_TotalTabel.Text = "Total :"
        End If
        If Pilih_JenisProduk_Induk = Pilihan_Semua And Pilih_KodeCustomer = Pilihan_Semua And Pilih_JatuhTempo = Pilihan_Semua And Pilih_LOS = Pilihan_Semua Then
            If DestinasiPenjualan = DestinasiPenjualan_Lokal And Pilih_JenisRelasi <> Pilihan_Semua Then VisibilitasInfoSaldo(True)
            If DestinasiPenjualan = DestinasiPenjualan_Ekspor Then VisibilitasInfoSaldo(True)
        Else
            VisibilitasInfoSaldo(False)
        End If
        BersihkanSeleksiPembayaran()
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub


    Sub VisibilitasFilterJenisRelasi(Visibilitas As Boolean)
        brd_FilterJenisProduk.Visibility = Visibility.Collapsed
        lbl_FilterJenisRelasi.Visibility = Visibility.Collapsed
        cmb_JenisRelasi.Visibility = Visibility.Collapsed
        Jenis_Relasi.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisRelasi_Induk = Pilihan_Semua Then
                brd_FilterJenisProduk.Visibility = Visibility.Visible
                lbl_FilterJenisRelasi.Visibility = Visibility.Visible
                cmb_JenisRelasi.Visibility = Visibility.Visible
                Jenis_Relasi.Visibility = Visibility.Visible
            End If
        Else
            brd_FilterJenisProduk.Visibility = Visibility.Collapsed
            lbl_FilterJenisRelasi.Visibility = Visibility.Collapsed
            cmb_JenisRelasi.Visibility = Visibility.Collapsed
            Jenis_Relasi.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasInfoSaldo(Visibilitas As Boolean)
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_TotalTabel.Visibility = Visibility.Collapsed
        If Visibilitas Then
            grb_InfoSaldo.Visibility = Visibility.Visible
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                pnl_SaldoAwalPlusAJP.Visibility = Visibility.Visible
                pnl_TotalTabel.Visibility = Visibility.Visible
            End If
        Else
            grb_InfoSaldo.Visibility = Visibility.Collapsed
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
            pnl_TotalTabel.Visibility = Visibility.Collapsed
        End If
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


    Sub VisibilitasTombolDPPU(Visibilitas As Boolean)
        brd_DPPU.Visibility = Visibility.Collapsed
        btn_DPPU.Visibility = Visibility.Collapsed
        Return 'Sementara ini, kita sembunyikan saja secara mutlak.....!!!!!!!
        'If Visibilitas Then
        '    brd_DPHU.Visibility = Visibility.Visible
        '    btn_DPHU.Visibility = Visibility.Visible
        'Else
        '    brd_DPHU.Visibility = Visibility.Collapsed
        '    btn_DPHU.Visibility = Visibility.Collapsed
        'End If
    End Sub


    Sub VisibilitasTombolSaldoAwal(Visibilitas As Boolean)
        brd_SaldoAwalPiutangUsaha.Visibility = Visibility.Collapsed
        btn_SaldoAwalPiutangUsaha.Visibility = Visibility.Collapsed
        Return '(Semetara ini, sembunyikan saja dulu....!)
        If Visibilitas Then
            If JenisRelasi_Induk <> Pilihan_Semua Then
                brd_SaldoAwalPiutangUsaha.Visibility = Visibility.Visible
                btn_SaldoAwalPiutangUsaha.Visibility = Visibility.Visible
            End If
        Else
            brd_SaldoAwalPiutangUsaha.Visibility = Visibility.Collapsed
            btn_SaldoAwalPiutangUsaha.Visibility = Visibility.Collapsed
        End If
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click

        Dim AdaJurnalRetur As Boolean = False

        If NomorJV_Penjualan_Terseleksi > 0 Then
            If Retur_Terseleksi > 0 Then
                AdaJurnalRetur = True
            Else
                AdaJurnalRetur = False
                LihatJurnal(NomorJV_Penjualan_Terseleksi)
            End If
        ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            MsgBox("Data terpilih BELUM masuk JURNAL.")
            Return
        End If

        If AdaJurnalRetur = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Retur " &
                                  " WHERE Nomor_Invoice_Produk = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            Dim NomorJV_Retur = dr.Item("Nomor_JV")
            AksesDatabase_Transaksi(Tutup)
            PesanUntukProgrammer("Nanti Form berikut ini harus diganti dengan Pilih Jurnal Penjualan/Retur..!!!")
            frm_PilihJurnal_Penjualan_Retur.ResetForm()
            frm_PilihJurnal_Penjualan_Retur.NomorJV_Penjualan = NomorJV_Penjualan_Terseleksi
            frm_PilihJurnal_Penjualan_Retur.NomorJV_Retur = NomorJV_Retur
            frm_PilihJurnal_Penjualan_Retur.ShowDialog()

        End If
    End Sub

    Private Sub btn_InputPiutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputPiutang.Click
        win_InputInvoicePenjualan = New wpfWin_InputInvoicePenjualan
        win_InputInvoicePenjualan.ResetForm()
        win_InputInvoicePenjualan.FungsiForm = FungsiForm_TAMBAH
        win_InputInvoicePenjualan.DestinasiPenjualan = DestinasiPenjualan
        win_InputInvoicePenjualan.JenisRelasi = JenisRelasi_Induk
        IsiValueComboBypassTerkunci(win_InputInvoicePenjualan.cmb_KodeMataUang, KodeMataUang)
        win_InputInvoicePenjualan.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePenjualan.MetodePembayaran = MetodePembayaran_Normal
        win_InputInvoicePenjualan.ShowDialog()
    End Sub

    Private Sub btn_EditPiutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditPiutang.Click
        win_InputInvoicePenjualan = New wpfWin_InputInvoicePenjualan
        win_InputInvoicePenjualan.ResetForm()
        win_InputInvoicePenjualan.FungsiForm = FungsiForm_EDIT
        win_InputInvoicePenjualan.DestinasiPenjualan = DestinasiPenjualan
        win_InputInvoicePenjualan.JenisRelasi = JenisRelasi_Induk
        win_InputInvoicePenjualan.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePenjualan.MetodePembayaran = MetodePembayaran_Normal
        IsiValueForm_InvoicePenjualan()
        win_InputInvoicePenjualan.ShowDialog()
    End Sub

    Sub IsiValueForm_InvoicePenjualan()
        ProsesIsiValueForm = True
        win_InputInvoicePenjualan.AngkaInvoice = AngkaInvoice_Terseleksi
        win_InputInvoicePenjualan.JenisProduk_Induk = JenisProduk_Terseleksi
        win_InputInvoicePenjualan.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        win_InputInvoicePenjualan.NomorPenjualan = NomorPenjualan_Terseleksi
        If AmbilTeksKanan(MasaJatuhTempo_Terseleksi, 2) = "ri" Then
            win_InputInvoicePenjualan.txt_JumlahHariJatuhTempo.Text = AmbilAngka(MasaJatuhTempo_Terseleksi)
            win_InputInvoicePenjualan.dtp_TanggalJatuhTempo.Text = Kosongan
            win_InputInvoicePenjualan.rdb_JumlahHariJatuhTempo.IsChecked = True
        Else
            win_InputInvoicePenjualan.txt_JumlahHariJatuhTempo.Text = Kosongan
            win_InputInvoicePenjualan.dtp_TanggalJatuhTempo.SelectedDate = TanggalFormatWPF(MasaJatuhTempo_Terseleksi)
            win_InputInvoicePenjualan.rdb_TanggalJatuhTempo.IsChecked = True
        End If
        win_InputInvoicePenjualan.cmb_JenisInvoice.SelectedValue = JenisInvoice_Terseleksi
        win_InputInvoicePenjualan.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
        win_InputInvoicePenjualan.txt_NamaCustomer.Text = NamaCustomer_Terseleksi
        win_InputInvoicePenjualan.txt_JumlahNota.Text = JumlahHarga_Terseleksi
        IsiValueElemenRichTextBox(win_InputInvoicePenjualan.txt_Catatan, Catatan_Terseleksi)
        win_InputInvoicePenjualan.NomorFakturPajak = NomorFakturPajak_Terseleksi
        win_InputInvoicePenjualan.NP = "N"
        win_InputInvoicePenjualan.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice_Terseleksi)
        win_InputInvoicePenjualan.IsiTabelProduk()
        win_InputInvoicePenjualan.IsiTabelSJBAST()
        ProsesIsiValueForm = False
    End Sub

    Private Sub btn_HapusPiutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusPiutang.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            HapusDataPenjualan_BerdasarkanNomorInvoice(NomorInvoice_Terseleksi)
            If StatusSuntingDatabase = True Then
                pesan_DataTerpilihBerhasilDihapus()
                TampilkanData()
            Else
                pesan_DataTerpilihGagalDihapus()
            End If
        End If
    End Sub

    Private Sub btn_DPPU_Click(sender As Object, e As RoutedEventArgs) Handles btn_DPPU.Click

    End Sub
    Private Sub btn_BukuPembantu_Click(sender As Object, e As RoutedEventArgs) Handles btn_BukuPembantu.Click
        BukuBesarPembantu(KodeCustomer_Terseleksi, COAPiutang)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Public Sub btn_SaldoAwalPiutangUsaha_ClickWPF(sender As Object, e As RoutedEventArgs) Handles btn_SaldoAwalPiutangUsaha.Click
    End Sub
    Public Sub btn_SaldoAwalPiutangUsaha_Click(sender As Object, e As EventArgs) Handles btn_SaldoAwalPiutangUsaha.Click
        Select Case JudulForm
            Case JudulForm_BukuPengawasanPiutangUsaha
                JudulForm = JudulForm_SaldoAwalPiutangUsaha
                btn_SaldoAwalPiutangUsaha.Content = JudulForm_BukuPengawasanPiutangUsaha
                VisibilitasTombolDPPU(False)
                VisibilitasTombolJurnal(False)
            Case JudulForm_SaldoAwalPiutangUsaha
                JudulForm = JudulForm_BukuPengawasanPiutangUsaha
                btn_SaldoAwalPiutangUsaha.Content = JudulForm_SaldoAwalPiutangUsaha
                VisibilitasTombolDPPU(True)
                VisibilitasTombolJurnal(True)
        End Select
        frm_BukuPengawasanPiutangUsaha.Text = JudulForm
        LogikaJudulForm()
    End Sub

    Sub LogikaJudulForm()
        If JenisRelasi_Induk = Pilihan_Semua Then
            lbl_JudulForm.Text = JudulForm
            frm_BukuPengawasanPiutangUsaha.Text = JudulForm
        Else
            lbl_JudulForm.Text = JudulForm & StripKosong & JenisRelasi_Induk
            frm_BukuPengawasanPiutangUsaha.Text = JudulForm & StripKosong & JenisRelasi_Induk
        End If
        If DestinasiPenjualan = DestinasiPenjualan_Ekspor Then
            lbl_JudulForm.Text = JudulForm & StripKosong & DestinasiPenjualan_Ekspor
            frm_BukuPengawasanPiutangUsaha.Text = JudulForm & StripKosong & DestinasiPenjualan_Ekspor
        End If
    End Sub


    Private Sub cmb_JenisRelasi_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisRelasi.SelectionChanged
        Pilih_JenisRelasi = cmb_JenisRelasi.SelectedValue
        TampilkanData()
    End Sub

    Private Sub cmb_JenisProduk_Induk_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisProduk_Induk.SelectionChanged
        Pilih_JenisProduk_Induk = cmb_JenisProduk_Induk.SelectedValue
        TampilkanData()
    End Sub

    Private Sub cmb_Customer_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Customer.SelectionChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Customer.SelectedValue & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeCustomer = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Customer.SelectedValue = Pilihan_Semua Then Pilih_KodeCustomer = Pilihan_Semua
        TampilkanData()
    End Sub

    Private Sub cmb_JatuhTempo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JatuhTempo.SelectionChanged
        Pilih_JatuhTempo = cmb_JatuhTempo.SelectedValue
        TampilkanData()
    End Sub

    Private Sub cmb_LOS_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_LOS.SelectionChanged
        Pilih_LOS = cmb_LOS.SelectedValue
        TampilkanData()
    End Sub


    Private Sub txt_KursHariIni_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KursHariIni.TextChanged
        KursHariIni = AmbilAngka_Desimal(txt_KursHariIni.Text)
    End Sub

    Private Sub btn_TerapkanKurs_Click(sender As Object, e As RoutedEventArgs) Handles btn_TerapkanKurs.Click
        If KursHariIni = 0 Then
            PesanPeringatan("Silakan isi kolom 'Kurs'.")
            txt_KursHariIni.Focus()
            Return
        End If
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

        NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        NomorBPPU_Terseleksi = rowviewUtama("Nomor_BPPU")
        NomorPenjualan_Terseleksi = rowviewUtama("Nomor_Penjualan")
        JenisInvoice_Terseleksi = rowviewUtama("Jenis_Invoice")
        JenisProduk_Terseleksi = rowviewUtama("Jenis_Produk")
        AngkaInvoice_Terseleksi = AmbilAngka(rowviewUtama("Angka_Invoice"))
        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice")
        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice")
        MasaJatuhTempo_Terseleksi = rowviewUtama("Masa_Jatuh_Tempo")
        NomorSJBAST_Terseleksi = rowviewUtama("Nomor_SJ_BAST")
        TanggalSJBAST_Terseleksi = rowviewUtama("Tanggal_SJ_BAST")
        NomorPO_Terseleksi = rowviewUtama("Nomor_PO")
        TanggalPO_Terseleksi = rowviewUtama("Tanggal_PO")
        KodeProject_Terseleksi = rowviewUtama("Kode_Project")
        NamaProduk_Terseleksi = rowviewUtama("Nama_Produk")
        KodeCustomer_Terseleksi = rowviewUtama("Kode_Customer")
        NamaCustomer_Terseleksi = rowviewUtama("Nama_Customer")
        JumlahHarga_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Harga"))
        DiskonRp_Terseleksi = AmbilAngka(rowviewUtama("Diskon_Rp"))
        DasarPengenaanPajak_Terseleksi = AmbilAngka(rowviewUtama("Dasar_Pengenaan_Pajak"))
        NomorFakturPajak_Terseleksi = rowviewUtama("Nomor_Faktur_Pajak")
        JenisPPN_Terseleksi = rowviewUtama("Jenis_PPN")
        PPN_Terseleksi = AmbilAngka(rowviewUtama("PPN_"))
        JenisPPh_Terseleksi = rowviewUtama("Jenis_PPh")
        PPhTerutang_Terseleksi = AmbilAngka(rowviewUtama("PPh_Terutang"))
        PPhDitanggung_Terseleksi = AmbilAngka(rowviewUtama("PPh_Ditanggung"))
        PPhDipotong_Terseleksi = AmbilAngka(rowviewUtama("PPh_Dipotong"))
        TagihanBruto_Terseleksi = AmbilAngka(rowviewUtama("Tagihan_Bruto"))
        Retur_Terseleksi = AmbilAngka(rowviewUtama("Retur_"))
        TagihanNetto_Terseleksi = AmbilAngka(rowviewUtama("Tagihan_Netto"))
        JumlahPiutangUsaha_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Piutang_Usaha"))
        JumlahBayar_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar"))
        SisaPiutangUsaha_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Piutang_Usaha"))
        SisaTagihan_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Tagihan"))
        Catatan_Terseleksi = rowviewUtama("Catatan_")
        NomorJV_Penjualan_Terseleksi = rowviewUtama("Nomor_JV_Penjualan")
        'Asing :
        JumlahPiutang_Asing_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Piutang_Asing"))
        JumlahBayar_Asing_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Bayar_Asing"))
        SisaPiutang_Asing_Terseleksi = AmbilAngka_Asing(rowviewUtama("Sisa_Piutang_Asing"))

        NomorJV_Pembayaran_Terseleksi = 0
        If Left(NomorBPPU_Terseleksi, PanjangTeks_AwalanBPPU_PlusTahunBuku) = AwalanBPPU_PlusTahunBuku Then
            TermasukPiutangTahunIni_Terseleksi = True
        Else
            TermasukPiutangTahunIni_Terseleksi = False
        End If

        If AngkaInvoice_Terseleksi > 0 Then
            If JudulForm <> JudulForm_SaldoAwalPiutangUsaha Then TampilkanData_Pencairan()
            If TermasukPiutangTahunIni_Terseleksi = True Then btn_LihatJurnal.IsEnabled = True
            btn_EditPiutang.IsEnabled = True
            btn_HapusPiutang.IsEnabled = True
            btn_BukuPembantu.IsEnabled = True
        Else
            BersihkanSeleksi()
        End If
        If TermasukPiutangTahunIni_Terseleksi = False Then btn_LihatJurnal.IsEnabled = False

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then btn_EditPiutang_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If AmbilAngka(e.Row.Item("Nomor_JV_Penjualan")) = 0 Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then e.Row.Foreground = WarnaDataTahunLalu_WPF
        Else
            If e.Row.Item("L_O_S") = los_L Then
                e.Row.Foreground = WarnaTeksStandar_WPF
            Else
                e.Row.Foreground = WarnaMerahSolid_WPF
            End If
        End If
    End Sub



    Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    End Sub
    Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
        HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomBayar IsNot Nothing Then
            BersihkanSeleksiPembayaran()
        End If
    End Sub
    Private Sub datagridBayar_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBayar.SelectedCellsChanged

        KolomTerseleksiBayar = datagridBayar.CurrentColumn
        BarisBayar_Terseleksi = datagridBayar.SelectedIndex
        If BarisBayar_Terseleksi < 0 Then Return
        rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
        If Not rowviewBayar IsNot Nothing Then Return

        NomorJV_Penjualan_Terseleksi = 0
        NomorIdPembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_ID_Bayar").ToString)
        NomorJV_Pembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_JV_Bayar").ToString)
        ReferensiBayar_Terseleksi = rowviewBayar("Referensi_Bayar")
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditBayar.IsEnabled = True
            btn_HapusBayar.IsEnabled = True
        Else
            BersihkanSeleksiPembayaran()
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.IsEnabled = False
    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
    End Sub



    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        'If LevelUserAktif < LevelUser_99_AppDeveloper And PenjualanEkspor Then
        '    PesanPemberitahuan("Mohon maaf. Fitur transaksi asing masih dalam pengembangan.")
        '    Return
        'End If

        If BarisTerseleksi < 0 Then
            MsgBox("Tidak ada baris data terseleksi.")
            Return
        End If

        'If SisaPiutangUsaha_Terseleksi <= 0 Then
        '    MsgBox("Data terpilih sudah LUNAS.")
        '    Return
        'End If
        PesanUntukProgrammer("Logika LUNAS perbaiki...!!!")

        win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PencairanPiutang
        win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
        If MitraSebagaiAfiliasi(KodeCustomer_Terseleksi) = True Then
            win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_PencairanPiutangUsaha_Afiliasi
        Else
            win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_PencairanPiutangUsaha_NonAfiliasi
        End If
        win_InputBuktiPenerimaan.NomorBP = NomorBPPU_Terseleksi
        win_InputBuktiPenerimaan.KodeMataUang = KodeMataUang
        win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeCustomer_Terseleksi
        win_InputBuktiPenerimaan.txt_Kurs.Text = KursHariIni
        ProsesIsiValueForm = False
        win_InputBuktiPenerimaan.ShowDialog()
        If win_InputBuktiPenerimaan.AdaPenyimpanan Then TampilkanData()

    End Sub


    Private Sub btn_EditBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPenerimaan()
    End Sub

    Private Sub btn_HapusBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPenerimaan()
    End Sub


    Sub TampilkanData_Pencairan()

        pnl_SidebarKanan.Visibility = Visibility.Visible
        datatabelBayar.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                              " WHERE Nomor_BP      = '" & NomorBPPU_Terseleksi & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim TotalBayar As Decimal = 0
        Dim JumlahBayar As Decimal
        Dim PPh As Int64 = 0
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
            Dim Referensi = dr.Item("Nomor_KM")
            If PenjualanLokal Then
                JumlahBayar = FormatUlangInt64(dr.Item("Jumlah_Bayar"))
            Else
                JumlahBayar = dr.Item("Jumlah_Bayar")
            End If
            Dim PotonganPPh As Int64 = dr.Item("PPh_Dipotong")
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, PotonganPPh, KeteranganBayar, NomorJVBayar)
            TotalBayar += JumlahBayar
            If PenjualanLokal Then FormatUlangInt64(TotalBayar)
            PPh += PotonganPPh
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksiPembayaran()

        If PenjualanLokal Then
            txt_JumlahPiutangUsaha.Text = JumlahPiutangUsaha_Terseleksi
            txt_JumlahBayar.Text = TotalBayar                                '(Sebaiknya tidak menggunakan variabel TotalBayar_Terseleksi. Agar lebih update).
            txt_SisaPiutang.Text = TagihanNetto_Terseleksi - TotalBayar      '(Sebaiknya tidak menggunakan variabel SisaPiutang_Terseleksi. Agar lebih update).
            txt_PPh.Text = PPh
        Else
            txt_JumlahPiutangUsaha.Text = JumlahPiutang_Asing_Terseleksi
            txt_JumlahBayar.Text = JumlahBayar_Asing_Terseleksi
            txt_SisaPiutang.Text = SisaPiutang_Asing_Terseleksi
            txt_PPh.Text = Kosongan
        End If

        If SisaTagihan_Terseleksi > 0 Then
            lbl_SisaPiutang.Visibility = Visibility.Visible
            txt_SisaPiutang.Visibility = Visibility.Visible
            txt_SisaPiutang.Foreground = WarnaPeringatan_WPF
            lbl_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Text = StatusLunas_BelumLunas
            txt_KeteranganLunas.Foreground = WarnaPeringatan_WPF
        Else
            lbl_SisaPiutang.Visibility = Visibility.Collapsed
            txt_SisaPiutang.Visibility = Visibility.Collapsed
            txt_SisaPiutang.Foreground = WarnaTeksStandar_WPF
            lbl_KeteranganLunas.Visibility = Visibility.Visible
            txt_KeteranganLunas.Visibility = Visibility.Visible
            txt_KeteranganLunas.Text = StatusLunas_Lunas
            txt_KeteranganLunas.Foreground = WarnaTeksStandar_WPF
        End If

        If PPh > 0 Then
            lbl_PPh.Visibility = Visibility.Visible
            txt_PPh.Visibility = Visibility.Visible
            lbl_PPh.Text = "PPh " & JenisPPh_Terseleksi
        Else
            lbl_PPh.Visibility = Visibility.Collapsed
            txt_PPh.Visibility = Visibility.Collapsed
            lbl_PPh.Text = Kosongan
        End If

    End Sub

    Sub BersihkanSeleksiPembayaran()
        BarisBayar_Terseleksi = -1
        datagridBayar.SelectedIndex = -1
        datagridBayar.SelectedItem = Nothing
        datagridBayar.SelectedCells.Clear()
        JumlahBarisBayar = datatabelBayar.Rows.Count
        btn_LihatJurnal.IsEnabled = False
        btn_EditBayar.IsEnabled = False
        btn_HapusBayar.IsEnabled = False
        NomorJV_Pembayaran_Terseleksi = 0
    End Sub



    Private Sub txt_JumlahPiutangUsaha_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPiutangUsaha.TextChanged
        'PemecahRibuanUntukTextBox_WPF(txt_JumlahPiutangUsaha)
    End Sub

    Private Sub txt_JumlahBayar_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBayar.TextChanged
        'PemecahRibuanUntukTextBox_WPF(txt_JumlahBayar)
    End Sub


    Private Sub txt_SisaPiutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SisaPiutang.TextChanged
        'PemecahRibuanUntukTextBox_WPF(txt_SisaPiutang)
    End Sub


    Private Sub txt_PPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPh.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPh)
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
    Dim Jenis_Relasi As New DataGridTextColumn
    Dim Nomor_BPPU As New DataGridTextColumn
    Dim Nomor_Penjualan As New DataGridTextColumn
    Dim Jenis_Invoice As New DataGridTextColumn
    Dim Jenis_Produk As New DataGridTextColumn
    Dim Angka_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Masa_Jatuh_Tempo As New DataGridTextColumn
    Dim Nomor_SJ_BAST As New DataGridTextColumn
    Dim Tanggal_SJ_BAST As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Tanggal_PO As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Kode_Customer As New DataGridTextColumn
    Dim Nama_Customer As New DataGridTextColumn
    Dim Jumlah_Harga As New DataGridTextColumn
    Dim Jumlah_Harga_Asing As New DataGridTextColumn
    Dim Diskon_Rp As New DataGridTextColumn
    Dim Diskon_Asing As New DataGridTextColumn
    Dim Dasar_Pengenaan_Pajak As New DataGridTextColumn
    Dim Jenis_PPN As New DataGridTextColumn
    Dim PPN_ As New DataGridTextColumn
    Dim Biaya_Transportasi As New DataGridTextColumn
    Dim Biaya_Biaya_Asing As New DataGridTextColumn
    Dim Tagihan_Bruto As New DataGridTextColumn
    Dim Jumlah_Piutang_Asing As New DataGridTextColumn
    Dim Retur_ As New DataGridTextColumn
    Dim Jumlah_Piutang_Usaha As New DataGridTextColumn
    Dim Sisa_Piutang_Usaha As New DataGridTextColumn
    Dim Jenis_PPh As New DataGridTextColumn
    Dim PPh_Terutang As New DataGridTextColumn
    Dim PPh_Ditanggung As New DataGridTextColumn
    Dim PPh_Dipotong As New DataGridTextColumn
    Dim Tagihan_Netto As New DataGridTextColumn
    Dim Keterangan_Jatuh_Tempo As New DataGridTextColumn
    Dim Tanggal_Bayar_Arr As New DataGridTextColumn
    Dim Jumlah_Bayar As New DataGridTextColumn
    Dim Jumlah_Bayar_Asing As New DataGridTextColumn
    Dim Sisa_Tagihan As New DataGridTextColumn
    Dim Sisa_Piutang_Asing As New DataGridTextColumn
    Dim Sisa_Piutang_Asing_IDR As New DataGridTextColumn
    Dim L_O_S As New DataGridTextColumn
    Dim Referensi_ As New DataGridTextColumn
    Dim Catatan_ As New DataGridTextColumn
    Dim Nomor_JV_Penjualan As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Jenis_Relasi")
        datatabelUtama.Columns.Add("Nomor_BPPU")
        datatabelUtama.Columns.Add("Nomor_Penjualan")
        datatabelUtama.Columns.Add("Jenis_Invoice")
        datatabelUtama.Columns.Add("Jenis_Produk")
        datatabelUtama.Columns.Add("Angka_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Masa_Jatuh_Tempo")
        datatabelUtama.Columns.Add("Nomor_SJ_BAST")
        datatabelUtama.Columns.Add("Tanggal_SJ_BAST")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Tanggal_PO")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Kode_Customer")
        datatabelUtama.Columns.Add("Nama_Customer")
        datatabelUtama.Columns.Add("Jumlah_Harga", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Harga_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Diskon_Rp", GetType(Int64))
        datatabelUtama.Columns.Add("Diskon_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Dasar_Pengenaan_Pajak", GetType(Int64))
        datatabelUtama.Columns.Add("Jenis_PPN")
        datatabelUtama.Columns.Add("PPN_", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Transportasi", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Biaya_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Tagihan_Bruto", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Piutang_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Retur_", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Piutang_Usaha", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Piutang_Usaha", GetType(Int64))
        datatabelUtama.Columns.Add("Jenis_PPh")
        datatabelUtama.Columns.Add("PPh_Terutang", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Ditanggung", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Dipotong", GetType(Int64))
        datatabelUtama.Columns.Add("Tagihan_Netto", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_Jatuh_Tempo")
        datatabelUtama.Columns.Add("Tanggal_Bayar_Arr")
        datatabelUtama.Columns.Add("Jumlah_Bayar", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Sisa_Tagihan", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Piutang_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Sisa_Piutang_Asing_IDR", GetType(Int64))
        datatabelUtama.Columns.Add("L_O_S")
        datatabelUtama.Columns.Add("Referensi_")
        datatabelUtama.Columns.Add("Catatan_")
        datatabelUtama.Columns.Add("Nomor_JV_Penjualan")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Relasi, "Jenis_Relasi", "Jenis Relasi", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPPU, "Nomor_BPPU", "Nomor BPPU", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Penjualan, "Nomor_Penjualan", "Nomor Penjualan", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Invoice, "Jenis_Invoice", "Jenis Invoice", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Produk, "Jenis_Produk", "Jenis Produk", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angka_Invoice, "Angka_Invoice", "Angka Invoice", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor" & Enter1Baris & "Faktur Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Masa_Jatuh_Tempo, "Masa_Jatuh_Tempo", "Masa" & Enter1Baris & "Jatuh Tempo", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ_BAST, "Nomor_SJ_BAST", "Nomor SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ_BAST, "Tanggal_SJ_BAST", "Tanggal SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PO, "Tanggal_PO", "Tanggal PO", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Customer, "Kode_Customer", "", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Customer, "Nama_Customer", "Nama Customer", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga, "Jumlah_Harga", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Asing, "Jumlah_Harga_Asing", "Jumlah Harga", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Rp, "Diskon_Rp", "Diskon", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Asing, "Diskon_Asing", "Diskon", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Dasar_Pengenaan_Pajak, "Dasar_Pengenaan_Pajak", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PPN, "Jenis_PPN", "Jenis PPN", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_, "PPN_", "PPN", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Transportasi, "Biaya_Transportasi", "Biaya Transportasi", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Biaya_Asing, "Biaya_Biaya_Asing", "Biaya-biaya", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Bruto, "Tagihan_Bruto", "Tagihan Bruto", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Piutang_Asing, "Jumlah_Piutang_Asing", "Jumlah" & Enter1Baris & "Piutang", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Retur_, "Retur_", "Retur", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Piutang_Usaha, "Jumlah_Piutang_Usaha", "Jumlah" & Enter1Baris & "Piutang Usaha", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Piutang_Usaha, "Sisa_Piutang_Usaha", "Sisa" & Enter1Baris & "Piutang Usaha", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PPh, "Jenis_PPh", "Jenis PPh", 69, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Terutang, "PPh_Terutang", "PPh Terutang", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung, "PPh_Ditanggung", "PPh Ditanggung", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong, "PPh_Dipotong", "PPh Dipotong", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Netto, "Tagihan_Netto", "Tagihan Netto", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_Jatuh_Tempo, "Keterangan_Jatuh_Tempo", "Jatuh Tempo", 57, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Bayar_Arr, "Tanggal_Bayar_Arr", "Tanggal Bayar", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar, "Jumlah_Bayar", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar_Asing, "Jumlah_Bayar_Asing", "Jumlah Bayar", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Tagihan, "Sisa_Tagihan", "Sisa Tagihan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Piutang_Asing, "Sisa_Piutang_Asing", "Sisa Piutang" & Enter1Baris & "(USD)", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Piutang_Asing_IDR, "Sisa_Piutang_Asing_IDR", "Sisa Piutang" & Enter1Baris & "(IDR)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, L_O_S, "L_O_S", "L/OS", 45, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Referensi_, "Referensi_", "Referensi", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Catatan_, "Catatan_", "Catatan", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV_Penjualan, "Nomor_JV_Penjualan", "", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)

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
    Dim Referensi_Bayar As New DataGridTextColumn
    Dim Nominal_Bayar As New DataGridTextColumn
    Dim Potongan_PPh As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar_Lokal()

        datatabelBayar = New DataTable
        datagridBayar.Columns.Clear()
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_Bayar")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("Potongan_PPh", GetType(Int64))
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_Bayar, "Referensi_Bayar", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Potongan_PPh, "Potongan_PPh", "Potongan PPh", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub Buat_DataTabelBayar_Asing()

        datatabelBayar = New DataTable
        datagridBayar.Columns.Clear()
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_Bayar")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Decimal))
        datatabelBayar.Columns.Add("Potongan_PPh", GetType(Decimal))
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_Bayar, "Referensi_Bayar", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 111, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Potongan_PPh, "Potongan_PPh", "Potongan PPh", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        datatabelBayar = New DataTable
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_SaldoBerdasarkanList.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.IsReadOnly = True
        txt_SelisihSaldo.IsReadOnly = True
        txt_KursHariIni.IsReadOnly = True
        btn_TerapkanKurs.Visibility = Visibility.Collapsed
        txt_JumlahPiutangUsaha.IsReadOnly = True
        txt_JumlahBayar.IsReadOnly = True
        txt_SisaPiutang.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_AJP.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
        lbl_KeteranganLunas.Visibility = Visibility.Collapsed
        txt_KeteranganLunas.Visibility = Visibility.Collapsed
        brd_DPPU.Visibility = Visibility.Collapsed
        btn_DPPU.Visibility = Visibility.Collapsed
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
    Dim SaldoAwal_BerdasarkanCOA
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    Dim SaldoAkhir_BerdasarkanList
    Dim SaldoAkhir_BerdasarkanCOA
    Dim JumlahPenyesuaianSaldo

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAPiutang, SaldoAwal_BerdasarkanCOA, JumlahPenyesuaianSaldo, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian,
                                                                  txt_SaldoAwalBerdasarkanCOA, txt_AJP, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAPiutang, SaldoAkhir_BerdasarkanCOA, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
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
        Dim Halaman As String = Kosongan
        If JenisRelasi_Induk = JenisRelasi_NonAfiliasi Then Halaman = Halaman_BUKUPENGAWASANPIUTANGUSAHANONAFILIASI
        If JenisRelasi_Induk = JenisRelasi_Afiliasi Then Halaman = Halaman_BUKUPENGAWASANPIUTANGUSAHAAFILIASI
        SesuaikanSaldoAwal(Halaman, COAPiutang, SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian)
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