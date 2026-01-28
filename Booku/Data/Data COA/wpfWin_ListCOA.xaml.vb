Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfWin_ListCOA

    Dim JudulForm
    Dim QueryTampilan
    Dim FilterData
    Dim FilterKategori

    Public ListAkun
    Public COATerseleksi As String
    Public NamaAkunTerseleksi As String
    Public TampilkanYangTersembunyi As Boolean
    Public KodeMataUang As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Select Case ListAkun
            Case ListAkun_Semua
                JudulForm = "Daftar Akun"
                lbl_CariCOA.Text = "Cari Akun :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = " "
            Case ListAkun_TautanCOA
                JudulForm = "Daftar Akun"
                lbl_CariCOA.Text = "Cari Akun :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = " "
            Case ListAkun_Pembelian
                JudulForm = "Daftar Akun Pembelian"
                lbl_CariCOA.Text = "Cari Akun :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_Pembelian
            Case ListAkun_KasOutlet
                JudulForm = "Daftar Akun Kas Outlet"
                lbl_CariCOA.Text = "Cari Akun Kas Outlet :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Kas"
                FilterKategori = FilterListCOA_KasOutlet
            Case ListAkun_Bank
                JudulForm = "Daftar Akun Bank"
                lbl_CariCOA.Text = "Cari Akun Bank :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Bank"
                FilterKategori = FilterListCOA_Bank
            Case ListAkun_Amortisasi
                JudulForm = "Daftar Akun Amortisasi"
                lbl_CariCOA.Text = "Cari Akun Amortisasi :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_Amortisasi
            Case ListAkun_AssetTetap
                JudulForm = "Daftar Akun Asset"
                lbl_CariCOA.Text = "Cari Akun Asset :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_AssetTetap
            Case ListAkun_AssetTanah
                JudulForm = "Daftar Akun Asset"
                lbl_CariCOA.Text = "Cari Akun Asset :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_AssetTanah
            Case ListAkun_AssetTetap_SelainTanah
                JudulForm = "Daftar Akun Asset"
                lbl_CariCOA.Text = "Cari Akun Asset :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_AssetTetap_SelainTanah
            Case ListAkun_BiayaAmortisasi
                JudulForm = "Daftar Akun Biaya Amortisasi"
                lbl_CariCOA.Text = "Cari Akun Biaya :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_BiayaAmortisasi
            Case ListAkun_BiayaPenyusutan
                JudulForm = "Daftar Akun Biaya Penyusutan Asset Tetap"
                lbl_CariCOA.Text = "Cari Akun Biaya :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_BiayaPenyusutan
            Case ListAkun_AkumulasiPenyusutan
                JudulForm = "Daftar Akun Akumulasi Penyusutan"
                lbl_CariCOA.Text = "Cari Akun :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_AkumulasiPenyusutan
            Case ListAkun_PokokPajak
                JudulForm = "Daftar Akun Pokok Pajak"
                lbl_CariCOA.Text = "Cari Akun Pokok Pajak :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_PokokPajak
            Case ListAkun_DepositOperasional
                JudulForm = "Daftar Akun Deposit Operasional"
                lbl_CariCOA.Text = "Cari Akun Deposit Operasional :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_DepositOperasional
            Case ListAkun_AktivaLainnya
                JudulForm = "Daftar Akun Aktiva Lain-lain"
                lbl_CariCOA.Text = "Cari Akun Aktiva Lain-lain :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = FilterListCOA_AktivaLainnya
            Case Else
                JudulForm = "Daftar Akun"
                lbl_CariCOA.Text = "Cari Akun :"
                COA_.Header = "Kode"
                Nama_Akun.Header = "Nama Akun"
                FilterKategori = " "
        End Select

        Title = JudulForm

        RefreshTampilanData()

        txt_CariAkun.Focus()

    End Sub

    Sub TampilkanData()

        'Filter Pencarian
        Dim FilterPencarian = " "
        If txt_CariAkun.Text <> "" Then
            Dim Srch = txt_CariAkun.Text
            Dim clm_COA = " COA LIKE '%" & Srch & "%' "
            Dim clm_NamaAkun = " OR Nama_Akun LIKE '%" & Srch & "%' "
            FilterPencarian = " AND (" & clm_COA & clm_NamaAkun & ") "
        End If

        'Filter KodeMataUang :
        Dim FilterKodeMataUang = " "
        If Not KodeMataUang = Kosongan Then
            FilterKodeMataUang = " AND Kode_Mata_Uang = '" & KodeMataUang & "' "
        End If

        'Query Tampilan :
        FilterData = FilterKategori & FilterKodeMataUang & FilterPencarian
        If ListAkun = ListAkun_TautanCOA Or TampilkanYangTersembunyi = True Then
            QueryTampilan = " SELECT * FROM tbl_COA WHERE Visibilitas <> 'XXX' " & FilterData
        Else
            QueryTampilan = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterData
        End If

        'Data Tabel :
        datatabelUtama.Rows.Clear()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            Dim COA = dr.Item("COA")
            Dim NamaAkun = dr.Item("Nama_Akun")
            Dim Visibilitas = dr.Item("Visibilitas")
            datatabelUtama.Rows.Add(COA, NamaAkun, Visibilitas)
        Loop
        AksesDatabase_General(Tutup)
        BersihkanSeleksi()
        txt_CariAkun.Focus()

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Pilih.IsEnabled = False
    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        txt_CariAkun.Text = Kosongan
        COATerseleksi = Kosongan
        NamaAkunTerseleksi = Kosongan
        ListAkun = ListAkun_Semua
        chk_TampilkanYangTersembunyi.IsChecked = False
        TampilkanYangTersembunyi = False
        KodeMataUang = Kosongan

        FilterListCOA_BiayaAmortisasi = Kosongan

        btn_Pilih.IsEnabled = False

        ProsesResetForm = False

    End Sub


    Private Sub txt_CariAkun_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_CariAkun.TextChanged
        btn_Pilih.IsEnabled = False
        TampilkanData()
    End Sub

    Private Sub chk_TampilkanYangTersembunyi_Checked(sender As Object, e As RoutedEventArgs) Handles chk_TampilkanYangTersembunyi.Checked
        TampilkanYangTersembunyi = True
        btn_Pilih.IsEnabled = False
        TampilkanData()
    End Sub

    Private Sub chk_TampilkanYangTersembunyi_Unchecked(sender As Object, e As RoutedEventArgs) Handles chk_TampilkanYangTersembunyi.Unchecked
        TampilkanYangTersembunyi = False
        btn_Pilih.IsEnabled = False
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

        btn_Pilih.IsEnabled = True

    End Sub

    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If BarisTerseleksi < 0 Then Return
        btn_Pilih_Click(sender, Nothing)
    End Sub


    Private Sub btn_Pilih_Click(sender As Object, e As RoutedEventArgs) Handles btn_Pilih.Click

        If BarisTerseleksi < 0 Then
            PesanUntukProgrammer("Tidak ada baris terseleksi.!!!")
            Return
        End If

        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If rowviewUtama Is Nothing Then Return

        COATerseleksi = rowviewUtama("COA_")
        NamaAkunTerseleksi = rowviewUtama("Nama_Akun")
        Dim VisibilitasTerseleksi = rowviewUtama("Visibilitas_")

        If VisibilitasTerseleksi = Keterangan_Tidak Then

            If Not TanyaKonfirmasi("Dengan memilih Akun ini berarti Anda setuju untuk mengubah Visibilitas-nya menjadi 'Terlihat'." & Enter2Baris &
                                   "Lanjutkan?") Then
                COATerseleksi = Kosongan
                NamaAkunTerseleksi = Kosongan
                Return
            End If

            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_COA SET Visibilitas = '" & Keterangan_YA_ & "' " &
                                  " WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)

        End If

        txt_CariAkun.Text = Kosongan
        Me.Close()

    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
    End Sub


    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer

    Dim COA_ As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Visibilitas_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("COA_")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Visibilitas_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_, "COA_", "Kode", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 350, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Visibilitas_, "Visibilitas_", "Visibilitas", 72, FormatString, TengahTengah, KunciUrut, Tersembunyi)

    End Sub

End Class
