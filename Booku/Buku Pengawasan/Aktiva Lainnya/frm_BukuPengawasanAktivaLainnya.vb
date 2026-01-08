Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanAktivaLainnya

    Public JudulForm As String

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = "Data Awal Aktiva Lain-lain"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            JudulForm = "Buku Pengawasan Aktiva Lain-lain"
        End If

        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanAktivaLainnya
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanAktivaLainnya = New wpfUsc_BukuPengawasanAktivaLainnya
        usc_BukuPengawasanAktivaLainnya.NamaHalaman = Halaman_BUKUPENGAWASANAKTIVALAINNYA
        usc_BukuPengawasanAktivaLainnya.JudulForm = JudulForm
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanAktivaLainnya.RefreshTampilanData()
    End Sub

End Class
