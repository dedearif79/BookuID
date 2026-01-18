Imports bcomm
Imports System.Data.Odbc
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfUsc_DataCOA

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False

    Dim QueryTampilan
    Dim FilterData
    Dim COATerseleksi

    Dim JumlahAktiva
    Dim JumlahPassiva
    Dim SelisihNeraca
    Public KeseimbanganNeraca As Boolean

    Dim KepalaCOA
    Dim CariAkun
    Dim VisibilitasCOA
    Dim EksekusiTampilanData As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_DataCOA.JudulForm
        pnl_FilterData.Visibility = Visibility.Collapsed
        pnl_FilterData.Visibility = Visibility.Visible


        If SistemCOA = SistemCOA_StandarAplikasi Then
            VisibilitasTombolTautanCOA(False)
        Else
            VisibilitasTombolTautanCOA(True)
        End If
        lbl_JudulForm.Text = "DATA COA - Tahun " & TahunBukuAktif
        RefreshTampilanData()

        If LevelUserAktif >= LevelUser_99_AppDeveloper Then
            VisibilitasTombolImport(True)
            VisibilitasTombolTautanCOA(True)
            KetersediaanTombolHapus(True)
        Else
            VisibilitasTombolImport(False)
            VisibilitasTombolTautanCOA(False)
            KetersediaanTombolHapus(False)
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            VisibilitasTombolBukuBesar(True)
            Saldo_Awal.Header = "Saldo Awal"
        Else
            VisibilitasTombolBukuBesar(False)
            Saldo_Awal.Header = "Saldo Akhir"
        End If
        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub

    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        txt_CariAkun.Text = Kosongan
        txt_KepalaCOA.Text = Kosongan
        KontenComboVisibilitas()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Sub VisibilitasTombolBukuBesar(Visibilitas As Boolean)
        If Visibilitas Then
            brd_BukuBesar.Visibility = Visibility.Visible
            btn_BukuBesar.Visibility = Visibility.Visible
        Else
            brd_BukuBesar.Visibility = Visibility.Collapsed
            btn_BukuBesar.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTombolImport(Visibilitas As Boolean)
        If Visibilitas Then
            btn_Import.Visibility = Visibility.Visible
        Else
            btn_Import.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KetersediaanTombolHapus(Tersedia As Boolean)
        btn_Hapus.IsEnabled = False
        If Tersedia Then
            If LevelUserAktif = LevelUser_99_AppDeveloper Then
                btn_Hapus.IsEnabled = True
            End If
        Else
            btn_Hapus.IsEnabled = False
        End If
    End Sub


    Sub VisibilitasTombolTautanCOA(Visibilitas As Boolean)
        If Visibilitas Then
            brd_TautanCOA.Visibility = Visibility.Visible
            btn_TautanCOA.Visibility = Visibility.Visible
        Else
            brd_TautanCOA.Visibility = Visibility.Collapsed
            btn_TautanCOA.Visibility = Visibility.Collapsed
        End If
    End Sub




    ''' <summary>
    ''' Method async untuk memuat data COA dengan UI responsive
    ''' </summary>
    Async Sub TampilkanDataAsync()

        ' Guard clause: Cegah loading berulang
        If SedangMemuatData Then Return
        If EksekusiTampilanData = False Then Return
        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)  ' Beri waktu UI render

        Try
            datatabelUtama.Rows.Clear()

            'Filter Pencarian :
            Dim FilterPencarian = " "
            If CariAkun = Kosongan Then
                FilterPencarian = " "
            Else
                Dim clm_COA = " COA LIKE '%" & CariAkun & "%' "
                Dim clm_NamaAkun = " OR Nama_Akun LIKE '%" & CariAkun & "%' "
                FilterPencarian = " AND ( " & clm_COA & clm_NamaAkun & " ) "
            End If

            'Filter Visibilitas :
            Dim FilterVisibilitas
            FilterVisibilitas = " "
            If VisibilitasCOA = Pilihan_Semua Then FilterVisibilitas = " "
            If VisibilitasCOA = Pilihan_Ya Then FilterVisibilitas = " AND Visibilitas = '" & Pilihan_Ya & "' "
            If VisibilitasCOA = Pilihan_Tidak Then FilterVisibilitas = " AND Visibilitas <> '" & Pilihan_Ya & "' "

            'Filter Kepala COA :
            Dim FilterKepalaCOA
            If KepalaCOA = Kosongan Then
                FilterKepalaCOA = " "
            Else
                FilterKepalaCOA = " AND COA LIKE '" & KepalaCOA & "%' "
            End If


            'Query Tampilan :
            FilterData = FilterPencarian & FilterVisibilitas & FilterKepalaCOA
            QueryTampilan = " SELECT * FROM tbl_COA WHERE D_K <> 'X' " & FilterData

            'Data Tabel :
            Dim COA
            Dim NamaAkun
            Dim KodeMataUang As String
            Dim Kurs As Decimal
            Dim DK
            Dim Saldo As Int64
            Dim Uraian
            Dim Visibilitas
            JumlahAktiva = 0
            JumlahPassiva = 0
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Do While dr.Read
                COA = dr.Item("COA")
                NamaAkun = dr.Item("Nama_Akun")
                KodeMataUang = dr.Item("Kode_Mata_Uang")
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then Kurs = KursTengahBI_AkhirTahunLalu(KodeMataUang)
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Kurs = KursTengahBI_AkhirTahunIni(KodeMataUang)
                DK = dr.Item("D_K")
                Saldo = AmbilValue_NilaiMataUang(KodeMataUang, Kurs, dr.Item("Saldo_Awal"))
                Uraian = dr.Item("Uraian")
                Visibilitas = dr.Item("Visibilitas")
                If Left(COA.ToString, 1) = "1" Then JumlahAktiva += AmbilAngka(Saldo)
                If Left(COA.ToString, 1) = "2" Or Left(COA.ToString, 1) = "3" Then JumlahPassiva += AmbilAngka(Saldo)
                datatabelUtama.Rows.Add(COA, NamaAkun, KodeMataUang, DK, Saldo, Uraian, Visibilitas)
                Await Task.Yield()  ' Beri kesempatan UI refresh
            Loop

            AksesDatabase_General(Tutup)

            If KepalaCOA <> Kosongan Or CariAkun <> Kosongan Or VisibilitasCOA = Pilihan_Tidak Then
                grb_BalanceControl.Visibility = Visibility.Collapsed
            Else
                grb_BalanceControl.Visibility = Visibility.Visible
                CekKeseimbanganNeraca()
            End If

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_DataCOA")
            SedangMemuatData = False

        Finally
            BersihkanSeleksi_SetelahLoading()
        End Try

    End Sub

    ''' <summary>
    ''' Wrapper untuk backward compatibility
    ''' </summary>
    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub
    ''' <summary>
    ''' Logika utama reset seleksi (TANPA enable UI)
    ''' </summary>
    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Edit.IsEnabled = False
        KetersediaanTombolHapus(False)
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_TotalTabel.Text = JumlahBaris
        SedangMemuatData = False
    End Sub

    ''' <summary>
    ''' Wrapper: reset seleksi + enable UI (untuk backward compatibility)
    ''' </summary>
    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub

    Sub KontenComboVisibilitas()
        cmb_Visibilitas.Items.Clear()
        cmb_Visibilitas.Items.Add(Pilihan_Ya)
        cmb_Visibilitas.Items.Add(Pilihan_Tidak)
        cmb_Visibilitas.Items.Add(Pilihan_Semua)
        cmb_Visibilitas.SelectedValue = Pilihan_Semua
    End Sub

    Sub CekKeseimbanganNeraca()

        SelisihNeraca = JumlahAktiva - JumlahPassiva

        txt_JumlahAktiva.Text = JumlahAktiva
        txt_JumlahPassiva.Text = JumlahPassiva
        txt_SelisihNeraca.Text = SelisihNeraca

        If SelisihNeraca = 0 Then
            KeseimbanganNeraca = True
            txt_JumlahAktiva.Foreground = clrTeksPrimer
            txt_JumlahPassiva.Foreground = clrTeksPrimer
            txt_SelisihNeraca.Foreground = clrTeksPrimer
        Else
            KeseimbanganNeraca = False
            txt_JumlahAktiva.Foreground = clrWarning
            txt_JumlahPassiva.Foreground = clrWarning
            txt_SelisihNeraca.Foreground = clrWarning
        End If

        NotifikasiKeseimbanganNeraca()

    End Sub





    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_TautanCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_TautanCOA.Click
        win_TautanCOA = New wpfWin_TautanCOA
        win_TautanCOA.ShowDialog()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputCOA = New wpfWin_InputCOA
        win_InputCOA.ResetForm()
        win_InputCOA.FungsiForm = FungsiForm_TAMBAH
        win_InputCOA.ShowDialog()
        If win_InputCOA.ProsesSuntingDatabase = True Then TampilkanData()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        If BarisTerseleksi = -1 Then
            PesanPeringatan("Tidak ada data terseleksi")
            Return
        End If
        win_InputCOA = New wpfWin_InputCOA
        win_InputCOA.ResetForm()
        win_InputCOA.FungsiForm = FungsiForm_EDIT
        win_InputCOA.JalurMasuk = Halaman_DATACOA
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        win_InputCOA.txt_COA.Text = COATerseleksi
        win_InputCOA.txt_NamaAkun.Text = dr.Item("Nama_Akun")
        Dim KodeMataUang = dr.Item("Kode_Mata_Uang")
        win_InputCOA.cmb_KodeMataUang.SelectedValue = KodeMataUang
        win_InputCOA.cmb_DebetKredit.SelectedValue = dr.Item("D_K")
        Dim SaldoAwal As Decimal = dr.Item("Saldo_Awal")
        If SaldoAwal = 0 Then
            win_InputCOA.txt_SaldoAwal.Text = Kosongan
        Else
            If KodeMataUang = KodeMataUang_IDR Then
                win_InputCOA.txt_SaldoAwal.Text = Convert.ToInt64(SaldoAwal)
            Else
                win_InputCOA.txt_SaldoAwal.Text = SaldoAwal
            End If
        End If
        win_InputCOA.txt_Uraian.Text = dr.Item("Uraian")
        win_InputCOA.cmb_Visibilitas.SelectedValue = dr.Item("Visibilitas")
        AksesDatabase_General(Tutup)
        win_InputCOA.ShowDialog()
        If win_InputCOA.ProsesSuntingDatabase = True Then TampilkanData()
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            PesanPemberitahuan("Menu ini belum bisa digunakan..!")
            Return
        End If

        If BarisTerseleksi < 0 Then
            PesanPeringatan("Tidak ada baris yang terseleksi..!")
            Return
        End If

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_General(Buka)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_COA WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseGeneral)
        cmdHAPUS_ExecuteNonQuery()
        cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            TahunBuku_Alternatif = dr.Item("Tahun_Buku")
            BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_SaldoAwalCOA WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseTransaksi_Alternatif)
            cmdHAPUS_ExecuteNonQuery()
            TutupDatabaseTransaksi_Alternatif()
        Loop
        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            TampilkanData()
            'pesan_DataTerpilihBerhasilDihapus()
        End If

    End Sub


    Private Sub btn_BukuBesar_Click(sender As Object, e As RoutedEventArgs) Handles btn_BukuBesar.Click
        BukaHalamanBukuBesar(COATerseleksi)
    End Sub


    Private Sub btn_Import_Click(sender As Object, e As RoutedEventArgs) Handles btn_Import.Click
        PesanPemberitahuan("Fitur ini masih dalam perbaikan.")
        Return
        win_ProgressImportDataCOA = New wpfWin_ProgressImportDataCOA
        win_ProgressImportDataCOA.ShowDialog()
        If StatusPosting = "BATAL" Then
            Pesan_Informasi("Proses posting telah dibatalkan seluruhnya pada event ini.")
        End If
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub txt_KepalaCOA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KepalaCOA.TextChanged
        KepalaCOA = txt_KepalaCOA.Text
        txt_CariAkun.Text = Kosongan
        TampilkanData()
    End Sub
    Private Sub txt_KepalaCOA_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_KepalaCOA.PreviewTextInput

    End Sub


    Private Sub txt_CariAkun_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_CariAkun.TextChanged
        CariAkun = txt_CariAkun.Text
        TampilkanData()
    End Sub


    Private Sub cmb_Visibilitas_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Visibilitas.SelectionChanged
        VisibilitasCOA = cmb_Visibilitas.SelectedValue
        TampilkanData()
    End Sub


    Private Sub txt_JumlahAktiva_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahAktiva.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahAktiva)
    End Sub


    Private Sub txt_JumlahPassiva_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPassiva.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahPassiva)
    End Sub


    Private Sub txt_SelisihNeraca_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihNeraca.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihNeraca)
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
        COATerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_")

        If COATerseleksi = Kosongan Then
            btn_BukuBesar.IsEnabled = False
            btn_Edit.IsEnabled = False
            KetersediaanTombolHapus(False)
        Else
            btn_BukuBesar.IsEnabled = True
            btn_Edit.IsEnabled = True
            KetersediaanTombolHapus(True)
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If IsDBNull(e.Row.Item("COA_")) Then
            e.Row.FontWeight = FontWeights.Bold
        Else
            If e.Row.Item("Visibilitas_") = Pilihan_Ya Then
                e.Row.Foreground = clrTeksPrimer
            Else
                e.Row.Foreground = clrNeutral500
            End If
        End If
    End Sub



    Sub NotifikasiKeseimbanganNeraca()

        Dim NotifikasiNeracaTidakSeimbang = "Neraca pada Saldo Akhir COA tidak seimbang!"


        If KeseimbanganNeraca = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" DELETE FROM tbl_Notifikasi WHERE Notifikasi = '" & NotifikasiNeracaTidakSeimbang & "' ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        If KeseimbanganNeraca = False Then
            Dim NotifikasiSudahAda As Boolean
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Notifikasi WHERE Notifikasi = '" & NotifikasiNeracaTidakSeimbang & "' ", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                NotifikasiSudahAda = True
            Else
                NotifikasiSudahAda = False
            End If
            AksesDatabase_Transaksi(Tutup)
            If NotifikasiSudahAda = False Then
                notif_NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Notifikasi") + 1
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = Today
                notif_Notifikasi = NotifikasiNeracaTidakSeimbang
                notif_HalamanTarget = Halaman_DATACOA
                notif_Pesan = NotifikasiNeracaTidakSeimbang & Enter2Baris & "Silakan seimbangkan Neraca yang ada di Data COA!"
                notif_StatusDibaca = 0
                notif_StatusDieksekusi = 0
                SimpanNotifikasi()
            End If
        End If

        win_BOOKU.IsiKontenNotifikasi()

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

    Dim COA_ As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Kode_Mata_Uang As New DataGridTextColumn
    Dim D_K As New DataGridTextColumn
    Dim Saldo_Awal As New DataGridTextColumn
    Dim Uraian_ As New DataGridTextColumn
    Dim Visibilitas_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("COA_")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Kode_Mata_Uang")
        datatabelUtama.Columns.Add("D_K")
        datatabelUtama.Columns.Add("Saldo_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Uraian_")
        datatabelUtama.Columns.Add("Visibilitas_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_, "COA_", "COA", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 447, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Mata_Uang, "Kode_Mata_Uang", "Mata" & Enter1Baris & "Uang", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, D_K, "D_K", "D/K", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal, "Saldo_Awal", "Saldo Awal", 123, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_, "Uraian_", "Uraian", 441, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Visibilitas_, "Visibilitas_", "Visibilitas", 72, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_JumlahAktiva.IsReadOnly = True
        txt_JumlahPassiva.IsReadOnly = True
        txt_SelisihNeraca.IsReadOnly = True
        cmb_Visibilitas.IsReadOnly = True
        txt_TotalTabel.Text = "Jumlah COA : "
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
        pnl_SidebarKiri.Height = TinggiKonten
        pnl_SidebarKanan.Height = TinggiKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
