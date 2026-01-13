Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Media

Public Class wpfWin_TautanCOA

    ' === VARIABEL FORM ===
    Dim JudulForm As String
    Dim QueryTampilan As String
    Dim NomorUrut As Integer
    Dim JumlahTautan As Integer

    ' === VARIABEL TERSELEKSI ===
    Dim TautanCOA_Terseleksi As String
    Dim COA_Terseleksi As String
    Dim NamaAkun_Terseleksi As String

    ' === DATATABLE & DATAGRID ===
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer

    ' === KOLOM DATAGRID ===
    Dim Nomor_Urut As New DataGridTextColumn
    Dim Tautan_COA As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Status_Tautan As New DataGridTextColumn


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        JudulForm = "Tautan COA"
        Title = JudulForm

        RefreshTampilanData()

    End Sub


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Tautan_COA")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Status_Tautan")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tautan_COA, "Tautan_COA", "Tautan COA", 270, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 80, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 270, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_Tautan, "Status_Tautan", "Status", 120, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub


    Sub TampilkanData()

        ' Query Tampilan
        QueryTampilan = " SELECT * FROM tbl_TautanCOA ORDER BY COA "

        ' Data Tabel
        datatabelUtama.Rows.Clear()
        NomorUrut = 0

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader

        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            NomorUrut += 1
            Dim TautanCOA = dr.Item("Tautan_COA").ToString()
            Dim COA = dr.Item("COA").ToString()
            If COA = Nothing OrElse COA = "" Then COA = StripKosong

            Dim NamaAkun As String = "-"
            Dim StatusTautan As String = "Belum Tertaut"

            ' Cek apakah COA sudah tertaut
            Dim cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            Dim drCOA = cmdCOA.ExecuteReader()
            If drCOA.HasRows Then
                drCOA.Read()
                NamaAkun = drCOA.Item("Nama_Akun").ToString()
                StatusTautan = "Sudah Tertaut"
            End If
            drCOA.Close()

            datatabelUtama.Rows.Add(NomorUrut, TautanCOA, COA, NamaAkun, StatusTautan)
        Loop

        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

        JumlahTautan = datatabelUtama.Rows.Count
        lbl_JumlahTautan.Text = JumlahTautan.ToString()

        ' Terapkan pewarnaan setelah data dimuat
        Pewarnaan()

    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_EditTautan.IsEnabled = False
    End Sub


    Sub Pewarnaan()
        ' Pewarnaan akan diterapkan melalui event LoadingRow
        RemoveHandler datagridUtama.LoadingRow, AddressOf datagridUtama_LoadingRow
        AddHandler datagridUtama.LoadingRow, AddressOf datagridUtama_LoadingRow
        datagridUtama.Items.Refresh()
    End Sub


    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs)
        Dim row = e.Row
        Dim rowView = TryCast(row.Item, DataRowView)
        If rowView Is Nothing Then Return

        Dim statusTautan = rowView("Status_Tautan").ToString()
        If statusTautan = "Belum Tertaut" Then
            row.Foreground = Brushes.Red
        Else
            row.Foreground = Brushes.Black
        End If
    End Sub


    ' ==================== EVENT HANDLER ====================

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
        If rowviewUtama Is Nothing Then Return

        TautanCOA_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tautan_COA")
        COA_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Akun")
        NamaAkun_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Akun")

        btn_EditTautan.IsEnabled = True

    End Sub


    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If BarisTerseleksi < 0 Then Return
        btn_EditTautan_Click(sender, Nothing)
    End Sub


    Private Sub btn_EditTautan_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditTautan.Click

        If BarisTerseleksi < 0 Then
            Pesan_Peringatan("Tidak ada COA yang terseleksi.")
            Return
        End If

        ' Buka window list COA
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_TautanCOA
        win_ListCOA.ShowDialog()

        Dim COATerpilih = win_ListCOA.COATerseleksi
        Dim NamaAkunTerpilih = win_ListCOA.NamaAkunTerseleksi

        If COATerpilih = Nothing OrElse COATerpilih = Kosongan Then Return

        ' Update database
        Try
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_TautanCOA SET COA = '" & COATerpilih & "' WHERE Tautan_COA = '" & TautanCOA_Terseleksi & "' ", KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            AksesDatabase_General(Tutup)

            ' Update tampilan
            rowviewUtama("Kode_Akun") = COATerpilih
            rowviewUtama("Nama_Akun") = NamaAkunTerpilih
            rowviewUtama("Status_Tautan") = "Sudah Tertaut"

            Pewarnaan()
            IsiValueTautanCOA()

            Pesan_Sukses("COA telah berhasil ditautkan." & Enter2Baris & "Sebaiknya penautan ini tidak dirubah lagi jika sudah terjadi transaksi pada COA terkait.")

        Catch ex As Exception
            Pesan_Gagal("Penautan COA belum berhasil." & Enter2Baris & "Silakan dicoba lagi.")
        End Try

    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub


End Class
