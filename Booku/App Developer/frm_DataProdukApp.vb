Imports System.Windows
Imports System.Windows.Forms.Integration

Public Class frm_DataProdukApp

    Public JudulForm

    Private Sub frm_DataProdukApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Style_HalamanModul(Me)

        JudulForm = "Data Produk Aplikasi"
        Me.Text = JudulForm

        usc_DataProdukApp = New wpfUsc_DataProdukApp
        Dim host As New ElementHost
        host.Dock = DockStyle.Fill
        host.Child = usc_DataProdukApp
        Me.Controls.Add(host)

    End Sub

End Class
