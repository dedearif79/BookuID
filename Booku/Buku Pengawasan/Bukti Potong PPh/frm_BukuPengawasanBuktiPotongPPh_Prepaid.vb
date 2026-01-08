Imports System.Windows.Forms.Integration
Imports bcomm

Public Class frm_BukuPengawasanBuktiPotongPPh_Prepaid

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Bukti Potong PPh (Prepaid)"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanBuktiPotongPPh_Prepaid
        Me.Controls.Add(host)

    End Sub


    Sub Inisialisasi()
        usc_BukuPengawasanBuktiPotongPPh_Prepaid = New wpfUsc_BukuPengawasanBuktiPotongPPh_Prepaid
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanBuktiPotongPPh_Prepaid.TampilkanData()
    End Sub

End Class
