Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc

Public Class wpfWin_InputBuktiPotongPPh

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk
    Public JalurMasuk_Paid = "Paid"
    Public JalurMasuk_Prepaid = "Prepaid"

    Public NomorID
    Public SumberData
    Public NomorJV_BuktiPotong

    'Konstanta Sumber Data (Prepaid):
    Public Const SumberData_Penjualan = "Penjualan"
    Public Const SumberData_PencairanPiutangUsaha = "Piutang Usaha"
    Public Const SumberData_PencairanPiutangPihakKetiga = "Piutang Pihak Ketiga"
    Public Const SumberData_PencairanPiutangAfiliasi = "Piutang Afiliasi"
    Public Const SumberData_PiutangDividen = "Piutang Dividen"
    'Konstanta Sumber Data (Paid):
    Public Const SumberData_Pembelian = "Pembelian"
    Public Const SumberData_PembayaranHutangUsaha = "Hutang Usaha"
    Public Const SumberData_PembayaranHutangPihakKetiga = "Hutang Pihak Ketiga"
    Public Const SumberData_PembayaranHutangAfiliasi = "Hutang Afiliasi"
    Public Const SumberData_PembayaranHutangBank = "Hutang Bank"
    Public Const SumberData_PembayaranHutangLeasing = "Hutang Leasing"
    Public Const SumberData_HutangDividen = "Hutang Dividen"

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


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

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

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan

        JalurMasuk = Kosongan

        NomorID = 0
        NomorJV_BuktiPotong = 0

        txt_NomorInvoice.Text = Kosongan
        dtp_TanggalInvoice.SelectedDate = Nothing
        txt_NomorFakturPajak.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_DPP.Text = Kosongan
        txt_PPhTerutang.Text = Kosongan
        txt_NomorBuktiPotong.Text = Kosongan
        dtp_TanggalBuktiPotong.SelectedDate = TanggalFormatWPF(TanggalIni)
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        ProsesResetForm = False

    End Sub


    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub

    Private Sub dtp_TanggalInvoice_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalInvoice.SelectedDateChanged
        If dtp_TanggalInvoice.Text <> Kosongan Then
            TanggalInvoice = TanggalFormatTampilan(dtp_TanggalInvoice.SelectedDate)
        End If
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub

    Private Sub txt_DPP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPP.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_DPP)
    End Sub

    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PPhTerutang.TextChanged
        JumlahPPhTerutang = AmbilAngka(txt_PPhTerutang.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhTerutang)
    End Sub

    Private Sub txt_JenisPPh_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JenisPPh.TextChanged
        JenisPPh = txt_JenisPPh.Text
    End Sub

    Private Sub txt_NomorBuktiPotong_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBuktiPotong.TextChanged
        NomorBuktiPotong = txt_NomorBuktiPotong.Text
    End Sub

    Private Sub dtp_TanggalBuktiPotong_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalBuktiPotong.SelectedDateChanged
        If dtp_TanggalBuktiPotong.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalBuktiPotong)
            TanggalBuktiPotong = dtp_TanggalBuktiPotong.SelectedDate
        End If
    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        'Pengisian ulang value :
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)

        If NomorBuktiPotong = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NomorBuktiPotong, "Nomor Bukti Potong")
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
            Case SumberData_Penjualan
                QueryAwal = "  UPDATE tbl_Penjualan_Invoice "
                QueryAkhir = " WHERE Nomor_Invoice     = '" & NomorInvoice & "' "
            Case SumberData_PencairanPiutangUsaha
                QueryAwal = "  UPDATE tbl_BuktiPenerimaan "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_PencairanPiutangPihakKetiga
                QueryAwal = "  UPDATE tbl_JadwalAngsuranPiutangPihakKetiga "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_PencairanPiutangAfiliasi
                QueryAwal = "  UPDATE tbl_JadwalAngsuranPiutangAfiliasi "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_PiutangDividen
                QueryAwal = "  UPDATE tbl_PengawasanPiutangDividen "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_Pembelian
                QueryAwal = "  UPDATE tbl_Pembelian_Invoice "
                QueryAkhir = " WHERE Nomor_Invoice     = '" & NomorInvoice & "' "
            Case SumberData_PembayaranHutangUsaha
                QueryAwal = "  UPDATE tbl_BuktiPengeluaran "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_PembayaranHutangPihakKetiga
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangPihakKetiga "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_PembayaranHutangAfiliasi
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangAfiliasi "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_PembayaranHutangBank
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangBank "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_PembayaranHutangLeasing
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangLeasing "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID & "' "
            Case SumberData_HutangDividen
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


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        ResetForm()
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
    End Sub

End Class
