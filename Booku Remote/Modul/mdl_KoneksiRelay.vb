Option Explicit On
Option Strict On

Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Text.Json

''' <summary>
''' Modul untuk menangani koneksi ke Relay Server (mode Internet).
''' Host mendaftar ke relay server dan menerima HostCode.
''' </summary>
Public Module mdl_KoneksiRelay

#Region "Variabel Koneksi Relay"

    ''' <summary>TcpClient untuk koneksi ke relay server</summary>
    Private TcpClientRelay As TcpClient

    ''' <summary>NetworkStream untuk komunikasi dengan relay</summary>
    Private StreamRelay As NetworkStream

    ''' <summary>CancellationTokenSource untuk menghentikan operasi async</summary>
    Private CtsRelay As CancellationTokenSource

    ''' <summary>Timer untuk heartbeat ke relay server</summary>
    Private TimerHeartbeat As System.Timers.Timer

    ''' <summary>Flag apakah sedang dalam proses koneksi</summary>
    Private SedangMenghubungkan As Boolean = False

    ''' <summary>Buffer untuk menerima data dari relay</summary>
    Private ReadOnly BufferRelay As Byte() = New Byte(65535) {}

#End Region

#Region "Events"

    ''' <summary>Event ketika berhasil terdaftar di relay dengan HostCode</summary>
    Public Event TerdaftarDiRelay(hostCode As String, expiryMinutes As Integer)

    ''' <summary>Event ketika koneksi relay terputus</summary>
    Public Event KoneksiRelayTerputus(alasan As String)

    ''' <summary>Event ketika ada permintaan koneksi via relay</summary>
    Public Event PermintaanKoneksiViaRelay(idSesi As String, namaTamu As String, alamatIP As String)

    ''' <summary>Event ketika ada error dari relay</summary>
    Public Event ErrorDariRelay(kodeError As Integer, pesan As String)

    ''' <summary>Event ketika ada paket dari relay yang perlu diproses</summary>
    Public Event PaketDariRelay(paket As cls_PaketData)

    ' === Events untuk Tamu ===
    ''' <summary>Event ketika hasil query host diterima</summary>
    Public Event HasilQueryHost(found As Boolean, namaHost As String, requiresPassword As Boolean, pesan As String)

    ''' <summary>Event ketika koneksi berhasil via relay (untuk Tamu)</summary>
    Public Event KoneksiBerhasilViaRelay(kunciSesi As String, izinKontrol As Boolean)

    ''' <summary>Event ketika koneksi ditolak via relay (untuk Tamu)</summary>
    Public Event KoneksiDitolakViaRelay(pesan As String)

    ''' <summary>Event ketika frame layar diterima (untuk Tamu)</summary>
    Public Event FrameDiterimaViaRelay(paket As cls_PaketData)

#End Region

#Region "Koneksi Ke Relay"

    ''' <summary>
    ''' Menghubungkan ke relay server dan mendaftarkan Host.
    ''' </summary>
    ''' <param name="password">Password opsional untuk koneksi ke Host ini</param>
    ''' <returns>True jika berhasil terhubung</returns>
    Public Async Function HubungkanKeRelayAsync(Optional password As String = "") As Task(Of Boolean)
        If SedangMenghubungkan Then Return False
        If TerhubungKeRelay Then Return True

        SedangMenghubungkan = True

        Try
            Console.WriteLine($"[RELAY] Menghubungkan ke {RelayServerIPAktif}:{PortRelayAktif}...")

            ' Buat koneksi TCP ke relay server
            TcpClientRelay = New TcpClient()
            TcpClientRelay.ReceiveBufferSize = 1024 * 1024 ' 1MB
            TcpClientRelay.SendBufferSize = 1024 * 1024

            ' Connect dengan timeout
            Dim connectTask = TcpClientRelay.ConnectAsync(RelayServerIPAktif, PortRelayAktif)
            Dim completedTask = Await Task.WhenAny(connectTask, Task.Delay(TIMEOUT_KONEKSI))
            If completedTask IsNot connectTask Then
                Throw New TimeoutException("Timeout menghubungkan ke relay server")
            End If

            StreamRelay = TcpClientRelay.GetStream()
            StreamRelay.ReadTimeout = 30000
            StreamRelay.WriteTimeout = 10000

            ' Kirim paket registrasi
            Dim paketRegister = BuatPaketRelayRegisterHost(NamaPerangkatIni, password)
            Await KirimPaketKeRelayAsync(paketRegister)

            ' Mulai mendengarkan paket dari relay
            CtsRelay = New CancellationTokenSource()
            Dim taskDengarkan = Task.Run(Function() DengarkanPaketDariRelayAsync(CtsRelay.Token))

            ' Mulai heartbeat timer
            MulaiHeartbeatRelay()

            TerhubungKeRelay = True
            ModeKoneksiSaatIni = ModeKoneksi.INTERNET
            SedangMenghubungkan = False

            Console.WriteLine("[RELAY] Terhubung ke relay server, menunggu HostCode...")
            Return True

        Catch ex As Exception
            Console.WriteLine($"[RELAY] Gagal menghubungkan: {ex.Message}")
            TutupKoneksiRelay()
            SedangMenghubungkan = False
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Menutup koneksi ke relay server.
    ''' </summary>
    Public Sub TutupKoneksiRelay()
        Try
            ' Hentikan heartbeat
            HentikanHeartbeatRelay()

            ' Cancel tasks
            If CtsRelay IsNot Nothing Then
                CtsRelay.Cancel()
                CtsRelay.Dispose()
                CtsRelay = Nothing
            End If

            ' Kirim unregister jika masih terhubung
            If TcpClientRelay IsNot Nothing AndAlso TcpClientRelay.Connected Then
                Try
                    Dim paketUnregister = BuatPaketRelayUnregisterHost()
                    Dim jsonStr = SerializePaket(paketUnregister)
                    Dim bytes = Encoding.UTF8.GetBytes(jsonStr)
                    StreamRelay?.Write(bytes, 0, bytes.Length)
                Catch
                    ' Ignore error saat unregister
                End Try
            End If

            ' Tutup stream dan client
            StreamRelay?.Close()
            StreamRelay?.Dispose()
            StreamRelay = Nothing

            TcpClientRelay?.Close()
            TcpClientRelay?.Dispose()
            TcpClientRelay = Nothing

            ' Reset state
            TerhubungKeRelay = False
            HostCodeAktif = ""

            Console.WriteLine("[RELAY] Koneksi ke relay ditutup")

        Catch ex As Exception
            Console.WriteLine($"[RELAY] Error saat tutup koneksi: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Kirim Paket"

    ''' <summary>
    ''' Mengirim paket ke relay server.
    ''' </summary>
    Public Async Function KirimPaketKeRelayAsync(paket As cls_PaketData) As Task(Of Boolean)
        If Not TerhubungKeRelay AndAlso paket.TipePaket <> TipePaket.RELAY_REGISTER_HOST Then
            Return False
        End If

        Try
            If TcpClientRelay Is Nothing OrElse Not TcpClientRelay.Connected Then
                Return False
            End If

            Dim jsonStr = SerializePaket(paket)
            Dim bytes = Encoding.UTF8.GetBytes(jsonStr)
            Await StreamRelay.WriteAsync(bytes, 0, bytes.Length)
            Await StreamRelay.FlushAsync()

            Return True

        Catch ex As Exception
            Console.WriteLine($"[RELAY] Error kirim paket: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

#Region "Dengarkan Paket"

    ''' <summary>
    ''' Loop mendengarkan paket dari relay server.
    ''' </summary>
    Private Async Function DengarkanPaketDariRelayAsync(ct As CancellationToken) As Task
        Dim buffer As New StringBuilder()

        Try
            While Not ct.IsCancellationRequested AndAlso TcpClientRelay IsNot Nothing AndAlso TcpClientRelay.Connected

                If StreamRelay Is Nothing OrElse Not StreamRelay.CanRead Then
                    Exit While
                End If

                ' Baca data dari stream
                Dim bytesRead = Await StreamRelay.ReadAsync(BufferRelay, 0, BufferRelay.Length, ct)

                If bytesRead = 0 Then
                    ' Koneksi terputus
                    Exit While
                End If

                ' Tambah ke buffer
                buffer.Append(Encoding.UTF8.GetString(BufferRelay, 0, bytesRead))

                ' Parse paket menggunakan bracket tracking
                Dim bufferStr = buffer.ToString()
                Dim startIdx = 0

                While startIdx < bufferStr.Length
                    ' Cari awal JSON object
                    Dim objStart = bufferStr.IndexOf("{"c, startIdx)
                    If objStart < 0 Then Exit While

                    ' Cari akhir JSON object dengan bracket tracking
                    Dim depth = 0
                    Dim objEnd = -1
                    Dim inString = False
                    Dim escape = False

                    For i = objStart To bufferStr.Length - 1
                        Dim c = bufferStr(i)

                        If escape Then
                            escape = False
                            Continue For
                        End If

                        If c = "\"c Then
                            escape = True
                            Continue For
                        End If

                        If c = """"c Then
                            inString = Not inString
                            Continue For
                        End If

                        If Not inString Then
                            If c = "{"c Then
                                depth += 1
                            ElseIf c = "}"c Then
                                depth -= 1
                                If depth = 0 Then
                                    objEnd = i
                                    Exit For
                                End If
                            End If
                        End If
                    Next

                    If objEnd < 0 Then
                        ' JSON belum lengkap, tunggu data berikutnya
                        Exit While
                    End If

                    ' Extract JSON dan parse
                    Dim jsonStr = bufferStr.Substring(objStart, objEnd - objStart + 1)
                    startIdx = objEnd + 1

                    Try
                        Dim paket = DeserializePaket(jsonStr)
                        If paket IsNot Nothing Then
                            Await ProsesPaketDariRelayAsync(paket)
                        End If
                    Catch ex As Exception
                        Console.WriteLine($"[RELAY] Error parse paket: {ex.Message}")
                    End Try
                End While

                ' Hapus data yang sudah diproses dari buffer
                If startIdx > 0 Then
                    buffer.Remove(0, startIdx)
                End If

            End While

        Catch ex As OperationCanceledException
            ' Normal saat cancel
        Catch ex As Exception
            Console.WriteLine($"[RELAY] Error dengarkan paket: {ex.Message}")
        End Try

        ' Koneksi terputus
        If Not ct.IsCancellationRequested Then
            TerhubungKeRelay = False
            HostCodeAktif = ""
            RaiseEvent KoneksiRelayTerputus("Koneksi ke relay server terputus")
        End If
    End Function

    ''' <summary>
    ''' Memproses paket yang diterima dari relay server.
    ''' </summary>
    Private Async Function ProsesPaketDariRelayAsync(paket As cls_PaketData) As Task
        Select Case paket.TipePaket

            Case TipePaket.RELAY_REGISTER_HOST_OK
                ' Registrasi berhasil, simpan HostCode
                Dim payload = DeserializeRegisterHostOK(paket.Payload)
                If payload IsNot Nothing Then
                    HostCodeAktif = payload.HostCode
                    Console.WriteLine($"[RELAY] Terdaftar dengan HostCode: {HostCodeAktif}")
                    RaiseEvent TerdaftarDiRelay(payload.HostCode, payload.ExpiryMinutes)
                End If

            Case TipePaket.PERMINTAAN_KONEKSI
                ' Ada permintaan koneksi dari Tamu via relay
                Dim payload = DeserializePermintaanKoneksi(paket.Payload)
                If payload IsNot Nothing Then
                    ' Simpan IdSesi dari paket
                    KunciSesiAktif = paket.IdSesi
                    Console.WriteLine($"[RELAY] Permintaan koneksi dari: {payload.NamaPerangkat} ({payload.AlamatIP})")
                    RaiseEvent PermintaanKoneksiViaRelay(paket.IdSesi, payload.NamaPerangkat, payload.AlamatIP)
                End If

            Case TipePaket.RELAY_ERROR
                ' Error dari relay
                Dim payload = DeserializeRelayError(paket.Payload)
                If payload IsNot Nothing Then
                    Console.WriteLine($"[RELAY] Error {payload.KodeError}: {payload.Pesan}")
                    RaiseEvent ErrorDariRelay(payload.KodeError, payload.Pesan)
                End If

            Case TipePaket.RELAY_HOST_OFFLINE, TipePaket.RELAY_INVALID_CODE
                ' Error spesifik
                Dim payload = DeserializeRelayError(paket.Payload)
                If payload IsNot Nothing Then
                    Console.WriteLine($"[RELAY] Error {paket.TipePaket}: {payload.Pesan}")
                    RaiseEvent ErrorDariRelay(CInt(paket.TipePaket), payload.Pesan)
                End If

            Case TipePaket.PERMINTAAN_STREAMING, TipePaket.HENTIKAN_STREAMING,
                 TipePaket.INPUT_KEYBOARD, TipePaket.INPUT_MOUSE,
                 TipePaket.TUTUP_KONEKSI, TipePaket.HEARTBEAT
                ' Paket dari Tamu via relay - forward ke handler yang ada (Host mode)
                RaiseEvent PaketDariRelay(paket)

            ' === Paket untuk Tamu ===
            Case TipePaket.RELAY_QUERY_HOST_RESULT
                ' Hasil query host (untuk Tamu)
                Dim payload = DeserializeQueryHostResult(paket.Payload)
                If payload IsNot Nothing Then
                    Console.WriteLine($"[RELAY-TAMU] Query result: Found={payload.Found}, Name={payload.NamaHost}")
                    RaiseEvent HasilQueryHost(payload.Found, payload.NamaHost, payload.RequiresPassword, payload.Pesan)
                End If

            Case TipePaket.RESPON_KONEKSI
                ' Respon koneksi dari Host (untuk Tamu via relay)
                Dim payload = DeserializeResponKoneksi(paket.Payload)
                If payload IsNot Nothing Then
                    If payload.Hasil = HasilPersetujuan.DITERIMA Then
                        KunciSesiAktif = payload.KunciSesi
                        Console.WriteLine($"[RELAY-TAMU] Koneksi diterima, kunci sesi: {KunciSesiAktif.Substring(0, 8)}...")
                        RaiseEvent KoneksiBerhasilViaRelay(payload.KunciSesi, payload.IzinKontrol)
                    Else
                        Console.WriteLine($"[RELAY-TAMU] Koneksi ditolak: {payload.Pesan}")
                        RaiseEvent KoneksiDitolakViaRelay(payload.Pesan)
                    End If
                End If

            Case TipePaket.FRAME_LAYAR
                ' Frame layar dari Host (untuk Tamu via relay)
                RaiseEvent FrameDiterimaViaRelay(paket)

            Case Else
                Console.WriteLine($"[RELAY] Paket tidak dikenal: {paket.TipePaket}")

        End Select

        Await Task.CompletedTask
    End Function

#End Region

#Region "Heartbeat"

    ''' <summary>
    ''' Memulai timer heartbeat ke relay server.
    ''' </summary>
    Private Sub MulaiHeartbeatRelay()
        If TimerHeartbeat IsNot Nothing Then
            TimerHeartbeat.Stop()
            TimerHeartbeat.Dispose()
        End If

        TimerHeartbeat = New System.Timers.Timer(INTERVAL_HEARTBEAT_RELAY)
        AddHandler TimerHeartbeat.Elapsed, AddressOf KirimHeartbeatRelay
        TimerHeartbeat.AutoReset = True
        TimerHeartbeat.Start()

        Console.WriteLine($"[RELAY] Heartbeat dimulai (interval: {INTERVAL_HEARTBEAT_RELAY}ms)")
    End Sub

    ''' <summary>
    ''' Menghentikan timer heartbeat.
    ''' </summary>
    Private Sub HentikanHeartbeatRelay()
        If TimerHeartbeat IsNot Nothing Then
            TimerHeartbeat.Stop()
            TimerHeartbeat.Dispose()
            TimerHeartbeat = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Event handler untuk mengirim heartbeat.
    ''' </summary>
    Private Async Sub KirimHeartbeatRelay(sender As Object, e As System.Timers.ElapsedEventArgs)
        If Not TerhubungKeRelay Then Return

        Try
            Dim paketHeartbeat = BuatPaketRelayHeartbeat()
            Await KirimPaketKeRelayAsync(paketHeartbeat)
        Catch ex As Exception
            Console.WriteLine($"[RELAY] Error kirim heartbeat: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Kirim Respon Koneksi"

    ''' <summary>
    ''' Mengirim respon persetujuan koneksi ke Tamu via relay.
    ''' </summary>
    ''' <param name="idSesi">ID sesi dari permintaan</param>
    ''' <param name="diterima">True jika diterima</param>
    ''' <param name="izinKontrol">True jika izinkan kontrol keyboard/mouse</param>
    ''' <param name="pesan">Pesan opsional</param>
    Public Async Function KirimResponKoneksiViaRelayAsync(idSesi As String, diterima As Boolean,
                                                          izinKontrol As Boolean, Optional pesan As String = "") As Task(Of Boolean)
        Try
            ' Buat payload respon
            Dim hasil = If(diterima, HasilPersetujuan.DITERIMA, HasilPersetujuan.DITOLAK)
            Dim kunciSesi = If(diterima, AcakKarakter(32), "")

            Dim paketRespon = BuatPaketResponKoneksi(hasil, kunciSesi, pesan, izinKontrol)
            paketRespon.IdSesi = idSesi

            ' Kirim via relay
            Dim berhasil = Await KirimPaketKeRelayAsync(paketRespon)

            If berhasil AndAlso diterima Then
                KunciSesiAktif = kunciSesi
            End If

            Return berhasil

        Catch ex As Exception
            Console.WriteLine($"[RELAY] Error kirim respon koneksi: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

#Region "Helper Deserialize Relay"

    ''' <summary>
    ''' Deserialize payload RELAY_ERROR.
    ''' </summary>
    Public Function DeserializeRelayError(json As String) As cls_PayloadRelayError
        Try
            Return JsonSerializer.Deserialize(Of cls_PayloadRelayError)(json, New JsonSerializerOptions With {
                .PropertyNameCaseInsensitive = True
            })
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Koneksi Tamu via Relay"

    ''' <summary>
    ''' Menghubungkan ke relay server sebagai Tamu (tanpa registrasi Host).
    ''' </summary>
    ''' <returns>True jika berhasil terhubung</returns>
    Public Async Function HubungkanKeRelayAsTamuAsync() As Task(Of Boolean)
        If SedangMenghubungkan Then Return False
        If TerhubungKeRelay Then Return True

        SedangMenghubungkan = True

        Try
            Console.WriteLine($"[RELAY-TAMU] Menghubungkan ke {RelayServerIPAktif}:{PortRelayAktif}...")

            ' Buat koneksi TCP ke relay server
            TcpClientRelay = New TcpClient()
            TcpClientRelay.ReceiveBufferSize = 1024 * 1024 ' 1MB
            TcpClientRelay.SendBufferSize = 1024 * 1024

            ' Connect dengan timeout
            Dim connectTask = TcpClientRelay.ConnectAsync(RelayServerIPAktif, PortRelayAktif)
            Dim completedTask = Await Task.WhenAny(connectTask, Task.Delay(TIMEOUT_KONEKSI))
            If completedTask IsNot connectTask Then
                Throw New TimeoutException("Timeout menghubungkan ke relay server")
            End If

            StreamRelay = TcpClientRelay.GetStream()
            StreamRelay.ReadTimeout = 30000
            StreamRelay.WriteTimeout = 10000

            ' Mulai mendengarkan paket dari relay
            CtsRelay = New CancellationTokenSource()
            Dim taskDengarkan = Task.Run(Function() DengarkanPaketDariRelayAsync(CtsRelay.Token))

            TerhubungKeRelay = True
            ModeKoneksiSaatIni = ModeKoneksi.INTERNET
            SedangMenghubungkan = False

            Console.WriteLine("[RELAY-TAMU] Terhubung ke relay server")
            Return True

        Catch ex As Exception
            Console.WriteLine($"[RELAY-TAMU] Gagal menghubungkan: {ex.Message}")
            TutupKoneksiRelay()
            SedangMenghubungkan = False
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Query apakah Host dengan HostCode tertentu online.
    ''' </summary>
    ''' <param name="hostCode">HostCode 6 karakter</param>
    ''' <returns>True jika query terkirim</returns>
    Public Async Function QueryHostAsync(hostCode As String) As Task(Of Boolean)
        If Not TerhubungKeRelay Then
            ' Connect dulu jika belum
            Dim connected = Await HubungkanKeRelayAsTamuAsync()
            If Not connected Then Return False
        End If

        Try
            Dim paketQuery = BuatPaketRelayQueryHost(hostCode.ToUpper().Trim())
            Return Await KirimPaketKeRelayAsync(paketQuery)
        Catch ex As Exception
            Console.WriteLine($"[RELAY-TAMU] Error query host: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Meminta koneksi ke Host via relay.
    ''' </summary>
    ''' <param name="hostCode">HostCode tujuan</param>
    ''' <param name="password">Password (jika Host memerlukan)</param>
    ''' <returns>True jika request terkirim</returns>
    Public Async Function MintaKoneksiViaRelayAsync(hostCode As String, Optional password As String = "") As Task(Of Boolean)
        If Not TerhubungKeRelay Then Return False

        Try
            Dim paketRequest = BuatPaketRelayConnectRequest(hostCode.ToUpper().Trim(), NamaPerangkatIni, password)
            Return Await KirimPaketKeRelayAsync(paketRequest)
        Catch ex As Exception
            Console.WriteLine($"[RELAY-TAMU] Error minta koneksi: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Mengirim permintaan streaming ke Host via relay.
    ''' </summary>
    Public Async Function MintaStreamingViaRelayAsync() As Task(Of Boolean)
        If Not TerhubungKeRelay OrElse String.IsNullOrEmpty(KunciSesiAktif) Then Return False

        Try
            Dim paket = BuatPaketPermintaanStreaming(KunciSesiAktif)
            Return Await KirimPaketKeRelayAsync(paket)
        Catch ex As Exception
            Console.WriteLine($"[RELAY-TAMU] Error minta streaming: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Mengirim input keyboard ke Host via relay.
    ''' </summary>
    Public Async Function KirimInputKeyboardViaRelayAsync(keyCode As Integer, isKeyDown As Boolean,
                                                           isExtended As Boolean, modifiers As Integer) As Task(Of Boolean)
        If Not TerhubungKeRelay OrElse String.IsNullOrEmpty(KunciSesiAktif) Then Return False

        Try
            Dim paket = BuatPaketInputKeyboard(keyCode, isKeyDown, isExtended, modifiers)
            paket.IdSesi = KunciSesiAktif
            Return Await KirimPaketKeRelayAsync(paket)
        Catch ex As Exception
            Console.WriteLine($"[RELAY-TAMU] Error kirim keyboard: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Mengirim input mouse ke Host via relay.
    ''' </summary>
    Public Async Function KirimInputMouseViaRelayAsync(tipeAksi As TipeAksiMouse, x As Double, y As Double,
                                                        button As Integer, isButtonDown As Boolean,
                                                        wheelDelta As Integer) As Task(Of Boolean)
        If Not TerhubungKeRelay OrElse String.IsNullOrEmpty(KunciSesiAktif) Then Return False

        Try
            Dim paket = BuatPaketInputMouse(tipeAksi, x, y, button, isButtonDown, wheelDelta)
            paket.IdSesi = KunciSesiAktif
            Return Await KirimPaketKeRelayAsync(paket)
        Catch ex As Exception
            Console.WriteLine($"[RELAY-TAMU] Error kirim mouse: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

End Module
