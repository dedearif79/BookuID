Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfUsc_InvoicePembelian

    Public StatusAktif As Boolean
    Public JudulForm As String

    Public KesesuaianJurnal As Boolean
    Public JenisProduk_Menu
    Public InvoiceDenganPO As Boolean

    'Variabel Tabel :
    Dim Index_BarisTabel
    Dim NomorUrut
    Dim JenisInvoice
    Dim JenisProduk
    Dim AngkaInvoice
    Dim NomorInvoice_Sebelumnya
    Dim NomorInvoice
    Dim NomorPembelian
    Dim NP
    Dim TanggalInvoice
    Dim TahunInvoice As Integer
    Dim TanggalPembetulan
    Dim Tanggallapor
    Dim JatuhTempo
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim TanggalDiterima
    Dim NomorPO
    Dim TanggalPO
    Dim NamaProduk
    Dim KodeProject
    Dim KodeSupplier
    Dim NamaSupplier
    Dim KodeMataUang
    Dim Kurs As Decimal
    Dim JumlahHarga
    Dim DiskonRp
    Dim BasisPerhitunganTermin As String
    Dim ProsentaseTermin_String
    Dim DasarPengenaanPajak
    Dim NomorFakturPajak
    Dim JenisPPN
    Dim PPN
    Dim PPhDipotong
    Dim TagihanKotor
    Dim TotalTagihan
    Dim ReturDPP
    Dim ReturPPN
    Dim Retur
    Dim Catatan
    Dim NomorJV

    'Asing
    Dim JumlahHarga_Asing As Decimal
    Dim DiskonAsing As Decimal
    Dim TotalTagihan_Asing As Decimal

    'Variabel Rekap :
    Dim Rekap_JumlahHarga
    Dim Rekap_DiskonRp
    Dim Rekap_DasarPengenaanPajak
    Dim Rekap_PPN
    Dim Rekap_PPhDipotong
    Dim Rekap_Retur
    Dim Rekap_TagihanKotor
    Dim Rekap_TotalTagihan
    'Asing :
    Dim Rekap_JumlahHarga_Asing As Decimal
    Dim Rekap_Diskon_Asing As Decimal
    Dim Rekap_TotalTagihan_Asing As Decimal

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim JenisInvoice_Terseleksi
    Dim JenisProduk_Terseleksi
    Dim AngkaInvoice_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim NomorPembelian_Terseleksi
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
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
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
    Dim Catatan_Terseleksi
    Dim NomorJV_Terseleksi


    Dim NomorSJBAST_Satuan
    Dim NomorSJBAST_Sebelumnya

    'Variabel Filter :
    Dim Pilih_KodeSupplier
    Dim Pilih_JatuhTempo

    'Jenis Tampilan :
    Public JenisTampilan
    Public JenisTampilan_Semua = "Semua"
    Public JenisTampilan_HasilAkhir = "Hasil Akhir"

    Public MetodePembayaran

    Public AsalPembelian
    Dim PembelianLokal As Boolean
    Dim PembelianImpor As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StatusAktif = True

        lbl_JudulForm.Text = JudulForm
        Terabas()

        lbl_TotalTabel.Visibility = Visibility.Collapsed
        txt_TotalTabel.Visibility = Visibility.Collapsed

        ProsesLoadingForm = True

        If AsalPembelian = AsalPembelian_Lokal Then
            PembelianLokal = True
            PembelianImpor = False
        Else
            PembelianLokal = False
            PembelianImpor = True
        End If

        LogikaAsalPembelian()

        If JenisProduk_Menu = JenisProduk_Barang Then
            PPh_Dipotong.Visibility = Visibility.Collapsed
        Else
            If AsalPembelian = AsalPembelian_Lokal Then PPh_Dipotong.Visibility = Visibility.Visible
        End If

        If InvoiceDenganPO = True Then
            Nomor_SJ_BAST.Visibility = Visibility.Visible
            Tanggal_SJ_BAST.Visibility = Visibility.Visible
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

    End Sub


    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_JenisTampilan()
        KontenCombo_Supplier()
        EksekusiKode = True
        TampilkanData()
    End Sub


    Sub TampilkanData()

        If EksekusiKode = False Then Return

        KesesuaianJurnal = True

        KetersediaanMenuHalaman(pnl_Halaman, False)

        'Style Tabel :
        datatabelUtama.Rows.Clear()
        If PembelianLokal Then
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

        'Filter Supplier :
        Dim FilterSupplier = Spasi1
        If cmb_Supplier.SelectedValue <> Pilihan_Semua Then FilterSupplier = " AND Kode_Supplier = '" & Pilih_KodeSupplier & "' "

        'Filter Metode Pembayaran :
        Dim FilterMetodePembayaran = Spasi1
        If MetodePembayaran <> Pilihan_Semua Then FilterMetodePembayaran = "  AND Metode_Pembayaran = '" & MetodePembayaran & "' "

        'Filter Data :
        Dim FilterData = FilterInvoiceDenganPO & FilterJenisProdukInduk & FilterSupplier & FilterMetodePembayaran

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
        Rekap_TagihanKotor = 0
        Rekap_TotalTagihan = 0
        Rekap_Retur = 0
        'Asing :
        Rekap_JumlahHarga_Asing = 0
        Rekap_Diskon_Asing = 0
        Rekap_TotalTagihan_Asing = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
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
            NamaProduk = Kosongan
            NomorSJBAST = Kosongan
            NomorSJBAST_Satuan = Kosongan
            NomorSJBAST_Sebelumnya = Kosongan
            TanggalSJBAST = Kosongan
            AngkaInvoice = dr.Item("Angka_Invoice")
            JenisInvoice = dr.Item("Jenis_Invoice")
            JenisProduk = dr.Item("Jenis_Produk_Induk")
            NomorInvoice = dr.Item("Nomor_Invoice")
            NomorPembelian = dr.Item("Nomor_Pembelian")
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            NP = dr.Item("N_P")
            NamaProduk = dr.Item("Nama_Produk")
            Dim NomorInvoice_Pembetulan = Kosongan
            Dim NP_Pembetulan = Kosongan
            If NP = "N" Then
                NomorInvoice_Pembetulan = NomorInvoice & "-P1"
            Else
                Dim PembetulanKe = AmbilAngka(NP)
                NP_Pembetulan = "P" & (PembetulanKe + 1)
                NomorInvoice_Pembetulan = GantiTeks(NomorInvoice, NP, NP_Pembetulan)
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
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
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice WHERE " &
                                         " Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
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
                            NomorSJBAST &= SlashGanda_Pemisah & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_SJ"))
                            NomorPO &= SlashGanda_Pemisah & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                          " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject &= SlashGanda_Pemisah & drTELUSUR3.Item("Kode_Project_Produk")
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
                            KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
                            NomorPO &= SlashGanda_Pemisah & drTELUSUR2.Item("Nomor_PO_Produk")
                            TanggalPO &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO_Produk"))
                            cmdTELUSUR3 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                                          " WHERE Nomor_PO = '" & drTELUSUR2.Item("Nomor_PO_Produk") & "' ", KoneksiDatabaseTransaksi)
                            drTELUSUR3_ExecuteReader()
                            drTELUSUR3.Read()
                            KodeProject &= SlashGanda_Pemisah & drTELUSUR3.Item("Kode_Project_Produk")
                        End If
                    End If
                End If
                NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
            Loop
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
                    If PembelianLokal Then
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
            PPhDipotong = dr.Item("PPh_Dipotong")
            TotalTagihan = dr.Item("Total_Tagihan")
            TotalTagihan_Asing = dr.Item("Total_Tagihan")
            ReturDPP = dr.Item("Retur_DPP")
            ReturPPN = dr.Item("Retur_PPN")
            Retur = ReturDPP + ReturPPN
            Catatan = PenghapusEnter(dr.Item("Catatan"))
            NomorJV = dr.Item("Nomor_JV")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then
                If PembelianLokal And Not MitraSebagaiPerusahaanLuarNegeri(KodeSupplier) Then TambahBaris()
                If PembelianImpor And MitraSebagaiPerusahaanLuarNegeri(KodeSupplier) Then TambahBaris()
            End If
            NomorInvoice_Sebelumnya = NomorInvoice
        Loop

        AksesDatabase_Transaksi(Tutup)

        If JenisTampilan = JenisTampilan_HasilAkhir Then
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Rekap_JumlahHarga, Rekap_JumlahHarga_Asing, Rekap_DiskonRp, Rekap_Diskon_Asing, Kosongan, Rekap_DasarPengenaanPajak, Kosongan, Kosongan, Rekap_PPN,
                                Rekap_TagihanKotor, Rekap_PPhDipotong, Rekap_TotalTagihan, Rekap_TotalTagihan_Asing, 0, 0, Rekap_Retur, Kosongan)
        End If

        BersihkanSeleksi()

    End Sub


    Sub TambahBaris()
        If TahunInvoice <> TahunBukuAktif Then Return
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, JenisInvoice, JenisProduk, AngkaInvoice, NomorInvoice, NomorPembelian, NP, TanggalInvoice, TanggalPembetulan, Tanggallapor, JatuhTempo,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, NamaProduk, KodeProject, KodeSupplier, NamaSupplier, KodeMataUang,
                                JumlahHarga, JumlahHarga_Asing, DiskonRp, DiskonAsing, ProsentaseTermin_String, DasarPengenaanPajak, NomorFakturPajak, JenisPPN, PPN,
                                TagihanKotor, PPhDipotong, TotalTagihan, TotalTagihan_Asing, ReturDPP, ReturPPN, Retur, Catatan, NomorJV)
        Terabas()
        'Akumulasi/Total :
        Rekap_JumlahHarga += JumlahHarga
        Rekap_DiskonRp += DiskonRp
        Rekap_DasarPengenaanPajak += DasarPengenaanPajak
        Rekap_PPN += PPN
        Rekap_PPhDipotong += PPhDipotong
        Rekap_TagihanKotor += TagihanKotor
        Rekap_TotalTagihan += TotalTagihan
        Rekap_Retur += Retur
        'Asing :
        Rekap_JumlahHarga_Asing += JumlahHarga_Asing
        Rekap_Diskon_Asing += DiskonAsing
        Rekap_TotalTagihan_Asing += TotalTagihan_Asing
        Index_BarisTabel += 1
    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
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

    Sub KontenCombo_Supplier()
        cmb_Supplier.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        Dim FilterLokasiWP As String = Kosongan
        If PembelianLokal Then FilterLokasiWP = " AND Lokasi_WP = '" & LokasiPS_DalamNegeri & "' "
        If PembelianImpor Then FilterLokasiWP = " AND Lokasi_WP = '" & LokasiPS_LuarNegeri & "' "
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Supplier = 1 " & FilterLokasiWP, KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_Supplier.Items.Add(Pilihan_Semua)
        Do While dr.Read
            cmb_Supplier.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        cmb_Supplier.SelectedValue = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub


    Sub LogikaAsalPembelian()
        'Lokal :
        Kode_Mata_Uang.Visibility = Visibility.Collapsed
        Nomor_Faktur_Pajak.Header = "Nomor Faktur Pajak"
        Jumlah_Harga.Visibility = Visibility.Visible
        Diskon_Rp.Visibility = Visibility.Visible
        Dasar_Pengenaan_Pajak.Visibility = Visibility.Visible
        PPN_.Visibility = Visibility.Visible
        Tagihan_Kotor.Visibility = Visibility.Visible
        PPh_Dipotong.Visibility = Visibility.Visible
        Total_Tagihan.Visibility = Visibility.Visible
        Nomor_SJ_BAST.Visibility = Visibility.Visible
        Tanggal_SJ_BAST.Visibility = Visibility.Visible
        Retur_DPP.Visibility = Visibility.Visible
        Retur_PPN.Visibility = Visibility.Visible
        Retur_.Visibility = Visibility.Visible
        'Asing :
        Jumlah_Harga_Asing.Visibility = Visibility.Collapsed
        Diskon_Asing.Visibility = Visibility.Collapsed
        Total_Tagihan_Asing.Visibility = Visibility.Collapsed
        If PembelianImpor Then
            'Lokal :
            Kode_Mata_Uang.Visibility = Visibility.Visible
            Nomor_Faktur_Pajak.Header = "Nomor PIB"
            Jumlah_Harga.Visibility = Visibility.Collapsed
            Diskon_Rp.Visibility = Visibility.Collapsed
            Dasar_Pengenaan_Pajak.Visibility = Visibility.Collapsed
            PPN_.Visibility = Visibility.Collapsed
            Tagihan_Kotor.Visibility = Visibility.Collapsed
            PPh_Dipotong.Visibility = Visibility.Collapsed
            Total_Tagihan.Visibility = Visibility.Collapsed
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
            Retur_DPP.Visibility = Visibility.Collapsed
            Retur_PPN.Visibility = Visibility.Collapsed
            Retur_.Visibility = Visibility.Collapsed
            'Asing :
            Jumlah_Harga_Asing.Visibility = Visibility.Visible
            Diskon_Asing.Visibility = Visibility.Visible
            Total_Tagihan_Asing.Visibility = Visibility.Visible
        End If
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputInvoicePembelian = New wpfWin_InputInvoicePembelian
        win_InputInvoicePembelian.ResetForm()
        win_InputInvoicePembelian.FungsiForm = FungsiForm_TAMBAH
        win_InputInvoicePembelian.AsalPembelian = AsalPembelian
        win_InputInvoicePembelian.JenisProduk_Induk = JenisProduk_Menu
        win_InputInvoicePembelian.InvoiceDenganPO = InvoiceDenganPO
        If Not Pilih_KodeSupplier = Pilihan_Semua Then win_InputInvoicePembelian.txt_KodeSupplier.Text = Pilih_KodeSupplier
        win_InputInvoicePembelian.MetodePembayaran = MetodePembayaran
        win_InputInvoicePembelian.ShowDialog()
        If win_InputInvoicePembelian.BukaFormPengajuanPengeluaranBankCash Then BukaFormPengajuanPengeluaranBankCash()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        Dim BisaDiedit As Boolean

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' " &
                              " AND Nomor_BP = '" & KonversiNomorPembelianKeNomorBPHU(NomorPembelian_Terseleksi) & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            MsgBox("Invoice ini tidak dapat diedit karena sudah ada data pembayaran..!" & Enter2Baris &
                   "Jika ingin mengeditnya, silakan hapus terlebih dahulu data pembayaran terkait Invoice ini.")
            BisaDiedit = False
        Else
            BisaDiedit = True
        End If
        AksesDatabase_Transaksi(Tutup)

        win_InputInvoicePembelian = New wpfWin_InputInvoicePembelian
        win_InputInvoicePembelian.ResetForm()
        win_InputInvoicePembelian.FungsiForm = FungsiForm_EDIT
        If BisaDiedit = False Then win_InputInvoicePembelian.FungsiForm = FungsiForm_LIHAT
        win_InputInvoicePembelian.AsalPembelian = AsalPembelian
        win_InputInvoicePembelian.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePembelian.MetodePembayaran = MetodePembayaran
        IsiValueForm_InvoicePembelian()
        win_InputInvoicePembelian.ShowDialog()
        If win_InputInvoicePembelian.BukaFormPengajuanPengeluaranBankCash Then BukaFormPengajuanPengeluaranBankCash()
    End Sub
    Sub BukaFormPengajuanPengeluaranBankCash()
        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.dtp_TanggalKK.SelectedDate = win_InputInvoicePembelian.dtp_TanggalDiterimaInvoice.SelectedDate
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        If MitraSebagaiAfiliasi(win_InputInvoicePembelian.KodeSupplier) = True Then
            win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangUsaha_Afiliasi
        Else
            win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangUsaha_NonAfiliasi
        End If
        win_InputBuktiPengeluaran.NomorBP = KonversiNomorPembelianKeNomorBPHU(KonversiNomorInvoiceKeNomorPembelian(win_InputInvoicePembelian.NomorInvoice))
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = win_InputInvoicePembelian.KodeSupplier
        IsiValueComboBypassTerkunci(win_InputBuktiPengeluaran.cmb_SaranaPembayaran, win_InputInvoicePembelian.SaranaPembayaran)
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
    End Sub
    Sub IsiValueForm_InvoicePembelian()
        ProsesIsiValueForm = True
        win_InputInvoicePembelian.AngkaInvoice = AngkaInvoice_Terseleksi
        win_InputInvoicePembelian.JenisProduk_Induk = JenisProduk_Terseleksi
        win_InputInvoicePembelian.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        win_InputInvoicePembelian.NomorPembelian = NomorPembelian_Terseleksi
        If AmbilTeksKanan(JatuhTempo_Terseleksi, 2) = "ri" Then
            win_InputInvoicePembelian.txt_JumlahHariJatuhTempo.Text = AmbilAngka(JatuhTempo_Terseleksi)
            win_InputInvoicePembelian.dtp_TanggalJatuhTempo.Text = Kosongan
            win_InputInvoicePembelian.rdb_JumlahHariJatuhTempo.IsChecked = True
        Else
            win_InputInvoicePembelian.txt_JumlahHariJatuhTempo.Text = Kosongan
            win_InputInvoicePembelian.dtp_TanggalJatuhTempo.SelectedDate = TanggalFormatWPF(JatuhTempo_Terseleksi)
            win_InputInvoicePembelian.rdb_TanggalJatuhTempo.IsChecked = True
        End If
        win_InputInvoicePembelian.cmb_JenisInvoice.SelectedValue = JenisInvoice_Terseleksi
        win_InputInvoicePembelian.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        win_InputInvoicePembelian.txt_NamaSupplier.Text = NamaSupplier_Terseleksi
        win_InputInvoicePembelian.txt_JumlahNota.Text = JumlahHarga_Terseleksi
        win_InputInvoicePembelian.ReturDPP = ReturDPP_Terseleksi
        win_InputInvoicePembelian.ReturPPN = ReturPPN_Terseleksi
        IsiValueElemenRichTextBox(win_InputInvoicePembelian.txt_Catatan, Catatan_Terseleksi)
        win_InputInvoicePembelian.NomorJV = NomorJV_Terseleksi
        win_InputInvoicePembelian.NomorFakturPajak = NomorFakturPajak_Terseleksi
        win_InputInvoicePembelian.NP = NP_Terseleksi
        If NP_Terseleksi = "N" Then
            win_InputInvoicePembelian.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice_Terseleksi)
        Else
            win_InputInvoicePembelian.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalPembetulan_Terseleksi)
            win_InputInvoicePembelian.TanggalInvoice = TanggalInvoice_Terseleksi
        End If
        win_InputInvoicePembelian.IsiTabelProduk()
        win_InputInvoicePembelian.IsiTabelSJBAST()
        ProsesIsiValueForm = False
    End Sub



    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            MsgBox("Invoice ini tidak dapat dihapus karena sudah ada data pembayaran..!" & Enter2Baris &
                   "Jika ingin menghapusnya, silakan hapus terlebih dahulu data pembayaran terkait Invoice ini.")
            Return
        End If
        AksesDatabase_Transaksi(Tutup)

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        'Hapus data-data terkait :
        BukaDatabaseTransaksiGeneral() '----------------------------------------
        PesanUntukProgrammer("Koneksi :" & StatusKoneksiDatabase)
        HapusDataPembelian_BerdasarkanNomorInvoice(NomorInvoice_Terseleksi)
        PesanUntukProgrammer("Hapus Data Pembelian :" & StatusKoneksiDatabase)
        HapusDataAsset_BerdasarkanNomorPembelian(NomorPembelian_Terseleksi)
        PesanUntukProgrammer("Hapus Data Asset :" & StatusKoneksiDatabase)
        HapusDataAmortisasi_BerdasarkanNomorPembelian(NomorPembelian_Terseleksi)
        PesanUntukProgrammer("Hapus Data Amortisasi :" & StatusKoneksiDatabase)
        HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)
        PesanUntukProgrammer("Hapus Data Jurnal :" & StatusKoneksiDatabase)
        TutupDatabaseTransaksiGeneral() '_______________________________________

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
            frm_BukuPembelian.TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub


    Private Sub btn_LihatInvoice_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatInvoice.Click
        win_InputInvoicePembelian = New wpfWin_InputInvoicePembelian
        win_InputInvoicePembelian.ResetForm()
        win_InputInvoicePembelian.FungsiForm = FungsiForm_LIHAT
        win_InputInvoicePembelian.AsalPembelian = AsalPembelian
        win_InputInvoicePembelian.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePembelian.MetodePembayaran = MetodePembayaran
        IsiValueForm_InvoicePembelian()
        win_InputInvoicePembelian.ShowDialog()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi = 0 Then
            MsgBox("Invoice ini belum didorong ke Jurnal.")
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
            NomorInvoice_Pembetulan = Microsoft.VisualBasic.Replace(NomorInvoice_Terseleksi, NP_Terseleksi, NP_Pembetulan)
        End If
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
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
            MsgBox("Invoice ini sudah pernah dibetulkan." & Enter2Baris & "Silakan pilih pembetulan yang terakhir terkait invoice ini.")
            Return
        End If

        'Isi Variabel :
        frm_Input_InvoicePembelian.ResetForm()
        frm_Input_InvoicePembelian.FungsiForm = FungsiForm_PEMBETULAN
        IsiValueForm_InvoicePembelian()

        'Reset Variabel-bariabel Tertentu :
        frm_Input_InvoicePembelian.NomorJV = 0
        frm_Input_InvoicePembelian.ReturDPP = 0
        frm_Input_InvoicePembelian.ReturPPN = 0
        EksekusiKode = False
        frm_Input_InvoicePembelian.dtp_TanggalInvoice.Value = Today
        frm_Input_InvoicePembelian.TanggalInvoice = TanggalInvoice_Terseleksi
        EksekusiKode = True

        frm_Input_InvoicePembelian.ShowDialog()

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub cmb_JenisTampilan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisTampilan.SelectionChanged
        JenisTampilan = cmb_JenisTampilan.SelectedValue
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
        NomorPembelian_Terseleksi = rowviewUtama("Nomor_Pembelian")
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
        KodeSupplier_Terseleksi = rowviewUtama("Kode_Supplier")
        NamaSupplier_Terseleksi = rowviewUtama("Nama_Supplier")
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
        'If AmbilAngka(e.Row.Item("Nomor_JV_Pembelian")) = 0 Then
        '    If JenisTahunBuku = JenisTahunBuku_NORMAL Then e.Row.Foreground = WarnaDataTahunLalu_WPF
        'Else
        '    e.Row.Foreground = WarnaTegas_WPF
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
    Dim Nomor_Pembelian As New DataGridTextColumn
    Dim N_P As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Tanggal_Pembetulan As New DataGridTextColumn
    Dim Tanggal_Lapor As New DataGridTextColumn
    Dim Jatuh_Tempo As New DataGridTextColumn
    Dim Nomor_SJ_BAST As New DataGridTextColumn
    Dim Tanggal_SJ_BAST As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Tanggal_PO As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Kode_Supplier As New DataGridTextColumn
    Dim Nama_Supplier As New DataGridTextColumn
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
    Dim PPh_Dipotong As New DataGridTextColumn
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
        datatabelUtama.Columns.Add("Nomor_Pembelian")
        datatabelUtama.Columns.Add("N_P")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Pembetulan")
        datatabelUtama.Columns.Add("Tanggal_Lapor")
        datatabelUtama.Columns.Add("Jatuh_Tempo")
        datatabelUtama.Columns.Add("Nomor_SJ_BAST")
        datatabelUtama.Columns.Add("Tanggal_SJ_BAST")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Tanggal_PO")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Kode_Supplier")
        datatabelUtama.Columns.Add("Nama_Supplier")
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
        datatabelUtama.Columns.Add("PPh_Dipotong", GetType(Int64))
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
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Pembelian, "Nomor_Pembelian", "Nomor Pembelian", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, N_P, "N_P", "N/P", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pembetulan, "Tanggal_Pembetulan", "Tanggal Pembetulan", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Lapor, "Tanggal_Lapor", "Tanggal Lapor", 75, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jatuh_Tempo, "Jatuh_Tempo", "Jatuh Tempo", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ_BAST, "Nomor_SJ_BAST", "Nomor SJ/BAST", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ_BAST, "Tanggal_SJ_BAST", "Tanggal SJ/BAST", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PO, "Tanggal_PO", "Tanggal PO", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Supplier, "Kode_Supplier", "Kode Supplier", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Supplier, "Nama_Supplier", "Nama Supplier", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mata_Uang, "Kode_Mata_Uang", "Mata Uang", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga, "Jumlah_Harga", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Asing, "Jumlah_Harga_Asing", "Jumlah Harga", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Rp, "Diskon_Rp", "Diskon (RP)", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Asing, "Diskon_Asing", "Diskon", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Prosentase_Termin, "Prosentase_Termin", "Termin (%)", 57, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Dasar_Pengenaan_Pajak, "Dasar_Pengenaan_Pajak", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PPN, "Jenis_PPN", "Jenis PPN", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_, "PPN_", "PPN", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tagihan_Kotor, "Tagihan_Kotor", "Tagihan Kotor", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong, "PPh_Dipotong", "PPh Dipotong", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
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
        StatusAktif = False
    End Sub

End Class
