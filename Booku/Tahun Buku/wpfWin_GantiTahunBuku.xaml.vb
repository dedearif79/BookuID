Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls


Public Class wpfWin_GantiTahunBuku

    Public FungsiForm As String
    Public JudulForm As String


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        KontenComboTahunBuku()

        Select Case FungsiForm
            Case FungsiForm_MASUKTAHUNBUKU
                JudulForm = "Masuk Tahun Buku"
                btn_Ganti.Content = "Masuk"
            Case FungsiForm_GANTITAHUNBUKU
                JudulForm = "Ganti Tahun Buku"
                btn_Ganti.Content = "Ganti"
            Case FungsiForm_EksekusiSub_PROSESGANTITAHUNBUKU
                Me.Visibility = Visibility.Hidden
                ProsesGantiTahunBuku()
                Me.Close()
                Return
        End Select

        Me.Visibility = Visibility.Visible
        Me.Title = JudulForm
        lbl_JudulForm.Text = JudulForm

    End Sub


    Sub KontenComboTahunBuku()
        cmb_TahunBuku.Items.Clear()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ORDER BY Tahun_Buku ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Dim ListTahunBuku = Nothing
        Do While dr.Read
            ListTahunBuku = dr.Item("Tahun_Buku")
            cmb_TahunBuku.Items.Add(ListTahunBuku)
        Loop
        cmb_TahunBuku.SelectedValue = ListTahunBuku
        AksesDatabase_General(Tutup)
    End Sub


    Public Sub ProsesGantiTahunBuku()

        Dim ProsesGantiTahun As Boolean = True

        BuatDsnGeneral()
        BuatDsnTransaksi(TahunBukuBaru)

        TahunBukuAktif = TahunBukuBaru
        TahunBukuAktifAsli = TahunBukuAktif
        AwalTahunBukuAktif = "01/01/" & TahunBukuAktif
        TahunBukuKemarin = TahunBukuAktif - 1
        AkhirTahunBukuKemarin = "31/12/" & TahunBukuKemarin
        TanggalAkhirTahunKemarin = New Date(TahunBukuAktif - 1, 12, 31)
        TanggalAwalTahunBukuAktif = New Date(TahunBukuAktif, 1, 1)
        TanggalAkhirTahunBukuAktif = New Date(TahunBukuAktif, 12, 31)
        If TahunKabisat_TahunBukuAktif = False _
            And AmbilAngka(BulanIni) = 2 _
            And AmbilAngka(TanggalHariIni) = 29 _
            Then
            HariIniTahunBukuKemarin = 28 & "/" & BulanIni & "/" & TahunBukuKemarin
        Else
            HariIniTahunBukuKemarin = TanggalHariIni & "/" & BulanIni & "/" & TahunBukuKemarin
        End If

        'Variabel Awalan Sistem Penomoran :
        AwalanNomorJV = "JV-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNomorJV = Len(AwalanNomorJV)
        PanjangTeks_AwalanNomorJV_Plus1 = Len(AwalanNomorJV) + 1

        AwalanPO = "PO-"
        PanjangTeks_AwalanPO = Len(AwalanPO)
        PanjangTeks_AwalanPO_Plus1 = Len(AwalanPO) + 1

        AwalanSJ = "SJ-"
        PanjangTeks_AwalanSJ = Len(AwalanSJ)
        PanjangTeks_AwalanSJ_Plus1 = Len(AwalanSJ) + 1

        AwalanBAST = "BAST-"
        PanjangTeks_AwalanBAST = Len(AwalanBAST)
        PanjangTeks_AwalanBAST_Plus1 = Len(AwalanBAST) + 1

        AwalanPEMB = "PEMB-"
        PanjangTeks_AwalanPEMB = Len(AwalanPEMB)
        PanjangTeks_AwalanPEMB_Plus1 = Len(AwalanPEMB) + 1

        AwalanPEMB_PlusTahunBuku = AwalanPEMB & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanPEMB_PlusTahunBuku = Len(AwalanPEMB_PlusTahunBuku)
        PanjangTeks_AwalanPEMB_PlusTahunBuku_Plus1 = Len(AwalanPEMB_PlusTahunBuku) + 1

        AwalanPENJ = "PENJ-"
        PanjangTeks_AwalanPENJ = Len(AwalanPENJ)
        PanjangTeks_AwalanPENJ_Plus1 = Len(AwalanPENJ) + 1

        AwalanPENJ_PlusTahunBuku = AwalanPENJ & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanPENJ_PlusTahunBuku = Len(AwalanPENJ_PlusTahunBuku)
        PanjangTeks_AwalanPENJ_PlusTahunBuku_Plus1 = Len(AwalanPENJ_PlusTahunBuku) + 1

        AwalanNR = "NR-"
        PanjangTeks_AwalanNR = Len(AwalanNR)
        PanjangTeks_AwalanNR_Plus1 = Len(AwalanNR) + 1

        AwalanINV = "INV-"
        PanjangTeks_AwalanINV = Len(AwalanINV)
        PanjangTeks_AwalanINV_Plus1 = Len(AwalanINV) + 1

        AwalanBPHU = "BPHU-"
        PanjangTeks_AwalanBPHU = Len(AwalanBPHU)
        PanjangTeks_AwalanBPHU_Plus1 = Len(AwalanBPHU) + 1

        AwalanBPHU_PlusTahunBuku = AwalanBPHU & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHU_PlusTahunBuku = Len(AwalanBPHU_PlusTahunBuku)
        PanjangTeks_AwalanBPHU_PlusTahunBuku_Plus1 = Len(AwalanBPHU_PlusTahunBuku) + 1

        AwalanBPHD = "BPHD-"
        PanjangTeks_AwalanBPHD = Len(AwalanBPHD)
        PanjangTeks_AwalanBPHD_Plus1 = Len(AwalanBPHD) + 1

        AwalanBPHD_PlusTahunBuku = AwalanBPHD & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHD_PlusTahunBuku = Len(AwalanBPHD_PlusTahunBuku)
        PanjangTeks_AwalanBPHD_PlusTahunBuku_Plus1 = Len(AwalanBPHD_PlusTahunBuku) + 1

        AwalanBPHKS = "BPHKS-"
        PanjangTeks_AwalanBPHKS = Len(AwalanBPHKS)
        PanjangTeks_AwalanBPHKS_Plus1 = Len(AwalanBPHKS) + 1

        AwalanBPHKS_PlusTahunBuku = AwalanBPHKS & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHKS_PlusTahunBuku = Len(AwalanBPHKS_PlusTahunBuku)
        PanjangTeks_AwalanBPHKS_PlusTahunBuku_Plus1 = Len(AwalanBPHKS_PlusTahunBuku) + 1

        AwalanBPHTK = "BPHTK-"
        PanjangTeks_AwalanBPHTK = Len(AwalanBPHTK)
        PanjangTeks_AwalanBPHTK_Plus1 = Len(AwalanBPHTK) + 1

        AwalanBPHTK_PlusTahunBuku = AwalanBPHTK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHTK_PlusTahunBuku = Len(AwalanBPHTK_PlusTahunBuku)
        PanjangTeks_AwalanBPHTK_PlusTahunBuku_Plus1 = Len(AwalanBPHTK_PlusTahunBuku) + 1

        AwalanBPHKK = "BPHKK-"
        PanjangTeks_AwalanBPHKK = Len(AwalanBPHKK)
        PanjangTeks_AwalanBPHKK_Plus1 = Len(AwalanBPHKK) + 1

        AwalanBPHKK_PlusTahunBuku = AwalanBPHKK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHKK_PlusTahunBuku = Len(AwalanBPHKK_PlusTahunBuku)
        PanjangTeks_AwalanBPHKK_PlusTahunBuku_Plus1 = Len(AwalanBPHKK_PlusTahunBuku) + 1

        AwalanBPHS = "BPHS-"
        PanjangTeks_AwalanBPHS = Len(AwalanBPHS)
        PanjangTeks_AwalanBPHS_Plus1 = Len(AwalanBPHS) + 1

        AwalanBPHS_PlusTahunBuku = AwalanBPHS & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHS_PlusTahunBuku = Len(AwalanBPHS_PlusTahunBuku)
        PanjangTeks_AwalanBPHS_PlusTahunBuku_Plus1 = Len(AwalanBPHS_PlusTahunBuku) + 1

        AwalanBPHB = "BPHB-"
        PanjangTeks_AwalanBPHB = Len(AwalanBPHB)
        PanjangTeks_AwalanBPHB_Plus1 = Len(AwalanBPHB) + 1

        AwalanBPHB_PlusTahunBuku = AwalanBPHB & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHB_PlusTahunBuku = Len(AwalanBPHB_PlusTahunBuku)
        PanjangTeks_AwalanBPHB_PlusTahunBuku_Plus1 = Len(AwalanBPHB_PlusTahunBuku) + 1

        AwalanBPHL = "BPHL-"
        PanjangTeks_AwalanBPHL = Len(AwalanBPHL)
        PanjangTeks_AwalanBPHL_Plus1 = Len(AwalanBPHL) + 1

        AwalanBPHL_PlusTahunBuku = AwalanBPHL & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHL_PlusTahunBuku = Len(AwalanBPHL_PlusTahunBuku)
        PanjangTeks_AwalanBPHL_PlusTahunBuku_Plus1 = Len(AwalanBPHL_PlusTahunBuku) + 1

        AwalanBPHPK = "BPHPK-"
        PanjangTeks_AwalanBPHPK = Len(AwalanBPHPK)
        PanjangTeks_AwalanBPHPK_Plus1 = Len(AwalanBPHPK) + 1

        AwalanBPHPK_PlusTahunBuku = AwalanBPHPK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHPK_PlusTahunBuku = Len(AwalanBPHPK_PlusTahunBuku)
        PanjangTeks_AwalanBPHPK_PlusTahunBuku_Plus1 = Len(AwalanBPHPK_PlusTahunBuku) + 1

        AwalanBPPPK = "BPPPK-"
        PanjangTeks_AwalanBPPPK = Len(AwalanBPPPK)
        PanjangTeks_AwalanBPPPK_Plus1 = Len(AwalanBPPPK) + 1

        AwalanBPPPK_PlusTahunBuku = AwalanBPPPK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPPK_PlusTahunBuku = Len(AwalanBPPPK_PlusTahunBuku)
        PanjangTeks_AwalanBPPPK_PlusTahunBuku_Plus1 = Len(AwalanBPPPK_PlusTahunBuku) + 1

        AwalanBPHA = "BPHA-"
        PanjangTeks_AwalanBPHA = Len(AwalanBPHA)
        PanjangTeks_AwalanBPHA_Plus1 = Len(AwalanBPHA) + 1

        AwalanBPHA_PlusTahunBuku = AwalanBPHA & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHA_PlusTahunBuku = Len(AwalanBPHA_PlusTahunBuku)
        PanjangTeks_AwalanBPHA_PlusTahunBuku_Plus1 = Len(AwalanBPHA_PlusTahunBuku) + 1

        AwalanBPPA = "BPPA-"
        PanjangTeks_AwalanBPPA = Len(AwalanBPPA)
        PanjangTeks_AwalanBPPA_Plus1 = Len(AwalanBPPA) + 1

        AwalanBPPA_PlusTahunBuku = AwalanBPPA & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPA_PlusTahunBuku = Len(AwalanBPPA_PlusTahunBuku)
        PanjangTeks_AwalanBPPA_PlusTahunBuku_Plus1 = Len(AwalanBPPA_PlusTahunBuku) + 1

        AwalanBPHK = "BPHK-"
        PanjangTeks_AwalanBPHK = Len(AwalanBPHK)
        PanjangTeks_AwalanBPHK_Plus1 = Len(AwalanBPHK) + 1

        AwalanBPHK_PlusTahunBuku = AwalanBPHK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHK_PlusTahunBuku = Len(AwalanBPHK_PlusTahunBuku)
        PanjangTeks_AwalanBPHK_PlusTahunBuku_Plus1 = Len(AwalanBPHK_PlusTahunBuku) + 1

        AwalanBPPK = "BPPK-"
        PanjangTeks_AwalanBPPK = Len(AwalanBPPK)
        PanjangTeks_AwalanBPPK_Plus1 = Len(AwalanBPPK) + 1

        AwalanBPPK_PlusTahunBuku = AwalanBPPK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPK_PlusTahunBuku = Len(AwalanBPPK_PlusTahunBuku)
        PanjangTeks_AwalanBPPK_PlusTahunBuku_Plus1 = Len(AwalanBPPK_PlusTahunBuku) + 1

        AwalanBPM = "BPM-"
        PanjangTeks_AwalanBPM = Len(AwalanBPM)
        PanjangTeks_AwalanBPM_Plus1 = Len(AwalanBPM) + 1

        AwalanBPM_PlusTahunBuku = AwalanBPM & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPM_PlusTahunBuku = Len(AwalanBPM_PlusTahunBuku)
        PanjangTeks_AwalanBPM_PlusTahunBuku_Plus1 = Len(AwalanBPM_PlusTahunBuku) + 1

        AwalanBPHPS = "BPHPS-"
        PanjangTeks_AwalanBPHPS = Len(AwalanBPHPS)
        PanjangTeks_AwalanBPHPS_Plus1 = Len(AwalanBPHPS) + 1

        AwalanBPHPS_PlusTahunBuku = AwalanBPHPS & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHPS_PlusTahunBuku = Len(AwalanBPHPS_PlusTahunBuku)
        PanjangTeks_AwalanBPHPS_PlusTahunBuku_Plus1 = Len(AwalanBPHPS_PlusTahunBuku) + 1

        AwalanBPPPS = "BPPPS-"
        PanjangTeks_AwalanBPPPS = Len(AwalanBPPPS)
        PanjangTeks_AwalanBPPPS_Plus1 = Len(AwalanBPPPS) + 1

        AwalanBPPPS_PlusTahunBuku = AwalanBPPPS & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPPS_PlusTahunBuku = Len(AwalanBPPPS_PlusTahunBuku)
        PanjangTeks_AwalanBPPPS_PlusTahunBuku_Plus1 = Len(AwalanBPPPS_PlusTahunBuku) + 1

        AwalanNPPHU_PlusTahunBuku = "NPPHU-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPHU_PlusTahunBuku = Len(AwalanNPPHU_PlusTahunBuku)
        PanjangTeks_AwalanNPPHU_PlusTahunBuku_Plus1 = Len(AwalanNPPHU_PlusTahunBuku) + 1

        AwalanNRBHU_PlusTahunBuku = "NRBHU-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBHU_PlusTahunBuku = Len(AwalanNRBHU_PlusTahunBuku)
        PanjangTeks_AwalanNRBHU_PlusTahunBuku_Plus1 = Len(AwalanNRBHU_PlusTahunBuku) + 1

        AwalanBPHG = "BPHG-"
        AwalanBPHG_PlusTahunBuku = AwalanBPHG & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHG_PlusTahunBuku = Len(AwalanBPHG_PlusTahunBuku)
        PanjangTeks_AwalanBPHG_PlusTahunBuku_Plus1 = Len(AwalanBPHG_PlusTahunBuku) + 1

        AwalanBPHPPN = "BPHPPN-"
        AwalanBPHPPN_PlusTahunBuku = AwalanBPHPPN & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHPPN_PlusTahunBuku = Len(AwalanBPHPPN_PlusTahunBuku)
        PanjangTeks_AwalanBPHPPN_PlusTahunBuku_Plus1 = Len(AwalanBPHPPN_PlusTahunBuku) + 1

        AwalanBPHP21 = "BPHP21-"
        AwalanBPHP21_PlusTahunBuku = AwalanBPHP21 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP21_PlusTahunBuku = Len(AwalanBPHP21_PlusTahunBuku)
        PanjangTeks_AwalanBPHP21_PlusTahunBuku_Plus1 = Len(AwalanBPHP21_PlusTahunBuku) + 1

        AwalanBPHP23 = "BPHP23-"
        AwalanBPHP23_PlusTahunBuku = AwalanBPHP23 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP23_PlusTahunBuku = Len(AwalanBPHP23_PlusTahunBuku)
        PanjangTeks_AwalanBPHP23_PlusTahunBuku_Plus1 = Len(AwalanBPHP23_PlusTahunBuku) + 1

        AwalanBPHP25 = "BPHP25-"
        AwalanBPHP25_PlusTahunBuku = AwalanBPHP25 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP25_PlusTahunBuku = Len(AwalanBPHP25_PlusTahunBuku)
        PanjangTeks_AwalanBPHP25_PlusTahunBuku_Plus1 = Len(AwalanBPHP25_PlusTahunBuku) + 1

        AwalanBPHP26 = "BPHP26-"
        AwalanBPHP26_PlusTahunBuku = AwalanBPHP26 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP26_PlusTahunBuku = Len(AwalanBPHP26_PlusTahunBuku)
        PanjangTeks_AwalanBPHP26_PlusTahunBuku_Plus1 = Len(AwalanBPHP26_PlusTahunBuku) + 1

        AwalanBPHP29_PlusTahunBuku = "BPHP29-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP29_PlusTahunBuku = Len(AwalanBPHP29_PlusTahunBuku)
        PanjangTeks_AwalanBPHP29_PlusTahunBuku_Plus1 = Len(AwalanBPHP29_PlusTahunBuku) + 1

        AwalanBPHP42 = "BPHP42-"
        AwalanBPHP42_PlusTahunBuku = AwalanBPHP42 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP42_PlusTahunBuku = Len(AwalanBPHP42_PlusTahunBuku)
        PanjangTeks_AwalanBPHP42_PlusTahunBuku_Plus1 = Len(AwalanBPHP42_PlusTahunBuku) + 1

        AwalanBPKP = "BPKP-"
        AwalanBPKP_PlusTahunBuku = AwalanBPKP & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPKP_PlusTahunBuku = Len(AwalanBPKP_PlusTahunBuku)
        PanjangTeks_AwalanBPKP_PlusTahunBuku_Plus1 = Len(AwalanBPKP_PlusTahunBuku) + 1

        AwalanNPPHP_PlusTahunBuku = "NPPHP-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPHP_PlusTahunBuku = Len(AwalanNPPHP_PlusTahunBuku)
        PanjangTeks_AwalanNPPHP_PlusTahunBuku_Plus1 = Len(AwalanNPPHP_PlusTahunBuku) + 1

        AwalanNRBHP_PlusTahunBuku = "NRBHP-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBHP_PlusTahunBuku = Len(AwalanNRBHP_PlusTahunBuku)
        PanjangTeks_AwalanNRBHP_PlusTahunBuku_Plus1 = Len(AwalanNRBHP_PlusTahunBuku) + 1

        AwalanNPPG_PlusTahunBuku = "NPPG-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPG_PlusTahunBuku = Len(AwalanNPPG_PlusTahunBuku)
        PanjangTeks_AwalanNPPG_PlusTahunBuku_Plus1 = Len(AwalanNPPG_PlusTahunBuku) + 1

        AwalanNRBG_PlusTahunBuku = "NRBG-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBG_PlusTahunBuku = Len(AwalanNRBG_PlusTahunBuku)
        PanjangTeks_AwalanNRBG_PlusTahunBuku_Plus1 = Len(AwalanNRBG_PlusTahunBuku) + 1

        AwalanNPPHKS_PlusTahunBuku = "NPPHKS-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPHKS_PlusTahunBuku = Len(AwalanNPPHKS_PlusTahunBuku)
        PanjangTeks_AwalanNPPHKS_PlusTahunBuku_Plus1 = Len(AwalanNPPHKS_PlusTahunBuku) + 1

        AwalanNRBHKS_PlusTahunBuku = "NRBHKS-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBHKS_PlusTahunBuku = Len(AwalanNRBHKS_PlusTahunBuku)
        PanjangTeks_AwalanNRBHKS_PlusTahunBuku_Plus1 = Len(AwalanNRBHKS_PlusTahunBuku) + 1

        AwalanNPPHTK_PlusTahunBuku = "NPPHTK-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPHTK_PlusTahunBuku = Len(AwalanNPPHTK_PlusTahunBuku)
        PanjangTeks_AwalanNPPHTK_PlusTahunBuku_Plus1 = Len(AwalanNPPHTK_PlusTahunBuku) + 1

        AwalanNRBHTK_PlusTahunBuku = "NRBHTK-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBHTK_PlusTahunBuku = Len(AwalanNRBHTK_PlusTahunBuku)
        PanjangTeks_AwalanNRBHTK_PlusTahunBuku_Plus1 = Len(AwalanNRBHTK_PlusTahunBuku) + 1

        AwalanBPPU = "BPPU-"
        PanjangTeks_AwalanBPPU = Len(AwalanBPPU)
        PanjangTeks_AwalanBPPU_Plus1 = Len(AwalanBPPU) + 1

        AwalanBPPU_PlusTahunBuku = AwalanBPPU & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPU_PlusTahunBuku = Len(AwalanBPPU_PlusTahunBuku)
        PanjangTeks_AwalanBPPU_PlusTahunBuku_Plus1 = Len(AwalanBPPU_PlusTahunBuku) + 1

        AwalanBPPD = "BPPD-"
        PanjangTeks_AwalanBPPD = Len(AwalanBPPD)
        PanjangTeks_AwalanBPPD_Plus1 = Len(AwalanBPPD) + 1

        AwalanBPPD_PlusTahunBuku = AwalanBPPD & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPD_PlusTahunBuku = Len(AwalanBPPD_PlusTahunBuku)
        PanjangTeks_AwalanBPPD_PlusTahunBuku_Plus1 = Len(AwalanBPPD_PlusTahunBuku) + 1

        AwalanNPC_PlusTahunBuku = "NPC-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPC_PlusTahunBuku = Len(AwalanNPC_PlusTahunBuku)
        PanjangTeks_AwalanNPC_PlusTahunBuku_Plus1 = Len(AwalanNPC_PlusTahunBuku) + 1

        AwalanBPPB_PlusTahunBuku = "BPPB-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPB_PlusTahunBuku = Len(AwalanBPPB_PlusTahunBuku)
        PanjangTeks_AwalanBPPB_PlusTahunBuku_Plus1 = Len(AwalanBPPB_PlusTahunBuku) + 1

        AwalanKM = "KM-"
        PanjangTeks_AwalanKM = Len(AwalanKM)
        PanjangTeks_AwalanKM_Plus1 = Len(AwalanKM) + 1

        AwalanKM_PlusTahunBuku = AwalanKM & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanKM_PlusTahunBuku = Len(AwalanKM_PlusTahunBuku)
        PanjangTeks_AwalanKM_PlusTahunBuku_Plus1 = Len(AwalanKM_PlusTahunBuku) + 1

        AwalanKK = "KK-"
        PanjangTeks_AwalanKK = Len(AwalanKK)
        PanjangTeks_AwalanKK_Plus1 = Len(AwalanKK) + 1

        AwalanKK_PlusTahunBuku = AwalanKK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanKK_PlusTahunBuku = Len(AwalanKK_PlusTahunBuku)
        PanjangTeks_AwalanKK_PlusTahunBuku_Plus1 = Len(AwalanKK_PlusTahunBuku) + 1

        AwalanBundelKK = "BKK-"
        PanjangTeks_AwalanBundelKK = Len(AwalanBundelKK)
        PanjangTeks_AwalanBundelKK_Plus1 = Len(AwalanBundelKK) + 1

        AwalanBundelKK_PlusTahunBuku = AwalanBundelKK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBundelKK_PlusTahunBuku = Len(AwalanBundelKK_PlusTahunBuku)
        PanjangTeks_AwalanBundelKK_PlusTahunBuku_Plus1 = Len(AwalanBundelKK_PlusTahunBuku) + 1

        AwalanBPBG = "BPBG-"
        PanjangTeks_AwalanBPBG = Len(AwalanBPBG)
        PanjangTeks_AwalanBPBG_Plus1 = Len(AwalanBPBG) + 1

        AwalanBPBG_PlusTahunBuku = AwalanBPBG & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPBG_PlusTahunBuku = Len(AwalanBPBG_PlusTahunBuku)
        PanjangTeks_AwalanBPBG_PlusTahunBuku_Plus1 = Len(AwalanBPBG_PlusTahunBuku) + 1

        AwalanBPDO = "BPDO-"
        PanjangTeks_AwalanBPDO = Len(AwalanBPDO)
        PanjangTeks_AwalanBPDO_Plus1 = Len(AwalanBPDO) + 1

        AwalanBPDO_PlusTahunBuku = AwalanBPDO & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPDO_PlusTahunBuku = Len(AwalanBPDO_PlusTahunBuku)
        PanjangTeks_AwalanBPDO_PlusTahunBuku_Plus1 = Len(AwalanBPDO_PlusTahunBuku) + 1

        AwalanBPAL = "BPAL-"
        PanjangTeks_AwalanBPAL = Len(AwalanBPAL)
        PanjangTeks_AwalanBPAL_Plus1 = Len(AwalanBPAL) + 1

        AwalanBPAL_PlusTahunBuku = AwalanBPAL & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPAL_PlusTahunBuku = Len(AwalanBPAL_PlusTahunBuku)
        PanjangTeks_AwalanBPAL_PlusTahunBuku_Plus1 = Len(AwalanBPAL_PlusTahunBuku) + 1

        'Logika Tahun Kabisat untuk Tahun Buku Aktif :
        If TahunBukuAktif Mod 4 = 0 Then
            TahunKabisat_TahunBukuAktif = True
        Else
            TahunKabisat_TahunBukuAktif = False
        End If

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabaseGeneral = True Then ProsesGantiTahun = True
        If StatusKoneksiDatabaseGeneral = False Then ProsesGantiTahun = False

        'Lihat Informasi Tahun Buku Terakhir Dibuka :
        If ProsesGantiTahun = True Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_SC WHERE ID = 1 ", KoneksiDatabaseGeneral)
            Try
                dr = cmd.ExecuteReader
                dr.Read()
                TahunBukuTerakhirDibuka = dr.Item("Tahun_Buku_Terakhir_Dibuka")
                ProsesGantiTahun = True
            Catch ex As Exception
                ProsesGantiTahun = False
            End Try
            AksesDatabase_General(Tutup)
        End If

        'Tentukan Jenis Fitur Tahun Buku :
        If ProsesGantiTahun = True Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData WHERE Tahun_Buku = '" & TahunBukuBaru & "' ", KoneksiDatabaseGeneral)
            Try
                dr = cmd.ExecuteReader
                dr.Read()
                JenisTahunBuku = dr.Item("Jenis_Tahun_Buku")
                StatusBuku = dr.Item("Status_Buku")
                ProsesGantiTahun = True
            Catch ex As Exception
                ProsesGantiTahun = False
            End Try
            AksesDatabase_General(Tutup)
        End If

        'Update Saldo Awal COA :
        If ProsesGantiTahun Then
            If TahunBukuAktif = TahunBukuTerakhirDibuka Then
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData WHERE Tahun_Buku = '" & TahunBukuAktif & " ' ", KoneksiDatabaseGeneral)
                dr = cmd.ExecuteReader
                dr.Read()
                If AmbilAngka(dr.Item("Trial_Balance")) = 1 Then
                    StatusTrialBalance = True
                Else
                    StatusTrialBalance = False
                End If
                AksesDatabase_General(Tutup)
            Else
                Dim cmdUPDATE As OdbcCommand
                Dim COA As String
                Dim Saldo As Decimal
                AksesDatabase_Transaksi(Buka)
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA ", KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                Do While dr.Read
                    COA = dr.Item("COA")
                    Saldo = dr.Item("Saldo_Awal")
                    cmdUPDATE = New OdbcCommand(" UPDATE tbl_COA SET Saldo_Awal = '" & Saldo & "' WHERE COA = '" & COA & "'", KoneksiDatabaseGeneral)
                    Try
                        cmdUPDATE.ExecuteNonQuery()
                        ProsesGantiTahun = True
                    Catch ex As Exception
                        ProsesGantiTahun = False
                        Exit Do
                    End Try
                Loop
                AksesDatabase_General(Tutup)
                AksesDatabase_Transaksi(Tutup)
            End If
        End If

        'Catat Informasi Tahun Buku Terakhir Dibuka :
        AksesDatabase_General(Buka)
        If ProsesGantiTahun = True Then
            cmd = New OdbcCommand(" UPDATE tbl_SC SET Tahun_Buku_Terakhir_Dibuka = '" & TahunBukuAktif & "' ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesGantiTahun = True
            Catch ex As Exception
                ProsesGantiTahun = False
            End Try
        Else
            cmd = New OdbcCommand(" UPDATE tbl_SC SET Tahun_Buku_Terakhir_Dibuka = '0' ", KoneksiDatabaseGeneral)
            cmd.ExecuteNonQuery()
        End If
        AksesDatabase_General(Tutup)

        KeluarDariSemuaModul()
        JudulAplikasi = NamaAplikasi & " - " & NamaPerusahaan & " - Tahun Buku " & TahunBukuAktif
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then JudulAplikasi = JudulAplikasi & " (Lampau)"
        win_BOOKU.Title = JudulAplikasi

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            win_BOOKU.mnu_BukuPengawasan.IsEnabled = False
            win_BOOKU.mnu_Pembelian.IsEnabled = False
            win_BOOKU.mnu_Penjualan.IsEnabled = False
            win_BOOKU.mnu_Transaksi.IsEnabled = False
            win_BOOKU.mnu_StockOpname.IsEnabled = False
            win_BOOKU.mnu_Akuntansi.IsEnabled = False
            win_BOOKU.mnu_ManajemenAsset.IsEnabled = False
            win_BOOKU.mnu_Pengajuan.IsEnabled = False
            win_BOOKU.mnu_Pajak.IsEnabled = False
            win_BOOKU.mnu_DataAwal.Visibility = Visibility.Visible
        End If
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            win_BOOKU.mnu_BukuPengawasan.IsEnabled = True
            win_BOOKU.mnu_Pembelian.IsEnabled = True
            win_BOOKU.mnu_Penjualan.IsEnabled = True
            win_BOOKU.mnu_Transaksi.IsEnabled = True
            win_BOOKU.mnu_StockOpname.IsEnabled = True
            win_BOOKU.mnu_Akuntansi.IsEnabled = True
            win_BOOKU.mnu_ManajemenAsset.IsEnabled = True
            win_BOOKU.mnu_Pengajuan.IsEnabled = True
            win_BOOKU.mnu_Pajak.IsEnabled = True
            win_BOOKU.mnu_DataAwal.Visibility = Visibility.Collapsed
        End If


        If ProsesGantiTahun = True Then
            If FungsiForm <> FungsiForm_EksekusiSub_PROSESGANTITAHUNBUKU Then Pesan_Sukses("Anda memasuki Tahun Buku " & TahunBukuAktif & ".")
            Notifikasi_AwalMasukTahunBuku()
            UpdateInfoBulanBukuAktif()
            UpdateDataKursAkhirBulan()
            TesKoneksiDatabaseTransaksiYangBaru()
        Else
            Pesan_Gagal("Mohon maaf, Anda dikeluarkan dari LOGIN karena ada kesalahan teknis." _
                   & Enter2Baris & teks_SilakanCobaLagi_Database)
            LoginGagal()
            Return
        End If

    End Sub


    Private Sub btn_Ganti_Click(sender As Object, e As RoutedEventArgs) Handles btn_Ganti.Click
        TahunBukuBaru = AmbilAngka(cmb_TahunBuku.SelectedValue)
        ProsesGantiTahunBuku()
        Me.Close()
    End Sub


    Sub TesKoneksiDatabaseTransaksiYangBaru()
        AksesDatabase_Transaksi(Buka)
        AksesDatabase_Transaksi(Tutup)
        BukaDatabaseTransaksi_Kondisional()
        TutupDatabaseTransaksi_Kondisional()
        BukaDatabaseGeneral_Kondisional()
        TutupDatabaseGeneral_Kondisional()
        TahunBuku_Alternatif = TahunBukuAktif
        BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
        TutupDatabaseTransaksi_Alternatif()
        BukaDatabaseTransaksi_Alternatif_Kondisional(TahunBuku_Alternatif)
        TutupDatabaseTransaksi_Alternatif_Kondisional()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
