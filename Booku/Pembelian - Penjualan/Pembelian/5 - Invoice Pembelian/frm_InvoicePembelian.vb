Imports bcomm
Imports System.Data.Odbc

Public Class frm_InvoicePembelian

    Public KesesuaianJurnal As Boolean
    Public JenisProduk_Menu
    Public JudulForm
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
    Dim TanggalPembetulan
    Dim Tanggallapor
    Dim JatuhTempo
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim TanggalDiterima
    Dim NomorPO
    Dim TanggalPO
    Dim KodeProject
    Dim KodeSupplier
    Dim NamaSupplier
    Dim JumlahHarga
    Dim DiskonRp
    Dim DasarPengenaanPajak
    Dim NomorFakturPajak
    Dim JenisPPN
    Dim PPN
    Dim PPhDipotong
    Dim TagihanKotor
    Dim ReturDPP
    Dim ReturPPN
    Dim Retur
    Dim Catatan
    Dim NomorJV

    'Variabel Total :
    Dim Total_JumlahHarga
    Dim Total_DiskonRp
    Dim Total_DasarPengenaanPajak
    Dim Total_PPN
    Dim Total_PPhDipotong
    Dim Total_Retur
    Dim Total_TagihanKotor

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

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        If JenisProduk_Menu = JenisProduk_Barang Then
            DataTabelUtama.Columns("PPh_Dipotong").Visible = False
        Else
            DataTabelUtama.Columns("PPh_Dipotong").Visible = True
        End If

        If InvoiceDenganPO = True Then
            DataTabelUtama.Columns("Nomor_SJ_BAST").Visible = True
            DataTabelUtama.Columns("Tanggal_SJ_BAST").Visible = True
            DataTabelUtama.Columns("Nomor_PO").Visible = True
            DataTabelUtama.Columns("Tanggal_PO").Visible = True
        Else
            DataTabelUtama.Columns("Nomor_SJ_BAST").Visible = False
            DataTabelUtama.Columns("Tanggal_SJ_BAST").Visible = False
            DataTabelUtama.Columns("Nomor_PO").Visible = False
            DataTabelUtama.Columns("Tanggal_PO").Visible = False
        End If

        If InvoiceDenganPO = True Then JudulForm = "Invoice Pembelian"
        If InvoiceDenganPO = False Then JudulForm = "Invoice Pembelian - " & JenisProduk_Menu

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

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

    Sub KontenCombo_JenisTampilan()
        cmb_JenisTampilan.Items.Clear()
        cmb_JenisTampilan.Items.Add(JenisTampilan_Semua)
        cmb_JenisTampilan.Items.Add(JenisTampilan_HasilAkhir)
        cmb_JenisTampilan.Text = JenisTampilan_Semua
    End Sub

    Sub KontenCombo_Supplier()
        cmb_Supplier.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Supplier = 1 ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_Supplier.Items.Add(Pilihan_Semua)
        Do While dr.Read
            Dim NamaSupplier = dr.Item("Nama_Mitra")
            cmb_Supplier.Items.Add(NamaSupplier)
        Loop
        cmb_Supplier.Text = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub

    Sub TampilkanData()

        If EksekusiKode = False Then Return

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Filter Invoice Dengan PO :
        Dim FilterInvoiceDenganPO = " "
        If MetodePembayaran = MetodePembayaran_Normal Then
            If InvoiceDenganPO = True Then FilterInvoiceDenganPO = " AND Nomor_PO_Produk <> '' "
            If InvoiceDenganPO = False Then FilterInvoiceDenganPO = " AND Nomor_PO_Produk = '' "
        End If

        'Filter Jenis Produk Induk :
        Dim FilterJenisProdukInduk = " "
        If JenisProduk_Menu <> JenisProduk_Semua Then FilterJenisProdukInduk = " AND Jenis_Produk_Induk = '" & JenisProduk_Menu & "' "

        'Filter Supplier :
        Dim FilterSupplier = " "
        If cmb_Supplier.Text <> Pilihan_Semua Then FilterSupplier = " AND Kode_Supplier = '" & Pilih_KodeSupplier & "' "

        'Filter Metode Pembayaran :
        Dim FilterMetodePembayaran = " "
        If MetodePembayaran <> Pilihan_Semua Then FilterMetodePembayaran = "  AND Metode_Pembayaran = '" & MetodePembayaran & "' "

        'Filter Data :
        Dim FilterData = FilterInvoiceDenganPO & FilterJenisProdukInduk & FilterSupplier & FilterMetodePembayaran

        'Data Tabel :
        Index_BarisTabel = 0
        NomorUrut = 0
        NomorInvoice_Sebelumnya = Kosongan

        'Total :
        Total_JumlahHarga = 0
        Total_DiskonRp = 0
        Total_DasarPengenaanPajak = 0
        Total_PPN = 0
        Total_PPhDipotong = 0
        Total_TagihanKotor = 0
        Total_Retur = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice <> 'X' " & FilterData &
                              " ORDER BY Tanggal_Invoice ", KoneksiDatabaseTransaksi)
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
            NomorInvoice = dr.Item("Nomor_Invoice")
            NomorPembelian = dr.Item("Nomor_Pembelian")
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
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                         " WHERE Nomor_Invoice = '" & NomorInvoice_Pembetulan & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            If drTELUSUR.HasRows And JenisTampilan = JenisTampilan_HasilAkhir Then Continue Do
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
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
                            KodeProject = drTELUSUR3.Item("Kode_Project_Produk")
                        Else
                            NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                            TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_BAST"))
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
                NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
            Loop
            If InvoiceDenganPO = False Then KodeProject = dr.Item("Kode_Project_Produk")
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            JenisPPN = dr.Item("Jenis_PPN")
            JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan")
            DiskonRp = dr.Item("Diskon")
            If DiskonRp = 0 Then DiskonRp = StripKosong
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            PPN = dr.Item("PPN")
            If PPN = 0 Then PPN = StripKosong
            PPhDipotong = dr.Item("PPh_Dipotong")
            If PPhDipotong = 0 Then PPhDipotong = StripKosong
            TagihanKotor = dr.Item("Total_Tagihan")
            ReturDPP = dr.Item("Retur_DPP")
            ReturPPN = dr.Item("Retur_PPN")
            Retur = ReturDPP + ReturPPN
            Catatan = dr.Item("Catatan")
            NomorJV = dr.Item("Nomor_JV")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then TambahBaris()
            NomorInvoice_Sebelumnya = NomorInvoice
        Loop

        AksesDatabase_Transaksi(Tutup)

        If JenisTampilan = JenisTampilan_HasilAkhir Then
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, KodeSupplier, Kosongan,
                                Total_JumlahHarga, Total_DiskonRp, Total_DasarPengenaanPajak, Kosongan, Kosongan, Total_PPN, Total_PPhDipotong,
                                Total_TagihanKotor, Kosongan, Kosongan, Total_Retur, Kosongan)
        End If

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, JenisInvoice, JenisProduk, AngkaInvoice, NomorInvoice, NomorPembelian, NP, TanggalInvoice, TanggalPembetulan, Tanggallapor, JatuhTempo,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject, KodeSupplier, NamaSupplier,
                                JumlahHarga, DiskonRp, DasarPengenaanPajak, NomorFakturPajak, JenisPPN, PPN, PPhDipotong,
                                TagihanKotor, ReturDPP, ReturPPN, Retur, Catatan, NomorJV)
        If NomorJV > 0 Then
            If NP = "N" Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaTegas
            If NP <> "N" Then DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaBiruSolid
        Else
            DataTabelUtama.Rows(Index_BarisTabel).DefaultCellStyle.ForeColor = WarnaPudar
        End If
        'Akumulasi/Total :
        Total_JumlahHarga += AmbilAngka(JumlahHarga)
        Total_DiskonRp += AmbilAngka(DiskonRp)
        Total_DasarPengenaanPajak += AmbilAngka(DasarPengenaanPajak)
        Total_PPN += AmbilAngka(PPN)
        Total_PPhDipotong += AmbilAngka(PPhDipotong)
        Total_TagihanKotor += AmbilAngka(TagihanKotor)
        Total_Retur += AmbilAngka(Retur)
        Index_BarisTabel += 1
    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Pratinjau.Enabled = False
        btn_Cetak.Enabled = False
        btn_LihatJurnal.Enabled = False
        btn_LihatInvoice.Enabled = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        btn_Pembetulan.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_JenisTampilan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisTampilan.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisTampilan_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisTampilan.TextChanged
        JenisTampilan = cmb_JenisTampilan.Text
        TampilkanData()
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

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi = 0 Then
            MsgBox("Invoice ini belum didorong ke Jurnal.")
            Return
        End If
        win_JurnalVoucher = New wpfWin_JurnalVoucher
        win_JurnalVoucher.ResetForm()
        win_JurnalVoucher.FungsiForm = FungsiForm_INFOJURNAL
        win_JurnalVoucher.Angka_NomorJV = NomorJV_Terseleksi
        win_JurnalVoucher.ShowDialog()
    End Sub


    Private Sub btn_LihatInvoice_Click(sender As Object, e As EventArgs) Handles btn_LihatInvoice.Click
        frm_Input_InvoicePembelian.ResetForm()
        frm_Input_InvoicePembelian.FungsiForm = FungsiForm_LIHAT
        IsiValueForm_InvoicePembelian()
        frm_Input_InvoicePembelian.ShowDialog()
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        FormInvoiceBaru_WPF()
        'FormInvoiceLama_WinForm()
    End Sub

    Sub FormInvoiceBaru_WPF()
        win_InputInvoicePembelian = New wpfWin_InputInvoicePembelian
        win_InputInvoicePembelian.ResetForm()
        win_InputInvoicePembelian.FungsiForm = FungsiForm_TAMBAH
        win_InputInvoicePembelian.JenisProduk_Induk = JenisProduk_Menu
        win_InputInvoicePembelian.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePembelian.MetodePembayaran = MetodePembayaran
        win_InputInvoicePembelian.ShowDialog()
        If win_InputInvoicePembelian.BukaFormPengajuanPengeluaranBankCash Then BukaFormPengajuanPengeluaranBankCash()
    End Sub
    Sub FormInvoiceLama_WinForm()
        frm_Input_InvoicePembelian.ResetForm()
        frm_Input_InvoicePembelian.FungsiForm = FungsiForm_TAMBAH
        frm_Input_InvoicePembelian.JenisProduk_Induk = JenisProduk_Menu
        frm_Input_InvoicePembelian.InvoiceDenganPO = InvoiceDenganPO
        frm_Input_InvoicePembelian.ShowDialog()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        Dim BisaDiedit As Boolean

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha " &
                              " WHERE Nomor_Pembelian = '" & NomorPembelian_Terseleksi & "' ", KoneksiDatabaseTransaksi)
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

        'frm_Input_InvoicePembelian.ResetForm()
        'frm_Input_InvoicePembelian.FungsiForm = FungsiForm_EDIT
        'If BisaDiedit = False Then frm_Input_InvoicePembelian.FungsiForm = FungsiForm_LIHAT
        'frm_Input_InvoicePembelian.InvoiceDenganPO = InvoiceDenganPO
        'IsiValueForm_InvoicePembelian()
        'BeginInvoke(Sub() ResetDuaComboPPN())
        'frm_Input_InvoicePembelian.ShowDialog()

        win_InputInvoicePembelian = New wpfWin_InputInvoicePembelian
        win_InputInvoicePembelian.ResetForm()
        win_InputInvoicePembelian.FungsiForm = FungsiForm_EDIT
        If BisaDiedit = False Then win_InputInvoicePembelian.FungsiForm = FungsiForm_LIHAT
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

    Sub ResetDuaComboPPN()
        'Ini kode khusus untuk menangani anomali coding, yang masih sulit dipecahkan solusinya secara normal.
        If PerusahaanSebagaiPKP = False Then
            frm_Input_InvoicePembelian.cmb_PPNDikreditkan.Enabled = False
            frm_Input_InvoicePembelian.cmb_PilihanPPN.Enabled = False
        End If
        If frm_Input_InvoicePembelian.cmb_PPNDikreditkan.Enabled = False Then frm_Input_InvoicePembelian.cmb_PPNDikreditkan.Text = Kosongan
        If frm_Input_InvoicePembelian.cmb_PilihanPPN.Enabled = False Then frm_Input_InvoicePembelian.cmb_PilihanPPN.Text = Kosongan
    End Sub

    Sub IsiValueForm_InvoicePembelian()
        'ProsesIsiValueForm = True
        'frm_Input_InvoicePembelian.AngkaInvoice = AngkaInvoice_Terseleksi
        'frm_Input_InvoicePembelian.JenisProduk_Induk = JenisProduk_Terseleksi
        'frm_Input_InvoicePembelian.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        'frm_Input_InvoicePembelian.NomorPembelian = NomorPembelian_Terseleksi
        'If AmbilTeksKanan(JatuhTempo_Terseleksi, 2) = "ri" Then
        '    frm_Input_InvoicePembelian.txt_JumlahHariJatuhTempo.Text = AmbilAngka(JatuhTempo_Terseleksi)
        '    frm_Input_InvoicePembelian.dtp_TanggalJatuhTempo.Value = Today
        '    frm_Input_InvoicePembelian.rdb_JumlahHariJatuhTempo.Checked = True
        'Else
        '    frm_Input_InvoicePembelian.txt_JumlahHariJatuhTempo.Text = Kosongan
        '    frm_Input_InvoicePembelian.dtp_TanggalJatuhTempo.Value = JatuhTempo_Terseleksi
        '    frm_Input_InvoicePembelian.rdb_TanggalJatuhTempo.Checked = True
        'End If
        'frm_Input_InvoicePembelian.cmb_JenisInvoice.Text = JenisInvoice_Terseleksi
        'frm_Input_InvoicePembelian.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        'frm_Input_InvoicePembelian.txt_NamaSupplier.Text = NamaSupplier_Terseleksi
        'frm_Input_InvoicePembelian.txt_JumlahHargaKeseluruhan.Text = JumlahHarga_Terseleksi
        'frm_Input_InvoicePembelian.ReturDPP = ReturDPP_Terseleksi
        'frm_Input_InvoicePembelian.ReturPPN = ReturPPN_Terseleksi
        'frm_Input_InvoicePembelian.txt_Catatan.Text = Catatan_Terseleksi
        'frm_Input_InvoicePembelian.NomorJV = NomorJV_Terseleksi
        'frm_Input_InvoicePembelian.NomorFakturPajak = NomorFakturPajak_Terseleksi
        'frm_Input_InvoicePembelian.NP = NP_Terseleksi
        'If NP_Terseleksi = "N" Then
        '    frm_Input_InvoicePembelian.dtp_TanggalInvoice.Value = TanggalInvoice_Terseleksi
        'Else
        '    frm_Input_InvoicePembelian.dtp_TanggalInvoice.Value = TanggalPembetulan_Terseleksi
        '    frm_Input_InvoicePembelian.TanggalInvoice = TanggalInvoice_Terseleksi
        'End If
        'IsiTabelProduk_InvoicePembelian()
        'IsiTabelSJBAST()
        'ProsesIsiValueForm = False
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
        Dim KodeDivisiAsset As String
        Dim KelompokAsset
        Dim COABiaya
        Dim MasaAmortisasi
        NomorUrut = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut += 1
            JenisProdukPerItem = dr.Item("Jenis_Produk_Per_Item")
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_SJ_BAST_Produk"))
            TanggalDiterimaSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_Diterima_SJ_BAST_Produk"))
            COAProdukPerItem = dr.Item("COA_Produk")
            NomorPO = dr.Item("Nomor_PO_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            HargaSatuan = dr.Item("Harga_Satuan")
            JumlahHargaPerItem = JumlahProduk * HargaSatuan
            DiskonPerItem_Persen = dr.Item("Diskon_Per_Item")
            DiskonPerItem_Rp = JumlahHargaPerItem * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem = dr.Item("Total_Harga_Per_Item")
            KelompokAsset = dr.Item("Kelompok_Asset")
            KodeDivisiAsset = dr.Item("Kode_Divisi_Asset").ToString
            COABiaya = dr.Item("COA_Biaya")
            MasaAmortisasi = dr.Item("Masa_Amortisasi")
            KodeProject = dr.Item("Kode_Project_Produk")
            frm_Input_InvoicePembelian.DataTabelUtama.Rows.Add(
                NomorUrut, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, NomorPO, COAProdukPerItem, NamaProduk, DeskripsiProduk,
                JumlahProduk, SatuanProduk, HargaSatuan, JumlahHargaPerItem,
                (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHargaPerItem, KodeProject,
                KelompokAsset, KodeDivisiAsset, COABiaya, MasaAmortisasi)
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

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha " &
                              " WHERE Nomor_Pembelian = '" & NomorPembelian_Terseleksi & "' ", KoneksiDatabaseTransaksi)
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

    Private Sub btn_Pembetulan_Click(sender As Object, e As EventArgs) Handles btn_Pembetulan.Click

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

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return

        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        JenisInvoice_Terseleksi = DataTabelUtama.Item("Jenis_Invoice", Baris_Terseleksi).Value
        JenisProduk_Terseleksi = DataTabelUtama.Item("Jenis_Produk", Baris_Terseleksi).Value
        AngkaInvoice_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_Invoice", Baris_Terseleksi).Value)
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        NomorPembelian_Terseleksi = DataTabelUtama.Item("Nomor_Pembelian", Baris_Terseleksi).Value
        NP_Terseleksi = DataTabelUtama.Item("N_P", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        TanggalPembetulan_Terseleksi = DataTabelUtama.Item("Tanggal_Pembetulan", Baris_Terseleksi).Value
        TanggalLapor_Terseleksi = DataTabelUtama.Item("Tanggal_Lapor", Baris_Terseleksi).Value
        JatuhTempo_Terseleksi = DataTabelUtama.Item("Jatuh_Tempo", Baris_Terseleksi).Value
        NomorSJBAST_Terseleksi = DataTabelUtama.Item("Nomor_SJ_BAST", Baris_Terseleksi).Value
        TanggalSJBAST_Terseleksi = DataTabelUtama.Item("Tanggal_SJ_BAST", Baris_Terseleksi).Value
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        TanggalPO_Terseleksi = DataTabelUtama.Item("Tanggal_PO", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        KodeSupplier_Terseleksi = DataTabelUtama.Item("Kode_Supplier", Baris_Terseleksi).Value
        NamaSupplier_Terseleksi = DataTabelUtama.Item("Nama_Supplier", Baris_Terseleksi).Value
        JumlahHarga_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Harga", Baris_Terseleksi).Value)
        DiskonRp_Terseleksi = AmbilAngka(DataTabelUtama.Item("Diskon_Rp", Baris_Terseleksi).Value)
        DasarPengenaanPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Dasar_Pengenaan_Pajak", Baris_Terseleksi).Value)
        NomorFakturPajak_Terseleksi = DataTabelUtama.Item("Nomor_Faktur_Pajak", Baris_Terseleksi).Value
        JenisPPN_Terseleksi = DataTabelUtama.Item("Jenis_PPN", Baris_Terseleksi).Value
        PPN_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPN_", Baris_Terseleksi).Value)
        PPhDipotong_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPh_Dipotong", Baris_Terseleksi).Value)
        TagihanKotor_Terseleksi = AmbilAngka(DataTabelUtama.Item("Tagihan_Kotor", Baris_Terseleksi).Value)
        ReturDPP_Terseleksi = AmbilAngka(DataTabelUtama.Item("Retur_DPP", Baris_Terseleksi).Value)
        ReturPPN_Terseleksi = AmbilAngka(DataTabelUtama.Item("Retur_PPN", Baris_Terseleksi).Value)
        Retur_Terseleksi = AmbilAngka(DataTabelUtama.Item("Retur_", Baris_Terseleksi).Value)
        Catatan_Terseleksi = DataTabelUtama.Item("Catatan_", Baris_Terseleksi).Value
        NomorJV_Terseleksi = DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value

        If NomorInvoice_Terseleksi = Kosongan Then
            BersihkanSeleksi()
        Else
            btn_Pratinjau.Enabled = True
            btn_Cetak.Enabled = True
            btn_LihatJurnal.Enabled = True
            btn_LihatInvoice.Enabled = True
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
            If NomorJV_Terseleksi > 0 Then
                btn_Pembetulan.Enabled = True
                'btn_Edit.Enabled = False
                'btn_Hapus.Enabled = False
            Else
                btn_Pembetulan.Enabled = False
            End If
        End If
        If NomorJV_Terseleksi = 0 Then btn_LihatJurnal.Enabled = False

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub

    Private Sub btn_Pratinjau_Click(sender As Object, e As EventArgs) Handles btn_Pratinjau.Click
        frm_Cetak.FungsiForm = FungsiForm_PRATINJAU
        IsiValueHalamanCetak()
    End Sub

    Private Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        frm_Cetak.FungsiForm = FungsiForm_CETAK
        IsiValueHalamanCetak()
    End Sub

    Sub IsiValueHalamanCetak()
        'NomorInvoicePembelian_Cetak = NomorInvoice_Terseleksi
        'TampilkanHalamanCetak_InvoicePembelian()
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class