Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm


Public Class wpfUsc_InvoicePenjualan

    Public StatusAktif As Boolean
    Private SudahDimuat As Boolean = False
    Public JudulForm As String

    Public KesesuaianJurnal As Boolean
    Public JenisProduk_Menu
    Public InvoiceDenganPO As Boolean

    Public MetodePembayaran

    'Variabel Tabel :
    Public Index_BarisTabel
    Public NomorUrut
    Public JenisInvoice
    Public JenisProduk
    Public AngkaInvoice
    Public NomorInvoice_Sebelumnya
    Public NomorInvoice
    Public NomorPenjualan
    Public NP
    Public TanggalInvoice
    Public TahunInvoice As Integer
    Public TanggalPembetulan
    Public Tanggallapor
    Public JatuhTempo
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
    Public JumlahHarga As Int64
    Public JumlahHarga_Asing As Decimal
    Public DiskonRp As Int64
    Public DiskonAsing As Decimal
    Public BasisPerhitunganTermin As String
    Public ProsentaseTermin_String
    Public DasarPengenaanPajak
    Public NomorFakturPajak
    Public JenisPPN
    Public PPN
    Public PPhDipotong
    Public TagihanKotor As Int64
    Public TagihanKotor_Asing As Decimal
    Public BiayaLainnya As Int64
    Public BiayaLainnya_Asing As Decimal
    Public TotalTagihan As Int64
    Public TotalTagihan_Asing As Decimal
    Public ReturDPP
    Public ReturPPN
    Public Retur
    Public InvoicePenjualanAsset
    Public Catatan
    Public NomorJV

    'Variabel Rekap :
    Dim Rekap_JumlahHarga
    Dim Rekap_DiskonRp
    Dim Rekap_DasarPengenaanPajak
    Dim Rekap_PPN
    Dim Rekap_TagihanKotor
    Dim Rekap_PPhDipotong
    Dim Rekap_BiayaLainnya
    Dim Rekap_TotalTagihan
    Dim Rekap_Retur

    Dim Rekap_JumlahHarga_Asing As Decimal
    Dim Rekap_DiskonAsing As Decimal
    Dim Rekap_TagihanKotor_Asing As Decimal
    Dim Rekap_TotalTagihan_Asing As Decimal
    Dim Rekap_BiayaLainnya_Asing As Decimal

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim JenisInvoice_Terseleksi
    Dim JenisProduk_Terseleksi
    Dim AngkaInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim NomorPenjualan_Terseleksi
    Dim NP_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim TanggalPembetulan_Terseleksi
    Dim TanggalLapor_Terseleksi
    Dim JatuhTempo_Terseleksi
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
    Dim NomorFakturPajak_Terseleksi
    Dim JenisPPN_Terseleksi
    Dim PPN_Terseleksi
    Dim PPhDipotong_Terseleksi
    Dim TagihanKotor_Terseleksi
    Dim ReturDPP_Terseleksi
    Dim ReturPPN_Terseleksi
    Dim Retur_Terseleksi
    Dim InvoicePenjualanAsset_Terseleksi
    Dim Catatan_Terseleksi
    Dim NomorJV_Terseleksi


    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Filter :
    Dim Pilih_KodeCustomer
    Dim Pilih_JatuhTempo

    'Jenis Tampilan :
    Public JenisTampilan
    Public JenisTampilan_Semua = "Semua"
    Public JenisTampilan_HasilAkhir = "Hasil Akhir"

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

        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            PenjualanLokal = True
            PenjualanEkspor = False
        Else
            PenjualanLokal = False
            PenjualanEkspor = True
        End If

        LogikaDestinasiPenjualan()

        If JenisProduk_Menu = JenisProduk_Barang Then
            PPh_Dipotong.Visibility = Visibility.Collapsed
        Else
            PPh_Dipotong.Visibility = Visibility.Visible
        End If

        If InvoiceDenganPO = True Then
            If PenjualanLokal Then
                Nomor_SJ_BAST.Visibility = Visibility.Visible
                Tanggal_SJ_BAST.Visibility = Visibility.Visible
            End If
            Nomor_PO.Visibility = Visibility.Visible
            Tanggal_PO.Visibility = Visibility.Visible
        Else
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Nomor_PO.Visibility = Visibility.Collapsed
            Tanggal_PO.Visibility = Visibility.Collapsed
        End If

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_JenisTampilan()
        KontenCombo_Customer()
        EksekusiKode = True
        TampilkanData()
    End Sub


    Sub TampilkanData()

        If EksekusiKode = False Then Return

        KesesuaianJurnal = True

        KetersediaanMenuHalaman(pnl_Halaman, False)

        'Style Tabel :
        datatabelUtama.Rows.Clear()
        If PenjualanLokal Then
            Nomor_SJ_BAST.Visibility = Visibility.Visible
            Tanggal_SJ_BAST.Visibility = Visibility.Visible
        End If
        If MetodePembayaran = MetodePembayaran_Termin Then
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Prosentase_Termin.Visibility = Visibility.Visible
        End If

        'Filter Invoice Dengan PO :
        Dim FilterInvoiceDenganPO = Spasi1
        If MetodePembayaran = MetodePembayaran_Normal Then
            Prosentase_Termin.Visibility = Visibility.Collapsed
            If InvoiceDenganPO = True Then FilterInvoiceDenganPO = " AND Nomor_PO_Produk <> '' "
            If InvoiceDenganPO = False Then FilterInvoiceDenganPO = " AND Nomor_PO_Produk = '' "
        End If

        'Filter Jenis Produk Induk :
        Dim FilterJenisProdukInduk = Spasi1
        If JenisProduk_Menu <> JenisProduk_Semua Then FilterJenisProdukInduk = " AND Jenis_Produk_Induk = '" & JenisProduk_Menu & "' "

        'Filter Customer :
        Dim FilterCustomer = Spasi1
        If cmb_Customer.SelectedValue <> Pilihan_Semua Then FilterCustomer = " AND Kode_Customer = '" & Pilih_KodeCustomer & "' "

        'Filter Metode Pembayaran :
        Dim FilterMetodePembayaran = Spasi1
        If MetodePembayaran <> Pilihan_Semua Then FilterMetodePembayaran = "  AND Metode_Pembayaran = '" & MetodePembayaran & "' "

        'Filter Data :
        Dim FilterData = FilterInvoiceDenganPO & FilterJenisProdukInduk & FilterCustomer & FilterMetodePembayaran

        'Data Tabel :
        Index_BarisTabel = 0
        NomorUrut = 0
        NomorInvoice_Sebelumnya = Kosongan

        'Total :
        Rekap_JumlahHarga = 0
        Rekap_JumlahHarga_Asing = 0
        Rekap_DiskonRp = 0
        Rekap_DiskonAsing = 0
        Rekap_DasarPengenaanPajak = 0
        Rekap_PPN = 0
        Rekap_PPhDipotong = 0
        Rekap_TagihanKotor = 0
        Rekap_TagihanKotor_Asing = 0
        Rekap_TotalTagihan = 0
        Rekap_BiayaLainnya_Asing = 0
        Rekap_TotalTagihan_Asing = 0
        Rekap_Retur = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice <> 'X' " & FilterData &
                              " ORDER BY Tanggal_Invoice ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then
            KetersediaanMenuHalaman(pnl_Halaman, True)
            Return
        End If

        Do While dr.Read
            NomorPO = Kosongan
            TanggalPO = Kosongan
            NomorSJBAST = Kosongan
            NomorSJBAST_Satuan = Kosongan
            NomorSJBAST_Sebelumnya = Kosongan
            TanggalSJBAST = Kosongan
            AngkaInvoice = dr.Item("Angka_Invoice")
            JenisInvoice = dr.Item("Jenis_Invoice")
            JenisProduk = dr.Item("Jenis_Produk_Induk")
            NomorInvoice = dr.Item("Nomor_Invoice")
            NomorPenjualan = dr.Item("Nomor_Penjualan")
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
                NomorInvoice_Pembetulan = GantiTeks(NomorInvoice, NP, NP_Pembetulan)
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                         " WHERE Nomor_Invoice = '" & NomorInvoice_Pembetulan & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            If drTELUSUR.HasRows And JenisTampilan = JenisTampilan_HasilAkhir Then Continue Do
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            TahunInvoice = Right(TanggalInvoice, 4)
            If NP = "N" Then
                TanggalPembetulan = StripKosong
            Else
                TanggalPembetulan = TanggalFormatTampilan(dr.Item("Tanggal_Pembetulan"))
            End If
            Tanggallapor = TanggalFormatTampilan(dr.Item("Tanggal_Lapor"))
            If Tanggallapor = TanggalKosong Then Tanggallapor = StripKosong
            JatuhTempo = dr.Item("Jumlah_Hari_Jatuh_Tempo")
            If JatuhTempo > 0 Then
                JatuhTempo &= " hari"
            Else
                JatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            End If
            If MetodePembayaran = MetodePembayaran_Termin Or CustomerSebagaiPerusahaanLuarNegeri(KodeCustomer) Then
                NomorPO = dr.Item("Nomor_PO_Produk")
                If NomorPO <> Kosongan Then TanggalPO = TanggalFormatTampilan(AmbilValue_TanggalPOBerdasarkanNomorPOPenjualan(NomorPO))
            Else
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice WHERE " &
                                             " Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                Do While drTELUSUR.Read
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
                                NomorSJBAST &= SlashGanda_Pemisah & NomorSJBAST_Satuan
                                TanggalSJBAST &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_SJ"))
                                NomorPO &= SlashGanda_Pemisah & drTELUSUR2.Item("Nomor_PO_Produk")
                                TanggalPO &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                                cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                              " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                                drTELUSUR3_ExecuteReader()
                                drTELUSUR3.Read()
                                KodeProject &= SlashGanda_Pemisah & drTELUSUR3.Item("Kode_Project_Produk")
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
                                NomorSJBAST &= SlashGanda_Pemisah & NomorSJBAST_Satuan
                                TanggalSJBAST &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
                                NomorPO &= SlashGanda_Pemisah & drTELUSUR2.Item("Nomor_PO_Produk")
                                TanggalPO &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                                cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                              " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                                drTELUSUR3_ExecuteReader()
                                drTELUSUR3.Read()
                                KodeProject &= SlashGanda_Pemisah & drTELUSUR3.Item("Kode_Project_Produk")
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
            KodeMataUang = dr.Item("Kode_Mata_Uang")
            Kurs = dr.Item("Kurs")
            JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan")
            JumlahHarga_Asing = dr.Item("Jumlah_Harga_Keseluruhan")
            Dim Termin As Decimal = dr.Item("Termin")
            Dim TerminPersen As Decimal = Termin
            Select Case dr.Item("Basis_Perhitungan_Termin")
                Case BasisPerhitunganTermin_Prosentase
                    TerminPersen = Termin
                Case BasisPerhitunganTermin_Nominal
                    If PenjualanLokal Then
                        TerminPersen = (FormatUlangInt64(Termin) / JumlahHarga) * 100
                    Else
                        TerminPersen = (Termin / JumlahHarga_Asing) * 100
                    End If
            End Select
            DiskonRp = dr.Item("Diskon")
            DiskonAsing = AmbilValue_DiskonAsingBerdasarkanNomorInvoicePenjualan(NomorInvoice)
            ProsentaseTermin_String = PembulatanDesimal2Digit(TerminPersen).ToString & " % "
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            PPN = dr.Item("PPN")
            TagihanKotor = dr.Item("Total_Tagihan_Kotor")
            TagihanKotor_Asing = (JumlahHarga_Asing - DiskonAsing) * (Persen(TerminPersen))
            If PenjualanLokal Then
                PPhDipotong = dr.Item("PPh_Dipotong")
            Else
                PPhDipotong = 0
            End If
            BiayaLainnya = dr.Item("Biaya_Transportasi")
            BiayaLainnya_Asing = dr.Item("Freight") + dr.Item("Insurance")
            TotalTagihan = dr.Item("Total_Tagihan")
            TotalTagihan_Asing = dr.Item("Total_Tagihan")
            ReturDPP = dr.Item("Retur_DPP")
            ReturPPN = dr.Item("Retur_PPN")
            Retur = ReturDPP + ReturPPN
            Catatan = PenghapusEnter(dr.Item("Catatan"))
            NomorJV = dr.Item("Nomor_JV")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then
                If PenjualanLokal And Not MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then TambahBaris()
                If PenjualanEkspor And MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then TambahBaris()
            End If
            NomorInvoice_Sebelumnya = NomorInvoice
        Loop

        AksesDatabase_Transaksi(Tutup)

        If JenisTampilan = JenisTampilan_HasilAkhir Then
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Rekap_JumlahHarga, Rekap_JumlahHarga_Asing, Rekap_DiskonRp, Rekap_DiskonAsing, Kosongan, Rekap_DasarPengenaanPajak, Kosongan, Kosongan, Rekap_PPN,
                                Rekap_TagihanKotor, Rekap_TagihanKotor_Asing, Rekap_PPhDipotong, Rekap_BiayaLainnya, Rekap_BiayaLainnya_Asing,
                                Rekap_TotalTagihan, Rekap_TotalTagihan_Asing, 0, 0, Rekap_Retur, Kosongan)
        End If

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        If TahunInvoice <> TahunBukuAktif Then Return
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, JenisInvoice, JenisProduk, AngkaInvoice, NomorInvoice, NomorPenjualan, NP, TanggalInvoice, TanggalPembetulan, Tanggallapor, JatuhTempo,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject, KodeCustomer, NamaCustomer, KodeMataUang,
                                JumlahHarga, JumlahHarga_Asing, DiskonRp, DiskonAsing, ProsentaseTermin_String, DasarPengenaanPajak, NomorFakturPajak, JenisPPN, PPN,
                                TagihanKotor, TagihanKotor_Asing, PPhDipotong, BiayaLainnya, BiayaLainnya_Asing,
                                TotalTagihan, TotalTagihan_Asing, ReturDPP, ReturPPN, Retur, Catatan, NomorJV)
        Terabas()
        'If NomorJV > 0 Then
        '    If NP = "N" Then datatabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaTegas
        '    If NP <> "N" Then datatabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaBiruSolid
        'Else
        '    datatabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
        'End If
        'Akumulasi/Total :
        Rekap_JumlahHarga += JumlahHarga
        Rekap_JumlahHarga_Asing += JumlahHarga_Asing
        Rekap_DiskonRp += DiskonRp
        Rekap_DiskonAsing += DiskonAsing
        Rekap_DasarPengenaanPajak += DasarPengenaanPajak
        Rekap_PPN += PPN
        Rekap_PPhDipotong += PPhDipotong
        Rekap_TagihanKotor += TagihanKotor
        Rekap_TagihanKotor_Asing += TagihanKotor_Asing
        Rekap_BiayaLainnya += BiayaLainnya
        Rekap_BiayaLainnya_Asing += BiayaLainnya_Asing
        Rekap_TotalTagihan += TotalTagihan
        Rekap_TotalTagihan_Asing += TotalTagihan_Asing
        Rekap_Retur += Retur
        Index_BarisTabel += 1
    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Cetak.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_LihatInvoice.IsEnabled = False
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_Pembetulan.IsEnabled = False
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub


    Sub KontenCombo_JenisTampilan()
        cmb_JenisTampilan.Items.Clear()
        cmb_JenisTampilan.Items.Add(JenisTampilan_Semua)
        cmb_JenisTampilan.Items.Add(JenisTampilan_HasilAkhir)
        cmb_JenisTampilan.SelectedValue = JenisTampilan_Semua
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


    Sub LogikaDestinasiPenjualan()
        'Lokal :
        Kode_Mata_Uang.Visibility = Visibility.Collapsed
        Nomor_Faktur_Pajak.Header = "Nomor Faktur Pajak"
        Jumlah_Harga.Visibility = Visibility.Visible
        Diskon_Rp.Visibility = Visibility.Visible
        Dasar_Pengenaan_Pajak.Visibility = Visibility.Visible
        PPN_.Visibility = Visibility.Visible
        Tagihan_Kotor.Visibility = Visibility.Visible
        PPh_Dipotong.Visibility = Visibility.Visible
        Biaya_Lainnya.Visibility = Visibility.Visible
        Total_Tagihan.Visibility = Visibility.Visible
        Nomor_SJ_BAST.Visibility = Visibility.Visible
        Tanggal_SJ_BAST.Visibility = Visibility.Visible
        Retur_DPP.Visibility = Visibility.Visible
        Retur_PPN.Visibility = Visibility.Visible
        Retur_.Visibility = Visibility.Visible
        'Asing :
        Jumlah_Harga_Asing.Visibility = Visibility.Collapsed
        Diskon_Asing.Visibility = Visibility.Collapsed
        Tagihan_Kotor_Asing.Visibility = Visibility.Collapsed
        Biaya_Lainnya_Asing.Visibility = Visibility.Collapsed
        Total_Tagihan_Asing.Visibility = Visibility.Collapsed
        If PenjualanEkspor Then
            'Lokal :
            Kode_Mata_Uang.Visibility = Visibility.Visible
            Nomor_Faktur_Pajak.Header = "Nomor PEB"
            Jumlah_Harga.Visibility = Visibility.Collapsed
            Diskon_Rp.Visibility = Visibility.Collapsed
            Dasar_Pengenaan_Pajak.Visibility = Visibility.Collapsed
            PPN_.Visibility = Visibility.Collapsed
            Tagihan_Kotor.Visibility = Visibility.Collapsed
            PPh_Dipotong.Visibility = Visibility.Collapsed
            Biaya_Lainnya.Visibility = Visibility.Collapsed
            Total_Tagihan.Visibility = Visibility.Collapsed
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Retur_DPP.Visibility = Visibility.Collapsed
            Retur_PPN.Visibility = Visibility.Collapsed
            Retur_.Visibility = Visibility.Collapsed
            'Asing :
            Jumlah_Harga_Asing.Visibility = Visibility.Visible
            Diskon_Asing.Visibility = Visibility.Visible
            Tagihan_Kotor_Asing.Visibility = Visibility.Visible
            Biaya_Lainnya_Asing.Visibility = Visibility.Visible
            Total_Tagihan_Asing.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            win_InputInvoicePenjualan_Alt = New wpfWin_InputInvoicePenjualan_Alt
            win_InputInvoicePenjualan_Alt.ResetForm()
            win_InputInvoicePenjualan_Alt.FungsiForm = FungsiForm_TAMBAH
            win_InputInvoicePenjualan_Alt.DestinasiPenjualan = DestinasiPenjualan
            win_InputInvoicePenjualan_Alt.JenisProduk_Induk = JenisProduk_Menu
            win_InputInvoicePenjualan_Alt.InvoiceDenganPO = InvoiceDenganPO
            If Not Pilih_KodeCustomer = Pilihan_Semua Then win_InputInvoicePenjualan_Alt.txt_KodeCustomer.Text = Pilih_KodeCustomer
            win_InputInvoicePenjualan_Alt.MetodePembayaran = MetodePembayaran
            win_InputInvoicePenjualan_Alt.ShowDialog()
        Else
            win_InputInvoicePenjualan = New wpfWin_InputInvoicePenjualan
            win_InputInvoicePenjualan.ResetForm()
            win_InputInvoicePenjualan.FungsiForm = FungsiForm_TAMBAH
            win_InputInvoicePenjualan.DestinasiPenjualan = DestinasiPenjualan
            win_InputInvoicePenjualan.JenisProduk_Induk = JenisProduk_Menu
            win_InputInvoicePenjualan.InvoiceDenganPO = InvoiceDenganPO
            If Not Pilih_KodeCustomer = Pilihan_Semua Then win_InputInvoicePenjualan.txt_KodeCustomer.Text = Pilih_KodeCustomer
            win_InputInvoicePenjualan.MetodePembayaran = MetodePembayaran
            win_InputInvoicePenjualan.ShowDialog()
        End If
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        If InvoicePenjualanAsset_Terseleksi = 1 Then
            Pesan_Informasi("Sementara ini, sistem belum menyediakan fitur Edit untuk Invoice Penjualan Asset.")
            Return
        End If

        Dim BisaDiedit As Boolean

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            Pesan_Peringatan("Invoice ini tidak dapat diedit karena sudah ada data pencairan." & Enter2Baris &
                   "Jika ingin mengeditnya, silakan hapus terlebih dahulu data pencairan terkait Invoice ini.")
            BisaDiedit = False
        Else
            BisaDiedit = True
        End If
        AksesDatabase_Transaksi(Tutup)

        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            win_InputInvoicePenjualan_Alt = New wpfWin_InputInvoicePenjualan_Alt
            win_InputInvoicePenjualan_Alt.ResetForm()
            win_InputInvoicePenjualan_Alt.FungsiForm = FungsiForm_EDIT
            If BisaDiedit = False Then win_InputInvoicePenjualan_Alt.FungsiForm = FungsiForm_LIHAT
            win_InputInvoicePenjualan_Alt.DestinasiPenjualan = DestinasiPenjualan
            win_InputInvoicePenjualan_Alt.InvoiceDenganPO = InvoiceDenganPO
            win_InputInvoicePenjualan_Alt.MetodePembayaran = MetodePembayaran
            IsiValueForm_InvoicePenjualan()
            win_InputInvoicePenjualan_Alt.ShowDialog()
        Else
            win_InputInvoicePenjualan = New wpfWin_InputInvoicePenjualan
            win_InputInvoicePenjualan.ResetForm()
            win_InputInvoicePenjualan.FungsiForm = FungsiForm_EDIT
            If BisaDiedit = False Then win_InputInvoicePenjualan.FungsiForm = FungsiForm_LIHAT
            win_InputInvoicePenjualan.DestinasiPenjualan = DestinasiPenjualan
            win_InputInvoicePenjualan.InvoiceDenganPO = InvoiceDenganPO
            win_InputInvoicePenjualan.MetodePembayaran = MetodePembayaran
            IsiValueForm_InvoicePenjualan()
            win_InputInvoicePenjualan.ShowDialog()
        End If

    End Sub
    Sub IsiValueForm_InvoicePenjualan()
        ProsesIsiValueForm = True
        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            win_InputInvoicePenjualan_Alt.AngkaInvoice = AngkaInvoice_Terseleksi
            win_InputInvoicePenjualan_Alt.JenisProduk_Induk = JenisProduk_Terseleksi
            win_InputInvoicePenjualan_Alt.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
            win_InputInvoicePenjualan_Alt.NomorPenjualan = NomorPenjualan_Terseleksi
            If AmbilTeksKanan(JatuhTempo_Terseleksi, 2) = "ri" Then
                win_InputInvoicePenjualan_Alt.txt_JumlahHariJatuhTempo.Text = AmbilAngka(JatuhTempo_Terseleksi)
                win_InputInvoicePenjualan_Alt.dtp_TanggalJatuhTempo.Text = Kosongan
                win_InputInvoicePenjualan_Alt.rdb_JumlahHariJatuhTempo.IsChecked = True
            Else
                win_InputInvoicePenjualan_Alt.txt_JumlahHariJatuhTempo.Text = Kosongan
                win_InputInvoicePenjualan_Alt.dtp_TanggalJatuhTempo.SelectedDate = TanggalFormatWPF(JatuhTempo_Terseleksi)
                win_InputInvoicePenjualan_Alt.rdb_TanggalJatuhTempo.IsChecked = True
            End If
            win_InputInvoicePenjualan_Alt.cmb_JenisInvoice.SelectedValue = JenisInvoice_Terseleksi
            win_InputInvoicePenjualan_Alt.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
            win_InputInvoicePenjualan_Alt.txt_NamaCustomer.Text = NamaCustomer_Terseleksi
            win_InputInvoicePenjualan_Alt.txt_JumlahNota.Text = JumlahHarga_Terseleksi
            win_InputInvoicePenjualan_Alt.ReturDPP = ReturDPP_Terseleksi
            win_InputInvoicePenjualan_Alt.ReturPPN = ReturPPN_Terseleksi
            IsiValueElemenRichTextBox(win_InputInvoicePenjualan_Alt.txt_Catatan, Catatan_Terseleksi)
            win_InputInvoicePenjualan_Alt.NomorJV = NomorJV_Terseleksi
            win_InputInvoicePenjualan_Alt.NomorFakturPajak = NomorFakturPajak_Terseleksi
            win_InputInvoicePenjualan_Alt.NP = NP_Terseleksi
            If NP_Terseleksi = "N" Then
                win_InputInvoicePenjualan_Alt.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice_Terseleksi)
            Else
                win_InputInvoicePenjualan_Alt.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalPembetulan_Terseleksi)
                win_InputInvoicePenjualan_Alt.TanggalInvoice = TanggalInvoice_Terseleksi
            End If
            win_InputInvoicePenjualan_Alt.IsiTabelProduk()
            win_InputInvoicePenjualan_Alt.IsiTabelSJBAST()
        Else
            win_InputInvoicePenjualan.AngkaInvoice = AngkaInvoice_Terseleksi
            win_InputInvoicePenjualan.JenisProduk_Induk = JenisProduk_Terseleksi
            win_InputInvoicePenjualan.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
            win_InputInvoicePenjualan.NomorPenjualan = NomorPenjualan_Terseleksi
            If AmbilTeksKanan(JatuhTempo_Terseleksi, 2) = "ri" Then
                win_InputInvoicePenjualan.txt_JumlahHariJatuhTempo.Text = AmbilAngka(JatuhTempo_Terseleksi)
                win_InputInvoicePenjualan.dtp_TanggalJatuhTempo.Text = Kosongan
                win_InputInvoicePenjualan.rdb_JumlahHariJatuhTempo.IsChecked = True
            Else
                win_InputInvoicePenjualan.txt_JumlahHariJatuhTempo.Text = Kosongan
                win_InputInvoicePenjualan.dtp_TanggalJatuhTempo.SelectedDate = TanggalFormatWPF(JatuhTempo_Terseleksi)
                win_InputInvoicePenjualan.rdb_TanggalJatuhTempo.IsChecked = True
            End If
            win_InputInvoicePenjualan.cmb_JenisInvoice.SelectedValue = JenisInvoice_Terseleksi
            win_InputInvoicePenjualan.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
            win_InputInvoicePenjualan.txt_NamaCustomer.Text = NamaCustomer_Terseleksi
            win_InputInvoicePenjualan.txt_JumlahNota.Text = JumlahHarga_Terseleksi
            win_InputInvoicePenjualan.ReturDPP = ReturDPP_Terseleksi
            win_InputInvoicePenjualan.ReturPPN = ReturPPN_Terseleksi
            IsiValueElemenRichTextBox(win_InputInvoicePenjualan.txt_Catatan, Catatan_Terseleksi)
            win_InputInvoicePenjualan.NomorJV = NomorJV_Terseleksi
            win_InputInvoicePenjualan.NomorFakturPajak = NomorFakturPajak_Terseleksi
            win_InputInvoicePenjualan.NP = NP_Terseleksi
            If NP_Terseleksi = "N" Then
                win_InputInvoicePenjualan.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice_Terseleksi)
            Else
                win_InputInvoicePenjualan.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalPembetulan_Terseleksi)
                win_InputInvoicePenjualan.TanggalInvoice = TanggalInvoice_Terseleksi
            End If
            win_InputInvoicePenjualan.IsiTabelProduk()
            win_InputInvoicePenjualan.IsiTabelSJBAST()
        End If
        ProsesIsiValueForm = False
    End Sub



    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            Pesan_Peringatan("Invoice ini tidak dapat dihapus karena sudah ada data pencairan." & Enter2Baris &
                   "Jika ingin menghapusnya, silakan hapus terlebih dahulu data pencairan terkait Invoice ini.")
            Return
        End If
        AksesDatabase_Transaksi(Tutup)

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)

        'Hapus Data Terpilih Pada Tabel Invoice Penjualan (tbl_Penjualan_Invoice) :
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Penjualan_Invoice " &
                                   " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()

        'Hapus Data Terpilih Pada Tabel Jurnal (tbl_Transaksi) :
        cmd = New OdbcCommand("DELETE FROM tbl_Transaksi " &
                              " WHERE Nomor_JV = '" & NomorJV_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
            'frm_BukuPenjualan.TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub


    Private Sub btn_LihatInvoice_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatInvoice.Click
        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            win_InputInvoicePenjualan_Alt = New wpfWin_InputInvoicePenjualan_Alt
            win_InputInvoicePenjualan_Alt.ResetForm()
            win_InputInvoicePenjualan_Alt.FungsiForm = FungsiForm_LIHAT
            win_InputInvoicePenjualan_Alt.DestinasiPenjualan = DestinasiPenjualan
            win_InputInvoicePenjualan_Alt.InvoiceDenganPO = InvoiceDenganPO
            win_InputInvoicePenjualan_Alt.MetodePembayaran = MetodePembayaran
            IsiValueForm_InvoicePenjualan()
            win_InputInvoicePenjualan_Alt.ShowDialog()
        Else
            win_InputInvoicePenjualan = New wpfWin_InputInvoicePenjualan
            win_InputInvoicePenjualan.ResetForm()
            win_InputInvoicePenjualan.FungsiForm = FungsiForm_LIHAT
            win_InputInvoicePenjualan.DestinasiPenjualan = DestinasiPenjualan
            win_InputInvoicePenjualan.InvoiceDenganPO = InvoiceDenganPO
            win_InputInvoicePenjualan.MetodePembayaran = MetodePembayaran
            IsiValueForm_InvoicePenjualan()
            win_InputInvoicePenjualan.ShowDialog()
        End If
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi = 0 Then
            Pesan_Informasi("Invoice ini belum didorong ke Jurnal.")
            Return
        End If
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Pembetulan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Pembetulan.Click

        'Validasi :
        Dim Invoice_SudahDibetulkan As Boolean = False
        Dim NomorInvoice_Pembetulan = Kosongan
        Dim NP_Pembetulan = Kosongan
        If NP_Terseleksi = "N" Then
            NomorInvoice_Pembetulan = NomorInvoice_Terseleksi & "-P1"
        Else
            Dim PembetulanKe = AmbilAngka(NP_Terseleksi)
            NP_Pembetulan = "P" & (PembetulanKe + 1)
            NomorInvoice_Pembetulan = NomorInvoice_Terseleksi.Replace(NP_Terseleksi, NP_Pembetulan)
        End If
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Pembetulan & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            Invoice_SudahDibetulkan = True
        Else
            Invoice_SudahDibetulkan = False
        End If
        AksesDatabase_Transaksi(Tutup)
        If Invoice_SudahDibetulkan = True Then
            Pesan_Peringatan("Invoice ini sudah pernah dibetulkan." & Enter2Baris & "Silakan pilih pembetulan yang terakhir terkait invoice ini.")
            Return
        End If

        ''Isi Variabel :
        'frm_Input_InvoicePenjualan.ResetForm()
        'frm_Input_InvoicePenjualan.FungsiForm = FungsiForm_PEMBETULAN
        'IsiValueForm_InvoicePenjualan()

        ''Reset Variabel-bariabel Tertentu :
        'frm_Input_InvoicePenjualan.NomorJV = 0
        'frm_Input_InvoicePenjualan.ReturDPP = 0
        'frm_Input_InvoicePenjualan.ReturPPN = 0
        'EksekusiKode = False
        'frm_Input_InvoicePenjualan.dtp_TanggalInvoice.Value = Today
        'frm_Input_InvoicePenjualan.TanggalInvoice = TanggalInvoice_Terseleksi
        'EksekusiKode = True

        'frm_Input_InvoicePenjualan.ShowDialog()

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
        JenisInvoice_Terseleksi = rowviewUtama("Jenis_Invoice")
        JenisProduk_Terseleksi = rowviewUtama("Jenis_Produk")
        AngkaInvoice_Terseleksi = AmbilAngka(rowviewUtama("Angka_Invoice"))
        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice")
        NomorPenjualan_Terseleksi = rowviewUtama("Nomor_Penjualan")
        NP_Terseleksi = rowviewUtama("N_P")
        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice")
        TanggalPembetulan_Terseleksi = rowviewUtama("Tanggal_Pembetulan")
        TanggalLapor_Terseleksi = rowviewUtama("Tanggal_Lapor")
        JatuhTempo_Terseleksi = rowviewUtama("Jatuh_Tempo")
        NomorSJBAST_Terseleksi = rowviewUtama("Nomor_SJ_BAST")
        TanggalSJBAST_Terseleksi = rowviewUtama("Tanggal_SJ_BAST")
        NomorPO_Terseleksi = rowviewUtama("Nomor_PO")
        TanggalPO_Terseleksi = rowviewUtama("Tanggal_PO")
        KodeProject_Terseleksi = rowviewUtama("Kode_Project")
        KodeCustomer_Terseleksi = rowviewUtama("Kode_Customer")
        NamaCustomer_Terseleksi = rowviewUtama("Nama_Customer")
        JumlahHarga_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Harga"))
        DiskonRp_Terseleksi = AmbilAngka(rowviewUtama("Diskon_Rp"))
        DasarPengenaanPajak_Terseleksi = AmbilAngka(rowviewUtama("Dasar_Pengenaan_Pajak"))
        NomorFakturPajak_Terseleksi = rowviewUtama("Nomor_Faktur_Pajak")
        JenisPPN_Terseleksi = rowviewUtama("Jenis_PPN")
        PPN_Terseleksi = AmbilAngka(rowviewUtama("PPN_"))
        PPhDipotong_Terseleksi = AmbilAngka(rowviewUtama("PPh_Dipotong"))
        TagihanKotor_Terseleksi = AmbilAngka(rowviewUtama("Tagihan_Kotor"))
        ReturDPP_Terseleksi = AmbilAngka(rowviewUtama("Retur_DPP"))
        ReturPPN_Terseleksi = AmbilAngka(rowviewUtama("Retur_PPN"))
        Retur_Terseleksi = AmbilAngka(rowviewUtama("Retur_"))
        Catatan_Terseleksi = rowviewUtama("Catatan_")
        NomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))

        If NomorInvoice_Terseleksi = Kosongan Then
            BersihkanSeleksi()
        Else
            btn_Cetak.IsEnabled = True
            btn_LihatJurnal.IsEnabled = True
            btn_LihatInvoice.IsEnabled = True
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
            If NomorJV_Terseleksi > 0 Then
                btn_Pembetulan.IsEnabled = True
                'btn_Edit.isenabled = False
                'btn_Hapus.isenabled = False
            Else
                btn_Pembetulan.IsEnabled = False
            End If
        End If
        If NomorJV_Terseleksi = 0 Then btn_LihatJurnal.IsEnabled = False

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        'If AmbilAngka(e.Row.Item("Nomor_JV_Penjualan")) = 0 Then
        '    If JenisTahunBuku = JenisTahunBuku_NORMAL Then e.Row.Foreground = clrDataTahunLalu
        'Else
        '    e.Row.Foreground = clrBlack
        'End If
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
    Dim Jenis_Invoice As New DataGridTextColumn
    Dim Jenis_Produk As New DataGridTextColumn
    Dim Angka_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Nomor_Penjualan As New DataGridTextColumn
    Dim N_P As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Tanggal_Pembetulan As New DataGridTextColumn
    Dim Tanggal_Lapor As New DataGridTextColumn
    Dim Jatuh_Tempo As New DataGridTextColumn
    Dim Nomor_SJ_BAST As New DataGridTextColumn
    Dim Tanggal_SJ_BAST As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Tanggal_PO As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Kode_Customer As New DataGridTextColumn
    Dim Nama_Customer As New DataGridTextColumn
    Dim Kode_Mata_Uang As New DataGridTextColumn
    Dim Jumlah_Harga As New DataGridTextColumn
    Dim Jumlah_Harga_Asing As New DataGridTextColumn
    Dim Diskon_Rp As New DataGridTextColumn
    Dim Diskon_Asing As New DataGridTextColumn
    Dim Prosentase_Termin As New DataGridTextColumn
    Dim Dasar_Pengenaan_Pajak As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Jenis_PPN As New DataGridTextColumn
    Dim PPN_ As New DataGridTextColumn
    Dim Tagihan_Kotor As New DataGridTextColumn
    Dim Tagihan_Kotor_Asing As New DataGridTextColumn
    Dim PPh_Dipotong As New DataGridTextColumn
    Dim Biaya_Lainnya As New DataGridTextColumn
    Dim Biaya_Lainnya_Asing As New DataGridTextColumn
    Dim Total_Tagihan As New DataGridTextColumn
    Dim Total_Tagihan_Asing As New DataGridTextColumn
    Dim Retur_DPP As New DataGridTextColumn
    Dim Retur_PPN As New DataGridTextColumn
    Dim Retur_ As New DataGridTextColumn
    Dim Catatan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Jenis_Invoice")
        datatabelUtama.Columns.Add("Jenis_Produk")
        datatabelUtama.Columns.Add("Angka_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Nomor_Penjualan")
        datatabelUtama.Columns.Add("N_P")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Pembetulan")
        datatabelUtama.Columns.Add("Tanggal_Lapor")
        datatabelUtama.Columns.Add("Jatuh_Tempo")
        datatabelUtama.Columns.Add("Nomor_SJ_BAST")
        datatabelUtama.Columns.Add("Tanggal_SJ_BAST")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Tanggal_PO")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Kode_Customer")
        datatabelUtama.Columns.Add("Nama_Customer")
        datatabelUtama.Columns.Add("Kode_Mata_Uang")
        datatabelUtama.Columns.Add("Jumlah_Harga", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Harga_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Diskon_Rp", GetType(Int64))
        datatabelUtama.Columns.Add("Diskon_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Prosentase_Termin")
        datatabelUtama.Columns.Add("Dasar_Pengenaan_Pajak", GetType(Int64))
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Jenis_PPN")
        datatabelUtama.Columns.Add("PPN_", GetType(Int64))
        datatabelUtama.Columns.Add("Tagihan_Kotor", GetType(Int64))
        datatabelUtama.Columns.Add("Tagihan_Kotor_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("PPh_Dipotong", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Lainnya", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Lainnya_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Total_Tagihan", GetType(Int64))
        datatabelUtama.Columns.Add("Total_Tagihan_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Retur_DPP", GetType(Int64))
        datatabelUtama.Columns.Add("Retur_PPN", GetType(Int64))
        datatabelUtama.Columns.Add("Retur_", GetType(Int64))
        datatabelUtama.Columns.Add("Catatan_")
        datatabelUtama.Columns.Add("Nomor_JV")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Invoice, "Jenis_Invoice", "Jenis Invoice", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Produk, "Jenis_Produk", "Jenis Produk", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angka_Invoice, "Angka_Invoice", "Angka Invoice", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Penjualan, "Nomor_Penjualan", "Nomor Penjualan", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, N_P, "N_P", "N/P", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pembetulan, "Tanggal_Pembetulan", "Tanggal Pembetulan", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Lapor, "Tanggal_Lapor", "Tanggal Lapor", 75, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jatuh_Tempo, "Jatuh_Tempo", "Jatuh Tempo", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ_BAST, "Nomor_SJ_BAST", "Nomor SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ_BAST, "Tanggal_SJ_BAST", "Tanggal SJ/BAST", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PO, "Tanggal_PO", "Tanggal PO", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Customer, "Kode_Customer", "Kode Customer", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Customer, "Nama_Customer", "Nama Customer", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mata_Uang, "Kode_Mata_Uang", "Mata Uang", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga, "Jumlah_Harga", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Asing, "Jumlah_Harga_Asing", "Jumlah Harga", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Rp, "Diskon_Rp", "Diskon", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Asing, "Diskon_Asing", "Diskon", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Prosentase_Termin, "Prosentase_Termin", "Termin (%)", 57, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Dasar_Pengenaan_Pajak, "Dasar_Pengenaan_Pajak", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PPN, "Jenis_PPN", "Jenis PPN", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_, "PPN_", "PPN", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Kotor, "Tagihan_Kotor", "Tagihan Kotor", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Kotor_Asing, "Tagihan_Kotor_Asing", "Tagihan Kotor", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong, "PPh_Dipotong", "PPh Dipotong", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Lainnya, "Biaya_Lainnya", "Biaya Lainnya", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Lainnya_Asing, "Biaya_Lainnya_Asing", "Biaya Lainnya", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Tagihan, "Total_Tagihan", "Total Tagihan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Tagihan_Asing, "Total_Tagihan_Asing", "Total Tagihan", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Retur_DPP, "Retur_DPP", "Retur DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Retur_PPN, "Retur_PPN", "Retur PPN", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Retur_, "Retur_", "Retur", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
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
                Dim TagihanKotor_Lama As Int64 = AmbilAngka(row("Tagihan_Kotor"))
                Dim TagihanKotor_Asing_Lama As Decimal = AmbilAngka_Asing(row("Tagihan_Kotor_Asing"))
                Dim BiayaLainnya_Lama As Int64 = AmbilAngka(row("Biaya_Lainnya"))
                Dim BiayaLainnya_Asing_Lama As Decimal = AmbilAngka_Asing(row("Biaya_Lainnya_Asing"))
                Dim TotalTagihan_Lama As Int64 = AmbilAngka(row("Total_Tagihan"))
                Dim TotalTagihan_Asing_Lama As Decimal = AmbilAngka_Asing(row("Total_Tagihan_Asing"))
                Dim Retur_Lama As Int64 = AmbilAngka(row("Retur_"))

                ' Update kolom-kolom
                row("Jenis_Invoice") = JenisInvoice
                row("Jenis_Produk") = JenisProduk
                row("Angka_Invoice") = AngkaInvoice
                row("Nomor_Invoice") = NomorInvoice
                row("Nomor_Penjualan") = NomorPenjualan
                row("N_P") = NP
                row("Tanggal_Invoice") = TanggalInvoice
                row("Tanggal_Pembetulan") = TanggalPembetulan
                row("Tanggal_Lapor") = Tanggallapor
                row("Jatuh_Tempo") = JatuhTempo
                row("Nomor_SJ_BAST") = NomorSJBAST
                row("Tanggal_SJ_BAST") = TanggalSJBAST
                row("Nomor_PO") = NomorPO
                row("Tanggal_PO") = TanggalPO
                row("Kode_Project") = KodeProject
                row("Kode_Customer") = KodeCustomer
                row("Nama_Customer") = NamaCustomer
                row("Kode_Mata_Uang") = KodeMataUang
                row("Jumlah_Harga") = JumlahHarga
                row("Jumlah_Harga_Asing") = JumlahHarga_Asing
                row("Diskon_Rp") = DiskonRp
                row("Diskon_Asing") = DiskonAsing
                row("Prosentase_Termin") = ProsentaseTermin_String
                row("Dasar_Pengenaan_Pajak") = DasarPengenaanPajak
                row("Nomor_Faktur_Pajak") = NomorFakturPajak
                row("Jenis_PPN") = JenisPPN
                row("PPN_") = PPN
                row("Tagihan_Kotor") = TagihanKotor
                row("Tagihan_Kotor_Asing") = TagihanKotor_Asing
                row("PPh_Dipotong") = PPhDipotong
                row("Biaya_Lainnya") = BiayaLainnya
                row("Biaya_Lainnya_Asing") = BiayaLainnya_Asing
                row("Total_Tagihan") = TotalTagihan
                row("Total_Tagihan_Asing") = TotalTagihan_Asing
                row("Retur_DPP") = ReturDPP
                row("Retur_PPN") = ReturPPN
                row("Retur_") = Retur
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
                Rekap_TagihanKotor += (TagihanKotor - TagihanKotor_Lama)
                Rekap_TagihanKotor_Asing += (TagihanKotor_Asing - TagihanKotor_Asing_Lama)
                Rekap_BiayaLainnya += (BiayaLainnya - BiayaLainnya_Lama)
                Rekap_BiayaLainnya_Asing += (BiayaLainnya_Asing - BiayaLainnya_Asing_Lama)
                Rekap_TotalTagihan += (TotalTagihan - TotalTagihan_Lama)
                Rekap_TotalTagihan_Asing += (TotalTagihan_Asing - TotalTagihan_Asing_Lama)
                Rekap_Retur += (Retur - Retur_Lama)

                Exit For
            End If
        Next
    End Sub

End Class
