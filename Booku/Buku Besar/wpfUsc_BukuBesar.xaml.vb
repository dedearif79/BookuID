Imports System.Windows.Controls
Imports System.Windows
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Windows.Threading
Imports bcomm

Public Class wpfUsc_BukuBesar

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Public KesesuaianJurnal As Boolean

    Public FungsiModul As String

    Dim QueryTampilan As String
    Dim QueryTampilan2 As String
    Dim NomorPembelianTerseleksi As String
    Dim NomorBPHUTerseleksi As String
    Public COA_BukuBesar As String
    Public NamaAkun_BukuBesar As String
    Dim DebetKreditCOA

    Dim KodeMataUang As String

    Dim NomorUrut
    Dim NomorJVString As String
    Dim NomorJV As Int64
    Dim JenisJurnal As String
    Dim TanggalTransaksi
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim NamaProduk
    Dim NamaAkunLawan
    Dim DK
    Dim DebetMUA As Decimal
    Dim KreditMUA As Decimal
    Dim SaldoMUA As Decimal
    Dim Kurs As Decimal
    Dim DebetIDR As Int64
    Dim KreditIDR As Int64
    Dim SaldoIDR As Int64
    Dim UraianTransaksi
    Dim Direct

    Dim NomorUrut_Terseleksi
    Dim NomorJV_String_Terseleksi As String
    Dim NomorJV_Terseleksi As Int64
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim Direct_Terseleksi

    Dim SaldoAwal As Int64
    Dim SaldoAwalMUA As Int64
    Dim SaldoAkhir As Int64
    Dim JumlahDebet As Int64
    Dim JumlahKredit As Int64

    Dim FilterData
    Dim FilterLawanTransaksi
    Dim FilterDebetKredit
    Dim FilterDirect

    Dim Pilih_KodeLawanTransaksi
    Dim Pilih_DebetKredit
    Dim Pilih_Direct

    Dim DK_COABukuBesar As String
    Dim TampilSemua As Boolean
    Dim NomorIDTerakhir As Int64

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        'Baris-baris Coding yang ada di sini jangan dijadikan acuan untuk di-copy pada Modul Baru...!!!!!!!!!!
        'Dia beda tersendiri....!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        StatusAktif = True
        Terabas()

        ProsesLoadingForm = True

        VisibilitasSaldo(False)

        LogikaTampilan()

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub

    Sub ResetForm()
        KodeMataUang = Kosongan
        txt_COA.Text = Kosongan
    End Sub

    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_LawanTransaksi()
        KontenCombo_DebetKredit()
        KontenCombo_Direct()
        EksekusiTampilanData = True
        TampilkanData()
        TampilSemua = True
    End Sub


    Public EksekusiTampilanData As Boolean
    Sub TampilkanData()

        If Not EksekusiTampilanData Then Return

        KetersediaanMenuHalaman(pnl_Halaman, False)

        COA_BukuBesar = txt_COA.Text 'Jangan Dihapus...! Dan Jangan dipindah ke Sub RefreshTampilanData()..!!! Sudah benar di sini..!!!

        'Mengisi Value Saldo Awal
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then
            KetersediaanMenuHalaman(pnl_Halaman, True)
            Return
        End If
        cmd = New OdbcCommand("SELECT * FROM tbl_COA WHERE COA = '" & COA_BukuBesar & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        'If dr.HasRows Then txt_SaldoAwal.Text = dr.Item("Saldo_Awal")
        AksesDatabase_General(Tutup)


        datatabelUtama.Rows.Clear()

        FilterData = Kosongan 'Ini jangan dihapus...! Ada kepentingan logika tampilan Saldo.

        'Filter Lawan Transaksi :
        Select Case Pilih_KodeLawanTransaksi
            Case Kosongan
                FilterLawanTransaksi = Kosongan 'Ini harus kosongan..! Jangan dirubah jadi Spasi1..!
            Case Pilihan_Semua
                FilterLawanTransaksi = Kosongan 'Ini harus kosongan..! Jangan dirubah jadi Spasi1..!
            Case Else
                FilterLawanTransaksi = " AND Kode_Lawan_Transaksi = '" & Pilih_KodeLawanTransaksi & "' "
        End Select

        Debet_IDR.Visibility = Visibility.Visible
        Kredit_IDR.Visibility = Visibility.Visible
        Saldo_IDR.Visibility = Visibility.Visible

        'Filter DebetKredit:
        Select Case Pilih_DebetKredit
            Case Kosongan
                FilterDebetKredit = Kosongan 'Ini harus kosongan..! Jangan dirubah jadi Spasi1..!
            Case Pilihan_Semua
                FilterDebetKredit = Kosongan 'Ini harus kosongan..! Jangan dirubah jadi Spasi1..!
            Case dk_Debet
                FilterDebetKredit = " AND D_K = '" & dk_D & "' "
                Kredit_IDR.Visibility = Visibility.Collapsed
            Case dk_Kredit
                FilterDebetKredit = " AND D_K = '" & dk_K & "' "
                Debet_IDR.Visibility = Visibility.Collapsed
        End Select

        'Filter Direct:
        Select Case Pilih_Direct
            Case Kosongan
                FilterDirect = Kosongan 'Ini harus kosongan..! Jangan dirubah jadi Spasi1..!
            Case Pilihan_Semua
                FilterDirect = Kosongan 'Ini harus kosongan..! Jangan dirubah jadi Spasi1..!
            Case Pilihan_Ya
                FilterDirect = " AND Direct = 1 "
            Case Pilihan_Tidak
                FilterDirect = " AND Direct = 0 "
        End Select

        FilterData = FilterLawanTransaksi & FilterDebetKredit & FilterDirect

        If FilterData = Kosongan Then
            Saldo_IDR.Visibility = Visibility.Visible
        Else
            Saldo_IDR.Visibility = Visibility.Collapsed
        End If

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM tbl_Transaksi " &
            " WHERE Valid <> '" & _X_ & "' " &
            " AND   COA             = '" & COA_BukuBesar & "' " &
            " AND   Status_Approve  = 1 " &
            FilterData

        'SaldoAwal = AmbilAngka(txt_SaldoAwal.Text)
        NomorUrut = 0
        SaldoAwal = SaldoAwalTahunCOA(COA_BukuBesar)
        SaldoAwalMUA = SaldoAwalTahunCOA_MUA(COA_BukuBesar)
        SaldoAkhir = 0
        SaldoIDR = SaldoAwal
        SaldoMUA = SaldoAwalMUA
        JumlahDebet = 0
        JumlahKredit = 0

        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabase = False Then
            KetersediaanMenuHalaman(pnl_Halaman, True)
            Return
        End If
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Tanggal_Transaksi, Nomor_JV, D_K ", KoneksiDatabaseTransaksi) 'QUERY ada di sub masing-masing Tipe Tampilan (Semua/Pencarian/Filter)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorJV = dr.Item("Nomor_JV")
            JenisJurnal = dr.Item("Jenis_Jurnal")
            NomorJVString = KonversiNomorJVAngkaKeString(NomorJV)
            TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            TanggalInvoice = dr.Item("Tanggal_Invoice")
            NomorInvoice = dr.Item("Nomor_Invoice")
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            NamaProduk = dr.Item("Nama_Produk")
            UraianTransaksi = PenghapusEnter(dr.Item("Uraian_Transaksi"))
            Kurs = dr.Item("Kurs")
            DK = dr.Item("D_K")
            DebetMUA = dr.Item("Jumlah_Debet")
            KreditMUA = dr.Item("Jumlah_Kredit")
            NamaAkunLawan = DaftarNamaAkunLawan(NomorJV, DK)
            DebetIDR = AmbilValue_NilaiMataUang_WithCOA(dr.Item("COA"), dr.Item("Kode_Mata_Uang"), Kurs, DebetMUA)
            KreditIDR = AmbilValue_NilaiMataUang_WithCOA(dr.Item("COA"), dr.Item("Kode_Mata_Uang"), Kurs, KreditMUA)
            If Kurs = 1 Then
                DebetMUA = 0
                KreditMUA = 0
            End If
            If COATermasukDEBET(COA_BukuBesar) Then
                SaldoIDR += DebetIDR - KreditIDR
                SaldoMUA += DebetMUA - KreditMUA
            Else
                SaldoIDR += KreditIDR - DebetIDR
                SaldoMUA += KreditMUA - DebetMUA
            End If
            If dr.Item("Direct") = 1 Then
                Direct = "Ya"
            Else
                Direct = Kosongan
            End If
            TambahBaris()
            JumlahDebet += DebetIDR
            JumlahKredit += KreditIDR
            Terabas()
        Loop
        AksesDatabase_Transaksi(Tutup)
        SaldoAkhir = SaldoIDR
        'txt_SaldoAwal.Text = SaldoAwal
        'If SaldoAwal = 0 Then txt_SaldoAwal.Text = StripKosong

        'txt_SaldoAkhir.Text = SaldoAkhir
        'If SaldoAkhir = 0 Then txt_SaldoAkhir.Text = StripKosong

        'If FungsiModul = Halaman_BUKUBANK Then
        '    txt_JumlahDebet.Text = JumlahKredit
        '    txt_JumlahKredit.Text = JumlahDebet
        'Else
        '    txt_JumlahDebet.Text = JumlahDebet
        '    txt_JumlahKredit.Text = JumlahKredit
        'End If

        BersihkanSeleksi()

        JumlahBaris = datatabelUtama.Rows.Count

        'lbl_JumlahBaris.Text = "Jumlah Baris : " & JumlahBaris

        txt_SaldoAwal.Text = SaldoAwal
        If FilterData = Kosongan Then
            txt_SaldoAkhir.Text = SaldoAkhir
        Else
            txt_SaldoAkhir.Text = Kosongan
        End If

    End Sub

    Sub TambahBaris()
        If DebetIDR = 0 And KreditIDR = 0 Then Return
        NomorUrut += 1
        If FungsiModul = Halaman_BUKUBANK Then
            datatabelUtama.Rows.Add(NomorUrut, TanggalTransaksi, NomorJVString, JenisJurnal, TanggalInvoice, NomorInvoice, NomorFakturPajak,
                                        KodeLawanTransaksi, NamaLawanTransaksi, NamaProduk, NamaAkunLawan, DK, DebetMUA, KreditMUA, SaldoMUA, Kurs,
                                        DebetIDR, KreditIDR, SaldoIDR, UraianTransaksi, Direct)
        Else
            datatabelUtama.Rows.Add(NomorUrut, TanggalTransaksi, NomorJVString, JenisJurnal, TanggalInvoice, NomorInvoice, NomorFakturPajak,
                                        KodeLawanTransaksi, NamaLawanTransaksi, NamaProduk, NamaAkunLawan, DK, DebetMUA, KreditMUA, SaldoMUA, Kurs,
                                        DebetIDR, KreditIDR, SaldoIDR, UraianTransaksi, Direct)
        End If
    End Sub

    Sub TambahDataBaru()
        If Not TampilSemua Then Return
    End Sub


    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_BukuPembantu.IsEnabled = False
        KetersediaanMenuHalaman(pnl_Halaman, True)
        TampilSemua = False
    End Sub


    Sub LogikaTampilan()

        Select Case FungsiModul
            Case Halaman_BUKUBESAR
                If COA_BukuBesar = Kosongan Then
                    lbl_JudulForm.Text = "Buku Besar"
                End If
                lbl_COA.Text = "Kode Akun :"
                VisibilitasComboCOA(False)
                VisibilitasFilterAkun(True)
                VisibilitasCRUD(False)
            Case Halaman_BUKUKAS
                lbl_JudulForm.Text = "Buku Kas"
                txt_COA.Text = Kosongan
                lbl_ComboCOA.Text = "Pilih Kas :"
                VisibilitasComboCOA(True)
                KontenComboKas_WPF(cmb_COA)
                VisibilitasFilterAkun(False)
                VisibilitasCRUD(True)
            Case Halaman_BUKUBANK
                lbl_JudulForm.Text = "Buku Bank"
                txt_COA.Text = Kosongan
                lbl_ComboCOA.Text = "Pilih Bank :"
                VisibilitasComboCOA(True)
                KontenComboBank_WPF(cmb_COA)
                VisibilitasFilterAkun(False)
                VisibilitasCRUD(True)
            Case Halaman_BUKUCASHADVANCE
                lbl_JudulForm.Text = "Buku Cash Advance"
                txt_COA.Text = Kosongan
                lbl_ComboCOA.Text = "Pilih Cash Advance :"
                VisibilitasComboCOA(True)
                KontenComboCashAdvance_WPF(cmb_COA)
                VisibilitasFilterAkun(False)
                VisibilitasCRUD(True)
            Case Halaman_BUKUPETTYCASH
                lbl_JudulForm.Text = "Buku Petty Cash"
                txt_COA.Text = Kosongan
                lbl_ComboCOA.Text = "Pilih Petty Cash :"
                VisibilitasComboCOA(True)
                KontenComboPettyCash_WPF(cmb_COA)
                VisibilitasFilterAkun(False)
                VisibilitasCRUD(True)
            Case Kosongan
                PesanUntukProgrammer("Fungsi Modul belum ditentukan...!!!")
        End Select

    End Sub


    Sub VisibilitasFilterAkun(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_COA.Visibility = Visibility.Visible
            txt_COA.Visibility = Visibility.Visible
            btn_PilihCOA.Visibility = Visibility.Visible
            brd_Akun.Visibility = Visibility.Visible
        Else
            lbl_COA.Visibility = Visibility.Collapsed
            txt_COA.Visibility = Visibility.Collapsed
            btn_PilihCOA.Visibility = Visibility.Collapsed
            brd_Akun.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasFilterDirect(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_Direct.Visibility = Visibility.Visible
            cmb_Direct.Visibility = Visibility.Visible
        Else
            lbl_Direct.Visibility = Visibility.Collapsed
            cmb_Direct.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasCRUD(Visibilitas As Boolean)
        If Visibilitas Then
            brd_CRUD.Visibility = Visibility.Visible
            btn_Input.Visibility = Visibility.Visible
            btn_Edit.Visibility = Visibility.Visible
            btn_Hapus.Visibility = Visibility.Visible
        Else
            brd_CRUD.Visibility = Visibility.Collapsed
            btn_Input.Visibility = Visibility.Collapsed
            btn_Edit.Visibility = Visibility.Collapsed
            btn_Hapus.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasComboCOA(Visibilitas As Boolean)
        If Visibilitas Then
            brd_ComboAkun.Visibility = Visibility.Visible
            lbl_ComboCOA.Visibility = Visibility.Visible
            cmb_COA.Visibility = Visibility.Visible
        Else
            brd_ComboAkun.Visibility = Visibility.Collapsed
            lbl_ComboCOA.Visibility = Visibility.Collapsed
            cmb_COA.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasSaldo(Visibilitas As Boolean)
        If Visibilitas Then
            grb_InfoSaldo.Visibility = Visibility.Visible
        Else
            grb_InfoSaldo.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub KontenCombo_LawanTransaksi()
        cmb_LawanTransaksi.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT Nama_Mitra FROM tbl_LawanTransaksi ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_LawanTransaksi.Items.Add(Pilihan_Semua)
        Do While dr.Read
            cmb_LawanTransaksi.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        cmb_LawanTransaksi.Items.Add(Pilihan_Semua)
        cmb_LawanTransaksi.SelectedValue = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub


    Sub KontenCombo_DebetKredit()
        cmb_DebetKredit.Items.Clear()
        cmb_DebetKredit.Items.Add(Pilihan_Semua)
        cmb_DebetKredit.Items.Add(dk_Debet)
        cmb_DebetKredit.Items.Add(dk_Kredit)
        cmb_DebetKredit.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenCombo_Direct()
        cmb_Direct.Items.Clear()
        cmb_Direct.Items.Add(Pilihan_Semua)
        cmb_Direct.Items.Add(Pilihan_Ya)
        cmb_Direct.Items.Add(Pilihan_Tidak)
        cmb_Direct.SelectedValue = Pilihan_Semua
    End Sub


    Private Sub txt_COA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COA.TextChanged
        COA_BukuBesar = txt_COA.Text
        DK_COABukuBesar = PenentuanDK_COA(COA_BukuBesar)
        If COA_BukuBesar = Kosongan Then
            KodeMataUang = Kosongan
            Select Case FungsiModul
                Case Halaman_BUKUBESAR
                    lbl_JudulForm.Text = "Buku Besar"
                Case Halaman_BUKUBANK
                    lbl_JudulForm.Text = "Buku Bank"
                Case Halaman_BUKUPETTYCASH
                    lbl_JudulForm.Text = "Buku Petty Cash"
                Case Halaman_BUKUKAS
                    lbl_JudulForm.Text = "Buku Kas"
                Case Else
                    lbl_JudulForm.Text = "Buku Besar"
            End Select
            VisibilitasSaldo(False)
            txt_SaldoAwal.Text = Kosongan
            txt_SaldoAkhir.Text = Kosongan
        Else
            NamaAkun_BukuBesar = AmbilValue_NamaAkun(COA_BukuBesar)
            Select Case FungsiModul
                Case Halaman_BUKUBESAR
                    lbl_JudulForm.Text = "Buku Besar - " & NamaAkun_BukuBesar
                Case Halaman_BUKUBANK
                    lbl_JudulForm.Text = "Buku Bank - " & NamaAkun_BukuBesar
                Case Halaman_BUKUPETTYCASH
                    lbl_JudulForm.Text = "Buku Petty Cash - " & NamaAkun_BukuBesar
                Case Halaman_BUKUKAS
                    lbl_JudulForm.Text = "Buku Kas - " & NamaAkun_BukuBesar
                Case Else
                    lbl_JudulForm.Text = "Buku Besar - " & NamaAkun_BukuBesar
            End Select
            VisibilitasSaldo(True)
            KodeMataUang = AmbilValue_KodeMataUang_BerdasarkanCOA(COA_BukuBesar)
            If KodeMataUang = KodeMataUang_IDR Then
                Debet_IDR.Header = "Debet"
                Kredit_IDR.Header = "Kredit"
                Saldo_IDR.Header = "Saldo"
                Kurs_.Visibility = Visibility.Collapsed
                Debet_MUA.Visibility = Visibility.Collapsed
                Kredit_MUA.Visibility = Visibility.Collapsed
                Saldo_MUA.Visibility = Visibility.Collapsed
            Else
                Kurs_.Visibility = Visibility.Visible
                Debet_MUA.Header = "Debet" & Enter1Baris & "(" & KodeMataUang & ")"
                Kredit_MUA.Header = "Kredit" & Enter1Baris & "(" & KodeMataUang & ")"
                Saldo_MUA.Header = "Saldo" & Enter1Baris & "(" & KodeMataUang & ")"
                Debet_IDR.Header = "Debet" & Enter1Baris & "(IDR)"
                Kredit_IDR.Header = "Kredit" & Enter1Baris & "(IDR)"
                Saldo_IDR.Header = "Saldo" & Enter1Baris & "(IDR)"
                Debet_MUA.Visibility = Visibility.Visible
                Kredit_MUA.Visibility = Visibility.Visible
                Saldo_MUA.Visibility = Visibility.Visible
            End If
            TampilkanData()
        End If
    End Sub
    Private Sub btn_PilihCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        If FungsiModul = Halaman_BUKUBESAR Then win_ListCOA.ListAkun = ListAkun_Semua
        If FungsiModul = Halaman_BUKUBANK Then win_ListCOA.ListAkun = ListAkun_Bank
        win_ListCOA.ShowDialog()
        txt_COA.Text = win_ListCOA.COATerseleksi
    End Sub


    Private Sub cmb_COA_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_COA.SelectionChanged
        txt_COA.Text = KonversiSaranaPembayaranKeCOA(cmb_COA.SelectedValue)
    End Sub


    Private Sub cmb_LawanTransaksi_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_LawanTransaksi.SelectionChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_LawanTransaksi.SelectedValue & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeLawanTransaksi = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_LawanTransaksi.SelectedValue = Pilihan_Semua Then Pilih_KodeLawanTransaksi = Pilihan_Semua
        TampilkanData()
    End Sub


    Private Sub cmb_DebetKredit_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DebetKredit.SelectionChanged
        Pilih_DebetKredit = cmb_DebetKredit.SelectedValue
        TampilkanData()
    End Sub

    Private Sub cmb_Direct_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Direct.SelectionChanged
        Pilih_Direct = cmb_Direct.SelectedValue
        TampilkanData()
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        ProsesIsiValueForm = True
        win_InputTransaksi = New wpfWin_InputTransaksi
        win_InputTransaksi.ResetForm()
        win_InputTransaksi.FungsiForm = FungsiForm_TAMBAH
        IsiValueComboBypassTerkunci(win_InputTransaksi.cmb_KodeMataUang, KodeMataUang)
        Select Case FungsiModul
            Case Halaman_BUKUBANK
                win_InputTransaksi.JalurMasuk = Halaman_BUKUBANK
                If COA_BukuBesar = Kosongan Then
                    PesanPeringatan("Silakan pilih Bank terlebih dahulu.")
                    Return
                End If
            Case Halaman_BUKUKAS
                win_InputTransaksi.JalurMasuk = Halaman_BUKUKAS
                If COA_BukuBesar = Kosongan Then
                    PesanPeringatan("Silakan pilih Kas terlebih dahulu.")
                    Return
                End If
            Case Halaman_BUKUCASHADVANCE
                win_InputTransaksi.JalurMasuk = Halaman_BUKUCASHADVANCE
                If COA_BukuBesar = Kosongan Then
                    PesanPeringatan("Silakan pilih Cash Advance terlebih dahulu.")
                    Return
                End If
            Case Halaman_BUKUPETTYCASH
                win_InputTransaksi.JalurMasuk = Halaman_BUKUPETTYCASH
                If COA_BukuBesar = Kosongan Then
                    PesanPeringatan("Silakan pilih Petty Cash terlebih dahulu.")
                    Return
                End If
            Case Halaman_BUKUHUTANGGAJI
                FiturBelumBisaDigunakan()
        End Select
        IsiValueComboBypassTerkunci(win_InputTransaksi.cmb_SaranaPembayaran, KonversiCOAKeSaranaPembayaran(COA_BukuBesar))
        win_InputTransaksi.NamaAkunUtama = NamaAkun_BukuBesar
        win_InputTransaksi.SaranaPembayaran = COA_BukuBesar & StripPemisah & NamaAkun_BukuBesar
        ProsesIsiValueForm = False
        win_InputTransaksi.ShowDialog()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        If AmbilValue_JumlahBarisJurnal(NomorJV_Terseleksi) > 2 Then
            EditJurnal(NomorJV_Terseleksi)
            Return
        End If

        Dim COA_Selain = Kosongan

        win_InputTransaksi = New wpfWin_InputTransaksi
        win_InputTransaksi.ResetForm()
        win_InputTransaksi.FungsiForm = FungsiForm_EDIT
        IsiValueComboBypassTerkunci(win_InputTransaksi.cmb_KodeMataUang, KodeMataUang)

        Select Case FungsiModul
            Case Halaman_BUKUKAS
                win_InputTransaksi.JalurMasuk = Halaman_BUKUKAS
            Case Halaman_BUKUPETTYCASH
                win_InputTransaksi.JalurMasuk = Halaman_BUKUPETTYCASH
            Case Halaman_BUKUBANK
                win_InputTransaksi.JalurMasuk = Halaman_BUKUBANK
        End Select

        COA_Selain = COA_BukuBesar

        win_InputTransaksi.COAUtama = COA_Selain
        win_InputTransaksi.NamaAkunUtama = NamaAkun_BukuBesar
        win_InputTransaksi.NomorJV = NomorJV_Terseleksi
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_Terseleksi & "' AND COA <> '" & COA_Selain & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            win_InputTransaksi.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(dr.Item("Tanggal_Transaksi"))
            If dr.Item("Jumlah_Debet") > 0 Then
                If COATermasukDEBET(win_InputTransaksi.COAUtama) Then win_InputTransaksi.cmb_AlurTransaksi.SelectedValue = AlurTransaksi_OUT
                If COATermasukKREDIT(win_InputTransaksi.COAUtama) Then win_InputTransaksi.cmb_AlurTransaksi.SelectedValue = AlurTransaksi_IN
                If KodeMataUang = KodeMataUang_IDR Then
                    win_InputTransaksi.txt_JumlahTransaksi_IDR.Text = FormatUlangInt64(dr.Item("Jumlah_Debet"))
                Else
                    win_InputTransaksi.txt_JumlahTransaksi_MUA.Text = dr.Item("Jumlah_Debet")
                End If
            End If
            If dr.Item("Jumlah_Kredit") > 0 Then
                If COATermasukDEBET(win_InputTransaksi.COAUtama) Then win_InputTransaksi.cmb_AlurTransaksi.SelectedValue = AlurTransaksi_IN
                If COATermasukKREDIT(win_InputTransaksi.COAUtama) Then win_InputTransaksi.cmb_AlurTransaksi.SelectedValue = AlurTransaksi_OUT
                If KodeMataUang = KodeMataUang_IDR Then
                    win_InputTransaksi.txt_JumlahTransaksi_IDR.Text = FormatUlangInt64(dr.Item("Jumlah_Kredit"))
                Else
                    win_InputTransaksi.txt_JumlahTransaksi_MUA.Text = dr.Item("Jumlah_Kredit")
                End If
            End If
            win_InputTransaksi.txt_NomorBukti.Text = dr.Item("Referensi")
            win_InputTransaksi.txt_KodeLawanTransaksi.Text = dr.Item("Kode_Lawan_Transaksi")
            win_InputTransaksi.txt_NamaLawanTransaksi.Text = dr.Item("Nama_Lawan_Transaksi")
            win_InputTransaksi.txt_COALawan.Text = dr.Item("COA")
            win_InputTransaksi.txt_NamaLawanAkun.Text = dr.Item("Nama_Akun")
            win_InputTransaksi.txt_NamaProduk.Text = dr.Item("Nama_Produk")
            IsiValueElemenRichTextBox(win_InputTransaksi.txt_Uraian, dr.Item("Uraian_Transaksi"))
        End If
        AksesDatabase_Transaksi(Tutup)
        win_InputTransaksi.txt_NomorBukti.Focus()
        win_InputTransaksi.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin akan menghapus data nomor " & NomorJV_String_Terseleksi & "?") Then Return

        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabaseTransaksi = False Then
            Pesan_Gagal("Data terpilih GAGAL dihapus." & Enter2Baris & teks_SilakanCobaLagi_Database)
            Return
        End If

        'Hapus DataTerpilih Pada Tabel Jurnal (tbl_Transaksi) :
        HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

        AksesDatabase_Transaksi(Tutup)
        TampilkanData()
        Pesan_Sukses("Data terpilih BERHASIL DIHAPUS")

    End Sub


    Private Sub btn_BukuPembantu_Click(sender As Object, e As RoutedEventArgs) Handles btn_BukuPembantu.Click
        BukuBesarPembantu(KodeLawanTransaksi_Terseleksi, COA_BukuBesar)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub txt_SaldoAwal_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwal.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwal)
    End Sub


    Private Sub txt_SaldoAkhir_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAkhir.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAkhir)
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
        KodeLawanTransaksi_Terseleksi = rowviewUtama("Kode_Lawan_Transaksi")
        NamaLawanTransaksi_Terseleksi = rowviewUtama("Nama_Lawan_Transaksi")
        NomorJV_String_Terseleksi = rowviewUtama("Nomor_JV")
        NomorJV_Terseleksi = KonversiNomorJVStringKeAngka(NomorJV_String_Terseleksi)
        Direct_Terseleksi = rowviewUtama("Direct_")

        If Direct_Terseleksi = Pilihan_Ya Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
        End If
        If NomorJV_Terseleksi > 0 Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If
        If KodeLawanTransaksi_Terseleksi <> Kosongan Then
            btn_BukuPembantu.IsEnabled = True
        Else
            btn_BukuPembantu.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        'Tidak ada coding.
    End Sub





    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim Jenis_Jurnal As New DataGridTextColumn
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Nama_Akun_Lawan As New DataGridTextColumn
    Dim D_K As New DataGridTextColumn
    Dim Debet_MUA As New DataGridTextColumn
    Dim Kredit_MUA As New DataGridTextColumn
    Dim Saldo_MUA As New DataGridTextColumn
    Dim Kurs_ As New DataGridTextColumn
    Dim Debet_IDR As New DataGridTextColumn
    Dim Kredit_IDR As New DataGridTextColumn
    Dim Saldo_IDR As New DataGridTextColumn
    Dim Uraian_Transaksi As New DataGridTextColumn
    Dim Direct_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Tanggal_Transaksi")
        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("Jenis_Jurnal")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Nama_Akun_Lawan")
        datatabelUtama.Columns.Add("D_K")
        datatabelUtama.Columns.Add("Debet_MUA", GetType(Decimal))
        datatabelUtama.Columns.Add("Kredit_MUA", GetType(Decimal))
        datatabelUtama.Columns.Add("Saldo_MUA", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_", GetType(Decimal))
        datatabelUtama.Columns.Add("Debet_IDR", GetType(Int64))
        datatabelUtama.Columns.Add("Kredit_IDR", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_IDR", GetType(Int64))
        datatabelUtama.Columns.Add("Uraian_Transaksi")
        datatabelUtama.Columns.Add("Direct_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No. Urut", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Transaksi, "Tanggal_Transaksi", "Tanggal Transaksi", 81, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "No. JV", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Jurnal, "Jenis_Jurnal", "Jenis Jurnal", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode Lawan Transaksi", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Lawan Transaksi", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun_Lawan, "Nama_Akun_Lawan", "Nama Akun Lawan", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, D_K, "D_K", "D/K", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Debet_MUA, "Debet_MUA", "Debet MUA", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kredit_MUA, "Kredit_MUA", "Kredit MUA", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_MUA, "Saldo_MUA", "Saldo MUA", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_, "Kurs_", "Kurs", 72, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Debet_IDR, "Debet_IDR", "Debet", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kredit_IDR, "Kredit_IDR", "Kredit", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_IDR, "Saldo_IDR", "Saldo", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_Transaksi, "Uraian_Transaksi", "Uraian", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Direct_, "Direct_", "Direct", 51, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        txt_COA.IsReadOnly = True
        cmb_COA.IsReadOnly = True
        cmb_Direct.IsReadOnly = True
        txt_SaldoAwal.IsReadOnly = True
        txt_SaldoAkhir.IsReadOnly = True
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
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class