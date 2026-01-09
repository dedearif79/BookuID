Imports bcomm
Imports System.Windows
Imports System.Data.Odbc
Imports System.Windows.Input
Imports DocumentFormat.OpenXml.Drawing

Public Class wpfWin_InputJumlahBankCashInOut

    Public FungsiForm
    Public PeruntukanPembayaran
    Public JumlahInputan
    Public JudulForm
    Public Status

    Public NomorInvoice
    Public NomorBP
    Public JenisPajak
    Public KodeSetoran

    Dim DPP
    Dim JumlahTagihan
    Dim SudahDibayar
    Dim SisaTagihan


    Public PPhTerutang_UntukIsiValue As Int64
    Public PPhDitanggung_UntukIsiValue As Int64
    Public PPhDipotong_UntukIsiValue As Int64

    Dim PPhTerutang_Total
    Dim PPhDitanggung_Total
    Dim PPhDipotong_Total

    Public PPhTerutang_ValueAwal_dB
    Public PPhDitanggung_ValueAwal_dB
    Public PPhDipotong_ValueAwal_dB

    Public Pelunasan As Boolean

    Public KodeMataUang As String
    Dim Kurs As Decimal
    Dim JumlahIDR As Int64

    Public MataUang_IDR As Boolean
    Public MataUang_USD As Boolean

    Public Proses As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If JudulForm = Kosongan Then PesanUntukProgrammer("Judul Form belum ditentukan...!!!")
        If KodeMataUang = Kosongan Then PesanUntukProgrammer("Kode Mata Uang belum ditentukan...!!!")

        LogikaKodeMataUang()

        Me.Title = JudulForm
        lbl_Jumlah.Text = JudulForm

        SisaTagihan = JumlahTagihan - SudahDibayar
        txt_SudahDibayar.Text = SudahDibayar
        txt_SisaTagihan.Text = SisaTagihan

        If MataUang_IDR Then
            If PeruntukanPembayaran = Peruntukan_PembayaranHutangUsaha_NonAfiliasi _
                Or PeruntukanPembayaran = Peruntukan_PencairanPiutangUsaha_NonAfiliasi _
                Then
                Dim Tabel = Kosongan
                Select Case PeruntukanPembayaran
                    Case Peruntukan_PembayaranHutangUsaha_NonAfiliasi
                        Tabel = "tbl_Pembelian_Invoice"
                    Case Peruntukan_PencairanPiutangUsaha_NonAfiliasi
                        Tabel = "tbl_Penjualan_Invoice"
                End Select
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" SELECT * FROM " & Tabel &
                                  " WHERE Nomor_Invoice = '" & NomorInvoice & "' ", KoneksiDatabaseTransaksi)
                dr_ExecuteReader()
                dr.Read()
                txt_DPP.Text = FormatUlangInt64(dr.Item("Dasar_Pengenaan_Pajak"))
                txt_PPN.Text = FormatUlangInt64(dr.Item("PPN"))
                JenisPajak = KonversiJenisPPhKeJenisPajak(dr.Item("Jenis_PPh"))
                KodeSetoran = dr.Item("Kode_Setoran")
                txt_PPhTerutang.Text = dr.Item("PPh_Terutang")
                txt_PPhDitanggung.Text = dr.Item("PPh_Ditanggung")
                txt_PPhDipotong.Text = dr.Item("PPh_Dipotong")
                txt_BiayaTransportasi.Text = dr.Item("Biaya_Transportasi")
                If PeruntukanPembayaran = Peruntukan_PembayaranHutangUsaha_NonAfiliasi Then txt_BiayaMaterai.Text = dr.Item("Biaya_Materai")
                txt_Retur.Text = dr.Item("Retur_DPP") + dr.Item("Retur_PPN")
                AksesDatabase_Transaksi(Tutup)
                RasioPPh()
            End If
            lbl_PPhTerutang.Text = JenisPajak
        End If


        If FungsiForm = FungsiForm_LIHAT Then
            txt_Jumlah.IsEnabled = False
            txt_Jumlah.IsReadOnly = True
            btn_Batal.Content = teks_Tutup
            btn_OK.IsEnabled = False
        Else
            txt_Jumlah.IsEnabled = True
            txt_Jumlah.IsReadOnly = False
            btn_Batal.Content = teks_Batal
            btn_OK.IsEnabled = True
            txt_Jumlah.Focus()
        End If

        If txt_DPP.Text = Kosongan Then
            lbl_DPP.Visibility = Visibility.Collapsed
            txt_DPP.Visibility = Visibility.Collapsed
        End If
        If txt_PPN.Text = Kosongan Then
            lbl_PPN.Visibility = Visibility.Collapsed
            txt_PPN.Visibility = Visibility.Collapsed
        End If
        If txt_PPhTerutang.Text = Kosongan Then
            lbl_PPhTerutang.Visibility = Visibility.Collapsed
            lbl_PPhDItanggung.Visibility = Visibility.Collapsed
            lbl_PPhDipotong.Visibility = Visibility.Collapsed
            txt_PPhTerutang.Visibility = Visibility.Collapsed
            txt_PPhDitanggung.Visibility = Visibility.Collapsed
            txt_PPhDipotong.Visibility = Visibility.Collapsed
        End If
        If txt_BiayaTransportasi.Text = Kosongan Then
            lbl_BiayaTransportasi.Visibility = Visibility.Collapsed
            txt_BiayaTransportasi.Visibility = Visibility.Collapsed
        End If
        If txt_BiayaMaterai.Text = Kosongan Then
            lbl_BiayaMaterai.Visibility = Visibility.Collapsed
            txt_BiayaMaterai.Visibility = Visibility.Collapsed
        End If
        If txt_Retur.Text = Kosongan Then
            lbl_Retur.Visibility = Visibility.Collapsed
            txt_Retur.Visibility = Visibility.Collapsed
        End If

        PerhitunganKurs()

        ProsesLoadingForm = False

    End Sub

    Sub ResetForm()

        JudulForm = Kosongan
        FungsiForm = Kosongan
        KodeMataUang = Kosongan
        MataUang_IDR = False
        MataUang_USD = False
        PeruntukanPembayaran = Kosongan
        NomorInvoice = Kosongan
        txt_DPP.Text = Kosongan
        txt_PPN.Text = Kosongan
        txt_PPhTerutang.Text = Kosongan
        txt_PPhDitanggung.Text = Kosongan
        txt_PPhDipotong.Text = Kosongan
        txt_BiayaTransportasi.Text = Kosongan
        txt_BiayaMaterai.Text = Kosongan
        txt_Retur.Text = Kosongan
        txt_JumlahTagihan.Text = Kosongan
        txt_SudahDibayar.Text = Kosongan
        txt_SisaTagihan.Text = Kosongan
        txt_Jumlah.Text = Kosongan
        txt_Kurs.Text = Kosongan
        txt_JumlahIDR.Text = Kosongan
        btn_Batal.Content = teks_Batal

        PPhTerutang_UntukIsiValue = 0
        PPhDitanggung_UntukIsiValue = 0
        PPhDipotong_UntukIsiValue = 0

        PPhTerutang_Total = 0
        PPhDitanggung_Total = 0
        PPhDipotong_Total = 0

        PPhTerutang_ValueAwal_dB = 0
        PPhDitanggung_ValueAwal_dB = 0
        PPhDipotong_ValueAwal_dB = 0

        lbl_DPP.Visibility = Visibility.Visible
        lbl_PPN.Visibility = Visibility.Visible
        lbl_PPhTerutang.Visibility = Visibility.Visible
        lbl_PPhDItanggung.Visibility = Visibility.Visible
        lbl_PPhDipotong.Visibility = Visibility.Visible
        lbl_BiayaTransportasi.Visibility = Visibility.Visible
        lbl_BiayaMaterai.Visibility = Visibility.Visible
        lbl_Retur.Visibility = Visibility.Visible
        lbl_JumlahTagihan.Visibility = Visibility.Visible
        lbl_SudahDibayar.Visibility = Visibility.Visible
        lbl_SisaTagihan.Visibility = Visibility.Visible
        lbl_Jumlah.Visibility = Visibility.Visible

        txt_DPP.Visibility = Visibility.Visible
        txt_PPN.Visibility = Visibility.Visible
        txt_PPhTerutang.Visibility = Visibility.Visible
        txt_PPhDitanggung.Visibility = Visibility.Visible
        txt_PPhDipotong.Visibility = Visibility.Visible
        txt_BiayaTransportasi.Visibility = Visibility.Visible
        txt_BiayaMaterai.Visibility = Visibility.Visible
        txt_Retur.Visibility = Visibility.Visible
        txt_JumlahTagihan.Visibility = Visibility.Visible
        txt_SudahDibayar.Visibility = Visibility.Visible
        txt_SisaTagihan.Visibility = Visibility.Visible
        txt_Jumlah.Visibility = Visibility.Visible

    End Sub


    Sub LogikaKodeMataUang()

        If KodeMataUang = KodeMataUang_IDR Then
            MataUang_IDR = True
            MataUang_USD = False
            txt_Kurs.Text = 1
            lbl_Kurs.Visibility = Visibility.Collapsed
            txt_Kurs.Visibility = Visibility.Collapsed
            lbl_JumlahIDR.Visibility = Visibility.Collapsed
            txt_JumlahIDR.Visibility = Visibility.Collapsed
            Convert.ToInt64(JumlahTagihan)
            Convert.ToInt64(SudahDibayar)
            Convert.ToInt64(SisaTagihan)
            Convert.ToInt64(JumlahInputan)
            txt_JumlahTagihan.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_SudahDibayar.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_SisaTagihan.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlusReadOnly), Style)
            txt_Jumlah.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaSeparatePlus), Style)
        Else
            MataUang_USD = True
            MataUang_IDR = False
            lbl_Kurs.Visibility = Visibility.Visible
            txt_Kurs.Visibility = Visibility.Visible
            lbl_JumlahIDR.Visibility = Visibility.Visible
            txt_JumlahIDR.Visibility = Visibility.Visible
            Convert.ToDecimal(JumlahTagihan)
            Convert.ToDecimal(SudahDibayar)
            Convert.ToDecimal(SisaTagihan)
            Convert.ToDecimal(JumlahInputan)
            txt_JumlahTagihan.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_SudahDibayar.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_SisaTagihan.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlusReadOnly), Style)
            txt_Jumlah.Style = CType(Me.FindResource(style_TextBoxFormDialogAngkaAsingPlus), Style)
        End If

    End Sub

    Sub RasioPPh()

        Dim RasioBayar = JumlahInputan / JumlahTagihan
        Dim PPhTerutang_SudahDibayar = 0
        Dim PPhDitanggung_SudahDibayar = 0
        Dim PPhDipotong_SudahDibayar = 0

        If JumlahInputan >= SisaTagihan Then '(Jika pembayaran untuk pelunasan) :
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                  " WHERE Nomor_BP      = '" & NomorBP & "' " &
                                  " AND Status_Invoice  = '" & Status_Dibayar & "' ", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            Do While dr.Read
                PPhTerutang_SudahDibayar += dr.Item("PPh_Terutang")
                PPhDitanggung_SudahDibayar += dr.Item("PPh_Ditanggung")
                PPhDipotong_SudahDibayar += dr.Item("PPh_Dipotong")
            Loop
            AksesDatabase_Transaksi(Tutup)
            If FungsiForm = FungsiForm_POSTING And Status <> Status_Dibayar Then
                'Jika dalam rangka ngedit data yang sudah disetujui :
                PPhTerutang_SudahDibayar -= PPhTerutang_ValueAwal_dB
                PPhDitanggung_SudahDibayar -= PPhDitanggung_ValueAwal_dB
                PPhDipotong_SudahDibayar -= PPhDipotong_ValueAwal_dB
            End If
            PPhTerutang_UntukIsiValue = PPhTerutang_Total - PPhTerutang_SudahDibayar
            PPhDitanggung_UntukIsiValue = PPhDitanggung_Total - PPhDitanggung_SudahDibayar
            PPhDipotong_UntukIsiValue = PPhDipotong_Total - PPhDipotong_SudahDibayar
            Pelunasan = True
        Else '(Jika belum pelunasan) :
            PPhTerutang_UntukIsiValue = PPhTerutang_Total * RasioBayar
            PPhDitanggung_UntukIsiValue = PPhDitanggung_Total * RasioBayar
            PPhDipotong_UntukIsiValue = PPhDipotong_Total * RasioBayar
            Pelunasan = False
        End If

        'Jika sudah tidak ada masalah, baris koding yang nonaktif di bawah ini sebaiknya dihapus saja.
        'If PPhTerutang_UntukIsiValue > PPhTerutang_ValueAwal_dB Then PPhTerutang_UntukIsiValue = PPhTerutang_ValueAwal_dB
        'If PPhDitanggung_UntukIsiValue > PPhDitanggung_ValueAwal_dB Then PPhDitanggung_UntukIsiValue = PPhDitanggung_ValueAwal_dB
        'If PPhDipotong_UntukIsiValue > PPhDipotong_ValueAwal_dB Then PPhDipotong_UntukIsiValue = PPhDipotong_ValueAwal_dB

    End Sub



    Private Sub txt_DPP_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_DPP.TextChanged
        DPP = AmbilAngka(txt_DPP.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DPP)
    End Sub


    Private Sub txt_PPN_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_PPN.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_PPN)
    End Sub


    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_PPhTerutang.TextChanged
        PPhTerutang_Total = AmbilAngka(txt_PPhTerutang.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhTerutang)
    End Sub


    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung_Total = AmbilAngka(txt_PPhDitanggung.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhDitanggung)
    End Sub


    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong_Total = AmbilAngka(txt_PPhDipotong.Text)
        PemecahRibuanUntukTextBox_WPF(txt_PPhDipotong)
    End Sub


    Private Sub txt_BiayaTransportasi_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_BiayaTransportasi.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_BiayaTransportasi)
    End Sub


    Private Sub txt_BiayaMaterai_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_BiayaMaterai.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_BiayaMaterai)
    End Sub


    Private Sub txt_Retur_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_Retur.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_Retur)
    End Sub


    Private Sub txt_JumlahTagihan_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_JumlahTagihan.TextChanged
        If MataUang_IDR Then JumlahTagihan = AmbilAngka(txt_JumlahTagihan.Text)
        If MataUang_USD Then JumlahTagihan = AmbilAngka_Asing(txt_JumlahTagihan.Text)
    End Sub

    Private Sub txt_SudahDibayar_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_SudahDibayar.TextChanged
        If MataUang_IDR Then SudahDibayar = AmbilAngka(txt_SudahDibayar.Text)
        If MataUang_USD Then SudahDibayar = AmbilAngka_Asing(txt_SudahDibayar.Text)
    End Sub


    Private Sub txt_SisaTagihan_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_SisaTagihan.TextChanged
        If MataUang_IDR Then SisaTagihan = AmbilAngka(txt_SisaTagihan.Text)
        If MataUang_USD Then SisaTagihan = AmbilAngka_Asing(txt_SisaTagihan.Text)
    End Sub


    Private Sub txt_Jumlah_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_Jumlah.TextChanged
        If MataUang_IDR Then
            JumlahInputan = AmbilAngka(txt_Jumlah.Text)
            RasioPPh()
        Else
            JumlahInputan = AmbilAngka_Asing(txt_Jumlah.Text)
            PerhitunganKurs()
        End If
    End Sub


    Private Sub txt_Kurs_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_Kurs.TextChanged
        Kurs = AmbilAngka_Desimal(txt_Kurs.Text)
        PerhitunganKurs()
    End Sub


    Private Sub txt_JumlahIDR_TextChanged(sender As Object, e As Controls.TextChangedEventArgs) Handles txt_JumlahIDR.TextChanged
        JumlahIDR = AmbilAngka(txt_JumlahIDR.Text)
        PemecahRibuanUntukTextBox_WPF(txt_JumlahIDR)
    End Sub


    Sub PerhitunganKurs()
        txt_JumlahIDR.Text = AmbilValue_NilaiMataUang_BulatKeAtas(KodeMataUang, Kurs, JumlahInputan)
    End Sub



    Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click
        If JumlahInputan = 0 Then
            PesanPeringatan("Silakan input angka dengan benar.")
        ElseIf JumlahInputan > SisaTagihan Then
            PesanPeringatan("Silakan input angka dengan benar.")
        Else
            Proses = True
            Close()
        End If
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Proses = False
        Close()
    End Sub


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        txt_DPP.IsReadOnly = True
        txt_PPN.IsReadOnly = True
        txt_PPhTerutang.IsReadOnly = True
        txt_PPhDitanggung.IsReadOnly = True
        txt_PPhDipotong.IsReadOnly = True
        txt_BiayaTransportasi.IsReadOnly = True
        txt_BiayaMaterai.IsReadOnly = True
        txt_Retur.IsReadOnly = True
        txt_JumlahTagihan.IsReadOnly = True
        txt_SudahDibayar.IsReadOnly = True
        txt_SisaTagihan.IsReadOnly = True
        txt_Kurs.IsReadOnly = True
        txt_JumlahIDR.IsReadOnly = True
    End Sub

End Class
