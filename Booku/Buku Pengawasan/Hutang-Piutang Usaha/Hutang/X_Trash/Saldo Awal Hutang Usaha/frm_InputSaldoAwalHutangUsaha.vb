Imports bcomm
Imports System.Data.Odbc

Public Class frm_InputSaldoAwalHutangUsaha

    Public FungsiForm
    Dim ProsesSuntingDatabase As Boolean
    Dim DPP, PPN, PPhDipotong
    Dim JumlahTagihan, JumlahBayar, SisaHutang

    Private Sub frm_InputSaldoAwalHutangUsaha_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FungsiForm = FungsiForm_EDIT Then
            Me.Text = "Edit Data Sisa Hutang Usaha"
            txt_NomorPembelian.Enabled = False
            btn_Reset.Enabled = False
        End If
        If FungsiForm = FungsiForm_TAMBAH Then
            Me.Text = "Input Data Sisa Hutang Usaha"
            txt_NomorPembelian.Enabled = True
            btn_Reset_Click(sender, e)
            btn_Reset.Enabled = True
        End If

    End Sub

    Sub PerhitunganJumlahTagihan()

        DPP = AmbilAngka(txt_DPP.Text)
        PPN = AmbilAngka(txt_PPN.Text)
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)

        JumlahTagihan = DPP + PPN - PPhDipotong

        If JumlahTagihan = 0 Then
            txt_JumlahTagihan.Text = Nothing
        Else
            txt_JumlahTagihan.Text = JumlahTagihan
        End If

        PerhitunganSisaHutang()

    End Sub

    Sub PerhitunganSisaHutang()

        JumlahTagihan = AmbilAngka(txt_JumlahTagihan.Text)
        JumlahBayar = AmbilAngka(txt_JumlahBayar.Text)
        SisaHutang = JumlahTagihan - JumlahBayar

        If SisaHutang = 0 Then
            txt_SisaHutang.Text = Nothing
        Else
            txt_SisaHutang.Text = SisaHutang
        End If

    End Sub

    Private Sub btn_Tutup_Click(sender As Object, e As EventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As EventArgs) Handles btn_Simpan.Click

        'Validasi Form :

        If txt_NomorPembelian.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Nomor Pembelian'")
            txt_NomorPembelian.Focus()
            Return
        End If

        If txt_Referensi.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Referensi/BPHU'")
            txt_Referensi.Focus()
            Return
        End If

        If txt_NomorInvoice.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Nomor Invoice")
            txt_NomorInvoice.Focus()
            Return
        End If

        If txt_KodeSupplier.Text = Nothing Then
            MsgBox("Silakan pilih Supplier")
            txt_KodeSupplier.Focus()
            Return
        End If

        If txt_NamaBarang.Text = Nothing And txt_NamaJasa.Text = Nothing Then
            MsgBox("Silakan isi kolom 'Nama Barang' dan/atau kolom 'Nama Jasa'")
            txt_NamaBarang.Focus()
            Return
        End If

        If txt_DPP.Text = Nothing Then
            MsgBox("Silakan isi kolom 'DPP'")
            txt_DPP.Focus()
            Return
        End If

        TrialBalance_Mentahkan() 'Mentahkan data dari awal lebih baik.

        Dim NomorPembelian = txt_NomorPembelian.Text
        Dim Referensi = txt_Referensi.Text
        Dim TanggalInvoice = Microsoft.VisualBasic.Left(dtp_TanggalInvoice.Value, 10)
        Dim NomorInvoice = txt_NomorInvoice.Text
        Dim NomorFakturPajak = txt_NomorFakturPajak.Text
        Dim TanggalFakturPajak
        If txt_NomorFakturPajak.Text = Nothing Then
            TanggalFakturPajak = TanggalKosong
        Else
            TanggalFakturPajak = Microsoft.VisualBasic.Left(dtp_TanggalFakturPajak.Value, 10)
        End If
        Dim KodeSupplier = txt_KodeSupplier.Text
        Dim NamaSupplier = txt_NamaSupplier.Text
        Dim NamaBarang = txt_NamaBarang.Text
        Dim NamaJasa = txt_NamaJasa.Text
        DPP = AmbilAngka(txt_DPP.Text)
        PPN = AmbilAngka(txt_PPN.Text)
        PPhDipotong = AmbilAngka(txt_PPhDipotong.Text)
        JumlahTagihan = AmbilAngka(txt_JumlahTagihan.Text)
        JumlahBayar = AmbilAngka(txt_JumlahBayar.Text)
        SisaHutang = AmbilAngka(txt_SisaHutang.Text)
        Dim DueDate = Microsoft.VisualBasic.Left(dtp_DueDate.Value, 10)
        Dim StatusLunas = Nothing
        Dim Keterangan = txt_Keterangan.Text

        'Jika Bermaksud Menambah Data Baru Sisa Hutang Usaha :
        If FungsiForm = FungsiForm_TAMBAH Then

            'Simpan Data ke Tabel Sisa Hutang Usaha
            AksesDatabase_Transaksi(Buka)
            If StatusKoneksiDatabase = False Then Return
            cmd = New OdbcCommand(" INSERT INTO tbl_SisaHutangUsaha VALUES (" & _
                                  " '" & NomorPembelian & "', " & _
                                  " '" & Referensi & "', " & _
                                  " '" & TanggalFormatSimpan(TanggalInvoice) & "', " & _
                                  " '" & NomorInvoice & "', " & _
                                  " '" & TanggalFormatSimpan(TanggalFakturPajak) & "', " & _
                                  " '" & NomorFakturPajak & "', " & _
                                  " '" & KodeSupplier & "', " & _
                                  " '" & NamaSupplier & "', " & _
                                  " '" & NamaBarang & "', " & _
                                  " '" & NamaJasa & "', " & _
                                  " '" & DPP & "', " & _
                                  " '" & PPN & "', " & _
                                  " '" & PPhDipotong & "', " & _
                                  " '" & JumlahTagihan & "', " & _
                                  " '" & TanggalFormatSimpan(DueDate) & "', " & _
                                  " '" & StatusLunas & "', " & _
                                  " '" & Keterangan & "' " & _
                                  ") ", KoneksiDatabaseTransaksi)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
                AksesDatabase_Transaksi(Tutup)
            End Try
            AksesDatabase_Transaksi(Tutup)

            'Simpan value Jumlah_Bayar ke tbl_PembayaranHutangUsaha
            Dim NomorIDBPHU = AmbilAngka(AmbilNomorIdTerakhir(DatabaseTransaksi, "tbl_PembayaranHutangUsaha") + 1)
            Dim NomorBPHU = Referensi
            Dim NPPHU = Nothing
            Dim TanggalBayar = AkhirTahunBukuKemarin
            Dim COAKredit = Nothing
            Dim NomorJV = 0
            Dim User = UserAktif
            If ProsesSuntingDatabase = True Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" INSERT INTO tbl_PembayaranHutangUsaha VALUES (" & _
                                      " '" & NomorIDBPHU & "', " & _
                                      " '" & NomorBPHU & "', " & _
                                      " '" & NomorPembelian & "', " & _
                                      " '" & NPPHU & "', " & _
                                      " '" & KodeSupplier & "', " & _
                                      " '" & NamaSupplier & "', " & _
                                      " '" & TanggalFormatSimpan(TanggalBayar) & "', " & _
                                      " '" & JumlahBayar & "', " & _
                                      " '" & COAKredit & "', " & _
                                      " '" & Keterangan & "', " & _
                                      " '" & NomorJV & "', " & _
                                      " '" & User & "' " & _
                                      ") ", KoneksiDatabaseTransaksi)
                Try
                    cmd.ExecuteNonQuery()
                    ProsesSuntingDatabase = True
                Catch ex As Exception
                    ProsesSuntingDatabase = False
                    AksesDatabase_Transaksi(Tutup)
                End Try
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        'Jika Bermaksud Edit Data Sisa Hutang Usaha yang sudah ada :
        If FungsiForm = FungsiForm_EDIT Then
            AksesDatabase_Transaksi(Buka)
            cmd = New OdbcCommand(" UPDATE tbl_SisaHutangUsaha SET " & _
                                  " Referensi             = '" & Referensi & "', " & _
                                  " Tanggal_Invoice       = '" & TanggalFormatSimpan(TanggalInvoice) & "', " & _
                                  " Nomor_Invoice         = '" & NomorInvoice & "', " & _
                                  " Tanggal_Faktur_Pajak  = '" & TanggalFormatSimpan(TanggalFakturPajak) & "', " & _
                                  " Nomor_Faktur_Pajak    = '" & NomorFakturPajak & "', " & _
                                  " Kode_Supplier         = '" & KodeSupplier & "', " & _
                                  " Nama_Supplier         = '" & NamaSupplier & "', " & _
                                  " Nama_Barang           = '" & NamaBarang & "', " & _
                                  " Nama_Jasa             = '" & NamaJasa & "', " & _
                                  " DPP                   = '" & DPP & "', " & _
                                  " PPN                   = '" & PPN & "', " & _
                                  " PPh_Dipotong          = '" & PPhDipotong & "', " & _
                                  " Jumlah_Tagihan        = '" & JumlahTagihan & "', " & _
                                  " Due_Date              = '" & TanggalFormatSimpan(DueDate) & "', " & _
                                  " Keterangan            = '" & Keterangan & "' " & _
                                  " WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", _
                                  KoneksiDatabaseTransaksi)
            Try
                cmd.ExecuteNonQuery()
                ProsesSuntingDatabase = True
            Catch ex As Exception
                ProsesSuntingDatabase = False
            End Try
            AksesDatabase_Transaksi(Tutup)

            'Edit Value di tbl_PembayaranHutangUsaha
            If ProsesSuntingDatabase = True Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" UPDATE tbl_PembayaranHutangUsaha SET      " & _
                                      " Nomor_BPHU    = '" & Referensi & "', " & _
                                      " Kode_Supplier = '" & KodeSupplier & "',   " & _
                                      " Nama_Supplier = '" & NamaSupplier & "',   " & _
                                      " Jumlah_Bayar  = '" & JumlahBayar & "',    " & _
                                      " Keterangan    = '" & Keterangan & "'      " & _
                                      " WHERE Nomor_Pembelian = '" & NomorPembelian & "' AND Tanggal_Bayar < '" & TanggalFormatSimpan(AwalTahunBukuAktif) & "' ", KoneksiDatabaseTransaksi)
                Try
                    cmd.ExecuteNonQuery()
                    ProsesSuntingDatabase = True
                Catch ex As Exception
                    ProsesSuntingDatabase = False
                End Try
                AksesDatabase_Transaksi(Tutup)
            End If

        End If

        If ProsesSuntingDatabase = True Then
            frm_SaldoAwalHutangUsaha.TampilkanData()
            If usc_BukuPengawasanHutangUsaha.StatusAktif Then usc_BukuPengawasanHutangUsaha.TampilkanData()
            If FungsiForm = FungsiForm_TAMBAH Then
                btn_Reset_Click(sender, e)
                MsgBox("Data Sisa Hutang Usaha berhasil disimpan.")
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data Sisa Hutang Usaha berhasil diedit.")
                Me.Close()
            End If
        Else
            If FungsiForm = FungsiForm_TAMBAH Then
                AksesDatabase_Transaksi(Buka)
                cmd = New OdbcCommand(" DELETE FROM tbl_SisaHutangUsaha WHERE Nomor_Pembelian = '" & NomorPembelian & "' ", KoneksiDatabaseTransaksi)
                cmd.ExecuteNonQuery()
                AksesDatabase_Transaksi(Tutup)
                MsgBox("Data Sisa Hutang Usaha GAGAL disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
            If FungsiForm = FungsiForm_EDIT Then
                MsgBox("Data Sisa Hutang Usaha GAGAL diedit." & Enter2Baris & teks_SilakanCobaLagi_Database)
            End If
        End If

    End Sub

    Private Sub btn_Reset_Click(sender As Object, e As EventArgs) Handles btn_Reset.Click
        ResetForm()
    End Sub

    Public Sub ResetForm()
        txt_NomorPembelian.Text = Nothing
        txt_Referensi.Text = Nothing
        txt_NomorInvoice.Text = Nothing
        txt_NomorFakturPajak.Text = Nothing
        txt_KodeSupplier.Text = Nothing
        txt_NamaSupplier.Text = Nothing
        txt_NamaBarang.Text = Nothing
        txt_NamaJasa.Text = Nothing
        txt_DPP.Text = Nothing
        txt_PPN.Text = Nothing
        txt_PPhDipotong.Text = Nothing
        txt_JumlahTagihan.Text = Nothing
        txt_JumlahBayar.Text = Nothing
        txt_SisaHutang.Text = Nothing
        dtp_DueDate.Value = Today
        txt_Keterangan.Text = Nothing
        'Value Tanggal gak perlu ada. Sudah dibikin logikanya oleh sistem, untuk penguncian tahunnya.
    End Sub

    Private Sub dtp_TanggalInvoice_ValueChanged(sender As Object, e As EventArgs) Handles dtp_TanggalInvoice.ValueChanged
        Dim TahunKuncian = TahunBukuAktif
        If JenisTahunBuku = JenisTahunBuku_LAMPAU Then TahunKuncian = TahunBukuAktif
        If JenisTahunBuku = JenisTahunBuku_NORMAL Then TahunKuncian = TahunBukuKemarin
        If dtp_TanggalInvoice.Value.Year > TahunKuncian Then
            dtp_TanggalInvoice.Value = New Date(TahunKuncian, dtp_TanggalInvoice.Value.Month, dtp_TanggalInvoice.Value.Day)
        End If
        dtp_TanggalFakturPajak.Value = dtp_TanggalInvoice.Value
    End Sub

    Private Sub txt_NomorFakturPajak_TextChanged(sender As Object, e As EventArgs) Handles txt_NomorFakturPajak.TextChanged
        If txt_NomorFakturPajak.Text = Nothing Then
            txt_PPN.Enabled = False
            txt_PPN.Text = Nothing
        Else
            txt_PPN.Enabled = True
        End If
    End Sub

    Private Sub txt_KodeSupplier_Click(sender As Object, e As EventArgs) Handles txt_KodeSupplier.Click
        btn_PilihMitra_Click(sender, e)
    End Sub
    Private Sub txt_KodeSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_KodeSupplier.KeyPress
        KunciTotalInputan(sender, e)
    End Sub
    Private Sub txt_KodeSupplier_TextChanged(sender As Object, e As EventArgs) Handles txt_KodeSupplier.TextChanged
    End Sub

    Private Sub btn_PilihMitra_Click(sender As Object, e As EventArgs) Handles btn_PilihMitra.Click
        frm_ListMitra.PilihJenisLawanTransaksi = Mitra_Supplier
        If txt_KodeSupplier.Text = Nothing Then
            frm_ListMitra.ResetForm()
        Else
            frm_ListMitra.KodeMitraTerseleksi = txt_KodeSupplier.Text
            frm_ListMitra.NamaMitraTerseleksi = txt_NamaSupplier.Text
        End If
        frm_ListMitra.ShowDialog()
        txt_KodeSupplier.Text = frm_ListMitra.KodeMitraTerseleksi
        txt_NamaSupplier.Text = frm_ListMitra.NamaMitraTerseleksi
    End Sub

    Private Sub txt_DPP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DPP.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_DPP_TextChanged(sender As Object, e As EventArgs) Handles txt_DPP.TextChanged
        Try
            If txt_DPP.Text.Trim() <> "" Then
                txt_DPP.Text = CDec(txt_DPP.Text).ToString("N0")
                txt_DPP.SelectionStart = txt_DPP.TextLength
            End If
        Catch ex As Exception
        End Try
        PerhitunganJumlahTagihan()
    End Sub

    Private Sub txt_PPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPN.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PPN_TextChanged(sender As Object, e As EventArgs) Handles txt_PPN.TextChanged
        Try
            If txt_PPN.Text.Trim() <> "" Then
                txt_PPN.Text = CDec(txt_PPN.Text).ToString("N0")
                txt_PPN.SelectionStart = txt_PPN.TextLength
            End If
        Catch ex As Exception
        End Try
        PerhitunganJumlahTagihan()
    End Sub

    Private Sub txt_PPhDipotong_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_PPhDipotong.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_PPhDipotong_TextChanged(sender As Object, e As EventArgs) Handles txt_PPhDipotong.TextChanged
        Try
            If txt_PPhDipotong.Text.Trim() <> "" Then
                txt_PPhDipotong.Text = CDec(txt_PPhDipotong.Text).ToString("N0")
                txt_PPhDipotong.SelectionStart = txt_PPhDipotong.TextLength
            End If
        Catch ex As Exception
        End Try
        PerhitunganJumlahTagihan()
    End Sub

    Private Sub txt_JumlahTagihan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahTagihan.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_JumlahTagihan_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahTagihan.TextChanged
        Try
            If txt_JumlahTagihan.Text.Trim() <> "" Then
                txt_JumlahTagihan.Text = CDec(txt_JumlahTagihan.Text).ToString("N0")
                txt_JumlahTagihan.SelectionStart = txt_JumlahTagihan.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txt_JumlahBayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_JumlahBayar.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_JumlahBayar_TextChanged(sender As Object, e As EventArgs) Handles txt_JumlahBayar.TextChanged
        Try
            If txt_JumlahBayar.Text.Trim() <> "" Then
                txt_JumlahBayar.Text = CDec(txt_JumlahBayar.Text).ToString("N0")
                txt_JumlahBayar.SelectionStart = txt_JumlahBayar.TextLength
            End If
        Catch ex As Exception
        End Try
        PerhitunganSisaHutang()
    End Sub

    Private Sub txt_SisaHutang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SisaHutang.KeyPress
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_SisaHutang_TextChanged(sender As Object, e As EventArgs) Handles txt_SisaHutang.TextChanged
        Try
            If txt_SisaHutang.Text.Trim() <> "" Then
                txt_SisaHutang.Text = CDec(txt_SisaHutang.Text).ToString("N0")
                txt_SisaHutang.SelectionStart = txt_SisaHutang.TextLength
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class