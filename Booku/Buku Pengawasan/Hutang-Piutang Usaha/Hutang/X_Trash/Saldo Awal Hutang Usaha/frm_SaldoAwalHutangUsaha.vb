Imports bcomm
Imports System.Data.Odbc

Public Class frm_SaldoAwalHutangUsaha

    Dim JudulForm
    Dim QueryTampilan
    Dim FilterData
    Public BarisTerseleksi
    Dim TotalSaldoAwalHutangUsaha As Int64
    Dim TotalSaldoAkhirHutangUsaha As Int64
    Dim SaldoAwalCOAHutangUsaha As Int64
    Dim SaldoAkhirCOAHutangUsaha As Int64
    Dim SaldoAwalCOAHutangUsahaPlusPenyesuaian As Int64
    Dim JumlahPenyesuaianHutangUsaha As Int64
    Public KesesuaianSaldoAwal As Boolean
    Public KesesuaianSaldoAkhir As Boolean

    Private Sub frm_SaldoAwalHutangUsaha_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                JudulForm = "Saldo Akhir Hutang Usaha - Tahun " & TahunBukuAktif
                lbl_TotalSaldo.Text = "Total Saldo Akhir Hutang Usaha"
                lbl_SaldoCOA_PlusAJP.Text = "Saldo Akhir COA Hutang Usaha"
                lbl_SaldoAwalCOA.Visible = False
                txt_SaldoAwalCOAHutangUsaha.Visible = False
                lbl_AJP.Visible = False
                txt_JumlahPenyesuaianHutangUsaha.Visible = False
            Case JenisTahunBuku_NORMAL
                JudulForm = "Saldo Awal Hutang Usaha - Tahun " & TahunBukuAktif
                lbl_TotalSaldo.Text = "Total Saldo Awal Hutang Usaha"
                lbl_SaldoCOA_PlusAJP.Text = "Saldo Awal COA Hutang Usaha + AJP"
                lbl_SaldoAwalCOA.Visible = True
                txt_SaldoAwalCOAHutangUsaha.Visible = True
                lbl_AJP.Visible = True
                txt_JumlahPenyesuaianHutangUsaha.Visible = True
        End Select

        Me.Text = JudulForm
        lbl_JudulForm.Text = JudulForm

        RefreshTampilanData()

    End Sub

    Sub RefreshTampilanData()

        TampilkanData()

    End Sub

    Sub TampilkanData()

        'Filter Pencarian :
        Dim FilterPencarian = " "
        If txt_Pencarian.Text = Nothing Then
            FilterPencarian = " "
        Else
            Dim Srch = txt_Pencarian.Text
            Dim clm_NamaBarang = " Nama_Barang LIKE '%" & Srch & "%' "
            Dim clm_NamaJasa = " OR Nama_Jasa LIKE '%" & Srch & "%' "
            FilterPencarian = " AND ( " & clm_NamaBarang & clm_NamaJasa & " ) "
        End If


        'Query Tampilan :
        FilterData = FilterPencarian
        QueryTampilan = " SELECT * FROM tbl_SisaHutangUsaha WHERE Kode_Supplier <> 'X' " & FilterData

        'Style Tabel :
        DataGridView.Rows.Clear()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan

        'Data Tabel :
        Dim NomorUrut = 0
        Dim NomorPembelian
        Dim Referensi
        Dim TanggalInvoice
        Dim NomorInvoice
        Dim TanggalFakturPajak
        Dim NomorFakturPajak
        Dim KodeSupplier
        Dim NamaSupplier
        Dim DPP
        Dim PPN
        Dim NamaBarang
        Dim NamaJasa
        Dim PPhDipotong
        Dim JumlahTagihan
        Dim JumlahBayar
        Dim SisaHutang
        TotalSaldoAwalHutangUsaha = 0
        Dim DueDate
        Dim Keterangan
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan & " ORDER BY Tanggal_Invoice ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut = NomorUrut + 1
            NomorPembelian = dr.Item("Nomor_Pembelian")
            Referensi = dr.Item("Referensi")
            TanggalInvoice = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Invoice"), 10)
            NomorInvoice = dr.Item("Nomor_Invoice")
            TanggalFakturPajak = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Faktur_Pajak"), 10)
            If TanggalFakturPajak = TanggalKosong Then TanggalFakturPajak = StripKosong
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            KodeSupplier = dr.Item("Kode_Supplier")
            NamaSupplier = dr.Item("Nama_Supplier")
            NamaBarang = dr.Item("Nama_Barang")
            NamaJasa = dr.Item("Nama_Jasa")
            DPP = dr.Item("DPP")
            PPN = dr.Item("PPN")
            PPhDipotong = dr.Item("PPh_Dipotong")
            JumlahTagihan = dr.Item("Jumlah_Tagihan")
            Dim cmdBAYAR = New OdbcCommand(" SELECT * FROM tbl_PembayaranHutangUsaha WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi)
            Dim drBAYAR = cmdBAYAR.ExecuteReader
            drBAYAR.Read()
            JumlahBayar = drBAYAR.Item("Jumlah_Bayar")
            SisaHutang = JumlahTagihan - JumlahBayar
            TotalSaldoAwalHutangUsaha = TotalSaldoAwalHutangUsaha + SisaHutang
            DueDate = Microsoft.VisualBasic.Left(dr.Item("Due_Date"), 10)
            Keterangan = dr.Item("Keterangan")
            DataGridView.Rows.Add(NomorUrut, NomorPembelian, Referensi, _
                                  TanggalInvoice, NomorInvoice, _
                                  TanggalFakturPajak, NomorFakturPajak, _
                                  KodeSupplier, NamaSupplier, _
                                  NamaBarang, NamaJasa, _
                                  DPP, PPN, _
                                  PPhDipotong, JumlahTagihan, JumlahBayar, SisaHutang, _
                                  DueDate, Keterangan)
        Loop

        AksesDatabase_Transaksi(Tutup)
        DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
        BarisTerseleksi = -1
        DataGridView.ClearSelection()
        txt_TotalSaldoAwalHutangUsaha.Text = TotalSaldoAwalHutangUsaha

        Select Case JenisTahunBuku
            Case JenisTahunBuku_LAMPAU
                TotalSaldoAkhirHutangUsaha = TotalSaldoAwalHutangUsaha
                AmbilValueSaldoAkhirCOAHutangUsaha()
                CekKesesuaianSaldoAkhir()
            Case JenisTahunBuku_NORMAL
                AmbilValueSaldoAwalCOAHutangUsaha_PlusPenyesuaian()
                CekKesesuaianSaldoAwal()
        End Select

    End Sub

    Sub AmbilValueSaldoAwalCOAHutangUsaha_PlusPenyesuaian()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        dr.Read()
        SaldoAwalCOAHutangUsaha = dr.Item("Saldo_Awal")
        txt_SaldoAwalCOAHutangUsaha.Text = SaldoAwalCOAHutangUsaha
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE COA = '" & KodeTautanCOA_BiayaSelisihPencatatan & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        JumlahPenyesuaianHutangUsaha = 0
        Do While dr.Read
            Dim NomorJV_Telusur = dr.Item("Nomor_JV")
            Dim cmdTELUSUR = New OdbcCommand(" SELECT * FROM tbl_Transaksi " & _
                                             " WHERE Nomor_JV = '" & NomorJV_Telusur & "' " & _
                                             " AND COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' ", KoneksiDatabaseTransaksi)
            Dim drTELUSUR = cmdTELUSUR.ExecuteReader
            drTELUSUR.Read()
            If drTELUSUR.HasRows Then
                JumlahPenyesuaianHutangUsaha = JumlahPenyesuaianHutangUsaha + drTELUSUR.Item("Jumlah_Kredit")
            End If
        Loop
        AksesDatabase_Transaksi(Tutup)
        SaldoAwalCOAHutangUsahaPlusPenyesuaian = SaldoAwalCOAHutangUsaha + JumlahPenyesuaianHutangUsaha
        txt_JumlahPenyesuaianHutangUsaha.Text = JumlahPenyesuaianHutangUsaha
        txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Text = SaldoAwalCOAHutangUsahaPlusPenyesuaian
    End Sub

    Sub AmbilValueSaldoAkhirCOAHutangUsaha()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_SaldoAwalCOA WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        dr.Read()
        SaldoAkhirCOAHutangUsaha = dr.Item("Saldo_Awal")
        txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Text = SaldoAkhirCOAHutangUsaha
    End Sub

    Sub CekKesesuaianSaldoAwal()
        If TotalSaldoAwalHutangUsaha = SaldoAwalCOAHutangUsahaPlusPenyesuaian Then
            KesesuaianSaldoAwal = True
            btn_Sesuaikan.Enabled = False
            txt_TotalSaldoAwalHutangUsaha.ForeColor = Color.Black
            txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.ForeColor = Color.Black
        End If
        If TotalSaldoAwalHutangUsaha > SaldoAwalCOAHutangUsahaPlusPenyesuaian Then
            KesesuaianSaldoAwal = False
            btn_Sesuaikan.Enabled = True
            txt_TotalSaldoAwalHutangUsaha.ForeColor = Color.Black
            txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.ForeColor = Color.Red
        End If
        If TotalSaldoAwalHutangUsaha < SaldoAwalCOAHutangUsahaPlusPenyesuaian Then
            KesesuaianSaldoAwal = False
            btn_Sesuaikan.Enabled = True
            txt_TotalSaldoAwalHutangUsaha.ForeColor = Color.Red
            txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.ForeColor = Color.Black
        End If
    End Sub

    Sub CekKesesuaianSaldoAkhir()
        If TotalSaldoAkhirHutangUsaha = SaldoAkhirCOAHutangUsaha Then
            KesesuaianSaldoAkhir = True
            btn_Sesuaikan.Enabled = False
            txt_TotalSaldoAwalHutangUsaha.ForeColor = Color.Black
            txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.ForeColor = Color.Black
        End If
        If TotalSaldoAkhirHutangUsaha > SaldoAkhirCOAHutangUsaha Then
            KesesuaianSaldoAkhir = False
            btn_Sesuaikan.Enabled = True
            txt_TotalSaldoAwalHutangUsaha.ForeColor = Color.Black
            txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.ForeColor = Color.Red
        End If
        If TotalSaldoAkhirHutangUsaha < SaldoAkhirCOAHutangUsaha Then
            KesesuaianSaldoAkhir = False
            btn_Sesuaikan.Enabled = True
            txt_TotalSaldoAwalHutangUsaha.ForeColor = Color.Red
            txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.ForeColor = Color.Black
        End If
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick
    End Sub
    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick
        If DataGridView.RowCount = 0 Then Return
        BarisTerseleksi = DataGridView.CurrentRow.Index

    End Sub

    Private Sub txt_Pencarian_TextChanged(sender As Object, e As EventArgs) Handles txt_Pencarian.TextChanged
        TampilkanData()
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        frm_InputSaldoAwalHutangUsaha.ResetForm()
        frm_InputSaldoAwalHutangUsaha.FungsiForm = FungsiForm_TAMBAH
        frm_InputSaldoAwalHutangUsaha.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        If BarisTerseleksi = -1 Then
            MsgBox("Tidak ada data terseleksi")
            Return
        End If
        frm_InputSaldoAwalHutangUsaha.ResetForm()
        frm_InputSaldoAwalHutangUsaha.txt_NomorPembelian.Text = DataGridView.Item("Nomor_Pembelian", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.txt_Referensi.Text = DataGridView.Item("Referensi_", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.dtp_TanggalInvoice.Value = DataGridView.Item("Tanggal_Invoice", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.txt_NomorInvoice.Text = DataGridView.Item("Nomor_Invoice", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.dtp_TanggalFakturPajak.Value = DataGridView.Item("Tanggal_Invoice", BarisTerseleksi).Value 'Dibikin Simple saja : TanggalFakturPajak ngambil dari TanggalInvoice
        frm_InputSaldoAwalHutangUsaha.txt_NomorFakturPajak.Text = DataGridView.Item("Nomor_Faktur_Pajak", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.txt_KodeSupplier.Text = DataGridView.Item("Kode_Supplier", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.txt_NamaSupplier.Text = DataGridView.Item("Nama_Supplier", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.txt_NamaBarang.Text = DataGridView.Item("Nama_Barang", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.txt_NamaJasa.Text = DataGridView.Item("Nama_Jasa", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.txt_DPP.Text = AmbilAngka(DataGridView.Item("DPP_", BarisTerseleksi).Value)
        frm_InputSaldoAwalHutangUsaha.txt_PPN.Text = AmbilAngka(DataGridView.Item("PPN_", BarisTerseleksi).Value)
        frm_InputSaldoAwalHutangUsaha.txt_PPhDipotong.Text = AmbilAngka(DataGridView.Item("PPh_Dipotong", BarisTerseleksi).Value)
        frm_InputSaldoAwalHutangUsaha.txt_JumlahBayar.Text = AmbilAngka(DataGridView.Item("Jumlah_Bayar", BarisTerseleksi).Value)
        frm_InputSaldoAwalHutangUsaha.dtp_DueDate.Value = DataGridView.Item("Due_Date", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.txt_Keterangan.Text = DataGridView.Item("Keterangan_", BarisTerseleksi).Value
        frm_InputSaldoAwalHutangUsaha.FungsiForm = FungsiForm_EDIT
        frm_InputSaldoAwalHutangUsaha.ShowDialog()
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click
        FiturBelumBisaDigunakan()
        'Harus Dibedakan ketika menghapus pembayaran untuk Hutang pada tahun buku aktif, dengan pembayaran untuk sisa hutang tahun sebelumnya....!!!
        Return
    End Sub


    Private Sub txt_TotalSaldoAwalHutangUsaha_TextChanged(sender As Object, e As EventArgs) Handles txt_TotalSaldoAwalHutangUsaha.TextChanged
        Try
            If txt_TotalSaldoAwalHutangUsaha.Text.Trim() <> "" Then
                txt_TotalSaldoAwalHutangUsaha.Text = CDec(txt_TotalSaldoAwalHutangUsaha.Text).ToString("N0")
                txt_TotalSaldoAwalHutangUsaha.SelectionStart = txt_TotalSaldoAwalHutangUsaha.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txt_TotalSaldoAwalHutangUsaha_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_TotalSaldoAwalHutangUsaha.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoAwalCOAHutangUsaha_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwalCOAHutangUsaha.TextChanged
        Try
            If txt_SaldoAwalCOAHutangUsaha.Text.Trim() <> "" Then
                txt_SaldoAwalCOAHutangUsaha.Text = CDec(txt_SaldoAwalCOAHutangUsaha.Text).ToString("N0")
                txt_SaldoAwalCOAHutangUsaha.SelectionStart = txt_SaldoAwalCOAHutangUsaha.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txt_SaldoAwalCOAHutangUsaha_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwalCOAHutangUsaha.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.TextChanged
        Try
            If txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Text.Trim() <> "" Then
                txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Text = CDec(txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Text).ToString("N0")
                txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.SelectionStart = txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_JumlahPenyesuaianHutangUsaha_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahPenyesuaianHutangUsaha.TextChanged
        Try
            If txt_JumlahPenyesuaianHutangUsaha.Text.Trim() <> "" Then
                txt_JumlahPenyesuaianHutangUsaha.Text = CDec(txt_JumlahPenyesuaianHutangUsaha.Text).ToString("N0")
                txt_JumlahPenyesuaianHutangUsaha.SelectionStart = txt_JumlahPenyesuaianHutangUsaha.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub txt_JumlahPenyesuaianHutangUsaha_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahPenyesuaianHutangUsaha.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_Sesuaikan_Click(sender As Object, e As EventArgs) Handles btn_Sesuaikan.Click

        'JIKA JENIS TAHUN BUKU LAMPAU :
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            frm_InputCOA.ResetForm()
            frm_InputCOA.FungsiForm = FungsiForm_EDIT
            frm_InputCOA.JalurMasuk = Halaman_TUTUPBUKU
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & KodeTautanCOA_HutangUsaha_NonAfiliasi & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            frm_InputCOA.txt_COA.Text = KodeTautanCOA_HutangUsaha_NonAfiliasi
            frm_InputCOA.txt_NamaAkun.Text = dr.Item("Nama_Akun")
            frm_InputCOA.cmb_DebetKredit.Text = dr.Item("D_K")
            Dim InputSaldoAwal = dr.Item("Saldo_Awal")
            If InputSaldoAwal = 0 Then
                frm_InputCOA.txt_SaldoAwal.Text = ""
            Else
                frm_InputCOA.txt_SaldoAwal.Text = InputSaldoAwal
            End If
            frm_InputCOA.txt_Uraian.Text = dr.Item("Uraian")
            frm_InputCOA.cmb_Visibilitas.Text = dr.Item("Visibilitas")
            AksesDatabase_General(Tutup)
            frm_InputCOA.ShowDialog()
            SaldoAkhirCOAHutangUsaha = AmbilAngka(frm_InputCOA.txt_SaldoAwal.Text)
            txt_SaldoAwalCOAHutangUsahaPlusPenyesuaian.Text = SaldoAkhirCOAHutangUsaha
            CekKesesuaianSaldoAkhir()
        End If

        'JIKA JENIS TAHUN BUKU NORMAL :
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then
            'Jika SaldoBerdasarkanList < ( Slado COA + Adjusment ) :
            If TotalSaldoAwalHutangUsaha < SaldoAwalCOAHutangUsahaPlusPenyesuaian Then
                btn_Tambah_Click(sender, e)
            End If

            'Jika SaldoBerdasarkanList > ( Slado COA + Adjusment ) :
            If TotalSaldoAwalHutangUsaha > SaldoAwalCOAHutangUsahaPlusPenyesuaian Then
                MsgBox("Silakan buat Jurnal Penyesuaian (Adjusment).")
                Dim NamaAkun_BiayaSelisihPencatatan
                Dim NamaAkun_HutangUsaha
                Dim JumlahPenyesuaian = TotalSaldoAwalHutangUsaha - SaldoAwalCOAHutangUsahaPlusPenyesuaian
                KodeAkun_Tembak = KodeTautanCOA_BiayaSelisihPencatatan
                PengisianValue_NamaAkun()
                NamaAkun_BiayaSelisihPencatatan = NamaAkun_Tembak
                KodeAkun_Tembak = KodeTautanCOA_HutangUsaha_NonAfiliasi
                PengisianValue_NamaAkun()
                NamaAkun_HutangUsaha = NamaAkun_Tembak
                frm_InputJurnal.ResetForm()
                frm_InputJurnal.JalurMasuk = Halaman_SALDOAWALHUTANGUSAHA
                frm_InputJurnal.FungsiForm = FungsiForm_TAMBAH
                frm_InputJurnal.DataTabelUtama.Rows.Insert(0, 1, KodeTautanCOA_BiayaSelisihPencatatan, NamaAkun_BiayaSelisihPencatatan, "D", JumlahPenyesuaian, Nothing)
                frm_InputJurnal.DataTabelUtama.Rows.Insert(1, 2, KodeTautanCOA_HutangUsaha_NonAfiliasi, PenjorokNamaAkun & NamaAkun_HutangUsaha, "K", Nothing, JumlahPenyesuaian)
                frm_InputJurnal.DataTabelUtama.Item("Debet", 3).Value = JumlahPenyesuaian
                frm_InputJurnal.DataTabelUtama.Item("Kredit", 3).Value = JumlahPenyesuaian
                frm_InputJurnal.lbl_StatusBalance.ForeColor = Color.Green
                frm_InputJurnal.lbl_StatusBalance.Text = "Tidak Ada Selisih"
                BeginInvoke(Sub() frm_InputJurnal.btn_TambahTransaksi.Enabled = False)
                BeginInvoke(Sub() frm_InputJurnal.btn_Reset.Enabled = False)
                BeginInvoke(Sub() frm_InputJurnal.btn_Simpan.Enabled = True)
                BeginInvoke(Sub() frm_InputJurnal.JumlahBarisJurnal = 2)
                frm_InputJurnal.ShowDialog()
            End If
        End If

    End Sub

End Class