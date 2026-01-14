Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input


Public Class wpfWin_VerifikasiDataJurnal

    Public NomorJV As String = ""
    Public BarisAwalBahanJV As Integer = 0
    Public BarisAkhirBahanJV As Integer = 0
    Public JumlahBarisBahanJV As Integer = 0

    Dim TotalDebet As Int64 = 0
    Dim TotalKredit As Int64 = 0
    Dim StatusBalance As String = ""

    Dim COATerseleksi As String = ""
    Dim NamaAkunTerseleksi As String = ""
    Dim DKTerseleksi As String = ""
    Dim JumlahDebetTerseleksi As Int64 = 0
    Dim JumlahKreditTerseleksi As Int64 = 0
    Dim NomorIDTerseleksi As String = ""


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        lbl_Judul.Text = "Verifikasi Data Jurnal : Nomor " & NomorJV
        lbl_BarisTabelExcel.Text = "( Baris " & BarisAwalBahanJV & " sampai " & BarisAkhirBahanJV & " pada tabel Excel )."

        ResetForm()
        TampilkanData()

    End Sub


    Sub ResetForm()
        btn_OK.IsEnabled = True
        datatabelUtama.Rows.Clear()
        BersihkanSeleksi()
    End Sub


    Sub TampilkanData()

        Dim NomorUrut As Integer = 0
        Dim clm_NoUrut As String = ""
        Dim COA As String = ""
        Dim NamaAkun As String = ""
        Dim DK As String = ""
        Dim JumlahDebet As Int64 = 0
        Dim JumlahKredit As Int64 = 0
        Dim clm_JumlahDebet As Object = Nothing
        Dim clm_JumlahKredit As Object = Nothing
        Dim NomorID As String = ""
        Dim cmdCOA As OdbcCommand
        Dim drCOA As OdbcDataReader

        TotalDebet = 0
        TotalKredit = 0

        AksesDatabase_Transaksi(Buka)
        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & AmbilTengah(NomorJV, PanjangTeks_AwalanNomorJV_Plus1) & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader

        datatabelUtama.Rows.Clear()

        Do While dr.Read
            NomorUrut = NomorUrut + 1
            clm_NoUrut = NomorUrut & "."
            COA = dr.Item("COA")

            cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drCOA = cmdCOA.ExecuteReader
            drCOA.Read()

            If drCOA.HasRows Then
                NamaAkun = drCOA.Item("Nama_Akun")
            Else
                NamaAkun = teks_CoaBelumTerdaftar
            End If

            DK = dr.Item("D_K")
            JumlahDebet = dr.Item("Jumlah_Debet")
            JumlahKredit = dr.Item("Jumlah_Kredit")
            clm_JumlahDebet = JumlahDebet
            clm_JumlahKredit = JumlahKredit

            If JumlahDebet = 0 Then
                clm_JumlahDebet = 0
                NamaAkun = PenjorokNamaAkun & NamaAkun
            End If

            If JumlahKredit = 0 Then
                clm_JumlahKredit = 0
            End If

            TotalDebet = TotalDebet + JumlahDebet
            TotalKredit = TotalKredit + JumlahKredit
            NomorID = dr.Item("Nomor_ID")

            datatabelUtama.Rows.Add(clm_NoUrut, COA, NamaAkun, DK, clm_JumlahDebet, clm_JumlahKredit, NomorID)
        Loop

        AksesDatabase_General(Tutup)
        AksesDatabase_Transaksi(Tutup)

        If StatusPosting = "BATAL" Then
            Pesan_Gagal("Proses GAGAL, karena ada COA yang belum terdaftar." &
                Enter2Baris & "Silakan perbaiki dan sesuaikan sumber data, kemudian ulangi lagi.")
            win_ImportJurnal.HapusSemuaDataPostinganJurnalEventIni()
            Me.Close()
            Return
        End If

        JumlahBaris = datatabelUtama.Rows.Count
        JumlahBarisBahanJV = JumlahBaris

        ' Tambah baris kosong dan baris total
        datatabelUtama.Rows.Add()
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, 0, 0, Kosongan)

        NotifBalance()

        BersihkanSeleksi()

    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
    End Sub


    Sub NotifBalance()

        If TotalDebet = TotalKredit Then
            StatusBalance = teks_TidakAdaSelisih
            lbl_StatusBalance.Foreground = clrPrimary
        Else
            StatusBalance = teks_AdaSelisih
            lbl_StatusBalance.Foreground = clrWarning
        End If

        lbl_StatusBalance.Text = StatusBalance

        Dim clm_TotalDebet As Object = Nothing
        Dim clm_TotalKredit As Object = Nothing

        If TotalDebet = 0 Then
            clm_TotalDebet = 0
        Else
            clm_TotalDebet = TotalDebet
        End If

        If TotalKredit = 0 Then
            clm_TotalKredit = 0
        Else
            clm_TotalKredit = TotalKredit
        End If

        ' Update baris terakhir sebagai baris total
        datatabelUtama.Rows(JumlahBaris + 1)("Nama_Akun") = "J  U  M  L  A  H"
        datatabelUtama.Rows(JumlahBaris + 1)("Jumlah_Debet") = clm_TotalDebet
        datatabelUtama.Rows(JumlahBaris + 1)("Jumlah_Kredit") = clm_TotalKredit

        If TotalKredit = 0 And TotalDebet > 0 Then
            lbl_StatusBalance.Text = teks_TidakAdaKredit
            btn_OK.IsEnabled = False
        End If

        If TotalDebet = 0 And TotalKredit > 0 Then
            lbl_StatusBalance.Text = teks_TidakAdaDebet
            btn_OK.IsEnabled = False
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

        COATerseleksi = rowviewUtama("Kode_Akun")
        NamaAkunTerseleksi = rowviewUtama("Nama_Akun")
        DKTerseleksi = rowviewUtama("D_K")
        JumlahDebetTerseleksi = AmbilAngka(rowviewUtama("Jumlah_Debet"))
        JumlahKreditTerseleksi = AmbilAngka(rowviewUtama("Jumlah_Kredit"))
        NomorIDTerseleksi = rowviewUtama("Nomor_ID")

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick

        If BarisTerseleksi < 0 Then Return
        If BarisTerseleksi >= JumlahBaris Then Return ' Jangan edit baris kosong atau total

        If String.IsNullOrEmpty(COATerseleksi) Then Return

        win_InputJurnalPerTransaksi = New wpfWin_InputJurnalPerTransaksi
        win_InputJurnalPerTransaksi.ResetForm()
        win_InputJurnalPerTransaksi.FungsiForm = FungsiForm_EDIT
        win_InputJurnalPerTransaksi.KodeAkunInput = COATerseleksi
        win_InputJurnalPerTransaksi.NamaAkunInput = NamaAkunTerseleksi

        If DKTerseleksi = "D" Then
            win_InputJurnalPerTransaksi.DKInput = "D"
            win_InputJurnalPerTransaksi.JumlahTransaksiInput = JumlahDebetTerseleksi
        End If

        If DKTerseleksi = "K" Then
            win_InputJurnalPerTransaksi.DKInput = "K"
            win_InputJurnalPerTransaksi.JumlahTransaksiInput = JumlahKreditTerseleksi
        End If

        win_InputJurnalPerTransaksi.ShowDialog()

        ' Update data setelah edit
        rowviewUtama("Kode_Akun") = win_InputJurnalPerTransaksi.COATerseleksi
        rowviewUtama("Nama_Akun") = win_InputJurnalPerTransaksi.NamaAkunTerseleksi
        Dim DK = win_InputJurnalPerTransaksi.DK
        Dim JumlahTransaksi As Int64 = win_InputJurnalPerTransaksi.JumlahTransaksi

        If DK = "D" Then
            rowviewUtama("D_K") = "D"
            rowviewUtama("Jumlah_Debet") = JumlahTransaksi
            rowviewUtama("Jumlah_Kredit") = 0
        End If

        If DK = "K" Then
            rowviewUtama("D_K") = "K"
            rowviewUtama("Jumlah_Debet") = 0
            rowviewUtama("Jumlah_Kredit") = JumlahTransaksi
        End If

        ' Hitung ulang total
        TotalDebet = 0
        TotalKredit = 0
        For Baris As Integer = 0 To JumlahBaris - 1
            Dim row = datatabelUtama.Rows(Baris)
            TotalDebet = TotalDebet + AmbilAngka(row("Jumlah_Debet"))
            TotalKredit = TotalKredit + AmbilAngka(row("Jumlah_Kredit"))
        Next

        NotifBalance()

    End Sub


    Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click

        If TotalDebet <> TotalKredit Then
            Pesan_Peringatan("Jurnal tidak dapat diposting karena ADA SELISIH." &
                   Enter2Baris & "Silakan dikoreksi kembali.")
            Return
        End If

        If Not TanyaKonfirmasi("Jurnal yang Anda posting tidak akan bisa diedit lagi." & Enter2Baris & "Yakin data sudah benar..?") Then Return

        Dim COA As String = ""
        Dim DK As String = ""
        Dim JumlahDebet As Int64 = 0
        Dim JumlahKredit As Int64 = 0
        Dim NomorID As String = ""
        Dim QueryEdit As String = ""

        AksesDatabase_Transaksi(Buka)

        For Baris As Integer = 0 To JumlahBaris - 1
            Dim row = datatabelUtama.Rows(Baris)
            NomorID = row("Nomor_ID").ToString()
            COA = row("Kode_Akun").ToString()
            DK = row("D_K").ToString()
            JumlahDebet = AmbilAngka(row("Jumlah_Debet"))
            JumlahKredit = AmbilAngka(row("Jumlah_Kredit"))

            QueryEdit = " UPDATE tbl_Transaksi SET " &
                        " COA = '" & COA & "', D_K = '" & DK & "', Jumlah_Debet = '" & JumlahDebet & "', Jumlah_Kredit = '" & JumlahKredit & "', " &
                        " Status_Approve = 1 WHERE Nomor_ID = '" & NomorID & "' "

            cmd = New OdbcCommand(QueryEdit, KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
        Next

        AksesDatabase_Transaksi(Tutup)
        Me.Close()

    End Sub


    Private Sub btn_BuangJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_BuangJurnal.Click

        If Not TanyaKonfirmasi("Jurnal yang Anda buang tidak akan terposting pada event ini." & Enter2Baris & "Yakin akan membuang..?") Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand("DELETE FROM tbl_Transaksi WHERE Nomor_JV = " & jur_NomorJV & " ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
        jur_StatusPenyimpananJurnal_Lengkap = False
        HasilPosting = "BERMASALAH"
        Me.Close()

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click

        win_ImportJurnal.TanyaBatalPostingJurnal()
        If StatusPosting = "BATAL" Then
            Me.Close()
        End If

    End Sub



    '═══════════════════════════════════════════════════════════════════════════
    ' PEMBUATAN TABEL UTAMA
    '═══════════════════════════════════════════════════════════════════════════

    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim D_K As New DataGridTextColumn
    Dim Jumlah_Debet As New DataGridTextColumn
    Dim Jumlah_Kredit As New DataGridTextColumn
    Dim Nomor_ID As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("D_K")
        datatabelUtama.Columns.Add("Jumlah_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("Nomor_ID")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 42, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 60, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, D_K, "D_K", "D/K", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Debet, "Jumlah_Debet", "Debet", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Kredit, "Jumlah_Kredit", "Kredit", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 0, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub


End Class
