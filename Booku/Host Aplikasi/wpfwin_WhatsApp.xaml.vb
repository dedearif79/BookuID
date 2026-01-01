Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Forms
Imports System.Windows.Forms.Integration
Imports bcomm


Public Class wpfwin_WhatsApp

    Dim FilePathExe As String = "notepad.exe"

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        EmbedExternalApp()
    End Sub


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
        Dim proc As Process = Process.Start(FilePathExe )
        proc.WaitForInputIdle()

        ' Set parent aplikasi agar masuk ke dalam panel
        SetParent(proc.MainWindowHandle, panel.Handle)
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub


End Class
