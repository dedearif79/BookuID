Module wpfMdl_ClassWindow

    ' NOTE: win_BOOKU dan win_Startup TIDAK boleh As New karena akan membuat instance
    ' sebelum Main() dijalankan. Inisialisasi dilakukan di mdlWpf_Program.Main()
    Public win_BOOKU As wpfWin_BOOKU
    Public win_Startup As wpfWin_StartUp

    '═══════════════════════════════════════════════════════════════════════════
    ' STARTUP & LOGIN
    '═══════════════════════════════════════════════════════════════════════════
    Public win_Login As New wpfWin_Login
    Public win_KunciAkses As New wpfWin_KunciAkses

    '═══════════════════════════════════════════════════════════════════════════
    ' REGISTRASI & TAHUN BUKU
    '═══════════════════════════════════════════════════════════════════════════
    Public win_Registrasi As New wpfWin_Registrasi
    Public win_Registrasi_IsiDataCompany As New wpfWin_Registrasi_IsiDataCompany
    Public win_Registrasi_IsiDataUser As New wpfWin_Registrasi_IsiDataUser
    Public win_BuatDatabaseBukuBaru As New wpfWin_BuatDatabaseBukuBaru
    Public win_GantiTahunBuku As New wpfWin_GantiTahunBuku

    '═══════════════════════════════════════════════════════════════════════════
    ' PROGRESS & LOADING
    '═══════════════════════════════════════════════════════════════════════════
    Public win_Loading As New wpfWin_Loading

    '═══════════════════════════════════════════════════════════════════════════
    ' DIALOG PILIHAN / UTILITY
    '═══════════════════════════════════════════════════════════════════════════
    Public win_PilihBulan As New wpfWin_PilihBulan
    Public win_CeklisBulan As New wpfWin_CeklisBulan
    Public win_PilihJurnalAdjusment As New wpfWin_PilihJurnalAdjusment
    Public win_MetodeInputBayar As New wpfWin_MetodeInputBayar
    Public win_Pengaturan As New wpfWin_Pengaturan

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - COA
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputCOA As New wpfWin_InputCOA
    Public win_ListCOA As New wpfWin_ListCOA
    Public win_TautanCOA As New wpfWin_TautanCOA
    Public win_ProgressImportDataCOA As New wpfWin_ProgressImportDataCOA

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - LAWAN TRANSAKSI / MITRA
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputLawanTransaksi As New wpfWin_InputLawanTransaksi
    Public win_ListLawanTransaksi As New wpfWin_ListLawanTransaksi

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - USER
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputUser As New wpfWin_InputUser
    Public win_GantiPassword As New wpfWin_GantiPassword

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - PROJECT
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputDataProject As New wpfWin_InputDataProject
    Public win_ListDataProject As New wpfWin_ListDataProject

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - KARYAWAN
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputDataKaryawan As New wpfWin_InputDataKaryawan
    Public win_ListDataKaryawan As New wpfWin_ListDataKaryawan

    '═══════════════════════════════════════════════════════════════════════════
    ' BANK GARANSI
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputBankGaransi As New wpfWin_InputBankGaransi

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - KURS
    '═══════════════════════════════════════════════════════════════════════════
    Public win_UpdateKurs_Bulanan As New wpfWin_UpdateKurs_Bulanan
    Public win_UpdateKurs_Harian As New wpfWin_UpdateKurs_Harian

    '═══════════════════════════════════════════════════════════════════════════
    ' DATA MASTER - PEMEGANG SAHAM
    '═══════════════════════════════════════════════════════════════════════════
    Public win_DaftarSaham As New wpfWin_DaftarSaham

    '═══════════════════════════════════════════════════════════════════════════
    ' PEMBELIAN - PENJUALAN : LIST
    '═══════════════════════════════════════════════════════════════════════════
    Public win_ListPO As New wpfWin_ListPO
    Public win_ListSJBAST As New wpfWin_ListSJBAST
    Public win_ListInvoice As New wpfWin_ListInvoice

    '═══════════════════════════════════════════════════════════════════════════
    ' PEMBELIAN - PENJUALAN : INPUT MANUAL
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputSJBASTManual As New wpfWin_InputSJBASTManual
    Public win_InputProduk_Nota As New wpfWin_InputProduk_Nota
    Public win_DorongInvoiceKeJurnal As New wpfWin_DorongInvoiceKeJurnal

    '═══════════════════════════════════════════════════════════════════════════
    ' PEMBELIAN : INPUT NOTA
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputPOPembelian As New wpfWin_InputPOPembelian
    Public win_InputSuratJalanPembelian As New wpfWin_InputSuratJalanPembelian
    Public win_InputBASTPembelian As New wpfWin_InputBASTPembelian
    Public win_InputInvoicePembelian As New wpfWin_InputInvoicePembelian
    Public win_InputInvoicePembelian_Alt As New wpfWin_InputInvoicePembelian_Alt
    Public win_InputReturPembelian As New wpfWin_InputReturPembelian

    '═══════════════════════════════════════════════════════════════════════════
    ' PENJUALAN : INPUT NOTA
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputPOPenjualan As New wpfWin_InputPOPenjualan
    Public win_InputSuratJalanPenjualan As New wpfWin_InputSuratJalanPenjualan
    Public win_InputBASTPenjualan As New wpfWin_InputBASTPenjualan
    Public win_InputInvoicePenjualan As New wpfWin_InputInvoicePenjualan
    Public win_InputInvoicePenjualan_Alt As New wpfWin_InputInvoicePenjualan_Alt
    Public win_InputReturPenjualan As New wpfWin_InputReturPenjualan
    Public win_InputPenjualanEceran As New wpfWin_InputPenjualanEceran

    '═══════════════════════════════════════════════════════════════════════════
    ' TRANSAKSI & JURNAL
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputTransaksi As New wpfWin_InputTransaksi
    Public win_InputJurnal As New wpfWin_InputJurnal
    Public win_InputJurnalAdjusmentForex As New wpfWin_InputJurnalAdjusmentForex
    Public win_JurnalVoucher As New wpfWin_JurnalVoucher
    Public win_ImportJurnal As New wpfWin_ImportJurnal
    Public win_VerifikasiDataJurnal As New wpfWin_VerifikasiDataJurnal

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU BESAR
    '═══════════════════════════════════════════════════════════════════════════
    Public win_BukuBesarPembantu As New wpfWin_BukuBesarPembantu

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - BANK / CASH
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
    Public win_InputBuktiPenerimaan As New wpfWin_InputBuktiPenerimaan
    Public win_InputBundelPengajuanPengeluaranBankCash As New wpfWin_InputBundelPengajuanPengeluaranBankCash
    Public win_InputJumlahBankCashInOut As New wpfWin_InputJumlahBankCashInOut
    Public win_InputPemindahbukuan As New wpfWin_InputPemindahbukuan

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - HUTANG PIUTANG (NON-USAHA)
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputHutangPiutangAfiliasi As New wpfWin_InputHutangPiutangAfiliasi
    Public win_InputJadwalAngsuranAfiliasi As New wpfWin_InputJadwalAngsuranAfiliasi()
    Public win_InputHutangPiutangPihakKetiga As New wpfWin_InputHutangPiutangPihakKetiga
    Public win_InputJadwalAngsuranPihakKetiga As New wpfWin_InputJadwalAngsuranPihakKetiga()
    Public win_InputHutangPiutangKaryawan As New wpfWin_InputHutangPiutangKaryawan
    Public win_InputHutangPiutangPemegangSaham As New wpfWin_InputHutangPiutangPemegangSaham
    Public win_InputHutangPiutangDividen As New wpfWin_InputHutangPiutangDividen
    Public win_InputHutangBankLeasing As New wpfWin_InputHutangBankLeasing
    Public win_InputJadwalAngsuranHutangBankLeasing As New wpfWin_InputJadwalAngsuranHutangBankLeasing

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - DEPOSIT OPERASIONAL
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputDepositOperasional As New wpfWin_InputDepositOperasional
    Public win_InputProduk_DepositOperasional As New wpfWin_InputProduk_DepositOperasional

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - AKTIVA LAINNYA
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputAktivaLainnya As New wpfWin_InputAktivaLainnya

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - PAJAK
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputHutangPajak As New wpfWin_InputHutangPajak
    Public win_InputHutangPPhPasal21_GajiDanPesangon As New wpfWin_InputHutangPPhPasal21_GajiDanPesangon
    Public win_InputHutangPPhPasal25 As New wpfWin_InputHutangPPhPasal25
    Public win_InputKetetapanPajak As New wpfWin_InputKetetapanPajak
    Public win_InputLaporPajak As New wpfWin_InputLaporPajak
    Public win_BayarPajakDengan As New wpfWin_BayarPajakDengan
    Public win_InputBuktiPotongPPh As wpfWin_InputBuktiPotongPPh

    '═══════════════════════════════════════════════════════════════════════════
    ' BUKU PENGAWASAN - GAJI
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputGaji As New wpfWin_InputGaji
    Public win_InputTagihanTurunanGaji As New wpfWin_InputTagihanTurunanGaji

    '═══════════════════════════════════════════════════════════════════════════
    ' MANAJEMEN ASSET
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputDataAsset As New wpfWin_InputDataAsset
    Public win_InputDisposalAssetTetap As New wpfWin_InputDisposalAssetTetap
    Public win_JualAsset As New wpfWin_JualAsset
    Public win_InputAmortisasiBiaya As New wpfWin_InputAmortisasiBiaya
    Public win_ProgressImportDataAsset As New wpfWin_ProgressImportDataAsset

    '═══════════════════════════════════════════════════════════════════════════
    ' STOCK OPNAME
    '═══════════════════════════════════════════════════════════════════════════
    Public win_InputStockOpname As New wpfWin_InputStockOpname

    '═══════════════════════════════════════════════════════════════════════════
    ' BACKUP & RESTORE
    '═══════════════════════════════════════════════════════════════════════════
    Public win_BackupData As New wpfWin_BackupData
    Public win_PulihkanData As New wpfWin_PulihkanData
    Public win_KloningData As New wpfWin_KloningData

    '═══════════════════════════════════════════════════════════════════════════
    ' APP DEVELOPER
    '═══════════════════════════════════════════════════════════════════════════
    Public win_Updater As New wpfWin_Updater
    Public win_PenerbitInstaller As New wpfWin_PenerbitInstaller
    Public win_PenerbitUpdater As New wpfWin_PenerbitUpdater
    Public win_InputProdukApp As New wpfWin_InputProdukApp

    '═══════════════════════════════════════════════════════════════════════════
    ' EXPORT & PREVIEW
    '═══════════════════════════════════════════════════════════════════════════
    Public win_Pratinjau As New wpfWin_Pratinjau
    Public win_ProgressExport_EXCEL As New wpfWin_ProgresExport_EXCEL

End Module
