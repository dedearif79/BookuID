Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_DataToko

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Dim EksekusiTampilanData As Boolean

    Public JudulForm
    Public KesesuaianJurnal

    Dim NomorUrut
    Dim KodeToko
    Dim NamaToko
    Dim Alamat
    Dim Deskripsi

    Dim NomorUrut_Terseleksi
    Dim KodeToko_Terseleksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_DataToko.JudulForm


        ProsesLoadingForm = False

        RefreshTampilanData()

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        ' (Tidak ada ComboBox filter untuk diisi)
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    ''' <summary>
    ''' Method async untuk memuat data Toko dengan UI responsive
    ''' </summary>
    Async Sub TampilkanDataAsync()

        ' Guard clause: Cegah loading berulang
        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)  ' Beri waktu UI render

        Try
            KesesuaianJurnal = True

            'Data Tabel :
            datatabelUtama.Rows.Clear()
            NomorUrut = 0

            AksesDatabase_General(Buka)

            cmd = New OdbcCommand(" SELECT * FROM tbl_Toko ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()

            Do While dr.Read
                KodeToko = dr.Item("Kode_Toko")
                NamaToko = dr.Item("Nama_Toko")
                Alamat = PenghapusEnter(dr.Item("Alamat"))
                Deskripsi = PenghapusEnter(dr.Item("Deskripsi"))
                TambahBaris()
                Await Task.Yield()  ' Beri kesempatan UI refresh
            Loop
            AksesDatabase_General(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_DataToko")
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

    Sub AmbilValueChecked(ByVal Kolom As String, ByRef Cek As Boolean)
        If dr.Item(Kolom) = 1 Then
            Cek = True
        Else
            Cek = False
        End If
    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, KodeToko, NamaToko, Alamat, Deskripsi)
    End Sub

    ''' <summary>
    ''' Logika utama reset seleksi (TANPA enable UI)
    ''' </summary>
    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
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



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputToko = New wpfWin_InputToko
        win_InputToko.ResetForm()
        win_InputToko.FungsiForm = FungsiForm_TAMBAH
        win_InputToko.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputToko = New wpfWin_InputToko
        win_InputToko.ResetForm()
        win_InputToko.FungsiForm = FungsiForm_EDIT
        win_InputToko.txt_KodeToko.Text = KodeToko_Terseleksi
        win_InputToko.ShowDialog()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click
        PesanPeringatan("Menu 'Hapus' tidak dapat digunakan..!")
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
        KodeToko_Terseleksi = rowviewUtama("Kode_Toko")

        If NomorUrut_Terseleksi > 0 Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
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
    Dim Kode_Toko As New DataGridTextColumn
    Dim Nama_Toko As New DataGridTextColumn
    Dim Alamat_ As New DataGridTextColumn
    Dim Deskripsi_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Kode_Toko")
        datatabelUtama.Columns.Add("Nama_Toko")
        datatabelUtama.Columns.Add("Alamat_")
        datatabelUtama.Columns.Add("Deskripsi_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Toko, "Kode_Toko", "Kode", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Toko, "Nama_Toko", "Nama" & Enter1Baris & "Toko", 270, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Alamat_, "Alamat_", "Alamat", 450, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Deskripsi_, "Deskripsi_", "Atas Nama", 450, FormatString, KiriTengah, KunciUrut, Terlihat)

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
