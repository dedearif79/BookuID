Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Data.Odbc

Public Class wpfWin_DetailPembayaranPajak

    ' === PUBLIC PROPERTIES ===
    Public JenisPajak As String
    Public JudulForm As String

    ' === STATUS FORM ===
    Private SudahDimuat As Boolean = False

    ' === VARIABEL INTERNAL ===
    Dim JenisPPh As String
    Dim QueryTampilan As String

    ' === DATATABLE & DATAVIEW ===
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    ' === KOLOM DATAGRID ===
    Dim Kolom_NomorUrut As New DataGridTextColumn
    Dim Kolom_MasaPajak As New DataGridTextColumn
    Dim Kolom_JenisPajak As New DataGridTextColumn
    Dim Kolom_NomorKetetapan As New DataGridTextColumn
    Dim Kolom_KodeSetoran As New DataGridTextColumn
    Dim Kolom_TanggalBayar As New DataGridTextColumn
    Dim Kolom_JumlahBayar As New DataGridTextColumn
    Dim Kolom_KodeNTPN As New DataGridTextColumn
    Dim Kolom_TWTL As New DataGridTextColumn
    Dim Kolom_NomorJV As New DataGridTextColumn

    ' === VARIABEL TERSELEKSI ===
    Dim NomorUrut_Terseleksi As Integer
    Dim NomorJV_Terseleksi As Int64

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
    End Sub

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If SudahDimuat Then Return

        ' Konfigurasi visibility kolom berdasarkan jenis pajak
        If JenisPajak = JenisPajak_KetetapanPajak Then
            Kolom_JenisPajak.Visibility = Visibility.Visible
            Kolom_NomorKetetapan.Visibility = Visibility.Visible
            Kolom_KodeSetoran.Visibility = Visibility.Collapsed
            Me.Width = 750
        Else
            Kolom_JenisPajak.Visibility = Visibility.Collapsed
            Kolom_NomorKetetapan.Visibility = Visibility.Collapsed
            Kolom_KodeSetoran.Visibility = Visibility.Visible
            Me.Width = 600
        End If

        JenisPPh = KonversiJenisPajakKeJenisPPh(JenisPajak)

        JudulForm = "Detail Pembayaran " & JenisPajak & " - Tahun " & TahunBukuAktif
        Title = JudulForm

        TampilkanData()

        SudahDimuat = True

    End Sub

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Masa_Pajak")
        datatabelUtama.Columns.Add("Jenis_Pajak")
        datatabelUtama.Columns.Add("Nomor_Ketetapan")
        datatabelUtama.Columns.Add("Kode_Setoran")
        datatabelUtama.Columns.Add("Tanggal_Bayar")
        datatabelUtama.Columns.Add("Jumlah_Bayar", GetType(Int64))
        datatabelUtama.Columns.Add("Kode_NTPN")
        datatabelUtama.Columns.Add("TW_TL")
        datatabelUtama.Columns.Add("Nomor_JV", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_NomorUrut, "Nomor_Urut", "No.", 36, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_MasaPajak, "Masa_Pajak", "Masa Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_JenisPajak, "Jenis_Pajak", "Jenis Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_NomorKetetapan, "Nomor_Ketetapan", "Nomor Ketetapan", 135, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_KodeSetoran, "Kode_Setoran", "Kode", 72, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_TanggalBayar, "Tanggal_Bayar", "Tgl Bayar", 72, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_JumlahBayar, "Jumlah_Bayar", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_KodeNTPN, "Kode_NTPN", "Kode NTPN", 144, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_TWTL, "TW_TL", "TW/TL", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_NomorJV, "Nomor_JV", "Nomor JV", 72, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Public Sub ResetForm()
        JenisPajak = Kosongan
        datatabelUtama.Rows.Clear()
        BersihkanSeleksi()
    End Sub

    Sub TampilkanData()

        datatabelUtama.Rows.Clear()

        Dim NomorUrut As Integer = 0
        Dim NomorBPHP As String
        Dim NomorBulan As Integer
        Dim MasaPajak As String
        Dim JenisPajak_UntukTabel As String
        Dim NomorKetetapan As String
        Dim KodeSetoran As String
        Dim TanggalBayar As String
        Dim JumlahBayar As Int64
        Dim KodeNTPN As String
        Dim TWTL As String
        Dim NomorJV As Int64

        AksesDatabase_Transaksi(Buka)

        If JenisPajak = JenisPajak_KetetapanPajak Then
            QueryTampilan = " SELECT * FROM tbl_PembayaranHutangPajak " &
                " WHERE Nomor_BPHP LIKE '%" & AwalanBPKP & "%' " &
                " ORDER BY Tanggal_Bayar "
        Else
            QueryTampilan = " SELECT * FROM tbl_PembayaranHutangPajak " &
                " WHERE Jenis_Pajak = '" & JenisPajak & "' " &
                " ORDER BY Tanggal_Bayar "
        End If

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read
            NomorUrut += 1
            NomorBPHP = dr.Item("Nomor_BPHP").ToString()
            MasaPajak = dr.Item("Masa_Pajak").ToString()
            NomorBulan = AmbilAngka(KonversiBulanKeAngka(MasaPajak))
            JenisPajak_UntukTabel = dr.Item("Jenis_Pajak").ToString()
            KodeSetoran = dr.Item("Kode_Setoran").ToString()
            TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            JumlahBayar = AmbilAngka(dr.Item("Jumlah_Bayar"))
            KodeNTPN = dr.Item("NTPN").ToString()

            If JenisPajak = JenisPajak_KetetapanPajak Then
                Dim TanggalKetetapan_Date As Date
                cmdTELUSUR = New OdbcCommand(" SELECT Nomor_Ketetapan, Tanggal_Ketetapan " &
                                             " FROM tbl_KetetapanPajak " &
                                             " WHERE Nomor_BPHP = '" & NomorBPHP & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    TanggalKetetapan_Date = drTELUSUR.Item("Tanggal_Ketetapan")
                    NomorKetetapan = drTELUSUR.Item("Nomor_Ketetapan").ToString()
                Else
                    NomorKetetapan = Kosongan
                End If
                Dim TanggalJatuhTempo_Date As Date = TanggalKetetapan_Date.AddDays(30)
                Dim TanggalBayar_Date As Date = dr.Item("Tanggal_Bayar")
                If TanggalBayar_Date > TanggalJatuhTempo_Date Then
                    TWTL = TWTL_Terlambat
                Else
                    TWTL = TWTL_TepatWaktu
                End If
            Else
                NomorKetetapan = Kosongan
                TWTL = LogikaTWTL(JenisPajak, NomorBulan, TahunPajak, TanggalBayar)
            End If

            NomorJV = AmbilAngka(dr.Item("Nomor_JV"))

            datatabelUtama.Rows.Add(NomorUrut, MasaPajak, JenisPajak_UntukTabel, NomorKetetapan,
                                    KodeSetoran, TanggalBayar, JumlahBayar, KodeNTPN, TWTL, NomorJV)
        Loop

        JumlahBaris = NomorUrut

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        NomorUrut_Terseleksi = 0
        NomorJV_Terseleksi = 0
        btn_LihatJurnal.IsEnabled = False
    End Sub

    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return

        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If rowviewUtama Is Nothing Then Return

        NomorUrut_Terseleksi = AmbilAngka(rowviewUtama("Nomor_Urut"))
        NomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))

        If NomorUrut_Terseleksi > 0 Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If

    End Sub

    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        Dim HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub

    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If BarisTerseleksi < 0 Then Return
        btn_LihatJurnal_Click(sender, Nothing)
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Terseleksi)
        Else
            Pesan_Peringatan("Data terpilih BELUM masuk JURNAL.")
            Return
        End If
    End Sub

End Class
