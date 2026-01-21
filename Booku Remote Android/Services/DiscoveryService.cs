using System.Net;
using System.Net.Sockets;
using System.Text;
using BookuRemoteAndroid.Models;

namespace BookuRemoteAndroid.Services;

/// <summary>
/// Service untuk discovery perangkat Host di LAN menggunakan UDP broadcast
/// </summary>
public class DiscoveryService
{
    public const int PORT_DISCOVERY = 45678;
    public const int TIMEOUT_DISCOVERY_MS = 3000;

    private readonly ProtocolService _protocolService;
    private UdpClient? _udpClient;
    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    /// Event ketika perangkat ditemukan
    /// </summary>
    public event EventHandler<PerangkatLAN>? DeviceDiscovered;

    public DiscoveryService()
    {
        _protocolService = new ProtocolService();
    }

    /// <summary>
    /// Scan perangkat Host di jaringan LAN
    /// </summary>
    public async Task ScanDevicesAsync()
    {
        StopListening();

        _cancellationTokenSource = new CancellationTokenSource();

        try
        {
            // Buat UDP client untuk broadcast
            _udpClient = new UdpClient();
            _udpClient.EnableBroadcast = true;

            // Kirim broadcast discovery
            var paket = _protocolService.CreateDiscoveryBroadcast();
            var data = _protocolService.SerializePaketToBytes(paket);
            var broadcastEndpoint = new IPEndPoint(IPAddress.Broadcast, PORT_DISCOVERY);

            await _udpClient.SendAsync(data, data.Length, broadcastEndpoint);

            // Dengarkan response selama timeout
            var listenTask = ListenForResponsesAsync(_cancellationTokenSource.Token);
            var timeoutTask = Task.Delay(TIMEOUT_DISCOVERY_MS, _cancellationTokenSource.Token);

            await Task.WhenAny(listenTask, timeoutTask);

            // Setelah timeout, stop listening
            StopListening();
        }
        catch (OperationCanceledException)
        {
            // Normal cancellation
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Discovery error: {ex.Message}");
        }
    }

    /// <summary>
    /// Mendengarkan response dari Host
    /// </summary>
    private async Task ListenForResponsesAsync(CancellationToken cancellationToken)
    {
        if (_udpClient == null) return;

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Tunggu response
                var result = await _udpClient.ReceiveAsync(cancellationToken);

                // Parse response
                var json = Encoding.UTF8.GetString(result.Buffer);
                var paket = _protocolService.DeserializePaket(json);

                if (paket != null && paket.TipePaket == (int)TipePaket.RESPON_DISCOVERY)
                {
                    var device = _protocolService.ParseResponDiscovery(paket);
                    if (device != null)
                    {
                        // Tambah IP dari sender jika tidak ada di payload
                        if (string.IsNullOrEmpty(device.AlamatIP))
                        {
                            device.AlamatIP = result.RemoteEndPoint.Address.ToString();
                        }

                        device.WaktuTerdeteksi = DateTime.UtcNow;

                        // Raise event
                        DeviceDiscovered?.Invoke(this, device);
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Normal cancellation
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Listen error: {ex.Message}");
        }
    }

    /// <summary>
    /// Stop listening dan cleanup
    /// </summary>
    public void StopListening()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;

        _udpClient?.Close();
        _udpClient?.Dispose();
        _udpClient = null;
    }

    /// <summary>
    /// Mendapatkan alamat IP lokal perangkat Android
    /// </summary>
    public static string GetLocalIPAddress()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
        }
        catch
        {
            // Fallback
        }

        return "127.0.0.1";
    }

    /// <summary>
    /// Mendapatkan nama perangkat Android
    /// </summary>
    public static string GetDeviceName()
    {
        try
        {
            return DeviceInfo.Name;
        }
        catch
        {
            return "Android Device";
        }
    }
}
