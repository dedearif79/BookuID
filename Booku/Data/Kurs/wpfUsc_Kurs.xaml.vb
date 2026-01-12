Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop

Public Class wpfUsc_Kurs

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Dim NomorID
    Dim KodeMataUang As String
    Dim KursAkhirBulan(13) As Decimal

    Dim TahunKurs As Integer
    Dim TahunKursTerakhir As Integer

    Dim NomorID_Terseleksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            btn_Refresh.Content = teks_Refresh
            brd_Upload.Visibility = Visibility.Visible
            btn_Upload.Visibility = Visibility.Visible
            brd_CRUD.Visibility = Visibility.Visible
            btn_Tambah.Visibility = Visibility.Visible
            btn_Edit.Visibility = Visibility.Visible
            pnl_FilterData.Visibility = Visibility.Visible
            Kurs_Akhir_Tahun_Lalu.Visibility = Visibility.Visible
        Else
            btn_Refresh.Content = teks_Update
            brd_Upload.Visibility = Visibility.Collapsed
            btn_Upload.Visibility = Visibility.Collapsed
            brd_CRUD.Visibility = Visibility.Collapsed
            btn_Tambah.Visibility = Visibility.Collapsed
            btn_Edit.Visibility = Visibility.Collapsed
            pnl_FilterData.Visibility = Visibility.Collapsed
            Kurs_Akhir_Tahun_Lalu.Visibility = Visibility.Collapsed
        End If

        'lbl_JudulForm.Text = frm_Kurs.JudulForm

        KontenCombo_TahunKurs()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub KontenCombo_TahunKurs()

        Dim Tahun As Integer
        Dim TahunSebelumnya As Integer

        BukaDatabasePublic()

        If Not StatusKoneksiDatabasePublic Then
            PesanPeringatan("Sistem tidak dapat mengakses Data Kurs" & Enter2Baris & teks_SilakanCobaLagi_Internet)
            Return
        End If

        cmb_TahunKurs.Items.Clear()

        Try
            cmdPublic = New MySqlCommand(" SELECT Tahun FROM tbl_kursakhirbulan ORDER BY ID ", KoneksiDatabasePublic)
            drPublic_ExecuteReader()
            TahunSebelumnya = 0
            Do While drPublic.Read
                Tahun = drPublic.Item("Tahun")
                If TahunSebelumnya <> Tahun Then cmb_TahunKurs.Items.Add(Tahun)
                TahunSebelumnya = Tahun
            Loop
        Catch ex As Exception
            PesanPeringatan("Sistem tidak dapat mengakses Data Kurs" & Enter2Baris & teks_SilakanCobaLagi_Internet)
            Return
        End Try
        TutupDatabasePublic()

        cmb_TahunKurs.SelectedValue = Tahun

        TahunKursTerakhir = Tahun

    End Sub


    Sub RefreshTampilanData()
        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            KontenCombo_TahunKurs()
        Else
            UpdateDataKursAkhirBulan()
            If StatusKoneksiDatabaseTransaksi_MySQL Then
                TampilkanData_SisiClient()
            Else
                PesanPeringatan("Update Gagal." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
        End If
    End Sub


    Sub TampilkanData()

        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            TampilkanData_SisiServer()
        Else
            TampilkanData_SisiClient()
        End If

        BersihkanSeleksi()

    End Sub

    Sub TampilkanData_SisiServer()

        BukaDatabasePublic()

        If Not StatusKoneksiDatabasePublic Then
            PesanPeringatan("Sistem tidak dapat mengakses Data Kurs" & Enter2Baris & teks_SilakanCobaLagi_Internet)
            Return
        End If

        'Data Tabel :
        datatabelUtama.Clear()

        cmdPublic = New MySqlCommand(" SELECT * FROM tbl_kursakhirbulan WHERE Tahun = '" & TahunKurs & "' ORDER BY ID ", KoneksiDatabasePublic)
        drPublic_ExecuteReader()
        Do While drPublic.Read
            NomorID = drPublic.Item("ID")
            KodeMataUang = drPublic.Item("Kode_Mata_Uang")
            KursAkhirBulan = {
                drPublic.Item("Akhir_Tahun_Lalu"),
                drPublic.Item("Januari"),
                drPublic.Item("Februari"),
                drPublic.Item("Maret"),
                drPublic.Item("April"),
                drPublic.Item("Mei"),
                drPublic.Item("Juni"),
                drPublic.Item("Juli"),
                drPublic.Item("Agustus"),
                drPublic.Item("September"),
                drPublic.Item("Oktober"),
                drPublic.Item("Nopember"),
                drPublic.Item("Desember")}
            TambahBaris()
        Loop

        TutupDatabasePublic()

    End Sub

    Sub TampilkanData_SisiClient()

        'Data Tabel :
        datatabelUtama.Clear()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_kursakhirbulan ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorID = Kosongan
            KodeMataUang = dr.Item("Kode_Mata_Uang")
            KursAkhirBulan = {
                dr.Item("Akhir_Tahun_Lalu"),
                dr.Item("Januari"),
                dr.Item("Februari"),
                dr.Item("Maret"),
                dr.Item("April"),
                dr.Item("Mei"),
                dr.Item("Juni"),
                dr.Item("Juli"),
                dr.Item("Agustus"),
                dr.Item("September"),
                dr.Item("Oktober"),
                dr.Item("Nopember"),
                dr.Item("Desember")}
            TambahBaris()
        Loop

        AksesDatabase_Transaksi(Tutup)

    End Sub

    Sub TambahBaris()
        datatabelUtama.Rows.Add(NomorID, KodeMataUang, KursAkhirBulan(0), KursAkhirBulan(1), KursAkhirBulan(2), KursAkhirBulan(3), KursAkhirBulan(4), KursAkhirBulan(5), KursAkhirBulan(6),
                                    KursAkhirBulan(7), KursAkhirBulan(8), KursAkhirBulan(9), KursAkhirBulan(10), KursAkhirBulan(11), KursAkhirBulan(12))
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
    End Sub


    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Upload_Click(sender As Object, e As RoutedEventArgs) Handles btn_Upload.Click

        If Not TanyaKonfirmasi("Yakin ingin mengunggah Data Kurs?") Then Return

        BukaDatabasePublic()
        If Not StatusKoneksiDatabasePublic Then
            pesan_AdaMasalahDenganKoneksiInternet()
            Return
        End If

        Try
        Catch ex As Exception
        End Try

        UploadKurs(KodeMataUang_USD)
        UploadKurs(KodeMataUang_AUD)
        UploadKurs(KodeMataUang_JPY)
        UploadKurs(KodeMataUang_CNY)
        UploadKurs(KodeMataUang_EUR)
        UploadKurs(KodeMataUang_SGD)
        UploadKurs(KodeMataUang_GBP)

        TutupDatabasePublic()

    End Sub

    Sub UploadKurs(KodeMataUang As String)

        PesanUntukProgrammer("Sub Ini sudah tidak relevan")
        Return

        Dim xlApp As New Excel.Application
        Dim xlWorkbook As Excel.Workbook = Nothing
        Dim xlWorksheet As Excel.Worksheet = Nothing

        Try

            xlWorkbook = xlApp.Workbooks.Open("D:\VB .Net Project\BookuID\Booku\00 SUPPORT\Dokumen Bahan\Kurs BI\Kurs Tengah BI.xlsx")
            xlWorksheet = xlWorkbook.Sheets(KodeMataUang)

            Dim lastRow As Integer = xlWorksheet.UsedRange.Rows.Count

            For i As Integer = 2 To lastRow ' Mulai dari baris ke-2
                Dim kursJual As Decimal = Convert.ToDecimal(xlWorksheet.Cells(i, 2).Value)
                Dim kursTengah As Decimal = Convert.ToDecimal(xlWorksheet.Cells(i, 3).Value)
                Dim kursBeli As Decimal = Convert.ToDecimal(xlWorksheet.Cells(i, 4).Value)
                Dim tglHari As Integer = AmbilAngka(AmbilTeksKiri(xlWorksheet.Cells(i, 5).Value.ToString, 2))
                Dim tglBulan As Integer = AmbilAngka(AmbilTeksTengah(xlWorksheet.Cells(i, 5).Value.ToString, 4, 2))
                Dim tglTahun As Integer = AmbilAngka(AmbilTeksTengah(xlWorksheet.Cells(i, 5).Value.ToString, 7, 4))
                Dim TglString As String = tglTahun & "-" & KonversiAngkaKeStringDuaDigit(tglBulan) & "-" & KonversiAngkaKeStringDuaDigit(tglHari)
                'PesanUntukProgrammer("Hari : " & tglHari & Enter1Baris &
                '                     "Bulan : " & tglBulan & Enter1Baris &
                '                     "Tahun : " & tglTahun & Enter1Baris &
                '                     "Tanggal String : " & TglString & Enter2Baris &
                '                     "Kurs Jual :" & kursJual & Enter1Baris &
                '                     "Kurs Tengah :" & kursTengah & Enter1Baris &
                '                     "Kurs Beli : " & kursBeli)
                Dim tanggal As Date
                tanggal = New Date(tglTahun, tglBulan, tglHari)
                'PesanUntukProgrammer("Tanggal Date : " & tanggal.ToString)

                If KodeMataUang = KodeMataUang_USD Then kursTengah = Convert.ToInt64(kursTengah)

                cmdPublic = New MySqlCommand(" INSERT INTO tbl_kurs ( Tanggal, Kode_Mata_Uang, Kurs_Jual, Kurs_Tengah, Kurs_Beli ) " &
                                             " VALUES ( '" & TanggalFormatSimpan(tanggal) & "', '" & KodeMataUang & "', " &
                                             " '" & DesimalFormatSimpan(kursJual) & "', '" & DesimalFormatSimpan(kursTengah) & "', '" & DesimalFormatSimpan(kursBeli) & "' ) ",
                                             KoneksiDatabasePublic)
                cmdPublic_ExecuteNonQuery()

            Next

            Pesan_Sukses("Import selesai.")

        Catch ex As Exception
            Pesan_Gagal("Gagal import: " & ex.Message)
        Finally
            If xlWorkbook IsNot Nothing Then xlWorkbook.Close(False)
            xlApp.Quit()
            ReleaseObject(xlWorksheet)
            ReleaseObject(xlWorkbook)
            ReleaseObject(xlApp)
        End Try

    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambah.Click

        If Not TanyaKonfirmasi("Yakin ingin menambahkan Data Kurs Tahun " & TahunKursTerakhir + 1 & "?") Then Return

        BukaDatabasePublic()
        If Not StatusKoneksiDatabasePublic Then
            pesan_AdaMasalahDenganKoneksiInternet()
            Return
        End If

        TahunKursTerakhir += 1
        NomorID = TahunKursTerakhir * 100
        cmdPublic = New MySqlCommand(
            " INSERT INTO tbl_kursakhirbulan ( ID, Tahun, Kode_Mata_Uang ) VALUES ( '" & NomorID + 1 & "', '" & TahunKursTerakhir & "', '" & KodeMataUang_USD & "' ); " & vbCrLf &
            " INSERT INTO tbl_kursakhirbulan ( ID, Tahun, Kode_Mata_Uang ) VALUES ( '" & NomorID + 2 & "', '" & TahunKursTerakhir & "', '" & KodeMataUang_AUD & "' ); " & vbCrLf &
            " INSERT INTO tbl_kursakhirbulan ( ID, Tahun, Kode_Mata_Uang ) VALUES ( '" & NomorID + 3 & "', '" & TahunKursTerakhir & "', '" & KodeMataUang_JPY & "' ); " & vbCrLf &
            " INSERT INTO tbl_kursakhirbulan ( ID, Tahun, Kode_Mata_Uang ) VALUES ( '" & NomorID + 4 & "', '" & TahunKursTerakhir & "', '" & KodeMataUang_CNY & "' ); " & vbCrLf &
            " INSERT INTO tbl_kursakhirbulan ( ID, Tahun, Kode_Mata_Uang ) VALUES ( '" & NomorID + 5 & "', '" & TahunKursTerakhir & "', '" & KodeMataUang_EUR & "' ); " & vbCrLf &
            " INSERT INTO tbl_kursakhirbulan ( ID, Tahun, Kode_Mata_Uang ) VALUES ( '" & NomorID + 6 & "', '" & TahunKursTerakhir & "', '" & KodeMataUang_SGD & "' ); " & vbCrLf &
            " INSERT INTO tbl_kursakhirbulan ( ID, Tahun, Kode_Mata_Uang ) VALUES ( '" & NomorID + 7 & "', '" & TahunKursTerakhir & "', '" & KodeMataUang_GBP & "' ); " & vbCrLf &
            " ", KoneksiDatabasePublic)
        cmdPublic_ExecuteNonQuery()

        TutupDatabasePublic()

        If StatusKoneksiDatabasePublic Then
            pesan_DataBerhasilDitambahkan()
            KontenCombo_TahunKurs()
        Else
            TahunKursTerakhir -= 1
            pesan_AdaMasalahDenganKoneksiInternet()
            Return
        End If

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        If LevelUserAktif < LevelUser_99_AppDeveloper Then Return
        win_UpdateKurs_Bulanan = New wpfWin_UpdateKurs_Bulanan
        win_UpdateKurs_Bulanan.ResetForm()
        win_UpdateKurs_Bulanan.NomorID = NomorID_Terseleksi
        win_UpdateKurs_Bulanan.TahunKurs = TahunKurs
        win_UpdateKurs_Bulanan.ShowDialog()
        If win_UpdateKurs_Bulanan.Proses Then TampilkanData()
    End Sub


    Private Sub cmb_TahunKurs_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_TahunKurs.SelectionChanged
        TahunKurs = AmbilAngka(cmb_TahunKurs.SelectedValue)
        If TahunKurs > 0 Then TampilkanData()
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

        'NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellBerpotensiDBNull(rowviewUtama, "Nomor_Urut"))
        NomorID_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_ID"))

        If NomorID_Terseleksi > 0 Then
            btn_Edit.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
        End If
    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
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

    Dim Nomor_ID As New DataGridTextColumn
    Dim Kode_Mata_Uang As New DataGridTextColumn
    Dim Kurs_Akhir_Tahun_Lalu As New DataGridTextColumn
    Dim Kurs_Akhir_Januari As New DataGridTextColumn
    Dim Kurs_Akhir_Februari As New DataGridTextColumn
    Dim Kurs_Akhir_Maret As New DataGridTextColumn
    Dim Kurs_Akhir_April As New DataGridTextColumn
    Dim Kurs_Akhir_Mei As New DataGridTextColumn
    Dim Kurs_Akhir_Juni As New DataGridTextColumn
    Dim Kurs_Akhir_Juli As New DataGridTextColumn
    Dim Kurs_Akhir_Agustus As New DataGridTextColumn
    Dim Kurs_Akhir_September As New DataGridTextColumn
    Dim Kurs_Akhir_Oktober As New DataGridTextColumn
    Dim Kurs_Akhir_Nopember As New DataGridTextColumn
    Dim Kurs_Akhir_Desember As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Kode_Mata_Uang")
        datatabelUtama.Columns.Add("Kurs_Akhir_Tahun_Lalu", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Januari", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Februari", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Maret", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_April", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Mei", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Juni", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Juli", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Agustus", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_September", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Oktober", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Nopember", GetType(Decimal))
        datatabelUtama.Columns.Add("Kurs_Akhir_Desember", GetType(Decimal))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "ID", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mata_Uang, "Kode_Mata_Uang", "Kode Mata Uang", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Tahun_Lalu, "Kurs_Akhir_Tahun_Lalu", "Tahun Lalu", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Januari, "Kurs_Akhir_Januari", "Januari", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Februari, "Kurs_Akhir_Februari", "Februari", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Maret, "Kurs_Akhir_Maret", "Maret", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_April, "Kurs_Akhir_April", "April", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Mei, "Kurs_Akhir_Mei", "Mei", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Juni, "Kurs_Akhir_Juni", "Juni", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Juli, "Kurs_Akhir_Juli", "Juli", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Agustus, "Kurs_Akhir_Agustus", "Agustus", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_September, "Kurs_Akhir_September", "September", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Oktober, "Kurs_Akhir_Oktober", "Oktober", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Nopember, "Kurs_Akhir_Nopember", "Nopember", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kurs_Akhir_Desember, "Kurs_Akhir_Desember", "Desember", 87, FormatDesimal, KananTengah, KunciUrut, Terlihat)

    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
