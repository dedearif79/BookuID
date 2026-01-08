Imports System.IO
Imports bcomm

Public Class frm_ProgressExport_CSV

    Public BahanExport As DataGridView
    Dim JumlahBarisBahanExport
    Dim JumlahBarisTerexport
    Dim ProsesExport As Boolean
    Dim JumlahKolom
    Dim Kolom As Integer
    Dim NamaFileExportCSV
    Dim KontenTabel
    Dim KontenKepalaKolom
    Dim KontenPerBaris
    Dim PemisahKolom = ";"

    Public NamaSheet

    Private Sub frm_ProgressExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckForIllegalCrossThreadCalls = True

        Me.Text = "Proses..."
        lbl_ProgressReport.Visible = False
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Proses sedang berjalan."
        btn_Tutup.Visible = False

        JumlahBarisBahanExport = BahanExport.RowCount
        If JumlahBarisBahanExport = 0 Then
            MsgBox("Tidak ada bahan data yang akan di-export.")
            TutupForm()
        Else
            'Dialog Penyimpanan (Save As) :
            sfd_Simpan.FileName = Kosongan
            sfd_Simpan.Filter = "CSV Files (*.csv)|*.csv"
            If sfd_Simpan.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                pgb_Progress.Maximum = JumlahBarisBahanExport
                pgb_Progress.Value = 0
                lbl_ProgressReport.Visible = True
                Bgw_Export.RunWorkerAsync()
            Else
                TutupForm()
            End If
        End If

    End Sub

    Private Sub Bgw_Export_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Bgw_Export.DoWork

        ProsesExport = True

        JumlahKolom = BahanExport.ColumnCount
        NamaFileExportCSV = sfd_Simpan.FileName
        KontenTabel = Kosongan
        KontenKepalaKolom = Kosongan
        KontenPerBaris = Kosongan

        'Jika Nama File Sudah Ada :
        If File.Exists(NamaFileExportCSV) Then
            Try
                File.Delete(NamaFileExportCSV)
                ProsesExport = True
            Catch ex As Exception
                MsgBox("File yang menjadi sasaran export data sedang terbuka." & Enter2Baris & "Harap TUTUP terlebih dahulu file tersebut, dan silakan ulangi lagi.")
                ProsesExport = False
            End Try
        End If

        'Pengisian Value Kepala/Judul Kolom :
        If ProsesExport = True Then
            Kolom = 0
            For Each column As DataGridViewColumn In BahanExport.Columns
                If column.Visible = True Then
                    KontenTabel += BahanExport.Columns(Kolom).HeaderText & PemisahKolom
                End If
                Kolom += 1
            Next
            KontenTabel &= Enter1Baris
        End If

        'Pengisian Value Konten Per Baris :
        If ProsesExport = True Then
            Dim Baris = 0
            For Each row As DataGridViewRow In BahanExport.Rows
                Baris += 1
                BeginInvoke(Sub() pgb_Progress.Value = Baris)
                BeginInvoke(Sub() JumlahBarisTerexport = Baris)
                BeginInvoke(Sub() lbl_ProgressReport.Text = "Progress :  Export data baris ke-" & JumlahBarisTerexport)
                Kolom = 0
                KontenPerBaris = Kosongan
                For Each column As DataGridViewColumn In BahanExport.Columns
                    If column.Visible = True Then
                        KontenPerBaris += PenghapusEnter(row.Cells(Kolom).Value) & PemisahKolom
                    End If
                    Kolom += 1
                Next
                KontenPerBaris &= Enter1Baris
                KontenTabel &= KontenPerBaris
                If Baris < 9 Then System.Threading.Thread.Sleep(63)
            Next
        End If

    End Sub

    Private Sub Bgw_Export_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles Bgw_Export.ProgressChanged

    End Sub

    Private Sub Bgw_Export_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bgw_Export.RunWorkerCompleted

        lbl_ProgressReport.Visible = False

        'Penyimpanan File CSV :
        If ProsesExport = True Then
            Try
                My.Computer.FileSystem.WriteAllText(NamaFileExportCSV, KontenTabel, True)
                ProsesExport = True
            Catch ex As Exception
                ProsesExport = False
            End Try
        End If

        System.Threading.Thread.Sleep(123)

        'Laporan Hasil Export :
        If ProsesExport = True Then
            Me.Text = "Selesai..!!!"
            lbl_Baris_01.Text = "Export BERHASIL."
            lbl_Baris_02.Text = "Silakan tutup."
        Else
            lbl_Baris_01.Text = "Export GAGAL..!!!"
            lbl_Baris_02.Text = "Silakan tutup, dan coba lagi."
        End If

        btn_Tutup.Visible = True

    End Sub

    Sub TutupForm()
        Me.Close()
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        TutupForm()
    End Sub

End Class