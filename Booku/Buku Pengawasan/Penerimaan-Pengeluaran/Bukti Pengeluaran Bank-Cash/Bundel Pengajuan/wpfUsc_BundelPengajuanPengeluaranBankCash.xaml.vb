Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm


Public Class wpfUsc_BundelPengajuanPengeluaranBankCash

    Public StatusAktif As Boolean = False

    'Variabel Umum :
    Public TotalPengajuan
    Public TotalDisetujui
    Public TotalCashAdvance

    'Variabel Bundel :
    Dim bundel_NomorUrut
    Dim bundel_AngkaBundel
    Dim bundel_NomorBundel
    Dim bundel_AngkaBundel_Sebelumnya
    Dim bundel_TanggalBundel
    Dim bundel_JumlahLembarPengajuan
    Dim bundel_TotalPengajuan
    Dim bundel_TotalDisetujui
    Dim bundel_Status

    Dim bundel_NomorUrut_Terseleksi
    Dim bundel_AngkaBundel_Terseleksi
    Dim bundel_NomorBundel_Terseleksi
    Dim bundel_TanggalBundel_Terseleksi
    Dim bundel_JumlahLembarPengajuan_Terseleksi
    Dim bundel_TotalPengajuan_Terseleksi
    Dim bundel_TotalDisetujui_Terseleksi
    Dim bundel_Status_Terseleksi

    'Variabel Pengajuan :
    Dim pengajuan_NomorUrut
    Dim pengajuan_AngkaKK
    Dim pengajuan_NomorKK
    Dim pengajuan_KodeLawanTransaksi
    Dim pengajuan_NamaLawanTransaksi
    Dim pengajuan_JumlahLembarInvoice
    Dim pengajuan_JumlahPengajuan
    Dim pengajuan_JumlahDisetujui
    Dim pengajuan_SaranaPembayaran
    Dim pengajuan_Status

    Dim pengajuan_NomorUrut_Terseleksi
    Dim pengajuan_AngkaKK_Terseleksi
    Dim pengajuan_NomorKK_Terseleksi
    Dim pengajuan_KodeLawanTransaksi_Terseleksi
    Dim pengajuan_NamaLawanTransaksi_Terseleksi
    Dim pengajuan_JumlahLembarInvoice_Terseleksi
    Dim pengajuan_JumlahPengajuan_Terseleksi
    Dim pengajuan_JumlahDisetujui_Terseleksi
    Dim pengajuan_SaranaPembayaran_Terseleksi
    Dim pengajuan_Status_Terseleksi

    Public KesesuaianJurnal As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_BundelPengajuanPengeluaranBankCash.JudulForm

        RefreshTampilanData()

        txt_TotalPengajuan.IsReadOnly = True
        txt_TotalDisetujui.IsReadOnly = True
        txt_TotalCashAdvance.IsReadOnly = True

        btn_Edit.Visibility = Visibility.Collapsed

        ProsesLoadingForm = False

    End Sub


    Sub RefreshTampilanData()
        TampilkanData_Bundel()
    End Sub

    Sub TampilkanData_Bundel()

        KesesuaianJurnal = True

        'Data Tabel :
        datatabelBundel.Clear()
        Dim i = 0
        bundel_AngkaBundel_Sebelumnya = 0
        bundel_NomorUrut = 0
        bundel_TotalPengajuan = 0
        bundel_TotalDisetujui = 0
        bundel_JumlahLembarPengajuan = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_BundelPengajuanPengeluaran " &
                              " ORDER BY Angka_Bundel ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read
            bundel_AngkaBundel = dr.Item("Angka_Bundel")
            If i > 0 And bundel_AngkaBundel <> bundel_AngkaBundel_Sebelumnya Then TambahBaris_Bundel()
            bundel_NomorBundel = dr.Item("Nomor_Bundel")
            bundel_TanggalBundel = TanggalFormatTampilan(dr.Item("Tanggal_Bundel"))
            bundel_TotalPengajuan += dr.Item("Jumlah_Pengajuan_Per_Baris")
            bundel_TotalDisetujui += dr.Item("Jumlah_Disetujui_Per_Baris")
            bundel_Status = dr.Item("Status")
            bundel_JumlahLembarPengajuan += 1
            bundel_AngkaBundel_Sebelumnya = bundel_AngkaBundel
            i += 1
        Loop

        If i > 0 Then TambahBaris_Bundel()

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi_Bundel()

    End Sub

    Sub TambahBaris_Bundel()
        bundel_NomorUrut += 1
        datatabelBundel.Rows.Add(bundel_NomorUrut, bundel_AngkaBundel_Sebelumnya, bundel_NomorBundel, bundel_TanggalBundel,
                                 bundel_JumlahLembarPengajuan, bundel_TotalPengajuan, bundel_TotalDisetujui, bundel_Status)
        bundel_TotalPengajuan = 0
        bundel_TotalDisetujui = 0
        bundel_JumlahLembarPengajuan = 0
        Terabas()
    End Sub

    Sub BersihkanSeleksi_Bundel()
        JumlahBaris_Bundel = datatabelBundel.Rows.Count
        BarisTerseleksi_Bundel = -1
        datagridBundel.SelectedIndex = -1
        datagridBundel.SelectedItem = Nothing
        datagridBundel.SelectedCells.Clear()
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        btn_Cetak.IsEnabled = False
        pnl_Pengajuan.Visibility = Visibility.Collapsed
    End Sub


    Sub TampilkanData_Pengajuan()

        pnl_Pengajuan.Visibility = Visibility.Visible

        KesesuaianJurnal = True

        'Style Tabel :
        datatabelPengajuan.Rows.Clear()

        'Data Tabel :
        pengajuan_NomorUrut = 0

        pengajuan_JumlahPengajuan = 0
        pengajuan_JumlahDisetujui = 0
        TotalCashAdvance = 0

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BundelPengajuanPengeluaran " &
                              " WHERE Nomor_Bundel = '" & bundel_NomorBundel_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            pengajuan_NomorKK = dr.Item("Nomor_KK_Per_Baris")
            pengajuan_KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi_Per_Baris")
            pengajuan_NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi_Per_Baris")
            pengajuan_JumlahLembarInvoice = dr.Item("Jumlah_Invoice_Per_Baris")
            pengajuan_JumlahPengajuan = dr.Item("Jumlah_Pengajuan_Per_Baris")
            pengajuan_JumlahDisetujui = dr.Item("Jumlah_Disetujui_Per_Baris")
            pengajuan_SaranaPembayaran = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Kredit_Per_Baris"))
            cmdTELUSUR = New OdbcCommand(" SELECT Angka_KK, Status_Pengajuan, Denda FROM tbl_BuktiPengeluaran " &
                                         " WHERE Nomor_KK = '" & pengajuan_NomorKK & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            If drTELUSUR.HasRows Then
                pengajuan_AngkaKK = drTELUSUR.Item("Angka_KK")
                pengajuan_Status = drTELUSUR.Item("Status_Pengajuan")
            End If
            TambahBaris_Pengajuan()
        Loop

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi_Pengajuan()

        txt_TotalPengajuan.Text = bundel_TotalPengajuan_Terseleksi
        txt_TotalDisetujui.Text = bundel_TotalDisetujui_Terseleksi
        txt_TotalCashAdvance.Text = TotalCashAdvance
        Visibilitas_KolomTotal()

    End Sub

    Sub TambahBaris_Pengajuan()
        pengajuan_NomorUrut += 1
        If KonversiSaranaPembayaranKeCOA(pengajuan_SaranaPembayaran) = KodeTautanCOA_CashAdvance _
            And pengajuan_KodeLawanTransaksi <> KodeLawanTransaksi_Internal _
            Then
            TotalCashAdvance += pengajuan_JumlahPengajuan
        End If
        datatabelPengajuan.Rows.Add(pengajuan_NomorUrut, pengajuan_AngkaKK, pengajuan_NomorKK, pengajuan_KodeLawanTransaksi, pengajuan_NamaLawanTransaksi,
                                    pengajuan_JumlahLembarInvoice, pengajuan_JumlahPengajuan, pengajuan_JumlahDisetujui, pengajuan_SaranaPembayaran, pengajuan_Status)
    End Sub

    Sub BersihkanSeleksi_Pengajuan()
        JumlahBaris_Pengajuan = datatabelPengajuan.Rows.Count
        BarisTerseleksi_Pengajuan = -1
        datagridPengajuan.SelectedIndex = -1
        datagridPengajuan.SelectedItem = Nothing
        datagridPengajuan.SelectedCells.Clear()
        btn_UpdatePengajuan.IsEnabled = False
        btn_TolakPengajuan.IsEnabled = False
    End Sub

    Sub Visibilitas_KolomTotal()
        If TotalDisetujui > 0 Then
            lbl_TotalDisetujui.Visibility = Visibility.Visible
            txt_TotalDisetujui.Visibility = Visibility.Visible
        Else
            lbl_TotalDisetujui.Visibility = Visibility.Collapsed
            txt_TotalDisetujui.Visibility = Visibility.Collapsed
        End If
        If TotalCashAdvance > 0 Then
            lbl_TotalCashAdvance.Visibility = Visibility.Visible
            txt_TotalCashAdvance.Visibility = Visibility.Visible
            btn_PengajuanCashAdvance.Visibility = Visibility.Visible
        Else
            lbl_TotalCashAdvance.Visibility = Visibility.Collapsed
            txt_TotalCashAdvance.Visibility = Visibility.Collapsed
            btn_PengajuanCashAdvance.Visibility = Visibility.Collapsed
        End If
    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Tambah_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

        win_InputBundelPengajuanPengeluaranBankCash = New wpfWin_InputBundelPengajuanPengeluaranBankCash
        win_InputBundelPengajuanPengeluaranBankCash.ResetForm()
        win_InputBundelPengajuanPengeluaranBankCash.FungsiForm = FungsiForm_TAMBAH
        win_InputBundelPengajuanPengeluaranBankCash.JalurMasuk = JalurUtama
        win_InputBundelPengajuanPengeluaranBankCash.ShowDialog()

    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        PesanUntukProgrammer("Tombol EDIT tidak diperlukan...!!!")
        Return

        win_InputBundelPengajuanPengeluaranBankCash = New wpfWin_InputBundelPengajuanPengeluaranBankCash
        win_InputBundelPengajuanPengeluaranBankCash.ResetForm()
        win_InputBundelPengajuanPengeluaranBankCash.FungsiForm = FungsiForm_EDIT
        win_InputBundelPengajuanPengeluaranBankCash.JalurMasuk = JalurUtama
        win_InputBundelPengajuanPengeluaranBankCash.AngkaBundel = bundel_AngkaBundel_Terseleksi
        win_InputBundelPengajuanPengeluaranBankCash.txt_NomorBundel.Text = bundel_NomorBundel_Terseleksi
        win_InputBundelPengajuanPengeluaranBankCash.dtp_TanggalBundel.SelectedDate = TanggalFormatWPF(bundel_TanggalBundel_Terseleksi)
        win_InputBundelPengajuanPengeluaranBankCash.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Dengan menghapus bundelan, data pengajuan terkait masih tetap ada dan berubah kembali menjadi 'Open'." & Enter2Baris &
                               "Lanjutkan hapus bundelan?") Then Return

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_BundelPengajuanPengeluaran " &
                              " WHERE Angka_Bundel = '" & bundel_AngkaBundel_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        cmd = New OdbcCommand(" UPDATE tbl_BuktiPengeluaran SET " &
                              " Nomor_Bundel        = '" & Kosongan & "', " &
                              " Status_Invoice      = '" & Status_Open & "', " &
                              " Status_Pengajuan    = '" & Status_Open & "' " &
                              " WHERE Nomor_Bundel  = '" & bundel_NomorBundel_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Bundelan berhasil dihapus, dan data pengajuan dikembalikan statusnya menjadi 'Open'.")
            TampilkanData_Bundel()
            X_frm_BukuPengawasanBuktiPengeluaranBankCash_X.TampilkanData()
        Else
            Pesan_Peringatan("Bundelan gagal dihapus." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If


    End Sub


    Private Sub btn_Cetak_Click(sender As Object, e As RoutedEventArgs) Handles btn_Cetak.Click
        Cetak(JenisFormCetak_BundelanPengajuanPengeluaran, bundel_NomorBundel_Terseleksi, True, False)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataGridKeEXCEL(datagridBundel)
    End Sub


    Private Sub datagridBundel_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridBundel.SelectionChanged
    End Sub
    Private Sub datagridBundel_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridBundel.PreviewMouseLeftButtonUp
        HeaderKolom_Bundel = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom_Bundel IsNot Nothing Then
            BersihkanSeleksi_Bundel()
        End If
    End Sub
    Private Sub datagridBundel_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridBundel.SelectedCellsChanged

        KolomTerseleksi_Bundel = datagridBundel.CurrentColumn
        BarisTerseleksi_Bundel = datagridBundel.SelectedIndex
        If BarisTerseleksi_Bundel < 0 Then Return
        rowviewBundel = TryCast(datagridBundel.SelectedItem, DataRowView)
        If Not rowviewBundel IsNot Nothing Then Return

        bundel_NomorUrut_Terseleksi = AmbilAngka(rowviewBundel("Nomor_Urut"))
        bundel_AngkaBundel_Terseleksi = rowviewBundel("Angka_Bundel")
        bundel_NomorBundel_Terseleksi = rowviewBundel("Nomor_Bundel")
        bundel_TanggalBundel_Terseleksi = rowviewBundel("Tanggal_Bundel")
        bundel_JumlahLembarPengajuan_Terseleksi = rowviewBundel("Jumlah_Lembar_Pengajuan")
        bundel_TotalPengajuan_Terseleksi = rowviewBundel("Total_Pengajuan")
        bundel_TotalDisetujui_Terseleksi = rowviewBundel("Total_Disetujui")
        bundel_Status_Terseleksi = rowviewBundel("Status_")

        If bundel_NomorUrut_Terseleksi >= 0 Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
            btn_Cetak.IsEnabled = True
            TampilkanData_Pengajuan()
        Else
            BersihkanSeleksi_Bundel()
        End If

        If bundel_Status_Terseleksi = Status_Closed Then
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
            btn_Cetak.IsEnabled = False
            pnl_Pengajuan.IsEnabled = False
            btn_UpdatePengajuan.Visibility = Visibility.Collapsed
            btn_TolakPengajuan.Visibility = Visibility.Collapsed
        Else
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
            btn_Cetak.IsEnabled = True
            pnl_Pengajuan.IsEnabled = True
            btn_UpdatePengajuan.Visibility = Visibility.Visible
            btn_TolakPengajuan.Visibility = Visibility.Visible
        End If

    End Sub
    Private Sub datagridBundel_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridBundel.MouseDoubleClick
        If datatabelBundel.Rows.Count = 0 Then Return
        If BarisTerseleksi_Bundel < 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub



    'Event DataGridPengajuan, disini :
    Private Sub datagridPengajuan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridPengajuan.SelectionChanged
    End Sub
    Private Sub datagridPengajuan_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridPengajuan.PreviewMouseLeftButtonUp
        HeaderKolom_Pengajuan = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom_Pengajuan IsNot Nothing Then
            BersihkanSeleksi_Pengajuan()
        End If
    End Sub
    Private Sub datagridPengajuan_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridPengajuan.SelectedCellsChanged
        KolomTerseleksi_Pengajuan = datagridPengajuan.CurrentColumn
        BarisTerseleksi_Pengajuan = datagridPengajuan.SelectedIndex
        If BarisTerseleksi_Pengajuan < 0 Then Return
        rowviewPengajuan = TryCast(datagridPengajuan.SelectedItem, DataRowView)
        If Not rowviewPengajuan IsNot Nothing Then Return

        pengajuan_NomorUrut_Terseleksi = AmbilAngka(rowviewPengajuan("Nomor_Urut").ToString)
        pengajuan_AngkaKK_Terseleksi = AmbilAngka(rowviewPengajuan("Angka_KK").ToString)
        pengajuan_NomorKK_Terseleksi = rowviewPengajuan("Nomor_KK")
        pengajuan_Status_Terseleksi = rowviewPengajuan("Status_")

        If pengajuan_NomorUrut_Terseleksi >= 0 Then
            If bundel_Status_Terseleksi = Status_Open Then
                btn_UpdatePengajuan.IsEnabled = True
                btn_TolakPengajuan.IsEnabled = True
            End If
        Else
            BersihkanSeleksi_Pengajuan()
        End If
    End Sub
    Private Sub datagridPengajuan_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridPengajuan.MouseDoubleClick
        If datatabelPengajuan.Rows.Count = 0 Then Return
        If BarisTerseleksi_Pengajuan < 0 Then Return
        If bundel_Status_Terseleksi = Status_Closed Then Return
        btn_UpdatePengajuan_Click(sender, e)
    End Sub


    Private Sub txt_TotalPengajuan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalPengajuan.TextChanged
        TotalPengajuan = AmbilAngka(txt_TotalPengajuan.Text)
        PemecahRibuanUntukTextBox_WPF(txt_TotalPengajuan)
    End Sub

    Private Sub txt_TotalDisetujui_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalDisetujui.TextChanged
        TotalDisetujui = AmbilAngka(txt_TotalDisetujui.Text)
        PemecahRibuanUntukTextBox_WPF(txt_TotalDisetujui)
    End Sub

    Private Sub txt_TotalCashAdvance_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalCashAdvance.TextChanged
        TotalCashAdvance = AmbilAngka(txt_TotalCashAdvance.Text)
        PemecahRibuanUntukTextBox_WPF(txt_TotalCashAdvance)
    End Sub


    Private Sub btn_PengajuanCashAdvance_Click(sender As Object, e As RoutedEventArgs) Handles btn_PengajuanCashAdvance.Click

        frm_InputPemindahbukuan.ResetForm()
        frm_InputPemindahbukuan.FungsiForm = FungsiForm_TAMBAH
        frm_InputPemindahbukuan.dtp_TanggalBPPB.Value = bundel_TanggalBundel_Terseleksi
        frm_InputPemindahbukuan.txt_JumlahTransaksi.Text = TotalCashAdvance
        frm_InputPemindahbukuan.cmb_KeBuku.SelectedValue = KonversiCOAKeSaranaPembayaran(KodeTautanCOA_CashAdvance)
        frm_InputPemindahbukuan.ShowDialog()

    End Sub

    Private Sub btn_UpdatePengajuan_Click(sender As Object, e As RoutedEventArgs) Handles btn_UpdatePengajuan.Click
        win_InputBuktiPengeluaran = New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_UPDATEPERSETUJUAN
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.AngkaKK = pengajuan_AngkaKK_Terseleksi
        win_InputBuktiPengeluaran.ShowDialog()
        Dim X_BarisTerseleksi_Bundel = BarisTerseleksi_Bundel
        Dim X_BarisTerseleksi_Pengajuan = BarisTerseleksi_Pengajuan
        Dim X_bundel_NomorBundel_Terseleksi = bundel_NomorBundel_Terseleksi
        Dim X_pengajuan_NomorKK_Terseleksi = pengajuan_NomorKK_Terseleksi
        If win_InputBuktiPengeluaran.AdaPenyimpanan = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_BundelPengajuanPengeluaran " &
                                  " SET Jumlah_Disetujui_Per_Baris  = '" & win_InputBuktiPengeluaran.JumlahBayar_Total & "' " &
                                  " WHERE Nomor_KK_Per_Baris      = '" & pengajuan_NomorKK_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
            TampilkanData_Bundel()
            BarisTerseleksi_Bundel = X_BarisTerseleksi_Bundel
            BarisTerseleksi_Pengajuan = X_BarisTerseleksi_Pengajuan
            datagridBundel.SelectedIndex = BarisTerseleksi_Bundel
            datagridPengajuan.SelectedIndex = BarisTerseleksi_Pengajuan
            bundel_NomorBundel_Terseleksi = X_bundel_NomorBundel_Terseleksi
            pengajuan_NomorKK_Terseleksi = X_pengajuan_NomorKK_Terseleksi
            TampilkanData_Pengajuan()
        End If
    End Sub

    Private Sub btn_Tolak_Click(sender As Object, e As RoutedEventArgs) Handles btn_TolakPengajuan.Click
        If Not TanyaKonfirmasi("Yakin ingin menolak pengajuan terpilih?") Then Return

        AksesDatabase_Transaksi(Buka)
        cmdEDIT = New OdbcCommand(" UPDATE tbl_BuktiPengeluaran SET " &
                                  " Jumlah_Bayar        = 0, " &
                                  " Rekening_Penerima   = '', " &
                                  " Atas_Nama_Penerima  = '', " &
                                  " Status_Pengajuan    = '" & Status_Ditolak & "' " &
                                  " WHERE Angka_KK      = '" & pengajuan_AngkaKK_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmdEDIT_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            Pesan_Sukses("Pengajuan terpilih telah ditolak.")
            TampilkanData_Pengajuan()
        Else
            pesan_DataTerpilihGagalDiperbarui()
        End If
    End Sub

    Private Sub pnl_Bundel_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridBundel.SizeChanged
        'datagridBundel.Height = pnl_Bundel.Height - 69
        'PesanUntukProgrammer(pnl_Bundel.Height)
    End Sub


    Sub New()
        InitializeComponent()
        Buat_TabelBundel()
        Buat_TabelPengajuan()
    End Sub

    'Tabel Bundel :
    Public datatabelBundel As DataTable
    Public dataviewBundel As DataView
    Public rowviewBundel As DataRowView
    Public newRowBundel As DataRow
    Public HeaderKolom_Bundel As DataGridColumnHeader
    Public KolomTerseleksi_Bundel As DataGridColumn
    Public BarisTerseleksi_Bundel As Integer
    Public JumlahBaris_Bundel As Integer
    Dim bundel_Nomor_Urut As New DataGridTextColumn
    Dim bundel_Angka_Bundel As New DataGridTextColumn
    Dim bundel_Nomor_Bundel As New DataGridTextColumn
    Dim bundel_Tanggal_Bundel As New DataGridTextColumn
    Dim bundel_Jumlah_Lembar_Pengajuan As New DataGridTextColumn
    Dim bundel_Total_Pengajuan As New DataGridTextColumn
    Dim bundel_Total_Disetujui As New DataGridTextColumn
    Dim bundel_Status_ As New DataGridTextColumn
    Sub Buat_TabelBundel()

        datatabelBundel = New DataTable
        datatabelBundel.Columns.Add("Nomor_Urut")
        datatabelBundel.Columns.Add("Angka_Bundel")
        datatabelBundel.Columns.Add("Nomor_Bundel")
        datatabelBundel.Columns.Add("Tanggal_Bundel")
        datatabelBundel.Columns.Add("Jumlah_Lembar_Pengajuan", GetType(Int64))
        datatabelBundel.Columns.Add("Total_Pengajuan", GetType(Int64))
        datatabelBundel.Columns.Add("Total_Disetujui", GetType(Int64))
        datatabelBundel.Columns.Add("Status_")

        StyleTabelUtama_WPF(datagridBundel, datatabelBundel, dataviewBundel)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBundel, bundel_Nomor_Urut, "Nomor_Urut", "No.", 39, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBundel, bundel_Angka_Bundel, "Angka_Bundel", "Angka Bundel", 63, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBundel, bundel_Nomor_Bundel, "Nomor_Bundel", "Nomor Bundel", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBundel, bundel_Tanggal_Bundel, "Tanggal_Bundel", "Tanggal Bundel", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBundel, bundel_Jumlah_Lembar_Pengajuan, "Jumlah_Lembar_Pengajuan", "Jumlah Lembar", 63, FormatAngka, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBundel, bundel_Total_Pengajuan, "Total_Pengajuan", "Total Pengajuan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBundel, bundel_Total_Disetujui, "Total_Disetujui", "Total Disetujui", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridBundel, bundel_Status_, "Status_", "Status", 51, FormatString, KiriTengah, KunciUrut, Terlihat)

        datagridBundel.Height = 444

    End Sub

    'Tabel Pengajuan :
    Public datatabelPengajuan As DataTable
    Public dataviewPengajuan As DataView
    Public rowviewPengajuan As DataRowView
    Public newRowPengajuan As DataRow
    Public HeaderKolom_Pengajuan As DataGridColumnHeader
    Public KolomTerseleksi_Pengajuan As DataGridColumn
    Public BarisTerseleksi_Pengajuan As Integer
    Public JumlahBaris_Pengajuan As Integer
    Dim pengajuan_Nomor_Urut As New DataGridTextColumn
    Dim pengajuan_Angka_KK As New DataGridTextColumn
    Dim pengajuan_Nomor_KK As New DataGridTextColumn
    Dim pengajuan_Kode_Lawan_Transaksi As New DataGridTextColumn
    Dim pengajuan_Nama_Lawan_Transaksi As New DataGridTextColumn
    Dim pengajuan_Jumlah_Lembar_Invoice As New DataGridTextColumn
    Dim pengajuan_Jumlah_Pengajuan As New DataGridTextColumn
    Dim pengajuan_Jumlah_Disetujui As New DataGridTextColumn
    Dim pengajuan_Sarana_Pembayaran As New DataGridTextColumn
    Dim pengajuan_Status_ As New DataGridTextColumn
    Sub Buat_TabelPengajuan()

        datatabelPengajuan = New DataTable
        datatabelPengajuan.Columns.Add("Nomor_Urut")
        datatabelPengajuan.Columns.Add("Angka_KK")
        datatabelPengajuan.Columns.Add("Nomor_KK")
        datatabelPengajuan.Columns.Add("Kode_Lawan_Transaksi")
        datatabelPengajuan.Columns.Add("Nama_Lawan_Transaksi")
        datatabelPengajuan.Columns.Add("Jumlah_Lembar_Invoice", GetType(Int64))
        datatabelPengajuan.Columns.Add("Jumlah_Pengajuan", GetType(Int64))
        datatabelPengajuan.Columns.Add("Jumlah_Disetujui", GetType(Int64))
        datatabelPengajuan.Columns.Add("Sarana_Pembayaran")
        datatabelPengajuan.Columns.Add("Status_")
        StyleTabelUtama_WPF(datagridPengajuan, datatabelPengajuan, dataviewPengajuan)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Nomor_Urut, "Nomor_Urut", "No.", 39, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Angka_KK, "Angka_KK", "Angka KK", 39, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Nomor_KK, "Nomor_KK", "Nomor Pengajuan", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Kode_Lawan_Transaksi, "Kode_Lawan_Transaksi", "Kode Lawan Transaksi", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Nama_Lawan_Transaksi, "Nama_Lawan_Transaksi", "Nama Lawan Transaksi", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Jumlah_Lembar_Invoice, "Jumlah_Lembar_Invoice", "Jumlah Lembar", 63, FormatAngka, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Jumlah_Pengajuan, "Jumlah_Pengajuan", "Jumlah Pengajuan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Jumlah_Disetujui, "Jumlah_Disetujui", "Jumlah Disetujui", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Sarana_Pembayaran, "Sarana_Pembayaran", "Sarana Pembayaran", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPengajuan, pengajuan_Status_, "Status_", "Status", 81, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub

    Sub datagridPengajuan_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridPengajuan.SizeChanged
        KetentuanUkuran()
    End Sub
    Sub datagridBundel_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridBundel.SizeChanged
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
        datagridBundel.Height = TinggiKonten
        datagridPengajuan.MaxHeight = TinggiKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        StatusAktif = False
    End Sub

End Class