Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm


Public Class wpfUsc_DataProject

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Dim NomorUrut
    Dim NomorID
    Dim KodeProject
    Dim NamaProject
    Dim NomorPO
    Dim KodeCustomer
    Dim NamaCustomer
    Dim NilaiProject
    Dim Keterangan
    Dim Status

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim KodeProject_Terseleksi
    Dim NamaProject_Terseleksi
    Dim NomorPO_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim NilaiProject_Terseleksi
    Dim Keterangan_Terseleksi
    Dim Status_Terseleksi


    Public KesesuaianJurnal As Boolean

    ' Flag untuk mencegah multiple loading bersamaan
    Private SedangMemuatData As Boolean = False

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_DataProject.JudulForm
        pnl_FilterData.Visibility = Visibility.Collapsed

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    ''' <summary>
    ''' Menampilkan data dengan async pattern.
    ''' Baris data ditampilkan satu per satu seperti sistem lama,
    ''' dengan loading window yang tetap responsive (animasi berputar).
    ''' </summary>
    Async Sub TampilkanDataAsync()

        ' Guard clause
        If SedangMemuatData Then Return

        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)

        ' Beri waktu UI untuk menampilkan loading window
        Await Task.Delay(50)

        Try
            KesesuaianJurnal = True

            'Data Tabel :
            datatabelUtama.Clear()
            NomorUrut = 0

            AksesDatabase_General(Buka)

            cmd = New OdbcCommand(" SELECT * FROM tbl_DataProject ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()

            Do While dr.Read
                NomorUrut += 1
                NomorID = dr.Item("Nomor_ID")
                KodeProject = dr.Item("Kode_Project")
                NamaProject = dr.Item("Nama_Project")
                NomorPO = dr.Item("Nomor_PO")
                KodeCustomer = dr.Item("Kode_Customer")
                NamaCustomer = dr.Item("Nama_Customer")
                NilaiProject = dr.Item("Nilai_Project")
                Keterangan = dr.Item("Keterangan")
                Status = dr.Item("Status")
                datatabelUtama.Rows.Add(NomorUrut, NomorID, KodeProject, NamaProject, NomorPO, KodeCustomer, NamaCustomer, NilaiProject, Keterangan, Status)

                ' Yield untuk memberi kesempatan UI refresh (animasi loading + tampil baris)
                Await Task.Yield()
            Loop

            AksesDatabase_General(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_DataProject")

        Finally
            BersihkanSeleksi()
            ' Enable UI dan tutup loading
            KetersediaanMenuHalaman(pnl_Halaman, True)
            SedangMemuatData = False
        End Try

    End Sub


    ''' <summary>
    ''' Wrapper untuk backward compatibility.
    ''' Dipanggil dari form lain yang masih menggunakan nama lama.
    ''' </summary>
    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub


    ''' <summary>
    ''' Membersihkan seleksi tanpa mengubah state loading.
    ''' Digunakan oleh TampilkanDataAsync() karena loading dihandle secara terpisah.
    ''' </summary>
    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
    End Sub

    ''' <summary>
    ''' Membersihkan seleksi dan mengaktifkan kembali UI.
    ''' Digunakan untuk backward compatibility dengan kode lama.
    ''' </summary>
    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True, False)
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputDataProject = New wpfWin_InputDataProject 'baris baru
        win_InputDataProject.ResetForm()
        win_InputDataProject.FungsiForm = FungsiForm_TAMBAH
        win_InputDataProject.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        win_InputDataProject = New wpfWin_InputDataProject 'baris baru
        win_InputDataProject.ResetForm()
        win_InputDataProject.FungsiForm = FungsiForm_EDIT
        win_InputDataProject.txt_KodeProject.Text = KodeProject_Terseleksi
        win_InputDataProject.txt_NamaProject.Text = NamaProject_Terseleksi
        win_InputDataProject.txt_NomorPO.Text = NomorPO_Terseleksi
        win_InputDataProject.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
        win_InputDataProject.txt_NamaCustomer.Text = NamaCustomer_Terseleksi
        win_InputDataProject.txt_NilaiProject.Text = NilaiProject_Terseleksi
        win_InputDataProject.txt_Keterangan.Text = Keterangan_Terseleksi
        win_InputDataProject.cmb_Status.SelectedValue = Status_Terseleksi
        win_InputDataProject.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_DataProject " &
                              " WHERE Kode_Project = '" & KodeProject_Terseleksi & "' ", KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()

        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

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
        NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID"))
        KodeProject_Terseleksi = rowviewUtama("Kode_Project")
        NamaProject_Terseleksi = rowviewUtama("Nama_Project")
        NomorPO_Terseleksi = rowviewUtama("Nomor_PO")
        KodeCustomer_Terseleksi = rowviewUtama("Kode_Customer")
        NamaCustomer_Terseleksi = rowviewUtama("Nama_Customer")
        NilaiProject_Terseleksi = AmbilAngka(rowviewUtama("Nilai_Project"))
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        Status_Terseleksi = rowviewUtama("Status_")

        If NomorID_Terseleksi >= 0 Then
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
    Dim Nomor_ID As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Nama_Project As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Kode_Customer As New DataGridTextColumn
    Dim Nama_Customer As New DataGridTextColumn
    Dim Nilai_Project As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Status_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Nama_Project")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Kode_Customer")
        datatabelUtama.Columns.Add("Nama_Customer")
        datatabelUtama.Columns.Add("Nilai_Project", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Status_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 39, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 165, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Project, "Nama_Project", "Nama Project", 222, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO / SPK", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Customer, "Kode_Customer", "Kode Customer", 87, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Customer, "Nama_Customer", "Nama Customer", 222, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nilai_Project, "Nilai_Project", "Nilai Project", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_, "Status_", "Status", 51, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
