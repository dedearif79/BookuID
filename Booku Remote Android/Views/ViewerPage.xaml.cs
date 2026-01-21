using BookuRemoteAndroid.Models;
using BookuRemoteAndroid.Services;

namespace BookuRemoteAndroid.Views;

public partial class ViewerPage : ContentPage
{
    private readonly NetworkService _networkService;
    private readonly SessionService _sessionService;

    private bool _isFullscreen = false;
    private bool _controlEnabled = false;
    private int _frameCount = 0;
    private DateTime _lastFpsUpdate = DateTime.UtcNow;
    private double _lastPanX = 0;
    private double _lastPanY = 0;
    private double _screenWidth = 0;
    private double _screenHeight = 0;
    private bool _useBufferA = true; // Double buffering flag

    public ViewerPage()
    {
        InitializeComponent();

        // Get services from DI
        _networkService = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<NetworkService>();
        _sessionService = _networkService.Session;

        // Subscribe to events
        _networkService.FrameReceived += OnFrameReceived;
        _networkService.ConnectionStatusChanged += OnConnectionStatusChanged;
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

        // Request streaming
        await _networkService.RequestStreamingAsync();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        // Stop streaming
        await _networkService.StopStreamingAsync();
    }

    #region Event Handlers

    private void OnFrameReceived(object? sender, FrameLayar frame)
    {
        // Pre-decode image bytes di background thread untuk menghindari blocking UI
        byte[]? imageBytes = null;
        try
        {
            imageBytes = frame.GetImageBytes();
        }
        catch
        {
            return;
        }

        if (imageBytes == null || imageBytes.Length == 0)
            return;

        // Store dimensions (thread-safe read)
        var frameWidth = frame.Lebar;
        var frameHeight = frame.Tinggi;

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

                // Double buffering: load ke buffer tersembunyi, lalu swap visibility
                var targetImage = _useBufferA ? imgScreenA : imgScreenB;
                var otherImage = _useBufferA ? imgScreenB : imgScreenA;

                // Set image source ke buffer yang akan ditampilkan
                targetImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));

                // Swap visibility - tampilkan buffer baru, sembunyikan buffer lama
                targetImage.IsVisible = true;
                otherImage.IsVisible = false;

                // Toggle buffer untuk frame berikutnya
                _useBufferA = !_useBufferA;

                // Store screen dimensions
                _screenWidth = frameWidth;
                _screenHeight = frameHeight;

                // Calculate FPS
                _frameCount++;
                var now = DateTime.UtcNow;
                var elapsed = (now - _lastFpsUpdate).TotalSeconds;
                if (elapsed >= 1.0)
                {
                    var fps = _frameCount / elapsed;
                    lblFPS.Text = $"FPS: {fps:F1}";
                    _frameCount = 0;
                    _lastFpsUpdate = now;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Frame display error: {ex.Message}");
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
