Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input


Public Class wpfUsc_DataLawanTransaksi

    Public StatusAktif
    Public JudulForm
    Public KesesuaianJurnal

    Dim NomorUrut
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UMKM
    Dim PemegangSaham
    Dim Afiliasi
    Dim Supplier
    Dim Customer
    Dim Keuangan
    Dim PKP
    Dim PemotongPPh
    Dim PJK
    Dim NPWP
    Dim JenisWP
    Dim LokasiWP
    Dim Alamat
    Dim Email
    Dim PIC
    Dim RekeningBank
    Dim AtasNama

    Dim NomorUrut_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True


        StatusAktif = True

        lbl_JudulForm.Text = frm_DataLawanTransaksi.JudulForm


        ProsesLoadingForm = False

        RefreshTampilanData()

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

        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read
            KodeLawanTransaksi = dr.Item("Kode_Mitra")
            NamaLawanTransaksi = dr.Item("Nama_Mitra")
            AmbilValueChecked("UMKM", UMKM)
            AmbilValueChecked("Pemegang_Saham", PemegangSaham)
            AmbilValueChecked("Afiliasi", Afiliasi)
            AmbilValueChecked("Supplier", Supplier)
            AmbilValueChecked("Customer", Customer)
            AmbilValueChecked("Keuangan", Keuangan)
            AmbilValueChecked("PKP", PKP)
            AmbilValueChecked("Pemotong_PPh", PemotongPPh)
            AmbilValueChecked("PJK", PJK)
            NPWP = dr.Item("NPWP")
            JenisWP = dr.Item("Jenis_WP")
            LokasiWP = dr.Item("Lokasi_WP")
            Alamat = PenghapusEnter(dr.Item("Alamat"))
            Email = dr.Item("Email")
            PIC = dr.Item("PIC")
            RekeningBank = dr.Item("Rekening_Bank")
            AtasNama = dr.Item("Atas_Nama")
            TambahBaris()
        Loop
        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

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
        datatabelUtama.Rows.Add(NomorUrut, KodeLawanTransaksi, NamaLawanTransaksi,
                                UMKM, PemegangSaham, Afiliasi, Supplier, Customer, Keuangan, PKP, PemotongPPh, PJK,
                                NPWP, JenisWP, LokasiWP, Alamat, Email, PIC, RekeningBank, AtasNama)
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputLawanTransaksi = New wpfWin_InputLawanTransaksi
        win_InputLawanTransaksi.ResetForm()
        win_InputLawanTransaksi.FungsiForm = FungsiForm_TAMBAH
        win_InputLawanTransaksi.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputLawanTransaksi = New wpfWin_InputLawanTransaksi
        win_InputLawanTransaksi.ResetForm()
        win_InputLawanTransaksi.FungsiForm = FungsiForm_EDIT
        win_InputLawanTransaksi.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        win_InputLawanTransaksi.ShowDialog()
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
        KodeLawanTransaksi_Terseleksi = rowviewUtama("Kode_Lawan_Transaksi")

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
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim UMKM_ As New DataGridCheckBoxColumn
    Dim Pemegang_Saham As New DataGridCheckBoxColumn
    Dim Afiliasi_ As New DataGridCheckBoxColumn
    Dim Supplier_ As New DataGridCheckBoxColumn
    Dim Customer_ As New DataGridCheckBoxColumn
    Dim Keuangan_ As New DataGridCheckBoxColumn
    Dim PKP_ As New DataGridCheckBoxColumn
    Dim Pemotong_PPh As New DataGridCheckBoxColumn
    Dim PJK_ As New DataGridCheckBoxColumn
    Dim NPWP_ As New DataGridTextColumn
    Dim Jenis_WP As New DataGridTextColumn
    Dim Lokasi_WP As New DataGridTextColumn
    Dim Alamat_ As New DataGridTextColumn
    Dim Email_ As New DataGridTextColumn
    Dim PIC_ As New DataGridTextColumn
    Dim Rekening_Bank As New DataGridTextColumn
    Dim Atas_Nama As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("UMKM_", GetType(Boolean))
        datatabelUtama.Columns.Add("Pemegang_Saham", GetType(Boolean))
        datatabelUtama.Columns.Add("Afiliasi_", GetType(Boolean))
        datatabelUtama.Columns.Add("Supplier_", GetType(Boolean))
        datatabelUtama.Columns.Add("Customer_", GetType(Boolean))
        datatabelUtama.Columns.Add("Keuangan_", GetType(Boolean))
        datatabelUtama.Columns.Add("PKP_", GetType(Boolean))
        datatabelUtama.Columns.Add("Pemotong_PPh", GetType(Boolean))
        datatabelUtama.Columns.Add("PJK_", GetType(Boolean))
        datatabelUtama.Columns.Add("NPWP_")
        datatabelUtama.Columns.Add("Jenis_WP")
        datatabelUtama.Columns.Add("Lokasi_WP")
        datatabelUtama.Columns.Add("Alamat_")
        datatabelUtama.Columns.Add("Email_")
        datatabelUtama.Columns.Add("PIC_")
        datatabelUtama.Columns.Add("Rekening_Bank")
        datatabelUtama.Columns.Add("Atas_Nama")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Nama" & Enter1Baris & "Lawan Transaksi", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, UMKM_, "UMKM_", "UMKM", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, Pemegang_Saham, "Pemegang_Saham", "Pem. Saham", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, Afiliasi_, "Afiliasi_", "Afiliasi", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, Supplier_, "Supplier_", "Supplier", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, Customer_, "Customer_", "Customer", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, Keuangan_, "Keuangan_", "Lemb. Keu", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, PKP_, "PKP_", "PKP", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, Pemotong_PPh, "Pemotong_PPh", "Pmtg. PPh", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, PJK_, "PJK_", "PJK", 63, FormatString, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NPWP_, "NPWP_", "NPWP", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_WP, "Jenis_WP", "Jenis WP", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Lokasi_WP, "Lokasi_WP", "Lokasi WP", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Alamat_, "Alamat_", "Alamat", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Email_, "Email_", "Email", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PIC_, "PIC_", "PIC", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Rekening_Bank, "Rekening_Bank", "Rekening Bank", 162, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Atas_Nama, "Atas_Nama", "Atas Nama", 123, FormatString, KiriTengah, KunciUrut, Terlihat)

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
        StatusAktif = False
    End Sub

End Class
