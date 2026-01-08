Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangLeasing

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Leasing"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangLeasing
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangLeasing = New wpfUsc_BukuPengawasanHutangBankLeasing
        usc_BukuPengawasanHutangLeasing.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGLEASING
        usc_BukuPengawasanHutangLeasing.BankLeasing = bl_Leasing
        usc_BukuPengawasanHutangLeasing.JudulForm = JudulForm
        usc_BukuPengawasanHutangLeasing.COAHutang = KodeTautanCOA_HutangLeasing
        usc_BukuPengawasanHutangLeasing.TabelPengawasan = "tbl_PengawasanHutangLeasing"
        usc_BukuPengawasanHutangLeasing.TabelAngsuran = "tbl_JadwalAngsuranHutangLeasing"
        usc_BukuPengawasanHutangLeasing.KolomNomorBPH = "Nomor_BPHL"
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangLeasing.RefreshTampilanData()
    End Sub

End Class