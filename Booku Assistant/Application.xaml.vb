Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_DispatcherUnhandledException(sender As Object,
           e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs) _
           Handles Me.DispatcherUnhandledException

        ' Log + tampilkan info singkat
        ' Anda bisa panggil fungsi logger yang sama seperti di WinForms
        ' (misalnya buat Logger module terpisah)
        e.Handled = True
        MessageBox.Show("Terjadi kesalahan pada tampilan WPF. Silakan cek folder Logs.", "Error")
    End Sub

End Class
