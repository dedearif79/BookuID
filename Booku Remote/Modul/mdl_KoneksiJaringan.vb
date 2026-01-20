Option Explicit On
Option Strict On

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
            _tcpListener = New TcpListener(IPAddress.Any, PORT_KONEKSI)
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

            If bytesRead > 0 Then
                Dim json = BytesKeString(buffer, 0, bytesRead)
                Dim paket = DeserializePaket(json)

                If paket IsNot Nothing AndAlso paket.TipePaket = TipePaket.PERMINTAAN_KONEKSI Then
                    Dim permintaan = DeserializePermintaanKoneksi(paket.Payload)
                    If permintaan IsNot Nothing Then
                        ' Raise event untuk dialog persetujuan
                        RaiseEvent PermintaanKoneksiMasuk(permintaan, client)
                    End If
                End If
            End If

        Catch ex As Exception
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
                                                   Optional pesan As String = "") As Task(Of Boolean)
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

            ' Buat dan kirim paket respon
            Dim paketRespon = BuatPaketResponKoneksi(hasil, kunciSesi, pesan, izinKontrol, izinTransfer, izinClipboard)
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

            ' Kirim permintaan koneksi
            Dim paket = BuatPaketPermintaanKoneksi(NamaPerangkatIni, AlamatIPLokal)
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
    ''' </summary>
    Public Async Function MulaiStreamingLayarAsync(Optional skala As Double = 0.5, Optional targetFPS As Integer = 15) As Task
        System.Diagnostics.Debug.WriteLine($"[DEBUG] MulaiStreamingLayarAsync dipanggil. _sedangStreaming={_sedangStreaming}, _terhubung={_terhubung}")

        If _sedangStreaming Then
            System.Diagnostics.Debug.WriteLine("[DEBUG] RETURN: Sudah sedang streaming")
            Return
        End If
        If Not _terhubung Then
            System.Diagnostics.Debug.WriteLine("[DEBUG] RETURN: Tidak terhubung")
            Return
        End If

        System.Diagnostics.Debug.WriteLine("[DEBUG] Memulai streaming loop...")
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

        Dim delayMs = SesiRemoteAktif.IntervalDelayMs()
        Dim token = _streamingCancellationToken.Token

        Dim frameCount As Integer = 0
        Try
            System.Diagnostics.Debug.WriteLine("[DEBUG] Masuk streaming loop")
            While _sedangStreaming AndAlso _terhubung AndAlso Not token.IsCancellationRequested
                Dim perluDelayError As Boolean = False

                Try
                    ' Tangkap frame
                    Dim frame = Await mdl_TangkapLayar.TangkapFrameAsync(skala)
                    If frame IsNot Nothing AndAlso frame.IsValid() Then
                        ' Kirim frame ke Tamu
                        Dim paket = BuatPaketFrameLayar(frame)
                        Await KirimPaketAsync(paket)

                        ' Update statistik
                        SesiRemoteAktif.CatatFrame(frame.NomorFrame, frame.UkuranDataKB())

                        frameCount += 1
                        If frameCount Mod 30 = 1 Then ' Log setiap 30 frame
                            System.Diagnostics.Debug.WriteLine($"[DEBUG] Frame #{frameCount} terkirim, size={frame.UkuranDataKB():F1}KB")
                        End If
                    Else
                        System.Diagnostics.Debug.WriteLine("[DEBUG] Frame null atau tidak valid!")
                    End If

                    ' Delay untuk target FPS
                    Await Task.Delay(delayMs, token)

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
            SesiRemoteAktif?.HentikanStreaming()
        End Try
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

End Module
