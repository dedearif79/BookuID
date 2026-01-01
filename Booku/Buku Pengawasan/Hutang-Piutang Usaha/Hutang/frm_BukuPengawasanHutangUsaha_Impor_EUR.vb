Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangUsaha_Impor_EUR

    Public JudulForm
    Public KodeMataUang = KodeMataUang_EUR

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Usaha - Impor - " & KodeMataUang
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangUsaha_Impor_EUR
        Me.Controls.Add(host)

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