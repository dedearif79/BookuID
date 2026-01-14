Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_LaporanLabaRugi_Tahunan

    Public StatusAktif As Boolean = False
    Private SudahDimuat As Boolean = False

    Public JudulForm

    Dim QueryTampilan

    Dim TahunLabaRugi = TahunBukuAktif
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
    Dim SaldoKeseluruhan As Int64

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
    Dim TotalSaldoKeseluruhan As Int64

    Dim COA_Terseleksi

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If SudahDimuat Then Return
        StatusAktif = True

        ProsesLoadingForm = True

        'lbl_JudulForm.Text = frm_LaporanLabaRugi_Tahunan.JudulForm


        ProsesLoadingForm = False

        RefreshTampilanData()

        SudahDimuat = True
    End Sub



    Sub RefreshTampilanData()

        If StatusTrialBalance = True Then
            TampilkanData()
        Else
            Pesan_Peringatan("Telah terjadi perubahan data pada Jurnal." &
            Enter2Baris & "Silakan klik tombol 'Trial Balance' terlebih dahulu.")
            Return
        End If
    End Sub




    Sub TampilkanData()

        KetersediaanMenuHalaman(pnl_Halaman, False)

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        Dim QueryTampilanUmum = " SELECT * FROM tbl_COA WHERE Visibilitas = '" & Pilihan_Ya & "' "

        '-----------------------------------------------------------
        'Pendapatan
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("Pendapatan")

        'Penjualan :
        'DataGridView.Rows.Add("Penjualan")
        QueryTampilan = QueryTampilanUmum & " AND COA LIKE '4%' "
        DataPerKategoriCOA()

        'Total Pendapatan
        Dim TotalSaldoJanuariPendapatan As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariPendapatan As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretPendapatan As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilPendapatan As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiPendapatan As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniPendapatan As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliPendapatan As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusPendapatan As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberPendapatan As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberPendapatan As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberPendapatan As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberPendapatan As Int64 = TotalSaldoDesember
        Dim TotalSaldoKeseluruhanPendapatan As Int64 = TotalSaldoKeseluruhan
        datatabelUtama.Rows.Add(
            "Total Pendapatan", "",
            TotalSaldoJanuariPendapatan,
            TotalSaldoFebruariPendapatan,
            TotalSaldoMaretPendapatan,
            TotalSaldoAprilPendapatan,
            TotalSaldoMeiPendapatan,
            TotalSaldoJuniPendapatan,
            TotalSaldoJuliPendapatan,
            TotalSaldoAgustusPendapatan,
            TotalSaldoSeptemberPendapatan,
            TotalSaldoOktoberPendapatan,
            TotalSaldoNopemberPendapatan,
            TotalSaldoDesemberPendapatan,
            TotalSaldoKeseluruhanPendapatan)
        'datatabelUtama.Rows.Add()

        '-----------------------------------------------------------
        'Harga Pokok Produksi
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("Harga Pokok Penjualan", "")

        'QueryTampilan = QueryTampilanUmum & " AND COA LIKE '5%' " &
        '    " AND COA <> '" & KodeTautanCOA_HargaPokokPenjualan & "' "
        QueryTampilan = QueryTampilanUmum & " AND COA = '" & KodeTautanCOA_HargaPokokPenjualan & "' "

        'Total Beban Pokok Penjualan
        Dim TotalSaldoJanuariBebanPokokPenjualan As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariBebanPokokPenjualan As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretBebanPokokPenjualan As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilBebanPokokPenjualan As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiBebanPokokPenjualan As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniBebanPokokPenjualan As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliBebanPokokPenjualan As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusBebanPokokPenjualan As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberBebanPokokPenjualan As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberBebanPokokPenjualan As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberBebanPokokPenjualan As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberBebanPokokPenjualan As Int64 = TotalSaldoDesember
        Dim TotalSaldoKeseluruhanBebanPokokPenjualan As Int64 = TotalSaldoKeseluruhan
        datatabelUtama.Rows.Add(
            "Total Beban Pokok Penjualan", "",
            TotalSaldoJanuariBebanPokokPenjualan,
            TotalSaldoFebruariBebanPokokPenjualan,
            TotalSaldoMaretBebanPokokPenjualan,
            TotalSaldoAprilBebanPokokPenjualan,
            TotalSaldoMeiBebanPokokPenjualan,
            TotalSaldoJuniBebanPokokPenjualan,
            TotalSaldoJuliBebanPokokPenjualan,
            TotalSaldoAgustusBebanPokokPenjualan,
            TotalSaldoSeptemberBebanPokokPenjualan,
            TotalSaldoOktoberBebanPokokPenjualan,
            TotalSaldoNopemberBebanPokokPenjualan,
            TotalSaldoDesemberBebanPokokPenjualan,
            TotalSaldoKeseluruhanBebanPokokPenjualan)
        'datatabelUtama.Rows.Add()

        'Laba/Rugi Bruto
        Dim LabaRugiBrutoJanuari As Int64 = TotalSaldoJanuariPendapatan - TotalSaldoJanuariBebanPokokPenjualan
        Dim LabaRugiBrutoFebruari As Int64 = TotalSaldoFebruariPendapatan - TotalSaldoFebruariBebanPokokPenjualan
        Dim LabaRugiBrutoMaret As Int64 = TotalSaldoMaretPendapatan - TotalSaldoMaretBebanPokokPenjualan
        Dim LabaRugiBrutoApril As Int64 = TotalSaldoAprilPendapatan - TotalSaldoAprilBebanPokokPenjualan
        Dim LabaRugiBrutoMei As Int64 = TotalSaldoMeiPendapatan - TotalSaldoMeiBebanPokokPenjualan
        Dim LabaRugiBrutoJuni As Int64 = TotalSaldoJuniPendapatan - TotalSaldoJuniBebanPokokPenjualan
        Dim LabaRugiBrutoJuli As Int64 = TotalSaldoJuliPendapatan - TotalSaldoJuliBebanPokokPenjualan
        Dim LabaRugiBrutoAgustus As Int64 = TotalSaldoAgustusPendapatan - TotalSaldoAgustusBebanPokokPenjualan
        Dim LabaRugiBrutoSeptember As Int64 = TotalSaldoSeptemberPendapatan - TotalSaldoSeptemberBebanPokokPenjualan
        Dim LabaRugiBrutoOktober As Int64 = TotalSaldoOktoberPendapatan - TotalSaldoOktoberBebanPokokPenjualan
        Dim LabaRugiBrutoNopember As Int64 = TotalSaldoNopemberPendapatan - TotalSaldoNopemberBebanPokokPenjualan
        Dim LabaRugiBrutoDesember As Int64 = TotalSaldoDesemberPendapatan - TotalSaldoDesemberBebanPokokPenjualan
        Dim TotalLabaRugiBruto As Int64 = TotalSaldoKeseluruhanPendapatan - TotalSaldoKeseluruhanBebanPokokPenjualan
        datatabelUtama.Rows.Add(
            "Laba/Rugi Bruto", "",
            LabaRugiBrutoJanuari,
            LabaRugiBrutoFebruari,
            LabaRugiBrutoMaret,
            LabaRugiBrutoApril,
            LabaRugiBrutoMei,
            LabaRugiBrutoJuni,
            LabaRugiBrutoJuli,
            LabaRugiBrutoAgustus,
            LabaRugiBrutoSeptember,
            LabaRugiBrutoOktober,
            LabaRugiBrutoNopember,
            LabaRugiBrutoDesember,
            TotalLabaRugiBruto)
        'datatabelUtama.Rows.Add()

        '-----------------------------------------------------------
        'Beban Usaha
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("Beban Usaha")

        'Penjualan :
        'DataGridView.Rows.Add("Penjualan")
        QueryTampilan = QueryTampilanUmum & " AND COA LIKE '6%' "
        DataPerKategoriCOA()

        'Total BebanUsaha
        Dim TotalSaldoJanuariBebanUsaha As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariBebanUsaha As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretBebanUsaha As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilBebanUsaha As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiBebanUsaha As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniBebanUsaha As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliBebanUsaha As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusBebanUsaha As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberBebanUsaha As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberBebanUsaha As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberBebanUsaha As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberBebanUsaha As Int64 = TotalSaldoDesember
        Dim TotalSaldoKeseluruhanBebanUsaha As Int64 = TotalSaldoKeseluruhan
        datatabelUtama.Rows.Add(
            "Total Beban Usaha", "",
            TotalSaldoJanuariBebanUsaha,
            TotalSaldoFebruariBebanUsaha,
            TotalSaldoMaretBebanUsaha,
            TotalSaldoAprilBebanUsaha,
            TotalSaldoMeiBebanUsaha,
            TotalSaldoJuniBebanUsaha,
            TotalSaldoJuliBebanUsaha,
            TotalSaldoAgustusBebanUsaha,
            TotalSaldoSeptemberBebanUsaha,
            TotalSaldoOktoberBebanUsaha,
            TotalSaldoNopemberBebanUsaha,
            TotalSaldoDesemberBebanUsaha,
            TotalSaldoKeseluruhanBebanUsaha)
        'datatabelUtama.Rows.Add()

        'Laba/Rugi Usaha
        Dim LabaRugiUsahaJanuari As Int64 = LabaRugiBrutoJanuari - TotalSaldoJanuariBebanUsaha
        Dim LabaRugiUsahaFebruari As Int64 = LabaRugiBrutoFebruari - TotalSaldoFebruariBebanUsaha
        Dim LabaRugiUsahaMaret As Int64 = LabaRugiBrutoMaret - TotalSaldoMaretBebanUsaha
        Dim LabaRugiUsahaApril As Int64 = LabaRugiBrutoApril - TotalSaldoAprilBebanUsaha
        Dim LabaRugiUsahaMei As Int64 = LabaRugiBrutoMei - TotalSaldoMeiBebanUsaha
        Dim LabaRugiUsahaJuni As Int64 = LabaRugiBrutoJuni - TotalSaldoJuniBebanUsaha
        Dim LabaRugiUsahaJuli As Int64 = LabaRugiBrutoJuli - TotalSaldoJuliBebanUsaha
        Dim LabaRugiUsahaAgustus As Int64 = LabaRugiBrutoAgustus - TotalSaldoAgustusBebanUsaha
        Dim LabaRugiUsahaSeptember As Int64 = LabaRugiBrutoSeptember - TotalSaldoSeptemberBebanUsaha
        Dim LabaRugiUsahaOktober As Int64 = LabaRugiBrutoOktober - TotalSaldoOktoberBebanUsaha
        Dim LabaRugiUsahaNopember As Int64 = LabaRugiBrutoNopember - TotalSaldoNopemberBebanUsaha
        Dim LabaRugiUsahaDesember As Int64 = LabaRugiBrutoDesember - TotalSaldoDesemberBebanUsaha
        Dim TotalLabaRugiUsaha As Int64 = TotalLabaRugiBruto - TotalSaldoKeseluruhanBebanUsaha
        datatabelUtama.Rows.Add(
            "Laba/Rugi Usaha", "",
            LabaRugiUsahaJanuari,
            LabaRugiUsahaFebruari,
            LabaRugiUsahaMaret,
            LabaRugiUsahaApril,
            LabaRugiUsahaMei,
            LabaRugiUsahaJuni,
            LabaRugiUsahaJuli,
            LabaRugiUsahaAgustus,
            LabaRugiUsahaSeptember,
            LabaRugiUsahaOktober,
            LabaRugiUsahaNopember,
            LabaRugiUsahaDesember,
            TotalLabaRugiUsaha)
        'datatabelUtama.Rows.Add()

        '-----------------------------------------------------------
        'Pendapatan di Luar Usaha
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("Pendapatan di Luar Usaha")

        'Penjualan :
        'DataGridView.Rows.Add("Penjualan")
        QueryTampilan = QueryTampilanUmum & " AND COA LIKE '7%' "
        DataPerKategoriCOA()

        'Total Pendapatan di Luar Usaha
        Dim TotalSaldoJanuariPendapatanDiLuarUsaha As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariPendapatanDiLuarUsaha As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretPendapatanDiLuarUsaha As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilPendapatanDiLuarUsaha As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiPendapatanDiLuarUsaha As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniPendapatanDiLuarUsaha As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliPendapatanDiLuarUsaha As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusPendapatanDiLuarUsaha As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberPendapatanDiLuarUsaha As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberPendapatanDiLuarUsaha As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberPendapatanDiLuarUsaha As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberPendapatanDiLuarUsaha As Int64 = TotalSaldoDesember
        Dim TotalSaldoKeseluruhanPendapatanDiLuarUsaha As Int64 = TotalSaldoKeseluruhan
        datatabelUtama.Rows.Add(
            "Total Pendapatan di Luar Usaha", "",
            TotalSaldoJanuariPendapatanDiLuarUsaha,
            TotalSaldoFebruariPendapatanDiLuarUsaha,
            TotalSaldoMaretPendapatanDiLuarUsaha,
            TotalSaldoAprilPendapatanDiLuarUsaha,
            TotalSaldoMeiPendapatanDiLuarUsaha,
            TotalSaldoJuniPendapatanDiLuarUsaha,
            TotalSaldoJuliPendapatanDiLuarUsaha,
            TotalSaldoAgustusPendapatanDiLuarUsaha,
            TotalSaldoSeptemberPendapatanDiLuarUsaha,
            TotalSaldoOktoberPendapatanDiLuarUsaha,
            TotalSaldoNopemberPendapatanDiLuarUsaha,
            TotalSaldoDesemberPendapatanDiLuarUsaha,
            TotalSaldoKeseluruhanPendapatanDiLuarUsaha)
        'datatabelUtama.Rows.Add()


        '-----------------------------------------------------------
        'Biaya di Luar Usaha
        '-----------------------------------------------------------
        datatabelUtama.Rows.Add("Biaya di Luar Usaha")

        'Penjualan :
        'DataGridView.Rows.Add("Penjualan")
        QueryTampilan = QueryTampilanUmum & " AND COA LIKE '8%' "
        DataPerKategoriCOA()

        'Total Biaya di Luar Usaha
        Dim TotalSaldoJanuariBiayaDiLuarUsaha As Int64 = TotalSaldoJanuari
        Dim TotalSaldoFebruariBiayaDiLuarUsaha As Int64 = TotalSaldoFebruari
        Dim TotalSaldoMaretBiayaDiLuarUsaha As Int64 = TotalSaldoMaret
        Dim TotalSaldoAprilBiayaDiLuarUsaha As Int64 = TotalSaldoApril
        Dim TotalSaldoMeiBiayaDiLuarUsaha As Int64 = TotalSaldoMei
        Dim TotalSaldoJuniBiayaDiLuarUsaha As Int64 = TotalSaldoJuni
        Dim TotalSaldoJuliBiayaDiLuarUsaha As Int64 = TotalSaldoJuli
        Dim TotalSaldoAgustusBiayaDiLuarUsaha As Int64 = TotalSaldoAgustus
        Dim TotalSaldoSeptemberBiayaDiLuarUsaha As Int64 = TotalSaldoSeptember
        Dim TotalSaldoOktoberBiayaDiLuarUsaha As Int64 = TotalSaldoOktober
        Dim TotalSaldoNopemberBiayaDiLuarUsaha As Int64 = TotalSaldoNopember
        Dim TotalSaldoDesemberBiayaDiLuarUsaha As Int64 = TotalSaldoDesember
        Dim TotalSaldoKeseluruhanBiayaDiLuarUsaha As Int64 = TotalSaldoKeseluruhan
        datatabelUtama.Rows.Add(
            "Total Biaya di Luar Usaha", "",
            TotalSaldoJanuariBiayaDiLuarUsaha,
            TotalSaldoFebruariBiayaDiLuarUsaha,
            TotalSaldoMaretBiayaDiLuarUsaha,
            TotalSaldoAprilBiayaDiLuarUsaha,
            TotalSaldoMeiBiayaDiLuarUsaha,
            TotalSaldoJuniBiayaDiLuarUsaha,
            TotalSaldoJuliBiayaDiLuarUsaha,
            TotalSaldoAgustusBiayaDiLuarUsaha,
            TotalSaldoSeptemberBiayaDiLuarUsaha,
            TotalSaldoOktoberBiayaDiLuarUsaha,
            TotalSaldoNopemberBiayaDiLuarUsaha,
            TotalSaldoDesemberBiayaDiLuarUsaha,
            TotalSaldoKeseluruhanBiayaDiLuarUsaha)
        'datatabelUtama.Rows.Add()

        'Laba/Rugi Sebelum Pajak
        Dim LabaRugiBersihJanuari As Int64 = LabaRugiUsahaJanuari + TotalSaldoJanuariPendapatanDiLuarUsaha - TotalSaldoJanuariBiayaDiLuarUsaha
        Dim LabaRugiBersihFebruari As Int64 = LabaRugiUsahaFebruari + TotalSaldoFebruariPendapatanDiLuarUsaha - TotalSaldoFebruariBiayaDiLuarUsaha
        Dim LabaRugiBersihMaret As Int64 = LabaRugiUsahaMaret + TotalSaldoMaretPendapatanDiLuarUsaha - TotalSaldoMaretBiayaDiLuarUsaha
        Dim LabaRugiBersihApril As Int64 = LabaRugiUsahaApril + TotalSaldoAprilPendapatanDiLuarUsaha - TotalSaldoAprilBiayaDiLuarUsaha
        Dim LabaRugiBersihMei As Int64 = LabaRugiUsahaMei + TotalSaldoMeiPendapatanDiLuarUsaha - TotalSaldoMeiBiayaDiLuarUsaha
        Dim LabaRugiBersihJuni As Int64 = LabaRugiUsahaJuni + TotalSaldoJuniPendapatanDiLuarUsaha - TotalSaldoJuniBiayaDiLuarUsaha
        Dim LabaRugiBersihJuli As Int64 = LabaRugiUsahaJuli + TotalSaldoJuliPendapatanDiLuarUsaha - TotalSaldoJuliBiayaDiLuarUsaha
        Dim LabaRugiBersihAgustus As Int64 = LabaRugiUsahaAgustus + TotalSaldoAgustusPendapatanDiLuarUsaha - TotalSaldoAgustusBiayaDiLuarUsaha
        Dim LabaRugiBersihSeptember As Int64 = LabaRugiUsahaSeptember + TotalSaldoSeptemberPendapatanDiLuarUsaha - TotalSaldoSeptemberBiayaDiLuarUsaha
        Dim LabaRugiBersihOktober As Int64 = LabaRugiUsahaOktober + TotalSaldoOktoberPendapatanDiLuarUsaha - TotalSaldoOktoberBiayaDiLuarUsaha
        Dim LabaRugiBersihNopember As Int64 = LabaRugiUsahaNopember + TotalSaldoNopemberPendapatanDiLuarUsaha - TotalSaldoNopemberBiayaDiLuarUsaha
        Dim LabaRugiBersihDesember As Int64 = LabaRugiUsahaDesember + TotalSaldoDesemberPendapatanDiLuarUsaha - TotalSaldoDesemberBiayaDiLuarUsaha
        Dim TotalLabaRugiBersih As Int64 = TotalLabaRugiUsaha + TotalSaldoKeseluruhanPendapatanDiLuarUsaha - TotalSaldoKeseluruhanBiayaDiLuarUsaha
        datatabelUtama.Rows.Add(
            "Laba/Rugi Sebelum Pajak", "",
            LabaRugiBersihJanuari,
            LabaRugiBersihFebruari,
            LabaRugiBersihMaret,
            LabaRugiBersihApril,
            LabaRugiBersihMei,
            LabaRugiBersihJuni,
            LabaRugiBersihJuli,
            LabaRugiBersihAgustus,
            LabaRugiBersihSeptember,
            LabaRugiBersihOktober,
            LabaRugiBersihNopember,
            LabaRugiBersihDesember,
            TotalLabaRugiBersih)
        'datatabelUtama.Rows.Add()

        BersihkanSeleksi()

    End Sub

    Sub DataPerKategoriCOA()

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
        TotalSaldoKeseluruhan = 0

        'Data Tabel :
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
            SaldoKeseluruhan = SaldoJanuari + SaldoFebruari + SaldoMaret + SaldoApril + SaldoMei + SaldoJuni + SaldoJuli + SaldoAgustus + SaldoSeptember + SaldoOktober + SaldoNopember + SaldoDesember
            If SaldoKeseluruhan <> 0 Then
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
            TotalSaldoAwal = TotalSaldoAwal + SaldoAwal
            TotalSaldoJanuari = TotalSaldoJanuari + SaldoJanuari
            TotalSaldoFebruari = TotalSaldoFebruari + SaldoFebruari
            TotalSaldoMaret = TotalSaldoMaret + SaldoMaret
            TotalSaldoApril = TotalSaldoApril + SaldoApril
            TotalSaldoMei = TotalSaldoMei + SaldoMei
            TotalSaldoJuni = TotalSaldoJuni + SaldoJuni
            TotalSaldoJuli = TotalSaldoJuli + SaldoJuli
            TotalSaldoAgustus = TotalSaldoAgustus + SaldoAgustus
            TotalSaldoSeptember = TotalSaldoSeptember + SaldoSeptember
            TotalSaldoOktober = TotalSaldoOktober + SaldoOktober
            TotalSaldoNopember = TotalSaldoNopember + SaldoNopember
            TotalSaldoDesember = TotalSaldoDesember + SaldoDesember
            TotalSaldoKeseluruhan = TotalSaldoKeseluruhan + SaldoKeseluruhan
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
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub




    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub


    Private Sub btn_BukuBesar_Click(sender As Object, e As RoutedEventArgs) Handles btn_BukuBesar.Click
        BukaHalamanBukuBesar(COA_Terseleksi)
    End Sub


    'Private Sub btn_TrialBalance_Click(sender As Object, e As RoutedEventArgs) Handles btn_TrialBalance.Click
    '    win_BOOKU.BukaModul_LaporanTrialBalance()
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
            e.Row.FontWeight = FontWeights.Bold
            e.Row.Foreground = WarnaTegas_WPF
        Else
            If AmbilAngka(e.Row.Item("Kode_Akun")) = 0 Then
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

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
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