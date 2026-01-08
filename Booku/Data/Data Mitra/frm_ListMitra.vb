Imports bcomm
Imports System.Data.Odbc

Public Class frm_ListMitra

    Dim QueryTampilan
    Dim BarisTerseleksi

    Public PilihJenisLawanTransaksi
    Public PilihLembagaKeuangan
    Public PilihPemegangSaham
    Public PilihAfiliasi
    Public PilihLokasiWP
    Public FilterData

    Public JalurMasuk

    Public KodeMitraTerseleksi As String
    Public NamaMitraTerseleksi As String
    Public NPWPTerseleksi As String
    Public JenisWPTerseleksi As String
    Public AlamatMitraTerseleksi As String

    Dim JenisLawanTransaksi

    Public TampilkanDataPemegangSaham As Boolean

    Private Sub frm_COA_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If PilihJenisLawanTransaksi = Mitra_Supplier Then
            Me.Text = "Daftar Supplier"
            lbl_CariMitra.Text = "Cari Supplier :"
            btn_TambahDataMitra.Text = "Tambah Data Supplier"
            DataTabelUtama.Columns("Kode_Mitra").HeaderText = "Kode Supplier"
            DataTabelUtama.Columns("Nama_Mitra").HeaderText = "Nama Supplier"
            cmb_JenisLawanTransaksi.Text = Mitra_Supplier
            cmb_JenisLawanTransaksi.Enabled = False
        End If
        If PilihJenisLawanTransaksi = Mitra_Customer Then
            Me.Text = "Daftar Customer"
            lbl_CariMitra.Text = "Cari Customer :"
            btn_TambahDataMitra.Text = "Tambah Data Customer"
            DataTabelUtama.Columns("Kode_Mitra").HeaderText = "Kode Customer"
            DataTabelUtama.Columns("Nama_Mitra").HeaderText = "Nama Customer"
            cmb_JenisLawanTransaksi.Text = Mitra_Customer
            cmb_JenisLawanTransaksi.Enabled = False
        End If
        If PilihJenisLawanTransaksi = Pilihan_Semua Then
            Me.Text = "Daftar Mitra"
            lbl_CariMitra.Text = "Cari Mitra :"
            btn_TambahDataMitra.Text = "Tambah Data Mitra"
            DataTabelUtama.Columns("Kode_Mitra").HeaderText = "Kode Mitra"
            DataTabelUtama.Columns("Nama_Mitra").HeaderText = "Nama Mitra"
            cmb_JenisLawanTransaksi.Text = Pilihan_Semua
            cmb_JenisLawanTransaksi.Enabled = True
        End If
        RefreshTampilanData()
        txt_CariMitra.Focus()

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        'Filter Kategori :
        Dim FilterJenisLawanTransaksi = " "
        If JenisLawanTransaksi = Pilihan_Semua Then FilterJenisLawanTransaksi = " "
        If JenisLawanTransaksi = Mitra_Supplier Then FilterJenisLawanTransaksi = " AND Supplier = 1 "
        If JenisLawanTransaksi = Mitra_Customer Then FilterJenisLawanTransaksi = " AND Customer = 1 "

        'Filter Lembaga Keuangan :
        Dim FilterLembagaKeuangan = " "
        If PilihLembagaKeuangan = Pilihan_Semua Then FilterLembagaKeuangan = " "
        If PilihLembagaKeuangan = Pilihan_Ya Then FilterLembagaKeuangan = " AND Keuangan = 1 "
        If PilihLembagaKeuangan = Pilihan_Tidak Then FilterLembagaKeuangan = " AND Keuangan = 0 "

        'Filter PemegangSaham :
        Dim FilterPemegangSaham = " "
        If PilihPemegangSaham = Pilihan_Semua Then FilterPemegangSaham = " "
        If PilihPemegangSaham = Pilihan_Ya Then FilterPemegangSaham = " AND Pemegang_Saham = 1 "
        If PilihPemegangSaham = Pilihan_Tidak Then FilterPemegangSaham = " AND Pemegang_Saham = 0 "

        'Filter Afiliasi :
        Dim FilterAfiliasi = " "
        If PilihAfiliasi = Pilihan_Semua Then FilterAfiliasi = " "
        If PilihAfiliasi = Pilihan_Ya Then FilterAfiliasi = " AND Afiliasi = 1 "
        If PilihAfiliasi = Pilihan_Tidak Then FilterAfiliasi = " AND Afiliasi = 0 "

        'Filter Lokasi :
        Dim FilterLokasi = " "
        If PilihLokasiWP = Pilihan_Semua Then FilterLokasi = " "
        If PilihLokasiWP = LokasiWP_DalamNegeri Then FilterLokasi = " AND Lokasi_WP = '" & LokasiWP_DalamNegeri & "'"
        If PilihLokasiWP = LokasiWP_LuarNegeri Then FilterLokasi = " AND Lokasi_WP = '" & LokasiWP_LuarNegeri & "'"

        'Filter Pencarian
        Dim FilterPencarian = " "
        If txt_CariMitra.Text <> "" Then
            Dim Srch = txt_CariMitra.Text
            Dim clm_KodeMitra = " Kode_Mitra LIKE '%" & Srch & "%' "
            Dim clm_NamaMitra = " OR Nama_Mitra LIKE '%" & Srch & "%' "
            FilterPencarian = " AND (" & clm_KodeMitra & clm_NamaMitra & ") "
        End If

        'Query Tampilan :
        FilterData = FilterJenisLawanTransaksi & FilterLembagaKeuangan & FilterPemegangSaham & FilterAfiliasi & FilterLokasi & FilterPencarian
        QueryTampilan = " SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra <> 'X' " & FilterData

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        AksesDatabase_General(Buka)
        'Tampilkan Data Mitra :
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Kode_Mitra ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()
        Do While dr.Read
            Dim KodeMitra = dr.Item("Kode_Mitra")
            Dim NamaMitra = dr.Item("Nama_Mitra")
            Dim NPWP = dr.Item("NPWP")
            Dim JenisWP = dr.Item("Jenis_WP")
            Dim SebagaiSupplier = dr.Item("Supplier")
            Dim SebagaiCustomer = dr.Item("Customer")
            Dim AlamatMitra = dr.Item("Alamat")
            DataTabelUtama.Rows.Add(KodeMitra, NamaMitra, NPWP, JenisWP, SebagaiSupplier, SebagaiCustomer, AlamatMitra)
        Loop
        AksesDatabase_General(Tutup)
        If PilihJenisLawanTransaksi = Pilihan_Semua _
            And PilihLembagaKeuangan = Pilihan_Semua _
            And PilihPemegangSaham = Pilihan_Semua _
            And PilihAfiliasi = Pilihan_Semua _
            Then
            DataTabelUtama.Rows.Add(KodeLawanTransaksi_Karyawan, NamaLawanTransaksi_Karyawan)
            DataTabelUtama.Rows.Add(KodeLawanTransaksi_Internal, NamaLawanTransaksi_Internal)
        End If
        BersihkanSeleksi()
        txt_CariMitra.Focus()
    End Sub

    Public Sub ResetForm()
        KontenComboKategori()
        PilihJenisLawanTransaksi = Pilihan_Semua
        PilihLembagaKeuangan = Pilihan_Semua
        PilihPemegangSaham = Pilihan_Semua
        PilihAfiliasi = Pilihan_Semua
        PilihLokasiWP = Pilihan_Semua
        TampilkanDataPemegangSaham = False
        lbl_CariMitra.Text = "Cari Mitra :"
        txt_CariMitra.Text = ""
        btn_TambahDataMitra.Text = "Tambah Data Mitra"
        btn_Pilih.Enabled = False
        BarisTerseleksi = Nothing
        KodeMitraTerseleksi = Nothing
        NamaMitraTerseleksi = Nothing
        NPWPTerseleksi = Nothing
        JenisWPTerseleksi = Nothing
        DataTabelUtama.Rows.Clear()
    End Sub

    Sub KontenComboKategori()
        cmb_JenisLawanTransaksi.Enabled = True
        cmb_JenisLawanTransaksi.Items.Clear()
        cmb_JenisLawanTransaksi.Items.Add(Pilihan_Semua)
        cmb_JenisLawanTransaksi.Items.Add(Mitra_Supplier)
        cmb_JenisLawanTransaksi.Items.Add(Mitra_Customer)
        cmb_JenisLawanTransaksi.Text = Pilihan_Semua
    End Sub

    Sub BersihkanSeleksi()
        btn_Pilih.Enabled = False
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
    End Sub

    Private Sub txt_CariMitra_TextChanged(sender As Object, e As EventArgs) Handles txt_CariMitra.TextChanged
        TampilkanData()
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        If BarisTerseleksi >= 0 Then
            btn_Pilih.Enabled = True
        Else
            btn_Pilih.Enabled = False
        End If
    End Sub
    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        btn_Pilih_Click(sender, e)
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

    Private Sub cmb_JenisLawanTansaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisLawanTransaksi.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub cmb_JenisLawanTansaksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisLawanTransaksi.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisLawanTansaksi_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisLawanTransaksi.TextChanged
        JenisLawanTransaksi = cmb_JenisLawanTransaksi.Text
        TampilkanData()
    End Sub

    Private Sub btn_TambahDataMitra_Click(sender As Object, e As EventArgs) Handles btn_TambahDataMitra.Click
        'frm_InputMitra.btn_Reset_Click(sender, e)
        'frm_InputMitra.FungsiForm = FungsiForm_TAMBAH
        'frm_InputMitra.ShowDialog()
        win_InputLawanTransaksi = New wpfWin_InputLawanTransaksi
        win_InputLawanTransaksi.ResetForm()
        win_InputLawanTransaksi.FungsiForm = FungsiForm_TAMBAH
        win_InputLawanTransaksi.ShowDialog()
    End Sub

    Private Sub btn_Pilih_Click(sender As Object, e As EventArgs) Handles btn_Pilih.Click
        If BarisTerseleksi < 0 Then
            PesanUntukProgrammer("Tidak ada baris terseleksi.!!!")
            Return
        End If
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        KodeMitraTerseleksi = DataTabelUtama.Item("Kode_Mitra", BarisTerseleksi).Value
        NamaMitraTerseleksi = DataTabelUtama.Item("Nama_Mitra", BarisTerseleksi).Value
        NPWPTerseleksi = DataTabelUtama.Item("NPWP", BarisTerseleksi).Value
        JenisWPTerseleksi = DataTabelUtama.Item("Jenis_WP", BarisTerseleksi).Value
        AlamatMitraTerseleksi = DataTabelUtama.Item("Alamat_Mitra", BarisTerseleksi).Value
        Dim SebagaiSupplier = AmbilAngka(DataTabelUtama.Item("Supplier", BarisTerseleksi).Value)
        Dim SebagaiCustomer = AmbilAngka(DataTabelUtama.Item("Customer", BarisTerseleksi).Value)
        If PilihJenisLawanTransaksi = Mitra_Supplier And SebagaiSupplier = 0 Then
            Dim PilihEditMitra = MessageBox.Show(NamaMitraTerseleksi & " belum tercatat sebagai SUPPLIER di database." & Enter2Baris & "Ingin mengedit data " & NamaMitraTerseleksi & "..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If PilihEditMitra = vbYes Then
                frm_InputMitra.FungsiForm = FungsiForm_EDIT
                frm_InputMitra.StatusEdit = "BATAL"
                frm_InputMitra.txt_KodeMitra.Text = KodeMitraTerseleksi
                frm_InputMitra.ShowDialog()
                If frm_InputMitra.StatusEdit = "BATAL" Then Return 'Jika tidak ada pengeditan data, maka return
                If frm_InputMitra.chk_Supplier.Checked = False Then Return 'Jika mitra yang bersangkutan tidak dijadikan sebagai SUPPLIER, maka RETURN
            Else
                Return
            End If
        End If
        If PilihJenisLawanTransaksi = Mitra_Customer And SebagaiCustomer = 0 Then
            Dim PilihEditMitra = MessageBox.Show(NamaMitraTerseleksi & " belum tercatat sebagai CUSTOMER di database." & Enter2Baris & "Ingin mengedit data " & NamaMitraTerseleksi & "..?", "Perhatian..!", MessageBoxButtons.YesNo)
            If PilihEditMitra = vbYes Then
                frm_InputMitra.FungsiForm = FungsiForm_EDIT
                frm_InputMitra.StatusEdit = "BATAL"
                frm_InputMitra.txt_KodeMitra.Text = KodeMitraTerseleksi
                frm_InputMitra.ShowDialog()
                If frm_InputMitra.StatusEdit = "BATAL" Then Return 'Jika tidak ada pengeditan data, maka return
                If frm_InputMitra.chk_Customer.Checked = False Then Return 'Jika mitra yang bersangkutan tidak dijadikan sebagai CUSTOMER, maka RETURN
            Else
                Return
            End If
        End If
        txt_CariMitra.Text = Kosongan
        Me.Close()
    End Sub

End Class