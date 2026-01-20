Option Explicit On
Option Strict On

Imports System.Diagnostics
Imports System.Threading
Imports System.Windows

''' <summary>
''' Entry point utama aplikasi Booku Remote dengan WPF Application class.
''' Fitur:
''' - Single instance protection (Mutex)
''' - Global exception handlers
''' - Manual startup control
''' </summary>
Partial Public Class App
    Inherits System.Windows.Application

    Private Shared MutexApp As Mutex

#Region "Main Entry Point"

    ''' <summary>
    ''' Entry point utama aplikasi. Diperlukan untuk VB.NET WPF.
    ''' </summary>
    <STAThread>
    Public Shared Sub Main()
        Dim app As New App()
        app.Run()
    End Sub

#End Region

#Region "Instance Constructor"

    ''' <summary>
    ''' Instance constructor - load XAML resources.
    ''' </summary>
    Public Sub New()
        ' Panggil InitializeComponent untuk load XAML resources dari Application.xaml
        InitializeComponent()

        ' Backup load resources jika XAML tidak memuat dengan benar
        If Me.Resources.MergedDictionaries.Count = 0 Then
            Dim sharedStyles As New ResourceDictionary()
            sharedStyles.Source = New Uri("pack://application:,,,/BookuID.Styles;component/WPF/Styles/StyleAplikasi.xaml", UriKind.Absolute)
            Me.Resources.MergedDictionaries.Add(sharedStyles)
        End If
    End Sub

#End Region

#Region "Static Constructor (Mutex)"

    ''' <summary>
    ''' Static constructor - dipanggil SEBELUM instance Application dibuat.
    ''' Tempat ideal untuk Mutex.
    ''' </summary>
    Shared Sub New()
        ' Single Instance Protection
        Dim appName As String = "BookuRemoteSingleInstance"
        Dim createdNew As Boolean
        MutexApp = New Mutex(True, appName, createdNew)

        If Not createdNew Then
            ' Aplikasi sudah berjalan, fokuskan window yang ada lalu keluar
            FocusExistingApp()
            Environment.Exit(0)
        End If

        ' Setup exception handler
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf Domain_UnhandledException
    End Sub

#End Region

#Region "Application Lifecycle"

    ''' <summary>
    ''' Dipanggil saat aplikasi startup.
    ''' </summary>
    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        MyBase.OnStartup(e)

        ' Setup WPF Dispatcher Exception Handler
        AddHandler Me.DispatcherUnhandledException, AddressOf Wpf_DispatcherUnhandledException

        ' Show Main Window
        Dim mainWindow As New wpfWin_StartUp()
        Me.MainWindow = mainWindow
        mainWindow.Show()
    End Sub

    ''' <summary>
    ''' Dipanggil saat aplikasi exit. Cleanup resources.
    ''' </summary>
    Protected Overrides Sub OnExit(e As ExitEventArgs)
        ' Cleanup jaringan sebelum exit
        Try
            mdl_PenemuanPerangkat.HentikanDiscovery()
            mdl_KoneksiJaringan.HentikanServer()
        Catch
        End Try

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
        ShowError(TryCast(e.ExceptionObject, Exception))
    End Sub

    Private Sub Wpf_DispatcherUnhandledException(sender As Object, e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs)
        ShowError(e.Exception)
        e.Handled = True
    End Sub

    Private Shared Sub ShowError(ex As Exception)
        Try
            MessageBox.Show(
                "Terjadi kesalahan:" & Environment.NewLine &
                If(ex IsNot Nothing, ex.Message, "Unknown error"),
                "Booku Remote - Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error)
        Catch
        End Try
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
