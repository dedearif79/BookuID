Imports System.Data.Odbc
Imports System.IO
Imports System.Text
Imports System.Windows
Imports bcomm
Imports Microsoft.Win32
Imports MySql.Data.MySqlClient


Public Class wpfWin_KloningData


    Dim ProsesKloningData As Boolean
    Dim PenambahanProgress As String
    Dim ProsentaseProgress As Integer
    Public KloningDatabaseBerhasil As Boolean

    Dim IdCustomer_Sumber As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        PembatasanHakAkses(Me)

        ProsesLoadingForm = True

        txt_IDClient.Text = Kosongan 'Ini disimpan di sini saja. Jangan di Sub Reset()
        VisibilitasProgressBar(False)
        btn_Kloning.Visibility = Visibility.Visible
        btn_Tutup.Visibility = Visibility.Visible
        Terabas()

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()
        btn_Kloning.IsEnabled = True
        btn_Tutup.IsEnabled = True
        pgb_Progress.Value = 0
        lbl_Progress.Text = Kosongan
        KloningDatabaseBerhasil = False
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


    Private Sub txt_IDClient_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_IDClient.TextChanged
        IdCustomer_Sumber = txt_IDClient.Text
    End Sub




    Sub LogikaProsesPulihkanData(ProsesBerhasil As Boolean)
        If ProsesBerhasil Then
            ProsesKloningData = True
            pgb_Progress.Value += PenambahanProgress
            ProsentaseProgress += PenambahanProgress
            lbl_Progress.Text = "Proses pemulihan... " & ProsentaseProgress & " %"
        Else
            ProsesKloningData = False
        End If
    End Sub

    Dim KoneksiBackUp As MySqlConnection
    Dim cmdBackUp As MySqlCommand
    Dim strBackUp As String

    Private Sub TahapanKloning()

        Dim fbd_SumberData As New FolderBrowserDialog
        Dim folderPath As String
        If fbd_SumberData.ShowDialog() = Forms.DialogResult.OK Then
            folderPath = fbd_SumberData.SelectedPath
        Else
            ResetForm()
            Return
        End If

        VisibilitasProgressBar(True)

        ProsesKloningData = True
        pgb_Progress.Value = 0
        ProsentaseProgress = 0

        lbl_Progress.Text = "Proses pemulihan... 0 %"
        Jeda(333)

        Dim JumlahDatabase = 0

        Dim DaftarFileKloningan() As String = Directory.GetFiles(folderPath)

        For Each filePath As String In DaftarFileKloningan
            If AmbilTeksKanan(filePath, 3) = "sql" And AmbilTeksKanan(filePath, 9) <> "dasar.sql" Then JumlahDatabase += 1
        Next

        PenambahanProgress = 100 / JumlahDatabase

        connStr =
            "Data Source    = " & LokasiServerDatabase & ";" &
            "port           = " & PortDatabase & ";" &
            "username       = " & UserDatabase & ";" &
            "password       = " & PasswordDatabase & ";" &
            "SSL Mode       = None"

        For Each filePath As String In DaftarFileKloningan
            If AmbilTeksKanan(filePath, 3) = "sql" And AmbilTeksKanan(filePath, 9) <> "dasar.sql" Then
                ProsesPemulihanData(filePath)
                LogikaProsesPulihkanData(KloningDatabaseBerhasil)
                Terabas()
                Jeda(333)
            End If
        Next

        Jeda(333)

        PesanUntukProgrammer("Ada 1 tahap yang belum di-coding : " & Enter2Baris &
                             "Yaitu penyesuaian tahun-tahun database Transaksi. Yakni, hapus database transaksi untuk tahun yang tidak diperlukan...!!!")

        'Penutup :
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_Perangkat SET " &
                              " ID_Komputer = '" & ID_CPU & "', " &
                              " Kode_Khusus = '" & EnkripsiTeks(ID_CPU) & "' ", KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()
        cmd = New OdbcCommand(" UPDATE tbl_Company SET " &
                              " Nomor_Seri_Produk = '" & NomorSeriProduk & "' ", KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()
        AksesDatabase_General(Tutup)


        btn_Tutup.IsEnabled = True

        If ProsesKloningData = True Then
            pgb_Progress.Value = 100
            lbl_Progress.Text = "Pemulihan database berhasil."
            btn_Kloning.Visibility = Visibility.Collapsed
            btn_Tutup.Visibility = Visibility.Visible
            Terabas()
            Jeda(999)
        Else
            pgb_Progress.Value = 0
            lbl_Progress.Text = "Pemulihan database gagal."
            pgb_Progress.Foreground = WarnaPeringatan_WPF
            Terabas()
            Jeda(999)
            btn_Kloning.Content = "Ulangi"
            btn_Kloning.Visibility = Visibility.Visible
        End If

    End Sub


    Sub ProsesPemulihanData(filePath As String)

        Dim builder As New StringBuilder()

        Using sr As New StreamReader(filePath, Encoding.UTF8)

            While Not sr.EndOfStream

                Dim line As String = sr.ReadLine()

                line = Replace(line, IdCustomer_Sumber, ID_Customer)

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
                    KloningDatabaseBerhasil = True
                Catch ex As Exception
                    KloningDatabaseBerhasil = False
                    PesanUntukProgrammer("Kloning database '" & filePath & "' gagal...!!!.")
                End Try
            End Using
        End Using
    End Sub



    Private Sub btn_Kloning_Click(sender As Object, e As RoutedEventArgs) Handles btn_Kloning.Click
        If IdCustomer_Sumber = Kosongan Then
            PesanPeringatan("Isi ID Client..!")
            txt_IDClient.Focus()
            Return
        End If
        ResetForm()
        btn_Kloning.IsEnabled = False
        btn_Tutup.IsEnabled = False
        Terabas()
        TahapanKloning()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_IDClient.Text = Kosongan 'Ini disimpan di sini saja. Jangan di Sub Reset()
    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Close()
    End Sub

End Class
