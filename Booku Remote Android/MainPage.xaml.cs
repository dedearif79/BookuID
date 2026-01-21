using BookuRemoteAndroid.Models;
using BookuRemoteAndroid.Services;
using BookuRemoteAndroid.Views;
using System.Collections.ObjectModel;

namespace BookuRemoteAndroid;

public partial class MainPage : ContentPage
{
    private readonly DiscoveryService _discoveryService;
    private readonly NetworkService _networkService;
    private readonly SessionService _sessionService;

    public ObservableCollection<PerangkatLAN> Devices { get; } = new();
    private PerangkatLAN? _selectedDevice;
    private bool _isScanning = false;

    public MainPage()
    {
        InitializeComponent();

        // Get services from DI
        _discoveryService = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<DiscoveryService>();
        _networkService = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<NetworkService>();
        _sessionService = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<SessionService>();

        // Subscribe to events
        _discoveryService.DeviceDiscovered += OnDeviceDiscovered;
        _networkService.ConnectionStatusChanged += OnConnectionStatusChanged;

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Auto scan saat halaman muncul
        await ScanDevicesAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Cleanup
        _discoveryService.StopListening();
    }

    #region Event Handlers

    private void OnDeviceDiscovered(object? sender, PerangkatLAN device)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // Cek apakah device sudah ada di list
            var existing = Devices.FirstOrDefault(d => d.AlamatIP == device.AlamatIP);
            if (existing != null)
            {
                // Update existing
                var index = Devices.IndexOf(existing);
                Devices[index] = device;
            }
            else
            {
                // Add new
                Devices.Add(device);
            }
        });
    }

    private void OnConnectionStatusChanged(object? sender, StatusKoneksi status)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            switch (status)
            {
                case StatusKoneksi.MENUNGGU_PERSETUJUAN:
                    lblLoadingStatus.Text = "Menunggu persetujuan Host...";
                    break;

                case StatusKoneksi.TERHUBUNG:
                    gridLoading.IsVisible = false;
                    // Navigate ke Viewer
                    await Shell.Current.GoToAsync(nameof(ViewerPage));
                    break;

                case StatusKoneksi.DITOLAK:
                    gridLoading.IsVisible = false;
                    await DisplayAlert("Ditolak", "Permintaan koneksi ditolak oleh Host.", "OK");
                    break;

                case StatusKoneksi.TERPUTUS:
                    gridLoading.IsVisible = false;
                    break;
            }
        });
    }

    private void OnDeviceSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            _selectedDevice = e.CurrentSelection[0] as PerangkatLAN;
            btnConnect.IsEnabled = _selectedDevice?.Status == StatusPerangkat.TERSEDIA;
        }
        else
        {
            _selectedDevice = null;
            btnConnect.IsEnabled = false;
        }
    }

    #endregion

    #region Button Handlers

    private async void OnScanClicked(object? sender, EventArgs e)
    {
        await ScanDevicesAsync();
    }

    private async void OnConnectClicked(object? sender, EventArgs e)
    {
        if (_selectedDevice == null) return;

        await ConnectToDeviceAsync(_selectedDevice);
    }

    private void OnManualIPTextChanged(object? sender, TextChangedEventArgs e)
    {
        // Enable tombol jika IP valid (minimal ada titik dan angka)
        var ip = e.NewTextValue?.Trim() ?? "";
        btnConnectManual.IsEnabled = IsValidIPFormat(ip);
    }

    private async void OnConnectManualClicked(object? sender, EventArgs e)
    {
        var ip = entryManualIP.Text?.Trim() ?? "";
        if (string.IsNullOrEmpty(ip)) return;

        // Buat device manual
        var manualDevice = new PerangkatLAN
        {
            NamaPerangkat = "Host (" + ip + ")",
            AlamatIP = ip,
            Status = StatusPerangkat.TERSEDIA
        };

        await ConnectToDeviceAsync(manualDevice);
    }

    #endregion

    #region Connection Helper

    private async Task ConnectToDeviceAsync(PerangkatLAN device)
    {
        try
        {
            // Show loading
            gridLoading.IsVisible = true;
            lblLoadingStatus.Text = "Menghubungkan ke " + device.NamaPerangkat + "...";
            btnConnect.IsEnabled = false;
            btnConnectManual.IsEnabled = false;

            // Attempt connection
            var result = await _networkService.ConnectAsync(device);

            if (!result.Success)
            {
                gridLoading.IsVisible = false;
                await DisplayAlert("Gagal", result.Message, "OK");
                btnConnect.IsEnabled = _selectedDevice?.Status == StatusPerangkat.TERSEDIA;
                btnConnectManual.IsEnabled = IsValidIPFormat(entryManualIP.Text ?? "");
            }
            // Jika berhasil, event handler akan navigate ke ViewerPage
        }
        catch (Exception ex)
        {
            gridLoading.IsVisible = false;
            await DisplayAlert("Error", ex.Message, "OK");
            btnConnect.IsEnabled = _selectedDevice?.Status == StatusPerangkat.TERSEDIA;
            btnConnectManual.IsEnabled = IsValidIPFormat(entryManualIP.Text ?? "");
        }
    }

    private static bool IsValidIPFormat(string ip)
    {
        if (string.IsNullOrWhiteSpace(ip)) return false;

        // Simple validation: harus ada minimal 3 titik dan tidak kosong
        var parts = ip.Split('.');
        if (parts.Length != 4) return false;

        foreach (var part in parts)
        {
            if (string.IsNullOrEmpty(part)) return false;
            if (!int.TryParse(part, out int num)) return false;
            if (num < 0 || num > 255) return false;
        }

        return true;
    }

    #endregion

    #region Private Methods

    private async Task ScanDevicesAsync()
    {
        if (_isScanning) return;

        try
        {
            _isScanning = true;
            btnScan.IsEnabled = false;
            btnScan.Text = "⏳ Scanning...";

            // Clear existing devices
            Devices.Clear();
            _selectedDevice = null;
            btnConnect.IsEnabled = false;

            // Start discovery
            await _discoveryService.ScanDevicesAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Gagal scan: " + ex.Message, "OK");
        }
        finally
        {
            _isScanning = false;
            btnScan.IsEnabled = true;
            btnScan.Text = "🔍 Scan";
        }
    }

    #endregion
}
