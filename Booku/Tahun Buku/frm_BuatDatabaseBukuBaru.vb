Imports bcomm
Imports System.Data.Odbc

Public Class frm_BuatDatabaseBukuBaru

    Private Sub frm_BuatDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub ResetForm()
        txt_TahunBuku.Enabled = True
        cmb_JenisTahunBuku.Enabled = True
        btn_BuatDatabase.Enabled = True

        txt_TahunBuku.Text = Nothing
        KontenComboJenisFitur()
    End Sub

    Private Sub btn_BuatDatabase_Click(sender As Object, e As EventArgs) Handles btn_BuatDatabase.Click

        TahunBukuBaru = txt_TahunBuku.Text
        Dim TahunBukuSebelumnya = TahunBukuAktif

        If AmbilAngka(TahunBukuBaru) > AmbilAngka(TahunIni) Then
            MsgBox("Database Tahun Buku " & TahunBukuBaru & " belum saatnya untuk dibuat.")
            btn_BuatDatabase.Enabled = True
            Return
        End If

        If AmbilAngka(TahunBukuBaru) < (AmbilAngka(TahunIni) - 4) Then
            MsgBox("Sistem hanya bisa membuat database untuk tahun ini dan 4 tahun ke belakang.")
            btn_BuatDatabase.Enabled = True
            Return
        End If

        If cmb_JenisTahunBuku.Text = Nothing Then
            MsgBox("Silakan pilih Jenis Fitur")
            cmb_JenisTahunBuku.Focus()
            Return
        End If

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        btn_BuatDatabase.Enabled = False
        cmd = New OdbcCommand("SELECT * FROM tbl_InfoData WHERE Tahun_Buku = '" & TahunBukuBaru & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            JenisTahunBuku_Baru = cmb_JenisTahunBuku.Text
            BuatDatabaseBaruTransaksi(TahunBukuBaru)
        Else
            MsgBox("Database Tahun Buku " & TahunBukuBaru & " sudah ada.")
            AksesDatabase_General(Tutup)
            btn_BuatDatabase.Enabled = True
            Return
        End If
        AksesDatabase_General(Tutup)

        If HasilPembuatanDatabaseTransaksi = True Then
            MsgBox("Database Tahun Buku " & TahunBukuBaru & " berhasil dibuat.")
            frm_GantiTahunBuku.ProsesGantiTahunBuku()
            Me.Close()
        Else
            MsgBox("Database Tahun Buku " & TahunBukuBaru & " GAGAL dibuat karena kendala tertentu." & Enter2Baris & teks_SilakanCobaLagi_Database)
            TahunBukuAktif = TahunBukuSebelumnya
            btn_BuatDatabase.Enabled = True
        End If

    End Sub

    Sub KontenComboJenisFitur()
        cmb_JenisTahunBuku.Items.Clear()
        cmb_JenisTahunBuku.Items.Add(JenisTahunBuku_LAMPAU)
        cmb_JenisTahunBuku.Items.Add(JenisTahunBuku_NORMAL)
        cmb_JenisTahunBuku.Text = Nothing
    End Sub

    Private Sub txt_TahunBuku_TextChanged(sender As Object, e As EventArgs) Handles txt_TahunBuku.TextChanged
    End Sub
    Private Sub txt_TahunBuku_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TahunBuku.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

End Class