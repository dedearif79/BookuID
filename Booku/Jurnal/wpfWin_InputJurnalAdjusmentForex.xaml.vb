Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Data.Odbc
Imports System.Windows.Input
Imports System.Windows.Controls.Primitives
Imports System.Windows.Threading

Public Class wpfWin_InputJurnalAdjusmentForex


    Public JudulForm
    Public FungsiForm
    Public JalurMasuk
    Public AngkaNomorJV
    Public JumlahBarisJurnal
    Dim JumlahDebet As Int64
    Dim JumlahKredit As Int64
    Public TotalDebet As Int64
    Public TotalKredit As Int64
    Dim StatusBalance = Kosongan
    Dim COA
    Dim DK = Nothing
    Dim TahunJurnal
    Public JurnalTersimpan As Boolean 'Ini Penting...!!! Ada kaitannya dengan form lain.

    Dim TanggalJurnal
    Dim JenisJurnal
    Dim KodeDokumen
    Dim Bundelan
    Dim TanggalInvoice
    Dim NomorInvoice
    Dim NomorFakturPajak
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UraianTransaksi

    Dim KursAkhirBulan As Decimal

    Dim SaldoSaatTerakhirTransaksiHutangUsahaImpor_NonIDR As Decimal
    Dim SaldoSaatTerakhirTransaksiHutangUsahaImpor_IDR As Int64
    Dim SaldoAkhirHutangUsahaImpor_SaatAdjusment As Int64
    Dim Selisih As Int64

    Dim BulanAngka As Integer


    Dim COATerseleksi
    Dim NamaAkunTerseleksi
    Dim DKTerseleksi
    Dim JumlahDebetTerseleksi
    Dim JumlahKreditTerseleksi

    Public TanggalTerakhirTransaksi As Date
    Public AkunAdjusment As String
    Dim teks_Laba = "Laba"
    Dim teks_Rugi = "Rugi"

    Public KodeMataUang

    Public BisaDiedit As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        Select Case FungsiForm
            Case FungsiForm_TAMBAH
                JudulForm = "Input Jurnal"
                SistemPenomoranOtomatis_NomorJV()
                AngkaNomorJV = jur_NomorJV
                btn_Reset.IsEnabled = True
            Case FungsiForm_EDIT
                JudulForm = "Edit Jurnal"
                btn_Reset.IsEnabled = False
                IsiValue_UntukEdit()
                jur_NomorJV = AngkaNomorJV
        End Select

        If KodeMataUang = Kosongan Then PesanUntukProgrammer("Kode Mata Uang belum ditentukan...!!!")

        If JalurMasuk = Halaman_BUKUPENGAWASANTURUNANGAJI Then
            txt_TanggalInvoice.IsEnabled = False
        Else
            txt_TanggalInvoice.IsEnabled = True
        End If

        If JalurMasuk = Menu_JurnalAdjusment Or JalurMasuk = Halaman_JURNALADJUSMENT Then
            dtp_TanggalJurnal.IsEnabled = False
            cmb_JenisJurnal.IsEnabled = False
            txt_KodeDokumen.IsEnabled = False
            txt_Bundelan.IsEnabled = False
            txt_TanggalInvoice.IsEnabled = False
            txt_NomorInvoice.IsEnabled = False
            txt_NomorFakturPajak.IsEnabled = False
            txt_KodeLawanTransaksi.IsEnabled = False
            btn_PilihMitra.IsEnabled = False
            txt_NamaLawanTransaksi.IsEnabled = False
            'btn_Input.IsEnabled = False
            btn_Reset.IsEnabled = False
            BisaDiedit = False
        End If

        Title = JudulForm
        txt_NomorJV.Text = AwalanNomorJV & AngkaNomorJV

        BulanAngka = AmbilBulanAngka_DariTanggal(TanggalJurnal)

        Dim COA_NonIDR As String = PenentuanCOA_HutangUsahaImpor_BerdasarkanKodeMataUang(KodeMataUang)

        SaldoSaatTerakhirTransaksiHutangUsahaImpor_NonIDR = SaldoAkhirCOA_SampaiAkhirBulanTertentu_MUA(COA_NonIDR, BulanAngka)
        SaldoSaatTerakhirTransaksiHutangUsahaImpor_IDR = SaldoAkhirCOA_SampaiAkhirBulanTertentu(COA_NonIDR, BulanAngka)

        txt_KursAkhirBulan.Focus()

        JurnalTersimpan = False 'Default False ini harus dieksekusi saat loading, untuk jaga-jaga. Jangan dihapus...!!!

        If TanggalTerakhirTransaksi = TanggalKosong Then TanggalTerakhirTransaksi = TanggalAkhirTahunKemarin
        Dim TanggalTerakhirTransaksi_Str As String = TanggalTerakhirTransaksi.Day & Spasi1 & KonversiAngkaKeBulanString(TanggalTerakhirTransaksi.Month) & Spasi1 & TanggalTerakhirTransaksi.Year
        Dim TanggalAkhirBulan_Str As String = dtp_TanggalJurnal.SelectedDate.Value.Day & Spasi1 & KonversiAngkaKeBulanString(dtp_TanggalJurnal.SelectedDate.Value.Month) & Spasi1 & dtp_TanggalJurnal.SelectedDate.Value.Year

        lbl_SaldoAkhirMUA.Text = "Saldo Akhir (" & KodeMataUang & ")"
        lbl_SaldoAkhirBulanIDR.Text = "Saldo Akhir " & KonversiAngkaKeBulanString(dtp_TanggalJurnal.SelectedDate.Value.Month) & Spasi1 & dtp_TanggalJurnal.SelectedDate.Value.Year
        lbl_Pertanggal_SaldoAkhirMUA.Text = "(Per " & TanggalTerakhirTransaksi_Str & ")"
        lbl_Pertanggal_SaldoAkhirIDR.Text = lbl_Pertanggal_SaldoAkhirMUA.Text
        lbl_Pertanggal_SaldoAkhirBulanIDR.Text = "(Per " & TanggalAkhirBulan_Str & ")"

        Select Case Selisih
            Case 0
                lbl_LabaRugi.Text = Kosongan
            Case > 0
                If COATermasukDEBET(AkunAdjusment) = True Then
                    lbl_LabaRugi.Text = "(" & teks_Rugi & ")"
                Else
                    lbl_LabaRugi.Text = "(" & teks_Laba & ")"
                End If
            Case < 0
                If COATermasukDEBET(AkunAdjusment) = True Then
                    lbl_LabaRugi.Text = "(" & teks_Laba & ")"
                Else
                    lbl_LabaRugi.Text = "(" & teks_Rugi & ")"
                End If
        End Select

        ProsesLoadingForm = False

    End Sub



    Sub ResetForm()

        ProsesResetForm = True

        JurnalTersimpan = False 'Default False ini juga harus dieksekusi saat reset form, untuk jaga-jaga. Jangan dihapus...!!!

        BisaDiedit = True

        JalurMasuk = Kosongan

        KodeMataUang = Kosongan

        lbl_NomorFaktur.Text = "Nomor Faktur Pajak"

        cmb_JenisJurnal.IsEnabled = True
        txt_KodeDokumen.IsEnabled = True
        txt_Bundelan.IsEnabled = True
        txt_TanggalInvoice.IsEnabled = True
        txt_NomorInvoice.IsEnabled = True
        txt_NomorFakturPajak.IsEnabled = True
        btn_PilihMitra.IsEnabled = True
        txt_KodeLawanTransaksi.IsEnabled = True
        txt_NamaLawanTransaksi.IsEnabled = True
        txt_UraianTransaksi.IsEnabled = True
        'btn_Input.IsEnabled = True
        'btn_Edit.IsEnabled = True
        'btn_Hapus.IsEnabled = True
        btn_Simpan.IsEnabled = False
        btn_Reset.IsEnabled = True

        KosongkanDatePicker(dtp_TanggalJurnal)
        KontenComboJenisJurnal_Public_WPF(cmb_JenisJurnal)
        txt_KodeDokumen.Text = Kosongan
        txt_Bundelan.Text = Kosongan
        txt_TanggalInvoice.Text = Kosongan
        txt_NomorInvoice.Text = Kosongan
        txt_NomorFakturPajak.Text = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        txt_KursAkhirBulan.Text = 1
        KosongkanValueElemenRichTextBox(txt_UraianTransaksi)

        datatabelUtama.Rows.Clear()
        BersihkanSeleksi()
        JumlahBarisJurnal = 0
        datatabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, 0, 0) 'Jangan dihapus, dan jangan dipindahkan..!!!

        TampilkanData()

        ProsesResetForm = False

    End Sub



    Sub TampilkanData()

        Dim Baris = 0
        TotalDebet = 0
        TotalKredit = 0
        Do While Baris < JumlahBarisJurnal
            JumlahDebet = AmbilAngka(datatabelUtama.Rows(Baris)("Jumlah_Debet"))
            JumlahKredit = AmbilAngka(datatabelUtama.Rows(Baris)("Jumlah_Kredit"))
            TotalDebet += JumlahDebet
            TotalKredit += JumlahKredit
            Baris += 1
        Loop

        BersihkanSeleksi()

        NotifBalance()

    End Sub

    Sub IsiValue_UntukEdit()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Dim NomorUrut = 0
        Dim COA
        Dim NamaAkun
        Dim DK
        Dim JumlahDebet
        Dim JumlahKredit
        TotalDebet = 0
        TotalKredit = 0
        datatabelUtama.Rows.Clear()
        Do While dr.Read
            dtp_TanggalJurnal.SelectedDate = dr.Item("Tanggal_Transaksi")
            cmb_JenisJurnal.SelectedValue = dr.Item("Jenis_Jurnal")
            txt_KodeDokumen.Text = dr.Item("Referensi")
            txt_Bundelan.Text = dr.Item("Bundelan")
            txt_TanggalInvoice.Text = dr.Item("Tanggal_Invoice")
            txt_NomorInvoice.Text = dr.Item("Nomor_Invoice")
            txt_NomorFakturPajak.Text = dr.Item("Nomor_Faktur_Pajak")
            txt_KodeLawanTransaksi.Text = dr.Item("Kode_Lawan_Transaksi")
            txt_NamaLawanTransaksi.Text = dr.Item("Nama_Lawan_Transaksi")
            IsiValueElemenRichTextBox(txt_UraianTransaksi, dr.Item("Uraian_Transaksi"))
            NomorUrut = NomorUrut + 1
            COA = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            DK = dr.Item("D_K")
            If DK = "K" Then NamaAkun = PenjorokNamaAkun + NamaAkun
            JumlahDebet = dr.Item("Jumlah_Debet")
            JumlahKredit = dr.Item("Jumlah_Kredit")
            TotalDebet += JumlahDebet
            TotalKredit += JumlahKredit
            datatabelUtama.Rows.Add(NomorUrut, COA, NamaAkun, DK, JumlahDebet, JumlahKredit)
        Loop
        AksesDatabase_Transaksi(Tutup)
        BersihkanSeleksi()
        JumlahBarisJurnal = NomorUrut
        datatabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, 0, 0) 'Jangan dihapus, dan jangan dipindahkan..!!!
        NotifBalance()
    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        'btn_Edit.IsEnabled = False
        'btn_Hapus.IsEnabled = False
    End Sub

    Sub NotifBalance()

        If TotalDebet = TotalKredit Then
            StatusBalance = teks_TidakAdaSelisih
            lbl_StatusBalance.Foreground = WarnaHijauSolid_WPF
        Else
            StatusBalance = teks_TidakAdaSelisih
            lbl_StatusBalance.Foreground = WarnaPeringatan_WPF
        End If

        lbl_StatusBalance.Text = StatusBalance

        Dim clm_TotalDebet
        If TotalDebet = 0 Then
            clm_TotalDebet = 0
        Else
            clm_TotalDebet = TotalDebet
        End If

        Dim clm_TotalKredit
        If TotalKredit = 0 Then
            clm_TotalKredit = 0
        Else
            clm_TotalKredit = TotalKredit
        End If

        datatabelUtama.Rows(JumlahBarisJurnal + 1)("Nama_Akun") = "J  U  M  L  A  H"
        datatabelUtama.Rows(JumlahBarisJurnal + 1)("Jumlah_Debet") = clm_TotalDebet
        datatabelUtama.Rows(JumlahBarisJurnal + 1)("Jumlah_Kredit") = clm_TotalKredit

        If TotalKredit = 0 And TotalDebet > 0 Then
            lbl_StatusBalance.Text = teks_TidakAdaKredit
        Else
        End If
        If TotalDebet = 0 And TotalKredit > 0 Then
            lbl_StatusBalance.Text = teks_TidakAdaDebet
        End If

        If JumlahBarisJurnal = 0 Then
            lbl_StatusBalance.Foreground = WarnaPeringatan_WPF
            lbl_StatusBalance.Text = teks_TidakAdaTransaksi
        End If

        LogikaTombolSimpan()

    End Sub


    Sub LogikaTombolSimpan()
        If lbl_StatusBalance.Text = teks_TidakAdaSelisih And JenisJurnal <> Kosongan Then
            btn_Simpan.IsEnabled = True
        Else
            btn_Simpan.IsEnabled = False
        End If
    End Sub


    Private Sub dtp_TanggalJurnal_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_TanggalJurnal.SelectedDateChanged
        If dtp_TanggalJurnal.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_TanggalJurnal)
            TanggalJurnal = Microsoft.VisualBasic.Left(dtp_TanggalJurnal.Text, 10)
            TahunJurnal = dtp_TanggalJurnal.SelectedDate.Value.Year
        End If
    End Sub
    Private Sub dtp_TanggalJurnal_LostFocus(sender As Object, e As RoutedEventArgs) Handles dtp_TanggalJurnal.LostFocus
        If dtp_TanggalJurnal.Text <> Kosongan Then
            If dtp_TanggalJurnal.SelectedDate.Value > Today Then
                PesanPeringatan("Tanggal Bayar yang Anda input melebihi hari ini." & Enter2Baris & "Silakan isi kolom 'Tanggal Jurnal' dengan benar.")
                dtp_TanggalJurnal.Text = Kosongan
                dtp_TanggalJurnal.Focus()
                Return
            End If
        End If
    End Sub



    Private Sub cmb_JenisJurnal_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisJurnal.SelectionChanged
        JenisJurnal = cmb_JenisJurnal.SelectedValue
        LogikaTombolSimpan()
    End Sub


    Private Sub txt_KodeDokumen_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeDokumen.TextChanged
        KodeDokumen = txt_KodeDokumen.Text
    End Sub


    Private Sub txt_Bundelan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Bundelan.TextChanged
        Bundelan = txt_Bundelan.Text
    End Sub


    Private Sub txt_TanggalInvoice_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_TanggalInvoice.TextChanged
        TanggalInvoice = txt_TanggalInvoice.Text
    End Sub


    Private Sub txt_NomorInvoice_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorInvoice.TextChanged
        NomorInvoice = txt_NomorInvoice.Text
    End Sub


    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NomorFakturPajak.TextChanged
        NomorFakturPajak = txt_NomorFakturPajak.Text
    End Sub


    Private Sub txt_KodeLawanTransaksi_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text
    End Sub
    Private Sub btn_PilihMitra_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihMitra.Click
        BukaFormListLawanTransaksi(txt_KodeLawanTransaksi, txt_NamaLawanTransaksi, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua, Pilihan_Semua)
    End Sub
    Private Sub txt_NamaLawanTransaksi_TextChanged_1(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub


    Private Sub txt_KursAkhirBulan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KursAkhirBulan.TextChanged
        KursAkhirBulan = AmbilAngka_Desimal(txt_KursAkhirBulan.Text)
        Perhitungan()
    End Sub


    Private Sub txt_UraianTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_UraianTransaksi.TextChanged
        UraianTransaksi = IsiValueVariabelRichTextBox(txt_UraianTransaksi)
    End Sub


    Private Sub txt_SaldoAkhirMUA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAkhirMUA.TextChanged

    End Sub

    Private Sub txt_SaldoAkhirIDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAkhirIDR.TextChanged

    End Sub

    Private Sub txt_SaldoAkhirBulanIDR_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SaldoAkhirBulanIDR.TextChanged

    End Sub

    Private Sub txt_Selisih_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Selisih.TextChanged
        Selisih = AmbilAngka(txt_Selisih.Text)
    End Sub


    Sub Perhitungan()

        'SaldoAkhirHutangUsahaImpor_SaatAdjusment = AmbilValue_NilaiMataUang(KodeMataUang, KursAkhirBulan, SaldoSaatTerakhirTransaksiHutangUsahaImpor_NonIDR)
        'Selisih = SaldoAkhirHutangUsahaImpor_SaatAdjusment - SaldoSaatTerakhirTransaksiHutangUsahaImpor_IDR

        'btn_Simpan.IsEnabled = True

        'If Selisih = 0 Then
        '    btn_Simpan.IsEnabled = False
        'ElseIf Selisih > 0 Then

        'Else

        'End If

    End Sub



    'Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click

    '    frm_InputJurnalPerTransaksi.ResetForm()
    '    frm_InputJurnalPerTransaksi.ShowDialog()

    '    If frm_InputJurnalPerTransaksi.TombolPenutup = "OK" Then

    '        Dim BarisDitambahkan
    '        Dim DK = frm_InputJurnalPerTransaksi.DK
    '        Dim JumlahTransaksi As Int64 = frm_InputJurnalPerTransaksi.JumlahTransaksi
    '        Dim KodeAkun = frm_InputJurnalPerTransaksi.COATerseleksi
    '        Dim NamaAkun = frm_InputJurnalPerTransaksi.NamaAkunTerseleksi
    '        Dim JumlahDebet
    '        Dim JumlahKredit
    '        If DK = dk_D Then
    '            JumlahDebet = JumlahTransaksi
    '            JumlahKredit = 0
    '        Else
    '            JumlahDebet = 0
    '            JumlahKredit = JumlahTransaksi
    '            NamaAkunTerseleksi = PenjorokNamaAkun + NamaAkunTerseleksi
    '        End If

    '        Select Case DK
    '            Case dk_D
    '                Dim DK_Telusur As String = Kosongan
    '                BarisDitambahkan = 0
    '                For Each row As DataRow In datatabelUtama.Rows
    '                    If Not IsDBNull(row("D_K")) Then
    '                        DK_Telusur = row("D_K")
    '                        If DK_Telusur <> dk_D Then Exit For
    '                        BarisDitambahkan += 1
    '                    Else
    '                        Exit For
    '                    End If
    '                Next
    '            Case dk_K
    '                BarisDitambahkan = JumlahBarisJurnal
    '                NamaAkun = PenjorokNamaAkun + NamaAkun
    '        End Select

    '        JumlahBarisJurnal += 1

    '        newRow = datatabelUtama.NewRow()
    '        newRow("Nomor_Urut") = Kosongan
    '        newRow("Kode_Akun") = KodeAkun
    '        newRow("Nama_Akun") = NamaAkun
    '        newRow("D_K") = DK
    '        newRow("Jumlah_Debet") = JumlahDebet
    '        newRow("Jumlah_Kredit") = JumlahKredit
    '        datatabelUtama.Rows.InsertAt(newRow, BarisDitambahkan)

    '        TampilkanData()

    '        Dim NomorUrut = 0
    '        For Each row As DataRow In datatabelUtama.Rows
    '            NomorUrut += 1
    '            row("Nomor_Urut") = NomorUrut
    '            If NomorUrut = JumlahBarisJurnal Then Exit For
    '        Next

    '    End If

    'End Sub


    'Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click

    '    If (BarisTerseleksi < 0) Or (BarisTerseleksi >= JumlahBarisJurnal) Then
    '        MsgBox("Tidak ada baris terseleksi.")
    '        Return
    '    End If

    '    frm_InputJurnalPerTransaksi.ResetForm()
    '    frm_InputJurnalPerTransaksi.txt_COA.Text = COATerseleksi
    '    frm_InputJurnalPerTransaksi.txt_NamaAkun.Text = NamaAkunTerseleksi
    '    If DKTerseleksi = dk_D Then
    '        frm_InputJurnalPerTransaksi.cmb_DK.Text = "DEBET"
    '        frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text = JumlahDebetTerseleksi
    '    End If
    '    If DKTerseleksi = dk_K Then
    '        frm_InputJurnalPerTransaksi.cmb_DK.Text = "KREDIT"
    '        frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text = JumlahKreditTerseleksi
    '    End If

    '    'Reset form Input Jurnal, ada di atas. Jangan taruh di sini...!!!
    '    frm_InputJurnalPerTransaksi.ShowDialog()

    '    DKTerseleksi = frm_InputJurnalPerTransaksi.DK
    '    Dim KodeAkun
    '    Dim NamaAkun
    '    Dim JumlahTransaksi As Int64 = AmbilAngka(frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text)
    '    KodeAkun = frm_InputJurnalPerTransaksi.COATerseleksi
    '    NamaAkun = GantiTeks(frm_InputJurnalPerTransaksi.NamaAkunTerseleksi, PenjorokNamaAkun, Kosongan)
    '    If DKTerseleksi = dk_D Then
    '        rowviewUtama("D_K") = "D"
    '        rowviewUtama("Jumlah_Debet") = JumlahTransaksi
    '        rowviewUtama("Jumlah_Kredit") = 0
    '    End If
    '    If DKTerseleksi = dk_K Then
    '        rowviewUtama("D_K") = "K"
    '        rowviewUtama("Jumlah_Debet") = 0
    '        rowviewUtama("Jumlah_Kredit") = JumlahTransaksi
    '        NamaAkun = PenjorokNamaAkun + NamaAkun
    '    End If
    '    rowviewUtama("Kode_Akun") = KodeAkun
    '    rowviewUtama("Nama_Akun") = NamaAkun

    '    TampilkanData()

    'End Sub


    'Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

    '    If (BarisTerseleksi < 0) Or (BarisTerseleksi >= JumlahBarisJurnal) Then
    '        MsgBox("Tidak ada baris terseleksi.")
    '        Return
    '    End If

    '    datatabelUtama.Rows.RemoveAt(BarisTerseleksi)

    '    JumlahBarisJurnal -= 1
    '    Dim NomorUrut = 0
    '    For Each row As DataRow In datatabelUtama.Rows
    '        NomorUrut += 1
    '        row("Nomor_Urut") = NomorUrut
    '        If NomorUrut = JumlahBarisJurnal Then Exit For
    '    Next

    '    TampilkanData()

    'End Sub



    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        COATerseleksi = rowviewUtama("Kode_Akun")
        NamaAkunTerseleksi = rowviewUtama("Nama_Akun")
        DKTerseleksi = rowviewUtama("D_K")
        JumlahDebetTerseleksi = rowviewUtama("Jumlah_Debet")
        JumlahKreditTerseleksi = rowviewUtama("Jumlah_Kredit")

        'If BarisTerseleksi < JumlahBarisJurnal Then
        '    btn_Edit.IsEnabled = True
        '    btn_Hapus.IsEnabled = True
        'Else
        '    btn_Edit.IsEnabled = False
        '    btn_Hapus.IsEnabled = False
        'End If

        'If BisaDiedit = False Then
        '    btn_Input.IsEnabled = False
        '    btn_Edit.IsEnabled = False
        '    btn_Hapus.IsEnabled = False
        'End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        JurnalTersimpan = False 'Default : Wajib False
        jur_StatusPenyimpananJurnal_Lengkap = False

        If dtp_TanggalJurnal.Text = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTanggal(dtp_TanggalJurnal, "Tanggal Jurnal")
            Return
        End If

        If JalurMasuk = Kosongan Then
            PesanUntukProgrammer("Jalur Masuk belum ditentukan...!")
            Return
        End If

        If TotalDebet <> TotalKredit Then
            MsgBox("Jurnal tidak dapat diposting karena ADA SELISIH." &
                   Enter2Baris & "Silakan dikoreksi kembali.")
            Return
        End If

        If cmb_JenisJurnal.Text = Kosongan Then
            MsgBox("Silakan tentukan 'Jenis Jurnal'.")
            cmb_JenisJurnal.Focus()
            Return
        End If

        If UraianTransaksi = Kosongan Then
            PesanPeringatan("Silakan isi kolom 'Uraian'.")
            txt_UraianTransaksi.Focus()
            Return
        End If

        Pilihan = MessageBox.Show("Yakin data sudah benar..?", "Perhatian..!", MessageBoxButtons.YesNo)
        'PesanUntukProgrammer("Pilihan : " & Pilihan.ToString)
        If Pilihan = vbNo Then Return

        'Jika form berfungsi untuk mengedit, maka hapus data sebelumnya :
        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV & "' ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
            jur_NomorJV = AngkaNomorJV
        End If

        '====================================================================================
        'PENYIMPANAN JURNAL :
        '====================================================================================
        ResetValueJurnal()
        jur_TanggalTransaksi = TanggalFormatSimpan(dtp_TanggalJurnal.SelectedDate)
        jur_JenisJurnal = JenisJurnal
        jur_KodeDokumen = KodeDokumen
        jur_Referensi = KodeDokumen
        jur_Bundelan = Bundelan
        jur_TanggalInvoice = TanggalInvoice
        jur_NomorInvoice = NomorInvoice
        jur_NomorFakturPajak = NomorFakturPajak
        jur_KodeLawanTransaksi = KodeLawanTransaksi
        jur_NamaLawanTransaksi = NamaLawanTransaksi
        jur_UraianTransaksi = UraianTransaksi
        jur_KodeMataUang = KodeMataUang_IDR
        jur_Kurs = 1
        If JalurMasuk = Halaman_JURNALUMUM Then
            jur_Direct = 1
        Else
            jur_Direct = 0
        End If

        Dim NomorUrut = 0
        For Each row As DataRow In datatabelUtama.Rows
            NomorUrut += 1
            jur_COA = row("Kode_Akun")
            jur_DK = row("D_K")
            If jur_DK = dk_D Then jur_JumlahMutasi = AmbilAngka(row("Jumlah_Debet"))
            If jur_DK = dk_K Then jur_JumlahMutasi = AmbilAngka(row("Jumlah_Kredit"))
            ______________________________________SimpanJurnal_PerBaris()
            If NomorUrut = JumlahBarisJurnal Then Exit For
        Next

        If jur_StatusPenyimpananJurnal_PerBaris = True Then
            JurnalTersimpan = True
            MsgBox("Jurnal BERHASIL disimpan.")
            Me.Close()
        Else
            JurnalTersimpan = False
            MsgBox("Jurnal GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub btn_Reset_Click(sender As Object, e As RoutedEventArgs) Handles btn_Reset.Click
        Pilihan = MessageBox.Show("Yakin akan me-reset..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return
        ResetForm()
    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()
        txt_NomorJV.IsReadOnly = True
        cmb_JenisJurnal.IsReadOnly = True
        txt_KodeLawanTransaksi.IsReadOnly = True
        txt_NamaLawanTransaksi.IsReadOnly = True
        txt_KursAkhirBulan.IsReadOnly = True
        btn_Reset.Visibility = Visibility.Collapsed
        lbl_StatusBalance.Visibility = Visibility.Collapsed
    End Sub




    'Pembuatan Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_Urut As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim D_K As New DataGridTextColumn
    Dim Jumlah_Debet As New DataGridTextColumn
    Dim Jumlah_Kredit As New DataGridTextColumn
    Dim Keterangan_ As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_Urut")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("D_K")
        datatabelUtama.Columns.Add("Jumlah_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("Keterangan_")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Urut, "Nomor_Urut", "No.", 45, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, D_K, "D_K", "D/K", 33, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Debet, "Jumlah_Debet", "Jumlah Debet", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Kredit, "Jumlah_Kredit", "Jumlah Kredit", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub


End Class
