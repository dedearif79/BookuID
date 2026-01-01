Imports System.Windows.Forms.Integration

Public Class frm_TutupBuku

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Tutup Buku - Tahun " & TahunBukuAktif
        Me.Text = JudulForm

        usc_TutupBuku = New wpfUsc_TutupBuku
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_TutupBuku
        Me.Controls.Add(host)

    End Sub

End Class