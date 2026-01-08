Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_Laporan_Neraca_X

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

    Private Sub frm_LaporanNeraca_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lbl_Judul.Text = "Laporan Neraca - Tahun " & TahunBukuAktif

        RefreshTampilanData()

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

    Sub TampilkanData()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        Dim QueryTampilanUmum = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' "

        '-----------------------------------------------------------
        'AKTIVA :
        '-----------------------------------------------------------
        DataTabelUtama.Rows.Add("AKTIVA")

        'Aktiva Lancar :
        DataTabelUtama.Rows.Add("Aktiva Lancar")
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
        DataTabelUtama.Rows.Add(
            "Total Aktiva Lancar", "",
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
        DataTabelUtama.Rows.Add()

        'Aktiva Tetap :
        DataTabelUtama.Rows.Add("Aktiva Tetap")
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
        DataTabelUtama.Rows.Add(
            "Total Aktiva Tetap", "",
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
        DataTabelUtama.Rows.Add()

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
        DataTabelUtama.Rows.Add(
            "TOTAL AKTIVA", "",
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
        DataTabelUtama.Rows.Add()

        '-----------------------------------------------------------
        'PASSIVA
        DataTabelUtama.Rows.Add("PASSIVA")
        '-----------------------------------------------------------

        'Hutang :
        DataTabelUtama.Rows.Add("Hutang")

        'Hutang Jangka Pendek :
        DataTabelUtama.Rows.Add("Hutang Jangka Pendek")
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
        DataTabelUtama.Rows.Add(
            "Total Hutang Jangka Pendek", "",
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
        DataTabelUtama.Rows.Add()

        'Hutang Jangka Panjang :
        DataTabelUtama.Rows.Add("Hutang Jangka Panjang")
        QueryTampilan = QueryTampilanUmum & " AND COA BETWEEN 22000 AND 22999 " ' ( COA Kelompok HUTANG JANGKA PANJANG )
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
        DataTabelUtama.Rows.Add(
            "Total Hutang Jangka Panjang", "",
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
        DataTabelUtama.Rows.Add()

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
        DataTabelUtama.Rows.Add(
            "Total Hutang", "",
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
        DataTabelUtama.Rows.Add()

        'Modal
        DataTabelUtama.Rows.Add("Modal")
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
        Dim TotalSaldoAwalLabaTahunBerjalan As Int64 = TotalSaldoAwal
        Dim TotalSaldoJanuariLabaTahunBerjalan As Int64 = TotalSaldoAwalLabaTahunBerjalan + TotalSaldoJanuari
        Dim TotalSaldoFebruariLabaTahunBerjalan As Int64 = TotalSaldoJanuariLabaTahunBerjalan + TotalSaldoFebruari
        Dim TotalSaldoMaretLabaTahunBerjalan As Int64 = TotalSaldoFebruariLabaTahunBerjalan + TotalSaldoMaret
        Dim TotalSaldoAprilLabaTahunBerjalan As Int64 = TotalSaldoMaretLabaTahunBerjalan + TotalSaldoApril
        Dim TotalSaldoMeiLabaTahunBerjalan As Int64 = TotalSaldoAprilLabaTahunBerjalan + TotalSaldoMei
        Dim TotalSaldoJuniLabaTahunBerjalan As Int64 = TotalSaldoMeiLabaTahunBerjalan + TotalSaldoJuni
        Dim TotalSaldoJuliLabaTahunBerjalan As Int64 = TotalSaldoJuniLabaTahunBerjalan + TotalSaldoJuli
        Dim TotalSaldoAgustusLabaTahunBerjalan As Int64 = TotalSaldoJuliLabaTahunBerjalan + TotalSaldoAgustus
        Dim TotalSaldoSeptemberLabaTahunBerjalan As Int64 = TotalSaldoAgustusLabaTahunBerjalan + TotalSaldoSeptember
        Dim TotalSaldoOktoberLabaTahunBerjalan As Int64 = TotalSaldoSeptemberLabaTahunBerjalan + TotalSaldoOktober
        Dim TotalSaldoNopemberLabaTahunBerjalan As Int64 = TotalSaldoOktoberLabaTahunBerjalan + TotalSaldoNopember
        Dim TotalSaldoDesemberLabaTahunBerjalan As Int64 = TotalSaldoNopemberLabaTahunBerjalan + TotalSaldoDesember
        DataTabelUtama.Rows.Add(
            "Laba Tahun Berjalan", KodeTautanCOA_LabaTahunBerjalan,
            TotalSaldoAwalLabaTahunBerjalan,
            TotalSaldoJanuariLabaTahunBerjalan,
            TotalSaldoFebruariLabaTahunBerjalan,
            TotalSaldoMaretLabaTahunBerjalan,
            TotalSaldoAprilLabaTahunBerjalan,
            TotalSaldoMeiLabaTahunBerjalan,
            TotalSaldoJuniLabaTahunBerjalan,
            TotalSaldoJuliLabaTahunBerjalan,
            TotalSaldoAgustusLabaTahunBerjalan,
            TotalSaldoSeptemberLabaTahunBerjalan,
            TotalSaldoOktoberLabaTahunBerjalan,
            TotalSaldoNopemberLabaTahunBerjalan,
            TotalSaldoDesemberLabaTahunBerjalan)
        DataTabelUtama.Rows.Add()

        'Total Equity
        Dim TotalSaldoAwalEquity As Int64 = TotalSaldoAwalModal + TotalSaldoAwalLabaTahunBerjalan
        Dim TotalSaldoJanuariEquity As Int64 = TotalSaldoJanuariModal + TotalSaldoJanuariLabaTahunBerjalan
        Dim TotalSaldoFebruariEquity As Int64 = TotalSaldoFebruariModal + TotalSaldoFebruariLabaTahunBerjalan
        Dim TotalSaldoMaretEquity As Int64 = TotalSaldoMaretModal + TotalSaldoMaretLabaTahunBerjalan
        Dim TotalSaldoAprilEquity As Int64 = TotalSaldoAprilModal + TotalSaldoAprilLabaTahunBerjalan
        Dim TotalSaldoMeiEquity As Int64 = TotalSaldoMeiModal + TotalSaldoMeiLabaTahunBerjalan
        Dim TotalSaldoJuniEquity As Int64 = TotalSaldoJuniModal + TotalSaldoJuniLabaTahunBerjalan
        Dim TotalSaldoJuliEquity As Int64 = TotalSaldoJuliModal + TotalSaldoJuliLabaTahunBerjalan
        Dim TotalSaldoAgustusEquity As Int64 = TotalSaldoAgustusModal + TotalSaldoAgustusLabaTahunBerjalan
        Dim TotalSaldoSeptemberEquity As Int64 = TotalSaldoSeptemberModal + TotalSaldoSeptemberLabaTahunBerjalan
        Dim TotalSaldoOktoberEquity As Int64 = TotalSaldoOktoberModal + TotalSaldoOktoberLabaTahunBerjalan
        Dim TotalSaldoNopemberEquity As Int64 = TotalSaldoNopemberModal + TotalSaldoNopemberLabaTahunBerjalan
        Dim TotalSaldoDesemberEquity As Int64 = TotalSaldoDesemberModal + TotalSaldoDesemberLabaTahunBerjalan
        DataTabelUtama.Rows.Add(
            "Total Equity", "",
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
        DataTabelUtama.Rows.Add()

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
        DataTabelUtama.Rows.Add(
            "TOTAL PASSIVA", "",
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
        DataTabelUtama.Rows.Add()

        DataTabelUtama.ClearSelection()

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
        Dim TahunNeraca = TahunBukuAktif
        Dim COA
        Dim NamaAkun
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
            DebetKreditCOA = dr.Item("D_K")
            SaldoAwal = dr.Item("Saldo_Awal")
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
            If COA < 40000 Then '( Jika COA termasuk Kelompok NERACA )
                DataTabelUtama.Rows.Add(
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
                If DebetKreditCOA = "DEBET" Then
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
                If DebetKreditCOA = "KREDIT" Then
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

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_TrialBalance_Click(sender As Object, e As EventArgs) Handles btn_TrialBalance.Click
        'BeginInvoke(Sub() frm_Laporan_TrialBalance.btn_Proses_Click(sender, e))
        'frm_Laporan_TrialBalance.JalurMasuk = Halaman_LAPORANNERACA
        'frm_Laporan_TrialBalance.Show()
    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub



    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class