Imports System.Data.Odbc
Imports bcomm

Public Class X_frm_BukuPengawasanBuktiPotongPPh_Paid

    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim SumberData
    Dim NomorID
    Dim NomorPatokan
    Dim NomorInvoice
    Dim TanggalInvoice
    Dim NomorFakturPajak
    Dim KodeSupplier
    Dim NamaSupplier
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
    Dim KodeSupplier_Terseleksi
    Dim NamaSupplier_Terseleksi
    Dim DPP_Terseleksi
    Dim JenisPPh_Terseleksi
    Dim JumlahPPh_Terseleksi
    Dim TanggalDipotong_Terseleksi
    Dim NomorBuktiPotong_Terseleksi
    Dim TanggalBuktiPotong_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Terseleksi

    'Variabel Sumber Data :
    Public SumberData_Pembelian = "Pembelian"
    Public SumberData_PembayaranHutangUsaha = "Hutang Usaha"
    Public SumberData_PembayaranHutangPihakKetiga = "Hutang Pihak Ketiga"
    Public SumberData_PembayaranHutangAfiliasi = "Hutang Afiliasi"
    Public SumberData_PembayaranHutangBank = "Hutang Bank"
    Public SumberData_PembayaranHutangLeasing = "Hutang Leasing"
    Public SumberData_HutangDividen = "Hutang Dividen"

    'Variabel Bukti Potong Sudah/Belum Diterima :
    Dim BP_SudahDilaporkan = "Sudah Dilaporkan"
    Dim BP_BelumDilaporkan = "Belum Dilaporkan"

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
        If PilihanJenisPPh = Pilihan_Semua Then FilterJenisPPh = " AND " &
            "  ( Jenis_PPh = '" & JenisPPh_Pasal21 & "' " &
            " OR Jenis_PPh = '" & JenisPPh_Pasal23 & "' " &
            " OR Jenis_PPh = '" & JenisPPh_Pasal42 & "' " &
            " OR Jenis_PPh = '" & JenisPPh_Pasal26 & "' ) "
        If PilihanJenisPPh = JenisPPh_Pasal21 Then FilterJenisPPh = " AND Jenis_PPh = '" & JenisPPh_Pasal21 & "' "
        If PilihanJenisPPh = JenisPPh_Pasal23 Then FilterJenisPPh = " AND Jenis_PPh = '" & JenisPPh_Pasal23 & "' "
        If PilihanJenisPPh = JenisPPh_Pasal42 Then FilterJenisPPh = " AND Jenis_PPh = '" & JenisPPh_Pasal42 & "' "
        If PilihanJenisPPh = JenisPPh_Pasal26 Then FilterJenisPPh = " AND Jenis_PPh = '" & JenisPPh_Pasal26 & "' "

        'Filter Bukti Potong
        If PilihanBuktiPotong = Pilihan_Semua Then FilterBuktiPotong = " "
        If PilihanBuktiPotong = BP_SudahDilaporkan Then FilterBuktiPotong = " AND Nomor_Bukti_Potong <> '' "
        If PilihanBuktiPotong = BP_BelumDilaporkan Then FilterBuktiPotong = " AND Nomor_Bukti_Potong = '' "

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel
        NomorUrut = 0

        AksesDatabase_Transaksi(Buka)

        'PPh Dari Pembelian Tunai :
        If PilihanSumberData = SumberData_Pembelian _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_Pembelian
            cmd = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                  " WHERE PPh_Terutang > 0 " &
                                  " AND Jenis_Pembelian = '" & JenisPembelian_Tunai & "' " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Dim NomorPatokanSebelumnya = Kosongan
            Do While dr.Read
                NomorPatokan = dr.Item("Nomor_Pembelian")
                If NomorPatokan <> NomorPatokanSebelumnya Then
                    NomorUrut += 1
                    NomorID = dr.Item("Nomor_ID")
                    NomorInvoice = dr.Item("Nomor_Invoice")
                    TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Invoice"))
                    NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
                    KodeSupplier = dr.Item("Kode_Supplier")
                    NamaSupplier = dr.Item("Nama_Supplier")
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
                NomorPatokanSebelumnya = NomorPatokan
            Loop

        End If

        'PPh Dari Pembayaran atas Hutang Usaha :
        If PilihanSumberData = SumberData_PembayaranHutangUsaha _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_PembayaranHutangUsaha
            FilterJenisPajak = Replace(FilterJenisPPh, "Jenis_PPh", "Jenis_Pajak")
            FilterJenisPajak = Replace(FilterJenisPajak, "Pasal", "PPh Pasal")
            cmd = New OdbcCommand(" SELECT * FROM tbl_BuktiPengeluaran " &
                                  " WHERE Peruntukan = '" & Peruntukan_PembayaranHutangUsaha_NonAfiliasi & "' AND PPh_Terutang > 0 " &
                                  FilterJenisPajak & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorID = dr.Item("Nomor_ID")
                NomorPatokan = KonversiNomorBPHUKeNomorPembelian(dr.Item("Nomor_BP"))
                KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                NamaSupplier = dr.Item("Nama_Lawan_Transaksi")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Pembelian_Invoice " &
                                             " WHERE Nomor_Pembelian = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                Dim DPPPembelian As Int64 = 0
                Dim HutangUsaha As Int64 = 0
                If drTELUSUR.HasRows Then
                    NomorInvoice = drTELUSUR.Item("Nomor_Invoice")
                    TanggalInvoice = TanggalFormatTampilan(drTELUSUR.Item("Tanggal_Invoice"))
                    NomorFakturPajak = drTELUSUR.Item("Nomor_Faktur_Pajak")
                    DPPPembelian = drTELUSUR.Item("Dasar_Pengenaan_Pajak")
                    HutangUsaha = drTELUSUR.Item("Jumlah_Hutang_Usaha")
                End If
                Dim JumlahBayar = dr.Item("Jumlah_Bayar")
                DPP = JumlahBayar * DPPPembelian / HutangUsaha
                JenisPPh = KonversiJenisPajakKeJenisPPh(dr.Item("Jenis_Pajak"))
                JumlahPPh = dr.Item("PPh_Terutang")
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

        'PPh Dari Pembayaran Hutang Pihak Ketiga :
        If PilihanSumberData = SumberData_PembayaranHutangPihakKetiga _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_PembayaranHutangPihakKetiga
            cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangPihakKetiga " &
                                  " WHERE Jumlah_PPh > 0 " &
                                  " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorPatokan = dr.Item("Nomor_BPHPK")
                NomorID = dr.Item("Nomor_ID")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangPihakKetiga " &
                                         " WHERE Nomor_BPHPK = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NomorInvoice = drTELUSUR.Item("Nomor_Kontrak")
                    TanggalInvoice = StripKosong
                    NomorFakturPajak = StripKosong
                End If
                KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                NamaSupplier = AmbilValue_NamaMitra(KodeSupplier)
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

        'PPh Dari Pembayaran Hutang Afiliasi :
        If PilihanSumberData = SumberData_PembayaranHutangAfiliasi _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_PembayaranHutangAfiliasi
            cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangAfiliasi " &
                                  " WHERE Jumlah_PPh > 0 " &
                                  " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorPatokan = dr.Item("Nomor_BPHA")
                NomorID = dr.Item("Nomor_ID")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangAfiliasi " &
                                         " WHERE Nomor_BPHA = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NomorInvoice = drTELUSUR.Item("Nomor_Kontrak")
                    TanggalInvoice = StripKosong
                    NomorFakturPajak = StripKosong
                End If
                KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                NamaSupplier = AmbilValue_NamaMitra(KodeSupplier)
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

        'PPh Dari Pembayaran Hutang Bank :
        If PilihanSumberData = SumberData_PembayaranHutangBank _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_PembayaranHutangBank
            cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangBank " &
                                  " WHERE Jumlah_PPh > 0 " &
                                  " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorPatokan = dr.Item("Nomor_BPHB")
                NomorID = dr.Item("Nomor_ID")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangBank " &
                                         " WHERE Nomor_BPHB = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NomorInvoice = drTELUSUR.Item("Nomor_Kontrak")
                    TanggalInvoice = StripKosong
                    NomorFakturPajak = StripKosong
                End If
                KodeSupplier = dr.Item("Kode_Kreditur")
                NamaSupplier = AmbilValue_NamaMitra(KodeSupplier)
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

        'PPh Dari Pembayaran Hutang Leasing :
        If PilihanSumberData = SumberData_PembayaranHutangLeasing _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_PembayaranHutangLeasing
            cmd = New OdbcCommand(" SELECT * FROM tbl_JadwalAngsuranHutangLeasing " &
                                  " WHERE Jumlah_PPh > 0 " &
                                  " AND Tanggal_Bayar <> '" & TanggalKosongSimpan & "' " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorPatokan = dr.Item("Nomor_BPHL")
                NomorID = dr.Item("Nomor_ID")
                cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangLeasing " &
                                         " WHERE Nomor_BPHL = '" & NomorPatokan & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    NomorInvoice = drTELUSUR.Item("Nomor_Kontrak")
                    TanggalInvoice = StripKosong
                    NomorFakturPajak = StripKosong
                End If
                KodeSupplier = dr.Item("Kode_Kreditur")
                NamaSupplier = AmbilValue_NamaMitra(KodeSupplier)
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

        'PPh Dari Hutang Dividen :
        If PilihanSumberData = SumberData_HutangDividen _
            Or PilihanSumberData = Pilihan_Semua Then
            SumberData = SumberData_HutangDividen
            cmd = New OdbcCommand(" SELECT * FROM tbl_PengawasanHutangDividen " &
                                  " WHERE PPh_Terutang > 0 " &
                                  FilterJenisPPh & FilterBuktiPotong, KoneksiDatabaseTransaksi)
            dr_ExecuteReader()
            Do While dr.Read
                NomorUrut += 1
                NomorPatokan = dr.Item("Nomor_BPHD")
                NomorID = dr.Item("Nomor_ID")
                NomorInvoice = dr.Item("Nomor_Akta_Notaris")
                TanggalInvoice = TanggalFormatTampilan(dr.Item("Tanggal_Akta_Notaris"))
                NomorFakturPajak = StripKosong
                KodeSupplier = dr.Item("Kode_Lawan_Transaksi")
                NamaSupplier = AmbilValue_NamaPemegangSaham(KodeSupplier)
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
        DataTabelUtama.Rows.Add(NomorUrut, SumberData, NomorID, NomorPatokan, NomorInvoice, TanggalInvoice, NomorFakturPajak, KodeSupplier, NamaSupplier,
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
        cmb_SumberData.Items.Add(SumberData_Pembelian)
        cmb_SumberData.Items.Add(SumberData_PembayaranHutangUsaha)
        cmb_SumberData.Items.Add(SumberData_PembayaranHutangPihakKetiga)
        cmb_SumberData.Items.Add(SumberData_PembayaranHutangAfiliasi)
        cmb_SumberData.Items.Add(SumberData_PembayaranHutangBank)
        cmb_SumberData.Items.Add(SumberData_PembayaranHutangLeasing)
        cmb_SumberData.Items.Add(SumberData_HutangDividen)
        cmb_SumberData.Text = Pilihan_Semua
    End Sub

    Sub KontenCombo_JenisPPh()
        cmb_JenisPPh.Items.Clear()
        cmb_JenisPPh.Items.Add(Pilihan_Semua)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal21)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal23)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal42)
        cmb_JenisPPh.Items.Add(JenisPPh_Pasal26)
        cmb_JenisPPh.Text = Pilihan_Semua
    End Sub

    Sub KontenCombo_BuktiPotong()
        cmb_BuktiPotong.Items.Clear()
        cmb_BuktiPotong.Items.Add(Pilihan_Semua)
        cmb_BuktiPotong.Items.Add(BP_SudahDilaporkan)
        cmb_BuktiPotong.Items.Add(BP_BelumDilaporkan)
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
        frm_InputBuktiPotongPPh.JalurMasuk = frm_InputBuktiPotongPPh.JalurMasuk_Paid
        frm_InputBuktiPotongPPh.SumberData = SumberData_Terseleksi
        frm_InputBuktiPotongPPh.NomorID = NomorID_Terseleksi
        frm_InputBuktiPotongPPh.txt_NomorInvoice.Text = NomorInvoice_Terseleksi
        frm_InputBuktiPotongPPh.dtp_TanggalInvoice.Value = TanggalInvoice_Terseleksi
        frm_InputBuktiPotongPPh.txt_NomorFakturPajak.Text = NomorFakturPajak_Terseleksi
        frm_InputBuktiPotongPPh.KodeLawanTransaksi = KodeSupplier_Terseleksi
        frm_InputBuktiPotongPPh.txt_NamaLawanTransaksi.Text = NamaSupplier_Terseleksi
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
            Case SumberData_Pembelian
                QueryAwal = "  UPDATE tbl_Pembelian_Invoice "
                QueryAkhir = " WHERE Nomor_Invoice     = '" & NomorInvoice_Terseleksi & "' "
            Case SumberData_PembayaranHutangUsaha
                QueryAwal = "  UPDATE tbl_BuktiPengeluaran "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PembayaranHutangPihakKetiga
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangPihakKetiga "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PembayaranHutangAfiliasi
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangAfiliasi "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PembayaranHutangBank
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangBank "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_PembayaranHutangLeasing
                QueryAwal = "  UPDATE tbl_JadwalAngsuranHutangLeasing "
                QueryAkhir = " WHERE Nomor_ID          = '" & NomorID_Terseleksi & "' "
            Case SumberData_HutangDividen
                QueryAwal = "  UPDATE tbl_PengawasanHutangDividen "
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
        KodeSupplier_Terseleksi = DataTabelUtama.Item("Kode_Supplier", BarisTerseleksi).Value
        NamaSupplier_Terseleksi = DataTabelUtama.Item("Nama_Supplier", BarisTerseleksi).Value
        JenisPPh_Terseleksi = DataTabelUtama.Item("Jenis_PPh", BarisTerseleksi).Value
        DPP_Terseleksi = AmbilAngka(DataTabelUtama.Item("DPP_", BarisTerseleksi).Value)
        JumlahPPh_Terseleksi = AmbilAngka(DataTabelUtama.Item("Jumlah_PPh", BarisTerseleksi).Value)
        TanggalDipotong_Terseleksi = DataTabelUtama.Item("Tanggal_Dipotong", BarisTerseleksi).Value
        NomorBuktiPotong_Terseleksi = DataTabelUtama.Item("Nomor_Bukti_Potong", BarisTerseleksi).Value
        TanggalBuktiPotong_Terseleksi = DataTabelUtama.Item("Tanggal_Bukti_Potong", BarisTerseleksi).Value
        Keterangan_Terseleksi = DataTabelUtama.Item("Keterangan_", BarisTerseleksi).Value
        NomorJV_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", BarisTerseleksi).Value)

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