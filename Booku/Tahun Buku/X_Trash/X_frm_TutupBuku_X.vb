Imports bcomm
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.ComponentModel

Public Class X_frm_TutupBuku_X

    Public JudulForm
    Dim BarisTerseleksi
    Dim COATerseleksi
    Dim ProsesSuntingDatabase As Boolean

    Dim pesan_TutupBukuGagal
    Dim TahunBukuDitutup
    Dim TanggalBukuDitutup
    Dim DatabaseTahunBukuBaruSudahAda As Boolean 'Untuk Jaga-jaga aja.
    Dim TahunBukuYangAkanDitutup


    Dim NomorID_Tabel

    Dim NomorBPHP
    Dim NomorBulan
    Dim MasaPajak
    Dim TanggalTransaksiPajak

    Dim TanggalInvoice
    Dim AngkaInvoice
    Dim AngkaInvoice_Sebelumnya

    Dim KodeSupplier
    Dim NamaSupplier
    Dim KodeCustomer
    Dim NamaCustomer

    Dim KesesuaianData As Boolean

    Private Sub frm_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Style_HalamanModul(Me)

        JudulForm = "Tutup Buku - Tahun " & TahunBukuAktif

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm
        DataTabelUtama.Columns("Saldo_Awal").HeaderText = "Saldo Awal " & TahunBukuAktif
        DataTabelUtama.Columns("Saldo_Akhir").HeaderText = "Saldo Akhir " & TahunBukuAktif
        DataTabelUtama.Columns("Saldo_Awal_Tahun_Berikutnya").HeaderText = "Saldo Awal " & (TahunBukuAktif + 1)

        If StatusTrialBalance = True Then TampilkanData()

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then Sub_JenisTahunBuku_BACKUP()
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then Sub_JenisTahunBuku_NORMAL()

    End Sub

    Sub ResetForm()
        btn_TransferSaldoDanTutupBuku.Enabled = False
        btn_TrialBalance.Enabled = True
        btn_EditSaldo.Enabled = False
    End Sub

    Sub Sub_JenisTahunBuku_BACKUP()
        btn_EditSaldo.Visible = True
        DataTabelUtama.Columns("Saldo_Awal").Visible = False
        DataTabelUtama.Columns("Debet_").Visible = False
        DataTabelUtama.Columns("Kredit_").Visible = False
    End Sub

    Sub Sub_JenisTahunBuku_NORMAL()
        btn_EditSaldo.Visible = False
        DataTabelUtama.Columns("Saldo_Awal").Visible = True
        DataTabelUtama.Columns("Debet_").Visible = True
        DataTabelUtama.Columns("Kredit_").Visible = True
    End Sub

    Sub TampilkanData()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel : 
        Dim NomorUrut = 0
        Dim KodeAkun
        Dim NamaAkun
        Dim SaldoAwal As Int64
        Dim Debet As Int64
        Dim Kredit As Int64
        Dim SaldoAkhir As Int64
        Dim Pemisah = Nothing

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA < '" & AwalAkunBiaya & "' AND Visibilitas = '" & Pilihan_Ya & "' ",
                              KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            KodeAkun = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            SaldoAwal = dr.Item("Saldo_Awal")
            Debet =
                dr.Item("Debet_Januari") +
                dr.Item("Debet_Februari") +
                dr.Item("Debet_Maret") +
                dr.Item("Debet_April") +
                dr.Item("Debet_Mei") +
                dr.Item("Debet_Juni") +
                dr.Item("Debet_Juli") +
                dr.Item("Debet_Agustus") +
                dr.Item("Debet_September") +
                dr.Item("Debet_Oktober") +
                dr.Item("Debet_Nopember") +
                dr.Item("Debet_Desember")
            Kredit =
                dr.Item("Kredit_Januari") +
                dr.Item("Kredit_Februari") +
                dr.Item("Kredit_Maret") +
                dr.Item("Kredit_April") +
                dr.Item("Kredit_Mei") +
                dr.Item("Kredit_Juni") +
                dr.Item("Kredit_Juli") +
                dr.Item("Kredit_Agustus") +
                dr.Item("Kredit_September") +
                dr.Item("Kredit_Oktober") +
                dr.Item("Kredit_Nopember") +
                dr.Item("Kredit_Desember")
            If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                SaldoAkhir = SaldoAwal
            Else
                SaldoAkhir = dr.Item("Saldo_Desember")
            End If

            DataTabelUtama.Rows.Add(NomorUrut, KodeAkun, NamaAkun, SaldoAwal, Debet, Kredit, SaldoAkhir, Pemisah, Nothing)

        Loop

        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

        If TahunBukuAktif < TahunIni Then btn_TransferSaldoDanTutupBuku.Enabled = True

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_EditSaldo.Enabled = False
    End Sub

    Private Sub btn_TrialBalance_Click(sender As Object, e As EventArgs) Handles btn_TrialBalance.Click
        'BeginInvoke(Sub() frm_Laporan_TrialBalance.btn_Proses_Click(sender, e))
        'frm_Laporan_TrialBalance.JalurMasuk = Halaman_TUTUPBUKU
        'frm_Laporan_TrialBalance.Show()
    End Sub

    Private Sub btn_EditSaldo_Click(sender As Object, e As EventArgs) Handles btn_EditSaldo.Click

        win_InputCOA = New wpfWin_InputCOA
        win_InputCOA.ResetForm()
        win_InputCOA.FungsiForm = FungsiForm_EDIT
        win_InputCOA.JalurMasuk = Halaman_TUTUPBUKU
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        win_InputCOA.txt_COA.Text = COATerseleksi
        win_InputCOA.txt_NamaAkun.Text = dr.Item("Nama_Akun")
        win_InputCOA.cmb_DebetKredit.Text = dr.Item("D_K")
        Dim InputSaldoAwal = dr.Item("Saldo_Awal")
        If InputSaldoAwal = 0 Then
            win_InputCOA.txt_SaldoAwal_IDR.Text = ""
        Else
            win_InputCOA.txt_SaldoAwal_IDR.Text = InputSaldoAwal
        End If
        win_InputCOA.txt_Uraian.Text = dr.Item("Uraian")
        win_InputCOA.cmb_Visibilitas.Text = dr.Item("Visibilitas")
        AksesDatabase_General(Tutup)
        win_InputCOA.ShowDialog()

        If win_InputCOA.ProsesSuntingDatabase = True Then
            'MsgBox("Setelah melakukan pengeditan Saldo COA, maka lakukan lagi Trial Balance sebelum Tutup Buku.")
            'btn_TrialBalance.Enabled = True
            'btn_TransferSaldoDanTutupBuku.Enabled = False
        End If

    End Sub

    Private Sub btn_TransferSaldoDanTutupBuku_Click(sender As Object, e As EventArgs) Handles btn_TransferSaldoDanTutupBuku.Click

        'Cek Keseimbangan Neraca Saldo Akhir
        usc_DataCOA.RefreshTampilanData()
        If usc_DataCOA.KeseimbanganNeraca = False Then
            MsgBox("Neraca pada Data COA tidak seimbang!" & Enter2Baris &
                   "Silakan seimbangkan terlebih dahulu Neraca pada halaman Data COA, atau silakan cek notifikasi.")
            Return
        End If

        Pilihan = MessageBox.Show("Silakan periksa kembali data SALDO dengan seksama." _
                                  & Enter2Baris & "Setelah selesai Proses Tutup Buku, Anda akan keluar dari Tahun Buku ini dan tidak dapat mengeditnya lagi." _
                                  & Enter2Baris & "Yakin melanjutkan 'Tutup Buku Tahun " & TahunBukuAktif & "'..?", "PERHATIAN..!!!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        ProsesTutupBuku = True
        KesesuaianData = True
        pesan_TutupBukuGagal = "Proses Tutup Buku GAGAL..!" & Enter2Baris & teks_SilakanCobaLagi_Database
        TahunBukuDitutup = TahunBukuAktif
        TanggalBukuDitutup = "31-12-" & TahunBukuDitutup

        TahunPajak = TahunBukuAktif 'Ini penting untuk ditentukan di awal. Untuk kepentingan Kesesuaian Saldo Awal Semua Jenis PPh.

        ProgressValue = 0
        ProgressMaximum = 10
        frm_ProgressLoadingData.lbl_Baris_01.Text = "Harap Tunggu..."
        frm_ProgressLoadingData.lbl_Baris_02.Text = "Sistem sedang mengecek kesesuaian data."
        frm_ProgressLoadingData.lbl_ProgressReport.Text = "Jangan memutus proses ini..!"
        PesanUntukProgrammer("Cek kesesuaian data, di-skip dulu...!")
        bgw_CekKesesuaianData.RunWorkerAsync()
        frm_ProgressLoadingData.ShowDialog()


        'Buat Tahun Buku Berikutnya :
        TahunBukuYangAkanDitutup = TahunBukuAktif
        TahunBukuBaru = TahunBukuAktif + 1
        JenisTahunBuku_Baru = JenisTahunBuku_NORMAL
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData " &
                              " WHERE Tahun_Buku = '" & TahunBukuBaru & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            DatabaseTahunBukuBaruSudahAda = True
        Else
            DatabaseTahunBukuBaruSudahAda = False
        End If
        AksesDatabase_General(Tutup)

        If DatabaseTahunBukuBaruSudahAda = False Then
            BuatDatabaseBaruTransaksi(TahunBukuBaru)
            If HasilPembuatanDatabaseTransaksi = True Then
                MsgBox("Database Tahun Buku " & TahunBukuBaru & " BERHASIL dibuat.")
            Else
                MsgBox("Database Tahun Buku " & TahunBukuBaru & " GAGAL dibuat karena ada kesalahan teknis." & Enter2Baris &
                       teks_SilakanCobaLagi_Database)
                ProsesTutupBuku = False
                Return
            End If
        End If

        'Pulihkan Beberapa Variabel dan Dsn Transaksi setelah Pembuatan Database Baru :
        TahunBukuAktif = TahunBukuAktifAsli
        BuatDsnTransaksi(TahunBukuAktifAsli)
        '(Jika tidak dilakukan pemulihan, berpotensi terjadi kekacauan data)

        'Isi Value Saldo Awal Tahun Buku yang baru, berdasarkan Saldo Akhir Tahun Buku yang akan ditutup :
        TransferSaldoAntarTahunBuku()

        'Hapus semua Hutang PPh, baik di Tabel Sisa Hutang PPh, maupun di Tabel Pembayaran Hutang PPh :
        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_HutangPajak ", KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PembayaranHutangPajak ", KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        'TransferData_SisaHutangUsaha_WPF(TahunBukuDitutup, TanggalBukuDitutup, pesan_TutupBukuGagal)
        'TransferData_SisaPiutangUsaha_WPF(TahunBukuDitutup, TanggalBukuDitutup, pesan_TutupBukuGagal)

        'TransferData_SisaHutangBank()

        'NomorID_Tabel = 0
        'TransferData_SisaHutang_PPhPasal21()
        TransferData_SisaHutang_PPhPasal23()
        'TransferData_SisaHutang_PPhPasal25()
        'TransferData_SisaHutang_PPhPasal26()
        'TransferData_SisaHutang_PPhPasal42()
        'TransferData_SisaHutang_PPN()
        'TransferData_SisaHutang_KetetapanPajak()


        IsiDataNotifikasi()

        CloseTahunBukuAktif()

        ProsesTutupBuku = False

    End Sub

    Sub CekKesesuaianData()

        Dim pesan_DataTidakSesuai = "Transfer Nilai Saldo dan Tutup Buku bermasalah, karena :"

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then

            'Cek Kesesuaian Saldo Akhir Hutang Usaha
            usc_BukuPengawasanHutangUsaha.TampilkanData()
            If usc_BukuPengawasanHutangUsaha.KesesuaianSaldoAkhir = False Then
                KesesuaianData = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Data Saldo Akhir Hutang Usaha tidak sesuai"
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 21 :
            usc_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
            If usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAkhir_100 = False Then
                KesesuaianData = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Data Saldo Akhir Hutang PPh Pasal 21 tidak sesuai"
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 23 :
            usc_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
            If usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_100 = False Then
                KesesuaianData = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Data Saldo Akhir Hutang PPh Pasal 23 tidak sesuai"
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 25 :
            usc_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
            If usc_BukuPengawasanHutangPPhPasal25.KesesuaianSaldoAkhir = False Then
                KesesuaianData = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Data Saldo Akhir Hutang PPh Pasal 25 tidak sesuai"
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 26 :
            usc_BukuPengawasanHutangPPhPasal26.RefreshTampilanData()
            If usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_100 = False Then
                KesesuaianData = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Data Saldo Akhir Hutang PPh Pasal 26 tidak sesuai"
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 29 :
            '(Belum Ada Coding)

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 4 (2) :
            usc_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
            If usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_402 = False Then
                KesesuaianData = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Data Saldo Akhir Hutang PPh Pasal 4 (2) tidak sesuai"
            End If

        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            'Belum Ada Coding
        End If

        If KesesuaianData = False Then
            MsgBox(pesan_DataTidakSesuai & "." & Enter2Baris &
                   "Proses Tutup Buku akan tetap dilanjutkan dengan beberapa catatan yang akan dikirim ke Pembukuan Tahun Berikutnya." &
                   Enter2Baris &
                   "Silakan nanti buka notifikasinya untuk arahan bagi perbaikan.")
        End If

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        COATerseleksi = DataTabelUtama.Item("COA_", BarisTerseleksi).Value

        If BarisTerseleksi >= 0 Then
            btn_EditSaldo.Enabled = True
        Else
            btn_EditSaldo.Enabled = False
        End If

    End Sub

    Private Sub bgw_CekKesesuaianData_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw_CekKesesuaianData.DoWork

        CekKesesuaianData()

    End Sub

    Private Sub bgw_CekKesesuaianData_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw_CekKesesuaianData.ProgressChanged

        frm_ProgressLoadingData.pgb_Progress.Value = ProgressValue

    End Sub

    Private Sub bgw_CekKesesuaianData_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw_CekKesesuaianData.RunWorkerCompleted

        frm_ProgressLoadingData.Close()

    End Sub

    Sub TransferSaldoAntarTahunBuku()

        Dim KodeAkun_Terindeks
        Dim SaldoAkhir_Terindeks As Int64
        Dim BarisTelusur = 0
        TahunBuku_Alternatif = TahunBukuBaru
        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            BarisTelusur += 1
            KodeAkun_Terindeks = AmbilAngka(row.Cells("COA_").Value)
            SaldoAkhir_Terindeks = AmbilAngka(row.Cells("Saldo_Akhir").Value)
            cmd = New OdbcCommand(" UPDATE tbl_SaldoAwalCOA " &
                                  " SET Saldo_Awal = '" & SaldoAkhir_Terindeks & "' " &
                                  " WHERE COA = '" & KodeAkun_Terindeks & "' ", KoneksiDatabaseTransaksi_Alternatif)
            cmd_ExecuteNonQuery()
            If StatusSuntingDatabase = False Then Exit For
            row.Cells("Saldo_Awal_Tahun_Berikutnya").Value = SaldoAkhir_Terindeks
            'Application.DoEvents()
            If BarisTelusur <= 9 Then System.Threading.Thread.Sleep(123) '(Untuk Dramatisasi saja.... Wkwkwk...)
        Next
        TutupDatabaseTransaksi_Alternatif()

        If StatusSuntingDatabase = True Then
            MsgBox("Data Saldo Akhir Tahun " & TahunBukuAktif & " BERHASIL dikirim sebagai Saldo Awal Tahun " & TahunBukuBaru & ".")
        Else
            MsgBox("Pengisian Saldo Awal Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Return
        End If

    End Sub

    Sub TransferData_SisaHutangUsaha()

        NomorID_Tabel = 0
        AngkaInvoice = 0
        Dim NomorPembelian
        Dim NomorPembelian_Sebelumnya = Kosongan
        KodeSupplier = Kosongan
        NamaSupplier = Kosongan
        Dim NomorIDBPHU = 0
        Dim NomorBPHU
        Dim KodeSetoran = Kosongan
        Dim NPPHU = Kosongan
        Dim TanggalBayar_HutangUsaha = TanggalBukuDitutup
        Dim JumlahTagihan_HutangUsaha
        Dim JumlahBayar_HutangUsaha
        Dim JumlahPPhDipotong_HutangUsaha
        Dim JumlahPencairan_HutangUsaha
        Dim SisaHutang_HutangUsaha
        Dim COAKredit = Kosongan
        Dim NomorJV = 0

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Pembelian_Invoice ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PembayaranHutangUsaha ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang Usaha Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorPembelian = dr.Item("Nomor_Pembelian")
            NomorBPHU = AwalanBPHU & Mid(NomorPembelian, PanjangTeks_AwalanBPHU_Plus1)
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            JumlahTagihan_HutangUsaha = dr.Item("Jumlah_Hutang_Usaha")

            JumlahBayar_HutangUsaha = 0
            JumlahPPhDipotong_HutangUsaha = 0
            JumlahPencairan_HutangUsaha = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha " &
                                       " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_HutangUsaha += drBAYAR.Item("Jumlah_Bayar")
                JumlahPPhDipotong_HutangUsaha += drBAYAR.Item("Jumlah_PPh_Dipotong")
                JumlahPencairan_HutangUsaha += drBAYAR.Item("Jumlah_Kredit")
            Loop
            SisaHutang_HutangUsaha = JumlahTagihan_HutangUsaha - JumlahBayar_HutangUsaha

            If SisaHutang_HutangUsaha > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Sisa Hutang Usaha
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_Pembelian_Invoice VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                dr.Item("Angka_Invoice") & "', '" &
                                dr.Item("Nomor_Invoice") & "', '" &
                                dr.Item("Jenis_Invoice") & "', '" &
                                dr.Item("Nomor_Pembelian") & "', '" &
                                dr.Item("Referensi") & "', '" &
                                dr.Item("N_P") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Invoice")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Diterima_Invoice")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Pembetulan")) & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Lapor")) & "', '" &
                                dr.Item("Jumlah_Hari_Jatuh_Tempo") & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                dr.Item("Jenis_Produk_Induk") & "', '" &
                                dr.Item("Kode_Supplier") & "', '" &
                                dr.Item("Nama_Supplier") & "', '" &
                                dr.Item("Jenis_Jasa") & "', '" &
                                dr.Item("Nomor_Urut_Produk") & "', '" &
                                dr.Item("Jenis_Produk_Per_Item") & "', '" &
                                dr.Item("Nomor_SJ_BAST_Produk") & "', '" &
                                dr.Item("Tanggal_SJ_BAST_Produk") & "', '" &
                                dr.Item("Tanggal_Diterima_SJ_BAST_Produk") & "', '" &
                                dr.Item("Kode_Project") & "', '" &
                                dr.Item("COA_Produk") & "', '" &
                                dr.Item("Nama_Produk") & "', '" &
                                dr.Item("Deskripsi_Produk") & "', '" &
                                dr.Item("Jumlah_Produk") & "', '" &
                                dr.Item("Satuan_Produk") & "', '" &
                                dr.Item("Harga_Satuan") & "', '" &
                                dr.Item("Diskon_Per_Item") & "', '" &
                                dr.Item("Total_Harga_Per_Item") & "', '" &
                                dr.Item("Jumlah_Harga_Keseluruhan") & "', '" &
                                dr.Item("Diskon") & "', '" &
                                dr.Item("Dasar_Pengenaan_Pajak") & "', '" &
                                dr.Item("Nomor_Faktur_Pajak") & "', '" &
                                dr.Item("Jenis_PPN") & "', '" &
                                dr.Item("Perlakuan_PPN") & "', '" &
                                dr.Item("PPN") & "', '" &
                                dr.Item("Jenis_PPh") & "', '" &
                                dr.Item("Kode_Setoran") & "', '" &
                                dr.Item("Tarif_PPh") & "', '" &
                                dr.Item("PPh_Terutang") & "', '" &
                                dr.Item("PPh_Ditanggung") & "', '" &
                                dr.Item("PPh_Dipotong") & "', '" &
                                dr.Item("Total_Tagihan") & "', '" &
                                dr.Item("Jumlah_Hutang_Usaha") & "', '" &
                                dr.Item("Jenis_Pembelian") & "', '" &
                                dr.Item("COA_Kredit") & "', '" &
                                dr.Item("Sarana_Pembayaran") & "', '" &
                                dr.Item("Biaya_Administrasi_Bank") & "', '" &
                                dr.Item("Ditanggung_Oleh") & "', '" &
                                dr.Item("Biaya_Transportasi") & "', '" &
                                dr.Item("Biaya_Materai") & "', '" &
                                dr.Item("Retur") & "', '" &
                                dr.Item("Catatan") & "', '" &
                                0 & "', '" &
                                dr.Item("User") & "', '" &
                                dr.Item("Koreksi") & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan value Jumlah_Bayar ke tbl_PembayaranHutangUsaha
                If StatusSuntingDatabase = True And NomorPembelian <> NomorPembelian_Sebelumnya And JumlahBayar_HutangUsaha > 0 Then
                    NomorIDBPHU = NomorIDBPHU + 1
                    Dim KeteranganBayar = Nothing
                    Dim BiayaAdministrasiBank = 0
                    Dim DitanggungOleh = Nothing
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PembayaranHutangUsaha VALUES ( " &
                                          " '" & NomorIDBPHU & "', " &
                                          " '" & NomorBPHU & "', " &
                                          " '" & NomorPembelian & "', " &
                                          " '" & dr.Item("Jenis_PPh") & "', " &
                                          " '" & dr.Item("Kode_Setoran") & "', " &
                                          " '" & NPPHU & "', " &
                                          " '" & KodeSupplier & "', " &
                                          " '" & NamaSupplier & "', " &
                                          " '" & TanggalFormatSimpan(TanggalBayar_HutangUsaha) & "', " &
                                          " '" & JumlahBayar_HutangUsaha & "', " &
                                          " '" & JumlahPPhDipotong_HutangUsaha & "', " &
                                          " '" & JumlahPencairan_HutangUsaha & "', " &
                                          " '" & COAKredit & "', " &
                                          " '" & BiayaAdministrasiBank & "', " &
                                          " '" & DitanggungOleh & "', " &
                                          " '" & KeteranganBayar & "', " &
                                          " '" & NomorJV & "', " &
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    If StatusSuntingDatabase = False Then Exit Do
                End If
            End If
            NomorPembelian_Sebelumnya = NomorPembelian
        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            MsgBox("Pengisian Saldo Awal Hutang Usaha Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            MsgBox("Pengisian Saldo Awal Hutang Usaha Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            MsgBox(pesan_TutupBukuGagal)
            Return
        End If

    End Sub


    Sub TransferData_SisaHutangBank()

        NomorID_Tabel = 0
        AngkaInvoice = 0
        KodeSupplier = Kosongan
        NamaSupplier = Kosongan
        Dim NomorID_JadwalAngsuran = 0
        Dim NomorBPHB
        Dim JumlahTagihan_HutangBank
        Dim JumlahBayar_HutangBank
        Dim SisaHutang_HutangBank
        Dim TanggalBayar = TanggalKosong

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_PengawasanHutangBank ",
                                   KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangBank ",
                              KoneksiDatabaseTransaksi) 'Bahan Loop Hutang Bank Tahun Buku Aktif
        dr_ExecuteReader()

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)

        Do While dr.Read
            NomorID_Tabel += 1
            NomorBPHB = dr.Item("Nomor_BPHB")
            JumlahTagihan_HutangBank = dr.Item("Jumlah_Pinjaman")

            JumlahBayar_HutangBank = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangBank " &
                                       " WHERE Nomor_BPHB = '" & NomorBPHB & "' AND Nomor_JV > 0 ",
                                       KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                JumlahBayar_HutangBank += drBAYAR.Item("Pokok")
            Loop
            SisaHutang_HutangBank = JumlahTagihan_HutangBank - JumlahBayar_HutangBank

            If SisaHutang_HutangBank > 0 Then 'Jika BELUM LUNAS, maka :

                'Simpan Data ke Tabel Sisa Hutang Usaha
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_PengawasanHutangBank VALUES ( '" &
                                NomorID_Tabel & "', '" &
                                NomorBPHB & "', '" &
                                dr.Item("Kode_Kreditur") & "', '" &
                                dr.Item("Nama_Kreditur") & "', '" &
                                TanggalKosongSimpan & "', '" &
                                TanggalFormatSimpan(dr.Item("Tanggal_Jatuh_Tempo")) & "', '" &
                                TanggalKosongSimpan & "', '" &
                                SisaHutang_HutangBank & "', '" &
                                dr.Item("Nomor_Kontrak") & "', '" &
                                0 & "', '" &
                                0 & "', '" &
                                0 & "', '" &
                                Kosongan & "', '" &
                                dr.Item("Keterangan") & "', '" &
                                0 & "', '" &
                                0 & "', '" &
                                UserAktif & "' ) ",
                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                If StatusSuntingDatabase = False Then Exit Do

                'Simpan Jadwal Angsuran Yang Belum Terbayarkan :
                If StatusSuntingDatabase = True Then
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangBank " &
                                                 " WHERE Nomor_BPHB = '" & NomorBPHB & "' AND Nomor_JV = 0 ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    Do While drTELUSUR.Read
                        NomorID_JadwalAngsuran += 1
                        cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_JadwalAngsuranHutangBank VALUES ( " &
                                          " '" & NomorID_JadwalAngsuran & "', " &
                                          " '" & NomorBPHB & "', " &
                                          " '" & drTELUSUR.Item("Kode_Kreditur") & "', " &
                                          " '" & drTELUSUR.Item("Angsuran_Ke") & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Jatuh_Tempo")) & "', " &
                                          " '" & TanggalFormatSimpan(drTELUSUR.Item("Tanggal_Bayar")) & "', " &
                                          " '" & drTELUSUR.Item("Pokok") & "', " &
                                          " '" & drTELUSUR.Item("Bagi_Hasil") & "', " &
                                          " '" & DesimalFormatSimpan(drTELUSUR.Item("Tarif_PPh")) & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_PPh") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Ditanggung") & "', " &
                                          " '" & drTELUSUR.Item("PPh_Dipotong") & "', " &
                                          " '" & drTELUSUR.Item("Jumlah_Dibayarkan") & "', " &
                                          " '" & drTELUSUR.Item("Denda") & "', " &
                                          " '" & drTELUSUR.Item("Jenis_PPh") & "', " &
                                          " '" & drTELUSUR.Item("Kode_Setoran") & "', " &
                                          " '" & drTELUSUR.Item("COA_Kredit") & "', " &
                                          " '" & drTELUSUR.Item("Biaya_Administrasi_Bank") & "', " &
                                          " '" & drTELUSUR.Item("Ditanggung_Oleh") & "', " &
                                          " '" & drTELUSUR.Item("Keterangan") & "', " &
                                          " '" & 0 & "', " &
                                          " '" & UserAktif & "' " &
                                          " ) ", KoneksiDatabaseTransaksi_Alternatif)
                        cmdSIMPAN_ExecuteNonQuery()
                        If StatusSuntingDatabase = False Then Exit Do
                    Loop
                End If

            End If

        Loop

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

    End Sub

    Sub TransferData_SisaHutang_PPhPasal21()
        Dim TahunHutangPajakTerlama_PPhPasal21 = AmbilTahunTerlama_SisaHutangPajak(JenisPajak_PPhPasal21)
        TanggalInvoice = TanggalKosong
        Dim TanggalBayar_HutangPPhPasal21 = TanggalBukuDitutup
        Dim Jumlah_HutangPPhPasal21_JasaOP
        Dim Jumlah_HutangPPhPasal21_Gaji

        AksesDatabase_Transaksi(Buka)

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)

        TahunPajak = TahunHutangPajakTerlama_PPhPasal21
        Do While TahunPajak <= TahunBukuAktif
            NomorBulan = 0
            Do While AmbilAngka(NomorBulan) < 12
                NomorBulan = AmbilAngka(NomorBulan) + 1
                MasaPajak = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBPHP21 & TahunPajak & "-" & NomorBulan.ToString
                Jumlah_HutangPPhPasal21_JasaOP = 0
                Jumlah_HutangPPhPasal21_Gaji = 0
                If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                    NomorBulan = "0" & NomorBulan.ToString
                Else
                    NomorBulan = NomorBulan.ToString
                End If
                'Data Hutang/Tagihan PPh Pasal 21 - Jasa OP :
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                             " WHERE Jenis_Pajak = '" & JenisPajak_PPhPasal21 & "' AND Nama_Jasa <> 'Gaji' " &
                                             " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                             KoneksiDatabaseTransaksi)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    Jumlah_HutangPPhPasal21_JasaOP += drTAGIHAN.Item("Jumlah_Hutang")
                Loop
                'Penyimpanan Data Sisa Hutang :
                TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunPajak, AmbilAngka(NomorBulan))
                NomorID_Tabel += 1
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                " '" & NomorID_Tabel & "', " &
                                                " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JenisPajak_PPhPasal21 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Jumlah_HutangPPhPasal21_JasaOP & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & UserAktif & "' ) ",
                                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                'Data Pembayaran tidak perlu ditransfer,
                'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
                '-----------------------------------------
                'Data Hutang/Tagihan PPh Pasal 21 - Gaji : 
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                             " WHERE Jenis_Pajak = '" & JenisPajak_PPhPasal21 & "' AND Nama_Jasa = 'Gaji' " &
                                             " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                             KoneksiDatabaseTransaksi)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    Jumlah_HutangPPhPasal21_Gaji += drTAGIHAN.Item("Jumlah_Hutang")
                Loop
                'Penyimpanan Data Sisa Hutang :
                TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunPajak, AmbilAngka(NomorBulan))
                NomorID_Tabel += 1
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                " '" & NomorID_Tabel & "', " &
                                                " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & "Gaji" & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JenisPajak_PPhPasal21 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Jumlah_HutangPPhPasal21_Gaji & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & UserAktif & "' ) ",
                                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                'Data Pembayaran tidak perlu ditransfer,
                'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
            Loop
            TahunPajak += 1
        Loop

        'Jika Tahun Buku NORMAL
        If StatusSuntingDatabase = True Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                NomorBulan = 0
                Do While AmbilAngka(NomorBulan) < 12
                    NomorBulan = AmbilAngka(NomorBulan) + 1
                    MasaPajak = BulanTerbilang(NomorBulan)
                    NomorBPHP = AwalanBPHP21 & TahunBukuAktif & "-" & NomorBulan.ToString
                    Jumlah_HutangPPhPasal21_JasaOP = 0
                    Jumlah_HutangPPhPasal21_Gaji = 0
                    If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                        NomorBulan = "0" & NomorBulan.ToString
                    Else
                        NomorBulan = NomorBulan.ToString
                    End If
                    'Data Hutang/Tagihan PPh Pasal 21 dari Pembayaran atas Pembelian Tunai (Jasa OP) :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Angka_Invoice, PPh_Dipotong FROM tbl_Pembelian_Invoice " &
                                                 " WHERE Jenis_PPh = '" & JenisPPh_Pasal21 & "' " &
                                                 " AND Jenis_Pembelian = '" & JenisPembelian_Tunai & "' " &
                                                 " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    AngkaInvoice_Sebelumnya = 0
                    Do While drTAGIHAN.Read
                        AngkaInvoice = drTAGIHAN.Item("Angka_Invoice")
                        If AngkaInvoice <> AngkaInvoice_Sebelumnya Then Jumlah_HutangPPhPasal21_JasaOP += drTAGIHAN.Item("PPh_Dipotong")
                        AngkaInvoice_Sebelumnya = AngkaInvoice
                    Loop
                    'Data Hutang/Tagihan PPh Pasal 21 dari Pembayaran atas Hutang Usaha (Jasa OP) :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Jumlah_PPh_Dipotong FROM tbl_PembayaranHutangUsaha " &
                                                 " WHERE Jenis_PPh = '" & JenisPPh_Pasal21 & "' " &
                                                 " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    Do While drTAGIHAN.Read
                        Jumlah_HutangPPhPasal21_JasaOP += drTAGIHAN.Item("Jumlah_PPh_Dipotong")
                    Loop
                    'Penyimpanan Data Sisa Hutang : 
                    TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunBukuAktif, AmbilAngka(NomorBulan))
                    NomorID_Tabel += 1
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                    " '" & NomorID_Tabel & "', " &
                                                    " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                    " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & JenisPajak_PPhPasal21 & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Jumlah_HutangPPhPasal21_JasaOP & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & UserAktif & "' ) ",
                                                    KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    'Data Pembayaran tidak perlu ditransfer,
                    'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
                    '--------------------------------------------
                    'Data Hutang/Tagihan PPh Pasal 21 dari Gaji :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Potongan_Hutang_PPh_Pasal_21 FROM tbl_PengawasanGaji " &
                                                 " WHERE DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    Do While drTAGIHAN.Read
                        Jumlah_HutangPPhPasal21_Gaji += drTAGIHAN.Item("Potongan_Hutang_PPh_Pasal_21")
                    Loop
                    'Penyimpanan Data Sisa Hutang : 
                    TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunBukuAktif, AmbilAngka(NomorBulan))
                    NomorID_Tabel += 1
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                    " '" & NomorID_Tabel & "', " &
                                                    " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                    " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & "Gaji" & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & JenisPajak_PPhPasal21 & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Jumlah_HutangPPhPasal21_Gaji & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & UserAktif & "' ) ",
                                                    KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    'Data Pembayaran tidak perlu ditransfer,
                    'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
                Loop
            End If
        End If

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 21 Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 21 Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
               & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            MsgBox(pesan_TutupBukuGagal)
            Return
        End If

    End Sub

    Sub TransferData_SisaHutang_PPhPasal23()

        Dim TahunHutangPajakTerlama_PPhPasal23 = AmbilTahunTerlama_SisaHutangPajak(JenisPajak_PPhPasal23)
        Dim TanggalBayar_HutangPPhPasal23 = TanggalBukuDitutup
        Dim JumlahTagihan_HutangPPhPasal23

        AksesDatabase_Transaksi(Buka)

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)

        TahunPajak = TahunHutangPajakTerlama_PPhPasal23
        Do While TahunPajak <= TahunBukuAktif
            NomorBulan = 0
            Do While AmbilAngka(NomorBulan) < 12
                NomorBulan = AmbilAngka(NomorBulan) + 1
                MasaPajak = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBPHP23 & TahunPajak & "-" & NomorBulan.ToString
                JumlahTagihan_HutangPPhPasal23 = 0
                If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                    NomorBulan = "0" & NomorBulan.ToString
                Else
                    NomorBulan = NomorBulan.ToString
                End If
                'Data Hutang/Tagihan PPh Pasal 23 :
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                             " WHERE Jenis_Pajak = '" & JenisPajak_PPhPasal23 & "' " &
                                             " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                             KoneksiDatabaseTransaksi)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    JumlahTagihan_HutangPPhPasal23 += drTAGIHAN.Item("Jumlah_Hutang")
                Loop
                'Penyimpanan Data Sisa Hutang :
                TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunPajak, AmbilAngka(NomorBulan))
                NomorID_Tabel += 1
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                " '" & NomorID_Tabel & "', " &
                                                " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JenisPajak_PPhPasal23 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JumlahTagihan_HutangPPhPasal23 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & UserAktif & "' ) ",
                                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                'Data Pembayaran tidak perlu ditransfer,
                'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
            Loop
            TahunPajak += 1
        Loop

        'Jika Tahun Buku NORMAL
        If StatusSuntingDatabase = True Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                NomorBulan = 0
                Do While AmbilAngka(NomorBulan) < 12
                    NomorBulan = AmbilAngka(NomorBulan) + 1
                    MasaPajak = BulanTerbilang(NomorBulan)
                    NomorBPHP = AwalanBPHP23 & TahunBukuAktif & "-" & NomorBulan.ToString
                    JumlahTagihan_HutangPPhPasal23 = 0
                    If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                        NomorBulan = "0" & NomorBulan.ToString
                    Else
                        NomorBulan = NomorBulan.ToString
                    End If
                    'Data Hutang/Tagihan PPh Pasal 23 dari Pembayaran atas Pembelian Tunai :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Angka_Invoice, PPh_Dipotong FROM tbl_Pembelian_Invoice " &
                                                 " WHERE Jenis_PPh = '" & JenisPPh_Pasal23 & "' " &
                                                 " AND Jenis_Pembelian = '" & JenisPembelian_Tunai & "' " &
                                                 " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    AngkaInvoice_Sebelumnya = 0
                    Do While drTAGIHAN.Read
                        AngkaInvoice = drTAGIHAN.Item("Angka_Invoice")
                        If AngkaInvoice <> AngkaInvoice_Sebelumnya Then JumlahTagihan_HutangPPhPasal23 += drTAGIHAN.Item("PPh_Dipotong")
                        AngkaInvoice_Sebelumnya = AngkaInvoice
                    Loop
                    'Data Hutang/Tagihan PPh Pasal 23 dari Pembayaran atas Hutang Usaha :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Jumlah_PPh_Dipotong FROM tbl_PembayaranHutangUsaha " &
                                                 " WHERE Jenis_PPh = '" & JenisPPh_Pasal23 & "' " &
                                                 " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    Do While drTAGIHAN.Read
                        JumlahTagihan_HutangPPhPasal23 += drTAGIHAN.Item("Jumlah_PPh_Dipotong")
                    Loop
                    'Penyimpanan Data Sisa Hutang :
                    TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunBukuAktif, AmbilAngka(NomorBulan))
                    NomorID_Tabel += 1
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                    " '" & NomorID_Tabel & "', " &
                                                    " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                    " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & JenisPajak_PPhPasal23 & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & JumlahTagihan_HutangPPhPasal23 & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & UserAktif & "' ) ",
                                                    KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    'Data Pembayaran tidak perlu ditransfer,
                    'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
                Loop
            End If
        End If

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 23 Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 23 Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
               & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            MsgBox(pesan_TutupBukuGagal)
            Return
        End If

    End Sub

    Sub TransferData_SisaHutang_PPhPasal42()

        Dim TahunHutangPajakTerlama_PPhPasal42 = AmbilTahunTerlama_SisaHutangPajak(JenisPajak_PPhPasal42)
        Dim TanggalBayar_HutangPPhPasal42 = TanggalBukuDitutup
        Dim JumlahTagihan_HutangPPhPasal42

        AksesDatabase_Transaksi(Buka)

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)

        TahunPajak = TahunHutangPajakTerlama_PPhPasal42
        Do While TahunPajak <= TahunBukuAktif
            NomorBulan = 0
            Do While AmbilAngka(NomorBulan) < 12
                NomorBulan = AmbilAngka(NomorBulan) + 1
                MasaPajak = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBPHP42 & TahunPajak & "-" & NomorBulan.ToString
                JumlahTagihan_HutangPPhPasal42 = 0
                If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                    NomorBulan = "0" & NomorBulan.ToString
                Else
                    NomorBulan = NomorBulan.ToString
                End If
                'Data Hutang/Tagihan PPh Pasal 4 (2) : 
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                             " WHERE Jenis_Pajak = '" & JenisPajak_PPhPasal42 & "' " &
                                             " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                             KoneksiDatabaseTransaksi)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    JumlahTagihan_HutangPPhPasal42 += drTAGIHAN.Item("Jumlah_Hutang")
                Loop
                'Penyimpanan Data Sisa Hutang : 
                TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunPajak, AmbilAngka(NomorBulan))
                NomorID_Tabel += 1
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                " '" & NomorID_Tabel & "', " &
                                                " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JenisPajak_PPhPasal42 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JumlahTagihan_HutangPPhPasal42 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & UserAktif & "' ) ",
                                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                'Data Pembayaran tidak perlu ditransfer,
                'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
            Loop
            TahunPajak += 1
        Loop

        'Jika Tahun Buku NORMAL
        If StatusSuntingDatabase = True Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                NomorBulan = 0
                Do While AmbilAngka(NomorBulan) < 12
                    NomorBulan = AmbilAngka(NomorBulan) + 1
                    MasaPajak = BulanTerbilang(NomorBulan)
                    NomorBPHP = AwalanBPHP42 & TahunBukuAktif & "-" & NomorBulan.ToString
                    JumlahTagihan_HutangPPhPasal42 = 0
                    If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                        NomorBulan = "0" & NomorBulan.ToString
                    Else
                        NomorBulan = NomorBulan.ToString
                    End If
                    'Data Hutang/Tagihan PPh Pasal 4 (2) dari Pembayaran atas Pembelian Tunai :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Angka_Invoice, PPh_Dipotong FROM tbl_Pembelian_Invoice " &
                                                 " WHERE Jenis_PPh = '" & JenisPPh_Pasal42 & "' " &
                                                 " AND Jenis_Pembelian = '" & JenisPembelian_Tunai & "' " &
                                                 " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    AngkaInvoice_Sebelumnya = 0
                    Do While drTAGIHAN.Read
                        AngkaInvoice = drTAGIHAN.Item("Angka_Invoice")
                        If AngkaInvoice <> AngkaInvoice_Sebelumnya Then JumlahTagihan_HutangPPhPasal42 += drTAGIHAN.Item("PPh_Dipotong")
                        AngkaInvoice_Sebelumnya = AngkaInvoice
                    Loop
                    'Data Hutang/Tagihan PPh Pasal 4 (2) dari Pembayaran atas Hutang Usaha :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Jumlah_PPh_Dipotong FROM tbl_PembayaranHutangUsaha " &
                                                 " WHERE Jenis_PPh = '" & JenisPPh_Pasal42 & "' " &
                                                 " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    Do While drTAGIHAN.Read
                        JumlahTagihan_HutangPPhPasal42 += drTAGIHAN.Item("Jumlah_PPh_Dipotong")
                    Loop
                    'Penyimpanan Data Sisa Hutang :
                    TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunBukuAktif, AmbilAngka(NomorBulan))
                    NomorID_Tabel += 1
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                " '" & NomorID_Tabel & "', " &
                                                " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JenisPajak_PPhPasal42 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JumlahTagihan_HutangPPhPasal42 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & UserAktif & "' ) ",
                                                KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    'Data Pembayaran tidak perlu ditransfer,
                    'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
                Loop
            End If
        End If

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 4 (2) Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 4 (2) Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
               & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            MsgBox(pesan_TutupBukuGagal)
            Return
        End If

    End Sub

    Sub TransferData_SisaHutang_PPhPasal25()

        'Untuk sementara ini, tidak ada pentransferan data untuk Hutang PPh Pasal 25
        'Datanya ada di masing-masing Tahun Buku
        If StatusSuntingDatabase = True Then
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 25 Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 25 Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
               & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            MsgBox(pesan_TutupBukuGagal)
            Return
        End If

    End Sub

    Sub TransferData_SisaHutang_PPhPasal26()

        Dim TahunHutangPajakTerlama_PPhPasal26 = AmbilTahunTerlama_SisaHutangPajak(JenisPajak_PPhPasal26)
        Dim TanggalBayar_HutangPPhPasal26 = TanggalBukuDitutup
        Dim JumlahTagihan_HutangPPhPasal26

        AksesDatabase_Transaksi(Buka)

        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)

        TahunPajak = TahunHutangPajakTerlama_PPhPasal26
        Do While TahunPajak <= TahunBukuAktif
            NomorBulan = 0
            Do While AmbilAngka(NomorBulan) < 12
                NomorBulan = AmbilAngka(NomorBulan) + 1
                MasaPajak = BulanTerbilang(NomorBulan)
                NomorBPHP = AwalanBPHP26 & TahunPajak & "-" & NomorBulan.ToString
                JumlahTagihan_HutangPPhPasal26 = 0
                If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                    NomorBulan = "0" & NomorBulan.ToString
                Else
                    NomorBulan = NomorBulan.ToString
                End If
                'Data Hutang/Tagihan PPh Pasal 26 :
                cmdTAGIHAN = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                             " WHERE Jenis_Pajak = '" & JenisPajak_PPhPasal26 & "' " &
                                             " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunPajak & "-" & NomorBulan & "' ",
                                             KoneksiDatabaseTransaksi)
                drTAGIHAN_ExecuteReader()
                Do While drTAGIHAN.Read
                    JumlahTagihan_HutangPPhPasal26 += drTAGIHAN.Item("Jumlah_Hutang")
                Loop
                'Penyimpanan Data Sisa Hutang : 
                TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunPajak, AmbilAngka(NomorBulan))
                NomorID_Tabel += 1
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                " '" & NomorID_Tabel & "', " &
                                                " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JenisPajak_PPhPasal26 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & JumlahTagihan_HutangPPhPasal26 & "', " &
                                                " '" & Nothing & "', " &
                                                " '" & UserAktif & "' ) ",
                                                KoneksiDatabaseTransaksi_Alternatif)
                cmdSIMPAN_ExecuteNonQuery()
                'Data Pembayaran tidak perlu ditransfer,
                'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
            Loop
            TahunPajak += 1
        Loop

        'Jika Tahun Buku NORMAL
        If StatusSuntingDatabase = True Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                NomorBulan = 0
                Do While AmbilAngka(NomorBulan) < 12
                    NomorBulan = AmbilAngka(NomorBulan) + 1
                    MasaPajak = BulanTerbilang(NomorBulan)
                    NomorBPHP = AwalanBPHP26 & TahunBukuAktif & "-" & NomorBulan.ToString
                    JumlahTagihan_HutangPPhPasal26 = 0
                    If Microsoft.VisualBasic.Len(NomorBulan.ToString) = 1 Then
                        NomorBulan = "0" & NomorBulan.ToString
                    Else
                        NomorBulan = NomorBulan.ToString
                    End If
                    'Data Hutang/Tagihan PPh Pasal 26 dari Pembayaran atas Pembelian Tunai :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Angka_Invoice, PPh_Dipotong FROM tbl_Pembelian_Invoice " &
                                                 " WHERE Jenis_PPh = '" & JenisPPh_Pasal26 & "' " &
                                                 " AND Jenis_Pembelian = '" & JenisPembelian_Tunai & "' " &
                                                 " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    AngkaInvoice_Sebelumnya = 0
                    Do While drTAGIHAN.Read
                        AngkaInvoice = drTAGIHAN.Item("Angka_Invoice")
                        If AngkaInvoice <> AngkaInvoice_Sebelumnya Then JumlahTagihan_HutangPPhPasal26 += drTAGIHAN.Item("PPh_Dipotong")
                        AngkaInvoice_Sebelumnya = AngkaInvoice
                    Loop
                    'Data Hutang/Tagihan PPh Pasal 26 dari Pembayaran atas Hutang Usaha :
                    cmdTAGIHAN = New OdbcCommand(" SELECT Jumlah_PPh_Dipotong FROM tbl_PembayaranHutangUsaha " &
                                                 " WHERE Jenis_PPh = '" & JenisPPh_Pasal26 & "' " &
                                                 " AND DATE_FORMAT(Tanggal_Bayar, '%Y-%m') = '" & TahunBukuAktif & "-" & NomorBulan & "' ",
                                                 KoneksiDatabaseTransaksi)
                    drTAGIHAN_ExecuteReader()
                    Do While drTAGIHAN.Read
                        JumlahTagihan_HutangPPhPasal26 += drTAGIHAN.Item("Jumlah_PPh_Dipotong")
                    Loop
                    'Penyimpanan Data Sisa Hutang : 
                    TanggalTransaksiPajak = TanggalAkhirBulan_Case(TahunBukuAktif, AmbilAngka(NomorBulan))
                    NomorID_Tabel += 1
                    cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_HutangPajak VALUES ( " &
                                                    " '" & NomorID_Tabel & "', " &
                                                    " '" & TanggalFormatSimpan(TanggalTransaksiPajak) & "', " &
                                                    " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & JenisPajak_PPhPasal26 & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & JumlahTagihan_HutangPPhPasal26 & "', " &
                                                    " '" & Nothing & "', " &
                                                    " '" & UserAktif & "' ) ",
                                                    KoneksiDatabaseTransaksi_Alternatif)
                    cmdSIMPAN_ExecuteNonQuery()
                    'Data Pembayaran tidak perlu ditransfer,
                    'karena nanti pembacaan data pembayaran akan menelusuri semua Database Tahun Buku terkait.
                Loop
            End If
        End If

        TutupDatabaseTransaksi_Alternatif()

        AksesDatabase_Transaksi(Tutup)

        'Laporan :
        If StatusSuntingDatabase = True Then
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 26 Tahun Buku " & TahunBukuBaru & " BERHASIL.")
        Else
            MsgBox("Pengisian Saldo Awal Hutang PPh Pasal 26 Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
               & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            MsgBox(pesan_TutupBukuGagal)
            Return
        End If

    End Sub

    Sub TransferData_SisaHutang_PPN()

    End Sub

    Sub TransferData_SisaHutang_KetetapanPajak()

    End Sub

    Sub IsiDataNotifikasi()

        'Kosongkan Data Notifikasi :
        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        cmd = New OdbcCommand(" DELETE FROM tbl_Notifikasi ", KoneksiDatabaseTransaksi_Alternatif)
        cmd.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        'Pengisian Data Notifikasi :
        TahunPajak = TahunBukuAktif 'Ini penting untuk ditentukan di awal. Untuk kepentingan Kesesuaian Saldo Awal Semua Jenis PPh.
        Dim NomorID_Notifikasi = 0
        Dim WaktuNotifikasi = Today
        notif_StatusDibaca = 0
        notif_StatusDieksekusi = 0

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then

            'Cek Kesesuaian Saldo Akhir Hutang Usaha
            If usc_BukuPengawasanHutangUsaha.KesesuaianSaldoAkhir = False Then
                NomorID_Notifikasi += 1
                notif_NomorID = NomorID_Notifikasi
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = WaktuNotifikasi
                notif_Notifikasi = "Ada selisih pada Saldo Awal Hutang Usaha." & Enter1Baris & "Silakan perbaiki...!"
                notif_HalamanTarget = Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI
                notif_Pesan = teks_SilakanSesuaikanSaldo
                SimpanNotifikasi()
            End If

            'Cek Kesesuaian Saldo Akhir Hutang Bank
            If usc_BukuPengawasanHutangBank.KesesuaianSaldoAkhir = False Then
                NomorID_Notifikasi += 1
                notif_NomorID = NomorID_Notifikasi
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = WaktuNotifikasi
                notif_Notifikasi = "Ada selisih pada Saldo Awal Hutang Bank." & Enter1Baris & "Silakan perbaiki...!"
                notif_HalamanTarget = Halaman_BUKUPENGAWASANHUTANGBANK
                notif_Pesan = teks_SilakanSesuaikanSaldo
                SimpanNotifikasi()
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 21 :
            If usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAkhir_100 = False Then
                NomorID_Notifikasi += 1
                notif_NomorID = NomorID_Notifikasi
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = WaktuNotifikasi
                notif_Notifikasi = "Ada selisih pada Saldo Awal Hutang PPh Pasal 21." & Enter1Baris & "Silakan perbaiki...!"
                notif_HalamanTarget = Halaman_BUKUPENGAWASANHUTANGPPHPASAL21
                notif_Pesan = teks_SilakanSesuaikanSaldo
                SimpanNotifikasi()
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 23 :
            If usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_100 = False Then
                NomorID_Notifikasi += 1
                notif_NomorID = NomorID_Notifikasi
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = WaktuNotifikasi
                notif_Notifikasi = "Ada selisih pada Saldo Awal Hutang PPh Pasal 23." & Enter1Baris & "Silakan perbaiki...!"
                notif_HalamanTarget = Halaman_BUKUPENGAWASANHUTANGPPHPASAL23
                notif_Pesan = teks_SilakanSesuaikanSaldo
                SimpanNotifikasi()
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 25 :
            If usc_BukuPengawasanHutangPPhPasal25.KesesuaianSaldoAkhir = False Then
                NomorID_Notifikasi += 1
                notif_NomorID = NomorID_Notifikasi
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = WaktuNotifikasi
                notif_Notifikasi = "Ada selisih pada Saldo Awal Hutang PPh Pasal 25." & Enter1Baris & "Silakan perbaiki...!"
                notif_HalamanTarget = Halaman_BUKUPENGAWASANHUTANGPPHPASAL25
                notif_Pesan = teks_SilakanSesuaikanSaldo
                SimpanNotifikasi()
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 26 :
            If usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_100 = False Then
                NomorID_Notifikasi += 1
                notif_NomorID = NomorID_Notifikasi
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = WaktuNotifikasi
                notif_Notifikasi = "Ada selisih pada Saldo Awal Hutang PPh Pasal 26." & Enter1Baris & "Silakan perbaiki...!"
                notif_HalamanTarget = Halaman_BUKUPENGAWASANHUTANGPPHPASAL26
                notif_Pesan = teks_SilakanSesuaikanSaldo
                SimpanNotifikasi()
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 4 (2) :
            If usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_402 = False Then
                NomorID_Notifikasi += 1
                notif_NomorID = NomorID_Notifikasi
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = WaktuNotifikasi
                notif_Notifikasi = "Ada selisih pada Saldo Awal Hutang PPh Pasal 4 (2)." & Enter1Baris & "Silakan perbaiki...!"
                notif_HalamanTarget = Halaman_BUKUPENGAWASANHUTANGPPHPASAL42
                notif_Pesan = teks_SilakanSesuaikanSaldo
                SimpanNotifikasi()
            End If

            'Cek Kesesuaian Saldo Akhir Hutang PPh Pasal 29 :
            '(Belum Ada Coding)

        End If

    End Sub

    Sub CloseTahunBukuAktif()

        'Close Tahun Buku Aktif :
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" UPDATE tbl_InfoData " &
                              " SET Status_Buku = '" & StatusBuku_CLOSED & "' " &
                              " WHERE Tahun_Buku = '" & TahunBukuYangAkanDitutup & "' ", KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()
        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            MsgBox("Proses Tutup Buku Tahun " & TahunBukuYangAkanDitutup & " BERHASIL.")
        Else
            MsgBox("Proses Tutup Buku GAGAL karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Return
        End If

        'Masuk ke Tahun Buku Baru
        frm_GantiTahunBuku.FungsiForm = FungsiForm_GANTITAHUNBUKU
        frm_GantiTahunBuku.ProsesGantiTahunBuku()

    End Sub

End Class