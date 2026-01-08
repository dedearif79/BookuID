Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_POPenjualan_X

    Public JudulForm

    Public KesesuaianJurnal As Boolean
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
    Dim NomorSJBAST
    Dim TanggalSJBAST
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim KodeProject
    Dim KodeCustomer
    Dim NamaCustomer
    Dim JumlahHarga
    Dim DiskonRp
    Dim DasarPengenaanPajak
    Dim JenisPPN
    Dim PerlakuanPPN
    Dim PPN
    Dim PPhDipotong
    Dim TotalTagihan
    Dim JangkaWaktuPenyelesaian
    Dim Catatan
    Dim StatusKontrol

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Public AngkaPO_Terseleksi
    Dim NomorPO_Terseleksi
    Dim TanggalPO_Terseleksi
    Dim JenisProduk_Terseleksi
    Dim NomorSJBAST_Terseleksi
    Dim TanggalSJBAST_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim KodeProject_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim JumlahHarga_Terseleksi
    Dim DiskonRp_Terseleksi
    Dim DasarPengenaanPajak_Terseleksi
    Dim JenisPPN_Terseleksi
    Dim PerlakuanPPN_Terseleksi
    Dim PPN_Terseleksi
    Dim TotalTagihan_Terseleksi
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
    Dim Pilih_Kontrol
    Dim Pilih_JenisProduk_Induk


    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        ResetJudulForm()

        KontenCombo_JenisProduk_Induk()
        KontenCombo_Kontrol()

        'RefreshTampilanData()

        'BeginInvoke(Sub() PesanUntukProgrammer("Value Nomor dan Tanggal Invoice masih memungkinkan Double. Jadi, harus diperbaiki lagi konsepnya."))

        ProsesLoadingForm = False

    End Sub

    Sub ResetJudulForm()
        If Pilih_JenisProduk_Induk = JenisProduk_Semua Then
            JudulForm = "Buku Pengawasan PO Penjualan"
        Else
            JudulForm = "Buku Pengawasan PO Penjualan - " & JenisProduk_Menu
        End If
        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm
    End Sub

    Sub KontenCombo_JenisProduk_Induk()
        cmb_JenisProduk_Induk.Items.Clear()
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Semua)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Barang)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_Jasa)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_BarangDanJasa)
        cmb_JenisProduk_Induk.Items.Add(JenisProduk_JasaKonstruksi)
    End Sub

    Sub KontenCombo_Kontrol()
        cmb_Kontrol.Items.Clear()
        cmb_Kontrol.Items.Add(Status_All)
        cmb_Kontrol.Items.Add(Status_Open)
        cmb_Kontrol.Items.Add(Status_Closed)
        cmb_Kontrol.Text = Status_All
    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        'Filter Jenis Produk Induk :
        Dim FilterJenisProduk_Induk = " "
        If cmb_JenisProduk_Induk.Text <> JenisProduk_Semua Then FilterJenisProduk_Induk = " AND Jenis_Produk_Induk = '" & Pilih_JenisProduk_Induk & "' "

        'FilterKontrol
        Dim FilterKontrol = Kosongan
        If Pilih_Kontrol = Status_All Then FilterKontrol = " "
        If Pilih_Kontrol = Status_Open Then FilterKontrol = " AND Kontrol = '" & Status_Open & "' "
        If Pilih_Kontrol = Status_Closed Then FilterKontrol = " AND Kontrol = '" & Status_Closed & "' "

        'Filter Data :
        Dim FilterData = FilterJenisProduk_Induk & FilterKontrol

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

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
            SJBAST_Invoice()
            KodeProject = dr.Item("Kode_Project_Produk")
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
            JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan")
            DiskonRp = dr.Item("Diskon")
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            PPN = dr.Item("PPN")
            PPhDipotong = dr.Item("PPh_Dipotong")
            TotalTagihan = dr.Item("Total_Tagihan")
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
            JangkaWaktuPenyelesaian = dr.Item("Jumlah_Hari_Jangka_Waktu_Penyelesaian")
            If JangkaWaktuPenyelesaian > 0 Then
                JangkaWaktuPenyelesaian &= " hari"
            Else
                JangkaWaktuPenyelesaian = TanggalFormatTampilan(dr.Item("Tanggal_Jangka_Waktu_Penyelesaian"))
            End If
            Catatan = dr.Item("Catatan")
            StatusKontrol = dr.Item("Kontrol")
            If AngkaPO <> AngkaPO_Sebelumnya Then TambahBaris()
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
                    NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                    TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR.Item("Tanggal_SJ"))
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
                            NomorInvoice &= SlashGanda_Pemisah & Enter1Baris & NomorInvoice_Satuan
                            TanggalInvoice &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2("Tanggal_Invoice"))
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
                    NomorSJBAST &= SlashGanda_Pemisah & Enter1Baris & NomorSJBAST_Satuan
                    TanggalSJBAST &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR.Item("Tanggal_BAST"))
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
                            NomorInvoice &= SlashGanda_Pemisah & Enter1Baris & NomorInvoice_Satuan
                            TanggalInvoice &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2("Tanggal_Invoice"))
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
        DataTabelUtama.Rows.Add(NomorUrut, AngkaPO, KodeCustomer, NamaCustomer, NomorPO, TanggalPO, JenisProduk, KodeProject,
                                JumlahHarga, DiskonRp, DasarPengenaanPajak,
                                JenisPPN, PerlakuanPPN, PPN, PPhDipotong, TotalTagihan, TermOfPayment,
                                NomorSJBAST, TanggalSJBAST, NomorInvoice, TanggalInvoice, JangkaWaktuPenyelesaian, Catatan, StatusKontrol)
    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_JenisProduk_Induk_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk_Induk.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisProduk_Induk_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisProduk_Induk.TextChanged
        Pilih_JenisProduk_Induk = cmb_JenisProduk_Induk.Text
        JenisProduk_Menu = cmb_JenisProduk_Induk.Text
        ResetJudulForm()
        TampilkanData()
    End Sub

    Private Sub cmb_Kontrol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Kontrol.SelectedIndexChanged
    End Sub
    Private Sub cmb_Kontrol_TextChanged(sender As Object, e As EventArgs) Handles cmb_Kontrol.TextChanged
        Pilih_Kontrol = cmb_Kontrol.Text
        If ProsesLoadingForm = False And ProsesResetForm = False Then TampilkanData()
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        If JenisProduk_Menu = JenisProduk_Semua Then
            MsgBox("Silakan tentukan 'Jenis Produk' terlebih dahulu.")
            cmb_JenisProduk_Induk.Focus()
            Return
        End If

        win_InputPOPenjualan = New wpfWin_InputPOPenjualan
        win_InputPOPenjualan.ResetForm()
        win_InputPOPenjualan.FungsiForm = FungsiForm_TAMBAH
        win_InputPOPenjualan.JenisProduk_Induk = JenisProduk_Menu
        win_InputPOPenjualan.ShowDialog()

        'frm_Input_POPenjualan.ResetForm()
        'frm_Input_POPenjualan.FungsiForm = FungsiForm_TAMBAH
        'frm_Input_POPenjualan.JenisProduk_Induk = JenisProduk_Menu
        'frm_Input_POPenjualan.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        Dim BisaDiedit As Boolean

        If NomorSJBAST_Terseleksi <> Nothing Or NomorInvoice_Terseleksi <> Nothing Then
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
        If Microsoft.VisualBasic.Right(JangkaWaktuPenyelesaian_Terseleksi, 2) = "ri" Then
            win_InputPOPenjualan.txt_JumlahHariJangkaWaktuPenyelesaian.Text = AmbilAngka(JangkaWaktuPenyelesaian_Terseleksi)
            win_InputPOPenjualan.dtp_TanggalJangkaWaktuPenyelesaian.Text = Kosongan
            win_InputPOPenjualan.rdb_JumlahHariJangkaWaktuPenyelesaian.IsChecked = True
        Else
            win_InputPOPenjualan.txt_JumlahHariJangkaWaktuPenyelesaian.Text = Kosongan
            win_InputPOPenjualan.dtp_TanggalJangkaWaktuPenyelesaian.SelectedDate = TanggalFormatWPF(JangkaWaktuPenyelesaian_Terseleksi)
            win_InputPOPenjualan.rdb_TanggalJangkaWaktuPenyelesaian.IsChecked = True
        End If
        win_InputPOPenjualan.cmb_Kontrol.SelectedValue = Kontrol_Terseleksi
        IsiTabelSJBAST()
        IsiTabelInvoice()
        ProsesIsiValueForm = False
        win_InputPOPenjualan.ShowDialog()

    End Sub

    Sub IsiTabelProduk() 'Ini sistem lama. Nanti hapus saja.
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

    Sub IsiTabelSJBAST()
        NomorSJBAST_Bundelan = NomorSJBAST_Terseleksi
        Dim NomorSJBAST = Kosongan
        Dim TanggalSJBAST = Kosongan
        If NomorSJBAST_Bundelan <> Kosongan Then
            My.Computer.FileSystem.WriteAllText(FolderRootApp & NamaFileDataSJBAST, NomorSJBAST_Bundelan, False)
        Else
            Return
        End If
        AksesDatabase_Transaksi(Tutup)
        DataSJBAST = IO.File.ReadLines(FolderRootApp & NamaFileDataSJBAST)
        For Each KontenPerBaris In DataSJBAST
            TanggalSJBAST = Kosongan
            NomorSJBAST = Microsoft.VisualBasic.Replace(KontenPerBaris, SlashGanda_Pemisah, "")
            AksesDatabase_Transaksi(Buka)
            Dim Tabel
            Dim KolomNomor
            If Microsoft.VisualBasic.Left(NomorSJBAST, 2) = "SJ" Then
                Tabel = "tbl_Penjualan_SJ"
                KolomNomor = "Nomor_SJ"
            Else
                Tabel = "tbl_Penjualan_BAST"
                KolomNomor = "Nomor_BAST"
            End If
            cmd = New OdbcCommand(" Select * FROM " & Tabel &
                                  " WHERE " & KolomNomor & " = '" & NomorSJBAST & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                TanggalSJBAST = TanggalFormatTampilan(dr.Item("Tanggal_Diterima"))
            End If
            AksesDatabase_Transaksi(Tutup)
            frm_Input_POPenjualan.dgv_SJBAST.Rows.Add(NomorSJBAST, TanggalSJBAST)
        Next
        HapusFile(FolderRootApp & NamaFileDataSJBAST)
        frm_Input_POPenjualan.BersihkanSeleksi_TabelSJBAST()
    End Sub

    Sub IsiTabelInvoice()
        NomorInvoice_Bundelan = NomorInvoice_Terseleksi
        Dim NomorInvoice = Kosongan
        Dim TanggalInvoice = Kosongan
        If NomorInvoice_Bundelan <> Kosongan Then
            My.Computer.FileSystem.WriteAllText(FolderRootApp & NamaFileDataInvoice, NomorInvoice_Bundelan, False)
        Else
            Return
        End If
        AksesDatabase_Transaksi(Tutup)
        DataInvoice = IO.File.ReadLines(FolderRootApp & NamaFileDataInvoice)
        For Each KontenPerBaris In DataInvoice
            NomorInvoice = Microsoft.VisualBasic.Replace(KontenPerBaris, SlashGanda_Pemisah, "")
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                  " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            End If
            AksesDatabase_Transaksi(Tutup)
            frm_Input_POPenjualan.dgv_Invoice.Rows.Add(NomorInvoice, TanggalInvoice)
        Next
        HapusFile(FolderRootApp & NamaFileDataInvoice)
        frm_Input_POPenjualan.BersihkanSeleksi_TabelInvoice()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        If NomorSJBAST_Terseleksi <> Nothing Or NomorInvoice_Terseleksi <> Nothing Then
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

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return

        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        AngkaPO_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_PO", Baris_Terseleksi).Value)
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        TanggalPO_Terseleksi = DataTabelUtama.Item("Tanggal_PO", Baris_Terseleksi).Value
        JenisProduk_Terseleksi = DataTabelUtama.Item("Jenis_Produk", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        KodeCustomer_Terseleksi = DataTabelUtama.Item("Kode_Customer", Baris_Terseleksi).Value
        NamaCustomer_Terseleksi = DataTabelUtama.Item("Nama_Customer", Baris_Terseleksi).Value
        JumlahHarga_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Harga", Baris_Terseleksi).Value)
        DiskonRp_Terseleksi = AmbilAngka(DataTabelUtama.Item("Diskon_Rp", Baris_Terseleksi).Value)
        DasarPengenaanPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Dasar_Pengenaan_Pajak", Baris_Terseleksi).Value)
        JenisPPN_Terseleksi = DataTabelUtama.Item("Jenis_PPN", Baris_Terseleksi).Value
        PerlakuanPPN_Terseleksi = DataTabelUtama.Item("Perlakuan_PPN", Baris_Terseleksi).Value
        PPN_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPN_", Baris_Terseleksi).Value)
        TotalTagihan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Total_Tagihan", Baris_Terseleksi).Value)
        NomorSJBAST_Terseleksi = DataTabelUtama.Item("Nomor_SJ_BAST", Baris_Terseleksi).Value
        TanggalSJBAST_Terseleksi = DataTabelUtama.Item("Tanggal_SJ_BAST", Baris_Terseleksi).Value
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        Catatan_Terseleksi = DataTabelUtama.Item("Catatan_", Baris_Terseleksi).Value
        JangkaWaktuPenyelesaian_Terseleksi = DataTabelUtama.Item("Jangka_Waktu_Penyelesaian", Baris_Terseleksi).Value
        Kontrol_Terseleksi = DataTabelUtama.Item("Kontrol_", Baris_Terseleksi).Value

        If AngkaPO_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class