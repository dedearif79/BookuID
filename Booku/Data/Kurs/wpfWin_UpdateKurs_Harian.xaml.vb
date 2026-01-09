Imports System.Windows.Controls.Primitives
Imports System.Windows.Controls
Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports System.Net.Http
Imports System.Windows
Imports System.IO
Imports bcomm

Public Class wpfWin_UpdateKurs_Harian

    Dim KodeMataUang As String
    Dim KursJual As String
    Dim KursTengah As String
    Dim KursBeli As String
    Public Tanggal As String

    Private Shared ReadOnly _http As New HttpClient() With {.Timeout = TimeSpan.FromSeconds(30)}

    Private Structure KursBJ
        Public Beli As Decimal
        Public Jual As Decimal
    End Structure


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        dtp_Tanggal.SelectedDate = TanggalFormatWPF(TanggalIni)

        ProsesLoadingForm = False

    End Sub


    Private Async Function IsiKurs6MataUangAsync(tgl As Date) As Task

        Dim daftar = New String() {
            KodeMataUang_USD,
            KodeMataUang_AUD,
            KodeMataUang_JPY,
            KodeMataUang_CNY,
            KodeMataUang_EUR,
            KodeMataUang_SGD,
            KodeMataUang_GBP}

        datatabelUtama.Clear()

        For Each mts In daftar
            Dim hasil = Await AmbilKursTransaksi1HariAsync(mts, tgl)

            Dim r = datatabelUtama.NewRow()
            r("Kode_Mata_Uang") = mts

            If hasil.HasValue Then
                Dim beli = hasil.Value.Beli
                Dim jual = hasil.Value.Jual
                Dim tengah = (beli + jual) / 2D

                r("Kurs_Beli") = beli
                r("Kurs_Jual") = jual
                r("Kurs_Tengah") = tengah
            Else
                ' Kalau BI tidak ada data (weekend/libur), boleh Anda kosongkan
                r("Kurs_Beli") = DBNull.Value
                r("Kurs_Jual") = DBNull.Value
                r("Kurs_Tengah") = DBNull.Value
            End If

            datatabelUtama.Rows.Add(r)
        Next

        datatabelUtama.AcceptChanges()
    End Function



    Private Async Function AmbilKursTransaksi1HariAsync(mts As String, tgl As Date) As Task(Of KursBJ?)
        mts = If(mts, "").Trim().ToUpperInvariant()
        If mts = "" Then Return Nothing

        ' Karena BI param tanggal string, kita coba 2 format yang paling umum.
        Dim formats() As String = {"dd-MM-yyyy", "yyyy-MM-dd"}
        Dim lastErr As Exception = Nothing

        For Each fmt In formats
            Try
                Dim dsStr = tgl.ToString(fmt, CultureInfo.InvariantCulture)

                Dim TanggalTelusur As String = TanggalFormatWPF(Tanggal)

                Dim url =
                        "https://www.bi.go.id/biwebservice/wskursbi.asmx/getSubKursLokal3" &
                        $"?mts={Uri.EscapeDataString(mts)}&startdate={Uri.EscapeDataString(TanggalTelusur)}&enddate={Uri.EscapeDataString(TanggalTelusur)}"

                Dim xml = Await _http.GetStringAsync(url)

                Dim ds As New DataSet()
                Using sr As New StringReader(xml)
                    ds.ReadXml(sr)
                End Using

                If ds.Tables.Count = 0 Then Return Nothing

                Dim raw = ds.Tables(0)
                If raw.Rows.Count = 0 Then Return Nothing

                ' Ambil baris pertama (harusnya 1 baris untuk 1 hari)
                Dim row = raw.Rows(0)

                ' Cari kolom beli/jual (nama kolom dari BI kadang beda)
                Dim colBeli = CariKolom(raw, {"beli", "buy", "kursbeli"})
                Dim colJual = CariKolom(raw, {"jual", "sell", "kursjual"})

                If colBeli Is Nothing OrElse colJual Is Nothing Then
                    Throw New Exception("Kolom beli/jual tidak ditemukan pada response BI.")
                End If

                Dim beli = AmbilAngka_Desimal(row(colBeli).ToString())
                Dim jual = AmbilAngka_Desimal(row(colJual).ToString())

                Return New KursBJ With {.Beli = beli, .Jual = jual}

            Catch ex As Exception
                lastErr = ex
            End Try
        Next

        ' Jika Anda ingin mengetahui error terakhir, bisa lempar exception:
        ' Throw New Exception("Gagal ambil kurs BI: " & lastErr.Message)

        Return Nothing
    End Function

    Private Function CariKolom(t As DataTable, kandidat() As String) As DataColumn
        For Each c As DataColumn In t.Columns
            Dim name = c.ColumnName.ToLowerInvariant().Replace(" ", "").Replace("_", "")
            For Each k In kandidat
                Dim kk = k.ToLowerInvariant().Replace(" ", "").Replace("_", "")
                If name.Contains(kk) Then Return c
            Next
        Next
        Return Nothing
    End Function


    Private Function xParseDecimalFlex(s As String) As Decimal

        'Function ini sebetulnya tidak dipakai.
        'Tapi bolehlah dipertahankan untuk dipelajari.
        'Ini codingan dari chatGPT

        s = If(s, "").Trim()
        If s = "" Then Return 0D

        Dim dec As Decimal
        If Decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, dec) Then Return dec
        If Decimal.TryParse(s, NumberStyles.Any, New CultureInfo("id-ID"), dec) Then Return dec

        Dim normalized = s.Replace(".", "").Replace(",", ".")
        If Decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, dec) Then Return dec

        Return 0D
    End Function


    Dim Tahap As Integer
    Async Sub LoadingDataKurs()

        Try
            Dim tgl = Date.Today
            If dtp_Tanggal.SelectedDate.HasValue Then tgl = dtp_Tanggal.SelectedDate.Value.Date
            Await IsiKurs6MataUangAsync(tgl)
        Catch ex As Exception
            Pesan_Gagal(ex.Message)
        End Try

    End Sub

    Private Sub dtp_Tanggal_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_Tanggal.SelectedDateChanged
        If dtp_Tanggal.Text = Kosongan Then
            Tanggal = TanggalKosong
        Else
            Tanggal = dtp_Tanggal.SelectedDate
            LoadingDataKurs()
        End If
    End Sub

    Private Sub btn_Reload_Click(sender As Object, e As RoutedEventArgs) Handles btn_Reload.Click
        LoadingDataKurs()
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If datatabelUtama.Rows.Count = 0 Then
            PesanPeringatan("Tidak ada data kurs yang akan di-upload.")
            Return
        End If

        BukaDatabasePublic()
        For Each row As DataRow In datatabelUtama.Rows
            KodeMataUang = row("Kode_Mata_Uang")
            KursJual = row("Kurs_Jual")
            KursTengah = row("Kurs_Tengah")
            KursBeli = row("Kurs_Beli")
            cmdPublic = New MySqlCommand(" INSERT INTO tbl_kurs " &
                                         " ( Tanggal, Kode_Mata_Uang, Kurs_Jual, Kurs_Tengah, Kurs_Beli ) " &
                                         " VALUES ( " &
                                         " '" & TanggalFormatSimpan(Tanggal) & "', " &
                                         " '" & KodeMataUang & "', " &
                                         " '" & DesimalFormatSimpan(KursJual) & "', " &
                                         " '" & DesimalFormatSimpan(KursTengah) & "', " &
                                         " '" & DesimalFormatSimpan(KursBeli) & "' " &
                                         " ) ",
                                         KoneksiDatabasePublic)
            cmdPublic_ExecuteNonQuery()
            Terabas()
        Next
        TutupDatabasePublic()
        If StatusKoneksiDatabasePublic Then
            PesanSukses("Upload Data Kurs berhasil.")
            datatabelUtama.Rows.Clear()
        Else
            PesanPeringatan("Upload Data Kurs gagal...!!!")
        End If
        Terabas()

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Proses = False
        Close()
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

    Dim Kode_Mata_Uang As New DataGridTextColumn
    Dim Kurs_Jual As New DataGridTextColumn
    Dim Kurs_Tengah As New DataGridTextColumn
    Dim Kurs_Beli As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Kode_Mata_Uang")
        datatabelUtama.Columns.Add("Kurs_Jual", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Tengah", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Beli", GetType(Decimal))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mata_Uang, "Kode_Mata_Uang", "MUA", 45, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Jual, "Kurs_Jual", "Kurs Jual", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Tengah, "Kurs_Tengah", "Kurs Tengah", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Beli, "Kurs_Beli", "Kurs Beli", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)

    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
    End Sub

End Class
