Imports bcomm
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_ProgressImportDataCOA

    Public BarisAwalBahanDataCOA
    Public BarisAkhirBahanDataCOA

    Dim COA
    Dim NamaAkun
    Dim DebetKredit
    Dim Uraian

    Dim Baris
    Dim JumlahBaris_SumberData
    Dim JumlahBaris_Terposting
    Dim JumlahBaris_GagalPosting
    Dim JumlahBarisBahanDataCOA
    Dim StatusImport
    Dim WaktuKerja
    Dim WaktuIstirahat = 1000
    Dim BatasLimitJumlahBaris = 5000
    Dim DurasiIstirahatSejenak = 1 'Detik
    Dim DurasiIstirahatLama = 5 'Detik
    Dim DataValid As Boolean

    Dim ConnImport As OleDbConnection
    Dim DaImport As OleDbDataAdapter
    Public DsImport As New DataSet
    Dim MyProses As New Process

    Dim cmdDIVISI As OdbcCommand
    Dim drDIVISI As OdbcDataReader
    Dim cmdCOA As OdbcCommand
    Dim drCOA As OdbcDataReader

    Private Sub frm_ProgressImportDataCOA_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'TrialBalance_Mentahkan()

        Me.Text = "Proses..."
        Me.Height = 142
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Sistem sedang menganalisa sumber data."
        lbl_ProgressReport.Visible = False
        btn_Proses.Visible = False
        btn_Batal.Visible = False
        pgb_Progress.Visible = False
        Control.CheckForIllegalCrossThreadCalls = False
        bgw_ImportDataExcel.CancelAsync()
        bgw_ImportDataExcel.Dispose()
        bgw_Posting.CancelAsync()
        bgw_Posting.Dispose()
        ImportDataExcel()
        ResetForm()

    End Sub

    Sub ResetForm()
        txt_LaporanHasilPostingJurnal.Location = New Drawing.Point(35, 333)
        txt_LaporanHasilPostingJurnal.Clear()
        txt_LaporanHasilPostingJurnal.Text = Enter1Baris & "LAPORAN HASIL POSTING :"
        lbl_Baris_01.ForeColor = Color.Black
        lbl_Baris_02.ForeColor = Color.Black
        btn_Proses.Enabled = True
        btn_Batal.Enabled = True
        btn_BuangHasilPosting.Enabled = False
        btn_TetapkanHasilPosting.Enabled = False
        btn_Proses.Focus()
    End Sub

    Sub ImportDataExcel()

        ofd_Import.FileName = Nothing
        ofd_Import.Filter = "(*xlsx)|*xlsx|(*xls)|*xls|All Files(*.*)|*.*"
        ofd_Import.ShowDialog()

        If ofd_Import.FileName = Nothing Then
            Me.Close()
        Else
            ConnImport = New OleDbConnection("provider=Microsoft.jet.OLEDB.4.0;" & _
                                             "data source='" & ofd_Import.FileName & "';Extended Properties=Excel 8.0;")
            DaImport = New OleDbDataAdapter("SELECT * FROM [Bahan Data COA$]", ConnImport)
            bgw_ImportDataExcel.RunWorkerAsync()
        End If

    End Sub

    Private Sub bgw_ImportDataExcel_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw_ImportDataExcel.DoWork

        StatusImport = "SUKSES"

        Try
            ConnImport.Open()
            DsImport.Clear()
            DaImport.Fill(DsImport)
            DataGridView.DataSource = DsImport.Tables(0)
            ConnImport.Close()
        Catch ex As Exception
            StatusImport = "GAGAL"
        End Try

    End Sub

    Private Sub bgw_ImportDataExcel_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw_ImportDataExcel.ProgressChanged

    End Sub

    Private Sub bgw_ImportDataExcel_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw_ImportDataExcel.RunWorkerCompleted

        Dim IsiCOA As String
        Dim IsiNamaAkun As String
        Dim IsiDebetKredit As String

        JumlahBaris_SumberData = 0

        For Each row As DataGridViewRow In DataGridView.Rows
            IsiCOA = row.Cells(0).Value.ToString
            IsiNamaAkun = row.Cells(1).Value
            IsiDebetKredit = row.Cells(2).Value
            If IsiCOA <> Nothing Then
                JumlahBaris_SumberData = JumlahBaris_SumberData + 1
                If IsiNamaAkun = Nothing _
                    Or IsiDebetKredit = Nothing _
                    Or Len(IsiCOA) <> 5 _
                    Then
                    StatusImport = "DATA RUSAK"
                End If
            Else
                If IsiNamaAkun <> Nothing _
                    Or IsiDebetKredit <> Nothing _
                    Then
                    StatusImport = "DATA RUSAK"
                End If
            End If
        Next

        If JumlahBaris_SumberData >= BatasLimitJumlahBaris Then
            StatusImport = "BARIS LEBIH"
        End If

        If StatusImport = "DATA RUSAK" Then
            MsgBox("Format sumber data tidak sesuai." & Enter2Baris & "Silakan perbaiki terlebih dahulu dan ulangi lagi.")
            TutupForm()
        End If

        If StatusImport = "GAGAL" Then
            MsgBox("Silakan buka/aktifkan terlebih dahulu file yang bersangkutan menggunakan Excel, dan jalankan kembali fitur ini.")
            TutupForm()
        End If

        If StatusImport = "BARIS LEBIH" Then
            MsgBox("Sistem import tidak mengizinkan data melebihi baris " & BatasLimitJumlahBaris & " pada tabel Excel dalam sekali event." & _
                   Enter2Baris & "Silakan Anda pecah sumber data menjadi beberapa partisi, dan ulangi kembali." & _
                   "")
            DsImport.Clear()
            TutupForm()
        End If

        If StatusImport = "SUKSES" Then
            Me.Height = 231
            pgb_Progress.Value = 0
            pgb_Progress.Maximum = JumlahBaris_SumberData
            lbl_Baris_01.Text = "Anda akan memposting " & JumlahBaris_SumberData & " baris data."
            lbl_Baris_02.Text = "Silakan klik tombol 'Proses' untuk melanjutkan."
            btn_Proses.Visible = True
            btn_Proses.Enabled = True
            btn_Batal.Visible = True
            btn_Batal.Text = "Batal"
            pgb_Progress.Visible = True
            lbl_ProgressReport.Visible = False
        End If

    End Sub

    Private Sub bgw_Posting_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw_Posting.DoWork

        ProsesPostingJurnal()

    End Sub

    Sub ProsesPostingJurnal()

        StatusPosting = "PROSES"
        HasilPosting = "NORMAL"
        WaktuKerja = 0
        Baris = 0
        BarisAwalBahanDataCOA = 0
        JumlahBarisBahanDataCOA = 0
        JumlahBaris_Terposting = 0
        JumlahBaris_GagalPosting = 0
        lbl_ProgressReport.Text = Nothing
        lbl_ProgressReport.Visible = True

        Do While Baris < JumlahBaris_SumberData
            DataValid = True
            WaktuKerja = WaktuKerja + 1
            If WaktuKerja = WaktuIstirahat Then
                WaktuKerja = 0
                lbl_ProgressReport.Text = lbl_ProgressReport.Text & "  |  [ Break... ]"
                Threading.Thread.Sleep(DurasiIstirahatSejenak * 1000)
            End If
            If Baris = 5000 Then
                Threading.Thread.Sleep(DurasiIstirahatLama * 1000)
            End If
            Try
                COA = DataGridView.Item(0, Baris).Value
                NamaAkun = DataGridView.Item(1, Baris).Value
                DebetKredit = DataGridView.Item(2, Baris).Value
                Uraian = DataGridView.Item(3, Baris).Value
            Catch ex As Exception
                ProsesPostingGagal()
                Return
            End Try
            JumlahBarisBahanDataCOA = JumlahBarisBahanDataCOA + 1
            If StatusPosting = "BATAL" Then Return
            If DataValid = True Then
                AksesDatabase_General(Buka)
                If StatusKoneksiDatabase = False Then Return
                cmd = New OdbcCommand(" INSERT INTO tbl_COA VALUES ( " & _
                                      " '" & COA & "', " & _
                                      " '" & NamaAkun & "', " & _
                                      " '" & DebetKredit & "', " & _
                                      " '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', " & _
                                      " '" & Uraian & "', " & _
                                      " 'YA' " & _
                                      " ) ", KoneksiDatabaseGeneral)
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    'Kode Apabila gagal penyimpanan :
                    JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + 1
                    HasilPosting = "BERMASALAH"
                End Try
                AksesDatabase_General(Tutup)
            Else
                JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + 1
                MsgBox("Ada masalah pada baris " & Baris + 2 & " di tabel Excel.")
            End If
            If StatusPosting = "TAHAN" Then
                TanyaBatalPostingDataAsset()
            End If
            Baris = Baris + 1
            pgb_Progress.Value = Baris
            lbl_ProgressReport.Text = "Progress :  Baris " & Baris + 1
        Loop

        If StatusPosting = "BATAL" Then Return

    End Sub

    Private Sub bgw_Posting_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw_Posting.ProgressChanged

    End Sub

    Private Sub bgw_Posting_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw_Posting.RunWorkerCompleted

        If StatusPosting = "BATAL" Then
            TutupForm()
            Return
        Else
            pgb_Progress.Visible = False
            StatusPosting = "SELESAI"
            pgb_Progress.Visible = False
            btn_Batal.Text = "Tutup"
            lbl_ProgressReport.Visible = False
            btn_Proses.Enabled = False
            btn_Batal.Enabled = False
            btn_BuangHasilPosting.Enabled = True
            btn_TetapkanHasilPosting.Enabled = True
            If HasilPosting = "NORMAL" Then
                Me.Text = "Sukses..!"
                lbl_Baris_01.Text = "Sukses..!"
                lbl_Baris_02.Text = "Data Jurnal berhasil diposting seluruhnya."
                lbl_Baris_01.ForeColor = Color.Black
                lbl_Baris_02.ForeColor = Color.Black
                txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Data berhasil diposting seluruhnya."
                btn_TetapkanHasilPosting.Focus()
            End If
            If HasilPosting = "BERMASALAH" Then
                Me.Text = "Peringatan..!"
                lbl_Baris_01.Text = "Hasil posting bermasalah..!"
                lbl_Baris_02.Text = "Silakan periksa laporan."
                lbl_Baris_01.ForeColor = Color.Red
                lbl_Baris_02.ForeColor = Color.Red
                btn_BuangHasilPosting.Focus()
            End If
        End If

        Me.Height = 333
        JumlahBaris_Terposting = JumlahBaris_SumberData - JumlahBaris_GagalPosting
        txt_LaporanHasilPostingJurnal.Location = New Drawing.Point(35, 87)
        txt_LaporanHasilPostingJurnal.Visible = True
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "_____________________________________________________"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "RESUME :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Sumber Data :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & JumlahBaris_SumberData & " baris."
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Data Terposting :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & JumlahBaris_Terposting & " baris."
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Data Gagal Terposting :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & JumlahBaris_GagalPosting & " baris."
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris

    End Sub

    Private Sub btn_Proses_Click(sender As Object, e As EventArgs) Handles btn_Proses.Click

        'Hapus semua data Tautan COA :
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_TautanCOA SET COA = '' ", KoneksiDatabaseGeneral)
        cmd.ExecuteNonQuery()
        AksesDatabase_General(Tutup)

        SistemPenomoranOtomatis_NomorJV()
        Dim NomorJV_AwalPosting = jur_NomorJV
        MsgBox("Harap tunggu, dan jangan hentikan aplikasi selama proses import berjalan..!")
        btn_Proses.Enabled = False
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Proses posting sedang berjalan."
        pgb_Progress.Visible = True
        StatusPosting = "PROSES"
        bgw_Posting.RunWorkerAsync()

    End Sub

    Public Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        If btn_Batal.Text = "Batal" Then
            If StatusPosting = "PROSES" Then
                StatusPosting = "TAHAN"
            Else
                TanyaBatalPostingDataAsset()
            End If
        End If
        If StatusPosting = "BATAL" Or StatusPosting = "SELESAI" Then
            TutupForm()
        End If
    End Sub

    Public Sub TanyaBatalPostingDataAsset()
        Dim PilihBatalPosting = MessageBox.Show("Seluruh proses posting pada event ini akan dibatalkan." & Enter2Baris & "Yakin akan membatalkan proses posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihBatalPosting = vbYes Then
            StatusPosting = "BATAL"
            HapusSemuaDataPostinganPadaEventIni()
        ElseIf PilihBatalPosting = vbNo Then
            StatusPosting = "PROSES"
        End If
    End Sub

    Sub ProsesPostingGagal()
        HapusSemuaDataPostinganPadaEventIni()
        MsgBox("Proses GAGAL, karena ada kesalahan format dari sumber data." & _
               Enter2Baris & "Silakan perbaiki dan sesuaikan sumber data, kemudian ulangi lagi.")
        MsgBox("Info :" & _
               Enter1Baris & "Kesalahan ada pada baris " & Baris + 2 & " tabel Excel sumber data." & _
               Enter2Baris & "Tips :" & _
               Enter1Baris & "- Susun sumber data sesuai kerangka yang disediakan" & _
               Enter1Baris & "- Kolom tanggal jangan diisi selain data tanggal" & _
               Enter1Baris & "- Kolom angka jangan disusupi karakter" & _
               Enter1Baris & "- Jangan menggunakan tanda petik satu ( ' )" & _
               Enter1Baris & "- Setelah baris akhir sumber data, tabel harus clear" & _
               "")
        DsImport.Clear()
        StatusPosting = "BATAL"
    End Sub

    Sub HapusSemuaDataPostinganPadaEventIni()

        Baris = 0

        Do While Baris < JumlahBaris_SumberData
            Try
                COA = DataGridView.Item(0, Baris).Value
            Catch ex As Exception
                Return
            End Try
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" DELETE FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            AksesDatabase_General(Tutup)
            Baris = Baris + 1
        Loop

    End Sub

    Private Sub txt_LaporanHasilImport_KeyPress(sender As Object, e As KeyPressEventArgs)
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_TetapkanHasilPosting_Click(sender As Object, e As EventArgs) Handles btn_TetapkanHasilPosting.Click
        If HasilPosting = "BERMASALAH" Then
            Dim PilihTetapkanHasilPosting = MessageBox.Show("Hasil posting bermasalah..!" & Enter2Baris & "Yakin akan menetapkan hasil posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If PilihTetapkanHasilPosting = vbYes Then
                usc_DataCOA.TampilkanData()
                TutupForm()
            Else
                Return
            End If
        End If
        If HasilPosting = "NORMAL" Then
            usc_DataCOA.TampilkanData()
            TutupForm()
        End If
    End Sub

    Private Sub btn_BuangHasilPosting_Click(sender As Object, e As EventArgs) Handles btn_BuangHasilPosting.Click
        TanyaBatalPostingDataAsset()
        If StatusPosting = "BATAL" Then
            TutupForm()
        End If
    End Sub

    Sub TutupForm()
        bgw_ImportDataExcel.CancelAsync()
        bgw_ImportDataExcel.Dispose()
        bgw_Posting.CancelAsync()
        bgw_Posting.Dispose()
        Me.Close()
    End Sub

End Class