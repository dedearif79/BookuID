Imports bcomm
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_ProgressImportJurnal

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
    Dim BatasLimitJumlahBaris = 5000
    Dim DurasiIstirahatSejenak = 1 'Detik
    Dim DurasiIstirahatLama = 5 'Detik

    Dim ConnImport As OleDbConnection
    Dim DaImport As OleDbDataAdapter
    Public DsImport As New DataSet
    Dim MyProses As New Process

    Dim TahunTransaksiSesuai As Boolean

    Private Sub frm_ProgressImportJurnal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        ResetValueJurnal()  'Jangan dihapus, dan jangan dipindahkan....!!!!!!!!!!!!
        NomorID_AwalPosting = jur_NomorID + 1  'Untuk mengetahui Nomor ID paling awal pada event import..!!!!!
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
            DataGridView.DataSource = DsImport.Tables(0)
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
        Dim JumlahBaris = DataGridView.RowCount - 1
        Dim IsiGJ As String
        Dim IsiCOA As String
        Dim IsiCOA_Int As Int64 = 0

        JumlahBaris_SumberData = 0

        For Each row As DataGridViewRow In DataGridView.Rows
            IsiCOA = row.Cells(0).Value.ToString
            IsiGJ = row.Cells(2).Value.ToString
            IsiCOA_Int = AmbilAngka(IsiCOA)
            If IsiCOA_Int >= 10000 Then
                JumlahBaris_SumberData += 1
            Else
                If IsiGJ <> Nothing Then
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
            MsgBox("Sistem import tidak mengizinkan data melebihi baris " & BatasLimitJumlahBaris & " pada tabel Excel dalam sekali event." & _
                   Enter2Baris & "Silakan Anda pecah sumber data menjadi beberapa partisi, dan ulangi kembali." & _
                   "")
            DsImport.Clear()
            TutupForm()
        End If

        If StatusImport = Status_SUKSES Then
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
        lbl_ProgressReport.Text = Nothing
        lbl_ProgressReport.Visible = True

        Do While Baris < JumlahBaris_SumberData
            WaktuKerja = WaktuKerja + 1
            If WaktuKerja = WaktuIstirahat Then
                WaktuKerja = 0
                lbl_ProgressReport.Text = lbl_ProgressReport.Text & "  |  [ Break... ]"
                Threading.Thread.Sleep(DurasiIstirahatSejenak * 1000)
            End If
            If Baris = 5000 Then
                Threading.Thread.Sleep(DurasiIstirahatLama * 1000)
            End If
            JumlahDebetSebelumnya = JumlahDebet
            JumlahKreditSebelumnya = JumlahKredit
            TotalDebet += JumlahDebetSebelumnya
            TotalKredit += JumlahKreditSebelumnya
            Try
                COA = DataGridView.Item(0, Baris).Value
                TanggalJurnal = TanggalFormatSimpan(DataGridView.Item(1, Baris).Value)
                TahunJurnal = Format(DataGridView.Item(1, Baris).Value, "yyyy")
                If TahunJurnal = TahunBukuAktif Then
                    TahunTransaksiSesuai = True
                    jur_TahunTransaksiSesuai = True
                Else
                    TahunTransaksiSesuai = False
                    jur_TahunTransaksiSesuai = False
                End If
                NomorGJ = DataGridView.Item(2, Baris).Value.ToString
                Referensi = DataGridView.Item(3, Baris).Value.ToString
                Bundelan = DataGridView.Item(4, Baris).Value.ToString
                TanggalInvoice = DataGridView.Item(5, Baris).Value.ToString
                If Len(TanggalInvoice) > 10 Then TanggalInvoice = Microsoft.VisualBasic.Left(TanggalInvoice, 10)
                NomorInvoice = DataGridView.Item(6, Baris).Value.ToString
                NomorFakturPajak = DataGridView.Item(7, Baris).Value.ToString
                NamaLawanTransaksi = DataGridView.Item(8, Baris).Value.ToString
                DK = DataGridView.Item(9, Baris).Value.ToString
                If DK <> "D" And DK <> "K" Then
                    ProsesPostingGagal()
                    Return
                End If
                If DataGridView.Item(10, Baris).Value.ToString = Nothing Then
                    JumlahDebet = 0
                Else
                    JumlahDebet = AmbilAngka(DataGridView.Item(10, Baris).Value)
                End If
                If DataGridView.Item(11, Baris).Value.ToString = Nothing Then
                    JumlahKredit = 0
                Else
                    JumlahKredit = AmbilAngka(DataGridView.Item(11, Baris).Value)
                End If
                UraianTransaksi = DataGridView.Item(12, Baris).Value.ToString
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
            pgb_Progress.Value = Baris
            lbl_ProgressReport.Text = "Progress :  Baris " & Baris + 1 & " | Nomor " & AwalanNomorJV & jur_NomorJV
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

        Me.Height = 333
        JumlahJV_Terposting = JumlahJV_SumberData - JumlahJV_GagalPosting
        JumlahBaris_Terposting = JumlahBaris_SumberData - JumlahBaris_GagalPosting
        txt_LaporanHasilPostingJurnal.Location = New Drawing.Point(35, 87)
        txt_LaporanHasilPostingJurnal.Visible = True
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "_____________________________________________________"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "RESUME :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Sumber Data :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & JumlahBaris_SumberData & " baris; " & JumlahJV_SumberData & " JV"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Data Terposting :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & JumlahJV_Terposting & " JV  ( " & JumlahBaris_Terposting & " baris )"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Data Gagal Terposting :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & JumlahJV_GagalPosting & " JV  ( " & JumlahBaris_GagalPosting & " baris )"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Data Direvisi :"
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & JumlahBaris_Direvisi & " baris."
        txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris

    End Sub

    Sub DataJurnalTidakValid()
        If jur_StatusPenyimpananJurnal_Lengkap = True Then
            BarisAwalBahanJV = Baris - JumlahBarisBahanJV + 2
            BarisAkhirBahanJV = BarisAwalBahanJV - 1 + JumlahBarisBahanJV
            MsgBox("Data tidak valid pada Jurnal Nomor " & AwalanNomorJV & jur_NomorJV & "." & _
                   Enter1Baris & "( Baris " & BarisAwalBahanJV & " sampai " & BarisAkhirBahanJV & " pada tabel Excel )." & _
                   Enter2Baris & "Silakan diperbaiki.")
            frm_VerifikasiDataJurnal.NomorJV = AwalanNomorJV & jur_NomorJV
            frm_VerifikasiDataJurnal.BarisAwalBahanJV = BarisAwalBahanJV
            frm_VerifikasiDataJurnal.BarisAkhirBahanJV = BarisAkhirBahanJV
            frm_VerifikasiDataJurnal.JumlahBarisBahanJV = JumlahBarisBahanJV
            frm_VerifikasiDataJurnal.ShowDialog()
            If jur_StatusPenyimpananJurnal_Lengkap = False Then
                JumlahJV_GagalPosting = JumlahJV_GagalPosting + 1
                JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + JumlahBarisBahanJV
                txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Nomor " & AwalanNomorJV & jur_NomorJV & " dibuang..!"
                txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & "( Periksa tabel Excel pada baris " & BarisAwalBahanJV & " sampai baris akhir jurnal terkait )."
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
            txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Baris " & Baris + 2 & " pada tabel Excel direvisi."
            txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & "- [" & NamaLawanTransaksi & "] menjadi [" & jur_NamaLawanTransaksi & "]"
            AdaRevisi = "YA"
        End If
        jur_UraianTransaksi = Replace(UraianTransaksi, "'", "")
        If jur_UraianTransaksi <> UraianTransaksi Then
            If AdaRevisi = "TIDAK" Then
                txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Baris " & Baris + 2 & " pada tabel Excel direvisi."
            End If
            txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & "- [" & UraianTransaksi & "] menjadi [" & jur_UraianTransaksi & "]"
            AdaRevisi = "YA"
        End If
        If AdaRevisi = "YA" Then
            Me.Height = 388
            txt_LaporanHasilPostingJurnal.Location = New Drawing.Point(35, 186)
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
            MsgBox(PesanError & Enter2Baris & _
                   "Silakan koreksi baris " & Baris + 2 & " pada tabel Excel.")
        End If
        If jur_StatusPenyimpananJurnal_PerBaris = False Then
            JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + 1
            jur_StatusPenyimpananJurnal_Lengkap = False
        End If
        If jur_StatusPenyimpananJurnal_Lengkap = False And jur_NomorJV <> NomorJV_GagalPosting Then
            HasilPosting = Hasil_BERMASALAH
            Me.Height = 388
            txt_LaporanHasilPostingJurnal.Location = New Drawing.Point(35, 186)
            BarisAwalBahanJV = Baris - JumlahBarisBahanJV + 2
            BarisAkhirBahanJV = BarisAwalBahanJV - 1 + JumlahBarisBahanJV
            txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter2Baris & "Nomor " & AwalanNomorJV & jur_NomorJV & " tidak tersimpan..!"
            txt_LaporanHasilPostingJurnal.Text = txt_LaporanHasilPostingJurnal.Text & Enter1Baris & "( Periksa tabel Excel pada baris " & BarisAwalBahanJV & " sampai baris akhir jurnal terkait )."
            JumlahJV_GagalPosting = JumlahJV_GagalPosting + 1
            JumlahBaris_GagalPosting = JumlahBaris_GagalPosting + JumlahBarisBahanJV
            NomorJV_GagalPosting = jur_NomorJV
        End If
    End Sub

    Private Sub btn_Proses_Click(sender As Object, e As EventArgs) Handles btn_Proses.Click
        SistemPenomoranOtomatis_NomorJV()
        Dim NomorJV_AwalPosting = jur_NomorJV
        NomorJV_GagalPosting = 0
        MsgBox("Penomoran jurnal pada event ini dimulai dari Nomor " & AwalanNomorJV & NomorJV_AwalPosting & "")
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
                TanyaBatalPostingJurnal()
            End If
        End If
        If StatusPosting = Status_BATAL Or StatusPosting = Status_SELESAI Then
            TutupForm()
        End If
    End Sub

    Public Sub TanyaBatalPostingJurnal()
        Dim PilihBatalPosting = MessageBox.Show("Seluruh proses posting pada event ini akan dibatalkan." & Enter2Baris & "Yakin akan membatalkan proses posting..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihBatalPosting = vbYes Then
            StatusPosting = Status_BATAL
            HapusSemuaDataPostinganJurnalEventIni()
        ElseIf PilihBatalPosting = vbNo Then
            StatusPosting = Status_PROSES
        End If
    End Sub

    Sub ProsesPostingGagal()
        HapusSemuaDataPostinganJurnalEventIni()
        MsgBox("Proses GAGAL, karena ada kesalahan format dari sumber data." & _
               Enter2Baris & "Silakan perbaiki dan sesuaikan sumber data, kemudian ulangi lagi.")
        MsgBox("Info :" & _
               Enter1Baris & "Kesalahan ada pada baris " & Baris + 2 & " tabel Excel sumber data." & _
               Enter2Baris & "Tips :" & _
               Enter1Baris & "- Susun sumber data sesuai kerangka yang disediakan" & _
               Enter1Baris & "- Kolom Tanggal jangan diisi selain data tanggal" & _
               Enter1Baris & "- Tahun Jurnal harus seuai dengan 'Tahun Buku Aktif'" & _
               Enter1Baris & "- Kolom D/K hanya boleh diisi hufuf D atau huruf K" & _
               Enter1Baris & "- Kolom angka jangan disusupi karakter" & _
               Enter1Baris & "- Jangan menggunakan tanda petik satu ( ' )" & _
               Enter1Baris & "- Setelah baris akhir sumber data, tabel harus clear" & _
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

    Private Sub btn_TetapkanHasilPosting_Click(sender As Object, e As EventArgs) Handles btn_TetapkanHasilPosting.Click
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

    Private Sub btn_BuangHasilPosting_Click(sender As Object, e As EventArgs) Handles btn_BuangHasilPosting.Click
        TanyaBatalPostingJurnal()
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