Imports bcomm
Imports System.Data.Odbc

Module mdl_TautanCOA

    Public JumlahDigitCOA = 5

    'Variabel Kode dan Nama Tautan COA :

    'Kepala 1 :
    Public KodeTautanCOA_PettyCashAdministrasi
    Public NamaTautanCOA_PettyCashAdministrasi = "PETTY CASH - ADMINISTRASI"
    Public KodeTautanCOA_PettyCashProduksi
    Public NamaTautanCOA_PettyCashProduksi = "PETTY CASH - PRODUKSI"
    Public KodeTautanCOA_Kas
    Public NamaTautanCOA_Kas = "KAS"
    Public KodeTautanCOA_Kas_USD
    Public NamaTautanCOA_Kas_USD = "KAS (USD)"
    Public KodeTautanCOA_Kas_AUD
    Public NamaTautanCOA_Kas_AUD = "KAS (AUD)"
    Public KodeTautanCOA_Kas_JPY
    Public NamaTautanCOA_Kas_JPY = "KAS (JPY)"
    Public KodeTautanCOA_Kas_CNY
    Public NamaTautanCOA_Kas_CNY = "KAS (CNY)"
    Public KodeTautanCOA_Kas_EUR
    Public NamaTautanCOA_Kas_EUR = "KAS (EUR)"
    Public KodeTautanCOA_Kas_SGD
    Public NamaTautanCOA_Kas_SGD = "KAS (SGD)"
    Public KodeTautanCOA_Kas_GBP
    Public NamaTautanCOA_Kas_GBP = "KAS (GBP)"
    Public KodeTautanCOA_CashAdvance
    Public NamaTautanCOA_CashAdvance = "CASH ADVANCE"

    Public KodeTautanCOA_PiutangUsaha_NonAfiliasi
    Public NamaTautanCOA_PiutangUsaha_NonAfiliasi = "PIUTANG USAHA - NON AFILIASI"
    Public KodeTautanCOA_PiutangUsaha_Afiliasi
    Public NamaTautanCOA_PiutangUsaha_Afiliasi = "PIUTANG USAHA - AFILIASI"
    Public KodeTautanCOA_PiutangUsaha_USD
    Public NamaTautanCOA_PiutangUsaha_USD = "PIUTANG USAHA - USD"
    Public KodeTautanCOA_PiutangUsaha_AUD
    Public NamaTautanCOA_PiutangUsaha_AUD = "PIUTANG USAHA - AUD"
    Public KodeTautanCOA_PiutangUsaha_JPY
    Public NamaTautanCOA_PiutangUsaha_JPY = "PIUTANG USAHA - JPY"
    Public KodeTautanCOA_PiutangUsaha_CNY
    Public NamaTautanCOA_PiutangUsaha_CNY = "PIUTANG USAHA - CNY"
    Public KodeTautanCOA_PiutangUsaha_EUR
    Public NamaTautanCOA_PiutangUsaha_EUR = "PIUTANG USAHA - EUR"
    Public KodeTautanCOA_PiutangUsaha_SGD
    Public NamaTautanCOA_PiutangUsaha_SGD = "PIUTANG USAHA - SGD"
    Public KodeTautanCOA_PiutangUsaha_GBP
    Public NamaTautanCOA_PiutangUsaha_GBP = "PIUTANG USAHA - GBP"
    Public KodeTautanCOA_PiutangAfiliasi
    Public NamaTautanCOA_PiutangAfiliasi = "PIUTANG AFILIASI"
    Public KodeTautanCOA_PiutangPihakKetiga
    Public NamaTautanCOA_PiutangPihakKetiga = "PIUTANG PIHAK KETIGA"
    Public KodeTautanCOA_PiutangPemegangSaham
    Public NamaTautanCOA_PiutangPemegangSaham = "PIUTANG PEMEGANG SAHAM"
    Public KodeTautanCOA_PiutangDividen
    Public NamaTautanCOA_PiutangDividen = "PIUTANG DIVIDEN"
    Public KodeTautanCOA_PiutangKaryawan
    Public NamaTautanCOA_PiutangKaryawan = "PIUTANG KARYAWAN"
    Public KodeTautanCOA_PiutangLainnya
    Public NamaTautanCOA_PiutangLainnya = "PIUTANG LAINNYA"

    Public KodeTautanCOA_SewaTanahDanAtauBangunanDibayarDimuka
    Public NamaTautanCOA_SewaTanahDanAtauBangunanDibayarDimuka = "SEWA TANAH DAN/ATAU BANGUNAN DIBAYAR DIMUKA"
    Public KodeTautanCOA_SewaMesinDanPeralatanDibayarDimuka
    Public NamaTautanCOA_SewaMesinDanPeralatanDibayarDimuka = "SEWA MESIN DAN PERALATAN DIBAYAR DIMUKA"
    Public KodeTautanCOA_SewaKendaraanDibayarDimuka
    Public NamaTautanCOA_SewaKendaraanDibayarDimuka = "SEWA KENDARAAN DIBAYAR DIMUKA"
    Public KodeTautanCOA_BiayaRenovasiDibayarDimuka
    Public NamaTautanCOA_BiayaRenovasiDibayarDimuka = "BIAYA RENOVASI DIBAYAR DIMUKA"
    Public KodeTautanCOA_BiayaPendirianPerusahaan
    Public NamaTautanCOA_BiayaPendirianPerusahaan = "BIAYA PENDIRIAN PERUSAHAAN"
    Public KodeTautanCOA_AsuransiDibayarDimuka
    Public NamaTautanCOA_AsuransiDibayarDimuka = "ASURANSI DIBAYAR DIMUKA"
    Public KodeTautanCOA_SewaAssetLainnyaDibayarDimuka
    Public NamaTautanCOA_SewaAssetLainnyaDibayarDimuka = "SEWA ASSET LAINNYA DIBAYAR DIMUKA"
    Public KodeTautanCOA_DepositOperasional
    Public NamaTautanCOA_DepositOperasional = "DEPOSIT OPERASIONAL"
    Public KodeTautanCOA_BankGaransi
    Public NamaTautanCOA_BankGaransi = "BANK GARANSI"
    Public KodeTautanCOA_DepositOperasionalEkspor
    Public NamaTautanCOA_DepositOperasionalEkspor = "DEPOSIT OPERASIONAL EKSPOR"
    Public KodeTautanCOA_UangMukaPembelian
    Public NamaTautanCOA_UangMukaPembelian = "UANG MUKA PEMBELIAN"
    Public KodeTautanCOA_UangMukaPembelian_Impor_USD
    Public NamaTautanCOA_UangMukaPembelian_Impor_USD = "UANG MUKA PEMBELIAN - IMPOR - USD"
    Public KodeTautanCOA_UangMukaPembelian_Impor_AUD
    Public NamaTautanCOA_UangMukaPembelian_Impor_AUD = "UANG MUKA PEMBELIAN - IMPOR - AUD"
    Public KodeTautanCOA_UangMukaPembelian_Impor_JPY
    Public NamaTautanCOA_UangMukaPembelian_Impor_JPY = "UANG MUKA PEMBELIAN - IMPOR - JPY"
    Public KodeTautanCOA_UangMukaPembelian_Impor_CNY
    Public NamaTautanCOA_UangMukaPembelian_Impor_CNY = "UANG MUKA PEMBELIAN - IMPOR - CNY"
    Public KodeTautanCOA_UangMukaPembelian_Impor_EUR
    Public NamaTautanCOA_UangMukaPembelian_Impor_EUR = "UANG MUKA PEMBELIAN - IMPOR - EUR"
    Public KodeTautanCOA_UangMukaPembelian_Impor_SGD
    Public NamaTautanCOA_UangMukaPembelian_Impor_SGD = "UANG MUKA PEMBELIAN - IMPOR - SGD"
    Public KodeTautanCOA_UangMukaPembelian_Impor_GBP
    Public NamaTautanCOA_UangMukaPembelian_Impor_GBP = "UANG MUKA PEMBELIAN - IMPOR - GBP"
    Public KodeTautanCOA_BiayaDibayarDimuka
    Public NamaTautanCOA_BiayaDibayarDimuka = "BIAYA DIBAYAR DIMUKA"
    Public KodeTautanCOA_BiayaDibayarDimuka_USD
    Public NamaTautanCOA_BiayaDibayarDimuka_USD = "BIAYA DIBAYAR DIMUKA - USD"
    Public KodeTautanCOA_BiayaDibayarDimuka_AUD
    Public NamaTautanCOA_BiayaDibayarDimuka_AUD = "BIAYA DIBAYAR DIMUKA - AUD"
    Public KodeTautanCOA_BiayaDibayarDimuka_JPY
    Public NamaTautanCOA_BiayaDibayarDimuka_JPY = "BIAYA DIBAYAR DIMUKA - JPY"
    Public KodeTautanCOA_BiayaDibayarDimuka_CNY
    Public NamaTautanCOA_BiayaDibayarDimuka_CNY = "BIAYA DIBAYAR DIMUKA - CNY"
    Public KodeTautanCOA_BiayaDibayarDimuka_EUR
    Public NamaTautanCOA_BiayaDibayarDimuka_EUR = "BIAYA DIBAYAR DIMUKA - EUR"
    Public KodeTautanCOA_BiayaDibayarDimuka_SGD
    Public NamaTautanCOA_BiayaDibayarDimuka_SGD = "BIAYA DIBAYAR DIMUKA - SGD"
    Public KodeTautanCOA_BiayaDibayarDimuka_GBP
    Public NamaTautanCOA_BiayaDibayarDimuka_GBP = "BIAYA DIBAYAR DIMUKA - GBP"


    Public KodeTautanCOA_PersediaanBahanPenolong
    Public NamaTautanCOA_PersediaanBahanPenolong = "PERSEDIAAN BAHAN PENOLONG"
    Public KodeTautanCOA_PersediaanBahanBaku_Lokal
    Public NamaTautanCOA_PersediaanBahanBaku_Lokal = "PERSEDIAAN BAHAN BAKU - LOKAL"
    Public KodeTautanCOA_PersediaanBarangDalamProses
    Public NamaTautanCOA_PersediaanBarangDalamProses = "PERSEDIAAN BARANG DALAM PROSES"
    Public KodeTautanCOA_PersediaanBarangJadi
    Public NamaTautanCOA_PersediaanBarangJadi = "PERSEDIAAN BARANG JADI"
    Public KodeTautanCOA_PersediaanBahanBaku_Import
    Public NamaTautanCOA_PersediaanBahanBaku_Import = "PERSEDIAAN BAHAN BAKU - IMPORT"

    Public KodeTautanCOA_PPhPasal21DibayarDimuka
    Public NamaTautanCOA_PPhPasal21DibayarDimuka = "PPH PASAL 21 DIBAYAR DIMUKA"
    Public KodeTautanCOA_PPhPasal22DibayarDimuka_Lokal
    Public NamaTautanCOA_PPhPasal22DibayarDimuka_Lokal = "PPH PASAL 21 DIBAYAR DIMUKA - LOKAL"
    Public KodeTautanCOA_PPhPasal22DibayarDimuka_Impor
    Public NamaTautanCOA_PPhPasal22DibayarDimuka_Impor = "PPH PASAL 21 DIBAYAR DIMUKA - IMPOR"
    Public KodeTautanCOA_PPhPasal23DibayarDimuka
    Public NamaTautanCOA_PPhPasal23DibayarDimuka = "PPH PASAL 23 DIBAYAR DIMUKA"
    Public KodeTautanCOA_PPhPasal42DibayarDimuka
    Public NamaTautanCOA_PPhPasal42DibayarDimuka = "PPH PASAL 4 (2) DIBAYAR DIMUKA"

    Public KodeTautanCOA_PPhPasal25DibayarDimuka
    Public NamaTautanCOA_PPhPasal25DibayarDimuka = "PPH PASAL 25 DIBAYAR DIMUKA"
    Public KodeTautanCOA_PPNMasukan_Lokal
    Public NamaTautanCOA_PPNMasukan_Lokal = "PPN MASUKAN - LOKAL"
    Public KodeTautanCOA_PPNMasukan_Impor
    Public NamaTautanCOA_PPNMasukan_Impor = "PPN MASUKAN - IMPORT"
    Public KodeTautanCOA_PPhPasal23DibayarDimuka_BPBelumDiterima
    Public NamaTautanCOA_PPhPasal23DibayarDimuka_BPBelumDiterima = "PPH PASAL 23 DIBAYAR DIMUKA - BP BELUM DITERIMA"
    Public KodeTautanCOA_PPhPasal42DibayarDimuka_BPBelumDiterima
    Public NamaTautanCOA_PPhPasal42DibayarDimuka_BPBelumDiterima = "PPH PASAL 4 (2) DIBAYAR DIMUKA - BP BELUM DITERIMA"

    Public KodeTautanCOA_BankEceran1
    Public NamaTautanCOA_BankEceran1 = "BANK ECERAN 1"
    Public KodeTautanCOA_BankEceran2
    Public NamaTautanCOA_BankEceran2 = "BANK ECERAN 2"
    Public KodeTautanCOA_BankEceran3
    Public NamaTautanCOA_BankEceran3 = "BANK ECERAN 3"

    Public KodeTautanCOA_Bangunan
    Public NamaTautanCOA_Bangunan = "BANGUNAN"

    Public KodeTautanCOA_InvestasiDeposito
    Public NamaTautanCOA_InvestasiDeposito = "INVESTASI DEPOSITO"
    Public KodeTautanCOA_InvestasiSuratBerharga
    Public NamaTautanCOA_InvestasiSuratBerharga = "INVESTASI SURAT BERHARGA"
    Public KodeTautanCOA_InvestasiLogamMulia
    Public NamaTautanCOA_InvestasiLogamMulia = "INVESTASI LOGAM MULIA"
    Public KodeTautanCOA_InvestasiPadaPerusahaanAnak
    Public NamaTautanCOA_InvestasiPadaPerusahaanAnak = "INVESTASI PADA PERUSAHAAN ANAK"
    Public KodeTautanCOA_InvestasiGoodWill
    Public NamaTautanCOA_InvestasiGoodWill = "INVESTASI GOODWILL"

    'Kepala 2 :
    Public KodeTautanCOA_HutangUsaha_USD
    Public NamaTautanCOA_HutangUsaha_USD = "HUTANG USAHA - USD"
    Public KodeTautanCOA_HutangUsaha_AUD
    Public NamaTautanCOA_HutangUsaha_AUD = "HUTANG USAHA - AUD"
    Public KodeTautanCOA_HutangUsaha_JPY
    Public NamaTautanCOA_HutangUsaha_JPY = "HUTANG USAHA - JPY"
    Public KodeTautanCOA_HutangUsaha_CNY
    Public NamaTautanCOA_HutangUsaha_CNY = "HUTANG USAHA - CNY"
    Public KodeTautanCOA_HutangUsaha_EUR
    Public NamaTautanCOA_HutangUsaha_EUR = "HUTANG USAHA - EUR"
    Public KodeTautanCOA_HutangUsaha_SGD
    Public NamaTautanCOA_HutangUsaha_SGD = "HUTANG USAHA - SGD"
    Public KodeTautanCOA_HutangUsaha_GBP
    Public NamaTautanCOA_HutangUsaha_GBP = "HUTANG USAHA - GBP"
    Public KodeTautanCOA_HutangUsaha_NonAfiliasi
    Public NamaTautanCOA_HutangUsaha_NonAfiliasi = "HUTANG USAHA - NON AFILIASI"
    Public KodeTautanCOA_HutangUsaha_Afiliasi
    Public NamaTautanCOA_HutangUsaha_Afiliasi = "HUTANG USAHA - AFILIASI"
    Public KodeTautanCOA_HutangDeposit
    Public NamaTautanCOA_HutangDeposit = "HUTANG DEPOSIT"
    Public KodeTautanCOA_HutangBiaya
    Public NamaTautanCOA_HutangBiaya = "HUTANG BIAYA"
    Public KodeTautanCOA_HutangKetetapanPajak
    Public NamaTautanCOA_HutangKetetapanPajak = "HUTANG KETETAPAN PAJAK"

    Public KodeTautanCOA_HutangBpjsKesehatan
    Public NamaTautanCOA_HutangBpjsKesehatan = "HUTANG BPJS KESEHATAN"
    Public KodeTautanCOA_HutangBpjsKetenagakerjaan
    Public NamaTautanCOA_HutangBpjsKetenagakerjaan = "HUTANG BPJS KETENAGAKERJAAN"
    Public KodeTautanCOA_HutangGaji
    Public NamaTautanCOA_HutangGaji = "HUTANG GAJI"
    Public KodeTautanCOA_UangMukaPenjualan
    Public NamaTautanCOA_UangMukaPenjualan = "UANG MUKA PENJUALAN"
    Public KodeTautanCOA_UangMukaPenjualan_Ekspor_USD
    Public NamaTautanCOA_UangMukaPenjualan_Ekspor_USD = "UANG MUKA PENJUALAN - EKSPOR - USD"
    Public KodeTautanCOA_UangMukaPenjualan_Ekspor_AUD
    Public NamaTautanCOA_UangMukaPenjualan_Ekspor_AUD = "UANG MUKA PENJUALAN - EKSPOR - AUD"
    Public KodeTautanCOA_UangMukaPenjualan_Ekspor_JPY
    Public NamaTautanCOA_UangMukaPenjualan_Ekspor_JPY = "UANG MUKA PENJUALAN - EKSPOR - JPY"
    Public KodeTautanCOA_UangMukaPenjualan_Ekspor_CNY
    Public NamaTautanCOA_UangMukaPenjualan_Ekspor_CNY = "UANG MUKA PENJUALAN - EKSPOR - CNY"
    Public KodeTautanCOA_UangMukaPenjualan_Ekspor_EUR
    Public NamaTautanCOA_UangMukaPenjualan_Ekspor_EUR = "UANG MUKA PENJUALAN - EKSPOR - EUR"
    Public KodeTautanCOA_UangMukaPenjualan_Ekspor_SGD
    Public NamaTautanCOA_UangMukaPenjualan_Ekspor_SGD = "UANG MUKA PENJUALAN - EKSPOR - SGD"
    Public KodeTautanCOA_UangMukaPenjualan_Ekspor_GBP
    Public NamaTautanCOA_UangMukaPenjualan_Ekspor_GBP = "UANG MUKA PENJUALAN - EKSPOR - GBP"
    Public KodeTautanCOA_HutangPPhPasal21
    Public NamaTautanCOA_HutangPPhPasal21 = "HUTANG PPH PASAL 21"
    Public KodeTautanCOA_HutangPPhPasal22
    Public NamaTautanCOA_HutangPPhPasal22 = "HUTANG PPH PASAL 22"
    Public KodeTautanCOA_HutangPPhPasal23
    Public NamaTautanCOA_HutangPPhPasal23 = "HUTANG PPH PASAL 23"
    Public KodeTautanCOA_HutangPPhPasal42
    Public NamaTautanCOA_HutangPPhPasal42 = "HUTANG PPH PASAL 4 (2)"
    Public KodeTautanCOA_HutangPPhPasal25
    Public NamaTautanCOA_HutangPPhPasal25 = "HUTANG PPH PASAL 25"
    Public KodeTautanCOA_HutangPPhPasal26
    Public NamaTautanCOA_HutangPPhPasal26 = "HUTANG PPH PASAL 26"
    Public KodeTautanCOA_PPNKeluaran
    Public NamaTautanCOA_PPNKeluaran = "PPN KELUARAN"
    Public KodeTautanCOA_HutangPPN
    Public NamaTautanCOA_HutangPPN = "HUTANG PPN"
    Public KodeTautanCOA_HutangPPhPasal29
    Public NamaTautanCOA_HutangPPhPasal29 = "HUTANG PPH PASAL 29"
    Public KodeTautanCOA_HutangPPhPasal21_100
    Public NamaTautanCOA_HutangPPhPasal21_100 = "HUTANG PPH PASAL 21 - 100"
    Public KodeTautanCOA_HutangPPhPasal21_401
    Public NamaTautanCOA_HutangPPhPasal21_401 = "HUTANG PPH PASAL 21 - 401"
    Public KodeTautanCOA_HutangPPhPasal23_100
    Public NamaTautanCOA_HutangPPhPasal23_100 = "HUTANG PPH PASAL 23 - 100"
    Public KodeTautanCOA_HutangPPhPasal23_101
    Public NamaTautanCOA_HutangPPhPasal23_101 = "HUTANG PPH PASAL 23 - 101"
    Public KodeTautanCOA_HutangPPhPasal23_102
    Public NamaTautanCOA_HutangPPhPasal23_102 = "HUTANG PPH PASAL 23 - 102"
    Public KodeTautanCOA_HutangPPhPasal23_103
    Public NamaTautanCOA_HutangPPhPasal23_103 = "HUTANG PPH PASAL 23 - 103"
    Public KodeTautanCOA_HutangPPhPasal23_104
    Public NamaTautanCOA_HutangPPhPasal23_104 = "HUTANG PPH PASAL 23 - 104"
    Public KodeTautanCOA_HutangPPhPasal42_402
    Public NamaTautanCOA_HutangPPhPasal42_402 = "HUTANG PPH PASAL 4 (2) - 402"
    Public KodeTautanCOA_HutangPPhPasal42_403
    Public NamaTautanCOA_HutangPPhPasal42_403 = "HUTANG PPH PASAL 4 (2) - 403"
    Public KodeTautanCOA_HutangPPhPasal42_409
    Public NamaTautanCOA_HutangPPhPasal42_409 = "HUTANG PPH PASAL 4 (2) - 409"
    Public KodeTautanCOA_HutangPPhPasal42_419
    Public NamaTautanCOA_HutangPPhPasal42_419 = "HUTANG PPH PASAL 4 (2) - 419"
    Public KodeTautanCOA_HutangPPhPasal26_100
    Public NamaTautanCOA_HutangPPhPasal26_100 = "HUTANG PPH PASAL 26 - 100"
    Public KodeTautanCOA_HutangPPhPasal26_101
    Public NamaTautanCOA_HutangPPhPasal26_101 = "HUTANG PPH PASAL 26 - 101"
    Public KodeTautanCOA_HutangPPhPasal26_102
    Public NamaTautanCOA_HutangPPhPasal26_102 = "HUTANG PPH PASAL 26 - 102"
    Public KodeTautanCOA_HutangPPhPasal26_103
    Public NamaTautanCOA_HutangPPhPasal26_103 = "HUTANG PPH PASAL 26 - 103"
    Public KodeTautanCOA_HutangPPhPasal26_104
    Public NamaTautanCOA_HutangPPhPasal26_104 = "HUTANG PPH PASAL 26 - 104"
    Public KodeTautanCOA_HutangPPhPasal26_105
    Public NamaTautanCOA_HutangPPhPasal26_105 = "HUTANG PPH PASAL 26 - 105"

    Public KodeTautanCOA_HutangPPN_100
    Public NamaTautanCOA_HutangPPN_100 = "HUTANG PPN - 100"
    Public KodeTautanCOA_HutangPPN_101
    Public NamaTautanCOA_HutangPPN_101 = "HUTANG PPN - 101"
    Public KodeTautanCOA_HutangPPN_102
    Public NamaTautanCOA_HutangPPN_102 = "HUTANG PPN - 102"
    Public KodeTautanCOA_HutangPPN_103
    Public NamaTautanCOA_HutangPPN_103 = "HUTANG PPN - 103"
    Public KodeTautanCOA_HutangPPN_Impor
    Public NamaTautanCOA_HutangPPN_Impor = "HUTANG PPN - IMPOR"

    Public KodeTautanCOA_HutangKoperasiKaryawan
    Public NamaTautanCOA_HutangKoperasiKaryawan = "HUTANG KOPERASI KARYAWAN"
    Public KodeTautanCOA_HutangSerikat
    Public NamaTautanCOA_HutangSerikat = "HUTANG SERIKAT"
    Public KodeTautanCOA_HutangPihakKetiga
    Public NamaTautanCOA_HutangPihakKetiga = "HUTANG PIHAK KETIGA"
    Public KodeTautanCOA_HutangKaryawan
    Public NamaTautanCOA_HutangKaryawan = "HUTANG KARYAWAN"
    Public KodeTautanCOA_HutangLancarLainnya
    Public NamaTautanCOA_HutangLancarLainnya = "HUTANG LANCAR LAINNYA"

    Public KodeTautanCOA_HutangLeasing
    Public NamaTautanCOA_HutangLeasing = "HUTANG LEASING"
    Public KodeTautanCOA_HutangBank
    Public NamaTautanCOA_HutangBank = "HUTANG BANK"
    Public KodeTautanCOA_HutangPemegangSaham
    Public NamaTautanCOA_HutangPemegangSaham = "HUTANG PEMEGANG SAHAM"
    Public KodeTautanCOA_HutangAfiliasi
    Public NamaTautanCOA_HutangAfiliasi = "HUTANG AFILIASI"

    Public KodeTautanCOA_HutangDividen
    Public NamaTautanCOA_HutangDividen = "HUTANG DIVIDEN"

    'Kepala 3 :
    Public KodeTautanCOA_Modal
    Public NamaTautanCOA_Modal = "HUTANG MODAL"
    Public KodeTautanCOA_LabaDitahan
    Public NamaTautanCOA_LabaDitahan = "LABA DITAHAN"
    Public KodeTautanCOA_LabaTahunBerjalan
    Public NamaTautanCOA_LabaTahunBerjalan = "LABA TAHUN BERJALAN"

    'Kepala 4 :
    Public KodeTautanCOA_PenjualanBarang_Trading
    Public NamaTautanCOA_PenjualanBarang_Trading = "PENJUALAN BARANG - TRADING"
    Public KodeTautanCOA_PenjualanJasa
    Public NamaTautanCOA_PenjualanJasa = "PENJUALAN JASA"
    Public KodeTautanCOA_PenjualanJasaKonstruksi
    Public NamaTautanCOA_PenjualanJasaKonstruksi = "PENJUALAN JASA KONSTRUKSI"
    Public KodeTautanCOA_PenjualanEkspor
    Public NamaTautanCOA_PenjualanEkspor = "PENJUALAN EKSPOR"
    Public KodeTautanCOA_PenjualanEceran
    Public NamaTautanCOA_PenjualanEceran = "PENJUALAN ECERAN"
    Public KodeTautanCOA_PenjualanBarang_Manufaktur
    Public NamaTautanCOA_PenjualanBarang_Manufaktur = "PENJUALAN BARANG - MANUFAKTUR"
    Public KodeTautanCOA_PenjualanAssetTanahBangunan
    Public NamaTautanCOA_PenjualanAssetTanahBangunan = "PENJUALAN ASSET TANAH DAN/ATAU BANGUNAN"
    Public KodeTautanCOA_PenjualanAssetLainnya
    Public NamaTautanCOA_PenjualanAssetLainnya = "PENJUALAN ASSET LAINNYA"
    Public KodeTautanCOA_ReturPenjualan
    Public NamaTautanCOA_ReturPenjualan = "RETUR PENJUALAN"

    'Kepala 5:
    Public KodeTautanCOA_ReturPembelianBahanBaku_Lokal
    Public NamaTautanCOA_ReturPembelianBahanBakuLokal = "RETUR PEMBELIAN BAHAN BAKU - LOKAL"
    Public KodeTautanCOA_BiayaTransportasiPembelianBb_Lokal
    Public NamaTautanCOA_BiayaTransportasiPembelianBb_Lokal = "BIAYA TRANSPORTASI PEMBELIAN BB - LOKAL"
    Public KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal
    Public NamaTautanCOA_BiayaPemakaianBahanBaku_Lokal = "BIAYA PEMAKAIAN BAHAN BAKU - LOKAL"
    Public KodeTautanCOA_ReturPembelianBahanBaku_Import
    Public NamaTautanCOA_ReturPembelianBahanBaku_Import = "RETUR PEMBELIAN BAHAN BAKU - IMPORT"
    Public KodeTautanCOA_BiayaAsuransiImport
    Public NamaTautanCOA_BiayaAsuransiImport = "BIAYA ASURANSI - IMPORT"
    Public KodeTautanCOA_BiayaPengapalan
    Public NamaTautanCOA_BiayaPengapalan = "BIAYA PENGAPALAN"
    Public KodeTautanCOA_BeaMasuk_Impor
    Public NamaTautanCOA_BeaMasuk_Impor = "BEA MASUK IMPOR"
    Public KodeTautanCOA_BiayaTransportasi_Import
    Public NamaTautanCOA_BiayaTransportasi_Import = "BIAYA TRANSPORTASI - IMPORT"
    Public KodeTautanCOA_BiayaPemakaianBahanBaku_Import
    Public NamaTautanCOA_BiayaPemakaianBahanBaku_Import = "BIAYA PEMAKAIAN BAHAN BAKU - IMPORT"

    Public KodeTautanCOA_BiayaBahanBaku
    Public NamaTautanCOA_BiayaBahanBaku = "BIAYA BAHAN BAKU"

    Public KodeTautanCOA_BiayaGajiProduksi
    Public NamaTautanCOA_BiayaGajiProduksi = "BIAYA GAJI PRODUKSI"
    Public KodeTautanCOA_BiayaGajiProduksi2
    Public NamaTautanCOA_BiayaGajiProduksi2 = "BIAYA GAJI PRODUKSI 2"
    Public KodeTautanCOA_BiayaGajiProduksi3
    Public NamaTautanCOA_BiayaGajiProduksi3 = "BIAYA GAJI PRODUKSI 3"
    Public KodeTautanCOA_BiayaGajiProduksi4
    Public NamaTautanCOA_BiayaGajiProduksi4 = "BIAYA GAJI PRODUKSI 4"
    Public KodeTautanCOA_BiayaThrBonusProduksi
    Public NamaTautanCOA_BiayaThrBonusProduksi = "BIAYA THR/BONUS PRODUKSI"
    Public KodeTautanCOA_BiayaTunjanganPPh21Produksi
    Public NamaTautanCOA_BiayaTunjanganPPh21Produksi = "BIAYA TUNJANGAN PPH 21 PRODUKSI"
    Public KodeTautanCOA_BiayaBpjsTkJkkJkmProduksi
    Public NamaTautanCOA_BiayaBpjsTkJkkJkmProduksi = "BIAYA BPJS TK-JKK/JKM - PRODUKSI"
    Public KodeTautanCOA_BiayaBpjsTkJhtIpProduksi
    Public NamaTautanCOA_BiayaBpjsTkJhtIpProduksi = "BIAYA BPJS TK-JHT/IP - PRODUKSI"
    Public KodeTautanCOA_BiayaBpjsKesehatanProduksi
    Public NamaTautanCOA_BiayaBpjsKesehatanProduksi = "BIAYA BPJS KESEHATAN - PRODUKSI"
    Public KodeTautanCOA_BiayaAsuransiKaryawanProduksi
    Public NamaTautanCOA_BiayaAsuransiKaryawanProduksi = "BIAYA ASURANSI KARYAWAN - PRODUKSI"
    Public KodeTautanCOA_BiayaPesangonKaryawanProduksi
    Public NamaTautanCOA_BiayaPesangonKaryawanProduksi = "BIAYA PESANGON KARYAWAN - PRODUKSI"
    Public KodeTautanCOA_BiayaTenagaKerjaLangsung
    Public NamaTautanCOA_BiayaTenagaKerjaLangsung = "BIAYA TENAGA KERJA LANGSUNG"

    Public KodeTautanCOA_ReturPembelianBahanPenolong
    Public NamaTautanCOA_ReturPembelianBahanPenolong = "RETUR PEMBELIAN BAHAN PENOLONG"
    Public KodeTautanCOA_BiayaPengirimanDanEkspedisiProduksi
    Public NamaTautanCOA_BiayaPengirimanDanEkspedisiProduksi = "BIAYA PENGIRIMAN DAN EKSPEDISI PRODUKSI"
    Public KodeTautanCOA_BiayaPemakaianBahanPenolong
    Public NamaTautanCOA_BiayaPemakaianBahanPenolong = "BIAYA PEMAKAIAN BAHAN PENOLONG"
    Public KodeTautanCOA_BiayaOverheadPabrik
    Public NamaTautanCOA_BiayaOverheadPabrik = "BIAYA OVERHEAD PABRIK"
    Public KodeTautanCOA_ReturPembelianLainnya
    Public NamaTautanCOA_ReturPembelianLainnya = "RETUR PEMBELIAN LAINNYA"
    Public KodeTautanCOA_BiayaProduksi
    Public NamaTautanCOA_BiayaProduksi = "BIAYA PRODUKSI"
    Public KodeTautanCOA_HargaPokokProduksi
    Public NamaTautanCOA_HargaPokokProduksi = "HARGA POKOK PRODUKSI"
    Public KodeTautanCOA_HargaPokokPenjualan
    Public NamaTautanCOA_HargaPokokPenjualan = "HARGA POKOK PENJUALAN"
    Public KodeTautanCOA_PembelianBahanBaku_Lokal
    Public NamaTautanCOA_PembelianBahanBaku_Lokal = "PEMBELIAN BAHAN BAKU LOKAL"
    Public KodeTautanCOA_PembelianBahanBaku_Import
    Public NamaTautanCOA_PembelianBahanBaku_Import = "PEMBELIAN BAHAN BAKU IMPORT"
    Public KodeTautanCOA_PembelianBahanPenolong
    Public NamaTautanCOA_PembelianBahanPenolong = "PEMBELIAN BAHAN PENOLONG"

    'Kepala 6 :
    Public KodeTautanCOA_BiayaGajiAdministrasi
    Public NamaTautanCOA_BiayaGajiAdministrasi = "BIAYA GAJI ADMINISTRASI"
    Public KodeTautanCOA_BiayaGajiAdministrasi2
    Public NamaTautanCOA_BiayaGajiAdministrasi2 = "BIAYA GAJI ADMINISTRASI 2"
    Public KodeTautanCOA_BiayaGajiAdministrasi3
    Public NamaTautanCOA_BiayaGajiAdministrasi3 = "BIAYA GAJI ADMINISTRASI 3"
    Public KodeTautanCOA_BiayaGajiAdministrasi4
    Public NamaTautanCOA_BiayaGajiAdministrasi4 = "BIAYA GAJI ADMINISTRASI 4"
    Public KodeTautanCOA_BiayaThrBonusAdministrasi
    Public NamaTautanCOA_BiayaThrBonusAdministrasi = "BIAYA THR/BONUS ADMINISTRASI"
    Public KodeTautanCOA_BiayaTunjanganPPh21Administrasi
    Public NamaTautanCOA_BiayaTunjanganPPh21Administrasi = "BIAYA TUNJANGAN PPH 21 ADMINISTRASI"
    Public KodeTautanCOA_BiayaBpjsTkJkkJkmAdministrasi
    Public NamaTautanCOA_BiayaBpjsTkJkkJkmAdministrasi = "BIAYA BPJS TK-JKK/JKM - ADMINISTRASI"
    Public KodeTautanCOA_BiayaBpjsTkJhtIpAdministrasi
    Public NamaTautanCOA_BiayaBpjsTkJhtIpAdministrasi = "BIAYA BPJS TK-JHT/IP - ADMINISTRASI"
    Public KodeTautanCOA_BiayaBpjsKesehatanAdministrasi
    Public NamaTautanCOA_BiayaBpjsKesehatanAdministrasi = "BIAYA BPJS KESEHATAN - ADMINISTRASI"
    Public KodeTautanCOA_BiayaAsuransiKaryawanAdministrasi
    Public NamaTautanCOA_BiayaAsuransiKaryawanAdministrasi = "BIAYA ASURANSI KARYAWAN - ADMINISTRASI"
    Public KodeTautanCOA_BiayaPesangonKaryawanAdministrasi
    Public NamaTautanCOA_BiayaPesangonKaryawanAdministrasi = "BIAYA PESANGON KARYAWAN - ADMINISTRASI"

    Public KodeTautanCOA_BiayaPPhPasal21
    Public NamaTautanCOA_BiayaPPhPasal21 = "BIAYA PPH PASAL 21"
    Public KodeTautanCOA_BiayaPPhPasal22
    Public NamaTautanCOA_BiayaPPhPasal22 = "BIAYA PPH PASAL 22"
    Public KodeTautanCOA_BiayaPPhPasal23
    Public NamaTautanCOA_BiayaPPhPasal23 = "BIAYA PPH PASAL 23"
    Public KodeTautanCOA_BiayaPPhPasal24
    Public NamaTautanCOA_BiayaPPhPasal24 = "BIAYA PPH PASAL 24"
    Public KodeTautanCOA_BiayaPPhPasal25
    Public NamaTautanCOA_BiayaPPhPasal25 = "BIAYA PPH PASAL 25"
    Public KodeTautanCOA_BiayaPPhPasal26
    Public NamaTautanCOA_BiayaPPhPasal26 = "BIAYA PPH PASAL 26"
    Public KodeTautanCOA_BiayaPPhPasal29
    Public NamaTautanCOA_BiayaPPhPasal29 = "BIAYA PPH PASAL 29"
    Public KodeTautanCOA_BiayaPPhPasal42
    Public NamaTautanCOA_BiayaPPhPasal42 = "BIAYA PPH PASAL 4 (2)"
    Public KodeTautanCOA_BiayaPPN
    Public NamaTautanCOA_BiayaPPN = "BIAYA PPN"
    Public KodeTautanCOA_BiayaKetetapanPajak
    Public NamaTautanCOA_BiayaKetetapanPajak = "BIAYA KETETAPAN PAJAK"

    'Public KodeTautanCOA_BiayaBpjsKesehatan
    'Public NamaTautanCOA_BiayaBpjsKesehatan = "BIAYA BPJS KESEHATAN"
    'Public KodeTautanCOA_BiayaBpjsKetenagakerjaan
    'Public NamaTautanCOA_BiayaBpjsKetenagakerjaan = "BIAYA BPJS KETENAGAKERJAAN"

    Public KodeTautanCOA_BiayaPerlengkapanKantor
    Public NamaTautanCOA_BiayaPerlengkapanKantor = "BIAYA KEPERLUAN KANTOR"
    Public KodeTautanCOA_BiayaSertifikasiDanLegalitas
    Public NamaTautanCOA_BiayaSertifikasiDanLegalitas = "BIAYA SERTIFIKASI DAN LEGALITAS"
    Public KodeTautanCOA_BiayaPengirimanPosDanEkspedisiAdministrasi
    Public NamaTautanCOA_BiayaPengirimanPosDanEkspedisiAdministrasi = "BIAYA PENGIRIMAN POS DAN EKSPEDISI ADMINISTRASI"
    Public KodeTautanCOA_BiayaAsuransiPenjualan
    Public NamaTautanCOA_BiayaAsuransiPenjualan = "BIAYA ASURANSI PENJUALAN"

    'Kepala 7 :
    Public KodeTautanCOA_PenghasilanBungaDendaPinjaman
    Public NamaTautanCOA_PenghasilanBungaDendaPinjaman = "PENGHASILAN BUNGA/DENDA PINJAMAN"
    Public KodeTautanCOA_LabaRugiSelisihKurs
    Public NamaTautanCOA_LabaRugiSelisihKurs = "LABA/RUGI SELISIH KURS"
    Public KodeTautanCOA_PenghasilanDividen
    Public NamaTautanCOA_PenghasilanDividen = "PENGHASILAN DIVIDEN"
    Public KodeTautanCOA_PenjualanLainnya
    Public NamaTautanCOA_PenjualanLainnya = "PENJUALAN LAINNYA"
    Public KodeTautanCOA_PenghasilanLainnya
    Public NamaTautanCOA_PenghasilanLainnya = "PENGHASILAN LAINNYA"

    'Kepala 8 :
    Public KodeTautanCOA_BiayaAdministrasiBank
    Public NamaTautanCOA_BiayaAdministrasiBank = "BIAYA ADMINISTRASI BANK"
    Public KodeTautanCOA_BiayaPPhPasal42_402
    Public NamaTautanCOA_BiayaPPhPasal42_402 = "BIAYA PPH PASAL 4 (2) - 402"
    Public KodeTautanCOA_BiayaBungaBank
    Public NamaTautanCOA_BiayaBungaBank = "BIAYA BUNGA BANK"
    Public KodeTautanCOA_BiayaDendaBank
    Public NamaTautanCOA_BiayaDendaBank = "BIAYA DENDA BANK"
    Public KodeTautanCOA_BiayaAdministrasiPerjanjian
    Public NamaTautanCOA_BiayaAdministrasiPerjanjian = "BIAYA ADMINISTRASI PERJANJIAN"
    Public KodeTautanCOA_HPPPenjualanDisposalAsset
    Public NamaTautanCOA_HPPPenjualanDisposalAsset = "HPP PENJUALAN/DISPOSAL ASSET"
    Public KodeTautanCOA_BiayaSelisihPencatatan
    Public NamaTautanCOA_BiayaSelisihPencatatan = "BIAYA SELISIH PENCATATAN"


    Sub PerbaruiTabelTautanCOA(SistemCOA As String)
        IsiTabelTautanCOA(SistemCOA)
        IsiValueTautanCOA()
    End Sub

    Public Sub IsiValueTautanCOA()

        AksesDatabase_General(Buka)

        'Kepala 1 :
        KodeTautanCOA_PettyCashAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_PettyCashAdministrasi)
        KodeTautanCOA_PettyCashProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_PettyCashProduksi)
        KodeTautanCOA_Kas = AmbilValueKodeTautanCOA(NamaTautanCOA_Kas)
        KodeTautanCOA_Kas_USD = AmbilValueKodeTautanCOA(NamaTautanCOA_Kas_USD)
        KodeTautanCOA_Kas_AUD = AmbilValueKodeTautanCOA(NamaTautanCOA_Kas_AUD)
        KodeTautanCOA_Kas_JPY = AmbilValueKodeTautanCOA(NamaTautanCOA_Kas_JPY)
        KodeTautanCOA_Kas_CNY = AmbilValueKodeTautanCOA(NamaTautanCOA_Kas_CNY)
        KodeTautanCOA_Kas_EUR = AmbilValueKodeTautanCOA(NamaTautanCOA_Kas_EUR)
        KodeTautanCOA_Kas_SGD = AmbilValueKodeTautanCOA(NamaTautanCOA_Kas_SGD)
        KodeTautanCOA_Kas_GBP = AmbilValueKodeTautanCOA(NamaTautanCOA_Kas_GBP)
        KodeTautanCOA_CashAdvance = AmbilValueKodeTautanCOA(NamaTautanCOA_CashAdvance)

        KodeTautanCOA_PiutangUsaha_NonAfiliasi = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_NonAfiliasi)
        KodeTautanCOA_PiutangUsaha_Afiliasi = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_Afiliasi)
        KodeTautanCOA_PiutangUsaha_USD = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_USD)
        KodeTautanCOA_PiutangUsaha_AUD = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_AUD)
        KodeTautanCOA_PiutangUsaha_JPY = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_JPY)
        KodeTautanCOA_PiutangUsaha_CNY = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_CNY)
        KodeTautanCOA_PiutangUsaha_EUR = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_EUR)
        KodeTautanCOA_PiutangUsaha_SGD = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_SGD)
        KodeTautanCOA_PiutangUsaha_GBP = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangUsaha_GBP)
        KodeTautanCOA_PiutangAfiliasi = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangAfiliasi)
        KodeTautanCOA_PiutangPihakKetiga = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangPihakKetiga)
        KodeTautanCOA_PiutangPemegangSaham = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangPemegangSaham)
        KodeTautanCOA_PiutangDividen = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangDividen)
        KodeTautanCOA_PiutangKaryawan = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangKaryawan)
        KodeTautanCOA_PiutangLainnya = AmbilValueKodeTautanCOA(NamaTautanCOA_PiutangLainnya)

        KodeTautanCOA_PersediaanBahanPenolong = AmbilValueKodeTautanCOA(NamaTautanCOA_PersediaanBahanPenolong)
        KodeTautanCOA_PersediaanBahanBaku_Lokal = AmbilValueKodeTautanCOA(NamaTautanCOA_PersediaanBahanBaku_Lokal)
        KodeTautanCOA_PersediaanBarangDalamProses = AmbilValueKodeTautanCOA(NamaTautanCOA_PersediaanBarangDalamProses)
        KodeTautanCOA_PersediaanBarangJadi = AmbilValueKodeTautanCOA(NamaTautanCOA_PersediaanBarangJadi)
        KodeTautanCOA_PersediaanBahanBaku_Import = AmbilValueKodeTautanCOA(NamaTautanCOA_PersediaanBahanBaku_Import)

        KodeTautanCOA_SewaTanahDanAtauBangunanDibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_SewaTanahDanAtauBangunanDibayarDimuka)
        KodeTautanCOA_SewaMesinDanPeralatanDibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_SewaMesinDanPeralatanDibayarDimuka)
        KodeTautanCOA_SewaKendaraanDibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_SewaKendaraanDibayarDimuka)
        KodeTautanCOA_BiayaRenovasiDibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaRenovasiDibayarDimuka)
        KodeTautanCOA_BiayaPendirianPerusahaan = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPendirianPerusahaan)
        KodeTautanCOA_AsuransiDibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_AsuransiDibayarDimuka)
        KodeTautanCOA_SewaAssetLainnyaDibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_SewaAssetLainnyaDibayarDimuka)
        KodeTautanCOA_DepositOperasional = AmbilValueKodeTautanCOA(NamaTautanCOA_DepositOperasional)
        KodeTautanCOA_BankGaransi = AmbilValueKodeTautanCOA(NamaTautanCOA_BankGaransi)
        KodeTautanCOA_DepositOperasionalEkspor = AmbilValueKodeTautanCOA(NamaTautanCOA_DepositOperasionalEkspor)
        KodeTautanCOA_UangMukaPembelian = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPembelian)
        KodeTautanCOA_UangMukaPembelian_Impor_USD = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPembelian_Impor_USD)
        KodeTautanCOA_UangMukaPembelian_Impor_AUD = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPembelian_Impor_AUD)
        KodeTautanCOA_UangMukaPembelian_Impor_JPY = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPembelian_Impor_JPY)
        KodeTautanCOA_UangMukaPembelian_Impor_CNY = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPembelian_Impor_CNY)
        KodeTautanCOA_UangMukaPembelian_Impor_EUR = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPembelian_Impor_EUR)
        KodeTautanCOA_UangMukaPembelian_Impor_SGD = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPembelian_Impor_SGD)
        KodeTautanCOA_UangMukaPembelian_Impor_GBP = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPembelian_Impor_GBP)
        KodeTautanCOA_BiayaDibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDibayarDimuka)
        KodeTautanCOA_BiayaDibayarDimuka_USD = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDibayarDimuka_USD)
        KodeTautanCOA_BiayaDibayarDimuka_AUD = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDibayarDimuka_AUD)
        KodeTautanCOA_BiayaDibayarDimuka_JPY = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDibayarDimuka_JPY)
        KodeTautanCOA_BiayaDibayarDimuka_CNY = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDibayarDimuka_CNY)
        KodeTautanCOA_BiayaDibayarDimuka_EUR = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDibayarDimuka_EUR)
        KodeTautanCOA_BiayaDibayarDimuka_SGD = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDibayarDimuka_SGD)
        KodeTautanCOA_BiayaDibayarDimuka_GBP = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDibayarDimuka_GBP)

        KodeTautanCOA_PPhPasal21DibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_PPhPasal21DibayarDimuka)
        KodeTautanCOA_PPhPasal22DibayarDimuka_Lokal = AmbilValueKodeTautanCOA(NamaTautanCOA_PPhPasal22DibayarDimuka_Lokal)
        KodeTautanCOA_PPhPasal22DibayarDimuka_Impor = AmbilValueKodeTautanCOA(NamaTautanCOA_PPhPasal22DibayarDimuka_Impor)
        KodeTautanCOA_PPhPasal23DibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_PPhPasal23DibayarDimuka)
        KodeTautanCOA_PPhPasal25DibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_PPhPasal25DibayarDimuka)
        KodeTautanCOA_PPhPasal42DibayarDimuka = AmbilValueKodeTautanCOA(NamaTautanCOA_PPhPasal42DibayarDimuka)
        KodeTautanCOA_PPNMasukan_Lokal = AmbilValueKodeTautanCOA(NamaTautanCOA_PPNMasukan_Lokal)
        KodeTautanCOA_PPNMasukan_Impor = AmbilValueKodeTautanCOA(NamaTautanCOA_PPNMasukan_Impor)
        KodeTautanCOA_PPhPasal23DibayarDimuka_BPBelumDiterima = AmbilValueKodeTautanCOA(NamaTautanCOA_PPhPasal23DibayarDimuka_BPBelumDiterima)
        KodeTautanCOA_PPhPasal42DibayarDimuka_BPBelumDiterima = AmbilValueKodeTautanCOA(NamaTautanCOA_PPhPasal42DibayarDimuka_BPBelumDiterima)

        KodeTautanCOA_BankEceran1 = AmbilValueKodeTautanCOA(NamaTautanCOA_BankEceran1)
        KodeTautanCOA_BankEceran2 = AmbilValueKodeTautanCOA(NamaTautanCOA_BankEceran2)
        KodeTautanCOA_BankEceran3 = AmbilValueKodeTautanCOA(NamaTautanCOA_BankEceran3)
        KodeTautanCOA_Bangunan = AmbilValueKodeTautanCOA(NamaTautanCOA_Bangunan)

        KodeTautanCOA_InvestasiDeposito = AmbilValueKodeTautanCOA(NamaTautanCOA_InvestasiDeposito)
        KodeTautanCOA_InvestasiSuratBerharga = AmbilValueKodeTautanCOA(NamaTautanCOA_InvestasiSuratBerharga)
        KodeTautanCOA_InvestasiLogamMulia = AmbilValueKodeTautanCOA(NamaTautanCOA_InvestasiLogamMulia)
        KodeTautanCOA_InvestasiPadaPerusahaanAnak = AmbilValueKodeTautanCOA(NamaTautanCOA_InvestasiPadaPerusahaanAnak)
        KodeTautanCOA_InvestasiGoodWill = AmbilValueKodeTautanCOA(NamaTautanCOA_InvestasiGoodWill)

        'Kepala 2 :
        KodeTautanCOA_HutangUsaha_NonAfiliasi = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_NonAfiliasi)
        KodeTautanCOA_HutangUsaha_Afiliasi = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_Afiliasi)
        KodeTautanCOA_HutangDeposit = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangDeposit)
        KodeTautanCOA_HutangUsaha_USD = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_USD)
        KodeTautanCOA_HutangUsaha_AUD = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_AUD)
        KodeTautanCOA_HutangUsaha_JPY = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_JPY)
        KodeTautanCOA_HutangUsaha_CNY = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_CNY)
        KodeTautanCOA_HutangUsaha_EUR = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_EUR)
        KodeTautanCOA_HutangUsaha_SGD = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_SGD)
        KodeTautanCOA_HutangUsaha_GBP = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangUsaha_GBP)
        KodeTautanCOA_HutangBiaya = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangBiaya)
        KodeTautanCOA_HutangKetetapanPajak = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangKetetapanPajak)

        KodeTautanCOA_HutangBpjsKesehatan = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangBpjsKesehatan)
        KodeTautanCOA_HutangBpjsKetenagakerjaan = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangBpjsKetenagakerjaan)
        KodeTautanCOA_HutangGaji = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangGaji)
        KodeTautanCOA_UangMukaPenjualan = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPenjualan)
        KodeTautanCOA_UangMukaPenjualan_Ekspor_USD = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPenjualan_Ekspor_USD)
        KodeTautanCOA_UangMukaPenjualan_Ekspor_AUD = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPenjualan_Ekspor_AUD)
        KodeTautanCOA_UangMukaPenjualan_Ekspor_JPY = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPenjualan_Ekspor_JPY)
        KodeTautanCOA_UangMukaPenjualan_Ekspor_CNY = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPenjualan_Ekspor_CNY)
        KodeTautanCOA_UangMukaPenjualan_Ekspor_EUR = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPenjualan_Ekspor_EUR)
        KodeTautanCOA_UangMukaPenjualan_Ekspor_SGD = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPenjualan_Ekspor_SGD)
        KodeTautanCOA_UangMukaPenjualan_Ekspor_GBP = AmbilValueKodeTautanCOA(NamaTautanCOA_UangMukaPenjualan_Ekspor_GBP)
        KodeTautanCOA_HutangPPhPasal21 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal21)
        KodeTautanCOA_HutangPPhPasal22 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal22)
        KodeTautanCOA_HutangPPhPasal23 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal23)
        KodeTautanCOA_HutangPPhPasal42 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal42)
        KodeTautanCOA_HutangPPhPasal25 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal25)
        KodeTautanCOA_HutangPPhPasal26 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal26)
        KodeTautanCOA_PPNKeluaran = AmbilValueKodeTautanCOA(NamaTautanCOA_PPNKeluaran)
        KodeTautanCOA_HutangPPN = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPN)
        KodeTautanCOA_HutangPPhPasal29 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal29)
        KodeTautanCOA_HutangPPhPasal21_100 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal21_100)
        KodeTautanCOA_HutangPPhPasal21_401 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal21_401)
        KodeTautanCOA_HutangPPhPasal23_100 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal23_100)
        KodeTautanCOA_HutangPPhPasal23_101 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal23_101)
        KodeTautanCOA_HutangPPhPasal23_102 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal23_102)
        KodeTautanCOA_HutangPPhPasal23_103 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal23_103)
        KodeTautanCOA_HutangPPhPasal23_104 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal23_104)
        KodeTautanCOA_HutangPPhPasal42_402 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal42_402)
        KodeTautanCOA_HutangPPhPasal42_403 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal42_403)
        KodeTautanCOA_HutangPPhPasal42_409 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal42_409)
        KodeTautanCOA_HutangPPhPasal42_419 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal42_419)
        KodeTautanCOA_HutangPPhPasal26_100 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal26_100)
        KodeTautanCOA_HutangPPhPasal26_101 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal26_101)
        KodeTautanCOA_HutangPPhPasal26_102 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal26_102)
        KodeTautanCOA_HutangPPhPasal26_103 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal26_103)
        KodeTautanCOA_HutangPPhPasal26_104 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal26_104)
        KodeTautanCOA_HutangPPhPasal26_105 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPhPasal26_105)

        KodeTautanCOA_HutangPPN_100 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPN_100)
        KodeTautanCOA_HutangPPN_101 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPN_101)
        KodeTautanCOA_HutangPPN_102 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPN_102)
        KodeTautanCOA_HutangPPN_103 = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPN_103)
        KodeTautanCOA_HutangPPN_Impor = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPPN_Impor)

        KodeTautanCOA_HutangKoperasiKaryawan = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangKoperasiKaryawan)
        KodeTautanCOA_HutangSerikat = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangSerikat)
        KodeTautanCOA_HutangPihakKetiga = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPihakKetiga)
        KodeTautanCOA_HutangKaryawan = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangKaryawan)
        KodeTautanCOA_HutangLancarLainnya = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangLancarLainnya)

        KodeTautanCOA_HutangLeasing = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangLeasing)
        KodeTautanCOA_HutangBank = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangBank)
        KodeTautanCOA_HutangPemegangSaham = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangPemegangSaham)
        KodeTautanCOA_HutangAfiliasi = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangAfiliasi)

        KodeTautanCOA_HutangDividen = AmbilValueKodeTautanCOA(NamaTautanCOA_HutangDividen)

        'Kepala 3 :
        KodeTautanCOA_Modal = AmbilValueKodeTautanCOA(NamaTautanCOA_Modal)
        KodeTautanCOA_LabaDitahan = AmbilValueKodeTautanCOA(NamaTautanCOA_LabaDitahan)
        KodeTautanCOA_LabaTahunBerjalan = AmbilValueKodeTautanCOA(NamaTautanCOA_LabaTahunBerjalan)

        'Kepala 4 :
        KodeTautanCOA_PenjualanBarang_Trading = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanBarang_Trading)
        KodeTautanCOA_PenjualanJasa = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanJasa)
        KodeTautanCOA_PenjualanJasaKonstruksi = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanJasaKonstruksi)
        KodeTautanCOA_PenjualanEkspor = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanEkspor)
        KodeTautanCOA_PenjualanEceran = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanEceran)
        KodeTautanCOA_PenjualanBarang_Manufaktur = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanBarang_Manufaktur)
        KodeTautanCOA_PenjualanAssetTanahBangunan = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanAssetTanahBangunan)
        KodeTautanCOA_PenjualanAssetLainnya = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanAssetLainnya)
        KodeTautanCOA_ReturPenjualan = AmbilValueKodeTautanCOA(NamaTautanCOA_ReturPenjualan)

        'Kepala 5 :
        KodeTautanCOA_ReturPembelianBahanBaku_Lokal = AmbilValueKodeTautanCOA(NamaTautanCOA_ReturPembelianBahanBakuLokal)
        KodeTautanCOA_BiayaTransportasiPembelianBb_Lokal = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaTransportasiPembelianBb_Lokal)
        KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPemakaianBahanBaku_Lokal)
        KodeTautanCOA_ReturPembelianBahanBaku_Import = AmbilValueKodeTautanCOA(NamaTautanCOA_ReturPembelianBahanBaku_Import)
        KodeTautanCOA_BiayaAsuransiImport = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaAsuransiImport)
        KodeTautanCOA_BiayaPengapalan = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPengapalan)
        KodeTautanCOA_BeaMasuk_Impor = AmbilValueKodeTautanCOA(NamaTautanCOA_BeaMasuk_Impor)
        KodeTautanCOA_BiayaTransportasi_Import = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaTransportasi_Import)
        KodeTautanCOA_BiayaPemakaianBahanBaku_Import = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPemakaianBahanBaku_Import)
        KodeTautanCOA_BiayaBahanBaku = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaBahanBaku)

        KodeTautanCOA_BiayaGajiProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaGajiProduksi)
        KodeTautanCOA_BiayaGajiProduksi2 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaGajiProduksi2)
        KodeTautanCOA_BiayaGajiProduksi3 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaGajiProduksi3)
        KodeTautanCOA_BiayaGajiProduksi4 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaGajiProduksi4)
        KodeTautanCOA_BiayaThrBonusProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaThrBonusProduksi)
        KodeTautanCOA_BiayaTunjanganPPh21Produksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaTunjanganPPh21Produksi)
        KodeTautanCOA_BiayaBpjsTkJkkJkmProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaBpjsTkJkkJkmProduksi)
        KodeTautanCOA_BiayaBpjsTkJhtIpProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaBpjsTkJhtIpProduksi)
        KodeTautanCOA_BiayaBpjsKesehatanProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaBpjsKesehatanProduksi)
        KodeTautanCOA_BiayaAsuransiKaryawanProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaAsuransiKaryawanProduksi)
        KodeTautanCOA_BiayaPesangonKaryawanProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPesangonKaryawanProduksi)
        KodeTautanCOA_BiayaTenagaKerjaLangsung = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaTenagaKerjaLangsung)

        KodeTautanCOA_ReturPembelianBahanPenolong = AmbilValueKodeTautanCOA(NamaTautanCOA_ReturPembelianBahanPenolong)
        KodeTautanCOA_BiayaPengirimanDanEkspedisiProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPengirimanDanEkspedisiProduksi)
        KodeTautanCOA_BiayaPemakaianBahanPenolong = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPemakaianBahanPenolong)
        KodeTautanCOA_BiayaOverheadPabrik = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaOverheadPabrik)
        KodeTautanCOA_ReturPembelianLainnya = AmbilValueKodeTautanCOA(NamaTautanCOA_ReturPembelianLainnya)
        KodeTautanCOA_BiayaProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaProduksi)
        KodeTautanCOA_HargaPokokProduksi = AmbilValueKodeTautanCOA(NamaTautanCOA_HargaPokokProduksi)
        KodeTautanCOA_HargaPokokPenjualan = AmbilValueKodeTautanCOA(NamaTautanCOA_HargaPokokPenjualan)
        KodeTautanCOA_PembelianBahanBaku_Lokal = AmbilValueKodeTautanCOA(NamaTautanCOA_PembelianBahanBaku_Lokal)
        KodeTautanCOA_PembelianBahanBaku_Import = AmbilValueKodeTautanCOA(NamaTautanCOA_PembelianBahanBaku_Import)
        KodeTautanCOA_PembelianBahanPenolong = AmbilValueKodeTautanCOA(NamaTautanCOA_PembelianBahanPenolong)

        'Kepala 6 :
        KodeTautanCOA_BiayaGajiAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaGajiAdministrasi)
        KodeTautanCOA_BiayaGajiAdministrasi2 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaGajiAdministrasi2)
        KodeTautanCOA_BiayaGajiAdministrasi3 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaGajiAdministrasi3)
        KodeTautanCOA_BiayaGajiAdministrasi4 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaGajiAdministrasi4)
        KodeTautanCOA_BiayaThrBonusAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaThrBonusAdministrasi)
        KodeTautanCOA_BiayaTunjanganPPh21Administrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaTunjanganPPh21Administrasi)
        KodeTautanCOA_BiayaBpjsTkJkkJkmAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaBpjsTkJkkJkmAdministrasi)
        KodeTautanCOA_BiayaBpjsTkJhtIpAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaBpjsTkJhtIpAdministrasi)
        KodeTautanCOA_BiayaBpjsKesehatanAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaBpjsKesehatanAdministrasi)
        KodeTautanCOA_BiayaAsuransiKaryawanAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaAsuransiKaryawanAdministrasi)
        KodeTautanCOA_BiayaPesangonKaryawanAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPesangonKaryawanAdministrasi)

        KodeTautanCOA_BiayaPPhPasal21 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal21)
        KodeTautanCOA_BiayaPPhPasal22 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal22)
        KodeTautanCOA_BiayaPPhPasal23 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal23)
        KodeTautanCOA_BiayaPPhPasal24 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal24)
        KodeTautanCOA_BiayaPPhPasal25 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal25)
        KodeTautanCOA_BiayaPPhPasal26 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal26)
        KodeTautanCOA_BiayaPPhPasal29 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal29)
        KodeTautanCOA_BiayaPPhPasal42 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal42)
        KodeTautanCOA_BiayaPPN = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPN)
        KodeTautanCOA_BiayaKetetapanPajak = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaKetetapanPajak)

        KodeTautanCOA_BiayaPerlengkapanKantor = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPerlengkapanKantor)
        KodeTautanCOA_BiayaSertifikasiDanLegalitas = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaSertifikasiDanLegalitas)
        KodeTautanCOA_BiayaPengirimanPosDanEkspedisiAdministrasi = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPengirimanPosDanEkspedisiAdministrasi)
        KodeTautanCOA_BiayaAsuransiPenjualan = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaAsuransiPenjualan)

        'Kepala 7 :
        KodeTautanCOA_PenghasilanBungaDendaPinjaman = AmbilValueKodeTautanCOA(NamaTautanCOA_PenghasilanBungaDendaPinjaman)
        KodeTautanCOA_LabaRugiSelisihKurs = AmbilValueKodeTautanCOA(NamaTautanCOA_LabaRugiSelisihKurs)
        KodeTautanCOA_PenghasilanDividen = AmbilValueKodeTautanCOA(NamaTautanCOA_PenghasilanDividen)
        KodeTautanCOA_PenjualanLainnya = AmbilValueKodeTautanCOA(NamaTautanCOA_PenjualanLainnya)
        KodeTautanCOA_PenghasilanLainnya = AmbilValueKodeTautanCOA(NamaTautanCOA_PenghasilanLainnya)

        'Kepala 8 :
        KodeTautanCOA_BiayaAdministrasiBank = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaAdministrasiBank)
        KodeTautanCOA_BiayaPPhPasal42_402 = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaPPhPasal42_402)
        KodeTautanCOA_BiayaBungaBank = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaBungaBank)
        KodeTautanCOA_BiayaDendaBank = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaDendaBank)
        KodeTautanCOA_BiayaAdministrasiPerjanjian = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaAdministrasiPerjanjian)
        KodeTautanCOA_HPPPenjualanDisposalAsset = AmbilValueKodeTautanCOA(NamaTautanCOA_HPPPenjualanDisposalAsset)
        KodeTautanCOA_BiayaSelisihPencatatan = AmbilValueKodeTautanCOA(NamaTautanCOA_BiayaSelisihPencatatan)

        AksesDatabase_General(Tutup)

    End Sub

    Public Function AmbilValueKodeTautanCOA(NamaTautanCoa)

        Dim KodeTautanCOA = Kosongan
        Dim StatusKoneksiDatabaseSebelumIni As Boolean
        RefreshInfo_StatusKoneksiDatabaseGeneral()
        If StatusKoneksiDatabaseGeneral = True Then StatusKoneksiDatabaseSebelumIni = True
        If StatusKoneksiDatabaseGeneral = False Then StatusKoneksiDatabaseSebelumIni = False
        If StatusKoneksiDatabaseSebelumIni = False Then AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_TautanCOA WHERE Tautan_COA = '" & NamaTautanCoa & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader()
        dr.Read()
        If dr.HasRows Then KodeTautanCOA = dr.Item("COA")
        If StatusKoneksiDatabaseSebelumIni = False Then AksesDatabase_General(Tutup)
        Return KodeTautanCOA

    End Function

    Public Sub IsiTabelTautanCOA(SistemCOA_Reg)

        Dim QueryIsiTautanCOA_Reg = " INSERT INTO tbl_TautanCOA ( Tautan_COA, COA ) VALUES "

        'Kepala 1 :
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PettyCashAdministrasi & "',                      '11101'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PettyCashProduksi & "',                          '11102'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Kas & "',                                        '11201'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Kas_USD & "',                                    '11211'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Kas_AUD & "',                                    '11212'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Kas_JPY & "',                                    '11213'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Kas_CNY & "',                                    '11214'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Kas_EUR & "',                                    '11215'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Kas_SGD & "',                                    '11216'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Kas_GBP & "',                                    '11217'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_CashAdvance & "',                                '11401'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_NonAfiliasi & "',                   '11501'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangAfiliasi & "',                            '11503'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangPihakKetiga & "',                         '11504'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangPemegangSaham & "',                       '11505'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangDividen & "',                             '11506'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangKaryawan & "',                            '11508'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangLainnya & "',                             '11509'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_USD & "',                           '11511'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_AUD & "',                           '11512'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_JPY & "',                           '11513'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_CNY & "',                           '11514'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_EUR & "',                           '11515'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_SGD & "',                           '11516'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_GBP & "',                           '11517'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PiutangUsaha_Afiliasi & "',                      '11520'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_SewaTanahDanAtauBangunanDibayarDimuka & "',      '11601'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_SewaMesinDanPeralatanDibayarDimuka & "',         '11602'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_SewaKendaraanDibayarDimuka & "',                 '11603'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaRenovasiDibayarDimuka & "',                 '11604'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPendirianPerusahaan & "',                   '11605'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_AsuransiDibayarDimuka & "',                      '11606'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_SewaAssetLainnyaDibayarDimuka & "',              '11607'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_DepositOperasional & "',                         '11608'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BankGaransi & "',                                '11609'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_DepositOperasionalEkspor & "',                   '11610'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPembelian & "',                          '11700'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPembelian_Impor_USD & "',                '11701'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPembelian_Impor_AUD & "',                '11702'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPembelian_Impor_JPY & "',                '11703'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPembelian_Impor_CNY & "',                '11704'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPembelian_Impor_EUR & "',                '11705'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPembelian_Impor_SGD & "',                '11706'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPembelian_Impor_GBP & "',                '11707'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDibayarDimuka & "',                         '11710'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDibayarDimuka_USD & "',                     '11711'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDibayarDimuka_AUD & "',                     '11712'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDibayarDimuka_JPY & "',                     '11713'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDibayarDimuka_CNY & "',                     '11714'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDibayarDimuka_EUR & "',                     '11715'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDibayarDimuka_SGD & "',                     '11716'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDibayarDimuka_GBP & "',                     '11717'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PersediaanBahanPenolong & "',                    '11801'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PersediaanBahanBaku_Lokal & "',                  '11802'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PersediaanBarangDalamProses & "',                '11803'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PersediaanBarangJadi & "',                       '11804'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PersediaanBahanBaku_Import & "',                 '11805'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPhPasal21DibayarDimuka & "',                    '11901'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPhPasal22DibayarDimuka_Lokal & "',              '11902'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPhPasal22DibayarDimuka_Impor & "',              '11903'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPhPasal23DibayarDimuka & "',                    '11904'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPhPasal42DibayarDimuka & "',                    '11905'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPhPasal25DibayarDimuka & "',                    '11906'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPNMasukan_Lokal & "',                           '11907'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPNMasukan_Impor & "',                           '11908'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPhPasal23DibayarDimuka_BPBelumDiterima & "',    '11914'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPhPasal42DibayarDimuka_BPBelumDiterima & "',    '11915'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BankEceran1 & "',                                '11301'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BankEceran2 & "',                                '11302'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BankEceran3 & "',                                '11303'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Bangunan & "',                                   '12220'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_InvestasiDeposito & "',                          '13101'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_InvestasiSuratBerharga & "',                     '13102'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_InvestasiLogamMulia & "',                        '13103'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_InvestasiPadaPerusahaanAnak & "',                '13201'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_InvestasiGoodWill & "',                          '14101'), "

        'Kepala 2 :
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_NonAfiliasi & "',                    '21101'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_USD & "',                            '21111'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_AUD & "',                            '21112'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_JPY & "',                            '21113'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_CNY & "',                            '21114'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_EUR & "',                            '21115'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_SGD & "',                            '21116'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_GBP & "',                            '21117'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangUsaha_Afiliasi & "',                       '21120'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangDeposit & "',                              '21130'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangBiaya & "',                                '21201'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangKetetapanPajak & "',                       '21202'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangBpjsKesehatan & "',                        '21301'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangBpjsKetenagakerjaan & "',                  '21302'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangGaji & "',                                 '21401'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPenjualan & "',                          '21500'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPenjualan_Ekspor_USD & "',               '21501'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPenjualan_Ekspor_AUD & "',               '21502'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPenjualan_Ekspor_JPY & "',               '21503'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPenjualan_Ekspor_CNY & "',               '21504'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPenjualan_Ekspor_EUR & "',               '21505'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPenjualan_Ekspor_SGD & "',               '21506'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_UangMukaPenjualan_Ekspor_GBP & "',               '21507'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal21 & "',                           '21601'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal22 & "',                           '21602'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal23 & "',                           '21603'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal42 & "',                           '21604'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal25 & "',                           '21605'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal26 & "',                           '21606'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PPNKeluaran & "',                                '21607'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPN & "',                                  '21608'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal29 & "',                           '21609'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal21_100 & "',                       '21610'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal21_401 & "',                       '21611'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal23_100 & "',                       '21630'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal23_101 & "',                       '21631'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal23_102 & "',                       '21632'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal23_103 & "',                       '21633'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal23_104 & "',                       '21634'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal42_402 & "',                       '21642'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal42_403 & "',                       '21643'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal42_409 & "',                       '21648'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal42_419 & "',                       '21649'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal26_100 & "',                       '21660'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal26_101 & "',                       '21661'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal26_102 & "',                       '21662'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal26_103 & "',                       '21663'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal26_104 & "',                       '21664'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPhPasal26_105 & "',                       '21665'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPN_100 & "',                              '21680'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPN_101 & "',                              '21681'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPN_102 & "',                              '21682'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPN_103 & "',                              '21683'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPPN_Impor & "',                            '21684'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangKoperasiKaryawan & "',                     '21701'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangSerikat & "',                              '21801'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPihakKetiga & "',                          '21802'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangKaryawan & "',                             '21808'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangLancarLainnya & "',                        '21901'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangLeasing & "',                              '22100'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangBank & "',                                 '23100'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangPemegangSaham & "',                        '24100'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangAfiliasi & "',                             '25100'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HutangDividen & "',                              '29999'), "

        'Kepala 3 :
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_Modal & "',                                      '31101'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_LabaDitahan & "',                                '34101'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_LabaTahunBerjalan & "',                          '35101'), "

        'Kepala 4 "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanBarang_Trading & "',                    '41001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanJasa & "',                              '42001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanJasaKonstruksi & "',                    '43001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanEkspor & "',                            '44001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanEceran & "',                            '45001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanBarang_Manufaktur & "',                 '47001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanAssetTanahBangunan & "',                '48001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanAssetLainnya & "',                      '48002'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_ReturPenjualan & "',                             '49999'), "

        'Kepala 5 :
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PembelianBahanBaku_Lokal & "',                   '51100'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_ReturPembelianBahanBakuLokal & "',               '51101'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaTransportasiPembelianBb_Lokal & "',         '51102'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPemakaianBahanBaku_Lokal & "',              '51199'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PembelianBahanBaku_Import & "',                  '51200'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_ReturPembelianBahanBaku_Import & "',             '51201'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaAsuransiImport & "',                        '51202'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPengapalan & "',                            '51203'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BeaMasuk_Impor & "',                             '51204'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaTransportasi_Import & "',                   '51205'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPemakaianBahanBaku_Import & "',             '51299'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBahanBaku & "',                             '51300'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaGajiProduksi & "',                          '52101'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaGajiProduksi2 & "',                         '52102'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaGajiProduksi3 & "',                         '52103'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaGajiProduksi4 & "',                         '52104'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaThrBonusProduksi & "',                      '52105'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaTunjanganPPh21Produksi & "',                '52106'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBpjsTkJkkJkmProduksi & "',                  '52107'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBpjsTkJhtIpProduksi & "',                   '52108'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBpjsKesehatanProduksi & "',                 '52109'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaAsuransiKaryawanProduksi & "',              '52110'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPesangonKaryawanProduksi & "',              '52115'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaTenagaKerjaLangsung & "',                   '52300'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PembelianBahanPenolong & "',                     '53100'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_ReturPembelianBahanPenolong & "',                '53101'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPengirimanDanEkspedisiProduksi & "',        '53122'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPemakaianBahanPenolong & "',                '53199'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaOverheadPabrik & "',                        '53500'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_ReturPembelianLainnya & "',                      '53998'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaProduksi & "',                              '55551'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HargaPokokProduksi & "',                         '55552'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HargaPokokPenjualan & "',                        '55555'), "

        'Kepala 6 :
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaGajiAdministrasi & "',                      '61101'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaGajiAdministrasi2 & "',                     '61102'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaGajiAdministrasi3 & "',                     '61103'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaGajiAdministrasi4 & "',                     '61104'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaThrBonusAdministrasi & "',                  '61105'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaTunjanganPPh21Administrasi & "',            '61106'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBpjsTkJkkJkmAdministrasi & "',              '61107'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBpjsTkJhtIpAdministrasi & "',               '61108'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBpjsKesehatanAdministrasi & "',             '61109'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaAsuransiKaryawanAdministrasi & "',          '61110'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPesangonKaryawanAdministrasi & "',          '61129'), "

        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal21 & "',                            '61701'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal22 & "',                            '61702'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal23 & "',                            '61703'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal24 & "',                            '61704'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal25 & "',                            '61705'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal26 & "',                            '61706'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal29 & "',                            '61707'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal42 & "',                            '61708'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPN & "',                                   '61709'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaKetetapanPajak & "',                        '61710'), "

        'QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBpjsKesehatan & "',                         ''), "
        'QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBpjsKetenagakerjaan & "',                   ''), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPerlengkapanKantor & "',                    '61203'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaSertifikasiDanLegalitas & "',               '61209'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPengirimanPosDanEkspedisiAdministrasi & "', '61213'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaAsuransiPenjualan & "',                     '62404'), "

        'Kepala 7 :
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenghasilanBungaDendaPinjaman & "',              '71001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_LabaRugiSelisihKurs & "',                        '78001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenghasilanDividen & "',                         '78002'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenjualanLainnya & "',                           '79998'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_PenghasilanLainnya & "',                         '79999'), "

        'Kepala 8 :
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaAdministrasiBank & "',                      '81001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaPPhPasal42_402 & "',                        '82002'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaBungaBank & "',                             '83001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaDendaBank & "',                             '83002'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaAdministrasiPerjanjian & "',                '83003'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_HPPPenjualanDisposalAsset & "',                  '84001'), "
        QueryIsiTautanCOA_Reg &= " ('" & NamaTautanCOA_BiayaSelisihPencatatan & "',                     '89999')  " '<--- UJUNG QUERY..! TIDAK PAKAI TANDA KOMA..!!!!

        AksesDatabase_General(Buka)

        Try
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_TautanCOA ", KoneksiDatabaseGeneral)
            cmdHAPUS.ExecuteNonQuery()
            cmd = New OdbcCommand(QueryIsiTautanCOA_Reg, KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
            HasilPembuatanDatabaseGeneral = True
            If SistemCOA_Reg = SistemCOA_StandarAplikasi Then PesanKhususPCDeveloper("Pengisian Data 'Tautan COA' Berhasil.")
        Catch ex As Exception
            AksesDatabase_General(Tutup)
            HasilPembuatanDatabaseGeneral = False
            If SistemCOA_Reg = SistemCOA_StandarAplikasi Then PesanKhususPCDeveloper("Pengisian Data 'Tautan COA' Gagal.")
        End Try

        'Jika Menggunakan Sistem COA Customize, maka value COA pada Tabel tbl_TautanCOA harus dikosongkan..!!!
        If HasilPembuatanDatabaseGeneral = True Then
            If SistemCOA_Reg = SistemCOA_Customize Then
                cmd = New OdbcCommand(" UPDATE tbl_TautanCOA SET COA = '' ", KoneksiDatabaseGeneral)
                cmd_ExecuteNonQuery()
                If StatusSuntingDatabase = True Then
                    HasilPembuatanDatabaseGeneral = True
                    PesanKhususPCDeveloper("Pengisian Data 'Tautan COA' Berhasil.")
                Else
                    HasilPembuatanDatabaseGeneral = False
                    PesanKhususPCDeveloper("Pengisian Data 'Tautan COA' Gagal.")
                End If
            End If
        End If

        AksesDatabase_General(Tutup)

    End Sub

End Module
