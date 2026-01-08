Imports System.Windows.Forms.Integration

Public Class frm_BukuDisposalAssetTetap

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        Me.Text = "Buku Disposal Asset Tetap"

        usc_BukuDisposalAssetTetap = New wpfUsc_BukuDisposalAssetTetap
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_BukuDisposalAssetTetap
        Me.Controls.Add(host)

    End Sub

End Class
