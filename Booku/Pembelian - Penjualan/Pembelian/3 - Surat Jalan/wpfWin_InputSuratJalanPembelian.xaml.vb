Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives


Public Class wpfWin_InputSuratJalanPembelian

    Public JudulForm
    Public FungsiForm
    Public NomorID

    Public JenisProduk_Induk
    Public JenisPPN
    Public PerlakuanPPN

    'Variabel Kolom :
    Public AngkaSJ
    Public NomorSJ
    Dim NomorSJ_Lama
    Dim TanggalSJ
    Dim NamaPenerima
    Dim TanggalDiterima
    Dim NomorPO
    Dim TanggalPO
    Dim KodeSupplier
    Dim NamaSupplier
    Dim Catatan

    'Variabel Tabel :
    Dim NomorUrutProduk
    Dim KodeProjectProduk
    Dim NamaProduk
    Dim DeskripsiProduk
    Dim JumlahProduk_PerItem
    Dim SatuanProduk
    Dim KeteranganProduk

    'Variabel Tabel Produk - Index :
    Dim BarisProduk_Terseleksi
    Dim NomorUrutProduk_Terseleksi
    Dim NamaProduk_Terseleksi
    Dim DeskripsiProduk_Terseleksi
    Dim JumlahProduk_Terseleksi
    Dim JumlahProduk_Maksimal_Terseleksi
    Dim SatuanProduk_Terseleksi
    Dim KeteranganProduk_Terseleksi

    'Variabel Tabel PO - Index :
    Dim NomorPO_Terseleksi

    Dim JumlahProduk
    Dim JumlahPO

    Dim NomorPO_Sebelumnya = Kosongan
    Dim StatusKontrolPO = Kosongan

    Dim Koreksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Surat Jalan"
            AngkaSJ = AmbilNomorTerakhir(DatabaseTransaksi, "tbl_Pembelian_SJ", "Angka_SJ") + 1
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Surat Jalan"
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                  " WHERE Kode_Mitra = '" & KodeSupplier & "' ", KoneksiDatabaseGeneral)
            dr_ExecuteReader()
            dr.Read()
            txt_AlamatSupplier.Text = dr.Item("Alamat")
            AksesDatabase_General(Tutup)
            NomorSJ_Lama = NomorSJ
        End If

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub

    Sub IsiTabelProduk()
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim JumlahProduk_Dipesan
        Dim JumlahProduk_Dieksekusi
        Dim JumlahProduk_Maksimal
        Dim SatuanProduk
        Dim KeteranganProduk
        Dim KodeProject
        Dim NomorUrut = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                              " WHERE Angka_SJ = '" & AngkaSJ & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        Do While dr.Read
            NomorUrut += 1
            NomorPO = dr.Item("Nomor_PO_Produk")
            TanggalPO = dr.Item("Tanggal_PO_Produk")
            KodeProject = dr.Item("Kode_Project_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                         " WHERE Nomor_PO = '" & NomorPO & "' " &
                                         " AND Nama_Produk = '" & NamaProduk & "' ",
                                         KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            JumlahProduk_Dipesan = drTELUSUR.Item("Jumlah_Produk")
            cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                                          " WHERE Nomor_PO_Produk = '" & NomorPO & "' " &
                                          " AND Nama_Produk = '" & NamaProduk & "' ",
                                          KoneksiDatabaseTransaksi)
            drTELUSUR2_ExecuteReader()
            JumlahProduk_Dieksekusi = 0
            Do While drTELUSUR2.Read
                JumlahProduk_Dieksekusi += drTELUSUR2.Item("Jumlah_Produk")
            Loop
            JumlahProduk_Maksimal = JumlahProduk_Dipesan - JumlahProduk_Dieksekusi + JumlahProduk
            SatuanProduk = dr.Item("Satuan_Produk")
            KeteranganProduk = dr.Item("Keterangan_Produk")
            datatabelUtama.Rows.Add(NomorUrut, NomorPO, TanggalPO,
                                    NamaProduk, DeskripsiProduk, JumlahProduk, JumlahProduk_Maksimal, SatuanProduk, KodeProject, KeteranganProduk)
        Loop
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi_TabelProduk()
    End Sub

    Sub IsiTabelPO()
        Dim KodeProject = Kosongan
        NomorPO_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                              " WHERE Angka_SJ = '" & AngkaSJ & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorPO = dr.Item("Nomor_PO_Produk")
            JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
            IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN)
            IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, PerlakuanPPN)
            If NomorPO <> NomorPO_Sebelumnya Then
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                                             " WHERE Nomor_PO = '" & NomorPO & "' ",
                                             KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    TanggalPO = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_PO"))
                    KodeProject = drTELUSUR.Item("Kode_Project_Produk")
                End If
                datatabelPO.Rows.Add(NomorPO, TanggalPO, KodeProject)
            End If
            NomorPO_Sebelumnya = NomorPO
        Loop
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi_TabelPO()
    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        JenisPPN = Kosongan
        PerlakuanPPN = Kosongan
        JenisProduk_Induk = Kosongan

        NomorID = 0
        AngkaSJ = 0
        txt_NomorSJ.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalSJ)
        txt_NamaPenerima.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalDiterima)
        txt_KodeSupplier.Text = Kosongan
        txt_NamaSupplier.Text = Kosongan
        txt_AlamatSupplier.Text = Kosongan
        KontenCombo_PerlakuanPPN_Kosongan()
        KosongkanValueElemenRichTextBox(txt_Catatan)
        Kosongkan_TabelProduk()
        Kosongkan_TabelPO()
        btn_Simpan.IsEnabled = True

        Koreksi = Kosongan

        ProsesResetForm = False

    End Sub


    Sub KontenCombo_PerlakuanPPN_Kosongan()
        cmb_PerlakuanPPN.Items.Clear()
        cmb_PerlakuanPPN.Text = Kosongan
    End Sub


    Sub VisibilitasComboPerlakuanPPN(Visibilitas As Boolean)
        If Visibilitas Then
            lbl_PerlakuanPPN.Visibility = Visibility.Visible
            cmb_PerlakuanPPN.Visibility = Visibility.Visible
        Else
            lbl_PerlakuanPPN.Visibility = Visibility.Collapsed
            cmb_PerlakuanPPN.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub Kosongkan_TabelProduk()
        datatabelUtama.Rows.Clear()
        JumlahProduk = 0
    End Sub


    Sub BersihkanSeleksi_TabelProduk()
        BarisTerseleksi = -1
        btn_Perbaiki.IsEnabled = False
        btn_Singkirkan.IsEnabled = False
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        JumlahProduk = datatabelUtama.Rows.Count
    End Sub


    Sub BersihkanSeleksi_TabelPO()
        BarisTerseleksiPO = -1
        NomorPO_Terseleksi = Kosongan
        btn_SingkirkanPO.IsEnabled = False
        datagridPO.SelectedIndex = -1
        datagridPO.SelectedItem = Nothing
        datagridPO.SelectedCells.Clear()
        JumlahPO = datatabelPO.Rows.Count
    End Sub

    Sub Kosongkan_TabelPO()
        datatabelPO.Rows.Clear()
    End Sub

    Sub KondisiFormSetelahPerubahan()
        If ProsesLoadingForm = True Or ProsesResetForm = True Or ProsesIsiValueForm = True Then Return
        BersihkanSeleksi_TabelProduk()
    End Sub


    Private Sub txt_NomorSJ_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorSJ.TextChanged
        NomorSJ = txt_NomorSJ.Text
    End Sub


    Private Sub dtp_TanggalSJ_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalSJ.SelectedDateChanged
        If dtp_TanggalSJ.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalSJ)
            TanggalSJ = TanggalFormatTampilan(dtp_TanggalSJ.SelectedDate)
            KondisiFormSetelahPerubahan()
        End If
    End Sub


    Private Sub txt_NamaPenerima_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaPenerima.TextChanged
        NamaPenerima = txt_NamaPenerima.Text
        KondisiFormSetelahPerubahan()
    End Sub


    Private Sub dtp_TanggalDiterima_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalDiterima.SelectedDateChanged
        If dtp_TanggalDiterima.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalDiterima)
            TanggalDiterima = TanggalFormatTampilan(dtp_TanggalDiterima.SelectedDate)
            KondisiFormSetelahPerubahan()
        End If
    End Sub


    Private Sub txt_KodeSupplier_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_KodeSupplier.TextChanged
        KodeSupplier = txt_KodeSupplier.Text
        txt_NamaSupplier.Text = AmbilValue_NamaMitra(KodeSupplier)
        txt_PICSupplier.Text = AmbilValue_PICMitra(KodeSupplier)
        txt_AlamatSupplier.Text = AmbilValue_AlamatMitra(KodeSupplier)
        If KodeSupplier = Kosongan Then
            btn_TambahPO.IsEnabled = False
        Else
            btn_TambahPO.IsEnabled = True
        End If
        Kosongkan_TabelPO()
    End Sub
    Private Sub btn_PilihSupplier_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        BukaFormListLawanTransaksi(txt_KodeSupplier, txt_NamaSupplier, Mitra_Supplier, LokasiWP_DalamNegeri, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaSupplier_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_NamaSupplier.TextChanged
        NamaSupplier = txt_NamaSupplier.Text
    End Sub
    Private Sub txt_PICSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PICSupplier.TextChanged
    End Sub
    Private Sub txt_AlamatSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AlamatSupplier.TextChanged
    End Sub


    Private Sub cmb_JenisPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPPN.SelectionChanged
        JenisPPN = cmb_JenisPPN.SelectedValue
        If JenisPPN = JenisPPN_NonPPN Then
            VisibilitasComboPerlakuanPPN(False)
        Else
            VisibilitasComboPerlakuanPPN(True)
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub cmb_PerlakuanPPN_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_PerlakuanPPN.SelectionChanged
        PerlakuanPPN = cmb_PerlakuanPPN.SelectedValue
    End Sub

    Private Sub txt_Catatan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Catatan.TextChanged
        Catatan = IsiValueVariabelRichTextBox(txt_Catatan)
    End Sub


    Private Sub btn_TambahPO_Click(sender As Object, e As RoutedEventArgs) Handles btn_TambahPO.Click
        win_ListPO = New wpfWin_ListPO
        win_ListPO.ResetForm()
        win_ListPO.Sisi = win_ListPO.Sisi_POPembelian
        win_ListPO.NamaMitra_Filter = NamaSupplier
        win_ListPO.FilterMitra_Aktif = False
        win_ListPO.JenisProduk_Induk = JenisProduk_Induk
        win_ListPO.JenisPPN = JenisPPN
        win_ListPO.PerlakuanPPN = PerlakuanPPN
        win_ListPO.JalurMasuk = Form_INPUTSURATJALANPEMBELIAN
        For Each row As DataRow In datatabelPO.Rows
            win_ListPO.ListNomorPO_Singkirkan.Add(row("Nomor_PO").ToString())
        Next
        win_ListPO.ShowDialog()
        NomorPO = win_ListPO.NomorPO_Terseleksi
        TanggalPO = win_ListPO.TanggalPO_Terseleksi
        JenisProduk_Induk = win_ListPO.JenisProdukInduk_Terseleksi
        If NomorPO = Kosongan Then Return
        Dim TelusurPO = Kosongan
        For Each row As DataRow In datatabelPO.Rows
            TelusurPO = row("Nomor_PO")
            If TelusurPO = NomorPO Then
                PesanPeringatan("Nomor PO ini sudah diinput..!")
                Return
            End If
        Next
        datatabelPO.Rows.Add(NomorPO, TanggalPO, win_ListPO.KodeProject_Terseleksi)        '<-- Penambahan Baris PO
        BersihkanSeleksi_TabelPO() 'Ini Jangan dihapus...! Penting..!
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                              " WHERE Nomor_PO = '" & NomorPO & "' " &
                              " AND Jenis_Produk_Per_Item = '" & JenisProduk_Barang & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        NomorUrutProduk = datatabelUtama.Rows.Count
        Dim JumlahProduk_Dipesan
        Dim JumlahProduk_Dieksekusi
        Dim JumlahProduk_Maksimal
        Dim JumlahBarisProduk = 0
        Do While dr.Read
            KodeProjectProduk = dr.Item("Kode_Project_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk_Dipesan = dr.Item("Jumlah_Produk")
            cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                                          " WHERE Nomor_PO_Produk = '" & NomorPO & "' " &
                                          " AND Nama_Produk = '" & NamaProduk & "' ",
                                          KoneksiDatabaseTransaksi)
            drTELUSUR2_ExecuteReader()
            JumlahProduk_Dieksekusi = 0
            Do While drTELUSUR2.Read
                JumlahProduk_Dieksekusi += drTELUSUR2.Item("Jumlah_Produk")
            Loop
            JumlahProduk_Maksimal = JumlahProduk_Dipesan - JumlahProduk_Dieksekusi
            SatuanProduk = dr.Item("Satuan_Produk")
            KeteranganProduk = Kosongan
            If JumlahProduk_Maksimal > 0 Then
                NomorUrutProduk += 1
                datatabelUtama.Rows.Add(NomorUrutProduk, NomorPO, TanggalPO,
                                        NamaProduk, DeskripsiProduk, JumlahProduk_Maksimal, JumlahProduk_Maksimal,
                                        SatuanProduk, KodeProjectProduk, KeteranganProduk)
                JumlahBarisProduk += 1
                KondisiFormSetelahPerubahan()
            End If
        Loop
        If JumlahBarisProduk = 0 Then
            For i As Integer = datatabelPO.Rows.Count - 1 To 0 Step -1
                Dim row As DataRow = datatabelPO.Rows(i)
                If row("Nomor_PO") = NomorPO Then
                    datatabelPO.Rows.Remove(row)
                End If
            Next
            PesanPeringatan("Produk dengan Nomor PO ini sudah diserahkan semua.")
            Return
        End If
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                              " WHERE Nomor_PO = '" & NomorPO & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            JenisPPN = dr.Item("Jenis_PPN")
            PerlakuanPPN = dr.Item("Perlakuan_PPN")
        Else
            pesan_AdaKesalahanTeknis_Database(Kosongan)
            Return
        End If
        If JenisProduk_Induk = JenisProduk_BarangDanJasa Or JenisProduk_Induk = JenisProduk_JasaKonstruksi Then btn_TambahPO.IsEnabled = False
        IsiValueComboBypassTerkunci(cmb_JenisPPN, JenisPPN)
        IsiValueComboBypassTerkunci(cmb_PerlakuanPPN, PerlakuanPPN)
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi_TabelProduk() 'Ini Jangan dihapus...! Penting..!
    End Sub

    Private Sub btn_SingkirkanPO_Click(sender As Object, e As RoutedEventArgs) Handles btn_SingkirkanPO.Click
        Dim NomorPO_UntukDihapus = rowviewPO("Nomor_PO")
        rowviewPO.Delete()
        BersihkanSeleksi_TabelPO()
        Dim BarisUntukDihapus As New List(Of DataGridViewRow)
        For i As Integer = datatabelUtama.Rows.Count - 1 To 0 Step -1
            Dim row As DataRow = datatabelUtama.Rows(i)
            If row("Nomor_PO_Produk") = NomorPO_UntukDihapus Then
                row.Delete()
            End If
        Next
        NomorUrutProduk = 0
        For Each row As DataRow In datatabelUtama.Rows
            NomorUrutProduk += 1
            row("Nomor_Urut") = NomorUrutProduk
        Next
        BersihkanSeleksi_TabelProduk()
        If JumlahPO = 0 Then
            JenisProduk_Induk = Kosongan
            JenisPPN = Kosongan
            PerlakuanPPN = Kosongan
            cmb_JenisPPN.SelectedValue = Kosongan
            KontenCombo_PerlakuanPPN_Kosongan()
            lbl_JenisPPN.IsEnabled = True
            cmb_JenisPPN.IsEnabled = True
            btn_TambahPO.IsEnabled = True
        End If
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub datagridPO_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridPO.SelectionChanged
    End Sub
    Private Sub datagridPO_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridPO.PreviewMouseLeftButtonUp
        HeaderKolomPO = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolomPO IsNot Nothing Then
            BersihkanSeleksi_TabelPO()
        End If
    End Sub
    Private Sub datagridPO_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridPO.SelectedCellsChanged

        KolomTerseleksiPO = datagridPO.CurrentColumn
        BarisTerseleksiPO = datagridPO.SelectedIndex
        If BarisTerseleksiPO < 0 Then Return
        rowviewPO = TryCast(datagridPO.SelectedItem, DataRowView)
        If Not rowviewPO IsNot Nothing Then Return

        If datatabelPO.Rows.Count = 0 Then Return
        NomorPO_Terseleksi = rowviewPO("Nomor_PO")
        If BarisTerseleksiPO >= 0 Then
            btn_SingkirkanPO.IsEnabled = True
        Else
            btn_SingkirkanPO.IsEnabled = False
        End If

    End Sub
    Private Sub datagridPO_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridPO.MouseDoubleClick
    End Sub



    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi_TabelProduk()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        NomorUrutProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        NomorUrutProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nomor_Urut"))
        NamaProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Produk")
        DeskripsiProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Deskripsi_Produk")
        JumlahProduk_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Produk"))
        JumlahProduk_Maksimal_Terseleksi = AmbilAngka(AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Jumlah_Produk_Maksimal"))
        SatuanProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Satuan_Produk")
        KeteranganProduk_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Keterangan_Produk")

        If BarisTerseleksi >= 0 Then
            btn_Perbaiki.IsEnabled = True
            btn_Singkirkan.IsEnabled = True
        Else
            btn_Perbaiki.IsEnabled = False
            btn_Singkirkan.IsEnabled = False
        End If

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


    Private Sub btn_Perbaiki_Click(sender As Object, e As RoutedEventArgs) Handles btn_Perbaiki.Click
        win_InputProduk_SJBAST = New wpfWin_InputProduk_SJBAST
        win_InputProduk_SJBAST.ResetForm()
        win_InputProduk_SJBAST.FungsiForm = FungsiForm_EDIT
        win_InputProduk_SJBAST.txt_NomorUrut.Text = NomorUrutProduk_Terseleksi
        win_InputProduk_SJBAST.txt_NamaProduk.Text = NamaProduk_Terseleksi
        win_InputProduk_SJBAST.txt_DeskripsiProduk.Text = DeskripsiProduk_Terseleksi
        win_InputProduk_SJBAST.txt_JumlahProduk.Text = JumlahProduk_Terseleksi
        win_InputProduk_SJBAST.JumlahProduk_Maksimal = JumlahProduk_Maksimal_Terseleksi
        win_InputProduk_SJBAST.txt_Satuan.Text = SatuanProduk_Terseleksi
        win_InputProduk_SJBAST.txt_Keterangan.Text = KeteranganProduk_Terseleksi
        win_InputProduk_SJBAST.JalurMasuk = Form_INPUTSURATJALANPEMBELIAN
        win_InputProduk_SJBAST.ShowDialog()
        KondisiFormSetelahPerubahan()
    End Sub

    Private Sub btn_Singkirkan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Singkirkan.Click
        If Not TanyaKonfirmasi("Yakin ingin menyingkirkan item terpilih?") Then Return
        rowviewUtama.Delete()
        Dim i = 0
        For Each row As DataRow In datatabelUtama.Rows
            i += 1
            row("Nomor_Urut") = i
        Next
        KondisiFormSetelahPerubahan()
    End Sub




    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If NomorSJ = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NomorSJ, "Nomor Surat Jalan")
            Return
        End If

        If dtp_TanggalSJ.Text = Kosongan Then
            PesanPeringatan("Silakan isi 'Tanggal Surat Jalan'.")
            dtp_TanggalSJ.Focus()
            Return
        End If

        If JumlahPO = 0 Then
            Pesan_Peringatan("Silakan input 'PO'.")
            btn_TambahPO.Focus()
            Return
        End If

        If JumlahProduk = 0 Then
            Pesan_Peringatan("Silakan tambahkan data 'Barang/Jasa'.")
            Return
        End If

        If KodeSupplier = Nothing Then
            Pesan_Peringatan("Silakan isi data 'Supplier'.")
            Return
        End If

        If NamaPenerima = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NamaPenerima, "Nama Penerima")
            Return
        End If

        If dtp_TanggalDiterima.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalDiterima, "Tanggal Diterima")
            Return
        End If

        StatusSuntingDatabase = True 'Ini Jangan dihapus..!!!

        'Jika Form berfungsi untuk EDIT, maka HAPUS data sebelumnya, untuk nantinya diganti dengan data yang baru.
        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Pembelian_SJ " &
                                       " WHERE Angka_SJ = '" & AngkaSJ & "' ",
                                       KoneksiDatabaseTransaksi)
            cmdHAPUS_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        If StatusSuntingDatabase = True Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Pembelian_SJ")

            AksesDatabase_Transaksi(Buka)


            NomorPO_Sebelumnya = Kosongan
            StatusKontrolPO = Kosongan

            Dim QueryPenyimpanan = Nothing
            NomorUrutProduk = 0

            For Each row As DataRow In datatabelUtama.Rows 'Awal Loop ========================================================
                NomorUrutProduk += 1
                NomorID += 1
                NomorPO = row("Nomor_PO_Produk")
                TanggalPO = row("Tanggal_PO_Produk")
                KodeProjectProduk = row("Kode_Project_Produk")
                NamaProduk = row("Nama_Produk")
                DeskripsiProduk = row("Deskripsi_Produk")
                JumlahProduk_PerItem = AmbilAngka(row("Jumlah_Produk"))
                SatuanProduk = row("Satuan_Produk")
                KeteranganProduk = row("Keterangan_Produk")
                QueryPenyimpanan = " INSERT INTO tbl_Pembelian_SJ VALUES ( " &
                    " '" & NomorID & "', " &
                    " '" & AngkaSJ & "', " &
                    " '" & NomorSJ & "', " &
                    " '" & TanggalFormatSimpan(TanggalSJ) & "', " &
                    " '" & JenisProduk_Induk & "', " &
                    " '" & NamaPenerima & "', " &
                    " '" & TanggalFormatSimpan(TanggalDiterima) & "', " &
                    " '" & KodeSupplier & "', " &
                    " '" & NamaSupplier & "', " &
                    " '" & NomorUrutProduk & "', " &
                    " '" & NomorPO & "', " &
                    " '" & TanggalFormatSimpan(TanggalPO) & "', " &
                    " '" & KodeProjectProduk & "', " &
                    " '" & NamaProduk & "', " &
                    " '" & DeskripsiProduk & "', " &
                    " '" & JumlahProduk_PerItem & "', " &
                    " '" & SatuanProduk & "', " &
                    " '" & KeteranganProduk & "', " &
                    " '" & JenisPPN & "', " &
                    " '" & PerlakuanPPN & "', " &
                    " '" & Catatan & "', " &
                    " '" & UserAktif & "', " &
                    " '" & Koreksi & "' ) "
                cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                If StatusKoneksiDatabase = False Then Exit For
                If NomorUrutProduk > 1 And NomorPO <> NomorPO_Sebelumnya Then UpdateStatusPO()
                NomorPO_Sebelumnya = NomorPO
            Next

            UpdateStatusPO()

            AksesDatabase_Transaksi(Tutup)

        End If

        'Jika ada perubahan Nomor Surat Jalan :
        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_EDIT And NomorSJ_Lama <> NomorSJ Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_Pembelian_Invoice " &
                                      " SET Nomor_SJ_BAST_Produk = '" & NomorSJ & "', Tanggal_SJ_BAST_Produk = '" & TanggalFormatSimpan(TanggalSJ) & "' " &
                                      " WHERE Nomor_SJ_BAST_Produk = '" & NomorSJ_Lama & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
                'frm_InvoicePembelian.TampilkanData()
                'frm_BukuPembelian.TampilkanData()
            End If
        End If

        If StatusSuntingDatabase = True Then
            Try
                RefreshTampilanPOPembelian()
                RefreshTampilanSJBASTPembelian()
            Catch ex As Exception
                WriteException(ex, "RefreshTampilanSetelahSimpanSuratJalanPembelian")
            End Try
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub


    Sub UpdateStatusPO()
        Dim NomorPO_Update = NomorPO_Sebelumnya
        Dim MetodePembayaran As String = Kosongan
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_PO " &
                              " WHERE Nomor_PO = '" & NomorPO_Update & "'",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        dr.Read()
        MetodePembayaran = dr.Item("Metode_Pembayaran")
        UpdateStatusKontrolPOPembelianBerdasarkanMetodePembayaran(MetodePembayaran, NomorPO_Update)
    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub



    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
        Buat_DataTabelPO()
        txt_KodeSupplier.IsReadOnly = True
        txt_NamaSupplier.IsReadOnly = True
        txt_PICSupplier.IsReadOnly = True
        txt_AlamatSupplier.IsReadOnly = True
        cmb_JenisPPN.IsReadOnly = True
        cmb_PerlakuanPPN.IsReadOnly = True
        scv_Kiri.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
        scv_Kanan.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
    End Sub

    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Nomor_PO_Produk As New DataGridTextColumn
    Dim Tanggal_PO_Produk As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Deskripsi_Produk As New DataGridTextColumn
    Dim Jumlah_Produk As New DataGridTextColumn
    Dim Jumlah_Produk_Maksimal As New DataGridTextColumn
    Dim Satuan_Produk As New DataGridTextColumn
    Dim Kode_Project_Produk As New DataGridTextColumn
    Dim Keterangan_Produk As New DataGridTextColumn
    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_PO_Produk")
        datatabelUtama.Columns.Add("Tanggal_PO_Produk")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Deskripsi_Produk")
        datatabelUtama.Columns.Add("Jumlah_Produk")
        datatabelUtama.Columns.Add("Jumlah_Produk_Maksimal")
        datatabelUtama.Columns.Add("Satuan_Produk")
        datatabelUtama.Columns.Add("Kode_Project_Produk")
        datatabelUtama.Columns.Add("Keterangan_Produk")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 36, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO_Produk, "Nomor_PO_Produk", "Nomor PO", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_PO_Produk, "Tanggal_PO_Produk", "Tanggal PO", 72, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Deskripsi_Produk, "Deskripsi_Produk", "Deskripsi", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Produk, "Jumlah_Produk", "Jumlah Produk", 51, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Produk_Maksimal, "Jumlah_Produk_Maksimal", "No.", 51, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Satuan_Produk, "Satuan_Produk", "Satuan", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project_Produk, "Kode_Project_Produk", "Kode Project", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_Produk, "Keterangan_Produk", "Keterangan", 150, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub


    'Pembuatan Tabel PO :
    Public datatabelPO As DataTable
    Public dataviewPO As DataView
    Public rowviewPO As DataRowView
    Public newRowPO As DataRow
    Public HeaderKolomPO As DataGridColumnHeader
    Public KolomTerseleksiPO As DataGridColumn
    Public BarisTerseleksiPO As Integer
    Public JumlahBarisPO As Integer

    Dim Nomor_PO As New DataGridTextColumn
    Dim Tanggal_PO As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Sub Buat_DataTabelPO()

        datatabelPO = New DataTable
        datatabelPO.Columns.Add("Nomor_PO")
        datatabelPO.Columns.Add("Tanggal_PO")
        datatabelPO.Columns.Add("Kode_Project")

        StyleTabelUtama_WPF(datagridPO, datatabelPO, dataviewPO)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPO, Nomor_PO, "Nomor_PO", "Nomor PO", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPO, Tanggal_PO, "Tanggal_PO", "Tanggal PO", 72, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridPO, Kode_Project, "Kode_Project", "Kode Project", 135, FormatString, KiriTengah, KunciUrut, Terlihat)

    End Sub

End Class
