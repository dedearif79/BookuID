Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanDepositOperasional

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = "Data Awal Deposit Operasional"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            JudulForm = "Buku Pengawasan Deposit Operasional"
        End If

        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanDepositOperasional
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanDepositOperasional = New wpfUsc_BukuPengawasanDepositOperasional
        usc_BukuPengawasanDepositOperasional.NamaHalaman = Halaman_BUKUPENGAWASANDEPOSITOPERASIONAL
        usc_BukuPengawasanDepositOperasional.COAPiutang = KodeTautanCOA_DepositOperasional
        usc_BukuPengawasanDepositOperasional.JudulForm = JudulForm
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanDepositOperasional.RefreshTampilanData()
    End Sub

End Class