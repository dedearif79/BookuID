' =====================================================================
' mdl_UdpStreaming.vb
' Modul untuk UDP video streaming
'
' Arsitektur:
' - TCP (port 45679/45680): Control plane (login, input, heartbeat)
' - UDP (port 45681): Data plane (video frames)
'
' Host: Kirim frame via UDP
' Tamu: Terima frame via UDP, reassembly, render
' Relay: Forward UDP berdasarkan SessionId
' =====================================================================

Option Explicit On
Option Strict On

Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports Booku_Remote

Public Module mdl_UdpStreaming

#Region "Variabel Global"

    ''' <summary>UDP client untuk mengirim frame (Host) atau menerima frame (Tamu)</summary>
    Private _udpClient As UdpClient = Nothing

    ''' <summary>Flag apakah UDP streaming aktif</summary>
    Public UdpStreamingAktif As Boolean = False

    ''' <summary>CancellationTokenSource untuk stop streaming</summary>
    Private _ctsUdpStreaming As CancellationTokenSource = Nothing

    ''' <summary>Frame assembler untuk Tamu (reassembly chunks)</summary>
    Private _frameAssembler As Booku_Remote.cls_UdpFrameAssembler = Nothing

    ''' <summary>Counter untuk FrameId (Host side)</summary>
    Private _frameIdCounter As Integer = 0

    ''' <summary>SessionId untuk routing (dari IdSesiRelay atau hash dari KunciSesiAktif)</summary>
    Public UdpSessionId As Integer = 0

    ''' <summary>Endpoint tujuan untuk mengirim UDP (Host side)</summary>
    Private _targetEndpoint As IPEndPoint = Nothing

    ''' <summary>Endpoint relay untuk UDP (jika via relay)</summary>
    Private _relayUdpEndpoint As IPEndPoint = Nothing

    ''' <summary>H.264 encoder instance (Host side)</summary>
    Private _h264Encoder As Booku_Remote.cls_H264Encoder = Nothing

    ''' <summary>NAL parser untuk memisahkan NAL units dari H.264 stream</summary>
    Private _nalParser As Booku_Remote.cls_NalParser = Nothing

    ''' <summary>Codec aktif yang digunakan untuk streaming saat ini</summary>
    Public CodecStreaming As TipeKodek = TipeKodek.JPEG

    ''' <summary>Flag apakah H.264 encoder sedang aktif</summary>
    Public H264EncoderAktif As Boolean = False

    ''' <summary>Flag apakah keyframe pertama dengan SPS/PPS sudah dikirim (Host side)</summary>
    Private _firstKeyframeSent As Boolean = False

    ''' <summary>H.264 decoder instance (Tamu side)</summary>
    Private _h264Decoder As Booku_Remote.cls_H264Decoder = Nothing

    ''' <summary>Flag apakah H.264 decoder sedang aktif</summary>
    Public H264DecoderAktif As Boolean = False

    ''' <summary>Dimensi video yang diharapkan untuk decoder (set saat terima first frame)</summary>
    Private _decoderWidth As Integer = 0
    Private _decoderHeight As Integer = 0

#End Region

#Region "Events"

    ''' <summary>Event ketika frame JPEG diterima dan siap di-render</summary>
    Public Event FrameUdpDiterima(frameData As Byte(), frameId As Integer, timestampMs As Integer)

    ''' <summary>Event ketika frame JPEG diterima dengan info codec (untuk backward compat, frameData = JPEG)</summary>
    Public Event FrameUdpDiterimaEx(frameData As Byte(), frameId As Integer, timestampMs As Integer, codecType As Byte)

    ''' <summary>Event ketika frame BGRA diterima (hasil decode H.264, siap di-render)</summary>
    Public Event FrameBgraDiterima(bgraData As Byte(), width As Integer, height As Integer)

    ''' <summary>Event untuk statistik UDP</summary>
    Public Event StatistikUdp(packetsReceived As Integer, packetsDropped As Integer, fps As Double)

#End Region

#Region "Statistik"

    Private _packetsReceived As Integer = 0
    Private _packetsDropped As Integer = 0
    Private _framesRendered As Integer = 0
    Private _lastStatTime As DateTime = DateTime.UtcNow

#End Region

#Region "Host - UDP Sender"

    ''' <summary>
    ''' Mulai UDP sender untuk Host (mengirim frame ke Tamu)
    ''' </summary>
    ''' <param name="targetIP">IP address tujuan (Tamu untuk LAN, Relay untuk Internet)</param>
    ''' <param name="targetPort">Port UDP tujuan</param>
    ''' <param name="sessionId">Session ID untuk routing</param>
    Public Sub MulaiUdpSender(targetIP As String, targetPort As Integer, sessionId As Integer)
        Try
            ' Stop jika sudah ada
            HentikanUdpStreaming()

            ' Setup
            UdpSessionId = sessionId
            _frameIdCounter = 0
            _targetEndpoint = New IPEndPoint(IPAddress.Parse(targetIP), targetPort)

            ' Buat UDP client (bind ke port random untuk send)
            _udpClient = New UdpClient()
            _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, True)

            UdpStreamingAktif = True
            Console.WriteLine($"[UDP-HOST] Sender dimulai, target: {targetIP}:{targetPort}, SessionId: {sessionId}")

        Catch ex As Exception
            Console.WriteLine($"[UDP-HOST] Error mulai sender: {ex.Message}")
            UdpStreamingAktif = False
        End Try
    End Sub

    ''' <summary>
    ''' Kirim satu frame JPEG via UDP (dipanggil dari loop streaming Host)
    ''' </summary>
    ''' <param name="jpegData">Data JPEG frame</param>
    Public Async Function KirimFrameUdpAsync(jpegData As Byte()) As Task(Of Boolean)
        If Not UdpStreamingAktif OrElse _udpClient Is Nothing OrElse _targetEndpoint Is Nothing Then
            Return False
        End If

        Try
            ' Increment frame ID
            _frameIdCounter += 1

            ' Pecah frame menjadi chunks
            Dim packets = Booku_Remote.cls_UdpFrameChunker.ChunkFrame(UdpSessionId, _frameIdCounter, jpegData)

            ' Kirim semua chunks
            For Each packet In packets
                Dim data = packet.ToBytes()
                Await _udpClient.SendAsync(data, data.Length, _targetEndpoint)
            Next

            Return True

        Catch ex As Exception
            Console.WriteLine($"[UDP-HOST] Error kirim frame: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

#Region "Host - H.264 Encoder"

    ''' <summary>
    ''' Mulai H.264 encoder untuk streaming.
    ''' </summary>
    ''' <param name="width">Lebar video</param>
    ''' <param name="height">Tinggi video</param>
    ''' <param name="fps">Target FPS</param>
    ''' <returns>True jika berhasil dimulai</returns>
    Public Function MulaiH264Encoder(width As Integer, height As Integer, fps As Integer) As Boolean
        Try
            ' Cek FFmpeg tersedia
            If Not mdl_FFmpegManager.FFmpegTersedia Then
                Console.WriteLine("[H264] FFmpeg tidak tersedia, fallback ke JPEG")
                CodecStreaming = TipeKodek.JPEG
                Return False
            End If

            ' Stop encoder lama jika ada
            HentikanH264Encoder()

            ' Buat encoder dan NAL parser baru
            _h264Encoder = New Booku_Remote.cls_H264Encoder()
            _nalParser = New Booku_Remote.cls_NalParser()

            ' Subscribe ke event DataReady
            AddHandler _h264Encoder.DataReady, AddressOf OnH264DataReady
            AddHandler _h264Encoder.EncoderError, AddressOf OnH264EncoderError
            AddHandler _h264Encoder.EncoderStopped, AddressOf OnH264EncoderStopped

            ' Start encoder
            If _h264Encoder.Start(width, height, fps) Then
                H264EncoderAktif = True
                CodecStreaming = TipeKodek.H264
                _firstKeyframeSent = False  ' Reset flag
                Console.WriteLine($"[H264] Encoder started: {width}x{height} @ {fps}fps")
                Return True
            Else
                Console.WriteLine("[H264] Failed to start encoder, fallback ke JPEG")
                HentikanH264Encoder()
                CodecStreaming = TipeKodek.JPEG
                Return False
            End If

        Catch ex As Exception
            Console.WriteLine($"[H264] Error mulai encoder: {ex.Message}")
            HentikanH264Encoder()
            CodecStreaming = TipeKodek.JPEG
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Menghentikan H.264 encoder.
    ''' </summary>
    Public Sub HentikanH264Encoder()
        Try
            If _h264Encoder IsNot Nothing Then
                RemoveHandler _h264Encoder.DataReady, AddressOf OnH264DataReady
                RemoveHandler _h264Encoder.EncoderError, AddressOf OnH264EncoderError
                RemoveHandler _h264Encoder.EncoderStopped, AddressOf OnH264EncoderStopped
                _h264Encoder.Dispose()
                _h264Encoder = Nothing
            End If

            If _nalParser IsNot Nothing Then
                _nalParser.Reset()
                _nalParser = Nothing
            End If

            H264EncoderAktif = False
            Console.WriteLine("[H264] Encoder stopped")

        Catch ex As Exception
            Console.WriteLine($"[H264] Error menghentikan encoder: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim raw BGRA frame ke H.264 encoder.
    ''' Data H.264 akan dikirim via UDP saat event DataReady triggered.
    ''' </summary>
    ''' <param name="bgraData">Raw BGRA pixel data</param>
    ''' <returns>True jika berhasil dikirim ke encoder</returns>
    Public Async Function KirimFrameKeEncoderAsync(bgraData As Byte()) As Task(Of Boolean)
        If Not H264EncoderAktif OrElse _h264Encoder Is Nothing Then
            Return False
        End If

        Return Await _h264Encoder.SendFrameAsync(bgraData)
    End Function

    ''' <summary>
    ''' Handler untuk event DataReady dari H.264 encoder.
    ''' Parse NAL units dan kirim via UDP.
    ''' </summary>
    Private Sub OnH264DataReady(sender As Object, e As Booku_Remote.H264DataEventArgs)
        System.Diagnostics.Debug.WriteLine($"[H264-DATA] Event received: {e.Data.Length} bytes")

        If Not UdpStreamingAktif OrElse _nalParser Is Nothing Then
            System.Diagnostics.Debug.WriteLine($"[H264-DATA] DataReady ignored: UdpActive={UdpStreamingAktif}, Parser={(If(_nalParser IsNot Nothing, "OK", "NULL"))}")
            Return
        End If

        Try
            ' Parse NAL units dari data
            Dim nalUnits = _nalParser.ParseData(e.Data)

            System.Diagnostics.Debug.WriteLine($"[H264-DATA] Parsed {nalUnits.Count} NAL units from {e.Data.Length} bytes")

            For Each nalUnit In nalUnits
                ' Log NAL type untuk debugging
                System.Diagnostics.Debug.WriteLine($"[H264-NAL] Type={nalUnit.NalType} ({nalUnit.TypeName}), Size={nalUnit.Size}, IsKeyframe={nalUnit.IsKeyframe}, IsParam={nalUnit.IsParameterSet}")

                ' Skip parameter sets saja (akan di-include dengan keyframe)
                If nalUnit.IsParameterSet Then
                    System.Diagnostics.Debug.WriteLine($"[H264-DATA] Storing parameter set (type={nalUnit.NalType})")
                    Continue For
                End If

                ' PENTING: Skip non-keyframe sampai keyframe pertama terkirim
                ' Decoder tidak bisa decode P/B-frame tanpa I-frame sebelumnya
                If Not _firstKeyframeSent AndAlso Not nalUnit.IsKeyframe Then
                    System.Diagnostics.Debug.WriteLine($"[H264-DATA] Skipping non-keyframe (type={nalUnit.NalType}) - waiting for first keyframe")
                    Continue For
                End If

                ' Untuk keyframe, include SPS+PPS
                Dim dataToSend As Byte()
                If nalUnit.IsKeyframe AndAlso _nalParser.HasParameterSets Then
                    Dim isFirstKeyframe = Not _firstKeyframeSent
                    dataToSend = _nalParser.CreateAccessUnitWithParams(nalUnit)
                    _firstKeyframeSent = True  ' Mark keyframe sent
                    System.Diagnostics.Debug.WriteLine($"[H264-DATA] IDR frame with SPS+PPS prepended: {dataToSend.Length} bytes (FIRST={isFirstKeyframe})")
                Else
                    dataToSend = nalUnit.RawData
                    System.Diagnostics.Debug.WriteLine($"[H264-DATA] Non-IDR frame: {dataToSend.Length} bytes (type={nalUnit.NalType})")
                End If

                ' Kirim NAL unit sebagai frame
                KirimH264NalUnitViaUdp(dataToSend, nalUnit.IsKeyframe)
            Next

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"[H264-DATA] Error processing NAL data: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim satu NAL unit via UDP (dengan CodecType = H264).
    ''' </summary>
    Private Sub KirimH264NalUnitViaUdp(nalData As Byte(), isKeyframe As Boolean)
        If Not UdpStreamingAktif OrElse _udpClient Is Nothing Then
            System.Diagnostics.Debug.WriteLine($"[H264-SEND] Cannot send: UdpActive={UdpStreamingAktif}, Client={(If(_udpClient IsNot Nothing, "OK", "NULL"))}")
            Return
        End If
        If nalData Is Nothing OrElse nalData.Length = 0 Then Return

        Try
            _frameIdCounter += 1

            ' Pecah NAL unit menjadi chunks dengan codec type H264
            Dim packets = Booku_Remote.cls_UdpFrameChunker.ChunkFrame(
                UdpSessionId, _frameIdCounter, nalData, Booku_Remote.UdpConstants.CODEC_TYPE_H264)

            ' Tentukan endpoint (langsung atau via relay)
            Dim endpoint = If(_relayUdpEndpoint, _targetEndpoint)
            If endpoint Is Nothing Then
                System.Diagnostics.Debug.WriteLine($"[H264-SEND] No endpoint! Relay={If(_relayUdpEndpoint IsNot Nothing, _relayUdpEndpoint.ToString(), "NULL")}, Target={If(_targetEndpoint IsNot Nothing, _targetEndpoint.ToString(), "NULL")}")
                Return
            End If

            ' Kirim semua chunks
            For Each packet In packets
                Dim data = packet.ToBytes()
                _udpClient.Send(data, data.Length, endpoint)
            Next

            ' Log pengiriman (setiap 10 frame untuk debugging)
            If _frameIdCounter Mod 10 = 1 OrElse _frameIdCounter <= 20 Then
                System.Diagnostics.Debug.WriteLine($"[H264-SEND] Sent NAL #{_frameIdCounter}: {nalData.Length} bytes ({packets.Count} chunks) to {endpoint}, keyframe={isKeyframe}")
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"[H264-SEND] Error kirim NAL unit: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Handler untuk error encoder.
    ''' </summary>
    Private Sub OnH264EncoderError(sender As Object, e As Booku_Remote.EncoderErrorEventArgs)
        Console.WriteLine($"[H264] Encoder error: {e.Message}")
        ' Fallback ke JPEG
        CodecStreaming = TipeKodek.JPEG
        H264EncoderAktif = False
    End Sub

    ''' <summary>
    ''' Handler untuk encoder stopped.
    ''' </summary>
    Private Sub OnH264EncoderStopped(sender As Object, e As EventArgs)
        Console.WriteLine("[H264] Encoder stopped event received")
        H264EncoderAktif = False
    End Sub

#End Region

#Region "Tamu - H.264 Decoder"

    ''' <summary>
    ''' Mulai H.264 decoder untuk Tamu dengan resolusi tetap.
    ''' </summary>
    ''' <param name="width">Lebar video output</param>
    ''' <param name="height">Tinggi video output</param>
    ''' <returns>True jika berhasil dimulai</returns>
    Public Function MulaiH264Decoder(width As Integer, height As Integer) As Boolean
        Try
            ' Cek FFmpeg tersedia
            If Not mdl_FFmpegManager.FFmpegTersedia Then
                Console.WriteLine("[H264-DEC] FFmpeg tidak tersedia")
                Return False
            End If

            ' Stop decoder lama jika ada
            HentikanH264Decoder()

            ' Simpan dimensi
            _decoderWidth = width
            _decoderHeight = height

            ' Buat decoder baru
            _h264Decoder = New Booku_Remote.cls_H264Decoder()

            ' Subscribe ke events
            AddHandler _h264Decoder.FrameReady, AddressOf OnH264FrameDecoded
            AddHandler _h264Decoder.DecoderError, AddressOf OnH264DecoderError
            AddHandler _h264Decoder.DecoderStopped, AddressOf OnH264DecoderStopped

            ' Start decoder
            If _h264Decoder.Start(width, height) Then
                H264DecoderAktif = True
                Console.WriteLine($"[H264-DEC] Decoder started: {width}x{height}")
                Return True
            Else
                Console.WriteLine("[H264-DEC] Failed to start decoder")
                HentikanH264Decoder()
                Return False
            End If

        Catch ex As Exception
            Console.WriteLine($"[H264-DEC] Error mulai decoder: {ex.Message}")
            HentikanH264Decoder()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Mulai H.264 decoder untuk Tamu dengan auto-deteksi resolusi.
    ''' FFmpeg akan mendeteksi resolusi dari SPS dalam H.264 stream.
    ''' </summary>
    ''' <returns>True jika berhasil dimulai</returns>
    Public Function MulaiH264DecoderAutoResolusi() As Boolean
        Try
            ' Cek FFmpeg tersedia
            If Not mdl_FFmpegManager.FFmpegTersedia Then
                Console.WriteLine("[H264-DEC] FFmpeg tidak tersedia")
                Return False
            End If

            ' Stop decoder lama jika ada
            HentikanH264Decoder()

            ' Reset dimensi (akan di-detect dari stream)
            _decoderWidth = 0
            _decoderHeight = 0

            ' Buat decoder baru
            _h264Decoder = New Booku_Remote.cls_H264Decoder()

            ' Subscribe ke events
            AddHandler _h264Decoder.FrameReady, AddressOf OnH264FrameDecoded
            AddHandler _h264Decoder.DecoderError, AddressOf OnH264DecoderError
            AddHandler _h264Decoder.DecoderStopped, AddressOf OnH264DecoderStopped

            ' Start decoder dengan auto-resolusi
            If _h264Decoder.StartAutoResolusi() Then
                H264DecoderAktif = True
                Console.WriteLine("[H264-DEC] Decoder started with auto-resolution detection")
                Return True
            Else
                Console.WriteLine("[H264-DEC] Failed to start auto-res decoder")
                HentikanH264Decoder()
                Return False
            End If

        Catch ex As Exception
            Console.WriteLine($"[H264-DEC] Error mulai auto-res decoder: {ex.Message}")
            HentikanH264Decoder()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Menghentikan H.264 decoder.
    ''' </summary>
    Public Sub HentikanH264Decoder()
        Try
            If _h264Decoder IsNot Nothing Then
                RemoveHandler _h264Decoder.FrameReady, AddressOf OnH264FrameDecoded
                RemoveHandler _h264Decoder.DecoderError, AddressOf OnH264DecoderError
                RemoveHandler _h264Decoder.DecoderStopped, AddressOf OnH264DecoderStopped
                _h264Decoder.Dispose()
                _h264Decoder = Nothing
            End If

            H264DecoderAktif = False
            _decoderWidth = 0
            _decoderHeight = 0
            Console.WriteLine("[H264-DEC] Decoder stopped")

        Catch ex As Exception
            Console.WriteLine($"[H264-DEC] Error menghentikan decoder: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim H.264 data ke decoder untuk di-decode.
    ''' </summary>
    Private Sub DecodeH264Data(h264Data As Byte())
        If Not H264DecoderAktif OrElse _h264Decoder Is Nothing Then Return
        If h264Data Is Nothing OrElse h264Data.Length = 0 Then Return

        ' Kirim ke decoder (async)
        _h264Decoder.SendData(h264Data)
    End Sub

    ''' <summary>
    ''' Handler untuk event FrameReady dari H.264 decoder.
    ''' </summary>
    Private Sub OnH264FrameDecoded(sender As Object, e As Booku_Remote.DecodedFrameEventArgs)
        ' Raise event untuk viewer
        RaiseEvent FrameBgraDiterima(e.BgraData, e.Width, e.Height)
        _framesRendered += 1
    End Sub

    ''' <summary>
    ''' Handler untuk error decoder.
    ''' </summary>
    Private Sub OnH264DecoderError(sender As Object, e As Booku_Remote.DecoderErrorEventArgs)
        Console.WriteLine($"[H264-DEC] Decoder error: {e.Message}")
    End Sub

    ''' <summary>
    ''' Handler untuk decoder stopped.
    ''' </summary>
    Private Sub OnH264DecoderStopped(sender As Object, e As EventArgs)
        Console.WriteLine("[H264-DEC] Decoder stopped event received")
        H264DecoderAktif = False
    End Sub

#End Region

#Region "Tamu - UDP Receiver"

    ''' <summary>
    ''' Mulai UDP receiver untuk Tamu (menerima frame dari Host)
    ''' </summary>
    ''' <param name="listenPort">Port UDP untuk listen</param>
    ''' <param name="sessionId">Session ID untuk filter</param>
    Public Async Function MulaiUdpReceiverAsync(listenPort As Integer, sessionId As Integer) As Task
        Try
            ' Stop jika sudah ada
            HentikanUdpStreaming()

            ' Setup
            UdpSessionId = sessionId
            _frameAssembler = New Booku_Remote.cls_UdpFrameAssembler()
            _ctsUdpStreaming = New CancellationTokenSource()

            ' Reset statistik
            _packetsReceived = 0
            _packetsDropped = 0
            _framesRendered = 0
            _lastStatTime = DateTime.UtcNow

            ' Buat UDP client untuk listen
            _udpClient = New UdpClient(listenPort)
            _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, True)
            _udpClient.Client.ReceiveTimeout = 100 ' 100ms timeout untuk non-blocking

            UdpStreamingAktif = True
            Console.WriteLine($"[UDP-TAMU] Receiver dimulai, port: {listenPort}, SessionId: {sessionId}")

            ' Mulai loop receive di background
            Await Task.Run(Sub() LoopTerimaUdp(_ctsUdpStreaming.Token))

        Catch ex As Exception
            Console.WriteLine($"[UDP-TAMU] Error mulai receiver: {ex.Message}")
            UdpStreamingAktif = False
        End Try
    End Function

    ''' <summary>
    ''' Loop menerima packet UDP (berjalan di background thread)
    ''' </summary>
    Private Sub LoopTerimaUdp(token As CancellationToken)
        Dim remoteEP As IPEndPoint = Nothing
        Dim orderedModeEnabled As Boolean = False  ' Flag untuk aktifkan ordered mode sekali saja

        While UdpStreamingAktif AndAlso Not token.IsCancellationRequested
            Try
                ' Non-blocking receive dengan timeout
                If _udpClient Is Nothing Then Exit While

                Dim data As Byte() = Nothing
                Try
                    data = _udpClient.Receive(remoteEP)
                Catch ex As SocketException When ex.SocketErrorCode = SocketError.TimedOut
                    ' Timeout adalah normal, lanjut loop
                    Continue While
                End Try

                If data Is Nothing OrElse data.Length < Booku_Remote.UdpConstants.UDP_HEADER_SIZE Then
                    Continue While
                End If

                ' Parse packet
                Dim packet = Booku_Remote.cls_UdpPacket.FromBytes(data)
                If packet Is Nothing Then
                    _packetsDropped += 1
                    Continue While
                End If

                ' Filter by SessionId (keamanan)
                If packet.SessionId <> UdpSessionId Then
                    _packetsDropped += 1
                    Continue While
                End If

                _packetsReceived += 1

                ' PENTING: Aktifkan ordered mode saat H.264 pertama kali terdeteksi
                ' H.264 NAL units HARUS diproses berurutan untuk decoding yang benar
                If Not orderedModeEnabled AndAlso packet.CodecType = Booku_Remote.UdpConstants.CODEC_TYPE_H264 Then
                    _frameAssembler.SetOrderedMode(True)
                    orderedModeEnabled = True
                    Console.WriteLine("[UDP-TAMU] H.264 detected, enabled ordered delivery mode")
                End If

                ' Tambahkan ke assembler dengan info codec
                Dim frameData As Byte() = Nothing
                Dim codecType As Byte = Booku_Remote.UdpConstants.CODEC_TYPE_JPEG

                If _frameAssembler.AddPacketWithCodec(packet, frameData, codecType) Then
                    ' Frame lengkap - route berdasarkan codec type
                    If codecType = Booku_Remote.UdpConstants.CODEC_TYPE_H264 Then
                        ' H.264: Kirim ke decoder
                        If Not H264DecoderAktif Then
                            ' Start decoder dengan auto-deteksi resolusi dari SPS
                            MulaiH264DecoderAutoResolusi()
                        End If
                        DecodeH264Data(frameData)
                    Else
                        ' JPEG: Raise event langsung
                        _framesRendered += 1
                        RaiseEvent FrameUdpDiterima(frameData, packet.FrameId, packet.TimestampMs)
                    End If
                End If

                ' Update statistik setiap 1 detik
                If (DateTime.UtcNow - _lastStatTime).TotalSeconds >= 1 Then
                    Dim fps = _framesRendered / (DateTime.UtcNow - _lastStatTime).TotalSeconds
                    RaiseEvent StatistikUdp(_packetsReceived, _packetsDropped, fps)
                    _framesRendered = 0
                    _lastStatTime = DateTime.UtcNow
                End If

            Catch ex As ObjectDisposedException
                ' UDP client sudah disposed, keluar
                Exit While
            Catch ex As Exception
                Console.WriteLine($"[UDP-TAMU] Error receive: {ex.Message}")
            End Try
        End While

        Console.WriteLine("[UDP-TAMU] Loop receive berakhir")
    End Sub

#End Region

#Region "Relay - UDP Forwarder"

    ''' <summary>
    ''' Setup endpoint relay untuk forwarding UDP (dipanggil setelah session established)
    ''' </summary>
    ''' <param name="relayIP">IP address relay server</param>
    ''' <param name="relayUdpPort">Port UDP relay</param>
    Public Sub SetupRelayUdpEndpoint(relayIP As String, relayUdpPort As Integer)
        Try
            _relayUdpEndpoint = New IPEndPoint(IPAddress.Parse(relayIP), relayUdpPort)
            Console.WriteLine($"[UDP] Relay endpoint set: {relayIP}:{relayUdpPort}")
        Catch ex As Exception
            Console.WriteLine($"[UDP] Error setup relay endpoint: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Kirim frame via relay (Host side, untuk mode Internet)
    ''' </summary>
    Public Async Function KirimFrameViaRelayAsync(jpegData As Byte()) As Task(Of Boolean)
        If Not UdpStreamingAktif OrElse _udpClient Is Nothing OrElse _relayUdpEndpoint Is Nothing Then
            Return False
        End If

        Try
            _frameIdCounter += 1
            Dim packets = Booku_Remote.cls_UdpFrameChunker.ChunkFrame(UdpSessionId, _frameIdCounter, jpegData)

            For Each packet In packets
                Dim data = packet.ToBytes()
                Await _udpClient.SendAsync(data, data.Length, _relayUdpEndpoint)
            Next

            Return True

        Catch ex As Exception
            Console.WriteLine($"[UDP-HOST] Error kirim frame via relay: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Kirim registration packet ke relay agar relay tahu endpoint Tamu.
    ''' Dipanggil oleh Tamu setelah memulai UDP receiver di mode Internet.
    ''' Paket registration = header-only dengan FrameId=0.
    ''' </summary>
    ''' <param name="relayIP">IP address relay server</param>
    ''' <param name="relayUdpPort">Port UDP relay</param>
    ''' <param name="sessionId">Session ID untuk routing</param>
    ''' <returns>True jika berhasil, False jika gagal</returns>
    Public Async Function KirimRegistrasiKeRelayAsync(relayIP As String, relayUdpPort As Integer, sessionId As Integer) As Task(Of Boolean)
        If _udpClient Is Nothing Then
            Console.WriteLine("[UDP-TAMU] Cannot send registration: UDP client not initialized")
            Return False
        End If

        Try
            ' Setup relay endpoint jika belum
            If _relayUdpEndpoint Is Nothing Then
                _relayUdpEndpoint = New IPEndPoint(IPAddress.Parse(relayIP), relayUdpPort)
            End If

            ' Buat registration packet (header-only, FrameId=0, ChunkIndex=0, ChunkCount=1)
            ' Ini memberitahu relay bahwa endpoint ini adalah Tamu untuk session ini
            Dim registrationPacket = New Booku_Remote.cls_UdpPacket() With {
                .SessionId = sessionId,
                .FrameId = 0,
                .ChunkIndex = 0,
                .ChunkCount = 1,
                .TimestampMs = CInt(DateTime.UtcNow.Ticks Mod Integer.MaxValue),
                .Data = Array.Empty(Of Byte)()
            }

            Dim data = registrationPacket.ToBytes()
            Await _udpClient.SendAsync(data, data.Length, _relayUdpEndpoint)

            Console.WriteLine($"[UDP-TAMU] Registration packet sent to relay {relayIP}:{relayUdpPort}, SessionId={sessionId}")
            Return True

        Catch ex As Exception
            Console.WriteLine($"[UDP-TAMU] Error sending registration: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Kirim periodic registration untuk menjaga endpoint tetap terdaftar di relay.
    ''' Dipanggil setiap beberapa detik selama streaming aktif.
    ''' </summary>
    Public Async Function KirimRegistrasiPeriodikAsync() As Task
        If _relayUdpEndpoint Is Nothing OrElse _udpClient Is Nothing Then Return

        Try
            Dim registrationPacket = New Booku_Remote.cls_UdpPacket() With {
                .SessionId = UdpSessionId,
                .FrameId = 0,
                .ChunkIndex = 0,
                .ChunkCount = 1,
                .TimestampMs = CInt(DateTime.UtcNow.Ticks Mod Integer.MaxValue),
                .Data = Array.Empty(Of Byte)()
            }

            Dim data = registrationPacket.ToBytes()
            Await _udpClient.SendAsync(data, data.Length, _relayUdpEndpoint)
        Catch
            ' Ignore errors for periodic registration
        End Try
    End Function

#End Region

#Region "Common"

    ''' <summary>
    ''' Hentikan UDP streaming (baik sender maupun receiver)
    ''' </summary>
    Public Sub HentikanUdpStreaming()
        Try
            UdpStreamingAktif = False

            ' Stop H.264 encoder jika aktif
            HentikanH264Encoder()

            ' Cancel token
            If _ctsUdpStreaming IsNot Nothing Then
                _ctsUdpStreaming.Cancel()
                _ctsUdpStreaming.Dispose()
                _ctsUdpStreaming = Nothing
            End If

            ' Close UDP client
            If _udpClient IsNot Nothing Then
                _udpClient.Close()
                _udpClient.Dispose()
                _udpClient = Nothing
            End If

            ' Reset assembler
            If _frameAssembler IsNot Nothing Then
                _frameAssembler.Reset()
                _frameAssembler = Nothing
            End If

            ' Reset variabel
            _targetEndpoint = Nothing
            _relayUdpEndpoint = Nothing
            _frameIdCounter = 0
            UdpSessionId = 0
            ' CATATAN: CodecStreaming TIDAK di-reset di sini.
            ' Codec negotiation sudah terjadi sebelum streaming dimulai dan hasilnya
            ' harus dipertahankan. Reset codec hanya dilakukan saat koneksi berakhir sepenuhnya.

            Console.WriteLine("[UDP] Streaming dihentikan")

        Catch ex As Exception
            Console.WriteLine($"[UDP] Error saat hentikan: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Generate SessionId dari string menggunakan djb2 hash (deterministic, cross-platform).
    ''' PENTING: GetHashCode() tidak konsisten antar platform (.NET Windows vs Android).
    ''' djb2 menghasilkan hash yang sama di semua platform.
    ''' </summary>
    Public Function GenerateSessionId(sessionKey As String) As Integer
        If String.IsNullOrEmpty(sessionKey) Then Return 0

        ' djb2 hash algorithm - deterministic dan cross-platform
        ' Menggunakan ULong untuk mencegah overflow, lalu mask ke 32-bit
        Dim hash As ULong = 5381
        For Each c As Char In sessionKey
            ' ((hash << 5) + hash) ^ c
            ' Mask dengan &HFFFFFFFFUL untuk tetap dalam 32-bit range (seperti unchecked di C#)
            hash = ((hash << 5) + hash) Xor CULng(AscW(c))
            hash = hash And &HFFFFFFFFUL
        Next

        ' Convert to positive 32-bit integer
        Return CInt(hash And &H7FFFFFFFUL)
    End Function

    ''' <summary>
    ''' Cek apakah UDP streaming sedang aktif
    ''' </summary>
    Public Function IsUdpStreamingActive() As Boolean
        Return UdpStreamingAktif AndAlso _udpClient IsNot Nothing
    End Function

#End Region

End Module
