Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Forms.Integration

''' <summary>
''' WPF Application Shell untuk BOOKU (Mode Modern)
''' - Langsung membuka WPF UserControl jika tersedia
''' - Fallback ke WindowsFormsHost untuk form yang belum migrasi
''' </summary>
Public Class wpfWin_BOOKU

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
        If System.Windows.MessageBox.Show("Keluar dari aplikasi?", "Konfirmasi",
            MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
            System.Windows.Application.Current.Shutdown()
        End If
    End Sub

    ' === DATA - Langsung ke WPF UserControl ===
    Private Sub mnu_DataUser_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataUser.Click
        BukaUserControlDalamTab(New wpfUsc_DataUser(), "Data User")
    End Sub

    Private Sub mnu_DataCOA_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataCOA.Click
        BukaUserControlDalamTab(New wpfUsc_DataCOA(), "Data COA")
    End Sub

    Private Sub mnu_DataLawanTransaksi_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataLawanTransaksi.Click
        BukaUserControlDalamTab(New wpfUsc_DataLawanTransaksi(), "Data Lawan Transaksi")
    End Sub

    Private Sub mnu_DataKaryawan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataKaryawan.Click
        ' wpfUsc_DataKaryawan belum ada, gunakan fallback ke WinForms
        BukaFormDalamTab(New frm_DataKaryawan(), "Data Karyawan")
    End Sub

    Private Sub mnu_DataProject_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DataProject.Click
        BukaUserControlDalamTab(New wpfUsc_DataProject(), "Data Project")
    End Sub

    Private Sub mnu_Kurs_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Kurs.Click
        BukaUserControlDalamTab(New wpfUsc_Kurs(), "Kurs")
    End Sub

    ' === AKUNTANSI - Langsung ke WPF UserControl ===
    Private Sub mnu_JurnalUmum_Click(sender As Object, e As RoutedEventArgs) Handles mnu_JurnalUmum.Click
        BukaUserControlDalamTab(New wpfUsc_JurnalUmum(), "Jurnal Umum")
    End Sub

    Private Sub mnu_BukuBesar_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBesar.Click
        BukaUserControlDalamTab(New wpfUsc_BukuBesar(), "Buku Besar")
    End Sub

    Private Sub mnu_TrialBalance_Click(sender As Object, e As RoutedEventArgs) Handles mnu_TrialBalance.Click
        BukaUserControlDalamTab(New wpfUsc_LaporanTrialBalance(), "Trial Balance")
    End Sub

    Private Sub mnu_LaporanHPP_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LaporanHPP.Click
        BukaUserControlDalamTab(New wpfUsc_LaporanHPP(), "Laporan HPP")
    End Sub

    Private Sub mnu_LabaRugi_Bulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LabaRugi_Bulanan.Click
        BukaUserControlDalamTab(New wpfUsc_LaporanLabaRugi_Bulanan(), "Laba Rugi Bulanan")
    End Sub

    Private Sub mnu_LabaRugi_Tahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_LabaRugi_Tahunan.Click
        BukaUserControlDalamTab(New wpfUsc_LaporanLabaRugi_Tahunan(), "Laba Rugi Tahunan")
    End Sub

    Private Sub mnu_Neraca_Bulanan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Neraca_Bulanan.Click
        BukaUserControlDalamTab(New wpfUsc_LaporanNeraca_Bulanan(), "Neraca Bulanan")
    End Sub

    Private Sub mnu_Neraca_Tahunan_Click(sender As Object, e As RoutedEventArgs) Handles mnu_Neraca_Tahunan.Click
        BukaUserControlDalamTab(New wpfUsc_LaporanNeraca_Tahunan(), "Neraca Tahunan")
    End Sub

    Private Sub mnu_NeracaLajur_Click(sender As Object, e As RoutedEventArgs) Handles mnu_NeracaLajur.Click
        BukaUserControlDalamTab(New wpfUsc_LaporanNeracaLajur(), "Neraca Lajur")
    End Sub

    ' === BUKU PENGAWASAN ===
    Private Sub mnu_BukuBank_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuBank.Click
        ' wpfUsc_BukuBank belum ada, gunakan fallback ke WinForms
        BukaFormDalamTab(New frm_BukuBank(), "Buku Bank")
    End Sub

    Private Sub mnu_BukuKas_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuKas.Click
        ' wpfUsc_BukuKas belum ada, gunakan fallback ke WinForms
        BukaFormDalamTab(New frm_BukuKas(), "Buku Kas")
    End Sub

    Private Sub mnu_BukuPettyCash_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPettyCash.Click
        ' wpfUsc_BukuPettyCash belum ada, gunakan fallback ke WinForms
        BukaFormDalamTab(New frm_BukuPettyCash(), "Petty Cash")
    End Sub

    Private Sub mnu_BukuPengawasanGaji_Click(sender As Object, e As RoutedEventArgs) Handles mnu_BukuPengawasanGaji.Click
        BukaUserControlDalamTab(New wpfUsc_BukuPengawasanGaji(), "Buku Pengawasan Gaji")
    End Sub

    ' === ASSET ===
    Private Sub mnu_DaftarPenyusutanAssetTetap_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DaftarPenyusutanAssetTetap.Click
        BukaUserControlDalamTab(New wpfUsc_DaftarPenyusutanAssetTetap(), "Penyusutan Asset")
    End Sub

    Private Sub mnu_DaftarAmortisasiBiaya_Click(sender As Object, e As RoutedEventArgs) Handles mnu_DaftarAmortisasiBiaya.Click
        BukaUserControlDalamTab(New wpfUsc_DaftarAmortisasiBiaya(), "Amortisasi Biaya")
    End Sub

#End Region

    Private Sub wpfWin_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
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
    End Sub

End Class
