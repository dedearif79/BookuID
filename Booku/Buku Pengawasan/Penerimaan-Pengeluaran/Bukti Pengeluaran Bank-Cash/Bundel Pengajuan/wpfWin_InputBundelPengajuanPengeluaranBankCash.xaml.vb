Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports bcomm

Public Class wpfWin_InputBundelPengajuanPengeluaranBankCash

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Public AngkaBundel
    Dim NomorBundel
    Dim TanggalBundel
    Dim TotalPengajuan
    Dim Catatan

    Dim NomorUrutPengajuan
    Dim NomorKKPerBaris
    Dim KodeLawanTransaksiPerBaris
    Dim NamaLawanTransaksiPerbaris
    Dim JumlahInvoicePerBaris
    Dim JumlahPengajuanPerBaris
    Dim DendaPerBaris
    Dim SaranaPembayaranPerBaris

    Dim NomorUrutPerBaris_Terseleksi
    Dim NomorKKPerBaris_Terseleksi
    Dim KodeLawanTransaksiPerBaris_Terseleksi
    Dim NamaLawanTransaksiPerbaris_Terseleksi
    Dim JumlahInvoicePerBaris_Terseleksi
    Dim JumlahPengajuanPerBaris_Terseleksi
    Dim SaranaPembayaranPerBaris_Terseleksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Bundel Pengajuan Pengeluaran Bank-Cash"
            dtp_TanggalBundel.IsEnabled = True
            btn_Simpan.Content = teks_Simpan
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Input Bundel Pengajuan Pengeluaran Bank-Cash"
            dtp_TanggalBundel.IsEnabled = False
            btn_Simpan.Content = teks_Perbarui
        End If

        ProsesLoadingForm = False

        BersihkanSeleksi()

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        JalurMasuk = Kosongan
        JumlahBarisTabel = 0

        txt_NomorBundel.IsEnabled = False
        KosongkanDatePicker(dtp_TanggalBundel)
        txt_TotalPengajuan.IsEnabled = False
        KosongkanValueElemenRichTextBox(txt_Catatan)
        VisibilitasTabel()

        ProsesResetForm = False

    End Sub

    Sub SistemPenomoranOtomatis_Bundel()

        If dtp_TanggalBundel.Text <> Kosongan Then
            If FungsiForm = FungsiForm_TAMBAH Then
                AngkaBundel = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_BundelPengajuanPengeluaran", "Angka_Bundel") + 1
                NomorBundel = AwalanBundelKK & AngkaBundel & "/" & BulanRomawi(dtp_TanggalBundel.SelectedDate.Value.Month) & "/" & TahunBukuAktif
                txt_NomorBundel.Text = NomorBundel
            End If
        End If

    End Sub

    Sub TampilkanData()

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Data Tabel :
        Dim i = 0
        Dim AngkaKK
        Dim AngkaKK_Sebelumnya = 0
        NomorUrutPengajuan = 0

        TotalPengajuan = 0
        JumlahInvoicePerBaris = 0

        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                  " WHERE Tanggal_KK      LIKE '" & TanggalFormatSimpan(TanggalBundel) & "%' " &
                                  " AND Nomor_Bundel        = '' " &
                                  " AND Status_Pengajuan    = '" & Status_Dicetak & "' " &
                                  " ORDER BY Angka_KK ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                AngkaKK = dr.Item("Angka_KK")
                If i > 0 And AngkaKK <> AngkaKK_Sebelumnya Then TambahBaris()
                NomorKKPerBaris = dr.Item("Nomor_KK")
                KodeLawanTransaksiPerBaris = dr.Item("Kode_Lawan_Transaksi")
                NamaLawanTransaksiPerbaris = dr.Item("Nama_Lawan_Transaksi")
                JumlahPengajuanPerBaris += KonversiDesimalKeInt64BulatKeAtas(dr.Item("Kurs") * dr.Item("Jumlah_Pengajuan"))
                DendaPerBaris = dr.Item("Denda") '(Value Denda jangan diakumulatif).
                SaranaPembayaranPerBaris = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Kredit"))
                AngkaKK_Sebelumnya = AngkaKK
                i += 1
                JumlahInvoicePerBaris += 1
            Loop
            If i > 0 Then TambahBaris()
            AksesDatabase_Transaksi(Tutup)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BundelPengajuanPengeluaran " &
                                  " WHERE Angka_Bundel = '" & AngkaBundel & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorKKPerBaris = dr.Item("Nomor_KK_Per_Baris")
                KodeLawanTransaksiPerBaris = dr.Item("Kode_Lawan_Transaksi_Per_Baris")
                NamaLawanTransaksiPerbaris = dr.Item("Nama_Lawan_Transaksi_Per_Baris")
                JumlahPengajuanPerBaris = dr.Item("Jumlah_Pengajuan_Per_Baris")
                JumlahInvoicePerBaris = dr.Item("Jumlah_Invoice_Per_Baris")
                SaranaPembayaranPerBaris = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Kredit_Per_Baris"))
                TambahBaris()
            Loop
            AksesDatabase_Transaksi(Tutup)
        End If

        BersihkanSeleksi()

        VisibilitasTabel()

        If JumlahBarisTabel = 0 Then PesanPemberitahuan("Tidak ada data yang bisa diajukan untuk tanggal " & TanggalBundel & ".")


    End Sub

    Sub TambahBaris()
        NomorUrutPengajuan += 1
        JumlahPengajuanPerBaris += DendaPerBaris
        TotalPengajuan += JumlahPengajuanPerBaris
        datatabelUtama.Rows.Add(NomorUrutPengajuan, NomorKKPerBaris, KodeLawanTransaksiPerBaris, NamaLawanTransaksiPerbaris,
                                     JumlahInvoicePerBaris, JumlahPengajuanPerBaris, SaranaPembayaranPerBaris)
        JumlahPengajuanPerBaris = 0
        JumlahInvoicePerBaris = 0
    End Sub


    Sub BersihkanSeleksi()
        JumlahBarisTabel = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
    End Sub


    Sub VisibilitasTabel()
        If JumlahBarisTabel > 0 Then
            pnl_Kanan.Visibility = Visibility.Visible
            pnl_Pemisah.Visibility = Visibility.Visible
            txt_TotalPengajuan.Text = TotalPengajuan
            btn_Pratinjau.Visibility = Visibility.Visible
        Else
            pnl_Kanan.Visibility = Visibility.Collapsed
            pnl_Pemisah.Visibility = Visibility.Collapsed
            txt_TotalPengajuan.Text = Kosongan
            btn_Pratinjau.Visibility = Visibility.Collapsed
        End If

    End Sub


    Private Sub dtp_TanggalBundel_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalBundel.SelectedDateChanged
        If dtp_TanggalBundel.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalBundel)
            TanggalBundel = TanggalFormatTampilan(dtp_TanggalBundel.SelectedDate)
            If ProsesLoadingForm = False Then SistemPenomoranOtomatis_Bundel()
            TampilkanData()
        End If
    End Sub


    Private Sub txt_NomorBundel_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBundel.TextChanged
        NomorBundel = txt_NomorBundel.Text
    End Sub


    Private Sub txt_TotalPengajuan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TotalPengajuan.TextChanged
        TotalPengajuan = txt_TotalPengajuan.Text
        PemecahRibuanUntukTextBox_WPF(txt_TotalPengajuan)
    End Sub


    Private Sub txt_Catatan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Catatan.TextChanged
        Catatan = IsiValueVariabelRichTextBox(txt_Catatan)
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

        NomorUrutPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Nomor_Urut_Pengajuan"))
        NomorKKPerBaris_Terseleksi = rowviewUtama("Nomor_KK_Per_Baris")
        KodeLawanTransaksiPerBaris_Terseleksi = rowviewUtama("Kode_Lawan_Transaksi_Per_Baris")
        NamaLawanTransaksiPerbaris_Terseleksi = rowviewUtama("Nama_Lawan_Transaksi_Per_Baris")
        JumlahInvoicePerBaris_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Invoice_Per_Baris"))
        JumlahPengajuanPerBaris_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Pengajuan_Per_Baris"))
        SaranaPembayaranPerBaris_Terseleksi = rowviewUtama("Sarana_Pembayaran_Per_Baris")

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        'Untuk Saat ini, belum ada Coding di sini...!
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Validasi Form :
        If dtp_TanggalBundel.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Bundel'.")
            dtp_TanggalBundel.Focus()
            Return
        End If

        If JumlahBarisTabel = 0 Then
            PesanPeringatan("Silakan isi Baris Pengajuan")
            pnl_Kanan.Visibility = Visibility.Visible
            pnl_Pemisah.Visibility = Visibility.Visible
            txt_TotalPengajuan.Text = TotalPengajuan
            Return
        End If

        StatusSuntingDatabase = True

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_BundelPengajuanPengeluaran")


        If FungsiForm = FungsiForm_TAMBAH Then
            'Belum ada kebutuhan coding disini.
        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" DELETE FROM tbl_BundelPengajuanPengeluaran " &
                                  " WHERE Angka_Bundel = '" & AngkaBundel & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        AksesDatabase_Transaksi(Buka)

        For Each row As DataRow In datatabelUtama.Rows

            NomorID += 1
            NomorKKPerBaris = row("Nomor_KK_Per_Baris")
            KodeLawanTransaksiPerBaris = row("Kode_Lawan_Transaksi_Per_Baris")
            NamaLawanTransaksiPerbaris = row("Nama_Lawan_Transaksi_Per_Baris")
            JumlahInvoicePerBaris = AmbilAngka(row("Jumlah_Invoice_Per_Baris").ToString)
            JumlahPengajuanPerBaris = AmbilAngka(row("Jumlah_Pengajuan_Per_Baris").ToString)
            SaranaPembayaranPerBaris = row("Sarana_Pembayaran_Per_Baris")

            cmd = New OdbcCommand(" INSERT INTO tbl_BundelPengajuanPengeluaran VALUES (" &
                                  " '" & NomorID & "', " &
                                  " '" & AngkaBundel & "', " &
                                  " '" & NomorBundel & "', " &
                                  " '" & TanggalFormatSimpan(TanggalBundel) & "', " &
                                  " '" & NomorKKPerBaris & "', " &
                                  " '" & KodeLawanTransaksiPerBaris & "', " &
                                  " '" & NamaLawanTransaksiPerbaris & "', " &
                                  " '" & JumlahInvoicePerBaris & "', " &
                                  " '" & JumlahPengajuanPerBaris & "', " &
                                  " '" & 0 & "', " &
                                  " '" & KonversiSaranaPembayaranKeCOA(SaranaPembayaranPerBaris) & "', " &
                                  " '" & Status_Open & "', " &
                                  " '" & UserAktif & "' ) ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            cmd = New OdbcCommand(" UPDATE tbl_BuktiPengeluaran SET " &
                                  " Nomor_Bundel        = '" & NomorBundel & "', " &
                                  " Status_Invoice      = '" & Status_Dibundel & "', " &
                                  " Status_Pengajuan    = '" & Status_Dibundel & "' " &
                                  " WHERE Nomor_KK    = '" & NomorKKPerBaris & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

        Next

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_BundelPengajuanPengeluaranBankCash.StatusAktif Then usc_BundelPengajuanPengeluaranBankCash.TampilkanData_Bundel()
            If usc_BukuPengawasanBuktiPengeluaranBankCash.StatusAktif Then usc_BukuPengawasanBuktiPengeluaranBankCash.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Private Sub btn_Pratinjau_Click(sender As Object, e As RoutedEventArgs) Handles btn_Pratinjau.Click

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
    End Sub

    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBarisTabel As Integer


    Dim Nomor_Urut_Pengajuan As New DataGridTextColumn
    Dim Nomor_KK_Per_Baris As New DataGridTextColumn
    Dim Kode_Lawan_Transaksi_Per_Baris As New DataGridTextColumn
    Dim Nama_Lawan_Transaksi_Per_Baris As New DataGridTextColumn
    Dim Jumlah_Invoice_Per_Baris As New DataGridTextColumn
    Dim Jumlah_Pengajuan_Per_Baris As New DataGridTextColumn
    Dim Sarana_Pembayaran_Per_Baris As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut_Pengajuan")
        datatabelUtama.Columns.Add("Nomor_KK_Per_Baris")
        datatabelUtama.Columns.Add("Kode_Lawan_Transaksi_Per_Baris")
        datatabelUtama.Columns.Add("Nama_Lawan_Transaksi_Per_Baris")
        datatabelUtama.Columns.Add("Jumlah_Invoice_Per_Baris")
        datatabelUtama.Columns.Add("Jumlah_Pengajuan_Per_Baris", GetType(Int64))
        datatabelUtama.Columns.Add("Sarana_Pembayaran_Per_Baris")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut_Pengajuan, "Nomor_Urut_Pengajuan", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_KK_Per_Baris, "Nomor_KK_Per_Baris", "Nomor KK", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Lawan_Transaksi_Per_Baris, "Kode_Lawan_Transaksi_Per_Baris", "Kode Lawan Transaksi", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Lawan_Transaksi_Per_Baris, "Nama_Lawan_Transaksi_Per_Baris", "Nama Lawan Transaksi", 165, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Invoice_Per_Baris, "Jumlah_Invoice_Per_Baris", "Jumlah Invoice", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Pengajuan_Per_Baris, "Jumlah_Pengajuan_Per_Baris", "Jumlah Pengajuan", 87, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Sarana_Pembayaran_Per_Baris, "Sarana_Pembayaran_Per_Baris", "Sarana Pembayaran", 150, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub

End Class
