Imports System.Windows.Forms.Integration

Public Class frm_DataLawanTransaksi

    Public JudulForm

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Data Lawan Transaksi"
        Me.Text = JudulForm

        usc_DataLawanTransaksi = New wpfUsc_DataLawanTransaksi
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DataLawanTransaksi
        Me.Controls.Add(host)

    End Sub

End Class