Imports System.Data.OleDb
Imports System.Data.Odbc

Public Class frm_DataMitra

    Dim BarisTerseleksi
    Dim KodeMitraTerseleksi

    Dim KodeMitra
    Dim NamaMitra
    Dim Supplier
    Dim Customer
    Dim PemegangSaham
    Dim Afiliasi
    Dim LembagaKeuangan
    Dim PKP
    Dim PJK
    Dim Kategori = Nothing
    Dim NPWP
    Dim JenisWP
    Dim LokasiWP
    Dim Alamat
    Dim Email
    Dim PIC
    Dim RekeningBank
    Dim AtasNama

    Private Sub frm_DataSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshTampilanData()
    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel :
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            KodeMitra = dr.Item("Kode_Mitra")
            NamaMitra = dr.Item("Nama_Mitra")
            PemegangSaham = dr.Item("Pemegang_Saham")
            Afiliasi = dr.Item("Afiliasi")
            Supplier = dr.Item("Supplier")
            Customer = dr.Item("Customer")
            LembagaKeuangan = dr.Item("Keuangan")
            PKP = dr.Item("PKP")
            PJK = dr.Item("PJK")
            If PemegangSaham = 1 Then
                PemegangSaham = True
            Else
                PemegangSaham = False
            End If
            If Afiliasi = 1 Then
                Afiliasi = True
            Else
                Afiliasi = False
            End If
            If Supplier = 1 Then
                Kategori = Mitra_Supplier
                If Customer = 1 Then
                    Kategori = Mitra_Supplier & SlashGanda_Pemisah & Mitra_Customer
                End If
            Else
                If Customer = 1 Then
                    Kategori = Mitra_Customer
                End If
            End If
            If LembagaKeuangan = 1 Then
                LembagaKeuangan = True
            Else
                LembagaKeuangan = False
            End If
            If PKP = 1 Then
                PKP = True
            Else
                PKP = False
            End If
            If PJK = 1 Then
                PJK = True
            Else
                PJK = False
            End If
            NPWP = dr.Item("NPWP")
            JenisWP = dr.Item("Jenis_WP")
            LokasiWP = dr.Item("Lokasi_WP")
            Alamat = dr.Item("Alamat")
            Email = dr.Item("Email")
            PIC = dr.Item("PIC")
            RekeningBank = dr.Item("Rekening_Bank")
            AtasNama = dr.Item("Atas_Nama")
            DataTabelUtama.Rows.Add(KodeMitra, NamaMitra, Supplier, Customer, PemegangSaham, Afiliasi, Kategori, LembagaKeuangan, PKP, PJK, NPWP,
                                    JenisWP, LokasiWP, Alamat, Email, PIC, RekeningBank, AtasNama)
        Loop
        AksesDatabase_General(Tutup)

        DataTabelUtama.ClearSelection()
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False

    End Sub


    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        'frm_InputMitra.FungsiForm = FungsiForm_TAMBAH
        'frm_InputMitra.ShowDialog()
        win_InputLawanTransaksi = New wpfWin_InputLawanTransaksi
        win_InputLawanTransaksi.ResetForm()
        win_InputLawanTransaksi.FungsiForm = FungsiForm_TAMBAH
        win_InputLawanTransaksi.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        win_InputLawanTransaksi = New wpfWin_InputLawanTransaksi
        win_InputLawanTransaksi.ResetForm()
        win_InputLawanTransaksi.FungsiForm = FungsiForm_EDIT
        win_InputLawanTransaksi.txt_KodeLawanTransaksi.Text = KodeMitraTerseleksi
        win_InputLawanTransaksi.ShowDialog()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click
        FiturDalamPengembangan()
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        KodeMitraTerseleksi = DataTabelUtama.Item("Kode_Mitra", BarisTerseleksi).Value
        btn_Edit.Enabled = True
    End Sub

End Class