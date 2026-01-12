Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanPiutangUsaha
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Semua (Lokal - IDR)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_IDR

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_BukuPengawasanPiutangUsaha.JenisRelasi_Induk = Pilihan_Semua
        usc_BukuPengawasanPiutangUsaha.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha.COAPiutang = Kosongan
        usc_BukuPengawasanPiutangUsaha.VisibilitasFilterJenisRelasi(True)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Afiliasi (Lokal - IDR)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_Afiliasi
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_IDR

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Afiliasi"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_Afiliasi
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Afiliasi = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Afiliasi.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_BukuPengawasanPiutangUsaha_Afiliasi.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Afiliasi.JenisRelasi_Induk = JenisRelasi_Afiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Afiliasi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 3: Non Afiliasi (Lokal - IDR)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_NonAfiliasi
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_IDR

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Non Afiliasi"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_NonAfiliasi
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi.DestinasiPenjualan = DestinasiPenjualan_Lokal
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi.JenisRelasi_Induk = JenisRelasi_NonAfiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_NonAfiliasi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 4: Ekspor USD
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_Ekspor_USD
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_USD

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_Ekspor_USD
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_USD = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_USD.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Ekspor_USD.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_USD.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_USD.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 5: Ekspor AUD
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_Ekspor_AUD
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_AUD

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_Ekspor_AUD
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_AUD = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_AUD.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Ekspor_AUD.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_AUD.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_AUD.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 6: Ekspor JPY
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_Ekspor_JPY
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_JPY

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_Ekspor_JPY
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 7: Ekspor CNY
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_Ekspor_CNY
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_CNY

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_Ekspor_CNY
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_CNY = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_CNY.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Ekspor_CNY.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_CNY.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_CNY.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 8: Ekspor EUR
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_Ekspor_EUR
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_EUR

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_Ekspor_EUR
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 9: Ekspor SGD
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_Ekspor_SGD
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_SGD

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_Ekspor_SGD
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_SGD = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_SGD.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Ekspor_SGD.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_SGD.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_SGD.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 10: Ekspor GBP
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanPiutangUsaha_Ekspor_GBP
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_GBP

    Sub New()
        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanPiutangUsaha_Ekspor_GBP
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP.KodeMataUang = KodeMataUang
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_GBP.RefreshTampilanData()
    End Sub

End Class
