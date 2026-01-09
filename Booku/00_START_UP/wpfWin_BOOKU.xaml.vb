Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Forms.Integration

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
    Public Sub BukaUserControlDalamTab(userControl As System.Windows.Controls.UserControl, tabHeader As String)
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
    ''' Membuka form WinForms dalam tab (fallback untuk yang belum migrasi)
    ''' </summary>
    Public Sub BukaFormDalamTab(form As System.Windows.Forms.Form, tabHeader As String)
        Dim tabExisting = CariTabByHeader(tabHeader)
        If tabExisting IsNot Nothing Then
            tab_MainContent.SelectedItem = tabExisting
            Return
        End If

        ' Konfigurasi form untuk di-host
        form.TopLevel = False
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        form.Dock = System.Windows.Forms.DockStyle.Fill

        Dim host As New WindowsFormsHost()
        host.Child = form

        Dim tabBaru = BuatTabDenganCloseButton(tabHeader)
        tabBaru.Content = host
        tab_MainContent.Items.Add(tabBaru)
        tab_MainContent.SelectedItem = tabBaru

        form.Show()
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
            .Background = System.Windows.Media.Brushes.Transparent,
            .BorderThickness = New Thickness(0),
            .Cursor = System.Windows.Input.Cursors.Hand,
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
            Dim form = TryCast(host.Child, System.Windows.Forms.Form)
            If form IsNot Nothing Then
                form.Close()
                form.Dispose()
            End If
            host.Dispose()
        End If

        tab_MainContent.Items.Remove(tab)
    End Sub

#End Region

#Region "Menu Event Handlers - Langsung ke UserControl"

    ' === FILE ===
    Private Sub mnu_Pengaturan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Pengaturan.Click
        win_Pengaturan = New wpfWin_Pengaturan With {.FungsiForm = "PENGATURAN"}
        win_Pengaturan.ShowDialog()
    End Sub

    Private Sub mnu_KembaliKeClassic_Click(sender As Object, e As RoutedEventArgs) Handles mnu_KembaliKeClassic.Click
        If System.Windows.MessageBox.Show(
            "Beralih ke Mode Classic? Aplikasi akan restart.",
            "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
            ModusAplikasi = "CLASSIC"
            System.Windows.Forms.Application.Restart()
            System.Windows.Application.Current.Shutdown()
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
        ' wpfUsc_DataKaryawan belum ada, gunakan fallback ke WinForms
        BukaFormDalamTab(New frm_DataKaryawan(), "Data Karyawan")
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
        usc_LaporanHPP = New wpfUsc_LaporanHPP
        BukaUserControlDalamTab(usc_LaporanHPP, "Laporan HPP")
    End Sub

    Private Sub mnu_LabaRugi_Bulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LabaRugi_Bulanan.Click
        usc_LaporanLabaRugi_Bulanan = New wpfUsc_LaporanLabaRugi_Bulanan
        BukaUserControlDalamTab(usc_LaporanLabaRugi_Bulanan, "Laba Rugi Bulanan")
    End Sub

    Private Sub mnu_LabaRugi_Tahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LabaRugi_Tahunan.Click
        usc_LaporanLabaRugi_Tahunan = New wpfUsc_LaporanLabaRugi_Tahunan
        BukaUserControlDalamTab(usc_LaporanLabaRugi_Tahunan, "Laba Rugi Tahunan")
    End Sub

    Private Sub mnu_Neraca_Bulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Neraca_Bulanan.Click
        usc_LaporanNeraca_Bulanan = New wpfUsc_LaporanNeraca_Bulanan
        BukaUserControlDalamTab(usc_LaporanNeraca_Bulanan, "Neraca Bulanan")
    End Sub

    Private Sub mnu_Neraca_Tahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Neraca_Tahunan.Click
        usc_LaporanNeraca_Tahunan = New wpfUsc_LaporanNeraca_Tahunan
        BukaUserControlDalamTab(usc_LaporanNeraca_Tahunan, "Neraca Tahunan")
    End Sub

    Private Sub mnu_NeracaLajur_Click(sender As Object, e As RoutedEventArgs) Handles mnu_NeracaLajur.Click
        usc_LaporanNeracaLajur = New wpfUsc_LaporanNeracaLajur
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
        usc_BukuPengawasanGaji = New wpfUsc_BukuPengawasanGaji
        BukaUserControlDalamTab(usc_BukuPengawasanGaji, "Buku Pengawasan Gaji")
    End Sub

    ' === ASSET ===
    Private Sub mnu_DaftarPenyusutanAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DaftarPenyusutanAssetTetap.Click
        usc_DaftarPenyusutanAssetTetap = New wpfUsc_DaftarPenyusutanAssetTetap
        BukaUserControlDalamTab(usc_DaftarPenyusutanAssetTetap, "Penyusutan Asset")
    End Sub

    Private Sub mnu_ManajemenAmortisasiBiaya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenAmortisasiBiaya.Click
        usc_DaftarAmortisasiBiaya = New wpfUsc_DaftarAmortisasiBiaya
        BukaUserControlDalamTab(usc_DaftarAmortisasiBiaya, "Amortisasi Biaya")
    End Sub

#End Region

#Region "Menu Event Handlers - Belum Diimplementasi"

    ' ============================================================
    ' FILE - DATABASE
    ' ============================================================
    Private Sub mnu_Database_Cadangkan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Database_Cadangkan.Click
    End Sub

    Private Sub mnu_Database_Pulihkan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Database_Pulihkan.Click
    End Sub

    Private Sub mnu_Database_Kloning_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Database_Kloning.Click
    End Sub

    ' ============================================================
    ' DATA - COMPANY PROFILE & PEMEGANG SAHAM
    ' ============================================================
    Private Sub mnu_CompanyProfile_Click(sender As Object, e As RoutedEventArgs) Handles mnu_CompanyProfile.Click
    End Sub

    Private Sub mnu_DaftarPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DaftarPemegangSaham.Click
    End Sub

    ' ============================================================
    ' DATA - DATA AWAL HUTANG
    ' ============================================================
    Private Sub mnu_DataAwal_HutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_NonAfiliasi.Click
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Afiliasi.Click
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_USD.Click
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_AUD.Click
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_JPY.Click
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_CNY.Click
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_EUR.Click
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_SGD.Click
    End Sub

    Private Sub mnu_DataAwal_HutangUsaha_Impor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangUsaha_Impor_GBP.Click
    End Sub

    Private Sub mnu_DataAwal_HutangBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangBank.Click
    End Sub

    Private Sub mnu_DataAwal_HutangLeasing_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangLeasing.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPihakKetiga.Click
    End Sub

    Private Sub mnu_DataAwal_HutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangAfiliasi.Click
    End Sub

    Private Sub mnu_DataAwal_HutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangKaryawan.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPemegangSaham.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal21_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal21.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal23_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal23.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal42_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal42.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal25_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal25.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal26_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal26.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPPhPasal29_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPhPasal29.Click
    End Sub

    Private Sub mnu_DataAwal_HutangPPN_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangPPN.Click
    End Sub

    Private Sub mnu_DataAwal_HutangKetetapanPajak_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangKetetapanPajak.Click
    End Sub

    Private Sub mnu_DataAwal_HutangGaji_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangGaji.Click
    End Sub

    Private Sub mnu_DataAwal_HutangBPJSKesehatan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangBPJSKesehatan.Click
    End Sub

    Private Sub mnu_DataAwal_HutangBPJSKetenagakerjaan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangBPJSKetenagakerjaan.Click
    End Sub

    Private Sub mnu_DataAwal_HutangKoperasiKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangKoperasiKaryawan.Click
    End Sub

    Private Sub mnu_DataAwal_HutangSerikat_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_HutangSerikat.Click
    End Sub

    ' ============================================================
    ' DATA - DATA AWAL PIUTANG
    ' ============================================================
    Private Sub mnu_DataAwal_PiutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_NonAfiliasi.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Afiliasi.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_USD.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_AUD.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_JPY.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_CNY.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_EUR.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_SGD.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangUsaha_Ekspor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangUsaha_Ekspor_GBP.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangPihakKetiga.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangAfiliasi.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangKaryawan.Click
    End Sub

    Private Sub mnu_DataAwal_PiutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_PiutangPemegangSaham.Click
    End Sub

    Private Sub mnu_DataAwal_DepositOperasional_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_DepositOperasional.Click
    End Sub

    ' ============================================================
    ' DATA - DATA AWAL ASSET
    ' ============================================================
    Private Sub mnu_DataAwal_AmortisasiBiaya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_AmortisasiBiaya.Click
    End Sub

    Private Sub mnu_DataAwal_AssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataAwal_AssetTetap.Click
    End Sub

    ' ============================================================
    ' DATA - TAHUN BUKU
    ' ============================================================
    Private Sub mnu_BuatBukuBaru_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BuatBukuBaru.Click
    End Sub

    Private Sub mnu_GantiTahunBuku_Click(sender As Object, e As RoutedEventArgs) Handles mnu_GantiTahunBuku.Click
    End Sub

    Private Sub mnu_TutupBuku_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TutupBuku.Click
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
    End Sub

    Private Sub mnu_TransaksiOUT_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TransaksiOUT.Click
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
    End Sub

    Private Sub mnu_POPembelian_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_Jasa.Click
    End Sub

    Private Sub mnu_POPembelian_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_BarangDanJasa.Click
    End Sub

    Private Sub mnu_POPembelian_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Lokal_JasaKonstruksi.Click
    End Sub

    Private Sub mnu_POPembelian_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPembelian_Semua.Click
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Barang.Click
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Jasa.Click
    End Sub

    Private Sub mnu_PO_Pembelian_Impor_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PO_Pembelian_Impor_Semua.Click
    End Sub

    ' ============================================================
    ' PEMBELIAN - SURAT JALAN & BAST
    ' ============================================================
    Private Sub mnu_SuratJalanPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_SuratJalanPembelian.Click
    End Sub

    Private Sub mnu_BASTPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BASTPembelian.Click
    End Sub

    ' ============================================================
    ' PEMBELIAN - INVOICE DENGAN PO
    ' ============================================================
    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal_Rutin.Click
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Lokal_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Lokal_Termin.Click
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor_Rutin.Click
    End Sub

    Private Sub mnu_InvoicePembelian_DenganPO_Impor_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_DenganPO_Impor_Termin.Click
    End Sub

    ' ============================================================
    ' PEMBELIAN - INVOICE TANPA PO
    ' ============================================================
    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_Barang.Click
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_Jasa.Click
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Click
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Click
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor_Barang.Click
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_Impor_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_Impor_Jasa.Click
    End Sub

    Private Sub mnu_InvoicePembelian_TanpaPO_LokalMUA_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePembelian_TanpaPO_LokalMUA.Click
    End Sub

    ' ============================================================
    ' PEMBELIAN - BUKU & RETUR
    ' ============================================================
    Private Sub mnu_BukuPembelian_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPembelian_Lokal.Click
    End Sub

    Private Sub mnu_BukuPembelian_Impor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPembelian_Impor.Click
    End Sub

    Private Sub mnu_ReturPembelian_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ReturPembelian.Click
    End Sub

    ' ============================================================
    ' PENJUALAN - PO
    ' ============================================================
    Private Sub mnu_POPenjualan_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Barang.Click
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Jasa.Click
    End Sub

    Private Sub mnu_POPenjualan_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_BarangDanJasa.Click
    End Sub

    Private Sub mnu_POPenjualan_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_JasaKonstruksi.Click
    End Sub

    Private Sub mnu_POPenjualan_Lokal_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Lokal_Semua.Click
    End Sub

    Private Sub mnu_POPenjualan_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_POPenjualan_Ekspor.Click
    End Sub

    ' ============================================================
    ' PENJUALAN - SURAT JALAN & BAST
    ' ============================================================
    Private Sub mnu_SuratJalanPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_SuratJalanPenjualan.Click
    End Sub

    Private Sub mnu_BASTPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BASTPenjualan.Click
    End Sub

    ' ============================================================
    ' PENJUALAN - INVOICE DENGAN PO
    ' ============================================================
    Private Sub mnu_InvoicePenjualan_DenganPO_Lokal_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Lokal_Rutin.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Lokal_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Lokal_Termin.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_DenganPO_Ekspor_Termin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_DenganPO_Ekspor_Termin.Click
    End Sub

    ' ============================================================
    ' PENJUALAN - INVOICE TANPA PO
    ' ============================================================
    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_Barang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_Barang.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Ekspor.Click
    End Sub

    Private Sub mnu_InvoicePenjualan_TanpaPO_Asset_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InvoicePenjualan_TanpaPO_Asset.Click
    End Sub

    ' ============================================================
    ' PENJUALAN - BUKU & RETUR
    ' ============================================================
    Private Sub mnu_BukuPenjualan_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualan_Lokal.Click
    End Sub

    Private Sub mnu_BukuPenjualan_Ekspor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualan_Ekspor.Click
    End Sub

    Private Sub mnu_BukuPenjualanEceran_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualanEceran.Click
    End Sub

    Private Sub mnu_BukuPengawasanReturPenjualan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanReturPenjualan.Click
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - BANK & CASH
    ' ============================================================
    Private Sub mnu_BukuCashAdvance_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuCashAdvance.Click
    End Sub

    Private Sub mnu_BukuBankGaransi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBankGaransi.Click
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - GAJI
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangBPJSKesehatan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBPJSKesehatan.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangBPJSKetenagakerjaan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBPJSKetenagakerjaan.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangKoperasiKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangKoperasiKaryawan.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangSerikat_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangSerikat.Click
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - HUTANG USAHA
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_NonAfiliasi.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Afiliasi.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Semua.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_USD.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_AUD.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_JPY.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_CNY.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_EUR.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_SGD.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangUsaha_Impor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangUsaha_Impor_GBP.Click
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - HUTANG LAINNYA
    ' ============================================================
    Private Sub mnu_BukuPengawasanHutangBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangBank.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangLeasing_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangLeasing.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPihakKetiga.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangAfiliasi.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangKaryawan.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPemegangSaham.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangDividen_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangDividen.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangLainnya.Click
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - PIUTANG USAHA
    ' ============================================================
    Private Sub mnu_BukuPengawasanPiutangUsaha_NonAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_NonAfiliasi.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Afiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Afiliasi.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Semua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Semua.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_USD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_USD.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP.Click
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - PIUTANG LAINNYA
    ' ============================================================
    Private Sub mnu_BukuPengawasanPiutangPihakKetiga_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangPihakKetiga.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangAfiliasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangAfiliasi.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangKaryawan.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangPemegangSaham_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangPemegangSaham.Click
    End Sub

    Private Sub mnu_BukuPengawasanDepositOperasional_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanDepositOperasional.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangDividen_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangDividen.Click
    End Sub

    Private Sub mnu_BukuPengawasanPiutangLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPiutangLainnya.Click
    End Sub

    ' ============================================================
    ' BUKU PENGAWASAN - BUKTI & PEMINDABUKUAN
    ' ============================================================
    Private Sub mnu_BukuPengawasanBuktiPenerimaanBankCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPenerimaanBankCash.Click
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPengeluaranBankCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPengeluaranBankCash.Click
    End Sub

    Private Sub mnu_BukuPengawasanPemindabukuan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanPemindabukuan.Click
    End Sub

    Private Sub mnu_BukuPengawasanAktivaLainnya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanAktivaLainnya.Click
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
    End Sub

    Private Sub mnu_StockOpname_BahanBaku_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BahanBaku.Click
    End Sub

    Private Sub mnu_StockOpname_BarangDalamProses_CekFisik_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangDalamProses_CekFisik.Click
    End Sub

    Private Sub mnu_StockOpname_BarangDalamProses_TarikanData_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangDalamProses_TarikanData.Click
    End Sub

    Private Sub mnu_StockOpname_BarangJadi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_StockOpname_BarangJadi.Click
    End Sub

    ' ============================================================
    ' AKUNTANSI - JURNAL ADJUSMENT
    ' ============================================================
    Private Sub mnu_JurnalAdjusment_Penyusutan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Penyusutan.Click
    End Sub

    Private Sub mnu_JurnalAdjusment_Amortisasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Amortisasi.Click
    End Sub

    Private Sub mnu_JurnalAdjusment_Forex_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_Forex.Click
    End Sub

    Private Sub mnu_JurnalAdjusment_HPP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalAdjusment_HPP.Click
    End Sub

    Private Sub mnu_LaporanAktivitasTransaksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LaporanAktivitasTransaksi.Click
    End Sub

    ' ============================================================
    ' MANAJEMEN ASSET
    ' ============================================================
    Private Sub mnu_ManajemenAmortisasiAssetTidakBerwujud_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenAmortisasiAssetTidakBerwujud.Click
    End Sub

    Private Sub mnu_BukuPenjualanAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPenjualanAssetTetap.Click
    End Sub

    Private Sub mnu_BukuDisposalAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuDisposalAssetTetap.Click
    End Sub

    ' ============================================================
    ' PAJAK
    ' ============================================================
    Private Sub mnu_ProfilPajakPerusahaan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ProfilPajakPerusahaan.Click
    End Sub

    Private Sub mnu_PerhitunganPajakPajakBulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PerhitunganPajakPajakBulanan.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal21_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal21.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal22_Lokal_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22_Lokal.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal22_Impor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal22_Impor.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal23_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal23.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal42_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal42.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal25_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal25.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal26_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal26.Click
    End Sub

    Private Sub mnu_BukuPengawasanHutangPPhPasal29_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanHutangPPhPasal29.Click
    End Sub

    Private Sub mnu_PPN_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PPN.Click
    End Sub

    Private Sub mnu_KetetapanPajak_Click(sender As Object, e As RoutedEventArgs) Handles mnu_KetetapanPajak.Click
    End Sub

    Private Sub mnu_PajakImpor_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PajakImpor.Click
    End Sub

    Private Sub mnu_InputBuktiPBk_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InputBuktiPBk.Click
    End Sub

    Private Sub mnu_InputKetetapanPajak_Click(sender As Object, e As RoutedEventArgs) Handles mnu_InputKetetapanPajak.Click
    End Sub

    Private Sub mnu_PerhitunganEqualisasiPajakPajakTahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PerhitunganEqualisasiPajakPajakTahunan.Click
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPotongPPh_Paid_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPotongPPh_Paid.Click
    End Sub

    Private Sub mnu_BukuPengawasanBuktiPotongPPh_Prepaid_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanBuktiPotongPPh_Prepaid.Click
    End Sub

    ' ============================================================
    ' USER
    ' ============================================================
    Private Sub mnu_SwitchUser_Click(sender As Object, e As RoutedEventArgs) Handles mnu_SwitchUser.Click
    End Sub

    Private Sub mnu_GantiPassword_Click(sender As Object, e As RoutedEventArgs) Handles mnu_GantiPassword.Click
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
    End Sub

    ' ============================================================
    ' JENDELA
    ' ============================================================
    Private Sub mnu_Jendela_TutupSemua_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Jendela_TutupSemua.Click
    End Sub

    ' ============================================================
    ' TENTANG, HELP, REGISTRASI, NOTIFIKASI
    ' ============================================================
    Private Sub mnu_Tentang_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Tentang.Click
    End Sub

    Private Sub mnu_Help_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Help.Click
    End Sub

    Private Sub mnu_Registrasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Registrasi.Click
    End Sub

    Private Sub mnu_Notifikasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Notifikasi.Click
    End Sub

    ' ============================================================
    ' TECHNICAL SUPPORT
    ' ============================================================
    Private Sub mnu_PhpMyAdmin_Click(sender As Object, e As RoutedEventArgs) Handles mnu_PhpMyAdmin.Click
    End Sub

    ' ============================================================
    ' APP DEVELOPER
    ' ============================================================
    Private Sub mnu_ManajemenAplikasi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenAplikasi.Click
    End Sub

    Private Sub mnu_ManajemenClient_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenClient.Click
    End Sub

    Private Sub mnu_ManajemenKurs_Click(sender As Object, e As RoutedEventArgs) Handles mnu_ManajemenKurs.Click
    End Sub

    Private Sub mnu_DataProduk_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataProduk.Click
    End Sub

    Private Sub mnu_DataPerangkat_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataPerangkat.Click
    End Sub

    Private Sub mnu_DataVoucher_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataVoucher.Click
    End Sub

    Private Sub mnu_TabPokok_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TabPokok.Click
    End Sub

    Private Sub mnu_TryApp_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TryApp.Click
    End Sub

#End Region

    Private Sub wpfWin_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Cek apakah perlu konfirmasi
        If PaksaKeluarAplikasi Then
            e.Cancel = False
        Else
            Dim hasil = System.Windows.MessageBox.Show(
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
        For Each item As TabItem In tab_MainContent.Items
            If TypeOf item.Content Is WindowsFormsHost Then
                Dim host = CType(item.Content, WindowsFormsHost)
                Dim form = TryCast(host.Child, System.Windows.Forms.Form)
                If form IsNot Nothing Then
                    form.Close()
                    form.Dispose()
                End If
                host.Dispose()
            End If
        Next

        ' Karena ini adalah WPF Shell yang menggantikan frm_BOOKU,
        ' saat ditutup harus keluar dari aplikasi sepenuhnya
        System.Windows.Forms.Application.Exit()
    End Sub

End Class
