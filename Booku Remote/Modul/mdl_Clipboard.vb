Option Explicit On
Option Strict On

Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Media.Imaging

''' <summary>
''' Modul untuk sinkronisasi clipboard bidirectional antara Host dan Tamu.
''' Mendukung TEXT dan IMAGE (PNG, max 5MB).
''' Loop prevention dengan hash comparison + grace period.
''' </summary>
Public Module mdl_Clipboard

#Region "Constants"

    ''' <summary>Tipe data clipboard: Teks</summary>
    Public Const CLIPBOARD_TYPE_TEXT As String = "TEXT"

    ''' <summary>Tipe data clipboard: Gambar</summary>
    Public Const CLIPBOARD_TYPE_IMAGE As String = "IMAGE"

    ''' <summary>Sumber clipboard: Host</summary>
    Public Const CLIPBOARD_SOURCE_HOST As String = "HOST"

    ''' <summary>Sumber clipboard: Tamu</summary>
    Public Const CLIPBOARD_SOURCE_TAMU As String = "TAMU"

    ''' <summary>Grace period setelah menerima clipboard dari remote (ms)</summary>
    Private Const GRACE_PERIOD_MS As Integer = 500

    ''' <summary>Maksimum ukuran gambar clipboard (5MB)</summary>
    Private Const MAX_IMAGE_SIZE As Integer = 5 * 1024 * 1024

    ''' <summary>Interval polling clipboard (ms)</summary>
    Private Const POLLING_INTERVAL_MS As Integer = 250

#End Region

#Region "State Variables"

    ''' <summary>Flag untuk mengaktifkan/menonaktifkan clipboard sync</summary>
    Public ClipboardSyncAktif As Boolean = False

    ''' <summary>Flag izin dari Host untuk clipboard</summary>
    Public IzinClipboardDariHost As Boolean = False

    ''' <summary>Hash terakhir dari clipboard lokal</summary>
    Private _lastLocalClipboardHash As String = ""

    ''' <summary>Hash terakhir dari clipboard remote (untuk loop prevention)</summary>
    Private _lastRemoteClipboardHash As String = ""

    ''' <summary>Timestamp terakhir menerima update dari remote</summary>
    Private _lastRemoteUpdateTicks As Long = 0

    ''' <summary>Thread untuk polling clipboard</summary>
    Private _clipboardPollingThread As Thread = Nothing

    ''' <summary>Flag untuk menghentikan polling</summary>
    Private _stopPolling As Boolean = False

    ''' <summary>Callback untuk kirim clipboard ke peer</summary>
    Public ClipboardKirimCallback As Action(Of cls_PayloadClipboard) = Nothing

#End Region

#Region "Clipboard Monitoring"

    ''' <summary>
    ''' Memulai monitoring clipboard dengan polling.
    ''' WPF tidak support clipboard notification secara langsung,
    ''' jadi kita gunakan polling dengan interval kecil.
    ''' </summary>
    ''' <param name="source">Sumber: "HOST" atau "TAMU"</param>
    Public Sub MulaiClipboardMonitoring(source As String)
        WriteLog($"[CLIPBOARD] MulaiClipboardMonitoring dipanggil, source={source}, ClipboardSyncAktif={ClipboardSyncAktif}, Callback={ClipboardKirimCallback IsNot Nothing}")

        If _clipboardPollingThread IsNot Nothing AndAlso _clipboardPollingThread.IsAlive Then
            WriteLog("[CLIPBOARD] Monitoring sudah berjalan - skip")
            Return
        End If

        _stopPolling = False
        _lastLocalClipboardHash = ""
        _lastRemoteClipboardHash = ""

        _clipboardPollingThread = New Thread(Sub() PollingClipboardLoop(source))
        _clipboardPollingThread.SetApartmentState(ApartmentState.STA) ' Clipboard butuh STA
        _clipboardPollingThread.IsBackground = True
        _clipboardPollingThread.Name = "ClipboardPolling"
        _clipboardPollingThread.Start()

        WriteLog($"[CLIPBOARD] Monitoring thread dimulai sebagai {source}, ThreadId={_clipboardPollingThread.ManagedThreadId}")
    End Sub

    ''' <summary>
    ''' Menghentikan monitoring clipboard.
    ''' </summary>
    Public Sub HentikanClipboardMonitoring()
        _stopPolling = True
        If _clipboardPollingThread IsNot Nothing Then
            Try
                If _clipboardPollingThread.IsAlive Then
                    _clipboardPollingThread.Join(1000)
                End If
            Catch
            End Try
            _clipboardPollingThread = Nothing
        End If
        WriteLog("[CLIPBOARD] Monitoring dihentikan")
    End Sub

    ''' <summary>
    ''' Loop polling clipboard untuk mendeteksi perubahan.
    ''' </summary>
    Private _pollingLoopCount As Integer = 0
    Private Sub PollingClipboardLoop(source As String)
        WriteLog($"[CLIPBOARD] PollingClipboardLoop STARTED, source={source}")
        _pollingLoopCount = 0

        While Not _stopPolling AndAlso ClipboardSyncAktif
            Try
                _pollingLoopCount += 1

                ' Log setiap 20 iterasi (5 detik) untuk memastikan loop berjalan
                If _pollingLoopCount Mod 20 = 1 Then
                    WriteLog($"[CLIPBOARD] Polling loop #{_pollingLoopCount}, _stopPolling={_stopPolling}, ClipboardSyncAktif={ClipboardSyncAktif}")
                End If

                ' Cek apakah dalam grace period (baru terima dari remote)
                Dim ticksNow = DateTime.UtcNow.Ticks
                Dim msSinceRemote = (ticksNow - _lastRemoteUpdateTicks) / TimeSpan.TicksPerMillisecond
                If msSinceRemote < GRACE_PERIOD_MS Then
                    Thread.Sleep(POLLING_INTERVAL_MS)
                    Continue While
                End If

                ' Baca clipboard saat ini
                Dim clipboardData = BacaClipboard()
                If clipboardData IsNot Nothing Then
                    ' Hash data
                    Dim currentHash = HitungHashClipboard(clipboardData.Data)

                    ' Cek apakah berbeda dari lokal terakhir DAN bukan dari remote
                    If currentHash <> _lastLocalClipboardHash AndAlso
                       currentHash <> _lastRemoteClipboardHash Then

                        ' Ada perubahan lokal - kirim ke peer
                        clipboardData.Source = source
                        clipboardData.HashData = currentHash
                        clipboardData.Timestamp = DateTime.UtcNow.Ticks

                        _lastLocalClipboardHash = currentHash

                        ' Kirim via callback
                        If ClipboardKirimCallback IsNot Nothing Then
                            WriteLog($"[CLIPBOARD] CHANGE DETECTED! Mengirim {clipboardData.TipeData} ({clipboardData.Data.Length} chars), hash={currentHash.Substring(0, 8)}")
                            ClipboardKirimCallback(clipboardData)
                        Else
                            WriteLog($"[CLIPBOARD] CHANGE DETECTED tapi ClipboardKirimCallback Is Nothing!")
                        End If
                    End If
                End If

            Catch ex As Exception
                WriteLog($"[CLIPBOARD] Error polling: {ex.Message}")
                WriteLog($"[CLIPBOARD] Stack: {ex.StackTrace}")
            End Try

            Thread.Sleep(POLLING_INTERVAL_MS)
        End While

        WriteLog($"[CLIPBOARD] PollingClipboardLoop ENDED, _stopPolling={_stopPolling}, ClipboardSyncAktif={ClipboardSyncAktif}")
    End Sub

    ''' <summary>
    ''' Membaca isi clipboard saat ini.
    ''' </summary>
    Private Function BacaClipboard() As cls_PayloadClipboard
        Try
            ' Coba baca teks dulu
            If Clipboard.ContainsText() Then
                Dim text = Clipboard.GetText()
                If Not String.IsNullOrEmpty(text) Then
                    Return New cls_PayloadClipboard With {
                        .TipeData = CLIPBOARD_TYPE_TEXT,
                        .Data = text
                    }
                End If
            End If

            ' Coba baca gambar
            If Clipboard.ContainsImage() Then
                Dim image = Clipboard.GetImage()
                If image IsNot Nothing Then
                    Dim base64 = BitmapSourceKeBase64(image)
                    If Not String.IsNullOrEmpty(base64) AndAlso base64.Length <= MAX_IMAGE_SIZE Then
                        Return New cls_PayloadClipboard With {
                            .TipeData = CLIPBOARD_TYPE_IMAGE,
                            .Data = base64
                        }
                    ElseIf base64 IsNot Nothing AndAlso base64.Length > MAX_IMAGE_SIZE Then
                        WriteLog($"[CLIPBOARD] Gambar terlalu besar: {base64.Length / 1024}KB > {MAX_IMAGE_SIZE / 1024}KB")
                    End If
                End If
            End If

        Catch ex As Exception
            ' Clipboard mungkin sedang diakses proses lain
            ' WriteLog($"[CLIPBOARD] Error baca: {ex.Message}")
        End Try

        Return Nothing
    End Function

#End Region

#Region "Clipboard Write (dari Remote)"

    ''' <summary>
    ''' Menerima dan menulis clipboard dari remote ke lokal.
    ''' </summary>
    Public Sub TerimaClipboardDariRemote(payload As cls_PayloadClipboard)
        WriteLog($"[CLIPBOARD] TerimaClipboardDariRemote dipanggil, payload IsNot Nothing={payload IsNot Nothing}, ClipboardSyncAktif={ClipboardSyncAktif}")

        If payload Is Nothing Then
            WriteLog("[CLIPBOARD] TerimaClipboardDariRemote: payload Is Nothing - SKIP")
            Return
        End If
        If Not ClipboardSyncAktif Then
            WriteLog("[CLIPBOARD] TerimaClipboardDariRemote: ClipboardSyncAktif=False - SKIP")
            Return
        End If

        Try
            WriteLog($"[CLIPBOARD] Menerima dari remote: TipeData={payload.TipeData}, Source={payload.Source}, DataLen={payload.Data?.Length}, Hash={payload.HashData?.Substring(0, Math.Min(8, If(payload.HashData?.Length, 0)))}")

            ' Set grace period dan hash
            _lastRemoteUpdateTicks = DateTime.UtcNow.Ticks
            _lastRemoteClipboardHash = payload.HashData

            ' Tulis ke clipboard (harus di UI thread)
            Application.Current?.Dispatcher?.Invoke(Sub()
                                                        Try
                                                            Select Case payload.TipeData
                                                                Case CLIPBOARD_TYPE_TEXT
                                                                    Clipboard.SetText(payload.Data)
                                                                    WriteLog($"[CLIPBOARD] SUCCESS: Tulis TEXT ke clipboard lokal, {payload.Data.Length} chars dari {payload.Source}")

                                                                Case CLIPBOARD_TYPE_IMAGE
                                                                    Dim image = Base64KeBitmapSource(payload.Data)
                                                                    If image IsNot Nothing Then
                                                                        Clipboard.SetImage(image)
                                                                        WriteLog($"[CLIPBOARD] SUCCESS: Tulis IMAGE ke clipboard lokal, {payload.Data.Length / 1024}KB dari {payload.Source}")
                                                                    Else
                                                                        WriteLog($"[CLIPBOARD] FAILED: Gagal decode IMAGE dari Base64")
                                                                    End If

                                                                Case Else
                                                                    WriteLog($"[CLIPBOARD] WARNING: TipeData tidak dikenal: {payload.TipeData}")
                                                            End Select
                                                        Catch ex As Exception
                                                            WriteLog($"[CLIPBOARD] Error tulis ke clipboard: {ex.Message}")
                                                        End Try
                                                    End Sub)

        Catch ex As Exception
            WriteLog($"[CLIPBOARD] Error terima: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Helper Functions"

    ''' <summary>
    ''' Menghitung MD5 hash dari data clipboard.
    ''' </summary>
    Public Function HitungHashClipboard(data As String) As String
        If String.IsNullOrEmpty(data) Then Return ""

        Using md5 As MD5 = MD5.Create()
            Dim hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data))
            Return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant()
        End Using
    End Function

    ''' <summary>
    ''' Konversi BitmapSource ke Base64 PNG.
    ''' </summary>
    Private Function BitmapSourceKeBase64(image As BitmapSource) As String
        Try
            Dim encoder As New PngBitmapEncoder()
            encoder.Frames.Add(BitmapFrame.Create(image))

            Using ms As New MemoryStream()
                encoder.Save(ms)
                Return Convert.ToBase64String(ms.ToArray())
            End Using
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Konversi Base64 PNG ke BitmapSource.
    ''' </summary>
    Private Function Base64KeBitmapSource(base64 As String) As BitmapSource
        Try
            Dim bytes = Convert.FromBase64String(base64)
            Using ms As New MemoryStream(bytes)
                Dim decoder = BitmapDecoder.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad)
                Return decoder.Frames(0)
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Reset state clipboard sync.
    ''' </summary>
    Public Sub ResetClipboardState()
        _lastLocalClipboardHash = ""
        _lastRemoteClipboardHash = ""
        _lastRemoteUpdateTicks = 0
        ClipboardSyncAktif = False
        IzinClipboardDariHost = False
        ClipboardKirimCallback = Nothing
    End Sub

#End Region

End Module
