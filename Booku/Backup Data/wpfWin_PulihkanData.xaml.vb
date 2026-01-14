Imports System.IO
Imports System.Text
Imports System.Windows
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports bcomm
Imports DocumentFormat.OpenXml.Drawing.Diagrams
Imports MySql.Data.MySqlClient
Imports SixLabors.Fonts.Tables.General


Public Class wpfWin_PulihkanData


    Dim ProsesPulihkanData As Boolean
    Dim PenambahanProgress As String
    Dim ProsentaseProgress As Integer
    Dim PulihkanDatabaseBerhasil As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        PembatasanHakAkses(Me)

        ProsesLoadingForm = True

        VisibilitasProgressBar(False)
        btn_Pulihkan.Visibility = Visibility.Visible
        btn_Tutup.Visibility = Visibility.Visible
        Terabas()

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()
        btn_Pulihkan.IsEnabled = True
        pgb_Progress.Value = 0
        lbl_Progress.Text = Kosongan
    End Sub

    Sub VisibilitasProgressBar(Visibilitas As Boolean)
        If Visibilitas Then
            pgb_Progress.Visibility = Visibility.Visible
            lbl_Progress.Visibility = Visibility.Visible
        Else
            pgb_Progress.Visibility = Visibility.Collapsed
            lbl_Progress.Visibility = Visibility.Collapsed
        End If
    End Sub



    Sub LogikaProsesPulihkanData(ProsesBerhasil As Boolean)
        If ProsesBerhasil Then
            ProsesPulihkanData = True
            pgb_Progress.Value += PenambahanProgress
            ProsentaseProgress += PenambahanProgress
            lbl_Progress.Text = "Proses pemulihan... " & ProsentaseProgress & " %"
        Else
            ProsesPulihkanData = False
        End If
    End Sub

    Dim KoneksiBackUp As MySqlConnection
    Dim cmdBackUp As MySqlCommand
    Dim strBackUp As String

    Sub TahapanPemulihan()

        Dim PesanPertanyaan As String = "Anda akan melakukan pemulihan database."
        Pilihan = MessageBox.Show(PesanPertanyaan & Enter2Baris & "Lanjutkan proses..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        VisibilitasProgressBar(True)

        ProsesPulihkanData = True
        pgb_Progress.Value = 0
        ProsentaseProgress = 0

        lbl_Progress.Text = "Proses pemulihan... 0 %"
        Jeda(333)

        Dim JumlahDatabase = 0

        Dim DaftarFileBackup() As String = Directory.GetFiles(FolderCompany_Backup_MySQL)

        For Each filePath As String In DaftarFileBackup
            If AmbilTeksKanan(filePath, 3) = "sql" Then JumlahDatabase += 1
        Next

        PenambahanProgress = 100 / JumlahDatabase

        connStr =
            "Data Source    = " & LokasiServerDatabase & ";" &
            "port           = " & PortDatabase & ";" &
            "username       = " & UserDatabase & ";" &
            "password       = " & PasswordDatabase & ";" &
            "SSL Mode       = None"

        For Each filePath As String In DaftarFileBackup
            If AmbilTeksKanan(filePath, 3) = "sql" Then
                ProsesPemulihanData(filePath)
                LogikaProsesPulihkanData(PulihkanDatabaseBerhasil)
                Terabas()
                Jeda(333)
            End If
        Next

        If ProsesPulihkanData = True Then
            pgb_Progress.Value = 100
            lbl_Progress.Text = "Pemulihan database berhasil."
            btn_Pulihkan.Visibility = Visibility.Collapsed
            btn_Tutup.Visibility = Visibility.Visible
            Terabas()
            Jeda(999)
        Else
            pgb_Progress.Value = 0
            lbl_Progress.Text = "Pemulihan database gagal."
            pgb_Progress.Foreground = clrWarning
            Terabas()
            Jeda(999)
            btn_Pulihkan.Content = "Ulangi"
            btn_Pulihkan.Visibility = Visibility.Visible
        End If

    End Sub

    Sub ProsesPemulihanData(filePath As String)

        Dim builder As New StringBuilder()

        Using sr As New StreamReader(filePath, Encoding.UTF8)

            While Not sr.EndOfStream

                Dim line = sr.ReadLine()

                If line IsNot Nothing Then

                    ' Lewati baris komentar tertentu jika perlu
                    If line.StartsWith("--") OrElse line.StartsWith("/*") Then
                        Continue While
                    End If

                    builder.AppendLine(line)
                    ' Kalau ketemu titik koma di akhir: anggap satu statement selesai
                    If line.Trim().EndsWith(";"c) Then
                        Dim sql = builder.ToString()
                        EksekusiKeMySQL(sql, filePath) ' pakai MySqlCommand
                        builder.Clear()
                    End If

                End If

            End While

        End Using


    End Sub


    Dim connStr As String
    Sub EksekusiKeMySQL(sql As String, filePath As String)
        Using conn As New MySqlConnection(connStr)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.CommandTimeout = 0 ' jangan dibatasi 30 detik
                Try
                    cmd.ExecuteNonQuery()
                    PulihkanDatabaseBerhasil = True
                Catch ex As Exception
                    PulihkanDatabaseBerhasil = False
                    PesanUntukProgrammer("Pemulihan database '" & filePath & "' gagal...!!!.")
                End Try
            End Using
        End Using
    End Sub



    Private Sub btn_Pulihkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Pulihkan.Click
        ResetForm()
        btn_Pulihkan.IsEnabled = False
        Terabas()
        TahapanPemulihan()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub


End Class
