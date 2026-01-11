Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm


Public Class wpfUsc_BukuDisposalAssetTetap

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorBeritaAcara
    Dim TanggalBeritaAcara
    Dim KodeAsset
    Dim NamaAsset
    Dim JumlahAsset
    Dim TanggalPerolehan
    Dim HargaPerolehan
    Dim AkumulasiPenyusutan
    Dim LabaRugi
    Dim Keterangan
    Dim NomorJV_Closing

    Dim NomorUrut_Terseleksi
    Dim NomorBeritaAcara_Terseleksi
    Dim TanggalBeritaAcara_Terseleksi
    Dim KodeAsset_Terseleksi
    Dim NamaAsset_Terseleksi
    Dim JumlahAsset_Terseleksi
    Dim TanggalPerolehan_Terseleksi
    Dim HargaPerolehan_Terseleksi
    Dim AkumulasiPenyusutan_Terseleksi
    Dim LabaRugi_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Closing_Terseleksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_BukuDisposalAssetTetap.Text

        ProsesLoadingForm = False

        RefreshTampilanData()

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Sub TampilkanData()

        KesesuaianJurnal = True

        'Data Tabel :
        datatabelUtama.Rows.Clear()
        NomorUrut = 0

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_DataAsset " &
                              " WHERE Tanggal_Closing <> '" & TanggalKosongSimpan & "' " &
                              " AND Harga_Jual = 0 " &
                              " ORDER BY Nomor_JV_Closing ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read

            NomorBeritaAcara = dr.Item("Kode_Closing")
            TanggalBeritaAcara = TanggalFormatTampilan(dr.Item("Tanggal_Closing"))
            KodeAsset = dr.Item("Kode_Asset")
            NamaAsset = dr.Item("Nama_Aktiva")
            JumlahAsset = 1
            TanggalPerolehan = TanggalFormatTampilan(dr.Item("Tanggal_Perolehan"))
            HargaPerolehan = dr.Item("Harga_Perolehan")
            AkumulasiPenyusutan = dr.Item("Akumulasi_Penyusutan")
            LabaRugi = AkumulasiPenyusutan - HargaPerolehan
            Keterangan = PenghapusEnter(dr.Item("Keterangan"))
            NomorJV_Closing = dr.Item("Nomor_JV_Closing")

            If HargaPerolehan = 0 Then HargaPerolehan = StripKosong
            If AkumulasiPenyusutan = 0 Then AkumulasiPenyusutan = StripKosong
            If LabaRugi = 0 Then LabaRugi = StripKosong

            TambahBaris()

        Loop

        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

    End Sub


    Sub TambahBaris()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, NomorBeritaAcara, TanggalBeritaAcara, KodeAsset, NamaAsset, JumlahAsset,
                                TanggalPerolehan, HargaPerolehan, AkumulasiPenyusutan, LabaRugi, Keterangan, NomorJV_Closing)
    End Sub


    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_LihatJurnal.IsEnabled = False
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Closing_Terseleksi)
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
        NomorBeritaAcara_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Berita_Acara")
        TanggalBeritaAcara_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Berita_Acara")
        KodeAsset_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Asset")
        NamaAsset_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Asset")
        JumlahAsset_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Asset"))
        TanggalPerolehan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Perolehan")
        HargaPerolehan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Harga_Perolehan"))
        AkumulasiPenyusutan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Akumulasi_Penyusutan"))
        LabaRugi_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Laba_Rugi"))
        Keterangan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Keterangan_")
        NomorJV_Closing_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV_Closing"))

        If NomorJV_Closing_Terseleksi > 0 Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datagridUtama.Items.Count = 0 Then Return
        If NomorJV_Closing_Terseleksi > 0 Then
            btn_LihatJurnal_Click(sender, e)
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
    Dim Nomor_Berita_Acara As New DataGridTextColumn
    Dim Tanggal_Berita_Acara As New DataGridTextColumn
    Dim Kode_Asset As New DataGridTextColumn
    Dim Nama_Asset As New DataGridTextColumn
    Dim Jumlah_Asset As New DataGridTextColumn
    Dim Tanggal_Perolehan As New DataGridTextColumn
    Dim Harga_Perolehan As New DataGridTextColumn
    Dim Akumulasi_Penyusutan As New DataGridTextColumn
    Dim Laba_Rugi As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV_Closing As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_Berita_Acara")
        datatabelUtama.Columns.Add("Tanggal_Berita_Acara")
        datatabelUtama.Columns.Add("Kode_Asset")
        datatabelUtama.Columns.Add("Nama_Asset")
        datatabelUtama.Columns.Add("Jumlah_Asset", GetType(Integer))
        datatabelUtama.Columns.Add("Tanggal_Perolehan")
        datatabelUtama.Columns.Add("Harga_Perolehan")
        datatabelUtama.Columns.Add("Akumulasi_Penyusutan")
        datatabelUtama.Columns.Add("Laba_Rugi")
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV_Closing")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 33, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Berita_Acara, "Nomor_Berita_Acara", "Nomor Berita Acara", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Berita_Acara, "Tanggal_Berita_Acara", "Tanggal" & Enter1Baris & "Berita Acara", 90, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Asset, "Kode_Asset", "Kode Asset", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Asset, "Nama_Asset", "Nama Asset", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Asset, "Jumlah_Asset", "Jumlah", 51, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Perolehan, "Tanggal_Perolehan", "Tanggal Perolehan", 90, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Perolehan, "Harga_Perolehan", "Harga Perolehan", 99, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Akumulasi_Penyusutan, "Akumulasi_Penyusutan", "Akumulasi Penyusutan", 99, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Laba_Rugi, "Laba_Rugi", "Laba/Rugi Disposal", 99, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 270, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV_Closing, "Nomor_JV_Closing", "Nomor JV", 75, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
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
