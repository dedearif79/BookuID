Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Forms.Integration
Imports bcomm

Imports System.IO
''' <summary>
''' WPF Application Shell untuk BOOKU (Mode Modern)
''' - Langsung membuka WPF UserControl jika tersedia
''' - Fallback ke WindowsFormsHost untuk form yang belum migrasi
''' </summary>
Public Class wpfWin_BOOKU

    ''' <summary>
    ''' Jika True, aplikasi akan keluar tanpa konfirmasi (misal: setelah kloning database)
    ''' </summary>
    Public PaksaKeluarAplikasi As Boolean = False

    Sub New()
        InitializeComponent()

        ' Inisialisasi DataTable Notifikasi
        Buat_DataTabelNotifikasi()
    End Sub

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        UpdateStatusBar()

    End Sub

    Public Sub UpdateStatusBar()
        Try
            lbl_StatusCompany.Text = "Company: " & If(String.IsNullOrEmpty(NamaPerusahaan), "-", NamaPerusahaan)
            lbl_StatusTahunBuku.Text = "Tahun Buku: " & If(TahunBukuAktif = 0, "-", TahunBukuAktif.ToString())
            lbl_StatusUser.Text = "User: " & If(String.IsNullOrEmpty(NamaUserAktif), "-", NamaUserAktif)
        Catch ex As Exception
        End Try
    End Sub

#Region "Helper Methods"

    ''' <summary>
    ''' Membuka WPF UserControl dalam tab baru
    ''' </summary>
    Public Sub BukaUserControlDalamTab(userControl As UserControl, tabHeader As String)
        ' Cek apakah tab sudah ada
        Dim tabExisting = CariTabByHeader(tabHeader)
        If tabExisting IsNot Nothing Then
            tab_MainContent.SelectedItem = tabExisting
            Return
        End If

        ' Buat tab baru dengan close button
        Dim tabBaru = BuatTabDenganCloseButton(tabHeader)
        tabBaru.Content = userControl
        tab_MainContent.Items.Add(tabBaru)
        tab_MainContent.SelectedItem = tabBaru
    End Sub


    ''' <summary>
    ''' Menampilkan pesan bahwa menu masih dalam pengembangan
    ''' </summary>
    Private Sub MenuIniMasihDalamPengembangan()
        Pesan_Informasi("Menu ini masih dalam pengembangan.")
    End Sub

    Private Function CariTabByHeader(header As String) As TabItem
        For Each item As TabItem In tab_MainContent.Items
            If TypeOf item.Header Is StackPanel Then
                Dim sp = CType(item.Header, StackPanel)
                If sp.Children.Count > 0 AndAlso TypeOf sp.Children(0) Is TextBlock Then
                    If CType(sp.Children(0), TextBlock).Text = header Then Return item
                End If
            ElseIf item.Header?.ToString() = header Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Private Function BuatTabDenganCloseButton(header As String) As TabItem
        Dim tabBaru As New TabItem()

        Dim headerPanel As New StackPanel() With {.Orientation = Orientation.Horizontal}
        Dim headerText As New TextBlock() With {.Text = header, .Margin = New Thickness(0, 0, 10, 0)}
        Dim closeBtn As New Button() With {
            .Content = "×",
            .FontSize = 14,
            .FontWeight = FontWeights.Bold,
            .Background = Media.Brushes.Transparent,
            .BorderThickness = New Thickness(0),
            .Cursor = Input.Cursors.Hand,
            .Padding = New Thickness(3, 0, 3, 0),
            .Tag = tabBaru
        }
        AddHandler closeBtn.Click, AddressOf TutupTab_Click

        headerPanel.Children.Add(headerText)
        headerPanel.Children.Add(closeBtn)
        tabBaru.Header = headerPanel

        Return tabBaru
    End Function

    Private Sub TutupTab_Click(sender As Object, e As RoutedEventArgs)
        Dim tab = CType(CType(sender, Button).Tag, TabItem)

        ' Dispose resources
        If TypeOf tab.Content Is WindowsFormsHost Then
            Dim host = CType(tab.Content, WindowsFormsHost)
            Dim form = TryCast(host.Child, Form)
            If form IsNot Nothing Then
                form.Close()
                form.Dispose()
            End If
            host.Dispose()
        End If

        tab_MainContent.Items.Remove(tab)
    End Sub

    ''' <summary>
    ''' Menutup semua tab yang aktif di WPF Shell
    ''' </summary>
    Public Sub TutupSemuaTab()
        ' Dispose semua WindowsFormsHost terlebih dahulu
        For Each item As TabItem In tab_MainContent.Items
            If TypeOf item.Content Is WindowsFormsHost Then
                Dim host = CType(item.Content, WindowsFormsHost)
                Dim form = TryCast(host.Child, Form)
                If form IsNot Nothing Then
                    form.Close()
                    form.Dispose()
                End If
                host.Dispose()
            End If
        Next
        ' Hapus semua tab
        tab_MainContent.Items.Clear()
    End Sub

#End Region

#Region "Menu Event Handlers - Langsung ke UserControl"

    ' === FILE ===
    Private Sub mnu_Pengaturan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Pengaturan.Click
        win_Pengaturan = New wpfWin_Pengaturan With {.FungsiForm = "PENGATURAN"}
        win_Pengaturan.ShowDialog()
    End Sub

    Private Sub mnu_KembaliKeClassic_Click(sender As Object, e As RoutedEventArgs) Handles mnu_KembaliKeClassic.Click
        If MessageBox.Show(
            "Beralih ke Mode Classic? Aplikasi akan restart.",
            "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
            ModusAplikasi = "CLASSIC"
            Forms.Application.Restart()
            Application.Current.Shutdown()
        End If
    End Sub

    Private Sub mnu_Keluar_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Keluar.Click
        Me.Close()
    End Sub

    ' === DATA ===
    Private Sub mnu_DataUser_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataUser.Click
        usc_DataUser = New wpfUsc_DataUser
        BukaUserControlDalamTab(usc_DataUser, "Data User")
    End Sub

    Private Sub mnu_DataCOA_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataCOA.Click
        usc_DataCOA = New wpfUsc_DataCOA
        BukaUserControlDalamTab(usc_DataCOA, "Data COA")
    End Sub

    Private Sub mnu_DataLawanTransaksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataLawanTransaksi.Click
        usc_DataLawanTransaksi = New wpfUsc_DataLawanTransaksi
        BukaUserControlDalamTab(usc_DataLawanTransaksi, "Data Lawan Transaksi")
    End Sub

    Private Sub mnu_DataKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataKaryawan.Click
        usc_DataKaryawan = New wpfUsc_DataKaryawan
        BukaUserControlDalamTab(usc_DataKaryawan, "Data Karyawan")
    End Sub

    Private Sub mnu_DataProject_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataProject.Click
        usc_DataProject = New wpfUsc_DataProject
        BukaUserControlDalamTab(usc_DataProject, "Data Project")
    End Sub

    Private Sub mnu_Kurs_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Kurs.Click
        usc_Kurs = New wpfUsc_Kurs
        BukaUserControlDalamTab(usc_Kurs, "Kurs")
    End Sub

    ' === AKUNTANSI ===
    Private Sub mnu_JurnalUmum_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalUmum.Click
        usc_JurnalUmum = New wpfUsc_JurnalUmum
        BukaUserControlDalamTab(usc_JurnalUmum, "Jurnal Umum")
    End Sub

    Private Sub mnu_BukuBesar_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBesar.Click
        usc_BukuBesar = New wpfUsc_BukuBesar
        usc_BukuBesar.FungsiModul = Halaman_BUKUBESAR
        BukaUserControlDalamTab(usc_BukuBesar, "Buku Besar")
    End Sub

    ' mnu_TrialBalance di frm_BOOKU memiliki Visible = False, sehingga tidak ditambahkan ke wpfWin_BOOKU
    ' Handler ini di-comment untuk sementara
    ' Private Sub mnu_TrialBalance_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TrialBalance.Click
    '     usc_LaporanTrialBalance = New wpfUsc_LaporanTrialBalance
    '     BukaUserControlDalamTab(usc_LaporanTrialBalance, "Trial Balance")
    ' End Sub

    Private Sub mnu_LaporanHPP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LaporanHPP.Click
        BukaHalaman_LaporanHPP()
    End Sub
    Sub BukaHalaman_LaporanHPP()
        usc_LaporanHPP = New wpfUsc_LaporanHPP
        BukaUserControlDalamTab(usc_LaporanHPP, "Laporan HPP")
    End Sub

    Private Sub mnu_LabaRugi_Bulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LabaRugi_Bulanan.Click
        BukaHalaman_LaporanLabaRugi_Bulanan()
    End Sub
    Sub BukaHalaman_LaporanLabaRugi_Bulanan()
        usc_LaporanLabaRugi_Bulanan = New wpfUsc_LaporanLabaRugi_Bulanan
        BukaUserControlDalamTab(usc_LaporanLabaRugi_Bulanan, "Laba Rugi Bulanan")
    End Sub

    Private Sub mnu_LabaRugi_Tahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LabaRugi_Tahunan.Click
        usc_LaporanLabaRugi_Tahunan = New wpfUsc_LaporanLabaRugi_Tahunan
        BukaUserControlDalamTab(usc_LaporanLabaRugi_Tahunan, "Laba Rugi Tahunan")
    End Sub

    Private Sub mnu_Neraca_Bulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Neraca_Bulanan.Click
        BukaHalaman_LaporanNeraca_Bulanan()
    End Sub
    Sub BukaHalaman_LaporanNeraca_Bulanan()
        usc_LaporanNeraca_Bulanan = New wpfUsc_LaporanNeraca_Bulanan
        BukaUserControlDalamTab(usc_LaporanNeraca_Bulanan, "Neraca Bulanan")
    End Sub

    Private Sub mnu_Neraca_Tahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Neraca_Tahunan.Click
        usc_LaporanNeraca_Tahunan = New wpfUsc_LaporanNeraca_Tahunan
        BukaUserControlDalamTab(usc_LaporanNeraca_Tahunan, "Neraca Tahunan")
    End Sub

    Private Sub mnu_NeracaLajur_Click(sender As Object, e As RoutedEventArgs) Handles mnu_NeracaLajur.Click
        usc_LaporanNeracaLajur = New wpfUsc_LaporanNeracaLajur With {
            .JalurMasuk = Halaman_MENUUTAMA
        }
        BukaUserControlDalamTab(usc_LaporanNeracaLajur, "Neraca Lajur")
    End Sub

    ' === BUKU PENGAWASAN ===
    Private Sub mnu_BukuBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBank.Click
        usc_BukuBank = New wpfUsc_BukuBesar
        usc_BukuBank.FungsiModul = Halaman_BUKUBANK
        BukaUserControlDalamTab(usc_BukuBank, "Buku Bank")
    End Sub

    Private Sub mnu_BukuKas_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuKas.Click
        usc_BukuKas = New wpfUsc_BukuBesar
        usc_BukuKas.FungsiModul = Halaman_BUKUKAS
        BukaUserControlDalamTab(usc_BukuKas, "Buku Kas")
    End Sub

    Private Sub mnu_BukuPettyCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPettyCash.Click
        usc_BukuPettyCash = New wpfUsc_BukuBesar
        usc_BukuPettyCash.FungsiModul = Halaman_BUKUPETTYCASH
        BukaUserControlDalamTab(usc_BukuPettyCash, "Petty Cash")
    End Sub

    Private Sub mnu_BukuPengawasanGaji_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanGaji.Click
        BukaModul_BukuPengawasanGaji()
    End Sub
    Sub BukaModul_BukuPengawasanGaji()
        usc_BukuPengawasanGaji = New wpfUsc_BukuPengawasanGaji
        BukaUserControlDalamTab(usc_BukuPengawasanGaji, "Buku Pengawasan Gaji")
    End Sub

    ' === ASSET ===
    Private Sub mnu_DaftarPenyusutanAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DaftarPenyusutanAssetTetap.Click
        BukaModul_DaftarPenyusutanAssetTetap()
    End Sub
    Sub BukaModul_DaftarPenyusutanAssetTetap()
        Dim JudulForm = "Daftar Penyusutan Asset Tetap"
        usc_DaftarPenyusutanAssetTetap = New wpfUsc_DaftarPenyusutanAssetTetap With {
            .JalurMasuk = Halaman_MENUUTAMA
        }
        BukaUserControlDalamTab(usc_DaftarPenyusutanAssetTetap, JudulForm)
    End Sub

    Private Sub mnu_ManajemenAmortisasiBiaya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenAmortisasiBiaya.Click
        BukaModul_DaftarAmortisasiBiaya()
    End Sub
    Sub BukaModul_DaftarAmortisasiBiaya()
        Dim JudulForm = "Daftar Amortisasi Biaya"
        usc_DaftarAmortisasiBiaya = New wpfUsc_DaftarAmortisasiBiaya
        BukaUserControlDalamTab(usc_DaftarAmortisasiBiaya, JudulForm)
    End Sub

#End Region

#Region "Menu Event Handlers - Belum Diimplementasi"

    ' ============================================================
    ' FILE - DATABASE
    ' ============================================================
    Private Sub mnu_Database_Cadangkan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Database_Cadangkan.Click
        Dim PesanPertanyaan As String = "Anda akan melakukan pencadangan database."
        If Not TanyaKonfirmasi(PesanPertanyaan & Enter2Baris & "Lanjutkan?") Then Return
        win_BackupData = New wpfWin_BackupData
        win_BackupData.ResetForm()
        win_BackupData.ShowDialog()
    End Sub

    Private Sub mnu_Database_Pulihkan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Database_Pulihkan.Click
        win_PulihkanData = New wpfWin_PulihkanData
        win_PulihkanData.ResetForm()
        win_PulihkanData.ShowDialog()
    End Sub

    Private Sub mnu_Database_Kloning_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Database_Kloning.Click
        win_KloningData = New wpfWin_KloningData
        win_KloningData.ResetForm()
        win_KloningData.ShowDialog()
        If win_KloningData.KloningDatabaseBerhasil Then
            PaksaKeluarAplikasi = True
            Me.Close()
        End If
    End Sub

    ' ============================================================
    ' DATA - COMPANY PROFILE & PEMEGANG SAHAM
    ' ============================================================
    Private Sub mnu_CompanyProfile_Click(sender As Object, e As RoutedEventArgs) Handles mnu_CompanyProfile.Click
        win_Pengaturan = New wpfWin_Pengaturan With {
            .FungsiForm = "PENGATURAN"
        }
        win_Pengaturan.tab_Pengaturan.SelectedIndex = 1
        win_Pengaturan.ShowDialog()
    End Sub

    Private Sub mnu_DaftarPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DaftarPemegangSaham.Click
        Dim JudulForm = "Daftar Pemegang Saham"
        usc_DaftarPemegangSaham = New wpfUsc_DaftarPemegangSaham
        BukaUserControlDalamTab(usc_DaftarPemegangSaham, JudulForm)
    End Sub

    ' ============================================================
    ' DATA - DATA AWAL HUTANG
    ' ============================================================
    Private Sub mnu_DataAwal_HutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_USD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_AUD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_AUD()
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_JPY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_JPY()
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_CNY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_CNY()
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_EUR.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_EUR()
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_SGD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_SGD()
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_GBP.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_GBP()
    End Sub

    Private Sub mnu_DataAwal_HutangBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangBank.Click
        BukaModul_BukuPengawasanHutangBank()
    End Sub

    Private Sub mnu_DataAwal_HutangLeasing_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangLeasing.Click
        BukaModul_BukuPengawasanHutangLeasing()
    End Sub

    Private Sub mnu_DataAwal_HutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPihakKetiga.Click
        BukaModul_BukuPengawasanHutangPihakKetiga()
    End Sub

    Private Sub mnu_DataAwal_HutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangAfiliasi.Click
        BukaModul_BukuPengawasanHutangAfiliasi()
    End Sub

    Private Sub mnu_DataAwal_HutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangKaryawan.Click
        BukaModul_BukuPengawasanHutangKaryawan()
    End Sub

    Private Sub mnu_DataAwal_HutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPemegangSaham.Click
        BukaModul_BukuPengawasanHutangPemegangSaham()
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal21_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal21.Click
        BukaModul_BukuPengawasanHutangPPhPasal21()
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal23_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal23.Click
        BukaModul_BukuPengawasanHutangPPhPasal23()
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal42_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal42.Click
        BukaModul_BukuPengawasanHutangPPhPasal42()
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal25_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal25.Click
        BukaModul_BukuPengawasanHutangPPhPasal25()
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal26_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal26.Click
        BukaModul_BukuPengawasanHutangPPhPasal26()
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal29_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal29.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_DataAwal_HutangPPN_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPN.Click
        BukaModul_BukuPengawasanPelaporanPPN()
    End Sub

    Private Sub mnu_DataAwal_HutangKetetapanPajak_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangKetetapanPajak.Click
        BukaModul_BukuPengawasanKetetapanPajak()
    End Sub

    Private Sub mnu_DataAwal_HutangGaji_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangGaji.Click
        BukaModul_BukuPengawasanGaji()
    End Sub

    Private Sub mnu_DataAwal_HutangBPJSKesehatan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangBPJSKesehatan.Click
        BukaModul_BukuPengawasanHutangBPJSKesehatan()
    End Sub

    Private Sub mnu_DataAwal_HutangBPJSKetenagakerjaan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangBPJSKetenagakerjaan.Click
        BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
    End Sub

    Private Sub mnu_DataAwal_HutangKoperasiKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangKoperasiKaryawan.Click
        BukaModul_BukuPengawasanHutangKoperasiKaryawan()
    End Sub

    Private Sub mnu_DataAwal_HutangSerikat_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangSerikat.Click
        BukaModul_BukuPengawasanHutangSerikat()
    End Sub

    ' ============================================================
    ' DATA - DATA AWAL PIUTANG
    ' ============================================================
    Private Sub mnu_DataAwal_PiutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_NonAfiliasi()
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_USD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_AUD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_AUD()
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_JPY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_JPY()
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_CNY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_CNY()
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_EUR.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_EUR()
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_SGD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_SGD()
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_GBP.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_GBP()
    End Sub

    Private Sub mnu_DataAwal_PiutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangPihakKetiga.Click
        BukaModul_BukuPengawasanPiutangPihakKetiga()
    End Sub

    Private Sub mnu_DataAwal_PiutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangAfiliasi.Click
        BukaModul_BukuPengawasanPiutangAfiliasi()
    End Sub

    Private Sub mnu_DataAwal_PiutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangKaryawan.Click
        BukaModul_BukuPengawasanPiutangKaryawan()
    End Sub

    Private Sub mnu_DataAwal_PiutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangPemegangSaham.Click
        BukaModul_BukuPengawasanPiutangPemegangSaham()
    End Sub

    Private Sub mnu_DataAwal_DepositOperasional_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_DepositOperasional.Click
        BukaModul_BukuPengawasanDepositOperasional(DariDataAwal:=True)
    End Sub

    ' ============================================================
    ' DATA - DATA AWAL ASSET
    ' ============================================================
    Private Sub mnu_DataAwal_AmortisasiBiaya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_AmortisasiBiaya.Click
        BukaModul_DaftarAmortisasiBiaya()
    End Sub

    Private Sub mnu_DataAwal_AssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_AssetTetap.Click
        BukaModul_DaftarPenyusutanAssetTetap()
    End Sub

    ' ============================================================
    ' DATA - TAHUN BUKU
    ' ============================================================
    Private Sub mnu_BuatBukuBaru_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BuatBukuBaru.Click
        win_BuatDatabaseBukuBaru = New wpfWin_BuatDatabaseBukuBaru
        win_BuatDatabaseBukuBaru.ResetForm()
        win_BuatDatabaseBukuBaru.ShowDialog()
    End Sub

    Private Sub mnu_GantiTahunBuku_Click(sender As Object, e As RoutedEventArgs) Handles mnu_GantiTahunBuku.Click
        win_GantiTahunBuku = New wpfWin_GantiTahunBuku With {
            .FungsiForm = FungsiForm_GANTITAHUNBUKU
        }
        win_GantiTahunBuku.ShowDialog()
    End Sub

    Private Sub mnu_TutupBuku_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TutupBuku.Click
        Dim JudulForm = "Tutup Buku"
        usc_TutupBuku = New wpfUsc_TutupBuku
        usc_TutupBuku.ResetForm()
        BukaUserControlDalamTab(usc_TutupBuku, JudulForm)
    End Sub

    ' ============================================================
    ' TRANSAKSI - PEREKAMAN DATA
    ' ============================================================
    Private Sub mnu_InputPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InputPembelian.Click
    End Sub

    Private Sub mnu_InputPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InputPenjualan.Click
    End Sub

    Private Sub mnu_InputReturPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InputReturPembelian.Click
    End Sub

    Private Sub mnu_InputReturPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InputReturPenjualan.Click
    End Sub

    Private Sub mnu_PenghasilanBunga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PenghasilanBunga.Click
    End Sub

    Private Sub mnu_PPhAtasBunga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PPhAtasBunga.Click
    End Sub

    Private Sub mnu_BiayaAdministrasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BiayaAdministrasi.Click
    End Sub

    Private Sub mnu_PerekamanDataLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PerekamanDataLainnya.Click
    End Sub

    Private Sub mnu_TransaksiIN_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TransaksiIN.Click
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            FiturBelumBisaDigunakan()
            Return
        End If
        win_InputTransaksi = New wpfWin_InputTransaksi With {
            .FungsiForm = FungsiForm_TAMBAH,
            .JalurMasuk = Halaman_MENUUTAMA
        }
        win_InputTransaksi.ResetForm()
        IsiValueComboBypassTerkunci(win_InputTransaksi.cmb_AlurTransaksi, AlurTransaksi_IN)
        win_InputTransaksi.ShowDialog()
    End Sub

    Private Sub mnu_TransaksiOUT_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TransaksiOUT.Click
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            FiturBelumBisaDigunakan()
            Return
        End If
        win_InputTransaksi = New wpfWin_InputTransaksi With {
            .FungsiForm = FungsiForm_TAMBAH,
            .JalurMasuk = Halaman_MENUUTAMA
        }
        win_InputTransaksi.ResetForm()
        IsiValueComboBypassTerkunci(win_InputTransaksi.cmb_AlurTransaksi, AlurTransaksi_OUT)
        win_InputTransaksi.ShowDialog()
    End Sub

    ' ============================================================
    ' TRANSAKSI - ADJUSMENT
    ' ============================================================
    Private Sub mnu_Adjusment_BiayaPenyusutanAsset_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Adjusment_BiayaPenyusutanAsset.Click
    End Sub

    Private Sub mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka.Click
    End Sub

    Private Sub mnu_Adjusment_PenghapusanPiutang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Adjusment_PenghapusanPiutang.Click
    End Sub

    Private Sub mnu_Adjusment_SelisihKurs_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Adjusment_SelisihKurs.Click
    End Sub

    Private Sub mnu_Adjusment_SelisihPencatatan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Adjusment_SelisihPencatatan.Click
    End Sub

    Private Sub mnu_AdjusmentLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_AdjusmentLainnya.Click
    End Sub

    Private Sub mnu_Pemindahbukuan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Pemindahbukuan.Click
    End Sub

    ' ============================================================
    ' PEMBELIAN - PO
    ' ============================================================
    Private Sub mnu_POPembelian_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_Barang.Click
        Dim JudulForm = "PO Pembelian - Barang"
        usc_POPembelian_Lokal_Barang = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPembelian_Lokal_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_Barang.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPembelian_Lokal_Barang, JudulForm)
    End Sub

    Private Sub mnu_POPembelian_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_Jasa.Click
        Dim JudulForm = "PO Pembelian - Jasa"
        usc_POPembelian_Lokal_Jasa = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPembelian_Lokal_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_Jasa.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPembelian_Lokal_Jasa, JudulForm)
    End Sub

    Private Sub mnu_POPembelian_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_BarangDanJasa.Click
        Dim JudulForm = "PO Pembelian - Barang dan Jasa"
        usc_POPembelian_Lokal_BarangDanJasa = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_BarangDanJasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_BarangDanJasa
        usc_POPembelian_Lokal_BarangDanJasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_BarangDanJasa.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPembelian_Lokal_BarangDanJasa, JudulForm)
    End Sub

    Private Sub mnu_POPembelian_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_JasaKonstruksi.Click
        Dim JudulForm = "PO Pembelian - Jasa Konstruksi"
        usc_POPembelian_Lokal_JasaKonstruksi = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_JasaKonstruksi.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_JasaKonstruksi
        usc_POPembelian_Lokal_JasaKonstruksi.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Lokal_JasaKonstruksi.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPembelian_Lokal_JasaKonstruksi, JudulForm)
    End Sub

    Private Sub mnu_POPembelian_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Semua.Click
        Dim JudulForm = "PO Pembelian"
        usc_POPembelian_Lokal_Semua = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        usc_POPembelian_Lokal_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPembelian_Lokal_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPembelian_Lokal_Semua.VisibilitasFilterJenisProdukInduk(True)
        BukaUserControlDalamTab(usc_POPembelian_Lokal_Semua, JudulForm)
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Barang.Click
        Dim JudulForm = "PO Pembelian Impor - Barang"
        usc_POPembelian_Impor_Barang = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
        usc_POPembelian_Impor_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPembelian_Impor_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Impor_Barang.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPembelian_Impor_Barang, JudulForm)
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Jasa.Click
        Dim JudulForm = "PO Pembelian Impor - Jasa"
        usc_POPembelian_Impor_Jasa = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
        usc_POPembelian_Impor_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPembelian_Impor_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPembelian_Impor_Jasa.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPembelian_Impor_Jasa, JudulForm)
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Semua.Click
        Dim JudulForm = "PO Pembelian Impor"
        usc_POPembelian_Impor_Semua = New wpfUsc_POPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
        usc_POPembelian_Impor_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPembelian_Impor_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPembelian_Impor_Semua.VisibilitasFilterJenisProdukInduk(True)
        BukaUserControlDalamTab(usc_POPembelian_Impor_Semua, JudulForm)
    End Sub

    ' ============================================================
    ' PEMBELIAN - SURAT JALAN & BAST
    ' ============================================================
    Private Sub mnu_SuratJalanPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_SuratJalanPembelian.Click
        Dim JudulForm = "Surat Jalan Pembelian"
        usc_SuratJalanPembelian = New wpfUsc_SuratJalanPembelian With {
            .JudulForm = JudulForm
        }
        BukaUserControlDalamTab(usc_SuratJalanPembelian, JudulForm)
    End Sub

    Private Sub mnu_BASTPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BASTPembelian.Click
        Dim JudulForm = "BAST Pembelian"
        usc_BASTPembelian = New wpfUsc_BASTPembelian With {
            .JudulForm = JudulForm
        }
        BukaUserControlDalamTab(usc_BASTPembelian, JudulForm)
    End Sub

    ' ============================================================
    ' PEMBELIAN - INVOICE DENGAN PO
    ' ============================================================
    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal_Rutin.Click
        Dim JudulForm = "Invoice Pembelian - Rutin"
        usc_InvoicePembelian_DenganPO_Lokal_Rutin = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_DenganPO_Lokal_Rutin, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal_Termin.Click
        Dim JudulForm = "Invoice Pembelian - Termin"
        usc_InvoicePembelian_DenganPO_Lokal_Termin = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Termin
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_DenganPO_Lokal_Termin, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor_Rutin.Click
        Dim JudulForm = "Invoice Pembelian - Impor - Rutin"
        usc_InvoicePembelian_DenganPO_Impor_Rutin = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_DenganPO_Impor_Rutin, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor_Termin.Click
        Dim JudulForm = "Invoice Pembelian Impor - Termin"
        usc_InvoicePembelian_DenganPO_Impor_Termin = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Termin
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_DenganPO_Impor_Termin, JudulForm)
    End Sub

    ' ============================================================
    ' PEMBELIAN - INVOICE TANPA PO
    ' ============================================================
    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_Barang.Click
        Dim JudulForm = "Invoice Pembelian Tanpa PO - Barang"
        usc_InvoicePembelian_TanpaPO_Lokal_Barang = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Barang,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Lokal_Barang, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_Jasa.Click
        Dim JudulForm = "Invoice Pembelian Tanpa PO - Jasa"
        usc_InvoicePembelian_TanpaPO_Lokal_Jasa = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Jasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Lokal_Jasa, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Click
        Dim JudulForm = "Invoice Pembelian Tanpa PO - Barang dan Jasa"
        usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_BarangDanJasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Click
        Dim JudulForm = "Invoice Pembelian Tanpa PO - Jasa Konstruksi"
        usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_JasaKonstruksi,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor_Barang.Click
        Dim JudulForm = "Invoice Pembelian Tanpa PO - Impor - Barang"
        usc_InvoicePembelian_TanpaPO_Impor_Barang = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Barang,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Impor_Barang, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor_Jasa.Click
        Dim JudulForm = "Invoice Pembelian Tanpa PO - Impor - Jasa"
        usc_InvoicePembelian_TanpaPO_Impor_Jasa = New wpfUsc_InvoicePembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Jasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Impor_Jasa, JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_LokalMUA_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_LokalMUA.Click
        ' TODO: Implementasi MUA (Make-Up Artist) jika diperlukan
    End Sub

    ' ============================================================
    ' PEMBELIAN - BUKU & RETUR
    ' ============================================================
    Private Sub mnu_BukuPembelian_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPembelian_Lokal.Click
        Dim JudulForm = "Buku Pembelian"
        usc_BukuPembelian_Lokal = New wpfUsc_BukuPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
        BukaUserControlDalamTab(usc_BukuPembelian_Lokal, JudulForm)
    End Sub

    Private Sub mnu_BukuPembelian_Impor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPembelian_Impor.Click
        Dim JudulForm = "Buku Pembelian - Impor"
        usc_BukuPembelian_Impor = New wpfUsc_BukuPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
        BukaUserControlDalamTab(usc_BukuPembelian_Impor, JudulForm)
    End Sub

    Private Sub mnu_ReturPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ReturPembelian.Click
        ' TODO: Implementasi Retur Pembelian
    End Sub

    ' ============================================================
    ' PENJUALAN - PO
    ' ============================================================
    Private Sub mnu_POPenjualan_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Barang.Click
        Dim JudulForm = "PO Penjualan - Barang"
        usc_POPenjualan_Barang = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_Barang.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPenjualan_Barang.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Barang.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPenjualan_Barang, JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Jasa.Click
        Dim JudulForm = "PO Penjualan - Jasa"
        usc_POPenjualan_Jasa = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_Jasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Jasa
        usc_POPenjualan_Jasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Jasa.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPenjualan_Jasa, JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_BarangDanJasa.Click
        Dim JudulForm = "PO Penjualan - Barang dan Jasa"
        usc_POPenjualan_BarangDanJasa = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_BarangDanJasa.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_BarangDanJasa
        usc_POPenjualan_BarangDanJasa.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_BarangDanJasa.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPenjualan_BarangDanJasa, JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_JasaKonstruksi.Click
        Dim JudulForm = "PO Penjualan - Jasa Konstruksi"
        usc_POPenjualan_JasaKonstruksi = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_JasaKonstruksi.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_JasaKonstruksi
        usc_POPenjualan_JasaKonstruksi.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_JasaKonstruksi.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPenjualan_JasaKonstruksi, JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Semua.Click
        Dim JudulForm = "PO Penjualan"
        usc_POPenjualan_Semua = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_POPenjualan_Semua.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Semua
        usc_POPenjualan_Semua.pnl_CRUD.Visibility = Visibility.Collapsed
        usc_POPenjualan_Semua.VisibilitasFilterJenisProdukInduk(True)
        BukaUserControlDalamTab(usc_POPenjualan_Semua, JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Ekspor.Click
        Dim JudulForm = "PO Penjualan - Ekspor"
        usc_POPenjualan_Ekspor = New wpfUsc_POPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor
        }
        usc_POPenjualan_Ekspor.cmb_JenisProduk_Induk.SelectedValue = JenisProduk_Barang
        usc_POPenjualan_Ekspor.pnl_CRUD.Visibility = Visibility.Visible
        usc_POPenjualan_Ekspor.VisibilitasFilterJenisProdukInduk(False)
        BukaUserControlDalamTab(usc_POPenjualan_Ekspor, JudulForm)
    End Sub

    ' ============================================================
    ' PENJUALAN - SURAT JALAN & BAST
    ' ============================================================
    Private Sub mnu_SuratJalanPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_SuratJalanPenjualan.Click
        Dim JudulForm = "Surat Jalan Penjualan"
        usc_SuratJalanPenjualan = New wpfUsc_SuratJalanPenjualan With {
            .JudulForm = JudulForm
        }
        BukaUserControlDalamTab(usc_SuratJalanPenjualan, JudulForm)
    End Sub

    Private Sub mnu_BASTPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BASTPenjualan.Click
        Dim JudulForm = "Berita Serah Terima Acara (BAST) Penjualan"
        usc_BASTPenjualan = New wpfUsc_BASTPenjualan With {
            .JudulForm = JudulForm
        }
        BukaUserControlDalamTab(usc_BASTPenjualan, JudulForm)
    End Sub

    ' ============================================================
    ' PENJUALAN - INVOICE DENGAN PO
    ' ============================================================
    Private Sub mnu_InvoicePenjualan_DenganPO_Lokal_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Lokal_Rutin.Click
        Dim JudulForm = "Invoice Penjualan - Rutin"
        usc_InvoicePenjualan_DenganPO_Lokal_Rutin = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_DenganPO_Lokal_Rutin, JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Lokal_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Lokal_Termin.Click
        Dim JudulForm = "Invoice Penjualan - Termin"
        usc_InvoicePenjualan_DenganPO_Lokal_Termin = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Termin
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_DenganPO_Lokal_Termin, JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin.Click
        Dim JudulForm = "Invoice Penjualan Ekspor - Rutin"
        usc_InvoicePenjualan_DenganPO_Ekspor_Rutin = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_DenganPO_Ekspor_Rutin, JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Ekspor_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Ekspor_Termin.Click
        Dim JudulForm = "Invoice Penjualan Ekspor - Termin"
        usc_InvoicePenjualan_DenganPO_Ekspor_Termin = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .InvoiceDenganPO = True,
            .JenisProduk_Menu = JenisProduk_Semua,
            .MetodePembayaran = MetodePembayaran_Termin
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_DenganPO_Ekspor_Termin, JudulForm)
    End Sub

    ' ============================================================
    ' PENJUALAN - INVOICE TANPA PO
    ' ============================================================
    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_Barang.Click
        Dim JudulForm = "Invoice Penjualan - Tanpa PO - Barang"
        usc_InvoicePenjualan_TanpaPO_Lokal_Barang = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Barang,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Lokal_Barang, JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa.Click
        Dim JudulForm = "Invoice Penjualan - Tanpa PO - Jasa"
        usc_InvoicePenjualan_TanpaPO_Lokal_Jasa = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Jasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Lokal_Jasa, JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.Click
        Dim JudulForm = "Invoice Penjualan - Tanpa PO - Barang dan Jasa"
        usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_BarangDanJasa,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa, JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Click
        Dim JudulForm = "Invoice Penjualan - Tanpa PO - Jasa Konstruksi"
        usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_JasaKonstruksi,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi, JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Ekspor.Click
        Dim JudulForm = "Invoice Penjualan Tanpa PO - Ekspor"
        usc_InvoicePenjualan_TanpaPO_Ekspor = New wpfUsc_InvoicePenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .InvoiceDenganPO = False,
            .JenisProduk_Menu = JenisProduk_Barang,
            .MetodePembayaran = MetodePembayaran_Normal
        }
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Ekspor, JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Asset_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Asset.Click
        ' Redirect ke Daftar Penyusutan Asset Tetap (sama seperti di frm_BOOKU)
        BukaModul_DaftarPenyusutanAssetTetap()
    End Sub

    ' ============================================================
    ' PENJUALAN - BUKU & RETUR
    ' ============================================================
    Private Sub mnu_BukuPenjualan_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualan_Lokal.Click
        Dim JudulForm = "Buku Penjualan"
        usc_BukuPenjualan_Lokal = New wpfUsc_BukuPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_BukuPenjualan_Lokal.JenisPenjualan = usc_BukuPenjualan_Lokal.JenisPenjualan_Rutin
        BukaUserControlDalamTab(usc_BukuPenjualan_Lokal, JudulForm)
    End Sub

    Private Sub mnu_BukuPenjualan_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualan_Ekspor.Click
        Dim JudulForm = "Buku Penjualan - Ekspor"
        usc_BukuPenjualan_Ekspor = New wpfUsc_BukuPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor
        }
        usc_BukuPenjualan_Ekspor.JenisPenjualan = usc_BukuPenjualan_Ekspor.JenisPenjualan_Rutin
        BukaUserControlDalamTab(usc_BukuPenjualan_Ekspor, JudulForm)
    End Sub

    Private Sub mnu_BukuPenjualanEceran_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualanEceran.Click
        Dim JudulForm = "Buku Penjualan Eceran"
        usc_BukuPenjualanEceran = New wpfUsc_BukuPenjualanEceran
        BukaUserControlDalamTab(usc_BukuPenjualanEceran, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanReturPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanReturPenjualan.Click
        ' TODO: Implementasi Retur Penjualan
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - BANK & CASH
    ' ============================================================
    Private Sub mnu_BukuCashAdvance_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuCashAdvance.Click
        Dim JudulForm = "Buku Cash Advance"
        usc_BukuCashAdvance = New wpfUsc_BukuBesar With {
            .FungsiModul = Halaman_BUKUCASHADVANCE
        }
        BukaUserControlDalamTab(usc_BukuCashAdvance, JudulForm)
    End Sub

    Private Sub mnu_BukuBankGaransi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBankGaransi.Click
        Dim JudulForm = "Buku Bank Garansi"
        usc_BukuBankGaransi = New wpfUsc_BukuBankGaransi
        BukaUserControlDalamTab(usc_BukuBankGaransi, JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - GAJI
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangBPJSKesehatan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBPJSKesehatan.Click
        BukaModul_BukuPengawasanHutangBPJSKesehatan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBPJSKesehatan()
        Dim JudulForm = "Buku Pengawasan Hutang BPJS Kesehatan"
        usc_BukuPengawasanHutangBPJSKesehatan = New wpfUsc_BukuPengawasanTurunanGaji With {
            .JudulForm = JudulForm,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGBPJSKESEHATAN,
            .COAHutang = KodeTautanCOA_HutangBpjsKesehatan,
            .TabelPengawasan = "tbl_PengawasanHutangBpjsKesehatan",
            .AwalanBPH = AwalanBPHKS,
            .KolomPotongan = "Potongan_Hutang_BPJS_Kesehatan"
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangBPJSKesehatan, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangBPJSKetenagakerjaan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBPJSKetenagakerjaan.Click
        BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
        Dim JudulForm = "Buku Pengawasan Hutang BPJS Ketenagakerjaan"
        usc_BukuPengawasanHutangBPJSKetenagakerjaan = New wpfUsc_BukuPengawasanTurunanGaji With {
            .JudulForm = JudulForm,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGBPJSKETENAGAKERJAAN,
            .COAHutang = KodeTautanCOA_HutangBpjsKetenagakerjaan,
            .TabelPengawasan = "tbl_PengawasanHutangBpjsKetenagakerjaan",
            .AwalanBPH = AwalanBPHTK,
            .KolomPotongan = "Potongan_Hutang_BPJS_Ketenagakerjaan"
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangBPJSKetenagakerjaan, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangKoperasiKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangKoperasiKaryawan.Click
        BukaModul_BukuPengawasanHutangKoperasiKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangKoperasiKaryawan()
        Dim JudulForm = "Buku Pengawasan Hutang Koperasi Karyawan"
        usc_BukuPengawasanHutangKoperasiKaryawan = New wpfUsc_BukuPengawasanTurunanGaji With {
            .JudulForm = JudulForm,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGKOPERASIKARYAWAN,
            .COAHutang = KodeTautanCOA_HutangKoperasiKaryawan,
            .TabelPengawasan = "tbl_PengawasanHutangKoperasiKaryawan",
            .AwalanBPH = AwalanBPHKK,
            .KolomPotongan = "Potongan_Hutang_Koperasi"
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangKoperasiKaryawan, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangSerikat_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangSerikat.Click
        BukaModul_BukuPengawasanHutangSerikat()
    End Sub
    Sub BukaModul_BukuPengawasanHutangSerikat()
        Dim JudulForm = "Buku Pengawasan Hutang Serikat"
        usc_BukuPengawasanHutangSerikat = New wpfUsc_BukuPengawasanTurunanGaji With {
            .JudulForm = JudulForm,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGSERIKAT,
            .COAHutang = KodeTautanCOA_HutangSerikat,
            .TabelPengawasan = "tbl_PengawasanHutangSerikat",
            .AwalanBPH = AwalanBPHS,
            .KolomPotongan = "Potongan_Hutang_Serikat"
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangSerikat, JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - HUTANG USAHA
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha - Non Afiliasi"
        usc_BukuPengawasanHutangUsaha_NonAfiliasi = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Lokal,
            .KodeMataUang = KodeMataUang_IDR,
            .JenisRelasi_Induk = JenisRelasi_NonAfiliasi
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_NonAfiliasi, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha -  Afiliasi"
        usc_BukuPengawasanHutangUsaha_Afiliasi = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Lokal,
            .KodeMataUang = KodeMataUang_IDR,
            .JenisRelasi_Induk = JenisRelasi_Afiliasi
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Afiliasi, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Semua.Click
        Dim JudulForm = "Buku Pengawasan Hutang Usaha"
        usc_BukuPengawasanHutangUsaha = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Lokal,
            .KodeMataUang = KodeMataUang_IDR,
            .JenisRelasi_Induk = Pilihan_Semua,
            .COAHutang = Kosongan
        }
        usc_BukuPengawasanHutangUsaha.VisibilitasFilterJenisRelasi(True)
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_USD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha - Impor - USD"
        usc_BukuPengawasanHutangUsaha_Impor_USD = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Impor,
            .KodeMataUang = KodeMataUang_USD,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_USD, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_AUD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_AUD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_AUD()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha - Impor - AUD"
        usc_BukuPengawasanHutangUsaha_Impor_AUD = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Impor,
            .KodeMataUang = KodeMataUang_AUD,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_AUD, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_JPY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_JPY()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_JPY()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha - Impor - JPY"
        usc_BukuPengawasanHutangUsaha_Impor_JPY = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Impor,
            .KodeMataUang = KodeMataUang_JPY,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_JPY, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_CNY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_CNY()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_CNY()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha - Impor - CNY"
        usc_BukuPengawasanHutangUsaha_Impor_CNY = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Impor,
            .KodeMataUang = KodeMataUang_CNY,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_CNY, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_EUR.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_EUR()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_EUR()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha - Impor - EUR"
        usc_BukuPengawasanHutangUsaha_Impor_EUR = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Impor,
            .KodeMataUang = KodeMataUang_EUR,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_EUR, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_SGD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_SGD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_SGD()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha - Impor - SGD"
        usc_BukuPengawasanHutangUsaha_Impor_SGD = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Impor,
            .KodeMataUang = KodeMataUang_SGD,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_SGD, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_GBP.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_GBP()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_GBP()
        Dim JudulForm = "Buku Pengawasan Hutang Usaha - Impor - GBP"
        usc_BukuPengawasanHutangUsaha_Impor_GBP = New wpfUsc_BukuPengawasanHutangUsaha With {
            .AsalPembelian = AsalPembelian_Impor,
            .KodeMataUang = KodeMataUang_GBP,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_GBP, JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - HUTANG LAINNYA
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBank.Click
        BukaModul_BukuPengawasanHutangBank()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBank()
        Dim JudulForm = "Buku Pengawasan Hutang Bank"
        usc_BukuPengawasanHutangBank = New wpfUsc_BukuPengawasanHutangBankLeasing With {
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGBANK,
            .BankLeasing = bl_Bank,
            .JudulForm = JudulForm,
            .COAHutang = KodeTautanCOA_HutangBank,
            .TabelPengawasan = "tbl_PengawasanHutangBank",
            .TabelAngsuran = "tbl_JadwalAngsuranHutangBank",
            .KolomNomorBPH = "Nomor_BPHB"
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangBank, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangLeasing_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangLeasing.Click
        BukaModul_BukuPengawasanHutangLeasing()
    End Sub
    Sub BukaModul_BukuPengawasanHutangLeasing()
        Dim JudulForm = "Buku Pengawasan Hutang Leasing"
        usc_BukuPengawasanHutangLeasing = New wpfUsc_BukuPengawasanHutangBankLeasing With {
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGLEASING,
            .BankLeasing = bl_Leasing,
            .JudulForm = JudulForm,
            .COAHutang = KodeTautanCOA_HutangLeasing,
            .TabelPengawasan = "tbl_PengawasanHutangLeasing",
            .TabelAngsuran = "tbl_JadwalAngsuranHutangLeasing",
            .KolomNomorBPH = "Nomor_BPHL"
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangLeasing, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPihakKetiga.Click
        BukaModul_BukuPengawasanHutangPihakKetiga()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPihakKetiga()
        Dim JudulForm = "Buku Pengawasan Hutang Pihak Ketiga"
        usc_BukuPengawasanHutangPihakKetiga = New wpfUsc_BukuPengawasanHutangPihakKetiga With {
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPIHAKKETIGA,
            .JudulForm = JudulForm,
            .COAHutang = KodeTautanCOA_HutangPihakKetiga
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPihakKetiga, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangAfiliasi.Click
        BukaModul_BukuPengawasanHutangAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangAfiliasi()
        Dim JudulForm = "Buku Pengawasan Hutang Afiliasi"
        usc_BukuPengawasanHutangAfiliasi = New wpfUsc_BukuPengawasanHutangAfiliasi With {
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGAFILIASI,
            .JudulForm = JudulForm,
            .COAHutang = KodeTautanCOA_HutangAfiliasi
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangAfiliasi, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangKaryawan.Click
        BukaModul_BukuPengawasanHutangKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangKaryawan()
        Dim JudulForm = "Buku Pengawasan Hutang Karyawan"
        usc_BukuPengawasanHutangKaryawan = New wpfUsc_BukuPengawasanHutangKaryawan With {
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGKARYAWAN,
            .COAHutang = KodeTautanCOA_HutangKaryawan
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangKaryawan, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPemegangSaham.Click
        BukaModul_BukuPengawasanHutangPemegangSaham()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPemegangSaham()
        Dim JudulForm = "Buku Pengawasan Hutang Pemegang Saham"
        usc_BukuPengawasanHutangPemegangSaham = New wpfUsc_BukuPengawasanHutangPemegangSaham With {
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPEMEGANGSAHAM,
            .COAHutang = KodeTautanCOA_HutangPemegangSaham
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPemegangSaham, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangDividen_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangDividen.Click
        BukaModul_BukuPengawasanHutangDividen()
    End Sub
    Sub BukaModul_BukuPengawasanHutangDividen()
        Dim JudulForm = "Buku Pengawasan Hutang Dividen"
        usc_BukuPengawasanHutangDividen = New wpfUsc_BukuPengawasanHutangDividen
        BukaUserControlDalamTab(usc_BukuPengawasanHutangDividen, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangLainnya.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - PIUTANG USAHA
    ' ============================================================
    Private Sub mnu_BukuPengawasanPiutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_NonAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_NonAfiliasi()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Non Afiliasi"
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .KodeMataUang = KodeMataUang_IDR,
            .JenisRelasi_Induk = JenisRelasi_NonAfiliasi
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_NonAfiliasi, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Afiliasi"
        usc_BukuPengawasanPiutangUsaha_Afiliasi = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .KodeMataUang = KodeMataUang_IDR,
            .JenisRelasi_Induk = JenisRelasi_Afiliasi
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Afiliasi, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Semua.Click
        Dim JudulForm = "Buku Pengawasan Piutang Usaha"
        usc_BukuPengawasanPiutangUsaha = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Lokal,
            .KodeMataUang = KodeMataUang_IDR,
            .JenisRelasi_Induk = Pilihan_Semua,
            .COAPiutang = Kosongan
        }
        usc_BukuPengawasanPiutangUsaha.VisibilitasFilterJenisRelasi(True)
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_USD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - USD"
        usc_BukuPengawasanPiutangUsaha_Ekspor_USD = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .KodeMataUang = KodeMataUang_USD,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_USD, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_AUD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_AUD()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - AUD"
        usc_BukuPengawasanPiutangUsaha_Ekspor_AUD = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .KodeMataUang = KodeMataUang_AUD,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_AUD, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_JPY()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_JPY()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - JPY"
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .KodeMataUang = KodeMataUang_JPY,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_JPY, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_CNY()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_CNY()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - CNY"
        usc_BukuPengawasanPiutangUsaha_Ekspor_CNY = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .KodeMataUang = KodeMataUang_CNY,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_CNY, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_EUR()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_EUR()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - EUR"
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .KodeMataUang = KodeMataUang_EUR,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_EUR, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_SGD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_SGD()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - SGD"
        usc_BukuPengawasanPiutangUsaha_Ekspor_SGD = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .KodeMataUang = KodeMataUang_SGD,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_SGD, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_GBP()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_GBP()
        Dim JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - GBP"
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP = New wpfUsc_BukuPengawasanPiutangUsaha With {
            .DestinasiPenjualan = DestinasiPenjualan_Ekspor,
            .KodeMataUang = KodeMataUang_GBP,
            .JenisRelasi_Induk = Pilihan_Semua
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_GBP, JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - PIUTANG LAINNYA
    ' ============================================================
    Private Sub mnu_BukuPengawasanPiutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangPihakKetiga.Click
        BukaModul_BukuPengawasanPiutangPihakKetiga()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangPihakKetiga()
        Dim JudulForm = "Buku Pengawasan Piutang Pihak Ketiga"
        usc_BukuPengawasanPiutangPihakKetiga = New wpfUsc_BukuPengawasanPiutangPihakKetiga With {
            .NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGPIHAKKETIGA,
            .JudulForm = JudulForm,
            .COAPiutang = KodeTautanCOA_PiutangPihakKetiga
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangPihakKetiga, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangAfiliasi.Click
        BukaModul_BukuPengawasanPiutangAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangAfiliasi()
        Dim JudulForm = "Buku Pengawasan Piutang Afiliasi"
        usc_BukuPengawasanPiutangAfiliasi = New wpfUsc_BukuPengawasanPiutangAfiliasi With {
            .NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGAFILIASI,
            .JudulForm = JudulForm,
            .COAPiutang = KodeTautanCOA_PiutangAfiliasi
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangAfiliasi, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangKaryawan.Click
        BukaModul_BukuPengawasanPiutangKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangKaryawan()
        Dim JudulForm = "Buku Pengawasan Piutang Karyawan"
        usc_BukuPengawasanPiutangKaryawan = New wpfUsc_BukuPengawasanPiutangKaryawan With {
            .NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGKARYAWAN,
            .COAPiutang = KodeTautanCOA_PiutangKaryawan
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangKaryawan, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangPemegangSaham.Click
        BukaModul_BukuPengawasanPiutangPemegangSaham()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangPemegangSaham()
        Dim JudulForm = "Buku Pengawasan Piutang Pemegang Saham"
        usc_BukuPengawasanPiutangPemegangSaham = New wpfUsc_BukuPengawasanPiutangPemegangSaham With {
            .NamaHalaman = Halaman_BUKUPENGAWASANPIUTANGPEMEGANGSAHAM,
            .COAPiutang = KodeTautanCOA_PiutangPemegangSaham
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangPemegangSaham, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanDepositOperasional_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanDepositOperasional.Click
        BukaModul_BukuPengawasanDepositOperasional()
    End Sub
    Sub BukaModul_BukuPengawasanDepositOperasional(Optional DariDataAwal As Boolean = False)
        Dim JudulForm As String
        If DariDataAwal AndAlso JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = "Data Awal Deposit Operasional"
        Else
            JudulForm = "Buku Pengawasan Deposit Operasional"
        End If
        usc_BukuPengawasanDepositOperasional = New wpfUsc_BukuPengawasanDepositOperasional With {
            .NamaHalaman = Halaman_BUKUPENGAWASANDEPOSITOPERASIONAL,
            .COAPiutang = KodeTautanCOA_DepositOperasional,
            .JudulForm = JudulForm
        }
        BukaUserControlDalamTab(usc_BukuPengawasanDepositOperasional, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangDividen_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangDividen.Click
        Dim JudulForm = "Buku Pengawasan Piutang Dividen"
        usc_BukuPengawasanPiutangDividen = New wpfUsc_BukuPengawasanPiutangDividen
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangDividen, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangLainnya.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - BUKTI & PEMINDABUKUAN
    ' ============================================================
    Private Sub mnu_BukuPengawasanBuktiPenerimaanBankCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPenerimaanBankCash.Click
        Dim JudulForm = "Buku Pengawasan Bukti Penerimaan Bank-Cash"
        usc_BukuPengawasanBuktiPenerimaanBankCash = New wpfUsc_BukuPengawasanBuktiPenerimaanBankCash
        BukaUserControlDalamTab(usc_BukuPengawasanBuktiPenerimaanBankCash, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPengeluaranBankCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPengeluaranBankCash.Click
        Dim JudulForm = "Buku Pengawasan Bukti Pengeluaran Bank-Cash"
        usc_BukuPengawasanBuktiPengeluaranBankCash = New wpfUsc_BukuPengawasanBuktiPengeluaranBankCash
        BukaUserControlDalamTab(usc_BukuPengawasanBuktiPengeluaranBankCash, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPemindabukuan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPemindabukuan.Click
        Dim JudulForm = "Buku Pengawasan Pemindahbukuan"
        usc_BukuPengawasanPemindahbukuan = New wpfUsc_BukuPengawasanPemindahbukuan
        BukaUserControlDalamTab(usc_BukuPengawasanPemindahbukuan, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanAktivaLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanAktivaLainnya.Click
        Dim JudulForm = "Buku Pengawasan Aktiva Lain-lain"
        usc_BukuPengawasanAktivaLainnya = New wpfUsc_BukuPengawasanAktivaLainnya With {
            .NamaHalaman = Halaman_BUKUPENGAWASANAKTIVALAINNYA,
            .JudulForm = JudulForm
        }
        BukaUserControlDalamTab(usc_BukuPengawasanAktivaLainnya, JudulForm)
    End Sub

    ' ============================================================
    ' PENGAJUAN
    ' ============================================================
    Private Sub mnu_PengajuanPembayaranPembelianTunai_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranPembelianTunai.Click
    End Sub

    Private Sub mnu_PengajuanPembayaranHutangUsaha_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranHutangUsaha.Click
    End Sub

    Private Sub mnu_PengajuanPembayaranHutangPajak_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranHutangPajak.Click
    End Sub

    Private Sub mnu_PengajuanPembayaranHutangBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranHutangBank.Click
    End Sub

    Private Sub mnu_PengajuanPembayaranHutangLeasing_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranHutangLeasing.Click
    End Sub

    Private Sub mnu_PengajuanPembayaranHutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranHutangAfiliasi.Click
    End Sub

    Private Sub mnu_PengajuanPembayaranHutangLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranHutangLainnya.Click
    End Sub

    Private Sub mnu_PengajuanPembayaranKasbon_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranKasbon.Click
    End Sub

    Private Sub mnu_PengajuanPembayaranInvestasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPembayaranInvestasi.Click
    End Sub

    Private Sub mnu_PengajuanPemindahbukuan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPemindahbukuan.Click
    End Sub

    Private Sub mnu_PengajuanLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanLainnya.Click
    End Sub

    Private Sub mnu_PengajuanPO_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PengajuanPO.Click
    End Sub

    ' ============================================================
    ' STOCK OPNAME
    ' ============================================================
    Private Sub mnu_StockOpname_BahanPenolong_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BahanPenolong.Click
        BukaModul_StockOpname_BahanPenolong()
        Dim JudulForm = "Stock Opname Bahan Penolong"
        usc_BahanPenolong = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BahanPenolong
        }
        usc_BahanPenolong.JenisPengecekan_Menu = usc_BahanPenolong.JenisPengecekan_CekFisik
        BukaUserControlDalamTab(usc_BahanPenolong, JudulForm)
    End Sub
    Sub BukaModul_StockOpname_BahanPenolong()
        Dim JudulForm = "Stock Opname Bahan Penolong"
        usc_BahanPenolong = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BahanPenolong
        }
        usc_BahanPenolong.JenisPengecekan_Menu = usc_BahanPenolong.JenisPengecekan_CekFisik
        BukaUserControlDalamTab(usc_BahanPenolong, JudulForm)
    End Sub

    Private Sub mnu_StockOpname_BahanBaku_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BahanBaku.Click
        BukaModul_StockOpname_BahanBaku()
    End Sub
    Sub BukaModul_StockOpname_BahanBaku()
        Dim JudulForm = "Stock Opname Bahan Baku"
        usc_BahanBaku = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BahanBaku
        }
        usc_BahanBaku.JenisPengecekan_Menu = usc_BahanBaku.JenisPengecekan_CekFisik
        BukaUserControlDalamTab(usc_BahanBaku, JudulForm)
    End Sub
    Private Sub mnu_StockOpname_BarangDalamProses_CekFisik_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangDalamProses_CekFisik.Click
        Dim JudulForm = "Stock Opname Barang Dalam Proses (Cek Fisik)"
        usc_BarangDalamProses_CekFisik = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BarangDalamProses
        }
        usc_BarangDalamProses_CekFisik.JenisPengecekan_Menu = usc_BarangDalamProses_CekFisik.JenisPengecekan_CekFisik
        BukaUserControlDalamTab(usc_BarangDalamProses_CekFisik, JudulForm)
    End Sub

    Private Sub mnu_StockOpname_BarangDalamProses_TarikanData_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangDalamProses_TarikanData.Click
        Dim JudulForm = "Stock Opname Barang Dalam Proses (Tarikan Data)"
        usc_BarangDalamProses_TarikanData = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BarangDalamProses
        }
        usc_BarangDalamProses_TarikanData.JenisPengecekan_Menu = usc_BarangDalamProses_TarikanData.JenisPengecekan_TarikanData
        BukaUserControlDalamTab(usc_BarangDalamProses_TarikanData, JudulForm)
    End Sub

    Private Sub mnu_StockOpname_BarangJadi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangJadi.Click
        Dim JudulForm = "Stock Opname Barang Jadi"
        usc_BarangJadi = New wpfUsc_StockOpname With {
            .JenisStok_Menu = JenisStok_BarangJadi
        }
        usc_BarangJadi.JenisPengecekan_Menu = usc_BarangJadi.JenisPengecekan_CekFisik
        BukaUserControlDalamTab(usc_BarangJadi, JudulForm)
    End Sub

    ' ============================================================
    ' AKUNTANSI - JURNAL ADJUSMENT
    ' ============================================================
    Private Sub mnu_JurnalAdjusment_Penyusutan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Penyusutan.Click
        BukaHalamanAdjusmentPenyusutanAsset()
    End Sub
    Sub BukaHalamanAdjusmentPenyusutanAsset()
        Dim JudulForm = "Adjusment Penyusutan Asset"
        usc_Adjusment_PenyusutanAsset = New wpfUsc_Adjusment_PenyusutanAsset
        BukaUserControlDalamTab(usc_Adjusment_PenyusutanAsset, JudulForm)
    End Sub

    Private Sub mnu_JurnalAdjusment_Amortisasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Amortisasi.Click
        BukaHalamanAdjusmentAmortisasi()
    End Sub
    Sub BukaHalamanAdjusmentAmortisasi()
        Dim JudulForm = "Adjusment Amortisasi Biaya"
        usc_Adjusment_Amortisasi = New wpfUsc_Adjusment_Amortisasi
        BukaUserControlDalamTab(usc_Adjusment_Amortisasi, JudulForm)
    End Sub

    Private Sub mnu_JurnalAdjusment_Forex_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Forex.Click
        Dim JudulForm = "Adjusment Forex - " & TahunBukuAktif
        usc_Adjusment_Forex = New wpfUsc_JurnalAdjusment_Forex
        BukaUserControlDalamTab(usc_Adjusment_Forex, JudulForm)
    End Sub

    Private Sub mnu_JurnalAdjusment_HPP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_HPP.Click
        Dim JudulForm = "Adjusment HPP - " & TahunBukuAktif
        usc_JurnalAdjusment_HPP = New wpfUsc_JurnalAdjusment_HPP
        BukaUserControlDalamTab(usc_JurnalAdjusment_HPP, JudulForm)
    End Sub

    Private Sub mnu_LaporanAktivitasTransaksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LaporanAktivitasTransaksi.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    ' ============================================================
    ' MANAJEMEN ASSET
    ' ============================================================
    Private Sub mnu_ManajemenAmortisasiAssetTidakBerwujud_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenAmortisasiAssetTidakBerwujud.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_BukuPenjualanAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualanAssetTetap.Click
        Dim JudulForm = "Buku Penjualan - Asset"
        usc_BukuPenjualan_Asset = New wpfUsc_BukuPenjualan With {
            .JudulForm = JudulForm,
            .DestinasiPenjualan = DestinasiPenjualan_Lokal
        }
        usc_BukuPenjualan_Asset.JenisPenjualan = usc_BukuPenjualan_Asset.JenisPenjualan_Asset
        BukaUserControlDalamTab(usc_BukuPenjualan_Asset, JudulForm)
    End Sub

    Private Sub mnu_BukuDisposalAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuDisposalAssetTetap.Click
        Dim JudulForm = "Buku Disposal Asset Tetap"
        usc_BukuDisposalAssetTetap = New wpfUsc_BukuDisposalAssetTetap
        BukaUserControlDalamTab(usc_BukuDisposalAssetTetap, JudulForm)
    End Sub

    ' ============================================================
    ' PAJAK
    ' ============================================================
    Private Sub mnu_ProfilPajakPerusahaan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ProfilPajakPerusahaan.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_PerhitunganPajakPajakBulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PerhitunganPajakPajakBulanan.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal21_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal21.Click
        BukaModul_BukuPengawasanHutangPPhPasal21()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal21()
        Dim JudulForm = "Buku Pengawasan Hutang PPh Pasal 21"
        usc_BukuPengawasanHutangPPhPasal21 = New wpfUsc_BukuPengawasanHutangPPhPasal21 With {
            .JenisPajak = JenisPajak_PPhPasal21,
            .AwalanBP = AwalanBPHP21,
            .COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal21_100,
            .COAHutangPajak_401 = KodeTautanCOA_HutangPPhPasal21_401,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL21
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal21, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal22_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22_Lokal.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal22_Impor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22_Impor.Click
        Dim JudulForm = "Buku Pengawasan PPh Pasal 22 - Impor"
        usc_BukuPengawasanHutangPPhPasal22_Impor = New wpfUsc_BukuPengawasanHutangPPhPasal22_Impor With {
            .JenisPajak = JenisPajak_PPhPasal22_Impor,
            .COAPajak = KodeTautanCOA_PPhPasal22DibayarDimuka_Impor,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL22_IMPOR
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal22_Impor, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal23_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal23.Click
        BukaModul_BukuPengawasanHutangPPhPasal23()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal23()
        Dim JudulForm = "Buku Pengawasan Hutang PPh Pasal 23"
        usc_BukuPengawasanHutangPPhPasal23 = New wpfUsc_BukuPengawasanHutangPPhPasal23 With {
            .JenisPajak = JenisPajak_PPhPasal23,
            .AwalanBP = AwalanBPHP23,
            .COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal23_100,
            .COAHutangPajak_101 = KodeTautanCOA_HutangPPhPasal23_101,
            .COAHutangPajak_102 = KodeTautanCOA_HutangPPhPasal23_102,
            .COAHutangPajak_103 = KodeTautanCOA_HutangPPhPasal23_103,
            .COAHutangPajak_104 = KodeTautanCOA_HutangPPhPasal23_104,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL23
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal23, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal42_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal42.Click
        BukaModul_BukuPengawasanHutangPPhPasal42()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal42()
        Dim JudulForm = "Buku Pengawasan Hutang PPh Pasal 4 (2)"
        usc_BukuPengawasanHutangPPhPasal42 = New wpfUsc_BukuPengawasanHutangPPhPasal42 With {
            .JenisPajak = JenisPajak_PPhPasal42,
            .AwalanBP = AwalanBPHP42,
            .COAHutangPajak_402 = KodeTautanCOA_HutangPPhPasal42_402,
            .COAHutangPajak_403 = KodeTautanCOA_HutangPPhPasal42_403,
            .COAHutangPajak_409 = KodeTautanCOA_HutangPPhPasal42_409,
            .COAHutangPajak_419 = KodeTautanCOA_HutangPPhPasal42_419,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL42
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal42, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal25_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal25.Click
        BukaModul_BukuPengawasanHutangPPhPasal25()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal25()
        Dim JudulForm = "Buku Pengawasan Hutang PPh Pasal 25"
        usc_BukuPengawasanHutangPPhPasal25 = New wpfUsc_BukuPengawasanHutangPPhPasal25 With {
            .JenisPajak = JenisPajak_PPhPasal25,
            .AwalanBP = AwalanBPHP25,
            .COAHutangPajak = KodeTautanCOA_HutangPPhPasal25,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL25
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal25, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal26_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal26.Click
        BukaModul_BukuPengawasanHutangPPhPasal26()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal26()
        Dim JudulForm = "Buku Pengawasan Hutang PPh Pasal 26"
        usc_BukuPengawasanHutangPPhPasal26 = New wpfUsc_BukuPengawasanHutangPPhPasal26 With {
            .JenisPajak = JenisPajak_PPhPasal26,
            .AwalanBP = AwalanBPHP26,
            .COAHutangPajak_100 = KodeTautanCOA_HutangPPhPasal26_100,
            .COAHutangPajak_101 = KodeTautanCOA_HutangPPhPasal26_101,
            .COAHutangPajak_102 = KodeTautanCOA_HutangPPhPasal26_102,
            .COAHutangPajak_103 = KodeTautanCOA_HutangPPhPasal26_103,
            .COAHutangPajak_104 = KodeTautanCOA_HutangPPhPasal26_104,
            .COAHutangPajak_105 = KodeTautanCOA_HutangPPhPasal26_105,
            .NamaHalaman = Halaman_BUKUPENGAWASANHUTANGPPHPASAL26
        }
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal26, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal29_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal29.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_PPN_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PPN.Click
        BukaModul_BukuPengawasanPelaporanPPN()
    End Sub
    Sub BukaModul_BukuPengawasanPelaporanPPN()
        Dim JudulForm = "Buku Pengawasan Pelaporan PPN"
        usc_BukuPengawasanPelaporanPPN = New wpfUsc_BukuPengawasanPelaporanPPN With {
            .JenisPajak = JenisPajak_PPN,
            .AwalanBP = AwalanBPHPPN,
            .COAHutangPajak = KodeTautanCOA_HutangPPN,
            .NamaHalaman = Halaman_BUKUPENGAWASANPELAPORANPPN
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPelaporanPPN, JudulForm)
    End Sub

    Private Sub mnu_KetetapanPajak_Click(sender As Object, e As RoutedEventArgs) Handles mnu_KetetapanPajak.Click
        BukaModul_BukuPengawasanKetetapanPajak()
    End Sub
    Sub BukaModul_BukuPengawasanKetetapanPajak()
        Dim JudulForm = "Buku Pengawasan Ketetapan Pajak"
        usc_BukuPengawasanKetetapanPajak = New wpfUsc_BukuPengawasanKetetapanPajak With {
            .JenisPajak = JenisPajak_KetetapanPajak,
            .AwalanBP = AwalanBPKP,
            .NamaHalaman = Halaman_BUKUPENGAWASANKETETAPANPAJAK,
            .COAHutangPajak = KodeTautanCOA_HutangKetetapanPajak
        }
        BukaUserControlDalamTab(usc_BukuPengawasanKetetapanPajak, JudulForm)
    End Sub

    Private Sub mnu_PajakImpor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PajakImpor.Click
        Dim JudulForm = "Buku Pengawasan Pajak-pajak Impor"
        usc_BukuPengawasanPajakImpor = New wpfUsc_BukuPengawasanPajakImpor With {
            .JenisPajak = JenisPajak_PajakPajakImpor,
            .AwalanBP = AwalanBPHP23,
            .COABeaMasukImpor = KodeTautanCOA_BeaMasuk_Impor,
            .COAPPhPasal22Impor = KodeTautanCOA_PPhPasal22DibayarDimuka_Impor,
            .COAPPNMasukanImpor = KodeTautanCOA_PPNMasukan_Impor,
            .NamaHalaman = Halaman_BUKUPENGAWASANPAJAKPAJAKIMPOR
        }
        BukaUserControlDalamTab(usc_BukuPengawasanPajakImpor, JudulForm)
    End Sub

    Private Sub mnu_InputBuktiPBk_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InputBuktiPBk.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_InputKetetapanPajak_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InputKetetapanPajak.Click
        win_InputKetetapanPajak = New wpfWin_InputKetetapanPajak
        win_InputKetetapanPajak.ShowDialog()
    End Sub

    Private Sub mnu_PerhitunganEqualisasiPajakPajakTahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PerhitunganEqualisasiPajakPajakTahunan.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPotongPPh_Paid_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPotongPPh_Paid.Click
        Dim JudulForm = "Buku Pengawasan Bukti Potong PPh (Paid)"
        usc_BukuPengawasanBuktiPotongPPh_Paid = New wpfUsc_BukuPengawasanBuktiPotongPPh_Paid
        BukaUserControlDalamTab(usc_BukuPengawasanBuktiPotongPPh_Paid, JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPotongPPh_Prepaid_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPotongPPh_Prepaid.Click
        Dim JudulForm = "Buku Pengawasan Bukti Potong PPh (Prepaid)"
        usc_BukuPengawasanBuktiPotongPPh_Prepaid = New wpfUsc_BukuPengawasanBuktiPotongPPh_Prepaid
        BukaUserControlDalamTab(usc_BukuPengawasanBuktiPotongPPh_Prepaid, JudulForm)
    End Sub

    ' ============================================================
    ' USER
    ' ============================================================
    Private Sub mnu_User_Click(sender As Object, e As RoutedEventArgs) Handles mnu_User.Click
    End Sub
    Private Sub mnu_SwitchUser_Click(sender As Object, e As RoutedEventArgs) Handles mnu_SwitchUser.Click
        StatusMenuPosisiLogout()
        TutupSemuaTab()
        KeluarDariSemuaModul()
        BukaFormLogin()
        If LevelUserAktif < LevelUser_81_TimIT Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ORDER BY Tahun_Buku ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Do While dr.Read 'Loop ini untuk mengambil value Tahun Buku yang paling akhir.
                TahunBukuBaru = dr.Item("Tahun_Buku")
            Loop
            AksesDatabase_General(Tutup)
            If TahunBukuBaru <> TahunBukuAktif Then
                win_GantiTahunBuku = New wpfWin_GantiTahunBuku
                win_GantiTahunBuku.ProsesGantiTahunBuku()
            End If
        End If
    End Sub

    Private Sub mnu_GantiPassword_Click(sender As Object, e As RoutedEventArgs) Handles mnu_GantiPassword.Click
        win_GantiPassword = New wpfWin_GantiPassword
        win_GantiPassword.ResetForm()
        win_GantiPassword.ShowDialog()
    End Sub

    Private Sub mnu_PeranOperator_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PeranOperator.Click
    End Sub

    Private Sub mnu_PeranManager_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PeranManager.Click
    End Sub

    Private Sub mnu_PeranDirektur_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PeranDirektur.Click
    End Sub

    Private Sub mnu_PeranTimIT_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PeranTimIT.Click
    End Sub

    Private Sub mnu_PeranAppDeveloper_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PeranAppDeveloper.Click
    End Sub

    Private Sub mnu_Log_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Log.Click
        If mnu_Log.Header = "Login" Then
            BukaFormLogin()
        Else 'Jika teks Menu "Logout" maka
            StatusMenuPosisiLogout()
            TutupSemuaTab()
            KeluarDariSemuaModul()
            MsgBox("Anda telah LOGOUT dari sistem.")
        End If
    End Sub

    ' ============================================================
    ' JENDELA
    ' ============================================================
    Private Sub mnu_Jendela_TutupSemua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Jendela_TutupSemua.Click
        TutupSemuaTab()
        KeluarDariSemuaModul()
    End Sub

    ' ============================================================
    ' TENTANG, HELP, REGISTRASI, NOTIFIKASI
    ' ============================================================
    Private Sub mnu_Tentang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Tentang.Click
        frm_Tentang.ShowDialog()
    End Sub

    Private Sub mnu_Help_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Help.Click
        frm_Help.ShowDialog()
    End Sub

    Private Sub mnu_Registrasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Registrasi.Click
        If VersiBooku_SisiAplikasi <> VersiBooku_SisiPublic Or ApdetBooku_SisiAplikasi <> ApdetBooku_SisiPublic Then
            PesanPeringatan("Registrasi hanya bisa dilakukan jika aplikasi sudah sesuai dengan Update terbaru." & Enter2Baris &
                            "Silakan lakukan update terlebih dahulu.")
            Return
        End If
        win_Registrasi = New wpfWin_Registrasi
        win_Registrasi.ShowDialog()
    End Sub

    ' ============================================================
    ' TECHNICAL SUPPORT
    ' ============================================================
    Private Sub mnu_PhpMyAdmin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PhpMyAdmin.Click
        frm_phpMyAdmin.Show()
        frm_phpMyAdmin.Focus()
    End Sub

    ' ============================================================
    ' APP DEVELOPER
    ' ============================================================
    Private Sub mnu_ManajemenAplikasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenAplikasi.Click
        Dim JudulForm = "Manajemen Aplikasi"
        usc_ManajemenAplikasi = New wpfUsc_ManajemenAplikasi
        BukaUserControlDalamTab(usc_ManajemenAplikasi, JudulForm)
    End Sub

    Private Sub mnu_ManajemenClient_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenClient.Click
        Dim JudulForm = "Manajemen Klien"
        usc_ManajemenClient = New wpfUsc_ManajemenClient
        BukaUserControlDalamTab(usc_ManajemenClient, JudulForm)
    End Sub

    Private Sub mnu_ManajemenKurs_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenKurs.Click
        Dim JudulForm = "Manajemen Kurs"
        usc_ManajemenKurs = New wpfUsc_ManajemenKurs
        BukaUserControlDalamTab(usc_ManajemenKurs, JudulForm)
    End Sub

    Private Sub mnu_DataProduk_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataProduk.Click
        Dim JudulForm = "Data Produk Aplikasi"
        usc_DataProdukApp = New wpfUsc_DataProdukApp
        BukaUserControlDalamTab(usc_DataProdukApp, JudulForm)
    End Sub

    Private Sub mnu_DataPerangkat_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataPerangkat.Click
        Dim JudulForm = "Data Perangkat Aplikasi"
        usc_DataPerangkatApp = New wpfUsc_DataPerangkatApp
        BukaUserControlDalamTab(usc_DataPerangkatApp, JudulForm)
    End Sub

    Private Sub mnu_DataVoucher_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataVoucher.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_TabPokok_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TabPokok.Click
        Dim JudulForm = "Tab Pokok"
        usc_TabPokok = New wpfUsc_TabPokok
        BukaUserControlDalamTab(usc_TabPokok, JudulForm)
    End Sub

    Private Sub mnu_TryApp_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TryApp.Click
        frm_TryApp.ShowDialog()
    End Sub

#End Region

#Region "Manajemen Menu - Visibility & Enabled"

    ''' <summary>
    ''' Sembunyikan semua menu utama
    ''' </summary>
    Sub SembunyikanSemuaMenu()
        mnu_File.Visibility = Visibility.Collapsed
        mnu_Data.Visibility = Visibility.Collapsed
        mnu_Transaksi.Visibility = Visibility.Collapsed
        mnu_Pembelian.Visibility = Visibility.Collapsed
        mnu_Penjualan.Visibility = Visibility.Collapsed
        mnu_BukuPengawasan.Visibility = Visibility.Collapsed
        mnu_Pengajuan.Visibility = Visibility.Collapsed
        mnu_StockOpname.Visibility = Visibility.Collapsed
        mnu_Akuntansi.Visibility = Visibility.Collapsed
        mnu_ManajemenAsset.Visibility = Visibility.Collapsed
        mnu_Pajak.Visibility = Visibility.Collapsed
        mnu_User.Visibility = Visibility.Collapsed
        mnu_Help.Visibility = Visibility.Collapsed
        mnu_Jendela.Visibility = Visibility.Collapsed
        mnu_Tentang.Visibility = Visibility.Collapsed
        mnu_Registrasi.Visibility = Visibility.Collapsed
        mnu_TechnicalSupport.Visibility = Visibility.Collapsed
        mnu_AppDeveloper.Visibility = Visibility.Collapsed
        mnu_Notifikasi.Visibility = Visibility.Collapsed
    End Sub

    ''' <summary>
    ''' Tampilkan menu sesuai dengan hak akses user
    ''' </summary>
    Sub TampilkanSemuaMenu()
        mnu_File.Visibility = Visibility.Visible
        mnu_Data.Visibility = Visibility.Visible
        mnu_ManajemenAsset.Visibility = Visibility.Visible
        mnu_Pajak.Visibility = Visibility.Visible
        mnu_Jendela.Visibility = Visibility.Visible
        mnu_Tentang.Visibility = Visibility.Visible
        mnu_User.Visibility = Visibility.Visible
        mnu_Help.Visibility = Visibility.Visible
        mnu_AppDeveloper.Visibility = Visibility.Visible
        mnu_TechnicalSupport.Visibility = Visibility.Visible
        mnu_Registrasi.Visibility = Visibility.Visible
        mnu_DataCOA.IsEnabled = False
        mnu_DataLawanTransaksi.IsEnabled = False
        mnu_TahunBuku.Visibility = Visibility.Collapsed
        mnu_StockOpname.Visibility = Visibility.Visible
        If ClusterFinance = 1 Then
            mnu_Transaksi.Visibility = Visibility.Visible
            mnu_Pembelian.Visibility = Visibility.Visible
            mnu_Penjualan.Visibility = Visibility.Visible
            mnu_BukuPengawasan.Visibility = Visibility.Visible
            If SistemApprovalPerusahaan = True Then mnu_Pengajuan.Visibility = Visibility.Visible
            mnu_BukuPengawasanGaji_Induk.Visibility = Visibility.Visible
            mnu_DataLawanTransaksi.IsEnabled = True
        End If
        If ClusterAccounting = 1 Then
            mnu_Akuntansi.Visibility = Visibility.Visible
            mnu_DataCOA.IsEnabled = True
            mnu_TahunBuku.Visibility = Visibility.Visible
        End If
        mnu_Notifikasi.Visibility = Visibility.Visible
    End Sub

    ''' <summary>
    ''' Status menu saat user sudah login
    ''' </summary>
    Sub StatusMenuPosisiLogin()
        SembunyikanSemuaMenu()
        TampilkanSemuaMenu()
        mnu_Log.Header = "Logout"
        mnu_SwitchUser.IsEnabled = True
        mnu_GantiPassword.IsEnabled = True
    End Sub

    ''' <summary>
    ''' Status menu saat user logout
    ''' </summary>
    Sub StatusMenuPosisiLogout()
        SembunyikanSemuaMenu()
        mnu_File.Visibility = Visibility.Visible
        mnu_Tentang.Visibility = Visibility.Visible
        mnu_User.Visibility = Visibility.Visible
        mnu_Log.Header = "Login"
        mnu_SwitchUser.IsEnabled = False
        mnu_GantiPassword.IsEnabled = False
        mnu_GantiPeran.Visibility = Visibility.Collapsed
        mnu_Help.Visibility = Visibility.Visible
        Me.Title = NamaAplikasi
        lbl_StatusUser.Text = "User :"
    End Sub

    ''' <summary>
    ''' Status menu untuk Level 1 - Operator
    ''' </summary>
    Sub StatusMenuLevel_1_Operator()
        StatusMenuLevel_2_Manager()
        If ClusterFinance = 1 Then mnu_Transaksi.Visibility = Visibility.Visible
        mnu_Akuntansi.Visibility = Visibility.Collapsed
        mnu_Pengajuan.Header = "Pengajuan"
        mnu_PengajuanPembayaranPembelianTunai.Header = "Pengajuan Pembayaran Pembelian Tunai"
        mnu_PengajuanPembayaranHutangUsaha.Header = "Pengajuan Pembayaran Hutang Usaha"
        mnu_PengajuanPembayaranHutangPajak.Header = "Pengajuan Pembayaran Hutang Pajak"
        mnu_PengajuanPembayaranHutangBank.Header = "Pengajuan Pembayaran Hutang Bank"
        mnu_PengajuanPembayaranHutangAfiliasi.Header = "Pengajuan Pembayaran Hutang Afiliasi"
        mnu_PengajuanPembayaranHutangLainnya.Header = "Pengajuan Pembayaran Hutang Lainnya"
        mnu_PengajuanPembayaranKasbon.Header = "Pengajuan Pembayaran Kasbon"
        mnu_PengajuanPembayaranInvestasi.Header = "Pengajuan Pembayaran Investasi"
        mnu_PengajuanPemindahbukuan.Header = "Pengajuan Pemindahbukuan"
        mnu_PengajuanLainnya.Header = "Pengajuan Lainnya"
        mnu_PengajuanPO.Header = "Pengajuan PO"
    End Sub

    ''' <summary>
    ''' Status menu untuk Level 2 - Manager
    ''' </summary>
    Sub StatusMenuLevel_2_Manager()
        StatusMenuLevel_3_Direktur()
    End Sub

    ''' <summary>
    ''' Status menu untuk Level 3 - Direktur
    ''' </summary>
    Sub StatusMenuLevel_3_Direktur()
        StatusMenuLevel_4_GeneralUser()
    End Sub

    ''' <summary>
    ''' Status menu untuk Level 4 - General User
    ''' </summary>
    Sub StatusMenuLevel_4_GeneralUser()
        StatusMenuLevel_9_SuperUser()
    End Sub

    ''' <summary>
    ''' Status menu untuk Level 9 - Super User
    ''' </summary>
    Sub StatusMenuLevel_9_SuperUser()
        StatusMenuLevel_81_TimIT()
        mnu_Pengaturan.IsEnabled = False
        mnu_TechnicalSupport.Visibility = Visibility.Collapsed
        mnu_Registrasi.Visibility = Visibility.Collapsed
        mnu_BuatBukuBaru.IsEnabled = False
        mnu_GantiTahunBuku.IsEnabled = False
    End Sub

    ''' <summary>
    ''' Status menu untuk Level 81 - Tim IT
    ''' </summary>
    Sub StatusMenuLevel_81_TimIT()
        StatusMenuLevel_99_AppDeveloper()
        mnu_AppDeveloper.Visibility = Visibility.Collapsed
        mnu_TrialBalance.Visibility = Visibility.Collapsed
    End Sub

    ''' <summary>
    ''' Status menu untuk Level 99 - App Developer (Master)
    ''' </summary>
    Sub StatusMenuLevel_99_AppDeveloper()
        StatusMenuPosisiLogin()
        mnu_Pengaturan.IsEnabled = True
        If SistemApprovalPerusahaan = True Then
            mnu_Transaksi.Visibility = Visibility.Collapsed
        Else
            mnu_Transaksi.Visibility = Visibility.Visible
        End If
        mnu_Pengajuan.Header = "Persetujuan"
        mnu_PengajuanPembayaranPembelianTunai.Header = "Persetujuan Pembayaran Pembelian Tunai"
        mnu_PengajuanPembayaranHutangUsaha.Header = "Persetujuan Pembayaran Hutang Usaha"
        mnu_PengajuanPembayaranHutangPajak.Header = "Persetujuan Pembayaran Hutang Pajak"
        mnu_PengajuanPembayaranHutangBank.Header = "Persetujuan Pembayaran Hutang Bank"
        mnu_PengajuanPembayaranHutangAfiliasi.Header = "Persetujuan Pembayaran Hutang Afiliasi"
        mnu_PengajuanPembayaranHutangLainnya.Header = "Persetujuan Pembayaran Hutang Lainnya"
        mnu_PengajuanPembayaranKasbon.Header = "Persetujuan Pembayaran Kasbon"
        mnu_PengajuanPembayaranInvestasi.Header = "Persetujuan Pembayaran Investasi"
        mnu_PengajuanPemindahbukuan.Header = "Persetujuan Pemindahbukuan"
        mnu_PengajuanLainnya.Header = "Persetujuan Lainnya"
        mnu_PengajuanPO.Header = "Persetujuan PO"
        mnu_BuatBukuBaru.IsEnabled = True
        mnu_GantiTahunBuku.IsEnabled = True
        mnu_AppDeveloper.Visibility = Visibility.Visible
    End Sub

#End Region

    Private Sub wpfWin_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing

        ProsesKeluarAplikasi = True

        ' Cek apakah perlu konfirmasi
        If PaksaKeluarAplikasi Then
            e.Cancel = False
        Else
            Dim hasil = MessageBox.Show(
                "Yakin akan keluar dari aplikasi..?",
                "Perhatian..!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question)

            If hasil = MessageBoxResult.No Then
                e.Cancel = True
                Return
            End If
        End If

        ' Bersihkan semua WindowsFormsHost yang ada di tab
        TutupSemuaTab()

        ' Karena ini adalah WPF Shell yang menggantikan frm_BOOKU,
        ' saat ditutup harus keluar dari aplikasi sepenuhnya
        Forms.Application.Exit()

    End Sub

#Region "Panel Notifikasi"

    ' === DATATABLE & DATAVIEW NOTIFIKASI ===
    Public datatabelNotifikasi As DataTable
    Public dataviewNotifikasi As DataView
    Public rowviewNotifikasi As DataRowView
    Public BarisNotifikasi_Terseleksi As Integer
    Public JumlahBarisNotifikasi As Integer

    ' === KOLOM DATAGRID NOTIFIKASI ===
    Dim Kolom_NomorID As New DataGridTextColumn
    Dim Kolom_JenisNotifikasi As New DataGridTextColumn
    Dim Kolom_Waktu As New DataGridTextColumn
    Dim Kolom_Konten As New DataGridTextColumn
    Dim Kolom_HalamanTarget As New DataGridTextColumn
    Dim Kolom_PesanEksekusi As New DataGridTextColumn
    Dim Kolom_StatusDibaca As New DataGridTextColumn
    Dim Kolom_StatusDieksekusi As New DataGridTextColumn

    ' === VARIABEL TERSELEKSI ===
    Public NomorIDNotifikasi_Terseleksi As Integer
    Public KontenNotifikasi_Terseleksi As String
    Public HalamanTargetNotifikasi_Terseleksi As String
    Public PesanEksekusiNotifikasi_Terseleksi As String
    Public StatusDibacaNotifikasi_Terseleksi As Integer
    Public StatusDieksekusiNotifikasi_Terseleksi As Integer

    ''' <summary>
    ''' Membuat struktur DataTable untuk Notifikasi
    ''' Dipanggil saat window di-load
    ''' </summary>
    Public Sub Buat_DataTabelNotifikasi()

        datatabelNotifikasi = New DataTable

        datatabelNotifikasi.Columns.Add("Nomor_ID", GetType(Integer))
        datatabelNotifikasi.Columns.Add("Jenis_Notifikasi")
        datatabelNotifikasi.Columns.Add("Waktu")
        datatabelNotifikasi.Columns.Add("Konten")
        datatabelNotifikasi.Columns.Add("Halaman_Target")
        datatabelNotifikasi.Columns.Add("Pesan_Eksekusi")
        datatabelNotifikasi.Columns.Add("Status_Dibaca", GetType(Integer))
        datatabelNotifikasi.Columns.Add("Status_Dieksekusi", GetType(Integer))

        StyleTabelUtama_WPF(datagridNotifikasi, datatabelNotifikasi, dataviewNotifikasi)

        TambahkanKolomTextBoxDataGrid_WPF(datagridNotifikasi, Kolom_NomorID, "Nomor_ID", "ID", 40, FormatAngka, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridNotifikasi, Kolom_JenisNotifikasi, "Jenis_Notifikasi", "Jenis", 60, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridNotifikasi, Kolom_Waktu, "Waktu", "Waktu", 80, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridNotifikasi, Kolom_Konten, "Konten", "Notifikasi", 300, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridNotifikasi, Kolom_HalamanTarget, "Halaman_Target", "Target", 80, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridNotifikasi, Kolom_PesanEksekusi, "Pesan_Eksekusi", "Pesan", 100, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridNotifikasi, Kolom_StatusDibaca, "Status_Dibaca", "Dibaca", 50, FormatAngka, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridNotifikasi, Kolom_StatusDieksekusi, "Status_Dieksekusi", "Eksekusi", 50, FormatAngka, TengahTengah, KunciUrut, Tersembunyi)

    End Sub

    ''' <summary>
    ''' Mengisi konten DataGrid Notifikasi dari database
    ''' </summary>
    Public Sub IsiKontenNotifikasi()

        datatabelNotifikasi.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabase = False Then Return

        cmd = New OdbcCommand(" SELECT * FROM tbl_Notifikasi " &
                              " WHERE Status_Dibaca = 0 " &
                              " OR Status_Dieksekusi = 0 ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader

        Do While dr.Read
            Dim NomorID As Integer = dr.Item("Nomor_ID")
            Dim JenisNotifikasi As String = dr.Item("Jenis_Notifikasi").ToString()
            Dim WaktuNotifikasi As String = dr.Item("Waktu").ToString()
            Dim Notifikasi As String = dr.Item("Notifikasi").ToString()
            Dim HalamanTarget As String = dr.Item("Halaman_Target").ToString()
            Dim Pesan As String = If(IsDBNull(dr.Item("Pesan")), "", dr.Item("Pesan").ToString())
            Dim StatusDibaca As Integer = dr.Item("Status_Dibaca")
            Dim StatusDieksekusi As Integer = dr.Item("Status_Dieksekusi")

            ' Format konten: Waktu + Notifikasi
            Dim Konten As String = WaktuNotifikasi & " :" & vbCrLf & Notifikasi

            datatabelNotifikasi.Rows.Add(NomorID, JenisNotifikasi, WaktuNotifikasi, Konten,
                                         HalamanTarget, Pesan, StatusDibaca, StatusDieksekusi)
        Loop

        AksesDatabase_Transaksi(Tutup)

        JumlahBarisNotifikasi = datatabelNotifikasi.Rows.Count

        ' Clear selection
        BersihkanSeleksiNotifikasi()

    End Sub

    ''' <summary>
    ''' Membersihkan seleksi pada DataGrid Notifikasi
    ''' </summary>
    Sub BersihkanSeleksiNotifikasi()
        datagridNotifikasi.SelectedIndex = -1
        BarisNotifikasi_Terseleksi = -1
        NomorIDNotifikasi_Terseleksi = 0
        KontenNotifikasi_Terseleksi = Kosongan
        HalamanTargetNotifikasi_Terseleksi = Kosongan
        PesanEksekusiNotifikasi_Terseleksi = Kosongan
        StatusDibacaNotifikasi_Terseleksi = 0
        StatusDieksekusiNotifikasi_Terseleksi = 0
    End Sub

    ''' <summary>
    ''' Menampilkan panel notifikasi
    ''' </summary>
    Public Sub TampilkanPanelNotifikasi()
        pnl_Notifikasi.Visibility = Visibility.Visible
        mnu_Notifikasi.Header = "Tutup"
        IsiKontenNotifikasi()
        VisibilitasNotifikasi = True
    End Sub

    ''' <summary>
    ''' Menutup panel notifikasi
    ''' </summary>
    Public Sub TutupPanelNotifikasi()
        pnl_Notifikasi.Visibility = Visibility.Collapsed
        mnu_Notifikasi.Header = "N_otifikasi"
        VisibilitasNotifikasi = False
    End Sub

    ''' <summary>
    ''' Event handler untuk menu Notifikasi
    ''' </summary>
    Private Sub mnu_Notifikasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Notifikasi.Click
        Select Case VisibilitasNotifikasi
            Case True
                TutupPanelNotifikasi()
            Case False
                TampilkanPanelNotifikasi()
        End Select
    End Sub

    ''' <summary>
    ''' Event handler untuk tombol tutup panel notifikasi
    ''' </summary>
    Private Sub btn_TutupNotifikasi_Click(sender As Object, e As RoutedEventArgs) Handles btn_TutupNotifikasi.Click
        TutupPanelNotifikasi()
    End Sub

    ''' <summary>
    ''' Event handler saat baris notifikasi diklik
    ''' </summary>
    Private Sub datagridNotifikasi_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridNotifikasi.SelectedCellsChanged

        BarisNotifikasi_Terseleksi = datagridNotifikasi.SelectedIndex
        If BarisNotifikasi_Terseleksi < 0 Then Return

        rowviewNotifikasi = TryCast(datagridNotifikasi.SelectedItem, DataRowView)
        If rowviewNotifikasi Is Nothing Then Return

        ' Ambil data dari baris terseleksi
        NomorIDNotifikasi_Terseleksi = CInt(rowviewNotifikasi("Nomor_ID"))
        KontenNotifikasi_Terseleksi = rowviewNotifikasi("Konten").ToString()
        HalamanTargetNotifikasi_Terseleksi = rowviewNotifikasi("Halaman_Target").ToString()
        PesanEksekusiNotifikasi_Terseleksi = rowviewNotifikasi("Pesan_Eksekusi").ToString()
        StatusDibacaNotifikasi_Terseleksi = CInt(rowviewNotifikasi("Status_Dibaca"))
        StatusDieksekusiNotifikasi_Terseleksi = CInt(rowviewNotifikasi("Status_Dieksekusi"))

        ' Navigasi ke halaman target
        EksekusiNotifikasi()

    End Sub

    ''' <summary>
    ''' Eksekusi notifikasi: navigasi ke halaman target dan update status
    ''' </summary>
    Private Sub EksekusiNotifikasi()

        KeluarDariSemuaModul()

        ' Navigasi berdasarkan halaman target
        Select Case HalamanTargetNotifikasi_Terseleksi
            Case Halaman_DATACOA
                ' Buka modul Data COA
                usc_DataCOA = New wpfUsc_DataCOA
                BukaUserControlDalamTab(usc_DataCOA, "Data COA")

            Case Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI
                BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()

            Case Halaman_BUKUPENGAWASANHUTANGUSAHAAFILIASI
                BukaModul_BukuPengawasanHutangUsaha_Afiliasi()

            Case Halaman_BUKUPENGAWASANPIUTANGUSAHANONAFILIASI
                BukaModul_BukuPengawasanPiutangUsaha_NonAfiliasi()

            Case Halaman_BUKUPENGAWASANPIUTANGUSAHAAFILIASI
                BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()

            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL21
                BukaModul_BukuPengawasanHutangPPhPasal21()

            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL23
                BukaModul_BukuPengawasanHutangPPhPasal23()

            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL42
                BukaModul_BukuPengawasanHutangPPhPasal42()

            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL25
                BukaModul_BukuPengawasanHutangPPhPasal25()

            Case Halaman_BUKUPENGAWASANHUTANGPPHPASAL26
                BukaModul_BukuPengawasanHutangPPhPasal26()

            Case Halaman_BUKUPENGAWASANHUTANGKARYAWAN
                BukaModul_BukuPengawasanHutangKaryawan()

            Case Halaman_BUKUPENGAWASANPIUTANGKARYAWAN
                BukaModul_BukuPengawasanPiutangKaryawan()

            Case Halaman_BUKUPENGAWASANHUTANGPEMEGANGSAHAM
                BukaModul_BukuPengawasanHutangPemegangSaham()

            Case Halaman_BUKUPENGAWASANPIUTANGPEMEGANGSAHAM
                BukaModul_BukuPengawasanPiutangPemegangSaham()

            Case Halaman_BUKUPENGAWASANHUTANGBANK
                BukaModul_BukuPengawasanHutangBank()

            Case Halaman_BUKUPENGAWASANHUTANGLEASING
                BukaModul_BukuPengawasanHutangLeasing()

            Case Halaman_BUKUPENGAWASANHUTANGPIHAKKETIGA
                BukaModul_BukuPengawasanHutangPihakKetiga()

            Case Halaman_BUKUPENGAWASANPIUTANGPIHAKKETIGA
                BukaModul_BukuPengawasanPiutangPihakKetiga()

            Case Halaman_BUKUPENGAWASANHUTANGAFILIASI
                BukaModul_BukuPengawasanHutangAfiliasi()

            Case Halaman_BUKUPENGAWASANPIUTANGAFILIASI
                BukaModul_BukuPengawasanPiutangAfiliasi()

            Case Halaman_BUKUPENGAWASANDEPOSITOPERASIONAL
                BukaModul_BukuPengawasanDepositOperasional()

            Case Kosongan
                ' Tidak ada eksekusi

            Case Else
                PesanUntukProgrammer("Halaman Target Belum Ditentukan: " & HalamanTargetNotifikasi_Terseleksi)
        End Select

        ' Tampilkan pesan eksekusi jika ada
        If Not String.IsNullOrEmpty(PesanEksekusiNotifikasi_Terseleksi) Then
            Pesan_Informasi(PesanEksekusiNotifikasi_Terseleksi)
        End If

        ' Update status dibaca
        UpdateNotifikasi_Dibaca()

        ' Refresh konten notifikasi
        IsiKontenNotifikasi()

    End Sub

    ''' <summary>
    ''' Update status notifikasi menjadi sudah dibaca
    ''' </summary>
    Private Sub UpdateNotifikasi_Dibaca()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Notifikasi SET " &
                              " Status_Dibaca = 1 " &
                              " WHERE Nomor_ID = " & NomorIDNotifikasi_Terseleksi, KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
    End Sub

#End Region

End Class
