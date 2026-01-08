Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc

Public Class wpfWin_InputHutangPPhPasal25

    Public FungsiForm As String
    Public JudulForm As String
    Public NomorId
    Dim Tahun
    Dim MasaPajak
    Dim NomorBulan
    Dim TanggalTransaksi
    Dim JumlahTerutang
    Dim NomorBPHP
    Dim KodeBilling
    Dim Keterangan
    Public NomorJV
    Dim COADebet, COAKredit

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Hutang PPh Pasal 25"
            cmb_Tahun.IsEnabled = True
            cmb_MasaPajak.IsEnabled = True
        End If

        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Hutang PPh Pasal 25"
            cmb_Tahun.IsEnabled = False
            cmb_MasaPajak.IsEnabled = False
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Tentukan dulu Fungsi Form-nya...!!!!")

        Title = JudulForm

        If JenisTahunBuku = JenisTahunBuku_NORMAL Then cmb_Tahun.IsEnabled = False


        ProsesLoadingForm = False

    End Sub



    Sub ResetForm()

        ProsesResetForm = True

        FungsiForm = Kosongan
        JudulForm = Kosongan 'Jangan dihapus...!!!
        NomorId = 0
        NomorJV = 0
        NomorBulan = 0

        KontenCombo_Tahun()
        KontenCombo_MasaPajak()
        txt_NomorBPHP.IsEnabled = False
        txt_JumlahTerutang.IsEnabled = True
        txt_Keterangan.IsEnabled = True

        txt_NomorBPHP.Text = Kosongan
        txt_JumlahTerutang.Text = Kosongan
        KosongkanValueElemenRichTextBox(txt_Keterangan)

        ProsesResetForm = False

    End Sub



    Sub KontenCombo_Tahun()
        Dim TahunHutangPajakTerlama = TahunIni - 10
        Dim ListTahunPajak = TahunBukuAktif

        cmb_Tahun.Items.Clear()
        Do While ListTahunPajak >= TahunHutangPajakTerlama
            cmb_Tahun.Items.Add(ListTahunPajak.ToString)    'Harus pakai ToString. Kalau tidak, bisa jadi masalah....!!!
            ListTahunPajak -= 1
        Loop
        cmb_Tahun.SelectedValue = TahunPajak.ToString       'Harus pakai ToString. Kalau tidak, bisa jadi masalah....!!!
    End Sub


    Sub KontenCombo_MasaPajak()
        cmb_MasaPajak.Items.Clear()
        cmb_MasaPajak.Items.Add(Bulan_Januari)
        cmb_MasaPajak.Items.Add(Bulan_Februari)
        cmb_MasaPajak.Items.Add(Bulan_Maret)
        cmb_MasaPajak.Items.Add(Bulan_April)
        cmb_MasaPajak.Items.Add(Bulan_Mei)
        cmb_MasaPajak.Items.Add(Bulan_Juni)
        cmb_MasaPajak.Items.Add(Bulan_Juli)
        cmb_MasaPajak.Items.Add(Bulan_Agustus)
        cmb_MasaPajak.Items.Add(Bulan_September)
        cmb_MasaPajak.Items.Add(Bulan_Oktober)
        cmb_MasaPajak.Items.Add(Bulan_Nopember)
        cmb_MasaPajak.Items.Add(Bulan_Desember)
        cmb_MasaPajak.Text = Kosongan
    End Sub

    Sub IsiValueForm()
        If NomorBulan > 0 Then
            Dim NomorBulan_String As String
            If Len(NomorBulan.ToString) = 1 Then
                NomorBulan_String = "0" & NomorBulan.ToString
            Else
                NomorBulan_String = NomorBulan.ToString
            End If
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_HutangPajak " &
                                  " WHERE Jenis_Pajak                           = '" & JenisPajak_PPhPasal25 & "' " &
                                  " AND DATE_FORMAT(Tanggal_Transaksi, '%Y-%m') = '" & Tahun & "-" & NomorBulan_String & "' ",
                                  KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            If dr.HasRows Then
                FungsiForm = FungsiForm_EDIT
                JudulForm = "Edit Hutang PPh Pasal 25"
                NomorId = dr.Item("Nomor_ID")
                NomorJV = dr.Item("Nomor_JV")
                txt_JumlahTerutang.Text = dr.Item("Jumlah_Hutang")
                IsiValueElemenRichTextBox(txt_Keterangan, dr.Item("Keterangan"))
            Else
                FungsiForm = FungsiForm_TAMBAH
                JudulForm = "Input Hutang PPh Pasal 25"
                NomorId = 0
                NomorJV = 0
                txt_JumlahTerutang.Text = Kosongan
                KosongkanValueElemenRichTextBox(txt_Keterangan)
            End If
            AksesDatabase_Transaksi(Tutup)
            txt_NomorBPHP.Text = AwalanBPHP25 & Tahun & "-" & NomorBulan
            If Tahun > 0 Then TanggalTransaksi = TanggalAkhirBulan_Case(Tahun, NomorBulan)
        Else
            txt_NomorBPHP.Text = Kosongan
        End If
        Title = JudulForm
    End Sub


    Private Sub cmb_Tahun_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Tahun.SelectionChanged
        Tahun = AmbilAngka(cmb_Tahun.SelectedValue)
        IsiValueForm()
    End Sub


    Private Sub cmb_MasaPajak_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_MasaPajak.SelectionChanged
        MasaPajak = cmb_MasaPajak.SelectedValue
        NomorBulan = KonversiBulanKeAngka(MasaPajak)
        IsiValueForm()
    End Sub


    Private Sub txt_NomorBPHP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorBPHP.TextChanged
        NomorBPHP = txt_NomorBPHP.Text
    End Sub


    Private Sub txt_JumlahTerutang_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahTerutang.TextChanged
        JumlahTerutang = AmbilAngka(txt_JumlahTerutang.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahTerutang)
    End Sub


    Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
        Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        TrialBalance_Mentahkan()

        If MasaPajak = Kosongan Then
            MsgBox("Silakan pilih 'Masa Pajak'.")
            cmb_MasaPajak.Focus()
            Return
        End If

        If JumlahTerutang = 0 Then
            MsgBox("Silakan isi kolom 'Jumlah Terutang'.")
            txt_JumlahTerutang.Text = Kosongan
            txt_JumlahTerutang.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                Pilihan = MessageBox.Show(teks_DataAkanDisimpanDiBukuPengawasanDanJurnal, "Perhatian..!", MessageBoxButtons.YesNo)
                If Pilihan = vbNo Then Return
            End If
        End If

        If FungsiForm = FungsiForm_EDIT Then
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then
                Pilihan = MessageBox.Show(teks_PerubahanDataAkanBerpengaruhPadaJurnal, "Perhatian..!", MessageBoxButtons.YesNo)
                If Pilihan = vbNo Then Return
            End If
        End If

        StatusSuntingDatabase = False

        TrialBalance_Mentahkan()

        'Simpan Data Hutang ke Tabel : tbl_HutangPPhPasal25
        If FungsiForm = FungsiForm_TAMBAH Then
            NomorId = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_HutangPajak") + 1
            SistemPenomoranOtomatis_NomorJV()
            NomorJV = jur_NomorJV
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(
                " INSERT INTO tbl_HutangPajak VALUES ( " &
                " '" & NomorId & "', " &
                " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                " '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                " '" & Kosongan & "', " &
                " '" & Kosongan & "', " &
                " '" & Kosongan & "', " &
                " '" & Kosongan & "', " &
                " '" & Kosongan & "', " &
                " '" & Kosongan & "', " &
                " '" & Kosongan & "', " &
                " '" & JenisPajak_PPhPasal25 & "', " &
                " '" & KodeSetoran_Non & "', " &
                " '" & JumlahTerutang & "', " &
                " '" & Keterangan & "', " &
                " '" & NomorJV & "', " &
                " '" & UserAktif & "' ) ",
                KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        'Jika Form Berfungsi untuk EDIT, maka hapus dulu data Jurnal yang lama, sebelum menyimpan data Jurnal yang Baru :
        If FungsiForm = FungsiForm_EDIT Then

            AksesDatabase_Transaksi(Buka)

            'Edit Data Hutang di Tabel : tbl_HutangPPhPasal25
            cmd = New OdbcCommand(
                " UPDATE tbl_HutangPajak SET " &
                " Tanggal_Transaksi     = '" & TanggalFormatSimpan(TanggalTransaksi) & "', " &
                " Jumlah_Hutang         = '" & JumlahTerutang & "', " &
                " Keterangan            = '" & Keterangan & "', " &
                " Nomor_JV              = '" & NomorJV & "', " &
                " User                  = '" & UserAktif & "' " &
                " WHERE Nomor_ID        = '" & NomorId & "' ",
                KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()

            'Hapus Jurnal Yang Ada :
            If StatusSuntingDatabase = True Then
                jur_NomorJV = NomorJV 'Ini jangan dihapus. Penting untuk value saat mengisi data jurnal
                cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & jur_NomorJV & "' ", KoneksiDatabaseTransaksi)
                cmd_ExecuteNonQuery()
            End If

            AksesDatabase_Transaksi(Tutup)

        End If

        'Simpan Data Hutang ke Jurnal (tbl_Transaksi) || Hanya berlaku untuk Jenis Tahun Buku NORMAL :
        If StatusSuntingDatabase = True And JenisTahunBuku = JenisTahunBuku_NORMAL Then

            ResetValueJurnal()
            jur_JenisJurnal = JenisJurnal_AdjusmentPajak
            jur_TanggalTransaksi = TanggalFormatSimpan(TanggalTransaksi)
            jur_Bundelan = NomorBPHP
            jur_NomorInvoice = KodeBilling
            jur_KodeLawanTransaksi = KodeLawanTransaksi_DJP
            jur_NamaLawanTransaksi = NamaLawanTransaksi_DJP
            jur_UraianTransaksi = Keterangan
            jur_Direct = 0
            COADebet = KodeTautanCOA_PPhPasal25DibayarDimuka
            COAKredit = KodeTautanCOA_HutangPPhPasal25

            'Simpan Jurnal :
            ___jurDebet(COADebet, JumlahTerutang)
            _______jurKredit(COAKredit, JumlahTerutang)

            If jur_StatusPenyimpananJurnal_Lengkap = True Then
                StatusSuntingDatabase = True
            Else
                StatusSuntingDatabase = False
            End If

        End If

        If StatusSuntingDatabase = True Then
            ResetForm()
            If FungsiForm = FungsiForm_TAMBAH Then MsgBox("Data BERHASIL disimpan.")
            If FungsiForm = FungsiForm_EDIT Then MsgBox("Data BERHASIL diedit.")
            If usc_BukuPengawasanHutangPPhPasal25.StatusAktif Then usc_BukuPengawasanHutangPPhPasal25.RefreshTampilanData()
            Me.Close()
        Else
            MsgBox("Data GAGAL disimpan..!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Close()
    End Sub

    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        cmb_Tahun.IsReadOnly = True
        cmb_MasaPajak.IsReadOnly = True
        txt_NomorBPHP.IsReadOnly = True
    End Sub

End Class
