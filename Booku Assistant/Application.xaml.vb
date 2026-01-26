Option Explicit On
Option Strict On

Imports System.Threading
Imports System.Runtime.InteropServices

''' <summary>
''' Entry point aplikasi Booku Assistant dengan Single Instance protection.
''' </summary>
Partial Public Class App
    Inherits Application

    Private Shared MutexApp As Mutex

#Region "Main Entry Point"

    ''' <summary>
    ''' Entry point utama aplikasi. Diperlukan untuk VB.NET WPF.
    ''' </summary>
    <STAThread>
    Public Shared Sub Main()
        Dim app As New App()
        app.InitializeComponent()
        app.Run(New wpfWin_StartUp())
    End Sub

#End Region

#Region "Static Constructor (Single Instance)"

    ''' <summary>
    ''' Static constructor - dipanggil SEBELUM instance Application dibuat.
    ''' Tempat ideal untuk Mutex (Single Instance protection).
    ''' </summary>
    Shared Sub New()
        ' Single Instance Protection menggunakan Mutex
        Dim appName As String = "BookuAssistantSingleInstance"
        Dim createdNew As Boolean
        MutexApp = New Mutex(True, appName, createdNew)

        If Not createdNew Then
            ' Aplikasi sudah berjalan, fokuskan window yang ada lalu keluar
            FocusExistingApp()
            Environment.Exit(0)
        End If
    End Sub

#End Region

#Region "Application Lifecycle"

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

#Region "Exception Handler"

    Private Sub Application_DispatcherUnhandledException(sender As Object,
           e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs) _
           Handles Me.DispatcherUnhandledException

        e.Handled = True
        MessageBox.Show("Terjadi kesalahan pada tampilan WPF. Silakan cek folder Logs.", "Error")
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
    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As Integer) As Boolean
    End Function

    Private Const SW_RESTORE As Integer = 9

#End Region

End Class
