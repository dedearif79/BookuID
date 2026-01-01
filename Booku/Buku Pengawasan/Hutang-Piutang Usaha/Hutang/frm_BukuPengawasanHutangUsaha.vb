Imports System.Windows.Forms.Integration
Imports bcomm


Public Class frm_BukuPengawasanHutangUsaha

    Public JudulForm
    Public KodeMataUang = KodeMataUang_IDR

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Usaha"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangUsaha
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangUsaha = New wpfUsc_BukuPengawasanHutangUsaha
        usc_BukuPengawasanHutangUsaha.AsalPembelian = AsalPembelian_Lokal
        usc_BukuPengawasanHutangUsaha.KodeMataUang = KodeMataUang
        usc_BukuPengawasanHutangUsaha.JenisRelasi_Induk = Pilihan_Semua
        usc_BukuPengawasanHutangUsaha.COAHutang = Kosongan
        usc_BukuPengawasanHutangUsaha.VisibilitasFilterJenisRelasi(True)
    End Sub

End Class