Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputHutangPajak

    Public FungsiForm
    Public JalurMasuk
    Public JudulForm
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
    Public JenisPajak As String
    Public KodeSetoran As String
    Dim JumlahHutang
    Dim Keterangan


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Hutang Pajak - " & JenisPajak
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Hutang Pajak - " & JenisPajak
        End If

        Title = JudulForm

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        KosongkanDatePicker(dtp_TanggalTransaksi)
        KosongkanDatePicker(dtp_TanggalInvoice)
        txt_NomorInvoice.Text = Kosongan
        txt_NomorFakturPajak.Text = Kosongan
        txt_NamaJasa.Text = Kosongan
        txt_KodeSupplier.Text = Kosongan
        txt_NamaSupplier.Text = Kosongan
        txt_NPWP.Text = Kosongan
        txt_DPP.Text = Kosongan
        KontenCombo_JenisPajak()
        txt_JumlahHutang.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        ProsesResetForm = False

    End Sub


    Sub KontenCombo_JenisPajak()
        cmb_JenisPajak.Items.Clear()
        cmb_JenisPajak.Items.Add(JenisPajak_PPhPasal21)
        cmb_JenisPajak.Items.Add(JenisPajak_PPhPasal23)
        cmb_JenisPajak.Items.Add(JenisPajak_PPhPasal25)
        cmb_JenisPajak.Items.Add(JenisPajak_PPhPasal26)
        cmb_JenisPajak.Items.Add(JenisPajak_PPhPasal42)
        cmb_JenisPajak.Text = Kosongan
    End Sub


    Sub KontenCombo_KodeSetoran()
        VisibilitasKodeSetoran(True)
        KontenCombo_KodeSetoran_Public_WPF(cmb_KodeSetoran, JenisPajak)
        cmb_KodeSetoran.IsEnabled = True
        If JenisPajak = JenisPajak_PPhPasal21 Then IsiValueComboBypassTerkunci(cmb_KodeSetoran, KodeSetoran_100) 'Khusus PPh Pasal 21, tidak menampilkan Kode-401, sebab Kode-401 (Pesangon) diinput di Form Gaji dan Pesangon.
        If JenisPajak = JenisPajak_PPN _
            Or JenisPajak = JenisPajak_PPhPasal25 _
            Or JenisPajak = JenisPajak_KetetapanPajak _
            Then
            VisibilitasKodeSetoran(False)
        End If
    End Sub

    Sub VisibilitasKodeSetoran(Visibilitas As Boolean)
        If Visibilitas Then
            cmb_KodeSetoran.Visibility = Visibility.Visible
        Else
            cmb_KodeSetoran.Visibility = Visibility.Collapsed
            cmb_KodeSetoran.Items.Clear()
        End If

    End Sub


    Private Sub dtp_TanggalTransaksi_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalTransaksi.SelectedDateChanged
        If dtp_TanggalTransaksi.Text <> Kosongan Then
            KunciTahun_TidakBolehLebihDariTahunBukuAktif_WPF(dtp_TanggalTransaksi)
            TanggalTransaksi = dtp_TanggalTransaksi.SelectedDate
            BulanTransaksi = dtp_TanggalTransaksi.SelectedDate.Value.Month
            TahunTransaksi = dtp_TanggalTransaksi.SelectedDate.Value.Year
        End If
    End Sub

    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalInvoice.SelectedDateChanged
        If dtp_TanggalInvoice.Text <> Kosongan Then
            If dtp_TanggalTransaksi.Text = Kosongan Then
                dtp_TanggalInvoice.Text = Kosongan
                PesanPeringatan("Silakan isi terlebih dahulu 'Tanggal Transaksi'.")
                dtp_TanggalTransaksi.Focus()
                Return
            End If
            KunciTanggalBulanDanTahun_TidakBolehLebihDari_WPF(dtp_TanggalInvoice, dtp_TanggalTransaksi.SelectedDate.Value.Day, BulanTransaksi, TahunTransaksi)
            TanggalInvoice = dtp_TanggalInvoice.SelectedDate.Value
        End If
    End Sub


    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub

    Private Sub txt_NamaJasa_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaJasa.TextChanged
        NamaJasa = txt_NamaJasa.Text
    End Sub


    Private Sub txt_KodeSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeSupplier.TextChanged
        KodeSupplier = txt_KodeSupplier.Text
        txt_NamaSupplier.Text = AmbilValue_NamaMitra(KodeSupplier)
        txt_NPWP.Text = AmbilValue_NPWPMitra(KodeSupplier)
        If KodeSupplier <> Kosongan And NamaSupplier = Kosongan Then txt_NamaSupplier.Text = win_ListLawanTransaksi.NamaMitraTerseleksi
    End Sub
    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        BukaFormListLawanTransaksi(txt_KodeSupplier, txt_NamaSupplier, Mitra_Supplier, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Tidak)
    End Sub
    Private Sub txt_NamaSupplier_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaSupplier.TextChanged
        NamaSupplier = txt_NamaSupplier.Text
    End Sub
    Private Sub txt_NPWP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NPWP.TextChanged
        NPWP = txt_NPWP.Text
    End Sub

    Private Sub txt_DPP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DPP.TextChanged
        DPP = AmbilAngka(txt_DPP.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPP)
    End Sub
    Private Sub txt_DPP_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DPP.PreviewTextInput

    End Sub

    Private Sub cmb_JenisPajak_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisPajak.SelectionChanged
        JenisPajak = cmb_JenisPajak.SelectedValue
        KontenCombo_KodeSetoran()
    End Sub

    Private Sub cmb_KodeSetoran_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KodeSetoran.SelectionChanged
        KodeSetoran = AmbilAngka(cmb_KodeSetoran.SelectedValue)
    End Sub

    Private Sub txt_JumlahHutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahHutang.TextChanged
        JumlahHutang = AmbilAngka(txt_JumlahHutang.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahHutang)
    End Sub
    Private Sub txt_JumlahHutang_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahHutang.PreviewTextInput

    End Sub

    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_Simpan.Click

        If dtp_TanggalTransaksi.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalTransaksi, "Tanggal Transaksi")
            Return
        End If

        If dtp_TanggalInvoice.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalInvoice, "Tanggal Invoice")
            Return
        End If

        If NamaJasa = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NamaJasa, "Nama Jasa/Sewa")
            Return
        End If

        If KodeSupplier = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_KodeSupplier, "Lawan Transaksi")
            Return
        End If

        If DPP = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_DPP, "Jumlah DPP")
            Return
        End If

        If JenisPajak = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_JenisPajak, "Jenis Pajak")
            Return
        End If

        If cmb_KodeSetoran.Visibility = Visibility.Visible And KodeSetoran = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_KodeSetoran, "Kode Setoran")
            Return
        End If

        If JumlahHutang = 0 Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_JumlahHutang, "Jumlah Pajak")
            Return
        End If

        ProsesPenyimpanan = True

        Dim QueryPenyimpanan

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorId = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_HutangPajak") + 1

            QueryPenyimpanan =
                " INSERT INTO tbl_HutangPajak VALUES ( " &
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
                " '" & JenisPajak & "', " &
                " '" & KodeSetoran & "', " &
                " '" & JumlahHutang & "', " &
                " '" & Keterangan & "', " &
                " '" & 0 & "', " &
                " '" & UserAktif & "' ) "

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then

            QueryPenyimpanan =
                " UPDATE tbl_HutangPajak SET " &
                " Tanggal_Transaksi     = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                " Tanggal_Invoice       = '" & TanggalFormatSimpan(TanggalInvoice) & "', " &
                " Nomor_Invoice         = '" & NomorInvoice & "', " &
                " Nomor_Faktur_Pajak    = '" & NomorFakturPajak & "', " &
                " Nama_Jasa             = '" & NamaJasa & "', " &
                " NPWP                  = '" & NPWP & "', " &
                " Kode_Supplier         = '" & KodeSupplier & "', " &
                " Nama_Supplier         = '" & NamaSupplier & "', " &
                " DPP                   = '" & DPP & "', " &
                " Jenis_Pajak           = '" & JenisPajak & "', " &
                " Kode_Setoran          = '" & KodeSetoran & "', " &
                " Jumlah_Hutang         = '" & JumlahHutang & "', " &
                " Keterangan            = '" & Keterangan & "', " &
                " Nomor_JV              = '" & 0 & "', " &
                " User                  = '" & UserAktif & "' " &
                " WHERE Nomor_ID        = '" & NomorId & "' "

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryPenyimpanan, KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

        End If

        If StatusSuntingDatabase = True Then
            ResetForm()
            If usc_BukuPengawasanHutangPPhPasal21.StatusAktif Then usc_BukuPengawasanHutangPPhPasal21.TampilkanData()
            If usc_BukuPengawasanHutangPPhPasal23.StatusAktif Then usc_BukuPengawasanHutangPPhPasal23.TampilkanData()
            If usc_BukuPengawasanHutangPPhPasal25.StatusAktif Then usc_BukuPengawasanHutangPPhPasal25.TampilkanData()
            If usc_BukuPengawasanHutangPPhPasal26.StatusAktif Then usc_BukuPengawasanHutangPPhPasal26.TampilkanData()
            If usc_BukuPengawasanHutangPPhPasal42.StatusAktif Then usc_BukuPengawasanHutangPPhPasal42.TampilkanData()
            If FungsiForm = FungsiForm_TAMBAH Then Pesan_Sukses("Data berhasil disimpan.")
            If FungsiForm = FungsiForm_EDIT Then Pesan_Sukses("Data berhasil diedit.")
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then Pesan_Peringatan("Data gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            If FungsiForm = FungsiForm_EDIT Then Pesan_Peringatan("Data gagal diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_KodeSupplier.IsReadOnly = True
        txt_NamaSupplier.IsReadOnly = True
        txt_NPWP.IsReadOnly = True
        cmb_JenisPajak.IsReadOnly = True
        cmb_KodeSetoran.IsReadOnly = True
    End Sub

End Class
