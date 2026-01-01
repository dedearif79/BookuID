Imports System.Threading
Imports System.Windows.Forms.Integration
Imports bcomm

Public Class frm_DaftarAmortisasiBiaya

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Daftar Amortisasi Biaya"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DaftarAmortisasiBiaya
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        usc_DaftarAmortisasiBiaya = New wpfUsc_DaftarAmortisasiBiaya
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DaftarAmortisasiBiaya.RefreshTampilanData()
    End Sub


End Class