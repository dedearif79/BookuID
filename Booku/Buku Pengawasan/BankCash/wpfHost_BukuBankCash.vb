Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk Buku Bank, Kas, PettyCash, CashAdvance
' Semua menggunakan wpfUsc_BukuBesar dengan FungsiModul berbeda
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Buku Bank
' ---------------------------------------------------------------------
Public Class wpfHost_BukuBank
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Bank"
        Inisialisasi()
        Me.Content = usc_BukuBank
    End Sub

    Sub Inisialisasi()
        usc_BukuBank = New wpfUsc_BukuBesar
        usc_BukuBank.FungsiModul = Halaman_BUKUBANK
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuBank.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Buku Kas
' ---------------------------------------------------------------------
Public Class wpfHost_BukuKas
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Kas"
        Inisialisasi()
        Me.Content = usc_BukuKas
    End Sub

    Sub Inisialisasi()
        usc_BukuKas = New wpfUsc_BukuBesar
        usc_BukuKas.FungsiModul = Halaman_BUKUKAS
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuKas.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 3: Petty Cash
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPettyCash
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Petty Cash"
        Inisialisasi()
        Me.Content = usc_BukuPettyCash
    End Sub

    Sub Inisialisasi()
        usc_BukuPettyCash = New wpfUsc_BukuBesar
        usc_BukuPettyCash.FungsiModul = Halaman_BUKUPETTYCASH
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPettyCash.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 4: Cash Advance
' ---------------------------------------------------------------------
Public Class wpfHost_BukuCashAdvance
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Cash Advance"
        Inisialisasi()
        Me.Content = usc_BukuCashAdvance
    End Sub

    Sub Inisialisasi()
        usc_BukuCashAdvance = New wpfUsc_BukuBesar
        usc_BukuCashAdvance.FungsiModul = Halaman_BUKUCASHADVANCE
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuCashAdvance.RefreshTampilanData()
    End Sub

End Class
