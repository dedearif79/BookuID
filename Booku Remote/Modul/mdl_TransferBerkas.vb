Option Explicit On
Option Strict On

Imports System.Collections.Concurrent
Imports System.IO
Imports System.Threading

''' <summary>
''' Modul untuk mengelola transfer berkas antara Host dan Tamu.
''' Menangani pengiriman, penerimaan, chunking, dan folder browsing.
''' </summary>
Public Module mdl_TransferBerkas

#Region "Constants"

    ''' <summary>Delay antara pengiriman chunk (ms)</summary>
    Public Const DELAY_ANTAR_CHUNK As Integer = 10

    ''' <summary>Timeout menunggu konfirmasi chunk (ms)</summary>
    Public Const TIMEOUT_KONFIRMASI_CHUNK As Integer = 5000

    ''' <summary>Maksimum retry per chunk</summary>
    Public Const MAX_RETRY_CHUNK As Integer = 3

#End Region

#Region "Variables"

    ''' <summary>Dictionary transfer yang sedang berjalan (by TransferId)</summary>
    Public TransferAktif As New ConcurrentDictionary(Of String, cls_TransferBerkas)

    ''' <summary>CancellationTokenSource untuk membatalkan transfer</summary>
    Private _ctsTransfer As CancellationTokenSource

    ''' <summary>Flag apakah sedang ada transfer aktif</summary>
    Public Property SedangTransfer As Boolean = False

#End Region

#Region "Events"

    ''' <summary>Event ketika progress transfer berubah</summary>
    Public Event TransferProgressChanged(transferId As String, persentase As Double, kecepatan As Double, estimasiWaktu As Double)

    ''' <summary>Event ketika transfer selesai (sukses atau gagal)</summary>
    Public Event TransferCompleted(transferId As String, sukses As Boolean, pesan As String)

    ''' <summary>Event ketika ada permintaan transfer masuk (di Host)</summary>
    Public Event TransferRequestReceived(transfer As cls_TransferBerkas)

    ''' <summary>Event ketika chunk diterima (untuk progress di receiver)</summary>
    Public Event ChunkReceived(transferId As String, chunkIndex As Integer, totalChunk As Integer)

#End Region

#Region "Transfer Management"

    ''' <summary>
    ''' Memulai transfer baru (sebagai pengirim).
    ''' </summary>
    ''' <param name="pathFile">Path file yang akan dikirim</param>
    ''' <param name="arah">Arah transfer (UPLOAD atau DOWNLOAD)</param>
    ''' <param name="ukuranChunk">Ukuran chunk (default sesuai mode)</param>
    ''' <returns>Transfer state object</returns>
    Public Function MulaiTransferBaru(pathFile As String, arah As ArahTransfer,
                                       Optional ukuranChunk As Integer = 0) As cls_TransferBerkas
        Try
            ' Tentukan ukuran chunk berdasarkan mode koneksi
            If ukuranChunk = 0 Then
                ukuranChunk = If(ModeKoneksiSaatIni = ModeKoneksi.INTERNET,
                                 cls_TransferBerkas.UKURAN_CHUNK_RELAY,
                                 cls_TransferBerkas.UKURAN_CHUNK_DEFAULT)
            End If

            ' Buat state transfer baru
            Dim transfer As New cls_TransferBerkas(arah, pathFile, ukuranChunk)
            transfer.IsPengirim = True

            ' Hitung hash file
            transfer.HashFile = cls_TransferBerkas.HitungHashFile(pathFile)

            ' Tambahkan ke dictionary
            TransferAktif.TryAdd(transfer.TransferId, transfer)

            WriteLog($"[TRANSFER] Transfer baru dibuat: {transfer.TransferId}, File: {transfer.NamaFile}, Size: {transfer.UkuranFile}, Chunks: {transfer.TotalChunk}")

            Return transfer
        Catch ex As Exception
            WriteLog($"[TRANSFER] Error membuat transfer baru: {ex.Message}")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Menerima permintaan transfer (sebagai penerima).
    ''' </summary>
    ''' <param name="payload">Payload dari PERMINTAAN_BERKAS</param>
    ''' <param name="folderTujuan">Folder tujuan untuk menyimpan file</param>
    ''' <returns>Transfer state object</returns>
    Public Function TerimaPermintaanTransfer(payload As cls_PayloadPermintaanTransfer,
                                              folderTujuan As String) As cls_TransferBerkas
        Try
            Dim transfer As New cls_TransferBerkas()
            transfer.TransferId = payload.TransferId
            transfer.Arah = If(payload.Arah = "UPLOAD", ArahTransfer.UPLOAD, ArahTransfer.DOWNLOAD)
            transfer.NamaFile = payload.NamaFile
            transfer.UkuranFile = payload.UkuranFile
            transfer.HashFile = payload.HashFile
            transfer.TotalChunk = payload.TotalChunk
            transfer.UkuranChunk = payload.UkuranChunk
            transfer.IsPengirim = False

            ' Set path tujuan
            transfer.PathTujuan = Path.Combine(folderTujuan, payload.NamaFile)

            ' Cek apakah bisa resume (file partial sudah ada)
            Dim pathPartial = transfer.PathTujuan & cls_TransferBerkas.EKSTENSI_PARTIAL
            If File.Exists(pathPartial) Then
                ' TODO: Implementasi resume dari progress file
            End If

            ' Tambahkan ke dictionary
            TransferAktif.TryAdd(transfer.TransferId, transfer)

            WriteLog($"[TRANSFER] Permintaan transfer diterima: {transfer.TransferId}, File: {transfer.NamaFile}")

            ' Raise event untuk UI
            RaiseEvent TransferRequestReceived(transfer)

            Return transfer
        Catch ex As Exception
            WriteLog($"[TRANSFER] Error menerima permintaan transfer: {ex.Message}")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Mendapatkan transfer berdasarkan ID.
    ''' </summary>
    Public Function GetTransfer(transferId As String) As cls_TransferBerkas
        Dim transfer As cls_TransferBerkas = Nothing
        TransferAktif.TryGetValue(transferId, transfer)
        Return transfer
    End Function

    ''' <summary>
    ''' Menghapus transfer dari dictionary.
    ''' </summary>
    Public Sub HapusTransfer(transferId As String)
        Dim transfer As cls_TransferBerkas = Nothing
        TransferAktif.TryRemove(transferId, transfer)
    End Sub

    ''' <summary>
    ''' Membatalkan semua transfer aktif.
    ''' </summary>
    Public Sub BatalkanSemuaTransfer()
        _ctsTransfer?.Cancel()

        For Each kvp In TransferAktif
            kvp.Value.BatalkanTransfer("Koneksi terputus")
        Next

        TransferAktif.Clear()
        SedangTransfer = False
    End Sub

#End Region

#Region "Send Helper"

    ''' <summary>
    ''' Mengirim paket transfer dengan memilih metode berdasarkan mode koneksi.
    ''' Jika mode INTERNET, kirim via Relay. Jika LAN, kirim via TCP langsung.
    ''' </summary>
    Private Async Function KirimPaketTransferAsync(paket As cls_PaketData) As Task
        If ModeKoneksiSaatIni = ModeKoneksi.INTERNET Then
            ' PENTING: Set IdSesi untuk routing di relay
            paket.IdSesi = IdSesiRelay
            ' Kirim via Relay
            Await mdl_KoneksiRelay.KirimPaketKeRelayAsync(paket)
        Else
            ' Kirim via TCP langsung (LAN)
            Await mdl_KoneksiJaringan.KirimPaketAsync(paket)
        End If
    End Function

#End Region

#Region "Send Logic"

    ''' <summary>
    ''' Mengirim file secara async (dipanggil setelah dapat respon positif).
    ''' </summary>
    ''' <param name="transferId">ID transfer</param>
    ''' <param name="mulaiDariChunk">Index chunk untuk mulai (untuk resume)</param>
    Public Async Function KirimFileAsync(transferId As String,
                                          Optional mulaiDariChunk As Integer = 0) As Task(Of Boolean)
        Dim transfer = GetTransfer(transferId)
        If transfer Is Nothing Then
            WriteLog($"[TRANSFER] Transfer tidak ditemukan: {transferId}")
            Return False
        End If

        Try
            _ctsTransfer = New CancellationTokenSource()
            SedangTransfer = True
            transfer.MulaiTransfer()

            WriteLog($"[TRANSFER] Mulai kirim file: {transfer.NamaFile}, dari chunk {mulaiDariChunk}")

            For i = mulaiDariChunk To transfer.TotalChunk - 1
                If _ctsTransfer.Token.IsCancellationRequested Then
                    transfer.BatalkanTransfer("Dibatalkan oleh user")
                    Return False
                End If

                ' Baca chunk
                Dim chunkData = transfer.BacaChunk(i)
                If chunkData Is Nothing Then
                    transfer.GagalTransfer($"Gagal membaca chunk {i}")
                    Return False
                End If

                ' Buat dan kirim paket via metode yang sesuai (LAN atau Relay)
                Dim paket = BuatPaketDataBerkas(transferId, i, chunkData)
                Await KirimPaketTransferAsync(paket)

                ' Update progress
                transfer.ChunkTerakhir = i
                transfer.ChunkDiterima.Add(i)
                transfer.BytesTerkirim = CLng(transfer.ChunkDiterima.Count) * transfer.UkuranChunk
                If transfer.BytesTerkirim > transfer.UkuranFile Then
                    transfer.BytesTerkirim = transfer.UkuranFile
                End If

                ' Raise progress event
                RaiseEvent TransferProgressChanged(transferId, transfer.Persentase, transfer.Kecepatan, transfer.EstimasiWaktuTersisa)

                ' Delay kecil antar chunk
                Await Task.Delay(DELAY_ANTAR_CHUNK, _ctsTransfer.Token)
            Next

            WriteLog($"[TRANSFER] Selesai kirim semua chunk: {transfer.NamaFile}")

            ' Kirim konfirmasi berkas selesai via metode yang sesuai (LAN atau Relay)
            Dim paketKonfirmasi = BuatPaketKonfirmasiBerkas(transferId, True, transfer.HashFile)
            Await KirimPaketTransferAsync(paketKonfirmasi)

            transfer.WaktuSelesai = DateTime.Now
            transfer.Status = StatusTransfer.COMPLETED
            SedangTransfer = False

            RaiseEvent TransferCompleted(transferId, True, "Transfer selesai")
            Return True

        Catch ex As OperationCanceledException
            transfer.BatalkanTransfer("Dibatalkan")
            SedangTransfer = False
            Return False

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error kirim file: {ex.Message}")
            transfer.GagalTransfer(ex.Message)
            SedangTransfer = False
            RaiseEvent TransferCompleted(transferId, False, ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Receive Logic"

    ''' <summary>
    ''' Memproses chunk yang diterima.
    ''' </summary>
    ''' <param name="payload">Payload dari DATA_BERKAS</param>
    ''' <returns>True jika chunk valid dan berhasil ditulis</returns>
    Public Function ProsesChunkDiterima(payload As cls_PayloadDataBerkas) As Boolean
        Dim transfer = GetTransfer(payload.TransferId)
        If transfer Is Nothing Then
            WriteLog($"[TRANSFER] Transfer tidak ditemukan untuk chunk: {payload.TransferId}")
            Return False
        End If

        Try
            ' Decode data dari Base64
            Dim data = Convert.FromBase64String(payload.Data)

            ' Verifikasi checksum
            Dim checksumHitung = cls_TransferBerkas.HitungHashData(data)
            If checksumHitung <> payload.Checksum Then
                WriteLog($"[TRANSFER] Checksum mismatch pada chunk {payload.ChunkIndex}")
                Return False
            End If

            ' Tulis chunk ke file
            If Not transfer.TulisChunk(payload.ChunkIndex, data) Then
                WriteLog($"[TRANSFER] Gagal menulis chunk {payload.ChunkIndex}")
                Return False
            End If

            ' Raise event
            RaiseEvent ChunkReceived(payload.TransferId, payload.ChunkIndex, transfer.TotalChunk)
            RaiseEvent TransferProgressChanged(payload.TransferId, transfer.Persentase, transfer.Kecepatan, transfer.EstimasiWaktuTersisa)

            WriteLog($"[TRANSFER] Chunk {payload.ChunkIndex + 1}/{transfer.TotalChunk} diterima")

            Return True

        Catch ex As Exception
            WriteLog($"[TRANSFER] Error proses chunk: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Memproses konfirmasi transfer selesai.
    ''' </summary>
    ''' <param name="payload">Payload dari KONFIRMASI_BERKAS</param>
    Public Sub ProsesKonfirmasiBerkas(payload As cls_PayloadKonfirmasiBerkas)
        Dim transfer = GetTransfer(payload.TransferId)
        If transfer Is Nothing Then Return

        If payload.Sukses Then
            ' Verifikasi hash
            If transfer.SemuaChunkDiterima() Then
                ' Finalisasi file
                If transfer.FinalisasiTransfer() Then
                    ' Verifikasi hash file hasil
                    Dim hashHasil = cls_TransferBerkas.HitungHashFile(transfer.PathTujuan)
                    If hashHasil = payload.HashHasil OrElse hashHasil = transfer.HashFile Then
                        WriteLog($"[TRANSFER] Transfer sukses dan terverifikasi: {transfer.NamaFile}")
                        RaiseEvent TransferCompleted(payload.TransferId, True, "Transfer selesai dan terverifikasi")
                    Else
                        WriteLog($"[TRANSFER] Hash mismatch: expected {payload.HashHasil}, got {hashHasil}")
                        transfer.GagalTransfer("Verifikasi hash gagal")
                        RaiseEvent TransferCompleted(payload.TransferId, False, "Verifikasi hash gagal")
                    End If
                Else
                    RaiseEvent TransferCompleted(payload.TransferId, False, transfer.PesanError)
                End If
            Else
                WriteLog($"[TRANSFER] Ada chunk yang hilang")
                transfer.GagalTransfer("Ada chunk yang hilang")
                RaiseEvent TransferCompleted(payload.TransferId, False, "Ada chunk yang hilang")
            End If
        Else
            transfer.GagalTransfer(payload.Pesan)
            RaiseEvent TransferCompleted(payload.TransferId, False, payload.Pesan)
        End If

        SedangTransfer = False
    End Sub

    ''' <summary>
    ''' Memproses pembatalan transfer.
    ''' </summary>
    ''' <param name="payload">Payload dari BATAL_TRANSFER</param>
    Public Sub ProsesBatalTransfer(payload As cls_PayloadBatalTransfer)
        Dim transfer = GetTransfer(payload.TransferId)
        If transfer IsNot Nothing Then
            transfer.BatalkanTransfer(payload.Alasan)
            RaiseEvent TransferCompleted(payload.TransferId, False, $"Dibatalkan: {payload.Alasan}")
        End If

        _ctsTransfer?.Cancel()
        SedangTransfer = False
    End Sub

#End Region

#Region "Folder Browsing"

    ''' <summary>
    ''' Mendapatkan daftar file dan folder di path tertentu.
    ''' </summary>
    ''' <param name="path">Path folder (kosong = home folder)</param>
    ''' <returns>List of cls_ItemFolder</returns>
    Public Function DapatkanDaftarFolder(path As String) As cls_PayloadResponDaftarFolder
        Dim response As New cls_PayloadResponDaftarFolder()

        Try
            ' Jika path kosong, gunakan home folder
            If String.IsNullOrEmpty(path) Then
                path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            End If

            ' Pastikan folder ada
            If Not Directory.Exists(path) Then
                response.Sukses = False
                response.Pesan = "Folder tidak ditemukan"
                Return response
            End If

            response.Path = path
            response.Items = New List(Of cls_ItemFolder)()

            ' Dapatkan parent path
            Dim parentInfo = Directory.GetParent(path)
            If parentInfo IsNot Nothing Then
                response.ParentPath = parentInfo.FullName
            End If

            ' Tambahkan subfolder
            For Each dirPath In Directory.GetDirectories(path)
                Try
                    Dim dirInfo As New DirectoryInfo(dirPath)
                    ' Skip folder hidden dan system
                    If (dirInfo.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then Continue For
                    If (dirInfo.Attributes And FileAttributes.System) = FileAttributes.System Then Continue For

                    Dim item As New cls_ItemFolder With {
                        .Nama = dirInfo.Name,
                        .IsFolder = True,
                        .Ukuran = 0,
                        .TanggalModifikasi = dirInfo.LastWriteTime,
                        .PathLengkap = dirPath
                    }
                    response.Items.Add(item)
                Catch
                    ' Skip folder yang tidak bisa diakses
                End Try
            Next

            ' Tambahkan file
            For Each filePath In Directory.GetFiles(path)
                Try
                    Dim fileInfo As New FileInfo(filePath)
                    ' Skip file hidden dan system
                    If (fileInfo.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then Continue For
                    If (fileInfo.Attributes And FileAttributes.System) = FileAttributes.System Then Continue For

                    Dim item As New cls_ItemFolder With {
                        .Nama = fileInfo.Name,
                        .IsFolder = False,
                        .Ukuran = fileInfo.Length,
                        .TanggalModifikasi = fileInfo.LastWriteTime,
                        .PathLengkap = filePath
                    }
                    response.Items.Add(item)
                Catch
                    ' Skip file yang tidak bisa diakses
                End Try
            Next

            response.Sukses = True

        Catch ex As UnauthorizedAccessException
            response.Sukses = False
            response.Pesan = "Akses ditolak"

        Catch ex As Exception
            response.Sukses = False
            response.Pesan = ex.Message
        End Try

        Return response
    End Function

    ''' <summary>
    ''' Mendapatkan daftar drive yang tersedia.
    ''' </summary>
    Public Function DapatkanDaftarDrive() As cls_PayloadResponDaftarFolder
        Dim response As New cls_PayloadResponDaftarFolder With {
            .Path = "",
            .ParentPath = "",
            .Items = New List(Of cls_ItemFolder)()
        }

        Try
            For Each drive In DriveInfo.GetDrives()
                If drive.IsReady Then
                    Dim item As New cls_ItemFolder With {
                        .Nama = $"{drive.Name} ({drive.VolumeLabel})",
                        .IsFolder = True,
                        .Ukuran = drive.TotalSize,
                        .TanggalModifikasi = DateTime.Now,
                        .PathLengkap = drive.RootDirectory.FullName
                    }
                    response.Items.Add(item)
                End If
            Next

            response.Sukses = True

        Catch ex As Exception
            response.Sukses = False
            response.Pesan = ex.Message
        End Try

        Return response
    End Function

#End Region

#Region "Helper Methods"

    ''' <summary>
    ''' Memformat ukuran file ke string yang readable.
    ''' </summary>
    Public Function FormatUkuranFile(bytes As Long) As String
        Dim sizes = {"B", "KB", "MB", "GB", "TB"}
        Dim order = 0
        Dim size As Double = bytes

        While size >= 1024 AndAlso order < sizes.Length - 1
            order += 1
            size /= 1024
        End While

        Return $"{size:0.##} {sizes(order)}"
    End Function

    ''' <summary>
    ''' Memformat waktu ke string yang readable.
    ''' </summary>
    Public Function FormatWaktu(detik As Double) As String
        If detik < 60 Then
            Return $"{CInt(detik)} detik"
        ElseIf detik < 3600 Then
            Dim menit = CInt(detik / 60)
            Dim sisa = CInt(detik Mod 60)
            Return $"{menit}:{sisa:00} menit"
        Else
            Dim jam = CInt(detik / 3600)
            Dim menit = CInt((detik Mod 3600) / 60)
            Return $"{jam}:{menit:00} jam"
        End If
    End Function

#End Region

End Module
