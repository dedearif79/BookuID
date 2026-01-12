Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanBuktiPotongPPh
' 1 file berisi 2 varian class (Paid dan Prepaid)
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Bukti Potong PPh (Paid)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanBuktiPotongPPh_Paid
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Bukti Potong PPh (Paid)"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanBuktiPotongPPh_Paid
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanBuktiPotongPPh_Paid = New wpfUsc_BukuPengawasanBuktiPotongPPh_Paid
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanBuktiPotongPPh_Paid.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Bukti Potong PPh (Prepaid)
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPengawasanBuktiPotongPPh_Prepaid
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pengawasan Bukti Potong PPh (Prepaid)"
        Inisialisasi()
        Me.Content = usc_BukuPengawasanBuktiPotongPPh_Prepaid
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanBuktiPotongPPh_Prepaid = New wpfUsc_BukuPengawasanBuktiPotongPPh_Prepaid
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanBuktiPotongPPh_Prepaid.RefreshTampilanData()
    End Sub

End Class
