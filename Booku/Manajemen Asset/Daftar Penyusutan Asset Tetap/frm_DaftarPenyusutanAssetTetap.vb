Imports System.Windows.Forms.Integration
Imports bcomm


Public Class frm_DaftarPenyusutanAssetTetap

    Public JudulForm
    Public JalurMasuk

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Daftar Penyusutan Asset Tetap"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DaftarPenyusutanAssetTetap
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_DaftarPenyusutanAssetTetap = New wpfUsc_DaftarPenyusutanAssetTetap
        usc_DaftarPenyusutanAssetTetap.JalurMasuk = JalurMasuk
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DaftarPenyusutanAssetTetap.ResetFilter()
        usc_DaftarPenyusutanAssetTetap.EksekusiTampilanData = True
        usc_DaftarPenyusutanAssetTetap.TampilkanData_Detail_Rekap() '(Untuk menampilkan Jurnal, sekaligus mengecek kesesuaiannya)
    End Sub

End Class