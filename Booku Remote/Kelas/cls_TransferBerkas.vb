Option Explicit On
Option Strict On

Imports System.IO
Imports System.Security.Cryptography

''' <summary>
''' Class untuk mengelola state satu sesi transfer berkas.
''' Digunakan baik oleh pengirim maupun penerima.
''' </summary>
Public Class cls_TransferBerkas

#Region "Constants"

    ''' <summary>Ukuran chunk default: 32 KB</summary>
    Public Const UKURAN_CHUNK_DEFAULT As Integer = 32768

    ''' <summary>Ukuran chunk untuk relay: 16 KB (lebih kecil untuk buffer)</summary>
    Public Const UKURAN_CHUNK_RELAY As Integer = 16384

    ''' <summary>Ekstensi file partial (sementara)</summary>
    Public Const EKSTENSI_PARTIAL As String = ".part"

    ''' <summary>Ekstensi file progress (untuk resume)</summary>
    Public Const EKSTENSI_PROGRESS As String = ".progress"

#End Region

#Region "Properties"

    ''' <summary>ID unik transfer (GUID)</summary>
    Public Property TransferId As String = ""

    ''' <summary>Arah transfer</summary>
    Public Property Arah As ArahTransfer = ArahTransfer.DOWNLOAD

    ''' <summary>Status transfer saat ini</summary>
    Public Property Status As StatusTransfer = StatusTransfer.IDLE

    ''' <summary>Nama file (tanpa path)</summary>
    Public Property NamaFile As String = ""

    ''' <summary>Path file sumber (untuk upload/send)</summary>
    Public Property PathSumber As String = ""

    ''' <summary>Path file tujuan (untuk download/receive)</summary>
    Public Property PathTujuan As String = ""

    ''' <summary>Ukuran file total dalam bytes</summary>
    Public Property UkuranFile As Long = 0

    ''' <summary>MD5 hash file untuk verifikasi</summary>
    Public Property HashFile As String = ""

    ''' <summary>Total jumlah chunk</summary>
    Public Property TotalChunk As Integer = 0

    ''' <summary>Ukuran per chunk dalam bytes</summary>
    Public Property UkuranChunk As Integer = UKURAN_CHUNK_DEFAULT

    ''' <summary>Index chunk terakhir yang berhasil ditransfer</summary>
    Public Property ChunkTerakhir As Integer = -1

    ''' <summary>Daftar chunk yang sudah diterima (untuk receiver)</summary>
    Public Property ChunkDiterima As HashSet(Of Integer) = New HashSet(Of Integer)()

    ''' <summary>Bytes yang sudah ditransfer</summary>
    Public Property BytesTerkirim As Long = 0

    ''' <summary>Waktu mulai transfer</summary>
    Public Property WaktuMulai As DateTime = DateTime.MinValue

    ''' <summary>Waktu selesai transfer</summary>
    Public Property WaktuSelesai As DateTime = DateTime.MinValue

    ''' <summary>Pesan error jika gagal</summary>
    Public Property PesanError As String = ""

    ''' <summary>Flag apakah ini adalah pengirim (True) atau penerima (False)</summary>
    Public Property IsPengirim As Boolean = False

#End Region

#Region "Computed Properties"

    ''' <summary>Persentase progress (0-100)</summary>
    Public ReadOnly Property Persentase As Double
        Get
            If TotalChunk <= 0 Then Return 0
            Return (ChunkDiterima.Count / TotalChunk) * 100.0
        End Get
    End Property

    ''' <summary>Kecepatan transfer dalam bytes per detik</summary>
    Public ReadOnly Property Kecepatan As Double
        Get
            If WaktuMulai = DateTime.MinValue Then Return 0
            Dim durasi = (DateTime.Now - WaktuMulai).TotalSeconds
            If durasi <= 0 Then Return 0
            Return BytesTerkirim / durasi
        End Get
    End Property

    ''' <summary>Estimasi waktu tersisa dalam detik</summary>
    Public ReadOnly Property EstimasiWaktuTersisa As Double
        Get
            If Kecepatan <= 0 Then Return 0
            Dim sisaBytes = UkuranFile - BytesTerkirim
            Return sisaBytes / Kecepatan
        End Get
    End Property

    ''' <summary>True jika transfer sudah selesai (sukses atau gagal)</summary>
    Public ReadOnly Property Selesai As Boolean
        Get
            Return Status = StatusTransfer.COMPLETED OrElse
                   Status = StatusTransfer.CANCELLED OrElse
                   Status = StatusTransfer.FAILED
        End Get
    End Property

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor default.
    ''' </summary>
    Public Sub New()
        TransferId = Guid.NewGuid().ToString("N")
    End Sub

    ''' <summary>
    ''' Constructor dengan parameter.
    ''' </summary>
    Public Sub New(arah As ArahTransfer, pathFile As String, Optional ukuranChunk As Integer = UKURAN_CHUNK_DEFAULT)
        Me.New()
        Me.Arah = arah
        Me.UkuranChunk = ukuranChunk

        If arah = ArahTransfer.UPLOAD Then
            ' Upload: file sumber ada di lokal
            PathSumber = pathFile
            If File.Exists(pathFile) Then
                Dim fi = New FileInfo(pathFile)
                NamaFile = fi.Name
                UkuranFile = fi.Length
                TotalChunk = CInt(Math.Ceiling(UkuranFile / UkuranChunk))
            End If
        Else
            ' Download: file tujuan di lokal
            PathTujuan = pathFile
            NamaFile = Path.GetFileName(pathFile)
        End If
    End Sub

#End Region

#Region "Hash Methods"

    ''' <summary>
    ''' Menghitung MD5 hash dari file.
    ''' </summary>
    Public Shared Function HitungHashFile(pathFile As String) As String
        Try
            If Not File.Exists(pathFile) Then Return ""

            Using md5 As MD5 = MD5.Create()
                Using stream As FileStream = File.OpenRead(pathFile)
                    Dim hash = md5.ComputeHash(stream)
                    Return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant()
                End Using
            End Using
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Menghitung MD5 hash dari data bytes.
    ''' </summary>
    Public Shared Function HitungHashData(data As Byte()) As String
        Try
            Using md5 As MD5 = MD5.Create()
                Dim hash = md5.ComputeHash(data)
                Return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant()
            End Using
        Catch
            Return ""
        End Try
    End Function

#End Region

#Region "Chunk Methods"

    ''' <summary>
    ''' Membaca satu chunk dari file sumber.
    ''' </summary>
    Public Function BacaChunk(chunkIndex As Integer) As Byte()
        Try
            If String.IsNullOrEmpty(PathSumber) OrElse Not File.Exists(PathSumber) Then
                Return Nothing
            End If

            Dim offset As Long = CLng(chunkIndex) * UkuranChunk
            If offset >= UkuranFile Then Return Nothing

            Dim ukuranBaca = CInt(Math.Min(UkuranChunk, UkuranFile - offset))
            Dim buffer(ukuranBaca - 1) As Byte

            Using fs As New FileStream(PathSumber, FileMode.Open, FileAccess.Read, FileShare.Read)
                fs.Seek(offset, SeekOrigin.Begin)
                fs.Read(buffer, 0, ukuranBaca)
            End Using

            Return buffer
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Menulis satu chunk ke file tujuan.
    ''' </summary>
    Public Function TulisChunk(chunkIndex As Integer, data As Byte()) As Boolean
        Try
            If String.IsNullOrEmpty(PathTujuan) OrElse data Is Nothing Then
                Return False
            End If

            ' Pastikan folder tujuan ada
            Dim folder = Path.GetDirectoryName(PathTujuan)
            If Not String.IsNullOrEmpty(folder) AndAlso Not Directory.Exists(folder) Then
                Directory.CreateDirectory(folder)
            End If

            ' Tulis ke file partial
            Dim pathPartial = PathTujuan & EKSTENSI_PARTIAL
            Dim offset As Long = CLng(chunkIndex) * UkuranChunk

            Using fs As New FileStream(pathPartial, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)
                fs.Seek(offset, SeekOrigin.Begin)
                fs.Write(data, 0, data.Length)
            End Using

            ' Tandai chunk sudah diterima
            ChunkDiterima.Add(chunkIndex)
            BytesTerkirim = CLng(ChunkDiterima.Count) * UkuranChunk
            If BytesTerkirim > UkuranFile Then BytesTerkirim = UkuranFile

            Return True
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Finalisasi transfer: rename file partial ke file final.
    ''' </summary>
    Public Function FinalisasiTransfer() As Boolean
        Try
            Dim pathPartial = PathTujuan & EKSTENSI_PARTIAL
            Dim pathProgress = PathTujuan & EKSTENSI_PROGRESS

            ' Hapus file lama jika ada
            If File.Exists(PathTujuan) Then
                File.Delete(PathTujuan)
            End If

            ' Rename partial ke final
            If File.Exists(pathPartial) Then
                File.Move(pathPartial, PathTujuan)
            End If

            ' Hapus file progress
            If File.Exists(pathProgress) Then
                File.Delete(pathProgress)
            End If

            WaktuSelesai = DateTime.Now
            Status = StatusTransfer.COMPLETED
            Return True
        Catch ex As Exception
            PesanError = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Membersihkan file temporary jika transfer dibatalkan.
    ''' </summary>
    Public Sub BersihkanFileTemporer()
        Try
            Dim pathPartial = PathTujuan & EKSTENSI_PARTIAL
            Dim pathProgress = PathTujuan & EKSTENSI_PROGRESS

            If File.Exists(pathPartial) Then
                File.Delete(pathPartial)
            End If

            If File.Exists(pathProgress) Then
                File.Delete(pathProgress)
            End If
        Catch
        End Try
    End Sub

#End Region

#Region "State Methods"

    ''' <summary>
    ''' Memulai transfer.
    ''' </summary>
    Public Sub MulaiTransfer()
        WaktuMulai = DateTime.Now
        Status = StatusTransfer.TRANSFERRING
        ChunkTerakhir = -1
        ChunkDiterima.Clear()
        BytesTerkirim = 0
    End Sub

    ''' <summary>
    ''' Membatalkan transfer.
    ''' </summary>
    Public Sub BatalkanTransfer(alasan As String)
        Status = StatusTransfer.CANCELLED
        PesanError = alasan
        WaktuSelesai = DateTime.Now

        If Not IsPengirim Then
            BersihkanFileTemporer()
        End If
    End Sub

    ''' <summary>
    ''' Menandai transfer gagal.
    ''' </summary>
    Public Sub GagalTransfer(pesan As String)
        Status = StatusTransfer.FAILED
        PesanError = pesan
        WaktuSelesai = DateTime.Now

        If Not IsPengirim Then
            BersihkanFileTemporer()
        End If
    End Sub

    ''' <summary>
    ''' Cek apakah semua chunk sudah diterima.
    ''' </summary>
    Public Function SemuaChunkDiterima() As Boolean
        Return ChunkDiterima.Count >= TotalChunk
    End Function

#End Region

End Class
