Imports bcomm
Imports System.Data.Odbc

Public Class frm_BukuPembantuHutangPemegangSaham

    Public JudulForm
    Public FungsiForm
    Public JalurMasuk

    Public NomorBPHPS
    Public JumlahPinjaman
    Public SaldoAwal
    Public SaldoAkhir

    Dim NomorUrut
    Dim AngsuranKe
    Dim TanggalAngsuran
    Dim JumlahAngsuran
    Dim TotalAngsuran
    Dim Saldo
    Dim Keterangan

    Dim BarisTerseleksi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        TampilkanData()

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesResetForm = True
        FungsiForm = Kosongan


        ProsesResetForm = False

    End Sub

    Sub TampilkanData()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangPemegangSaham " &
                              " WHERE Nomor_BPHPS = '" & NomorBPHPS & "' " &
                              " ORDER BY Tanggal_Angsuran ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Saldo = SaldoAwal
        TotalAngsuran = 0
        NomorUrut = 0
        Do While dr.Read
            NomorUrut += 1
            AngsuranKe = "Ke-" & NomorUrut.ToString
            TanggalAngsuran = TanggalFormatTampilan(dr.Item("Tanggal_Angsuran"))
            JumlahAngsuran = dr.Item("Jumlah_Angsuran")
            TotalAngsuran += JumlahAngsuran
            Saldo -= JumlahAngsuran
            Keterangan = dr.Item("Keterangan")
            If Saldo = 0 Then Saldo = StripKosong
            DataTabelUtama.Rows.Add(AngsuranKe, TanggalAngsuran, JumlahAngsuran, Saldo, Keterangan)
        Loop
        AksesDatabase_Transaksi(Tutup)

        txt_SaldoAkhir.Text = Saldo

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
    End Sub

    Private Sub txt_JumlahPinjaman_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPinjaman.TextChanged
        JumlahPinjaman = AmbilAngka(txt_JumlahPinjaman.Text)
        PemecahRibuanUntukTextBox(txt_JumlahPinjaman)
    End Sub
    Private Sub txt_JumlahPinjaman_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahPinjaman.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_SaldoAwal_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwal.TextChanged
        SaldoAwal = AmbilAngka(txt_SaldoAwal.Text)
        PemecahRibuanUntukTextBox(txt_SaldoAwal)
    End Sub
    Private Sub txt_SaldoAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwal.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_SaldoAkhir_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAkhir.TextChanged
        SaldoAkhir = AmbilAngka(txt_SaldoAkhir.Text)
        PemecahRibuanUntukTextBox(txt_SaldoAkhir)
    End Sub
    Private Sub txt_SaldoAkhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAkhir.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
    End Sub

End Class