using BookuRemoteAndroid.Models;
using BookuRemoteAndroid.Services;

namespace BookuRemoteAndroid.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadSettings();
    }

    private void LoadSettings()
    {
        var settings = SettingsService.Current;

        entryPortDiscovery.Text = settings.PortDiscovery.ToString();
        entryPortKoneksi.Text = settings.PortKoneksi.ToString();
        entryRelayServerIP.Text = settings.RelayServerIP;
        entryPortRelay.Text = settings.PortRelay.ToString();
        entryPortUdpVideo.Text = settings.PortUdpVideo.ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Validate inputs
        if (!int.TryParse(entryPortDiscovery.Text, out int portDiscovery) ||
            portDiscovery < 1 || portDiscovery > 65535)
        {
            await DisplayAlert("Validasi", "Port Discovery harus berupa angka antara 1 - 65535.", "OK");
            entryPortDiscovery.Focus();
            return;
        }

        if (!int.TryParse(entryPortKoneksi.Text, out int portKoneksi) ||
            portKoneksi < 1 || portKoneksi > 65535)
        {
            await DisplayAlert("Validasi", "Port Koneksi harus berupa angka antara 1 - 65535.", "OK");
            entryPortKoneksi.Focus();
            return;
        }

        if (!int.TryParse(entryPortRelay.Text, out int portRelay) ||
            portRelay < 1 || portRelay > 65535)
        {
            await DisplayAlert("Validasi", "Port Relay harus berupa angka antara 1 - 65535.", "OK");
            entryPortRelay.Focus();
            return;
        }

        if (!int.TryParse(entryPortUdpVideo.Text, out int portUdpVideo) ||
            portUdpVideo < 1 || portUdpVideo > 65535)
        {
            await DisplayAlert("Validasi", "Port UDP Video harus berupa angka antara 1 - 65535.", "OK");
            entryPortUdpVideo.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(entryRelayServerIP.Text))
        {
            await DisplayAlert("Validasi", "Alamat IP Relay Server tidak boleh kosong.", "OK");
            entryRelayServerIP.Focus();
            return;
        }

        // Save to settings
        var settings = SettingsService.Instance.Settings;
        settings.PortDiscovery = portDiscovery;
        settings.PortKoneksi = portKoneksi;
        settings.PortRelay = portRelay;
        settings.PortUdpVideo = portUdpVideo;
        settings.RelayServerIP = entryRelayServerIP.Text.Trim();

        SettingsService.Instance.SaveSettings();

        await DisplayAlert("Sukses", "Pengaturan berhasil disimpan.\nRestart aplikasi untuk menerapkan perubahan.", "OK");

        // Navigate back
        await Navigation.PopAsync();
    }

    private async void OnResetClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Konfirmasi",
            "Reset semua pengaturan ke nilai default?", "Ya", "Batal");

        if (confirm)
        {
            SettingsService.Instance.ResetToDefaults();
            LoadSettings();

            await DisplayAlert("Sukses", "Pengaturan berhasil di-reset ke default.", "OK");
        }
    }
}
