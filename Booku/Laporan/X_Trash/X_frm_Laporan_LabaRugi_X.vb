Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_Laporan_LabaRugi_X

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
    Dim TotalSaldoKeseluruhan As Int64

    Private Sub frm_Laporan_LabaRugi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lbl_Judul.Text = "Laporan Laba Rugi - Tahun " & TahunBukuAktif

        DataTabelUtama.Rows.Clear() 'Sengaja tabel dihapus dulu semua. Untuk mencegah missdata, saat terjadi perubahan data Jurnal.
        If StatusTrialBalance = True Then TampilkanData()

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
        'Pendapatan
        '-----------------------------------------------------------
        DataTabelUtama.Rows.Add("Pendapatan")

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
        DataTabelUtama.Rows.Add(
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
        DataTabelUtama.Rows.Add()

        '-----------------------------------------------------------
        'Harga Pokok Produksi
        '-----------------------------------------------------------
        DataTabelUtama.Rows.Add("Harga Pokok Penjualan", "")

        QueryTampilan = QueryTampilanUmum & " AND COA LIKE '5%' "
        DataPerKategoriCOA()

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
        DataTabelUtama.Rows.Add(
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
        DataTabelUtama.Rows.Add()

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
        DataTabelUtama.Rows.Add(
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
        DataTabelUtama.Rows.Add()

        '-----------------------------------------------------------
        'Beban Usaha
        '-----------------------------------------------------------
        DataTabelUtama.Rows.Add("Beban Usaha")

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
        DataTabelUtama.Rows.Add(
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
        DataTabelUtama.Rows.Add()

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
        DataTabelUtama.Rows.Add(
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
        DataTabelUtama.Rows.Add()

        '-----------------------------------------------------------
        'Pendapatan di Luar Usaha
        '-----------------------------------------------------------
        DataTabelUtama.Rows.Add("Pendapatan di Luar Usaha")

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
        DataTabelUtama.Rows.Add(
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
        DataTabelUtama.Rows.Add()


        '-----------------------------------------------------------
        'Biaya di Luar Usaha
        '-----------------------------------------------------------
        DataTabelUtama.Rows.Add("Biaya di Luar Usaha")

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
        DataTabelUtama.Rows.Add(
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
        DataTabelUtama.Rows.Add()

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
        DataTabelUtama.Rows.Add(
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
        DataTabelUtama.Rows.Add()

        DataTabelUtama.ClearSelection()

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
            DataTabelUtama.Rows.Add(
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

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_TrialBalance_Click(sender As Object, e As EventArgs) Handles btn_TrialBalance.Click
        'BeginInvoke(Sub() frm_Laporan_TrialBalance.btn_Proses_Click(sender, e))
        'frm_Laporan_TrialBalance.JalurMasuk = Halaman_LAPORANLABARUGI
        'frm_Laporan_TrialBalance.Show()
    End Sub

    Private Sub cmb_TahunTrialBalance_SelectedIndexChanged(sender As Object, e As EventArgs)
        'TampilkanData()
    End Sub
    Private Sub cmb_TahunTrialBalance_TextChanged(sender As Object, e As EventArgs)
        'TampilkanData()
    End Sub
    Private Sub cmb_TahunTrialBalance_KeyPress(sender As Object, e As KeyPressEventArgs)
        KunciTotalInputan(sender, e)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub



    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class