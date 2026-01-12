Imports System.Windows.Controls
Imports bcomm

' =====================================================================
' WPF Host untuk wpfUsc_BukuPengawasanDepositOperasional
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================

Public Class wpfHost_BukuPengawasanDepositOperasional
    Inherits ContentControl

    Public Property JudulForm As String
    Public Property DariDataAwal As Boolean = False

    Sub New(Optional DariDataAwal As Boolean = False)
        Me.DariDataAwal = DariDataAwal

        If DariDataAwal AndAlso JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = "Data Awal Deposit Operasional"
        Else
            JudulForm = "Buku Pengawasan Deposit Operasional"
        End If

        Inisialisasi()
        Me.Content = usc_BukuPengawasanDepositOperasional
    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanDepositOperasional = New wpfUsc_BukuPengawasanDepositOperasional
        usc_BukuPengawasanDepositOperasional.NamaHalaman = Halaman_BUKUPENGAWASANDEPOSITOPERASIONAL
        usc_BukuPengawasanDepositOperasional.JudulForm = JudulForm
        usc_BukuPengawasanDepositOperasional.COAPiutang = KodeTautanCOA_DepositOperasional
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanDepositOperasional.RefreshTampilanData()
    End Sub

End Class
