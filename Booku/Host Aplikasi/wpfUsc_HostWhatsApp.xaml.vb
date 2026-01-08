Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Windows.Forms.Integration

Public Class wpfUsc_HostWhatsApp

    <DllImport("user32.dll")>
    Private Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    End Function

    Private Sub EmbedExternalApp()
        ' Buat Panel untuk menampung aplikasi
        Dim panel As New System.Windows.Forms.Panel()
        Dim host As New WindowsFormsHost()
        host.Child = panel
        Me.MainGrid.Children.Add(host) ' MainGrid adalah Grid di XAML yang menjadi parent

        ' Jalankan aplikasi eksternal
        Dim proc As Process = Process.Start("notepad.exe")
        proc.WaitForInputIdle()

        ' Set parent aplikasi agar masuk ke dalam panel
        SetParent(proc.MainWindowHandle, panel.Handle)
    End Sub

End Class
