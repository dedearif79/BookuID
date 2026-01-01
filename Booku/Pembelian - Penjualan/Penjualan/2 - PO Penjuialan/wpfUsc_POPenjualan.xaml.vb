Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfUsc_POPenjualan

    Public StatusAktif As Boolean
    Public JudulForm As String

    Public JenisProduk_Menu

    'Variabel Tabel :
    Dim NomorUrut
    Dim AngkaPO
    Dim AngkaPO_Sebelumnya
    Dim NomorPO
    Dim TanggalPO
    Dim JenisProduk
    Dim TermOfPayment
    Dim KeteranganToP
    Dim KodeProject
    Dim KodeCustomer
    Dim NamaCustomer
    Dim JumlahHarga As Int64
    Dim JumlahHarga_Asing As Decimal
    Dim DiskonRp As Int64
    Dim DiskonAsing As Decimal
    Dim DasarPengenaanPajak As Int64
    Dim JenisPPN
    Dim PerlakuanPPN
    Dim PPN As Int64
    Dim TotalTagihan As Int64
    Dim TotalTagihan_Asing As Decimal
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim JangkaWaktuPenyelesaian
    Dim Catatan
    Dim Kontrol

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Public AngkaPO_Terseleksi
    Dim NomorPO_Terseleksi
    Dim TanggalPO_Terseleksi
    Dim JenisProduk_Terseleksi
    Dim KodeProjectProduk_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim JumlahHarga_Terseleksi
    Dim DiskonRp_Terseleksi
    Dim DasarPengenaanPajak_Terseleksi
    Dim JenisPPN_Terseleksi
    Dim PerlakuanPPN_Terseleksi
    Dim PPN_Terseleksi
    Dim TotalTagihan_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim TanggalSJBAST_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim JangkaWaktuPenyelesaian_Terseleksi
    Dim Catatan_Terseleksi
    Dim Kontrol_Terseleksi

    'Variabel Bundelan SJBAST :
    Dim NamaFileDataSJBAST = "DataSJBAST.txt"
    Dim DataSJBAST
    Dim NomorSJBAST_Bundelan = Kosongan

    'Variabel Bundelan Invoice :
    Dim NamaFileDataInvoice = "DataInvoice.txt"
    Dim DataInvoice
    Dim NomorInvoice_Bundelan = Kosongan

    'Variabel Filter :
    Dim Pilih_JenisProduk_Induk
    Dim Pilih_Kontrol
    Dim Pilih_KodeCustomer

    Public DestinasiPenjualan
    Dim PenjualanLokal As Boolean
    Dim PenjualanEkspor As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StatusAktif = True

        lbl_JudulForm.Text = JudulForm
        Terabas()

        ProsesLoadingForm = True

        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            PenjualanLokal = True
            PenjualanEkspor = False
        Else
            PenjualanLokal = False
            PenjualanEkspor = True
        End If

        LogikaDestinasiPenjualan()

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_JenisProduk_Induk()
        Select Case JudulForm
            Case frm_POPenjualan_Lokal_Barang.JudulForm
                cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
            Case frm_POPenjualan_Lokal_Jasa.JudulForm
                cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
            Case frm_POPenjualan_Lokal_BarangDanJasa.JudulForm
                cmb_JenisProduk_Induk.SelectedValue = JenisProduk_BarangDanJasa
            Case frm_POPenjualan_Lokal_JasaKonstruksi.JudulForm
                cmb_JenisProduk_Induk.SelectedValue = JenisProduk_JasaKonstruksi
            Case frm_POPenjualan_Lokal_Semua.JudulForm
                cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
            Case frm_POPenjualan_Ekspor.JudulForm
                cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        End Select
        KontenCombo_Kontrol()
        KontenCombo_Customer()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Dim EksekusiTampilanData As Boolean
    Sub TampilkanData()

        If EksekusiTampilanData = False Then Return

        'PesanUntukProgrammer("Jenis Produk : " & Pilih_JenisProduk_Induk & Enter2Baris &
        '                      "Kontrol : " & Pilih_Kontrol)

        'Filter Jenis Produk Induk :
        Dim FilterJenisProduk_Induk = " "
        If Pilih_JenisProduk_Induk <> JenisProduk_Semua Then FilterJenisProduk_Induk = " AND Jenis_Produk_Induk = '" & Pilih_JenisProduk_Induk & "' "

        'FilterKontrol
        Dim FilterKontrol = Kosongan
        If Pilih_Kontrol = Status_Semua Then FilterKontrol = " "
        If Pilih_Kontrol = Status_Open Then FilterKontrol = " AND Kontrol = '" & Status_Open & "' "
        If Pilih_Kontrol = Status_Used Then FilterKontrol = " AND Kontrol = '" & Status_Used & "' "
        If Pilih_Kontrol = Status_Closed Then FilterKontrol = " AND Kontrol = '" & Status_Closed & "' "

        'Filter Customer :
        Dim FilterCustomer = Spasi1
        If cmb_Customer.SelectedValue <> Pilihan_Semua Then FilterCustomer = " AND Kode_Customer = '" & Pilih_KodeCustomer & "' "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterKontrol & FilterCustomer


        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Data Tabel :
        NomorUrut = 0
        AngkaPO_Sebelumnya = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                              " WHERE Nomor_PO <> 'X' " & FilterData &
                              " ORDER BY Angka_PO ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            AngkaPO = dr.Item("Angka_PO")
            NomorPO = dr.Item("Nomor_PO")
            TanggalPO = TanggalFormatTampilan(dr.Item("Tanggal_PO"))
            JenisProduk = dr.Item("Jenis_Produk_Induk")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            KodeProject = dr.Item("Kode_Project_Produk")
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
            JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan")
            JumlahHarga_Asing = AmbilValue_JumlahHargaKeseluruhanAsingBerdasarkanNomorPOPenjualan(NomorPO)
            DiskonRp = dr.Item("Diskon")
            DiskonAsing = AmbilValue_DiskonAsingBerdasarkanNomorPOPenjualan(NomorPO)
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            PPN = dr.Item("PPN")
            TotalTagihan = dr.Item("Total_Tagihan")
            TotalTagihan_Asing = JumlahHarga_Asing - DiskonAsing
            TermOfPayment = dr.Item("Term_Of_Payment")
            KeteranganToP = dr.Item("Keterangan_ToP")
            If AmbilAngka(TermOfPayment) > 0 Then
                TermOfPayment &= " Hari"
            Else
                TermOfPayment = Kosongan
            End If
            If TermOfPayment = Kosongan Then
                TermOfPayment = KeteranganToP
            Else
                If KeteranganToP <> Kosongan Then TermOfPayment = TermOfPayment & " / " & KeteranganToP
            End If
            SJBAST_Invoice()
            JangkaWaktuPenyelesaian = dr.Item("Jumlah_Hari_Jangka_Waktu_Penyelesaian")
            If JangkaWaktuPenyelesaian > 0 Then
                JangkaWaktuPenyelesaian &= " hari"
            Else
                JangkaWaktuPenyelesaian = TanggalFormatTampilan(dr.Item("Tanggal_Jangka_Waktu_Penyelesaian"))
            End If
            Catatan = PenghapusEnter(dr.Item("Catatan"))
            Kontrol = dr.Item("Kontrol")
            If AngkaPO <> AngkaPO_Sebelumnya Then
                If PenjualanLokal And Not MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then TambahBaris()
                If PenjualanEkspor And MitraSebagaiPerusahaanLuarNegeri(KodeCustomer) Then TambahBaris()
            End If
            AngkaPO_Sebelumnya = AngkaPO
        Loop

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub



    Sub SJBAST_Invoice()
        NomorSJBAST = Kosongan
        TanggalSJBAST = Kosongan
        NomorInvoice = Kosongan
        TanggalInvoice = Kosongan
        Dim NomorSJBAST_Satuan = Kosongan
        Dim NomorSJBAST_Sebelumnya = Kosongan
        'Surat Jalan : ---------------------------------------------------
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_SJ " &
                                     " WHERE Nomor_PO_Produk = '" & NomorPO & "' ",
        KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            NomorSJBAST_Satuan = drTELUSUR.Item("Nomor_SJ")
            If NomorSJBAST_Satuan <> NomorSJBAST_Sebelumnya Then
                If NomorSJBAST = Kosongan Then
                    NomorSJBAST = NomorSJBAST_Satuan
                    TanggalSJBAST = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_SJ"))
                Else
                    NomorSJBAST &= SlashGanda_Pemisah & NomorSJBAST_Satuan
                    TanggalSJBAST &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR.Item("Tanggal_SJ"))
                End If
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                              " WHERE Nomor_SJ_BAST_Produk = '" & NomorSJBAST_Satuan & "' ",
                                              KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                Dim NomorInvoice_Satuan = Kosongan
                Dim NomorInvoice_Sebelumnya = Kosongan
                Do While drTELUSUR2.Read
                    NomorInvoice_Satuan = drTELUSUR2.Item("Nomor_Invoice")
                    If NomorInvoice_Satuan <> NomorInvoice_Sebelumnya Then
                        If NomorInvoice = Kosongan Then
                            NomorInvoice = NomorInvoice_Satuan
                            TanggalInvoice = TanggalFormatTampilan(drTELUSUR2("Tanggal_Invoice"))
                        Else
                            NomorInvoice &= SlashGanda_Pemisah & NomorInvoice_Satuan
                            TanggalInvoice &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2("Tanggal_Invoice"))
                        End If
                    End If
                    NomorInvoice_Sebelumnya = NomorInvoice_Satuan
                Loop
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
        Loop
        'BAST : ------------------------------------------------------
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                         " WHERE Nomor_PO_Produk = '" & NomorPO & "' ",
        KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            NomorSJBAST_Satuan = drTELUSUR.Item("Nomor_BAST")
            If NomorSJBAST_Satuan <> NomorSJBAST_Sebelumnya Then
                If NomorSJBAST = Kosongan Then
                    NomorSJBAST = NomorSJBAST_Satuan
                    TanggalSJBAST = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_BAST"))
                Else
                    NomorSJBAST &= SlashGanda_Pemisah & NomorSJBAST_Satuan
                    TanggalSJBAST &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR.Item("Tanggal_BAST"))
                End If
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                              " WHERE Nomor_SJ_BAST_Produk = '" & NomorSJBAST_Satuan & "' ",
                                              KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                Dim NomorInvoice_Satuan = Kosongan
                Dim NomorInvoice_Sebelumnya = Kosongan
                Do While drTELUSUR2.Read
                    NomorInvoice_Satuan = drTELUSUR2.Item("Nomor_Invoice")
                    If NomorInvoice_Satuan <> NomorInvoice_Sebelumnya Then
                        If NomorInvoice = Kosongan Then
                            NomorInvoice = NomorInvoice_Satuan
                            TanggalInvoice = TanggalFormatTampilan(drTELUSUR2("Tanggal_Invoice"))
                        Else
                            NomorInvoice &= SlashGanda_Pemisah & NomorInvoice_Satuan
                            TanggalInvoice &= SlashGanda_Pemisah & TanggalFormatTampilan(drTELUSUR2("Tanggal_Invoice"))
                        End If
                    End If
                    NomorInvoice_Sebelumnya = NomorInvoice_Satuan
                Loop
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
        Loop
    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, AngkaPO, NomorPO, TanggalPO, JenisProduk, KodeCustomer, NamaCustomer, KodeProject,
                                JumlahHarga, JumlahHarga_Asing, DiskonRp, DiskonAsing, DasarPengenaanPajak,
                                JenisPPN, PerlakuanPPN, PPN, TotalTagihan, TotalTagihan_Asing, TermOfPayment,
                                NomorSJBAST, TanggalSJBAST, NomorInvoice, TanggalInvoice, JangkaWaktuPenyelesaian, Catatan, Kontrol)
        Terabas()
    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
    End Sub


    Sub KontenCombo_JenisProduk_Induk()
        cmb_JenisProduk_Induk.Items.Clear()
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Barang)
        If DestinasiPenjualan = DestinasiPenjualan_Lokal Then
            cmb_JenisProduk_Induk.Items.Add(JenisProduk_Jasa)
            cmb_JenisProduk_Induk.Items.Add(JenisProduk_BarangDanJasa)
            cmb_JenisProduk_Induk.Items.Add(JenisProduk_JasaKonstruksi)
        End If
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Semua)
    End Sub

    Sub KontenCombo_Kontrol()
        cmb_Kontrol.Items.Clear()
        cmb_Kontrol.Items.Add(Status_Semua)
        cmb_Kontrol.Items.Add(Status_Open)
        cmb_Kontrol.Items.Add(Status_Used)
        cmb_Kontrol.Items.Add(Status_Closed)
        cmb_Kontrol.SelectedValue = Status_Semua
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


    Public Sub VisibilitasFilterJenisProdukInduk(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_FilterJenisProduk.Visibility = Visibility.Visible
            cmb_JenisProduk_Induk.Visibility = Visibility.Visible
            brd_FilterJenisProduk.Visibility = Visibility.Visible
        Else
            lbl_FilterJenisProduk.Visibility = Visibility.Collapsed
            cmb_JenisProduk_Induk.Visibility = Visibility.Collapsed
            brd_FilterJenisProduk.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub LogikaDestinasiPenjualan()
        Jumlah_Harga.Visibility = Visibility.Visible
        Diskon_Rp.Visibility = Visibility.Visible
        Total_Tagihan.Visibility = Visibility.Visible
        Jumlah_Harga_Asing.Visibility = Visibility.Collapsed
        Diskon_Asing.Visibility = Visibility.Collapsed
        Total_Tagihan_Asing.Visibility = Visibility.Collapsed
        Dasar_Pengenaan_Pajak.Visibility = Visibility.Visible
        PPN_.Visibility = Visibility.Visible
        Nomor_SJ_BAST.Visibility = Visibility.Visible
        Tanggal_SJ_BAST.Visibility = Visibility.Visible
        If PenjualanEkspor Then
            Jumlah_Harga.Visibility = Visibility.Collapsed
            Diskon_Rp.Visibility = Visibility.Collapsed
            Total_Tagihan.Visibility = Visibility.Collapsed
            Jumlah_Harga_Asing.Visibility = Visibility.Visible
            Diskon_Asing.Visibility = Visibility.Visible
            Total_Tagihan_Asing.Visibility = Visibility.Visible
            Dasar_Pengenaan_Pajak.Visibility = Visibility.Collapsed
            PPN_.Visibility = Visibility.Collapsed
            Nomor_SJ_BAST.Visibility = Visibility.Collapsed
            Tanggal_SJ_BAST.Visibility = Visibility.Collapsed
        End If
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        If JenisProduk_Menu = JenisProduk_Semua Then
            MsgBox("Silakan tentukan 'Jenis Produk' terlebih dahulu.")
            cmb_JenisProduk_Induk.Focus()
            Return
        End If

        win_InputPOPenjualan = New wpfWin_InputPOPenjualan
        win_InputPOPenjualan.ResetForm()
        win_InputPOPenjualan.FungsiForm = FungsiForm_TAMBAH
        win_InputPOPenjualan.DestinasiPenjualan = DestinasiPenjualan
        win_InputPOPenjualan.JenisProduk_Induk = JenisProduk_Menu
        win_InputPOPenjualan.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        Dim BisaDiedit As Boolean

        If Kontrol_Terseleksi <> Status_Open Then
            BisaDiedit = False
            MsgBox("PO ini sudah tidak dapat diedit." & Enter2Baris &
                   "Jika ingin mengeditnya, silakan hapus terlebih dahulu data PO ini yang tersimpan di Surat Jalan/BAST dan/atau Invoice.")
        Else
            BisaDiedit = True
        End If

        win_InputPOPenjualan = New wpfWin_InputPOPenjualan
        win_InputPOPenjualan.ResetForm()
        If BisaDiedit = False Then win_InputPOPenjualan.btn_Simpan.IsEnabled = False
        win_InputPOPenjualan.FungsiForm = FungsiForm_EDIT
        win_InputPOPenjualan.DestinasiPenjualan = DestinasiPenjualan
        ProsesIsiValueForm = True
        win_InputPOPenjualan.AngkaPO = AngkaPO_Terseleksi
        win_InputPOPenjualan.txt_NomorPO.Text = NomorPO_Terseleksi
        win_InputPOPenjualan.dtp_TanggalPO.SelectedDate = TanggalFormatWPF(TanggalPO_Terseleksi)
        win_InputPOPenjualan.JenisProduk_Induk = JenisProduk_Terseleksi
        win_InputPOPenjualan.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
        win_InputPOPenjualan.cmb_JenisPPN.SelectedValue = JenisPPN_Terseleksi
        win_InputPOPenjualan.cmb_PerlakuanPPN.SelectedValue = PerlakuanPPN_Terseleksi
        win_InputPOPenjualan.IsiTabelProduk()
        win_InputPOPenjualan.txt_JumlahNota.Text = JumlahHarga_Terseleksi
        IsiValueElemenRichTextBox(win_InputPOPenjualan.txt_Catatan, Catatan_Terseleksi)
        If Right(JangkaWaktuPenyelesaian_Terseleksi, 2) = "ri" Then
            win_InputPOPenjualan.txt_JumlahHariJangkaWaktuPenyelesaian.Text = AmbilAngka(JangkaWaktuPenyelesaian_Terseleksi)
            win_InputPOPenjualan.dtp_TanggalJangkaWaktuPenyelesaian.Text = Kosongan
            win_InputPOPenjualan.rdb_JumlahHariJangkaWaktuPenyelesaian.IsChecked = True
        Else
            win_InputPOPenjualan.txt_JumlahHariJangkaWaktuPenyelesaian.Text = Kosongan
            win_InputPOPenjualan.dtp_TanggalJangkaWaktuPenyelesaian.SelectedDate = TanggalFormatWPF(JangkaWaktuPenyelesaian_Terseleksi)
            win_InputPOPenjualan.rdb_TanggalJangkaWaktuPenyelesaian.IsChecked = True
        End If
        IsiValueComboBypassTerkunci(win_InputPOPenjualan.cmb_Kontrol, Kontrol_Terseleksi)
        ProsesIsiValueForm = False
        win_InputPOPenjualan.ShowDialog()

    End Sub

    Sub IsiTabelProduk()
        Dim JenisProduk_PerItem
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
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                              " WHERE Angka_PO = '" & AngkaPO_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut += 1
            JenisProduk_PerItem = dr.Item("Jenis_Produk_Per_Item")
            KodeProject = dr.Item("Kode_Project_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            HargaSatuan = dr.Item("Harga_Satuan")
            JumlahHargaPerItem = JumlahProduk * HargaSatuan
            DiskonPerItem_Persen = FormatUlangDesimal_Prosentase(dr.Item("Diskon_Per_Item"))
            DiskonPerItem_Rp = JumlahHargaPerItem * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem = dr.Item("Total_Harga_Per_Item")
            frm_Input_POPenjualan.DataTabelUtama.Rows.Add(NomorUrut, JenisProduk_PerItem, NamaProduk, DeskripsiProduk,
                                                          JumlahProduk, SatuanProduk, HargaSatuan, JumlahHargaPerItem,
                                                          (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHargaPerItem, KodeProject)
        Loop
        AksesDatabase_Transaksi(Tutup)
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Kontrol_Terseleksi <> Status_Open Then
            MsgBox("PO ini sudah tidak dapat dihapus." & Enter2Baris &
                   "Jika ingin mengahapusnya, silakan hapus terlebih dahulu data PO ini yang tersimpan di Surat Jalan/BAST dan/atau Invoice.")
            Return
        End If

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Penjualan_PO " &
                                   " WHERE Angka_PO = '" & AngkaPO_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub



    Private Sub cmb_JenisProduk_Induk_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisProduk_Induk.SelectionChanged
        Pilih_JenisProduk_Induk = cmb_JenisProduk_Induk.SelectedValue
        JenisProduk_Menu = cmb_JenisProduk_Induk.SelectedValue
        TampilkanData()
    End Sub


    Private Sub cmb_Kontrol_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Kontrol.SelectionChanged
        Pilih_Kontrol = cmb_Kontrol.SelectedValue
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
        AngkaPO_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Angka_PO"))
        NomorPO_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_PO")
        TanggalPO_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_PO")
        JenisProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jenis_Produk")
        KodeProjectProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Project")
        KodeCustomer_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Customer")
        NamaCustomer_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Customer")
        JumlahHarga_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Harga"))
        DiskonRp_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Diskon_Rp"))
        DasarPengenaanPajak_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Dasar_Pengenaan_Pajak"))
        JenisPPN_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jenis_PPN")
        PerlakuanPPN_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Perlakuan_PPN")
        PPN_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "PPN_"))
        TotalTagihan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Total_Tagihan"))
        NomorSJBAST_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_SJ_BAST")
        TanggalSJBAST_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_SJ_BAST")
        NomorInvoice_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Invoice")
        TanggalInvoice_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Invoice")
        Catatan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Catatan_")
        JangkaWaktuPenyelesaian_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jangka_Waktu_Penyelesaian")
        Kontrol_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kontrol_")

        If AngkaPO_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        'If AmbilAngka(e.Row.Item("Nomor_JV_Penjualan")) = 0 Then
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
    Dim Angka_PO As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Tanggal_PO As New DataGridTextColumn
    Dim Jenis_Produk As New DataGridTextColumn
    Dim Kode_Customer As New DataGridTextColumn
    Dim Nama_Customer As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Jumlah_Harga As New DataGridTextColumn
    Dim Jumlah_Harga_Asing As New DataGridTextColumn
    Dim Diskon_Rp As New DataGridTextColumn
    Dim Diskon_Asing As New DataGridTextColumn
    Dim Dasar_Pengenaan_Pajak As New DataGridTextColumn
    Dim Jenis_PPN As New DataGridTextColumn
    Dim Perlakuan_PPN As New DataGridTextColumn
    Dim PPN_ As New DataGridTextColumn
    Dim Total_Tagihan As New DataGridTextColumn
    Dim Total_Tagihan_Asing As New DataGridTextColumn
    Dim Term_Of_Payment As New DataGridTextColumn
    Dim Nomor_SJ_BAST As New DataGridTextColumn
    Dim Tanggal_SJ_BAST As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Jangka_Waktu_Penyelesaian As New DataGridTextColumn
    Dim Catatan_ As New DataGridTextColumn
    Dim Kontrol_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Angka_PO")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Tanggal_PO")
        datatabelUtama.Columns.Add("Jenis_Produk")
        datatabelUtama.Columns.Add("Kode_Customer")
        datatabelUtama.Columns.Add("Nama_Customer")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Jumlah_Harga", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Harga_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Diskon_Rp", GetType(Int64))
        datatabelUtama.Columns.Add("Diskon_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Dasar_Pengenaan_Pajak", GetType(Int64))
        datatabelUtama.Columns.Add("Jenis_PPN")
        datatabelUtama.Columns.Add("Perlakuan_PPN")
        datatabelUtama.Columns.Add("PPN_", GetType(Int64))
        datatabelUtama.Columns.Add("Total_Tagihan", GetType(Int64))
        datatabelUtama.Columns.Add("Total_Tagihan_Asing", GetType(Decimal))
        datatabelUtama.Columns.Add("Term_Of_Payment")
        datatabelUtama.Columns.Add("Nomor_SJ_BAST")
        datatabelUtama.Columns.Add("Tanggal_SJ_BAST")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Jangka_Waktu_Penyelesaian")
        datatabelUtama.Columns.Add("Catatan_")
        datatabelUtama.Columns.Add("Kontrol_")


        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angka_PO, "Angka_PO", "Angka PO", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PO, "Tanggal_PO", "Tanggal PO", 75, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Produk, "Jenis_Produk", "Jenis Produk", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Customer, "Kode_Customer", "Kode Customer", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Customer, "Nama_Customer", "Nama Customer", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga, "Jumlah_Harga", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Asing, "Jumlah_Harga_Asing", "Jumlah Harga", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Rp, "Diskon_Rp", "Diskon", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Diskon_Asing, "Diskon_Asing", "Diskon", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Dasar_Pengenaan_Pajak, "Dasar_Pengenaan_Pajak", "DPP", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PPN, "Jenis_PPN", "Jenis PPN", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Perlakuan_PPN, "Perlakuan_PPN", "Perlakuan PPN", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPN_, "PPN_", "PPN", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Tagihan, "Total_Tagihan", "Total Tagihan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Tagihan_Asing, "Total_Tagihan_Asing", "Total Tagihan", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Term_Of_Payment, "Term_Of_Payment", "Term of Payment", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ_BAST, "Nomor_SJ_BAST", "Nomor SJ/BAST", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ_BAST, "Tanggal_SJ_BAST", "Tanggal SJ/BAST", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jangka_Waktu_Penyelesaian, "Jangka_Waktu_Penyelesaian", "Jangka Waktu Penylsn", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Catatan_, "Catatan_", "Catatan", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kontrol_, "Kontrol_", "Kontrol", 63, FormatString, KiriTengah, KunciUrut, Terlihat)

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
