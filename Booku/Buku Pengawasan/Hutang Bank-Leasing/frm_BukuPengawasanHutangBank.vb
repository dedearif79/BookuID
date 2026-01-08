Imports System.Windows.Forms.Integration

Public Class frm_BukuPengawasanHutangBank

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Hutang Bank"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanHutangBank
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_BukuPengawasanHutangBank = New wpfUsc_BukuPengawasanHutangBankLeasing
        usc_BukuPengawasanHutangBank.NamaHalaman = Halaman_BUKUPENGAWASANHUTANGBANK
        usc_BukuPengawasanHutangBank.BankLeasing = bl_Bank
        usc_BukuPengawasanHutangBank.JudulForm = JudulForm
        usc_BukuPengawasanHutangBank.COAHutang = KodeTautanCOA_HutangBank
        usc_BukuPengawasanHutangBank.TabelPengawasan = "tbl_PengawasanHutangBank"
        usc_BukuPengawasanHutangBank.TabelAngsuran = "tbl_JadwalAngsuranHutangBank"
        usc_BukuPengawasanHutangBank.KolomNomorBPH = "Nomor_BPHB"
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanHutangBank.RefreshTampilanData()
    End Sub

End Class