Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_DetailBayar

    Dim TipeTampilan As String
    Dim QueryTampilan As String
    Dim BarisTerseleksi As Int64
    Dim JumlahBaris As Int64
    Public KodeSupplier As String
    Public JalurMasuk
    Public SaldoAwalHutang As Int64
    Public SaldoAkhirHutang As Int64

    Dim NomorUrut = 0
    Dim TanggalTransaksi
    Dim NomorInvoice
    Dim TanggalHutang
    Dim TanggalBayar
    Dim JumlahTagihan
    Dim PenambahanHutang
    Dim BayarHutang
    Dim LinkNPPHU
    Dim TanggalAntaraAwal As Date
    Dim TanggalAntaraAkhir As Date
    Dim SaldoHutang As Int64
    Dim cmdBayar As OdbcCommand
    Dim drBayar As OdbcDataReader
    Dim cmdSarpem As OdbcCommand
    Dim drSarpem As OdbcDataReader
    Dim SaranaPembayaran = Nothing

    Private Sub frm_DetailBayar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Isi Konten ComboBox Periode
        cmb_Periode.Items.Clear()
        cmb_Periode.Items.Add("Semua")
        cmb_Periode.Items.Add("Periode")
        cmb_Periode.Text = "Semua"

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()
        QueryTampilan = " SELECT * FROM tbl_Pembelian " & _
            " WHERE Kode_Supplier = '" & KodeSupplier & "' AND Jenis_Pembelian = 'TEMPO' " & _
            " ORDER BY Tanggal_Invoice "
        TampilkanData()
    End Sub

    Sub TampilkanData()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.Rows.Clear()
        SaldoHutang = SaldoAwalHutang
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        TanggalHutang = "01/01/" & TahunBukuAktif
        Do While dr.Read
            TanggalAntaraAwal = TanggalHutang
            TanggalHutang = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Invoice"), 10)
            TanggalAntaraAkhir = TanggalHutang
            TanggalAntaraAkhir = TanggalAntaraAkhir.AddDays(-1) 'Kenapa dikurangi satu..? Yaitu untuk menghindari double data.
            JumlahTagihan = dr.Item("Jumlah_Tagihan")
            LoopBayar()
            TanggalTransaksi = TanggalHutang
            NomorInvoice = dr.Item("Nomor_Invoice")
            PenambahanHutang = JumlahTagihan
            BayarHutang = 0
            SaranaPembayaran = Nothing
            KodingTambahBaris()
        Loop
        TanggalAntaraAwal = TanggalHutang
        TanggalAntaraAkhir = TanggalIni
        LoopBayar()
        AksesDatabase_Transaksi(Tutup)
        SaldoAkhirHutang = SaldoHutang
        txt_SaldoAwal.Text = SaldoAwalHutang
        txt_SaldoAkhir.Text = SaldoAkhirHutang
        DataGridView.ClearSelection()
    End Sub

    Sub LoopBayar()
        cmdBayar = New OdbcCommand(" SELECT * FROM tbl_Pembelian LEFT JOIN tbl_PembayaranHutangUsaha " & _
                           " ON tbl_Pembelian.Nomor_Pembelian = tbl_PembayaranHutangUsaha.Nomor_Pembelian " & _
                           " WHERE Jenis_Pembelian = 'TEMPO' " & _
                           " AND Kode_Supplier = '" & KodeSupplier & "' " & _
                           " AND Tanggal_Bayar BETWEEN '" & TanggalFormatSimpan(TanggalAntaraAwal) & "' AND '" & TanggalFormatSimpan(TanggalAntaraAkhir) & "' " & _
                           " ORDER BY Tanggal_Bayar ", _
                           KoneksiDatabaseTransaksi)
        drBayar = cmdBayar.ExecuteReader
        Do While drBayar.Read
            LinkNPPHU = drBayar.Item("NPPHU")
            TanggalBayar = Microsoft.VisualBasic.Left(drBayar.Item("Tanggal_Bayar"), 10)
            TanggalTransaksi = TanggalBayar
            NomorInvoice = drBayar.Item("Nomor_Invoice")
            PenambahanHutang = 0
            BayarHutang = drBayar.Item("Jumlah_Bayar")
            cmdSarpem = New OdbcCommand(" SELECT * FROM tbl_PengajuanPembayaranHutangUsaha WHERE Nomor_Pengajuan = '" & LinkNPPHU & "' ", KoneksiDatabaseTransaksi)
            drSarpem = cmdSarpem.ExecuteReader
            drSarpem.Read()
            SaranaPembayaran = drSarpem.Item("Sarana_Pembayaran")
            KodingTambahBaris()
        Loop
    End Sub

    Sub KodingTambahBaris()
        NomorUrut = NomorUrut + 1
        SaldoHutang = SaldoHutang + PenambahanHutang - BayarHutang
        DataGridView.Rows.Add(NomorUrut, TanggalTransaksi, NomorInvoice, PenambahanHutang, BayarHutang, SaldoHutang, SaranaPembayaran)
    End Sub

    Private Sub txt_SaldoAwal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwal.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoAkhir_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAkhir.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoAwal_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwal.TextChanged
        Try
            If txt_SaldoAwal.Text.Trim() <> "" Then
                txt_SaldoAwal.Text = CDec(txt_SaldoAwal.Text).ToString("N0")
                txt_SaldoAwal.SelectionStart = txt_SaldoAwal.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txt_SaldoAkhir_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAkhir.TextChanged
        Try
            If txt_SaldoAkhir.Text.Trim() <> "" Then
                txt_SaldoAkhir.Text = CDec(txt_SaldoAkhir.Text).ToString("N0")
                txt_SaldoAkhir.SelectionStart = txt_SaldoAkhir.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class