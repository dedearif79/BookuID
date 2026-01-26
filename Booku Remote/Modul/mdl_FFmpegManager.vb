Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports System.IO

''' <summary>
''' Modul untuk mengelola FFmpeg process lifecycle.
''' Mendeteksi ketersediaan FFmpeg dan mengelola process encoder/decoder.
''' </summary>
Public Module mdl_FFmpegManager

#Region "Constants"

    ''' <summary>Nama executable FFmpeg</summary>
    Private Const FFMPEG_EXE As String = "ffmpeg.exe"

    ''' <summary>Nama folder bundled FFmpeg relatif terhadap aplikasi</summary>
    Private Const FFMPEG_BUNDLED_FOLDER As String = "ffmpeg"

#End Region

#Region "Private Fields"

    ''' <summary>Path ke FFmpeg executable yang ditemukan</summary>
    Private _ffmpegPath As String = ""

    ''' <summary>Flag apakah FFmpeg sudah dicek</summary>
    Private _sudahDicek As Boolean = False

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' Path lengkap ke FFmpeg executable.
    ''' Kosong jika FFmpeg tidak tersedia.
    ''' </summary>
    Public ReadOnly Property FFmpegPath As String
        Get
            If Not _sudahDicek Then
                CekKetersediaanFFmpeg()
            End If
            Return _ffmpegPath
        End Get
    End Property

    ''' <summary>
    ''' True jika FFmpeg tersedia di sistem.
    ''' </summary>
    Public ReadOnly Property FFmpegTersedia As Boolean
        Get
            Return FFmpegPath <> ""
        End Get
    End Property

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Mengecek ketersediaan FFmpeg di sistem.
    ''' Urutan pencarian: bundled folder -> system PATH.
    ''' </summary>
    Public Sub CekKetersediaanFFmpeg()
        _sudahDicek = True
        _ffmpegPath = ""

        ' 1. Cek di folder bundled (relatif terhadap aplikasi)
        Dim bundledPath = CariFFmpegBundled()
        If bundledPath <> "" Then
            _ffmpegPath = bundledPath
            Debug.WriteLine($"[FFMPEG] Found bundled: {_ffmpegPath}")
            Return
        End If

        ' 2. Cek di system PATH
        Dim systemPath = CariFFmpegDiPath()
        If systemPath <> "" Then
            _ffmpegPath = systemPath
            Debug.WriteLine($"[FFMPEG] Found in PATH: {_ffmpegPath}")
            Return
        End If

        Debug.WriteLine("[FFMPEG] Not found")
    End Sub

    ''' <summary>
    ''' Mendapatkan versi FFmpeg yang terinstall.
    ''' </summary>
    ''' <returns>String versi atau "Not available" jika tidak tersedia</returns>
    Public Function DapatkanVersiFFmpeg() As String
        If Not FFmpegTersedia Then Return "Not available"

        Try
            Using proc As New Process()
                proc.StartInfo.FileName = FFmpegPath
                proc.StartInfo.Arguments = "-version"
                proc.StartInfo.UseShellExecute = False
                proc.StartInfo.RedirectStandardOutput = True
                proc.StartInfo.CreateNoWindow = True

                proc.Start()

                Dim output = proc.StandardOutput.ReadLine()
                proc.WaitForExit(3000)

                ' Parse versi dari output: "ffmpeg version N-xxxxx-g..."
                If output IsNot Nothing AndAlso output.StartsWith("ffmpeg version") Then
                    Dim parts = output.Split(" "c)
                    If parts.Length >= 3 Then
                        Return parts(2)
                    End If
                End If

                Return output?.Substring(0, Math.Min(50, If(output?.Length, 0))) & "..."
            End Using
        Catch ex As Exception
            Debug.WriteLine($"[FFMPEG] Error getting version: {ex.Message}")
            Return "Error"
        End Try
    End Function

    ''' <summary>
    ''' Membuat Process untuk FFmpeg encoder dengan parameter low-latency H.264.
    ''' </summary>
    ''' <param name="width">Lebar video</param>
    ''' <param name="height">Tinggi video</param>
    ''' <param name="fps">Target FPS</param>
    ''' <returns>Process yang sudah dikonfigurasi (belum dimulai)</returns>
    Public Function BuatProcessEncoder(width As Integer, height As Integer, fps As Integer) As Process
        If Not FFmpegTersedia Then Return Nothing

        Dim proc As New Process()

        ' Konfigurasi low-latency H.264 encoding
        ' Input: raw BGRA video via stdin (pipe:0)
        ' Output: H.264 elementary stream via stdout (pipe:1)
        ' PENTING: -pix_fmt yuv420p diperlukan karena baseline profile hanya support 4:2:0
        '          Input BGRA adalah 4:4:4, jadi perlu konversi
        Dim args = $"-f rawvideo -pix_fmt bgra -s {width}x{height} -r {fps} " &
                   "-i pipe:0 " &
                   "-c:v libx264 -preset ultrafast -tune zerolatency " &
                   "-profile:v baseline -level 3.0 " &
                   "-pix_fmt yuv420p " &
                   "-x264-params ""bframes=0:keyint=30:min-keyint=30"" " &
                   "-f h264 pipe:1"

        proc.StartInfo.FileName = FFmpegPath
        proc.StartInfo.Arguments = args
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardError = True
        proc.StartInfo.CreateNoWindow = True

        Debug.WriteLine($"[FFMPEG] Encoder args: {args}")

        Return proc
    End Function

    ''' <summary>
    ''' Membuat Process untuk FFmpeg decoder (H.264 ke raw frames).
    ''' Resolusi output akan di-scale ke ukuran yang ditentukan.
    ''' </summary>
    ''' <param name="width">Lebar video output</param>
    ''' <param name="height">Tinggi video output</param>
    ''' <returns>Process yang sudah dikonfigurasi (belum dimulai)</returns>
    Public Function BuatProcessDecoder(width As Integer, height As Integer) As Process
        If Not FFmpegTersedia Then Return Nothing

        Dim proc As New Process()

        ' Konfigurasi H.264 decoding
        ' Input: H.264 elementary stream via stdin (pipe:0)
        ' Output: raw BGRA frames via stdout (pipe:1)
        Dim args = $"-f h264 -i pipe:0 " &
                   $"-f rawvideo -pix_fmt bgra -s {width}x{height} " &
                   "pipe:1"

        proc.StartInfo.FileName = FFmpegPath
        proc.StartInfo.Arguments = args
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardError = True
        proc.StartInfo.CreateNoWindow = True

        Debug.WriteLine($"[FFMPEG] Decoder args: {args}")

        Return proc
    End Function

    ''' <summary>
    ''' Membuat Process untuk FFmpeg decoder (H.264 ke raw frames) tanpa scaling.
    ''' FFmpeg akan auto-detect resolusi dari SPS dan output dengan resolusi asli.
    ''' </summary>
    ''' <returns>Process yang sudah dikonfigurasi (belum dimulai)</returns>
    Public Function BuatProcessDecoderAutoResolusi() As Process
        If Not FFmpegTersedia Then Return Nothing

        Dim proc As New Process()

        ' Konfigurasi H.264 decoding tanpa specify output size
        ' FFmpeg akan auto-detect resolusi dari SPS dalam H.264 stream
        ' Input: H.264 elementary stream via stdin (pipe:0)
        ' Output: raw BGRA frames via stdout (pipe:1) dengan resolusi asli
        Dim args = "-f h264 -i pipe:0 " &
                   "-f rawvideo -pix_fmt bgra " &
                   "pipe:1"

        proc.StartInfo.FileName = FFmpegPath
        proc.StartInfo.Arguments = args
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardError = True
        proc.StartInfo.CreateNoWindow = True

        Debug.WriteLine($"[FFMPEG] Decoder args (auto-res): {args}")

        Return proc
    End Function

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Mencari FFmpeg di folder bundled.
    ''' </summary>
    Private Function CariFFmpegBundled() As String
        Try
            ' Path relatif terhadap executable aplikasi
            Dim appDir = AppDomain.CurrentDomain.BaseDirectory
            Dim bundledPath = Path.Combine(appDir, FFMPEG_BUNDLED_FOLDER, FFMPEG_EXE)

            If File.Exists(bundledPath) Then
                Return bundledPath
            End If

            ' Coba juga di folder aplikasi langsung
            Dim directPath = Path.Combine(appDir, FFMPEG_EXE)
            If File.Exists(directPath) Then
                Return directPath
            End If

        Catch ex As Exception
            Debug.WriteLine($"[FFMPEG] Error checking bundled: {ex.Message}")
        End Try

        Return ""
    End Function

    ''' <summary>
    ''' Mencari FFmpeg di system PATH.
    ''' </summary>
    Private Function CariFFmpegDiPath() As String
        Try
            Dim pathVar = Environment.GetEnvironmentVariable("PATH")
            If String.IsNullOrEmpty(pathVar) Then Return ""

            Dim paths = pathVar.Split(Path.PathSeparator)

            For Each pathDir In paths
                Dim fullPath = Path.Combine(pathDir.Trim(), FFMPEG_EXE)
                If File.Exists(fullPath) Then
                    Return fullPath
                End If
            Next

        Catch ex As Exception
            Debug.WriteLine($"[FFMPEG] Error checking PATH: {ex.Message}")
        End Try

        Return ""
    End Function

#End Region

End Module
