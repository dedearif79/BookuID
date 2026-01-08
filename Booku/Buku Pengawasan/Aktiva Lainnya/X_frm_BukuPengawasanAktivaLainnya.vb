Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanAktivaLainnya

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorID
    Dim NomorBPAL
    Dim NomorBukti
    Dim TanggalBukti
    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UraianTransaksi
    Dim COADebet
    Dim COAKredit
    Dim NamaAkun
    Dim JumlahTransaksi
    Dim TanggalPencairan
    Dim Keterangan
    Dim NomorJV
    Dim User

    Dim NomorUrut_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorBPAL_Terseleksi
    Dim NomorBukti_Terseleksi
    Dim TanggalBukti_Terseleksi
    Dim KodeLawanTransaksi_Terseleksi
    Dim NamaLawanTransaksi_Terseleksi
    Dim UraianTransaksi_Terseleksi
    Dim COADebet_Terseleksi
    Dim COAKredit_Terseleksi
    Dim NamaAkun_Terseleksi
    Dim JumlahTransaksi_Terseleksi
    Dim Keterangan_Terseleksi
    Dim TanggalPencairan_Terseleksi
    Dim NomorJV_Terseleksi
    Dim User_Terseleksi

    Dim TotalAktivaLainnya

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
        NomorUrut = 0
        TotalAktivaLainnya = 0

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_AktivaLainnya ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        Do While dr.Read
            NomorUrut += 1
            NomorID = dr.Item("Nomor_ID")
            NomorBPAL = dr.Item("Nomor_BPAL")
            NomorBukti = dr.Item("Nomor_Bukti")
            TanggalBukti = TanggalFormatTampilan(dr.Item("Tanggal_Bukti"))
            KodeLawanTransaksi = dr.Item("Kode_Lawan_Transaksi")
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            UraianTransaksi = dr.Item("Uraian_Transaksi")
            COADebet = dr.Item("COA_Debet")
            COAKredit = dr.Item("COA_Kredit")
            NamaAkun = AmbilValue_NamaAkun(COADebet)
            JumlahTransaksi = dr.Item("Jumlah_Transaksi")
            TanggalPencairan = TanggalFormatTampilan(dr.Item("Tanggal_Pencairan"))
            Keterangan = dr.Item("Keterangan")
            NomorJV = dr.Item("Nomor_JV")
            User = dr.Item("User")
            If TanggalPencairan = TanggalKosong Then TanggalPencairan = StripKosong
            DataTabelUtama.Rows.Add(NomorUrut, NomorID, NomorBPAL, NomorBukti, TanggalBukti,
                                    KodeLawanTransaksi, NamaLawanTransaksi, UraianTransaksi,
                                    COADebet, COAKredit, NamaAkun, JumlahTransaksi, TanggalPencairan, Keterangan, NomorJV, User)
            If NomorJV = 0 Then
                DataTabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaPudar
            Else
                TotalAktivaLainnya += JumlahTransaksi
                DataTabelUtama.Rows(NomorUrut - 1).DefaultCellStyle.ForeColor = WarnaTegas
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub



    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click

        X_frm_InputAktivaLainnya.ResetForm()
        X_frm_InputAktivaLainnya.FungsiForm = FungsiForm_TAMBAH
        X_frm_InputAktivaLainnya.ShowDialog()

    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        X_frm_InputAktivaLainnya.ResetForm()
        X_frm_InputAktivaLainnya.FungsiForm = FungsiForm_EDIT
        X_frm_InputAktivaLainnya.NomorID = NomorID_Terseleksi
        X_frm_InputAktivaLainnya.txt_NomorBPAL.Text = NomorBPAL_Terseleksi
        X_frm_InputAktivaLainnya.txt_NomorBukti.Text = NomorBukti_Terseleksi
        X_frm_InputAktivaLainnya.txt_KodeLawanTransaksi.Text = KodeLawanTransaksi_Terseleksi
        X_frm_InputAktivaLainnya.txt_UraianTransaksi.Text = UraianTransaksi_Terseleksi
        X_frm_InputAktivaLainnya.txt_COADebet.Text = COADebet_Terseleksi
        X_frm_InputAktivaLainnya.txt_JumlahTransaksi.Text = JumlahTransaksi_Terseleksi
        X_frm_InputAktivaLainnya.txt_Keterangan.Text = Keterangan_Terseleksi
        X_frm_InputAktivaLainnya.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)

        cmd = New OdbcCommand(" DELETE FROM tbl_AktivaLainnya " &
                              " WHERE Nomor_BPAL = '" & NomorBPAL_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)

        If StatusSuntingDatabase = True Then
            pesan_DataTerpilihBerhasilDihapus()
            TampilkanData()
        Else
            pesan_DataTerpilihGagalDihapus()
        End If

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_Urut", BarisTerseleksi).Value)
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_ID", BarisTerseleksi).Value)
        NomorBPAL_Terseleksi = DataTabelUtama("Nomor_BPAL", BarisTerseleksi).Value
        NomorBukti_Terseleksi = DataTabelUtama("Nomor_Bukti", BarisTerseleksi).Value
        TanggalBukti_Terseleksi = DataTabelUtama("Tanggal_Bukti", BarisTerseleksi).Value
        KodeLawanTransaksi_Terseleksi = DataTabelUtama("Kode_Lawan_Transaksi", BarisTerseleksi).Value
        NamaLawanTransaksi_Terseleksi = DataTabelUtama("Nama_Lawan_Transaksi", BarisTerseleksi).Value
        UraianTransaksi_Terseleksi = DataTabelUtama("Uraian_Transaksi", BarisTerseleksi).Value
        COADebet_Terseleksi = DataTabelUtama("COA_Debet", BarisTerseleksi).Value
        COAKredit_Terseleksi = DataTabelUtama("COA_Kredit", BarisTerseleksi).Value
        NamaAkun_Terseleksi = DataTabelUtama("Nama_Akun", BarisTerseleksi).Value
        JumlahTransaksi_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Transaksi", BarisTerseleksi).Value)
        TanggalPencairan_Terseleksi = DataTabelUtama("Tanggal_Pencairan", BarisTerseleksi).Value
        Keterangan_Terseleksi = DataTabelUtama("Keterangan_", BarisTerseleksi).Value
        NomorJV_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_JV", BarisTerseleksi).Value)
        User_Terseleksi = DataTabelUtama("User_", BarisTerseleksi).Value

        If NomorID_Terseleksi > 0 Then
            If NomorJV_Terseleksi > 0 Then
                btn_LihatJurnal.Enabled = True
                btn_Edit.Enabled = False
                btn_Hapus.Enabled = False
            Else
                btn_LihatJurnal.Enabled = False
                btn_Edit.Enabled = True
                btn_Hapus.Enabled = True
            End If
        Else
            BersihkanSeleksi()
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class