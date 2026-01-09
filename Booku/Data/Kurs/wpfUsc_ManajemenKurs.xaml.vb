Imports System.Globalization
Imports System.IO
Imports System.Net.Http
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports MySql.Data.MySqlClient
Imports bcomm

Public Class wpfUsc_ManajemenKurs

    Dim KodeMataUang As String
    Public StatusAktif As Boolean

    Private Shared ReadOnly _http As New HttpClient() With {.Timeout = TimeSpan.FromSeconds(30)}

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        KontenCombo_KodeMataUang()
        If dpStart.SelectedDate Is Nothing Then dpStart.SelectedDate = Date.Today
        If dpEnd.SelectedDate Is Nothing Then dpEnd.SelectedDate = Date.Today
        datatabelUtama = BuatSchema4Kolom()
        datagridUtama.ItemsSource = datatabelUtama.DefaultView
    End Sub


    Sub KontenCombo_KodeMataUang()
        KontenCombo_KodeMataUangAsing_Public(cmb_KodeMataUang)
    End Sub


    Private Sub btn_Upload_Click(sender As Object, e As RoutedEventArgs) Handles btn_Upload.Click
        If datatabelUtama.Rows.Count = 0 Then
            PesanPeringatan("Tidak ada baris yang akan di-upload.")
            Return
        End If
        If Not TanyaKonfirmasi("Lanjutkan proses upload Data Kurs?") Then Return
        KetersediaanMenuHalaman(pnl_Halaman, False)
        Terabas()
        Dim Tanggal As String
        Dim KursJual As Decimal
        Dim KursTengah As Decimal
        Dim KursBeli As Decimal
        BukaDatabasePublic()
        For Each row As DataRow In datatabelUtama.Rows
            Tanggal = row("Tanggal_")
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
        KetersediaanMenuHalaman(pnl_Halaman, True)
        If StatusKoneksiDatabasePublic Then
            PesanSukses("Upload Data Kurs berhasil.")
            datatabelUtama.Rows.Clear()
        Else
            PesanPeringatan("Upload Data Kurs gagal...!!!")
        End If
        Terabas()
    End Sub


    Private Sub btn_UpdateKursHarian_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdateKursHarian.Click

        win_UpdateKurs_Harian = New wpfWin_UpdateKurs_Harian
        win_UpdateKurs_Harian.ShowDialog()

    End Sub


    Private Sub cmb_KodeMataUang_SelectionChanged(sender As Object, e As Controls.SelectionChangedEventArgs) Handles cmb_KodeMataUang.SelectionChanged
        KodeMataUang = cmb_KodeMataUang.SelectedItem
    End Sub


    Private Async Sub btn_Filter_Click(sender As Object, e As RoutedEventArgs) Handles btn_Filter.Click
        Try
            btn_Filter.IsEnabled = False

            Dim mts = If(KodeMataUang, "").Trim().ToUpperInvariant()
            If mts = "" Then Throw New Exception("Mata uang belum diisi. Contoh: USD, AUD, JPY, CNY.")

            If dpStart.SelectedDate Is Nothing OrElse dpEnd.SelectedDate Is Nothing Then
                Throw New Exception("Start/End date belum dipilih.")
            End If

            Dim startD = dpStart.SelectedDate.Value.Date
            Dim endD = dpEnd.SelectedDate.Value.Date
            If endD < startD Then Throw New Exception("End date tidak boleh lebih kecil dari Start date.")

            Dim dt3 = Await GetKursTransaksi4Kolom_IntervalAsync(mts, startD, endD)

            datatabelUtama.Clear()
            For Each r As DataRow In dt3.Rows
                datatabelUtama.ImportRow(r)
            Next
            datatabelUtama.AcceptChanges()

        Catch ex As Exception
            Pesan_Peringatan(ex.Message)
        Finally
            btn_Filter.IsEnabled = True
        End Try
    End Sub



    '========================
    ' Opsi 2C: getSubKursLokal3
    '========================
    Private Async Function GetKursTransaksi4Kolom_IntervalAsync(mts As String, startD As Date, endD As Date) As Task(Of DataTable)

        ' BI parameter tanggal adalah string. Umumnya dd-MM-yyyy.
        ' Saya buat fallback juga ke yyyy-MM-dd supaya aman.
        Dim dateFormats() As String = {"dd-MM-yyyy", "yyyy-MM-dd"}
        Dim lastErr As Exception = Nothing

        For Each fmt In dateFormats
            Try
                Dim s = startD.ToString(fmt, CultureInfo.InvariantCulture)
                Dim e = endD.ToString(fmt, CultureInfo.InvariantCulture)

                s = TanggalFormatSimpan(s)
                e = TanggalFormatSimpan(e)

                Dim url =
                    "https://www.bi.go.id/biwebservice/wskursbi.asmx/getSubKursLokal3" &
                    $"?mts={Uri.EscapeDataString(mts)}&startdate={Uri.EscapeDataString(s)}&enddate={Uri.EscapeDataString(e)}"

                Dim xml = Await _http.GetStringAsync(url)

                Dim ds As New DataSet()
                Using sr As New StringReader(xml)
                    ds.ReadXml(sr)
                End Using

                If ds.Tables.Count = 0 Then Throw New Exception("Response BI tidak mengandung tabel data.")

                Dim raw As DataTable = ds.Tables(0)
                If raw.Rows.Count = 0 Then
                    ' Kalau format tanggal salah atau mts tidak dikenali, seringnya row=0
                    Throw New Exception($"BI mengembalikan 0 baris untuk fmt={fmt}.")
                End If

                Return MapRawTo4Kolom(raw)

            Catch ex As Exception
                lastErr = ex
            End Try
        Next

        Throw New Exception("Gagal mengambil data kurs dari BI. Detail: " & If(lastErr?.Message, "Unknown error"))
    End Function

    Private Function BuatSchema4Kolom() As DataTable
        Dim dt As New DataTable("KursTransaksi_View")
        dt.Columns.Add("Tanggal_", GetType(String))
        dt.Columns.Add("Kurs_Jual", GetType(Decimal))
        dt.Columns.Add("Kurs_Tengah", GetType(Decimal))
        dt.Columns.Add("Kurs_Beli", GetType(Decimal))
        Return dt
    End Function

    '========================
    ' Mapping raw BI -> 3 kolom standar
    '========================
    Private Function MapRawTo4Kolom(raw As DataTable) As DataTable
        Dim dt = BuatSchema4Kolom()

        Dim colTgl = CariKolom(raw, {"tgl", "Tanggal", "date"})
        Dim colBeli = CariKolom(raw, {"beli", "buy", "kursbeli"})
        Dim colJual = CariKolom(raw, {"jual", "sell", "kursjual"})
        If colTgl Is Nothing OrElse colBeli Is Nothing OrElse colJual Is Nothing Then

            ' Kalau mapping gagal, tampilkan nama kolom raw biar kita tahu struktur yang datang
            Dim colList = String.Join(Environment.NewLine, raw.Columns.Cast(Of DataColumn)().Select(Function(c) c.ColumnName))
            Throw New Exception("Struktur kolom BI berbeda dari dugaan. Kolom yang diterima:" &
                                Environment.NewLine & colList)
        End If

        For Each r As DataRow In raw.Rows
            Dim tgl = TanggalFormatWPF(r(colTgl).ToString()) 'Ini sengaja menggunakan format yyyy-MM-dd agar mudah di-sort/order
            Dim beli = AmbilAngka_Desimal(r(colBeli).ToString())
            Dim jual = AmbilAngka_Desimal(r(colJual).ToString())
            Dim tengah = (AmbilAngka_Desimal(jual) + AmbilAngka_Desimal(beli)) / 2

            Dim nr = dt.NewRow()
            nr("Tanggal_") = tgl
            nr("Kurs_Jual") = jual
            nr("Kurs_Tengah") = tengah
            nr("Kurs_Beli") = beli
            dt.Rows.Add(nr)
        Next

        dt.DefaultView.Sort = "Tanggal_ ASC"
        Return dt.DefaultView.ToTable()
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

    Private Function ParseTanggal(s As String) As Date
        s = If(s, "").Trim()
        Dim fmts() As String = {"dd-MM-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "yyyy/MM/dd", "M/d/yyyy", "d/M/yyyy"}
        Dim d As Date
        If Date.TryParseExact(s, fmts, CultureInfo.InvariantCulture, DateTimeStyles.None, d) Then Return d
        If Date.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, d) Then Return d
        Return Date.MinValue
    End Function

    Private Function ParseDecimalFlex(s As String) As Decimal
        s = If(s, "").Trim()
        If s = "" Then Return 0D

        Dim dec As Decimal
        If Decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, dec) Then Return dec
        If Decimal.TryParse(s, NumberStyles.Any, New CultureInfo("id-ID"), dec) Then Return dec

        ' Normalisasi sederhana: 15.123,45 -> 15123.45
        Dim normalized = s.Replace(".", "").Replace(",", ".")
        If Decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, dec) Then Return dec

        Return 0D
    End Function




    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Tanggal_ As New DataGridTextColumn
    Dim Kurs_Jual As New DataGridTextColumn
    Dim Kurs_Tengah As New DataGridTextColumn
    Dim Kurs_Beli As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Tanggal_")
        datatabelUtama.Columns.Add("Kurs_Jual", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Tengah", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Beli", GetType(Decimal))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_, "Tanggal_", "Tanggal", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Jual, "Kurs_Jual", "Kurs Jual", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Tengah, "Kurs_Tengah", "Kurs Tengah", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Beli, "Kurs_Beli", "Kurs Beli", 99, FormatDesimal, KananTengah, KunciUrut, Terlihat)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
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
        StatusAktif = False
    End Sub

End Class
