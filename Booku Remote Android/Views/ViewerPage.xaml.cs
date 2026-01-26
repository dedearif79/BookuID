using BookuRemoteAndroid.Models;
using BookuRemoteAndroid.Services;

namespace BookuRemoteAndroid.Views;

public partial class ViewerPage : ContentPage
{
    private readonly NetworkService _networkService;
    private readonly SessionService _sessionService;
    private readonly UdpStreamingService _udpStreamingService;
    private readonly H264DecoderService _h264DecoderService;

    private bool _isFullscreen = false;
    private bool _controlEnabled = false;
    private int _frameCount = 0;
    private DateTime _lastFpsUpdate = DateTime.UtcNow;
    private double _lastPanX = 0;
    private double _lastPanY = 0;
    private double _screenWidth = 0;
    private double _screenHeight = 0;
    private CancellationTokenSource? _periodicRegistrationCts; // Timer untuk periodic registration

    // Frame throttling - time-based limiting untuk stabilitas
    private volatile bool _isRendering = false;
    private int _skippedFrames = 0;
    private DateTime _lastFrameTime = DateTime.UtcNow;
    private readonly object _frameLock = new object();

    // PENTING: Tidak dispose stream sama sekali!
    // Lambda di ImageSource.FromStream(() => stream) di-capture dan dipanggil KEMUDIAN oleh MAUI.
    // Jika kita dispose stream sebelum MAUI selesai render, akan crash dengan "recycled bitmap".
    // Biarkan GC menangani memory management untuk menghindari race condition.

    // Konstanta untuk throttling
    private const int MIN_FRAME_INTERVAL_MS = 50; // Max ~20 FPS (lebih konservatif untuk stabilitas)

    public ViewerPage()
    {
        InitializeComponent();

        // Get services from DI
        _networkService = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<NetworkService>();
        _sessionService = _networkService.Session;
        _udpStreamingService = new UdpStreamingService();
        _h264DecoderService = new H264DecoderService();

        // Subscribe to TCP frame events (fallback)
        _networkService.FrameReceived += OnFrameReceived;
        _networkService.ConnectionStatusChanged += OnConnectionStatusChanged;

        // Subscribe to UDP frame events (primary)
        _udpStreamingService.FrameReceived += OnUdpFrameReceived;
        _udpStreamingService.StatisticsUpdated += OnUdpStatisticsUpdated;

        // Subscribe to H.264 decoder events
        _h264DecoderService.FrameDecoded += OnH264FrameDecoded;
        _h264DecoderService.DecoderError += OnH264DecoderError;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Set host name
        if (_sessionService.HostTerhubung != null)
        {
            lblHostName.Text = _sessionService.HostTerhubung.NamaPerangkat;
        }

        // Update control toggle based on permission
        switchControl.IsEnabled = _sessionService.IzinKontrol;
        if (!_sessionService.IzinKontrol)
        {
            switchControl.IsToggled = false;
        }

        // Start UDP receiver
        await StartUdpReceiverAsync();

        // Request streaming via TCP (Host akan mengirim frame via UDP)
        await _networkService.RequestStreamingAsync();
    }

    /// <summary>
    /// Memulai UDP receiver berdasarkan mode koneksi
    /// </summary>
    private async Task StartUdpReceiverAsync()
    {
        try
        {
            // Hitung UDP Session ID dari KunciSesi menggunakan djb2 hash (cross-platform)
            // PENTING: Tidak boleh pakai GetHashCode() karena hasilnya berbeda antar platform!
            var kunciSesi = _sessionService.KunciSesi;
            var sessionId = UdpStreamingService.GenerateSessionId(kunciSesi);

            System.Diagnostics.Debug.WriteLine($"[VIEWER] UDP: KunciSesi={kunciSesi}, SessionId={sessionId}");

            if (sessionId == 0)
            {
                System.Diagnostics.Debug.WriteLine("[VIEWER] UDP: SessionId is 0, skipping UDP receiver");
                return;
            }

            // Start UDP receiver
            _udpStreamingService.StartReceiver(sessionId);

            // Jika mode relay, kirim registrasi ke relay server
            if (_networkService.IsRelayMode)
            {
                var relayIP = SettingsService.Current.RelayServerIP;
                var relayUdpPort = SettingsService.Current.PortUdpVideo;

                // Kirim beberapa kali untuk memastikan relay menerima
                for (int i = 0; i < 3; i++)
                {
                    await _udpStreamingService.SendRegistrationAsync(relayIP, relayUdpPort);
                    await Task.Delay(100);
                }

                System.Diagnostics.Debug.WriteLine($"[VIEWER] UDP registered to relay {relayIP}:{relayUdpPort}, SessionId={sessionId}");

                // Mulai periodic registration untuk menjaga endpoint di relay
                StartPeriodicRegistration(relayIP, relayUdpPort);
            }

            System.Diagnostics.Debug.WriteLine($"[VIEWER] UDP receiver started, SessionId={sessionId}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[VIEWER] StartUdpReceiver error: {ex.Message}");
        }
    }

    /// <summary>
    /// Mulai periodic registration ke relay server setiap 5 detik
    /// </summary>
    private void StartPeriodicRegistration(string relayIP, int relayUdpPort)
    {
        _periodicRegistrationCts?.Cancel();
        _periodicRegistrationCts = new CancellationTokenSource();

        Task.Run(async () =>
        {
            var token = _periodicRegistrationCts.Token;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(5000, token); // 5 detik interval
                    if (!token.IsCancellationRequested)
                    {
                        await _udpStreamingService.SendRegistrationAsync(relayIP, relayUdpPort);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[VIEWER] Periodic registration error: {ex.Message}");
                }
            }
        });

        System.Diagnostics.Debug.WriteLine("[VIEWER] Periodic registration started (5s interval)");
    }

    /// <summary>
    /// Stop periodic registration
    /// </summary>
    private void StopPeriodicRegistration()
    {
        _periodicRegistrationCts?.Cancel();
        _periodicRegistrationCts?.Dispose();
        _periodicRegistrationCts = null;
        System.Diagnostics.Debug.WriteLine("[VIEWER] Periodic registration stopped");
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        // Stop periodic registration
        StopPeriodicRegistration();

        // Stop UDP receiver
        _udpStreamingService.StopReceiver();

        // Stop H.264 decoder
        _h264DecoderService.Dispose();

        // Stop streaming via TCP
        await _networkService.StopStreamingAsync();

        // Clear image source untuk membantu GC
        imgScreenA.Source = null;

        // Hint GC untuk cleanup streams (tapi jangan force - biarkan GC bekerja)
        // Streams tidak di-dispose manual untuk menghindari race condition dengan MAUI rendering
    }

    #region Event Handlers

    private void OnFrameReceived(object? sender, FrameLayar frame)
    {
        var now = DateTime.UtcNow;

        // Time-based throttling: batasi frame rate untuk stabilitas
        lock (_frameLock)
        {
            var elapsed = (now - _lastFrameTime).TotalMilliseconds;
            if (elapsed < MIN_FRAME_INTERVAL_MS)
            {
                _skippedFrames++;
                return;
            }

            if (_isRendering)
            {
                _skippedFrames++;
                return;
            }

            _isRendering = true;
            _lastFrameTime = now;
        }

        // Pre-decode image bytes
        byte[]? imageBytes = null;
        try
        {
            imageBytes = frame.GetImageBytes();
        }
        catch
        {
            _isRendering = false;
            return;
        }

        if (imageBytes == null || imageBytes.Length == 0)
        {
            _isRendering = false;
            return;
        }

        // Store dimensions
        var frameWidth = frame.Lebar;
        var frameHeight = frame.Tinggi;

        // Copy byte array
        var imageCopy = new byte[imageBytes.Length];
        Buffer.BlockCopy(imageBytes, 0, imageCopy, 0, imageBytes.Length);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                if (activityIndicator.IsVisible)
                {
                    activityIndicator.IsRunning = false;
                    activityIndicator.IsVisible = false;
                    lblNoFrame.IsVisible = false;
                }

                // Buat MemoryStream baru dan set langsung ke single Image
                // PENTING: Tidak dispose stream - biarkan GC handle untuk menghindari "recycled bitmap" crash
                var newStream = new MemoryStream(imageCopy);
                imgScreenA.Source = ImageSource.FromStream(() => newStream);

                _screenWidth = frameWidth;
                _screenHeight = frameHeight;

                _frameCount++;
                var nowInner = DateTime.UtcNow;
                var elapsedFps = (nowInner - _lastFpsUpdate).TotalSeconds;
                if (elapsedFps >= 1.0)
                {
                    var fps = _frameCount / elapsedFps;
                    lblFPS.Text = $"FPS: {fps:F1}";
                    _frameCount = 0;
                    _lastFpsUpdate = nowInner;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Frame display error: {ex.Message}");
            }
            finally
            {
                _isRendering = false;
            }
        });
    }

    private void OnConnectionStatusChanged(object? sender, StatusKoneksi status)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (status == StatusKoneksi.TERPUTUS || status == StatusKoneksi.TIDAK_TERHUBUNG)
            {
                await DisplayAlert("Terputus", "Koneksi ke Host terputus.", "OK");
                await Shell.Current.GoToAsync("..");
            }
        });
    }

    /// <summary>
    /// Handler untuk frame UDP yang diterima
    /// </summary>
    private void OnUdpFrameReceived(object? sender, UdpFrameEventArgs e)
    {
        if (e.FrameData == null || e.FrameData.Length == 0)
            return;

        // Log frame received (lebih sering untuk debugging H.264)
        if (e.FrameId <= 30 || e.FrameId % 10 == 1)
        {
            System.Diagnostics.Debug.WriteLine($"[VIEWER-UDP] Frame #{e.FrameId} received: {e.FrameData.Length} bytes, codec={e.CodecType} (isH264={e.IsH264})");

            // Log first bytes untuk H.264 frames
            if (e.IsH264 && e.FrameData.Length >= 8 && e.FrameId <= 20)
            {
                var hexBytes = BitConverter.ToString(e.FrameData, 0, Math.Min(16, e.FrameData.Length));
                System.Diagnostics.Debug.WriteLine($"[VIEWER-UDP] H264 first bytes: {hexBytes}");
            }
        }

        // Route berdasarkan codec type
        if (e.IsH264)
        {
            // H.264 frame - kirim ke decoder service
            ProcessH264Frame(e.FrameData);
            return;
        }

        // JPEG frame - render langsung
        ProcessJpegFrame(e.FrameData);
    }

    /// <summary>
    /// Process H.264 frame - kirim ke decoder
    /// </summary>
    private void ProcessH264Frame(byte[] h264Data)
    {
        try
        {
            // Auto-start decoder jika belum running
            if (!_h264DecoderService.IsDecoderAvailable)
            {
                // Estimasi resolusi berdasarkan scale default Host (0.35 dari 1920x1080 = 672x378)
                // MediaCodec akan auto-detect resolusi sebenarnya dari SPS
                int estimatedWidth = 672;
                int estimatedHeight = 378;

                System.Diagnostics.Debug.WriteLine($"[H264-VIEWER] Auto-starting decoder: {estimatedWidth}x{estimatedHeight}");

                bool started = _h264DecoderService.Start(estimatedWidth, estimatedHeight);
                if (!started)
                {
                    System.Diagnostics.Debug.WriteLine("[H264-VIEWER] Failed to start decoder, skipping H.264 frames");
                    return;
                }
            }

            _h264DecoderService.ProcessH264Data(h264Data);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[H264-VIEWER] ProcessH264Frame error: {ex.Message}");
        }
    }

    /// <summary>
    /// Process JPEG frame - render langsung
    /// </summary>
    private void ProcessJpegFrame(byte[] jpegData)
    {
        var now = DateTime.UtcNow;

        // Time-based throttling: batasi frame rate untuk stabilitas
        lock (_frameLock)
        {
            var elapsed = (now - _lastFrameTime).TotalMilliseconds;
            if (elapsed < MIN_FRAME_INTERVAL_MS)
            {
                _skippedFrames++;
                return;
            }

            // Skip jika masih rendering frame sebelumnya
            if (_isRendering)
            {
                _skippedFrames++;
                return;
            }

            _isRendering = true;
            _lastFrameTime = now;
        }

        // Copy byte array di luar lock untuk menghindari blocking
        var imageBytes = new byte[jpegData.Length];
        Buffer.BlockCopy(jpegData, 0, imageBytes, 0, jpegData.Length);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                // Hide loading (only on first frame)
                if (activityIndicator.IsVisible)
                {
                    activityIndicator.IsRunning = false;
                    activityIndicator.IsVisible = false;
                    lblNoFrame.IsVisible = false;
                }

                // Buat MemoryStream baru dan set langsung ke single Image
                // PENTING: Tidak dispose stream - biarkan GC handle untuk menghindari "recycled bitmap" crash
                var newStream = new MemoryStream(imageBytes);
                imgScreenA.Source = ImageSource.FromStream(() => newStream);

                _frameCount++;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[UDP] Frame display error: {ex.Message}");
            }
            finally
            {
                _isRendering = false;
            }
        });
    }

    // Throttling untuk H.264 decoded frames
    private DateTime _lastH264FrameTime = DateTime.UtcNow;
    private int _h264FrameCount = 0;
    private int _h264SkippedCount = 0;

    /// <summary>
    /// Handler untuk H.264 frame yang sudah di-decode (output adalah JPEG)
    /// </summary>
    private void OnH264FrameDecoded(object? sender, DecodedFrameEventArgs e)
    {
        try
        {
            if (e.JpegData == null || e.JpegData.Length == 0)
                return;

            _h264FrameCount++;

            // Throttle H.264 decoded frames - max 20 FPS untuk kualitas lebih baik
            var now = DateTime.UtcNow;
            var elapsed = (now - _lastH264FrameTime).TotalMilliseconds;
            if (elapsed < 50) // 50ms = 20 FPS max
            {
                _h264SkippedCount++;
                return;
            }
            _lastH264FrameTime = now;

            // Log setiap 30 frame
            if (_h264FrameCount % 30 == 1)
            {
                System.Diagnostics.Debug.WriteLine($"[H264-DECODED] Frame #{_h264FrameCount}: {e.JpegData.Length} bytes, skipped={_h264SkippedCount}");
            }

            // Render decoded frame (sudah dalam format JPEG)
            ProcessJpegFrame(e.JpegData);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[H264-DECODED] Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Handler untuk H.264 decoder error
    /// </summary>
    private void OnH264DecoderError(object? sender, DecoderErrorEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"[H264] Decoder error: {e.Message}, fallback={e.ShouldFallbackToJpeg}");

        // Jika perlu fallback ke JPEG, seharusnya sudah di-handle via negosiasi codec
        // Di sini kita hanya log untuk debugging
    }

    /// <summary>
    /// Handler untuk statistik UDP
    /// </summary>
    private void OnUdpStatisticsUpdated(object? sender, UdpStatisticsEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            lblFPS.Text = $"FPS: {e.FPS} (UDP)";
        });
    }

    #endregion

    #region Button Handlers

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Konfirmasi", "Putuskan koneksi ke Host?", "Ya", "Tidak");
        if (confirm)
        {
            _networkService.Disconnect();
            await Shell.Current.GoToAsync("..");
        }
    }

    private void OnControlToggled(object? sender, ToggledEventArgs e)
    {
        _controlEnabled = e.Value;
        btnKeyboard.IsEnabled = _controlEnabled;

        // Show toast
        var message = _controlEnabled ? "Kontrol diaktifkan" : "Kontrol dinonaktifkan";
        // Toast not available in MAUI by default, could use CommunityToolkit
    }

    private void OnFullscreenClicked(object? sender, EventArgs e)
    {
        _isFullscreen = !_isFullscreen;

        if (_isFullscreen)
        {
            // Hide bars
            Shell.SetNavBarIsVisible(this, false);
            gridBottomBar.IsVisible = false;
            btnFullscreen.Text = "⛶"; // Exit fullscreen icon
        }
        else
        {
            // Show bars
            Shell.SetNavBarIsVisible(this, false); // Keep nav hidden but show bottom bar
            gridBottomBar.IsVisible = true;
            btnFullscreen.Text = "⛶";
        }
    }

    private void OnKeyboardClicked(object? sender, EventArgs e)
    {
        // Show soft keyboard
        // Note: MAUI doesn't have a direct way to show keyboard without a focused Entry
        // This would need platform-specific implementation
    }

    #endregion

    #region Touch/Gesture Handlers

    private async void OnScreenTapped(object? sender, TappedEventArgs e)
    {
        if (!_controlEnabled) return;

        var position = e.GetPosition(touchOverlay);
        if (position == null) return;

        var normalizedX = position.Value.X / touchOverlay.Width;
        var normalizedY = position.Value.Y / touchOverlay.Height;

        // Clamp values
        normalizedX = Math.Clamp(normalizedX, 0, 1);
        normalizedY = Math.Clamp(normalizedY, 0, 1);

        // Send click (down + up)
        await _networkService.SendMouseClickAsync(normalizedX, normalizedY, TombolMouse.LEFT, true);
        await Task.Delay(50);
        await _networkService.SendMouseClickAsync(normalizedX, normalizedY, TombolMouse.LEFT, false);
    }

    private async void OnScreenDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (!_controlEnabled) return;

        var position = e.GetPosition(touchOverlay);
        if (position == null) return;

        var normalizedX = position.Value.X / touchOverlay.Width;
        var normalizedY = position.Value.Y / touchOverlay.Height;

        normalizedX = Math.Clamp(normalizedX, 0, 1);
        normalizedY = Math.Clamp(normalizedY, 0, 1);

        // Send double click
        for (int i = 0; i < 2; i++)
        {
            await _networkService.SendMouseClickAsync(normalizedX, normalizedY, TombolMouse.LEFT, true);
            await Task.Delay(30);
            await _networkService.SendMouseClickAsync(normalizedX, normalizedY, TombolMouse.LEFT, false);
            await Task.Delay(50);
        }
    }

    private async void OnScreenPanned(object? sender, PanUpdatedEventArgs e)
    {
        if (!_controlEnabled) return;

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _lastPanX = 0;
                _lastPanY = 0;
                break;

            case GestureStatus.Running:
                if (switchTouchpad.IsToggled)
                {
                    // Touchpad mode - relative movement
                    var deltaX = (e.TotalX - _lastPanX) / touchOverlay.Width;
                    var deltaY = (e.TotalY - _lastPanY) / touchOverlay.Height;

                    _lastPanX = e.TotalX;
                    _lastPanY = e.TotalY;

                    // Send relative mouse move (scaled)
                    // Note: This needs different handling on host side for relative mode
                    await _networkService.SendMouseMoveAsync(
                        Math.Clamp(0.5 + deltaX * 2, 0, 1),
                        Math.Clamp(0.5 + deltaY * 2, 0, 1));
                }
                else
                {
                    // Direct mode - absolute position
                    // This requires knowing the start position which we don't have easily
                    // For now, treat as drag
                }
                break;

            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                _lastPanX = 0;
                _lastPanY = 0;
                break;
        }
    }

    private async void OnScreenPinched(object? sender, PinchGestureUpdatedEventArgs e)
    {
        if (!_controlEnabled) return;

        if (e.Status == GestureStatus.Running)
        {
            // Convert pinch to scroll
            var delta = (e.Scale - 1) * 240; // Scale to wheel delta
            var wheelDelta = (int)Math.Round(delta);

            if (Math.Abs(wheelDelta) > 10)
            {
                await _networkService.SendMouseWheelAsync(0.5, 0.5, wheelDelta);
            }
        }
    }

    #endregion
}
