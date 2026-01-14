Imports System.IO
Imports System.Text

Module mdl_Logger

    Private ReadOnly LogDir As String =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")

    Public Function WriteException(ex As Exception, source As String) As String
        Directory.CreateDirectory(LogDir)

        Dim logPath = Path.Combine(LogDir, $"crash_{DateTime.Now:yyyyMMdd_HHmmss}.log")

        Dim sb As New StringBuilder()
        sb.AppendLine($"Time   : {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        sb.AppendLine($"Source : {source}")
        Dim asm = Reflection.Assembly.GetExecutingAssembly()
        sb.AppendLine($"App    : {asm.GetName().Name} {asm.GetName().Version}")
        sb.AppendLine($"OS     : {Environment.OSVersion}")
        sb.AppendLine($"User   : {Environment.UserName}")
        sb.AppendLine(New String("-"c, 60))
        sb.AppendLine(If(ex?.ToString(), "Exception is Nothing"))

        File.WriteAllText(logPath, sb.ToString(), Encoding.UTF8)
        Return logPath
    End Function

End Module
