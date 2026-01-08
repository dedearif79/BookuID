<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_BOOKU
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        mnu_MenuUtama = New MenuStrip()
        mnu_File = New ToolStripMenuItem()
        mnu_Pengaturan = New ToolStripMenuItem()
        mnu_Database = New ToolStripMenuItem()
        mnu_Database_Cadangkan = New ToolStripMenuItem()
        mnu_Database_Pulihkan = New ToolStripMenuItem()
        mnu_Database_Kloning = New ToolStripMenuItem()
        mnu_Keluar = New ToolStripMenuItem()
        mnu_Data = New ToolStripMenuItem()
        mnu_CompanyProfile = New ToolStripMenuItem()
        mnu_DataAwal = New ToolStripMenuItem()
        mnu_DataAwal_Hutang = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_NonAfiliasi = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_Afiliasi = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_Impor = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_Impor_USD = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_Impor_AUD = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_Impor_JPY = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_Impor_CNY = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_Impor_EUR = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_Impor_SGD = New ToolStripMenuItem()
        mnu_DataAwal_HutangUsaha_BAK = New ToolStripMenuItem()
        mnu_DataAwal_HutangBank = New ToolStripMenuItem()
        mnu_DataAwal_HutangLeasing = New ToolStripMenuItem()
        mnu_DataAwal_HutangPihakKetiga = New ToolStripMenuItem()
        mnu_DataAwal_HutangAfiliasi = New ToolStripMenuItem()
        mnu_DataAwal_HutangKaryawan = New ToolStripMenuItem()
        mnu_DataAwal_HutangPemegangSaham = New ToolStripMenuItem()
        mnu_DataAwal_HutangPajak = New ToolStripMenuItem()
        mnu_DataAwal_HutangPPhPasal21 = New ToolStripMenuItem()
        mnu_DataAwal_HutangPPhPasal23 = New ToolStripMenuItem()
        mnu_DataAwal_HutangPPhPasal42 = New ToolStripMenuItem()
        mnu_DataAwal_HutangPPhPasal25 = New ToolStripMenuItem()
        mnu_DataAwal_HutangPPhPasal26 = New ToolStripMenuItem()
        mnu_DataAwal_HutangPPhPasal29 = New ToolStripMenuItem()
        mnu_DataAwal_HutangPPN = New ToolStripMenuItem()
        mnu_DataAwal_HutangKetetapanPajak = New ToolStripMenuItem()
        mnu_DataAwal_Gaji = New ToolStripMenuItem()
        mnu_DataAwal_HutangGaji = New ToolStripMenuItem()
        mnu_DataAwal_HutangBPJSKesehatan = New ToolStripMenuItem()
        mnu_DataAwal_HutangBPJSKetenagakerjaan = New ToolStripMenuItem()
        mnu_DataAwal_HutangKoperasiKaryawan = New ToolStripMenuItem()
        mnu_DataAwal_HutangSerikat = New ToolStripMenuItem()
        mnu_DataAwal_Piutang = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_NonAfiliasi = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Afiliasi = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Ekspor = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Ekspor_USD = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Ekspor_AUD = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Ekspor_JPY = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Ekspor_CNY = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Ekspor_EUR = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Ekspor_SGD = New ToolStripMenuItem()
        mnu_DataAwal_PiutangPihakKetiga = New ToolStripMenuItem()
        mnu_DataAwal_PiutangAfiliasi = New ToolStripMenuItem()
        mnu_DataAwal_PiutangKaryawan = New ToolStripMenuItem()
        mnu_DataAwal_PiutangPemegangSaham = New ToolStripMenuItem()
        mnu_DataAwal_DepositOperasional = New ToolStripMenuItem()
        mnu_DataAwal_Asset = New ToolStripMenuItem()
        mnu_DataAwal_AmortisasiBiaya = New ToolStripMenuItem()
        mnu_DataAwal_AssetTetap = New ToolStripMenuItem()
        mnu_DataUser = New ToolStripMenuItem()
        mnu_DataCOA = New ToolStripMenuItem()
        mnu_DataMitra = New ToolStripMenuItem()
        mnu_DataLawanTransaksi = New ToolStripMenuItem()
        mnu_DataKaryawan = New ToolStripMenuItem()
        mnu_DaftarPemegangSaham = New ToolStripMenuItem()
        mnu_DataPemegangSaham = New ToolStripMenuItem()
        mnu_DataProject = New ToolStripMenuItem()
        mnu_TahunBuku = New ToolStripMenuItem()
        mnu_BuatBukuBaru = New ToolStripMenuItem()
        mnu_GantiTahunBuku = New ToolStripMenuItem()
        mnu_TutupBuku = New ToolStripMenuItem()
        KursToolStripMenuItem = New ToolStripMenuItem()
        mnu_Transaksi = New ToolStripMenuItem()
        mnu_PerekamanData = New ToolStripMenuItem()
        mnu_InputPembelian = New ToolStripMenuItem()
        mnu_InputPenjualan = New ToolStripMenuItem()
        mnu_InputReturPembelian = New ToolStripMenuItem()
        mnu_InputReturPenjualan = New ToolStripMenuItem()
        mnu_PencatatanTransaksiBankOtomatis = New ToolStripMenuItem()
        mnu_PenghasilanBunga = New ToolStripMenuItem()
        mnu_PPhAtasBunga = New ToolStripMenuItem()
        mnu_BiayaAdministrasi = New ToolStripMenuItem()
        mnu_PerekamanDataLainnya = New ToolStripMenuItem()
        mnu_TransaksiIN = New ToolStripMenuItem()
        mnu_TransaksiOUT = New ToolStripMenuItem()
        mnu_Adjusment = New ToolStripMenuItem()
        mnu_Adjusment_BiayaPenyusutanAsset = New ToolStripMenuItem()
        mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka = New ToolStripMenuItem()
        mnu_Adjusment_PenghapusanPiutang = New ToolStripMenuItem()
        mnu_Adjusment_SelisihKurs = New ToolStripMenuItem()
        mnu_Adjusment_SelisihPencatatan = New ToolStripMenuItem()
        mnu_Adjusment_HPP = New ToolStripMenuItem()
        mnu_Adjusment_HPP_PemakaianBahanPenolong = New ToolStripMenuItem()
        mnu_Adjusment_HPP_PemakaianBahanBaku = New ToolStripMenuItem()
        mnu_Adjusment_HPP_BiayaBahanBaku = New ToolStripMenuItem()
        mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung = New ToolStripMenuItem()
        mnu_Adjusment_HPP_BiayaOverheadPabrik = New ToolStripMenuItem()
        mnu_Adjusment_HPP_BiayaProduksi = New ToolStripMenuItem()
        mnu_Adjusment_HPP_HargaPokokProduksi = New ToolStripMenuItem()
        mnu_Adjusment_HPP_HargaPokokPenjualan = New ToolStripMenuItem()
        mnu_AdjusmentLainnya = New ToolStripMenuItem()
        mnu_Pemindahbukuan = New ToolStripMenuItem()
        mnu_Pembelian = New ToolStripMenuItem()
        mnu_PO_Pembelian = New ToolStripMenuItem()
        mnu_PO_Pembelian_Lokal = New ToolStripMenuItem()
        mnu_POPembelian_Lokal_Barang = New ToolStripMenuItem()
        mnu_POPembelian_Lokal_Jasa = New ToolStripMenuItem()
        mnu_POPembelian_Lokal_BarangDanJasa = New ToolStripMenuItem()
        mnu_POPembelian_Lokal_JasaKonstruksi = New ToolStripMenuItem()
        mnu_POPembelian_Semua = New ToolStripMenuItem()
        mnu_PO_Pembelian_Impor = New ToolStripMenuItem()
        mnu_PO_Pembelian_Impor_Barang = New ToolStripMenuItem()
        mnu_PO_Pembelian_Impor_Jasa = New ToolStripMenuItem()
        mnu_PO_Pembelian_Impor_Semua = New ToolStripMenuItem()
        mnu_SuratJalanPembelian = New ToolStripMenuItem()
        mnu_BASTPembelian = New ToolStripMenuItem()
        mnu_InvoicePembelian = New ToolStripMenuItem()
        mnu_InvoicePembelian_DenganPO = New ToolStripMenuItem()
        mnu_InvoicePembelian_DenganPO_Lokal = New ToolStripMenuItem()
        mnu_InvoicePembelian_DenganPO_Lokal_Rutin = New ToolStripMenuItem()
        mnu_InvoicePembelian_DenganPO_Lokal_Termin = New ToolStripMenuItem()
        mnu_InvoicePembelian_DenganPO_Impor = New ToolStripMenuItem()
        mnu_InvoicePembelian_DenganPO_Impor_Rutin = New ToolStripMenuItem()
        mnu_InvoicePembelian_DenganPO_Impor_Termin = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_Lokal = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_Lokal_Barang = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_Lokal_Jasa = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_Impor = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_Impor_Barang = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_Impor_Jasa = New ToolStripMenuItem()
        mnu_InvoicePembelian_TanpaPO_LokalMUA = New ToolStripMenuItem()
        mnu_BukuPembelian = New ToolStripMenuItem()
        mnu_BukuPembelian_Lokal = New ToolStripMenuItem()
        mnu_BukuPembelian_Impor = New ToolStripMenuItem()
        mnu_ReturPembelian = New ToolStripMenuItem()
        mnu_Penjualan = New ToolStripMenuItem()
        mnu_BukuPengawasanPOPenjualan = New ToolStripMenuItem()
        mnu_POPenjualan_Lokal = New ToolStripMenuItem()
        mnu_POPenjualan_Lokal_Barang = New ToolStripMenuItem()
        mnu_POPenjualan_Lokal_Jasa = New ToolStripMenuItem()
        mnu_POPenjualan_Lokal_BarangDanJasa = New ToolStripMenuItem()
        mnu_POPenjualan_Lokal_JasaKonstruksi = New ToolStripMenuItem()
        mnu_POPenjualan_Lokal_Semua = New ToolStripMenuItem()
        mnu_POPenjualan_Ekspor = New ToolStripMenuItem()
        mnu_SuratJalanPenjualan = New ToolStripMenuItem()
        mnu_BASTPenjualan = New ToolStripMenuItem()
        mnu_InvoicePenjualan = New ToolStripMenuItem()
        mnu_InvoicePenjualan_DenganPO = New ToolStripMenuItem()
        mnu_InvoicePenjualan_DenganPO_Lokal = New ToolStripMenuItem()
        mnu_InvoicePenjualan_DenganPO_Lokal_Rutin = New ToolStripMenuItem()
        mnu_InvoicePenjualan_DenganPO_Lokal_Termin = New ToolStripMenuItem()
        mnu_InvoicePenjualan_DenganPO_Ekspor = New ToolStripMenuItem()
        mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin = New ToolStripMenuItem()
        mnu_InvoicePenjualan_DenganPO_Ekspor_Termin = New ToolStripMenuItem()
        mnu_InvoicePenjualan_TanpaPO = New ToolStripMenuItem()
        mnu_InvoicePenjualan_TanpaPO_Lokal = New ToolStripMenuItem()
        mnu_InvoicePenjualan_TanpaPO_Lokal_Barang = New ToolStripMenuItem()
        mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa = New ToolStripMenuItem()
        mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa = New ToolStripMenuItem()
        mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi = New ToolStripMenuItem()
        mnu_InvoicePenjualan_TanpaPO_Ekspor = New ToolStripMenuItem()
        mnu_InvoicePenjualan_TanpaPO_Asset = New ToolStripMenuItem()
        mnu_BukuPenjualan = New ToolStripMenuItem()
        mnu_BukuPenjualan_Lokal = New ToolStripMenuItem()
        mnu_BukuPenjualan_Ekspor = New ToolStripMenuItem()
        mnu_BukuPenjualanEceran = New ToolStripMenuItem()
        mnu_BukuPengawasanReturPenjualan = New ToolStripMenuItem()
        mnu_BukuPengawasan = New ToolStripMenuItem()
        mnu_BukuBankCash = New ToolStripMenuItem()
        mnu_BukuBank = New ToolStripMenuItem()
        mnu_BukuKas = New ToolStripMenuItem()
        mnu_BukuPettyCash = New ToolStripMenuItem()
        mnu_BukuCashAdvance = New ToolStripMenuItem()
        mnu_BukuBankGaransi = New ToolStripMenuItem()
        mnu_BukuPengawasanGaji_Induk = New ToolStripMenuItem()
        mnu_BukuPengawasanGaji = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangBPJSKesehatan = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangBPJSKetenagakerjaan = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangKoperasiKaryawan = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangSerikat = New ToolStripMenuItem()
        mnu_BukuPengawasanHutang = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Lokal = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_NonAfiliasi = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Afiliasi = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Semua = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Impor = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Impor_USD = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Impor_AUD = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Impor_JPY = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Impor_CNY = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Impor_EUR = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Impor_SGD = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_Impor_GBP = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangUsaha_BAK = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangBank = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangLeasing = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPihakKetiga = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangAfiliasi = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangKaryawan = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPemegangSaham = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangDividen = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangLainnya = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutang = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Lokal = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_NonAfiliasi = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Afiliasi = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Semua = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Ekspor = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Ekspor_USD = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangPihakKetiga = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangAfiliasi = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangKaryawan = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangPemegangSaham = New ToolStripMenuItem()
        mnu_BukuPengawasanDepositOperasional = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangDividen = New ToolStripMenuItem()
        mnu_BukuPengawasanPiutangLainnya = New ToolStripMenuItem()
        mnu_BukuPengawasanBuktiPenerimaanBankCash = New ToolStripMenuItem()
        mnu_BukuPengawasanBuktiPengeluaranBankCash = New ToolStripMenuItem()
        mnu_BukuPengawasanPemindabukuan = New ToolStripMenuItem()
        mnu_BukuPengawasanAktivaLainnya = New ToolStripMenuItem()
        mnu_Pengajuan = New ToolStripMenuItem()
        mnu_PengajuanPembayaranPembelianTunai = New ToolStripMenuItem()
        mnu_PengajuanPembayaranHutangUsaha = New ToolStripMenuItem()
        mnu_PengajuanPembayaranHutangPajak = New ToolStripMenuItem()
        mnu_PengajuanPembayaranHutangBank = New ToolStripMenuItem()
        mnu_PengajuanPembayaranHutangLeasing = New ToolStripMenuItem()
        mnu_PengajuanPembayaranHutangAfiliasi = New ToolStripMenuItem()
        mnu_PengajuanPembayaranHutangLainnya = New ToolStripMenuItem()
        mnu_PengajuanPembayaranKasbon = New ToolStripMenuItem()
        mnu_PengajuanPembayaranInvestasi = New ToolStripMenuItem()
        mnu_PengajuanPemindahbukuan = New ToolStripMenuItem()
        mnu_PengajuanLainnya = New ToolStripMenuItem()
        mnu_PengajuanPO = New ToolStripMenuItem()
        mnu_StockOpname = New ToolStripMenuItem()
        mnu_StockOpname_BahanPenolong = New ToolStripMenuItem()
        mnu_StockOpname_BahanBaku = New ToolStripMenuItem()
        mnu_StockOpname_BarangDalamProses = New ToolStripMenuItem()
        mnu_StockOpname_BarangDalamProses_CekFisik = New ToolStripMenuItem()
        mnu_StockOpname_BarangDalamProses_TarikanData = New ToolStripMenuItem()
        mnu_StockOpname_BarangJadi = New ToolStripMenuItem()
        mnu_Akuntansi = New ToolStripMenuItem()
        mnu_Jurnal = New ToolStripMenuItem()
        mnu_JurnalUmum = New ToolStripMenuItem()
        mnu_JurnalAdjusment = New ToolStripMenuItem()
        mnu_JurnalAdjusment_Penyusutan = New ToolStripMenuItem()
        mnu_JurnalAdjusment_Amortisasi = New ToolStripMenuItem()
        mnu_JurnalAdjusment_Forex = New ToolStripMenuItem()
        mnu_JurnalAdjusment_HPP = New ToolStripMenuItem()
        mnu_BukuBesar = New ToolStripMenuItem()
        mnu_Laporan = New ToolStripMenuItem()
        mnu_TrialBalance = New ToolStripMenuItem()
        mnu_NeracaLajur = New ToolStripMenuItem()
        mnu_LaporanKeuangan = New ToolStripMenuItem()
        mnu_LaporanHPP = New ToolStripMenuItem()
        mnu_LabaRugi = New ToolStripMenuItem()
        mnu_LabaRugi_Bulanan = New ToolStripMenuItem()
        mnu_LabaRugi_Tahunan = New ToolStripMenuItem()
        mnu_Neraca = New ToolStripMenuItem()
        mnu_Neraca_Bulanan = New ToolStripMenuItem()
        mnu_Neraca_Tahunan = New ToolStripMenuItem()
        mnu_LaporanAktivitasTransaksi = New ToolStripMenuItem()
        mnu_ManajemenAsset = New ToolStripMenuItem()
        mnu_ManajemenAmortisasi = New ToolStripMenuItem()
        mnu_ManajemenAmortisasiBiaya = New ToolStripMenuItem()
        mnu_ManajemenAmortisasiAssetTidakBerwujud = New ToolStripMenuItem()
        mnu_ManajemenAssetTetap = New ToolStripMenuItem()
        mnu_DaftarPenyusutanAssetTetap = New ToolStripMenuItem()
        mnu_BukuPenjualanAssetTetap = New ToolStripMenuItem()
        mnu_BukuDisposalAssetTetap = New ToolStripMenuItem()
        mnu_Pajak = New ToolStripMenuItem()
        mnu_ProfilPajakPerusahaan = New ToolStripMenuItem()
        mnu_PerhitunganPajakPajakBulanan = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPajak = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal21 = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal22 = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal22_Lokal = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal22_Impor = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal23 = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal42 = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal25 = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal26 = New ToolStripMenuItem()
        mnu_BukuPengawasanHutangPPhPasal29 = New ToolStripMenuItem()
        mnu_PPN = New ToolStripMenuItem()
        mnu_KetetapanPajak = New ToolStripMenuItem()
        mnu_PajakImpor = New ToolStripMenuItem()
        mnu_InputDokumenPerpajakan = New ToolStripMenuItem()
        mnu_InputBuktiPBk = New ToolStripMenuItem()
        mnu_InputKetetapanPajak = New ToolStripMenuItem()
        mnu_PerhitunganEqualisasiPajakPajakTahunan = New ToolStripMenuItem()
        mnu_BukuPengawasanBuktiPotongPPh_Paid = New ToolStripMenuItem()
        mnu_BukuPengawasanBuktiPotongPPh_Prepaid = New ToolStripMenuItem()
        mnu_User = New ToolStripMenuItem()
        mnu_SwitchUser = New ToolStripMenuItem()
        mnu_GantiPassword = New ToolStripMenuItem()
        mnu_GantiPeran = New ToolStripMenuItem()
        mnu_PeranOperator = New ToolStripMenuItem()
        mnu_PeranManager = New ToolStripMenuItem()
        mnu_PeranDirektur = New ToolStripMenuItem()
        mnu_PeranTimIT = New ToolStripMenuItem()
        mnu_PeranAppDeveloper = New ToolStripMenuItem()
        mnu_Log = New ToolStripMenuItem()
        mnu_Jendela = New ToolStripMenuItem()
        mnu_Jendela_TutupSemua = New ToolStripMenuItem()
        mnu_Tentang = New ToolStripMenuItem()
        mnu_Help = New ToolStripMenuItem()
        mnu_Registrasi = New ToolStripMenuItem()
        mnu_Notifikasi = New ToolStripMenuItem()
        mnu_TechnicalSupport = New ToolStripMenuItem()
        mnu_PhpMyAdmin = New ToolStripMenuItem()
        mnu_AppDeveloper = New ToolStripMenuItem()
        mnu_ManajemenAplikasi = New ToolStripMenuItem()
        mnu_ManajemenClient = New ToolStripMenuItem()
        mnu_ManajemenKurs = New ToolStripMenuItem()
        mnu_DataProduk = New ToolStripMenuItem()
        mnu_DataPerangkat = New ToolStripMenuItem()
        mnu_DataVoucher = New ToolStripMenuItem()
        mnu_TabPokok = New ToolStripMenuItem()
        mnu_TryApp = New ToolStripMenuItem()
        StatusStrip1 = New StatusStrip()
        tls_UserAktif = New ToolStripStatusLabel()
        pnl_Notifikasi = New Panel()
        dgv_Notifikasi = New DataGridView()
        Nomor_ID = New DataGridViewTextBoxColumn()
        Jenis_Notifikasi = New DataGridViewTextBoxColumn()
        Waktu_ = New DataGridViewTextBoxColumn()
        Konten_ = New DataGridViewTextBoxColumn()
        Halaman_Target = New DataGridViewTextBoxColumn()
        Pesan_Eksekusi = New DataGridViewTextBoxColumn()
        Status_Dibaca = New DataGridViewTextBoxColumn()
        Status_Dieksekusi = New DataGridViewTextBoxColumn()
        mnu_DataAwal_HutangUsaha_Impor_GBP = New ToolStripMenuItem()
        mnu_DataAwal_PiutangUsaha_Ekspor_GBP = New ToolStripMenuItem()
        mnu_MenuUtama.SuspendLayout()
        StatusStrip1.SuspendLayout()
        pnl_Notifikasi.SuspendLayout()
        CType(dgv_Notifikasi, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' mnu_MenuUtama
        ' 
        mnu_MenuUtama.ImageScalingSize = New Size(20, 20)
        mnu_MenuUtama.Items.AddRange(New ToolStripItem() {mnu_File, mnu_Data, mnu_Transaksi, mnu_Pembelian, mnu_Penjualan, mnu_BukuPengawasan, mnu_Pengajuan, mnu_StockOpname, mnu_Akuntansi, mnu_ManajemenAsset, mnu_Pajak, mnu_User, mnu_Jendela, mnu_Tentang, mnu_Help, mnu_Registrasi, mnu_Notifikasi, mnu_TechnicalSupport, mnu_AppDeveloper})
        mnu_MenuUtama.Location = New Point(0, 0)
        mnu_MenuUtama.Name = "mnu_MenuUtama"
        mnu_MenuUtama.Padding = New Padding(6, 3, 0, 3)
        mnu_MenuUtama.Size = New Size(1717, 30)
        mnu_MenuUtama.TabIndex = 1
        mnu_MenuUtama.Text = "MenuStrip1"
        ' 
        ' mnu_File
        ' 
        mnu_File.DropDownItems.AddRange(New ToolStripItem() {mnu_Pengaturan, mnu_Database, mnu_Keluar})
        mnu_File.Name = "mnu_File"
        mnu_File.Size = New Size(46, 24)
        mnu_File.Text = "File"
        ' 
        ' mnu_Pengaturan
        ' 
        mnu_Pengaturan.Name = "mnu_Pengaturan"
        mnu_Pengaturan.Size = New Size(166, 26)
        mnu_Pengaturan.Text = "Pengaturan"
        ' 
        ' mnu_Database
        ' 
        mnu_Database.DropDownItems.AddRange(New ToolStripItem() {mnu_Database_Cadangkan, mnu_Database_Pulihkan, mnu_Database_Kloning})
        mnu_Database.Name = "mnu_Database"
        mnu_Database.Size = New Size(166, 26)
        mnu_Database.Text = "Database"
        ' 
        ' mnu_Database_Cadangkan
        ' 
        mnu_Database_Cadangkan.Name = "mnu_Database_Cadangkan"
        mnu_Database_Cadangkan.Size = New Size(166, 26)
        mnu_Database_Cadangkan.Text = "Cadangkan"
        ' 
        ' mnu_Database_Pulihkan
        ' 
        mnu_Database_Pulihkan.Name = "mnu_Database_Pulihkan"
        mnu_Database_Pulihkan.Size = New Size(166, 26)
        mnu_Database_Pulihkan.Text = "Pulihkan"
        ' 
        ' mnu_Database_Kloning
        ' 
        mnu_Database_Kloning.Name = "mnu_Database_Kloning"
        mnu_Database_Kloning.Size = New Size(166, 26)
        mnu_Database_Kloning.Text = "Kloning"
        ' 
        ' mnu_Keluar
        ' 
        mnu_Keluar.Name = "mnu_Keluar"
        mnu_Keluar.Size = New Size(166, 26)
        mnu_Keluar.Text = "Keluar"
        ' 
        ' mnu_Data
        ' 
        mnu_Data.DropDownItems.AddRange(New ToolStripItem() {mnu_CompanyProfile, mnu_DataAwal, mnu_DataUser, mnu_DataCOA, mnu_DataMitra, mnu_DataLawanTransaksi, mnu_DataKaryawan, mnu_DaftarPemegangSaham, mnu_DataPemegangSaham, mnu_DataProject, mnu_TahunBuku, KursToolStripMenuItem})
        mnu_Data.Name = "mnu_Data"
        mnu_Data.Size = New Size(55, 24)
        mnu_Data.Text = "Data"
        ' 
        ' mnu_CompanyProfile
        ' 
        mnu_CompanyProfile.Name = "mnu_CompanyProfile"
        mnu_CompanyProfile.Size = New Size(257, 26)
        mnu_CompanyProfile.Text = "Company Profile"
        ' 
        ' mnu_DataAwal
        ' 
        mnu_DataAwal.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_Hutang, mnu_DataAwal_Piutang, mnu_DataAwal_Asset})
        mnu_DataAwal.Name = "mnu_DataAwal"
        mnu_DataAwal.Size = New Size(257, 26)
        mnu_DataAwal.Text = "Data Awal"
        ' 
        ' mnu_DataAwal_Hutang
        ' 
        mnu_DataAwal_Hutang.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_HutangUsaha, mnu_DataAwal_HutangUsaha_BAK, mnu_DataAwal_HutangBank, mnu_DataAwal_HutangLeasing, mnu_DataAwal_HutangPihakKetiga, mnu_DataAwal_HutangAfiliasi, mnu_DataAwal_HutangKaryawan, mnu_DataAwal_HutangPemegangSaham, mnu_DataAwal_HutangPajak, mnu_DataAwal_Gaji})
        mnu_DataAwal_Hutang.Name = "mnu_DataAwal_Hutang"
        mnu_DataAwal_Hutang.Size = New Size(224, 26)
        mnu_DataAwal_Hutang.Text = "Hutang"
        ' 
        ' mnu_DataAwal_HutangUsaha
        ' 
        mnu_DataAwal_HutangUsaha.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_HutangUsaha_NonAfiliasi, mnu_DataAwal_HutangUsaha_Afiliasi, mnu_DataAwal_HutangUsaha_Impor})
        mnu_DataAwal_HutangUsaha.Name = "mnu_DataAwal_HutangUsaha"
        mnu_DataAwal_HutangUsaha.Size = New Size(264, 26)
        mnu_DataAwal_HutangUsaha.Text = "Hutang Usaha"
        ' 
        ' mnu_DataAwal_HutangUsaha_NonAfiliasi
        ' 
        mnu_DataAwal_HutangUsaha_NonAfiliasi.Name = "mnu_DataAwal_HutangUsaha_NonAfiliasi"
        mnu_DataAwal_HutangUsaha_NonAfiliasi.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_NonAfiliasi.Text = "Non Afiliasi"
        ' 
        ' mnu_DataAwal_HutangUsaha_Afiliasi
        ' 
        mnu_DataAwal_HutangUsaha_Afiliasi.Name = "mnu_DataAwal_HutangUsaha_Afiliasi"
        mnu_DataAwal_HutangUsaha_Afiliasi.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Afiliasi.Text = "Afiliasi"
        ' 
        ' mnu_DataAwal_HutangUsaha_Impor
        ' 
        mnu_DataAwal_HutangUsaha_Impor.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_HutangUsaha_Impor_USD, mnu_DataAwal_HutangUsaha_Impor_AUD, mnu_DataAwal_HutangUsaha_Impor_JPY, mnu_DataAwal_HutangUsaha_Impor_CNY, mnu_DataAwal_HutangUsaha_Impor_EUR, mnu_DataAwal_HutangUsaha_Impor_SGD, mnu_DataAwal_HutangUsaha_Impor_GBP})
        mnu_DataAwal_HutangUsaha_Impor.Name = "mnu_DataAwal_HutangUsaha_Impor"
        mnu_DataAwal_HutangUsaha_Impor.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Impor.Text = "Impor"
        ' 
        ' mnu_DataAwal_HutangUsaha_Impor_USD
        ' 
        mnu_DataAwal_HutangUsaha_Impor_USD.Name = "mnu_DataAwal_HutangUsaha_Impor_USD"
        mnu_DataAwal_HutangUsaha_Impor_USD.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Impor_USD.Text = "USD"
        ' 
        ' mnu_DataAwal_HutangUsaha_Impor_AUD
        ' 
        mnu_DataAwal_HutangUsaha_Impor_AUD.Name = "mnu_DataAwal_HutangUsaha_Impor_AUD"
        mnu_DataAwal_HutangUsaha_Impor_AUD.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Impor_AUD.Text = "AUD"
        ' 
        ' mnu_DataAwal_HutangUsaha_Impor_JPY
        ' 
        mnu_DataAwal_HutangUsaha_Impor_JPY.Name = "mnu_DataAwal_HutangUsaha_Impor_JPY"
        mnu_DataAwal_HutangUsaha_Impor_JPY.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Impor_JPY.Text = "JPY"
        ' 
        ' mnu_DataAwal_HutangUsaha_Impor_CNY
        ' 
        mnu_DataAwal_HutangUsaha_Impor_CNY.Name = "mnu_DataAwal_HutangUsaha_Impor_CNY"
        mnu_DataAwal_HutangUsaha_Impor_CNY.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Impor_CNY.Text = "CNY"
        ' 
        ' mnu_DataAwal_HutangUsaha_Impor_EUR
        ' 
        mnu_DataAwal_HutangUsaha_Impor_EUR.Name = "mnu_DataAwal_HutangUsaha_Impor_EUR"
        mnu_DataAwal_HutangUsaha_Impor_EUR.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Impor_EUR.Text = "EUR"
        ' 
        ' mnu_DataAwal_HutangUsaha_Impor_SGD
        ' 
        mnu_DataAwal_HutangUsaha_Impor_SGD.Name = "mnu_DataAwal_HutangUsaha_Impor_SGD"
        mnu_DataAwal_HutangUsaha_Impor_SGD.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Impor_SGD.Text = "SGD"
        ' 
        ' mnu_DataAwal_HutangUsaha_BAK
        ' 
        mnu_DataAwal_HutangUsaha_BAK.Name = "mnu_DataAwal_HutangUsaha_BAK"
        mnu_DataAwal_HutangUsaha_BAK.Size = New Size(264, 26)
        mnu_DataAwal_HutangUsaha_BAK.Text = "Hutang Usaha (BackUp)"
        mnu_DataAwal_HutangUsaha_BAK.Visible = False
        ' 
        ' mnu_DataAwal_HutangBank
        ' 
        mnu_DataAwal_HutangBank.Name = "mnu_DataAwal_HutangBank"
        mnu_DataAwal_HutangBank.Size = New Size(264, 26)
        mnu_DataAwal_HutangBank.Text = "Hutang Bank"
        ' 
        ' mnu_DataAwal_HutangLeasing
        ' 
        mnu_DataAwal_HutangLeasing.Name = "mnu_DataAwal_HutangLeasing"
        mnu_DataAwal_HutangLeasing.Size = New Size(264, 26)
        mnu_DataAwal_HutangLeasing.Text = "Hutang Leasing"
        ' 
        ' mnu_DataAwal_HutangPihakKetiga
        ' 
        mnu_DataAwal_HutangPihakKetiga.Name = "mnu_DataAwal_HutangPihakKetiga"
        mnu_DataAwal_HutangPihakKetiga.Size = New Size(264, 26)
        mnu_DataAwal_HutangPihakKetiga.Text = "Hutang Pihak Ketiga"
        ' 
        ' mnu_DataAwal_HutangAfiliasi
        ' 
        mnu_DataAwal_HutangAfiliasi.Name = "mnu_DataAwal_HutangAfiliasi"
        mnu_DataAwal_HutangAfiliasi.Size = New Size(264, 26)
        mnu_DataAwal_HutangAfiliasi.Text = "Hutang Afiliasi"
        ' 
        ' mnu_DataAwal_HutangKaryawan
        ' 
        mnu_DataAwal_HutangKaryawan.Name = "mnu_DataAwal_HutangKaryawan"
        mnu_DataAwal_HutangKaryawan.Size = New Size(264, 26)
        mnu_DataAwal_HutangKaryawan.Text = "Hutang Karyawan"
        ' 
        ' mnu_DataAwal_HutangPemegangSaham
        ' 
        mnu_DataAwal_HutangPemegangSaham.Name = "mnu_DataAwal_HutangPemegangSaham"
        mnu_DataAwal_HutangPemegangSaham.Size = New Size(264, 26)
        mnu_DataAwal_HutangPemegangSaham.Text = "Hutang Pemegang Saham"
        ' 
        ' mnu_DataAwal_HutangPajak
        ' 
        mnu_DataAwal_HutangPajak.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_HutangPPhPasal21, mnu_DataAwal_HutangPPhPasal23, mnu_DataAwal_HutangPPhPasal42, mnu_DataAwal_HutangPPhPasal25, mnu_DataAwal_HutangPPhPasal26, mnu_DataAwal_HutangPPhPasal29, mnu_DataAwal_HutangPPN, mnu_DataAwal_HutangKetetapanPajak})
        mnu_DataAwal_HutangPajak.Name = "mnu_DataAwal_HutangPajak"
        mnu_DataAwal_HutangPajak.Size = New Size(264, 26)
        mnu_DataAwal_HutangPajak.Text = "Hutang Pajak"
        ' 
        ' mnu_DataAwal_HutangPPhPasal21
        ' 
        mnu_DataAwal_HutangPPhPasal21.Name = "mnu_DataAwal_HutangPPhPasal21"
        mnu_DataAwal_HutangPPhPasal21.Size = New Size(198, 26)
        mnu_DataAwal_HutangPPhPasal21.Text = "PPh Pasal 21"
        ' 
        ' mnu_DataAwal_HutangPPhPasal23
        ' 
        mnu_DataAwal_HutangPPhPasal23.Name = "mnu_DataAwal_HutangPPhPasal23"
        mnu_DataAwal_HutangPPhPasal23.Size = New Size(198, 26)
        mnu_DataAwal_HutangPPhPasal23.Text = "PPh Pasal 23"
        ' 
        ' mnu_DataAwal_HutangPPhPasal42
        ' 
        mnu_DataAwal_HutangPPhPasal42.Name = "mnu_DataAwal_HutangPPhPasal42"
        mnu_DataAwal_HutangPPhPasal42.Size = New Size(198, 26)
        mnu_DataAwal_HutangPPhPasal42.Text = "PPh Pasal 4 (2)"
        ' 
        ' mnu_DataAwal_HutangPPhPasal25
        ' 
        mnu_DataAwal_HutangPPhPasal25.Name = "mnu_DataAwal_HutangPPhPasal25"
        mnu_DataAwal_HutangPPhPasal25.Size = New Size(198, 26)
        mnu_DataAwal_HutangPPhPasal25.Text = "PPh Pasal 25"
        ' 
        ' mnu_DataAwal_HutangPPhPasal26
        ' 
        mnu_DataAwal_HutangPPhPasal26.Name = "mnu_DataAwal_HutangPPhPasal26"
        mnu_DataAwal_HutangPPhPasal26.Size = New Size(198, 26)
        mnu_DataAwal_HutangPPhPasal26.Text = "PPh Pasal 26"
        ' 
        ' mnu_DataAwal_HutangPPhPasal29
        ' 
        mnu_DataAwal_HutangPPhPasal29.Name = "mnu_DataAwal_HutangPPhPasal29"
        mnu_DataAwal_HutangPPhPasal29.Size = New Size(198, 26)
        mnu_DataAwal_HutangPPhPasal29.Text = "PPh Pasal 29"
        ' 
        ' mnu_DataAwal_HutangPPN
        ' 
        mnu_DataAwal_HutangPPN.Name = "mnu_DataAwal_HutangPPN"
        mnu_DataAwal_HutangPPN.Size = New Size(198, 26)
        mnu_DataAwal_HutangPPN.Text = "PPN"
        ' 
        ' mnu_DataAwal_HutangKetetapanPajak
        ' 
        mnu_DataAwal_HutangKetetapanPajak.Name = "mnu_DataAwal_HutangKetetapanPajak"
        mnu_DataAwal_HutangKetetapanPajak.Size = New Size(198, 26)
        mnu_DataAwal_HutangKetetapanPajak.Text = "Ketetapan Pajak"
        ' 
        ' mnu_DataAwal_Gaji
        ' 
        mnu_DataAwal_Gaji.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_HutangGaji, mnu_DataAwal_HutangBPJSKesehatan, mnu_DataAwal_HutangBPJSKetenagakerjaan, mnu_DataAwal_HutangKoperasiKaryawan, mnu_DataAwal_HutangSerikat})
        mnu_DataAwal_Gaji.Name = "mnu_DataAwal_Gaji"
        mnu_DataAwal_Gaji.Size = New Size(264, 26)
        mnu_DataAwal_Gaji.Text = "Gaji"
        ' 
        ' mnu_DataAwal_HutangGaji
        ' 
        mnu_DataAwal_HutangGaji.Name = "mnu_DataAwal_HutangGaji"
        mnu_DataAwal_HutangGaji.Size = New Size(271, 26)
        mnu_DataAwal_HutangGaji.Text = "Gaji"
        ' 
        ' mnu_DataAwal_HutangBPJSKesehatan
        ' 
        mnu_DataAwal_HutangBPJSKesehatan.Name = "mnu_DataAwal_HutangBPJSKesehatan"
        mnu_DataAwal_HutangBPJSKesehatan.Size = New Size(271, 26)
        mnu_DataAwal_HutangBPJSKesehatan.Text = "BPJS Kesehatan"
        ' 
        ' mnu_DataAwal_HutangBPJSKetenagakerjaan
        ' 
        mnu_DataAwal_HutangBPJSKetenagakerjaan.Name = "mnu_DataAwal_HutangBPJSKetenagakerjaan"
        mnu_DataAwal_HutangBPJSKetenagakerjaan.Size = New Size(271, 26)
        mnu_DataAwal_HutangBPJSKetenagakerjaan.Text = "BPJS Ketenagakerjaan"
        ' 
        ' mnu_DataAwal_HutangKoperasiKaryawan
        ' 
        mnu_DataAwal_HutangKoperasiKaryawan.Name = "mnu_DataAwal_HutangKoperasiKaryawan"
        mnu_DataAwal_HutangKoperasiKaryawan.Size = New Size(271, 26)
        mnu_DataAwal_HutangKoperasiKaryawan.Text = "Hutang Koperasi Karyawan"
        ' 
        ' mnu_DataAwal_HutangSerikat
        ' 
        mnu_DataAwal_HutangSerikat.Name = "mnu_DataAwal_HutangSerikat"
        mnu_DataAwal_HutangSerikat.Size = New Size(271, 26)
        mnu_DataAwal_HutangSerikat.Text = "Hutang Serikat"
        ' 
        ' mnu_DataAwal_Piutang
        ' 
        mnu_DataAwal_Piutang.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_PiutangUsaha, mnu_DataAwal_PiutangPihakKetiga, mnu_DataAwal_PiutangAfiliasi, mnu_DataAwal_PiutangKaryawan, mnu_DataAwal_PiutangPemegangSaham, mnu_DataAwal_DepositOperasional})
        mnu_DataAwal_Piutang.Name = "mnu_DataAwal_Piutang"
        mnu_DataAwal_Piutang.Size = New Size(224, 26)
        mnu_DataAwal_Piutang.Text = "Piutang"
        ' 
        ' mnu_DataAwal_PiutangUsaha
        ' 
        mnu_DataAwal_PiutangUsaha.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_PiutangUsaha_NonAfiliasi, mnu_DataAwal_PiutangUsaha_Afiliasi, mnu_DataAwal_PiutangUsaha_Ekspor})
        mnu_DataAwal_PiutangUsaha.Name = "mnu_DataAwal_PiutangUsaha"
        mnu_DataAwal_PiutangUsaha.Size = New Size(265, 26)
        mnu_DataAwal_PiutangUsaha.Text = "Piutang Usaha"
        ' 
        ' mnu_DataAwal_PiutangUsaha_NonAfiliasi
        ' 
        mnu_DataAwal_PiutangUsaha_NonAfiliasi.Name = "mnu_DataAwal_PiutangUsaha_NonAfiliasi"
        mnu_DataAwal_PiutangUsaha_NonAfiliasi.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_NonAfiliasi.Text = "Non Afiliasi"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Afiliasi
        ' 
        mnu_DataAwal_PiutangUsaha_Afiliasi.Name = "mnu_DataAwal_PiutangUsaha_Afiliasi"
        mnu_DataAwal_PiutangUsaha_Afiliasi.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Afiliasi.Text = "Afiliasi"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Ekspor
        ' 
        mnu_DataAwal_PiutangUsaha_Ekspor.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_PiutangUsaha_Ekspor_USD, mnu_DataAwal_PiutangUsaha_Ekspor_AUD, mnu_DataAwal_PiutangUsaha_Ekspor_JPY, mnu_DataAwal_PiutangUsaha_Ekspor_CNY, mnu_DataAwal_PiutangUsaha_Ekspor_EUR, mnu_DataAwal_PiutangUsaha_Ekspor_SGD, mnu_DataAwal_PiutangUsaha_Ekspor_GBP})
        mnu_DataAwal_PiutangUsaha_Ekspor.Name = "mnu_DataAwal_PiutangUsaha_Ekspor"
        mnu_DataAwal_PiutangUsaha_Ekspor.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Ekspor.Text = "Ekspor"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Ekspor_USD
        ' 
        mnu_DataAwal_PiutangUsaha_Ekspor_USD.Name = "mnu_DataAwal_PiutangUsaha_Ekspor_USD"
        mnu_DataAwal_PiutangUsaha_Ekspor_USD.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Ekspor_USD.Text = "USD"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Ekspor_AUD
        ' 
        mnu_DataAwal_PiutangUsaha_Ekspor_AUD.Name = "mnu_DataAwal_PiutangUsaha_Ekspor_AUD"
        mnu_DataAwal_PiutangUsaha_Ekspor_AUD.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Ekspor_AUD.Text = "AUD"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Ekspor_JPY
        ' 
        mnu_DataAwal_PiutangUsaha_Ekspor_JPY.Name = "mnu_DataAwal_PiutangUsaha_Ekspor_JPY"
        mnu_DataAwal_PiutangUsaha_Ekspor_JPY.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Ekspor_JPY.Text = "JPY"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Ekspor_CNY
        ' 
        mnu_DataAwal_PiutangUsaha_Ekspor_CNY.Name = "mnu_DataAwal_PiutangUsaha_Ekspor_CNY"
        mnu_DataAwal_PiutangUsaha_Ekspor_CNY.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Ekspor_CNY.Text = "CNY"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Ekspor_EUR
        ' 
        mnu_DataAwal_PiutangUsaha_Ekspor_EUR.Name = "mnu_DataAwal_PiutangUsaha_Ekspor_EUR"
        mnu_DataAwal_PiutangUsaha_Ekspor_EUR.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Ekspor_EUR.Text = "EUR"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Ekspor_SGD
        ' 
        mnu_DataAwal_PiutangUsaha_Ekspor_SGD.Name = "mnu_DataAwal_PiutangUsaha_Ekspor_SGD"
        mnu_DataAwal_PiutangUsaha_Ekspor_SGD.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Ekspor_SGD.Text = "SGD"
        ' 
        ' mnu_DataAwal_PiutangPihakKetiga
        ' 
        mnu_DataAwal_PiutangPihakKetiga.Name = "mnu_DataAwal_PiutangPihakKetiga"
        mnu_DataAwal_PiutangPihakKetiga.Size = New Size(265, 26)
        mnu_DataAwal_PiutangPihakKetiga.Text = "Piutang Pihak Ketiga"
        ' 
        ' mnu_DataAwal_PiutangAfiliasi
        ' 
        mnu_DataAwal_PiutangAfiliasi.Name = "mnu_DataAwal_PiutangAfiliasi"
        mnu_DataAwal_PiutangAfiliasi.Size = New Size(265, 26)
        mnu_DataAwal_PiutangAfiliasi.Text = "Piutang Afiliasi"
        ' 
        ' mnu_DataAwal_PiutangKaryawan
        ' 
        mnu_DataAwal_PiutangKaryawan.Name = "mnu_DataAwal_PiutangKaryawan"
        mnu_DataAwal_PiutangKaryawan.Size = New Size(265, 26)
        mnu_DataAwal_PiutangKaryawan.Text = "Piutang Karyawan"
        ' 
        ' mnu_DataAwal_PiutangPemegangSaham
        ' 
        mnu_DataAwal_PiutangPemegangSaham.Name = "mnu_DataAwal_PiutangPemegangSaham"
        mnu_DataAwal_PiutangPemegangSaham.Size = New Size(265, 26)
        mnu_DataAwal_PiutangPemegangSaham.Text = "Piutang Pemegang Saham"
        ' 
        ' mnu_DataAwal_DepositOperasional
        ' 
        mnu_DataAwal_DepositOperasional.Name = "mnu_DataAwal_DepositOperasional"
        mnu_DataAwal_DepositOperasional.Size = New Size(265, 26)
        mnu_DataAwal_DepositOperasional.Text = "Deposit Operasional"
        ' 
        ' mnu_DataAwal_Asset
        ' 
        mnu_DataAwal_Asset.DropDownItems.AddRange(New ToolStripItem() {mnu_DataAwal_AmortisasiBiaya, mnu_DataAwal_AssetTetap})
        mnu_DataAwal_Asset.Name = "mnu_DataAwal_Asset"
        mnu_DataAwal_Asset.Size = New Size(224, 26)
        mnu_DataAwal_Asset.Text = "Asset"
        ' 
        ' mnu_DataAwal_AmortisasiBiaya
        ' 
        mnu_DataAwal_AmortisasiBiaya.Name = "mnu_DataAwal_AmortisasiBiaya"
        mnu_DataAwal_AmortisasiBiaya.Size = New Size(202, 26)
        mnu_DataAwal_AmortisasiBiaya.Text = "Amortisasi Biaya"
        ' 
        ' mnu_DataAwal_AssetTetap
        ' 
        mnu_DataAwal_AssetTetap.Name = "mnu_DataAwal_AssetTetap"
        mnu_DataAwal_AssetTetap.Size = New Size(202, 26)
        mnu_DataAwal_AssetTetap.Text = "Asset Tetap"
        ' 
        ' mnu_DataUser
        ' 
        mnu_DataUser.Name = "mnu_DataUser"
        mnu_DataUser.Size = New Size(257, 26)
        mnu_DataUser.Text = "Data User"
        ' 
        ' mnu_DataCOA
        ' 
        mnu_DataCOA.Name = "mnu_DataCOA"
        mnu_DataCOA.Size = New Size(257, 26)
        mnu_DataCOA.Text = "Data COA"
        ' 
        ' mnu_DataMitra
        ' 
        mnu_DataMitra.Name = "mnu_DataMitra"
        mnu_DataMitra.Size = New Size(257, 26)
        mnu_DataMitra.Text = "Data Mitra"
        mnu_DataMitra.Visible = False
        ' 
        ' mnu_DataLawanTransaksi
        ' 
        mnu_DataLawanTransaksi.Name = "mnu_DataLawanTransaksi"
        mnu_DataLawanTransaksi.Size = New Size(257, 26)
        mnu_DataLawanTransaksi.Text = "Data Lawan Transaksi"
        ' 
        ' mnu_DataKaryawan
        ' 
        mnu_DataKaryawan.Name = "mnu_DataKaryawan"
        mnu_DataKaryawan.Size = New Size(257, 26)
        mnu_DataKaryawan.Text = "Data Karyawan"
        ' 
        ' mnu_DaftarPemegangSaham
        ' 
        mnu_DaftarPemegangSaham.Name = "mnu_DaftarPemegangSaham"
        mnu_DaftarPemegangSaham.Size = New Size(257, 26)
        mnu_DaftarPemegangSaham.Text = "Daftar Pemegang Saham"
        ' 
        ' mnu_DataPemegangSaham
        ' 
        mnu_DataPemegangSaham.Name = "mnu_DataPemegangSaham"
        mnu_DataPemegangSaham.Size = New Size(257, 26)
        mnu_DataPemegangSaham.Text = "Data Pemegang Saham"
        mnu_DataPemegangSaham.Visible = False
        ' 
        ' mnu_DataProject
        ' 
        mnu_DataProject.Name = "mnu_DataProject"
        mnu_DataProject.Size = New Size(257, 26)
        mnu_DataProject.Text = "Data Project"
        ' 
        ' mnu_TahunBuku
        ' 
        mnu_TahunBuku.DropDownItems.AddRange(New ToolStripItem() {mnu_BuatBukuBaru, mnu_GantiTahunBuku, mnu_TutupBuku})
        mnu_TahunBuku.Name = "mnu_TahunBuku"
        mnu_TahunBuku.Size = New Size(257, 26)
        mnu_TahunBuku.Text = "Tahun Buku"
        ' 
        ' mnu_BuatBukuBaru
        ' 
        mnu_BuatBukuBaru.Name = "mnu_BuatBukuBaru"
        mnu_BuatBukuBaru.Size = New Size(205, 26)
        mnu_BuatBukuBaru.Text = "Buat Buku Baru"
        ' 
        ' mnu_GantiTahunBuku
        ' 
        mnu_GantiTahunBuku.Name = "mnu_GantiTahunBuku"
        mnu_GantiTahunBuku.Size = New Size(205, 26)
        mnu_GantiTahunBuku.Text = "Ganti Tahun Buku"
        ' 
        ' mnu_TutupBuku
        ' 
        mnu_TutupBuku.Name = "mnu_TutupBuku"
        mnu_TutupBuku.Size = New Size(205, 26)
        mnu_TutupBuku.Text = "Tutup Buku"
        ' 
        ' KursToolStripMenuItem
        ' 
        KursToolStripMenuItem.Name = "KursToolStripMenuItem"
        KursToolStripMenuItem.Size = New Size(257, 26)
        KursToolStripMenuItem.Text = "Kurs"
        ' 
        ' mnu_Transaksi
        ' 
        mnu_Transaksi.DropDownItems.AddRange(New ToolStripItem() {mnu_PerekamanData, mnu_TransaksiIN, mnu_TransaksiOUT, mnu_Adjusment, mnu_Pemindahbukuan})
        mnu_Transaksi.Name = "mnu_Transaksi"
        mnu_Transaksi.Size = New Size(82, 24)
        mnu_Transaksi.Text = "Transaksi"
        ' 
        ' mnu_PerekamanData
        ' 
        mnu_PerekamanData.DropDownItems.AddRange(New ToolStripItem() {mnu_InputPembelian, mnu_InputPenjualan, mnu_InputReturPembelian, mnu_InputReturPenjualan, mnu_PencatatanTransaksiBankOtomatis, mnu_PerekamanDataLainnya})
        mnu_PerekamanData.Name = "mnu_PerekamanData"
        mnu_PerekamanData.Size = New Size(205, 26)
        mnu_PerekamanData.Text = "Perekaman Data"
        ' 
        ' mnu_InputPembelian
        ' 
        mnu_InputPembelian.Name = "mnu_InputPembelian"
        mnu_InputPembelian.Size = New Size(328, 26)
        mnu_InputPembelian.Text = "Pembelian"
        ' 
        ' mnu_InputPenjualan
        ' 
        mnu_InputPenjualan.Name = "mnu_InputPenjualan"
        mnu_InputPenjualan.Size = New Size(328, 26)
        mnu_InputPenjualan.Text = "Penjualan"
        ' 
        ' mnu_InputReturPembelian
        ' 
        mnu_InputReturPembelian.Name = "mnu_InputReturPembelian"
        mnu_InputReturPembelian.Size = New Size(328, 26)
        mnu_InputReturPembelian.Text = "Retur Pembelian"
        ' 
        ' mnu_InputReturPenjualan
        ' 
        mnu_InputReturPenjualan.Name = "mnu_InputReturPenjualan"
        mnu_InputReturPenjualan.Size = New Size(328, 26)
        mnu_InputReturPenjualan.Text = "Retur Penjualan"
        ' 
        ' mnu_PencatatanTransaksiBankOtomatis
        ' 
        mnu_PencatatanTransaksiBankOtomatis.DropDownItems.AddRange(New ToolStripItem() {mnu_PenghasilanBunga, mnu_PPhAtasBunga, mnu_BiayaAdministrasi})
        mnu_PencatatanTransaksiBankOtomatis.Name = "mnu_PencatatanTransaksiBankOtomatis"
        mnu_PencatatanTransaksiBankOtomatis.Size = New Size(328, 26)
        mnu_PencatatanTransaksiBankOtomatis.Text = "Pencatatan Transaksi Bank Otomatis"
        ' 
        ' mnu_PenghasilanBunga
        ' 
        mnu_PenghasilanBunga.Name = "mnu_PenghasilanBunga"
        mnu_PenghasilanBunga.Size = New Size(216, 26)
        mnu_PenghasilanBunga.Text = "Penghasilan Bunga"
        ' 
        ' mnu_PPhAtasBunga
        ' 
        mnu_PPhAtasBunga.Name = "mnu_PPhAtasBunga"
        mnu_PPhAtasBunga.Size = New Size(216, 26)
        mnu_PPhAtasBunga.Text = "PPh atas Bunga"
        ' 
        ' mnu_BiayaAdministrasi
        ' 
        mnu_BiayaAdministrasi.Name = "mnu_BiayaAdministrasi"
        mnu_BiayaAdministrasi.Size = New Size(216, 26)
        mnu_BiayaAdministrasi.Text = "Biaya Administrasi"
        ' 
        ' mnu_PerekamanDataLainnya
        ' 
        mnu_PerekamanDataLainnya.Name = "mnu_PerekamanDataLainnya"
        mnu_PerekamanDataLainnya.Size = New Size(328, 26)
        mnu_PerekamanDataLainnya.Text = "Perekaman Data Lainnya"
        ' 
        ' mnu_TransaksiIN
        ' 
        mnu_TransaksiIN.Name = "mnu_TransaksiIN"
        mnu_TransaksiIN.Size = New Size(205, 26)
        mnu_TransaksiIN.Text = "Transaksi IN"
        ' 
        ' mnu_TransaksiOUT
        ' 
        mnu_TransaksiOUT.Name = "mnu_TransaksiOUT"
        mnu_TransaksiOUT.Size = New Size(205, 26)
        mnu_TransaksiOUT.Text = "Transaksi OUT"
        ' 
        ' mnu_Adjusment
        ' 
        mnu_Adjusment.DropDownItems.AddRange(New ToolStripItem() {mnu_Adjusment_BiayaPenyusutanAsset, mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka, mnu_Adjusment_PenghapusanPiutang, mnu_Adjusment_SelisihKurs, mnu_Adjusment_SelisihPencatatan, mnu_Adjusment_HPP, mnu_AdjusmentLainnya})
        mnu_Adjusment.Name = "mnu_Adjusment"
        mnu_Adjusment.Size = New Size(205, 26)
        mnu_Adjusment.Text = "Adjusment"
        ' 
        ' mnu_Adjusment_BiayaPenyusutanAsset
        ' 
        mnu_Adjusment_BiayaPenyusutanAsset.Name = "mnu_Adjusment_BiayaPenyusutanAsset"
        mnu_Adjusment_BiayaPenyusutanAsset.Size = New Size(394, 26)
        mnu_Adjusment_BiayaPenyusutanAsset.Text = "Biaya Penyusutan Asset"
        ' 
        ' mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka
        ' 
        mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka.Name = "mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka"
        mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka.Size = New Size(394, 26)
        mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka.Text = "Biaya Amortisasi Sewa/Biaya Dibayar Dimuka"
        ' 
        ' mnu_Adjusment_PenghapusanPiutang
        ' 
        mnu_Adjusment_PenghapusanPiutang.Name = "mnu_Adjusment_PenghapusanPiutang"
        mnu_Adjusment_PenghapusanPiutang.Size = New Size(394, 26)
        mnu_Adjusment_PenghapusanPiutang.Text = "Penghapusan Piutang"
        ' 
        ' mnu_Adjusment_SelisihKurs
        ' 
        mnu_Adjusment_SelisihKurs.Name = "mnu_Adjusment_SelisihKurs"
        mnu_Adjusment_SelisihKurs.Size = New Size(394, 26)
        mnu_Adjusment_SelisihKurs.Text = "Selisih Kurs"
        ' 
        ' mnu_Adjusment_SelisihPencatatan
        ' 
        mnu_Adjusment_SelisihPencatatan.Name = "mnu_Adjusment_SelisihPencatatan"
        mnu_Adjusment_SelisihPencatatan.Size = New Size(394, 26)
        mnu_Adjusment_SelisihPencatatan.Text = "Selisih Pencatatan"
        ' 
        ' mnu_Adjusment_HPP
        ' 
        mnu_Adjusment_HPP.DropDownItems.AddRange(New ToolStripItem() {mnu_Adjusment_HPP_PemakaianBahanPenolong, mnu_Adjusment_HPP_PemakaianBahanBaku, mnu_Adjusment_HPP_BiayaBahanBaku, mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung, mnu_Adjusment_HPP_BiayaOverheadPabrik, mnu_Adjusment_HPP_BiayaProduksi, mnu_Adjusment_HPP_HargaPokokProduksi, mnu_Adjusment_HPP_HargaPokokPenjualan})
        mnu_Adjusment_HPP.Name = "mnu_Adjusment_HPP"
        mnu_Adjusment_HPP.Size = New Size(394, 26)
        mnu_Adjusment_HPP.Text = "HPP"
        mnu_Adjusment_HPP.Visible = False
        ' 
        ' mnu_Adjusment_HPP_PemakaianBahanPenolong
        ' 
        mnu_Adjusment_HPP_PemakaianBahanPenolong.Name = "mnu_Adjusment_HPP_PemakaianBahanPenolong"
        mnu_Adjusment_HPP_PemakaianBahanPenolong.Size = New Size(285, 26)
        mnu_Adjusment_HPP_PemakaianBahanPenolong.Text = "Pemakaian Bahan Penolong"
        ' 
        ' mnu_Adjusment_HPP_PemakaianBahanBaku
        ' 
        mnu_Adjusment_HPP_PemakaianBahanBaku.Name = "mnu_Adjusment_HPP_PemakaianBahanBaku"
        mnu_Adjusment_HPP_PemakaianBahanBaku.Size = New Size(285, 26)
        mnu_Adjusment_HPP_PemakaianBahanBaku.Text = "Pemakaian Bahan Baku"
        ' 
        ' mnu_Adjusment_HPP_BiayaBahanBaku
        ' 
        mnu_Adjusment_HPP_BiayaBahanBaku.Name = "mnu_Adjusment_HPP_BiayaBahanBaku"
        mnu_Adjusment_HPP_BiayaBahanBaku.Size = New Size(285, 26)
        mnu_Adjusment_HPP_BiayaBahanBaku.Text = "Biaya Bahan Baku"
        ' 
        ' mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung
        ' 
        mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung.Name = "mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung"
        mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung.Size = New Size(285, 26)
        mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung.Text = "Biaya Tenaga Kerja Langsung"
        ' 
        ' mnu_Adjusment_HPP_BiayaOverheadPabrik
        ' 
        mnu_Adjusment_HPP_BiayaOverheadPabrik.Name = "mnu_Adjusment_HPP_BiayaOverheadPabrik"
        mnu_Adjusment_HPP_BiayaOverheadPabrik.Size = New Size(285, 26)
        mnu_Adjusment_HPP_BiayaOverheadPabrik.Text = "Biaya Overhead Pabrik"
        ' 
        ' mnu_Adjusment_HPP_BiayaProduksi
        ' 
        mnu_Adjusment_HPP_BiayaProduksi.Name = "mnu_Adjusment_HPP_BiayaProduksi"
        mnu_Adjusment_HPP_BiayaProduksi.Size = New Size(285, 26)
        mnu_Adjusment_HPP_BiayaProduksi.Text = "Biaya Produksi"
        ' 
        ' mnu_Adjusment_HPP_HargaPokokProduksi
        ' 
        mnu_Adjusment_HPP_HargaPokokProduksi.Name = "mnu_Adjusment_HPP_HargaPokokProduksi"
        mnu_Adjusment_HPP_HargaPokokProduksi.Size = New Size(285, 26)
        mnu_Adjusment_HPP_HargaPokokProduksi.Text = "Harga Pokok Produksi"
        ' 
        ' mnu_Adjusment_HPP_HargaPokokPenjualan
        ' 
        mnu_Adjusment_HPP_HargaPokokPenjualan.Name = "mnu_Adjusment_HPP_HargaPokokPenjualan"
        mnu_Adjusment_HPP_HargaPokokPenjualan.Size = New Size(285, 26)
        mnu_Adjusment_HPP_HargaPokokPenjualan.Text = "Harga Pokok Penjualan"
        ' 
        ' mnu_AdjusmentLainnya
        ' 
        mnu_AdjusmentLainnya.Name = "mnu_AdjusmentLainnya"
        mnu_AdjusmentLainnya.Size = New Size(394, 26)
        mnu_AdjusmentLainnya.Text = "Adjusment Lainnya"
        ' 
        ' mnu_Pemindahbukuan
        ' 
        mnu_Pemindahbukuan.Name = "mnu_Pemindahbukuan"
        mnu_Pemindahbukuan.Size = New Size(205, 26)
        mnu_Pemindahbukuan.Text = "Pemindahbukuan"
        ' 
        ' mnu_Pembelian
        ' 
        mnu_Pembelian.DropDownItems.AddRange(New ToolStripItem() {mnu_PO_Pembelian, mnu_SuratJalanPembelian, mnu_BASTPembelian, mnu_InvoicePembelian, mnu_BukuPembelian, mnu_ReturPembelian})
        mnu_Pembelian.Name = "mnu_Pembelian"
        mnu_Pembelian.Size = New Size(92, 24)
        mnu_Pembelian.Text = "Pembelian"
        ' 
        ' mnu_PO_Pembelian
        ' 
        mnu_PO_Pembelian.DropDownItems.AddRange(New ToolStripItem() {mnu_PO_Pembelian_Lokal, mnu_PO_Pembelian_Impor})
        mnu_PO_Pembelian.Name = "mnu_PO_Pembelian"
        mnu_PO_Pembelian.Size = New Size(263, 26)
        mnu_PO_Pembelian.Text = "PO"
        ' 
        ' mnu_PO_Pembelian_Lokal
        ' 
        mnu_PO_Pembelian_Lokal.DropDownItems.AddRange(New ToolStripItem() {mnu_POPembelian_Lokal_Barang, mnu_POPembelian_Lokal_Jasa, mnu_POPembelian_Lokal_BarangDanJasa, mnu_POPembelian_Lokal_JasaKonstruksi, mnu_POPembelian_Semua})
        mnu_PO_Pembelian_Lokal.Name = "mnu_PO_Pembelian_Lokal"
        mnu_PO_Pembelian_Lokal.Size = New Size(132, 26)
        mnu_PO_Pembelian_Lokal.Text = "Lokal"
        ' 
        ' mnu_POPembelian_Lokal_Barang
        ' 
        mnu_POPembelian_Lokal_Barang.Name = "mnu_POPembelian_Lokal_Barang"
        mnu_POPembelian_Lokal_Barang.Size = New Size(199, 26)
        mnu_POPembelian_Lokal_Barang.Text = "Barang"
        ' 
        ' mnu_POPembelian_Lokal_Jasa
        ' 
        mnu_POPembelian_Lokal_Jasa.Name = "mnu_POPembelian_Lokal_Jasa"
        mnu_POPembelian_Lokal_Jasa.Size = New Size(199, 26)
        mnu_POPembelian_Lokal_Jasa.Text = "Jasa"
        ' 
        ' mnu_POPembelian_Lokal_BarangDanJasa
        ' 
        mnu_POPembelian_Lokal_BarangDanJasa.Name = "mnu_POPembelian_Lokal_BarangDanJasa"
        mnu_POPembelian_Lokal_BarangDanJasa.Size = New Size(199, 26)
        mnu_POPembelian_Lokal_BarangDanJasa.Text = "Barang dan Jasa"
        ' 
        ' mnu_POPembelian_Lokal_JasaKonstruksi
        ' 
        mnu_POPembelian_Lokal_JasaKonstruksi.Name = "mnu_POPembelian_Lokal_JasaKonstruksi"
        mnu_POPembelian_Lokal_JasaKonstruksi.Size = New Size(199, 26)
        mnu_POPembelian_Lokal_JasaKonstruksi.Text = "Jasa Konstruksi"
        ' 
        ' mnu_POPembelian_Semua
        ' 
        mnu_POPembelian_Semua.Name = "mnu_POPembelian_Semua"
        mnu_POPembelian_Semua.Size = New Size(199, 26)
        mnu_POPembelian_Semua.Text = "Semua"
        ' 
        ' mnu_PO_Pembelian_Impor
        ' 
        mnu_PO_Pembelian_Impor.DropDownItems.AddRange(New ToolStripItem() {mnu_PO_Pembelian_Impor_Barang, mnu_PO_Pembelian_Impor_Jasa, mnu_PO_Pembelian_Impor_Semua})
        mnu_PO_Pembelian_Impor.Name = "mnu_PO_Pembelian_Impor"
        mnu_PO_Pembelian_Impor.Size = New Size(132, 26)
        mnu_PO_Pembelian_Impor.Text = "Impor"
        ' 
        ' mnu_PO_Pembelian_Impor_Barang
        ' 
        mnu_PO_Pembelian_Impor_Barang.Name = "mnu_PO_Pembelian_Impor_Barang"
        mnu_PO_Pembelian_Impor_Barang.Size = New Size(139, 26)
        mnu_PO_Pembelian_Impor_Barang.Text = "Barang"
        ' 
        ' mnu_PO_Pembelian_Impor_Jasa
        ' 
        mnu_PO_Pembelian_Impor_Jasa.Name = "mnu_PO_Pembelian_Impor_Jasa"
        mnu_PO_Pembelian_Impor_Jasa.Size = New Size(139, 26)
        mnu_PO_Pembelian_Impor_Jasa.Text = "Jasa"
        ' 
        ' mnu_PO_Pembelian_Impor_Semua
        ' 
        mnu_PO_Pembelian_Impor_Semua.Name = "mnu_PO_Pembelian_Impor_Semua"
        mnu_PO_Pembelian_Impor_Semua.Size = New Size(139, 26)
        mnu_PO_Pembelian_Impor_Semua.Text = "Semua"
        ' 
        ' mnu_SuratJalanPembelian
        ' 
        mnu_SuratJalanPembelian.Name = "mnu_SuratJalanPembelian"
        mnu_SuratJalanPembelian.Size = New Size(263, 26)
        mnu_SuratJalanPembelian.Text = "Surat Jalan"
        ' 
        ' mnu_BASTPembelian
        ' 
        mnu_BASTPembelian.Name = "mnu_BASTPembelian"
        mnu_BASTPembelian.Size = New Size(263, 26)
        mnu_BASTPembelian.Text = "Berita Acara Serah Terima"
        ' 
        ' mnu_InvoicePembelian
        ' 
        mnu_InvoicePembelian.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePembelian_DenganPO, mnu_InvoicePembelian_TanpaPO})
        mnu_InvoicePembelian.Name = "mnu_InvoicePembelian"
        mnu_InvoicePembelian.Size = New Size(263, 26)
        mnu_InvoicePembelian.Text = "Invoice Pembelian"
        ' 
        ' mnu_InvoicePembelian_DenganPO
        ' 
        mnu_InvoicePembelian_DenganPO.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePembelian_DenganPO_Lokal, mnu_InvoicePembelian_DenganPO_Impor})
        mnu_InvoicePembelian_DenganPO.Name = "mnu_InvoicePembelian_DenganPO"
        mnu_InvoicePembelian_DenganPO.Size = New Size(167, 26)
        mnu_InvoicePembelian_DenganPO.Text = "Dengan PO"
        ' 
        ' mnu_InvoicePembelian_DenganPO_Lokal
        ' 
        mnu_InvoicePembelian_DenganPO_Lokal.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePembelian_DenganPO_Lokal_Rutin, mnu_InvoicePembelian_DenganPO_Lokal_Termin})
        mnu_InvoicePembelian_DenganPO_Lokal.Name = "mnu_InvoicePembelian_DenganPO_Lokal"
        mnu_InvoicePembelian_DenganPO_Lokal.Size = New Size(132, 26)
        mnu_InvoicePembelian_DenganPO_Lokal.Text = "Lokal"
        ' 
        ' mnu_InvoicePembelian_DenganPO_Lokal_Rutin
        ' 
        mnu_InvoicePembelian_DenganPO_Lokal_Rutin.Name = "mnu_InvoicePembelian_DenganPO_Lokal_Rutin"
        mnu_InvoicePembelian_DenganPO_Lokal_Rutin.Size = New Size(137, 26)
        mnu_InvoicePembelian_DenganPO_Lokal_Rutin.Text = "Rutin"
        ' 
        ' mnu_InvoicePembelian_DenganPO_Lokal_Termin
        ' 
        mnu_InvoicePembelian_DenganPO_Lokal_Termin.Name = "mnu_InvoicePembelian_DenganPO_Lokal_Termin"
        mnu_InvoicePembelian_DenganPO_Lokal_Termin.Size = New Size(137, 26)
        mnu_InvoicePembelian_DenganPO_Lokal_Termin.Text = "Termin"
        ' 
        ' mnu_InvoicePembelian_DenganPO_Impor
        ' 
        mnu_InvoicePembelian_DenganPO_Impor.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePembelian_DenganPO_Impor_Rutin, mnu_InvoicePembelian_DenganPO_Impor_Termin})
        mnu_InvoicePembelian_DenganPO_Impor.Name = "mnu_InvoicePembelian_DenganPO_Impor"
        mnu_InvoicePembelian_DenganPO_Impor.Size = New Size(132, 26)
        mnu_InvoicePembelian_DenganPO_Impor.Text = "Impor"
        ' 
        ' mnu_InvoicePembelian_DenganPO_Impor_Rutin
        ' 
        mnu_InvoicePembelian_DenganPO_Impor_Rutin.Name = "mnu_InvoicePembelian_DenganPO_Impor_Rutin"
        mnu_InvoicePembelian_DenganPO_Impor_Rutin.Size = New Size(137, 26)
        mnu_InvoicePembelian_DenganPO_Impor_Rutin.Text = "Rutin"
        ' 
        ' mnu_InvoicePembelian_DenganPO_Impor_Termin
        ' 
        mnu_InvoicePembelian_DenganPO_Impor_Termin.Name = "mnu_InvoicePembelian_DenganPO_Impor_Termin"
        mnu_InvoicePembelian_DenganPO_Impor_Termin.Size = New Size(137, 26)
        mnu_InvoicePembelian_DenganPO_Impor_Termin.Text = "Termin"
        ' 
        ' mnu_InvoicePembelian_TanpaPO
        ' 
        mnu_InvoicePembelian_TanpaPO.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePembelian_TanpaPO_Lokal, mnu_InvoicePembelian_TanpaPO_Impor, mnu_InvoicePembelian_TanpaPO_LokalMUA})
        mnu_InvoicePembelian_TanpaPO.Name = "mnu_InvoicePembelian_TanpaPO"
        mnu_InvoicePembelian_TanpaPO.Size = New Size(167, 26)
        mnu_InvoicePembelian_TanpaPO.Text = "Tanpa PO"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_Lokal
        ' 
        mnu_InvoicePembelian_TanpaPO_Lokal.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePembelian_TanpaPO_Lokal_Barang, mnu_InvoicePembelian_TanpaPO_Lokal_Jasa, mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa, mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi})
        mnu_InvoicePembelian_TanpaPO_Lokal.Name = "mnu_InvoicePembelian_TanpaPO_Lokal"
        mnu_InvoicePembelian_TanpaPO_Lokal.Size = New Size(164, 26)
        mnu_InvoicePembelian_TanpaPO_Lokal.Text = "Lokal"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_Lokal_Barang
        ' 
        mnu_InvoicePembelian_TanpaPO_Lokal_Barang.Name = "mnu_InvoicePembelian_TanpaPO_Lokal_Barang"
        mnu_InvoicePembelian_TanpaPO_Lokal_Barang.Size = New Size(199, 26)
        mnu_InvoicePembelian_TanpaPO_Lokal_Barang.Text = "Barang"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_Lokal_Jasa
        ' 
        mnu_InvoicePembelian_TanpaPO_Lokal_Jasa.Name = "mnu_InvoicePembelian_TanpaPO_Lokal_Jasa"
        mnu_InvoicePembelian_TanpaPO_Lokal_Jasa.Size = New Size(199, 26)
        mnu_InvoicePembelian_TanpaPO_Lokal_Jasa.Text = "Jasa"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa
        ' 
        mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Name = "mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa"
        mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Size = New Size(199, 26)
        mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa.Text = "Barang dan Jasa"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi
        ' 
        mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Name = "mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi"
        mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Size = New Size(199, 26)
        mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi.Text = "Jasa Konstruksi"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_Impor
        ' 
        mnu_InvoicePembelian_TanpaPO_Impor.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePembelian_TanpaPO_Impor_Barang, mnu_InvoicePembelian_TanpaPO_Impor_Jasa})
        mnu_InvoicePembelian_TanpaPO_Impor.Name = "mnu_InvoicePembelian_TanpaPO_Impor"
        mnu_InvoicePembelian_TanpaPO_Impor.Size = New Size(164, 26)
        mnu_InvoicePembelian_TanpaPO_Impor.Text = "Impor"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_Impor_Barang
        ' 
        mnu_InvoicePembelian_TanpaPO_Impor_Barang.Name = "mnu_InvoicePembelian_TanpaPO_Impor_Barang"
        mnu_InvoicePembelian_TanpaPO_Impor_Barang.Size = New Size(139, 26)
        mnu_InvoicePembelian_TanpaPO_Impor_Barang.Text = "Barang"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_Impor_Jasa
        ' 
        mnu_InvoicePembelian_TanpaPO_Impor_Jasa.Name = "mnu_InvoicePembelian_TanpaPO_Impor_Jasa"
        mnu_InvoicePembelian_TanpaPO_Impor_Jasa.Size = New Size(139, 26)
        mnu_InvoicePembelian_TanpaPO_Impor_Jasa.Text = "Jasa"
        ' 
        ' mnu_InvoicePembelian_TanpaPO_LokalMUA
        ' 
        mnu_InvoicePembelian_TanpaPO_LokalMUA.Name = "mnu_InvoicePembelian_TanpaPO_LokalMUA"
        mnu_InvoicePembelian_TanpaPO_LokalMUA.Size = New Size(164, 26)
        mnu_InvoicePembelian_TanpaPO_LokalMUA.Text = "Lokal MUA"
        ' 
        ' mnu_BukuPembelian
        ' 
        mnu_BukuPembelian.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPembelian_Lokal, mnu_BukuPembelian_Impor})
        mnu_BukuPembelian.Name = "mnu_BukuPembelian"
        mnu_BukuPembelian.Size = New Size(263, 26)
        mnu_BukuPembelian.Text = "Buku Pembelian"
        ' 
        ' mnu_BukuPembelian_Lokal
        ' 
        mnu_BukuPembelian_Lokal.Name = "mnu_BukuPembelian_Lokal"
        mnu_BukuPembelian_Lokal.Size = New Size(132, 26)
        mnu_BukuPembelian_Lokal.Text = "Lokal"
        ' 
        ' mnu_BukuPembelian_Impor
        ' 
        mnu_BukuPembelian_Impor.Name = "mnu_BukuPembelian_Impor"
        mnu_BukuPembelian_Impor.Size = New Size(132, 26)
        mnu_BukuPembelian_Impor.Text = "Impor"
        ' 
        ' mnu_ReturPembelian
        ' 
        mnu_ReturPembelian.Name = "mnu_ReturPembelian"
        mnu_ReturPembelian.Size = New Size(263, 26)
        mnu_ReturPembelian.Text = "Retur Pembelian"
        ' 
        ' mnu_Penjualan
        ' 
        mnu_Penjualan.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanPOPenjualan, mnu_SuratJalanPenjualan, mnu_BASTPenjualan, mnu_InvoicePenjualan, mnu_BukuPenjualan, mnu_BukuPenjualanEceran, mnu_BukuPengawasanReturPenjualan})
        mnu_Penjualan.Name = "mnu_Penjualan"
        mnu_Penjualan.Size = New Size(86, 24)
        mnu_Penjualan.Text = "Penjualan"
        ' 
        ' mnu_BukuPengawasanPOPenjualan
        ' 
        mnu_BukuPengawasanPOPenjualan.DropDownItems.AddRange(New ToolStripItem() {mnu_POPenjualan_Lokal, mnu_POPenjualan_Ekspor})
        mnu_BukuPengawasanPOPenjualan.Name = "mnu_BukuPengawasanPOPenjualan"
        mnu_BukuPengawasanPOPenjualan.Size = New Size(315, 26)
        mnu_BukuPengawasanPOPenjualan.Text = "Buku Pengawasan PO Penjualan"
        ' 
        ' mnu_POPenjualan_Lokal
        ' 
        mnu_POPenjualan_Lokal.DropDownItems.AddRange(New ToolStripItem() {mnu_POPenjualan_Lokal_Barang, mnu_POPenjualan_Lokal_Jasa, mnu_POPenjualan_Lokal_BarangDanJasa, mnu_POPenjualan_Lokal_JasaKonstruksi, mnu_POPenjualan_Lokal_Semua})
        mnu_POPenjualan_Lokal.Name = "mnu_POPenjualan_Lokal"
        mnu_POPenjualan_Lokal.Size = New Size(136, 26)
        mnu_POPenjualan_Lokal.Text = "Lokal"
        ' 
        ' mnu_POPenjualan_Lokal_Barang
        ' 
        mnu_POPenjualan_Lokal_Barang.Name = "mnu_POPenjualan_Lokal_Barang"
        mnu_POPenjualan_Lokal_Barang.Size = New Size(199, 26)
        mnu_POPenjualan_Lokal_Barang.Text = "Barang"
        ' 
        ' mnu_POPenjualan_Lokal_Jasa
        ' 
        mnu_POPenjualan_Lokal_Jasa.Name = "mnu_POPenjualan_Lokal_Jasa"
        mnu_POPenjualan_Lokal_Jasa.Size = New Size(199, 26)
        mnu_POPenjualan_Lokal_Jasa.Text = "Jasa"
        ' 
        ' mnu_POPenjualan_Lokal_BarangDanJasa
        ' 
        mnu_POPenjualan_Lokal_BarangDanJasa.Name = "mnu_POPenjualan_Lokal_BarangDanJasa"
        mnu_POPenjualan_Lokal_BarangDanJasa.Size = New Size(199, 26)
        mnu_POPenjualan_Lokal_BarangDanJasa.Text = "Barang dan Jasa"
        ' 
        ' mnu_POPenjualan_Lokal_JasaKonstruksi
        ' 
        mnu_POPenjualan_Lokal_JasaKonstruksi.Name = "mnu_POPenjualan_Lokal_JasaKonstruksi"
        mnu_POPenjualan_Lokal_JasaKonstruksi.Size = New Size(199, 26)
        mnu_POPenjualan_Lokal_JasaKonstruksi.Text = "Jasa Konstruksi"
        ' 
        ' mnu_POPenjualan_Lokal_Semua
        ' 
        mnu_POPenjualan_Lokal_Semua.Name = "mnu_POPenjualan_Lokal_Semua"
        mnu_POPenjualan_Lokal_Semua.Size = New Size(199, 26)
        mnu_POPenjualan_Lokal_Semua.Text = "Semua"
        ' 
        ' mnu_POPenjualan_Ekspor
        ' 
        mnu_POPenjualan_Ekspor.Name = "mnu_POPenjualan_Ekspor"
        mnu_POPenjualan_Ekspor.Size = New Size(136, 26)
        mnu_POPenjualan_Ekspor.Text = "Ekspor"
        ' 
        ' mnu_SuratJalanPenjualan
        ' 
        mnu_SuratJalanPenjualan.Name = "mnu_SuratJalanPenjualan"
        mnu_SuratJalanPenjualan.Size = New Size(315, 26)
        mnu_SuratJalanPenjualan.Text = "Surat Jalan"
        ' 
        ' mnu_BASTPenjualan
        ' 
        mnu_BASTPenjualan.Name = "mnu_BASTPenjualan"
        mnu_BASTPenjualan.Size = New Size(315, 26)
        mnu_BASTPenjualan.Text = "Berita Acara Serah Terima"
        ' 
        ' mnu_InvoicePenjualan
        ' 
        mnu_InvoicePenjualan.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePenjualan_DenganPO, mnu_InvoicePenjualan_TanpaPO})
        mnu_InvoicePenjualan.Name = "mnu_InvoicePenjualan"
        mnu_InvoicePenjualan.Size = New Size(315, 26)
        mnu_InvoicePenjualan.Text = "Invoice Penjualan"
        ' 
        ' mnu_InvoicePenjualan_DenganPO
        ' 
        mnu_InvoicePenjualan_DenganPO.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePenjualan_DenganPO_Lokal, mnu_InvoicePenjualan_DenganPO_Ekspor})
        mnu_InvoicePenjualan_DenganPO.Name = "mnu_InvoicePenjualan_DenganPO"
        mnu_InvoicePenjualan_DenganPO.Size = New Size(167, 26)
        mnu_InvoicePenjualan_DenganPO.Text = "Dengan PO"
        ' 
        ' mnu_InvoicePenjualan_DenganPO_Lokal
        ' 
        mnu_InvoicePenjualan_DenganPO_Lokal.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePenjualan_DenganPO_Lokal_Rutin, mnu_InvoicePenjualan_DenganPO_Lokal_Termin})
        mnu_InvoicePenjualan_DenganPO_Lokal.Name = "mnu_InvoicePenjualan_DenganPO_Lokal"
        mnu_InvoicePenjualan_DenganPO_Lokal.Size = New Size(136, 26)
        mnu_InvoicePenjualan_DenganPO_Lokal.Text = "Lokal"
        ' 
        ' mnu_InvoicePenjualan_DenganPO_Lokal_Rutin
        ' 
        mnu_InvoicePenjualan_DenganPO_Lokal_Rutin.Name = "mnu_InvoicePenjualan_DenganPO_Lokal_Rutin"
        mnu_InvoicePenjualan_DenganPO_Lokal_Rutin.Size = New Size(137, 26)
        mnu_InvoicePenjualan_DenganPO_Lokal_Rutin.Text = "Rutin"
        ' 
        ' mnu_InvoicePenjualan_DenganPO_Lokal_Termin
        ' 
        mnu_InvoicePenjualan_DenganPO_Lokal_Termin.Name = "mnu_InvoicePenjualan_DenganPO_Lokal_Termin"
        mnu_InvoicePenjualan_DenganPO_Lokal_Termin.Size = New Size(137, 26)
        mnu_InvoicePenjualan_DenganPO_Lokal_Termin.Text = "Termin"
        ' 
        ' mnu_InvoicePenjualan_DenganPO_Ekspor
        ' 
        mnu_InvoicePenjualan_DenganPO_Ekspor.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin, mnu_InvoicePenjualan_DenganPO_Ekspor_Termin})
        mnu_InvoicePenjualan_DenganPO_Ekspor.Name = "mnu_InvoicePenjualan_DenganPO_Ekspor"
        mnu_InvoicePenjualan_DenganPO_Ekspor.Size = New Size(136, 26)
        mnu_InvoicePenjualan_DenganPO_Ekspor.Text = "Ekspor"
        ' 
        ' mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin
        ' 
        mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin.Name = "mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin"
        mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin.Size = New Size(137, 26)
        mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin.Text = "Rutin"
        ' 
        ' mnu_InvoicePenjualan_DenganPO_Ekspor_Termin
        ' 
        mnu_InvoicePenjualan_DenganPO_Ekspor_Termin.Name = "mnu_InvoicePenjualan_DenganPO_Ekspor_Termin"
        mnu_InvoicePenjualan_DenganPO_Ekspor_Termin.Size = New Size(137, 26)
        mnu_InvoicePenjualan_DenganPO_Ekspor_Termin.Text = "Termin"
        ' 
        ' mnu_InvoicePenjualan_TanpaPO
        ' 
        mnu_InvoicePenjualan_TanpaPO.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePenjualan_TanpaPO_Lokal, mnu_InvoicePenjualan_TanpaPO_Ekspor, mnu_InvoicePenjualan_TanpaPO_Asset})
        mnu_InvoicePenjualan_TanpaPO.Name = "mnu_InvoicePenjualan_TanpaPO"
        mnu_InvoicePenjualan_TanpaPO.Size = New Size(167, 26)
        mnu_InvoicePenjualan_TanpaPO.Text = "Tanpa PO"
        ' 
        ' mnu_InvoicePenjualan_TanpaPO_Lokal
        ' 
        mnu_InvoicePenjualan_TanpaPO_Lokal.DropDownItems.AddRange(New ToolStripItem() {mnu_InvoicePenjualan_TanpaPO_Lokal_Barang, mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa, mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa, mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi})
        mnu_InvoicePenjualan_TanpaPO_Lokal.Name = "mnu_InvoicePenjualan_TanpaPO_Lokal"
        mnu_InvoicePenjualan_TanpaPO_Lokal.Size = New Size(136, 26)
        mnu_InvoicePenjualan_TanpaPO_Lokal.Text = "Lokal"
        ' 
        ' mnu_InvoicePenjualan_TanpaPO_Lokal_Barang
        ' 
        mnu_InvoicePenjualan_TanpaPO_Lokal_Barang.Name = "mnu_InvoicePenjualan_TanpaPO_Lokal_Barang"
        mnu_InvoicePenjualan_TanpaPO_Lokal_Barang.Size = New Size(199, 26)
        mnu_InvoicePenjualan_TanpaPO_Lokal_Barang.Text = "Barang"
        ' 
        ' mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa
        ' 
        mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa.Name = "mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa"
        mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa.Size = New Size(199, 26)
        mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa.Text = "Jasa"
        ' 
        ' mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa
        ' 
        mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.Name = "mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa"
        mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.Size = New Size(199, 26)
        mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa.Text = "Barang dan Jasa"
        ' 
        ' mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi
        ' 
        mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Name = "mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi"
        mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Size = New Size(199, 26)
        mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi.Text = "Jasa Konstruksi"
        ' 
        ' mnu_InvoicePenjualan_TanpaPO_Ekspor
        ' 
        mnu_InvoicePenjualan_TanpaPO_Ekspor.Name = "mnu_InvoicePenjualan_TanpaPO_Ekspor"
        mnu_InvoicePenjualan_TanpaPO_Ekspor.Size = New Size(136, 26)
        mnu_InvoicePenjualan_TanpaPO_Ekspor.Text = "Ekspor"
        ' 
        ' mnu_InvoicePenjualan_TanpaPO_Asset
        ' 
        mnu_InvoicePenjualan_TanpaPO_Asset.Name = "mnu_InvoicePenjualan_TanpaPO_Asset"
        mnu_InvoicePenjualan_TanpaPO_Asset.Size = New Size(136, 26)
        mnu_InvoicePenjualan_TanpaPO_Asset.Text = "Asset"
        ' 
        ' mnu_BukuPenjualan
        ' 
        mnu_BukuPenjualan.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPenjualan_Lokal, mnu_BukuPenjualan_Ekspor})
        mnu_BukuPenjualan.Name = "mnu_BukuPenjualan"
        mnu_BukuPenjualan.Size = New Size(315, 26)
        mnu_BukuPenjualan.Text = "Buku Penjualan"
        ' 
        ' mnu_BukuPenjualan_Lokal
        ' 
        mnu_BukuPenjualan_Lokal.Name = "mnu_BukuPenjualan_Lokal"
        mnu_BukuPenjualan_Lokal.Size = New Size(136, 26)
        mnu_BukuPenjualan_Lokal.Text = "Lokal"
        ' 
        ' mnu_BukuPenjualan_Ekspor
        ' 
        mnu_BukuPenjualan_Ekspor.Name = "mnu_BukuPenjualan_Ekspor"
        mnu_BukuPenjualan_Ekspor.Size = New Size(136, 26)
        mnu_BukuPenjualan_Ekspor.Text = "Ekspor"
        ' 
        ' mnu_BukuPenjualanEceran
        ' 
        mnu_BukuPenjualanEceran.Name = "mnu_BukuPenjualanEceran"
        mnu_BukuPenjualanEceran.Size = New Size(315, 26)
        mnu_BukuPenjualanEceran.Text = "Buku Penjualan Eceran"
        ' 
        ' mnu_BukuPengawasanReturPenjualan
        ' 
        mnu_BukuPengawasanReturPenjualan.Name = "mnu_BukuPengawasanReturPenjualan"
        mnu_BukuPengawasanReturPenjualan.Size = New Size(315, 26)
        mnu_BukuPengawasanReturPenjualan.Text = "Buku Pengawasan Retur Penjualan"
        ' 
        ' mnu_BukuPengawasan
        ' 
        mnu_BukuPengawasan.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuBankCash, mnu_BukuPengawasanGaji_Induk, mnu_BukuPengawasanHutang, mnu_BukuPengawasanPiutang, mnu_BukuPengawasanBuktiPenerimaanBankCash, mnu_BukuPengawasanBuktiPengeluaranBankCash, mnu_BukuPengawasanPemindabukuan, mnu_BukuPengawasanAktivaLainnya})
        mnu_BukuPengawasan.Name = "mnu_BukuPengawasan"
        mnu_BukuPengawasan.Size = New Size(140, 24)
        mnu_BukuPengawasan.Text = "Buku Pengawasan"
        ' 
        ' mnu_BukuBankCash
        ' 
        mnu_BukuBankCash.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuBank, mnu_BukuKas, mnu_BukuPettyCash, mnu_BukuCashAdvance, mnu_BukuBankGaransi})
        mnu_BukuBankCash.Name = "mnu_BukuBankCash"
        mnu_BukuBankCash.Size = New Size(402, 26)
        mnu_BukuBankCash.Text = "Buku Bank Cash"
        ' 
        ' mnu_BukuBank
        ' 
        mnu_BukuBank.Name = "mnu_BukuBank"
        mnu_BukuBank.Size = New Size(220, 26)
        mnu_BukuBank.Text = "Buku Bank"
        ' 
        ' mnu_BukuKas
        ' 
        mnu_BukuKas.Name = "mnu_BukuKas"
        mnu_BukuKas.Size = New Size(220, 26)
        mnu_BukuKas.Text = "Buku Kas"
        ' 
        ' mnu_BukuPettyCash
        ' 
        mnu_BukuPettyCash.Name = "mnu_BukuPettyCash"
        mnu_BukuPettyCash.Size = New Size(220, 26)
        mnu_BukuPettyCash.Text = "Buku Petty Cash"
        ' 
        ' mnu_BukuCashAdvance
        ' 
        mnu_BukuCashAdvance.Name = "mnu_BukuCashAdvance"
        mnu_BukuCashAdvance.Size = New Size(220, 26)
        mnu_BukuCashAdvance.Text = "Buku Cash Advance"
        ' 
        ' mnu_BukuBankGaransi
        ' 
        mnu_BukuBankGaransi.Name = "mnu_BukuBankGaransi"
        mnu_BukuBankGaransi.Size = New Size(220, 26)
        mnu_BukuBankGaransi.Text = "Buku Bank Garansi"
        ' 
        ' mnu_BukuPengawasanGaji_Induk
        ' 
        mnu_BukuPengawasanGaji_Induk.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanGaji, mnu_BukuPengawasanHutangBPJSKesehatan, mnu_BukuPengawasanHutangBPJSKetenagakerjaan, mnu_BukuPengawasanHutangKoperasiKaryawan, mnu_BukuPengawasanHutangSerikat})
        mnu_BukuPengawasanGaji_Induk.Name = "mnu_BukuPengawasanGaji_Induk"
        mnu_BukuPengawasanGaji_Induk.Size = New Size(402, 26)
        mnu_BukuPengawasanGaji_Induk.Text = "Buku Pengawasan Gaji"
        ' 
        ' mnu_BukuPengawasanGaji
        ' 
        mnu_BukuPengawasanGaji.Name = "mnu_BukuPengawasanGaji"
        mnu_BukuPengawasanGaji.Size = New Size(410, 26)
        mnu_BukuPengawasanGaji.Text = "Buku Pengawasan Gaji"
        ' 
        ' mnu_BukuPengawasanHutangBPJSKesehatan
        ' 
        mnu_BukuPengawasanHutangBPJSKesehatan.Name = "mnu_BukuPengawasanHutangBPJSKesehatan"
        mnu_BukuPengawasanHutangBPJSKesehatan.Size = New Size(410, 26)
        mnu_BukuPengawasanHutangBPJSKesehatan.Text = "Buku Pengawasan Hutang BPJS Kesehatan"
        ' 
        ' mnu_BukuPengawasanHutangBPJSKetenagakerjaan
        ' 
        mnu_BukuPengawasanHutangBPJSKetenagakerjaan.Name = "mnu_BukuPengawasanHutangBPJSKetenagakerjaan"
        mnu_BukuPengawasanHutangBPJSKetenagakerjaan.Size = New Size(410, 26)
        mnu_BukuPengawasanHutangBPJSKetenagakerjaan.Text = "Buku Pengawasan Hutang BPJS Ketenagakerjaan"
        ' 
        ' mnu_BukuPengawasanHutangKoperasiKaryawan
        ' 
        mnu_BukuPengawasanHutangKoperasiKaryawan.Name = "mnu_BukuPengawasanHutangKoperasiKaryawan"
        mnu_BukuPengawasanHutangKoperasiKaryawan.Size = New Size(410, 26)
        mnu_BukuPengawasanHutangKoperasiKaryawan.Text = "Buku Pengawasan Hutang Koperasi Karyawan"
        ' 
        ' mnu_BukuPengawasanHutangSerikat
        ' 
        mnu_BukuPengawasanHutangSerikat.Name = "mnu_BukuPengawasanHutangSerikat"
        mnu_BukuPengawasanHutangSerikat.Size = New Size(410, 26)
        mnu_BukuPengawasanHutangSerikat.Text = "Buku Pengawasan Hutang Serikat"
        ' 
        ' mnu_BukuPengawasanHutang
        ' 
        mnu_BukuPengawasanHutang.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanHutangUsaha, mnu_BukuPengawasanHutangUsaha_BAK, mnu_BukuPengawasanHutangBank, mnu_BukuPengawasanHutangLeasing, mnu_BukuPengawasanHutangPihakKetiga, mnu_BukuPengawasanHutangAfiliasi, mnu_BukuPengawasanHutangKaryawan, mnu_BukuPengawasanHutangPemegangSaham, mnu_BukuPengawasanHutangDividen, mnu_BukuPengawasanHutangLainnya})
        mnu_BukuPengawasanHutang.Name = "mnu_BukuPengawasanHutang"
        mnu_BukuPengawasanHutang.Size = New Size(402, 26)
        mnu_BukuPengawasanHutang.Text = "Buku Pengawasan Hutang"
        ' 
        ' mnu_BukuPengawasanHutangUsaha
        ' 
        mnu_BukuPengawasanHutangUsaha.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanHutangUsaha_Lokal, mnu_BukuPengawasanHutangUsaha_Impor})
        mnu_BukuPengawasanHutangUsaha.Name = "mnu_BukuPengawasanHutangUsaha"
        mnu_BukuPengawasanHutangUsaha.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangUsaha.Text = "Buku Pengawasan Hutang Usaha"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Lokal
        ' 
        mnu_BukuPengawasanHutangUsaha_Lokal.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanHutangUsaha_NonAfiliasi, mnu_BukuPengawasanHutangUsaha_Afiliasi, mnu_BukuPengawasanHutangUsaha_Semua})
        mnu_BukuPengawasanHutangUsaha_Lokal.Name = "mnu_BukuPengawasanHutangUsaha_Lokal"
        mnu_BukuPengawasanHutangUsaha_Lokal.Size = New Size(132, 26)
        mnu_BukuPengawasanHutangUsaha_Lokal.Text = "Lokal"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_NonAfiliasi
        ' 
        mnu_BukuPengawasanHutangUsaha_NonAfiliasi.Name = "mnu_BukuPengawasanHutangUsaha_NonAfiliasi"
        mnu_BukuPengawasanHutangUsaha_NonAfiliasi.Size = New Size(169, 26)
        mnu_BukuPengawasanHutangUsaha_NonAfiliasi.Text = "Non Afiliasi"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Afiliasi
        ' 
        mnu_BukuPengawasanHutangUsaha_Afiliasi.Name = "mnu_BukuPengawasanHutangUsaha_Afiliasi"
        mnu_BukuPengawasanHutangUsaha_Afiliasi.Size = New Size(169, 26)
        mnu_BukuPengawasanHutangUsaha_Afiliasi.Text = "Afiliasi"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Semua
        ' 
        mnu_BukuPengawasanHutangUsaha_Semua.Name = "mnu_BukuPengawasanHutangUsaha_Semua"
        mnu_BukuPengawasanHutangUsaha_Semua.Size = New Size(169, 26)
        mnu_BukuPengawasanHutangUsaha_Semua.Text = "Semua"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Impor
        ' 
        mnu_BukuPengawasanHutangUsaha_Impor.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanHutangUsaha_Impor_USD, mnu_BukuPengawasanHutangUsaha_Impor_AUD, mnu_BukuPengawasanHutangUsaha_Impor_JPY, mnu_BukuPengawasanHutangUsaha_Impor_CNY, mnu_BukuPengawasanHutangUsaha_Impor_EUR, mnu_BukuPengawasanHutangUsaha_Impor_SGD, mnu_BukuPengawasanHutangUsaha_Impor_GBP})
        mnu_BukuPengawasanHutangUsaha_Impor.Name = "mnu_BukuPengawasanHutangUsaha_Impor"
        mnu_BukuPengawasanHutangUsaha_Impor.Size = New Size(132, 26)
        mnu_BukuPengawasanHutangUsaha_Impor.Text = "Impor"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Impor_USD
        ' 
        mnu_BukuPengawasanHutangUsaha_Impor_USD.Name = "mnu_BukuPengawasanHutangUsaha_Impor_USD"
        mnu_BukuPengawasanHutangUsaha_Impor_USD.Size = New Size(123, 26)
        mnu_BukuPengawasanHutangUsaha_Impor_USD.Text = "USD"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Impor_AUD
        ' 
        mnu_BukuPengawasanHutangUsaha_Impor_AUD.Name = "mnu_BukuPengawasanHutangUsaha_Impor_AUD"
        mnu_BukuPengawasanHutangUsaha_Impor_AUD.Size = New Size(123, 26)
        mnu_BukuPengawasanHutangUsaha_Impor_AUD.Text = "AUD"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Impor_JPY
        ' 
        mnu_BukuPengawasanHutangUsaha_Impor_JPY.Name = "mnu_BukuPengawasanHutangUsaha_Impor_JPY"
        mnu_BukuPengawasanHutangUsaha_Impor_JPY.Size = New Size(123, 26)
        mnu_BukuPengawasanHutangUsaha_Impor_JPY.Text = "JPY"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Impor_CNY
        ' 
        mnu_BukuPengawasanHutangUsaha_Impor_CNY.Name = "mnu_BukuPengawasanHutangUsaha_Impor_CNY"
        mnu_BukuPengawasanHutangUsaha_Impor_CNY.Size = New Size(123, 26)
        mnu_BukuPengawasanHutangUsaha_Impor_CNY.Text = "CNY"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Impor_EUR
        ' 
        mnu_BukuPengawasanHutangUsaha_Impor_EUR.Name = "mnu_BukuPengawasanHutangUsaha_Impor_EUR"
        mnu_BukuPengawasanHutangUsaha_Impor_EUR.Size = New Size(123, 26)
        mnu_BukuPengawasanHutangUsaha_Impor_EUR.Text = "EUR"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Impor_SGD
        ' 
        mnu_BukuPengawasanHutangUsaha_Impor_SGD.Name = "mnu_BukuPengawasanHutangUsaha_Impor_SGD"
        mnu_BukuPengawasanHutangUsaha_Impor_SGD.Size = New Size(123, 26)
        mnu_BukuPengawasanHutangUsaha_Impor_SGD.Text = "SGD"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_Impor_GBP
        ' 
        mnu_BukuPengawasanHutangUsaha_Impor_GBP.Name = "mnu_BukuPengawasanHutangUsaha_Impor_GBP"
        mnu_BukuPengawasanHutangUsaha_Impor_GBP.Size = New Size(123, 26)
        mnu_BukuPengawasanHutangUsaha_Impor_GBP.Text = "GBP"
        ' 
        ' mnu_BukuPengawasanHutangUsaha_BAK
        ' 
        mnu_BukuPengawasanHutangUsaha_BAK.Name = "mnu_BukuPengawasanHutangUsaha_BAK"
        mnu_BukuPengawasanHutangUsaha_BAK.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangUsaha_BAK.Text = "Buku Pengawasan Hutang Usaha (Backup)"
        mnu_BukuPengawasanHutangUsaha_BAK.Visible = False
        ' 
        ' mnu_BukuPengawasanHutangBank
        ' 
        mnu_BukuPengawasanHutangBank.Name = "mnu_BukuPengawasanHutangBank"
        mnu_BukuPengawasanHutangBank.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangBank.Text = "Buku Pengawasan Hutang Bank"
        ' 
        ' mnu_BukuPengawasanHutangLeasing
        ' 
        mnu_BukuPengawasanHutangLeasing.Name = "mnu_BukuPengawasanHutangLeasing"
        mnu_BukuPengawasanHutangLeasing.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangLeasing.Text = "Buku Pengawasan Hutang Leasing"
        ' 
        ' mnu_BukuPengawasanHutangPihakKetiga
        ' 
        mnu_BukuPengawasanHutangPihakKetiga.Name = "mnu_BukuPengawasanHutangPihakKetiga"
        mnu_BukuPengawasanHutangPihakKetiga.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangPihakKetiga.Text = "Buku Pengawasan Hutang Pihak Ketiga"
        ' 
        ' mnu_BukuPengawasanHutangAfiliasi
        ' 
        mnu_BukuPengawasanHutangAfiliasi.Name = "mnu_BukuPengawasanHutangAfiliasi"
        mnu_BukuPengawasanHutangAfiliasi.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangAfiliasi.Text = "Buku Pengawasan Hutang Afiliasi"
        ' 
        ' mnu_BukuPengawasanHutangKaryawan
        ' 
        mnu_BukuPengawasanHutangKaryawan.Name = "mnu_BukuPengawasanHutangKaryawan"
        mnu_BukuPengawasanHutangKaryawan.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangKaryawan.Text = "Buku Pengawasan Hutang Karyawan"
        ' 
        ' mnu_BukuPengawasanHutangPemegangSaham
        ' 
        mnu_BukuPengawasanHutangPemegangSaham.Name = "mnu_BukuPengawasanHutangPemegangSaham"
        mnu_BukuPengawasanHutangPemegangSaham.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangPemegangSaham.Text = "Buku Pengawasan Hutang Pemegang Saham"
        ' 
        ' mnu_BukuPengawasanHutangDividen
        ' 
        mnu_BukuPengawasanHutangDividen.Name = "mnu_BukuPengawasanHutangDividen"
        mnu_BukuPengawasanHutangDividen.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangDividen.Text = "Buku Pengawasan Hutang Dividen"
        ' 
        ' mnu_BukuPengawasanHutangLainnya
        ' 
        mnu_BukuPengawasanHutangLainnya.Name = "mnu_BukuPengawasanHutangLainnya"
        mnu_BukuPengawasanHutangLainnya.Size = New Size(385, 26)
        mnu_BukuPengawasanHutangLainnya.Text = "Buku Pengawasan Hutang Lainnya"
        ' 
        ' mnu_BukuPengawasanPiutang
        ' 
        mnu_BukuPengawasanPiutang.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanPiutangUsaha, mnu_BukuPengawasanPiutangPihakKetiga, mnu_BukuPengawasanPiutangAfiliasi, mnu_BukuPengawasanPiutangKaryawan, mnu_BukuPengawasanPiutangPemegangSaham, mnu_BukuPengawasanDepositOperasional, mnu_BukuPengawasanPiutangDividen, mnu_BukuPengawasanPiutangLainnya})
        mnu_BukuPengawasanPiutang.Name = "mnu_BukuPengawasanPiutang"
        mnu_BukuPengawasanPiutang.Size = New Size(402, 26)
        mnu_BukuPengawasanPiutang.Text = "Buku Pengawasan Piutang"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha
        ' 
        mnu_BukuPengawasanPiutangUsaha.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanPiutangUsaha_Lokal, mnu_BukuPengawasanPiutangUsaha_Ekspor})
        mnu_BukuPengawasanPiutangUsaha.Name = "mnu_BukuPengawasanPiutangUsaha"
        mnu_BukuPengawasanPiutangUsaha.Size = New Size(386, 26)
        mnu_BukuPengawasanPiutangUsaha.Text = "Buku Pengawasan Piutang Usaha"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Lokal
        ' 
        mnu_BukuPengawasanPiutangUsaha_Lokal.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanPiutangUsaha_NonAfiliasi, mnu_BukuPengawasanPiutangUsaha_Afiliasi, mnu_BukuPengawasanPiutangUsaha_Semua})
        mnu_BukuPengawasanPiutangUsaha_Lokal.Name = "mnu_BukuPengawasanPiutangUsaha_Lokal"
        mnu_BukuPengawasanPiutangUsaha_Lokal.Size = New Size(136, 26)
        mnu_BukuPengawasanPiutangUsaha_Lokal.Text = "Lokal"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_NonAfiliasi
        ' 
        mnu_BukuPengawasanPiutangUsaha_NonAfiliasi.Name = "mnu_BukuPengawasanPiutangUsaha_NonAfiliasi"
        mnu_BukuPengawasanPiutangUsaha_NonAfiliasi.Size = New Size(169, 26)
        mnu_BukuPengawasanPiutangUsaha_NonAfiliasi.Text = "Non Afiliasi"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Afiliasi
        ' 
        mnu_BukuPengawasanPiutangUsaha_Afiliasi.Name = "mnu_BukuPengawasanPiutangUsaha_Afiliasi"
        mnu_BukuPengawasanPiutangUsaha_Afiliasi.Size = New Size(169, 26)
        mnu_BukuPengawasanPiutangUsaha_Afiliasi.Text = "Afiliasi"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Semua
        ' 
        mnu_BukuPengawasanPiutangUsaha_Semua.Name = "mnu_BukuPengawasanPiutangUsaha_Semua"
        mnu_BukuPengawasanPiutangUsaha_Semua.Size = New Size(169, 26)
        mnu_BukuPengawasanPiutangUsaha_Semua.Text = "Semua"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Ekspor
        ' 
        mnu_BukuPengawasanPiutangUsaha_Ekspor.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanPiutangUsaha_Ekspor_USD, mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD, mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY, mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY, mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR, mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD, mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP})
        mnu_BukuPengawasanPiutangUsaha_Ekspor.Name = "mnu_BukuPengawasanPiutangUsaha_Ekspor"
        mnu_BukuPengawasanPiutangUsaha_Ekspor.Size = New Size(136, 26)
        mnu_BukuPengawasanPiutangUsaha_Ekspor.Text = "Ekspor"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Ekspor_USD
        ' 
        mnu_BukuPengawasanPiutangUsaha_Ekspor_USD.Name = "mnu_BukuPengawasanPiutangUsaha_Ekspor_USD"
        mnu_BukuPengawasanPiutangUsaha_Ekspor_USD.Size = New Size(123, 26)
        mnu_BukuPengawasanPiutangUsaha_Ekspor_USD.Text = "USD"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD
        ' 
        mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD.Name = "mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD"
        mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD.Size = New Size(123, 26)
        mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD.Text = "AUD"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY
        ' 
        mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY.Name = "mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY"
        mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY.Size = New Size(123, 26)
        mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY.Text = "JPY"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY
        ' 
        mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY.Name = "mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY"
        mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY.Size = New Size(123, 26)
        mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY.Text = "CNY"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR
        ' 
        mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR.Name = "mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR"
        mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR.Size = New Size(123, 26)
        mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR.Text = "EUR"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD
        ' 
        mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD.Name = "mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD"
        mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD.Size = New Size(123, 26)
        mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD.Text = "SGD"
        ' 
        ' mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP
        ' 
        mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP.Name = "mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP"
        mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP.Size = New Size(123, 26)
        mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP.Text = "GBP"
        ' 
        ' mnu_BukuPengawasanPiutangPihakKetiga
        ' 
        mnu_BukuPengawasanPiutangPihakKetiga.Name = "mnu_BukuPengawasanPiutangPihakKetiga"
        mnu_BukuPengawasanPiutangPihakKetiga.Size = New Size(386, 26)
        mnu_BukuPengawasanPiutangPihakKetiga.Text = "Buku Pengawasan Piutang Pihak Ketiga"
        ' 
        ' mnu_BukuPengawasanPiutangAfiliasi
        ' 
        mnu_BukuPengawasanPiutangAfiliasi.Name = "mnu_BukuPengawasanPiutangAfiliasi"
        mnu_BukuPengawasanPiutangAfiliasi.Size = New Size(386, 26)
        mnu_BukuPengawasanPiutangAfiliasi.Text = "Buku Pengawasan Piutang Afiliasi"
        ' 
        ' mnu_BukuPengawasanPiutangKaryawan
        ' 
        mnu_BukuPengawasanPiutangKaryawan.Name = "mnu_BukuPengawasanPiutangKaryawan"
        mnu_BukuPengawasanPiutangKaryawan.Size = New Size(386, 26)
        mnu_BukuPengawasanPiutangKaryawan.Text = "Buku Pengawasan Piutang Karyawan"
        ' 
        ' mnu_BukuPengawasanPiutangPemegangSaham
        ' 
        mnu_BukuPengawasanPiutangPemegangSaham.Name = "mnu_BukuPengawasanPiutangPemegangSaham"
        mnu_BukuPengawasanPiutangPemegangSaham.Size = New Size(386, 26)
        mnu_BukuPengawasanPiutangPemegangSaham.Text = "Buku Pengawasan Piutang Pemegang Saham"
        ' 
        ' mnu_BukuPengawasanDepositOperasional
        ' 
        mnu_BukuPengawasanDepositOperasional.Name = "mnu_BukuPengawasanDepositOperasional"
        mnu_BukuPengawasanDepositOperasional.Size = New Size(386, 26)
        mnu_BukuPengawasanDepositOperasional.Text = "Buku Pengawasan Deposit Operasional"
        ' 
        ' mnu_BukuPengawasanPiutangDividen
        ' 
        mnu_BukuPengawasanPiutangDividen.Name = "mnu_BukuPengawasanPiutangDividen"
        mnu_BukuPengawasanPiutangDividen.Size = New Size(386, 26)
        mnu_BukuPengawasanPiutangDividen.Text = "Buku Pengawasan Piutang Dividen"
        ' 
        ' mnu_BukuPengawasanPiutangLainnya
        ' 
        mnu_BukuPengawasanPiutangLainnya.Name = "mnu_BukuPengawasanPiutangLainnya"
        mnu_BukuPengawasanPiutangLainnya.Size = New Size(386, 26)
        mnu_BukuPengawasanPiutangLainnya.Text = "Buku Pengawasan Piutang Lainnya"
        ' 
        ' mnu_BukuPengawasanBuktiPenerimaanBankCash
        ' 
        mnu_BukuPengawasanBuktiPenerimaanBankCash.Name = "mnu_BukuPengawasanBuktiPenerimaanBankCash"
        mnu_BukuPengawasanBuktiPenerimaanBankCash.Size = New Size(402, 26)
        mnu_BukuPengawasanBuktiPenerimaanBankCash.Text = "Buku Pengawasan Bukti Penerimaan Bank Cash"
        ' 
        ' mnu_BukuPengawasanBuktiPengeluaranBankCash
        ' 
        mnu_BukuPengawasanBuktiPengeluaranBankCash.Name = "mnu_BukuPengawasanBuktiPengeluaranBankCash"
        mnu_BukuPengawasanBuktiPengeluaranBankCash.Size = New Size(402, 26)
        mnu_BukuPengawasanBuktiPengeluaranBankCash.Text = "Buku Pengawasan Bukti Pengeluaran Bank Cash"
        ' 
        ' mnu_BukuPengawasanPemindabukuan
        ' 
        mnu_BukuPengawasanPemindabukuan.Name = "mnu_BukuPengawasanPemindabukuan"
        mnu_BukuPengawasanPemindabukuan.Size = New Size(402, 26)
        mnu_BukuPengawasanPemindabukuan.Text = "Buku Pengawasan Pemindabukuan"
        ' 
        ' mnu_BukuPengawasanAktivaLainnya
        ' 
        mnu_BukuPengawasanAktivaLainnya.Name = "mnu_BukuPengawasanAktivaLainnya"
        mnu_BukuPengawasanAktivaLainnya.Size = New Size(402, 26)
        mnu_BukuPengawasanAktivaLainnya.Text = "Buku Pengawasan Aktiva Lain-lain"
        ' 
        ' mnu_Pengajuan
        ' 
        mnu_Pengajuan.DropDownItems.AddRange(New ToolStripItem() {mnu_PengajuanPembayaranPembelianTunai, mnu_PengajuanPembayaranHutangUsaha, mnu_PengajuanPembayaranHutangPajak, mnu_PengajuanPembayaranHutangBank, mnu_PengajuanPembayaranHutangLeasing, mnu_PengajuanPembayaranHutangAfiliasi, mnu_PengajuanPembayaranHutangLainnya, mnu_PengajuanPembayaranKasbon, mnu_PengajuanPembayaranInvestasi, mnu_PengajuanPemindahbukuan, mnu_PengajuanLainnya, mnu_PengajuanPO})
        mnu_Pengajuan.Name = "mnu_Pengajuan"
        mnu_Pengajuan.Size = New Size(91, 24)
        mnu_Pengajuan.Text = "Pengajuan"
        ' 
        ' mnu_PengajuanPembayaranPembelianTunai
        ' 
        mnu_PengajuanPembayaranPembelianTunai.Name = "mnu_PengajuanPembayaranPembelianTunai"
        mnu_PengajuanPembayaranPembelianTunai.Size = New Size(358, 26)
        mnu_PengajuanPembayaranPembelianTunai.Text = "Pengajuan Pembayaran Pembelian Tunai"
        ' 
        ' mnu_PengajuanPembayaranHutangUsaha
        ' 
        mnu_PengajuanPembayaranHutangUsaha.Name = "mnu_PengajuanPembayaranHutangUsaha"
        mnu_PengajuanPembayaranHutangUsaha.Size = New Size(358, 26)
        mnu_PengajuanPembayaranHutangUsaha.Text = "Pengajuan Pembayaran Hutang Usaha"
        ' 
        ' mnu_PengajuanPembayaranHutangPajak
        ' 
        mnu_PengajuanPembayaranHutangPajak.Name = "mnu_PengajuanPembayaranHutangPajak"
        mnu_PengajuanPembayaranHutangPajak.Size = New Size(358, 26)
        mnu_PengajuanPembayaranHutangPajak.Text = "Pengajuan Pembayaran Hutang Pajak"
        ' 
        ' mnu_PengajuanPembayaranHutangBank
        ' 
        mnu_PengajuanPembayaranHutangBank.Name = "mnu_PengajuanPembayaranHutangBank"
        mnu_PengajuanPembayaranHutangBank.Size = New Size(358, 26)
        mnu_PengajuanPembayaranHutangBank.Text = "Pengajuan Pembayaran Hutang Bank"
        ' 
        ' mnu_PengajuanPembayaranHutangLeasing
        ' 
        mnu_PengajuanPembayaranHutangLeasing.Name = "mnu_PengajuanPembayaranHutangLeasing"
        mnu_PengajuanPembayaranHutangLeasing.Size = New Size(358, 26)
        mnu_PengajuanPembayaranHutangLeasing.Text = "Pengajuan Pembayaran Hutang Leasing"
        ' 
        ' mnu_PengajuanPembayaranHutangAfiliasi
        ' 
        mnu_PengajuanPembayaranHutangAfiliasi.Name = "mnu_PengajuanPembayaranHutangAfiliasi"
        mnu_PengajuanPembayaranHutangAfiliasi.Size = New Size(358, 26)
        mnu_PengajuanPembayaranHutangAfiliasi.Text = "Pengajuan Pembayaran Hutang Afiliasi"
        ' 
        ' mnu_PengajuanPembayaranHutangLainnya
        ' 
        mnu_PengajuanPembayaranHutangLainnya.Name = "mnu_PengajuanPembayaranHutangLainnya"
        mnu_PengajuanPembayaranHutangLainnya.Size = New Size(358, 26)
        mnu_PengajuanPembayaranHutangLainnya.Text = "Pengajuan Pembayaran Hutang Lainnya"
        ' 
        ' mnu_PengajuanPembayaranKasbon
        ' 
        mnu_PengajuanPembayaranKasbon.Name = "mnu_PengajuanPembayaranKasbon"
        mnu_PengajuanPembayaranKasbon.Size = New Size(358, 26)
        mnu_PengajuanPembayaranKasbon.Text = "Pengajuan Pembayaran Kasbon"
        ' 
        ' mnu_PengajuanPembayaranInvestasi
        ' 
        mnu_PengajuanPembayaranInvestasi.Name = "mnu_PengajuanPembayaranInvestasi"
        mnu_PengajuanPembayaranInvestasi.Size = New Size(358, 26)
        mnu_PengajuanPembayaranInvestasi.Text = "Pengajuan Pembayaran Investasi"
        ' 
        ' mnu_PengajuanPemindahbukuan
        ' 
        mnu_PengajuanPemindahbukuan.Name = "mnu_PengajuanPemindahbukuan"
        mnu_PengajuanPemindahbukuan.Size = New Size(358, 26)
        mnu_PengajuanPemindahbukuan.Text = "Pengajuan Pemindahbukuan"
        ' 
        ' mnu_PengajuanLainnya
        ' 
        mnu_PengajuanLainnya.Name = "mnu_PengajuanLainnya"
        mnu_PengajuanLainnya.Size = New Size(358, 26)
        mnu_PengajuanLainnya.Text = "Pengajuan Lainnya"
        ' 
        ' mnu_PengajuanPO
        ' 
        mnu_PengajuanPO.Name = "mnu_PengajuanPO"
        mnu_PengajuanPO.Size = New Size(358, 26)
        mnu_PengajuanPO.Text = "Pengajuan PO"
        ' 
        ' mnu_StockOpname
        ' 
        mnu_StockOpname.DropDownItems.AddRange(New ToolStripItem() {mnu_StockOpname_BahanPenolong, mnu_StockOpname_BahanBaku, mnu_StockOpname_BarangDalamProses, mnu_StockOpname_BarangJadi})
        mnu_StockOpname.Name = "mnu_StockOpname"
        mnu_StockOpname.Size = New Size(120, 24)
        mnu_StockOpname.Text = "Stock Opname"
        ' 
        ' mnu_StockOpname_BahanPenolong
        ' 
        mnu_StockOpname_BahanPenolong.Name = "mnu_StockOpname_BahanPenolong"
        mnu_StockOpname_BahanPenolong.Size = New Size(233, 26)
        mnu_StockOpname_BahanPenolong.Text = "Bahan Penolong"
        ' 
        ' mnu_StockOpname_BahanBaku
        ' 
        mnu_StockOpname_BahanBaku.Name = "mnu_StockOpname_BahanBaku"
        mnu_StockOpname_BahanBaku.Size = New Size(233, 26)
        mnu_StockOpname_BahanBaku.Text = "Bahan Baku"
        ' 
        ' mnu_StockOpname_BarangDalamProses
        ' 
        mnu_StockOpname_BarangDalamProses.DropDownItems.AddRange(New ToolStripItem() {mnu_StockOpname_BarangDalamProses_CekFisik, mnu_StockOpname_BarangDalamProses_TarikanData})
        mnu_StockOpname_BarangDalamProses.Name = "mnu_StockOpname_BarangDalamProses"
        mnu_StockOpname_BarangDalamProses.Size = New Size(233, 26)
        mnu_StockOpname_BarangDalamProses.Text = "Barang Dalam Proses"
        ' 
        ' mnu_StockOpname_BarangDalamProses_CekFisik
        ' 
        mnu_StockOpname_BarangDalamProses_CekFisik.Name = "mnu_StockOpname_BarangDalamProses_CekFisik"
        mnu_StockOpname_BarangDalamProses_CekFisik.Size = New Size(174, 26)
        mnu_StockOpname_BarangDalamProses_CekFisik.Text = "Cek Fisik"
        ' 
        ' mnu_StockOpname_BarangDalamProses_TarikanData
        ' 
        mnu_StockOpname_BarangDalamProses_TarikanData.Name = "mnu_StockOpname_BarangDalamProses_TarikanData"
        mnu_StockOpname_BarangDalamProses_TarikanData.Size = New Size(174, 26)
        mnu_StockOpname_BarangDalamProses_TarikanData.Text = "Tarikan Data"
        ' 
        ' mnu_StockOpname_BarangJadi
        ' 
        mnu_StockOpname_BarangJadi.Name = "mnu_StockOpname_BarangJadi"
        mnu_StockOpname_BarangJadi.Size = New Size(233, 26)
        mnu_StockOpname_BarangJadi.Text = "Barang Jadi"
        ' 
        ' mnu_Akuntansi
        ' 
        mnu_Akuntansi.DropDownItems.AddRange(New ToolStripItem() {mnu_Jurnal, mnu_BukuBesar, mnu_Laporan})
        mnu_Akuntansi.Name = "mnu_Akuntansi"
        mnu_Akuntansi.Size = New Size(87, 24)
        mnu_Akuntansi.Text = "Akuntansi"
        ' 
        ' mnu_Jurnal
        ' 
        mnu_Jurnal.DropDownItems.AddRange(New ToolStripItem() {mnu_JurnalUmum, mnu_JurnalAdjusment})
        mnu_Jurnal.Name = "mnu_Jurnal"
        mnu_Jurnal.Size = New Size(164, 26)
        mnu_Jurnal.Text = "Jurnal"
        ' 
        ' mnu_JurnalUmum
        ' 
        mnu_JurnalUmum.Name = "mnu_JurnalUmum"
        mnu_JurnalUmum.Size = New Size(205, 26)
        mnu_JurnalUmum.Text = "Jurnal Umum"
        ' 
        ' mnu_JurnalAdjusment
        ' 
        mnu_JurnalAdjusment.DropDownItems.AddRange(New ToolStripItem() {mnu_JurnalAdjusment_Penyusutan, mnu_JurnalAdjusment_Amortisasi, mnu_JurnalAdjusment_Forex, mnu_JurnalAdjusment_HPP})
        mnu_JurnalAdjusment.Name = "mnu_JurnalAdjusment"
        mnu_JurnalAdjusment.Size = New Size(205, 26)
        mnu_JurnalAdjusment.Text = "Jurnal Adjusment"
        ' 
        ' mnu_JurnalAdjusment_Penyusutan
        ' 
        mnu_JurnalAdjusment_Penyusutan.Name = "mnu_JurnalAdjusment_Penyusutan"
        mnu_JurnalAdjusment_Penyusutan.Size = New Size(165, 26)
        mnu_JurnalAdjusment_Penyusutan.Text = "Penyusutan"
        ' 
        ' mnu_JurnalAdjusment_Amortisasi
        ' 
        mnu_JurnalAdjusment_Amortisasi.Name = "mnu_JurnalAdjusment_Amortisasi"
        mnu_JurnalAdjusment_Amortisasi.Size = New Size(165, 26)
        mnu_JurnalAdjusment_Amortisasi.Text = "Amortisasi"
        ' 
        ' mnu_JurnalAdjusment_Forex
        ' 
        mnu_JurnalAdjusment_Forex.Name = "mnu_JurnalAdjusment_Forex"
        mnu_JurnalAdjusment_Forex.Size = New Size(165, 26)
        mnu_JurnalAdjusment_Forex.Text = "Forex"
        ' 
        ' mnu_JurnalAdjusment_HPP
        ' 
        mnu_JurnalAdjusment_HPP.Name = "mnu_JurnalAdjusment_HPP"
        mnu_JurnalAdjusment_HPP.Size = New Size(165, 26)
        mnu_JurnalAdjusment_HPP.Text = "HPP"
        ' 
        ' mnu_BukuBesar
        ' 
        mnu_BukuBesar.Name = "mnu_BukuBesar"
        mnu_BukuBesar.Size = New Size(164, 26)
        mnu_BukuBesar.Text = "Buku Besar"
        ' 
        ' mnu_Laporan
        ' 
        mnu_Laporan.DropDownItems.AddRange(New ToolStripItem() {mnu_TrialBalance, mnu_NeracaLajur, mnu_LaporanKeuangan, mnu_LaporanAktivitasTransaksi})
        mnu_Laporan.Name = "mnu_Laporan"
        mnu_Laporan.Size = New Size(164, 26)
        mnu_Laporan.Text = "Laporan"
        ' 
        ' mnu_TrialBalance
        ' 
        mnu_TrialBalance.Name = "mnu_TrialBalance"
        mnu_TrialBalance.Size = New Size(264, 26)
        mnu_TrialBalance.Text = "Trial Balance"
        mnu_TrialBalance.Visible = False
        ' 
        ' mnu_NeracaLajur
        ' 
        mnu_NeracaLajur.Name = "mnu_NeracaLajur"
        mnu_NeracaLajur.Size = New Size(264, 26)
        mnu_NeracaLajur.Text = "Neraca Lajur"
        ' 
        ' mnu_LaporanKeuangan
        ' 
        mnu_LaporanKeuangan.DropDownItems.AddRange(New ToolStripItem() {mnu_LaporanHPP, mnu_LabaRugi, mnu_Neraca})
        mnu_LaporanKeuangan.Name = "mnu_LaporanKeuangan"
        mnu_LaporanKeuangan.Size = New Size(264, 26)
        mnu_LaporanKeuangan.Text = "Laporan Keuangan"
        ' 
        ' mnu_LaporanHPP
        ' 
        mnu_LaporanHPP.Name = "mnu_LaporanHPP"
        mnu_LaporanHPP.Size = New Size(218, 26)
        mnu_LaporanHPP.Text = "Laporan HPP"
        ' 
        ' mnu_LabaRugi
        ' 
        mnu_LabaRugi.DropDownItems.AddRange(New ToolStripItem() {mnu_LabaRugi_Bulanan, mnu_LabaRugi_Tahunan})
        mnu_LabaRugi.Name = "mnu_LabaRugi"
        mnu_LabaRugi.Size = New Size(218, 26)
        mnu_LabaRugi.Text = "Laporan Rugi-Laba"
        ' 
        ' mnu_LabaRugi_Bulanan
        ' 
        mnu_LabaRugi_Bulanan.Name = "mnu_LabaRugi_Bulanan"
        mnu_LabaRugi_Bulanan.Size = New Size(146, 26)
        mnu_LabaRugi_Bulanan.Text = "Bulanan"
        ' 
        ' mnu_LabaRugi_Tahunan
        ' 
        mnu_LabaRugi_Tahunan.Name = "mnu_LabaRugi_Tahunan"
        mnu_LabaRugi_Tahunan.Size = New Size(146, 26)
        mnu_LabaRugi_Tahunan.Text = "Tahunan"
        ' 
        ' mnu_Neraca
        ' 
        mnu_Neraca.DropDownItems.AddRange(New ToolStripItem() {mnu_Neraca_Bulanan, mnu_Neraca_Tahunan})
        mnu_Neraca.Name = "mnu_Neraca"
        mnu_Neraca.Size = New Size(218, 26)
        mnu_Neraca.Text = "Neraca"
        ' 
        ' mnu_Neraca_Bulanan
        ' 
        mnu_Neraca_Bulanan.Name = "mnu_Neraca_Bulanan"
        mnu_Neraca_Bulanan.Size = New Size(146, 26)
        mnu_Neraca_Bulanan.Text = "Bulanan"
        ' 
        ' mnu_Neraca_Tahunan
        ' 
        mnu_Neraca_Tahunan.Name = "mnu_Neraca_Tahunan"
        mnu_Neraca_Tahunan.Size = New Size(146, 26)
        mnu_Neraca_Tahunan.Text = "Tahunan"
        ' 
        ' mnu_LaporanAktivitasTransaksi
        ' 
        mnu_LaporanAktivitasTransaksi.Name = "mnu_LaporanAktivitasTransaksi"
        mnu_LaporanAktivitasTransaksi.Size = New Size(264, 26)
        mnu_LaporanAktivitasTransaksi.Text = "Laporan Akivitas Transaksi"
        ' 
        ' mnu_ManajemenAsset
        ' 
        mnu_ManajemenAsset.DropDownItems.AddRange(New ToolStripItem() {mnu_ManajemenAmortisasi, mnu_ManajemenAssetTetap})
        mnu_ManajemenAsset.Name = "mnu_ManajemenAsset"
        mnu_ManajemenAsset.Size = New Size(140, 24)
        mnu_ManajemenAsset.Text = "Manajemen Asset"
        ' 
        ' mnu_ManajemenAmortisasi
        ' 
        mnu_ManajemenAmortisasi.DropDownItems.AddRange(New ToolStripItem() {mnu_ManajemenAmortisasiBiaya, mnu_ManajemenAmortisasiAssetTidakBerwujud})
        mnu_ManajemenAmortisasi.Name = "mnu_ManajemenAmortisasi"
        mnu_ManajemenAmortisasi.Size = New Size(250, 26)
        mnu_ManajemenAmortisasi.Text = "Manajemen Amortisasi"
        ' 
        ' mnu_ManajemenAmortisasiBiaya
        ' 
        mnu_ManajemenAmortisasiBiaya.Name = "mnu_ManajemenAmortisasiBiaya"
        mnu_ManajemenAmortisasiBiaya.Size = New Size(307, 26)
        mnu_ManajemenAmortisasiBiaya.Text = "Amortisasi Biaya"
        ' 
        ' mnu_ManajemenAmortisasiAssetTidakBerwujud
        ' 
        mnu_ManajemenAmortisasiAssetTidakBerwujud.Name = "mnu_ManajemenAmortisasiAssetTidakBerwujud"
        mnu_ManajemenAmortisasiAssetTidakBerwujud.Size = New Size(307, 26)
        mnu_ManajemenAmortisasiAssetTidakBerwujud.Text = "Amortisasi Asset Tidak Berwujud"
        ' 
        ' mnu_ManajemenAssetTetap
        ' 
        mnu_ManajemenAssetTetap.DropDownItems.AddRange(New ToolStripItem() {mnu_DaftarPenyusutanAssetTetap, mnu_BukuPenjualanAssetTetap, mnu_BukuDisposalAssetTetap})
        mnu_ManajemenAssetTetap.Name = "mnu_ManajemenAssetTetap"
        mnu_ManajemenAssetTetap.Size = New Size(250, 26)
        mnu_ManajemenAssetTetap.Text = "Manajemen Asset Tetap"
        ' 
        ' mnu_DaftarPenyusutanAssetTetap
        ' 
        mnu_DaftarPenyusutanAssetTetap.Name = "mnu_DaftarPenyusutanAssetTetap"
        mnu_DaftarPenyusutanAssetTetap.Size = New Size(291, 26)
        mnu_DaftarPenyusutanAssetTetap.Text = "Daftar Penyusutan Asset Tetap"
        ' 
        ' mnu_BukuPenjualanAssetTetap
        ' 
        mnu_BukuPenjualanAssetTetap.Name = "mnu_BukuPenjualanAssetTetap"
        mnu_BukuPenjualanAssetTetap.Size = New Size(291, 26)
        mnu_BukuPenjualanAssetTetap.Text = "Buku Penjualan Asset Tetap"
        ' 
        ' mnu_BukuDisposalAssetTetap
        ' 
        mnu_BukuDisposalAssetTetap.Name = "mnu_BukuDisposalAssetTetap"
        mnu_BukuDisposalAssetTetap.Size = New Size(291, 26)
        mnu_BukuDisposalAssetTetap.Text = "Buku Disposal Asset Tetap"
        ' 
        ' mnu_Pajak
        ' 
        mnu_Pajak.DropDownItems.AddRange(New ToolStripItem() {mnu_ProfilPajakPerusahaan, mnu_PerhitunganPajakPajakBulanan, mnu_BukuPengawasanHutangPajak, mnu_InputDokumenPerpajakan, mnu_PerhitunganEqualisasiPajakPajakTahunan, mnu_BukuPengawasanBuktiPotongPPh_Paid, mnu_BukuPengawasanBuktiPotongPPh_Prepaid})
        mnu_Pajak.Name = "mnu_Pajak"
        mnu_Pajak.Size = New Size(57, 24)
        mnu_Pajak.Text = "Pajak"
        ' 
        ' mnu_ProfilPajakPerusahaan
        ' 
        mnu_ProfilPajakPerusahaan.Name = "mnu_ProfilPajakPerusahaan"
        mnu_ProfilPajakPerusahaan.Size = New Size(438, 26)
        mnu_ProfilPajakPerusahaan.Text = "Profil Pajak Perusahaan"
        ' 
        ' mnu_PerhitunganPajakPajakBulanan
        ' 
        mnu_PerhitunganPajakPajakBulanan.Name = "mnu_PerhitunganPajakPajakBulanan"
        mnu_PerhitunganPajakPajakBulanan.Size = New Size(438, 26)
        mnu_PerhitunganPajakPajakBulanan.Text = "Perhitungan Pajak-Pajak Bulanan"
        ' 
        ' mnu_BukuPengawasanHutangPajak
        ' 
        mnu_BukuPengawasanHutangPajak.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanHutangPPhPasal21, mnu_BukuPengawasanHutangPPhPasal22, mnu_BukuPengawasanHutangPPhPasal23, mnu_BukuPengawasanHutangPPhPasal42, mnu_BukuPengawasanHutangPPhPasal25, mnu_BukuPengawasanHutangPPhPasal26, mnu_BukuPengawasanHutangPPhPasal29, mnu_PPN, mnu_KetetapanPajak, mnu_PajakImpor})
        mnu_BukuPengawasanHutangPajak.Name = "mnu_BukuPengawasanHutangPajak"
        mnu_BukuPengawasanHutangPajak.Size = New Size(438, 26)
        mnu_BukuPengawasanHutangPajak.Text = "Buku Pengawasan Hutang Pajak"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal21
        ' 
        mnu_BukuPengawasanHutangPPhPasal21.Name = "mnu_BukuPengawasanHutangPPhPasal21"
        mnu_BukuPengawasanHutangPPhPasal21.Size = New Size(212, 26)
        mnu_BukuPengawasanHutangPPhPasal21.Text = "PPh Pasal 21"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal22
        ' 
        mnu_BukuPengawasanHutangPPhPasal22.DropDownItems.AddRange(New ToolStripItem() {mnu_BukuPengawasanHutangPPhPasal22_Lokal, mnu_BukuPengawasanHutangPPhPasal22_Impor})
        mnu_BukuPengawasanHutangPPhPasal22.Name = "mnu_BukuPengawasanHutangPPhPasal22"
        mnu_BukuPengawasanHutangPPhPasal22.Size = New Size(212, 26)
        mnu_BukuPengawasanHutangPPhPasal22.Text = "PPh Pasal 22"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal22_Lokal
        ' 
        mnu_BukuPengawasanHutangPPhPasal22_Lokal.Name = "mnu_BukuPengawasanHutangPPhPasal22_Lokal"
        mnu_BukuPengawasanHutangPPhPasal22_Lokal.Size = New Size(132, 26)
        mnu_BukuPengawasanHutangPPhPasal22_Lokal.Text = "Lokal"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal22_Impor
        ' 
        mnu_BukuPengawasanHutangPPhPasal22_Impor.Name = "mnu_BukuPengawasanHutangPPhPasal22_Impor"
        mnu_BukuPengawasanHutangPPhPasal22_Impor.Size = New Size(132, 26)
        mnu_BukuPengawasanHutangPPhPasal22_Impor.Text = "Impor"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal23
        ' 
        mnu_BukuPengawasanHutangPPhPasal23.Name = "mnu_BukuPengawasanHutangPPhPasal23"
        mnu_BukuPengawasanHutangPPhPasal23.Size = New Size(212, 26)
        mnu_BukuPengawasanHutangPPhPasal23.Text = "PPh Pasal 23"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal42
        ' 
        mnu_BukuPengawasanHutangPPhPasal42.Name = "mnu_BukuPengawasanHutangPPhPasal42"
        mnu_BukuPengawasanHutangPPhPasal42.Size = New Size(212, 26)
        mnu_BukuPengawasanHutangPPhPasal42.Text = "PPh Pasal 4 (2)"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal25
        ' 
        mnu_BukuPengawasanHutangPPhPasal25.Name = "mnu_BukuPengawasanHutangPPhPasal25"
        mnu_BukuPengawasanHutangPPhPasal25.Size = New Size(212, 26)
        mnu_BukuPengawasanHutangPPhPasal25.Text = "PPh Pasal 25"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal26
        ' 
        mnu_BukuPengawasanHutangPPhPasal26.Name = "mnu_BukuPengawasanHutangPPhPasal26"
        mnu_BukuPengawasanHutangPPhPasal26.Size = New Size(212, 26)
        mnu_BukuPengawasanHutangPPhPasal26.Text = "PPh Pasal 26"
        ' 
        ' mnu_BukuPengawasanHutangPPhPasal29
        ' 
        mnu_BukuPengawasanHutangPPhPasal29.Name = "mnu_BukuPengawasanHutangPPhPasal29"
        mnu_BukuPengawasanHutangPPhPasal29.Size = New Size(212, 26)
        mnu_BukuPengawasanHutangPPhPasal29.Text = "PPh Pasal 29"
        ' 
        ' mnu_PPN
        ' 
        mnu_PPN.Name = "mnu_PPN"
        mnu_PPN.Size = New Size(212, 26)
        mnu_PPN.Text = "PPN"
        ' 
        ' mnu_KetetapanPajak
        ' 
        mnu_KetetapanPajak.Name = "mnu_KetetapanPajak"
        mnu_KetetapanPajak.Size = New Size(212, 26)
        mnu_KetetapanPajak.Text = "Ketetapan Pajak"
        ' 
        ' mnu_PajakImpor
        ' 
        mnu_PajakImpor.Name = "mnu_PajakImpor"
        mnu_PajakImpor.Size = New Size(212, 26)
        mnu_PajakImpor.Text = "Pajak-pajak Impor"
        ' 
        ' mnu_InputDokumenPerpajakan
        ' 
        mnu_InputDokumenPerpajakan.DropDownItems.AddRange(New ToolStripItem() {mnu_InputBuktiPBk, mnu_InputKetetapanPajak})
        mnu_InputDokumenPerpajakan.Name = "mnu_InputDokumenPerpajakan"
        mnu_InputDokumenPerpajakan.Size = New Size(438, 26)
        mnu_InputDokumenPerpajakan.Text = "Input Dokumen Perpajakan"
        ' 
        ' mnu_InputBuktiPBk
        ' 
        mnu_InputBuktiPBk.Name = "mnu_InputBuktiPBk"
        mnu_InputBuktiPBk.Size = New Size(236, 26)
        mnu_InputBuktiPBk.Text = "Input Bukti PBk"
        ' 
        ' mnu_InputKetetapanPajak
        ' 
        mnu_InputKetetapanPajak.Name = "mnu_InputKetetapanPajak"
        mnu_InputKetetapanPajak.Size = New Size(236, 26)
        mnu_InputKetetapanPajak.Text = "Input Ketetapan Pajak"
        ' 
        ' mnu_PerhitunganEqualisasiPajakPajakTahunan
        ' 
        mnu_PerhitunganEqualisasiPajakPajakTahunan.Name = "mnu_PerhitunganEqualisasiPajakPajakTahunan"
        mnu_PerhitunganEqualisasiPajakPajakTahunan.Size = New Size(438, 26)
        mnu_PerhitunganEqualisasiPajakPajakTahunan.Text = "Perhitungan Equalisasi Pajak-Pajak Tahunan"
        ' 
        ' mnu_BukuPengawasanBuktiPotongPPh_Paid
        ' 
        mnu_BukuPengawasanBuktiPotongPPh_Paid.Name = "mnu_BukuPengawasanBuktiPotongPPh_Paid"
        mnu_BukuPengawasanBuktiPotongPPh_Paid.Size = New Size(438, 26)
        mnu_BukuPengawasanBuktiPotongPPh_Paid.Text = "Buku Pengawasan Bukti Potong PPh (21, 23, 4 (2), 26)"
        ' 
        ' mnu_BukuPengawasanBuktiPotongPPh_Prepaid
        ' 
        mnu_BukuPengawasanBuktiPotongPPh_Prepaid.Name = "mnu_BukuPengawasanBuktiPotongPPh_Prepaid"
        mnu_BukuPengawasanBuktiPotongPPh_Prepaid.Size = New Size(438, 26)
        mnu_BukuPengawasanBuktiPotongPPh_Prepaid.Text = "Buku Pengawasan Bukti Potong PPh (Prepaid)"
        ' 
        ' mnu_User
        ' 
        mnu_User.DropDownItems.AddRange(New ToolStripItem() {mnu_SwitchUser, mnu_GantiPassword, mnu_GantiPeran, mnu_Log})
        mnu_User.Name = "mnu_User"
        mnu_User.Size = New Size(52, 24)
        mnu_User.Text = "User"
        ' 
        ' mnu_SwitchUser
        ' 
        mnu_SwitchUser.Name = "mnu_SwitchUser"
        mnu_SwitchUser.Size = New Size(192, 26)
        mnu_SwitchUser.Text = "Switch User"
        ' 
        ' mnu_GantiPassword
        ' 
        mnu_GantiPassword.Name = "mnu_GantiPassword"
        mnu_GantiPassword.Size = New Size(192, 26)
        mnu_GantiPassword.Text = "Ganti Password"
        ' 
        ' mnu_GantiPeran
        ' 
        mnu_GantiPeran.DropDownItems.AddRange(New ToolStripItem() {mnu_PeranOperator, mnu_PeranManager, mnu_PeranDirektur, mnu_PeranTimIT, mnu_PeranAppDeveloper})
        mnu_GantiPeran.Name = "mnu_GantiPeran"
        mnu_GantiPeran.Size = New Size(192, 26)
        mnu_GantiPeran.Text = "Ganti Peran"
        ' 
        ' mnu_PeranOperator
        ' 
        mnu_PeranOperator.Name = "mnu_PeranOperator"
        mnu_PeranOperator.Size = New Size(193, 26)
        mnu_PeranOperator.Text = "Operator"
        ' 
        ' mnu_PeranManager
        ' 
        mnu_PeranManager.Name = "mnu_PeranManager"
        mnu_PeranManager.Size = New Size(193, 26)
        mnu_PeranManager.Text = "Manager"
        ' 
        ' mnu_PeranDirektur
        ' 
        mnu_PeranDirektur.Name = "mnu_PeranDirektur"
        mnu_PeranDirektur.Size = New Size(193, 26)
        mnu_PeranDirektur.Text = "Direktur"
        ' 
        ' mnu_PeranTimIT
        ' 
        mnu_PeranTimIT.Name = "mnu_PeranTimIT"
        mnu_PeranTimIT.Size = New Size(193, 26)
        mnu_PeranTimIT.Text = "Tim IT"
        ' 
        ' mnu_PeranAppDeveloper
        ' 
        mnu_PeranAppDeveloper.Name = "mnu_PeranAppDeveloper"
        mnu_PeranAppDeveloper.Size = New Size(193, 26)
        mnu_PeranAppDeveloper.Text = "App Developer"
        ' 
        ' mnu_Log
        ' 
        mnu_Log.Name = "mnu_Log"
        mnu_Log.Size = New Size(192, 26)
        mnu_Log.Text = "Log"
        ' 
        ' mnu_Jendela
        ' 
        mnu_Jendela.DropDownItems.AddRange(New ToolStripItem() {mnu_Jendela_TutupSemua})
        mnu_Jendela.Name = "mnu_Jendela"
        mnu_Jendela.Size = New Size(73, 24)
        mnu_Jendela.Text = "Jendela"
        ' 
        ' mnu_Jendela_TutupSemua
        ' 
        mnu_Jendela_TutupSemua.Name = "mnu_Jendela_TutupSemua"
        mnu_Jendela_TutupSemua.Size = New Size(179, 26)
        mnu_Jendela_TutupSemua.Text = "Tutup Semua"
        ' 
        ' mnu_Tentang
        ' 
        mnu_Tentang.Name = "mnu_Tentang"
        mnu_Tentang.Size = New Size(76, 24)
        mnu_Tentang.Text = "Tentang"
        ' 
        ' mnu_Help
        ' 
        mnu_Help.Name = "mnu_Help"
        mnu_Help.Size = New Size(55, 24)
        mnu_Help.Text = "Help"
        ' 
        ' mnu_Registrasi
        ' 
        mnu_Registrasi.Name = "mnu_Registrasi"
        mnu_Registrasi.Size = New Size(87, 24)
        mnu_Registrasi.Text = "Registrasi"
        ' 
        ' mnu_Notifikasi
        ' 
        mnu_Notifikasi.Alignment = ToolStripItemAlignment.Right
        mnu_Notifikasi.Name = "mnu_Notifikasi"
        mnu_Notifikasi.Size = New Size(86, 24)
        mnu_Notifikasi.Text = "Notifikasi"
        ' 
        ' mnu_TechnicalSupport
        ' 
        mnu_TechnicalSupport.DropDownItems.AddRange(New ToolStripItem() {mnu_PhpMyAdmin})
        mnu_TechnicalSupport.Name = "mnu_TechnicalSupport"
        mnu_TechnicalSupport.Size = New Size(141, 24)
        mnu_TechnicalSupport.Text = "Technical Support"
        ' 
        ' mnu_PhpMyAdmin
        ' 
        mnu_PhpMyAdmin.Name = "mnu_PhpMyAdmin"
        mnu_PhpMyAdmin.Size = New Size(182, 26)
        mnu_PhpMyAdmin.Text = "phpMyAdmin"
        ' 
        ' mnu_AppDeveloper
        ' 
        mnu_AppDeveloper.DropDownItems.AddRange(New ToolStripItem() {mnu_ManajemenAplikasi, mnu_ManajemenClient, mnu_ManajemenKurs, mnu_DataProduk, mnu_DataPerangkat, mnu_DataVoucher, mnu_TabPokok, mnu_TryApp})
        mnu_AppDeveloper.Name = "mnu_AppDeveloper"
        mnu_AppDeveloper.Size = New Size(124, 24)
        mnu_AppDeveloper.Text = "App Developer"
        ' 
        ' mnu_ManajemenAplikasi
        ' 
        mnu_ManajemenAplikasi.Name = "mnu_ManajemenAplikasi"
        mnu_ManajemenAplikasi.Size = New Size(226, 26)
        mnu_ManajemenAplikasi.Text = "Manajemen Aplikasi"
        ' 
        ' mnu_ManajemenClient
        ' 
        mnu_ManajemenClient.Name = "mnu_ManajemenClient"
        mnu_ManajemenClient.Size = New Size(226, 26)
        mnu_ManajemenClient.Text = "Manajemen Klien"
        ' 
        ' mnu_ManajemenKurs
        ' 
        mnu_ManajemenKurs.Name = "mnu_ManajemenKurs"
        mnu_ManajemenKurs.Size = New Size(226, 26)
        mnu_ManajemenKurs.Text = "Manajemen Kurs"
        ' 
        ' mnu_DataProduk
        ' 
        mnu_DataProduk.Name = "mnu_DataProduk"
        mnu_DataProduk.Size = New Size(226, 26)
        mnu_DataProduk.Text = "Data Produk"
        ' 
        ' mnu_DataPerangkat
        ' 
        mnu_DataPerangkat.Name = "mnu_DataPerangkat"
        mnu_DataPerangkat.Size = New Size(226, 26)
        mnu_DataPerangkat.Text = "Data Perangkat"
        ' 
        ' mnu_DataVoucher
        ' 
        mnu_DataVoucher.Name = "mnu_DataVoucher"
        mnu_DataVoucher.Size = New Size(226, 26)
        mnu_DataVoucher.Text = "Data Voucher"
        ' 
        ' mnu_TabPokok
        ' 
        mnu_TabPokok.Name = "mnu_TabPokok"
        mnu_TabPokok.Size = New Size(226, 26)
        mnu_TabPokok.Text = "Tab Pokok"
        ' 
        ' mnu_TryApp
        ' 
        mnu_TryApp.Name = "mnu_TryApp"
        mnu_TryApp.Size = New Size(226, 26)
        mnu_TryApp.Text = "Try"
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(20, 20)
        StatusStrip1.Items.AddRange(New ToolStripItem() {tls_UserAktif})
        StatusStrip1.Location = New Point(0, 922)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(1, 0, 18, 0)
        StatusStrip1.Size = New Size(1717, 26)
        StatusStrip1.TabIndex = 3
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' tls_UserAktif
        ' 
        tls_UserAktif.Name = "tls_UserAktif"
        tls_UserAktif.Size = New Size(45, 20)
        tls_UserAktif.Text = "User :"
        ' 
        ' pnl_Notifikasi
        ' 
        pnl_Notifikasi.Controls.Add(dgv_Notifikasi)
        pnl_Notifikasi.Dock = DockStyle.Right
        pnl_Notifikasi.Location = New Point(1434, 30)
        pnl_Notifikasi.Margin = New Padding(5, 4, 5, 4)
        pnl_Notifikasi.Name = "pnl_Notifikasi"
        pnl_Notifikasi.Size = New Size(283, 892)
        pnl_Notifikasi.TabIndex = 5
        pnl_Notifikasi.Visible = False
        ' 
        ' dgv_Notifikasi
        ' 
        dgv_Notifikasi.AllowUserToAddRows = False
        dgv_Notifikasi.AllowUserToDeleteRows = False
        dgv_Notifikasi.AllowUserToResizeRows = False
        dgv_Notifikasi.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgv_Notifikasi.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgv_Notifikasi.BorderStyle = BorderStyle.None
        dgv_Notifikasi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv_Notifikasi.ColumnHeadersVisible = False
        dgv_Notifikasi.Columns.AddRange(New DataGridViewColumn() {Nomor_ID, Jenis_Notifikasi, Waktu_, Konten_, Halaman_Target, Pesan_Eksekusi, Status_Dibaca, Status_Dieksekusi})
        dgv_Notifikasi.Location = New Point(5, 4)
        dgv_Notifikasi.Margin = New Padding(5, 4, 5, 4)
        dgv_Notifikasi.MultiSelect = False
        dgv_Notifikasi.Name = "dgv_Notifikasi"
        dgv_Notifikasi.ReadOnly = True
        dgv_Notifikasi.RowHeadersVisible = False
        dgv_Notifikasi.RowHeadersWidth = 33
        dgv_Notifikasi.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv_Notifikasi.Size = New Size(275, 881)
        dgv_Notifikasi.TabIndex = 10041
        ' 
        ' Nomor_ID
        ' 
        Nomor_ID.HeaderText = "Nomor ID"
        Nomor_ID.MinimumWidth = 6
        Nomor_ID.Name = "Nomor_ID"
        Nomor_ID.ReadOnly = True
        Nomor_ID.SortMode = DataGridViewColumnSortMode.NotSortable
        Nomor_ID.Visible = False
        Nomor_ID.Width = 125
        ' 
        ' Jenis_Notifikasi
        ' 
        Jenis_Notifikasi.HeaderText = "Jenis Notifikasi"
        Jenis_Notifikasi.MinimumWidth = 6
        Jenis_Notifikasi.Name = "Jenis_Notifikasi"
        Jenis_Notifikasi.ReadOnly = True
        Jenis_Notifikasi.SortMode = DataGridViewColumnSortMode.NotSortable
        Jenis_Notifikasi.Visible = False
        Jenis_Notifikasi.Width = 125
        ' 
        ' Waktu_
        ' 
        Waktu_.HeaderText = "Waktu"
        Waktu_.MinimumWidth = 6
        Waktu_.Name = "Waktu_"
        Waktu_.ReadOnly = True
        Waktu_.SortMode = DataGridViewColumnSortMode.NotSortable
        Waktu_.Visible = False
        Waktu_.Width = 125
        ' 
        ' Konten_
        ' 
        DataGridViewCellStyle1.Padding = New Padding(3, 5, 3, 5)
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        Konten_.DefaultCellStyle = DataGridViewCellStyle1
        Konten_.HeaderText = "Konten"
        Konten_.MinimumWidth = 6
        Konten_.Name = "Konten_"
        Konten_.ReadOnly = True
        Konten_.SortMode = DataGridViewColumnSortMode.NotSortable
        Konten_.Width = 120
        ' 
        ' Halaman_Target
        ' 
        Halaman_Target.HeaderText = "Halaman Target"
        Halaman_Target.MinimumWidth = 6
        Halaman_Target.Name = "Halaman_Target"
        Halaman_Target.ReadOnly = True
        Halaman_Target.SortMode = DataGridViewColumnSortMode.NotSortable
        Halaman_Target.Visible = False
        Halaman_Target.Width = 125
        ' 
        ' Pesan_Eksekusi
        ' 
        Pesan_Eksekusi.HeaderText = "Pesan Eksekusi"
        Pesan_Eksekusi.MinimumWidth = 6
        Pesan_Eksekusi.Name = "Pesan_Eksekusi"
        Pesan_Eksekusi.ReadOnly = True
        Pesan_Eksekusi.SortMode = DataGridViewColumnSortMode.NotSortable
        Pesan_Eksekusi.Visible = False
        Pesan_Eksekusi.Width = 125
        ' 
        ' Status_Dibaca
        ' 
        Status_Dibaca.HeaderText = "Dibaca"
        Status_Dibaca.MinimumWidth = 6
        Status_Dibaca.Name = "Status_Dibaca"
        Status_Dibaca.ReadOnly = True
        Status_Dibaca.SortMode = DataGridViewColumnSortMode.NotSortable
        Status_Dibaca.Visible = False
        Status_Dibaca.Width = 125
        ' 
        ' Status_Dieksekusi
        ' 
        Status_Dieksekusi.HeaderText = "Dieksekusi"
        Status_Dieksekusi.MinimumWidth = 6
        Status_Dieksekusi.Name = "Status_Dieksekusi"
        Status_Dieksekusi.ReadOnly = True
        Status_Dieksekusi.SortMode = DataGridViewColumnSortMode.NotSortable
        Status_Dieksekusi.Visible = False
        Status_Dieksekusi.Width = 125
        ' 
        ' mnu_DataAwal_HutangUsaha_Impor_GBP
        ' 
        mnu_DataAwal_HutangUsaha_Impor_GBP.Name = "mnu_DataAwal_HutangUsaha_Impor_GBP"
        mnu_DataAwal_HutangUsaha_Impor_GBP.Size = New Size(224, 26)
        mnu_DataAwal_HutangUsaha_Impor_GBP.Text = "GBP"
        ' 
        ' mnu_DataAwal_PiutangUsaha_Ekspor_GBP
        ' 
        mnu_DataAwal_PiutangUsaha_Ekspor_GBP.Name = "mnu_DataAwal_PiutangUsaha_Ekspor_GBP"
        mnu_DataAwal_PiutangUsaha_Ekspor_GBP.Size = New Size(224, 26)
        mnu_DataAwal_PiutangUsaha_Ekspor_GBP.Text = "GBP"
        ' 
        ' frm_BOOKU
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1717, 948)
        Controls.Add(pnl_Notifikasi)
        Controls.Add(StatusStrip1)
        Controls.Add(mnu_MenuUtama)
        IsMdiContainer = True
        Margin = New Padding(5, 4, 5, 4)
        Name = "frm_BOOKU"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Booku"
        mnu_MenuUtama.ResumeLayout(False)
        mnu_MenuUtama.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        pnl_Notifikasi.ResumeLayout(False)
        CType(dgv_Notifikasi, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents mnu_MenuUtama As System.Windows.Forms.MenuStrip
    Friend WithEvents mnu_File As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Help As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Tentang As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Data As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataMitra As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Pajak As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPajak As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ProfilPajakPerusahaan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Pengaturan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Keluar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tls_UserAktif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnu_DataCOA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataAwal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_User As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_GantiPassword As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_CompanyProfile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Log As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Transaksi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TransaksiIN As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TransaksiOUT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PerekamanData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Adjusment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutang As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutang As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangBank As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangAfiliasi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangLainnya As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangPemegangSaham As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangKaryawan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangAfiliasi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangLainnya As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuBankCash As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuBank As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuKas As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPettyCash As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuCashAdvance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_InputPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_InputPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PencatatanTransaksiBankOtomatis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PenghasilanBunga As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PPhAtasBunga As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BiayaAdministrasi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PerekamanDataLainnya As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_BiayaPenyusutanAsset As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_BiayaAmortisasiSewaBiayaDibayarDiMuka As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_PenghapusanPiutang As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_SelisihKurs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_SelisihPencatatan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_AdjusmentLainnya As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Pengajuan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranHutangUsaha As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Pemindahbukuan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPemindahbukuan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranPembelianTunai As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Pembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_SwitchUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_AppDeveloper As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TahunBuku As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_GantiTahunBuku As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BuatBukuBaru As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_GantiPeran As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PeranOperator As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PeranManager As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PeranDirektur As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PeranAppDeveloper As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PeranTimIT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Registrasi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataProduk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataPerangkat As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataVoucher As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TryApp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal21 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal23 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal42 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal25 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal26 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal29 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranHutangPajak As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranHutangBank As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranHutangAfiliasi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranHutangLainnya As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranKasbon As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranInvestasi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_PengajuanLainnya As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_InputDokumenPerpajakan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_InputBuktiPBk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_InputKetetapanPajak As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_KetetapanPajak As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_ReturPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_InputReturPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_InputReturPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPemindabukuan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Jendela As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Jendela_TutupSemua As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TutupBuku As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPajak As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPPhPasal21 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPPhPasal23 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPPhPasal42 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPPhPasal25 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPPhPasal26 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPPhPasal29 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanGaji_Induk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Akuntansi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuBesar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Jurnal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Laporan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_TrialBalance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_Neraca As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_LabaRugi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanGaji As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangBPJSKesehatan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangBPJSKetenagakerjaan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangKoperasiKaryawan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangSerikat As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnl_Notifikasi As System.Windows.Forms.Panel
    Friend WithEvents mnu_Notifikasi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgv_Notifikasi As System.Windows.Forms.DataGridView
    Friend WithEvents Nomor_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Jenis_Notifikasi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Waktu_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Konten_ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Halaman_Target As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pesan_Eksekusi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status_Dibaca As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status_Dieksekusi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnu_PO_Pembelian As ToolStripMenuItem
    Friend WithEvents mnu_Penjualan As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPOPenjualan As ToolStripMenuItem
    Friend WithEvents mnu_BukuPenjualan As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanReturPenjualan As ToolStripMenuItem
    Friend WithEvents mnu_PPN As ToolStripMenuItem
    Friend WithEvents mnu_PerhitunganPajakPajakBulanan As ToolStripMenuItem
    Friend WithEvents mnu_PerhitunganEqualisasiPajakPajakTahunan As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan As ToolStripMenuItem
    Friend WithEvents mnu_SuratJalanPenjualan As ToolStripMenuItem
    Friend WithEvents mnu_POPenjualan_Lokal_Barang As ToolStripMenuItem
    Friend WithEvents mnu_POPenjualan_Lokal_Jasa As ToolStripMenuItem
    Friend WithEvents mnu_POPenjualan_Lokal_BarangDanJasa As ToolStripMenuItem
    Friend WithEvents mnu_POPenjualan_Lokal_JasaKonstruksi As ToolStripMenuItem
    Friend WithEvents mnu_BASTPenjualan As ToolStripMenuItem
    Friend WithEvents mnu_POPembelian_Lokal_Barang As ToolStripMenuItem
    Friend WithEvents mnu_POPembelian_Lokal_Jasa As ToolStripMenuItem
    Friend WithEvents mnu_POPembelian_Lokal_BarangDanJasa As ToolStripMenuItem
    Friend WithEvents mnu_POPembelian_Lokal_JasaKonstruksi As ToolStripMenuItem
    Friend WithEvents mnu_SuratJalanPembelian As ToolStripMenuItem
    Friend WithEvents mnu_BASTPembelian As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_Lokal_Barang As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_Lokal_Jasa As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_Lokal_BarangDanJasa As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_Lokal_JasaKonstruksi As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_DenganPO_Lokal_Rutin As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_TanpaPO_Asset As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_DenganPO As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_BAK As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPemegangSaham As ToolStripMenuItem
    Friend WithEvents mnu_DataProject As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_Hutang As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_Piutang As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_BAK As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangBank As ToolStripMenuItem
    Friend WithEvents mnu_ManajemenAsset As ToolStripMenuItem
    Friend WithEvents mnu_ManajemenAssetTetap As ToolStripMenuItem
    Friend WithEvents mnu_ManajemenAmortisasi As ToolStripMenuItem
    Friend WithEvents mnu_ManajemenAmortisasiBiaya As ToolStripMenuItem
    Friend WithEvents mnu_ManajemenAmortisasiAssetTidakBerwujud As ToolStripMenuItem
    Friend WithEvents mnu_DaftarPenyusutanAssetTetap As ToolStripMenuItem
    Friend WithEvents mnu_BukuPenjualanAssetTetap As ToolStripMenuItem
    Friend WithEvents mnu_BukuDisposalAssetTetap As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_Asset As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_AmortisasiBiaya As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_AssetTetap As ToolStripMenuItem
    Friend WithEvents mnu_DataKaryawan As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangKaryawan As ToolStripMenuItem
    Friend WithEvents mnu_PengajuanPembayaranHutangLeasing As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangLeasing As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangLeasing As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPihakKetiga As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPihakKetiga As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangKaryawan As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangKaryawan As ToolStripMenuItem
    Friend WithEvents mnu_DataPemegangSaham As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPemegangSaham As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangPihakKetiga As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanBuktiPotongPPh_Prepaid As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanBuktiPotongPPh_Paid As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_DenganPO_Lokal_Termin As ToolStripMenuItem
    Friend WithEvents mnu_StockOpname As ToolStripMenuItem
    Friend WithEvents mnu_StockOpname_BahanPenolong As ToolStripMenuItem
    Friend WithEvents mnu_StockOpname_BahanBaku As ToolStripMenuItem
    Friend WithEvents mnu_StockOpname_BarangDalamProses As ToolStripMenuItem
    Friend WithEvents mnu_StockOpname_BarangJadi As ToolStripMenuItem
    Friend WithEvents mnu_StockOpname_BarangDalamProses_CekFisik As ToolStripMenuItem
    Friend WithEvents mnu_StockOpname_BarangDalamProses_TarikanData As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP_PemakaianBahanPenolong As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP_PemakaianBahanBaku As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP_BiayaBahanBaku As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP_BiayaTenagaKerjaLangsung As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP_BiayaOverheadPabrik As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP_BiayaProduksi As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP_HargaPokokProduksi As ToolStripMenuItem
    Friend WithEvents mnu_Adjusment_HPP_HargaPokokPenjualan As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanBuktiPengeluaranBankCash As ToolStripMenuItem
    Friend WithEvents mnu_BukuBankGaransi As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanDepositOperasional As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanAktivaLainnya As ToolStripMenuItem
    Friend WithEvents WPFPertamaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UserControlPertamaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnu_TemplateFormInput2Kolom As ToolStripMenuItem
    Friend WithEvents BelajarTabelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanBuktiPenerimaanBankCash As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangDividen As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangDividen As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_DenganPO_Lokal_Rutin As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_DenganPO_Lokal_Termin As ToolStripMenuItem
    Friend WithEvents mnu_DaftarPemegangSaham As ToolStripMenuItem
    Friend WithEvents mnu_BukuPenjualanEceran As ToolStripMenuItem
    Friend WithEvents mnu_DataLawanTransaksi As ToolStripMenuItem
    Friend WithEvents mnu_LaporanAktivitasTransaksi As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Afiliasi As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_NonAfiliasi As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Afiliasi As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_NonAfiliasi As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Afiliasi As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_NonAfiliasi As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Afiliasi As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_NonAfiliasi As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Semua As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Semua As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangPihakKetiga As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangPemegangSaham As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_Gaji As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangGaji As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangBPJSKesehatan As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangBPJSKetenagakerjaan As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangKoperasiKaryawan As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangSerikat As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangAfiliasi As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangAfiliasi As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangKetetapanPajak As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangPPN As ToolStripMenuItem
    Friend WithEvents mnu_ManajemenClient As ToolStripMenuItem
    Friend WithEvents mnu_ManajemenAplikasi As ToolStripMenuItem
    Friend WithEvents mnu_TechnicalSupport As ToolStripMenuItem
    Friend WithEvents mnu_PhpMyAdmin As ToolStripMenuItem
    Friend WithEvents mnu_Database As ToolStripMenuItem
    Friend WithEvents mnu_Database_Cadangkan As ToolStripMenuItem
    Friend WithEvents mnu_Database_Pulihkan As ToolStripMenuItem
    Friend WithEvents mnu_TabPokok As ToolStripMenuItem
    Friend WithEvents mnu_POPembelian_Semua As ToolStripMenuItem
    Friend WithEvents mnu_POPenjualan_Lokal_Semua As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_DepositOperasional As ToolStripMenuItem
    Friend WithEvents mnu_POPenjualan_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_POPenjualan_Ekspor As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_DenganPO_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_DenganPO_Ekspor As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_DenganPO_Ekspor_Rutin As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_DenganPO_Ekspor_Termin As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Impor As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Ekspor As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Impor As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Ekspor As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_DenganPO As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_TanpaPO As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_TanpaPO_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_TanpaPO_Lokal_Barang As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_TanpaPO_Lokal_Jasa As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_TanpaPO_Lokal_BarangDanJasa As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_TanpaPO_Lokal_JasaKonstruksi As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePenjualan_TanpaPO_Ekspor As ToolStripMenuItem
    Friend WithEvents mnu_PO_Pembelian_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_PO_Pembelian_Impor As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_DenganPO_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_DenganPO_Impor As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_DenganPO_Impor_Rutin As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_DenganPO_Impor_Termin As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_Impor As ToolStripMenuItem
    Friend WithEvents mnu_BukuPembelian_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_BukuPembelian_Impor As ToolStripMenuItem
    Friend WithEvents mnu_BukuPenjualan_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_BukuPenjualan_Ekspor As ToolStripMenuItem
    Friend WithEvents mnu_PO_Pembelian_Impor_Barang As ToolStripMenuItem
    Friend WithEvents mnu_PO_Pembelian_Impor_Jasa As ToolStripMenuItem
    Friend WithEvents mnu_PO_Pembelian_Impor_Semua As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_Impor_Barang As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_Impor_Jasa As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal22 As ToolStripMenuItem
    Friend WithEvents mnu_JurnalUmum As ToolStripMenuItem
    Friend WithEvents mnu_JurnalAdjusment As ToolStripMenuItem
    Friend WithEvents KursToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Impor_USD As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Impor_AUD As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Impor_JPY As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Impor_CNY As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Impor_EUR As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Ekspor_USD As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Ekspor_AUD As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Ekspor_JPY As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Ekspor_CNY As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Ekspor_EUR As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Impor_USD As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Impor_AUD As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Impor_JPY As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Impor_CNY As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Impor_EUR As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Ekspor_USD As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Ekspor_AUD As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Ekspor_JPY As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Ekspor_CNY As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Ekspor_EUR As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Impor_SGD As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Ekspor_SGD As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Impor_SGD As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Ekspor_SGD As ToolStripMenuItem
    Friend WithEvents mnu_PajakImpor As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal22_Lokal As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangPPhPasal22_Impor As ToolStripMenuItem
    Friend WithEvents mnu_InvoicePembelian_TanpaPO_LokalMUA As ToolStripMenuItem
    Friend WithEvents mnu_Database_Kloning As ToolStripMenuItem
    Friend WithEvents mnu_JurnalAdjusment_Forex As ToolStripMenuItem
    Friend WithEvents mnu_JurnalAdjusment_HPP As ToolStripMenuItem
    Friend WithEvents mnu_NeracaLajur As ToolStripMenuItem
    Friend WithEvents mnu_LaporanHPP As ToolStripMenuItem
    Friend WithEvents mnu_LabaRugi_Bulanan As ToolStripMenuItem
    Friend WithEvents mnu_LabaRugi_Tahunan As ToolStripMenuItem
    Friend WithEvents mnu_Neraca_Bulanan As ToolStripMenuItem
    Friend WithEvents mnu_Neraca_Tahunan As ToolStripMenuItem
    Friend WithEvents mnu_JurnalAdjusment_Penyusutan As ToolStripMenuItem
    Friend WithEvents mnu_JurnalAdjusment_Amortisasi As ToolStripMenuItem
    Friend WithEvents mnu_LaporanKeuangan As ToolStripMenuItem
    Friend WithEvents mnu_ManajemenKurs As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanHutangUsaha_Impor_GBP As ToolStripMenuItem
    Friend WithEvents mnu_BukuPengawasanPiutangUsaha_Ekspor_GBP As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_HutangUsaha_Impor_GBP As ToolStripMenuItem
    Friend WithEvents mnu_DataAwal_PiutangUsaha_Ekspor_GBP As ToolStripMenuItem
End Class
