Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_DataCOA_X

    Dim QueryTampilan
    Dim FilterData
    Public Baris_Terseleksi
    Dim COATerseleksi

    Dim JumlahAktiva
    Dim JumlahPassiva
    Dim SelisihNeraca
    Public KeseimbanganNeraca As Boolean


    Private Sub frm_DataCOA_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        If SistemCOA = SistemCOA_StandarAplikasi Then
            btn_TautanCOA.Visible = False
        Else
            btn_TautanCOA.Visible = True
        End If
        lbl_Judul.Text = "DATA COA - Tahun " & TahunBukuAktif
        RefreshTampilanData()

        If LevelUserAktif >= LevelUser_99_AppDeveloper Then
            btn_ImportCOA.Visible = True
            btn_TautanCOA.Visible = True
            btn_Hapus.Enabled = True
        Else
            btn_ImportCOA.Visible = False
            btn_TautanCOA.Visible = False
            btn_Hapus.Enabled = False
        End If

        ProsesLoadingForm = False

        Style_HalamanModul(Me)

    End Sub

    Sub RefreshTampilanData()
        txt_CariAkun.Text = Kosongan
        txt_KepalaCOA.Text = Kosongan
        TampilkanData()
        KontenComboVisibilitas()
    End Sub

    Sub TampilkanData()

        'Filter Pencarian :
        Dim FilterPencarian = " "
        If txt_CariAkun.Text = Nothing Then
            FilterPencarian = " "
        Else
            Dim Srch = txt_CariAkun.Text
            Dim clm_COA = " COA LIKE '%" & Srch & "%' "
            Dim clm_NamaAkun = " OR Nama_Akun LIKE '%" & Srch & "%' "
            FilterPencarian = " AND ( " & clm_COA & clm_NamaAkun & " ) "
        End If

        'Filter Visibilitas :
        Dim FilterVisibilitas
        FilterVisibilitas = " "
        If cmb_Visibilitas.Text = "SEMUA" Then FilterVisibilitas = " "
        If cmb_Visibilitas.Text = "YA" Then FilterVisibilitas = " AND Visibilitas = '" & Pilihan_Ya & "' "
        If cmb_Visibilitas.Text = "TIDAK" Then FilterVisibilitas = " AND Visibilitas <> '" & Pilihan_Ya & "' "

        'Filter Kepala COA :
        Dim FilterKepalaCOA
        If txt_KepalaCOA.Text = Kosongan Then
            FilterKepalaCOA = " "
        Else
            FilterKepalaCOA = " AND COA LIKE '" & txt_KepalaCOA.Text & "%' "
        End If



        'Query Tampilan :
        FilterData = FilterPencarian & FilterVisibilitas & FilterKepalaCOA
        QueryTampilan = " SELECT * FROM tbl_COA WHERE D_K <> 'X' " & FilterData

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel :
        Dim COA
        Dim NamaAkun
        Dim DK
        Dim Saldo
        Dim Uraian
        Dim Visibilitas
        JumlahAktiva = 0
        JumlahPassiva = 0
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            COA = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            DK = dr.Item("D_K")
            Saldo = dr.Item("Saldo_Awal")
            If Saldo = 0 Then
                Saldo = StripKosong
            End If
            Uraian = dr.Item("Uraian")
            Visibilitas = dr.Item("Visibilitas")
            If Microsoft.VisualBasic.Left(COA.ToString, 1) = "1" Then JumlahAktiva += AmbilAngka(Saldo)
            If Microsoft.VisualBasic.Left(COA.ToString, 1) = "2" Or Microsoft.VisualBasic.Left(COA.ToString, 1) = "3" Then JumlahPassiva += AmbilAngka(Saldo)
            DataTabelUtama.Rows.Add(COA, NamaAkun, DK, Saldo, Uraian, Visibilitas)
        Loop

        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If row.Cells("Visibilitas_").Value <> "YA" Then
                row.DefaultCellStyle.ForeColor = Color.Gray
            End If
        Next

        AksesDatabase_General(Tutup)
        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
        BersihkanSeleksi()

        If txt_CariAkun.Text <> Nothing Then
            grb_Balancer.Visible = False
        Else
            grb_Balancer.Visible = True
            CekKeseimbanganNeraca()
        End If

    End Sub

    Sub BersihkanSeleksi()
        DataTabelUtama.ClearSelection()
        Baris_Terseleksi = -1
    End Sub

    Sub KontenComboVisibilitas()
        cmb_Visibilitas.Items.Clear()
        cmb_Visibilitas.Items.Add("YA")
        cmb_Visibilitas.Items.Add("TIDAK")
        cmb_Visibilitas.Items.Add("SEMUA")
        cmb_Visibilitas.Text = "YA"
    End Sub

    Sub CekKeseimbanganNeraca()

        SelisihNeraca = JumlahAktiva - JumlahPassiva

        txt_JumlahAktiva.Text = JumlahAktiva
        txt_JumlahPassiva.Text = JumlahPassiva
        txt_SelisihNeraca.Text = SelisihNeraca

        If SelisihNeraca = 0 Then
            KeseimbanganNeraca = True
            txt_JumlahAktiva.ForeColor = WarnaTegas
            txt_JumlahPassiva.ForeColor = WarnaTegas
            txt_SelisihNeraca.ForeColor = WarnaTegas
        Else
            KeseimbanganNeraca = False
            txt_JumlahAktiva.ForeColor = WarnaPeringatan
            txt_JumlahPassiva.ForeColor = WarnaPeringatan
            txt_SelisihNeraca.ForeColor = WarnaPeringatan
        End If

        NotifikasiKeseimbanganNeraca()

    End Sub

    Sub NotifikasiKeseimbanganNeraca()

        Dim NotifikasiNeracaTidakSeimbang = "Neraca pada Saldo Akhir COA tidak seimbang!"


        If KeseimbanganNeraca = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" DELETE FROM tbl_Notifikasi WHERE Notifikasi = '" & NotifikasiNeracaTidakSeimbang & "' ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
        End If

        If KeseimbanganNeraca = False Then
            Dim NotifikasiSudahAda As Boolean
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_Notifikasi WHERE Notifikasi = '" & NotifikasiNeracaTidakSeimbang & "' ", KoneksiDatabaseTransaksi)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                NotifikasiSudahAda = True
            Else
                NotifikasiSudahAda = False
            End If
            AksesDatabase_Transaksi(Tutup)
            If NotifikasiSudahAda = False Then
                notif_NomorID = AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_Notifikasi") + 1
                notif_Jenis = JenisNotifikasi_PerintahEksekusi
                notif_Waktu = Today
                notif_Notifikasi = NotifikasiNeracaTidakSeimbang
                notif_HalamanTarget = Halaman_DATACOA
                notif_Pesan = NotifikasiNeracaTidakSeimbang & Enter2Baris & "Silakan seimbangkan Neraca yang ada di Data COA!"
                notif_StatusDibaca = 0
                notif_StatusDieksekusi = 0
                SimpanNotifikasi()
            End If
        End If

        frm_BOOKU.IsiKontenNotifikasi()

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub txt_CariAkun_TextChanged(sender As Object, e As EventArgs) Handles txt_CariAkun.TextChanged
        TampilkanData()
    End Sub

    Private Sub txt_KepalaCOA_TextChanged(sender As Object, e As EventArgs) Handles txt_KepalaCOA.TextChanged
        txt_CariAkun.Text = Kosongan
        TampilkanData()
    End Sub
    Private Sub txt_KepalaCOA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KepalaCOA.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        COATerseleksi = DataTabelUtama.Item("COA_", Baris_Terseleksi).Value
    End Sub

    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_Edit_Click(sender, e)
    End Sub

    Private Sub cmb_Visibilitas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Visibilitas.SelectedIndexChanged
        TampilkanData()
    End Sub

    Private Sub btn_TautanCOA_Click(sender As Object, e As EventArgs) Handles btn_TautanCOA.Click
        frm_TautanCOA.ShowDialog()
    End Sub

    Private Sub txt_JumlahAktiva_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahAktiva.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahAktiva)
    End Sub
    Private Sub txt_JumlahAktiva_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahAktiva.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahPassiva_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPassiva.TextChanged
        PemecahRibuanUntukTextBox(txt_JumlahPassiva)
    End Sub
    Private Sub txt_JumlahPassiva_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahPassiva.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SelisihNeraca_TextChanged(sender As Object, e As EventArgs) Handles txt_SelisihNeraca.TextChanged
        PemecahRibuanUntukTextBox(txt_SelisihNeraca)
    End Sub
    Private Sub txt_SelisihNeraca_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SelisihNeraca.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_ImportCOA_Click(sender As Object, e As EventArgs) Handles btn_ImportCOA.Click
        PesanPemberitahuan("Fitur ini masih dalam perbaikan.")
        Return
        frm_ProgressImportDataCOA.ShowDialog()
        If StatusPosting = "BATAL" Then
            MsgBox("Proses posting telah dibatalkan seluruhnya pada event ini.")
        End If
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        frm_InputCOA.ResetForm()
        frm_InputCOA.FungsiForm = FungsiForm_TAMBAH
        frm_InputCOA.ShowDialog()
        If frm_InputCOA.ProsesSuntingDatabase = True Then TampilkanData()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        If Baris_Terseleksi = -1 Then
            MsgBox("Tidak ada data terseleksi")
            Return
        End If
        frm_InputCOA.ResetForm()
        frm_InputCOA.FungsiForm = FungsiForm_EDIT
        frm_InputCOA.JalurMasuk = Halaman_DATACOA
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        frm_InputCOA.txt_COA.Text = COATerseleksi
        frm_InputCOA.txt_NamaAkun.Text = dr.Item("Nama_Akun")
        frm_InputCOA.cmb_DebetKredit.Text = dr.Item("D_K")
        Dim InputSaldoAwal = dr.Item("Saldo_Awal")
        If InputSaldoAwal = 0 Then
            frm_InputCOA.txt_SaldoAwal.Text = ""
        Else
            frm_InputCOA.txt_SaldoAwal.Text = InputSaldoAwal
        End If
        frm_InputCOA.txt_Uraian.Text = dr.Item("Uraian")
        frm_InputCOA.cmb_Visibilitas.Text = dr.Item("Visibilitas")
        AksesDatabase_General(Tutup)
        frm_InputCOA.ShowDialog()
        If frm_InputCOA.ProsesSuntingDatabase = True Then TampilkanData()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            PesanPemberitahuan("Menu ini belum bisa digunakan..!")
            Return
        End If

        If Baris_Terseleksi < 0 Then
            PesanPeringatan("Tidak ada baris yang terseleksi..!")
            Return
        End If

        Pilihan = MessageBox.Show("Yakin akan menghapus data terpilih..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_General(Buka)
        cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_COA WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseGeneral)
        cmdHAPUS_ExecuteNonQuery()
        cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            TahunBuku_Alternatif = dr.Item("Tahun_Buku")
            BukaDatabaseTransaksi_Alternatif(TahunBuku_Alternatif)
            cmdHAPUS = New OdbcCommand(" DELETE FROM tbl_SaldoAwalCOA WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseTransaksi_Alternatif)
            cmdHAPUS_ExecuteNonQuery()
            TutupDatabaseTransaksi_Alternatif()
        Loop
        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase = True Then
            TampilkanData()
            'pesan_DataTerpilihBerhasilDihapus()
        End If

    End Sub

End Class