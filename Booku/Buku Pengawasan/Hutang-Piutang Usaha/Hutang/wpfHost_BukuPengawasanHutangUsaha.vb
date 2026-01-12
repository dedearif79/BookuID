Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanHutangUsaha
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Semua (Lokal - IDR)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_IDR

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha.AsalPembelian = AsalPembelian_Lokal
        usc_BukuPengawasanHutangUsaha.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha.JenisRelasi_Induk = Pilihan_Semua
        usc_BukuPengawasanHutangUsaha.COAHutang = Kosongan
        usc_BukuPengawasanHutangUsaha.VisibilitasFilterJenisRelasi(True)
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Afiliasi (Lokal - IDR)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_Afiliasi
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_IDR

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Afiliasi"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_Afiliasi
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Afiliasi = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Afiliasi.AsalPembelian = AsalPembelian_Lokal
        usc_BukuPengawasanHutangUsaha_Afiliasi.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Afiliasi.JenisRelasi_Induk = JenisRelasi_Afiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Afiliasi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 3: Non Afiliasi (Lokal - IDR)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_NonAfiliasi
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_IDR

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Non Afiliasi"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_NonAfiliasi
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_NonAfiliasi = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_NonAfiliasi.AsalPembelian = AsalPembelian_Lokal
        usc_BukuPengawasanHutangUsaha_NonAfiliasi.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_NonAfiliasi.JenisRelasi_Induk = JenisRelasi_NonAfiliasi
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_NonAfiliasi.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 4: Impor USD
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_Impor_USD
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_USD

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_Impor_USD
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_USD = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_USD.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_USD.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_USD.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_USD.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 5: Impor AUD
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_Impor_AUD
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_AUD

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_Impor_AUD
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_AUD = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_AUD.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_AUD.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_AUD.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_AUD.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 6: Impor JPY
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_Impor_JPY
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_JPY

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_Impor_JPY
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_JPY = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_JPY.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_JPY.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_JPY.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_JPY.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 7: Impor CNY
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_Impor_CNY
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_CNY

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_Impor_CNY
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_CNY = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_CNY.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_CNY.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_CNY.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_CNY.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 8: Impor EUR
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_Impor_EUR
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_EUR

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_Impor_EUR
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_EUR = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_EUR.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_EUR.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_EUR.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_EUR.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 9: Impor SGD
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_Impor_SGD
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_SGD

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_Impor_SGD
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_SGD = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_SGD.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_SGD.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_SGD.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_SGD.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 10: Impor GBP
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanHutangUsaha_Impor_GBP
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property KodeMataUang As String = KodeMataUang_GBP

    Sub New()
        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Inisialisasi()
        Me.Content = usc_BukuPengawasanHutangUsaha_Impor_GBP
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_GBP = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha_Impor_GBP.AsalPembelian = AsalPembelian_Impor
        usc_BukuPengawasanHutangUsaha_Impor_GBP.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha_Impor_GBP.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangUsaha_Impor_GBP.RefreshTampilanData()
    End Sub

End Class
