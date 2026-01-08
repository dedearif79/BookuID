Imports bcomm
Imports System.Data.Odbc

Public Class frm_ReturPembelian

    Public KesesuaianJurnal As Boolean

    'Variabel Tabel :
    Dim NomorUrut
    Dim AngkaRetur
    Dim AngkaRetur_Sebelumnya
    Dim NomorRetur
    Dim TanggalRetur
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim KodeProject
    Dim KodeSupplier
    Dim NamaSupplier
    Dim JumlahHarga
    Dim DiskonPersen As Decimal
    Dim DiskonRp
    Dim DasarPengenaanPajak
    Dim JenisPPN
    Dim PPN
    Dim TotalRetur
    Dim Catatan
    Dim NomorJV

    'Variabel Data Terseleksi :
    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Public AngkaRetur_Terseleksi
    Dim NomorRetur_Terseleksi
    Dim TanggalRetur_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim KodeProject_Terseleksi
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim JumlahHarga_Terseleksi
    Dim DiskonPersen_Terseleksi As Decimal
    Dim DiskonRp_Terseleksi
    Dim DasarPengenaanPajak_Terseleksi
    Dim JenisPPN_Terseleksi
    Dim PPN_Terseleksi
    Dim TotalRetur_Terseleksi
    Dim Catatan_Terseleksi
    Dim NomorJV_Terseleksi

    Dim NomorInvoice_Satuan
    Dim NomorInvoice_Sebelumnya

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

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        NomorUrut = 0
        AngkaRetur_Sebelumnya = 0

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur " &
                              " ORDER BY Angka_Retur ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return

        Do While dr.Read
            AngkaRetur = dr.Item("Angka_Retur")
            NomorRetur = dr.Item("Nomor_Retur")
            TanggalRetur = TanggalFormatTampilan(dr.Item("Tanggal_Retur"))
            cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur " &
                                         " WHERE Angka_Retur = '" & AngkaRetur & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            NomorInvoice = Kosongan
            NomorInvoice_Satuan = Kosongan
            NomorInvoice_Sebelumnya = Kosongan
            TanggalInvoice = Kosongan
            KodeProject = Kosongan
            Do While drTELUSUR.Read
                NomorInvoice_Satuan = drTELUSUR.Item("Nomor_Invoice_Produk")
                If NomorInvoice_Satuan <> NomorInvoice_Sebelumnya Then
                    If NomorInvoice = Kosongan Then
                        NomorInvoice = NomorInvoice_Satuan
                        TanggalInvoice = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_Invoice_Produk"))
                        KodeProject = drTELUSUR.Item("Kode_Project_Produk")
                    Else
                        NomorInvoice &= ";" & Enter1Baris & NomorInvoice_Satuan
                        TanggalInvoice &= ";" & Enter1Baris & TanggalFormatTampilan(drTELUSUR.Item("Tanggal_Invoice_Produk"))
                        KodeProject &= ";" & Enter1Baris & drTELUSUR.Item("Kode_Project_Produk")
                    End If
                End If
                NomorInvoice_Sebelumnya = NomorInvoice_Satuan
            Loop
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            JenisPPN = dr.Item("Jenis_PPN")
            JumlahHarga = dr.Item("Jumlah_Harga_Keseluruhan")
            DiskonPersen = 0 'Sementara tidak pakai Diskon Persen
            DiskonRp = dr.Item("Diskon")
            DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
            PPN = dr.Item("PPN")
            TotalRetur = dr.Item("Total_Retur")
            Catatan = dr.Item("Catatan")
            NomorJV = dr.Item("Nomor_JV")
            If AngkaRetur <> AngkaRetur_Sebelumnya Then
                TambahBaris()
            End If
            AngkaRetur_Sebelumnya = AngkaRetur
        Loop

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, AngkaRetur, NomorRetur, TanggalRetur, NomorInvoice, TanggalInvoice, KodeProject, KodeSupplier, NamaSupplier,
                                JumlahHarga, (FormatUlangDesimal_Prosentase(DiskonPersen) & " %"), DiskonRp, DasarPengenaanPajak, JenisPPN, PPN, TotalRetur, Catatan, NomorJV)
    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_LihatJurnal.Enabled = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Pratinjau_Click(sender As Object, e As EventArgs) Handles btn_Pratinjau.Click
        Cetak(JenisFormCetak_NotaRetur, AngkaRetur_Terseleksi, True, False)
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        win_InputReturPembelian = New wpfWin_InputReturPembelian
        win_InputReturPembelian.ResetForm()
        win_InputReturPembelian.FungsiForm = FungsiForm_TAMBAH
        win_InputReturPembelian.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        win_InputReturPembelian = New wpfWin_InputReturPembelian
        win_InputReturPembelian.ResetForm()
        win_InputReturPembelian.FungsiForm = FungsiForm_EDIT
        ProsesIsiValueForm = True
        win_InputReturPembelian.AngkaRetur = AngkaRetur_Terseleksi
        win_InputReturPembelian.txt_NomorRetur.Text = NomorRetur_Terseleksi
        win_InputReturPembelian.dtp_TanggalRetur.SelectedDate = TanggalFormatWPF(TanggalRetur_Terseleksi)
        win_InputReturPembelian.txt_KodeSupplier.Text = KodeSupplier_Terseleksi
        win_InputReturPembelian.txt_NamaSupplier.Text = NamaSupplier_Terseleksi
        win_InputReturPembelian.txt_JumlahNota.Text = JumlahHarga_Terseleksi
        IsiValueElemenRichTextBox(win_InputReturPembelian.txt_Catatan, Catatan_Terseleksi)
        win_InputReturPembelian.NomorJV = NomorJV_Terseleksi
        IsiTabelProduk()
        IsiTabelInvoice()
        ProsesIsiValueForm = False
        win_InputReturPembelian.ShowDialog()
    End Sub

    Sub IsiTabelProduk()
        Dim JenisProdukPerItem
        Dim NomorInvoiceProduk
        Dim TanggalInvoiceProduk
        Dim KodeProjectProduk
        Dim COAProduk
        Dim NamaProduk
        Dim DeskripsiProduk
        Dim JumlahProduk
        Dim SatuanProduk
        Dim HargaSatuan
        Dim JumlahHargaPerItem
        Dim DiskonPerItem_Persen As Decimal
        Dim DiskonPerItem_Rp As Int64  '(Harus Pakai Int64...! Jangan dirubah dengan yang lain. Karena ada efek dari perhitungan dengan desimal).
        Dim TotalHargaPerItem
        NomorUrut = 0
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur WHERE Angka_Retur = '" & AngkaRetur_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut += 1
            JenisProdukPerItem = dr.Item("Jenis_Produk_Per_Item")
            NomorInvoiceProduk = dr.Item("Nomor_Invoice_Produk")
            TanggalInvoiceProduk = dr.Item("Tanggal_Invoice_Produk")
            COAProduk = dr.Item("COA_Produk")
            NamaProduk = dr.Item("Nama_Produk")
            DeskripsiProduk = dr.Item("Deskripsi_Produk")
            JumlahProduk = dr.Item("Jumlah_Produk")
            SatuanProduk = dr.Item("Satuan_Produk")
            HargaSatuan = dr.Item("Harga_Satuan")
            JumlahHargaPerItem = JumlahProduk * HargaSatuan
            DiskonPerItem_Persen = dr.Item("Diskon_Per_Item")
            DiskonPerItem_Rp = JumlahHargaPerItem * (DiskonPerItem_Persen / 100)
            TotalHargaPerItem = dr.Item("Total_Harga_Per_Item")
            KodeProjectProduk = dr.Item("Kode_Project_Produk")
            win_InputReturPembelian.datatabelUtama.Rows.Add(NomorUrut, JenisProdukPerItem, NomorInvoiceProduk, TanggalInvoiceProduk,
                                                             COAProduk, NamaProduk, DeskripsiProduk, JumlahProduk, SatuanProduk, HargaSatuan,
                                                             JumlahHargaPerItem, (FormatUlangDesimal_Prosentase(DiskonPerItem_Persen) & " %"), DiskonPerItem_Rp, TotalHargaPerItem, KodeProjectProduk)
            win_InputReturPembelian.TarifPPN = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPN"))
        Loop
        AksesDatabase_Transaksi(Tutup)
    End Sub

    Sub IsiTabelInvoice()

        NomorInvoice_Sebelumnya = Kosongan
        AksesDatabase_Transaksi(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur " &
                                     " WHERE Angka_Retur = '" & AngkaRetur & "' ", KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        NomorInvoice_Sebelumnya = Kosongan
        Do While drTELUSUR.Read
            NomorInvoice = drTELUSUR.Item("Nomor_Invoice_Produk")
            If NomorInvoice <> NomorInvoice_Sebelumnya Then
                TanggalInvoice = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_Invoice_Produk"))
                KodeProject = drTELUSUR.Item("Kode_Project_Produk")
                win_InputReturPembelian.datatabelInvoice.Rows.Add(NomorInvoice, TanggalInvoice, KodeProject)
            End If
            NomorInvoice_Sebelumnya = NomorInvoice
            win_InputReturPembelian.cmb_JenisPPN.Text = drTELUSUR.Item("Jenis_PPN")
            win_InputReturPembelian.cmb_PerlakuanPPN.Text = drTELUSUR.Item("Perlakuan_PPN")
        Loop
        AksesDatabase_Transaksi(Tutup)

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)

        'Kosongkan Terlebih dahulu Data Retur di Masing-masing Invoice :
        cmd = New OdbcCommand(" SELECT Nomor_Invoice_Produk FROM tbl_Pembelian_Retur " &
                              " WHERE Angka_Retur = '" & AngkaRetur_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Dim NomorInvoiceDiedit
        Do While dr.Read
            NomorInvoiceDiedit = dr.Item("Nomor_Invoice_Produk")
            cmdEDIT = New OdbcCommand(" UPDATE tbl_Pembelian_Invoice SET " &
                                      " Retur_DPP = 0, " &
                                      " Retur_PPN = 0  " &
                                      " WHERE Nomor_Invoice = '" & NomorInvoiceDiedit & "' ", KoneksiDatabaseTransaksi)
            cmdEDIT_ExecuteNonQuery()
        Loop

        'Hapus Data Terpilih Pada Tabel Retur Pembelian (tbl_Pembelian_Retur) :
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_Pembelian_Retur " &
                                   " WHERE Angka_Retur = '" & AngkaRetur_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()

        'Hapus Jurnal Terkait (tbl_Transaksi) :
        HapusJurnal_BerdasarkanNomorJV(NomorJV_Terseleksi)

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

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
        AngkaRetur_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_Retur", Baris_Terseleksi).Value)
        NomorRetur_Terseleksi = DataTabelUtama.Item("Nomor_Retur", Baris_Terseleksi).Value
        TanggalRetur_Terseleksi = DataTabelUtama.Item("Tanggal_Retur", Baris_Terseleksi).Value
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", Baris_Terseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", Baris_Terseleksi).Value
        KodeProject_Terseleksi = DataTabelUtama.Item("Kode_Project", Baris_Terseleksi).Value
        KodeSupplier_Terseleksi = DataTabelUtama.Item("Kode_Supplier", Baris_Terseleksi).Value
        NamaSupplier_Terseleksi = DataTabelUtama.Item("Nama_Supplier", Baris_Terseleksi).Value
        JumlahHarga_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Harga", Baris_Terseleksi).Value)
        DiskonPersen_Terseleksi = GantiTeks(DataTabelUtama.Item("Diskon_Persen", Baris_Terseleksi).Value, " %", "") 'Jangan pakai function AmbilAngka()..!!!!
        DiskonRp_Terseleksi = AmbilAngka(DataTabelUtama.Item("Diskon_Rp", Baris_Terseleksi).Value)
        DasarPengenaanPajak_Terseleksi = AmbilAngka(DataTabelUtama.Item("Dasar_Pengenaan_Pajak", Baris_Terseleksi).Value)
        JenisPPN_Terseleksi = DataTabelUtama.Item("Jenis_PPN", Baris_Terseleksi).Value
        PPN_Terseleksi = AmbilAngka(DataTabelUtama.Item("PPN_", Baris_Terseleksi).Value)
        TotalRetur_Terseleksi = AmbilAngka(DataTabelUtama.Item("Total_Retur", Baris_Terseleksi).Value)
        Catatan_Terseleksi = DataTabelUtama.Item("Catatan_", Baris_Terseleksi).Value
        NomorJV_Terseleksi = DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value

        If AngkaRetur_Terseleksi = 0 Then
            BersihkanSeleksi()
        Else
            btn_LihatJurnal.Enabled = True
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class