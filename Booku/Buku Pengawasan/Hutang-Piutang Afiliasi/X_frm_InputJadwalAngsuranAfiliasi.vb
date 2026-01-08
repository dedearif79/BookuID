Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_InputJadwalAngsuranAfiliasi


    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public HutangPiutang
    Dim TabelAngsuran
    Dim KolomNomorBP
    Dim AwalanBP
    Dim KolomCOASaranaPembayaran

    Public NomorJV
    Public NomorID
    Public NomorBP
    Public KodeLawanTransaksi
    Dim SebagaiLembagaKeuangan As Boolean
    Dim LokasiLawanTransaksi
    Dim JenisPPh
    Dim KodeSetoran
    Dim AngsuranKe
    Dim TanggalJatuhTempo
    Dim Pokok
    Dim BagiHasil
    Dim TarifPPh As Decimal
    Dim JumlahPPh
    Dim PPhDitanggung
    Dim PPhDipotong
    Dim JumlahDibayarkan
    Dim SaldoAkhir

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Select Case HutangPiutang
            Case hp_Hutang
                TabelAngsuran = "tbl_JadwalAngsuranHutangAfiliasi"
                KolomNomorBP = "Nomor_BPHA"
                AwalanBP = AwalanBPHA_PlusTahunBuku
                KolomCOASaranaPembayaran = "COA_Kredit"
            Case hp_Piutang
                TabelAngsuran = "tbl_JadwalAngsuranPiutangAfiliasi"
                KolomNomorBP = "Nomor_BPPA"
                AwalanBP = AwalanBPPA_PlusTahunBuku
                KolomCOASaranaPembayaran = "COA_Debet"
            Case Else
                PesanUntukProgrammer("Tentukan dulu, hutang atau piutang..!!!")
        End Select

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                              " WHERE Kode_Mitra = '" & KodeLawanTransaksi & "' ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        dr.Read()
        If dr.HasRows Then
            If dr.Item("Keuangan") = 1 Then SebagaiLembagaKeuangan = True
            If dr.Item("Keuangan") = 0 Then SebagaiLembagaKeuangan = False
            LokasiLawanTransaksi = dr.Item("Lokasi_WP")
        End If
        AksesDatabase_General(Tutup)

        If LokasiLawanTransaksi = LokasiWP_LuarNegeri Then
            grb_BiayaPPh.Enabled = False
            grb_BiayaPPh.Text = "Biaya PPh :"
            JenisPPh = JenisPPh_Pasal26
            KodeSetoran = KodeSetoran_102
        Else
            If SebagaiLembagaKeuangan = True Then
                grb_BiayaPPh.Enabled = False
                grb_BiayaPPh.Text = "Biaya PPh :"
                txt_TarifPPh.Text = Kosongan
                JenisPPh = Kosongan
                KodeSetoran = Kosongan
            Else
                grb_BiayaPPh.Enabled = True
                grb_BiayaPPh.Text = "PPh Pasal 23 :"
                txt_TarifPPh.Text = 15
                JenisPPh = JenisPPh_Pasal23
                KodeSetoran = KodeSetoran_102
            End If
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Jadwal Angsuran " & HutangPiutang & " Afiliasi"
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                                  " WHERE " & KolomNomorBP & " = '" & NomorBP & "' " &
                                  " ORDER BY Angsuran_Ke ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                AngsuranKe = dr.Item("Angsuran_Ke")
            Loop
            txt_AngsuranKe.Text = AngsuranKe + 1
            AksesDatabase_Transaksi(Tutup)
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Jadwal Angsuran " & HutangPiutang & " Afiliasi"
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM " & TabelAngsuran &
                                  " WHERE " & KolomNomorBP & " = '" & NomorBP & "' " &
                                  " AND Angsuran_Ke = '" & AngsuranKe & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                txt_TarifPPh.Text = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh"))
                txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
            End If
            AksesDatabase_Transaksi(Tutup)
        End If

        Me.Text = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        NomorID = 0
        NomorJV = 0
        KodeLawanTransaksi = Kosongan
        SebagaiLembagaKeuangan = False
        txt_AngsuranKe.Text = Kosongan
        dtp_TanggalJatuhTempo.Text = TanggalIni
        txt_Pokok.Text = Kosongan
        txt_BagiHasil.Text = Kosongan
        txt_TarifPPh.Text = Kosongan
        txt_JumlahPPh.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_JumlahDibayarkan.Text = Kosongan

        ProsesResetForm = False

    End Sub


    Private Sub txt_AngsuranKe_TextChanged(sender As Object, e As EventArgs) Handles txt_AngsuranKe.TextChanged
        AngsuranKe = AmbilAngka(txt_AngsuranKe.Text)
    End Sub
    Private Sub txt_AngsuranKe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_AngsuranKe.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub dtp_TanggalJatuhTempo_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalJatuhTempo.ValueChanged
        TanggalJatuhTempo = dtp_TanggalJatuhTempo.Value
    End Sub

    Private Sub txt_Pokok_TextChanged(sender As Object, e As EventArgs) Handles txt_Pokok.TextChanged
        Pokok = AmbilAngka(txt_Pokok.Text)
        PerhitunganJumlahDibayarkan()
        PemecahRibuanUntukTextBox(txt_Pokok)
    End Sub
    Private Sub txt_Pokok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Pokok.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_BagiHasil_TextChanged(sender As Object, e As EventArgs) Handles txt_BagiHasil.TextChanged
        BagiHasil = AmbilAngka(txt_BagiHasil.Text)
        PerhitunganPPh()
        PemecahRibuanUntukTextBox(txt_BagiHasil)
    End Sub

    Private Sub txt_BagiHasil_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_BagiHasil.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_TarifPPh_TextChanged(sender As Object, e As EventArgs) Handles txt_TarifPPh.TextChanged
        If txt_TarifPPh.Text = "," Then
            txt_TarifPPh.Text = Kosongan
            Return
        End If
        If txt_TarifPPh.Text = Kosongan Then
            TarifPPh = 0
        Else
            TarifPPh = txt_TarifPPh.Text
        End If
        If TarifPPh > 20 Then
            MsgBox("Silakan isi kolom 'Tarif PPh' dengan benar.")
            txt_TarifPPh.Text = Kosongan
            txt_TarifPPh.Focus()
            Return
        End If
        PerhitunganPPh()
    End Sub
    Private Sub txt_TarifPPh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TarifPPh.KeyPress
        If LokasiLawanTransaksi = LokasiWP_DalamNegeri Then
            KunciTotalInputan(sender, e)
        Else
            HanyaBolehInputAngkaDesimalPlus(sender, e)
        End If
    End Sub

    Private Sub txt_JumlahPPh_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPPh.TextChanged
        JumlahPPh = AmbilAngka(txt_JumlahPPh.Text)
        PemecahRibuanUntukTextBox(txt_JumlahPPh)
    End Sub
    Private Sub txt_JumlahPPh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahPPh.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung = AmbilAngka(txt_PPhDitanggung.Text)
        If PPhDitanggung > JumlahPPh Then
            MsgBox("Silakan isi kolom 'PPh Pasal 23 Ditanggung' dengan benar..!")
            txt_PPhDitanggung.Text = Kosongan
            txt_PPhDitanggung.Focus()
            Return
        End If
        PerhitunganPPh()
        PemecahRibuanUntukTextBox(txt_PPhDitanggung)
    End Sub
    Private Sub txt_PPhDitanggung_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDitanggung.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
        PerhitunganJumlahDibayarkan()
        PemecahRibuanUntukTextBox(txt_PPhDipotong)
    End Sub
    Private Sub txt_PPhDipotong_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDipotong.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Sub PerhitunganPPh()
        txt_JumlahPPh.Text = BagiHasil * Persen(TarifPPh)
        txt_PPhDipotong.Text = JumlahPPh - PPhDitanggung
        PerhitunganJumlahDibayarkan()
    End Sub

    Private Sub txt_JumlahDibayarkan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahDibayarkan.TextChanged
        JumlahDibayarkan = AmbilAngka(txt_JumlahDibayarkan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahDibayarkan)
    End Sub
    Private Sub txt_JumlahDibayarkan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahDibayarkan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Sub PerhitunganJumlahDibayarkan()
        txt_JumlahDibayarkan.Text = Pokok + BagiHasil - PPhDipotong
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        If Pokok = 0 Then
            MsgBox("Silakan isi kolom 'Pokok'.")
            txt_Pokok.Focus()
            Return
        End If

        If grb_BiayaPPh.Enabled = True And TarifPPh = 0 Then
            MsgBox("Silakan isi kolom 'Tarif PPh'.")
            txt_TarifPPh.Focus()
            Return
        End If

        If PPhDitanggung > JumlahPPh Then
            MsgBox("Silakan isi kolom 'PPh Ditanggung' dengan benar..!")
            txt_PPhDitanggung.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, TabelAngsuran) + 1

            AksesDatabase_Transaksi(Buka)

            Dim QueryPenyimpanan =
                " INSERT INTO " & TabelAngsuran & " VALUES ( " &
                " '" & NomorID & "', " &
                " '" & NomorBP & "', " &
                " '" & KodeLawanTransaksi & "', " &
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
                " '" & UserAktif & "' ) "
            cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            cmd = New OdbcCommand(" UPDATE " & TabelAngsuran & " SET " &
                                  KolomNomorBP & "              = '" & NomorBP & "', " &
                                  " Kode_Lawan_Transaksi        = '" & KodeLawanTransaksi & "', " &
                                  " Angsuran_Ke                 = '" & AngsuranKe & "', " &
                                  " Tanggal_Jatuh_Tempo         = '" & TanggalFormatSimpan(TanggalJatuhTempo) & "', " &
                                  " Tanggal_Bayar               = '" & TanggalKosongSimpan & "', " &
                                  " Pokok                       = '" & Pokok & "', " &
                                  " Bagi_Hasil                  = '" & BagiHasil & "', " &
                                  " Tarif_PPh                   = '" & DesimalFormatSimpan(TarifPPh) & "', " &
                                  " Jumlah_PPh                  = '" & JumlahPPh & "', " &
                                  " PPh_Ditanggung              = '" & PPhDitanggung & "', " &
                                  " PPh_Dipotong                = '" & PPhDipotong & "', " &
                                  " Jumlah_Dibayarkan           = '" & JumlahDibayarkan & "', " &
                                  " Denda                       = '" & 0 & "', " &
                                  " Jenis_PPh                   = '" & JenisPPh & "', " &
                                  " Kode_Setoran                = '" & KodeSetoran & "', " &
                                  KolomCOASaranaPembayaran & "  = '" & Kosongan & "', " &
                                  " Biaya_Administrasi_Bank     = '" & 0 & "', " &
                                  " Ditanggung_Oleh             = '" & Kosongan & "', " &
                                  " Nomor_JV                    = '" & 0 & "', " &
                                  " User                        = '" & UserAktif & "' " &
                                  " WHERE Nomor_ID              = '" & NomorID & "' ",
                                  KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If HutangPiutang = hp_Hutang Then usc_BukuPengawasanHutangAfiliasi.TampilkanData_JadwalAngsuran()
            If HutangPiutang = hp_Piutang Then usc_BukuPengawasanPiutangAfiliasi.TampilkanData_JadwalAngsuran()
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