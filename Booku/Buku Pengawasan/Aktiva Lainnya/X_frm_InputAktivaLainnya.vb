Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_InputAktivaLainnya

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim NomorBPAL
    Dim NomorBukti
    Dim TanggalBukti
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UraianTransaksi
    Dim COADebet
    Dim COAKredit
    Dim NamaAkun
    Dim JumlahTransaksi
    Dim Keterangan

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Data Investasi"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Data Investasi"
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
        txt_NomorBPAL.Text = Kosongan
        dtp_TanggalBukti.Value = Today
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_UraianTransaksi.Text = Kosongan
        txt_COADebet.Text = Kosongan
        txt_NamaAkun.Text = Kosongan
        txt_JumlahTransaksi.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        ProsesResetForm = False

    End Sub


    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_AktivaLainnya") + 1
        NomorBPAL = AwalanBPAL_PlusTahunBuku & NomorID
        txt_NomorBPAL.Text = NomorBPAL

    End Sub



    Private Sub txt_NomorBPAL_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBPAL.TextChanged
        NomorBPAL = txt_NomorBPAL.Text
    End Sub
    Private Sub txt_NomorBPAL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBPAL.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub txt_NomorBukti_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBukti.TextChanged
        NomorBukti = txt_NomorBukti.Text
    End Sub


    Private Sub dtp_TanggalBukti_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalBukti.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalBukti)
        TanggalBukti = dtp_TanggalBukti.Value
    End Sub


    Private Sub txt_KodeLawanTransaksi_Click(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
    End Sub
    Private Sub txt_KodeLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        frm_ListMitra.ResetForm()
        If txt_KodeLawanTransaksi.Text <> Kosongan Then
            frm_ListMitra.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
            frm_ListMitra.NamaMitraTerseleksi = txt_NamaLawanTransaksi.Text
        End If
        frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
        frm_ListMitra.ShowDialog()
        txt_KodeLawanTransaksi.Text = frm_ListMitra.KodeMitraTerseleksi
    End Sub


    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub
    Private Sub txt_NamaLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_UraianTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_UraianTransaksi.TextChanged
        UraianTransaksi = txt_UraianTransaksi.Text
    End Sub

    Private Sub txt_COADebet_TextChanged(sender As Object, e As EventArgs) Handles txt_COADebet.TextChanged
        COADebet = txt_COADebet.Text
        txt_NamaAkun.Text = AmbilValue_NamaAkun(COADebet)
    End Sub
    Private Sub txt_COADebet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COADebet.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOADebet_Click(sender As Object, e As EventArgs) Handles btn_PilihCOADebet.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_AktivaLainnya
        If txt_COADebet.Text <> Kosongan Then
            win_ListCOA.COATerseleksi = txt_COADebet.Text
            win_ListCOA.NamaAkunTerseleksi = txt_NamaAkun.Text
        End If
        win_ListCOA.ShowDialog()
        txt_COADebet.Text = win_ListCOA.COATerseleksi
    End Sub
    Private Sub txt_NamaAkun_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun.TextChanged
        NamaAkun = txt_NamaAkun.Text
    End Sub
    Private Sub txt_NamaAkun_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkun.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTransaksi)
    End Sub
    Private Sub txt_JumlahTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransaksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Isi Ulang Value :
        UraianTransaksi = txt_UraianTransaksi.Text
        Keterangan = txt_Keterangan.Text

        'Validasi Form :
        If NomorBukti = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Bukti'.")
            txt_NomorBukti.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lawan Transaksi'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If COADebet = Kosongan Then
            PesanPeringatan("Silakan pilih 'Akun'.")
            txt_COADebet.Focus()
            Return
        End If

        If JumlahTransaksi = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Focus()
            Return
        End If


        If FungsiForm = FungsiForm_TAMBAH Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" INSERT INTO tbl_AktivaLainnya VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPAL & "', " &
                                  " '" & NomorBukti & "', " &
                                  " '" & TanggalFormatSimpan(TanggalBukti) & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & UraianTransaksi & "', " &
                                  " '" & COADebet & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & TanggalFormatSimpan(TanggalKosong) & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & 0 & "', " &
                                  " '" & UserAktif & "' ) ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE tbl_AktivaLainnya SET " &
                                  " Nomor_Bukti             = '" & NomorBukti & "', " &
                                  " Tanggal_Bukti           = '" & TanggalFormatSimpan(TanggalBukti) & "', " &
                                  " Kode_Lawan_Transaksi    = '" & KodeLawanTransaksi & "', " &
                                  " Nama_Lawan_Transaksi    = '" & NamaLawanTransaksi & "', " &
                                  " Uraian_Transaksi        = '" & UraianTransaksi & "', " &
                                  " COA_Debet               = '" & COADebet & "', " &
                                  " Jumlah_Transaksi        = '" & JumlahTransaksi & "', " &
                                  " Keterangan              = '" & Keterangan & "', " &
                                  " User                    = '" & UserAktif & "' " &
                                  " WHERE Nomor_BPAL        = '" & NomorBPAL & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If usc_BukuPengawasanAktivaLainnya.StatusAktif Then usc_BukuPengawasanAktivaLainnya.TampilkanData()
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