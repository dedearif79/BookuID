Imports bcomm
Imports System.Data
Imports System.Data.Odbc
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input

Public Class wpfUsc_BukuPengawasanBuktiPotongPPh_Prepaid

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False

    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim SumberData
    Dim NomorID
    Dim NomorPatokan
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim NomorFakturPajak
    Dim KodeCustomer
    Dim NamaCustomer
    Dim JenisPPh
    Dim DPP
    Dim JumlahPPh
    Dim TanggalDipotong
    Dim NomorBuktiPotong
    Dim TanggalBuktiPotong
    Dim Keterangan
    Dim NomorJV

    Dim BarisTerseleksi
    Dim NomorUrut_Terseleksi
    Dim SumberData_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorPatokan_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim DPP_Terseleksi
    Dim JenisPPh_Terseleksi
    Dim JumlahPPh_Terseleksi
    Dim TanggalDipotong_Terseleksi
    Dim NomorBuktiPotong_Terseleksi
    Dim TanggalBuktiPotong_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi

    'Variabel Sumber Data :
    Public SumberData_Penjualan = "Penjualan"
    Public SumberData_PencairanPiutangUsaha = "Piutang Usaha"
    Public SumberData_PencairanPiutangPihakKetiga = "Piutang Pihak Ketiga"
    Public SumberData_PencairanPiutangAfiliasi = "Piutang Afiliasi"
    Public SumberData_PiutangDividen = "Piutang Dividen"

    'Variabel Bukti Potong Sudah/Belum Diterima :
    Dim BP_SudahDiterima = "Sudah Diterima"
    Dim BP_BelumDiterima = "Belum Diterima"

    'Variabel Filter :
    Dim PilihanSumberData
    Dim PilihanJenisPPh
    Dim PilihanBuktiPotong
    Dim FilterJenisPPh
    Dim FilterJenisPajak
    Dim FilterBuktiPotong

    'DataTable untuk DataGrid :
    Dim datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn


    Private Sub wpfUsc_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return

        StatusAktif = True

        ProsesLoadingForm = True

        Buat_DataTabelUtama()
        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_SumberData()
        KontenCombo_JenisPPh()
        KontenCombo_BuktiPotong()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Dim EksekusiTampilanData As Boolean

    Async Sub TampilkanDataAsync()

        If Not EksekusiTampilanData Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            KesesuaianJurnal = True

            'Filter Sumber Data :
            'Untuk Filter Sumber Data, ada Query-nya tersendiri.

            'Filter Jenis PPh :
            If PilihanJenisPPh = Pilihan_Semua Then FilterJenisPPh = " AND ( Jenis_PPh = '" & JenisPPh_Pasal23 & "' OR Jenis_PPh = '" & JenisPPh_Pasal42 & "' ) "
            If PilihanJenisPPh = JenisPPh_Pasal23 Then FilterJenisPPh = " AND Jenis_PPh = '" & JenisPPh_Pasal23 & "' "
            If PilihanJenisPPh = JenisPPh_Pasal42 Then FilterJenisPPh = " AND Jenis_PPh = '" & JenisPPh_Pasal42 & "' "

            'Filter Bukti Potong
            If PilihanBuktiPotong = Pilihan_Semua Then FilterBuktiPotong = " "
            If PilihanBuktiPotong = BP_SudahDiterima Then FilterBuktiPotong = " AND Nomor_Bukti_Potong <> '' "
            If PilihanBuktiPotong = BP_BelumDiterima Then FilterBuktiPotong = " AND Nomor_Bukti_Potong = '' "

            'Style Tabel :
            datatabelUtama.Rows.Clear()

            'Data Tabel
            NomorUrut = 0

            AksesDatabase_Transaksi(Buka)
            If StatusKoneksiDatabase = False Then Exit Try

            'PPh Dari Penjualan Tunai :
            If PilihanSumberData = SumberData_Penjualan _
                Or PilihanSumberData = Pilihan_Semua Then
                SumberData = SumberData_Penjualan
                cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                      " WHERE PPh_Terutang > 0 " &
                                      " AND Jenis_Penjualan = '" & JenisPenjualan_Tunai & "' " &
                                      FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                Dim NomorPenjualanSebelumnya = Kosongan
                Do While dr.Read
                    NomorPatokan = dr.Item("Nomor_Penjualan")
                    If NomorPatokan <> NomorPenjualanSebelumnya Then
                        NomorUrut += 1
                        NomorID = dr.Item("Nomor_ID")
                        NomorInvoice = dr.Item("Nomor_Invoice")
                        TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                        NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
                        KodeCustomer = dr.Item("Kode_Customer")
                        NamaCustomer = dr.Item("Nama_Customer")
                        DPP = dr.Item("Dasar_Pengenaan_Pajak")
                        JenisPPh = dr.Item("Jenis_PPh")
                        JumlahPPh = dr.Item("PPh_Terutang")
                        TanggalDipotong = TanggalInvoice
                        NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                        TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                        If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                        If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                        Keterangan = PenghapusEnter(dr.Item("Keterangan_Bukti_Potong"))
                        NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                        TambahBaris()
                    End If
                    NomorPenjualanSebelumnya = NomorPatokan
                    Await Task.Yield()
                Loop

            End If

            'PPh Dari Pencairan atas Piutang Usaha :
            If PilihanSumberData = SumberData_PencairanPiutangUsaha _
                Or PilihanSumberData = Pilihan_Semua Then
                FilterJenisPajak = Replace(FilterJenisPPh, "Jenis_PPh", "Jenis_Pajak")  'Karena Perbedaan Nama Kolom, jagi begini..!!!
                FilterJenisPajak = Replace(FilterJenisPajak, "Pasal", "PPh Pasal")      'Karena Perbedaan Nama Kolom, jagi begini..!!!
                SumberData = SumberData_PencairanPiutangUsaha
                cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                      " WHERE Peruntukan = '" & Peruntukan_PencairanPiutangUsaha_NonAfiliasi & "' AND PPh_Terutang > 0 " &
                                      FilterJenisPajak & FilterBuktiPotong, KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                Do While dr.Read
                    NomorUrut += 1
                    NomorID = dr.Item("Nomor_ID")
                    NomorPatokan = KonversiNomorBPPUKeNomorPenjualan(dr.Item("Nomor_BP"))
                    KodeCustomer = dr.Item("Kode_Lawan_Transaksi")
                    NamaCustomer = dr.Item("Nama_Lawan_Transaksi")
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                                 " WHERE Nomor_Penjualan = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    drTELUSUR.Read()
                    Dim DPPPenjualan As Int64 = 0
                    Dim PiutangUsaha As Int64 = 0
                    If drTELUSUR.HasRows Then
                        NomorInvoice = drTELUSUR.Item("Nomor_Invoice")
                        TanggalInvoice = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_Invoice"))
                        NomorFakturPajak = drTELUSUR.Item("Nomor_Faktur_Pajak")
                        DPPPenjualan = drTELUSUR.Item("Dasar_Pengenaan_Pajak")
                        PiutangUsaha = drTELUSUR.Item("Jumlah_Piutang_Usaha")
                    End If
                    Dim JumlahBayar = dr.Item("Jumlah_Bayar")
                    DPP = JumlahBayar * DPPPenjualan / PiutangUsaha
                    JenisPPh = KonversiJenisPajakKeJenisPPh(dr.Item("Jenis_Pajak"))
                    JumlahPPh = dr.Item("PPh_Terutang")
                    TanggalDipotong = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
                    NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                    TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                    If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                    If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                    Keterangan = PenghapusEnter(dr.Item("Keterangan_Bukti_Potong"))
                    NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                    TambahBaris()
                    Await Task.Yield()
                Loop
            End If

            'PPh Dari Pencairan Piutang Pihak Ketiga :
            If PilihanSumberData = SumberData_PencairanPiutangPihakKetiga _
                Or PilihanSumberData = Pilihan_Semua Then
                SumberData = SumberData_PencairanPiutangPihakKetiga
                cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangPihakKetiga " &
                                      " WHERE Jumlah_PPh > 0 " &
                                      " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' " &
                                      FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                Do While dr.Read
                    NomorUrut += 1
                    NomorPatokan = dr.Item("Nomor_BPPPK")
                    NomorID = dr.Item("Nomor_ID")
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangPihakKetiga " &
                                             " WHERE Nomor_BPPPK = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    drTELUSUR.Read()
                    If drTELUSUR.HasRows Then
                        NomorInvoice = drTELUSUR.Item("Nomor_Kontrak")
                        TanggalInvoice = StripKosong
                        NomorFakturPajak = StripKosong
                    End If
                    KodeCustomer = dr.Item("Kode_Lawan_Transaksi")
                    NamaCustomer = AmbilValue_NamaMitra(KodeCustomer)
                    DPP = dr.Item("Pokok")
                    JenisPPh = dr.Item("Jenis_PPh")
                    JumlahPPh = dr.Item("Jumlah_PPh")
                    TanggalDipotong = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                    NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                    TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                    If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                    If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                    Keterangan = PenghapusEnter(dr.Item("Keterangan_Bukti_Potong"))
                    NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                    TambahBaris()
                    Await Task.Yield()
                Loop
            End If

            'PPh Dari Pencairan Piutang Afiliasi :
            If PilihanSumberData = SumberData_PencairanPiutangAfiliasi _
                Or PilihanSumberData = Pilihan_Semua Then
                SumberData = SumberData_PencairanPiutangAfiliasi
                cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangAfiliasi " &
                                      " WHERE Jumlah_PPh > 0 " &
                                      " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' " &
                                      FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                Do While dr.Read
                    NomorUrut += 1
                    NomorPatokan = dr.Item("Nomor_BPPA")
                    NomorID = dr.Item("Nomor_ID")
                    cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangAfiliasi " &
                                             " WHERE Nomor_BPPA = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                    drTELUSUR_ExecuteReader()
                    drTELUSUR.Read()
                    If drTELUSUR.HasRows Then
                        NomorInvoice = drTELUSUR.Item("Nomor_Kontrak")
                        TanggalInvoice = StripKosong
                        NomorFakturPajak = StripKosong
                    End If
                    KodeCustomer = dr.Item("Kode_Lawan_Transaksi")
                    NamaCustomer = AmbilValue_NamaMitra(KodeCustomer)
                    DPP = dr.Item("Pokok")
                    JenisPPh = dr.Item("Jenis_PPh")
                    JumlahPPh = dr.Item("Jumlah_PPh")
                    TanggalDipotong = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                    NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                    TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                    If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                    If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                    Keterangan = PenghapusEnter(dr.Item("Keterangan_Bukti_Potong"))
                    NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                    TambahBaris()
                    Await Task.Yield()
                Loop
            End If

            'PPh Dari Piutang Dividen :
            If PilihanSumberData = SumberData_PiutangDividen _
                Or PilihanSumberData = Pilihan_Semua Then
                SumberData = SumberData_PiutangDividen
                cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangDividen " &
                                      " WHERE PPh_Terutang > 0 " &
                                      FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                Do While dr.Read
                    NomorUrut += 1
                    NomorPatokan = dr.Item("Nomor_BPPD")
                    NomorID = dr.Item("Nomor_ID")
                    NomorInvoice = dr.Item("Nomor_Akta_Notaris")
                    TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                    NomorFakturPajak = StripKosong
                    KodeCustomer = dr.Item("Kode_Lawan_Transaksi")
                    NamaCustomer = AmbilValue_NamaMitra(KodeCustomer)
                    DPP = dr.Item("Jumlah_Dividen")
                    JenisPPh = dr.Item("Jenis_PPh")
                    JumlahPPh = dr.Item("PPh_Terutang")
                    TanggalDipotong = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                    NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                    TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                    If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                    If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                    Keterangan = PenghapusEnter(dr.Item("Keterangan_Bukti_Potong"))
                    NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                    TambahBaris()
                    Await Task.Yield()
                Loop
            End If

            AksesDatabase_Transaksi(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_BukuPengawasanBuktiPotongPPh_Prepaid")

        Finally
            BersihkanSeleksi()
            KetersediaanMenuHalaman(pnl_Halaman, True)
            SedangMemuatData = False
        End Try

    End Sub


    Public Sub TampilkanData()
        TampilkanDataAsync()
    End Sub


    Sub TambahBaris()
        datatabelUtama.Rows.Add(NomorUrut, SumberData, NomorID, NomorPatokan, NomorInvoice, TanggalInvoice, NomorFakturPajak, KodeCustomer, NamaCustomer,
                                DPP, JenisPPh, JumlahPPh, TanggalDipotong, NomorBuktiPotong, TanggalBuktiPotong, Keterangan, NomorJV)
    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        NomorUrut_Terseleksi = 0
        datagridUtama.SelectedIndex = -1
        btn_LihatJurnal.IsEnabled = False
        btn_Input.IsEnabled = False
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
    End Sub


    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True, False)
    End Sub


    Sub KontenCombo_SumberData()
        cmb_SumberData.Items.Clear()
        cmb_SumberData.Items.Add(Pilihan_Semua)
        cmb_SumberData.Items.Add(SumberData_Penjualan)
        cmb_SumberData.Items.Add(SumberData_PencairanPiutangUsaha)
        cmb_SumberData.Items.Add(SumberData_PencairanPiutangPihakKetiga)
        cmb_SumberData.Items.Add(SumberData_PencairanPiutangAfiliasi)
        cmb_SumberData.Items.Add(SumberData_PiutangDividen)
        cmb_SumberData.Text = Pilihan_Semua
    End Sub


    Sub KontenCombo_JenisPPh()
        cmb_JenisPPh.Items.Clear()
        cmb_JenisPPh.Items.Add(Pilihan_Semua)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal23)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal42)
        cmb_JenisPPh.Text = Pilihan_Semua
    End Sub


    Sub KontenCombo_BuktiPotong()
        cmb_BuktiPotong.Items.Clear()
        cmb_BuktiPotong.Items.Add(Pilihan_Semua)
        cmb_BuktiPotong.Items.Add(BP_SudahDiterima)
        cmb_BuktiPotong.Items.Add(BP_BelumDiterima)
        cmb_BuktiPotong.Text = Pilihan_Semua
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub cmb_SumberData_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_SumberData.SelectionChanged
        PilihanSumberData = cmb_SumberData.SelectedItem
        If ProsesLoadingForm Then Return
        TampilkanData()
    End Sub


    Private Sub cmb_JenisPPh_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPPh.SelectionChanged
        PilihanJenisPPh = cmb_JenisPPh.SelectedItem
        If ProsesLoadingForm Then Return
        TampilkanData()
    End Sub


    Private Sub cmb_BuktiPotong_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_BuktiPotong.SelectionChanged
        PilihanBuktiPotong = cmb_BuktiPotong.SelectedItem
        If ProsesLoadingForm Then Return
        TampilkanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputBuktiPotongPPh = New wpfWin_InputBuktiPotongPPh()
        win_InputBuktiPotongPPh.FungsiForm = FungsiForm_TAMBAH
        IsiValueFormInput()
        win_InputBuktiPotongPPh.ShowDialog()

    End Sub


    Sub IsiValueFormInput()
        win_InputBuktiPotongPPh.JalurMasuk = win_InputBuktiPotongPPh.JalurMasuk_Prepaid
        win_InputBuktiPotongPPh.SumberData = SumberData_Terseleksi
        win_InputBuktiPotongPPh.NomorID = NomorID_Terseleksi
        win_InputBuktiPotongPPh.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        win_InputBuktiPotongPPh.dtp_TanggalInvoice.SelectedDate = TanggalFormatWPF(TanggalInvoice_Terseleksi)
        win_InputBuktiPotongPPh.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        win_InputBuktiPotongPPh.KodeLawanTransaksi = KodeCustomer_Terseleksi
        win_InputBuktiPotongPPh.txt_NamaLawanTransaksi.Text = NamaCustomer_Terseleksi
        win_InputBuktiPotongPPh.txt_DPP.Text = DPP_Terseleksi
        win_InputBuktiPotongPPh.txt_JenisPPh.Text = JenisPPh_Terseleksi
        win_InputBuktiPotongPPh.txt_PPhTerutang.Text = JumlahPPh_Terseleksi
        win_InputBuktiPotongPPh.dtp_TanggalDipotong.SelectedDate = TanggalFormatWPF(TanggalDipotong_Terseleksi)
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        win_InputBuktiPotongPPh = New wpfWin_InputBuktiPotongPPh()
        win_InputBuktiPotongPPh.FungsiForm = FungsiForm_EDIT
        IsiValueFormInput()
        win_InputBuktiPotongPPh.txt_NomorBuktiPotong.Text = NomorBuktiPotong_Terseleksi
        win_InputBuktiPotongPPh.dtp_TanggalBuktiPotong.SelectedDate = TanggalFormatWPF(TanggalBuktiPotong_Terseleksi)
        IsiValueElemenRichTextBox(win_InputBuktiPotongPPh.txt_Keterangan, Keterangan_Terseleksi)
        win_InputBuktiPotongPPh.NomorJV_BuktiPotong = NomorJV_Terseleksi
        win_InputBuktiPotongPPh.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data 'Bukti Potong' pada baris terpilih?") Then Return

        Dim QueryAwal = Kosongan
        Dim QueryAkhir = Kosongan
        Select Case SumberData_Terseleksi
            Case SumberData_Penjualan
                QueryAwal = "  UPDATE tbl_Penjualan_Invoice "
                QueryAkhir = " WHERE Nomor_Invoice     = '" & NomorInvoice_Terseleksi & "' "
            Case SumberData_PencairanPiutangUsaha
                QueryAwal = "  UPDATE tbl_BuktiPenerimaan "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PencairanPiutangPihakKetiga
                QueryAwal = "  UPDATE tbl_JadwalAngsuranPiutangPihakKetiga "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PencairanPiutangAfiliasi
                QueryAwal = "  UPDATE tbl_JadwalAngsuranPiutangAfiliasi "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PiutangDividen
                QueryAwal = "  UPDATE tbl_PengawasanPiutangDividen "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
        End Select
        Dim KolomKolomYangDiedit = " SET " &
            " Nomor_Bukti_Potong      = '" & Kosongan & "', " &
            " Tanggal_Bukti_Potong    = '" & TanggalKosongSimpan & "', " &
            " Keterangan_Bukti_Potong = '" & Keterangan & "', " &
            " Nomor_JV_Bukti_Potong   = 0 "
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryAwal & KolomKolomYangDiedit & QueryAkhir, KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        'Hapus Jurnal yang lama :
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_Terseleksi & "' ", KoneksiDatabaseTransaksi)
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
        If NomorUrut_Terseleksi = 0 Then
            BersihkanSeleksi()
            Return
        End If
        SumberData_Terseleksi = rowviewUtama("Sumber_Data")
        NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID"))
        NomorPatokan_Terseleksi = rowviewUtama("Nomor_Patokan")
        NomorInvoice_Terseleksi = rowviewUtama("Nomor_Invoice")
        TanggalInvoice_Terseleksi = rowviewUtama("Tanggal_Invoice")
        NomorFakturPajak_Terseleksi = rowviewUtama("Nomor_Faktur_Pajak")
        KodeCustomer_Terseleksi = rowviewUtama("Kode_Customer")
        NamaCustomer_Terseleksi = rowviewUtama("Nama_Customer")
        DPP_Terseleksi = AmbilAngka(rowviewUtama("DPP_"))
        JenisPPh_Terseleksi = rowviewUtama("Jenis_PPh")
        JumlahPPh_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_PPh"))
        TanggalDipotong_Terseleksi = rowviewUtama("Tanggal_Dipotong")
        NomorBuktiPotong_Terseleksi = rowviewUtama("Nomor_Bukti_Potong")
        TanggalBuktiPotong_Terseleksi = rowviewUtama("Tanggal_Bukti_Potong")
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        NomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))

        If NomorUrut_Terseleksi > 0 Then
            If NomorJV_Terseleksi > 0 Then
                btn_LihatJurnal.IsEnabled = True
            Else
                btn_LihatJurnal.IsEnabled = False
            End If
            If NomorBuktiPotong_Terseleksi <> StripKosong Then
                btn_Input.IsEnabled = False
                btn_Edit.IsEnabled = True
                btn_Hapus.IsEnabled = True
            Else
                btn_Input.IsEnabled = True
                btn_Edit.IsEnabled = False
                btn_Hapus.IsEnabled = False
            End If
        Else
            BersihkanSeleksi()
        End If

    End Sub

    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If datatabelUtama.Rows.Count = 0 Then Return
        If BarisTerseleksi < 0 Then Return
        If NomorBuktiPotong_Terseleksi <> StripKosong Then
            btn_Edit_Click(sender, e)
        Else
            btn_Input_Click(sender, e)
        End If
    End Sub

    Private Sub wpfUsc_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
    End Sub


    'Tabel Utama :
    Dim Nomor_Urut As New DataGridTextColumn
    Dim Sumber_Data As New DataGridTextColumn
    Dim Nomor_ID As New DataGridTextColumn
    Dim Nomor_Patokan As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Kode_Customer As New DataGridTextColumn
    Dim Nama_Customer As New DataGridTextColumn
    Dim DPP_ As New DataGridTextColumn
    Dim Jenis_PPh As New DataGridTextColumn
    Dim Jumlah_PPh As New DataGridTextColumn
    Dim Tanggal_Dipotong As New DataGridTextColumn
    Dim Nomor_Bukti_Potong As New DataGridTextColumn
    Dim Tanggal_Bukti_Potong As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Nomor_JV As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Sumber_Data")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Nomor_Patokan")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Kode_Customer")
        datatabelUtama.Columns.Add("Nama_Customer")
        datatabelUtama.Columns.Add("DPP_", GetType(Int64))
        datatabelUtama.Columns.Add("Jenis_PPh")
        datatabelUtama.Columns.Add("Jumlah_PPh", GetType(Int64))
        datatabelUtama.Columns.Add("Tanggal_Dipotong")
        datatabelUtama.Columns.Add("Nomor_Bukti_Potong")
        datatabelUtama.Columns.Add("Tanggal_Bukti_Potong")
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Nomor_JV")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sumber_Data, "Sumber_Data", "Sumber Data", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Patokan, "Nomor_Patokan", "Nomor Patokan", 120, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 90, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 120, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Customer, "Kode_Customer", "Kode Customer", 90, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Customer, "Nama_Customer", "Nama Customer", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, DPP_, "DPP_", "DPP", 100, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_PPh, "Jenis_PPh", "Jenis PPh", 80, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_PPh, "Jumlah_PPh", "Jumlah PPh", 100, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Dipotong, "Tanggal_Dipotong", "Tanggal Dipotong", 90, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Bukti_Potong, "Nomor_Bukti_Potong", "Nomor Bukti Potong", 130, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Bukti_Potong, "Tanggal_Bukti_Potong", "Tanggal Bukti Potong", 90, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 250, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor JV", 70, FormatAngka, KananTengah, KunciUrut, Tersembunyi)

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

End Class
