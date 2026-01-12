' =====================================================================
' Modul Deklarasi Variabel Host WPF
' Semua variabel host dideklarasikan secara Public di modul ini
' =====================================================================

Module wpfMdl_ClassHost

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG USAHA
    ' =================================================================
    Public host_BukuPengawasanHutangUsaha As wpfHost_BukuPengawasanHutangUsaha
    Public host_BukuPengawasanHutangUsaha_Afiliasi As wpfHost_BukuPengawasanHutangUsaha_Afiliasi
    Public host_BukuPengawasanHutangUsaha_NonAfiliasi As wpfHost_BukuPengawasanHutangUsaha_NonAfiliasi
    Public host_BukuPengawasanHutangUsaha_Impor_USD As wpfHost_BukuPengawasanHutangUsaha_Impor_USD
    Public host_BukuPengawasanHutangUsaha_Impor_AUD As wpfHost_BukuPengawasanHutangUsaha_Impor_AUD
    Public host_BukuPengawasanHutangUsaha_Impor_JPY As wpfHost_BukuPengawasanHutangUsaha_Impor_JPY
    Public host_BukuPengawasanHutangUsaha_Impor_CNY As wpfHost_BukuPengawasanHutangUsaha_Impor_CNY
    Public host_BukuPengawasanHutangUsaha_Impor_EUR As wpfHost_BukuPengawasanHutangUsaha_Impor_EUR
    Public host_BukuPengawasanHutangUsaha_Impor_SGD As wpfHost_BukuPengawasanHutangUsaha_Impor_SGD
    Public host_BukuPengawasanHutangUsaha_Impor_GBP As wpfHost_BukuPengawasanHutangUsaha_Impor_GBP

    ' =================================================================
    ' BUKU PENGAWASAN - PIUTANG USAHA
    ' =================================================================
    Public host_BukuPengawasanPiutangUsaha As wpfHost_BukuPengawasanPiutangUsaha
    Public host_BukuPengawasanPiutangUsaha_Afiliasi As wpfHost_BukuPengawasanPiutangUsaha_Afiliasi
    Public host_BukuPengawasanPiutangUsaha_NonAfiliasi As wpfHost_BukuPengawasanPiutangUsaha_NonAfiliasi
    Public host_BukuPengawasanPiutangUsaha_Ekspor_USD As wpfHost_BukuPengawasanPiutangUsaha_Ekspor_USD
    Public host_BukuPengawasanPiutangUsaha_Ekspor_AUD As wpfHost_BukuPengawasanPiutangUsaha_Ekspor_AUD
    Public host_BukuPengawasanPiutangUsaha_Ekspor_JPY As wpfHost_BukuPengawasanPiutangUsaha_Ekspor_JPY
    Public host_BukuPengawasanPiutangUsaha_Ekspor_CNY As wpfHost_BukuPengawasanPiutangUsaha_Ekspor_CNY
    Public host_BukuPengawasanPiutangUsaha_Ekspor_EUR As wpfHost_BukuPengawasanPiutangUsaha_Ekspor_EUR
    Public host_BukuPengawasanPiutangUsaha_Ekspor_SGD As wpfHost_BukuPengawasanPiutangUsaha_Ekspor_SGD
    Public host_BukuPengawasanPiutangUsaha_Ekspor_GBP As wpfHost_BukuPengawasanPiutangUsaha_Ekspor_GBP

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG-PIUTANG AFILIASI
    ' =================================================================
    Public host_BukuPengawasanHutangAfiliasi As wpfHost_BukuPengawasanHutangAfiliasi
    Public host_BukuPengawasanPiutangAfiliasi As wpfHost_BukuPengawasanPiutangAfiliasi

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG-PIUTANG PIHAK KETIGA
    ' =================================================================
    Public host_BukuPengawasanHutangPihakKetiga As wpfHost_BukuPengawasanHutangPihakKetiga
    Public host_BukuPengawasanPiutangPihakKetiga As wpfHost_BukuPengawasanPiutangPihakKetiga

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG-PIUTANG KARYAWAN
    ' =================================================================
    Public host_BukuPengawasanHutangKaryawan As wpfHost_BukuPengawasanHutangKaryawan
    Public host_BukuPengawasanPiutangKaryawan As wpfHost_BukuPengawasanPiutangKaryawan

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG-PIUTANG PEMEGANG SAHAM
    ' =================================================================
    Public host_BukuPengawasanHutangPemegangSaham As wpfHost_BukuPengawasanHutangPemegangSaham
    Public host_BukuPengawasanPiutangPemegangSaham As wpfHost_BukuPengawasanPiutangPemegangSaham

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG-PIUTANG DIVIDEN
    ' =================================================================
    Public host_BukuPengawasanHutangDividen As wpfHost_BukuPengawasanHutangDividen
    Public host_BukuPengawasanPiutangDividen As wpfHost_BukuPengawasanPiutangDividen

    ' =================================================================
    ' BUKU PENGAWASAN - DEPOSIT OPERASIONAL
    ' =================================================================
    Public host_BukuPengawasanDepositOperasional As wpfHost_BukuPengawasanDepositOperasional

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG BANK-LEASING
    ' =================================================================
    Public host_BukuPengawasanHutangBank As wpfHost_BukuPengawasanHutangBank
    Public host_BukuPengawasanHutangLeasing As wpfHost_BukuPengawasanHutangLeasing

    ' =================================================================
    ' BUKU PENGAWASAN - GAJI
    ' =================================================================
    Public host_BukuPengawasanGaji As wpfHost_BukuPengawasanGaji

    ' =================================================================
    ' BUKU PENGAWASAN - TURUNAN GAJI
    ' =================================================================
    Public host_BukuPengawasanHutangBPJSKesehatan As wpfHost_BukuPengawasanHutangBPJSKesehatan
    Public host_BukuPengawasanHutangBPJSKetenagakerjaan As wpfHost_BukuPengawasanHutangBPJSKetenagakerjaan
    Public host_BukuPengawasanHutangKoperasiKaryawan As wpfHost_BukuPengawasanHutangKoperasiKaryawan
    Public host_BukuPengawasanHutangSerikat As wpfHost_BukuPengawasanHutangSerikat

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG PAJAK
    ' =================================================================
    Public host_BukuPengawasanHutangPPhPasal21 As wpfHost_BukuPengawasanHutangPPhPasal21
    Public host_BukuPengawasanHutangPPhPasal22_Impor As wpfHost_BukuPengawasanHutangPPhPasal22_Impor
    Public host_BukuPengawasanHutangPPhPasal23 As wpfHost_BukuPengawasanHutangPPhPasal23
    Public host_BukuPengawasanHutangPPhPasal42 As wpfHost_BukuPengawasanHutangPPhPasal42
    Public host_BukuPengawasanHutangPPhPasal25 As wpfHost_BukuPengawasanHutangPPhPasal25
    Public host_BukuPengawasanHutangPPhPasal26 As wpfHost_BukuPengawasanHutangPPhPasal26
    Public host_BukuPengawasanPelaporanPPN As wpfHost_BukuPengawasanPelaporanPPN
    Public host_BukuPengawasanKetetapanPajak As wpfHost_BukuPengawasanKetetapanPajak
    Public host_BukuPengawasanPajakImpor As wpfHost_BukuPengawasanPajakImpor

    ' =================================================================
    ' BUKU PENGAWASAN - BUKTI POTONG PPH
    ' =================================================================
    Public host_BukuPengawasanBuktiPotongPPh_Paid As wpfHost_BukuPengawasanBuktiPotongPPh_Paid
    Public host_BukuPengawasanBuktiPotongPPh_Prepaid As wpfHost_BukuPengawasanBuktiPotongPPh_Prepaid

    ' =================================================================
    ' BUKU PENGAWASAN - AKTIVA LAINNYA
    ' =================================================================
    Public host_BukuPengawasanAktivaLainnya As wpfHost_BukuPengawasanAktivaLainnya

    ' =================================================================
    ' BUKU PENGAWASAN - PEMINDAHBUKUAN
    ' =================================================================
    Public host_BukuPengawasanPemindahbukuan As wpfHost_BukuPengawasanPemindahbukuan

    ' =================================================================
    ' BANK GARANSI
    ' =================================================================
    Public host_BukuBankGaransi As wpfHost_BukuBankGaransi

    ' =================================================================
    ' PENERIMAAN-PENGELUARAN BANK CASH
    ' =================================================================
    Public host_BukuPengawasanBuktiPenerimaanBankCash As wpfHost_BukuPengawasanBuktiPenerimaanBankCash
    Public host_BukuPengawasanBuktiPengeluaranBankCash As wpfHost_BukuPengawasanBuktiPengeluaranBankCash

    ' =================================================================
    ' PENJUALAN ECERAN
    ' =================================================================
    Public host_BukuPenjualanEceran As wpfHost_BukuPenjualanEceran

    ' =================================================================
    ' BANK CASH (Bank, Kas, PettyCash, CashAdvance)
    ' =================================================================
    Public host_BukuBank As wpfHost_BukuBank
    Public host_BukuKas As wpfHost_BukuKas
    Public host_BukuPettyCash As wpfHost_BukuPettyCash
    Public host_BukuCashAdvance As wpfHost_BukuCashAdvance

    ' =================================================================
    ' DATA MASTER
    ' =================================================================
    Public host_DaftarPemegangSaham As wpfHost_DaftarPemegangSaham
    Public host_DataCOA As wpfHost_DataCOA
    Public host_DataLawanTransaksi As wpfHost_DataLawanTransaksi
    Public host_DataKaryawan As wpfHost_DataKaryawan
    Public host_DataUser As wpfHost_DataUser
    Public host_DataProject As wpfHost_DataProject
    Public host_DataKurs As wpfHost_DataKurs

    ' =================================================================
    ' JURNAL & ADJUSMENT
    ' =================================================================
    Public host_JurnalUmum As wpfHost_JurnalUmum
    Public host_AdjusmentPenyusutanAsset As wpfHost_AdjusmentPenyusutanAsset
    Public host_AdjusmentAmortisasi As wpfHost_AdjusmentAmortisasi
    Public host_AdjusmentForex As wpfHost_AdjusmentForex
    Public host_AdjusmentHPP As wpfHost_AdjusmentHPP

    ' =================================================================
    ' BUKU BESAR
    ' =================================================================
    Public host_BukuBesar As wpfHost_BukuBesar

    ' =================================================================
    ' LAPORAN
    ' =================================================================
    Public host_LaporanTrialBalance As wpfHost_LaporanTrialBalance
    Public host_LaporanNeracaLajur As wpfHost_LaporanNeracaLajur
    Public host_LaporanHPP As wpfHost_LaporanHPP
    Public host_LaporanNeraca_Bulanan As wpfHost_LaporanNeraca_Bulanan
    Public host_LaporanNeraca_Tahunan As wpfHost_LaporanNeraca_Tahunan
    Public host_LaporanLabaRugi_Bulanan As wpfHost_LaporanLabaRugi_Bulanan
    Public host_LaporanLabaRugi_Tahunan As wpfHost_LaporanLabaRugi_Tahunan

    ' =================================================================
    ' MANAJEMEN ASSET
    ' =================================================================
    Public host_DaftarPenyusutanAssetTetap As wpfHost_DaftarPenyusutanAssetTetap
    Public host_DaftarAmortisasiBiaya As wpfHost_DaftarAmortisasiBiaya
    Public host_BukuDisposalAssetTetap As wpfHost_BukuDisposalAssetTetap

    ' =================================================================
    ' STOCK OPNAME
    ' =================================================================
    Public host_StockOpname_BahanPenolong As wpfHost_StockOpname_BahanPenolong
    Public host_StockOpname_BahanBaku As wpfHost_StockOpname_BahanBaku
    Public host_StockOpname_BarangJadi As wpfHost_StockOpname_BarangJadi
    Public host_StockOpname_BarangDalamProses_CekFisik As wpfHost_StockOpname_BarangDalamProses_CekFisik
    Public host_StockOpname_BarangDalamProses_TarikanData As wpfHost_StockOpname_BarangDalamProses_TarikanData

    ' =================================================================
    ' APP DEVELOPER
    ' =================================================================
    Public host_ManajemenAplikasi As wpfHost_ManajemenAplikasi
    Public host_ManajemenClient As wpfHost_ManajemenClient
    Public host_ManajemenKurs As wpfHost_ManajemenKurs
    Public host_DataProdukApp As wpfHost_DataProdukApp
    Public host_DataPerangkatApp As wpfHost_DataPerangkatApp
    Public host_TabPokok As wpfHost_TabPokok

    ' =================================================================
    ' PEMBELIAN - PO
    ' =================================================================
    Public host_POPembelian_Lokal_Barang As wpfHost_POPembelian_Lokal_Barang
    Public host_POPembelian_Lokal_Jasa As wpfHost_POPembelian_Lokal_Jasa
    Public host_POPembelian_Lokal_BarangDanJasa As wpfHost_POPembelian_Lokal_BarangDanJasa
    Public host_POPembelian_Lokal_JasaKonstruksi As wpfHost_POPembelian_Lokal_JasaKonstruksi
    Public host_POPembelian_Lokal_Semua As wpfHost_POPembelian_Lokal_Semua
    Public host_POPembelian_Impor_Barang As wpfHost_POPembelian_Impor_Barang
    Public host_POPembelian_Impor_Jasa As wpfHost_POPembelian_Impor_Jasa
    Public host_POPembelian_Impor_Semua As wpfHost_POPembelian_Impor_Semua

    ' =================================================================
    ' PEMBELIAN - SURAT JALAN & BAST
    ' =================================================================
    Public host_SuratJalanPembelian As wpfHost_SuratJalanPembelian
    Public host_BASTPembelian As wpfHost_BASTPembelian

    ' =================================================================
    ' PEMBELIAN - INVOICE DENGAN PO
    ' =================================================================
    Public host_InvoicePembelian_DenganPO_Lokal_Rutin As wpfHost_InvoicePembelian_DenganPO_Lokal_Rutin
    Public host_InvoicePembelian_DenganPO_Lokal_Termin As wpfHost_InvoicePembelian_DenganPO_Lokal_Termin
    Public host_InvoicePembelian_DenganPO_Impor_Rutin As wpfHost_InvoicePembelian_DenganPO_Impor_Rutin
    Public host_InvoicePembelian_DenganPO_Impor_Termin As wpfHost_InvoicePembelian_DenganPO_Impor_Termin

    ' =================================================================
    ' PEMBELIAN - INVOICE TANPA PO
    ' =================================================================
    Public host_InvoicePembelian_TanpaPO_Lokal_Barang As wpfHost_InvoicePembelian_TanpaPO_Lokal_Barang
    Public host_InvoicePembelian_TanpaPO_Lokal_Jasa As wpfHost_InvoicePembelian_TanpaPO_Lokal_Jasa
    Public host_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa As wpfHost_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa
    Public host_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi As wpfHost_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi
    Public host_InvoicePembelian_TanpaPO_Impor_Barang As wpfHost_InvoicePembelian_TanpaPO_Impor_Barang
    Public host_InvoicePembelian_TanpaPO_Impor_Jasa As wpfHost_InvoicePembelian_TanpaPO_Impor_Jasa

    ' =================================================================
    ' PEMBELIAN - BUKU & RETUR
    ' =================================================================
    Public host_BukuPembelian_Lokal As wpfHost_BukuPembelian_Lokal
    Public host_BukuPembelian_Impor As wpfHost_BukuPembelian_Impor
    Public host_ReturPembelian As wpfHost_ReturPembelian

    ' =================================================================
    ' PENJUALAN - PO
    ' =================================================================
    Public host_POPenjualan_Lokal_Barang As wpfHost_POPenjualan_Lokal_Barang
    Public host_POPenjualan_Lokal_Jasa As wpfHost_POPenjualan_Lokal_Jasa
    Public host_POPenjualan_Lokal_BarangDanJasa As wpfHost_POPenjualan_Lokal_BarangDanJasa
    Public host_POPenjualan_Lokal_JasaKonstruksi As wpfHost_POPenjualan_Lokal_JasaKonstruksi
    Public host_POPenjualan_Lokal_Semua As wpfHost_POPenjualan_Lokal_Semua
    Public host_POPenjualan_Ekspor As wpfHost_POPenjualan_Ekspor

    ' =================================================================
    ' PENJUALAN - SURAT JALAN & BAST
    ' =================================================================
    Public host_SuratJalanPenjualan As wpfHost_SuratJalanPenjualan
    Public host_BASTPenjualan As wpfHost_BASTPenjualan

    ' =================================================================
    ' PENJUALAN - INVOICE DENGAN PO
    ' =================================================================
    Public host_InvoicePenjualan_DenganPO_Lokal_Rutin As wpfHost_InvoicePenjualan_DenganPO_Lokal_Rutin
    Public host_InvoicePenjualan_DenganPO_Lokal_Termin As wpfHost_InvoicePenjualan_DenganPO_Lokal_Termin
    Public host_InvoicePenjualan_DenganPO_Ekspor_Rutin As wpfHost_InvoicePenjualan_DenganPO_Ekspor_Rutin
    Public host_InvoicePenjualan_DenganPO_Ekspor_Termin As wpfHost_InvoicePenjualan_DenganPO_Ekspor_Termin

    ' =================================================================
    ' PENJUALAN - INVOICE TANPA PO
    ' =================================================================
    Public host_InvoicePenjualan_TanpaPO_Lokal_Barang As wpfHost_InvoicePenjualan_TanpaPO_Lokal_Barang
    Public host_InvoicePenjualan_TanpaPO_Lokal_Jasa As wpfHost_InvoicePenjualan_TanpaPO_Lokal_Jasa
    Public host_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa As wpfHost_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa
    Public host_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi As wpfHost_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi
    Public host_InvoicePenjualan_TanpaPO_Ekspor As wpfHost_InvoicePenjualan_TanpaPO_Ekspor

    ' =================================================================
    ' PENJUALAN - BUKU & RETUR
    ' =================================================================
    Public host_BukuPenjualan_Lokal As wpfHost_BukuPenjualan_Lokal
    Public host_BukuPenjualan_Ekspor As wpfHost_BukuPenjualan_Ekspor
    Public host_BukuPenjualan_Asset As wpfHost_BukuPenjualan_Asset
    Public host_ReturPenjualan As wpfHost_ReturPenjualan

    ' =================================================================
    ' TAHUN BUKU
    ' =================================================================
    Public host_TutupBuku As wpfHost_TutupBuku

End Module
