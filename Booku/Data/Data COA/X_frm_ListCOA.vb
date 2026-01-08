Imports bcomm
Imports System.Data.Odbc

Public Class frm_ListCOA

    Dim JudulForm
    Dim QueryTampilan
    Dim BarisTerseleksi
    Dim VisibilitasTerseleksi
    Dim FilterData
    Dim FilterKategori
    Public ListAkun
    Public COATerseleksi, NamaAkunTerseleksi As String

    Public TampilkanYangTersembunyi As Boolean
    Public KodeMataUang As String

    Private Sub frm_COA_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Select Case ListAkun
            Case ListAkun_Semua
                JudulForm = "Daftar Akun"
                lbl_CariCOA.Text = "Cari Akun :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = " "
            Case ListAkun_TautanCOA
                JudulForm = "Daftar Akun"
                lbl_CariCOA.Text = "Cari Akun :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = " "
            Case ListAkun_Pembelian
                JudulForm = "Daftar Akun Pembelian"
                lbl_CariCOA.Text = "Cari Akun :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_Pembelian
            Case ListAkun_Bank
                JudulForm = "Daftar Akun Bank"
                lbl_CariCOA.Text = "Cari Akun Bank :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Bank"
                FilterKategori = FilterListCOA_Bank
            Case ListAkun_Amortisasi
                JudulForm = "Daftar Akun Amortisasi"
                lbl_CariCOA.Text = "Cari Akun Amortisasi :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_Amortisasi
            Case ListAkun_AssetTetap
                JudulForm = "Daftar Akun Asset"
                lbl_CariCOA.Text = "Cari Akun Asset :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_AssetTetap
            Case ListAkun_AssetTanah
                JudulForm = "Daftar Akun Asset"
                lbl_CariCOA.Text = "Cari Akun Asset :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_AssetTanah
            Case ListAkun_AssetTetap_SelainTanah
                JudulForm = "Daftar Akun Asset"
                lbl_CariCOA.Text = "Cari Akun Asset :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_AssetTetap_SelainTanah
            Case ListAkun_BiayaAmortisasi
                JudulForm = "Daftar Akun Biaya Amortisasi"
                lbl_CariCOA.Text = "Cari Akun Biaya :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_BiayaAmortisasi
            Case ListAkun_BiayaPenyusutan
                JudulForm = "Daftar Akun Biaya Penyusutan Asset Tetap"
                lbl_CariCOA.Text = "Cari Akun Biaya :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_BiayaPenyusutan
            Case ListAkun_AkumulasiPenyusutan
                JudulForm = "Daftar Akun Akumulasi Penyusutan"
                lbl_CariCOA.Text = "Cari Akun :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_AkumulasiPenyusutan
            Case ListAkun_PokokPajak
                JudulForm = "Daftar Akun Pokok Pajak"
                lbl_CariCOA.Text = "Cari Akun Pokok Pajak :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_PokokPajak
            Case ListAkun_DepositOperasional
                JudulForm = "Daftar Akun Deposit Operasional"
                lbl_CariCOA.Text = "Cari Akun Deposit Operasional :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_DepositOperasional
            Case ListAkun_AktivaLainnya
                JudulForm = "Daftar Akun Aktiva Lain-lain"
                lbl_CariCOA.Text = "Cari Akun Aktiva Lain-lain :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = FilterListCOA_AktivaLainnya
            Case Else
                JudulForm = "Daftar Akun"
                lbl_CariCOA.Text = "Cari Akun :"
                DataTabelUtama.Columns("COA_").HeaderText = "Kode"
                DataTabelUtama.Columns("Nama_Akun").HeaderText = "Nama Akun"
                FilterKategori = " "
        End Select

        Me.Text = JudulForm

        RefreshTampilanData()

        txt_CariAkun.Focus()

    End Sub

    Sub TampilkanData()

        'Filter Kategori ada di Sub Loading...

        'Filter Pencarian
        Dim FilterPencarian = " "
        If txt_CariAkun.Text <> "" Then
            Dim Srch = txt_CariAkun.Text
            Dim clm_COA = " COA LIKE '%" & Srch & "%' "
            Dim clm_NamaAkun = " OR Nama_Akun LIKE '%" & Srch & "%' "
            FilterPencarian = " AND (" & clm_COA & clm_NamaAkun & ") "
        End If

        'Filter Visibilitas :
        Dim FilterVisibilitas = " "

        'Filter KodeMataUang :
        Dim FilterKodeMataUang = " "
        If Not KodeMataUang = Kosongan Then
            FilterKodeMataUang = " AND Kode_Mata_Uang = '" & KodeMataUang & "' "
        End If

        'Query Tampilan :
        FilterData = FilterKategori & FilterKodeMataUang & FilterPencarian
        If ListAkun = ListAkun_TautanCOA Or TampilkanYangTersembunyi = True Then
            QueryTampilan = " SELECT * FROM tbl_COA WHERE Visibilitas <> 'XXX' " & FilterData 'Tampilan semua jenis Visibilitas
        Else
            QueryTampilan = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' " & FilterData 'Hanya tampil yang Visibilitas-nya 'YA'
        End If

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel :
        Dim IndexRow = 0
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            Dim COA = dr.Item("COA")
            Dim NamaAkun = dr.Item("Nama_Akun")
            Dim Visibilitas = dr.Item("Visibilitas")
            DataTabelUtama.Rows.Add(COA, NamaAkun, Visibilitas)
            If Visibilitas = Keterangan_YA_ Then DataTabelUtama.Rows(IndexRow).DefaultCellStyle.ForeColor = WarnaTegas
            If Visibilitas = Keterangan_TIDAK_ Then DataTabelUtama.Rows(IndexRow).DefaultCellStyle.ForeColor = WarnaPudar
            IndexRow += 1
        Loop
        AksesDatabase_General(Tutup)
        DataTabelUtama.ClearSelection()
        txt_CariAkun.Focus()

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Public Sub ResetForm()

        ProsesResetForm = True

        txt_CariAkun.Text = Kosongan
        COATerseleksi = Kosongan
        NamaAkunTerseleksi = Kosongan
        VisibilitasTerseleksi = Kosongan
        ListAkun = ListAkun_Semua
        chk_TampilkanYangTersembunyi.Checked = False
        KodeMataUang = Kosongan

        FilterListCOA_BiayaAmortisasi = Kosongan

        btn_Pilih.Enabled = False

        ProsesResetForm = False

    End Sub

    Private Sub txt_CariAkun_TextChanged(sender As Object, e As EventArgs) Handles txt_CariAkun.TextChanged
        btn_Pilih.Enabled = False
        TampilkanData()
    End Sub

    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        DataTabelUtama_CellContentClick(sender, e)
    End Sub
    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        btn_Pilih.Enabled = True
    End Sub
    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        btn_Pilih_Click(sender, e)
    End Sub
    Private Sub DataTabelUtama_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentDoubleClick
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        btn_Pilih_Click(sender, e)
    End Sub

    Private Sub chk_TampilkanYangTersembunyi_CheckedChanged(sender As Object, e As EventArgs) Handles chk_TampilkanYangTersembunyi.CheckedChanged
        If chk_TampilkanYangTersembunyi.Checked = True Then TampilkanYangTersembunyi = True
        If chk_TampilkanYangTersembunyi.Checked = False Then TampilkanYangTersembunyi = False
        btn_Pilih.Enabled = False
        TampilkanData()
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub


    Private Sub btn_Pilih_Click(sender As Object, e As EventArgs) Handles btn_Pilih.Click
        If BarisTerseleksi < 0 Then
            PesanUntukProgrammer("Tidak ada baris terseleksi.!!!")
            Return
        End If

        COATerseleksi = DataTabelUtama.Item("COA_", BarisTerseleksi).Value
        NamaAkunTerseleksi = DataTabelUtama.Item("Nama_Akun", BarisTerseleksi).Value
        VisibilitasTerseleksi = DataTabelUtama.Item("Visibilitas_", BarisTerseleksi).Value

        If VisibilitasTerseleksi = Keterangan_TIDAK_ Then

            Pilihan = MessageBox.Show("Dengan memilih Akun ini berarti Anda setuju untuk mengubah Visibilitas-nya menjadi 'Terlihat'." & Enter2Baris &
                                      "Lanjutkan..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If Pilihan = vbNo Then
                COATerseleksi = Kosongan
                NamaAkunTerseleksi = Kosongan
                VisibilitasTerseleksi = Kosongan
                Return
            End If

            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_COA SET Visibilitas = '" & Keterangan_YA_ & "' " &
                                  " WHERE COA = '" & COATerseleksi & "' ", KoneksiDatabaseGeneral)
            cmd_ExecuteNonQuery()
            AksesDatabase_General(Tutup)

        End If

        txt_CariAkun.Text = Kosongan
        Me.Close()

    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class