Imports bcomm
Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_InputBankGaransi

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorID
    Dim NomorBPBG
    Dim NomorKontrak
    Dim TanggalTransaksi
    Dim NamaBank
    Dim Keperluan
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahTransaksi
    Dim BiayaProvisi
    Dim Keterangan
    Public NomorJV_Transaksi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Bank Garansi"
            SistemPenomoranOtomatis()
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Bank Garansi"
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
        dtp_TanggalTransaksi.Value = Today
        txt_NomorBPBG.Text = Kosongan
        txt_NamaBank.Text = Kosongan
        txt_Keperluan.Text = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_JumlahTransaksi.Text = Kosongan
        txt_BiayaProvisi.Text = Kosongan
        txt_Keterangan.Text = Kosongan

        NomorJV_Transaksi = 0


        ProsesResetForm = False

    End Sub


    Sub SistemPenomoranOtomatis()

        NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_BankGaransi") + 1
        NomorBPBG = AwalanBPBG_PlusTahunBuku & NomorID
        txt_NomorBPBG.Text = NomorBPBG

    End Sub


    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalTransaksi)
        TanggalTransaksi = dtp_TanggalTransaksi.Value
    End Sub

    Private Sub txt_NomorBPBG_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBPBG.TextChanged
        NomorBPBG = txt_NomorBPBG.Text
    End Sub
    Private Sub txt_NomorBPBG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NomorBPBG.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NomorKontrak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorKontrak.TextChanged
        NomorKontrak = txt_NomorKontrak.Text
    End Sub

    Private Sub txt_NamaBank_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaBank.TextChanged
        NamaBank = txt_NamaBank.Text
    End Sub

    Private Sub txt_Keperluan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keperluan.TextChanged
        Keperluan = txt_Keperluan.Text
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
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = Pilihan_Semua
        If txt_KodeLawanTransaksi.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = txt_KodeLawanTransaksi.Text
            win_ListLawanTransaksi.NamaMitraTerseleksi = txt_NamaLawanTransaksi.Text
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeLawanTransaksi.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
    End Sub


    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub
    Private Sub txt_NamaLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransaksi.TextChanged
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTransaksi)
    End Sub
    Private Sub txt_JumlahTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransaksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_BiayaProvisi_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaProvisi.TextChanged
        BiayaProvisi = AmbilAngka(txt_BiayaProvisi.Text)
        PemecahRibuanUntukTextBox(txt_BiayaProvisi)
    End Sub
    Private Sub txt_BiayaProvisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BiayaProvisi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub



    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Isi Ulang Value :
        Keterangan = txt_Keterangan.Text

        'Validasi Form :
        If NomorKontrak = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nomor Kontrak'.")
            txt_NomorKontrak.Focus()
            Return
        End If

        If NamaBank = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Nama Bank'.")
            txt_NamaBank.Focus()
            Return
        End If

        If Keperluan = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Keperluan'.")
            txt_Keperluan.Focus()
            Return
        End If

        If KodeLawanTransaksi = Kosongan Then
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

            cmd = New OdbcCommand(" INSERT INTO tbl_BankGaransi VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & NomorBPBG & "', " &
                                  " '" & NomorKontrak & "', " &
                                  " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " '" & NamaBank & "', " &
                                  " '" & Keperluan & "', " &
                                  " '" & KodeLawanTransaksi & "', " &
                                  " '" & NamaLawanTransaksi & "', " &
                                  " '" & JumlahTransaksi & "', " &
                                  " '" & BiayaProvisi & "', " &
                                  " '" & Kosongan & "', " &
                                  " '" & TanggalKosongSimpan & "', " &
                                  " '" & Keterangan & "', " &
                                  " '" & NomorJV_Transaksi & "', " &
                                  " '" & 0 & "', " &
                                  " '" & UserAktif & "' ) ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

        End If

        If FungsiForm = FungsiForm_EDIT Then

            cmd = New OdbcCommand(" UPDATE tbl_BankGaransi SET " &
                                  " Tanggal_Transaksi       = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                                  " Nomor_Kontrak           = '" & NomorKontrak & "', " &
                                  " Nama_Bank               = '" & NamaBank & "', " &
                                  " Keperluan               = '" & Keperluan & "', " &
                                  " Kode_Lawan_Transaksi    = '" & KodeLawanTransaksi & "', " &
                                  " Nama_Lawan_Transaksi    = '" & NamaLawanTransaksi & "', " &
                                  " Jumlah_Transaksi        = '" & JumlahTransaksi & "', " &
                                  " Biaya_Provisi           = '" & BiayaProvisi & "', " &
                                  " Keterangan              = '" & Keterangan & "', " &
                                  " Nomor_JV_Transaksi      = '" & NomorJV_Transaksi & "', " &
                                  " User                    = '" & UserAktif & "' " &
                                  " WHERE Nomor_BPBG        = '" & NomorBPBG & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

        End If

        AksesDatabase_Transaksi(Tutup)


        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            frm_BukuBankGaransi.TampilkanData() 'Ini Harus Diganti dengan Modul Terkait.
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