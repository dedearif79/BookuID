Imports System.Windows
Imports System.IO

Public Class wpfWin_PilihModeAplikasi

    ''' <summary>
    ''' Hasil pilihan mode aplikasi.
    ''' True = Mode Modern (WPF Shell)
    ''' False = Mode Classic (WinForms MDI)
    ''' </summary>
    Public Property ModeModernDipilih As Boolean = False

    Private ReadOnly FilePathPilihanMode As String = Path.Combine(FolderNotesApp, "pilihan_mode_aplikasi.txt")

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ' Cek apakah ada pilihan yang disimpan sebelumnya
        If File.Exists(FilePathPilihanMode) Then
            Try
                Dim pilihanTersimpan As String = File.ReadAllText(FilePathPilihanMode).Trim()
                If pilihanTersimpan = "MODERN" Then
                    ModeModernDipilih = True
                    Me.DialogResult = True
                    Me.Close()
                    Return
                ElseIf pilihanTersimpan = "CLASSIC" Then
                    ModeModernDipilih = False
                    Me.DialogResult = True
                    Me.Close()
                    Return
                End If
            Catch ex As Exception
                ' Jika gagal baca, tampilkan dialog seperti biasa
            End Try
        End If
    End Sub

    Private Sub btn_ModeClassic_Click(sender As Object, e As RoutedEventArgs) Handles btn_ModeClassic.Click
        ModeModernDipilih = False
        SimpanPilihanJikaDiminta()
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub btn_ModeModern_Click(sender As Object, e As RoutedEventArgs) Handles btn_ModeModern.Click
        ModeModernDipilih = True
        SimpanPilihanJikaDiminta()
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub SimpanPilihanJikaDiminta()
        If chk_IngatPilihan.IsChecked = True Then
            Try
                Dim pilihan As String = If(ModeModernDipilih, "MODERN", "CLASSIC")
                File.WriteAllText(FilePathPilihanMode, pilihan)
            Catch ex As Exception
                ' Abaikan jika gagal menyimpan
            End Try
        Else
            ' Hapus file pilihan jika checkbox tidak dicentang
            Try
                If File.Exists(FilePathPilihanMode) Then
                    File.Delete(FilePathPilihanMode)
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

End Class
