Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports bcomm

Public Class wpfWin_InputDepositOperasional

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorJV
    Public KodeMataUang

    Public NomorID
    Public AngkaBPDO
    Dim NomorBPDO
    Dim NomorBukti
    Dim TanggalBukti
    Dim NomorFakturPajak
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UraianTransaksi
    Dim KodeCustomer
    Dim NamaCustomer
    Dim JumlahTransaksi
    Dim Keterangan

    Dim NomorUrutProduk
    Dim COAProduk
    Dim NamaProduk
    Dim NomorReferensiProduk
    Dim TanggalReferensiProduk
    Dim JumlahHargaProduk

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Deposit Operasional"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Or FungsiForm = FungsiForm_LIHAT Then
            JudulForm = "Edit Deposit Operasional"
            TampilkanData()
        End If

        If FungsiForm = FungsiForm_LIHAT Then
            btn_Simpan.IsEnabled = False
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")


        Me.Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID = 0
        AngkaBPDO = 0
        txt_NomorBPDO.Text = Kosongan
        txt_NomorBukti.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalBukti)
        txt_NomorFakturPajak.Text = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        datatabelUtama.Rows.Clear()
        txt_JumlahTransaksi.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)
        btn_Simpan.IsEnabled = True
        JumlahBaris = 0

        ProsesResetForm = False

    End Sub


    Sub SistemPenomoranOtomatis()
        AngkaBPDO = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_DepositOperasional", "Angka_BPDO") + 1
        NomorBPDO = AwalanBPDO_PlusTahunBuku & AngkaBPDO
        txt_NomorBPDO.Text = NomorBPDO
    End Sub


    Sub TampilkanData()

        'Style Tabel :
        Terabas()
        datatabelUtama.Rows.Clear()

        'Data Tabel :
        NomorUrutProduk = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_DepositOperasional " &
                              " WHERE Nomor_BPDO = '" & NomorBPDO & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            COAProduk = dr.Item("COA_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            NomorReferensiProduk = dr.Item("Nomor_Referensi_Produk")
            TanggalReferensiProduk = TanggalFormatTampilan(dr.Item("Tanggal_Referensi_Produk"))
            JumlahHargaProduk = dr.Item("Jumlah_Harga_Produk")
            TambahBaris()
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        NomorUrutProduk += 1
        datatabelUtama.Rows.Add(NomorUrutProduk, COAProduk, NamaProduk, NomorReferensiProduk, TanggalReferensiProduk, JumlahHargaProduk)
    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_Perbaiki.IsEnabled = False
        btn_Singkirkan.IsEnabled = False
        Perhitungan()
    End Sub


    Sub Perhitungan()
        JumlahTransaksi = 0
        For Each row As DataRow In datatabelUtama.Rows
            JumlahTransaksi += AmbilAngka(row("Jumlah_Harga_Produk"))
        Next
        txt_JumlahTransaksi.Text = JumlahTransaksi
    End Sub


    Private Sub txt_NomorBPDO_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBPDO.TextChanged
        NomorBPDO = txt_NomorBPDO.Text
    End Sub


    Private Sub txt_NomorBukti_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBukti.TextChanged
        NomorBukti = txt_NomorBukti.Text
    End Sub


    Private Sub dtp_TanggalBukti_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalBukti.SelectedDateChanged
        If dtp_TanggalBukti.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalBukti)
            TanggalBukti = TanggalFormatTampilan(dtp_TanggalBukti.SelectedDate)
        End If
    End Sub


    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub


    Private Sub txt_KodeLawanTransaksi_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
    End Sub
    Private Sub btn_PilihLawanTransaksi_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihLawanTransaksi.Click
        BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaLawanTransaksi_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub


    Private Sub txt_KodeCustomer_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
        txt_NamaCustomer.Text = AmbilValue_NamaMitra(KodeCustomer)
    End Sub
    Private Sub btn_PilihCustomer_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCustomer.Click
        BukaFormListLawanTransaksi(txt_KodeCustomer, txt_NamaCustomer, Mitra_Customer, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaCustomer_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_NamaCustomer.TextChanged
        NamaCustomer = txt_NamaCustomer.Text
    End Sub


    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahTransaksi)
    End Sub
    Private Sub txt_JumlahTransaksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahTransaksi.PreviewTextInput
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub



    Dim NomorUrutProduk_Terseleksi
    Dim COAProduk_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim NomorReferensiProduk_Terseleksi
    Dim TanggalReferensiProduk_Terseleksi
    Dim JumlahHargaProduk_Terseleksi
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

        NomorUrutProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut_Produk"))
        COAProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "COA_Produk")
        NamaProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Produk")
        NomorReferensiProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Referensi_Produk")
        TanggalReferensiProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Referensi_Produk")
        JumlahHargaProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Harga_Produk").ToString)

        If NomorUrutProduk_Terseleksi > 0 Then
            btn_Perbaiki.IsEnabled = True
            btn_Singkirkan.IsEnabled = True
        Else
            btn_Perbaiki.IsEnabled = False
            btn_Singkirkan.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Perbaiki_Click(sender, e)
    End Sub


    Private Sub btn_Tambahkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tambahkan.Click

        win_InputProduk_DepositOperasional = New wpfWin_InputProduk_DepositOperasional
        win_InputProduk_DepositOperasional.ResetForm()
        win_InputProduk_DepositOperasional.FungsiForm = FungsiForm_TAMBAH
        win_InputProduk_DepositOperasional.ShowDialog()

        If win_InputProduk_DepositOperasional.Tambahkan Then
            NomorUrutProduk = JumlahBaris + 1
            datatabelUtama.Rows.Add(NomorUrutProduk,
                                    win_InputProduk_DepositOperasional.KodeAkun,
                                    win_InputProduk_DepositOperasional.NamaProduk,
                                    win_InputProduk_DepositOperasional.NomorReferensi,
                                    win_InputProduk_DepositOperasional.TanggalReferensi,
                                    win_InputProduk_DepositOperasional.Jumlah
                                    )
            BersihkanSeleksi() '(Wajib bersihkan seleksi, agar tidak terjadi masalah...!)
        End If

    End Sub

    Private Sub btn_Perbaiki_Click(sender As Object, e As RoutedEventArgs) Handles btn_Perbaiki.Click

        win_InputProduk_DepositOperasional = New wpfWin_InputProduk_DepositOperasional
        win_InputProduk_DepositOperasional.ResetForm()
        win_InputProduk_DepositOperasional.FungsiForm = FungsiForm_EDIT
        win_InputProduk_DepositOperasional.NomorUrut = NomorUrutProduk_Terseleksi
        win_InputProduk_DepositOperasional.txt_KodeAkun.Text = COAProduk_Terseleksi
        win_InputProduk_DepositOperasional.txt_NamaProduk.Text = NamaProduk_Terseleksi
        win_InputProduk_DepositOperasional.txt_NomorReferensi.Text = NomorReferensiProduk_Terseleksi
        win_InputProduk_DepositOperasional.dtp_TanggalReferensi.SelectedDate = TanggalFormatWPF(TanggalReferensiProduk_Terseleksi)
        win_InputProduk_DepositOperasional.txt_Jumlah.Text = JumlahHargaProduk_Terseleksi
        win_InputProduk_DepositOperasional.ShowDialog()

        rowviewUtama("COA_Produk") = win_InputProduk_DepositOperasional.KodeAkun
        rowviewUtama("Nama_Produk") = win_InputProduk_DepositOperasional.NamaProduk
        rowviewUtama("Nomor_Referensi_Produk") = win_InputProduk_DepositOperasional.NomorReferensi
        rowviewUtama("Tanggal_Referensi_Produk") = win_InputProduk_DepositOperasional.TanggalReferensi
        rowviewUtama("Jumlah_Harga_Produk") = win_InputProduk_DepositOperasional.Jumlah

        BersihkanSeleksi() '(Wajib bersihkan seleksi, agar ada refresh value. Kalau tidak, bisa rancu value terseleksinya...!)

    End Sub


    Private Sub btn_Singkirkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Singkirkan.Click
        rowviewUtama.Delete()
        rowviewUtama.Delete()
        Dim i = 0
        For Each row As DataRow In datatabelUtama.Rows
            i += 1
            row("Nomor_Urut") = i
        Next
        BersihkanSeleksi()
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Validasi Form :
        If NomorBukti = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Bukti'.")
            txt_NomorBukti.Focus()
            Return
        End If

        If dtp_TanggalBukti.Text = Kosongan Then
            PesanPeringatan("Silakan isi 'Tanggal Bukti'.")
            dtp_TanggalBukti.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lawan Transaksi'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If KodeCustomer = Kosongan Then
            PesanPeringatan("Silakan pilih 'Customer'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If JumlahTransaksi = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Focus()
            Return
        End If

        If JumlahBaris = 0 Then
            PesanUntukProgrammer("Silakan isi 'Tabel Produk'.")
            Return
        End If


        AksesDatabase_Transaksi(Buka)

        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        If FungsiForm = FungsiForm_EDIT Then
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_DepositOperasional WHERE Nomor_BPDO = '" & NomorBPDO & "' ", KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()
            jur_NomorJV = NomorJV
            HapusJurnal_BerdasarkanNomorJV(NomorJV)
            'Coding Sementara, untuk memperbaiki data di komputer pa Aris
            '--------------------------------------------------------------
            If NomorJV = 0 Then
                SistemPenomoranOtomatis_NomorJV()
                NomorJV = jur_NomorJV
                PesanUntukProgrammer("Nomor JV : " & NomorJV)
            End If
            '--------------------------------------------------------------
            'Setelah rapi, maka baris-baris ini harus dihapus
        End If

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_DepositOperasional")
        NomorUrutProduk = 0

        Dim NamaProduk_Bundel As String = Kosongan

        For Each row As DataRow In datatabelUtama.Rows 'Awal Loop ========================================================

            If AmbilAngka(row("Nomor_Urut_Produk").ToString) = 0 Then Exit For

            COAProduk = row("COA_Produk")
            NamaProduk = row("Nama_Produk")
            NomorReferensiProduk = row("Nomor_Referensi_Produk")
            TanggalReferensiProduk = row("Tanggal_Referensi_Produk")
            JumlahHargaProduk = AmbilAngka(row("Jumlah_Harga_Produk"))

            NomorID += 1
            NomorUrutProduk += 1
            cmd = New OdbcCommand(" INSERT INTO tbl_DepositOperasional VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & AngkaBPDO & "', " &
                                  " '" & NomorBPDO & "', " &
                                  " '" & NomorBukti & "', " &
                                  " '" & TanggalFormatSimpan(TanggalBukti) & "', " &
                                  " '" & NomorFakturPajak & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & KodeCustomer & "', " &
                                  " '" & NamaCustomer & "', " &
                                  " '" & NomorUrutProduk & "', " &
                                  " '" & COAProduk & "', " &
                                  " '" & NamaProduk & "', " &
                                  " '" & NomorReferensiProduk & "', " &
                                  " '" & TanggalFormatSimpan(TanggalReferensiProduk) & "', " &
                                  " '" & JumlahHargaProduk & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            If NomorUrutProduk = 1 Then
                NamaProduk_Bundel = NamaProduk
            Else
                NamaProduk_Bundel &= SlashGanda_Pemisah & NamaProduk
            End If

        Next 'Akhir Loop =================================================================================================

        AksesDatabase_Transaksi(Tutup)

        'Simpan Jurnal
        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalBukti
        jur_JenisJurnal = JenisJurnal_DepositOperasional
        jur_Referensi = NomorBPDO
        jur_TanggalInvoice = TanggalBukti
        jur_NomorInvoice = NomorBukti
        jur_NomorFakturPajak = NomorFakturPajak
        jur_NamaProduk = NamaProduk_Bundel
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTransaksi
        'jur_KodeMataUang = KodeMataUang
        jur_UraianTransaksi = Keterangan
        jur_Direct = 0

        ___jurDebet(KodeTautanCOA_DepositOperasional, JumlahTransaksi)
        _______jurKredit(KodeTautanCOA_HutangDeposit, JumlahTransaksi)


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            usc_BukuPengawasanDepositOperasional.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
        txt_NomorBPDO.IsReadOnly = True
        txt_KodeLawanTransaksi.IsReadOnly = True
        txt_NamaLawanTransaksi.IsReadOnly = True
        txt_KodeCustomer.IsReadOnly = True
        txt_NamaCustomer.IsReadOnly = True
        txt_JumlahTransaksi.IsReadOnly = True
        btn_Perbaiki.IsEnabled = False
        btn_Singkirkan.IsEnabled = False
    End Sub

    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer


    Dim Nomor_Urut_Produk As New DataGridTextColumn
    Dim COA_Produk As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Nomor_Referensi_Produk As New DataGridTextColumn
    Dim Tanggal_Referensi_Produk As New DataGridTextColumn
    Dim Jumlah_Harga_Produk As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut_Produk")
        datatabelUtama.Columns.Add("COA_Produk")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Nomor_Referensi_Produk")
        datatabelUtama.Columns.Add("Tanggal_Referensi_Produk")
        datatabelUtama.Columns.Add("Jumlah_Harga_Produk", GetType(Int64))

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut_Produk, "Nomor_Urut_Produk", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, COA_Produk, "COA_Produk", "Kode Akun", 72, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Referensi_Produk, "Nomor_Referensi_Produk", "Nomor Referensi", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Referensi_Produk, "Tanggal_Referensi_Produk", "Tanggal Referensi", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga_Produk, "Jumlah_Harga_Produk", "Jumlah", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub

End Class
