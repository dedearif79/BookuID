' Suppress warning untuk FtpWebRequest yang sudah obsolete tapi tidak ada pengganti langsung di .NET modern
' Microsoft merekomendasikan library pihak ketiga (seperti FluentFTP) untuk FTP di .NET 5+
' FtpWebRequest masih berfungsi, hanya ditandai obsolete
#Disable Warning SYSLIB0014

Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Security.Cryptography
Imports System.Text.Json
Imports System.Threading
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Threading


Public Module mdlPub_ModulUmum

    Public FolderRootBookuID As String = "C:\BookuID\"

    'Setingan Region Aplikasi :
    Public appCulture As CultureInfo
    Public appRegion As RegionInfo

    Public standardCultureName As String = "id-ID"
    Public standardRegionName As String = "ID"
    Public standardShortDateFormat As String = "dd/MM/yyyy"
    Public standardLongDateFormat As String = "dddd, dd MMMM yyyy"
    Public standardPemisahRibuan As String = "."
    Public standardPemisahDesimal As String = ","


    'Setingan Region pada Windows :
    Public currentCulture As CultureInfo = CultureInfo.CurrentCulture
    Public currentRegion As New RegionInfo(CultureInfo.CurrentCulture.Name)
    Public currentDateFormat As DateTimeFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat
    Public currentNumberFormat As NumberFormatInfo = CultureInfo.CurrentUICulture.NumberFormat

    Public currentCultureName = currentCulture.Name
    Public currentRegionName = currentRegion.Name
    Public currentShortDateFormat = currentDateFormat.ShortDatePattern
    Public currentLongDateFormat = currentDateFormat.LongDatePattern
    Public currentPemisahRibuan = currentNumberFormat.NumberGroupSeparator
    Public currentPemisahDesimal = currentNumberFormat.NumberDecimalSeparator

    'Public APIKey_PHPCode As String = "kJU7^%^yGR$2579"
    Public APIKey_PHPCode As String = DekripsiTeks("!&Tz3WVgf-suUks<HR7A?V@X;t=q;cc-:E@4(#BLE66hCIOe@m7F>|X?*-Dl?R||]Y276~E+BS41yT:ze{k+&V=^Y:?~s~*b;2FtvXlR(Nz4cW*zv3NCc2)>(Y(9MHOVwlW)T7)^|>T<^925fuO&S#LIo2QOA2(i9&~$d^e><]a*GRgSpo]LO3EG8C/L^sF9HyZ-u/I%)zR^#-Y|N6+MA%p+67FXi*G^v7}PL1KQ87Fe+V4P%p>U8lzPPy?2tJF~ROGINm[k!_8!HQ}A[")





    'Pewarnaan :
    Public WarnaAbuAbu_WPF As New SolidColorBrush(Colors.Gray)
    Public WarnaBiruSolid_WPF As New SolidColorBrush(Colors.Blue)
    Public WarnaBiruTerang_WPF As New SolidColorBrush(Colors.LightBlue)
    Public WarnaMerahSolid_WPF As New SolidColorBrush(Colors.Red)
    Public WarnaMerahTerang_WPF As New SolidColorBrush(Colors.Pink)
    Public WarnaKuningSolid_WPF As New SolidColorBrush(Colors.Yellow)
    Public WarnaKuningTerang_WPF As New SolidColorBrush(Colors.LightYellow)
    Public WarnaHijauSolid_WPF As New SolidColorBrush(Colors.Green)
    Public WarnaHijauTerang_WPF As New SolidColorBrush(Colors.LightGreen)
    Public WarnaHijauProgressBar_WPF As New SolidColorBrush(Color.FromRgb(0, 170, 0))
    Public WarnaPutih_WPF As New SolidColorBrush(Colors.White)
    Public WarnaPutihAsap_WPF As New SolidColorBrush(Colors.WhiteSmoke)
    Public WarnaHitam_5_WPF As New SolidColorBrush(Color.FromRgb(242, 242, 242))
    Public WarnaHitam_10_WPF As New SolidColorBrush(Color.FromRgb(230, 230, 230))
    Public WarnaHitam_15_WPF As New SolidColorBrush(Color.FromRgb(217, 217, 217))
    Public WarnaHitam_20_WPF As New SolidColorBrush(Color.FromRgb(204, 204, 204))
    Public WarnaHitam_25_WPF As New SolidColorBrush(Color.FromRgb(191, 191, 191))
    Public WarnaHitam_30_WPF As New SolidColorBrush(Color.FromRgb(179, 179, 179))
    Public WarnaHitam_35_WPF As New SolidColorBrush(Color.FromRgb(166, 166, 166))
    Public WarnaHitam_40_WPF As New SolidColorBrush(Color.FromRgb(153, 153, 153))
    Public WarnaHitam_45_WPF As New SolidColorBrush(Color.FromRgb(140, 140, 140))
    Public WarnaHitam_50_WPF As New SolidColorBrush(Color.FromRgb(128, 128, 128))
    Public WarnaHitam_55_WPF As New SolidColorBrush(Color.FromRgb(115, 115, 115))
    Public WarnaHitam_60_WPF As New SolidColorBrush(Color.FromRgb(102, 102, 102))
    Public WarnaHitam_65_WPF As New SolidColorBrush(Color.FromRgb(89, 89, 89))
    Public WarnaHitam_70_WPF As New SolidColorBrush(Color.FromRgb(77, 77, 77))
    Public WarnaHitam_75_WPF As New SolidColorBrush(Color.FromRgb(64, 64, 64))
    Public WarnaHitam_80_WPF As New SolidColorBrush(Color.FromRgb(51, 51, 51))
    Public WarnaHitam_85_WPF As New SolidColorBrush(Color.FromRgb(38, 38, 38))
    Public WarnaHitam_90_WPF As New SolidColorBrush(Color.FromRgb(26, 26, 26))
    Public WarnaHitam_95_WPF As New SolidColorBrush(Color.FromRgb(13, 13, 13))
    Public WarnaHitam_100_WPF As New SolidColorBrush(Color.FromRgb(0, 0, 0))
    Public WarnaHitamSolid_WPF As New SolidColorBrush(Colors.Black)


    Public WarnaAlternatif_1_WPF = WarnaBiruSolid_WPF
    Public WarnaDasar_WPF = WarnaPutihAsap_WPF
    Public WarnaTeksStandar_WPF = WarnaHitam_70_WPF
    Public WarnaTegas_WPF = WarnaHitamSolid_WPF
    Public WarnaPudar_WPF = WarnaAbuAbu_WPF
    Public WarnaPeringatan_WPF = WarnaMerahSolid_WPF
    Public WarnaBermasalahTerseleksi_WPF = WarnaMerahTerang_WPF
    Public WarnaDataTahunLalu_WPF As New SolidColorBrush(Color.FromArgb(255, 63, 63, 128))


    Sub StandarisasiSetinganAplikasi()

        'Setingan Region :
        appCulture = New CultureInfo(standardCultureName)
        appRegion = New RegionInfo(standardRegionName)

        'Setingan Tanggal :
        appCulture.DateTimeFormat.ShortDatePattern = standardShortDateFormat
        appCulture.DateTimeFormat.LongDatePattern = standardLongDateFormat

        'Setingan Angka :
        appCulture.NumberFormat.NumberGroupSeparator = standardPemisahRibuan
        appCulture.NumberFormat.NumberDecimalSeparator = standardPemisahDesimal

        'Penerapan :
        Thread.CurrentThread.CurrentCulture = appCulture
        Thread.CurrentThread.CurrentUICulture = appCulture

    End Sub


    Public Sub Terabas()
        Dim frame As New DispatcherFrame()
        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, New DispatcherOperationCallback(Function(obj)
                                                                                                                    frame.Continue = False
                                                                                                                    Return Nothing
                                                                                                                End Function), Nothing)
        Dispatcher.PushFrame(frame)
    End Sub




    Public Sub BuatFolder(FolderPath As String)
        If Not Directory.Exists(FolderPath) Then Directory.CreateDirectory(FolderPath)
    End Sub

    Public HapusFolderBerhasil As Boolean
    Sub HapusFolder(FolderPath As String)
        Try
            Directory.Delete(FolderPath, True)
            HapusFolderBerhasil = True
        Catch ex As Exception
            HapusFolderBerhasil = False
        End Try
    End Sub

    Public HapusHanyaFiledalamFolderBerhasil As Boolean
    Sub HapusHanyaFileDalamFolder(FolderPath As String)
        Try
            Dim files As String() = Directory.GetFiles(FolderPath)
            For Each berkas In files
                File.Delete(berkas)
            Next
            HapusHanyaFiledalamFolderBerhasil = True
        Catch ex As Exception
            HapusHanyaFiledalamFolderBerhasil = False
        End Try
    End Sub

    Public HapusFileBerhasil As Boolean
    Sub HapusFile(FilePath As String)
        Try
            File.Delete(FilePath)
            HapusFileBerhasil = True
        Catch ex As Exception
            HapusFileBerhasil = False
        End Try
    End Sub

    Public SalinFileBerhasil As Boolean
    Sub SalinFile(ByVal sourceFile As String, ByVal destinationFile As String)
        If Not File.Exists(sourceFile) Then
            SalinFileBerhasil = False
            Return
        End If
        Try
            'Copy file dengan overwrite jika sudah ada :
            File.Copy(sourceFile, destinationFile, True)
            SalinFileBerhasil = True
        Catch ex As Exception
            SalinFileBerhasil = False
        End Try
    End Sub


    Public SalinFolderBerhasil As Boolean
    Public Sub SalinFolder(sourcePath As String, destinationPath As String)
        SalinFolderBerhasil = False
        If String.IsNullOrEmpty(sourcePath) Then Return
        If String.IsNullOrEmpty(destinationPath) Then Return
        If sourcePath.Equals(destinationPath, StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("Folder tujuan tidak boleh sama dengan folder sumber!", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If
        Try
            CopyDirectory(sourcePath, destinationPath)
            SalinFolderBerhasil = True
        Catch ex As Exception
            SalinFolderBerhasil = False
        End Try
    End Sub
    Private Sub CopyDirectory(sourceDir As String, destDir As String)
        ' Buat folder tujuan jika belum ada
        If Not Directory.Exists(destDir) Then
            Directory.CreateDirectory(destDir)
        End If
        ' Pastikan folder tujuan ada
        If Not Directory.Exists(destDir) Then
            Directory.CreateDirectory(destDir)
        End If
        ' Salin semua file dalam folder sumber ke folder tujuan
        For Each filePath As String In Directory.GetFiles(sourceDir)
            Dim fileName As String = Path.GetFileName(filePath) ' Ambil nama file saja
            Dim destFilePath As String = Path.Combine(destDir, fileName) ' Buat path tujuan
            File.Copy(filePath, destFilePath, True)
        Next
        ' Salin semua subfolder secara rekursif
        For Each subDir As String In Directory.GetDirectories(sourceDir)
            Dim destSubDir As String = Path.Combine(destDir, Path.GetFileName(subDir))
            CopyDirectory(subDir, destSubDir)
        Next
    End Sub

    Public RenameFolderBerhasil As Boolean
    Sub RenameFolder(FolderLama As String, FolderBaru As String)
        Try
            ' Pastikan folder lama ada sebelum merename
            If Directory.Exists(FolderLama) Then
                Directory.Move(FolderLama, FolderBaru)
                RenameFolderBerhasil = True
            Else
                RenameFolderBerhasil = False
                'MsgBox("Folder lama tidak ditemukan.")
            End If
        Catch ex As IOException
            RenameFolderBerhasil = False
            'MsgBox("Kesalahan I/O: " & ex.Message)
        Catch ex As UnauthorizedAccessException
            RenameFolderBerhasil = False
            'MsgBox("Akses ditolak: " & ex.Message)
        Catch ex As Exception
            RenameFolderBerhasil = False
            'MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub



    Public httpKlien As HttpClient
    Public cts As CancellationTokenSource
    Public DownloadBerhasil As Boolean
    Public DownloadDibatalkan As Boolean
    Public Async Function DownloadFile_MetodeFTP(url As String, filePath As String, pgb_Progress As ProgressBar, lbl_Progress As TextBlock) As Task

        DownloadBerhasil = False
        DownloadDibatalkan = False

        ' Inisialisasi HttpClient dan CancellationToken
        httpKlien = New HttpClient
        cts = New CancellationTokenSource()

        Try
            ' Memulai download dengan progress
            Await DownloadFileAsync_MetodeFTP(url, filePath, cts.Token, pgb_Progress, lbl_Progress)
            lbl_Progress.Text = "Download selesai!"
            DownloadBerhasil = True
        Catch ex As OperationCanceledException
            DownloadDibatalkan = True
            lbl_Progress.Text = "Download dibatalkan!"
        Catch ex As Exception
            DownloadBerhasil = False
            lbl_Progress.Text = "Terjadi kesalahan: " & ex.Message
        End Try

    End Function

    Public Async Function DownloadFileAsync_MetodeFTP(url As String, filePath As String, cancellationToken As CancellationToken, pgb_Progress As ProgressBar, lbl_Progress As TextBlock) As Task
        Dim dispat As New DispatcherFrame
        Using response As HttpResponseMessage = Await httpKlien.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            response.EnsureSuccessStatusCode()

            Dim totalBytes As Long = response.Content.Headers.ContentLength.GetValueOrDefault()
            Dim downloadedBytes As Long = 0

            Using stream As Stream = Await response.Content.ReadAsStreamAsync(),
                  fileStream As FileStream = New FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, True)

                Dim buffer(8191) As Byte
                Dim bytesRead As Integer
                Do
                    bytesRead = Await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)
                    If bytesRead = 0 Then Exit Do

                    Await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken)
                    downloadedBytes += bytesRead

                    ' Update progress
                    Dim percent As Integer = If(totalBytes > 0, CInt((downloadedBytes * 100) / totalBytes), 0)
                    dispat.Dispatcher.Invoke(Sub()
                                                 pgb_Progress.Value = percent
                                                 lbl_Progress.Text = $"Download... {percent} %"
                                             End Sub)
                Loop
            End Using
        End Using
    End Function


    ' Return: True jika sukses, False jika gagal (label akan menampilkan pesan)
    Public Async Function DownloadFileDariServerAsync_MetodeHTTP(sourcePath As String,
                                                                 localDestPath As String,
                                                                 url_FileDownloader_PHP As String,
                                                                 pgb_Progress As ProgressBar,
                                                                 lbl_Progress As TextBlock,
                                                                 Optional useSsl As Boolean = True) As Task(Of Boolean)

        Dim Sukses As Boolean = False

        ' --- helper update UI aman cross-thread ---
        Dim ui = pgb_Progress.Dispatcher
        Dim SafeUpdate As Action(Of Double, String) =
        Sub(pct As Double, caption As String)
            Dim p = Math.Max(0, Math.Min(100, pct))
            If ui.CheckAccess() Then
                pgb_Progress.Value = p
                lbl_Progress.Text = caption
            Else
                ui.BeginInvoke(Sub()
                                   pgb_Progress.Value = p
                                   lbl_Progress.Text = caption
                               End Sub, DispatcherPriority.Background)
            End If
        End Sub

        Try
            'Dim endpoint As String = ftpHost.TrimEnd("/"c) & "/download.php" 'Ini tidak dihapus. Untuk dipelajari

            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromMinutes(15)
                If Not String.IsNullOrEmpty(APIKey_PHPCode) Then
                    client.DefaultRequestHeaders.Add("X-Api-Key", APIKey_PHPCode) ' API key
                End If

                ' Kirim permintaan download (sourcePath bisa URL atau relatif)
                Dim form As New MultipartFormDataContent()
                form.Add(New StringContent(sourcePath), "file")

                Using resp As HttpResponseMessage = Await client.PostAsync(url_FileDownloader_PHP, form)
                    If Not resp.IsSuccessStatusCode Then
                        Dim err = Await resp.Content.ReadAsStringAsync()
                        Throw New Exception($"HTTP {(CInt(resp.StatusCode))} {resp.ReasonPhrase}: {err}")
                    End If

                    Dim total As Long = If(resp.Content.Headers.ContentLength.HasValue,
                                       resp.Content.Headers.ContentLength.Value, 0)

                    Using inStream As Stream = Await resp.Content.ReadAsStreamAsync(),
                      outFs As New FileStream(localDestPath, FileMode.Create, FileAccess.Write, FileShare.None)

                        Dim buf(64 * 1024 - 1) As Byte
                        Dim read As Integer
                        Dim done As Long = 0

                        Do
                            read = Await inStream.ReadAsync(buf, 0, buf.Length)
                            If read <= 0 Then Exit Do
                            Await outFs.WriteAsync(buf, 0, read)
                            done += read

                            Dim percent As Double = If(total > 0, (done * 100.0R / total), 0)
                            SafeUpdate(percent, If(total > 0,
                                               $"Download... {percent:F2}%",
                                               $"Download... {done \ 1024} KB"))
                        Loop
                    End Using
                End Using
            End Using

            Sukses = True
            SafeUpdate(100, "Download selesai!")

        Catch ex As Exception
            Sukses = False
            SafeUpdate(pgb_Progress.Value, $"Error: {ex.Message}")
        End Try

        'Jalur alternatif, melalui token :
        If Not Sukses Then Sukses = Await DownloadViaTokenFallbackAsync(sourcePath, localDestPath, urlFileDownloaderViaToken_PHP, pgb_Progress, lbl_Progress)

        Return Sukses
    End Function


    Public Async Function DownloadViaTokenFallbackAsync(urlFileAtServer As String,
                                                        localDestPath As String,
                                                        tokenEndpoint As String,
                                                        pgb_Progress As ProgressBar,
                                                        lbl_Progress As TextBlock) _
                                                        As Task(Of Boolean)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim ui = lbl_Progress.Dispatcher
        Dim SafeText As Action(Of String) =
        Sub(t)
            If ui.CheckAccess() Then : lbl_Progress.Text = t : Else : ui.BeginInvoke(Sub() lbl_Progress.Text = t) : End If
        End Sub
        Dim SafeProg As Action(Of Double) =
        Sub(v)
            Dim p = Math.Max(0, Math.Min(100, v))
            If ui.CheckAccess() Then : pgb_Progress.Value = p : Else : ui.BeginInvoke(Sub() pgb_Progress.Value = p) : End If
        End Sub

        Try
            ' ---------- 1) Minta TOKEN (HTTPS) ----------
            SafeText("Meminta token...")
            Dim h As New HttpClientHandler() With {
            .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
        }
            Using client As New HttpClient(h)
                client.Timeout = TimeSpan.FromMinutes(2)
                client.DefaultRequestHeaders.Remove("X-Api-Key")
                client.DefaultRequestHeaders.Add("X-Api-Key", APIKey_PHPCode)
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BookuUpdater/1.0")

                Dim form As New MultipartFormDataContent()
                form.Add(New StringContent(urlFileAtServer), "file")     ' <-- boleh URL penuh
                form.Add(New StringContent(APIKey_PHPCode), "api_key")            ' <-- backup kalau header di-strip

                Dim resp = Await client.PostAsync(tokenEndpoint, form)
                Dim body = Await resp.Content.ReadAsStringAsync()

                If Not resp.IsSuccessStatusCode Then
                    SafeText($"Token gagal: HTTP {(CInt(resp.StatusCode))} {resp.ReasonPhrase}{Environment.NewLine}{body}")
                    Return False
                End If

                ' ---------- 2) Parse JSON aman ----------
                Dim tokenUrl As String = Nothing
                Dim hashExpected As String = Nothing
                Try
                    Using doc = JsonDocument.Parse(body)
                        If doc.RootElement.TryGetProperty("url", Nothing) Then
                            tokenUrl = doc.RootElement.GetProperty("url").GetString()
                        End If
                        If doc.RootElement.TryGetProperty("sha256", Nothing) Then
                            hashExpected = doc.RootElement.GetProperty("sha256").GetString()
                        End If
                    End Using
                Catch jex As Exception
                    SafeText("Token bukan JSON valid: " & jex.Message & Environment.NewLine & body)
                    Return False
                End Try

                If String.IsNullOrWhiteSpace(tokenUrl) Then
                    SafeText("Token tidak berisi URL. Respons: " & body)
                    Return False
                End If

                ' ---------- 3) Download via HTTP (stream + progress) ----------
                'SafeText("Mengunduh melalui HTTP fallback...")
                SafeText("Mengunduh melalui jalur alternatif...")
                Dim dh As New HttpClientHandler() With {
                .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            }
                Using dlClient As New HttpClient(dh)
                    dlClient.Timeout = TimeSpan.FromMinutes(15)
                    dlClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BookuUpdater/1.0")

                    Using respDl = Await dlClient.GetAsync(tokenUrl, HttpCompletionOption.ResponseHeadersRead)
                        If Not respDl.IsSuccessStatusCode Then
                            SafeText($"Download gagal: HTTP {(CInt(respDl.StatusCode))} {respDl.ReasonPhrase}")
                            Return False
                        End If

                        Dim total As Long = If(respDl.Content.Headers.ContentLength, 0)
                        Using inS = Await respDl.Content.ReadAsStreamAsync(),
                          outFs As New FileStream(localDestPath, FileMode.Create, FileAccess.Write, FileShare.None, 64 * 1024, True)

                            Dim buf(64 * 1024 - 1) As Byte
                            Dim read As Integer
                            Dim done As Long = 0
                            Do
                                read = Await inS.ReadAsync(buf, 0, buf.Length)
                                If read <= 0 Then Exit Do
                                Await outFs.WriteAsync(buf, 0, read)
                                done += read
                                If total > 0 Then SafeProg(done * 100.0R / total)
                            Loop
                        End Using
                    End Using
                End Using

                ' ---------- 4) Verifikasi SHA-256 (jika diberikan) ----------
                If Not String.IsNullOrWhiteSpace(hashExpected) Then
                    SafeText("Verifikasi integritas...")
                    Using fs = File.OpenRead(localDestPath)
                        Using sha = SHA256.Create()
                            Dim hashBytes = sha.ComputeHash(fs)
                            Dim hashStr = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()
                            If Not String.Equals(hashStr, hashExpected.Trim().ToLowerInvariant(), StringComparison.Ordinal) Then
                                SafeText("File tidak valid (hash mismatch).")
                                Return False
                            End If
                        End Using
                    End Using
                End If
            End Using

            SafeProg(100) : SafeText("Selesai ✔")
            Return True

        Catch ex As Exception
            SafeText("Error: " & ex.Message)
            Return False
        End Try
    End Function




    Public KompressBerhasil As Boolean
    Public Async Function KompressFile(folderPath As String, zipFilePath As String, pgb_Progress As ProgressBar, lbl_Progress As TextBlock,
                                       TahapKe As Integer, AngkaFull As Decimal) As Task

        If Directory.Exists(folderPath) Then
            Await CompressFolderAsync(folderPath, zipFilePath, pgb_Progress, lbl_Progress, TahapKe, AngkaFull)
            KompressBerhasil = True
            'MessageBox.Show("Kompresi selesai!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information)
        Else
            KompressBerhasil = False
            'MessageBox.Show("Folder tidak ditemukan!", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If

    End Function


    Private Async Function CompressFolderAsync(folderPath As String, zipFilePath As String, pgb_Progress As ProgressBar, lbl_Progress As TextBlock,
                                               TahapKe As Integer, AngkaFull As Decimal) As Task
        ' Dapatkan semua file dan folder dalam direktori
        Dim allFiles As List(Of String) = GetAllFilesRecursively(folderPath)
        Dim totalFiles As Integer = allFiles.Count

        Dim dispat As New DispatcherFrame
        If totalFiles = 0 Then
            MessageBox.Show("Tidak ada file untuk dikompres.", "Informasi", MessageBoxButton.OK, MessageBoxImage.Information)
            Return
        End If

        Using zipStream As FileStream = New FileStream(zipFilePath, FileMode.Create)
            Using archive As ZipArchive = New ZipArchive(zipStream, ZipArchiveMode.Create)
                For i As Integer = 0 To totalFiles - 1
                    Dim filePath As String = allFiles(i)

                    ' Perbaikan: Gunakan Path.GetRelativePath untuk mencegah hilangnya huruf pertama
                    Dim relativePath As String = Path.GetRelativePath(folderPath, filePath)

                    ' Tambahkan file ke dalam arsip ZIP
                    Await Task.Run(Sub() archive.CreateEntryFromFile(filePath, relativePath))

                    ' Perbarui progress
                    Dim progress As Integer = ((TahapKe - 1) * AngkaFull) + CInt(((i + 1) / totalFiles) * AngkaFull)
                    dispat.Dispatcher.Invoke(Sub()
                                                 pgb_Progress.Value = progress
                                                 lbl_Progress.Text = $"Progress: {progress}%"
                                             End Sub)
                Next
            End Using
        End Using

    End Function

    ' Mendapatkan semua file dalam folder dan sub-folder
    Private Function GetAllFilesRecursively(rootFolder As String) As List(Of String)

        Dim files As List(Of String) = Directory.GetFiles(rootFolder).ToList()

        For Each subFolder As String In Directory.GetDirectories(rootFolder)
            files.AddRange(GetAllFilesRecursively(subFolder))
        Next

        Return files

    End Function


    Public EkstrakBerhasil As Boolean
    Public Async Function EkstrakFile(zipPath As String, extractPath As String, pgb_Progress As ProgressBar, lbl_Progress As TextBlock) As Task

        If Not File.Exists(zipPath) Then
            MsgBox("File ZIP tidak ditemukan!")
            Return
        End If

        pgb_Progress.Value = 0

        Try
            Await ExtractZipWithProgressAsync(zipPath, extractPath, pgb_Progress, lbl_Progress)
            EkstrakBerhasil = True
        Catch ex As Exception
            EkstrakBerhasil = False
        Finally
            'Final
        End Try
    End Function
    Public Async Function ExtractZipWithProgressAsync(zipPath As String, extractPath As String, pgb_Progress As ProgressBar, lbl_Progress As TextBlock) As Task
        Dim dispat As New DispatcherFrame
        Await Task.Run(Sub()
                           Using zip As ZipArchive = ZipFile.OpenRead(zipPath)
                               Dim totalEntries As Integer = zip.Entries.Count
                               Dim currentEntry As Integer = 0

                               For Each entry As ZipArchiveEntry In zip.Entries
                                   Dim destinationPath As String = Path.Combine(extractPath, entry.FullName)
                                   If Not String.IsNullOrEmpty(entry.Name) Then
                                       Directory.CreateDirectory(Path.GetDirectoryName(destinationPath))
                                       entry.ExtractToFile(destinationPath, True)
                                   End If

                                   currentEntry += 1
                                   Dim percent As Integer = CInt((currentEntry / totalEntries) * 100)
                                   dispat.Dispatcher.Invoke(Sub()
                                                                pgb_Progress.Value = percent
                                                                lbl_Progress.Text = $"Ekstraksi... {percent } %"
                                                            End Sub)
                                   Terabas()
                               Next
                           End Using
                       End Sub)
    End Function


    Public SimpanDokumenBerhasil As Boolean
    Public Sub SimpanDokumen(filePath As String, Konten As String)
        Try
            File.WriteAllText(filePath, Konten)
            SimpanDokumenBerhasil = True
        Catch ex As Exception
            SimpanDokumenBerhasil = False
        End Try
    End Sub



    Public UploadBerhasil As Boolean
    Public Async Function UploadFileFTPAsync_MetodeFTP(filePath As String, ftpServer As String, ftpUsername As String, ftpPassword As String,
                                             pgb_Progress As ProgressBar, lbl_Progress As TextBlock, TahapKe As Integer, AngkaFull As Decimal) As Task
        UploadBerhasil = False
        Try
            Dim fileInfo As New FileInfo(filePath)
            Dim ftpUri As String = ftpServer & fileInfo.Name
            Dim request As FtpWebRequest = CType(WebRequest.Create(ftpUri), FtpWebRequest)

            request.Method = WebRequestMethods.Ftp.UploadFile
            request.Credentials = New NetworkCredential(ftpUsername, ftpPassword)
            request.UsePassive = True
            request.UseBinary = True
            request.KeepAlive = False

            ' Membaca file
            Using fileStream As FileStream = File.OpenRead(filePath)
                Using requestStream As Stream = Await request.GetRequestStreamAsync()
                    Dim buffer(8192 - 1) As Byte
                    Dim totalBytes As Long = fileStream.Length
                    Dim uploadedBytes As Long = 0
                    Dim bytesRead As Integer

                    Do
                        bytesRead = Await fileStream.ReadAsync(buffer, 0, buffer.Length)
                        If bytesRead = 0 Then Exit Do

                        Await requestStream.WriteAsync(buffer, 0, bytesRead)
                        uploadedBytes += bytesRead

                        ' Update ProgressBar
                        Dim progress As Double = ((TahapKe - 1) * AngkaFull) + (uploadedBytes / totalBytes) * AngkaFull
                        pgb_Progress.Value = progress
                        lbl_Progress.Text = $"{progress:F2}%"
                    Loop
                End Using
            End Using
            UploadBerhasil = True
            lbl_Progress.Text = "Upload berhasil!"
        Catch ex As Exception
            UploadBerhasil = False
            lbl_Progress.Text = $"Error: {ex.Message}"
        End Try
    End Function

    Public Async Sub PindahkanFileAntarFolderDiServer_MetodeHTTP(sourcePath As String,
                                                                 destPath As String,
                                                                 urlFileRenamer_PHP As String,
                                                                 Optional useSsl As Boolean = False)

        Try
            ' ftpHost: alamat dasar, mis. "https://booku.id/booku/"
            ' sourcePath: path relatif file asal, mis. "folderA/data.zip"
            ' destPath: path relatif tujuan, mis. "folderB/data.zip"
            ' username: API key
            ' password & useSsl: diabaikan (demi kompatibilitas param)

            'Dim endpoint As String = ftpHost.TrimEnd("/"c) & "/rename.php"

            Using client As New HttpClient()
                client.Timeout = TimeSpan.FromSeconds(30)
                client.DefaultRequestHeaders.Add("X-Api-Key", APIKey_PHPCode)

                Dim data As New Dictionary(Of String, String) From {
                {"source", sourcePath},
                {"destination", destPath}
            }

                Dim content As New FormUrlEncodedContent(data)
                Dim response As HttpResponseMessage = Await client.PostAsync(urlFileRenamer_PHP, content)

                Dim result As String = Await response.Content.ReadAsStringAsync()

                If Not response.IsSuccessStatusCode Then
                    Throw New Exception($"HTTP {(CInt(response.StatusCode))} {response.ReasonPhrase}: {result}")
                End If
            End Using


        Catch ex As Exception
            ' Abaikan atau log error jika gagal
        End Try
    End Sub


    Sub PindahkanFileAnterFolderDiServer_MetodeFTP(sourcePath As String, destPath As String, ftpHost As String, username As String, password As String,
                                         Optional useSsl As Boolean = False)
        Try
            Dim delReq As FtpWebRequest = DirectCast(WebRequest.Create(ftpHost & destPath), FtpWebRequest)
            delReq.Method = WebRequestMethods.Ftp.DeleteFile
            delReq.Credentials = New NetworkCredential(username, password)
            Using delResp = DirectCast(delReq.GetResponse(), FtpWebResponse)
            End Using
        Catch ex As WebException
            ' Abaikan error jika file tidak ada
        End Try

        ' Lanjut rename (pindahkan)
        Try
            Dim moveReq As FtpWebRequest = DirectCast(WebRequest.Create(ftpHost & sourcePath), FtpWebRequest)
            moveReq.Method = WebRequestMethods.Ftp.Rename
            moveReq.Credentials = New NetworkCredential(username, password)
            moveReq.RenameTo = destPath
            Using moveResp = DirectCast(moveReq.GetResponse(), FtpWebResponse)
            End Using
        Catch ex As Exception
            ' Abaikan error jika gagal.
        End Try
    End Sub


    Public BackUpDatabaseBerhasil As Boolean
    Async Function BackUpMySql_DenganProgress(databaseName As String, username As String, password As String, host As String, port As String, folderPath As String,
        pgb_Progress As ProgressBar, lbl_Progress As TextBlock) As Task

        Dim outputFilePath As String = Path.Combine(folderPath, databaseName)

        ' Reset ProgressBar sebelum memulai backup
        pgb_Progress.Value = 0
        lbl_Progress.Text = "Memulai backup database..."

        ' Lakukan backup secara async
        Await BackupDatabaseAsyncByProgress(databaseName, username, password, host, port, folderPath, pgb_Progress, lbl_Progress)

        pgb_Progress.Value = 100
        lbl_Progress.Text = "Backup selesai!"
        'MessageBox.Show("Backup selesai! File disimpan di: " & outputFilePath, "Sukses", MessageBoxButton.OK, MessageBoxImage.Information)

    End Function



    Public mysqldumpPath As String = "C:\xampp\mysql\bin\mysqldump.exe"
    Public Async Function BackupDatabaseAsyncByProgress(databaseName As String, user As String, pass As String, host As String, port As String, folderPath As String, pgb_Progress As ProgressBar, lbl_Progress As TextBlock) As Task

        Dim dispat As New DispatcherFrame

        Dim outputPath As String = Path.Combine(folderPath, databaseName & ".sql")

        Dim progress As Integer = 0

        Await Task.Run(Sub()
                           Try

                               BuatFolder(folderPath)

                               If Not File.Exists(mysqldumpPath) Then
                                   dispat.Dispatcher.Invoke(Sub()
                                                                MessageBox.Show("mysqldump tidak ditemukan! Periksa lokasi instalasi MySQL.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                                                            End Sub)
                                   Exit Sub
                               End If

                               ' Perintah untuk backup database
                               Dim command As String = $"--host={host} --port={port} --user={user} --password={pass} --databases {databaseName} --result-file=""{outputPath}"" --routines --events --triggers"

                               ' Inisialisasi Process
                               Dim process As New Process()
                               process.StartInfo.FileName = mysqldumpPath
                               process.StartInfo.Arguments = command
                               process.StartInfo.RedirectStandardError = True
                               process.StartInfo.RedirectStandardOutput = True
                               process.StartInfo.UseShellExecute = False
                               process.StartInfo.CreateNoWindow = True

                               ' Jalankan mysqldump
                               process.Start()

                               ' Baca output secara async untuk update progress
                               While Not process.StandardOutput.EndOfStream
                                   Dim line As String = process.StandardOutput.ReadLine()
                                   progress += 10
                                   If progress > 100 Then progress = 100
                                   dispat.Dispatcher.Invoke(Sub()
                                                                pgb_Progress.Value = progress
                                                                lbl_Progress.Text = "Backup berjalan... " & progress & "%"
                                                            End Sub)
                               End While

                               ' Tunggu hingga proses selesai
                               process.WaitForExit()
                               BackUpDatabaseBerhasil = True

                               ' Cek error
                               Dim errorMsg As String = process.StandardError.ReadToEnd()
                               If Not String.IsNullOrEmpty(errorMsg) Then
                                   BackUpDatabaseBerhasil = False
                                   dispat.Dispatcher.Invoke(Sub()
                                                                MessageBox.Show("Terjadi kesalahan saat backup: " & errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                                                            End Sub)
                               End If

                           Catch ex As Exception
                               BackUpDatabaseBerhasil = False
                               dispat.Dispatcher.Invoke(Sub()
                                                            MessageBox.Show("Error: " & ex.Message, "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error)
                                                        End Sub)
                           End Try
                       End Sub)
    End Function


    Async Function BackUpMySql_TanpaProgress(databaseName As String, username As String, password As String, host As String, port As String, folderPath As String) As Task

        Dim outputFilePath As String = Path.Combine(folderPath, databaseName)

        ' Lakukan backup secara async
        Await BackupDatabaseAsync(databaseName, username, password, host, port, folderPath)

        'MessageBox.Show("Backup selesai! File disimpan di: " & outputFilePath, "Sukses", MessageBoxButton.OK, MessageBoxImage.Information)

    End Function



    Public Async Function BackupDatabaseAsync(databaseName As String, user As String, pass As String, host As String, port As String, folderPath As String) As Task

        Dim outputPath As String = Path.Combine(folderPath, databaseName & ".sql")

        Dim progress As Integer = 0

        Await Task.Run(Sub()
                           Try

                               BuatFolder(folderPath)

                               If Not File.Exists(mysqldumpPath) Then
                                   MessageBox.Show("mysqldump tidak ditemukan! Periksa lokasi instalasi MySQL.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                                   Exit Sub
                               End If

                               ' Perintah untuk backup database
                               Dim command As String = $"--host={host} --port={port} --user={user} --password={pass} --databases {databaseName} --result-file=""{outputPath}"" --routines --events --triggers"

                               ' Inisialisasi Process
                               Dim process As New Process()
                               process.StartInfo.FileName = mysqldumpPath
                               process.StartInfo.Arguments = command
                               process.StartInfo.RedirectStandardError = True
                               process.StartInfo.RedirectStandardOutput = True
                               process.StartInfo.UseShellExecute = False
                               process.StartInfo.CreateNoWindow = True

                               ' Jalankan mysqldump
                               process.Start()

                               ' Baca output secara async untuk update progress
                               While Not process.StandardOutput.EndOfStream
                                   Dim line As String = process.StandardOutput.ReadLine()
                                   progress += 10
                               End While

                               ' Tunggu hingga proses selesai
                               process.WaitForExit()
                               BackUpDatabaseBerhasil = True

                               ' Cek error
                               Dim errorMsg As String = process.StandardError.ReadToEnd()
                               If Not String.IsNullOrEmpty(errorMsg) Then
                                   BackUpDatabaseBerhasil = False
                                   MessageBox.Show("Terjadi kesalahan saat backup: " & errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                               End If

                           Catch ex As Exception
                               BackUpDatabaseBerhasil = False
                               MessageBox.Show("Error: " & ex.Message, "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error)
                           End Try
                       End Sub)
    End Function

    Public Async Function UploadFileFTPAsync_MetodeHTTP(filePath As String,
                                                        urlFolderTarget As String,
                                                        url_FileUploader_PHP As String,
                                                        pgb_Progress As ProgressBar,
                                                        lbl_Progress As TextBlock,
                                                        TahapKe As Integer,
                                                        AngkaFull As Decimal) As Task

        UploadBerhasil = False

        Try
            Dim fi As New FileInfo(filePath)
            If Not fi.Exists Then Throw New FileNotFoundException("File tidak ditemukan.", filePath)

            ' --- helper: update UI safely via Dispatcher ---
            Dim ui = pgb_Progress.Dispatcher
            Dim SafeUpdate As Action(Of Double) =
            Sub(p)
                If ui.CheckAccess() Then
                    pgb_Progress.Value = Math.Max(0, Math.Min(100, p))
                    lbl_Progress.Text = $"{pgb_Progress.Value:F2}%"
                Else
                    ui.BeginInvoke(Sub()
                                       pgb_Progress.Value = Math.Max(0, Math.Min(100, p))
                                       lbl_Progress.Text = $"{pgb_Progress.Value:F2}%"
                                   End Sub, DispatcherPriority.Background)
                End If
            End Sub

            'Dim handler As New HttpClientHandler() With {
            '.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            '}

            'Using client As New HttpClient(handler)
            '    client.Timeout = TimeSpan.FromMinutes(69)
            '    If Not String.IsNullOrEmpty(APIKey_PHPCode) Then
            '        client.DefaultRequestHeaders.Add("X-Api-Key", APIKey_PHPCode) ' API key
            '    End If

            '    Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
            '        Dim content As HttpContent =
            '        New ProgressStreamContent(fs,
            '            Sub(sent, total)
            '                Dim frac As Double = If(total > 0, sent / CDbl(total), 0.0R)
            '                Dim progress As Double = ((TahapKe - 1) * CDbl(AngkaFull)) + (frac * CDbl(AngkaFull))
            '                SafeUpdate(progress)
            '            End Sub)

            '        content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")
            '        content.Headers.ContentDisposition = New ContentDispositionHeaderValue("form-data") With {
            '        .Name = """file""",
            '        .FileName = """" & fi.Name & """"
            '        }

            '        Using form As New MultipartFormDataContent()
            '            form.Add(content, "file", fi.Name)
            '            form.Add(New StringContent(Environment.MachineName), "machine")
            '            form.Add(New StringContent(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")), "ts")
            '            form.Add(New StringContent(urlFolderTarget), "target_url")

            '            Dim resp As HttpResponseMessage = Await client.PostAsync(url_FileUploader_PHP, form)
            '            Dim body As String = Await resp.Content.ReadAsStringAsync()
            '            If Not resp.IsSuccessStatusCode Then
            '                Throw New Exception($"HTTP {(CInt(resp.StatusCode))} {resp.ReasonPhrase}: {body}")
            '            End If
            '        End Using
            '    End Using
            'End Using

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim handler As New HttpClientHandler() With {
    .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
}

            Using client As New HttpClient(handler)
                ' ——— koneksi/HTTP behaviour ———
                client.Timeout = TimeSpan.FromMinutes(60)
                client.DefaultRequestHeaders.ExpectContinue = False
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BookuUploader/1.0")

                If Not String.IsNullOrEmpty(APIKey_PHPCode) Then
                    client.DefaultRequestHeaders.Remove("X-Api-Key")
                    client.DefaultRequestHeaders.Add("X-Api-Key", APIKey_PHPCode)  ' API key (header)
                End If

                ' ——— file stream async + buffer besar ———
                'Dim fi As New FileInfo(filePath)
                Using fs As New FileStream(
        path:=filePath,
        mode:=FileMode.Open,
        access:=FileAccess.Read,
        share:=FileShare.Read,
        bufferSize:=64 * 1024,
        options:=FileOptions.Asynchronous Or FileOptions.SequentialScan)

                    ' konten yang dilapisi progress
                    Dim content As HttpContent =
            New ProgressStreamContent(
                fs,
                Sub(sent As Long, total As Long)
                    Dim frac As Double = If(total > 0, sent / CDbl(total), 0.0R)
                    Dim progress As Double = ((TahapKe - 1) * CDbl(AngkaFull)) +
                                             (frac * CDbl(AngkaFull))
                    SafeUpdate(progress)    ' panggil delegatmu untuk pgb + lbl
                End Sub)

                    ' header konten file
                    content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")
                    content.Headers.ContentDisposition = New ContentDispositionHeaderValue("form-data") With {
            .Name = """file""",
            .FileName = """" & fi.Name & """"
        }

                    ' ——— multipart form ———
                    Using form As New MultipartFormDataContent()
                        form.Add(content) ' <input type="file" name="file">
                        form.Add(New StringContent(Environment.MachineName), "machine")
                        form.Add(New StringContent(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")), "ts")

                        ' target directory/url (server akan memutuskan cara mapping)
                        form.Add(New StringContent(urlFolderTarget), "target_url")

                        ' backup API key via body (jika header di-strip oleh jaringan)
                        If Not String.IsNullOrEmpty(APIKey_PHPCode) Then
                            form.Add(New StringContent(APIKey_PHPCode), "api_key")
                        End If

                        ' ——— kirim ———
                        Dim resp As HttpResponseMessage = Nothing
                        Dim body As String = ""
                        Try
                            resp = Await client.PostAsync(url_FileUploader_PHP, form)
                            body = Await resp.Content.ReadAsStringAsync().ConfigureAwait(False)

                            If Not resp.IsSuccessStatusCode Then
                                Throw New Exception($"HTTP {(CInt(resp.StatusCode))} {resp.ReasonPhrase}: {body}")
                            End If
                        Catch ex As TaskCanceledException
                            Throw New Exception("Upload timeout. Perbesar client.Timeout / max_execution_time server.")
                        Catch ex As HttpRequestException
                            Throw New Exception("Koneksi putus saat upload: " & If(ex.InnerException?.Message, ex.Message))
                        End Try
                    End Using
                End Using
            End Using



            UploadBerhasil = True
            Await ui.BeginInvoke(Sub() lbl_Progress.Text = "Upload berhasil!")

        Catch ex As Exception
            UploadBerhasil = False
            ' aman-kan update label dari worker thread
            Dim ui = lbl_Progress.Dispatcher
            ui.Invoke(Sub() lbl_Progress.Text = $"Error: {ex.Message}")
        End Try
    End Function




    '' --- helper content dengan progress ---
    'Friend Class ProgressStreamContent
    '    Inherits HttpContent

    '    Private ReadOnly _stream As Stream
    '    Private ReadOnly _bufSize As Integer = 64 * 1024
    '    Private ReadOnly _onProgress As Action(Of Long, Long)

    '    Public Sub New(inner As Stream, onProgress As Action(Of Long, Long))
    '        _stream = inner
    '        _onProgress = onProgress
    '    End Sub

    '    Protected Overrides Function TryComputeLength(ByRef length As Long) As Boolean
    '        If _stream.CanSeek Then
    '            length = _stream.Length : Return True
    '        End If
    '        length = -1 : Return False
    '    End Function

    '    Protected Overrides Function SerializeToStreamAsync(dst As Stream, ctx As TransportContext) As Task
    '        Return Task.Run(Sub()
    '                            Dim buf(_bufSize - 1) As Byte
    '                            Dim total As Long = If(_stream.CanSeek, _stream.Length, -1)
    '                            Dim sent As Long = 0
    '                            While True
    '                                Dim read = _stream.Read(buf, 0, buf.Length)
    '                                If read <= 0 Then Exit While
    '                                dst.Write(buf, 0, read)
    '                                sent += read
    '                                _onProgress?.Invoke(sent, If(total < 0, sent, total))
    '                            End While
    '                            dst.Flush()
    '                        End Sub)
    '    End Function
    'End Class


    Friend Class ProgressStreamContent
        Inherits HttpContent

        Private ReadOnly _stream As Stream
        Private ReadOnly _bufferSize As Integer
        Private ReadOnly _onProgress As Action(Of Long, Long)

        Public Sub New(inner As Stream,
                   onProgress As Action(Of Long, Long),
                   Optional bufferSize As Integer = 64 * 1024)
            _stream = inner
            _onProgress = onProgress
            _bufferSize = bufferSize
        End Sub

        Protected Overrides Function TryComputeLength(ByRef length As Long) As Boolean
            If _stream.CanSeek Then
                length = _stream.Length
                Return True
            End If
            length = -1
            Return False
        End Function

        ' <-- INI yang penting: Async Function, bukan Task.Run(Sub()...)
        Protected Overrides Async Function SerializeToStreamAsync(dst As Stream,
                                                             ctx As TransportContext) As Task
            Dim buf = New Byte(_bufferSize - 1) {}
            Dim total As Long = If(_stream.CanSeek, _stream.Length, -1L)
            Dim sent As Long = 0

            While True
                Dim read = Await _stream.ReadAsync(buf, 0, buf.Length).ConfigureAwait(False)
                If read <= 0 Then Exit While

                Await dst.WriteAsync(buf, 0, read).ConfigureAwait(False)
                sent += read
                _onProgress?.Invoke(sent, total)

                Await Task.Yield() ' beri kesempatan scheduler (sedikit membantu WAF yang sensitif)
            End While

            Await dst.FlushAsync().ConfigureAwait(False)
        End Function

        ' Jika compiler minta signature dengan CancellationToken (di .NET 5+),
        ' tambahkan wrapper ini:
        'Protected Overrides Function SerializeToStreamAsync(dst As Stream,
        '                                                   ctx As TransportContext,
        '                                                   cancellationToken As Threading.CancellationToken) As Task
        '    Return SerializeToStreamAsync(dst, ctx)
        'End Function
    End Class



    Public Async Function UploadFileAsync_MetodeChunked(
        filePath As String,
        serverTarget As String,
        urlUploadChunk As String,
        urlMerge As String,
        pgb_Progress As ProgressBar,
        lbl_Progress As TextBlock,
        Optional chunkSize As Integer = 8 * 1024 * 1024,     ' 8 MB
        Optional maxRetry As Integer = 3
        ) As Task(Of Boolean)

        Dim PersenProgress As Integer

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim fi = New FileInfo(filePath)
        If Not fi.Exists Then Throw New FileNotFoundException(filePath)

        Dim fileId As String = Guid.NewGuid().ToString("N")
        Dim total As Integer = CInt(Math.Ceiling(fi.Length / chunkSize))

        Dim ui = lbl_Progress.Dispatcher

        ' Untuk update teks label dari thread mana pun
        Dim SetText As Action(Of String)
        SetText = Sub(t)
                      If ui.CheckAccess() Then
                          lbl_Progress.Text = t
                      Else
                          ui.BeginInvoke(Sub() lbl_Progress.Text = t)
                      End If
                  End Sub

        ' Untuk update progress bar dari thread mana pun
        Dim SetProg As Action(Of Double)
        SetProg = Sub(v)
                      PersenProgress = v
                      If ui.CheckAccess() Then
                          pgb_Progress.Value = v
                          lbl_Progress.Text = PersenProgress.ToString & " %"
                      Else
                          ui.BeginInvoke(Sub() pgb_Progress.Value = v)
                          ui.BeginInvoke(Sub() lbl_Progress.Text = PersenProgress.ToString & " %")
                      End If
                  End Sub

        SetText($"Mengunggah {fi.Name} ({fi.Length \ (1024 * 1024)} MB) dalam {total} chunk...")
        SetProg(0)

        Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 64 * 1024, FileOptions.Asynchronous Or FileOptions.SequentialScan)
            For idx = 0 To total - 1
                Dim remaining = CInt(Math.Min(chunkSize, fi.Length - fs.Position))
                Dim buf(remaining - 1) As Byte
                Dim read = Await fs.ReadAsync(buf, 0, remaining)
                If read <= 0 Then Exit For

                Dim attempt = 0
                Dim sent = False
                Do
                    attempt += 1
                    Try
                        Using handler As New HttpClientHandler() With {.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate}
                            Using client As New HttpClient(handler)
                                client.Timeout = TimeSpan.FromMinutes(10)
                                client.DefaultRequestHeaders.ExpectContinue = False
                                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BookuUploader/1.0")
                                client.DefaultRequestHeaders.Add("X-Api-Key", APIKey_PHPCode)

                                Dim form As New MultipartFormDataContent()
                                form.Add(New StringContent(APIKey_PHPCode), "api_key")
                                form.Add(New StringContent(fileId), "fileId")
                                form.Add(New StringContent(idx.ToString()), "index")
                                form.Add(New StringContent(total.ToString()), "total")

                                Dim part As New ByteArrayContent(buf)
                                part.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")
                                form.Add(part, "chunk", $"part{idx}")

                                Dim resp = Await client.PostAsync(urlUploadChunk, form)
                                Dim body = Await resp.Content.ReadAsStringAsync()
                                If Not resp.IsSuccessStatusCode Then Throw New Exception($"HTTP {(CInt(resp.StatusCode))} {resp.ReasonPhrase}: {body}")
                            End Using
                        End Using
                        sent = True
                    Catch ex As Exception
                        If attempt >= maxRetry Then
                            SetText($"Gagal di chunk {idx}/{total - 1}: {ex.Message}")
                            Return False
                        End If
                        System.Threading.Thread.Sleep(1000 * attempt) ' backoff
                    End Try
                Loop Until sent

                SetProg((idx + 1) * 100.0R / total)
            Next
        End Using

        ' Minta merge
        SetText("Menggabungkan potongan di server...")
        Using client As New HttpClient()
            client.Timeout = TimeSpan.FromMinutes(10)
            client.DefaultRequestHeaders.ExpectContinue = False
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BookuUploader/1.0")
            client.DefaultRequestHeaders.Add("X-Api-Key", APIKey_PHPCode)

            Dim form = New MultipartFormDataContent()
            form.Add(New StringContent(APIKey_PHPCode), "api_key")
            form.Add(New StringContent(fileId), "fileId")
            form.Add(New StringContent(total.ToString()), "total")
            form.Add(New StringContent(serverTarget), "target")

            Dim resp = Await client.PostAsync(urlMerge, form)
            Dim json = Await resp.Content.ReadAsStringAsync()
            If Not resp.IsSuccessStatusCode Then
                Throw New Exception($"Merge gagal: HTTP {(CInt(resp.StatusCode))} {resp.ReasonPhrase}: {json}")
            End If
        End Using

        SetProg(100)
        SetText("Upload selesai ✔")
        Return True
    End Function



    Sub Jeda(MiliDetik As Integer)
        Thread.Sleep(MiliDetik)
    End Sub


    Sub JalankanAplikasi(filePathExe)
        Dim po As New Process
        po.StartInfo.FileName = filePathExe
        po.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        Try
            po.Start()
        Catch ex As Exception
        End Try
    End Sub



    Sub LogikaTahapandanProses(ByVal Tahapan As Boolean, ByRef Proses As Boolean)
        If Tahapan Then
            Proses = True
        Else
            Proses = False
        End If
    End Sub



End Module
