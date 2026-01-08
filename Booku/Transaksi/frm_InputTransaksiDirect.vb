Imports bcomm

Public Class frm_InputTransaksiDirect

    Public NomorJV
    Public FungsiForm
    Public JalurMasuk
    Public JudulForm
    Dim ProsesSuntingDatabase As Boolean

    Dim AlurTransaksi
    Dim NomorBukti
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Public COAUtama
    Public NamaAkunUtama
    Dim COALawan
    Dim NamaAkunLawan
    Dim JumlahTransaksi
    Dim NamaProduk
    Dim TanggalTransaksi
    Dim Uraian
    Dim TahunTransaksi
    Public AngkaNomorJV_Terseleksi
    Public SaranaPembayaran


    Private Sub frm_InputTransaksiDirect_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btn_Lanjutkan.Visible = False

        Select Case FungsiForm
            Case FungsiForm_TAMBAH
                JudulForm = "Input " & JudulForm
            Case FungsiForm_EDIT
                JudulForm = "Edit " & JudulForm
                If NomorJV = 0 Then PesanUntukProgrammer("Value Nomor JV belum terisi...!!!")
        End Select

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Me.Text = JudulForm

    End Sub


    Public Sub ResetForm()

        NomorJV = 0
        AngkaNomorJV_Terseleksi = 0
        KontenComboAlurTransaksi()
        txt_NomorBukti.Text = Nothing
        txt_KodeLawanTransaksi.Text = Nothing
        txt_NamaLawanTransaksi.Text = Nothing
        txt_COALawan.Text = Nothing
        txt_NamaAkunLawan.Text = Nothing
        txt_JumlahTransaksi.Text = Nothing
        txt_NamaProduk.Text = Kosongan
        dtp_TanggalTransaksi.Value = Today
        txt_Uraian.Text = "" 'Biarkan bengini. Dia tidak mau pakai value 'Nothing'
    End Sub

    Sub KontenComboAlurTransaksi()
        cmb_AlurTransaksi.Items.Clear()
        cmb_AlurTransaksi.Items.Add(AlurTransaksi_IN)
        cmb_AlurTransaksi.Items.Add(AlurTransaksi_OUT)
        cmb_AlurTransaksi.Text = Nothing
    End Sub

    Private Sub cmb_AlurTransaksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_AlurTransaksi.SelectedIndexChanged

        AlurTransaksi = cmb_AlurTransaksi.Text

    End Sub
    Private Sub cmb_AlurTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_AlurTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NomorBukti_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBukti.TextChanged
        NomorBukti = txt_NomorBukti.Text
    End Sub

    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
    End Sub
    Private Sub txt_KodeLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_KodeLawanTransaksi_Click(sender As Object, e As EventArgs) Handles txt_KodeLawanTransaksi.Click
        btn_PilihKodeLawanTransaksi_Click(sender, e)
    End Sub

    Private Sub btn_PilihKodeLawanTransaksi_Click(sender As Object, e As EventArgs) Handles btn_PilihKodeLawanTransaksi.Click
        win_ListLawanTransaksi = New wpfWin_ListLawanTransaksi
        win_ListLawanTransaksi.ResetForm()
        win_ListLawanTransaksi.PilihJenisLawanTransaksi = Pilihan_Semua
        win_ListLawanTransaksi.PilihPemegangSaham = Pilihan_Ya
        If txt_KodeLawanTransaksi.Text <> Kosongan Then
            win_ListLawanTransaksi.KodeMitraTerseleksi = KodeLawanTransaksi
            win_ListLawanTransaksi.NamaMitraTerseleksi = NamaLawanTransaksi
        End If
        win_ListLawanTransaksi.ShowDialog()
        txt_KodeLawanTransaksi.Text = win_ListLawanTransaksi.KodeMitraTerseleksi
        txt_NamaLawanTransaksi.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
    End Sub

    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub
    Private Sub txt_NamaLawanTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_COALawan_TextChanged(sender As Object, e As EventArgs) Handles txt_COALawan.TextChanged
        COALawan = txt_COALawan.Text
    End Sub
    Private Sub txt_COALawan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COALawan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_COALawan_Click(sender As Object, e As EventArgs) Handles txt_COALawan.Click
        btn_PilihCOALawan_Click(sender, e)
    End Sub

    Private Sub btn_PilihCOALawan_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA.Click
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_Semua
        If txt_COALawan.Text <> Nothing Then
            win_ListCOA.COATerseleksi = COALawan
            win_ListCOA.NamaAkunTerseleksi = NamaAkunLawan
        End If
        win_ListCOA.ShowDialog()
        txt_COALawan.Text = win_ListCOA.COATerseleksi
        txt_NamaAkunLawan.Text = win_ListCOA.NamaAkunTerseleksi
    End Sub

    Private Sub txt_NamaAkunlawan_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkunLawan.TextChanged
        NamaAkunLawan = txt_NamaAkunLawan.Text
    End Sub
    Private Sub txt_NamaAkunLawan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaAkunLawan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTransaksi.TextChanged
        Try
            If txt_JumlahTransaksi.Text.Trim() <> Nothing Then
                txt_JumlahTransaksi.Text = CDec(txt_JumlahTransaksi.Text).ToString("N0")
                txt_JumlahTransaksi.SelectionStart = txt_JumlahTransaksi.TextLength
            End If
        Catch ex As Exception
        End Try
        JumlahTransaksi = AmbilAngka(txt_JumlahTransaksi.Text)
    End Sub
    Private Sub txt_JumlahTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTransaksi.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub


    Private Sub txt_NamaProduk_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaProduk.TextChanged
        NamaProduk = txt_NamaProduk.Text
    End Sub


    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.ValueChanged
        If dtp_TanggalTransaksi.Value.Year <> TahunBukuAktif Then
            dtp_TanggalTransaksi.Value = New Date(TahunBukuAktif, dtp_TanggalTransaksi.Value.Month, dtp_TanggalTransaksi.Value.Day)
        End If
        TanggalTransaksi = TanggalFormatSimpan(dtp_TanggalTransaksi.Value)
        TahunTransaksi = dtp_TanggalTransaksi.Value.Year
    End Sub
    Private Sub dtp_TanggalTransaksi_Leave(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.Leave
        If dtp_TanggalTransaksi.Value > Today Then
            MsgBox("Tanggal Bayar yang Anda input melebihi hari ini." & Enter2Baris & "Silakan isi kolom 'Tanggal Bayar' dengan benar.")
            dtp_TanggalTransaksi.Value = Today
            dtp_TanggalTransaksi.Focus()
            Return
        End If
    End Sub

    Private Sub txt_Uraian_TextChanged(sender As Object, e As EventArgs) Handles txt_Uraian.TextChanged
        Uraian = txt_Uraian.Text
    End Sub

    Private Sub btn_Lanjutkan_Click(sender As Object, e As EventArgs) Handles btn_Lanjutkan.Click

        If cmb_AlurTransaksi.Text = Nothing Then
            MsgBox("Silakan pilih 'Alur Transaksi'")
            cmb_AlurTransaksi.Focus()
            Return
        End If

        If NamaLawanTransaksi = Nothing Then
            MsgBox("Silakan isi kolom 'Lawan Transaksi'")
            txt_NamaLawanTransaksi.Focus()
            Return
        End If

        If COALawan = Nothing Then
            MsgBox("Silakan isi kolom 'Lawan Akun'")
            txt_COALawan.Focus()
            Return
        End If

        If JumlahTransaksi = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Transaksi'")
            txt_JumlahTransaksi.Focus()
            Return
        End If

        If TahunTransaksi <> TahunBukuAktif Then
            MsgBox("Silakan isi kolom 'Tanggal Transaksi' dengan benar." & Enter2Baris & "Sesuaikan tahunnya dengan Tahun Buku Aktif.")
            Return
        End If

        frm_InputTransaksi.ResetForm()
        frm_InputTransaksi.JalurMasuk = Halaman_INPUTTRANSAKSIKAS
        frm_InputTransaksi.FungsiForm = FungsiForm
        If FungsiForm = FungsiForm_EDIT Then jur_NomorJV = AngkaNomorJV_Terseleksi
        frm_InputTransaksi.AlurTransaksi = AlurTransaksi
        Select Case AlurTransaksi
            Case AlurTransaksi_IN
                frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_DanaMasukLainnya
                frm_InputTransaksi.cmb_SaranaPembayaran.Text = Kosongan
                frm_InputTransaksi.COAUtama = COALawan
                frm_InputTransaksi.COASaranaPembayaran = COAUtama
            Case AlurTransaksi_OUT
                frm_InputTransaksi.cmb_JenisTransaksi.Text = JenisTransaksi_DanaKeluarLainnya
                frm_InputTransaksi.COAUtama = COAUtama
                frm_InputTransaksi.COASaranaPembayaran = COALawan
                frm_InputTransaksi.cmb_SaranaPembayaran.Text = SaranaPembayaran
        End Select
        frm_InputTransaksi.txt_Referensi.Text = NomorBukti
        frm_InputTransaksi.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi
        frm_InputTransaksi.txt_NamaLawanTransaksi.Text = NamaLawanTransaksi
        frm_InputTransaksi.txt_JumlahTransaksi.Text = JumlahTransaksi
        frm_InputTransaksi.JumlahMutasiBankCash = JumlahTransaksi
        frm_InputTransaksi.dtp_TanggalTransaksi.Value = dtp_TanggalTransaksi.Value
        frm_InputTransaksi.txt_UraianTransaksi.Text = Uraian
        frm_InputTransaksi.cmb_JenisJurnal.Text = Kosongan  'Ini dibikin kosong, 
        frm_InputTransaksi.cmb_JenisJurnal.Enabled = True   'dan dibikin tersedia untuk dipilih, karena Jenis Jurnal pada kasus ini tidak mutlak mengikuti Sarana Pembayaran
        frm_InputTransaksi.ShowDialog()

        If frm_InputTransaksi.PenyimpananSukses = True Then
            If usc_BukuBesar.StatusAktif = True Then usc_BukuBesar.TampilkanData()
            Me.Close()
        End If

    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
        End If

        If FungsiForm = FungsiForm_EDIT Then
            jur_NomorJV = NomorJV
            HapusJurnal_BerdasarkanNomorJV(jur_NomorJV)
        End If

        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
        jur_JenisJurnal = KonversiCOAKeSaranaPembayaran(KodeTautanCOA_PettyCashAdministrasi)
        jur_UraianTransaksi = Uraian
        jur_NamaProduk = NamaProduk
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTransaksi
        jur_Direct = 1

        'Simpan Jurnal :
        ___jurDebet(COALawan, JumlahTransaksi)
        _______jurKredit(COAUtama, JumlahTransaksi)

        If jur_StatusPenyimpananJurnal_Lengkap = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan_PlusJurnal()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                pesan_DataGagalDisimpan()
            Else
                PesanPeringatan("Data GAGAL diperbarui..!")
            End If
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Close()
    End Sub

End Class