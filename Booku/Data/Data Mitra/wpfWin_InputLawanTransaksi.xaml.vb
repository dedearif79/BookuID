Imports bcomm
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Data.Odbc
Imports System.Text.RegularExpressions


Public Class wpfWin_InputLawanTransaksi

    Public FungsiForm
    Public JudulForm
    Public StatusEdit

    Dim KodeLawanTransaksi
    Dim NamaLawanTransaksi
    Dim UMKM
    Dim PemegangSaham
    Dim Afiliasi
    Dim Supplier
    Dim Customer
    Dim PKP
    Dim PemotongPPh
    Dim PJK
    Dim Keuangan
    Dim NPWP
    Dim JenisWP
    Dim LokasiWP
    Dim Alamat
    Dim Email
    Dim PIC
    Dim RekeningBank
    Dim AtasNama

    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        If FungsiForm = FungsiForm_TAMBAH Then
            JudulForm = "Input Lawan Transaksi"
            txt_KodeLawanTransaksi.IsReadOnly = False
        End If
        If FungsiForm = FungsiForm_EDIT Then
            JudulForm = "Edit Lawan Transaksi"
            txt_KodeLawanTransaksi.IsReadOnly = True
            IsiValueForm()
        End If

        If FungsiForm = Kosongan Then PesanUntukProgrammer("Fungsi Form belum ditentukan...!!!")

        Title = JudulForm


        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()

        ProsesIsiValueForm = True

        FungsiForm = Kosongan
        txt_KodeLawanTransaksi.Text = Kosongan
        txt_NamaLawanTransaksi.Text = Kosongan
        chk_UMKM.IsChecked = False
        chk_UMKM.IsEnabled = True
        chk_PemegangSaham.IsChecked = False
        chk_PemegangSaham.IsEnabled = True
        chk_Afiliasi.IsChecked = False
        chk_Afiliasi.IsEnabled = True
        chk_Supplier.IsChecked = False
        chk_Supplier.IsEnabled = True
        chk_Customer.IsChecked = False
        chk_Customer.IsEnabled = True
        chk_PKP.IsChecked = False
        chk_PKP.IsEnabled = True
        chk_PemotongPPh.IsChecked = False
        chk_PemotongPPh.IsEnabled = True
        chk_PJK.IsChecked = False
        chk_PJK.IsEnabled = True
        chk_Keuangan.IsChecked = False
        chk_Keuangan.IsEnabled = True
        txt_NPWP.Text = Kosongan
        KontenComboJenisWP()
        KontenComboLokasiWP()
        KosongkanValueElemenRichTextBox(txt_Alamat)
        txt_Email.Text = Kosongan
        txt_PIC.Text = Kosongan
        txt_RekeningBank.Text = Kosongan
        txt_AtasNama.Text = Kosongan

        ProsesIsiValueForm = False

    End Sub


    Sub KontenComboJenisWP()
        cmb_JenisWP.Items.Clear()
        cmb_JenisWP.Items.Add(JenisWP_OrangPribadi)
        cmb_JenisWP.Items.Add(JenisWP_BadanHukum)
        cmb_JenisWP.Text = Kosongan
    End Sub

    Sub KontenComboLokasiWP()
        cmb_LokasiWP.Items.Clear()
        cmb_LokasiWP.Items.Add(LokasiWP_DalamNegeri)
        cmb_LokasiWP.Items.Add(LokasiWP_LuarNegeri)
        cmb_LokasiWP.Text = Kosongan
    End Sub


    Sub IsiValueForm()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & txt_KodeLawanTransaksi.Text & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        txt_NamaLawanTransaksi.Text = dr.Item("Nama_Mitra")
        If dr.Item("UMKM") = 1 Then
            chk_UMKM.IsChecked = True
            'chk_UMKM.Enabled = False
        Else
            chk_UMKM.IsChecked = False
        End If
        If dr.Item("Pemegang_Saham") = 1 Then
            chk_PemegangSaham.IsChecked = True
            'chk_PemegangSaham.Enabled = False
        Else
            chk_PemegangSaham.IsChecked = False
        End If
        If dr.Item("Afiliasi") = 1 Then
            chk_Afiliasi.IsChecked = True
            'chk_Afiliasi.Enabled = False
        Else
            chk_Afiliasi.IsChecked = False
        End If
        If dr.Item("Supplier") = 1 Then
            chk_Supplier.IsChecked = True
            'chk_Supplier.Enabled = False
        Else
            chk_Supplier.IsChecked = False
        End If
        If dr.Item("Customer") = 1 Then
            chk_Customer.IsChecked = True
            'chk_Customer.Enabled = False
        Else
            chk_Customer.IsChecked = False
        End If
        If dr.Item("Keuangan") = 1 Then
            chk_Keuangan.IsChecked = True
            'chk_Keuangan.Enabled = False
        Else
            chk_Keuangan.IsChecked = False
        End If
        If dr.Item("PKP") = 1 Then
            chk_PKP.IsChecked = True
            'chk_PKP.Enabled = False
        Else
            chk_PKP.IsChecked = False
        End If
        If dr.Item("Pemotong_PPh") = 1 Then
            chk_PemotongPPh.IsChecked = True
            'chk_PemotongPPh.Enabled = False
        Else
            chk_PemotongPPh.IsChecked = False
        End If
        If dr.Item("PJK") = 1 Then
            chk_PJK.IsChecked = True
            'chk_PJK.Enabled = False
        Else
            chk_PJK.IsChecked = False
        End If
        txt_NPWP.Text = dr.Item("NPWP")
        cmb_JenisWP.SelectedValue = dr.Item("Jenis_WP")
        cmb_LokasiWP.SelectedValue = dr.Item("Lokasi_WP")
        IsiValueElemenRichTextBox(txt_Alamat, dr.Item("Alamat"))
        txt_Email.Text = dr.Item("Email")
        txt_PIC.Text = dr.Item("PIC")
        txt_RekeningBank.Text = dr.Item("Rekening_Bank")
        txt_AtasNama.Text = dr.Item("Atas_Nama")
        AksesDatabase_General(Tutup)
    End Sub



    Private Sub txt_KodeLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_KodeLawanTransaksi.TextChanged
        KodeLawanTransaksi = txt_KodeLawanTransaksi.Text.ToUpper
    End Sub
    Private Sub txt_KodeLawanTransaksi_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txt_KodeLawanTransaksi.PreviewTextInput
        Dim regex As New Regex("^[a-zA-Z0-9]+$")
        If Not regex.IsMatch(e.Text) Then
            e.Handled = True ' Mencegah input yang tidak sesuai
        End If
    End Sub
    Private Sub txt_KodeLawanTransaksi_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles txt_KodeLawanTransaksi.PreviewKeyDown
        If e.Key = Key.Space Then
            e.Handled = True ' Mencegah input spasi
        End If
    End Sub
    Private Sub txt_KodeLawanTransaksi_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_KodeLawanTransaksi.LostFocus
        If FungsiForm = FungsiForm_TAMBAH Then
            AksesDatabase_General(Buka)
            cmd = New OdbcCommand(" SELECT * FROM tbl_LawanTransaksi WHERE Kode_Mitra = '" & KodeLawanTransaksi & "' ", KoneksiDatabaseGeneral)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                Dim NamaMitra = dr.Item("Nama_Mitra")
                Pesan_Peringatan("Kode '" & KodeLawanTransaksi & "' sudah terdaftar" & Enter1Baris & "untuk " & NamaMitra & "." & Enter2Baris & "Silakan masukkan kode yang lain.")
                txt_KodeLawanTransaksi.Text = Kosongan
                txt_KodeLawanTransaksi.Focus()
                AksesDatabase_General(Tutup)
                Return
            End If
            AksesDatabase_General(Tutup)
        End If
    End Sub


    Private Sub txt_NamaLawanTransaksi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NamaLawanTransaksi.TextChanged
        NamaLawanTransaksi = txt_NamaLawanTransaksi.Text
    End Sub


    Private Sub chk_UMKM_Checked(sender As Object, e As RoutedEventArgs) Handles chk_UMKM.Checked
        UMKM = 1
    End Sub
    Private Sub chk_UMKM_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_UMKM.Unchecked
        UMKM = 0
    End Sub


    Private Sub chk_PemegangSaham_Checked(sender As Object, e As RoutedEventArgs) Handles chk_PemegangSaham.Checked
        PemegangSaham = 1
        chk_Afiliasi.IsChecked = True
    End Sub
    Private Sub chk_PemegangSaham_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_PemegangSaham.Unchecked
        PemegangSaham = 0
    End Sub


    Private Sub chk_Afiliasi_Checked(sender As Object, e As RoutedEventArgs) Handles chk_Afiliasi.Checked
        Afiliasi = 1
    End Sub
    Private Sub chk_Afiliasi_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_Afiliasi.Unchecked
        Afiliasi = 0
        chk_PemegangSaham.IsChecked = False
    End Sub


    Private Sub chk_Supplier_Checked(sender As Object, e As RoutedEventArgs) Handles chk_Supplier.Checked
        Supplier = 1
    End Sub
    Private Sub chk_Supplier_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_Supplier.Unchecked
        Supplier = 0
    End Sub


    Private Sub chk_Customer_Checked(sender As Object, e As RoutedEventArgs) Handles chk_Customer.Checked
        Customer = 1
    End Sub
    Private Sub chk_Customer_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_Customer.Unchecked
        Customer = 0
    End Sub


    Private Sub chk_Keuangan_Checked(sender As Object, e As RoutedEventArgs) Handles chk_Keuangan.Checked
        Keuangan = 1
    End Sub
    Private Sub chk_Keuangan_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_Keuangan.Unchecked
        Keuangan = 0
    End Sub


    Private Sub chk_PKP_Checked(sender As Object, e As RoutedEventArgs) Handles chk_PKP.Checked
        PKP = 1
    End Sub
    Private Sub chk_PKP_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_PKP.Unchecked
        PKP = 0
    End Sub


    Private Sub chk_PemotongPPh_Checked(sender As Object, e As RoutedEventArgs) Handles chk_PemotongPPh.Checked
        PemotongPPh = 1
    End Sub
    Private Sub chk_PemotongPPh_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_PemotongPPh.Unchecked
        PemotongPPh = 0
    End Sub


    Private Sub chk_PJK_Checked(sender As Object, e As RoutedEventArgs) Handles chk_PJK.Checked
        PJK = 1
    End Sub
    Private Sub chk_PJK_UnChecked(sender As Object, e As RoutedEventArgs) Handles chk_PJK.Unchecked
        PJK = 0
    End Sub


    Private Sub txt_NPWP_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_NPWP.TextChanged
        NPWP = txt_NPWP.Text
    End Sub


    Private Sub cmb_JenisWP_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_JenisWP.SelectionChanged
        JenisWP = cmb_JenisWP.SelectedValue
    End Sub


    Private Sub cmb_LokasiWP_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmb_LokasiWP.SelectionChanged
        LokasiWP = cmb_LokasiWP.SelectedValue
        If LokasiWP = LokasiPS_DalamNegeri Then
            chk_PKP.IsEnabled = True
            chk_PemotongPPh.IsEnabled = True
            chk_UMKM.IsEnabled = True
        Else
            chk_PKP.IsEnabled = False
            chk_PKP.IsChecked = False
            chk_PemotongPPh.IsEnabled = False
            chk_PemotongPPh.IsChecked = False
            chk_UMKM.IsEnabled = False
            chk_UMKM.IsChecked = False
        End If
    End Sub


    Private Sub txt_Alamat_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Alamat.TextChanged
        Alamat = IsiValueVariabelRichTextBox(txt_Alamat)
    End Sub


    Private Sub txt_Email_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Email.TextChanged
        Email = txt_Email.Text
    End Sub


    Private Sub txt_PIC_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_PIC.TextChanged
        PIC = txt_PIC.Text
    End Sub


    Private Sub txt_RekeningBank_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_RekeningBank.TextChanged
        RekeningBank = txt_RekeningBank.Text
    End Sub


    Private Sub txt_AtasNama_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_AtasNama.TextChanged
        AtasNama = txt_AtasNama.Text
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        If KodeLawanTransaksi = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_KodeLawanTransaksi, "Kode Lawan Transaksi")
            Return
        End If

        If Len(KodeLawanTransaksi) < 3 Then
            PesanPeringatan("Kode Lawan Transaksi minimal 3 huruf")
            txt_KodeLawanTransaksi.Focus()
            Return
        End If

        If NamaLawanTransaksi = Kosongan Then
            PesanPeringatan_SilakanIsiKolomTeks(txt_NamaLawanTransaksi, "Nama Lawan Transaksi")
            Return
        End If

        If JenisWP = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_JenisWP, "Jenis WP")
            Return
        End If

        If LokasiWP = Kosongan Then
            PesanPeringatan_SilakanPilihCombo(cmb_LokasiWP, "Lokasi WP")
            Return
        End If

        AksesDatabase_General(Buka)

        Dim QuerySimpan = Kosongan

        If FungsiForm = FungsiForm_TAMBAH Then

            QuerySimpan = " INSERT INTO tbl_LawanTransaksi VALUES ( " &
                " '" & KodeLawanTransaksi & "', " &
                " '" & NamaLawanTransaksi & "', " &
                " '" & UMKM & "', " &
                " '" & PemegangSaham & "', " &
                " '" & Afiliasi & "', " &
                " '" & Supplier & "', " &
                " '" & Customer & "', " &
                " '" & Keuangan & "', " &
                " '" & PKP & "', " &
                " '" & PemotongPPh & "', " &
                " '" & PJK & "', " &
                " '" & NPWP & "', " &
                " '" & JenisWP & "', " &
                " '" & LokasiWP & "', " &
                " '" & Alamat & "', " &
                " '" & Email & "', " &
                " '" & PIC & "', " &
                " '" & RekeningBank & "', " &
                " '" & AtasNama & "' ) "

        End If

        If FungsiForm = FungsiForm_EDIT Then

            QuerySimpan = " UPDATE tbl_LawanTransaksi SET " &
                " Nama_Mitra        = '" & NamaLawanTransaksi & "', " &
                " UMKM              = '" & UMKM & "', " &
                " Pemegang_Saham    = '" & PemegangSaham & "', " &
                " Afiliasi          = '" & Afiliasi & "', " &
                " Supplier          = '" & Supplier & "', " &
                " Customer          = '" & Customer & "', " &
                " Keuangan          = '" & Keuangan & "', " &
                " PKP               = '" & PKP & "', " &
                " Pemotong_PPh      = '" & PemotongPPh & "', " &
                " PJK               = '" & PJK & "', " &
                " NPWP              = '" & NPWP & "', " &
                " Jenis_WP          = '" & JenisWP & "', " &
                " Lokasi_WP         = '" & LokasiWP & "', " &
                " Alamat            = '" & Alamat & "', " &
                " Email             = '" & Email & "', " &
                " PIC               = '" & PIC & "', " &
                " Rekening_Bank     = '" & RekeningBank & "', " &
                " Atas_Nama         = '" & AtasNama & "' " &
                " WHERE Kode_Mitra  = '" & KodeLawanTransaksi & "' "

        End If

        cmd = New OdbcCommand(QuerySimpan, KoneksiDatabaseGeneral)
        cmd_ExecuteNonQuery()

        AksesDatabase_General(Tutup)

        If StatusSuntingDatabase Then
            pesan_DataBerhasilDisimpan()
            If usc_DataLawanTransaksi.StatusAktif Then usc_DataLawanTransaksi.TampilkanData()
            win_ListLawanTransaksi.TampilkanData()
            Me.Close()
        Else
            pesan_DataGagalDisimpan()
        End If

    End Sub


    Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
        Me.Close()
    End Sub




    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        cmb_JenisWP.IsReadOnly = True
        cmb_LokasiWP.IsReadOnly = True
        txt_KodeLawanTransaksi.MaxLength = 12
    End Sub

End Class
