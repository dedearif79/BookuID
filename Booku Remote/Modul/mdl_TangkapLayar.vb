Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Threading.Tasks

''' <summary>
''' Modul untuk menangkap layar (screen capture).
''' Menggunakan Graphics.CopyFromScreen untuk capture.
''' </summary>
Public Module mdl_TangkapLayar

#Region "Variabel"

    ''' <summary>Counter nomor frame</summary>
    Private _nomorFrame As Long = 0

    ''' <summary>Skala default untuk capture (0.5 = 50%)</summary>
    Public Const SKALA_DEFAULT As Double = 0.5

    ''' <summary>Kualitas JPEG (tidak dipakai, kita pakai PNG)</summary>
    Public Const KUALITAS_JPEG As Integer = 70

#End Region

#Region "Screen Capture - Full Screen"

    ''' <summary>
    ''' Tangkap seluruh layar utama (primary screen).
    ''' </summary>
    Public Function TangkapLayarPenuh() As Bitmap
        Try
            Dim bounds = Screen.PrimaryScreen.Bounds
            Dim bitmap As New Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb)

            Using g = Graphics.FromImage(bitmap)
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
            End Using

            Return bitmap

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Tangkap layar dengan skala tertentu (untuk mengurangi bandwidth).
    ''' </summary>
    Public Function TangkapLayarDenganSkala(Optional skala As Double = SKALA_DEFAULT) As Bitmap
        Try
            Dim bounds = Screen.PrimaryScreen.Bounds
            Dim lebarBaru = CInt(bounds.Width * skala)
            Dim tinggiBaru = CInt(bounds.Height * skala)

            ' Capture full screen terlebih dahulu
            Using fullBitmap As New Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb)
                Using g = Graphics.FromImage(fullBitmap)
                    g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)
                End Using

                ' Resize ke skala yang diinginkan
                Dim scaledBitmap As New Bitmap(lebarBaru, tinggiBaru, PixelFormat.Format32bppArgb)
                Using g = Graphics.FromImage(scaledBitmap)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                    g.CompositingQuality = Drawing2D.CompositingQuality.HighSpeed
                    g.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighSpeed
                    g.DrawImage(fullBitmap, 0, 0, lebarBaru, tinggiBaru)
                End Using

                Return scaledBitmap
            End Using

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "Screen Capture - Area Tertentu"

    ''' <summary>
    ''' Tangkap area tertentu dari layar.
    ''' </summary>
    Public Function TangkapArea(area As Rectangle) As Bitmap
        Try
            Dim bitmap As New Bitmap(area.Width, area.Height, PixelFormat.Format32bppArgb)

            Using g = Graphics.FromImage(bitmap)
                g.CopyFromScreen(area.X, area.Y, 0, 0, area.Size, CopyPixelOperation.SourceCopy)
            End Using

            Return bitmap

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Tangkap area tertentu dengan skala.
    ''' </summary>
    Public Function TangkapAreaDenganSkala(area As Rectangle, skala As Double) As Bitmap
        Try
            Dim lebarBaru = CInt(area.Width * skala)
            Dim tinggiBaru = CInt(area.Height * skala)

            Using fullBitmap = TangkapArea(area)
                If fullBitmap Is Nothing Then Return Nothing

                Dim scaledBitmap As New Bitmap(lebarBaru, tinggiBaru, PixelFormat.Format32bppArgb)
                Using g = Graphics.FromImage(scaledBitmap)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                    g.DrawImage(fullBitmap, 0, 0, lebarBaru, tinggiBaru)
                End Using

                Return scaledBitmap
            End Using

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "Frame Builder"

    ''' <summary>
    ''' Tangkap layar dan buat objek cls_FrameLayar.
    ''' </summary>
    Public Function TangkapFrame(Optional skala As Double = SKALA_DEFAULT) As cls_FrameLayar
        Try
            _nomorFrame += 1

            Using bitmap = TangkapLayarDenganSkala(skala)
                If bitmap Is Nothing Then Return Nothing
                Return New cls_FrameLayar(_nomorFrame, bitmap)
            End Using

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Tangkap frame secara async (untuk tidak blocking UI).
    ''' </summary>
    Public Async Function TangkapFrameAsync(Optional skala As Double = SKALA_DEFAULT) As Task(Of cls_FrameLayar)
        Return Await Task.Run(Function() TangkapFrame(skala))
    End Function

    ''' <summary>
    ''' Reset counter nomor frame.
    ''' </summary>
    Public Sub ResetNomorFrame()
        _nomorFrame = 0
    End Sub

    ''' <summary>
    ''' Mendapatkan nomor frame saat ini.
    ''' </summary>
    Public Function NomorFrameSaatIni() As Long
        Return _nomorFrame
    End Function

#End Region

#Region "Screen Info"

    ''' <summary>
    ''' Mendapatkan ukuran layar utama.
    ''' </summary>
    Public Function UkuranLayarUtama() As Size
        Return Screen.PrimaryScreen.Bounds.Size
    End Function

    ''' <summary>
    ''' Mendapatkan jumlah monitor.
    ''' </summary>
    Public Function JumlahMonitor() As Integer
        Return Screen.AllScreens.Length
    End Function

    ''' <summary>
    ''' Mendapatkan daftar semua layar.
    ''' </summary>
    Public Function DaftarLayar() As Screen()
        Return Screen.AllScreens
    End Function

    ''' <summary>
    ''' Mendapatkan bounds layar utama.
    ''' </summary>
    Public Function BoundsLayarUtama() As Rectangle
        Return Screen.PrimaryScreen.Bounds
    End Function

#End Region

#Region "Conversion Helpers"

    ''' <summary>
    ''' Resize bitmap ke ukuran tertentu.
    ''' </summary>
    Public Function ResizeBitmap(source As Bitmap, lebarBaru As Integer, tinggiBaru As Integer) As Bitmap
        Try
            Dim result As New Bitmap(lebarBaru, tinggiBaru, PixelFormat.Format32bppArgb)
            Using g = Graphics.FromImage(result)
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
                g.DrawImage(source, 0, 0, lebarBaru, tinggiBaru)
            End Using
            Return result
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Konversi Bitmap ke byte array (PNG format).
    ''' </summary>
    Public Function BitmapKePNG(bitmap As Bitmap) As Byte()
        Try
            Using ms As New System.IO.MemoryStream()
                bitmap.Save(ms, ImageFormat.Png)
                Return ms.ToArray()
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Estimasi ukuran frame dalam KB berdasarkan skala.
    ''' </summary>
    Public Function EstimasiUkuranFrameKB(skala As Double) As Double
        ' Estimasi kasar: PNG biasanya 10-20% dari raw bitmap
        Dim bounds = Screen.PrimaryScreen.Bounds
        Dim lebar = CInt(bounds.Width * skala)
        Dim tinggi = CInt(bounds.Height * skala)
        Dim rawSize = lebar * tinggi * 4 ' 4 bytes per pixel (ARGB)
        Dim estimatedPngSize = rawSize * 0.15 ' ~15% compression ratio
        Return estimatedPngSize / 1024
    End Function

#End Region

End Module
