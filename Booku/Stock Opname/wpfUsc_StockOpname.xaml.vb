Imports System.Data.Odbc
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports System.Windows.Threading
Imports bcomm

Public Class wpfUsc_StockOpname

    Public StatusAktif As Boolean
    Private SudahDimuat As Boolean = False

    ' Flag untuk mencegah multiple loading bersamaan
    Private SedangMemuatData As Boolean = False

    ' Flag untuk mencegah eksekusi TampilkanData saat ComboBox sedang diisi
    Dim EksekusiTampilanData As Boolean
    Public JudulForm
    Public KesesuaianJurnal As Boolean

    Public JenisStok_Menu As String
    Public JenisPengecekan_Menu

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

    Dim NomorJV As Int64
    Dim COAPersediaan

    Dim JumlahBarisBahanData

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        brd_Jurnal.Visibility = Visibility.Collapsed
        btn_Jurnal.Visibility = Visibility.Collapsed

        Terabas()

        ProsesLoadingForm = True

        JudulForm = "Stock Opname " & JenisStok_Menu

        Select Case JenisStok_Menu
            Case JenisStok_BahanPenolong
                COAPersediaan = KodeTautanCOA_PersediaanBahanPenolong
                Asal_.Visibility = Visibility.Visible
            Case JenisStok_BahanBaku
                COAPersediaan = KodeTautanCOA_PersediaanBahanBaku_Lokal
                Asal_.Visibility = Visibility.Visible
            Case JenisStok_BarangDalamProses
                COAPersediaan = KodeTautanCOA_PersediaanBarangDalamProses
                Asal_.Visibility = Visibility.Collapsed
            Case JenisStok_BarangJadi
                COAPersediaan = KodeTautanCOA_PersediaanBarangJadi
                Asal_.Visibility = Visibility.Collapsed
        End Select

        Select Case JenisPengecekan_Menu
            Case JenisPengecekan_CekFisik
                Nama_Barang.Visibility = Visibility.Visible
                Jumlah_Barang.Visibility = Visibility.Visible
                Satuan_.Visibility = Visibility.Visible
                Harga_Satuan.Visibility = Visibility.Visible
                Nomor_Invoice.Visibility = Visibility.Collapsed
                Tanggal_Invoice.Visibility = Visibility.Collapsed
                Nomor_Faktur_Pajak.Visibility = Visibility.Collapsed
                Nama_Supplier.Visibility = Visibility.Collapsed
                Lokasi_.Visibility = Visibility.Visible
                Kode_Project.Visibility = Visibility.Collapsed
                Kode_Akun.Visibility = Visibility.Collapsed
                Nama_Akun.Visibility = Visibility.Collapsed
                If JenisStok_Menu = JenisStok_BarangDalamProses Then JudulForm &= " - Cek Fisik"
                'btn_Jurnal.Visibility = Visibility.Visible
            Case JenisPengecekan_TarikanData
                Nama_Barang.Visibility = Visibility.Collapsed
                Jumlah_Barang.Visibility = Visibility.Collapsed
                Satuan_.Visibility = Visibility.Collapsed
                Harga_Satuan.Visibility = Visibility.Collapsed
                Nomor_Invoice.Visibility = Visibility.Visible
                Tanggal_Invoice.Visibility = Visibility.Visible
                Nomor_Faktur_Pajak.Visibility = Visibility.Visible
                Nama_Supplier.Visibility = Visibility.Visible
                Lokasi_.Visibility = Visibility.Collapsed
                Kode_Project.Visibility = Visibility.Visible
                Kode_Akun.Visibility = Visibility.Visible
                Nama_Akun.Visibility = Visibility.Visible
                JudulForm &= " - Tarikan Data"
                btn_Jurnal.Visibility = Visibility.Collapsed
        End Select

        lbl_JudulForm.Text = JudulForm

        KontenCombo_Bulan()

        ProsesLoadingForm = False

        SudahDimuat = True
    End Sub


    Sub RefreshTampilanData()
        EksekusiTampilanData = False
        KontenCombo_Bulan()
        EksekusiTampilanData = True
        TampilkanData()
    End Sub

    Async Sub TampilkanDataAsync()

        ' Guard clause: Jangan eksekusi saat ComboBox sedang diisi
        If Not EksekusiTampilanData Then Return

        ' Guard clause: Jangan eksekusi jika sedang loading
        If SedangMemuatData Then Return
        SedangMemuatData = True

        ' Disable UI dan tampilkan loading
        KetersediaanMenuHalaman(pnl_Halaman, False)
        Await Task.Delay(50)  ' Beri waktu UI render

        Try
            KesesuaianJurnal = True

            'Style Tabel :
            datatabelUtama.Rows.Clear()

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
                NomorJV = NomorJV_SaatPengecekanJurnal
                pnl_CRUD.IsEnabled = False
            Else
                Sub_TombolJurnal_Dorong()
                NomorJV = 0
                If JenisPengecekan_Menu = JenisPengecekan_CekFisik Then
                    pnl_CRUD.IsEnabled = True
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
                Await Task.Yield()  ' Beri kesempatan UI refresh
            Loop

            AksesDatabase_Transaksi(Tutup)

            If TotalPersediaan > 0 Then
                datatabelUtama.Rows.Add(Kosongan) 'Variabel "Kosongan" jangan dihapus. Ini diperlukan untuk menghindari dbNull. Ada kepentingan logika di situ...!!!!
                datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan,
                                        Kosongan, Kosongan, 0, Kosongan, "TOTAL",
                                        Kosongan, Kosongan, Kosongan, "TOTAL",
                                        TotalPersediaan)
            End If

        Catch ex As Exception
            mdl_Logger.WriteException(ex, "TampilkanDataAsync - wpfUsc_StockOpname")

        Finally
            BersihkanSeleksi_SetelahLoading()

        End Try

        Dim PR As String = "PR : " & Enter2Baris
        PR &= "- Tinjau ulang perhitungan, karena harus memperhatikan bahan yang diretur setelah pembelian...!!!" & Enter2Baris

        'Dispatcher.BeginInvoke(Sub() PesanUntukProgrammer(PR))

    End Sub

    ' Wrapper untuk backward compatibility
    Public Sub TampilkanData()
        TampilkanDataAsync()
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
        Keterangan = PenghapusEnter(dr.Item("Keterangan"))
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
        datatabelUtama.Rows.Add(NomorUrut, NomorID, JenisStok,
                                TanggalPengecekan, NamaBarang, JumlahBarang, Satuan, HargaSatuan,
                                NomorInvoice, TanggalInvoice, NomorFakturPajak, NamaSupplier,
                                JumlahHarga, Asal, Lokasi,
                                KodeProject, Kodeakun, NamaAkun,
                                Keterangan, JenisData)
    End Sub

    Sub BersihkanSeleksi()
        BersihkanSeleksi_WPF(datagridUtama, datatabelUtama, BarisTerseleksi, JumlahBaris)
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        SedangMemuatData = False
    End Sub

    Sub BersihkanSeleksi_SetelahLoading()
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)
        SedangMemuatData = False
    End Sub


    Sub KontenCombo_Bulan()
        Dim BulanTertuaAngka As Integer
        'Dim Kolom = "Tanggal_Transaksi"
        'Dim TabelDanKriteria = " tbl_Transaksi WHERE COA = '" & COAPersediaan & "' " & " AND Jenis_Jurnal = '" & JenisJurnal_AdjusmentHPP & "' "
        'BulanTertuaAngka = 1 + AmbilValue_BulanTertuaAngka(TabelDanKriteria, Kolom)
        BulanTertuaAngka = BulanBukuAktif
        If BulanTertuaAngka > 12 Then BulanTertuaAngka = 12
        KontenComboBulanDibatasi_Public_WPF(cmb_Bulan, BulanTertuaAngka)
        cmb_Bulan.SelectedValue = KonversiAngkaKeBulanString(BulanTertuaAngka)
    End Sub


    Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        TampilkanData()
    End Sub

    Sub btn_Jurnal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Jurnal.Click
        Select Case TombolJurnal
            Case TombolJurnal_Dorong
                DorongKeJurnal()
            Case TombolJurnal_Lihat
                LihatJurnal(NomorJV) 'Jangan menggunakan variabel NomorJV_Terseleksi..!!!
        End Select
    End Sub
    Sub DorongKeJurnal()

        If JumlahBarisBahanData > 0 Then
            PesanPeringatan("Masih ada " & JumlahBarisBahanData & " baris data yang belum dicek." & Enter2Baris &
                                "Silakan selesaikan semua pengecekan..!")
            Return
        End If

        Dim pesan As String = "Setelah didorong ke Jurnal, seluruh data Stock Opname '" & PilihanBulan &
                "' dikunci dan tidak dapat diedit lagi."

        If Not Pesan_KonfirmasiLanjutkan(pesan) Then Return

        Dim SaldoAkhir_Persediaan As Int64 = 0
        Dim BiayaPemakaianBahan As Int64 = 0
        Dim TotalPembelianBahan As Int64 = 0

        Dim SaldoAkhir_Persediaan_Import As Int64 = 0
        Dim BiayaPemakaianBahan_Import As Int64 = 0
        Dim TotalPembelianBahan_Import As Int64 = 0

        Dim HargaPokokProduksi As Int64 = 0
        Dim HargaPokokPenjualan As Int64 = 0
        Dim BiayaProduksi As Int64 = 0

        Dim TanggalJurnal As Date = AmbilTanggalAkhirBulan_BerdasarkanBulanDanTahun(PilihanBulan_Angka, TahunBukuAktif)

        Select Case JenisStok_Menu

            Case JenisStok_BahanPenolong

                SaldoAkhir_Persediaan = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBahanPenolong, PilihanBulan_Angka - 1)
                TotalPembelianBahan = AmbilValue_TotalPembelianBahan(KodeTautanCOA_PembelianBahanPenolong)
                BiayaPemakaianBahan = (SaldoAkhir_Persediaan + TotalPembelianBahan) - TotalPersediaan_Lokal


                Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, TanggalJurnal)

                'Debet :
                If TotalPersediaan_Lokal = 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBahanPenolong, 0)    'Ini harus tetap ada jurnalnya walau pun 0, untuk kebutuhan sistem.
                If TotalPersediaan_Lokal <> 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBahanPenolong, TotalPersediaan_Lokal)
                If BiayaPemakaianBahan > 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaPemakaianBahanPenolong, BiayaPemakaianBahan)

                'Kredit:
                If BiayaPemakaianBahan < 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaPemakaianBahanPenolong, -BiayaPemakaianBahan)
                If TotalPembelianBahan <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PembelianBahanPenolong, TotalPembelianBahan)
                If SaldoAkhir_Persediaan <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBahanPenolong, SaldoAkhir_Persediaan)
                If SaldoAkhir_Persediaan = 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBahanPenolong, 0)   'Ini harus tetap ada jurnalnya walau pun 0, untuk kebutuhan sistem.

                TampilkanFormInputJurnal()

                If win_InputJurnal.JurnalTersimpan = True Then TransferDataStockOpnameKeBulanBerikutnya()

            Case JenisStok_BahanBaku

                SaldoAkhir_Persediaan = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBahanBaku_Lokal, PilihanBulan_Angka - 1)
                SaldoAkhir_Persediaan_Import = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBahanBaku_Import, PilihanBulan_Angka - 1)
                TotalPembelianBahan = AmbilValue_TotalPembelianBahan(KodeTautanCOA_PembelianBahanBaku_Lokal)
                TotalPembelianBahan_Import = AmbilValue_TotalPembelianBahan(KodeTautanCOA_PembelianBahanBaku_Import)
                BiayaPemakaianBahan = (SaldoAkhir_Persediaan + TotalPembelianBahan) - TotalPersediaan_Lokal
                BiayaPemakaianBahan_Import = (SaldoAkhir_Persediaan_Import + TotalPembelianBahan_Import) - TotalPersediaan_Import

                Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, TanggalJurnal)

                'Debet :
                If TotalPersediaan_Lokal = 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBahanBaku_Lokal, 0)      'Ini harus tetap ada jurnalnya walau pun 0, untuk kebutuhan sistem.
                If TotalPersediaan_Import = 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBahanBaku_Import, 0)    'Idem
                If TotalPersediaan_Lokal <> 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBahanBaku_Lokal, TotalPersediaan_Lokal)
                If TotalPersediaan_Import <> 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBahanBaku_Import, TotalPersediaan_Import)
                If BiayaPemakaianBahan > 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal, BiayaPemakaianBahan)
                If BiayaPemakaianBahan_Import > 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_BiayaPemakaianBahanBaku_Import, BiayaPemakaianBahan_Import)

                'Kredit:
                If BiayaPemakaianBahan < 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal, -BiayaPemakaianBahan)
                If BiayaPemakaianBahan_Import < 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaPemakaianBahanBaku_Import, -BiayaPemakaianBahan_Import)
                If TotalPembelianBahan <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PembelianBahanBaku_Lokal, TotalPembelianBahan)
                If TotalPembelianBahan_Import <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PembelianBahanBaku_Import, TotalPembelianBahan_Import)
                If SaldoAkhir_Persediaan <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBahanBaku_Lokal, SaldoAkhir_Persediaan)
                If SaldoAkhir_Persediaan_Import <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBahanBaku_Import, SaldoAkhir_Persediaan_Import)
                If SaldoAkhir_Persediaan = 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBahanBaku_Lokal, 0)         'Ini harus tetap ada jurnalnya walau pun 0, untuk kebutuhan sistem.
                If SaldoAkhir_Persediaan_Import = 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBahanBaku_Import, 0) 'Idem

                TampilkanFormInputJurnal()

                If win_InputJurnal.JurnalTersimpan = True Then TransferDataStockOpnameKeBulanBerikutnya()

            Case JenisStok_BarangDalamProses

                SaldoAkhir_Persediaan = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBarangDalamProses, PilihanBulan_Angka - 1)
                BiayaProduksi = TotalDebetCOA_BulanTertentu(KodeTautanCOA_BiayaProduksi, PilihanBulan_Angka)
                HargaPokokProduksi = (SaldoAkhir_Persediaan + BiayaProduksi) - TotalPersediaan

                Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, TanggalJurnal)

                'Debet :
                If TotalPersediaan = 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBarangDalamProses, 0)  'Ini harus tetap ada jurnalnya walau pun 0, untuk kebutuhan sistem.
                If TotalPersediaan <> 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBarangDalamProses, TotalPersediaan)
                If HargaPokokProduksi <> 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_HargaPokokProduksi, HargaPokokProduksi)

                'Kredit:
                If BiayaProduksi <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_BiayaProduksi, BiayaProduksi)
                If SaldoAkhir_Persediaan <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBarangDalamProses, SaldoAkhir_Persediaan)

                If TotalPersediaan = 0 And HargaPokokProduksi = 0 And BiayaProduksi = 0 Then
                    TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBarangDalamProses, 0)
                    TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBarangDalamProses, 0)
                End If

                TampilkanFormInputJurnal()

                If win_InputJurnal.JurnalTersimpan = True Then TransferDataStockOpnameKeBulanBerikutnya()

            Case JenisStok_BarangJadi

                If CekKeberadaanJurnal_DiBulanTertentu(KodeTautanCOA_PersediaanBarangDalamProses, JenisJurnal_AdjusmentHPP, PilihanBulan_Angka) = False Then
                    PesanPeringatan("Silakan dorong terlebih dahulu Jurnal Persediaan Bahan Barang Dalam Proses untuk Bulan '" &
                                        KonversiAngkaKeBulanString(PilihanBulan_Angka) & "' sebelum melakukan Adjusment.")
                    'JenisStok_Menu = JenisStok_BarangDalamProses
                    'JenisPengecekan_Menu = JenisPengecekan_CekFisik
                    'JudulForm = "Stock Opname " & JenisStok_Menu
                    'Me.Text = JudulForm
                    'lbl_JudulForm.Text = JudulForm
                    'KontenCombo_Bulan()
                    win_BOOKU.BukaModul_StockOpname_BarangDalamProses_CekFisik()
                    Return
                End If

                SaldoAkhir_Persediaan = SaldoAkhirCOA_SampaiAkhirBulanTertentu(KodeTautanCOA_PersediaanBarangJadi, PilihanBulan_Angka - 1)
                HargaPokokProduksi = TotalDebetCOA_BulanTertentu(KodeTautanCOA_HargaPokokProduksi, PilihanBulan_Angka)
                HargaPokokPenjualan = (SaldoAkhir_Persediaan + HargaPokokProduksi) - TotalPersediaan

                Reset_BahanJurnal(JenisJurnal_AdjusmentHPP, TanggalJurnal)

                'Debet :
                If TotalPersediaan = 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBarangJadi, 0) 'Ini harus tetap ada jurnalnya walau pun 0, untuk kebutuhan sistem.
                If TotalPersediaan <> 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_PersediaanBarangJadi, TotalPersediaan)
                If HargaPokokPenjualan <> 0 Then TambahBarisDebet_BahanJurnal(KodeTautanCOA_HargaPokokPenjualan, HargaPokokPenjualan)

                'Kredit:
                If HargaPokokProduksi <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_HargaPokokProduksi, HargaPokokProduksi)
                If SaldoAkhir_Persediaan <> 0 Then TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBarangJadi, SaldoAkhir_Persediaan)

                If TotalPersediaan = 0 And HargaPokokPenjualan = 0 And HargaPokokProduksi = 0 Then
                    TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBarangJadi, 0)
                    TambahBarisKredit_BahanJurnal(KodeTautanCOA_PersediaanBarangJadi, 0)
                End If

                TampilkanFormInputJurnal()

                If win_InputJurnal.JurnalTersimpan = True Then TransferDataStockOpnameKeBulanBerikutnya()

        End Select

        If win_InputJurnal.JurnalTersimpan = True Then
            If StatusAktif Then
                If PilihanBulan_Angka < 12 Then
                    KontenCombo_Bulan()
                End If
                usc_JurnalAdjusment_HPP.TampilkanData()
            End If
        Else
            Return
        End If

    End Sub
    Sub Sub_TombolJurnal_Dorong()
        btn_Jurnal.Content = TombolJurnal_Dorong
        TombolJurnal = TombolJurnal_Dorong
    End Sub
    Sub Sub_TombolJurnal_Lihat()
        btn_Jurnal.Content = TombolJurnal_Lihat
        TombolJurnal = TombolJurnal_Lihat
    End Sub
    Function AmbilValue_TotalPembelianBahan(COA As String) As Int64
        Dim Total As Int64
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT Total_Harga_Per_Item, Kode_Mata_Uang, Kurs FROM tbl_Pembelian_Invoice " &
                              " WHERE COA_Produk = '" & COA & "' " &
                              " AND DATE_FORMAT(Tanggal_Invoice, '%Y-%m') = '" & TahunBukuAktif & "-" & PilihanBulan_NomorString & "' ",
                              KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Total = 0
        Do While dr.Read
            Total += AmbilValue_NilaiMataUang(dr.Item("Kode_Mata_Uang"), dr.Item("Kurs"), dr.Item("Total_Harga_Per_Item"))
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


    Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        win_InputStockOpname = New wpfWin_InputStockOpname
        win_InputStockOpname.BulanPengunci_Angka = PilihanBulan_Angka    'Ini Harus Paling Awal, sebelum Reset Form
        win_InputStockOpname.JenisStok = JenisStok_Menu                  'Ini Harus Paling Awal, sebelum Reset Form
        win_InputStockOpname.ResetForm()
        win_InputStockOpname.FungsiForm = FungsiForm_TAMBAH
        win_InputStockOpname.ShowDialog()
    End Sub


    Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        win_InputStockOpname = New wpfWin_InputStockOpname
        win_InputStockOpname.BulanPengunci_Angka = PilihanBulan_Angka    'Ini Harus Paling Awal, sebelum Reset Form
        win_InputStockOpname.JenisStok = JenisStok_Menu                  'Ini Harus Paling Awal, sebelum Reset Form
        win_InputStockOpname.ResetForm()
        win_InputStockOpname.FungsiForm = FungsiForm_EDIT
        win_InputStockOpname.NomorID = NomorID_Terseleksi
        win_InputStockOpname.dtp_TanggalPengecekan.SelectedDate = TanggalFormatWPF(TanggalPengecekan_Terseleksi)
        win_InputStockOpname.txt_NamaBarang.Text = NamaBarang_Terseleksi
        win_InputStockOpname.txt_JumlahBarang.Text = JumlahBarang_Terseleksi
        win_InputStockOpname.txt_Satuan.Text = Satuan_Terseleksi
        win_InputStockOpname.txt_HargaSatuan.Text = HargaSatuan_Terseleksi
        win_InputStockOpname.txt_JumlahHarga.Text = JumlahHarga_Terseleksi
        win_InputStockOpname.cmb_Asal.SelectedValue = Asal_Terseleksi
        win_InputStockOpname.txt_Lokasi.Text = Lokasi_Terseleksi
        IsiValueElemenRichTextBox(win_InputStockOpname.txt_Keterangan, Keterangan_Terseleksi)
        win_InputStockOpname.ShowDialog()
    End Sub


    Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click
        If Not Pesan_KonfirmasiHapus() Then Return

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


    Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub cmb_Bulan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Bulan.SelectionChanged
        PilihanBulan = cmb_Bulan.SelectedValue
        PilihanBulan_NomorString = KonversiBulanKeNomor_String(PilihanBulan)
        PilihanBulan_Angka = AmbilAngka(PilihanBulan_NomorString)
        If PilihanBulan_Angka = 0 Then
            btn_Jurnal.IsEnabled = False
        Else
            btn_Jurnal.IsEnabled = True
            TampilkanData()
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
        NomorID_Terseleksi = AmbilAngka(rowviewUtama("Nomor_ID"))
        JenisStok_Terseleksi = rowviewUtama("Jenis_Stok")
        TanggalPengecekan_Terseleksi = rowviewUtama("Tanggal_Pengecekan")
        NamaBarang_Terseleksi = rowviewUtama("Nama_Barang")
        JumlahBarang_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Barang"))
        Satuan_Terseleksi = rowviewUtama("Satuan_")
        HargaSatuan_Terseleksi = AmbilAngka(rowviewUtama("Harga_Satuan"))
        JumlahHarga_Terseleksi = AmbilAngka(rowviewUtama("Jumlah_Harga"))
        Asal_Terseleksi = rowviewUtama("Asal_")
        Lokasi_Terseleksi = rowviewUtama("Lokasi_")
        Keterangan_Terseleksi = rowviewUtama("Keterangan_")
        JenisData_Terseleksi = rowviewUtama("Jenis_Data")

        If NomorID_Terseleksi > 0 Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        btn_Edit_Click(sender, e)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If btn_Jurnal.Content = TombolJurnal_Dorong Then e.Row.Foreground = clrTeksPrimer
        If btn_Jurnal.Content = TombolJurnal_Lihat Then e.Row.Foreground = clrNeutral500
        Dim JenisData As String = Kosongan
        If Not IsDBNull(e.Row.Item("Jenis_Data")) Then JenisData = e.Row.Item("Jenis_Data")
        Select Case JenisData
            Case JenisData_Bahan
                e.Row.Foreground = clrWarning
            Case JenisData_Normal
                If NomorJV = 0 Then e.Row.Foreground = clrTeksPrimer
                If NomorJV > 0 Then e.Row.Foreground = clrNeutral500
        End Select
        If e.Row.Item("Nomor_Urut") = Kosongan And JumlahBarisBahanData > 0 Then e.Row.Foreground = clrWarning '(Pewarnaan untuk Baris Total).
    End Sub
    Private Sub datagridUtama_LostFocus(sender As Object, e As RoutedEventArgs) Handles datagridUtama.LostFocus
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
    Dim Jenis_Stok As New DataGridTextColumn
    Dim Tanggal_Pengecekan As New DataGridTextColumn
    Dim Nama_Barang As New DataGridTextColumn
    Dim Jumlah_Barang As New DataGridTextColumn
    Dim Satuan_ As New DataGridTextColumn
    Dim Harga_Satuan As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Nama_Supplier As New DataGridTextColumn
    Dim Jumlah_Harga As New DataGridTextColumn
    Dim Asal_ As New DataGridTextColumn
    Dim Lokasi_ As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn
    Dim Jenis_Data As New DataGridTextColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Nomor_ID")
        datatabelUtama.Columns.Add("Jenis_Stok")
        datatabelUtama.Columns.Add("Tanggal_Pengecekan")
        datatabelUtama.Columns.Add("Nama_Barang")
        datatabelUtama.Columns.Add("Jumlah_Barang", GetType(Int64))
        datatabelUtama.Columns.Add("Satuan_")
        datatabelUtama.Columns.Add("Harga_Satuan")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Nama_Supplier")
        datatabelUtama.Columns.Add("Jumlah_Harga", GetType(Int64))
        datatabelUtama.Columns.Add("Asal_")
        datatabelUtama.Columns.Add("Lokasi_")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Keterangan_")
        datatabelUtama.Columns.Add("Jenis_Data")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_ID, "Nomor_ID", "Nomor ID", 45, FormatAngka, KananTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Stok, "Jenis_Stok", "Jenis Stok", 45, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Pengecekan, "Tanggal_Pengecekan", "Tanggal Pengecekan", 81, FormatString, TengahTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Barang, "Nama_Barang", "Nama Barang", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Barang, "Jumlah_Barang", "Jumlah Barang", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Satuan_, "Satuan_", "Satuan", 63, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Harga_Satuan, "Harga_Satuan", "Harga Satuan", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 81, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Supplier, "Nama_Supplier", "Nama Supplier", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Harga, "Jumlah_Harga", "Jumlah Harga", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Asal_, "Asal_", "Asal", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Lokasi_, "Lokasi_", "Lokasi", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 111, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Keterangan_, "Keterangan_", "Keterangan", 210, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Data, "Jenis_Data", "Jenis Data", 75, FormatString, KiriTengah, KunciUrut, Tersembunyi)

    End Sub


    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        grb_InfoSaldo.Visibility = Visibility.Collapsed
        pnl_SaldoAwalPlusAJP.Visibility = Visibility.Collapsed
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        txt_SaldoBerdasarkanList.IsReadOnly = True
        txt_saldoBerdasarkanCOA_PlusPenyesuaian.IsReadOnly = True
        txt_SelisihSaldo.IsReadOnly = True
        txt_SaldoAwalPerBaris.IsReadOnly = True
        txt_JumlahBayarPerBaris.IsReadOnly = True
        txt_SaldoAkhirPerbaris.IsReadOnly = True
        txt_SaldoAwalBerdasarkanCOA.IsReadOnly = True
        txt_AJP.IsReadOnly = True
        txt_TotalTabel.IsReadOnly = True
        lbl_KeteranganLunas.Visibility = Visibility.Collapsed
        txt_KeteranganLunas.Visibility = Visibility.Collapsed
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
