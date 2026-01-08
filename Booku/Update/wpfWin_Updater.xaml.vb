Imports System.Net.Http
Imports System.Windows
Imports bcomm
Imports DocumentFormat.OpenXml.Office2016.Drawing.Charts

Public Class wpfWin_Updater

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        DownloadBerhasil = False
        lbl_01.Text = "Tersedia Update terbaru untuk aplikasi ini!"
        pgb_Progress.Visibility = Visibility.Collapsed
        lbl_Progress.Visibility = Visibility.Collapsed
        btn_Lanjutkan.Content = "Update Sekarang"
        btn_Kembali.Content = "Update Nanti"

    End Sub


    Async Sub TahapanUpdating()

        ProsesUpdate_Aplikasi = True

        Await DownloadPaketUpdater()

        Await EkstrakPaketUpdater()

        JalankanAplikasiUpdater()

        Finish()

    End Sub


    Async Function DownloadPaketUpdater() As Task

        If ProsesUpdate_Aplikasi = True Then
            BuatFolder(FolderTempUpdater)
            DownloadBerhasil = Await DownloadFileDariServerAsync_MetodeHTTP(urlPaketUpdaterPublic, FilePathPaketUpdaterLokal, urlFileDownloader_PHP, pgb_Progress, lbl_Progress)
        End If

        If DownloadBerhasil Then
            ProsesUpdate_Aplikasi = True
        Else
            ProsesUpdate_Aplikasi = False
        End If

        Jeda(333)

    End Function

    Async Function EkstrakPaketUpdater() As Task


        lbl_01.Text = "Paket updater sedang diekstrak."
        lbl_Progress.Text = Kosongan
        Terabas()

        If ProsesUpdate_Aplikasi Then
            Await EkstrakFile(FilePathPaketUpdaterLokal, FolderTempUpdater, pgb_Progress, lbl_Progress)
            If EkstrakBerhasil Then
                ProsesUpdate_Aplikasi = True
            Else
                ProsesUpdate_Aplikasi = False
            End If
        End If

    End Function


    Sub JalankanAplikasiUpdater()

        If ProsesUpdate_Aplikasi Then
            Dim po As New Process
            po.StartInfo.FileName = FilePathUpdaterLokal
            po.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Try
                po.Start()
                ProsesUpdate_Aplikasi = True
            Catch ex As Exception
                ProsesUpdate_Aplikasi = False
            End Try
        End If

    End Sub


    Sub Finish()

        If ProsesUpdate_Aplikasi = True Then
            End     '(Aplikasi ditutup karena akan menjalankan Updater...!)
        Else
            Close() '(Jendela ditutup, aplikasi tetap dilanjutkan tanpa Update)
        End If

    End Sub



    Private Sub btn_Lanjutkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Lanjutkan.Click

        btn_Kembali.Content = "Batalkan Update"
        btn_Lanjutkan.Visibility = Visibility.Collapsed
        pgb_Progress.Visibility = Visibility.Visible
        lbl_Progress.Visibility = Visibility.Visible
        lbl_01.Text = "Mohon tunggu! Sistem sedang mendownload file Updater."
        Terabas()

        ProsesUpdate_Aplikasi = True

        If Not DownloadBerhasil Then TahapanUpdating()

    End Sub


    Private Sub btn_Kembali_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kembali.Click
        ProsesUpdate_Aplikasi = False
        If cts IsNot Nothing Then
            cts.Cancel()
        End If
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        WindowStyle = WindowStyle.None
    End Sub
End Class
