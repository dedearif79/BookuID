Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks
Imports bcomm

Public Class wpfUsc_BukuPengawasanAktivaLainnya

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Dim EksekusiTampilanData As Boolean

    Public JudulForm As String
    Public NamaHalaman As String
    Dim TotalAktivaLainnya As Int64

    Dim NomorUrut
    Dim NomorID
    Dim NomorBPAL
    Dim NomorBukti
    Dim TanggalBukti
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UraianTransaksi
    Dim COADebet
    Dim COAKredit
    Dim NamaAkun
    Dim JumlahTransaksi
    Dim TanggalPencairan
    Dim Keterangan
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPAL_Terseleksi
    Dim NomorBukti_Terseleksi
    Dim TanggalBukti_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim UraianTransaksi_Terseleksi
    Dim COADebet_Terseleksi
    Dim COAKredit_Terseleksi
    Dim NamaAkun_Terseleksi
    Dim JumlahTransaksi_Terseleksi As Int64
    Dim Keterangan_Terseleksi As String
    Dim TanggalPencairan_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = JudulForm

        VisibilitasInfoSaldo(False)
        VisibilitasTombolJurnal(True)

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub

    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        ' (Tidak ada ComboBox filter di halaman ini)
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Async Sub TampilkanDataAsync()

        ' Guard clause
        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            KesesuaianJurnal = True

            'Style Tabel :
            Terabas()
            datatabelUtama.Rows.Clear()

            'Data Tabel :
            NomorUrut = 0
            TotalAktivaLainnya = 0

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_AktivaLainnya ORDER BY Nomor_ID ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            If StatusKoneksiDatabase = False Then Exit Try

            Do While dr.Read
                NomorUrut += 1
                NomorID = dr.Item("Nomor_ID")
                NomorBPAL = dr.Item("Nomor_BPAL")
                NomorBukti = dr.Item("Nomor_Bukti")
                TanggalBukti = TanggalFormatTampilan(dr.Item("Tanggal_Bukti"))
                KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
                NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
                UraianTransaksi = dr.Item("Uraian_Transaksi")
                COADebet = dr.Item("COA_Debet")
                COAKredit = dr.Item("COA_Kredit")
                NamaAkun = AmbilValue_NamaAkun(COADebet)
                JumlahTransaksi = dr.Item("Jumlah_Transaksi")
                TanggalPencairan = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
                Keterangan = PenghapusEnter(dr.Item("Keterangan"))
                NomorJV = dr.Item("Nomor_JV")
                User = dr.Item("User")
                If TanggalPencairan = TanggalKosong Then TanggalPencairan = StripKosong
                datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPAL, NomorBukti, TanggalBukti,
                                        KodeLawanTransaksi, NamaLawanTransaksi, UraianTransaksi,
                                        COADebet, COAKredit, NamaAkun, JumlahTransaksi, TanggalPencairan, Keterangan, NomorJV, User)
                If NomorJV > 0 Then
                    TotalAktivaLainnya += JumlahTransaksi
                End If
                Terabas()
                Await Task.Yield()
            Loop
            AksesDatabase_Transaksi(Tutup)

            txt_TotalTabel.Text = TotalAktivaLainnya

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPengawasanAktivaLainnya")
            SedangMemuatData = False

        Finally
            BersihkanSeleksi_SetelahLoading()
        End Try

    End Sub

    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        SedangMemuatData = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        brd_LihatJurnal.Visibility = Visibility.Collapsed
        btn_LihatJurnal.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                brd_LihatJurnal.Visibility = Visibility.Visible
                btn_LihatJurnal.Visibility = Visibility.Visible
            End If
        End If
    End Sub

    Sub VisibilitasInfoSaldo(Visibilitas As Boolean)
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_TotalTabel.Visibility = Visibility.Collapsed
        If Visibilitas Then
            grb_InfoSaldo.Visibility = Visibility.Visible
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                pnl_SaldoAwalPlusAJP.Visibility = Visibility.Visible
                pnl_TotalTabel.Visibility = Visibility.Visible
            End If
        End If
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputAktivaLainnya = New wpfWin_InputAktivaLainnya
        win_InputAktivaLainnya.ResetForm()
        win_InputAktivaLainnya.FungsiForm = FungsiForm_TAMBAH
        win_InputAktivaLainnya.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        win_InputAktivaLainnya = New wpfWin_InputAktivaLainnya
        win_InputAktivaLainnya.ResetForm()
        win_InputAktivaLainnya.FungsiForm = FungsiForm_EDIT
        win_InputAktivaLainnya.NomorID = NomorID_Terseleksi
        win_InputAktivaLainnya.txt_NomorBPAL.Text = NomorBPAL_Terseleksi
        win_InputAktivaLainnya.txt_NomorBukti.Text = NomorBukti_Terseleksi
        win_InputAktivaLainnya.dtp_TanggalBukti.SelectedDate = TanggalFormatWPF(TanggalBukti_Terseleksi)
        win_InputAktivaLainnya.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        IsiValueElemenRichTextBox(win_InputAktivaLainnya.txt_UraianTransaksi, UraianTransaksi_Terseleksi)
        win_InputAktivaLainnya.txt_COADebet.Text = COADebet_Terseleksi
        win_InputAktivaLainnya.txt_JumlahTransaksi.Text = JumlahTransaksi_Terseleksi
        IsiValueElemenRichTextBox(win_InputAktivaLainnya.txt_Keterangan, Keterangan_Terseleksi)
        win_InputAktivaLainnya.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_AktivaLainnya " &
                              " WHERE Nomor_BPAL = '" & NomorBPAL_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

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
        NomorID_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_ID").ToString)
        NomorBPAL_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_BPAL")
        NomorBukti_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Bukti")
        TanggalBukti_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Bukti")
        KodeLawanTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Lawan_Transaksi")
        NamaLawanTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Lawan_Transaksi")
        UraianTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Uraian_Transaksi")
        COADebet_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Debet")
        COAKredit_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Kredit")
        NamaAkun_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Akun")
        JumlahTransaksi_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Transaksi"))
        TanggalPencairan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Pencairan")
        Keterangan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Keterangan_")
        NomorJV_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV"))
        User_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "User_")

        If NomorID_Terseleksi > 0 Then
            If NomorJV_Terseleksi > 0 Then
                btn_LihatJurnal.IsEnabled = True
                btn_Edit.IsEnabled = False
                btn_Hapus.IsEnabled = False
            Else
                btn_LihatJurnal.IsEnabled = False
                btn_Edit.IsEnabled = True
                btn_Hapus.IsEnabled = True
            End If
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If btn_Edit.IsEnabled Then btn_Edit_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If AmbilAngka(e.Row.Item("Nomor_JV")) > 0 Then
                e.Row.Foreground = clrTeksPrimer
            Else
                e.Row.Foreground = clrError
            End If
        Else
            e.Row.Foreground = clrTeksPrimer
        End If
    End Sub



    'Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Nomor_ID As New DataGridTextColumn
    Dim Nomor_BPAL As New DataGridTextColumn
    Dim Nomor_Bukti As New DataGridTextColumn
    Dim Tanggal_Bukti As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim Uraian_Transaksi As New DataGridTextColumn
    Dim COA_Debet As New DataGridTextColumn
    Dim COA_Kredit As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Jumlah_Transaksi As New DataGridTextColumn
    Dim Tanggal_Pencairan As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPAL")
        datatabelUtama.Columns.Add("Nomor_Bukti")
        datatabelUtama.Columns.Add("Tanggal_Bukti")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Uraian_Transaksi")
        datatabelUtama.Columns.Add("COA_Debet")
        datatabelUtama.Columns.Add("COA_Kredit")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Jumlah_Transaksi", GetType(Int64))
        datatabelUtama.Columns.Add("Tanggal_Pencairan")
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV", GetType(Int64))
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPAL, "Nomor_BPAL", "Nomor BPAL", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Bukti, "Nomor_Bukti", "Nomor Bukti", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Bukti, "Tanggal_Bukti", "Tanggal Bukti", 87, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode Lawan Transaksi", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Nama Lawan Transaksi", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_Transaksi, "Uraian_Transaksi", "Uraian Transaksi", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Debet, "COA_Debet", "Kode Akun", 63, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Kredit, "COA_Kredit", "COA Kredit", 45, FormatString, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Transaksi, "Jumlah_Transaksi", "Jumlah Transaksi", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pencairan, "Tanggal_Pencairan", "Tanggal Pencairan", 87, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)

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
        pnl_Footer.Width = LebarKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub


    Private Sub txt_TotalTabel_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel)
    End Sub


    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean
    Public KesesuaianJurnal As Boolean

    Private Sub txt_SaldoBerdasarkanList_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanCOA_PlusPenyesuaian.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Private Sub txt_SelisihSaldo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo)
    End Sub

    Private Sub btn_Sesuaikan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan.Click
        FiturBelumBisaDigunakan()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA)
    End Sub

    Private Sub txt_AJP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP)
    End Sub


    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================


End Class
