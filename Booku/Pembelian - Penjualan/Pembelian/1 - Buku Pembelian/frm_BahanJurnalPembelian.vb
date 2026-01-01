Imports bcomm
Imports System.Data.Odbc

Public Class frm_BahanJurnalPembelian

    Public NomorPEMB
    Public NomorBPHU
    Public StatusKirimJurnal
    Dim BarisTerseleksi
    Dim DK
    Dim JumlahBaris
    Dim StatusBalance = Nothing
    Dim Referensi = Nothing
    Dim KodeAkun
    Public KodeSupplier
    Public COADebet, COAKredit As String
    Public COABarang1, COABarang2, COAJasa1, COAJasa2, COAJasa3 As String
    Public COAPPN, COAPPh21, COAPPh23, COAPPh29, COAPPh42, COAPPhTerutang As String
    Public DPPBarang1, DPPBarang2, DPPJasa1, DPPJasa2, DPPJasa3 As Int64
    Public JumlahDPP, JumlahPPN, JumlahPPhDitanggung, JumlahPPh21, JumlahPPh23, JumlahPPh42 As Int64
    Public BiayaAdministrasiBank As Int64
    Public DitanggungOleh
    Dim JumlahTransaksi As Int64
    Dim JumlahDebet As Int64
    Dim JumlahKredit As Int64
    Dim TotalDebet As Int64
    Dim TotalKredit As Int64
    Dim TanggalTransaksi
    Public UraianTransaksi
    Dim NomorUrut

    Private Sub frm_JurnalVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lbl_NomorPEMB.Text = NomorPEMB
        NomorBPHU = AwalanBPHU & Microsoft.VisualBasic.Mid(NomorPEMB, PanjangTeks_AwalanBPHU_Plus1)
        TampilkanData()

    End Sub

    Sub ResetForm()
        NomorPEMB = StripKosong
        lbl_NomorPEMB.Text = StripKosong
        dtp_TanggalJurnal.Text = Today
        lbl_TanggalInvoice.Text = StripKosong
        lbl_NomorInvoice.Text = StripKosong
        lbl_TanggalFakturPajak.Text = StripKosong
        lbl_NomorFakturPajak.Text = StripKosong
        lbl_PPNDikreditkan.Text = StripKosong
        lbl_PPhTerutang.Text = StripKosong
        lbl_NamaLawanTransaksi.Text = StripKosong
        lbl_AlamatLawanTransaksi.Text = StripKosong
        btn_Batal.Text = "Batal"
        DataGridView.Rows.Clear()
    End Sub

    Sub TampilkanData()

        TotalDebet = 0
        TotalKredit = 0
        DPPBarang1 = AmbilAngka(frm_InputPembelian.txt_DPPBarang1.Text)
        DPPBarang2 = AmbilAngka(frm_InputPembelian.txt_DPPBarang2.Text)
        DPPJasa1 = AmbilAngka(frm_InputPembelian.txt_DPPJasa1.Text)
        DPPJasa2 = AmbilAngka(frm_InputPembelian.txt_DPPJasa2.Text)
        DPPJasa3 = AmbilAngka(frm_InputPembelian.txt_DPPJasa3.Text)
        'JumlahPPN = frm_InputPembelian.JumlahPPN_Trx
        'JumlahPPhDitanggung = frm_InputPembelian.JumlahPPhDitanggung_Trx
        'JumlahPPh21 = frm_InputPembelian.JumlahPPh21_Trx
        'JumlahPPh23 = frm_InputPembelian.JumlahPPh23_Trx
        'JumlahPPh42 = frm_InputPembelian.JumlahPPh42_Trx
        BiayaAdministrasiBank = AmbilAngka(frm_InputPembelian.txt_BiayaAdministrasiBank.Text)
        DitanggungOleh = frm_InputPembelian.cmb_DitanggungOleh.Text
        'JumlahTransaksi = frm_InputPembelian.JumlahTransaksi_Trx
        NomorUrut = 0

        DataGridView.Rows.Clear()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'KELOMPOK DATA BARANG =============================
        'Penyimpanan Debet Barang1 :
        If DPPBarang1 > 0 Then
            DK = "D"
            KodeAkun = frm_InputPembelian.COABarang1
            JumlahDebet = DPPBarang1
            JumlahKredit = 0
            '--------------------------------
            TambahBarisTabel()
        End If
        'Penyimpanan Debet Barang2 :
        If DPPBarang2 > 0 Then
            DK = "D"
            KodeAkun = frm_InputPembelian.COABarang2
            JumlahDebet = DPPBarang2
            JumlahKredit = 0
            '--------------------------------
            TambahBarisTabel()
        End If

        'KELOMPOK DATA JASA ===============================
        'Penyimpanan Debet Jasa1 :
        If DPPJasa1 > 0 Then
            DK = "D"
            KodeAkun = frm_InputPembelian.COAJasa1
            JumlahDebet = DPPJasa1
            JumlahKredit = 0
            '--------------------------------
            TambahBarisTabel()
        End If
        'Penyimpanan Debet Jasa2 :
        If DPPJasa2 > 0 Then
            DK = "D"
            KodeAkun = frm_InputPembelian.COAJasa2
            JumlahDebet = DPPJasa2
            JumlahKredit = 0
            '--------------------------------
            TambahBarisTabel()
        End If

        'Penyimpanan Debet Jasa3 :
        If DPPJasa3 > 0 Then
            DK = "D"
            KodeAkun = frm_InputPembelian.COAJasa3
            JumlahDebet = DPPJasa3
            JumlahKredit = 0
            '--------------------------------
            TambahBarisTabel()
        End If

        'KELOMPOK DATA PPN & PPH ==========================
        'Penyimpanan Data PPN :
        If JumlahPPN > 0 Then
            DK = "D"
            KodeAkun = KodeTautanCOA_PPNMasukan_Lokal
            JumlahDebet = JumlahPPN
            JumlahKredit = 0
            '--------------------------------
            TambahBarisTabel()
        End If
        'Penyimpanan Data PPh Ditanggung :
        If JumlahPPhDitanggung > 0 Then
            DK = "D"
            'KodeAkun = KodeTautanCOA_BiayaPPh
            JumlahDebet = JumlahPPhDitanggung
            JumlahKredit = 0
            '--------------------------------
            TambahBarisTabel()
        End If

        'Penyimpan Debet Biaya Administrasi, jika ada, baik ditanggung oleh SUPPLIER maupun PERUSAHAAN :
        If BiayaAdministrasiBank > 0 Then
            DK = "D"
            KodeAkun = KodeTautanCOA_BiayaAdministrasiBank
            JumlahDebet = BiayaAdministrasiBank
            JumlahKredit = 0
            '--------------------------------
            TambahBarisTabel()
        End If

        jur_DK = dk_K '==================== KELOMPOK DATA KREDIT =======================
        'Penyimpanan Data PPh Pasal 21 :
        If JumlahPPh21 > 0 Then
            DK = "K"
            KodeAkun = KodeTautanCOA_HutangPPhPasal21
            JumlahDebet = 0
            JumlahKredit = JumlahPPh21
            '--------------------------------
            TambahBarisTabel()
        End If
        'Penyimpanan Data PPh Pasal 23 :
        If JumlahPPh23 > 0 Then
            DK = "K"
            KodeAkun = KodeTautanCOA_HutangPPhPasal23
            JumlahDebet = 0
            JumlahKredit = JumlahPPh23
            '--------------------------------
            TambahBarisTabel()
        End If
        'Penyimpanan Data PPh Pasal 4 (2) :
        If JumlahPPh42 > 0 Then
            DK = "K"
            KodeAkun = KodeTautanCOA_HutangPPhPasal42
            JumlahDebet = 0
            JumlahKredit = JumlahPPh42
            '--------------------------------
            TambahBarisTabel()
        End If
        'Penyimpanan Data Kredit (Kas/PettyCash/Bank/CashAdvance) :
        If BiayaAdministrasiBank > 0 Then
            If DitanggungOleh = DitanggungOleh_LawanTransaksi Then
                DK = "K"
                KodeAkun = frm_InputPembelian.COAKredit
                JumlahDebet = 0
                JumlahKredit = JumlahTransaksi - BiayaAdministrasiBank
                '--------------------------------
                TambahBarisTabel()
                '--------------------------------
                '--------------------------------
                DK = "K"
                KodeAkun = frm_InputPembelian.COAKredit
                JumlahDebet = 0
                JumlahKredit = BiayaAdministrasiBank
                '--------------------------------
                TambahBarisTabel()
                '--------------------------------
                '--------------------------------
                DK = "K"
                KodeAkun = KodeTautanCOA_BiayaAdministrasiBank
                JumlahDebet = 0
                JumlahKredit = BiayaAdministrasiBank
                '--------------------------------
                TambahBarisTabel()
            End If
            If DitanggungOleh = DitanggungOleh_Perusahaan Then
                DK = "K"
                KodeAkun = frm_InputPembelian.COAKredit
                JumlahDebet = 0
                JumlahKredit = JumlahTransaksi
                '--------------------------------
                TambahBarisTabel()
                '--------------------------------
                '--------------------------------
                DK = "K"
                KodeAkun = frm_InputPembelian.COAKredit
                JumlahDebet = 0
                JumlahKredit = BiayaAdministrasiBank
                '--------------------------------
                TambahBarisTabel()
            End If
        Else 'Normal, tanpa Biaya Admin :
            DK = "K"
            KodeAkun = frm_InputPembelian.COAKredit
            JumlahDebet = 0
            JumlahKredit = JumlahTransaksi
            '--------------------------------
            TambahBarisTabel()
        End If

        JumlahBaris = DataGridView.RowCount 'Jangan dihapus, dan jangan dipindahkan..!!!
        DataGridView.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        DataGridView.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!

        NotifBalance()

        DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
        DataGridView.ClearSelection()

    End Sub

    Private Sub frm_BahanJurnal_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim LebarForm As Int64
        Dim TinggiForm As Int64
        LebarForm = Me.Width
        TinggiForm = Me.Height
        DataGridView.Size = New Drawing.Size(LebarForm - 40, TinggiForm - 300)
        lbl_StatusBalance.Location = New Drawing.Point(295, TinggiForm - 122)
        btn_Edit.Location = New Drawing.Point(619, TinggiForm - 87)
        btn_Batal.Location = New Drawing.Point(708, TinggiForm - 87)
        If LebarForm <= 820 Then
            Me.Size = New Drawing.Size(820, TinggiForm)
        End If
        If TinggiForm <= 700 Then
            Me.Size = New Drawing.Size(LebarForm, 700)
        End If
    End Sub

    Sub TambahBarisTabel()
        NomorUrut = NomorUrut + 1
        Dim clm_NoUrut = NomorUrut & "."
        Dim clm_JumlahDebet
        Dim clm_JumlahKredit
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Me.Close()
        cmd = New OdbcCommand(" SELECT Nama_Akun FROM tbl_COA WHERE COA = '" & KodeAkun & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Dim NamaAkun = dr.Item("Nama_Akun")
            clm_JumlahDebet = JumlahDebet
            clm_JumlahKredit = JumlahKredit
            If JumlahDebet = 0 Then
                clm_JumlahDebet = StripKosong
                NamaAkun = PenjorokNamaAkun & NamaAkun
            Else
                clm_JumlahDebet = JumlahDebet
            End If
            If JumlahKredit = 0 Then
                clm_JumlahKredit = StripKosong
            Else
                clm_JumlahKredit = JumlahKredit
            End If
            DataGridView.Rows.Add(clm_NoUrut, KodeAkun, NamaAkun, DK, clm_JumlahDebet, clm_JumlahKredit, UraianTransaksi)
        End If
        AksesDatabase_General(Tutup)
        TotalDebet = TotalDebet + JumlahDebet
        TotalKredit = TotalKredit + JumlahKredit
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

        DataGridView.Item("Nama_Akun", JumlahBaris + 1).Value = "J  U  M  L  A  H"
        DataGridView.Item("Debet", JumlahBaris + 1).Value = clm_TotalDebet
        DataGridView.Item("Kredit", JumlahBaris + 1).Value = clm_TotalKredit
        DataGridView.Item("Keterangan", JumlahBaris + 1).Value = StatusBalance

        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("Keterangan").Value = "Ada Selisih" Then
                row.DefaultCellStyle.ForeColor = Color.Red
            End If
            If row.Cells("Keterangan").Value = "Tidak Ada Selisih" Then
                row.DefaultCellStyle.ForeColor = Color.Black
            End If
        Next

    End Sub

    Private Sub btn_Setujui_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        frm_InputPembelian.FungsiForm = FungsiForm_EDIT
        frm_InputPembelian.ShowDialog()
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        StatusKirimJurnal = "BATAL"
        Me.Close()
    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick
    End Sub
    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick
    End Sub

    Private Sub btn_Kirim_Click(sender As Object, e As EventArgs) Handles btn_Kirim.Click

        'Tahun Jurnal harus sama dengan Tahun Buku Aktif
        Dim TahunJurnal = Format(dtp_TanggalJurnal.Value, "yyyy")
        If TahunJurnal <> TahunBukuAktif Then
            MsgBox("Tahun Jurnal tidak sesuai dengan Tahun Buku Aktif." & Enter2Baris & "Silakan isi kolom 'Tanggal Jurnal' dengan benar.")
            dtp_TanggalJurnal.Focus()
            Return
        End If

        'Tanggal Transaksi tidak mungkin melebihi Tanggal Invoice
        Dim TanggalInvoice As Date = lbl_TanggalInvoice.Text
        If dtp_TanggalJurnal.Value > TanggalInvoice Then
            MsgBox("Silakan isi kolom 'Tanggal Jurnal' dengan benar.")
            dtp_TanggalJurnal.Focus()
            Return
        End If

        Pilihan = MessageBox.Show("Yakin akan mengirim data ini ke Jurnal..?", "Perhatian..!", MessageBoxButtons.YesNo)

        If Pilihan = vbYes Then
            StatusKirimJurnal = "KIRIM"
            Me.Close()
        Else
            StatusKirimJurnal = "BATAL"
            Return
        End If

    End Sub

End Class