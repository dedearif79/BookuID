Imports bcomm
Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls

Public Class wpfWin_InputJadwalAngsuranHutangBankLeasing

    ' === PUBLIC PROPERTIES (KONFIGURASI) ===
    Public Property JudulForm As String
    Public Property FungsiForm As String
    Public Property JalurMasuk As String
    Public Property BankLeasing As String

    ' === PUBLIC PROPERTIES (DATA INPUT) ===
    Public Property NomorJV As Int64
    Public Property NomorID As Int64
    Public Property NomorBPH As String
    Public Property KodeKreditur As String

    ' === VARIABEL INTERNAL ===
    Dim TabelAngsuran As String
    Dim KolomNomorBPH As String
    Dim SebagaiLembagaKeuangan As Boolean
    Dim LokasiKreditur As String
    Dim JenisPPh As String
    Dim KodeSetoran As String

    ' === NILAI FORM ===
    Dim AngsuranKe As Integer
    Dim TanggalJatuhTempo As Date
    Dim Pokok As Int64
    Dim BagiHasil As Int64
    Dim TarifPPh As Decimal
    Dim JumlahPPh As Int64
    Dim PPhDitanggung As Int64
    Dim PPhDipotong As Int64
    Dim JumlahDibayarkan As Int64

    ' === GUARD FLAGS ===
    Dim ProsesLoadingForm As Boolean
    Dim ProsesResetForm As Boolean


    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If BankLeasing = Kosongan Then PesanUntukProgrammer("Tentukan dulu, ini untuk Bank atau Leasing..???!!!!")

        If BankLeasing = bl_Bank Then
            TabelAngsuran = "tbl_JadwalAngsuranHutangBank"
            KolomNomorBPH = "Nomor_BPHB"
        End If

        If BankLeasing = bl_Leasing Then
            TabelAngsuran = "tbl_JadwalAngsuranHutangLeasing"
            KolomNomorBPH = "Nomor_BPHL"
        End If

        ' Query info kreditur
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Kode_Mitra = '" & KodeKreditur & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Keuangan") = 1 Then SebagaiLembagaKeuangan = True
            If dr.Item("Keuangan") = 0 Then SebagaiLembagaKeuangan = False
            LokasiKreditur = dr.Item("Lokasi_WP").ToString()
        End If
        AksesDatabase_General(Tutup)

        ' Tentukan jenis PPh berdasarkan lokasi dan jenis kreditur
        If LokasiKreditur = LokasiWP_LuarNegeri Then
            grb_BiayaPPh.IsEnabled = True
            grb_BiayaPPh.Header = "PPh Pasal 26 :"
            JenisPPh = JenisPPh_Pasal26
            KodeSetoran = KodeSetoran_102
        Else
            If SebagaiLembagaKeuangan = True Then
                grb_BiayaPPh.IsEnabled = False
                grb_BiayaPPh.Header = "Biaya PPh :"
                txt_TarifPPh.Text = Kosongan
                JenisPPh = Kosongan
                KodeSetoran = Kosongan
            Else
                grb_BiayaPPh.IsEnabled = True
                grb_BiayaPPh.Header = "PPh Pasal 23 :"
                txt_TarifPPh.Text = "15"
                JenisPPh = JenisPPh_Pasal23
                KodeSetoran = KodeSetoran_102
            End If
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Jadwal Angsuran Hutang " & BankLeasing
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                                  " WHERE " & KolomNomorBPH & " = '" & NomorBPH & "' " &
                                  " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                AngsuranKe = dr.Item("Angsuran_Ke")
            Loop
            txt_AngsuranKe.Text = (AngsuranKe + 1).ToString()
            AksesDatabase_Transaksi(Tutup)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Jadwal Angsuran Hutang " & BankLeasing
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                                  " WHERE Nomor_ID = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                AngsuranKe = dr.Item("Angsuran_Ke")
                txt_AngsuranKe.Text = AngsuranKe.ToString()
                If Not IsDBNull(dr.Item("Tanggal_Jatuh_Tempo")) Then
                    dtp_TanggalJatuhTempo.SelectedDate = dr.Item("Tanggal_Jatuh_Tempo")
                End If
                txt_Pokok.Text = dr.Item("Pokok").ToString()
                txt_BagiHasil.Text = dr.Item("Bagi_Hasil").ToString()
                txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh"))
                txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung").ToString()
            End If
            AksesDatabase_Transaksi(Tutup)
        End If

        Title = JudulForm

        ' Set tanggal default hanya jika TAMBAH
        If FungsiForm = FungsiForm_TAMBAH Then
            dtp_TanggalJatuhTempo.Text = Kosongan
        End If

        txt_AngsuranKe.Focus()

        ProsesLoadingForm = False

    End Sub


    Public Sub ResetForm()

        ProsesResetForm = True

        BankLeasing = Kosongan

        NomorID = 0
        NomorJV = 0
        KodeKreditur = Kosongan
        SebagaiLembagaKeuangan = False

        txt_AngsuranKe.Text = Kosongan
        dtp_TanggalJatuhTempo.Text = Kosongan
        txt_Pokok.Text = Kosongan
        txt_BagiHasil.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        txt_JumlahPPh.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_JumlahDibayarkan.Text = Kosongan

        AngsuranKe = 0
        Pokok = 0
        BagiHasil = 0
        TarifPPh = 0
        JumlahPPh = 0
        PPhDitanggung = 0
        PPhDipotong = 0
        JumlahDibayarkan = 0

        ProsesResetForm = False

    End Sub


    Private Sub txt_AngsuranKe_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AngsuranKe.TextChanged
        AngsuranKe = AmbilAngka(txt_AngsuranKe.Text)
    End Sub


    Private Sub dtp_TanggalJatuhTempo_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalJatuhTempo.SelectedDateChanged
        If dtp_TanggalJatuhTempo.SelectedDate.HasValue Then
            TanggalJatuhTempo = dtp_TanggalJatuhTempo.SelectedDate.Value
        End If
    End Sub


    Private Sub txt_Pokok_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Pokok.TextChanged
        Pokok = AmbilAngka(txt_Pokok.Text)
        PerhitunganJumlahDibayarkan()
    End Sub


    Private Sub txt_BagiHasil_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_BagiHasil.TextChanged
        BagiHasil = AmbilAngka(txt_BagiHasil.Text)
        PerhitunganPPh()
    End Sub


    Private Sub txt_TarifPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TarifPPh.TextChanged
        If txt_TarifPPh.Text = "," Then
            txt_TarifPPh.Text = Kosongan
            Return
        End If
        If txt_TarifPPh.Text = Kosongan Then
            TarifPPh = 0
        Else
            Decimal.TryParse(txt_TarifPPh.Text.Replace(",", "."), TarifPPh)
        End If
        If TarifPPh > 20 Then
            Pesan_Peringatan("Silakan isi kolom 'Tarif PPh' dengan benar.")
            txt_TarifPPh.Text = Kosongan
            txt_TarifPPh.Focus()
            Return
        End If
        PerhitunganPPh()
    End Sub


    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        If PPhDitanggung > JumlahPPh Then
            Pesan_Peringatan("Silakan isi kolom 'PPh Ditanggung' dengan benar..!")
            txt_PPhDitanggung.Text = Kosongan
            txt_PPhDitanggung.Focus()
            Return
        End If
        PerhitunganPPh()
    End Sub


    Private Sub txt_JumlahPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahPPh.TextChanged
        JumlahPPh = AmbilAngka(txt_JumlahPPh.Text)
    End Sub


    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
        PerhitunganJumlahDibayarkan()
    End Sub


    Private Sub txt_JumlahDibayarkan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahDibayarkan.TextChanged
        JumlahDibayarkan = AmbilAngka(txt_JumlahDibayarkan.Text)
    End Sub


    Sub PerhitunganPPh()
        txt_JumlahPPh.Text = (BagiHasil * Persen(TarifPPh)).ToString()
        txt_PPhDipotong.Text = (JumlahPPh - PPhDitanggung).ToString()
        PerhitunganJumlahDibayarkan()
    End Sub


    Sub PerhitunganJumlahDibayarkan()
        txt_JumlahDibayarkan.Text = (Pokok + BagiHasil - PPhDipotong).ToString()
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        ' Validasi
        If Pokok = 0 Then
            Pesan_Peringatan("Silakan isi kolom 'Pokok'.")
            txt_Pokok.Focus()
            Return
        End If

        If grb_BiayaPPh.IsEnabled = True And TarifPPh = 0 Then
            Pesan_Peringatan("Silakan isi kolom 'Tarif PPh'.")
            txt_TarifPPh.Focus()
            Return
        End If

        If PPhDitanggung > JumlahPPh Then
            Pesan_Peringatan("Silakan isi kolom 'PPh Ditanggung' dengan benar..!")
            txt_PPhDitanggung.Focus()
            Return
        End If

        ' Ambil tanggal
        If dtp_TanggalJatuhTempo.SelectedDate.HasValue Then
            TanggalJatuhTempo = dtp_TanggalJatuhTempo.SelectedDate.Value
        Else
            TanggalJatuhTempo = TanggalIni
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelAngsuran) + 1

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" INSERT INTO " & TabelAngsuran & " VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPH & "', " &
                                  " '" & KodeKreditur & "', " &
                                  " '" & AngsuranKe & "', " &
                                  " '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " '" & TanggalKosongSimpan & "', " &
                                  " '" & Pokok & "', " &
                                  " '" & BagiHasil & "', " &
                                  " '" & DesimalFormatSimpan(TarifPPh) & "', " &
                                  " '" & JumlahPPh & "', " &
                                  " '" & PPhDitanggung & "', " &
                                  " '" & PPhDipotong & "', " &
                                  " '" & JumlahDibayarkan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & JenisPPh & "', " &
                                  " '" & KodeSetoran & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & NomorJV & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & TanggalKosongSimpan & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE " & TabelAngsuran & " SET " &
                                  KolomNomorBPH & "           = '" & NomorBPH & "', " &
                                  " Kode_Kreditur           = '" & KodeKreditur & "', " &
                                  " Angsuran_Ke             = '" & AngsuranKe & "', " &
                                  " Tanggal_Jatuh_Tempo     = '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " Tanggal_Bayar           = '" & TanggalKosongSimpan & "', " &
                                  " Pokok                   = '" & Pokok & "', " &
                                  " Bagi_Hasil              = '" & BagiHasil & "', " &
                                  " Tarif_PPh               = '" & DesimalFormatSimpan(TarifPPh) & "', " &
                                  " Jumlah_PPh              = '" & JumlahPPh & "', " &
                                  " PPh_Ditanggung          = '" & PPhDitanggung & "', " &
                                  " PPh_Dipotong            = '" & PPhDipotong & "', " &
                                  " Jumlah_Dibayarkan       = '" & JumlahDibayarkan & "', " &
                                  " Denda                   = '" & 0 & "', " &
                                  " Jenis_PPh               = '" & JenisPPh & "', " &
                                  " Kode_Setoran            = '" & KodeSetoran & "', " &
                                  " COA_Kredit              = '" & Kosongan & "', " &
                                  " Biaya_Administrasi_Bank = '" & 0 & "', " &
                                  " Ditanggung_Oleh         = '" & Kosongan & "', " &
                                  " Nomor_JV                = '" & 0 & "', " &
                                  " User                    = '" & UserAktif & "' " &
                                  " WHERE Nomor_ID          = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataBerhasilDiedit_PlusJurnal()
            If usc_BukuPengawasanHutangBank.StatusAktif Then usc_BukuPengawasanHutangBank.TampilkanData_JadwalAngsuran()
            If usc_BukuPengawasanHutangLeasing.StatusAktif Then usc_BukuPengawasanHutangLeasing.TampilkanData_JadwalAngsuran()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class
