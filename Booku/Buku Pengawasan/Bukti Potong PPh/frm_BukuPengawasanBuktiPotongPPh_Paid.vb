Imports System.Windows.Forms.Integration
Imports bcomm

Public Class frm_BukuPengawasanBuktiPotongPPh_Paid

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Buku Pengawasan Bukti Potong PPh (Paid)"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuPengawasanBuktiPotongPPh_Paid
        Me.Controls.Add(host)

    End Sub


    Sub Inisialisasi()
        usc_BukuPengawasanBuktiPotongPPh_Paid = New wpfUsc_BukuPengawasanBuktiPotongPPh_Paid
    End Sub


    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPengawasanBuktiPotongPPh_Paid.TampilkanData()
    End Sub

End Class
