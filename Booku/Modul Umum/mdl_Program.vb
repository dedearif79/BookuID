Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Module mdl_Program

    ' =============================================================================
    ' NOTE: Sub Main dipindahkan ke mdlWpf_Program.vb untuk mendukung WPF Application.
    ' Modul ini tetap dipertahankan untuk referensi dan compatibility.
    ' Kode di bawah ini di-comment out karena sudah tidak digunakan.
    ' =============================================================================

    '<STAThread>
    'Sub Main()
    '    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)

    '    AddHandler Application.ThreadException, AddressOf WinForms_ThreadException
    '    AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf Domain_UnhandledException
    '    AddHandler TaskScheduler.UnobservedTaskException, AddressOf Task_UnobservedTaskException

    '    Application.EnableVisualStyles()
    '    Application.SetCompatibleTextRenderingDefault(False)

    '    Application.Run(New frm_BOOKU())
    'End Sub

    'Private Sub WinForms_ThreadException(sender As Object, e As ThreadExceptionEventArgs)
    '    ShowAndExit(e.Exception, "WinForms ThreadException")
    'End Sub

    'Private Sub Domain_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
    '    ShowAndExit(TryCast(e.ExceptionObject, Exception), "AppDomain UnhandledException")
    'End Sub

    'Private Sub Task_UnobservedTaskException(sender As Object, e As UnobservedTaskExceptionEventArgs)
    '    ShowAndExit(e.Exception, "Task UnobservedTaskException")
    '    e.SetObserved()
    'End Sub

    'Private Sub ShowAndExit(ex As Exception, source As String)
    '    Dim logPath As String = ""
    '    Try
    '        logPath = mdl_Logger.WriteException(ex, source)
    '    Catch
    '    End Try

    '    Try
    '        MessageBox.Show(
    '            "Maaf, terjadi kesalahan dan aplikasi harus berhenti." & Environment.NewLine &
    '            If(logPath <> "", "Silakan kirim file log ini ke admin:" & Environment.NewLine & logPath,
    '                            "Silakan cek folder Logs dan kirim file log terbaru ke admin."),
    '            "BOOKU - Error",
    '            MessageBoxButtons.OK,
    '            MessageBoxIcon.Error)
    '    Catch
    '    End Try

    '    Environment.Exit(1)
    'End Sub

End Module
