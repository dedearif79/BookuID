Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_BukuDisposalAssetTetap

    Dim BarisTerseleksi
    Public KesesuaianJurnal As Boolean

    Dim NomorUrut
    Dim NomorBeritaAcara
    Dim TanggalBeritaAcara
    Dim KodeAsset
    Dim NamaAsset
    Dim JumlahAsset
    Dim TanggalPerolehan
    Dim HargaPerolehan
    Dim AkumulasiPenyusutan
    Dim LabaRugi
    Dim Keterangan
    Dim NomorJV_Closing

    Dim NomorUrut_Terseleksi
    Dim NomorBeritaAcara_Terseleksi
    Dim TanggalBeritaAcara_Terseleksi
    Dim KodeAsset_Terseleksi
    Dim NamaAsset_Terseleksi
    Dim JumlahAsset_Terseleksi
    Dim TanggalPerolehan_Terseleksi
    Dim HargaPerolehan_Terseleksi
    Dim AkumulasiPenyusutan_Terseleksi
    Dim LabaRugi_Terseleksi
    Dim Keterangan_Terseleksi
    Dim NomorJV_Closing_Terseleksi

    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Style_HalamanModul(Me)

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Sub RefreshTampilanData()
        TampilkanData()
    End Sub

    Sub TampilkanData()

        KesesuaianJurnal = True

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Data Tabel :
        NomorUrut = 0

        AksesDatabase_General(Buka)

        cmd = New OdbcCommand(" SELECT * FROM tbl_DataAsset " &
                              " WHERE Tanggal_Closing <> '" & TanggalKosongSimpan & "' " &
                              " AND Harga_Jual = 0 " &
                              " ORDER BY Nomor_JV_Closing ", KoneksiDatabaseGeneral)
        dr_ExecuteReader()

        Do While dr.Read

            NomorUrut += 1
            NomorBeritaAcara = dr.Item("Kode_Closing")
            TanggalBeritaAcara = TanggalFormatTampilan(dr.Item("Tanggal_Closing"))
            KodeAsset = dr.Item("Kode_Asset")
            NamaAsset = dr.Item("Nama_Aktiva")
            JumlahAsset = 1
            TanggalPerolehan = TanggalFormatTampilan(dr.Item("Tanggal_Perolehan"))
            HargaPerolehan = dr.Item("Harga_Perolehan")
            AkumulasiPenyusutan = dr.Item("Akumulasi_Penyusutan")
            LabaRugi = AkumulasiPenyusutan - HargaPerolehan
            Keterangan = dr.Item("Keterangan")
            NomorJV_Closing = dr.Item("Nomor_JV_Closing")

            If HargaPerolehan = 0 Then HargaPerolehan = StripKosong
            If AkumulasiPenyusutan = 0 Then AkumulasiPenyusutan = StripKosong
            If LabaRugi = 0 Then LabaRugi = StripKosong

            DataTabelUtama.Rows.Add(NomorUrut, NomorBeritaAcara, TanggalBeritaAcara, KodeAsset, NamaAsset, JumlahAsset,
                                    TanggalPerolehan, HargaPerolehan, AkumulasiPenyusutan, LabaRugi, Keterangan, NomorJV_Closing)

        Loop
        AksesDatabase_General(Tutup)

        BersihkanSeleksi()

    End Sub

    Sub BersihkanSeleksi()
        BarisTerseleksi = -1
        DataTabelUtama.ClearSelection()
        btn_LihatJurnal.Enabled = False
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_LihatJurnal_Click(sender As Object, e As EventArgs) Handles btn_LihatJurnal.Click
        LihatJurnal(NomorJV_Closing_Terseleksi)
    End Sub


    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub



    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick
        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        NomorUrut_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_Urut", BarisTerseleksi).Value)
        NomorBeritaAcara_Terseleksi = DataTabelUtama("Nomor_Berita_Acara", BarisTerseleksi).Value
        TanggalBeritaAcara_Terseleksi = DataTabelUtama("Tanggal_Berita_Acara", BarisTerseleksi).Value
        KodeAsset_Terseleksi = DataTabelUtama("Kode_Asset", BarisTerseleksi).Value
        NamaAsset_Terseleksi = DataTabelUtama("Nama_Asset", BarisTerseleksi).Value
        JumlahAsset_Terseleksi = AmbilAngka(DataTabelUtama("Jumlah_Asset", BarisTerseleksi).Value)
        TanggalPerolehan_Terseleksi = AmbilAngka(DataTabelUtama("Tanggal_Perolehan", BarisTerseleksi).Value)
        HargaPerolehan_Terseleksi = AmbilAngka(DataTabelUtama("Harga_Perolehan", BarisTerseleksi).Value)
        AkumulasiPenyusutan_Terseleksi = AmbilAngka(DataTabelUtama("Akumulasi_Penyusutan", BarisTerseleksi).Value)
        LabaRugi_Terseleksi = AmbilAngka(DataTabelUtama("Laba_Rugi", BarisTerseleksi).Value)
        Keterangan_Terseleksi = DataTabelUtama("Keterangan_", BarisTerseleksi).Value
        NomorJV_Closing_Terseleksi = AmbilAngka(DataTabelUtama("Nomor_JV_Closing", BarisTerseleksi).Value)

        If NomorJV_Closing_Terseleksi > 0 Then
            btn_LihatJurnal.Enabled = True
        Else
            btn_LihatJurnal.Enabled = False
        End If

    End Sub
    Private Sub DataTabelUtama_DoubleClick(sender As Object, e As EventArgs) Handles DataTabelUtama.DoubleClick
        If DataTabelUtama.RowCount = 0 Then Return
        btn_LihatJurnal_Click(sender, e)
    End Sub


    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class