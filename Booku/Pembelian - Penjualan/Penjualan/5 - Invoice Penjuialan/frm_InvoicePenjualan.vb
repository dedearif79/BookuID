Imports bcomm
Imports System.Data.Odbc

Public Class frm_InvoicePenjualan

    Public KesesuaianJurnal As Boolean
    Public JenisProduk_Menu
    Public JudulForm
    Public InvoiceDenganPO As Boolean

    Public MetodePembayaran

    'Variabel Tabel :
    Dim Index_BarisTabel
    Dim NomorUrut
    Dim JenisInvoice
    Dim JenisProduk
    Dim AngkaInvoice
    Dim NomorInvoice_Sebelumnya
    Dim NomorInvoice
    Dim NomorPenjualan
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
    Dim KodeCustomer
    Dim NamaCustomer
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
    Dim InvoicePenjualanAsset
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

        If InvoiceDenganPO = True Then JudulForm = "Invoice Penjualan"
        If InvoiceDenganPO = False Then JudulForm = "Invoice Penjualan - " & JenisProduk_Menu

        If PerusahaanSebagaiPKP = True Then
            DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = True
        Else
            DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = False
        End If

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        EksekusiKode = False
        KontenCombo_JenisTampilan()
        KontenCombo_Customer()
        EksekusiKode = True
        TampilkanData()
    End Sub

    Sub KontenCombo_JenisTampilan()
        cmb_JenisTampilan.Items.Clear()
        cmb_JenisTampilan.Items.Add(JenisTampilan_Semua)
        cmb_JenisTampilan.Items.Add(JenisTampilan_HasilAkhir)
        cmb_JenisTampilan.Text = JenisTampilan_Semua
    End Sub

    Sub KontenCombo_Customer()
        cmb_Customer.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Customer = 1 ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_Customer.Items.Add(Pilihan_Semua)
        Do While dr.Read
            Dim NamaCustomer = dr.Item("Nama_Mitra")
            cmb_Customer.Items.Add(NamaCustomer)
        Loop
        cmb_Customer.Text = Pilihan_Semua
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
            If InvoiceDenganPO = True Then FilterInvoiceDenganPO = " AND Nomor_SJ_BAST_Produk <> '' "
            If InvoiceDenganPO = False Then FilterInvoiceDenganPO = " AND Nomor_SJ_BAST_Produk = '' "
        End If

        'Filter Jenis Produk Induk :
        Dim FilterJenisProdukInduk = " "
        If JenisProduk_Menu <> JenisProduk_Semua Then FilterJenisProdukInduk = " AND Jenis_Produk_Induk = '" & JenisProduk_Menu & "' "

        'Filter Customer :
        Dim FilterCustomer = " "
        If cmb_Customer.Text <> Pilihan_Semua Then FilterCustomer = " AND Kode_Customer = '" & Pilih_KodeCustomer & "' "

        'Filter Metode Pembayaran :
        Dim FilterMetodePembayaran = " "
        If MetodePembayaran <> Pilihan_Semua Then FilterMetodePembayaran = "  AND Metode_Pembayaran = '" & MetodePembayaran & "' "

        'Filter Data :
        Dim FilterData = FilterInvoiceDenganPO & FilterJenisProdukInduk & FilterCustomer & FilterMetodePembayaran

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

        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice <> 'X' " & FilterData &
                              " ORDER BY Angka_Invoice ", KoneksiDatabaseTransaksi)
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
            NomorPenjualan = dr.Item("Nomor_Penjualan")
            NP = dr.Item("N_P")
            Dim NomorInvoice_Pembetulan = Kosongan
            Dim NP_Pembetulan = Kosongan
            If NP = "N" Then
                NomorInvoice_Pembetulan = NomorInvoice & "-P1"
            Else
                Dim PembetulanKe = AmbilAngka(NP)
                NP_Pembetulan = "P" & (PembetulanKe + 1)
                NomorInvoice_Pembetulan = Microsoft.VisualBasic.Replace(NomorInvoice, NP, NP_Pembetulan)
            End If
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
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
            If InvoiceDenganPO = False Then KodeProject = dr.Item("Kode_Project_Produk")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
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
            InvoicePenjualanAsset = dr.Item("Asset")
            Catatan = dr.Item("Catatan")
            NomorJV = dr.Item("Nomor_JV")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then TambahBaris()
            NomorInvoice_Sebelumnya = NomorInvoice
        Loop

        AksesDatabase_Transaksi(Tutup)

        If JenisTampilan = JenisTampilan_HasilAkhir Then
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                                Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, KodeCustomer, Kosongan,
                                Total_JumlahHarga, Total_DiskonRp, Total_DasarPengenaanPajak, Kosongan, Kosongan, Total_PPN, Total_PPhDipotong,
                                Total_TagihanKotor, Kosongan, Kosongan, Total_Retur, Kosongan)
        End If

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, JenisInvoice, JenisProduk, AngkaInvoice, NomorInvoice, NomorPenjualan, NP, TanggalInvoice, TanggalPembetulan, Tanggallapor, JatuhTempo,
                                NomorSJBAST, TanggalSJBAST, NomorPO, TanggalPO, KodeProject, KodeCustomer, NamaCustomer,
                                JumlahHarga, DiskonRp, DasarPengenaanPajak, NomorFakturPajak, JenisPPN, PPN, PPhDipotong,
                                TagihanKotor, ReturDPP, ReturPPN, Retur, InvoicePenjualanAsset, Catatan, NomorJV)
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

    Private Sub cmb_Customer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Customer.SelectedIndexChanged
    End Sub
    Private Sub cmb_Customer_TextChanged(sender As Object, e As EventArgs) Handles cmb_Customer.TextChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Customer.Text & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeCustomer = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Customer.Text = Pilihan_Semua Then Pilih_KodeCustomer = Pilihan_Semua
        TampilkanData()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi = 0 Then
            MsgBox("Invoice ini belum didorong ke Jurnal.")
            Return
        End If
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_LihatInvoice_Click(sender As Object, e As EventArgs) Handles btn_LihatInvoice.Click
        frm_Input_InvoicePenjualan.ResetForm()
        frm_Input_InvoicePenjualan.FungsiForm = FungsiForm_LIHAT
        IsiValueForm_InvoicePenjualan()
        frm_Input_InvoicePenjualan.ShowDialog()
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        FormInvoiceBaru_WPF()
        'FormInvoiceLama_WinForm()
    End Sub

    Sub FormInvoiceBaru_WPF()
        win_InputInvoicePenjualan = New wpfWin_InputInvoicePenjualan
        win_InputInvoicePenjualan.ResetForm()
        win_InputInvoicePenjualan.FungsiForm = FungsiForm_TAMBAH
        win_InputInvoicePenjualan.JenisProduk_Induk = JenisProduk_Menu
        win_InputInvoicePenjualan.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePenjualan.MetodePembayaran = MetodePembayaran
        win_InputInvoicePenjualan.ShowDialog()
    End Sub
    Sub FormInvoiceLama_WinForm()
        frm_Input_InvoicePenjualan.ResetForm()
        frm_Input_InvoicePenjualan.FungsiForm = FungsiForm_TAMBAH
        frm_Input_InvoicePenjualan.JenisProduk_Induk = JenisProduk_Menu
        frm_Input_InvoicePenjualan.InvoiceDenganPO = InvoiceDenganPO
        frm_Input_InvoicePenjualan.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        If InvoicePenjualanAsset_Terseleksi = 1 Then
            MsgBox("Sementara ini, sistem belum menyediakan fitur Edit untuk Invoice Penjualan Asset.")
            Return
        End If

        Dim BisaDiedit As Boolean

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_PencairanPiutangUsaha " &
                              " WHERE Nomor_Penjualan = '" & NomorPenjualan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            MsgBox("Invoice ini tidak dapat diedit karena sudah ada data pencairan..!" & Enter2Baris &
                   "Jika ingin mengeditnya, silakan hapus terlebih dahulu data pencairan terkait Invoice ini.")
            BisaDiedit = False
        Else
            BisaDiedit = True
        End If
        AksesDatabase_Transaksi(Tutup)

        win_InputInvoicePenjualan = New wpfWin_InputInvoicePenjualan
        win_InputInvoicePenjualan.ResetForm()
        win_InputInvoicePenjualan.FungsiForm = FungsiForm_EDIT
        If BisaDiedit = False Then win_InputInvoicePenjualan.FungsiForm = FungsiForm_LIHAT
        win_InputInvoicePenjualan.InvoiceDenganPO = InvoiceDenganPO
        win_InputInvoicePenjualan.MetodePembayaran = MetodePembayaran
        IsiValueForm_InvoicePenjualan()
        win_InputInvoicePenjualan.ShowDialog()

    End Sub

    Sub IsiValueForm_InvoicePenjualan()
        ProsesIsiValueForm = True
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
        ProsesIsiValueForm = False
    End Sub

    Sub IsiTabelProduk_InvoicePenjualan()
        Dim TanggalDiterimaSJBAST
        Dim JenisProdukPerItem
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
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut += 1
            JenisProdukPerItem = dr.Item("Jenis_Produk_Per_Item")
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            TanggalSJBAST = dr.Item("Tanggal_SJ_BAST_Produk")
            TanggalDiterimaSJBAST = dr.Item("Tanggal_Diterima_SJ_BAST_Produk")
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
            KodeProject = dr.Item("Kode_Project_Produk")
            frm_Input_InvoicePenjualan.DataTabelUtama.Rows.Add(
                NomorUrut, JenisProdukPerItem, NomorSJBAST, TanggalSJBAST, TanggalDiterimaSJBAST, NomorPO, NamaProduk, DeskripsiProduk,
                JumlahProduk, SatuanProduk, HargaSatuan, JumlahHargaPerItem,
                (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHargaPerItem, KodeProject)
        Loop
        AksesDatabase_Transaksi(Tutup)
    End Sub

    Sub IsiTabelSJBAST()

        NomorSJBAST_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                              " WHERE Nomor_Invoice = '" & NomorInvoice_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorSJBAST = dr.Item("Nomor_SJ_BAST_Produk")
            frm_Input_InvoicePenjualan.JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            frm_Input_InvoicePenjualan.cmb_JenisPPN.Text = dr.Item("Jenis_PPN")
            frm_Input_InvoicePenjualan.cmb_PerlakuanPPN.Text = dr.Item("Perlakuan_PPN")
            Dim Tabel
            Dim KolomNomor
            Dim KolomTanggal
            If Microsoft.VisualBasic.Left(NomorSJBAST, 2) = "SJ" Then
                Tabel = "tbl_Penjualan_SJ"
                KolomNomor = "Nomor_SJ"
                KolomTanggal = "Tanggal_SJ"
            Else
                Tabel = "tbl_Penjualan_BAST"
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
                frm_Input_InvoicePenjualan.dgv_SJBAST.Rows.Add(NomorSJBAST, TanggalSJBAST, TanggalDiterima, NomorPO)
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST
        Loop
        AksesDatabase_Transaksi(Tutup)

        BeginInvoke(Sub() frm_Input_InvoicePenjualan.BersihkanSeleksi_TabelSJBAST())

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_PencairanPiutangUsaha " &
                              " WHERE Nomor_Penjualan = '" & NomorPenjualan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            MsgBox("Invoice ini tidak dapat dihapus karena sudah ada data pencairan..!" & Enter2Baris &
                   "Jika ingin menghapusnya, silakan hapus terlebih dahulu data pencairan terkait Invoice ini.")
            Return
        End If
        AksesDatabase_Transaksi(Tutup)

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

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
            frm_BukuPenjualan.TampilkanData()
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
            NomorInvoice_Pembetulan = GantiTeks(NomorInvoice_Terseleksi, NP_Terseleksi, NP_Pembetulan)
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
            MsgBox("Invoice ini sudah pernah dibetulkan." & Enter2Baris & "Silakan pilih pembetulan yang terakhir terkait invoice ini.")
            Return
        End If

        'Isi Variabel :
        frm_Input_InvoicePenjualan.ResetForm()
        frm_Input_InvoicePenjualan.FungsiForm = FungsiForm_PEMBETULAN
        IsiValueForm_InvoicePenjualan()

        'Reset Variabel-bariabel Tertentu :
        frm_Input_InvoicePenjualan.NomorJV = 0
        frm_Input_InvoicePenjualan.ReturDPP = 0
        frm_Input_InvoicePenjualan.ReturPPN = 0
        EksekusiKode = False
        frm_Input_InvoicePenjualan.dtp_TanggalInvoice.Value = Today
        frm_Input_InvoicePenjualan.TanggalInvoice = TanggalInvoice_Terseleksi
        EksekusiKode = True

        frm_Input_InvoicePenjualan.ShowDialog()

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
        NomorPenjualan_Terseleksi = DataTabelUtama.Item("Nomor_Penjualan", Baris_Terseleksi).Value
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
        KodeCustomer_Terseleksi = DataTabelUtama.Item("Kode_Customer", Baris_Terseleksi).Value
        NamaCustomer_Terseleksi = DataTabelUtama.Item("Nama_Customer", Baris_Terseleksi).Value
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
        InvoicePenjualanAsset_Terseleksi = AmbilAngka(DataTabelUtama.Item("Asset_", Baris_Terseleksi).Value)
        NomorJV_Terseleksi = DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value

        If AngkaInvoice_Terseleksi = 0 Then
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
                btn_Edit.Enabled = False
                btn_Hapus.Enabled = False
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
        Cetak(JenisFormCetak_Invoice, NomorInvoice_Terseleksi, True, False)
        'frm_Cetak.FungsiForm = FungsiForm_PRATINJAU
        'IsiValueHalamanCetak()
    End Sub

    Private Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        Cetak(JenisFormCetak_Invoice, NomorInvoice_Terseleksi, True, False)
        'frm_Cetak.FungsiForm = FungsiForm_CETAK
        'IsiValueHalamanCetak()
    End Sub

    Sub IsiValueHalamanCetak()
        NomorInvoicePenjualan_Cetak = NomorInvoice_Terseleksi
        TampilkanHalamanCetak_InvoicePenjualan()
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class