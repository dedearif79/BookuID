Option Explicit On
Option Strict On

Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

''' <summary>
''' Kelas untuk model data frame layar yang dikirim dari Host ke Tamu.
''' Frame disimpan dalam format JPEG terkompresi sebagai Base64.
''' </summary>
Public Class cls_FrameLayar

#Region "Constants"

    ''' <summary>Kualitas JPEG default (0-100). Nilai lebih rendah = ukuran lebih kecil, kualitas lebih rendah.</summary>
    Public Const JPEG_QUALITY_DEFAULT As Integer = 30

#End Region

#Region "Properties"

    ''' <summary>Nomor urut frame (untuk tracking)</summary>
    Public Property NomorFrame As Long = 0

    ''' <summary>Lebar gambar (pixel)</summary>
    Public Property Lebar As Integer = 0

    ''' <summary>Tinggi gambar (pixel)</summary>
    Public Property Tinggi As Integer = 0

    ''' <summary>Data gambar PNG dalam format Base64</summary>
    Public Property DataGambarBase64 As String = ""

    ''' <summary>Timestamp saat frame ditangkap (UTC ticks)</summary>
    Public Property Timestamp As Long = DateTime.UtcNow.Ticks

    ''' <summary>Checksum MD5 untuk validasi integritas</summary>
    Public Property Checksum As String = ""

#End Region

#Region "Constructor"

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Konstruktor dengan data gambar (menggunakan kualitas JPEG default).
    ''' </summary>
    Public Sub New(nomorFrame As Long, bitmap As Bitmap)
        Me.New(nomorFrame, bitmap, JPEG_QUALITY_DEFAULT)
    End Sub

    ''' <summary>
    ''' Konstruktor dengan data gambar dan kualitas JPEG kustom.
    ''' </summary>
    Public Sub New(nomorFrame As Long, bitmap As Bitmap, jpegQuality As Integer)
        Me.NomorFrame = nomorFrame
        Me.Lebar = bitmap.Width
        Me.Tinggi = bitmap.Height
        Me.Timestamp = DateTime.UtcNow.Ticks
        Me.DataGambarBase64 = BitmapKeBase64JPEG(bitmap, jpegQuality)
        Me.Checksum = HitungChecksum()
    End Sub

#End Region

#Region "Conversion Methods"

    ''' <summary>
    ''' Konversi Bitmap ke string Base64 (format JPEG dengan kualitas tertentu).
    ''' JPEG jauh lebih kecil dari PNG - cocok untuk streaming.
    ''' </summary>
    Public Shared Function BitmapKeBase64JPEG(bitmap As Bitmap, quality As Integer) As String
        Try
            ' Clamp quality ke 0-100
            quality = Math.Max(0, Math.Min(100, quality))

            ' Setup JPEG encoder dengan quality parameter
            Dim jpegCodec = GetEncoderInfo("image/jpeg")
            If jpegCodec Is Nothing Then
                ' Fallback jika codec tidak ditemukan
                Using ms As New MemoryStream()
                    bitmap.Save(ms, ImageFormat.Jpeg)
                    Return Convert.ToBase64String(ms.ToArray())
                End Using
            End If

            Dim encoderParams As New EncoderParameters(1)
            encoderParams.Param(0) = New EncoderParameter(Encoder.Quality, CLng(quality))

            Using ms As New MemoryStream()
                bitmap.Save(ms, jpegCodec, encoderParams)
                Return Convert.ToBase64String(ms.ToArray())
            End Using
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Mendapatkan ImageCodecInfo untuk format tertentu.
    ''' </summary>
    Private Shared Function GetEncoderInfo(mimeType As String) As ImageCodecInfo
        Dim codecs = ImageCodecInfo.GetImageEncoders()
        For Each codec In codecs
            If codec.MimeType = mimeType Then
                Return codec
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Konversi Bitmap ke string Base64 (format PNG) - DEPRECATED, gunakan BitmapKeBase64JPEG.
    ''' </summary>
    Public Shared Function BitmapKeBase64(bitmap As Bitmap) As String
        Try
            Using ms As New MemoryStream()
                bitmap.Save(ms, ImageFormat.Png)
                Return Convert.ToBase64String(ms.ToArray())
            End Using
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Konversi string Base64 ke Bitmap.
    ''' </summary>
    Public Shared Function Base64KeBitmap(base64 As String) As Bitmap
        Try
            Dim bytes = Convert.FromBase64String(base64)
            Using ms As New MemoryStream(bytes)
                Return New Bitmap(ms)
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Mendapatkan data gambar sebagai Bitmap.
    ''' </summary>
    Public Function DapatkanBitmap() As Bitmap
        If String.IsNullOrEmpty(DataGambarBase64) Then Return Nothing
        Return Base64KeBitmap(DataGambarBase64)
    End Function

    ''' <summary>
    ''' Mendapatkan data gambar sebagai BitmapImage (untuk WPF).
    ''' </summary>
    Public Function DapatkanBitmapImage() As System.Windows.Media.Imaging.BitmapImage
        Try
            If String.IsNullOrEmpty(DataGambarBase64) Then Return Nothing

            Dim bytes = Convert.FromBase64String(DataGambarBase64)
            Dim bi As New System.Windows.Media.Imaging.BitmapImage()
            Using ms As New MemoryStream(bytes)
                bi.BeginInit()
                bi.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad
                bi.StreamSource = ms
                bi.EndInit()
                bi.Freeze() ' Penting untuk cross-thread
            End Using
            Return bi
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Validation"

    ''' <summary>
    ''' Menghitung checksum MD5 dari data frame.
    ''' </summary>
    Public Function HitungChecksum() As String
        Try
            Dim data = $"{NomorFrame}|{Lebar}|{Tinggi}|{Timestamp}|{DataGambarBase64}"
            Using md5 = System.Security.Cryptography.MD5.Create()
                Dim hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data))
                Return BitConverter.ToString(hash).Replace("-", "").ToLower()
            End Using
        Catch
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Validasi checksum frame.
    ''' </summary>
    Public Function ValidasiChecksum() As Boolean
        Return Checksum = HitungChecksum()
    End Function

    ''' <summary>
    ''' Cek apakah frame valid (memiliki data).
    ''' </summary>
    Public Function IsValid() As Boolean
        Return Not String.IsNullOrEmpty(DataGambarBase64) AndAlso Lebar > 0 AndAlso Tinggi > 0
    End Function

#End Region

#Region "Size Info"

    ''' <summary>
    ''' Mendapatkan ukuran data dalam bytes.
    ''' </summary>
    Public Function UkuranDataBytes() As Integer
        If String.IsNullOrEmpty(DataGambarBase64) Then Return 0
        Return DataGambarBase64.Length
    End Function

    ''' <summary>
    ''' Mendapatkan ukuran data dalam kilobytes.
    ''' </summary>
    Public Function UkuranDataKB() As Double
        Return UkuranDataBytes() / 1024.0
    End Function

#End Region

End Class
