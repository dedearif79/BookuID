Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_BukuPengawasanHutangBankLeasing

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public NamaHalaman
    Public JudulForm
    Public COAHutang
    Dim Total_SisaHutang As Int64
    Dim TotalTabel As Int64
    Dim QueryTampilan
    Dim QueryTampilanHutangTahunLalu As String
    Dim QueryTampilanHutangTahunAktif As String


    Public BankLeasing
    Public TabelPengawasan
    Public TabelAngsuran
    Public KolomNomorBPH

    Dim NomorUrut
    Dim NomorID
    Dim NomorBPH
    Dim KodeKreditur
    Dim NamaKreditur
    Dim TanggalPersetujuan
    Dim TanggalPencairan
    Dim TanggalJatuhTempo
    Dim NomorKontrak
    Dim SaldoAwalPerBaris
    Dim JumlahAngsuran
    Dim SaldoAkhirPerBaris
    Dim Keterangan
    Dim NomorJV_Persetujuan
    Dim NomorJV_Pencairan

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPH_Terseleksi
    Dim KodeKreditur_Terseleksi
    Dim NamaKreditur_Terseleksi
    Dim LembagaKeuangan_Terseleksi
    Dim TanggalPersetujuan_Terseleksi
    Dim TanggalPencairan_Terseleksi
    Dim TanggalJatuhTempo_Terseleksi
    Dim NomorKontrak_Terseleksi
    Dim SaldoAwalPerBaris_Terseleksi
    Dim JumlahAngsuran_Terseleksi
    Dim SaldoAkhirPerBaris_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Persetujuan_Terseleksi
    Dim NomorJV_Pencairan_Terseleksi

    Dim jadwal_NomorUrut
    Dim jadwal_Ceklis As Boolean
    Dim jadwal_NomorID
    Dim jadwal_TanggalJatuhTempo
    Dim jadwal_TanggalBayar
    Dim jadwal_Pokok
    Dim jadwal_BagiHasil
    Dim jadwal_PPhDitanggung
    Dim jadwal_PPhDipotong
    Dim jadwal_Jumlah
    Dim jadwal_SaldoAkhir
    Dim jadwal_NomorJV

    Dim jadwal_SudahLengkap As Boolean
    Dim jadwal_SaldoAkhirBarisAkhir

    Dim jadwal_NomorUrut_Terseleksi
    Dim jadwal_NomorID_Terseleksi
    Dim jadwal_TanggalJatuhTempo_Terseleksi
    Dim jadwal_TanggalBayar_Terseleksi
    Dim jadwal_Pokok_Terseleksi
    Dim jadwal_BagiHasil_Terseleksi
    Dim jadwal_PPhDitanggung_Terseleksi
    Dim jadwal_PPhDipotong_Terseleksi
    Dim jadwal_Jumlah_Terseleksi
    Dim jadwal_SaldoAkhir_Terseleksi
    Dim jadwal_NomorJV_Terseleksi
    Dim jadwal_Ceklis_Terseleksi As Boolean

    Dim JumlahBaris_TabelJadwalAngsuran_YangSudahDibayar
    Dim TermasukHutangTahunIni_Terseleksi

    Dim JumlahBarisYangAkanDiakses
    Dim AngsuranKe_Array() As Integer
    Dim AngsuranKe_Index
    Dim AngsuranKe As String

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        Terabas()
        lbl_JudulForm.Text = JudulForm

        ProsesLoadingForm = True

        VisibilitasTombolJurnal(True)

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub

    Public Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Public Sub TampilkanData()

        KesesuaianJurnal = True

        'Style Tabel :
        datatabelUtama.Rows.Clear()
        pnl_SidebarKanan.Visibility = Visibility.Collapsed

        QueryTampilanHutangTahunLalu =
            " SELECT * FROM " & TabelPengawasan &
            " WHERE (Tanggal_Persetujuan <  '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') "
        QueryTampilanHutangTahunAktif =
            " SELECT * FROM " & TabelPengawasan &
            " WHERE (Tanggal_Persetujuan >= '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') "


        'Data Tabel :
        NomorUrut = 0
        SaldoAwal_BerdasarkanList = 0
        Total_SisaHutang = 0

        'Data Tabel Sisa Hutang Tahun Lalu :
        QueryTampilan = QueryTampilanHutangTahunLalu
        DataTabel()

        'Data Tabel Hutang Tahun Buku Aktif :
        QueryTampilan = QueryTampilanHutangTahunAktif
        DataTabel()

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
                txt_TotalTabel.Text = TotalTabel
        End Select

        lbl_TotalTabel.Text = "Saldo Akhir Hutang " & BankLeasing & " : "

        BersihkanSeleksi()

    End Sub

    Sub DataTabel()

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Tanggal_Persetujuan ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()


        Do While dr.Read

            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPH = dr.Item(KolomNomorBPH)
            KodeKreditur = dr.Item("Kode_Kreditur")
            NamaKreditur = dr.Item("Nama_Kreditur")
            TanggalPersetujuan = TanggalFormatTampilan(dr.Item("Tanggal_Persetujuan"))
            TanggalPencairan = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
            If TanggalPersetujuan = TanggalKosong Then TanggalPersetujuan = StripKosong
            If TanggalPencairan = TanggalKosong Then TanggalPencairan = StripKosong
            TanggalJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            NomorKontrak = dr.Item("Nomor_Kontrak")
            SaldoAwalPerBaris = dr.Item("Jumlah_Pinjaman")
            Keterangan = PenghapusEnter(dr.Item("Keterangan"))
            NomorJV_Persetujuan = dr.Item("Nomor_JV_Persetujuan")
            NomorJV_Pencairan = dr.Item("Nomor_JV_Pencairan")

            'Algoritma Pembayaran : ---------------------------------------------------------------------------------------------------
            Dim JumlahAngsuran_TahunLalu = 0
            JumlahAngsuran = 0
            cmdBAYAR = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                                       " WHERE " & KolomNomorBPH & " = '" & NomorBPH & "' ", KoneksiDatabaseTransaksi)
            drBAYAR_ExecuteReader()
            Do While drBAYAR.Read
                If TanggalFormatTampilan(drBAYAR.Item("Tanggal_Bayar")) <> TanggalKosong Then
                    Select Case JenisTahunBuku
                        Case JenisTahunBuku_LAMPAU
                            JumlahAngsuran += drBAYAR.Item("Pokok")
                        Case JenisTahunBuku_NORMAL
                            If drBAYAR.Item("Tanggal_Bayar") < TanggalFormatSimpan(AwalTahunBukuAktif) Then
                                JumlahAngsuran_TahunLalu += drBAYAR.Item("Pokok")
                            End If
                            If drBAYAR.Item("Tanggal_Bayar") >= TanggalFormatSimpan(AwalTahunBukuAktif) Then
                                JumlahAngsuran += drBAYAR.Item("Pokok")
                            End If
                    End Select
                End If
            Loop
            JumlahAngsuran = JumlahAngsuran_TahunLalu + JumlahAngsuran
            SaldoAkhirPerBaris = SaldoAwalPerBaris - JumlahAngsuran
            Total_SisaHutang += SaldoAkhirPerBaris
            If QueryTampilan = QueryTampilanHutangTahunLalu Then SaldoAwal_BerdasarkanList += (SaldoAwalPerBaris - JumlahAngsuran_TahunLalu)
            '-------------------------------------------------------------------------------------------------------------------------
            datatabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPH, KodeKreditur, NamaKreditur,
                                    TanggalPersetujuan, TanggalPencairan, TanggalJatuhTempo, NomorKontrak,
                                    SaldoAwalPerBaris, JumlahAngsuran, SaldoAkhirPerBaris, Keterangan, NomorJV_Persetujuan, NomorJV_Pencairan)
        Loop

        AksesDatabase_Transaksi(Tutup)

    End Sub


    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Posting.IsEnabled = False
        btn_LihatJurnalPersetujuan.IsEnabled = False
        btn_LihatJurnalPencairan.IsEnabled = False
        btn_EditHutang.IsEnabled = False
        btn_HapusHutang.IsEnabled = False
        grb_JadwalAngsuran.Header = "Jadwal Angsuran Hutang " & BankLeasing & " : "
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
    End Sub

    Sub BersihkanSeleksi_JadwalAngsuran()
        BersihkanSeleksi_WPF(datagridJadwalAngsuran, datatabelJadwalAngsuran, baristerseleksiJadwalAngsuran, jumlahbarisJadwalAngsuran)
        KetersediaanTombolLihatJurnalJadwal(False)
        btn_EditJadwal.IsEnabled = False
        btn_HapusJadwal.IsEnabled = False
    End Sub



    Sub BersihkanSeluruhCeklis()
        For Each row As DataRow In datatabelJadwalAngsuran.Rows
            row("Jadwal_Ceklis_") = False
        Next
    End Sub



    Sub VisibilitasTombolJurnal(Visibilitas As Boolean)
        brd_JurnalPersetujuan.Visibility = Visibility.Collapsed
        btn_LihatJurnalPersetujuan.Visibility = Visibility.Collapsed
        brd_JurnalPencairan.Visibility = Visibility.Collapsed
        btn_Posting.Visibility = Visibility.Collapsed
        btn_LihatJurnalPencairan.Visibility = Visibility.Collapsed
        brd_LihatJurnalJadwal.Visibility = Visibility.Collapsed
        btn_LihatJurnalJadwal.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                brd_JurnalPersetujuan.Visibility = Visibility.Visible
                btn_LihatJurnalPersetujuan.Visibility = Visibility.Visible
                brd_JurnalPencairan.Visibility = Visibility.Visible
                btn_Posting.Visibility = Visibility.Visible
                btn_LihatJurnalPencairan.Visibility = Visibility.Visible
                brd_LihatJurnalJadwal.Visibility = Visibility.Visible
                btn_LihatJurnalJadwal.Visibility = Visibility.Visible
            End If
        Else
            brd_JurnalPersetujuan.Visibility = Visibility.Collapsed
            btn_LihatJurnalPersetujuan.Visibility = Visibility.Collapsed
            brd_JurnalPencairan.Visibility = Visibility.Collapsed
            btn_Posting.Visibility = Visibility.Collapsed
            btn_LihatJurnalPencairan.Visibility = Visibility.Collapsed
            brd_LihatJurnalJadwal.Visibility = Visibility.Collapsed
            btn_LihatJurnalJadwal.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KetersediaanTombolPosting(Tersedia As Boolean)
        btn_Posting.IsEnabled = False
        If Tersedia Then
            If TermasukHutangTahunIni_Terseleksi Then btn_Posting.IsEnabled = True
        Else
            btn_Posting.IsEnabled = False
        End If
    End Sub


    Sub KetersediaanTombolLihatJurnalJadwal(Tersedia As Boolean)
        btn_LihatJurnalJadwal.IsEnabled = False
        If Tersedia Then
            If Right(rowviewJadwalAngsuran("jadwal_Tanggal_Bayar"), 4) = TahunBukuAktif Then btn_LihatJurnalJadwal.IsEnabled = True
        Else
            btn_LihatJurnalJadwal.IsEnabled = False
        End If
    End Sub



    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnalPersetujuan_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnalPersetujuan.Click
        LihatJurnal(NomorJV_Persetujuan_Terseleksi)
    End Sub


    Private Sub btn_Posting_Click(sender As Object, e As RoutedEventArgs) Handles btn_Posting.Click
        win_InputBuktiPenerimaan = New wpfWin_InputBuktiPenerimaan
        win_InputBuktiPenerimaan.ResetForm()
        win_InputBuktiPenerimaan.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPenerimaan.cmb_Kategori.IsEnabled = False
        win_InputBuktiPenerimaan.cmb_Kategori.SelectedValue = Kategori_PenerimaanTunai
        win_InputBuktiPenerimaan.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPenerimaan.NomorBP = NomorBPH_Terseleksi
        Select Case BankLeasing
            Case bl_Bank
                win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_HutangBank
                win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeKreditur_Terseleksi
                win_InputBuktiPenerimaan.TambahkanDataPenerimaanHutangBank()
            Case bl_Leasing
                win_InputBuktiPenerimaan.cmb_Peruntukan.SelectedValue = Peruntukan_HutangLeasing
                win_InputBuktiPenerimaan.txt_KodeLawanTransaksi.Text = KodeKreditur_Terseleksi
                win_InputBuktiPenerimaan.TambahkanDataPenerimaanHutangLeasing()
        End Select
        win_InputBuktiPenerimaan.ShowDialog()
    End Sub


    Private Sub btn_LihatJurnalPencairan_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnalPencairan.Click
        If NomorJV_Pencairan_Terseleksi = 0 Then
            Pesan_Informasi("Belum ada pencairan.")
            Return
        End If
        LihatJurnal(NomorJV_Pencairan_Terseleksi)
    End Sub


    Private Sub btn_InputHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputHutang.Click
        win_InputHutangBankLeasing = New wpfWin_InputHutangBankLeasing
        win_InputHutangBankLeasing.ResetForm()
        win_InputHutangBankLeasing.FungsiForm = FungsiForm_TAMBAH
        win_InputHutangBankLeasing.BankLeasing = BankLeasing
        win_InputHutangBankLeasing.ShowDialog()
    End Sub


    Private Sub btn_EditHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditHutang.Click

        If JumlahAngsuran_Terseleksi > 0 Or NomorJV_Pencairan_Terseleksi > 0 Then
            Pesan_Peringatan("'Data Hutang' ini sudah tidak dapat diedit.")
            Return
        End If

        win_InputHutangBankLeasing = New wpfWin_InputHutangBankLeasing
        win_InputHutangBankLeasing.ResetForm()
        ProsesIsiValueForm = True
        win_InputHutangBankLeasing.FungsiForm = FungsiForm_EDIT
        win_InputHutangBankLeasing.BankLeasing = BankLeasing
        win_InputHutangBankLeasing.NomorID = NomorID_Terseleksi
        win_InputHutangBankLeasing.txt_NomorBP.Text = NomorBPH_Terseleksi
        win_InputHutangBankLeasing.NomorJV_Persetujuan = NomorJV_Persetujuan_Terseleksi
        win_InputHutangBankLeasing.NomorJV_Pencairan = NomorJV_Pencairan_Terseleksi
        win_InputHutangBankLeasing.dtp_TanggalPersetujuan.Text = TanggalPersetujuan_Terseleksi
        win_InputHutangBankLeasing.txt_KodeKreditur.Text = KodeKreditur_Terseleksi
        win_InputHutangBankLeasing.txt_NamaKreditur.Text = NamaKreditur_Terseleksi
        win_InputHutangBankLeasing.dtp_TanggalJatuhTempo.Text = TanggalJatuhTempo_Terseleksi
        win_InputHutangBankLeasing.txt_JumlahPinjaman.Text = SaldoAwalPerBaris_Terseleksi
        win_InputHutangBankLeasing.txt_NomorKontrak.Text = NomorKontrak_Terseleksi
        IsiValueElemenRichTextBox(win_InputHutangBankLeasing.txt_Keterangan, Keterangan_Terseleksi)
        ProsesIsiValueForm = False
        win_InputHutangBankLeasing.ShowDialog()

    End Sub


    Private Sub btn_HapusHutang_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusHutang.Click

        If jumlahbarisJadwalAngsuran > 0 Or NomorJV_Pencairan_Terseleksi > 0 Then
            Pesan_Peringatan("'Data Hutang' ini sudah tidak dapat dihapus.")
            Return
        End If

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM " & TabelPengawasan &
                              " WHERE Nomor_ID = '" & NomorID_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                              " WHERE Nomor_JV = '" & NomorJV_Persetujuan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi " &
                              " WHERE Nomor_JV = '" & NomorJV_Pencairan_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
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
        NomorBPH_Terseleksi = rowviewUtama("Nomor_BPH")
        KodeKreditur_Terseleksi = rowviewUtama("Kode_Kreditur")
        NamaKreditur_Terseleksi = rowviewUtama("Nama_Kreditur")
        TanggalPersetujuan_Terseleksi = rowviewUtama("Tanggal_Persetujuan")
        If TanggalPersetujuan_Terseleksi = StripKosong Then TanggalPersetujuan_Terseleksi = TanggalKosong
        TanggalPencairan_Terseleksi = rowviewUtama("Tanggal_Pencairan")
        If TanggalPencairan_Terseleksi = StripKosong Then TanggalPencairan_Terseleksi = TanggalKosong
        TanggalJatuhTempo_Terseleksi = rowviewUtama("Jatuh_Tempo")
        NomorKontrak_Terseleksi = rowviewUtama("Nomor_Kontrak")
        SaldoAwalPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Saldo_Awal"))
        JumlahAngsuran_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Bayar"))
        SaldoAkhirPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Saldo_Akhir"))
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        NomorJV_Persetujuan_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV_Persetujuan"))
        NomorJV_Pencairan_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV_Pencairan"))
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeKreditur_Terseleksi & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            LembagaKeuangan_Terseleksi = dr.Item("Keuangan")
        End If
        AksesDatabase_General(Tutup)

        If AmbilTahun_DariTanggal(TanggalPersetujuan_Terseleksi) = TahunBukuAktif Then TermasukHutangTahunIni_Terseleksi = True
        If AmbilTahun_DariTanggal(TanggalPersetujuan_Terseleksi) <> TahunBukuAktif Then TermasukHutangTahunIni_Terseleksi = False

        If KodeKreditur_Terseleksi <> Kosongan Then
            TampilkanData_JadwalAngsuran()
            If NomorJV_Persetujuan_Terseleksi > 0 Then
                btn_LihatJurnalPersetujuan.IsEnabled = True
            Else
                btn_LihatJurnalPersetujuan.IsEnabled = False
            End If
            If NomorJV_Pencairan_Terseleksi > 0 Then
                KetersediaanTombolPosting(False)
                btn_LihatJurnalPencairan.IsEnabled = True
            Else
                KetersediaanTombolPosting(True)
                btn_LihatJurnalPencairan.IsEnabled = False
                PesanUntukProgrammer("Nomor JV Pencairan: " & NomorJV_Pencairan_Terseleksi & Enter2Baris &
                                     "Termasuk Hutang Tahun Ini: " & TermasukHutangTahunIni_Terseleksi)
            End If
            If JumlahAngsuran_Terseleksi > 0 Then
                btn_EditHutang.IsEnabled = False
                btn_HapusHutang.IsEnabled = False
            Else
                btn_EditHutang.IsEnabled = True
                If jumlahbarisJadwalAngsuran > 0 Then
                    btn_HapusHutang.IsEnabled = False
                Else
                    btn_HapusHutang.IsEnabled = True
                End If
            End If
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If BarisTerseleksi < 0 Then Return
        btn_EditHutang_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        Dim AwalanBPH_PlusTahunBuku As String = Kosongan
        Dim PanjangTeks_AwalanBPH_PlusTahunBuku As Integer
        Select Case BankLeasing
            Case bl_Bank
                AwalanBPH_PlusTahunBuku = AwalanBPHB_PlusTahunBuku
                PanjangTeks_AwalanBPH_PlusTahunBuku = PanjangTeks_AwalanBPHB_PlusTahunBuku
            Case bl_Leasing
                PanjangTeks_AwalanBPH_PlusTahunBuku = PanjangTeks_AwalanBPHL_PlusTahunBuku
                AwalanBPH_PlusTahunBuku = AwalanBPHL_PlusTahunBuku
        End Select
        If Left(e.Row.Item("Nomor_BPH"), PanjangTeks_AwalanBPH_PlusTahunBuku) = AwalanBPH_PlusTahunBuku Then
            If e.Row.Item("Nomor_JV_Pencairan") > 0 Then
                e.Row.Foreground = clrTeksPrimer
            Else
                If JenisTahunBuku = JenisTahunBuku_NORMAL Then e.Row.Foreground = clrNeutral500
            End If
        Else
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then e.Row.Foreground = clrDataTahunLalu
        End If
    End Sub

    Sub RefreshTampilanData_JadwalAngsuran()
        TampilkanData_JadwalAngsuran()
    End Sub


    Public Sub TampilkanData_JadwalAngsuran()

        pnl_SidebarKanan.Visibility = Visibility.Visible

        If BankLeasing = bl_Bank Then grb_JadwalAngsuran.Header = "Jadwal Angsurang Hutang Bank - " & NamaKreditur_Terseleksi & " :"
        If BankLeasing = bl_Leasing Then grb_JadwalAngsuran.Header = "Jadwal Angsurang Hutang Leasing - " & NamaKreditur_Terseleksi & " :"

        'Style Tabel :
        datatabelJadwalAngsuran.Rows.Clear()

        'Data Tabel :
        Dim Index_BarisTabel = 0
        jadwal_NomorUrut = 0
        JumlahBaris_TabelJadwalAngsuran_YangSudahDibayar = 0
        Dim jadwal_SaldoAkhirSebelumnya = SaldoAwalPerBaris_Terseleksi

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                              " WHERE " & KolomNomorBPH & " = '" & NomorBPH_Terseleksi & "' " &
                              " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read

            jadwal_NomorUrut = dr.Item("Angsuran_Ke")
            jadwal_Ceklis = False
            jadwal_NomorID = dr.Item("Nomor_ID")
            jadwal_TanggalJatuhTempo = TanggalFormatTampilan(dr.Item("Tanggal_Jatuh_Tempo"))
            jadwal_TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            If jadwal_TanggalBayar <> TanggalKosong Then
                JumlahBaris_TabelJadwalAngsuran_YangSudahDibayar += 1
            Else
                jadwal_TanggalBayar = StripKosong
            End If
            jadwal_Pokok = dr.Item("Pokok")
            jadwal_BagiHasil = dr.Item("Bagi_Hasil")
            jadwal_PPhDitanggung = dr.Item("PPh_Ditanggung")
            jadwal_PPhDipotong = dr.Item("PPh_Dipotong")
            jadwal_Jumlah = jadwal_Pokok + jadwal_BagiHasil - jadwal_PPhDipotong
            jadwal_SaldoAkhir = jadwal_SaldoAkhirSebelumnya - jadwal_Pokok
            jadwal_NomorJV = dr.Item("Nomor_JV")

            datatabelJadwalAngsuran.Rows.Add(jadwal_NomorUrut, jadwal_Ceklis, jadwal_NomorID, jadwal_TanggalJatuhTempo, jadwal_TanggalBayar,
                                    jadwal_Pokok, jadwal_BagiHasil, jadwal_PPhDitanggung, jadwal_PPhDipotong,
                                    jadwal_Jumlah, jadwal_SaldoAkhir, jadwal_NomorJV)

            jadwal_SaldoAkhirSebelumnya = AmbilAngka(jadwal_SaldoAkhir)

            Index_BarisTabel += 1

        Loop

        AksesDatabase_Transaksi(Tutup)

        jumlahbarisJadwalAngsuran = datatabelJadwalAngsuran.Rows.Count

        jadwal_SaldoAkhirBarisAkhir = AmbilAngka(jadwal_SaldoAkhir)

        If jumlahbarisJadwalAngsuran > 0 Then
            If jadwal_SaldoAkhirBarisAkhir <= 0 Then jadwal_SudahLengkap = True
            If jadwal_SaldoAkhirBarisAkhir > 0 Then jadwal_SudahLengkap = False
        Else
            jadwal_SudahLengkap = False
        End If

        BersihkanSeleksi_JadwalAngsuran()

        If jadwal_SaldoAkhirBarisAkhir < 0 Then
            PesanPeringatan("Saldo Akhir Jadwal Angsuran pada hutang ini kurang dari 0 (nol)." & Enter2Baris &
                            "Silakan perbaiki 'Jadwal Angsuran'..!")
            Terabas()
        End If

    End Sub



    Private Sub btn_RefreshJadwal_Click(sender As Object, e As RoutedEventArgs) Handles btn_RefreshJadwal.Click
        RefreshTampilanData_JadwalAngsuran()
    End Sub


    Private Sub btn_InputJadwal_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputJadwal.Click

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If TanggalPencairan_Terseleksi = TanggalKosong Then
                Pesan_Peringatan("Data terpilih belum diposting. Tidak dapat menginput jadwal.")
                Return
            End If
        End If

        BersihkanSeluruhCeklis()

        If jadwal_SudahLengkap = True Then
            Pesan_Informasi("Jadwal sudah lengkap." & Enter2Baris & "Anda sudah tidak dapat menginput 'Jadwal Angsuran' pada hutang ini.")
            Return
        End If

        win_InputJadwalAngsuranHutangBankLeasing = New wpfWin_InputJadwalAngsuranHutangBankLeasing
        win_InputJadwalAngsuranHutangBankLeasing.ResetForm()
        win_InputJadwalAngsuranHutangBankLeasing.BankLeasing = BankLeasing
        win_InputJadwalAngsuranHutangBankLeasing.FungsiForm = FungsiForm_TAMBAH
        win_InputJadwalAngsuranHutangBankLeasing.NomorBPH = NomorBPH_Terseleksi
        win_InputJadwalAngsuranHutangBankLeasing.KodeKreditur = KodeKreditur_Terseleksi
        win_InputJadwalAngsuranHutangBankLeasing.ShowDialog()

    End Sub


    Private Sub btn_EditJadwal_Click(sender As Object, e As RoutedEventArgs) Handles btn_EditJadwal.Click

        BersihkanSeluruhCeklis()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP = '" & NomorBPH_Terseleksi & "' " &
                              " AND Angsuran_Ke = '" & jadwal_NomorUrut_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If dr.HasRows Then
            PesanPeringatan("Data sudah diajukan dan tidak dapat dihapus/edit.")
            AksesDatabase_Transaksi(Tutup)
            Return
        End If
        AksesDatabase_Transaksi(Tutup)

        win_InputJadwalAngsuranHutangBankLeasing = New wpfWin_InputJadwalAngsuranHutangBankLeasing
        win_InputJadwalAngsuranHutangBankLeasing.ResetForm()
        win_InputJadwalAngsuranHutangBankLeasing.FungsiForm = FungsiForm_EDIT
        win_InputJadwalAngsuranHutangBankLeasing.BankLeasing = BankLeasing
        win_InputJadwalAngsuranHutangBankLeasing.NomorID = jadwal_NomorID_Terseleksi
        win_InputJadwalAngsuranHutangBankLeasing.NomorBPH = NomorBPH_Terseleksi
        win_InputJadwalAngsuranHutangBankLeasing.NomorJV = jadwal_NomorJV_Terseleksi
        win_InputJadwalAngsuranHutangBankLeasing.KodeKreditur = KodeKreditur_Terseleksi
        win_InputJadwalAngsuranHutangBankLeasing.ShowDialog()

    End Sub


    Private Sub btn_HapusJadwal_Click(sender As Object, e As RoutedEventArgs) Handles btn_HapusJadwal.Click

        BersihkanSeluruhCeklis()

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " WHERE Nomor_BP = '" & NomorBPH_Terseleksi & "' " &
                              " AND Angsuran_Ke = '" & jadwal_NomorUrut_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If dr.HasRows Then
            PesanPeringatan("Data sudah diajukan dan tidak dapat dihapus/edit.")
            AksesDatabase_Transaksi(Tutup)
            Return
        End If
        AksesDatabase_Transaksi(Tutup)

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM " & TabelAngsuran &
                              " WHERE Nomor_ID = '" & jadwal_NomorID_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData_JadwalAngsuran()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub


    Private Sub btn_InputBayar_Click(sender As Object, e As RoutedEventArgs) Handles btn_InputBayar.Click

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            If TanggalPencairan_Terseleksi = TanggalKosong Then
                Pesan_Peringatan("Silakan lengkapi terlebih dahulu 'Data Pencairan'.")
                Return
            End If
        End If
        Dim IndexBaris = 0
        Dim JumlahBarisTerceklis = 0
        Dim Ceklis As Boolean = False
        Do While IndexBaris < jumlahbarisJadwalAngsuran
            Ceklis = datatabelJadwalAngsuran.Rows(IndexBaris)("Jadwal_Ceklis_")
            If Ceklis = True Then JumlahBarisTerceklis += 1
            IndexBaris += 1
        Loop
        If JumlahBarisTerceklis = 0 Then JumlahBarisTerceklis = 1

        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        ProsesIsiValueForm = True
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.cmb_Kategori.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Peruntukan.IsEnabled = False
        win_InputBuktiPengeluaran.cmb_Kategori.SelectedValue = Kategori_PembayaranHutang
        If BankLeasing = bl_Bank Then win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangBank
        If BankLeasing = bl_Leasing Then win_InputBuktiPengeluaran.cmb_Peruntukan.SelectedValue = Peruntukan_PembayaranHutangLeasing
        win_InputBuktiPengeluaran.JumlahAngsuranTerjadwal = JumlahBarisTerceklis
        win_InputBuktiPengeluaran.NomorBP = NomorBPH_Terseleksi
        win_InputBuktiPengeluaran.txt_KodeLawanTransaksi.Text = KodeKreditur_Terseleksi
        ProsesIsiValueForm = False
        win_InputBuktiPengeluaran.ShowDialog()
        If win_InputBuktiPengeluaran.DialogResult = DialogResult.OK And win_InputBuktiPengeluaran.StatusPengajuan = Status_Dibayar Then
            BersihkanSeluruhCeklis()
            RefreshSetelahBayar()
        End If

    End Sub
    Sub RefreshSetelahBayar()
        'If frm_InputTransaksi.PenyimpananSukses = True Then
        '    TampilkanData_JadwalAngsuran()
        '    'If JenisPPh_Terseleksi = JenisPPh_Pasal21 Then frm_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
        '    'If JenisPPh_Terseleksi = JenisPPh_Pasal22 Then frm_BukuPengawasanHutangPPhPasal22.RefreshTampilanData()
        '    'If JenisPPh_Terseleksi = JenisPPh_Pasal23 Then frm_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
        '    'If JenisPPh_Terseleksi = JenisPPh_Pasal42 Then frm_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
        '    'If JenisPPh_Terseleksi = JenisPPh_Pasal25 Then frm_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
        '    'If JenisPPh_Terseleksi = JenisPPh_Pasal26 Then frm_BukuPengawasanHutangPPhPasal26.RefreshTampilanData()
        '    'If JenisPPh_Terseleksi = JenisPPh_Pasal29 Then frm_BukuPengawasanHutangPPhPasal29.RefreshTampilanData()
        'End If
    End Sub


    Private Sub btn_LihatJurnalJadwal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnalJadwal.Click
        LihatJurnal(jadwal_NomorJV_Terseleksi)
    End Sub


    Private Sub btn_ExportJadwal_Click(sender As Object, e As RoutedEventArgs) Handles btn_ExportJadwal.Click
        EksporDataGridKeEXCEL(datagridJadwalAngsuran)
    End Sub



    Private Sub datagridJadwalAngsuran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridJadwalAngsuran.SelectionChanged
    End Sub
    Private Sub datagridJadwalAngsuran_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridJadwalAngsuran.PreviewMouseLeftButtonUp
        HeaderKolomJadwalAngsuran = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomJadwalAngsuran IsNot Nothing Then
            BersihkanSeleksi_JadwalAngsuran()
        End If
    End Sub
    Private Sub datagridJadwalAngsuran_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridJadwalAngsuran.SelectedCellsChanged

        KolomTerseleksiJadwalAngsuran = datagridJadwalAngsuran.CurrentColumn
        baristerseleksiJadwalAngsuran = datagridJadwalAngsuran.SelectedIndex
        If baristerseleksiJadwalAngsuran < 0 Then Return
        rowviewJadwalAngsuran = TryCast(datagridJadwalAngsuran.SelectedItem, DataRowView)
        If Not rowviewJadwalAngsuran IsNot Nothing Then Return

        baristerseleksiJadwalAngsuran = baristerseleksiJadwalAngsuran
        jadwal_Ceklis_Terseleksi = rowviewJadwalAngsuran("Jadwal_Ceklis_")
        jadwal_NomorUrut_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_Nomor_Urut"))
        jadwal_NomorID_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_Nomor_ID"))
        jadwal_TanggalJatuhTempo_Terseleksi = rowviewJadwalAngsuran("Jadwal_Jatuh_Tempo")
        jadwal_TanggalBayar_Terseleksi = rowviewJadwalAngsuran("Jadwal_Tanggal_Bayar")
        jadwal_Pokok_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_Pokok_"))
        jadwal_BagiHasil_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_Bagi_Hasil"))
        jadwal_PPhDitanggung_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_PPh_Ditanggung"))
        jadwal_PPhDipotong_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_PPh_Dipotong"))
        jadwal_Jumlah_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_Jumlah_"))
        jadwal_SaldoAkhir_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_Saldo_Akhir"))
        jadwal_NomorJV_Terseleksi = AmbilAngka(rowviewJadwalAngsuran("Jadwal_Nomor_JV"))

        If jadwal_NomorID_Terseleksi > 0 Then
            If jadwal_TanggalBayar_Terseleksi <> StripKosong Then 'Jika sudah ada pembayaran
                KetersediaanTombolLihatJurnalJadwal(True)
                btn_EditJadwal.IsEnabled = False
                btn_HapusJadwal.IsEnabled = False
            Else
                KetersediaanTombolLihatJurnalJadwal(False)
                btn_EditJadwal.IsEnabled = True
                If jumlahbarisJadwalAngsuran = baristerseleksiJadwalAngsuran + 1 Then
                    btn_HapusJadwal.IsEnabled = True
                Else
                    btn_HapusJadwal.IsEnabled = False
                End If
            End If
        Else
            BersihkanSeleksi_JadwalAngsuran()
        End If

    End Sub
    Private Sub datagridJadwalAngsuran_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles datagridJadwalAngsuran.MouseDown
    End Sub
    Private Sub datagridJadwalAngsuran_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles datagridJadwalAngsuran.MouseUp
        If KolomTerseleksiJadwalAngsuran.DisplayIndex = Jadwal_Ceklis_.DisplayIndex Then AlgoritmaCeklis()
    End Sub
    Private Sub datagridJadwalAngsuran_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridJadwalAngsuran.MouseDoubleClick
        If datatabelJadwalAngsuran.Rows.Count = 0 Or baristerseleksiJadwalAngsuran < 0 Then Return
        If jadwal_NomorJV_Terseleksi = 0 Then
            btn_EditJadwal_Click(sender, e)
        Else
            btn_LihatJurnalJadwal_Click(sender, e)
        End If
    End Sub
    Sub AlgoritmaCeklis()
        If jadwal_Ceklis_Terseleksi = False Then
            For Each row As DataRow In datatabelJadwalAngsuran.Rows
                If row("Jadwal_Tanggal_Bayar") <> StripKosong Then row("Jadwal_Ceklis_") = False
                If row("Jadwal_Tanggal_Bayar") = StripKosong Then row("Jadwal_Ceklis_") = True
                If row.Table.Rows.IndexOf(row) = baristerseleksiJadwalAngsuran Then Exit For
            Next
        Else
            For Each row As DataRow In datatabelJadwalAngsuran.Rows
                If row.Table.Rows.IndexOf(row) >= baristerseleksiJadwalAngsuran Then row("Jadwal_Ceklis_") = False
            Next
        End If
    End Sub
    Private Sub datagridJadwalAngsuran_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridJadwalAngsuran.LoadingRow
        If e.Row.Item("jadwal_Tanggal_Bayar") = StripKosong Then
            e.Row.Foreground = clrNeutral500
        Else
            e.Row.Foreground = clrTeksPrimer
        End If
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
    Dim Nomor_BPH As New DataGridTextColumn
    Dim Kode_Kreditur As New DataGridTextColumn
    Dim Nama_Kreditur As New DataGridTextColumn
    Dim Tanggal_Persetujuan As New DataGridTextColumn
    Dim Tanggal_Pencairan As New DataGridTextColumn
    Dim Jatuh_Tempo As New DataGridTextColumn
    Dim Nomor_Kontrak As New DataGridTextColumn
    Dim Saldo_Awal As New DataGridTextColumn
    Dim Jumlah_Bayar As New DataGridTextColumn
    Dim Saldo_Akhir As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV_Persetujuan As New DataGridTextColumn
    Dim Nomor_JV_Pencairan As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_BPH")
        datatabelUtama.Columns.Add("Kode_Kreditur")
        datatabelUtama.Columns.Add("Nama_Kreditur")
        datatabelUtama.Columns.Add("Tanggal_Persetujuan")
        datatabelUtama.Columns.Add("Tanggal_Pencairan")
        datatabelUtama.Columns.Add("Jatuh_Tempo")
        datatabelUtama.Columns.Add("Nomor_Kontrak")
        datatabelUtama.Columns.Add("Saldo_Awal", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Bayar", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Akhir", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV_Persetujuan", GetType(Int64))
        datatabelUtama.Columns.Add("Nomor_JV_Pencairan", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_BPH, "Nomor_BPH", "Nomor BPH", 123, FormatString, KiriTengah, KunciUrut, TerlihatKhususProgrammer)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Kreditur, "Kode_Kreditur", "Kode Kreditur", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Kreditur, "Nama_Kreditur", "Nama Kreditur", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Persetujuan, "Tanggal_Persetujuan", "Tanggal Persetujuan", 81, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pencairan, "Tanggal_Pencairan", "Tanggal Pencairan", 81, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jatuh_Tempo, "Jatuh_Tempo", "Jatuh Tempo", 81, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Kontrak, "Nomor_Kontrak", "Nomor Kontrak", 150, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal, "Saldo_Awal", "Saldo Awal", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Bayar, "Jumlah_Bayar", "Jumlah Bayar", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Akhir, "Saldo_Akhir", "Saldo Akhir", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV_Persetujuan, "Nomor_JV_Persetujuan", "Nomor JV Persetujuan", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV_Pencairan, "Nomor_JV_Pencairan", "Nomor JV Pencairan", 99, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

    End Sub

    'Tabel Bayar :
    Public datatabelJadwalAngsuran As DataTable
    Public dataviewJadwalAngsuran As DataView
    Public rowviewJadwalAngsuran As DataRowView
    Public newRowJadwalAngsuran As DataRow
    Public HeaderKolomJadwalAngsuran As DataGridColumnHeader
    Public KolomTerseleksiJadwalAngsuran As DataGridColumn
    Public baristerseleksiJadwalAngsuran As Integer
    Public jumlahbarisJadwalAngsuran As Integer

    Dim Jadwal_Nomor_Urut As New DataGridTextColumn
    Dim Jadwal_Ceklis_ As New DataGridCheckBoxColumn
    Dim Jadwal_Nomor_ID As New DataGridTextColumn
    Dim Jadwal_Jatuh_Tempo As New DataGridTextColumn
    Dim Jadwal_Tanggal_Bayar As New DataGridTextColumn
    Dim Jadwal_Pokok_ As New DataGridTextColumn
    Dim Jadwal_Bagi_Hasil As New DataGridTextColumn
    Dim Jadwal_PPh_Ditanggung As New DataGridTextColumn
    Dim Jadwal_PPh_Dipotong As New DataGridTextColumn
    Dim Jadwal_Jumlah_ As New DataGridTextColumn
    Dim Jadwal_Saldo_Akhir As New DataGridTextColumn
    Dim Jadwal_Nomor_JV As New DataGridTextColumn

    Sub Buat_DataTabelBayar()

        datatabelJadwalAngsuran = New DataTable
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Nomor_Urut")
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Ceklis_")
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Nomor_ID")
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Jatuh_Tempo")
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Tanggal_Bayar")
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Pokok_", GetType(Int64))
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Bagi_Hasil", GetType(Int64))
        datatabelJadwalAngsuran.Columns.Add("Jadwal_PPh_Ditanggung", GetType(Int64))
        datatabelJadwalAngsuran.Columns.Add("Jadwal_PPh_Dipotong", GetType(Int64))
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Jumlah_", GetType(Int64))
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Saldo_Akhir", GetType(Int64))
        datatabelJadwalAngsuran.Columns.Add("Jadwal_Nomor_JV", GetType(Int64))



        StyleTabelPembantu_WPF(datagridJadwalAngsuran, datatabelJadwalAngsuran, dataviewJadwalAngsuran)

        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Nomor_Urut, "Jadwal_Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Ceklis_, "Jadwal_Ceklis_", "Chk", 45, FormatAngka, TengahTengah, KunciUrut, Terlihat, False)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Nomor_ID, "Jadwal_Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Jatuh_Tempo, "Jadwal_Jatuh_Tempo", "Jatuh Tempo", 75, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Tanggal_Bayar, "Jadwal_Tanggal_Bayar", "Tanggal Bayar", 75, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Pokok_, "Jadwal_Pokok_", "Pokok", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Bagi_Hasil, "Jadwal_Bagi_Hasil", "Bunga/" & Enter1Baris & "Bagi Hasil", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_PPh_Ditanggung, "Jadwal_PPh_Ditanggung", "PPh Ditanggung", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_PPh_Dipotong, "Jadwal_PPh_Dipotong", "PPh Dipotong", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Jumlah_, "Jadwal_Jumlah_", "Jumlah", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Saldo_Akhir, "Jadwal_Saldo_Akhir", "Saldo Akhir", 81, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridJadwalAngsuran, Jadwal_Nomor_JV, "Jadwal_Nomor_JV", "Nomor_JV", 81, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

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
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_AJP.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
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
