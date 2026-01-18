Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks


Public Class wpfUsc_BukuBankGaransi

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    ' Flag untuk mencegah multiple loading bersamaan
    Private SedangMemuatData As Boolean = False

    Dim NomorUrut
    Dim NomorID
    Dim NomorBPBG
    Dim NomorKontrak
    Dim TanggalTransaksi
    Dim NamaBank
    Dim Keperluan
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahTransaksi
    Dim BiayaProvisi
    Dim TanggalPencairan
    Dim Keterangan
    Dim NomorJV_Transaksi
    Dim NomorJV_Pencairan
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPBG_Terseleksi
    Dim NomorKontrak_Terseleksi
    Dim TanggalTransaksi_Terseleksi
    Dim NamaBank_Terseleksi
    Dim Keperluan_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim BiayaProvisi_Terseleksi
    Dim TanggalPencairan_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Transaksi_Terseleksi
    Dim NomorJV_Pencairan_Terseleksi
    Dim User_Terseleksi


    Public KesesuaianJurnal As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_BukuBankGaransi.JudulForm

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Async Sub TampilkanDataAsync()

        ' Guard clause
        If SedangMemuatData Then Return
        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            KesesuaianJurnal = True

            AksesDatabase_Transaksi(Buka)

            'Data Tabel :
            datatabelUtama.Clear()
            NomorUrut = 0

            cmd = New OdbcCommand(" SELECT * FROM tbl_BankGaransi ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorID = dr.Item("Nomor_ID")
                NomorBPBG = dr.Item("Nomor_BPBG")
                NomorKontrak = dr.Item("Nomor_Kontrak")
                TanggalTransaksi = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
                NamaBank = dr.Item("Nama_Bank")
                Keperluan = dr.Item("Keperluan")
                KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
                NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
                JumlahTransaksi = dr.Item("Jumlah_Transaksi")
                BiayaProvisi = dr.Item("Biaya_Provisi")
                TanggalPencairan = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
                If TanggalPencairan = TanggalKosong Then TanggalPencairan = StripKosong
                Keterangan = dr.Item("Keterangan")
                NomorJV_Transaksi = dr.Item("Nomor_JV_Transaksi")
                NomorJV_Pencairan = dr.Item("Nomor_JV_Pencairan")
                User = dr.Item("User")
                datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPBG, NomorKontrak, TanggalTransaksi, NamaBank, Keperluan,
                                        KodeLawanTransaksi, NamaLawanTransaksi, JumlahTransaksi, BiayaProvisi,
                                        TanggalPencairan, Keterangan, NomorJV_Transaksi, NomorJV_Pencairan, User)
                Await Task.Yield()
            Loop

            AksesDatabase_Transaksi(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuBankGaransi")
            SedangMemuatData = False

        Finally
            BersihkanSeleksi_SetelahLoading()
        End Try

    End Sub

    ' Wrapper untuk backward compatibility
    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnalTransaksi.IsEnabled = False
        btn_LihatJurnalPencairan.IsEnabled = False
        SedangMemuatData = False
    End Sub

    ' Wrapper: reset seleksi + enable UI (untuk backward compatibility)
    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_LihatJurnalTransaksi_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnalTransaksi.Click
        LihatJurnal(NomorJV_Transaksi_Terseleksi)
    End Sub

    Private Sub btn_LihatJurnalPencairan_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnalPencairan.Click
        LihatJurnal(NomorJV_Pencairan_Terseleksi)
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataGridKeEXCEL(datagridUtama)
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputBankGaransi = New wpfWin_InputBankGaransi
        win_InputBankGaransi.ResetForm()
        win_InputBankGaransi.FungsiForm = FungsiForm_TAMBAH
        win_InputBankGaransi.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        win_InputBankGaransi = New wpfWin_InputBankGaransi
        win_InputBankGaransi.ResetForm()
        win_InputBankGaransi.FungsiForm = FungsiForm_EDIT
        win_InputBankGaransi.NomorID = NomorID_Terseleksi
        win_InputBankGaransi.NomorJV_Transaksi = NomorJV_Transaksi_Terseleksi
        win_InputBankGaransi.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(TanggalTransaksi_Terseleksi)
        win_InputBankGaransi.txt_NomorBPBG.Text = NomorBPBG_Terseleksi
        win_InputBankGaransi.txt_NomorKontrak.Text = NomorKontrak_Terseleksi
        win_InputBankGaransi.txt_NamaBank.Text = NamaBank_Terseleksi
        win_InputBankGaransi.txt_Keperluan.Text = Keperluan_Terseleksi
        win_InputBankGaransi.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        win_InputBankGaransi.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Terseleksi
        win_InputBankGaransi.txt_JumlahTransaksi.Text = JumlahTransaksi_Terseleksi
        win_InputBankGaransi.txt_BiayaProvisi.Text = BiayaProvisi_Terseleksi
        win_InputBankGaransi.txt_Keterangan.Text = Keterangan_Terseleksi
        win_InputBankGaransi.ShowDialog()

    End Sub

    Private Sub btn_Cairkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Cairkan.Click
        FiturBelumBisaDigunakan()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_BankGaransi " &
                              " WHERE Nomor_BPBG = '" & NomorBPBG_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

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
        NomorID_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_ID"))
        NomorBPBG_Terseleksi = rowviewUtama("Nomor_BPBG")
        NomorKontrak_Terseleksi = rowviewUtama("Nomor_Kontrak")
        TanggalTransaksi_Terseleksi = rowviewUtama("Tanggal_Transaksi")
        NamaBank_Terseleksi = rowviewUtama("Nama_Bank")
        Keperluan_Terseleksi = rowviewUtama("Keperluan_")
        KodeLawanTransaksi_Terseleksi = rowviewUtama("Kode_Lawan_Transaksi")
        NamaLawanTransaksi_Terseleksi = rowviewUtama("Nama_Lawan_Transaksi")
        JumlahTransaksi_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Transaksi"))
        BiayaProvisi_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Biaya_Provisi"))
        TanggalPencairan_Terseleksi = rowviewUtama("Tanggal_Pencairan")
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        NomorJV_Transaksi_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV_Transaksi"))
        NomorJV_Pencairan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV_Pencairan"))
        User_Terseleksi = rowviewUtama("User_")

        If NomorID_Terseleksi > 0 Then
            If NomorJV_Transaksi_Terseleksi > 0 Then
                btn_LihatJurnalTransaksi.IsEnabled = True
                btn_Edit.IsEnabled = False
                btn_Hapus.IsEnabled = False
            Else
                btn_LihatJurnalTransaksi.IsEnabled = False
                btn_Edit.IsEnabled = True
                btn_Hapus.IsEnabled = True
            End If
            If NomorJV_Pencairan_Terseleksi > 0 Then
                btn_LihatJurnalPencairan.IsEnabled = True
            Else
                btn_LihatJurnalPencairan.IsEnabled = False
            End If
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
    Dim Nomor_BPBG As New DataGridTextColumn
    Dim Nomor_Kontrak As New DataGridTextColumn
    Dim Tanggal_Transaksi As New DataGridTextColumn
    Dim Nama_Bank As New DataGridTextColumn
    Dim Keperluan_ As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim Jumlah_Transaksi As New DataGridTextColumn
    Dim Biaya_Provisi As New DataGridTextColumn
    Dim Tanggal_Pencairan As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV_Transaksi As New DataGridTextColumn
    Dim Nomor_JV_Pencairan As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPBG")
        datatabelUtama.Columns.Add("Nomor_Kontrak")
        datatabelUtama.Columns.Add("Tanggal_Transaksi")
        datatabelUtama.Columns.Add("Nama_Bank")
        datatabelUtama.Columns.Add("Keperluan_")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Jumlah_Transaksi", GetType(Int64))
        datatabelUtama.Columns.Add("Biaya_Provisi", GetType(Int64))
        datatabelUtama.Columns.Add("Tanggal_Pencairan")
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV_Transaksi")
        datatabelUtama.Columns.Add("Nomor_JV_Pencairan")
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 60, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPBG, "Nomor_BPBG", "Nomor BPBG", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Kontrak, "Nomor_Kontrak", "Nomor Kontrak", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Transaksi, "Tanggal_Transaksi", "Tgl. Transaksi", 75, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Bank, "Nama_Bank", "Nama Bank", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keperluan_, "Keperluan_", "Keperluan", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode", 80, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Lawan Transaksi", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Transaksi, "Jumlah_Transaksi", "Jumlah", 100, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Biaya_Provisi, "Biaya_Provisi", "Biaya Provisi", 81, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pencairan, "Tanggal_Pencairan", "Tgl. Pencairan", 75, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV_Transaksi, "Nomor_JV_Transaksi", "JV Transaksi", 80, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV_Pencairan, "Nomor_JV_Pencairan", "JV Pencairan", 80, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 80, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
