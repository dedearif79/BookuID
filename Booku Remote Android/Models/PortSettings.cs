namespace BookuRemoteAndroid.Models;

/// <summary>
/// Model untuk menyimpan pengaturan port dan alamat server.
/// </summary>
public class PortSettings
{
    #region Default Constants

    /// <summary>Port UDP default untuk discovery perangkat di LAN</summary>
    public const int DEFAULT_PORT_DISCOVERY = 45678;

    /// <summary>Port TCP default untuk koneksi remote LAN</summary>
    public const int DEFAULT_PORT_KONEKSI = 45679;

    /// <summary>Port TCP default untuk relay server (internet)</summary>
    public const int DEFAULT_PORT_RELAY = 45680;

    /// <summary>Port UDP default untuk video streaming</summary>
    public const int DEFAULT_PORT_UDP_VIDEO = 45681;

    /// <summary>Alamat IP default relay server</summary>
    public const string DEFAULT_RELAY_SERVER_IP = "155.117.43.209";

    #endregion

    #region Properties

    /// <summary>Port UDP untuk discovery perangkat di LAN</summary>
    public int PortDiscovery { get; set; } = DEFAULT_PORT_DISCOVERY;

    /// <summary>Port TCP untuk koneksi remote LAN</summary>
    public int PortKoneksi { get; set; } = DEFAULT_PORT_KONEKSI;

    /// <summary>Port TCP untuk relay server (internet)</summary>
    public int PortRelay { get; set; } = DEFAULT_PORT_RELAY;

    /// <summary>Port UDP untuk video streaming</summary>
    public int PortUdpVideo { get; set; } = DEFAULT_PORT_UDP_VIDEO;

    /// <summary>Alamat IP relay server</summary>
    public string RelayServerIP { get; set; } = DEFAULT_RELAY_SERVER_IP;

    #endregion

    #region Computed Properties

    /// <summary>
    /// Mendapatkan alamat lengkap relay server (IP:Port).
    /// </summary>
    public string RelayServerAddress => $"{RelayServerIP}:{PortRelay}";

    #endregion

    #region Methods

    /// <summary>
    /// Reset semua pengaturan ke nilai default.
    /// </summary>
    public void ResetToDefaults()
    {
        PortDiscovery = DEFAULT_PORT_DISCOVERY;
        PortKoneksi = DEFAULT_PORT_KONEKSI;
        PortRelay = DEFAULT_PORT_RELAY;
        PortUdpVideo = DEFAULT_PORT_UDP_VIDEO;
        RelayServerIP = DEFAULT_RELAY_SERVER_IP;
    }

    /// <summary>
    /// Validasi dan koreksi nilai port yang tidak valid.
    /// </summary>
    public void Validate()
    {
        // Validasi port (harus antara 1-65535)
        if (PortDiscovery < 1 || PortDiscovery > 65535)
            PortDiscovery = DEFAULT_PORT_DISCOVERY;

        if (PortKoneksi < 1 || PortKoneksi > 65535)
            PortKoneksi = DEFAULT_PORT_KONEKSI;

        if (PortRelay < 1 || PortRelay > 65535)
            PortRelay = DEFAULT_PORT_RELAY;

        if (PortUdpVideo < 1 || PortUdpVideo > 65535)
            PortUdpVideo = DEFAULT_PORT_UDP_VIDEO;

        // Validasi IP (tidak boleh kosong)
        if (string.IsNullOrWhiteSpace(RelayServerIP))
            RelayServerIP = DEFAULT_RELAY_SERVER_IP;
    }

    /// <summary>
    /// Cek apakah settings saat ini sama dengan default.
    /// </summary>
    public bool IsDefault()
    {
        return PortDiscovery == DEFAULT_PORT_DISCOVERY &&
               PortKoneksi == DEFAULT_PORT_KONEKSI &&
               PortRelay == DEFAULT_PORT_RELAY &&
               PortUdpVideo == DEFAULT_PORT_UDP_VIDEO &&
               RelayServerIP == DEFAULT_RELAY_SERVER_IP;
    }

    /// <summary>
    /// Clone settings ke instance baru.
    /// </summary>
    public PortSettings Clone()
    {
        return new PortSettings
        {
            PortDiscovery = this.PortDiscovery,
            PortKoneksi = this.PortKoneksi,
            PortRelay = this.PortRelay,
            PortUdpVideo = this.PortUdpVideo,
            RelayServerIP = this.RelayServerIP
        };
    }

    public override string ToString()
    {
        return $"PortSettings: Discovery={PortDiscovery}, Koneksi={PortKoneksi}, Relay={PortRelay}, UdpVideo={PortUdpVideo}, IP={RelayServerIP}";
    }

    #endregion
}
