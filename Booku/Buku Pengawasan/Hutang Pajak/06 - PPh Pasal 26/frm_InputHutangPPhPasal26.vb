Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputHutangPPhPasal26

    Public FungsiForm
    Public JalurMasuk
    Public NomorId

    Dim ProsesPenyimpanan As Boolean

    Public BulanTransaksi
    Dim TahunTransaksi
    Dim TanggalTransaksi
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim NamaJasa
    Dim KodeSupplier
    Dim NamaSupplier
    Dim NPWP
    Dim DPP
    Dim PPhPasal26
    Dim Keterangan

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        BeginInvoke(Sub() Style_FormInput(Me))

        If FungsiForm = FungsiForm_TAMBAH Then Me.Text = "Input Hutang PPh Pasal 26"
        If FungsiForm = FungsiForm_EDIT Then Me.Text = "Edit Hutang PPh Pasal 26"

        ProsesLoadingForm = False

    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        dtp_TanggalTransaksi.Value = Today
        dtp_TanggalInvoice.Value = Today
        txt_NomorInvoice.Text = Nothing
        txt_NomorFakturPajak.Text = Nothing
        txt_NamaJasa.Text = Nothing
        txt_KodeSupplier.Text = Nothing
        txt_NamaSupplier.Text = Nothing
        txt_NPWP.Text = Nothing
        txt_DPP.Text = Nothing
        txt_PPhPasal26.Text = Nothing
        txt_Keterangan.Text = Nothing

        ProsesResetForm = False

    End Sub

    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalTransaksi.ValueChanged
        KunciTahun_TidakBolehLebihDariTahunBukuAktif(dtp_TanggalTransaksi)
        'If FungsiForm = FungsiForm_TAMBAH _
        '    And frm_BukuPengawasanHutangPPhPasal26.JenisTampilan = frm_BukuPengawasanHutangPPhPasal26.JenisTampilan_DETAIL _
        '    Then
        '    KunciBulanDanTahun_HarusSama(dtp_TanggalTransaksi, BulanTransaksi, TahunPajak)
        'End If
        TanggalTransaksi = dtp_TanggalTransaksi.Value
        BulanTransaksi = dtp_TanggalTransaksi.Value.Month
        TahunTransaksi = dtp_TanggalTransaksi.Value.Year
        KunciTanggalBulanDanTahun_TidakBolehLebihDari(dtp_TanggalInvoice, dtp_TanggalTransaksi.Value.Day, BulanTransaksi, TahunTransaksi)
    End Sub

    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalInvoice.ValueChanged
        KunciTanggalBulanDanTahun_TidakBolehLebihDari(dtp_TanggalInvoice, dtp_TanggalTransaksi.Value.Day, BulanTransaksi, TahunTransaksi)
        TanggalInvoice = dtp_TanggalInvoice.Value
    End Sub

    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub txt_NamaJasa_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaJasa.TextChanged
        NamaJasa = txt_NamaJasa.Text
    End Sub

    Private Sub txt_KodeSupplier_Click(sender As Object, e As EventArgs) Handles txt_KodeSupplier.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeSupplier.TextChanged
        KodeSupplier = txt_KodeSupplier.Text
    End Sub
    Private Sub txt_KodeSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeSupplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Supplier
        If txt_KodeSupplier.Text = Nothing Then
            frm_ListMitra.ResetForm()
        Else
            frm_ListMitra.KodeMitraTerseleksi = txt_KodeSupplier.Text
            frm_ListMitra.NamaMitraTerseleksi = txt_NamaSupplier.Text
            frm_ListMitra.NPWPTerseleksi = txt_NPWP.Text
        End If
        frm_ListMitra.ShowDialog()
        txt_KodeSupplier.Text = frm_ListMitra.KodeMitraTerseleksi
        txt_NamaSupplier.Text = frm_ListMitra.NamaMitraTerseleksi
        txt_NPWP.Text = frm_ListMitra.NPWPTerseleksi
    End Sub

    Private Sub txt_NamaSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaSupplier.TextChanged
        NamaSupplier = txt_NamaSupplier.Text
    End Sub
    Private Sub txt_NamaSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaSupplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_NPWP_TextChanged(sender As Object, e As EventArgs) Handles txt_NPWP.TextChanged
        NPWP = txt_NPWP.Text
    End Sub
    Private Sub txt_NPWP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NPWP.KeyPress
        KunciTotalInputan(sender, e)
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_DPP_TextChanged(sender As Object, e As EventArgs) Handles txt_DPP.TextChanged
        DPP = AmbilAngka(txt_DPP.Text)
        PemecahRibuanUntukTextBox(txt_DPP)
    End Sub

    Private Sub txt_PPhPasal26_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhPasal26.TextChanged
        PPhPasal26 = AmbilAngka(txt_PPhPasal26.Text)
        PemecahRibuanUntukTextBox(txt_PPhPasal26)
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'TrialBalance_Mentahkan()

        ProsesPenyimpanan = True

        Dim QueryPenyimpanan

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorId = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_SisaHutangPPhPasal26") + 1

            QueryPenyimpanan = " INSERT INTO tbl_SisaHutangPPhPasal26 VALUES ( " &
                " '" & NomorId & "', " &
                " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                " '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                " '" & NomorInvoice & "', " &
                " '" & NomorFakturPajak & "', " &
                " '" & NamaJasa & "', " &
                " '" & KodeSupplier & "', " &
                " '" & NamaSupplier & "', " &
                " '" & NPWP & "', " &
                " '" & DPP & "', " &
                " '" & PPhPasal26 & "', " &
                " '" & Keterangan & "', " &
                " '" & UserAktif & "' ) "

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            Try
                cmd.ExecuteNonQuery()
                ProsesPenyimpanan = True
            Catch ex As Exception
                ProsesPenyimpanan = False
            End Try
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            QueryPenyimpanan = " UPDATE tbl_SisaHutangPPhPasal26 SET " &
                " Tanggal_Transaksi     = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                " Tanggal_Invoice       = '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                " Nomor_Invoice         = '" & NomorInvoice & "', " &
                " Nomor_Faktur_Pajak    = '" & NomorFakturPajak & "', " &
                " Nama_Jasa             = '" & NamaJasa & "', " &
                " NPWP                  = '" & NPWP & "', " &
                " Kode_Supplier         = '" & KodeSupplier & "', " &
                " Nama_Supplier         = '" & NamaSupplier & "', " &
                " DPP                   = '" & DPP & "', " &
                " PPh_Pasal_26          = '" & PPhPasal26 & "', " &
                " Keterangan            = '" & Keterangan & "', " &
                " User                  = '" & UserAktif & "' " &
                " WHERE Nomor_ID        = '" & NomorId & "' "

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            Try
                cmd.ExecuteNonQuery()
                ProsesPenyimpanan = True
            Catch ex As Exception
                ProsesPenyimpanan = False
            End Try
            AksesDatabase_Transaksi(Tutup)

        End If

        If ProsesPenyimpanan = True Then
            ResetForm()
            usc_BukuPengawasanHutangPPhPasal26.TampilkanData()
            If FungsiForm = FungsiForm_TAMBAH Then MsgBox("Data BERHASIL Disimpan.")
            If FungsiForm = FungsiForm_EDIT Then MsgBox("Data BERHASIL Diedit.")
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then MsgBox("Data GAGAL Disimpan!" & Enter2Baris & teks_SilakanCobaLagi_Database)
            If FungsiForm = FungsiForm_EDIT Then MsgBox("Data GAGAL Diedit!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub

End Class