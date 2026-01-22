Class wpfWin_StartUp

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Set konten setiap tab ke UserControl masing-masing
        konten_Beranda.Content = usc_Beranda
        konten_Aplikasi.Content = usc_Aplikasi
        konten_Pengaturan.Content = usc_Pengaturan
    End Sub

#Region "Window Control Buttons"

    Private Sub btn_Minimize_Click(sender As Object, e As RoutedEventArgs) Handles btn_Minimize.Click
        Me.WindowState = WindowState.Minimized
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As RoutedEventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub Header_MouseLeftButtonDown(sender As Object, e As Input.MouseButtonEventArgs)
        ' Memungkinkan drag window dari header
        If e.LeftButton = Input.MouseButtonState.Pressed Then
            Me.DragMove()
        End If
    End Sub

#End Region

End Class
