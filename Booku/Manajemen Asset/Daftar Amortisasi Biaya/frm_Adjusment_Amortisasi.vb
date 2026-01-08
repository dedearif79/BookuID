Imports System.Threading
Imports System.Windows.Forms.Integration
Imports bcomm

Public Class frm_Adjusment_Amortisasi


    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Adjusment Amortisasi Biaya"
        Me.Text = JudulForm

        Inisialisasi()
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = Usc_Adjusment_Amortisasi
        Me.Controls.Add(host)

    End Sub

    Sub Inisialisasi()
        Usc_Adjusment_Amortisasi = New wpfUsc_Adjusment_Amortisasi
    End Sub

    Sub CekAdjusment()
        Inisialisasi()
        usc_Adjusment_Amortisasi.RefreshTampilanData()
    End Sub


End Class