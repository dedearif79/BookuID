Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfUsc_LaporanNeracaLajur

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Public JalurMasuk

    Dim QueryTampilan

    Dim ProsesDone As Boolean
    Dim LoopingTrialBalance As Boolean
    Dim JedaPerBarisCOA = 0 'milidetik
    Dim JedaPerKelompokCOA = 3 'milidetik
    Dim JumlahBukuYangDicek As Integer

    Dim KesesuaianData_TrialBalance As Boolean

    Dim BulanLaporan_String
    Dim BulanLaporan_Angka

    Dim JenisLaporan
    Dim JenisLaporan_HPP = "HPP"
    Dim JenisLaporan_LabaRugi = "Laba/Rugi"
    Dim JenisLaporan_Neraca = "Neraca"

    Dim ProsesTutupBukuBulan As Boolean
    Dim StatusBulan As String
    Dim StatusBulan_Belum As String = "Belum"
    Dim StatusBulan_Terbuka As String = "Terbuka"
    Dim StatusBulan_Tertutup As String = "Tertutup"


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        If LevelUserAktif < LevelUser_99_AppDeveloper Then btn_ResetSaldoSaja.Visibility = Visibility.Collapsed

        btn_Trialbalance.IsEnabled = False
        btn_Adjusment.IsEnabled = False
        btn_TutupBuku.IsEnabled = False
        KetersediaanTombolReset(False)
        btn_Export.IsEnabled = False

        KontenCombo_BulanLaporan()
        Visibilitas_JenisLaporan(False)
        QueryTampilan = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' "

        'lbl_JudulForm.Text = frm_LaporanNeracaLajur.JudulForm

        pnl_Progress.Visibility = Visibility.Collapsed

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub KontenCombo_BulanLaporan()
        cmb_BulanLaporan.Items.Clear()
        cmb_BulanLaporan.Items.Add(Bulan_Januari)
        cmb_BulanLaporan.Items.Add(Bulan_Februari)
        cmb_BulanLaporan.Items.Add(Bulan_Maret)
        cmb_BulanLaporan.Items.Add(Bulan_April)
        cmb_BulanLaporan.Items.Add(Bulan_Mei)
        cmb_BulanLaporan.Items.Add(Bulan_Juni)
        cmb_BulanLaporan.Items.Add(Bulan_Juli)
        cmb_BulanLaporan.Items.Add(Bulan_Agustus)
        cmb_BulanLaporan.Items.Add(Bulan_September)
        cmb_BulanLaporan.Items.Add(Bulan_Oktober)
        cmb_BulanLaporan.Items.Add(Bulan_Nopember)
        cmb_BulanLaporan.Items.Add(Bulan_Desember)
        cmb_BulanLaporan.Text = Kosongan
    End Sub


    Sub KontenCombo_JenisLaporan()
        cmb_JenisLaporan.Items.Clear()
        cmb_JenisLaporan.Items.Add(JenisLaporan_HPP)
        cmb_JenisLaporan.Items.Add(JenisLaporan_LabaRugi)
        cmb_JenisLaporan.Items.Add(JenisLaporan_Neraca)
        cmb_JenisLaporan.Text = Kosongan
    End Sub


    Sub Visibilitas_JenisLaporan(Visibilitas As Boolean)
        If Visibilitas Then
            KontenCombo_JenisLaporan()
            lbl_JenisLaporan.Visibility = Visibility.Visible
            cmb_JenisLaporan.Visibility = Visibility.Visible
        Else
            lbl_JenisLaporan.Visibility = Visibility.Collapsed
            cmb_JenisLaporan.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KetersediaanTombolReset(Tersedia As Boolean)
        If Tersedia Then
            If BulanTerakhirDitutup = BulanLaporan_Angka Then
                btn_Reset.IsEnabled = True
                btn_ResetSaldoSaja.IsEnabled = True
            End If
        Else
            btn_Reset.IsEnabled = False
            btn_ResetSaldoSaja.IsEnabled = False
        End If
    End Sub


    Sub btn_TrialBalance_Click(sender As Object, e As RoutedEventArgs) Handles btn_Trialbalance.Click

        'CEK KESESUAIAN DATA :
        KesesuaianData_TrialBalance = True
        TahunPajak = TahunBukuAktif 'Ini penting untuk ditentukan di awal. Untuk kepentingan Kesesuaian Saldo Awal Semua Jenis PPh.

        'CekKesesuaianData()
        pnl_Progress.Visibility = Visibility.Collapsed
        Terabas()

        If KesesuaianData_TrialBalance Then
            ProsesTutupBukuBulan = False
            RefreshTampilanData()
        End If

    End Sub


    Private Sub btn_TutupBuku_Click(sender As Object, e As RoutedEventArgs) Handles btn_TutupBuku.Click
        If Total_NeracaDebet <> Total_NeracaKredit Then
            PesanPeringatan("Neraca belum seimbang. Tutup Buku tidak dapat diproses." & Enter2Baris &
                            "Tips : " & Enter1Baris &
                            "- Pastikan visibilitas COA sudah sesuai." & Enter1Baris &
                            "- Pastikan adjusment akhir bulan sudah selesai semua, meliputi :" & Enter1Baris &
                            "   - Adjusment Penyusutan Asset" & Enter1Baris &
                            "   - Adjusment Amortisasi Biaya" & Enter1Baris &
                            "   - Adjusment Mata Uang Asing" & Enter1Baris &
                            "   - Adjusment HPP" & Enter1Baris &
                            Kosongan)
            Return
        End If
        Dim Pesan As String = "Yakin akan melanjutkan proses Tutup Buku?"
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        ProsesTutupBukuBulan = True
        RefreshTampilanData()
        ProsesTutupBukuBulan = False
        If ProsesDone Then
            UpdateInfoBulanBukuAktif()
            'btn_Trialbalance.IsEnabled = False
            btn_Adjusment.IsEnabled = False
            btn_TutupBuku.IsEnabled = False
            KetersediaanTombolReset(True)
            pnl_DataGridUtama.Visibility = Visibility.Collapsed
            PesanSukses("Tutup Buku BERHASIL.")
            Visibilitas_JenisLaporan(True)
        End If
    End Sub


    Private Sub btn_Adjusment_Click(sender As Object, e As RoutedEventArgs) Handles btn_Adjusment.Click
        win_PilihJurnalAdjusment = New wpfWin_PilihJurnalAdjusment
        win_PilihJurnalAdjusment.ResetForm()
        win_PilihJurnalAdjusment.Opacity = 33
        win_PilihJurnalAdjusment.ShowDialog()
        If win_PilihJurnalAdjusment.AdjusmentBulanBukuAktifSudahLengkap Then
            btn_TrialBalance_Click(sender, e)
            btn_TutupBuku.IsEnabled = True
        End If
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub
    Private Sub btn_Reset_Click(sender As Object, e As RoutedEventArgs) Handles btn_Reset.Click
        Dim Pesan As String = "Yakin akan me-reset Laporan Bulan " & BulanLaporan_String & "?"
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        ResetAdjusmentAkhirBulan(BulanLaporan_Angka)
        ResetLaporan()
    End Sub
    Private Sub btn_ResetSaldoSaja_Click(sender As Object, e As RoutedEventArgs) Handles btn_ResetSaldoSaja.Click
        Dim Pesan As String = "Yakin akan me-reset Laporan Bulan " & BulanLaporan_String & "?"
        Pilihan = MessageBox.Show(Pesan, "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        ResetLaporan()
    End Sub
    Sub ResetLaporan()
        pnl_DataGridUtama.Visibility = Visibility.Collapsed
        KetersediaanMenuHalaman(pnl_Halaman, False)
        AksesDatabase_General(Buka)
        Try
            cmd = New OdbcCommand(" UPDATE tbl_COA SET " &
                                  " Debet_" & BulanLaporan_String & " = 0, " &
                                  " Kredit_" & BulanLaporan_String & " = 0, " &
                                  " Saldo_" & BulanLaporan_String & " = 0", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            StatusSuntingDatabase = True
        Catch ex As Exception
            StatusSuntingDatabase = False
        End Try
        AksesDatabase_General(Tutup)
        If StatusSuntingDatabase Then
            BulanBukuAktif -= 1
            btn_Trialbalance.IsEnabled = True
            StatusBulan = StatusBulan_Terbuka
            btn_Adjusment.IsEnabled = False
            btn_TutupBuku.IsEnabled = False
            KetersediaanTombolReset(False)
            Visibilitas_JenisLaporan(False)
        Else
            KetersediaanTombolReset(True)
        End If
        UpdateInfoBulanBukuAktif()
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub
    Sub ResetAdjusmentAkhirBulan(BulanAngka As Integer)
        HapusJurnalAdjusmentBulanTertentu(JenisJurnal_Penyusutan, BulanAngka)
        HapusJurnalAdjusmentBulanTertentu(JenisJurnal_Amortisasi, BulanAngka)
        HapusJurnalAdjusmentBulanTertentu(JenisJurnal_AdjusmentHPP, BulanAngka)
        HapusJurnalAdjusmentBulanTertentu(JenisJurnal_AdjusmentForex, BulanAngka)
    End Sub


    Private Sub cmb_BulanLaporan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_BulanLaporan.SelectionChanged
        BulanLaporan_String = cmb_BulanLaporan.SelectedValue
        btn_Trialbalance.IsEnabled = False
        btn_Adjusment.IsEnabled = False
        KetersediaanTombolReset(False)
        pnl_DataGridUtama.Visibility = Visibility.Collapsed
        btn_Export.IsEnabled = False
        Visibilitas_JenisLaporan(False)
        If BulanLaporan_String = Kosongan Then
            BulanLaporan_Angka = 0
        Else
            BulanLaporan_Angka = KonversiBulanKeAngka(BulanLaporan_String)
            CekStatusBulan()
            Select Case StatusBulan
                Case StatusBulan_Belum
                    btn_Trialbalance.IsEnabled = False
                Case StatusBulan_Terbuka
                    btn_Trialbalance.IsEnabled = True
                Case StatusBulan_Tertutup
                    'btn_Trialbalance.IsEnabled = False
                    btn_Trialbalance.IsEnabled = True
                    KetersediaanTombolReset(True)
                    Visibilitas_JenisLaporan(True)
            End Select
        End If
        btn_Adjusment.IsEnabled = False 'Ini sudah benar paling bawah...!
        btn_TutupBuku.IsEnabled = False 'Ini sudah benar paling bawah...!
    End Sub


    Private Sub cmb_JenisLaporan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisLaporan.SelectionChanged
        JenisLaporan = cmb_JenisLaporan.SelectedValue
        If JenisLaporan <> Kosongan Then
            Select Case JenisLaporan
                Case JenisLaporan_HPP
                    win_BOOKU.BukaHalaman_LaporanHPP()
                Case JenisLaporan_LabaRugi
                    win_BOOKU.BukaHalaman_LaporanLabaRugi_Bulanan()
                Case JenisLaporan_Neraca
                    win_BOOKU.BukaHalaman_LaporanNeraca_Bulanan()
            End Select
            'TampilkanLaporan()
        End If
    End Sub


    Sub CekStatusBulan()
        Dim TotalSaldo As Decimal = 0
        CekSaldoAkhirBulan(BulanLaporan_Angka, TotalSaldo)
        If TotalSaldo = 0 Then
            If BulanLaporan_Angka = 1 Then
                StatusBulan = StatusBulan_Terbuka
            Else
                CekSaldoAkhirBulan(BulanLaporan_Angka - 1, TotalSaldo)
                If TotalSaldo = 0 Then
                    StatusBulan = StatusBulan_Belum
                Else
                    StatusBulan = StatusBulan_Terbuka
                End If
            End If
        Else
            StatusBulan = StatusBulan_Tertutup
        End If
    End Sub
    Sub CekSaldoAkhirBulan(ByVal BulanAngka As Integer, ByRef TotalSaldo As Decimal)
        Dim BulanString As String = KonversiAngkaKeBulanString(BulanAngka)
        TotalSaldo = 0
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT Saldo_" & BulanString & " FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Dim Saldo As Decimal
        Do While dr.Read
            Saldo = dr.Item("Saldo_" & BulanString)
            TotalSaldo += Saldo
        Loop
        AksesDatabase_General(Tutup)
    End Sub



    Dim pesan_DataTidakSesuai As String
    Sub CekKesesuaianData()

        pnl_DataGridUtama.Visibility = Visibility.Collapsed

        JumlahBukuYangDicek = 37 'Angka ini bisa berubah-ubah, tergantung berapa jumlah Data yang harus dicek (disesuaikan).
        ProgressMaximum = JumlahBukuYangDicek - 1
        ProgressInfo = "Mohon tunggu..! Sistem sedang mengecek kesesuaian Saldo Awal"
        StartProgress()

        If JenisTahunBuku <> JenisTahunBuku_LAMPAU Then

            pesan_DataTidakSesuai = "Proses 'Trial Balance' mengalami kendala, karena :"


            '1. Cek Kesesuaian Jurnal Pada Data Amortisasi Biaya :
            host_DaftarAmortisasiBiaya.CekKesesuaianData()
            If usc_DaftarAmortisasiBiaya.KesesuaianJurnal = False Then
                KesesuaianData_TrialBalance = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Ada Data Amortisasi Biaya yang belum didorong ke Jurnal"
            End If
            ProgressUp(pgb_Progress)

            '2. Cek Kesesuaian Jurnal Pada Data Penyusutan Asset Tetap :
            usc_DaftarPenyusutanAssetTetap.ResetFilter()
            usc_DaftarPenyusutanAssetTetap.TampilkanData_Detail_Rekap() '(Untuk menampilkan Jurnal)
            If usc_DaftarPenyusutanAssetTetap.KesesuaianJurnal = False Then
                KesesuaianData_TrialBalance = False
                pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Ada Data Penyusutan Asset Tetap yang belum didorong ke Jurnal"
            End If
            ProgressUp(pgb_Progress)

            host_BukuPengawasanHutangUsaha_NonAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanHutangUsaha_Afiliasi.CekKesesuaianData()
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
            host_BukuPengawasanPiutangUsaha_NonAfiliasi.CekKesesuaianData()
            ProgressUp(pgb_Progress)
            host_BukuPengawasanPiutangUsaha_Afiliasi.CekKesesuaianData()
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

            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_NonAfiliasi.KesesuaianSaldoAwal, "Hutang Usaha Non-Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Afiliasi.KesesuaianSaldoAwal, "Hutang Usaha Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_USD.KesesuaianSaldoAwal, "Hutang Usaha (USD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_AUD.KesesuaianSaldoAwal, "Hutang Usaha (AUD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_JPY.KesesuaianSaldoAwal, "Hutang Usaha (JPY)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_CNY.KesesuaianSaldoAwal, "Hutang Usaha (CNY)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_EUR.KesesuaianSaldoAwal, "Hutang Usaha (EUR)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangUsaha_Impor_SGD.KesesuaianSaldoAwal, "Hutang Usaha (SGD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_NonAfiliasi.KesesuaianSaldoAwal, "Piutang Usaha Non-Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Afiliasi.KesesuaianSaldoAwal, "Piutang Usaha Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_USD.KesesuaianSaldoAwal, "Piutang Usaha (USD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_AUD.KesesuaianSaldoAwal, "Piutang Usaha (AUD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_JPY.KesesuaianSaldoAwal, "Piutang Usaha (JPY)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_CNY.KesesuaianSaldoAwal, "Piutang Usaha (CNY)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_EUR.KesesuaianSaldoAwal, "Piutang Usaha (EUR)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangUsaha_Ekspor_SGD.KesesuaianSaldoAwal, "Piutang Usaha (SGD)")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAwal_100, "Hutang PPh Pasal 21 - 100")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal21.KesesuaianSaldoAwal_401, "Hutang PPh Pasal 21 - 401")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_100, "Hutang PPh Pasal 23 - 100")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_101, "Hutang PPh Pasal 23 - 101")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_102, "Hutang PPh Pasal 23 - 102")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_103, "Hutang PPh Pasal 23 - 103")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal23.KesesuaianSaldoAwal_104, "Hutang PPh Pasal 23 - 104")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal25.KesesuaianSaldoAwal, "Hutang PPh Pasal 25")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_100, "Hutang PPh Pasal 26 - 100")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_101, "Hutang PPh Pasal 26 - 101")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_102, "Hutang PPh Pasal 26 - 102")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_103, "Hutang PPh Pasal 26 - 103")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_104, "Hutang PPh Pasal 26 - 104")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal26.KesesuaianSaldoAwal_105, "Hutang PPh Pasal 26 - 105")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_402, "Hutang PPh Pasal 42 - 402")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_403, "Hutang PPh Pasal 42 - 403")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_409, "Hutang PPh Pasal 42 - 409")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPPhPasal42.KesesuaianSaldoAwal_419, "Hutang PPh Pasal 42 - 419")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangKaryawan.KesesuaianSaldoAwal, "Hutang Karyawan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangKaryawan.KesesuaianSaldoAwal, "Piutang Karyawan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPemegangSaham.KesesuaianSaldoAwal, "Hutang Pemegang Saham")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangPemegangSaham.KesesuaianSaldoAwal, "Piutang Pemegang Saham")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangBPJSKesehatan.KesesuaianSaldoAwal, "Hutang BPJS Kesehatan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangBPJSKetenagakerjaan.KesesuaianSaldoAwal, "Hutang BPJS Ketenagakerjaan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangKoperasiKaryawan.KesesuaianSaldoAwal, "Hutang Koperasi Karyawan")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangSerikat.KesesuaianSaldoAwal, "Hutang Serikat")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangBank.KesesuaianSaldoAwal, "Hutang Bank")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangLeasing.KesesuaianSaldoAwal, "Hutang Leasing")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangPihakKetiga.KesesuaianSaldoAwal, "Hutang Pihak Ketiga")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangPihakKetiga.KesesuaianSaldoAwal, "Piutang Pihak Ketiga")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanHutangAfiliasi.KesesuaianSaldoAwal, "Hutang Afiliasi")
            CekKesesuaianSaldoAwal(usc_BukuPengawasanPiutangAfiliasi.KesesuaianSaldoAwal, "Piutang Afiliasi")

            '=========================================== SETELAH PENGECEKAN : ===========================================
            KetersediaanMenuHalaman(pnl_Halaman, True) 'Jaga-jaga ada proses yang tidak selesai. Ketersediaan UI harus dipulihkan
            If KesesuaianData_TrialBalance = False Then
                pgb_Progress.Foreground = WarnaPeringatan_WPF
                pesan_DataTidakSesuai &= "." & Enter2Baris &
                    "Silakan perbaiki dan sesuaikan semua data tersebut agar proses 'Trial Balance' berjalan dengan baik." & Enter2Baris &
                    "Atau silakan klik tombol 'Yes' untuk melanjutkan 'Trial Balance'."
                If JalurMasuk <> Halaman_MENUUTAMA Then TutupHalaman()
                Pilihan = MessageBox.Show(pesan_DataTidakSesuai, "Perhatian..!", MessageBoxButtons.YesNo)
                If Pilihan = vbNo Then Return
                If Pilihan = vbYes Then
                    KetersediaanMenuHalaman(pnl_Halaman, False)
                    KesesuaianData_TrialBalance = True
                End If
            End If
            '============================================================================================================
        End If

    End Sub
    Sub CekKesesuaianSaldoAwal(KesesuaianSaldoAwal As Boolean, NamaBP As String)
        If KesesuaianSaldoAwal = False Then
            KesesuaianData_TrialBalance = False
            pesan_DataTidakSesuai = pesan_DataTidakSesuai & Enter2Baris &
                    "- Data Saldo Awal " & NamaBP & " tidak sesuai"
        End If
    End Sub


    Sub RefreshTampilanData()

        Proses = True
        ProsesDone = False
        LoopingTrialBalance = True
        'TrialBalance_Mentahkan() 'Mentahkan dulu data, karena khawatir proses berhenti di tengah jalan.
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
        ProgressInfo = "Mohon tunggu..! Sistem sedang merekap seluruh COA."
        'PesanUntukProgrammer("Jumlah COA : " & JumlahBarisCOA)
        StartProgress()
        TrialBalance()
        If Proses = False Then ProsesDone = False
        If ProsesDone = True Then
            TrialBalance_Matangkan()
            If ProsesTutupBukuBulan Then StatusBulan = StatusBulan_Tertutup
            If StatusBulan = StatusBulan_Terbuka Then PesanSukses("Trial Balance BERHASIL.")
            pnl_Progress.Visibility = Visibility.Collapsed
        Else
            LoopingTrialBalance = False 'Keluar dari looping dan BackgroundWorker
            datatabelUtama.Rows.Clear()
            MsgBox("Trial Balance GAGAL." & Enter2Baris & teks_SilakanCobaLagi_Database)
            datatabelUtama.Rows.Clear() 'Kenapa coding ini harus dua kali..? Karena kita bekerja di BackgroundWorker. Jadi, yang terakhir ini untuk menyapu sisa-sisa baris yang masih ada. Intinya : BARIS INI JANGAN DIHAPUS...!!!
            If JalurMasuk <> Halaman_MENUUTAMA Then TutupHalaman()
        End If
        Proses = False

    End Sub


    Sub BersihkanSeleksi()
        ProsesDone = True
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Export.IsEnabled = True
        If StatusBulan = StatusBulan_Terbuka Then
            btn_Adjusment.IsEnabled = True
        End If
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub


    Sub TampilkanLaporan()

        KetersediaanMenuHalaman(pnl_Halaman, False)

        pnl_DataGridUtama.Visibility = Visibility.Visible

        Mutasi_Debet.Visibility = Visibility.Collapsed
        Mutasi_Kredit.Visibility = Visibility.Collapsed
        Neraca_Saldo_Debet.Visibility = Visibility.Collapsed
        Neraca_Saldo_Kredit.Visibility = Visibility.Collapsed
        Adjusment_Debet.Visibility = Visibility.Collapsed
        Adjusment_Kredit.Visibility = Visibility.Collapsed
        NSSD_Debet.Visibility = Visibility.Collapsed
        NSSD_Kredit.Visibility = Visibility.Collapsed
        Laporan_Rugi_Laba_Debet.Visibility = Visibility.Collapsed
        Laporan_Rugi_Laba_Kredit.Visibility = Visibility.Collapsed
        Neraca_Debet.Visibility = Visibility.Collapsed
        Neraca_Kredit.Visibility = Visibility.Collapsed
        Select Case JenisLaporan
            Case JenisLaporan_Neraca
                Neraca_Debet.Visibility = Visibility.Visible
                Neraca_Kredit.Visibility = Visibility.Visible
            Case JenisLaporan_LabaRugi
                Saldo_Awal.Visibility = Visibility.Collapsed
                Laporan_Rugi_Laba_Debet.Visibility = Visibility.Visible
                Laporan_Rugi_Laba_Kredit.Visibility = Visibility.Visible
        End Select
        AksesDatabase_General(Buka)
        AksesDatabase_Transaksi(Buka)
        Try
            cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            Proses = True
        Catch ex As Exception
            Proses = False
            Return
        End Try

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Data Tabel
        Dim DebetKreditCOA
        Dim KodeMataUang As String
        Dim KursTengahBI_SaldoAwal As Decimal

        ResetTotal()

        Do While dr.Read
            Try
                COA = dr.Item("COA")
                NamaAkun = dr.Item("Nama_Akun")
                DebetKreditCOA = dr.Item("D_K")
                KodeMataUang = dr.Item("Kode_Mata_Uang")
                If BulanLaporan_Angka = 1 Then
                    KursTengahBI_SaldoAwal = KursTengahBI_AkhirTahunLalu(KodeMataUang)
                    SaldoAwalBulan_IDR = AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_SaldoAwal, dr.Item("Saldo_Awal"))
                    SaldoAwalBulan_MUA = dr.Item("Saldo_Awal")
                Else
                    KursTengahBI_SaldoAwal = KursTengahBI_AkhirBulan(KodeMataUang, BulanLaporan_Angka - 1)
                    SaldoAwalBulan_IDR = AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_SaldoAwal, dr.Item("Saldo_" & KonversiAngkaKeBulanString(BulanLaporan_Angka - 1)))
                    SaldoAwalBulan_MUA = dr.Item("Saldo_" & KonversiAngkaKeBulanString(BulanLaporan_Angka - 1))
                End If
                If COA = KodeTautanCOA_LabaDitahan Then PerhitunganSaldoAwalBulan_LabaDitahan()
                MutasiPerBulan(BulanLaporan_Angka, COA, KodeMataUang, DebetKreditCOA,
                               SaldoAwalBulan_MUA, MutasiDebet_MUA, MutasiKredit_MUA, SaldoAkhirBulan_MUA,
                               SaldoAwalBulan_IDR, MutasiDebet_IDR, MutasiKredit_IDR, SaldoAkhirBulan_IDR)
                If COA = KodeTautanCOA_LabaTahunBerjalan Or COATermasukLabaRugi(COA) Then
                    SaldoAwalBulan_MUA = 0
                    SaldoAwalBulan_IDR = 0
                End If
                If COA = KodeTautanCOA_LabaTahunBerjalan Then
                    MutasiDebet_IDR = dr.Item("Debet_" & BulanLaporan_String)
                    MutasiKredit_IDR = dr.Item("Kredit_" & BulanLaporan_String)
                End If
                If DebetKreditCOA = dk_DEBET_ Then
                    NeracaSaldoDebet = SaldoAwalBulan_IDR + MutasiDebet_IDR - MutasiKredit_IDR
                    NeracaSaldoKredit = 0
                Else
                    NeracaSaldoDebet = 0
                    NeracaSaldoKredit = SaldoAwalBulan_IDR + MutasiKredit_IDR - MutasiDebet_IDR
                End If
                AdjusmentPerBulan(BulanLaporan_Angka, COA, NamaAkun, DebetKreditCOA, AdjusmentDebet, AdjusmentKredit)
                If DebetKreditCOA = dk_DEBET_ Then
                    NSSDDebet = NeracaSaldoDebet + AdjusmentDebet - AdjusmentKredit
                    NSSDKredit = 0
                    SaldoAkhirBulan_SDS = NSSDDebet
                Else
                    NSSDDebet = 0
                    NSSDKredit = NeracaSaldoKredit + AdjusmentKredit - AdjusmentDebet
                    SaldoAkhirBulan_SDS = NSSDKredit
                End If
                If COATermasukLabaRugi(COA) Then
                    LaporanRugiLabaDebet = NSSDDebet
                    LaporanRugiLabaKredit = NSSDKredit
                    NeracaDebet = 0
                    NeracaKredit = 0
                End If
                If COATermasukNeraca(COA) Then
                    NeracaDebet = NSSDDebet
                    NeracaKredit = NSSDKredit
                    LaporanRugiLabaDebet = 0
                    LaporanRugiLabaKredit = 0
                End If
                TambahBaris()
                ProgressUp(pgb_Progress)
                Proses = True
            Catch ex As Exception
                PesanUntukProgrammer("Kesalahan Query atau Coding")
                Proses = False
                Exit Do
            End Try
            OperasiTotal()
        Loop
        AksesDatabase_Transaksi(Tutup)
        AksesDatabase_General(Tutup)
        'Total : 
        datatabelUtama.Rows.Add(
                    "T O T A L", Kosongan, 0,
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0,
                    Total_LaporanRugiLabaDebet, Total_LaporanRugiLabaKredit,
                    Total_NeracaDebet, Total_NeracaKredit)
        BersihkanSeleksi()
    End Sub


    Sub ResetTotal()
        Total_MutasiDebet_IDR = 0
        Total_MutasiKredit_IDR = 0
        Total_MutasiDebet_SDS = 0
        Total_MutasiKredit_SDS = 0
        Total_SaldoAkhirBulan_SDS = 0
        Total_NeracaSaldoDebet = 0
        Total_NeracaSaldoKredit = 0
        Total_AdjusmentDebet = 0
        Total_AdjusmentKredit = 0
        Total_NSSDDebet = 0
        Total_NSSDKredit = 0
        Total_LaporanRugiLabaDebet = 0
        Total_LaporanRugiLabaKredit = 0
        Total_NeracaDebet = 0
        Total_NeracaKredit = 0
    End Sub

    Sub OperasiTotal()
        Total_MutasiDebet_IDR += MutasiDebet_IDR
        Total_MutasiKredit_IDR += MutasiKredit_IDR
        Total_MutasiDebet_SDS += MutasiDebet_SDS
        Total_MutasiKredit_SDS += MutasiKredit_SDS
        Total_SaldoAkhirBulan_SDS += SaldoAkhirBulan_SDS
        Total_NeracaSaldoDebet += NeracaSaldoDebet
        Total_NeracaSaldoKredit += NeracaSaldoKredit
        Total_AdjusmentDebet += AdjusmentDebet
        Total_AdjusmentKredit += AdjusmentKredit
        Total_NSSDDebet += NSSDDebet
        Total_NSSDKredit += NSSDKredit
        Total_LaporanRugiLabaDebet += LaporanRugiLabaDebet
        Total_LaporanRugiLabaKredit += LaporanRugiLabaKredit
        Total_NeracaDebet += NeracaDebet
        Total_NeracaKredit += NeracaKredit
    End Sub

    Dim SaldoAwalBulan_MUA As Decimal
    Dim SaldoAkhirBulan_MUA As Decimal
    Dim MutasiDebet_MUA As Decimal
    Dim MutasiKredit_MUA As Decimal

    Dim SaldoAwalBulan_IDR As Int64
    Dim SaldoAkhirBulan_IDR As Int64
    Dim MutasiDebet_IDR As Int64
    Dim MutasiKredit_IDR As Int64
    Dim MutasiDebet_SDS As Int64
    Dim MutasiKredit_SDS As Int64
    Dim SaldoAkhirBulan_SDS As Int64
    Dim NeracaSaldoDebet As Int64
    Dim NeracaSaldoKredit As Int64
    Dim AdjusmentDebet As Int64
    Dim AdjusmentKredit As Int64
    Dim NSSDDebet As Int64
    Dim NSSDKredit As Int64
    Dim LaporanRugiLabaDebet As Int64
    Dim LaporanRugiLabaKredit As Int64
    Dim NeracaDebet As Int64
    Dim NeracaKredit As Int64

    Dim Total_MutasiDebet_IDR As Int64
    Dim Total_MutasiKredit_IDR As Int64
    Dim Total_MutasiDebet_SDS As Int64
    Dim Total_MutasiKredit_SDS As Int64
    Dim Total_SaldoAkhirBulan_SDS As Int64
    Dim Total_NeracaSaldoDebet As Int64
    Dim Total_NeracaSaldoKredit As Int64
    Dim Total_AdjusmentDebet As Int64
    Dim Total_AdjusmentKredit As Int64
    Dim Total_NSSDDebet As Int64
    Dim Total_NSSDKredit As Int64
    Dim Total_LaporanRugiLabaDebet As Int64
    Dim Total_LaporanRugiLabaKredit As Int64
    Dim Total_NeracaDebet As Int64
    Dim Total_NeracaKredit As Int64

    Dim COA
    Dim NamaAkun
    Dim LabaRugiTahunBerjalan As Int64
    Dim QuerySimpanSaldo As String
    Dim cmdSimpanSaldo As OdbcCommand
    Sub TrialBalance()

        pnl_DataGridUtama.Visibility = Visibility.Visible

        KetersediaanMenuHalaman(pnl_Halaman, False)

        Saldo_Awal.Visibility = Visibility.Visible
        Mutasi_Debet.Visibility = Visibility.Visible
        Mutasi_Kredit.Visibility = Visibility.Visible
        Neraca_Saldo_Debet.Visibility = Visibility.Visible
        Neraca_Saldo_Kredit.Visibility = Visibility.Visible
        Adjusment_Debet.Visibility = Visibility.Visible
        Adjusment_Kredit.Visibility = Visibility.Visible
        NSSD_Debet.Visibility = Visibility.Visible
        NSSD_Kredit.Visibility = Visibility.Visible
        Laporan_Rugi_Laba_Debet.Visibility = Visibility.Visible
        Laporan_Rugi_Laba_Kredit.Visibility = Visibility.Visible
        Neraca_Debet.Visibility = Visibility.Visible
        Neraca_Kredit.Visibility = Visibility.Visible

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Data Tabel
        Dim DebetKreditCOA
        Dim KodeMataUang As String
        Dim KursTengahBI_SaldoAwal As Decimal
        Dim looping = 0 'Hanya untuk keperluan sewaktu-waktu saja
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
        ResetTotal()
        Do While dr.Read
            looping += 1  'Hanya untuk keperluan sewaktu-waktu saja
            'PesanUntukProgrammer("Looping : " & looping)
            Try
                If LoopingTrialBalance = False Then Exit Do
                COA = dr.Item("COA")
                NamaAkun = dr.Item("Nama_Akun")
                DebetKreditCOA = dr.Item("D_K")
                KodeMataUang = dr.Item("Kode_Mata_Uang")
                If BulanLaporan_Angka = 1 Then
                    KursTengahBI_SaldoAwal = KursTengahBI_AkhirTahunLalu(KodeMataUang)
                    SaldoAwalBulan_IDR = AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_SaldoAwal, dr.Item("Saldo_Awal"))
                    SaldoAwalBulan_MUA = dr.Item("Saldo_Awal")
                Else
                    KursTengahBI_SaldoAwal = KursTengahBI_AkhirBulan(KodeMataUang, BulanLaporan_Angka - 1)
                    SaldoAwalBulan_IDR = AmbilValue_NilaiMataUang(KodeMataUang, KursTengahBI_SaldoAwal, dr.Item("Saldo_" & KonversiAngkaKeBulanString(BulanLaporan_Angka - 1)))
                    SaldoAwalBulan_MUA = dr.Item("Saldo_" & KonversiAngkaKeBulanString(BulanLaporan_Angka - 1))
                End If
                If COA = KodeTautanCOA_LabaDitahan Then PerhitunganSaldoAwalBulan_LabaDitahan()
                MutasiPerBulan(BulanLaporan_Angka, COA, KodeMataUang, DebetKreditCOA,
                               SaldoAwalBulan_MUA, MutasiDebet_MUA, MutasiKredit_MUA, SaldoAkhirBulan_MUA,
                               SaldoAwalBulan_IDR, MutasiDebet_IDR, MutasiKredit_IDR, SaldoAkhirBulan_IDR)
                If COA = KodeTautanCOA_LabaTahunBerjalan Then
                    SaldoAwalBulan_MUA = 0
                    MutasiDebet_MUA = 0
                    MutasiKredit_MUA = 0
                    SaldoAwalBulan_IDR = 0
                    MutasiDebet_IDR = 0
                    MutasiKredit_IDR = 0
                End If
                If COATermasukLabaRugi(COA) Then
                    SaldoAwalBulan_MUA = 0
                    SaldoAwalBulan_IDR = 0
                End If
                If DebetKreditCOA = dk_DEBET_ Then
                    NeracaSaldoDebet = SaldoAwalBulan_IDR + MutasiDebet_IDR - MutasiKredit_IDR
                    NeracaSaldoKredit = 0
                Else
                    NeracaSaldoDebet = 0
                    NeracaSaldoKredit = SaldoAwalBulan_IDR + MutasiKredit_IDR - MutasiDebet_IDR
                End If
                AdjusmentPerBulan(BulanLaporan_Angka, COA, NamaAkun, DebetKreditCOA, AdjusmentDebet, AdjusmentKredit)
                MutasiDebet_SDS = MutasiDebet_IDR + AdjusmentDebet
                MutasiKredit_SDS = MutasiKredit_IDR + AdjusmentKredit
                If DebetKreditCOA = dk_DEBET_ Then
                    NSSDDebet = NeracaSaldoDebet + AdjusmentDebet - AdjusmentKredit
                    NSSDKredit = 0
                    SaldoAkhirBulan_SDS = NSSDDebet
                Else
                    NSSDDebet = 0
                    NSSDKredit = NeracaSaldoKredit + AdjusmentKredit - AdjusmentDebet
                    SaldoAkhirBulan_SDS = NSSDKredit
                End If
                If COATermasukLabaRugi(COA) Then
                    LaporanRugiLabaDebet = NSSDDebet
                    LaporanRugiLabaKredit = NSSDKredit
                    NeracaDebet = 0
                    NeracaKredit = 0
                End If
                If COATermasukNeraca(COA) Then
                    NeracaDebet = NSSDDebet
                    NeracaKredit = NSSDKredit
                    LaporanRugiLabaDebet = 0
                    LaporanRugiLabaKredit = 0
                End If
                TambahBaris()
                ProgressUp(pgb_Progress)
                If ProsesTutupBukuBulan Then
                    If KodeMataUang = KodeMataUang_IDR Then
                        Dim MutasiDebet_Simpan As Int64 = 0
                        Dim MutasiKredit_Simpan As Int64 = 0
                        Dim SaldoAkhirBulan_Simpan As Int64
                        If KepalaCOA(COA, 1) Or KepalaCOA(COA, 2) Or KepalaCOA(COA, 3) Or KepalaCOA(COA, 4) Then
                            MutasiDebet_Simpan = MutasiDebet_SDS
                            MutasiKredit_Simpan = MutasiKredit_SDS
                            SaldoAkhirBulan_Simpan = SaldoAkhirBulan_SDS
                        ElseIf KepalaCOA(COA, 5) Then
                            SaldoAkhirBulan_Simpan = MutasiDebet_IDR
                            If COA = KodeTautanCOA_HargaPokokPenjualan Then
                                SaldoAkhirBulan_Simpan = AdjusmentDebet
                            ElseIf KepalaCOA(COA, 533) _
                                Or COA = KodeTautanCOA_BiayaTenagaKerjaLangsung _
                                Or COA = KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal _
                                Or COA = KodeTautanCOA_BiayaPemakaianBahanBaku_Import _
                                Or COA = KodeTautanCOA_BiayaBahanBaku _
                                Or COA = KodeTautanCOA_PembelianBahanPenolong _
                                Or COA = KodeTautanCOA_BiayaPemakaianBahanPenolong _
                                Or COA = KodeTautanCOA_BiayaOverheadPabrik _
                                Or COA = KodeTautanCOA_BiayaProduksi _
                                Or COA = KodeTautanCOA_HargaPokokProduksi _
                                Then
                                SaldoAkhirBulan_Simpan = AdjusmentKredit
                            End If
                        ElseIf KepalaCOA(COA, 6) Then
                            SaldoAkhirBulan_Simpan = NSSDDebet
                        ElseIf KepalaCOA(COA, 7) Then
                            SaldoAkhirBulan_Simpan = NSSDKredit
                        ElseIf KepalaCOA(COA, 8) Then
                            SaldoAkhirBulan_Simpan = NSSDDebet
                        End If
                        'SaldoAkhirBulan_Simpan = SaldoAkhirBulan_SDS
                        QuerySimpanSaldo =
                            " UPDATE tbl_COA SET " &
                            " Debet_" & BulanLaporan_String & "     = '" & DesimalFormatSimpan(MutasiDebet_Simpan) & "', " &
                            " Kredit_" & BulanLaporan_String & "    = '" & DesimalFormatSimpan(MutasiKredit_Simpan) & "', " &
                            " Saldo_" & BulanLaporan_String & "     = '" & DesimalFormatSimpan(SaldoAkhirBulan_Simpan) & "' " &
                            " WHERE COA                             = '" & COA & "' "
                    Else
                        QuerySimpanSaldo =
                            " UPDATE tbl_COA SET " &
                            " Debet_" & BulanLaporan_String & "     = '" & DesimalFormatSimpan(MutasiDebet_MUA) & "', " &
                            " Kredit_" & BulanLaporan_String & "    = '" & DesimalFormatSimpan(MutasiKredit_MUA) & "', " &
                            " Saldo_" & BulanLaporan_String & "     = '" & DesimalFormatSimpan(SaldoAkhirBulan_MUA) & "' " &
                            " WHERE COA                             = '" & COA & "' "
                    End If
                    cmdSimpanSaldo = New OdbcCommand(QuerySimpanSaldo, KoneksiDatabaseGeneral)
                    cmdSimpanSaldo.ExecuteNonQuery()
                    Jeda(JedaPerBarisCOA)
                End If
                Proses = True
            Catch ex As Exception
                PesanUntukProgrammer("Kesalahan Query atau Coding")
                Proses = False
                Exit Do
            End Try
            OperasiTotal()
        Loop
        AksesDatabase_Transaksi(Tutup)
        LabaRugiTahunBerjalan = Total_LaporanRugiLabaKredit - Total_LaporanRugiLabaDebet
        'PesanUntukProgrammer("Laba/Rugi Tahun Berjalan : " & LabaRugiTahunBerjalan & Enter2Baris &
        '                     "Laba/Rugi Debet : " & Total_LaporanRugiLabaDebet & Enter2Baris &
        '                     "Laba/Rugi Kredit " & Total_LaporanRugiLabaKredit)
        If ProsesTutupBukuBulan Then
            QuerySimpanSaldo =
            " UPDATE tbl_COA SET " &
            " Debet_" & BulanLaporan_String & "     = '" & DesimalFormatSimpan(Total_LaporanRugiLabaDebet) & "', " &
            " Kredit_" & BulanLaporan_String & "    = '" & DesimalFormatSimpan(Total_LaporanRugiLabaKredit) & "', " &
            " Saldo_" & BulanLaporan_String & "     = '" & DesimalFormatSimpan(LabaRugiTahunBerjalan) & "' " &
            " WHERE COA                             = '" & KodeTautanCOA_LabaTahunBerjalan & "' "
            Try
                cmdSimpanSaldo = New OdbcCommand(QuerySimpanSaldo, KoneksiDatabaseGeneral)
                cmdSimpanSaldo.ExecuteNonQuery()
                Proses = True
            Catch ex As Exception
                Proses = False
                PesanPeringatan("Proses Tutup Buku Gagal...!")
                ResetLaporan()
                Exit Sub
            End Try
        End If
        AksesDatabase_General(Tutup)
        'Total : 
        datatabelUtama.Rows.Add(
                    "T O T A L", Kosongan, 0,
                    Total_MutasiDebet_IDR, Total_MutasiKredit_IDR,
                    Total_NeracaSaldoDebet, Total_NeracaSaldoKredit,
                    Total_AdjusmentDebet, Total_AdjusmentKredit,
                    Total_NSSDDebet, Total_NSSDKredit,
                    Total_LaporanRugiLabaDebet, Total_LaporanRugiLabaKredit,
                    Total_NeracaDebet, Total_NeracaKredit)
        'Laba Tahun Berjalan: 
        Dim LabaRugiTahunBerjalan_Debet As Int64 = 0
        Dim LabaRugiTahunBerjalan_Kredit As Int64 = 0
        If LabaRugiTahunBerjalan > 0 Then
            LabaRugiTahunBerjalan_Debet = LabaRugiTahunBerjalan
            Total_LaporanRugiLabaDebet += LabaRugiTahunBerjalan_Debet
        End If
        If LabaRugiTahunBerjalan < 0 Then
            LabaRugiTahunBerjalan_Kredit = -LabaRugiTahunBerjalan
            Total_LaporanRugiLabaKredit += LabaRugiTahunBerjalan_Kredit
        End If
        datatabelUtama.Rows.Add(
                    AmbilValue_NamaAkun(KodeTautanCOA_LabaTahunBerjalan), KodeTautanCOA_LabaTahunBerjalan, 0,
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0,
                    LabaRugiTahunBerjalan_Debet, LabaRugiTahunBerjalan_Kredit,
                    0, LabaRugiTahunBerjalan)
        'Balance :
        Total_NeracaKredit += LabaRugiTahunBerjalan
        datatabelUtama.Rows.Add(
                    "B A L A N C E", Kosongan, 0,
                    0, 0,
                    0, 0,
                    0, 0,
                    0, 0,
                    Total_LaporanRugiLabaDebet, Total_LaporanRugiLabaKredit,
                    Total_NeracaDebet, Total_NeracaKredit)
        BersihkanSeleksi()
    End Sub
    Sub TambahBaris()
        If SaldoAwalBulan_IDR <> 0 _
            Or MutasiDebet_IDR <> 0 Or MutasiKredit_IDR <> 0 _
            Or NeracaSaldoDebet <> 0 Or NeracaSaldoKredit <> 0 _
            Or AdjusmentDebet <> 0 Or AdjusmentKredit <> 0 _
            Or NSSDDebet <> 0 Or NSSDKredit <> 0 _
            Or LaporanRugiLabaDebet <> 0 Or LaporanRugiLabaKredit <> 0 _
            Or NeracaDebet <> 0 Or NeracaKredit <> 0 Then
            datatabelUtama.Rows.Add(
                NamaAkun, COA, SaldoAwalBulan_IDR,
                MutasiDebet_IDR, MutasiKredit_IDR,
                NeracaSaldoDebet, NeracaSaldoKredit,
                AdjusmentDebet, AdjusmentKredit,
                NSSDDebet, NSSDKredit,
                LaporanRugiLabaDebet, LaporanRugiLabaKredit,
                NeracaDebet, NeracaKredit)
        End If
    End Sub

    Sub MutasiPerBulan(ByVal BulanAngka As String, ByVal COA As String, ByVal KodeMataUang_COA As String, ByVal DebetKreditCOA As String,
                       ByVal SaldoAwalBulan_MUA As Decimal, ByRef MutasiDebet_MUA As Decimal, ByRef MutasiKredit_MUA As Decimal, ByRef SaldoAkhirBulan_MUA As Decimal,
                       ByVal SaldoAwalBulan_IDR As Int64, ByRef MutasiDebet_IDR As Int64, ByRef MutasiKredit_IDR As Int64, ByRef SaldoAkhirBulan_IDR As Int64)
        Dim QueryBulan =
            " SELECT Kode_Mata_Uang, Kurs, Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE Valid <> '" & _X_ & "' " &
            " AND Status_Approve = 1 " &
            " AND COA = '" & COA & "' " &
            " AND Jenis_Jurnal  <> '" & JenisJurnal_Penyusutan & "' " &
            " AND Jenis_Jurnal  <> '" & JenisJurnal_Amortisasi & "' " &
            " AND Jenis_Jurnal  <> '" & JenisJurnal_AdjusmentHPP & "' " &
            " AND ( NOT (Jenis_Jurnal = '" & JenisJurnal_AdjusmentForex & "' AND Tanggal_Transaksi = '" & TanggalFormatSimpan(AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka, TahunBukuAktif)) & "' ) ) " &
            FilterBulan(BulanAngka)
        Dim cmdBulan = New OdbcCommand(QueryBulan, KoneksiDatabaseTransaksi)
        Dim drBulan = cmdBulan.ExecuteReader
        Dim KodeMataUang_Transaksi As String
        Dim Kurs As Decimal
        Dim JumlahDebet As Decimal
        Dim JumlahKredit As Decimal
        MutasiDebet_MUA = 0
        MutasiKredit_MUA = 0
        MutasiDebet_IDR = 0
        MutasiKredit_IDR = 0
        Do While drBulan.Read
            KodeMataUang_Transaksi = drBulan.Item("Kode_Mata_Uang")
            Kurs = drBulan.Item("Kurs")
            JumlahDebet = drBulan.Item("Jumlah_Debet")
            JumlahKredit = drBulan.Item("Jumlah_Kredit")
            If KodeMataUang_COA = KodeMataUang_Transaksi Then
                MutasiDebet_MUA += JumlahDebet
                MutasiKredit_MUA += JumlahKredit
            End If
            MutasiDebet_IDR += AmbilValue_NilaiMataUang(KodeMataUang_Transaksi, Kurs, JumlahDebet)
            MutasiKredit_IDR += AmbilValue_NilaiMataUang(KodeMataUang_Transaksi, Kurs, JumlahKredit)
        Loop
        If COA >= AwalAkunBiaya Then '( Jika COA termasuk Kelompok LABA/RUGI )
            SaldoAkhirBulan_IDR = 0
        Else
            If DebetKreditCOA = dk_DEBET_ Then
                SaldoAkhirBulan_MUA = SaldoAwalBulan_MUA + MutasiDebet_MUA - MutasiKredit_MUA
                SaldoAkhirBulan_IDR = SaldoAwalBulan_IDR + MutasiDebet_IDR - MutasiKredit_IDR
            Else
                SaldoAkhirBulan_MUA = SaldoAwalBulan_MUA + MutasiKredit_MUA - MutasiDebet_MUA
                SaldoAkhirBulan_IDR = SaldoAwalBulan_IDR + MutasiKredit_IDR - MutasiDebet_IDR
            End If
        End If
    End Sub

    Sub AdjusmentPerBulan(ByVal BulanAngka As String,
                          ByVal COA As String,
                          ByVal NamaAkun As String,
                          ByVal DebetKreditCOA As String,
                          ByRef MutasiDebetBulan_IDR As Int64,
                          ByRef MutasiKreditBulan_IDR As Int64)
        'PesanUntukProgrammer("Bulan Laporan Angka : " & BulanLaporan_Angka & Enter2Baris &
        '                     "Proses Trial Balance : " & ProsesTrialBalance)
        Dim QueryBulan =
            " SELECT Jumlah_Debet, Jumlah_Kredit FROM tbl_Transaksi WHERE Valid <> '" & _X_ & "' " &
            " AND Status_Approve = 1 " &
            " AND COA = '" & COA & "' AND " &
            " ( " &
            "    Jenis_Jurnal   = '" & JenisJurnal_Penyusutan & "' " &
            " OR Jenis_Jurnal   = '" & JenisJurnal_Amortisasi & "' " &
            " OR Jenis_Jurnal   = '" & JenisJurnal_AdjusmentHPP & "' " &
            " OR (Jenis_Jurnal  = '" & JenisJurnal_AdjusmentForex & "' AND Tanggal_Transaksi = '" & TanggalFormatSimpan(AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAngka, TahunBukuAktif)) & "' ) " &
            " ) " & FilterBulan(BulanAngka)
        'PesanUntukProgrammer(QueryBulan)
        Dim cmdBulan = New OdbcCommand(QueryBulan, KoneksiDatabaseTransaksi)
        Dim drBulan = cmdBulan.ExecuteReader
        Dim JumlahDebet_IDR As Int64    'Dengan asumsi, semua Jurnal Adjusment itu mata uangnya adalah IDR
        Dim JumlahKredit_IDR As Int64   'Idem
        MutasiDebetBulan_IDR = 0
        MutasiKreditBulan_IDR = 0
        Do While drBulan.Read
            JumlahDebet_IDR = drBulan.Item("Jumlah_Debet")
            JumlahKredit_IDR = drBulan.Item("Jumlah_Kredit")
            MutasiDebetBulan_IDR += JumlahDebet_IDR
            MutasiKreditBulan_IDR += JumlahKredit_IDR
        Loop
    End Sub

    Sub PerhitunganSaldoAwalBulan_LabaDitahan()
        Dim SaldoAwalBulan_LabaDitahan As Int64
        Dim SaldoAwalTahun_LabaDitahan As Int64 = SaldoAwalTahunCOA(KodeTautanCOA_LabaDitahan)
        Dim Akumulasi_LabaBerjalan As Int64 = 0
        Dim BulanTelusur As Integer
        If BulanLaporan_Angka = 1 Then
            SaldoAwalBulan_LabaDitahan = SaldoAwalTahun_LabaDitahan
        Else
            BulanTelusur = 1 'Karena dimulai dari Februari (bukan Januari), maka value BulanTelusur dimulai dari angka 1 (bukan 0).
            Do While BulanTelusur < BulanLaporan_Angka
                BulanTelusur += 1
                Akumulasi_LabaBerjalan += SaldoAkhirBulanCOA(KodeTautanCOA_LabaTahunBerjalan, BulanTelusur - 1)
            Loop
            SaldoAwalBulan_LabaDitahan = SaldoAwalTahun_LabaDitahan + Akumulasi_LabaBerjalan
        End If
        SaldoAwalBulan_IDR = SaldoAwalBulan_LabaDitahan
        'PesanUntukProgrammer("Akun : " & COA & StripKosong & AmbilValue_NamaAkun(COA) & Enter2Baris &
        '                                 "Bulan Laporan : " & KonversiAngkaKeBulanString(BulanLaporan_Angka) & Enter2Baris &
        '                                 "Bulan Akhir Telusur Akumulasi Laba Berjalan : " & KonversiAngkaKeBulanString(BulanTelusur - 1) & Enter2Baris &
        '                                 "Saldo Awal Tahun Laba Ditahan : " & SaldoAwalTahun_LabaDitahan & Enter2Baris &
        '                                 "Akumulasi Laba Berjalan : " & Akumulasi_LabaBerjalan & Enter2Baris &
        '                                 "Saldo Awal Bulan Laba Ditahan : " & SaldoAwalBulan_IDR)
    End Sub

    Function FilterBulan(BulanAngka As Integer) As String
        Return " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & TahunBukuAktif & "-" & KonversiAngkaKeStringDuaDigit(BulanAngka) & "' "
    End Function



    Private Sub cmb_TahunTrialBalance_SelectedIndexChanged(sender As Object, e As EventArgs)
        'TampilkanData()
    End Sub
    Private Sub cmb_TahunTrialBalance_TextChanged(sender As Object, e As EventArgs)
        'TampilkanData()
    End Sub
    Private Sub cmb_TahunTrialBalance_KeyPress(sender As Object, e As KeyPressEventArgs)
        KunciTotalInputan(sender, e)
    End Sub







    Sub TutupHalaman()
        'frm_LaporanNeracaLajur.Close()
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

    Dim Nama_Akun As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Saldo_Awal As New DataGridTextColumn
    Dim Mutasi_Debet As New DataGridTextColumn
    Dim Mutasi_Kredit As New DataGridTextColumn
    Dim Neraca_Saldo_Debet As New DataGridTextColumn
    Dim Neraca_Saldo_Kredit As New DataGridTextColumn
    Dim Adjusment_Debet As New DataGridTextColumn
    Dim Adjusment_Kredit As New DataGridTextColumn
    Dim NSSD_Debet As New DataGridTextColumn
    Dim NSSD_Kredit As New DataGridTextColumn
    Dim Laporan_Rugi_Laba_Debet As New DataGridTextColumn
    Dim Laporan_Rugi_Laba_Kredit As New DataGridTextColumn
    Dim Neraca_Debet As New DataGridTextColumn
    Dim Neraca_Kredit As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Saldo_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Mutasi_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("Neraca_Saldo_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Neraca_Saldo_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("Adjusment_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Adjusment_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("NSSD_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("NSSD_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("Laporan_Rugi_Laba_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Laporan_Rugi_Laba_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("Neraca_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Neraca_Kredit", GetType(Int64))


        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 330, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal, "Saldo_Awal", "Saldo Awal", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Debet, "Mutasi_Debet", "Jurnal" & Enter1Baris & "(Debet)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Mutasi_Kredit, "Mutasi_Kredit", "Jurnal" & Enter1Baris & "(Kredit)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Neraca_Saldo_Debet, "Neraca_Saldo_Debet", "Neraca Saldo" & Enter1Baris & "(Debet)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Neraca_Saldo_Kredit, "Neraca_Saldo_Kredit", "Neraca Saldo" & Enter1Baris & "(Kredit)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Adjusment_Debet, "Adjusment_Debet", "Adjusment" & Enter1Baris & "(Debet)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Adjusment_Kredit, "Adjusment_Kredit", "Adjusment" & Enter1Baris & "(Kredit)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NSSD_Debet, "NSSD_Debet", "NSSD" & Enter1Baris & "(Debet)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, NSSD_Kredit, "NSSD_Kredit", "NSSD" & Enter1Baris & "(Kredit)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Laporan_Rugi_Laba_Debet, "Laporan_Rugi_Laba_Debet", "Lap. Rugi Laba" & Enter1Baris & "(Debet)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Laporan_Rugi_Laba_Kredit, "Laporan_Rugi_Laba_Kredit", "Lap. Rugi Laba" & Enter1Baris & "(Kredit)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Neraca_Debet, "Neraca_Debet", "Neraca" & Enter1Baris & "(Debet)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Neraca_Kredit, "Neraca_Kredit", "Neraca" & Enter1Baris & "(Kredit)", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_DataGridUtama.Visibility = Visibility.Collapsed
    End Sub

    Sub StartProgress()
        pgb_Progress.Foreground = WarnaHijauSolid_WPF
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
