Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports Org.BouncyCastle.Math.EC
Imports DocumentFormat.OpenXml.Drawing
Imports ZstdSharp.Unsafe
Imports bcomm

Public Class wpfWin_BukuBesarPembantu

    Dim JudulForm
    Public COABukuPembantu
    Public KodeLawanTransaksi As String

    Dim MutasiDebetMUA As Decimal
    Dim MutasiKreditMUA As Decimal

    Dim MutasiDebetIDR As Int64
    Dim MutasiKreditIDR As Int64

    Dim SaldoAwalIDR As Int64
    Dim SaldoAkhirIDR As Int64

    Dim NomorUrut
    Dim NomorJV
    Dim TanggalTransaksi
    Dim NamaProduk
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim NomorFakturPajak
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

    Dim NomorJV_Terseleksi

    Public KodeMataUang
    Public KursTerakhirTransaksi
    Dim TanggalTerakhirTransaksi_Date As Date

    Dim QueryTampilan

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        lbl_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
        txt_SaldoAwal.Text = Kosongan

        JudulForm = "Buku Besar Pembantu - " & AmbilValue_NamaAkun(COABukuPembantu)
        KodeMataUang = AmbilValue_KodeMataUang_BerdasarkanCOA(COABukuPembantu)

        Title = JudulForm
        lbl_JudulForm.Text = JudulForm

        If KodeMataUang = KodeMataUang_IDR Then
            Debet_IDR.Header = "Debet"
            Kredit_IDR.Header = "Kredit"
            Saldo_IDR.Header = "Saldo"
            Kurs_.Visibility = Visibility.Collapsed
            Debet_MUA.Visibility = Visibility.Collapsed
            Kredit_MUA.Visibility = Visibility.Collapsed
            Saldo_MUA.Visibility = Visibility.Collapsed
            lbl_KursTerakhirTransaksi.Visibility = Visibility.Collapsed
            lbl_TitikDuaKursTerakhirTransaksi.Visibility = Visibility.Collapsed
            txt_KursTerakhirTransaksi.Visibility = Visibility.Collapsed
            lbl_TanggalTerakhirTransaksi.Visibility = Visibility.Collapsed
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
            lbl_KursTerakhirTransaksi.Visibility = Visibility.Visible
            lbl_TitikDuaKursTerakhirTransaksi.Visibility = Visibility.Visible
            txt_KursTerakhirTransaksi.Visibility = Visibility.Visible
            lbl_TanggalTerakhirTransaksi.Visibility = Visibility.Visible
        End If

        TampilkanData()

    End Sub


    Sub TampilkanData()

        Dim NomorJVSebelumnya = 0

        txt_KursTerakhirTransaksi.Text = Kosongan
        lbl_TanggalTerakhirTransaksi.Text = Kosongan
        txt_MutasiDebet.Text = Kosongan
        txt_MutasiKredit.Text = Kosongan
        txt_SaldoAwal.Text = Kosongan
        txt_SaldoAkhir.Text = Kosongan

        MutasiDebetMUA = 0
        MutasiKreditMUA = 0
        SaldoIDR = SaldoAwalIDR

        QueryTampilan = " Select * FROM tbl_Transaksi " &
            " WHERE COA                 = '" & COABukuPembantu & "' " &
            " AND Kode_Lawan_Transaksi  = '" & KodeLawanTransaksi & "' " &
            " AND Status_Approve        = 1 " &
            " ORDER BY Tanggal_Transaksi "
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        NomorUrut = 0
        Do While dr.Read
            NomorJV = dr.Item("Nomor_JV")
            If NomorJV <> NomorJVSebelumnya Then
                TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
                NamaProduk = dr.Item("Nama_Produk")
                TanggalInvoice = dr.Item("Tanggal_Invoice")
                NomorInvoice = dr.Item("Nomor_Invoice")
                NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
                Kurs = dr.Item("Kurs")
                DK = dr.Item("D_K")
                DebetMUA = dr.Item("Jumlah_Debet")
                KreditMUA = dr.Item("Jumlah_Kredit")
                DebetIDR = AmbilValue_NilaiMataUang_WithCOA(dr.Item("COA"), dr.Item("Kode_Mata_Uang"), Kurs, DebetMUA)
                KreditIDR = AmbilValue_NilaiMataUang_WithCOA(dr.Item("COA"), dr.Item("Kode_Mata_Uang"), Kurs, KreditMUA)
                If Kurs = 1 Then
                    DebetMUA = 0
                    KreditMUA = 0
                End If
                If COATermasukDEBET(COABukuPembantu) Then
                    SaldoIDR += DebetIDR - KreditIDR
                    SaldoMUA += DebetMUA - KreditMUA
                Else
                    SaldoIDR += KreditIDR - DebetIDR
                    SaldoMUA += KreditMUA - DebetMUA
                End If
                TambahBaris()
            End If
            NomorJVSebelumnya = NomorJV
        Loop
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi()
        If KodeMataUang = KodeMataUang_IDR Then
            SaldoAkhirIDR = SaldoIDR
        Else
            txt_KursTerakhirTransaksi.Text = AmbilValue_KursTengahBI(KodeMataUang, TanggalTerakhirTransaksi_Date, txt_KursTerakhirTransaksi)
            lbl_TanggalTerakhirTransaksi.Text = "(" & TanggalFormatTampilan(TanggalTerakhirTransaksi_Date) & ")"
            If Not KodeMataUang = KodeMataUang_IDR Then
                MutasiDebetIDR = AmbilValue_NilaiMataUang(KodeMataUang, KursTerakhirTransaksi, MutasiDebetMUA)
                MutasiKreditIDR = AmbilValue_NilaiMataUang(KodeMataUang, KursTerakhirTransaksi, MutasiKreditMUA)
            End If
            SaldoAkhirIDR = AmbilValue_NilaiMataUang(KodeMataUang, KursTerakhirTransaksi, SaldoIDR)
        End If
        txt_MutasiDebet.Text = MutasiDebetIDR
        txt_MutasiKredit.Text = MutasiKreditIDR
        txt_SaldoAwal.Text = SaldoAwalIDR
        txt_SaldoAkhir.Text = SaldoAkhirIDR
    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        If COATermasukDEBET(COABukuPembantu) = True Then
            SaldoIDR += DebetIDR - KreditIDR
            SaldoMUA += DebetMUA - KreditMUA
        Else
            SaldoIDR += KreditIDR - DebetIDR
            SaldoMUA += KreditMUA - DebetMUA
        End If
        datatabelUtama.Rows.Add(NomorUrut, NomorJV, TanggalTransaksi, NamaProduk,
                                TanggalInvoice, NomorInvoice, NomorFakturPajak,
                                DK, DebetMUA, KreditMUA, SaldoMUA,
                                Kurs, DebetIDR, KreditIDR, SaldoIDR,
                                UraianTransaksi, Direct)
        If KodeMataUang = KodeMataUang_IDR Then
            MutasiDebetIDR += DebetIDR
            MutasiKreditIDR += KreditIDR
        Else
            MutasiDebetMUA += DebetMUA
            MutasiKreditMUA += KreditMUA
        End If
        TanggalTerakhirTransaksi_Date = TanggalTransaksi
    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        QueryTampilan = Kosongan
        KodeLawanTransaksi = Kosongan
        COABukuPembantu = Kosongan

        txt_MutasiDebet.Text = Kosongan
        txt_MutasiKredit.Text = Kosongan
        txt_SaldoAwal.Text = Kosongan
        txt_SaldoAkhir.Text = Kosongan

        ProsesResetForm = False

    End Sub



    Private Sub txt_KursTerakhirTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KursTerakhirTransaksi.TextChanged
        KursTerakhirTransaksi = AmbilAngka_Asing(txt_KursTerakhirTransaksi.Text)
    End Sub

    Private Sub txt_SaldoAwal_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwal.TextChanged
        SaldoAwalIDR = AmbilAngka(txt_SaldoAwal.Text)
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwal)
    End Sub

    Private Sub txt_SaldoAkhir_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAkhir.TextChanged
        SaldoAkhirIDR = AmbilAngka(txt_SaldoAkhir.Text)
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

        NomorJV_Terseleksi = rowviewUtama("Nomor_JV")

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If BarisTerseleksi < 0 Then Return
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Me.ResizeMode = ResizeMode.CanResize
        Buat_DataTabelUtama()
        txt_MutasiDebet.IsReadOnly = True
        txt_MutasiKredit.IsReadOnly = True
        txt_SaldoAwal.IsReadOnly = True
        txt_SaldoAkhir.IsReadOnly = True
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
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
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
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("Tanggal_Transaksi")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
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
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "No. JV", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Transaksi, "Tanggal_Transaksi", "Tanggal Transaksi", 81, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, D_K, "D_K", "D/K", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Debet_MUA, "Debet_MUA", "Debet MUA", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kredit_MUA, "Kredit_MUA", "Kredit MUA", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_MUA, "Saldo_MUA", "Saldo MUA", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_, "Kurs_", "Kurs", 72, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Debet_IDR, "Debet_IDR", "Debet", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kredit_IDR, "Kredit_IDR", "Kredit", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_IDR, "Saldo_IDR", "Saldo", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_Transaksi, "Uraian_Transaksi", "Uraian", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Direct_, "Direct_", "Direct", 51, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub



End Class
