Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc
Imports Org.BouncyCastle.Math.EC

Public Class wpfWin_InputPenjualanEceran

    Public JudulForm
    Public FungsiForm
    Public NomorID
    Public NomorJV

    Dim TanggalTransaksi
    Public KodeToko
    Dim NamaToko
    Public COAKas

    Dim JumlahKas As Int64
    Dim JumlahBank_01 As Int64
    Dim JumlahBank_02 As Int64
    Dim JumlahBank_03 As Int64
    Dim JumlahBank_04 As Int64
    Dim JumlahBank_05 As Int64
    Dim JumlahBank_06 As Int64
    Dim JumlahBank_07 As Int64
    Dim JumlahBank_08 As Int64
    Dim JumlahBank_09 As Int64
    Dim JumlaheWallet_01 As Int64
    Dim JumlaheWallet_02 As Int64
    Dim JumlaheWallet_03 As Int64
    Dim JumlaheWallet_04 As Int64
    Dim JumlaheWallet_05 As Int64
    Dim JumlaheWallet_06 As Int64
    Dim JumlaheWallet_07 As Int64
    Dim JumlaheWallet_08 As Int64
    Dim JumlaheWallet_09 As Int64
    Dim JumlahTransaksi As Int64
    Dim JumlahDPP As Int64
    Dim JumlahPPN As Int64
    Dim Keterangan

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Penjualan Eceran"
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Penjualan Eceran"
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        LogikaTampilanKolom()

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        NomorID = 0
        NomorJV = 0
        KodeToko = Kosongan
        COAKas = Kosongan
        FungsiForm = Kosongan

        KosongkanDatePicker(dtp_TanggalTransaksi)
        txt_NamaToko.Text = Kosongan
        txt_JumlahKas.Text = Kosongan
        txt_JumlahBank_01.Text = Kosongan
        txt_JumlahBank_02.Text = Kosongan
        txt_JumlahBank_03.Text = Kosongan
        txt_JumlahBank_04.Text = Kosongan
        txt_JumlahBank_05.Text = Kosongan
        txt_JumlahBank_06.Text = Kosongan
        txt_JumlahBank_07.Text = Kosongan
        txt_JumlahBank_08.Text = Kosongan
        txt_JumlahBank_09.Text = Kosongan
        txt_JumlaheWallet_01.Text = Kosongan
        txt_JumlaheWallet_02.Text = Kosongan
        txt_JumlaheWallet_03.Text = Kosongan
        txt_JumlaheWallet_04.Text = Kosongan
        txt_JumlaheWallet_05.Text = Kosongan
        txt_JumlaheWallet_06.Text = Kosongan
        txt_JumlaheWallet_07.Text = Kosongan
        txt_JumlaheWallet_08.Text = Kosongan
        txt_JumlaheWallet_09.Text = Kosongan
        txt_JumlahTransaksi.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        ProsesResetForm = False

    End Sub


    Sub LogikaTampilanKolom()
        'Bank Eceran:
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_01, lbl_Bank_01, txt_JumlahBank_01)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_02, lbl_Bank_02, txt_JumlahBank_02)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_03, lbl_Bank_03, txt_JumlahBank_03)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_04, lbl_Bank_04, txt_JumlahBank_04)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_05, lbl_Bank_05, txt_JumlahBank_05)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_06, lbl_Bank_06, txt_JumlahBank_06)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_07, lbl_Bank_07, txt_JumlahBank_07)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_08, lbl_Bank_08, txt_JumlahBank_08)
        VisibilitasKolomCOA(KodeTautanCOA_BankEceran_09, lbl_Bank_09, txt_JumlahBank_09)
        'eWallet:
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_01, lbl_eWallet_01, txt_JumlaheWallet_01)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_02, lbl_eWallet_02, txt_JumlaheWallet_02)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_03, lbl_eWallet_03, txt_JumlaheWallet_03)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_04, lbl_eWallet_04, txt_JumlaheWallet_04)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_05, lbl_eWallet_05, txt_JumlaheWallet_05)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_06, lbl_eWallet_06, txt_JumlaheWallet_06)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_07, lbl_eWallet_07, txt_JumlaheWallet_07)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_08, lbl_eWallet_08, txt_JumlaheWallet_08)
        VisibilitasKolomCOA(KodeTautanCOA_eWallet_09, lbl_eWallet_09, txt_JumlaheWallet_09)
        lbl_KasEceran.Text = AmbilValue_NamaAkun(COAKas)
    End Sub

    Sub VisibilitasKolomCOA(COA As String, Label As TextBlock, Teks As TextBox)
        If VisibilitasCOA(COA) Then
            Label.Visibility = Visibility.Visible
            Teks.Visibility = Visibility.Visible
        Else
            Label.Visibility = Visibility.Collapsed
            Teks.Visibility = Visibility.Collapsed
        End If
        Label.Text = AmbilValue_NamaAkun(COA)
    End Sub


    Private Sub dtp_TanggalTransaksi_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalTransaksi.SelectedDateChanged
        If dtp_TanggalTransaksi.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalTransaksi)
            TanggalTransaksi = TanggalFormatTampilan(dtp_TanggalTransaksi.SelectedDate)
        End If
    End Sub


    Private Sub txt_NamaToko_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaToko.TextChanged
        NamaToko = txt_NamaToko.Text
    End Sub


    Private Sub txt_JumlahKas_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahKas.TextChanged
        JumlahKas = AmbilAngka(txt_JumlahKas.Text)
        Perhitungan()
    End Sub


    Private Sub txt_JumlahBank_01_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_01.TextChanged
        JumlahBank_01 = AmbilAngka(txt_JumlahBank_01.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahBank_02_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_02.TextChanged
        JumlahBank_02 = AmbilAngka(txt_JumlahBank_02.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahBank_03_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_03.TextChanged
        JumlahBank_03 = AmbilAngka(txt_JumlahBank_03.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahBank_04_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_04.TextChanged
        JumlahBank_04 = AmbilAngka(txt_JumlahBank_04.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahBank_05_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_05.TextChanged
        JumlahBank_05 = AmbilAngka(txt_JumlahBank_05.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahBank_06_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_06.TextChanged
        JumlahBank_06 = AmbilAngka(txt_JumlahBank_06.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahBank_07_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_07.TextChanged
        JumlahBank_07 = AmbilAngka(txt_JumlahBank_07.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahBank_08_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_08.TextChanged
        JumlahBank_08 = AmbilAngka(txt_JumlahBank_08.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlahBank_09_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank_09.TextChanged
        JumlahBank_09 = AmbilAngka(txt_JumlahBank_09.Text)
        Perhitungan()
    End Sub


    Private Sub txt_JumlaheWallet_01_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_01.TextChanged
        JumlaheWallet_01 = AmbilAngka(txt_JumlaheWallet_01.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlaheWallet_02_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_02.TextChanged
        JumlaheWallet_02 = AmbilAngka(txt_JumlaheWallet_02.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlaheWallet_03_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_03.TextChanged
        JumlaheWallet_03 = AmbilAngka(txt_JumlaheWallet_03.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlaheWallet_04_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_04.TextChanged
        JumlaheWallet_04 = AmbilAngka(txt_JumlaheWallet_04.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlaheWallet_05_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_05.TextChanged
        JumlaheWallet_05 = AmbilAngka(txt_JumlaheWallet_05.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlaheWallet_06_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_06.TextChanged
        JumlaheWallet_06 = AmbilAngka(txt_JumlaheWallet_06.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlaheWallet_07_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_07.TextChanged
        JumlaheWallet_07 = AmbilAngka(txt_JumlaheWallet_07.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlaheWallet_08_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_08.TextChanged
        JumlaheWallet_08 = AmbilAngka(txt_JumlaheWallet_08.Text)
        Perhitungan()
    End Sub
    Private Sub txt_JumlaheWallet_09_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlaheWallet_09.TextChanged
        JumlaheWallet_09 = AmbilAngka(txt_JumlaheWallet_09.Text)
        Perhitungan()
    End Sub


    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
    End Sub


    Sub Perhitungan()
        Dim TarifPPN = AmbilValue_TarifPPNBerdasarkanTanggal(TanggalTransaksi)
        JumlahTransaksi = JumlahKas + JumlahBank() + JumlaheWallet()
        JumlahPPN = HitungPPNInclude(JumlahTransaksi, TarifPPN)
        JumlahDPP = JumlahTransaksi - JumlahPPN
        txt_JumlahTransaksi.Text = JumlahTransaksi
    End Sub

    Function JumlahBank() As Int64
        Dim Jumlah As Int64
        Jumlah = 0 _
            + JumlahBank_01 _
            + JumlahBank_02 _
            + JumlahBank_03 _
            + JumlahBank_04 _
            + JumlahBank_05 _
            + JumlahBank_06 _
            + JumlahBank_07 _
            + JumlahBank_08 _
            + JumlahBank_09
        Return Jumlah
    End Function

    Function JumlaheWallet() As Int64
        Dim Jumlah As Int64
        Jumlah = 0 _
            + JumlaheWallet_01 _
            + JumlaheWallet_02 _
            + JumlaheWallet_03 _
            + JumlaheWallet_04 _
            + JumlaheWallet_05 _
            + JumlaheWallet_06 _
            + JumlaheWallet_07 _
            + JumlaheWallet_08 _
            + JumlaheWallet_09
        Return Jumlah
    End Function


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub




    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'PesanUntukProgrammer(
        '    "Jumlah Kas : " & JumlahKas & Enter2Baris &
        '    "Jumlah Bank : " & JumlahBank() & Enter2Baris &
        '    "Jumlah eWallet : " & JumlaheWallet() & Enter2Baris &
        '    "Jumlah Transaksi : " & JumlahTransaksi & Enter2Baris &
        '    "Jumlah DPP : " & JumlahDPP & Enter2Baris &
        '    "Jumlah PPN : " & JumlahPPN & Enter2Baris &
        '    "")

        If dtp_TanggalTransaksi.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalTransaksi, "Tanggal Transaksi")
            Return
        End If

        If JumlahTransaksi = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_JumlahKas, "Jumlah Kas atau Jumlah Bank")
            Return
        End If

        SimpanJurnal()

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            usc_BukuPenjualanEceran_RekapHarian.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataGagalDiperbarui()
        End If

    End Sub

    Sub SimpanJurnal()

        ResetValueJurnal()

        Select Case FungsiForm
            Case FungsiForm_TAMBAH
                SistemPenomoranOtomatis_NomorJV()
                NomorJV = jur_NomorJV
            Case FungsiForm_EDIT
                HapusJurnal_BerdasarkanNomorJV(NomorJV)
                jur_NomorJV = NomorJV
        End Select

        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
        jur_JenisJurnal = JenisJurnal_PenjualanEceran
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NamaProduk = teks_Eceran
        jur_KodeLawanTransaksi = KodeToko 'Hati-hati merubah ini...! Ada masuk ke logika
        jur_NamaLawanTransaksi = NamaToko 'Hati-hati merubah ini...! Ada masuk ke logika
        jur_UraianTransaksi = Keterangan
        jur_Direct = 0

        'Simpan Jurnal :
        ___jurDebet(COAKas, JumlahKas)
        ___jurDebet(KodeTautanCOA_BankEceran_01, JumlahBank_01)
        ___jurDebet(KodeTautanCOA_BankEceran_02, JumlahBank_02)
        ___jurDebet(KodeTautanCOA_BankEceran_03, JumlahBank_03)
        ___jurDebet(KodeTautanCOA_BankEceran_04, JumlahBank_04)
        ___jurDebet(KodeTautanCOA_BankEceran_05, JumlahBank_05)
        ___jurDebet(KodeTautanCOA_BankEceran_06, JumlahBank_06)
        ___jurDebet(KodeTautanCOA_BankEceran_07, JumlahBank_07)
        ___jurDebet(KodeTautanCOA_BankEceran_08, JumlahBank_08)
        ___jurDebet(KodeTautanCOA_BankEceran_09, JumlahBank_09)
        ___jurDebet(KodeTautanCOA_eWallet_01, JumlaheWallet_01)
        ___jurDebet(KodeTautanCOA_eWallet_02, JumlaheWallet_02)
        ___jurDebet(KodeTautanCOA_eWallet_03, JumlaheWallet_03)
        ___jurDebet(KodeTautanCOA_eWallet_04, JumlaheWallet_04)
        ___jurDebet(KodeTautanCOA_eWallet_05, JumlaheWallet_05)
        ___jurDebet(KodeTautanCOA_eWallet_06, JumlaheWallet_06)
        ___jurDebet(KodeTautanCOA_eWallet_07, JumlaheWallet_07)
        ___jurDebet(KodeTautanCOA_eWallet_08, JumlaheWallet_08)
        ___jurDebet(KodeTautanCOA_eWallet_09, JumlaheWallet_09)
        _______jurKredit(KodeTautanCOA_PPNKeluaran, JumlahPPN)
        _______jurKredit(KodeTautanCOA_PenjualanEceran, JumlahDPP)

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Close()
    End Sub

    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        dtp_TanggalTransaksi.IsEnabled = False
        txt_NamaToko.IsReadOnly = True
        txt_JumlahTransaksi.IsReadOnly = True
        scv_Kiri.MaxHeight = TinggiMaximalScrollViewerFormDialogVertikal
    End Sub

End Class
