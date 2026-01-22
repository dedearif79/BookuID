using System.IO;
using System.Net.Sockets;
using System.Text;
using BookuRemoteAndroid.Models;

namespace BookuRemoteAndroid.Services;

/// <summary>
/// Service untuk koneksi TCP dan streaming
/// </summary>
public class NetworkService : IDisposable
{
    // Default constants for fallback
    public const int DEFAULT_PORT_KONEKSI = 45679;
    public const int TIMEOUT_KONEKSI_MS = 10000;
    public const int HEARTBEAT_INTERVAL_MS = 5000;
    public const int MOUSE_THROTTLE_MS = 30;

    // Default Relay Server constants
    public const string DEFAULT_RELAY_SERVER_IP = "155.117.43.209";
    public const int DEFAULT_PORT_RELAY = 45680;

    /// <summary>
    /// Port koneksi TCP aktif (dari SettingsService)
    /// </summary>
    public int PortKoneksi => SettingsService.Current.PortKoneksi;

    /// <summary>
    /// Port relay aktif (dari SettingsService)
    /// </summary>
    public int PortRelay => SettingsService.Current.PortRelay;

    /// <summary>
    /// IP relay server aktif (dari SettingsService)
    /// </summary>
    public string RelayServerIP => SettingsService.Current.RelayServerIP;

    private readonly ProtocolService _protocolService;
    private readonly SessionService _sessionService;

    private TcpClient? _tcpClient;
    private NetworkStream? _networkStream;
    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _receiveTask;
    private Task? _heartbeatTask;
    private DateTime _lastMouseMove = DateTime.MinValue;

    // Relay mode tracking
    private ModeKoneksi _modeKoneksi = ModeKoneksi.LAN;
    private string _hostCode = string.Empty;

    /// <summary>
    /// Event ketika status koneksi berubah
    /// </summary>
    public event EventHandler<StatusKoneksi>? ConnectionStatusChanged;

    /// <summary>
    /// Event ketika frame layar diterima
    /// </summary>
    public event EventHandler<FrameLayar>? FrameReceived;

    public NetworkService()
    {
        _protocolService = new ProtocolService();
        _sessionService = new SessionService();
    }

    #region Connection

    /// <summary>
    /// Menghubungkan ke Host
    /// </summary>
    public async Task<KoneksiResult> ConnectAsync(PerangkatLAN host)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"[CONNECT] ConnectAsync started. Host={host.AlamatIP}:{host.PortTCP}");

            Disconnect();
            System.Diagnostics.Debug.WriteLine("[CONNECT] Disconnect() completed");

            _cancellationTokenSource = new CancellationTokenSource();

            // Buat koneksi TCP
            _tcpClient = new TcpClient();
            _tcpClient.ReceiveTimeout = TIMEOUT_KONEKSI_MS;
            _tcpClient.SendTimeout = TIMEOUT_KONEKSI_MS;
            System.Diagnostics.Debug.WriteLine("[CONNECT] TcpClient created");

            // Connect dengan timeout
            using var connectCts = new CancellationTokenSource(TIMEOUT_KONEKSI_MS);
            System.Diagnostics.Debug.WriteLine("[CONNECT] Connecting...");
            await _tcpClient.ConnectAsync(host.AlamatIP, host.PortTCP, connectCts.Token);
            System.Diagnostics.Debug.WriteLine("[CONNECT] TCP Connected!");

            _networkStream = _tcpClient.GetStream();
            System.Diagnostics.Debug.WriteLine($"[CONNECT] NetworkStream obtained. CanWrite={_networkStream?.CanWrite}, CanRead={_networkStream?.CanRead}");

            // Kirim permintaan koneksi
            var deviceName = DiscoveryService.GetDeviceName();
            var deviceIP = DiscoveryService.GetLocalIPAddress();
            System.Diagnostics.Debug.WriteLine($"[CONNECT] Device: {deviceName}, IP: {deviceIP}");

            var paketRequest = _protocolService.CreatePermintaanKoneksi(deviceName, deviceIP);
            System.Diagnostics.Debug.WriteLine($"[CONNECT] Paket created. TipePaket={paketRequest.TipePaket}");

            System.Diagnostics.Debug.WriteLine("[CONNECT] Calling SendPacketAsync...");
            await SendPacketAsync(paketRequest);
            System.Diagnostics.Debug.WriteLine("[CONNECT] SendPacketAsync completed");

            // Update status
            RaiseConnectionStatus(StatusKoneksi.MENUNGGU_PERSETUJUAN);

            // Tunggu respon
            var response = await ReceivePacketAsync(_cancellationTokenSource.Token);
            if (response == null)
            {
                Disconnect();
                return KoneksiResult.Gagal("Tidak ada respon dari Host");
            }

            // Parse respon koneksi
            var responData = _protocolService.ParseResponKoneksi(response);
            if (responData == null)
            {
                Disconnect();
                return KoneksiResult.Gagal("Respon tidak valid");
            }

            // Cek hasil
            if (responData.Hasil == HasilPersetujuan.DITERIMA)
            {
                // Simpan sesi
                _sessionService.MulaiSesi(host, responData);

                // Mulai receive loop dan heartbeat
                StartReceiveLoop();
                StartHeartbeat();

                RaiseConnectionStatus(StatusKoneksi.TERHUBUNG);

                return KoneksiResult.Sukses(responData.KunciSesi, responData.IzinKontrol);
            }
            else if (responData.Hasil == HasilPersetujuan.DITOLAK)
            {
                Disconnect();
                RaiseConnectionStatus(StatusKoneksi.DITOLAK);
                return KoneksiResult.Gagal(responData.Pesan ?? "Koneksi ditolak oleh Host");
            }
            else
            {
                Disconnect();
                return KoneksiResult.Gagal("Timeout menunggu persetujuan");
            }
        }
        catch (OperationCanceledException)
        {
            Disconnect();
            return KoneksiResult.Gagal("Koneksi timeout");
        }
        catch (SocketException ex)
        {
            Disconnect();
            return KoneksiResult.Gagal($"Gagal terhubung: {ex.Message}");
        }
        catch (Exception ex)
        {
            Disconnect();
            return KoneksiResult.Gagal($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Memutuskan koneksi
    /// </summary>
    public void Disconnect()
    {
        try
        {
            // Kirim paket tutup koneksi jika masih terhubung
            if (_sessionService.IsTerhubung && _networkStream != null)
            {
                var paket = _protocolService.CreateTutupKoneksi(_sessionService.KunciSesi!);
                SendPacketSync(paket);
            }
        }
        catch
        {
            // Ignore errors saat disconnect
        }

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;

        _networkStream?.Close();
        _networkStream?.Dispose();
        _networkStream = null;

        _tcpClient?.Close();
        _tcpClient?.Dispose();
        _tcpClient = null;

        _sessionService.AkhiriSesi();
        RaiseConnectionStatus(StatusKoneksi.TIDAK_TERHUBUNG);

        // Reset relay mode
        _modeKoneksi = ModeKoneksi.LAN;
        _hostCode = string.Empty;
    }

    #endregion

    #region Relay Connection

    /// <summary>
    /// Query Host via Relay Server berdasarkan HostCode
    /// </summary>
    public async Task<PayloadQueryHostResult?> QueryHostViaRelayAsync(string hostCode)
    {
        TcpClient? tempClient = null;
        NetworkStream? tempStream = null;

        try
        {
            System.Diagnostics.Debug.WriteLine($"[RELAY] QueryHost started. HostCode={hostCode}");

            tempClient = new TcpClient();
            tempClient.ReceiveTimeout = TIMEOUT_KONEKSI_MS;
            tempClient.SendTimeout = TIMEOUT_KONEKSI_MS;

            // Connect ke Relay Server
            using var connectCts = new CancellationTokenSource(TIMEOUT_KONEKSI_MS);
            await tempClient.ConnectAsync(RelayServerIP, PortRelay, connectCts.Token);
            System.Diagnostics.Debug.WriteLine("[RELAY] Connected to Relay Server");

            tempStream = tempClient.GetStream();

            // Kirim RELAY_QUERY_HOST
            var payload = new PayloadQueryHost { HostCode = hostCode };
            var paket = PaketData.Create(TipePaket.RELAY_QUERY_HOST, string.Empty,
                System.Text.Json.JsonSerializer.Serialize(payload, BookuJsonContext.Default.PayloadQueryHost));

            var json = System.Text.Json.JsonSerializer.Serialize(paket, BookuJsonContext.Default.PaketData);
            var bytes = Encoding.UTF8.GetBytes(json);
            await tempStream.WriteAsync(bytes, 0, bytes.Length);
            System.Diagnostics.Debug.WriteLine($"[RELAY] Sent RELAY_QUERY_HOST: {json}");

            // Terima response
            var buffer = new byte[8192];
            var receivedData = new StringBuilder();
            using var readCts = new CancellationTokenSource(TIMEOUT_KONEKSI_MS);

            while (true)
            {
                var bytesRead = await tempStream.ReadAsync(buffer, 0, buffer.Length, readCts.Token);
                if (bytesRead == 0) break;

                receivedData.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                var data = receivedData.ToString();

                // Cari JSON lengkap dengan bracket tracking
                int jsonEnd = FindCompleteJsonEnd(data);
                if (jsonEnd > 0)
                {
                    var jsonStr = data.Substring(0, jsonEnd + 1); // +1 untuk include closing brace
                    System.Diagnostics.Debug.WriteLine($"[RELAY] Received: {jsonStr}");

                    var responsePaket = System.Text.Json.JsonSerializer.Deserialize(jsonStr, BookuJsonContext.Default.PaketData);
                    if (responsePaket?.TipePaket == (int)TipePaket.RELAY_QUERY_HOST_RESULT)
                    {
                        return System.Text.Json.JsonSerializer.Deserialize(responsePaket.Payload, BookuJsonContext.Default.PayloadQueryHostResult);
                    }
                    else if (responsePaket?.TipePaket == (int)TipePaket.RELAY_INVALID_CODE)
                    {
                        return new PayloadQueryHostResult { Found = false, Pesan = "Kode Host tidak valid" };
                    }
                    else if (responsePaket?.TipePaket == (int)TipePaket.RELAY_HOST_OFFLINE)
                    {
                        return new PayloadQueryHostResult { Found = false, Pesan = "Host sedang offline" };
                    }
                    break;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[RELAY] QueryHost error: {ex.Message}");
            return null;
        }
        finally
        {
            tempStream?.Close();
            tempStream?.Dispose();
            tempClient?.Close();
            tempClient?.Dispose();
        }
    }

    /// <summary>
    /// Menghubungkan ke Host melalui Relay Server
    /// </summary>
    public async Task<KoneksiResult> ConnectViaRelayAsync(string hostCode, string password = "")
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"[RELAY] ConnectViaRelay started. HostCode={hostCode}");

            Disconnect();
            _cancellationTokenSource = new CancellationTokenSource();

            // Connect ke Relay Server
            _tcpClient = new TcpClient();
            _tcpClient.ReceiveTimeout = TIMEOUT_KONEKSI_MS;
            _tcpClient.SendTimeout = TIMEOUT_KONEKSI_MS;

            using var connectCts = new CancellationTokenSource(TIMEOUT_KONEKSI_MS);
            await _tcpClient.ConnectAsync(RelayServerIP, PortRelay, connectCts.Token);
            System.Diagnostics.Debug.WriteLine("[RELAY] Connected to Relay Server");

            _networkStream = _tcpClient.GetStream();

            // Set mode relay
            _modeKoneksi = ModeKoneksi.INTERNET;
            _hostCode = hostCode;

            // Kirim RELAY_CONNECT_REQUEST
            var deviceName = DiscoveryService.GetDeviceName();
            var deviceIP = DiscoveryService.GetLocalIPAddress();

            var payload = new PayloadRelayConnectRequest
            {
                HostCode = hostCode,
                NamaPerangkat = deviceName,
                AlamatIP = deviceIP,
                VersiProtokol = "1.0",
                Password = password
            };

            var paket = PaketData.Create(TipePaket.RELAY_CONNECT_REQUEST, string.Empty,
                System.Text.Json.JsonSerializer.Serialize(payload, BookuJsonContext.Default.PayloadRelayConnectRequest));

            await SendPacketAsync(paket);
            System.Diagnostics.Debug.WriteLine("[RELAY] Sent RELAY_CONNECT_REQUEST");

            // Update status
            RaiseConnectionStatus(StatusKoneksi.MENUNGGU_PERSETUJUAN);

            // Tunggu respon (bisa RESPON_KONEKSI atau RELAY_ERROR)
            var response = await ReceivePacketAsync(_cancellationTokenSource.Token);
            if (response == null)
            {
                Disconnect();
                return KoneksiResult.Gagal("Tidak ada respon dari Relay Server");
            }

            System.Diagnostics.Debug.WriteLine($"[RELAY] Response TipePaket={response.TipePaket}");

            // Handle berbagai tipe respon
            if (response.TipePaket == (int)TipePaket.RELAY_ERROR)
            {
                var error = System.Text.Json.JsonSerializer.Deserialize(response.Payload, BookuJsonContext.Default.PayloadRelayError);
                Disconnect();
                return KoneksiResult.Gagal(error?.Pesan ?? "Error dari Relay Server");
            }
            else if (response.TipePaket == (int)TipePaket.RELAY_HOST_OFFLINE)
            {
                Disconnect();
                return KoneksiResult.Gagal("Host sedang offline");
            }
            else if (response.TipePaket == (int)TipePaket.RELAY_INVALID_CODE)
            {
                Disconnect();
                return KoneksiResult.Gagal("Kode Host tidak valid");
            }
            else if (response.TipePaket == (int)TipePaket.RESPON_KONEKSI)
            {
                // Parse respon koneksi dari Host (melalui Relay)
                var responData = _protocolService.ParseResponKoneksi(response);
                if (responData == null)
                {
                    Disconnect();
                    return KoneksiResult.Gagal("Respon tidak valid dari Host");
                }

                // Hasil dari HasilPersetujuan
                if (responData.Hasil == HasilPersetujuan.DITOLAK)
                {
                    Disconnect();
                    RaiseConnectionStatus(StatusKoneksi.DITOLAK);
                    return KoneksiResult.Gagal(responData.Pesan ?? "Koneksi ditolak oleh Host");
                }

                if (responData.Hasil == HasilPersetujuan.DITERIMA)
                {
                    // PENTING: Untuk mode Relay, gunakan IdSesi dari paket (Relay Session ID)
                    // bukan KunciSesi dari payload, karena Relay routing berdasarkan IdSesi paket
                    var relaySessionId = response.IdSesi;
                    System.Diagnostics.Debug.WriteLine($"[RELAY] Using RelaySessionId: {relaySessionId}");

                    // Simpan session menggunakan method khusus relay dengan RelaySessionId
                    _sessionService.MulaiSesiRelay(hostCode, "Host via Relay", responData, relaySessionId);
                    RaiseConnectionStatus(StatusKoneksi.TERHUBUNG);

                    // Mulai receive loop dan heartbeat
                    StartReceiveLoop();
                    StartHeartbeat();

                    return KoneksiResult.Sukses(responData.KunciSesi, responData.IzinKontrol);
                }
            }

            Disconnect();
            return KoneksiResult.Gagal("Respon tidak dikenali");
        }
        catch (OperationCanceledException)
        {
            Disconnect();
            return KoneksiResult.Gagal("Koneksi timeout");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[RELAY] ConnectViaRelay error: {ex.Message}");
            Disconnect();
            return KoneksiResult.Gagal($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Property untuk cek apakah dalam mode relay
    /// </summary>
    public bool IsRelayMode => _modeKoneksi == ModeKoneksi.INTERNET;

    /// <summary>
    /// Property untuk mendapatkan HostCode (untuk mode relay)
    /// </summary>
    public string HostCode => _hostCode;

    #endregion

    #region Streaming

    /// <summary>
    /// Meminta Host untuk mulai streaming layar
    /// </summary>
    public async Task RequestStreamingAsync()
    {
        if (!_sessionService.IsTerhubung) return;

        var paket = _protocolService.CreatePermintaanStreaming(_sessionService.KunciSesi!);
        await SendPacketAsync(paket);
    }

    /// <summary>
    /// Meminta Host untuk menghentikan streaming
    /// </summary>
    public async Task StopStreamingAsync()
    {
        if (!_sessionService.IsTerhubung) return;

        var paket = _protocolService.CreateHentikanStreaming(_sessionService.KunciSesi!);
        await SendPacketAsync(paket);
    }

    #endregion

    #region Input

    /// <summary>
    /// Mengirim input keyboard ke Host
    /// </summary>
    public async Task SendKeyboardInputAsync(int keyCode, bool isKeyDown, bool isExtended = false)
    {
        if (!_sessionService.IsTerhubung || !_sessionService.IzinKontrol) return;

        var input = InputKeyboard.Create(keyCode, isKeyDown, isExtended);
        var paket = _protocolService.CreateInputKeyboard(_sessionService.KunciSesi!, input);
        await SendPacketAsync(paket);
    }

    /// <summary>
    /// Mengirim input mouse move ke Host (dengan throttling)
    /// </summary>
    public async Task SendMouseMoveAsync(double normalizedX, double normalizedY)
    {
        if (!_sessionService.IsTerhubung || !_sessionService.IzinKontrol) return;

        // Throttle mouse move
        var now = DateTime.UtcNow;
        if ((now - _lastMouseMove).TotalMilliseconds < MOUSE_THROTTLE_MS)
            return;

        _lastMouseMove = now;

        var input = InputMouse.CreateMove(normalizedX, normalizedY);
        var paket = _protocolService.CreateInputMouse(_sessionService.KunciSesi!, input);
        await SendPacketAsync(paket);
    }

    /// <summary>
    /// Mengirim input mouse click ke Host
    /// </summary>
    public async Task SendMouseClickAsync(double normalizedX, double normalizedY, TombolMouse button, bool isDown)
    {
        if (!_sessionService.IsTerhubung || !_sessionService.IzinKontrol) return;

        var input = InputMouse.CreateClick(normalizedX, normalizedY, button, isDown);
        var paket = _protocolService.CreateInputMouse(_sessionService.KunciSesi!, input);
        await SendPacketAsync(paket);
    }

    /// <summary>
    /// Mengirim input mouse wheel ke Host
    /// </summary>
    public async Task SendMouseWheelAsync(double normalizedX, double normalizedY, int delta)
    {
        if (!_sessionService.IsTerhubung || !_sessionService.IzinKontrol) return;

        var input = InputMouse.CreateWheel(normalizedX, normalizedY, delta);
        var paket = _protocolService.CreateInputMouse(_sessionService.KunciSesi!, input);
        await SendPacketAsync(paket);
    }

    #endregion

    #region Packet I/O

    /// <summary>
    /// Mengirim paket ke Host
    /// </summary>
    private async Task SendPacketAsync(PaketData paket)
    {
        System.Diagnostics.Debug.WriteLine($"[SEND] SendPacketAsync called. TipePaket={paket.TipePaket}");
        System.Diagnostics.Debug.WriteLine($"[SEND] _networkStream={(_networkStream != null ? "OK" : "NULL")}, CanWrite={(_networkStream?.CanWrite ?? false)}");

        if (_networkStream == null)
        {
            System.Diagnostics.Debug.WriteLine("[SEND] ERROR: _networkStream is NULL!");
            return;
        }

        if (!_networkStream.CanWrite)
        {
            System.Diagnostics.Debug.WriteLine("[SEND] ERROR: _networkStream.CanWrite is FALSE!");
            return;
        }

        try
        {
            var json = _protocolService.SerializePaket(paket);
            System.Diagnostics.Debug.WriteLine($"[SEND] JSON length={json?.Length ?? 0}");

            if (string.IsNullOrEmpty(json))
            {
                System.Diagnostics.Debug.WriteLine("[SEND] ERROR: JSON is empty!");
                return;
            }

            var data = Encoding.UTF8.GetBytes(json); // Tanpa newline - sesuai protokol Host
            System.Diagnostics.Debug.WriteLine($"[SEND] Writing {data.Length} bytes...");

            await _networkStream.WriteAsync(data);
            await _networkStream.FlushAsync();

            System.Diagnostics.Debug.WriteLine($"[SEND] SUCCESS! TipePaket={paket.TipePaket}, Size={data.Length}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[SEND] EXCEPTION: {ex.GetType().Name}: {ex.Message}");
        }
    }

    /// <summary>
    /// Mengirim paket secara synchronous (untuk disconnect)
    /// </summary>
    private void SendPacketSync(PaketData paket)
    {
        if (_networkStream == null || !_networkStream.CanWrite) return;

        try
        {
            var json = _protocolService.SerializePaket(paket);
            var data = Encoding.UTF8.GetBytes(json); // Tanpa newline

            _networkStream.Write(data);
            _networkStream.Flush();
        }
        catch
        {
            // Ignore
        }
    }

    /// <summary>
    /// Menerima paket dari Host (tanpa newline delimiter - sesuai protokol Host)
    /// </summary>
    private async Task<PaketData?> ReceivePacketAsync(CancellationToken cancellationToken)
    {
        if (_networkStream == null || !_networkStream.CanRead) return null;

        try
        {
            var buffer = new byte[1024 * 64]; // 64KB buffer
            var sb = new StringBuilder();

            System.Diagnostics.Debug.WriteLine("[RECV] ReceivePacketAsync started, waiting for data...");

            while (!cancellationToken.IsCancellationRequested)
            {
                var bytesRead = await _networkStream.ReadAsync(buffer, cancellationToken);
                System.Diagnostics.Debug.WriteLine($"[RECV] ReadAsync returned: {bytesRead} bytes");

                if (bytesRead == 0)
                {
                    // Connection closed
                    System.Diagnostics.Debug.WriteLine("[RECV] Connection closed (0 bytes)");
                    return null;
                }

                sb.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                var data = sb.ToString();

                System.Diagnostics.Debug.WriteLine($"[RECV] Buffer total: {data.Length} chars, first 300: {data.Substring(0, Math.Min(300, data.Length))}");

                // Gunakan FindCompleteJsonEnd untuk konsistensi dengan StartReceiveLoop
                int endIndex = FindCompleteJsonEnd(data);
                if (endIndex >= 0)
                {
                    var json = data.Substring(0, endIndex + 1);
                    System.Diagnostics.Debug.WriteLine($"[RECV] Found complete JSON, length={json.Length}");

                    var paket = _protocolService.DeserializePaket(json);
                    if (paket != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"[RECV] SUCCESS! TipePaket={paket.TipePaket}, IdSesi={paket.IdSesi?.Substring(0, Math.Min(8, paket.IdSesi?.Length ?? 0))}...");
                        return paket;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"[RECV] DeserializePaket returned null!");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[RECV] No complete JSON yet, waiting for more data...");
                }

                // Jika JSON belum lengkap, lanjut baca
            }
        }
        catch (OperationCanceledException)
        {
            System.Diagnostics.Debug.WriteLine("[RECV] Cancelled");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[RECV] Exception: {ex.GetType().Name}: {ex.Message}");
        }

        return null;
    }

    #endregion

    #region Background Tasks

    /// <summary>
    /// Memulai loop penerima paket (tanpa newline delimiter - sesuai protokol Host)
    /// </summary>
    private void StartReceiveLoop()
    {
        _receiveTask = Task.Run(async () =>
        {
            // Buffer besar untuk frame layar (bisa ratusan KB)
            var buffer = new byte[1024 * 1024 * 2]; // 2MB buffer
            var accumulatedData = new MemoryStream();

            while (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    if (_networkStream == null || !_networkStream.CanRead) break;

                    var bytesRead = await _networkStream.ReadAsync(buffer, _cancellationTokenSource.Token);
                    if (bytesRead == 0)
                    {
                        // Connection closed
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Disconnect();
                            RaiseConnectionStatus(StatusKoneksi.TERPUTUS);
                        });
                        break;
                    }

                    // Tambahkan data ke accumulator
                    accumulatedData.Write(buffer, 0, bytesRead);

                    // Coba ekstrak dan proses paket JSON yang lengkap
                    ProcessAccumulatedData(accumulatedData);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Receive loop error: {ex.Message}");
                    // Jangan break, coba lanjutkan
                    await Task.Delay(100);
                }
            }

            accumulatedData.Dispose();
        });
    }

    /// <summary>
    /// Memproses data yang terakumulasi, ekstrak JSON packets yang lengkap
    /// </summary>
    private void ProcessAccumulatedData(MemoryStream accumulatedData)
    {
        while (accumulatedData.Length > 0)
        {
            var data = Encoding.UTF8.GetString(accumulatedData.ToArray());
            if (string.IsNullOrEmpty(data)) break;

            // Cari JSON object lengkap dengan tracking bracket di luar string
            int endIndex = FindCompleteJsonEnd(data);
            if (endIndex < 0)
            {
                // Belum ada JSON lengkap, tunggu data berikutnya
                break;
            }

            // Ekstrak JSON dan proses
            var json = data.Substring(0, endIndex + 1);
            var paket = _protocolService.DeserializePaket(json);

            if (paket != null)
            {
                System.Diagnostics.Debug.WriteLine($"[RECV-LOOP] TipePaket={paket.TipePaket}, Size={json.Length}");
                ProcessPacket(paket);
            }

            // Hapus data yang sudah diproses dari accumulator
            var remaining = data.Substring(endIndex + 1);
            accumulatedData.SetLength(0);
            if (!string.IsNullOrEmpty(remaining))
            {
                var remainingBytes = Encoding.UTF8.GetBytes(remaining);
                accumulatedData.Write(remainingBytes, 0, remainingBytes.Length);
            }
        }

        // Batasi ukuran buffer untuk mencegah memory leak
        if (accumulatedData.Length > 1024 * 1024 * 10) // 10MB limit
        {
            System.Diagnostics.Debug.WriteLine("[WARN] Buffer overflow, clearing...");
            accumulatedData.SetLength(0);
        }
    }

    /// <summary>
    /// Mencari posisi akhir JSON object yang lengkap (tracking string escapes)
    /// </summary>
    private static int FindCompleteJsonEnd(string data)
    {
        int braceCount = 0;
        bool inString = false;
        bool escape = false;

        for (int i = 0; i < data.Length; i++)
        {
            char c = data[i];

            if (escape)
            {
                escape = false;
                continue;
            }

            if (c == '\\' && inString)
            {
                escape = true;
                continue;
            }

            if (c == '"')
            {
                inString = !inString;
                continue;
            }

            if (inString) continue;

            if (c == '{')
            {
                braceCount++;
            }
            else if (c == '}')
            {
                braceCount--;
                if (braceCount == 0)
                {
                    return i; // Found complete JSON object
                }
            }
        }

        return -1; // No complete JSON found
    }

    /// <summary>
    /// Memproses paket yang diterima
    /// </summary>
    private void ProcessPacket(PaketData paket)
    {
        switch ((TipePaket)paket.TipePaket)
        {
            case TipePaket.FRAME_LAYAR:
                var frame = _protocolService.ParseFrameLayar(paket);
                if (frame != null)
                {
                    FrameReceived?.Invoke(this, frame);
                }
                break;

            case TipePaket.HEARTBEAT:
                // Respond to heartbeat (optional)
                break;

            case TipePaket.TUTUP_KONEKSI:
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Disconnect();
                    RaiseConnectionStatus(StatusKoneksi.TERPUTUS);
                });
                break;
        }
    }

    /// <summary>
    /// Memulai heartbeat task
    /// </summary>
    private void StartHeartbeat()
    {
        _heartbeatTask = Task.Run(async () =>
        {
            while (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(HEARTBEAT_INTERVAL_MS, _cancellationTokenSource.Token);

                    if (_sessionService.IsTerhubung)
                    {
                        var paket = _protocolService.CreateHeartbeat(_sessionService.KunciSesi!);
                        await SendPacketAsync(paket);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch
                {
                    // Ignore heartbeat errors
                }
            }
        });
    }

    #endregion

    #region Helpers

    private void RaiseConnectionStatus(StatusKoneksi status)
    {
        ConnectionStatusChanged?.Invoke(this, status);
    }

    public SessionService Session => _sessionService;

    public void Dispose()
    {
        Disconnect();
    }

    #endregion
}
