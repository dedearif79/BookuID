Imports bcomm
Imports System.Data.Odbc

Public Class X_frm_JurnalUmum_X

    Dim BarisTerseleksi
    Dim QueryTampilan
    Dim FilterData
    Dim PengurutanData
    Dim KolomUrut
    Dim JumlahListJV
    Dim JumlahBaris
    Dim StatusApprove_Terseleksi As String
    Dim NomorJV_Terseleksi
    Dim AngkaNomorJV_Terseleksi

    Private Sub frm_Jurnal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ProsesLoadingForm = True

        Select Case SistemApprovalPerusahaan
            Case True
                btn_Setujui.Visible = True
                DataTabelUtama.Columns("Pilih_").Visible = True
                CheckBoxHeader.Visible = True
            Case False
                btn_Setujui.Visible = False
                DataTabelUtama.Columns("Pilih_").Visible = False
                CheckBoxHeader.Visible = False
        End Select

        If LevelUserAktif = LevelUser_01_Operator Then
            DataTabelUtama.Columns("Pilih_").Visible = False
            CheckBoxHeader.Visible = False
            btn_Setujui.Enabled = False
        End If

        KodingCheckBoxHeader()

        cmb_JenisJurnal.Items.Add("Semua")
        KontenComboJenisJurnal_Public(cmb_JenisJurnal)
        cmb_JenisJurnal.Text = "Semua"

        RefreshTampilanData()

        ProsesLoadingForm = False

    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Sub RefreshTampilanData()
        ProsesResetForm = True
        dtp_DariTanggal.Value = Today
        dtp_SampaiTanggal.Value = Today
        txt_DariNomorJV.Text = Nothing
        txt_SampaiNomorJV.Text = Nothing
        txt_COA.Text = Nothing
        txt_Cari.Text = Nothing
        KontenComboStatusApprove()
        KontenComboDirect()
        KontenComboUrutBerdasarkan()
        KontenComboASC()
        CheckBoxHeader.Checked = False
        ProsesResetForm = False
        TampilkanData()
    End Sub

    Sub TampilkanData()

        JumlahListJV = 0

        'Style Tabel :
        DataTabelUtama.Rows.Clear()
        StyleTabelUtama(DataTabelUtama)

        'Filter Jenis Jurnal :
        Dim PilihanJenisJurnal = cmb_JenisJurnal.Text
        Dim FilterJenisJurnal
        If PilihanJenisJurnal = "Semua" Then
            FilterJenisJurnal = " "
        Else
            FilterJenisJurnal = " AND Jenis_Jurnal = '" & PilihanJenisJurnal & "' "
        End If

        'Filter Periode :
        Dim DariTanggal = TanggalFormatSimpan(dtp_DariTanggal.Value)
        Dim SampaiTanggal = TanggalFormatSimpan(dtp_SampaiTanggal.Value)
        Dim FilterPeriode = " AND Tanggal_Transaksi BETWEEN '" & DariTanggal & "' AND '" & SampaiTanggal & "' "

        'Filter Nomor JV :
        Dim FilterNomorJV = " "
        If txt_DariNomorJV.Text <> Nothing And txt_SampaiNomorJV.Text = Nothing Then
            FilterNomorJV = " AND Nomor_JV = '" & txt_DariNomorJV.Text & "' "
            FilterPeriode = " " '(Filter periode tidak berlaku saat user menggunakan filter Nomor JV)
        End If
        If txt_DariNomorJV.Text = Nothing And txt_SampaiNomorJV.Text <> Nothing Then
            FilterNomorJV = " AND Nomor_JV = '" & txt_SampaiNomorJV.Text & "' "
            FilterPeriode = " " '(Filter periode tidak berlaku saat user menggunakan filter Nomor JV)
        End If
        If txt_DariNomorJV.Text <> Nothing And txt_SampaiNomorJV.Text <> Nothing Then
            FilterNomorJV = " AND NOMOR_JV BETWEEN '" & txt_DariNomorJV.Text & "' AND '" & txt_SampaiNomorJV.Text & "' "
            FilterPeriode = " " '(Filter periode tidak berlaku saat user menggunakan filter Nomor JV)
        End If

        'Filter Status Approve :
        Dim FilterStatusApprove = " "
        If cmb_StatusApprove.Text = "ALL" Then FilterStatusApprove = " "
        If cmb_StatusApprove.Text = "CLOSED" Then FilterStatusApprove = " AND Status_Approve = 1 "
        If cmb_StatusApprove.Text = "OPEN" Then FilterStatusApprove = " AND Status_Approve = 0 "

        'Filter Direct :
        Dim FilterDirect = " "
        Select Case cmb_Direct.Text
            Case "SEMUA"
                FilterDirect = " "
            Case "YA"
                FilterDirect = " AND Direct = 1 "
            Case "TIDAK"
                FilterDirect = " AND Direct = 0 "
        End Select


        'Filter COA :
        Dim FilterCOA = " "
        If txt_COA.Text <> Nothing Then
            FilterCOA = " AND COA = '" & txt_COA.Text & "' "
        End If

        'Filter Pencarian :
        Dim FilterPencarian = " "
        If txt_Cari.Text <> Nothing Then
            FilterPencarian = " AND ( " &
                " COA                       LIKE '%" & txt_Cari.Text & "%' OR " &
                " Nama_Akun                 LIKE '%" & txt_Cari.Text & "%' OR " &
                " Jenis_Jurnal              LIKE '%" & txt_Cari.Text & "%' OR " &
                " Kode_Dokumen              LIKE '%" & txt_Cari.Text & "%' OR " &
                " Nomor_PO                  LIKE '%" & txt_Cari.Text & "%' OR " &
                " Kode_Project              LIKE '%" & txt_Cari.Text & "%' OR " &
                " Nama_Akun                 LIKE '%" & txt_Cari.Text & "%' OR " &
                " Referensi                 LIKE '%" & txt_Cari.Text & "%' OR " &
                " Bundelan                  LIKE '%" & txt_Cari.Text & "%' OR " &
                " Nomor_Invoice             LIKE '%" & txt_Cari.Text & "%' OR " &
                " Nomor_Faktur_Pajak        LIKE '%" & txt_Cari.Text & "%' OR " &
                " Kode_Lawan_Transaksi      LIKE '%" & txt_Cari.Text & "%' OR " &
                " Nama_Lawan_Transaksi      LIKE '%" & txt_Cari.Text & "%' OR " &
                " Uraian_Transaksi          LIKE '%" & txt_Cari.Text & "%' ) "
        End If

        'Pengurutan Data :
        If cmb_UrutBerdasarkan.Text = "NOMOR VOUCHER" Then KolomUrut = " ORDER BY Nomor_JV "
        If cmb_UrutBerdasarkan.Text = "TANGGAL JURNAL" Then KolomUrut = " ORDER BY Tanggal_Transaksi "

        'Query Tampilan :
        FilterData = FilterJenisJurnal & FilterPeriode & FilterNomorJV & FilterStatusApprove & FilterCOA & FilterPencarian & FilterDirect
        PengurutanData = KolomUrut & cmb_ASC.Text
        QueryTampilan = " SELECT * FROM tbl_Transaksi WHERE Nomor_ID > 0 " & FilterData & PengurutanData

        'Data Tabel :
        Dim NomorUrut = 0
        Dim AngkaNomorJV = 0
        Dim NomorJV
        Dim NomorJVSebelumnya = ""
        Dim TanggalJurnal
        Dim JenisJurnal
        Dim KodeDokumen
        Dim NomorPO
        Dim KodeProject
        Dim NamaProduk
        Dim Referensi
        'Dim SumberTransaksi
        Dim TanggalInvoice
        Dim NomorInvoice
        Dim NomorFakturPajak
        Dim LawanTransaksi
        Dim COA
        Dim KodeAkun
        Dim NamaAkun
        Dim DK
        Dim JumlahDebet As Int64
        Dim JumlahKredit As Int64
        Dim UraianTransaksi
        Dim Direct
        Dim StatusApprove As Int64
        Dim clm_Pilih As CheckBox = Nothing
        Dim KoneksiCOA As Boolean = True

        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(QueryTampilan, KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader
        Do While dr.Read
            NomorUrut = NomorUrut + 1
            AngkaNomorJV = dr.Item("Nomor_JV")
            NomorJV = AwalanNomorJV & AngkaNomorJV
            If NomorJVSebelumnya <> NomorJV Then
                DataTabelUtama.Rows.Add()
                JumlahListJV = JumlahListJV + 1
            End If
            TanggalJurnal = Microsoft.VisualBasic.Left(dr.Item("Tanggal_Transaksi"), 10)
            JenisJurnal = dr.Item("Jenis_Jurnal")
            If JenisJurnal = Nothing Then JenisJurnal = StripKosong
            KodeDokumen = dr.Item("Kode_Dokumen")
            If KodeDokumen = Nothing Then KodeDokumen = StripKosong
            NomorPO = dr.Item("Nomor_PO")
            If NomorPO = Nothing Then NomorPO = StripKosong
            KodeProject = dr.Item("Kode_Project")
            If KodeProject = Nothing Then KodeProject = StripKosong
            NamaProduk = dr.Item("Nama_Produk")
            If NamaProduk = Kosongan Then NamaProduk = StripKosong
            Referensi = dr.Item("Referensi")
            If Referensi = Nothing Then Referensi = StripKosong
            TanggalInvoice = dr.Item("Tanggal_Invoice")
            If TanggalInvoice = Nothing Then TanggalInvoice = StripKosong
            NomorInvoice = dr.Item("Nomor_Invoice")
            If NomorInvoice = Nothing Then NomorInvoice = StripKosong
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            If NomorFakturPajak = Nothing Then NomorFakturPajak = StripKosong
            LawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            If LawanTransaksi = Nothing Then LawanTransaksi = StripKosong
            COA = dr.Item("COA")
            KodeAkun = COA
            NamaAkun = dr.Item("Nama_Akun")
            DK = dr.Item("D_K")
            Dim clm_JumlahDebet, clm_JumlahKredit
            JumlahDebet = dr.Item("Jumlah_Debet")
            If JumlahDebet = 0 Then
                clm_JumlahDebet = StripKosong
                NamaAkun = PenjorokNamaAkun & NamaAkun 'Dibikin menjorok ke kanan
            Else
                clm_JumlahDebet = JumlahDebet
            End If
            JumlahKredit = dr.Item("Jumlah_Kredit")
            If JumlahKredit = 0 Then
                clm_JumlahKredit = StripKosong
            Else
                clm_JumlahKredit = JumlahKredit
            End If
            UraianTransaksi = dr.Item("Uraian_Transaksi")
            If UraianTransaksi = Nothing Then UraianTransaksi = StripKosong
            If dr.Item("Direct") = 1 Then
                Direct = "Ya"
            Else
                Direct = Nothing
            End If
            StatusApprove = dr.Item("Status_Approve")
            DataTabelUtama.Rows.Add(clm_Pilih, AngkaNomorJV, TanggalJurnal,
                                    JenisJurnal, KodeDokumen, NomorPO, KodeProject, NamaProduk,
                                    Referensi, TanggalInvoice, NomorInvoice, NomorFakturPajak, LawanTransaksi, NamaAkun,
                                    KodeAkun, DK, clm_JumlahDebet, clm_JumlahKredit,
                                    UraianTransaksi, Direct, StatusApprove)
            NomorJVSebelumnya = NomorJV
        Loop
        AksesDatabase_Transaksi(Tutup)

        DataTabelUtama.Rows.Add()

        JumlahBaris = DataTabelUtama.RowCount
        lbl_JumlahListJV.Text = "Jumlah List JV : " & JumlahListJV

        BersihkanSeleksi()

        Dim NomorJV_Telusur = Nothing
        Dim NomorJV_Sebelumnya = Nothing
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            NomorJV_Telusur = row.Cells("Nomor_JV").Value
            row.ReadOnly = True
            If row.Cells("Status_Approve").Value = 0 Then
                row.DefaultCellStyle.ForeColor = Color.DarkSlateGray
                If NomorJV_Telusur <> Nothing And NomorJV_Telusur <> NomorJV_Sebelumnya Then
                    row.ReadOnly = False
                End If
            End If
            If row.Cells("Nama_Akun").Value = teks_CoaBelumTerdaftar Or row.Cells("Nama_Akun").Value = PenjorokNamaAkun & teks_CoaBelumTerdaftar Then
                row.DefaultCellStyle.ForeColor = Color.Red
            End If
            NomorJV_Sebelumnya = NomorJV_Telusur
        Next

    End Sub

    'Koding CheckList All
    Private CheckBoxHeader As CheckBox = New CheckBox()
    Sub KodingCheckBoxHeader()
        Dim HeaderCellLocation As Point = Me.DataTabelUtama.GetCellDisplayRectangle(0, -1, True).Location
        CheckBoxHeader.Location = New Point(HeaderCellLocation.X + 9, HeaderCellLocation.Y + 10)
        CheckBoxHeader.Size = New Size(13, 13)
        CheckBoxHeader.BackColor = Color.White
        DataTabelUtama.Controls.Add(CheckBoxHeader)
        AddHandler CheckBoxHeader.Click, AddressOf HeaderCheckBox_Click
    End Sub
    Private Sub HeaderCheckBox_Click(ByVal sender As Object, ByVal e As EventArgs)
        DataTabelUtama.EndEdit()
        Dim NomorJV_Telusur = Nothing
        Dim NomorJV_Sebelumnya = Nothing
        For Each row As DataGridViewRow In DataTabelUtama.Rows
            NomorJV_Telusur = row.Cells("Nomor_JV").Value
            Dim Ceklis As DataGridViewCheckBoxCell = TryCast(row.Cells("Pilih_"), DataGridViewCheckBoxCell)
            If row.Cells("Status_Approve").Value = 0 And NomorJV_Telusur <> Nothing And NomorJV_Telusur <> NomorJV_Sebelumnya Then
                Ceklis.Value = CheckBoxHeader.Checked
            End If
            NomorJV_Sebelumnya = NomorJV_Telusur
        Next
    End Sub

    Sub BersihkanSeleksi()
        DataTabelUtama.ClearSelection()
        BarisTerseleksi = -1
        btn_JurnalVoucher.Enabled = False
        btn_Edit.Enabled = False
        btn_Hapus.Enabled = False
    End Sub

    Sub KontenComboStatusApprove()
        cmb_StatusApprove.Items.Clear()
        cmb_StatusApprove.Items.Add("ALL")
        cmb_StatusApprove.Items.Add("CLOSED")
        cmb_StatusApprove.Items.Add("OPEN")
        cmb_StatusApprove.Text = "ALL"
    End Sub

    Sub KontenComboDirect()
        cmb_Direct.Items.Clear()
        cmb_Direct.Items.Add("SEMUA")
        cmb_Direct.Items.Add("YA")
        cmb_Direct.Items.Add("TIDAK")
        cmb_Direct.Text = "SEMUA"
    End Sub

    Sub KontenComboUrutBerdasarkan()
        cmb_UrutBerdasarkan.Items.Clear()
        cmb_UrutBerdasarkan.Items.Add("NOMOR VOUCHER")
        cmb_UrutBerdasarkan.Items.Add("TANGGAL JURNAL")
        cmb_UrutBerdasarkan.Text = "NOMOR VOUCHER"
    End Sub

    Sub KontenComboASC()
        cmb_ASC.Items.Clear()
        cmb_ASC.Items.Add("ASC")
        cmb_ASC.Items.Add("DESC")
        cmb_ASC.Text = "ASC"
    End Sub

    Private Sub cmb_StatusApprove_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_StatusApprove.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_StatusApprove_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_StatusApprove.SelectedIndexChanged
        TampilkanData()
    End Sub

    Private Sub btn_JurnalVoucher_Click(sender As Object, e As EventArgs) Handles btn_JurnalVoucher.Click
        win_JurnalVoucher = New wpfWin_JurnalVoucher
        If BarisTerseleksi < 0 Then Return
        win_JurnalVoucher.ResetForm()
        If AngkaNomorJV_Terseleksi < 1 Then Return
        If StatusApprove_Terseleksi = "0" Then
            win_JurnalVoucher.FungsiForm = FungsiForm_JURNALAPPROVE
            win_JurnalVoucher.Angka_NomorJV = AngkaNomorJV_Terseleksi
            win_JurnalVoucher.ShowDialog()
        Else
            LihatJurnal(AngkaNomorJV_Terseleksi)
        End If
    End Sub

    Private Sub btn_Setujui_Click(sender As Object, e As EventArgs) Handles btn_Setujui.Click

        Dim ProsesApprove As Boolean = False

        Dim Baris
        Dim BarisTelusur
        Dim NomorJV_Telusur = Nothing
        Dim NomorJV_Sebelumnya = Nothing
        Dim JumlahTerceklis

        Baris = 0
        BarisTelusur = 0
        JumlahTerceklis = 0
        Do While BarisTelusur < JumlahListJV
            Dim Ceklis As Boolean = DataTabelUtama.Item("Pilih_", Baris).Value
            NomorJV_Telusur = DataTabelUtama.Item("Nomor_JV", Baris).Value
            If NomorJV_Telusur <> Nothing And NomorJV_Telusur <> NomorJV_Sebelumnya Then
                If Ceklis = True Then
                    JumlahTerceklis = JumlahTerceklis + 1
                End If
                BarisTelusur = BarisTelusur + 1
            End If
            Baris = Baris + 1
            NomorJV_Sebelumnya = NomorJV_Telusur
        Loop

        If JumlahTerceklis = 0 Then
            MsgBox("Tidak ada jurnal yang terceklis.")
            Return
        End If

        Dim PilihSetujuiSemua = MessageBox.Show("Ada " & JumlahTerceklis & " jurnal yang terceklis." & Enter2Baris & "Yakin akan menyetujuinya..?", "Perhatian..!", MessageBoxButtons.YesNo)
        If PilihSetujuiSemua = vbNo Then
            Return
        End If

        TrialBalance_Mentahkan()

        Baris = 0
        JumlahTerceklis = 0
        Do While Baris < JumlahBaris
            Dim Ceklis As Boolean = DataTabelUtama.Item("Pilih_", Baris).Value
            Dim AngkaNomorJV_Telusur = Microsoft.VisualBasic.Mid(DataTabelUtama.Item("Nomor_JV", Baris).Value, PanjangTeks_AwalanNomorJV_Plus1)
            If Ceklis = True Then
                AksesDatabase_Transaksi(Buka)
                If StatusKoneksiDatabaseTransaksi = False Then
                    ProsesApprove = False
                    Exit Do
                End If
                cmd = New OdbcCommand(" UPDATE tbl_Transaksi SET " &
                                      " Status_Approve = 1, Username_Approve = '" & UserAktif & "', " &
                                      " Nama_User_Approve = '" & NamaUserAktif & "' " &
                                      " WHERE Nomor_JV = '" & AngkaNomorJV_Telusur & "' ",
                                      KoneksiDatabaseTransaksi)
                Try
                    cmd.ExecuteNonQuery()
                    ProsesApprove = True
                Catch ex As Exception
                    ProsesApprove = False
                End Try
                'Update Status Jurnal di Tabel Pembelian, menjadi bernilai '1', agar bisa tampil di Buku Pengawasan Hutang Usaha.
                cmd = New OdbcCommand(" UPDATE tbl_Pembelian SET Status_Jurnal = 1 WHERE Nomor_JV = '" & AngkaNomorJV_Telusur & "' ", KoneksiDatabaseTransaksi)
                Try
                    cmd.ExecuteNonQuery()
                    ProsesApprove = True
                Catch ex As Exception
                    ProsesApprove = False
                End Try
                AksesDatabase_Transaksi(Tutup)
            End If
            Baris = Baris + 1
        Loop

        TampilkanData()
        CheckBoxHeader.Checked = False

        If ProsesApprove = True Then
            MsgBox("Proses Approve massal SUKSES.")
        Else
            MsgBox("Proses Approve massal GAGAL, atau hanya baru sebagian yang sukses..!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub

    Private Sub DataTabelUtama_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellContentClick
    End Sub
    Private Sub DataTabelUtama_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataTabelUtama.ColumnHeaderMouseClick
        BersihkanSeleksi()
    End Sub
    Private Sub DataTabelUtama_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellClick

        If DataTabelUtama.RowCount = 0 Then Return
        BarisTerseleksi = DataTabelUtama.CurrentRow.Index
        StatusApprove_Terseleksi = DataTabelUtama.Item("Status_Approve", BarisTerseleksi).Value
        AngkaNomorJV_Terseleksi = AmbilAngka(DataTabelUtama.Item("Nomor_JV", BarisTerseleksi).Value)
        NomorJV_Terseleksi = AwalanNomorJV & AngkaNomorJV_Terseleksi

        If DataTabelUtama.Item("Direct_", BarisTerseleksi).Value = "Ya" Then
            btn_Edit.Enabled = True
            btn_Hapus.Enabled = True
        Else
            btn_Edit.Enabled = False
            btn_Hapus.Enabled = False
        End If

        If AngkaNomorJV_Terseleksi > 0 Then
            btn_JurnalVoucher.Enabled = True
        Else
            btn_JurnalVoucher.Enabled = False
        End If

    End Sub

    Private Sub DataTabelUtama_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataTabelUtama.CellDoubleClick
        btn_JurnalVoucher_Click(sender, e)
    End Sub

    Private Sub cmb_JenisJurnal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_JenisJurnal.SelectedIndexChanged
    End Sub
    Private Sub cmb_JenisJurnal_TextChanged(sender As Object, e As EventArgs) Handles cmb_JenisJurnal.TextChanged
        'TampilkanData()
    End Sub
    Private Sub cmb_JenisJurnal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_JenisJurnal.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub dtp_DariTanggal_ValueChanged(sender As Object, e As EventArgs) Handles dtp_DariTanggal.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_DariTanggal)
        txt_DariNomorJV.Text = Nothing 'Filter berdasarkan NomorJV tidak berlaku saat adanya filter berdasarkan periode
        txt_SampaiNomorJV.Text = Nothing 'Filter berdasarkan NomorJV tidak berlaku saat adanya filter berdasarkan periode
        'TampilkanData()
    End Sub
    Private Sub dtp_DariTanggal_Leave(sender As Object, e As EventArgs) Handles dtp_DariTanggal.Leave
        Dim TahunFilter = Format(dtp_DariTanggal.Value, "yyyy")
        Dim BulanFilter = Format(dtp_DariTanggal.Value, "MM")
        Dim HariFilter = Format(dtp_DariTanggal.Value, "dd")
        If TahunFilter <> TahunBukuAktif Then
            MsgBox("Halaman ini hanya bisa menampilkan Jurnal sesuai dengan Tahun Buku Aktif, dalam hal ini Tahun " & TahunBukuAktif & "." _
                   & Enter2Baris & "Jika ingin melihat Jurnal Tahun " & TahunFilter & ", silakan masuk ke Tahun Buku tersebut.")
            dtp_DariTanggal.Text = HariFilter & "-" & BulanFilter & "-" & TahunBukuAktif
        End If
    End Sub

    Private Sub dtp_SampaiTanggal_ValueChanged(sender As Object, e As EventArgs) Handles dtp_SampaiTanggal.ValueChanged
        KunciTahun_HarusSamaDenganTahunBukuAktif(dtp_SampaiTanggal)
        txt_DariNomorJV.Text = Nothing 'Filter berdasarkan NomorJV tidak berlaku saat adanya filter berdasarkan periode
        txt_SampaiNomorJV.Text = Nothing 'Filter berdasarkan NomorJV tidak berlaku saat adanya filter berdasarkan periode
        'TampilkanData()
    End Sub
    Private Sub dtp_SampaiTanggal_Leave(sender As Object, e As EventArgs) Handles dtp_SampaiTanggal.Leave
        Dim TahunFilter = Format(dtp_SampaiTanggal.Value, "yyyy")
        Dim BulanFilter = Format(dtp_SampaiTanggal.Value, "MM")
        Dim HariFilter = Format(dtp_SampaiTanggal.Value, "dd")
        If TahunFilter <> TahunBukuAktif Then
            MsgBox("Halaman ini hanya bisa menampilkan Jurnal sesuai dengan Tahun Buku Aktif, dalam hal ini Tahun " & TahunBukuAktif & "." _
                   & Enter2Baris & "Jika ingin melihat Jurnal Tahun " & TahunFilter & ", silakan masuk ke Tahun Buku tersebut.")
            dtp_SampaiTanggal.Text = HariFilter & "-" & BulanFilter & "-" & TahunBukuAktif
        End If
    End Sub

    Private Sub cmb_UrutBerdasarkan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_UrutBerdasarkan.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_UrutBerdasarkan_TextChanged(sender As Object, e As EventArgs) Handles cmb_UrutBerdasarkan.TextChanged
        cmb_UrutBerdasarkan_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub cmb_UrutBerdasarkan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_UrutBerdasarkan.SelectedIndexChanged
        TampilkanData()
    End Sub

    Private Sub cmb_ASC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmb_ASC.KeyPress
        KunciTotalInputan(sender, e)
    End Sub

    Private Sub cmb_ASC_TextChanged(sender As Object, e As EventArgs) Handles cmb_ASC.TextChanged
        cmb_ASC_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub cmb_ASC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ASC.SelectedIndexChanged
        TampilkanData()
    End Sub

    Private Sub txt_DariNomorJV_TextChanged(sender As Object, e As EventArgs) Handles txt_DariNomorJV.TextChanged
    End Sub
    Private Sub txt_DariNomorJV_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_DariNomorJV.KeyPress
        If e.KeyChar = Chr(13) Then TampilkanData()
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_DariNomorJV_Leave(sender As Object, e As EventArgs) Handles txt_DariNomorJV.Leave
        TampilkanData()
    End Sub

    Private Sub txt_SampaiNomorJV_TextChanged(sender As Object, e As EventArgs) Handles txt_SampaiNomorJV.TextChanged
    End Sub
    Private Sub txt_SampaiNomorJV_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_SampaiNomorJV.KeyPress
        If e.KeyChar = Chr(13) Then TampilkanData()
        HanyaBolehInputAngkaPlus(sender, e)
    End Sub
    Private Sub txt_SampaiNomorJV_Leave(sender As Object, e As EventArgs) Handles txt_SampaiNomorJV.Leave
        TampilkanData()
    End Sub

    Private Sub txt_COA_TextChanged(sender As Object, e As EventArgs) Handles txt_COA.TextChanged
    End Sub

    Private Sub txt_COA_Enter(sender As Object, e As EventArgs) Handles txt_COA.Enter
        txt_COA_Click(sender, e)
    End Sub

    Private Sub txt_COA_Click(sender As Object, e As EventArgs) Handles txt_COA.Click
        frm_ListCOA.ListAkun = ListAkun_Semua
        If txt_COA.Text = Nothing Then
            frm_ListCOA.ResetForm()
        Else
            frm_ListCOA.COATerseleksi = txt_COA.Text
        End If
        frm_ListCOA.ShowDialog()
        txt_COA.Text = frm_ListCOA.COATerseleksi
    End Sub

    Private Sub txt_COA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_COA.KeyPress
        'KunciTotalInputan(sender, e)
    End Sub

    Private Sub btn_Import_Click(sender As Object, e As EventArgs) Handles btn_Import.Click

        Pilihan = MessageBox.Show("Fitur ini hanya digunakan untuk mengimport Data Jurnal yang berdiri sendiri dan tidak berkaitan dengan Data Finance yang ada disini." & Enter2Baris &
                                  "Lanjutkan import..?",
                                  "PERHATIAN..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        StatusImportJurnal = True

        frm_ProgressImportJurnal.ShowDialog()
        If StatusPosting = "BATAL" Then
            MsgBox("Proses posting telah dibatalkan seluruhnya pada event ini.")
        End If

        StatusImportJurnal = False

    End Sub

    Private Sub btn_Export_Click(sender As Object, e As EventArgs) Handles btn_Export.Click
        EksporDataGridViewKeEXCEL(DataTabelUtama)
    End Sub

    Private Sub btn_Filter_Click(sender As Object, e As EventArgs) Handles btn_Filter.Click
        TampilkanData()
    End Sub

    Private Sub btn_Tambah_Click(sender As Object, e As EventArgs) Handles btn_Tambah.Click
        'frm_InputJurnal.ResetForm()
        'frm_InputJurnal.JalurMasuk = Halaman_JURNALUMUM
        'frm_InputJurnal.FungsiForm = FungsiForm_TAMBAH
        'frm_InputJurnal.ShowDialog()
        win_InputJurnal = New wpfWin_InputJurnal
        win_InputJurnal.ResetForm()
        win_InputJurnal.JalurMasuk = Halaman_JURNALUMUM
        win_InputJurnal.FungsiForm = FungsiForm_TAMBAH
        win_InputJurnal.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As EventArgs) Handles btn_Edit.Click
        EditJurnal(AngkaNomorJV_Terseleksi)
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As EventArgs) Handles btn_Hapus.Click

        'Yakin akan menghapus..????
        Pilihan = MessageBox.Show("Yakin akan menghapus data nomor " & NomorJV_Terseleksi & " ?", "Perhatian..!", MessageBoxButtons.YesNo)
        If Pilihan = vbNo Then Return

        AksesDatabase_Transaksi(Buka)
        If StatusKoneksiDatabaseTransaksi = False Then
            MsgBox("Data terpilih GAGAL dihapus." & Enter2Baris & teks_SilakanCobaLagi_Database)
            Return
        End If

        'Hapus DataTerpilih Pada Tabel Jurnal (tbl_Transaksi) :
        cmd = New OdbcCommand("DELETE FROM tbl_Transaksi WHERE Nomor_JV = '" & AngkaNomorJV_Terseleksi & "' ", KoneksiDatabaseTransaksi)
        cmd.ExecuteNonQuery()

        AksesDatabase_Transaksi(Tutup)
        TampilkanData()
        MsgBox("Data terpilih BERHASIL dihapus.")

    End Sub

    Private Sub grb_Filter_Enter(sender As Object, e As EventArgs) Handles grb_Filter.Enter

    End Sub

    Private Sub frm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' Sub ini nanti hapus saja. Sudah tidak diperlukan...!!!
    End Sub

End Class