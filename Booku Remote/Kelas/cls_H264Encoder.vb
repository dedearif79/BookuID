Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks

Namespace Booku_Remote

    ''' <summary>
    ''' H.264 encoder wrapper menggunakan FFmpeg.
    ''' Menerima raw BGRA frames dan menghasilkan H.264 NAL units.
    ''' </summary>
    Public Class cls_H264Encoder
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
        Private _fps As Integer
        Private _isRunning As Boolean
        Private _isDisposed As Boolean

        Private ReadOnly _lockObj As New Object()

#End Region

#Region "Events"

        ''' <summary>
        ''' Event ketika data H.264 tersedia.
        ''' Data bisa berupa satu atau lebih NAL units.
        ''' </summary>
        Public Event DataReady As EventHandler(Of H264DataEventArgs)

        ''' <summary>
        ''' Event ketika terjadi error pada encoder.
        ''' </summary>
        Public Event EncoderError As EventHandler(Of EncoderErrorEventArgs)

        ''' <summary>
        ''' Event ketika encoder berhenti.
        ''' </summary>
        Public Event EncoderStopped As EventHandler

#End Region

#Region "Properties"

        ''' <summary>True jika encoder sedang berjalan</summary>
        Public ReadOnly Property IsRunning As Boolean
            Get
                Return _isRunning
            End Get
        End Property

        ''' <summary>Lebar video</summary>
        Public ReadOnly Property Width As Integer
            Get
                Return _width
            End Get
        End Property

        ''' <summary>Tinggi video</summary>
        Public ReadOnly Property Height As Integer
            Get
                Return _height
            End Get
        End Property

        ''' <summary>Target FPS</summary>
        Public ReadOnly Property FPS As Integer
            Get
                Return _fps
            End Get
        End Property

#End Region

#Region "Public Methods"

        ''' <summary>
        ''' Memulai encoder dengan parameter yang ditentukan.
        ''' </summary>
        ''' <param name="width">Lebar video input (BGRA)</param>
        ''' <param name="height">Tinggi video input (BGRA)</param>
        ''' <param name="fps">Target frame rate</param>
        ''' <returns>True jika berhasil dimulai</returns>
        Public Function Start(width As Integer, height As Integer, fps As Integer) As Boolean
            SyncLock _lockObj
                If _isRunning Then Return True
                If _isDisposed Then Return False

                ' Cek ketersediaan FFmpeg
                If Not mdl_FFmpegManager.FFmpegTersedia Then
                    Debug.WriteLine("[H264-ENC] FFmpeg not available")
                    Return False
                End If

                _width = width
                _height = height
                _fps = fps

                Try
                    ' Buat process encoder
                    _ffmpegProcess = mdl_FFmpegManager.BuatProcessEncoder(width, height, fps)
                    If _ffmpegProcess Is Nothing Then
                        Debug.WriteLine("[H264-ENC] Failed to create encoder process")
                        Return False
                    End If

                    _cancellationTokenSource = New CancellationTokenSource()

                    ' Start FFmpeg process
                    _ffmpegProcess.Start()

                    ' Tunggu sebentar untuk memastikan process berjalan
                    ' WaitForInputIdle() tidak bekerja untuk console app (tidak ada message loop)
                    ' Gunakan Thread.Sleep() sebagai gantinya
                    Try
                        _ffmpegProcess.WaitForInputIdle(STARTUP_TIMEOUT_MS)
                    Catch
                        ' Expected untuk console app - tidak ada message loop
                        ' Gunakan delay sederhana sebagai gantinya
                        Threading.Thread.Sleep(100)
                    End Try

                    ' Verifikasi process masih berjalan
                    If _ffmpegProcess.HasExited Then
                        Debug.WriteLine("[H264-ENC] FFmpeg exited immediately after start")
                        Return False
                    End If

                    ' Start read task untuk membaca output H.264
                    _readTask = Task.Run(Sub() ReadOutputLoop(_cancellationTokenSource.Token))

                    ' Start error task untuk membaca stderr (logging)
                    _errorTask = Task.Run(Sub() ReadErrorLoop(_cancellationTokenSource.Token))

                    _isRunning = True
                    Debug.WriteLine($"[H264-ENC] Started: {width}x{height} @ {fps}fps")
                    Return True

                Catch ex As Exception
                    Debug.WriteLine($"[H264-ENC] Start error: {ex.Message}")
                    Cleanup()
                    Return False
                End Try
            End SyncLock
        End Function

        ''' <summary>
        ''' Menghentikan encoder.
        ''' </summary>
        Public Sub [Stop]()
            SyncLock _lockObj
                If Not _isRunning Then Return

                Debug.WriteLine("[H264-ENC] Stopping...")

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

                RaiseEvent EncoderStopped(Me, EventArgs.Empty)
                Debug.WriteLine("[H264-ENC] Stopped")
            End SyncLock
        End Sub

        ''' <summary>
        ''' Mengirim raw BGRA frame ke encoder.
        ''' </summary>
        ''' <param name="bgraData">Raw BGRA pixel data (4 bytes per pixel)</param>
        ''' <returns>True jika berhasil dikirim</returns>
        Public Function SendFrame(bgraData As Byte()) As Boolean
            If Not _isRunning Then Return False
            If bgraData Is Nothing OrElse bgraData.Length = 0 Then Return False

            ' Validasi ukuran data
            Dim expectedSize = _width * _height * 4 ' BGRA = 4 bytes per pixel
            If bgraData.Length <> expectedSize Then
                Debug.WriteLine($"[H264-ENC] Frame size mismatch: got {bgraData.Length}, expected {expectedSize}")
                Return False
            End If

            Try
                ' Tulis ke stdin FFmpeg
                _ffmpegProcess.StandardInput.BaseStream.Write(bgraData, 0, bgraData.Length)
                _ffmpegProcess.StandardInput.BaseStream.Flush()
                Return True
            Catch ex As Exception
                Debug.WriteLine($"[H264-ENC] SendFrame error: {ex.Message}")
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Mengirim raw BGRA frame ke encoder secara async.
        ''' </summary>
        Public Async Function SendFrameAsync(bgraData As Byte()) As Task(Of Boolean)
            If Not _isRunning Then Return False
            If bgraData Is Nothing OrElse bgraData.Length = 0 Then Return False

            Dim expectedSize = _width * _height * 4
            If bgraData.Length <> expectedSize Then Return False

            Try
                Await _ffmpegProcess.StandardInput.BaseStream.WriteAsync(bgraData, 0, bgraData.Length)
                Await _ffmpegProcess.StandardInput.BaseStream.FlushAsync()
                Return True
            Catch ex As Exception
                Debug.WriteLine($"[H264-ENC] SendFrameAsync error: {ex.Message}")
                Return False
            End Try
        End Function

#End Region

#Region "Private Methods"

        ''' <summary>
        ''' Loop untuk membaca output H.264 dari FFmpeg stdout.
        ''' </summary>
        Private Sub ReadOutputLoop(token As CancellationToken)
            Dim buffer(READ_BUFFER_SIZE - 1) As Byte
            Dim totalBytesRead As Long = 0
            Dim readCount As Integer = 0

            Debug.WriteLine("[H264-ENC] ReadOutput loop started")

            Try
                While Not token.IsCancellationRequested
                    If _ffmpegProcess Is Nothing OrElse _ffmpegProcess.HasExited Then
                        Debug.WriteLine($"[H264-ENC] Process exited or null, ending read loop. HasExited={If(_ffmpegProcess IsNot Nothing, _ffmpegProcess.HasExited.ToString(), "NULL")}")
                        Exit While
                    End If

                    ' Baca dari stdout
                    Dim bytesRead = _ffmpegProcess.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length)

                    If bytesRead > 0 Then
                        readCount += 1
                        totalBytesRead += bytesRead

                        ' Copy data ke array baru
                        Dim data(bytesRead - 1) As Byte
                        Array.Copy(buffer, data, bytesRead)

                        ' Log setiap 10 reads
                        If readCount Mod 10 = 1 Then
                            Debug.WriteLine($"[H264-ENC] Read #{readCount}: {bytesRead} bytes, total={totalBytesRead / 1024:F1}KB")
                        End If

                        ' Raise event
                        RaiseEvent DataReady(Me, New H264DataEventArgs With {
                            .Data = data,
                            .Timestamp = DateTime.UtcNow
                        })
                    ElseIf bytesRead = 0 Then
                        ' EOF - process selesai
                        Debug.WriteLine("[H264-ENC] EOF received from stdout")
                        Exit While
                    End If
                End While
            Catch ex As OperationCanceledException
                Debug.WriteLine("[H264-ENC] ReadOutput cancelled")
            Catch ex As Exception
                Debug.WriteLine($"[H264-ENC] ReadOutput error: {ex.Message}")
                RaiseEvent EncoderError(Me, New EncoderErrorEventArgs With {
                    .Message = ex.Message,
                    .Exception = ex
                })
            End Try

            Debug.WriteLine($"[H264-ENC] ReadOutput loop ended. Total reads={readCount}, Total bytes={totalBytesRead / 1024:F1}KB")
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

                    Debug.WriteLine($"[FFMPEG-ERR] {line}")
                End While
            Catch ex As OperationCanceledException
                ' Normal cancellation
            Catch ex As Exception
                Debug.WriteLine($"[H264-ENC] ReadError error: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Membersihkan resources.
        ''' </summary>
        Private Sub Cleanup()
            Try
                _cancellationTokenSource?.Dispose()
                _cancellationTokenSource = Nothing

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
            Catch ex As Exception
                Debug.WriteLine($"[H264-ENC] Cleanup error: {ex.Message}")
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
    ''' Event args untuk data H.264 yang siap.
    ''' </summary>
    Public Class H264DataEventArgs
        Inherits EventArgs

        ''' <summary>Data H.264 (bisa berisi satu atau lebih NAL units)</summary>
        Public Property Data As Byte()

        ''' <summary>Timestamp ketika data dihasilkan</summary>
        Public Property Timestamp As DateTime
    End Class

    ''' <summary>
    ''' Event args untuk error encoder.
    ''' </summary>
    Public Class EncoderErrorEventArgs
        Inherits EventArgs

        ''' <summary>Pesan error</summary>
        Public Property Message As String

        ''' <summary>Exception (jika ada)</summary>
        Public Property Exception As Exception
    End Class

#End Region

End Namespace
