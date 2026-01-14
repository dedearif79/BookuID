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
        PanjangTeks_AwalanNomorJV = AwalanNomorJV.Length
        PanjangTeks_AwalanNomorJV_Plus1 = AwalanNomorJV.Length + 1

        AwalanPO = "PO-"
        PanjangTeks_AwalanPO = AwalanPO.Length
        PanjangTeks_AwalanPO_Plus1 = AwalanPO.Length + 1

        AwalanSJ = "SJ-"
        PanjangTeks_AwalanSJ = AwalanSJ.Length
        PanjangTeks_AwalanSJ_Plus1 = AwalanSJ.Length + 1

        AwalanBAST = "BAST-"
        PanjangTeks_AwalanBAST = AwalanBAST.Length
        PanjangTeks_AwalanBAST_Plus1 = AwalanBAST.Length + 1

        AwalanPEMB = "PEMB-"
        PanjangTeks_AwalanPEMB = AwalanPEMB.Length
        PanjangTeks_AwalanPEMB_Plus1 = AwalanPEMB.Length + 1

        AwalanPEMB_PlusTahunBuku = AwalanPEMB & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanPEMB_PlusTahunBuku = AwalanPEMB_PlusTahunBuku.Length
        PanjangTeks_AwalanPEMB_PlusTahunBuku_Plus1 = AwalanPEMB_PlusTahunBuku.Length + 1

        AwalanPENJ = "PENJ-"
        PanjangTeks_AwalanPENJ = AwalanPENJ.Length
        PanjangTeks_AwalanPENJ_Plus1 = AwalanPENJ.Length + 1

        AwalanPENJ_PlusTahunBuku = AwalanPENJ & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanPENJ_PlusTahunBuku = AwalanPENJ_PlusTahunBuku.Length
        PanjangTeks_AwalanPENJ_PlusTahunBuku_Plus1 = AwalanPENJ_PlusTahunBuku.Length + 1

        AwalanNR = "NR-"
        PanjangTeks_AwalanNR = AwalanNR.Length
        PanjangTeks_AwalanNR_Plus1 = AwalanNR.Length + 1

        AwalanINV = "INV-"
        PanjangTeks_AwalanINV = AwalanINV.Length
        PanjangTeks_AwalanINV_Plus1 = AwalanINV.Length + 1

        AwalanBPHU = "BPHU-"
        PanjangTeks_AwalanBPHU = AwalanBPHU.Length
        PanjangTeks_AwalanBPHU_Plus1 = AwalanBPHU.Length + 1

        AwalanBPHU_PlusTahunBuku = AwalanBPHU & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHU_PlusTahunBuku = AwalanBPHU_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHU_PlusTahunBuku_Plus1 = AwalanBPHU_PlusTahunBuku.Length + 1

        AwalanBPHD = "BPHD-"
        PanjangTeks_AwalanBPHD = AwalanBPHD.Length
        PanjangTeks_AwalanBPHD_Plus1 = AwalanBPHD.Length + 1

        AwalanBPHD_PlusTahunBuku = AwalanBPHD & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHD_PlusTahunBuku = AwalanBPHD_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHD_PlusTahunBuku_Plus1 = AwalanBPHD_PlusTahunBuku.Length + 1

        AwalanBPHKS = "BPHKS-"
        PanjangTeks_AwalanBPHKS = AwalanBPHKS.Length
        PanjangTeks_AwalanBPHKS_Plus1 = AwalanBPHKS.Length + 1

        AwalanBPHKS_PlusTahunBuku = AwalanBPHKS & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHKS_PlusTahunBuku = AwalanBPHKS_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHKS_PlusTahunBuku_Plus1 = AwalanBPHKS_PlusTahunBuku.Length + 1

        AwalanBPHTK = "BPHTK-"
        PanjangTeks_AwalanBPHTK = AwalanBPHTK.Length
        PanjangTeks_AwalanBPHTK_Plus1 = AwalanBPHTK.Length + 1

        AwalanBPHTK_PlusTahunBuku = AwalanBPHTK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHTK_PlusTahunBuku = AwalanBPHTK_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHTK_PlusTahunBuku_Plus1 = AwalanBPHTK_PlusTahunBuku.Length + 1

        AwalanBPHKK = "BPHKK-"
        PanjangTeks_AwalanBPHKK = AwalanBPHKK.Length
        PanjangTeks_AwalanBPHKK_Plus1 = AwalanBPHKK.Length + 1

        AwalanBPHKK_PlusTahunBuku = AwalanBPHKK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHKK_PlusTahunBuku = AwalanBPHKK_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHKK_PlusTahunBuku_Plus1 = AwalanBPHKK_PlusTahunBuku.Length + 1

        AwalanBPHS = "BPHS-"
        PanjangTeks_AwalanBPHS = AwalanBPHS.Length
        PanjangTeks_AwalanBPHS_Plus1 = AwalanBPHS.Length + 1

        AwalanBPHS_PlusTahunBuku = AwalanBPHS & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHS_PlusTahunBuku = AwalanBPHS_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHS_PlusTahunBuku_Plus1 = AwalanBPHS_PlusTahunBuku.Length + 1

        AwalanBPHB = "BPHB-"
        PanjangTeks_AwalanBPHB = AwalanBPHB.Length
        PanjangTeks_AwalanBPHB_Plus1 = AwalanBPHB.Length + 1

        AwalanBPHB_PlusTahunBuku = AwalanBPHB & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHB_PlusTahunBuku = AwalanBPHB_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHB_PlusTahunBuku_Plus1 = AwalanBPHB_PlusTahunBuku.Length + 1

        AwalanBPHL = "BPHL-"
        PanjangTeks_AwalanBPHL = AwalanBPHL.Length
        PanjangTeks_AwalanBPHL_Plus1 = AwalanBPHL.Length + 1

        AwalanBPHL_PlusTahunBuku = AwalanBPHL & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHL_PlusTahunBuku = AwalanBPHL_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHL_PlusTahunBuku_Plus1 = AwalanBPHL_PlusTahunBuku.Length + 1

        AwalanBPHPK = "BPHPK-"
        PanjangTeks_AwalanBPHPK = AwalanBPHPK.Length
        PanjangTeks_AwalanBPHPK_Plus1 = AwalanBPHPK.Length + 1

        AwalanBPHPK_PlusTahunBuku = AwalanBPHPK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHPK_PlusTahunBuku = AwalanBPHPK_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHPK_PlusTahunBuku_Plus1 = AwalanBPHPK_PlusTahunBuku.Length + 1

        AwalanBPPPK = "BPPPK-"
        PanjangTeks_AwalanBPPPK = AwalanBPPPK.Length
        PanjangTeks_AwalanBPPPK_Plus1 = AwalanBPPPK.Length + 1

        AwalanBPPPK_PlusTahunBuku = AwalanBPPPK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPPK_PlusTahunBuku = AwalanBPPPK_PlusTahunBuku.Length
        PanjangTeks_AwalanBPPPK_PlusTahunBuku_Plus1 = AwalanBPPPK_PlusTahunBuku.Length + 1

        AwalanBPHA = "BPHA-"
        PanjangTeks_AwalanBPHA = AwalanBPHA.Length
        PanjangTeks_AwalanBPHA_Plus1 = AwalanBPHA.Length + 1

        AwalanBPHA_PlusTahunBuku = AwalanBPHA & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHA_PlusTahunBuku = AwalanBPHA_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHA_PlusTahunBuku_Plus1 = AwalanBPHA_PlusTahunBuku.Length + 1

        AwalanBPPA = "BPPA-"
        PanjangTeks_AwalanBPPA = AwalanBPPA.Length
        PanjangTeks_AwalanBPPA_Plus1 = AwalanBPPA.Length + 1

        AwalanBPPA_PlusTahunBuku = AwalanBPPA & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPA_PlusTahunBuku = AwalanBPPA_PlusTahunBuku.Length
        PanjangTeks_AwalanBPPA_PlusTahunBuku_Plus1 = AwalanBPPA_PlusTahunBuku.Length + 1

        AwalanBPHK = "BPHK-"
        PanjangTeks_AwalanBPHK = AwalanBPHK.Length
        PanjangTeks_AwalanBPHK_Plus1 = AwalanBPHK.Length + 1

        AwalanBPHK_PlusTahunBuku = AwalanBPHK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHK_PlusTahunBuku = AwalanBPHK_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHK_PlusTahunBuku_Plus1 = AwalanBPHK_PlusTahunBuku.Length + 1

        AwalanBPPK = "BPPK-"
        PanjangTeks_AwalanBPPK = AwalanBPPK.Length
        PanjangTeks_AwalanBPPK_Plus1 = AwalanBPPK.Length + 1

        AwalanBPPK_PlusTahunBuku = AwalanBPPK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPK_PlusTahunBuku = AwalanBPPK_PlusTahunBuku.Length
        PanjangTeks_AwalanBPPK_PlusTahunBuku_Plus1 = AwalanBPPK_PlusTahunBuku.Length + 1

        AwalanBPM = "BPM-"
        PanjangTeks_AwalanBPM = AwalanBPM.Length
        PanjangTeks_AwalanBPM_Plus1 = AwalanBPM.Length + 1

        AwalanBPM_PlusTahunBuku = AwalanBPM & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPM_PlusTahunBuku = AwalanBPM_PlusTahunBuku.Length
        PanjangTeks_AwalanBPM_PlusTahunBuku_Plus1 = AwalanBPM_PlusTahunBuku.Length + 1

        AwalanBPHPS = "BPHPS-"
        PanjangTeks_AwalanBPHPS = AwalanBPHPS.Length
        PanjangTeks_AwalanBPHPS_Plus1 = AwalanBPHPS.Length + 1

        AwalanBPHPS_PlusTahunBuku = AwalanBPHPS & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHPS_PlusTahunBuku = AwalanBPHPS_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHPS_PlusTahunBuku_Plus1 = AwalanBPHPS_PlusTahunBuku.Length + 1

        AwalanBPPPS = "BPPPS-"
        PanjangTeks_AwalanBPPPS = AwalanBPPPS.Length
        PanjangTeks_AwalanBPPPS_Plus1 = AwalanBPPPS.Length + 1

        AwalanBPPPS_PlusTahunBuku = AwalanBPPPS & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPPS_PlusTahunBuku = AwalanBPPPS_PlusTahunBuku.Length
        PanjangTeks_AwalanBPPPS_PlusTahunBuku_Plus1 = AwalanBPPPS_PlusTahunBuku.Length + 1

        AwalanNPPHU_PlusTahunBuku = "NPPHU-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPHU_PlusTahunBuku = AwalanNPPHU_PlusTahunBuku.Length
        PanjangTeks_AwalanNPPHU_PlusTahunBuku_Plus1 = AwalanNPPHU_PlusTahunBuku.Length + 1

        AwalanNRBHU_PlusTahunBuku = "NRBHU-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBHU_PlusTahunBuku = AwalanNRBHU_PlusTahunBuku.Length
        PanjangTeks_AwalanNRBHU_PlusTahunBuku_Plus1 = AwalanNRBHU_PlusTahunBuku.Length + 1

        AwalanBPHG = "BPHG-"
        AwalanBPHG_PlusTahunBuku = AwalanBPHG & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHG_PlusTahunBuku = AwalanBPHG_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHG_PlusTahunBuku_Plus1 = AwalanBPHG_PlusTahunBuku.Length + 1

        AwalanBPHPPN = "BPHPPN-"
        AwalanBPHPPN_PlusTahunBuku = AwalanBPHPPN & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHPPN_PlusTahunBuku = AwalanBPHPPN_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHPPN_PlusTahunBuku_Plus1 = AwalanBPHPPN_PlusTahunBuku.Length + 1

        AwalanBPHP21 = "BPHP21-"
        AwalanBPHP21_PlusTahunBuku = AwalanBPHP21 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP21_PlusTahunBuku = AwalanBPHP21_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHP21_PlusTahunBuku_Plus1 = AwalanBPHP21_PlusTahunBuku.Length + 1

        AwalanBPHP23 = "BPHP23-"
        AwalanBPHP23_PlusTahunBuku = AwalanBPHP23 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP23_PlusTahunBuku = AwalanBPHP23_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHP23_PlusTahunBuku_Plus1 = AwalanBPHP23_PlusTahunBuku.Length + 1

        AwalanBPHP25 = "BPHP25-"
        AwalanBPHP25_PlusTahunBuku = AwalanBPHP25 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP25_PlusTahunBuku = AwalanBPHP25_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHP25_PlusTahunBuku_Plus1 = AwalanBPHP25_PlusTahunBuku.Length + 1

        AwalanBPHP26 = "BPHP26-"
        AwalanBPHP26_PlusTahunBuku = AwalanBPHP26 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP26_PlusTahunBuku = AwalanBPHP26_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHP26_PlusTahunBuku_Plus1 = AwalanBPHP26_PlusTahunBuku.Length + 1

        AwalanBPHP29_PlusTahunBuku = "BPHP29-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP29_PlusTahunBuku = AwalanBPHP29_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHP29_PlusTahunBuku_Plus1 = AwalanBPHP29_PlusTahunBuku.Length + 1

        AwalanBPHP42 = "BPHP42-"
        AwalanBPHP42_PlusTahunBuku = AwalanBPHP42 & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPHP42_PlusTahunBuku = AwalanBPHP42_PlusTahunBuku.Length
        PanjangTeks_AwalanBPHP42_PlusTahunBuku_Plus1 = AwalanBPHP42_PlusTahunBuku.Length + 1

        AwalanBPKP = "BPKP-"
        AwalanBPKP_PlusTahunBuku = AwalanBPKP & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPKP_PlusTahunBuku = AwalanBPKP_PlusTahunBuku.Length
        PanjangTeks_AwalanBPKP_PlusTahunBuku_Plus1 = AwalanBPKP_PlusTahunBuku.Length + 1

        AwalanNPPHP_PlusTahunBuku = "NPPHP-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPHP_PlusTahunBuku = AwalanNPPHP_PlusTahunBuku.Length
        PanjangTeks_AwalanNPPHP_PlusTahunBuku_Plus1 = AwalanNPPHP_PlusTahunBuku.Length + 1

        AwalanNRBHP_PlusTahunBuku = "NRBHP-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBHP_PlusTahunBuku = AwalanNRBHP_PlusTahunBuku.Length
        PanjangTeks_AwalanNRBHP_PlusTahunBuku_Plus1 = AwalanNRBHP_PlusTahunBuku.Length + 1

        AwalanNPPG_PlusTahunBuku = "NPPG-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPG_PlusTahunBuku = AwalanNPPG_PlusTahunBuku.Length
        PanjangTeks_AwalanNPPG_PlusTahunBuku_Plus1 = AwalanNPPG_PlusTahunBuku.Length + 1

        AwalanNRBG_PlusTahunBuku = "NRBG-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBG_PlusTahunBuku = AwalanNRBG_PlusTahunBuku.Length
        PanjangTeks_AwalanNRBG_PlusTahunBuku_Plus1 = AwalanNRBG_PlusTahunBuku.Length + 1

        AwalanNPPHKS_PlusTahunBuku = "NPPHKS-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPHKS_PlusTahunBuku = AwalanNPPHKS_PlusTahunBuku.Length
        PanjangTeks_AwalanNPPHKS_PlusTahunBuku_Plus1 = AwalanNPPHKS_PlusTahunBuku.Length + 1

        AwalanNRBHKS_PlusTahunBuku = "NRBHKS-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBHKS_PlusTahunBuku = AwalanNRBHKS_PlusTahunBuku.Length
        PanjangTeks_AwalanNRBHKS_PlusTahunBuku_Plus1 = AwalanNRBHKS_PlusTahunBuku.Length + 1

        AwalanNPPHTK_PlusTahunBuku = "NPPHTK-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPPHTK_PlusTahunBuku = AwalanNPPHTK_PlusTahunBuku.Length
        PanjangTeks_AwalanNPPHTK_PlusTahunBuku_Plus1 = AwalanNPPHTK_PlusTahunBuku.Length + 1

        AwalanNRBHTK_PlusTahunBuku = "NRBHTK-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNRBHTK_PlusTahunBuku = AwalanNRBHTK_PlusTahunBuku.Length
        PanjangTeks_AwalanNRBHTK_PlusTahunBuku_Plus1 = AwalanNRBHTK_PlusTahunBuku.Length + 1

        AwalanBPPU = "BPPU-"
        PanjangTeks_AwalanBPPU = AwalanBPPU.Length
        PanjangTeks_AwalanBPPU_Plus1 = AwalanBPPU.Length + 1

        AwalanBPPU_PlusTahunBuku = AwalanBPPU & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPU_PlusTahunBuku = AwalanBPPU_PlusTahunBuku.Length
        PanjangTeks_AwalanBPPU_PlusTahunBuku_Plus1 = AwalanBPPU_PlusTahunBuku.Length + 1

        AwalanBPPD = "BPPD-"
        PanjangTeks_AwalanBPPD = AwalanBPPD.Length
        PanjangTeks_AwalanBPPD_Plus1 = AwalanBPPD.Length + 1

        AwalanBPPD_PlusTahunBuku = AwalanBPPD & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPD_PlusTahunBuku = AwalanBPPD_PlusTahunBuku.Length
        PanjangTeks_AwalanBPPD_PlusTahunBuku_Plus1 = AwalanBPPD_PlusTahunBuku.Length + 1

        AwalanNPC_PlusTahunBuku = "NPC-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanNPC_PlusTahunBuku = AwalanNPC_PlusTahunBuku.Length
        PanjangTeks_AwalanNPC_PlusTahunBuku_Plus1 = AwalanNPC_PlusTahunBuku.Length + 1

        AwalanBPPB_PlusTahunBuku = "BPPB-" & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPPB_PlusTahunBuku = AwalanBPPB_PlusTahunBuku.Length
        PanjangTeks_AwalanBPPB_PlusTahunBuku_Plus1 = AwalanBPPB_PlusTahunBuku.Length + 1

        AwalanKM = "KM-"
        PanjangTeks_AwalanKM = AwalanKM.Length
        PanjangTeks_AwalanKM_Plus1 = AwalanKM.Length + 1

        AwalanKM_PlusTahunBuku = AwalanKM & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanKM_PlusTahunBuku = AwalanKM_PlusTahunBuku.Length
        PanjangTeks_AwalanKM_PlusTahunBuku_Plus1 = AwalanKM_PlusTahunBuku.Length + 1

        AwalanKK = "KK-"
        PanjangTeks_AwalanKK = AwalanKK.Length
        PanjangTeks_AwalanKK_Plus1 = AwalanKK.Length + 1

        AwalanKK_PlusTahunBuku = AwalanKK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanKK_PlusTahunBuku = AwalanKK_PlusTahunBuku.Length
        PanjangTeks_AwalanKK_PlusTahunBuku_Plus1 = AwalanKK_PlusTahunBuku.Length + 1

        AwalanBundelKK = "BKK-"
        PanjangTeks_AwalanBundelKK = AwalanBundelKK.Length
        PanjangTeks_AwalanBundelKK_Plus1 = AwalanBundelKK.Length + 1

        AwalanBundelKK_PlusTahunBuku = AwalanBundelKK & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBundelKK_PlusTahunBuku = AwalanBundelKK_PlusTahunBuku.Length
        PanjangTeks_AwalanBundelKK_PlusTahunBuku_Plus1 = AwalanBundelKK_PlusTahunBuku.Length + 1

        AwalanBPBG = "BPBG-"
        PanjangTeks_AwalanBPBG = AwalanBPBG.Length
        PanjangTeks_AwalanBPBG_Plus1 = AwalanBPBG.Length + 1

        AwalanBPBG_PlusTahunBuku = AwalanBPBG & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPBG_PlusTahunBuku = AwalanBPBG_PlusTahunBuku.Length
        PanjangTeks_AwalanBPBG_PlusTahunBuku_Plus1 = AwalanBPBG_PlusTahunBuku.Length + 1

        AwalanBPDO = "BPDO-"
        PanjangTeks_AwalanBPDO = AwalanBPDO.Length
        PanjangTeks_AwalanBPDO_Plus1 = AwalanBPDO.Length + 1

        AwalanBPDO_PlusTahunBuku = AwalanBPDO & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPDO_PlusTahunBuku = AwalanBPDO_PlusTahunBuku.Length
        PanjangTeks_AwalanBPDO_PlusTahunBuku_Plus1 = AwalanBPDO_PlusTahunBuku.Length + 1

        AwalanBPAL = "BPAL-"
        PanjangTeks_AwalanBPAL = AwalanBPAL.Length
        PanjangTeks_AwalanBPAL_Plus1 = AwalanBPAL.Length + 1

        AwalanBPAL_PlusTahunBuku = AwalanBPAL & TahunBukuAktif.ToString & "-"
        PanjangTeks_AwalanBPAL_PlusTahunBuku = AwalanBPAL_PlusTahunBuku.Length
        PanjangTeks_AwalanBPAL_PlusTahunBuku_Plus1 = AwalanBPAL_PlusTahunBuku.Length + 1

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
