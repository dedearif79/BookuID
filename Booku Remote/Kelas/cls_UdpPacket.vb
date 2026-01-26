' =====================================================================
' cls_UdpPacket.vb
' Model untuk UDP video streaming dengan header minimal (16 byte)
'
' Format:
' [SessionId:4][FrameId:4][ChunkIndex:2][ChunkCount:2][TimestampMs:4][CodecType:1][Data:<=1199]
'
' Total header: 16 byte
' CodecType: 1 byte (0x00=JPEG, 0x01=H.264) - bagian dari payload
' Max video data: 1199 byte (setelah dikurangi CodecType)
' Max datagram: 1216 byte
' =====================================================================

Imports System.IO

Namespace Booku_Remote

    ''' <summary>
    ''' Konstanta untuk UDP streaming
    ''' </summary>
    Public Module UdpConstants
        ''' <summary>Header size: 16 byte</summary>
        Public Const UDP_HEADER_SIZE As Integer = 16

        ''' <summary>Codec type size: 1 byte (bagian dari payload)</summary>
        Public Const UDP_CODEC_TYPE_SIZE As Integer = 1

        ''' <summary>Max video data per chunk: 1199 byte (setelah dikurangi CodecType)</summary>
        Public Const UDP_MAX_VIDEO_DATA_SIZE As Integer = 1199

        ''' <summary>Max payload per chunk: 1200 byte (CodecType + video data)</summary>
        Public Const UDP_MAX_PAYLOAD_SIZE As Integer = UDP_CODEC_TYPE_SIZE + UDP_MAX_VIDEO_DATA_SIZE

        ''' <summary>Max total datagram: 1216 byte</summary>
        Public Const UDP_MAX_DATAGRAM_SIZE As Integer = UDP_HEADER_SIZE + UDP_MAX_PAYLOAD_SIZE

        ''' <summary>Timeout untuk reassembly frame (ms)</summary>
        ''' <remarks>
        ''' Ditingkatkan dari 100ms ke 500ms untuk H.264 streaming.
        ''' H.264 keyframes bisa sangat besar (50KB+ = 40+ chunks).
        ''' Dengan UDP jitter dan latency, 100ms terlalu pendek untuk
        ''' menerima semua chunks dari satu keyframe.
        ''' </remarks>
        Public Const UDP_FRAME_TIMEOUT_MS As Integer = 500

        ''' <summary>Max frame buffer (jumlah frame yang di-track untuk reassembly)</summary>
        ''' <remarks>
        ''' Ditingkatkan dari 5 ke 15 untuk H.264 streaming.
        ''' H.264 mengirim setiap NAL unit sebagai "frame" terpisah
        ''' (SPS, PPS, IDR, SLICE, SLICE...), sehingga perlu buffer lebih besar.
        ''' </remarks>
        Public Const UDP_MAX_FRAME_BUFFER As Integer = 15

        ''' <summary>Codec type: JPEG (default)</summary>
        Public Const CODEC_TYPE_JPEG As Byte = 0

        ''' <summary>Codec type: H.264</summary>
        Public Const CODEC_TYPE_H264 As Byte = 1
    End Module

    ''' <summary>
    ''' Representasi satu UDP packet untuk video streaming
    ''' </summary>
    Public Class cls_UdpPacket
        ''' <summary>Session ID untuk routing di relay (4 byte)</summary>
        Public Property SessionId As Integer

        ''' <summary>Frame ID, increment per frame (4 byte)</summary>
        Public Property FrameId As Integer

        ''' <summary>Index chunk dalam frame ini (2 byte, 0-based)</summary>
        Public Property ChunkIndex As UShort

        ''' <summary>Total jumlah chunk untuk frame ini (2 byte)</summary>
        Public Property ChunkCount As UShort

        ''' <summary>Timestamp dalam milliseconds (4 byte)</summary>
        Public Property TimestampMs As Integer

        ''' <summary>Codec type: 0=JPEG, 1=H.264 (1 byte, bagian dari payload)</summary>
        Public Property CodecType As Byte = CODEC_TYPE_JPEG

        ''' <summary>Video data payload (max 1199 byte, tidak termasuk CodecType)</summary>
        Public Property Data As Byte()

        ''' <summary>
        ''' Serialize packet ke byte array untuk dikirim via UDP
        ''' </summary>
        Public Function ToBytes() As Byte()
            Using ms As New MemoryStream()
                Using bw As New BinaryWriter(ms)
                    bw.Write(SessionId)      ' 4 byte
                    bw.Write(FrameId)        ' 4 byte
                    bw.Write(ChunkIndex)     ' 2 byte
                    bw.Write(ChunkCount)     ' 2 byte
                    bw.Write(TimestampMs)    ' 4 byte
                    bw.Write(CodecType)      ' 1 byte (bagian dari payload)
                    If Data IsNot Nothing AndAlso Data.Length > 0 Then
                        bw.Write(Data)       ' <= 1199 byte
                    End If
                End Using
                Return ms.ToArray()
            End Using
        End Function

        ''' <summary>
        ''' Deserialize dari byte array yang diterima via UDP
        ''' </summary>
        Public Shared Function FromBytes(buffer As Byte()) As cls_UdpPacket
            ' Minimal: header (16) + codec type (1)
            If buffer Is Nothing OrElse buffer.Length < UDP_HEADER_SIZE + UDP_CODEC_TYPE_SIZE Then
                Return Nothing
            End If

            Try
                Using ms As New MemoryStream(buffer)
                    Using br As New BinaryReader(ms)
                        Dim packet As New cls_UdpPacket()
                        packet.SessionId = br.ReadInt32()      ' 4 byte
                        packet.FrameId = br.ReadInt32()        ' 4 byte
                        packet.ChunkIndex = br.ReadUInt16()    ' 2 byte
                        packet.ChunkCount = br.ReadUInt16()    ' 2 byte
                        packet.TimestampMs = br.ReadInt32()    ' 4 byte
                        packet.CodecType = br.ReadByte()       ' 1 byte

                        ' Sisa adalah video data
                        Dim dataLength = buffer.Length - UDP_HEADER_SIZE - UDP_CODEC_TYPE_SIZE
                        If dataLength > 0 Then
                            packet.Data = br.ReadBytes(dataLength)
                        Else
                            packet.Data = Array.Empty(Of Byte)()
                        End If

                        Return packet
                    End Using
                End Using
            Catch
                Return Nothing
            End Try
        End Function
    End Class

    ''' <summary>
    ''' Helper untuk chunking frame video menjadi UDP packets
    ''' </summary>
    Public Class cls_UdpFrameChunker
        ''' <summary>
        ''' Pecah frame video menjadi array of UDP packets
        ''' </summary>
        ''' <param name="sessionId">Session ID untuk routing</param>
        ''' <param name="frameId">Frame ID (increment per frame)</param>
        ''' <param name="videoData">Data video frame (JPEG atau H.264)</param>
        ''' <param name="codecType">Tipe codec: CODEC_TYPE_JPEG atau CODEC_TYPE_H264</param>
        ''' <returns>Array of cls_UdpPacket siap kirim</returns>
        Public Shared Function ChunkFrame(sessionId As Integer, frameId As Integer, videoData As Byte(), Optional codecType As Byte = CODEC_TYPE_JPEG) As cls_UdpPacket()
            If videoData Is Nothing OrElse videoData.Length = 0 Then
                Return Array.Empty(Of cls_UdpPacket)()
            End If

            ' Hitung jumlah chunk yang dibutuhkan (menggunakan UDP_MAX_VIDEO_DATA_SIZE = 1199)
            Dim chunkCount As Integer = CInt(Math.Ceiling(videoData.Length / CDbl(UDP_MAX_VIDEO_DATA_SIZE)))
            Dim packets(chunkCount - 1) As cls_UdpPacket

            ' Timestamp saat ini
            Dim timestampMs As Integer = CInt(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() And Integer.MaxValue)

            ' Pecah data menjadi chunks
            For i As Integer = 0 To chunkCount - 1
                Dim offset As Integer = i * UDP_MAX_VIDEO_DATA_SIZE
                Dim length As Integer = Math.Min(UDP_MAX_VIDEO_DATA_SIZE, videoData.Length - offset)

                Dim chunkData(length - 1) As Byte
                Array.Copy(videoData, offset, chunkData, 0, length)

                packets(i) = New cls_UdpPacket() With {
                    .SessionId = sessionId,
                    .FrameId = frameId,
                    .ChunkIndex = CUShort(i),
                    .ChunkCount = CUShort(chunkCount),
                    .TimestampMs = timestampMs,
                    .CodecType = codecType,
                    .Data = chunkData
                }
            Next

            Return packets
        End Function

        ''' <summary>
        ''' Pecah frame JPEG menjadi array of UDP packets (backward compatible)
        ''' </summary>
        Public Shared Function ChunkFrameJPEG(sessionId As Integer, frameId As Integer, jpegData As Byte()) As cls_UdpPacket()
            Return ChunkFrame(sessionId, frameId, jpegData, CODEC_TYPE_JPEG)
        End Function

        ''' <summary>
        ''' Pecah frame H.264 menjadi array of UDP packets
        ''' </summary>
        Public Shared Function ChunkFrameH264(sessionId As Integer, frameId As Integer, h264Data As Byte()) As cls_UdpPacket()
            Return ChunkFrame(sessionId, frameId, h264Data, CODEC_TYPE_H264)
        End Function
    End Class

    ''' <summary>
    ''' Helper untuk reassembly UDP packets menjadi frame JPEG/H.264
    ''' </summary>
    Public Class cls_UdpFrameAssembler
        ' Buffer untuk menyimpan chunks per frame
        Private ReadOnly _frameBuffers As New Dictionary(Of Integer, FrameBuffer)
        Private ReadOnly _lock As New Object()

        ' Track frame terakhir yang berhasil di-render (untuk skip frame lama)
        Private _lastRenderedFrameId As Integer = -1

        ' Mode ordered delivery untuk H.264 (memproses frame secara berurutan)
        Private _orderedMode As Boolean = False

        ' Frame berikutnya yang diharapkan (untuk ordered mode)
        Private _nextExpectedFrameId As Integer = 1

        ' Buffer untuk frame yang sudah lengkap tapi belum diproses (untuk ordered mode)
        Private ReadOnly _completedFrames As New SortedDictionary(Of Integer, CompletedFrame)

        ''' <summary>
        ''' Class untuk menyimpan frame yang sudah lengkap (untuk ordered mode)
        ''' </summary>
        Private Class CompletedFrame
            Public Property FrameData As Byte()
            Public Property CodecType As Byte
            Public Property TimestampMs As Integer
            Public Property FrameId As Integer
        End Class

        ''' <summary>
        ''' Aktifkan/nonaktifkan ordered delivery mode.
        ''' Untuk H.264, ordered mode WAJIB aktif karena NAL units harus diproses berurutan.
        ''' </summary>
        ''' <param name="enabled">True untuk mengaktifkan ordered mode</param>
        Public Sub SetOrderedMode(enabled As Boolean)
            SyncLock _lock
                _orderedMode = enabled
                If enabled Then
                    ' Reset tracking saat mengaktifkan ordered mode
                    _nextExpectedFrameId = 1
                    _completedFrames.Clear()
                End If
            End SyncLock
        End Sub

        ''' <summary>
        ''' Cek apakah ordered mode aktif
        ''' </summary>
        Public ReadOnly Property IsOrderedMode As Boolean
            Get
                Return _orderedMode
            End Get
        End Property

        Private Class FrameBuffer
            Public Property FrameId As Integer
            Public Property ChunkCount As UShort
            Public Property ReceivedChunks As Dictionary(Of UShort, Byte())
            Public Property FirstChunkTime As DateTime
            Public Property TimestampMs As Integer
            Public Property CodecType As Byte = CODEC_TYPE_JPEG

            Public Sub New(frameId As Integer, chunkCount As UShort, timestampMs As Integer, codecType As Byte)
                Me.FrameId = frameId
                Me.ChunkCount = chunkCount
                Me.ReceivedChunks = New Dictionary(Of UShort, Byte())()
                Me.FirstChunkTime = DateTime.UtcNow
                Me.TimestampMs = timestampMs
                Me.CodecType = codecType
            End Sub

            Public ReadOnly Property IsComplete As Boolean
                Get
                    Return ReceivedChunks.Count = ChunkCount
                End Get
            End Property

            Public ReadOnly Property IsExpired As Boolean
                Get
                    Return (DateTime.UtcNow - FirstChunkTime).TotalMilliseconds > UDP_FRAME_TIMEOUT_MS
                End Get
            End Property

            Public Function Assemble() As Byte()
                If Not IsComplete Then Return Nothing

                ' Hitung total size
                Dim totalSize As Integer = 0
                For i As UShort = 0 To ChunkCount - 1US
                    If ReceivedChunks.ContainsKey(i) Then
                        totalSize += ReceivedChunks(i).Length
                    Else
                        Return Nothing ' Missing chunk
                    End If
                Next

                ' Gabungkan semua chunks
                Dim result(totalSize - 1) As Byte
                Dim offset As Integer = 0
                For i As UShort = 0 To ChunkCount - 1US
                    Dim chunk = ReceivedChunks(i)
                    Array.Copy(chunk, 0, result, offset, chunk.Length)
                    offset += chunk.Length
                Next

                Return result
            End Function
        End Class

        ''' <summary>
        ''' Tambahkan packet yang diterima. Return frame JPEG jika lengkap, Nothing jika belum.
        ''' </summary>
        Public Function AddPacket(packet As cls_UdpPacket) As Byte()
            If packet Is Nothing Then Return Nothing

            SyncLock _lock
                ' Skip frame lama (sudah ada frame lebih baru yang di-render)
                If packet.FrameId <= _lastRenderedFrameId Then
                    Return Nothing
                End If

                ' Cleanup expired frames
                CleanupExpiredFrames()

                ' Cari atau buat buffer untuk frame ini
                Dim buffer As FrameBuffer = Nothing
                If Not _frameBuffers.TryGetValue(packet.FrameId, buffer) Then
                    ' Limit jumlah frame yang di-track
                    If _frameBuffers.Count >= UDP_MAX_FRAME_BUFFER Then
                        ' Hapus frame paling lama
                        Dim oldestFrameId = _frameBuffers.Keys.Min()
                        _frameBuffers.Remove(oldestFrameId)
                    End If

                    buffer = New FrameBuffer(packet.FrameId, packet.ChunkCount, packet.TimestampMs, packet.CodecType)
                    _frameBuffers(packet.FrameId) = buffer
                End If

                ' Tambahkan chunk (skip jika sudah ada)
                If Not buffer.ReceivedChunks.ContainsKey(packet.ChunkIndex) Then
                    buffer.ReceivedChunks(packet.ChunkIndex) = packet.Data
                End If

                ' Cek apakah frame sudah lengkap
                If buffer.IsComplete Then
                    Dim frameData = buffer.Assemble()
                    _frameBuffers.Remove(packet.FrameId)
                    _lastRenderedFrameId = packet.FrameId
                    Return frameData
                End If

                Return Nothing
            End SyncLock
        End Function

        ''' <summary>
        ''' Tambahkan packet yang diterima dan kembalikan frame data beserta codec type jika lengkap.
        ''' </summary>
        ''' <param name="packet">UDP packet yang diterima</param>
        ''' <param name="frameData">Output: data frame jika lengkap, Nothing jika belum</param>
        ''' <param name="codecType">Output: codec type (JPEG=0, H264=1)</param>
        ''' <returns>True jika frame lengkap dan siap di-render</returns>
        Public Function AddPacketWithCodec(packet As cls_UdpPacket, ByRef frameData As Byte(), ByRef codecType As Byte) As Boolean
            frameData = Nothing
            codecType = CODEC_TYPE_JPEG

            If packet Is Nothing Then Return False

            SyncLock _lock
                ' === ORDERED MODE (untuk H.264) ===
                If _orderedMode Then
                    Return AddPacketOrdered(packet, frameData, codecType)
                End If

                ' === NORMAL MODE (untuk JPEG - skip frame lama) ===
                ' Skip frame lama (sudah ada frame lebih baru yang di-render)
                If packet.FrameId <= _lastRenderedFrameId Then
                    Return False
                End If

                ' Cleanup expired frames
                CleanupExpiredFrames()

                ' Cari atau buat buffer untuk frame ini
                Dim buffer As FrameBuffer = Nothing
                If Not _frameBuffers.TryGetValue(packet.FrameId, buffer) Then
                    ' Limit jumlah frame yang di-track
                    If _frameBuffers.Count >= UDP_MAX_FRAME_BUFFER Then
                        ' Hapus frame paling lama
                        Dim oldestFrameId = _frameBuffers.Keys.Min()
                        _frameBuffers.Remove(oldestFrameId)
                    End If

                    buffer = New FrameBuffer(packet.FrameId, packet.ChunkCount, packet.TimestampMs, packet.CodecType)
                    _frameBuffers(packet.FrameId) = buffer
                End If

                ' Tambahkan chunk (skip jika sudah ada)
                If Not buffer.ReceivedChunks.ContainsKey(packet.ChunkIndex) Then
                    buffer.ReceivedChunks(packet.ChunkIndex) = packet.Data
                End If

                ' Cek apakah frame sudah lengkap
                If buffer.IsComplete Then
                    frameData = buffer.Assemble()
                    codecType = buffer.CodecType
                    _frameBuffers.Remove(packet.FrameId)
                    _lastRenderedFrameId = packet.FrameId
                    Return True
                End If

                Return False
            End SyncLock
        End Function

        ''' <summary>
        ''' Internal: Tambahkan packet dengan ordered delivery (untuk H.264).
        ''' NAL units HARUS diproses berurutan untuk decoding yang benar.
        ''' </summary>
        Private Function AddPacketOrdered(packet As cls_UdpPacket, ByRef frameData As Byte(), ByRef codecType As Byte) As Boolean
            ' Cleanup expired frames
            CleanupExpiredFrames()
            CleanupOldCompletedFrames()

            ' Cari atau buat buffer untuk frame ini
            Dim buffer As FrameBuffer = Nothing
            If Not _frameBuffers.TryGetValue(packet.FrameId, buffer) Then
                ' Limit jumlah frame yang di-track
                If _frameBuffers.Count >= UDP_MAX_FRAME_BUFFER Then
                    ' Hapus frame paling lama
                    Dim oldestFrameId = _frameBuffers.Keys.Min()
                    _frameBuffers.Remove(oldestFrameId)
                End If

                buffer = New FrameBuffer(packet.FrameId, packet.ChunkCount, packet.TimestampMs, packet.CodecType)
                _frameBuffers(packet.FrameId) = buffer
            End If

            ' Tambahkan chunk (skip jika sudah ada)
            If Not buffer.ReceivedChunks.ContainsKey(packet.ChunkIndex) Then
                buffer.ReceivedChunks(packet.ChunkIndex) = packet.Data
            End If

            ' Cek apakah frame sudah lengkap
            If buffer.IsComplete Then
                Dim assembled = buffer.Assemble()
                _frameBuffers.Remove(packet.FrameId)

                ' Simpan ke completed frames buffer
                If Not _completedFrames.ContainsKey(packet.FrameId) Then
                    _completedFrames(packet.FrameId) = New CompletedFrame() With {
                        .FrameData = assembled,
                        .CodecType = buffer.CodecType,
                        .TimestampMs = buffer.TimestampMs,
                        .FrameId = packet.FrameId
                    }
                End If
            End If

            ' Cek apakah frame yang diharapkan sudah tersedia
            If _completedFrames.ContainsKey(_nextExpectedFrameId) Then
                Dim completed = _completedFrames(_nextExpectedFrameId)
                frameData = completed.FrameData
                codecType = completed.CodecType
                _completedFrames.Remove(_nextExpectedFrameId)
                _lastRenderedFrameId = _nextExpectedFrameId
                _nextExpectedFrameId += 1
                Return True
            End If

            Return False
        End Function

        ''' <summary>
        ''' Cleanup completed frames yang sudah terlalu lama menunggu (expired).
        ''' Jika frame yang diharapkan tidak datang dalam waktu lama, skip ke frame berikutnya.
        ''' </summary>
        Private Sub CleanupOldCompletedFrames()
            ' Jika ada banyak completed frames tapi yang diharapkan tidak ada,
            ' kemungkinan frame tersebut hilang. Skip ke frame yang tersedia.
            If _completedFrames.Count > 10 Then
                ' Ambil frame pertama yang tersedia
                Dim firstAvailable = _completedFrames.Keys.Min()
                If firstAvailable > _nextExpectedFrameId Then
                    ' Skip ke frame yang tersedia
                    _nextExpectedFrameId = firstAvailable
                End If
            End If

            ' Hapus completed frames yang sudah sangat tua (lebih dari 30 frame di belakang)
            Dim framesToRemove = _completedFrames.Keys.Where(Function(k) k < _nextExpectedFrameId - 30).ToList()
            For Each frameId In framesToRemove
                _completedFrames.Remove(frameId)
            Next
        End Sub

        ''' <summary>
        ''' Cleanup frame yang sudah expired (timeout)
        ''' </summary>
        Private Sub CleanupExpiredFrames()
            Dim expiredFrames = _frameBuffers.Where(Function(kv) kv.Value.IsExpired).Select(Function(kv) kv.Key).ToList()
            For Each frameId In expiredFrames
                _frameBuffers.Remove(frameId)
            Next
        End Sub

        ''' <summary>
        ''' Reset assembler (saat disconnect)
        ''' </summary>
        Public Sub Reset()
            SyncLock _lock
                _frameBuffers.Clear()
                _lastRenderedFrameId = -1
                ' Reset ordered mode state
                _completedFrames.Clear()
                _nextExpectedFrameId = 1
                ' Note: _orderedMode tidak di-reset, tetap sesuai setting terakhir
            End SyncLock
        End Sub

        ''' <summary>
        ''' Get statistik untuk debugging
        ''' </summary>
        Public Function GetStats() As String
            SyncLock _lock
                If _orderedMode Then
                    Return $"Buffers: {_frameBuffers.Count}, LastRendered: {_lastRenderedFrameId}, Ordered: Yes, NextExpected: {_nextExpectedFrameId}, Completed: {_completedFrames.Count}"
                Else
                    Return $"Buffers: {_frameBuffers.Count}, LastRendered: {_lastRenderedFrameId}, Ordered: No"
                End If
            End SyncLock
        End Function
    End Class

End Namespace
