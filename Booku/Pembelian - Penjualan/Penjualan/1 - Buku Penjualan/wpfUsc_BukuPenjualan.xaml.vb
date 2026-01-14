Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Windows.Threading
Imports bcomm

Public Class wpfUsc_BukuPenjualan

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False


    Public KesesuaianJurnal As Boolean
    Public JudulForm
    Dim QueryTampilan

    'Variabel Kolom Tabel :
    Public Index_BarisTabel
    Public NomorUrut
    Public NomorPenjualan
    Public JenisInvoice
    Public JenisProduk
    Public AngkaInvoice
    Public NomorInvoice_Sebelumnya
    Public NomorInvoice
    Public NomorFakturPajak
    Public NP
    Public TanggalInvoice
    Public TahunInvoice As Integer
    Public TanggalPembetulan
    Public MasaJatuhTempo
    Public NomorSJBAST
    Public TanggalSJBAST
    Public TanggalDiterima
    Public NomorPO
    Public TanggalPO
    Public KodeProject
    Public KodeCustomer
    Public NamaCustomer
    Public KodeMataUang
    Public Kurs As Decimal
    Public JumlahHarga
    Public JumlahHarga_Asing As Decimal
    Public DiskonRp
    Public DiskonAsing As Decimal
    Public DasarPengenaanPajak
    Public JenisPPN
    Public PerlakuanPPN
    Public PPN
    Public PPhDipotong
    Public TagihanBruto
    Public TagihanBruto_Asing As Decimal
    Public BiayaLainnya
    Public BiayaLainnya_Asing As Decimal
    Public Retur
    Public TagihanNetto
    Public KeteranganJatuhTempo
    Public KodeFP
    Public Catatan
    Public NomorJV

    'Variabel Rekap :
    Dim Rekap_JumlahHarga
    Dim Rekap_DiskonRp
    Dim Rekap_DasarPengenaanPajak
    Dim Rekap_PPN
    Dim Rekap_PPhDipotong
    Dim Rekap_BiayaLainnya
    Dim Rekap_TagihanBruto
    Dim Rekap_Retur
    Dim Rekap_TagihanNetto

    Dim Rekap_JumlahHarga_Asing As Decimal
    Dim Rekap_DiskonAsing As Decimal
    Dim Rekap_BiayaLainnya_Asing As Decimal
    Dim Rekap_TagihanBruto_Asing As Decimal

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorPenjualan_Terseleksi
    Dim JenisInvoice_Terseleksi
    Dim JenisProduk_Terseleksi
    Dim AngkaInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim NP_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim TanggalPembetulan_Terseleksi
    Dim MasaJatuhTempo_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim TanggalSJBAST_Terseleksi
    Dim NomorPO_Terseleksi
    Dim TanggalPO_Terseleksi
    Dim KodeProject_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim JumlahHarga_Terseleksi
    Dim DiskonRp_Terseleksi
    Dim DasarPengenaanPajak_Terseleksi
    Dim JenisPPN_Terseleksi
    Dim PerlakuanPPN_Terseleksi
    Dim PPN_Terseleksi
    Dim PPhDipotong_Terseleksi
    Dim TagihanBruto_Terseleksi
    Dim Retur_Terseleksi
    Dim TagihanNetto_Terseleksi
    Dim KodeFP_Terseleksi
    Dim Catatan_Terseleksi
    Dim NomorJV_Terseleksi

    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Filter :
    Dim Pilih_JenisProduk_Induk
    Dim Pilih_KodeCustomer
    Dim Pilih_JatuhTempo

    'Jenis Tampilan :
    Public JenisTampilan
    Public JenisTampilan_Semua = "Semua"
    Public JenisTampilan_HasilAkhir = "Hasil Akhir"

    'Jenis Penjualan :
    Public JenisPenjualan
    Public JenisPenjualan_Rutin = "Rutin"
    Public JenisPenjualan_Asset = "Asset"

    Public DestinasiPenjualan As String
    Dim PenjualanLokal As Boolean
    Dim PenjualanEkspor As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        lbl_JudulForm.Text = JudulForm
        Terabas()

        lbl_TotalTabel.Visibility = Visibility.Collapsed
        txt_TotalTabel.Visibility = Visibility.Collapsed

        ProsesLoadingForm = True

        lbl_TotalTabel.Visibility = Visibility.Collapsed
        txt_TotalTabel.Visibility = Visibility.Collapsed

        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            PenjualanLokal = True
            PenjualanEkspor = False
            lbl_FilterJenisProduk.Visibility = Visibility.Visible
            cmb_JenisProduk_Induk.Visibility = Visibility.Visible
            brd_FilterJenisProduk.Visibility = Visibility.Visible
        Else
            PenjualanLokal = False
            PenjualanEkspor = True
            lbl_FilterJenisProduk.Visibility = Visibility.Collapsed
            cmb_JenisProduk_Induk.Visibility = Visibility.Collapsed
            brd_FilterJenisProduk.Visibility = Visibility.Collapsed
        End If

        LogikaDestinasiPenjualan()

        If JenisPenjualan = Kosongan Then JenisPenjualan = JenisPenjualan_Rutin
        If JenisPenjualan = JenisPenjualan_Rutin Then JudulForm = "Buku Penjualan"
        If JenisPenjualan = JenisPenjualan_Asset Then JudulForm = "Buku Penjualan Asset Tetap"

        If PenjualanLokal And Not PerusahaanSebagaiPKP Then
            Nomor_Faktur_Pajak.Visibility = Visibility.Collapsed
        Else
            Nomor_Faktur_Pajak.Visibility = Visibility.Visible
        End If

        ProsesLoadingForm = False

        RefreshTampilanData()

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_JenisTampilan()
        KontenCombo_JenisProduk_Induk()
        Select Case JudulForm
            Case host_BukuPenjualan_Lokal.JudulForm
                cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
            Case host_BukuPenjualan_Lokal.JudulForm
                IsiValueComboBypassTerkunci(cmb_JenisProduk_Induk, JenisProduk_Barang)
        End Select
        KontenCombo_Customer()
        KontenCombo_JatuhTempo()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Dim EksekusiTampilanData
    Sub TampilkanData()

        If EksekusiTampilanData = False Then Return

        KetersediaanMenuHalaman(pnl_Halaman, False)
        If JenisPenjualan = Kosongan Then JenisPenjualan = JenisPenjualan_Rutin

        KesesuaianJurnal = True

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Filter Jenis Produk Induk :
        Dim FilterJenisProduk_Induk = " "
        If cmb_JenisProduk_Induk.SelectedValue <> JenisProduk_Semua Then FilterJenisProduk_Induk = " AND Jenis_Produk_Induk = '" & Pilih_JenisProduk_Induk & "' "

        'Filter Customer :
        Dim FilterCustomer = " "
        If cmb_Customer.SelectedValue <> Pilihan_Semua Then FilterCustomer = " AND Kode_Customer = '" & Pilih_KodeCustomer & "' "

        'Filter Jatuh Tempo :
        Dim FilterJatuhTempo = " "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterCustomer & FilterJatuhTempo

        'Query Tampilan :
        If JenisPenjualan = JenisPenjualan_Rutin Then
            QueryTampilan = " SELECT * FROM tbl_Penjualan_Invoice " &
            " WHERE Nomor_Invoice <> 'X' " & FilterData & " ORDER BY Angka_Invoice "
        End If

        If JenisPenjualan = JenisPenjualan_Asset Then
            QueryTampilan = " SELECT * FROM tbl_Penjualan_Invoice " &
            " WHERE Asset = 1 " & FilterData & " ORDER BY Angka_Invoice "
        End If

        'Data Tabel :
        Index_BarisTabel = 0
        NomorUrut = 0
        NomorInvoice_Sebelumnya = Kosongan

        'Total :
        Rekap_JumlahHarga = 0
        Rekap_DiskonRp = 0
        Rekap_DasarPengenaanPajak = 0
        Rekap_PPN = 0
        Rekap_PPhDipotong = 0
        Rekap_BiayaLainnya = 0
        Rekap_TagihanBruto = 0
        Rekap_Retur = 0
        Rekap_TagihanNetto = 0

        Rekap_JumlahHarga_Asing = 0
        Rekap_DiskonAsing = 0
        Rekap_BiayaLainnya_Asing = 0
        Rekap_TagihanBruto_Asing = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            NomorPO = Kosongan
            TanggalPO = Kosongan
            NomorSJBAST = Kosongan
            NomorSJBAST_Satuan = Kosongan
            NomorSJBAST_Sebelumnya = Kosongan
            TanggalSJBAST = Kosongan
            NomorPenjualan = dr.Item("Nomor_Penjualan")
            AngkaInvoice = dr.Item("Angka_Invoice")
            JenisInvoice = dr.Item("Jenis_Invoice")
            JenisProduk = dr.Item("Jenis_Produk_Induk")
            NomorInvoice = dr.Item("Nomor_Invoice")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            NP = dr.Item("N_P")
            Dim NomorInvoice_Pembetulan = Kosongan
            Dim NP_Pembetulan = Kosongan
            If NP = "N" Then
                NomorInvoice_Pembetulan = NomorInvoice & "-P1"
            Else
                Dim PembetulanKe = AmbilAngka(NP)
                NP_Pembetulan = "P" & (PembetulanKe + 1)
                NomorInvoice_Pembetulan = Replace(NomorInvoice, NP, NP_Pembetulan)
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                         " WHERE Nomor_Invoice = '" & NomorInvoice_Pembetulan & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            If drTELUSUR.HasRows And JenisTampilan = JenisTampilan_HasilAkhir Then Continue Do
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            TahunInvoice = Right(TanggalInvoice, 4)
            If NP = "N" Then
                TanggalPembetulan = StripKosong
            Else
                TanggalPembetulan = TanggalFormatTampilan(dr.Item("Tanggal_Pembetulan"))
            End If
            MasaJatuhTempo = dr.Item("Jumlah_Hari_Jatuh_Tempo")
            If MasaJatuhTempo > 0 Then
                MasaJatuhTempo &= " hari"
            Else
                MasaJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            End If
            If dr.Item("Metode_Pembayaran") = MetodePembayaran_Termin Or CustomerSebagaiPerusahaanLuarNegeri(KodeCustomer) Then
                NomorPO = dr.Item("Nomor_PO_Produk")
                If NomorPO <> Kosongan Then TanggalPO = TanggalFormatTampilan(AmbilValue_TanggalPOBerdasarkanNomorPOPenjualan(NomorPO))
            Else
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice WHERE " &
                             " Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
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
                                KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                            Else
                                NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                                TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
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
                    NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
                Loop
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
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
            KodeMataUang = dr.Item("Kode_Mata_Uang")
            Kurs = dr.Item("Kurs")
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
            PPN = dr.Item("PPN")
            If PenjualanLokal Then
                PPhDipotong = dr.Item("PPh_Dipotong")
            Else
                PPhDipotong = 0
            End If
            BiayaLainnya = dr.Item("Biaya_Transportasi")
            BiayaLainnya_Asing = dr.Item("Freight") + dr.Item("Insurance")
            TagihanBruto = dr.Item("Total_Tagihan")
            TagihanBruto_Asing = dr.Item("Total_Tagihan")
            Retur = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
            TagihanNetto = TagihanBruto - Retur
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
            KodeFP = Left(NomorFakturPajak, 2)
            Catatan = PenghapusEnter(dr.Item("Catatan"))
            NomorJV = dr.Item("Nomor_JV")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then
                If Pilih_JatuhTempo = JatuhTempo_Semua Then
                    TambahBaris()
                Else
                    If Pilih_JatuhTempo = KeteranganJatuhTempo Then TambahBaris()
                End If
            End If
            NomorInvoice_Sebelumnya = NomorInvoice
        Loop

        AksesDatabase_Transaksi(Tutup)

        If JenisTampilan = JenisTampilan_HasilAkhir Then
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan,
                                Rekap_JumlahHarga, Rekap_JumlahHarga_Asing, Rekap_DiskonRp, Rekap_DiskonAsing, Rekap_DasarPengenaanPajak, Kosongan, Kosongan,
                                Rekap_PPN, Rekap_PPhDipotong, Rekap_BiayaLainnya, Rekap_BiayaLainnya_Asing, Rekap_TagihanBruto, Rekap_TagihanBruto_Asing, Rekap_Retur, Rekap_TagihanNetto,
                                Kosongan)
        End If

        BersihkanSeleksi()

    End Sub



    Sub TambahBaris()
        If TahunInvoice <> TahunBukuAktif Then Return
        If PenjualanLokal And MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then Return
        If PenjualanEkspor And Not MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then Return
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, NomorPenjualan, JenisInvoice, JenisProduk, AngkaInvoice, NomorInvoice, NP, NomorFakturPajak, TanggalInvoice, TanggalPembetulan,
                                KodeCustomer, NamaCustomer, KodeMataUang,
                                JumlahHarga, JumlahHarga_Asing, DiskonRp, DiskonAsing, DasarPengenaanPajak, JenisPPN, PerlakuanPPN, PPN, PPhDipotong,
                                BiayaLainnya, BiayaLainnya_Asing, TagihanBruto, TagihanBruto_Asing, Retur, TagihanNetto,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject,
                                MasaJatuhTempo, KeteranganJatuhTempo, KodeFP, Catatan, NomorJV)
        Terabas()
        'Akumulasi/Total :
        Rekap_JumlahHarga += JumlahHarga
        Rekap_JumlahHarga_Asing += JumlahHarga_Asing
        Rekap_DiskonRp += DiskonRp
        Rekap_DiskonAsing += DiskonAsing
        Rekap_DasarPengenaanPajak += DasarPengenaanPajak
        Rekap_PPN += PPN
        Rekap_PPhDipotong += PPhDipotong
        Rekap_BiayaLainnya += BiayaLainnya
        Rekap_BiayaLainnya_Asing += BiayaLainnya_Asing
        Rekap_TagihanBruto += TagihanBruto
        Rekap_TagihanBruto_Asing += TagihanBruto_Asing
        Rekap_Retur += Retur
        Rekap_TagihanNetto += TagihanNetto
        Index_BarisTabel += 1
    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Cetak.IsEnabled = False
        btn_DorongKeJurnal.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub




    Sub KontenCombo_JenisTampilan()
        cmb_JenisTampilan.Items.Clear()
        cmb_JenisTampilan.Items.Add(JenisTampilan_Semua)
        cmb_JenisTampilan.Items.Add(JenisTampilan_HasilAkhir)
        cmb_JenisTampilan.SelectedValue = JenisTampilan_Semua
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
        cmb_Customer.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        Dim FilterLokasiWP As String = Kosongan
        If PenjualanLokal Then FilterLokasiWP = " AND Lokasi_WP = '" & LokasiPS_DalamNegeri & "' "
        If PenjualanEkspor Then FilterLokasiWP = " AND Lokasi_WP = '" & LokasiPS_LuarNegeri & "' "
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Customer = 1 " & FilterLokasiWP, KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_Customer.Items.Add(Pilihan_Semua)
        Do While dr.Read
            cmb_Customer.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        cmb_Customer.SelectedValue = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub

    Sub KontenCombo_JatuhTempo()
        cmb_JatuhTempo.Items.Clear()
        cmb_JatuhTempo.Items.Add(JatuhTempo_Semua)
        cmb_JatuhTempo.Items.Add(JatuhTempo_Belum)
        cmb_JatuhTempo.Items.Add(JatuhTempo_JT)
        cmb_JatuhTempo.SelectedValue = JatuhTempo_Semua
    End Sub


    Sub LogikaDestinasiPenjualan()
        'Lokal :
        Kode_Mata_Uang.Visibility = Visibility.Collapsed
        Nomor_Faktur_Pajak.Header = "Nomor" & Enter1Baris & "Faktur Pajak"
        Jumlah_Harga.Visibility = Visibility.Visible
        Diskon_Rp.Visibility = Visibility.Visible
        Dasar_Pengenaan_Pajak.Visibility = Visibility.Visible
        PPN_.Visibility = Visibility.Visible
        PPh_Dipotong.Visibility = Visibility.Visible
        Biaya_Lainnya.Visibility = Visibility.Visible
        Tagihan_Bruto.Visibility = Visibility.Visible
        Nomor_SJ_BAST.Visibility = Visibility.Visible
        Tanggal_SJ_BAST.Visibility = Visibility.Visible
        Retur_.Visibility = Visibility.Visible
        Tagihan_Netto.Visibility = Visibility.Visible
        'Asing :
        Jumlah_Harga_Asing.Visibility = Visibility.Collapsed
        Diskon_Asing.Visibility = Visibility.Collapsed
        Biaya_Lainnya_Asing.Visibility = Visibility.Collapsed
        Tagihan_Bruto_Asing.Visibility = Visibility.Collapsed
        If PenjualanEkspor Then
            'Lokal :
            Kode_Mata_Uang.Visibility = Visibility.Visible
            Nomor_Faktur_Pajak.Header = "Nomor PEB"
            Jumlah_Harga.Visibility = Visibility.Collapsed
            Diskon_Rp.Visibility = Visibility.Collapsed
            Dasar_Pengenaan_Pajak.Visibility = Visibility.Collapsed
            PPN_.Visibility = Visibility.Collapsed
            PPh_Dipotong.Visibility = Visibility.Collapsed
            Biaya_Lainnya.Visibility = Visibility.Collapsed
            Tagihan_Bruto.Visibility = Visibility.Collapsed
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Retur_.Visibility = Visibility.Collapsed
            Tagihan_Netto.Visibility = Visibility.Collapsed
            'Asing :
            Jumlah_Harga_Asing.Visibility = Visibility.Visible
            Diskon_Asing.Visibility = Visibility.Visible
            Biaya_Lainnya_Asing.Visibility = Visibility.Visible
            Tagihan_Bruto_Asing.Visibility = Visibility.Visible
        End If
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Jurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_DorongKeJurnal.Click

        Select Case AmbilValue_JenisPenjualanBerdasarkanInvoicePenjualan(NomorInvoice_Terseleksi)
            Case JenisPenjualan_Tunai
                win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
                win_InputBuktiPenerimaan.ResetForm()
                ProsesIsiValueForm = True
                win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
                win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
                win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PenerimaanTunai
                win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
                win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_InvoiceTunai
                win_InputBuktiPenerimaan.NomorBP = NomorInvoice_Terseleksi
                win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeCustomer_Terseleksi
                ProsesIsiValueForm = False
                win_InputBuktiPenerimaan.ShowDialog()
                If win_InputBuktiPenerimaan.DialogResult = DialogResult.OK Then
                    TampilkanData()
                End If
            Case JenisPenjualan_Tempo
                win_DorongInvoiceKeJurnal = New wpfWin_DorongInvoiceKeJurnal
                win_DorongInvoiceKeJurnal.ResetForm()
                win_DorongInvoiceKeJurnal.NomorInvoice = NomorInvoice_Terseleksi
                win_DorongInvoiceKeJurnal.NP = NP_Terseleksi
                win_DorongInvoiceKeJurnal.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
                win_DorongInvoiceKeJurnal.ShowDialog()
        End Select

    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Cetak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Cetak.Click
        Cetak(JenisFormCetak_Invoice, NomorInvoice_Terseleksi, True, False)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub cmb_JenisTampilan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisTampilan.SelectionChanged
        JenisTampilan = cmb_JenisTampilan.SelectedValue
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
        JenisInvoice_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jenis_Invoice")
        NomorPenjualan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Penjualan"))
        JenisProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jenis_Produk")
        AngkaInvoice_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Angka_Invoice"))
        NomorInvoice_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Invoice")
        NomorFakturPajak_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Faktur_Pajak")
        NP_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "N_P")
        TanggalInvoice_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Invoice")
        TanggalPembetulan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Pembetulan")
        MasaJatuhTempo_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Masa_Jatuh_Tempo")
        NomorSJBAST_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_SJ_BAST")
        TanggalSJBAST_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_SJ_BAST")
        NomorPO_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_PO")
        TanggalPO_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_PO")
        KodeProject_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Project")
        KodeCustomer_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Customer")
        NamaCustomer_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Customer")
        JumlahHarga_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Harga"))
        DiskonRp_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Rp"))
        DasarPengenaanPajak_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Dasar_Pengenaan_Pajak"))
        JenisPPN_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jenis_PPN")
        PerlakuanPPN_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Perlakuan_PPN")
        PPN_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "PPN_"))
        PPhDipotong_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "PPh_Dipotong"))
        TagihanBruto_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tagihan_Bruto"))
        Retur_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Retur_"))
        TagihanNetto_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tagihan_Netto"))
        KodeFP_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_FP")
        Catatan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Catatan_")
        NomorJV_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV")

        If AngkaInvoice_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            btn_Cetak.IsEnabled = True
            btn_DorongKeJurnal.IsEnabled = True
            If NomorJV_Terseleksi = 0 Then
                btn_DorongKeJurnal.IsEnabled = True
                btn_LihatJurnal.IsEnabled = False
            Else
                btn_DorongKeJurnal.IsEnabled = False
                btn_LihatJurnal.IsEnabled = True
            End If
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Cetak_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If AmbilAngka(e.Row.Item("Nomor_JV")) > 0 Then
            e.Row.Foreground = clrTeksPrimer
        Else
            e.Row.Foreground = clrNeutral500
        End If
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
    Dim Nomor_Penjualan As New DataGridTextColumn
    Dim Jenis_Invoice As New DataGridTextColumn
    Dim Jenis_Produk As New DataGridTextColumn
    Dim Angka_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim N_P As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Tanggal_Pembetulan As New DataGridTextColumn
    Dim Kode_Customer As New DataGridTextColumn
    Dim Nama_Customer As New DataGridTextColumn
    Dim Kode_Mata_Uang As New DataGridTextColumn
    Dim Jumlah_Harga As New DataGridTextColumn
    Dim Jumlah_Harga_Asing As New DataGridTextColumn
    Dim Diskon_Rp As New DataGridTextColumn
    Dim Diskon_Asing As New DataGridTextColumn
    Dim Dasar_Pengenaan_Pajak As New DataGridTextColumn
    Dim Jenis_PPN As New DataGridTextColumn
    Dim Perlakuan_PPN As New DataGridTextColumn
    Dim PPN_ As New DataGridTextColumn
    Dim PPh_Dipotong As New DataGridTextColumn
    Dim Biaya_Lainnya As New DataGridTextColumn
    Dim Biaya_Lainnya_Asing As New DataGridTextColumn
    Dim Tagihan_Bruto As New DataGridTextColumn
    Dim Tagihan_Bruto_Asing As New DataGridTextColumn
    Dim Retur_ As New DataGridTextColumn
    Dim Tagihan_Netto As New DataGridTextColumn
    Dim Nomor_SJ_BAST As New DataGridTextColumn
    Dim Tanggal_SJ_BAST As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Tanggal_PO As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Masa_Jatuh_Tempo As New DataGridTextColumn
    Dim Keterangan_Jatuh_Tempo As New DataGridTextColumn
    Dim Kode_FP As New DataGridTextColumn
    Dim Catatan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_Penjualan")
        datatabelUtama.Columns.Add("Jenis_Invoice")
        datatabelUtama.Columns.Add("Jenis_Produk")
        datatabelUtama.Columns.Add("Angka_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("N_P")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Pembetulan")
        datatabelUtama.Columns.Add("Kode_Customer")
        datatabelUtama.Columns.Add("Nama_Customer")
        datatabelUtama.Columns.Add("Kode_Mata_Uang")
        datatabelUtama.Columns.Add("Jumlah_Harga", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Harga_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Diskon_Rp", GetType(Int64))
        datatabelUtama.Columns.Add("Diskon_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Dasar_Pengenaan_Pajak", GetType(Int64))
        datatabelUtama.Columns.Add("Jenis_PPN")
        datatabelUtama.Columns.Add("Perlakuan_PPN")
        datatabelUtama.Columns.Add("PPN_", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Dipotong", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Lainnya", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Lainnya_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Tagihan_Bruto", GetType(Int64))
        datatabelUtama.Columns.Add("Tagihan_Bruto_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Retur_", GetType(Int64))
        datatabelUtama.Columns.Add("Tagihan_Netto", GetType(Int64))
        datatabelUtama.Columns.Add("Nomor_SJ_BAST")
        datatabelUtama.Columns.Add("Tanggal_SJ_BAST")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Tanggal_PO")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Masa_Jatuh_Tempo")
        datatabelUtama.Columns.Add("Keterangan_Jatuh_Tempo")
        datatabelUtama.Columns.Add("Kode_FP")
        datatabelUtama.Columns.Add("Catatan_")
        datatabelUtama.Columns.Add("Nomor_JV", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Penjualan, "Nomor_Penjualan", "Nomor Penjualan", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Invoice, "Jenis_Invoice", "Jenis Invoice", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Produk, "Jenis_Produk", "Jenis Produk", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angka_Invoice, "Angka_Invoice", "Angka Invoice", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, N_P, "N_P", "N/P", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pembetulan, "Tanggal_Pembetulan", "Tanggal Pembetulan", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Customer, "Kode_Customer", "Kode Customer", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Customer, "Nama_Customer", "Nama Customer", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mata_Uang, "Kode_Mata_Uang", "Mata Uang", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga, "Jumlah_Harga", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Asing, "Jumlah_Harga_Asing", "Jumlah Harga", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Rp, "Diskon_Rp", "Diskon", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Asing, "Diskon_Asing", "Diskon", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Dasar_Pengenaan_Pajak, "Dasar_Pengenaan_Pajak", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PPN, "Jenis_PPN", "Jenis PPN", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Perlakuan_PPN, "Perlakuan_PPN", "Perlakuan PPN", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_, "PPN_", "PPN", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong, "PPh_Dipotong", "PPh Dipotong", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Lainnya, "Biaya_Lainnya", "Biaya Lainnya", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Lainnya_Asing, "Biaya_Lainnya_Asing", "Biaya Lainnya", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Bruto, "Tagihan_Bruto", "Tagihan Bruto", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Bruto_Asing, "Tagihan_Bruto_Asing", "Tagihan Bruto", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Retur_, "Retur_", "Retur", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Netto, "Tagihan_Netto", "Tagihan Netto", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ_BAST, "Nomor_SJ_BAST", "Nomor SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ_BAST, "Tanggal_SJ_BAST", "Tanggal SJ/BAST", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PO, "Tanggal_PO", "Tanggal PO", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Masa_Jatuh_Tempo, "Masa_Jatuh_Tempo", "Jatuh Tempo", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_Jatuh_Tempo, "Keterangan_Jatuh_Tempo", "Keterangan Jatuh Tempo", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_FP, "Kode_FP", "Kode FP", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Catatan_, "Catatan_", "Catatan", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub









    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
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

    Public NomorInvoiceLama As String

    Sub UpdateBaris()
        If TahunInvoice <> TahunBukuAktif Then Return
        For Each row As DataRow In datatabelUtama.Rows
            If row("Nomor_Invoice").ToString() = NomorInvoiceLama Then
                ' Simpan nilai lama untuk update rekap
                Dim JumlahHarga_Lama As Int64 = AmbilAngka(row("Jumlah_Harga"))
                Dim JumlahHarga_Asing_Lama As Decimal = AmbilAngka_Asing(row("Jumlah_Harga_Asing"))
                Dim DiskonRp_Lama As Int64 = AmbilAngka(row("Diskon_Rp"))
                Dim DiskonAsing_Lama As Decimal = AmbilAngka_Asing(row("Diskon_Asing"))
                Dim DasarPengenaanPajak_Lama As Int64 = AmbilAngka(row("Dasar_Pengenaan_Pajak"))
                Dim PPN_Lama As Int64 = AmbilAngka(row("PPN_"))
                Dim PPhDipotong_Lama As Int64 = AmbilAngka(row("PPh_Dipotong"))
                Dim BiayaLainnya_Lama As Int64 = AmbilAngka(row("Biaya_Lainnya"))
                Dim BiayaLainnya_Asing_Lama As Decimal = AmbilAngka_Asing(row("Biaya_Lainnya_Asing"))
                Dim TagihanBruto_Lama As Int64 = AmbilAngka(row("Tagihan_Bruto"))
                Dim TagihanBruto_Asing_Lama As Decimal = AmbilAngka_Asing(row("Tagihan_Bruto_Asing"))
                Dim Retur_Lama As Int64 = AmbilAngka(row("Retur_"))
                Dim TagihanNetto_Lama As Int64 = AmbilAngka(row("Tagihan_Netto"))

                ' Update kolom-kolom
                row("Nomor_Penjualan") = NomorPenjualan
                row("Jenis_Invoice") = JenisInvoice
                row("Jenis_Produk") = JenisProduk
                row("Angka_Invoice") = AngkaInvoice
                row("Nomor_Invoice") = NomorInvoice
                row("N_P") = NP
                row("Nomor_Faktur_Pajak") = NomorFakturPajak
                row("Tanggal_Invoice") = TanggalInvoice
                row("Tanggal_Pembetulan") = TanggalPembetulan
                row("Kode_Customer") = KodeCustomer
                row("Nama_Customer") = NamaCustomer
                row("Kode_Mata_Uang") = KodeMataUang
                row("Jumlah_Harga") = JumlahHarga
                row("Jumlah_Harga_Asing") = JumlahHarga_Asing
                row("Diskon_Rp") = DiskonRp
                row("Diskon_Asing") = DiskonAsing
                row("Dasar_Pengenaan_Pajak") = DasarPengenaanPajak
                row("Jenis_PPN") = JenisPPN
                row("Perlakuan_PPN") = PerlakuanPPN
                row("PPN_") = PPN
                row("PPh_Dipotong") = PPhDipotong
                row("Biaya_Lainnya") = BiayaLainnya
                row("Biaya_Lainnya_Asing") = BiayaLainnya_Asing
                row("Tagihan_Bruto") = TagihanBruto
                row("Tagihan_Bruto_Asing") = TagihanBruto_Asing
                row("Retur_") = Retur
                row("Tagihan_Netto") = TagihanNetto
                row("Nomor_SJ_BAST") = NomorSJBAST
                row("Tanggal_SJ_BAST") = TanggalSJBAST
                row("Nomor_PO") = NomorPO
                row("Tanggal_PO") = TanggalPO
                row("Kode_Project") = KodeProject
                row("Masa_Jatuh_Tempo") = MasaJatuhTempo
                row("Keterangan_Jatuh_Tempo") = KeteranganJatuhTempo
                row("Kode_FP") = KodeFP
                row("Catatan_") = Catatan
                row("Nomor_JV") = NomorJV

                ' Update rekap (selisih nilai baru - nilai lama)
                Rekap_JumlahHarga += (JumlahHarga - JumlahHarga_Lama)
                Rekap_JumlahHarga_Asing += (JumlahHarga_Asing - JumlahHarga_Asing_Lama)
                Rekap_DiskonRp += (DiskonRp - DiskonRp_Lama)
                Rekap_DiskonAsing += (DiskonAsing - DiskonAsing_Lama)
                Rekap_DasarPengenaanPajak += (DasarPengenaanPajak - DasarPengenaanPajak_Lama)
                Rekap_PPN += (PPN - PPN_Lama)
                Rekap_PPhDipotong += (PPhDipotong - PPhDipotong_Lama)
                Rekap_BiayaLainnya += (BiayaLainnya - BiayaLainnya_Lama)
                Rekap_BiayaLainnya_Asing += (BiayaLainnya_Asing - BiayaLainnya_Asing_Lama)
                Rekap_TagihanBruto += (TagihanBruto - TagihanBruto_Lama)
                Rekap_TagihanBruto_Asing += (TagihanBruto_Asing - TagihanBruto_Asing_Lama)
                Rekap_Retur += (Retur - Retur_Lama)
                Rekap_TagihanNetto += (TagihanNetto - TagihanNetto_Lama)

                Exit For
            End If
        Next
    End Sub

End Class
