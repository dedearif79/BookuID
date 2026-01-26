Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm
Imports MySql.Data.MySqlClient


Public Class wpfUsc_ManajemenAplikasi


    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False

    Dim NomorUrut

    Dim VersiApp
    Dim ApdetApp
    Dim urlPaketBooku
    Dim urlPaketInstaller
    Dim urlPaketUpdater
    Dim NamaFolderTempPaketBooku
    Dim NamaFolderTempPaketInstaller
    Dim NamaFolderTempPaketUpdater
    Dim NamaFileZipPaketBooku
    Dim NamaFileZipPaketBookuAssistant
    Dim NamaFileZipPaketBookuRemote
    Dim NamaFileZipPaketInstaller
    Dim NamaFileZipPaketUpdater
    Dim NamaFileExeInstaller
    Dim NamaFileExeUpdater

    Dim AdaPerubahanInfoGeneral As Boolean
    Dim AdaPerubahanInfoInstaller As Boolean
    Dim AdaPerubahanInfoUpdater As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        AdaPerubahanInfoGeneral = False
        AdaPerubahanInfoInstaller = False
        AdaPerubahanInfoUpdater = False

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_ManajemenAplikasi.JudulForm

        RefreshTampilanData()

        btn_SimpanPerubahanInfoUpdater.IsEnabled = False

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub RefreshTampilanData()
        btn_Refresh.IsEnabled = False
        ResetForm()
        Terabas()
        Jeda(333)
        TampilkanData()
        btn_Refresh.IsEnabled = True
    End Sub


    Async Sub TampilkanDataAsync()

        ' Guard clause
        If SedangMemuatData Then Return
        SedangMemuatData = True

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            TampilkanDataInfoGeneral()
            TampilkanDataInfoInstaller()
            TampilkanDataInfoUpdater()

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_ManajemenAplikasi")
            SedangMemuatData = False

        Finally
            KetersediaanMenuHalaman(pnl_Halaman, True)
            SedangMemuatData = False

        End Try

    End Sub

    ' Wrapper untuk backward compatibility
    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub


    Sub TampilkanDataInfoGeneral()

        Terabas()

        BukaDatabasePublic()

        If StatusKoneksiDatabasePublic Then
            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabasePublic)
            drPublic_ExecuteReader()
            drPublic.Read()
            txt_VersiApp.Text = drPublic.Item("Versi_App")
            txt_ApdetApp.Text = drPublic.Item("Apdet_App")
            txt_UrlPaketBooku.Text = drPublic.Item("URL_Paket_Booku")
            txt_FolderTempPaketBooku.Text = drPublic.Item("Folder_Temp_Paket_Booku")
            txt_FilePaketBooku.Text = drPublic.Item("File_Paket_Booku")
            NamaFileZipPaketBookuAssistant = drPublic.Item("File_Paket_Booku_Assistant")    'ByPass
            NamaFileZipPaketBookuRemote = drPublic.Item("File_Paket_Booku_Remote")          'ByPass
        Else
            ResetForm()
            PesanPeringatan("Koneksi ke server gagal..!")
        End If

        TutupDatabasePublic()

        btn_SimpanPerubahanInfoGeneral.IsEnabled = False
        AdaPerubahanInfoGeneral = False

    End Sub

    Sub TampilkanDataInfoInstaller()

        Terabas()

        BukaDatabasePublic()

        If StatusKoneksiDatabasePublic Then
            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabasePublic)
            drPublic_ExecuteReader()
            drPublic.Read()
            txt_UrlPaketInstaller.Text = drPublic.Item("URL_Paket_Installer")
            txt_FolderTempPaketInstaller.Text = drPublic.Item("Folder_Temp_Paket_Installer")
            txt_FilePaketInstaller.Text = drPublic.Item("File_Paket_Installer")
            txt_FileInstaller.Text = drPublic.Item("File_Installer")
        Else
            ResetForm()
            PesanPeringatan("Koneksi ke server gagal..!")
        End If

        TutupDatabasePublic()

        btn_SimpanPerubahanInfoInstaller.IsEnabled = False
        AdaPerubahanInfoInstaller = False

    End Sub

    Sub TampilkanDataInfoUpdater()

        Terabas()

        BukaDatabasePublic()

        If StatusKoneksiDatabasePublic Then
            cmdPublic = New MySqlCommand(" SELECT * FROM tbl_infoaplikasi ", KoneksiDatabasePublic)
            drPublic_ExecuteReader()
            drPublic.Read()
            txt_UrlPaketUpdater.Text = drPublic.Item("URL_Paket_Updater")
            txt_FolderTempPaketUpdater.Text = drPublic.Item("Folder_Temp_Paket_Updater")
            txt_FilePaketUpdater.Text = drPublic.Item("File_Paket_Updater")
            txt_FileUpdater.Text = drPublic.Item("File_Updater")
        Else
            ResetForm()
            PesanPeringatan("Koneksi ke server gagal..!")
        End If

        TutupDatabasePublic()

        btn_SimpanPerubahanInfoUpdater.IsEnabled = False
        AdaPerubahanInfoUpdater = False

    End Sub

    Sub ResetForm()
        ResetFormInfoGeneral()
        ResetFormInfoInstaller()
        ResetFormInfoUpdater()
    End Sub

    Sub ResetFormInfoGeneral()
        btn_SimpanPerubahanInfoGeneral.IsEnabled = False
        txt_VersiApp.Text = Kosongan
        txt_ApdetApp.Text = Kosongan
        txt_UrlPaketBooku.Text = Kosongan
        txt_FolderTempPaketBooku.Text = Kosongan
        txt_FilePaketBooku.Text = Kosongan
        Terabas()
    End Sub

    Sub ResetFormInfoInstaller()
        btn_SimpanPerubahanInfoInstaller.IsEnabled = False
        txt_UrlPaketInstaller.Text = Kosongan
        txt_FolderTempPaketInstaller.Text = Kosongan
        txt_FilePaketInstaller.Text = Kosongan
        txt_FileInstaller.Text = Kosongan
        Terabas()
    End Sub

    Sub ResetFormInfoUpdater()
        btn_SimpanPerubahanInfoUpdater.IsEnabled = False
        txt_UrlPaketUpdater.Text = Kosongan
        txt_FolderTempPaketUpdater.Text = Kosongan
        txt_FilePaketUpdater.Text = Kosongan
        txt_FileUpdater.Text = Kosongan
        Terabas()
    End Sub

    Sub PerubahanFormInfoGeneral()
        btn_SimpanPerubahanInfoGeneral.IsEnabled = True
        AdaPerubahanInfoGeneral = True
    End Sub

    Sub PerubahanFormInfoInstaller()
        btn_SimpanPerubahanInfoInstaller.IsEnabled = True
        AdaPerubahanInfoInstaller = True
    End Sub

    Sub PerubahanFormInfoUpdater()
        btn_SimpanPerubahanInfoUpdater.IsEnabled = True
        AdaPerubahanInfoUpdater = True
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Async Sub btn_Backup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Backup.Click
        Dim FolderPathBackup As String = "D:\VB .Net Project\BookuID\Booku\Backup dbPublic"
        PesanUntukProgrammer("Nama Database : " & NamaDatabase_BookuID_Booku_Public & Enter2Baris &
                             "User : " & UserDatabasePublic & Enter2Baris &
                             "Password : " & PasswordDatabasePublic & Enter2Baris &
                             "Host : " & LokasiServerDatabasePublic & Enter2Baris &
                             "Folder : " & FolderPathBackup)
        Await BackUpMySql_TanpaProgress(NamaDatabase_BookuID_Booku_Public, UserDatabasePublic, PasswordDatabasePublic, LokasiServerDatabasePublic, PortDatabasePublic, FolderPathBackup)
    End Sub


    Private Sub btn_Installer_Click(sender As Object, e As RoutedEventArgs) Handles btn_Installer.Click

        If AdaPerubahanInfoGeneral Or AdaPerubahanInfoInstaller Or AdaPerubahanInfoUpdater Then
            PesanPeringatan("Penerbitan Installer tidak dapat dijalankan sebelum data fix semua..!")
            Return
        End If

        win_PenerbitInstaller = New wpfWin_PenerbitInstaller
        win_PenerbitInstaller.ResetForm()
        win_PenerbitInstaller.VersiApp = VersiApp
        win_PenerbitInstaller.ApdetApp = ApdetApp
        win_PenerbitInstaller.urlPaketBooku = urlPaketBooku
        win_PenerbitInstaller.urlPaketInstaller = urlPaketInstaller
        win_PenerbitInstaller.NamaFolderTempPaketBooku = NamaFolderTempPaketBooku
        win_PenerbitInstaller.NamaFolderTempPaketInstaller = NamaFolderTempPaketInstaller
        win_PenerbitInstaller.NamaFileZipPaketBooku = NamaFileZipPaketBooku
        win_PenerbitInstaller.NamaFileZipPaketInstaller = NamaFileZipPaketInstaller
        win_PenerbitInstaller.NamaFileExeInstaller = NamaFileExeInstaller
        win_PenerbitInstaller.ShowDialog()

    End Sub


    Private Sub btn_Updater_Click(sender As Object, e As RoutedEventArgs) Handles btn_Updater.Click

        If AdaPerubahanInfoGeneral Or AdaPerubahanInfoInstaller Or AdaPerubahanInfoUpdater Then
            PesanPeringatan("Penerbitan Updater tidak dapat dijalankan sebelum data fix semua..!")
            Return
        End If

        win_PenerbitUpdater = New wpfWin_PenerbitUpdater
        win_PenerbitUpdater.ResetForm()
        win_PenerbitUpdater.VersiApp = VersiApp
        win_PenerbitUpdater.ApdetApp = ApdetApp
        win_PenerbitUpdater.urlPaketBooku = urlPaketBooku
        win_PenerbitUpdater.urlPaketUpdater = urlPaketUpdater
        win_PenerbitUpdater.NamaFolderTempPaketBooku = NamaFolderTempPaketBooku
        win_PenerbitUpdater.NamaFolderTempPaketUpdater = NamaFolderTempPaketUpdater
        win_PenerbitUpdater.NamaFileZipPaketBooku = NamaFileZipPaketBooku
        win_PenerbitUpdater.NamaFileZipPaketBookuAssistant = NamaFileZipPaketBookuAssistant
        win_PenerbitUpdater.NamaFileZipPaketBookuRemote = NamaFileZipPaketBookuRemote
        win_PenerbitUpdater.NamaFileZipPaketUpdater = NamaFileZipPaketUpdater
        win_PenerbitUpdater.NamaFileExeUpdater = NamaFileExeUpdater
        win_PenerbitUpdater.ShowDialog()

    End Sub



    'General :
    Private Sub txt_VersiApp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_VersiApp.TextChanged
        VersiApp = txt_VersiApp.Text
        PerubahanFormInfoGeneral()
    End Sub

    Private Sub txt_ApdetApp_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_ApdetApp.TextChanged
        ApdetApp = txt_ApdetApp.Text
        PerubahanFormInfoGeneral()
    End Sub

    Private Sub txt_UrlPaketBooku_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UrlPaketBooku.TextChanged
        urlPaketBooku = txt_UrlPaketBooku.Text
        PerubahanFormInfoGeneral()
    End Sub

    Private Sub txt_FolderTempPaketBooku_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_FolderTempPaketBooku.TextChanged
        NamaFolderTempPaketBooku = txt_FolderTempPaketBooku.Text
        PerubahanFormInfoGeneral()
    End Sub

    Private Sub txt_FilePaketBooku_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_FilePaketBooku.TextChanged
        NamaFileZipPaketBooku = txt_FilePaketBooku.Text
        PerubahanFormInfoGeneral()
    End Sub

    'Installer :
    Private Sub txt_UrlPaketInstaller_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UrlPaketInstaller.TextChanged
        urlPaketInstaller = txt_UrlPaketInstaller.Text
        PerubahanFormInfoInstaller()
    End Sub

    Private Sub txt_FolderTempPaketInstaller_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_FolderTempPaketInstaller.TextChanged
        NamaFolderTempPaketInstaller = txt_FolderTempPaketInstaller.Text
        PerubahanFormInfoInstaller()
    End Sub

    Private Sub txt_FilePaketInstaller_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_FilePaketInstaller.TextChanged
        NamaFileZipPaketInstaller = txt_FilePaketInstaller.Text
        PerubahanFormInfoInstaller()
    End Sub

    Private Sub txt_FileInstaller_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_FileInstaller.TextChanged
        NamaFileExeInstaller = txt_FileInstaller.Text
        PerubahanFormInfoInstaller()
    End Sub

    'Updater :
    Private Sub txt_UrlPaketUpdater_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UrlPaketUpdater.TextChanged
        urlPaketUpdater = txt_UrlPaketUpdater.Text
        PerubahanFormInfoUpdater()
    End Sub

    Private Sub txt_FolderTempPaketUpdater_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_FolderTempPaketUpdater.TextChanged
        NamaFolderTempPaketUpdater = txt_FolderTempPaketUpdater.Text
        PerubahanFormInfoUpdater()
    End Sub

    Private Sub txt_FilePaketUpdater_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_FilePaketUpdater.TextChanged
        NamaFileZipPaketUpdater = txt_FilePaketUpdater.Text
        PerubahanFormInfoUpdater()
    End Sub

    Private Sub txt_FileUpdater_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_FileUpdater.TextChanged
        NamaFileExeUpdater = txt_FileUpdater.Text
        PerubahanFormInfoUpdater()
    End Sub


    Private Sub btn_SimpanPerubahanInfoGeneral_Click(sender As Object, e As RoutedEventArgs) Handles btn_SimpanPerubahanInfoGeneral.Click

        btn_SimpanPerubahanInfoGeneral.IsEnabled = False
        btn_Refresh.IsEnabled = False
        Terabas()

        BukaDatabasePublic()
        cmdPublic = New MySqlCommand(" Update tbl_infoaplikasi SET " &
                                     " Versi_App                    = '" & VersiApp & "', " &
                                     " Apdet_App                    = '" & ApdetApp & "', " &
                                     " URL_Paket_Booku              = '" & urlPaketBooku & "', " &
                                     " Folder_Temp_Paket_Booku      = '" & NamaFolderTempPaketBooku & "', " &
                                     " File_Paket_Booku             = '" & NamaFileZipPaketBooku & "' ",
                                     KoneksiDatabasePublic)
        cmdPublic_ExecuteNonQuery()
        TutupDatabasePublic()

        If StatusKoneksiDatabasePublic = True Then
            PesanSukses("Penyimpanan Sukses")
            TampilkanDataInfoGeneral()
        Else
            PesanPeringatan("Penyimpanan Gagal..!")
            btn_SimpanPerubahanInfoGeneral.IsEnabled = True
        End If

        btn_Refresh.IsEnabled = True

    End Sub

    Private Sub btn_SimpanPerubahanInfoInstaller_Click(sender As Object, e As RoutedEventArgs) Handles btn_SimpanPerubahanInfoInstaller.Click

        btn_SimpanPerubahanInfoInstaller.IsEnabled = False
        btn_Refresh.IsEnabled = False
        Terabas()

        BukaDatabasePublic()
        cmdPublic = New MySqlCommand(" Update tbl_infoaplikasi SET " &
                                     " URL_Paket_Installer          = '" & urlPaketInstaller & "', " &
                                     " Folder_Temp_Paket_Installer  = '" & NamaFolderTempPaketInstaller & "', " &
                                     " File_Paket_Installer         = '" & NamaFileZipPaketInstaller & "', " &
                                     " File_Installer               = '" & NamaFileExeInstaller & "' ",
                                     KoneksiDatabasePublic)
        cmdPublic_ExecuteNonQuery()
        TutupDatabasePublic()

        If StatusKoneksiDatabasePublic = True Then
            PesanSukses("Penyimpanan Sukses")
            TampilkanDataInfoInstaller()
        Else
            PesanPeringatan("Penyimpanan Gagal..!")
            btn_SimpanPerubahanInfoInstaller.IsEnabled = True
        End If

        btn_Refresh.IsEnabled = True

    End Sub

    Private Sub btn_SimpanPerubahanInfoUpdater_Click(sender As Object, e As RoutedEventArgs) Handles btn_SimpanPerubahanInfoUpdater.Click

        btn_SimpanPerubahanInfoUpdater.IsEnabled = False
        btn_Refresh.IsEnabled = False
        Terabas()

        BukaDatabasePublic()
        cmdPublic = New MySqlCommand(" Update tbl_infoaplikasi SET " &
                                     " URL_Paket_Updater            = '" & urlPaketUpdater & "', " &
                                     " Folder_Temp_Paket_Updater    = '" & NamaFolderTempPaketUpdater & "', " &
                                     " File_Paket_Updater           = '" & NamaFileZipPaketUpdater & "', " &
                                     " File_Updater                 = '" & NamaFileExeUpdater & "' ",
                                     KoneksiDatabasePublic)
        cmdPublic_ExecuteNonQuery()
        TutupDatabasePublic()

        If StatusKoneksiDatabasePublic = True Then
            PesanSukses("Penyimpanan Sukses")
            TampilkanDataInfoUpdater()
        Else
            PesanPeringatan("Penyimpanan Gagal..!")
            btn_SimpanPerubahanInfoUpdater.IsEnabled = True
        End If

        btn_Refresh.IsEnabled = True

    End Sub



    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
