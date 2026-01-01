Imports System.ComponentModel
Imports System.Data.Odbc

Public Class frm_ProgressIsiTabelSaldoAwal

    Private Sub frm_ProgressIsiTabelSaldoAwal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        HasilPembuatanDatabaseTransaksi = False 'Defaulnya harus false dulu. Ketika ada masalah di coding, maka kembali ke default (false/gagal).

        lbl_Baris_01.Text = "Harap Tunggu..."
        lbl_Baris_02.Text = "Sistem sedang mennyiapkan konfigurasi database untuk Tahun Buku " & TahunBukuBaru & "."
        lbl_ProgressReport.Text = "Jangan memutus proses ini..!"
        Control.CheckForIllegalCrossThreadCalls = True
        pgb_Progress.Maximum = 80000
        Bgw_ProsesIsiTabel.RunWorkerAsync()

    End Sub

    Private Sub Bgw_ProsesIsiTabel_DoWork(sender As Object, e As DoWorkEventArgs) Handles Bgw_ProsesIsiTabel.DoWork

        Dim QueryIsiTabelSaldoAwal = Nothing

        Try
            QueryIsiTabelSaldoAwal = IO.File.ReadAllText("support/tbl_saldoawal.sql")
        Catch ex As Exception
        End Try

        'Pengisian COA Tabel Saldo dengan value Saldo_Awal 0 (nol) semua
        Try
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryIsiTabelSaldoAwal, KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
            HasilPembuatanDatabaseTransaksi = True
        Catch ex As Exception
            HasilPembuatanDatabaseTransaksi = False
        End Try

        QueryIsiTabelSaldoAwal = Nothing

    End Sub

    Private Sub Bgw_ProsesIsiTabel_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles Bgw_ProsesIsiTabel.ProgressChanged

    End Sub

    Private Sub Bgw_ProsesIsiTabel_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles Bgw_ProsesIsiTabel.RunWorkerCompleted
        Me.Close()
    End Sub

End Class