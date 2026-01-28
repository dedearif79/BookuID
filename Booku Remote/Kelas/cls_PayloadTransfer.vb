Option Explicit On
Option Strict On

''' <summary>
''' Kumpulan payload classes untuk fitur Transfer Berkas (Fase 3b).
''' </summary>

#Region "Permintaan Transfer"

''' <summary>
''' Payload untuk request transfer file (PERMINTAAN_BERKAS).
''' Dikirim oleh pengirim untuk meminta izin transfer.
''' </summary>
Public Class cls_PayloadPermintaanTransfer
    ''' <summary>ID unik untuk transfer ini (GUID)</summary>
    Public Property TransferId As String = ""

    ''' <summary>Arah transfer: "UPLOAD" (Tamu→Host) atau "DOWNLOAD" (Host→Tamu)</summary>
    Public Property Arah As String = ""

    ''' <summary>Nama file (tanpa path)</summary>
    Public Property NamaFile As String = ""

    ''' <summary>Ukuran file dalam bytes</summary>
    Public Property UkuranFile As Long = 0

    ''' <summary>MD5 hash file untuk verifikasi integritas</summary>
    Public Property HashFile As String = ""

    ''' <summary>Total jumlah chunk</summary>
    Public Property TotalChunk As Integer = 0

    ''' <summary>Ukuran per chunk dalam bytes (default 32KB)</summary>
    Public Property UkuranChunk As Integer = 32768

    ''' <summary>Path tujuan di penerima (untuk download, path di Host)</summary>
    Public Property PathSumber As String = ""
End Class

#End Region

#Region "Respon Transfer"

''' <summary>
''' Payload untuk response transfer (RESPON_TRANSFER).
''' Dikirim oleh penerima untuk accept/reject request.
''' </summary>
Public Class cls_PayloadResponTransfer
    ''' <summary>ID transfer yang direspon</summary>
    Public Property TransferId As String = ""

    ''' <summary>True jika request diterima</summary>
    Public Property Diterima As Boolean = False

    ''' <summary>Pesan (alasan penolakan jika ditolak)</summary>
    Public Property Pesan As String = ""

    ''' <summary>Mulai dari chunk index ini (untuk resume)</summary>
    Public Property MulaiDariChunk As Integer = 0
End Class

#End Region

#Region "Data Berkas"

''' <summary>
''' Payload untuk data chunk file (DATA_BERKAS).
''' Berisi satu chunk dari file yang ditransfer.
''' </summary>
Public Class cls_PayloadDataBerkas
    ''' <summary>ID transfer</summary>
    Public Property TransferId As String = ""

    ''' <summary>Index chunk (0-based)</summary>
    Public Property ChunkIndex As Integer = 0

    ''' <summary>Data chunk dalam Base64</summary>
    Public Property Data As String = ""

    ''' <summary>MD5 checksum per-chunk untuk verifikasi</summary>
    Public Property Checksum As String = ""
End Class

#End Region

#Region "Konfirmasi Chunk"

''' <summary>
''' Payload untuk ACK per chunk (KONFIRMASI_CHUNK).
''' Dikirim oleh penerima setelah menerima chunk.
''' </summary>
Public Class cls_PayloadKonfirmasiChunk
    ''' <summary>ID transfer</summary>
    Public Property TransferId As String = ""

    ''' <summary>Index chunk yang dikonfirmasi</summary>
    Public Property ChunkIndex As Integer = 0

    ''' <summary>True jika chunk diterima dengan benar</summary>
    Public Property Sukses As Boolean = False

    ''' <summary>True jika perlu kirim ulang chunk ini</summary>
    Public Property KirimUlang As Boolean = False
End Class

#End Region

#Region "Konfirmasi Berkas"

''' <summary>
''' Payload untuk konfirmasi transfer selesai (KONFIRMASI_BERKAS).
''' Dikirim setelah seluruh file berhasil ditransfer.
''' </summary>
Public Class cls_PayloadKonfirmasiBerkas
    ''' <summary>ID transfer</summary>
    Public Property TransferId As String = ""

    ''' <summary>True jika transfer sukses</summary>
    Public Property Sukses As Boolean = False

    ''' <summary>Pesan (error message jika gagal)</summary>
    Public Property Pesan As String = ""

    ''' <summary>Hash file hasil untuk verifikasi</summary>
    Public Property HashHasil As String = ""
End Class

#End Region

#Region "Batal Transfer"

''' <summary>
''' Payload untuk membatalkan transfer (BATAL_TRANSFER).
''' Bisa dikirim oleh pengirim atau penerima.
''' </summary>
Public Class cls_PayloadBatalTransfer
    ''' <summary>ID transfer yang dibatalkan</summary>
    Public Property TransferId As String = ""

    ''' <summary>Alasan pembatalan</summary>
    Public Property Alasan As String = ""
End Class

#End Region

#Region "Daftar Folder"

''' <summary>
''' Payload untuk request daftar folder (DAFTAR_FOLDER).
''' Dikirim oleh Tamu untuk browse folder di Host.
''' </summary>
Public Class cls_PayloadDaftarFolder
    ''' <summary>Path folder yang diminta (kosong = home folder)</summary>
    Public Property Path As String = ""
End Class

''' <summary>
''' Payload untuk response daftar folder (RESPON_DAFTAR_FOLDER).
''' Berisi daftar file dan folder di path tertentu.
''' </summary>
Public Class cls_PayloadResponDaftarFolder
    ''' <summary>Path folder yang di-list</summary>
    Public Property Path As String = ""

    ''' <summary>Daftar item (file dan folder)</summary>
    Public Property Items As List(Of cls_ItemFolder) = New List(Of cls_ItemFolder)()

    ''' <summary>True jika berhasil</summary>
    Public Property Sukses As Boolean = False

    ''' <summary>Pesan error jika gagal</summary>
    Public Property Pesan As String = ""

    ''' <summary>Path parent folder (untuk navigasi Up)</summary>
    Public Property ParentPath As String = ""
End Class

''' <summary>
''' Model untuk satu item dalam daftar folder (file atau subfolder).
''' </summary>
Public Class cls_ItemFolder
    ''' <summary>Nama file/folder</summary>
    Public Property Nama As String = ""

    ''' <summary>True jika ini adalah folder</summary>
    Public Property IsFolder As Boolean = False

    ''' <summary>Ukuran file dalam bytes (0 untuk folder)</summary>
    Public Property Ukuran As Long = 0

    ''' <summary>Tanggal modifikasi terakhir</summary>
    Public Property TanggalModifikasi As DateTime = DateTime.MinValue

    ''' <summary>Path lengkap</summary>
    Public Property PathLengkap As String = ""
End Class

#End Region
