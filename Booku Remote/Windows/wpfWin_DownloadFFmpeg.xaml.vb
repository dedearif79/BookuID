Option Explicit On
Option Strict On

Imports System.IO
Imports System.IO.Compression
Imports System.Net.Http
Imports System.Windows
Imports BookuID.Styles

''' <summary>
''' Window untuk download dan ekstrak FFmpeg dari server.
''' </summary>
Class wpfWin_DownloadFFmpeg

#Region "Constants"

    ''' <summary>URL download FFmpeg ZIP dari server</summary>
    Private Const URL_FFMPEG_ZIP As String = "https://booku.id/booku/support/ffmpeg.zip"

    ''' <summary>Nama file ZIP yang didownload</summary>
    Private Const NAMA_FILE_ZIP As String = "ffmpeg.zip"

#End Region

#Region "Private Fields"

    ''' <summary>Flag apakah download berhasil</summary>
    Private _downloadBerhasil As Boolean = False

    ''' <summary>Flag untuk membatalkan download</summary>
    Private _batalDownload As Boolean = False

    ''' <summary>Path folder aplikasi</summary>
    Private ReadOnly _folderAplikasi As String

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' True jika download dan ekstrak berhasil.
    ''' </summary>
    Public ReadOnly Property DownloadBerhasil As Boolean
        Get
            Return _downloadBerhasil
        End Get
    End Property

#End Region

#Region "Constructor"

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        StyleWindowDialogWPF_TanpaTombolX(Me)

        _folderAplikasi = AppDomain.CurrentDomain.BaseDirectory
    End Sub

#End Region

#Region "Window Events"

    Private Async Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Mulai proses download otomatis
        Await MulaiDownloadAsync()
    End Sub

    Private Sub wpfWin_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Batalkan download jika masih berjalan
        _batalDownload = True
    End Sub

#End Region

#Region "Button Click Handlers"

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        _batalDownload = True
        lbl_Status.Text = "Membatalkan download..."
        btn_Batal.IsEnabled = False
    End Sub

#End Region

#Region "Download Logic"

    ''' <summary>
    ''' Memulai proses download FFmpeg.
    ''' </summary>
    Private Async Function MulaiDownloadAsync() As Task
        _downloadBerhasil = False
        _batalDownload = False

        btn_Batal.Visibility = Visibility.Visible

        Dim pathZip = Path.Combine(_folderAplikasi, NAMA_FILE_ZIP)

        Try
            ' === TAHAP 1: DOWNLOAD ===
            lbl_Status.Text = "Mengunduh file dari server..."
            Dim hasilDownload = Await DownloadFileAsync(URL_FFMPEG_ZIP, pathZip)

            If Not hasilDownload Then
                If _batalDownload Then
                    lbl_Status.Text = "Download dibatalkan."
                Else
                    lbl_Status.Text = "Gagal mengunduh file. Silakan coba lagi."
                End If
                Await Task.Delay(2000)
                Me.Close()
                Return
            End If

            ' === TAHAP 2: EKSTRAK ===
            lbl_Status.Text = "Mengekstrak file..."
            pgb_Progress.Value = 0
            lbl_Progress.Text = "Ekstrak..."

            Dim hasilEkstrak = Await EkstrakFileAsync(pathZip, _folderAplikasi)

            If Not hasilEkstrak Then
                lbl_Status.Text = "Gagal mengekstrak file."
                Await Task.Delay(2000)
                Me.Close()
                Return
            End If

            ' === TAHAP 3: HAPUS ZIP ===
            lbl_Status.Text = "Membersihkan file temporary..."
            Try
                If File.Exists(pathZip) Then
                    File.Delete(pathZip)
                End If
            Catch
                ' Abaikan error hapus - tidak kritis
            End Try

            ' === SELESAI ===
            _downloadBerhasil = True
            pgb_Progress.Value = 100
            lbl_Progress.Text = "100%"
            lbl_Status.Text = "Download selesai! Memulai aplikasi..."
            btn_Batal.Visibility = Visibility.Collapsed

            Await Task.Delay(1500)
            Me.Close()

        Catch ex As Exception
            lbl_Status.Text = $"Error: {ex.Message}"
        End Try

        ' Delay dan close jika ada error (di luar try-catch karena Await tidak bisa di dalam Catch)
        If Not _downloadBerhasil Then
            Await Task.Delay(3000)
            Me.Close()
        End If
    End Function

    ''' <summary>
    ''' Download file dari URL dengan progress.
    ''' </summary>
    Private Async Function DownloadFileAsync(url As String, localPath As String) As Task(Of Boolean)
        Try
            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromMinutes(10)

                ' Kirim request untuk mendapatkan stream
                Using response As HttpResponseMessage = Await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead)
                    response.EnsureSuccessStatusCode()

                    Dim totalBytes As Long = If(response.Content.Headers.ContentLength.HasValue,
                                                response.Content.Headers.ContentLength.Value, 0)

                    Using inputStream = Await response.Content.ReadAsStreamAsync(),
                          outputStream As New FileStream(localPath, FileMode.Create, FileAccess.Write, FileShare.None)

                        Dim buffer(64 * 1024 - 1) As Byte
                        Dim bytesRead As Integer
                        Dim totalBytesRead As Long = 0

                        Do
                            If _batalDownload Then
                                Return False
                            End If

                            bytesRead = Await inputStream.ReadAsync(buffer, 0, buffer.Length)
                            If bytesRead <= 0 Then Exit Do

                            Await outputStream.WriteAsync(buffer, 0, bytesRead)
                            totalBytesRead += bytesRead

                            ' Update progress
                            Dim persen As Double = If(totalBytes > 0, (totalBytesRead * 100.0R / totalBytes), 0)
                            UpdateProgress(persen, If(totalBytes > 0,
                                                     $"Download: {persen:F1}% ({totalBytesRead \ 1024} KB / {totalBytes \ 1024} KB)",
                                                     $"Download: {totalBytesRead \ 1024} KB"))
                        Loop

                        Return True
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Debug.WriteLine($"[DOWNLOAD] Error: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Ekstrak file ZIP dengan progress.
    ''' </summary>
    Private Async Function EkstrakFileAsync(zipPath As String, extractPath As String) As Task(Of Boolean)
        Try
            Return Await Task.Run(Function()
                                      Using zip As ZipArchive = ZipFile.OpenRead(zipPath)
                                          Dim totalEntries As Integer = zip.Entries.Count
                                          Dim currentEntry As Integer = 0

                                          For Each entry As ZipArchiveEntry In zip.Entries
                                              If _batalDownload Then
                                                  Return False
                                              End If

                                              Dim destinationPath As String = Path.Combine(extractPath, entry.FullName)

                                              If Not String.IsNullOrEmpty(entry.Name) Then
                                                  Directory.CreateDirectory(Path.GetDirectoryName(destinationPath))
                                                  entry.ExtractToFile(destinationPath, True)
                                              End If

                                              currentEntry += 1
                                              Dim persen As Integer = CInt((currentEntry / totalEntries) * 100)

                                              ' Update progress di UI thread
                                              Dispatcher.Invoke(Sub()
                                                                    pgb_Progress.Value = persen
                                                                    lbl_Progress.Text = $"Ekstrak: {persen}%"
                                                                End Sub)
                                          Next

                                          Return True
                                      End Using
                                  End Function)

        Catch ex As Exception
            Debug.WriteLine($"[EKSTRAK] Error: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Update progress bar dan label dari thread manapun.
    ''' </summary>
    Private Sub UpdateProgress(persen As Double, text As String)
        If Dispatcher.CheckAccess() Then
            pgb_Progress.Value = persen
            lbl_Progress.Text = text
        Else
            Dispatcher.BeginInvoke(Sub()
                                       pgb_Progress.Value = persen
                                       lbl_Progress.Text = text
                                   End Sub)
        End If
    End Sub

#End Region

End Class
