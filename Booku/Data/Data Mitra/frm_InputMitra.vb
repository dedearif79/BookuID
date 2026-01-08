Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputMitra

    Public FungsiForm
    Public StatusEdit

    Private Sub frm_InputMitra_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_TAMBAH Then
            ResetForm()
            Me.Text = "Input Data Mitra"
            txt_KodeMitra.Enabled = True
            btn_Reset.Enabled = True
        End If
        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit Data Mitra"
            txt_KodeMitra.Enabled = False
            btn_Reset.Enabled = False
            IsiValueForm()
        End If
        If FungsiForm = Nothing Then
            PesanUntukProgrammer("Tentukan Status Form Dulu..!")
            Me.Close()
        End If

    End Sub

    Sub ResetForm()
        txt_KodeMitra.Text = Nothing
        txt_NamaMitra.Text = Nothing
        chk_Afiliasi.Checked = False
        chk_Afiliasi.Enabled = True
        chk_Supplier.Checked = False
        chk_Supplier.Enabled = True
        chk_Customer.Checked = False
        chk_Customer.Enabled = True
        chk_Keuangan.Checked = False
        chk_Keuangan.Enabled = True
        chk_PKP.Checked = False
        chk_PKP.Enabled = True
        chk_PJK.Checked = False
        chk_PJK.Enabled = True
        txt_NPWP.Text = Nothing
        KontenComboJenisWP()
        KontenComboLokasiWP()
        txt_Alamat.Text = Nothing
        txt_Email.Text = Nothing
        txt_PIC.Text = Nothing
        txt_RekeningBank.Text = Nothing
        txt_AtasNama.Text = Nothing
    End Sub

    Sub KontenComboJenisWP()
        cmb_JenisWP.Items.Clear()
        cmb_JenisWP.Items.Add("Orang Pribadi")
        cmb_JenisWP.Items.Add("Badan Hukum")
        cmb_JenisWP.Text = Nothing
    End Sub

    Sub KontenComboLokasiWP()
        cmb_LokasiWP.Items.Clear()
        cmb_LokasiWP.Items.Add("Dalam Negeri")
        cmb_LokasiWP.Items.Add("Luar Negeri")
        cmb_LokasiWP.Text = Nothing
    End Sub

    Sub IsiValueForm()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & txt_KodeMitra.Text & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        txt_NamaMitra.Text = dr.Item("Nama_Mitra")
        If dr.Item("Afiliasi") = 1 Then
            chk_Afiliasi.Checked = True
            'chk_Afiliasi.Enabled = False
        Else
            chk_Afiliasi.Checked = False
        End If
        If dr.Item("Supplier") = 1 Then
            chk_Supplier.Checked = True
            'chk_Supplier.Enabled = False
        Else
            chk_Supplier.Checked = False
        End If
        If dr.Item("Customer") = 1 Then
            chk_Customer.Checked = True
            'chk_Customer.Enabled = False
        Else
            chk_Customer.Checked = False
        End If
        If dr.Item("Keuangan") = 1 Then
            chk_Keuangan.Checked = True
            'chk_Keuangan.Enabled = False
        Else
            chk_Keuangan.Checked = False
        End If
        If dr.Item("PKP") = 1 Then
            chk_PKP.Checked = True
            'chk_PKP.Enabled = False
        Else
            chk_PKP.Checked = False
        End If
        If dr.Item("PJK") = 1 Then
            chk_PJK.Checked = True
            'chk_PJK.Enabled = False
        Else
            chk_PJK.Checked = False
        End If
        txt_NPWP.Text = dr.Item("NPWP")
        cmb_JenisWP.Text = dr.Item("Jenis_WP")
        cmb_LokasiWP.Text = dr.Item("Lokasi_WP")
        txt_Alamat.Text = dr.Item("Alamat")
        txt_Email.Text = dr.Item("Email")
        txt_PIC.Text = dr.Item("PIC")
        txt_RekeningBank.Text = dr.Item("Rekening_Bank")
        txt_AtasNama.Text = dr.Item("Atas_Nama")
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub txt_KodeMitra_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeMitra.TextChanged
    End Sub
    Private Sub txt_KodeMitra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeMitra.KeyPress
        HanyaBoleh_Huruf_Angka_dan_Strip(sender, e)
    End Sub
    Private Sub txt_KodeMitra_Leave(sender As Object, e As EventArgs) Handles txt_KodeMitra.Leave
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & txt_KodeMitra.Text & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Dim NamaMitra = dr.Item("Nama_Mitra")
            MsgBox("Kode '" & txt_KodeMitra.Text & "' sudah terdaftar" & Enter1Baris & "untuk " & NamaMitra & "." & Enter2Baris & "Silakan masukkan kode yang lain.")
            txt_KodeMitra.Text = Nothing
            txt_KodeMitra.Focus()
            AksesDatabase_General(Tutup)
            Return
        End If
        AksesDatabase_General(Tutup)
    End Sub

    Private Sub txt_NamaMitra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NamaMitra.KeyPress
        TolakKarakterTertentu(sender, e)
    End Sub

    Private Sub txt_NPWP_TextChanged(sender As Object, e As EventArgs) Handles txt_NPWP.TextChanged
    End Sub
    Private Sub txt_NPWP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_NPWP.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub

    Private Sub cmb_JenisWP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisWP.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisWP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisWP.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        StatusEdit = "EDIT"

        'Validasi Form :
        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & txt_KodeMitra.Text & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                Dim NamaMitra = dr.Item("Nama_Mitra")
                MsgBox("Kode '" & txt_KodeMitra.Text & "' sudah terdaftar" & Enter1Baris & "untuk " & NamaMitra & "." & Enter2Baris & "Silakan masukkan kode yang lain.")
                txt_KodeMitra.Text = Nothing
                txt_KodeMitra.Focus()
                AksesDatabase_General(Tutup)
                Return
            End If
            AksesDatabase_General(Tutup)
        End If

        If txt_KodeMitra.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Kode Mitra'.")
            txt_KodeMitra.Focus()
            Return
        End If

        If txt_NamaMitra.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Nama Mitra'.")
            txt_NamaMitra.Focus()
            Return
        End If

        'If chk_Supplier.Checked = False And chk_Customer.Checked = False Then
        '    MsgBox("Silakan pilih kategori mitra, sebagai Supplier dan/atau Customer.")
        '    chk_Supplier.Focus()
        '    Return
        'End If

        'If txt_NPWP.Text = Nothing Then
        '    MsgBox("Silakan isi kolom 'NPWP'.")
        '    txt_NPWP.Focus()
        '    Return
        'End If

        If cmb_JenisWP.Text = Nothing Then
            MsgBox("Silakan pilih 'Jenis WP (Wajib Pajak)'")
            cmb_JenisWP.Focus()
            Return
        End If

        If cmb_LokasiWP.Text = Nothing Then
            MsgBox("Silakan pilih 'Lokasi WP (Wajib Pajak)'")
            cmb_LokasiWP.Focus()
            Return
        End If

        If txt_Alamat.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Alamat'.")
            txt_Alamat.Focus()
            Return
        End If

        If txt_Email.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Email'.")
            txt_Email.Focus()
            Return
        End If

        If txt_PIC.Text = Nothing Then
            MsgBox("Silakan isi kolom 'PIC'.")
            txt_PIC.Focus()
            Return
        End If

        If FungsiForm = FungsiForm_TAMBAH Then
            Dim KodeMitra = " '" & txt_KodeMitra.Text & "', "
            Dim NamaMitra = " '" & txt_NamaMitra.Text & "', "
            Dim SebagaiAfiliasi
            If chk_Afiliasi.Checked = True Then
                SebagaiAfiliasi = " 1, "
            Else
                SebagaiAfiliasi = " 0, "
            End If
            Dim SebagaiSupplier
            If chk_Supplier.Checked = True Then
                SebagaiSupplier = " 1, "
            Else
                SebagaiSupplier = " 0, "
            End If
            Dim SebagaiCustomer
            If chk_Customer.Checked = True Then
                SebagaiCustomer = " 1, "
            Else
                SebagaiCustomer = " 0, "
            End If
            Dim SebagaiKeuangan
            If chk_Keuangan.Checked = True Then
                SebagaiKeuangan = " 1, "
            Else
                SebagaiKeuangan = " 0, "
            End If
            Dim SebagaiPKP
            If chk_PKP.Checked = True Then
                SebagaiPKP = " 1, "
            Else
                SebagaiPKP = " 0, "
            End If
            Dim SebagaiPJK
            If chk_PJK.Checked = True Then
                SebagaiPJK = " 1, "
            Else
                SebagaiPJK = " 0, "
            End If
            Dim NPWP = " '" & txt_NPWP.Text & "', "
            Dim JenisWP = " '" & cmb_JenisWP.Text & "', "
            Dim LokasiWP = " '" & cmb_LokasiWP.Text & "', "
            Dim Alamat = " '" & txt_Alamat.Text & "', "
            Dim Email = " '" & txt_Email.Text & "', "
            Dim PIC = " '" & txt_PIC.Text & "', "
            Dim RekeningBank = " '" & txt_RekeningBank.Text & "', "
            Dim AtasNama = " '" & txt_AtasNama.Text & "' "
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" INSERT INTO tbl_LawanTransaksi VALUES (" & KodeMitra & NamaMitra &
                                  SebagaiAfiliasi & SebagaiSupplier & SebagaiCustomer & SebagaiKeuangan & SebagaiPKP & SebagaiPJK &
                                  NPWP & JenisWP & LokasiWP & Alamat & Email & PIC & RekeningBank & AtasNama & ") ", KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                StatusSuntingDatabase = True
            Catch ex As Exception
                StatusSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)

        End If

        If FungsiForm = FungsiForm_EDIT Then
            Dim KodeMitra = txt_KodeMitra.Text
            Dim NamaMitra = " Nama_Mitra = '" & txt_NamaMitra.Text & "', "
            Dim SebagaiAfiliasi
            If chk_Afiliasi.Checked = True Then
                SebagaiAfiliasi = " Afiliasi = 1, "
            Else
                SebagaiAfiliasi = " Afiliasi = 0, "
            End If
            Dim SebagaiSupplier
            If chk_Supplier.Checked = True Then
                SebagaiSupplier = " Supplier = 1, "
            Else
                SebagaiSupplier = " Supplier = 0, "
            End If
            Dim SebagaiCustomer
            If chk_Customer.Checked = True Then
                SebagaiCustomer = " Customer = 1, "
            Else
                SebagaiCustomer = " Customer = 0, "
            End If
            Dim SebagaiKeuangan
            If chk_Keuangan.Checked = True Then
                SebagaiKeuangan = " Keuangan = 1, "
            Else
                SebagaiKeuangan = " Keuangan = 0, "
            End If
            Dim SebagaiPKP
            If chk_PKP.Checked = True Then
                SebagaiPKP = " PKP = 1, "
            Else
                SebagaiPKP = " PKP = 0, "
            End If
            Dim SebagaiPJK
            If chk_PJK.Checked = True Then
                SebagaiPJK = " PJK = 1, "
            Else
                SebagaiPJK = " PJK = 0, "
            End If
            Dim NPWP = " NPWP = '" & txt_NPWP.Text & "', "
            Dim JenisWP = " Jenis_WP = '" & cmb_JenisWP.Text & "', "
            Dim lokasiWP = " Lokasi_WP = '" & cmb_LokasiWP.Text & "', "
            Dim Alamat = " Alamat = '" & txt_Alamat.Text & "', "
            Dim Email = " Email = '" & txt_Email.Text & "', "
            Dim PIC = " PIC = '" & txt_PIC.Text & "', "
            Dim RekeningBank = " Rekening_Bank = '" & txt_RekeningBank.Text & "', "
            Dim AtasNama = " Atas_Nama = '" & txt_AtasNama.Text & "' "
            AksesDatabase_General(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" UPDATE tbl_LawanTransaksi SET " & NamaMitra & SebagaiAfiliasi & SebagaiSupplier & SebagaiCustomer & SebagaiKeuangan & SebagaiPKP & SebagaiPJK &
                                  NPWP & JenisWP & lokasiWP & Alamat & Email & PIC & RekeningBank & AtasNama & " WHERE Kode_Mitra = '" & KodeMitra & "' ",
                                  KoneksiDatabaseGeneral)
            Try
                cmd.ExecuteNonQuery()
                StatusSuntingDatabase = True
            Catch ex As Exception
                StatusSuntingDatabase = False
            End Try
            AksesDatabase_General(Tutup)
        End If

        If StatusSuntingDatabase = True Then
            If FungsiForm = FungsiForm_TAMBAH Then
                MsgBox("Data Mitra berhasil disimpan.")
                win_ListLawanTransaksi.TampilkanData()
                frm_DataMitra.TampilkanData()
                btn_Reset_Click(sender, e)
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data Mitra berhasil diedit.")
                frm_DataMitra.TampilkanData()
            End If
            Me.Close()
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                AksesDatabase_General(Buka)
                cmd = New OdbcCommand(" DELETE FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & txt_KodeMitra.Text & "' ", KoneksiDatabaseGeneral)
                cmd.ExecuteNonQuery()
                AksesDatabase_General(Tutup)
                MsgBox("Data Mitra GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data Mitra GAGAL diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
        End If

    End Sub

    Public Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        StatusEdit = "BATAL"
        Me.Close()
    End Sub

End Class