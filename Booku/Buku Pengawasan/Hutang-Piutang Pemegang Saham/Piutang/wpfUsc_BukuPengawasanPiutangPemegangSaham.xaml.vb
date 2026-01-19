Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks
Imports bcomm

Public Class wpfUsc_BukuPengawasanPiutangPemegangSaham

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Dim EksekusiTampilanData As Boolean

    Public JudulForm As String
    'Public JudulForm_SaldoAwalPiutangPemegangSaham = "Saldo Awal Piutang Pemegang Saham"
    Public JudulForm_SaldoAkhirPiutangPemegangSaham = "Saldo Akhir Piutang Pemegang Saham"
    Public JudulForm_BukuPengawasanPiutangPemegangSaham = "Buku Pengawasan Piutang Pemegang Saham"
    Public NamaHalaman
    Public COAPiutang
    Dim Total_SisaPiutang As Int64
    Dim TotalTabel As Int64
    Dim QueryTampilan
    Dim QueryTampilanPiutangTahunLalu As String
    Dim QueryTampilanPiutangTahunAktif As String


    Dim NomorUrut
    Dim NomorID
    Dim NomorBPPPS
    Dim KodePemegangSaham
    Dim NamaPemegangSaham
    Dim TanggalPinjam
    Dim JumlahPiutang
    Dim SaldoAwalPerBaris
    Dim JumlahAngsuran
    Dim SaldoAkhirPerBaris
    Dim Keterangan
    Dim NomorJV

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPPPS_Terseleksi
    Dim KodePemegangSaham_Terseleksi
    Dim NamaPemegangSaham_Terseleksi
    Dim TanggalPinjam_Terseleksi
    Dim JumlahPiutang_Terseleksi
    Dim SaldoAwalPerBaris_Terseleksi
    Dim JumlahAngsuran_Terseleksi
    Dim SaldoAkhirPerBaris_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi

    Dim NomorIdPembayaran_Terseleksi
    Dim Referensi_Terseleksi
    Dim NomorJV_Pembayaran_Terseleksi

    Dim TermasukPiutangTahunIni_Terseleksi As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        Terabas()

        ProsesLoadingForm = True

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            JudulForm = JudulForm_SaldoAkhirPiutangPemegangSaham
            VisibilitasTombolBukuPembantu(False)
            VisibilitasTombolJurnal(False)
            VisibilitasTombolPosting(False)
            VisibilitasTombolCRUD(True)
            grb_InfoSaldo.Header = "Saldo Akhir :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA"
        End If

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            JudulForm = JudulForm_BukuPengawasanPiutangPemegangSaham
            VisibilitasTombolBukuPembantu(True)
            VisibilitasTombolPosting(True)
            VisibilitasTombolJurnal(True)
            VisibilitasTombolCRUD(True)
            grb_InfoSaldo.Header = "Saldo Awal :"
            lbl_SaldoBerdasarkanList.Text = "Berdasarkan List"
            lbl_SaldoBerdasarkanCOA_PlusAJP.Text = "Berdasarkan COA + AJP"
        End If

        lbl_JudulForm.Text = JudulForm

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
                " SELECT * FROM tbl_PengawasanPiutangPemegangSaham " &
                " WHERE (Tanggal_Transaksi <  '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') "
            QueryTampilanPiutangTahunAktif =
                " SELECT * FROM tbl_PengawasanPiutangPemegangSaham " &
                " WHERE (Tanggal_Transaksi >= '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') "

            'Data Tabel :
            NomorUrut = 0
            SaldoAwal_BerdasarkanList = 0
            Total_SisaPiutang = 0

            'Data Tabel Sisa Piutang Usaha Tahun Lalu :
            QueryTampilan = QueryTampilanPiutangTahunLalu
            Await DataTabelAsync()

            'Data Tabel BPHU Tahun Buku Aktif :
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

            lbl_TotalTabel.Text = "Saldo Akhir Piutang PemegangSaham : "

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPengawasanPiutangPemegangSaham")

        Finally
            BersihkanSeleksi_SetelahLoading()

        End Try

    End Sub

    ' Wrapper untuk backward compatibility
    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub

    Async Function DataTabelAsync() As Task

        AksesDatabase_Transaksi(Buka)
        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPPPS = dr.Item("Nomor_BPPPS")
            KodePemegangSaham = dr.Item("Kode_Lawan_Transaksi")
            NamaPemegangSaham = AmbilValue_NamaPemegangSaham(KodePemegangSaham)
            TanggalPinjam = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            JumlahPiutang = dr.Item("Jumlah_Pinjaman")
            SaldoAwalPerBaris = dr.Item("Saldo_Awal")
            Keterangan = PenghapusEnter(dr.Item("Keterangan"))
            NomorJV = dr.Item("Nomor_JV")

            'Algoritma Pembayaran : ---------------------------------------------------------------------------------------------------
            Dim JumlahAngsuran_TahunLalu = 0
            JumlahAngsuran = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                       " WHERE Nomor_BP = '" & NomorBPPPS & "' ",
            KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                If drBAYAR.Item("Tanggal_KM") < TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahAngsuran_TahunLalu += drBAYAR.Item("Jumlah_Bayar")
                End If
                If drBAYAR.Item("Tanggal_KM") >= TanggalFormatSimpan(AwalTahunBukuAktif) Then
                    JumlahAngsuran += drBAYAR.Item("Jumlah_Bayar")
                End If
            Loop
            JumlahAngsuran = JumlahAngsuran_TahunLalu + JumlahAngsuran
            SaldoAkhirPerBaris = SaldoAwalPerBaris - JumlahAngsuran
            Total_SisaPiutang += SaldoAkhirPerBaris
            If QueryTampilan = QueryTampilanPiutangTahunLalu Then SaldoAwal_BerdasarkanList += (SaldoAwalPerBaris - JumlahAngsuran_TahunLalu)
            '--------------------------------------------------------------------------------------------------------------------------

            datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPPPS, KodePemegangSaham, NamaPemegangSaham, TanggalPinjam,
                                    JumlahPiutang, SaldoAwalPerBaris, JumlahAngsuran, SaldoAkhirPerBaris, Keterangan, NomorJV)

            Await Task.Yield()

        Loop

        AksesDatabase_General(Tutup)
        AksesDatabase_Transaksi(Tutup)

    End Function


    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        KetersediaanTombolPosting(False)
        KetersediaanTombolJurnal(False)
        KetersediaanTombolUpdate(False)
        btn_BukuPembantu.IsEnabled = False
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        NomorJV_Pembayaran_Terseleksi = 0
        VisibilitasInfoSaldo(True)
        BersihkanSeleksiPembayaran()
        SedangMemuatData = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub


    Sub VisibilitasTombolBukuPembantu(Visibilitas As Boolean)
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                brd_BukuPembantu.Visibility = Visibility.Visible
                btn_BukuPembantu.Visibility = Visibility.Visible
            End If
        Else
            brd_BukuPembantu.Visibility = Visibility.Collapsed
            btn_BukuPembantu.Visibility = Visibility.Collapsed
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


    Sub VisibilitasTombolPosting(Visibilitas As Boolean)
        If Visibilitas Then
            brd_Posting.Visibility = Visibility.Visible
            btn_Posting.Visibility = Visibility.Visible
        Else
            brd_Posting.Visibility = Visibility.Collapsed
            btn_Posting.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        If Visibilitas Then
            brd_LihatJurnal.Visibility = Visibility.Visible
            btn_LihatJurnal.Visibility = Visibility.Visible
        Else
            brd_LihatJurnal.Visibility = Visibility.Collapsed
            btn_LihatJurnal.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTombolCRUD(Visibilitas As Boolean)
        If Visibilitas Then
            pnl_CRUD.Visibility = Visibility.Visible
        Else
            pnl_CRUD.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTabelPembayaran()
        If JumlahBarisBayar > 0 Then
            datagridBayar.Visibility = Visibility.Visible
        Else
            datagridBayar.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KetersediaanTombolPosting(Tersedia As Boolean)
        btn_Posting.IsEnabled = False
        If Tersedia Then
            If TermasukPiutangTahunIni_Terseleksi And NomorJV_Terseleksi = 0 Then btn_Posting.IsEnabled = True
        Else
            btn_Posting.IsEnabled = False
        End If
    End Sub

    Sub KetersediaanTombolJurnal(Tersedia As Boolean)
        btn_LihatJurnal.IsEnabled = False
        If Tersedia Then
            If TermasukPiutangTahunIni_Terseleksi And NomorJV_Terseleksi > 0 Then btn_LihatJurnal.IsEnabled = True
        Else
            btn_LihatJurnal.IsEnabled = False
        End If
    End Sub

    Sub KetersediaanTombolUpdate(Tersedia As Boolean)
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        If Tersedia Then
            Select Case JenisTahunBuku
                Case JenisTahunBuku_LAMPAU
                    btn_Edit.IsEnabled = True
                    btn_Hapus.IsEnabled = True
                Case JenisTahunBuku_NORMAL
                    If TermasukPiutangTahunIni_Terseleksi And NomorJV_Terseleksi = 0 Then
                        btn_Edit.IsEnabled = True
                        btn_Hapus.IsEnabled = True
                    End If
            End Select
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
        End If

    End Sub




    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_BukuPembantu_Click(sender As Object, e As RoutedEventArgs) Handles btn_BukuPembantu.Click
        FiturDalamPengembangan()
    End Sub


    Private Sub btn_Posting_Click(sender As Object, e As RoutedEventArgs) Handles btn_Posting.Click
        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PengeluaranTunai
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PiutangPemegangSaham
        win_InputBuktiPengeluaran.NomorBP = NomorBPPPS_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodePemegangSaham_Terseleksi
        win_InputBuktiPengeluaran.TambahkanDataPengeluaranPiutangPemegangSaham()
        win_InputBuktiPengeluaran.ShowDialog()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Terseleksi)
        ElseIf NomorJV_Pembayaran_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Pembayaran_Terseleksi)
        Else
            Pesan_Informasi("Data terpilih belum masuk jurnal.")
            Return
        End If
    End Sub

    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputHutangPiutangPemegangSaham = New wpfWin_InputHutangPiutangPemegangSaham
        win_InputHutangPiutangPemegangSaham.ResetForm()
        win_InputHutangPiutangPemegangSaham.HutangPiutang = hp_Piutang
        win_InputHutangPiutangPemegangSaham.FungsiForm = FungsiForm_TAMBAH
        win_InputHutangPiutangPemegangSaham.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        If NomorJV_Terseleksi > 0 Then
            Pesan_Peringatan("Data terpilih sudah diposting. Tidak dapat diedit/hapus.")
            Return
        End If

        win_InputHutangPiutangPemegangSaham = New wpfWin_InputHutangPiutangPemegangSaham
        win_InputHutangPiutangPemegangSaham.ResetForm()
        ProsesIsiValueForm = True
        win_InputHutangPiutangPemegangSaham.HutangPiutang = hp_Piutang
        win_InputHutangPiutangPemegangSaham.FungsiForm = FungsiForm_EDIT
        win_InputHutangPiutangPemegangSaham.NomorID = NomorID_Terseleksi
        win_InputHutangPiutangPemegangSaham.txt_NomorBP.Text = NomorBPPPS_Terseleksi
        win_InputHutangPiutangPemegangSaham.NomorJV = NomorJV_Terseleksi
        win_InputHutangPiutangPemegangSaham.dtp_TanggalPinjam.SelectedDate = TanggalFormatWPF(TanggalPinjam_Terseleksi)
        win_InputHutangPiutangPemegangSaham.txt_KodeLawanTransaksi.Text = KodePemegangSaham_Terseleksi
        win_InputHutangPiutangPemegangSaham.txt_NamaLawanTransaksi.Text = NamaPemegangSaham_Terseleksi
        win_InputHutangPiutangPemegangSaham.txt_JumlahPinjaman.Text = SaldoAwalPerBaris_Terseleksi
        IsiValueElemenRichTextBox(win_InputHutangPiutangPemegangSaham.txt_Keterangan, Keterangan_Terseleksi)
        ProsesIsiValueForm = False
        win_InputHutangPiutangPemegangSaham.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If JumlahAngsuran_Terseleksi > 0 Then
            Pesan_Peringatan("Data terpilih sudah ada pembayaran. Tidak dapat dihapus.")
            Return
        End If

        If NomorJV_Terseleksi > 0 Then
            Pesan_Peringatan("Data terpilih sudah diposting. Tidak dapat diedit/hapus.")
            Return
        End If

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)

        TransactionBegin_Transaksi()

        'Hapus Data Piutang :
        If StatusSuntingDatabase = True Then HapusDataTabel_BerdasarkanNomorID_dbTransaksi("tbl_PengawasanPiutangPemegangSaham", NomorID_Terseleksi)

        'Hapus Jurnal :
        If StatusSuntingDatabase = True Then HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

        'Komit :
        If StatusSuntingDatabase = True Then TransactionCommit_Transaksi()

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

        AksesDatabase_Transaksi(Tutup)

    End Sub

    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub

    Sub TampilkanData_Pembayaran()

        pnl_SidebarKanan.Visibility = Visibility.Visible
        datatabelBayar.Rows.Clear()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" Select * FROM tbl_BuktiPenerimaan " &
                              " WHERE Nomor_BP = '" & NomorBPPPS_Terseleksi & "' " &
                              " ORDER BY Nomor_ID ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim TotalBayar As Int64 = 0
        Do While dr.Read
            Dim NomorIdBayar = dr.Item("Nomor_ID")
            Dim TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
            Dim Referensi = dr.Item("Nomor_KM")
            Dim JumlahBayar As Int64 = dr.Item("Jumlah_Bayar")
            Dim KeteranganBayar = dr.Item("Catatan")
            Dim NomorJVBayar = dr.Item("Nomor_JV")
            datatabelBayar.Rows.Add(NomorIdBayar, TanggalBayar, Referensi, JumlahBayar, KeteranganBayar, NomorJVBayar)
            TotalBayar += JumlahBayar
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksiPembayaran()

        txt_SaldoAwalPerBaris.Text = SaldoAwalPerBaris_Terseleksi
        txt_JumlahBayarPerBaris.Text = TotalBayar                               '(Sebaiknya tidak menggunakan variabel TotalBayar_Terseleksi. Agar lebih update).
        txt_SaldoAkhirPerbaris.Text = SaldoAwalPerBaris_Terseleksi - TotalBayar '(Sebaiknya tidak menggunakan variabel SisaPiutang_Terseleksi. Agar lebih update).

        If SaldoAkhirPerBaris_Terseleksi > 0 Then
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


    Sub BersihkanSeleksiPembayaran()
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

        NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID"))
        NomorBPPPS_Terseleksi = rowviewUtama("Nomor_BPPPS")
        KodePemegangSaham_Terseleksi = rowviewUtama("Kode_Pemegang_Saham")
        NamaPemegangSaham_Terseleksi = rowviewUtama("Nama_Pemegang_Saham")
        TanggalPinjam_Terseleksi = rowviewUtama("Tanggal_Pinjam")
        JumlahPiutang_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Piutang"))
        SaldoAwalPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Saldo_Awal"))
        JumlahAngsuran_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Angsuran"))
        SaldoAkhirPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Saldo_Akhir"))
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        NomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))


        NomorJV_Pembayaran_Terseleksi = 0
        Dim TotalJumlahBayar = AmbilAngka(rowviewUtama("Jumlah_Angsuran"))
        If Left(NomorBPPPS_Terseleksi, PanjangTeks_AwalanBPPPS_PlusTahunBuku) = AwalanBPPPS_PlusTahunBuku Then
            TermasukPiutangTahunIni_Terseleksi = True
        Else
            TermasukPiutangTahunIni_Terseleksi = False
        End If

        If NomorBPPPS_Terseleksi <> Kosongan Then
            TampilkanData_Pembayaran()
            btn_BukuPembantu.IsEnabled = True
        Else
            BersihkanSeleksi()
        End If

        KetersediaanTombolPosting(True)
        KetersediaanTombolJurnal(True)
        KetersediaanTombolUpdate(True)

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then btn_Edit_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If Left(e.Row.Item("Nomor_BPPPS"), PanjangTeks_AwalanBPPPS_PlusTahunBuku) = AwalanBPPPS_PlusTahunBuku Then
            If e.Row.Item("Nomor_JV") > 0 Then
                e.Row.Foreground = clrTeksPrimer
            Else
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then e.Row.Foreground = clrNeutral500
            End If
        Else
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then e.Row.Foreground = clrDataTahunLalu
        End If
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

        KolomTerseleksiBayar = datagridBayar.CurrentColumn
        BarisBayar_Terseleksi = datagridBayar.SelectedIndex
        If BarisBayar_Terseleksi < 0 Then Return
        rowviewBayar = TryCast(datagridBayar.SelectedItem, DataRowView)
        If Not rowviewBayar IsNot Nothing Then Return

        NomorJV_Terseleksi = 0
        NomorIdPembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_ID_Bayar").ToString)
        NomorJV_Pembayaran_Terseleksi = AmbilAngka(rowviewBayar("Nomor_JV_Bayar").ToString)
        Referensi_Terseleksi = rowviewBayar("Referensi_")
        If BarisBayar_Terseleksi >= 0 Then
            btn_LihatJurnal.IsEnabled = True
            btn_EditBayar.IsEnabled = True
            btn_HapusBayar.IsEnabled = True
        Else
            BersihkanSeleksiPembayaran()
        End If
        If AmbilAngka(NomorJV_Pembayaran_Terseleksi) = 0 Then KetersediaanTombolJurnal(False)
    End Sub
    Private Sub datagridBayar_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBayar.MouseDoubleClick
        'Belum ada kebutuhan kode di sini.
    End Sub


    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        If JenisTahunBuku = JenisTahunBuku_NORMAL And TermasukPiutangTahunIni_Terseleksi Then

            If NomorJV_Terseleksi = 0 Then
                Pesan_Peringatan("Data terpilih belum diposting. Tidak dapat menginput pencairan.")
                Return
            End If

        End If

        If BarisTerseleksi < 0 Then
            Pesan_Peringatan("Tidak ada baris data terseleksi.")
            Return
        End If

        If SaldoAkhirPerBaris_Terseleksi <= 0 Then
            Pesan_Informasi("Data terpilih sudah lunas.")
            Return
        End If

        win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PencairanPiutang
        win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_PencairanPiutangPemegangSaham
        win_InputBuktiPenerimaan.NomorBP = NomorBPPPS_Terseleksi
        win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodePemegangSaham_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPenerimaan.ShowDialog()
        If win_InputBuktiPenerimaan.AdaPenyimpanan Then TampilkanData()

    End Sub

    Private Sub btn_EditBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPenerimaan()
    End Sub

    Private Sub btn_HapusBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusBayar.Click
        KelolaDataPembayaranDiBukuPengawasanPenerimaan()
    End Sub


    Private Sub txt_JumlahPiutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAwalPerBaris.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_SaldoAwalPerBaris)
    End Sub

    Private Sub txt_JumlahBayar_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBayarPerBaris.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahBayarPerBaris)
    End Sub

    Private Sub txt_SisaPiutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAkhirPerbaris.TextChanged
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
    Dim Nomor_ID As New DataGridTextColumn
    Dim Nomor_BPPPS As New DataGridTextColumn
    Dim Kode_Pemegang_Saham As New DataGridTextColumn
    Dim Nama_Pemegang_Saham As New DataGridTextColumn
    Dim Tanggal_Pinjam As New DataGridTextColumn
    Dim Jumlah_Piutang As New DataGridTextColumn
    Dim Saldo_Awal As New DataGridTextColumn
    Dim Jumlah_Angsuran As New DataGridTextColumn
    Dim Saldo_Akhir As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPPPS")
        datatabelUtama.Columns.Add("Kode_Pemegang_Saham")
        datatabelUtama.Columns.Add("Nama_Pemegang_Saham")
        datatabelUtama.Columns.Add("Tanggal_Pinjam")
        datatabelUtama.Columns.Add("Jumlah_Piutang", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Angsuran", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Akhir", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPPPS, "Nomor_BPPPS", "Nomor BPPPS", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Pemegang_Saham, "Kode_Pemegang_Saham", "Kode" & Enter1Baris & "Pemegang Saham", 81, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Pemegang_Saham, "Nama_Pemegang_Saham", "Nama" & Enter1Baris & "Pemegang Saham", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pinjam, "Tanggal_Pinjam", "Tanggal Pinjam", 81, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Piutang, "Jumlah_Piutang", "Jumlah Piutang", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal, "Saldo_Awal", "Saldo Awal", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Angsuran, "Jumlah_Angsuran", "Jumlah Angsuran", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Akhir, "Saldo_Akhir", "Saldo Akhir", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)

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
