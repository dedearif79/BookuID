Imports bcomm
Imports System.Data.Odbc

Public Class frm_DetailPembayaranPajak

    Public JenisPajak
    Dim JenisPPh
    Dim JudulForm

    Dim Index_BarisTabel
    Dim NomorUrut
    Dim NomorBPHP
    Dim NomorBulan
    Dim MasaPajak
    Dim JenisPajak_UntukTabel
    Dim NomorKetetapan
    Dim KodeSetoran
    Dim TanggalBayar
    Dim JumlahBayar
    Dim KodeNTPN
    Dim TWTL
    Dim NomorJV

    Dim Baris_Terseleksi
    Dim NomorUrut_Terseleksi
    Dim NomorJV_Terseleksi

    Dim QueryTampilan

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If JenisPajak = JenisPajak_KetetapanPajak Then
            DataTabelUtama.Columns("Jenis_Pajak").Visible = True
            DataTabelUtama.Columns("Nomor_Ketetapan").Visible = True
            DataTabelUtama.Columns("Kode_Setoran").Visible = False
            Me.Width = 734
        Else
            DataTabelUtama.Columns("Jenis_Pajak").Visible = False
            DataTabelUtama.Columns("Nomor_Ketetapan").Visible = False
            DataTabelUtama.Columns("Kode_Setoran").Visible = True
            Me.Width = 555
        End If

        Select Case JenisPajak
            Case JenisPajak_PPhPasal21
            Case JenisPajak_PPhPasal22_Lokal
            Case JenisPajak_PPhPasal23
            Case JenisPajak_PPhPasal42
            Case JenisPajak_PPhPasal25
            Case JenisPajak_PPhPasal26
            Case JenisPajak_PPhPasal29
            Case JenisPajak_PPN
            Case JenisPajak_PPN_Impor
            Case JenisPajak_KetetapanPajak
        End Select

        JenisPPh = KonversiJenisPajakKeJenisPPh(JenisPajak)

        JudulForm = "Detail Pembayaran " & JenisPajak & " - Tahun " & TahunBukuAktif
        Me.Text = JudulForm

        StyleTabelUtama(DataTabelUtama)

        If LevelUserAktif = LevelUser_99_AppDeveloper Then
            TampilkanData()
        Else
            FiturDalamPerbaikan()
            Close()
        End If

    End Sub

    Public Sub ResetForm()
        JenisPajak = Kosongan

    End Sub

    Sub TampilkanData()

        'Style Tabel :
        DataTabelUtama.Rows.Clear()

        'Data Tabel :
        Index_BarisTabel = 0
        NomorUrut = 0

        AksesDatabase_Transaksi(Buka)

        If JenisPajak = JenisPajak_KetetapanPajak Then
            QueryTampilan = " SELECT * FROM tbl_PembayaranHutangPajak " &
                " WHERE Nomor_BPHP LIKE '%" & AwalanBPKP & "%' " &
                " ORDER BY Tanggal_Bayar "
        Else
            QueryTampilan = " SELECT * FROM tbl_PembayaranHutangPajak " &
                " WHERE Jenis_Pajak = '" & JenisPajak & "' " &
                " ORDER BY Tanggal_Bayar "
        End If

        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr_ExecuteReader()

        Do While dr.Read
            NomorUrut += 1
            NomorBPHP = dr.Item("Nomor_BPHP")
            MasaPajak = dr.Item("Masa_Pajak")
            NomorBulan = AmbilAngka(KonversiBulanKeAngka(MasaPajak))
            JenisPajak_UntukTabel = dr.Item("Jenis_Pajak")
            KodeSetoran = dr.Item("Kode_Setoran")
            TanggalBayar = TanggalFormatTampilan(dr.Item("Tanggal_Bayar"))
            JumlahBayar = dr.Item("Jumlah_Bayar")
            KodeNTPN = dr.Item("NTPN")
            If JenisPajak = JenisPajak_KetetapanPajak Then
                Dim TanggalKetetapan_Date As Date
                cmdTELUSUR = New OdbcCommand(" SELECT Nomor_Ketetapan, Tanggal_Ketetapan " &
                                             " FROM tbl_KetetapanPajak " &
                                             " WHERE Nomor_BPHP = '" & NomorBPHP & "' ", KoneksiDatabaseTransaksi)
                drTELUSUR_ExecuteReader()
                drTELUSUR.Read()
                If drTELUSUR.HasRows Then
                    TanggalKetetapan_Date = drTELUSUR.Item("Tanggal_Ketetapan")
                    NomorKetetapan = drTELUSUR.Item("Nomor_Ketetapan")
                End If
                Dim TanggalJatuhTempo_Date As Date = TanggalKetetapan_Date.AddDays(30)
                Dim TanggalBayar_Date As Date = TanggalBayar
                If TanggalBayar_Date > TanggalJatuhTempo_Date Then
                    TWTL = TWTL_Terlambat
                Else
                    TWTL = TWTL_TepatWaktu
                End If
            Else
                NomorKetetapan = Kosongan
                TWTL = LogikaTWTL(JenisPajak, NomorBulan, TahunPajak, TanggalBayar)
            End If
            NomorJV = dr.Item("Nomor_JV")
            DataTabelUtama.Rows.Add(NomorUrut, MasaPajak, JenisPajak_UntukTabel, NomorKetetapan, KodeSetoran, TanggalBayar, JumlahBayar, KodeNTPN, TWTL, NomorJV)
            If TWTL = TWTL_Terlambat Then PengaturWaranaCell_Huruf(DataTabelUtama, "TW_TL", Index_BarisTabel, WarnaMerahSolid)
            Index_BarisTabel += 1
        Loop

        AksesDatabase_Transaksi(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        Baris_Terseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_LihatJurnal.Enabled = False
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        If NomorJV_Terseleksi > 0 Then
            LihatJurnal(NomorJV_Terseleksi)
        Else
            MsgBox("Data terpilih BELUM masuk JURNAL.")
            Return
        End If
    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub

    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        Baris_Terseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_Urut", Baris_Terseleksi).Value)
        NomorJV_Terseleksi = DataTabelUtama.Item("Nomor_JV", Baris_Terseleksi).Value

        If NomorUrut_Terseleksi > 0 Then
            btn_LihatJurnal.Enabled = True
        Else
            btn_LihatJurnal.Enabled = False
        End If

    End Sub

    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        If Baris_Terseleksi < 0 Then Return
        btn_LihatJurnal_Click(sender, e)
    End Sub

End Class