Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfWin_DaftarSaham

    Dim SahamKe
    Dim HargaPerlembar


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TampilkanData()
    End Sub


    Sub TampilkanData()

        'Style Tabel :
        Terabas()
        datatabelUtama.Rows.Clear()

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DaftarSaham ORDER BY Saham_Ke ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            SahamKe = dr.Item("Saham_Ke")
            HargaPerlembar = dr.Item("Harga")
            datatabelUtama.Rows.Add(SahamKe, HargaPerlembar)
        Loop
        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

    End Sub


    Sub BersihkanSeleksi()
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        ResetForm()
    End Sub



    Sub ResetForm()
        txt_SahamKe.Text = SahamKe + 1
        txt_HargaPerlembar.Text = Kosongan
        btn_Terbitkan.IsEnabled = False
    End Sub


    Sub LogikaTombolTerbitkan()
        If HargaPerlembar = 0 Then
            btn_Terbitkan.IsEnabled = False
        Else
            btn_Terbitkan.IsEnabled = True
        End If
    End Sub

    Private Sub txt_SahamKe_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SahamKe.TextChanged
        SahamKe = AmbilAngka(txt_SahamKe.Text)
        LogikaTombolTerbitkan()
    End Sub

    Private Sub txt_HargaPerlembar_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_HargaPerlembar.TextChanged
        HargaPerlembar = AmbilAngka(txt_HargaPerlembar.Text)
        LogikaTombolTerbitkan()
        PemecahRibuanUntukTextBox_WPF(txt_HargaPerlembar)
    End Sub


    Private Sub btn_Terbitkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Terbitkan.Click

        Dim Pesan As String =
            "Silakan periksa angka dengan seksama!" & Enter2Baris &
            "Setelah diterbitkan, data saham tidak dapat diedit dan dihapus." & Enter2Baris &
            "Lanjutkan?"
        If Not TanyaKonfirmasi(Pesan) Then Return

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" INSERT INTO tbl_DaftarSaham " &
                              " VALUES ( " & SahamKe & ", " & HargaPerlembar & " ) ",
                              KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()
        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase Then
            TampilkanData()
        Else
            PesanPeringatan("Saham gagal diterbitkan!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If


    End Sub



    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub




    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer


    Dim Saham_Ke As New DataGridTextColumn
    Dim Harga_Perlembar As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Clear()
        datatabelUtama.Columns.Add("Saham_Ke")
        datatabelUtama.Columns.Add("Harga_Perlembar", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saham_Ke, "Saham_Ke", "Saham Ke-", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Perlembar, "Harga_Perlembar", "Harga Perlembar", 123, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
        txt_SahamKe.IsReadOnly = True
    End Sub

End Class
