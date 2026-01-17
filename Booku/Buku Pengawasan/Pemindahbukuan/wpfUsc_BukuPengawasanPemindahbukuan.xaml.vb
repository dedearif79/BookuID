Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks
Imports bcomm


Public Class wpfUsc_BukuPengawasanPemindahbukuan


    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False

    Dim QueryTampilan As String
    Dim FilterDariAkun
    Dim FilterKeAkun
    Dim FilterData
    Public PilihanFilter
    Dim FilterViewLevel
    Dim StatusSuntingData As Boolean
    Dim cmdCOA, cmdCOABANK As OdbcCommand
    Dim drCOA, drCOABANK As OdbcDataReader
    Dim ItemCmb_COA, ItemCmb_NamaAkun

    'Data Tabel :
    Dim NomorUrut
    Dim NomorID
    Dim NomorBPPB
    Dim TanggalBPPB
    Dim NomorKK
    Dim COAKredit
    Dim COADebet
    Dim DariBuku
    Dim KeBuku
    Dim Penanggungjawab
    Dim TanggalTransaksi
    Dim JumlahTransaksi As Int64
    Dim UraianTransaksi
    Dim NomorJV
    Dim User

    'Data Baris Tabel Terseleksi :
    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPPB_Terseleksi
    Dim TanggalBPPB_Terseleksi
    Dim NomorKK_Terseleksi
    Dim COAKredit_Terseleksi
    Dim COADebet_Terseleksi
    Dim DariBuku_Terseleksi
    Dim KeBuku_Terseleksi
    Dim Penanggungjawab_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim UraianTransaksi_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi

    Dim TotalTransaksiPemindahbukuan As Int64


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True
        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_BukuPengawasanPemindahbukuan.JudulForm

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenComboDariCOA()
        KontenComboKeCOA()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Dim EksekusiTampilanData As Boolean

    Async Sub TampilkanDataAsync()

        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            'Style Tabel :
            Terabas()
            datatabelUtama.Rows.Clear()


            'Filter Dari Akun :
            If cmb_DariBuku.SelectedValue = "Semua" Then
                FilterDariAkun = " "
            Else
                FilterDariAkun = " AND COA_Kredit = '" & KonversiSaranaPembayaranKeCOA(cmb_DariBuku.SelectedValue) & "' "
            End If

            'Filter Ke Akun :
            If cmb_KeBuku.SelectedValue = "Semua" Then
                FilterKeAkun = " "
            Else
                FilterKeAkun = " AND COA_Debet = '" & KonversiSaranaPembayaranKeCOA(cmb_KeBuku.SelectedValue) & "' "
            End If

            'Query Tampilan :
            FilterData = FilterDariAkun & FilterKeAkun
            QueryTampilan = " SELECT * FROM tbl_Pemindahbukuan WHERE Nomor_BPPB <> 'X' " & FilterData

            TotalTransaksiPemindahbukuan = 0
            NomorUrut = 0

            AksesDatabase_Transaksi(Buka)
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(QueryTampilan & " ORDER by Nomor_ID ", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            Do While dr.Read
                NomorUrut += 1
                NomorID = dr.Item("Nomor_ID")
                NomorBPPB = dr.Item("Nomor_BPPB")
                TanggalBPPB = TanggalFormatTampilan(dr.Item("Tanggal_BPPB"))
                NomorKK = AmbilValue_NomorKKBerdasarkanNomorBP(NomorBPPB)
                COAKredit = dr.Item("COA_Kredit")
                COADebet = dr.Item("COA_Debet")
                DariBuku = KonversiCOAKeSaranaPembayaran(COAKredit)
                KeBuku = KonversiCOAKeSaranaPembayaran(COADebet)
                Penanggungjawab = dr.Item("Penanggungjawab")
                TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
                If TanggalTransaksi = TanggalKosong Then TanggalTransaksi = StripKosong
                JumlahTransaksi = AmbilValue_NilaiMataUang(dr.Item("Kode_Mata_Uang_Kredit"), dr.Item("Kurs_BI_Kredit"), dr.Item("Jumlah_Kredit"))
                UraianTransaksi = PenghapusEnter(dr.Item("Uraian_Transaksi"))
                NomorJV = dr.Item("Nomor_JV")
                User = dr.Item("User")
                datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPPB, TanggalBPPB, NomorKK, TanggalTransaksi, COAKredit, COADebet, DariBuku, KeBuku, Penanggungjawab,
                                        JumlahTransaksi, UraianTransaksi, NomorJV, User)
                If NomorJV = 0 Then
                    'datatabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaPudar
                Else
                    TotalTransaksiPemindahbukuan += JumlahTransaksi
                    'datatabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaTegas
                End If
                Await Task.Yield()
            Loop
            AksesDatabase_General(Tutup)
            AksesDatabase_Transaksi(Tutup)

            'If TotalTransaksiPemindahbukuan = 0 Then
            '    txt_TotalTransaksiPBk.Text = StripKosong
            'Else
            '    txt_TotalTransaksiPBk.Text = TotalTransaksiPemindahbukuan
            'End If

            PesanUntukProgrammer("Pewarnaan belum...!!!")

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPengawasanPemindahbukuan")

        Finally
            BersihkanSeleksi()
            KetersediaanMenuHalaman(pnl_Halaman, True)
            SedangMemuatData = False
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
        btn_Ajukan.IsEnabled = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True, False)
    End Sub




    Sub KontenComboDariCOA()
        KontenComboSaranaPembayaran_Public_WPF(cmb_DariBuku, KodeMatauang_Semua)
        cmb_DariBuku.Items.Add(Pilihan_Semua)
        cmb_DariBuku.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenComboKeCOA()
        KontenComboSaranaPembayaran_Public_WPF(cmb_KeBuku, KodeMataUang_Semua)
        cmb_KeBuku.Items.Add(Pilihan_Semua)
        cmb_KeBuku.SelectedValue = Pilihan_Semua
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Ajukan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Ajukan.Click

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.dtp_TanggalKK.IsEnabled = False
        win_InputBuktiPengeluaran.dtp_TanggalKK.SelectedDate = TanggalFormatWPF(TanggalBPPB_Terseleksi)
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_Pemindahbukuan
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = KeBuku_Terseleksi
        win_InputBuktiPengeluaran.cmb_SaranaPembayaran.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_SaranaPembayaran.SelectedValue = DariBuku_Terseleksi
        win_InputBuktiPengeluaran.NomorBP = NomorBPPB_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Internal
        If JumlahTransaksi_Terseleksi = 0 Then JumlahTransaksi_Terseleksi = StripKosong
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, NomorBPPB_Terseleksi, TanggalBPPB_Terseleksi, UraianTransaksi_Terseleksi, NomorBPPB_Terseleksi,
                                JumlahTransaksi_Terseleksi, 0, 0, 0, 0, JumlahTransaksi_Terseleksi,
                                Kosongan, Kosongan, 0, 0, 0,
                                JumlahTransaksi_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()

    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputPemindahbukuan = New wpfWin_InputPemindahbukuan
        win_InputPemindahbukuan.ResetForm()
        win_InputPemindahbukuan.FungsiForm = FungsiForm_TAMBAH
        win_InputPemindahbukuan.ShowDialog()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        win_InputPemindahbukuan = New wpfWin_InputPemindahbukuan
        win_InputPemindahbukuan.ResetForm()
        win_InputPemindahbukuan.FungsiForm = FungsiForm_EDIT
        win_InputPemindahbukuan.NomorID = NomorID_Terseleksi
        win_InputPemindahbukuan.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Data terpilih akan dihapus dari Tabel ini dan Tabel Jurnal." & Enter2Baris &
                                "Yakin ingin menghapus?") Then Return


        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabaseTransaksi = False Then
            Pesan_Gagal("Data terpilih gagal dihapus. " & Enter2Baris & teks_SilakanCobaLagi_Database)
            Return
        End If

        'Hapus Data Pengajuan Pemindahbukuan
        cmd = New OdbcCommand(" DELETE FROM tbl_Pemindahbukuan WHERE Nomor_BPPB = '" & NomorBPPB_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

        TampilkanData()

        Pesan_Sukses("Data terpilih berhasil dihapus dari Tabel ini dan Tabel Jurnal.")

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub cmb_DariBuku_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_DariBuku.SelectionChanged
        TampilkanData()
    End Sub


    Private Sub cmb_KeBuku_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KeBuku.SelectionChanged
        TampilkanData()
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
        NomorBPPB_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_BPPB")
        TanggalBPPB_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_BPPB")
        NomorKK_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_KK")
        COAKredit_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Kredit")
        COADebet_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Debet")
        DariBuku_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Dari_Buku")
        KeBuku_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Ke_Buku")
        Penanggungjawab_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Penanggungjawab_")
        TanggalTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Transaksi")
        JumlahTransaksi_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Transaksi"))
        UraianTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Uraian_Transaksi")
        NomorJV_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV").ToString)
        User_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "User_")

        If NomorJV_Terseleksi > 0 Then
            btn_Ajukan.IsEnabled = False
            btn_LihatJurnal.IsEnabled = True
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
            If NomorKK_Terseleksi = Kosongan Then
                btn_Edit.IsEnabled = True
                btn_Hapus.IsEnabled = True
            End If
        Else
            btn_Ajukan.IsEnabled = True
            btn_LihatJurnal.IsEnabled = False
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        End If

        If NomorID_Terseleksi = 0 Then BersihkanSeleksi()

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


    Dim Nomor_Urut As New DataGridTextColumn
    Dim Nomor_ID As New DataGridTextColumn
    Dim Nomor_BPPB As New DataGridTextColumn
    Dim Tanggal_BPPB As New DataGridTextColumn
    Dim Nomor_KK As New DataGridTextColumn
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim COA_Kredit As New DataGridTextColumn
    Dim COA_Debet As New DataGridTextColumn
    Dim Dari_Buku As New DataGridTextColumn
    Dim Ke_Buku As New DataGridTextColumn
    Dim Penanggungjawab_ As New DataGridTextColumn
    Dim Jumlah_Transaksi As New DataGridTextColumn
    Dim Uraian_Transaksi As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPPB")
        datatabelUtama.Columns.Add("Tanggal_BPPB")
        datatabelUtama.Columns.Add("Nomor_KK")
        datatabelUtama.Columns.Add("Tanggal_Transaksi")
        datatabelUtama.Columns.Add("COA_Kredit")
        datatabelUtama.Columns.Add("COA_Debet")
        datatabelUtama.Columns.Add("Dari_Buku")
        datatabelUtama.Columns.Add("Ke_Buku")
        datatabelUtama.Columns.Add("Penanggungjawab_")
        datatabelUtama.Columns.Add("Jumlah_Transaksi", GetType(Int64))
        datatabelUtama.Columns.Add("Uraian_Transaksi")
        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor_ID", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPPB, "Nomor_BPPB", "Nomor Pemindabukuan", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_BPPB, "Tanggal_BPPB", "Tanggal Pengajuan", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_KK, "Nomor_KK", "Nomor" & Enter1Baris & "Pengajuan", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Transaksi, "Tanggal_Transaksi", "Tanggal Transaksi", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Kredit, "COA_Kredit", "COA Kredit", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Debet, "COA_Debet", "COA Debet", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Dari_Buku, "Dari_Buku", "Dari Buku", 182, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Ke_Buku, "Ke_Buku", "Ke Buku", 182, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Penanggungjawab_, "Penanggungjawab_", "Penangungjawab", 150, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Transaksi, "Jumlah_Transaksi", "Jumlah" & Enter1Baris & "Transaksi", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_Transaksi, "Uraian_Transaksi", "Uraian", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub datagridUtma_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
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
