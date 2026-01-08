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
    Dim JumlahKas As Int64
    Dim JumlahBank As Int64
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

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        NomorID = 0
        NomorJV = 0
        FungsiForm = Kosongan

        KosongkanDatePicker(dtp_TanggalTransaksi)
        txt_JumlahKas.Text = Kosongan
        txt_JumlahBank.Text = Kosongan
        txt_JumlahTransaksi.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        ProsesResetForm = False

    End Sub



    Private Sub dtp_TanggalTransaksi_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalTransaksi.SelectedDateChanged
        If dtp_TanggalTransaksi.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalTransaksi)
            TanggalTransaksi = TanggalFormatTampilan(dtp_TanggalTransaksi.SelectedDate)
        End If
    End Sub


    Private Sub txt_JumlahKas_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahKas.TextChanged
        JumlahKas = AmbilAngka(txt_JumlahKas.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(txt_JumlahKas)
    End Sub
    Private Sub txt_JumlahKas_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahKas.PreviewTextInput
              
    End Sub


    Private Sub txt_JumlahBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahBank.TextChanged
        JumlahBank = AmbilAngka(txt_JumlahBank.Text)
        Perhitungan()
        PemecahRibuanUntukTextBox_WPF(txt_JumlahBank)
    End Sub
    Private Sub txt_JumlahBank_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahBank.PreviewTextInput
              
    End Sub


    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahTransaksi)
    End Sub
    Private Sub txt_JumlahTransaksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahTransaksi.PreviewTextInput
              
    End Sub


    Sub Perhitungan()
        Dim TarifPPN = AmbilValue_TarifPPNBerdasarkanTanggal(TanggalTransaksi)
        JumlahTransaksi = JumlahKas + JumlahBank
        JumlahPPN = HitungPPNInclude(JumlahTransaksi, TarifPPN)
        JumlahDPP = JumlahTransaksi - JumlahPPN
        txt_JumlahTransaksi.Text = JumlahTransaksi
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub




    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If dtp_TanggalTransaksi.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalTransaksi, "Tanggal Transaksi")
            Return
        End If

        If JumlahTransaksi = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_JumlahKas, "Jumlah Kas atau Jumlah Bank")
            Return
        End If

        AksesDatabase_Transaksi(Buka)

        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PenjualanEceran") + 1
            cmd = New OdbcCommand(" INSERT INTO tbl_PenjualanEceran VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " '" & JumlahKas & "', " &
                                  " '" & JumlahBank & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & JumlahDPP & "', " &
                                  " '" & JumlahPPN & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV & "', " &
                                  " '" & UserAktif & "' " &
                                  " ) ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            jur_NomorJV = NomorJV
            cmd = New OdbcCommand(" UPDATE tbl_PenjualanEceran SET " &
                                  " Tanggal_Transaksi   = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " Jumlah_Kas          = '" & JumlahKas & "', " &
                                  " Jumlah_Bank         = '" & JumlahBank & "', " &
                                  " Jumlah_Transaksi    = '" & JumlahTransaksi & "', " &
                                  " DPP                 = '" & JumlahDPP & "', " &
                                  " PPN                 = '" & JumlahPPN & "', " &
                                  " Keterangan          = '" & Keterangan & "', " &
                                  " Nomor_JV            = '" & NomorJV & "', " &
                                  " User                = '" & UserAktif & "'  " &
                                  " WHERE Nomor_ID      = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            HapusJurnal_BerdasarkanNomorJV(jur_NomorJV)
        End If

        AksesDatabase_Transaksi(Tutup)

        SimpanJurnal()

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            usc_BukuPenjualanEceran.TampilkanData()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataGagalDiperbarui()
        End If

    End Sub

    Sub SimpanJurnal()

        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
        jur_JenisJurnal = JenisJurnal_Penjualan
        jur_KodeDokumen = Kosongan
        jur_NomorPO = Kosongan
        jur_KodeProject = Kosongan
        jur_Referensi = Kosongan
        jur_TanggalInvoice = Kosongan
        jur_NomorInvoice = Kosongan
        jur_NamaProduk = teks_Eceran
        jur_KodeLawanTransaksi = KodeLawanTransaksi_Customer
        jur_NamaLawanTransaksi = NamaLawanTransaksi_Customer
        jur_UraianTransaksi = Keterangan
        jur_Direct = 0


        'Simpan Jurnal :
        ___jurDebet(KodeTautanCOA_Kas, JumlahKas)
        ___jurDebet(KodeTautanCOA_BankEceran1, JumlahBank)
        _______jurKredit(KodeTautanCOA_PPNKeluaran, JumlahPPN)
        _______jurKredit(KodeTautanCOA_PenjualanEceran, JumlahDPP)

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Close()
    End Sub

    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_JumlahTransaksi.IsReadOnly = True
    End Sub

End Class
