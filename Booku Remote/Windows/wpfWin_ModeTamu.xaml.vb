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

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Subscribe ke events
        AddHandler mdl_PenemuanPerangkat.PerangkatDitemukan, AddressOf OnPerangkatDitemukan
        AddHandler mdl_PenemuanPerangkat.ErrorDiscovery, AddressOf OnErrorDiscovery
        AddHandler mdl_KoneksiJaringan.KoneksiBerhasil, AddressOf OnKoneksiBerhasil
        AddHandler mdl_KoneksiJaringan.KoneksiDitolak, AddressOf OnKoneksiDitolak
        AddHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi

        ' Auto scan saat window dibuka
        ScanPerangkatAsync()
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Unsubscribe events
        RemoveHandler mdl_PenemuanPerangkat.PerangkatDitemukan, AddressOf OnPerangkatDitemukan
        RemoveHandler mdl_PenemuanPerangkat.ErrorDiscovery, AddressOf OnErrorDiscovery
        RemoveHandler mdl_KoneksiJaringan.KoneksiBerhasil, AddressOf OnKoneksiBerhasil
        RemoveHandler mdl_KoneksiJaringan.KoneksiDitolak, AddressOf OnKoneksiDitolak
        RemoveHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi

        ' Jangan putuskan koneksi jika sudah terhubung dan sesi aktif
        ' (berarti kita sedang pindah ke viewer)
        If SesiRemoteAktif IsNot Nothing AndAlso SesiRemoteAktif.IsSesiValid() Then
            ' Koneksi tetap aktif untuk viewer
            Return
        End If

        ' Putuskan koneksi jika belum terhubung (user batal)
        If mdl_KoneksiJaringan.Terhubung Then
            mdl_KoneksiJaringan.Putuskan("Window ditutup")
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
        Dim alamatIP = txt_AlamatIPManual.Text.Trim()
        Dim portText = txt_PortManual.Text.Trim()

        If String.IsNullOrEmpty(alamatIP) Then
            MessageBox.Show("Silakan pilih perangkat atau masukkan alamat IP.", "Perhatian",
                           MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If

        Dim port As Integer
        If Not Integer.TryParse(portText, port) Then
            port = PORT_KONEKSI
        End If

        SambungKeHostAsync(alamatIP, port)
    End Sub

    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        Me.Close()
    End Sub

#End Region

End Class
