Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Threading.Tasks
Imports bcomm

''' <summary>
''' Modul untuk manajemen koneksi TCP antara Host dan Tamu.
''' </summary>
Public Module mdl_KoneksiJaringan

#Region "Events"

    ''' <summary>Event ketika ada permintaan koneksi masuk (untuk Host)</summary>
    Public Event PermintaanKoneksiMasuk(permintaan As cls_PayloadPermintaanKoneksi, clientSocket As TcpClient)

    ''' <summary>Event ketika koneksi berhasil dibuat</summary>
    Public Event KoneksiBerhasil(kunciSesi As String)

    ''' <summary>Event ketika koneksi ditolak</summary>
    Public Event KoneksiDitolak(pesan As String)

    ''' <summary>Event ketika koneksi terputus</summary>
    Public Event KoneksiTerputus(alasan As String)

    ''' <summary>Event ketika menerima paket data</summary>
    Public Event PaketDiterima(paket As cls_PaketData)

    ''' <summary>Event ketika terjadi error</summary>
    Public Event ErrorKoneksi(pesan As String)

#End Region

#Region "Private Variables"

    Private _tcpListener As TcpListener
    Private _tcpClient As TcpClient
    Private _networkStream As NetworkStream
    Private _sedangMendengarkan As Boolean = False
    Private _terhubung As Boolean = False
    Private _cancellationTokenSource As CancellationTokenSource
    Private _clientTerhubung As TcpClient

#End Region

#Region "Properties"

    ''' <summary>Status koneksi saat ini</summary>
    Public ReadOnly Property Terhubung As Boolean
        Get
            Return _terhubung
        End Get
    End Property

    ''' <summary>Status server (mendengarkan)</summary>
    Public ReadOnly Property ServerAktif As Boolean
        Get
            Return _sedangMendengarkan
        End Get
    End Property

#End Region

#Region "Host Mode - Server TCP"

    ''' <summary>
    ''' Mulai server TCP untuk menerima koneksi (mode Host).
    ''' </summary>
    Public Sub MulaiServer()
        If _sedangMendengarkan Then Return

        Try
            _cancellationTokenSource = New CancellationTokenSource()
            _tcpListener = New TcpListener(IPAddress.Any, PortKoneksiAktif)
            _tcpListener.Start()
            _sedangMendengarkan = True

            ' Mulai task untuk menerima koneksi
            Task.Run(Async Function()
                         Await TerimaKoneksiAsync(_cancellationTokenSource.Token)
                     End Function)

        Catch ex As Exception
            RaiseEvent ErrorKoneksi($"Gagal memulai server: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Task async untuk menerima koneksi masuk.
    ''' </summary>
    Private Async Function TerimaKoneksiAsync(token As CancellationToken) As Task
        While _sedangMendengarkan AndAlso Not token.IsCancellationRequested
            Try
                ' Tunggu koneksi masuk
                Dim client = Await _tcpListener.AcceptTcpClientAsync()

                ' Proses koneksi di task terpisah
                Await Task.Run(Async Function()
                                   Await ProsesKoneksiMasukAsync(client, token)
                               End Function)

            Catch ex As ObjectDisposedException
                Exit While
            Catch ex As SocketException
                If Not token.IsCancellationRequested Then
                    RaiseEvent ErrorKoneksi($"Error saat menerima koneksi: {ex.Message}")
                End If
            Catch ex As Exception
                If Not token.IsCancellationRequested Then
                    RaiseEvent ErrorKoneksi($"Error: {ex.Message}")
                End If
            End Try
        End While
    End Function

    ''' <summary>
    ''' Proses koneksi masuk dari Tamu.
    ''' </summary>
    Private Async Function ProsesKoneksiMasukAsync(client As TcpClient, token As CancellationToken) As Task
        Try
            Dim stream = client.GetStream()
            stream.ReadTimeout = TIMEOUT_KONEKSI

            ' Baca paket permintaan koneksi
            Dim buffer(8191) As Byte
            Dim bytesRead = Await stream.ReadAsync(buffer, 0, buffer.Length)

            System.Diagnostics.Debug.WriteLine($"[DEBUG] ProsesKoneksiMasukAsync: bytesRead={bytesRead}")

            If bytesRead > 0 Then
                Dim json = BytesKeString(buffer, 0, bytesRead)
                System.Diagnostics.Debug.WriteLine($"[DEBUG] JSON diterima: {json.Substring(0, Math.Min(500, json.Length))}...")

                Dim paket = DeserializePaket(json)
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Paket deserialize: {If(paket IsNot Nothing, "OK", "NULL")}")

                If paket IsNot Nothing Then
                    System.Diagnostics.Debug.WriteLine($"[DEBUG] TipePaket={paket.TipePaket} (expected={TipePaket.PERMINTAAN_KONEKSI})")

                    If paket.TipePaket = TipePaket.PERMINTAAN_KONEKSI Then
                        Dim permintaan = DeserializePermintaanKoneksi(paket.Payload)
                        System.Diagnostics.Debug.WriteLine($"[DEBUG] Permintaan deserialize: {If(permintaan IsNot Nothing, "OK - " & permintaan.NamaPerangkat, "NULL")}")

                        If permintaan IsNot Nothing Then
                            ' Raise event untuk dialog persetujuan
                            System.Diagnostics.Debug.WriteLine("[DEBUG] RaiseEvent PermintaanKoneksiMasuk...")
                            RaiseEvent PermintaanKoneksiMasuk(permintaan, client)
                        Else
                            System.Diagnostics.Debug.WriteLine($"[DEBUG] GAGAL deserialize permintaan. Payload: {paket.Payload}")
                        End If
                    Else
                        System.Diagnostics.Debug.WriteLine($"[DEBUG] TipePaket tidak match! Got={CInt(paket.TipePaket)}, Expected={CInt(TipePaket.PERMINTAAN_KONEKSI)}")
                    End If
                Else
                    System.Diagnostics.Debug.WriteLine("[DEBUG] GAGAL deserialize paket!")
                End If
            Else
                System.Diagnostics.Debug.WriteLine("[DEBUG] bytesRead = 0, tidak ada data!")
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"[DEBUG] EXCEPTION: {ex.Message}")
            RaiseEvent ErrorKoneksi($"Error memproses koneksi masuk: {ex.Message}")
            Try
                client.Close()
            Catch
            End Try
        End Try
    End Function

    ''' <summary>
    ''' Kirim respon koneksi ke Tamu (dipanggil setelah user menerima/menolak).
    ''' </summary>
    Public Async Function KirimResponKoneksiAsync(client As TcpClient, hasil As HasilPersetujuan,
                                                   Optional izinKontrol As Boolean = True,
                                                   Optional izinTransfer As Boolean = False,
                                                   Optional izinClipboard As Boolean = False,
                                                   Optional pesan As String = "",
                                                   Optional supportedCodecs As String() = Nothing) As Task(Of Boolean)
        Try
            Dim stream = client.GetStream()
            Dim kunciSesi As String = ""

            If hasil = HasilPersetujuan.DITERIMA Then
                ' Generate kunci sesi
                kunciSesi = AcakKarakter(32)
                KunciSesiAktif = kunciSesi
                _clientTerhubung = client
                _terhubung = True
                StatusKoneksiSaatIni = StatusKoneksi.TERHUBUNG
            End If

            ' Tentukan codec yang akan digunakan
            Dim selectedCodec = TentukanCodecStreaming(supportedCodecs)

            ' Buat dan kirim paket respon
            Dim paketRespon = BuatPaketResponKoneksi(hasil, kunciSesi, pesan, izinKontrol, izinTransfer, izinClipboard, selectedCodec)
            Dim data = StringKeBytes(SerializePaket(paketRespon))
            Await stream.WriteAsync(data, 0, data.Length)

            If hasil = HasilPersetujuan.DITERIMA Then
                RaiseEvent KoneksiBerhasil(kunciSesi)
                ' Mulai listening untuk paket data
                Task.Run(Async Function()
                             Await DengarkanPaketAsync(client, _cancellationTokenSource.Token)
                         End Function)
            Else
                client.Close()
            End If

            Return True

        Catch ex As Exception
            RaiseEvent ErrorKoneksi($"Gagal mengirim respon: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Tentukan codec streaming berdasarkan ketersediaan FFmpeg dan codec yang didukung client.
    ''' </summary>
    ''' <param name="clientSupportedCodecs">Daftar codec yang didukung client</param>
    ''' <returns>Codec yang dipilih ("H264" atau "JPEG")</returns>
    Public Function TentukanCodecStreaming(clientSupportedCodecs As String()) As String
        ' Default: JPEG (selalu didukung)
        Dim selectedCodec = "JPEG"

        ' Jika client tidak mengirim informasi codec, gunakan JPEG (backward compatible)
        If clientSupportedCodecs Is Nothing OrElse clientSupportedCodecs.Length = 0 Then
            System.Diagnostics.Debug.WriteLine("[CODEC] Client tidak kirim supportedCodecs, using JPEG")
            mdl_UdpStreaming.CodecStreaming = TipeKodek.JPEG
            Return selectedCodec
        End If

        ' Cek apakah client mendukung H.264
        Dim clientSupportsH264 = clientSupportedCodecs.Any(Function(c) c.Equals("H264", StringComparison.OrdinalIgnoreCase))

        ' Cek apakah FFmpeg tersedia di Host
        Dim ffmpegTersedia = mdl_FFmpegManager.FFmpegTersedia

        System.Diagnostics.Debug.WriteLine($"[CODEC] Client supports H264: {clientSupportsH264}, FFmpeg available: {ffmpegTersedia}")

        ' Pilih H.264 jika client support DAN FFmpeg tersedia
        If clientSupportsH264 AndAlso ffmpegTersedia Then
            selectedCodec = "H264"
            mdl_UdpStreaming.CodecStreaming = TipeKodek.H264
            System.Diagnostics.Debug.WriteLine("[CODEC] Selected: H.264")
        Else
            mdl_UdpStreaming.CodecStreaming = TipeKodek.JPEG
            System.Diagnostics.Debug.WriteLine("[CODEC] Selected: JPEG")
        End If

        Return selectedCodec
    End Function

#End Region

#Region "Tamu Mode - Client TCP"

    ''' <summary>
    ''' Sambungkan ke Host (mode Tamu).
    ''' </summary>
    Public Async Function SambungKeHostAsync(alamatIP As String, port As Integer) As Task(Of Boolean)
        Try
            _cancellationTokenSource = New CancellationTokenSource()
            _tcpClient = New TcpClient()
            _tcpClient.ReceiveTimeout = TIMEOUT_KONEKSI
            _tcpClient.SendTimeout = TIMEOUT_KONEKSI

            ' Koneksi ke server
            Await _tcpClient.ConnectAsync(alamatIP, port)
            _networkStream = _tcpClient.GetStream()

            ' Kirim permintaan koneksi (dengan daftar codec yang didukung)
            Dim supportedCodecs = {"JPEG", "H264"}
            Dim paket = BuatPaketPermintaanKoneksi(NamaPerangkatIni, AlamatIPLokal, supportedCodecs)
            Dim data = StringKeBytes(SerializePaket(paket))
            Await _networkStream.WriteAsync(data, 0, data.Length)

            StatusKoneksiSaatIni = StatusKoneksi.MENUNGGU_PERSETUJUAN

            ' Tunggu respon dari Host
            Dim buffer(8191) As Byte
            Dim bytesRead = Await _networkStream.ReadAsync(buffer, 0, buffer.Length)

            If bytesRead > 0 Then
                Dim json = BytesKeString(buffer, 0, bytesRead)
                Dim paketRespon = DeserializePaket(json)

                If paketRespon IsNot Nothing AndAlso paketRespon.TipePaket = TipePaket.RESPON_KONEKSI Then
                    Dim respon = DeserializeResponKoneksi(paketRespon.Payload)

                    If respon IsNot Nothing Then
                        If respon.Hasil = HasilPersetujuan.DITERIMA Then
                            KunciSesiAktif = respon.KunciSesi
                            _terhubung = True
                            StatusKoneksiSaatIni = StatusKoneksi.TERHUBUNG
                            RaiseEvent KoneksiBerhasil(respon.KunciSesi)

                            ' Mulai listening untuk paket data
                            Task.Run(Async Function()
                                         Await DengarkanPaketAsync(_tcpClient, _cancellationTokenSource.Token)
                                     End Function)

                            Return True
                        Else
                            StatusKoneksiSaatIni = StatusKoneksi.TIDAK_TERHUBUNG
                            RaiseEvent KoneksiDitolak(respon.Pesan)
                            _tcpClient.Close()
                            Return False
                        End If
                    End If
                End If
            End If

            StatusKoneksiSaatIni = StatusKoneksi.TIDAK_TERHUBUNG
            _tcpClient.Close()
            Return False

        Catch ex As Exception
            StatusKoneksiSaatIni = StatusKoneksi.TIDAK_TERHUBUNG
            RaiseEvent ErrorKoneksi($"Gagal menyambung: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

#Region "Komunikasi Data"

    ''' <summary>
    ''' Dengarkan paket data dari koneksi yang terhubung.
    ''' </summary>
    Private Async Function DengarkanPaketAsync(client As TcpClient, token As CancellationToken) As Task
        Try
            Dim stream = client.GetStream()
            Dim buffer(65535) As Byte

            While _terhubung AndAlso Not token.IsCancellationRequested
                Try
                    Dim bytesRead = Await stream.ReadAsync(buffer, 0, buffer.Length)

                    If bytesRead = 0 Then
                        ' Koneksi ditutup oleh peer
                        Exit While
                    End If

                    Dim json = BytesKeString(buffer, 0, bytesRead)
                    Dim paket = DeserializePaket(json)

                    If paket IsNot Nothing Then
                        Select Case paket.TipePaket
                            Case TipePaket.TUTUP_KONEKSI
                                Exit While

                            Case TipePaket.HEARTBEAT
                                ' Respond heartbeat
                                Await KirimPaketAsync(BuatPaketHeartbeat(paket.IdSesi))

                            Case TipePaket.PERMINTAAN_STREAMING
                                ' Tamu minta mulai streaming (Host side)
                                System.Diagnostics.Debug.WriteLine($"[DEBUG] PERMINTAAN_STREAMING diterima. Mode={ModeAplikasiSaatIni}, _terhubung={_terhubung}")
                                If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                                    System.Diagnostics.Debug.WriteLine("[DEBUG] Memulai MulaiStreamingLayarAsync...")
                                    Task.Run(Async Function()
                                                 Await MulaiStreamingLayarAsync()
                                             End Function)
                                Else
                                    System.Diagnostics.Debug.WriteLine($"[DEBUG] SKIP: Mode bukan HOST ({ModeAplikasiSaatIni})")
                                End If

                            Case TipePaket.HENTIKAN_STREAMING
                                ' Tamu minta stop streaming (Host side)
                                If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                                    HentikanStreamingLayar()
                                End If

                            ' === FASE 2b: Handle Input Keyboard/Mouse dari Tamu ===

                            Case TipePaket.INPUT_KEYBOARD
                                ' Proses input keyboard dari Tamu (Host side only)
                                If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                                    ProsesInputKeyboard(paket.Payload)
                                End If

                            Case TipePaket.INPUT_MOUSE
                                ' Proses input mouse dari Tamu (Host side only)
                                If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                                    ProsesInputMouseDariPaket(paket.Payload)
                                End If

                            Case Else
                                RaiseEvent PaketDiterima(paket)
                        End Select
                    End If

                Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is SocketException
                    Exit While
                End Try
            End While

        Catch ex As Exception
            RaiseEvent ErrorKoneksi($"Error saat mendengarkan: {ex.Message}")
        Finally
            Putuskan("Koneksi terputus")
        End Try
    End Function

    ''' <summary>
    ''' Kirim paket data ke peer yang terhubung.
    ''' </summary>
    Public Async Function KirimPaketAsync(paket As cls_PaketData) As Task(Of Boolean)
        Try
            Dim stream As NetworkStream = Nothing

            If ModeAplikasiSaatIni = ModeAplikasi.HOST AndAlso _clientTerhubung IsNot Nothing Then
                stream = _clientTerhubung.GetStream()
            ElseIf ModeAplikasiSaatIni = ModeAplikasi.TAMU AndAlso _tcpClient IsNot Nothing Then
                stream = _tcpClient.GetStream()
            End If

            If stream IsNot Nothing Then
                paket.IdSesi = KunciSesiAktif
                Dim data = StringKeBytes(SerializePaket(paket))
                Await stream.WriteAsync(data, 0, data.Length)
                Return True
            End If

            Return False

        Catch ex As Exception
            RaiseEvent ErrorKoneksi($"Gagal mengirim paket: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

#Region "Streaming Layar (Host)"

    ''' <summary>Token untuk membatalkan streaming loop</summary>
    Private _streamingCancellationToken As CancellationTokenSource

    ''' <summary>Flag apakah sedang streaming</summary>
    Private _sedangStreaming As Boolean = False

    ''' <summary>
    ''' Mulai streaming layar ke Tamu (dipanggil oleh Host).
    ''' Menggunakan UDP untuk pengiriman frame (TCP head-of-line blocking issue solved).
    ''' FPS diambil dari TargetFPSAktif (settings).
    ''' </summary>
    Public Async Function MulaiStreamingLayarAsync(Optional skala As Double = 0.4) As Task
        Dim targetFPS As Integer = TargetFPSAktif
        System.Diagnostics.Debug.WriteLine($"[DEBUG] MulaiStreamingLayarAsync dipanggil. _sedangStreaming={_sedangStreaming}, _terhubung={_terhubung}")

        If _sedangStreaming Then
            System.Diagnostics.Debug.WriteLine("[DEBUG] RETURN: Sudah sedang streaming")
            Return
        End If
        If Not _terhubung Then
            System.Diagnostics.Debug.WriteLine("[DEBUG] RETURN: Tidak terhubung")
            Return
        End If

        System.Diagnostics.Debug.WriteLine("[DEBUG] Memulai streaming loop dengan UDP...")
        _sedangStreaming = True
        _streamingCancellationToken = New CancellationTokenSource()

        ' Inisialisasi sesi remote
        If SesiRemoteAktif Is Nothing Then
            SesiRemoteAktif = New cls_SesiRemote()
        End If
        SesiRemoteAktif.MulaiStreaming()
        SesiRemoteAktif.SkalaGambar = skala
        SesiRemoteAktif.TargetFPS = targetFPS

        ' Reset nomor frame
        mdl_TangkapLayar.ResetNomorFrame()

        ' Mulai UDP sender untuk streaming frame
        ' Untuk LAN: kirim langsung ke IP Tamu
        ' Untuk Internet: kirim via Relay server (UDP)
        ' PENTING: SessionId untuk UDP routing:
        '   - Mode LAN: hash dari KunciSesiAktif
        '   - Mode Internet: hash dari IdSesiRelay (sama dengan yang digunakan relay server)
        Dim sessionId As Integer
        If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
            ' Gunakan IdSesiRelay agar cocok dengan UdpSessionId di relay server
            sessionId = mdl_UdpStreaming.GenerateSessionId(IdSesiRelay)
            System.Diagnostics.Debug.WriteLine($"[UDP-HOST] Using IdSesiRelay for SessionId: {IdSesiRelay} -> {sessionId}")
        Else
            sessionId = mdl_UdpStreaming.GenerateSessionId(KunciSesiAktif)
        End If

        If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
            ' Mode Internet: UDP via Relay (endpoint sudah di-setup saat connect ke relay)
            mdl_UdpStreaming.MulaiUdpSender(RelayServerIPAktif, PortUdpVideoAktif, sessionId)
            System.Diagnostics.Debug.WriteLine($"[UDP-HOST] UDP sender dimulai via Relay {RelayServerIPAktif}:{PortUdpVideoAktif}, SessionId={sessionId}")
        Else
            ' Mode LAN: kirim langsung ke IP Tamu
            Dim tamuIP = DapatkanIPTamuTerhubung()
            If Not String.IsNullOrEmpty(tamuIP) Then
                mdl_UdpStreaming.MulaiUdpSender(tamuIP, PortUdpVideoAktif, sessionId)
                System.Diagnostics.Debug.WriteLine($"[UDP-HOST] UDP sender dimulai ke {tamuIP}:{PortUdpVideoAktif}")
            Else
                System.Diagnostics.Debug.WriteLine("[UDP-HOST] Gagal mendapatkan IP Tamu, fallback ke TCP")
            End If
        End If

        ' === H.264 Encoder: Mulai jika codec negosiasi adalah H264 ===
        Dim useH264 As Boolean = False
        If mdl_UdpStreaming.CodecStreaming = TipeKodek.H264 Then
            ' Dapatkan ukuran frame untuk encoder
            Dim bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds
            Dim encoderWidth = CInt(bounds.Width * skala)
            Dim encoderHeight = CInt(bounds.Height * skala)

            ' Mulai H.264 encoder
            useH264 = mdl_UdpStreaming.MulaiH264Encoder(encoderWidth, encoderHeight, targetFPS)
            If useH264 Then
                System.Diagnostics.Debug.WriteLine($"[H264] Encoder started: {encoderWidth}x{encoderHeight} @ {targetFPS}fps")
            Else
                System.Diagnostics.Debug.WriteLine("[H264] Encoder failed to start, fallback to JPEG")
            End If
        End If

        Dim targetDelayMs = SesiRemoteAktif.IntervalDelayMs()
        Dim token = _streamingCancellationToken.Token
        Dim useUdp = mdl_UdpStreaming.IsUdpStreamingActive()

        Dim frameCount As Integer = 0
        Try
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Masuk streaming loop, UDP={useUdp}, H264={useH264}, targetFPS={SesiRemoteAktif.TargetFPS}, targetDelay={targetDelayMs}ms")
            While _sedangStreaming AndAlso _terhubung AndAlso Not token.IsCancellationRequested
                Dim perluDelayError As Boolean = False
                Dim stopwatch = System.Diagnostics.Stopwatch.StartNew()

                Try
                    frameCount += 1

                    ' === PATH H.264: Capture BGRA dan kirim ke encoder ===
                    If useH264 AndAlso mdl_UdpStreaming.H264EncoderAktif Then
                        ' Capture layar langsung ke BGRA untuk H.264 encoder
                        Dim frameWidth As Integer = 0
                        Dim frameHeight As Integer = 0
                        Dim bgraData = mdl_TangkapLayar.TangkapLayarKeBgra(skala, frameWidth, frameHeight)

                        If bgraData IsNot Nothing AndAlso bgraData.Length > 0 Then
                            ' Kirim ke H.264 encoder (encoder akan trigger event DataReady -> kirim via UDP)
                            Await mdl_UdpStreaming.KirimFrameKeEncoderAsync(bgraData)

                            ' Update statistik (estimasi ukuran BGRA / 50 untuk approx H.264 size)
                            SesiRemoteAktif.CatatFrame(frameCount, bgraData.Length / 50000.0)

                            If frameCount Mod 50 = 1 Then
                                System.Diagnostics.Debug.WriteLine($"[H264] Frame #{frameCount} sent to encoder, BGRA={bgraData.Length / 1024:F0}KB, elapsed={stopwatch.ElapsedMilliseconds}ms")
                            End If
                        Else
                            System.Diagnostics.Debug.WriteLine("[H264] BGRA capture failed!")
                        End If

                    ' === PATH JPEG: Capture JPEG dan kirim langsung ===
                    Else
                        ' Tangkap frame sebagai JPEG
                        Dim frame = Await mdl_TangkapLayar.TangkapFrameAsync(skala)
                        If frame IsNot Nothing AndAlso frame.IsValid() Then

                            ' Kirim frame via UDP (preferred) atau TCP (fallback)
                            If useUdp AndAlso mdl_UdpStreaming.IsUdpStreamingActive() Then
                                ' UDP: Kirim raw JPEG bytes (lebih efisien)
                                Dim jpegData = frame.DapatkanJpegBytes()
                                If jpegData IsNot Nothing Then
                                    Await mdl_UdpStreaming.KirimFrameUdpAsync(jpegData)
                                End If
                            Else
                                ' TCP fallback: Kirim paket JSON (lama)
                                Dim paket = BuatPaketFrameLayar(frame)
                                Await KirimPaketAsync(paket)
                            End If

                            ' Update statistik
                            SesiRemoteAktif.CatatFrame(frame.NomorFrame, frame.UkuranDataKB())

                            If frameCount Mod 50 = 1 Then ' Log setiap 50 frame
                                System.Diagnostics.Debug.WriteLine($"[JPEG] Frame #{frameCount} terkirim via {If(useUdp, "UDP", "TCP")}, size={frame.UkuranDataKB():F1}KB, elapsed={stopwatch.ElapsedMilliseconds}ms")
                            End If
                        Else
                            System.Diagnostics.Debug.WriteLine("[JPEG] Frame null atau tidak valid!")
                        End If
                    End If

                    stopwatch.Stop()

                    ' Delay dinamis untuk target FPS (dikurangi waktu operasi)
                    Dim actualDelayMs = Math.Max(1, targetDelayMs - CInt(stopwatch.ElapsedMilliseconds))
                    Await Task.Delay(actualDelayMs, token)

                Catch ex As TaskCanceledException
                    Exit While
                Catch ex As Exception
                    ' Log error tapi lanjutkan streaming
                    System.Diagnostics.Debug.WriteLine($"Error streaming frame: {ex.Message}")
                    perluDelayError = True
                End Try

                ' Delay setelah error (di luar Catch karena VB.NET tidak support Await dalam Catch)
                If perluDelayError Then
                    Try
                        Await Task.Delay(100, token)
                    Catch
                        Exit While
                    End Try
                End If
            End While

        Finally
            _sedangStreaming = False
            mdl_UdpStreaming.HentikanUdpStreaming()
            SesiRemoteAktif?.HentikanStreaming()
        End Try
    End Function

    ''' <summary>
    ''' Mendapatkan IP address Tamu yang sedang terhubung (untuk LAN mode).
    ''' </summary>
    Private Function DapatkanIPTamuTerhubung() As String
        Try
            If _clientTerhubung IsNot Nothing AndAlso _clientTerhubung.Connected Then
                Dim endpoint = TryCast(_clientTerhubung.Client.RemoteEndPoint, IPEndPoint)
                If endpoint IsNot Nothing Then
                    Return endpoint.Address.ToString()
                End If
            End If
        Catch
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' Hentikan streaming layar.
    ''' </summary>
    Public Sub HentikanStreamingLayar()
        _sedangStreaming = False
        Try
            _streamingCancellationToken?.Cancel()
        Catch
        End Try
        SesiRemoteAktif?.HentikanStreaming()
    End Sub

    ''' <summary>
    ''' Apakah sedang streaming?
    ''' </summary>
    Public ReadOnly Property SedangStreaming As Boolean
        Get
            Return _sedangStreaming
        End Get
    End Property

#End Region

#Region "Disconnect & Cleanup"

    ''' <summary>
    ''' Putuskan koneksi.
    ''' </summary>
    Public Sub Putuskan(Optional alasan As String = "")
        If Not _terhubung AndAlso StatusKoneksiSaatIni = StatusKoneksi.TIDAK_TERHUBUNG Then Return

        ' Hentikan streaming jika sedang aktif
        HentikanStreamingLayar()

        _terhubung = False
        StatusKoneksiSaatIni = StatusKoneksi.TERPUTUS
        KunciSesiAktif = ""

        Try
            _networkStream?.Close()
        Catch
        End Try

        Try
            _tcpClient?.Close()
        Catch
        End Try

        Try
            _clientTerhubung?.Close()
        Catch
        End Try

        _networkStream = Nothing
        _tcpClient = Nothing
        _clientTerhubung = Nothing

        StatusKoneksiSaatIni = StatusKoneksi.TIDAK_TERHUBUNG
        RaiseEvent KoneksiTerputus(alasan)
    End Sub

    ''' <summary>
    ''' Hentikan server TCP.
    ''' </summary>
    Public Sub HentikanServer()
        _sedangMendengarkan = False

        Try
            _cancellationTokenSource?.Cancel()
        Catch
        End Try

        Try
            _tcpListener?.Stop()
        Catch
        End Try

        Putuskan("Server dihentikan")

        _tcpListener = Nothing
    End Sub

#End Region

#Region "Input Processing (Fase 2b - Host Side)"

    ''' <summary>
    ''' Proses input keyboard dari Tamu dan inject ke sistem Host.
    ''' </summary>
    Private Sub ProsesInputKeyboard(payload As String)
        Try
            Dim input = DeserializeInputKeyboard(payload)
            If input Is Nothing Then Return

            ' Inject keyboard event ke sistem
            mdl_InjeksiInput.InjeksiKeyboard(input.KeyCode, input.IsKeyDown, input.IsExtended)

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error proses input keyboard: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Proses input mouse dari Tamu dan inject ke sistem Host.
    ''' </summary>
    Private Sub ProsesInputMouseDariPaket(payload As String)
        Try
            Dim input = DeserializeInputMouse(payload)
            If input Is Nothing Then Return

            ' Gunakan fungsi helper di mdl_InjeksiInput
            mdl_InjeksiInput.ProsesInputMouse(input)

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error proses input mouse: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Relay Mode - Internet"

    ''' <summary>
    ''' Proses paket yang masuk via relay server (mode Internet).
    ''' Method ini dipanggil dari wpfWin_ModeHost ketika menerima paket dari relay.
    ''' </summary>
    ''' <param name="paket">Paket data dari relay</param>
    Public Sub ProsesPaketMasukViaRelay(paket As cls_PaketData)
        Try
            Select Case paket.TipePaket
                Case TipePaket.TUTUP_KONEKSI
                    ' Koneksi ditutup oleh Tamu
                    Putuskan("Koneksi ditutup oleh Tamu")

                Case TipePaket.HEARTBEAT
                    ' Respond heartbeat via relay
                    Task.Run(Async Function()
                                 Dim pktResp = BuatPaketHeartbeat(paket.IdSesi)
                                 Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(pktResp)
                             End Function)

                Case TipePaket.PERMINTAAN_STREAMING
                    ' Tamu minta mulai streaming via relay
                    System.Diagnostics.Debug.WriteLine($"[RELAY] PERMINTAAN_STREAMING diterima via relay")
                    If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                        ' Set flag terhubung agar streaming bisa jalan
                        _terhubung = True
                        StatusKoneksiSaatIni = StatusKoneksi.TERHUBUNG

                        Task.Run(Async Function()
                                     Await MulaiStreamingLayarViaRelayAsync()
                                 End Function)
                    End If

                Case TipePaket.HENTIKAN_STREAMING
                    ' Tamu minta stop streaming
                    If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                        HentikanStreamingLayar()
                    End If

                Case TipePaket.INPUT_KEYBOARD
                    ' Proses input keyboard dari Tamu
                    If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                        ProsesInputKeyboard(paket.Payload)
                    End If

                Case TipePaket.INPUT_MOUSE
                    ' Proses input mouse dari Tamu
                    If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                        ProsesInputMouseDariPaket(paket.Payload)
                    End If

                Case Else
                    RaiseEvent PaketDiterima(paket)
            End Select

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"[RELAY] Error proses paket: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Mulai streaming layar ke Tamu via relay server.
    ''' Menggunakan UDP untuk video streaming (port 45681) dan TCP untuk control (port 45680).
    ''' </summary>
    Private Async Function MulaiStreamingLayarViaRelayAsync() As Task
        If SesiRemoteAktif Is Nothing Then
            SesiRemoteAktif = New cls_SesiRemote()
        End If

        SesiRemoteAktif.MulaiStreaming()
        SesiRemoteAktif.TargetFPS = TargetFPSAktif
        SesiRemoteAktif.SkalaGambar = 0.35

        System.Diagnostics.Debug.WriteLine($"[RELAY] Streaming layar dimulai dengan FPS={TargetFPSAktif}")

        ' Setup UDP sender untuk streaming via relay
        ' SessionId harus sama dengan yang digunakan relay server (hash dari IdSesiRelay)
        Dim sessionId = mdl_UdpStreaming.GenerateSessionId(IdSesiRelay)
        mdl_UdpStreaming.MulaiUdpSender(RelayServerIPAktif, PortUdpVideoAktif, sessionId)
        mdl_UdpStreaming.SetupRelayUdpEndpoint(RelayServerIPAktif, PortUdpVideoAktif)
        Debug.WriteLine($"[RELAY-UDP] UDP sender dimulai ke {RelayServerIPAktif}:{PortUdpVideoAktif}, SessionId={sessionId} (dari IdSesi={IdSesiRelay})")

        ' === H.264 Encoder: Mulai jika codec negosiasi adalah H264 ===
        Dim useH264 As Boolean = False
        If mdl_UdpStreaming.CodecStreaming = TipeKodek.H264 Then
            Dim bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds
            Dim encoderWidth = CInt(bounds.Width * SesiRemoteAktif.SkalaGambar)
            Dim encoderHeight = CInt(bounds.Height * SesiRemoteAktif.SkalaGambar)

            useH264 = mdl_UdpStreaming.MulaiH264Encoder(encoderWidth, encoderHeight, TargetFPSAktif)
            If useH264 Then
                Debug.WriteLine($"[RELAY-H264] Encoder started: {encoderWidth}x{encoderHeight} @ {TargetFPSAktif}fps")
            Else
                Debug.WriteLine("[RELAY-H264] Encoder failed to start, fallback to JPEG")
            End If
        End If

        Dim useUdp = mdl_UdpStreaming.IsUdpStreamingActive()
        Debug.WriteLine($"[RELAY-UDP] UDP aktif = {useUdp}, H264 = {useH264}")

        Dim frameCount = 0

        Try
            Debug.WriteLine($"[RELAY-STREAM] Loop dimulai. IsStreamingAktif={SesiRemoteAktif.IsStreamingAktif()}, TerhubungKeRelay={TerhubungKeRelay}, UseUDP={useUdp}, H264={useH264}")

            While SesiRemoteAktif.IsStreamingAktif() AndAlso TerhubungKeRelay

                frameCount += 1
                Dim stopwatch = System.Diagnostics.Stopwatch.StartNew()

                ' === PATH H.264: Capture BGRA dan kirim ke encoder ===
                If useH264 AndAlso mdl_UdpStreaming.H264EncoderAktif Then
                    Dim frameWidth As Integer = 0
                    Dim frameHeight As Integer = 0
                    Dim bgraData = mdl_TangkapLayar.TangkapLayarKeBgra(SesiRemoteAktif.SkalaGambar, frameWidth, frameHeight)

                    If bgraData IsNot Nothing AndAlso bgraData.Length > 0 Then
                        Await mdl_UdpStreaming.KirimFrameKeEncoderAsync(bgraData)
                        SesiRemoteAktif.CatatFrame(frameCount, bgraData.Length / 50000.0)

                        If frameCount Mod 50 = 1 Then
                            Debug.WriteLine($"[RELAY-H264] Frame #{frameCount}: BGRA={bgraData.Length / 1024:F0}KB sent to encoder")
                        End If
                    Else
                        Debug.WriteLine("[RELAY-H264] BGRA capture failed!")
                    End If

                ' === PATH JPEG: Capture JPEG dan kirim langsung ===
                Else
                    Dim frame = Await mdl_TangkapLayar.TangkapFrameAsync(SesiRemoteAktif.SkalaGambar)

                    If frame IsNot Nothing Then
                        ' Kirim frame via UDP (preferred) atau TCP (fallback)
                        If useUdp AndAlso mdl_UdpStreaming.IsUdpStreamingActive() Then
                            ' UDP: Kirim raw JPEG bytes via relay
                            Dim jpegData = frame.DapatkanJpegBytes()
                            If jpegData IsNot Nothing Then
                                Await mdl_UdpStreaming.KirimFrameViaRelayAsync(jpegData)
                                If frameCount Mod 50 = 1 Then
                                    Debug.WriteLine($"[RELAY-JPEG] Frame #{frameCount}: {jpegData.Length / 1024.0:F1}KB via UDP")
                                End If
                            End If
                        Else
                            ' TCP fallback: Kirim paket JSON via relay TCP
                            Dim paket = BuatPaketFrameLayar(frame)
                            paket.IdSesi = IdSesiRelay

                            If frameCount Mod 50 = 1 Then
                                Debug.WriteLine($"[RELAY-TCP] Frame #{frameCount}: {frame.UkuranDataKB():F1}KB via TCP (fallback)")
                            End If

                            Try
                                Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paket)
                            Catch ioEx As IOException
                                Debug.WriteLine($"[RELAY-TCP] Frame #{frameCount}: IOException: {ioEx.Message}")
                                Exit While
                            End Try
                        End If

                        ' Catat statistik frame
                        SesiRemoteAktif.CatatFrame(frame.NomorFrame, frame.UkuranDataKB())
                    Else
                        Debug.WriteLine($"[RELAY-STREAM] Frame #{frameCount}: frame = Nothing!")
                    End If
                End If

                stopwatch.Stop()

                ' Hitung delay untuk target FPS
                Dim targetDelay = 1000 \ SesiRemoteAktif.TargetFPS
                Dim actualDelay = Math.Max(1, targetDelay - CInt(stopwatch.ElapsedMilliseconds))
                Await Task.Delay(actualDelay)

            End While

            Debug.WriteLine($"[RELAY-STREAM] Loop selesai. IsStreamingAktif={SesiRemoteAktif.IsStreamingAktif()}, TerhubungKeRelay={TerhubungKeRelay}")

        Catch ex As Exception
            Debug.WriteLine($"[RELAY-STREAM] EXCEPTION: {ex.GetType().Name}: {ex.Message}")
        Finally
            ' Hentikan UDP streaming saat selesai
            mdl_UdpStreaming.HentikanUdpStreaming()
            Debug.WriteLine("[RELAY-UDP] UDP streaming dihentikan")
        End Try

        Debug.WriteLine($"[RELAY-STREAM] Berhenti setelah {frameCount} frame")
    End Function

#End Region

End Module
