Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangUsaha_Afiliasi

    Public JudulForm
    Public KodeMataUang As String = KodeMataUang_IDR

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Usaha - Afiliasi"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangUsaha_Afiliasi
        Me.Controls.Add(host)

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