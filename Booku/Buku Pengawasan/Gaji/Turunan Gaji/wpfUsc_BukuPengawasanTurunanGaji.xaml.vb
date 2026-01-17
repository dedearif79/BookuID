Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks
Imports bcomm

Public Class wpfUsc_BukuPengawasanTurunanGaji

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Dim EksekusiTampilanData As Boolean

    Public JudulForm = Kosongan
    Public JudulForm_HutangBPJSKesehatan = "Buku Pengawasan Hutang BPJS Kesehatan"
    Public JudulForm_HutangBPJSKetenagakerjaan = "Buku Pengawasan Hutang BPJS Ketenagakerjaan"
    Public JudulForm_HutangKoperasiKaryawan = "Buku Pengawasan Hutang Koperasi Karyawan"
    Public JudulForm_HutangSerikat = "Buku Pengawasan Hutang Serikat"

    Public NamaHalaman
    Public COAHutang
    Dim Total_SisaHutang As Int64
    Dim TotalTabel As Int64

    Dim NomorUrut_Terseleksi
    Dim Bulan_Terseleksi
    Dim NomorBPH_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim JumlahPotongan_Terseleksi
    Dim SelisihTagihan_Terseleksi
    Dim KoreksiSelisih_Terseleksi
    Dim Selisih_Terseleksi
    Dim JumlahPembayaran_Terseleksi
    Dim SisaPembayaran_Terseleksi
    Dim Keterangan_Terseleksi

    Dim NomorIdPembayaran_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi As Integer
    Dim Referensi_Terseleksi

    Dim TermasukHutangTahunIni_Terseleksi As Boolean

    Dim Tombol_Tagihan = "Tagihan"
    Dim Tombol_InputTagihan = "Input Tagihan"
    Dim Tombol_EditTagihan = "Edit Tagihan"

    Public TabelPengawasan
    Public AwalanBPH
    Dim AwalanBPH_PlusTahunBuku
    Public KolomPotongan

    Public TahunTelusurData As Integer
    Public TahunTelusurDataTerlama As Integer
    Public TahunTelusurDataSebelumnya As Integer

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        lbl_JudulForm.Text = JudulForm
        Terabas()

        ProsesLoadingForm = True

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            grb_InfoSaldo.Header = "Saldo Akhir :"
            VisibilitasTombolAdjusment(False)
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            grb_InfoSaldo.Header = "Saldo Awal :"
            VisibilitasTombolAdjusment(True)
        End If

        VisibilitasTombolTambahDanHapusTahun()

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub



    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenComboTahunTelusurData(True)
        EksekusiTampilanData = True
        TampilkanData()
    End Sub



    Async Sub TampilkanDataAsync()

        'PesanUntukProgrammer("Eksekusi Tampilan Data : " & EksekusiTampilanData & Enter2Baris &
        '                     "Tahun Telusur Data : " & TahunTelusurData)

        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            KesesuaianJurnal = True

            datatabelUtama.Rows.Clear()

            'Data Tabel :
            Dim NomorUrut As Integer
            Dim Bulan = Nothing
            Dim NomorBPH
            Dim JumlahTagihan As Int64
            Dim JumlahPotongan As Int64
            Dim SelisihTagihan As Int64
            Dim KoreksiSelisihPencatatan As Int64
            Dim Selisih As Int64
            Dim JumlahPembayaran As Int64
            Dim SisaPembayaran As Int64
            Dim Keterangan

            Dim RekapTotal_JumlahTagihan As Int64
            Dim RekapTotal_JumlahPotongan As Int64
            Dim RekapTotal_SelisihTagihan As Int64
            Dim RekapTotal_KoreksiSelisih As Int64
            Dim RekapTotal_Selisih As Int64
            Dim RekapTotal_JumlahPembayaran As Int64
            Dim RekapTotal_SisaPembayaran As Int64

            RekapTotal_JumlahTagihan = 0
            RekapTotal_JumlahPotongan = 0
            RekapTotal_SelisihTagihan = 0
            RekapTotal_KoreksiSelisih = 0
            RekapTotal_Selisih = 0
            RekapTotal_JumlahPembayaran = 0
            RekapTotal_SisaPembayaran = 0

            'Ambil Value Saldo Saldo Awal Berdasarkan List : -----------------------------------------
            SaldoAwal_BerdasarkanList = 0
            BukaDatabaseTransaksi_Alternatif(TahunTelusurDataSebelumnya)
            cmdTAGIHAN = New OdbcCommand(" SELECT * FROM " & TabelPengawasan &
                                         " WHERE Tahun <= '" & TahunTelusurDataSebelumnya & "' ",
                                         KoneksiDatabaseTransaksi_Alternatif)
            drTAGIHAN = cmdTAGIHAN.ExecuteReader
            Do While drTAGIHAN.Read
                SaldoAwal_BerdasarkanList += drTAGIHAN.Item("Jumlah_Tagihan")
            Loop
            TutupDatabaseTransaksi_Alternatif()
            '-----------------------------------------------------------------------------------------
            If TahunBukuSudahStabil(TahunTelusurData) = True Then
                Total_SisaHutang = SaldoAwalTahunCOA(COAHutang)
            Else
                Total_SisaHutang = SaldoAwal_BerdasarkanList
            End If

            BukaDatabaseTransaksi_Alternatif(TahunTelusurData)
            cmdTAGIHAN = New OdbcCommand(" SELECT * FROM " & TabelPengawasan &
                                         " WHERE Tahun = '" & TahunTelusurData & "' ",
                                         KoneksiDatabaseTransaksi_Alternatif)
            drTAGIHAN = cmdTAGIHAN.ExecuteReader
            Do While drTAGIHAN.Read
                NomorUrut = drTAGIHAN.Item("Nomor_Urut")
                Bulan = drTAGIHAN.Item("Bulan")
                NomorBPH = AwalanBPH_PlusTahunBuku & NomorUrut
                JumlahTagihan = drTAGIHAN.Item("Jumlah_Tagihan")
                JumlahPotongan = 0
                SelisihTagihan = 0
                KoreksiSelisihPencatatan = 0
                Selisih = 0
                JumlahPembayaran = 0
                Keterangan = drTAGIHAN.Item("Keterangan")
                If TahunTelusurData = TahunBukuAktif And TahunTelusurData > TahunCutOff Then
                    cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanGaji WHERE Bulan = '" & Bulan & "'", KoneksiDatabaseTransaksi_Alternatif)
                    dr = cmd.ExecuteReader
                    Do While dr.Read
                        JumlahPotongan += dr.Item(KolomPotongan)
                    Loop
                Else
                    JumlahPotongan = JumlahTagihan
                End If
                SelisihTagihan = JumlahTagihan - JumlahPotongan
                KoreksiSelisihPencatatan = drTAGIHAN.Item("Koreksi_Selisih")
                Selisih = SelisihTagihan + KoreksiSelisihPencatatan
                If Selisih <> 0 Then KesesuaianJurnal = False
                RekapTotal_JumlahTagihan += JumlahTagihan
                RekapTotal_JumlahPotongan += JumlahPotongan
                RekapTotal_SelisihTagihan += SelisihTagihan
                RekapTotal_KoreksiSelisih += KoreksiSelisihPencatatan
                RekapTotal_Selisih += Selisih
                cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                           " WHERE Nomor_BP     = '" & NomorBPH & "' " &
                                           " AND Status_Invoice = '" & Status_Dibayar & "' ",
                                           KoneksiDatabaseTransaksi_Alternatif)
                drBAYAR = cmdBAYAR.ExecuteReader
                Do While drBAYAR.Read
                    JumlahPembayaran += drBAYAR.Item("Jumlah_Bayar")
                Loop
                SisaPembayaran = JumlahTagihan - JumlahPembayaran
                RekapTotal_JumlahPembayaran = RekapTotal_JumlahPembayaran + JumlahPembayaran
                RekapTotal_SisaPembayaran = RekapTotal_SisaPembayaran + SisaPembayaran
                datatabelUtama.Rows.Add(NomorUrut, Bulan, NomorBPH, JumlahTagihan, JumlahPotongan,
                                        SelisihTagihan, KoreksiSelisihPencatatan, Selisih,
                                        JumlahPembayaran, SisaPembayaran, Keterangan)
                Total_SisaHutang += SisaPembayaran
                Await Task.Yield()
            Loop
            TutupDatabaseTransaksi_Alternatif()
            datatabelUtama.Rows.Add()
            datatabelUtama.Rows.Add(Kosongan, "J U M L A H", Kosongan, RekapTotal_JumlahTagihan, RekapTotal_JumlahPotongan,
                                    RekapTotal_SelisihTagihan, RekapTotal_KoreksiSelisih, RekapTotal_Selisih,
                                    RekapTotal_JumlahPembayaran, RekapTotal_SisaPembayaran, Kosongan)

            TotalTabel = Total_SisaHutang

            Select Case JenisTahunBuku
                Case JenisTahunBuku_LAMPAU
                    SaldoAkhir_BerdasarkanList = Total_SisaHutang
                    txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                    AmbilValue_SaldoAkhirBerdasarkanCOA()
                    CekKesesuaianSaldoAkhir()
                    txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
                Case JenisTahunBuku_NORMAL
                    txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
                    CekKesesuaianSaldoAwal()
                    txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
            End Select

            txt_TotalTabel.Text = TotalTabel
            lbl_TotalTabel.Text = "Saldo Akhir " & TahunTelusurData & " : "

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPengawasanTurunanGaji")

        Finally
            BersihkanSeleksi()
            KetersediaanMenuHalaman(pnl_Halaman, True)
            SedangMemuatData = False
        End Try

    End Sub

    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub





    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Tagihan.Content = Tombol_Tagihan
        btn_Tagihan.IsEnabled = False
        btn_Adjusment.IsEnabled = False
        KetersediaanTombolJurnal(False)
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        NomorJV_Pembayaran_Terseleksi = 0
        VisibilitasInfoSaldo(True)
        BersihkanSeleksiTabelPembayaran()
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True, False)
    End Sub


    Sub KontenComboTahunTelusurData(IsiComboSaja As Boolean)

        TahunTelusurDataTerlama = AmbilTahunTerlama_BerdasarkanKolomTahun(TahunCutOff, TabelPengawasan, "Tahun")
        Dim ListTahunTelusurData = TahunBukuAktif

        cmb_TahunTelusurData.Items.Clear()
        TahunTelusurData = TahunBukuAktif
        Do While ListTahunTelusurData >= TahunTelusurDataTerlama
            cmb_TahunTelusurData.Items.Add(ListTahunTelusurData)
            ListTahunTelusurData -= 1
        Loop

        cmb_TahunTelusurData.SelectedValue = TahunBukuAktif

    End Sub

    Sub VisibilitasTombolTagihan(Visibilitas As Boolean)
        If Visibilitas Then
            brd_Tagihan.Visibility = Visibility.Visible
            btn_Tagihan.Visibility = Visibility.Visible
        Else
            brd_Tagihan.Visibility = Visibility.Collapsed
            btn_Tagihan.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolAdjusment(Visibilitas As Boolean)
        If Visibilitas Then
            brd_Adjusment.Visibility = Visibility.Visible
            btn_Adjusment.Visibility = Visibility.Visible
        Else
            brd_Adjusment.Visibility = Visibility.Collapsed
            btn_Adjusment.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTombolTambahDanHapusTahun()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            btn_TambahTahun.Visibility = Visibility.Visible
            btn_HapusTahun.Visibility = Visibility.Visible
        Else
            btn_TambahTahun.Visibility = Visibility.Collapsed
            btn_HapusTahun.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasKolomTabel()
        Jumlah_Potongan.Visibility = Visibility.Collapsed
        Selisih_Tagihan.Visibility = Visibility.Collapsed
        Koreksi_Selisih.Visibility = Visibility.Collapsed
        Selisih_.Visibility = Visibility.Collapsed
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            Jumlah_Potongan.Visibility = Visibility.Collapsed
            Selisih_Tagihan.Visibility = Visibility.Collapsed
            Koreksi_Selisih.Visibility = Visibility.Collapsed
            Selisih_.Visibility = Visibility.Collapsed
        Else
            If TahunTelusurData = TahunBukuAktif Then
                Jumlah_Potongan.Visibility = Visibility.Visible
                Selisih_Tagihan.Visibility = Visibility.Visible
                Koreksi_Selisih.Visibility = Visibility.Visible
                Selisih_.Visibility = Visibility.Visible
            End If
        End If
    End Sub

    Sub VisibilitasInfoSaldo(Visibilitas As Boolean)
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_TotalTabel.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If TahunTelusurData = TahunBukuAktif Then
                grb_InfoSaldo.Visibility = Visibility.Visible
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                    pnl_SaldoAwalPlusAJP.Visibility = Visibility.Visible
                    pnl_TotalTabel.Visibility = Visibility.Visible
                End If
            End If
        Else
            grb_InfoSaldo.Visibility = Visibility.Collapsed
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
            pnl_TotalTabel.Visibility = Visibility.Collapsed
        End If
        If JenisTahunBuku = JenisTahunBuku_LAMPAU And TahunTelusurData = TahunBukuAktif Then
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
            pnl_TotalTabel.Visibility = Visibility.Collapsed
        Else
            pnl_TotalTabel.Visibility = Visibility.Visible
        End If
    End Sub


    Sub VisibilitasTabelPembayaran()
        If JumlahBarisBayar > 0 Then
            datagridBayar.Visibility = Visibility.Visible
        Else
            datagridBayar.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KetersediaanTombolJurnal(Tersedia As Boolean)
        btn_LihatJurnal.IsEnabled = False
        If Tersedia Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If
    End Sub






    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Tagihan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tagihan.Click
        win_InputTagihanTurunanGaji = New wpfWin_InputTagihanTurunanGaji
        win_InputTagihanTurunanGaji.ResetForm()
        win_InputTagihanTurunanGaji.JudulForm = JudulForm
        win_InputTagihanTurunanGaji.TahunTelusurData = TahunTelusurData
        win_InputTagihanTurunanGaji.Bulan = Bulan_Terseleksi
        win_InputTagihanTurunanGaji.JumlahTagihan = AmbilAngka(JumlahTagihan_Terseleksi)
        win_InputTagihanTurunanGaji.JumlahPotongan = AmbilAngka(JumlahPotongan_Terseleksi)
        win_InputTagihanTurunanGaji.Keterangan = Keterangan_Terseleksi
        win_InputTagihanTurunanGaji.ShowDialog()
    End Sub

    Private Sub btn_Adjusment_Click(sender As Object, e As RoutedEventArgs) Handles btn_Adjusment.Click
        PenyesuaianSelisih()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Pembayaran_Terseleksi)
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Private Sub cmb_TahunTelusurData_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_TahunTelusurData.SelectionChanged
        If Not EksekusiTampilanData Then Return
        TahunTelusurData = cmb_TahunTelusurData.SelectedValue
        If TahunTelusurData = 0 Then
            cmb_TahunTelusurData.Text = TahunTelusurDataTerlama
            TahunTelusurData = TahunTelusurDataTerlama
        End If
        PerubahanTahunTelusurData()
    End Sub
    Sub GantiTahunTelusurData_ManualPaksa(TahunPengganti As Integer)
        If TahunPengganti < TahunTelusurDataTerlama Then TahunPengganti = TahunTelusurDataTerlama
        cmb_TahunTelusurData.Text = TahunPengganti
        TahunTelusurData = TahunPengganti
        EksekusiTampilanData = True
        PerubahanTahunTelusurData()
    End Sub
    Sub PerubahanTahunTelusurData()
        TahunTelusurDataSebelumnya = TahunTelusurData - 1
        AwalanBPH_PlusTahunBuku = AwalanBPH & TahunTelusurData & "-"
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            VisibilitasTombolTagihan(True)
        Else
            If TahunTelusurData = TahunBukuAktif Then
                VisibilitasTombolTagihan(True)
            Else
                VisibilitasTombolTagihan(False)
            End If
        End If
        VisibilitasKolomTabel()
        TampilkanData()
    End Sub

    Private Sub btn_TambahTahun_Click(sender As Object, e As RoutedEventArgs) Handles btn_TambahTahun.Click

        Dim TahunTelusurDataBaru = TahunTelusurDataTerlama - 1

        Dim Pesan As String =
            "Anda akan menambahkan Tabel Tagihan " & TahunTelusurDataBaru & "." & Enter1Baris &
            "Lanjutkan proses?"
        If Not TanyaKonfirmasi(Pesan) Then Return

        Dim NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelPengawasan)
        Dim NomorUrut = 0
        Dim Bulan = Kosongan
        AksesDatabase_Transaksi(Buka)
        Do While NomorUrut < 12
            NomorID += 1
            NomorUrut += 1
            Bulan = BulanTerbilang(NomorUrut)
            cmd = New OdbcCommand(
                " INSERT INTO " & TabelPengawasan & " VALUES ( " &
                " '" & NomorID & "', " &
                " '" & NomorUrut & "', " &
                " '" & Bulan & "', " &
                " '" & TahunTelusurDataBaru & "', " &
                " '" & 0 & "', " &
                " '" & 0 & "', " &
                " '" & Kosongan & "' " &
                " )",
                KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        Loop
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            KontenComboTahunTelusurData(False)
            GantiTahunTelusurData_ManualPaksa(TahunTelusurDataTerlama)
            Terabas()
            Pesan_Sukses("Tabel Tagihan Tahun " & TahunTelusurDataBaru & " berhasil ditambahkan.")
        Else
            Pesan_Peringatan("Tabel Tagihan Tahun " & TahunTelusurDataBaru & " gagal ditambahkan." & Enter2Baris &
            teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub btn_HapusTahun_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusTahun.Click

        If TahunTelusurDataTerlama = TahunBukuAktif Then
            Pesan_Peringatan("Tabel Tagihan Tahun " & TahunTelusurDataTerlama & " tidak dapat dihapus." & Enter1Baris &
                             "Namun Anda dapat mengosongkannya.")
            Return
        End If

        Dim TahunTelusurDataAsal = TahunTelusurData
        Dim TahunTelusurDataYangDihapus = TahunTelusurDataTerlama

        Dim Pesan As String =
            "Anda akan menghapus tabel dan seluruh data Tagihan Tahun " & TahunTelusurDataYangDihapus & "." & Enter1Baris &
            "Lanjutkan proses?"
        If Not TanyaKonfirmasi(Pesan) Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(
            " DELETE FROM " & TabelPengawasan &
            " WHERE Tahun = '" & TahunTelusurDataYangDihapus & "' ",
            KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            KontenComboTahunTelusurData(False)
            GantiTahunTelusurData_ManualPaksa(TahunTelusurDataAsal)
            Terabas()
            Pesan_Sukses("Tabel Tagihan Tahun " & TahunTelusurDataYangDihapus & " berhasil dihapus.")
        Else
            Pesan_Peringatan("Tabel Tagihan Tahun " & TahunTelusurDataYangDihapus & " gagal dihapus." & Enter2Baris &
            teks_SilakanCobaLagi_Database)
        End If

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

        NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        Bulan_Terseleksi = rowviewUtama("Bulan_")
        NomorBPH_Terseleksi = rowviewUtama("Nomor_BPH")
        JumlahTagihan_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Tagihan"))
        JumlahPotongan_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Potongan"))
        SelisihTagihan_Terseleksi = AmbilAngka(rowviewUtama("Selisih_Tagihan"))
        KoreksiSelisih_Terseleksi = AmbilAngka(rowviewUtama("Koreksi_Selisih"))
        Selisih_Terseleksi = AmbilAngka(rowviewUtama("Selisih_"))
        JumlahPembayaran_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Pembayaran"))
        SisaPembayaran_Terseleksi = AmbilAngka(rowviewUtama("Sisa_Pembayaran"))
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")

        btn_Tagihan.IsEnabled = True
        If Selisih_Terseleksi = 0 Then
            btn_Adjusment.IsEnabled = False
        Else
            btn_Adjusment.IsEnabled = True
        End If
        btn_LihatJurnal.IsEnabled = False

        If JumlahTagihan_Terseleksi > 0 Then
            btn_InputBayar.IsEnabled = True
        Else
            btn_InputBayar.IsEnabled = False
        End If

        If JumlahTagihan_Terseleksi = 0 Then
            btn_Tagihan.Content = Tombol_InputTagihan
        Else
            btn_Tagihan.Content = Tombol_EditTagihan
        End If

        TampilkanDataPembayaran()

        If BarisTerseleksi >= 12 Then BersihkanSeleksi()

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        'Belum ada coding.
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If AmbilAngka(e.Row.Item("Jumlah_Potongan")) > 0 Then e.Row.Foreground = clrTeksPrimer
        If AmbilAngka(e.Row.Item("Jumlah_Potongan")) = 0 Then e.Row.Foreground = clrNeutral500
        If AmbilAngka(e.Row.Item("Selisih_")) = 0 Then
            'PewarnaanCellFormatTeks(Selisih_, e.Row, clrBlack)
            e.Row.Foreground = clrTeksPrimer
        Else
            'PewarnaanCellFormatTeks(Selisih_, e.Row, clrWarning)
            e.Row.Foreground = clrWarning
        End If
    End Sub


    Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    End Sub
    Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
        HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomBayar IsNot Nothing Then
            BersihkanSeleksiTabelPembayaran()
        End If
    End Sub
    Private Sub datagridBayar_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBayar.SelectedCellsChanged

        KolomTerseleksiBayar = datagridBayar.CurrentColumn
        BarisBayar_Terseleksi = datagridBayar.SelectedIndex
        If BarisBayar_Terseleksi < 0 Then Return
        rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
        If Not rowviewBayar IsNot Nothing Then Return

        NomorIdPembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_ID_Bayar").ToString)
        NomorJV_Pembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_JV_Bayar").ToString)
        Referensi_Terseleksi = rowviewBayar("Referensi_")
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditBayar.IsEnabled = True
            btn_HapusBayar.IsEnabled = True
        Else
            BersihkanSeleksiTabelPembayaran()
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then KetersediaanTombolJurnal(False)


    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
    End Sub



    Sub TampilkanDataPembayaran()

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then pnl_SidebarKanan.Visibility = Visibility.Visible
        datatabelBayar.Rows.Clear()

        Dim Index_BarisTabelPembayaran = 0
        Dim NomorIdBayar
        Dim TanggalBayar
        Dim Referensi
        Dim JumlahBayar As Int64 = 0
        Dim TotalBayar As Int64 = 0
        Dim KeteranganBayar
        Dim NomorJVBayar
        Dim TahunSumberDataPembayaran = 0

        Dim TahunTelusurPembayaran = TahunTelusurData
        Dim PencegahLoopingTahunTelusurDataLampau = 0
        Do While TahunTelusurPembayaran <= TahunBukuAktif
            If TahunTelusurPembayaran <= TahunCutOff Then TahunSumberDataPembayaran = TahunCutOff
            If TahunTelusurPembayaran > TahunCutOff Then TahunSumberDataPembayaran = TahunTelusurPembayaran
            If TahunTelusurPembayaran > TahunCutOff Or PencegahLoopingTahunTelusurDataLampau = 0 Then
                BukaDatabaseTransaksi_Alternatif(TahunSumberDataPembayaran)
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                      " WHERE Nomor_BP          = '" & NomorBPH_Terseleksi & "' " &
                                      " AND Status_Invoice      = '" & Status_Dibayar & "' " &
                                      " ORDER BY Nomor_ID ", KoneksiDatabaseTransaksi_Alternatif)
                dr = cmd.ExecuteReader
                Do While dr.Read
                    NomorIdBayar = dr.Item("Nomor_ID")
                    TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                    Referensi = dr.Item("Nomor_KK")
                    JumlahBayar = dr.Item("Jumlah_Bayar")
                    TotalBayar += JumlahBayar
                    KeteranganBayar = dr.Item("Catatan")
                    If TahunTelusurPembayaran = TahunBukuAktif Then
                        NomorJVBayar = dr.Item("Nomor_JV")
                    Else
                        NomorJVBayar = 0
                    End If
                    datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJVBayar)
                    If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                        If TahunTelusurPembayaran = TahunBukuAktif Then
                            'datatabelBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaTegas
                        Else
                            'datatabelBayar.Rows(Index_BarisTabelPembayaran).DefaultCellStyle.ForeColor = WarnaPudar
                        End If
                    End If
                    If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
                    Index_BarisTabelPembayaran += 1
                Loop
                TutupDatabaseTransaksi_Alternatif()
            End If
            If TotalBayar >= JumlahTagihan_Terseleksi Then Exit Do
            PencegahLoopingTahunTelusurDataLampau += 1
            TahunTelusurPembayaran += 1
        Loop

        'BukaDatabaseTransaksi_Alternatif(TahunTelusurData)
        'cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
        '                      " WHERE Nomor_BP      = '" & NomorBPH_Terseleksi & "' " &
        '                      " AND Status_Invoice  = '" & Status_Dibayar & "' " &
        '                      " ORDER BY Nomor_ID ",
        '                      KoneksiDatabaseTransaksi_Alternatif)
        'dr_ExecuteReader()
        'Do While dr.Read
        '    NomorIdBayar = dr.Item("Nomor_ID")
        '    TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
        '    Referensi = dr.Item("Nomor_KK")
        '    JumlahBayar = dr.Item("Jumlah_Bayar")
        '    KeteranganBayar = dr.Item("Catatan")
        '    NomorJVBayar = dr.Item("Nomor_JV")
        '    datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJVBayar)
        '    TotalBayar += JumlahBayar
        'Loop
        'TutupDatabaseTransaksi_Alternatif()

        BersihkanSeleksiTabelPembayaran()

        txt_SaldoAwalPerBaris.Text = JumlahTagihan_Terseleksi
        txt_JumlahBayarPerBaris.Text = TotalBayar                               '(Sebaiknya tidak menggunakan variabel TotalBayar_Terseleksi. Agar lebih update).
        txt_SaldoAkhirPerbaris.Text = JumlahTagihan_Terseleksi - TotalBayar '(Sebaiknya tidak menggunakan variabel SisaHutang_Terseleksi. Agar lebih update).

        If SisaPembayaran_Terseleksi > 0 Then
            lbl_SaldoAkhirPerBaris.Visibility = Visibility.Visible
            txt_SaldoAkhirPerbaris.Visibility = Visibility.Visible
            txt_SaldoAkhirPerbaris.Foreground = clrWarning
            lbl_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Visibility = Visibility.Collapsed
            txt_KeteranganLunas.Text = StatusLunas_BelumLunas
            txt_KeteranganLunas.Foreground = clrWarning
        Else
            lbl_SaldoAkhirPerBaris.Visibility = Visibility.Collapsed
            txt_SaldoAkhirPerbaris.Visibility = Visibility.Collapsed
            txt_SaldoAkhirPerbaris.Foreground = clrTeksPrimer
            lbl_KeteranganLunas.Visibility = Visibility.Visible
            txt_KeteranganLunas.Visibility = Visibility.Visible
            txt_KeteranganLunas.Text = StatusLunas_Lunas
            txt_KeteranganLunas.Foreground = clrTeksPrimer
        End If

    End Sub



    Sub BersihkanSeleksiTabelPembayaran()
        BarisBayar_Terseleksi = -1
        datagridBayar.SelectedIndex = -1
        datagridBayar.SelectedItem = Nothing
        datagridBayar.SelectedCells.Clear()
        JumlahBarisBayar = datatabelBayar.Rows.Count
        VisibilitasTabelPembayaran()
        KetersediaanTombolJurnal(False)
        btn_EditBayar.IsEnabled = False
        btn_HapusBayar.IsEnabled = False
        NomorJV_Pembayaran_Terseleksi = 0
    End Sub



    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        If SisaPembayaran_Terseleksi <= 0 Then
            Pesan_Informasi("Tagihan BPJS Kesehatan bulan " & Bulan_Terseleksi & " sudah dibayar seluruhnya.")
            Return
        End If

        Dim Pesan As String = Kosongan

        'Jika Selisih Plus :
        If Selisih_Terseleksi > 0 Then
            Pesan =
                "Ada selisih sebesar Rp. " & Selisih_Terseleksi & ",-" & Enter2Baris &
                "Untuk bisa menginput pembayaran pada tagihan ini, silakan perbaiki terlebih dahulu data di Buku Pengawasan Gaji sehingga nilai selisih menjadi nol." & Enter2Baris &
                "Lanjutkan proses?"
            If Not TanyaKonfirmasi(Pesan) Then Return
            PenyesuaianSelisih()
            If StatusSuntingDatabase = False Then Return
        End If

        'Jika Selisih Minus :
        If Selisih_Terseleksi < 0 Then
            Pesan =
                "Ada selisih sebesar Rp. " & Selisih_Terseleksi & ",-" & Enter2Baris &
                "Untuk bisa menginput pembayaran pada tagihan ini, silakan perbaiki terlebih dahulu data di Buku Pengawasan Gaji sehingga nilai selisih menjadi nol." & Enter2Baris &
                "Lanjutkan proses?"
            If Not TanyaKonfirmasi(Pesan) Then Return
            PenyesuaianSelisih()
            If win_InputJurnal.JurnalTersimpan = False Then Return
        End If

        Dim Uraian = Kosongan
        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        Select Case JudulForm
            Case JudulForm_HutangBPJSKesehatan
                win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangBPJSKesehatan
                win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_BPJS_KS
                Uraian = "Pembayaran Hutang BPJS Kesehatan " & Bulan_Terseleksi
            Case JudulForm_HutangBPJSKetenagakerjaan
                win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangBPJSKetenagakerjaan
                win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_BPJS_TK
                Uraian = "Pembayaran Hutang BPJS Ketenagakerjaan " & Bulan_Terseleksi
            Case JudulForm_HutangKoperasiKaryawan
                win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangKoperasiKaryawan
                win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_KoperasiKaryawan
                Uraian = "Pembayaran Hutang Koperasi Karyawan " & Bulan_Terseleksi
            Case JudulForm_HutangSerikat
                win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangSerikat
                win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_SerikatPekerja
                Uraian = "Pembayaran Hutang Serikat " & Bulan_Terseleksi
        End Select
        win_InputBuktiPengeluaran.NomorBP = NomorBPH_Terseleksi
        win_InputBuktiPengeluaran.
        datatabelUtama.Rows.Add(1, Kosongan, Kosongan, Uraian, NomorBPH_Terseleksi,
                                JumlahTagihan_Terseleksi, 0, 0, 0, JumlahPembayaran_Terseleksi, SisaPembayaran_Terseleksi,
                                Kosongan, Kosongan, 0, 0, 0,
                                SisaPembayaran_Terseleksi, 0)
        win_InputBuktiPengeluaran.NomorUrutInvoice = 1 'Ini jangan sembarangan dihapus..! Penting..!
        win_InputBuktiPengeluaran.Perhitungan_Tabel()
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.AdaPenyimpanan Then TampilkanData()

    End Sub


    Sub PenyesuaianSelisih()
        Dim COADebet = Kosongan
        Dim COAKredit = Kosongan
        Dim JumlahPenyesuaian = 0
        If Selisih_Terseleksi > 0 Then
            COADebet = KodeTautanCOA_HutangLancarLainnya
            COAKredit = COAHutang
            JumlahPenyesuaian = Selisih_Terseleksi
        End If
        If Selisih_Terseleksi < 0 Then
            COADebet = COAHutang
            COAKredit = KodeTautanCOA_HutangLancarLainnya
            JumlahPenyesuaian = 0 - Selisih_Terseleksi
        End If
        JurnalAdjusment_TanpaKirimTanggal(NamaHalaman, COADebet, COAKredit, JumlahPenyesuaian, JenisJurnal_AdjusmentSelisih)
        Dim KoreksiSelisihebelumnya = 0
        Dim KoreksiSelisihSekarang = 0
        If win_InputJurnal.JurnalTersimpan = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelPengawasan & " " &
                                  " WHERE Bulan = '" & Bulan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            KoreksiSelisihebelumnya = dr.Item("Koreksi_Selisih")
            KoreksiSelisihSekarang = KoreksiSelisihebelumnya - Selisih_Terseleksi
            'PesanUntukProgrammer("Selisih Terseleksi : " & SelisihTagihan_Terseleksi & Enter2Baris &
            '                     "Koreksi Selisih Sebelumnya : " & KoreksiSelisihebelumnya & Enter2Baris &
            '                     "Koreksi Selisih Sekarang : " & KoreksiSelisihSekarang)
            cmd = New OdbcCommand(" UPDATE " & TabelPengawasan & " SET " &
                                  " Koreksi_Selisih = '" & KoreksiSelisihSekarang & "' " &
                                  " WHERE Bulan     = '" & Bulan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
            TampilkanData()
        End If
    End Sub


    Private Sub btn_EditBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub

    Private Sub btn_HapusBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPengeluaran()
    End Sub



    Private Sub txt_SaldoAwalPerBaris_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalPerBaris.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalPerBaris)
    End Sub

    Private Sub txt_JumlahBayarPerBaris_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBayarPerBaris.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahBayarPerBaris)
    End Sub

    Private Sub txt_SaldoAkhirPerbaris_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAkhirPerbaris.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAkhirPerbaris)
    End Sub

    Private Sub txt_KeteranganLunas_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KeteranganLunas.TextChanged

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
    Dim Bulan_ As New DataGridTextColumn
    Dim Nomor_BPH As New DataGridTextColumn
    Dim Jumlah_Tagihan As New DataGridTextColumn
    Dim Jumlah_Potongan As New DataGridTextColumn
    Dim Selisih_Tagihan As New DataGridTextColumn
    Dim Koreksi_Selisih As New DataGridTextColumn
    Dim Selisih_ As New DataGridTextColumn
    Dim Jumlah_Pembayaran As New DataGridTextColumn
    Dim Sisa_Pembayaran As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Bulan_")
        datatabelUtama.Columns.Add("Nomor_BPH")
        datatabelUtama.Columns.Add("Jumlah_Tagihan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Potongan", GetType(Int64))
        datatabelUtama.Columns.Add("Selisih_Tagihan", GetType(Int64))
        datatabelUtama.Columns.Add("Koreksi_Selisih", GetType(Int64))
        datatabelUtama.Columns.Add("Selisih_", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Pembayaran", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Pembayaran", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Bulan_, "Bulan_", "Bulan", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPH, "Nomor_BPH", "Nomor BPH", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Tagihan, "Jumlah_Tagihan", "Jumlah Tagihan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Potongan, "Jumlah_Potongan", "Jumlah Potongan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Selisih_Tagihan, "Selisih_Tagihan", "Selisih Tagihan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Koreksi_Selisih, "Koreksi_Selisih", "Koreksi Selisih", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Selisih_, "Selisih_", "Selisih", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Pembayaran, "Jumlah_Pembayaran", "Jumlah" & Enter1Baris & "Pembayaran", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Pembayaran, "Sisa_Pembayaran", "Sisa" & Enter1Baris & "Pembayaran", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 270, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub

    'Tabel Bayar :
    Public datatabelBayar As DataTable
    Public dataviewBayar As DataView
    Public rowviewBayar As DataRowView
    Public newRowBayar As DataRow
    Public HeaderKolomBayar As DataGridColumnHeader
    Public KolomTerseleksiBayar As DataGridColumn
    Public BarisBayar_Terseleksi As Integer
    Public JumlahBarisBayar As Integer

    Dim Nomor_ID_Bayar As New DataGridTextColumn
    Dim Tanggal_Bayar As New DataGridTextColumn
    Dim Referensi_ As New DataGridTextColumn
    Dim Nominal_Bayar As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar()

        datatabelBayar = New DataTable
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_, "Referensi_", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        Buat_DataTabelBayar()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_SaldoBerdasarkanList.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.IsReadOnly = True
        txt_SelisihSaldo.IsReadOnly = True
        txt_SaldoAwalPerBaris.IsReadOnly = True
        txt_JumlahBayarPerBaris.IsReadOnly = True
        txt_SaldoAkhirPerbaris.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_AJP.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
        lbl_KeteranganLunas.Visibility = Visibility.Collapsed
        txt_KeteranganLunas.Visibility = Visibility.Collapsed
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
        pnl_SidebarKiri.Height = TinggiKonten
        pnl_SidebarKanan.Height = TinggiKonten
        pnl_Footer.Width = LebarKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub




    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================

    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean
    Public KesesuaianJurnal As Boolean

    Dim SaldoAwal_BerdasarkanList As Int64
    Dim SaldoAwal_BerdasarkanCOA As Int64
    Dim SaldoAwal_BerdasarkanCOA_PlusPenyesuaian As Int64
    Dim SaldoAkhir_BerdasarkanList As Int64
    Dim SaldoAkhir_BerdasarkanCOA As Int64
    Dim JumlahPenyesuaianSaldo As Int64

    Sub AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAHutang, SaldoAwal_BerdasarkanCOA, JumlahPenyesuaianSaldo, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian,
                                                                  txt_SaldoAwalBerdasarkanCOA, txt_AJP, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAHutang, SaldoAkhir_BerdasarkanCOA, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub CekKesesuaianSaldoAwal()
        CekKesesuaianSaldoAwal_Public(SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian, KesesuaianSaldoAwal,
                                      btn_Sesuaikan, txt_SaldoBerdasarkanList, txt_saldoBerdasarkanCOA_PlusPenyesuaian, txt_SelisihSaldo)
    End Sub

    Sub CekKesesuaianSaldoAkhir()
        CekKesesuaianSaldoAkhir_Public(SaldoAkhir_BerdasarkanList, SaldoAkhir_BerdasarkanCOA, KesesuaianSaldoAkhir,
                                      btn_Sesuaikan, txt_SaldoBerdasarkanList, txt_saldoBerdasarkanCOA_PlusPenyesuaian, txt_SelisihSaldo)
    End Sub

    Private Sub txt_SaldoBerdasarkanList_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoBerdasarkanList.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoBerdasarkanList)
    End Sub

    Private Sub txt_SaldoBerdasarkanCOA_PlusPenyesuaian_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_saldoBerdasarkanCOA_PlusPenyesuaian.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Private Sub txt_SelisihSaldo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SelisihSaldo.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SelisihSaldo)
    End Sub

    Private Sub btn_Sesuaikan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Sesuaikan.Click
        SesuaikanSaldoAwal(NamaHalaman, COAHutang, SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian)
        If win_InputJurnal.JurnalTersimpan = True Then RefreshTampilanData()
    End Sub

    Private Sub txt_SaldoAwalBerdasarkanCOA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalBerdasarkanCOA.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalBerdasarkanCOA)
    End Sub

    Private Sub txt_AJP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AJP.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_AJP)
    End Sub

    Private Sub txt_TotalTabel_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalTabel.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_TotalTabel)
    End Sub



    '=======================================================================================================================================
    '================================================== KOLEKSI ELEMEN KESESUAIAN SALDO ====================================================
    '=======================================================================================================================================

End Class
