Option Explicit On
Option Strict On

Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Threading
Imports bcomm

''' <summary>
''' Entry point utama aplikasi BOOKU dengan WPF Application class.
''' Menggabungkan fitur:
''' - Single instance protection (Mutex)
''' - Global exception handlers (AppDomain, Task, WPF Dispatcher)
''' - Startup logic (login, data loading)
''' - WPF Application lifecycle
''' </summary>
Partial Public Class App
    Inherits Application

    Private Shared MutexApp As Mutex

#Region "Instance Constructor"

    ''' <summary>
    ''' Instance constructor - manual load XAML resources karena InitializeComponent() tidak di-generate.
    ''' </summary>
    Public Sub New()
        ' Manual load resources dari StyleAplikasi.xaml
        ' Karena WPF SDK tidak generate InitializeComponent() dengan MyType setting
        Dim resourceDict As New ResourceDictionary()
        resourceDict.Source = New Uri("pack://application:,,,/Booku;component/WPF/Styles/StyleAplikasi.xaml", UriKind.Absolute)
        Me.Resources.MergedDictionaries.Add(resourceDict)
    End Sub

#End Region

#Region "Static Constructor (Mutex & AppDomain Exception)"

    ''' <summary>
    ''' Static constructor - dipanggil SEBELUM instance Application dibuat.
    ''' Tempat ideal untuk Mutex dan AppDomain exception handler.
    ''' </summary>
    Shared Sub New()
        ' 1. Single Instance Protection
        Dim appName As String = "BookuSingleInstance"
        Dim createdNew As Boolean
        MutexApp = New Mutex(True, appName, createdNew)

        If Not createdNew Then
            ' Aplikasi sudah berjalan, fokuskan window yang ada lalu keluar
            FocusExistingApp()
            Environment.Exit(0)
        End If

        ' 2. Setup AppDomain Exception Handler (paling awal)
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf Domain_UnhandledException
        AddHandler TaskScheduler.UnobservedTaskException, AddressOf Task_UnobservedTaskException
    End Sub

#End Region

#Region "Application Lifecycle"

    ''' <summary>
    ''' Dipanggil saat aplikasi startup. Resources sudah auto-load dari XAML.
    ''' </summary>
    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        MyBase.OnStartup(e)

        ' 1. Setup WPF Dispatcher Exception Handler
        AddHandler Me.DispatcherUnhandledException, AddressOf Wpf_DispatcherUnhandledException
        Me.ShutdownMode = ShutdownMode.OnMainWindowClose

        ' =====================================================
        ' 2. STARTUP LOGIC (sebelum main window ditampilkan)
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
        ' 3. Show Main Window
        ' =====================================================
        Me.MainWindow = win_BOOKU
        win_BOOKU.Show()
    End Sub

    ''' <summary>
    ''' Dipanggil saat aplikasi exit. Cleanup resources.
    ''' </summary>
    Protected Overrides Sub OnExit(e As ExitEventArgs)
        ' Release Mutex
        If MutexApp IsNot Nothing Then
            Try
                MutexApp.ReleaseMutex()
            Catch
                ' Ignore - mungkin sudah released atau tidak owned
            End Try
        End If

        MyBase.OnExit(e)
    End Sub

#End Region

#Region "Exception Handlers"

    Private Shared Sub Domain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
        ShowAndExit(TryCast(e.ExceptionObject, Exception), "AppDomain UnhandledException")
    End Sub

    Private Shared Sub Task_UnobservedTaskException(sender As Object, e As UnobservedTaskExceptionEventArgs)
        ShowAndExit(e.Exception, "Task UnobservedTaskException")
        e.SetObserved()
    End Sub

    Private Sub Wpf_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs)
        ShowAndExit(e.Exception, "WPF DispatcherUnhandledException")
        e.Handled = True
    End Sub

    Private Shared Sub ShowAndExit(ex As Exception, source As String)
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

    Private Shared Sub FocusExistingApp()
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
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    <Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    Private Const SW_RESTORE As Integer = 9

#End Region

End Class
