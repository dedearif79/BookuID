Option Explicit On
Option Strict On

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Threading.Tasks
Imports System.Runtime.InteropServices

''' <summary>
''' Modul untuk menangkap layar (screen capture).
''' Menggunakan Graphics.CopyFromScreen untuk capture.
''' Mendukung capture cursor untuk ditampilkan di remote viewer.
''' </summary>
Public Module mdl_TangkapLayar

#Region "Windows API untuk Cursor"

    <StructLayout(LayoutKind.Sequential)>
    Private Structure POINT
        Public X As Integer
        Public Y As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure CURSORINFO
        Public cbSize As Integer
        Public flags As Integer
        Public hCursor As IntPtr
        Public ptScreenPos As POINT
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure ICONINFO
        Public fIcon As Boolean
        Public xHotspot As Integer
        Public yHotspot As Integer
        Public hbmMask As IntPtr
        Public hbmColor As IntPtr
    End Structure

    Private Const CURSOR_SHOWING As Integer = &H1

    <DllImport("user32.dll")>
    Private Function GetCursorInfo(ByRef pci As CURSORINFO) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Function GetIconInfo(hIcon As IntPtr, ByRef piconinfo As ICONINFO) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Function CopyIcon(hIcon As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Function DestroyIcon(hIcon As IntPtr) As Boolean
    End Function

    <DllImport("gdi32.dll")>
    Private Function DeleteObject(hObject As IntPtr) As Boolean
    End Function

#End Region

#Region "Variabel"

    ''' <summary>Counter nomor frame</summary>
    Private _nomorFrame As Long = 0

    ''' <summary>Skala default untuk capture (0.6 = 60% - balance kualitas dan bandwidth)</summary>
    Public Const SKALA_DEFAULT As Double = 0.6

    ''' <summary>Kualitas JPEG untuk streaming (50 = balance antara kualitas dan ukuran)</summary>
    Public Const KUALITAS_JPEG As Integer = 50

    ''' <summary>Flag untuk mengaktifkan/menonaktifkan capture cursor</summary>
    Public GambarCursor As Boolean = True

#End Region

#Region "Cursor Drawing"

    ''' <summary>
    ''' Menggambar cursor pada bitmap.
    ''' </summary>
    ''' <param name="bitmap">Bitmap target untuk digambar cursor</param>
    ''' <param name="screenBounds">Bounds layar asli (sebelum skala)</param>
    ''' <param name="skala">Skala yang diterapkan pada bitmap</param>
    Private Sub GambarCursorPadaBitmap(bitmap As Bitmap, screenBounds As Rectangle, skala As Double)
        If Not GambarCursor Then Return

        Try
            Dim cursorInfo As New CURSORINFO()
            cursorInfo.cbSize = Marshal.SizeOf(cursorInfo)

            If GetCursorInfo(cursorInfo) AndAlso (cursorInfo.flags And CURSOR_SHOWING) <> 0 Then
                ' Copy cursor handle
                Dim hCursor = CopyIcon(cursorInfo.hCursor)
                If hCursor = IntPtr.Zero Then Return

                Try
                    ' Get cursor icon info untuk hotspot
                    Dim iconInfo As New ICONINFO()
                    If GetIconInfo(hCursor, iconInfo) Then
                        Try
                            ' Hitung posisi cursor dengan memperhitungkan skala dan hotspot
                            Dim cursorX = CInt((cursorInfo.ptScreenPos.X - screenBounds.X - iconInfo.xHotspot) * skala)
                            Dim cursorY = CInt((cursorInfo.ptScreenPos.Y - screenBounds.Y - iconInfo.yHotspot) * skala)

                            ' Gambar cursor ke bitmap
                            Using cursorIcon = Icon.FromHandle(hCursor)
                                Using g = Graphics.FromImage(bitmap)
                                    ' Hitung ukuran cursor yang di-scale
                                    Dim cursorWidth = CInt(cursorIcon.Width * skala)
                                    Dim cursorHeight = CInt(cursorIcon.Height * skala)

                                    ' Pastikan ukuran minimal
                                    If cursorWidth < 16 Then cursorWidth = 16
                                    If cursorHeight < 16 Then cursorHeight = 16

                                    g.DrawIcon(cursorIcon, New Rectangle(cursorX, cursorY, cursorWidth, cursorHeight))
                                End Using
                            End Using
                        Finally
                            ' Cleanup bitmap handles dari ICONINFO
                            If iconInfo.hbmMask <> IntPtr.Zero Then DeleteObject(iconInfo.hbmMask)
                            If iconInfo.hbmColor <> IntPtr.Zero Then DeleteObject(iconInfo.hbmColor)
                        End Try
                    End If
                Finally
                    DestroyIcon(hCursor)
                End Try
            End If
        Catch ex As Exception
            ' Jangan crash jika gagal menggambar cursor
            WriteLog($"[CURSOR] Error drawing cursor: {ex.Message}")
        End Try
    End Sub

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

            ' Gambar cursor pada skala 1.0
            GambarCursorPadaBitmap(bitmap, bounds, 1.0)

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

                ' Gambar cursor pada bitmap yang sudah di-scale
                GambarCursorPadaBitmap(scaledBitmap, bounds, skala)

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
    ''' Ekstrak raw BGRA pixel data dari Bitmap untuk H.264 encoding.
    ''' FFmpeg membutuhkan format bgra (Blue-Green-Red-Alpha, 4 bytes per pixel).
    ''' </summary>
    ''' <param name="bitmap">Bitmap source (Format32bppArgb)</param>
    ''' <returns>Byte array berisi BGRA data, Nothing jika gagal</returns>
    Public Function BitmapKeBgra(bitmap As Bitmap) As Byte()
        If bitmap Is Nothing Then Return Nothing

        Try
            ' Lock bitmap untuk akses langsung ke pixel data
            Dim rect As New Rectangle(0, 0, bitmap.Width, bitmap.Height)
            Dim bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb)

            Try
                ' Calculate buffer size (4 bytes per pixel: BGRA)
                Dim byteCount = Math.Abs(bmpData.Stride) * bitmap.Height
                Dim bgraData(byteCount - 1) As Byte

                ' Copy pixel data
                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, bgraData, 0, byteCount)

                Return bgraData
            Finally
                bitmap.UnlockBits(bmpData)
            End Try

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"[BGRA] Error extracting BGRA: {ex.Message}")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Tangkap layar dan return raw BGRA data untuk H.264 encoding.
    ''' </summary>
    ''' <param name="skala">Skala capture (0.0 - 1.0)</param>
    ''' <param name="lebar">Output: lebar gambar</param>
    ''' <param name="tinggi">Output: tinggi gambar</param>
    ''' <returns>BGRA byte array, Nothing jika gagal</returns>
    Public Function TangkapLayarKeBgra(skala As Double, ByRef lebar As Integer, ByRef tinggi As Integer) As Byte()
        lebar = 0
        tinggi = 0

        Using bitmap = TangkapLayarDenganSkala(skala)
            If bitmap Is Nothing Then Return Nothing

            lebar = bitmap.Width
            tinggi = bitmap.Height
            Return BitmapKeBgra(bitmap)
        End Using
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
