Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_JurnalAdjusment_Forex

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Dim COA
    Dim NamaAkun

    Dim KodeAkun_Terseleksi As String
    Dim NamaAkun_Terseleksi As String
    Dim BulanAngka_Terseleksi As Integer
    Dim KodeMataUang_Terseleksi As String
    Dim JenisAdjusment_Terseleksi As String

    Dim lbl_TombolJanuari As String
    Dim lbl_TombolFebruari As String
    Dim lbl_TombolMaret As String
    Dim lbl_TombolApril As String
    Dim lbl_TombolMei As String
    Dim lbl_TombolJuni As String
    Dim lbl_TombolJuli As String
    Dim lbl_TombolAgustus As String
    Dim lbl_TombolSeptember As String
    Dim lbl_TombolOktober As String
    Dim lbl_TombolNopember As String
    Dim lbl_TombolDesember As String


    Public AdjusmentBulanBukuAktifSudahLengkap As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_Adjusment_Forex.JudulForm
        pnl_FilterData.Visibility = Visibility.Collapsed
        pnl_TombolForm.Visibility = Visibility.Collapsed

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Dim Kolom As String = "Tanggal_Transaksi"
    Dim TabelDanKriteria As String
    Dim BulanTertuaAngka As Integer
    Sub TampilkanData()

        AdjusmentBulanBukuAktifSudahLengkap = True

        KetersediaanMenuHalaman(pnl_Halaman, False)

        UpdateInfoBulanBukuAktif()

        lbl_TombolJanuari = teks_Posting
        lbl_TombolFebruari = teks_Posting
        lbl_TombolMaret = teks_Posting
        lbl_TombolApril = teks_Posting
        lbl_TombolMei = teks_Posting
        lbl_TombolJuni = teks_Posting
        lbl_TombolJuli = teks_Posting
        lbl_TombolAgustus = teks_Posting
        lbl_TombolSeptember = teks_Posting
        lbl_TombolOktober = teks_Posting
        lbl_TombolNopember = teks_Posting
        lbl_TombolDesember = teks_Posting

        'Data Tabel :
        datatabelUtama.Clear()

        ResetTombol()

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT COA, Nama_Akun FROM tbl_COA " &
                              " WHERE   Visibilitas     = '" & Pilihan_Ya & "' " &
                              " AND     Kode_Mata_Uang <> '" & KodeMataUang_IDR & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            COA = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            TambahBaris()
        Loop

        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

    End Sub


    Sub TambahBaris()
        TabelDanKriteria = " tbl_Transaksi WHERE COA = '" & COA & "' " & " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' "
        BulanTertuaAngka = AmbilValue_BulanTertuaAngka(TabelDanKriteria, Kolom)
        If BulanTertuaAngka > 12 Then BulanTertuaAngka = 12
        AlgoritmaTombol(COA)
        datatabelUtama.Rows.Add(COA, NamaAkun,
                                    lbl_TombolJanuari, lbl_TombolFebruari, lbl_TombolMaret, lbl_TombolApril, lbl_TombolMei, lbl_TombolJuni,
                                    lbl_TombolJuli, lbl_TombolAgustus, lbl_TombolSeptember, lbl_TombolOktober, lbl_TombolNopember, lbl_TombolDesember)
        Terabas()
        Dim lbl_Tombol As String = Kosongan
        Select Case BulanTerakhirDitutup + 1
            Case 1
                lbl_Tombol = lbl_TombolJanuari
            Case 2
                lbl_Tombol = lbl_TombolFebruari
            Case 3
                lbl_Tombol = lbl_TombolMaret
            Case 4
                lbl_Tombol = lbl_TombolApril
            Case 5
                lbl_Tombol = lbl_TombolMei
            Case 6
                lbl_Tombol = lbl_TombolJuni
            Case 7
                lbl_Tombol = lbl_TombolJuli
            Case 8
                lbl_Tombol = lbl_TombolAgustus
            Case 9
                lbl_Tombol = lbl_TombolSeptember
            Case 10
                lbl_Tombol = lbl_TombolOktober
            Case 11
                lbl_Tombol = lbl_TombolNopember
            Case 12
                lbl_Tombol = lbl_TombolDesember
        End Select
        If lbl_Tombol = teks_Posting Then AdjusmentBulanBukuAktifSudahLengkap = False
    End Sub


    Sub AlgoritmaTombol(COA As String)
        ResetTombol()
        Dim KodeMataUang As String = AmbilValue_KodeMataUang_BerdasarkanCOA(COA)
        Dim BulanAngka As Integer = 0
        Dim TanggalAkhirBulan As Date
        Dim AdaJurnal As Boolean
        Dim TanggalTerakhirTransaksi As Date
        Do While BulanAngka < 12
            BulanAngka += 1
            TanggalAkhirBulan = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka, TahunBukuAktif)
            AdaJurnal = CekKeberadaanJurnal_DiTanggalTertentu(COA, JenisJurnal_AdjusmentForex, TanggalAkhirBulan)
            TanggalTerakhirTransaksi = TanggalTerakhirTransaksiCOA(COA, TanggalAkhirTahunBukuAktif)
            Select Case BulanAngka
                Case 1
                    If AdaJurnal Then
                        lbl_TombolJanuari = teks_Lihat
                        lbl_TombolFebruari = teks_Posting
                    Else
                        lbl_TombolJanuari = teks_Posting
                    End If
                Case 2
                    If AdaJurnal Then
                        lbl_TombolFebruari = teks_Lihat
                        lbl_TombolMaret = teks_Posting
                    End If
                Case 3
                    If AdaJurnal Then
                        lbl_TombolMaret = teks_Lihat
                        lbl_TombolApril = teks_Posting
                    End If
                Case 4
                    If AdaJurnal Then
                        lbl_TombolApril = teks_Lihat
                        lbl_TombolMei = teks_Posting
                    End If
                Case 5
                    If AdaJurnal Then
                        lbl_TombolMei = teks_Lihat
                        lbl_TombolJuni = teks_Posting
                    End If
                Case 6
                    If AdaJurnal Then
                        lbl_TombolJuni = teks_Lihat
                        lbl_TombolJuli = teks_Posting
                    End If
                Case 7
                    If AdaJurnal Then
                        lbl_TombolJuli = teks_Lihat
                        lbl_TombolAgustus = teks_Posting
                    End If
                Case 8
                    If AdaJurnal Then
                        lbl_TombolAgustus = teks_Lihat
                        lbl_TombolSeptember = teks_Posting
                    End If
                Case 9
                    If AdaJurnal Then
                        lbl_TombolSeptember = teks_Lihat
                        lbl_TombolOktober = teks_Posting
                    End If
                Case 10
                    If AdaJurnal Then
                        lbl_TombolOktober = teks_Lihat
                        lbl_TombolNopember = teks_Posting
                    End If
                Case 11
                    If AdaJurnal Then
                        lbl_TombolNopember = teks_Lihat
                        lbl_TombolDesember = teks_Posting
                    End If
                Case 12
                    If AdaJurnal Then
                        lbl_TombolDesember = teks_Lihat
                    End If
            End Select
        Loop
        AlgoritmaTombolBerdasarkanBulanTerakhirDitutup()
        AlgoritmaTombolUntukTahunBukuBerjalan()
    End Sub

    Sub ResetTombol()
        lbl_TombolJanuari = Kosongan
        lbl_TombolFebruari = Kosongan
        lbl_TombolMaret = Kosongan
        lbl_TombolApril = Kosongan
        lbl_TombolMei = Kosongan
        lbl_TombolJuni = Kosongan
        lbl_TombolJuli = Kosongan
        lbl_TombolAgustus = Kosongan
        lbl_TombolSeptember = Kosongan
        lbl_TombolOktober = Kosongan
        lbl_TombolNopember = Kosongan
        lbl_TombolDesember = Kosongan
    End Sub

    Sub AlgoritmaTombolBerdasarkanBulanTerakhirDitutup()
        If BulanTerakhirDitutup = 0 Then lbl_TombolFebruari = Kosongan
        If BulanTerakhirDitutup <= 1 Then lbl_TombolMaret = Kosongan
        If BulanTerakhirDitutup <= 2 Then lbl_TombolApril = Kosongan
        If BulanTerakhirDitutup <= 3 Then lbl_TombolMei = Kosongan
        If BulanTerakhirDitutup <= 4 Then lbl_TombolJuni = Kosongan
        If BulanTerakhirDitutup <= 5 Then lbl_TombolJuli = Kosongan
        If BulanTerakhirDitutup <= 6 Then lbl_TombolAgustus = Kosongan
        If BulanTerakhirDitutup <= 7 Then lbl_TombolSeptember = Kosongan
        If BulanTerakhirDitutup <= 8 Then lbl_TombolOktober = Kosongan
        If BulanTerakhirDitutup <= 9 Then lbl_TombolNopember = Kosongan
        If BulanTerakhirDitutup <= 10 Then lbl_TombolDesember = Kosongan
    End Sub
    Sub AlgoritmaTombolUntukTahunBukuBerjalan()
        If TahunBukuAktif = TahunIni Then
            If BulanIni = 1 Then lbl_TombolJanuari = Kosongan
            If BulanIni <= 2 Then lbl_TombolFebruari = Kosongan
            If BulanIni <= 3 Then lbl_TombolMaret = Kosongan
            If BulanIni <= 4 Then lbl_TombolApril = Kosongan
            If BulanIni <= 5 Then lbl_TombolMei = Kosongan
            If BulanIni <= 6 Then lbl_TombolJuni = Kosongan
            If BulanIni <= 7 Then lbl_TombolJuli = Kosongan
            If BulanIni <= 8 Then lbl_TombolAgustus = Kosongan
            If BulanIni <= 9 Then lbl_TombolSeptember = Kosongan
            If BulanIni <= 10 Then lbl_TombolOktober = Kosongan
            If BulanIni <= 11 Then lbl_TombolNopember = Kosongan
            If BulanIni <= 12 Then lbl_TombolDesember = Kosongan
        End If
    End Sub


    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

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

        KodeAkun_Terseleksi = rowviewUtama("Kode_Akun")
        NamaAkun_Terseleksi = rowviewUtama("Nama_Akun")
        KodeMataUang_Terseleksi = AmbilValue_KodeMataUang_BerdasarkanCOA(KodeAkun_Terseleksi)

        Dim PenyesuaianIndexBulan As Integer = 1 ' Ini harus disesuaikan dengan jumlah kolom pada tabel.

        BulanAngka_Terseleksi = KolomTerseleksi.DisplayIndex - PenyesuaianIndexBulan

    End Sub

    Private Sub btn_TombolTabel_Click(sender As Object, e As RoutedEventArgs)

        Dim btn = DirectCast(sender, Button)
        Dim rowView = TryCast(btn.DataContext, DataRowView)
        Dim BulanAngka As Integer = CInt(btn.Tag)
        If rowView Is Nothing Then Return
        Dim COA As String = rowView("Kode_Akun").ToString()
        Dim NamaAkun As String = rowView("Nama_Akun").ToString()
        Dim lbl_Tombol As String = btn.Content

        KetersediaanMenuHalaman(pnl_Halaman, False)
        If lbl_Tombol = teks_Posting Then
            InputJurnalAdjusment(COA, BulanAngka)
        Else
            LihatJurnalAdjusment(COA, BulanAngka)
        End If
        KetersediaanMenuHalaman(pnl_Halaman, True)

    End Sub


    Sub InputJurnalAdjusment(COA As String, BulanAngka As Integer)

        Dim KodeMataUang As String = AmbilValue_KodeMataUang_BerdasarkanCOA(COA)
        Dim TanggalTerakhirTransaksi As Date = TanggalTerakhirTransaksiCOA(COA, TanggalAkhirTahunBukuAktif)
        Dim BulanTerakhirTransaksi As Integer = TanggalTerakhirTransaksi.Month

        Dim Pesan As String = "Anda akan melakukan Adjusment untuk" & Enter1Baris &
            "Akun : " & COA & " - " & NamaAkun & Enter1Baris &
            "Bulan : " & KonversiAngkaKeBulanString(BulanAngka) & " " & TahunBukuAktif & Enter2Baris &
            "Lanjutkan proses?" & Enter1Baris &
            ""
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        InputJurnalAdjusmentAkhirBulan_Forex(KodeAkun_Terseleksi, BulanAngka_Terseleksi)
        If jur_StatusPenyimpananJurnal_PerBaris Then TampilkanData()

    End Sub


    Sub LihatJurnalAdjusment(COA As String, BulanAngka As Integer)
        Dim TanggalAdjusment As Date = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka, TahunBukuAktif)
        Dim NomorJV As Int64 = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi " &
                              " WHERE   Valid              <> '" & _X_ & "' " &
                              " AND     Jenis_Jurnal        = '" & JenisJurnal_AdjusmentForex & "' " &
                              " AND     Tanggal_Transaksi   = '" & TanggalFormatSimpan(TanggalAdjusment) & "' " &
                              " AND     COA                 = '" & COA & " ' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            NomorJV = dr.Item("Nomor_JV")
        End If
        AksesDatabase_Transaksi(Tutup)
        LihatJurnal(NomorJV)
    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
    End Sub

    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Kode_Akun As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Januari_ As New DataGridTextColumn
    Dim Februari_ As New DataGridTextColumn
    Dim Maret_ As New DataGridTextColumn
    Dim April_ As New DataGridTextColumn
    Dim Mei_ As New DataGridTextColumn
    Dim Juni_ As New DataGridTextColumn
    Dim Juli_ As New DataGridTextColumn
    Dim Agustus_ As New DataGridTextColumn
    Dim September_ As New DataGridTextColumn
    Dim Oktober_ As New DataGridTextColumn
    Dim Nopember_ As New DataGridTextColumn
    Dim Desember_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Januari_")
        datatabelUtama.Columns.Add("Februari_")
        datatabelUtama.Columns.Add("Maret_")
        datatabelUtama.Columns.Add("April_")
        datatabelUtama.Columns.Add("Mei_")
        datatabelUtama.Columns.Add("Juni_")
        datatabelUtama.Columns.Add("Juli_")
        datatabelUtama.Columns.Add("Agustus_")
        datatabelUtama.Columns.Add("September_")
        datatabelUtama.Columns.Add("Oktober_")
        datatabelUtama.Columns.Add("Nopember_")
        datatabelUtama.Columns.Add("Desember_")

        Dim LebarKolomBulan As Integer = 72

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 222, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Januari_", "Januari", LebarKolomBulan, 1, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Februari_", "Februari", LebarKolomBulan, 2, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Maret_", "Maret", LebarKolomBulan, 3, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "April_", "April", LebarKolomBulan, 4, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Mei_", "Mei", LebarKolomBulan, 5, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Juni_", "Juni", LebarKolomBulan, 6, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Juli_", "Juli", LebarKolomBulan, 7, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Agustus_", "Agustus", LebarKolomBulan, 8, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "September_", "September", LebarKolomBulan, 9, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Oktober_", "Oktober", LebarKolomBulan, 10, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Nopember_", "Nopember", LebarKolomBulan, 11, AddressOf btn_TombolTabel_Click)
        TambahkanKolomButtonDataGrid_WPF(datagridUtama, "Desember_", "Desember", LebarKolomBulan, 12, AddressOf btn_TombolTabel_Click)

    End Sub


    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
