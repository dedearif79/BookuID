Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks
Imports bcomm

Public Class wpfUsc_BukuPengawasanPiutangDividen

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Dim EksekusiTampilanData As Boolean
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim TanggalAktaNotaris
    Dim NomorBPPD
    Dim NomorAktaNotaris
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahDividen
    Dim TarifPPh
    Dim JumlahPPh
    Dim PPhDitanggung
    Dim PPhDipotong
    Dim PiutangDividen
    Dim TanggalJatuhTempo
    Dim TotalBayar
    Dim SisaPiutang
    Dim Keterangan
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim TanggalAktaNotaris_Terseleksi
    Dim NomorBPPD_Terseleksi
    Dim NomorAktaNotaris_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim JumlahDividen_Terseleksi
    Dim TarifPPh_Terseleksi
    Dim JumlahPPh_Terseleksi
    Dim PPhDitanggung_Terseleksi
    Dim PPhDipotong_Terseleksi
    Dim PiutangDividen_Terseleksi
    Dim TanggalJatuhTempo_Terseleksi
    Dim TotalBayar_Terseleksi
    Dim SisaPiutang_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi

    Dim NomorIdPencairan_Terseleksi
    Dim NomorJV_Pencairan_Terseleksi
    Dim Referensi_Terseleksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_BukuPengawasanPiutangDividen.JudulForm

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Public Sub TampilkanData()
        TampilkanDataAsync()
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

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangDividen" &
                                  " ORDER BY Nomor_ID ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorID = dr.Item("Nomor_ID")
                TanggalAktaNotaris = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                NomorBPPD = dr.Item("Nomor_BPPD")
                NomorAktaNotaris = dr.Item("Nomor_Akta_Notaris")
                KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
                NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
                JumlahDividen = dr.Item("Jumlah_Dividen")
                TarifPPh = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh"))
                JumlahPPh = dr.Item("PPh_Terutang")
                PPhDitanggung = dr.Item("PPh_Ditanggung")
                PPhDipotong = dr.Item("PPh_Dipotong")
                PiutangDividen = JumlahDividen - PPhDipotong
                TanggalJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
                TotalBayar = 0
                cmdBAYAR = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPenerimaan " &
                                           " WHERE Nomor_BP     = '" & NomorBPPD & "' ",
                                           KoneksiDatabaseTransaksi)
                drBAYAR_ExecuteReader()
                Do While drBAYAR.Read
                    TotalBayar += drBAYAR.Item("Jumlah_Bayar")
                Loop
                SisaPiutang = PiutangDividen - TotalBayar
                Keterangan = PenghapusEnter(dr.Item("Keterangan"))
                NomorJV = dr.Item("Nomor_JV")
                User = dr.Item("User")
                TambahBaris()
                Await Task.Yield()
            Loop

            AksesDatabase_Transaksi(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPengawasanPiutangDividen")

        Finally
            BersihkanSeleksi_SetelahLoading()

        End Try

    End Sub


    Sub TambahBaris()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPPD, TanggalAktaNotaris, NomorAktaNotaris, KodeLawanTransaksi, NamaLawanTransaksi,
                                JumlahDividen, TarifPPh, JumlahPPh, PPhDitanggung, PPhDipotong, PiutangDividen, TanggalJatuhTempo,
                                TotalBayar, SisaPiutang, Keterangan, NomorJV, User)
        Terabas()
    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_Cair.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        SedangMemuatData = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub


    Sub TampilkanData_Pencairan()

        pnl_SidebarKanan.Visibility = Visibility.Visible
        datatabelBayar.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                              " WHERE Nomor_BP      = '" & NomorBPPD_Terseleksi & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        TotalBayar = 0
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
            Dim Referensi = dr.Item("Nomor_KM")
            Dim JumlahBayar = dr.Item("Jumlah_Bayar")
            TotalBayar += JumlahBayar
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJVBayar)
        Loop
        AksesDatabase_Transaksi(Tutup)

        datagridBayar.SelectedIndex = -1
        datagridBayar.SelectedItem = Nothing
        BarisBayar_Terseleksi = -1
        JumlahBarisBayar = datatabelBayar.Rows.Count

        txt_PiutangDividen.Text = PiutangDividen_Terseleksi
        txt_TotalBayar.Text = TotalBayar                                '(Sebaiknya tidak menggunakan variabel TotalBayar_Terseleksi. Agar lebih update).
        txt_SisaPiutang.Text = PiutangDividen_Terseleksi - TotalBayar   '(Sebaiknya tidak menggunakan variabel SisaPiutang_Terseleksi. Agar lebih update).

        If SisaPiutang_Terseleksi > 0 Then
            lbl_SisaPiutang.Visibility = Visibility.Visible
            txt_SisaPiutang.Visibility = Visibility.Visible
            lbl_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Text = StatusLunas_BelumLunas
        Else
            lbl_SisaPiutang.Visibility = Visibility.Collapsed
            txt_SisaPiutang.Visibility = Visibility.Collapsed
            lbl_KeteranganLunas.Visibility = Visibility.Visible
            txt_KeteranganLunas.Visibility = Visibility.Visible
            txt_KeteranganLunas.Text = StatusLunas_Lunas
        End If

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Terseleksi)
        ElseIf NomorJV_Pencairan_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pencairan_Terseleksi)
        Else
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
            Return
        End If
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputHutangPiutangDividen = New wpfWin_InputHutangPiutangDividen
        win_InputHutangPiutangDividen.ResetForm()
        win_InputHutangPiutangDividen.HutangPiutang = hp_Piutang
        win_InputHutangPiutangDividen.FungsiForm = FungsiForm_TAMBAH
        win_InputHutangPiutangDividen.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputHutangPiutangDividen = New wpfWin_InputHutangPiutangDividen
        ProsesIsiValueForm = True
        win_InputHutangPiutangDividen.ResetForm()
        win_InputHutangPiutangDividen.HutangPiutang = hp_Piutang
        win_InputHutangPiutangDividen.FungsiForm = FungsiForm_EDIT
        win_InputHutangPiutangDividen.NomorID = NomorID_Terseleksi
        win_InputHutangPiutangDividen.dtp_TanggalAktaNotaris.SelectedDate = TanggalFormatWPF(TanggalAktaNotaris_Terseleksi)
        win_InputHutangPiutangDividen.txt_NomorBP.Text = NomorBPPD_Terseleksi
        win_InputHutangPiutangDividen.txt_NomorAktaNotaris.Text = NomorAktaNotaris_Terseleksi
        win_InputHutangPiutangDividen.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        win_InputHutangPiutangDividen.txt_JumlahDividen.Text = JumlahDividen_Terseleksi
        win_InputHutangPiutangDividen.txt_TarifPPh.Text = TarifPPh_Terseleksi
        win_InputHutangPiutangDividen.txt_PPhDitanggung.Text = PPhDitanggung_Terseleksi
        win_InputHutangPiutangDividen.dtp_TanggalJatuhTempo.SelectedDate = TanggalFormatWPF(TanggalJatuhTempo_Terseleksi)
        IsiValueElemenRichTextBox(win_InputHutangPiutangDividen.txt_Keterangan, Keterangan_Terseleksi)
        win_InputHutangPiutangDividen.NomorJV = NomorJV_Terseleksi
        ProsesIsiValueForm = False
        win_InputHutangPiutangDividen.ShowDialog()
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanPiutangDividen " &
                                   " WHERE Nomor_BPPD = '" & NomorBPPD_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)


        If StatusSuntingDatabase = True Then
            HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)
            pesan_DataTerpilihBerhasilDihapus_PlusJurnal()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Private Sub btn_Cair_Click(sender As Object, e As RoutedEventArgs) Handles btn_Cair.Click

        If SisaPiutang_Terseleksi <= 0 Then
            Pesan_Informasi("Data terpilih sudah lunas.")
            Return
        End If

        win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PencairanPiutang
        win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_PencairanPiutangDividen
        win_InputBuktiPenerimaan.NomorBP = NomorBPPD_Terseleksi
        win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPenerimaan.ShowDialog()
        If win_InputBuktiPenerimaan.DialogResult = DialogResult.OK Then TampilkanData()

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
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
        NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID").ToString)
        TanggalAktaNotaris_Terseleksi = rowviewUtama("Tanggal_Akta_Notaris")
        NomorBPPD_Terseleksi = rowviewUtama("Nomor_BPPD")
        NomorAktaNotaris_Terseleksi = rowviewUtama("Nomor_Akta_Notaris")
        KodeLawanTransaksi_Terseleksi = rowviewUtama("Kode_Lawan_Transaksi")
        NamaLawanTransaksi_Terseleksi = rowviewUtama("Nama_Lawan_Transaksi")
        JumlahDividen_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Dividen").ToString)
        TarifPPh_Terseleksi = rowviewUtama("Tarif_PPh")
        JumlahPPh_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_PPh").ToString)
        PPhDitanggung_Terseleksi = AmbilAngka(rowviewUtama("PPh_Ditanggung").ToString)
        PPhDipotong_Terseleksi = AmbilAngka(rowviewUtama("PPh_Dipotong").ToString)
        PiutangDividen_Terseleksi = AmbilAngka(rowviewUtama("Piutang_Dividen").ToString)
        TanggalJatuhTempo_Terseleksi = rowviewUtama("Tanggal_Jatuh_Tempo")
        TotalBayar_Terseleksi = AmbilAngka(rowviewUtama("Total_Bayar").ToString)
        SisaPiutang_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Piutang").ToString)
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        NomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV").ToString)
        User_Terseleksi = rowviewUtama("User_")

        If NomorID_Terseleksi > 0 Then
            TampilkanData_Pencairan()
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
            btn_LihatJurnal.IsEnabled = True
            btn_Cair.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
            btn_LihatJurnal.IsEnabled = False
            btn_Cair.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub


    Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    End Sub
    Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
        HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomBayar IsNot Nothing Then
            datagridBayar.SelectedIndex = -1
            datagridBayar.SelectedItem = Nothing
            datagridBayar.SelectedCells.Clear()
            BarisBayar_Terseleksi = -1
            btn_LihatJurnal.IsEnabled = False
            NomorJV_Pencairan_Terseleksi = 0
        End If
    End Sub
    Private Sub datagridBayar_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBayar.SelectedCellsChanged

        KolomTerseleksiBayar = datagridBayar.CurrentColumn
        BarisBayar_Terseleksi = datagridBayar.SelectedIndex
        If BarisBayar_Terseleksi < 0 Then Return
        rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
        If Not rowviewBayar IsNot Nothing Then Return

        NomorJV_Terseleksi = 0
        NomorIdPencairan_Terseleksi = AmbilAngka(rowviewBayar("Nomor_ID_Bayar").ToString)
        NomorJV_Pencairan_Terseleksi = AmbilAngka(rowviewBayar("Nomor_JV_Bayar").ToString)
        Referensi_Terseleksi = rowviewBayar("Referensi_")
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
        End If
        If AmbilAngka(NomorJV_Pencairan_Terseleksi) = 0 Then btn_LihatJurnal.IsEnabled = False
    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
    End Sub


    Private Sub txt_PiutangDividen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PiutangDividen.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PiutangDividen)
    End Sub

    Private Sub txt_TotalBayar_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalBayar.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalBayar)
    End Sub

    Private Sub txt_SisaPiutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SisaPiutang.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SisaPiutang)
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
    Dim Nomor_BPPD As New DataGridTextColumn
    Dim Tanggal_Akta_Notaris As New DataGridTextColumn
    Dim Nomor_Akta_Notaris As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim Jumlah_Dividen As New DataGridTextColumn
    Dim Tarif_PPh As New DataGridTextColumn
    Dim Jumlah_PPh As New DataGridTextColumn
    Dim PPh_Ditanggung As New DataGridTextColumn
    Dim PPh_Dipotong As New DataGridTextColumn
    Dim Piutang_Dividen As New DataGridTextColumn
    Dim Tanggal_Jatuh_Tempo As New DataGridTextColumn
    Dim Total_Bayar As New DataGridTextColumn
    Dim Sisa_Piutang As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPPD")
        datatabelUtama.Columns.Add("Tanggal_Akta_Notaris")
        datatabelUtama.Columns.Add("Nomor_Akta_Notaris")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Jumlah_Dividen", GetType(Int64))
        datatabelUtama.Columns.Add("Tarif_PPh", GetType(Double))
        datatabelUtama.Columns.Add("Jumlah_PPh", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Ditanggung", GetType(Int64))
        datatabelUtama.Columns.Add("PPh_Dipotong", GetType(Int64))
        datatabelUtama.Columns.Add("Piutang_Dividen", GetType(Int64))
        datatabelUtama.Columns.Add("Tanggal_Jatuh_Tempo")
        datatabelUtama.Columns.Add("Total_Bayar", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Piutang", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPPD, "Nomor_BPPD", "Nomor BPPD", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Akta_Notaris, "Tanggal_Akta_Notaris", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Akta_Notaris, "Nomor_Akta_Notaris", "Nomor Akta Notaris", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode Anak Usaha", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Anak Usaha", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Dividen, "Jumlah_Dividen", "Jumlah Dividen", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tarif_PPh, "Tarif_PPh", "Tarif PPh (%)", 87, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_PPh, "Jumlah_PPh", "Jumlah PPh", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Ditanggung, "PPh_Ditanggung", "PPh Ditanggung", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PPh_Dipotong, "PPh_Dipotong", "PPh Dipotong", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Piutang_Dividen, "Piutang_Dividen", "Piutang Dividen", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Jatuh_Tempo, "Tanggal_Jatuh_Tempo", "Jatuh Tempo", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Total_Bayar, "Total_Bayar", "Total Bayar", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Piutang, "Sisa_Piutang", "Sisa Piutang", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

    'Tabel Bayar :
    Public datatabelBayar As DataTable
    Public dataviewBayar As DataView
    Public rowviewBayar As DataRowView
    Public newRowBayar As DataRow
    Public HeaderKolomBayar As DataGridColumnHeader
    Public KolomTerseleksiBayar As DataGridColumn
    Public BarisBayar_Terseleksi As Integer
    Public JumlahBarisBayar As Integer

    Dim Nomor_ID_Bayar As New DataGridTextColumn
    Dim Tanggal_Bayar As New DataGridTextColumn
    Dim Referensi_ As New DataGridTextColumn
    Dim Jumlah_Bayar As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar()

        datatabelBayar = New DataTable
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_")
        datatabelBayar.Columns.Add("Jumlah_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_, "Referensi_", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Jumlah_Bayar, "Jumlah_Bayar", "Jumlah Bayar", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        Buat_DataTabelBayar()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_PiutangDividen.IsReadOnly = True
        txt_TotalBayar.IsReadOnly = True
        txt_SisaPiutang.IsReadOnly = True
        lbl_KeteranganLunas.Visibility = Visibility.Collapsed
        txt_KeteranganLunas.Visibility = Visibility.Collapsed
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