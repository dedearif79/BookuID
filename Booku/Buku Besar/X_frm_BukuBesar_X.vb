Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuBesar_X

    Public FungsiModul
    Dim QueryTampilan As String
    Dim QueryTampilan2 As String
    Dim BarisTerseleksi As Integer
    Dim JumlahBaris As Int64
    Dim NomorPembelianTerseleksi As String
    Dim NomorBPHUTerseleksi As String
    Public COA_BukuBesar As String
    Public NamaAkun_BukuBesar As String
    Dim DebetKreditCOA
    Dim NomorJV_Terseleksi
    Dim AngkaNomorJV_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim FilterDirect

    Private Sub frm_BukuBesar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txt_SaldoAwal.Text = Nothing

        If FungsiModul = Halaman_BUKUBANK _
            Or FungsiModul = Halaman_BUKUCASHADVANCE _
            Or FungsiModul = Halaman_BUKUPETTYCASH _
            Then
            btn_InputTransaksi.Visible = True
            lbl_Direct.Visible = True
            cmb_Direct.Visible = True
            btn_Edit.Visible = True
            btn_Hapus.Visible = True
            DataTabelUtama.Columns("Direct_").Visible = True
            If FungsiModul = Halaman_BUKUBANK Then
                btn_InputTransaksi.Enabled = False
            Else
                btn_InputTransaksi.Enabled = True
            End If
        Else
            btn_InputTransaksi.Visible = False
            lbl_Direct.Visible = False
            cmb_Direct.Visible = False
            btn_Edit.Visible = False
            btn_Hapus.Visible = False
            DataTabelUtama.Columns("Direct_").Visible = False
        End If

        KontenComboDirect()

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        COA_BukuBesar = txt_COA.Text 'Jangan Dihapus...! Dan Jangan dipindah ke Sub RefreshTampilanData()..!!! Sudah benar di sini..!!!

        'Mengisi Value Saldo Awal
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand("SELECT * FROM tbl_COA WHERE COA = '" & COA_BukuBesar & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then txt_SaldoAwal.Text = dr.Item("Saldo_Awal")
        AksesDatabase_General(Tutup)

        'Filter Direct:
        Select Case cmb_Direct.Text
            Case "SEMUA"
                FilterDirect = " "
            Case "YA"
                FilterDirect = " AND Direct = 1 "
            Case "TIDAK"
                FilterDirect = " AND Direct = 0 "
        End Select

        'Query Tampilan :
        QueryTampilan = " SELECT * FROM tbl_Transaksi WHERE COA = '" & COA_BukuBesar & "' AND Status_Approve = 1 " & FilterDirect

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        Dim clm_NoJV, clm_TgTr, clm_BgrJs, clm_TglInv, clm_NoInv, clm_NFP, clm_KLTr, clm_NLTr, clm_UrTr, clm_AkLwn, clm_DK, clm_Deb, clm_Krd, clm_Sld, clm_Direct
        Dim SaldoAwal As Int64
        SaldoAwal = AmbilAngka(txt_SaldoAwal.Text)
        Dim SaldoAkhir As Int64 = 0
        Dim Debet As Int64
        Dim Kredit As Int64
        Dim Saldo As Int64 = SaldoAwal
        Dim JumlahDebet As Int64 = 0
        Dim JumlahKredit As Int64 = 0
        clm_NoJV = Nothing
        clm_TgTr = Nothing
        clm_BgrJs = Nothing
        clm_NFP = Nothing
        clm_TglInv = Nothing
        clm_NoInv = Nothing
        clm_KLTr = Nothing
        clm_NLTr = Nothing
        clm_UrTr = Nothing
        clm_AkLwn = Nothing
        clm_DK = Nothing
        clm_Deb = Nothing
        clm_Krd = Nothing
        clm_Sld = Nothing
        clm_Direct = Nothing

        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        Dim cmdCOA As OdbcCommand
        Dim drCOA As OdbcDataReader
        cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA_BukuBesar & "' ", KoneksiDatabaseGeneral)
        drCOA = cmdCOA.ExecuteReader
        drCOA.Read()
        If drCOA.HasRows Then DebetKreditCOA = drCOA.Item("D_K")
        AksesDatabase_General(Tutup)

        Dim AngkaNomorJV
        Dim cmdAKUNLAWAN As OdbcCommand
        Dim drAKUNLAWAN As OdbcDataReader
        Dim KodeAkunLawan = Nothing
        Dim NamaAkunLawan = Nothing
        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Nomor_JV, D_K ", KoneksiDatabaseTransaksi) 'QUERY ada di sub masing-masing Tipe Tampilan (Semua/Pencarian/Filter)
        dr = cmd.ExecuteReader
        Do While dr.Read
            Try
                AngkaNomorJV = dr.Item("Nomor_JV")
                clm_NoJV = AwalanNomorJV & AngkaNomorJV
                clm_TgTr = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Transaksi"), 10)
                clm_BgrJs = dr.Item("Nama_Produk")
                clm_TglInv = dr.Item("Tanggal_Invoice")
                clm_NoInv = dr.Item("Nomor_Invoice")
                clm_NFP = dr.Item("Nomor_Faktur_Pajak")
                clm_KLTr = dr.Item("Kode_Lawan_Transaksi")
                clm_NLTr = dr.Item("Nama_Lawan_Transaksi")
                clm_UrTr = dr.Item("Uraian_Transaksi")
                clm_DK = dr.Item("D_K")
                If clm_DK = "D" Then
                    cmdAKUNLAWAN = New OdbcCommand("SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV & "' AND D_K = 'K' ", KoneksiDatabaseTransaksi)
                Else
                    cmdAKUNLAWAN = New OdbcCommand("SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV & "' AND D_K = 'D' ", KoneksiDatabaseTransaksi)
                End If
                drAKUNLAWAN = cmdAKUNLAWAN.ExecuteReader
                clm_AkLwn = Nothing
                Do While drAKUNLAWAN.Read()
                    KodeAkunLawan = drAKUNLAWAN.Item("COA")
                    AksesDatabase_General(Buka)
                    cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeAkunLawan & "' ", KoneksiDatabaseGeneral)
                    drCOA = cmdCOA.ExecuteReader
                    drCOA.Read()
                    If drCOA.HasRows Then
                        NamaAkunLawan = drCOA.Item("Nama_Akun")
                    Else
                        NamaAkunLawan = teks_CoaBelumTerdaftar
                    End If
                    AksesDatabase_General(Tutup)
                    If clm_AkLwn = Nothing Then
                        clm_AkLwn = NamaAkunLawan
                    Else
                        clm_AkLwn = clm_AkLwn & SlashGanda_Pemisah & NamaAkunLawan
                    End If
                Loop
                Debet = dr.Item("Jumlah_Debet")
                If Debet = 0 Then
                    clm_Deb = StripKosong
                Else
                    clm_Deb = Debet
                End If
                Kredit = dr.Item("Jumlah_Kredit")
                If Kredit = 0 Then
                    clm_Krd = StripKosong
                Else
                    clm_Krd = Kredit
                End If
                If DebetKreditCOA = "DEBET" Then Saldo = Saldo + Debet - Kredit
                If DebetKreditCOA = "KREDIT" Then Saldo = Saldo - Debet + Kredit
                clm_Sld = Saldo
                If dr.Item("Direct") = 1 Then
                    clm_Direct = "Ya"
                Else
                    clm_Direct = Nothing
                End If
                If FungsiModul = Halaman_BUKUBANK Then
                    DataTabelUtama.Rows.Add(clm_NoJV, clm_TgTr, clm_BgrJs, clm_TglInv, clm_NoInv, clm_NFP, clm_KLTr, clm_NLTr, clm_AkLwn, clm_DK, clm_Krd, clm_Deb, clm_Sld, clm_UrTr, clm_Direct)
                Else
                    DataTabelUtama.Rows.Add(clm_NoJV, clm_TgTr, clm_BgrJs, clm_TglInv, clm_NoInv, clm_NFP, clm_KLTr, clm_NLTr, clm_AkLwn, clm_DK, clm_Deb, clm_Krd, clm_Sld, clm_UrTr, clm_Direct)
                End If
                JumlahDebet = JumlahDebet + Debet
                JumlahKredit = JumlahKredit + Kredit
            Catch ex As Exception
                DataTabelUtama.Rows.Clear()
                pesan_AdaMasalahDenganKoneksiDatabase()
                Return
            End Try
        Loop
        AksesDatabase_Transaksi(Tutup)
        SaldoAkhir = Saldo
        txt_SaldoAwal.Text = SaldoAwal
        If SaldoAwal = 0 Then txt_SaldoAwal.Text = StripKosong

        txt_SaldoAkhir.Text = SaldoAkhir
        If SaldoAkhir = 0 Then txt_SaldoAkhir.Text = StripKosong

        If FungsiModul = Halaman_BUKUBANK Then
            txt_JumlahDebet.Text = JumlahKredit
            txt_JumlahKredit.Text = JumlahDebet
        Else
            txt_JumlahDebet.Text = JumlahDebet
            txt_JumlahKredit.Text = JumlahKredit
        End If

        BersihkanSeleksi()

        JumlahBaris = DataTabelUtama.RowCount

        lbl_JumlahBaris.Text = "Jumlah Baris : " & JumlahBaris

    End Sub

    Sub BersihkanSeleksi()
        DataTabelUtama.ClearSelection()
        BarisTerseleksi = -1
        btn_LihatJurnal.Enabled = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
        btn_BukuPembantu.Enabled = False
        btn_LihatJurnal.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Sub KontenComboDirect()
        cmb_Direct.Items.Clear()
        cmb_Direct.Items.Add("SEMUA")
        cmb_Direct.Items.Add("YA")
        cmb_Direct.Items.Add("TIDAK")
        cmb_Direct.Text = "SEMUA"
    End Sub

    Private Sub btn_InputTransaksi_Click(sender As Object, e As EventArgs) Handles btn_InputTransaksi.Click
        win_InputTransaksi = New wpfWin_InputTransaksi
        win_InputTransaksi.ResetForm()
        win_InputTransaksi.FungsiForm = FungsiForm_TAMBAH
        Select Case FungsiModul
            Case Halaman_BUKUKAS
                win_InputTransaksi.JalurMasuk = Halaman_BUKUKAS
            Case Halaman_BUKUPETTYCASH
                win_InputTransaksi.JalurMasuk = Halaman_BUKUPETTYCASH
            Case Halaman_BUKUBANK
                win_InputTransaksi.JalurMasuk = Halaman_BUKUBANK
            Case Halaman_BUKUHUTANGGAJI
                FiturBelumBisaDigunakan()
        End Select
        win_InputTransaksi.COAUtama = COA_BukuBesar
        win_InputTransaksi.NamaAkunUtama = NamaAkun_BukuBesar
        win_InputTransaksi.SaranaPembayaran = COA_BukuBesar & StripPemisah & NamaAkun_BukuBesar
        win_InputTransaksi.ShowDialog()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(AngkaNomorJV_Terseleksi)
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

    Private Sub txt_JumlahDebet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahDebet.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahDebet_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahDebet.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahDebet)
    End Sub

    Private Sub txt_JumlahKredit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahKredit.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahKredit_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahKredit.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahKredit)
    End Sub

    Private Sub txt_COA_TextChanged(sender As Object, e As EventArgs) Handles txt_COA.TextChanged
        COA_BukuBesar = txt_COA.Text
        If COA_BukuBesar = KodeTautanCOA_HutangUsaha_NonAfiliasi _
            Or COA_BukuBesar = KodeTautanCOA_PiutangUsaha_NonAfiliasi _
            Or COA_BukuBesar = KodeTautanCOA_HutangUsaha_Afiliasi _
            Or COA_BukuBesar = KodeTautanCOA_PiutangUsaha_Afiliasi _
            Then
            btn_BukuPembantu.Visible = True
        Else
            btn_BukuPembantu.Visible = False
        End If
        If FungsiModul = Halaman_BUKUBANK Then
            If txt_COA.Text = Nothing Then
                btn_InputTransaksi.Enabled = False
            Else
                btn_InputTransaksi.Enabled = True
            End If
        End If
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA_BukuBesar & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        NamaAkun_BukuBesar = dr.Item("Nama_Akun")
        AksesDatabase_General(Tutup)
    End Sub
    Private Sub txt_COA_Click(sender As Object, e As EventArgs) Handles txt_COA.Click
        btn_PilihCOA_Click(sender, e)
    End Sub
    Private Sub txt_COA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_PilihCOA_Click(sender As Object, e As EventArgs) Handles btn_PilihCOA.Click
        If FungsiModul = Halaman_BUKUBESAR Then
            frm_ListCOA.ListAkun = ListAkun_Semua
        End If
        If FungsiModul = Halaman_BUKUBANK Then
            frm_ListCOA.ListAkun = ListAkun_Bank
        End If
        If txt_COA.Text = Nothing Then
            frm_ListCOA.ResetForm()
        End If
        frm_ListCOA.ShowDialog()
        txt_COA.Text = frm_ListCOA.COATerseleksi
        COA_BukuBesar = txt_COA.Text
        NamaAkun_BukuBesar = frm_ListCOA.NamaAkunTerseleksi
        If COA_BukuBesar = Nothing Then
            If FungsiModul = Halaman_BUKUBESAR Then lbl_BukuBesar.Text = Halaman_BUKUBESAR
            If FungsiModul = Halaman_BUKUBANK Then lbl_BukuBesar.Text = Halaman_BUKUBANK
        Else
            If FungsiModul = Halaman_BUKUBESAR Then lbl_BukuBesar.Text = "Buku Besar - " & NamaAkun_BukuBesar
            If FungsiModul = Halaman_BUKUBANK Then lbl_BukuBesar.Text = "Buku Bank - " & NamaAkun_BukuBesar
        End If
        TampilkanData()
    End Sub

    Private Sub cmb_Direct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Direct.SelectedIndexChanged
        TampilkanData()
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub btn_BukuPembantu_Click(sender As Object, e As EventArgs) Handles btn_BukuPembantu.Click
        'frm_BBHU.KodeSupplier = KodeLawanTransaksi_Terseleksi
        'frm_BBHU.lbl_NamaSupplier.Text = NamaLawanTransaksi_Terseleksi
        'frm_BBHU.JalurMasuk = Halaman_BUKUBESAR
        'frm_BBHU.ShowDialog()
        BukuBesarPembantu(KodeLawanTransaksi_Terseleksi, COA_BukuBesar)
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        Dim COA_Selain = Kosongan

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT Nomor_JV FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Dim JumlahBarisJurnal = 0
        Do While dr.Read
            JumlahBarisJurnal = JumlahBarisJurnal + 1
        Loop
        AksesDatabase_Transaksi(Tutup)

        If JumlahBarisJurnal > 2 Then
            Pilihan = MessageBox.Show("Data nomor " & NomorJV_Terseleksi & " hanya bisa diedit melalui Pengeditan Jurnal." _
                                      & Enter2Baris & "Lanjutkan mengedit..? ", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then Return
            frm_InputJurnal.ResetForm()
            frm_InputJurnal.JalurMasuk = Halaman_BUKUBESAR
            frm_InputJurnal.FungsiForm = FungsiForm_EDIT
            frm_InputJurnal.AngkaNomorJV = AngkaNomorJV_Terseleksi
            frm_InputJurnal.ShowDialog()
            Return
        End If

        win_InputTransaksi = New wpfWin_InputTransaksi
        win_InputTransaksi.ResetForm()
        win_InputTransaksi.FungsiForm = FungsiForm_EDIT

        Select Case FungsiModul
            Case Halaman_BUKUKAS
                win_InputTransaksi.JalurMasuk = Halaman_BUKUKAS
            Case Halaman_BUKUPETTYCASH
                win_InputTransaksi.JalurMasuk = Halaman_BUKUPETTYCASH
            Case Halaman_BUKUBANK
                win_InputTransaksi.JalurMasuk = Halaman_BUKUBANK
        End Select

        COA_Selain = COA_BukuBesar

        win_InputTransaksi.COAUtama = COA_Selain
        win_InputTransaksi.NamaAkunUtama = NamaAkun_BukuBesar
        win_InputTransaksi.NomorJV = AngkaNomorJV_Terseleksi
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV_Terseleksi & "' AND COA <> '" & COA_Selain & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.Item("Jumlah_Debet") > 0 Then
            win_InputTransaksi.cmb_AlurTransaksi.SelectedValue = AlurTransaksi_OUT
            win_InputTransaksi.txt_JumlahTransaksi_IDR.Text = AmbilAngka(dr.Item("Jumlah_Debet"))
        End If
        If dr.Item("Jumlah_Kredit") > 0 Then
            win_InputTransaksi.cmb_AlurTransaksi.SelectedValue = AlurTransaksi_IN
            win_InputTransaksi.txt_JumlahTransaksi_IDR.Text = AmbilAngka(dr.Item("Jumlah_Kredit"))
        End If
        win_InputTransaksi.txt_NomorBukti.Text = dr.Item("Referensi")
        win_InputTransaksi.txt_KodeLawanTransaksi.Text = dr.Item("Kode_Lawan_Transaksi")
        win_InputTransaksi.txt_NamaLawanTransaksi.Text = dr.Item("Nama_Lawan_Transaksi")
        win_InputTransaksi.txt_COALawan.Text = dr.Item("COA")
        win_InputTransaksi.txt_NamaLawanAkun.Text = dr.Item("Nama_Akun")
        win_InputTransaksi.txt_NamaProduk.Text = dr.Item("Nama_Produk")
        win_InputTransaksi.dtp_TanggalTransaksi.SelectedDate = TanggalFormatWPF(dr.Item("Tanggal_Transaksi"))
        IsiValueElemenRichTextBox(win_InputTransaksi.txt_Uraian, dr.Item("Uraian_Transaksi"))
        AksesDatabase_Transaksi(Tutup)
        win_InputTransaksi.txt_NomorBukti.Focus()
        win_InputTransaksi.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        'Yakin akan menghapus..????
        Pilihan = MessageBox.Show("Yakin akan menghapus data nomor " & NomorJV_Terseleksi & " ?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabaseTransaksi = False Then
            MsgBox("Data terpilih GAGAL dihapus." & Enter2Baris & teks_SilakanCobaLagi_Database)
            Return
        End If

        'Hapus DataTerpilih Pada Tabel Jurnal (tbl_Transaksi) :
        HapusJurnal_BerdasarkanNomorJV(AngkaNomorJV_Terseleksi)

        AksesDatabase_Transaksi(Tutup)
        TampilkanData()
        MsgBox("Data terpilih BERHASIL DIHAPUS")

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        NomorJV_Terseleksi = DataTabelUtama.Item("Nomor_JV", BarisTerseleksi).Value
        AngkaNomorJV_Terseleksi = Mid(NomorJV_Terseleksi, PanjangTeks_AwalanNomorJV_Plus1)
        KodeLawanTransaksi_Terseleksi = DataTabelUtama.Item("Kode_Lawan_Transaksi", BarisTerseleksi).Value
        NamaLawanTransaksi_Terseleksi = DataTabelUtama.Item("Nama_Lawan_Transaksi", BarisTerseleksi).Value
        If DataTabelUtama.Item("Direct_", BarisTerseleksi).Value = "Ya" Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If
        If AngkaNomorJV_Terseleksi > 0 Then
            btn_LihatJurnal.Enabled = True
        Else
            btn_LihatJurnal.Enabled = False
        End If
        If KodeLawanTransaksi_Terseleksi <> Kosongan Then
            btn_BukuPembantu.Enabled = True
        Else
            btn_BukuPembantu.Enabled = False
        End If
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class