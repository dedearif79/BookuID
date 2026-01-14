Imports bcomm
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports Microsoft.Win32
Imports System.Threading.Tasks

Public Class wpfWin_ProgressImportDataAsset

    Public BarisAwalBahanDataAsset
    Public BarisAkhirBahanDataAsset

    Dim JumlahMaksimalPilihanKelompokHarta = 6

    Dim IdAsset
    Dim KodeAsset
    Dim NamaAktiva
    Dim KodeAkun_Asset
    Dim NamaAkun_Asset
    Dim KodeAkun_BiayaPenyusutan
    Dim NamaAkun_BiayaPenyusutan
    Dim KodeAkun_AkumulasiPenyusutan
    Dim KelompokHarta
    Dim MasaManfaat
    Dim Angka_KodeDivisi As Long
    Dim KodeDivisi As String
    Dim Divisi
    Dim TanggalPerolehan
    Dim HargaPerolehan As Int64
    Dim Keterangan

    Dim Baris
    Dim JumlahBaris_SumberData
    Dim JumlahBaris_Terposting
    Dim JumlahBaris_GagalPosting
    Dim JumlahBarisBahanDataAsset
    Dim StatusImport
    Dim WaktuKerja
    Dim WaktuIstirahat = 1000
    Dim BatasLimitJumlahBaris = 5000
    Dim DurasiIstirahatSejenak = 1 'Detik
    Dim DurasiIstirahatLama = 5 'Detik
    Dim DataValid As Boolean
    Dim PesanMasalah

    Dim ConnImport As OleDbConnection
    Dim DaImport As OleDbDataAdapter
    Public DsImport As DataSet

    Dim ofd_Import As OpenFileDialog
    Dim datatabelBahanImpor As DataTable

    Dim LaporanHasilPosting As String


    Private Async Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StyleWindowDialogWPF_TanpaTombolX(Me)

        'TrialBalance_Mentahkan()

        win_InputDataAsset = New wpfWin_InputDataAsset

        Title = "Proses..."
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Sistem sedang menganalisa sumber data."
        lbl_ProgressReport.Visibility = Visibility.Collapsed
        btn_Proses.Visibility = Visibility.Collapsed
        btn_Batal.Visibility = Visibility.Collapsed
        btn_Buang.Visibility = Visibility.Collapsed
        btn_Terapkan.Visibility = Visibility.Collapsed
        pgb_Progress.Visibility = Visibility.Collapsed
        txt_LaporanHasilPosting.Visibility = Visibility.Collapsed

        Await ImportDataExcelAsync()
        ResetForm()

    End Sub


    Sub ResetForm()
        KosongkanValueElemenRichTextBox(txt_LaporanHasilPosting)
        LaporanHasilPosting = Enter1Baris & "LAPORAN HASIL POSTING :"
        lbl_Baris_01.Foreground = WarnaTeksStandar_WPF
        lbl_Baris_02.Foreground = WarnaTeksStandar_WPF
        btn_Proses.Visibility = Visibility.Visible
        btn_Batal.Visibility = Visibility.Visible
        btn_Buang.Visibility = Visibility.Collapsed
        btn_Terapkan.Visibility = Visibility.Collapsed
        txt_LaporanHasilPosting.Visibility = Visibility.Collapsed
        btn_Proses.Focus()
    End Sub


    Async Function ImportDataExcelAsync() As Task

        ofd_Import.FileName = Kosongan
        ofd_Import.Filter = "(*xlsx)|*xlsx|(*xls)|*xls|All Files(*.*)|*.*"
        ofd_Import.ShowDialog()

        If ofd_Import.FileName = Kosongan Then
            Me.Close()
            Return
        End If

        ConnImport = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" &
                                         "Data Source=" & ofd_Import.FileName & ";" &
                                         "Extended Properties='Excel 12.0 Xml;HDR=YES';")
        DaImport = New OleDbDataAdapter("SELECT * FROM [Bahan Data Asset Tetap$]", ConnImport)

        StatusImport = Status_SUKSES

        Await Task.Run(
            Sub()
                Try
                    ConnImport.Open()
                    DsImport.Clear()
                    DaImport.Fill(DsImport)
                    datatabelBahanImpor = DsImport.Tables(0)
                    ConnImport.Close()
                Catch ex As Exception
                    StatusImport = Status_GAGAL
                End Try
            End Sub)

        ProsesValidasiDataExcel()

    End Function


    Sub ProsesValidasiDataExcel()

        pnl_TombolForm.IsEnabled = True

        Dim BarisIterasi = 0
        Dim IsiNamaAktiva As String
        Dim IsiCOA_Asset As String
        Dim IsiCOA_BiayaPenyusutan As String
        Dim IsiCOA_AkumulasiPenyusutan As String
        Dim IsiKelompokHarta As String
        Dim IsiKodeDivisi As String
        Dim IsiTanggalPerolehan As String
        Dim IsiHargaPerolehan
        Dim TahunPerolehan

        JumlahBaris_SumberData = 0

        For Each row As DataRow In datatabelBahanImpor.Rows
            BarisIterasi += 1
            IsiNamaAktiva = row(0).ToString
            IsiCOA_Asset = row(1).ToString
            IsiCOA_BiayaPenyusutan = row(2).ToString
            IsiCOA_AkumulasiPenyusutan = row(3).ToString
            IsiKelompokHarta = row(4).ToString
            IsiKodeDivisi = row(5).ToString
            IsiTanggalPerolehan = row(6).ToString
            IsiHargaPerolehan = row(7).ToString
            TahunPerolehan = AmbilKiri(TanggalFormatWPF(IsiTanggalPerolehan), 4)
            If IsiNamaAktiva <> Nothing Then
                JumlahBaris_SumberData += 1
                If IsiCOA_Asset = Nothing _
                    Or IsiCOA_BiayaPenyusutan = Nothing _
                    Or IsiKelompokHarta = Nothing _
                    Or IsiKodeDivisi = Nothing _
                    Or IsiTanggalPerolehan = Nothing _
                    Or IsiHargaPerolehan = Nothing _
                    Then
                    StatusImport = Status_DATARUSAK
                End If
            Else
                If IsiCOA_Asset <> Nothing _
                    Or IsiCOA_BiayaPenyusutan <> Nothing _
                    Or IsiKelompokHarta <> Nothing _
                    Or IsiKodeDivisi <> Nothing _
                    Or IsiTanggalPerolehan <> Nothing _
                    Or IsiHargaPerolehan <> Nothing _
                    Then
                    StatusImport = Status_DATARUSAK
                End If
            End If
            If (AmbilAngka(IsiKelompokHarta) > JumlahMaksimalPilihanKelompokHarta) Or (AmbilAngka(IsiKelompokHarta) < 1) Then StatusImport = Status_DATARUSAK
            Select Case JenisTahunBuku
                Case JenisTahunBuku_LAMPAU
                    If AmbilAngka(TahunPerolehan) > TahunBukuAktif Then
                        StatusImport = Status_DATARUSAK
                        Pesan_Peringatan("Tahun Perolehan di baris ke-" & BarisIterasi & " pada sumber data tidak sesuai, karena melebihi 'Tahun Buku Aktif'.")
                    End If
                Case JenisTahunBuku_NORMAL
                    If AmbilAngka(TahunPerolehan) <> TahunBukuAktif Then
                        StatusImport = Status_DATARUSAK
                        Pesan_Peringatan("Tahun Perolehan di baris ke-" & BarisIterasi & " pada sumber data tidak sesuai dengan 'Tahun Buku Aktif'.")
                    End If
            End Select
        Next

        If JumlahBaris_SumberData >= BatasLimitJumlahBaris Then
            StatusImport = Status_BARISLEBIH
        End If

        If StatusImport = Status_DATARUSAK Then
            Pesan_Peringatan("Import Dibatalkan..!!!" & Enter2Baris & "Format sumber data tidak sesuai." & Enter2Baris & "Silakan perbaiki terlebih dahulu sebelum kembali melakukan import.")
            TutupForm()
            Return
        End If

        If StatusImport = Status_GAGAL Then
            Pesan_Peringatan("Silakan buka/aktifkan terlebih dahulu file yang bersangkutan menggunakan Excel, dan jalankan kembali fitur ini.")
            TutupForm()
            Return
        End If

        If StatusImport = Status_BARISLEBIH Then
            Pesan_Peringatan("Sistem import tidak mengizinkan data melebihi baris " & BatasLimitJumlahBaris & " pada tabel Excel dalam sekali event." &
                   Enter2Baris & "Silakan Anda pecah sumber data menjadi beberapa partisi, dan ulangi kembali." &
                   "")
            DsImport.Clear()
            TutupForm()
            Return
        End If

        If StatusImport = Status_SUKSES Then
            If JumlahBaris_SumberData = 0 Then
                Pesan_Informasi("Tidak ada data yang bisa diimpor.")
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


    Async Function ProsesPostingDataAssetAsync() As Task

        StatusPosting = Status_PROSES
        HasilPosting = Hasil_NORMAL
        WaktuKerja = 0
        Baris = 0
        BarisAwalBahanDataAsset = 0
        JumlahBarisBahanDataAsset = 0
        JumlahBaris_Terposting = 0
        JumlahBaris_GagalPosting = 0

        win_InputDataAsset.SistemPenomoranOtomatis_KodeAsset()
        lbl_ProgressReport.Text = Kosongan
        lbl_ProgressReport.Visibility = Visibility.Visible

        Do While Baris < JumlahBaris_SumberData

            DataValid = True

            PesanMasalah = "Ada masalah pada baris " & Baris + 2 & " di tabel Excel."

            WaktuKerja = WaktuKerja + 1
            If WaktuKerja = WaktuIstirahat Then
                WaktuKerja = 0
                lbl_ProgressReport.Text = lbl_ProgressReport.Text & "  |  [ Break... ]"
                Await Task.Delay(DurasiIstirahatSejenak * 1000)
            End If
            If Baris = 5000 Then Await Task.Delay(DurasiIstirahatLama * 1000)

            Try
                NamaAktiva = datatabelBahanImpor.Rows(Baris)(0)
                KodeAkun_Asset = datatabelBahanImpor.Rows(Baris)(1)
                KodeAkun_BiayaPenyusutan = datatabelBahanImpor.Rows(Baris)(2)
                KodeAkun_AkumulasiPenyusutan = datatabelBahanImpor.Rows(Baris)(3)
                KelompokHarta = AmbilAngka(datatabelBahanImpor.Rows(Baris)(4))
                Angka_KodeDivisi = AmbilAngka(datatabelBahanImpor.Rows(Baris)(5))
                TanggalPerolehan = datatabelBahanImpor.Rows(Baris)(6)
                HargaPerolehan = AmbilAngka(datatabelBahanImpor.Rows(Baris)(7))
                Keterangan = datatabelBahanImpor.Rows(Baris)(8)
            Catch ex As Exception
                ProsesPostingGagal()
                Return
            End Try

            MasaManfaat = 0
            If KelompokHarta = 1 Then MasaManfaat = 4
            If KelompokHarta = 2 Then MasaManfaat = 8
            If KelompokHarta = 3 Then MasaManfaat = 16
            If KelompokHarta = 4 Then MasaManfaat = 20
            If KelompokHarta = 5 Then MasaManfaat = 20
            If KelompokHarta = 6 Then MasaManfaat = 10
            If KelompokHarta > JumlahMaksimalPilihanKelompokHarta Or KelompokHarta < 1 Then
                DataValid = False
                HasilPosting = Hasil_BERMASALAH
                PesanMasalah = PesanMasalah & Enter2Baris & " - Data pada kolom 'Kelompok Harta' tidak sesuai ketentuan."
            End If
            KodeDivisi = Angka_KodeDivisi.ToString
            If Angka_KodeDivisi < 100 Then KodeDivisi = "0" & Angka_KodeDivisi.ToString
            If Angka_KodeDivisi < 10 Then KodeDivisi = "00" & Angka_KodeDivisi.ToString

            AksesDatabase_General(Buka)

            Divisi = AmbilValue_DivisiAsset(KodeDivisi)
            If Divisi = Kosongan Then
                DataValid = False
                Divisi = Nothing
                HasilPosting = Hasil_BERMASALAH
                PesanMasalah = PesanMasalah & Enter2Baris & " - 'Kode Divisi' tidak terdaftar."
            End If

            NamaAkun_Asset = AmbilValue_NamaAkun(KodeAkun_Asset)
            If NamaAkun_Asset = Kosongan Then
                DataValid = False
                NamaAkun_Asset = Nothing
                HasilPosting = Hasil_BERMASALAH
                PesanMasalah = PesanMasalah & Enter2Baris & " - 'COA Asset' tidak terdaftar."
            End If

            NamaAkun_BiayaPenyusutan = AmbilValue_NamaAkun(KodeAkun_BiayaPenyusutan)
            If NamaAkun_BiayaPenyusutan = Kosongan Then
                DataValid = False
                NamaAkun_BiayaPenyusutan = Nothing
                HasilPosting = Hasil_BERMASALAH
                PesanMasalah = PesanMasalah & Enter2Baris & " - 'COA Biaya Penyusutan' tidak terdaftar."
            End If

            AksesDatabase_General(Tutup)

            JumlahBarisBahanDataAsset += 1
            If StatusPosting = Status_BATAL Then Return
            If DataValid = True Then
                win_InputDataAsset.SistemPenomoranOtomatis_KodeAsset()
                IdAsset = win_InputDataAsset.IdAsset
                Dim TahunPerolehan_Lokal = Format(TanggalPerolehan, "yyyy").ToString
                Dim BulanPerolehan_Lokal = Format(TanggalPerolehan, "MM").ToString
                KodeAsset = KodeAkun_Asset & "-" & KodeDivisi & "-" & TahunPerolehan_Lokal & "-" & BulanPerolehan_Lokal & "-" & IdAsset
                AksesDatabase_General(Buka)
                If StatusKoneksiDatabase = False Then Return
                cmd = New OdbcCommand(" INSERT INTO tbl_DataAsset VALUES ( " &
                      " '" & IdAsset & "', " &
                      " '" & Kosongan & "', " &
                      " '" & KodeAsset & "', " &
                      " '" & NamaAktiva & "', " &
                      " '" & KodeAkun_Asset & "', " &
                      " '" & KodeAkun_BiayaPenyusutan & "', " &
                      " '" & KodeAkun_AkumulasiPenyusutan & "', " &
                      " '" & KelompokHarta & "', " &
                      " '" & MasaManfaat & "', " &
                      " '" & KodeDivisi & "', " &
                      " '" & Divisi & "', " &
                      " '" & TanggalFormatSimpan(TanggalPerolehan) & "', " &
                      " '" & HargaPerolehan & "', " &
                      " '" & 0 & "', " &
                      " '" & 0 & "', " &
                      " '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                      " '" & Kosongan & "', " &
                      " '" & 0 & "', " &
                      " '" & Keterangan & "' " &
                      " ) ", KoneksiDatabaseGeneral)
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + 1
                    HasilPosting = Hasil_BERMASALAH
                    Pesan_Gagal("Baris ke " & Baris + 2 & " pada tabel Excel gagal tersimpan ke database.")
                End Try
                AksesDatabase_General(Tutup)
            Else
                JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + 1
                Pesan_Peringatan(PesanMasalah)
            End If
            If StatusPosting = Status_TAHAN Then
                TanyaBatalPostingDataAsset()
            End If
            Baris = Baris + 1
            pgb_Progress.Value = Baris
            lbl_ProgressReport.Text = "Progress :  Baris " & Baris + 1

            Await Task.Delay(1)

        Loop

        If StatusPosting = Status_BATAL Then Return

    End Function


    Sub TampilkanHasilPosting()

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
                lbl_Baris_02.Text = "Data Asset berhasil diposting seluruhnya."
                lbl_Baris_01.Foreground = WarnaTeksStandar_WPF
                lbl_Baris_02.Foreground = WarnaTeksStandar_WPF
                LaporanHasilPosting &= Enter2Baris & "Data berhasil diposting seluruhnya."
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

        JumlahBaris_Terposting = JumlahBaris_SumberData - JumlahBaris_GagalPosting
        txt_LaporanHasilPosting.Visibility = Visibility.Visible
        LaporanHasilPosting &= Enter2Baris & "_____________________________________________________"
        LaporanHasilPosting &= Enter2Baris & "RESUME :"
        LaporanHasilPosting &= Enter2Baris & "Sumber Data :"
        LaporanHasilPosting &= Enter1Baris & JumlahBaris_SumberData & " baris."
        LaporanHasilPosting &= Enter2Baris & "Data Terposting :"
        LaporanHasilPosting &= Enter1Baris & JumlahBaris_Terposting & " baris."
        LaporanHasilPosting &= Enter2Baris & "Data Gagal Terposting :"
        LaporanHasilPosting &= Enter1Baris & JumlahBaris_GagalPosting & " baris."
        LaporanHasilPosting &= Enter1Baris
        IsiValueElemenRichTextBox(txt_LaporanHasilPosting, LaporanHasilPosting)
        txt_LaporanHasilPosting.Visibility = Visibility.Visible

    End Sub


    Private Async Sub btn_Proses_Click(sender As Object, e As RoutedEventArgs) Handles btn_Proses.Click
        SistemPenomoranOtomatis_NomorJV()
        Dim NomorJV_AwalPosting = jur_NomorJV
        Pesan_Informasi("Harap tunggu, dan jangan hentikan aplikasi selama proses import berjalan..!")
        btn_Proses.Visibility = Visibility.Collapsed
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Proses posting sedang berjalan."
        pgb_Progress.Visibility = Visibility.Visible
        StatusPosting = Status_PROSES
        Await ProsesPostingDataAssetAsync()
        TampilkanHasilPosting()
    End Sub


    Public Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        If btn_Batal.Content = "Batal" Then
            If StatusPosting = Status_PROSES Then
                StatusPosting = Status_TAHAN
            Else
                TanyaBatalPostingDataAsset()
            End If
        End If
        If StatusPosting = Status_BATAL Or StatusPosting = Status_SELESAI Then
            TutupForm()
        End If
    End Sub


    Public Sub TanyaBatalPostingDataAsset()
        Dim PilihBatalPosting = MessageBox.Show("Seluruh proses posting pada event ini akan dibatalkan." &
                                                Enter2Baris & "Yakin akan membatalkan proses posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihBatalPosting = vbYes Then
            StatusPosting = Status_BATAL
            HapusSemuaDataPostinganPadaEventIni()
        ElseIf PilihBatalPosting = vbNo Then
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
               Enter1Baris & "- Setelah baris akhir sumber data, tabel harus clear" &
               "")
        DsImport.Clear()
        StatusPosting = Status_BATAL
    End Sub


    Sub HapusSemuaDataPostinganPadaEventIni()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand("DELETE FROM tbl_DataAsset WHERE Nomor_ID >= '" & NomorID_AwalPosting & "' ", KoneksiDatabaseGeneral)
        cmd.ExecuteNonQuery()
        AksesDatabase_General(Tutup)
    End Sub


    Private Sub btn_Terapkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Terapkan.Click
        If HasilPosting = Hasil_BERMASALAH Then
            Dim PilihTetapkanHasilPosting = MessageBox.Show("Hasil posting bermasalah..!" &
                                                            Enter2Baris & "Yakin akan menetapkan hasil posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If PilihTetapkanHasilPosting = vbYes Then
                If usc_DaftarPenyusutanAssetTetap.StatusAktif Then usc_DaftarPenyusutanAssetTetap.TampilkanData()
                TutupForm()
            Else
                Return
            End If
        End If
        If HasilPosting = Hasil_NORMAL Then
            If usc_DaftarPenyusutanAssetTetap.StatusAktif Then usc_DaftarPenyusutanAssetTetap.TampilkanData()
            TutupForm()
        End If
    End Sub


    Private Sub btn_Buang_Click(sender As Object, e As RoutedEventArgs) Handles btn_Buang.Click
        TanyaBatalPostingDataAsset()
        If StatusPosting = Status_BATAL Then
            TutupForm()
        End If
    End Sub


    Sub TutupForm()
        Me.Close()
    End Sub



    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Me.SizeToContent = SizeToContent.Height
        Me.Width = 520
        Me.MaxHeight = 600
        txt_LaporanHasilPosting.IsReadOnly = True
        DsImport = New DataSet
        ofd_Import = New OpenFileDialog
        datatabelBahanImpor = New DataTable
    End Sub

End Class
