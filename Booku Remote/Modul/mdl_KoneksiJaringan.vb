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

    ''' <summary>Event ketika koneksi berhasil dibuat (dengan izin dari Host)</summary>
    Public Event KoneksiBerhasil(kunciSesi As String, izinKontrol As Boolean, izinClipboard As Boolean, izinTransferBerkas As Boolean)

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

            WriteLog($"[HOST-TCP] Server dimulai di port {PortKoneksiAktif}")
            Console.WriteLine($"[HOST-TCP] Server dimulai di port {PortKoneksiAktif}")

            ' Mulai task untuk menerima koneksi
            Task.Run(Async Function()
                         Await TerimaKoneksiAsync(_cancellationTokenSource.Token)
                     End Function)

        Catch ex As Exception
            WriteLog($"[HOST-TCP] Gagal memulai server: {ex.Message}")
            RaiseEvent ErrorKoneksi($"Gagal memulai server: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Task async untuk menerima koneksi masuk.
    ''' </summary>
    Private Async Function TerimaKoneksiAsync(token As CancellationToken) As Task
        WriteLog($"[HOST-TCP] Menunggu koneksi masuk...")
        While _sedangMendengarkan AndAlso Not token.IsCancellationRequested
            Try
                ' Tunggu koneksi masuk
                Dim client = Await _tcpListener.AcceptTcpClientAsync()

                Dim remoteEP = TryCast(client.Client.RemoteEndPoint, System.Net.IPEndPoint)
                WriteLog($"[HOST-TCP] Koneksi masuk dari {remoteEP?.Address}:{remoteEP?.Port}")
                Console.WriteLine($"[HOST-TCP] Koneksi masuk dari {remoteEP?.Address}:{remoteEP?.Port}")

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

            WriteLog($"[DEBUG] ProsesKoneksiMasukAsync: bytesRead={bytesRead}")

            If bytesRead > 0 Then
                Dim json = BytesKeString(buffer, 0, bytesRead)
                WriteLog($"[DEBUG] JSON diterima: {json.Substring(0, Math.Min(500, json.Length))}...")

                Dim paket = DeserializePaket(json)
                WriteLog($"[DEBUG] Paket deserialize: {If(paket IsNot Nothing, "OK", "NULL")}")

                If paket IsNot Nothing Then
                    WriteLog($"[DEBUG] TipePaket={paket.TipePaket} (expected={TipePaket.PERMINTAAN_KONEKSI})")

                    If paket.TipePaket = TipePaket.PERMINTAAN_KONEKSI Then
                        Dim permintaan = DeserializePermintaanKoneksi(paket.Payload)
                        WriteLog($"[DEBUG] Permintaan deserialize: {If(permintaan IsNot Nothing, "OK - " & permintaan.NamaPerangkat, "NULL")}")

                        If permintaan IsNot Nothing Then
                            ' Raise event untuk dialog persetujuan
                            WriteLog("[DEBUG] RaiseEvent PermintaanKoneksiMasuk...")
                            RaiseEvent PermintaanKoneksiMasuk(permintaan, client)
                        Else
                            WriteLog($"[DEBUG] GAGAL deserialize permintaan. Payload: {paket.Payload}")
                        End If
                    Else
                        WriteLog($"[DEBUG] TipePaket tidak match! Got={CInt(paket.TipePaket)}, Expected={CInt(TipePaket.PERMINTAAN_KONEKSI)}")
                    End If
                Else
                    WriteLog("[DEBUG] GAGAL deserialize paket!")
                End If
            Else
                WriteLog("[DEBUG] bytesRead = 0, tidak ada data!")
            End If

        Catch ex As Exception
            WriteLog($"[DEBUG] EXCEPTION: {ex.Message}")
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
                RaiseEvent KoneksiBerhasil(kunciSesi, izinKontrol, izinClipboard, izinTransfer)
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
            WriteLog("[CODEC] Client tidak kirim supportedCodecs, using JPEG")
            mdl_UdpStreaming.CodecStreaming = TipeKodek.JPEG
            Return selectedCodec
        End If

        ' Cek apakah client mendukung H.264
        Dim clientSupportsH264 = clientSupportedCodecs.Any(Function(c) c.Equals("H264", StringComparison.OrdinalIgnoreCase))

        ' Cek apakah FFmpeg tersedia di Host
        Dim ffmpegTersedia = mdl_FFmpegManager.FFmpegTersedia

        WriteLog($"[CODEC] Client supports H264: {clientSupportsH264}, FFmpeg available: {ffmpegTersedia}")

        ' Pilih H.264 jika client support DAN FFmpeg tersedia
        If clientSupportsH264 AndAlso ffmpegTersedia Then
            selectedCodec = "H264"
            mdl_UdpStreaming.CodecStreaming = TipeKodek.H264
            WriteLog("[CODEC] Selected: H.264")
        Else
            mdl_UdpStreaming.CodecStreaming = TipeKodek.JPEG
            WriteLog("[CODEC] Selected: JPEG")
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
                            RaiseEvent KoneksiBerhasil(respon.KunciSesi, respon.IzinKontrol, respon.IzinClipboard, respon.IzinTransferBerkas)

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
            WriteLog($"[TCP-RECV] DengarkanPaketAsync dimulai, Mode={ModeAplikasiSaatIni}")
            Dim stream = client.GetStream()
            Dim buffer(65535) As Byte

            While _terhubung AndAlso Not token.IsCancellationRequested
                Try
                    Dim bytesRead = Await stream.ReadAsync(buffer, 0, buffer.Length)
                    WriteLog($"[TCP-RECV] Data diterima: {bytesRead} bytes")

                    If bytesRead = 0 Then
                        ' Koneksi ditutup oleh peer
                        WriteLog("[TCP-RECV] bytesRead=0, koneksi ditutup oleh peer")
                        Exit While
                    End If

                    Dim json = BytesKeString(buffer, 0, bytesRead)
                    Dim paket = DeserializePaket(json)
                    WriteLog($"[TCP-RECV] Paket TipePaket={paket?.TipePaket}")

                    If paket IsNot Nothing Then
                        Select Case paket.TipePaket
                            Case TipePaket.TUTUP_KONEKSI
                                Exit While

                            Case TipePaket.HEARTBEAT
                                ' Respond heartbeat
                                Await KirimPaketAsync(BuatPaketHeartbeat(paket.IdSesi))

                            Case TipePaket.PERMINTAAN_STREAMING
                                ' Tamu minta mulai streaming (Host side)
                                WriteLog($"[DEBUG] PERMINTAAN_STREAMING diterima. Mode={ModeAplikasiSaatIni}, _terhubung={_terhubung}")
                                If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                                    WriteLog("[DEBUG] Memulai MulaiStreamingLayarAsync...")
                                    Task.Run(Async Function()
                                                 Await MulaiStreamingLayarAsync()
                                             End Function)
                                Else
                                    WriteLog($"[DEBUG] SKIP: Mode bukan HOST ({ModeAplikasiSaatIni})")
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
                                WriteLog($"[HOST] Terima paket INPUT_MOUSE, ModeAplikasi={ModeAplikasiSaatIni}")
                                If ModeAplikasiSaatIni = ModeAplikasi.HOST Then
                                    ProsesInputMouseDariPaket(paket.Payload)
                                Else
                                    WriteLog($"[HOST] SKIP INPUT_MOUSE: Mode bukan HOST")
                                End If

                            Case TipePaket.CLIPBOARD_DATA
                                ' Proses clipboard sync (bidirectional)
                                ProsesClipboardData(paket.Payload)

                            ' === FASE 3b: Handle Transfer Berkas ===

                            Case TipePaket.PERMINTAAN_BERKAS
                                ' Permintaan transfer masuk (di Host atau Tamu)
                                ProsesPermintaanBerkas(paket.Payload)

                            Case TipePaket.RESPON_TRANSFER
                                ' Respon dari permintaan transfer
                                ProsesResponTransfer(paket.Payload)

                            Case TipePaket.DATA_BERKAS
                                ' Data chunk file diterima
                                ProsesDataBerkas(paket.Payload)

                            Case TipePaket.KONFIRMASI_CHUNK
                                ' ACK per chunk
                                ProsesKonfirmasiChunk(paket.Payload)

                            Case TipePaket.KONFIRMASI_BERKAS
                                ' Transfer selesai
                                mdl_TransferBerkas.ProsesKonfirmasiBerkas(DeserializeKonfirmasiBerkas(paket.Payload))

                            Case TipePaket.BATAL_TRANSFER
                                ' Transfer dibatalkan
                                mdl_TransferBerkas.ProsesBatalTransfer(DeserializeBatalTransfer(paket.Payload))

                            Case TipePaket.DAFTAR_FOLDER
                                ' Request daftar folder (di Host)
                                ProsesDaftarFolder(paket.Payload)

                            Case TipePaket.RESPON_DAFTAR_FOLDER
                                ' Response daftar folder (di Tamu)
                                ProsesResponDaftarFolder(paket.Payload)

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
    ''' Tidak bergantung pada ModeAplikasiSaatIni karena bisa di-reset oleh window lifecycle.
    ''' Cek langsung ketersediaan _tcpClient (Tamu) atau _clientTerhubung (Host).
    ''' </summary>
    Public Async Function KirimPaketAsync(paket As cls_PaketData) As Task(Of Boolean)
        Try
            WriteLog($"[TCP-SEND] KirimPaketAsync dipanggil, TipePaket={paket.TipePaket}")

            Dim stream As NetworkStream = Nothing

            ' Cek _tcpClient dulu (Tamu yang connect ke Host)
            If _tcpClient IsNot Nothing AndAlso _tcpClient.Connected Then
                stream = _tcpClient.GetStream()
                WriteLog($"[TCP-SEND] Menggunakan stream dari _tcpClient (Tamu)")
            ' Jika tidak ada, cek _clientTerhubung (Host yang menerima koneksi)
            ElseIf _clientTerhubung IsNot Nothing AndAlso _clientTerhubung.Connected Then
                stream = _clientTerhubung.GetStream()
                WriteLog($"[TCP-SEND] Menggunakan stream dari _clientTerhubung (Host)")
            End If

            If stream IsNot Nothing Then
                paket.IdSesi = KunciSesiAktif
                Dim data = StringKeBytes(SerializePaket(paket))
                Await stream.WriteAsync(data, 0, data.Length)
                WriteLog($"[TCP-SEND] Paket terkirim: {data.Length} bytes")
                Return True
            Else
                WriteLog($"[TCP-SEND] ERROR: stream Is Nothing! _tcpClient={_tcpClient IsNot Nothing}, _clientTerhubung={_clientTerhubung IsNot Nothing}")
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
    ''' FPS dan Skala diambil dari settings (TargetFPSAktif, SkalaCaptureAktif) secara realtime setiap frame.
    ''' </summary>
    Public Async Function MulaiStreamingLayarAsync() As Task
        WriteLog($"[DEBUG] MulaiStreamingLayarAsync dipanggil. _sedangStreaming={_sedangStreaming}, _terhubung={_terhubung}")

        If _sedangStreaming Then
            WriteLog("[DEBUG] RETURN: Sudah sedang streaming")
            Return
        End If
        If Not _terhubung Then
            WriteLog("[DEBUG] RETURN: Tidak terhubung")
            Return
        End If

        WriteLog($"[DEBUG] Memulai streaming loop dengan UDP... FPS={TargetFPSAktif}, Skala={SkalaCaptureAktif:F1}")
        _sedangStreaming = True
        _streamingCancellationToken = New CancellationTokenSource()

        ' Inisialisasi sesi remote
        If SesiRemoteAktif Is Nothing Then
            SesiRemoteAktif = New cls_SesiRemote()
        End If
        SesiRemoteAktif.MulaiStreaming()
        SesiRemoteAktif.SkalaGambar = SkalaCaptureAktif
        SesiRemoteAktif.TargetFPS = TargetFPSAktif

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
            WriteLog($"[UDP-HOST] Using IdSesiRelay for SessionId: {IdSesiRelay} -> {sessionId}")
        Else
            sessionId = mdl_UdpStreaming.GenerateSessionId(KunciSesiAktif)
        End If

        If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
            ' Mode Internet: UDP via Relay (endpoint sudah di-setup saat connect ke relay)
            mdl_UdpStreaming.MulaiUdpSender(RelayServerIPAktif, PortUdpVideoAktif, sessionId)
            WriteLog($"[UDP-HOST] UDP sender dimulai via Relay {RelayServerIPAktif}:{PortUdpVideoAktif}, SessionId={sessionId}")
        Else
            ' Mode LAN: kirim langsung ke IP Tamu
            Dim tamuIP = DapatkanIPTamuTerhubung()
            If Not String.IsNullOrEmpty(tamuIP) Then
                mdl_UdpStreaming.MulaiUdpSender(tamuIP, PortUdpVideoAktif, sessionId)
                WriteLog($"[UDP-HOST] UDP sender dimulai ke {tamuIP}:{PortUdpVideoAktif}")
            Else
                WriteLog("[UDP-HOST] Gagal mendapatkan IP Tamu, fallback ke TCP")
            End If
        End If

        ' === H.264 Encoder: Mulai jika codec negosiasi adalah H264 ===
        Dim useH264 As Boolean = False
        Dim h264EncoderWidth As Integer = 0
        Dim h264EncoderHeight As Integer = 0

        If mdl_UdpStreaming.CodecStreaming = TipeKodek.H264 Then
            ' Dapatkan ukuran frame untuk encoder
            Dim bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds
            h264EncoderWidth = CInt(bounds.Width * SkalaCaptureAktif)
            h264EncoderHeight = CInt(bounds.Height * SkalaCaptureAktif)

            ' Mulai H.264 encoder
            useH264 = mdl_UdpStreaming.MulaiH264Encoder(h264EncoderWidth, h264EncoderHeight, TargetFPSAktif)
            If useH264 Then
                WriteLog($"[H264] Encoder started: {h264EncoderWidth}x{h264EncoderHeight} @ {TargetFPSAktif}fps")
            Else
                WriteLog("[H264] Encoder failed to start, fallback to JPEG")
            End If
        End If

        Dim token = _streamingCancellationToken.Token
        Dim useUdp = mdl_UdpStreaming.IsUdpStreamingActive()

        Dim frameCount As Integer = 0
        Dim lastSkala As Double = SkalaCaptureAktif
        Try
            WriteLog($"[DEBUG] Masuk streaming loop, UDP={useUdp}, H264={useH264}, targetFPS={TargetFPSAktif}")
            While _sedangStreaming AndAlso _terhubung AndAlso Not token.IsCancellationRequested
                Dim perluDelayError As Boolean = False
                Dim stopwatch = System.Diagnostics.Stopwatch.StartNew()

                ' Baca nilai skala aktif setiap iterasi untuk perubahan realtime
                Dim currentSkala = SkalaCaptureAktif

                Try
                    frameCount += 1

                    ' === PATH H.264: Capture BGRA dan kirim ke encoder ===
                    If useH264 AndAlso mdl_UdpStreaming.H264EncoderAktif Then
                        ' Cek apakah skala berubah - jika ya, restart encoder dengan resolusi baru
                        If Math.Abs(currentSkala - lastSkala) > 0.01 Then
                            WriteLog($"[H264] Skala berubah dari {lastSkala:F2} ke {currentSkala:F2}, restart encoder...")
                            Dim bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds
                            h264EncoderWidth = CInt(bounds.Width * currentSkala)
                            h264EncoderHeight = CInt(bounds.Height * currentSkala)
                            mdl_UdpStreaming.HentikanH264Encoder()
                            useH264 = mdl_UdpStreaming.MulaiH264Encoder(h264EncoderWidth, h264EncoderHeight, TargetFPSAktif)
                            lastSkala = currentSkala
                            If useH264 Then
                                WriteLog($"[H264] Encoder restarted: {h264EncoderWidth}x{h264EncoderHeight}")
                            Else
                                WriteLog("[H264] Encoder restart failed, falling back to JPEG")
                            End If
                            Continue While
                        End If

                        ' Capture layar langsung ke BGRA untuk H.264 encoder
                        Dim frameWidth As Integer = 0
                        Dim frameHeight As Integer = 0
                        Dim bgraData = mdl_TangkapLayar.TangkapLayarKeBgra(currentSkala, frameWidth, frameHeight)

                        If bgraData IsNot Nothing AndAlso bgraData.Length > 0 Then
                            ' Kirim ke H.264 encoder (encoder akan trigger event DataReady -> kirim via UDP)
                            Await mdl_UdpStreaming.KirimFrameKeEncoderAsync(bgraData)

                            ' Update statistik (estimasi ukuran BGRA / 50 untuk approx H.264 size)
                            SesiRemoteAktif.CatatFrame(frameCount, bgraData.Length / 50000.0)

                            If frameCount Mod 50 = 1 Then
                                WriteLog($"[H264] Frame #{frameCount} sent to encoder, BGRA={bgraData.Length / 1024:F0}KB, skala={currentSkala:F2}, elapsed={stopwatch.ElapsedMilliseconds}ms")
                            End If
                        Else
                            WriteLog("[H264] BGRA capture failed!")
                        End If

                    ' === PATH JPEG: Capture JPEG dan kirim langsung ===
                    Else
                        ' Tangkap frame sebagai JPEG dengan skala aktif (realtime)
                        Dim frame = Await mdl_TangkapLayar.TangkapFrameAsync(currentSkala)
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
                                WriteLog($"[JPEG] Frame #{frameCount} terkirim via {If(useUdp, "UDP", "TCP")}, size={frame.UkuranDataKB():F1}KB, skala={currentSkala:F2}, elapsed={stopwatch.ElapsedMilliseconds}ms")
                            End If
                        Else
                            WriteLog("[JPEG] Frame null atau tidak valid!")
                        End If
                    End If

                    stopwatch.Stop()

                    ' Delay dinamis untuk target FPS (dikurangi waktu operasi)
                    Dim targetDelayMs = 1000 \ TargetFPSAktif
                    Dim actualDelayMs = Math.Max(1, targetDelayMs - CInt(stopwatch.ElapsedMilliseconds))
                    Await Task.Delay(actualDelayMs, token)

                Catch ex As TaskCanceledException
                    Exit While
                Catch ex As Exception
                    ' Log error tapi lanjutkan streaming
                    WriteLog($"Error streaming frame: {ex.Message}")
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

        ' Hentikan UDP streaming dan H264 encoder
        mdl_UdpStreaming.HentikanUdpStreaming()
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
            WriteLog($"Error proses input keyboard: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Proses input mouse dari Tamu dan inject ke sistem Host.
    ''' </summary>
    Private Sub ProsesInputMouseDariPaket(payload As String)
        Try
            WriteLog($"[HOST-INPUT] Menerima paket mouse, payload length={payload?.Length}")

            Dim input = DeserializeInputMouse(payload)
            If input Is Nothing Then
                WriteLog($"[HOST-INPUT] ERROR: DeserializeInputMouse return Nothing!")
                Return
            End If

            WriteLog($"[HOST-INPUT] Mouse action: {input.TipeAksi}, X={input.X:F3}, Y={input.Y:F3}, Button={input.Button}")

            ' Gunakan fungsi helper di mdl_InjeksiInput
            mdl_InjeksiInput.ProsesInputMouse(input)

        Catch ex As Exception
            WriteLog($"Error proses input mouse: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Clipboard Processing (Fase 3)"

    ''' <summary>
    ''' Proses clipboard data yang diterima dari peer (Host atau Tamu).
    ''' </summary>
    Private Sub ProsesClipboardData(payload As String)
        Try
            Dim clipboardPayload = DeserializeClipboard(payload)
            If clipboardPayload Is Nothing Then
                WriteLog("[CLIPBOARD] Gagal deserialize clipboard payload")
                Return
            End If

            ' Serahkan ke modul clipboard untuk diproses
            mdl_Clipboard.TerimaClipboardDariRemote(clipboardPayload)

        Catch ex As Exception
            WriteLog($"[CLIPBOARD] Error proses clipboard: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim clipboard data ke peer yang terhubung.
    ''' Dipanggil dari mdl_Clipboard via callback.
    ''' </summary>
    Public Async Function KirimClipboardAsync(payload As cls_PayloadClipboard) As Task
        Try
            Dim paket = BuatPaketClipboard(payload.TipeData, payload.Data, payload.Source, payload.HashData)
            paket.IdSesi = KunciSesiAktif

            ' Kirim via koneksi yang aktif
            Await KirimPaketAsync(paket)
            WriteLog($"[CLIPBOARD] Terkirim ke peer: {payload.TipeData}, hash={payload.HashData?.Substring(0, 8)}")

        Catch ex As Exception
            WriteLog($"[CLIPBOARD] Gagal kirim: {ex.Message}")
        End Try
    End Function

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
                    WriteLog($"[RELAY] PERMINTAAN_STREAMING diterima via relay")
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

                Case TipePaket.CLIPBOARD_DATA
                    ' Proses clipboard sync via relay
                    ProsesClipboardData(paket.Payload)

                ' === FASE 3b: Handle Transfer Berkas via Relay ===

                Case TipePaket.PERMINTAAN_BERKAS
                    ' Permintaan transfer masuk
                    ProsesPermintaanBerkas(paket.Payload)

                Case TipePaket.RESPON_TRANSFER
                    ' Respon dari permintaan transfer
                    ProsesResponTransfer(paket.Payload)

                Case TipePaket.DATA_BERKAS
                    ' Data chunk file diterima
                    ProsesDataBerkas(paket.Payload)

                Case TipePaket.KONFIRMASI_CHUNK
                    ' ACK per chunk
                    ProsesKonfirmasiChunk(paket.Payload)

                Case TipePaket.KONFIRMASI_BERKAS
                    ' Transfer selesai
                    mdl_TransferBerkas.ProsesKonfirmasiBerkas(DeserializeKonfirmasiBerkas(paket.Payload))

                Case TipePaket.BATAL_TRANSFER
                    ' Transfer dibatalkan
                    mdl_TransferBerkas.ProsesBatalTransfer(DeserializeBatalTransfer(paket.Payload))

                Case TipePaket.DAFTAR_FOLDER
                    ' Request daftar folder (di Host)
                    ProsesDaftarFolder(paket.Payload)

                Case TipePaket.RESPON_DAFTAR_FOLDER
                    ' Response daftar folder (di Tamu)
                    ProsesResponDaftarFolder(paket.Payload)

                Case Else
                    RaiseEvent PaketDiterima(paket)
            End Select

        Catch ex As Exception
            WriteLog($"[RELAY] Error proses paket: {ex.Message}")
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
        SesiRemoteAktif.SkalaGambar = SkalaCaptureAktif

        WriteLog($"[RELAY] Streaming layar dimulai dengan FPS={TargetFPSAktif}, Skala={SkalaCaptureAktif:F1}")

        ' Setup UDP sender untuk streaming via relay
        ' SessionId harus sama dengan yang digunakan relay server (hash dari IdSesiRelay)
        Dim sessionId = mdl_UdpStreaming.GenerateSessionId(IdSesiRelay)
        mdl_UdpStreaming.MulaiUdpSender(RelayServerIPAktif, PortUdpVideoAktif, sessionId)
        mdl_UdpStreaming.SetupRelayUdpEndpoint(RelayServerIPAktif, PortUdpVideoAktif)
        WriteLog($"[RELAY-UDP] UDP sender dimulai ke {RelayServerIPAktif}:{PortUdpVideoAktif}, SessionId={sessionId} (dari IdSesi={IdSesiRelay})")

        ' === H.264 Encoder: Mulai jika codec negosiasi adalah H264 ===
        Dim useH264 As Boolean = False
        Dim h264EncoderWidth As Integer = 0
        Dim h264EncoderHeight As Integer = 0
        Dim lastSkala As Double = SkalaCaptureAktif

        If mdl_UdpStreaming.CodecStreaming = TipeKodek.H264 Then
            Dim bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds
            h264EncoderWidth = CInt(bounds.Width * SkalaCaptureAktif)
            h264EncoderHeight = CInt(bounds.Height * SkalaCaptureAktif)

            useH264 = mdl_UdpStreaming.MulaiH264Encoder(h264EncoderWidth, h264EncoderHeight, TargetFPSAktif)
            If useH264 Then
                WriteLog($"[RELAY-H264] Encoder started: {h264EncoderWidth}x{h264EncoderHeight} @ {TargetFPSAktif}fps")
            Else
                WriteLog("[RELAY-H264] Encoder failed to start, fallback to JPEG")
            End If
        End If

        Dim useUdp = mdl_UdpStreaming.IsUdpStreamingActive()
        WriteLog($"[RELAY-UDP] UDP aktif = {useUdp}, H264 = {useH264}")

        Dim frameCount = 0

        Try
            WriteLog($"[RELAY-STREAM] Loop dimulai. IsStreamingAktif={SesiRemoteAktif.IsStreamingAktif()}, TerhubungKeRelay={TerhubungKeRelay}, UseUDP={useUdp}, H264={useH264}")

            While SesiRemoteAktif.IsStreamingAktif() AndAlso TerhubungKeRelay

                frameCount += 1
                Dim stopwatch = System.Diagnostics.Stopwatch.StartNew()

                ' Baca skala saat ini setiap iterasi (realtime dari slider)
                Dim currentSkala = SkalaCaptureAktif

                ' === PATH H.264: Capture BGRA dan kirim ke encoder ===
                If useH264 AndAlso mdl_UdpStreaming.H264EncoderAktif Then
                    ' Deteksi perubahan skala - perlu restart encoder jika berubah
                    If Math.Abs(currentSkala - lastSkala) > 0.01 Then
                        WriteLog($"[RELAY-H264] Skala berubah dari {lastSkala:F2} ke {currentSkala:F2}, restart encoder...")
                        Dim bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds
                        h264EncoderWidth = CInt(bounds.Width * currentSkala)
                        h264EncoderHeight = CInt(bounds.Height * currentSkala)

                        ' Hentikan encoder lama dan mulai dengan resolusi baru
                        mdl_UdpStreaming.HentikanH264Encoder()
                        useH264 = mdl_UdpStreaming.MulaiH264Encoder(h264EncoderWidth, h264EncoderHeight, TargetFPSAktif)
                        lastSkala = currentSkala

                        If useH264 Then
                            WriteLog($"[RELAY-H264] Encoder restarted: {h264EncoderWidth}x{h264EncoderHeight}")
                        Else
                            WriteLog("[RELAY-H264] Encoder restart failed, fallback to JPEG")
                        End If
                    End If

                    Dim frameWidth As Integer = 0
                    Dim frameHeight As Integer = 0
                    Dim bgraData = mdl_TangkapLayar.TangkapLayarKeBgra(currentSkala, frameWidth, frameHeight)

                    If bgraData IsNot Nothing AndAlso bgraData.Length > 0 Then
                        Await mdl_UdpStreaming.KirimFrameKeEncoderAsync(bgraData)
                        SesiRemoteAktif.CatatFrame(frameCount, bgraData.Length / 50000.0)

                        If frameCount Mod 50 = 1 Then
                            WriteLog($"[RELAY-H264] Frame #{frameCount}: BGRA={bgraData.Length / 1024:F0}KB sent to encoder")
                        End If
                    Else
                        WriteLog("[RELAY-H264] BGRA capture failed!")
                    End If

                ' === PATH JPEG: Capture JPEG dan kirim langsung ===
                Else
                    ' Update lastSkala untuk JPEG path juga
                    lastSkala = currentSkala
                    Dim frame = Await mdl_TangkapLayar.TangkapFrameAsync(currentSkala)

                    If frame IsNot Nothing Then
                        ' Kirim frame via UDP (preferred) atau TCP (fallback)
                        If useUdp AndAlso mdl_UdpStreaming.IsUdpStreamingActive() Then
                            ' UDP: Kirim raw JPEG bytes via relay
                            Dim jpegData = frame.DapatkanJpegBytes()
                            If jpegData IsNot Nothing Then
                                Await mdl_UdpStreaming.KirimFrameViaRelayAsync(jpegData)
                                If frameCount Mod 50 = 1 Then
                                    WriteLog($"[RELAY-JPEG] Frame #{frameCount}: {jpegData.Length / 1024.0:F1}KB via UDP")
                                End If
                            End If
                        Else
                            ' TCP fallback: Kirim paket JSON via relay TCP
                            Dim paket = BuatPaketFrameLayar(frame)
                            paket.IdSesi = IdSesiRelay

                            If frameCount Mod 50 = 1 Then
                                WriteLog($"[RELAY-TCP] Frame #{frameCount}: {frame.UkuranDataKB():F1}KB via TCP (fallback)")
                            End If

                            Try
                                Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paket)
                            Catch ioEx As IOException
                                WriteLog($"[RELAY-TCP] Frame #{frameCount}: IOException: {ioEx.Message}")
                                Exit While
                            End Try
                        End If

                        ' Catat statistik frame
                        SesiRemoteAktif.CatatFrame(frame.NomorFrame, frame.UkuranDataKB())
                    Else
                        WriteLog($"[RELAY-STREAM] Frame #{frameCount}: frame = Nothing!")
                    End If
                End If

                stopwatch.Stop()

                ' Hitung delay untuk target FPS (baca TargetFPSAktif langsung setiap iterasi)
                Dim targetDelayMs = 1000 \ TargetFPSAktif
                Dim actualDelay = Math.Max(1, targetDelayMs - CInt(stopwatch.ElapsedMilliseconds))
                Await Task.Delay(actualDelay)

            End While

            WriteLog($"[RELAY-STREAM] Loop selesai. IsStreamingAktif={SesiRemoteAktif.IsStreamingAktif()}, TerhubungKeRelay={TerhubungKeRelay}")

        Catch ex As Exception
            WriteLog($"[RELAY-STREAM] EXCEPTION: {ex.GetType().Name}: {ex.Message}")
        Finally
            ' Hentikan UDP streaming saat selesai
            mdl_UdpStreaming.HentikanUdpStreaming()
            WriteLog("[RELAY-UDP] UDP streaming dihentikan")
        End Try

        WriteLog($"[RELAY-STREAM] Berhenti setelah {frameCount} frame")
    End Function

#End Region

#Region "Transfer Berkas Handlers (Fase 3b)"

    ''' <summary>Event ketika permintaan transfer masuk (untuk UI)</summary>
    Public Event PermintaanTransferMasuk(payload As cls_PayloadPermintaanTransfer)

    ''' <summary>Event ketika daftar folder diterima (untuk UI)</summary>
    Public Event DaftarFolderDiterima(payload As cls_PayloadResponDaftarFolder)

    ''' <summary>
    ''' Folder default untuk menyimpan file yang diterima.
    ''' </summary>
    Public Property FolderDownload As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\Downloads"

    ''' <summary>
    ''' Proses permintaan transfer file yang masuk.
    ''' </summary>
    Private Sub ProsesPermintaanBerkas(payload As String)
        Try
            Dim data = DeserializePermintaanTransfer(payload)
            If data Is Nothing Then Return

            WriteLog($"[TRANSFER] Permintaan berkas diterima: {data.NamaFile}, Size: {data.UkuranFile}, Arah: {data.Arah}")

            ' Buat state transfer untuk menerima
            Dim transfer = mdl_TransferBerkas.TerimaPermintaanTransfer(data, FolderDownload)
            If transfer IsNot Nothing Then
                ' Raise event untuk UI menampilkan dialog konfirmasi
                RaiseEvent PermintaanTransferMasuk(data)
            End If

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error proses permintaan berkas: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Proses respon dari permintaan transfer.
    ''' </summary>
    Private Sub ProsesResponTransfer(payload As String)
        Try
            Dim data = DeserializeResponTransfer(payload)
            If data Is Nothing Then Return

            WriteLog($"[TRANSFER] Respon transfer diterima: ID={data.TransferId}, Diterima={data.Diterima}")

            Dim transfer = mdl_TransferBerkas.GetTransfer(data.TransferId)
            If transfer Is Nothing Then Return

            If data.Diterima Then
                ' Mulai kirim file
                transfer.MulaiTransfer()
                Task.Run(Async Function()
                             Await mdl_TransferBerkas.KirimFileAsync(data.TransferId, data.MulaiDariChunk)
                         End Function)
            Else
                ' Transfer ditolak
                transfer.GagalTransfer(data.Pesan)
            End If

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error proses respon transfer: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Proses data chunk file yang diterima.
    ''' </summary>
    Private Sub ProsesDataBerkas(payload As String)
        Try
            Dim data = DeserializeDataBerkas(payload)
            If data Is Nothing Then Return

            ' Proses chunk
            Dim sukses = mdl_TransferBerkas.ProsesChunkDiterima(data)

            ' Kirim konfirmasi chunk via metode yang sesuai (LAN atau Relay)
            Dim paketKonfirmasi = BuatPaketKonfirmasiChunk(data.TransferId, data.ChunkIndex, sukses, Not sukses)
            Task.Run(Async Function()
                         If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
                             ' PENTING: Set IdSesi untuk routing di relay
                             paketKonfirmasi.IdSesi = IdSesiRelay
                             Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paketKonfirmasi)
                         Else
                             Await KirimPaketAsync(paketKonfirmasi)
                         End If
                     End Function)

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error proses data berkas: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Proses konfirmasi chunk (ACK).
    ''' </summary>
    Private Sub ProsesKonfirmasiChunk(payload As String)
        Try
            Dim data = DeserializeKonfirmasiChunk(payload)
            If data Is Nothing Then Return

            If data.KirimUlang Then
                WriteLog($"[TRANSFER] Chunk {data.ChunkIndex} perlu dikirim ulang")
                ' TODO: Implementasi retry logic
            End If

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error proses konfirmasi chunk: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Proses permintaan daftar folder (di Host).
    ''' </summary>
    Private Sub ProsesDaftarFolder(payload As String)
        Try
            Dim data = DeserializeDaftarFolder(payload)
            If data Is Nothing Then Return

            WriteLog($"[TRANSFER] Request daftar folder: {data.Path}")

            ' Dapatkan daftar folder
            Dim response As cls_PayloadResponDaftarFolder
            If String.IsNullOrEmpty(data.Path) Then
                response = mdl_TransferBerkas.DapatkanDaftarDrive()
            Else
                response = mdl_TransferBerkas.DapatkanDaftarFolder(data.Path)
            End If

            ' Kirim response via metode yang sesuai (LAN atau Relay)
            Dim paket = BuatPaketResponDaftarFolder(response.Path, response.Items, response.Sukses,
                                                     response.ParentPath, response.Pesan)
            Task.Run(Async Function()
                         If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
                             ' PENTING: Set IdSesi untuk routing di relay
                             paket.IdSesi = IdSesiRelay
                             Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paket)
                         Else
                             Await KirimPaketAsync(paket)
                         End If
                     End Function)

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error proses daftar folder: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Proses response daftar folder (di Tamu).
    ''' </summary>
    Private Sub ProsesResponDaftarFolder(payload As String)
        Try
            Dim data = DeserializeResponDaftarFolder(payload)
            If data Is Nothing Then Return

            WriteLog($"[TRANSFER] Response daftar folder: {data.Path}, Items: {data.Items?.Count}")

            ' Raise event untuk UI
            RaiseEvent DaftarFolderDiterima(data)

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error proses respon daftar folder: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Mengirim permintaan transfer file ke peer.
    ''' </summary>
    ''' <param name="transfer">State transfer</param>
    Public Async Function KirimPermintaanTransferAsync(transfer As cls_TransferBerkas) As Task
        Try
            Dim paket = BuatPaketPermintaanBerkas(transfer)
            Await KirimPaketAsync(paket)
            WriteLog($"[TRANSFER] Permintaan transfer terkirim: {transfer.NamaFile}")
        Catch ex As Exception
            WriteLog($"[TRANSFER] Error kirim permintaan transfer: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' Mengirim respon transfer (terima/tolak).
    ''' </summary>
    ''' <param name="transferId">ID transfer</param>
    ''' <param name="diterima">True jika diterima</param>
    ''' <param name="pesan">Pesan opsional</param>
    Public Async Function KirimResponTransferAsync(transferId As String, diterima As Boolean,
                                                    Optional pesan As String = "") As Task
        Try
            Dim paket = BuatPaketResponTransfer(transferId, diterima, pesan)

            ' Pilih metode pengiriman berdasarkan mode koneksi
            If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
                ' Kirim via Relay - PENTING: Set IdSesi untuk routing di relay
                paket.IdSesi = IdSesiRelay
                Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paket)
                WriteLog($"[TRANSFER] Respon transfer terkirim via RELAY: ID={transferId}, Diterima={diterima}, IdSesi={paket.IdSesi}")
            Else
                ' Kirim via TCP langsung (LAN)
                Await KirimPaketAsync(paket)
                WriteLog($"[TRANSFER] Respon transfer terkirim via LAN: ID={transferId}, Diterima={diterima}")
            End If

            If diterima Then
                ' Mulai menerima file
                Dim transfer = mdl_TransferBerkas.GetTransfer(transferId)
                If transfer IsNot Nothing Then
                    transfer.MulaiTransfer()
                    mdl_TransferBerkas.SedangTransfer = True
                End If
            End If

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error kirim respon transfer: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' Mengirim permintaan daftar folder ke Host.
    ''' </summary>
    ''' <param name="path">Path folder (kosong = root/drives)</param>
    Public Async Function KirimPermintaanDaftarFolderAsync(Optional path As String = "") As Task
        Try
            Dim paket = BuatPaketDaftarFolder(path)

            ' Pilih metode pengiriman berdasarkan mode koneksi
            If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
                ' PENTING: Set IdSesi untuk routing di relay
                paket.IdSesi = IdSesiRelay
                Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paket)
                WriteLog($"[TRANSFER] Permintaan daftar folder terkirim via RELAY: {path}")
            Else
                Await KirimPaketAsync(paket)
                WriteLog($"[TRANSFER] Permintaan daftar folder terkirim via LAN: {path}")
            End If

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error kirim permintaan daftar folder: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' Membatalkan transfer yang sedang berjalan.
    ''' </summary>
    ''' <param name="transferId">ID transfer</param>
    ''' <param name="alasan">Alasan pembatalan</param>
    Public Async Function BatalkanTransferAsync(transferId As String, alasan As String) As Task
        Try
            Dim paket = BuatPaketBatalTransfer(transferId, alasan)

            ' Pilih metode pengiriman berdasarkan mode koneksi
            If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
                ' PENTING: Set IdSesi untuk routing di relay
                paket.IdSesi = IdSesiRelay
                Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paket)
                WriteLog($"[TRANSFER] Batal transfer terkirim via RELAY: {transferId}")
            Else
                Await KirimPaketAsync(paket)
                WriteLog($"[TRANSFER] Batal transfer terkirim via LAN: {transferId}")
            End If

            ' Update state lokal
            mdl_TransferBerkas.ProsesBatalTransfer(New cls_PayloadBatalTransfer With {
                .TransferId = transferId,
                .Alasan = alasan
            })

            WriteLog($"[TRANSFER] Transfer dibatalkan: {transferId}, Alasan: {alasan}")
        Catch ex As Exception
            WriteLog($"[TRANSFER] Error batalkan transfer: {ex.Message}")
        End Try
    End Function

#End Region

End Module
