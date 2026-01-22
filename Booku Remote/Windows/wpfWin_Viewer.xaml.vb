Option Explicit On
Option Strict On

Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Threading
Imports BookuID.Styles

''' <summary>
''' Window Viewer - Menampilkan layar remote dari Host.
''' Fase 2: View-only (tanpa kontrol mouse/keyboard).
''' Fase 2b: Kontrol keyboard dan mouse dari Tamu ke Host.
''' </summary>
Class wpfWin_Viewer

#Region "Variables"

    ''' <summary>Timer untuk update statistik</summary>
    Private _timerStatistik As DispatcherTimer

    ''' <summary>Flag untuk mode fullscreen</summary>
    Private _isFullscreen As Boolean = False

    ''' <summary>State window sebelum fullscreen</summary>
    Private _previousWindowState As WindowState = WindowState.Normal
    Private _previousWindowStyle As WindowStyle = WindowStyle.SingleBorderWindow
    Private _previousResizeMode As ResizeMode = ResizeMode.CanResize

    ' === FASE 2b: Kontrol Keyboard/Mouse ===

    ''' <summary>Flag apakah kontrol aktif (toggle oleh user)</summary>
    Private _kontrolAktif As Boolean = False

    ''' <summary>Flag apakah Host mengizinkan kontrol</summary>
    Private _izinKontrol As Boolean = False

    ''' <summary>Waktu terakhir mouse move dikirim (untuk throttling)</summary>
    Private _lastMouseMoveTime As DateTime = DateTime.MinValue

    ''' <summary>Interval minimum antara mouse move events (ms)</summary>
    Private Const MOUSE_MOVE_THROTTLE_MS As Integer = 30

#End Region

#Region "Public Properties"

    ''' <summary>Nama Host yang tersambung</summary>
    Public Property NamaHost As String = ""

    ''' <summary>Alamat IP Host</summary>
    Public Property AlamatIPHost As String = ""

    ''' <summary>Mode koneksi via Relay Server (True) atau LAN langsung (False)</summary>
    Public Property ModeViaRelay As Boolean = False

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Sizable(Me)
    End Sub

#End Region

#Region "Window Events"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Update info Host
        lbl_NamaHost.Text = $"Host: {NamaHost}"
        If ModeViaRelay Then
            lbl_AlamatIPHost.Text = "(via Relay Server)"
        Else
            lbl_AlamatIPHost.Text = $"({AlamatIPHost})"
        End If

        ' Subscribe ke events berdasarkan mode koneksi
        If ModeViaRelay Then
            ' Mode Internet: Subscribe ke relay events
            AddHandler mdl_KoneksiRelay.FrameDiterimaViaRelay, AddressOf OnFrameDiterimaViaRelay
            AddHandler mdl_KoneksiRelay.ErrorDariRelay, AddressOf OnErrorDariRelay
        Else
            ' Mode LAN: Subscribe ke koneksi jaringan events
            AddHandler mdl_KoneksiJaringan.PaketDiterima, AddressOf OnPaketDiterima
            AddHandler mdl_KoneksiJaringan.KoneksiTerputus, AddressOf OnKoneksiTerputus
            AddHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi
        End If

        ' Mulai timer statistik
        _timerStatistik = New DispatcherTimer()
        _timerStatistik.Interval = TimeSpan.FromMilliseconds(500)
        AddHandler _timerStatistik.Tick, AddressOf OnTimerStatistikTick
        _timerStatistik.Start()

        ' Kirim permintaan streaming ke Host
        KirimPermintaanStreaming()
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Hentikan timer
        _timerStatistik?.Stop()

        ' Unsubscribe events berdasarkan mode koneksi
        If ModeViaRelay Then
            RemoveHandler mdl_KoneksiRelay.FrameDiterimaViaRelay, AddressOf OnFrameDiterimaViaRelay
            RemoveHandler mdl_KoneksiRelay.ErrorDariRelay, AddressOf OnErrorDariRelay
        Else
            RemoveHandler mdl_KoneksiJaringan.PaketDiterima, AddressOf OnPaketDiterima
            RemoveHandler mdl_KoneksiJaringan.KoneksiTerputus, AddressOf OnKoneksiTerputus
            RemoveHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi
        End If

        ' Kirim permintaan hentikan streaming
        KirimHentikanStreaming()
    End Sub

    Private Sub wpfWin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' F11 untuk toggle fullscreen
        If e.Key = Key.F11 Then
            ToggleFullscreen()
            e.Handled = True
            Return
        End If

        ' Escape untuk keluar fullscreen
        If e.Key = Key.Escape AndAlso _isFullscreen Then
            ToggleFullscreen()
            e.Handled = True
            Return
        End If

        ' Ctrl+K untuk toggle kontrol
        If e.Key = Key.K AndAlso Keyboard.Modifiers = ModifierKeys.Control Then
            tgl_Kontrol.IsChecked = Not tgl_Kontrol.IsChecked
            e.Handled = True
            Return
        End If

        ' Forward keyboard events ke Host jika kontrol aktif
        If _kontrolAktif Then
            KirimInputKeyboard(e.Key, True, IsExtendedKey(e.Key))
            e.Handled = True
        End If
    End Sub

    Private Sub wpfWin_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        ' Forward keyboard events ke Host jika kontrol aktif
        If _kontrolAktif Then
            KirimInputKeyboard(e.Key, False, IsExtendedKey(e.Key))
            e.Handled = True
        End If
    End Sub

#End Region

#Region "Streaming Control"

    Private Async Sub KirimPermintaanStreaming()
        Try
            lbl_StatusLoading.Text = "Mengirim permintaan streaming..."

            If ModeViaRelay Then
                ' Mode Internet: Kirim via Relay
                Await mdl_KoneksiRelay.MintaStreamingViaRelayAsync()
            Else
                ' Mode LAN: Kirim langsung
                Dim paket = BuatPaketPermintaanStreaming(KunciSesiAktif)
                Await mdl_KoneksiJaringan.KirimPaketAsync(paket)
            End If
        Catch ex As Exception
            lbl_StatusLoading.Text = $"Error: {ex.Message}"
        End Try
    End Sub

    Private Async Sub KirimHentikanStreaming()
        Try
            If ModeViaRelay Then
                ' Mode Internet: Hentikan via Relay
                If TerhubungKeRelay Then
                    Dim paket = BuatPaketHentikanStreaming(KunciSesiAktif)
                    Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paket)
                End If
            Else
                ' Mode LAN: Hentikan langsung
                If mdl_KoneksiJaringan.Terhubung Then
                    Dim paket = BuatPaketHentikanStreaming(KunciSesiAktif)
                    Await mdl_KoneksiJaringan.KirimPaketAsync(paket)
                End If
            End If
        Catch
            ' Ignore errors saat closing
        End Try
    End Sub

#End Region

#Region "Event Handlers - Koneksi LAN"

    Private Sub OnPaketDiterima(paket As cls_PaketData)
        If paket.TipePaket = TipePaket.FRAME_LAYAR Then
            Dispatcher.Invoke(Sub()
                                  ProsesFrameLayar(paket.Payload)
                              End Sub)
        End If
    End Sub

    Private Sub OnKoneksiTerputus(alasan As String)
        Dispatcher.Invoke(Sub()
                              TampilkanError($"Koneksi terputus: {alasan}")
                          End Sub)
    End Sub

    Private Sub OnErrorKoneksi(pesan As String)
        Dispatcher.Invoke(Sub()
                              TampilkanError($"Error: {pesan}")
                          End Sub)
    End Sub

#End Region

#Region "Event Handlers - Relay"

    ''' <summary>
    ''' Handler untuk frame layar yang diterima via Relay.
    ''' </summary>
    Private Sub OnFrameDiterimaViaRelay(paket As cls_PaketData)
        Dispatcher.Invoke(Sub()
                              ProsesFrameLayar(paket.Payload)
                          End Sub)
    End Sub

    ''' <summary>
    ''' Handler untuk error dari Relay.
    ''' </summary>
    Private Sub OnErrorDariRelay(kodeError As Integer, pesan As String)
        Dispatcher.Invoke(Sub()
                              TampilkanError($"Relay Error ({kodeError}): {pesan}")
                          End Sub)
    End Sub

#End Region

#Region "Frame Processing"

    Private Sub ProsesFrameLayar(payload As String)
        Try
            Dim frame = DeserializeFrameLayar(payload)
            If frame Is Nothing OrElse Not frame.IsValid() Then Return

            ' Konversi ke BitmapImage dan tampilkan
            Dim bitmapImage = frame.DapatkanBitmapImage()
            If bitmapImage IsNot Nothing Then
                img_Layar.Source = bitmapImage

                ' Sembunyikan loading overlay
                If bdr_Loading.Visibility = Visibility.Visible Then
                    bdr_Loading.Visibility = Visibility.Collapsed
                End If

                ' Update statistik di SesiRemote
                If SesiRemoteAktif IsNot Nothing Then
                    SesiRemoteAktif.CatatFrame(frame.NomorFrame, frame.UkuranDataKB())
                    SesiRemoteAktif.CatatLatency(frame.Timestamp)
                End If
            End If

        Catch ex As Exception
            ' Log error tapi jangan ganggu streaming
            System.Diagnostics.Debug.WriteLine($"Error proses frame: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "UI Update"

    Private Sub OnTimerStatistikTick(sender As Object, e As EventArgs)
        If SesiRemoteAktif Is Nothing Then Return

        ' Update statistik
        lbl_FrameCount.Text = SesiRemoteAktif.TotalFrame.ToString("N0")
        lbl_FPS.Text = SesiRemoteAktif.FPSAktual.ToString("0.0")
        lbl_Latency.Text = $"{SesiRemoteAktif.LatencyMs:0} ms"
        lbl_FrameSize.Text = $"{SesiRemoteAktif.UkuranFrameKB:0.0} KB"
        lbl_Durasi.Text = SesiRemoteAktif.DurasiSesiString()

        ' Update status indicator - cek koneksi berdasarkan mode
        Dim terhubung As Boolean = If(ModeViaRelay, TerhubungKeRelay, mdl_KoneksiJaringan.Terhubung)

        If SesiRemoteAktif.IsStreamingAktif() Then
            elp_StatusIndicator.Fill = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50)) ' Green
            lbl_StatusKoneksi.Text = If(ModeViaRelay, "Streaming aktif (via Relay)", "Streaming aktif")
        ElseIf terhubung Then
            elp_StatusIndicator.Fill = New SolidColorBrush(Color.FromRgb(&HFF, &H98, &H0)) ' Orange
            lbl_StatusKoneksi.Text = "Terhubung, menunggu streaming"
        Else
            elp_StatusIndicator.Fill = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36)) ' Red
            lbl_StatusKoneksi.Text = "Terputus"
        End If
    End Sub

    Private Sub TampilkanError(pesan As String)
        bdr_Loading.Visibility = Visibility.Collapsed
        bdr_Error.Visibility = Visibility.Visible
        lbl_Error.Text = pesan

        elp_StatusIndicator.Fill = New SolidColorBrush(Color.FromRgb(&HF4, &H43, &H36))
        lbl_StatusKoneksi.Text = "Terputus"
    End Sub

#End Region

#Region "Fullscreen"

    Private Sub ToggleFullscreen()
        If _isFullscreen Then
            ' Keluar dari fullscreen
            WindowState = _previousWindowState
            WindowStyle = _previousWindowStyle
            ResizeMode = _previousResizeMode
            _isFullscreen = False
            btn_Fullscreen.Content = ChrW(&HE740) ' Expand icon
        Else
            ' Masuk ke fullscreen
            _previousWindowState = WindowState
            _previousWindowStyle = WindowStyle
            _previousResizeMode = ResizeMode

            WindowStyle = WindowStyle.None
            ResizeMode = ResizeMode.NoResize
            WindowState = WindowState.Maximized
            _isFullscreen = True
            btn_Fullscreen.Content = ChrW(&HE73F) ' Contract icon
        End If
    End Sub

#End Region

#Region "Button Click Handlers"

    Private Sub btn_Fullscreen_Click(sender As Object, e As RoutedEventArgs) Handles btn_Fullscreen.Click
        ToggleFullscreen()
    End Sub

    Private Sub btn_Putuskan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Putuskan.Click
        Dim result = MessageBox.Show("Apakah Anda yakin ingin memutuskan koneksi?",
                                    "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If result = MessageBoxResult.Yes Then
            If ModeViaRelay Then
                mdl_KoneksiRelay.TutupKoneksiRelay()
            Else
                mdl_KoneksiJaringan.Putuskan("User memutuskan koneksi")
            End If
            Me.Close()
        End If
    End Sub

    Private Sub btn_Reconnect_Click(sender As Object, e As RoutedEventArgs) Handles btn_Reconnect.Click
        ' Tutup viewer dan kembali ke Mode Tamu untuk reconnect
        Me.Close()
    End Sub

    Private Sub cmb_Skala_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Skala.SelectionChanged
        ' Kirim perubahan skala ke Host (akan diimplementasikan nanti)
        ' Untuk saat ini, skala diatur di sisi Host
    End Sub

#End Region

#Region "Input Control (Fase 2b)"

    ''' <summary>
    ''' Toggle kontrol diaktifkan oleh user.
    ''' </summary>
    Private Sub tgl_Kontrol_Checked(sender As Object, e As RoutedEventArgs) Handles tgl_Kontrol.Checked
        _kontrolAktif = True
        UpdateStatusKontrol()
        img_Layar.Focus()
        img_Layar.Cursor = Cursors.None ' Sembunyikan cursor saat kontrol aktif
    End Sub

    ''' <summary>
    ''' Toggle kontrol dinonaktifkan oleh user.
    ''' </summary>
    Private Sub tgl_Kontrol_Unchecked(sender As Object, e As RoutedEventArgs) Handles tgl_Kontrol.Unchecked
        _kontrolAktif = False
        UpdateStatusKontrol()
        img_Layar.Cursor = Cursors.Arrow ' Kembalikan cursor normal
    End Sub

    ''' <summary>
    ''' Update tampilan status kontrol di status bar dan toggle button.
    ''' </summary>
    Private Sub UpdateStatusKontrol()
        If _kontrolAktif Then
            lbl_StatusKontrol.Text = "Kontrol: ON"
            lbl_StatusKontrol.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50)) ' Green
            tgl_Kontrol.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50))
            tgl_Kontrol.BorderBrush = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50))
        Else
            lbl_StatusKontrol.Text = "Kontrol: OFF"
            lbl_StatusKontrol.Foreground = New SolidColorBrush(Color.FromRgb(&H9E, &H9E, &H9E)) ' Gray
            tgl_Kontrol.Foreground = New SolidColorBrush(Color.FromRgb(&H9E, &H9E, &H9E))
            tgl_Kontrol.BorderBrush = New SolidColorBrush(Color.FromRgb(&HBD, &HBD, &HBD))
        End If
    End Sub

    ''' <summary>
    ''' Aktifkan toggle button jika Host mengizinkan kontrol.
    ''' Dipanggil setelah koneksi established.
    ''' </summary>
    Public Sub AktifkanKontrolJikaDiizinkan(izinKontrol As Boolean)
        _izinKontrol = izinKontrol
        tgl_Kontrol.IsEnabled = izinKontrol
        If Not izinKontrol Then
            tgl_Kontrol.ToolTip = "Host tidak mengizinkan kontrol"
        Else
            tgl_Kontrol.ToolTip = "Aktifkan Kontrol Keyboard/Mouse (Ctrl+K)"
        End If
    End Sub

#End Region

#Region "Mouse Event Handlers (Fase 2b)"

    Private Sub img_Layar_MouseMove(sender As Object, e As MouseEventArgs) Handles img_Layar.MouseMove
        If Not _kontrolAktif Then Return

        ' Throttle mouse move events
        Dim now = DateTime.Now
        If (now - _lastMouseMoveTime).TotalMilliseconds < MOUSE_MOVE_THROTTLE_MS Then Return
        _lastMouseMoveTime = now

        ' Dapatkan posisi normalized
        Dim pos = e.GetPosition(img_Layar)
        Dim normalizedX = NormalizeX(pos.X)
        Dim normalizedY = NormalizeY(pos.Y)

        ' Kirim ke Host
        KirimInputMouseMove(normalizedX, normalizedY)
    End Sub

    Private Sub img_Layar_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles img_Layar.MouseDown
        If Not _kontrolAktif Then Return

        Dim pos = e.GetPosition(img_Layar)
        Dim normalizedX = NormalizeX(pos.X)
        Dim normalizedY = NormalizeY(pos.Y)
        Dim buttonCode = GetButtonCode(e.ChangedButton)

        KirimInputMouseClick(buttonCode, True, normalizedX, normalizedY)
        img_Layar.CaptureMouse()
    End Sub

    Private Sub img_Layar_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles img_Layar.MouseUp
        If Not _kontrolAktif Then Return

        Dim pos = e.GetPosition(img_Layar)
        Dim normalizedX = NormalizeX(pos.X)
        Dim normalizedY = NormalizeY(pos.Y)
        Dim buttonCode = GetButtonCode(e.ChangedButton)

        KirimInputMouseClick(buttonCode, False, normalizedX, normalizedY)
        img_Layar.ReleaseMouseCapture()
    End Sub

    Private Sub img_Layar_MouseWheel(sender As Object, e As MouseWheelEventArgs) Handles img_Layar.MouseWheel
        If Not _kontrolAktif Then Return

        Dim pos = e.GetPosition(img_Layar)
        Dim normalizedX = NormalizeX(pos.X)
        Dim normalizedY = NormalizeY(pos.Y)

        KirimInputMouseWheel(e.Delta, normalizedX, normalizedY)
    End Sub

#End Region

#Region "Input Send Functions (Fase 2b)"

    ''' <summary>
    ''' Kirim input keyboard ke Host.
    ''' </summary>
    Private Async Sub KirimInputKeyboard(key As Key, isKeyDown As Boolean, isExtended As Boolean)
        ' Cek koneksi berdasarkan mode
        Dim terhubung As Boolean = If(ModeViaRelay, TerhubungKeRelay, mdl_KoneksiJaringan.Terhubung)
        If Not terhubung Then Return

        Try
            ' Konversi WPF Key ke Virtual Key Code
            Dim virtualKey = KeyInterop.VirtualKeyFromKey(key)
            If virtualKey = 0 Then Return

            If ModeViaRelay Then
                ' Mode Internet: Kirim via Relay
                Await mdl_KoneksiRelay.KirimInputKeyboardViaRelayAsync(virtualKey, isKeyDown, isExtended, 0)
            Else
                ' Mode LAN: Kirim langsung
                Dim paket = BuatPaketInputKeyboard(virtualKey, isKeyDown, isExtended)
                Await mdl_KoneksiJaringan.KirimPaketAsync(paket)
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error kirim keyboard: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim input mouse move ke Host.
    ''' </summary>
    Private Async Sub KirimInputMouseMove(normalizedX As Double, normalizedY As Double)
        ' Cek koneksi berdasarkan mode
        Dim terhubung As Boolean = If(ModeViaRelay, TerhubungKeRelay, mdl_KoneksiJaringan.Terhubung)
        If Not terhubung Then Return

        Try
            If ModeViaRelay Then
                ' Mode Internet: Kirim via Relay
                Await mdl_KoneksiRelay.KirimInputMouseViaRelayAsync(TipeAksiMouse.PINDAH, normalizedX, normalizedY, 0, False, 0)
            Else
                ' Mode LAN: Kirim langsung
                Dim paket = BuatPaketInputMouseMove(normalizedX, normalizedY)
                Await mdl_KoneksiJaringan.KirimPaketAsync(paket)
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error kirim mouse move: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim input mouse click ke Host.
    ''' </summary>
    Private Async Sub KirimInputMouseClick(button As Integer, isDown As Boolean,
                                            normalizedX As Double, normalizedY As Double)
        ' Cek koneksi berdasarkan mode
        Dim terhubung As Boolean = If(ModeViaRelay, TerhubungKeRelay, mdl_KoneksiJaringan.Terhubung)
        If Not terhubung Then Return

        Try
            If ModeViaRelay Then
                ' Mode Internet: Kirim via Relay
                Await mdl_KoneksiRelay.KirimInputMouseViaRelayAsync(TipeAksiMouse.KLIK, normalizedX, normalizedY, button, isDown, 0)
            Else
                ' Mode LAN: Kirim langsung
                Dim paket = BuatPaketInputMouseClick(button, isDown, normalizedX, normalizedY)
                Await mdl_KoneksiJaringan.KirimPaketAsync(paket)
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error kirim mouse click: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim input mouse wheel ke Host.
    ''' </summary>
    Private Async Sub KirimInputMouseWheel(delta As Integer, normalizedX As Double, normalizedY As Double)
        ' Cek koneksi berdasarkan mode
        Dim terhubung As Boolean = If(ModeViaRelay, TerhubungKeRelay, mdl_KoneksiJaringan.Terhubung)
        If Not terhubung Then Return

        Try
            If ModeViaRelay Then
                ' Mode Internet: Kirim via Relay
                Await mdl_KoneksiRelay.KirimInputMouseViaRelayAsync(TipeAksiMouse.RODA, normalizedX, normalizedY, 0, False, delta)
            Else
                ' Mode LAN: Kirim langsung
                Dim paket = BuatPaketInputMouseWheel(delta, normalizedX, normalizedY)
                Await mdl_KoneksiJaringan.KirimPaketAsync(paket)
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error kirim mouse wheel: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Input Helper Functions (Fase 2b)"

    ''' <summary>
    ''' Normalize koordinat X ke range 0.0-1.0.
    ''' </summary>
    Private Function NormalizeX(x As Double) As Double
        If img_Layar.ActualWidth <= 0 Then Return 0
        Return Math.Max(0, Math.Min(1, x / img_Layar.ActualWidth))
    End Function

    ''' <summary>
    ''' Normalize koordinat Y ke range 0.0-1.0.
    ''' </summary>
    Private Function NormalizeY(y As Double) As Double
        If img_Layar.ActualHeight <= 0 Then Return 0
        Return Math.Max(0, Math.Min(1, y / img_Layar.ActualHeight))
    End Function

    ''' <summary>
    ''' Cek apakah key adalah extended key (arrows, numpad, Insert, Delete, dll).
    ''' </summary>
    Private Function IsExtendedKey(key As Key) As Boolean
        Select Case key
            Case Key.Up, Key.Down, Key.Left, Key.Right,
                 Key.Home, Key.End, Key.PageUp, Key.PageDown,
                 Key.Insert, Key.Delete,
                 Key.NumLock, Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3,
                 Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7,
                 Key.NumPad8, Key.NumPad9, Key.Multiply, Key.Add,
                 Key.Subtract, Key.Divide, Key.Decimal,
                 Key.PrintScreen, Key.Pause
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Konversi WPF MouseButton ke kode button (1=left, 2=right, 3=middle, 4=X1, 5=X2).
    ''' </summary>
    Private Function GetButtonCode(button As MouseButton) As Integer
        Select Case button
            Case MouseButton.Left : Return 1
            Case MouseButton.Right : Return 2
            Case MouseButton.Middle : Return 3
            Case MouseButton.XButton1 : Return 4
            Case MouseButton.XButton2 : Return 5
            Case Else : Return 0
        End Select
    End Function

#End Region

End Class
