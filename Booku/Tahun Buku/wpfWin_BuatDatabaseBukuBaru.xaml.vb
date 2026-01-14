Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input


Public Class wpfWin_BuatDatabaseBukuBaru

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub


    Sub ResetForm()
        txt_TahunBuku.IsEnabled = True
        cmb_JenisTahunBuku.IsEnabled = True
        btn_BuatDatabase.IsEnabled = True

        txt_TahunBuku.Text = Nothing
        KontenComboJenisFitur()
    End Sub


    Private Sub btn_BuatDatabase_Click(sender As Object, e As RoutedEventArgs) Handles btn_BuatDatabase.Click

        TahunBukuBaru = txt_TahunBuku.Text
        Dim TahunBukuSebelumnya = TahunBukuAktif

        If AmbilAngka(TahunBukuBaru) > AmbilAngka(TahunIni) Then
            Pesan_Peringatan("Database Tahun Buku " & TahunBukuBaru & " belum saatnya untuk dibuat.")
            btn_BuatDatabase.IsEnabled = True
            Return
        End If

        If AmbilAngka(TahunBukuBaru) < (AmbilAngka(TahunIni) - 4) Then
            Pesan_Peringatan("Sistem hanya bisa membuat database untuk tahun ini dan 4 tahun ke belakang.")
            btn_BuatDatabase.IsEnabled = True
            Return
        End If

        If cmb_JenisTahunBuku.SelectedValue Is Nothing Then
            Pesan_Peringatan("Silakan pilih Jenis Fitur")
            cmb_JenisTahunBuku.Focus()
            Return
        End If

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        btn_BuatDatabase.IsEnabled = False
        cmd = New OdbcCommand("SELECT * FROM tbl_InfoData WHERE Tahun_Buku = '" & TahunBukuBaru & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            JenisTahunBuku_Baru = CStr(cmb_JenisTahunBuku.SelectedValue)
            BuatDatabaseBaruTransaksi(TahunBukuBaru)
        Else
            Pesan_Peringatan("Database Tahun Buku " & TahunBukuBaru & " sudah ada.")
            AksesDatabase_General(Tutup)
            btn_BuatDatabase.IsEnabled = True
            Return
        End If
        AksesDatabase_General(Tutup)

        If HasilPembuatanDatabaseTransaksi = True Then
            Pesan_Sukses("Database Tahun Buku " & TahunBukuBaru & " berhasil dibuat.")
            win_GantiTahunBuku = New wpfWin_GantiTahunBuku
            win_GantiTahunBuku.ProsesGantiTahunBuku()
            Me.Close()
        Else
            Pesan_Gagal("Database Tahun Buku " & TahunBukuBaru & " GAGAL dibuat karena kendala tertentu." & Enter2Baris & teks_SilakanCobaLagi_Database)
            TahunBukuAktif = TahunBukuSebelumnya
            btn_BuatDatabase.IsEnabled = True
        End If

    End Sub


    Sub KontenComboJenisFitur()
        cmb_JenisTahunBuku.Items.Clear()
        cmb_JenisTahunBuku.Items.Add(JenisTahunBuku_LAMPAU)
        cmb_JenisTahunBuku.Items.Add(JenisTahunBuku_NORMAL)
        cmb_JenisTahunBuku.SelectedIndex = -1
    End Sub


    Private Sub txt_TahunBuku_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_TahunBuku.PreviewTextInput
    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
    End Sub

End Class
