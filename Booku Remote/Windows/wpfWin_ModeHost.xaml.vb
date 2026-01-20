Option Explicit On
Option Strict On

Imports System.Net.Sockets
Imports System.Windows
Imports System.Windows.Media
Imports BookuID.Styles

''' <summary>
''' Window Mode Host - Menunggu dan menerima koneksi dari Tamu.
''' </summary>
Class wpfWin_ModeHost

#Region "Variables"

    Private _riwayatKoneksi As New List(Of String)
    Private _hostAktif As Boolean = False

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
        lbl_Port.Text = PORT_KONEKSI.ToString()

        ' Subscribe ke events
        AddHandler mdl_KoneksiJaringan.PermintaanKoneksiMasuk, AddressOf OnPermintaanKoneksiMasuk
        AddHandler mdl_KoneksiJaringan.KoneksiBerhasil, AddressOf OnKoneksiBerhasil
        AddHandler mdl_KoneksiJaringan.KoneksiTerputus, AddressOf OnKoneksiTerputus
        AddHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi

        ' Aktifkan Host secara otomatis
        AktifkanHost()
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Unsubscribe events
        RemoveHandler mdl_KoneksiJaringan.PermintaanKoneksiMasuk, AddressOf OnPermintaanKoneksiMasuk
        RemoveHandler mdl_KoneksiJaringan.KoneksiBerhasil, AddressOf OnKoneksiBerhasil
        RemoveHandler mdl_KoneksiJaringan.KoneksiTerputus, AddressOf OnKoneksiTerputus
        RemoveHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi

        ' Nonaktifkan Host
        NonaktifkanHost()
    End Sub

#End Region

#Region "Host Control"

    Private Sub AktifkanHost()
        If _hostAktif Then Return

        Try
            ' Mulai discovery listener
            mdl_PenemuanPerangkat.MulaiMendengarkanDiscovery()

            ' Mulai TCP server
            mdl_KoneksiJaringan.MulaiServer()

            _hostAktif = True
            UpdateStatusUI(True)
            TambahRiwayat($"{DateTime.Now:HH:mm} - Host diaktifkan")

        Catch ex As Exception
            MessageBox.Show($"Gagal mengaktifkan Host: {ex.Message}", "Error",
                           MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub NonaktifkanHost()
        If Not _hostAktif Then Return

        Try
            mdl_PenemuanPerangkat.HentikanDiscovery()
            mdl_KoneksiJaringan.HentikanServer()

            _hostAktif = False
            UpdateStatusUI(False)
            TambahRiwayat($"{DateTime.Now:HH:mm} - Host dinonaktifkan")

        Catch ex As Exception
            MessageBox.Show($"Gagal menonaktifkan Host: {ex.Message}", "Error",
                           MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub UpdateStatusUI(aktif As Boolean)
        If aktif Then
            led_Status.Fill = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50)) ' Green
            lbl_Status.Text = "AKTIF"
            lbl_Status.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50))
            btn_NonaktifkanHost.Content = "Nonaktifkan Host"
        Else
            led_Status.Fill = New SolidColorBrush(Color.FromRgb(&H9E, &H9E, &H9E)) ' Gray
            lbl_Status.Text = "TIDAK AKTIF"
            lbl_Status.Foreground = New SolidColorBrush(Color.FromRgb(&H9E, &H9E, &H9E))
            btn_NonaktifkanHost.Content = "Aktifkan Host"
        End If
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

#End Region

End Class
