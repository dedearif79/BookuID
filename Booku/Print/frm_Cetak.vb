Imports System.IO
Imports Microsoft.Office.Interop.Excel

Public Class frm_Cetak

    Public HalamanTarget
    Public JudulForm
    Public FungsiForm
    Dim ClearHalaman = "http://127.0.0.1:88/rekend/sat/cetak"

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = JudulForm
        lbl_Judul.Text = JudulForm

        HapusCacheBrowser()

        TampilkanHalamanCetak()

        btn_PrintPreview.Visible = False

    End Sub

    Async Sub BersihkanHalaman()
        Await web_TampilanCetak.EnsureCoreWebView2Async(Nothing)
        web_TampilanCetak.Source = New Uri(ClearHalaman)
    End Sub

    Async Sub TampilkanHalamanCetak()

        Await web_TampilanCetak.EnsureCoreWebView2Async(Nothing)
        web_TampilanCetak.Source = New Uri(HalamanTarget)

        If FungsiForm = FungsiForm_CETAK Then Cetak()

    End Sub

    Async Sub PrintPreview()
        Await web_TampilanCetak.CoreWebView2.ExecuteScriptAsync("window.print();")
    End Sub

    Async Sub Cetak()
        Await web_TampilanCetak.CoreWebView2.ExecuteScriptAsync("window.print();")
    End Sub

    Public Sub btn_PrintPreview_Click(sender As Object, e As EventArgs) Handles btn_PrintPreview.Click
        PrintPreview()
    End Sub

    Public Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        Cetak()
    End Sub

    Sub HapusCacheBrowser()

        Dim cacheDirectory As String

        'cahce_data
        cacheDirectory = "SAT_Rekend.exe.WebView2\EBWebView\Default\Cache\Cache_Data"
        Try
            For Each cacheFile As String In Directory.GetFiles(cacheDirectory)
                File.Delete(cacheFile)
            Next
        Catch ex As Exception
        End Try

    End Sub

    Private Sub TutupForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        BersihkanHalaman()
    End Sub

End Class