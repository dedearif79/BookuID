Imports System.Data.Odbc
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports bcomm

Public Class wpfUsc_JurnalUmum

    Public StatusAktif As Boolean

    Dim QueryTampilan
    Dim FilterData
    Dim PengurutanData
    Dim KolomUrut
    Dim JumlahListJV
    Dim StatusApprove_Terseleksi As String
    Dim Direct_Terseleksi As String
    Dim NomorJV_Terseleksi
    Dim AngkaNomorJV_Terseleksi

    Dim PilihanJenisJurnal
    Dim DariTanggal_Date As Date
    Dim SampaiTanggal_Date As Date
    Dim DariNomorJV
    Dim SampaiNomorJV
    Dim PilihanStatusApprove
    Dim PilihanCOA
    Dim Cari
    Dim PilihanDirect

    Dim PilihanUrutan
    Dim PilihanASC

    Dim AdaPembuanganSampahJurnal As Boolean

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        lbl_JudulForm.Text = frm_JurnalUmum.JudulForm
        Terabas()
        StatusAktif = True

        ProsesLoadingForm = True

        Select Case SistemApprovalPerusahaan
            Case True
                VisibilitasTombolSetujui(True)
                Ceklis_Approve.Visibility = Visibility.Visible
            Case False
                VisibilitasTombolSetujui(False)
                Ceklis_Approve.Visibility = Visibility.Collapsed
        End Select

        If LevelUserAktif = LevelUser_01_Operator Then
            Ceklis_Approve.Visibility = Visibility.Collapsed
        End If

        RefreshTampilanData()

        ProsesLoadingForm = False


    End Sub


    Sub RefreshTampilanData()
        ProsesResetForm = True
        EksekusiKode = False
        dtp_DariTanggal.SelectedDate = Today
        dtp_SampaiTanggal.SelectedDate = Today
        txt_DariNomorJV.Text = Kosongan
        txt_SampaiNomorJV.Text = Kosongan
        txt_COA.Text = Kosongan
        txt_Cari.Text = Kosongan
        KontenComboJenisJurnal()
        KontenComboDirect()
        'KontenComboStatusApprove()
        KontenComboUrutBerdasarkan()
        KontenComboASC()
        'CheckBoxHeader.Checked = False
        ProsesResetForm = False
        EksekusiKode = True
        TampilkanData()
    End Sub


    Sub TampilkanData()

        If ProsesResetForm = True Then Return
        If EksekusiKode = False Then Return

        AdaPembuanganSampahJurnal = False

        'PesanUntukProgrammer("Eksekusi Sub : TampilkanData()")

        KetersediaanMenuHalaman(pnl_Halaman, False)

        JumlahListJV = 0

        'Style Tabel :
        datatabelUtama.Rows.Clear()

        'Filter Jenis Jurnal :
        Dim FilterJenisJurnal
        If PilihanJenisJurnal = "Semua" Then
            FilterJenisJurnal = Spasi1
        Else
            FilterJenisJurnal = " AND Jenis_Jurnal = '" & PilihanJenisJurnal & "' "
        End If

        'Filter Periode :
        Dim FilterPeriode As String = Spasi1
        Dim DariTanggal_String As String = Kosongan
        Dim SampaiTanggal_String As String = Kosongan
        If dtp_DariTanggal.Text <> Kosongan And dtp_SampaiTanggal.Text <> Kosongan Then
            If dtp_DariTanggal.Text <> Kosongan Then DariTanggal_String = TanggalFormatSimpan(dtp_DariTanggal.SelectedDate)
            If dtp_SampaiTanggal.Text <> Kosongan Then SampaiTanggal_String = TanggalFormatSimpan(dtp_SampaiTanggal.SelectedDate)
            FilterPeriode = " AND Tanggal_Transaksi BETWEEN '" & DariTanggal_String & "' AND '" & SampaiTanggal_String & "' "
        End If

        'Filter Nomor JV :
        Dim FilterNomorJV = Spasi1
        If txt_DariNomorJV.Text <> Kosongan And txt_SampaiNomorJV.Text = Kosongan Then
            FilterNomorJV = " AND Nomor_JV = '" & DariNomorJV & "' "
            FilterPeriode = Spasi1 '(Filter periode tidak berlaku saat user menggunakan filter Nomor JV)
        End If
        If txt_DariNomorJV.Text = Kosongan And txt_SampaiNomorJV.Text <> Kosongan Then
            FilterNomorJV = " AND Nomor_JV = '" & SampaiNomorJV & "' "
            FilterPeriode = Spasi1 '(Filter periode tidak berlaku saat user menggunakan filter Nomor JV)
        End If
        If txt_DariNomorJV.Text <> Kosongan And txt_SampaiNomorJV.Text <> Kosongan Then
            FilterNomorJV = " AND NOMOR_JV BETWEEN '" & DariNomorJV & "' AND '" & SampaiNomorJV & "' "
            FilterPeriode = Spasi1 '(Filter periode tidak berlaku saat user menggunakan filter Nomor JV)
        End If

        'Filter Status Approve :
        Dim FilterStatusApprove = Spasi1
        If cmb_StatusApprove.SelectedValue = Pilihan_ALL_ Then FilterStatusApprove = Spasi1
        If cmb_StatusApprove.SelectedValue = Pilihan_CLOSED_ Then FilterStatusApprove = " AND Status_Approve = 1 "
        If cmb_StatusApprove.SelectedValue = Pilihan_OPEN_ Then FilterStatusApprove = " AND Status_Approve = 0 "

        'Filter Direct :
        Dim FilterDirect = Spasi1
        Select Case PilihanDirect
            Case Pilihan_Semua
                FilterDirect = Spasi1
            Case Pilihan_Ya
                FilterDirect = " AND Direct = 1 "
            Case Pilihan_Tidak
                FilterDirect = " AND Direct = 0 "
        End Select


        'Filter COA :
        Dim FilterCOA = Spasi1
        If PilihanCOA <> Kosongan Then
            FilterCOA = " AND COA = '" & PilihanCOA & "' "
        End If

        'Filter Pencarian :
        Dim FilterPencarian = Spasi1
        If Cari <> Kosongan Then
            FilterPencarian = " AND ( " &
                " COA                       LIKE '%" & Cari & "%' OR " &
                " Nama_Akun                 LIKE '%" & Cari & "%' OR " &
                " Jenis_Jurnal              LIKE '%" & Cari & "%' OR " &
                " Kode_Dokumen              LIKE '%" & Cari & "%' OR " &
                " Nomor_PO                  LIKE '%" & Cari & "%' OR " &
                " Kode_Project              LIKE '%" & Cari & "%' OR " &
                " Nama_Akun                 LIKE '%" & Cari & "%' OR " &
                " Referensi                 LIKE '%" & Cari & "%' OR " &
                " Bundelan                  LIKE '%" & Cari & "%' OR " &
                " Nomor_Invoice             LIKE '%" & Cari & "%' OR " &
                " Nomor_Faktur_Pajak        LIKE '%" & Cari & "%' OR " &
                " Kode_Lawan_Transaksi      LIKE '%" & Cari & "%' OR " &
                " Nama_Lawan_Transaksi      LIKE '%" & Cari & "%' OR " &
                " Uraian_Transaksi          LIKE '%" & Cari & "%' ) "
        End If

        'Pengurutan Data :
        If PilihanUrutan = "Nomor Voucher" Then KolomUrut = " ORDER BY Nomor_ID, Nomor_JV, Tanggal_Transaksi "
        If PilihanUrutan = "Tanggal Jurnal" Then KolomUrut = " ORDER BY Nomor_ID, Tanggal_Transaksi, Nomor_JV "

        'Query Tampilan :
        FilterData = FilterJenisJurnal & FilterPeriode & FilterNomorJV & FilterStatusApprove & FilterCOA & FilterPencarian & FilterDirect
        PengurutanData = KolomUrut & PilihanASC
        QueryTampilan = " SELECT * FROM tbl_Transaksi WHERE Valid <> '" & _X_ & "' " & FilterData & PengurutanData

        'Data Tabel :
        Dim NomorUrut = 0
        Dim AngkaNomorJV = 0
        Dim AngkaNomorJV_Sebelumnya = 0
        Dim NomorJV
        Dim NomorJVSebelumnya = Kosongan
        Dim TanggalJurnal
        Dim JenisJurnal
        Dim KodeDokumen
        Dim NomorPO
        Dim KodeProject
        Dim NamaProduk
        Dim Referensi
        Dim TanggalInvoice
        Dim NomorInvoice
        Dim NomorFakturPajak
        Dim LawanTransaksi
        Dim COA
        Dim KodeAkun
        Dim NamaAkun
        Dim KodeMataUang As String
        Dim Kurs As Decimal
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
        Dim TotalDebetPerJurnal As Int64 = 0
        Dim TotalKreditPerJurnal As Int64 = 0
        Do While dr.Read
            NomorUrut += 1
            AngkaNomorJV = dr.Item("Nomor_JV")
            NomorJV = AwalanNomorJV & AngkaNomorJV
            If NomorJVSebelumnya <> NomorJV Then
                If Not TotalDebetPerJurnal = TotalKreditPerJurnal Then
                    ''If LevelUserAktif = LevelUser_99_AppDeveloper Then
                    'datatabelUtama.Rows.Add(Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan, Kosongan,
                    '                        TotalDebetPerJurnal, TotalKreditPerJurnal,
                    '                        "tidak balance", Kosongan, Kosongan)
                    ''End If
                    'BuangJurnal_BerdasarkanNomorJV(AngkaNomorJV_Sebelumnya)
                    'AdaPembuanganSampahJurnal = True
                End If
                datatabelUtama.Rows.Add()
                TotalDebetPerJurnal = 0
                TotalKreditPerJurnal = 0
                JumlahListJV += 1
            End If
            TanggalJurnal = TanggalFormatTampilan(dr.Item("Tanggal_Transaksi"))
            JenisJurnal = dr.Item("Jenis_Jurnal")
            KodeDokumen = dr.Item("Kode_Dokumen")
            NomorPO = dr.Item("Nomor_PO")
            KodeProject = dr.Item("Kode_Project")
            NamaProduk = dr.Item("Nama_Produk")
            Referensi = dr.Item("Referensi")
            TanggalInvoice = dr.Item("Tanggal_Invoice")
            NomorInvoice = dr.Item("Nomor_Invoice")
            NomorFakturPajak = dr.Item("Nomor_Faktur_Pajak")
            LawanTransaksi = dr.Item("Nama_Lawan_Transaksi")
            COA = dr.Item("COA")
            KodeAkun = COA
            NamaAkun = dr.Item("Nama_Akun")
            KodeMataUang = dr.Item("Kode_Mata_Uang")
            Kurs = dr.Item("Kurs")
            DK = dr.Item("D_K")
            If DK = dk_K Then NamaAkun = PenjorokNamaAkun & NamaAkun
            JumlahDebet = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, Kurs, dr.Item("Jumlah_Debet"))
            JumlahKredit = AmbilValue_NilaiMataUang_WithCOA(COA, KodeMataUang, Kurs, dr.Item("Jumlah_Kredit"))
            TotalDebetPerJurnal += JumlahDebet
            TotalKreditPerJurnal += JumlahKredit
            UraianTransaksi = PenghapusEnter(dr.Item("Uraian_Transaksi"))
            If dr.Item("Direct") = 1 Then
                Direct = Pilihan_Ya
            Else
                Direct = Kosongan
            End If
            StatusApprove = dr.Item("Status_Approve")
            If JenisJurnal = Kosongan Then JenisJurnal = StripKosong
            If KodeDokumen = Kosongan Then KodeDokumen = StripKosong
            If NomorPO = Kosongan Then NomorPO = StripKosong
            If KodeProject = Kosongan Then KodeProject = StripKosong
            If NamaProduk = Kosongan Then NamaProduk = StripKosong
            If Referensi = Kosongan Then Referensi = StripKosong
            If TanggalInvoice = Kosongan Then TanggalInvoice = StripKosong
            If NomorInvoice = Kosongan Then NomorInvoice = StripKosong
            If NomorFakturPajak = Kosongan Then NomorFakturPajak = StripKosong
            If LawanTransaksi = Kosongan Then LawanTransaksi = StripKosong
            If UraianTransaksi = Kosongan Then UraianTransaksi = StripKosong

            If Not (JumlahDebet = 0 And JumlahKredit = 0) Then
                datatabelUtama.Rows.Add(AngkaNomorJV, TanggalJurnal,
                                    JenisJurnal, KodeDokumen, NomorPO, KodeProject, NamaProduk,
                                    Referensi, TanggalInvoice, NomorInvoice, NomorFakturPajak, LawanTransaksi, NamaAkun,
                                    KodeAkun, DK, JumlahDebet, JumlahKredit,
                                    UraianTransaksi, Direct, StatusApprove)
            End If
            AngkaNomorJV_Sebelumnya = AngkaNomorJV
            NomorJVSebelumnya = NomorJV
            Terabas()
            txt_JumlahList.Text = JumlahListJV
        Loop
        AksesDatabase_Transaksi(Tutup)

        datatabelUtama.Rows.Add()

        BersihkanSeleksi()

        'Dim NomorJV_Telusur = kosongan
        'Dim NomorJV_Sebelumnya = kosongan
        'For Each row As DataRow In datatabelUtama.Rows
        '    NomorJV_Telusur = row("Nomor_JV")
        '    'row.isReadOnly = True
        '    If row("Status_Approve") = 0 Then
        '        'row.DefaultCellStyle.ForeColor = Color.DarkSlateGray
        '        If NomorJV_Telusur <> kosongan And NomorJV_Telusur <> NomorJV_Sebelumnya Then
        '            'row.ReadOnly = False
        '        End If
        '    End If
        '    If row("Nama_Akun") = teks_CoaBelumTerdaftar Or row("Nama_Akun") = PenjorokNamaAkun & teks_CoaBelumTerdaftar Then
        '        'row.DefaultCellStyle.ForeColor = Color.Red
        '    End If
        '    NomorJV_Sebelumnya = NomorJV_Telusur
        'Next

        If AdaPembuanganSampahJurnal Then
            TampilkanData()
        End If

    End Sub


    Sub BersihkanSeleksi()
        JumlahBaris = datatabelUtama.Rows.Count
        BarisTerseleksi = -1
        datagridUtama.SelectedIndex = -1
        datagridUtama.SelectedItem = Kosongan
        datagridUtama.SelectedCells.Clear()
        KetersediaanTombolUpdate(False)
        KetersediaanTombolJurnalVoucher(False)
        KetersediaanMenuHalaman(pnl_Halaman, True)
    End Sub



    Sub KontenComboJenisJurnal()
        KontenComboJenisJurnal_Public_WPF(cmb_JenisJurnal)
        cmb_JenisJurnal.Items.Add(Pilihan_Semua)
        cmb_JenisJurnal.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenComboStatusApprove()
        cmb_StatusApprove.Items.Clear()
        cmb_StatusApprove.Items.Add(Pilihan_ALL_)
        cmb_StatusApprove.Items.Add(Pilihan_CLOSED_)
        cmb_StatusApprove.Items.Add(Pilihan_OPEN_)
        cmb_StatusApprove.SelectedValue = Pilihan_ALL_
    End Sub

    Sub KontenComboDirect()
        cmb_Direct.Items.Clear()
        cmb_Direct.Items.Add(Pilihan_Semua)
        cmb_Direct.Items.Add(Pilihan_Ya)
        cmb_Direct.Items.Add(Pilihan_Tidak)
        cmb_Direct.SelectedValue = Pilihan_Semua
    End Sub

    Sub KontenComboUrutBerdasarkan()
        cmb_UrutBerdasarkan.Items.Clear()
        cmb_UrutBerdasarkan.Items.Add("Nomor Voucher")
        cmb_UrutBerdasarkan.Items.Add("Tanggal Jurnal")
        cmb_UrutBerdasarkan.SelectedValue = "Nomor Voucher"
    End Sub

    Sub KontenComboASC()
        cmb_ASC.Items.Clear()
        cmb_ASC.Items.Add("Asc")
        cmb_ASC.Items.Add("Desc")
        cmb_ASC.SelectedValue = "Asc"
    End Sub


    Sub VisibilitasTombolSetujui(Visibilitas As Boolean)
        brd_Setujui.Visibility = Visibility.Collapsed
        btn_Setujui.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If LevelUserAktif >= LevelUser_02_Manager Then
                brd_Setujui.Visibility = Visibility.Visible
                btn_Setujui.Visibility = Visibility.Visible
            End If
        Else
            brd_Setujui.Visibility = Visibility.Collapsed
            btn_Setujui.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub VisibilitasTombolImpor(Visibilitas As Boolean)
        btn_Import.Visibility = Visibility.Collapsed
        If Visibilitas Then
            btn_Import.Visibility = Visibility.Visible
        Else
            btn_Import.Visibility = Visibility.Collapsed
        End If
        If LevelUserAktif = LevelUser_99_AppDeveloper Then btn_Import.Visibility = Visibility.Visible
    End Sub


    Sub VisibilitasFilterStatusApprove(Visibilitas As Boolean)
        brd_FilterStatusApprove.Visibility = Visibility.Collapsed
        pnl_FilterStatusApprove.Visibility = Visibility.Collapsed
        If Visibilitas Then
            If LevelUserAktif >= LevelUser_02_Manager Then
                brd_FilterStatusApprove.Visibility = Visibility.Visible
                pnl_FilterStatusApprove.Visibility = Visibility.Visible
            End If
        Else
            brd_FilterStatusApprove.Visibility = Visibility.Collapsed
            pnl_FilterStatusApprove.Visibility = Visibility.Collapsed
        End If
    End Sub


    Sub KetersediaanTombolJurnalVoucher(Tersedia As Boolean)
        btn_JurnalVoucher.IsEnabled = False
        If Tersedia Then
            btn_JurnalVoucher.IsEnabled = True
        Else
            btn_JurnalVoucher.IsEnabled = False
        End If
    End Sub


    Sub KetersediaanTombolUpdate(Tersedia As Boolean)
        btn_Edit.IsEnabled = False
        btn_Hapus.IsEnabled = False
        If Tersedia Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
        End If
    End Sub




    Private Sub btn_Refresh_Click(sender As Object, e As RoutedEventArgs) Handles btn_Refresh.Click
        RefreshTampilanData()
    End Sub

    Private Sub btn_JurnalVoucher_Click(sender As Object, e As RoutedEventArgs) Handles btn_JurnalVoucher.Click
        LihatJurnal(AngkaNomorJV_Terseleksi)
    End Sub

    Private Sub btn_Setujui_Click(sender As Object, e As RoutedEventArgs) Handles btn_Setujui.Click

        'Dim ProsesApprove As Boolean = False

        'Dim Baris
        'Dim BarisTelusur
        'Dim NomorJV_Telusur = kosongan
        'Dim NomorJV_Sebelumnya = kosongan
        'Dim JumlahTerceklis

        'Baris = 0
        'BarisTelusur = 0
        'JumlahTerceklis = 0
        'Do While BarisTelusur < JumlahListJV
        '    Dim Ceklis As Boolean = datatabelUtama.Item("Pilih_", Baris).Value
        '    NomorJV_Telusur = datatabelUtama.Item("Nomor_JV", Baris).Value
        '    If NomorJV_Telusur <> kosongan And NomorJV_Telusur <> NomorJV_Sebelumnya Then
        '        If Ceklis = True Then
        '            JumlahTerceklis = JumlahTerceklis + 1
        '        End If
        '        BarisTelusur = BarisTelusur + 1
        '    End If
        '    Baris = Baris + 1
        '    NomorJV_Sebelumnya = NomorJV_Telusur
        'Loop

        'If JumlahTerceklis = 0 Then
        '    MsgBox("Tidak ada jurnal yang terceklis.")
        '    Return
        'End If

        'Dim PilihSetujuiSemua = MessageBox.Show("Ada " & JumlahTerceklis & " jurnal yang terceklis." & Enter2Baris & "Yakin akan menyetujuinya..?", "Perhatian..!", MessageBoxButtons.YesNo)
        'If PilihSetujuiSemua = vbNo Then
        '    Return
        'End If

        'TrialBalance_Mentahkan()

        'Baris = 0
        'JumlahTerceklis = 0
        'Do While Baris < JumlahBaris
        '    Dim Ceklis As Boolean = datatabelUtama.Item("Pilih_", Baris).Value
        '    Dim AngkaNomorJV_Telusur = Microsoft.VisualBasic.Mid(datatabelUtama.Item("Nomor_JV", Baris).Value, PanjangTeks_AwalanNomorJV_Plus1)
        '    If Ceklis = True Then
        '        AksesDatabase_Transaksi(Buka)
        '        If StatusKoneksiDatabaseTransaksi = False Then
        '            ProsesApprove = False
        '            Exit Do
        '        End If
        '        cmd = New OdbcCommand(" UPDATE tbl_Transaksi SET " &
        '                              " Status_Approve = 1, Username_Approve = '" & UserAktif & "', " &
        '                              " Nama_User_Approve = '" & NamaUserAktif & "' " &
        '                              " WHERE Nomor_JV = '" & AngkaNomorJV_Telusur & "' ",
        '                              KoneksiDatabaseTransaksi)
        '        Try
        '            cmd.ExecuteNonQuery()
        '            ProsesApprove = True
        '        Catch ex As Exception
        '            ProsesApprove = False
        '        End Try
        '        'Update Status Jurnal di Tabel Pembelian, menjadi bernilai '1', agar bisa tampil di Buku Pengawasan Hutang Usaha.
        '        cmd = New OdbcCommand(" UPDATE tbl_Pembelian SET Status_Jurnal = 1 WHERE Nomor_JV = '" & AngkaNomorJV_Telusur & "' ", KoneksiDatabaseTransaksi)
        '        Try
        '            cmd.ExecuteNonQuery()
        '            ProsesApprove = True
        '        Catch ex As Exception
        '            ProsesApprove = False
        '        End Try
        '        AksesDatabase_Transaksi(Tutup)
        '    End If
        '    Baris = Baris + 1
        'Loop

        'TampilkanData()
        'CheckBoxHeader.Checked = False

        'If ProsesApprove = True Then
        '    MsgBox("Proses Approve massal SUKSES.")
        'Else
        '    MsgBox("Proses Approve massal GAGAL, atau hanya baru sebagian yang sukses..!" & Enter2Baris & teks_SilakanCobaLagi_Database)
        'End If

    End Sub

    Private Sub btn_Input_Click(sender As Object, e As RoutedEventArgs) Handles btn_Input.Click
        'If LevelUserAktif < LevelUser_99_AppDeveloper Then
        '    PesanPemberitahuan("Mohon maaf. Fitur ini sedang dalam perbaikan")
        '    Return
        'End If
        win_InputJurnal = New wpfWin_InputJurnal
        win_InputJurnal.ResetForm()
        win_InputJurnal.JalurMasuk = Halaman_JURNALUMUM
        win_InputJurnal.FungsiForm = FungsiForm_TAMBAH
        win_InputJurnal.ShowDialog()
    End Sub

    Private Sub btn_Edit_Click(sender As Object, e As RoutedEventArgs) Handles btn_Edit.Click
        If LevelUserAktif < LevelUser_99_AppDeveloper Then
            PesanPemberitahuan("Mohon maaf. Fitur ini sedang dalam perbaikan")
            Return
        End If
        EditJurnal(AngkaNomorJV_Terseleksi)
    End Sub

    Private Sub btn_Hapus_Click(sender As Object, e As RoutedEventArgs) Handles btn_Hapus.Click

        If Not TanyaKonfirmasi("Yakin ingin menghapus data nomor " & NomorJV_Terseleksi & "?") Then Return

        HapusJurnal_BerdasarkanNomorJV(AngkaNomorJV_Terseleksi)

        If StatusSuntingDatabase = True Then
            TampilkanData()
            Pesan_Sukses("Jurnal terpilih berhasil dihapus.")
        Else
            Pesan_Gagal("Jurnal terpilih gagal dihapus." & teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub btn_Import_Click(sender As Object, e As RoutedEventArgs) Handles btn_Import.Click

        If Not TanyaKonfirmasi("Fitur ini hanya digunakan untuk mengimport Data Jurnal yang berdiri sendiri dan tidak berkaitan dengan Data Finance yang ada disini." & Enter2Baris &
                                  "Lanjutkan import?") Then Return

        StatusImportJurnal = True

        win_ImportJurnal = New wpfWin_ImportJurnal
        win_ImportJurnal.ResetForm()
        win_ImportJurnal.ShowDialog()
        If StatusPosting = "BATAL" Then
            Pesan_Informasi("Proses posting telah dibatalkan seluruhnya pada event ini.")
        End If

        StatusImportJurnal = False

    End Sub


    Private Sub btn_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Export.Click
        EksporDataTableKeEXCEL(datatabelUtama, datagridUtama)
    End Sub


    Private Sub cmb_JenisJurnal_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisJurnal.SelectionChanged
        PilihanJenisJurnal = cmb_JenisJurnal.SelectedValue
        'TampilkanData()
    End Sub


    Private Sub dtp_DariTanggal_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_DariTanggal.SelectedDateChanged
        If dtp_DariTanggal.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_DariTanggal)
            DariTanggal_Date = dtp_DariTanggal.SelectedDate
        End If
    End Sub
    Private Sub dtp_DariTanggal_LostFocus(sender As Object, e As RoutedEventArgs) Handles dtp_DariTanggal.LostFocus
        txt_DariNomorJV.Text = Kosongan     'Filter berdasarkan NomorJV tidak berlaku saat adanya filter berdasarkan periode
        txt_SampaiNomorJV.Text = Kosongan   'Filter berdasarkan NomorJV tidak berlaku saat adanya filter berdasarkan periode
        If dtp_SampaiTanggal.Text = Kosongan Then
            dtp_SampaiTanggal.SelectedDate = TanggalFormatWPF(DariTanggal_Date)
        Else
            If SampaiTanggal_Date < DariTanggal_Date Then dtp_SampaiTanggal.SelectedDate = TanggalFormatWPF(DariTanggal_Date)
        End If
        'If dtp_DariTanggal.Text <> Kosongan Then TampilkanData()
    End Sub


    Private Sub dtp_SampaiTanggal_ValueChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtp_SampaiTanggal.SelectedDateChanged
        If dtp_SampaiTanggal.Text <> Kosongan Then
            KunciTahun_HarusSamaDenganTahunBukuAktif_WPF(dtp_SampaiTanggal)
            SampaiTanggal_Date = dtp_SampaiTanggal.SelectedDate
        End If
    End Sub
    Private Sub dtp_SampaiTanggal_LostFocus(sender As Object, e As RoutedEventArgs) Handles dtp_SampaiTanggal.LostFocus
        txt_DariNomorJV.Text = Kosongan     'Filter berdasarkan NomorJV tidak berlaku saat adanya filter berdasarkan periode
        txt_SampaiNomorJV.Text = Kosongan   'Filter berdasarkan NomorJV tidak berlaku saat adanya filter berdasarkan periode
        If dtp_DariTanggal.Text = Kosongan Then
            dtp_DariTanggal.SelectedDate = TanggalFormatWPF(DariTanggal_Date)
        Else
            If DariTanggal_Date > DariTanggal_Date Then dtp_DariTanggal.SelectedDate = TanggalFormatWPF(DariTanggal_Date)
        End If
        'If dtp_SampaiTanggal.Text <> Kosongan Then TampilkanData()
    End Sub


    Private Sub txt_DariNomorJV_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_DariNomorJV.TextChanged
        DariNomorJV = AmbilAngka(txt_DariNomorJV.Text)
        PemecahRibuanUntukTextBox_WPF(txt_DariNomorJV)
    End Sub
    Private Sub txt_DariNomorJV_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_DariNomorJV.PreviewTextInput
              
    End Sub
    Private Sub txt_DariNomorJV_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_DariNomorJV.LostFocus
        If DariNomorJV > 0 Then
            If SampaiNomorJV > 0 And SampaiNomorJV < DariNomorJV Then txt_SampaiNomorJV.Text = Kosongan
            dtp_DariTanggal.Text = Kosongan
            dtp_SampaiTanggal.Text = Kosongan
            'TampilkanData()
        End If
    End Sub


    Private Sub txt_SampaiNomorJV_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_SampaiNomorJV.TextChanged
        SampaiNomorJV = AmbilAngka(txt_SampaiNomorJV.Text)
        PemecahRibuanUntukTextBox_WPF(txt_SampaiNomorJV)
    End Sub
    Private Sub txt_SampaiNomorJV_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_SampaiNomorJV.PreviewTextInput
              
    End Sub
    Private Sub txt_SampaiNomorJV_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_SampaiNomorJV.LostFocus
        If SampaiNomorJV > 0 Then
            If DariNomorJV > 0 And DariNomorJV > SampaiNomorJV Then txt_DariNomorJV.Text = Kosongan
            dtp_DariTanggal.Text = Kosongan
            dtp_SampaiTanggal.Text = Kosongan
            'TampilkanData()
        End If
    End Sub


    Private Sub cmb_StatusApprove_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_StatusApprove.SelectionChanged

    End Sub


    Private Sub txt_COA_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_COA.TextChanged
        PilihanCOA = txt_COA.Text
    End Sub
    Private Sub txt_COA_GotFocus(sender As Object, e As RoutedEventArgs) Handles txt_COA.GotFocus
        PilihCOA()
    End Sub
    Private Sub btn_PilihCOA_Click(sender As Object, e As RoutedEventArgs) Handles btn_PilihCOA.Click
        PilihCOA()
    End Sub
    Sub PilihCOA()
        btn_PilihCOA.Focus()
        win_ListCOA = New wpfWin_ListCOA
        win_ListCOA.ResetForm()
        win_ListCOA.ListAkun = ListAkun_Semua
        If PilihanCOA <> Kosongan Then
            win_ListCOA.COATerseleksi = PilihanCOA
        End If
        win_ListCOA.ShowDialog()
        txt_COA.Text = win_ListCOA.COATerseleksi
        'TampilkanData()
    End Sub


    Private Sub txt_Cari_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Cari.TextChanged
        Cari = txt_Cari.Text
        'TampilkanData()
        txt_Cari.Focus()
    End Sub


    Private Sub cmb_Direct_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_Direct.SelectionChanged
        PilihanDirect = cmb_Direct.SelectedValue
        'TampilkanData()
    End Sub


    Private Sub btn_Filter_Click(sender As Object, e As RoutedEventArgs) Handles btn_Filter.Click
        TampilkanData()
    End Sub


    Private Sub cmb_UrutBerdasarkan_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_UrutBerdasarkan.SelectionChanged
        PilihanUrutan = cmb_UrutBerdasarkan.SelectedValue
        TampilkanData()
    End Sub

    Private Sub cmb_ASC_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_ASC.SelectionChanged
        PilihanASC = cmb_ASC.SelectedValue
        TampilkanData()
    End Sub




    Private Sub datagridUtama_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles datagridUtama.SelectionChanged
    End Sub
    Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.PreviewMouseLeftButtonUp
        HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
        If HeaderKolom IsNot Nothing Then
            BersihkanSeleksi()
        End If
    End Sub
    Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles datagridUtama.SelectedCellsChanged

        KolomTerseleksi = datagridUtama.CurrentColumn
        BarisTerseleksi = datagridUtama.SelectedIndex
        If BarisTerseleksi < 0 Then Return
        rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
        If Not rowviewUtama IsNot Nothing Then Return

        StatusApprove_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Status_Approve")
        Direct_Terseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Direct_")
        AngkaNomorJV_Terseleksi = AmbilAngka(rowviewUtama("Nomor_JV"))
        NomorJV_Terseleksi = AwalanNomorJV & AngkaNomorJV_Terseleksi

        If Direct_Terseleksi = Pilihan_Ya Then
            btn_Edit.IsEnabled = True
            btn_Hapus.IsEnabled = True
        Else
            btn_Edit.IsEnabled = False
            btn_Hapus.IsEnabled = False
        End If

        If AngkaNomorJV_Terseleksi > 0 Then
            btn_JurnalVoucher.IsEnabled = True
        Else
            btn_JurnalVoucher.IsEnabled = False
        End If

    End Sub
    Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles datagridUtama.MouseDoubleClick
        If AngkaNomorJV_Terseleksi > 0 Then LihatJurnal(AngkaNomorJV_Terseleksi)
    End Sub
    Private Sub datagridUtama_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles datagridUtama.LoadingRow
        'If LevelUserAktif = LevelUser_99_AppDeveloper Then
        If Not IsDBNull(e.Row.Item("Uraian_Transaksi")) Then
            If e.Row.Item("Uraian_Transaksi") = "Tidak Balance" Then
                e.Row.Foreground = WarnaMerahSolid_WPF
            Else
                e.Row.Foreground = WarnaTeksStandar_WPF
            End If
        End If
        'End If
    End Sub


    Private Sub txt_JumlahList_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_JumlahList.TextChanged
        PemecahRibuanUntukTextBox_WPF(txt_JumlahList)
    End Sub




    Sub New()
        InitializeComponent()
        Buat_DataTabelUtama()
        pnl_SidebarKiri.Visibility = Visibility.Collapsed
        pnl_SidebarKanan.Visibility = Visibility.Collapsed
        VisibilitasTombolSetujui(False)
        VisibilitasTombolImpor(False)
        VisibilitasFilterStatusApprove(False)
        txt_COA.IsReadOnly = True
        cmb_JenisJurnal.IsReadOnly = True
        cmb_StatusApprove.IsReadOnly = True
        cmb_Direct.IsReadOnly = True
        txt_JumlahList.IsReadOnly = True
    End Sub


    'Tabel Utama :
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView
    Public rowviewUtama As DataRowView
    Public newRow As DataRow
    Public HeaderKolom As DataGridColumnHeader
    Public KolomTerseleksi As DataGridColumn
    Public BarisTerseleksi As Integer
    Public JumlahBaris As Integer

    Dim Nomor_JV As New DataGridTextColumn
    Dim Tanggal_Jurnal As New DataGridTextColumn
    Dim Jenis_Jurnal As New DataGridTextColumn
    Dim Kode_Dokumen As New DataGridTextColumn
    Dim Nomor_PO As New DataGridTextColumn
    Dim Kode_Project As New DataGridTextColumn
    Dim Nama_Produk As New DataGridTextColumn
    Dim Referensi_ As New DataGridTextColumn
    Dim Tanggal_Invoice As New DataGridTextColumn
    Dim Nomor_Invoice As New DataGridTextColumn
    Dim Nomor_Faktur_Pajak As New DataGridTextColumn
    Dim Lawan_Transaksi As New DataGridTextColumn
    Dim Nama_Akun As New DataGridTextColumn
    Dim Kode_Akun As New DataGridTextColumn
    Dim D_K As New DataGridTextColumn
    Dim Jumlah_Debet As New DataGridTextColumn
    Dim Jumlah_Kredit As New DataGridTextColumn
    Dim Uraian_Transaksi As New DataGridTextColumn
    Dim Direct_ As New DataGridTextColumn
    Dim Status_Approve As New DataGridTextColumn
    Dim Ceklis_Approve As New DataGridCheckBoxColumn


    Sub Buat_DataTabelUtama()

        datatabelUtama = New DataTable

        datatabelUtama.Columns.Add("Nomor_JV")
        datatabelUtama.Columns.Add("Tanggal_Jurnal")
        datatabelUtama.Columns.Add("Jenis_Jurnal")
        datatabelUtama.Columns.Add("Kode_Dokumen")
        datatabelUtama.Columns.Add("Nomor_PO")
        datatabelUtama.Columns.Add("Kode_Project")
        datatabelUtama.Columns.Add("Nama_Produk")
        datatabelUtama.Columns.Add("Referensi_")
        datatabelUtama.Columns.Add("Tanggal_Invoice")
        datatabelUtama.Columns.Add("Nomor_Invoice")
        datatabelUtama.Columns.Add("Nomor_Faktur_Pajak")
        datatabelUtama.Columns.Add("Lawan_Transaksi")
        datatabelUtama.Columns.Add("Nama_Akun")
        datatabelUtama.Columns.Add("Kode_Akun")
        datatabelUtama.Columns.Add("D_K")
        datatabelUtama.Columns.Add("Jumlah_Debet", GetType(Int64))
        datatabelUtama.Columns.Add("Jumlah_Kredit", GetType(Int64))
        datatabelUtama.Columns.Add("Uraian_Transaksi")
        datatabelUtama.Columns.Add("Direct_")
        datatabelUtama.Columns.Add("Status_Approve")
        datatabelUtama.Columns.Add("Ceklis_Approve")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_JV, "Nomor_JV", "Nomor" & Enter1Baris & "JV", 63, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Jurnal, "Tanggal_Jurnal", "Tanggal", 75, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jenis_Jurnal, "Jenis_Jurnal", "Jenis Jurnal", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Dokumen, "Kode_Dokumen", "Kode Dokumen", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_PO, "Nomor_PO", "Nomor PO", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Project, "Kode_Project", "Kode Project", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Produk, "Nama_Produk", "Nama Barang/Jasa", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Referensi_, "Referensi", "Referensi", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Tanggal_Invoice, "Tanggal_Invoice", "Tanggal Invoice", 99, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Invoice, "Nomor_Invoice", "Nomor Invoice", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nomor_Faktur_Pajak, "Nomor_Faktur_Pajak", "Nomor Faktur Pajak", 123, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Lawan_Transaksi, "Lawan_Transaksi", "Lawan Transaksi", 150, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Nama_Akun, "Nama_Akun", "Nama Akun", 180, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kode_Akun, "Kode_Akun", "Kode" & Enter1Baris & "Akun", 63, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, D_K, "D_K", "D/K", 33, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Debet, "Jumlah_Debet", "Debet", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Jumlah_Kredit, "Jumlah_Kredit", "Kredit", 99, FormatAngka, KananTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Uraian_Transaksi, "Uraian_Transaksi", "Uraian Transaksi", 210, FormatString, KiriTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Direct_, "Direct_", "Direct", 45, FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Status_Approve, "Status_Approve", "Status Approve", 99, FormatString, KiriTengah, KunciUrut, Tersembunyi)
        TambahkanKolomCheckBoxDataGrid_WPF(datagridUtama, Ceklis_Approve, "Ceklis_Approve", "Ceklis", 63, FormatString, TengahTengah, KunciUrut, Tersembunyi, True)

    End Sub

    Sub datagridUtama_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles datagridUtama.SizeChanged
        KetentuanUkuran()
    End Sub
    Sub pnl_Konten_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles pnl_Konten.SizeChanged
        KetentuanUkuran()
    End Sub
    Dim LebarKonten As Integer
    Dim TinggiKonten As Integer
    Sub KetentuanUkuran()
        LebarKonten = pnl_Konten.ActualWidth
        TinggiKonten = pnl_Konten.ActualHeight
        datagridUtama.MaxHeight = TinggiKonten
        pnl_SidebarKiri.Height = TinggiKonten
        pnl_SidebarKanan.Height = TinggiKonten
        pnl_Footer.Width = LebarKonten
    End Sub

    Private Sub wpfWin_Closed(sender As Object, e As EventArgs) Handles Me.Unloaded
        StatusAktif = False
    End Sub

End Class
