Option Explicit On
Option Strict On

Imports System.Windows
Imports System.Windows.Media
Imports BookuID.Styles

''' <summary>
''' Window Mode Tamu - Mencari dan menyambung ke Host.
''' </summary>
Class wpfWin_ModeTamu

#Region "Variables"

    Private _sedangScan As Boolean = False
    Private _sedangMenyambung As Boolean = False
    Private _daftarPerangkat As New List(Of cls_PerangkatLAN)

    ' Mode koneksi
    Private _modeKoneksi As ModeKoneksi = ModeKoneksi.LAN
    Private _hostCodeTerseleksi As String = ""
    Private _namaHostInternet As String = ""
    Private _sedangQueryHost As Boolean = False

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Subscribe ke events LAN
        AddHandler mdl_PenemuanPerangkat.PerangkatDitemukan, AddressOf OnPerangkatDitemukan
        AddHandler mdl_PenemuanPerangkat.ErrorDiscovery, AddressOf OnErrorDiscovery
        AddHandler mdl_KoneksiJaringan.KoneksiBerhasil, AddressOf OnKoneksiBerhasil
        AddHandler mdl_KoneksiJaringan.KoneksiDitolak, AddressOf OnKoneksiDitolak
        AddHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi

        ' Subscribe ke events Relay (Internet)
        AddHandler mdl_KoneksiRelay.HasilQueryHost, AddressOf OnHasilQueryHost
        AddHandler mdl_KoneksiRelay.KoneksiBerhasilViaRelay, AddressOf OnKoneksiBerhasilViaRelay
        AddHandler mdl_KoneksiRelay.KoneksiDitolakViaRelay, AddressOf OnKoneksiDitolakViaRelay
        AddHandler mdl_KoneksiRelay.ErrorDariRelay, AddressOf OnErrorDariRelay

        ' Isi nilai pengaturan port
        MuatPengaturanPortKeUI()

        ' Inisialisasi mode
        _modeKoneksi = ModeKoneksi.LAN
        UpdateVisibilitasMode()

        ' Auto scan saat window dibuka (untuk mode LAN)
        ScanPerangkatAsync()
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Unsubscribe events LAN
        RemoveHandler mdl_PenemuanPerangkat.PerangkatDitemukan, AddressOf OnPerangkatDitemukan
        RemoveHandler mdl_PenemuanPerangkat.ErrorDiscovery, AddressOf OnErrorDiscovery
        RemoveHandler mdl_KoneksiJaringan.KoneksiBerhasil, AddressOf OnKoneksiBerhasil
        RemoveHandler mdl_KoneksiJaringan.KoneksiDitolak, AddressOf OnKoneksiDitolak
        RemoveHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi

        ' Unsubscribe events Relay
        RemoveHandler mdl_KoneksiRelay.HasilQueryHost, AddressOf OnHasilQueryHost
        RemoveHandler mdl_KoneksiRelay.KoneksiBerhasilViaRelay, AddressOf OnKoneksiBerhasilViaRelay
        RemoveHandler mdl_KoneksiRelay.KoneksiDitolakViaRelay, AddressOf OnKoneksiDitolakViaRelay
        RemoveHandler mdl_KoneksiRelay.ErrorDariRelay, AddressOf OnErrorDariRelay

        ' Jangan putuskan koneksi jika sudah terhubung dan sesi aktif
        ' (berarti kita sedang pindah ke viewer)
        If SesiRemoteAktif IsNot Nothing AndAlso SesiRemoteAktif.IsSesiValid() Then
            ' Koneksi tetap aktif untuk viewer
            Return
        End If

        ' Putuskan koneksi jika belum terhubung (user batal)
        If _modeKoneksi = ModeKoneksi.LAN Then
            If mdl_KoneksiJaringan.Terhubung Then
                mdl_KoneksiJaringan.Putuskan("Window ditutup")
            End If
        Else
            If TerhubungKeRelay Then
                mdl_KoneksiRelay.TutupKoneksiRelay()
            End If
        End If
    End Sub

#End Region

#Region "Scan Perangkat"

    Private Async Sub ScanPerangkatAsync()
        If _sedangScan Then Return

        _sedangScan = True
        btn_ScanUlang.IsEnabled = False
        lbl_StatusScan.Text = "Memindai jaringan..."
        _daftarPerangkat.Clear()
        dgr_DaftarPerangkat.ItemsSource = Nothing

        Try
            Dim hasil = Await mdl_PenemuanPerangkat.CariPerangkatAsync()
            _daftarPerangkat = hasil

            dgr_DaftarPerangkat.ItemsSource = _daftarPerangkat
            lbl_TidakAdaPerangkat.Visibility = If(_daftarPerangkat.Count = 0, Visibility.Visible, Visibility.Collapsed)
            lbl_StatusScan.Text = $"Ditemukan {_daftarPerangkat.Count} perangkat"

        Catch ex As Exception
            lbl_StatusScan.Text = $"Error: {ex.Message}"
        Finally
            _sedangScan = False
            btn_ScanUlang.IsEnabled = True
        End Try
    End Sub

#End Region

#Region "Sambung ke Host"

    Private Async Sub SambungKeHostAsync(alamatIP As String, port As Integer)
        If _sedangMenyambung Then Return

        _sedangMenyambung = True
        btn_Sambungkan.IsEnabled = False
        btn_ScanUlang.IsEnabled = False
        bdr_StatusKoneksi.Visibility = Visibility.Visible
        lbl_StatusKoneksi.Text = "Menyambung ke Host..."
        lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HFF, &H98, &H0)) ' Warning

        Try
            Dim berhasil = Await mdl_KoneksiJaringan.SambungKeHostAsync(alamatIP, port)

            If berhasil Then
                lbl_StatusKoneksi.Text = "Terhubung! Menunggu persetujuan..."
                lbl_IconStatus.Text = ChrW(&HE895) ' Waiting icon
                lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HFF, &H98, &H0))
            End If

        Catch ex As Exception
            lbl_StatusKoneksi.Text = $"Gagal: {ex.Message}"
            lbl_IconStatus.Text = ChrW(&HE711) ' Error icon
            lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))
            _sedangMenyambung = False
            btn_Sambungkan.IsEnabled = True
            btn_ScanUlang.IsEnabled = True
        End Try
    End Sub

#End Region

#Region "Event Handlers - Discovery"

    Private Sub OnPerangkatDitemukan(perangkat As cls_PerangkatLAN)
        Dispatcher.Invoke(Sub()
                              ' Cek apakah sudah ada
                              Dim existing = _daftarPerangkat.FirstOrDefault(Function(p) p.AlamatIP = perangkat.AlamatIP)
                              If existing Is Nothing Then
                                  _daftarPerangkat.Add(perangkat)
                                  dgr_DaftarPerangkat.ItemsSource = Nothing
                                  dgr_DaftarPerangkat.ItemsSource = _daftarPerangkat
                                  lbl_TidakAdaPerangkat.Visibility = Visibility.Collapsed
                                  lbl_StatusScan.Text = $"Ditemukan {_daftarPerangkat.Count} perangkat"
                              End If
                          End Sub)
    End Sub

    Private Sub OnErrorDiscovery(pesan As String)
        Dispatcher.Invoke(Sub()
                              lbl_StatusScan.Text = $"Error: {pesan}"
                          End Sub)
    End Sub

#End Region

#Region "Event Handlers - Koneksi"

    Private Sub OnKoneksiBerhasil(kunciSesi As String)
        Dispatcher.Invoke(Sub()
                              lbl_StatusKoneksi.Text = "Terhubung!"
                              lbl_IconStatus.Text = ChrW(&HE73E) ' Checkmark icon
                              lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50))

                              _sedangMenyambung = False
                              btn_Sambungkan.IsEnabled = True
                              btn_ScanUlang.IsEnabled = True

                              ' Ambil info Host dari perangkat terseleksi
                              Dim namaHost = ""
                              Dim alamatIPHost = txt_AlamatIPManual.Text.Trim()
                              Dim selected = TryCast(dgr_DaftarPerangkat.SelectedItem, cls_PerangkatLAN)
                              If selected IsNot Nothing Then
                                  namaHost = selected.NamaPerangkat
                              Else
                                  namaHost = alamatIPHost
                              End If

                              ' Inisialisasi sesi remote
                              SesiRemoteAktif = New cls_SesiRemote(kunciSesi, ModeAplikasi.TAMU, namaHost, alamatIPHost)

                              ' Buka window viewer
                              Dim viewer As New wpfWin_Viewer()
                              viewer.NamaHost = namaHost
                              viewer.AlamatIPHost = alamatIPHost
                              viewer.Show()

                              ' Tutup window Mode Tamu
                              Me.Close()
                          End Sub)
    End Sub

    Private Sub OnKoneksiDitolak(pesan As String)
        Dispatcher.Invoke(Sub()
                              lbl_StatusKoneksi.Text = $"Ditolak: {pesan}"
                              lbl_IconStatus.Text = ChrW(&HE711) ' Error icon
                              lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))

                              _sedangMenyambung = False
                              btn_Sambungkan.IsEnabled = True
                              btn_ScanUlang.IsEnabled = True
                          End Sub)
    End Sub

    Private Sub OnErrorKoneksi(pesan As String)
        Dispatcher.Invoke(Sub()
                              lbl_StatusKoneksi.Text = $"Error: {pesan}"
                              lbl_IconStatus.Text = ChrW(&HE711) ' Error icon
                              lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))

                              _sedangMenyambung = False
                              btn_Sambungkan.IsEnabled = True
                              btn_ScanUlang.IsEnabled = True
                          End Sub)
    End Sub

#End Region

#Region "DataGrid Events"

    Private Sub dgr_DaftarPerangkat_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dgr_DaftarPerangkat.SelectionChanged
        Dim selected = TryCast(dgr_DaftarPerangkat.SelectedItem, cls_PerangkatLAN)
        btn_Sambungkan.IsEnabled = (selected IsNot Nothing AndAlso selected.Status = StatusPerangkat.TERSEDIA)

        If selected IsNot Nothing Then
            txt_AlamatIPManual.Text = selected.AlamatIP
            txt_PortManual.Text = selected.PortTCP.ToString()
        End If
    End Sub

#End Region

#Region "Button Click Handlers"

    Private Sub btn_ScanUlang_Click(sender As Object, e As RoutedEventArgs) Handles btn_ScanUlang.Click
        ScanPerangkatAsync()
    End Sub

    Private Sub btn_Sambungkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sambungkan.Click
        ' Cek mode koneksi
        If _modeKoneksi = ModeKoneksi.INTERNET Then
            ' Mode Internet - sambung via Relay
            SambungViaRelayAsync()
            Return
        End If

        ' Mode LAN - sambung langsung
        Dim alamatIP = txt_AlamatIPManual.Text.Trim()
        Dim portText = txt_PortManual.Text.Trim()

        If String.IsNullOrEmpty(alamatIP) Then
            MessageBox.Show("Silakan pilih perangkat atau masukkan alamat IP.", "Perhatian",
                           MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If

        Dim port As Integer
        If Not Integer.TryParse(portText, port) Then
            port = PortKoneksiAktif
        End If

        SambungKeHostAsync(alamatIP, port)
    End Sub

    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        Me.Close()
    End Sub

    Private Sub btn_CariHost_Click(sender As Object, e As RoutedEventArgs) Handles btn_CariHost.Click
        CariHostAsync()
    End Sub

#End Region

#Region "Mode Switching"

    Private Sub rdo_ModeLAN_Checked(sender As Object, e As RoutedEventArgs) Handles rdo_ModeLAN.Checked
        _modeKoneksi = ModeKoneksi.LAN
        UpdateVisibilitasMode()
    End Sub

    Private Sub rdo_ModeInternet_Checked(sender As Object, e As RoutedEventArgs) Handles rdo_ModeInternet.Checked
        _modeKoneksi = ModeKoneksi.INTERNET
        UpdateVisibilitasMode()
    End Sub

    Private Sub UpdateVisibilitasMode()
        If pnl_InputHostCode Is Nothing Then Return ' Belum loaded

        If _modeKoneksi = ModeKoneksi.LAN Then
            ' Mode LAN - tampilkan scan dan DataGrid
            pnl_InputHostCode.Visibility = Visibility.Collapsed
            btn_ScanUlang.Visibility = Visibility.Visible
            lbl_StatusScan.Visibility = Visibility.Visible
        Else
            ' Mode Internet - tampilkan input HostCode
            pnl_InputHostCode.Visibility = Visibility.Visible
            btn_ScanUlang.Visibility = Visibility.Collapsed
            lbl_StatusScan.Visibility = Visibility.Collapsed

            ' Reset status
            lbl_HasilCariHost.Text = ""
            txt_HostCode.Text = ""
            _hostCodeTerseleksi = ""
            _namaHostInternet = ""
            btn_Sambungkan.IsEnabled = False
        End If
    End Sub

#End Region

#Region "Cari Host via Relay"

    Private Async Sub CariHostAsync()
        Dim hostCode = txt_HostCode.Text.Trim().ToUpper()
        If hostCode.Length <> 6 Then
            lbl_HasilCariHost.Text = "Kode Host harus 6 karakter."
            lbl_HasilCariHost.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))
            Return
        End If

        If _sedangQueryHost Then Return
        _sedangQueryHost = True

        btn_CariHost.IsEnabled = False
        lbl_HasilCariHost.Text = "Mencari Host..."
        lbl_HasilCariHost.Foreground = New SolidColorBrush(Color.FromRgb(&H75, &H75, &H75))
        _hostCodeTerseleksi = hostCode

        Try
            Dim berhasil = Await mdl_KoneksiRelay.QueryHostAsync(hostCode)
            If Not berhasil Then
                lbl_HasilCariHost.Text = "Gagal menghubungi relay server."
                lbl_HasilCariHost.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))
                _sedangQueryHost = False
                btn_CariHost.IsEnabled = True
            End If
            ' Hasil akan diterima via event OnHasilQueryHost
        Catch ex As Exception
            lbl_HasilCariHost.Text = $"Error: {ex.Message}"
            lbl_HasilCariHost.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))
            _sedangQueryHost = False
            btn_CariHost.IsEnabled = True
        End Try
    End Sub

#End Region

#Region "Sambung via Relay"

    Private Async Sub SambungViaRelayAsync()
        If _sedangMenyambung Then Return
        If String.IsNullOrEmpty(_hostCodeTerseleksi) Then Return

        _sedangMenyambung = True
        btn_Sambungkan.IsEnabled = False
        bdr_StatusKoneksi.Visibility = Visibility.Visible
        lbl_StatusKoneksi.Text = "Meminta koneksi ke Host..."
        lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HFF, &H98, &H0)) ' Warning

        Try
            Dim berhasil = Await mdl_KoneksiRelay.MintaKoneksiViaRelayAsync(_hostCodeTerseleksi)

            If berhasil Then
                lbl_StatusKoneksi.Text = "Menunggu persetujuan dari Host..."
                lbl_IconStatus.Text = ChrW(&HE895) ' Waiting icon
            Else
                lbl_StatusKoneksi.Text = "Gagal mengirim permintaan koneksi."
                lbl_IconStatus.Text = ChrW(&HE711) ' Error icon
                lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))
                _sedangMenyambung = False
                btn_Sambungkan.IsEnabled = True
            End If
            ' Hasil akan diterima via event OnKoneksiBerhasilViaRelay atau OnKoneksiDitolakViaRelay

        Catch ex As Exception
            lbl_StatusKoneksi.Text = $"Error: {ex.Message}"
            lbl_IconStatus.Text = ChrW(&HE711) ' Error icon
            lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))
            _sedangMenyambung = False
            btn_Sambungkan.IsEnabled = True
        End Try
    End Sub

#End Region

#Region "Event Handlers - Relay"

    Private Sub OnHasilQueryHost(found As Boolean, namaHost As String, requiresPassword As Boolean, pesan As String)
        Dispatcher.Invoke(Sub()
                              _sedangQueryHost = False
                              btn_CariHost.IsEnabled = True

                              If found Then
                                  _namaHostInternet = namaHost
                                  lbl_HasilCariHost.Text = $"Host ditemukan: {namaHost}"
                                  lbl_HasilCariHost.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50)) ' Green
                                  btn_Sambungkan.IsEnabled = True
                              Else
                                  _namaHostInternet = ""
                                  lbl_HasilCariHost.Text = If(String.IsNullOrEmpty(pesan), "Host tidak ditemukan atau offline.", pesan)
                                  lbl_HasilCariHost.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36)) ' Red
                                  btn_Sambungkan.IsEnabled = False
                              End If
                          End Sub)
    End Sub

    Private Sub OnKoneksiBerhasilViaRelay(kunciSesi As String, izinKontrol As Boolean)
        Dispatcher.Invoke(Sub()
                              lbl_StatusKoneksi.Text = "Terhubung!"
                              lbl_IconStatus.Text = ChrW(&HE73E) ' Checkmark icon
                              lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50))

                              _sedangMenyambung = False

                              ' Inisialisasi sesi remote
                              SesiRemoteAktif = New cls_SesiRemote(kunciSesi, ModeAplikasi.TAMU, _namaHostInternet, "via-relay")
                              SesiRemoteAktif.IzinKontrol = izinKontrol

                              ' Buka window viewer
                              Dim viewer As New wpfWin_Viewer()
                              viewer.NamaHost = _namaHostInternet
                              viewer.AlamatIPHost = $"Relay ({_hostCodeTerseleksi})"
                              viewer.ModeViaRelay = True
                              viewer.Show()

                              ' Tutup window Mode Tamu
                              Me.Close()
                          End Sub)
    End Sub

    Private Sub OnKoneksiDitolakViaRelay(pesan As String)
        Dispatcher.Invoke(Sub()
                              lbl_StatusKoneksi.Text = $"Ditolak: {pesan}"
                              lbl_IconStatus.Text = ChrW(&HE711) ' Error icon
                              lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))

                              _sedangMenyambung = False
                              btn_Sambungkan.IsEnabled = True
                          End Sub)
    End Sub

    Private Sub OnErrorDariRelay(kodeError As Integer, pesan As String)
        Dispatcher.Invoke(Sub()
                              _sedangQueryHost = False
                              _sedangMenyambung = False
                              btn_CariHost.IsEnabled = True
                              btn_Sambungkan.IsEnabled = False

                              lbl_HasilCariHost.Text = $"Error ({kodeError}): {pesan}"
                              lbl_HasilCariHost.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))

                              If bdr_StatusKoneksi.Visibility = Visibility.Visible Then
                                  lbl_StatusKoneksi.Text = $"Error: {pesan}"
                                  lbl_IconStatus.Text = ChrW(&HE711)
                                  lbl_IconStatus.Foreground = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))
                              End If
                          End Sub)
    End Sub

#End Region

#Region "Pengaturan Port"

    Private Sub MuatPengaturanPortKeUI()
        txt_PortDiscovery.Text = PortDiscoveryAktif.ToString()
        txt_PortKoneksi.Text = PortKoneksiAktif.ToString()
        txt_PortUdpVideo.Text = PortUdpVideoAktif.ToString()
        txt_RelayServerIP.Text = RelayServerIPAktif
        txt_PortRelay.Text = PortRelayAktif.ToString()
    End Sub

    Private Sub btn_SimpanPengaturan_Click(sender As Object, e As RoutedEventArgs) Handles btn_SimpanPengaturan.Click
        ' Validasi input
        Dim portDiscovery As Integer
        Dim portKoneksi As Integer
        Dim portUdpVideo As Integer
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

        If Not Integer.TryParse(txt_PortUdpVideo.Text, portUdpVideo) OrElse portUdpVideo < 1 OrElse portUdpVideo > 65535 Then
            MessageBox.Show("Port Video (UDP) harus berupa angka antara 1 - 65535.", "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning)
            txt_PortUdpVideo.Focus()
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
        SetelPortAktif.PortUdpVideo = portUdpVideo
        SetelPortAktif.PortRelay = portRelay
        SetelPortAktif.RelayServerIP = txt_RelayServerIP.Text.Trim()

        If SetelPortAktif.SimpanKeFile() Then
            MessageBox.Show("Pengaturan berhasil disimpan." & Environment.NewLine &
                           "Restart aplikasi untuk menerapkan perubahan.", "Sukses",
                           MessageBoxButton.OK, MessageBoxImage.Information)
        Else
            MessageBox.Show("Gagal menyimpan pengaturan.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub

    Private Sub btn_ResetPengaturan_Click(sender As Object, e As RoutedEventArgs) Handles btn_ResetPengaturan.Click
        If MessageBox.Show("Reset semua pengaturan port ke nilai default?", "Konfirmasi",
                          MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
            SetelPortAktif.ResetKeDefault()
            If SetelPortAktif.SimpanKeFile() Then
                MuatPengaturanPortKeUI()
                MessageBox.Show("Pengaturan berhasil di-reset ke default.", "Sukses",
                               MessageBoxButton.OK, MessageBoxImage.Information)
            Else
                MessageBox.Show("Gagal menyimpan pengaturan.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End If
    End Sub

#End Region

End Class
