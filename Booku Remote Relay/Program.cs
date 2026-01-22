using BookuRemoteRelay.Services;
using Microsoft.Extensions.Configuration;

namespace BookuRemoteRelay;

/// <summary>
/// Booku Remote Relay Server
/// Relay server untuk koneksi remote desktop via internet.
/// </summary>
class Program
{
    private const int DEFAULT_PORT = 45680;
    private const string MUTEX_NAME = "Global\\BookuRemoteRelayServer";

    static async Task Main(string[] args)
    {
        // Single Instance: Cegah multiple instance menggunakan Mutex
        using var mutex = new Mutex(true, MUTEX_NAME, out bool isNewInstance);
        if (!isNewInstance)
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
        string portSource = "default";

        // 1. Coba dari CLI args
        if (args.Length > 0 && int.TryParse(args[0], out int cliPort))
        {
            port = cliPort;
            portSource = "CLI argument";
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
                if (configPort > 0)
                {
                    port = configPort;
                    portSource = "appsettings.json";
                }
            }
            catch
            {
                // Ignore config errors, use default
            }
        }

        Console.WriteLine($"[RELAY] Port source: {portSource}");

        // Inisialisasi services
        var protocolService = new ProtocolService();
        var connectionManager = new ConnectionManager();
        var packetRouter = new PacketRouter(connectionManager, protocolService);
        var tcpListener = new TcpListenerService(port, connectionManager, packetRouter, protocolService);

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
            // Start server
            await tcpListener.StartAsync(cts.Token);

            Console.WriteLine($"[RELAY] Server started on port {port}");
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
        }

        Console.WriteLine("[RELAY] Server stopped.");
    }
}
