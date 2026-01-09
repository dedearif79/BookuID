Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputLaporPajak

    Public FungsiForm
    Public NomorID
    Public JenisPajak
    Dim TanggalLapor
    Dim BulanAngka
    Dim TanggalPelunasan_Date As Date
    Public MasaPajak
    Public NP
    Public JumlahLebihBayar
    Public KompensasiKe_Bulan
    Public KompensasiKe_Tahun

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")
        If JenisPajak = Kosongan Then PesanUntukProgrammer("Jenis Pajak belum ditentukan...!!!")

        Dim AwalanBP = Kosongan

        Select Case JenisPajak
            Case JenisPajak_PPN
                AwalanBP = AwalanBPHPPN
            Case JenisPajak_PPhPasal21
                AwalanBP = AwalanBPHP21
            Case JenisPajak_PPhPasal23
                AwalanBP = AwalanBPHP23
            Case JenisPajak_PPhPasal25
                AwalanBP = AwalanBPHP25
            Case JenisPajak_PPhPasal26
                AwalanBP = AwalanBPHP26
            Case JenisPajak_PPhPasal42
                AwalanBP = AwalanBPHP42
        End Select

        If AwalanBP = Kosongan Then PesanUntukProgrammer("Awalan BP belum ditentukan...!!!")

        If NP = "N" Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                  " WHERE   Nomor_BP LIKE   '" & AwalanBP & "%' " &
                                  " AND     Nomor_BP LIKE   '%" & KonversiBulanKeAngka(MasaPajak) & "' " &
                                  " AND     Jenis_Pajak     = '" & JenisPajak & "' " &
                                  " AND     Status_Invoice  = '" & Status_Dibayar & "' " &
                                  " ORDER   BY Tanggal_Bayar ASC ", KoneksiDatabaseTransaksi)
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
                    cmb_KompensasiKe_Bulan.SelectedValue = KompensasiKe_Bulan
                    cmb_KompensasiKe_Tahun.SelectedValue = KompensasiKe_Tahun
                End If
                AksesDatabase_Transaksi(Tutup)
            End If
            PesanUntukProgrammer("Logika 'Kuncian Tanggal' untuk 'P' (Pembetulan) belum dibikin...!!!")
        End If

        If JumlahLebihBayar <= 0 Then Nonaktifkan_KolomKompensasiKe()

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()


        ProsesResetForm = True

        FungsiForm = Kosongan
        JenisPajak = Kosongan

        txt_MasaPajak.Text = Kosongan
        KosongkanDatePicker(dtp_TanggalLapor)
        txt_JumlahLebihBayar.Text = Kosongan
        NomorID = 0
        BulanAngka = 0
        TanggalPelunasan_Date = TanggalKosong
        KontenCombo_NP()

        ProsesResetForm = False

    End Sub


    Sub KontenCombo_NP()
        cmb_NP.Items.Clear()
        cmb_NP.Items.Add("N")
        cmb_NP.Items.Add("P")
        cmb_NP.Text = Kosongan
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
        cmb_KompensasiKe_Bulan.Text = Kosongan

    End Sub

    Sub KontenCombo_KompensasiKe_Tahun()
        cmb_KompensasiKe_Tahun.Items.Clear()
        cmb_KompensasiKe_Tahun.Items.Add(TahunPajak)
        cmb_KompensasiKe_Tahun.Items.Add(TahunPajak + 1)
        cmb_KompensasiKe_Tahun.Text = Kosongan
    End Sub

    Sub Aktifkan_KolomKompensasiKe()
        lbl_JumlahLebihBayar.Visibility = Visibility.Visible
        txt_JumlahLebihBayar.Visibility = Visibility.Visible
        lbl_KompensasiKe.Visibility = Visibility.Visible
        cmb_KompensasiKe_Bulan.Visibility = Visibility.Visible
        cmb_KompensasiKe_Tahun.Visibility = Visibility.Visible
        KontenCombo_KompensasiKe_Bulan()
        KontenCombo_KompensasiKe_Tahun()
    End Sub

    Sub Nonaktifkan_KolomKompensasiKe()
        lbl_JumlahLebihBayar.Visibility = Visibility.Collapsed
        txt_JumlahLebihBayar.Visibility = Visibility.Collapsed
        txt_JumlahLebihBayar.Text = Kosongan
        lbl_KompensasiKe.Visibility = Visibility.Collapsed
        cmb_KompensasiKe_Bulan.Visibility = Visibility.Collapsed
        cmb_KompensasiKe_Bulan.Text = Kosongan
        cmb_KompensasiKe_Tahun.Visibility = Visibility.Collapsed
        cmb_KompensasiKe_Tahun.Text = Kosongan
    End Sub




    Private Sub txt_MasaPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_MasaPajak.TextChanged
        MasaPajak = txt_MasaPajak.Text
        BulanAngka = AmbilAngka(KonversiBulanKeNomor_String(MasaPajak))
    End Sub


    Private Sub dtp_TanggalLapor_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalLapor.SelectedDateChanged
        If dtp_TanggalLapor.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalLapor)
            TanggalLapor = TanggalFormatTampilan(dtp_TanggalLapor.SelectedDate)
        End If
    End Sub


    Private Sub cmb_NP_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_NP.SelectionChanged
        NP = cmb_NP.SelectedValue
    End Sub


    Private Sub txt_JumlahLebihBayar_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahLebihBayar.TextChanged
        JumlahLebihBayar = AmbilAngka(txt_JumlahLebihBayar.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahLebihBayar)
    End Sub
    Private Sub txt_JumlahLebihBayar_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_JumlahLebihBayar.PreviewTextInput
              
    End Sub


    Private Sub cmb_KompensasiKe_Bulan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KompensasiKe_Bulan.SelectionChanged
        KompensasiKe_Bulan = cmb_KompensasiKe_Bulan.SelectedValue
    End Sub


    Private Sub cmb_KompensasiKe_Tahun_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_KompensasiKe_Tahun.SelectionChanged
        If cmb_KompensasiKe_Tahun.Text = Kosongan Then
            KompensasiKe_Tahun = 0
        Else
            KompensasiKe_Tahun = cmb_KompensasiKe_Tahun.SelectedValue
        End If
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If dtp_TanggalLapor.Text = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Tanggal Lapor'.")
            dtp_TanggalLapor.Focus()
            Return
        End If

        Dim TanggalLapor_Date As Date = TanggalLapor
        If TanggalLapor_Date < TanggalPelunasan_Date Then
            PesanPeringatan("'Tanggal Lapor' kurang dari 'Tanggal Pelunasan Pajak'." & Enter2Baris & "Silakan isi 'Tanggal Lapor' dengan benar.")
            dtp_TanggalLapor.Focus()
            Return
        End If

        If NP = Kosongan Then
            PesanPeringatan("Silakan pilih 'N/P'")
            cmb_NP.Focus()
            Return
        End If

        If cmb_KompensasiKe_Bulan.Visibility = Visibility.Visible Then
            If KompensasiKe_Bulan = Kosongan Then
                PesanPeringatan("Silakan pilih 'Kompensasi Ke Bulan'.")
                cmb_KompensasiKe_Bulan.Focus()
                Return
            End If
            If KompensasiKe_Tahun = Kosongan Then
                PesanPeringatan("Silakan pilih 'Kompensasi Ke Tahun'.")
                cmb_KompensasiKe_Tahun.Focus()
                Return
            End If
        End If

        If FungsiForm = FungsiForm_TAMBAH Then

            NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PengawasanPelaporanPajak") + 1

            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" INSERT INTO tbl_PengawasanPelaporanPajak VALUES ( " &
                                  " '" & NomorID & "', " &
                                  " '" & BulanAngka & "', " &
                                  " '" & JenisPajak & "', " &
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
                                  " Jenis_Pajak         = '" & JenisPajak & "', " &
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
            Select Case JenisPajak
                Case JenisPajak_PPN
                    If usc_BukuPengawasanPelaporanPPN.StatusAktif Then usc_BukuPengawasanPelaporanPPN.RefreshTampilanData()
                Case JenisPajak_PPhPasal21
                    'If usc_BukuPengawasanHutangPPhPasal21.StatusAktif Then usc_BukuPengawasanHutangPPhPasal21.RefreshTampilanData()
                Case JenisPajak_PPhPasal23
                    If usc_BukuPengawasanHutangPPhPasal23.StatusAktif Then usc_BukuPengawasanHutangPPhPasal23.RefreshTampilanData()
                Case JenisPajak_PPhPasal25
                    'If usc_BukuPengawasanHutangPPhPasal25.StatusAktif Then usc_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
                Case JenisPajak_PPhPasal26
                    'If usc_BukuPengawasanHutangPPhPasal26.StatusAktif Then usc_BukuPengawasanHutangPPhPasal26.RefreshTampilanData()
                Case JenisPajak_PPhPasal42
                    'If usc_BukuPengawasanHutangPPhPasal42.StatusAktif Then usc_BukuPengawasanHutangPPhPasal42.RefreshTampilanData()
            End Select
            Me.Close()
        Else
            pesan_DataGagalDisimpan()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Close()
    End Sub




    Public Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_MasaPajak.IsReadOnly = True
        txt_JumlahLebihBayar.IsReadOnly = True
        cmb_KompensasiKe_Bulan.IsReadOnly = True
        cmb_KompensasiKe_Tahun.IsReadOnly = True
    End Sub

End Class
