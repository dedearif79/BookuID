using BookuRemoteRelay.Services;
using Microsoft.Extensions.Configuration;

namespace BookuRemoteRelay;

/// <summary>
/// Booku Remote Relay Server
/// Relay server untuk koneksi remote desktop via internet.
/// Target: Windows x64 (VPS Windows)
/// </summary>
class Program
{
    private const int DEFAULT_PORT = 45680;
    private const int DEFAULT_UDP_PORT = 45681;  // Port untuk UDP video streaming

    // Mutex untuk single instance (Windows)
    private static Mutex? _mutex;
    private const string MUTEX_NAME = "Global\\BookuRemoteRelayServer";

    static async Task Main(string[] args)
    {
        // Single Instance: Cegah multiple instance menggunakan Mutex
        bool createdNew;
        _mutex = new Mutex(true, MUTEX_NAME, out createdNew);

        if (!createdNew)
        {
            Console.WriteLine("[RELAY] ERROR: Server sudah berjalan di instance lain.");
            Console.WriteLine("[RELAY] Hanya satu instance yang diizinkan.");
            return;
        }

        Console.WriteLine("===========================================");
        Console.WriteLine("  BOOKU REMOTE RELAY SERVER");
        Console.WriteLine("===========================================");
        Console.WriteLine();

        // Prioritas port: CLI args > appsettings.json > DEFAULT_PORT
        int port = DEFAULT_PORT;
        int udpPort = DEFAULT_UDP_PORT;
        string portSource = "default";
        string udpPortSource = "default";

        // 1. Coba dari CLI args
        // Format: program.exe [tcp_port] [udp_port]
        if (args.Length > 0 && int.TryParse(args[0], out int cliPort))
        {
            port = cliPort;
            portSource = "CLI argument";
        }
        if (args.Length > 1 && int.TryParse(args[1], out int cliUdpPort))
        {
            udpPort = cliUdpPort;
            udpPortSource = "CLI argument";
        }
        else
        {
            // 2. Coba dari appsettings.json
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .Build();

                var configPort = config.GetValue<int>("RelayServer:Port");
                if (configPort > 0 && portSource != "CLI argument")
                {
                    port = configPort;
                    portSource = "appsettings.json";
                }

                var configUdpPort = config.GetValue<int>("RelayServer:UdpPort");
                if (configUdpPort > 0)
                {
                    udpPort = configUdpPort;
                    udpPortSource = "appsettings.json";
                }
            }
            catch
            {
                // Ignore config errors, use default
            }
        }

        Console.WriteLine($"[RELAY] TCP Port: {port} (source: {portSource})");
        Console.WriteLine($"[RELAY] UDP Port: {udpPort} (source: {udpPortSource})");

        // Inisialisasi services
        var protocolService = new ProtocolService();
        var connectionManager = new ConnectionManager();
        var packetRouter = new PacketRouter(connectionManager, protocolService);
        var tcpListener = new TcpListenerService(port, connectionManager, packetRouter, protocolService);
        var udpRelay = new UdpRelayService(udpPort, connectionManager);

        // CancellationToken untuk graceful shutdown
        var cts = new CancellationTokenSource();

        // Handle Ctrl+C
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            Console.WriteLine("\n[RELAY] Shutting down...");
            cts.Cancel();
        };

        try
        {
            // Start servers
            await tcpListener.StartAsync(cts.Token);
            await udpRelay.StartAsync(cts.Token);

            Console.WriteLine();
            Console.WriteLine($"[RELAY] Server started:");
            Console.WriteLine($"        TCP: 0.0.0.0:{port} (control plane)");
            Console.WriteLine($"        UDP: 0.0.0.0:{udpPort} (video streaming)");
            Console.WriteLine("[RELAY] Press Ctrl+C to stop.");
            Console.WriteLine();

            // Tunggu sampai di-cancel
            await Task.Delay(Timeout.Infinite, cts.Token);
        }
        catch (OperationCanceledException)
        {
            // Normal shutdown
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RELAY] Fatal error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
        finally
        {
            tcpListener.Stop();
            udpRelay.Stop();
        }

        Console.WriteLine("[RELAY] Server stopped.");

        // Release mutex
        _mutex?.ReleaseMutex();
        _mutex?.Dispose();
    }
}
