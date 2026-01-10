Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputCOA

    Public FungsiForm
    Public JalurMasuk
    Dim JumlahDigitCOA = 5
    Public ProsesSuntingDatabase As Boolean

    Dim COA
    Dim NamaAkun
    Dim DK
    Dim SaldoAwal
    Dim Uraian
    Dim Visibilitas

    Private Sub frm_InputCOA_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_TAMBAH Then
            Me.Text = "Input Data COA"
            BeginInvoke(Sub() txt_COA.Focus())
        End If

        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit Data COA"
            txt_COA.Enabled = False
            btn_Reset.Enabled = False
            btn_Tutup.Text = "Batal"
            If JenisTahunBuku = JenisTahunBuku_NORMAL Then txt_SaldoAwal.Enabled = False
        End If

        'Kelompok coding ini nanti harus dihapus.
        If JalurMasuk = Halaman_TUTUPBUKU _
            Or JalurMasuk = Halaman_SALDOAWALHUTANGUSAHA _
            Or JalurMasuk = Halaman_BUKUPENGAWASANHUTANGPPHPASAL25 _
            Then
            txt_COA.Enabled = False
            txt_NamaAkun.Enabled = False
            txt_SaldoAwal.Enabled = True
            txt_Uraian.Enabled = False
            cmb_Visibilitas.Enabled = False
        End If

        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
            lbl_SaldoAwal.Text = "Saldo Akhir"
        Else
            lbl_SaldoAwal.Text = "Saldo Awal"
        End If

    End Sub

    Public Sub ResetForm()

        txt_COA.Enabled = True
        txt_NamaAkun.Enabled = True
        cmb_DebetKredit.Enabled = False
        txt_SaldoAwal.Enabled = False 'Sejatinya Saldo Awal itu tidak bisa diedit. Dia adalah konsekuensi dari Saldo Akhir Tahun sebelumnya. Saldo Awal hanya bisa diedit pada Jenis Tahun Buku Backup.
        txt_Uraian.Enabled = True
        cmb_Visibilitas.Enabled = True
        btn_Reset.Enabled = True

        KontenComboDebetKredit()
        KontenComboVisibilitas()
        txt_COA.Text = Nothing
        txt_NamaAkun.Text = Nothing
        cmb_DebetKredit.Text = Nothing
        txt_SaldoAwal.Text = Nothing
        txt_Uraian.Text = Nothing
        cmb_Visibilitas.Text = "YA"
        btn_Tutup.Text = "Tutup"

        ProsesSuntingDatabase = False

    End Sub

    Sub KontenComboDebetKredit()
        cmb_DebetKredit.Items.Clear()
        cmb_DebetKredit.Items.Add("DEBET")
        cmb_DebetKredit.Items.Add("KREDIT")
    End Sub

    Sub KontenComboVisibilitas()
        cmb_Visibilitas.Items.Clear()
        cmb_Visibilitas.Items.Add("YA")
        cmb_Visibilitas.Items.Add("TIDAK")
    End Sub

    Private Sub txt_COA_TextChanged(sender As Object, e As EventArgs) Handles txt_COA.TextChanged
        COA = txt_COA.Text
        cmb_DebetKredit.Text = PenentuanDEBETKREDIT_COA(COA)
        If Len(COA) = 5 Then
            If FungsiForm = FungsiForm_TAMBAH Then
                Dim KodeCOATersedia As Boolean = True
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT COA FROM tbl_COA WHERE COA = '" & COA & " ' ", KoneksiDatabaseGeneral)
                dr_ExecuteReader()
                dr.Read()
                If dr.HasRows Then KodeCOATersedia = False
                AksesDatabase_General(Tutup)
                If KodeCOATersedia = False Then
                    MsgBox("Kode COA " & COA & " sudah ada..!" & Enter2Baris &
                       "Silakan masukkan 'Kode COA' yang lain.")
                    txt_COA.Text = Kosongan
                    txt_COA.Focus()
                    Return
                End If
            End If
            If AmbilAngka(COA) >= AwalAkunBiaya Then
                txt_SaldoAwal.Enabled = False
                txt_SaldoAwal.Text = Nothing
            Else
                If JenisTahunBuku = JenisTahunBuku_LAMPAU Then
                    txt_SaldoAwal.Enabled = True
                Else
                    txt_SaldoAwal.Enabled = False
                End If
            End If
        End If
    End Sub
    Private Sub txt_COA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub txt_NamaAkun_TextChanged(sender As Object, e As EventArgs) Handles txt_NamaAkun.TextChanged
        NamaAkun = txt_NamaAkun.Text
    End Sub

    Private Sub cmb_DebetKredit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DebetKredit.SelectedIndexChanged
    End Sub
    Private Sub cmb_DebetKredit_TextChanged(sender As Object, e As EventArgs) Handles cmb_DebetKredit.TextChanged
        DK = cmb_DebetKredit.Text
    End Sub
    Private Sub cmb_DebetKredit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_DebetKredit.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub txt_Saldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SaldoAwal.KeyPress
        HanyaBolehInputAngkaPlusMinus(sender, e)
    End Sub

    Private Sub txt_Saldo_TextChanged(sender As Object, e As EventArgs) Handles txt_SaldoAwal.TextChanged
        SaldoAwal = AmbilAngka(txt_SaldoAwal.Text)
        PemecahRibuanUntukTextBox(txt_SaldoAwal)
    End Sub

    Private Sub txt_Uraian_TextChanged(sender As Object, e As EventArgs) Handles txt_Uraian.TextChanged
        Uraian = txt_Uraian.Text
    End Sub

    Private Sub cmb_Visibilitas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Visibilitas.SelectedIndexChanged
    End Sub
    Private Sub cmb_Visibilitas_TextChanged(sender As Object, e As EventArgs) Handles cmb_Visibilitas.TextChanged
        Visibilitas = cmb_Visibilitas.Text
    End Sub




    Public Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Isi Ulang Variabel :
        Uraian = txt_Uraian.Text

        'Validasi Form :
        If COA = Kosongan Then
            MsgBox("Silakan isi kolom 'Kode Akun'")
            txt_COA.Focus()
            Return
        End If

        If PanjangTeks(COA) <> JumlahDigitCOA Then
            MsgBox("Jumlah Digit COA harus 5.")
            txt_COA.Focus()
            Return
        End If

        If AmbilAngka(COA) >= 90000 Then
            MsgBox("Angka 9 tidak diperkenankan sebagai digit awal COA.")
            txt_COA.Focus()
            Return
        End If

        If NamaAkun = Kosongan Then
            MsgBox("Silakan isi kolom 'Nama Akun'")
            txt_NamaAkun.Focus()
            Return
        End If

        If DK = Kosongan Then
            MsgBox("Silakan pilih 'Debet/Kredit'")
            cmb_DebetKredit.Focus()
            Return
        End If

        'TrialBalance_Mentahkan() 'Mentahkan data dari awal lebih baik (Sebelum proses tambah COA maupun proses edit COA)

        'Jika Bermaksud Menambah Data COA Baru :
        If FungsiForm = FungsiForm_TAMBAH Then

            'Simpan Data ke Tabel COA
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" INSERT INTO tbl_COA VALUES (" &
                                  " '" & COA & "', " &
                                  " '" & NamaAkun & "', " &
                                  " '" & DK & "', " &
                                  " '" & SaldoAwal & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & 0 & "', " &
                                  " '" & Uraian & "', " &
                                  " '" & Visibilitas & "' " &
                                  ") ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
                AksesDatabase_General(Tutup)
            End Try
            AksesDatabase_General(Tutup)

            'Penyimpanan Data Saldo Awal COA ke seluruh Database Tahun Buku (Saldo Awal dibikin 0 semua dulu) :
            If ProsesSuntingDatabase = True Then
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" SELECT * FROM tbl_InfoData ", KoneksiDatabaseGeneral)
                dr = cmd.ExecuteReader
                Dim cmdSimpanCOA As New OdbcCommand
                Do While dr.Read
                    BukaDatabaseTransaksi_Alternatif(dr.Item("Tahun_Buku"))
                    cmdSimpanCOA = New OdbcCommand(" INSERT INTO tbl_SaldoAwalCOA (COA, Saldo_Awal) VALUES ('" & COA & "', 0) ", KoneksiDatabaseTransaksi_Alternatif)
                    Try
                        cmdSimpanCOA.ExecuteNonQuery()
                        ProsesSuntingDatabase = True
                    Catch ex As Exception
                        ProsesSuntingDatabase = False
                        AksesDatabase_Transaksi(Tutup)
                        Exit Do
                    End Try
                    TutupDatabaseTransaksi_Alternatif()
                Loop
                AksesDatabase_General(Tutup)
            End If

        End If

        'Jika Bermaksud Edit Data COA yang sudah ada :
        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_COA SET Nama_Akun = '" & NamaAkun & "', " &
                                  " D_K = '" & DK & "', " &
                                  " Saldo_Awal = '" & SaldoAwal & "', " &
                                  " Uraian = '" & Uraian & "', " &
                                  " Visibilitas = '" & Visibilitas & "' " &
                                  " WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)
        End If

        'Simpan Saldo Awal pada Tahun Buku Aktif (Kode ini berlaku untuk PENYIMPANAN maupun EDIT COA), karena posisinya di luar IF Tambah atau Edit)
        If ProsesSuntingDatabase = True Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_SaldoAwalCOA SET Saldo_Awal = '" & SaldoAwal & "' WHERE COA = '" & COA & "' ", KoneksiDatabaseTransaksi)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
                AksesDatabase_Transaksi(Tutup)
            End Try
            AksesDatabase_Transaksi(Tutup)
        End If
        If ProsesSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then
                btn_Reset_Click(sender, e)
                MsgBox("Data COA BERHASIL disimpan.")
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data COA BERHASIL diedit.")
                Select Case JalurMasuk
                    Case Halaman_DATACOA
                        usc_DataCOA.rowviewUtama("COA_") = COA
                        usc_DataCOA.rowviewUtama("Nama_Akun") = NamaAkun
                        usc_DataCOA.rowviewUtama("D_K") = DK
                        If SaldoAwal > 0 Then
                            usc_DataCOA.rowviewUtama("Saldo_Awal") = SaldoAwal
                        Else
                            usc_DataCOA.rowviewUtama("Saldo_Awal") = StripKosong
                        End If
                        usc_DataCOA.rowviewUtama("Uraian_") = Uraian
                        usc_DataCOA.rowviewUtama("Visibilitas_") = Visibilitas
                    Case Halaman_SALDOAWALHUTANGUSAHA
                        frm_SaldoAwalHutangUsaha.RefreshTampilanData()
                    Case Halaman_TUTUPBUKU
                        If usc_TutupBuku.StatusAktif Then usc_TutupBuku.TampilkanData()
                End Select
                Me.Close()
            End If
            If usc_DataCOA.StatusAktif Then usc_DataCOA.TampilkanData()
            'frm_DataCOA.RefreshTampilanData() '(Sengaja pakai RefreshTampilanData() bukan pakai TampilkanData(), agar otomatis bisa mengecek Keseimbangan Neraca di Data COA).
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" DELETE FROM tbl_COA WHERE COA = '" & COA & "' ", KoneksiDatabaseGeneral)
                cmd.ExecuteNonQuery()
                AksesDatabase_General(Tutup)
                MsgBox("Data COA GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data COA GAGAL diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
                'Jika penyimpanan gagal, tidak perlu revisi value Saldo Awal di tbl_COA, karena dengan sendirinya value tersebut akan ter-update sesuai value dari tbl_SaldoAwalCOA di databse transaksi secara otomatis saat masuk Tahun Buku.
            End If
        End If

    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class