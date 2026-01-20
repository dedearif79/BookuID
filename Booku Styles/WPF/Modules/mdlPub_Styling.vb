Imports System.Windows

Public Module mdlPub_Styling

    'Style :
    Public WindowDialogWPF_TanpaX As New cls_WindowDialogWPF_TanpaX

    Sub StyleWindowDialogWPF_Dasar(Jendela As Window)
        Jendela.SizeToContent = SizeToContent.WidthAndHeight
        Jendela.WindowStartupLocation = WindowStartupLocation.CenterScreen
        Jendela.WindowStyle = WindowStyle.SingleBorderWindow
        Jendela.ResizeMode = ResizeMode.NoResize
        AddHandler Jendela.SizeChanged,
            Sub()
                Jendela.Left = (SystemParameters.PrimaryScreenWidth - Jendela.ActualWidth) / 2
                Jendela.Top = (SystemParameters.PrimaryScreenHeight - Jendela.ActualHeight) / 2
            End Sub
    End Sub

    Sub StyleWindowDialogWPF_TanpaTombolX(Jendela As Window)
        WindowDialogWPF_TanpaX.Styling(Jendela)
    End Sub

    Sub StyleWindowDialogWPF_Sizable(Jendela As Window)
        Jendela.ResizeMode = ResizeMode.CanResize
    End Sub

End Module
