Option Explicit On
Option Strict On

Imports System.Runtime.InteropServices

''' <summary>
''' Modul untuk injeksi input keyboard dan mouse menggunakan Windows API SendInput.
''' Fase 2b: Kontrol Keyboard dan Mouse dari Tamu ke Host.
''' </summary>
Public Module mdl_InjeksiInput

#Region "Constants"

    ' Input type
    Private Const INPUT_MOUSE As UInteger = 0
    Private Const INPUT_KEYBOARD As UInteger = 1

    ' Mouse flags
    Private Const MOUSEEVENTF_MOVE As UInteger = &H1
    Private Const MOUSEEVENTF_LEFTDOWN As UInteger = &H2
    Private Const MOUSEEVENTF_LEFTUP As UInteger = &H4
    Private Const MOUSEEVENTF_RIGHTDOWN As UInteger = &H8
    Private Const MOUSEEVENTF_RIGHTUP As UInteger = &H10
    Private Const MOUSEEVENTF_MIDDLEDOWN As UInteger = &H20
    Private Const MOUSEEVENTF_MIDDLEUP As UInteger = &H40
    Private Const MOUSEEVENTF_XDOWN As UInteger = &H80
    Private Const MOUSEEVENTF_XUP As UInteger = &H100
    Private Const MOUSEEVENTF_WHEEL As UInteger = &H800
    Private Const MOUSEEVENTF_HWHEEL As UInteger = &H1000
    Private Const MOUSEEVENTF_ABSOLUTE As UInteger = &H8000

    ' Keyboard flags
    Private Const KEYEVENTF_EXTENDEDKEY As UInteger = &H1
    Private Const KEYEVENTF_KEYUP As UInteger = &H2

    ' System metrics
    Private Const SM_CXSCREEN As Integer = 0
    Private Const SM_CYSCREEN As Integer = 1

    ' XButton values
    Private Const XBUTTON1 As UInteger = &H1
    Private Const XBUTTON2 As UInteger = &H2

#End Region

#Region "Structures"

    ''' <summary>Struktur INPUT untuk SendInput API</summary>
    <StructLayout(LayoutKind.Sequential)>
    Private Structure INPUT
        Public type As UInteger
        Public union As INPUTUNION
    End Structure

    ''' <summary>Union untuk MOUSEINPUT dan KEYBDINPUT</summary>
    <StructLayout(LayoutKind.Explicit)>
    Private Structure INPUTUNION
        <FieldOffset(0)>
        Public mi As MOUSEINPUT
        <FieldOffset(0)>
        Public ki As KEYBDINPUT
    End Structure

    ''' <summary>Struktur untuk mouse input</summary>
    <StructLayout(LayoutKind.Sequential)>
    Private Structure MOUSEINPUT
        Public dx As Integer
        Public dy As Integer
        Public mouseData As UInteger
        Public dwFlags As UInteger
        Public time As UInteger
        Public dwExtraInfo As IntPtr
    End Structure

    ''' <summary>Struktur untuk keyboard input</summary>
    <StructLayout(LayoutKind.Sequential)>
    Private Structure KEYBDINPUT
        Public wVk As UShort
        Public wScan As UShort
        Public dwFlags As UInteger
        Public time As UInteger
        Public dwExtraInfo As IntPtr
    End Structure

#End Region

#Region "Windows API Declarations"

    ''' <summary>Inject input events</summary>
    <DllImport("user32.dll", SetLastError:=True)>
    Private Function SendInput(nInputs As UInteger, pInputs As INPUT(), cbSize As Integer) As UInteger
    End Function

    ''' <summary>Get system metrics</summary>
    <DllImport("user32.dll")>
    Private Function GetSystemMetrics(nIndex As Integer) As Integer
    End Function

    ''' <summary>Map virtual key to scan code</summary>
    <DllImport("user32.dll")>
    Private Function MapVirtualKey(uCode As UInteger, uMapType As UInteger) As UInteger
    End Function

#End Region

#Region "Public Properties"

    ''' <summary>Lebar layar dalam pixel</summary>
    Public ReadOnly Property LebarLayar As Integer
        Get
            Return GetSystemMetrics(SM_CXSCREEN)
        End Get
    End Property

    ''' <summary>Tinggi layar dalam pixel</summary>
    Public ReadOnly Property TinggiLayar As Integer
        Get
            Return GetSystemMetrics(SM_CYSCREEN)
        End Get
    End Property

#End Region

#Region "Keyboard Injection"

    ''' <summary>
    ''' Injeksi event keyboard.
    ''' </summary>
    ''' <param name="virtualKeyCode">Virtual key code (VK_*)</param>
    ''' <param name="isKeyDown">True untuk key down, False untuk key up</param>
    ''' <param name="isExtended">True jika extended key (arrows, numpad, dll)</param>
    Public Sub InjeksiKeyboard(virtualKeyCode As Integer, isKeyDown As Boolean, Optional isExtended As Boolean = False)
        Try
            Dim input As New INPUT()
            input.type = INPUT_KEYBOARD
            input.union.ki.wVk = CUShort(virtualKeyCode)
            input.union.ki.wScan = CUShort(MapVirtualKey(CUInt(virtualKeyCode), 0))
            input.union.ki.time = 0
            input.union.ki.dwExtraInfo = IntPtr.Zero

            ' Set flags
            Dim flags As UInteger = 0
            If isExtended Then
                flags = flags Or KEYEVENTF_EXTENDEDKEY
            End If
            If Not isKeyDown Then
                flags = flags Or KEYEVENTF_KEYUP
            End If
            input.union.ki.dwFlags = flags

            ' Send input
            Dim inputs() As INPUT = {input}
            SendInput(1, inputs, Marshal.SizeOf(GetType(INPUT)))

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error InjeksiKeyboard: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Injeksi kombinasi key press (down + up).
    ''' </summary>
    Public Sub InjeksiKeyPress(virtualKeyCode As Integer, Optional isExtended As Boolean = False)
        InjeksiKeyboard(virtualKeyCode, True, isExtended)
        InjeksiKeyboard(virtualKeyCode, False, isExtended)
    End Sub

#End Region

#Region "Mouse Injection"

    ''' <summary>
    ''' Injeksi gerakan mouse ke posisi absolut (normalized 0-1).
    ''' </summary>
    ''' <param name="normalizedX">Posisi X (0.0 - 1.0)</param>
    ''' <param name="normalizedY">Posisi Y (0.0 - 1.0)</param>
    Public Sub InjeksiMouseMove(normalizedX As Double, normalizedY As Double)
        Try
            ' Convert normalized coordinates to absolute (0-65535)
            ' SendInput dengan MOUSEEVENTF_ABSOLUTE menggunakan range 0-65535
            Dim absoluteX As Integer = CInt(normalizedX * 65535)
            Dim absoluteY As Integer = CInt(normalizedY * 65535)

            Dim input As New INPUT()
            input.type = INPUT_MOUSE
            input.union.mi.dx = absoluteX
            input.union.mi.dy = absoluteY
            input.union.mi.mouseData = 0
            input.union.mi.dwFlags = MOUSEEVENTF_MOVE Or MOUSEEVENTF_ABSOLUTE
            input.union.mi.time = 0
            input.union.mi.dwExtraInfo = IntPtr.Zero

            Dim inputs() As INPUT = {input}
            SendInput(1, inputs, Marshal.SizeOf(GetType(INPUT)))

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error InjeksiMouseMove: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Injeksi klik mouse.
    ''' </summary>
    ''' <param name="button">1=Left, 2=Right, 3=Middle, 4=XButton1, 5=XButton2</param>
    ''' <param name="isDown">True untuk mouse down, False untuk mouse up</param>
    ''' <param name="normalizedX">Posisi X (0.0 - 1.0), opsional</param>
    ''' <param name="normalizedY">Posisi Y (0.0 - 1.0), opsional</param>
    Public Sub InjeksiMouseClick(button As Integer, isDown As Boolean,
                                  Optional normalizedX As Double = -1,
                                  Optional normalizedY As Double = -1)
        Try
            ' Move mouse first if coordinates provided
            If normalizedX >= 0 AndAlso normalizedY >= 0 Then
                InjeksiMouseMove(normalizedX, normalizedY)
            End If

            Dim input As New INPUT()
            input.type = INPUT_MOUSE
            input.union.mi.dx = 0
            input.union.mi.dy = 0
            input.union.mi.time = 0
            input.union.mi.dwExtraInfo = IntPtr.Zero

            ' Set button flags
            Select Case button
                Case 1 ' Left
                    input.union.mi.dwFlags = If(isDown, MOUSEEVENTF_LEFTDOWN, MOUSEEVENTF_LEFTUP)
                    input.union.mi.mouseData = 0

                Case 2 ' Right
                    input.union.mi.dwFlags = If(isDown, MOUSEEVENTF_RIGHTDOWN, MOUSEEVENTF_RIGHTUP)
                    input.union.mi.mouseData = 0

                Case 3 ' Middle
                    input.union.mi.dwFlags = If(isDown, MOUSEEVENTF_MIDDLEDOWN, MOUSEEVENTF_MIDDLEUP)
                    input.union.mi.mouseData = 0

                Case 4 ' XButton1
                    input.union.mi.dwFlags = If(isDown, MOUSEEVENTF_XDOWN, MOUSEEVENTF_XUP)
                    input.union.mi.mouseData = XBUTTON1

                Case 5 ' XButton2
                    input.union.mi.dwFlags = If(isDown, MOUSEEVENTF_XDOWN, MOUSEEVENTF_XUP)
                    input.union.mi.mouseData = XBUTTON2

                Case Else
                    Return
            End Select

            Dim inputs() As INPUT = {input}
            SendInput(1, inputs, Marshal.SizeOf(GetType(INPUT)))

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error InjeksiMouseClick: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Injeksi scroll mouse wheel.
    ''' </summary>
    ''' <param name="delta">Scroll amount (positive=up, negative=down). Standard: 120 per notch.</param>
    ''' <param name="normalizedX">Posisi X (0.0 - 1.0), opsional</param>
    ''' <param name="normalizedY">Posisi Y (0.0 - 1.0), opsional</param>
    Public Sub InjeksiMouseWheel(delta As Integer,
                                  Optional normalizedX As Double = -1,
                                  Optional normalizedY As Double = -1)
        Try
            ' Move mouse first if coordinates provided
            If normalizedX >= 0 AndAlso normalizedY >= 0 Then
                InjeksiMouseMove(normalizedX, normalizedY)
            End If

            Dim input As New INPUT()
            input.type = INPUT_MOUSE
            input.union.mi.dx = 0
            input.union.mi.dy = 0
            input.union.mi.mouseData = CUInt(delta)
            input.union.mi.dwFlags = MOUSEEVENTF_WHEEL
            input.union.mi.time = 0
            input.union.mi.dwExtraInfo = IntPtr.Zero

            Dim inputs() As INPUT = {input}
            SendInput(1, inputs, Marshal.SizeOf(GetType(INPUT)))

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error InjeksiMouseWheel: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Injeksi scroll horizontal mouse wheel.
    ''' </summary>
    Public Sub InjeksiMouseHorizontalWheel(delta As Integer,
                                            Optional normalizedX As Double = -1,
                                            Optional normalizedY As Double = -1)
        Try
            ' Move mouse first if coordinates provided
            If normalizedX >= 0 AndAlso normalizedY >= 0 Then
                InjeksiMouseMove(normalizedX, normalizedY)
            End If

            Dim input As New INPUT()
            input.type = INPUT_MOUSE
            input.union.mi.dx = 0
            input.union.mi.dy = 0
            input.union.mi.mouseData = CUInt(delta)
            input.union.mi.dwFlags = MOUSEEVENTF_HWHEEL
            input.union.mi.time = 0
            input.union.mi.dwExtraInfo = IntPtr.Zero

            Dim inputs() As INPUT = {input}
            SendInput(1, inputs, Marshal.SizeOf(GetType(INPUT)))

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Error InjeksiMouseHorizontalWheel: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "High-Level Mouse Functions"

    ''' <summary>
    ''' Proses payload input mouse dari Tamu.
    ''' </summary>
    Public Sub ProsesInputMouse(input As cls_PayloadInputMouse)
        If input Is Nothing Then Return

        Select Case input.TipeAksi
            Case TipeAksiMouse.PINDAH
                InjeksiMouseMove(input.X, input.Y)

            Case TipeAksiMouse.KLIK
                InjeksiMouseClick(input.Button, input.IsButtonDown, input.X, input.Y)

            Case TipeAksiMouse.RODA
                InjeksiMouseWheel(input.WheelDelta, input.X, input.Y)
        End Select
    End Sub

#End Region

#Region "Utility"

    ''' <summary>
    ''' Convert normalized coordinates (0-1) ke pixel coordinates.
    ''' </summary>
    Public Function DenormalizeX(normalizedX As Double) As Integer
        Return CInt(normalizedX * LebarLayar)
    End Function

    ''' <summary>
    ''' Convert normalized coordinates (0-1) ke pixel coordinates.
    ''' </summary>
    Public Function DenormalizeY(normalizedY As Double) As Integer
        Return CInt(normalizedY * TinggiLayar)
    End Function

#End Region

End Module
