Imports bcomm
Imports System.ComponentModel
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Windows
Imports System.Windows.Controls
Imports Microsoft.Win32

Public Class wpfWin_ProgressImportDataCOA

    ' === PUBLIC PROPERTIES ===
    Public BarisAwalBahanDataCOA As Integer
    Public BarisAkhirBahanDataCOA As Integer

    ' === VARIABEL NILAI FORM ===
    Dim COA As String
    Dim NamaAkun As String
    Dim DebetKredit As String
    Dim Uraian As String

    Dim Baris As Integer
    Dim JumlahBaris_SumberData As Integer
    Dim JumlahBaris_Terposting As Integer
    Dim JumlahBaris_GagalPosting As Integer
    Dim JumlahBarisBahanDataCOA As Integer
    Dim StatusImport As String
    Dim WaktuKerja As Integer
    Dim WaktuIstirahat As Integer = 1000
    Dim BatasLimitJumlahBaris As Integer = 5000
    Dim DurasiIstirahatSejenak As Integer = 1 ' Detik
    Dim DurasiIstirahatLama As Integer = 5 ' Detik
    Dim DataValid As Boolean

    ' === KONEKSI OLEDB ===
    Dim ConnImport As OleDbConnection
    Dim DaImport As OleDbDataAdapter
    Public DsImport As DataSet
    Dim datatabelBahanImpor As DataTable

    ' === BACKGROUND WORKER ===
    Private WithEvents bgw_ImportDataExcel As BackgroundWorker
    Private WithEvents bgw_Posting As BackgroundWorker

    ' === OPEN FILE DIALOG ===
    Dim ofd_Import As Microsoft.Win32.OpenFileDialog

    ' === LAPORAN ===
    Dim LaporanHasilPostingCOA As String


    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_LaporanHasilPostingCOA.IsReadOnly = True

        ' Inisialisasi BackgroundWorker
        bgw_Posting = New BackgroundWorker
        bgw_Posting.WorkerReportsProgress = True
        bgw_Posting.WorkerSupportsCancellation = True

        bgw_ImportDataExcel = New BackgroundWorker
        bgw_ImportDataExcel.WorkerReportsProgress = True
        bgw_ImportDataExcel.WorkerSupportsCancellation = True

        ' Inisialisasi DataSet dan OpenFileDialog
        DsImport = New DataSet
        ofd_Import = New Microsoft.Win32.OpenFileDialog
        datatabelBahanImpor = New DataTable
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StyleWindowDialogWPF_TanpaTombolX(Me)
        Title = "Proses..."
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Sistem sedang menganalisa sumber data."
        lbl_ProgressReport.Visibility = Visibility.Collapsed
        btn_Proses.Visibility = Visibility.Collapsed
        btn_Batal.Visibility = Visibility.Collapsed
        pgb_Progress.Visibility = Visibility.Collapsed
        ResetForm()
        ImportDataExcel()

    End Sub


    Sub ResetForm()
        KosongkanValueElemenRichTextBox(txt_LaporanHasilPostingCOA)
        LaporanHasilPostingCOA = Enter1Baris & "LAPORAN HASIL POSTING :"
        lbl_Baris_01.Foreground = clrTeksPrimer
        lbl_Baris_02.Foreground = clrTeksPrimer
        btn_Proses.Visibility = Visibility.Visible
        btn_Batal.Visibility = Visibility.Visible
        btn_Buang.Visibility = Visibility.Collapsed
        btn_Terapkan.Visibility = Visibility.Collapsed
        txt_LaporanHasilPostingCOA.Visibility = Visibility.Collapsed
        btn_Proses.Focus()
    End Sub


    Sub ImportDataExcel()

        ofd_Import.FileName = Kosongan
        ofd_Import.Filter = "(*xlsx)|*xlsx|(*xls)|*xls|All Files(*.*)|*.*"
        Dim result = ofd_Import.ShowDialog()

        If result <> True Then
            Me.Close()
        Else
            ConnImport = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ofd_Import.FileName & ";Extended Properties='Excel 12.0 Xml;HDR=YES';")
            DaImport = New OleDbDataAdapter("SELECT * FROM [Bahan Data COA$]", ConnImport)
            bgw_ImportDataExcel.RunWorkerAsync()
        End If

    End Sub


    Private Sub bgw_ImportDataExcel_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw_ImportDataExcel.DoWork

        StatusImport = Status_SUKSES

        Try
            ConnImport.Open()
            DsImport.Clear()
            DaImport.Fill(DsImport)
            datatabelBahanImpor = DsImport.Tables(0)
            ConnImport.Close()
        Catch ex As Exception
            StatusImport = Status_GAGAL
        End Try

    End Sub


    Private Sub bgw_ImportDataExcel_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw_ImportDataExcel.ProgressChanged

    End Sub


    Private Sub bgw_ImportDataExcel_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw_ImportDataExcel.RunWorkerCompleted

        Dim IsiCOA As String
        Dim IsiNamaAkun As String
        Dim IsiDebetKredit As String

        JumlahBaris_SumberData = 0

        For Each row As DataRow In datatabelBahanImpor.Rows
            IsiCOA = row(0).ToString
            IsiNamaAkun = row(1).ToString
            IsiDebetKredit = row(2).ToString
            If IsiCOA <> Kosongan Then
                JumlahBaris_SumberData += 1
                If IsiNamaAkun = Kosongan _
                    Or IsiDebetKredit = Kosongan _
                    Or IsiCOA.Length <> 5 _
                    Then
                    StatusImport = Status_DATARUSAK
                End If
            Else
                If IsiNamaAkun <> Kosongan _
                    Or IsiDebetKredit <> Kosongan _
                    Then
                    StatusImport = Status_DATARUSAK
                End If
            End If
        Next

        If JumlahBaris_SumberData >= BatasLimitJumlahBaris Then
            StatusImport = Status_BARISLEBIH
        End If

        If StatusImport = Status_DATARUSAK Then
            Pesan_Peringatan("Format sumber data tidak sesuai." & Enter2Baris & "Silakan perbaiki terlebih dahulu dan ulangi lagi.")
            TutupForm()
        End If

        If StatusImport = Status_GAGAL Then
            Pesan_Peringatan("Silakan buka/aktifkan terlebih dahulu file yang bersangkutan menggunakan Excel, dan jalankan kembali fitur ini.")
            TutupForm()
        End If

        If StatusImport = Status_BARISLEBIH Then
            Pesan_Peringatan("Sistem import tidak mengizinkan data melebihi baris " & BatasLimitJumlahBaris & " pada tabel Excel dalam sekali event." &
                   Enter2Baris & "Silakan Anda pecah sumber data menjadi beberapa partisi, dan ulangi kembali.")
            DsImport.Clear()
            TutupForm()
        End If

        If StatusImport = Status_SUKSES Then
            If JumlahBaris_SumberData = 0 Then
                Pesan_Peringatan("Tidak ada data yang bisa diimpor.")
                TutupForm()
                Return
            End If
            pgb_Progress.Value = 0
            pgb_Progress.Maximum = JumlahBaris_SumberData
            lbl_Baris_01.Text = "Anda akan memposting " & JumlahBaris_SumberData & " baris data."
            lbl_Baris_02.Text = "Silakan klik tombol 'Proses' untuk melanjutkan."
            btn_Proses.Visibility = Visibility.Visible
            btn_Batal.Visibility = Visibility.Visible
            btn_Batal.Content = "Batal"
            pgb_Progress.Visibility = Visibility.Visible
            lbl_ProgressReport.Visibility = Visibility.Collapsed
        End If

    End Sub


    Private Sub bgw_Posting_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw_Posting.DoWork
        ProsesPostingCOA()
    End Sub


    Sub ProsesPostingCOA()

        StatusPosting = Status_PROSES
        HasilPosting = Hasil_NORMAL
        WaktuKerja = 0
        Baris = 0
        BarisAwalBahanDataCOA = 0
        JumlahBarisBahanDataCOA = 0
        JumlahBaris_Terposting = 0
        JumlahBaris_GagalPosting = 0
        Dispatcher.BeginInvoke(
            Sub()
                lbl_ProgressReport.Text = Kosongan
                lbl_ProgressReport.Visibility = Visibility.Visible
            End Sub)

        Do While Baris < JumlahBaris_SumberData
            DataValid = True
            WaktuKerja += 1
            If WaktuKerja = WaktuIstirahat Then
                WaktuKerja = 0
                Dispatcher.BeginInvoke(Sub() lbl_ProgressReport.Text = lbl_ProgressReport.Text & "  |  [ Break... ]")
                System.Threading.Thread.Sleep(DurasiIstirahatSejenak * 1000)
            End If
            If Baris = 5000 Then
                System.Threading.Thread.Sleep(DurasiIstirahatLama * 1000)
            End If
            Try
                COA = datatabelBahanImpor.Rows(Baris)(0).ToString
                NamaAkun = datatabelBahanImpor.Rows(Baris)(1).ToString
                DebetKredit = datatabelBahanImpor.Rows(Baris)(2).ToString
                Uraian = datatabelBahanImpor.Rows(Baris)(3).ToString
            Catch ex As Exception
                ProsesPostingGagal()
                Return
            End Try
            JumlahBarisBahanDataCOA += 1
            If StatusPosting = Status_BATAL Then Return
            If DataValid = True Then
                AksesDatabase_General(Buka)
                If StatusKoneksiDatabase = False Then Return
                cmd = New OdbcCommand(" INSERT INTO tbl_COA VALUES ( " &
                                      " '" & COA & "', " &
                                      " '" & NamaAkun & "', " &
                                      " '" & DebetKredit & "', " &
                                      " '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', " &
                                      " '" & Uraian & "', " &
                                      " 'YA' " &
                                      " ) ", KoneksiDatabaseGeneral)
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    JumlahBaris_GagalPosting += 1
                    HasilPosting = Hasil_BERMASALAH
                End Try
                AksesDatabase_General(Tutup)
            Else
                JumlahBaris_GagalPosting += 1
                Pesan_Peringatan("Ada masalah pada baris " & Baris + 2 & " di tabel Excel.")
            End If
            If StatusPosting = Status_TAHAN Then
                TanyaBatalPostingCOA()
            End If
            Baris += 1
            Dispatcher.BeginInvoke(
                Sub()
                    pgb_Progress.Value = Baris
                    lbl_ProgressReport.Text = "Progress :  Baris " & Baris + 1
                End Sub)
        Loop

        If StatusPosting = Status_BATAL Then Return

    End Sub


    Private Sub bgw_Posting_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw_Posting.ProgressChanged

    End Sub


    Private Sub bgw_Posting_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw_Posting.RunWorkerCompleted

        If StatusPosting = Status_BATAL Then
            TutupForm()
            Return
        Else
            DsImport.Clear()
            pgb_Progress.Visibility = Visibility.Collapsed
            StatusPosting = Status_SELESAI
            btn_Batal.Content = "Tutup"
            lbl_ProgressReport.Visibility = Visibility.Collapsed
            btn_Proses.Visibility = Visibility.Collapsed
            btn_Batal.Visibility = Visibility.Collapsed
            btn_Buang.Visibility = Visibility.Visible
            btn_Terapkan.Visibility = Visibility.Visible
            If HasilPosting = Hasil_NORMAL Then
                Title = "Sukses..!"
                lbl_Baris_01.Text = "Sukses..!"
                lbl_Baris_02.Text = "Data COA berhasil diposting seluruhnya."
                lbl_Baris_01.Foreground = clrTeksPrimer
                lbl_Baris_02.Foreground = clrTeksPrimer
                LaporanHasilPostingCOA &= Enter2Baris & "Data berhasil diposting seluruhnya."
                btn_Terapkan.Focus()
            End If
            If HasilPosting = Hasil_BERMASALAH Then
                Title = "Peringatan..!"
                lbl_Baris_01.Text = "Hasil posting bermasalah..!"
                lbl_Baris_02.Text = "Silakan periksa laporan."
                lbl_Baris_01.Foreground = clrWarning
                lbl_Baris_02.Foreground = clrWarning
                btn_Buang.Focus()
            End If
        End If

        JumlahBaris_Terposting = JumlahBaris_SumberData - JumlahBaris_GagalPosting
        txt_LaporanHasilPostingCOA.Visibility = Visibility.Visible
        LaporanHasilPostingCOA &= Enter2Baris & "_____________________________________________________"
        LaporanHasilPostingCOA &= Enter2Baris & "RESUME :"
        LaporanHasilPostingCOA &= Enter2Baris & "Sumber Data :"
        LaporanHasilPostingCOA &= Enter1Baris & JumlahBaris_SumberData & " baris."
        LaporanHasilPostingCOA &= Enter2Baris & "Data Terposting :"
        LaporanHasilPostingCOA &= Enter1Baris & JumlahBaris_Terposting & " baris."
        LaporanHasilPostingCOA &= Enter2Baris & "Data Gagal Terposting :"
        LaporanHasilPostingCOA &= Enter1Baris & JumlahBaris_GagalPosting & " baris."
        LaporanHasilPostingCOA &= Enter1Baris
        IsiValueElemenRichTextBox(txt_LaporanHasilPostingCOA, LaporanHasilPostingCOA)
        txt_LaporanHasilPostingCOA.Visibility = Visibility.Visible

    End Sub


    Private Sub btn_Proses_Click(sender As Object, e As RoutedEventArgs) Handles btn_Proses.Click

        ' Hapus semua data Tautan COA
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_TautanCOA SET COA = '' ", KoneksiDatabaseGeneral)
        cmd.ExecuteNonQuery()
        AksesDatabase_General(Tutup)

        Pesan_Informasi("Harap tunggu, dan jangan hentikan aplikasi selama proses import berjalan..!")
        btn_Proses.Visibility = Visibility.Collapsed
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Proses posting sedang berjalan."
        pgb_Progress.Visibility = Visibility.Visible
        StatusPosting = Status_PROSES
        bgw_Posting.RunWorkerAsync()

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        If btn_Batal.Content.ToString = "Batal" Then
            If StatusPosting = Status_PROSES Then
                StatusPosting = Status_TAHAN
            Else
                TanyaBatalPostingCOA()
            End If
        End If
        If StatusPosting = Status_BATAL Or StatusPosting = Status_SELESAI Then
            TutupForm()
        End If
    End Sub


    Public Sub TanyaBatalPostingCOA()
        If TanyaKonfirmasi("Seluruh proses posting pada event ini akan dibatalkan." & Enter2Baris &
                          "Yakin akan membatalkan proses posting?") Then
            StatusPosting = Status_BATAL
            HapusSemuaDataPostinganPadaEventIni()
        Else
            StatusPosting = Status_PROSES
        End If
    End Sub


    Sub ProsesPostingGagal()
        HapusSemuaDataPostinganPadaEventIni()
        Pesan_Gagal("Proses GAGAL, karena ada kesalahan format dari sumber data." &
               Enter2Baris & "Silakan perbaiki dan sesuaikan sumber data, kemudian ulangi lagi.")
        Pesan_Informasi("Info :" &
               Enter1Baris & "Kesalahan ada pada baris " & Baris + 2 & " tabel Excel sumber data." &
               Enter2Baris & "Tips :" &
               Enter1Baris & "- Susun sumber data sesuai kerangka yang disediakan" &
               Enter1Baris & "- Kolom tanggal jangan diisi selain data tanggal" &
               Enter1Baris & "- Kolom angka jangan disusupi karakter" &
               Enter1Baris & "- Jangan menggunakan tanda petik satu ( ' )" &
               Enter1Baris & "- Setelah baris akhir sumber data, tabel harus clear")
        DsImport.Clear()
        StatusPosting = Status_BATAL
    End Sub


    Sub HapusSemuaDataPostinganPadaEventIni()

        Baris = 0

        Do While Baris < JumlahBaris_SumberData
            Try
                COA = datatabelBahanImpor.Rows(Baris)(0).ToString
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
            Baris += 1
        Loop

    End Sub


    Private Sub btn_Terapkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Terapkan.Click
        If HasilPosting = Hasil_BERMASALAH Then
            If Not TanyaKonfirmasi("Hasil posting bermasalah." & Enter2Baris & "Yakin akan menetapkan hasil posting?") Then Return
            If usc_DataCOA.StatusAktif Then usc_DataCOA.TampilkanData(True)
            TutupForm()
        End If
        If HasilPosting = Hasil_NORMAL Then
            If usc_DataCOA.StatusAktif Then usc_DataCOA.TampilkanData(True)
            TutupForm()
        End If
    End Sub


    Private Sub btn_Buang_Click(sender As Object, e As RoutedEventArgs) Handles btn_Buang.Click
        TanyaBatalPostingCOA()
        If StatusPosting = Status_BATAL Then
            TutupForm()
        End If
    End Sub


    Sub TutupForm()
        Me.Close()
    End Sub


End Class
