Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Threading.Tasks
Imports bcomm

Public Class wpfUsc_SuratJalanPembelian

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False
    Private SedangMemuatData As Boolean = False
    Public JudulForm


    'Variabel Tabel :
    Dim NomorUrut
    Dim AngkaSJ
    Dim AngkaSJ_Sebelumnya
    Dim NomorSJ
    Dim TanggalSJ
    Dim NomorPO
    Dim TanggalPO
    Dim KodeProject
    Dim KodeSupplier
    Dim NamaSupplier
    Dim Catatan
    Dim StatusKontrol
    Dim NamaPenerima
    Dim TanggalDiterima

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Public AngkaSJ_Terseleksi
    Dim NomorSJ_Terseleksi
    Dim TanggalSJ_Terseleksi
    Dim NomorPO_Terseleksi
    Dim TanggalPO_Terseleksi
    Dim KodeProject_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim Catatan_Terseleksi
    Dim StatusKontrol_Terseleksi
    Dim NamaPenerima_Terseleksi
    Dim TanggalDiterima_Terseleksi

    Dim NomorPO_Satuan
    Dim NomorPO_Sebelumnya

    Dim Pilih_Kontrol
    Dim Pilih_KodeSupplier

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        If SudahDimuat Then Return

        StatusAktif = True

        lbl_JudulForm.Text = JudulForm
        Terabas()

        ProsesLoadingForm = True

        RefreshTampilanData()

        ProsesLoadingForm = False

        SudahDimuat = True

    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_Kontrol()
        KontenCombo_Supplier()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub


    Dim EksekusiTampilanData As Boolean

    Async Sub TampilkanDataAsync()

        If EksekusiTampilanData = False Then Return
        If SedangMemuatData Then Return
        SedangMemuatData = True

        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)

        Try
            'Filter Data :
            Dim FilterData = Kosongan

        'Filter Supplier :
        Dim FilterSupplier = Spasi1
        If cmb_Supplier.SelectedValue <> Pilihan_Semua Then FilterSupplier = " AND Kode_Supplier = '" & Pilih_KodeSupplier & "' "

        FilterData = FilterSupplier

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Data Tabel :
        NomorUrut = 0
        AngkaSJ_Sebelumnya = 0

        AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                              " WHERE Nomor_SJ <> 'X' " & FilterData &
                              " ORDER BY Angka_SJ ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            If StatusKoneksiDatabase = False Then Exit Try


            Do While dr.Read
                Await Task.Yield()
            NomorPO = Kosongan
            NomorPO_Satuan = Kosongan
            NomorPO_Sebelumnya = Kosongan
            TanggalPO = Kosongan
            KodeProject = Kosongan
            AngkaSJ = dr.Item("Angka_SJ")
            NomorSJ = dr.Item("Nomor_SJ")
            TanggalSJ = TanggalFormatTampilan(dr.Item("Tanggal_SJ"))
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ WHERE " &
                                         " Angka_SJ = '" & AngkaSJ & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            Do While drTELUSUR.Read
                NomorPO_Satuan = drTELUSUR.Item("Nomor_PO_Produk")
                cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                              " WHERE Nomor_PO = '" & NomorPO_Satuan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                drTELUSUR2.Read()
                If drTELUSUR2.HasRows Then
                    If NomorPO_Satuan <> NomorPO_Sebelumnya Then
                        If NomorPO = Kosongan Then
                            NomorPO = NomorPO_Satuan
                            TanggalPO = TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO"))
                            KodeProject = drTELUSUR2.Item("Kode_Project_Produk")
                        Else
                            NomorPO &= SlashGanda_Pemisah & Enter1Baris & NomorPO_Satuan
                            TanggalPO &= SlashGanda_Pemisah & Enter1Baris & TanggalFormatTampilan(drTELUSUR2.Item("Tanggal_PO"))
                            KodeProject &= SlashGanda_Pemisah & Enter1Baris & drTELUSUR2.Item("Kode_Project_Produk")
                        End If
                    End If
                End If
                NomorPO_Sebelumnya = NomorPO_Satuan
            Loop
            NamaPenerima = dr.Item("Nama_Penerima")
            TanggalDiterima = TanggalFormatTampilan(dr.Item("Tanggal_Diterima"))
            If NamaPenerima = Kosongan Then NamaPenerima = StripKosong
            If TanggalDiterima = TanggalKosong Then TanggalDiterima = StripKosong
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            Catatan = PenghapusEnter(dr.Item("Catatan"))
            If AdaSJBASTdiDataInvoicePembelian(NomorSJ) Then
                StatusKontrol = Status_Closed
            Else
                StatusKontrol = Status_Open
            End If
            Dim TambahkanBaris As Boolean = False
            If AngkaSJ <> AngkaSJ_Sebelumnya Then
                If Pilih_Kontrol = Pilihan_Semua Then
                    TambahkanBaris = True
                Else
                    If Pilih_Kontrol = StatusKontrol Then TambahkanBaris = True
                End If
                If TambahkanBaris = True Then TambahBaris()
            End If
            AngkaSJ_Sebelumnya = AngkaSJ
        Loop

            AksesDatabase_Transaksi(Tutup)

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_SuratJalanPembelian")

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
        NomorUrut += 1
        datatabelUtama.Rows.Add(NomorUrut, AngkaSJ, NomorSJ, TanggalSJ, NomorPO, TanggalPO,
                                KodeProject, KodeSupplier, NamaSupplier, Catatan, StatusKontrol, NamaPenerima, TanggalDiterima)
    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        'btn_Cetak.IsEnabled = False
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
    End Sub


    Sub KontenCombo_Kontrol()
        cmb_Kontrol.Items.Clear()
        cmb_Kontrol.Items.Add(Status_Semua)
        cmb_Kontrol.Items.Add(Status_Open)
        cmb_Kontrol.Items.Add(Status_Closed)
        cmb_Kontrol.SelectedValue = Status_Semua
    End Sub

    Sub KontenCombo_Supplier()
        cmb_Supplier.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Supplier = 1 AND Lokasi_WP = '" & LokasiPS_DalamNegeri & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_Supplier.Items.Add(Pilihan_Semua)
        Do While dr.Read
            cmb_Supplier.Items.Add(dr.Item("Nama_Mitra"))
        Loop
        cmb_Supplier.SelectedValue = Pilihan_Semua
        AksesDatabase_General(Tutup)
    End Sub






    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputSuratJalanPembelian = New wpfWin_InputSuratJalanPembelian
        win_InputSuratJalanPembelian.ResetForm()
        win_InputSuratJalanPembelian.FungsiForm = FungsiForm_TAMBAH
        win_InputSuratJalanPembelian.ShowDialog()
    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

        Dim BisaDiedit As Boolean = True
        AlgoritmaBisaDikorek(NomorSJ_Terseleksi, BisaDiedit)

        If BisaDiedit = False Then
            Pesan_Peringatan("Surat Jalan ini sudah tidak dapat diedit." & Enter2Baris &
                   "Jika ingin mengeditnya, silakan hapus terlebih dahulu data Surat Jalan ini yang tersimpan di Invoice.")
        End If

        win_InputSuratJalanPembelian = New wpfWin_InputSuratJalanPembelian
        win_InputSuratJalanPembelian.ResetForm()
        If BisaDiedit = False Then win_InputSuratJalanPembelian.btn_Simpan.IsEnabled = False
        win_InputSuratJalanPembelian.FungsiForm = FungsiForm_EDIT
        ProsesIsiValueForm = True
        win_InputSuratJalanPembelian.AngkaSJ = AngkaSJ_Terseleksi
        win_InputSuratJalanPembelian.txt_NomorSJ.Text = NomorSJ_Terseleksi
        win_InputSuratJalanPembelian.dtp_TanggalSJ.SelectedDate = TanggalFormatWPF(TanggalSJ_Terseleksi)
        If NamaPenerima_Terseleksi = StripKosong Then
            win_InputSuratJalanPembelian.txt_NamaPenerima.Text = Kosongan
        Else
            win_InputSuratJalanPembelian.txt_NamaPenerima.Text = NamaPenerima_Terseleksi
        End If
        If TanggalDiterima_Terseleksi = StripKosong Then
            win_InputSuratJalanPembelian.dtp_TanggalDiterima.Text = Kosongan
        Else
            win_InputSuratJalanPembelian.dtp_TanggalDiterima.SelectedDate = TanggalFormatWPF(TanggalDiterima_Terseleksi)
        End If
        win_InputSuratJalanPembelian.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        win_InputSuratJalanPembelian.txt_NamaSupplier.Text = NamaSupplier_Terseleksi
        IsiValueElemenRichTextBox(win_InputSuratJalanPembelian.txt_Catatan, Catatan_Terseleksi)
        win_InputSuratJalanPembelian.IsiTabelProduk()
        win_InputSuratJalanPembelian.IsiTabelPO()
        ProsesIsiValueForm = False
        win_InputSuratJalanPembelian.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        Dim BisaDihapus As Boolean = True
        AlgoritmaBisaDikorek(NomorSJ_Terseleksi, BisaDihapus)

        If BisaDihapus = False Then
            Pesan_Peringatan("Surat Jalan ini sudah tidak dapat dihapus." & Enter2Baris &
                   "Jika ingin menghapusnya, silakan hapus terlebih dahulu data Surat Jalan ini yang tersimpan di Invoice.")
            Return
        End If

        If Not TanyaKonfirmasi("Yakin ingin menghapus data terpilih?") Then Return


        AksesDatabase_Transaksi(Buka)

        'Telusuri Data PO yang ada di Surat Jalan terkait : (Kenapa harus begini? Karena dalam satu Surat Jalan, bisa memuat lebih dari 1 PO).
        Dim ListPO As New DataTable
        ListPO.Columns.Add("Kolom_PO")
        ListPO.Rows.Clear()
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                              " WHERE Nomor_SJ = '" & NomorSJ_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            ListPO.Rows.Add(dr.Item("Nomor_PO_Produk"))
        Loop

        'Hapus Surat Jalan / BAST : 
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Pembelian_SJ " &
                                   " WHERE Angka_SJ = '" & AngkaSJ_Terseleksi & "' ",
                                   KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()

        'Update PO satu persatu
        For Each row As DataRow In ListPO.Rows
            Dim NomorPO_Update As String = row("Kolom_PO")
            Dim MetodePembayaran As String = Kosongan
            Dim StatusKontrolPO As String = Kosongan
            cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                  " WHERE Nomor_PO = '" & NomorPO_Update & "'",
                                  KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            MetodePembayaran = dr.Item("Metode_Pembayaran")
            UpdateStatusKontrolPOPembelianBerdasarkanMetodePembayaran(MetodePembayaran, NomorPO_Update)
            If Not AdaPOdiDataSJBASTPembelian(NomorPO_Update) And Not AdaPOdiDataInvoicePembelian(NomorPO_Update) Then UpdateStatusKontrolPOPembelian(NomorPO_Update, Status_Open)
        Next

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            RefreshTampilanPOPembelian()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Sub AlgoritmaBisaDikorek(ByRef NomorPatokan, ByRef BisaDikorek)
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE Nomor_SJ_BAST_Produk = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            BisaDikorek = False
        Else
            BisaDikorek = True
        End If
        AksesDatabase_Transaksi(Tutup)
    End Sub



    Private Sub cmb_Kontrol_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Kontrol.SelectionChanged
        Pilih_Kontrol = cmb_Kontrol.SelectedValue
        TampilkanData()
    End Sub


    Private Sub cmb_Supplier_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Supplier.SelectionChanged
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & cmb_Supplier.SelectedValue & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If dr.HasRows Then Pilih_KodeSupplier = dr.Item("Kode_Mitra")
        AksesDatabase_General(Tutup)
        If cmb_Supplier.SelectedValue = Pilihan_Semua Then Pilih_KodeSupplier = Pilihan_Semua
        TampilkanData()
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
        AngkaSJ_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Angka_SJ"))
        NomorSJ_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_SJ")
        TanggalSJ_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_SJ")
        NomorPO_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_PO")
        TanggalPO_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_PO")
        KodeProject_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Project")
        KodeSupplier_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Supplier")
        NamaSupplier_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Supplier")
        Catatan_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Catatan_")
        StatusKontrol_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kontrol_")
        NamaPenerima_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Penerima")
        TanggalDiterima_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Tanggal_Diterima")

        If AngkaSJ_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            'btn_Cetak.IsEnabled = True
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        'If AmbilAngka(e.Row.Item("Nomor_JV_Penjualan")) = 0 Then
        '    If JenisTahunBuku = JenisTahunBuku_NORMAL Then e.Row.Foreground = clrDataTahunLalu
        'Else
        '    e.Row.Foreground = clrBlack
        'End If
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
    Dim Angka_SJ As New DataGridTextColumn
    Dim Nomor_SJ As New DataGridTextColumn
    Dim Tanggal_SJ As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Tanggal_PO As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Kode_Supplier As New DataGridTextColumn
    Dim Nama_Supplier As New DataGridTextColumn
    Dim Catatan_ As New DataGridTextColumn
    Dim Kontrol_ As New DataGridTextColumn
    Dim Nama_Penerima As New DataGridTextColumn
    Dim Tanggal_Diterima As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Angka_SJ")
        datatabelUtama.Columns.Add("Nomor_SJ")
        datatabelUtama.Columns.Add("Tanggal_SJ")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Tanggal_PO")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Kode_Supplier")
        datatabelUtama.Columns.Add("Nama_Supplier")
        datatabelUtama.Columns.Add("Catatan_")
        datatabelUtama.Columns.Add("Kontrol_")
        datatabelUtama.Columns.Add("Nama_Penerima")
        datatabelUtama.Columns.Add("Tanggal_Diterima")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Angka_SJ, "Angka_SJ", "Angka SJ", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_SJ, "Nomor_SJ", "Nomor SJ", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_SJ, "Tanggal_SJ", "Tanggal SJ", 75, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PO, "Tanggal_PO", "Tanggal PO", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Supplier, "Kode_Supplier", "Kode Supplier", 63, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Supplier, "Nama_Supplier", "Nama Supplier", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Catatan_, "Catatan_", "Catatan", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kontrol_, "Kontrol_", "Kontrol", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Penerima, "Nama_Penerima", "Nama Penerima", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Diterima, "Tanggal_Diterima", "Tanggal Diterima", 75, FormatString, TengahTengah, KunciUrut, Terlihat)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
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

End Class
