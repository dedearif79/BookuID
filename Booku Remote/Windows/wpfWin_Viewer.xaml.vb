Option Explicit On
Option Strict On

Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
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

    ' === FASE 3a: Clipboard Sync ===

    ''' <summary>Flag apakah clipboard sync aktif (toggle oleh user)</summary>
    Private _clipboardAktif As Boolean = False

    ''' <summary>Flag apakah Host mengizinkan clipboard sync</summary>
    Private _izinClipboard As Boolean = False

    ' === FASE 3b: Transfer Berkas ===

    ''' <summary>Flag apakah Host mengizinkan transfer berkas</summary>
    Private _izinTransferBerkas As Boolean = False

    ''' <summary>Waktu terakhir mouse move dikirim (untuk throttling)</summary>
    Private _lastMouseMoveTime As DateTime = DateTime.MinValue

    ''' <summary>Interval minimum antara mouse move events (ms)</summary>
    Private Const MOUSE_MOVE_THROTTLE_MS As Integer = 30

    ' === UDP Streaming ===

    ''' <summary>Flag apakah menggunakan UDP untuk menerima frame</summary>
    Private _useUdpReceiver As Boolean = False

    ''' <summary>Flag untuk double buffering (mengurangi flicker)</summary>
    Private _useBufferA As Boolean = True

    ' === H.264 Codec ===

    ''' <summary>WriteableBitmap untuk render BGRA frame dari H.264 decoder</summary>
    Private _bgraWriteableBitmap As WriteableBitmap = Nothing

    ''' <summary>Resolusi terakhir dari H.264 decoder</summary>
    Private _lastBgraWidth As Integer = 0
    Private _lastBgraHeight As Integer = 0

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

        ' Subscribe ke UDP streaming events (baik LAN maupun Internet)
        AddHandler mdl_UdpStreaming.FrameUdpDiterima, AddressOf OnFrameUdpDiterima
        AddHandler mdl_UdpStreaming.StatistikUdp, AddressOf OnStatistikUdp
        AddHandler mdl_UdpStreaming.FrameBgraDiterima, AddressOf OnFrameBgraDiterima

        ' Subscribe ke events berdasarkan mode koneksi
        If ModeViaRelay Then
            ' Mode Internet: Subscribe ke relay events untuk TCP fallback dan error handling
            AddHandler mdl_KoneksiRelay.FrameDiterimaViaRelay, AddressOf OnFrameDiterimaViaRelay
            AddHandler mdl_KoneksiRelay.ErrorDariRelay, AddressOf OnErrorDariRelay
            AddHandler mdl_KoneksiRelay.PaketDariRelay, AddressOf OnPaketDariRelay
        Else
            ' Mode LAN: Subscribe ke TCP events untuk fallback dan kontrol
            AddHandler mdl_KoneksiJaringan.PaketDiterima, AddressOf OnPaketDiterima
            AddHandler mdl_KoneksiJaringan.KoneksiTerputus, AddressOf OnKoneksiTerputus
            AddHandler mdl_KoneksiJaringan.ErrorKoneksi, AddressOf OnErrorKoneksi
        End If

        ' Mulai UDP receiver untuk menerima frame dari Host (baik LAN maupun Internet)
        MulaiUdpReceiver()

        ' Mulai timer statistik
        _timerStatistik = New DispatcherTimer()
        _timerStatistik.Interval = TimeSpan.FromMilliseconds(500)
        AddHandler _timerStatistik.Tick, AddressOf OnTimerStatistikTick
        _timerStatistik.Start()

        ' Kirim permintaan streaming ke Host (via TCP)
        KirimPermintaanStreaming()
    End Sub

    ''' <summary>
    ''' Mulai UDP receiver untuk menerima frame video dari Host.
    ''' Untuk mode Internet, juga mengirim registration packet ke relay.
    ''' </summary>
    Private Async Sub MulaiUdpReceiver()
        Try
            ' PENTING: SessionId untuk UDP routing:
            '   - Mode LAN: hash dari KunciSesiAktif
            '   - Mode Internet: hash dari IdSesiRelay (sama dengan yang digunakan relay server)
            Dim sessionId As Integer
            If ModeViaRelay Then
                sessionId = mdl_UdpStreaming.GenerateSessionId(IdSesiRelay)
                WriteLog($"[UDP-TAMU] Using IdSesiRelay for SessionId: {IdSesiRelay} -> {sessionId}")
            Else
                sessionId = mdl_UdpStreaming.GenerateSessionId(KunciSesiAktif)
            End If

            ' Mulai UDP receiver
            Await mdl_UdpStreaming.MulaiUdpReceiverAsync(PortUdpVideoAktif, sessionId)
            _useUdpReceiver = True

            WriteLog($"[UDP-TAMU] UDP receiver dimulai, port={PortUdpVideoAktif}, sessionId={sessionId}")

            ' Untuk mode Internet: Kirim registration packet ke relay
            ' Ini memberitahu relay IP:Port Tamu agar bisa forward paket dari Host
            If ModeViaRelay Then
                Await Task.Delay(100) ' Beri waktu receiver siap
                Dim regResult = Await mdl_UdpStreaming.KirimRegistrasiKeRelayAsync(RelayServerIPAktif, PortUdpVideoAktif, sessionId)
                If regResult Then
                    WriteLog($"[UDP-TAMU] Registration ke relay berhasil")
                    ' Mulai periodic registration
                    MulaiPeriodicRegistration()
                Else
                    WriteLog($"[UDP-TAMU] Registration ke relay gagal!")
                End If
            End If
        Catch ex As Exception
            WriteLog($"[UDP-TAMU] Gagal mulai UDP receiver: {ex.Message}")
            _useUdpReceiver = False
        End Try
    End Sub

    ''' <summary>Timer untuk periodic registration ke relay</summary>
    Private _timerRegistration As DispatcherTimer

    ''' <summary>
    ''' Mulai timer untuk kirim registration secara periodik ke relay.
    ''' Ini memastikan relay selalu tahu endpoint Tamu.
    ''' </summary>
    Private Sub MulaiPeriodicRegistration()
        If Not ModeViaRelay Then Return

        _timerRegistration = New DispatcherTimer()
        _timerRegistration.Interval = TimeSpan.FromSeconds(5) ' Kirim setiap 5 detik
        AddHandler _timerRegistration.Tick, AddressOf OnTimerRegistrationTick
        _timerRegistration.Start()
    End Sub

    Private Async Sub OnTimerRegistrationTick(sender As Object, e As EventArgs)
        If ModeViaRelay AndAlso _useUdpReceiver Then
            Await mdl_UdpStreaming.KirimRegistrasiPeriodikAsync()
        End If
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Hentikan timer statistik
        _timerStatistik?.Stop()

        ' Hentikan timer registration (mode Internet)
        If _timerRegistration IsNot Nothing Then
            _timerRegistration.Stop()
            RemoveHandler _timerRegistration.Tick, AddressOf OnTimerRegistrationTick
            _timerRegistration = Nothing
        End If

        ' Hentikan clipboard sync (Fase 3a)
        If _clipboardAktif Then
            mdl_Clipboard.HentikanClipboardMonitoring()
            mdl_Clipboard.ResetClipboardState()
        End If

        ' Unsubscribe UDP events (baik LAN maupun Internet)
        RemoveHandler mdl_UdpStreaming.FrameUdpDiterima, AddressOf OnFrameUdpDiterima
        RemoveHandler mdl_UdpStreaming.StatistikUdp, AddressOf OnStatistikUdp
        RemoveHandler mdl_UdpStreaming.FrameBgraDiterima, AddressOf OnFrameBgraDiterima

        ' Hentikan UDP receiver
        mdl_UdpStreaming.HentikanUdpStreaming()

        ' Unsubscribe events berdasarkan mode koneksi
        If ModeViaRelay Then
            RemoveHandler mdl_KoneksiRelay.FrameDiterimaViaRelay, AddressOf OnFrameDiterimaViaRelay
            RemoveHandler mdl_KoneksiRelay.ErrorDariRelay, AddressOf OnErrorDariRelay
            RemoveHandler mdl_KoneksiRelay.PaketDariRelay, AddressOf OnPaketDariRelay
        Else
            ' Unsubscribe TCP events
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
        Select Case paket.TipePaket
            Case TipePaket.FRAME_LAYAR
                Dispatcher.Invoke(Sub()
                                      ProsesFrameLayar(paket.Payload)
                                  End Sub)

            Case TipePaket.CLIPBOARD_DATA
                ' Clipboard data dari Host (Fase 3a)
                If _clipboardAktif Then
                    Dim payload = DeserializeClipboard(paket.Payload)
                    If payload IsNot Nothing Then
                        WriteLog($"[CLIPBOARD] Diterima dari Host: {payload.TipeData}")
                        mdl_Clipboard.TerimaClipboardDariRemote(payload)
                    End If
                End If
        End Select
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

    ''' <summary>
    ''' Handler untuk paket umum dari Relay (termasuk CLIPBOARD_DATA dan Transfer Berkas).
    ''' </summary>
    Private Sub OnPaketDariRelay(paket As cls_PaketData)
        Select Case paket.TipePaket
            Case TipePaket.CLIPBOARD_DATA
                ' Clipboard data dari Host via Relay (Fase 3a)
                If _clipboardAktif Then
                    Dim payload = DeserializeClipboard(paket.Payload)
                    If payload IsNot Nothing Then
                        WriteLog($"[CLIPBOARD-RELAY] Diterima dari Host: {payload.TipeData}")
                        mdl_Clipboard.TerimaClipboardDariRemote(payload)
                    End If
                End If

            ' === FASE 3b: Handle Transfer Berkas via Relay ===
            Case TipePaket.RESPON_TRANSFER,
                 TipePaket.DATA_BERKAS,
                 TipePaket.KONFIRMASI_CHUNK,
                 TipePaket.KONFIRMASI_BERKAS,
                 TipePaket.BATAL_TRANSFER,
                 TipePaket.RESPON_DAFTAR_FOLDER
                ' Forward ke handler transfer di mdl_KoneksiJaringan
                WriteLog($"[TRANSFER-RELAY] Paket diterima: {paket.TipePaket}")
                mdl_KoneksiJaringan.ProsesPaketMasukViaRelay(paket)
        End Select
    End Sub

#End Region

#Region "Event Handlers - UDP"

    ''' <summary>
    ''' Handler untuk frame yang diterima via UDP (sudah reassembled).
    ''' </summary>
    Private Sub OnFrameUdpDiterima(frameData As Byte(), frameId As Integer, timestampMs As Integer)
        Dispatcher.Invoke(Sub()
                              ProsesFrameUdp(frameData, frameId, timestampMs)
                          End Sub)
    End Sub

    ''' <summary>
    ''' Handler untuk statistik UDP (packets received, dropped, fps).
    ''' </summary>
    Private Sub OnStatistikUdp(packetsReceived As Integer, packetsDropped As Integer, fps As Double)
        ' Update statistik di UI jika perlu
        WriteLog($"[UDP] Packets: {packetsReceived}, Dropped: {packetsDropped}, FPS: {fps:F1}")
    End Sub

    ''' <summary>
    ''' Handler untuk frame BGRA yang sudah di-decode dari H.264.
    ''' </summary>
    Private _bgraFrameCount As Integer = 0
    Private Sub OnFrameBgraDiterima(bgraData As Byte(), width As Integer, height As Integer)
        _bgraFrameCount += 1
        If _bgraFrameCount Mod 10 = 1 Then
            WriteLog($"[VIEWER] OnFrameBgraDiterima #{_bgraFrameCount}: {width}x{height}, {bgraData.Length} bytes")
        End If
        Dispatcher.Invoke(Sub()
                              ProsesFrameBgra(bgraData, width, height)
                          End Sub)
    End Sub

#End Region

#Region "Frame Processing"

    ''' <summary>
    ''' Proses frame BGRA yang sudah di-decode dari H.264.
    ''' Data BGRA raw langsung di-render ke WriteableBitmap.
    ''' </summary>
    Private Sub ProsesFrameBgra(bgraData As Byte(), width As Integer, height As Integer)
        Try
            If bgraData Is Nothing OrElse bgraData.Length = 0 Then Return

            ' Validasi ukuran data (4 bytes per pixel: BGRA)
            Dim expectedSize = width * height * 4
            If bgraData.Length <> expectedSize Then
                WriteLog($"[H264] Invalid BGRA size: {bgraData.Length} (expected {expectedSize})")
                Return
            End If

            ' LOG DIAGNOSTIC: Tampilkan pixel di berbagai posisi untuk debug
            If _bgraFrameCount <= 3 Then
                Dim firstBytes = String.Join(",", bgraData.Take(16).Select(Function(b) b.ToString("X2")))
                WriteLog($"[H264-DIAG] Frame #{_bgraFrameCount} first 16 bytes: {firstBytes}")

                ' Pixel di berbagai posisi (BGRA = 4 bytes per pixel)
                Dim stride = width * 4

                ' Pixel [0,0] - top-left
                WriteLog($"[H264-DIAG] Pixel[0,0]: B={bgraData(0)}, G={bgraData(1)}, R={bgraData(2)}, A={bgraData(3)}")

                ' Pixel di tengah gambar
                Dim midY = height \ 2
                Dim midX = width \ 2
                Dim midOffset = (midY * stride) + (midX * 4)
                If midOffset + 3 < bgraData.Length Then
                    WriteLog($"[H264-DIAG] Pixel[{midX},{midY}] (center): B={bgraData(midOffset)}, G={bgraData(midOffset + 1)}, R={bgraData(midOffset + 2)}, A={bgraData(midOffset + 3)}")
                End If

                ' Pixel di bottom-right (terakhir)
                Dim lastOffset = bgraData.Length - 4
                WriteLog($"[H264-DIAG] Pixel[last]: B={bgraData(lastOffset)}, G={bgraData(lastOffset + 1)}, R={bgraData(lastOffset + 2)}, A={bgraData(lastOffset + 3)}")

                ' Hitung berapa pixel yang BUKAN hitam
                Dim nonBlackCount = 0
                For i As Integer = 0 To bgraData.Length - 4 Step 4
                    If bgraData(i) <> 0 OrElse bgraData(i + 1) <> 0 OrElse bgraData(i + 2) <> 0 Then
                        nonBlackCount += 1
                    End If
                Next
                WriteLog($"[H264-DIAG] Non-black pixels: {nonBlackCount} / {width * height} ({100.0 * nonBlackCount / (width * height):F2}%)")
            End If

            ' Gunakan data langsung (FFmpeg rawvideo sudah dengan orientasi yang benar)
            Dim sourceStride = width * 4

            ' Buat atau reuse WriteableBitmap jika resolusi berubah
            If _bgraWriteableBitmap Is Nothing OrElse
               _lastBgraWidth <> width OrElse
               _lastBgraHeight <> height Then

                _bgraWriteableBitmap = New WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, Nothing)
                _lastBgraWidth = width
                _lastBgraHeight = height
                img_Layar.Source = _bgraWriteableBitmap
                WriteLog($"[H264] New WriteableBitmap: {width}x{height}, BackBufferStride={_bgraWriteableBitmap.BackBufferStride}")
            End If

            ' Update pixels di WriteableBitmap
            _bgraWriteableBitmap.Lock()
            Try
                Dim backBuffer = _bgraWriteableBitmap.BackBuffer
                Dim destStride = _bgraWriteableBitmap.BackBufferStride

                ' PENTING: Handle stride mismatch
                ' FFmpeg output BGRA dengan stride = width * 4
                ' WriteableBitmap mungkin memiliki padding bytes di akhir setiap row
                If sourceStride = destStride Then
                    ' Fast path: stride cocok, direct copy
                    System.Runtime.InteropServices.Marshal.Copy(bgraData, 0, backBuffer, bgraData.Length)
                Else
                    ' Slow path: copy row-by-row untuk handle stride yang berbeda
                    For y As Integer = 0 To height - 1
                        Dim srcOffset = y * sourceStride
                        Dim dstOffset = y * destStride
                        Dim destPtr = IntPtr.Add(backBuffer, dstOffset)
                        System.Runtime.InteropServices.Marshal.Copy(bgraData, srcOffset, destPtr, sourceStride)
                    Next
                End If

                ' Mark seluruh bitmap sebagai dirty
                _bgraWriteableBitmap.AddDirtyRect(New Int32Rect(0, 0, width, height))
            Finally
                _bgraWriteableBitmap.Unlock()
            End Try

            ' Sembunyikan loading overlay
            If bdr_Loading.Visibility = Visibility.Visible Then
                bdr_Loading.Visibility = Visibility.Collapsed
                WriteLog($"[VIEWER] First frame rendered, loading overlay hidden")
            End If

            ' Update statistik di SesiRemote
            If SesiRemoteAktif IsNot Nothing Then
                SesiRemoteAktif.CatatFrame(0, bgraData.Length / 1024.0)
            End If

        Catch ex As Exception
            WriteLog($"[VIEWER] ERROR proses BGRA frame: {ex.Message}")
            WriteLog($"[VIEWER] Stack: {ex.StackTrace}")
        End Try
    End Sub

    ''' <summary>
    ''' Proses frame yang diterima via UDP (raw JPEG bytes).
    ''' </summary>
    Private Sub ProsesFrameUdp(jpegData As Byte(), frameId As Integer, timestampMs As Integer)
        Try
            If jpegData Is Nothing OrElse jpegData.Length = 0 Then Return

            ' Konversi JPEG bytes ke BitmapImage
            Dim bitmapImage = JpegBytesToBitmapImage(jpegData)
            If bitmapImage IsNot Nothing Then
                img_Layar.Source = bitmapImage

                ' Sembunyikan loading overlay
                If bdr_Loading.Visibility = Visibility.Visible Then
                    bdr_Loading.Visibility = Visibility.Collapsed
                End If

                ' Update statistik di SesiRemote
                If SesiRemoteAktif IsNot Nothing Then
                    SesiRemoteAktif.CatatFrame(frameId, jpegData.Length / 1024.0)
                    ' Hitung latency dari timestamp
                    Dim currentMs = CInt(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() And Integer.MaxValue)
                    Dim latency = currentMs - timestampMs
                    If latency > 0 AndAlso latency < 10000 Then ' Sanity check
                        SesiRemoteAktif.LatencyMs = latency
                    End If
                End If
            End If

        Catch ex As Exception
            WriteLog($"[UDP] Error proses frame: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Konversi JPEG byte array ke WPF BitmapImage.
    ''' </summary>
    Private Function JpegBytesToBitmapImage(jpegData As Byte()) As System.Windows.Media.Imaging.BitmapImage
        Try
            Dim bi As New System.Windows.Media.Imaging.BitmapImage()
            Using ms As New System.IO.MemoryStream(jpegData)
                bi.BeginInit()
                bi.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad
                bi.StreamSource = ms
                bi.EndInit()
                bi.Freeze() ' Penting untuk cross-thread
            End Using
            Return bi
        Catch
            Return Nothing
        End Try
    End Function

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
            WriteLog($"Error proses frame: {ex.Message}")
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

    ''' <summary>
    ''' Tombol Transfer Berkas diklik (Fase 3b).
    ''' </summary>
    Private Async Sub btn_Transfer_Click(sender As Object, e As RoutedEventArgs) Handles btn_Transfer.Click
        Try
            WriteLog("[TRANSFER] btn_Transfer_Click - membuka FileBrowser")

            ' Buka dialog file browser
            Dim winBrowser As New wpfWin_FileBrowser()
            winBrowser.ResetForm()
            winBrowser.ModeViaRelay = ModeViaRelay  ' Set mode koneksi agar FileBrowser gunakan fungsi yang tepat
            winBrowser.Owner = Me
            WriteLog($"[TRANSFER] FileBrowser ModeViaRelay={ModeViaRelay}")
            winBrowser.ShowDialog()

            WriteLog($"[TRANSFER] FileBrowser ditutup. HasilTransfer={winBrowser.HasilTransfer}, FileTerpilih IsNot Nothing={winBrowser.FileTerpilih IsNot Nothing}")

            ' Cek apakah user memilih file untuk transfer
            If winBrowser.HasilTransfer AndAlso winBrowser.FileTerpilih IsNot Nothing Then
                Dim fileItem = winBrowser.FileTerpilih
                WriteLog($"[TRANSFER] File terpilih: {fileItem.Nama}, Path={fileItem.PathLengkap}, IsFolder={fileItem.IsFolder}")

                ' Tentukan arah transfer
                Dim arah As ArahTransfer
                If winBrowser.ModeLokal Then
                    arah = ArahTransfer.UPLOAD ' Upload dari Tamu ke Host
                Else
                    arah = ArahTransfer.DOWNLOAD ' Download dari Host ke Tamu
                End If
                WriteLog($"[TRANSFER] ModeLokal={winBrowser.ModeLokal}, Arah={arah}")

                ' Buat transfer baru
                WriteLog($"[TRANSFER] Memanggil MulaiTransferBaru dengan path={fileItem.PathLengkap}")
                Dim transfer = mdl_TransferBerkas.MulaiTransferBaru(fileItem.PathLengkap, arah, 0)
                If transfer Is Nothing Then
                    WriteLog("[TRANSFER] MulaiTransferBaru mengembalikan Nothing")
                    MessageBox.Show("Gagal membuat transfer baru.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                    Return
                End If
                WriteLog($"[TRANSFER] Transfer dibuat: {transfer.TransferId}")

                ' Kirim permintaan transfer ke Host (sesuai mode koneksi)
                If ModeViaRelay Then
                    ' Mode Internet: Kirim via Relay
                    WriteLog("[TRANSFER] Mengirim permintaan via Relay")
                    Await mdl_KoneksiRelay.KirimPermintaanTransferViaRelayAsync(transfer)
                Else
                    ' Mode LAN: Kirim langsung
                    WriteLog("[TRANSFER] Mengirim permintaan via LAN")
                    Await mdl_KoneksiJaringan.KirimPermintaanTransferAsync(transfer)
                End If

                ' Tampilkan progress dialog
                WriteLog("[TRANSFER] Membuka progress dialog")
                Dim winProgress As New wpfWin_TransferProgress()
                winProgress.SetTransferInfo(transfer.TransferId, transfer.NamaFile, transfer.UkuranFile)
                winProgress.Owner = Me
                winProgress.ShowDialog()

                ' Cek hasil transfer
                If winProgress.TransferSukses Then
                    MessageBox.Show($"Transfer berhasil: {transfer.NamaFile}", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information)
                ElseIf winProgress.Dibatalkan Then
                    WriteLog($"[TRANSFER] Transfer dibatalkan: {transfer.TransferId}")
                Else
                    WriteLog($"[TRANSFER] Transfer gagal: {transfer.TransferId}")
                End If
            Else
                WriteLog("[TRANSFER] User tidak memilih file (HasilTransfer=False atau FileTerpilih=Nothing)")
            End If

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error: {ex.Message}")
            WriteLog($"[TRANSFER] StackTrace: {ex.StackTrace}")
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

#End Region

#Region "Input Control (Fase 2b)"

    ''' <summary>
    ''' Toggle kontrol diaktifkan oleh user.
    ''' </summary>
    Private Sub tgl_Kontrol_Checked(sender As Object, e As RoutedEventArgs) Handles tgl_Kontrol.Checked
        WriteLog($"[KONTROL] Toggle Checked - mengaktifkan kontrol")
        _kontrolAktif = True
        UpdateStatusKontrol()
        img_Layar.Focus()
        ' Pastikan cursor tetap terlihat (Arrow)
        img_Layar.Cursor = Cursors.Arrow
        WriteLog($"[KONTROL] _kontrolAktif={_kontrolAktif}")
    End Sub

    ''' <summary>
    ''' Toggle kontrol dinonaktifkan oleh user.
    ''' </summary>
    Private Sub tgl_Kontrol_Unchecked(sender As Object, e As RoutedEventArgs) Handles tgl_Kontrol.Unchecked
        _kontrolAktif = False
        UpdateStatusKontrol()
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

    ''' <summary>
    ''' Aktifkan tombol transfer berkas jika Host mengizinkan (Fase 3b).
    ''' Dipanggil setelah koneksi established.
    ''' </summary>
    Public Sub AktifkanTransferBerkasJikaDiizinkan(izinTransfer As Boolean)
        WriteLog($"[TRANSFER-VIEWER] AktifkanTransferBerkasJikaDiizinkan dipanggil, izinTransfer={izinTransfer}")
        _izinTransferBerkas = izinTransfer
        btn_Transfer.IsEnabled = izinTransfer
        If Not izinTransfer Then
            btn_Transfer.ToolTip = "Host tidak mengizinkan transfer berkas"
            WriteLog($"[TRANSFER-VIEWER] Tombol Transfer DISABLED - Host tidak mengizinkan")
        Else
            btn_Transfer.ToolTip = "Transfer Berkas"
            WriteLog($"[TRANSFER-VIEWER] Tombol Transfer ENABLED")
        End If
    End Sub

#End Region

#Region "Clipboard Control (Fase 3a)"

    ''' <summary>
    ''' Toggle clipboard sync diaktifkan oleh user.
    ''' </summary>
    Private Sub tgl_Clipboard_Checked(sender As Object, e As RoutedEventArgs) Handles tgl_Clipboard.Checked
        WriteLog($"[CLIPBOARD] Toggle Checked - mengaktifkan clipboard sync")
        _clipboardAktif = True
        UpdateStatusClipboard()

        ' Mulai clipboard monitoring sebagai Tamu
        mdl_Clipboard.ClipboardSyncAktif = True
        mdl_Clipboard.ClipboardKirimCallback = AddressOf KirimClipboardKePeer
        mdl_Clipboard.MulaiClipboardMonitoring(CLIPBOARD_SOURCE_TAMU)
    End Sub

    ''' <summary>
    ''' Toggle clipboard sync dinonaktifkan oleh user.
    ''' </summary>
    Private Sub tgl_Clipboard_Unchecked(sender As Object, e As RoutedEventArgs) Handles tgl_Clipboard.Unchecked
        WriteLog($"[CLIPBOARD] Toggle Unchecked - menonaktifkan clipboard sync")
        _clipboardAktif = False
        UpdateStatusClipboard()

        ' Hentikan clipboard monitoring
        mdl_Clipboard.HentikanClipboardMonitoring()
        mdl_Clipboard.ClipboardSyncAktif = False
    End Sub

    ''' <summary>
    ''' Update tampilan status clipboard di status bar dan toggle button.
    ''' </summary>
    Private Sub UpdateStatusClipboard()
        If _clipboardAktif Then
            lbl_StatusClipboard.Text = "Clipboard: ON"
            lbl_StatusClipboard.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50)) ' Green
            tgl_Clipboard.Foreground = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50))
            tgl_Clipboard.BorderBrush = New SolidColorBrush(Color.FromRgb(&H4C, &HAF, &H50))
        Else
            lbl_StatusClipboard.Text = "Clipboard: OFF"
            lbl_StatusClipboard.Foreground = New SolidColorBrush(Color.FromRgb(&H9E, &H9E, &H9E)) ' Gray
            tgl_Clipboard.Foreground = New SolidColorBrush(Color.FromRgb(&H9E, &H9E, &H9E))
            tgl_Clipboard.BorderBrush = New SolidColorBrush(Color.FromRgb(&HBD, &HBD, &HBD))
        End If
    End Sub

    ''' <summary>
    ''' Aktifkan toggle button clipboard jika Host mengizinkan.
    ''' Dipanggil setelah koneksi established.
    ''' </summary>
    Public Sub AktifkanClipboardJikaDiizinkan(izinClipboard As Boolean)
        WriteLog($"[CLIPBOARD-VIEWER] AktifkanClipboardJikaDiizinkan dipanggil, izinClipboard={izinClipboard}")
        _izinClipboard = izinClipboard
        tgl_Clipboard.IsEnabled = izinClipboard
        If Not izinClipboard Then
            tgl_Clipboard.ToolTip = "Host tidak mengizinkan clipboard sync"
            WriteLog($"[CLIPBOARD-VIEWER] Toggle button DISABLED - Host tidak mengizinkan")
        Else
            tgl_Clipboard.ToolTip = "Aktifkan Sinkronisasi Clipboard"
            WriteLog($"[CLIPBOARD-VIEWER] Toggle button ENABLED - klik untuk mengaktifkan clipboard sync")
        End If
    End Sub

    ''' <summary>
    ''' Callback untuk mengirim clipboard ke peer (Host via LAN atau Relay).
    ''' </summary>
    Private Async Sub KirimClipboardKePeer(payload As cls_PayloadClipboard)
        Try
            If ModeViaRelay Then
                ' Mode Internet: Kirim via Relay
                Await mdl_KoneksiRelay.KirimClipboardViaRelayAsync(payload)
            Else
                ' Mode LAN: Kirim langsung
                Await mdl_KoneksiJaringan.KirimClipboardAsync(payload)
            End If
        Catch ex As Exception
            WriteLog($"[CLIPBOARD] Error kirim ke peer: {ex.Message}")
        End Try
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
        WriteLog($"[MOUSE-EVENT] MouseDown di img_Layar, _kontrolAktif={_kontrolAktif}, Button={e.ChangedButton}")
        If Not _kontrolAktif Then
            WriteLog($"[MOUSE-EVENT] SKIP: _kontrolAktif=False")
            Return
        End If

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
            WriteLog($"Error kirim keyboard: {ex.Message}")
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
            WriteLog($"Error kirim mouse move: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim input mouse click ke Host.
    ''' </summary>
    Private Async Sub KirimInputMouseClick(button As Integer, isDown As Boolean,
                                            normalizedX As Double, normalizedY As Double)
        ' Debug logging
        WriteLog($"[INPUT] KirimInputMouseClick dipanggil: button={button}, isDown={isDown}, X={normalizedX:F3}, Y={normalizedY:F3}")

        ' Cek koneksi berdasarkan mode
        Dim terhubung As Boolean = If(ModeViaRelay, TerhubungKeRelay, mdl_KoneksiJaringan.Terhubung)
        WriteLog($"[INPUT] Status koneksi: ModeViaRelay={ModeViaRelay}, terhubung={terhubung}")

        If Not terhubung Then
            WriteLog($"[INPUT] SKIP: Tidak terhubung!")
            Return
        End If

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
            WriteLog($"Error kirim mouse click: {ex.Message}")
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
            WriteLog($"Error kirim mouse wheel: {ex.Message}")
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
