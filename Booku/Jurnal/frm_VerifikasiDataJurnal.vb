Imports bcomm
Imports System.Data.Odbc

Public Class frm_VerifikasiDataJurnal

    Public NomorJV
    Public BarisAwalBahanJV
    Public BarisAkhirBahanJV
    Public JumlahBarisBahanJV
    Dim BarisTerseleksi
    Dim JumlahBaris
    Dim TotalDebet As Int64
    Dim TotalKredit As Int64
    Dim StatusBalance = ""
    Dim Referensi = ""

    Private Sub frm_VerifikasiDataJurnal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lbl_Judul.Text = "Verifikasi Data Jurnal : Nomor " & NomorJV
        lbl_BarisTabelExcel.Text = "( Baris " & BarisAwalBahanJV & " sampai " & BarisAkhirBahanJV & " pada tabel Excel )."
        ResetForm()
        TampilkanData()

    End Sub

    Sub ResetForm()
        btn_OK.Enabled = True
        DataGridView.Rows.Clear()
    End Sub

    Sub TampilkanData()

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
        Dim NomorID
        Dim cmdCOA As OdbcCommand
        Dim drCOA As OdbcDataReader
        AksesDatabase_Transaksi(Buka)
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_Transaksi WHERE Nomor_JV = '" & Microsoft.VisualBasic.Mid(NomorJV, PanjangTeks_AwalanNomorJV_Plus1) & "' ", KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        DataGridView.Rows.Clear()
        DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Do While dr.Read
            NomorUrut = NomorUrut + 1
            clm_NoUrut = NomorUrut & "."
            COA = dr.Item("COA")
            cmdCOA = New OdbcCommand(" SELECT * FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            drCOA = cmdCOA.ExecuteReader
            drCOA.Read()
            If drCOA.HasRows Then
                NamaAkun = drCOA.Item("Nama_Akun")
            Else
                NamaAkun = teks_CoaBelumTerdaftar
                'StatusPosting = "BATAL"            <======== KODE INI UNTUK SEMENTARA TIDAK DIPAKAI.
            End If
            DK = dr.Item("D_K")
            JumlahDebet = dr.Item("Jumlah_Debet")
            JumlahKredit = dr.Item("Jumlah_Kredit")
            clm_JumlahDebet = JumlahDebet
            clm_JumlahKredit = JumlahKredit
            If JumlahDebet = 0 Then
                clm_JumlahDebet = StripKosong
                NamaAkun = PenjorokNamaAkun & NamaAkun
            End If
            If JumlahKredit = 0 Then
                clm_JumlahKredit = StripKosong
            End If
            TotalDebet = TotalDebet + JumlahDebet
            TotalKredit = TotalKredit + JumlahKredit
            NomorID = dr.Item("Nomor_ID")
            DataGridView.Rows.Add(clm_NoUrut, COA, NamaAkun, DK, clm_JumlahDebet, clm_JumlahKredit, NomorID)
        Loop
        AksesDatabase_General(Tutup)
        AksesDatabase_Transaksi(Tutup)
        If StatusPosting = "BATAL" Then
            MsgBox("Proses GAGAL, karena ada COA yang belum terdaftar." & _
                Enter2Baris & "Silakan perbaiki dan sesuaikan sumber data, kemudian ulangi lagi.")
            frm_ProgressImportJurnal.HapusSemuaDataPostinganJurnalEventIni()
            Me.Close()
        End If
        JumlahBaris = DataGridView.RowCount 'Jangan dihapus, dan jangan dipindahkan..!!!
        JumlahBarisBahanJV = JumlahBaris 'Jangan dihapus, dan jangan dipindahkan..!!!
        DataGridView.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!
        DataGridView.Rows.Add() 'Jangan dihapus, dan jangan dipindahkan..!!!

        NotifBalance()

        DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
        DataGridView.ClearSelection()

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

        If TotalKredit = 0 And TotalDebet > 0 Then
            lbl_StatusBalance.Text = "Tidak ada Kredit"
            btn_OK.Enabled = False
        Else
        End If
        If TotalDebet = 0 And TotalKredit > 0 Then
            lbl_StatusBalance.Text = "Tidak ada Debet"
            btn_OK.Enabled = False
        End If

    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentClick
    End Sub
    Private Sub DataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellClick

        BarisTerseleksi = DataGridView.CurrentRow.Index
        Dim COATerseleksi = DataGridView.Item("Kode_Akun", BarisTerseleksi).Value
        If COATerseleksi = Nothing Then Return
        'If Microsoft.VisualBasic.Left(Referensi, 3) = "NPPHU" Or Microsoft.VisualBasic.Left(Referensi, 4) = "PEMB" Then
        Dim NamaAkunTerseleksi = DataGridView.Item("Nama_Akun", BarisTerseleksi).Value
        Dim DK = DataGridView.Item("D_K", BarisTerseleksi).Value
        Dim JumlahDebet As Int64 = AmbilAngka(DataGridView.Item("Debet", BarisTerseleksi).Value)
        Dim JumlahKredit As Int64 = AmbilAngka(DataGridView.Item("Kredit", BarisTerseleksi).Value)
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

        DataGridView.Item("Kode_Akun", BarisTerseleksi).Value = frm_InputJurnalPerTransaksi.COATerseleksi
        DataGridView.Item("Nama_Akun", BarisTerseleksi).Value = frm_InputJurnalPerTransaksi.NamaAkunTerseleksi
        DK = frm_InputJurnalPerTransaksi.DK
        Dim JumlahTransaksi As Int64 = AmbilAngka(frm_InputJurnalPerTransaksi.txt_JumlahTransaksi.Text)
        If DK = "D" Then
            DataGridView.Item("D_K", BarisTerseleksi).Value = "D"
            DataGridView.Item("Debet", BarisTerseleksi).Value = JumlahTransaksi
            DataGridView.Item("Kredit", BarisTerseleksi).Value = StripKosong
        End If
        If DK = "K" Then
            DataGridView.Item("D_K", BarisTerseleksi).Value = "K"
            DataGridView.Item("Debet", BarisTerseleksi).Value = StripKosong
            DataGridView.Item("Kredit", BarisTerseleksi).Value = JumlahTransaksi
        End If

        Dim Baris = 0
        TotalDebet = 0
        TotalKredit = 0
        Do While Baris < JumlahBaris
            JumlahDebet = AmbilAngka(DataGridView.Item("Debet", Baris).Value)
            JumlahKredit = AmbilAngka(DataGridView.Item("Kredit", Baris).Value)
            TotalDebet = TotalDebet + JumlahDebet
            TotalKredit = TotalKredit + JumlahKredit
            Baris = Baris + 1
        Loop

        NotifBalance()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click

        If TotalDebet <> TotalKredit Then
            MsgBox("Jurnal tidak dapat diposting karena ADA SELISIH." & _
                   Enter2Baris & "Silakan dikoreksi kembali.")
            Return
        End If

        Dim PilihPosting = MessageBox.Show("Jurnal yang Anda posting tidak akan bisa diedit lagi." & Enter2Baris & "Yakin data sudah benar..?", "Perhatian..!", MessageBoxButtons.YesNo)

        If PilihPosting = vbYes Then
            Dim Baris = 0
            Dim NomorID
            Dim COA
            Dim DK = ""
            Dim JumlahDebet As Int64 = 0
            Dim JumlahKredit As Int64 = 0
            Dim QueryEdit, QueryEdit1, QueryEdit2, QueryEdit3
            AksesDatabase_Transaksi(Buka)
            Do While Baris < JumlahBaris
                NomorID = DataGridView.Item("Nomor_ID", Baris).Value
                COA = DataGridView.Item("Kode_Akun", Baris).Value
                DK = DataGridView.Item("D_K", Baris).Value
                JumlahDebet = AmbilAngka(DataGridView.Item("Debet", Baris).Value)
                JumlahKredit = AmbilAngka(DataGridView.Item("Kredit", Baris).Value)
                QueryEdit1 = " UPDATE tbl_Transaksi SET "
                QueryEdit2 = " COA = '" & COA & "', D_K = '" & DK & "', Jumlah_Debet = '" & JumlahDebet & "', Jumlah_Kredit = '" & JumlahKredit & "', "
                QueryEdit3 = " Status_Approve = 1 WHERE Nomor_ID = '" & NomorID & "' "
                QueryEdit = QueryEdit1 & QueryEdit2 & QueryEdit3
                cmd = New OdbcCommand(QueryEdit, KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                Baris = Baris + 1
            Loop
            AksesDatabase_Transaksi(Tutup)
            Me.Close()
        End If

    End Sub

    Private Sub btn_BuangJurnal_Click(sender As Object, e As EventArgs) Handles btn_BuangJurnal.Click

        Dim PilihBuang = MessageBox.Show("Jurnal yang Anda buang tidak akan terposting pada event ini." & Enter2Baris & "Yakin akan membuang..?", "Perhatian..!", MessageBoxButtons.YesNo)

        If PilihBuang = vbYes Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand("DELETE FROM tbl_Transaksi WHERE Nomor_JV = " & jur_NomorJV & " ", KoneksiDatabaseTransaksi)
            cmd.ExecuteNonQuery()
            AksesDatabase_Transaksi(Tutup)
            jur_StatusPenyimpananJurnal_Lengkap = False
            HasilPosting = "BERMASALAH"
            Me.Close()
        End If

    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click

        frm_ProgressImportJurnal.TanyaBatalPostingJurnal()
        If StatusPosting = "BATAL" Then
            Me.Close()
        End If

    End Sub

End Class