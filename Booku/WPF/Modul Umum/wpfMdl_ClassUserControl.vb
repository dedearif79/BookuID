Module wpfMdl_ClassUserControl

    '═══════════════════════════════════════════════════════════════════════════
    ' LOADING
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_Loading As New wpfUsc_Loading

    '═══════════════════════════════════════════════════════════════════════════
    ' PENGATURAN
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_CompanyProfile As New wpfUsc_CompanyProfile
    Public usc_PengaturanUmum As New wpfUsc_PengaturanUmum
    Public usc_TutupBuku As New wpfUsc_TutupBuku

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - COA
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DataCOA As New wpfUsc_DataCOA

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - LAWAN TRANSAKSI / MITRA
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DataLawanTransaksi As New wpfUsc_DataLawanTransaksi

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - TOKO
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DataToko As New wpfUsc_DataToko

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - USER
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DataUser As New wpfUsc_DataUser

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - PROJECT
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DataProject As New wpfUsc_DataProject

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - KARYAWAN
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DataKaryawan As New wpfUsc_DataKaryawan

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - KURS
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_Kurs As New wpfUsc_Kurs
    Public usc_ManajemenKurs As New wpfUsc_ManajemenKurs

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - PEMEGANG SAHAM
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DaftarPemegangSaham As New wpfUsc_DaftarPemegangSaham

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU BESAR & BUKU KAS/BANK
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuBesar As New wpfUsc_BukuBesar
    Public usc_BukuKas As New wpfUsc_BukuBesar
    Public usc_BukuBank As New wpfUsc_BukuBesar
    Public usc_BukuPettyCash As New wpfUsc_BukuBesar
    Public usc_BukuCashAdvance As New wpfUsc_BukuBesar

    '═══════════════════════════════════════════════════════════════════════════
    ' LAPORAN
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_LaporanTrialBalance As New wpfUsc_LaporanTrialBalance
    Public usc_LaporanNeracaLajur As New wpfUsc_LaporanNeracaLajur
    Public usc_LaporanHPP As New wpfUsc_LaporanHPP
    Public usc_LaporanNeraca As New wpfUsc_LaporanNeraca
    Public usc_LaporanNeraca_Bulanan As New wpfUsc_LaporanNeraca_Bulanan
    Public usc_LaporanNeraca_Tahunan As New wpfUsc_LaporanNeraca_Tahunan
    Public usc_LaporanLabaRugi As New wpfUsc_LaporanLabaRugi
    Public usc_LaporanLabaRugi_Bulanan As New wpfUsc_LaporanLabaRugi_Bulanan
    Public usc_LaporanLabaRugi_Tahunan As New wpfUsc_LaporanLabaRugi_Tahunan

    '═══════════════════════════════════════════════════════════════════════════
    ' JURNAL & ADJUSMENT
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_JurnalUmum As New wpfUsc_JurnalUmum
    Public usc_JurnalAdjusment As New wpfUsc_JurnalAdjusment
    Public usc_Adjusment_Forex As New wpfUsc_JurnalAdjusment_Forex
    Public usc_JurnalAdjusment_HPP As New wpfUsc_JurnalAdjusment_HPP
    Public usc_Adjusment_PenyusutanAsset As New wpfUsc_Adjusment_PenyusutanAsset
    Public usc_Adjusment_Amortisasi As New wpfUsc_Adjusment_Amortisasi

    '═══════════════════════════════════════════════════════════════════════════
    ' PEMBELIAN - PO
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_POPembelian_Lokal_Barang As New wpfUsc_POPembelian
    Public usc_POPembelian_Lokal_Jasa As New wpfUsc_POPembelian
    Public usc_POPembelian_Lokal_BarangDanJasa As New wpfUsc_POPembelian
    Public usc_POPembelian_Lokal_JasaKonstruksi As New wpfUsc_POPembelian
    Public usc_POPembelian_Lokal_Semua As New wpfUsc_POPembelian
    Public usc_POPembelian_Impor_Barang As New wpfUsc_POPembelian
    Public usc_POPembelian_Impor_Jasa As New wpfUsc_POPembelian
    Public usc_POPembelian_Impor_Semua As New wpfUsc_POPembelian

    '═══════════════════════════════════════════════════════════════════════════
    ' PEMBELIAN - SJ & BAST
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_SuratJalanPembelian As New wpfUsc_SuratJalanPembelian
    Public usc_BASTPembelian As New wpfUsc_BASTPembelian

    '═══════════════════════════════════════════════════════════════════════════
    ' PEMBELIAN - INVOICE
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_InvoicePembelian_DenganPO_Lokal_Rutin As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_DenganPO_Lokal_Termin As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_DenganPO_Impor_Rutin As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_DenganPO_Impor_Termin As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_TanpaPO_Lokal_Barang As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_TanpaPO_Lokal_Jasa As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_TanpaPO_Impor_Barang As New wpfUsc_InvoicePembelian
    Public usc_InvoicePembelian_TanpaPO_Impor_Jasa As New wpfUsc_InvoicePembelian

    '═══════════════════════════════════════════════════════════════════════════
    ' PEMBELIAN - BUKU & RETUR
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPembelian_Lokal As New wpfUsc_BukuPembelian
    Public usc_BukuPembelian_Impor As New wpfUsc_BukuPembelian
    Public usc_ReturPembelian As New wpfUsc_ReturPembelian

    '═══════════════════════════════════════════════════════════════════════════
    ' PENJUALAN - PO
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_POPenjualan_Barang As New wpfUsc_POPenjualan
    Public usc_POPenjualan_Jasa As New wpfUsc_POPenjualan
    Public usc_POPenjualan_BarangDanJasa As New wpfUsc_POPenjualan
    Public usc_POPenjualan_JasaKonstruksi As New wpfUsc_POPenjualan
    Public usc_POPenjualan_Semua As New wpfUsc_POPenjualan
    Public usc_POPenjualan_Ekspor As New wpfUsc_POPenjualan

    '═══════════════════════════════════════════════════════════════════════════
    ' PENJUALAN - SJ & BAST
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_SuratJalanPenjualan As New wpfUsc_SuratJalanPenjualan
    Public usc_BASTPenjualan As New wpfUsc_BASTPenjualan

    '═══════════════════════════════════════════════════════════════════════════
    ' PENJUALAN - INVOICE
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_InvoicePenjualan_Asset As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_DenganPO_Lokal_Rutin As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_DenganPO_Lokal_Termin As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_DenganPO_Ekspor_Rutin As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_DenganPO_Ekspor_Termin As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_TanpaPO_Lokal_Barang As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_TanpaPO_Lokal_Jasa As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi As New wpfUsc_InvoicePenjualan
    Public usc_InvoicePenjualan_TanpaPO_Ekspor As New wpfUsc_InvoicePenjualan

    '═══════════════════════════════════════════════════════════════════════════
    ' PENJUALAN - BUKU & RETUR
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPenjualan_Lokal As New wpfUsc_BukuPenjualan
    Public usc_BukuPenjualan_Ekspor As New wpfUsc_BukuPenjualan
    Public usc_BukuPenjualan_Asset As New wpfUsc_BukuPenjualan
    Public usc_ReturPenjualan As New wpfUsc_ReturPenjualan
    Public usc_BukuPenjualanEceran As New wpfUsc_BukuPenjualanEceran

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - BANK / CASH
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanBuktiPenerimaanBankCash As New wpfUsc_BukuPengawasanBuktiPenerimaanBankCash
    Public usc_BukuPengawasanBuktiPengeluaranBankCash As New wpfUsc_BukuPengawasanBuktiPengeluaranBankCash
    Public usc_BundelPengajuanPengeluaranBankCash As New wpfUsc_BundelPengajuanPengeluaranBankCash
    Public usc_BukuPengawasanPemindahbukuan As New wpfUsc_BukuPengawasanPemindahbukuan

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - HUTANG USAHA
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanHutangUsaha As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Afiliasi As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_NonAfiliasi As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Impor_USD As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Impor_AUD As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Impor_JPY As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Impor_CNY As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Impor_EUR As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Impor_SGD As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Impor_GBP As New wpfUsc_BukuPengawasanHutangUsaha

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - PIUTANG USAHA
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanPiutangUsaha As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_Afiliasi As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_NonAfiliasi As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_Ekspor_USD As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_Ekspor_AUD As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_Ekspor_JPY As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_Ekspor_CNY As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_Ekspor_EUR As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_Ekspor_SGD As New wpfUsc_BukuPengawasanPiutangUsaha
    Public usc_BukuPengawasanPiutangUsaha_Ekspor_GBP As New wpfUsc_BukuPengawasanPiutangUsaha

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - HUTANG PIUTANG AFILIASI
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanHutangAfiliasi As New wpfUsc_BukuPengawasanHutangAfiliasi
    Public usc_BukuPengawasanPiutangAfiliasi As New wpfUsc_BukuPengawasanPiutangAfiliasi

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - HUTANG PIUTANG PIHAK KETIGA
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanHutangPihakKetiga As New wpfUsc_BukuPengawasanHutangPihakKetiga
    Public usc_BukuPengawasanPiutangPihakKetiga As New wpfUsc_BukuPengawasanPiutangPihakKetiga

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - HUTANG PIUTANG KARYAWAN
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanHutangKaryawan As New wpfUsc_BukuPengawasanHutangKaryawan
    Public usc_BukuPengawasanPiutangKaryawan As New wpfUsc_BukuPengawasanPiutangKaryawan

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - HUTANG PIUTANG PEMEGANG SAHAM
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanHutangPemegangSaham As New wpfUsc_BukuPengawasanHutangPemegangSaham
    Public usc_BukuPengawasanPiutangPemegangSaham As New wpfUsc_BukuPengawasanPiutangPemegangSaham

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - HUTANG PIUTANG DIVIDEN
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanHutangDividen As New wpfUsc_BukuPengawasanHutangDividen
    Public usc_BukuPengawasanPiutangDividen As New wpfUsc_BukuPengawasanPiutangDividen

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - HUTANG BANK / LEASING
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanHutangBank As New wpfUsc_BukuPengawasanHutangBankLeasing
    Public usc_BukuPengawasanHutangLeasing As New wpfUsc_BukuPengawasanHutangBankLeasing

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - DEPOSIT OPERASIONAL
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanDepositOperasional As New wpfUsc_BukuPengawasanDepositOperasional

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - AKTIVA LAINNYA
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanAktivaLainnya As New wpfUsc_BukuPengawasanAktivaLainnya

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - BUKTI POTONG PPh
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanBuktiPotongPPh_Prepaid As New wpfUsc_BukuPengawasanBuktiPotongPPh_Prepaid
    Public usc_BukuPengawasanBuktiPotongPPh_Paid As New wpfUsc_BukuPengawasanBuktiPotongPPh_Paid

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - PAJAK (PPh)
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanHutangPPhPasal21 As New wpfUsc_BukuPengawasanHutangPPhPasal21
    Public usc_BukuPengawasanHutangPPhPasal22_Impor As New wpfUsc_BukuPengawasanHutangPPhPasal22_Impor
    Public usc_BukuPengawasanHutangPPhPasal22_Lokal As New wpfUsc_BukuPengawasanHutangPPhPasal22_Lokal
    Public usc_BukuPengawasanHutangPPhPasal23 As New wpfUsc_BukuPengawasanHutangPPhPasal23
    Public usc_BukuPengawasanHutangPPhPasal25 As New wpfUsc_BukuPengawasanHutangPPhPasal25
    Public usc_BukuPengawasanHutangPPhPasal26 As New wpfUsc_BukuPengawasanHutangPPhPasal26
    Public usc_BukuPengawasanHutangPPhPasal42 As New wpfUsc_BukuPengawasanHutangPPhPasal42

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - PAJAK (PPN, KETETAPAN, IMPOR)
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanPelaporanPPN As New wpfUsc_BukuPengawasanPelaporanPPN
    Public usc_BukuPengawasanKetetapanPajak As New wpfUsc_BukuPengawasanKetetapanPajak
    Public usc_BukuPengawasanPajakImpor As New wpfUsc_BukuPengawasanPajakImpor

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - GAJI
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuPengawasanGaji As New wpfUsc_BukuPengawasanGaji
    Public usc_BukuPengawasanHutangBPJSKesehatan As New wpfUsc_BukuPengawasanTurunanGaji
    Public usc_BukuPengawasanHutangBPJSKetenagakerjaan As New wpfUsc_BukuPengawasanTurunanGaji
    Public usc_BukuPengawasanHutangKoperasiKaryawan As New wpfUsc_BukuPengawasanTurunanGaji
    Public usc_BukuPengawasanHutangSerikat As New wpfUsc_BukuPengawasanTurunanGaji

    '═══════════════════════════════════════════════════════════════════════════
    ' MANAJEMEN ASSET - PENYUSUTAN
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DaftarPenyusutanAssetTetap As New wpfUsc_DaftarPenyusutanAssetTetap
    Public usc_BukuDisposalAssetTetap As New wpfUsc_BukuDisposalAssetTetap

    '═══════════════════════════════════════════════════════════════════════════
    ' MANAJEMEN ASSET - AMORTISASI
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DaftarAmortisasiBiaya As New wpfUsc_DaftarAmortisasiBiaya

    '═══════════════════════════════════════════════════════════════════════════
    ' BANK GARANSI
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BukuBankGaransi As New wpfUsc_BukuBankGaransi

    '═══════════════════════════════════════════════════════════════════════════
    ' STOCK OPNAME
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_BahanPenolong As New wpfUsc_StockOpname
    Public usc_BahanBaku As New wpfUsc_StockOpname
    Public usc_BarangJadi As New wpfUsc_StockOpname
    Public usc_BarangDalamProses_CekFisik As New wpfUsc_StockOpname
    Public usc_BarangDalamProses_TarikanData As New wpfUsc_StockOpname

    '═══════════════════════════════════════════════════════════════════════════
    ' DESAIN CETAK - HEADER & FOOTER
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DesainHeader As New wpfUsc_DesainHeader
    Public usc_DesainFooter As New wpfUsc_DesainFooter

    '═══════════════════════════════════════════════════════════════════════════
    ' DESAIN CETAK - NOTA
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DesainPO As New wpfUsc_DesainPO()
    Public usc_DesainSuratJalan As New wpfUsc_DesainSuratJalan()
    Public usc_DesainBAST As New wpfUsc_DesainBAST()
    Public usc_DesainInvoice As New wpfUsc_DesainInvoice()
    Public usc_DesainNotaRetur As New wpfUsc_DesainNotaRetur()
    Public usc_DesainNotaDebet As New wpfUsc_DesainNotaDebet

    '═══════════════════════════════════════════════════════════════════════════
    ' DESAIN CETAK - BUKTI & JURNAL
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_DesainBuktiPengeluaran As New wpfUsc_DesainBuktiPengeluaran
    Public usc_DesainBundelanPengajuanPengeluaran As New wpfUsc_DesainBundelanPengajuanPengeluaran
    Public usc_DesainJurnalVoucher As New wpfUsc_DesainJurnalVoucher

    '═══════════════════════════════════════════════════════════════════════════
    ' APP DEVELOPER
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_ManajemenClient As New wpfUsc_ManajemenClient
    Public usc_ManajemenAplikasi As New wpfUsc_ManajemenAplikasi
    Public usc_DataProdukApp As New wpfUsc_DataProdukApp
    Public usc_DataPerangkatApp As New wpfUsc_DataPerangkatApp
    Public usc_TabPokok As New wpfUsc_TabPokok

    '═══════════════════════════════════════════════════════════════════════════
    ' WEB BROWSER
    '═══════════════════════════════════════════════════════════════════════════
    Public usc_phpMyAdmin As New wpfUsc_WebBrowser
    Public usc_WhatsApp As New wpfUsc_WebBrowser
    Public usc_Telegram As New wpfUsc_WebBrowser
    Public usc_ChatGPT As New wpfUsc_WebBrowser
    Public usc_Gmail As New wpfUsc_WebBrowser
    Public usc_YouTube As New wpfUsc_WebBrowser

End Module
