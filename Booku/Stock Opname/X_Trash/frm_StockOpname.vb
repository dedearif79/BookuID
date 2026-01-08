Imports bcomm
Imports System.Data.Odbc

Public Class frm_StockOpname

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Public JenisStok_Menu
    Public JenisPengecekan_Menu
    Public JudulForm

    Dim NomorUrut
    Dim NomorID
    Dim JenisStok
    Dim TanggalPengecekan
    Dim NamaBarang
    Dim JumlahBarang
    Dim Satuan
    Dim HargaSatuan
    Dim JumlahHarga
    Dim Asal
    Dim Lokasi
    Dim Keterangan
    Dim JenisData

    Dim NomorInvoice
    Dim TanggalInvoice
    Dim NomorFakturPajak
    Dim NamaSupplier
    Dim KodeProject
    Dim Kodeakun
    Dim NamaAkun

    Dim TotalPersediaan
    Dim TotalPersediaan_Lokal
    Dim TotalPersediaan_Import

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim JenisStok_Terseleksi
    Dim TanggalPengecekan_Terseleksi
    Dim NamaBarang_Terseleksi
    Dim JumlahBarang_Terseleksi
    Dim Satuan_Terseleksi
    Dim HargaSatuan_Terseleksi
    Dim JumlahHarga_Terseleksi
    Dim Asal_Terseleksi
    Dim Lokasi_Terseleksi
    Dim Keterangan_Terseleksi
    Dim JenisData_Terseleksi

    'Tombol Jurnal
    Dim TombolJurnal
    Dim TombolJurnal_Jurnal = "Jurnal"
    Dim TombolJurnal_Dorong = "Dorong ke Jurnal"
    Dim TombolJurnal_Lihat = "Lihat Jurnal"

    'Jenis Pengecekan :
    Public JenisPengecekan_CekFisik = "Cek Fisik"
    Public JenisPengecekan_TarikanData = "Tarikan Data"

    'Jenis Data :
    Dim JenisData_Normal = "Normal"
    Dim JenisData_Bahan = "Bahan"

    Dim QueryAwal
    Dim PilihanBulan
    Dim PilihanBulan_Angka
    Dim PilihanBulan_NomorString
    Dim FilterBulan

    Dim NomorJV
    Dim COAPersediaan

    Dim JumlahBarisBahanData

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        JudulForm = "Stock Opname " & JenisStok_Menu

        Select Case JenisStok_Menu
            Case JenisStok_BahanPenolong
                COAPersediaan = KodeTautanCOA_PersediaanBahanPenolong
                DataTabelUtama.Columns("Asal_").Visible = True
            Case JenisStok_BahanBaku
                COAPersediaan = KodeTautanCOA_PersediaanBahanBaku_Lokal
                DataTabelUtama.Columns("Asal_").Visible = True
            Case JenisStok_BarangDalamProses
                COAPersediaan = KodeTautanCOA_PersediaanBarangDalamProses
                DataTabelUtama.Columns("Asal_").Visible = False
            Case JenisStok_BarangJadi
                COAPersediaan = KodeTautanCOA_PersediaanBarangJadi
                DataTabelUtama.Columns("Asal_").Visible = False
        End Select

        Select Case JenisPengecekan_Menu
            Case JenisPengecekan_CekFisik
                DataTabelUtama.Columns("Nama_Barang").Visible = True
                DataTabelUtama.Columns("Jumlah_Barang").Visible = True
                DataTabelUtama.Columns("Satuan_").Visible = True
                DataTabelUtama.Columns("Harga_Satuan").Visible = True
                DataTabelUtama.Columns("Nomor_Invoice").Visible = False
                DataTabelUtama.Columns("Tanggal_Invoice").Visible = False
                DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = False
                DataTabelUtama.Columns("Nama_Supplier").Visible = False
                DataTabelUtama.Columns("Lokasi_").Visible = True
                DataTabelUtama.Columns("Kode_Project").Visible = False
                DataTabelUtama.Columns("Kode_Akun").Visible = False
                DataTabelUtama.Columns("Nama_Akun").Visible = False
                If JenisStok_Menu = JenisStok_BarangDalamProses Then JudulForm &= " - Cek Fisik"
                btn_Jurnal.Visible = True
            Case JenisPengecekan_TarikanData
                DataTabelUtama.Columns("Nama_Barang").Visible = False
                DataTabelUtama.Columns("Jumlah_Barang").Visible = False
                DataTabelUtama.Columns("Satuan_").Visible = False
                DataTabelUtama.Columns("Harga_Satuan").Visible = False
                DataTabelUtama.Columns("Nomor_Invoice").Visible = True
                DataTabelUtama.Columns("Tanggal_Invoice").Visible = True
                DataTabelUtama.Columns("Nomor_Faktur_Pajak").Visible = True
                DataTabelUtama.Columns("Nama_Supplier").Visible = True
                DataTabelUtama.Columns("Lokasi_").Visible = False
                DataTabelUtama.Columns("Kode_Project").Visible = True
                DataTabelUtama.Columns("Kode_Akun").Visible = True
                DataTabelUtama.Columns("Nama_Akun").Visible = True
                JudulForm &= " - Tarikan Data"
                btn_Jurnal.Visible = False
        End Select

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        KontenCombo_Bulan()

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

        'Data Tabel
        JumlahBarisBahanData = 0
        NomorJV = 0
        NomorUrut = 0
        TotalPersediaan = 0
        TotalPersediaan_Lokal = 0
        TotalPersediaan_Import = 0

        'Cek Keberadaan Jurnal :
        If CekKeberadaanJurnal_DiBulanTertentu(COAPersediaan, JenisJurnal_AdjusmentHPP, PilihanBulan_Angka) = True Then
            Sub_TombolJurnal_Lihat()
            DataTabelUtama.DefaultCellStyle.ForeColor = WarnaPudar
            NomorJV = NomorJV_SaatPengecekanJurnal
            pnl_CRUD.Enabled = False
        Else
            Sub_TombolJurnal_Dorong()
            DataTabelUtama.DefaultCellStyle.ForeColor = WarnaTegas
            NomorJV = 0
            If JenisPengecekan_Menu = JenisPengecekan_CekFisik Then
                pnl_CRUD.Enabled = True
            End If
        End If

        AksesDatabase_Transaksi(Buka)

        Select Case JenisPengecekan_Menu
            Case JenisPengecekan_CekFisik
                QueryAwal = " SELECT * FROM tbl_StockOpname WHERE Jenis_Stok = '" & JenisStok_Menu & "' "
                FilterBulan = " AND DATE_FORMAT(Tanggal_Pengecekan, '%Y-%m') = '" & TahunBukuAktif & "-" & PilihanBulan_NomorString & "' "
            Case JenisPengecekan_TarikanData
                QueryAwal = " SELECT * FROM tbl_Pembelian_Invoice WHERE COA_Produk LIKE '5%' "
                FilterBulan = " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') <= '" & TahunBukuAktif & "-" & PilihanBulan_NomorString & "' "
        End Select
        cmd = New OdbcCommand(QueryAwal & FilterBulan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            Select Case JenisPengecekan_Menu
                Case JenisPengecekan_CekFisik
                    TampilkanData_CekFisik()
                Case JenisPengecekan_TarikanData
                    TampilkanData_TarikanData()
            End Select
        Loop

        AksesDatabase_Transaksi(Tutup)

        If TotalPersediaan > 0 Then
            If JumlahBarisBahanData > 0 Then DataTabelUtama.DefaultCellStyle.ForeColor = WarnaPeringatan
            DataTabelUtama.Rows.Add()
            DataTabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan,
                                    Kosongan, Kosongan, Kosongan, Kosongan, "TOTAL",
                                    Kosongan, Kosongan, Kosongan, "TOTAL",
                                    TotalPersediaan)
        End If

        BersihkanSeleksi()

        BeginInvoke(Sub() PesanUntukProgrammer("Tinjau ulang perhitungan, karena harus memperhatikan bahan yang diretur setelah pembelian...!!!"))

    End Sub

    Sub TampilkanData_CekFisik()
        NomorID = dr.Item("Nomor_ID")
        JenisStok = dr.Item("Jenis_Stok")
        TanggalPengecekan = TanggalFormatTampilan(dr.Item("Tanggal_Pengecekan"))
        NamaBarang = dr.Item("Nama_Barang")
        JumlahBarang = dr.Item("Jumlah_Barang")
        Satuan = dr.Item("Satuan")
        HargaSatuan = dr.Item("Harga_Satuan")
        JumlahHarga = dr.Item("Jumlah_Harga")
        Asal = dr.Item("Asal")
        Lokasi = dr.Item("Lokasi")
        NomorInvoice = Kosongan
        TanggalInvoice = Kosongan
        NomorFakturPajak = Kosongan
        NamaSupplier = Kosongan
        KodeProject = Kosongan
        Kodeakun = Kosongan
        NamaAkun = Kosongan
        Keterangan = dr.Item("Keterangan")
        JenisData = dr.Item("Jenis_Data")
        If JenisData = JenisData_Bahan Then JumlahBarisBahanData += 1
        TambahBaris()
    End Sub

    Sub TampilkanData_TarikanData()
        NomorID = 0
        JenisStok = JenisStok_BarangDalamProses
        TanggalPengecekan = Kosongan
        JumlahHarga = dr.Item("Total_Harga_Per_Item")
        If JumlahHarga > 0 Then '(Invoice yang sudah di-nol-kan (dibetulkan) tidak boleh ditampilkan...!)
            NamaBarang = dr.Item("Nama_Produk")
            JumlahBarang = 0
            Satuan = Kosongan
            HargaSatuan = 0
            Asal = Kosongan
            Lokasi = Kosongan
            NomorInvoice = dr.Item("Nomor_Invoice")
            TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            NamaSupplier = AmbilValue_NamaSupplier(dr.Item("Kode_Supplier"))
            KodeProject = dr.Item("Kode_Project")
            Kodeakun = dr.Item("COA_Produk")
            NamaAkun = AmbilValue_NamaAkun(Kodeakun)
            Keterangan = dr.Item("Catatan")
            JenisData = Kosongan
            cmdTELUSUR = New OdbcCommand(" SELECT Tanggal_Invoice, Kode_Project FROM tbl_Penjualan_Invoice " &
                                         " WHERE Kode_Project = '" & KodeProject & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR_ExecuteReader()
            drTELUSUR.Read()
            If drTELUSUR.HasRows Then
                If PilihanBulan_Angka < AmbilAngka(drTELUSUR.Item("Tanggal_Invoice").Month) Then TambahBaris()
            Else
                TambahBaris()
            End If
        End If
    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        If Asal = val_Lokal Then TotalPersediaan_Lokal += JumlahHarga
        If Asal = val_Import Then TotalPersediaan_Import += JumlahHarga
        TotalPersediaan += JumlahHarga
        DataTabelUtama.Rows.Add(NomorUrut, NomorID, JenisStok,
                                TanggalPengecekan, NamaBarang, JumlahBarang, Satuan, HargaSatuan,
                                NomorInvoice, TanggalInvoice, NomorFakturPajak, NamaSupplier,
                                JumlahHarga, Asal, Lokasi,
                                KodeProject, Kodeakun, NamaAkun,
                                Keterangan, JenisData)
        Select Case JenisData
            Case JenisData_Bahan
                DataTabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaPeringatan
            Case JenisData_Normal
                If NomorJV = 0 Then DataTabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaTegas
                If NomorJV > 0 Then DataTabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaPudar
        End Select
    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Sub KontenCombo_Bulan()
        Dim Kolom = "Tanggal_Transaksi"
        Dim TabelDanKriteria = " tbl_Transaksi WHERE COA = '" & COAPersediaan & "' " & " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' "
        Dim BulanTertuaAngka = 1 + AmbilValue_BulanTertuaAngka(TabelDanKriteria, Kolom)
        If BulanTertuaAngka > 12 Then BulanTertuaAngka = 12
        KontenComboBulanDibatasi_Public(cmb_Bulan, BulanTertuaAngka)
        cmb_Bulan.Text = KonversiAngkaKeBulanString(BulanTertuaAngka)
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        KontenCombo_Bulan()
    End Sub

    Private Sub cmb_Bulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Bulan.SelectedIndexChanged
        PilihanBulan = cmb_Bulan.Text
        PilihanBulan_NomorString = KonversiBulanKeNomor_String(PilihanBulan)
        PilihanBulan_Angka = AmbilAngka(PilihanBulan_NomorString)
        TampilkanData()
    End Sub

    Private Sub btn_Jurnal_Click(sender As Object, e As EventArgs) Handles btn_Jurnal.Click

        If TombolJurnal = TombolJurnal_Dorong Then

            If JumlahBarisBahanData > 0 Then
                PesanPeringatan("Masih ada " & JumlahBarisBahanData & " baris data yang belum dicek." & Enter2Baris &
                                "Silakan selesaikan semua pengecekan..!")
                Return
            End If

            Pilihan = MessageBox.Show("Setelah didorong ke Jurnal, seluruh data Stock Opname '" & PilihanBulan &
                                      "' dikunci dan tidak dapat diedit lagi." & Enter2Baris &
                                      "Lanjutkan dorong..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then Return

            Dim SaldoAkhir_Persediaan As Int64 = 0
            Dim BiayaPemakaianBahan As Int64 = 0
            Dim TotalPembelianBahan As Int64 = 0

            Dim SaldoAkhir_Persediaan_Import As Int64 = 0
            Dim BiayaPemakaianBahan_Import As Int64 = 0
            Dim TotalPembelianBahan_Import As Int64 = 0

            Dim HargaPokokProduksi As Int64 = 0
            Dim HargaPokokPenjualan As Int64 = 0
            Dim BiayaProduksi As Int64 = 0

            Select Case JenisStok_Menu

                Case JenisStok_BahanPenolong

                    SaldoAkhir_Persediaan = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBahanPenolong, PilihanBulan_Angka - 1)
                    TotalPembelianBahan = AmbilValue_TotalPembelianBahan(KodeTautanCOA_PembelianBahanPenolong)
                    BiayaPemakaianBahan = (SaldoAkhir_Persediaan + TotalPembelianBahan) - TotalPersediaan_Lokal

                    ResetForm_JurnalAdjusment()

                    'Debet :
                    If TotalPersediaan_Lokal <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBahanPenolong, TotalPersediaan_Lokal)
                    If BiayaPemakaianBahan <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_BiayaPemakaianBahanPenolong, BiayaPemakaianBahan)

                    'Kredit:
                    If TotalPembelianBahan <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PembelianBahanPenolong, TotalPembelianBahan)
                    If SaldoAkhir_Persediaan <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBahanPenolong, SaldoAkhir_Persediaan)

                    TampilkanFormInputJurnalAdjusment(PilihanBulan_Angka)

                    If frm_InputJurnal.JurnalTersimpan = True Then TransferDataStockOpnameKeBulanBerikutnya()

                Case JenisStok_BahanBaku

                    SaldoAkhir_Persediaan = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBahanBaku_Lokal, PilihanBulan_Angka - 1)
                    SaldoAkhir_Persediaan_Import = SaldoAkhirCOA(KodeTautanCOA_PersediaanBahanBaku_Import)
                    TotalPembelianBahan = AmbilValue_TotalPembelianBahan(KodeTautanCOA_PembelianBahanBaku_Lokal)
                    TotalPembelianBahan_Import = AmbilValue_TotalPembelianBahan(KodeTautanCOA_PembelianBahanBaku_Import)
                    BiayaPemakaianBahan = (SaldoAkhir_Persediaan + TotalPembelianBahan) - TotalPersediaan_Lokal
                    BiayaPemakaianBahan_Import = (SaldoAkhir_Persediaan_Import + TotalPembelianBahan_Import) - TotalPersediaan_Import

                    ResetForm_JurnalAdjusment()

                    'Debet :
                    If TotalPersediaan_Lokal <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBahanBaku_Lokal, TotalPersediaan_Lokal)
                    If TotalPersediaan_Import <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBahanBaku_Import, TotalPersediaan_Import)
                    If BiayaPemakaianBahan <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal, BiayaPemakaianBahan)
                    If BiayaPemakaianBahan_Import <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_BiayaPemakaianBahanBaku_Import, BiayaPemakaianBahan_Import)

                    'Kredit:
                    If TotalPembelianBahan <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PembelianBahanBaku_Lokal, TotalPembelianBahan)
                    If TotalPembelianBahan_Import <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PembelianBahanBaku_Import, TotalPembelianBahan_Import)
                    If SaldoAkhir_Persediaan <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBahanBaku_Lokal, SaldoAkhir_Persediaan)
                    If SaldoAkhir_Persediaan_Import <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBahanBaku_Import, SaldoAkhir_Persediaan_Import)

                    TampilkanFormInputJurnalAdjusment(PilihanBulan_Angka)

                    If frm_InputJurnal.JurnalTersimpan = True Then TransferDataStockOpnameKeBulanBerikutnya()

                Case JenisStok_BarangDalamProses

                    SaldoAkhir_Persediaan = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBarangDalamProses, PilihanBulan_Angka - 1)
                    BiayaProduksi = TotalDebetCOA_BulanTertentu(KodeTautanCOA_BiayaProduksi, PilihanBulan_Angka)
                    HargaPokokProduksi = (SaldoAkhir_Persediaan + BiayaProduksi) - TotalPersediaan

                    ResetForm_JurnalAdjusment()

                    'Debet :
                    If TotalPersediaan <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBarangDalamProses, TotalPersediaan)
                    If HargaPokokProduksi <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_HargaPokokProduksi, HargaPokokProduksi)

                    'Kredit:
                    If BiayaProduksi <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_BiayaProduksi, BiayaProduksi)
                    If SaldoAkhir_Persediaan <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBarangDalamProses, SaldoAkhir_Persediaan)

                    If TotalPersediaan = 0 And HargaPokokProduksi = 0 And BiayaProduksi = 0 Then
                        BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBarangDalamProses, 0)
                        BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBarangDalamProses, 0)
                    End If

                    TampilkanFormInputJurnalAdjusment(PilihanBulan_Angka)

                    If frm_InputJurnal.JurnalTersimpan = True Then TransferDataStockOpnameKeBulanBerikutnya()

                Case JenisStok_BarangJadi

                    If CekKeberadaanJurnal_DiBulanTertentu(KodeTautanCOA_PersediaanBarangDalamProses, JenisJurnal_AdjusmentHPP, PilihanBulan_Angka) = False Then
                        PesanPeringatan("Silakan dorong terlebih dahulu Jurnal Persediaan Bahan Barang Dalam Proses untuk Bulan '" &
                                        KonversiAngkaKeBulanString(PilihanBulan_Angka) & "' sebelum melakukan Adjusment.")
                        JenisStok_Menu = JenisStok_BarangDalamProses
                        JenisPengecekan_Menu = JenisPengecekan_CekFisik
                        JudulForm = "Stock Opname " & JenisStok_Menu
                        Me.Text = JudulForm
                        lbl_JudulForm.Text = JudulForm
                        KontenCombo_Bulan()
                        Return
                    End If

                    SaldoAkhir_Persediaan = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBarangJadi, PilihanBulan_Angka - 1)
                    HargaPokokProduksi = TotalDebetCOA_BulanTertentu(KodeTautanCOA_HargaPokokProduksi, PilihanBulan_Angka)
                    HargaPokokPenjualan = (SaldoAkhir_Persediaan + HargaPokokProduksi) - TotalPersediaan

                    ResetForm_JurnalAdjusment()

                    'Debet :
                    If TotalPersediaan <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBarangJadi, TotalPersediaan)
                    If HargaPokokPenjualan <> 0 Then BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_HargaPokokPenjualan, HargaPokokPenjualan)

                    'Kredit:
                    If HargaPokokProduksi <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_HargaPokokProduksi, HargaPokokProduksi)
                    If SaldoAkhir_Persediaan <> 0 Then BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBarangJadi, SaldoAkhir_Persediaan)

                    If TotalPersediaan = 0 And HargaPokokPenjualan = 0 And HargaPokokProduksi = 0 Then
                        BarisDebet_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBarangJadi, 0)
                        BarisKredit_BahanJurnalAdjusment(KodeTautanCOA_PersediaanBarangJadi, 0)
                    End If

                    TampilkanFormInputJurnalAdjusment(PilihanBulan_Angka)

                    If frm_InputJurnal.JurnalTersimpan = True Then TransferDataStockOpnameKeBulanBerikutnya()

            End Select

            If frm_InputJurnal.JurnalTersimpan = True Then
                TampilkanData()
                If PilihanBulan_Angka < 12 Then
                    KontenCombo_Bulan()
                    cmb_Bulan.Text = KonversiAngkaKeBulanString(PilihanBulan_Angka - 1)
                End If
            Else
                Return
            End If

        End If

        If TombolJurnal = TombolJurnal_Lihat Then
            LihatJurnal(NomorJV) 'Jangan menggunakan variabel NomorJV_Terseleksi..!!!
        End If

    End Sub
    Public TotalDebetAdjusment As Int64
    Public TotalKreditAdjusment As Int64
    Public NomorUrutJurnalAdjusment
    Public Sub ResetForm_JurnalAdjusment()
        NomorUrutJurnalAdjusment = 0
        frm_InputJurnal.ResetForm()
        frm_InputJurnal.JalurMasuk = Menu_JurnalAdjusment
        frm_InputJurnal.FungsiForm = FungsiForm_TAMBAH
        TotalDebetAdjusment = 0
        TotalKreditAdjusment = 0
    End Sub
    Sub BarisDebet_BahanJurnalAdjusment(COA, JumlahDebet)
        NomorUrutJurnalAdjusment += 1
        TotalDebetAdjusment += JumlahDebet
        frm_InputJurnal.DataTabelUtama.Rows.Insert(NomorUrutJurnalAdjusment - 1, NomorUrutJurnalAdjusment, COA, AmbilValue_NamaAkun(COA), dk_D, JumlahDebet)
    End Sub
    Sub BarisKredit_BahanJurnalAdjusment(COA, JumlahKredit)
        NomorUrutJurnalAdjusment += 1
        TotalKreditAdjusment += JumlahKredit
        frm_InputJurnal.DataTabelUtama.Rows.Insert(NomorUrutJurnalAdjusment - 1, NomorUrutJurnalAdjusment, COA, PenjorokNamaAkun & AmbilValue_NamaAkun(COA), dk_K, Kosongan, JumlahKredit)
    End Sub
    Sub TampilkanFormInputJurnalAdjusment(BulanAdjusment_Angka)
        frm_InputJurnal.JumlahBarisJurnal = NomorUrutJurnalAdjusment
        frm_InputJurnal.DataTabelUtama.Item("Debet_", NomorUrutJurnalAdjusment + 1).Value = TotalDebetAdjusment
        frm_InputJurnal.DataTabelUtama.Item("Kredit_", NomorUrutJurnalAdjusment + 1).Value = TotalKreditAdjusment
        If TotalDebetAdjusment = TotalKreditAdjusment Then
            frm_InputJurnal.lbl_StatusBalance.ForeColor = WarnaHijauSolid
            frm_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
            BeginInvoke(Sub() frm_InputJurnal.btn_Simpan.Enabled = True)
        Else
            frm_InputJurnal.lbl_StatusBalance.ForeColor = WarnaMerahSolid
            frm_InputJurnal.lbl_StatusBalance.Text = "Ada Selisih"
            BeginInvoke(Sub() frm_InputJurnal.btn_Simpan.Enabled = False)
        End If
        frm_InputJurnal.dtp_TanggalJurnal.Value = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(BulanAdjusment_Angka, TahunBukuAktif)
        frm_InputJurnal.cmb_JenisJurnal.Text = JenisJurnal_AdjusmentHPP
        frm_InputJurnal.ShowDialog()
    End Sub
    Sub Sub_TombolJurnal_Dorong()
        btn_Jurnal.Text = TombolJurnal_Dorong
        TombolJurnal = TombolJurnal_Dorong
    End Sub
    Sub Sub_TombolJurnal_Lihat()
        btn_Jurnal.Text = TombolJurnal_Lihat
        TombolJurnal = TombolJurnal_Lihat
    End Sub
    Function AmbilValue_TotalPembelianBahan(COA As String) As Int64
        Dim Total As Int64
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                              " WHERE COA_Produk = '" & COA & "' " &
                              " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunBukuAktif & "-" & PilihanBulan_NomorString & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Total = 0
        Do While dr.Read
            Total += dr.Item("Total_Harga_Per_Item")
        Loop
        AksesDatabase_Transaksi(Tutup)
        Return Total
    End Function

    Sub TransferDataStockOpnameKeBulanBerikutnya()
        'Cek Dulu, apakah sudah ada data di bulan berikutnya. Kalau sudah ada, berarti tidak perlu ada transfer data..!
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryAwal & " AND DATE_FORMAT(Tanggal_Pengecekan, '%Y-%m') = '" & TahunBukuAktif & "-" &
                              KonversiAngkaKeStringDuaDigit(PilihanBulan_Angka + 1) & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If dr.HasRows Then Return
        AksesDatabase_Transaksi(Tutup)
        Dim NomorID_Telusur = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_StockOpname")
        Dim JumlahBarisDataTertransfer = 0
        If PilihanBulan_Angka < 12 Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryAwal & FilterBulan, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorID_Telusur += 1
                JumlahBarisDataTertransfer += 1
                cmdSIMPAN = New OdbcCommand(" INSERT INTO tbl_StockOpname VALUES ( " &
                                            " '" & NomorID_Telusur & "', " &
                                            " '" & dr.Item("Jenis_Stok") & "', " &
                                            " '" & TanggalFormatSimpan(AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(PilihanBulan_Angka + 1, TahunBukuAktif)) & "', " &
                                            " '" & dr.Item("Nama_Barang") & "', " &
                                            " '" & dr.Item("Jumlah_Barang") & "', " &
                                            " '" & dr.Item("Satuan") & "', " &
                                            " '" & dr.Item("Harga_Satuan") & "', " &
                                            " '" & dr.Item("Jumlah_Harga") & "', " &
                                            " '" & dr.Item("Asal") & "', " &
                                            " '" & dr.Item("Lokasi") & "', " &
                                            " '" & dr.Item("Keterangan") & "', " &
                                            " '" & JenisData_Bahan & "', " &
                                            " '" & UserAktif & "' ) ",
                                            KoneksiDatabaseTransaksi)
                cmdSIMPAN_ExecuteNonQuery()
            Loop
            AksesDatabase_Transaksi(Tutup)
            If StatusSuntingDatabase = True And JumlahBarisDataTertransfer > 0 Then
                PesanSukses("Selusuh Data Stock Opname '" & PilihanBulan & "' BERHASIL disalin ke '" &
                            KonversiAngkaKeBulanString(PilihanBulan_Angka + 1) & "' sebagai BAHAN PENGECEKAN berikutnya.")
            End If
        End If
    End Sub



    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub



    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        frm_InputStockOpname.BulanPengunci_Angka = PilihanBulan_Angka 'Ini Harus Paling Awal, sebelum Reset Form
        frm_InputStockOpname.ResetForm()
        frm_InputStockOpname.FungsiForm = FungsiForm_TAMBAH
        frm_InputStockOpname.JenisStok = JenisStok_Menu
        frm_InputStockOpname.ShowDialog()

    End Sub


    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        frm_InputStockOpname.BulanPengunci_Angka = PilihanBulan_Angka 'Ini Harus Paling Awal, sebelum Reset Form
        frm_InputStockOpname.ResetForm()
        frm_InputStockOpname.FungsiForm = FungsiForm_EDIT
        frm_InputStockOpname.NomorID = NomorID_Terseleksi
        frm_InputStockOpname.JenisStok = JenisStok_Menu
        frm_InputStockOpname.dtp_TanggalPengecekan.Value = TanggalPengecekan_Terseleksi
        frm_InputStockOpname.txt_NamaBarang.Text = NamaBarang_Terseleksi
        frm_InputStockOpname.txt_JumlahBarang.Text = JumlahBarang_Terseleksi
        frm_InputStockOpname.txt_Satuan.Text = Satuan_Terseleksi
        frm_InputStockOpname.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        frm_InputStockOpname.txt_JumlahHarga.Text = JumlahHarga_Terseleksi
        frm_InputStockOpname.cmb_Asal.Text = Asal_Terseleksi
        frm_InputStockOpname.txt_Lokasi.Text = Lokasi_Terseleksi
        frm_InputStockOpname.txt_Keterangan.Text = Keterangan_Terseleksi
        frm_InputStockOpname.ShowDialog()

    End Sub


    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" DELETE FROM tbl_StockOpname WHERE Nomor_ID = '" & NomorID_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            TampilkanData()
            pesan_DataTerpilihBerhasilDihapus()
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
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_Urut", BarisTerseleksi).Value)
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_ID", BarisTerseleksi).Value)
        JenisStok_Terseleksi = DataTabelUtama("Jenis_Stok", BarisTerseleksi).Value
        TanggalPengecekan_Terseleksi = DataTabelUtama("Tanggal_Pengecekan", BarisTerseleksi).Value
        NamaBarang_Terseleksi = DataTabelUtama("Nama_Barang", BarisTerseleksi).Value
        JumlahBarang_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Barang", BarisTerseleksi).Value)
        Satuan_Terseleksi = DataTabelUtama("Satuan_", BarisTerseleksi).Value
        HargaSatuan_Terseleksi = AmbilAngka(DataTabelUtama("Harga_Satuan", BarisTerseleksi).Value)
        JumlahHarga_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Harga", BarisTerseleksi).Value)
        Asal_Terseleksi = DataTabelUtama("Asal_", BarisTerseleksi).Value
        Lokasi_Terseleksi = DataTabelUtama("Lokasi_", BarisTerseleksi).Value
        Keterangan_Terseleksi = DataTabelUtama("Keterangan_", BarisTerseleksi).Value
        JenisData_Terseleksi = DataTabelUtama("Jenis_Data", BarisTerseleksi).Value

        If NomorID_Terseleksi > 0 Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If


    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

    Private Sub lbl_bantuan_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbl_bantuan.LinkClicked
        FiturDalamPengembangan()
    End Sub

End Class