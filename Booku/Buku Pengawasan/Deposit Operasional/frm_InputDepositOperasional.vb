Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputDepositOperasional

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim NomorBPDO
    Dim NomorBukti
    Dim TanggalBukti
    Dim NomorFakturPajak
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UraianTransaksi
    Dim KodeCustomer
    Dim NamaCustomer
    Dim JumlahTransaksi
    Dim Keterangan
    Public NomorJV

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Deposit Operasional"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Deposit Operasional"
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
        txt_NomorBPDO.Text = Kosongan
        txt_NomorBukti.Text = Kosongan
        dtp_TanggalBukti.Value = Today
        txt_NomorFakturPajak.Text = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_UraianTransaksi.Text = Kosongan
        txt_KodeCustomer.Text = Kosongan
        txt_NamaCustomer.Text = Kosongan
        txt_JumlahTransaksi.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        NomorJV = 0


        ProsesResetForm = False

    End Sub


    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_DepositOperasional") + 1
        NomorBPDO = AwalanBPDO_PlusTahunBuku & NomorID
        txt_NomorBPDO.Text = NomorBPDO

    End Sub


    Private Sub txt_NomorBPDO_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBPDO.TextChanged
        NomorBPDO = txt_NomorBPDO.Text
    End Sub
    Private Sub txt_NomorBPDO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBPDO.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NomorBukti_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBukti.TextChanged
        NomorBukti = txt_NomorBukti.Text
    End Sub

    Private Sub dtp_TanggalBukti_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalBukti.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalBukti)
        TanggalBukti = dtp_TanggalBukti.Value
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub txt_KodeLawanTransaksi_Click(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.Click
        btn_PilihLawanTransaksi_Click(sender, e)
    End Sub
    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
        txt_NamaLawanTransaksi.Text = AmbilValue_NamaMitra(KodeLawanTransaksi)
    End Sub
    Private Sub txt_KodeLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_PilihLawanTransaksi_Click(sender As Object, e As EventArgs) Handles btn_PilihLawanTransaksi.Click
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

    End Sub


    Private Sub txt_KodeCustomer_Click(sender As Object, e As EventArgs) Handles txt_KodeCustomer.Click
        btn_PilihCustomer_Click(sender, e)
    End Sub
    Private Sub txt_KodeCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeCustomer.TextChanged
        KodeCustomer = txt_KodeCustomer.Text
    End Sub
    Private Sub txt_KodeCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeCustomer.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_PilihCustomer_Click(sender As Object, e As EventArgs) Handles btn_PilihCustomer.Click
        frm_ListMitra.ResetForm()
        If txt_KodeCustomer.Text <> Kosongan Then
            frm_ListMitra.KodeMitraTerseleksi = txt_KodeCustomer.Text
            frm_ListMitra.NamaMitraTerseleksi = txt_NamaCustomer.Text
        End If
        frm_ListMitra.PilihJenisLawanTransaksi = Pilihan_Semua
        frm_ListMitra.ShowDialog()
        txt_KodeCustomer.Text = frm_ListMitra.KodeMitraTerseleksi
        txt_NamaCustomer.Text = frm_ListMitra.NamaMitraTerseleksi
    End Sub


    Private Sub txt_NamaCustomer_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaCustomer.TextChanged
        NamaCustomer = txt_NamaCustomer.Text
    End Sub
    Private Sub txt_NamaCustomer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaCustomer.KeyPress
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

        If KodeCustomer = Kosongan Then
            PesanPeringatan("Silakan pilih 'Lawan Transaksi'.")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If JumlahTransaksi = 0 Then
            PesanPeringatan("Silakan isi kolom 'Jumlah Transaksi'.")
            txt_JumlahTransaksi.Focus()
            Return
        End If



        AksesDatabase_Transaksi(Buka)

        If FungsiForm = FungsiForm_TAMBAH Then

            cmd = New OdbcCommand(" INSERT INTO tbl_DepositOperasional VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPDO & "', " &
                                  " '" & NomorBukti & "', " &
                                  " '" & TanggalFormatSimpan(TanggalBukti) & "', " &
                                  " '" & NomorFakturPajak & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & UraianTransaksi & "', " &
                                  " '" & KodeCustomer & "', " &
                                  " '" & NamaCustomer & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & UserAktif & "' ) ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

        End If

        If FungsiForm = FungsiForm_EDIT Then

            cmd = New OdbcCommand(" UPDATE tbl_DepositOperasional SET " &
                                  " Nomor_Bukti             = '" & NomorBukti & "', " &
                                  " Tanggal_Bukti           = '" & TanggalFormatSimpan(TanggalBukti) & "', " &
                                  " Nomor_Faktur_Pajak      = '" & NomorFakturPajak & "', " &
                                  " Kode_Lawan_Transaksi    = '" & KodeLawanTransaksi & "', " &
                                  " Nama_Lawan_Transaksi    = '" & NamaLawanTransaksi & "', " &
                                  " Uraian_Transaksi        = '" & UraianTransaksi & "', " &
                                  " Kode_CUstomer           = '" & KodeCustomer & "', " &
                                  " Nama_Customer           = '" & NamaCustomer & "', " &
                                  " Jumlah_Transaksi        = '" & JumlahTransaksi & "', " &
                                  " Keterangan              = '" & Keterangan & "', " &
                                  " User                    = '" & UserAktif & "' " &
                                  " WHERE Nomor_BPDO        = '" & NomorBPDO & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

        End If

        AksesDatabase_Transaksi(Tutup)


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            usc_BukuPengawasanDepositOperasional.TampilkanData() 'Ini Harus Diganti dengan Modul Terkait.
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