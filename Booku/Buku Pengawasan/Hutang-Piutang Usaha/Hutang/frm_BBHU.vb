Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_BBHU

    Dim TipeTampilan As String
    Dim QueryTampilan As String
    Dim BarisTerseleksi As Int64
    Dim JumlahBaris As Int64
    Public KodeSupplier As String
    Public JalurMasuk
    Public SaldoAwalHutang As Int64
    Public SaldoAkhirHutang As Int64

    Dim JumlahTagihanTahunLalu = 0
    Dim JumlahBayarTahunLalu = 0
    Dim SisaHutangTahunLalu = 0

    Dim NomorUrut
    Dim NomorPembelian
    Dim Tanggal
    Dim TanggalHutang
    Dim TanggalBayar
    Dim Referensi
    Dim NomorInvoice
    Dim NomorNPPHU
    Dim DPP As Int64
    Dim PPN As Int64
    Dim PPhDipotong As Int64
    Dim JumlahTagihan
    Dim JumlahBayar
    Dim Kredit As Int64
    Dim Debet As Int64
    Dim Saldo As Int64
    Dim TanggalAntaraAwal As Date
    Dim TanggalAntaraAkhir As Date
    Dim cmdBAYAR As OdbcCommand
    Dim drBAYAR As OdbcDataReader
    Dim cmdJURNAL As OdbcCommand
    Dim drJURNAL As OdbcDataReader
    Dim SaranaPembayaran = Nothing
    Dim clm_NomorUrut, clm_Tanggal, clm_Referensi, clm_DPP, clm_PPN, clm_PPhDipotong, clm_Debet, clm_Kredit, clm_Saldo, clm_SaranaPembayaran

    Private Sub frm_BBHU_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Isi Konten ComboBox Periode
        cmb_Periode.Items.Clear()
        cmb_Periode.Items.Add("Semua")
        cmb_Periode.Items.Add("Periode")
        cmb_Periode.Text = "Semua"

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
        DataTabelUtama.Rows.Clear()
        NomorUrut = 0
        Saldo = 0

        '-------------------------------------------------------------------------------------------
        'Data Sisa Hutang Usaha Tahun Lalu :
        QueryTampilan = " SELECT * FROM tbl_Pembelian " & _
            " WHERE Kode_Supplier = '" & KodeSupplier & "' AND  Jenis_Pembelian = 'TEMPO' AND (Tanggal_Invoice < '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " & _
            " ORDER BY Tanggal_Invoice "
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorPembelian = dr.Item("Nomor_Pembelian")
            TanggalHutang = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Invoice"), 10)
            NomorInvoice = dr.Item("Nomor_Invoice")
            JumlahTagihanTahunLalu = dr.Item("Jumlah_Tagihan")
            Tanggal = TanggalHutang
            Referensi = NomorInvoice
            Dim cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi)
            Dim drBAYAR = cmdBAYAR.ExecuteReader
            drBAYAR.Read()
            JumlahBayarTahunLalu = drBAYAR.Item("Jumlah_Bayar")
            SisaHutangTahunLalu = JumlahTagihanTahunLalu - JumlahBayarTahunLalu
            Kredit = SisaHutangTahunLalu
            Debet = 0
            Saldo = Saldo + Kredit - Debet
            SaranaPembayaran = Nothing
            clm_Tanggal = Tanggal
            clm_Referensi = Referensi
            clm_SaranaPembayaran = SaranaPembayaran
            KodingTambahBaris()
        Loop
        AksesDatabase_Transaksi(Tutup)

        SaldoAwalHutang = Saldo
        If SaldoAwalHutang = 0 Then
            txt_SaldoAwal.Text = StripKosong
        Else
            txt_SaldoAwal.Text = SaldoAwalHutang
        End If


        '-------------------------------------------------------------------------------------------
        'Data Hutang Tahun Ini :

        TanggalHutang = "01/01/" & TahunBukuAktif
        Dim TambahBaris = 0

        If JalurMasuk = Halaman_DPHU Then
            QueryTampilan = " SELECT * FROM tbl_Pembelian " & _
                " WHERE Kode_Supplier = '" & KodeSupplier & "' AND  Jenis_Pembelian = 'TEMPO' AND (Tanggal_Invoice >= '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') " & _
                " ORDER BY Tanggal_Invoice "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            Do While dr.Read
                TambahBaris = 0
                NomorPembelian = dr.Item("Nomor_Pembelian")
                TanggalAntaraAwal = TanggalHutang
                TanggalHutang = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Invoice"), 10)
                TanggalAntaraAkhir = TanggalHutang
                TanggalAntaraAkhir = TanggalAntaraAkhir.AddDays(-1) 'Kenapa dikurangi satu..? Yaitu untuk menghindari double data.
                LoopBayar()
                DPP = dr.Item("DPP")
                PPN = dr.Item("PPN")
                PPhDipotong = dr.Item("Jumlah_PPh_Dipotong")
                JumlahTagihan = dr.Item("Jumlah_Tagihan")
                Tanggal = TanggalHutang
                NomorInvoice = dr.Item("Nomor_Invoice")
                Referensi = NomorInvoice
                Kredit = JumlahTagihan
                Debet = 0
                Saldo = Saldo + Kredit - Debet
                SaranaPembayaran = Nothing
                clm_Tanggal = Tanggal
                clm_Referensi = Referensi
                clm_SaranaPembayaran = SaranaPembayaran
                KodingTambahBaris()
            Loop
            TanggalAntaraAwal = TanggalHutang
            TanggalAntaraAkhir = TanggalIni
            LoopBayar()
        End If

        If JalurMasuk = Halaman_BUKUBESAR Then
            QueryTampilan = " SELECT * FROM tbl_Transaksi " & _
                " WHERE Kode_Lawan_Transaksi = '" & KodeSupplier & "' AND COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' " & " AND Status_Approve = 1 " & _
                " ORDER BY Tanggal_Transaksi "
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            Do While dr.Read
                TambahBaris = 0
                DPP = 0
                PPN = 0
                PPhDipotong = 0
                Tanggal = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Transaksi"), 10)
                Kredit = dr.Item("Jumlah_Kredit")
                Debet = dr.Item("Jumlah_debet")
                SaranaPembayaran = Nothing
                If Kredit > 0 Then
                    NomorInvoice = dr.Item("Nomor_Invoice")
                Else
                    NomorInvoice = StripKosong
                End If
                Referensi = NomorInvoice
                clm_Tanggal = Tanggal
                clm_Referensi = Referensi
                clm_SaranaPembayaran = SaranaPembayaran
                Saldo = Saldo + Kredit - Debet
                KodingTambahBaris()
            Loop
        End If

        AksesDatabase_Transaksi(Tutup)

        SaldoAkhirHutang = Saldo
        If SaldoAkhirHutang = 0 Then
            txt_SaldoAkhir.Text = StripKosong
        Else
            txt_SaldoAkhir.Text = SaldoAkhirHutang
        End If

        DataTabelUtama.ClearSelection()

        'Coloring
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If row.Cells("Tanggal_").Value < AwalTahunBukuAktif Then
                row.DefaultCellStyle.ForeColor = Color.Blue
            Else
                row.DefaultCellStyle.ForeColor = Color.Black
            End If
        Next

    End Sub

    Sub LoopBayar()
        cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_PengajuanPembayaranHutangUsaha " & _
                                   " WHERE Kode_Supplier = '" & KodeSupplier & "' " & _
                                   " AND Status = 'DIBAYAR' " & _
                                   " AND Tanggal_Bayar BETWEEN '" & TanggalFormatSimpan(TanggalAntaraAwal) & "' AND '" & TanggalFormatSimpan(TanggalAntaraAkhir) & "' " & _
                                   " ORDER BY Tanggal_Bayar ", _
                                   KoneksiDatabaseTransaksi)
        'Kenapa nembaknya ke tabel tbl_PengajuanPembayaranHutangUsaha (NPPHU)..?
        'Kenapa bukan ke tabel tbl_PembayaranHutangUsaha..?
        'Karena pembayaran ke satu perusahaan memungkinkan banyak invoice.
        'Data yang diambil bukan data per-invoice, tapi per-pembayaran (bundel)
        'Disamping itu, juga karena ingin mengambil value Sarana Pembayaran, dan lainnya.
        drBAYAR = cmdBAYAR.ExecuteReader
        Dim Baris = 0
        Dim TambahBaris = 0
        Dim DebetPerItem = 0
        Do While drBAYAR.Read
            TanggalBayar = Microsoft.VisualBasic.Left(drBAYAR.Item("Tanggal_Bayar"), 10)
            Tanggal = TanggalBayar
            NomorNPPHU = drBAYAR.Item("Nomor_Pengajuan")
            Referensi = NomorNPPHU
            DPP = 0
            PPN = 0
            PPhDipotong = 0
            JumlahBayar = drBAYAR.Item("Jumlah_Bayar")
            DebetPerItem = JumlahBayar
            Kredit = 0
            SaranaPembayaran = drBAYAR.Item("Sarana_Pembayaran")
            If Referensi <> clm_Referensi And Baris > 0 Then
                TambahBaris = 1
            Else
                TambahBaris = 0
            End If
            If TambahBaris = 1 Then
                Saldo = Saldo - Debet
                KodingTambahBaris()
            End If
            clm_Tanggal = Tanggal
            clm_Referensi = Referensi
            clm_SaranaPembayaran = SaranaPembayaran
            If TambahBaris = 1 Then
                Debet = DebetPerItem
            Else
                Debet = Debet + DebetPerItem
            End If
            Baris = Baris + 1
        Loop
        If Baris > 0 Then
            Saldo = Saldo - Debet
            KodingTambahBaris()
        End If
    End Sub

    Sub KodingTambahBaris()
        NomorUrut = NomorUrut + 1
        clm_NomorUrut = NomorUrut
        If DPP = 0 Then
            clm_DPP = StripKosong
        Else
            clm_DPP = DPP
        End If
        If PPN = 0 Then
            clm_PPN = StripKosong
        Else
            clm_PPN = PPN
        End If
        If PPhDipotong = 0 Then
            clm_PPhDipotong = StripKosong
        Else
            clm_PPhDipotong = PPhDipotong
        End If
        If Debet = 0 Then
            clm_Debet = StripKosong
        Else
            clm_Debet = Debet
        End If
        If Kredit = 0 Then
            clm_Kredit = StripKosong
        Else
            clm_Kredit = Kredit
        End If
        If Saldo = 0 Then
            clm_Saldo = StripKosong
        Else
            clm_Saldo = Saldo
        End If
        DataTabelUtama.Rows.Add(clm_NomorUrut, clm_Tanggal, clm_Referensi, clm_DPP, clm_PPN, clm_PPhDipotong, clm_Debet, clm_Kredit, clm_Saldo, clm_SaranaPembayaran)
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