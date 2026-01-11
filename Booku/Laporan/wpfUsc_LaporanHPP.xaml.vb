Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_LaporanHPP

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public JudulForm

    Dim QueryTampilan

    Dim COA
    Dim NamaAkun
    Dim DebetKreditCOA
    Dim SaldoJanuari As Int64
    Dim SaldoFebruari As Int64
    Dim SaldoMaret As Int64
    Dim SaldoApril As Int64
    Dim SaldoMei As Int64
    Dim SaldoJuni As Int64
    Dim SaldoJuli As Int64
    Dim SaldoAgustus As Int64
    Dim SaldoSeptember As Int64
    Dim SaldoOktober As Int64
    Dim SaldoNopember As Int64
    Dim SaldoDesember As Int64
    Dim SaldoKeseluruhan As Int64

    Dim SaldoKhususJanuari As Int64
    Dim SaldoKhususFebruari As Int64
    Dim SaldoKhususMaret As Int64
    Dim SaldoKhususApril As Int64
    Dim SaldoKhususMei As Int64
    Dim SaldoKhususJuni As Int64
    Dim SaldoKhususJuli As Int64
    Dim SaldoKhususAgustus As Int64
    Dim SaldoKhususSeptember As Int64
    Dim SaldoKhususOktober As Int64
    Dim SaldoKhususNopember As Int64
    Dim SaldoKhususDesember As Int64
    Dim SaldoKhususKeseluruhan As Int64

    Dim COA_Terseleksi

    Dim JenisSaldo As String
    Dim JenisSaldo_Awal As String = "Awal"
    Dim JenisSaldo_Akhir As String = "Akhir"

    Dim Operasi As String
    Dim Operasi_Penambah As String = "Penambah"
    Dim Operasi_Pengurang As String = "Pengurang"
    Dim Operasi_Netral As String = "Netral"

    Dim Titel As String
    Dim Titel_NamaAkun As String = "Nama Akun"


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_LaporanHPP.JudulForm


        ProsesLoadingForm = False

        RefreshTampilanData()

        SudahDimuat = True
    End Sub



    Sub RefreshTampilanData()

        If StatusTrialBalance = True Then
            TampilkanData()
        Else
            MsgBox("Telah terjadi perubahan data pada Jurnal." &
            Enter2Baris & "Silakan klik tombol 'Trial Balance' terlebih dahulu.")
            Return
        End If

    End Sub


    Sub ResetSaldoKhusus()
        SaldoKhususJanuari = 0
        SaldoKhususFebruari = 0
        SaldoKhususMaret = 0
        SaldoKhususApril = 0
        SaldoKhususMei = 0
        SaldoKhususJuni = 0
        SaldoKhususJuli = 0
        SaldoKhususAgustus = 0
        SaldoKhususSeptember = 0
        SaldoKhususOktober = 0
        SaldoKhususNopember = 0
        SaldoKhususDesember = 0
        SaldoKhususKeseluruhan = 0
    End Sub

    Sub TampilkanData()

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        Dim QueryTampilanUmum = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' "

        '-----------------------------------------------------------
        ' BIAYA BAHAN BAKU :
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("Biaya Bahan Baku :")

        'LOKAL :
        ResetSaldoKhusus()

        'Persediaan Bahan Baku Lokal - Awal :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBahanBaku_Lokal & "' "
        DataPerKategoriCOA("Persediaan Bahan Baku Lokal - Awal", JenisSaldo_Awal, Operasi_Penambah)

        'Pembelian Bahan Baku - Lokal :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PembelianBahanBaku_Lokal & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        'Retur Pembelian Bahan Baku - Lokal :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_ReturPembelianBahanBaku_Lokal & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Pengurang)

        'Bahan Baku Lokal Siap Dipakai :
        datatabelUtama.Rows.Add("Bahan Baku Lokal Siap Dipakai", Kosongan,
                    SaldoKhususJanuari,
                    SaldoKhususFebruari,
                    SaldoKhususMaret,
                    SaldoKhususApril,
                    SaldoKhususMei,
                    SaldoKhususJuni,
                    SaldoKhususJuli,
                    SaldoKhususAgustus,
                    SaldoKhususSeptember,
                    SaldoKhususOktober,
                    SaldoKhususNopember,
                    SaldoKhususDesember,
                    SaldoKhususKeseluruhan)
        Terabas()

        'Persediaan Bahan Baku Lokal - Akhir :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBahanBaku_Lokal & "' "
        DataPerKategoriCOA("Persediaan Bahan Baku Lokal - Akhir", JenisSaldo_Akhir, Operasi_Penambah)

        'Biaya Pemakaian Bahan Baku - Lokal :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Netral)

        'Biaya Terkait Bahan Baku Lokal :
        ResetSaldoKhusus()
        QueryTampilan = QueryTampilanUmum &
            " AND COA LIKE '511%' " &
            " AND COA <> '" & KodeTautanCOA_PersediaanBahanBaku_Lokal & "' " &
            " AND COA <> '" & KodeTautanCOA_PembelianBahanBaku_Lokal & "' " &
            " AND COA <> '" & KodeTautanCOA_ReturPembelianBahanBaku_Lokal & "' " &
            " AND COA <> '" & KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal  & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        'Biaya Bahan Baku - Lokal :
        datatabelUtama.Rows.Add("Biaya Bahan Baku - Lokal", Kosongan,
                    SaldoKhususJanuari,
                    SaldoKhususFebruari,
                    SaldoKhususMaret,
                    SaldoKhususApril,
                    SaldoKhususMei,
                    SaldoKhususJuni,
                    SaldoKhususJuli,
                    SaldoKhususAgustus,
                    SaldoKhususSeptember,
                    SaldoKhususOktober,
                    SaldoKhususNopember,
                    SaldoKhususDesember,
                    SaldoKhususKeseluruhan)
        Terabas()

        'IMPORT :
        ResetSaldoKhusus()

        'Persediaan Bahan Baku Import - Awal :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBahanBaku_Import & "' "
        DataPerKategoriCOA("Persediaan Bahan Baku Impor - Awal", JenisSaldo_Awal, Operasi_Penambah)

        'Pembelian Bahan Baku - Import :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PembelianBahanBaku_Import & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        'Retur Pembelian Bahan Baku - Import :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_ReturPembelianBahanBaku_Import & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Pengurang)

        'Bahan Baku Import Siap Dipakai :
        datatabelUtama.Rows.Add("Bahan Baku Import Siap Dipakai", Kosongan,
                    SaldoKhususJanuari,
                    SaldoKhususFebruari,
                    SaldoKhususMaret,
                    SaldoKhususApril,
                    SaldoKhususMei,
                    SaldoKhususJuni,
                    SaldoKhususJuli,
                    SaldoKhususAgustus,
                    SaldoKhususSeptember,
                    SaldoKhususOktober,
                    SaldoKhususNopember,
                    SaldoKhususDesember,
                    SaldoKhususKeseluruhan)
        Terabas()

        'Persediaan Bahan Baku Import - Akhir :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBahanBaku_Import & "' "
        DataPerKategoriCOA("Persediaan Bahan Baku Impor - Akhir", JenisSaldo_Akhir, Operasi_Penambah)

        'Biaya Pemakaian Bahan Baku - Import :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_BiayaPemakaianBahanBaku_Import & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Netral)

        'Biaya Terkait Bahan Baku Import :
        ResetSaldoKhusus()
        QueryTampilan = QueryTampilanUmum &
            " AND COA LIKE '512%' " &
            " AND COA <> '" & KodeTautanCOA_PersediaanBahanBaku_Import & "' " &
            " AND COA <> '" & KodeTautanCOA_PembelianBahanBaku_Import & "' " &
            " AND COA <> '" & KodeTautanCOA_ReturPembelianBahanBaku_Import & "' " &
            " AND COA <> '" & KodeTautanCOA_BiayaPemakaianBahanBaku_Import & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        'Biaya Bahan Baku - Import :
        datatabelUtama.Rows.Add("Biaya Bahan Baku - Import", Kosongan,
                    SaldoKhususJanuari,
                    SaldoKhususFebruari,
                    SaldoKhususMaret,
                    SaldoKhususApril,
                    SaldoKhususMei,
                    SaldoKhususJuni,
                    SaldoKhususJuli,
                    SaldoKhususAgustus,
                    SaldoKhususSeptember,
                    SaldoKhususOktober,
                    SaldoKhususNopember,
                    SaldoKhususDesember,
                    SaldoKhususKeseluruhan)
        Terabas()

        'Biaya Bahan Baku : 
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_BiayaBahanBaku & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        '-----------------------------------------------------------
        ' BIAYA TENAGA KERJA LANGSUNG :
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("Biaya Tenaga Kerja Langsung :")

        QueryTampilan = QueryTampilanUmum &
            " AND COA LIKE '52%' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        '-----------------------------------------------------------
        ' BIAYA OVER HEAD PABRIK :
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("Biaya Overhead Pabrik :")

        ResetSaldoKhusus()

        'Persediaan Bahan Penolong - Awal :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBahanPenolong & "' "
        DataPerKategoriCOA("Persediaan Bahan Penolong - Awal", JenisSaldo_Awal, Operasi_Penambah)

        'Pembelian Bahan Penolong :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PembelianBahanPenolong & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        'Retur Pembelian Bahan Penolong :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_ReturPembelianBahanPenolong & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Pengurang)

        'Bahan Penolong Siap Dipakai :
        datatabelUtama.Rows.Add("Bahan Penolong Siap Dipakai", Kosongan,
                    SaldoKhususJanuari,
                    SaldoKhususFebruari,
                    SaldoKhususMaret,
                    SaldoKhususApril,
                    SaldoKhususMei,
                    SaldoKhususJuni,
                    SaldoKhususJuli,
                    SaldoKhususAgustus,
                    SaldoKhususSeptember,
                    SaldoKhususOktober,
                    SaldoKhususNopember,
                    SaldoKhususDesember,
                    SaldoKhususKeseluruhan)
        Terabas()

        'Persediaan Bahan Penolong - Akhir :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBahanPenolong & "' "
        DataPerKategoriCOA("Persediaan Bahan Penolong - Akhir", JenisSaldo_Akhir, Operasi_Penambah)

        'Biaya Pemakaian Bahan Penolong :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_BiayaPemakaianBahanPenolong & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Netral)

        'Biaya Terkait Bahan Penolong :
        QueryTampilan = QueryTampilanUmum &
            " AND COA LIKE '53%' " &
            " AND COA <> '" & KodeTautanCOA_PersediaanBahanPenolong & "' " &
            " AND COA <> '" & KodeTautanCOA_PembelianBahanPenolong & "' " &
            " AND COA <> '" & KodeTautanCOA_ReturPembelianBahanPenolong & "' " &
            " AND COA <> '" & KodeTautanCOA_BiayaPemakaianBahanPenolong & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        'Biaya Produksi :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_BiayaProduksi & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        'Persediaan Barang DalamProses - Awal :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBarangDalamProses & "' "
        DataPerKategoriCOA("Persediaan Barang DalamP roses - Awal", JenisSaldo_Awal, Operasi_Penambah)

        'Persediaan Barang DalamProses - Akhir :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBarangDalamProses & "' "
        DataPerKategoriCOA("Persediaan Barang Dalam Proses - Akhir", JenisSaldo_Akhir, Operasi_Penambah)

        'Harga Pokok Produksi :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_HargaPokokProduksi & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        'Persediaan Barang Jadi - Awal :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBarangJadi & "' "
        DataPerKategoriCOA("Persediaan Barang Jadi - Awal", JenisSaldo_Awal, Operasi_Penambah)

        'Persediaan Barang Jadi - Akhir :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_PersediaanBarangJadi & "' "
        DataPerKategoriCOA("Persediaan Barang Jadi - Akhir", JenisSaldo_Akhir, Operasi_Penambah)

        'Harga Pokok Penjualan :
        QueryTampilan = QueryTampilanUmum &
            " AND COA = '" & KodeTautanCOA_HargaPokokPenjualan & "' "
        DataPerKategoriCOA(Titel_NamaAkun, JenisSaldo_Akhir, Operasi_Penambah)

        BersihkanSeleksi()

    End Sub

    Sub DataPerKategoriCOA(Titel As String, JenisSaldo As String, Operasi As String)

        'Data Tabel :
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            COA = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            DebetKreditCOA = dr.Item("D_K")
            Select Case JenisSaldo
                Case JenisSaldo_Awal
                    SaldoJanuari = dr.Item("Saldo_Awal")
                    SaldoFebruari = dr.Item("Saldo_Januari")
                    SaldoMaret = dr.Item("Saldo_Februari")
                    SaldoApril = dr.Item("Saldo_Maret")
                    SaldoMei = dr.Item("Saldo_April")
                    SaldoJuni = dr.Item("Saldo_Mei")
                    SaldoJuli = dr.Item("Saldo_Juni")
                    SaldoAgustus = dr.Item("Saldo_Juli")
                    SaldoSeptember = dr.Item("Saldo_Agustus")
                    SaldoOktober = dr.Item("Saldo_September")
                    SaldoNopember = dr.Item("Saldo_Oktober")
                    SaldoDesember = dr.Item("Saldo_Nopember")
                Case JenisSaldo_Akhir
                    SaldoJanuari = dr.Item("Saldo_Januari")
                    SaldoFebruari = dr.Item("Saldo_Februari")
                    SaldoMaret = dr.Item("Saldo_Maret")
                    SaldoApril = dr.Item("Saldo_April")
                    SaldoMei = dr.Item("Saldo_Mei")
                    SaldoJuni = dr.Item("Saldo_Juni")
                    SaldoJuli = dr.Item("Saldo_Juli")
                    SaldoAgustus = dr.Item("Saldo_Agustus")
                    SaldoSeptember = dr.Item("Saldo_September")
                    SaldoOktober = dr.Item("Saldo_Oktober")
                    SaldoNopember = dr.Item("Saldo_Nopember")
                    SaldoDesember = dr.Item("Saldo_Desember")
            End Select
            If BulanBukuAktif = 1 Then SaldoJanuari = 0
            If BulanBukuAktif <= 2 Then SaldoFebruari = 0
            If BulanBukuAktif <= 3 Then SaldoMaret = 0
            If BulanBukuAktif <= 4 Then SaldoApril = 0
            If BulanBukuAktif <= 5 Then SaldoMei = 0
            If BulanBukuAktif <= 6 Then SaldoJuni = 0
            If BulanBukuAktif <= 7 Then SaldoJuli = 0
            If BulanBukuAktif <= 8 Then SaldoAgustus = 0
            If BulanBukuAktif <= 9 Then SaldoSeptember = 0
            If BulanBukuAktif <= 10 Then SaldoOktober = 0
            If BulanBukuAktif <= 11 Then SaldoNopember = 0
            If BulanBukuAktif <= 12 Then SaldoDesember = 0
            SaldoKeseluruhan _
                = SaldoJanuari + SaldoFebruari + SaldoMaret + SaldoApril + SaldoMei + SaldoJuni _
                + SaldoJuli + SaldoAgustus + SaldoSeptember + SaldoOktober + SaldoNopember + SaldoDesember
            If KepalaCOA(COA, 1) Then
                If Right(Titel, 4) = "Awal" Then
                    SaldoKeseluruhan = SaldoJanuari
                Else
                    Select Case BulanTerakhirDitutup
                        Case 0
                            SaldoKeseluruhan = 0
                        Case 1
                            SaldoKeseluruhan = SaldoJanuari
                        Case 2
                            SaldoKeseluruhan = SaldoFebruari
                        Case 3
                            SaldoKeseluruhan = SaldoMaret
                        Case 4
                            SaldoKeseluruhan = SaldoApril
                        Case 5
                            SaldoKeseluruhan = SaldoMei
                        Case 6
                            SaldoKeseluruhan = SaldoJuni
                        Case 7
                            SaldoKeseluruhan = SaldoJuli
                        Case 8
                            SaldoKeseluruhan = SaldoAgustus
                        Case 9
                            SaldoKeseluruhan = SaldoSeptember
                        Case 10
                            SaldoKeseluruhan = SaldoOktober
                        Case 11
                            SaldoKeseluruhan = SaldoNopember
                        Case 12
                            SaldoKeseluruhan = SaldoDesember
                    End Select
                End If
            End If
            If (Not SaldoKeseluruhan = 0) _
                Or (Not Titel = Titel_NamaAkun) _
                Or COA = KodeTautanCOA_PembelianBahanBaku_Lokal _
                Or COA = KodeTautanCOA_PembelianBahanBaku_Import _
                Or COA = KodeTautanCOA_BiayaPemakaianBahanBaku_Lokal _
                Or COA = KodeTautanCOA_BiayaPemakaianBahanBaku_Import _
                Or COA = KodeTautanCOA_PembelianBahanPenolong _
                Or COA = KodeTautanCOA_BiayaPemakaianBahanPenolong _
                Or COA = KodeTautanCOA_BiayaOverheadPabrik _
                Or COA = KodeTautanCOA_BiayaProduksi _
                Or COA = KodeTautanCOA_HargaPokokProduksi _
                Or COA = KodeTautanCOA_HargaPokokPenjualan _
                Then
                If Not Titel = Titel_NamaAkun Then NamaAkun = Titel
                datatabelUtama.Rows.Add(
                    NamaAkun, COA,
                    SaldoJanuari,
                    SaldoFebruari,
                    SaldoMaret,
                    SaldoApril,
                    SaldoMei,
                    SaldoJuni,
                    SaldoJuli,
                    SaldoAgustus,
                    SaldoSeptember,
                    SaldoOktober,
                    SaldoNopember,
                    SaldoDesember,
                    SaldoKeseluruhan)
                Terabas()
            End If
            Select Case Operasi
                Case Operasi_Penambah
                    SaldoKhususJanuari += SaldoJanuari
                    SaldoKhususFebruari += SaldoFebruari
                    SaldoKhususMaret += SaldoMaret
                    SaldoKhususApril += SaldoApril
                    SaldoKhususMei += SaldoMei
                    SaldoKhususJuni += SaldoJuni
                    SaldoKhususJuli += SaldoJuli
                    SaldoKhususAgustus += SaldoAgustus
                    SaldoKhususSeptember += SaldoSeptember
                    SaldoKhususOktober += SaldoOktober
                    SaldoKhususNopember += SaldoNopember
                    SaldoKhususDesember += SaldoDesember
                    SaldoKhususKeseluruhan += SaldoKeseluruhan
                Case Operasi_Pengurang
                    SaldoKhususJanuari -= SaldoJanuari
                    SaldoKhususFebruari -= SaldoFebruari
                    SaldoKhususMaret -= SaldoMaret
                    SaldoKhususApril -= SaldoApril
                    SaldoKhususMei -= SaldoMei
                    SaldoKhususJuni -= SaldoJuni
                    SaldoKhususJuli -= SaldoJuli
                    SaldoKhususAgustus -= SaldoAgustus
                    SaldoKhususSeptember -= SaldoSeptember
                    SaldoKhususOktober -= SaldoOktober
                    SaldoKhususNopember -= SaldoNopember
                    SaldoKhususDesember -= SaldoDesember
                    SaldoKhususKeseluruhan -= SaldoKeseluruhan
            End Select
        Loop
        AksesDatabase_General(Tutup)

    End Sub



    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_BukuBesar.IsEnabled = False
    End Sub




    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_BukuBesar_Click(sender As Object, e As RoutedEventArgs) Handles btn_BukuBesar.Click
        BukaHalamanBukuBesar(COA_Terseleksi)
    End Sub


    'Private Sub btn_TrialBalance_Click(sender As Object, e As RoutedEventArgs) Handles btn_TrialBalance.Click
    '    frm_BOOKU.BukaModul_LaporanTrialBalance()
    'End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub



    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        'NomorUrut_Terseleksi = AmbilAngka(AmbilValueCellBerpotensiDBNull(rowviewUtama, "Nomor_Urut"))
        COA_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Akun")

        If COA_Terseleksi = Kosongan Then
            btn_BukuBesar.IsEnabled = False
        Else
            btn_BukuBesar.IsEnabled = True
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        'Belum ada kebutuhan Coding di sini.
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        If IsDBNull(e.Row.Item("Kode_Akun")) Then
            e.Row.Background = WarnaHitam_10_WPF
            e.Row.FontWeight = FontWeights.Bold
            e.Row.Foreground = WarnaTegas_WPF
        Else
            If AmbilAngka(e.Row.Item("Kode_Akun")) = 0 Then
                e.Row.Background = WarnaHitam_5_WPF
                e.Row.Foreground = WarnaTegas_WPF
            ElseIf e.Row.Item("Kode_Akun") = KodeTautanCOA_HargaPokokPenjualan Then
                e.Row.Background = WarnaHitam_15_WPF
                e.Row.FontWeight = FontWeights.Bold
                e.Row.Foreground = WarnaTegas_WPF
            Else
                e.Row.Foreground = WarnaTeksStandar_WPF
            End If
        End If
    End Sub



    'Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public rowgridUtama As DataGridRow
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nama_Akun As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Saldo_Januari As New DataGridTextColumn
    Dim Saldo_Februari As New DataGridTextColumn
    Dim Saldo_Maret As New DataGridTextColumn
    Dim Saldo_April As New DataGridTextColumn
    Dim Saldo_Mei As New DataGridTextColumn
    Dim Saldo_Juni As New DataGridTextColumn
    Dim Saldo_Juli As New DataGridTextColumn
    Dim Saldo_Agustus As New DataGridTextColumn
    Dim Saldo_September As New DataGridTextColumn
    Dim Saldo_Oktober As New DataGridTextColumn
    Dim Saldo_Nopember As New DataGridTextColumn
    Dim Saldo_Desember As New DataGridTextColumn
    Dim Saldo_Total As New DataGridTextColumn

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Saldo_Januari", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Februari", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Maret", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_April", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Mei", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Juni", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Juli", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Agustus", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_September", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Oktober", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Nopember", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Desember", GetType(Int64))
        datatabelUtama.Columns.Add("Saldo_Total", GetType(Int64))

        StyleTabelUtama_Laporan_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Januari, "Saldo_Januari", "Januari", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Februari, "Saldo_Februari", "Februari", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Maret, "Saldo_Maret", "Maret", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_April, "Saldo_April", "April", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Mei, "Saldo_Mei", "Mei", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Juni, "Saldo_Juni", "Juni", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Juli, "Saldo_Juli", "Juli", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Agustus, "Saldo_Agustus", "Agustus", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_September, "Saldo_September", "September", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Oktober, "Saldo_Oktober", "Oktober", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Nopember, "Saldo_Nopember", "Nopember", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Desember, "Saldo_Desember", "Desember", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Total, "Saldo_Total", "Total", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)

    End Sub

    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
    End Sub

    Sub datagridUtama_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
        KetentuanUkuran()
    End Sub
    Sub pnl_Konten_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles pnl_Konten.SizeChanged
        KetentuanUkuran()
    End Sub
    Dim LebarKonten As Integer
    Dim TinggiKonten As Integer
    Sub KetentuanUkuran()
        LebarKonten = pnl_Konten.ActualWidth
        TinggiKonten = pnl_Konten.ActualHeight
        datagridUtama.MaxHeight = TinggiKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
    End Sub

End Class