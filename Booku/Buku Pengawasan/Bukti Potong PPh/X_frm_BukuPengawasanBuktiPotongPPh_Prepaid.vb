Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuPengawasanBuktiPotongPPh_Prepaid

    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim SumberData
    Dim NomorID
    Dim NomorPatokan
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim NomorFakturPajak
    Dim KodeCustomer
    Dim NamaCustomer
    Dim JenisPPh
    Dim DPP
    Dim JumlahPPh
    Dim TanggalDipotong
    Dim NomorBuktiPotong
    Dim TanggalBuktiPotong
    Dim Keterangan
    Dim NomorJV

    Dim BarisTerseleksi
    Dim NomorUrut_Terseleksi
    Dim SumberData_Terseleksi
    Dim NomorID_Terseleksi
    Dim NomorPatokan_Terseleksi
    Dim NomorInvoice_Terseleksi
    Dim TanggalInvoice_Terseleksi
    Dim NomorFakturPajak_Terseleksi
    Dim KodeCustomer_Terseleksi
    Dim NamaCustomer_Terseleksi
    Dim DPP_Terseleksi
    Dim JenisPPh_Terseleksi
    Dim JumlahPPh_Terseleksi
    Dim TanggalDipotong_Terseleksi
    Dim NomorBuktiPotong_Terseleksi
    Dim TanggalBuktiPotong_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi

    'Variabel Sumber Data :
    Public SumberData_Penjualan = "Penjualan"
    Public SumberData_PencairanPiutangUsaha = "Piutang Usaha"
    Public SumberData_PencairanPiutangPihakKetiga = "Piutang Pihak Ketiga"
    Public SumberData_PencairanPiutangAfiliasi = "Piutang Afiliasi"
    Public SumberData_PiutangDividen = "Piutang Dividen"

    'Variabel Bukti Potong Sudah/Belum Diterima :
    Dim BP_SudahDiterima = "Sudah Diterima"
    Dim BP_BelumDiterima = "Belum Diterima"

    'Variabel Filter :
    Dim PilihanSumberData
    Dim PilihanJenisPPh
    Dim PilihanBuktiPotong
    Dim FilterJenisPPh
    Dim FilterJenisPajak
    Dim FilterBuktiPotong

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        KontenCombo_SumberData()
        KontenCombo_JenisPPh()
        KontenCombo_BuktiPotong()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        'Filter Sumber Data :
        'Untuk Filter Sumber Data, ada Query-nya tersendiri.

        'Filter Jenis PPh :
        If PilihanJenisPPh = Pilihan_Semua Then FilterJenisPPh = " AND ( Jenis_PPh = '" & JenisPPh_Pasal23 & "' OR Jenis_PPh = '" & JenisPPh_Pasal42 & "' ) "
        If PilihanJenisPPh = JenisPPh_Pasal23 Then FilterJenisPPh = " AND Jenis_PPh = '" & JenisPPh_Pasal23 & "' "
        If PilihanJenisPPh = JenisPPh_Pasal42 Then FilterJenisPPh = " AND Jenis_PPh = '" & JenisPPh_Pasal42 & "' "

        'Filter Bukti Potong
        If PilihanBuktiPotong = Pilihan_Semua Then FilterBuktiPotong = " "
        If PilihanBuktiPotong = BP_SudahDiterima Then FilterBuktiPotong = " AND Nomor_Bukti_Potong <> '' "
        If PilihanBuktiPotong = BP_BelumDiterima Then FilterBuktiPotong = " AND Nomor_Bukti_Potong = '' "

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel
        NomorUrut = 0

        AksesDatabase_Transaksi(Buka)

        'PPh Dari Penjualan Tunai :
        If PilihanSumberData = SumberData_Penjualan _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_Penjualan
            cmd = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                  " WHERE PPh_Terutang > 0 " &
                                  " AND Jenis_Penjualan = '" & JenisPenjualan_Tunai & "' " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Dim NomorPenjualanSebelumnya = Kosongan
            Do While dr.Read
                NomorPatokan = dr.Item("Nomor_Penjualan")
                If NomorPatokan <> NomorPenjualanSebelumnya Then
                    NomorUrut += 1
                    NomorID = dr.Item("Nomor_ID")
                    NomorInvoice = dr.Item("Nomor_Invoice")
                    TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
                    KodeCustomer = dr.Item("Kode_Customer")
                    NamaCustomer = dr.Item("Nama_Customer")
                    DPP = dr.Item("Dasar_Pengenaan_Pajak")
                    JenisPPh = dr.Item("Jenis_PPh")
                    JumlahPPh = dr.Item("PPh_Terutang")
                    TanggalDipotong = TanggalInvoice
                    NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                    TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                    If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                    If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                    Keterangan = dr.Item("Keterangan_Bukti_Potong")
                    NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                    TambahBaris()
                End If
                NomorPenjualanSebelumnya = NomorPatokan
            Loop

        End If

        'PPh Dari Pencairan atas Piutang Usaha :
        If PilihanSumberData = SumberData_PencairanPiutangUsaha _
            Or PilihanSumberData = Pilihan_Semua Then
            FilterJenisPajak = Replace(FilterJenisPPh, "Jenis_PPh", "Jenis_Pajak")  'Karena Perbedaan Nama Kolom, jagi begini..!!!
            FilterJenisPajak = Replace(FilterJenisPajak, "Pasal", "PPh Pasal")      'Karena Perbedaan Nama Kolom, jagi begini..!!!
            SumberData = SumberData_PencairanPiutangUsaha
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPenerimaan " &
                                  " WHERE Peruntukan = '" & Peruntukan_PencairanPiutangUsaha_NonAfiliasi & "' AND PPh_Terutang > 0 " &
                                  FilterJenisPajak & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorID = dr.Item("Nomor_ID")
                NomorPatokan = KonversiNomorBPPUKeNomorPenjualan(dr.Item("Nomor_BP"))
                KodeCustomer = dr.Item("Kode_Lawan_Transaksi")
                NamaCustomer = dr.Item("Nama_Lawan_Transaksi")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                             " WHERE Nomor_Penjualan = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                Dim DPPPenjualan As Int64 = 0
                Dim PiutangUsaha As Int64 = 0
                If drTELUSUR.HasRows Then
                    NomorInvoice = drTELUSUR.Item("Nomor_Invoice")
                    TanggalInvoice = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_Invoice"))
                    NomorFakturPajak = drTELUSUR.Item("Nomor_Faktur_Pajak")
                    DPPPenjualan = drTELUSUR.Item("Dasar_Pengenaan_Pajak")
                    PiutangUsaha = drTELUSUR.Item("Jumlah_Piutang_Usaha")
                End If
                Dim JumlahBayar = dr.Item("Jumlah_Bayar")
                DPP = JumlahBayar * DPPPenjualan / PiutangUsaha
                JenisPPh = KonversiJenisPajakKeJenisPPh(dr.Item("Jenis_Pajak"))
                JumlahPPh = dr.Item("PPh_Terutang")
                TanggalDipotong = TanggalFormatTampilan(dr.Item("Tanggal_KM"))
                NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                Keterangan = dr.Item("Keterangan_Bukti_Potong")
                NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                TambahBaris()
            Loop
        End If

        'PPh Dari Pencairan Piutang Pihak Ketiga :
        If PilihanSumberData = SumberData_PencairanPiutangPihakKetiga _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_PencairanPiutangPihakKetiga
            cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangPihakKetiga " &
                                  " WHERE Jumlah_PPh > 0 " &
                                  " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorPatokan = dr.Item("Nomor_BPPPK")
                NomorID = dr.Item("Nomor_ID")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangPihakKetiga " &
                                         " WHERE Nomor_BPPPK = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NomorInvoice = drTELUSUR.Item("Nomor_Kontrak")
                    TanggalInvoice = StripKosong
                    NomorFakturPajak = StripKosong
                End If
                KodeCustomer = dr.Item("Kode_Lawan_Transaksi")
                NamaCustomer = AmbilValue_NamaMitra(KodeCustomer)
                DPP = dr.Item("Pokok")
                JenisPPh = dr.Item("Jenis_PPh")
                JumlahPPh = dr.Item("Jumlah_PPh")
                TanggalDipotong = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                Keterangan = dr.Item("Keterangan_Bukti_Potong")
                NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                TambahBaris()
            Loop
        End If

        'PPh Dari Pencairan Piutang Afiliasi :
        If PilihanSumberData = SumberData_PencairanPiutangAfiliasi _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_PencairanPiutangAfiliasi
            cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranPiutangAfiliasi " &
                                  " WHERE Jumlah_PPh > 0 " &
                                  " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorPatokan = dr.Item("Nomor_BPPA")
                NomorID = dr.Item("Nomor_ID")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangAfiliasi " &
                                         " WHERE Nomor_BPPA = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NomorInvoice = drTELUSUR.Item("Nomor_Kontrak")
                    TanggalInvoice = StripKosong
                    NomorFakturPajak = StripKosong
                End If
                KodeCustomer = dr.Item("Kode_Lawan_Transaksi")
                NamaCustomer = AmbilValue_NamaMitra(KodeCustomer)
                DPP = dr.Item("Pokok")
                JenisPPh = dr.Item("Jenis_PPh")
                JumlahPPh = dr.Item("Jumlah_PPh")
                TanggalDipotong = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
                NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                Keterangan = dr.Item("Keterangan_Bukti_Potong")
                NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                TambahBaris()
            Loop
        End If

        'PPh Dari Piutang Dividen :
        If PilihanSumberData = SumberData_PiutangDividen _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_PiutangDividen
            cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanPiutangDividen " &
                                  " WHERE PPh_Terutang > 0 " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorPatokan = dr.Item("Nomor_BPPD")
                NomorID = dr.Item("Nomor_ID")
                NomorInvoice = dr.Item("Nomor_Akta_Notaris")
                TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                NomorFakturPajak = StripKosong
                KodeCustomer = dr.Item("Kode_Lawan_Transaksi")
                NamaCustomer = AmbilValue_NamaMitra(KodeCustomer)
                DPP = dr.Item("Jumlah_Dividen")
                JenisPPh = dr.Item("Jenis_PPh")
                JumlahPPh = dr.Item("PPh_Terutang")
                TanggalDipotong = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                NomorBuktiPotong = dr.Item("Nomor_Bukti_Potong")
                TanggalBuktiPotong = TanggalFormatTampilan(dr.Item("Tanggal_Bukti_Potong"))
                If NomorBuktiPotong = Kosongan Then NomorBuktiPotong = StripKosong
                If TanggalBuktiPotong = TanggalKosong Then TanggalBuktiPotong = StripKosong
                Keterangan = dr.Item("Keterangan_Bukti_Potong")
                NomorJV = dr.Item("Nomor_JV_Bukti_Potong")
                TambahBaris()
            Loop
        End If

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub TambahBaris()
        DataTabelUtama.Rows.Add(NomorUrut, SumberData, NomorID, NomorPatokan, NomorInvoice, TanggalInvoice, NomorFakturPajak, KodeCustomer, NamaCustomer,
                                DPP, JenisPPh, JumlahPPh, TanggalDipotong, NomorBuktiPotong, TanggalBuktiPotong, Keterangan, NomorJV)
    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        NomorUrut_Terseleksi = 0
        DataTabelUtama.ClearSelection()
        btn_LihatJurnal.Enabled = False
        btn_Input.Enabled = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Sub KontenCombo_SumberData()
        cmb_SumberData.Items.Clear()
        cmb_SumberData.Items.Add(Pilihan_Semua)
        cmb_SumberData.Items.Add(SumberData_Penjualan)
        cmb_SumberData.Items.Add(SumberData_PencairanPiutangUsaha)
        cmb_SumberData.Items.Add(SumberData_PencairanPiutangPihakKetiga)
        cmb_SumberData.Items.Add(SumberData_PencairanPiutangAfiliasi)
        cmb_SumberData.Items.Add(SumberData_PiutangDividen)
        cmb_SumberData.Text = Pilihan_Semua
    End Sub

    Sub KontenCombo_JenisPPh()
        cmb_JenisPPh.Items.Clear()
        cmb_JenisPPh.Items.Add(Pilihan_Semua)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal23)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal42)
        cmb_JenisPPh.Text = Pilihan_Semua
    End Sub

    Sub KontenCombo_BuktiPotong()
        cmb_BuktiPotong.Items.Clear()
        cmb_BuktiPotong.Items.Add(Pilihan_Semua)
        cmb_BuktiPotong.Items.Add(BP_SudahDiterima)
        cmb_BuktiPotong.Items.Add(BP_BelumDiterima)
        cmb_BuktiPotong.Text = Pilihan_Semua
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub cmb_SumberData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_SumberData.SelectedIndexChanged
    End Sub
    Private Sub cmb_SumberData_TextChanged(sender As Object, e As EventArgs) Handles cmb_SumberData.TextChanged
        PilihanSumberData = cmb_SumberData.Text
        TampilkanData()
    End Sub

    Private Sub cmb_JenisPPh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisPPh_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisPPh.TextChanged
        PilihanJenisPPh = cmb_JenisPPh.Text
        TampilkanData()
    End Sub

    Private Sub cmb_BuktiPotong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_BuktiPotong.SelectedIndexChanged
    End Sub
    Private Sub cmb_BuktiPotong_TextChanged(sender As Object, e As EventArgs) Handles cmb_BuktiPotong.TextChanged
        PilihanBuktiPotong = cmb_BuktiPotong.Text
        TampilkanData()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Terseleksi)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub


    Private Sub btn_Input_Click(sender As Object, e As EventArgs) Handles btn_Input.Click

        frm_InputBuktiPotongPPh.ResetForm()
        frm_InputBuktiPotongPPh.FungsiForm = FungsiForm_TAMBAH
        IsiValueFormInput()
        frm_InputBuktiPotongPPh.ShowDialog()

    End Sub
    Sub IsiValueFormInput()
        frm_InputBuktiPotongPPh.JalurMasuk = frm_InputBuktiPotongPPh.JalurMasuk_Prepaid
        frm_InputBuktiPotongPPh.SumberData = SumberData_Terseleksi
        frm_InputBuktiPotongPPh.NomorID = NomorID_Terseleksi
        frm_InputBuktiPotongPPh.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        frm_InputBuktiPotongPPh.dtp_TanggalInvoice.Value = TanggalInvoice_Terseleksi
        frm_InputBuktiPotongPPh.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        frm_InputBuktiPotongPPh.KodeLawanTransaksi = KodeCustomer_Terseleksi
        frm_InputBuktiPotongPPh.txt_NamaLawanTransaksi.Text = NamaCustomer_Terseleksi
        frm_InputBuktiPotongPPh.txt_DPP.Text = DPP_Terseleksi
        frm_InputBuktiPotongPPh.txt_JenisPPh.Text = JenisPPh_Terseleksi
        frm_InputBuktiPotongPPh.txt_PPhTerutang.Text = JumlahPPh_Terseleksi
        frm_InputBuktiPotongPPh.dtp_TanggalDipotong.Value = TanggalDipotong_Terseleksi
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click

        frm_InputBuktiPotongPPh.ResetForm()
        frm_InputBuktiPotongPPh.FungsiForm = FungsiForm_EDIT
        IsiValueFormInput()
        frm_InputBuktiPotongPPh.txt_NomorBuktiPotong.Text = NomorBuktiPotong_Terseleksi
        frm_InputBuktiPotongPPh.dtp_TanggalBuktiPotong.Value = TanggalBuktiPotong_Terseleksi
        frm_InputBuktiPotongPPh.txt_Keterangan.Text = Keterangan_Terseleksi
        frm_InputBuktiPotongPPh.NomorJV_BuktiPotong = NomorJV_Terseleksi
        frm_InputBuktiPotongPPh.ShowDialog()

    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        Pilihan = MessageBox.Show("Yakin akan menghapus data 'Bukti Potong' pada baris terpilih?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        Dim QueryAwal = Kosongan
        Dim QueryAkhir = Kosongan
        Select Case SumberData_Terseleksi
            Case SumberData_Penjualan
                QueryAwal = "  UPDATE tbl_Penjualan_Invoice "
                QueryAkhir = " WHERE Nomor_Invoice     = '" & NomorInvoice_Terseleksi & "' "
            Case SumberData_PencairanPiutangUsaha
                QueryAwal = "  UPDATE tbl_BuktiPenerimaan "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PencairanPiutangPihakKetiga
                QueryAwal = "  UPDATE tbl_JadwalAngsuranPiutangPihakKetiga "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PencairanPiutangAfiliasi
                QueryAwal = "  UPDATE tbl_JadwalAngsuranPiutangAfiliasi "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PiutangDividen
                QueryAwal = "  UPDATE tbl_PengawasanPiutangDividen "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
        End Select
        Dim KolomKolomYangDiedit = " SET " &
            " Nomor_Bukti_Potong      = '" & Kosongan & "', " &
            " Tanggal_Bukti_Potong    = '" & TanggalKosongSimpan & "', " &
            " Keterangan_Bukti_Potong = '" & Keterangan & "', " &
            " Nomor_JV_Bukti_Potong   = 0 "
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryAwal & KolomKolomYangDiedit & QueryAkhir, KoneksiDatabaseTransaksi)
        cmd_ExecuteNonQuery()
        'Hapus Jurnal yang lama :
        cmd = New OdbcCommand(" DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & NomorJV_Terseleksi & "' ", KoneksiDatabaseTransaksi)
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
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", BarisTerseleksi).Value)
        SumberData_Terseleksi = DataTabelUtama.Item("Sumber_Data", BarisTerseleksi).Value
        NomorID_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_ID", BarisTerseleksi).Value)
        NomorPatokan_Terseleksi = DataTabelUtama.Item("Nomor_Patokan", BarisTerseleksi).Value
        NomorInvoice_Terseleksi = DataTabelUtama.Item("Nomor_Invoice", BarisTerseleksi).Value
        TanggalInvoice_Terseleksi = DataTabelUtama.Item("Tanggal_Invoice", BarisTerseleksi).Value
        NomorFakturPajak_Terseleksi = DataTabelUtama.Item("Nomor_Faktur_Pajak", BarisTerseleksi).Value
        KodeCustomer_Terseleksi = DataTabelUtama.Item("Kode_Customer", BarisTerseleksi).Value
        NamaCustomer_Terseleksi = DataTabelUtama.Item("Nama_Customer", BarisTerseleksi).Value
        JenisPPh_Terseleksi = DataTabelUtama.Item("Jenis_PPh", BarisTerseleksi).Value
        DPP_Terseleksi = AmbilAngka(DataTabelUtama.Item("DPP_", BarisTerseleksi).Value)
        JumlahPPh_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_PPh", BarisTerseleksi).Value)
        TanggalDipotong_Terseleksi = DataTabelUtama.Item("Tanggal_Dipotong", BarisTerseleksi).Value
        NomorBuktiPotong_Terseleksi = DataTabelUtama.Item("Nomor_Bukti_Potong", BarisTerseleksi).Value
        TanggalBuktiPotong_Terseleksi = DataTabelUtama.Item("Tanggal_Bukti_Potong", BarisTerseleksi).Value
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", BarisTerseleksi).Value
        NomorJV_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", BarisTerseleksi).Value)

        PesanUntukProgrammer(
            "Nomor Urut : " & NomorUrut_Terseleksi & Enter2Baris &
            "Nomor JV : " & NomorJV_Terseleksi)

        If NomorUrut_Terseleksi > 0 Then
            If NomorJV_Terseleksi > 0 Then
                btn_LihatJurnal.Enabled = True
            Else
                btn_LihatJurnal.Enabled = False
            End If
            If NomorBuktiPotong_Terseleksi <> StripKosong Then
                btn_Input.Enabled = False
                btn_Edit.Enabled = True
                btn_Hapus.Enabled = True
            Else
                btn_Input.Enabled = True
                btn_Edit.Enabled = False
                btn_Hapus.Enabled = False
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