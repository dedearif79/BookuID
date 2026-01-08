Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanPiutangUsaha_Ekspor_EUR

    Public JudulForm
    Public KodeMataUang = KodeMataUang_EUR

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Piutang Usaha - Ekspor - " & KodeMataUang
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanPiutangUsaha_Ekspor_EUR
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR = New wpfUsc_BukuPengawasanPiutangUsaha
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.KodeMataUang = KodeMataUang_EUR
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.DestinasiPenjualan = DestinasiPenjualan_Ekspor
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.JenisRelasi_Induk = Pilihan_Semua
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.RefreshTampilanData()
    End Sub

End Class