Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm


Public Class wpfUsc_BukuPengawasanHutangUsaha

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public JudulForm
    Public JudulForm_SaldoAkhirHutangUsaha = "Saldo Akhir Hutang Usaha"
    Public JudulForm_SaldoAwalHutangUsaha = "Saldo Awal Hutang Usaha"
    Public JudulForm_BukuPengawasanHutangUsaha = "Buku Pengawasan Hutang Usaha"

    Public JenisRelasi_Induk
    Public COAHutang

    Public QueryTampilan
    Public QueryTampilanHutangTahunLalu As String
    Public QueryTampilanHutangTahunAktif As String

    Public BarisIndex

    'Variabel Tabel :
    Public NomorUrut
    Public JenisRelasi
    Public NomorBPHU
    Public NomorPembelian
    Public JenisInvoice
    Public JenisProduk
    Public AngkaInvoice
    Public AngkaInvoice_Sebelumnya
    Public NomorInvoice
    Public NomorFakturPajak
    Public TanggalInvoice
    Public MasaJatuhTempo
    Public NomorSJBAST
    Public TanggalSJBAST
    Public TanggalDiterima
    Public NomorPO
    Public TanggalPO
    Public KodeProject
    Public NamaProduk
    Public KodeSupplier
    Public NamaSupplier
    Public Kurs As Decimal
    Public JumlahHarga
    Public DiskonRp
    Public DasarPengenaanPajak
    Public JenisPPN
    Public PPN
    Public JenisPPh
    Public PPhTerutang
    Public PPhDitanggung
    Public PPhDipotong
    Public TagihanBruto
    Public BiayaTransportasi
    Public BiayaMaterai
    Public Retur
    Public TagihanNetto
    Public JumlahHutangUsaha
    Public SisaHutangUsaha
    Public KeteranganJatuhTempo
    Public TanggalBayar_Arr
    Public SisaTagihan
    Public JumlahBayarTagihan
    Public JumlahBayarTagihan_TahunLalu
    Public JumlahBayarTagihan_TahunBukuAktif
    Public JumlahBayarHutangUsaha
    Public JumlahBayarHutangUsaha_TahunLalu
    Public JumlahBayarHutangUsaha_TahunBukuAktif
    Public LOS
    Public Referensi
    Public Catatan
    Public NomorJV_Pembelian

    Public Total_SisaHutangUsaha As Int64
    Public TotalTabel As Int64

    'Asing
    Public JumlahHarga_Asing As Decimal
    Public DiskonAsing As Decimal
    Public JumlahHutang_Asing As Decimal
    Public SisaHutang_Asing As Decimal
    Public SisaHutang_Asing_IDR As Int64
    Public JumlahBayar_Asing As Decimal
    Public BiayaBiaya_Asing As Decimal

    Public NomorInvoiceLama As String

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
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
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
    Dim JumlahHutangUsaha_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim SisaHutangUsaha_Terseleksi
    Dim SisaTagihan_Terseleksi
    Dim BiayaAdministrasiBank_Terseleksi
    Dim Catatan_Terseleksi

    Dim NomorJV_Pembelian_Terseleksi
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi
    Dim ReferensiBayar_Terseleksi
    Dim NomorPembelian_Terseleksi As String
    Dim NomorBPHU_Terseleksi As String

    'Asing :
    Dim JumlahHutang_Asing_Terseleksi As Decimal
    Dim JumlahBayar_Asing_Terseleksi As Decimal
    Dim SisaHutang_Asing_Terseleksi As Decimal

    Dim TermasukHutangTahunIni_Terseleksi As Boolean


    Dim InvoiceDenganPO As Boolean

    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Pilihan Filter :
    Dim Pilih_JenisRelasi
    Dim Pilih_JenisProduk_Induk
    Dim Pilih_KodeSupplier
    Dim Pilih_JatuhTempo
    Dim Pilih_LOS

    'Variabel Filter :
    Dim FilterLokasiWP As Boolean
    Dim FilterRelasi As Boolean
    Dim FilterJatuhTempo As Boolean

    Public AsalPembelian
    Dim PembelianLokal As Boolean
    Dim PembelianImpor As Boolean

    Public KodeMataUang As String
    Dim KursHariIni As Decimal

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If SudahDimuat Then Return
        StatusAktif = True

        Terabas()

        ProsesLoadingForm = True

        If AsalPembelian = AsalPembelian_Lokal Then
            PembelianLokal = True
            PembelianImpor = False
            txt_KursHariIni.Text = 1
        Else
            PembelianLokal = False
            PembelianImpor = True
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then txt_KursHariIni.Text = KursTengahBI_AkhirTahunIni(KodeMataUang)
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then txt_KursHariIni.Text = AmbilValue_KursTengahBI(KodeMataUang, Today)
        End If


        LogikaAsalPembelian()

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = JudulForm_SaldoAkhirHutangUsaha
            VisibilitasTombolJurnal(False)
            VisibilitasTombolCRUD(True)
            grb_InfoSaldo.Header = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
            VisibilitasTombolSaldoAwal(False)
            VisibilitasTombolDPHU(False)
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Nomor_PO.Visibility = Visibility.Collapsed
            Tanggal_PO.Visibility = Visibility.Collapsed
            lbl_Kurs.Text = "Kurs Akhir Tahun : "
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            JudulForm = JudulForm_BukuPengawasanHutangUsaha
            VisibilitasTombolJurnal(True)
            VisibilitasTombolCRUD(False)
            grb_InfoSaldo.Header = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
            VisibilitasTombolSaldoAwal(True)
            VisibilitasTombolDPHU(True)
            Nomor_SJ_BAST.Visibility = Visibility.Visible
            Tanggal_SJ_BAST.Visibility = Visibility.Visible
            Nomor_PO.Visibility = Visibility.Visible
            Tanggal_PO.Visibility = Visibility.Visible
            lbl_Kurs.Text = "Kurs Hari Ini : "
        End If

        LogikaJudulForm()

        ProsesLoadingForm = False

        VisibilitasFilterJenisRelasi(True)

        RefreshTampilanData()

        SudahDimuat = True

    End Sub


    Sub KontenCombo_JenisRelasi()
        cmb_JenisRelasi.Items.Clear()
        cmb_JenisRelasi.Items.Add(Pilihan_Semua)
        cmb_JenisRelasi.Items.Add(JenisRelasi_Afiliasi)
        cmb_JenisRelasi.Items.Add(JenisRelasi_NonAfiliasi)
        If AsalPembelian = AsalPembelian_Lokal Then
            Select Case JenisRelasi_Induk
                Case Pilihan_Semua
                    COAHutang = Kosongan
                    cmb_JenisRelasi.SelectedValue = Pilihan_Semua
                Case JenisRelasi_Afiliasi
                    COAHutang = KodeTautanCOA_HutangUsaha_Afiliasi
                    IsiValueComboBypassTerkunci(cmb_JenisRelasi, JenisRelasi_Afiliasi)
                Case JenisRelasi_NonAfiliasi
                    COAHutang = KodeTautanCOA_HutangUsaha_NonAfiliasi
                    IsiValueComboBypassTerkunci(cmb_JenisRelasi, JenisRelasi_NonAfiliasi)
            End Select
        End If
        If AsalPembelian = AsalPembelian_Impor Then
            Select Case KodeMataUang
                Case KodeMataUang_USD
                    COAHutang = KodeTautanCOA_HutangUsaha_USD
                Case KodeMataUang_AUD
                    COAHutang = KodeTautanCOA_HutangUsaha_AUD
                Case KodeMataUang_JPY
                    COAHutang = KodeTautanCOA_HutangUsaha_JPY
                Case KodeMataUang_CNY
                    COAHutang = KodeTautanCOA_HutangUsaha_CNY
                Case KodeMataUang_EUR
                    COAHutang = KodeTautanCOA_HutangUsaha_EUR
                Case KodeMataUang_SGD
                    COAHutang = KodeTautanCOA_HutangUsaha_SGD
                Case KodeMataUang_GBP
                    COAHutang = KodeTautanCOA_HutangUsaha_GBP
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


    Sub KontenCombo_Supplier()
        Dim Afiliasi As Integer
        If JenisRelasi_Induk = JenisRelasi_Afiliasi Then Afiliasi = 1
        If JenisRelasi_Induk = JenisRelasi_NonAfiliasi Then Afiliasi = 0
        Dim FilterLokasiWP As String = Kosongan
        If PembelianLokal Then FilterLokasiWP = " AND Lokasi_WP = '" & LokasiPS_DalamNegeri & "' "
        If PembelianImpor Then FilterLokasiWP = " AND Lokasi_WP = '" & LokasiPS_LuarNegeri & "' "
        cmb_Supplier.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Supplier = 1 " &
                              " AND Afiliasi = " & Afiliasi & FilterLokasiWP, KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        cmb_Supplier.Items.Add(Pilihan_Semua)
        Do While dr.Read
            Dim NamaSupplier = dr.Item("Nama_Mitra")
            cmb_Supplier.Items.Add(NamaSupplier)
        Loop
        cmb_Supplier.SelectedValue = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub


    Sub LogikaAsalPembelian()
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
        Biaya_Materai.Visibility = Visibility.Visible
        Jumlah_Hutang_Usaha.Visibility = Visibility.Visible
        Sisa_Hutang_Usaha.Visibility = Visibility.Visible
        Tagihan_Bruto.Visibility = Visibility.Visible
        Jenis_PPh.Visibility = Visibility.Visible
        PPh_Terutang.Visibility = Visibility.Visible
        PPh_Dipotong.Visibility = Visibility.Visible
        PPh_Ditanggung.Visibility = Visibility.Visible
        Tagihan_Netto.Visibility = Visibility.Visible
        Jumlah_Bayar.Visibility = Visibility.Visible
        Sisa_Tagihan.Visibility = Visibility.Visible
        Retur_.Visibility = Visibility.Visible
        Hutang_PPh.Visibility = Visibility.Visible
        'Asing :
        Jumlah_Harga_Asing.Visibility = Visibility.Collapsed
        Diskon_Asing.Visibility = Visibility.Collapsed
        Biaya_Biaya_Asing.Visibility = Visibility.Collapsed
        Jumlah_Hutang_Asing.Visibility = Visibility.Collapsed
        Jumlah_Bayar_Asing.Visibility = Visibility.Collapsed
        Sisa_Hutang_Asing.Visibility = Visibility.Collapsed
        Sisa_Hutang_Asing_IDR.Visibility = Visibility.Collapsed
        'Styling Kolom :
        txt_JumlahHutangUsaha.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        txt_JumlahBayar.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        txt_SisaHutang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
        'Sembunyikan Elemen Kurs :
        brd_Kurs.Visibility = Visibility.Collapsed
        lbl_Kurs.Visibility = Visibility.Collapsed
        txt_KursHariIni.Visibility = Visibility.Collapsed
        btn_TerapkanKurs.Visibility = Visibility.Collapsed
        If PembelianImpor Then
            Buat_DataTabelBayar_Asing()
            'Lokal :
            Nomor_Faktur_Pajak.Header = "Nomor PIB"
            Jumlah_Harga.Visibility = Visibility.Collapsed
            Diskon_Rp.Visibility = Visibility.Collapsed
            Dasar_Pengenaan_Pajak.Visibility = Visibility.Collapsed
            PPN_.Visibility = Visibility.Collapsed
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Biaya_Transportasi.Visibility = Visibility.Collapsed
            Biaya_Materai.Visibility = Visibility.Collapsed
            Jumlah_Hutang_Usaha.Visibility = Visibility.Collapsed
            Sisa_Hutang_Usaha.Visibility = Visibility.Collapsed
            Tagihan_Bruto.Visibility = Visibility.Collapsed
            Jenis_PPh.Visibility = Visibility.Collapsed
            PPh_Terutang.Visibility = Visibility.Collapsed
            PPh_Dipotong.Visibility = Visibility.Collapsed
            PPh_Ditanggung.Visibility = Visibility.Collapsed
            Tagihan_Netto.Visibility = Visibility.Collapsed
            Jumlah_Bayar.Visibility = Visibility.Collapsed
            Sisa_Tagihan.Visibility = Visibility.Collapsed
            Retur_.Visibility = Visibility.Collapsed
            Hutang_PPh.Visibility = Visibility.Collapsed
            'Asing :
            Jumlah_Harga_Asing.Visibility = Visibility.Visible
            Diskon_Asing.Visibility = Visibility.Visible
            Biaya_Biaya_Asing.Visibility = Visibility.Visible
            Jumlah_Hutang_Asing.Visibility = Visibility.Visible
            Jumlah_Bayar_Asing.Visibility = Visibility.Visible
            Sisa_Hutang_Asing.Visibility = Visibility.Visible
            Sisa_Hutang_Asing_IDR.Visibility = Visibility.Visible
            'Styling Kolom :
            txt_JumlahHutangUsaha.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_JumlahBayar.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_SisaHutang.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
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
        KontenCombo_Supplier()
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

        'Filter Supplier :
        Dim FilterSupplier = " "
        If cmb_Supplier.SelectedValue <> Pilihan_Semua Then FilterSupplier = " AND Kode_Supplier = '" & Pilih_KodeSupplier & "' "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterSupplier

        'Query Tampilan :
        Dim SeleksiJurnal = Kosongan
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then SeleksiJurnal = "WHERE Nomor_JV >= 0 " 'Semua (tanpa seleksi jurnal)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then SeleksiJurnal = "WHERE Nomor_JV >  0 " 'Yang ditampilkan hanya yang sudah dijurnal

        QueryTampilanHutangTahunLalu =
            " SELECT * FROM tbl_Pembelian_Invoice " &
            " WHERE Jenis_Pembelian = '" & JenisPembelian_Tempo & "' " &
            " AND Kode_Mata_Uang = '" & KodeMataUang & "' " &
            " AND (Tanggal_Invoice < '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " & FilterData &
            " ORDER BY Tanggal_Invoice "
        QueryTampilanHutangTahunAktif =
            " SELECT * FROM tbl_Pembelian_Invoice " & SeleksiJurnal &
            " AND Jenis_Pembelian = '" & JenisPembelian_Tempo & "' " &
            " AND Kode_Mata_Uang = '" & KodeMataUang & "' " &
            " AND (Tanggal_Invoice >= '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " & FilterData &
            " ORDER BY Tanggal_Invoice "

        NomorUrut = 0
        BarisIndex = 0
        AngkaInvoice_Sebelumnya = 0
        SaldoAwal_BerdasarkanList = 0
        Total_SisaHutangUsaha = 0

        AksesDatabase_Transaksi(Buka)

        '---------------------------------------------------------------
        'Data Tabel Sisa Hutang Usaha Tahun Lalu :
        '---------------------------------------------------------------
        QueryTampilan = QueryTampilanHutangTahunLalu
        DataTabel()

        '---------------------------------------------------------------
        'Data Tabel BPHU Tahun Buku Aktif :
        '---------------------------------------------------------------
        If JudulForm <> JudulForm_SaldoAwalHutangUsaha Then
            QueryTampilan = QueryTampilanHutangTahunAktif
            DataTabel()
        End If

        AksesDatabase_Transaksi(Tutup)

        TotalTabel = Total_SisaHutangUsaha

        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                SaldoAkhir_BerdasarkanList = Total_SisaHutangUsaha
                txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                AmbilValue_SaldoAkhirBerdasarkanCOA()
                CekKesesuaianSaldoAkhir()
                txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
            Case JenisTahunBuku_NORMAL
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
            NomorPembelian = dr.Item("Nomor_Pembelian")
            NomorBPHU = AwalanBPHU & Mid(NomorPembelian, PanjangTeks_AwalanBPHU_Plus1)
            NomorInvoice = dr.Item("Nomor_Invoice")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            MasaJatuhTempo = dr.Item("Jumlah_Hari_Jatuh_Tempo")
            If MasaJatuhTempo > 0 Then
                MasaJatuhTempo &= " hari"
            Else
                MasaJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                         " WHERE Angka_Invoice = '" & AngkaInvoice & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                KodeProject = Kosongan
                NomorSJBAST_Satuan = drTELUSUR.Item("Nomor_SJ_BAST_Produk")
                'Surat Jalan : ---------------------------------------------------
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
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
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                          " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_SJ"))
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                          " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR3.Item("Kode_Project_Produk")
                        End If
                    End If
                End If
                'BAST : ------------------------------------------------------------
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_BAST " &
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
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                          " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject = drTELUSUR3.Item("Kode_project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
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
            If PembelianLokal Then
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
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            If dr.Item("COA_Kredit") = KodeTautanCOA_HutangUsaha_Afiliasi Then JenisRelasi = JenisRelasi_Afiliasi
            If dr.Item("COA_Kredit") = KodeTautanCOA_HutangUsaha_NonAfiliasi Then JenisRelasi = JenisRelasi_NonAfiliasi
            JenisPPN = dr.Item("Jenis_PPN")
            Kurs = 1 '(Kenapa dibuat 1..? Sebetulnya untuk saat ini variabel Kurs tidak diperlukan sih. Tapi ga apa-apa, untuk jaga-jaga aja).
            Dim Termin As Decimal = dr.Item("Termin")
            Dim TerminPersen As Decimal = Termin
            If PembelianLokal Then
                JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan") * Persen(TerminPersen)
            Else
                JumlahHarga_Asing = dr.Item("Jumlah_Harga_Keseluruhan") * Persen(TerminPersen)
            End If
            If dr.Item("Basis_Perhitungan_Termin") = BasisPerhitunganTermin_Nominal Then
                If PembelianLokal Then
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
            If PembelianLokal Then
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
            BiayaMaterai = dr.Item("Biaya_Materai")
            BiayaBiaya_Asing = dr.Item("Insurance") + dr.Item("Freight")
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            TagihanNetto = TagihanBruto - Retur
            Dim JumlahHutangUsaha_Perhitungan = DasarPengenaanPajak + BiayaTransportasi + BiayaMaterai + PPN - Retur
            Dim JumlahHutangUsaha_Database = dr.Item("Jumlah_Hutang_Usaha")
            If JumlahHutangUsaha_Perhitungan = JumlahHutangUsaha_Database Then
                JumlahHutangUsaha = JumlahHutangUsaha_Database
            Else
                JumlahHutangUsaha = JumlahHutangUsaha_Perhitungan
            End If
            JumlahHutang_Asing = dr.Item("Jumlah_Hutang_Usaha")
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
            JumlahBayarHutangUsaha_TahunLalu = 0
            JumlahBayarHutangUsaha_TahunBukuAktif = 0

            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP     = '" & NomorBPHU & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Dim PPhTahunLalu = 0
            Dim PPh = 0
            Do While drBAYAR.Read
                If drBAYAR.Item("Tanggal_Bayar") < TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahBayarTagihan_TahunLalu += drBAYAR.Item("Jumlah_Bayar")
                    PPhTahunLalu += drBAYAR.Item("PPh_Dipotong")
                End If
                If drBAYAR.Item("Tanggal_Bayar") >= TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahBayarTagihan_TahunBukuAktif += drBAYAR.Item("Jumlah_Bayar")
                    If TanggalBayar_Arr = Kosongan Then
                        TanggalBayar_Arr &= TanggalFormatTampilan(drBAYAR.Item("Tanggal_KK"))
                    Else
                        TanggalBayar_Arr &= SlashGanda_Pemisah & TanggalFormatTampilan(drBAYAR.Item("Tanggal_KK"))
                    End If
                    PPh += drBAYAR.Item("PPh_Dipotong")
                End If
            Loop

            JumlahBayarHutangUsaha_TahunLalu = JumlahBayarTagihan_TahunLalu + PPhTahunLalu
            JumlahBayarHutangUsaha_TahunBukuAktif = JumlahBayarTagihan_TahunBukuAktif + PPh

            JumlahBayarTagihan = JumlahBayarTagihan_TahunLalu + JumlahBayarTagihan_TahunBukuAktif
            JumlahBayarHutangUsaha = JumlahBayarTagihan + PPhTahunLalu + PPh

            SisaTagihan = TagihanNetto - JumlahBayarTagihan
            SisaHutangUsaha = JumlahHutangUsaha - JumlahBayarHutangUsaha

            'Asing :
            If PembelianImpor Then
                JumlahBayar_Asing = JumlahBayarTagihan
                SisaHutang_Asing = JumlahHutang_Asing - JumlahBayar_Asing
                SisaHutang_Asing_IDR = AmbilValue_NilaiMataUang(KodeMataUang, KursHariIni, SisaHutang_Asing)
            Else
                JumlahBayar_Asing = 0
                SisaHutang_Asing = 0
                SisaHutang_Asing_IDR = 0
            End If

            If PembelianLokal Then
                If SisaHutangUsaha > 0 Then
                    LOS = los_OS
                Else
                    LOS = los_L
                End If
            Else
                If SisaHutang_Asing > 0 Then
                    LOS = los_OS
                Else
                    LOS = los_L
                End If
            End If

            Referensi = dr.Item("Referensi")
            Catatan = PenghapusEnter(dr.Item("Catatan"))
            NomorJV_Pembelian = dr.Item("Nomor_JV")

            If AngkaInvoice <> AngkaInvoice_Sebelumnya Then
                ''Filter LokasiWP :
                'FilterLokasiWP = False
                'Select Case AsalPembelian
                '    Case AsalPembelian_Lokal
                '        If Not MitraSebagaiPerusahaanLuarNegeri(KodeSupplier) Then FilterLokasiWP = True
                '    Case AsalPembelian_Impor
                '        If MitraSebagaiPerusahaanLuarNegeri(KodeSupplier) Then FilterLokasiWP = True
                'End Select
                'Filter Relasi :
                FilterRelasi = False
                Select Case Pilih_JenisRelasi
                    Case Pilihan_Semua
                        FilterRelasi = True
                    Case JenisRelasi_Afiliasi
                        If SupplierSebagaiAfiliasi(KodeSupplier) Then FilterRelasi = True
                    Case JenisRelasi_NonAfiliasi
                        If Not SupplierSebagaiAfiliasi(KodeSupplier) Then FilterRelasi = True
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
        datatabelUtama.Rows.Add(NomorUrut, JenisRelasi, NomorBPHU, NomorPembelian, JenisInvoice, JenisProduk,
                                AngkaInvoice, NomorInvoice, NomorFakturPajak, TanggalInvoice, MasaJatuhTempo,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject, NamaProduk, KodeSupplier, NamaSupplier,
                                JumlahHarga, JumlahHarga_Asing, DiskonRp, DiskonAsing, DasarPengenaanPajak, JenisPPN, PPN,
                                BiayaTransportasi, BiayaMaterai, BiayaBiaya_Asing, TagihanBruto, JumlahHutang_Asing, Retur,
                                JumlahHutangUsaha, SisaHutangUsaha, JenisPPh, PPhTerutang, PPhDitanggung, PPhDipotong, TagihanNetto,
                                KeteranganJatuhTempo, TanggalBayar_Arr, JumlahBayarTagihan, JumlahBayar_Asing,
                                SisaTagihan, SisaHutang_Asing, SisaHutang_Asing_IDR, LOS,
                                Referensi, Catatan, NomorJV_Pembelian)
        If PembelianLokal Then Total_SisaHutangUsaha += SisaHutangUsaha
        If PembelianImpor Then Total_SisaHutangUsaha += SisaHutang_Asing_IDR
        If QueryTampilan = QueryTampilanHutangTahunLalu Then
            SaldoAwal_BerdasarkanList += (JumlahHutangUsaha - JumlahBayarHutangUsaha_TahunLalu)
        End If
        BarisIndex += 1
        Terabas()
        UpdateTampilanRekap()
    End Sub

    Sub UpdateBaris()
        For Each row As DataRow In datatabelUtama.Rows
            If row("Nomor_Invoice").ToString() = NomorInvoiceLama Then
                'Simpan nilai lama untuk update rekap
                Dim SisaHutangUsaha_Lama As Int64 = AmbilAngka(row("Sisa_Hutang_Usaha"))
                Dim SisaHutang_Asing_IDR_Lama As Int64 = AmbilAngka(row("Sisa_Hutang_Asing_IDR"))
                Dim JumlahHutangUsaha_Lama As Int64 = AmbilAngka(row("Jumlah_Hutang_Usaha"))
                Dim JumlahBayarHutangUsaha_TahunLalu_Lama As Int64 = JumlahBayarHutangUsaha_TahunLalu

                'Update kolom-kolom
                row("Jenis_Relasi") = JenisRelasi
                row("Nomor_BPHU") = NomorBPHU
                row("Nomor_Pembelian") = NomorPembelian
                row("Jenis_Invoice") = JenisInvoice
                row("Jenis_Produk") = JenisProduk
                row("Angka_Invoice") = AngkaInvoice
                row("Nomor_Invoice") = NomorInvoice
                row("Nomor_Faktur_Pajak") = NomorFakturPajak
                row("Tanggal_Invoice") = TanggalInvoice
                row("Masa_Jatuh_Tempo") = MasaJatuhTempo
                row("Nomor_SJ_BAST") = NomorSJBAST
                row("Tanggal_SJ_BAST") = TanggalSJBAST
                row("Nomor_PO") = NomorPO
                row("Tanggal_PO") = TanggalPO
                row("Kode_Project") = KodeProject
                row("Nama_Produk") = NamaProduk
                row("Kode_Supplier") = KodeSupplier
                row("Nama_Supplier") = NamaSupplier
                row("Jumlah_Harga") = JumlahHarga
                row("Jumlah_Harga_Asing") = JumlahHarga_Asing
                row("Diskon_Rp") = DiskonRp
                row("Diskon_Asing") = DiskonAsing
                row("Dasar_Pengenaan_Pajak") = DasarPengenaanPajak
                row("Jenis_PPN") = JenisPPN
                row("PPN_") = PPN
                row("Biaya_Transportasi") = BiayaTransportasi
                row("Biaya_Materai") = BiayaMaterai
                row("Biaya_Biaya_Asing") = BiayaBiaya_Asing
                row("Tagihan_Bruto") = TagihanBruto
                row("Jumlah_Hutang_Asing") = JumlahHutang_Asing
                row("Retur_") = Retur
                row("Jumlah_Hutang_Usaha") = JumlahHutangUsaha
                row("Sisa_Hutang_Usaha") = SisaHutangUsaha
                row("Jenis_PPh") = JenisPPh
                row("PPh_Terutang") = PPhTerutang
                row("PPh_Ditanggung") = PPhDitanggung
                row("PPh_Dipotong") = PPhDipotong
                row("Tagihan_Netto") = TagihanNetto
                row("Keterangan_Jatuh_Tempo") = KeteranganJatuhTempo
                row("Tanggal_Bayar_Arr") = TanggalBayar_Arr
                row("Jumlah_Bayar") = JumlahBayarTagihan
                row("Jumlah_Bayar_Asing") = JumlahBayar_Asing
                row("Sisa_Tagihan") = SisaTagihan
                row("Sisa_Hutang_Asing") = SisaHutang_Asing
                row("Sisa_Hutang_Asing_IDR") = SisaHutang_Asing_IDR
                row("L_O_S") = LOS
                row("Referensi_") = Referensi
                row("Catatan_") = Catatan
                row("Nomor_JV_Pembelian") = NomorJV_Pembelian

                'Update rekap (selisih nilai baru - nilai lama)
                If PembelianLokal Then Total_SisaHutangUsaha += (SisaHutangUsaha - SisaHutangUsaha_Lama)
                If PembelianImpor Then Total_SisaHutangUsaha += (SisaHutang_Asing_IDR - SisaHutang_Asing_IDR_Lama)
                If QueryTampilan = QueryTampilanHutangTahunLalu Then
                    SaldoAwal_BerdasarkanList += ((JumlahHutangUsaha - JumlahBayarHutangUsaha_TahunLalu) - (JumlahHutangUsaha_Lama - JumlahBayarHutangUsaha_TahunLalu_Lama))
                End If

                UpdateTampilanRekap()
                Exit For
            End If
        Next
    End Sub

    Sub UpdateTampilanRekap()
        TotalTabel = Total_SisaHutangUsaha
        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                SaldoAkhir_BerdasarkanList = Total_SisaHutangUsaha
                txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
            Case JenisTahunBuku_NORMAL
                txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
                txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
                txt_TotalTabel.Text = TotalTabel
        End Select
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_EditHutang.IsEnabled = False
        btn_HapusHutang.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_BukuPembantu.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        NomorJV_Pembayaran_Terseleksi = 0
        If Pilih_JenisProduk_Induk = Pilihan_Semua _
            And Pilih_KodeSupplier = Pilihan_Semua _
            And Pilih_JatuhTempo = Pilihan_Semua _
            Then
            If JenisRelasi_Induk = Pilihan_Semua Then
                lbl_TotalTabel.Text = "Sisa Hutang Usaha :"
            Else
                lbl_TotalTabel.Text = "Sisa Hutang Usaha" & StripKosong & JenisRelasi_Induk & " :"
            End If
        Else
            lbl_TotalTabel.Text = "Total :"
        End If
        If Pilih_JenisProduk_Induk = Pilihan_Semua And Pilih_KodeSupplier = Pilihan_Semua And Pilih_JatuhTempo = Pilihan_Semua And Pilih_LOS = Pilihan_Semua Then
            If AsalPembelian = AsalPembelian_Lokal And Pilih_JenisRelasi <> Pilihan_Semua Then VisibilitasInfoSaldo(True)
            If AsalPembelian = AsalPembelian_Impor Then VisibilitasInfoSaldo(True)
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


    Sub VisibilitasTombolDPHU(Visibilitas As Boolean)
        brd_DPHU.Visibility = Visibility.Collapsed
        btn_DPHU.Visibility = Visibility.Collapsed
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
        brd_SaldoAwalHutangUsaha.Visibility = Visibility.Collapsed
        btn_SaldoAwalHutangUsaha.Visibility = Visibility.Collapsed
        Return '(Semetara ini, sembunyikan saja dulu....!)
        If Visibilitas Then
            If JenisRelasi_Induk <> Pilihan_Semua Then
                brd_SaldoAwalHutangUsaha.Visibility = Visibility.Visible
                btn_SaldoAwalHutangUsaha.Visibility = Visibility.Visible
            End If
        Else
            brd_SaldoAwalHutangUsaha.Visibility = Visibility.Collapsed
            btn_SaldoAwalHutangUsaha.Visibility = Visibility.Collapsed
        End If
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click

        Dim AdaJurnalRetur As Boolean = False

        If NomorJV_Pembelian_Terseleksi > 0 Then
            If Retur_Terseleksi > 0 Then
                AdaJurnalRetur = True
            Else
                AdaJurnalRetur = False
                LihatJurnal(NomorJV_Pembelian_Terseleksi)
            End If
        ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
            Return
        End If

        If AdaJurnalRetur = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur " &
                                  " WHERE Nomor_Invoice_Produk = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            Dim NomorJV_Retur = dr.Item("Nomor_JV")
            AksesDatabase_Transaksi(Tutup)
            PesanUntukProgrammer("Nanti Form berikut ini harus diganti dengan Pilih Jurnal Pembelian/Retur..!!!")
            frm_PilihJurnal_Penjualan_Retur.ResetForm()
            frm_PilihJurnal_Penjualan_Retur.NomorJV_Penjualan = NomorJV_Pembelian_Terseleksi
            frm_PilihJurnal_Penjualan_Retur.NomorJV_Retur = NomorJV_Retur
            frm_PilihJurnal_Penjualan_Retur.ShowDialog()

        End If
    End Sub

    Private Sub btn_InputHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputHutang.Click
        win_InputInvoicePembelian = New wpfWin_InputInvoicePembelian
        win_InputInvoicePembelian.ResetForm()
        win_InputInvoicePembelian.FungsiForm = FungsiForm_TAMBAH
        win_InputInvoicePembelian.AsalPembelian = AsalPembelian
        win_InputInvoicePembelian.JenisRelasi = JenisRelasi_Induk
        IsiValueComboBypassTerkunci(win_InputInvoicePembelian.cmb_KodeMataUang, KodeMataUang)
        win_InputInvoicePembelian.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePembelian.MetodePembayaran = MetodePembayaran_Normal
        win_InputInvoicePembelian.ShowDialog()
    End Sub

    Private Sub btn_EditHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditHutang.Click
        win_InputInvoicePembelian = New wpfWin_InputInvoicePembelian
        win_InputInvoicePembelian.ResetForm()
        win_InputInvoicePembelian.FungsiForm = FungsiForm_EDIT
        win_InputInvoicePembelian.AsalPembelian = AsalPembelian
        win_InputInvoicePembelian.JenisRelasi = JenisRelasi_Induk
        win_InputInvoicePembelian.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePembelian.MetodePembayaran = MetodePembayaran_Normal
        IsiValueForm_InvoicePembelian()
        win_InputInvoicePembelian.ShowDialog()
    End Sub

    Sub IsiValueForm_InvoicePembelian()
        ProsesIsiValueForm = True
        win_InputInvoicePembelian.AngkaInvoice = AngkaInvoice_Terseleksi
        win_InputInvoicePembelian.JenisProduk_Induk = JenisProduk_Terseleksi
        win_InputInvoicePembelian.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        win_InputInvoicePembelian.NomorPembelian = NomorPembelian_Terseleksi
        If AmbilTeksKanan(MasaJatuhTempo_Terseleksi, 2) = "ri" Then
            win_InputInvoicePembelian.txt_JumlahHariJatuhTempo.Text = AmbilAngka(MasaJatuhTempo_Terseleksi)
            win_InputInvoicePembelian.dtp_TanggalJatuhTempo.Text = Kosongan
            win_InputInvoicePembelian.rdb_JumlahHariJatuhTempo.IsChecked = True
        Else
            win_InputInvoicePembelian.txt_JumlahHariJatuhTempo.Text = Kosongan
            win_InputInvoicePembelian.dtp_TanggalJatuhTempo.SelectedDate = TanggalFormatWPF(MasaJatuhTempo_Terseleksi)
            win_InputInvoicePembelian.rdb_TanggalJatuhTempo.IsChecked = True
        End If
        win_InputInvoicePembelian.cmb_JenisInvoice.SelectedValue = JenisInvoice_Terseleksi
        win_InputInvoicePembelian.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        win_InputInvoicePembelian.txt_NamaSupplier.Text = NamaSupplier_Terseleksi
        win_InputInvoicePembelian.txt_JumlahNota.Text = JumlahHarga_Terseleksi
        IsiValueElemenRichTextBox(win_InputInvoicePembelian.txt_Catatan, Catatan_Terseleksi)
        win_InputInvoicePembelian.NomorFakturPajak = NomorFakturPajak_Terseleksi
        win_InputInvoicePembelian.NP = "N"
        win_InputInvoicePembelian.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice_Terseleksi)
        win_InputInvoicePembelian.IsiTabelProduk()
        win_InputInvoicePembelian.IsiTabelSJBAST()
        ProsesIsiValueForm = False
    End Sub

    Private Sub btn_HapusHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusHutang.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            HapusDataPembelian_BerdasarkanNomorInvoice(NomorInvoice_Terseleksi)
            If StatusSuntingDatabase = True Then
                pesan_DataTerpilihBerhasilDihapus()
                TampilkanData()
            Else
                pesan_DataTerpilihGagalDihapus()
            End If
        End If
    End Sub

    Private Sub btn_DPHU_Click(sender As Object, e As RoutedEventArgs) Handles btn_DPHU.Click

    End Sub
    Private Sub btn_BukuPembantu_Click(sender As Object, e As RoutedEventArgs) Handles btn_BukuPembantu.Click
        BukuBesarPembantu(KodeSupplier_Terseleksi, COAHutang)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Public Sub btn_SaldoAwalHutangUsaha_ClickWPF(sender As Object, e As RoutedEventArgs) Handles btn_SaldoAwalHutangUsaha.Click
    End Sub
    Public Sub btn_SaldoAwalHutangUsaha_Click(sender As Object, e As EventArgs) Handles btn_SaldoAwalHutangUsaha.Click
        Select Case JudulForm
            Case JudulForm_BukuPengawasanHutangUsaha
                JudulForm = JudulForm_SaldoAwalHutangUsaha
                btn_SaldoAwalHutangUsaha.Content = JudulForm_BukuPengawasanHutangUsaha
                VisibilitasTombolDPHU(False)
                VisibilitasTombolJurnal(False)
            Case JudulForm_SaldoAwalHutangUsaha
                JudulForm = JudulForm_BukuPengawasanHutangUsaha
                btn_SaldoAwalHutangUsaha.Content = JudulForm_SaldoAwalHutangUsaha
                VisibilitasTombolDPHU(True)
                VisibilitasTombolJurnal(True)
        End Select
        frm_BukuPengawasanHutangUsaha.Text = JudulForm
        LogikaJudulForm()
    End Sub

    Sub LogikaJudulForm()
        If JenisRelasi_Induk = Pilihan_Semua Then
            lbl_JudulForm.Text = JudulForm
            frm_BukuPengawasanHutangUsaha.Text = JudulForm
        Else
            lbl_JudulForm.Text = JudulForm & StripKosong & JenisRelasi_Induk
            frm_BukuPengawasanHutangUsaha.Text = JudulForm & StripKosong & JenisRelasi_Induk
        End If
        If AsalPembelian = AsalPembelian_Impor Then
            lbl_JudulForm.Text = JudulForm & StripKosong & AsalPembelian_Impor & StripKosong & KodeMataUang
            frm_BukuPengawasanHutangUsaha.Text = JudulForm & StripKosong & AsalPembelian_Impor & StripKosong & KodeMataUang
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

    Private Sub cmb_Supplier_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Supplier.SelectionChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Supplier.SelectedValue & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeSupplier = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Supplier.SelectedValue = Pilihan_Semua Then Pilih_KodeSupplier = Pilihan_Semua
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
        NomorBPHU_Terseleksi = rowviewUtama("Nomor_BPHU")
        NomorPembelian_Terseleksi = rowviewUtama("Nomor_Pembelian")
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
        KodeSupplier_Terseleksi = rowviewUtama("Kode_Supplier")
        NamaSupplier_Terseleksi = rowviewUtama("Nama_Supplier")
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
        JumlahHutangUsaha_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Hutang_Usaha"))
        JumlahBayar_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar"))
        SisaHutangUsaha_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Hutang_Usaha"))
        SisaTagihan_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Tagihan"))
        Catatan_Terseleksi = rowviewUtama("Catatan_")
        NomorJV_Pembelian_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV_Pembelian"))
        'Asing :
        JumlahHutang_Asing_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Hutang_Asing"))
        JumlahBayar_Asing_Terseleksi = AmbilAngka_Asing(rowviewUtama("Jumlah_Bayar_Asing"))
        SisaHutang_Asing_Terseleksi = AmbilAngka_Asing(rowviewUtama("Sisa_Hutang_Asing"))


        NomorJV_Pembayaran_Terseleksi = 0
        If Left(NomorBPHU_Terseleksi, PanjangTeks_AwalanBPHU_PlusTahunBuku) = AwalanBPHU_PlusTahunBuku Then
            TermasukHutangTahunIni_Terseleksi = True
        Else
            TermasukHutangTahunIni_Terseleksi = False
        End If

        If AngkaInvoice_Terseleksi > 0 Then
            If JudulForm <> JudulForm_SaldoAwalHutangUsaha Then TampilkanData_Pembayaran()
            If TermasukHutangTahunIni_Terseleksi = True Then btn_LihatJurnal.IsEnabled = True
            btn_EditHutang.IsEnabled = True
            btn_HapusHutang.IsEnabled = True
            btn_BukuPembantu.IsEnabled = True
        Else
            BersihkanSeleksi()
        End If
        If TermasukHutangTahunIni_Terseleksi = False Then btn_LihatJurnal.IsEnabled = False

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then btn_EditHutang_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If AmbilAngka(e.Row.Item("Nomor_JV_Pembelian")) = 0 Then
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

        NomorJV_Pembelian_Terseleksi = 0
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

        'If LevelUserAktif < LevelUser_99_AppDeveloper And PembelianImpor Then
        '    PesanPemberitahuan("Mohon maaf. Fitur transaksi asing masih dalam pengembangan.")
        '    Return
        'End If

        If BarisTerseleksi < 0 Then
            Pesan_Peringatan("Tidak ada baris data terseleksi.")
            Return
        End If

        If AsalPembelian = AsalPembelian_Lokal And SisaHutangUsaha_Terseleksi <= 0 Then
            Pesan_Informasi("Data terpilih sudah lunas.")
            Return
        End If

        If AsalPembelian = AsalPembelian_Impor And SisaHutang_Asing_Terseleksi <= 0 Then
            Pesan_Informasi("Data terpilih sudah lunas.")
            Return
        End If

        'Alternatif (Untuk Pencegahan) :
        If txt_KeteranganLunas.Text = StatusLunas_Lunas Then
            Pesan_Informasi("Data terpilih sudah lunas.")
            Return
        End If

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        If MitraSebagaiAfiliasi(KodeSupplier_Terseleksi) = True Then
            win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangUsaha_Afiliasi
        Else
            win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangUsaha_NonAfiliasi
        End If
        win_InputBuktiPengeluaran.NomorBP = NomorBPHU_Terseleksi
        win_InputBuktiPengeluaran.KodeMataUang = KodeMataUang
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeSupplier_Terseleksi
        win_InputBuktiPengeluaran.txt_Kurs.Text = KursHariIni
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.AdaPenyimpanan Then TampilkanData()

    End Sub

    Private Sub btn_EditBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub

    Private Sub btn_HapusBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub



    Sub TampilkanData_Pembayaran()

        pnl_SidebarKanan.Visibility = Visibility.Visible
        datatabelBayar.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP      = '" & NomorBPHU_Terseleksi & "' " &
                              " AND Status_Invoice  = '" & Status_Dibayar & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim TotalBayar As Decimal = 0
        Dim JumlahBayar As Decimal
        If PembelianImpor Then
            Convert.ToDecimal(JumlahBayar)
            Convert.ToDecimal(TotalBayar)
        End If
        Dim PPh As Int64 = 0
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            Dim Referensi = dr.Item("Nomor_KK")
            If PembelianLokal Then
                JumlahBayar = FormatUlangInt64(dr.Item("Jumlah_Bayar"))
            Else
                JumlahBayar = dr.Item("Jumlah_Bayar")
            End If
            Dim HutangPPh = dr.Item("PPh_Dipotong")
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, HutangPPh, KeteranganBayar, NomorJVBayar)
            TotalBayar += JumlahBayar
            If PembelianLokal Then FormatUlangInt64(TotalBayar)
            PPh += HutangPPh
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksiPembayaran()

        If PembelianLokal Then
            txt_JumlahHutangUsaha.Text = JumlahHutangUsaha_Terseleksi
            txt_JumlahBayar.Text = TotalBayar                               '(Sebaiknya tidak menggunakan variabel TotalBayar_Terseleksi. Agar lebih update).
            txt_SisaHutang.Text = TagihanNetto_Terseleksi - TotalBayar      '(Sebaiknya tidak menggunakan variabel SisaHutang_Terseleksi. Agar lebih update).
            txt_PPh.Text = PPh
        Else
            PPh = 0
            txt_JumlahHutangUsaha.Text = JumlahHutang_Asing_Terseleksi
            txt_JumlahBayar.Text = TotalBayar
            txt_SisaHutang.Text = SisaHutang_Asing_Terseleksi
            txt_PPh.Text = Kosongan
        End If

        If SisaTagihan_Terseleksi > 0 Then
            lbl_SisaHutang.Visibility = Visibility.Visible
            txt_SisaHutang.Visibility = Visibility.Visible
            txt_SisaHutang.Foreground = WarnaPeringatan_WPF
            lbl_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Text = StatusLunas_BelumLunas
            txt_KeteranganLunas.Foreground = WarnaPeringatan_WPF
        Else
            lbl_SisaHutang.Visibility = Visibility.Collapsed
            txt_SisaHutang.Visibility = Visibility.Collapsed
            txt_SisaHutang.Foreground = WarnaTeksStandar_WPF
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



    Private Sub txt_JumlahHutangUsaha_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHutangUsaha.TextChanged
        'PemecahRibuanUntukTextBox_WPF(txt_JumlahHutangUsaha) (Sudah tidak diperlukan)
    End Sub

    Private Sub txt_JumlahBayar_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBayar.TextChanged
        'PemecahRibuanUntukTextBox_WPF(txt_JumlahBayar) (Sudah tidak diperlukan)
    End Sub


    Private Sub txt_SisaHutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SisaHutang.TextChanged
        'PemecahRibuanUntukTextBox_WPF(txt_SisaHutang) (Sudah tidak diperlukan)
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
    Dim Nomor_BPHU As New DataGridTextColumn
    Dim Nomor_Pembelian As New DataGridTextColumn
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
    Dim Kode_Supplier As New DataGridTextColumn
    Dim Nama_Supplier As New DataGridTextColumn
    Dim Jumlah_Harga As New DataGridTextColumn
    Dim Jumlah_Harga_Asing As New DataGridTextColumn
    Dim Diskon_Rp As New DataGridTextColumn
    Dim Diskon_Asing As New DataGridTextColumn
    Dim Dasar_Pengenaan_Pajak As New DataGridTextColumn
    Dim Jenis_PPN As New DataGridTextColumn
    Dim PPN_ As New DataGridTextColumn
    Dim Biaya_Transportasi As New DataGridTextColumn
    Dim Biaya_Materai As New DataGridTextColumn
    Dim Biaya_Biaya_Asing As New DataGridTextColumn
    Dim Tagihan_Bruto As New DataGridTextColumn
    Dim Jumlah_Hutang_Asing As New DataGridTextColumn
    Dim Retur_ As New DataGridTextColumn
    Dim Jumlah_Hutang_Usaha As New DataGridTextColumn
    Dim Sisa_Hutang_Usaha As New DataGridTextColumn
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
    Dim Sisa_Hutang_Asing As New DataGridTextColumn
    Dim Sisa_Hutang_Asing_IDR As New DataGridTextColumn
    Dim L_O_S As New DataGridTextColumn
    Dim Referensi_ As New DataGridTextColumn
    Dim Catatan_ As New DataGridTextColumn
    Dim Nomor_JV_Pembelian As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Jenis_Relasi")
        datatabelUtama.Columns.Add("Nomor_BPHU")
        datatabelUtama.Columns.Add("Nomor_Pembelian")
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
        datatabelUtama.Columns.Add("Kode_Supplier")
        datatabelUtama.Columns.Add("Nama_Supplier")
        datatabelUtama.Columns.Add("Jumlah_Harga", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Harga_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Diskon_Rp", GetType(Int64))
        datatabelUtama.Columns.Add("Diskon_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Dasar_Pengenaan_Pajak", GetType(Int64))
        datatabelUtama.Columns.Add("Jenis_PPN")
        datatabelUtama.Columns.Add("PPN_", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Transportasi", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Materai", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Biaya_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Tagihan_Bruto", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Hutang_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Retur_", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Hutang_Usaha", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Hutang_Usaha", GetType(Int64))
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
        datatabelUtama.Columns.Add("Sisa_Hutang_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Sisa_Hutang_Asing_IDR", GetType(Int64))
        datatabelUtama.Columns.Add("L_O_S")
        datatabelUtama.Columns.Add("Referensi_")
        datatabelUtama.Columns.Add("Catatan_")
        datatabelUtama.Columns.Add("Nomor_JV_Pembelian")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Relasi, "Jenis_Relasi", "Jenis Relasi", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPHU, "Nomor_BPHU", "Nomor BPHU", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Pembelian, "Nomor_Pembelian", "Nomor Pembelian", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
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
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Supplier, "Kode_Supplier", "", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Supplier, "Nama_Supplier", "Nama Supplier", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga, "Jumlah_Harga", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Asing, "Jumlah_Harga_Asing", "Jumlah Harga", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Rp, "Diskon_Rp", "Diskon", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Asing, "Diskon_Asing", "Diskon", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Dasar_Pengenaan_Pajak, "Dasar_Pengenaan_Pajak", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PPN, "Jenis_PPN", "Jenis PPN", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_, "PPN_", "PPN", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Transportasi, "Biaya_Transportasi", "Biaya Transportasi", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Materai, "Biaya_Materai", "Biaya Materai", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Biaya_Asing, "Biaya_Biaya_Asing", "Biaya-biaya", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Bruto, "Tagihan_Bruto", "Tagihan Bruto", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Hutang_Asing, "Jumlah_Hutang_Asing", "Jumlah" & Enter1Baris & "Hutang", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Retur_, "Retur_", "Retur", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Hutang_Usaha, "Jumlah_Hutang_Usaha", "Jumlah" & Enter1Baris & "Hutang Usaha", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Hutang_Usaha, "Sisa_Hutang_Usaha", "Sisa" & Enter1Baris & "Hutang Usaha", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
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
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Hutang_Asing, "Sisa_Hutang_Asing", "Sisa Hutang" & Enter1Baris & "(" & KodeMataUang & ")", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Hutang_Asing_IDR, "Sisa_Hutang_Asing_IDR", "Sisa Hutang" & Enter1Baris & "(IDR)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, L_O_S, "L_O_S", "L/OS", 45, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Referensi_, "Referensi_", "Referensi", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Catatan_, "Catatan_", "Catatan", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV_Pembelian, "Nomor_JV_Pembelian", "Nomor JV Pembelian", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)

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
    Dim Hutang_PPh As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar_Lokal()

        datatabelBayar = New DataTable
        datagridBayar.Columns.Clear()
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_Bayar")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("Hutang_PPh", GetType(Int64))
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_Bayar, "Referensi_Bayar", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Hutang_PPh, "Hutang_PPh", "Hutang PPh", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
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
        datatabelBayar.Columns.Add("Hutang_PPh", GetType(Decimal))
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_Bayar, "Referensi_Bayar", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 111, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Hutang_PPh, "Hutang_PPh", "Hutang PPh", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
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
        txt_JumlahHutangUsaha.IsReadOnly = True
        txt_JumlahBayar.IsReadOnly = True
        txt_SisaHutang.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_AJP.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
        lbl_KeteranganLunas.Visibility = Visibility.Collapsed
        txt_KeteranganLunas.Visibility = Visibility.Collapsed
        brd_DPHU.Visibility = Visibility.Collapsed
        btn_DPHU.Visibility = Visibility.Collapsed
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
        Dim Halaman As String = Kosongan
        If JenisRelasi_Induk = JenisRelasi_NonAfiliasi Then Halaman = Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI
        If JenisRelasi_Induk = JenisRelasi_Afiliasi Then Halaman = Halaman_BUKUPENGAWASANHUTANGUSAHAAFILIASI
        SesuaikanSaldoAwal(Halaman, COAHutang, SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian)
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
