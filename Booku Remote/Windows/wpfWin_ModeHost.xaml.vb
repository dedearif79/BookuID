Option Explicit On
Option Strict On

Imports System.Net.Sockets
Imports System.Windows
Imports System.Windows.Media
Imports BookuID.Styles

''' <summary>
''' Window Mode Host - Menunggu dan menerima koneksi dari Tamu.
''' Mendukung mode LAN (jaringan lokal) dan Internet (via Relay Server).
''' </summary>
Class wpfWin_ModeHost

#Region "Variables"

    Private _riwayatKoneksi As New List(Of String)
    Private _hostAktif As Boolean = False
    Private _modeKoneksi As ModeKoneksi = ModeKoneksi.LAN

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Styling window
        StyleWindowDialogWPF_Dasar(Me)

        ' Tampilkan info perangkat
        lbl_NamaPerangkat.Text = NamaPerangkatIni
        lbl_AlamatIP.Text = AlamatIPLokal
        lbl_Port.Text = PortKoneksiAktif.ToString()

        ' Isi nilai pengaturan port
        MuatPengaturanPortKeUI()

        ' Subscribe ke events LAN
        AddHandler mdl_KoneksiJaringan.PermintaanKoneksiMasuk, AddressOf OnPermintaanKoneksiMasuk
        AddHandler mdl_KoneksiJaringan.KoneksiBerhasil, AddressOf OnKoneksiBerhasil
        AddHandler mdl_KoneksiJaringan.KoneksiTerputus, AddressOf OnKoneksiTerputus
        AddHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi

        ' Subscribe ke events Relay (Internet)
        AddHandler mdl_KoneksiRelay.TerdaftarDiRelay, AddressOf OnTerdaftarDiRelay
        AddHandler mdl_KoneksiRelay.KoneksiRelayTerputus, AddressOf OnKoneksiRelayTerputus
        AddHandler mdl_KoneksiRelay.PermintaanKoneksiViaRelay, AddressOf OnPermintaanKoneksiViaRelay
        AddHandler mdl_KoneksiRelay.ErrorDariRelay, AddressOf OnErrorDariRelay
        AddHandler mdl_KoneksiRelay.PaketDariRelay, AddressOf OnPaketDariRelay

        ' Subscribe radio button events
        AddHandler rdo_ModeLAN.Checked, AddressOf OnModeKoneksiBerubah
        AddHandler rdo_ModeInternet.Checked, AddressOf OnModeKoneksiBerubah

        ' Aktifkan Host secara otomatis (mode LAN default)
        AktifkanHost()
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Unsubscribe events LAN
        RemoveHandler mdl_KoneksiJaringan.PermintaanKoneksiMasuk, AddressOf OnPermintaanKoneksiMasuk
        RemoveHandler mdl_KoneksiJaringan.KoneksiBerhasil, AddressOf OnKoneksiBerhasil
        RemoveHandler mdl_KoneksiJaringan.KoneksiTerputus, AddressOf OnKoneksiTerputus
        RemoveHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi

        ' Unsubscribe events Relay
        RemoveHandler mdl_KoneksiRelay.TerdaftarDiRelay, AddressOf OnTerdaftarDiRelay
        RemoveHandler mdl_KoneksiRelay.KoneksiRelayTerputus, AddressOf OnKoneksiRelayTerputus
        RemoveHandler mdl_KoneksiRelay.PermintaanKoneksiViaRelay, AddressOf OnPermintaanKoneksiViaRelay
        RemoveHandler mdl_KoneksiRelay.ErrorDariRelay, AddressOf OnErrorDariRelay
        RemoveHandler mdl_KoneksiRelay.PaketDariRelay, AddressOf OnPaketDariRelay

        ' Nonaktifkan Host
        NonaktifkanHost()
    End Sub

#End Region

#Region "Mode Koneksi"

    Private Sub OnModeKoneksiBerubah(sender As Object, e As RoutedEventArgs)
        ' Tentukan mode baru
        Dim modeBaru = If(rdo_ModeInternet.IsChecked.GetValueOrDefault(), ModeKoneksi.INTERNET, ModeKoneksi.LAN)

        ' Jika mode sama, skip
        If modeBaru = _modeKoneksi AndAlso _hostAktif Then Return

        ' Nonaktifkan mode lama
        NonaktifkanHost()

        ' Set mode baru
        _modeKoneksi = modeBaru
        ModeKoneksiSaatIni = modeBaru

        ' Update UI
        If modeBaru = ModeKoneksi.INTERNET Then
            pnl_HostCode.Visibility = Visibility.Visible
            lbl_Port.Text = PortRelayAktif.ToString()
            lbl_AlamatIP.Text = RelayServerIPAktif
        Else
            pnl_HostCode.Visibility = Visibility.Collapsed
            lbl_Port.Text = PortKoneksiAktif.ToString()
            lbl_AlamatIP.Text = AlamatIPLokal
        End If

        ' Aktifkan mode baru
        AktifkanHost()
    End Sub

#End Region

#Region "Host Control"

    Private Async Sub AktifkanHost()
        If _hostAktif Then Return

        Try
            If _modeKoneksi = ModeKoneksi.LAN Then
                ' === MODE LAN ===
                ' Mulai discovery listener
                mdl_PenemuanPerangkat.MulaiMendengarkanDiscovery()

                ' Mulai TCP server
                mdl_KoneksiJaringan.MulaiServer()

                _hostAktif = True
                UpdateStatusUI(True, "AKTIF (LAN)")
                TambahRiwayat($"{DateTime.Now:HH:mm} - Host diaktifkan (mode LAN)")

            Else
                ' === MODE INTERNET ===
                ' Tampilkan status menghubungkan
                UpdateStatusUI(False, "MENGHUBUNGKAN...")
                lbl_HostCode.Text = "------"

                ' Hubungkan ke relay server
                Dim berhasil = Await mdl_KoneksiRelay.HubungkanKeRelayAsync()

                If berhasil Then
                    _hostAktif = True
                    UpdateStatusUI(True, "TERHUBUNG KE RELAY")
                    TambahRiwayat($"{DateTime.Now:HH:mm} - Terhubung ke relay server")
                    ' HostCode akan diupdate saat event TerdaftarDiRelay
                Else
                    UpdateStatusUI(False, "GAGAL TERHUBUNG")
                    TambahRiwayat($"{DateTime.Now:HH:mm} - Gagal terhubung ke relay server")
                    MessageBox.Show("Gagal menghubungkan ke relay server. Pastikan koneksi internet aktif.",
                                   "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show($"Gagal mengaktifkan Host: {ex.Message}", "Error",
                           MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub NonaktifkanHost()
        If Not _hostAktif Then Return

        Try
            If _modeKoneksi = ModeKoneksi.LAN Then
                ' === MODE LAN ===
                mdl_PenemuanPerangkat.HentikanDiscovery()
                mdl_KoneksiJaringan.HentikanServer()
            Else
                ' === MODE INTERNET ===
                mdl_KoneksiRelay.TutupKoneksiRelay()
                lbl_HostCode.Text = "------"
            End If

            _hostAktif = False
            UpdateStatusUI(False)
            TambahRiwayat($"{DateTime.Now:HH:mm} - Host dinonaktifkan")

        Catch ex As Exception
            MessageBox.Show($"Gagal menonaktifkan Host: {ex.Message}", "Error",
                           MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub UpdateStatusUI(aktif As Boolean, Optional statusText As String = "")
        If aktif Then
            led_Status.Fill = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50)) ' Green
            lbl_Status.Text = If(statusText <> "", statusText, "AKTIF")
            lbl_Status.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50))
            btn_NonaktifkanHost.Content = "Nonaktifkan Host"
        Else
            led_Status.Fill = New SolidColorBrush(Color.FromRgb(&H9E, &H9E, &H9E)) ' Gray
            lbl_Status.Text = If(statusText <> "", statusText, "TIDAK AKTIF")
            lbl_Status.Foreground = New SolidColorBrush(Color.FromRgb(&H9E, &H9E, &H9E))
            btn_NonaktifkanHost.Content = "Aktifkan Host"
        End If
    End Sub

#End Region

#Region "Event Handlers - Relay (Internet)"

    Private Sub OnTerdaftarDiRelay(hostCode As String, expiryMinutes As Integer)
        Dispatcher.Invoke(Sub()
                              ' Update HostCode di UI
                              lbl_HostCode.Text = hostCode
                              UpdateStatusUI(True, $"AKTIF (Internet)")
                              TambahRiwayat($"{DateTime.Now:HH:mm} - HostCode: {hostCode} (berlaku {expiryMinutes} menit)")
                          End Sub)
    End Sub

    Private Sub OnKoneksiRelayTerputus(alasan As String)
        Dispatcher.Invoke(Sub()
                              _hostAktif = False
                              lbl_HostCode.Text = "------"
                              UpdateStatusUI(False, "TERPUTUS")
                              TambahRiwayat($"{DateTime.Now:HH:mm} - Koneksi relay terputus: {alasan}")
                          End Sub)
    End Sub

    Private Sub OnPermintaanKoneksiViaRelay(idSesi As String, namaTamu As String, alamatIP As String)
        Dispatcher.Invoke(Sub()
                              TambahRiwayat($"{DateTime.Now:HH:mm} - Permintaan dari {namaTamu} ({alamatIP}) via Internet")

                              If chk_MintaPersetujuan.IsChecked.GetValueOrDefault() Then
                                  ' Tampilkan dialog persetujuan
                                  Dim winPersetujuan As New wpfWin_PersetujuanKoneksi()
                                  winPersetujuan.Owner = Me
                                  winPersetujuan.NamaPengirim = namaTamu
                                  winPersetujuan.AlamatIPPengirim = alamatIP
                                  winPersetujuan.IzinTransferBerkas = chk_IzinTransferBerkas.IsChecked.GetValueOrDefault()
                                  winPersetujuan.IzinClipboard = chk_IzinClipboard.IsChecked.GetValueOrDefault()
                                  winPersetujuan.ShowDialog()

                                  ' Kirim respon via relay
                                  KirimResponKoneksiViaRelayAsync(idSesi, winPersetujuan.Diterima, winPersetujuan.IzinKontrol)

                                  If winPersetujuan.Diterima Then
                                      TambahRiwayat($"{DateTime.Now:HH:mm} - Koneksi diterima: {namaTamu}")
                                  Else
                                      TambahRiwayat($"{DateTime.Now:HH:mm} - Koneksi ditolak: {namaTamu}")
                                  End If
                              Else
                                  ' Auto terima
                                  KirimResponKoneksiViaRelayAsync(idSesi, True, True)
                                  TambahRiwayat($"{DateTime.Now:HH:mm} - Koneksi diterima (auto): {namaTamu}")
                              End If
                          End Sub)
    End Sub

    Private Async Sub KirimResponKoneksiViaRelayAsync(idSesi As String, diterima As Boolean, izinKontrol As Boolean)
        Await mdl_KoneksiRelay.KirimResponKoneksiViaRelayAsync(idSesi, diterima, izinKontrol)

        If diterima Then
            ' Update status UI
            Dispatcher.Invoke(Sub()
                                  led_Status.Fill = New SolidColorBrush(Color.FromRgb(&H21, &H96, &HF3)) ' Blue
                                  lbl_Status.Text = "TERHUBUNG"
                                  lbl_Status.Foreground = New SolidColorBrush(Color.FromRgb(&H21, &H96, &HF3))
                              End Sub)
        End If
    End Sub

    Private Sub OnErrorDariRelay(kodeError As Integer, pesan As String)
        Dispatcher.Invoke(Sub()
                              TambahRiwayat($"{DateTime.Now:HH:mm} - Error relay ({kodeError}): {pesan}")
                          End Sub)
    End Sub

    Private Sub OnPaketDariRelay(paket As cls_PaketData)
        ' Forward paket ke handler yang ada di mdl_KoneksiJaringan
        ' Ini akan handle PERMINTAAN_STREAMING, INPUT_KEYBOARD, INPUT_MOUSE, dll
        mdl_KoneksiJaringan.ProsesPaketMasukViaRelay(paket)
    End Sub

#End Region

#Region "Event Handlers - Koneksi"

    Private Sub OnPermintaanKoneksiMasuk(permintaan As cls_PayloadPermintaanKoneksi, clientSocket As TcpClient)
        ' Jalankan di UI thread
        Dispatcher.Invoke(Sub()
                              TambahRiwayat($"{DateTime.Now:HH:mm} - Permintaan dari {permintaan.NamaPerangkat} ({permintaan.AlamatIP})")

                              If chk_MintaPersetujuan.IsChecked.GetValueOrDefault() Then
                                  ' Tampilkan dialog persetujuan
                                  Dim winPersetujuan As New wpfWin_PersetujuanKoneksi()
                                  winPersetujuan.Owner = Me
                                  winPersetujuan.NamaPengirim = permintaan.NamaPerangkat
                                  winPersetujuan.AlamatIPPengirim = permintaan.AlamatIP
                                  winPersetujuan.IzinTransferBerkas = chk_IzinTransferBerkas.IsChecked.GetValueOrDefault()
                                  winPersetujuan.IzinClipboard = chk_IzinClipboard.IsChecked.GetValueOrDefault()
                                  winPersetujuan.ShowDialog()

                                  ' Kirim respon berdasarkan hasil dialog
                                  If winPersetujuan.Diterima Then
                                      KirimResponKoneksiAsync(clientSocket, HasilPersetujuan.DITERIMA,
                                                              winPersetujuan.IzinKontrol,
                                                              winPersetujuan.IzinTransferBerkas,
                                                              winPersetujuan.IzinClipboard)
                                      TambahRiwayat($"{DateTime.Now:HH:mm} - Koneksi diterima: {permintaan.NamaPerangkat}")
                                  Else
                                      KirimResponKoneksiAsync(clientSocket, HasilPersetujuan.DITOLAK, False, False, False, "Ditolak oleh Host")
                                      TambahRiwayat($"{DateTime.Now:HH:mm} - Koneksi ditolak: {permintaan.NamaPerangkat}")
                                  End If
                              Else
                                  ' Auto terima
                                  KirimResponKoneksiAsync(clientSocket, HasilPersetujuan.DITERIMA,
                                                          True,
                                                          chk_IzinTransferBerkas.IsChecked.GetValueOrDefault(),
                                                          chk_IzinClipboard.IsChecked.GetValueOrDefault())
                                  TambahRiwayat($"{DateTime.Now:HH:mm} - Koneksi diterima (auto): {permintaan.NamaPerangkat}")
                              End If
                          End Sub)
    End Sub

    Private Async Sub KirimResponKoneksiAsync(clientSocket As TcpClient, hasil As HasilPersetujuan,
                                               izinKontrol As Boolean, izinTransfer As Boolean,
                                               izinClipboard As Boolean, Optional pesan As String = "")
        Await mdl_KoneksiJaringan.KirimResponKoneksiAsync(clientSocket, hasil, izinKontrol, izinTransfer, izinClipboard, pesan)
    End Sub

    Private Sub OnKoneksiBerhasil(kunciSesi As String)
        Dispatcher.Invoke(Sub()
                              TambahRiwayat($"{DateTime.Now:HH:mm} - Sesi aktif dimulai")
                              led_Status.Fill = New SolidColorBrush(Color.FromRgb(&H21, &H96, &HF3)) ' Blue - connected
                              lbl_Status.Text = "TERHUBUNG"
                              lbl_Status.Foreground = New SolidColorBrush(Color.FromRgb(&H21, &H96, &HF3))
                          End Sub)
    End Sub

    Private Sub OnKoneksiTerputus(alasan As String)
        Dispatcher.Invoke(Sub()
                              TambahRiwayat($"{DateTime.Now:HH:mm} - Koneksi terputus: {alasan}")
                              UpdateStatusUI(_hostAktif)
                          End Sub)
    End Sub

    Private Sub OnErrorKoneksi(pesan As String)
        Dispatcher.Invoke(Sub()
                              TambahRiwayat($"{DateTime.Now:HH:mm} - Error: {pesan}")
                          End Sub)
    End Sub

#End Region

#Region "Riwayat"

    Private Sub TambahRiwayat(teks As String)
        _riwayatKoneksi.Insert(0, teks)
        If _riwayatKoneksi.Count > 50 Then
            _riwayatKoneksi.RemoveAt(_riwayatKoneksi.Count - 1)
        End If

        lst_RiwayatKoneksi.ItemsSource = Nothing
        lst_RiwayatKoneksi.ItemsSource = _riwayatKoneksi
        lbl_RiwayatKosong.Visibility = If(_riwayatKoneksi.Count > 0, Visibility.Collapsed, Visibility.Visible)
    End Sub

#End Region

#Region "Button Click Handlers"

    Private Sub btn_NonaktifkanHost_Click(sender As Object, e As RoutedEventArgs) Handles btn_NonaktifkanHost.Click
        If _hostAktif Then
            NonaktifkanHost()
        Else
            AktifkanHost()
        End If
    End Sub

    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        Me.Close()
    End Sub

    Private Sub btn_SalinHostCode_Click(sender As Object, e As RoutedEventArgs) Handles btn_SalinHostCode.Click
        If HostCodeAktif <> "" AndAlso HostCodeAktif <> "------" Then
            Clipboard.SetText(HostCodeAktif)
            TambahRiwayat($"{DateTime.Now:HH:mm} - HostCode disalin ke clipboard")
        End If
    End Sub

#End Region

#Region "Pengaturan Port"

    Private Sub MuatPengaturanPortKeUI()
        txt_PortDiscovery.Text = PortDiscoveryAktif.ToString()
        txt_PortKoneksi.Text = PortKoneksiAktif.ToString()
        txt_RelayServerIP.Text = RelayServerIPAktif
        txt_PortRelay.Text = PortRelayAktif.ToString()
    End Sub

    Private Sub btn_SimpanPengaturanPort_Click(sender As Object, e As RoutedEventArgs) Handles btn_SimpanPengaturanPort.Click
        ' Validasi input
        Dim portDiscovery As Integer
        Dim portKoneksi As Integer
        Dim portRelay As Integer

        If Not Integer.TryParse(txt_PortDiscovery.Text, portDiscovery) OrElse portDiscovery < 1 OrElse portDiscovery > 65535 Then
            MessageBox.Show("Port Discovery harus berupa angka antara 1 - 65535.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning)
            txt_PortDiscovery.Focus()
            Return
        End If

        If Not Integer.TryParse(txt_PortKoneksi.Text, portKoneksi) OrElse portKoneksi < 1 OrElse portKoneksi > 65535 Then
            MessageBox.Show("Port Koneksi harus berupa angka antara 1 - 65535.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning)
            txt_PortKoneksi.Focus()
            Return
        End If

        If Not Integer.TryParse(txt_PortRelay.Text, portRelay) OrElse portRelay < 1 OrElse portRelay > 65535 Then
            MessageBox.Show("Port Relay harus berupa angka antara 1 - 65535.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning)
            txt_PortRelay.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txt_RelayServerIP.Text) Then
            MessageBox.Show("Relay Server IP tidak boleh kosong.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning)
            txt_RelayServerIP.Focus()
            Return
        End If

        ' Simpan ke settings
        SetelPortAktif.PortDiscovery = portDiscovery
        SetelPortAktif.PortKoneksi = portKoneksi
        SetelPortAktif.PortRelay = portRelay
        SetelPortAktif.RelayServerIP = txt_RelayServerIP.Text.Trim()

        If SetelPortAktif.SimpanKeFile() Then
            ' Update tampilan port
            If _modeKoneksi = ModeKoneksi.INTERNET Then
                lbl_Port.Text = PortRelayAktif.ToString()
                lbl_AlamatIP.Text = RelayServerIPAktif
            Else
                lbl_Port.Text = PortKoneksiAktif.ToString()
            End If

            TambahRiwayat($"{DateTime.Now:HH:mm} - Pengaturan port disimpan")
            MessageBox.Show("Pengaturan port berhasil disimpan." & Environment.NewLine &
                           "Restart aplikasi untuk menerapkan perubahan pada listener.", "Sukses",
                           MessageBoxButton.OK, MessageBoxImage.Information)
        Else
            MessageBox.Show("Gagal menyimpan pengaturan port.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub

    Private Sub btn_ResetPengaturanPort_Click(sender As Object, e As RoutedEventArgs) Handles btn_ResetPengaturanPort.Click
        If MessageBox.Show("Reset semua pengaturan port ke nilai default?", "Konfirmasi",
                          MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
            SetelPortAktif.ResetKeDefault()
            If SetelPortAktif.SimpanKeFile() Then
                MuatPengaturanPortKeUI()

                ' Update tampilan port
                If _modeKoneksi = ModeKoneksi.INTERNET Then
                    lbl_Port.Text = PortRelayAktif.ToString()
                    lbl_AlamatIP.Text = RelayServerIPAktif
                Else
                    lbl_Port.Text = PortKoneksiAktif.ToString()
                End If

                TambahRiwayat($"{DateTime.Now:HH:mm} - Pengaturan port di-reset ke default")
                MessageBox.Show("Pengaturan port berhasil di-reset ke default.", "Sukses",
                               MessageBoxButton.OK, MessageBoxImage.Information)
            Else
                MessageBox.Show("Gagal menyimpan pengaturan port.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End If
    End Sub

#End Region

End Class
