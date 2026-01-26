using BookuRemoteAndroid.Models;

namespace BookuRemoteAndroid.Services;

/// <summary>
/// Service untuk mengelola pengaturan aplikasi menggunakan MAUI Preferences API.
/// </summary>
public class SettingsService
{
    #region Preference Keys

    private const string KEY_PORT_DISCOVERY = "port_discovery";
    private const string KEY_PORT_KONEKSI = "port_koneksi";
    private const string KEY_PORT_RELAY = "port_relay";
    private const string KEY_PORT_UDP_VIDEO = "port_udp_video";
    private const string KEY_RELAY_SERVER_IP = "relay_server_ip";

    #endregion

    #region Static Instance

    private static SettingsService? _instance;
    private static readonly object _lock = new();

    /// <summary>
    /// Singleton instance untuk akses global.
    /// </summary>
    public static SettingsService Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new SettingsService();
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// Shortcut untuk mendapatkan settings aktif.
    /// </summary>
    public static PortSettings Current => Instance.Settings;

    #endregion

    #region Properties

    /// <summary>
    /// Pengaturan port yang sedang aktif.
    /// </summary>
    public PortSettings Settings { get; private set; }

    #endregion

    #region Constructor

    public SettingsService()
    {
        Settings = new PortSettings();
        LoadSettings();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Memuat pengaturan dari Preferences.
    /// </summary>
    public void LoadSettings()
    {
        try
        {
            Settings.PortDiscovery = Preferences.Get(KEY_PORT_DISCOVERY, PortSettings.DEFAULT_PORT_DISCOVERY);
            Settings.PortKoneksi = Preferences.Get(KEY_PORT_KONEKSI, PortSettings.DEFAULT_PORT_KONEKSI);
            Settings.PortRelay = Preferences.Get(KEY_PORT_RELAY, PortSettings.DEFAULT_PORT_RELAY);
            Settings.PortUdpVideo = Preferences.Get(KEY_PORT_UDP_VIDEO, PortSettings.DEFAULT_PORT_UDP_VIDEO);
            Settings.RelayServerIP = Preferences.Get(KEY_RELAY_SERVER_IP, PortSettings.DEFAULT_RELAY_SERVER_IP);

            // Validasi nilai yang dimuat
            Settings.Validate();

            Console.WriteLine($"[SettingsService] Settings loaded: {Settings}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SettingsService] Error loading settings: {ex.Message}");
            Settings.ResetToDefaults();
        }
    }

    /// <summary>
    /// Menyimpan pengaturan ke Preferences.
    /// </summary>
    public void SaveSettings()
    {
        try
        {
            // Validasi sebelum menyimpan
            Settings.Validate();

            Preferences.Set(KEY_PORT_DISCOVERY, Settings.PortDiscovery);
            Preferences.Set(KEY_PORT_KONEKSI, Settings.PortKoneksi);
            Preferences.Set(KEY_PORT_RELAY, Settings.PortRelay);
            Preferences.Set(KEY_PORT_UDP_VIDEO, Settings.PortUdpVideo);
            Preferences.Set(KEY_RELAY_SERVER_IP, Settings.RelayServerIP);

            Console.WriteLine($"[SettingsService] Settings saved: {Settings}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SettingsService] Error saving settings: {ex.Message}");
        }
    }

    /// <summary>
    /// Menyimpan pengaturan baru dan update instance.
    /// </summary>
    /// <param name="newSettings">Pengaturan baru untuk disimpan</param>
    public void SaveSettings(PortSettings newSettings)
    {
        Settings = newSettings.Clone();
        SaveSettings();
    }

    /// <summary>
    /// Reset pengaturan ke default dan simpan.
    /// </summary>
    public void ResetToDefaults()
    {
        Settings.ResetToDefaults();
        SaveSettings();
        Console.WriteLine("[SettingsService] Settings reset to defaults");
    }

    /// <summary>
    /// Hapus semua pengaturan dari Preferences.
    /// </summary>
    public void ClearSettings()
    {
        try
        {
            Preferences.Remove(KEY_PORT_DISCOVERY);
            Preferences.Remove(KEY_PORT_KONEKSI);
            Preferences.Remove(KEY_PORT_RELAY);
            Preferences.Remove(KEY_PORT_UDP_VIDEO);
            Preferences.Remove(KEY_RELAY_SERVER_IP);

            Settings.ResetToDefaults();
            Console.WriteLine("[SettingsService] Settings cleared");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SettingsService] Error clearing settings: {ex.Message}");
        }
    }

    #endregion

    #region Helper Properties (Shortcuts)

    /// <summary>
    /// Port discovery aktif.
    /// </summary>
    public int PortDiscovery => Settings.PortDiscovery;

    /// <summary>
    /// Port koneksi TCP aktif.
    /// </summary>
    public int PortKoneksi => Settings.PortKoneksi;

    /// <summary>
    /// Port relay aktif.
    /// </summary>
    public int PortRelay => Settings.PortRelay;

    /// <summary>
    /// Port UDP video streaming aktif.
    /// </summary>
    public int PortUdpVideo => Settings.PortUdpVideo;

    /// <summary>
    /// IP relay server aktif.
    /// </summary>
    public string RelayServerIP => Settings.RelayServerIP;

    /// <summary>
    /// Alamat lengkap relay server (IP:Port).
    /// </summary>
    public string RelayServerAddress => Settings.RelayServerAddress;

    #endregion
}
