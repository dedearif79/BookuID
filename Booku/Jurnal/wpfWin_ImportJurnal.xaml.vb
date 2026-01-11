Imports bcomm
Imports System.ComponentModel
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Interop
Imports Microsoft.Win32

Public Class wpfWin_ImportJurnal

    Public BarisAwalBahanJV
    Public BarisAkhirBahanJV
    Dim COA
    Dim TanggalJurnal
    Dim TahunJurnal
    Dim NomorGJ
    Dim NomorGJSebelumnya
    Dim Referensi
    Dim Bundelan
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim NamaLawanTransaksi
    Dim DK
    Dim JumlahDebet As Int64
    Dim JumlahKredit As Int64
    Dim UraianTransaksi

    Dim Baris
    Dim JumlahBaris_SumberData
    Dim JumlahBaris_Terposting
    Dim JumlahBaris_GagalPosting
    Dim JumlahBarisBahanJV
    Dim JumlahJV_SumberData
    Dim JumlahJV_Terposting
    Dim JumlahJV_GagalPosting
    Dim NomorJV_GagalPosting
    Dim JumlahBaris_Direvisi
    Dim BalanceDebetKredit As Boolean
    Dim StatusImport
    Dim WaktuKerja
    Dim WaktuIstirahat = 1000
    Dim BatasLimitJumlahBaris = 100000
    Dim DurasiIstirahatSejenak = 1 'Detik
    Dim DurasiIstirahatLama = 5 'Detik

    Dim ConnImport As OleDbConnection
    Dim DaImport As OleDbDataAdapter
    Public DsImport As DataSet

    Dim TahunTransaksiSesuai As Boolean

    Dim ofd_Import As OpenFileDialog
    Dim datatabelBahanImpor As DataTable

    Private WithEvents bgw_ImportDataExcel As BackgroundWorker
    Private WithEvents bgw_Posting As BackgroundWorker

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StyleWindowDialogWPF_TanpaTombolX(Me) 'Ini harus disimpan di Sub Loaded. Jangan di Sub Initialize
        Title = "Proses..."
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Sistem sedang menganalisa sumber data."
        lbl_ProgressReport.Visibility = Visibility.Collapsed
        btn_Proses.Visibility = Visibility.Collapsed
        btn_Batal.Visibility = Visibility.Collapsed
        pgb_Progress.Visibility = Visibility.Collapsed
        ImportDataExcel()
        ResetValueJurnal()  'Jangan dihapus dan/atau dipindahkan....!!!!!!!!!!!!
        NomorID_AwalPosting = jur_NomorID + 1  'Untuk mengetahui Nomor ID paling awal pada event import..!!!!!

    End Sub


    Sub ImportDataExcel()

        ofd_Import.FileName = Kosongan
        ofd_Import.Filter = "(*xlsx)|*xlsx|(*xls)|*xls|All Files(*.*)|*.*"
        ofd_Import.ShowDialog()

        If ofd_Import.FileName = Kosongan Then
            Me.Close()
        Else
            'ConnImport = New OleDbConnection("provider=Microsoft.jet.OLEDB.4.0;data source='" & ofd_Import.FileName & "';Extended Properties=Excel 8.0;") '(Ini Versi Lama....!!!!!)
            ConnImport = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ofd_Import.FileName & ";Extended Properties='Excel 12.0 Xml;HDR=YES';")
            DaImport = New OleDbDataAdapter("SELECT * FROM [Bahan Jurnal$]", ConnImport)
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

        Dim BarisPenelusur = 1
        Dim BarisPenghapus = 1
        Dim IsiGJ As String
        Dim IsiCOA As String
        Dim IsiCOA_Int As Int64 = 0

        JumlahBaris_SumberData = 0

        For Each row As DataRow In datatabelBahanImpor.Rows
            IsiCOA = row(0).ToString
            IsiGJ = row(2).ToString
            IsiCOA_Int = AmbilAngka(IsiCOA)
            If IsiCOA_Int >= 10000 Then
                JumlahBaris_SumberData += 1
            Else
                If IsiGJ <> Kosongan Then
                    StatusImport = Status_DATARUSAK
                End If
            End If
        Next

        If JumlahBaris_SumberData >= BatasLimitJumlahBaris Then
            StatusImport = Status_BARISLEBIH
        End If

        If StatusImport = Status_DATARUSAK Then
            MsgBox("Format sumber data tidak sesuai." & Enter2Baris & "Silakan perbaiki terlebih dahulu dan ulangi lagi.")
            TutupForm()
        End If

        If StatusImport = Status_GAGAL Then
            MsgBox("Silakan buka/aktifkan terlebih dahulu file yang bersangkutan menggunakan Excel, dan jalankan kembali fitur ini.")
            TutupForm()
        End If

        If StatusImport = Status_BARISLEBIH Then
            MsgBox("Sistem import tidak mengizinkan data melebihi baris " & BatasLimitJumlahBaris & " pada tabel Excel dalam sekali event." &
                   Enter2Baris & "Silakan Anda pecah sumber data menjadi beberapa partisi, dan ulangi kembali." &
                   "")
            DsImport.Clear()
            TutupForm()
        End If

        If StatusImport = Status_SUKSES Then
            If JumlahBaris_SumberData = 0 Then
                MsgBox("Tidak ada data yang bisa diimpor.")
                TutupForm()
            End If
            pgb_Progress.Value = 0
            pgb_Progress.Maximum = JumlahBaris_SumberData
            lbl_Baris_01.Text = "Anda akan memposting " & JumlahBaris_SumberData & " baris data."
            lbl_Baris_02.Text = "Silakan klik tombol 'Proses' untuk melanjutkan."
            btn_Proses.Visibility = Visibility.Visible
            btn_Proses.Visibility = Visibility.Visible
            btn_Batal.Visibility = Visibility.Visible
            btn_Batal.Content = "Batal"
            pgb_Progress.Visibility = Visibility.Visible
            lbl_ProgressReport.Visibility = Visibility.Collapsed
        End If

    End Sub

    Private Sub bgw_Posting_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw_Posting.DoWork

        ResetValueJurnal()
        ProsesPostingJurnal()

    End Sub

    Sub ProsesPostingJurnal()

        StatusPosting = Status_PROSES
        HasilPosting = Hasil_NORMAL
        WaktuKerja = 0
        Baris = 0
        BarisAwalBahanJV = 0
        JumlahBarisBahanJV = 0
        JumlahBaris_Terposting = 0
        JumlahBaris_GagalPosting = 0
        JumlahJV_SumberData = 0
        JumlahJV_Terposting = 0
        JumlahJV_GagalPosting = 0
        JumlahBaris_Direvisi = 0
        NomorGJSebelumnya = "XXXHkdncdh834kXXX"
        JumlahDebet = 0
        JumlahKredit = 0
        Dim JumlahDebetSebelumnya As Int64 = 0
        Dim JumlahKreditSebelumnya As Int64 = 0
        Dim TotalDebet As Int64 = 0
        Dim TotalKredit As Int64 = 0
        SistemPenomoranOtomatis_NomorJV() '---------------|---------- "Dua Sejoli" ----------|
        jur_NomorJV = jur_NomorJV - 1 '[Penyesuaian] -----|------- Jangan Dipisahkan --------|
        jur_JenisJurnal = "Import"
        Dispatcher.BeginInvoke(
            Sub()
                lbl_ProgressReport.Text = Kosongan
                lbl_ProgressReport.Visibility = Visibility.Visible
            End Sub)

        Do While Baris < JumlahBaris_SumberData
            WaktuKerja = WaktuKerja + 1
            If WaktuKerja = WaktuIstirahat Then
                WaktuKerja = 0
                Dispatcher.BeginInvoke(Sub() lbl_ProgressReport.Text = lbl_ProgressReport.Text & "  |  [ Break... ]")
                System.Threading.Thread.Sleep(DurasiIstirahatSejenak * 1000)
            End If
            If Baris = 5000 Then
                System.Threading.Thread.Sleep(DurasiIstirahatLama * 1000)
            End If
            JumlahDebetSebelumnya = JumlahDebet
            JumlahKreditSebelumnya = JumlahKredit
            TotalDebet += JumlahDebetSebelumnya
            TotalKredit += JumlahKreditSebelumnya
            Try
                COA = datatabelBahanImpor.Rows(Baris)(0)
                TanggalJurnal = TanggalFormatSimpan(datatabelBahanImpor.Rows(Baris)(1))
                TahunJurnal = Format(datatabelBahanImpor.Rows(Baris)(1), "yyyy")
                If TahunJurnal = TahunBukuAktif Then
                    TahunTransaksiSesuai = True
                    jur_TahunTransaksiSesuai = True
                Else
                    TahunTransaksiSesuai = False
                    jur_TahunTransaksiSesuai = False
                End If
                NomorGJ = datatabelBahanImpor.Rows(Baris)(2).ToString
                Referensi = datatabelBahanImpor.Rows(Baris)(3).ToString
                Bundelan = datatabelBahanImpor.Rows(Baris)(4).ToString
                TanggalInvoice = datatabelBahanImpor.Rows(Baris)(5).ToString
                If Len(TanggalInvoice) > 10 Then TanggalInvoice = Microsoft.VisualBasic.Left(TanggalInvoice, 10)
                NomorInvoice = datatabelBahanImpor.Rows(Baris)(6).ToString
                NomorFakturPajak = datatabelBahanImpor.Rows(Baris)(7).ToString
                NamaLawanTransaksi = datatabelBahanImpor.Rows(Baris)(8).ToString
                DK = datatabelBahanImpor.Rows(Baris)(9).ToString
                If DK <> "D" And DK <> "K" Then
                    ProsesPostingGagal()
                    Return
                End If
                If datatabelBahanImpor.Rows(Baris)(10).ToString = Kosongan Then
                    JumlahDebet = 0
                Else
                    JumlahDebet = AmbilAngka(datatabelBahanImpor.Rows(Baris)(10))
                End If
                If datatabelBahanImpor.Rows(Baris)(11).ToString = Kosongan Then
                    JumlahKredit = 0
                Else
                    JumlahKredit = AmbilAngka(datatabelBahanImpor.Rows(Baris)(11))
                End If
                UraianTransaksi = datatabelBahanImpor.Rows(Baris)(12).ToString
            Catch ex As Exception
                ProsesPostingGagal()
                Return
            End Try
            JumlahBarisBahanJV = JumlahBarisBahanJV + 1
            If NomorGJSebelumnya <> NomorGJ Then
                If TotalDebet = TotalKredit Then
                    BalanceDebetKredit = True
                Else
                    BalanceDebetKredit = False
                    DataJurnalTidakValid()
                    If StatusPosting = Status_BATAL Then Return
                End If
                jur_NomorJV += 1
                TotalDebet = 0
                TotalKredit = 0
                JumlahBarisBahanJV = 0
                JumlahJV_SumberData += 1
            End If
            If StatusPosting = Status_BATAL Then Return
            KirimDataJurnal()
            NomorGJSebelumnya = NomorGJ
            If StatusPosting = Status_TAHAN Then
                TanyaBatalPostingJurnal()
            End If
            Baris += 1
            Dispatcher.BeginInvoke(
                Sub()
                    pgb_Progress.Value = Baris
                    lbl_ProgressReport.Text = "Progress :  Baris " & Baris + 1 & " | Nomor " & AwalanNomorJV & jur_NomorJV
                End Sub)
        Loop

        If StatusPosting = Status_BATAL Then Return

        '====== Script Susulan. Jangan dihapus...!!! =====================================
        If StatusPosting = Status_PROSES Then                                           '|
            TotalDebet += JumlahDebet                                                   '|
            TotalKredit += JumlahKredit                                                 '|
            If TotalDebet = TotalKredit Then                                            '|
                BalanceDebetKredit = True                                               '|
            Else                                                                        '|
                BalanceDebetKredit = False                                              '|
                DataJurnalTidakValid()                                                  '|
                If StatusPosting = Status_BATAL Then   'Ini jangan dihapus.             '|
                    Return 'Untuk jaga-jaga, khawatir ada coding lagi setelah baris ini.'|
                End If                                                                  '|
            End If                                                                      '|
        End If                                                                          '|
        '=================================================================================

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
            pgb_Progress.Visibility = Visibility.Collapsed
            btn_Batal.Content = "Tutup"
            lbl_ProgressReport.Visibility = Visibility.Collapsed
            btn_Proses.Visibility = Visibility.Collapsed
            btn_Batal.Visibility = Visibility.Collapsed
            btn_Buang.Visibility = Visibility.Visible
            btn_Terapkan.Visibility = Visibility.Visible
            If HasilPosting = Hasil_NORMAL Then
                Title = "Sukses..!"
                lbl_Baris_01.Text = "Sukses..!"
                lbl_Baris_02.Text = "Data Jurnal berhasil diposting seluruhnya."
                lbl_Baris_01.Foreground = WarnaTeksStandar_WPF
                lbl_Baris_02.Foreground = WarnaTeksStandar_WPF
                LaporanHasilPostingJurnal &= Enter2Baris & "Data berhasil diposting seluruhnya."
                btn_Terapkan.Focus()
            End If
            If HasilPosting = Hasil_BERMASALAH Then
                Title = "Peringatan..!"
                lbl_Baris_01.Text = "Hasil posting bermasalah..!"
                lbl_Baris_02.Text = "Silakan periksa laporan."
                lbl_Baris_01.Foreground = WarnaPeringatan_WPF
                lbl_Baris_02.Foreground = WarnaPeringatan_WPF
                btn_Buang.Focus()
            End If
        End If

        JumlahJV_Terposting = JumlahJV_SumberData - JumlahJV_GagalPosting
        JumlahBaris_Terposting = JumlahBaris_SumberData - JumlahBaris_GagalPosting
        txt_LaporanHasilPostingJurnal.Visibility = Visibility.Visible
        LaporanHasilPostingJurnal &= Enter2Baris & "_____________________________________________________"
        LaporanHasilPostingJurnal &= Enter2Baris & "RESUME :"
        LaporanHasilPostingJurnal &= Enter2Baris & "Sumber Data :"
        LaporanHasilPostingJurnal &= Enter1Baris & JumlahBaris_SumberData & " baris; " & JumlahJV_SumberData & " JV"
        LaporanHasilPostingJurnal &= Enter2Baris & "Data Terposting :"
        LaporanHasilPostingJurnal &= Enter1Baris & JumlahJV_Terposting & " JV  ( " & JumlahBaris_Terposting & " baris )"
        LaporanHasilPostingJurnal &= Enter2Baris & "Data Gagal Terposting :"
        LaporanHasilPostingJurnal &= Enter1Baris & JumlahJV_GagalPosting & " JV  ( " & JumlahBaris_GagalPosting & " baris )"
        LaporanHasilPostingJurnal &= Enter2Baris & "Data Direvisi :"
        LaporanHasilPostingJurnal &= Enter1Baris & JumlahBaris_Direvisi & " baris."
        LaporanHasilPostingJurnal &= Enter1Baris
        IsiValueElemenRichTextBox(txt_LaporanHasilPostingJurnal, LaporanHasilPostingJurnal)
        txt_LaporanHasilPostingJurnal.Visibility = Visibility.Visible

    End Sub

    Sub DataJurnalTidakValid()
        If jur_StatusPenyimpananJurnal_Lengkap = True Then
            BarisAwalBahanJV = Baris - JumlahBarisBahanJV + 2
            BarisAkhirBahanJV = BarisAwalBahanJV - 1 + JumlahBarisBahanJV
            MsgBox("Data tidak valid pada Jurnal Nomor " & AwalanNomorJV & jur_NomorJV & "." &
                   Enter1Baris & "( Baris " & BarisAwalBahanJV & " sampai " & BarisAkhirBahanJV & " pada tabel Excel )." &
                   Enter2Baris & "Silakan diperbaiki.")
            win_VerifikasiDataJurnal = New wpfWin_VerifikasiDataJurnal
            win_VerifikasiDataJurnal.NomorJV = AwalanNomorJV & jur_NomorJV
            win_VerifikasiDataJurnal.BarisAwalBahanJV = BarisAwalBahanJV
            win_VerifikasiDataJurnal.BarisAkhirBahanJV = BarisAkhirBahanJV
            win_VerifikasiDataJurnal.JumlahBarisBahanJV = JumlahBarisBahanJV
            win_VerifikasiDataJurnal.ShowDialog()
            If jur_StatusPenyimpananJurnal_Lengkap = False Then
                JumlahJV_GagalPosting = JumlahJV_GagalPosting + 1
                JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + JumlahBarisBahanJV
                LaporanHasilPostingJurnal &= Enter2Baris & "Nomor " & AwalanNomorJV & jur_NomorJV & " dibuang..!"
                LaporanHasilPostingJurnal &= Enter1Baris & "( Periksa tabel Excel pada baris " & BarisAwalBahanJV & " sampai baris akhir jurnal terkait )."
            End If
        End If
    End Sub

    Sub KirimDataJurnal()
        Dim AdaRevisi = "TIDAK"
        jur_COA = COA
        jur_DK = DK
        jur_JumlahDebet = JumlahDebet
        jur_JumlahKredit = JumlahKredit
        jur_TanggalTransaksi = TanggalJurnal
        jur_Referensi = Referensi
        jur_Bundelan = Bundelan
        jur_TanggalInvoice = TanggalInvoice
        jur_NomorInvoice = NomorInvoice
        jur_NomorFakturPajak = NomorFakturPajak
        jur_NamaLawanTransaksi = Replace(NamaLawanTransaksi, "'", "")
        jur_Direct = 1
        If jur_NamaLawanTransaksi <> NamaLawanTransaksi Then
            LaporanHasilPostingJurnal &= Enter2Baris & "Baris " & Baris + 2 & " pada tabel Excel direvisi."
            LaporanHasilPostingJurnal &= Enter1Baris & "- [" & NamaLawanTransaksi & "] menjadi [" & jur_NamaLawanTransaksi & "]"
            AdaRevisi = "YA"
        End If
        jur_UraianTransaksi = Replace(UraianTransaksi, "'", "")
        If jur_UraianTransaksi <> UraianTransaksi Then
            If AdaRevisi = "TIDAK" Then
                LaporanHasilPostingJurnal &= Enter2Baris & "Baris " & Baris + 2 & " pada tabel Excel direvisi."
            End If
            LaporanHasilPostingJurnal &= Enter1Baris & "- [" & UraianTransaksi & "] menjadi [" & jur_UraianTransaksi & "]"
            AdaRevisi = "YA"
        End If
        If AdaRevisi = "YA" Then
            JumlahBaris_Direvisi = JumlahBaris_Direvisi + 1
        End If
        jur_StatusPenyimpananJurnal_Lengkap = True
        ______________________________________SimpanJurnal_PerBaris()
        Dim PesanError = "Jurnal dengan nomor " & AwalanNomorJV & jur_NomorJV & " tidak tersimpan." & Enter2Baris & "Masalah :"
        If jur_AkunTerdaftar = False Then
            PesanError = PesanError & Enter1Baris & "- COA tidak terdaftar."
        End If
        If TahunTransaksiSesuai = False Then
            PesanError = PesanError & Enter1Baris & "- 'Tahun Transaksi' tidak sesuai dengan 'Tahun Buku Aktif'."
        End If
        If jur_AkunTerdaftar = False Or TahunTransaksiSesuai = False Then
            MsgBox(PesanError & Enter2Baris &
                   "Silakan koreksi baris " & Baris + 2 & " pada tabel Excel.")
        End If
        If jur_StatusPenyimpananJurnal_PerBaris = False Then
            JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + 1
            jur_StatusPenyimpananJurnal_Lengkap = False
        End If
        If jur_StatusPenyimpananJurnal_Lengkap = False And jur_NomorJV <> NomorJV_GagalPosting Then
            HasilPosting = Hasil_BERMASALAH
            BarisAwalBahanJV = Baris - JumlahBarisBahanJV + 2
            BarisAkhirBahanJV = BarisAwalBahanJV - 1 + JumlahBarisBahanJV
            LaporanHasilPostingJurnal &= Enter2Baris & "Nomor " & AwalanNomorJV & jur_NomorJV & " tidak tersimpan..!"
            LaporanHasilPostingJurnal &= Enter1Baris & "( Periksa tabel Excel pada baris " & BarisAwalBahanJV & " sampai baris akhir jurnal terkait )."
            JumlahJV_GagalPosting = JumlahJV_GagalPosting + 1
            JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + JumlahBarisBahanJV
            NomorJV_GagalPosting = jur_NomorJV
        End If
    End Sub



    Public Sub TanyaBatalPostingJurnal()
        Dim PilihBatalPosting = MessageBox.Show("Seluruh proses posting pada event ini akan dibatalkan." & Enter2Baris &
                                                "Yakin akan membatalkan proses posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihBatalPosting = vbYes Then
            StatusPosting = Status_BATAL
            HapusSemuaDataPostinganJurnalEventIni()
        ElseIf PilihBatalPosting = vbNo Then
            StatusPosting = Status_PROSES
        End If
    End Sub

    Sub ProsesPostingGagal()
        HapusSemuaDataPostinganJurnalEventIni()
        MsgBox("Proses GAGAL, karena ada kesalahan format dari sumber data." &
               Enter2Baris & "Silakan perbaiki dan sesuaikan sumber data, kemudian ulangi lagi.")
        MsgBox("Info :" &
               Enter1Baris & "Kesalahan ada pada baris " & Baris + 2 & " tabel Excel sumber data." &
               Enter2Baris & "Tips :" &
               Enter1Baris & "- Susun sumber data sesuai kerangka yang disediakan" &
               Enter1Baris & "- Kolom Tanggal jangan diisi selain data tanggal" &
               Enter1Baris & "- Tahun Jurnal harus seuai dengan 'Tahun Buku Aktif'" &
               Enter1Baris & "- Kolom D/K hanya boleh diisi hufuf D atau huruf K" &
               Enter1Baris & "- Kolom angka jangan disusupi karakter" &
               Enter1Baris & "- Jangan menggunakan tanda petik satu ( ' )" &
               Enter1Baris & "- Setelah baris akhir sumber data, tabel harus clear" &
               "")
        DsImport.Clear()
        StatusPosting = Status_BATAL
    End Sub

    Sub HapusSemuaDataPostinganJurnalEventIni()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand("DELETE FROM tbl_Transaksi WHERE Nomor_ID >= '" & NomorID_AwalPosting & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)
    End Sub

    Private Sub txt_LaporanHasilImport_KeyPress(sender As Object, e As KeyPressEventArgs)
        KunciTotalInputan(sender, e)
    End Sub



    Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)
        ' Enable the button during the process
        Dim button = CType(sender, Button)
        btn_Proses.Visibility = Visibility.Collapsed

        ' Call the asynchronous method to update the progress bar
        Await UpdateProgressBar()

        ' Re-enable the button after the process is complete
        btn_Proses.Visibility = Visibility.Visible
    End Sub

    Private Async Function UpdateProgressBar() As Task
        For i As Integer = 1 To 100
            ' Update the progress bar on the UI thread
            pgb_Progress.Value = i

            ' Wait for 100 milliseconds
            Await Task.Delay(100)
        Next
    End Function



    Dim LaporanHasilPostingJurnal As String
    Sub ResetForm()
        KosongkanValueElemenRichTextBox(txt_LaporanHasilPostingJurnal)
        LaporanHasilPostingJurnal = Enter1Baris & "LAPORAN HASIL POSTING :"
        btn_Proses.Visibility = Visibility.Visible
        btn_Batal.Visibility = Visibility.Visible
        btn_Buang.Visibility = Visibility.Collapsed
        btn_Terapkan.Visibility = Visibility.Collapsed
        txt_LaporanHasilPostingJurnal.Visibility = Visibility.Collapsed
        btn_Proses.Focus()
    End Sub


    Private Sub btn_Buang_Click(sender As Object, e As RoutedEventArgs) Handles btn_Buang.Click
        TanyaBatalPostingJurnal()
        If StatusPosting = Status_BATAL Then
            TutupForm()
        End If
    End Sub


    Private Sub btn_Terapkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Terapkan.Click
        If HasilPosting = Hasil_BERMASALAH Then
            Dim PilihTetapkanHasilPosting = MessageBox.Show("Hasil posting bermasalah..!" & Enter2Baris & "Yakin akan menetapkan hasil posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If PilihTetapkanHasilPosting = vbYes Then
                TutupForm()
            Else
                Return
            End If
        End If
        If HasilPosting = Hasil_NORMAL Then
            If usc_JurnalUmum.StatusAktif Then usc_JurnalUmum.TampilkanData()
            TutupForm()
        End If
    End Sub


    Private Sub btn_Proses_Click(sender As Object, e As RoutedEventArgs) Handles btn_Proses.Click
        SistemPenomoranOtomatis_NomorJV()
        Dim NomorJV_AwalPosting = jur_NomorJV
        NomorJV_GagalPosting = 0
        MsgBox("Penomoran jurnal pada event ini dimulai dari Nomor " & AwalanNomorJV & NomorJV_AwalPosting & "")
        btn_Proses.Visibility = Visibility.Collapsed
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Proses posting sedang berjalan."
        pgb_Progress.Visibility = Visibility.Visible
        StatusPosting = Status_PROSES
        bgw_Posting.RunWorkerAsync()
    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        If btn_Batal.Content = "Batal" Then
            If StatusPosting = Status_PROSES Then
                StatusPosting = Status_TAHAN
            Else
                TanyaBatalPostingJurnal()
            End If
        End If
        If StatusPosting = Status_BATAL Or StatusPosting = Status_SELESAI Then
            TutupForm()
        End If
    End Sub
    Sub TutupForm()
        Me.Close()
    End Sub



    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_LaporanHasilPostingJurnal.IsReadOnly = True
        bgw_Posting = New BackgroundWorker
        bgw_Posting.WorkerReportsProgress = True
        bgw_Posting.WorkerSupportsCancellation = True
        bgw_ImportDataExcel = New BackgroundWorker
        bgw_ImportDataExcel.WorkerReportsProgress = True
        bgw_ImportDataExcel.WorkerSupportsCancellation = True
        DsImport = New DataSet
        ofd_Import = New OpenFileDialog
        datatabelBahanImpor = New DataTable
    End Sub


    'Sub StyleWindowDialogWPF_TanpaX(Window As Window)
    '    Window.WindowStyle = WindowStyle.ToolWindow
    '    Dim hwnd = New WindowInteropHelper(Window).Handle
    '    Dim currentStyle As Integer = GetWindowLong(hwnd, GWL_STYLE)
    '    SetWindowLong(hwnd, GWL_STYLE, currentStyle And Not WS_SYSMENU)
    'End Sub

    '<DllImport("user32.dll", SetLastError:=True)>
    'Public Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    'End Function

    '<DllImport("user32.dll")>
    'Private Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    'End Function

    'Private Const GWL_STYLE As Integer = -16
    'Private Const WS_SYSMENU As Integer = &H80000


End Class