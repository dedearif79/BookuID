Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Forms.Integration
Imports bcomm


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

    Private Sub mnu_Keluar_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Keluar.Click
        Me.Close()
    End Sub

    ' === DATA ===
    Private Sub mnu_DataUser_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataUser.Click
        host_DataUser = New wpfHost_DataUser
        BukaUserControlDalamTab(usc_DataUser, host_DataUser.JudulForm)
    End Sub

    Private Sub mnu_DataCOA_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataCOA.Click
        host_DataCOA = New wpfHost_DataCOA
        BukaUserControlDalamTab(usc_DataCOA, host_DataCOA.JudulForm)
    End Sub

    Private Sub mnu_DataLawanTransaksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataLawanTransaksi.Click
        host_DataLawanTransaksi = New wpfHost_DataLawanTransaksi
        BukaUserControlDalamTab(usc_DataLawanTransaksi, host_DataLawanTransaksi.JudulForm)
    End Sub

    Private Sub mnu_DataKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataKaryawan.Click
        host_DataKaryawan = New wpfHost_DataKaryawan
        BukaUserControlDalamTab(usc_DataKaryawan, host_DataKaryawan.JudulForm)
    End Sub

    Private Sub mnu_DataProject_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataProject.Click
        host_DataProject = New wpfHost_DataProject
        BukaUserControlDalamTab(usc_DataProject, host_DataProject.JudulForm)
    End Sub

    Private Sub mnu_Kurs_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Kurs.Click
        host_DataKurs = New wpfHost_DataKurs
        BukaUserControlDalamTab(usc_Kurs, host_DataKurs.JudulForm)
    End Sub

    ' === AKUNTANSI ===
    Private Sub mnu_JurnalUmum_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalUmum.Click
        host_JurnalUmum = New wpfHost_JurnalUmum
        BukaUserControlDalamTab(usc_JurnalUmum, host_JurnalUmum.JudulForm)
    End Sub

    Private Sub mnu_BukuBesar_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBesar.Click
        host_BukuBesar = New wpfHost_BukuBesar
        BukaUserControlDalamTab(usc_BukuBesar, host_BukuBesar.JudulForm)
    End Sub
    Sub BukaModul_BukuBesar(COA)
        ' Buka Buku Besar
        ' Catatan: Parameter COA untuk filter tidak diimplementasikan di WPF
        host_BukuBesar = New wpfHost_BukuBesar
        BukaUserControlDalamTab(usc_BukuBesar, host_BukuBesar.JudulForm)
    End Sub

    ' mnu_TrialBalance di frm_BOOKU memiliki Visible = False, sehingga tidak ditambahkan ke wpfWin_BOOKU
    ' Handler ini di-comment untuk sementara
    ' Private Sub mnu_TrialBalance_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TrialBalance.Click
    '     host_LaporanTrialBalance = New wpfHost_LaporanTrialBalance
    '     BukaUserControlDalamTab(usc_LaporanTrialBalance, host_LaporanTrialBalance.JudulForm)
    ' End Sub

    Private Sub mnu_LaporanHPP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LaporanHPP.Click
        BukaHalaman_LaporanHPP()
    End Sub
    Sub BukaHalaman_LaporanHPP()
        host_LaporanHPP = New wpfHost_LaporanHPP
        BukaUserControlDalamTab(usc_LaporanHPP, host_LaporanHPP.JudulForm)
    End Sub

    Private Sub mnu_LabaRugi_Bulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LabaRugi_Bulanan.Click
        BukaHalaman_LaporanLabaRugi_Bulanan()
    End Sub
    Sub BukaHalaman_LaporanLabaRugi_Bulanan()
        host_LaporanLabaRugi_Bulanan = New wpfHost_LaporanLabaRugi_Bulanan
        BukaUserControlDalamTab(usc_LaporanLabaRugi_Bulanan, host_LaporanLabaRugi_Bulanan.JudulForm)
    End Sub

    Private Sub mnu_LabaRugi_Tahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LabaRugi_Tahunan.Click
        host_LaporanLabaRugi_Tahunan = New wpfHost_LaporanLabaRugi_Tahunan
        BukaUserControlDalamTab(usc_LaporanLabaRugi_Tahunan, host_LaporanLabaRugi_Tahunan.JudulForm)
    End Sub

    Private Sub mnu_Neraca_Bulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Neraca_Bulanan.Click
        BukaHalaman_LaporanNeraca_Bulanan()
    End Sub
    Sub BukaHalaman_LaporanNeraca_Bulanan()
        host_LaporanNeraca_Bulanan = New wpfHost_LaporanNeraca_Bulanan
        BukaUserControlDalamTab(usc_LaporanNeraca_Bulanan, host_LaporanNeraca_Bulanan.JudulForm)
    End Sub

    Private Sub mnu_Neraca_Tahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Neraca_Tahunan.Click
        host_LaporanNeraca_Tahunan = New wpfHost_LaporanNeraca_Tahunan
        BukaUserControlDalamTab(usc_LaporanNeraca_Tahunan, host_LaporanNeraca_Tahunan.JudulForm)
    End Sub

    Private Sub mnu_NeracaLajur_Click(sender As Object, e As RoutedEventArgs) Handles mnu_NeracaLajur.Click
        host_LaporanNeracaLajur = New wpfHost_LaporanNeracaLajur
        BukaUserControlDalamTab(usc_LaporanNeracaLajur, host_LaporanNeracaLajur.JudulForm)
    End Sub

    ' === BUKU PENGAWASAN ===
    Private Sub mnu_BukuBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBank.Click
        BukaModul_BukuBank()
    End Sub
    Sub BukaModul_BukuBank()
        host_BukuBank = New wpfHost_BukuBank
        BukaUserControlDalamTab(usc_BukuBank, host_BukuBank.JudulForm)
    End Sub

    Private Sub mnu_BukuKas_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuKas.Click
        BukaModul_BukuKas()
    End Sub
    Sub BukaModul_BukuKas()
        host_BukuKas = New wpfHost_BukuKas
        BukaUserControlDalamTab(usc_BukuKas, host_BukuKas.JudulForm)
    End Sub

    Private Sub mnu_BukuPettyCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPettyCash.Click
        BukaModul_BukuPettyCash()
    End Sub
    Sub BukaModul_BukuPettyCash()
        host_BukuPettyCash = New wpfHost_BukuPettyCash
        BukaUserControlDalamTab(usc_BukuPettyCash, host_BukuPettyCash.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanGaji_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanGaji.Click
        BukaModul_BukuPengawasanGaji()
    End Sub
    Sub BukaModul_BukuPengawasanGaji()
        host_BukuPengawasanGaji = New wpfHost_BukuPengawasanGaji
        BukaUserControlDalamTab(usc_BukuPengawasanGaji, host_BukuPengawasanGaji.JudulForm)
    End Sub

    ' === ASSET ===
    Private Sub mnu_DaftarPenyusutanAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DaftarPenyusutanAssetTetap.Click
        BukaModul_DaftarPenyusutanAssetTetap()
    End Sub
    Sub BukaModul_DaftarPenyusutanAssetTetap()
        host_DaftarPenyusutanAssetTetap = New wpfHost_DaftarPenyusutanAssetTetap
        BukaUserControlDalamTab(usc_DaftarPenyusutanAssetTetap, host_DaftarPenyusutanAssetTetap.JudulForm)
    End Sub

    Private Sub mnu_ManajemenAmortisasiBiaya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenAmortisasiBiaya.Click
        BukaModul_DaftarAmortisasiBiaya()
    End Sub
    Sub BukaModul_DaftarAmortisasiBiaya()
        host_DaftarAmortisasiBiaya = New wpfHost_DaftarAmortisasiBiaya
        BukaUserControlDalamTab(usc_DaftarAmortisasiBiaya, host_DaftarAmortisasiBiaya.JudulForm)
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
        host_DaftarPemegangSaham = New wpfHost_DaftarPemegangSaham
        BukaUserControlDalamTab(usc_DaftarPemegangSaham, host_DaftarPemegangSaham.JudulForm)
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
        host_TutupBuku = New wpfHost_TutupBuku
        BukaUserControlDalamTab(usc_TutupBuku, host_TutupBuku.JudulForm)
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
        host_POPembelian_Lokal_Barang = New wpfHost_POPembelian_Lokal_Barang
        BukaUserControlDalamTab(usc_POPembelian_Lokal_Barang, host_POPembelian_Lokal_Barang.JudulForm)
    End Sub

    Private Sub mnu_POPembelian_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_Jasa.Click
        host_POPembelian_Lokal_Jasa = New wpfHost_POPembelian_Lokal_Jasa
        BukaUserControlDalamTab(usc_POPembelian_Lokal_Jasa, host_POPembelian_Lokal_Jasa.JudulForm)
    End Sub

    Private Sub mnu_POPembelian_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_BarangDanJasa.Click
        host_POPembelian_Lokal_BarangDanJasa = New wpfHost_POPembelian_Lokal_BarangDanJasa
        BukaUserControlDalamTab(usc_POPembelian_Lokal_BarangDanJasa, host_POPembelian_Lokal_BarangDanJasa.JudulForm)
    End Sub

    Private Sub mnu_POPembelian_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_JasaKonstruksi.Click
        host_POPembelian_Lokal_JasaKonstruksi = New wpfHost_POPembelian_Lokal_JasaKonstruksi
        BukaUserControlDalamTab(usc_POPembelian_Lokal_JasaKonstruksi, host_POPembelian_Lokal_JasaKonstruksi.JudulForm)
    End Sub

    Private Sub mnu_POPembelian_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Semua.Click
        host_POPembelian_Lokal_Semua = New wpfHost_POPembelian_Lokal_Semua
        BukaUserControlDalamTab(usc_POPembelian_Lokal_Semua, host_POPembelian_Lokal_Semua.JudulForm)
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Barang.Click
        host_POPembelian_Impor_Barang = New wpfHost_POPembelian_Impor_Barang
        BukaUserControlDalamTab(usc_POPembelian_Impor_Barang, host_POPembelian_Impor_Barang.JudulForm)
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Jasa.Click
        host_POPembelian_Impor_Jasa = New wpfHost_POPembelian_Impor_Jasa
        BukaUserControlDalamTab(usc_POPembelian_Impor_Jasa, host_POPembelian_Impor_Jasa.JudulForm)
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Semua.Click
        host_POPembelian_Impor_Semua = New wpfHost_POPembelian_Impor_Semua
        BukaUserControlDalamTab(usc_POPembelian_Impor_Semua, host_POPembelian_Impor_Semua.JudulForm)
    End Sub

    ' ============================================================
    ' PEMBELIAN - SURAT JALAN & BAST
    ' ============================================================
    Private Sub mnu_SuratJalanPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_SuratJalanPembelian.Click
        host_SuratJalanPembelian = New wpfHost_SuratJalanPembelian
        BukaUserControlDalamTab(usc_SuratJalanPembelian, host_SuratJalanPembelian.JudulForm)
    End Sub

    Private Sub mnu_BASTPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BASTPembelian.Click
        host_BASTPembelian = New wpfHost_BASTPembelian
        BukaUserControlDalamTab(usc_BASTPembelian, host_BASTPembelian.JudulForm)
    End Sub

    ' ============================================================
    ' PEMBELIAN - INVOICE DENGAN PO
    ' ============================================================
    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal_Rutin.Click
        host_InvoicePembelian_DenganPO_Lokal_Rutin = New wpfHost_InvoicePembelian_DenganPO_Lokal_Rutin
        BukaUserControlDalamTab(usc_InvoicePembelian_DenganPO_Lokal_Rutin, host_InvoicePembelian_DenganPO_Lokal_Rutin.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal_Termin.Click
        host_InvoicePembelian_DenganPO_Lokal_Termin = New wpfHost_InvoicePembelian_DenganPO_Lokal_Termin
        BukaUserControlDalamTab(usc_InvoicePembelian_DenganPO_Lokal_Termin, host_InvoicePembelian_DenganPO_Lokal_Termin.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor_Rutin.Click
        host_InvoicePembelian_DenganPO_Impor_Rutin = New wpfHost_InvoicePembelian_DenganPO_Impor_Rutin
        BukaUserControlDalamTab(usc_InvoicePembelian_DenganPO_Impor_Rutin, host_InvoicePembelian_DenganPO_Impor_Rutin.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor_Termin.Click
        host_InvoicePembelian_DenganPO_Impor_Termin = New wpfHost_InvoicePembelian_DenganPO_Impor_Termin
        BukaUserControlDalamTab(usc_InvoicePembelian_DenganPO_Impor_Termin, host_InvoicePembelian_DenganPO_Impor_Termin.JudulForm)
    End Sub

    ' ============================================================
    ' PEMBELIAN - INVOICE TANPA PO
    ' ============================================================
    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_Barang.Click
        host_InvoicePembelian_TanpaPO_Lokal_Barang = New wpfHost_InvoicePembelian_TanpaPO_Lokal_Barang
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Lokal_Barang, host_InvoicePembelian_TanpaPO_Lokal_Barang.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_Jasa.Click
        host_InvoicePembelian_TanpaPO_Lokal_Jasa = New wpfHost_InvoicePembelian_TanpaPO_Lokal_Jasa
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Lokal_Jasa, host_InvoicePembelian_TanpaPO_Lokal_Jasa.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Click
        host_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa = New wpfHost_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa, host_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Click
        host_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi = New wpfHost_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi, host_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor_Barang.Click
        host_InvoicePembelian_TanpaPO_Impor_Barang = New wpfHost_InvoicePembelian_TanpaPO_Impor_Barang
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Impor_Barang, host_InvoicePembelian_TanpaPO_Impor_Barang.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor_Jasa.Click
        host_InvoicePembelian_TanpaPO_Impor_Jasa = New wpfHost_InvoicePembelian_TanpaPO_Impor_Jasa
        BukaUserControlDalamTab(usc_InvoicePembelian_TanpaPO_Impor_Jasa, host_InvoicePembelian_TanpaPO_Impor_Jasa.JudulForm)
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_LokalMUA_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_LokalMUA.Click
        ' TODO: Implementasi MUA (Make-Up Artist) jika diperlukan
    End Sub

    ' ============================================================
    ' PEMBELIAN - BUKU & RETUR
    ' ============================================================
    Private Sub mnu_BukuPembelian_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPembelian_Lokal.Click
        host_BukuPembelian_Lokal = New wpfHost_BukuPembelian_Lokal
        BukaUserControlDalamTab(usc_BukuPembelian_Lokal, host_BukuPembelian_Lokal.JudulForm)
    End Sub

    Private Sub mnu_BukuPembelian_Impor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPembelian_Impor.Click
        host_BukuPembelian_Impor = New wpfHost_BukuPembelian_Impor
        BukaUserControlDalamTab(usc_BukuPembelian_Impor, host_BukuPembelian_Impor.JudulForm)
    End Sub

    Private Sub mnu_ReturPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ReturPembelian.Click
        host_ReturPembelian = New wpfHost_ReturPembelian
        BukaUserControlDalamTab(usc_ReturPembelian, host_ReturPembelian.JudulForm)
    End Sub

    ' ============================================================
    ' PENJUALAN - PO
    ' ============================================================
    Private Sub mnu_POPenjualan_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Barang.Click
        host_POPenjualan_Lokal_Barang = New wpfHost_POPenjualan_Lokal_Barang
        BukaUserControlDalamTab(usc_POPenjualan_Barang, host_POPenjualan_Lokal_Barang.JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Jasa.Click
        host_POPenjualan_Lokal_Jasa = New wpfHost_POPenjualan_Lokal_Jasa
        BukaUserControlDalamTab(usc_POPenjualan_Jasa, host_POPenjualan_Lokal_Jasa.JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_BarangDanJasa.Click
        host_POPenjualan_Lokal_BarangDanJasa = New wpfHost_POPenjualan_Lokal_BarangDanJasa
        BukaUserControlDalamTab(usc_POPenjualan_BarangDanJasa, host_POPenjualan_Lokal_BarangDanJasa.JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_JasaKonstruksi.Click
        host_POPenjualan_Lokal_JasaKonstruksi = New wpfHost_POPenjualan_Lokal_JasaKonstruksi
        BukaUserControlDalamTab(usc_POPenjualan_JasaKonstruksi, host_POPenjualan_Lokal_JasaKonstruksi.JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Semua.Click
        host_POPenjualan_Lokal_Semua = New wpfHost_POPenjualan_Lokal_Semua
        BukaUserControlDalamTab(usc_POPenjualan_Semua, host_POPenjualan_Lokal_Semua.JudulForm)
    End Sub

    Private Sub mnu_POPenjualan_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Ekspor.Click
        host_POPenjualan_Ekspor = New wpfHost_POPenjualan_Ekspor
        BukaUserControlDalamTab(usc_POPenjualan_Ekspor, host_POPenjualan_Ekspor.JudulForm)
    End Sub

    ' ============================================================
    ' PENJUALAN - SURAT JALAN & BAST
    ' ============================================================
    Private Sub mnu_SuratJalanPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_SuratJalanPenjualan.Click
        host_SuratJalanPenjualan = New wpfHost_SuratJalanPenjualan
        BukaUserControlDalamTab(usc_SuratJalanPenjualan, host_SuratJalanPenjualan.JudulForm)
    End Sub

    Private Sub mnu_BASTPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BASTPenjualan.Click
        host_BASTPenjualan = New wpfHost_BASTPenjualan
        BukaUserControlDalamTab(usc_BASTPenjualan, host_BASTPenjualan.JudulForm)
    End Sub

    ' ============================================================
    ' PENJUALAN - INVOICE DENGAN PO
    ' ============================================================
    Private Sub mnu_InvoicePenjualan_DenganPO_Lokal_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Lokal_Rutin.Click
        host_InvoicePenjualan_DenganPO_Lokal_Rutin = New wpfHost_InvoicePenjualan_DenganPO_Lokal_Rutin
        BukaUserControlDalamTab(usc_InvoicePenjualan_DenganPO_Lokal_Rutin, host_InvoicePenjualan_DenganPO_Lokal_Rutin.JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Lokal_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Lokal_Termin.Click
        host_InvoicePenjualan_DenganPO_Lokal_Termin = New wpfHost_InvoicePenjualan_DenganPO_Lokal_Termin
        BukaUserControlDalamTab(usc_InvoicePenjualan_DenganPO_Lokal_Termin, host_InvoicePenjualan_DenganPO_Lokal_Termin.JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin.Click
        host_InvoicePenjualan_DenganPO_Ekspor_Rutin = New wpfHost_InvoicePenjualan_DenganPO_Ekspor_Rutin
        BukaUserControlDalamTab(usc_InvoicePenjualan_DenganPO_Ekspor_Rutin, host_InvoicePenjualan_DenganPO_Ekspor_Rutin.JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Ekspor_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Ekspor_Termin.Click
        host_InvoicePenjualan_DenganPO_Ekspor_Termin = New wpfHost_InvoicePenjualan_DenganPO_Ekspor_Termin
        BukaUserControlDalamTab(usc_InvoicePenjualan_DenganPO_Ekspor_Termin, host_InvoicePenjualan_DenganPO_Ekspor_Termin.JudulForm)
    End Sub

    ' ============================================================
    ' PENJUALAN - INVOICE TANPA PO
    ' ============================================================
    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_Barang.Click
        host_InvoicePenjualan_TanpaPO_Lokal_Barang = New wpfHost_InvoicePenjualan_TanpaPO_Lokal_Barang
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Lokal_Barang, host_InvoicePenjualan_TanpaPO_Lokal_Barang.JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa.Click
        host_InvoicePenjualan_TanpaPO_Lokal_Jasa = New wpfHost_InvoicePenjualan_TanpaPO_Lokal_Jasa
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Lokal_Jasa, host_InvoicePenjualan_TanpaPO_Lokal_Jasa.JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.Click
        host_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa = New wpfHost_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa, host_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Click
        host_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi = New wpfHost_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi, host_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Ekspor.Click
        host_InvoicePenjualan_TanpaPO_Ekspor = New wpfHost_InvoicePenjualan_TanpaPO_Ekspor
        BukaUserControlDalamTab(usc_InvoicePenjualan_TanpaPO_Ekspor, host_InvoicePenjualan_TanpaPO_Ekspor.JudulForm)
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Asset_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Asset.Click
        ' Redirect ke Daftar Penyusutan Asset Tetap (sama seperti di frm_BOOKU)
        BukaModul_DaftarPenyusutanAssetTetap()
    End Sub

    ' ============================================================
    ' PENJUALAN - BUKU & RETUR
    ' ============================================================
    Private Sub mnu_BukuPenjualan_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualan_Lokal.Click
        host_BukuPenjualan_Lokal = New wpfHost_BukuPenjualan_Lokal
        BukaUserControlDalamTab(usc_BukuPenjualan_Lokal, host_BukuPenjualan_Lokal.JudulForm)
    End Sub

    Private Sub mnu_BukuPenjualan_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualan_Ekspor.Click
        host_BukuPenjualan_Ekspor = New wpfHost_BukuPenjualan_Ekspor
        BukaUserControlDalamTab(usc_BukuPenjualan_Ekspor, host_BukuPenjualan_Ekspor.JudulForm)
    End Sub

    Private Sub mnu_BukuPenjualanEceran_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualanEceran.Click
        BukaModul_BukuPenjualanEceran()
    End Sub
    Sub BukaModul_BukuPenjualanEceran()
        host_BukuPenjualanEceran = New wpfHost_BukuPenjualanEceran
        BukaUserControlDalamTab(usc_BukuPenjualanEceran, host_BukuPenjualanEceran.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanReturPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanReturPenjualan.Click
        host_ReturPenjualan = New wpfHost_ReturPenjualan
        BukaUserControlDalamTab(usc_ReturPenjualan, host_ReturPenjualan.JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - BANK & CASH
    ' ============================================================
    Private Sub mnu_BukuCashAdvance_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuCashAdvance.Click
        BukaModul_BukuCashAdvance()
    End Sub
    Sub BukaModul_BukuCashAdvance()
        host_BukuCashAdvance = New wpfHost_BukuCashAdvance
        BukaUserControlDalamTab(usc_BukuCashAdvance, host_BukuCashAdvance.JudulForm)
    End Sub

    Private Sub mnu_BukuBankGaransi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBankGaransi.Click
        BukaModul_BukuBankGaransi()
    End Sub
    Sub BukaModul_BukuBankGaransi()
        host_BukuBankGaransi = New wpfHost_BukuBankGaransi
        BukaUserControlDalamTab(usc_BukuBankGaransi, host_BukuBankGaransi.JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - GAJI
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangBPJSKesehatan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBPJSKesehatan.Click
        BukaModul_BukuPengawasanHutangBPJSKesehatan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBPJSKesehatan()
        host_BukuPengawasanHutangBPJSKesehatan = New wpfHost_BukuPengawasanHutangBPJSKesehatan
        BukaUserControlDalamTab(usc_BukuPengawasanHutangBPJSKesehatan, host_BukuPengawasanHutangBPJSKesehatan.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangBPJSKetenagakerjaan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBPJSKetenagakerjaan.Click
        BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBPJSKetenagakerjaan()
        host_BukuPengawasanHutangBPJSKetenagakerjaan = New wpfHost_BukuPengawasanHutangBPJSKetenagakerjaan
        BukaUserControlDalamTab(usc_BukuPengawasanHutangBPJSKetenagakerjaan, host_BukuPengawasanHutangBPJSKetenagakerjaan.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangKoperasiKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangKoperasiKaryawan.Click
        BukaModul_BukuPengawasanHutangKoperasiKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangKoperasiKaryawan()
        host_BukuPengawasanHutangKoperasiKaryawan = New wpfHost_BukuPengawasanHutangKoperasiKaryawan
        BukaUserControlDalamTab(usc_BukuPengawasanHutangKoperasiKaryawan, host_BukuPengawasanHutangKoperasiKaryawan.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangSerikat_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangSerikat.Click
        BukaModul_BukuPengawasanHutangSerikat()
    End Sub
    Sub BukaModul_BukuPengawasanHutangSerikat()
        host_BukuPengawasanHutangSerikat = New wpfHost_BukuPengawasanHutangSerikat
        BukaUserControlDalamTab(usc_BukuPengawasanHutangSerikat, host_BukuPengawasanHutangSerikat.JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - HUTANG USAHA
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_NonAfiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_NonAfiliasi()
        host_BukuPengawasanHutangUsaha_NonAfiliasi = New wpfHost_BukuPengawasanHutangUsaha_NonAfiliasi
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_NonAfiliasi, host_BukuPengawasanHutangUsaha_NonAfiliasi.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Afiliasi()
        host_BukuPengawasanHutangUsaha_Afiliasi = New wpfHost_BukuPengawasanHutangUsaha_Afiliasi
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Afiliasi, host_BukuPengawasanHutangUsaha_Afiliasi.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Semua.Click
        host_BukuPengawasanHutangUsaha = New wpfHost_BukuPengawasanHutangUsaha
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha, host_BukuPengawasanHutangUsaha.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_USD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_USD()
        host_BukuPengawasanHutangUsaha_Impor_USD = New wpfHost_BukuPengawasanHutangUsaha_Impor_USD
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_USD, host_BukuPengawasanHutangUsaha_Impor_USD.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_AUD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_AUD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_AUD()
        host_BukuPengawasanHutangUsaha_Impor_AUD = New wpfHost_BukuPengawasanHutangUsaha_Impor_AUD
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_AUD, host_BukuPengawasanHutangUsaha_Impor_AUD.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_JPY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_JPY()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_JPY()
        host_BukuPengawasanHutangUsaha_Impor_JPY = New wpfHost_BukuPengawasanHutangUsaha_Impor_JPY
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_JPY, host_BukuPengawasanHutangUsaha_Impor_JPY.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_CNY.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_CNY()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_CNY()
        host_BukuPengawasanHutangUsaha_Impor_CNY = New wpfHost_BukuPengawasanHutangUsaha_Impor_CNY
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_CNY, host_BukuPengawasanHutangUsaha_Impor_CNY.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_EUR.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_EUR()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_EUR()
        host_BukuPengawasanHutangUsaha_Impor_EUR = New wpfHost_BukuPengawasanHutangUsaha_Impor_EUR
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_EUR, host_BukuPengawasanHutangUsaha_Impor_EUR.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_SGD.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_SGD()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_SGD()
        host_BukuPengawasanHutangUsaha_Impor_SGD = New wpfHost_BukuPengawasanHutangUsaha_Impor_SGD
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_SGD, host_BukuPengawasanHutangUsaha_Impor_SGD.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_GBP.Click
        BukaModul_BukuPengawasanHutangUsaha_Impor_GBP()
    End Sub
    Sub BukaModul_BukuPengawasanHutangUsaha_Impor_GBP()
        host_BukuPengawasanHutangUsaha_Impor_GBP = New wpfHost_BukuPengawasanHutangUsaha_Impor_GBP
        BukaUserControlDalamTab(usc_BukuPengawasanHutangUsaha_Impor_GBP, host_BukuPengawasanHutangUsaha_Impor_GBP.JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - HUTANG LAINNYA
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBank.Click
        BukaModul_BukuPengawasanHutangBank()
    End Sub
    Sub BukaModul_BukuPengawasanHutangBank()
        host_BukuPengawasanHutangBank = New wpfHost_BukuPengawasanHutangBank
        BukaUserControlDalamTab(usc_BukuPengawasanHutangBank, host_BukuPengawasanHutangBank.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangLeasing_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangLeasing.Click
        BukaModul_BukuPengawasanHutangLeasing()
    End Sub
    Sub BukaModul_BukuPengawasanHutangLeasing()
        host_BukuPengawasanHutangLeasing = New wpfHost_BukuPengawasanHutangLeasing
        BukaUserControlDalamTab(usc_BukuPengawasanHutangLeasing, host_BukuPengawasanHutangLeasing.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPihakKetiga.Click
        BukaModul_BukuPengawasanHutangPihakKetiga()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPihakKetiga()
        host_BukuPengawasanHutangPihakKetiga = New wpfHost_BukuPengawasanHutangPihakKetiga
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPihakKetiga, host_BukuPengawasanHutangPihakKetiga.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangAfiliasi.Click
        BukaModul_BukuPengawasanHutangAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanHutangAfiliasi()
        host_BukuPengawasanHutangAfiliasi = New wpfHost_BukuPengawasanHutangAfiliasi
        BukaUserControlDalamTab(usc_BukuPengawasanHutangAfiliasi, host_BukuPengawasanHutangAfiliasi.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangKaryawan.Click
        BukaModul_BukuPengawasanHutangKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanHutangKaryawan()
        host_BukuPengawasanHutangKaryawan = New wpfHost_BukuPengawasanHutangKaryawan
        BukaUserControlDalamTab(usc_BukuPengawasanHutangKaryawan, host_BukuPengawasanHutangKaryawan.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPemegangSaham.Click
        BukaModul_BukuPengawasanHutangPemegangSaham()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPemegangSaham()
        host_BukuPengawasanHutangPemegangSaham = New wpfHost_BukuPengawasanHutangPemegangSaham
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPemegangSaham, host_BukuPengawasanHutangPemegangSaham.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangDividen_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangDividen.Click
        BukaModul_BukuPengawasanHutangDividen()
    End Sub
    Sub BukaModul_BukuPengawasanHutangDividen()
        host_BukuPengawasanHutangDividen = New wpfHost_BukuPengawasanHutangDividen
        BukaUserControlDalamTab(usc_BukuPengawasanHutangDividen, host_BukuPengawasanHutangDividen.JudulForm)
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
        host_BukuPengawasanPiutangUsaha_NonAfiliasi = New wpfHost_BukuPengawasanPiutangUsaha_NonAfiliasi
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_NonAfiliasi, host_BukuPengawasanPiutangUsaha_NonAfiliasi.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Afiliasi.Click
        BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Afiliasi()
        host_BukuPengawasanPiutangUsaha_Afiliasi = New wpfHost_BukuPengawasanPiutangUsaha_Afiliasi
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Afiliasi, host_BukuPengawasanPiutangUsaha_Afiliasi.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Semua.Click
        host_BukuPengawasanPiutangUsaha = New wpfHost_BukuPengawasanPiutangUsaha
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha, host_BukuPengawasanPiutangUsaha.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_USD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_USD()
        host_BukuPengawasanPiutangUsaha_Ekspor_USD = New wpfHost_BukuPengawasanPiutangUsaha_Ekspor_USD
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_USD, host_BukuPengawasanPiutangUsaha_Ekspor_USD.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_AUD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_AUD()
        host_BukuPengawasanPiutangUsaha_Ekspor_AUD = New wpfHost_BukuPengawasanPiutangUsaha_Ekspor_AUD
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_AUD, host_BukuPengawasanPiutangUsaha_Ekspor_AUD.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_JPY()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_JPY()
        host_BukuPengawasanPiutangUsaha_Ekspor_JPY = New wpfHost_BukuPengawasanPiutangUsaha_Ekspor_JPY
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_JPY, host_BukuPengawasanPiutangUsaha_Ekspor_JPY.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_CNY()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_CNY()
        host_BukuPengawasanPiutangUsaha_Ekspor_CNY = New wpfHost_BukuPengawasanPiutangUsaha_Ekspor_CNY
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_CNY, host_BukuPengawasanPiutangUsaha_Ekspor_CNY.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_EUR()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_EUR()
        host_BukuPengawasanPiutangUsaha_Ekspor_EUR = New wpfHost_BukuPengawasanPiutangUsaha_Ekspor_EUR
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_EUR, host_BukuPengawasanPiutangUsaha_Ekspor_EUR.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_SGD()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_SGD()
        host_BukuPengawasanPiutangUsaha_Ekspor_SGD = New wpfHost_BukuPengawasanPiutangUsaha_Ekspor_SGD
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_SGD, host_BukuPengawasanPiutangUsaha_Ekspor_SGD.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP.Click
        BukaModul_BukuPengawasanPiutangUsaha_Ekspor_GBP()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangUsaha_Ekspor_GBP()
        host_BukuPengawasanPiutangUsaha_Ekspor_GBP = New wpfHost_BukuPengawasanPiutangUsaha_Ekspor_GBP
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangUsaha_Ekspor_GBP, host_BukuPengawasanPiutangUsaha_Ekspor_GBP.JudulForm)
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - PIUTANG LAINNYA
    ' ============================================================
    Private Sub mnu_BukuPengawasanPiutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangPihakKetiga.Click
        BukaModul_BukuPengawasanPiutangPihakKetiga()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangPihakKetiga()
        host_BukuPengawasanPiutangPihakKetiga = New wpfHost_BukuPengawasanPiutangPihakKetiga
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangPihakKetiga, host_BukuPengawasanPiutangPihakKetiga.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangAfiliasi.Click
        BukaModul_BukuPengawasanPiutangAfiliasi()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangAfiliasi()
        host_BukuPengawasanPiutangAfiliasi = New wpfHost_BukuPengawasanPiutangAfiliasi
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangAfiliasi, host_BukuPengawasanPiutangAfiliasi.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangKaryawan.Click
        BukaModul_BukuPengawasanPiutangKaryawan()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangKaryawan()
        host_BukuPengawasanPiutangKaryawan = New wpfHost_BukuPengawasanPiutangKaryawan
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangKaryawan, host_BukuPengawasanPiutangKaryawan.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangPemegangSaham.Click
        BukaModul_BukuPengawasanPiutangPemegangSaham()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangPemegangSaham()
        host_BukuPengawasanPiutangPemegangSaham = New wpfHost_BukuPengawasanPiutangPemegangSaham
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangPemegangSaham, host_BukuPengawasanPiutangPemegangSaham.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanDepositOperasional_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanDepositOperasional.Click
        BukaModul_BukuPengawasanDepositOperasional()
    End Sub
    Sub BukaModul_BukuPengawasanDepositOperasional(Optional DariDataAwal As Boolean = False)
        host_BukuPengawasanDepositOperasional = New wpfHost_BukuPengawasanDepositOperasional(DariDataAwal)
        BukaUserControlDalamTab(usc_BukuPengawasanDepositOperasional, host_BukuPengawasanDepositOperasional.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangDividen_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangDividen.Click
        BukaModul_BukuPengawasanPiutangDividen()
    End Sub
    Sub BukaModul_BukuPengawasanPiutangDividen()
        host_BukuPengawasanPiutangDividen = New wpfHost_BukuPengawasanPiutangDividen
        BukaUserControlDalamTab(usc_BukuPengawasanPiutangDividen, host_BukuPengawasanPiutangDividen.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPiutangLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangLainnya.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - BUKTI & PEMINDABUKUAN
    ' ============================================================
    Private Sub mnu_BukuPengawasanBuktiPenerimaanBankCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPenerimaanBankCash.Click
        BukaModul_BukuPengawasanBuktiPenerimaanBankCash()
    End Sub
    Sub BukaModul_BukuPengawasanBuktiPenerimaanBankCash()
        host_BukuPengawasanBuktiPenerimaanBankCash = New wpfHost_BukuPengawasanBuktiPenerimaanBankCash
        BukaUserControlDalamTab(usc_BukuPengawasanBuktiPenerimaanBankCash, host_BukuPengawasanBuktiPenerimaanBankCash.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPengeluaranBankCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPengeluaranBankCash.Click
        BukaModul_BukuPengawasanBuktiPengeluaranBankCash()
    End Sub
    Sub BukaModul_BukuPengawasanBuktiPengeluaranBankCash()
        host_BukuPengawasanBuktiPengeluaranBankCash = New wpfHost_BukuPengawasanBuktiPengeluaranBankCash
        BukaUserControlDalamTab(usc_BukuPengawasanBuktiPengeluaranBankCash, host_BukuPengawasanBuktiPengeluaranBankCash.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanPemindabukuan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPemindabukuan.Click
        BukaModul_BukuPengawasanPemindahbukuan()
    End Sub
    Sub BukaModul_BukuPengawasanPemindahbukuan()
        host_BukuPengawasanPemindahbukuan = New wpfHost_BukuPengawasanPemindahbukuan
        BukaUserControlDalamTab(usc_BukuPengawasanPemindahbukuan, host_BukuPengawasanPemindahbukuan.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanAktivaLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanAktivaLainnya.Click
        BukaModul_BukuPengawasanAktivaLainnya()
    End Sub
    Sub BukaModul_BukuPengawasanAktivaLainnya()
        host_BukuPengawasanAktivaLainnya = New wpfHost_BukuPengawasanAktivaLainnya
        BukaUserControlDalamTab(usc_BukuPengawasanAktivaLainnya, host_BukuPengawasanAktivaLainnya.JudulForm)
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
    End Sub
    Sub BukaModul_StockOpname_BahanPenolong()
        host_StockOpname_BahanPenolong = New wpfHost_StockOpname_BahanPenolong
        BukaUserControlDalamTab(usc_BahanPenolong, host_StockOpname_BahanPenolong.JudulForm)
    End Sub

    Private Sub mnu_StockOpname_BahanBaku_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BahanBaku.Click
        BukaModul_StockOpname_BahanBaku()
    End Sub
    Sub BukaModul_StockOpname_BahanBaku()
        host_StockOpname_BahanBaku = New wpfHost_StockOpname_BahanBaku
        BukaUserControlDalamTab(usc_BahanBaku, host_StockOpname_BahanBaku.JudulForm)
    End Sub

    Private Sub mnu_StockOpname_BarangDalamProses_CekFisik_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangDalamProses_CekFisik.Click
        BukaModul_StockOpname_BarangDalamProses_CekFisik()
    End Sub
    Sub BukaModul_StockOpname_BarangDalamProses_CekFisik()
        host_StockOpname_BarangDalamProses_CekFisik = New wpfHost_StockOpname_BarangDalamProses_CekFisik
        BukaUserControlDalamTab(usc_BarangDalamProses_CekFisik, host_StockOpname_BarangDalamProses_CekFisik.JudulForm)
    End Sub

    Private Sub mnu_StockOpname_BarangDalamProses_TarikanData_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangDalamProses_TarikanData.Click
        host_StockOpname_BarangDalamProses_TarikanData = New wpfHost_StockOpname_BarangDalamProses_TarikanData
        BukaUserControlDalamTab(usc_BarangDalamProses_TarikanData, host_StockOpname_BarangDalamProses_TarikanData.JudulForm)
    End Sub

    Private Sub mnu_StockOpname_BarangJadi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangJadi.Click
        host_StockOpname_BarangJadi = New wpfHost_StockOpname_BarangJadi
        BukaUserControlDalamTab(usc_BarangJadi, host_StockOpname_BarangJadi.JudulForm)
    End Sub

    ' ============================================================
    ' AKUNTANSI - JURNAL ADJUSMENT
    ' ============================================================
    Private Sub mnu_JurnalAdjusment_Penyusutan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Penyusutan.Click
        BukaHalamanAdjusmentPenyusutanAsset()
    End Sub
    Sub BukaHalamanAdjusmentPenyusutanAsset()
        host_AdjusmentPenyusutanAsset = New wpfHost_AdjusmentPenyusutanAsset
        BukaUserControlDalamTab(usc_Adjusment_PenyusutanAsset, host_AdjusmentPenyusutanAsset.JudulForm)
    End Sub

    Private Sub mnu_JurnalAdjusment_Amortisasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Amortisasi.Click
        BukaHalamanAdjusmentAmortisasi()
    End Sub
    Sub BukaHalamanAdjusmentAmortisasi()
        host_AdjusmentAmortisasi = New wpfHost_AdjusmentAmortisasi
        BukaUserControlDalamTab(usc_Adjusment_Amortisasi, host_AdjusmentAmortisasi.JudulForm)
    End Sub

    Private Sub mnu_JurnalAdjusment_Forex_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Forex.Click
        BukaModul_AdjusmentForex()
    End Sub
    Sub BukaModul_AdjusmentForex()
        host_AdjusmentForex = New wpfHost_AdjusmentForex
        BukaUserControlDalamTab(usc_Adjusment_Forex, host_AdjusmentForex.JudulForm)
    End Sub

    Private Sub mnu_JurnalAdjusment_HPP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_HPP.Click
        'Cek Dulu Kelengkapan Adjusment Penyusutan Asset :
        host_AdjusmentPenyusutanAsset.CekAdjusment()
        If Not usc_Adjusment_PenyusutanAsset.AdjusmentBulanBukuAktifSudahLengkap Then
            PesanPemberitahuan("Silakan lengkapi dulu Adjusment Penyusutan Asset untuk Bulan " & KonversiAngkaKeBulanString(BulanBukuAktif) & ", baru masuk ke menu ini.")
            Return
        End If
        'Cek Dulu Kelengkapan Adjusment Amortisasi :
        host_AdjusmentAmortisasi.CekAdjusment()
        If Not usc_Adjusment_Amortisasi.AdjusmentBulanBukuAktifSudahLengkap Then
            PesanPemberitahuan("Silakan lengkapi dulu Adjusment Amortisasi untuk Bulan " & KonversiAngkaKeBulanString(BulanBukuAktif) & ", baru masuk ke menu ini.")
            Return
        End If
        'Cek Dulu Kelengkapan Adjusment Forex :
        host_AdjusmentForex.CekAdjusment()
        If Not usc_Adjusment_Forex.AdjusmentBulanBukuAktifSudahLengkap Then
            PesanPemberitahuan("Silakan tuntaskan dulu Adjusment Forex untuk Bulan " & KonversiAngkaKeBulanString(BulanBukuAktif) & ", baru masuk ke menu ini.")
            Return
        End If
        BukaModul_AdjusmentHPP()
    End Sub
    Sub BukaModul_AdjusmentHPP()
        host_AdjusmentHPP = New wpfHost_AdjusmentHPP
        BukaUserControlDalamTab(usc_JurnalAdjusment_HPP, host_AdjusmentHPP.JudulForm)
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
        BukaModul_BukuPenjualanAsset()
    End Sub
    Sub BukaModul_BukuPenjualanAsset()
        host_BukuPenjualan_Asset = New wpfHost_BukuPenjualan_Asset
        BukaUserControlDalamTab(usc_BukuPenjualan_Asset, host_BukuPenjualan_Asset.JudulForm)
    End Sub

    Private Sub mnu_BukuDisposalAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuDisposalAssetTetap.Click
        host_BukuDisposalAssetTetap = New wpfHost_BukuDisposalAssetTetap
        BukaUserControlDalamTab(usc_BukuDisposalAssetTetap, host_BukuDisposalAssetTetap.JudulForm)
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
        host_BukuPengawasanHutangPPhPasal21 = New wpfHost_BukuPengawasanHutangPPhPasal21
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal21, host_BukuPengawasanHutangPPhPasal21.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal22_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22_Lokal.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal22_Impor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22_Impor.Click
        BukaModul_BukuPengawasanHutangPPhPasal22_Impor()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal22_Impor()
        host_BukuPengawasanHutangPPhPasal22_Impor = New wpfHost_BukuPengawasanHutangPPhPasal22_Impor
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal22_Impor, host_BukuPengawasanHutangPPhPasal22_Impor.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal23_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal23.Click
        BukaModul_BukuPengawasanHutangPPhPasal23()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal23()
        host_BukuPengawasanHutangPPhPasal23 = New wpfHost_BukuPengawasanHutangPPhPasal23
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal23, host_BukuPengawasanHutangPPhPasal23.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal42_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal42.Click
        BukaModul_BukuPengawasanHutangPPhPasal42()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal42()
        host_BukuPengawasanHutangPPhPasal42 = New wpfHost_BukuPengawasanHutangPPhPasal42
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal42, host_BukuPengawasanHutangPPhPasal42.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal25_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal25.Click
        BukaModul_BukuPengawasanHutangPPhPasal25()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal25()
        host_BukuPengawasanHutangPPhPasal25 = New wpfHost_BukuPengawasanHutangPPhPasal25
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal25, host_BukuPengawasanHutangPPhPasal25.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal26_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal26.Click
        BukaModul_BukuPengawasanHutangPPhPasal26()
    End Sub
    Sub BukaModul_BukuPengawasanHutangPPhPasal26()
        host_BukuPengawasanHutangPPhPasal26 = New wpfHost_BukuPengawasanHutangPPhPasal26
        BukaUserControlDalamTab(usc_BukuPengawasanHutangPPhPasal26, host_BukuPengawasanHutangPPhPasal26.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal29_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal29.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_PPN_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PPN.Click
        BukaModul_BukuPengawasanPelaporanPPN()
    End Sub
    Sub BukaModul_BukuPengawasanPelaporanPPN()
        host_BukuPengawasanPelaporanPPN = New wpfHost_BukuPengawasanPelaporanPPN
        BukaUserControlDalamTab(usc_BukuPengawasanPelaporanPPN, host_BukuPengawasanPelaporanPPN.JudulForm)
    End Sub

    Private Sub mnu_KetetapanPajak_Click(sender As Object, e As RoutedEventArgs) Handles mnu_KetetapanPajak.Click
        BukaModul_BukuPengawasanKetetapanPajak()
    End Sub
    Sub BukaModul_BukuPengawasanKetetapanPajak()
        host_BukuPengawasanKetetapanPajak = New wpfHost_BukuPengawasanKetetapanPajak
        BukaUserControlDalamTab(usc_BukuPengawasanKetetapanPajak, host_BukuPengawasanKetetapanPajak.JudulForm)
    End Sub

    Private Sub mnu_PajakImpor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PajakImpor.Click
        BukaModul_BukuPengawasanPajakImpor()
    End Sub
    Sub BukaModul_BukuPengawasanPajakImpor()
        host_BukuPengawasanPajakImpor = New wpfHost_BukuPengawasanPajakImpor
        BukaUserControlDalamTab(usc_BukuPengawasanPajakImpor, host_BukuPengawasanPajakImpor.JudulForm)
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
        BukaModul_BukuPengawasanBuktiPotongPPh_Paid()
    End Sub
    Sub BukaModul_BukuPengawasanBuktiPotongPPh_Paid()
        host_BukuPengawasanBuktiPotongPPh_Paid = New wpfHost_BukuPengawasanBuktiPotongPPh_Paid
        BukaUserControlDalamTab(usc_BukuPengawasanBuktiPotongPPh_Paid, host_BukuPengawasanBuktiPotongPPh_Paid.JudulForm)
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPotongPPh_Prepaid_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPotongPPh_Prepaid.Click
        BukaModul_BukuPengawasanBuktiPotongPPh_Prepaid()
    End Sub
    Sub BukaModul_BukuPengawasanBuktiPotongPPh_Prepaid()
        host_BukuPengawasanBuktiPotongPPh_Prepaid = New wpfHost_BukuPengawasanBuktiPotongPPh_Prepaid
        BukaUserControlDalamTab(usc_BukuPengawasanBuktiPotongPPh_Prepaid, host_BukuPengawasanBuktiPotongPPh_Prepaid.JudulForm)
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
            Pesan_Informasi("Anda telah LOGOUT dari sistem.")
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
    End Sub

    Private Sub mnu_Help_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Help.Click
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
        host_ManajemenAplikasi = New wpfHost_ManajemenAplikasi
        BukaUserControlDalamTab(usc_ManajemenAplikasi, host_ManajemenAplikasi.JudulForm)
    End Sub

    Private Sub mnu_ManajemenClient_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenClient.Click
        host_ManajemenClient = New wpfHost_ManajemenClient
        BukaUserControlDalamTab(usc_ManajemenClient, host_ManajemenClient.JudulForm)
    End Sub

    Private Sub mnu_ManajemenKurs_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenKurs.Click
        host_ManajemenKurs = New wpfHost_ManajemenKurs
        BukaUserControlDalamTab(usc_ManajemenKurs, host_ManajemenKurs.JudulForm)
    End Sub

    Private Sub mnu_DataProduk_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataProduk.Click
        host_DataProdukApp = New wpfHost_DataProdukApp
        BukaUserControlDalamTab(usc_DataProdukApp, host_DataProdukApp.JudulForm)
    End Sub

    Private Sub mnu_DataPerangkat_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataPerangkat.Click
        host_DataPerangkatApp = New wpfHost_DataPerangkatApp
        BukaUserControlDalamTab(usc_DataPerangkatApp, host_DataPerangkatApp.JudulForm)
    End Sub

    Private Sub mnu_DataVoucher_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataVoucher.Click
        MenuIniMasihDalamPengembangan()
    End Sub

    Private Sub mnu_TabPokok_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TabPokok.Click
        host_TabPokok = New wpfHost_TabPokok
        BukaUserControlDalamTab(usc_TabPokok, host_TabPokok.JudulForm)
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
                host_DataCOA = New wpfHost_DataCOA
                BukaUserControlDalamTab(usc_DataCOA, host_DataCOA.JudulForm)

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
