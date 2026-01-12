Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives

Public Class wpfUsc_JurnalAdjusment


    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Dim KodeAkun
    Dim NamaAkun

    Dim KodeAkun_Terseleksi As String
    Dim NamaAkun_Terseleksi As String
    Dim BulanAngka_Terseleksi As Integer
    Dim KodeMataUang_Terseleksi As String
    Dim JenisAdjusment_Terseleksi As String


    Public KesesuaianJurnal As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_JurnalAdjusment.JudulForm
        pnl_FilterData.Visibility = Visibility.Collapsed
        pnl_TombolForm.Visibility = Visibility.Collapsed

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        Dim COA
        Dim NamaAkun

        'Data Tabel :
        datatabelUtama.Clear()

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT COA, Nama_Akun FROM tbl_COA WHERE Kode_Mata_Uang <> '" & KodeMataUang_IDR & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            COA = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            datatabelUtama.Rows.Add(COA, NamaAkun)
        Loop

        AksesDatabase_General(Tutup)

        TambahBaris_Manual(KodeTautanCOA_HutangUsaha_USD)
        TambahBaris_Manual(KodeTautanCOA_HutangUsaha_AUD)
        TambahBaris_Manual(KodeTautanCOA_HutangUsaha_JPY)
        TambahBaris_Manual(KodeTautanCOA_HutangUsaha_CNY)
        TambahBaris_Manual(KodeTautanCOA_HutangUsaha_EUR)
        TambahBaris_Manual(KodeTautanCOA_HutangUsaha_SGD)

        TambahBaris_Manual(KodeTautanCOA_PiutangUsaha_USD)
        TambahBaris_Manual(KodeTautanCOA_PiutangUsaha_AUD)
        TambahBaris_Manual(KodeTautanCOA_PiutangUsaha_JPY)
        TambahBaris_Manual(KodeTautanCOA_PiutangUsaha_CNY)
        TambahBaris_Manual(KodeTautanCOA_PiutangUsaha_EUR)
        TambahBaris_Manual(KodeTautanCOA_PiutangUsaha_SGD)


        BersihkanSeleksi()

    End Sub

    Sub TambahBaris_Manual(COA As String)
        datatabelUtama.Rows.Add(COA, AmbilValue_NamaAkun(COA))
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

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

        KodeAkun_Terseleksi = rowviewUtama("Kode_Akun")
        NamaAkun_Terseleksi = rowviewUtama("Nama_Akun")
        KodeMataUang_Terseleksi = AmbilValue_KodeMataUang_BerdasarkanCOA(KodeAkun_Terseleksi)

        Dim PenyesuaianIndexBulan As Integer = 1 ' Ini harus disesuaikan dengan jumlah kolom pada tabel.

        BulanAngka_Terseleksi = KolomTerseleksi.DisplayIndex - PenyesuaianIndexBulan

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If BarisTerseleksi < 0 Then Return
        If BulanAngka_Terseleksi < 1 Then Return
        If BulanAngka_Terseleksi > 12 Then Return
        Dim Pesan As String = "Anda akan melakukan Adjusment untuk," & Enter1Baris &
            "Akun : " & NamaAkun_Terseleksi & " (" & KodeAkun_Terseleksi & ")" & Enter1Baris &
            "Bulan : " & KonversiAngkaKeBulanString(BulanAngka_Terseleksi) & Enter2Baris &
            "Lanjutkan proses?" & Enter1Baris &
            ""
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        InputJurnalAdjusment()

    End Sub


    Sub InputJurnalAdjusment()

        InputJurnalAdjusmentAkhirBulan_Forex(KodeAkun_Terseleksi, BulanAngka_Terseleksi)

    End Sub


    Sub LihatJurnalAdjusment()

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

    Dim Kode_Akun As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Januari_ As New DataGridTextColumn
    Dim Februari_ As New DataGridTextColumn
    Dim Maret_ As New DataGridTextColumn
    Dim April_ As New DataGridTextColumn
    Dim Mei_ As New DataGridTextColumn
    Dim Juni_ As New DataGridTextColumn
    Dim Juli_ As New DataGridTextColumn
    Dim Agustus_ As New DataGridTextColumn
    Dim September_ As New DataGridTextColumn
    Dim Oktober_ As New DataGridTextColumn
    Dim Nopember_ As New DataGridTextColumn
    Dim Desember_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Januari_")
        datatabelUtama.Columns.Add("Februari_")
        datatabelUtama.Columns.Add("Maret_")
        datatabelUtama.Columns.Add("April_")
        datatabelUtama.Columns.Add("Mei_")
        datatabelUtama.Columns.Add("Juni_")
        datatabelUtama.Columns.Add("Juli_")
        datatabelUtama.Columns.Add("Agustus_")
        datatabelUtama.Columns.Add("September_")
        datatabelUtama.Columns.Add("Oktober_")
        datatabelUtama.Columns.Add("Nopember_")
        datatabelUtama.Columns.Add("Desember_")

        Dim LebarKolomBulan As Integer = 72

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 222, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Januari_, "Januari_", "Januari", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Februari_, "Februari_", "Februari", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Maret_, "Maret_", "Maret", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, April_, "April_", "April", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mei_, "Mei_", "Mei", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Juni_, "Juni_", "Juni", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Juli_, "Juli_", "Juli", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Agustus_, "Agustus_", "Agustus", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, September_, "September_", "September", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Oktober_, "Oktober_", "Oktober", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nopember_, "Nopember_", "Nopember", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Desember_, "Desember_", "Desember", LebarKolomBulan, FormatString, TengahTengah, KunciUrut, Terlihat)

    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
