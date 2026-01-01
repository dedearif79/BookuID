Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangUsaha_Impor_USD

    Public JudulForm
    Public KodeMataUang = KodeMataUang_USD

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangUsaha_Impor_USD
        Me.Controls.Add(host)

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