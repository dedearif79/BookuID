Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_BukuPenjualanEceran_RekapHarian

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Dim EksekusiTampilanData As Boolean

    Public JudulForm
    Public KesesuaianJurnal


    Dim TanggalTransaksi

    Dim NomorUrut
    Dim KodeToko
    Dim NamaToko
    Dim COAKas
    Dim JumlahKas
    Dim JumlahBank_01
    Dim JumlahBank_02
    Dim JumlahBank_03
    Dim JumlahBank_04
    Dim JumlahBank_05
    Dim JumlahBank_06
    Dim JumlahBank_07
    Dim JumlahBank_08
    Dim JumlahBank_09
    Dim JumlaheWallet_01
    Dim JumlaheWallet_02
    Dim JumlaheWallet_03
    Dim JumlaheWallet_04
    Dim JumlaheWallet_05
    Dim JumlaheWallet_06
    Dim JumlaheWallet_07
    Dim JumlaheWallet_08
    Dim JumlaheWallet_09
    Dim JumlahTransaksi
    Dim Keterangan
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim KodeToko_Terseleksi
    Dim NamaToko_Terseleksi
    Dim COAKas_Terseleksi
    Dim JumlahKas_Terseleksi
    Dim JumlahBank_01_Terseleksi
    Dim JumlahBank_02_Terseleksi
    Dim JumlahBank_03_Terseleksi
    Dim JumlahBank_04_Terseleksi
    Dim JumlahBank_05_Terseleksi
    Dim JumlahBank_06_Terseleksi
    Dim JumlahBank_07_Terseleksi
    Dim JumlahBank_08_Terseleksi
    Dim JumlahBank_09_Terseleksi
    Dim JumlaheWallet_01_Terseleksi
    Dim JumlaheWallet_02_Terseleksi
    Dim JumlaheWallet_03_Terseleksi
    Dim JumlaheWallet_04_Terseleksi
    Dim JumlaheWallet_05_Terseleksi
    Dim JumlaheWallet_06_Terseleksi
    Dim JumlaheWallet_07_Terseleksi
    Dim JumlaheWallet_08_Terseleksi
    Dim JumlaheWallet_09_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        ProsesLoadingForm = True

        LogikaTampilanKolom()

        'lbl_JudulForm.Text = frm_BukuPenjualanEceran.JudulForm

        ProsesLoadingForm = False

        EksekusiTampilanData = False
        dtp_TanggalTransaksi.Text = Kosongan
        EksekusiTampilanData = True

        RefreshTampilanData()

        SudahDimuat = True
    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub


    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub

    Async Sub TampilkanDataAsync()
        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            KesesuaianJurnal = True

            'Data Tabel :
            datatabelUtama.Rows.Clear()
            NomorUrut = 0

            AksesDatabase_General(Buka)
            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" SELECT * FROM tbl_Toko ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()

            Do While dr.Read

                KodeToko = dr.Item("Kode_Toko")
                NamaToko = dr.Item("Nama_Toko")
                COAKas = dr.Item("COA_Kas")

                JumlahKas = 0
                JumlahBank_01 = 0
                JumlahBank_02 = 0
                JumlahBank_03 = 0
                JumlahBank_04 = 0
                JumlahBank_05 = 0
                JumlahBank_06 = 0
                JumlahBank_07 = 0
                JumlahBank_08 = 0
                JumlahBank_09 = 0
                JumlaheWallet_01 = 0
                JumlaheWallet_02 = 0
                JumlaheWallet_03 = 0
                JumlaheWallet_04 = 0
                JumlaheWallet_05 = 0
                JumlaheWallet_06 = 0
                JumlaheWallet_07 = 0
                JumlaheWallet_08 = 0
                JumlaheWallet_09 = 0
                JumlahTransaksi = 0
                NomorJV = 0
                Keterangan = Kosongan
                User = Kosongan

                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Transaksi " &
                                             " WHERE Jenis_Jurnal       = '" & JenisJurnal_PenjualanEceran & "' " &
                                             " AND Kode_Lawan_Transaksi = '" & KodeToko & "' " &
                                             " AND Tanggal_Transaksi    = '" & TanggalFormatSimpan(TanggalTransaksi) & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()

                Do While drTELUSUR.Read
                    Dim COA = drTELUSUR.Item("COA")
                    Dim JumlahDebet As Int64 = drTELUSUR.Item("Jumlah_Debet")
                    'Kas Eceran:
                    If COA = COAKas Then JumlahKas = drTELUSUR.Item("Jumlah_Debet")
                    'Bank Eceran:
                    If COA = KodeTautanCOA_BankEceran_01 Then JumlahBank_01 = JumlahDebet
                    If COA = KodeTautanCOA_BankEceran_02 Then JumlahBank_02 = JumlahDebet
                    If COA = KodeTautanCOA_BankEceran_03 Then JumlahBank_03 = JumlahDebet
                    If COA = KodeTautanCOA_BankEceran_04 Then JumlahBank_04 = JumlahDebet
                    If COA = KodeTautanCOA_BankEceran_05 Then JumlahBank_05 = JumlahDebet
                    If COA = KodeTautanCOA_BankEceran_06 Then JumlahBank_06 = JumlahDebet
                    If COA = KodeTautanCOA_BankEceran_07 Then JumlahBank_07 = JumlahDebet
                    If COA = KodeTautanCOA_BankEceran_08 Then JumlahBank_08 = JumlahDebet
                    If COA = KodeTautanCOA_BankEceran_09 Then JumlahBank_09 = JumlahDebet
                    'eWallet:
                    If COA = KodeTautanCOA_eWallet_01 Then JumlaheWallet_01 = JumlahDebet
                    If COA = KodeTautanCOA_eWallet_02 Then JumlaheWallet_02 = JumlahDebet
                    If COA = KodeTautanCOA_eWallet_03 Then JumlaheWallet_03 = JumlahDebet
                    If COA = KodeTautanCOA_eWallet_04 Then JumlaheWallet_04 = JumlahDebet
                    If COA = KodeTautanCOA_eWallet_05 Then JumlaheWallet_05 = JumlahDebet
                    If COA = KodeTautanCOA_eWallet_06 Then JumlaheWallet_06 = JumlahDebet
                    If COA = KodeTautanCOA_eWallet_07 Then JumlaheWallet_07 = JumlahDebet
                    If COA = KodeTautanCOA_eWallet_08 Then JumlaheWallet_08 = JumlahDebet
                    If COA = KodeTautanCOA_eWallet_09 Then JumlaheWallet_09 = JumlahDebet
                    'Lain-lain:
                    JumlahTransaksi = JumlahKas + JumlahBank() + JumlaheWallet()
                    NomorJV = drTELUSUR.Item("Nomor_JV")
                    Keterangan = PenghapusEnter(drTELUSUR.Item("Uraian_Transaksi"))
                    User = drTELUSUR.Item("Username_Entry")
                Loop

                TambahBaris()

            Loop

            AksesDatabase_Transaksi(Tutup)
            AksesDatabase_General(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPenjualanEceran_RekapHarian")

        Finally
            BersihkanSeleksi_SetelahLoading()

        End Try

    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, KodeToko, NamaToko, COAKas, JumlahKas,
                                JumlahBank_01,
                                JumlahBank_02,
                                JumlahBank_03,
                                JumlahBank_04,
                                JumlahBank_05,
                                JumlahBank_06,
                                JumlahBank_07,
                                JumlahBank_08,
                                JumlahBank_09,
                                JumlaheWallet_01,
                                JumlaheWallet_02,
                                JumlaheWallet_03,
                                JumlaheWallet_04,
                                JumlaheWallet_05,
                                JumlaheWallet_06,
                                JumlaheWallet_07,
                                JumlaheWallet_08,
                                JumlaheWallet_09,
                                JumlahTransaksi, Keterangan, NomorJV, User)
    End Sub

    Function JumlahBank() As Int64
        Dim Jumlah As Int64
        Jumlah = 0 _
            + JumlahBank_01 _
            + JumlahBank_02 _
            + JumlahBank_03 _
            + JumlahBank_04 _
            + JumlahBank_05 _
            + JumlahBank_06 _
            + JumlahBank_07 _
            + JumlahBank_08 _
            + JumlahBank_09
        Return Jumlah
    End Function

    Function JumlaheWallet() As Int64
        Dim Jumlah As Int64
        Jumlah = 0 _
            + JumlaheWallet_01 _
            + JumlaheWallet_02 _
            + JumlaheWallet_03 _
            + JumlaheWallet_04 _
            + JumlaheWallet_05 _
            + JumlaheWallet_06 _
            + JumlaheWallet_07 _
            + JumlaheWallet_08 _
            + JumlaheWallet_09
        Return Jumlah
    End Function


    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Input.IsEnabled = False
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        SedangMemuatData = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub


    Sub LogikaTampilanKolom()
        Jumlah_Kas.Header = AmbilValue_NamaAkun(COAKas)
        'Bank Eceran:
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_01, Jumlah_Bank_01)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_02, Jumlah_Bank_02)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_03, Jumlah_Bank_03)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_04, Jumlah_Bank_04)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_05, Jumlah_Bank_05)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_06, Jumlah_Bank_06)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_07, Jumlah_Bank_07)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_08, Jumlah_Bank_08)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_09, Jumlah_Bank_09)
        'eWallet:
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_01, Jumlah_eWallet_01)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_02, Jumlah_eWallet_02)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_03, Jumlah_eWallet_03)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_04, Jumlah_eWallet_04)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_05, Jumlah_eWallet_05)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_06, Jumlah_eWallet_06)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_07, Jumlah_eWallet_07)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_08, Jumlah_eWallet_08)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_09, Jumlah_eWallet_09)
    End Sub

    Sub VisibilitasKolomCOA(COA As String, kolom As DataGridTextColumn)
        If VisibilitasCOA(COA) Then
            kolom.Visibility = Visibility.Visible
        Else
            kolom.Visibility = Visibility.Collapsed
        End If
        kolom.Header = AmbilValue_NamaAkun(COA)
    End Sub



    Private Sub dtp_TanggalTransaksi_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalTransaksi.SelectedDateChanged
        If dtp_TanggalTransaksi.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalTransaksi)
            TanggalTransaksi = TanggalFormatTampilan(dtp_TanggalTransaksi.SelectedDate)
        End If
        TampilkanData()
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        If dtp_TanggalTransaksi.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalTransaksi, "Tanggal Transaksi")
            Return
        End If
        win_InputPenjualanEceran = New wpfWin_InputPenjualanEceran
        win_InputPenjualanEceran.ResetForm()
        win_InputPenjualanEceran.FungsiForm = FungsiForm_TAMBAH
        InputEditData()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputPenjualanEceran = New wpfWin_InputPenjualanEceran
        win_InputPenjualanEceran.ResetForm()
        win_InputPenjualanEceran.FungsiForm = FungsiForm_EDIT
        win_InputPenjualanEceran.NomorJV = NomorJV_Terseleksi
        InputEditData()
    End Sub
    Sub InputEditData()
        win_InputPenjualanEceran.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(TanggalTransaksi)
        win_InputPenjualanEceran.KodeToko = KodeToko_Terseleksi
        win_InputPenjualanEceran.txt_NamaToko.Text = NamaToko_Terseleksi
        win_InputPenjualanEceran.COAKas = COAKas_Terseleksi
        win_InputPenjualanEceran.txt_JumlahKas.Text = JumlahKas_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_01.Text = JumlahBank_01_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_02.Text = JumlahBank_02_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_03.Text = JumlahBank_03_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_04.Text = JumlahBank_04_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_05.Text = JumlahBank_05_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_06.Text = JumlahBank_06_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_07.Text = JumlahBank_07_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_08.Text = JumlahBank_08_Terseleksi
        win_InputPenjualanEceran.txt_JumlahBank_09.Text = JumlahBank_09_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_09.Text = JumlaheWallet_09_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_01.Text = JumlaheWallet_01_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_02.Text = JumlaheWallet_02_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_03.Text = JumlaheWallet_03_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_04.Text = JumlaheWallet_04_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_05.Text = JumlaheWallet_05_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_06.Text = JumlaheWallet_06_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_07.Text = JumlaheWallet_07_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_08.Text = JumlaheWallet_08_Terseleksi
        win_InputPenjualanEceran.txt_JumlaheWallet_09.Text = JumlaheWallet_09_Terseleksi
        IsiValueElemenRichTextBox(win_InputPenjualanEceran.txt_Keterangan, Keterangan_Terseleksi)
        win_InputPenjualanEceran.ShowDialog()
    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

    End Sub


    Private Sub btn_DaftarToko_Click(sender As Object, e As RoutedEventArgs) Handles btn_DaftarToko.Click
        win_BOOKU.BukaHalaman_DataToko()
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
        KodeToko_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Toko")
        NamaToko_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Toko")
        COAKas_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Kas")
        JumlahKas_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Kas"))
        JumlahBank_01_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_01"))
        JumlahBank_02_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_02"))
        JumlahBank_03_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_03"))
        JumlahBank_04_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_04"))
        JumlahBank_05_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_05"))
        JumlahBank_06_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_06"))
        JumlahBank_07_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_07"))
        JumlahBank_08_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_08"))
        JumlahBank_09_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bank_09"))
        JumlaheWallet_01_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_01"))
        JumlaheWallet_02_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_02"))
        JumlaheWallet_03_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_03"))
        JumlaheWallet_04_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_04"))
        JumlaheWallet_05_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_05"))
        JumlaheWallet_06_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_06"))
        JumlaheWallet_07_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_07"))
        JumlaheWallet_08_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_08"))
        JumlaheWallet_09_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_eWallet_09"))
        JumlahTransaksi_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Transaksi"))
        Keterangan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Keterangan_")
        NomorJV_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV"))
        User_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "User_")

        If BarisTerseleksi >= 0 Then
            If NomorJV_Terseleksi > 0 Then
                btn_LihatJurnal.IsEnabled = True
                btn_Input.IsEnabled = False
                btn_Edit.IsEnabled = True
                btn_Hapus.IsEnabled = True
            Else
                btn_LihatJurnal.IsEnabled = False
                btn_Input.IsEnabled = True
                btn_Edit.IsEnabled = False
                btn_Hapus.IsEnabled = False
            End If
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        '(Belum ada Coding)
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
    Dim Kode_Toko As New DataGridTextColumn
    Dim Nama_Toko As New DataGridTextColumn
    Dim COA_Kas As New DataGridTextColumn
    Dim Jumlah_Kas As New DataGridTextColumn
    Dim Jumlah_Bank_01 As New DataGridTextColumn
    Dim Jumlah_Bank_02 As New DataGridTextColumn
    Dim Jumlah_Bank_03 As New DataGridTextColumn
    Dim Jumlah_Bank_04 As New DataGridTextColumn
    Dim Jumlah_Bank_05 As New DataGridTextColumn
    Dim Jumlah_Bank_06 As New DataGridTextColumn
    Dim Jumlah_Bank_07 As New DataGridTextColumn
    Dim Jumlah_Bank_08 As New DataGridTextColumn
    Dim Jumlah_Bank_09 As New DataGridTextColumn
    Dim Jumlah_eWallet_01 As New DataGridTextColumn
    Dim Jumlah_eWallet_02 As New DataGridTextColumn
    Dim Jumlah_eWallet_03 As New DataGridTextColumn
    Dim Jumlah_eWallet_04 As New DataGridTextColumn
    Dim Jumlah_eWallet_05 As New DataGridTextColumn
    Dim Jumlah_eWallet_06 As New DataGridTextColumn
    Dim Jumlah_eWallet_07 As New DataGridTextColumn
    Dim Jumlah_eWallet_08 As New DataGridTextColumn
    Dim Jumlah_eWallet_09 As New DataGridTextColumn
    Dim Jumlah_Transaksi As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Kode_Toko")
        datatabelUtama.Columns.Add("Nama_Toko")
        datatabelUtama.Columns.Add("COA_Kas")
        datatabelUtama.Columns.Add("Jumlah_Kas", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_01", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_02", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_03", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_04", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_05", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_06", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_07", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_08", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bank_09", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_01", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_02", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_03", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_04", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_05", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_06", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_07", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_08", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_eWallet_09", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Transaksi", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Toko, "Kode_Toko", "Kode Toko", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Toko, "Nama_Toko", "Nama Toko", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Kas, "COA_Kas", "COA Kas", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Kas, "Jumlah_Kas", "Kas", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_01, "Jumlah_Bank_01", "Jumlah Bank 01", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_02, "Jumlah_Bank_02", "Jumlah Bank 02", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_03, "Jumlah_Bank_03", "Jumlah Bank 03", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_04, "Jumlah_Bank_04", "Jumlah Bank 04", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_05, "Jumlah_Bank_05", "Jumlah Bank 05", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_06, "Jumlah_Bank_06", "Jumlah Bank 06", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_07, "Jumlah_Bank_07", "Jumlah Bank 07", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_08, "Jumlah_Bank_08", "Jumlah Bank 08", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bank_09, "Jumlah_Bank_09", "Jumlah Bank 09", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_01, "Jumlah_eWallet_01", "Jumlah eWallet 01", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_02, "Jumlah_eWallet_02", "Jumlah eWallet 02", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_03, "Jumlah_eWallet_03", "Jumlah eWallet 03", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_04, "Jumlah_eWallet_04", "Jumlah eWallet 04", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_05, "Jumlah_eWallet_05", "Jumlah eWallet 05", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_06, "Jumlah_eWallet_06", "Jumlah eWallet 06", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_07, "Jumlah_eWallet_07", "Jumlah eWallet 07", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_08, "Jumlah_eWallet_08", "Jumlah eWallet 08", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_eWallet_09, "Jumlah_eWallet_09", "Jumlah eWallet 09", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Transaksi, "Jumlah_Transaksi", "Jumlah", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Catatan", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        txt_SaldoBerdasarkanList.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_SelisihSaldo.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.IsReadOnly = True
        txt_AJP.IsReadOnly = True
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
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
