Imports System.Data.Odbc
Imports bcomm


Public Class X2_frm_BukuPengawasanHutangUsaha_X2

    Public JalurMasuk
    Public JudulForm
    Public JudulForm_SaldoAkhirHutangUsaha = "Saldo Akhir Hutang Usaha"
    Public JudulForm_SaldoAwalHutangUsaha = "Saldo Awal Hutang Usaha"
    Public JudulForm_BukuPengawasanHutangUsaha = "Buku Pengawasan Hutang Usaha"

    Public KesesuaianJurnal As Boolean

    Dim QueryTampilan
    Dim QueryTampilanHutangTahunLalu As String
    Dim QueryTampilanHutangTahunAktif As String

    Dim BarisIndex

    'Variabel Tabel :
    Dim NomorUrut
    Dim NomorBPHU
    Dim NomorPembelian
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
    Dim KodeSupplier
    Dim NamaSupplier
    Dim SupplierSebagaiAfiliasi As Boolean
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
    Dim BiayaMaterai
    Dim Retur
    Dim TagihanNetto
    Dim JumlahHutangUsaha
    Dim SisaHutangUsaha
    Dim KeteranganJatuhTempo
    Dim SisaTagihan
    Dim JumlahBayar
    Dim JumlahBayar_TahunLalu
    Dim JumlahBayar_TahunBukuAktif
    Dim Catatan
    Dim NomorJV_Pembelian

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
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
    Dim BiayaAdministrasiBank_Terseleksi
    Dim Catatan_Terseleksi



    Dim NomorJV_Pembelian_Terseleksi
    Dim BarisBayar_Terseleksi As Integer
    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi
    Dim NPPHU_Terseleksi
    Dim JumlahBaris As Integer
    Dim JumlahBarisBayar As Integer
    Dim NomorPembelian_Terseleksi As String
    Dim NomorBPHU_Terseleksi As String


    Dim TermasukHutangTahunIni_Terseleksi As Boolean


    Dim InvoiceDenganPO As Boolean

    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Pilihan Filter :
    Dim Pilih_JenisRelasi
    Dim Pilih_JenisProduk_Induk
    Dim Pilih_KodeSupplier
    Dim Pilih_JatuhTempo

    'Variabel Filter :
    Dim FilterJatuhTempo As Boolean
    Dim FilterRelasi As Boolean

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        grb_InfoSaldo.Location = New Point(942, 9)
        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = JudulForm_SaldoAkhirHutangUsaha
            btn_LihatJurnal.Visible = False
            grb_Hutang.Visible = True
            'DataTabelUtama.Columns("Status_Proses").Visible = False
            grb_InfoSaldo.Text = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
            lbl_SaldoAwalBerdasarkanCOA.Visible = False
            txt_SaldoAwalBerdasarkanCOA.Visible = False
            lbl_AJP.Visible = False
            txt_AJP.Visible = False
            btn_SaldoAwalHutangUsaha.Visible = False
            btn_DPHU.Visible = False
            DataTabelUtama.Columns("Nomor_SJ_BAST").Visible = False
            DataTabelUtama.Columns("Tanggal_SJ_BAST").Visible = False
            DataTabelUtama.Columns("Nomor_PO").Visible = False
            DataTabelUtama.Columns("Tanggal_PO").Visible = False
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            JudulForm = JudulForm_BukuPengawasanHutangUsaha
            btn_LihatJurnal.Visible = True
            grb_Hutang.Visible = False
            'DataTabelUtama.Columns("Status_Proses").Visible = True
            grb_InfoSaldo.Text = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
            lbl_SaldoAwalBerdasarkanCOA.Visible = True
            txt_SaldoAwalBerdasarkanCOA.Visible = True
            lbl_AJP.Visible = True
            txt_AJP.Visible = True
            btn_SaldoAwalHutangUsaha.Visible = True
            btn_DPHU.Visible = True
            DataTabelUtama.Columns("Nomor_SJ_BAST").Visible = True
            DataTabelUtama.Columns("Tanggal_SJ_BAST").Visible = True
            DataTabelUtama.Columns("Nomor_PO").Visible = True
            DataTabelUtama.Columns("Tanggal_PO").Visible = True
        End If

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        ProsesLoadingForm = False

        RefreshTampilanData()

    End Sub

    Sub KontenCombo_JenisRelasi()
        cmb_JenisRelasi.Items.Clear()
        cmb_JenisRelasi.Items.Add(Pilihan_Semua)
        cmb_JenisRelasi.Items.Add(JenisRelasi_Afiliasi)
        cmb_JenisRelasi.Items.Add(JenisRelasi_NonAfiliasi)
        cmb_JenisRelasi.Text = Pilihan_Semua
    End Sub

    Sub KontenCombo_JenisProduk_Induk()
        cmb_JenisProduk_Induk.Items.Clear()
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Semua)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Barang)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Jasa)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_BarangDanJasa)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_JasaKonstruksi)
        cmb_JenisProduk_Induk.Text = JenisProduk_Semua
    End Sub

    Sub KontenCombo_Supplier()
        cmb_Supplier.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Supplier = 1 ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        cmb_Supplier.Items.Add(Pilihan_Semua)
        Do While dr.Read
            Dim NamaSupplier = dr.Item("Nama_Mitra")
            cmb_Supplier.Items.Add(NamaSupplier)
        Loop
        cmb_Supplier.Text = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub

    Sub KontenCombo_JatuhTempo()
        cmb_JatuhTempo.Items.Clear()
        cmb_JatuhTempo.Items.Add(JatuhTempo_Semua)
        cmb_JatuhTempo.Items.Add(JatuhTempo_Belum)
        cmb_JatuhTempo.Items.Add(JatuhTempo_JT)
        cmb_JatuhTempo.Text = JatuhTempo_Semua
    End Sub

    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_JenisRelasi()
        KontenCombo_JenisProduk_Induk()
        KontenCombo_Supplier()
        KontenCombo_JatuhTempo()
        EksekusiKode = True
        TampilkanData()
    End Sub

    Sub TampilkanData()

        If ProsesLoadingForm = True Or EksekusiKode = False Then Return
        If ProsesLoadingForm = True Then Return
        If ProsesResetForm = True Then Return
        If EksekusiKode = False Then Return

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Filter Jenis Produk Induk :
        Dim FilterJenisProduk_Induk = " "
        If cmb_JenisProduk_Induk.Text <> JenisProduk_Semua Then FilterJenisProduk_Induk = " AND Jenis_Produk_Induk = '" & Pilih_JenisProduk_Induk & "' "

        'Filter Supplier :
        Dim FilterSupplier = " "
        If cmb_Supplier.Text <> Pilihan_Semua Then FilterSupplier = " AND Kode_Supplier = '" & Pilih_KodeSupplier & "' "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterSupplier

        'Query Tampilan :
        Dim SeleksiJurnal = Kosongan
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then SeleksiJurnal = "WHERE Nomor_JV >= 0 " 'Semua (tanpa seleksi jurnal)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then SeleksiJurnal = "WHERE Nomor_JV >  0 " 'Yang ditampilkan hanya yang sudah dijurnal

        QueryTampilanHutangTahunLalu =
            " SELECT * FROM tbl_Pembelian_Invoice " &
            " WHERE Jenis_Pembelian = '" & JenisPembelian_Tempo & "' " &
            " AND (Tanggal_Invoice < '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " & FilterData &
            " ORDER BY Tanggal_Invoice "
        QueryTampilanHutangTahunAktif =
            " SELECT * FROM tbl_Pembelian_Invoice " & SeleksiJurnal &
            " AND Jenis_Pembelian = '" & JenisPembelian_Tempo & "' " &
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
        End Select

        BersihkanSeleksi()
        JumlahBaris = DataTabelUtama.RowCount
        txt_TotalHutangUsaha.Text = Total_SisaHutangUsaha

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
            NomorBPHU = AwalanBPHU & Microsoft.VisualBasic.Mid(NomorPembelian, PanjangTeks_AwalanBPHU_Plus1)
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
            If InvoiceDenganPO = False Then KodeProject = dr.Item("Kode_Project_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            SupplierSebagaiAfiliasi = mdl_PublicSub.SupplierSebagaiAfiliasi(KodeSupplier)
            JenisPPN = dr.Item("Jenis_PPN")
            JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan")
            DiskonRp = dr.Item("Diskon")
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            PPN = dr.Item("PPN")
            JenisPPh = dr.Item("Jenis_PPh")
            PPhTerutang = dr.Item("PPh_Terutang")
            PPhDitanggung = dr.Item("PPh_Ditanggung")
            PPhDipotong = dr.Item("PPh_Dipotong")
            TagihanBruto = dr.Item("Total_Tagihan")
            BiayaTransportasi = dr.Item("Biaya_Transportasi")
            BiayaMaterai = dr.Item("Biaya_Materai")
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            TagihanNetto = TagihanBruto - Retur
            Dim JumlahHutangUsaha_Perhitungan = DasarPengenaanPajak + BiayaTransportasi + BiayaMaterai + PPN - Retur
            Dim JumlahHutangUsaha_Database = dr.Item("Jumlah_Hutang_Usaha")
            If JumlahHutangUsaha_Perhitungan = JumlahHutangUsaha_Database Then
                JumlahHutangUsaha = JumlahHutangUsaha_Database
            Else
                JumlahHutangUsaha = JumlahHutangUsaha_Perhitungan
            End If
            Dim TanggalJatuhTempo_Date As Date
            Dim TanggalInvoice_Date As New Date(Microsoft.VisualBasic.Right(TanggalInvoice, 4),
                    Microsoft.VisualBasic.Mid(TanggalInvoice, 4, 2),
                    Microsoft.VisualBasic.Left(TanggalInvoice, 2))
            If Microsoft.VisualBasic.Right(MasaJatuhTempo, 2) = "ri" Then
                TanggalJatuhTempo_Date = TanggalInvoice_Date.AddDays(AmbilAngka(MasaJatuhTempo))
            Else
                TanggalJatuhTempo_Date = New Date(Microsoft.VisualBasic.Right(MasaJatuhTempo, 4),
                    Microsoft.VisualBasic.Mid(MasaJatuhTempo, 4, 2),
                    Microsoft.VisualBasic.Left(MasaJatuhTempo, 2))
            End If
            If TanggalJatuhTempo_Date >= Today Then
                KeteranganJatuhTempo = JatuhTempo_Belum
            Else
                KeteranganJatuhTempo = JatuhTempo_JT
            End If

            JumlahBayar_TahunLalu = 0
            JumlahBayar_TahunBukuAktif = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP     = '" & NomorBPHU & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR = cmdBAYAR.ExecuteReader
            Dim HutangPPh_TahunLalu = 0
            Dim HutangPPh = 0
            Do While drBAYAR.Read
                If drBAYAR.Item("Tanggal_Bayar") < TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahBayar_TahunLalu += drBAYAR.Item("Jumlah_Bayar")
                    HutangPPh_TahunLalu += drBAYAR.Item("PPh_Dipotong")
                End If
                If drBAYAR.Item("Tanggal_Bayar") >= TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahBayar_TahunBukuAktif += drBAYAR.Item("Jumlah_Bayar")
                    HutangPPh += drBAYAR.Item("PPh_Dipotong")
                End If
            Loop
            JumlahBayar = JumlahBayar_TahunLalu + JumlahBayar_TahunBukuAktif

            SisaHutangUsaha = AmbilAngka(JumlahHutangUsaha) - AmbilAngka(JumlahBayar) - HutangPPh_TahunLalu - HutangPPh
            SisaTagihan = AmbilAngka(TagihanNetto) - AmbilAngka(JumlahBayar)
            Catatan = dr.Item("Catatan")
            NomorJV_Pembelian = dr.Item("Nomor_JV")

            If AngkaInvoice <> AngkaInvoice_Sebelumnya Then
                'Filter Relasi :
                FilterRelasi = False
                Select Case Pilih_JenisRelasi
                    Case Pilihan_Semua
                        FilterRelasi = True
                    Case JenisRelasi_Afiliasi
                        If SupplierSebagaiAfiliasi Then FilterRelasi = True
                    Case JenisRelasi_NonAfiliasi
                        If Not SupplierSebagaiAfiliasi Then FilterRelasi = True
                End Select
                'Filter Jatuh Tempo :
                FilterJatuhTempo = False
                If Pilih_JatuhTempo = JatuhTempo_Semua Then
                    FilterJatuhTempo = True
                Else
                    If Pilih_JatuhTempo = KeteranganJatuhTempo Then FilterJatuhTempo = True
                End If
                If FilterRelasi And FilterJatuhTempo Then TambahBaris()
            End If

            AngkaInvoice_Sebelumnya = AngkaInvoice

        Loop

    End Sub

    Sub TambahBaris()
        If AmbilAngka(SisaTagihan) <= 0 Then KeteranganJatuhTempo = Kosongan
        If DiskonRp = 0 Then DiskonRp = StripKosong
        If PPN = 0 Then PPN = StripKosong
        If PPhTerutang = 0 Then PPhTerutang = StripKosong
        If PPhDitanggung = 0 Then PPhDitanggung = StripKosong
        If PPhDipotong = 0 Then PPhDipotong = StripKosong
        If BiayaTransportasi = 0 Then BiayaTransportasi = StripKosong
        If BiayaMaterai = 0 Then BiayaMaterai = StripKosong
        If Retur = 0 Then Retur = StripKosong
        If JumlahBayar = 0 Then JumlahBayar = StripKosong
        If SisaHutangUsaha = 0 Then SisaHutangUsaha = StripKosong
        If SisaTagihan = 0 Then SisaTagihan = StripKosong
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, NomorBPHU, NomorPembelian, JenisInvoice, JenisProduk,
                                AngkaInvoice, NomorInvoice, NomorFakturPajak, TanggalInvoice, MasaJatuhTempo,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject, NamaProduk, KodeSupplier, NamaSupplier,
                                JumlahHarga, DiskonRp, DasarPengenaanPajak, JenisPPN, PPN, BiayaTransportasi, BiayaMaterai, TagihanBruto, Retur,
                                JumlahHutangUsaha, SisaHutangUsaha, JenisPPh, PPhTerutang, PPhDitanggung, PPhDipotong, TagihanNetto,
                                KeteranganJatuhTempo, JumlahBayar, SisaTagihan, Catatan, NomorJV_Pembelian)
        Total_SisaHutangUsaha += AmbilAngka(SisaHutangUsaha)
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            'Coloring
            If Microsoft.VisualBasic.Left(DataTabelUtama.Item("Nomor_BPHU", BarisIndex).Value, PanjangTeks_AwalanBPHU_PlusTahunBuku) <> AwalanBPHU_PlusTahunBuku Then
                DataTabelUtama.Rows(BarisIndex).DefaultCellStyle.ForeColor = WarnaAlternatif_1
            Else
                DataTabelUtama.Rows(BarisIndex).DefaultCellStyle.ForeColor = WarnaTegas
            End If
        End If
        If QueryTampilan = QueryTampilanHutangTahunLalu Then
            SaldoAwal_BerdasarkanList = SaldoAwal_BerdasarkanList + JumlahHutangUsaha - JumlahBayar_TahunLalu
        End If
        BarisIndex += 1
    End Sub

    Sub BersihkanSeleksi()
        DataTabelUtama.ClearSelection()
        Baris_Terseleksi = -1
        dgv_DetailBayar.Rows.Clear()
        grb_Pembayaran.Visible = False
        btn_LihatJurnal.Enabled = False
        btn_EditHutang.Enabled = False
        btn_HapusHutang.Enabled = False
        NomorJV_Pembayaran_Terseleksi = 0
        If Pilih_JenisProduk_Induk = Pilihan_Semua _
            And Pilih_KodeSupplier = Pilihan_Semua _
            And Pilih_JatuhTempo = Pilihan_Semua _
            Then
            grb_InfoSaldo.Visible = True
            lbl_TotalHutangUsaha.Text = "Total Hutang Usaha :"
        Else
            grb_InfoSaldo.Visible = False
            lbl_TotalHutangUsaha.Text = "Total :"
        End If
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_JenisRelasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisRelasi.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisRelasi_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisRelasi.TextChanged
        Pilih_JenisRelasi = cmb_JenisRelasi.Text
        TampilkanData()
    End Sub
    Private Sub cmb_JenisRelasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisRelasi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_JenisProduk_Induk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk_Induk.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisProduk_Induk_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk_Induk.TextChanged
        Pilih_JenisProduk_Induk = cmb_JenisProduk_Induk.Text
        TampilkanData()
    End Sub
    Private Sub cmb_JenisProduk_Induk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisProduk_Induk.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_Supplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Supplier.SelectedIndexChanged
    End Sub
    Private Sub cmb_Supplier_TextChanged(sender As Object, e As EventArgs) Handles cmb_Supplier.TextChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Supplier.Text & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeSupplier = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Supplier.Text = Pilihan_Semua Then Pilih_KodeSupplier = Pilihan_Semua
        TampilkanData()
    End Sub
    Private Sub cmb_Supplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_Supplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_JatuhTempo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JatuhTempo.SelectedIndexChanged
    End Sub
    Private Sub cmb_JatuhTempo_TextChanged(sender As Object, e As EventArgs) Handles cmb_JatuhTempo.TextChanged
        Pilih_JatuhTempo = cmb_JatuhTempo.Text
        TampilkanData()
    End Sub
    Private Sub cmb_JatuhTempo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JatuhTempo.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click

        'Dim AdaJurnalRetur As Boolean = False

        'frm_JurnalVoucher.ResetForm()
        'frm_JurnalVoucher.FungsiForm = FungsiForm_INFOJURNAL
        'If NomorJV_Pembelian_Terseleksi > 0 Then
        '    If Retur_Terseleksi > 0 Then
        '        AdaJurnalRetur = True
        '    Else
        '        AdaJurnalRetur = False
        '        frm_JurnalVoucher.Angka_NomorJV = NomorJV_Pembelian_Terseleksi
        '        frm_JurnalVoucher.ShowDialog()
        '    End If
        'ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
        '    frm_JurnalVoucher.Angka_NomorJV = NomorJV_Pembayaran_Terseleksi
        '    frm_JurnalVoucher.ShowDialog()
        'Else
        '    MsgBox("Data terpilih BELUM masuk JURNAL.")
        '    Return
        'End If

        'If AdaJurnalRetur = True Then
        '    AksesDatabase_Transaksi(Buka)
        '    cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur " &
        '                          " WHERE Nomor_Invoice_Produk = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        '    dr_ExecuteReader()
        '    dr.Read()
        '    Dim NomorJV_Retur = dr.Item("Nomor_JV")
        '    AksesDatabase_Transaksi(Tutup)
        '    PesanUntukProgrammer("Nanti Form berikut ini harus diganti dengan Pilih Jurnal Pembelian/Retur..!!!")
        '    frm_PilihJurnal_Penjualan_Retur.ResetForm()
        '    frm_PilihJurnal_Penjualan_Retur.NomorJV_Penjualan = NomorJV_Pembelian_Terseleksi
        '    frm_PilihJurnal_Penjualan_Retur.NomorJV_Retur = NomorJV_Retur
        '    frm_PilihJurnal_Penjualan_Retur.ShowDialog()
        'End If
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub btn_Bayar_Click(sender As Object, e As EventArgs) Handles btn_Bayar.Click
        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangUsaha_NonAfiliasi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeSupplier_Terseleksi
        win_InputBuktiPengeluaran.ShowDialog()
    End Sub


    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        AlgoritmaTabel()
    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
    End Sub
    Private Sub DataTabelUtama_SelectionChanged(sender As Object, e As EventArgs) Handles DataTabelUtama.SelectionChanged
        'If ProsesLoadingForm = False Then AlgoritmaTabel()
    End Sub

    Sub AlgoritmaTabel()

        If DataTabelUtama.RowCount = 0 Then Return

        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        NomorBPHU_Terseleksi = DataTabelUtama.Item("Nomor_BPHU", Baris_Terseleksi).Value
        NomorPembelian_Terseleksi = DataTabelUtama.Item("Nomor_Pembelian", Baris_Terseleksi).Value
        JenisInvoice_Terseleksi = DataTabelUtama.Item("Jenis_Invoice", Baris_Terseleksi).Value
        JenisProduk_Terseleksi = DataTabelUtama.Item("Jenis_Produk", Baris_Terseleksi).Value
        AngkaInvoice_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_Invoice", Baris_Terseleksi).Value)
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        MasaJatuhTempo_Terseleksi = DataTabelUtama.Item("Masa_Jatuh_Tempo", Baris_Terseleksi).Value
        NomorSJBAST_Terseleksi = DataTabelUtama.Item("Nomor_SJ_BAST", Baris_Terseleksi).Value
        TanggalSJBAST_Terseleksi = DataTabelUtama.Item("Tanggal_SJ_BAST", Baris_Terseleksi).Value
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        TanggalPO_Terseleksi = DataTabelUtama.Item("Tanggal_PO", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        NamaProduk_Terseleksi = DataTabelUtama.Item("Nama_Produk", Baris_Terseleksi).Value
        KodeSupplier_Terseleksi = DataTabelUtama.Item("Kode_Supplier", Baris_Terseleksi).Value
        NamaSupplier_Terseleksi = DataTabelUtama.Item("Nama_Supplier", Baris_Terseleksi).Value
        JumlahHarga_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Harga", Baris_Terseleksi).Value)
        DiskonRp_Terseleksi = AmbilAngka(DataTabelUtama.Item("Diskon_Rp", Baris_Terseleksi).Value)
        DasarPengenaanPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Dasar_Pengenaan_Pajak", Baris_Terseleksi).Value)
        NomorFakturPajak_Terseleksi = DataTabelUtama.Item("Nomor_Faktur_Pajak", Baris_Terseleksi).Value
        JenisPPN_Terseleksi = DataTabelUtama.Item("Jenis_PPN", Baris_Terseleksi).Value
        PPN_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPN_", Baris_Terseleksi).Value)
        JenisPPh_Terseleksi = DataTabelUtama.Item("Jenis_PPh", Baris_Terseleksi).Value
        PPhTerutang_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Terutang", Baris_Terseleksi).Value)
        PPhDitanggung_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Ditanggung", Baris_Terseleksi).Value)
        PPhDipotong_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Dipotong", Baris_Terseleksi).Value)
        TagihanBruto_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tagihan_Bruto", Baris_Terseleksi).Value)
        Retur_Terseleksi = AmbilAngka(DataTabelUtama.Item("Retur_", Baris_Terseleksi).Value)
        TagihanNetto_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tagihan_Netto", Baris_Terseleksi).Value)
        JumlahHutangUsaha_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Hutang_Usaha", Baris_Terseleksi).Value)
        JumlahBayar_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", Baris_Terseleksi).Value)
        SisaHutangUsaha_Terseleksi = AmbilAngka(DataTabelUtama.Item("Sisa_Hutang_Usaha", Baris_Terseleksi).Value)
        Catatan_Terseleksi = DataTabelUtama.Item("Catatan_", Baris_Terseleksi).Value
        NomorJV_Pembelian_Terseleksi = DataTabelUtama.Item("Nomor_JV_Pembelian", Baris_Terseleksi).Value

        NomorJV_Pembayaran_Terseleksi = 0
        Dim TotalJumlahBayar = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", Baris_Terseleksi).Value)
        If Microsoft.VisualBasic.Left(NomorBPHU_Terseleksi, PanjangTeks_AwalanBPHU_PlusTahunBuku) = AwalanBPHU_PlusTahunBuku Then
            TermasukHutangTahunIni_Terseleksi = True
        Else
            TermasukHutangTahunIni_Terseleksi = False
        End If

        If AngkaInvoice_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            If JudulForm <> JudulForm_SaldoAwalHutangUsaha Then TampilkanData_Pembayaran()
            If TermasukHutangTahunIni_Terseleksi = True Then btn_LihatJurnal.Enabled = True
            btn_EditHutang.Enabled = True
            btn_HapusHutang.Enabled = True
        End If
        If TermasukHutangTahunIni_Terseleksi = False Then btn_LihatJurnal.Enabled = False

    End Sub

    Private Sub btn_InputHutang_Click(sender As Object, e As EventArgs) Handles btn_InputHutang.Click
        frm_Input_InvoicePembelian.ResetForm()
        frm_Input_InvoicePembelian.FungsiForm = FungsiForm_TAMBAH
        frm_Input_InvoicePembelian.InvoiceDenganPO = False
        frm_Input_InvoicePembelian.ShowDialog()
    End Sub

    Private Sub btn_EditHutang_Click(sender As Object, e As EventArgs) Handles btn_EditHutang.Click

        frm_Input_InvoicePembelian.ResetForm()
        frm_Input_InvoicePembelian.FungsiForm = FungsiForm_EDIT
        frm_Input_InvoicePembelian.InvoiceDenganPO = False
        IsiValueForm_InvoicePembelian()
        frm_Input_InvoicePembelian.ShowDialog()

    End Sub

    Sub IsiValueForm_InvoicePembelian()
        ProsesIsiValueForm = True
        frm_Input_InvoicePembelian.AngkaInvoice = AngkaInvoice_Terseleksi
        frm_Input_InvoicePembelian.JenisProduk_Induk = JenisProduk_Terseleksi
        frm_Input_InvoicePembelian.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        frm_Input_InvoicePembelian.NomorPembelian = NomorPembelian_Terseleksi
        If Microsoft.VisualBasic.Right(MasaJatuhTempo_Terseleksi, 2) = "ri" Then
            frm_Input_InvoicePembelian.txt_JumlahHariJatuhTempo.Text = AmbilAngka(MasaJatuhTempo_Terseleksi)
            frm_Input_InvoicePembelian.dtp_TanggalJatuhTempo.Value = Today
            frm_Input_InvoicePembelian.rdb_JumlahHariJatuhTempo.Checked = True
        Else
            frm_Input_InvoicePembelian.txt_JumlahHariJatuhTempo.Text = Kosongan
            frm_Input_InvoicePembelian.dtp_TanggalJatuhTempo.Value = MasaJatuhTempo_Terseleksi
            frm_Input_InvoicePembelian.rdb_TanggalJatuhTempo.Checked = True
        End If
        frm_Input_InvoicePembelian.cmb_JenisInvoice.Text = JenisInvoice_Terseleksi
        frm_Input_InvoicePembelian.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        frm_Input_InvoicePembelian.txt_NamaSupplier.Text = NamaSupplier_Terseleksi
        frm_Input_InvoicePembelian.txt_JumlahHargaKeseluruhan.Text = JumlahHarga_Terseleksi
        frm_Input_InvoicePembelian.ReturDPP = Retur_Terseleksi
        frm_Input_InvoicePembelian.txt_Catatan.Text = Catatan_Terseleksi
        frm_Input_InvoicePembelian.NomorJV = NomorJV_Pembelian_Terseleksi
        frm_Input_InvoicePembelian.NomorFakturPajak = NomorFakturPajak_Terseleksi
        frm_Input_InvoicePembelian.NP = "N"
        frm_Input_InvoicePembelian.dtp_TanggalInvoice.Value = TanggalInvoice_Terseleksi
        IsiTabelProduk_InvoicePembelian()
        IsiTabelSJBAST()
        ProsesIsiValueForm = False
    End Sub

    Sub IsiTabelProduk_InvoicePembelian()
        Dim TanggalDiterimaSJBAST
        Dim JenisProdukPerItem
        Dim COAProdukPerItem
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim SatuanProduk
        Dim HargaSatuan
        Dim JumlahHargaPerItem
        Dim DiskonPerItem_Persen As Decimal
        Dim DiskonPerItem_Rp As Int64  '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
        Dim TotalHargaPerItem
        NomorUrut = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut += 1
            JenisProdukPerItem = dr.Item("Jenis_Produk_Per_Item")
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            TanggalSJBAST = dr.Item("Tanggal_SJ_BAST_Produk")
            TanggalDiterimaSJBAST = dr.Item("Tanggal_Diterima_SJ_BAST_Produk")
            COAProdukPerItem = dr.Item("COA_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            HargaSatuan = dr.Item("Harga_Satuan")
            JumlahHargaPerItem = JumlahProduk * HargaSatuan
            DiskonPerItem_Persen = dr.Item("Diskon_Per_Item")
            DiskonPerItem_Rp = JumlahHargaPerItem * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem = dr.Item("Total_Harga_Per_Item")
            frm_Input_InvoicePembelian.DataTabelUtama.Rows.Add(
                NomorUrut, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, COAProdukPerItem, NamaProduk, DeskripsiProduk,
                JumlahProduk, SatuanProduk, HargaSatuan, JumlahHargaPerItem,
                (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHargaPerItem)
        Loop
        AksesDatabase_Transaksi(Tutup)
    End Sub

    Sub IsiTabelSJBAST()

        NomorSJBAST_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            frm_Input_InvoicePembelian.JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            frm_Input_InvoicePembelian.cmb_JenisPPN.Text = dr.Item("Jenis_PPN")
            frm_Input_InvoicePembelian.cmb_PerlakuanPPN.Text = dr.Item("Perlakuan_PPN")
            Dim Tabel
            Dim KolomNomor
            Dim KolomTanggal
            If Microsoft.VisualBasic.Left(NomorSJBAST, 2) = "SJ" Then
                Tabel = "tbl_Pembelian_SJ"
                KolomNomor = "Nomor_SJ"
                KolomTanggal = "Tanggal_SJ"
            Else
                Tabel = "tbl_Pembelian_BAST"
                KolomNomor = "Nomor_BAST"
                KolomTanggal = "Tanggal_BAST"
            End If
            If NomorSJBAST <> NomorSJBAST_Sebelumnya Then
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM  " & Tabel &
                                             " WHERE " & KolomNomor & " = '" & NomorSJBAST & "' ",
                                             KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    TanggalSJBAST = TanggalFormatTampilan(drTELUSUR.Item(KolomTanggal))
                    TanggalDiterima = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_Diterima"))
                    NomorPO = drTELUSUR.Item("Nomor_PO_Produk")
                End If
                frm_Input_InvoicePembelian.dgv_SJBAST.Rows.Add(NomorSJBAST, TanggalSJBAST, TanggalDiterima, NomorPO)
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST
        Loop
        AksesDatabase_Transaksi(Tutup)

        BeginInvoke(Sub() frm_Input_InvoicePembelian.BersihkanSeleksi_TabelSJBAST())

    End Sub



    Private Sub btn_HapusHutang_Click(sender As Object, e As EventArgs) Handles btn_HapusHutang.Click

        'Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        'If Pilihan = vbNo Then Return

        'AksesDatabase_Transaksi(Buka)

        ''Hapus Data Terpilih Pada Tabel Invoice Pembelian (tbl_Pembelian_Invoice) :
        'cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Pembelian_Invoice " &
        '                           " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        'cmdHAPUS_ExecuteNonQuery()

        'AksesDatabase_Transaksi(Tutup)

        'If StatusSuntingDatabase = True Then
        '    pesan_DataTerpilihBerhasilDihapus()
        '    TampilkanData()
        '    frm_BukuPembelian.TampilkanData()
        'Else
        '    pesan_DataTerpilihGagalDihapus()
        'End If

    End Sub

    Private Sub btn_InputPembayaran_Click(sender As Object, e As EventArgs) Handles btn_InputPembayaran.Click

        If Baris_Terseleksi < 0 Then
            MsgBox("Tidak ada baris data terseleksi.")
            Return
        End If

        If SisaHutangUsaha_Terseleksi <= 0 Then
            MsgBox("Data terpilih sudah LUNAS.")
            Return
        End If

        'If JenisTahunBuku = JenisTahunBuku_NORMAL And
        '    DataTabelUtama.Item("Status_Proses", Baris_Terseleksi).Value = "BELUM DIJURNAL" Then
        '    MsgBox("Data terpilih BELUM masuk JURNAL.")
        '    Return
        'End If

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
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
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeSupplier_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK Then
            TampilkanData()
        End If

        RefreshSetelahBayar()

    End Sub

    Private Sub btn_EditPembayaran_Click(sender As Object, e As EventArgs) Handles btn_EditPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        Dim NominalBayar = AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisBayar_Terseleksi).Value)

        frm_InputPembayaranHutangUsaha.ResetForm()
        frm_InputPembayaranHutangUsaha.FungsiForm = FungsiForm_EDIT
        frm_InputPembayaranHutangUsaha.AlurTransaksi = frm_InputPembayaranHutangUsaha.AlurTransaksi_PembayaranHutangUsaha
        frm_InputPembayaranHutangUsaha.TermasukHutangTahunIni = TermasukHutangTahunIni_Terseleksi
        frm_InputPembayaranHutangUsaha.NomorJVBayar = NomorJV_Pembayaran_Terseleksi
        frm_InputPembayaranHutangUsaha.lbl_PembayaranKe.Text = "Pembayaran Ke-" & (BarisBayar_Terseleksi + 1).ToString
        frm_InputPembayaranHutangUsaha.Referensi = dgv_DetailBayar.Item("NPPHU_", BarisBayar_Terseleksi).Value
        IsiValueFormInputPembayaranHutangUsaha()
        frm_InputPembayaranHutangUsaha.NomorIdBayar = NomorIdPembayaran_Terseleksi
        Dim BarisKe = 0
        Dim HitungBayar = 0
        Do While BarisKe < BarisBayar_Terseleksi
            HitungBayar = HitungBayar + AmbilAngka(dgv_DetailBayar.Item("Nominal_Bayar", BarisKe).Value)
            BarisKe = BarisKe + 1
        Loop
        frm_InputPembayaranHutangUsaha.txt_JumlahTelahDibayar.Text = HitungBayar
        frm_InputPembayaranHutangUsaha.txt_SisaHutang.Text = JumlahHutangUsaha_Terseleksi - HitungBayar
        frm_InputPembayaranHutangUsaha.txt_JumlahBayarSekarang.Text = NominalBayar
        frm_InputPembayaranHutangUsaha.dtp_TanggalBayar.Value = dgv_DetailBayar.Item("Tanggal_Bayar", BarisBayar_Terseleksi).Value
        frm_InputPembayaranHutangUsaha.txt_Keterangan.Text = dgv_DetailBayar.Item("Keterangan_Bayar", BarisBayar_Terseleksi).Value
        'Value untuk Sarana Pembayaran ada di sub Loading Form Input.
        frm_InputPembayaranHutangUsaha.ShowDialog()

        RefreshSetelahBayar()

    End Sub

    Sub RefreshSetelahBayar()
        If frm_InputTransaksi.PenyimpananSukses = True Then
            RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal21 Then frm_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal22 Then frm_BukuPengawasanHutangPPhPasal22.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal23 Then frm_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal42 Then frm_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal25 Then frm_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal26 Then frm_BukuPengawasanHutangPPhPasal26.RefreshTampilanData()
            'If JenisPPh_Terseleksi = JenisPPh_Pasal29 Then frm_BukuPengawasanHutangPPhPasal29.RefreshTampilanData()
        End If
    End Sub

    Sub IsiValueFormInputPembayaranHutangUsaha()

        frm_InputPembayaranHutangUsaha.txt_NomorBPHU.Text = NomorBPHU_Terseleksi
        frm_InputPembayaranHutangUsaha.txt_NomorPembelian.Text = NomorPembelian_Terseleksi
        frm_InputPembayaranHutangUsaha.TanggalInvoice = TanggalInvoice_Terseleksi
        frm_InputPembayaranHutangUsaha.NomorInvoice = NomorInvoice_Terseleksi
        frm_InputPembayaranHutangUsaha.KodeMitra = KodeSupplier_Terseleksi
        frm_InputPembayaranHutangUsaha.NamaMitra = NamaSupplier_Terseleksi
        frm_InputPembayaranHutangUsaha.TanggalFakturPajak = TanggalInvoice_Terseleksi  '(Tanggal Faktur Pajak harus sama dengan Tanggal Invoice)
        frm_InputPembayaranHutangUsaha.NomorFakturPajak = NomorFakturPajak_Terseleksi
        frm_InputPembayaranHutangUsaha.txt_JumlahTerutang.Text = JumlahHutangUsaha_Terseleksi
        frm_InputPembayaranHutangUsaha.JumlahPPh_Total = PPhTerutang_Terseleksi
        frm_InputPembayaranHutangUsaha.JumlahPPhDitanggung_Total = PPhDitanggung_Terseleksi
        frm_InputPembayaranHutangUsaha.JumlahPPhDipotong_Total = PPhDipotong_Terseleksi

    End Sub

    Private Sub btn_HapusPembayaran_Click(sender As Object, e As EventArgs) Handles btn_HapusPembayaran.Click

        FiturBelumBisaDigunakan()
        Return

        Pilihan = MessageBox.Show("Dengan menghapus data terpilih, maka Jurnal yang terkait dengannya akan dihapus pula." & Enter2Baris &
                                  "Yakin akan menghapus..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        TrialBalance_Mentahkan()

        AksesDatabase_Transaksi(Buka)

        'Hapus Data di tbl_PembayaranHutangUsaha :
        cmd = New OdbcCommand(" DELETE FROM tbl_PembayaranHutangUsaha " &
                              " WHERE Nomor_ID = '" & NomorIdPembayaran_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        'Hapus Data di tbl_PengajuanPembayaranHutangUsaha :
        cmd = New OdbcCommand(" DELETE FROM tbl_PengajuanPembayaranHutangUsaha " &
                              " WHERE Nomor_Pengajuan = '" & NPPHU_Terseleksi & "' " &
                              " AND Nomor_BPHU = '" & NomorBPHU_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        'Hapus Data di tbl_Transaksi (Jurnal) :
        If StatusSuntingDatabase = True Then
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                                  " WHERE Nomor_JV = '" & NomorJV_Pembayaran_Terseleksi & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            'TampilkanData()
            'frm_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
            'frm_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
            'Nanti tambahkan Sub Tampilkan data untuk PPh Pasal 4 (2)
            pesan_DataTerpilihBerhasilDihapus_PlusJurnal()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If



    End Sub

    Sub TampilkanData_Pembayaran()

        grb_Pembayaran.Visible = True
        grb_InfoSaldo.Visible = False

        dgv_DetailBayar.Visible = True
        dgv_DetailBayar.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP      = '" & NomorBPHU_Terseleksi & "' " &
                              " AND Status_Invoice  = '" & Status_Dibayar & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            Dim Referensi = dr.Item("Nomor_KK")
            Dim JumlahBayar = dr.Item("Jumlah_Bayar")
            Dim HutangPPh = dr.Item("PPh_Dipotong")
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            dgv_DetailBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, HutangPPh, KeteranganBayar, NomorJVBayar)
        Loop
        AksesDatabase_Transaksi(Tutup)

        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        JumlahBarisBayar = dgv_DetailBayar.RowCount
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False

    End Sub

    Private Sub dgv_DetailBayar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellContentClick
    End Sub
    Private Sub dgv_DetailBayar_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_DetailBayar.ColumnHeaderMouseClick
        dgv_DetailBayar.ClearSelection()
        BarisBayar_Terseleksi = -1
        btn_EditPembayaran.Enabled = False
        btn_HapusPembayaran.Enabled = False
        btn_LihatJurnal.Enabled = False
        NomorJV_Pembelian_Terseleksi = 0
    End Sub
    Private Sub dgv_DetailBayar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_DetailBayar.CellClick
        Try
            BarisBayar_Terseleksi = dgv_DetailBayar.CurrentRow.Index
        Catch ex As Exception
            Return
        End Try
        NomorIdPembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_ID_Bayar", BarisBayar_Terseleksi).Value
        NomorJV_Pembayaran_Terseleksi = dgv_DetailBayar.Item("Nomor_JV_Bayar", BarisBayar_Terseleksi).Value
        NPPHU_Terseleksi = dgv_DetailBayar.Item("NPPHU_", BarisBayar_Terseleksi).Value
        NomorJV_Pembelian_Terseleksi = 0
        If BarisBayar_Terseleksi >= 0 Then
            btn_EditPembayaran.Enabled = True
            btn_HapusPembayaran.Enabled = True
            btn_LihatJurnal.Enabled = True
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then btn_LihatJurnal.Enabled = False
        If NPPHU_Terseleksi = Nothing Then
            btn_EditPembayaran.Enabled = False
            btn_HapusPembayaran.Enabled = False
        End If
    End Sub

    '=======================================================================================================================================
    '==================================== KOLEKSI VARIABEL DAN SUB DARI FORM LAMA (BACKUP) UJUNG AWAL - ====================================
    '=======================================================================================================================================

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean

    Dim Total_SisaHutangUsaha
    Dim SaldoAwal_BerdasarkanList
    Dim SaldoAwal_BerdasarkanCOA
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    Dim SaldoAkhir_BerdasarkanList
    Dim SaldoAkhir_BerdasarkanCOA
    Dim JumlahPenyesuaianSaldo

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        Dim JumlahPenyesuaian_DEBET = 0
        Dim JumlahPenyesuaian_KREDIT = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
                              " WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAwal_BerdasarkanCOA = dr.Item("Saldo_Awal")
        txt_SaldoAwalBerdasarkanCOA.Text = SaldoAwal_BerdasarkanCOA
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                              " WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' " &
                              " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentSaldoAwal & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            If dr.HasRows Then
                JumlahPenyesuaian_DEBET = JumlahPenyesuaian_DEBET + dr.Item("Jumlah_Debet")
                JumlahPenyesuaian_KREDIT = JumlahPenyesuaian_KREDIT + dr.Item("Jumlah_Kredit")
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)
        JumlahPenyesuaianSaldo = JumlahPenyesuaian_KREDIT - JumlahPenyesuaian_DEBET
        SaldoAwal_BerdasarkanCOA_PlusPenyesuaian = SaldoAwal_BerdasarkanCOA + JumlahPenyesuaianSaldo
        txt_AJP.Text = JumlahPenyesuaianSaldo
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA " &
                              " WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        SaldoAkhir_BerdasarkanCOA = dr.Item("Saldo_Awal")
        AksesDatabase_Transaksi(Tutup)
        txt_SaldoBerdasarkanCOA_PlusPenyesuaian.Text = SaldoAkhir_BerdasarkanCOA
    End Sub

    Sub CekKesesuaianSaldoAwal()
        If SaldoAwal_BerdasarkanList = SaldoAwal_BerdasarkanCOA_PlusPenyesuaian Then
            KesesuaianSaldoAwal = True
            btn_Sesuaikan.Enabled = False
            txt_SaldoBerdasarkanList.ForeColor = WarnaTegas
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaTegas
            txt_SelisihSaldo.ForeColor = WarnaTegas
        Else
            KesesuaianSaldoAwal = False
            btn_Sesuaikan.Enabled = True
            txt_SaldoBerdasarkanList.ForeColor = WarnaPeringatan
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaPeringatan
            txt_SelisihSaldo.ForeColor = WarnaPeringatan
        End If
    End Sub

    Sub CekKesesuaianSaldoAkhir()
        If SaldoAkhir_BerdasarkanList = SaldoAkhir_BerdasarkanCOA Then
            KesesuaianSaldoAkhir = True
            btn_Sesuaikan.Enabled = False
            txt_SaldoBerdasarkanList.ForeColor = WarnaTegas
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaTegas
            txt_SelisihSaldo.ForeColor = WarnaTegas
        Else
            KesesuaianSaldoAkhir = False
            btn_Sesuaikan.Enabled = True
            txt_SaldoBerdasarkanList.ForeColor = WarnaPeringatan
            txt_SaldoBerdasarkanCOA_PlusPenyesuaian.ForeColor = WarnaPeringatan
            txt_SelisihSaldo.ForeColor = WarnaPeringatan
        End If
    End Sub


    Public Sub btn_SaldoAwalHutangUsaha_Click(sender As Object, e As EventArgs) Handles btn_SaldoAwalHutangUsaha.Click

        Select Case JudulForm
            Case JudulForm_BukuPengawasanHutangUsaha
                JudulForm = JudulForm_SaldoAwalHutangUsaha
                btn_SaldoAwalHutangUsaha.Text = JudulForm_BukuPengawasanHutangUsaha
                btn_DPHU.Visible = False
                btn_LihatJurnal.Visible = False
                'grb_InfoSaldo.Visible = True
                'DataTabelUtama.Columns("Status_Proses").Visible = False
            Case JudulForm_SaldoAwalHutangUsaha
                JudulForm = JudulForm_BukuPengawasanHutangUsaha
                btn_SaldoAwalHutangUsaha.Text = JudulForm_SaldoAwalHutangUsaha
                btn_DPHU.Visible = True
                btn_LihatJurnal.Visible = True
                'grb_InfoSaldo.Visible = False
                'DataTabelUtama.Columns("Status_Proses").Visible = True
        End Select

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

    End Sub

    Public Sub UpdateNotifikasi()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Notifikasi SET " &
                              " Status_Dibaca = 1, " &
                              " Status_Dieksekusi = 1 " &
                              " WHERE Halaman_Target = '" & Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI & "' " &
                              " AND Pesan = '" & teks_SilakanSesuaikanSaldo & "' ",
                              KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
        frm_BOOKU.IsiKontenNotifikasi()
    End Sub

    Private Sub txt_SaldoBerdasarkanList_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoBerdasarkanList.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoBerdasarkanList)
    End Sub
    Private Sub txt_SaldoBerdasarkanList_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoBerdasarkanList.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub
    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoBerdasarkanCOA_PlusPenyesuaian.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SelisihSaldo_TextChanged(sender As Object, e As EventArgs) Handles txt_SelisihSaldo.TextChanged
        PemecahRibuanUntukTextBox(txt_SelisihSaldo)
    End Sub
    Private Sub txt_SelisihSaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SelisihSaldo.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwalBerdasarkanCOA.TextChanged
        PemecahRibuanUntukTextBox(txt_SaldoAwalBerdasarkanCOA)
    End Sub
    Private Sub txt_SaldoAwalBerdasarkanCOA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwalBerdasarkanCOA.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_AJP_TextChanged(sender As Object, e As EventArgs) Handles txt_AJP.TextChanged
        PemecahRibuanUntukTextBox(txt_AJP)
    End Sub
    Private Sub txt_AJP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AJP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_TotalHutangUsaha_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalHutangUsaha.TextChanged
        PemecahRibuanUntukTextBox(txt_TotalHutangUsaha)
    End Sub
    Private Sub txt_TotalHutangUsaha_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalHutangUsaha.KeyPress
        KunciTotalInputan(sender, e)
    End Sub




    '=======================================================================================================================================
    '===================================== KOLEKSI VARIABEL DAN SUB DARI FORM LAMA (BACKUP) - UJUNG AKHIR ==================================
    '=======================================================================================================================================

End Class