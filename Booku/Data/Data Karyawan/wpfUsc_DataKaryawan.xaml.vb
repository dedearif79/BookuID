Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives


Public Class wpfUsc_DataKaryawan

    Public StatusAktif As Boolean = False

    Dim NomorUrut
    Dim TanggalRegistrasi
    Dim NomorIDKaryawan
    Dim NIK
    Dim NamaKaryawan
    Dim Jabatan
    Dim RekeningBank
    Dim AtasNama
    Dim Catatan
    Dim StatusAktifKaryawan

    Dim NomorUrut_Terseleksi
    Dim TanggalRegistrasi_Terseleksi
    Dim NomorIDKaryawan_Terseleksi
    Dim NIK_Terseleksi
    Dim NamaKaryawan_Terseleksi
    Dim Jabatan_Terseleksi
    Dim RekeningBank_Terseleksi
    Dim AtasNama_Terseleksi
    Dim Catatan_Terseleksi
    Dim StatusAktifKaryawan_Terseleksi


    Public KesesuaianJurnal As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_DataKaryawan.JudulForm
        KontenCombo_FilterStatus()

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub


    Sub KontenCombo_FilterStatus()
        cmb_FilterStatus.Items.Clear()
        cmb_FilterStatus.Items.Add(Pilihan_Semua)
        cmb_FilterStatus.Items.Add("Aktif")
        cmb_FilterStatus.Items.Add("Non-Aktif")
        cmb_FilterStatus.SelectedIndex = 0
    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        AksesDatabase_General(Buka)

        'Data Tabel :
        datatabelUtama.Clear()
        NomorUrut = 0

        Dim FilterStatus = ""
        If cmb_FilterStatus.SelectedItem = Pilihan_Semua Then FilterStatus = ""
        If cmb_FilterStatus.SelectedItem = "Aktif" Then FilterStatus = " WHERE Status_Aktif = '1' "
        If cmb_FilterStatus.SelectedItem = "Non-Aktif" Then FilterStatus = " WHERE Status_Aktif = '0' "

        cmd = New OdbcCommand(" SELECT * FROM tbl_DataKaryawan " & FilterStatus, KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            TanggalRegistrasi = TanggalFormatTampilan(dr.Item("Tanggal_Registrasi"))
            NomorIDKaryawan = dr.Item("Nomor_ID_Karyawan")
            NIK = dr.Item("NIK")
            NamaKaryawan = dr.Item("Nama_Karyawan")
            Jabatan = dr.Item("Jabatan")
            RekeningBank = dr.Item("Rekening_Bank")
            AtasNama = dr.Item("Atas_Nama")
            Catatan = dr.Item("Catatan")
            StatusAktifKaryawan = dr.Item("Status_Aktif")
            datatabelUtama.Rows.Add(NomorUrut, TanggalRegistrasi, NomorIDKaryawan, NIK, NamaKaryawan, Jabatan, RekeningBank, AtasNama, Catatan, StatusAktifKaryawan)
        Loop

        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

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


    Private Sub cmb_FilterStatus_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_FilterStatus.SelectionChanged
        If ProsesLoadingForm Then Return
        RefreshTampilanData()
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputDataKaryawan = New wpfWin_InputDataKaryawan
        win_InputDataKaryawan.ResetForm()
        win_InputDataKaryawan.FungsiForm = FungsiForm_TAMBAH
        win_InputDataKaryawan.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        win_InputDataKaryawan = New wpfWin_InputDataKaryawan
        win_InputDataKaryawan.ResetForm()
        win_InputDataKaryawan.FungsiForm = FungsiForm_EDIT
        win_InputDataKaryawan.dtp_TanggalRegistrasi.SelectedDate = TanggalFormatWPF(TanggalRegistrasi_Terseleksi)
        win_InputDataKaryawan.txt_NomorIDKaryawan.Text = NomorIDKaryawan_Terseleksi
        win_InputDataKaryawan.txt_NIK.Text = NIK_Terseleksi
        win_InputDataKaryawan.txt_NamaKaryawan.Text = NamaKaryawan_Terseleksi
        win_InputDataKaryawan.txt_Jabatan.Text = Jabatan_Terseleksi
        win_InputDataKaryawan.txt_RekeningBank.Text = RekeningBank_Terseleksi
        win_InputDataKaryawan.txt_AtasNama.Text = AtasNama_Terseleksi
        win_InputDataKaryawan.txt_Catatan.Text = Catatan_Terseleksi
        If StatusAktifKaryawan_Terseleksi = 1 Then win_InputDataKaryawan.chk_StatusAktif.IsChecked = True
        If StatusAktifKaryawan_Terseleksi = 0 Then win_InputDataKaryawan.chk_StatusAktif.IsChecked = False
        win_InputDataKaryawan.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_DataKaryawan " &
                              " WHERE Nomor_ID_Karyawan = '" & NomorIDKaryawan_Terseleksi & "' ", KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()

        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
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

        NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        If NomorUrut_Terseleksi = 0 Then
            BersihkanSeleksi()
            Return
        End If
        TanggalRegistrasi_Terseleksi = rowviewUtama("Tanggal_Registrasi")
        NomorIDKaryawan_Terseleksi = rowviewUtama("Nomor_ID_Karyawan")
        NIK_Terseleksi = rowviewUtama("NIK_")
        NamaKaryawan_Terseleksi = rowviewUtama("Nama_Karyawan")
        Jabatan_Terseleksi = rowviewUtama("Jabatan_")
        RekeningBank_Terseleksi = rowviewUtama("Rekening_Bank")
        AtasNama_Terseleksi = rowviewUtama("Atas_Nama")
        Catatan_Terseleksi = rowviewUtama("Catatan_")
        StatusAktifKaryawan_Terseleksi = AmbilAngka(rowviewUtama("Status_Aktif"))

        If NomorIDKaryawan_Terseleksi <> Kosongan Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If BarisTerseleksi < 0 Then Return
        btn_Edit_Click(sender, e)
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
    Dim Tanggal_Registrasi As New DataGridTextColumn
    Dim Nomor_ID_Karyawan As New DataGridTextColumn
    Dim NIK_ As New DataGridTextColumn
    Dim Nama_Karyawan As New DataGridTextColumn
    Dim Jabatan_ As New DataGridTextColumn
    Dim Rekening_Bank As New DataGridTextColumn
    Dim Atas_Nama As New DataGridTextColumn
    Dim Catatan_ As New DataGridTextColumn
    Dim Status_Aktif As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Tanggal_Registrasi")
        datatabelUtama.Columns.Add("Nomor_ID_Karyawan")
        datatabelUtama.Columns.Add("NIK_")
        datatabelUtama.Columns.Add("Nama_Karyawan")
        datatabelUtama.Columns.Add("Jabatan_")
        datatabelUtama.Columns.Add("Rekening_Bank")
        datatabelUtama.Columns.Add("Atas_Nama")
        datatabelUtama.Columns.Add("Catatan_")
        datatabelUtama.Columns.Add("Status_Aktif")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 39, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Registrasi, "Tanggal_Registrasi", "Tgl. Registrasi", 75, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID_Karyawan, "Nomor_ID_Karyawan", "Nomor ID", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NIK_, "NIK_", "NIK", 130, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Karyawan, "Nama_Karyawan", "Nama Karyawan", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jabatan_, "Jabatan_", "Jabatan", 135, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Rekening_Bank, "Rekening_Bank", "Rekening Bank", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Atas_Nama, "Atas_Nama", "Atas Nama", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Catatan_, "Catatan_", "Catatan", 250, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_Aktif, "Status_Aktif", "Aktif", 45, FormatAngka, TengahTengah, KunciUrut, Tersembunyi)

    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        StatusAktif = False
    End Sub

End Class
