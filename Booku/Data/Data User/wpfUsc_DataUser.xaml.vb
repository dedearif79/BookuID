Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input


Public Class wpfUsc_DataUser

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public JudulForm

    Dim NomorUrut
    Dim Username
    Dim Password
    Dim Level
    Dim NamaLengkap
    Dim Jabatan
    Dim Cluster
    Dim ClusterFinance
    Dim ClusterAccounting
    Dim StatusAktifUser

    Dim NomorUrut_Terseleksi
    Dim Username_Terseleksi
    Dim Password_Terseleksi
    Dim Level_Terseleksi
    Dim NamaLengkap_Terseleksi
    Dim Jabatan_Terseleksi
    Dim ClusterFinance_Terseleksi
    Dim ClusterAccounting_Terseleksi
    Dim StatusAktif_Terseleksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_DataUser.JudulForm

        ProsesLoadingForm = False

        RefreshTampilanData()

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
        BersihkanSeleksi()
    End Sub


    Sub TampilkanData()

        datatabelUtama.Rows.Clear()
        NomorUrut = 0

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_User ORDER BY Level DESC ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read
            NomorUrut += 1
            Username = dr.Item("Username")
            Password = DekripsiTeks(dr.Item("Password"))
            Level = dr.Item("Level")
            NamaLengkap = dr.Item("Nama")
            Jabatan = dr.Item("Jabatan")
            ClusterFinance = dr.Item("Cluster_Finance")
            ClusterAccounting = dr.Item("Cluster_Accounting")

            Cluster = Nothing
            If ClusterFinance = 1 Then Cluster = Cluster & "Finance  &  "
            If ClusterAccounting = 1 Then Cluster = Cluster & "Accounting  &  "
            If Cluster IsNot Nothing Then
                Dim ClusterReverse = Microsoft.VisualBasic.StrReverse(Cluster)
                Cluster = Microsoft.VisualBasic.StrReverse(Microsoft.VisualBasic.Mid(ClusterReverse, 6))
            End If

            If dr.Item("Status_Aktif") = 1 Then
                StatusAktifUser = "YA"
            Else
                StatusAktifUser = "TIDAK"
            End If

            TambahBaris()
        Loop

        AksesDatabase_General(Tutup)

        ' Row coloring berdasarkan status aktif
        WarnaiBaris()

    End Sub


    Sub TambahBaris()
        datatabelUtama.Rows.Add(NomorUrut, Username, Password, Level, NamaLengkap, Jabatan, Cluster, ClusterFinance, ClusterAccounting, StatusAktifUser)
    End Sub


    Sub WarnaiBaris()
        For Each row As DataRowView In dataviewUtama
            ' Coloring dilakukan di datagridUtama_LoadingRow
        Next
    End Sub


    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Blokir.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputUser = New wpfWin_InputUser
        win_InputUser.ResetForm()
        win_InputUser.FungsiForm = FungsiForm_TAMBAH
        win_InputUser.ShowDialog()
        If win_InputUser.ProsesSuntingDatabase = True Then RefreshTampilanData()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        If BarisTerseleksi < 0 Then
            PesanPeringatan("Tidak ada data terseleksi.")
            Return
        End If

        win_InputUser = New wpfWin_InputUser
        win_InputUser.ResetForm()
        win_InputUser.FungsiForm = FungsiForm_EDIT
        win_InputUser.txt_Username.Text = Username_Terseleksi
        win_InputUser.txt_Password.Password = Password_Terseleksi
        win_InputUser.txt_NamaLengkap.Text = NamaLengkap_Terseleksi
        win_InputUser.cmb_Jabatan.SelectedValue = Jabatan_Terseleksi
        win_InputUser.chk_ClusterFinance.IsChecked = (ClusterFinance_Terseleksi = 1)
        win_InputUser.chk_ClusterAccounting.IsChecked = (ClusterAccounting_Terseleksi = 1)
        win_InputUser.cmb_StatusAktif.SelectedValue = StatusAktif_Terseleksi
        win_InputUser.ShowDialog()
        If win_InputUser.ProsesSuntingDatabase = True Then RefreshTampilanData()
    End Sub


    Private Sub btn_Blokir_Click(sender As Object, e As RoutedEventArgs) Handles btn_Blokir.Click
        If BarisTerseleksi < 0 Then
            PesanPeringatan("Tidak ada data terseleksi.")
            Return
        End If

        Dim StatusAktifEdit As Integer
        Dim JenisEksekusi = Nothing

        If btn_Blokir.Content.ToString() = "Blokir" Then
            StatusAktifEdit = 0
            JenisEksekusi = "memblokir"
        Else
            StatusAktifEdit = 1
            JenisEksekusi = "mengaktifkan"
        End If

        If Not TanyaKonfirmasi("Yakin ingin " & JenisEksekusi & " user '" & NamaLengkap_Terseleksi & "'?") Then Return

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_User SET Status_Aktif = '" & StatusAktifEdit & "' WHERE Username = '" & Username_Terseleksi & "' ", KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            RefreshTampilanData()
            If StatusAktifEdit = 1 Then
                PesanPemberitahuan("User '" & NamaLengkap_Terseleksi & "' berhasil diaktifkan.")
            Else
                PesanPemberitahuan("User '" & NamaLengkap_Terseleksi & "' berhasil diblokir.")
            End If
        Catch ex As Exception
            If StatusAktifEdit = 1 Then
                PesanPeringatan("User '" & NamaLengkap_Terseleksi & "' gagal diaktifkan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            Else
                PesanPeringatan("User '" & NamaLengkap_Terseleksi & "' gagal diblokir." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
        End Try
        AksesDatabase_General(Tutup)
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
        Username_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Username_")
        Password_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Password_User")
        Level_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Level_User"))
        NamaLengkap_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_User")
        Jabatan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jabatan_User")
        ClusterFinance_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Cluster_Finance"))
        ClusterAccounting_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Cluster_Accounting"))
        StatusAktif_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Status_Aktif")

        If NomorUrut_Terseleksi > 0 Then
            btn_Edit.IsEnabled = True

            ' User tidak bisa memblokir diri sendiri
            If Username_Terseleksi = UserAktif Then
                btn_Blokir.IsEnabled = False
            Else
                btn_Blokir.IsEnabled = True
            End If

            ' Update teks tombol Blokir
            If StatusAktif_Terseleksi = "YA" Then
                btn_Blokir.Content = "Blokir"
            Else
                btn_Blokir.Content = "Aktifkan"
            End If
        Else
            BersihkanSeleksi()
        End If

    End Sub

    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub

    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        Dim row = e.Row
        Dim item = TryCast(row.Item, DataRowView)
        If item IsNot Nothing Then
            Dim status = item("Status_Aktif").ToString()
            If status = "YA" Then
                row.Foreground = WarnaTeksStandar_WPF
            Else
                row.Foreground = WarnaPeringatan_WPF
            End If
        End If
    End Sub


    ' Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Username_ As New DataGridTextColumn
    Dim Password_User As New DataGridTextColumn
    Dim Level_User As New DataGridTextColumn
    Dim Nama_User As New DataGridTextColumn
    Dim Jabatan_User As New DataGridTextColumn
    Dim Cluster_User As New DataGridTextColumn
    Dim Cluster_Finance As New DataGridTextColumn
    Dim Cluster_Accounting As New DataGridTextColumn
    Dim Status_Aktif As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Username_")
        datatabelUtama.Columns.Add("Password_User")
        datatabelUtama.Columns.Add("Level_User")
        datatabelUtama.Columns.Add("Nama_User")
        datatabelUtama.Columns.Add("Jabatan_User")
        datatabelUtama.Columns.Add("Cluster_User")
        datatabelUtama.Columns.Add("Cluster_Finance")
        datatabelUtama.Columns.Add("Cluster_Accounting")
        datatabelUtama.Columns.Add("Status_Aktif")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Username_, "Username_", "Username", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Password_User, "Password_User", "Password", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Level_User, "Level_User", "Level", 54, FormatAngka, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_User, "Nama_User", "Nama", 240, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jabatan_User, "Jabatan_User", "Jabatan", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Cluster_User, "Cluster_User", "Cluster", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Cluster_Finance, "Cluster_Finance", "Fnc", 33, FormatAngka, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Cluster_Accounting, "Cluster_Accounting", "Acc", 33, FormatAngka, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_Aktif, "Status_Aktif", "Status Aktif", 72, FormatString, TengahTengah, KunciUrut, Terlihat)

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
