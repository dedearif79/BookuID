Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm
Imports MySql.Data.MySqlClient

Public Class wpfUsc_ManajemenClient

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False

    Dim NomorUrut

    Dim VersiApp
    Dim ApdetApp
    Dim urlPaketBooku
    Dim urlPaketInstaller
    Dim urlPaketUpdater
    Dim NamaFolderTempPaketBooku
    Dim NamaFolderTempPaketInstaller
    Dim NamaFolderTempPaketUpdater
    Dim NamaFileZipPaketBooku
    Dim NamaFileZipPaketInstaller
    Dim NamaFileZipPaketUpdater
    Dim NamaFileExeInstaller
    Dim NamaFileExeUpdater

    Dim AdaPerubahanInfoCustomer As Boolean

    Dim client_SerialNumber
    Dim client_ID
    Dim client_VersiApp
    Dim client_ApdetApp
    Dim client_NamaPerusahaan
    Dim client_PIC
    Dim client_TanggalExpire
    Dim client_StatusUpdate

    Dim PilihanStatusUpdate As String
    Dim StatusUpdate_Update = "Update"
    Dim StatusUpdate_Belum = "Belum"

    Dim JumlahClient
    Dim JumlahClientSudahUpdate
    Dim JumlahClientBelumUpdate

    Dim NomorUrut_Terseleksi
    Dim client_SerialNumber_Terseleksi
    Dim client_ID_Terseleksi
    Dim client_NamaPerusahaan_Terseleksi

    Dim FilterData As String
    Dim FilterStatusUpdate As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True
        AdaPerubahanInfoCustomer = False

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_ManajemenClient.JudulForm
        pnl_FilterData.Visibility = Visibility.Collapsed
        pnl_FilterData.Visibility = Visibility.Visible

        RefreshTampilanData()

        btn_SimpanPerubahanInfoCustomer.IsEnabled = False

        lbl_TotalTabel.Text = "Total Baris"

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_StatusUpdate()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Public EksekusiTampilanData As Boolean

    Async Sub TampilkanDataAsync()

        ' Guard clause
        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            'Data Tabel :
            datatabelUtama.Clear()
            NomorUrut = 0
            JumlahClientSudahUpdate = 0
            JumlahClientBelumUpdate = 0
            Terabas()

            'Filter Update :
            Select Case PilihanStatusUpdate
                Case Pilihan_Semua
                    FilterStatusUpdate = Spasi1
                Case StatusUpdate_Update
                    FilterStatusUpdate = " AND Versi_App = '" & VersiBooku_SisiPublic & "' AND Apdet_App = '" & ApdetBooku_SisiPublic & "' "
                Case StatusUpdate_Belum
                    FilterStatusUpdate = " AND ( Versi_App < '" & VersiBooku_SisiPublic & "' OR Apdet_App < '" & ApdetBooku_SisiPublic & "' ) "
            End Select

            FilterData = FilterStatusUpdate

            BukaDatabasePublic()

            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_customer WHERE ID_Customer <> 'X' " & FilterData, KoneksiDatabasePublic)
            drPublic_ExecuteReader()
            If Not StatusKoneksiDatabase Then Return

            Do While drPublic.Read

                NomorUrut += 1
                client_SerialNumber = drPublic.Item("Nomor_Seri_Produk")
                client_ID = drPublic.Item("ID_Customer")
                client_NamaPerusahaan = drPublic.Item("Nama_Perusahaan")
                client_VersiApp = drPublic.Item("Versi_App")
                client_ApdetApp = drPublic.Item("Apdet_App")
                client_PIC = drPublic.Item("PIC")
                client_TanggalExpire = TanggalFormatTampilan(drPublic.Item("Expire"))
                If client_VersiApp = VersiBooku_SisiPublic And client_ApdetApp = ApdetBooku_SisiPublic Then
                    client_StatusUpdate = StatusUpdate_Update
                    JumlahClientSudahUpdate += 1
                Else
                    client_StatusUpdate = StatusUpdate_Belum
                    JumlahClientBelumUpdate += 1
                End If

                datatabelUtama.Rows.Add(NomorUrut, client_SerialNumber, client_ID, client_NamaPerusahaan, client_VersiApp, client_ApdetApp,
                                        client_PIC, client_TanggalExpire, client_StatusUpdate)
                Terabas()

            Loop

            TutupDatabasePublic()

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_ManajemenClient")
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
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        JumlahClient = JumlahBaris
        pnl_CRUD.IsEnabled = False
        VisibilitasInfoCustomer(False)
        txt_SudahUpdate.Text = JumlahClientSudahUpdate
        txt_BelumUpdate.Text = JumlahClientBelumUpdate
        txt_TotalTabel.Text = JumlahClient
        If JumlahClientBelumUpdate > 0 Then
            lbl_BelumUpdate.Foreground = clrWarning
            txt_BelumUpdate.Foreground = clrWarning
        Else
            lbl_BelumUpdate.Foreground = clrTeksPrimer
            txt_BelumUpdate.Foreground = clrTeksPrimer
        End If
        SedangMemuatData = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub



    Sub TampilkanDataInfoCustomer()

        VisibilitasInfoCustomer(True)
        pnl_InfoCustomer.IsEnabled = False
        Terabas()

        BukaDatabasePublic()

        If StatusKoneksiDatabasePublic Then
            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_customer " &
                                         " WHERE ID_Customer = '" & client_ID_Terseleksi & "' ",
                                         KoneksiDatabasePublic)
            drPublic_ExecuteReader()
            drPublic.Read()
            txt_SerialNumber.Text = drPublic.Item("Nomor_Seri_Produk")
            txt_IDCustomer.Text = drPublic.Item("ID_Customer")
            txt_NamaPerusahaan.Text = drPublic.Item("Nama_Perusahaan")
            txt_PIC.Text = drPublic.Item("PIC")
            dtp_TanggalExpire.SelectedDate = TanggalFormatWPF(drPublic.Item("Expire"))
        Else
            ResetFormInfoCustomer()
            PesanPeringatan("Koneksi ke server gagal..!")
        End If

        TutupDatabasePublic()

        pnl_InfoCustomer.IsEnabled = True
        btn_SimpanPerubahanInfoCustomer.IsEnabled = False

    End Sub

    Sub ResetFormInfoCustomer()
        pnl_InfoCustomer.IsEnabled = False
        Terabas()
        txt_SerialNumber.Text = Kosongan
        txt_IDCustomer.Text = Kosongan
        txt_NamaPerusahaan.Text = Kosongan
        txt_PIC.Text = Kosongan
        dtp_TanggalExpire.Text = Kosongan
    End Sub

    Sub PerubahanFormInfoCustomer()
        btn_SimpanPerubahanInfoCustomer.IsEnabled = True
        AdaPerubahanInfoCustomer = True
    End Sub

    Sub VisibilitasInfoCustomer(Visibilitas As Boolean)
        If Visibilitas Then
            pnl_InfoCustomer.Visibility = Visibility.Visible
        Else
            pnl_InfoCustomer.Visibility = Visibility.Collapsed
        End If
    End Sub




    Sub KontenCombo_StatusUpdate()
        cmb_StatusUpdate.Items.Clear()
        cmb_StatusUpdate.Items.Add(Pilihan_Semua)
        cmb_StatusUpdate.Items.Add(StatusUpdate_Update)
        cmb_StatusUpdate.Items.Add(StatusUpdate_Belum)
        cmb_StatusUpdate.SelectedValue = Pilihan_Semua
    End Sub




    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        ' DEPRECATED: WhatsApp Host sudah tidak digunakan lagi
        ' Dim WAku As New wpfwin_WhatsApp
        ' WAku.ShowDialog()
        Pesan_Informasi("Fitur WhatsApp Host sudah tidak tersedia.")

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not Pesan_KonfirmasiHapus(client_NamaPerusahaan_Terseleksi) Then Return

        BukaDatabasePublic()

        TransactionBegin_Public()

        'Hapus Data Customer :
        If StatusSuntingDatabase = True Then
            cmdPublic = New MySqlCommand(" DELETE FROM tbl_customer " &
                                             " WHERE Nomor_Seri_Produk = '" & client_SerialNumber_Terseleksi & "' ", KoneksiDatabasePublic)
            cmdPublic_ExecuteNonQuery_Transaction()
        End If

        'Edit Data Produk (Kembalikan Status_Terpakai ke 0) :
        If StatusSuntingDatabase = True Then
            cmdPublic = New MySqlCommand(" UPDATE tbl_produk SET Status_Terpakai = 0 " &
                                             " WHERE Nomor_Seri_Produk = '" & client_SerialNumber_Terseleksi & "' ", KoneksiDatabasePublic)
            cmdPublic_ExecuteNonQuery_Transaction()
        End If

        'Hapus Data Perangkat :
        If StatusSuntingDatabase = True Then
            cmdPublic = New MySqlCommand(" DELETE FROM tbl_perangkat " &
                                             " WHERE Nomor_Seri_Produk = '" & client_SerialNumber_Terseleksi & "' ", KoneksiDatabasePublic)
            cmdPublic_ExecuteNonQuery_Transaction()
        End If

        'Komit :
        If StatusSuntingDatabase = True Then TransactionCommit_Public()

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

        TutupDatabasePublic()

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub cmb_StatusUpdate_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_StatusUpdate.SelectionChanged
        PilihanStatusUpdate = cmb_StatusUpdate.SelectedValue
        TampilkanData()
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
        client_SerialNumber_Terseleksi = rowviewUtama("Serial_Number")
        client_ID_Terseleksi = rowviewUtama("ID_Customer")
        client_NamaPerusahaan_Terseleksi = rowviewUtama("Nama_Perusahaan")

        If BarisTerseleksi >= 0 Then
            TampilkanDataInfoCustomer()
            pnl_CRUD.IsEnabled = True
        Else
            pnl_CRUD.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        'Belum ada kebutuhan kode di sini
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If e.Row.Item("Status_Update") = StatusUpdate_Update Then
            e.Row.Foreground = clrTeksPrimer
        Else
            e.Row.Foreground = clrNeutral500
        End If
    End Sub

    Private Sub txt_SerialNumber_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SerialNumber.TextChanged
        PerubahanFormInfoCustomer()
    End Sub

    Private Sub txt_IDCustomer_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_IDCustomer.TextChanged
        PerubahanFormInfoCustomer()
    End Sub

    Private Sub txt_NamaPerusahaan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaPerusahaan.TextChanged
        PerubahanFormInfoCustomer()
    End Sub

    Private Sub txt_PIC_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PIC.TextChanged
        PerubahanFormInfoCustomer()
    End Sub

    Private Sub dtp_TanggalExpire_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalExpire.SelectedDateChanged
        If dtp_TanggalExpire.Text <> Kosongan Then
            PerubahanFormInfoCustomer()
        End If
    End Sub


    Private Sub btn_RefreshInfoCustomer_Click(sender As Object, e As RoutedEventArgs) Handles btn_RefreshInfoCustomer.Click
        btn_RefreshInfoCustomer.IsEnabled = False
        btn_SimpanPerubahanInfoCustomer.IsEnabled = False
        ResetFormInfoCustomer()
        Terabas()
        Jeda(333)
        TampilkanDataInfoCustomer()
        btn_RefreshInfoCustomer.IsEnabled = True
    End Sub

    Private Sub btn_SimpanPerubahanInfoCustomer_Click(sender As Object, e As RoutedEventArgs) Handles btn_SimpanPerubahanInfoCustomer.Click

        If dtp_TanggalExpire.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalExpire, "Tanggal Expire")
            Return
        End If

        btn_SimpanPerubahanInfoCustomer.IsEnabled = False
        btn_RefreshInfoCustomer.IsEnabled = False
        Terabas()

        BukaDatabasePublic()
        cmdPublic = New MySqlCommand(" Update tbl_customer SET " &
                                     " Nama_Perusahaan      = '" & txt_NamaPerusahaan.Text & "', " &
                                     " PIC                  = '" & txt_PIC.Text & "', " &
                                     " Expire               = '" & TanggalFormatSimpan(dtp_TanggalExpire.SelectedDate) & "' " &
                                     " WHERE ID_Customer    = '" & client_ID_Terseleksi & "' ",
                                     KoneksiDatabasePublic)
        cmdPublic_ExecuteNonQuery()
        TutupDatabasePublic()

        If StatusKoneksiDatabasePublic = True Then
            PesanSukses("Penyimpanan Sukses")
            TampilkanDataInfoCustomer()
        Else
            PesanPeringatan("Penyimpanan Gagal..!")
            btn_SimpanPerubahanInfoCustomer.IsEnabled = True
        End If

        btn_RefreshInfoCustomer.IsEnabled = True

        TampilkanData()

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
    Dim Serial_Number As New DataGridTextColumn
    Dim ID_Customer As New DataGridTextColumn
    Dim Nama_Perusahaan As New DataGridTextColumn
    Dim Versi_App As New DataGridTextColumn
    Dim Apdet_App As New DataGridTextColumn
    Dim PIC_ As New DataGridTextColumn
    Dim Tanggal_Expire As New DataGridTextColumn
    Dim Status_Update As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Serial_Number")
        datatabelUtama.Columns.Add("ID_Customer")
        datatabelUtama.Columns.Add("Nama_Perusahaan")
        datatabelUtama.Columns.Add("Versi_App")
        datatabelUtama.Columns.Add("Apdet_App")
        datatabelUtama.Columns.Add("PIC_")
        datatabelUtama.Columns.Add("Tanggal_Expire")
        datatabelUtama.Columns.Add("Status_Update")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Serial_Number, "Serial_Number", "Serial Number", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, ID_Customer, "ID_Customer", "ID Client", 81, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Perusahaan, "Nama_Perusahaan", "Nama Perusahaan", 240, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Versi_App, "Versi_App", "Versi App", 45, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Apdet_App, "Apdet_App", "Apdet App", 45, FormatString, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, PIC_, "PIC_", "PIC", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Expire, "Tanggal_Expire", "Tanggal Expire", 81, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_Update, "Status_Update", "Status Upate", 63, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        txt_SudahUpdate.IsReadOnly = True
        txt_BelumUpdate.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
        txt_SerialNumber.IsReadOnly = True
        txt_IDCustomer.IsReadOnly = True
        txt_SudahUpdate.HorizontalContentAlignment = HorizontalAlignment.Right
        txt_BelumUpdate.HorizontalContentAlignment = HorizontalAlignment.Right
        txt_TotalTabel.HorizontalContentAlignment = HorizontalAlignment.Right
        pnl_InfoCustomer.Visibility = Visibility.Collapsed
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
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
