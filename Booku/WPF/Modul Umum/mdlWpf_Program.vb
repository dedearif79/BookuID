Option Explicit On
Option Strict On

Imports System.Threading

Public Module mdlWpf_Program


    Private MutexApp As Mutex

    Sub Main()
        Dim appName As String = "MyUniqueAppMutex"
        Dim createdNew As Boolean
        MutexApp = New Mutex(True, appName, createdNew) 'Membuat Mutex untuk mencegah lebih dari satu instance berjalan
        If Not createdNew Then 'Jika aplikasi sudah berjalan, fokuskan window yang ada lalu keluar
            FocusExistingApp()
            Return
        End If
        ' Menjalankan aplikasi utama (WinForms)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New frm_BOOKU())
        MutexApp.ReleaseMutex() 'Melepaskan Mutex setelah aplikasi ditutup
    End Sub
    Private Sub FocusExistingApp() 'Fokus ke aplikasi yang sudah berjalan
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

End Module
