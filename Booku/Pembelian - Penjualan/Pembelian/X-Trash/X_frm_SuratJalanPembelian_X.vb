Imports System.Data.Odbc
Imports System.Threading
Imports bcomm

Public Class X_frm_SuratJalanPembelian_X

    Public KesesuaianJurnal As Boolean

    Dim PilihanKontrol

    'Variabel Tabel :
    Dim NomorUrut
    Dim AngkaSJ
    Dim AngkaSJ_Sebelumnya
    Dim NomorSJ
    Dim TanggalSJ
    Dim NomorPO
    Dim TanggalPO
    Dim NamaPenerima
    Dim TanggalDiterima
    Dim KodeProject
    Dim KodeSupplier
    Dim NamaSupplier
    Dim Catatan

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Public AngkaSJ_Terseleksi
    Dim NomorSJ_Terseleksi
    Dim TanggalSJ_Terseleksi
    Dim NomorPO_Terseleksi
    Dim TanggalPO_Terseleksi
    Dim NamaPenerima_Terseleksi
    Dim TanggalDiterima_Terseleksi
    Dim KodeProject_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim Catatan_Terseleksi

    Dim NomorPO_Satuan
    Dim NomorPO_Sebelumnya

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        'Filter Data :
        Dim FilterData = Kosongan

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        NomorUrut = 0
        AngkaSJ_Sebelumnya = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                              " WHERE Nomor_SJ <> 'X' " & FilterData &
                              " ORDER BY Angka_SJ ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return


        Do While dr.Read
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
            Catatan = dr.Item("Catatan")
            If AngkaSJ <> AngkaSJ_Sebelumnya Then TambahBaris()
            AngkaSJ_Sebelumnya = AngkaSJ
        Loop

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, AngkaSJ, NomorSJ, TanggalSJ, NomorPO, TanggalPO,
                                KodeProject, KodeSupplier, NamaSupplier, Catatan, NamaPenerima, TanggalDiterima)
    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Pratinjau.Enabled = False
        btn_Cetak.Enabled = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        win_InputSuratJalanPembelian = New wpfWin_InputSuratJalanPembelian
        win_InputSuratJalanPembelian.ResetForm()
        win_InputSuratJalanPembelian.FungsiForm = FungsiForm_TAMBAH
        win_InputSuratJalanPembelian.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        Dim BisaDiedit As Boolean = True
        AlgoritmaBisaDikorek(NomorSJ_Terseleksi, BisaDiedit)

        If BisaDiedit = False Then
            MsgBox("Surat Jalan ini sudah tidak dapat diedit." & Enter2Baris &
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

    Sub IsiTabelProduk_SuratJalan()
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim JumlahProduk_Dipesan
        Dim JumlahProduk_Dieksekusi
        Dim JumlahProduk_Maksimal
        Dim SatuanProduk
        Dim KeteranganProduk
        NomorUrut = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                              " WHERE Angka_SJ = '" & AngkaSJ_Terseleksi & "' ",
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
            frm_Input_SuratJalanPembelian.JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            frm_Input_SuratJalanPembelian.JenisPPN = dr.Item("Jenis_PPN")
            frm_Input_SuratJalanPembelian.PerlakuanPPN = dr.Item("Perlakuan_PPN")
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
            frm_Input_SuratJalanPembelian.DataTabelUtama.Rows.Add(NomorUrut, NomorPO, TanggalPO,
                                                         NamaProduk, DeskripsiProduk, JumlahProduk, JumlahProduk_Maksimal, SatuanProduk, KodeProject, KeteranganProduk)
        Loop
        AksesDatabase_Transaksi(Tutup)
        frm_Input_SuratJalanPembelian.BersihkanSeleksi_TabelProduk()
    End Sub

    Sub IsiTabelPO_SuratJalan()

        NomorPO_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                              " WHERE Angka_SJ = '" & AngkaSJ_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorPO = dr.Item("Nomor_PO_Produk")
            frm_Input_SuratJalanPembelian.JenisProduk_Induk = dr.Item("Jenis_Produk_Induk")
            frm_Input_SuratJalanPembelian.cmb_JenisPPN.Text = dr.Item("Jenis_PPN")
            frm_Input_SuratJalanPembelian.cmb_PerlakuanPPN.Text = dr.Item("Perlakuan_PPN")
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
                frm_Input_SuratJalanPembelian.dgv_PO.Rows.Add(NomorPO, TanggalPO, KodeProject)
            End If
            NomorPO_Sebelumnya = NomorPO
        Loop
        AksesDatabase_Transaksi(Tutup)

        BeginInvoke(Sub() frm_Input_SuratJalanPembelian.BersihkanSeleksi_TabelPO())

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Dim BisaDihapus As Boolean = True
        AlgoritmaBisaDikorek(NomorSJ_Terseleksi, BisaDihapus)

        If BisaDihapus = False Then
            MsgBox("Surat Jalan ini sudah tidak dapat dihapus." & Enter2Baris &
                   "Jika ingin mengahapusnya, silakan hapus terlebih dahulu data Surat Jalan ini yang tersimpan di Invoice.")
            Return
        End If

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)

        'Pulihkan Status PO yang terkait dengan SJ/BAST, menjadi OPEN :
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_SJ " &
                              " WHERE Nomor_SJ = '" & NomorSJ_Terseleksi & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorPO = dr.Item("Nomor_PO_Produk")
            cmdEDIT = New OdbcCommand(" UPDATE tbl_Pembelian_PO " &
                                      " SET Kontrol = '" & Status_Open & "' " &
                                      " WHERE Nomor_PO = '" & NomorPO & "' ",
                                      KoneksiDatabaseTransaksi)
            cmdEDIT_ExecuteNonQuery()
        Loop

        'Hapus Surat Jalan / BAST
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Pembelian_SJ " &
                                   " WHERE Angka_SJ = '" & AngkaSJ_Terseleksi & "' ",
                                   KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
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


    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return

        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        AngkaSJ_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_SJ", Baris_Terseleksi).Value)
        NomorSJ_Terseleksi = DataTabelUtama.Item("Nomor_SJ", Baris_Terseleksi).Value
        TanggalSJ_Terseleksi = DataTabelUtama.Item("Tanggal_SJ", Baris_Terseleksi).Value
        NomorPO_Terseleksi = DataTabelUtama.Item("Nomor_PO", Baris_Terseleksi).Value
        TanggalPO_Terseleksi = DataTabelUtama.Item("Tanggal_PO", Baris_Terseleksi).Value
        NamaPenerima_Terseleksi = DataTabelUtama.Item("Nama_Penerima", Baris_Terseleksi).Value
        TanggalDiterima_Terseleksi = DataTabelUtama.Item("Tanggal_Diterima", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        KodeSupplier_Terseleksi = DataTabelUtama.Item("Kode_Supplier", Baris_Terseleksi).Value
        NamaSupplier_Terseleksi = DataTabelUtama.Item("Nama_Supplier", Baris_Terseleksi).Value
        Catatan_Terseleksi = DataTabelUtama.Item("Catatan_", Baris_Terseleksi).Value

        If AngkaSJ_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            btn_Pratinjau.Enabled = True
            btn_Cetak.Enabled = True
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub

    Private Sub btn_Pratinjau_Click(sender As Object, e As EventArgs) Handles btn_Pratinjau.Click
        frm_Cetak.FungsiForm = FungsiForm_PRATINJAU
        IsiValueHalamanCetak()
    End Sub

    Private Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        frm_Cetak.FungsiForm = FungsiForm_CETAK
        IsiValueHalamanCetak()
    End Sub

    Sub IsiValueHalamanCetak()
        AngkaSJ_Cetak = AngkaSJ_Terseleksi
        TampilkanHalamanCetak_SuratJalan()
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class