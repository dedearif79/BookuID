Imports bcomm
Imports System.Data.Odbc

Module mdl_PrintSupport

    Dim KontenFile_VariabelAwal
    Dim KontenFile_KoneksiDatabase
    Dim NamaFile_VariabelAwal = "variabelawal.php"
    Dim NamaFile_KoneksiDatabase = "koneksidatabase.php"

    Public Sub KoneksiDatabaseWeb()
        KontenFile_KoneksiDatabase =
            "<?php " & Enter1Baris &
            Enter1Baris &
            "// Parameter Koneksi Database :" & Enter1Baris &
            "$Host = 'localhost';" & Enter1Baris &
            "$User = 'root';" & Enter1Baris &
            "$Pass = '';" & Enter1Baris &
            Enter1Baris &
            "// Jenis Database :" & Enter1Baris &
            "$DatabaseGenearal = '" & NamaDatabaseGeneral & "';" & Enter1Baris &
            "$DatabaseTransaksi = '" & NamaDatabaseTransaksi & "';" & Enter1Baris &
            Enter1Baris &
            "// Koneksi Database " & Enter1Baris &
            "$KoneksiDatabaseGeneral = mysqli_connect($Host, $User, $Pass, $DatabaseGenearal);" & Enter1Baris &
            "$KoneksiDatabaseTransaksi = mysqli_connect($Host, $User, $Pass, $DatabaseTransaksi);" & Enter1Baris &
            Enter1Baris &
            "?>"

        Try
            My.Computer.FileSystem.WriteAllText(LokasiFolderXAMPP & "htdocs\rekend\sat\cetak\php\" & NamaFile_KoneksiDatabase, KontenFile_KoneksiDatabase, False)
        Catch ex As Exception
            pesan_AdaKesalahanTeknis_SilakanUlangiBeberapaSaat()
            Return
        End Try

    End Sub

    Dim Konten_VariabelPerusahaan
    Public Sub Isi_VariabelPerusahaan()
        Konten_VariabelPerusahaan =
            "// Variabel Perusahaan :" & Enter1Baris &
            "$NamaPerusahaan = '" & NamaPerusahaan & "';" & Enter1Baris &
            "$TaglinePerusahaan = '" & TaglinePerusahaan & "';" & Enter1Baris &
            "$AlamatPerusahaan = '" & AlamatPerusahaan & "';" & Enter1Baris &
            "$NomorKontak = '" & PICPerusahaan & "';" & Enter1Baris &
            "$NamaDirekturPerusahaan = '" & NamaDirekturPerusahaan & "';"
    End Sub

    Public AngkaPOPembelian_Cetak
    Public Sub TampilkanHalamanCetak_POPembelian()

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Pembelian_PO WHERE Angka_PO = '" & AngkaPOPembelian_Cetak & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        Dim KodeSupplier = dr.Item("Kode_Supplier")
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeSupplier & "'", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        drTELUSUR.Read()
        Dim NamaSupplier = drTELUSUR.Item("Nama_Mitra")
        Dim AlamatSupplier = drTELUSUR.Item("Alamat")
        AksesDatabase_General(Tutup)
        Dim Attention = dr.Item("Attention")
        Dim NomorPO = dr.Item("Nomor_PO")
        Dim TanggalPO = dr.Item("Tanggal_PO")
        Dim TermOfPayment = dr.Item("Term_Of_Payment")
        Dim KeteranganToP = dr.Item("Keterangan_ToP")
        If AmbilAngka(TermOfPayment) > 0 Then
            TermOfPayment &= " Hari"
        Else
            TermOfPayment = Kosongan
        End If
        If TermOfPayment = Kosongan Then
            TermOfPayment = KeteranganToP
        Else
            If KeteranganToP <> Kosongan Then TermOfPayment = TermOfPayment & " / " & KeteranganToP
        End If
        Dim KodeProject = dr.Item("Kode_Project_Produk")
        Dim JumlahHargaKeseluruhan = dr.Item("Jumlah_Harga_Keseluruhan")
        Dim Diskon_Persen = dr.Item("Diskon")
        Dim Diskon_Rupiah As Int64 = JumlahHargaKeseluruhan * (Diskon_Persen / 100)
        Dim DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
        Dim PPN = dr.Item("PPN")
        Dim JenisPPh = dr.Item("Jenis_PPh")
        Dim TarifPPh As String = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh")) & " %"
        Dim PPhTerutang = dr.Item("PPh_Terutang")
        Dim PPhDitanggung = dr.Item("PPh_Ditanggung")
        Dim PPhDipotong = dr.Item("PPh_Dipotong")
        Dim TotalTagihan = dr.Item("Total_Tagihan")
        Dim TotalTagihan_Terbilang = AngkaTerbilangRupiah(TotalTagihan)
        Dim Catatan = Microsoft.VisualBasic.Replace(dr.Item("Catatan"), Chr(10), "<br/>")  'Chr(10) mewakili karakter 'Enter' dalam penyimpanan MySQL. Bukan Chr(13) ya..!!!
        Dim PembuatPO = dr.Item("Pembuat_PO")
        AksesDatabase_Transaksi(Tutup)

        Isi_VariabelPerusahaan()

        KontenFile_VariabelAwal =
            "<?php " & Enter1Baris &
            Enter1Baris &
            Konten_VariabelPerusahaan & Enter1Baris &
            Enter1Baris &
            "// Variabel PO :" & Enter1Baris &
            "$AngkaNota = '" & AngkaPOPembelian_Cetak & "';" & Enter1Baris &
            "$NomorPO = '" & NomorPO & "';" & Enter1Baris &
            "$TanggalPO = '" & TanggalPO & "';" & Enter1Baris &
            "$TermOfPayment = '" & TermOfPayment & "';" & Enter1Baris &
            "$KodeProject = '" & KodeProject & "';" & Enter1Baris &
            "$NamaSupplier = '" & NamaSupplier & "';" & Enter1Baris &
            "$AlamatSupplier = '" & AlamatSupplier & "';" & Enter1Baris &
            "$Attention = '" & Attention & "';" & Enter1Baris &
            "$JumlahHargaKeseluruhan = '" & JumlahHargaKeseluruhan & "';" & Enter1Baris &
            "$DiskonPersen = '" & Microsoft.VisualBasic.Replace(Diskon_Persen, ",", ".") & "';" & Enter1Baris &
            "$DiskonRupiah = '" & Diskon_Rupiah & "';" & Enter1Baris &
            "$DasarPengenaanPajak = '" & DasarPengenaanPajak & "';" & Enter1Baris &
            "$PPN = '" & PPN & "';" & Enter1Baris &
            "$JenisPPh_NonPPh = '" & JenisPPh_NonPPh & "';" & Enter1Baris &
            "$JenisPPh = '" & JenisPPh & "';" & Enter1Baris &
            "$TarifPPh = '" & TarifPPh & "';" & Enter1Baris &
            "$PPhTerutang = '" & PPhTerutang & "';" & Enter1Baris &
            "$PPhDitanggung = '" & PPhDitanggung & "';" & Enter1Baris &
            "$PPhDipotong = '" & PPhDipotong & "';" & Enter1Baris &
            "$TotalTagihan = '" & TotalTagihan & "';" & Enter1Baris &
            "$TotalTagihan_Terbilang = '" & TotalTagihan_Terbilang & "';" & Enter1Baris &
            "$Catatan = '" & Catatan & "';" & Enter1Baris &
            "$PembuatPO = '" & PembuatPO & "';" & Enter1Baris &
            "" & Enter1Baris &
            "$TabelNota = 'tbl_Pembelian_PO';" & Enter1Baris &
            "$KolomIdentifikasi = 'Angka_PO';" & Enter1Baris &
            "" & Enter1Baris &
            "?>"

        Try
            My.Computer.FileSystem.WriteAllText(LokasiFolderXAMPP & "htdocs\rekend\sat\cetak\php\" & NamaFile_VariabelAwal, KontenFile_VariabelAwal, False)
        Catch ex As Exception
            pesan_AdaKesalahanTeknis_SilakanUlangiBeberapaSaat()
            Return
        End Try

        KoneksiDatabaseWeb()

        frm_Cetak.JudulForm = "Pratinjau Cetak PO"
        frm_Cetak.HalamanTarget = "http://127.0.0.1:88/rekend/sat/cetak/form/po"
        frm_Cetak.ShowDialog()

    End Sub



    Public AngkaReturPembelian_Cetak
    Public Sub TampilkanHalamanCetak_ReturPembelian()

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Pembelian_Retur WHERE Angka_Retur = '" & AngkaReturPembelian_Cetak & "' ", KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        Dim KodeSupplier = dr.Item("Kode_Supplier")
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeSupplier & "'", KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        drTELUSUR.Read()
        Dim NamaSupplier = drTELUSUR.Item("Nama_Mitra")
        Dim AlamatSupplier = drTELUSUR.Item("Alamat")
        AksesDatabase_General(Tutup)
        Dim NomorRetur = dr.Item("Nomor_Retur")
        Dim TanggalRetur = dr.Item("Tanggal_Retur")
        Dim NomorInvoice = dr.Item("Nomor_Invoice")
        Dim TanggalInvoice = dr.Item("Tanggal_Invoice")
        Dim KodeProject = dr.Item("Kode_Project_Produk")
        Dim JumlahHargaKeseluruhan = dr.Item("Jumlah_Harga_Keseluruhan")
        Dim Diskon_Persen = dr.Item("Diskon")
        Dim Diskon_Rupiah As Int64 = JumlahHargaKeseluruhan * (Diskon_Persen / 100)
        Dim DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
        Dim PPN = dr.Item("PPN")
        Dim TotalTagihan = dr.Item("Total_Tagihan")
        Dim TotalTagihan_Terbilang = AngkaTerbilangRupiah(TotalTagihan)
        Dim Catatan = Microsoft.VisualBasic.Replace(dr.Item("Catatan"), Chr(10), "<br/>")  'Chr(10) mewakili karakter 'Enter' dalam penyimpanan MySQL. Bukan Chr(13) ya..!!!
        AksesDatabase_Transaksi(Tutup)

        Isi_VariabelPerusahaan()

        KontenFile_VariabelAwal =
            "<?php " & Enter1Baris &
            Enter1Baris &
            Konten_VariabelPerusahaan & Enter1Baris &
            Enter1Baris &
            "// Variabel Retur :" & Enter1Baris &
            "$AngkaNota = '" & AngkaReturPembelian_Cetak & "';" & Enter1Baris &
            "$NomorRetur = '" & NomorRetur & "';" & Enter1Baris &
            "$TanggalRetur = '" & TanggalRetur & "';" & Enter1Baris &
            "$NomorInvoice = '" & NomorInvoice & "';" & Enter1Baris &
            "$TanggalInvoice = '" & TanggalInvoice & "';" & Enter1Baris &
            "$KodeProject = '" & KodeProject & "';" & Enter1Baris &
            "$NamaSupplier = '" & NamaSupplier & "';" & Enter1Baris &
            "$AlamatSupplier = '" & AlamatSupplier & "';" & Enter1Baris &
            "$JumlahHargaKeseluruhan = '" & JumlahHargaKeseluruhan & "';" & Enter1Baris &
            "$DiskonPersen = '" & Microsoft.VisualBasic.Replace(Diskon_Persen, ",", ".") & "';" & Enter1Baris &
            "$DiskonRupiah = '" & Diskon_Rupiah & "';" & Enter1Baris &
            "$DasarPengenaanPajak = '" & DasarPengenaanPajak & "';" & Enter1Baris &
            "$PPN = '" & PPN & "';" & Enter1Baris &
            "$TotalTagihan = '" & TotalTagihan & "';" & Enter1Baris &
            "$TotalTagihan_Terbilang = '" & TotalTagihan_Terbilang & "';" & Enter1Baris &
            "$Catatan = '" & Catatan & "';" & Enter1Baris &
            "" & Enter1Baris &
            "$TabelNota = 'tbl_Pembelian_Retur';" & Enter1Baris &
            "$KolomIdentifikasi = 'Angka_Retur';" & Enter1Baris &
            "" & Enter1Baris &
            "?>"

        Try
            My.Computer.FileSystem.WriteAllText(LokasiFolderXAMPP & "htdocs\rekend\sat\cetak\php\" & NamaFile_VariabelAwal, KontenFile_VariabelAwal, False)
        Catch ex As Exception
            pesan_AdaKesalahanTeknis_SilakanUlangiBeberapaSaat()
            Return
        End Try

        KoneksiDatabaseWeb()

        frm_Cetak.JudulForm = "Pratinjau Cetak Nota Retur Pembelian"
        frm_Cetak.HalamanTarget = "http://127.0.0.1:88/rekend/sat/cetak/form/notaretur"
        frm_Cetak.ShowDialog()

    End Sub

    Public NomorInvoicePenjualan_Cetak
    Public Sub TampilkanHalamanCetak_InvoicePenjualan()

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                   " WHERE Nomor_Invoice = '" & NomorInvoicePenjualan_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        If Not dr.HasRows Then
            PesanUntukProgrammer("Invoice " & NomorInvoicePenjualan_Cetak & " tidak terdaftar...!")
            Return
        End If
        Dim KodeCustomer = dr.Item("Kode_Customer")
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                     " WHERE Kode_Mitra = '" & KodeCustomer & "'",
                                     KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        drTELUSUR.Read()
        Dim NamaCustomer = drTELUSUR.Item("Nama_Mitra")
        Dim AlamatCustomer = drTELUSUR.Item("Alamat")
        AksesDatabase_General(Tutup)
        Dim NomorInvoice = dr.Item("Nomor_Invoice")
        Dim TanggalInvoice = dr.Item("Tanggal_Invoice")
        Dim TanggalJatuhTempo = dr.Item("Tanggal_Jatuh_Tempo")
        Dim TanggalSJBAST = dr.Item("Tanggal_Diterima_SJ_BAST_Produk")
        Dim NomorSJBAST = Kosongan
        Dim NomorSJBAST_Satuan = Kosongan
        Dim NomorSJBAST_Sebelumnya = Kosongan
        Dim NomorPO = Kosongan
        Dim KodeProject = Kosongan
        Dim KodeProject_Satuan = Kosongan
        Dim KodeProject_Sebelumnya = Kosongan
        cmdTELUSUR = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_Invoice " &
                                          " WHERE Nomor_Invoice = '" & NomorInvoicePenjualan_Cetak & "' ",
        KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read
            NomorSJBAST_Satuan = drTELUSUR.Item("Nomor_SJ_BAST_Produk")
            If NomorSJBAST_Satuan <> NomorSJBAST_Sebelumnya Then
                If NomorSJBAST = Kosongan Then
                    NomorSJBAST = NomorSJBAST_Satuan
                Else
                    NomorSJBAST &= "<br/>" & NomorSJBAST_Satuan
                End If
                If Microsoft.VisualBasic.Left(NomorSJBAST_Satuan, 2) = "SJ" Then
                    QueryTelusur2 = " SELECT * FROM tbl_Penjualan_SJ   WHERE Nomor_SJ   = '" & NomorSJBAST_Satuan & "' "
                Else
                    QueryTelusur2 = " SELECT * FROM tbl_Penjualan_BAST WHERE Nomor_BAST = '" & NomorSJBAST_Satuan & "' "
                End If
                cmdTELUSUR2 = New OdbcCommand(QueryTelusur2, KoneksiDatabaseTransaksi)
                drTELUSUR2_ExecuteReader()
                Do While drTELUSUR2.Read()
                    NomorPO = drTELUSUR2.Item("Nomor_PO_Produk")
                    cmdTELUSUR3 = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                                       " WHERE Nomor_PO = '" & NomorPO & "' ",
                                                       KoneksiDatabaseTransaksi)
                    drTELUSUR3_ExecuteReader()
                    drTELUSUR3.Read()
                    KodeProject_Satuan = drTELUSUR3.Item("Kode_Project_Produk")
                    If KodeProject_Satuan <> KodeProject_Sebelumnya Then
                        If KodeProject = Kosongan Then
                            KodeProject = KodeProject_Satuan
                        Else
                            KodeProject &= "<br/>" & KodeProject_Satuan
                        End If
                    End If
                    KodeProject_Sebelumnya = KodeProject_Satuan
                Loop
            End If
            NomorSJBAST_Sebelumnya = NomorSJBAST_Satuan
        Loop
        Dim JumlahHargaKeseluruhan = dr.Item("Jumlah_Harga_Keseluruhan")
        Dim Diskon_Rupiah As Int64 = dr.Item("Diskon")
        Dim DasarPengenaanPajak = dr.Item("Dasar_Pengenaan_Pajak")
        Dim PPN = dr.Item("PPN")
        Dim JenisPPh = dr.Item("Jenis_PPh")
        Dim TarifPPh As String = FormatUlangDesimal_Prosentase(dr.Item("Tarif_PPh")) & " %"
        Dim PPhTerutang = dr.Item("PPh_Terutang")
        Dim PPhDitanggung = dr.Item("PPh_Ditanggung")
        Dim PPhDipotong = dr.Item("PPh_Dipotong")
        Dim TotalTagihan = dr.Item("Total_Tagihan")
        Dim TotalTagihan_Terbilang = AngkaTerbilangRupiah(TotalTagihan)
        Dim Catatan = Microsoft.VisualBasic.Replace(dr.Item("Catatan"), Chr(10), "<br/>")  'Chr(10) mewakili karakter 'Enter' dalam penyimpanan MySQL. Bukan Chr(13) ya..!!!
        AksesDatabase_Transaksi(Tutup)

        Isi_VariabelPerusahaan()

        KontenFile_VariabelAwal =
            "<?php " & Enter1Baris &
            Enter1Baris &
            Konten_VariabelPerusahaan & Enter1Baris &
            Enter1Baris &
            "// Variabel Invoice :" & Enter1Baris &
            "$AngkaNota = '" & NomorInvoicePenjualan_Cetak & "';" & Enter1Baris &
            "$NomorInvoice = '" & NomorInvoice & "';" & Enter1Baris &
            "$TanggalInvoice = '" & TanggalInvoice & "';" & Enter1Baris &
            "$TanggalJatuhTempo = '" & TanggalJatuhTempo & "';" & Enter1Baris &
            "$NomorSJBAST = '" & NomorSJBAST & "';" & Enter1Baris &
            "$TanggalSJBAST = '" & TanggalSJBAST & "';" & Enter1Baris &
            "$KodeProject = '" & KodeProject & "';" & Enter1Baris &
            "$NamaCustomer = '" & NamaCustomer & "';" & Enter1Baris &
            "$AlamatCustomer = '" & AlamatCustomer & "';" & Enter1Baris &
            "$JumlahHargaKeseluruhan = '" & JumlahHargaKeseluruhan & "';" & Enter1Baris &
            "$DiskonRupiah = '" & Diskon_Rupiah & "';" & Enter1Baris &
            "$DasarPengenaanPajak = '" & DasarPengenaanPajak & "';" & Enter1Baris &
            "$PPN = '" & PPN & "';" & Enter1Baris &
            "$JenisPPh_NonPPh = '" & JenisPPh_NonPPh & "';" & Enter1Baris &
            "$JenisPPh = '" & JenisPPh & "';" & Enter1Baris &
            "$TarifPPh = '" & TarifPPh & "';" & Enter1Baris &
            "$PPhTerutang = '" & PPhTerutang & "';" & Enter1Baris &
            "$PPhDitanggung = '" & PPhDitanggung & "';" & Enter1Baris &
            "$PPhDipotong = '" & PPhDipotong & "';" & Enter1Baris &
            "$TotalTagihan = '" & TotalTagihan & "';" & Enter1Baris &
            "$TotalTagihan_Terbilang = '" & TotalTagihan_Terbilang & "';" & Enter1Baris &
            "$Catatan = '" & Catatan & "';" & Enter1Baris &
            "" & Enter1Baris &
            "$TabelNota = 'tbl_Penjualan_Invoice';" & Enter1Baris &
            "$KolomIdentifikasi = 'Angka_Invoice';" & Enter1Baris &
            "" & Enter1Baris &
            "?>"

        Try
            My.Computer.FileSystem.WriteAllText(LokasiFolderXAMPP & "htdocs\rekend\sat\cetak\php\" & NamaFile_VariabelAwal, KontenFile_VariabelAwal, False)
        Catch ex As Exception
            pesan_AdaKesalahanTeknis_SilakanUlangiBeberapaSaat()
            Return
        End Try

        KoneksiDatabaseWeb()

        frm_Cetak.JudulForm = "Pratinjau Cetak Invoice"
        frm_Cetak.HalamanTarget = "http://127.0.0.1:88/rekend/sat/cetak/form/invoice"
        frm_Cetak.ShowDialog()

    End Sub


    Public AngkaSJ_Cetak
    Public Sub TampilkanHalamanCetak_SuratJalan()

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_SJ " &
                                   " WHERE Angka_SJ = '" & AngkaSJ_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        Dim KodeCustomer = dr.Item("Kode_Customer")
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                     " WHERE Kode_Mitra = '" & KodeCustomer & "'",
                                     KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        drTELUSUR.Read()
        Dim NamaCustomer = drTELUSUR.Item("Nama_Mitra")
        Dim AlamatCustomer = drTELUSUR.Item("Alamat")
        AksesDatabase_General(Tutup)
        Dim NomorSJBAST = dr.Item("Nomor_SJ")
        Dim TanggalSJBAST = dr.Item("Tanggal_SJ")
        Dim NomorPO = Kosongan
        Dim NomorPO_Satuan = Kosongan
        Dim NomorPO_Sebelumnya = Kosongan
        Dim TanggalPO = Kosongan
        Dim KodeProject = Kosongan
        Dim PlatNomor = dr.Item("Plat_Nomor")
        Dim NamaSupir = dr.Item("Nama_Supir")
        Dim NamaPengirim = dr.Item("Nama_Pengirim")
        If NamaPengirim = Kosongan Then NamaPengirim = NamaSupir
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_SJ " &
                                     " WHERE Nomor_SJ = '" & NomorSJBAST & "' ",
                                     KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read()
            NomorPO_Satuan = drTELUSUR.Item("Nomor_PO_Produk")
            cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                          " WHERE Nomor_PO = '" & NomorPO_Satuan & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR2_ExecuteReader()
            drTELUSUR2.Read()
            If NomorPO_Satuan <> NomorPO_Sebelumnya Then
                If NomorPO = Kosongan Then
                    NomorPO = NomorPO_Satuan
                    TanggalPO = drTELUSUR.Item("Tanggal_PO_Produk")
                    KodeProject = drTELUSUR2.Item("Kode_Project_Produk")
                Else
                    NomorPO &= "<br/>" & NomorPO_Satuan
                    TanggalPO &= "<br/>" & drTELUSUR.Item("Tanggal_PO_Produk")
                    KodeProject &= "<br/>" & drTELUSUR2.Item("Kode_Project_Produk")
                End If
            End If
            NomorPO_Sebelumnya = NomorPO_Satuan
        Loop
        Dim Catatan = Microsoft.VisualBasic.Replace(dr.Item("Catatan"), Chr(10), "<br/>")  'Chr(10) mewakili karakter 'Enter' dalam penyimpanan MySQL. Bukan Chr(13) ya..!!!
        AksesDatabase_Transaksi(Tutup)

        Isi_VariabelPerusahaan()

        KontenFile_VariabelAwal =
            "<?php " & Enter1Baris &
            Enter1Baris &
            Konten_VariabelPerusahaan & Enter1Baris &
            Enter1Baris &
            "// Variabel Invoice :" & Enter1Baris &
            "$AngkaNota = '" & AngkaSJ_Cetak & "';" & Enter1Baris &
            "$NomorSJBAST = '" & NomorSJBAST & "';" & Enter1Baris &
            "$TanggalSJBAST = '" & TanggalSJBAST & "';" & Enter1Baris &
            "$NomorPO = '" & NomorPO & "';" & Enter1Baris &
            "$TanggalPO = '" & TanggalPO & "';" & Enter1Baris &
            "$KodeProject = '" & KodeProject & "';" & Enter1Baris &
            "$PlatNomor = '" & PlatNomor & "';" & Enter1Baris &
            "$NamaSupir = '" & NamaSupir & "';" & Enter1Baris &
            "$NamaPengirim = '" & NamaPengirim & "';" & Enter1Baris &
            "$NamaCustomer = '" & NamaCustomer & "';" & Enter1Baris &
            "$AlamatCustomer = '" & AlamatCustomer & "';" & Enter1Baris &
            "$Catatan = '" & Catatan & "';" & Enter1Baris &
            "" & Enter1Baris &
            "$TabelNota = 'tbl_Penjualan_SJ';" & Enter1Baris &
            "$KolomIdentifikasi = 'Angka_SJ';" & Enter1Baris &
            "" & Enter1Baris &
            "?>"

        Try
            My.Computer.FileSystem.WriteAllText(LokasiFolderXAMPP & "htdocs\rekend\sat\cetak\php\" & NamaFile_VariabelAwal, KontenFile_VariabelAwal, False)
        Catch ex As Exception
            pesan_AdaKesalahanTeknis_SilakanUlangiBeberapaSaat()
            Return
        End Try

        KoneksiDatabaseWeb()

        frm_Cetak.JudulForm = "Pratinjau Cetak Surat Jalan"
        frm_Cetak.HalamanTarget = "http://127.0.0.1:88/rekend/sat/cetak/form/suratjalan"
        frm_Cetak.ShowDialog()

    End Sub


    Public AngkaBAST_Cetak
    Public Sub TampilkanHalamanCetak_BAST()

        AksesDatabase_Transaksi(Buka)
        cmd = New Odbc.OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                   " WHERE Angka_BAST = '" & AngkaBAST_Cetak & "' ",
                                   KoneksiDatabaseTransaksi)
        dr_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        dr.Read()
        Dim KodeCustomer = dr.Item("Kode_Customer")
        AksesDatabase_General(Buka)
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi " &
                                     " WHERE Kode_Mitra = '" & KodeCustomer & "'",
                                     KoneksiDatabaseGeneral)
        drTELUSUR_ExecuteReader()
        If StatusKoneksiDatabase = False Then Return
        drTELUSUR.Read()
        Dim NamaCustomer = drTELUSUR.Item("Nama_Mitra")
        Dim AlamatCustomer = drTELUSUR.Item("Alamat")
        AksesDatabase_General(Tutup)
        Dim NomorSJBAST = dr.Item("Nomor_BAST")
        Dim TanggalSJBAST = dr.Item("Tanggal_BAST")
        Dim NomorPO = Kosongan
        Dim NomorPO_Satuan = Kosongan
        Dim NomorPO_Sebelumnya = Kosongan
        Dim TanggalPO = Kosongan
        Dim KodeProject = Kosongan
        Dim YangMenyerahkan = dr.Item("Yang_Menyerahkan")
        cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Penjualan_BAST " &
                                     " WHERE Nomor_BAST = '" & NomorSJBAST & "' ",
                                     KoneksiDatabaseTransaksi)
        drTELUSUR_ExecuteReader()
        Do While drTELUSUR.Read()
            NomorPO_Satuan = drTELUSUR.Item("Nomor_PO_Produk")
            cmdTELUSUR2 = New OdbcCommand(" SELECT * FROM tbl_Penjualan_PO " &
                                          " WHERE Nomor_PO = '" & NomorPO_Satuan & "' ", KoneksiDatabaseTransaksi)
            drTELUSUR2_ExecuteReader()
            drTELUSUR2.Read()
            If NomorPO_Satuan <> NomorPO_Sebelumnya Then
                If NomorPO = Kosongan Then
                    NomorPO = NomorPO_Satuan
                    TanggalPO = drTELUSUR.Item("Tanggal_PO_Produk")
                    KodeProject = drTELUSUR2.Item("Kode_Project_Produk")
                Else
                    NomorPO &= "<br/>" & NomorPO_Satuan
                    TanggalPO &= "<br/>" & drTELUSUR.Item("Tanggal_PO_Produk")
                    KodeProject &= "<br/>" & drTELUSUR2.Item("Kode_Project_Produk")
                End If
            End If
            NomorPO_Sebelumnya = NomorPO_Satuan
        Loop
        Dim Catatan = Microsoft.VisualBasic.Replace(dr.Item("Catatan"), Chr(10), "<br/>")  'Chr(10) mewakili karakter 'Enter' dalam penyimpanan MySQL. Bukan Chr(13) ya..!!!
        AksesDatabase_Transaksi(Tutup)

        Isi_VariabelPerusahaan()

        KontenFile_VariabelAwal =
            "<?php " & Enter1Baris &
            Enter1Baris &
            Konten_VariabelPerusahaan & Enter1Baris &
            Enter1Baris &
            "// Variabel Invoice :" & Enter1Baris &
            "$AngkaNota = '" & AngkaBAST_Cetak & "';" & Enter1Baris &
            "$NomorSJBAST = '" & NomorSJBAST & "';" & Enter1Baris &
            "$TanggalSJBAST = '" & TanggalSJBAST & "';" & Enter1Baris &
            "$NomorPO = '" & NomorPO & "';" & Enter1Baris &
            "$TanggalPO = '" & TanggalPO & "';" & Enter1Baris &
            "$KodeProject = '" & KodeProject & "';" & Enter1Baris &
            "$YangMenyerahkan = '" & YangMenyerahkan & "';" & Enter1Baris &
            "$NamaCustomer = '" & NamaCustomer & "';" & Enter1Baris &
            "$AlamatCustomer = '" & AlamatCustomer & "';" & Enter1Baris &
            "$Catatan = '" & Catatan & "';" & Enter1Baris &
            "" & Enter1Baris &
            "$TabelNota = 'tbl_Penjualan_BAST';" & Enter1Baris &
            "$KolomIdentifikasi = 'Angka_BAST';" & Enter1Baris &
            "" & Enter1Baris &
            "?>"

        Try
            My.Computer.FileSystem.WriteAllText(LokasiFolderXAMPP & "htdocs\rekend\sat\cetak\php\" & NamaFile_VariabelAwal, KontenFile_VariabelAwal, False)
        Catch ex As Exception
            pesan_AdaKesalahanTeknis_SilakanUlangiBeberapaSaat()
            Return
        End Try

        KoneksiDatabaseWeb()

        frm_Cetak.JudulForm = "Pratinjau Cetak BAST"
        frm_Cetak.HalamanTarget = "http://127.0.0.1:88/rekend/sat/cetak/form/bast"
        frm_Cetak.ShowDialog()

    End Sub

    Public NomorPatokan_Cetak
    Sub Cetak(JenisFormCetak As String, NomorPatokan As String, TampilkanHeader As Boolean, TampilkanFooter As Boolean)
        NomorPatokan_Cetak = NomorPatokan
        win_Pratinjau = New wpfWin_Pratinjau
        win_Pratinjau.ResetForm()
        win_Pratinjau.TampilkanHeader = TampilkanHeader
        win_Pratinjau.TampilkanFooter = TampilkanFooter
        win_Pratinjau.JenisFormCetak = JenisFormCetak
        win_Pratinjau.JudulForm = "Pratinjau " & JenisFormCetak & " - " & NomorPatokan
        win_Pratinjau.ShowDialog()
    End Sub

End Module
