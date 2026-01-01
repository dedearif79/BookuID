Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputPemindahbukuan

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim NomorBPPB
    Dim TanggalBPPB
    Dim COAKredit
    Dim COADebet
    Dim Penanggungjawab
    Dim JumlahTransaksi
    Dim UraianTransaksi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Pemindahbukuan"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Pemindahbukuan"
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")



        Me.Text = JudulForm

        ProsesLoadingForm = False

        BeginInvoke(Sub() Style_FormInput(Me))

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        NomorID = 0
        txt_NomorBPPB.Text = Kosongan
        dtp_TanggalBPPB.Value = Today
        KontenComboSaranaPembayaran_Public(cmb_DariBuku)
        KontenComboSaranaPembayaran_Public(cmb_KeBuku)
        COAKredit = Kosongan
        COADebet = Kosongan
        txt_Penanggungjawab.Text = Kosongan
        txt_JumlahTransaksi.Text = Kosongan


        ProsesResetForm = False

    End Sub


    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Pemindahbukuan") + 1
        NomorBPPB = AwalanBPPB_PlusTahunBuku & NomorID
        txt_NomorBPPB.Text = NomorBPPB

    End Sub


    Private Sub txt_NomorBPPB_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBPPB.TextChanged
        NomorBPPB = txt_NomorBPPB.Text
    End Sub
    Private Sub txt_NomorBPPB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBPPB.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub dtp_TanggalBPPB_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalBPPB.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalBPPB)
        TanggalBPPB = dtp_TanggalBPPB.Value
    End Sub


    Private Sub cmb_DariBuku_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DariBuku.SelectedIndexChanged
        COAKredit = KonversiSaranaPembayaranKeCOA(cmb_DariBuku.Text)
    End Sub
    Private Sub cmb_DariBuku_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_DariBuku.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_KeBuku_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_KeBuku.SelectedIndexChanged
        COADebet = KonversiSaranaPembayaranKeCOA(cmb_KeBuku.Text)
    End Sub
    Private Sub cmb_KeBuku_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_KeBuku.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub txt_Penanggungjawab_TextChanged(sender As Object, e As EventArgs) Handles txt_Penanggungjawab.TextChanged
        Penanggungjawab = txt_Penanggungjawab.Text
    End Sub


    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTransaksi)
    End Sub
    Private Sub txt_JumlahTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransaksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub


    Private Sub txt_UraianTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_UraianTransaksi.TextChanged
        UraianTransaksi = txt_UraianTransaksi.Text
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Isi Ulang Value :
        UraianTransaksi = txt_UraianTransaksi.Text

        'Verifikasi Kolom :

        If COAKredit = Kosongan Then
            PesanPeringatan("Silakan pilih 'Dari Buku'.")
            cmb_DariBuku.Focus()
            Return
        End If

        If COADebet = Kosongan Then
            PesanPeringatan("Silakan pilih 'Ke 'Buku'.")
            cmb_DariBuku.Focus()
            Return
        End If

        If Penanggungjawab = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Penanggungjawab'.")
            txt_Penanggungjawab.Focus()
            Return
        End If

        If JumlahTransaksi = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_Pemindahbukuan VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPPB & "', " &
                                  " '" & TanggalFormatSimpan(TanggalBPPB) & "', " &
                                  " '" & COAKredit & "', " &
                                  " '" & COADebet & "', " &
                                  " '" & Penanggungjawab & "', " &
                                  " '" & TanggalKosongSimpan & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & UraianTransaksi & "', " &
                                  " '" & 0 & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_Pemindahbukuan SET " &
                                  " Nomor_BPPB          = '" & NomorBPPB & "', " &
                                  " Tanggal_BPPB        = '" & TanggalFormatSimpan(TanggalBPPB) & "', " &
                                  " COA_Kredit          = '" & COAKredit & "', " &
                                  " COA_Debet           = '" & COADebet & "', " &
                                  " Penanggungjawab     = '" & Penanggungjawab & "', " &
                                  " Jumlah_Transaksi    = '" & JumlahTransaksi & "', " &
                                  " Uraian_Transaksi    = '" & UraianTransaksi & "', " &
                                  " User                = '" & UserAktif & "' " &
                                  " WHERE Nomor_BPPB    = '" & NomorBPPB & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_BukuPengawasanPemindahbukuan.StatusAktif Then usc_BukuPengawasanPemindahbukuan.TampilkanData()  'Ini Harus Diganti dengan Modul Terkait.
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataGagalDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihGagalDiperbarui()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

End Class