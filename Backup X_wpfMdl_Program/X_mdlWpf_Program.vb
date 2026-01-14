Option Explicit On
Option Strict On

Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Threading
Imports bcomm

''' <summary>
''' Entry point utama aplikasi BOOKU dengan WPF Application.
''' Menggabungkan fitur:
''' - Single instance protection (Mutex)
''' - Global exception handlers (AppDomain, Task, WPF Dispatcher)
''' - Startup logic (login, data loading)
''' - WPF Application lifecycle
''' </summary>
Public Module mdlWpf_Program

    Private MutexApp As Mutex

    <STAThread>
    Sub Main()
        ' 1. Single Instance Protection
        Dim appName As String = "BookuSingleInstance"
        Dim createdNew As Boolean
        MutexApp = New Mutex(True, appName, createdNew)

        If Not createdNew Then
            ' Aplikasi sudah berjalan, fokuskan window yang ada lalu keluar
            FocusExistingApp()
            Return
        End If

        ' 2. Setup Global Exception Handlers
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf Domain_UnhandledException
        AddHandler TaskScheduler.UnobservedTaskException, AddressOf Task_UnobservedTaskException

        ' 3. Create WPF Application
        Dim app As New Application()
        AddHandler app.DispatcherUnhandledException, AddressOf Wpf_DispatcherUnhandledException
        app.ShutdownMode = ShutdownMode.OnMainWindowClose

        ' 4. Load Application Resources (StyleAplikasi.xaml)
        Dim resourceDict As New ResourceDictionary()
        resourceDict.Source = New Uri("pack://application:,,,/Booku;component/WPF/Styles/StyleAplikasi.xaml", UriKind.Absolute)
        app.Resources.MergedDictionaries.Add(resourceDict)

        ' =====================================================
        ' 5. STARTUP LOGIC (sebelum main window ditampilkan)
        ' =====================================================

        ' Parameter Awal :
        FilePathDataKoneksi = Path.Combine(FolderNotesApp, NamaFileDataKoneksi)
        FilePathRegistrasiPerangkat = Path.Combine(FolderNotesApp, NamaFileRegistrasiPerangkat)
        FilePathRegistrasiPerangkat_Backup = Path.Combine(FolderNotesApp, NamaFileRegistrasiPerangkat_Backup)
        FilePathVersiDanApdetAplikasi = Path.Combine(FolderNotesApp, NamaFileVersiDanApdetAplikasi)

        ' Standarisasi Settingan :
        StandarisasiSetinganAplikasi()

        ' Start Up (Login Dialog)
        win_BOOKU = New wpfWin_BOOKU()
        win_Startup = New wpfWin_StartUp
        win_Startup.ShowDialog()

        ' Update Info Aplikasi dari server
        UpdateInfoAplikasi()

        ' Pengisian Value dari Variabel-variabel Penting di Awal :
        DataAwalLoadingAplikasi()

        ' Cek Versi dan Apdet Aplikasi :
        CekVersiDanApdetAplikasi()

        ' Cek Status Registrasi Perangkat :
        CekStatusRegistrasiPerangkat()

        ' Set flag agar wpfWin_BOOKU tidak menjalankan startup lagi
        StartupSudahDijalankan = True

        ' =====================================================
        ' 6. Run WPF Application
        ' =====================================================
        app.Run(win_BOOKU)

        ' 7. Cleanup
        MutexApp.ReleaseMutex()
    End Sub

#Region "Exception Handlers"

    Private Sub Domain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
        ShowAndExit(TryCast(e.ExceptionObject, Exception), "AppDomain UnhandledException")
    End Sub

    Private Sub Task_UnobservedTaskException(sender As Object, e As UnobservedTaskExceptionEventArgs)
        ShowAndExit(e.Exception, "Task UnobservedTaskException")
        e.SetObserved()
    End Sub

    Private Sub Wpf_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs)
        ShowAndExit(e.Exception, "WPF DispatcherUnhandledException")
        e.Handled = True
    End Sub

    Private Sub ShowAndExit(ex As Exception, source As String)
        Dim logPath As String = ""
        Try
            logPath = mdl_Logger.WriteException(ex, source)
        Catch
        End Try

        Try
            MessageBox.Show(
                "Maaf, terjadi kesalahan dan aplikasi harus berhenti." & Environment.NewLine &
                If(logPath <> "", "Silakan kirim file log ini ke admin:" & Environment.NewLine & logPath,
                                "Silakan cek folder Logs dan kirim file log terbaru ke admin."),
                "BOOKU - Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        Catch
        End Try

        Environment.Exit(1)
    End Sub

#End Region

#Region "Single Instance Helper"

    Private Sub FocusExistingApp()
        ' Fokus ke aplikasi yang sudah berjalan
        Dim proc As Process = Process.GetCurrentProcess()
        Dim processes As Process() = Process.GetProcessesByName(proc.ProcessName)

        For Each p As Process In processes
            If p.Id <> proc.Id Then
                ShowWindow(p.MainWindowHandle, SW_RESTORE)
                SetForegroundWindow(p.MainWindowHandle)
                Exit For
            End If
        Next
    End Sub

    ' Deklarasi fungsi API Windows untuk memfokuskan window
    <Runtime.InteropServices.DllImport("user32.dll")>
    Private Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    <Runtime.InteropServices.DllImport("user32.dll")>
    Private Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    Private Const SW_RESTORE As Integer = 9

#End Region

End Module
