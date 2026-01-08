Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputBuktiPotongPPh

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk
    Public JalurMasuk_Paid = "Paid"
    Public JalurMasuk_Prepaid = "Prepaid"

    Public NomorID
    Public SumberData
    Public NomorJV_BuktiPotong

    Dim NomorInvoice
    Dim TanggalInvoice
    Dim NomorFakturPajak
    Public KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JenisPPh
    Dim JumlahPPhTerutang
    Dim NomorBuktiPotong
    Dim TanggalBuktiPotong
    Dim Keterangan

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Bukti Potong PPh Dibayar Dimuka "
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Bukti Potong PPh Dibayar Dimuka "
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")
        If JalurMasuk = Kosongan Then PesanUntukProgrammer("Jalur Masuk belum ditentukan...!!!")

        If JalurMasuk = JalurMasuk_Paid Then
            JudulForm &= "(21, 23, 4 (2), 26)"
            lbl_Mitra.Text = "Supplier"
        End If
        If JalurMasuk = JalurMasuk_Prepaid Then
            JudulForm &= "(Prepaid)"
            lbl_Mitra.Text = "Customer"
        End If


        Me.Text = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        JalurMasuk = Kosongan

        NomorID = 0
        NomorJV_BuktiPotong = 0

        txt_NomorInvoice.Text = Kosongan
        dtp_TanggalInvoice.Value = TanggalIni
        txt_NomorFakturPajak.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_DPP.Text = Kosongan
        txt_PPhTerutang.Text = Kosongan
        txt_NomorBuktiPotong.Text = Kosongan
        dtp_TanggalBuktiPotong.Value = TanggalIni
        txt_Keterangan.Text = Kosongan

        ProsesResetForm = False

    End Sub


    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub

    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalInvoice.ValueChanged
        TanggalInvoice = TanggalFormatTampilan(dtp_TanggalInvoice.Value)
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub

    Private Sub txt_DPP_TextChanged(sender As Object, e As EventArgs) Handles txt_DPP.TextChanged
        PemecahRibuanUntukTextBox(txt_DPP)
    End Sub
    Private Sub txt_DPP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhTerutang.TextChanged
        JumlahPPhTerutang = AmbilAngka(txt_PPhTerutang.Text)
        PemecahRibuanUntukTextBox(txt_PPhTerutang)
    End Sub
    Private Sub txt_PPhDipotong_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhTerutang.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JenisPPh_TextChanged(sender As Object, e As EventArgs) Handles txt_JenisPPh.TextChanged
        JenisPPh = txt_JenisPPh.Text
    End Sub

    Private Sub txt_NomorBuktiPotong_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorBuktiPotong.TextChanged
        NomorBuktiPotong = txt_NomorBuktiPotong.Text
    End Sub

    Private Sub dtp_TanggalBuktiPotong_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalBuktiPotong.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_TanggalBuktiPotong)
        TanggalBuktiPotong = dtp_TanggalBuktiPotong.Value
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = txt_Keterangan.Text
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Pengisian ulang value :
        Keterangan = txt_Keterangan.Text

        If NomorBuktiPotong = Kosongan Then
            PesanUmum("Silakan isi kolom 'Nomor Bukti Potong'.")
            txt_NomorBuktiPotong.Focus()
            Return
        End If

        If JalurMasuk = JalurMasuk_Prepaid Then

            If FungsiForm = FungsiForm_TAMBAH Then
                SistemPenomoranOtomatis_NomorJV()
                NomorJV_BuktiPotong = jur_NomorJV
            End If

            If FungsiForm = FungsiForm_EDIT Then
                'Hapus Jurnal yang lama :
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_BuktiPotong & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
                jur_NomorJV = NomorJV_BuktiPotong
            End If

        End If

        Dim QueryAwal = Kosongan
        Dim QueryAkhir = Kosongan
        Select Case SumberData
            Case usc_BukuPengawasanBuktiPotongPPh_Prepaid.SumberData_Penjualan
                QueryAwal = "  UPDATE tbl_Penjualan_Invoice "
                QueryAkhir = " WHERE Nomor_Invoice     = '" & NomorInvoice & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Prepaid.SumberData_PencairanPiutangUsaha
                QueryAwal = "  UPDATE tbl_BuktiPenerimaan "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Prepaid.SumberData_PencairanPiutangPihakKetiga
                QueryAwal = "  UPDATE tbl_JadwalAngsuranPiutangPihakKetiga "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Prepaid.SumberData_PencairanPiutangAfiliasi
                QueryAwal = "  UPDATE tbl_JadwalAngsuranPiutangAfiliasi "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Prepaid.SumberData_PiutangDividen
                QueryAwal = "  UPDATE tbl_PengawasanPiutangDividen "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Paid.SumberData_Pembelian
                QueryAwal = "  UPDATE tbl_Pembelian_Invoice "
                QueryAkhir = " WHERE Nomor_Invoice     = '" & NomorInvoice & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Paid.SumberData_PembayaranHutangUsaha
                QueryAwal = "  UPDATE tbl_BuktiPengeluaran "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Paid.SumberData_PembayaranHutangPihakKetiga
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangPihakKetiga "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Paid.SumberData_PembayaranHutangAfiliasi
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangAfiliasi "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Paid.SumberData_PembayaranHutangBank
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangBank "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Paid.SumberData_PembayaranHutangLeasing
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangLeasing "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case usc_BukuPengawasanBuktiPotongPPh_Paid.SumberData_HutangDividen
                QueryAwal = "  UPDATE tbl_PengawasanHutangDividen "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
        End Select
        Dim KolomKolomYangDiedit = " SET " &
            " Nomor_Bukti_Potong      = '" & NomorBuktiPotong & "', " &
            " Tanggal_Bukti_Potong    = '" & TanggalFormatSimpan(TanggalBuktiPotong) & "', " &
            " Keterangan_Bukti_Potong = '" & Keterangan & "', " &
            " Nomor_JV_Bukti_Potong   = '" & NomorJV_BuktiPotong & "' "

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryAwal & KolomKolomYangDiedit & QueryAkhir, KoneksiDatabaseTransaksi)

        cmd_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        '=================== SIMPAN JURNAL ===================
        If JenisTahunBuku = JenisTahunBuku_NORMAL _
            And JalurMasuk = JalurMasuk_Prepaid Then

            ResetValueJurnal()
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalBuktiPotong)
            jur_JenisJurnal = JenisJurnal_CekBupot
            jur_KodeDokumen = Kosongan
            jur_NomorPO = Kosongan
            jur_KodeProject = Kosongan
            jur_Referensi = NomorBuktiPotong
            jur_TanggalInvoice = TanggalInvoice
            jur_NomorInvoice = NomorInvoice
            jur_NomorFakturPajak = NomorFakturPajak
            jur_KodeLawanTransaksi = KodeLawanTransaksi
            jur_NamaLawanTransaksi = NamaLawanTransaksi
            jur_UraianTransaksi = Keterangan
            jur_Direct = 0

            'Jurnal :
            ___jurDebet(PenentuanCOA_PPhDibayarDimuka(JenisPPh), JumlahPPhTerutang)
            _______jurKredit(PenentuanCOA_PPhDibayarDimuka_BP_BelumDiterima(JenisPPh), JumlahPPhTerutang)

        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then pesan_DataBerhasilDisimpan()
            If FungsiForm = FungsiForm_EDIT Then pesan_DataTerpilihBerhasilDiperbarui()
            If JalurMasuk = JalurMasuk_Paid Then usc_BukuPengawasanBuktiPotongPPh_Paid.TampilkanData()
            If JalurMasuk = JalurMasuk_Prepaid Then usc_BukuPengawasanBuktiPotongPPh_Prepaid.TampilkanData()
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