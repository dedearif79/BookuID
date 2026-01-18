Imports System.Data.Odbc
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm
Imports MySql.Data.MySqlClient

Public Class wpfUsc_TutupBuku

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    ' Flag untuk mencegah multiple loading bersamaan
    Private SedangMemuatData As Boolean = False

    Public JudulForm
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

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        Terabas()

        ProsesLoadingForm = True
        KetersediaanMenuHalaman(pnl_Halaman, False)

        'lbl_JudulForm.Text = frm_TutupBuku.JudulForm

        Saldo_Awal.Header = "Saldo Awal " & Enter1Baris & "Tahun " & TahunBukuAktif
        Saldo_Akhir.Header = "Saldo Akhir " & Enter1Baris & "Tahun " & TahunBukuAktif
        Saldo_Awal_Tahun_Berikutnya.Header = "Saldo Awal " & Enter1Baris & "Tahun " & (TahunBukuAktif + 1)

        If StatusTrialBalance = True Then TampilkanData()

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then KondisiJenisTahunBukuLAMPAU()
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then KondisiJenisTahunBukuNORMAL()

        KetersediaanMenuHalaman(pnl_Halaman, True)
        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub



    Sub ResetForm()
        btn_TransferSaldoDanTutupBuku.IsEnabled = False
        btn_EditSaldo.IsEnabled = False
    End Sub

    Sub KondisiJenisTahunBukuLAMPAU()
        btn_EditSaldo.Visibility = Visibility.Visible
        Saldo_Awal.Visibility = Visibility.Collapsed
        Debet_.Visibility = Visibility.Collapsed
        Kredit_.Visibility = Visibility.Collapsed
    End Sub

    Sub KondisiJenisTahunBukuNORMAL()
        btn_EditSaldo.Visibility = Visibility.Collapsed
        Saldo_Awal.Visibility = Visibility.Visible
        Debet_.Visibility = Visibility.Visible
        Kredit_.Visibility = Visibility.Visible
    End Sub

    Async Sub TampilkanDataAsync()

        ' Guard clause: Jangan eksekusi jika sedang loading
        If SedangMemuatData Then Return
        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            datatabelUtama.Rows.Clear()

            'Data Tabel :
            Dim NomorUrut = 0
            Dim KodeAkun
            Dim NamaAkun
            Dim SaldoAwal As Int64
            Dim Debet As Int64
            Dim Kredit As Int64
            Dim SaldoAkhir As Int64

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

                datatabelUtama.Rows.Add(NomorUrut, KodeAkun, NamaAkun, SaldoAwal, Debet, Kredit, SaldoAkhir, 0)

                Await Task.Yield()

            Loop

            AksesDatabase_General(Tutup)

            If TahunBukuAktif < TahunIni Then btn_TransferSaldoDanTutupBuku.IsEnabled = True

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_TutupBuku")
            SedangMemuatData = False

        Finally
            BersihkanSeleksi_SetelahLoading()
        End Try

    End Sub

    ' Wrapper untuk backward compatibility
    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_EditSaldo.IsEnabled = False
        SedangMemuatData = False
    End Sub

    ' Wrapper: reset seleksi + enable UI (untuk backward compatibility)
    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub



    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        COATerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_")

        If COATerseleksi = Kosongan Then
            btn_EditSaldo.IsEnabled = False
        Else
            btn_EditSaldo.IsEnabled = True
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        'Belum ada kebutuhan Coding di sini.
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If AmbilAngka(e.Row.Item("Saldo_Awal")) = 0 And AmbilAngka(e.Row.Item("Saldo_Akhir")) = 0 Then
            e.Row.Foreground = clrNeutral500
        Else
            e.Row.Foreground = clrTeksPrimer
        End If
    End Sub



    Private Sub btn_EditSaldo_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditSaldo.Click

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
        win_InputCOA.cmb_DebetKredit.SelectedValue = dr.Item("D_K")
        Dim InputSaldoAwal = dr.Item("Saldo_Awal")
        If InputSaldoAwal = 0 Then
            win_InputCOA.txt_SaldoAwal_IDR.Text = ""
        Else
            win_InputCOA.txt_SaldoAwal_IDR.Text = InputSaldoAwal
        End If
        win_InputCOA.txt_Uraian.Text = dr.Item("Uraian")
        win_InputCOA.cmb_Visibilitas.SelectedValue = dr.Item("Visibilitas")
        AksesDatabase_General(Tutup)
        win_InputCOA.ShowDialog()

        If win_InputCOA.ProsesSuntingDatabase = True Then
            'MsgBox("Setelah melakukan pengeditan Saldo COA, maka lakukan lagi Trial Balance sebelum Tutup Buku.")
            'btn_TrialBalance.Enabled = True
            'btn_TransferSaldoDanTutupBuku.Enabled = False
        End If


    End Sub

    Dim JumlahBukuYangDicek
    Private Sub btn_TransferSaldoDanTutupBuku_Click(sender As Object, e As RoutedEventArgs) Handles btn_TransferSaldoDanTutupBuku.Click

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            FiturBelumBisaDigunakan()
            Return
        End If

        KetersediaanMenuHalaman(pnl_Halaman, False)

        'Cek Keseimbangan Neraca Saldo Akhir
        usc_DataCOA.RefreshTampilanData()
        If usc_DataCOA.KeseimbanganNeraca = False Then
            Pesan_Peringatan("Neraca pada Data COA tidak seimbang!" & Enter2Baris &
                   "Silakan seimbangkan terlebih dahulu Neraca pada halaman Data COA, atau silakan cek notifikasi.")
            KetersediaanMenuHalaman(pnl_Halaman, True)
            Return
        End If


        Dim PesanKonfirmasi = "Silakan periksa kembali data SALDO dengan seksama." &
                              Enter2Baris & "Setelah selesai Proses Tutup Buku, Anda akan keluar dari Tahun Buku ini dan tidak dapat mengeditnya lagi." &
                              Enter2Baris & "Yakin melanjutkan 'Tutup Buku Tahun " & TahunBukuAktif & "'?"
        If Not TanyaKonfirmasi(PesanKonfirmasi) Then
            KetersediaanMenuHalaman(pnl_Halaman, True)
            Return
        End If

        ProsesTutupBuku = True
        pesan_TutupBukuGagal = "Proses Tutup Buku GAGAL..!" & Enter2Baris & teks_SilakanCobaLagi_Database
        TahunBukuDitutup = TahunBukuAktif
        TanggalBukuDitutup = "31-12-" & TahunBukuDitutup

        TahunPajak = TahunBukuAktif 'Ini penting untuk ditentukan di awal. Untuk kepentingan Kesesuaian Saldo Awal Semua Jenis PPh.


        win_ProgressLoadingData = New wpfWin_ProgressLoadingData
        win_ProgressLoadingData.lbl_Baris_01.Text = "Harap Tunggu..."
        win_ProgressLoadingData.lbl_Baris_02.Text = "Sistem sedang mengecek kesesuaian data."
        win_ProgressLoadingData.lbl_ProgressReport.Text = "Jangan memutus proses ini..!"
        CekKesesuaianData()

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

        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        If DatabaseTahunBukuBaruSudahAda Or StatusKoneksiDatabaseTransaksi_Alternatif Then
            PesanUntukProgrammer("Database Sudah Ada, dan akan dihapus")
            'Hapus Database
            Dim KoneksiHapusDb As MySqlConnection
            KoneksiHapusDb = New MySqlConnection("Data Source =" & LokasiServerDatabase &
                                            ";port=" & PortDatabase &
                                            ";username=" & UserDatabase &
                                            ";password=" & PasswordDatabase &
                                            ";SSL Mode=None")
            Dim cmdHapusDb As MySqlCommand
            Dim strHapusDb As String
            cmdHapusDb = KoneksiHapusDb.CreateCommand
            strHapusDb = " DROP DATABASE " & AwalanDatabase_BookuID_Booku & ID_Customer & "_" & TahunBukuBaru
            cmdHapusDb.CommandText = strHapusDb
            Try
                KoneksiHapusDb.Open()
                cmdHapusDb.ExecuteNonQuery()
                KoneksiHapusDb.Close()
            Catch ex As Exception
                PesanUntukProgrammer("Penghapusan Database Baru gagal." & Enter2Baris & ex.Message)
                PesanError("Tutup Buku gagal. Silakan ulangi lagi.")
                Return
            End Try
        End If

        BuatDatabaseBaruTransaksi(TahunBukuBaru)
        If HasilPembuatanDatabaseTransaksi = True Then
            Pesan_Sukses("Database Tahun Buku " & TahunBukuBaru & " BERHASIL dibuat.")
        Else
            Pesan_Gagal("Database Tahun Buku " & TahunBukuBaru & " GAGAL dibuat karena ada kesalahan teknis." & Enter2Baris &
                       teks_SilakanCobaLagi_Database & Enter2Baris)
            ProsesTutupBuku = False
            Return
        End If

        'Pulihkan Beberapa Variabel dan Dsn Transaksi setelah Pembuatan Database Baru :
        TahunBukuAktif = TahunBukuAktifAsli
        BuatDsnTransaksi(TahunBukuAktifAsli)
        '(Jika tidak dilakukan pemulihan, berpotensi terjadi kekacauan data)

        'Isi Value Saldo Awal Tahun Buku yang baru, berdasarkan Saldo Akhir Tahun Buku yang akan ditutup :
        TransferSaldoAntarTahunBuku()

        'Hapus semua Hutang PPh :
        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_HutangPajak ", KoneksiDatabaseTransaksi_Alternatif)
        cmdHAPUS.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        Dim NomorIdKK As Int64 = 0
        Dim NomoridKM As Int64 = 0

        Dim ProsesTransferData As Boolean = False

        If ProsesTransferData Then
            TransferData_SisaHutangUsaha_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomorIdKK, pesan_TutupBukuGagal)
            TransferData_SisaPiutangUsaha_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomoridKM, pesan_TutupBukuGagal)
            TransferData_SisaHutangKaryawan_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomorIdKK, pesan_TutupBukuGagal)
            TransferData_SisaPiutangKaryawan_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomoridKM, pesan_TutupBukuGagal)
            TransferData_SisaHutangPemegangSaham_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomorIdKK, pesan_TutupBukuGagal)
            TransferData_SisaPiutangPemegangSaham_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomoridKM, pesan_TutupBukuGagal)
            TransferData_SisaHutangBank_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomorIdKK, pesan_TutupBukuGagal)
            TransferData_SisaHutangLeasing_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomorIdKK, pesan_TutupBukuGagal)
            TransferData_SisaHutangPihakKetiga_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomorIdKK, pesan_TutupBukuGagal)
            TransferData_SisaPiutangPihakKetiga_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomoridKM, pesan_TutupBukuGagal)
            TransferData_SisaHutangAfiliasi_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomorIdKK, pesan_TutupBukuGagal)
            TransferData_SisaPiutangAfiliasi_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomoridKM, pesan_TutupBukuGagal)
            TransferData_SisaDepositOperasional_WPF(TahunBukuDitutup, TanggalBukuDitutup, NomorIdKK, NomoridKM, pesan_TutupBukuGagal)
        End If

        IsiDataNotifikasi()

        CloseTahunBukuAktif()

        KetersediaanMenuHalaman(pnl_Halaman, True)

        ProsesTutupBuku = False

    End Sub


    Sub TransferSaldoAntarTahunBuku()

        Dim KodeAkun_Terindeks
        Dim SaldoAkhir_Terindeks As Int64
        Dim BarisTelusur = 0
        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        For Each row As DataRow In datatabelUtama.Rows
            BarisTelusur += 1
            KodeAkun_Terindeks = AmbilAngka(row("COA_"))
            SaldoAkhir_Terindeks = AmbilAngka(row("Saldo_Akhir"))
            cmd = New OdbcCommand(" UPDATE tbl_SaldoAwalCOA " &
                                  " SET Saldo_Awal = '" & SaldoAkhir_Terindeks & "' " &
                                  " WHERE COA = '" & KodeAkun_Terindeks & "' ", KoneksiDatabaseTransaksi_Alternatif)
            cmd_ExecuteNonQuery()
            If StatusSuntingDatabase = False Then Exit For
            row("Saldo_Awal_Tahun_Berikutnya") = SaldoAkhir_Terindeks
            If BarisTelusur <= 9 Then System.Threading.Thread.Sleep(123) '(Untuk Dramatisasi saja.... Wkwkwk...)
        Next
        TutupDatabaseTransaksi_Alternatif()

        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Data Saldo Akhir Tahun " & TahunBukuAktif & " BERHASIL dikirim sebagai Saldo Awal Tahun " & TahunBukuBaru & ".")
        Else
            Pesan_Gagal("Pengisian Saldo Awal Tahun Buku " & TahunBukuBaru & " GAGAL atau hanya terisi sebagian karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Return
        End If

    End Sub

    Dim pesan_DataTidakSesuai As String
    Sub CekKesesuaianData()

        JumlahBukuYangDicek = 37
        ProgressMaximum = JumlahBukuYangDicek - 1
        ProgressInfo = "Mohon tunggu..! Sistem sedang mengecek kesesuaian Saldo Akhir"
        StartProgress()

        KetersediaanMenuHalaman(pnl_Halaman, False)

        KesesuaianData = True

        pesan_DataTidakSesuai = "Transfer Nilai Saldo dan Tutup Buku bermasalah, karena :"

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then

            'JIKA SUATU SAAT NANTI ADA MASALAH DI SINI, JANGAN KAGET...!!!
            'MUNGKIN PENYEBABNYA : host_XXX belum didefinisikan sebagai New wpfHost_XXX
            'Contoh : host_BukuPengawasanHutangUsaha_NonAfiliasi = New wpfHost_BukuPengawasanHutangUsaha_NonAfiliasi
            host_BukuPengawasanHutangUsaha_NonAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_NonAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangUsaha_Afiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_Afiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangUsaha_Impor_USD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangUsaha_Impor_AUD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangUsaha_Impor_JPY.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangUsaha_Impor_CNY.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangUsaha_Impor_EUR.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangUsaha_Impor_SGD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_Ekspor_USD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_Ekspor_AUD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_Ekspor_JPY.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_Ekspor_CNY.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_Ekspor_EUR.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_Ekspor_SGD.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangPPhPasal21.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangPPhPasal23.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangPPhPasal25.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangPPhPasal26.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangPPhPasal42.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanKetetapanPajak.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangKaryawan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangKaryawan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangPemegangSaham.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangPemegangSaham.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangBPJSKesehatan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangBPJSKetenagakerjaan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangKoperasiKaryawan.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangSerikat.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangBank.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangLeasing.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangPihakKetiga.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangPihakKetiga.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanDepositOperasional.CekKesesuaianData()
            ProgressUp(pgb_Progress)

            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_NonAfiliasi.KesesuaianSaldoAkhir, "Hutang Usaha Non-Afiliasi")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_NonAfiliasi.KesesuaianSaldoAkhir, "Piutang Usaha Non-Afiliasi")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Afiliasi.KesesuaianSaldoAkhir, "Hutang Usaha Afiliasi")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_USD.KesesuaianSaldoAkhir, "Hutang Usaha Impor - USD")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_AUD.KesesuaianSaldoAkhir, "Hutang Usaha Impor - AUD")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_JPY.KesesuaianSaldoAkhir, "Hutang Usaha Impor - JPY")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_CNY.KesesuaianSaldoAkhir, "Hutang Usaha Impor - CNY")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_EUR.KesesuaianSaldoAkhir, "Hutang Usaha Impor - EUR")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_SGD.KesesuaianSaldoAkhir, "Hutang Usaha Impor - SGD")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_USD.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - USD")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_AUD.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - AUD")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - JPY")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_CNY.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - CNY")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - EUR")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_SGD.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - SGD")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Afiliasi.KesesuaianSaldoAkhir, "Piutang Usaha Afiliasi")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAkhir_100, "Hutang PPh Pasal 21 - 100")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAkhir_401, "Hutang PPh Pasal 21 - 401")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_100, "Hutang PPh Pasal 23 - 100")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_101, "Hutang PPh Pasal 23 - 101")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_102, "Hutang PPh Pasal 23 - 102")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_103, "Hutang PPh Pasal 23 - 103")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_104, "Hutang PPh Pasal 23 - 104")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal25.KesesuaianSaldoAkhir, "Hutang PPh Pasal 25")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_100, "Hutang PPh Pasal 26 - 100")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_101, "Hutang PPh Pasal 26 - 101")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_102, "Hutang PPh Pasal 26 - 102")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_103, "Hutang PPh Pasal 26 - 103")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_104, "Hutang PPh Pasal 26 - 104")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_105, "Hutang PPh Pasal 26 - 105")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_402, "Hutang PPh Pasal 42 - 402")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_403, "Hutang PPh Pasal 42 - 403")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_409, "Hutang PPh Pasal 42 - 409")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_419, "Hutang PPh Pasal 42 - 419")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanKetetapanPajak.KesesuaianSaldoAkhir, "Hutang Ketetapan Pajak")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangKaryawan.KesesuaianSaldoAkhir, "Hutang Karyawan")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangKaryawan.KesesuaianSaldoAkhir, "Piutang Karyawan")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPemegangSaham.KesesuaianSaldoAkhir, "Hutang Pemegang Saham")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangPemegangSaham.KesesuaianSaldoAkhir, "Piutang Pemegang Saham")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangBPJSKesehatan.KesesuaianSaldoAkhir, "Hutang BPJS Kesehatan")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangBPJSKetenagakerjaan.KesesuaianSaldoAkhir, "Hutang BPJS Ketenagakerjaan")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangKoperasiKaryawan.KesesuaianSaldoAkhir, "Hutang Koperasi Karyawan")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangSerikat.KesesuaianSaldoAkhir, "Hutang Serikat")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangBank.KesesuaianSaldoAkhir, "Hutang Bank")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangLeasing.KesesuaianSaldoAkhir, "Hutang Leasing")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangPihakKetiga.KesesuaianSaldoAkhir, "Hutang Pihak Ketiga")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangPihakKetiga.KesesuaianSaldoAkhir, "Piutang Pihak Ketiga")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanHutangAfiliasi.KesesuaianSaldoAkhir, "Hutang Afiliasi")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanPiutangAfiliasi.KesesuaianSaldoAkhir, "Piutang Afiliasi")
            CekKesesuaianSaldoAkhir(usc_BukuPengawasanDepositOperasional.KesesuaianSaldoAkhir, "Deposit Operasional")

        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            'Belum Ada Coding
            PesanUntukProgrammer("Coding untuk Mengecek Kesesuaian Saldo Awal belum dibikin." & Enter2Baris &
                                 "Tapi sepertinya tidak diperlukan pengecekan lagi, asalkan Trial Balancenya sudah benar sebelum dilakukan Tutup Buku.")
        End If

        KetersediaanMenuHalaman(pnl_Halaman, True)

        If KesesuaianData = False Then
            Pesan_Peringatan(pesan_DataTidakSesuai & "." & Enter2Baris &
                   "Proses Tutup Buku akan tetap dilanjutkan dengan beberapa catatan yang akan dikirim ke Pembukuan Tahun Berikutnya." &
                   Enter2Baris &
                   "Silakan nanti buka notifikasinya untuk arahan bagi perbaikan.")
        End If

        pnl_Progress.Visibility = Visibility.Collapsed
        Terabas()

    End Sub
    Sub CekKesesuaianSaldoAkhir(KesesuaianSaldoAkhir As Boolean, BukuPengawasan As String)
        If KesesuaianSaldoAkhir = False Then
            KesesuaianData = False
            pesan_DataTidakSesuai &= Enter2Baris & "- Data Saldo Akhir " & BukuPengawasan & " tidak sesuai"
        End If
    End Sub


    Sub IsiDataNotifikasi()

        'Kosongkan Data Notifikasi :
        BukaDatabaseTransaksi_Alternatif(TahunBukuBaru)
        cmd = New OdbcCommand(" DELETE FROM tbl_Notifikasi ", KoneksiDatabaseTransaksi_Alternatif)
        cmd.ExecuteNonQuery()
        TutupDatabaseTransaksi_Alternatif()

        'Pengisian Data Notifikasi :
        TahunPajak = TahunBukuAktif 'Ini penting untuk ditentukan di awal. Untuk kepentingan Kesesuaian Saldo Awal Semua Jenis PPh.
        notif_NomorID = 0
        notif_Waktu = Today
        notif_StatusDibaca = 0
        notif_StatusDieksekusi = 0

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_NonAfiliasi.KesesuaianSaldoAkhir, "Hutang Usaha Non Afiliasi", Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Afiliasi.KesesuaianSaldoAkhir, "Hutang Usaha Afiliasi", Halaman_BUKUPENGAWASANHUTANGUSAHAAFILIASI)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_USD.KesesuaianSaldoAkhir, "Hutang Usaha Impor - USD", Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_USD)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_AUD.KesesuaianSaldoAkhir, "Hutang Usaha Impor - AUD", Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_AUD)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_JPY.KesesuaianSaldoAkhir, "Hutang Usaha Impor - JPY", Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_JPY)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_CNY.KesesuaianSaldoAkhir, "Hutang Usaha Impor - CNY", Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_CNY)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_EUR.KesesuaianSaldoAkhir, "Hutang Usaha Impor - EUR", Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_EUR)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangUsaha_Impor_SGD.KesesuaianSaldoAkhir, "Hutang Usaha Impor - SGD", Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_SGD)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_NonAfiliasi.KesesuaianSaldoAkhir, "Piutang Usaha Non Afiliasi", Halaman_BUKUPENGAWASANPIUTANGUSAHANONAFILIASI)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Afiliasi.KesesuaianSaldoAkhir, "Piutang Usaha Afiliasi", Halaman_BUKUPENGAWASANPIUTANGUSAHAAFILIASI)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_USD.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - USD", Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_USD)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_AUD.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - AUD", Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_AUD)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - JPY", Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_JPY)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_CNY.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - CNY", Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_CNY)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - EUR", Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_EUR)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangUsaha_Ekspor_SGD.KesesuaianSaldoAkhir, "Piutang Usaha Ekspor - SGD", Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_SGD)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAkhir_100, "Hutang PPh Pasal 21 - 100", Halaman_BUKUPENGAWASANHUTANGPPHPASAL21)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAkhir_401, "Hutang PPh Pasal 21 - 401", Halaman_BUKUPENGAWASANHUTANGPPHPASAL21)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_100, "Hutang PPh Pasal 23 - 100", Halaman_BUKUPENGAWASANHUTANGPPHPASAL23)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_101, "Hutang PPh Pasal 23 - 101", Halaman_BUKUPENGAWASANHUTANGPPHPASAL23)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_102, "Hutang PPh Pasal 23 - 102", Halaman_BUKUPENGAWASANHUTANGPPHPASAL23)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_103, "Hutang PPh Pasal 23 - 103", Halaman_BUKUPENGAWASANHUTANGPPHPASAL23)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAkhir_104, "Hutang PPh Pasal 23 - 104", Halaman_BUKUPENGAWASANHUTANGPPHPASAL23)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal25.KesesuaianSaldoAkhir, "Hutang PPh Pasal 25", Halaman_BUKUPENGAWASANHUTANGPPHPASAL25)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_100, "Hutang PPh Pasal 26 - 100", Halaman_BUKUPENGAWASANHUTANGPPHPASAL26)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_101, "Hutang PPh Pasal 26 - 101", Halaman_BUKUPENGAWASANHUTANGPPHPASAL26)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_102, "Hutang PPh Pasal 26 - 102", Halaman_BUKUPENGAWASANHUTANGPPHPASAL26)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_103, "Hutang PPh Pasal 26 - 103", Halaman_BUKUPENGAWASANHUTANGPPHPASAL26)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_104, "Hutang PPh Pasal 26 - 104", Halaman_BUKUPENGAWASANHUTANGPPHPASAL26)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAkhir_105, "Hutang PPh Pasal 26 - 105", Halaman_BUKUPENGAWASANHUTANGPPHPASAL26)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_402, "Hutang PPh Pasal 42 - 402", Halaman_BUKUPENGAWASANHUTANGPPHPASAL42)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_403, "Hutang PPh Pasal 42 - 403", Halaman_BUKUPENGAWASANHUTANGPPHPASAL42)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_409, "Hutang PPh Pasal 42 - 409", Halaman_BUKUPENGAWASANHUTANGPPHPASAL42)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAkhir_419, "Hutang PPh Pasal 42 - 419", Halaman_BUKUPENGAWASANHUTANGPPHPASAL42)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanKetetapanPajak.KesesuaianSaldoAkhir, "Hutang Ketetapan Pajak", Halaman_BUKUPENGAWASANKETETAPANPAJAK)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangKaryawan.KesesuaianSaldoAkhir, "Hutang Karyawan", Halaman_BUKUPENGAWASANHUTANGKARYAWAN)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangKaryawan.KesesuaianSaldoAkhir, "Piutang Karyawan", Halaman_BUKUPENGAWASANPIUTANGKARYAWAN)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPemegangSaham.KesesuaianSaldoAkhir, "Hutang Pemegang Saham", Halaman_BUKUPENGAWASANHUTANGPEMEGANGSAHAM)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangPemegangSaham.KesesuaianSaldoAkhir, "Piutang Pemegang Saham", Halaman_BUKUPENGAWASANPIUTANGPEMEGANGSAHAM)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangBPJSKesehatan.KesesuaianSaldoAkhir, "Hutang BPJS Kesehatan", Halaman_BUKUPENGAWASANHUTANGBPJSKESEHATAN)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangBPJSKetenagakerjaan.KesesuaianSaldoAkhir, "Hutang BPJS Ketenagakerjaan", Halaman_BUKUPENGAWASANHUTANGBPJSKETENAGAKERJAAN)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangKoperasiKaryawan.KesesuaianSaldoAkhir, "Hutang Koperasi Karyawan", Halaman_BUKUPENGAWASANHUTANGKOPERASIKARYAWAN)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangSerikat.KesesuaianSaldoAkhir, "Hutang Serikat", Halaman_BUKUPENGAWASANHUTANGSERIKAT)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangBank.KesesuaianSaldoAkhir, "Hutang Bank", Halaman_BUKUPENGAWASANHUTANGBANK)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangLeasing.KesesuaianSaldoAkhir, "Hutang Leasing", Halaman_BUKUPENGAWASANHUTANGLEASING)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangPihakKetiga.KesesuaianSaldoAkhir, "Hutang Pihak Ketiga", Halaman_BUKUPENGAWASANHUTANGPIHAKKETIGA)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangPihakKetiga.KesesuaianSaldoAkhir, "Piutang Pihak Ketiga", Halaman_BUKUPENGAWASANPIUTANGPIHAKKETIGA)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanHutangAfiliasi.KesesuaianSaldoAkhir, "Hutang Afiliasi", Halaman_BUKUPENGAWASANHUTANGAFILIASI)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanPiutangAfiliasi.KesesuaianSaldoAkhir, "Piutang Afiliasi", Halaman_BUKUPENGAWASANPIUTANGAFILIASI)
            NotifikasiKetidaksesuaianSaldoAkhir(usc_BukuPengawasanDepositOperasional.KesesuaianSaldoAkhir, "Deposit Operasional", Halaman_BUKUPENGAWASANDEPOSITOPERASIONAL)
        End If

    End Sub

    Dim teks_SilakanDiperbaiki As String = "Silakan perbaiki!"
    Sub NotifikasiKetidaksesuaianSaldoAkhir(KesesuaianSaldoAkhir As Boolean, BukuPengawasan As String, HalamanTarget As String)
        If KesesuaianSaldoAkhir = False Then
            notif_NomorID += 1
            notif_Jenis = JenisNotifikasi_PerintahEksekusi
            notif_Notifikasi = "Ada selisih pada Saldo Awal " & BukuPengawasan & "." & Enter1Baris & teks_SilakanDiperbaiki
            notif_HalamanTarget = HalamanTarget
            notif_Pesan = teks_SilakanSesuaikanSaldo
            SimpanNotifikasi()
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
            Pesan_Sukses("Proses Tutup Buku Tahun " & TahunBukuYangAkanDitutup & " BERHASIL.")
        Else
            Pesan_Gagal("Proses Tutup Buku GAGAL karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            ProsesTutupBuku = False
            Return
        End If

        'Masuk ke Tahun Buku Baru
        win_GantiTahunBuku = New wpfWin_GantiTahunBuku
        win_GantiTahunBuku.FungsiForm = FungsiForm_GANTITAHUNBUKU
        win_GantiTahunBuku.ProsesGantiTahunBuku()

    End Sub




    'Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim COA_ As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Saldo_Awal As New DataGridTextColumn
    Dim Debet_ As New DataGridTextColumn
    Dim Kredit_ As New DataGridTextColumn
    Dim Saldo_Akhir As New DataGridTextColumn
    Dim Saldo_Awal_Tahun_Berikutnya As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("COA_")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Saldo_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Debet_", GetType(Int64))
        datatabelUtama.Columns.Add("Kredit_", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Akhir", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Awal_Tahun_Berikutnya", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_, "COA_", "COA", 72, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 270, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal, "Saldo_Awal", "Saldo Awal", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Debet_, "Debet_", "Debet", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kredit_, "Kredit_", "Kredit", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Akhir, "Saldo_Akhir", "Saldo Akhir", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal_Tahun_Berikutnya, "Saldo_Awal_Tahun_Berikutnya", "Saldo Awal" & Enter1Baris & "Tahun Berikutnya", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
    End Sub




    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
    End Sub

    Sub StartProgress()
        pgb_Progress.Foreground = clrPrimary
        pnl_Progress.Visibility = Visibility.Visible
        pgb_Progress.Minimum = 0
        pgb_Progress.Maximum = ProgressMaximum
        pgb_Progress.Value = 0
        lbl_ProgressInfo.Text = ProgressInfo
    End Sub

    Sub datagridUtama_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
        KetentuanUkuran()
    End Sub
    Sub pnl_Konten_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles pnl_Konten.SizeChanged
        KetentuanUkuran()
    End Sub
    Dim LebarKonten As Integer
    Dim TinggiKonten As Integer
    Sub KetentuanUkuran()
        LebarKonten = pnl_Konten.ActualWidth
        TinggiKonten = pnl_Konten.ActualHeight
        datagridUtama.MaxHeight = TinggiKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class
