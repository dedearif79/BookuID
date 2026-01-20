Imports System.Windows
Imports System.Windows.Interop
Imports System.Runtime.InteropServices

Public Class cls_WindowDialogWPF_TanpaX

    Inherits Window

    Sub Styling(Window As Window)
        Window.WindowStyle = WindowStyle.ToolWindow
        Dim hwnd = New WindowInteropHelper(Window).Handle
        Dim currentStyle As Integer = GetWindowLong(hwnd, GWL_STYLE)
        SetWindowLong(hwnd, GWL_STYLE, currentStyle And Not WS_SYSMENU)
    End Sub

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    End Function

    Private Const GWL_STYLE As Integer = -16
    Private Const WS_SYSMENU As Integer = &H80000

End Class
