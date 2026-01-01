Imports bcomm
Imports System.ComponentModel
Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

Public Class X_frm_Laporan_TrialBalance_X

    Public JalurMasuk
    Dim QueryTampilan

    Dim TrialBalanceDone As Boolean
    Dim LoopingTrialBalance As Boolean
    Dim JedaPerBarisCOA = 3 'milidetik
    Dim JedaPerKelompokCOA = 33 'milidetik

    Dim KesesuaianData_TrialBalance As Boolean

    Private Sub frm_TrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If JalurMasuk = Halaman_MENUUTAMA Then Me.Visible = True
        If JalurMasuk <> Halaman_MENUUTAMA Then Me.Visible = False

        lbl_Judul.Text = "Trial Balance - Tahun " & TahunBukuAktif
        Control.CheckForIllegalCrossThreadCalls = True

        'If StatusTrialBalance = True Then TampilkanData()

    End Sub

    Sub RefreshTampilanData()

        Proses = True
        TrialBalanceDone = False
        LoopingTrialBalance = True
        TrialBalance_Mentahkan() 'Mentahkan dulu data, karena khawatir proses berhenti di tengah jalan.
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabaseGeneral = False Then Return
        Try
            cmd = New OdbcCommand(" SELECT COA FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Proses = True
        Catch ex As Exception
            Proses = False
            pesan_AdaMasalahDenganKoneksiDatabase()
            Return
        End Try
        Dim JumlahBarisCOA = 0
        Do While dr.Read
            JumlahBarisCOA += 1
        Loop
        AksesDatabase_General(Tutup)
        ProgressMaximum = JumlahBarisCOA
        frm_ProgressLoadingData.lbl_Baris_01.Text = "Harap Tunggu..."
        frm_ProgressLoadingData.lbl_Baris_02.Text = "Sistem sedang memproses data."
        frm_ProgressLoadingData.lbl_ProgressReport.Text = "Jangan memutus proses ini..!"
        bgw_ProsesTrialBalance.RunWorkerAsync()
        BeginInvoke(Sub() PosisiBug1())
        frm_ProgressLoadingData.ShowDialog()
        If Proses = False Then TrialBalanceDone = False
        If TrialBalanceDone = True Then
            TrialBalance_Matangkan()
            MsgBox("Trial Balance BERHASIL.")
            Select Case JalurMasuk
                Case Halaman_MENUUTAMA
                Case Halaman_TUTUPBUKU
                    If usc_TutupBuku.StatusAktif Then usc_TutupBuku.TampilkanData()
                    MsgBox("Silakan periksa data dengan teliti sebelum melakukan 'Tutup Buku'")
                    Me.Close()
                Case Halaman_LAPORANLABARUGI
                    X_frm_Laporan_LabaRugi_X.TampilkanData()
                    Me.Close()
                Case Halaman_LAPORANNERACA
                    X_frm_Laporan_Neraca_X.TampilkanData()
                    Me.Close()
                Case Else
                    Me.Close()
            End Select
        Else
            LoopingTrialBalance = False 'Keluar dari looping dan BackgroundWorker
            DataTabelUtama.Rows.Clear()
            MsgBox("Trial Balance gagal." & Enter2Baris & teks_SilakanCobaLagi_Database)
            DataTabelUtama.Rows.Clear() 'Kenapa coding ini harus dua kali..? Karena kita bekerja di BackgroundWorker. Jadi, yang terakhir ini untuk menyapu sisa-sisa baris yang masih ada. Intinya : BARIS INI JANGAN DIHAPUS...!!!
            If JalurMasuk <> Halaman_MENUUTAMA Then Me.Close()
        End If
        Proses = False

    End Sub

    Sub TampilkanData()

        ProgressValue = 0

        'Style Tabel :
        BeginInvoke(Sub() DataTabelUtama.Rows.Clear())
        StyleTabelUtama(DataTabelUtama)

        Dim QueryTampilanUmum = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' "


        'Data Tabel :
        BeginInvoke(Sub() DataTabelUtama.Rows.Add(
            "", "", "",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Januari",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Februari",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Maret",
            "Mutasi Debet", "Mutasi Kredit", "Saldo April",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Mei",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Juni",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Juli",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Agustus",
            "Mutasi Debet", "Mutasi Kredit", "Saldo September",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Oktober",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Nopember",
            "Mutasi Debet", "Mutasi Kredit", "Saldo Desember"))


        '-----------------------------------------------------------
        'AKTIVA :
        '-----------------------------------------------------------
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("AKTIVA"))

        'Aktiva Lancar :
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("Aktiva Lancar"))
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 10000 AND 11999 " '( COA Kelompok AKTIVA LANCAR )
        If Proses = True Then
            DataPerKategoriCOA()
        End If
        Threading.Thread.Sleep(JedaPerKelompokCOA)

        'Aktiva Tetap :
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("Aktiva Tetap"))
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 12000 AND 19999 " '( COA Kelompok AKTIVA TETAP )
        If Proses = True Then
            DataPerKategoriCOA()
        End If
        Threading.Thread.Sleep(JedaPerKelompokCOA)


        '-----------------------------------------------------------
        'PASSIVA
        '-----------------------------------------------------------
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("PASSIVA"))

        'Hutang :
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("Hutang"))

        'Hutang Jangka Pendek :
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("Hutang Jangka Pendek"))
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 21000 AND 21999 "  '( COA Kelompok HUTANG JANGKA PENDEK )
        If Proses = True Then
            DataPerKategoriCOA()
        End If
        Threading.Thread.Sleep(JedaPerKelompokCOA)

        'Hutang Jangka Panjang :
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("Hutang Jangka Panjang"))
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 22000 AND 22999 " ' ( COA Kelompok HUTANG JANGKA PANJANG )
        If Proses = True Then
            DataPerKategoriCOA()
        End If
        Threading.Thread.Sleep(JedaPerKelompokCOA)

        'Modal
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("Modal"))
        QueryTampilan = QueryTampilanUmum & " AND COA LIKE '3%' " '( COA Kelompok MODAL )
        If Proses = True Then
            DataPerKategoriCOA()
        End If
        Threading.Thread.Sleep(JedaPerKelompokCOA)

        'Laba Rugi
        BeginInvoke(Sub() DataTabelUtama.Rows.Add("Laba Rugi"))
        QueryTampilan = QueryTampilanUmum & " AND COA >= 40000 " '( COA Kelompok PENDAPATAN DAN BIAYA )
        If Proses = True Then
            DataPerKategoriCOA()
        End If
        Threading.Thread.Sleep(JedaPerKelompokCOA)

        DataTabelUtama.ClearSelection()

        If usc_TutupBuku.StatusAktif Then usc_TutupBuku.TampilkanData() 'Ini jangan dihapus. Ini dibutuhkan agar data pada Form Tutup Buku ter-refresh. Khawatir form ini sedang dibuka oleh user pada saat Trial Balance.

    End Sub

    Sub DataPerKategoriCOA_MySQL_BelumBerhasil()

        'Data Tabel
        Dim TahunTrialBalance = TahunBukuAktif
        Dim COA
        Dim NamaAkun
        Dim DebetKreditCOA
        Dim SaldoAwal As Int64
        BukaDatabaseGeneral_MySQL()
        If StatusKoneksiDatabaseGeneral_MySQL = False Then
            Proses = False
            Return
        End If
        AksesDatabase_Transaksi(Buka)
        Try
            cmdMySQL = New MySqlCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral_MySQL)
            drMySQL = cmdMySQL.ExecuteReader
            Proses = True
        Catch ex As Exception
            Proses = False
            Return
        End Try
        Do While drMySQL.Read
            Try
                If LoopingTrialBalance = False Then Exit Do
                ProgressValue += 1
                bgw_ProsesTrialBalance.ReportProgress(ProgressValue)
                COA = drMySQL.Item("COA")
                NamaAkun = drMySQL.Item("Nama_Akun")
                DebetKreditCOA = drMySQL.Item("D_K")
                SaldoAwal = drMySQL.Item("Saldo_Awal")
                'Mutasi Januari ---------------------------------------------------------------------------------
                Dim QueryJanuari = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/01/01' AND '" & TahunTrialBalance & "/01/31' "
                Dim cmdJanuari = New OdbcCommand(QueryJanuari, KoneksiDatabaseTransaksi)
                Dim drJanuari = cmdJanuari.ExecuteReader
                Dim MutasiDebetJanuari As Int64 = 0
                Dim MutasiKreditJanuari As Int64 = 0
                Do While drJanuari.Read
                    MutasiDebetJanuari += drJanuari.Item("Jumlah_Debet")
                    MutasiKreditJanuari += drJanuari.Item("Jumlah_Kredit")
                Loop
                Dim SaldoJanuari As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoJanuari = MutasiDebetJanuari - MutasiKreditJanuari
                    If DebetKreditCOA = "KREDIT" Then SaldoJanuari = MutasiKreditJanuari - MutasiDebetJanuari
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoJanuari = SaldoAwal + MutasiDebetJanuari - MutasiKreditJanuari
                    If DebetKreditCOA = "KREDIT" Then SaldoJanuari = SaldoAwal + MutasiKreditJanuari - MutasiDebetJanuari
                End If
                'Mutasi Februari ---------------------------------------------------------------------------------
                Dim DesimalTahunTrial As Decimal = TahunTrialBalance / 4
                Dim DesimalTahunTrialHapusKoma = Replace(DesimalTahunTrial.ToString, ",", ".")
                Dim DesimalTahunTrialHapusTitik = Replace(DesimalTahunTrialHapusKoma.ToString, ".", "")
                Dim TanggalAkhirBulanFebruariTrial
                If Len(DesimalTahunTrial.ToString) = Len(DesimalTahunTrialHapusTitik.ToString) Then
                    TanggalAkhirBulanFebruariTrial = "29"
                Else
                    TanggalAkhirBulanFebruariTrial = "28"
                End If
                Dim QueryFebruari = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/02/01' AND '" & TahunTrialBalance & "/02/" & TanggalAkhirBulanFebruariTrial & "' "
                Dim cmdFebruari = New OdbcCommand(QueryFebruari, KoneksiDatabaseTransaksi)
                Dim drFebruari = cmdFebruari.ExecuteReader
                Dim MutasiDebetFebruari As Int64 = 0
                Dim MutasiKreditFebruari As Int64 = 0
                Do While drFebruari.Read
                    MutasiDebetFebruari = MutasiDebetFebruari + drFebruari.Item("Jumlah_Debet")
                    MutasiKreditFebruari = MutasiKreditFebruari + drFebruari.Item("Jumlah_Kredit")
                Loop
                Dim SaldoFebruari As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoFebruari = MutasiDebetFebruari - MutasiKreditFebruari
                    If DebetKreditCOA = "KREDIT" Then SaldoFebruari = MutasiKreditFebruari - MutasiDebetFebruari
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoFebruari = SaldoJanuari + MutasiDebetFebruari - MutasiKreditFebruari
                    If DebetKreditCOA = "KREDIT" Then SaldoFebruari = SaldoJanuari + MutasiKreditFebruari - MutasiDebetFebruari
                End If
                'Mutasi Maret ---------------------------------------------------------------------------------
                Dim QueryMaret = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/03/01' AND '" & TahunTrialBalance & "/03/31' "
                Dim cmdMaret = New OdbcCommand(QueryMaret, KoneksiDatabaseTransaksi)
                Dim drMaret = cmdMaret.ExecuteReader
                Dim MutasiDebetMaret As Int64 = 0
                Dim MutasiKreditMaret As Int64 = 0
                Do While drMaret.Read
                    MutasiDebetMaret = MutasiDebetMaret + drMaret.Item("Jumlah_Debet")
                    MutasiKreditMaret = MutasiKreditMaret + drMaret.Item("Jumlah_Kredit")
                Loop
                Dim SaldoMaret As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoMaret = MutasiDebetMaret - MutasiKreditMaret
                    If DebetKreditCOA = "KREDIT" Then SaldoMaret = MutasiKreditMaret - MutasiDebetMaret
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoMaret = SaldoFebruari + MutasiDebetMaret - MutasiKreditMaret
                    If DebetKreditCOA = "KREDIT" Then SaldoMaret = SaldoFebruari + MutasiKreditMaret - MutasiDebetMaret
                End If
                'Mutasi April ---------------------------------------------------------------------------------
                Dim QueryApril = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/04/01' AND '" & TahunTrialBalance & "/04/30' "
                Dim cmdApril = New OdbcCommand(QueryApril, KoneksiDatabaseTransaksi)
                Dim drApril = cmdApril.ExecuteReader
                Dim MutasiDebetApril As Int64 = 0
                Dim MutasiKreditApril As Int64 = 0
                Do While drApril.Read
                    MutasiDebetApril = MutasiDebetApril + drApril.Item("Jumlah_Debet")
                    MutasiKreditApril = MutasiKreditApril + drApril.Item("Jumlah_Kredit")
                Loop
                Dim SaldoApril As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoApril = MutasiDebetApril - MutasiKreditApril
                    If DebetKreditCOA = "KREDIT" Then SaldoApril = MutasiKreditApril - MutasiDebetApril
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoApril = SaldoMaret + MutasiDebetApril - MutasiKreditApril
                    If DebetKreditCOA = "KREDIT" Then SaldoApril = SaldoMaret + MutasiKreditApril - MutasiDebetApril
                End If
                'Mutasi Mei ---------------------------------------------------------------------------------
                Dim QueryMei = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/05/01' AND '" & TahunTrialBalance & "/05/31' "
                Dim cmdMei = New OdbcCommand(QueryMei, KoneksiDatabaseTransaksi)
                Dim drMei = cmdMei.ExecuteReader
                Dim MutasiDebetMei As Int64 = 0
                Dim MutasiKreditMei As Int64 = 0
                Do While drMei.Read
                    MutasiDebetMei = MutasiDebetMei + drMei.Item("Jumlah_Debet")
                    MutasiKreditMei = MutasiKreditMei + drMei.Item("Jumlah_Kredit")
                Loop
                Dim SaldoMei As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoMei = MutasiDebetMei - MutasiKreditMei
                    If DebetKreditCOA = "KREDIT" Then SaldoMei = MutasiKreditMei - MutasiDebetMei
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoMei = SaldoApril + MutasiDebetMei - MutasiKreditMei
                    If DebetKreditCOA = "KREDIT" Then SaldoMei = SaldoApril + MutasiKreditMei - MutasiDebetMei
                End If
                'Mutasi Juni ---------------------------------------------------------------------------------
                Dim QueryJuni = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/06/01' AND '" & TahunTrialBalance & "/06/30' "
                Dim cmdJuni = New OdbcCommand(QueryJuni, KoneksiDatabaseTransaksi)
                Dim drJuni = cmdJuni.ExecuteReader
                Dim MutasiDebetJuni As Int64 = 0
                Dim MutasiKreditJuni As Int64 = 0
                Do While drJuni.Read
                    MutasiDebetJuni = MutasiDebetJuni + drJuni.Item("Jumlah_Debet")
                    MutasiKreditJuni = MutasiKreditJuni + drJuni.Item("Jumlah_Kredit")
                Loop
                Dim SaldoJuni As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoJuni = MutasiDebetJuni - MutasiKreditJuni
                    If DebetKreditCOA = "KREDIT" Then SaldoJuni = MutasiKreditJuni - MutasiDebetJuni
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoJuni = SaldoMei + MutasiDebetJuni - MutasiKreditJuni
                    If DebetKreditCOA = "KREDIT" Then SaldoJuni = SaldoMei + MutasiKreditJuni - MutasiDebetJuni
                End If
                'Mutasi Juli ---------------------------------------------------------------------------------
                Dim QueryJuli = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/07/01' AND '" & TahunTrialBalance & "/07/31' "
                Dim cmdJuli = New OdbcCommand(QueryJuli, KoneksiDatabaseTransaksi)
                Dim drJuli = cmdJuli.ExecuteReader
                Dim MutasiDebetJuli As Int64 = 0
                Dim MutasiKreditJuli As Int64 = 0
                Do While drJuli.Read
                    MutasiDebetJuli = MutasiDebetJuli + drJuli.Item("Jumlah_Debet")
                    MutasiKreditJuli = MutasiKreditJuli + drJuli.Item("Jumlah_Kredit")
                Loop
                Dim SaldoJuli As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoJuli = MutasiDebetJuli - MutasiKreditJuli
                    If DebetKreditCOA = "KREDIT" Then SaldoJuli = MutasiKreditJuli - MutasiDebetJuli
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoJuli = SaldoJuni + MutasiDebetJuli - MutasiKreditJuli
                    If DebetKreditCOA = "KREDIT" Then SaldoJuli = SaldoJuni + MutasiKreditJuli - MutasiDebetJuli
                End If
                'Mutasi Agustus ---------------------------------------------------------------------------------
                Dim QueryAgustus = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/08/01' AND '" & TahunTrialBalance & "/08/31' "
                Dim cmdAgustus = New OdbcCommand(QueryAgustus, KoneksiDatabaseTransaksi)
                Dim drAgustus = cmdAgustus.ExecuteReader
                Dim MutasiDebetAgustus As Int64 = 0
                Dim MutasiKreditAgustus As Int64 = 0
                Do While drAgustus.Read
                    MutasiDebetAgustus = MutasiDebetAgustus + drAgustus.Item("Jumlah_Debet")
                    MutasiKreditAgustus = MutasiKreditAgustus + drAgustus.Item("Jumlah_Kredit")
                Loop
                Dim SaldoAgustus As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoAgustus = MutasiDebetAgustus - MutasiKreditAgustus
                    If DebetKreditCOA = "KREDIT" Then SaldoAgustus = MutasiKreditAgustus - MutasiDebetAgustus
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoAgustus = SaldoJuli + MutasiDebetAgustus - MutasiKreditAgustus
                    If DebetKreditCOA = "KREDIT" Then SaldoAgustus = SaldoJuli + MutasiKreditAgustus - MutasiDebetAgustus
                End If
                'Mutasi September ---------------------------------------------------------------------------------
                Dim QuerySeptember = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/09/01' AND '" & TahunTrialBalance & "/09/30' "
                Dim cmdSeptember = New OdbcCommand(QuerySeptember, KoneksiDatabaseTransaksi)
                Dim drSeptember = cmdSeptember.ExecuteReader
                Dim MutasiDebetSeptember As Int64 = 0
                Dim MutasiKreditSeptember As Int64 = 0
                Do While drSeptember.Read
                    MutasiDebetSeptember = MutasiDebetSeptember + drSeptember.Item("Jumlah_Debet")
                    MutasiKreditSeptember = MutasiKreditSeptember + drSeptember.Item("Jumlah_Kredit")
                Loop
                Dim SaldoSeptember As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoSeptember = MutasiDebetSeptember - MutasiKreditSeptember
                    If DebetKreditCOA = "KREDIT" Then SaldoSeptember = MutasiKreditSeptember - MutasiDebetSeptember
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoSeptember = SaldoAgustus + MutasiDebetSeptember - MutasiKreditSeptember
                    If DebetKreditCOA = "KREDIT" Then SaldoSeptember = SaldoAgustus + MutasiKreditSeptember - MutasiDebetSeptember
                End If
                'Mutasi Oktober ---------------------------------------------------------------------------------
                Dim QueryOktober = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/10/01' AND '" & TahunTrialBalance & "/10/31' "
                Dim cmdOktober = New OdbcCommand(QueryOktober, KoneksiDatabaseTransaksi)
                Dim drOktober = cmdOktober.ExecuteReader
                Dim MutasiDebetOktober As Int64 = 0
                Dim MutasiKreditOktober As Int64 = 0
                Do While drOktober.Read
                    MutasiDebetOktober = MutasiDebetOktober + drOktober.Item("Jumlah_Debet")
                    MutasiKreditOktober = MutasiKreditOktober + drOktober.Item("Jumlah_Kredit")
                Loop
                Dim SaldoOktober As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoOktober = MutasiDebetOktober - MutasiKreditOktober
                    If DebetKreditCOA = "KREDIT" Then SaldoOktober = MutasiKreditOktober - MutasiDebetOktober
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoOktober = SaldoSeptember + MutasiDebetOktober - MutasiKreditOktober
                    If DebetKreditCOA = "KREDIT" Then SaldoOktober = SaldoSeptember + MutasiKreditOktober - MutasiDebetOktober
                End If
                'Mutasi Nopember ---------------------------------------------------------------------------------
                Dim QueryNopember = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/11/01' AND '" & TahunTrialBalance & "/11/30' "
                Dim cmdNopember = New OdbcCommand(QueryNopember, KoneksiDatabaseTransaksi)
                Dim drNopember = cmdNopember.ExecuteReader
                Dim MutasiDebetNopember As Int64 = 0
                Dim MutasiKreditNopember As Int64 = 0
                Do While drNopember.Read
                    MutasiDebetNopember = MutasiDebetNopember + drNopember.Item("Jumlah_Debet")
                    MutasiKreditNopember = MutasiKreditNopember + drNopember.Item("Jumlah_Kredit")
                Loop
                Dim SaldoNopember As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoNopember = MutasiDebetNopember - MutasiKreditNopember
                    If DebetKreditCOA = "KREDIT" Then SaldoNopember = MutasiKreditNopember - MutasiDebetNopember
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoNopember = SaldoOktober + MutasiDebetNopember - MutasiKreditNopember
                    If DebetKreditCOA = "KREDIT" Then SaldoNopember = SaldoOktober + MutasiKreditNopember - MutasiDebetNopember
                End If
                'Mutasi Desember ---------------------------------------------------------------------------------
                Dim QueryDesember = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/12/01' AND '" & TahunTrialBalance & "/12/31' "
                Dim cmdDesember = New OdbcCommand(QueryDesember, KoneksiDatabaseTransaksi)
                Dim drDesember = cmdDesember.ExecuteReader
                Dim MutasiDebetDesember As Int64 = 0
                Dim MutasiKreditDesember As Int64 = 0
                Do While drDesember.Read
                    MutasiDebetDesember = MutasiDebetDesember + drDesember.Item("Jumlah_Debet")
                    MutasiKreditDesember = MutasiKreditDesember + drDesember.Item("Jumlah_Kredit")
                Loop
                Dim SaldoDesember As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoDesember = MutasiDebetDesember - MutasiKreditDesember
                    If DebetKreditCOA = "KREDIT" Then SaldoDesember = MutasiKreditDesember - MutasiDebetDesember
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoDesember = SaldoNopember + MutasiDebetDesember - MutasiKreditDesember
                    If DebetKreditCOA = "KREDIT" Then SaldoDesember = SaldoNopember + MutasiKreditDesember - MutasiDebetDesember
                End If
                BeginInvoke(Sub() DataTabelUtama.Rows.Add(
                    NamaAkun, COA, SaldoAwal,
                    MutasiDebetJanuari, MutasiKreditJanuari, SaldoJanuari,
                    MutasiDebetFebruari, MutasiKreditFebruari, SaldoFebruari,
                    MutasiDebetMaret, MutasiKreditMaret, SaldoMaret,
                    MutasiDebetApril, MutasiKreditApril, SaldoApril,
                    MutasiDebetMei, MutasiKreditMei, SaldoMei,
                    MutasiDebetJuni, MutasiKreditJuni, SaldoJuni,
                    MutasiDebetJuli, MutasiKreditJuli, SaldoJuli,
                    MutasiDebetAgustus, MutasiKreditAgustus, SaldoAgustus,
                    MutasiDebetSeptember, MutasiKreditSeptember, SaldoSeptember,
                    MutasiDebetOktober, MutasiKreditOktober, SaldoOktober,
                    MutasiDebetNopember, MutasiKreditNopember, SaldoNopember,
                    MutasiDebetDesember, MutasiKreditDesember, SaldoDesember))
                Dim QuerySimpanSaldo = " UPDATE tbl_COA SET " &
                                       " Debet_Januari      = '" & MutasiDebetJanuari & "', " &
                                       " Kredit_Januari     = '" & MutasiKreditJanuari & "', " &
                                       " Saldo_Januari      = '" & SaldoJanuari & "', " &
                                       " Debet_Februari     = '" & MutasiDebetFebruari & "', " &
                                       " Kredit_Februari    = '" & MutasiKreditFebruari & "', " &
                                       " Saldo_Februari     = '" & SaldoFebruari & "', " &
                                       " Debet_Maret        = '" & MutasiDebetMaret & "', " &
                                       " Kredit_Maret       = '" & MutasiKreditMaret & "', " &
                                       " Saldo_Maret        = '" & SaldoMaret & "', " &
                                       " Debet_April        = '" & MutasiDebetApril & "', " &
                                       " Kredit_April       = '" & MutasiKreditApril & "', " &
                                       " Saldo_April        = '" & SaldoApril & "', " &
                                       " Debet_Mei          = '" & MutasiDebetMei & "', " &
                                       " Kredit_Mei         = '" & MutasiKreditMei & "', " &
                                       " Saldo_Mei          = '" & SaldoMei & "', " &
                                       " Debet_Juni         = '" & MutasiDebetJuni & "', " &
                                       " Kredit_Juni        = '" & MutasiKreditJuni & "', " &
                                       " Saldo_Juni         = '" & SaldoJuni & "', " &
                                       " Debet_Juli         = '" & MutasiDebetJuli & "', " &
                                       " Kredit_Juli        = '" & MutasiKreditJuli & "', " &
                                       " Saldo_Juli         = '" & SaldoJuli & "', " &
                                       " Debet_Agustus      = '" & MutasiDebetAgustus & "', " &
                                       " Kredit_Agustus     = '" & MutasiKreditAgustus & "', " &
                                       " Saldo_Agustus      = '" & SaldoAgustus & "', " &
                                       " Debet_September    = '" & MutasiDebetSeptember & "', " &
                                       " Kredit_September   = '" & MutasiKreditSeptember & "', " &
                                       " Saldo_September    = '" & SaldoSeptember & "', " &
                                       " Debet_Oktober      = '" & MutasiDebetOktober & "', " &
                                       " Kredit_Oktober     = '" & MutasiKreditOktober & "', " &
                                       " Saldo_Oktober      = '" & SaldoOktober & "', " &
                                       " Debet_Nopember     = '" & MutasiDebetNopember & "', " &
                                       " Kredit_Nopember    = '" & MutasiKreditNopember & "', " &
                                       " Saldo_Nopember     = '" & SaldoNopember & "', " &
                                       " Debet_Desember     = '" & MutasiDebetDesember & "', " &
                                       " Kredit_Desember    = '" & MutasiKreditDesember & "', " &
                                       " Saldo_Desember     = '" & SaldoDesember & "' " &
                                       " WHERE COA          = '" & COA & "' "
                Dim cmdSimpanSaldo = New MySqlCommand(QuerySimpanSaldo, KoneksiDatabaseGeneral_MySQL)
                cmdSimpanSaldo.ExecuteNonQuery()
                Threading.Thread.Sleep(JedaPerBarisCOA)
                Proses = True
            Catch ex As Exception
                Proses = False
                Exit Do
            End Try
        Loop
        AksesDatabase_Transaksi(Tutup)
        TutupDatabaseGeneral_MySQL()
    End Sub

    Sub DataPerKategoriCOA()

        'Data Tabel
        Dim TahunTrialBalance = TahunBukuAktif
        Dim COA
        Dim NamaAkun
        Dim DebetKreditCOA
        Dim SaldoAwal As Int64
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabaseGeneral = False Then
            Proses = False
            Return
        End If
        AksesDatabase_Transaksi(Buka)
        Try
            cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Proses = True
        Catch ex As Exception
            Proses = False
            Return
        End Try
        Do While dr.Read
            Try
                If LoopingTrialBalance = False Then Exit Do
                ProgressValue += 1
                bgw_ProsesTrialBalance.ReportProgress(ProgressValue)
                COA = dr.Item("COA")
                NamaAkun = dr.Item("Nama_Akun")
                DebetKreditCOA = dr.Item("D_K")
                SaldoAwal = dr.Item("Saldo_Awal")
                'Mutasi Januari ---------------------------------------------------------------------------------
                Dim QueryJanuari = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/01/01' AND '" & TahunTrialBalance & "/01/31' "
                Dim cmdJanuari = New OdbcCommand(QueryJanuari, KoneksiDatabaseTransaksi)
                Dim drJanuari = cmdJanuari.ExecuteReader
                Dim MutasiDebetJanuari As Int64 = 0
                Dim MutasiKreditJanuari As Int64 = 0
                Do While drJanuari.Read
                    MutasiDebetJanuari = MutasiDebetJanuari + drJanuari.Item("Jumlah_Debet")
                    MutasiKreditJanuari = MutasiKreditJanuari + drJanuari.Item("Jumlah_Kredit")
                Loop
                Dim SaldoJanuari As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoJanuari = MutasiDebetJanuari - MutasiKreditJanuari
                    If DebetKreditCOA = "KREDIT" Then SaldoJanuari = MutasiKreditJanuari - MutasiDebetJanuari
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoJanuari = SaldoAwal + MutasiDebetJanuari - MutasiKreditJanuari
                    If DebetKreditCOA = "KREDIT" Then SaldoJanuari = SaldoAwal + MutasiKreditJanuari - MutasiDebetJanuari
                End If
                'Mutasi Februari ---------------------------------------------------------------------------------
                Dim DesimalTahunTrial As Decimal = TahunTrialBalance / 4
                Dim DesimalTahunTrialHapusKoma = Replace(DesimalTahunTrial.ToString, ",", ".")
                Dim DesimalTahunTrialHapusTitik = Replace(DesimalTahunTrialHapusKoma.ToString, ".", "")
                Dim TanggalAkhirBulanFebruariTrial
                If Len(DesimalTahunTrial.ToString) = Len(DesimalTahunTrialHapusTitik.ToString) Then
                    TanggalAkhirBulanFebruariTrial = "29"
                Else
                    TanggalAkhirBulanFebruariTrial = "28"
                End If
                Dim QueryFebruari = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/02/01' AND '" & TahunTrialBalance & "/02/" & TanggalAkhirBulanFebruariTrial & "' "
                Dim cmdFebruari = New OdbcCommand(QueryFebruari, KoneksiDatabaseTransaksi)
                Dim drFebruari = cmdFebruari.ExecuteReader
                Dim MutasiDebetFebruari As Int64 = 0
                Dim MutasiKreditFebruari As Int64 = 0
                Do While drFebruari.Read
                    MutasiDebetFebruari = MutasiDebetFebruari + drFebruari.Item("Jumlah_Debet")
                    MutasiKreditFebruari = MutasiKreditFebruari + drFebruari.Item("Jumlah_Kredit")
                Loop
                Dim SaldoFebruari As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoFebruari = MutasiDebetFebruari - MutasiKreditFebruari
                    If DebetKreditCOA = "KREDIT" Then SaldoFebruari = MutasiKreditFebruari - MutasiDebetFebruari
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoFebruari = SaldoJanuari + MutasiDebetFebruari - MutasiKreditFebruari
                    If DebetKreditCOA = "KREDIT" Then SaldoFebruari = SaldoJanuari + MutasiKreditFebruari - MutasiDebetFebruari
                End If
                'Mutasi Maret ---------------------------------------------------------------------------------
                Dim QueryMaret = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/03/01' AND '" & TahunTrialBalance & "/03/31' "
                Dim cmdMaret = New OdbcCommand(QueryMaret, KoneksiDatabaseTransaksi)
                Dim drMaret = cmdMaret.ExecuteReader
                Dim MutasiDebetMaret As Int64 = 0
                Dim MutasiKreditMaret As Int64 = 0
                Do While drMaret.Read
                    MutasiDebetMaret = MutasiDebetMaret + drMaret.Item("Jumlah_Debet")
                    MutasiKreditMaret = MutasiKreditMaret + drMaret.Item("Jumlah_Kredit")
                Loop
                Dim SaldoMaret As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoMaret = MutasiDebetMaret - MutasiKreditMaret
                    If DebetKreditCOA = "KREDIT" Then SaldoMaret = MutasiKreditMaret - MutasiDebetMaret
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoMaret = SaldoFebruari + MutasiDebetMaret - MutasiKreditMaret
                    If DebetKreditCOA = "KREDIT" Then SaldoMaret = SaldoFebruari + MutasiKreditMaret - MutasiDebetMaret
                End If
                'Mutasi April ---------------------------------------------------------------------------------
                Dim QueryApril = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/04/01' AND '" & TahunTrialBalance & "/04/30' "
                Dim cmdApril = New OdbcCommand(QueryApril, KoneksiDatabaseTransaksi)
                Dim drApril = cmdApril.ExecuteReader
                Dim MutasiDebetApril As Int64 = 0
                Dim MutasiKreditApril As Int64 = 0
                Do While drApril.Read
                    MutasiDebetApril = MutasiDebetApril + drApril.Item("Jumlah_Debet")
                    MutasiKreditApril = MutasiKreditApril + drApril.Item("Jumlah_Kredit")
                Loop
                Dim SaldoApril As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoApril = MutasiDebetApril - MutasiKreditApril
                    If DebetKreditCOA = "KREDIT" Then SaldoApril = MutasiKreditApril - MutasiDebetApril
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoApril = SaldoMaret + MutasiDebetApril - MutasiKreditApril
                    If DebetKreditCOA = "KREDIT" Then SaldoApril = SaldoMaret + MutasiKreditApril - MutasiDebetApril
                End If
                'Mutasi Mei ---------------------------------------------------------------------------------
                Dim QueryMei = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/05/01' AND '" & TahunTrialBalance & "/05/31' "
                Dim cmdMei = New OdbcCommand(QueryMei, KoneksiDatabaseTransaksi)
                Dim drMei = cmdMei.ExecuteReader
                Dim MutasiDebetMei As Int64 = 0
                Dim MutasiKreditMei As Int64 = 0
                Do While drMei.Read
                    MutasiDebetMei = MutasiDebetMei + drMei.Item("Jumlah_Debet")
                    MutasiKreditMei = MutasiKreditMei + drMei.Item("Jumlah_Kredit")
                Loop
                Dim SaldoMei As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoMei = MutasiDebetMei - MutasiKreditMei
                    If DebetKreditCOA = "KREDIT" Then SaldoMei = MutasiKreditMei - MutasiDebetMei
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoMei = SaldoApril + MutasiDebetMei - MutasiKreditMei
                    If DebetKreditCOA = "KREDIT" Then SaldoMei = SaldoApril + MutasiKreditMei - MutasiDebetMei
                End If
                'Mutasi Juni ---------------------------------------------------------------------------------
                Dim QueryJuni = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/06/01' AND '" & TahunTrialBalance & "/06/30' "
                Dim cmdJuni = New OdbcCommand(QueryJuni, KoneksiDatabaseTransaksi)
                Dim drJuni = cmdJuni.ExecuteReader
                Dim MutasiDebetJuni As Int64 = 0
                Dim MutasiKreditJuni As Int64 = 0
                Do While drJuni.Read
                    MutasiDebetJuni = MutasiDebetJuni + drJuni.Item("Jumlah_Debet")
                    MutasiKreditJuni = MutasiKreditJuni + drJuni.Item("Jumlah_Kredit")
                Loop
                Dim SaldoJuni As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoJuni = MutasiDebetJuni - MutasiKreditJuni
                    If DebetKreditCOA = "KREDIT" Then SaldoJuni = MutasiKreditJuni - MutasiDebetJuni
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoJuni = SaldoMei + MutasiDebetJuni - MutasiKreditJuni
                    If DebetKreditCOA = "KREDIT" Then SaldoJuni = SaldoMei + MutasiKreditJuni - MutasiDebetJuni
                End If
                'Mutasi Juli ---------------------------------------------------------------------------------
                Dim QueryJuli = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/07/01' AND '" & TahunTrialBalance & "/07/31' "
                Dim cmdJuli = New OdbcCommand(QueryJuli, KoneksiDatabaseTransaksi)
                Dim drJuli = cmdJuli.ExecuteReader
                Dim MutasiDebetJuli As Int64 = 0
                Dim MutasiKreditJuli As Int64 = 0
                Do While drJuli.Read
                    MutasiDebetJuli = MutasiDebetJuli + drJuli.Item("Jumlah_Debet")
                    MutasiKreditJuli = MutasiKreditJuli + drJuli.Item("Jumlah_Kredit")
                Loop
                Dim SaldoJuli As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoJuli = MutasiDebetJuli - MutasiKreditJuli
                    If DebetKreditCOA = "KREDIT" Then SaldoJuli = MutasiKreditJuli - MutasiDebetJuli
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoJuli = SaldoJuni + MutasiDebetJuli - MutasiKreditJuli
                    If DebetKreditCOA = "KREDIT" Then SaldoJuli = SaldoJuni + MutasiKreditJuli - MutasiDebetJuli
                End If
                'Mutasi Agustus ---------------------------------------------------------------------------------
                Dim QueryAgustus = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/08/01' AND '" & TahunTrialBalance & "/08/31' "
                Dim cmdAgustus = New OdbcCommand(QueryAgustus, KoneksiDatabaseTransaksi)
                Dim drAgustus = cmdAgustus.ExecuteReader
                Dim MutasiDebetAgustus As Int64 = 0
                Dim MutasiKreditAgustus As Int64 = 0
                Do While drAgustus.Read
                    MutasiDebetAgustus = MutasiDebetAgustus + drAgustus.Item("Jumlah_Debet")
                    MutasiKreditAgustus = MutasiKreditAgustus + drAgustus.Item("Jumlah_Kredit")
                Loop
                Dim SaldoAgustus As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoAgustus = MutasiDebetAgustus - MutasiKreditAgustus
                    If DebetKreditCOA = "KREDIT" Then SaldoAgustus = MutasiKreditAgustus - MutasiDebetAgustus
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoAgustus = SaldoJuli + MutasiDebetAgustus - MutasiKreditAgustus
                    If DebetKreditCOA = "KREDIT" Then SaldoAgustus = SaldoJuli + MutasiKreditAgustus - MutasiDebetAgustus
                End If
                'Mutasi September ---------------------------------------------------------------------------------
                Dim QuerySeptember = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/09/01' AND '" & TahunTrialBalance & "/09/30' "
                Dim cmdSeptember = New OdbcCommand(QuerySeptember, KoneksiDatabaseTransaksi)
                Dim drSeptember = cmdSeptember.ExecuteReader
                Dim MutasiDebetSeptember As Int64 = 0
                Dim MutasiKreditSeptember As Int64 = 0
                Do While drSeptember.Read
                    MutasiDebetSeptember = MutasiDebetSeptember + drSeptember.Item("Jumlah_Debet")
                    MutasiKreditSeptember = MutasiKreditSeptember + drSeptember.Item("Jumlah_Kredit")
                Loop
                Dim SaldoSeptember As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoSeptember = MutasiDebetSeptember - MutasiKreditSeptember
                    If DebetKreditCOA = "KREDIT" Then SaldoSeptember = MutasiKreditSeptember - MutasiDebetSeptember
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoSeptember = SaldoAgustus + MutasiDebetSeptember - MutasiKreditSeptember
                    If DebetKreditCOA = "KREDIT" Then SaldoSeptember = SaldoAgustus + MutasiKreditSeptember - MutasiDebetSeptember
                End If
                'Mutasi Oktober ---------------------------------------------------------------------------------
                Dim QueryOktober = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/10/01' AND '" & TahunTrialBalance & "/10/31' "
                Dim cmdOktober = New OdbcCommand(QueryOktober, KoneksiDatabaseTransaksi)
                Dim drOktober = cmdOktober.ExecuteReader
                Dim MutasiDebetOktober As Int64 = 0
                Dim MutasiKreditOktober As Int64 = 0
                Do While drOktober.Read
                    MutasiDebetOktober = MutasiDebetOktober + drOktober.Item("Jumlah_Debet")
                    MutasiKreditOktober = MutasiKreditOktober + drOktober.Item("Jumlah_Kredit")
                Loop
                Dim SaldoOktober As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoOktober = MutasiDebetOktober - MutasiKreditOktober
                    If DebetKreditCOA = "KREDIT" Then SaldoOktober = MutasiKreditOktober - MutasiDebetOktober
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoOktober = SaldoSeptember + MutasiDebetOktober - MutasiKreditOktober
                    If DebetKreditCOA = "KREDIT" Then SaldoOktober = SaldoSeptember + MutasiKreditOktober - MutasiDebetOktober
                End If
                'Mutasi Nopember ---------------------------------------------------------------------------------
                Dim QueryNopember = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/11/01' AND '" & TahunTrialBalance & "/11/30' "
                Dim cmdNopember = New OdbcCommand(QueryNopember, KoneksiDatabaseTransaksi)
                Dim drNopember = cmdNopember.ExecuteReader
                Dim MutasiDebetNopember As Int64 = 0
                Dim MutasiKreditNopember As Int64 = 0
                Do While drNopember.Read
                    MutasiDebetNopember = MutasiDebetNopember + drNopember.Item("Jumlah_Debet")
                    MutasiKreditNopember = MutasiKreditNopember + drNopember.Item("Jumlah_Kredit")
                Loop
                Dim SaldoNopember As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoNopember = MutasiDebetNopember - MutasiKreditNopember
                    If DebetKreditCOA = "KREDIT" Then SaldoNopember = MutasiKreditNopember - MutasiDebetNopember
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoNopember = SaldoOktober + MutasiDebetNopember - MutasiKreditNopember
                    If DebetKreditCOA = "KREDIT" Then SaldoNopember = SaldoOktober + MutasiKreditNopember - MutasiDebetNopember
                End If
                'Mutasi Desember ---------------------------------------------------------------------------------
                Dim QueryDesember = " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE COA = '" & COA &
                    "' AND Status_Approve = 1 AND Tanggal_Transaksi BETWEEN '" & TahunTrialBalance & "/12/01' AND '" & TahunTrialBalance & "/12/31' "
                Dim cmdDesember = New OdbcCommand(QueryDesember, KoneksiDatabaseTransaksi)
                Dim drDesember = cmdDesember.ExecuteReader
                Dim MutasiDebetDesember As Int64 = 0
                Dim MutasiKreditDesember As Int64 = 0
                Do While drDesember.Read
                    MutasiDebetDesember = MutasiDebetDesember + drDesember.Item("Jumlah_Debet")
                    MutasiKreditDesember = MutasiKreditDesember + drDesember.Item("Jumlah_Kredit")
                Loop
                Dim SaldoDesember As Int64
                If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
                    If DebetKreditCOA = "DEBET" Then SaldoDesember = MutasiDebetDesember - MutasiKreditDesember
                    If DebetKreditCOA = "KREDIT" Then SaldoDesember = MutasiKreditDesember - MutasiDebetDesember
                Else
                    If DebetKreditCOA = "DEBET" Then SaldoDesember = SaldoNopember + MutasiDebetDesember - MutasiKreditDesember
                    If DebetKreditCOA = "KREDIT" Then SaldoDesember = SaldoNopember + MutasiKreditDesember - MutasiDebetDesember
                End If
                BeginInvoke(Sub() DataTabelUtama.Rows.Add(
                    NamaAkun, COA, SaldoAwal,
                    MutasiDebetJanuari, MutasiKreditJanuari, SaldoJanuari,
                    MutasiDebetFebruari, MutasiKreditFebruari, SaldoFebruari,
                    MutasiDebetMaret, MutasiKreditMaret, SaldoMaret,
                    MutasiDebetApril, MutasiKreditApril, SaldoApril,
                    MutasiDebetMei, MutasiKreditMei, SaldoMei,
                    MutasiDebetJuni, MutasiKreditJuni, SaldoJuni,
                    MutasiDebetJuli, MutasiKreditJuli, SaldoJuli,
                    MutasiDebetAgustus, MutasiKreditAgustus, SaldoAgustus,
                    MutasiDebetSeptember, MutasiKreditSeptember, SaldoSeptember,
                    MutasiDebetOktober, MutasiKreditOktober, SaldoOktober,
                    MutasiDebetNopember, MutasiKreditNopember, SaldoNopember,
                    MutasiDebetDesember, MutasiKreditDesember, SaldoDesember))
                Dim QuerySimpanSaldo = " UPDATE tbl_COA SET " &
                                       " Debet_Januari      = '" & MutasiDebetJanuari & "', " &
                                       " Kredit_Januari     = '" & MutasiKreditJanuari & "', " &
                                       " Saldo_Januari      = '" & SaldoJanuari & "', " &
                                       " Debet_Februari     = '" & MutasiDebetFebruari & "', " &
                                       " Kredit_Februari    = '" & MutasiKreditFebruari & "', " &
                                       " Saldo_Februari     = '" & SaldoFebruari & "', " &
                                       " Debet_Maret        = '" & MutasiDebetMaret & "', " &
                                       " Kredit_Maret       = '" & MutasiKreditMaret & "', " &
                                       " Saldo_Maret        = '" & SaldoMaret & "', " &
                                       " Debet_April        = '" & MutasiDebetApril & "', " &
                                       " Kredit_April       = '" & MutasiKreditApril & "', " &
                                       " Saldo_April        = '" & SaldoApril & "', " &
                                       " Debet_Mei          = '" & MutasiDebetMei & "', " &
                                       " Kredit_Mei         = '" & MutasiKreditMei & "', " &
                                       " Saldo_Mei          = '" & SaldoMei & "', " &
                                       " Debet_Juni         = '" & MutasiDebetJuni & "', " &
                                       " Kredit_Juni        = '" & MutasiKreditJuni & "', " &
                                       " Saldo_Juni         = '" & SaldoJuni & "', " &
                                       " Debet_Juli         = '" & MutasiDebetJuli & "', " &
                                       " Kredit_Juli        = '" & MutasiKreditJuli & "', " &
                                       " Saldo_Juli         = '" & SaldoJuli & "', " &
                                       " Debet_Agustus      = '" & MutasiDebetAgustus & "', " &
                                       " Kredit_Agustus     = '" & MutasiKreditAgustus & "', " &
                                       " Saldo_Agustus      = '" & SaldoAgustus & "', " &
                                       " Debet_September    = '" & MutasiDebetSeptember & "', " &
                                       " Kredit_September   = '" & MutasiKreditSeptember & "', " &
                                       " Saldo_September    = '" & SaldoSeptember & "', " &
                                       " Debet_Oktober      = '" & MutasiDebetOktober & "', " &
                                       " Kredit_Oktober     = '" & MutasiKreditOktober & "', " &
                                       " Saldo_Oktober      = '" & SaldoOktober & "', " &
                                       " Debet_Nopember     = '" & MutasiDebetNopember & "', " &
                                       " Kredit_Nopember    = '" & MutasiKreditNopember & "', " &
                                       " Saldo_Nopember     = '" & SaldoNopember & "', " &
                                       " Debet_Desember     = '" & MutasiDebetDesember & "', " &
                                       " Kredit_Desember    = '" & MutasiKreditDesember & "', " &
                                       " Saldo_Desember     = '" & SaldoDesember & "' " &
                                       " WHERE COA          = '" & COA & "' "
                Dim cmdSimpanSaldo = New OdbcCommand(QuerySimpanSaldo, KoneksiDatabaseGeneral)
                cmdSimpanSaldo.ExecuteNonQuery()
                Threading.Thread.Sleep(JedaPerBarisCOA)
                Proses = True
            Catch ex As Exception
                Proses = False
                Exit Do
            End Try
        Loop
        AksesDatabase_Transaksi(Tutup)
        AksesDatabase_General(Tutup)
    End Sub

    Public Sub btn_Proses_Click(sender As Object, e As EventArgs) Handles btn_Proses.Click

        If JalurMasuk <> Halaman_MENUUTAMA Then Me.Visible = False 'Ini Penting. Jangan dihapus..! Untuk jaga-jaga aja.

        'CEK KESESUAIAN DATA :
        KesesuaianData_TrialBalance = True
        TahunPajak = TahunBukuAktif 'Ini penting untuk ditentukan di awal. Untuk kepentingan Kesesuaian Saldo Awal Semua Jenis PPh.
        ProgressMaximum = 11 'Angka ini bisa berubah-ubah, tergantung berapa jumlah Data yang harus dicek (disesuaikan).
        frm_ProgressLoadingData.lbl_Baris_01.Text = "Harap Tunggu..."
        frm_ProgressLoadingData.lbl_Baris_02.Text = "Sistem sedang mengecek kesesuaian data."
        frm_ProgressLoadingData.lbl_ProgressReport.Text = "Jangan memutus proses ini..!"

        PesanUntukProgrammer("Untuk sementara ini proses dilakukan secara ByPass tanpa ada pengecekan kesesuaian data." & Enter2Baris &
                              "Untuk itu, coding pengecekan data dinonaktifkan sementara. Tapi nanti harus diaktifkan lagi." & Enter2Baris &
                              "Codingnya ada di bawah coding pesan ini.")
        'bgw_CekKesesuaianData.RunWorkerAsync()
        'frm_ProgressLoadingData.ShowDialog()

        If KesesuaianData_TrialBalance = False Then Return

        RefreshTampilanData()

    End Sub

    Sub CekKesesuaianData()

        ProgressValue = 0

        If JenisTahunBuku <> JenisTahunBuku_LAMPAU Then

            Dim pesan_DataTidakSesuai = "Trial Balance belum bisa diproses, karena :"

            ''1. Cek Kesesuaian Jurnal Pada Data Amortisasi Biaya :
            'usc_DaftarAmortisasiBiaya.KontenComboTahunLaporan()
            'PenambahanProgressKesesuaianData()
            'If usc_DaftarAmortisasiBiaya.KesesuaianJurnal = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Ada Data Amortisasi Biaya yang belum didorong ke Jurnal"
            'End If

            ''2. Cek Kesesuaian Jurnal Pada Data Penyusutan Asset Tetap :
            'usc_DaftarPenyusutanAssetTetap.ResetFilter()
            'usc_DaftarPenyusutanAssetTetap.TampilkanData_Detail_Rekap() '(Untuk menampilkan Jurnal)
            'PenambahanProgressKesesuaianData()
            'If usc_DaftarPenyusutanAssetTetap.KesesuaianJurnal = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Ada Data Penyusutan Asset Tetap yang belum didorong ke Jurnal"
            'End If

            ''3. Cek Kesesuaian Saldo Awal Hutang Usaha
            'usc_BukuPengawasanHutangUsaha.TampilkanData()
            'PenambahanProgressKesesuaianData()
            'If usc_BukuPengawasanHutangUsaha.KesesuaianSaldoAwal = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Data Saldo Awal Hutang Usaha tidak sesuai"
            'End If

            ''4. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang Bank :
            'If usc_BukuPengawasanHutangBank.StatusAktif Then usc_BukuPengawasanHutangBank.TampilkanData()
            'PenambahanProgressKesesuaianData()
            'If usc_BukuPengawasanHutangBank.KesesuaianSaldoAwal = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Data Saldo Awal Hutang Bank tidak sesuai"
            'End If

            ''4. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang Leasing :
            'If usc_BukuPengawasanHutangLeasing.StatusAktif Then usc_BukuPengawasanHutangLeasing.TampilkanData()
            'PenambahanProgressKesesuaianData()
            'If usc_BukuPengawasanHutangLeasing.KesesuaianSaldoAwal = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Data Saldo Awal Hutang Leasing tidak sesuai"
            'End If

            ''5. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang BPJS Kesehatan :
            'frm_BukuPengawasanTurunanGaji.TampilkanData()
            'PenambahanProgressKesesuaianData()
            'If frm_BukuPengawasanTurunanGaji.KesesuaianJurnal = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Ada Selisih pada Buku Pengawasan Hutang BPJS Kesehatan"
            'End If

            ''6. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang BPJS Ketenagakerjaan :
            ''frm_BukuPengawasanHutangBPJSKetenagakerjaan.TampilkanData()
            ''PenambahanProgressKesesuaianData()
            ''If frm_BukuPengawasanHutangBPJSKetenagakerjaan.KesesuaianJurnal = False Then
            ''    KesesuaianData_TrialBalance = False
            ''    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            ''        "- Ada Selisih pada Buku Pengawasan Hutang BPJS Ketenagakerjaan"
            ''End If

            ''7. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang PPh Pasal 21 :
            'usc_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
            'PenambahanProgressKesesuaianData()
            'If usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAwal_100 = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Data Saldo Awal Hutang PPh Pasal 21 tidak sesuai"
            'End If

            ''8. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang PPh Pasal 23 :
            'usc_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
            'PenambahanProgressKesesuaianData()
            'If usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_100 = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Data Saldo Awal Hutang PPh Pasal 23 tidak sesuai"
            'End If

            ''9. cek Kesesuaian jurnal pada buku pengawasan hutang pph pasal 25 :
            'usc_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
            'PenambahanProgressKesesuaianData()
            'If usc_BukuPengawasanHutangPPhPasal25.KesesuaianSaldoAwal = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- data saldo awal hutang pph pasal 25 tidak sesuai"
            'End If

            ''10. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang PPh Pasal 26 :
            'usc_BukuPengawasanHutangPPhPasal26.RefreshTampilanData()
            'PenambahanProgressKesesuaianData()
            'If usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_100 = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Data Saldo Awal Hutang PPh Pasal 26 tidak sesuai"
            'End If

            ''11. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang PPh Pasal 4 (2) :
            'usc_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
            'PenambahanProgressKesesuaianData()
            'If usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_402 = False Then
            '    KesesuaianData_TrialBalance = False
            '    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            '        "- Data Saldo Awal Hutang PPh Pasal 4 (2) tidak sesuai"
            'End If

            '''12. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang PPN :
            ''frm_BukuPengawasanPelaporanPPN.RefreshTampilanData()
            ''PenambahanProgressKesesuaianData()
            ''If frm_BukuPengawasanPelaporanPPN.KesesuaianSaldoAwal = False Then
            ''    KesesuaianData_TrialBalance = False
            ''    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            ''        "- Data Saldo Awal Hutang PPN tidak sesuai"
            ''End If

            '''13. Cek Kesesuaian Jurnal pada Buku Pengawasan Hutang Ketetapan Pajak :
            ''frm_BukuPengawasanKetetapanPajak.RefreshTampilanData()
            ''PenambahanProgressKesesuaianData()
            ''If frm_BukuPengawasanKetetapanPajak.KesesuaianSaldoAwal = False Then
            ''    KesesuaianData_TrialBalance = False
            ''    pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
            ''        "- Data Saldo Awal Ketetapan Pajak tidak sesuai"
            ''End If

            BeginInvoke(Sub() PesanUntukProgrammer(
                            "Koding untuk mengecek kesesuaian Saldo dari beberapa buku pengawasan belum dibuat...!!!" & Enter2Baris &
                            "- Cek semua 'Buku Pengawasan' yang memerlukan saldo awal." & Enter2Baris &
                            "- Hitung jumlah dari semua 'Buku Pengawasan' tersebut, ada berapa, dan sesuaikan dengan variabel 'ProgressMaximum.'" & Enter2Baris &
                            " "))

            '=========================================== SETELAH PENGECEKAN : ===========================================
            If KesesuaianData_TrialBalance = False Then
                BeginInvoke(Sub() MsgBox(pesan_DataTidakSesuai & "." & Enter2Baris &
                       "Silakan perbaiki dan sesuaikan semua data tersebut agar bisa dilakukan proses 'Trial Balance'."))
                If JalurMasuk <> Halaman_MENUUTAMA Then Me.Close()
                Return
            End If
            '============================================================================================================
        End If

    End Sub

    Sub PenambahanProgressKesesuaianData()
        ProgressValue += 1
        bgw_CekKesesuaianData.ReportProgress(ProgressValue)
    End Sub

    Private Sub cmb_TahunTrialBalance_SelectedIndexChanged(sender As Object, e As EventArgs)
        'TampilkanData()
    End Sub
    Private Sub cmb_TahunTrialBalance_TextChanged(sender As Object, e As EventArgs)
        'TampilkanData()
    End Sub
    Private Sub cmb_TahunTrialBalance_KeyPress(sender As Object, e As KeyPressEventArgs)
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub



    Private Sub bgw_CekKesesuaianData_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw_CekKesesuaianData.DoWork

        CekKesesuaianData()

    End Sub

    Private Sub bgw_CekKesesuaianData_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw_CekKesesuaianData.ProgressChanged

        frm_ProgressLoadingData.pgb_Progress.Value = ProgressValue

    End Sub

    Private Sub bgw_CekKesesuaianData_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw_CekKesesuaianData.RunWorkerCompleted

        Threading.Thread.Sleep(333) 'Jeda sedikit... Untuk menampilkan full progressbar
        frm_ProgressLoadingData.Close()

    End Sub

    Private Sub bgw_ProsesTrialBalance_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw_ProsesTrialBalance.DoWork

        TampilkanData()

    End Sub

    Private Sub bgw_ProsesTrialBalance_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw_ProsesTrialBalance.ProgressChanged

        frm_ProgressLoadingData.pgb_Progress.Value = ProgressValue

    End Sub

    Private Sub bgw_ProsesTrialBalance_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw_ProsesTrialBalance.RunWorkerCompleted

        Threading.Thread.Sleep(333) 'Jeda sedikit... Untuk menampilkan full progressbar
        frm_ProgressLoadingData.Close()
        TrialBalanceDone = True

    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick

    End Sub
End Class