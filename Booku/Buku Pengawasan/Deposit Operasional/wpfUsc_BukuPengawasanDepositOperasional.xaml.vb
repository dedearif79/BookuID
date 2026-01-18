Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks
Imports bcomm

Public Class wpfUsc_BukuPengawasanDepositOperasional

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Dim EksekusiTampilanData As Boolean

    Public JudulForm As String
    Public NamaHalaman
    Public COAPiutang
    Dim Total_SisaPiutang As Int64
    Dim TotalTabel As Int64
    Dim QueryTampilan
    Dim QueryTampilanPiutangTahunLalu As String
    Dim QueryTampilanPiutangTahunAktif As String

    Dim NomorUrut
    Dim AngkaBPDO_Sebelumnya
    Dim AngkaBPDO
    Dim NomorBPDO
    Dim NomorBukti
    Dim TanggalBukti
    Dim NomorFakturPajak
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim KodeCustomer
    Dim NamaCustomer
    Dim NamaProduk
    Dim JumlahTransaksi
    Dim JumlahTalangan
    Dim SisaTalangan
    Dim JumlahReimburse
    Dim PotonganReimburse
    Dim JumlahOutstanding
    Dim Keterangan
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim AngkaBPDO_Terseleksi
    Dim NomorBPDO_Terseleksi
    Dim NomorBukti_Terseleksi
    Dim TanggalBukti_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim JumlahTalangan_Terseleksi
    Dim SisaTalangan_Terseleksi
    Dim JumlahReimburse_Terseleksi
    Dim PotonganReimburse_Terseleksi
    Dim JumlahOutstanding_Terseleksi
    Dim Keterangan_Terseleksi As String
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi

    Dim TermasukPiutangTahunIni_Terseleksi As Boolean
    Dim JumlahReimburse_TahunLalu As Int64
    Dim JumlahTalangan_TahunLalu As Int64
    Dim JumlahOutStanding_TahunLalu As Int64

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = JudulForm
        pnl_FilterData.Visibility = Visibility.Collapsed
        VisibilitasTombolJurnal(True)

        VisibilitasInfoSaldo(True)

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub

    Sub RefreshTampilanData()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Async Sub TampilkanDataAsync()

        ' Guard clause
        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            KesesuaianJurnal = True

            'Style Tabel :
            Terabas()
            datatabelUtama.Rows.Clear()

            QueryTampilanPiutangTahunLalu =
                " SELECT * FROM tbl_DepositOperasional " &
                " WHERE (Tanggal_Bukti <  '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') "
            QueryTampilanPiutangTahunAktif =
                " SELECT * FROM tbl_DepositOperasional " &
                " WHERE (Tanggal_Bukti >= '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') "

            'Data Tabel :
            NomorUrut = 0
            SaldoAwal_BerdasarkanList = 0
            Total_SisaPiutang = 0

            'Data Tabel Deposit Operasional Tahun Lalu :
            QueryTampilan = QueryTampilanPiutangTahunLalu
            Await DataTabelAsync()

            'Data Tabel Deposit Operasional Tahun Buku Aktif :
            QueryTampilan = QueryTampilanPiutangTahunAktif
            Await DataTabelAsync()

            TotalTabel = Total_SisaPiutang

            Select Case JenisTahunBuku
                Case JenisTahunBuku_LAMPAU
                    SaldoAkhir_BerdasarkanList = Total_SisaPiutang
                    txt_SaldoBerdasarkanList.Text = SaldoAkhir_BerdasarkanList
                    AmbilValue_SaldoAkhirBerdasarkanCOA()
                    CekKesesuaianSaldoAkhir()
                    txt_SelisihSaldo.Text = SaldoAkhir_BerdasarkanList - SaldoAkhir_BerdasarkanCOA
                Case JenisTahunBuku_NORMAL
                    txt_SaldoBerdasarkanList.Text = SaldoAwal_BerdasarkanList
                    AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian()
                    CekKesesuaianSaldoAwal()
                    txt_SelisihSaldo.Text = SaldoAwal_BerdasarkanList - SaldoAwal_BerdasarkanCOA_PlusPenyesuaian
                    txt_TotalTabel.Text = TotalTabel
            End Select

            lbl_TotalTabel.Text = "Saldo Akhir Deposit Operasional : "

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPengawasanDepositOperasional")
            SedangMemuatData = False

        Finally
            BersihkanSeleksi_SetelahLoading()
        End Try

    End Sub

    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub

    Async Function DataTabelAsync() As Task

        AngkaBPDO_Sebelumnya = 0
        Dim i = 0
        NamaProduk = Kosongan

        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabase = False Then Exit Function

        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Angka_BPDO ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            AngkaBPDO = dr.Item("Angka_BPDO")
            If AngkaBPDO <> AngkaBPDO_Sebelumnya And i > 0 Then TambahBaris()
            NomorBPDO = dr.Item("Nomor_BPDO")
            NomorBukti = dr.Item("Nomor_Bukti")
            TanggalBukti = TanggalFormatTampilan(dr.Item("Tanggal_Bukti"))
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            KodeCustomer = dr.Item("Kode_Customer")
            NamaCustomer = dr.Item("Nama_Customer")
            If NamaProduk = Kosongan Then
                NamaProduk = dr.Item("Nama_Produk")
            Else
                NamaProduk &= SlashGanda_Pemisah & dr.Item("Nama_Produk")
            End If
            JumlahTransaksi = dr.Item("Jumlah_Transaksi")
            'Algoritma Talangan : -----------------------------------------------------------------------------------------------------
            JumlahTalangan_TahunLalu = 0
            JumlahTalangan = 0
            cmdBAYAR = New OdbcCommand(" SELECT Tanggal_KK, Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                       " WHERE Nomor_BP     = '" & NomorBPDO & "' " &
                                       " AND Status_Invoice = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                If drBAYAR.Item("Tanggal_KK") < TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahTalangan_TahunLalu += drBAYAR.Item("Jumlah_Bayar")
                End If
                If drBAYAR.Item("Tanggal_KK") >= TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahTalangan += drBAYAR.Item("Jumlah_Bayar")
                End If
            Loop
            JumlahTalangan = JumlahTalangan_TahunLalu + JumlahTalangan
            SisaTalangan = JumlahTransaksi - JumlahTalangan
            'Algoritma Reimburse : ----------------------------------------------------------------------------------------------------
            JumlahReimburse_TahunLalu = 0
            JumlahReimburse = 0
            cmdBAYAR = New OdbcCommand(" SELECT Tanggal_KM, Jumlah_Bayar FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP = '" & NomorBPDO & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                If drBAYAR.Item("Tanggal_KM") < TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahReimburse_TahunLalu += drBAYAR.Item("Jumlah_Bayar")
                End If
                If drBAYAR.Item("Tanggal_KM") >= TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahReimburse += drBAYAR.Item("Jumlah_Bayar")
                End If
            Loop
            JumlahReimburse = JumlahReimburse_TahunLalu + JumlahReimburse
            PotonganReimburse = dr.Item("Potongan_Reimburse")
            JumlahOutstanding = JumlahTalangan - JumlahReimburse - PotonganReimburse
            JumlahOutStanding_TahunLalu = JumlahTalangan_TahunLalu - JumlahReimburse_TahunLalu
            '--------------------------------------------------------------------------------------------------------------------------
            Keterangan = PenghapusEnter(dr.Item("Keterangan"))
            NomorJV = dr.Item("Nomor_JV")
            User = dr.Item("User")
            AngkaBPDO_Sebelumnya = AngkaBPDO
            i += 1
            Await Task.Yield()
        Loop

        If AngkaBPDO_Sebelumnya <> 0 Then TambahBaris()

        AksesDatabase_Transaksi(Tutup)

    End Function

    Sub TambahBaris()
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, AngkaBPDO_Sebelumnya, NomorBPDO, NomorBukti, TanggalBukti, NomorFakturPajak,
                                KodeLawanTransaksi, NamaLawanTransaksi, KodeCustomer, NamaCustomer, NamaProduk,
                                JumlahTransaksi, JumlahTalangan, SisaTalangan, JumlahReimburse, PotonganReimburse, JumlahOutstanding,
                                Keterangan, NomorJV, User)
        NamaProduk = Kosongan
        Total_SisaPiutang += JumlahOutstanding
        If QueryTampilan = QueryTampilanPiutangTahunLalu Then SaldoAwal_BerdasarkanList += (JumlahOutStanding_TahunLalu)
        Terabas()
    End Sub

    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_LihatJurnal.IsEnabled = False
        btn_Cetak.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        VisibilitasInfoSaldo(True)
        SedangMemuatData = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        brd_LihatJurnal.Visibility = Visibility.Collapsed
        btn_LihatJurnal.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                brd_LihatJurnal.Visibility = Visibility.Visible
                btn_LihatJurnal.Visibility = Visibility.Visible
            End If
        End If
    End Sub

    Sub VisibilitasInfoSaldo(Visibilitas As Boolean)
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_TotalTabel.Visibility = Visibility.Collapsed
        If Visibilitas Then
            grb_InfoSaldo.Visibility = Visibility.Visible
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                pnl_SaldoAwalPlusAJP.Visibility = Visibility.Visible
                pnl_TotalTabel.Visibility = Visibility.Visible
            End If
        Else
            grb_InfoSaldo.Visibility = Visibility.Collapsed
            pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
            pnl_TotalTabel.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTabelPembayaran()
        If JumlahBarisBayar > 0 Then
            datagridBayar.Visibility = Visibility.Visible
        Else
            datagridBayar.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub VisibilitasTabelPencairan()
        If JumlahBarisCair > 0 Then
            datagridCair.Visibility = Visibility.Visible
        Else
            datagridCair.Visibility = Visibility.Collapsed
        End If
    End Sub

    Sub KetersediaanTombolJurnal(Tersedia As Boolean)
        btn_LihatJurnal.IsEnabled = False
        If Tersedia Then
            If TermasukPiutangTahunIni_Terseleksi And (NomorJVBayar_Terseleksi > 0 Or NomorJVCair_Terseleksi > 0) Then btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi > 0 Then LihatJurnal(NomorJV_Terseleksi)
        If NomorJVBayar_Terseleksi > 0 Then LihatJurnal(NomorJVBayar_Terseleksi)
        If NomorJVCair_Terseleksi > 0 Then LihatJurnal(NomorJVCair_Terseleksi)
    End Sub


    Private Sub btn_Cetak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Cetak.Click
        Cetak(JenisFormCetak_NotaDebet, AngkaBPDO_Terseleksi, True, False)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputDepositOperasional = New wpfWin_InputDepositOperasional
        win_InputDepositOperasional.ResetForm()
        win_InputDepositOperasional.FungsiForm = FungsiForm_TAMBAH
        win_InputDepositOperasional.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        If JumlahTalangan_Terseleksi > 0 Then
            Pesan_Peringatan("Sudah ada pembayaran terkait data ini. Data tidak dapat dihapus/edit.")
            Return
        End If

        win_InputDepositOperasional = New wpfWin_InputDepositOperasional
        win_InputDepositOperasional.ResetForm()
        win_InputDepositOperasional.FungsiForm = FungsiForm_EDIT
        win_InputDepositOperasional.AngkaBPDO = AngkaBPDO_Terseleksi
        win_InputDepositOperasional.NomorJV = NomorJV_Terseleksi
        win_InputDepositOperasional.txt_NomorBPDO.Text = NomorBPDO_Terseleksi
        win_InputDepositOperasional.txt_NomorBukti.Text = NomorBukti_Terseleksi
        win_InputDepositOperasional.dtp_TanggalBukti.SelectedDate = TanggalFormatWPF(TanggalBukti_Terseleksi)
        win_InputDepositOperasional.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        win_InputDepositOperasional.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        win_InputDepositOperasional.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi_Terseleksi
        win_InputDepositOperasional.txt_KodeCustomer.Text = KodeCustomer_Terseleksi
        win_InputDepositOperasional.txt_NamaCustomer.Text = NamaCustomer_Terseleksi
        IsiValueElemenRichTextBox(win_InputDepositOperasional.txt_Keterangan, Keterangan_Terseleksi)
        win_InputDepositOperasional.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If JumlahTalangan_Terseleksi > 0 Then
            Pesan_Peringatan("Sudah ada pembayaran terkait data ini. Data tidak dapat dihapus/edit.")
            Return
        End If

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_DepositOperasional " &
                              " WHERE Nomor_BPDO = '" & NomorBPDO_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
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
        AngkaBPDO_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Angka_BPDO").ToString)
        NomorBPDO_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_BPDO")
        NomorBukti_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Bukti")
        TanggalBukti_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Bukti")
        NomorFakturPajak_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Faktur_Pajak")
        KodeLawanTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Lawan_Transaksi")
        NamaLawanTransaksi_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Lawan_Transaksi")
        KodeCustomer_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Customer")
        NamaCustomer_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Customer")
        JumlahTransaksi_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Transaksi"))
        JumlahTalangan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Talangan"))
        SisaTalangan_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Sisa_Talangan"))
        JumlahReimburse_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Reimburse"))
        PotonganReimburse_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Potongan_Reimburse"))
        JumlahOutstanding_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Outstanding"))
        Keterangan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Keterangan_")
        NomorJV_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_JV"))
        User_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "User_")

        If NomorUrut_Terseleksi > 0 Then
            TampilkanSidebarKanan()
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
            btn_Cetak.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
            btn_Cetak.IsEnabled = False
        End If

        Dim TanggalBukti_Date As Date = TanggalBukti_Terseleksi
        Dim Tahun_TanggalBukti As Integer = AmbilAngka(TanggalBukti_Date.Year)

        If Tahun_TanggalBukti <> TahunBukuAktif Then
            btn_Edit.IsEnabled = False
        End If

        NomorJVBayar_Terseleksi = 0
        NomorJVCair_Terseleksi = 0

        If NomorJV_Terseleksi > 0 Then
            btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If Left(e.Row.Item("Nomor_BPDO"), PanjangTeks_AwalanBPDO_PlusTahunBuku) = AwalanBPDO_PlusTahunBuku Then
                If AmbilAngka(e.Row.Item("Nomor_JV")) > 0 Then
                    e.Row.Foreground = clrTeksPrimer
                Else
                    e.Row.Foreground = clrError
                End If
            Else
                e.Row.Foreground = clrDataTahunLalu
            End If
        Else
            e.Row.Foreground = clrTeksPrimer
        End If
    End Sub


    Sub TampilkanSidebarKanan()
        TampilkanData_Pembayaran()
        TampilkanData_Pencairan()
        pnl_SidebarKanan.Visibility = Visibility.Visible
    End Sub

    Dim NomorJVBayar_Terseleksi
    Dim NomorIdBayar_Terseleksi
    Dim ReferensiBayar_Terseleksi
    Sub TampilkanData_Pembayaran()

        datatabelBayar.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP = '" & NomorBPDO_Terseleksi & "' " &
                              " AND Status_Invoice = '" & Status_Dibayar & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim TotalBayar As Int64 = 0
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_KK"))
            Dim Referensi = dr.Item("Nomor_KK")
            Dim JumlahBayar As Int64 = dr.Item("Jumlah_Bayar")
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJVBayar)
            TotalBayar += JumlahBayar
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksiPembayaran()

        TampilkanData_Pencairan()

    End Sub

    Sub BersihkanSeleksiPembayaran()
        BersihkanSeleksi_WPF(datagridBayar, datatabelBayar, BarisBayar_Terseleksi, JumlahBarisBayar)
        VisibilitasTabelPembayaran()
        KetersediaanTombolJurnal(False)
        btn_EditBayar.IsEnabled = False
        btn_HapusBayar.IsEnabled = False
        NomorJVBayar_Terseleksi = 0
    End Sub


    Private Sub datagridBayar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBayar.SelectionChanged
    End Sub
    Private Sub datagridBayar_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.PreviewMouseLeftButtonUp
        HeaderKolomBayar = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomBayar IsNot Nothing Then
            BersihkanSeleksiPembayaran()
        End If
    End Sub
    Private Sub datagridBayar_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBayar.SelectedCellsChanged

        KolomBayar_Terseleksi = datagridBayar.CurrentColumn
        BarisBayar_Terseleksi = datagridBayar.SelectedIndex
        If BarisBayar_Terseleksi < 0 Then Return
        rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
        If Not rowviewBayar IsNot Nothing Then Return

        NomorIdBayar_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewBayar, "Nomor_ID_Bayar").ToString)
        NomorJVBayar_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewBayar, "Nomor_JV_Bayar").ToString)
        NomorJV_Terseleksi = 0
        NomorJVCair_Terseleksi = 0
        ReferensiBayar_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewBayar, "Referensi_Bayar")
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditBayar.IsEnabled = True
            btn_HapusBayar.IsEnabled = True
        Else
            BersihkanSeleksiPembayaran()
        End If
        If AmbilAngka(NomorJVBayar_Terseleksi) = 0 Then KetersediaanTombolJurnal(False)
    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
    End Sub


    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click
        If SisaTalangan_Terseleksi <= 0 Then
            Pesan_Informasi("Data Talangan ini sudah dibayar semua.")
            Return
        End If
        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PengeluaranTunai
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_DepositOperasional
        win_InputBuktiPengeluaran.NomorBP = NomorBPDO_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If JenisTahunBuku = JenisTahunBuku_NORMAL And win_InputBuktiPengeluaran.AdaPenyimpanan And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then TampilkanData()
        If JenisTahunBuku = JenisTahunBuku_LAMPAU And win_InputBuktiPengeluaran.AdaPenyimpanan Then TampilkanData()
    End Sub

    Private Sub btn_EditBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditBayar.Click
        FiturBelumBisaDigunakan()
    End Sub

    Private Sub btn_HapusBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusBayar.Click
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            If Not TanyaKonfirmasi("Yakin ingin menghapus data pembayaran terpilih?") Then Return
            HapusDataPengeluaran_BerdasarkanNomorID(NomorIdBayar_Terseleksi)
            If StatusSuntingDatabase Then
                Pesan_Sukses("Data pembayaran berhasil dihapus.")
                TampilkanData()
            Else
                Pesan_Peringatan("Data pembayaran gagal dihapus.")
            End If
        Else
            FiturBelumBisaDigunakan()
            Return
            'Pilihan = MessageBox.Show("Yakin akan menghapus data pembayaran terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
            'If Pilihan = vbNo Then Return
            'HapusDataPengeluaran_BerdasarkanNomorID(NomorIdBayar_Terseleksi)
            'HapusJurnal_BerdasarkanNomorJV(NomorJVBayar_Terseleksi)
            'If StatusSuntingDatabase Then
            '    PesanPemberitahuan("Data pembayaran berhasil dihapus")
            '    TampilkanData()
            'Else
            '    PesanPemberitahuan("Data pembayaran gagal dihapus")
            'End If
        End If
    End Sub



    Dim NomorJVCair_Terseleksi
    Dim NomorIdCair_Terseleksi
    Dim ReferensiCair_Terseleksi
    Sub TampilkanData_Pencairan()

        datatabelCair.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                              " WHERE Nomor_BP = '" & NomorBPDO_Terseleksi & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim TotalCair As Int64 = 0
        Do While dr.Read
            Dim NomorIdCair = dr.Item("Nomor_ID")
            Dim TanggalCair = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
            Dim Referensi = dr.Item("Nomor_KM")
            Dim JumlahCair As Int64 = dr.Item("Jumlah_Bayar")
            Dim KeteranganCair = dr.Item("Catatan")
            Dim NomorJVCair = dr.Item("Nomor_JV")
            datatabelCair.Rows.Add(NomorIdCair, TanggalCair, Referensi, JumlahCair, KeteranganCair, NomorJVCair)
            TotalCair += JumlahCair
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksiPencairan()

    End Sub

    Sub BersihkanSeleksiPencairan()
        BersihkanSeleksi_WPF(datagridCair, datatabelCair, BarisCair_Terseleksi, JumlahBarisCair)
        VisibilitasTabelPencairan()
        KetersediaanTombolJurnal(False)
        btn_EditCair.IsEnabled = False
        btn_HapusCair.IsEnabled = False
        NomorJVCair_Terseleksi = 0
    End Sub


    Private Sub datagridCair_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridCair.SelectionChanged
    End Sub
    Private Sub datagridCair_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridCair.PreviewMouseLeftButtonUp
        HeaderKolomCair = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomCair IsNot Nothing Then
            BersihkanSeleksiPencairan()
        End If
    End Sub
    Private Sub datagridCair_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridCair.SelectedCellsChanged

        KolomCair_Terseleksi = datagridCair.CurrentColumn
        BarisCair_Terseleksi = datagridCair.SelectedIndex
        If BarisCair_Terseleksi < 0 Then Return
        rowviewCair = TryCast(datagridCair.SelectedItem, DataRowView)
        If Not rowviewCair IsNot Nothing Then Return

        NomorIdCair_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewCair, "Nomor_ID_Cair").ToString)
        NomorJVCair_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewCair, "Nomor_JV_Cair").ToString)
        NomorJV_Terseleksi = 0
        NomorJVBayar_Terseleksi = 0
        ReferensiCair_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewCair, "Referensi_Cair")
        If BarisCair_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditCair.IsEnabled = True
            btn_HapusCair.IsEnabled = True
        Else
            BersihkanSeleksiPencairan()
        End If
        If AmbilAngka(NomorJVCair_Terseleksi) = 0 Then KetersediaanTombolJurnal(False)
    End Sub
    Private Sub datagridCair_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridCair.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
    End Sub


    Private Sub btn_InputCair_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputCair.Click
        If JumlahTalangan_Terseleksi <= 0 Then
            Pesan_Informasi("Silakan isi data pembayaran talangan terlebih dahulu.")
            Return
        End If
        If JumlahOutstanding_Terseleksi <= 0 Then
            Pesan_Informasi("Data Reimburse ini sudah dicairkan semua.")
            Return
        End If
        win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PenerimaanTunai
        win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_DepositOperasional
        win_InputBuktiPenerimaan.NomorBP = NomorBPDO_Terseleksi
        win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeCustomer_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPenerimaan.ShowDialog()
        If win_InputBuktiPenerimaan.AdaPenyimpanan Then TampilkanData()
    End Sub

    Private Sub btn_EditCair_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditCair.Click
        FiturBelumBisaDigunakan()
    End Sub

    Private Sub btn_HapusCair_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusCair.Click
        FiturBelumBisaDigunakan()
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
    Dim Angka_BPDO As New DataGridTextColumn
    Dim Nomor_BPDO As New DataGridTextColumn
    Dim Nomor_Bukti As New DataGridTextColumn
    Dim Tanggal_Bukti As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim Kode_Customer As New DataGridTextColumn
    Dim Nama_Customer As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Jumlah_Transaksi As New DataGridTextColumn
    Dim Jumlah_Talangan As New DataGridTextColumn
    Dim Sisa_Talangan As New DataGridTextColumn
    Dim Jumlah_Reimburse As New DataGridTextColumn
    Dim Potongan_Reimburse As New DataGridTextColumn
    Dim Jumlah_Outstanding As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn
    Dim User_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Angka_BPDO")
        datatabelUtama.Columns.Add("Nomor_BPDO")
        datatabelUtama.Columns.Add("Nomor_Bukti")
        datatabelUtama.Columns.Add("Tanggal_Bukti")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi")
        datatabelUtama.Columns.Add("Kode_Customer")
        datatabelUtama.Columns.Add("Nama_Customer")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Jumlah_Transaksi", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Talangan", GetType(Int64))
        datatabelUtama.Columns.Add("Sisa_Talangan", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Reimburse", GetType(Int64))
        datatabelUtama.Columns.Add("Potongan_Reimburse", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Outstanding", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV", GetType(Int64))
        datatabelUtama.Columns.Add("User_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angka_BPDO, "Angka_BPDO", "Angka BPDO", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPDO, "Nomor_BPDO", "Nomor BPDO", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Bukti, "Nomor_Bukti", "Nomor Bukti", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Bukti, "Tanggal_Bukti", "Tanggal Bukti", 87, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "No. Faktur Pajak", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode Lawan Transaksi", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Nama Lawan Transaksi", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Customer, "Kode_Customer", "Kode Customer", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Customer, "Nama_Customer", "Nama Customer", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Transaksi, "Jumlah_Transaksi", "Jumlah Transaksi", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Talangan, "Jumlah_Talangan", "Jumlah Talangan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sisa_Talangan, "Sisa_Talangan", "Sisa Talangan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Reimburse, "Jumlah_Reimburse", "Jumlah Reimburse", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Potongan_Reimburse, "Potongan_Reimburse", "Potongan Reimburse", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Outstanding, "Jumlah_Outstanding", "Jumlah Outstanding", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "", 45, FormatString, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, User_, "User_", "User", 87, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub




    'Tabel Bayar :
    Public datatabelBayar As DataTable
    Public dataviewBayar As DataView
    Public rowviewBayar As DataRowView
    Public newRowBayar As DataRow
    Public HeaderKolomBayar As DataGridColumnHeader
    Public KolomBayar_Terseleksi As DataGridColumn
    Public BarisBayar_Terseleksi As Integer
    Public JumlahBarisBayar As Integer

    Dim Nomor_ID_Bayar As New DataGridTextColumn
    Dim Tanggal_Bayar As New DataGridTextColumn
    Dim Referensi_Bayar As New DataGridTextColumn
    Dim Nominal_Bayar As New DataGridTextColumn
    Dim Keterangan_Bayar As New DataGridTextColumn
    Dim Nomor_JV_Bayar As New DataGridTextColumn

    Sub Buat_DataTabelBayar()

        datatabelBayar = New DataTable
        datatabelBayar.Columns.Add("Nomor_ID_Bayar")
        datatabelBayar.Columns.Add("Tanggal_Bayar")
        datatabelBayar.Columns.Add("Referensi_Bayar")
        datatabelBayar.Columns.Add("Nominal_Bayar", GetType(Int64))
        datatabelBayar.Columns.Add("Keterangan_Bayar")
        datatabelBayar.Columns.Add("Nomor_JV_Bayar")

        StyleTabelPembantu_WPF(datagridBayar, datatabelBayar, dataviewBayar)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_ID_Bayar, "Nomor_ID_Bayar", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Tanggal_Bayar, "Tanggal_Bayar", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Referensi_Bayar, "Referensi_Bayar", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nominal_Bayar, "Nominal_Bayar", "Jumlah Bayar", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Keterangan_Bayar, "Keterangan_Bayar", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBayar, Nomor_JV_Bayar, "Nomor_JV_Bayar", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub




    'Tabel Cair :
    Public datatabelCair As DataTable
    Public dataviewCair As DataView
    Public rowviewCair As DataRowView
    Public newRowCair As DataRow
    Public HeaderKolomCair As DataGridColumnHeader
    Public KolomCair_Terseleksi As DataGridColumn
    Public BarisCair_Terseleksi As Integer
    Public JumlahBarisCair As Integer


    Dim Nomor_ID_Cair As New DataGridTextColumn
    Dim Tanggal_Cair As New DataGridTextColumn
    Dim Referensi_Cair As New DataGridTextColumn
    Dim Nominal_Cair As New DataGridTextColumn
    Dim Keterangan_Cair As New DataGridTextColumn
    Dim Nomor_JV_Cair As New DataGridTextColumn

    Sub Buat_DataTabelCair()

        datatabelCair = New DataTable
        datatabelCair.Columns.Add("Nomor_ID_Cair")
        datatabelCair.Columns.Add("Tanggal_Cair")
        datatabelCair.Columns.Add("Referensi_Cair")
        datatabelCair.Columns.Add("Nominal_Cair", GetType(Int64))
        datatabelCair.Columns.Add("Keterangan_Cair")
        datatabelCair.Columns.Add("Nomor_JV_Cair")

        StyleTabelPembantu_WPF(datagridCair, datatabelCair, dataviewCair)
        TambahkanKolomTextBoxDataGrid_WPF(datagridCair, Nomor_ID_Cair, "Nomor_ID_Cair", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridCair, Tanggal_Cair, "Tanggal_Cair", "Tanggal", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridCair, Referensi_Cair, "Referensi_Cair", "Referensi", 125, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridCair, Nominal_Cair, "Nominal_Cair", "Jumlah Cair", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridCair, Keterangan_Cair, "Keterangan_Cair", "Keterangan", 180, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridCair, Nomor_JV_Cair, "Nomor_JV_Cair", "Nomor JV", 87, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub



    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        Buat_DataTabelBayar()
        Buat_DataTabelCair()
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
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
        AmbilValue_SaldoAwalBerdasarkanCOA_PlusPenyesuaian_Public(COAPiutang, SaldoAwal_BerdasarkanCOA, JumlahPenyesuaianSaldo, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian,
                                                                  txt_SaldoAwalBerdasarkanCOA, txt_AJP, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
    End Sub

    Sub AmbilValue_SaldoAkhirBerdasarkanCOA()
        AmbilValue_SaldoAkhirBerdasarkanCOA_Public(COAPiutang, SaldoAkhir_BerdasarkanCOA, txt_saldoBerdasarkanCOA_PlusPenyesuaian)
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
        SesuaikanSaldoAwal(NamaHalaman, COAPiutang, SaldoAwal_BerdasarkanList, SaldoAwal_BerdasarkanCOA_PlusPenyesuaian)
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
