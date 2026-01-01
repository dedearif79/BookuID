Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_JurnalVoucher_X

    Public FungsiForm
    Public NomorJV
    Public Angka_NomorJV
    Public JenisJurnal
    Dim BarisTerseleksi
    Dim JumlahBaris
    Dim TotalDebet As Int64
    Dim TotalKredit As Int64
    Dim StatusBalance = Kosongan
    Dim Referensi = Kosongan
    Dim BPHBPP = Kosongan

    Private Sub frm_JurnalVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_JURNALAPPROVE Then
            If SistemApprovalPerusahaan = True Then btn_Setujui.Visible = True
            btn_Batal.Text = "Batal"
        End If
        If FungsiForm = FungsiForm_INFOJURNAL Then
            btn_Setujui.Visible = False
            btn_Batal.Text = "Tutup"
        End If

        If SistemApprovalPerusahaan = False Then
            lbl_Approve.Visible = False
            lbl_NamaUserApprove.Visible = False
        End If

        NomorJV = AwalanNomorJV & Angka_NomorJV
        lbl_NomorJV.Text = "No. " & NomorJV

        TampilkanData()

        BeginInvoke(Sub() DataTabelUtama.ClearSelection())

    End Sub

    Sub ResetForm()
        btn_Cetak.Visible = True
        If SistemApprovalPerusahaan = True Then
            btn_Setujui.Visible = True
        Else
            btn_Setujui.Visible = False
        End If
        btn_Batal.Text = "Batal"
    End Sub

    Sub TampilkanData()

        StyleTabelUtama(DataTabelUtama)

        Dim NomorUrut = 0
        Dim clm_NoUrut
        Dim COA = ""
        Dim NamaAkun = ""
        Dim DK = ""
        Dim JumlahDebet As Int64 = 0
        Dim JumlahKredit As Int64 = 0
        TotalDebet = 0
        TotalKredit = 0
        Dim clm_JumlahDebet
        Dim clm_JumlahKredit
        Dim Keterangan = ""
        Dim NomorID
        Dim cmdCOA As OdbcCommand
        Dim drCOA As OdbcDataReader
        Dim cmdMitra As OdbcCommand
        Dim drMitra As OdbcDataReader
        Dim TanggalTransaksi = "-"
        Dim TanggalInvoice = "-"
        Dim NomorInvoice = "-"
        Dim TanggalFakturPajak = "-"
        Dim NomorFakturPajak = "-"
        Dim PPNDikreditkan = "-"
        Dim PPhTerutang = "-"
        Dim KodeLawanTransaksi = ""
        Dim NamaLawanTransaksi = "-"
        Dim AlamatLawanTransaksi = "-"
        Dim NamaUserEntry = "-"
        Dim NamaUserApprove = "-"
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & Angka_NomorJV & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        DataTabelUtama.Rows.Clear()
        DataTabelUtama.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Do While dr.Read
            NomorUrut = NomorUrut + 1
            clm_NoUrut = NomorUrut & "."
            COA = dr.Item("COA")
            AksesDatabase_General(Buka)
            cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drCOA = cmdCOA.ExecuteReader
            drCOA.Read()
            NamaAkun = drCOA.Item("Nama_Akun")
            AksesDatabase_General(Tutup)
            DK = dr.Item("D_K")
            JumlahDebet = dr.Item("Jumlah_Debet")
            JumlahKredit = dr.Item("Jumlah_Kredit")
            clm_JumlahDebet = JumlahDebet
            clm_JumlahKredit = JumlahKredit
            If DK = "D" Then
                clm_JumlahKredit = StripKosong
            End If
            If DK = "K" Then
                clm_JumlahDebet = StripKosong
                NamaAkun = PenjorokNamaAkun & NamaAkun
            End If
            TotalDebet = TotalDebet + JumlahDebet
            TotalKredit = TotalKredit + JumlahKredit
            Keterangan = dr.Item("Uraian_Transaksi")
            NomorID = dr.Item("Nomor_ID")
            DataTabelUtama.Rows.Add(clm_NoUrut, COA, NamaAkun, DK, clm_JumlahDebet, clm_JumlahKredit, Keterangan, NomorID)
            TanggalTransaksi = dr.Item("Tanggal_Transaksi")
            Referensi = dr.Item("Referensi")
            BPHBPP = dr.Item("Bundelan")
            JenisJurnal = dr.Item("Jenis_Jurnal")
            TanggalInvoice = TampilanBundelan(dr.Item("Tanggal_Invoice"))
            NomorInvoice = TampilanBundelan(dr.Item("Nomor_Invoice"))
            TanggalFakturPajak = "-"
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            PPNDikreditkan = "-"
            PPhTerutang = "-"
            NamaLawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            AksesDatabase_General(Buka)
            cmdMitra = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Nama_Mitra = '" & NamaLawanTransaksi & "' ", KoneksiDatabaseGeneral)
            drMitra = cmdMitra.ExecuteReader
            drMitra.Read()
            If drMitra.HasRows Then AlamatLawanTransaksi = drMitra.Item("Alamat")
            AksesDatabase_General(Tutup)
            NamaUserEntry = dr.Item("Nama_User_Entry")
            NamaUserApprove = dr.Item("Nama_User_Approve")
        Loop
        AksesDatabase_Transaksi(Tutup)
        JumlahBaris = DataTabelUtama.RowCount 'Jangan dihapus, dan jangan dipindahkan..!!!
        DataTabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        DataTabelUtama.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!

        NotifBalance()

        DataTabelUtama.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
        DataTabelUtama.ClearSelection()

        lbl_TanggalJurnal.Text = TanggalTransaksi
        txt_TanggalInvoice.Text = TanggalInvoice
        txt_NomorInvoice.Text = NomorInvoice
        lbl_Referensi.Text = Referensi
        'lbl_TanggalFakturPajak.Text = TanggalFakturPajak
        lbl_NomorFakturPajak.Text = NomorFakturPajak
        'lbl_PPNDikreditkan.Text = PPNDikreditkan
        'lbl_PPhTerutang.Text = PPhTerutang
        lbl_NamaLawanTransaksi.Text = NamaLawanTransaksi
        'lbl_AlamatLawanTransaksi.Text = AlamatLawanTransaksi
        lbl_NamaUserEntry.Text = NamaUserEntry
        lbl_NamaUserApprove.Text = NamaUserApprove
        lbl_JenisJurnal.Text = "Jenis Jurnal   :   " & JenisJurnal

    End Sub

    Sub NotifBalance()

        If TotalDebet = TotalKredit Then
            StatusBalance = "Tidak Ada Selisih"
            lbl_StatusBalance.ForeColor = Color.Green
        Else
            StatusBalance = "Ada Selisih"
            lbl_StatusBalance.ForeColor = Color.Red
        End If

        lbl_StatusBalance.Text = StatusBalance

        Dim clm_TotalDebet
        If TotalDebet = 0 Then
            clm_TotalDebet = StripKosong
        Else
            clm_TotalDebet = TotalDebet
        End If

        Dim clm_TotalKredit
        If TotalKredit = 0 Then
            clm_TotalKredit = StripKosong
        Else
            clm_TotalKredit = TotalKredit
        End If

        DataTabelUtama.Item("Nama_Akun", JumlahBaris + 1).Value = "J  U  M  L  A  H"
        DataTabelUtama.Item("Jumlah_Debet", JumlahBaris + 1).Value = clm_TotalDebet
        DataTabelUtama.Item("Jumlah_Kredit", JumlahBaris + 1).Value = clm_TotalKredit
        DataTabelUtama.Item("Keterangan_", JumlahBaris + 1).Value = StatusBalance

        For Each row As DataGridViewRow In DataTabelUtama.Rows
            If row.Cells("Keterangan_").Value = "Ada Selisih" Then
                row.DefaultCellStyle.ForeColor = Color.Red
            End If
            If row.Cells("Keterangan_").Value = "Tidak Ada Selisih" Then
                row.DefaultCellStyle.ForeColor = Color.Black
            End If
        Next

    End Sub

    Private Sub btn_Setujui_Click(sender As Object, e As EventArgs) Handles btn_Setujui.Click

        If TotalDebet <> TotalKredit Then
            MsgBox("Data tidak dapat disimpan karena ADA SELISIH. Silakan dikoreksi kembali.")
            Return
        End If

        Dim PilihSetujui = MessageBox.Show("Data yang Anda setujui tidak akan bisa diedit lagi." & Enter2Baris & "Yakin akan menyetujui..?", "Perhatian..!", MessageBoxButtons.YesNo)

        If PilihSetujui = vbYes Then
            TrialBalance_Mentahkan() 'PENTING...!!!
            Dim Baris = 0
            Dim NomorID
            Dim COA
            Dim DK = ""
            Dim JumlahDebet As Int64 = 0
            Dim JumlahKredit As Int64 = 0
            Dim QueryEdit, QueryEdit1, QueryEdit2, QueryEdit3
            AksesDatabase_Transaksi(Buka)
            Do While Baris < JumlahBaris
                NomorID = DataTabelUtama.Item("Nomor_ID", Baris).Value
                COA = DataTabelUtama.Item("Kode_Akun", Baris).Value
                DK = DataTabelUtama.Item("D_K", Baris).Value
                JumlahDebet = AmbilAngka(DataTabelUtama.Item("Jumlah_Debet", Baris).Value)
                JumlahKredit = AmbilAngka(DataTabelUtama.Item("Jumlah_Kredit", Baris).Value)
                QueryEdit1 = " UPDATE tbl_Transaksi SET "
                QueryEdit2 = " COA = '" & COA & "', D_K = '" & DK & "', Jumlah_Debet = '" & JumlahDebet & "', Jumlah_Kredit = '" & JumlahKredit & "', "
                QueryEdit3 = " Status_Approve = 1, Username_Approve = '" & UserAktif & "', Nama_User_Approve = '" & NamaUserAktif & "' WHERE Nomor_ID = '" & NomorID & "' "
                QueryEdit = QueryEdit1 & QueryEdit2 & QueryEdit3
                cmd = New OdbcCommand(QueryEdit, KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                Baris = Baris + 1
            Loop
            AksesDatabase_Transaksi(Tutup)

            'Update Status Jurnal di Tabel Pembelian, menjadi bernilai '1', agar bisa tampil di Buku Pengawasan Hutang Usaha.
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_Pembelian SET Status_Jurnal = 1 WHERE Nomor_JV = '" & Angka_NomorJV & "' ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)

            MsgBox("Jurnal dengan Nomor " & NomorJV & " telah disetujui.")
            frm_BukuPembelian.TampilkanData()
            If usc_BukuPengawasanHutangUsaha.StatusAktif Then usc_BukuPengawasanHutangUsaha.TampilkanData()
            If usc_JurnalUmum.StatusAktif Then usc_JurnalUmum.TampilkanData()
            Me.Close()
        End If

    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub

    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
    End Sub
    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama__CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        Dim COATerseleksi = DataTabelUtama.Item("Kode_Akun", BarisTerseleksi).Value
        If COATerseleksi = Nothing Then Return
        'If Microsoft.VisualBasic.Left(Referensi, 3) = "NPPHU" Or Microsoft.VisualBasic.Left(Referensi, 4) = "PEMB" Then
        If COATerseleksi <> Nothing Then '(Ini logika hanya formalitas saja. Intinya code-code di bawah ini sementara tidak dipakai dulu.)
            'MsgBox("Data ini tidak bisa di-edit.")
            Return
        End If
        Dim NamaAkunTerseleksi = DataTabelUtama.Item("Nama_Akun", BarisTerseleksi).Value
        Dim DK = DataTabelUtama.Item("D_K", BarisTerseleksi).Value
        Dim JumlahDebet As Int64 = AmbilAngka(DataTabelUtama.Item("Jumlah_Debet", BarisTerseleksi).Value)
        Dim JumlahKredit As Int64 = AmbilAngka(DataTabelUtama.Item("Jumlah_Kredit", BarisTerseleksi).Value)
        frm_InputJurnalPerTransaksi.ResetForm()
        frm_InputJurnalPerTransaksi.txt_COA.Text = COATerseleksi
        frm_InputJurnalPerTransaksi.txt_NamaAkun.Text = NamaAkunTerseleksi
        If DK = "D" Then
            frm_InputJurnalPerTransaksi.cmb_DK.Text = "DEBET"
            frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text = JumlahDebet
        End If
        If DK = "K" Then
            frm_InputJurnalPerTransaksi.cmb_DK.Text = "KREDIT"
            frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text = JumlahKredit
        End If

        'Reset form Input Jurnal, ada di atas. Jangan taruh di sini...!!!
        frm_InputJurnalPerTransaksi.ShowDialog()

        DataTabelUtama.Item("Kode_Akun", BarisTerseleksi).Value = frm_InputJurnalPerTransaksi.COATerseleksi
        DataTabelUtama.Item("Nama_Akun", BarisTerseleksi).Value = frm_InputJurnalPerTransaksi.NamaAkunTerseleksi
        DK = frm_InputJurnalPerTransaksi.DK
        Dim JumlahTransaksi As Int64 = AmbilAngka(frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text)
        If DK = "D" Then
            DataTabelUtama.Item("D_K", BarisTerseleksi).Value = "D"
            DataTabelUtama.Item("Jumlah_Debet", BarisTerseleksi).Value = JumlahTransaksi
            DataTabelUtama.Item("Jumlah_Kredit", BarisTerseleksi).Value = StripKosong
        End If
        If DK = "K" Then
            DataTabelUtama.Item("D_K", BarisTerseleksi).Value = "K"
            DataTabelUtama.Item("Jumlah_Debet", BarisTerseleksi).Value = StripKosong
            DataTabelUtama.Item("Jumlah_Kredit", BarisTerseleksi).Value = JumlahTransaksi
        End If

        Dim Baris = 0
        TotalDebet = 0
        TotalKredit = 0
        Do While Baris < JumlahBaris
            JumlahDebet = AmbilAngka(DataTabelUtama.Item("Jumlah_Debet", Baris).Value)
            JumlahKredit = AmbilAngka(DataTabelUtama.Item("Jumlah_Kredit", Baris).Value)
            TotalDebet += JumlahDebet
            TotalKredit += JumlahKredit
            Baris += 1
        Loop

        NotifBalance()

    End Sub

    Private Sub btn_Cetak_Click(sender As Object, e As EventArgs) Handles btn_Cetak.Click
        Cetak(JenisFormCetak_JurnalVoucher, Angka_NomorJV, True, False)
    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        KunciUkuranForm(Me, 819, 693)
    End Sub

End Class