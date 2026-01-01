Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputJumlah

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

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If JudulForm = Kosongan Then PesanUntukProgrammer("Judul Form belum ditentukan...!!!")

        Me.Text = JudulForm
        Me.Height = 523
        lbl_Jumlah.Text = JudulForm

        SisaTagihan = JumlahTagihan - SudahDibayar
        txt_SudahDibayar.Text = SudahDibayar
        txt_SisaTagihan.Text = SisaTagihan

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
            txt_DPP.Text = dr.Item("Dasar_Pengenaan_Pajak")
            txt_PPN.Text = dr.Item("PPN")
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

        If FungsiForm = FungsiForm_LIHAT Then
            txt_Jumlah.Enabled = False
            btn_Batal.Text = teks_Tutup
            btn_OK.Enabled = False
        Else
            txt_Jumlah.Enabled = True
            btn_Batal.Text = teks_Batal
            btn_OK.Enabled = True
            BeginInvoke(Sub() txt_Jumlah.Focus())
        End If

        If txt_DPP.Text = Kosongan Then
            pnl_DPP.Visible = False
            Me.Height -= 28
        End If
        If txt_PPN.Text = Kosongan Then
            pnl_PPN.Visible = False
            Me.Height -= 28
        End If
        If txt_PPhTerutang.Text = Kosongan Then
            pnl_PPhTerutang.Visible = False
            pnl_PPhDitanggung.Visible = False
            pnl_PPhDipotong.Visible = False
            Me.Height -= 84
        End If
        If txt_BiayaTransportasi.Text = Kosongan Then
            pnl_BiayaTransportasi.Visible = False
            Me.Height -= 28
        End If
        If txt_BiayaMaterai.Text = Kosongan Then
            pnl_BiayaMaterai.Visible = False
            Me.Height -= 28
        End If
        If txt_Retur.Text = Kosongan Then
            pnl_Retur.Visible = False
            Me.Height -= 28
        End If
    End Sub


    Sub ResetForm()
        JudulForm = Kosongan
        FungsiForm = Kosongan
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
        btn_Batal.Text = teks_Batal

        PPhTerutang_UntukIsiValue = 0
        PPhDitanggung_UntukIsiValue = 0
        PPhDipotong_UntukIsiValue = 0

        PPhTerutang_Total = 0
        PPhDitanggung_Total = 0
        PPhDipotong_Total = 0

        PPhTerutang_ValueAwal_dB = 0
        PPhDitanggung_ValueAwal_dB = 0
        PPhDipotong_ValueAwal_dB = 0

        pnl_DPP.Visible = True
        pnl_PPN.Visible = True
        pnl_PPhTerutang.Visible = True
        pnl_PPhDitanggung.Visible = True
        pnl_PPhDipotong.Visible = True
        pnl_BiayaTransportasi.Visible = True
        pnl_BiayaMaterai.Visible = True
        pnl_Retur.Visible = True
        pnl_JumlahTagihan.Visible = True
        pnl_SudahDibayar.Visible = True
        pnl_SisaTagihan.Visible = True
        pnl_JumlahInputan.Visible = True

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

    Private Sub txt_DPP_TextChanged(sender As Object, e As EventArgs) Handles txt_DPP.TextChanged
        DPP = AmbilAngka(txt_DPP.Text)
        PemecahRibuanUntukTextBox(txt_DPP)
    End Sub

    Private Sub txt_PPN_TextChanged(sender As Object, e As EventArgs) Handles txt_PPN.TextChanged
        PemecahRibuanUntukTextBox(txt_PPN)
    End Sub

    Private Sub txt_PPhTerutang_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhTerutang.TextChanged
        PPhTerutang_Total = AmbilAngka(txt_PPhTerutang.Text)
        PemecahRibuanUntukTextBox(txt_PPhTerutang)
    End Sub

    Private Sub txt_PPhDitanggung_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDitanggung.TextChanged
        PPhDitanggung_Total = AmbilAngka(txt_PPhDitanggung.Text)
        PemecahRibuanUntukTextBox(txt_PPhDitanggung)
    End Sub

    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong.TextChanged
        PPhDipotong_Total = AmbilAngka(txt_PPhDipotong.Text)
        PemecahRibuanUntukTextBox(txt_PPhDipotong)
    End Sub

    Private Sub txt_BiayaTransportasi_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaTransportasi.TextChanged
        PemecahRibuanUntukTextBox(txt_BiayaTransportasi)
    End Sub

    Private Sub txt_BiayaMaterai_TextChanged(sender As Object, e As EventArgs) Handles txt_BiayaMaterai.TextChanged
        PemecahRibuanUntukTextBox(txt_BiayaMaterai)
    End Sub

    Private Sub txt_Retur_TextChanged(sender As Object, e As EventArgs) Handles txt_Retur.TextChanged
        PemecahRibuanUntukTextBox(txt_Retur)
    End Sub

    Private Sub txt_JumlahTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTagihan.TextChanged
        JumlahTagihan = AmbilAngka(txt_JumlahTagihan.Text)
        PemecahRibuanUntukTextBox(txt_JumlahTagihan)
    End Sub

    Private Sub txt_SudahDibayar_TextChanged(sender As Object, e As EventArgs) Handles txt_SudahDibayar.TextChanged
        SudahDibayar = AmbilAngka(txt_SudahDibayar.Text)
        PemecahRibuanUntukTextBox(txt_SudahDibayar)
    End Sub

    Private Sub txt_SisaTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_SisaTagihan.TextChanged
        SisaTagihan = AmbilAngka(txt_SisaTagihan.Text)
        PemecahRibuanUntukTextBox(txt_SisaTagihan)
    End Sub
    Private Sub txt_Jumlah_TextChanged(sender As Object, e As EventArgs) Handles txt_Jumlah.TextChanged
        JumlahInputan = AmbilAngka(txt_Jumlah.Text)
        PemecahRibuanUntukTextBox(txt_Jumlah)
        RasioPPh()
    End Sub
    Private Sub txt_Jumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Jumlah.KeyPress
        If FungsiForm = FungsiForm_LIHAT Then
            KunciTotalInputan(sender, e)
        Else
            HanyaBolehInputAngkaPlus(sender, e)
        End If
    End Sub


    Private Sub TutupForm(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If DialogResult = DialogResult.OK Then
            If JumlahInputan > SisaTagihan Then
                PesanPeringatan("Silakan input angka dengan benar..!")
                e.Cancel = True
                Return
            End If
        Else
            e.Cancel = False
        End If

    End Sub

End Class