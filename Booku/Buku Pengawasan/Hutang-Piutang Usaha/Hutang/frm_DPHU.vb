Imports bcomm
Imports System.Data.Odbc

Public Class frm_DPHU

    Dim TipeTampilan As String
    Dim QueryTampilan As String
    Dim BarisTerseleksi As Integer
    Dim JumlahBaris As Int64
    Dim FilterSupplier

    'Variabel Tampilan/Tabel :
    Dim Baris = 0
    Dim NomorUrut
    Dim KodeSupplier
    Dim NamaSupplier
    Dim SaldoAwalHutangTahunLalu
    Dim JumlahInvoice
    Dim DPP
    Dim PPN
    Dim PPhDipotong
    Dim JumlahTagihan
    Dim JumlahBayar
    Dim JumlahBayarHutangTahunLalu
    Dim SisaHutang
    Dim Rekap_SaldoAwalHutangTahunLalu
    Dim Rekap_DPP
    Dim Rekap_PPN
    Dim Rekap_PPhDipotong
    Dim Rekap_JumlahTagihan
    Dim Rekap_JumlahBayar
    Dim Rekap_SisaHutang


    Dim ProsesLoading As Boolean

    Private Sub frm_DBPHU_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoading = True

        KontenComboSupplier()

        TampilkanData()

        ProsesLoading = False

    End Sub

    Sub TampilkanData()

        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        Dim DataSumber As DataGridView = X2_frm_BukuPengawasanHutangUsaha_X2.DataTabelUtama

        NomorUrut = 0
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT Kode_Mitra FROM tbl_LawanTransaksi ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            KodeSupplier = dr.Item("Kode_Mitra")
            Rekap_SaldoAwalHutangTahunLalu = 0
            JumlahInvoice = 0
            Rekap_DPP = 0
            Rekap_PPN = 0
            Rekap_PPhDipotong = 0
            Rekap_JumlahTagihan = 0
            Rekap_JumlahBayar = 0
            Rekap_SisaHutang = 0
            For Each row As DataGridViewRow In DataSumber.Rows
                If row.Cells("Kode_Supplier").Value = KodeSupplier Then
                    Dim NomorBPHU = row.Cells("Nomor_BPHU").Value
                    NamaSupplier = row.Cells("Nama_Supplier").Value
                    JumlahInvoice += 1
                    If Microsoft.VisualBasic.Left(NomorBPHU, PanjangTeks_AwalanBPHU_PlusTahunBuku) <> AwalanBPHU_PlusTahunBuku Then '(Jika termasuk hutang tahun lalu, maka)
                        DPP = 0
                        PPN = 0
                        PPhDipotong = 0
                        JumlahTagihan = AmbilAngka(row.Cells("Jumlah_Tagihan").Value)
                        AksesDatabase_Transaksi(Buka)
                        Dim queryBayarHutangTahunLalu = " SELECT * FROM tbl_PembayaranHutangUsaha " & _
                            " WHERE Nomor_BPHU = '" & NomorBPHU & "' " & _
                            " AND (Tanggal_Bayar < '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "') "
                        Dim cmdBayarHutangTahunLalu = New OdbcCommand(queryBayarHutangTahunLalu, KoneksiDatabaseTransaksi)
                        Dim drBayarHutangTahunLalu = cmdBayarHutangTahunLalu.ExecuteReader
                        If drBayarHutangTahunLalu.HasRows Then
                            JumlahBayarHutangTahunLalu = drBayarHutangTahunLalu.Item("Jumlah_Bayar")
                        Else
                            JumlahBayarHutangTahunLalu = 0
                        End If
                        AksesDatabase_Transaksi(Tutup)
                        JumlahBayar = AmbilAngka(row.Cells("Jumlah_Bayar").Value) - JumlahBayarHutangTahunLalu
                        SisaHutang = 0
                        SaldoAwalHutangTahunLalu = AmbilAngka(JumlahTagihan) - JumlahBayarHutangTahunLalu
                    Else '(Jika termasuk hutang tahun buku aktif, maka)
                        DPP = AmbilAngka(row.Cells("DPP_").Value)
                        PPN = AmbilAngka(row.Cells("PPN_").Value)
                        PPhDipotong = AmbilAngka(row.Cells("Jumlah_PPh_Dipotong").Value)
                        JumlahBayar = AmbilAngka(row.Cells("Jumlah_Bayar").Value)
                        SisaHutang = AmbilAngka(JumlahTagihan) - AmbilAngka(JumlahBayar)
                        SaldoAwalHutangTahunLalu = 0
                    End If
                    Rekap_SaldoAwalHutangTahunLalu = AmbilAngka(Rekap_SaldoAwalHutangTahunLalu) + AmbilAngka(SaldoAwalHutangTahunLalu)
                    Rekap_DPP = AmbilAngka(Rekap_DPP) + AmbilAngka(DPP)
                    Rekap_PPN = AmbilAngka(Rekap_PPN) + AmbilAngka(PPN)
                    Rekap_PPhDipotong = AmbilAngka(Rekap_PPhDipotong) + AmbilAngka(PPhDipotong)
                    Rekap_JumlahTagihan = AmbilAngka(Rekap_SaldoAwalHutangTahunLalu) + AmbilAngka(Rekap_DPP) + AmbilAngka(Rekap_PPN) - AmbilAngka(Rekap_PPhDipotong)
                    Rekap_JumlahBayar = AmbilAngka(Rekap_JumlahBayar) + AmbilAngka(JumlahBayar)
                    Rekap_SisaHutang = AmbilAngka(Rekap_JumlahTagihan) - AmbilAngka(Rekap_JumlahBayar)
                    If AmbilAngka(Rekap_SaldoAwalHutangTahunLalu) = 0 Then Rekap_SaldoAwalHutangTahunLalu = StripKosong
                    If AmbilAngka(Rekap_DPP) = 0 Then Rekap_DPP = StripKosong
                    If AmbilAngka(Rekap_PPN) = 0 Then Rekap_PPN = StripKosong
                    If AmbilAngka(Rekap_PPhDipotong) = 0 Then Rekap_PPhDipotong = StripKosong
                    If AmbilAngka(Rekap_JumlahTagihan) = 0 Then Rekap_JumlahTagihan = StripKosong
                    If AmbilAngka(Rekap_JumlahBayar) = 0 Then Rekap_JumlahBayar = StripKosong
                    If AmbilAngka(Rekap_SisaHutang) = 0 Then Rekap_SisaHutang = StripKosong
                End If
            Next
            If JumlahInvoice > 0 Then 'Jika tidak ada invoice maka tidak perlu ditampilkan.
                KodingTambahBaris()
            End If
        Loop
        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub KodingTambahBaris()
        Select Case FilterSupplier
            Case "SEMUA"
                SubKodingTambahBaris()
            Case KodeSupplier
                If KodeSupplier = FilterSupplier Then SubKodingTambahBaris()
        End Select
    End Sub

    Sub SubKodingTambahBaris()
        NomorUrut += 1
        DataTabelUtama.Rows.Add(NomorUrut, KodeSupplier, NamaSupplier, Rekap_SaldoAwalHutangTahunLalu, JumlahInvoice, _
                                Rekap_DPP, Rekap_PPN, Rekap_PPhDipotong, Rekap_JumlahTagihan, Rekap_JumlahBayar, Rekap_SisaHutang)
    End Sub

    Sub BersihkanSeleksi()
        BeginInvoke(Sub() DataTabelUtama.ClearSelection())
        BarisTerseleksi = -1
        btn_BBHU.Enabled = False
        btn_DetailBayar.Enabled = False
    End Sub

    Sub KontenComboSupplier()
        cmb_Supplier.Items.Clear() 'Bersihkan dulu
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand("SELECT * FROM tbl_LawanTransaksi", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        cmb_Supplier.Items.Add("SEMUA")
        Do While dr.Read
            Dim KodeMitra = dr.Item("Kode_Mitra")
            Dim NamaMitra = dr.Item("Nama_Mitra")
            If Len(KodeMitra) = 1 Then KodeMitra = KodeMitra & "        "
            If Len(KodeMitra) = 2 Then KodeMitra = KodeMitra & "       "
            If Len(KodeMitra) = 3 Then KodeMitra = KodeMitra & "      "
            If Len(KodeMitra) = 4 Then KodeMitra = KodeMitra & "     "
            If Len(KodeMitra) = 5 Then KodeMitra = KodeMitra & "    "
            If Len(KodeMitra) = 6 Then KodeMitra = KodeMitra & "   "
            If Len(KodeMitra) = 7 Then KodeMitra = KodeMitra & "  "
            If Len(KodeMitra) = 8 Then KodeMitra = KodeMitra & " "
            cmb_Supplier.Items.Add(KodeMitra & " : " & NamaMitra)
        Loop
        cmb_Supplier.Text = "SEMUA"
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        If BarisTerseleksi >= 0 Then
            btn_BBHU.Enabled = True
            btn_DetailBayar.Enabled = True
        Else
            btn_BBHU.Enabled = False
            btn_DetailBayar.Enabled = False
        End If
    End Sub

    Private Sub btn_DetailBayar_Click(sender As Object, e As EventArgs) Handles btn_DetailBayar.Click
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        frm_DetailBayar.KodeSupplier = DataTabelUtama.Item("Kode_Supplier", BarisTerseleksi).Value
        frm_DetailBayar.lbl_NamaSupplier.Text = DataTabelUtama.Item("Nama_Supplier", BarisTerseleksi).Value
        frm_DetailBayar.SaldoAwalHutang = DataTabelUtama.Item("Saldo_Awal", BarisTerseleksi).Value
        frm_DetailBayar.ShowDialog()
    End Sub

    Private Sub btn_BBHU_Click(sender As Object, e As EventArgs) Handles btn_BBHU.Click
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        frm_BBHU.KodeSupplier = DataTabelUtama.Item("Kode_Supplier", BarisTerseleksi).Value
        frm_BBHU.lbl_NamaSupplier.Text = DataTabelUtama.Item("Nama_Supplier", BarisTerseleksi).Value
        frm_BBHU.JalurMasuk = Halaman_DPHU
        frm_BBHU.ShowDialog()
    End Sub

    Private Sub cmb_Supplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Supplier.SelectedIndexChanged
        Dim SupplierTerseleksi = cmb_Supplier.Text
        FilterSupplier = Replace(Microsoft.VisualBasic.Left(SupplierTerseleksi, 9), " ", "")
        If ProsesLoading = False Then TampilkanData()
    End Sub

End Class