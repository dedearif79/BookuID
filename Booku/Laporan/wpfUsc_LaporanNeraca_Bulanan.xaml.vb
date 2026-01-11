Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm
Imports Windows.Win32.System


Public Class wpfUsc_LaporanNeraca_Bulanan

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public JudulForm

    Dim QueryTampilan

    Dim TotalSaldoAwal As Int64
    Dim TotalSaldoJanuari As Int64
    Dim TotalSaldoFebruari As Int64
    Dim TotalSaldoMaret As Int64
    Dim TotalSaldoApril As Int64
    Dim TotalSaldoMei As Int64
    Dim TotalSaldoJuni As Int64
    Dim TotalSaldoJuli As Int64
    Dim TotalSaldoAgustus As Int64
    Dim TotalSaldoSeptember As Int64
    Dim TotalSaldoOktober As Int64
    Dim TotalSaldoNopember As Int64
    Dim TotalSaldoDesember As Int64

    Dim BalanceAwal As Int64
    Dim BalanceJanuari As Int64
    Dim BalanceFebruari As Int64
    Dim BalanceMaret As Int64
    Dim BalanceApril As Int64
    Dim BalanceMei As Int64
    Dim BalanceJuni As Int64
    Dim BalanceJuli As Int64
    Dim BalanceAgustus As Int64
    Dim BalanceSeptember As Int64
    Dim BalanceOktober As Int64
    Dim BalanceNopember As Int64
    Dim BalanceDesember As Int64

    Dim COA_Terseleksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        ProsesLoadingForm = True

        lbl_JudulForm.Text = frm_LaporanNeraca_Bulanan.JudulForm


        ProsesLoadingForm = False

        RefreshTampilanData()

        SudahDimuat = True
    End Sub



    Sub RefreshTampilanData()
        'If StatusTrialBalance Then
        '    TampilkanData()
        'Else
        '    MsgBox("Telah terjadi perubahan data pada Jurnal." &
        '    Enter2Baris & "Silakan klik tombol 'Trial Balance' terlebih dahulu.")
        '    Return
        'End If
        TampilkanData()
    End Sub




    Sub TampilkanData()

        KetersediaanMenuHalaman(pnl_Halaman, False)

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        Dim QueryTampilanUmum = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' "

        '-----------------------------------------------------------
        'AKTIVA :
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("AKTIVA")

        'Aktiva Lancar :
        datatabelUtama.Rows.Add("Aktiva Lancar")
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 10000 AND 11999 " '( COA Kelompok AKTIVA LANCAR )
        DataPerKategoriCOA()
        Dim TotalSaldoAwalAktivaLancar As Int64 = TotalSaldoAwal
        Dim TotalSaldoJanuariAktivaLancar As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariAktivaLancar As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretAktivaLancar As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilAktivaLancar As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiAktivaLancar As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniAktivaLancar As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliAktivaLancar As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusAktivaLancar As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberAktivaLancar As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberAktivaLancar As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberAktivaLancar As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberAktivaLancar As Int64 = TotalSaldoDesember
        datatabelUtama.Rows.Add(
            "Total Aktiva Lancar", Kosongan,
            TotalSaldoAwalAktivaLancar,
            TotalSaldoJanuariAktivaLancar,
            TotalSaldoFebruariAktivaLancar,
            TotalSaldoMaretAktivaLancar,
            TotalSaldoAprilAktivaLancar,
            TotalSaldoMeiAktivaLancar,
            TotalSaldoJuniAktivaLancar,
            TotalSaldoJuliAktivaLancar,
            TotalSaldoAgustusAktivaLancar,
            TotalSaldoSeptemberAktivaLancar,
            TotalSaldoOktoberAktivaLancar,
            TotalSaldoNopemberAktivaLancar,
            TotalSaldoDesemberAktivaLancar)
        'datatabelUtama.Rows.Add()

        'Aktiva Tetap :
        datatabelUtama.Rows.Add("Aktiva Tetap")
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 12000 AND 19999 " '( COA Kelompok AKTIVA TETAP )
        DataPerKategoriCOA()
        Dim TotalSaldoAwalAktivaTetap As Int64 = TotalSaldoAwal
        Dim TotalSaldoJanuariAktivaTetap As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariAktivaTetap As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretAktivaTetap As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilAktivaTetap As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiAktivaTetap As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniAktivaTetap As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliAktivaTetap As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusAktivaTetap As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberAktivaTetap As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberAktivaTetap As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberAktivaTetap As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberAktivaTetap As Int64 = TotalSaldoDesember
        datatabelUtama.Rows.Add(
            "Total Aktiva Tetap", Kosongan,
            TotalSaldoAwalAktivaTetap,
            TotalSaldoJanuariAktivaTetap,
            TotalSaldoFebruariAktivaTetap,
            TotalSaldoMaretAktivaTetap,
            TotalSaldoAprilAktivaTetap,
            TotalSaldoMeiAktivaTetap,
            TotalSaldoJuniAktivaTetap,
            TotalSaldoJuliAktivaTetap,
            TotalSaldoAgustusAktivaTetap,
            TotalSaldoSeptemberAktivaTetap,
            TotalSaldoOktoberAktivaTetap,
            TotalSaldoNopemberAktivaTetap,
            TotalSaldoDesemberAktivaTetap)
        'datatabelUtama.Rows.Add()

        'Total AKTIVA
        Dim TotalSaldoAwalAKTIVA As Int64 = TotalSaldoAwalAktivaLancar + TotalSaldoAwalAktivaTetap
        Dim TotalSaldoJanuariAKTIVA As Int64 = TotalSaldoJanuariAktivaLancar + TotalSaldoJanuariAktivaTetap
        Dim TotalSaldoFebruariAKTIVA As Int64 = TotalSaldoFebruariAktivaLancar + TotalSaldoFebruariAktivaTetap
        Dim TotalSaldoMaretAKTIVA As Int64 = TotalSaldoMaretAktivaLancar + TotalSaldoMaretAktivaTetap
        Dim TotalSaldoAprilAKTIVA As Int64 = TotalSaldoAprilAktivaLancar + TotalSaldoAprilAktivaTetap
        Dim TotalSaldoMeiAKTIVA As Int64 = TotalSaldoMeiAktivaLancar + TotalSaldoMeiAktivaTetap
        Dim TotalSaldoJuniAKTIVA As Int64 = TotalSaldoJuniAktivaLancar + TotalSaldoJuniAktivaTetap
        Dim TotalSaldoJuliAKTIVA As Int64 = TotalSaldoJuliAktivaLancar + TotalSaldoJuliAktivaTetap
        Dim TotalSaldoAgustusAKTIVA As Int64 = TotalSaldoAgustusAktivaLancar + TotalSaldoAgustusAktivaTetap
        Dim TotalSaldoSeptemberAKTIVA As Int64 = TotalSaldoSeptemberAktivaLancar + TotalSaldoSeptemberAktivaTetap
        Dim TotalSaldoOktoberAKTIVA As Int64 = TotalSaldoOktoberAktivaLancar + TotalSaldoOktoberAktivaTetap
        Dim TotalSaldoNopemberAKTIVA As Int64 = TotalSaldoNopemberAktivaLancar + TotalSaldoNopemberAktivaTetap
        Dim TotalSaldoDesemberAKTIVA As Int64 = TotalSaldoDesemberAktivaLancar + TotalSaldoDesemberAktivaTetap
        datatabelUtama.Rows.Add(
            "TOTAL AKTIVA", Kosongan,
            TotalSaldoAwalAKTIVA,
            TotalSaldoJanuariAKTIVA,
            TotalSaldoFebruariAKTIVA,
            TotalSaldoMaretAKTIVA,
            TotalSaldoAprilAKTIVA,
            TotalSaldoMeiAKTIVA,
            TotalSaldoJuniAKTIVA,
            TotalSaldoJuliAKTIVA,
            TotalSaldoAgustusAKTIVA,
            TotalSaldoSeptemberAKTIVA,
            TotalSaldoOktoberAKTIVA,
            TotalSaldoNopemberAKTIVA,
            TotalSaldoDesemberAKTIVA)
        'datatabelUtama.Rows.Add()

        '-----------------------------------------------------------
        'PASSIVA
        datatabelUtama.Rows.Add("PASSIVA")
        '-----------------------------------------------------------

        'Hutang :
        datatabelUtama.Rows.Add("Hutang")

        'Hutang Jangka Pendek :
        datatabelUtama.Rows.Add("Hutang Jangka Pendek")
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 21000 AND 21999 "  '( COA Kelompok HUTANG JANGKA PENDEK )
        DataPerKategoriCOA()
        Dim TotalSaldoAwalHutangJangkaPendek As Int64 = TotalSaldoAwal
        Dim TotalSaldoJanuariHutangJangkaPendek As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariHutangJangkaPendek As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretHutangJangkaPendek As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilHutangJangkaPendek As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiHutangJangkaPendek As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniHutangJangkaPendek As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliHutangJangkaPendek As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusHutangJangkaPendek As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberHutangJangkaPendek As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberHutangJangkaPendek As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberHutangJangkaPendek As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberHutangJangkaPendek As Int64 = TotalSaldoDesember
        datatabelUtama.Rows.Add(
            "Total Hutang Jangka Pendek", Kosongan,
            TotalSaldoAwalHutangJangkaPendek,
            TotalSaldoJanuariHutangJangkaPendek,
            TotalSaldoFebruariHutangJangkaPendek,
            TotalSaldoMaretHutangJangkaPendek,
            TotalSaldoAprilHutangJangkaPendek,
            TotalSaldoMeiHutangJangkaPendek,
            TotalSaldoJuniHutangJangkaPendek,
            TotalSaldoJuliHutangJangkaPendek,
            TotalSaldoAgustusHutangJangkaPendek,
            TotalSaldoSeptemberHutangJangkaPendek,
            TotalSaldoOktoberHutangJangkaPendek,
            TotalSaldoNopemberHutangJangkaPendek,
            TotalSaldoDesemberHutangJangkaPendek)
        'datatabelUtama.Rows.Add()

        'Hutang Jangka Panjang :
        datatabelUtama.Rows.Add("Hutang Jangka Panjang")
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 22000 AND 29999 " ' ( COA Kelompok HUTANG JANGKA PANJANG )
        DataPerKategoriCOA()
        Dim TotalSaldoAwalHutangJangkaPanjang As Int64 = TotalSaldoAwal
        Dim TotalSaldoJanuariHutangJangkaPanjang As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariHutangJangkaPanjang As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretHutangJangkaPanjang As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilHutangJangkaPanjang As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiHutangJangkaPanjang As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniHutangJangkaPanjang As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliHutangJangkaPanjang As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusHutangJangkaPanjang As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberHutangJangkaPanjang As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberHutangJangkaPanjang As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberHutangJangkaPanjang As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberHutangJangkaPanjang As Int64 = TotalSaldoDesember
        datatabelUtama.Rows.Add(
            "Total Hutang Jangka Panjang", Kosongan,
            TotalSaldoAwalHutangJangkaPanjang,
            TotalSaldoJanuariHutangJangkaPanjang,
            TotalSaldoFebruariHutangJangkaPanjang,
            TotalSaldoMaretHutangJangkaPanjang,
            TotalSaldoAprilHutangJangkaPanjang,
            TotalSaldoMeiHutangJangkaPanjang,
            TotalSaldoJuniHutangJangkaPanjang,
            TotalSaldoJuliHutangJangkaPanjang,
            TotalSaldoAgustusHutangJangkaPanjang,
            TotalSaldoSeptemberHutangJangkaPanjang,
            TotalSaldoOktoberHutangJangkaPanjang,
            TotalSaldoNopemberHutangJangkaPanjang,
            TotalSaldoDesemberHutangJangkaPanjang)
        'datatabelUtama.Rows.Add()

        'Total Hutang
        Dim TotalSaldoAwalHutang As Int64 = TotalSaldoAwalHutangJangkaPendek + TotalSaldoAwalHutangJangkaPanjang
        Dim TotalSaldoJanuariHutang As Int64 = TotalSaldoJanuariHutangJangkaPendek + TotalSaldoJanuariHutangJangkaPanjang
        Dim TotalSaldoFebruariHutang As Int64 = TotalSaldoFebruariHutangJangkaPendek + TotalSaldoFebruariHutangJangkaPanjang
        Dim TotalSaldoMaretHutang As Int64 = TotalSaldoMaretHutangJangkaPendek + TotalSaldoMaretHutangJangkaPanjang
        Dim TotalSaldoAprilHutang As Int64 = TotalSaldoAprilHutangJangkaPendek + TotalSaldoAprilHutangJangkaPanjang
        Dim TotalSaldoMeiHutang As Int64 = TotalSaldoMeiHutangJangkaPendek + TotalSaldoMeiHutangJangkaPanjang
        Dim TotalSaldoJuniHutang As Int64 = TotalSaldoJuniHutangJangkaPendek + TotalSaldoJuniHutangJangkaPanjang
        Dim TotalSaldoJuliHutang As Int64 = TotalSaldoJuliHutangJangkaPendek + TotalSaldoJuliHutangJangkaPanjang
        Dim TotalSaldoAgustusHutang As Int64 = TotalSaldoAgustusHutangJangkaPendek + TotalSaldoAgustusHutangJangkaPanjang
        Dim TotalSaldoSeptemberHutang As Int64 = TotalSaldoSeptemberHutangJangkaPendek + TotalSaldoSeptemberHutangJangkaPanjang
        Dim TotalSaldoOktoberHutang As Int64 = TotalSaldoOktoberHutangJangkaPendek + TotalSaldoOktoberHutangJangkaPanjang
        Dim TotalSaldoNopemberHutang As Int64 = TotalSaldoNopemberHutangJangkaPendek + TotalSaldoNopemberHutangJangkaPanjang
        Dim TotalSaldoDesemberHutang As Int64 = TotalSaldoDesemberHutangJangkaPendek + TotalSaldoDesemberHutangJangkaPanjang
        datatabelUtama.Rows.Add(
            "Total Hutang", Kosongan,
            TotalSaldoAwalHutang,
            TotalSaldoJanuariHutang,
            TotalSaldoFebruariHutang,
            TotalSaldoMaretHutang,
            TotalSaldoAprilHutang,
            TotalSaldoMeiHutang,
            TotalSaldoJuniHutang,
            TotalSaldoJuliHutang,
            TotalSaldoAgustusHutang,
            TotalSaldoSeptemberHutang,
            TotalSaldoOktoberHutang,
            TotalSaldoNopemberHutang,
            TotalSaldoDesemberHutang)
        'datatabelUtama.Rows.Add()

        'Modal
        datatabelUtama.Rows.Add("Modal")
        QueryTampilan = QueryTampilanUmum & " AND COA LIKE '3%' " '( COA Kelompok MODAL )
        DataPerKategoriCOA()

        Dim TotalSaldoAwalModal As Int64 = TotalSaldoAwal
        Dim TotalSaldoJanuariModal As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariModal As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretModal As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilModal As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiModal As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniModal As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliModal As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusModal As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberModal As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberModal As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberModal As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberModal As Int64 = TotalSaldoDesember



        'Laba Tahun Berjalan
        QueryTampilan = QueryTampilanUmum & " AND COA >= 40000 " '( COA Kelompok PENDAPATAN DAN BIAYA )
        DataPerKategoriCOA()

        'Total Equity
        'Rumus : Equity = Modal + Laba Tahun Berjalan
        Dim TotalSaldoAwalEquity As Int64 = TotalSaldoAwalModal
        Dim TotalSaldoJanuariEquity As Int64 = TotalSaldoJanuariModal
        Dim TotalSaldoFebruariEquity As Int64 = TotalSaldoFebruariModal
        Dim TotalSaldoMaretEquity As Int64 = TotalSaldoMaretModal
        Dim TotalSaldoAprilEquity As Int64 = TotalSaldoAprilModal
        Dim TotalSaldoMeiEquity As Int64 = TotalSaldoMeiModal
        Dim TotalSaldoJuniEquity As Int64 = TotalSaldoJuniModal
        Dim TotalSaldoJuliEquity As Int64 = TotalSaldoJuliModal
        Dim TotalSaldoAgustusEquity As Int64 = TotalSaldoAgustusModal
        Dim TotalSaldoSeptemberEquity As Int64 = TotalSaldoSeptemberModal
        Dim TotalSaldoOktoberEquity As Int64 = TotalSaldoOktoberModal
        Dim TotalSaldoNopemberEquity As Int64 = TotalSaldoNopemberModal
        Dim TotalSaldoDesemberEquity As Int64 = TotalSaldoDesemberModal
        datatabelUtama.Rows.Add(
            "Total Equity", Kosongan,
            TotalSaldoAwalEquity,
            TotalSaldoJanuariEquity,
            TotalSaldoFebruariEquity,
            TotalSaldoMaretEquity,
            TotalSaldoAprilEquity,
            TotalSaldoMeiEquity,
            TotalSaldoJuniEquity,
            TotalSaldoJuliEquity,
            TotalSaldoAgustusEquity,
            TotalSaldoSeptemberEquity,
            TotalSaldoOktoberEquity,
            TotalSaldoNopemberEquity,
            TotalSaldoDesemberEquity)
        'datatabelUtama.Rows.Add()

        'TOTAL PASSIVA
        Dim TotalSaldoAwalPASSIVA As Int64 = TotalSaldoAwalHutang + TotalSaldoAwalEquity
        Dim TotalSaldoJanuariPASSIVA As Int64 = TotalSaldoJanuariHutang + TotalSaldoJanuariEquity
        Dim TotalSaldoFebruariPASSIVA As Int64 = TotalSaldoFebruariHutang + TotalSaldoFebruariEquity
        Dim TotalSaldoMaretPASSIVA As Int64 = TotalSaldoMaretHutang + TotalSaldoMaretEquity
        Dim TotalSaldoAprilPASSIVA As Int64 = TotalSaldoAprilHutang + TotalSaldoAprilEquity
        Dim TotalSaldoMeiPASSIVA As Int64 = TotalSaldoMeiHutang + TotalSaldoMeiEquity
        Dim TotalSaldoJuniPASSIVA As Int64 = TotalSaldoJuniHutang + TotalSaldoJuniEquity
        Dim TotalSaldoJuliPASSIVA As Int64 = TotalSaldoJuliHutang + TotalSaldoJuliEquity
        Dim TotalSaldoAgustusPASSIVA As Int64 = TotalSaldoAgustusHutang + TotalSaldoAgustusEquity
        Dim TotalSaldoSeptemberPASSIVA As Int64 = TotalSaldoSeptemberHutang + TotalSaldoSeptemberEquity
        Dim TotalSaldoOktoberPASSIVA As Int64 = TotalSaldoOktoberHutang + TotalSaldoOktoberEquity
        Dim TotalSaldoNopemberPASSIVA As Int64 = TotalSaldoNopemberHutang + TotalSaldoNopemberEquity
        Dim TotalSaldoDesemberPASSIVA As Int64 = TotalSaldoDesemberHutang + TotalSaldoDesemberEquity
        datatabelUtama.Rows.Add(
            "TOTAL PASSIVA", Kosongan,
            TotalSaldoAwalPASSIVA,
            TotalSaldoJanuariPASSIVA,
            TotalSaldoFebruariPASSIVA,
            TotalSaldoMaretPASSIVA,
            TotalSaldoAprilPASSIVA,
            TotalSaldoMeiPASSIVA,
            TotalSaldoJuniPASSIVA,
            TotalSaldoJuliPASSIVA,
            TotalSaldoAgustusPASSIVA,
            TotalSaldoSeptemberPASSIVA,
            TotalSaldoOktoberPASSIVA,
            TotalSaldoNopemberPASSIVA,
            TotalSaldoDesemberPASSIVA)
        'datatabelUtama.Rows.Add()

        BalanceAwal = TotalSaldoAwalAKTIVA - TotalSaldoAwalPASSIVA
        BalanceJanuari = TotalSaldoJanuariAKTIVA - TotalSaldoJanuariPASSIVA
        BalanceFebruari = TotalSaldoFebruariAKTIVA - TotalSaldoFebruariPASSIVA
        BalanceMaret = TotalSaldoMaretAKTIVA - TotalSaldoMaretPASSIVA
        BalanceApril = TotalSaldoAprilAKTIVA - TotalSaldoAprilPASSIVA
        BalanceMei = TotalSaldoMeiAKTIVA - TotalSaldoMeiPASSIVA
        BalanceJuni = TotalSaldoJuniAKTIVA - TotalSaldoJuniPASSIVA
        BalanceJuli = TotalSaldoJuliAKTIVA - TotalSaldoJuliPASSIVA
        BalanceAgustus = TotalSaldoAgustusAKTIVA - TotalSaldoAgustusPASSIVA
        BalanceSeptember = TotalSaldoSeptemberAKTIVA - TotalSaldoSeptemberPASSIVA
        BalanceOktober = TotalSaldoOktoberAKTIVA - TotalSaldoOktoberPASSIVA
        BalanceNopember = TotalSaldoNopemberAKTIVA - TotalSaldoNopemberPASSIVA
        BalanceDesember = TotalSaldoDesemberAKTIVA - TotalSaldoDesemberPASSIVA

        BarisBalance()

        BersihkanSeleksi()

    End Sub

    Sub DataPerKategoriCOA()

        TotalSaldoAwal = 0
        TotalSaldoJanuari = 0
        TotalSaldoFebruari = 0
        TotalSaldoMaret = 0
        TotalSaldoApril = 0
        TotalSaldoMei = 0
        TotalSaldoJuni = 0
        TotalSaldoJuli = 0
        TotalSaldoAgustus = 0
        TotalSaldoSeptember = 0
        TotalSaldoOktober = 0
        TotalSaldoNopember = 0
        TotalSaldoDesember = 0

        'Data Tabel :
        Dim COA
        Dim NamaAkun
        Dim KodeMataUang As String
        Dim DebetKreditCOA
        Dim SaldoAwal As Int64
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

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY COA ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            COA = dr.Item("COA")
            NamaAkun = dr.Item("Nama_Akun")
            KodeMataUang = dr.Item("Kode_Mata_Uang")
            DebetKreditCOA = dr.Item("D_K")
            SaldoAwal = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirTahunLalu(KodeMataUang), dr.Item("Saldo_Awal"))
            SaldoJanuari = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 1), dr.Item("Saldo_Januari"))
            SaldoFebruari = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 2), dr.Item("Saldo_Februari"))
            SaldoMaret = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 3), dr.Item("Saldo_Maret"))
            SaldoApril = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 4), dr.Item("Saldo_April"))
            SaldoMei = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 5), dr.Item("Saldo_Mei"))
            SaldoJuni = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 6), dr.Item("Saldo_Juni"))
            SaldoJuli = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 7), dr.Item("Saldo_Juli"))
            SaldoAgustus = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 8), dr.Item("Saldo_Agustus"))
            SaldoSeptember = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 9), dr.Item("Saldo_September"))
            SaldoOktober = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 10), dr.Item("Saldo_Oktober"))
            SaldoNopember = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 11), dr.Item("Saldo_Nopember"))
            SaldoDesember = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, KursTengahBI_AkhirBulan(KodeMataUang, 12), dr.Item("Saldo_Desember"))
            If COA = KodeTautanCOA_LabaDitahan Then
                SaldoAwal = SaldoAwal
                SaldoJanuari = SaldoAwal
                SaldoFebruari = SaldoAwal
                SaldoMaret = SaldoAwal
                SaldoApril = SaldoAwal
                SaldoMei = SaldoAwal
                SaldoJuni = SaldoAwal
                SaldoJuli = SaldoAwal
                SaldoAgustus = SaldoAwal
                SaldoSeptember = SaldoAwal
                SaldoOktober = SaldoAwal
                SaldoNopember = SaldoAwal
                SaldoDesember = SaldoAwal
            End If
            If COA = KodeTautanCOA_LabaTahunBerjalan Then
                SaldoJanuari += SaldoAwal
                SaldoFebruari += SaldoJanuari
                SaldoMaret += SaldoFebruari
                SaldoApril += SaldoMaret
                SaldoMei += SaldoApril
                SaldoJuni += SaldoMei
                SaldoJuli += SaldoJuni
                SaldoAgustus += SaldoJuli
                SaldoSeptember += SaldoAgustus
                SaldoOktober += SaldoSeptember
                SaldoNopember += SaldoOktober
                SaldoDesember += SaldoNopember
            End If

            If COA < 40000 Then '( Jika COA termasuk Kelompok NERACA )
                If (SaldoJanuari <> 0 _
                    Or SaldoFebruari <> 0 _
                    Or SaldoMaret <> 0 _
                    Or SaldoApril <> 0 _
                    Or SaldoMei <> 0 _
                    Or SaldoJuni <> 0 _
                    Or SaldoJuli <> 0 _
                    Or SaldoAgustus <> 0 _
                    Or SaldoSeptember <> 0 _
                    Or SaldoOktober <> 0 _
                    Or SaldoNopember <> 0 _
                    Or SaldoDesember <> 0) Then
                    datatabelUtama.Rows.Add(
                        NamaAkun, COA, SaldoAwal,
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
                                       SaldoDesember)
                    Terabas()
                End If
                TotalSaldoAwal += SaldoAwal
                TotalSaldoJanuari += SaldoJanuari
                TotalSaldoFebruari += SaldoFebruari
                TotalSaldoMaret += SaldoMaret
                TotalSaldoApril += SaldoApril
                TotalSaldoMei += SaldoMei
                TotalSaldoJuni += SaldoJuni
                TotalSaldoJuli += SaldoJuli
                TotalSaldoAgustus += SaldoAgustus
                TotalSaldoSeptember += SaldoSeptember
                TotalSaldoOktober += SaldoOktober
                TotalSaldoNopember += SaldoNopember
                TotalSaldoDesember += SaldoDesember
            End If
            If COA >= 40000 Then '( Jika COA termasuk Kelompok LABA/RUGI )
                If DebetKreditCOA = dk_DEBET_ Then
                    TotalSaldoAwal -= SaldoAwal
                    TotalSaldoJanuari -= SaldoJanuari
                    TotalSaldoFebruari -= SaldoFebruari
                    TotalSaldoMaret -= SaldoMaret
                    TotalSaldoApril -= SaldoApril
                    TotalSaldoMei -= SaldoMei
                    TotalSaldoJuni -= SaldoJuni
                    TotalSaldoJuli -= SaldoJuli
                    TotalSaldoAgustus -= SaldoAgustus
                    TotalSaldoSeptember -= SaldoSeptember
                    TotalSaldoOktober -= SaldoOktober
                    TotalSaldoNopember -= SaldoNopember
                    TotalSaldoDesember -= SaldoDesember
                End If
                If DebetKreditCOA = dk_KREDIT_ Then
                    TotalSaldoAwal += SaldoAwal
                    TotalSaldoJanuari += SaldoJanuari
                    TotalSaldoFebruari += SaldoFebruari
                    TotalSaldoMaret += SaldoMaret
                    TotalSaldoApril += SaldoApril
                    TotalSaldoMei += SaldoMei
                    TotalSaldoJuni += SaldoJuni
                    TotalSaldoJuli += SaldoJuli
                    TotalSaldoAgustus += SaldoAgustus
                    TotalSaldoSeptember += SaldoSeptember
                    TotalSaldoOktober += SaldoOktober
                    TotalSaldoNopember += SaldoNopember
                    TotalSaldoDesember += SaldoDesember
                End If
            End If
        Loop
        AksesDatabase_General(Tutup)

    End Sub


    Sub BarisBalance()
        datatabelUtama.Rows.Add(
            "Balance Control", Kosongan,
            BalanceAwal,
            BalanceJanuari,
            BalanceFebruari,
            BalanceMaret,
            BalanceApril,
            BalanceMei,
            BalanceJuni,
            BalanceJuli,
            BalanceAgustus,
            BalanceSeptember,
            BalanceOktober,
            BalanceNopember,
            BalanceDesember)
        'datatabelUtama.Rows.Add()
    End Sub


    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Nothing
        datagridUtama.SelectedCells.Clear()
        btn_BukuBesar.IsEnabled = False
        KetersediaanMenuHalaman(pnl_Halaman, True)
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
            'e.Row.Background = WarnaHitam_10_WPF
            e.Row.FontWeight = FontWeights.Bold
            e.Row.Foreground = WarnaTegas_WPF
        Else
            If AmbilAngka(e.Row.Item("Kode_Akun")) = 0 Then
                'e.Row.Background = WarnaHitam_5_WPF
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
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nama_Akun As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim Saldo_Awal As New DataGridTextColumn
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

    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("Saldo_Awal", GetType(Int64))
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


        StyleTabelUtama_Laporan_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Saldo_Awal, "Saldo_Awal", "Saldo Awal", 111, FormatAngka, KananTengah, KunciUrut, Terlihat)
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
