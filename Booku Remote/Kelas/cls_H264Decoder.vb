Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks

Namespace Booku_Remote

    ''' <summary>
    ''' H.264 decoder wrapper menggunakan FFmpeg.
    ''' Menerima H.264 NAL units dan menghasilkan raw BGRA frames.
    ''' </summary>
    Public Class cls_H264Decoder
        Implements IDisposable

#Region "Constants"

        ''' <summary>Buffer size untuk membaca output FFmpeg</summary>
        Private Const READ_BUFFER_SIZE As Integer = 65536

        ''' <summary>Timeout untuk menunggu FFmpeg start (ms)</summary>
        Private Const STARTUP_TIMEOUT_MS As Integer = 5000

#End Region

#Region "Private Fields"

        Private _ffmpegProcess As Process
        Private _cancellationTokenSource As CancellationTokenSource
        Private _readTask As Task
        Private _errorTask As Task

        Private _width As Integer
        Private _height As Integer
        Private _bytesPerFrame As Integer
        Private _isRunning As Boolean
        Private _isDisposed As Boolean

        ''' <summary>Flag apakah menggunakan mode auto-resolusi</summary>
        Private _autoResolusi As Boolean = False

        ''' <summary>Flag apakah resolusi sudah terdeteksi (untuk mode auto)</summary>
        Private _resolusiTerdeteksi As Boolean = False

        ' Buffer untuk mengumpulkan bytes sampai satu frame lengkap
        Private _frameBuffer As MemoryStream
        Private ReadOnly _lockObj As New Object()

#End Region

#Region "Events"

        ''' <summary>
        ''' Event ketika frame BGRA tersedia (sudah di-decode).
        ''' </summary>
        Public Event FrameReady As EventHandler(Of DecodedFrameEventArgs)

        ''' <summary>
        ''' Event ketika terjadi error pada decoder.
        ''' </summary>
        Public Event DecoderError As EventHandler(Of DecoderErrorEventArgs)

        ''' <summary>
        ''' Event ketika decoder berhenti.
        ''' </summary>
        Public Event DecoderStopped As EventHandler

#End Region

#Region "Properties"

        ''' <summary>True jika decoder sedang berjalan</summary>
        Public ReadOnly Property IsRunning As Boolean
            Get
                Return _isRunning
            End Get
        End Property

        ''' <summary>Lebar video output</summary>
        Public ReadOnly Property Width As Integer
            Get
                Return _width
            End Get
        End Property

        ''' <summary>Tinggi video output</summary>
        Public ReadOnly Property Height As Integer
            Get
                Return _height
            End Get
        End Property

#End Region

#Region "Public Methods"

        ''' <summary>
        ''' Memulai decoder dengan parameter yang ditentukan.
        ''' </summary>
        ''' <param name="width">Lebar video output (BGRA)</param>
        ''' <param name="height">Tinggi video output (BGRA)</param>
        ''' <returns>True jika berhasil dimulai</returns>
        Public Function Start(width As Integer, height As Integer) As Boolean
            SyncLock _lockObj
                If _isRunning Then Return True
                If _isDisposed Then Return False

                ' Cek ketersediaan FFmpeg
                If Not mdl_FFmpegManager.FFmpegTersedia Then
                    Debug.WriteLine("[H264-DEC] FFmpeg not available")
                    Return False
                End If

                _width = width
                _height = height
                _bytesPerFrame = width * height * 4 ' BGRA = 4 bytes per pixel
                _autoResolusi = False
                _resolusiTerdeteksi = True  ' Resolusi sudah ditentukan

                Try
                    ' Buat process decoder
                    _ffmpegProcess = mdl_FFmpegManager.BuatProcessDecoder(width, height)
                    If _ffmpegProcess Is Nothing Then
                        Debug.WriteLine("[H264-DEC] Failed to create decoder process")
                        Return False
                    End If

                    _cancellationTokenSource = New CancellationTokenSource()
                    _frameBuffer = New MemoryStream()

                    ' Start FFmpeg process
                    _ffmpegProcess.Start()

                    ' Start read task untuk membaca output BGRA frames
                    _readTask = Task.Run(Sub() ReadOutputLoop(_cancellationTokenSource.Token))

                    ' Start error task untuk membaca stderr (logging)
                    _errorTask = Task.Run(Sub() ReadErrorLoop(_cancellationTokenSource.Token))

                    _isRunning = True
                    Debug.WriteLine($"[H264-DEC] Started: output {width}x{height}")
                    Return True

                Catch ex As Exception
                    Debug.WriteLine($"[H264-DEC] Start error: {ex.Message}")
                    Cleanup()
                    Return False
                End Try
            End SyncLock
        End Function

        ''' <summary>
        ''' Memulai decoder dengan auto-deteksi resolusi dari SPS dalam H.264 stream.
        ''' FFmpeg akan mendeteksi resolusi dari stream dan output dengan resolusi asli.
        ''' </summary>
        ''' <returns>True jika berhasil dimulai</returns>
        Public Function StartAutoResolusi() As Boolean
            SyncLock _lockObj
                If _isRunning Then Return True
                If _isDisposed Then Return False

                ' Cek ketersediaan FFmpeg
                If Not mdl_FFmpegManager.FFmpegTersedia Then
                    Debug.WriteLine("[H264-DEC] FFmpeg not available")
                    Return False
                End If

                ' Reset dimensi - akan di-set saat resolusi terdeteksi dari stderr
                _width = 0
                _height = 0
                _bytesPerFrame = 0
                _autoResolusi = True
                _resolusiTerdeteksi = False

                Try
                    ' Buat process decoder tanpa specify resolusi output
                    _ffmpegProcess = mdl_FFmpegManager.BuatProcessDecoderAutoResolusi()
                    If _ffmpegProcess Is Nothing Then
                        Debug.WriteLine("[H264-DEC] Failed to create auto-res decoder process")
                        Return False
                    End If

                    _cancellationTokenSource = New CancellationTokenSource()
                    _frameBuffer = New MemoryStream()

                    ' Start FFmpeg process
                    _ffmpegProcess.Start()

                    ' Start read task untuk membaca output BGRA frames
                    _readTask = Task.Run(Sub() ReadOutputLoop(_cancellationTokenSource.Token))

                    ' Start error task untuk membaca stderr dan parse resolusi
                    _errorTask = Task.Run(Sub() ReadErrorLoopAutoResolusi(_cancellationTokenSource.Token))

                    _isRunning = True
                    Debug.WriteLine("[H264-DEC] Started with auto-resolution detection")
                    Return True

                Catch ex As Exception
                    Debug.WriteLine($"[H264-DEC] StartAutoResolusi error: {ex.Message}")
                    Cleanup()
                    Return False
                End Try
            End SyncLock
        End Function

        ''' <summary>
        ''' Menghentikan decoder.
        ''' </summary>
        Public Sub [Stop]()
            SyncLock _lockObj
                If Not _isRunning Then Return

                Debug.WriteLine("[H264-DEC] Stopping...")

                _cancellationTokenSource?.Cancel()

                Try
                    ' Tutup stdin untuk signal EOF ke FFmpeg
                    _ffmpegProcess?.StandardInput?.Close()

                    ' Tunggu process selesai dengan timeout
                    If _ffmpegProcess IsNot Nothing AndAlso Not _ffmpegProcess.HasExited Then
                        If Not _ffmpegProcess.WaitForExit(3000) Then
                            _ffmpegProcess.Kill()
                        End If
                    End If
                Catch
                End Try

                Cleanup()
                _isRunning = False

                RaiseEvent DecoderStopped(Me, EventArgs.Empty)
                Debug.WriteLine("[H264-DEC] Stopped")
            End SyncLock
        End Sub

        ''' <summary>
        ''' Mengirim H.264 data ke decoder.
        ''' Data bisa berupa satu atau lebih NAL units.
        ''' </summary>
        ''' <param name="h264Data">H.264 data (NAL units)</param>
        ''' <returns>True jika berhasil dikirim</returns>
        Public Function SendData(h264Data As Byte()) As Boolean
            If Not _isRunning Then Return False
            If h264Data Is Nothing OrElse h264Data.Length = 0 Then Return False

            Try
                ' Tulis ke stdin FFmpeg
                _ffmpegProcess.StandardInput.BaseStream.Write(h264Data, 0, h264Data.Length)
                _ffmpegProcess.StandardInput.BaseStream.Flush()
                Return True
            Catch ex As Exception
                Debug.WriteLine($"[H264-DEC] SendData error: {ex.Message}")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Mengirim H.264 data ke decoder secara async.
        ''' </summary>
        Public Async Function SendDataAsync(h264Data As Byte()) As Task(Of Boolean)
            If Not _isRunning Then Return False
            If h264Data Is Nothing OrElse h264Data.Length = 0 Then Return False

            Try
                Await _ffmpegProcess.StandardInput.BaseStream.WriteAsync(h264Data, 0, h264Data.Length)
                Await _ffmpegProcess.StandardInput.BaseStream.FlushAsync()
                Return True
            Catch ex As Exception
                Debug.WriteLine($"[H264-DEC] SendDataAsync error: {ex.Message}")
                Return False
            End Try
        End Function

#End Region

#Region "Private Methods"

        ''' <summary>
        ''' Loop untuk membaca output BGRA frames dari FFmpeg stdout.
        ''' </summary>
        ''' <remarks>
        ''' PENTING: Kita HARUS selalu membaca stdout untuk mencegah FFmpeg blocked.
        ''' Jika resolusi belum diketahui (auto-resolusi mode), data tetap dibaca
        ''' dan di-buffer. Setelah resolusi terdeteksi, baru diproses.
        ''' </remarks>
        Private Sub ReadOutputLoop(token As CancellationToken)
            Dim buffer(READ_BUFFER_SIZE - 1) As Byte
            Dim totalBytesBuffered As Long = 0

            Try
                While Not token.IsCancellationRequested
                    If _ffmpegProcess Is Nothing OrElse _ffmpegProcess.HasExited Then
                        Exit While
                    End If

                    ' SELALU baca dari stdout untuk mencegah FFmpeg blocked
                    Dim bytesRead = _ffmpegProcess.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length)

                    If bytesRead > 0 Then
                        ' Tambahkan ke frame buffer
                        _frameBuffer.Write(buffer, 0, bytesRead)
                        totalBytesBuffered += bytesRead

                        ' Jika resolusi belum diketahui (auto-resolusi mode),
                        ' terus buffer data tapi jangan proses dulu
                        If _autoResolusi AndAlso Not _resolusiTerdeteksi Then
                            ' Log setiap 100KB untuk debugging
                            If totalBytesBuffered Mod 102400 < bytesRead Then
                                Debug.WriteLine($"[H264-DEC] Buffering while waiting for resolution: {totalBytesBuffered / 1024:F1}KB")
                            End If
                            Continue While
                        End If

                        ' Skip jika bytesPerFrame belum valid
                        If _bytesPerFrame <= 0 Then
                            Continue While
                        End If

                        ' Proses buffer - ekstrak frames yang lengkap
                        While _frameBuffer.Length >= _bytesPerFrame
                            ' Ekstrak satu frame
                            Dim frameData(_bytesPerFrame - 1) As Byte
                            _frameBuffer.Position = 0
                            _frameBuffer.Read(frameData, 0, _bytesPerFrame)

                            ' Simpan sisa bytes
                            Dim remaining = CInt(_frameBuffer.Length - _bytesPerFrame)
                            If remaining > 0 Then
                                Dim remainingData(remaining - 1) As Byte
                                _frameBuffer.Read(remainingData, 0, remaining)
                                _frameBuffer.SetLength(0)
                                _frameBuffer.Write(remainingData, 0, remaining)
                            Else
                                _frameBuffer.SetLength(0)
                            End If

                            ' Raise event dengan frame yang sudah di-decode
                            RaiseEvent FrameReady(Me, New DecodedFrameEventArgs With {
                                .BgraData = frameData,
                                .Width = _width,
                                .Height = _height,
                                .Timestamp = DateTime.UtcNow
                            })
                        End While

                    ElseIf bytesRead = 0 Then
                        ' EOF - process selesai
                        Exit While
                    End If
                End While
            Catch ex As OperationCanceledException
                ' Normal cancellation
            Catch ex As Exception
                Debug.WriteLine($"[H264-DEC] ReadOutput error: {ex.Message}")
                RaiseEvent DecoderError(Me, New DecoderErrorEventArgs With {
                    .Message = ex.Message,
                    .Exception = ex
                })
            End Try

            Debug.WriteLine($"[H264-DEC] ReadOutput loop ended, total buffered: {totalBytesBuffered / 1024:F1}KB")
        End Sub

        ''' <summary>
        ''' Loop untuk membaca stderr FFmpeg (untuk logging/debugging).
        ''' </summary>
        Private Sub ReadErrorLoop(token As CancellationToken)
            Try
                While Not token.IsCancellationRequested
                    If _ffmpegProcess Is Nothing OrElse _ffmpegProcess.HasExited Then
                        Exit While
                    End If

                    Dim line = _ffmpegProcess.StandardError.ReadLine()
                    If line Is Nothing Then Exit While

                    Debug.WriteLine($"[FFMPEG-DEC-ERR] {line}")
                End While
            Catch ex As OperationCanceledException
                ' Normal cancellation
            Catch ex As Exception
                Debug.WriteLine($"[H264-DEC] ReadError error: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Loop untuk membaca stderr FFmpeg dengan auto-deteksi resolusi.
        ''' Parse resolusi dari output seperti: "Stream #0:0: Video: h264..., 1024x640"
        ''' </summary>
        Private Sub ReadErrorLoopAutoResolusi(token As CancellationToken)
            ' Regex untuk parse resolusi dari output FFmpeg
            ' Contoh: "Stream #0:0: Video: h264 (Constrained Baseline), yuv420p(progressive), 1024x640"
            ' Atau: "Output #0, rawvideo to 'pipe:1': Stream #0:0: Video: rawvideo (BGRA...), bgra, 1024x640"
            Dim resolutionRegex As New System.Text.RegularExpressions.Regex(
                "(\d{2,5})x(\d{2,5})",
                System.Text.RegularExpressions.RegexOptions.Compiled)

            Try
                While Not token.IsCancellationRequested
                    If _ffmpegProcess Is Nothing OrElse _ffmpegProcess.HasExited Then
                        Exit While
                    End If

                    Dim line = _ffmpegProcess.StandardError.ReadLine()
                    If line Is Nothing Then Exit While

                    Debug.WriteLine($"[FFMPEG-DEC-ERR] {line}")

                    ' Parse resolusi dari output stream jika belum terdeteksi
                    ' Cari line yang mengandung "Stream #" dan "Video:" dan "bgra" (output format kita)
                    If Not _resolusiTerdeteksi AndAlso line.Contains("Stream #") AndAlso
                       line.Contains("Video:") AndAlso line.ToLower().Contains("bgra") Then

                        Dim match = resolutionRegex.Match(line)
                        If match.Success Then
                            Dim detectedWidth = Integer.Parse(match.Groups(1).Value)
                            Dim detectedHeight = Integer.Parse(match.Groups(2).Value)

                            If detectedWidth > 0 AndAlso detectedHeight > 0 Then
                                SyncLock _lockObj
                                    _width = detectedWidth
                                    _height = detectedHeight
                                    _bytesPerFrame = _width * _height * 4 ' BGRA = 4 bytes per pixel
                                    _resolusiTerdeteksi = True
                                End SyncLock

                                Debug.WriteLine($"[H264-DEC] Auto-detected resolution: {_width}x{_height}, bytesPerFrame={_bytesPerFrame}")
                            End If
                        End If
                    End If
                End While
            Catch ex As OperationCanceledException
                ' Normal cancellation
            Catch ex As Exception
                Debug.WriteLine($"[H264-DEC] ReadErrorAutoRes error: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Membersihkan resources.
        ''' </summary>
        Private Sub Cleanup()
            Try
                _cancellationTokenSource?.Dispose()
                _cancellationTokenSource = Nothing

                _frameBuffer?.Dispose()
                _frameBuffer = Nothing

                If _ffmpegProcess IsNot Nothing Then
                    If Not _ffmpegProcess.HasExited Then
                        Try
                            _ffmpegProcess.Kill()
                        Catch
                        End Try
                    End If
                    _ffmpegProcess.Dispose()
                    _ffmpegProcess = Nothing
                End If

                ' Reset flags untuk auto-resolusi
                _autoResolusi = False
                _resolusiTerdeteksi = False
            Catch ex As Exception
                Debug.WriteLine($"[H264-DEC] Cleanup error: {ex.Message}")
            End Try
        End Sub

#End Region

#Region "IDisposable"

        Public Sub Dispose() Implements IDisposable.Dispose
            If _isDisposed Then Return
            _isDisposed = True

            [Stop]()
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overrides Sub Finalize()
            Dispose()
        End Sub

#End Region

    End Class

#Region "Event Args"

    ''' <summary>
    ''' Event args untuk frame yang sudah di-decode.
    ''' </summary>
    Public Class DecodedFrameEventArgs
        Inherits EventArgs

        ''' <summary>Raw BGRA pixel data (4 bytes per pixel)</summary>
        Public Property BgraData As Byte()

        ''' <summary>Lebar frame</summary>
        Public Property Width As Integer

        ''' <summary>Tinggi frame</summary>
        Public Property Height As Integer

        ''' <summary>Timestamp ketika frame di-decode</summary>
        Public Property Timestamp As DateTime
    End Class

    ''' <summary>
    ''' Event args untuk error decoder.
    ''' </summary>
    Public Class DecoderErrorEventArgs
        Inherits EventArgs

        ''' <summary>Pesan error</summary>
        Public Property Message As String

        ''' <summary>Exception (jika ada)</summary>
        Public Property Exception As Exception
    End Class

#End Region

End Namespace
