Imports System.Data.Odbc
Imports bcomm

Public Class frm_InputLaporPPN

    Public FungsiForm
    Public NomorID
    Dim TanggalLapor
    Dim BulanAngka
    Dim TanggalPelunasan_Date As Date
    Public MasaPajak
    Public NP
    Public JumlahLebihBayar
    Public KompensasiKe_Bulan
    Public KompensasiKe_Tahun

    Private Sub frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If NP = "N" Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangPajak " &
                                  " WHERE Masa_Pajak = '" & MasaPajak & "' " &
                                  " ORDER BY Tanggal_Bayar ASC ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read 'Untuk mendeteksi Pembayaran Terakhir
                TanggalPelunasan_Date = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            Loop
            AksesDatabase_Transaksi(Tutup)
            Nonaktifkan_KolomKompensasiKe()
        Else
            Aktifkan_KolomKompensasiKe()
            If FungsiForm = FungsiForm_EDIT Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanPelaporanPajak " &
                                      " WHERE Bulan = '" & BulanAngka & "' " &
                                      " AND N_P = '" & NP & "' ", KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                dr.Read()
                If dr.HasRows Then
                    KompensasiKe_Bulan = dr.Item("Kompensasi_Ke_Bulan")
                    KompensasiKe_Tahun = dr.Item("Kompensasi_Ke_Tahun")
                    If KompensasiKe_Tahun = 0 Then KompensasiKe_Tahun = Kosongan
                    cmb_KompensasiKe_Bulan.Text = KompensasiKe_Bulan
                    cmb_KompensasiKe_Tahun.Text = KompensasiKe_Tahun
                End If
                AksesDatabase_Transaksi(Tutup)
            End If
            PesanUntukProgrammer("Logika 'Kuncian Tanggal' untuk 'P' (Pembetulan) belum dibikin...!!!")
        End If

        If JumlahLebihBayar <= 0 Then Nonaktifkan_KolomKompensasiKe()

    End Sub

    Sub ResetForm()

        ProsesResetForm = True

        txt_MasaPajak.Text = Kosongan
        dtp_TanggalLapor.Value = Today
        txt_JumlahLebihBayar.Text = Kosongan
        NomorID = 0
        BulanAngka = 0
        TanggalPelunasan_Date = TanggalKosong
        NP = Kosongan

        ProsesResetForm = False

    End Sub

    Sub KontenCombo_KompensasiKe_Bulan()

        cmb_KompensasiKe_Bulan.Items.Clear()
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Januari)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Februari)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Maret)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_April)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Mei)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Juni)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Juli)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Agustus)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_September)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Oktober)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Nopember)
        cmb_KompensasiKe_Bulan.Items.Add(Bulan_Desember)
        cmb_KompensasiKe_Bulan.Text = Bulan_Januari

    End Sub

    Sub KontenCombo_KompensasiKe_Tahun()
        cmb_KompensasiKe_Tahun.Items.Clear()
        cmb_KompensasiKe_Tahun.Items.Add(TahunPajak)
        cmb_KompensasiKe_Tahun.Items.Add(TahunPajak + 1)
        cmb_KompensasiKe_Tahun.Text = TahunPajak
    End Sub

    Sub Aktifkan_KolomKompensasiKe()
        lbl_JumlahLebihBayar.Enabled = True
        txt_JumlahLebihBayar.Enabled = True
        lbl_KompensasiKe.Enabled = True
        cmb_KompensasiKe_Bulan.Enabled = True
        cmb_KompensasiKe_Tahun.Enabled = True
        KontenCombo_KompensasiKe_Bulan()
        KontenCombo_KompensasiKe_Tahun()
    End Sub

    Sub Nonaktifkan_KolomKompensasiKe()
        lbl_JumlahLebihBayar.Enabled = False
        txt_JumlahLebihBayar.Enabled = False
        txt_JumlahLebihBayar.Text = Kosongan
        lbl_KompensasiKe.Enabled = False
        cmb_KompensasiKe_Bulan.Enabled = False
        cmb_KompensasiKe_Bulan.Text = Kosongan
        cmb_KompensasiKe_Tahun.Enabled = False
        cmb_KompensasiKe_Tahun.Text = Kosongan
    End Sub

    Private Sub txt_MasaPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_MasaPajak.TextChanged
        MasaPajak = txt_MasaPajak.Text
        BulanAngka = AmbilAngka(KonversiBulanKeNomor_String(MasaPajak))
    End Sub
    Private Sub txt_MasaPajak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_MasaPajak.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub dtp_TanggalLapor_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalLapor.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalLapor)
        TanggalLapor = TanggalFormatTampilan(dtp_TanggalLapor.Value)
    End Sub

    Private Sub txt_JumlahLebihBayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahLebihBayar.TextChanged
        JumlahLebihBayar = AmbilAngka(txt_JumlahLebihBayar.Text)
        PemecahRibuanUntukTextBox(txt_JumlahLebihBayar)
    End Sub
    Private Sub txt_JumlahLebihBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahLebihBayar.KeyPress
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub cmb_KompensasiKe_Bulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_KompensasiKe_Bulan.SelectedIndexChanged
    End Sub
    Private Sub cmb_KompensasiKe_Bulan_TextChanged(sender As Object, e As EventArgs) Handles cmb_KompensasiKe_Bulan.TextChanged
        KompensasiKe_Bulan = cmb_KompensasiKe_Bulan.Text
    End Sub

    Private Sub cmb_KompensasiKe_Tahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_KompensasiKe_Tahun.SelectedIndexChanged
    End Sub
    Private Sub cmb_KompensasiKe_Tahun_TextChanged(sender As Object, e As EventArgs) Handles cmb_KompensasiKe_Tahun.TextChanged
        If cmb_KompensasiKe_Tahun.Text = Kosongan Then
            KompensasiKe_Tahun = 0
        Else
            KompensasiKe_Tahun = cmb_KompensasiKe_Tahun.Text
        End If
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        Dim TanggalLapor_Date As Date = TanggalLapor
        If TanggalLapor_Date < TanggalPelunasan_Date Then
            MsgBox("'Tanggal Lapor' kurang dari 'Tanggal Pelunasan Pajak'." & Enter2Baris & "Silakan isi 'Tanggal Lapor' dengan benar..!")
            dtp_TanggalLapor.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PengawasanPelaporanPajak") + 1

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_PengawasanPelaporanPajak VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & BulanAngka & "', " &
                                  " '" & TanggalFormatSimpan(TanggalLapor) & "', " &
                                  " '" & NP & "', " &
                                  " '" & JumlahLebihBayar & "', " &
                                  " '" & KompensasiKe_Bulan & "', " &
                                  " '" & KompensasiKe_Tahun & "' " &
                                  " ) ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_PengawasanPelaporanPajak SET " &
                                  " Bulan               = '" & BulanAngka & "', " &
                                  " Tanggal_Lapor       = '" & TanggalFormatSimpan(TanggalLapor) & "', " &
                                  " N_P                 = '" & NP & "', " &
                                  " Jumlah_Lebih_Bayar  = '" & JumlahLebihBayar & "', " &
                                  " Kompensasi_Ke_Bulan = '" & KompensasiKe_Bulan & "', " &
                                  " Kompensasi_Ke_Tahun = '" & KompensasiKe_Tahun & "'  " &
                                  " WHERE Nomor_ID      = '" & NomorID & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then
            pesan_DataBerhasilDisimpan()
            If usc_BukuPengawasanPelaporanPPN.StatusAktif Then usc_BukuPengawasanPelaporanPPN.RefreshTampilanData()
            Me.Close()
        Else
            pesan_DataGagalDisimpan()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

End Class