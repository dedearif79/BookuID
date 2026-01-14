Imports System.IO
Imports bcomm

Module mdl_VariabelUmum

    Public NamaAplikasi
    Public JudulAplikasi
    Public VersiBooku_SisiPublic
    Public ApdetBooku_SisiPublic
    Public VersiBooku_SisiDatabase
    Public ApdetBooku_SisiDatabase
    Public VersiBooku_SisiAplikasi
    Public ApdetBooku_SisiAplikasi
    Public StatusUpdate_Aplikasi As Boolean = False
    Public ProsesUpdate_Aplikasi As Boolean = False
    Public urlPaketUpdaterPublic
    Public urlPaketInstallerPublic
    Public FolderTempInstaller
    Public FolderTempUpdater
    Public FilePathUpdaterLokal
    Public FilePathInstallerLokal
    Public FilePathPaketUpdaterLokal
    Public FilePathPaketInstallerLokal
    Public NomorHotLine
    Public WebsiteAplikasi = "booku.id"
    Public EmailAplikasi
    Public AdaInfoUdate As Boolean

    Public DirektoriDesktop As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
    Public NamaPerusahaan
    Public TaglinePerusahaan
    Public NamaDirekturPerusahaan
    Public NPWPPerusahaan
    Public JenisUsahaPerusahaan
    Public JenisWPPerusahaan
    Public JenisPerusahaan_IndustriManufactur = "Industri/Manufaktur"
    Public JenisPerusahaan_Dagang = "Dagang"
    Public JenisPerusahaan_Jasa = "Jasa"
    Public AlamatPerusahaan
    Public EmailPerusahaan
    Public KodeKPP_Perusahaan As String = Kosongan
    Public PICPerusahaan
    Public NomorSKTPerusahaan
    Public TanggalSKTPerusahaan
    Public EmailDJPO_Perusahaan
    Public PasswordDJPO_Perusahaan
    Public NomorSuketUMKM_Perusahaan As String
    Public TanggalSuketUMKM_Perusahaan As Date
    Public PerusahaanSebagaiUMKM As Boolean
    Public TanggalPKP_Perusahaan As Date
    Public PerusahaanSebagaiPKP As Boolean
    Public PerusahaanSebagaiPemotongPPh As Boolean
    Public Password_eFaktur_Perusahaan
    Public KodeAktivasiPerusahaan
    Public PassphrasePerusahaan
    Public LevelPJK_Perusahaan As Integer
    Public TanggalExpireSEPerusahaan As Date
    Public TanggalExpireSBUPerusahaan As Date
    Public JumlahPerangkat
    Public SistemApprovalPerusahaan As Boolean
    Public SistemCOA
    Public SistemCOA_StandarAplikasi = "Standar Aplikasi"
    Public SistemCOA_Customize = "Customize"

    Public AppInstall As Date = "01/01/1990"
    Public AppExpire As Date = "01/01/1900"
    Public ID_CPU As String
    Public ID_CPU_Developer As String = "BFEBFBFF000906A3" ' ID CPU PC Developer
    Public ModusAplikasi As String = "CLASSIC" ' CLASSIC = WinForms MDI, MODERN = WPF Shell
    Public StartupSudahDijalankan As Boolean = False ' Flag untuk skip startup di wpfWin_BOOKU jika sudah dijalankan dari frm_BOOKU
    Public ID_Customer As String
    Public ID_Customer_Kosongan As String = "00000000"
    Public NomorSeriProduk As String
    Public NomorSeriProduk_Kosongan As String = "000000-000000-000000-000000"
    Public StatusRegistrasiPerangkat As Boolean = False
    Public ProsesRegistrasiPerusahaan As Boolean
    Public ProsesRegistrasiPerangkat As Boolean

    Public FolderRootApp As String = AppDomain.CurrentDomain.BaseDirectory
    Public FolderAttachApp As String = Path.Combine(FolderRootApp, "Attach")
    Public FolderImpageApp As String = Path.Combine(FolderAttachApp, "Img")
    Public FolderNotesApp As String = Path.Combine(FolderAttachApp, "Notes")
    Public FolderListClient As String
    Public FolderCompany As String
    Public FolderCompany_Image As String
    Public FolderCompany_Backup As String
    Public FolderCompany_Backup_MySQL As String
    Public NamaFileLogoPerusahaan As String = "LogoPerusahaan.jpg"
    Public FilePathLogoPerusahaan As String

    Public Proses As Boolean = False

    Public VariabelTes1 = Nothing
    Public VariabelTes2 = Nothing
    Public VariabelTes3 = Nothing

    Public StatusImportJurnal As Boolean = False '(Default : False. Jangan dirubah..!!!)
    Public StatusExportJurnal As Boolean = False '(Default : False. Jangan dirubah..!!!)

    Public PengulanganLogin As Int64 = 0

    Public VisibilitasNotifikasi As Boolean = False
    Public JenisNotifikasi_Semua = "Semua"
    Public JenisNotifikasi_Pemberitahuan = "Pemberitahuan"
    Public JenisNotifikasi_PerintahEksekusi = "Perintah Eksekusi"

    Public StandarLebarLayar = 1350 'pixel
    Public StandarTinggiLayar = 720 'pixel

    Public KarakterDilarangMasuk = "'`;"""

    'Variabel USER :
    Public UserAktif As String
    Public NamaUserAktif As String
    Public LevelUserAktif As String
    Public JabatanUserAktif As String

    Public UsernameAppDeveloper = "dedearif79"
    Public PasswordAppDeveloper_Enk = "/]=s%$}dB>JX+YVvn;UD1jOqTU,A!nQGK4u]zd(7ilpuOWge{}zC.3k4vL%H]LA#=3-*@FC=Q+_|iz*V.p=KNDnHIpO)RENg*+7}|RYHPkuQ,UJVd(vCR<*g(s?n/]=s%$}dBaRGgIQ@FgNk&eS?$+$HSaMMXfQ_ULQiQhc@$Yn.+s=3}MWU(m)azYHjXOWv*hh-g$WSzCbaOF!VXQ}1mr4!O;yWG3."
    Public PasswordAppDeveloper = DekripsiTeks(PasswordAppDeveloper_Enk)
    Public NamaUserAppDeveloper = "Dede Arif Rahman"
    Public LevelUser_99_AppDeveloper = 99
    Public JabatanUser_AppDeveloper = "App Developer"

    Public UsernameTimIT = "timite"
    Public PasswordTimIT = "Amanah&Profesional"
    Public NamaUserTimIT = "Personal Tim IT"
    Public LevelUser_81_TimIT = 81
    Public JabatanUser_TimIT = "Tim IT"

    Public JabatanUser_SuperUser = "Super User"
    Public JabatanUser_GeneralUser = "General User"
    Public JabatanUser_Direktur = "Direktur"
    Public JabatanUser_Manager = "Manager"
    Public JabatanUser_Operator = "Operator"

    Public LevelUser_09_SuperUser = 9
    Public levelUser_04_GeneralUser = 4
    Public LevelUser_03_Direktur = 3
    Public LevelUser_02_Manager = 2
    Public LevelUser_01_Operator = 1

    Public KeteranganCluster = Nothing
    Public ClusterFinance = 0
    Public ClusterAccounting = 0

    Public StatusAktifUser = 0

    Public TanggalIni_Date As Date = Today

    Public TanggalIni = AmbilKiri(Today, 10)

    Public TanggalHariIni = Format(Today, "dd")

    Public BulanIni = Format(Today, "MM")

    Public TahunIni = Format(Today, "yyyy")

    Public TahunKabisat_TahunBukuAktif As Boolean

    Public TahunKabisat_TahunIni As Boolean

    Public TanggalAkhirBulan As String

    Public TanggalTakTerbatas As Date = "01/01/9999"

    Public TanggalTakTerbatasSimpan = TanggalFormatSimpan(TanggalTakTerbatas)

    Public TanggalKosong As Date = "01/01/1900"

    Public TanggalKosongSimpan = TanggalFormatSimpan(TanggalKosong)
    Public TanggalKosongWPF = TanggalFormatSimpan(TanggalKosong)

    Public TanggalAkhirTahunKemarin As Date
    Public TanggalAwalTahunBukuAktif As Date
    Public TanggalAkhirTahunBukuAktif As Date

    Public StripKosong = " - " 'Sengaja disisipkan spasi pada sebelum dan sesudah strip, untuk suatu kepentingan. Jadi, jangan dirubah..!!!
    Public StripPemisah = " - " 'Sengaja disisipkan spasi pada sebelum dan sesudah strip, untuk suatu kepentingan. Jadi, jangan dirubah..!!!

    Public PenjorokNamaAkun = "     "

    Public AmbangBatasJumlahPengajuanPembayaran As Int64 = 10000000 'Nilai ini nanti mengambil dari database tbl_DataAwal

    Public LewatiKode As Boolean = True
    Public EksekusiKode As Boolean = True

    Public JenisTahunBuku
    Public JenisTahunBuku_Baru
    Public JenisTahunBuku_LAMPAU = "LAMPAU"
    Public JenisTahunBuku_NORMAL = "NORMAL"

    Public StatusBuku
    Public StatusBuku_OPEN = "OPEN"
    Public StatusBuku_CLOSED = "CLOSED"
    Public TahunCutOff As Integer

    Public Buka As String = "Buka"
    Public Tutup As String = "Tutup"

    Public StatusLogin As Boolean = False

    Public Lanjutkan As Boolean

    Public Pilihan As DialogResult

    Public StatusPosting
    Public HasilPosting
    Public NomorID_AwalPosting
    Public ProgressValue As Int64
    Public ProgressMinimum As Int64
    Public ProgressMaximum As Int64
    Public ProgressInfo As String
    Public ProgressPersen As String

    Public TinggiMaximalScrollViewerFormDialogVertikal = 555


    'Variabel Range Akun :
    Public AwalAkunAssetTetap = 12000
    Public AkhirAkunAssetTetap = 12999
    Public AwalAkunBiaya = 40000

    Public KelompokCOA_Bank


    Public AwalanNomorJV
    Public PanjangTeks_AwalanNomorJV
    Public PanjangTeks_AwalanNomorJV_Plus1

    Public AwalanPO
    Public PanjangTeks_AwalanPO
    Public PanjangTeks_AwalanPO_Plus1

    Public AwalanSJ
    Public PanjangTeks_AwalanSJ
    Public PanjangTeks_AwalanSJ_Plus1

    Public AwalanBAST
    Public PanjangTeks_AwalanBAST
    Public PanjangTeks_AwalanBAST_Plus1

    Public AwalanPEMB
    Public PanjangTeks_AwalanPEMB
    Public PanjangTeks_AwalanPEMB_Plus1

    Public AwalanPENJ
    Public PanjangTeks_AwalanPENJ
    Public PanjangTeks_AwalanPENJ_Plus1

    Public AwalanNR
    Public PanjangTeks_AwalanNR
    Public PanjangTeks_AwalanNR_Plus1

    Public AwalanINV
    Public PanjangTeks_AwalanINV
    Public PanjangTeks_AwalanINV_Plus1

    'Kas Masuk :
    Public AwalanKM
    Public PanjangTeks_AwalanKM
    Public PanjangTeks_AwalanKM_Plus1

    Public AwalanKM_PlusTahunBuku
    Public PanjangTeks_AwalanKM_PlusTahunBuku
    Public PanjangTeks_AwalanKM_PlusTahunBuku_Plus1

    'Kas Keluar :
    Public AwalanKK
    Public PanjangTeks_AwalanKK
    Public PanjangTeks_AwalanKK_Plus1

    Public AwalanKK_PlusTahunBuku
    Public PanjangTeks_AwalanKK_PlusTahunBuku
    Public PanjangTeks_AwalanKK_PlusTahunBuku_Plus1

    'Bundel Pengajuan Pengeluaran Bank-Cash :
    Public AwalanBundelKK
    Public PanjangTeks_AwalanBundelKK
    Public PanjangTeks_AwalanBundelKK_Plus1

    Public AwalanBundelKK_PlusTahunBuku
    Public PanjangTeks_AwalanBundelKK_PlusTahunBuku
    Public PanjangTeks_AwalanBundelKK_PlusTahunBuku_Plus1

    'Buku Pengawasan Bank Garansi :
    Public AwalanBPBG
    Public PanjangTeks_AwalanBPBG
    Public PanjangTeks_AwalanBPBG_Plus1

    Public AwalanBPBG_PlusTahunBuku
    Public PanjangTeks_AwalanBPBG_PlusTahunBuku
    Public PanjangTeks_AwalanBPBG_PlusTahunBuku_Plus1

    'Buku Pengawasan Deposit Operasional :
    Public AwalanBPDO
    Public PanjangTeks_AwalanBPDO
    Public PanjangTeks_AwalanBPDO_Plus1

    Public AwalanBPDO_PlusTahunBuku
    Public PanjangTeks_AwalanBPDO_PlusTahunBuku
    Public PanjangTeks_AwalanBPDO_PlusTahunBuku_Plus1

    'Buku Pengawasan Deposit Operasional :
    Public AwalanBPAL
    Public PanjangTeks_AwalanBPAL
    Public PanjangTeks_AwalanBPAL_Plus1

    Public AwalanBPAL_PlusTahunBuku
    Public PanjangTeks_AwalanBPAL_PlusTahunBuku
    Public PanjangTeks_AwalanBPAL_PlusTahunBuku_Plus1

    'Buku Pembelian :
    Public AwalanPEMB_PlusTahunBuku
    Public PanjangTeks_AwalanPEMB_PlusTahunBuku
    Public PanjangTeks_AwalanPEMB_PlusTahunBuku_Plus1

    Public AwalanPENJ_PlusTahunBuku
    Public PanjangTeks_AwalanPENJ_PlusTahunBuku
    Public PanjangTeks_AwalanPENJ_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Usaha :
    Public AwalanBPHU
    Public PanjangTeks_AwalanBPHU
    Public PanjangTeks_AwalanBPHU_Plus1

    Public AwalanBPHU_PlusTahunBuku
    Public PanjangTeks_AwalanBPHU_PlusTahunBuku
    Public PanjangTeks_AwalanBPHU_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Dividen :
    Public AwalanBPHD
    Public PanjangTeks_AwalanBPHD
    Public PanjangTeks_AwalanBPHD_Plus1

    Public AwalanBPHD_PlusTahunBuku
    Public PanjangTeks_AwalanBPHD_PlusTahunBuku
    Public PanjangTeks_AwalanBPHD_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Bank :
    Public AwalanBPHB
    Public PanjangTeks_AwalanBPHB
    Public PanjangTeks_AwalanBPHB_Plus1

    Public AwalanBPHB_PlusTahunBuku
    Public PanjangTeks_AwalanBPHB_PlusTahunBuku
    Public PanjangTeks_AwalanBPHB_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Leasing :
    Public AwalanBPHL
    Public PanjangTeks_AwalanBPHL
    Public PanjangTeks_AwalanBPHL_Plus1

    Public AwalanBPHL_PlusTahunBuku
    Public PanjangTeks_AwalanBPHL_PlusTahunBuku
    Public PanjangTeks_AwalanBPHL_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang BPJS Kesehatan :
    Public AwalanBPHKS
    Public PanjangTeks_AwalanBPHKS
    Public PanjangTeks_AwalanBPHKS_Plus1

    Public AwalanBPHKS_PlusTahunBuku
    Public PanjangTeks_AwalanBPHKS_PlusTahunBuku
    Public PanjangTeks_AwalanBPHKS_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang BPJS Ketenagakerjaan :
    Public AwalanBPHTK
    Public PanjangTeks_AwalanBPHTK
    Public PanjangTeks_AwalanBPHTK_Plus1

    Public AwalanBPHTK_PlusTahunBuku
    Public PanjangTeks_AwalanBPHTK_PlusTahunBuku
    Public PanjangTeks_AwalanBPHTK_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Koperasi Karyawan :
    Public AwalanBPHKK
    Public PanjangTeks_AwalanBPHKK
    Public PanjangTeks_AwalanBPHKK_Plus1

    Public AwalanBPHKK_PlusTahunBuku
    Public PanjangTeks_AwalanBPHKK_PlusTahunBuku
    Public PanjangTeks_AwalanBPHKK_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Serikat :
    Public AwalanBPHS
    Public PanjangTeks_AwalanBPHS
    Public PanjangTeks_AwalanBPHS_Plus1

    Public AwalanBPHS_PlusTahunBuku
    Public PanjangTeks_AwalanBPHS_PlusTahunBuku
    Public PanjangTeks_AwalanBPHS_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Pihak Ketiga :
    Public AwalanBPHPK
    Public PanjangTeks_AwalanBPHPK
    Public PanjangTeks_AwalanBPHPK_Plus1

    Public AwalanBPHPK_PlusTahunBuku
    Public PanjangTeks_AwalanBPHPK_PlusTahunBuku
    Public PanjangTeks_AwalanBPHPK_PlusTahunBuku_Plus1

    'Buku Pengawasan Piutang Pihak Ketiga :
    Public AwalanBPPPK
    Public PanjangTeks_AwalanBPPPK
    Public PanjangTeks_AwalanBPPPK_Plus1

    Public AwalanBPPPK_PlusTahunBuku
    Public PanjangTeks_AwalanBPPPK_PlusTahunBuku
    Public PanjangTeks_AwalanBPPPK_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Afiliasi :
    Public AwalanBPHA
    Public PanjangTeks_AwalanBPHA
    Public PanjangTeks_AwalanBPHA_Plus1

    Public AwalanBPHA_PlusTahunBuku
    Public PanjangTeks_AwalanBPHA_PlusTahunBuku
    Public PanjangTeks_AwalanBPHA_PlusTahunBuku_Plus1

    'Buku Pengawasan Piutang Afiliasi :
    Public AwalanBPPA
    Public PanjangTeks_AwalanBPPA
    Public PanjangTeks_AwalanBPPA_Plus1

    Public AwalanBPPA_PlusTahunBuku
    Public PanjangTeks_AwalanBPPA_PlusTahunBuku
    Public PanjangTeks_AwalanBPPA_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Karyawan :
    Public AwalanBPHK
    Public PanjangTeks_AwalanBPHK
    Public PanjangTeks_AwalanBPHK_Plus1

    Public AwalanBPHK_PlusTahunBuku
    Public PanjangTeks_AwalanBPHK_PlusTahunBuku
    Public PanjangTeks_AwalanBPHK_PlusTahunBuku_Plus1

    'Buku Pengawasan Piutang Karyawan :
    Public AwalanBPPK
    Public PanjangTeks_AwalanBPPK
    Public PanjangTeks_AwalanBPPK_Plus1

    Public AwalanBPPK_PlusTahunBuku
    Public PanjangTeks_AwalanBPPK_PlusTahunBuku
    Public PanjangTeks_AwalanBPPK_PlusTahunBuku_Plus1

    'Buku Pengawasan Modal :
    Public AwalanBPM
    Public PanjangTeks_AwalanBPM
    Public PanjangTeks_AwalanBPM_Plus1

    Public AwalanBPM_PlusTahunBuku
    Public PanjangTeks_AwalanBPM_PlusTahunBuku
    Public PanjangTeks_AwalanBPM_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Pemegang Saham :
    Public AwalanBPHPS
    Public PanjangTeks_AwalanBPHPS
    Public PanjangTeks_AwalanBPHPS_Plus1

    Public AwalanBPHPS_PlusTahunBuku
    Public PanjangTeks_AwalanBPHPS_PlusTahunBuku
    Public PanjangTeks_AwalanBPHPS_PlusTahunBuku_Plus1

    'Buku Pengawasan Hutang Pemegang Saham :
    Public AwalanBPPPS
    Public PanjangTeks_AwalanBPPPS
    Public PanjangTeks_AwalanBPPPS_Plus1

    Public AwalanBPPPS_PlusTahunBuku
    Public PanjangTeks_AwalanBPPPS_PlusTahunBuku
    Public PanjangTeks_AwalanBPPPS_PlusTahunBuku_Plus1

    Public AwalanNPPHU_PlusTahunBuku
    Public PanjangTeks_AwalanNPPHU_PlusTahunBuku
    Public PanjangTeks_AwalanNPPHU_PlusTahunBuku_Plus1

    Public AwalanNRBHU_PlusTahunBuku
    Public PanjangTeks_AwalanNRBHU_PlusTahunBuku
    Public PanjangTeks_AwalanNRBHU_PlusTahunBuku_Plus1

    Public AwalanNPPHP_PlusTahunBuku
    Public PanjangTeks_AwalanNPPHP_PlusTahunBuku
    Public PanjangTeks_AwalanNPPHP_PlusTahunBuku_Plus1

    Public AwalanNRBHP_PlusTahunBuku
    Public PanjangTeks_AwalanNRBHP_PlusTahunBuku
    Public PanjangTeks_AwalanNRBHP_PlusTahunBuku_Plus1

    Public AwalanNPPG_PlusTahunBuku
    Public PanjangTeks_AwalanNPPG_PlusTahunBuku
    Public PanjangTeks_AwalanNPPG_PlusTahunBuku_Plus1

    Public AwalanNRBG_PlusTahunBuku
    Public PanjangTeks_AwalanNRBG_PlusTahunBuku
    Public PanjangTeks_AwalanNRBG_PlusTahunBuku_Plus1

    Public AwalanNPPHKS_PlusTahunBuku
    Public PanjangTeks_AwalanNPPHKS_PlusTahunBuku
    Public PanjangTeks_AwalanNPPHKS_PlusTahunBuku_Plus1

    Public AwalanNRBHKS_PlusTahunBuku
    Public PanjangTeks_AwalanNRBHKS_PlusTahunBuku
    Public PanjangTeks_AwalanNRBHKS_PlusTahunBuku_Plus1

    Public AwalanNPPHTK_PlusTahunBuku
    Public PanjangTeks_AwalanNPPHTK_PlusTahunBuku
    Public PanjangTeks_AwalanNPPHTK_PlusTahunBuku_Plus1

    Public AwalanNRBHTK_PlusTahunBuku
    Public PanjangTeks_AwalanNRBHTK_PlusTahunBuku
    Public PanjangTeks_AwalanNRBHTK_PlusTahunBuku_Plus1

    Public AwalanBPHG
    Public AwalanBPHG_PlusTahunBuku
    Public PanjangTeks_AwalanBPHG_PlusTahunBuku
    Public PanjangTeks_AwalanBPHG_PlusTahunBuku_Plus1

    Public AwalanBPHPPN
    Public AwalanBPHPPN_PlusTahunBuku
    Public PanjangTeks_AwalanBPHPPN_PlusTahunBuku
    Public PanjangTeks_AwalanBPHPPN_PlusTahunBuku_Plus1

    Public AwalanBPHP21
    Public AwalanBPHP21_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP21_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP21_PlusTahunBuku_Plus1

    Public AwalanBPHP23
    Public AwalanBPHP23_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP23_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP23_PlusTahunBuku_Plus1

    Public AwalanBPHP25
    Public AwalanBPHP25_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP25_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP25_PlusTahunBuku_Plus1

    Public AwalanBPHP26
    Public AwalanBPHP26_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP26_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP26_PlusTahunBuku_Plus1

    Public AwalanBPHP29_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP29_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP29_PlusTahunBuku_Plus1

    Public AwalanBPHP42
    Public AwalanBPHP42_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP42_PlusTahunBuku
    Public PanjangTeks_AwalanBPHP42_PlusTahunBuku_Plus1

    Public AwalanBPKP
    Public AwalanBPKP_PlusTahunBuku
    Public PanjangTeks_AwalanBPKP_PlusTahunBuku
    Public PanjangTeks_AwalanBPKP_PlusTahunBuku_Plus1

    'Buku Pengawasan Piutang Usaha :
    Public AwalanBPPU
    Public PanjangTeks_AwalanBPPU
    Public PanjangTeks_AwalanBPPU_Plus1

    Public AwalanBPPU_PlusTahunBuku
    Public PanjangTeks_AwalanBPPU_PlusTahunBuku
    Public PanjangTeks_AwalanBPPU_PlusTahunBuku_Plus1

    'Buku Pengawasan Piutang Dividen :
    Public AwalanBPPD
    Public PanjangTeks_AwalanBPPD
    Public PanjangTeks_AwalanBPPD_Plus1

    Public AwalanBPPD_PlusTahunBuku
    Public PanjangTeks_AwalanBPPD_PlusTahunBuku
    Public PanjangTeks_AwalanBPPD_PlusTahunBuku_Plus1

    Public AwalanNPC_PlusTahunBuku
    Public PanjangTeks_AwalanNPC_PlusTahunBuku
    Public PanjangTeks_AwalanNPC_PlusTahunBuku_Plus1

    Public AwalanBPPB_PlusTahunBuku
    Public PanjangTeks_AwalanBPPB_PlusTahunBuku
    Public PanjangTeks_AwalanBPPB_PlusTahunBuku_Plus1

    Public Halaman_MENUUTAMA As String = "MENU UTAMA"
    Public Halaman_BUKUBESAR As String = "BUKU BESAR"
    Public Halaman_BUKUBANK As String = "BUKU BANK"
    Public Halaman_BUKUKAS As String = "BUKU KAS"
    Public Halaman_BUKUPETTYCASH As String = "BUKU PETTY CASH"
    Public Halaman_BUKUCASHADVANCE As String = "BUKU CASH ADVANCE"
    Public Halaman_BUKUPENGAWASANGAJI As String = "BUKU PENGAWASAN GAJI"   '\ Awas Ketuker...!!!
    Public Halaman_BUKUHUTANGGAJI As String = "BUKU HUTANG GAJI"           '/ Awas Ketuker...!!!
    Public Halaman_BUKUPEMBELIAN As String = "BUKU PEMBELIAN"
    Public Halaman_JURNALUMUM As String = "JURNAL UMUM"
    Public Halaman_JURNALADJUSMENT As String = "JURNAL ADJUSMENT"
    Public Halaman_BAHANJURNAL As String = "BAHAN JURNAL"
    Public Halaman_INPUTPEMBELIAN As String = "INPUT PEMBELIAN"
    Public Halaman_BUKUPENGAWASANPEMINDABUKUAN As String = "BUKU PENGAWASAN PEMINDAHBUKUAN"
    Public Halaman_BUKUPENGAWASANHUTANGUSAHANONAFILIASI As String = "BUKU PENGAWASAN HUTANG USAHA - NON AFILIASI"
    Public Halaman_BUKUPENGAWASANHUTANGUSAHAAFILIASI As String = "BUKU PENGAWASAN HUTANG USAHA - AFILIASI"
    Public Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_USD As String = "BUKU PENGAWASAN HUTANG USAHA - IMPOR - USD"
    Public Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_AUD As String = "BUKU PENGAWASAN HUTANG USAHA - IMPOR - AUD"
    Public Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_JPY As String = "BUKU PENGAWASAN HUTANG USAHA - IMPOR - JPY"
    Public Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_CNY As String = "BUKU PENGAWASAN HUTANG USAHA - IMPOR - CNY"
    Public Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_EUR As String = "BUKU PENGAWASAN HUTANG USAHA - IMPOR - EUR"
    Public Halaman_BUKUPENGAWASANHUTANGUSAHAIMPOR_SGD As String = "BUKU PENGAWASAN HUTANG USAHA - IMPOR - SGD"
    Public Halaman_BUKUPENGAWASANPIUTANGUSAHANONAFILIASI As String = "BUKU PENGAWASAN PIUTANG USAHA - NON AFILIASI"
    Public Halaman_BUKUPENGAWASANPIUTANGUSAHAAFILIASI As String = "BUKU PENGAWASAN PIUTANG USAHA - AFILIASI"
    Public Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_USD As String = "BUKU PENGAWASAN PIUTANG USAHA - EKSPOR - USD"
    Public Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_AUD As String = "BUKU PENGAWASAN PIUTANG USAHA - EKSPOR - AUD"
    Public Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_JPY As String = "BUKU PENGAWASAN PIUTANG USAHA - EKSPOR - JPY"
    Public Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_CNY As String = "BUKU PENGAWASAN PIUTANG USAHA - EKSPOR - CNY"
    Public Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_EUR As String = "BUKU PENGAWASAN PIUTANG USAHA - EKSPOR - EUR"
    Public Halaman_BUKUPENGAWASANPIUTANGUSAHAEKSPOR_SGD As String = "BUKU PENGAWASAN PIUTANG USAHA - EKSPOR - SGD"
    Public Halaman_BUKUPENGAWASANHUTANGKARYAWAN As String = "BUKU PENGAWASAN HUTANG KARYAWAN"
    Public Halaman_BUKUPENGAWASANPIUTANGKARYAWAN As String = "BUKU PENGAWASAN PIUTANG KARYAWAN"
    Public Halaman_BUKUPENGAWASANHUTANGPEMEGANGSAHAM As String = "BUKU PENGAWASAN HUTANG PEMEGANG SAHAM"
    Public Halaman_BUKUPENGAWASANPIUTANGPEMEGANGSAHAM As String = "BUKU PENGAWASAN PIUTANG PEMEGANG SAHAM"
    Public Halaman_BUKUPENGAWASANHUTANGBANK As String = "BUKU PENGAWASAN HUTANG BANK"
    Public Halaman_BUKUPENGAWASANHUTANGLEASING As String = "BUKU PENGAWASAN HUTANG LEASING"
    Public Halaman_DATAASSETTETAP As String = "DATA ASET TETAP"
    Public Halaman_AMORTISASIBIAYA As String = "DATA AMORTISASI BIAYA"
    Public Halaman_SALDOAWALHUTANGUSAHA As String = "SALDO AWAL HUTANG USAHA"
    Public Halaman_BUKUPENGAWASANHUTANGBPJSKESEHATAN As String = "BUKU PENGAWASAN HUTANG BPJS KESEHATAN"
    Public Halaman_BUKUPENGAWASANHUTANGBPJSKETENAGAKERJAAN As String = "BUKU PENGAWASAN HUTANG BPJS KETENAGAKERJAAN"
    Public Halaman_BUKUPENGAWASANHUTANGKOPERASIKARYAWAN As String = "BUKU PENGAWASAN HUTANG KOPERASI KARYAWAN"
    Public Halaman_BUKUPENGAWASANHUTANGSERIKAT As String = "BUKU PENGAWASAN HUTANG SERIKAT"
    Public Halaman_BUKUPENGAWASANHUTANGPIHAKKETIGA As String = "BUKU PENGAWASAN HUTANG PIHAK KETIGA"
    Public Halaman_BUKUPENGAWASANPIUTANGPIHAKKETIGA As String = "BUKU PENGAWASAN PIUTANG PIHAK KETIGA"
    Public Halaman_BUKUPENGAWASANHUTANGAFILIASI As String = "BUKU PENGAWASAN HUTANG AFILIASI"
    Public Halaman_BUKUPENGAWASANPIUTANGAFILIASI As String = "BUKU PENGAWASAN PIUTANG AFILIASI"
    Public Halaman_BUKUPENGAWASANDEPOSITOPERASIONAL As String = "BUKU PENGAWASAN DEPOSIT OPERASIONAL"
    Public Halaman_BUKUPENGAWASANAKTIVALAINNYA As String = "BUKU PENGAWASAN AKTIVA LAIN-LAIN"

    Public Halaman_BAHANPENOLONG As String = "BAHAN PENOLONG"
    Public Halaman_BAHANBAKU As String = "BAHAN BAKU"
    Public Halaman_BARANGDALAMPROSES_CEKFISIK As String = "BARANG DALAM PROSES - CEK FISIK"
    Public Halaman_BARANGDALAMPROSES_TARIKANDATA As String = "BARANG DALAM PROSES - TARIKAN DATA"
    Public Halaman_BARANGJADI As String = "BARANG JADI"


    Public Halaman_BUKUPENGAWASANTURUNANGAJI As String = "BUKU PENGAWASAN TURUNAN GAJI"

    Public Halaman_BUKUPENGAWASANHUTANGPPHPASAL21 As String = "BUKU PENGAWASAN HUTANG PPH PASAL 21"
    Public Halaman_BUKUPENGAWASANHUTANGPPHPASAL22_LOKAL As String = "BUKU PENGAWASAN HUTANG PPH PASAL 22 - LOKAL"
    Public Halaman_BUKUPENGAWASANHUTANGPPHPASAL22_IMPOR As String = "BUKU PENGAWASAN HUTANG PPH PASAL 22 - IMPOR"
    Public Halaman_BUKUPENGAWASANHUTANGPPHPASAL23 As String = "BUKU PENGAWASAN HUTANG PPH PASAL 23"
    Public Halaman_BUKUPENGAWASANHUTANGPPHPASAL25 As String = "BUKU PENGAWASAN HUTANG PPH PASAL 25"
    Public Halaman_BUKUPENGAWASANHUTANGPPHPASAL26 As String = "BUKU PENGAWASAN HUTANG PPH PASAL 26"
    Public Halaman_BUKUPENGAWASANHUTANGPPHPASAL29 As String = "BUKU PENGAWASAN HUTANG PPH PASAL 29"
    Public Halaman_BUKUPENGAWASANHUTANGPPHPASAL42 As String = "BUKU PENGAWASAN HUTANG PPH PASAL 4 (2)"
    Public Halaman_BUKUPENGAWASANPELAPORANPPN As String = "BUKU PENGAWASAN PELAPORAN PPN"
    Public Halaman_BUKUPENGAWASANKETETAPANPAJAK As String = "BUKU PENGAWASAN KETETAPAN PAJAK"
    Public Halaman_BUKUPENGAWASANPAJAKPAJAKIMPOR As String = "BUKU PENGAWASAN PAJAK-PAJAK IMPOR"

    Public Halaman_LEMBARPENGAJUANPEMBAYARANPEMBELIANTUNAI As String = "LEMBAR PENGAJUAN PEMBAYARAN PEMBELIAN TUNAI"
    Public Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGUSAHA As String = "LEMBAR PENGAJUAN PEMBAYARAN HUTANG USAHA"
    Public Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGPAJAK As String = "LEMBAR PENGAJUAN PEMBAYARAN HUTANG PAJAK"
    Public Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGBANK As String = "LEMBAR PENGAJUAN PEMBAYARAN HUTANG BANK"
    Public Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGLEASING As String = "LEMBAR PENGAJUAN PEMBAYARAN HUTANG LEASING"
    Public Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGAFILIASI As String = "LEMBAR PENGAJUAN PEMBAYARAN HUTANG AFILIASI"
    Public Halaman_LEMBARPENGAJUANPEMBAYARANHUTANGLAINNYA As String = "LEMBAR PENGAJUAN PEMBAYARAN HUTANG LAINNYA"
    Public Halaman_LEMBARPENGAJUANPEMBAYARANKASBON As String = "LEMBAR PENGAJUAN PEMBAYARAN KASBON"
    Public Halaman_LEMBARPENGAJUANPEMBAYARANINVESTASI As String = "LEMBAR PENGAJUAN PEMBAYARAN INVESTASI"
    Public Halaman_LEMBARPENGAJUANPEMINDAHBUKUAN As String = "LEMBAR PENGAJUAN PEMINDAHBUKUAN"
    Public Halaman_LEMBARPENGAJUANLAINNYA As String = "LEMBAR PENGAJUAN LAINNYA"
    Public Halaman_LEMBARPENGAJUANPO As String = "LEMBAR PENGAJUAN PO"
    Public Halaman_LEMBARPENGAJUANCEK As String = "LEMBAR PENGAJUAN CEK"
    Public Halaman_DPHU As String = "DPHU"
    Public Halaman_INPUTPEMBAYARANGAJI As String = "INPUT PEMBAYARAN GAJI"
    Public Halaman_INPUTPEMBAYARANBPJSKESEHATAN As String = "INPUT PEMBAYARAN BPJS KESEHATAN"
    Public Halaman_INPUTPEMBAYARANBPJSKETENAGAKERJAAN As String = "INPUT PEMBAYARAN BPJS KETENAGAKERJAAN"
    Public Halaman_INPUTPEMBAYARANHUTANGPIUTANGUSAHA As String = "INPUT PEMBAYARAN HUTANG/PIUTANG USAHA"
    Public Halaman_INPUTPEMBAYARANHUTANGPIUTANGKARYAWAN As String = "INPUT PEMBAYARAN HUTANG/PIUTANG KARYAWAN"
    Public Halaman_INPUTPEMBAYARANHUTANGPIUTANGPEMEGANGSAHAM As String = "INPUT PEMBAYARAN HUTANG/PIUTANG PEMEGANG SAHAM"
    Public Halaman_INPUTPEMBAYARANHUTANGBANKLEASING As String = "INPUT PEMBAYARAN HUTANG BANK/LEASING"
    Public Halaman_INPUTPEMBAYARANHUTANGPIUTANGPIHAKKETIGA As String = "INPUT PEMBAYARAN HUTANG PIUTANG PIHAK KETIGA"
    Public Halaman_INPUTPEMBAYARANHUTANGPIUTANGAFILIASI As String = "INPUT PEMBAYARAN HUTANG PIUTANG AFILIASI"
    Public Halaman_INPUTPEMBAYARANHUTANGPPHPASAL21 As String = "INPUT PEMBAYARAN HUTANG PPH PASAL 21"
    Public Halaman_INPUTPEMBAYARANHUTANGPPHPASAL23 As String = "INPUT PEMBAYARAN HUTANG PPH PASAL 23"
    Public Halaman_INPUTPEMBAYARANHUTANGPPHPASAL25 As String = "INPUT PEMBAYARAN HUTANG PPH PASAL 25"
    Public Halaman_INPUTPEMBAYARANHUTANGPPHPASAL26 As String = "INPUT PEMBAYARAN HUTANG PPH PASAL 29"
    Public Halaman_INPUTPEMBAYARANHUTANGPPHPASAL29 As String = "INPUT PEMBAYARAN HUTANG PPH PASAL 29"
    Public Halaman_INPUTPEMBAYARANHUTANGPPHPASAL42 As String = "INPUT PEMBAYARAN HUTANG PPH PASAL 42"
    Public Halaman_INPUTPEMBAYARANHUTANGPPN As String = "INPUT PEMBAYARAN HUTANG PPN"
    Public Halaman_INPUTTRANSAKSIKAS As String = "INPUT TRANSAKSI KAS"
    Public Halaman_INPUTTRANSAKSIPETTYCASH As String = "INPUT TRANSAKSI PETTY CASH"
    Public Halaman_INPUTTRANSAKSIBANK As String = "INPUT TRANSAKSI BANK"
    Public Halaman_DATACOA As String = "HALAMAN DATA COA"
    Public Halaman_TUTUPBUKU As String = "HALAMAN TUTUP BUKU"
    Public Halaman_LAPORANNERACA As String = "HALAMAN LAPORAN NERACA"
    Public Halaman_LAPORANLABARUGI As String = "HALAMAN LAPORAN LABA/RUGI"
    Public Halaman_LAPORANNERACALAJUR As String = "NERACA LAJUR"

    Public Form_INPUTPOPEMBELIAN As String = "FORM INPUT PO PEMBELIAN"
    Public Form_INPUTRETURPEMBELIAN As String = "FORM INPUT RETUR PEMBELIAN"
    Public Form_INPUTINVOICEPEMBELIAN As String = "FORM INPUT INVOICE PEMBELIAN"
    Public Form_INPUTPOPENJUALAN As String = "FORM INPUT PO PENJUALAN"
    Public Form_INPUTRETURPENJUALAN As String = "FORM INPUT RETUR PENJUALAN"
    Public Form_INPUTINVOICEPENJUALAN As String = "FORM INPUT INVOICE PENJUALAN"
    Public Form_INPUTSURATJALANPENJUALAN As String = "FORM INPUT SURAT JALAN PENJUALAN"
    Public Form_INPUTBASTPENJUALAN As String = "FORM INPUT BAST PENJUALAN"
    Public Form_INPUTSURATJALANPEMBELIAN As String = "FORM INPUT SURAT JALAN PEMBELIAN"
    Public Form_INPUTBASTPEMBELIAN As String = "FORM INPUT BAST PEMBELIAN"

    Public Menu_JurnalAdjusment As String = "MENU JURNAL ADJUSMENT"

    Public FungsiForm_TAMBAH As String = "TAMBAH"
    Public FungsiForm_EDIT As String = "EDIT"
    Public FungsiForm_LIHAT As String = "LIHAT"
    Public FungsiForm_PEMBETULAN As String = "PEMBETULAN"
    Public FungsiForm_PENGAJUAN As String = "FORM PENGAJUAN"
    Public FungsiForm_PROSES As String = "PROSES"
    Public FungsiForm_UPDATEPERSETUJUAN As String = "UPDATE PERSETUJUAN"
    Public FungsiForm_POSTING As String = "POSTING"
    Public FungsiForm_INPUTRANSAKSI As String = "INPUT TRANSAKSI"
    Public FungsiForm_JURNALAPPROVE As String = "JURNAL APPROVE"
    Public FungsiForm_INFOJURNAL As String = "INFO JURNAL"
    Public FungsiForm_MASUKTAHUNBUKU As String = "MASUK TAHUN BUKU"
    Public FungsiForm_GANTITAHUNBUKU As String = "GANTI TAHUN BUKU"
    Public FungsiForm_EksekusiSub_PROSESGANTITAHUNBUKU As String = "EKSEKUSI SUB PROSES GANTI TAHUN BUKU"
    Public FungsiForm_SEKEDARLEWAT As String = "SEKEDAR LEWAT"
    Public FungsiForm_CETAK As String = "CETAK"
    Public FungsiForm_PRATINJAU As String = "PRATINJAU"

    Public ProsesLoadingForm As Boolean = False
    Public ProsesResetForm As Boolean = False
    Public ProsesIsiValueForm As Boolean = False

    'Variabel Keterangan :
    Public Keterangan_YA_ As String = "YA"
    Public Keterangan_TIDAK_ As String = "TIDAK"
    Public Keterangan_Ya As String = "Ya"
    Public Keterangan_Tidak As String = "Tidak"
    Public Status_BERHASIL As String = "BERHASIL"
    Public Status_SUKSES As String = "SUKSES"
    Public Status_GAGAL As String = "GAGAL"
    Public Status_BATAL As String = "BATAL"
    Public Status_SELESAI As String = "SELESAI"
    Public Status_TERTUNDA As String = "TERTUNDA"
    Public Status_PROSES As String = "PROSES"
    Public Status_TAHAN As String = "TAHAN"
    Public Status_DATARUSAK As String = "DATA RUSAK"
    Public Status_BARISLEBIH As String = "BARIS LEBIH"

    Public Proses_BERHASIL As String = "BERHASIL"
    Public Proses_SUKSES As String = "SUKSES"
    Public Proses_GAGAL As String = "GAGAL"
    Public Proses_BATAL As String = "BATAL"
    Public Proses_SELESAI As String = "SELESAI"
    Public Proses_TERTUNDA As String = "TERTUNDA"
    Public Proses_PROSES As String = "PROSES"
    Public Proses_TAHAN As String = "TAHAN"
    Public Proses_DATARUSAK As String = "DATA RUSAK"
    Public Proses_BARISLEBIH As String = "BARIS LEBIH"

    Public Hasil_NORMAL As String = "NORMAL"
    Public Hasil_BERMASALAH As String = "BERMASALAH"

    Public ListAkun_Semua As String = "SEMUA AKUN"
    Public ListAkun_Pembelian As String = "DAFTAR AKUN PEMBELIAN"
    Public ListAkun_Bank As String = "DAFTAR AKUN BANK"
    Public ListAkun_BarangJasa As String = "DAFTAR AKUN BARANG/JASA"
    Public ListAkun_AssetTanah As String = "DAFTAR AKUN ASSET TANAH"
    Public ListAkun_AssetTetap As String = "DAFTAR AKUN ASSET TETAP"
    Public ListAkun_AssetTetap_SelainTanah As String = "DAFTAR AKUN ASSET TETAP SELAIN TANAH"
    Public ListAkun_DepositOperasional As String = "DEPOSIT OPERASIONAL"
    Public ListAkun_AktivaLainnya As String = "DAFTAR AKUN AKTIVA LAINNYA"
    Public ListAkun_BiayaPenyusutan As String = "DAFTAR AKUN BIAYA PENYUSUTAN"
    Public ListAkun_AkumulasiPenyusutan As String = "DAFTAR AKUN AKUMULASI PENYUSUTAN"
    Public ListAkun_PokokPajak As String = "DAFTAR AKUN POKOK PAJAK"
    Public ListAkun_Amortisasi As String = "DAFTAR AKUN AMORTISASI"
    Public ListAkun_BiayaAmortisasi As String = "DAFTAR AKUN BIAYA AMORTISASI"
    Public ListAkun_TautanCOA As String = "TAUTAN COA"

    Public AlurTransaksi_IN As String = "IN"
    Public AlurTransaksi_OUT As String = "OUT"

    Public teks_Simpan As String = "Simpan"
    Public teks_Setujui As String = "Setujui"
    Public teks_Posting As String = "Posting"
    Public teks_Lihat As String = "Lihat"
    Public teks_LihatJurnal As String = "Lihat Jurnal"
    Public teks_Refresh As String = "Refresh"
    Public teks_Update As String = "Update"
    Public teks_Perbarui As String = "Perbarui"
    Public teks_Ok As String = "Ok"
    Public teks_Reset As String = "Reset"
    Public teks_Lanjutkan As String = "Lanjutkan >>"
    Public teks_Kembali As String = "<< Kembali"
    Public teks_Pilih As String = "Pilih"
    Public teks_Batal As String = "Batal"
    Public teks_Tutup As String = "Tutup"
    Public teks_Tolak As String = "Tolak"
    Public teks_Tambahkan_ As String = "Tambahkan >>"
    Public teks_Tambahkan As String = "Tambahkan"
    Public teks_Singkirkan_ As String = "Singkirkan >>"
    Public teks_Singkirkan As String = "Singkirkan"
    Public teks_Global As String = "<< Global"
    Public teks_Rinci As String = "Rinci"
    Public teks_Detail As String = "Detail"
    Public teks_DetailPerbulan As String = "Detail Perbulan >>"
    Public teks_Rekap As String = "Rekap"
    Public teks_RekapTotal As String = "Rekap Total"
    Public teks_RekapGlobal As String = "Rekap Global >>"
    Public teks_RekapPenyusutan As String = "Rekap Penyusutan >>"
    Public teks_Saham As String = "Saham"
    Public teks_Eceran As String = "Eceran"
    Public teks_Gaji As String = "Gaji"
    Public teks_Pesangon As String = "Pesangon"
    Public teks_AdaSelisih As String = "Ada Selisih"
    Public teks_TidakAdaSelisih As String = "Tidak Ada Selisih"
    Public teks_TidakAdaTransaksi As String = "Tidak Ada Transaksi"
    Public teks_TidakAdaDebet As String = "Tidak Ada Debet"
    Public teks_TidakAdaKredit As String = "Tidak Ada Kredit"
    Public teks_Proses As String = "Proses"
    Public teks_TrialBalance As String = "Trial Balance"
    Public teks_Total = "Total"
    Public teks_TOTAL_ = "T O T A L"

    Public JenisTampilan_Detail As String = "Detail"
    Public JenisTampilan_Rekap As String = "Rekap"

    Public teks_Sukses As String = "Sukses"
    Public teks_Gagal As String = "Gagal"


    Public teks_BukuPengawasan = "Buku Pengawasan"
    Public teks_SaldoAwal = "Saldo Awal"
    Public teks_SaldoAkhir = "Saldo Akhir"



    'Jenis-jenis Pajak :
    '-------------------
    Public JenisPajak_PPhPasal21 As String = "PPh Pasal 21"
    Public JenisPajak_PPhPasal22_Lokal As String = "PPh Pasal 22 - Lokal"
    Public JenisPajak_PPhPasal22_Impor As String = "PPh Pasal 22 - Impor"
    Public JenisPajak_PPhPasal23 As String = "PPh Pasal 23"
    Public JenisPajak_PPhPasal24 As String = "PPh Pasal 24"
    Public JenisPajak_PPhPasal25 As String = "PPh Pasal 25"
    Public JenisPajak_PPhPasal26 As String = "PPh Pasal 26"
    Public JenisPajak_PPhPasal29 As String = "PPh Pasal 29"
    Public JenisPajak_PPhPasal42 As String = "PPh Pasal 4 (2)"
    Public JenisPajak_PPN As String = "PPN"
    Public JenisPajak_PPN_Impor As String = "PPN - Impor"
    Public JenisPajak_KetetapanPajak As String = "Ketetapan Pajak"
    Public JenisPajak_PajakPajakImpor As String = "Pajak-pajak Impor"
    Public JenisPajak_BeaMasukImpor As String = "Bea Masuk Impor"
    Public JenisPajak_Non As String = Kosongan


    'Jenis-jenis PPh :
    '-------------------
    Public JenisPPh_NonPPh As String = "Non PPh"
    Public JenisPPh_Pasal21 As String = "Pasal 21"
    Public JenisPPh_Pasal22_Lokal As String = "Pasal 22 - Lokal"
    Public JenisPPh_Pasal22_Impor As String = "Pasal 22 - Impor"
    Public JenisPPh_Pasal23 As String = "Pasal 23"
    Public JenisPPh_Pasal24 As String = "Pasal 24"
    Public JenisPPh_Pasal25 As String = "Pasal 25"
    Public JenisPPh_Pasal26 As String = "Pasal 26"
    Public JenisPPh_Pasal29 As String = "Pasal 29"
    Public JenisPPh_Pasal42 As String = "Pasal 4 (2)"


    'Loko :
    '-------------------
    Public Loko_PelabuhanPenjual = "Pelabuhan Penjual"
    Public Loko_PelabuhanPembeli = "Pelabuhan Pembeli"
    Public Loko_GudangPembeli = "Gudang Pembeli"

    'Jenis-jenis PPh Berdasarkan Kode Setoran : (Sebetulnya ini tidak perlu. Dan suatu saat, hapus saja...!!!)
    '------------------------------------------

    'Public JenisPPh_Pasal21_100 = "Pasal 21 - 100"
    'Public JenisPPh_Pasal21_401 = "Pasal 21 - 401"

    'Public JenisPPh_Pasal23_100 = "Pasal 23 - 100"
    'Public JenisPPh_Pasal23_101 = "Pasal 23 - 101"
    'Public JenisPPh_Pasal23_102 = "Pasal 23 - 102"
    'Public JenisPPh_Pasal23_103 = "Pasal 23 - 103"
    'Public JenisPPh_Pasal23_104 = "Pasal 23 - 104"

    'Public JenisPPh_Pasal42_402 = "Pasal 4 (2) - 402"
    'Public JenisPPh_Pasal42_403 = "Pasal 4 (2) - 403"
    'Public JenisPPh_Pasal42_409 = "Pasal 4 (2) - 409"
    'Public JenisPPh_Pasal42_419 = "Pasal 4 (2) - 419"

    'Public JenisPPh_Pasal26_100 = "Pasal 26 - 100"
    'Public JenisPPh_Pasal26_101 = "Pasal 26 - 101"
    'Public JenisPPh_Pasal26_102 = "Pasal 26 - 102"
    'Public JenisPPh_Pasal26_103 = "Pasal 26 - 103"
    'Public JenisPPh_Pasal26_104 = "Pasal 26 - 104"
    'Public JenisPPh_Pasal26_105 = "Pasal 26 - 105"


    'Jenis-jenis PPN :
    '-----------------

    Public JenisPPN_NonPPN = "Non PPN"
    Public JenisPPN_Include = "PPN Include"
    Public JenisPPN_Exclude = "PPN Exclude"

    Public PilihanPPN_Dibiayakan = "Dibiayakan"
    Public PilihanPPN_Dikapitalisasi = "Dikapitalisasi"


    'Jenis-jenis Kode Setoran :
    '--------------------------
    Public KodeSetoran_Non As String = Kosongan
    Public KodeSetoran_100 As String = "100"
    Public KodeSetoran_101 As String = "101"
    Public KodeSetoran_102 As String = "102"
    Public KodeSetoran_103 As String = "103"
    Public KodeSetoran_104 As String = "104"
    Public KodeSetoran_105 As String = "105"
    Public KodeSetoran_401 As String = "401"
    Public KodeSetoran_402 As String = "402"
    Public KodeSetoran_403 As String = "403"
    Public KodeSetoran_409 As String = "409"
    Public KodeSetoran_419 As String = "419"
    Public KodeSetoran_423 As String = "423"
    'Public KodeSetoran_Pokok As String = "Pokok"    'Ini istilah dibikin-bikin oleh sistem, untuk kemudahan alokasi. Sebetulnya tidak ada istilah 'Kode Setoran Pokok' dan Perpajakan
    'Public KodeSetoran_Denda As String = "Denda"    'Ini istilah dibikin-bikin oleh sistem, untuk kemudahan alokasi. Sebetulnya tidak ada istilah 'Kode Setoran Denda' dan Perpajakan


    'Jenis-jenis Kode Setoran untuk PPh Pasal 21 :
    '---------------------------------------------
    Public PPhPasal21_100_SetoranMasa = "Setoran Masa"
    Public PPhPasal21_401_Pesangon = "Pesangon"

    'Jenis-jenis Kode Setoran untuk PPh Pasal 23 :
    '---------------------------------------------
    Public PPhPasal23_100_SewaAsset = "Sewa Asset"
    Public PPhPasal23_101_Dividen = "Dividen"
    Public PPhPasal23_102_Bunga = "Bunga"
    Public PPhPasal23_103_Royalty = "Royalty"
    Public PPhPasal23_104_JasaLainnya = "Jasa Lainnya"

    'Jenis-jenis Kode Setoran untuk PPh Pasal 4 (2) :
    '------------------------------------------------
    Public PPhPasal42_402_PengalihanTanahBangunan = "Pengalihan Tanah dan/atau Bangunan"
    Public PPhPasal42_403_SewaTanahBangunan = "Sewa Tanah dan/atau Bangunan"
    Public PPhPasal42_409_JasaKonstruksi = "Jasa Konstruksi"
    Public PPhPasal42_419_Dividen = "Dividen"

    'Jenis-jenis Kode Setoran untuk PPh Pasal 26 :
    '---------------------------------------------
    Public PPhPasal26_100_SewaAssetLuarNegeri = "Sewa Asset Luar Negeri"
    Public PPhPasal26_101_Dividen = "Dividen"
    Public PPhPasal26_102_Bunga = "Bunga"
    Public PPhPasal26_103_Royalty = "Royalty"
    Public PPhPasal26_104_Jasa = "Jasa"
    Public PPhPasal26_105_LabaPajakBUT = "Laba Pajak BUT"



    'Jenis-jenis Perlakuan PPN :
    '---------------------------
    Public PerlakuanPPN_TidakDipungut = "Tidak Dipungut"
    Public PerlakuanPPN_Dipungut = "Dipungut"
    Public PerlakuanPPN_Dibayar = "Dibayar"
    Public PerlakuanPPN_Ditanggung = "Ditanggung"
    Public PerlakuanPPN_Dibebaskan = "Dibebaskan"



    'Jenis Wajib Pajak :
    '-------------------
    Public JenisWP_OrangPribadi = "Orang Pribadi"
    Public JenisWP_BadanHukum = "Badan Hukum"


    'Lokasi Wajib Pajak :
    '--------------------
    Public LokasiWP_DalamNegeri = "Dalam Negeri"
    Public LokasiWP_LuarNegeri = "Luar Negeri"


    'Jenis Pemegang Saham :
    '-------------------
    Public JenisPS_OrangPribadi = "Orang Pribadi"
    Public JenisPS_BadanHukum = "Badan Hukum"


    'Lokasi Pemegang Saham :
    '--------------------
    Public LokasiPS_DalamNegeri = "Dalam Negeri"
    Public LokasiPS_LuarNegeri = "Luar Negeri"


    'Jenis Jenis Jasa :
    '------------------
    Public JenisJasa_JasaLainnya = "Jasa Lainnya"
    Public JenisJasa_JasaKonstruksi = "Jasa Konstruksi"
    Public JenisJasa_SewaAssetSelainTanahBangunan = "Sewa Asset Selain Tanah/Bangunan"
    Public JenisJasa_SewaTanahDanAtauBangunan = "Sewa Tanah dan/atau Bangunan"
    Public JenisJasa_BungaBagiHasil = "Bunga/Bagi Hasil"
    Public JenisJasa_Royalty = "Royalty"
    Public JenisJasa_Dividen = "Dividen"
    Public JenisJasa_LabaPajakBUT = "Laba Pajak BUT"
    Public JenisJasa_Lainnya = "Lainnya"



    'Tepat Waktu / Terlambat
    Public TWTL_TepatWaktu = "TW"
    Public TWTL_Terlambat = "TL"

    'Mitra (Customer/Supplier)
    Public Mitra_Supplier = "Supplier"
    Public Mitra_Customer = "Customer"
    Public Mitra_Keuangan = "Keuangan"
    Public Mitra_Afiliasi = "Afiliasi"

    Public Pilihan_Semua = "Semua"
    Public Pilihan_SEMUA_ = "SEMUA"
    Public Pilihan_All = "All"
    Public Pilihan_ALL_ = "ALL"
    Public Pilihan_CLOSED_ = "CLOSED"
    Public Pilihan_OPEN_ = "OPEN"

    Public Filter_Semua = "Semua"
    Public Filter_SEMUA_ = "SEMUA"

    'Status Lunas :
    Public StatusLunas_Lunas = "Lunas"
    Public StatusLunas_BelumLunas = "Belum Lunas"
    Public StatusLunas_LUNAS_ = "LUNAS"
    Public StatusLunas_BELUMLUNAS_ = "BELUM LUNAS"


    'Kode Lawan Transaksi :
    Public KodeLawanTransaksi_Customer = "CUST"
    Public KodeLawanTransaksi_Internal = "INT"
    Public KodeLawanTransaksi_DJP = "DJP"
    Public KodeLawanTransaksi_Karyawan = "KRY"
    Public KodeLawanTransaksi_BPJS_KS = "BPJS-KS"
    Public KodeLawanTransaksi_BPJS_TK = "BPJS-TK"
    Public KodeLawanTransaksi_KoperasiKaryawan = "KOPKAR"
    Public KodeLawanTransaksi_SerikatPekerja = "SP"

    'Nama Lawan Transaksi :
    Public NamaLawanTransaksi_Customer = "Customer"
    Public NamaLawanTransaksi_Internal = "Internal"
    Public NamaLawanTransaksi_DJP = "DJP"
    Public NamaLawanTransaksi_Karyawan = "Karyawan"
    Public NamaLawanTransaksi_BpjsKesehatan = "BPJS Kesehatan"
    Public NamaLawanTransaksi_BpjsKetenagakerjaan = "BPJS Ketenagakerjaan"
    Public NamaLawanTransaksi_KoperasiKaryawan = "Koperasi Karyawan"
    Public NamaLawanTransaksi_SerikatPekerja = "Serikat Pekerja"

    'Variabel Nama-nama Bulan
    Public Bulan_Januari = "Januari"
    Public Bulan_Februari = "Februari"
    Public Bulan_Maret = "Maret"
    Public Bulan_April = "April"
    Public Bulan_Mei = "Mei"
    Public Bulan_Juni = "Juni"
    Public Bulan_Juli = "Juli"
    Public Bulan_Agustus = "Agustus"
    Public Bulan_September = "September"
    Public Bulan_Oktober = "Oktober"
    Public Bulan_Nopember = "Nopember"
    Public Bulan_Desember = "Desember"

    'Variabel Angka-angka Bulan
    Public BulanAngka_Januari = 1
    Public BulanAngka_Februari = 2
    Public BulanAngka_Maret = 3
    Public BulanAngka_April = 4
    Public BulanAngka_Mei = 5
    Public BulanAngka_Juni = 6
    Public BulanAngka_Juli = 7
    Public BulanAngka_Agustus = 8
    Public BulanAngka_September = 9
    Public BulanAngka_Oktober = 10
    Public BulanAngka_Nopember = 11
    Public BulanAngka_Desember = 12

    'Variabel Angka Romawo
    Public Romawi_1 = "I"
    Public Romawi_2 = "II"
    Public Romawi_3 = "III"
    Public Romawi_4 = "IV"
    Public Romawi_5 = "V"
    Public Romawi_6 = "VI"
    Public Romawi_7 = "VII"
    Public Romawi_8 = "VIII"
    Public Romawi_9 = "IX"
    Public Romawi_10 = "X"
    Public Romawi_11 = "XI"
    Public Romawi_12 = "XII"

    'Variabel untuk Data Asset
    Public KelompokHarta_1 = "Kelompok 1"
    Public KelompokHarta_2 = "Kelompok 2"
    Public KelompokHarta_3 = "Kelompok 3"
    Public KelompokHarta_4 = "Kelompok 4"
    Public KelompokHarta_BangunanPermanen = "Kelompok Bangunan Permanen"
    Public KelompokHarta_BangunanTidakPermanen = "Kelompok Bangunan Tidak Permanen"
    Public KelompokHarta_Tanah = "Kelompok Tanah"

    'Jenis Transaksi IN :
    Public JenisTransaksi_DanaMasukLainnya = "Dana Masuk Lainnya"
    Public JenisTransaksi_PencairanPiutangUsaha = "Pencairan Piutang Usaha"
    Public JenisTransaksi_PencairanPiutangKaryawan = "Pencairan Piutang Karyawan"
    Public JenisTransaksi_PencairanPiutangPemegangSaham = "Pencairan Piutang Pemegang Saham"
    Public JenisTransaksi_PencairanPiutangAfiliasi = "Pencairan Piutang Afiliasi"
    Public JenisTransaksi_PencairanPiutangPihakKetiga = "Pencairan Piutang Pihak Ketiga"

    'Jenis Transaksi OUT :
    Public JenisTransaksi_PembayaranGaji = "Pembayaran Gaji"
    Public JenisTransaksi_PembayaranBpjsKesehatan = "Pembayaran BPJS Kesehatan"
    Public JenisTransaksi_PembayaranBpjsKetenagakerjaan = "Pembayaran BPJS Ketenagakerjaan"
    Public JenisTransaksi_PembayaranHutangUsaha = "Pembayaran Hutang Usaha"
    Public JenisTransaksi_PembayaranHutangKaryawan = "Pembayaran Hutang Karyawan"
    Public JenisTransaksi_PembayaranHutangBank = "Pembayaran Hutang Bank"
    Public JenisTransaksi_PembayaranHutangLeasing = "Pembayaran Hutang Leasing"
    Public JenisTransaksi_PembayaranHutangPemegangSaham = "Pembayaran Hutang Pemegang Saham"
    Public JenisTransaksi_PembayaranHutangAfiliasi = "Pembayaran Hutang Afiliasi"
    Public JenisTransaksi_PembayaranHutangPihakKetiga = "Pembayaran Hutang Pihak Ketiga"
    Public JenisTransaksi_PembayaranHutangPajak = "Pembayaran Hutang Pajak"
    Public JenisTransaksi_UangMukaPembelian = "Uang Muka Pembelian"
    Public JenisTransaksi_PemberianPiutangKas = "Pemberian Piutang Kas"
    Public JenisTransaksi_DanaKeluarLainnya = "Dana Keluar Lainnya"

    'Sarana Pembayaran :
    Public SaranaPembayaran_BANK = "BANK"
    Public SaranaPembayaran_KAS = "KAS"
    Public SaranaPembayaran_CASHADVANCE = "CASH ADVANCE"

    Public Pilihan_Ya = "Ya"
    Public Pilihan_YA_ = "YA"

    Public Pilihan_Tidak = "Tidak"
    Public Pilihan_TIDAK_ = "TIDAK"

    Public Pilihan_Belum = "Belum"
    Public Pilihan_BELUM_ = "BELUM"

    'Nama-Nama Tombol
    Public tombol_OK = "OK"
    Public tombol_BATAL = "Batal"
    Public tombol_TUTUP = "Tutup"
    Public tombol_SIMPAN = "Simpan"
    Public tombol_EDIT = "Edit"
    Public tombol_Revisi = "Revisi"
    Public tombol_SETUJUI = "Setujui"
    Public tombol_PEMBAYARAN = "Input/Edit Pembayaran"
    Public tombol_AJUKANPEMBAYARAN = "Ajukan Pembayaran"
    Public tombol_CETAK = "Cetak"
    Public tombol_LANJUTKAN = "Lanjutkan >>"
    Public tombol_LANJUTKAN_INPUT = "Lanjutkan Input >>"
    Public tombol_LANJUTKAN_EDIT = "Lanjutkan Edit >>"

    Public jawaban_OK = DialogResult.OK
    Public jawaban_Cancel = DialogResult.Cancel


    'Range Akun-akun Petty Cash :
    Public KodeAkun_PettyCash_Awal = 11101
    Public kodeakun_PettyCash_Akhir = 11199

    'Range Akun-akun Kas :
    Public KodeAkun_Kas_Awal = 11201
    Public kodeakun_Kas_Akhir = 11299

    'Range Akun-akun Bank :
    Public KodeAkun_Bank_Awal = 11301
    Public kodeakun_Bank_Akhir = 11399

    'Range Akun-akun Cash Advance :
    Public KodeAkun_CashAdvance_Awal = 11401
    Public kodeakun_CashAdvance_Akhir = 11499

    'Range Akun-akun Sarana Pembayaran :
    Public KodeAkun_SaranaPembayaran_Awal = 11001
    Public kodeakun_SaranaPembayaran_Akhir = 11499

    'Filter List COA :
    Public FilterListCOA_Pembelian = " AND ( " &
        "    COA LIKE '116%' " &
        " OR COA LIKE '117%' " &
        " OR COA LIKE '121%' " &
        " OR COA LIKE '122%' " &
        " OR COA LIKE '123%' " &
        " OR COA LIKE '124%' " &
        " OR COA LIKE '125%' " &
        " OR COA LIKE '126%' " &
        " OR COA LIKE '127%' " &
        " OR COA LIKE '128%' " &
        " OR COA LIKE '129%' " &
        " OR COA LIKE '5%'   " &
        " OR COA LIKE '6%' ) "
    Public FilterListCOA_Kas = " AND COA BETWEEN " & KodeAkun_Kas_Awal & " AND " & kodeakun_Kas_Akhir & " "
    Public FilterListCOA_PettyCash = " AND COA LIKE '1110%' "
    Public FilterListCOA_CashAdvance = " AND COA LIKE '1140%' "
    Public FilterListCOA_Bank = " AND COA BETWEEN " & KodeAkun_Bank_Awal & " AND " & kodeakun_Bank_Akhir & " "
    Public FilterListCOA_SaranaPembayaran = " AND COA BETWEEN " & KodeAkun_SaranaPembayaran_Awal & " AND " & kodeakun_SaranaPembayaran_Akhir & " "
    Public FilterListCOA_Amortisasi = " AND COA LIKE '116%' "
    Public FilterListCOA_BiayaAmortisasi = Kosongan 'Ini algoritmanya dinamis. Tidak ditentukan disini...!!!
    Public FilterListCOA_AssetTanah = " AND COA LIKE '121%' "
    Public FilterListCOA_AssetTetap = " AND COA LIKE '12%' AND COA LIKE '%0' "
    Public FilterListCOA_AssetTetap_SelainTanah = " AND (COA BETWEEN 12200 AND 12999) AND COA LIKE '%0' "
    Public FilterListCOA_BiayaPenyusutan = " AND ( (COA BETWEEN 62000 AND 62599) OR (COA BETWEEN 53200 AND 53999) ) "
    Public FilterListCOA_AkumulasiPenyusutan = " AND COA LIKE '12%1' "
    Public FilterListCOA_PokokPajak = " AND ( ((COA BETWEEN 10000 AND 29999) OR (COA BETWEEN 60000 AND 69999)) " &
        " AND ( (Nama_Akun LIKE '%Pajak%') OR (Nama_Akun LIKE '%PPN%') OR (Nama_Akun LIKE '%PPh%') ) ) "
    Public FilterListCOA_DepositOperasional = " AND ( COA = '11608' ) "
    Public FilterListCOA_AktivaLainnya = " AND ( COA LIKE '13%' OR COA LIKE '14%' ) "

    'Kepegawaian
    Public JenisPegawai
    Public JenisPegawai_Pegawai = "Pegawai"
    Public JenisPegawai_BukanPegawai = "Bukan Pegawai"


    'Tarif Pasal 17 UU HPP
    Public Lapisan1_Maximal = 60000000
    Public Lapisan2_Maximal = 250000000
    Public Lapisan3_Maximal = 500000000
    Public Lapisan4_Maximal = 5000000000

    Public Tarif_Lapisan1 = Persen(5)
    Public Tarif_Lapisan2 = Persen(15)
    Public Tarif_Lapisan3 = Persen(25)
    Public Tarif_Lapisan4 = Persen(30)
    Public Tarif_Lapisan5 = Persen(35)

    'Jenis-jenis Jurnal
    Public JenisJurnal_HutangBank = "Hutang Bank"
    Public JenisJurnal_HutangLeasing = "Hutang Leasing"
    Public JenisJurnal_HutangPihakKetiga = "Hutang Pihak Ketiga"
    Public JenisJurnal_PiutangPihakKetiga = "Piutang Pihak Ketiga"
    Public JenisJurnal_HutangAfiliasi = "Hutang Afiliasi"
    Public JenisJurnal_PiutangAfiliasi = "Piutang Afiliasi"
    Public JenisJurnal_DepositOperasional = "Deposit Operasional"
    Public JenisJurnal_Dividen = "Dividen"
    Public JenisJurnal_CashAdvance1 = "Cash Advance 1"
    Public JenisJurnal_CashAdvance2 = "Cash Advance 2"
    Public JenisJurnal_CashAdvance3 = "Cash Advance 3"
    Public JenisJurnal_Pemindahbukuan = "Pemindahbukuan"
    Public JenisJurnal_Pembelian = "Pembelian"
    Public JenisJurnal_PembelianImpor = "Pembelian Impor"
    Public JenisJurnal_ReturPembelian = "Retur Pembelian"
    Public JenisJurnal_Penjualan = "Penjualan"
    Public JenisJurnal_PenjualanEkspor = "Penjualan Ekspor"
    Public JenisJurnal_Asset = "Asset"
    Public JenisJurnal_DisposalAsset = "Disposal Asset"
    Public JenisJurnal_BangunanDalamPenyelesaian = "Bangunan Dalam Penyelesaian"
    Public JenisJurnal_ReturPenjualan = "Retur Penjualan"
    Public JenisJurnal_PBk = "PBk"
    Public JenisJurnal_Gaji = "Gaji"
    Public JenisJurnal_CekBupot = "Cek Bupot"
    Public JenisJurnal_HutangKaryawan = "Hutang Karyawan"
    Public JenisJurnal_PiutangKaryawan = "Piutang Karyawan"
    Public JenisJurnal_HutangPemegangSaham = "Hutang Pemegang Saham"
    Public JenisJurnal_PiutangPemegangSaham = "Piutang Pemegang Saham"
    Public JenisJurnal_Penyusutan = "Penyusutan"
    Public JenisJurnal_Amortisasi = "Amortisasi"
    Public JenisJurnal_KetetapanPajak = "Ketetapan Pajak"
    Public JenisJurnal_PajakImpor = "Pajak Impor"
    Public JenisJurnal_AdjusmentHPP = "Adjusment HPP"
    Public JenisJurnal_AdjusmentPajak = "Adjusment Pajak"
    Public JenisJurnal_AdjusmentGaji = "Adjusment Gaji"
    Public JenisJurnal_AdjusmentForex = "Adjusment Forex"
    Public JenisJurnal_AdjusmentSaldoAwal = "Adjusment Saldo Awal"
    Public JenisJurnal_AdjusmentSelisih = "Adjusment Selisih"
    Public JenisJurnal_AdjusmentLainnya = "Adjusment Lainnya"

    'Jenis Invoice :
    Public JenisInvoice_Biasa = "Biasa"
    Public JenisInvoice_Gabungan = "Gabungan"

    'Metode Pembayaran :
    Public MetodePembayaran_Normal = "Normal"
    Public MetodePembayaran_Termin = "Termin"

    'Basis Perhitungan Termin :
    Public BasisPerhitunganTermin_Prosentase = "Prosentase"
    Public BasisPerhitunganTermin_Nominal = "Nominal"

    'Termin
    Public TahapTermin_UangMuka = "Uang Muka"
    Public TahapTermin_Termin1 = "Termin 1"
    Public TahapTermin_Termin2 = "Termin 2"
    Public TahapTermin_Termin3 = "Termin 3"
    Public TahapTermin_Pelunasan = "Pelunasan"

    'Jenis Relasi :
    Public JenisRelasi_Afiliasi = "Afiliasi"
    Public JenisRelasi_NonAfiliasi = "Non Afiliasi"

    'Jenis Produk :
    Public JenisProduk_Semua = "Semua"
    Public JenisProduk_Barang = "Barang"
    Public JenisProduk_Jasa = "Jasa"
    Public JenisProduk_BarangDanJasa = "Barang dan Jasa"
    Public JenisProduk_JasaKonstruksi = "Jasa Konstruksi"

    'Destinasi Penjualan
    Public DestinasiPenjualan_Lokal = "Lokal"
    Public DestinasiPenjualan_Ekspor = "Ekspor"

    'Asal Pembelian
    Public AsalPembelian_Lokal = "Lokal"
    Public AsalPembelian_Impor = "Impor"

    'Opsi Jatuh Tempo :
    Public JatuhTempo_Semua = "Semua"
    Public JatuhTempo_JT = "JT"
    Public JatuhTempo_Belum = "Belum"

    'Koreksi :
    Public TandaKoreksi = "X"


    'Jenis Pembelian :
    Public JenisPembelian
    Public JenisPembelian_Tunai = "Tunai"
    Public JenisPembelian_Tempo = "Tempo"

    'Jenis Pembelian :
    Public JenisPenjualan
    Public JenisPenjualan_Tunai = "Tunai"
    Public JenisPenjualan_Tempo = "Tempo"

    'Bank/Leasing :
    Public bl_Bank = "Bank"
    Public bl_Leasing = "Leasing"

    'Hutang/Piutang :
    Public hp_Hutang = "Hutang"
    Public hp_Piutang = "Piutang"


    'Level Kecil/Menengah/Besar :
    Public Level_Kecil = "Kecil"
    Public Level_Menengah = "Menengah"
    Public Level_Besar = "Besar"

    'Jenis Stok :
    Public JenisStok_BahanBaku = "Bahan Baku"
    Public JenisStok_BahanPenolong = "Bahan Penolong"
    Public JenisStok_BarangDalamProses = "Barang Dalam Proses"
    Public JenisStok_BarangJadi = "Barang Jadi"

    'Value Khusus :
    Public val_Normal = "Normal"
    Public val_Bahan = "Bahan"
    Public val_Lokal = "Lokal"
    Public val_Import = "Import"

    'Peruntukan Project :
    Public Peruntukan_Project = "Project"
    Public Peruntukan_NonProject = "Non Project"

    'Kategori Penerimaan/Pengeluaran Bank/Cash :
    Public Kategori_PencairanPiutang = "Pencairan Piutang"
    Public Kategori_PenerimaanTunai = "Penerimaan Tunai"
    Public Kategori_Investasi = "Investasi"
    Public Kategori_Pengembalian = "Pengembalian"
    Public Kategori_PembayaranHutang = "Pembayaran Hutang"
    Public Kategori_PengeluaranTunai = "Pengeluaran Tunai"
    Public Kategori_Pemindahbukuan = "Pemindahbukuan"
    Public Kategori_Try = "Try"

    'Jenis-jenis Peruntukan Pencairan Piutang :
    Public Peruntukan_PencairanPiutangUsaha_NonAfiliasi = "Pencairan Piutang Usaha"
    Public Peruntukan_PencairanPiutangUsaha_Afiliasi = "Pencairan Piutang Usaha Afiliasi"
    Public Peruntukan_PencairanPiutangUsaha_Ekspor = "Pencairan Piutang Usaha Ekspor"
    Public Peruntukan_PencairanPiutangAfiliasi = "Pencairan Piutang Afiliasi"
    Public Peruntukan_PencairanPiutangPihakKetiga = "Pencairan Piutang Pihak Ketiga"
    Public Peruntukan_PencairanPiutangPemegangSaham = "Pencairan Piutang Pemegang Saham"
    Public Peruntukan_PencairanPiutangDividen = "Pencairan Piutang Dividen"
    Public Peruntukan_PencairanPiutangKaryawan = "Pencairan Piutang Karyawan"
    Public Peruntukan_PencairanPiutangLainnya = "Pencairan Piutang Lainnya"


    'Jenis-jenis Hutang :
    Public JenisHutang_Usaha = "Hutang Usaha"
    Public JenisHutang_Biaya = "Hutang Biaya"
    Public JenisHutang_BPJSKesehatan = "Hutang BPJS Kesehatan"
    Public JenisHutang_BPJSKetenagakerjaan = "Hutang BPJS Ketenagakerjaan"
    Public JenisHutang_KoperasiKaryawan = "Hutang Koperasi Karyawan"
    Public JenisHutang_Serikat = "Hutang Serikat"
    Public JenisHutang_PihakKetiga = "Hutang Pihak Ketiga"
    Public JenisHutang_Karyawan = "Hutang Karyawan"
    Public JenisHutang_Leasing = "Hutang Leasing"
    Public JenisHutang_Bank = "Hutang Bank"
    Public JenisHutang_PemegangSaham = "Hutang Pemegang Saham"
    Public JenisHutang_Afiliasi = "Hutang Afiliasi"
    Public JenisHutang_Pajak = "Hutang Pajak"
    Public JenisHutang_LancarLainnya = "Hutang Lancar Lainnya"

    'Jenis-jenis Peruntukan Pembayaran Hutang :
    Public Peruntukan_PembayaranHutangUsaha_NonAfiliasi = "Pembayaran Hutang Usaha"
    Public Peruntukan_PembayaranHutangUsaha_Afiliasi = "Pembayaran Hutang Usaha Afiliasi"
    Public Peruntukan_PembayaranHutangBiaya = "Pembayaran Hutang Biaya"
    Public Peruntukan_PembayaranHutangGaji = "Pembayaran Hutang Gaji"
    Public Peruntukan_PembayaranHutangBPJSKesehatan = "Pembayaran Hutang BPJS Kesehatan"
    Public Peruntukan_PembayaranHutangBPJSKetenagakerjaan = "Pembayaran Hutang BPJS Ketenagakerjaan"
    Public Peruntukan_PembayaranHutangKoperasiKaryawan = "Pembayaran Hutang Koperasi Karyawan"
    Public Peruntukan_PembayaranHutangSerikat = "Pembayaran Hutang Serikat"
    Public Peruntukan_PembayaranHutangPihakKetiga = "Pembayaran Hutang Pihak Ketiga"
    Public Peruntukan_PembayaranHutangKaryawan = "Pembayaran Hutang Karyawan"
    Public Peruntukan_PembayaranHutangLeasing = "Pembayaran Hutang Leasing"
    Public Peruntukan_PembayaranHutangBank = "Pembayaran Hutang Bank"
    Public Peruntukan_PembayaranHutangPemegangSaham = "Pembayaran Hutang Pemegang Saham"
    Public Peruntukan_PembayaranHutangAfiliasi = "Pembayaran Hutang Afiliasi"
    Public Peruntukan_PembayaranHutangPajak = "Pembayaran Hutang Pajak"
    Public Peruntukan_PembayaranHutangDividen = "Pembayaran Hutang Dividen"
    Public Peruntukan_PembayaranHutangLancarLainnya = "Pembayaran Hutang Lancar Lainnya"

    'Jenis-jenis Peruntukan Penerimaan/Pengeluaran Tunai :
    Public Peruntukan_PiutangPemegangSaham = "Piutang Pemegang Saham"
    Public Peruntukan_PiutangKaryawan = "Piutang Karyawan"
    Public Peruntukan_PiutangPihakKetiga = "Piutang Pihak Ketiga"
    Public Peruntukan_PiutangAfiliasi = "Piutang Afiliasi"
    Public Peruntukan_InvoiceTunai = "Invoice Tunai"
    Public Peruntukan_PenjualanEceranHarian = "Penjualan Eceran - Harian"
    'Public Peruntukan_UangMukaPenjualan = "Uang Muka Penjualan"
    'Public Peruntukan_UangMukaPembelian = "Uang Muka Pembelian"
    'Public Peruntukan_Termin = "Termin"
    'Public Peruntukan_Pelunasan = "Pelunasan"
    Public Peruntukan_UangMukaJangkaPanjang = "Uang Muka Jangka Panjang"
    Public Peruntukan_HutangOngkosKirimPenjualan = "Hutang Ongkos Kirim Penjualan"
    Public Peruntukan_HutangKoperasiKaryawan = "Hutang Koperasi Karyawan"
    Public Peruntukan_HutangSerikat = "Hutang Serikat"
    Public Peruntukan_HutangPihakKetiga = "Hutang Pihak Ketiga"
    Public Peruntukan_HutangKaryawan = "Hutang Karyawan"
    Public Peruntukan_HutangLancarLainnya = "Hutang Lancar Lainnya"
    Public Peruntukan_HutangLeasing = "Hutang Leasing"
    Public Peruntukan_HutangBank = "Hutang Bank"
    Public Peruntukan_HutangLembagaKeuanganNonBank = "Hutang Lembaga Keuangan Non Bank"
    Public Peruntukan_HutangPemegangSaham = "Hutang Pemegang Saham"
    Public Peruntukan_HutangAfiliasi = "Hutang Afiliasi"
    Public Peruntukan_DepositOperasional = "Deposit Operasional"
    Public Peruntukan_BankGaransi = "Bank Garansi"

    'Jenis-jenis Peruntukan Investasi :
    Public Peruntukan_InvestasiModal = "Modal"
    Public Peruntukan_InvestasiDeposito = "Investasi Deposito"
    Public Peruntukan_InvestasiSuratBerharga = "Investasi Surat Berharga"
    Public Peruntukan_InvestasiLogamMulia = "Investasi Logam Mulia"
    Public Peruntukan_InvestasiPadaPerusahaanAnak = "Investasi Pada Perusahaan Anak"
    Public Peruntukan_InvestasiGoodWill = "Investasi Goodwill"

    'Jenis-jenis Peruntukan Pengembalian :
    Public Peruntukan_LebihBayarPPhBadan = "Lebih Bayar PPh Badan"
    Public Peruntukan_KelebihanPenerimaanPembayaranHutang = "Kelebihan Perimaan Pembayaran Hutang"


    'Tahapan :
    Public Tahapan_Pengajuan = "Pengajuan"
    Public Tahapan_Pembayaran = "Pembayaran"


    'Status :
    Public Status_All = "All"
    Public Status_Semua = "Semua"
    Public Status_Open = "Open"
    Public Status_Dicetak = "Dicetak"
    Public Status_Diproses = "Diproses"
    Public Status_Dibundel = "Dibundel"
    Public Status_Ditolak = "Ditolak"
    Public Status_Disetujui = "Disetujui"
    Public Status_Dibayar = "Dibayar"
    Public Status_Used = "Used"
    Public Status_Closed = "Closed"

    'Jenis Form Cetak :
    Public JenisFormCetak_PO = "PO"
    Public JenisFormCetak_SuratJalan = "Surat Jalan"
    Public JenisFormCetak_BAST = "BAST"
    Public JenisFormCetak_Invoice = "Invoice"
    Public JenisFormCetak_NotaRetur = "Nota Retur"
    Public JenisFormCetak_NotaDebet = "Nota Debet"
    Public JenisFormCetak_PengajuanPengeluaran = "Pengajuan Pengeluaran"
    Public JenisFormCetak_BundelanPengajuanPengeluaran = "Bundelan Pengajuan Pengeluaran"
    Public JenisFormCetak_JurnalVoucher = "Jurnal Voucher"


    Public JalurUtama = "Jalur Utama"

    Public SlashGanda_Pemisah = " // "

    Public MataUang_Rupiah = "Mata Uang Rupiah"
    Public MataUang_Asing = "Mata Uang Asing"

    Public KodeMataUang_IDR = "IDR"
    Public KodeMataUang_NonIDR = "Non-IDR"
    Public KodeMataUang_Semua = "Semua"
    Public KodeMataUang_USD = "USD"
    Public KodeMataUang_AUD = "AUD"
    Public KodeMataUang_JPY = "JPY"
    Public KodeMataUang_CNY = "CNY"
    Public KodeMataUang_EUR = "EUR"
    Public KodeMataUang_SGD = "SGD"
    Public KodeMataUang_GBP = "GBP"

    Public KursAkhirBulan_USD(13) As Decimal
    Public KursAkhirBulan_AUD(13) As Decimal
    Public KursAkhirBulan_JPY(13) As Decimal
    Public KursAkhirBulan_CNY(13) As Decimal
    Public KursAkhirBulan_EUR(13) As Decimal

    Public los_L = "L"
    Public los_OS = "OS"

    'Simbol :
    Public _X_ = "X"

    Public ProsesKeluarAplikasi As Boolean = False

End Module
