Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanBuktiPengeluaranBankCash_X

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim AngkaKK_Sebelumnya
    Dim AngkaKK
    Dim NomorKK
    Dim TanggalKK
    Dim PeruntukanPembayaran
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim JumlahInvoicePerBaris
    Dim SaranaPembayaran
    Dim RekeningPenerima
    Dim AtasNamaPenerima
    Dim PenerimaPembayaran
    Dim NomorBP
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim JumlahTagihan
    Dim BiayaAdministrasiBank
    Dim Denda
    Dim JumlahPengajuan
    Dim JumlahBayar
    Dim TanggalBayar
    Dim KodeAkun
    Dim NamaAkun
    Dim StatusPengajuan
    Dim Uraian
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim AngkaKK_Terseleksi
    Dim NomorKK_Terseleksi
    Dim TanggalKK_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim JumlahInvoicePerBaris_Terseleksi
    Dim SaranaPembayaran_Terseleksi
    Dim COAKredit_Terseleksi
    Dim PenerimaPembayaran_Terseleksi
    Dim NomorBP_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim JumlahTagihan_Terseleksi
    Dim BiayaAdministrasiBank_Terseleksi
    Dim JumlahBayar_Terseleksi
    Dim TanggalBayar_Terseleksi
    Dim KodeAkun_Terseleksi
    Dim NamaAkun_Terseleksi
    Dim StatusPengajuan_Terseleksi
    Dim Uraian_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi


    Private Sub Tabel_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub


    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        Dim i = 0
        NomorUrut = 0
        AngkaKK_Sebelumnya = 0

        JumlahTagihan = 0
        JumlahPengajuan = 0
        JumlahBayar = 0
        JumlahInvoicePerBaris = 0

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                              " ORDER BY Angka_KK ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            AngkaKK = dr.Item("Angka_KK")
            If i > 0 And AngkaKK <> AngkaKK_Sebelumnya Then TambahBaris()
            NomorKK = dr.Item("Nomor_KK")
            TanggalKK = TanggalFormatTampilan(dr.Item("Tanggal_KK"))
            PeruntukanPembayaran = dr.Item("Peruntukan")
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            SaranaPembayaran = KonversiCOAKeSaranaPembayaran(dr.Item("COA_Kredit"))
            RekeningPenerima = dr.Item("Rekening_Penerima")
            AtasNamaPenerima = dr.Item("Atas_Nama_Penerima")
            If RekeningPenerima <> Kosongan Then
                If AtasNamaPenerima <> Kosongan Then
                    PenerimaPembayaran = RekeningPenerima & " a.n. " & AtasNamaPenerima
                Else
                    PenerimaPembayaran = RekeningPenerima
                End If
            Else
                PenerimaPembayaran = StripKosong
            End If
            NomorBP = dr.Item("Nomor_BP")
            If PeruntukanPembayaran = Peruntukan_PembayaranHutangBank _
                Or PeruntukanPembayaran = Peruntukan_PembayaranHutangLeasing _
                Or PeruntukanPembayaran = Peruntukan_PembayaranHutangAfiliasi _
                Or PeruntukanPembayaran = Peruntukan_PembayaranHutangPihakKetiga _
                Then
                NomorInvoice = dr.Item("Nomor_Invoice")
                TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
            Else
                If NomorInvoice = Kosongan Then
                    NomorInvoice = dr.Item("Nomor_Invoice")
                Else
                    NomorInvoice &= SlashGanda_Pemisah & Chr(13) & dr.Item("Nomor_Invoice")
                End If
                If TanggalInvoice = Kosongan Then
                    TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    If TanggalInvoice = TanggalKosong Then TanggalInvoice = Kosongan
                Else
                    TanggalInvoice &= SlashGanda_Pemisah & Chr(13) & TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                End If
            End If
            JumlahTagihan += dr.Item("Jumlah_Tagihan")
            JumlahPengajuan += dr.Item("Jumlah_Pengajuan")
            JumlahBayar += dr.Item("Jumlah_Bayar")
            TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            BiayaAdministrasiBank = dr.Item("Biaya_Administrasi_Bank")
            Denda = dr.Item("Denda")
            KodeAkun = dr.Item("COA_Debet")
            NamaAkun = AmbilValue_NamaAkun(KodeAkun)
            StatusPengajuan = dr.Item("Status_Pengajuan")
            Uraian = dr.Item("Catatan")
            NomorJV = dr.Item("Nomor_JV")
            User = dr.Item("User")
            AngkaKK_Sebelumnya = AngkaKK
            i += 1
            JumlahInvoicePerBaris += 1
        Loop

        If i > 0 Then TambahBaris()

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        NomorUrut += 1
        JumlahPengajuan += Denda
        If AmbilAngka(JumlahBayar) = 0 Then
            JumlahBayar = StripKosong
        Else
            JumlahBayar += Denda
        End If
        If AmbilAngka(BiayaAdministrasiBank) = 0 Then BiayaAdministrasiBank = StripKosong
        If Not StatusPengajuan = Status_Dibayar Then
            JumlahBayar = StripKosong
            TanggalBayar = StripKosong
        End If
        DataTabelUtama.Rows.Add(NomorUrut, AngkaKK_Sebelumnya, NomorKK, TanggalKK, KodeLawanTransaksi, NamaLawanTransaksi, JumlahInvoicePerBaris, SaranaPembayaran, PenerimaPembayaran,
                                NomorBP, NomorInvoice, TanggalInvoice, JumlahTagihan, BiayaAdministrasiBank, JumlahBayar, TanggalBayar,
                                KodeAkun, NamaAkun, StatusPengajuan, Uraian, NomorJV, User)
        Dim i = NomorUrut - 1
        Select Case StatusPengajuan
            Case Status_Dibayar
                DataTabelUtama.Rows(i).DefaultCellStyle.ForeColor = WarnaTegas
            Case Status_Ditolak
                DataTabelUtama.Rows(i).DefaultCellStyle.ForeColor = WarnaMerahSolid
            Case Else
                DataTabelUtama.Rows(i).DefaultCellStyle.ForeColor = WarnaPudar
        End Select
        NomorInvoice = Kosongan
        TanggalInvoice = Kosongan
        JumlahTagihan = 0
        JumlahPengajuan = 0
        JumlahBayar = 0
        JumlahInvoicePerBaris = 0
    End Sub


    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        grb_TindakLanjut.Enabled = False
        btn_LihatJurnal.Enabled = False
        btn_LihatBundelan.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Bundelan_Click(sender As Object, e As EventArgs) Handles btn_Bundelan.Click
        frm_BundelPengajuanPengeluaranBankCash.MdiParent = frm_BOOKU
        frm_BundelPengajuanPengeluaranBankCash.Show()
        frm_BundelPengajuanPengeluaranBankCash.Focus()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub

    Private Sub btn_LihatBundelan_Click(sender As Object, e As EventArgs) Handles btn_LihatBundelan.Click

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_LIHAT
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.AngkaKK = AngkaKK_Terseleksi
        win_InputBuktiPengeluaran.ShowDialog()

    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_TAMBAH
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_EDIT
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.AngkaKK = AngkaKK_Terseleksi
        win_InputBuktiPengeluaran.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_BuktiPengeluaran " &
                                   " WHERE Angka_KK = '" & AngkaKK_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmdHAPUS_ExecuteNonQuery()
        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub


    Private Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        If StatusPengajuan_Terseleksi = Status_Open Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_BuktiPengeluaran SET " &
                                  " Status_Invoice      = '" & Status_Dicetak & "', " &
                                  " Status_Pengajuan    = '" & Status_Dicetak & "' " &
                                  " WHERE Nomor_KK    = '" & NomorKK_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            cmd_ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
            If StatusSuntingDatabase = True Then TampilkanData()

        End If
        ProsesCetak()
    End Sub
    Sub ProsesCetak()
        PesanPemberitahuan("Ceritanya data sudah dicetak ya....!")
    End Sub


    Private Sub btn_Posting_Click(sender As Object, e As EventArgs) Handles btn_Posting.Click

        If Not (StatusPengajuan_Terseleksi = Status_Disetujui Or StatusPengajuan_Terseleksi = Status_Dibayar) Then
            PesanPeringatan("Data tidak dapat diposting ke Jurnal karena belum disetujui." & Enter2Baris &
                            "Silakan setujui terlebih dahulu..!")
            Return
        End If

        If COAKredit_Terseleksi = KodeTautanCOA_CashAdvance And StatusPengajuan_Terseleksi <> Status_Dibayar Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT Jumlah_Bayar FROM tbl_BuktiPengeluaran " &
                                  " WHERE Angka_KK = '" & AngkaKK_Terseleksi & "' ", KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            dr.Read()
            Dim JumlahDisetujui = dr.Item("Jumlah_Bayar")
            AksesDatabase_Transaksi(Tutup)
            If JumlahDisetujui > SaldoAkhirCOA(KodeTautanCOA_CashAdvance) Then
                PesanPeringatan("....!!!")
                Return
            End If
        End If

        Dim win_InputBuktiPengeluaran As New wpfWin_InputBuktiPengeluaran
        win_InputBuktiPengeluaran.ResetForm()
        win_InputBuktiPengeluaran.FungsiForm = FungsiForm_POSTING
        win_InputBuktiPengeluaran.JalurMasuk = JalurUtama
        win_InputBuktiPengeluaran.AngkaKK = AngkaKK_Terseleksi
        win_InputBuktiPengeluaran.ShowDialog()

    End Sub


    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index

        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", BarisTerseleksi).Value)
        AngkaKK_Terseleksi = AmbilAngka(DataTabelUtama.Item("Angka_KK", BarisTerseleksi).Value)
        NomorKK_Terseleksi = DataTabelUtama.Item("Nomor_KK", BarisTerseleksi).Value
        TanggalKK_Terseleksi = DataTabelUtama.Item("Tanggal_KK", BarisTerseleksi).Value
        KodeLawanTransaksi_Terseleksi = DataTabelUtama.Item("Kode_Lawan_Transaksi", BarisTerseleksi).Value
        NamaLawanTransaksi_Terseleksi = DataTabelUtama.Item("Nama_Lawan_Transaksi", BarisTerseleksi).Value
        JumlahInvoicePerBaris_Terseleksi = DataTabelUtama.Item("Jumlah_Invoice", BarisTerseleksi).Value
        SaranaPembayaran_Terseleksi = DataTabelUtama.Item("Sarana_Pembayaran", BarisTerseleksi).Value
        COAKredit_Terseleksi = KonversiSaranaPembayaranKeCOA(SaranaPembayaran_Terseleksi)
        PenerimaPembayaran_Terseleksi = DataTabelUtama.Item("Penerima_Pembayaran", BarisTerseleksi).Value
        NomorBP_Terseleksi = DataTabelUtama.Item("Nomor_BP", BarisTerseleksi).Value
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", BarisTerseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", BarisTerseleksi).Value
        JumlahTagihan_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Tagihan", BarisTerseleksi).Value)
        BiayaAdministrasiBank_Terseleksi = AmbilAngka(DataTabelUtama.Item("Biaya_Administrasi_Bank", BarisTerseleksi).Value)
        JumlahBayar_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_Bayar", BarisTerseleksi).Value)
        TanggalBayar_Terseleksi = DataTabelUtama.Item("Tanggal_Bayar", BarisTerseleksi).Value
        KodeAkun_Terseleksi = DataTabelUtama.Item("COA_Kredit", BarisTerseleksi).Value
        NamaAkun_Terseleksi = DataTabelUtama.Item("Nama_Akun", BarisTerseleksi).Value
        StatusPengajuan_Terseleksi = DataTabelUtama.Item("Status_", BarisTerseleksi).Value
        Uraian_Terseleksi = DataTabelUtama.Item("Uraian_", BarisTerseleksi).Value
        NomorJV_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", BarisTerseleksi).Value)
        User_Terseleksi = DataTabelUtama.Item("User_", BarisTerseleksi).Value

        If AngkaKK_Terseleksi > 0 Then
            If StatusPengajuan_Terseleksi = Status_Open Or StatusPengajuan_Terseleksi = Status_Dicetak Then
                btn_Edit.Enabled = True
                btn_Hapus.Enabled = True
                btn_Cetak.Enabled = True
            Else
                btn_Edit.Enabled = False
                btn_Hapus.Enabled = False
                btn_Cetak.Enabled = False
            End If
            grb_TindakLanjut.Enabled = True
            btn_LihatBundelan.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
            grb_TindakLanjut.Enabled = False
            btn_LihatBundelan.Enabled = False
        End If

        If NomorJV_Terseleksi > 0 Then
            btn_LihatJurnal.Enabled = True
        Else
            btn_LihatJurnal.Enabled = False
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class