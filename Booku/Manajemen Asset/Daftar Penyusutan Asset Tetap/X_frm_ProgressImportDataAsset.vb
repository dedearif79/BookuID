Imports bcomm
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class X_frm_ProgressImportDataAsset

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
    Public DsImport As New DataSet
    Dim MyProses As New Process

    Dim cmdDIVISI As OdbcCommand
    Dim drDIVISI As OdbcDataReader
    Dim cmdCOA As OdbcCommand
    Dim drCOA As OdbcDataReader

    Private Sub frm_ProgressImportDataAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TrialBalance_Mentahkan()

        win_InputDataAsset = New wpfWin_InputDataAsset

        Me.Text = "Proses..."
        Me.Height = 177
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
            'ConnImport = New OleDbConnection("provider=Microsoft.jet.OLEDB.4.0;" & _
            '                                 "data source='" & ofd_Import.FileName & "';Extended Properties=Excel 8.0;")
            ConnImport = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" &
                                             "Data Source=" & ofd_Import.FileName & ";" &
                                             "Extended Properties='Excel 12.0 Xml;HDR=YES';")
            DaImport = New OleDbDataAdapter("SELECT * FROM [Bahan Data Asset Tetap$]", ConnImport)
            bgw_ImportDataExcel.RunWorkerAsync()
        End If

    End Sub

    Private Sub bgw_ImportDataExcel_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw_ImportDataExcel.DoWork

        StatusImport = Status_SUKSES

        Try
            ConnImport.Open()
            DsImport.Clear()
            DaImport.Fill(DsImport)
            DataGridView.DataSource = DsImport.Tables(0)
            ConnImport.Close()
        Catch ex As Exception
            StatusImport = Status_GAGAL
        End Try

    End Sub

    Private Sub bgw_ImportDataExcel_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw_ImportDataExcel.ProgressChanged

    End Sub

    Private Sub bgw_ImportDataExcel_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw_ImportDataExcel.RunWorkerCompleted

        Dim Baris = 0
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

        For Each row As DataGridViewRow In DataGridView.Rows
            Baris += 1
            IsiNamaAktiva = row.Cells(0).Value.ToString
            IsiCOA_Asset = row.Cells(1).Value.ToString
            IsiCOA_BiayaPenyusutan = row.Cells(2).Value.ToString
            IsiCOA_AkumulasiPenyusutan = row.Cells(3).Value.ToString
            IsiKelompokHarta = row.Cells(4).Value.ToString
            IsiKodeDivisi = row.Cells(5).Value.ToString
            IsiTanggalPerolehan = row.Cells(6).Value.ToString
            IsiHargaPerolehan = row.Cells(7).Value.ToString
            TahunPerolehan = Microsoft.VisualBasic.Left(TanggalFormatWPF(IsiTanggalPerolehan), 4)
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
                        MsgBox("Tahun Perolehan di baris ke-" & Baris & " pada sumber data tidak sesuai, karena melebihi 'Tahun Buku Aktif'.")
                    End If
                Case JenisTahunBuku_NORMAL
                    If AmbilAngka(TahunPerolehan) <> TahunBukuAktif Then
                        StatusImport = Status_DATARUSAK
                        MsgBox("Tahun Perolehan di baris ke-" & Baris & " pada sumber data tidak sesuai dengan 'Tahun Buku Aktif'.")
                    End If
            End Select
        Next

        If JumlahBaris_SumberData >= BatasLimitJumlahBaris Then
            StatusImport = Status_BARISLEBIH
        End If

        If StatusImport = Status_DATARUSAK Then
            MsgBox("Import Dibatalkan..!!!" & Enter2Baris & "Format sumber data tidak sesuai." & Enter2Baris & "Silakan perbaiki terlebih dahulu sebelum kembali melakukan import.")
            TutupForm()
        End If

        If StatusImport = Status_GAGAL Then
            MsgBox("Silakan buka/aktifkan terlebih dahulu file yang bersangkutan menggunakan Excel, dan jalankan kembali fitur ini.")
            TutupForm()
        End If

        If StatusImport = Status_BARISLEBIH Then
            MsgBox("Sistem import tidak mengizinkan data melebihi baris " & BatasLimitJumlahBaris & " pada tabel Excel dalam sekali event." & _
                   Enter2Baris & "Silakan Anda pecah sumber data menjadi beberapa partisi, dan ulangi kembali." & _
                   "")
            DsImport.Clear()
            TutupForm()
        End If

        If StatusImport = Status_SUKSES Then
            Me.Height = 267
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

        StatusPosting = Status_PROSES
        HasilPosting = Hasil_NORMAL
        WaktuKerja = 0
        Baris = 0
        BarisAwalBahanDataAsset = 0
        JumlahBarisBahanDataAsset = 0
        JumlahBaris_Terposting = 0
        JumlahBaris_GagalPosting = 0
        BeginInvoke(Sub()
                        win_InputDataAsset.SistemPenomoranOtomatis_KodeAsset()
                    End Sub)
        lbl_ProgressReport.Text = Nothing
        lbl_ProgressReport.Visible = True

        Do While Baris < JumlahBaris_SumberData

            DataValid = True

            PesanMasalah = "Ada masalah pada baris " & Baris + 2 & " di tabel Excel."

            WaktuKerja = WaktuKerja + 1
            If WaktuKerja = WaktuIstirahat Then
                WaktuKerja = 0
                lbl_ProgressReport.Text = lbl_ProgressReport.Text & "  |  [ Break... ]"
                System.Threading.Thread.Sleep(DurasiIstirahatSejenak * 1000)
            End If
            If Baris = 5000 Then System.Threading.Thread.Sleep(DurasiIstirahatLama * 1000)

            Try
                NamaAktiva = DataGridView.Item(0, Baris).Value
                KodeAkun_Asset = DataGridView.Item(1, Baris).Value
                KodeAkun_BiayaPenyusutan = DataGridView.Item(2, Baris).Value
                KodeAkun_AkumulasiPenyusutan = DataGridView.Item(3, Baris).Value
                KelompokHarta = AmbilAngka(DataGridView.Item(4, Baris).Value)
                Angka_KodeDivisi = AmbilAngka(DataGridView.Item(5, Baris).Value)
                TanggalPerolehan = DataGridView.Item(6, Baris).Value
                HargaPerolehan = AmbilAngka(DataGridView.Item(7, Baris).Value)
                Keterangan = DataGridView.Item(8, Baris).Value
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

            'Ambil Value Divisi :
            cmdDIVISI = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset WHERE Kode_Divisi = '" & KodeDivisi & "' ", KoneksiDatabaseGeneral)
            drDIVISI = cmdDIVISI.ExecuteReader
            drDIVISI.Read()
            If drDIVISI.HasRows Then
                Divisi = drDIVISI.Item("Divisi")
            Else
                DataValid = False
                Divisi = Nothing
                HasilPosting = Hasil_BERMASALAH
                PesanMasalah = PesanMasalah & Enter2Baris & " - 'Kode Divisi' tidak terdaftar."
            End If

            'Ambil Value Akun Asset :
            cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeAkun_Asset & "' ", KoneksiDatabaseGeneral)
            drCOA = cmdCOA.ExecuteReader
            drCOA.Read()
            If drCOA.HasRows Then
                NamaAkun_Asset = drCOA.Item("Nama_Akun")
            Else
                DataValid = False
                NamaAkun_Asset = Nothing
                HasilPosting = Hasil_BERMASALAH
                PesanMasalah = PesanMasalah & Enter2Baris & " - 'COA Asset' tidak terdaftar."
            End If

            'Ambil Value Akun Biaya Penyusutan :
            cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeAkun_BiayaPenyusutan & "' ", KoneksiDatabaseGeneral)
            drCOA = cmdCOA.ExecuteReader
            drCOA.Read()
            If drCOA.HasRows Then
                NamaAkun_BiayaPenyusutan = drCOA.Item("Nama_Akun")
            Else
                DataValid = False
                NamaAkun_BiayaPenyusutan = Nothing
                HasilPosting = Hasil_BERMASALAH
                PesanMasalah = PesanMasalah & Enter2Baris & " - 'COA Biaya Penyusutan' tidak terdaftar."
            End If

            AksesDatabase_General(Tutup)

            JumlahBarisBahanDataAsset += 1
            If StatusPosting = Status_BATAL Then Return
            If DataValid = True Then
                BeginInvoke(Sub()
                                win_InputDataAsset.SistemPenomoranOtomatis_KodeAsset()
                                IdAsset = win_InputDataAsset.IdAsset
                                Dim TahunPerolehan = Format(TanggalPerolehan, "yyyy").ToString
                                Dim BulanPerolehan = Format(TanggalPerolehan, "MM").ToString
                                KodeAsset = KodeAkun_Asset & "-" & KodeDivisi & "-" & TahunPerolehan & "-" & BulanPerolehan & "-" & IdAsset
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
                                    'Kode Apabila gagal penyimpanan :
                                    JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + 1
                                    HasilPosting = Hasil_BERMASALAH
                                    MsgBox("Baris ke " & Baris + 2 & " pada tabel Excel gagal tersimpan ke database.")
                                End Try
                                AksesDatabase_General(Tutup)
                            End Sub)
            Else
                JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + 1
                MsgBox(PesanMasalah)
            End If
            If StatusPosting = Status_TAHAN Then
                TanyaBatalPostingDataAsset()
            End If
            Baris = Baris + 1
            pgb_Progress.Value = Baris
            lbl_ProgressReport.Text = "Progress :  Baris " & Baris + 1
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
            pgb_Progress.Visible = False
            StatusPosting = Status_SELESAI
            pgb_Progress.Visible = False
            btn_Batal.Text = "Tutup"
            lbl_ProgressReport.Visible = False
            btn_Proses.Enabled = False
            btn_Batal.Enabled = False
            btn_BuangHasilPosting.Enabled = True
            btn_TetapkanHasilPosting.Enabled = True
            If HasilPosting = Hasil_NORMAL Then
                Me.Text = "Sukses..!"
                lbl_Baris_01.Text = "Sukses..!"
                lbl_Baris_02.Text = "Data Jurnal berhasil diposting seluruhnya."
                lbl_Baris_01.ForeColor = Color.Black
                lbl_Baris_02.ForeColor = Color.Black
                txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Data berhasil diposting seluruhnya."
                btn_TetapkanHasilPosting.Focus()
            End If
            If HasilPosting = Hasil_BERMASALAH Then
                Me.Text = "Peringatan..!"
                lbl_Baris_01.Text = "Hasil posting bermasalah..!"
                lbl_Baris_02.Text = "Silakan periksa laporan."
                lbl_Baris_01.ForeColor = Color.Red
                lbl_Baris_02.ForeColor = Color.Red
                btn_BuangHasilPosting.Focus()
            End If
        End If

        Me.Height = 375
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
        SistemPenomoranOtomatis_NomorJV()
        Dim NomorJV_AwalPosting = jur_NomorJV
        MsgBox("Harap tunggu, dan jangan hentikan aplikasi selama proses import berjalan..!")
        btn_Proses.Enabled = False
        lbl_Baris_01.Text = "Harap tunggu..."
        lbl_Baris_02.Text = "Proses posting sedang berjalan."
        pgb_Progress.Visible = True
        StatusPosting = Status_PROSES
        bgw_Posting.RunWorkerAsync()
    End Sub

    Public Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        If btn_Batal.Text = "Batal" Then
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
        Dim PilihBatalPosting = MessageBox.Show("Seluruh proses posting pada event ini akan dibatalkan." _
                                                & Enter2Baris & "Yakin akan membatalkan proses posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihBatalPosting = vbYes Then
            StatusPosting = Status_BATAL
            HapusSemuaDataPostinganPadaEventIni()
        ElseIf PilihBatalPosting = vbNo Then
            StatusPosting = Status_PROSES
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
        StatusPosting = Status_BATAL
    End Sub

    Sub HapusSemuaDataPostinganPadaEventIni()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand("DELETE FROM tbl_DataAsset WHERE Nomor_ID >= '" & NomorID_AwalPosting & "' ", KoneksiDatabaseGeneral)
        cmd.ExecuteNonQuery()
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub txt_LaporanHasilImport_KeyPress(sender As Object, e As KeyPressEventArgs)
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_TetapkanHasilPosting_Click(sender As Object, e As EventArgs) Handles btn_TetapkanHasilPosting.Click
        If HasilPosting = Hasil_BERMASALAH Then
            Dim PilihTetapkanHasilPosting = MessageBox.Show("Hasil posting bermasalah..!" _
                                                            & Enter2Baris & "Yakin akan menetapkan hasil posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
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

    Private Sub btn_BuangHasilPosting_Click(sender As Object, e As EventArgs) Handles btn_BuangHasilPosting.Click
        TanyaBatalPostingDataAsset()
        If StatusPosting = Status_BATAL Then
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