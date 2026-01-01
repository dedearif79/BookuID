Imports System.Windows.Forms.Integration

Public Class frm_LaporanNeracaLajur

    Public JudulForm
    Public JalurMasuk

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Neraca Lajur"
        Me.Text = JudulForm

        usc_LaporanNeracaLajur = New wpfUsc_LaporanNeracaLajur
        usc_LaporanNeracaLajur.JalurMasuk = Halaman_MENUUTAMA
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_LaporanNeracaLajur
        Me.Controls.Add(host)

    End Sub

End Class