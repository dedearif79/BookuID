Imports System.Windows
Imports System.Windows.Controls
Imports bcomm
Imports System.Data.Odbc

Public Class wpfWin_InputKodeDivisi

    ' === GUARD FLAGS ===
    Dim ProsesLoadingForm As Boolean
    Dim ProsesSuntingDatabase As Boolean

    ' === VARIABEL NILAI ===
    Dim KodeDivisi_Angka As Integer
    Dim KodeDivisi As String
    Dim Divisi As String

    ' === DATATABLE & DATAVIEW ===
    Public datatabelUtama As DataTable
    Public dataviewUtama As DataView

    ' === KOLOM DATAGRID ===
    Dim Kolom_KodeDivisi As New DataGridTextColumn
    Dim Kolom_Divisi As New DataGridTextColumn


    Sub New()
        InitializeComponent()
        StyleWindowDialogWPF_Dasar(Me)
        Buat_DataTabelUtama()

        txt_KodeDivisi.MaxLength = 3
        txt_Divisi.MaxLength = 100
    End Sub


    Sub Buat_DataTabelUtama()
        datatabelUtama = New DataTable
        datatabelUtama.Columns.Add("Kode_Divisi")
        datatabelUtama.Columns.Add("Divisi")

        StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_KodeDivisi, "Kode_Divisi", "Kode Divisi", 80,
                                          FormatString, TengahTengah, KunciUrut, Terlihat)
        TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_Divisi, "Divisi", "Divisi", 310,
                                          FormatString, KiriTengah, KunciUrut, Terlihat)
    End Sub


    Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        ProsesLoadingForm = True

        ResetForm()
        SistemPenomoranOtomatis_KodeDivisi()
        TampilkanData()

        txt_Divisi.Focus()

        ProsesLoadingForm = False

    End Sub


    Sub ResetForm()
        txt_KodeDivisi.Text = Kosongan
        txt_Divisi.Text = Kosongan
        btn_Simpan.IsEnabled = False

        KodeDivisi = Kosongan
        Divisi = Kosongan
    End Sub


    Sub SistemPenomoranOtomatis_KodeDivisi()
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset WHERE Kode_Divisi IN (SELECT MAX(Kode_Divisi) FROM tbl_DivisiAsset) ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            KodeDivisi_Angka = AmbilAngka(dr.Item("Kode_Divisi"))
        Else
            KodeDivisi_Angka = 0
        End If
        AksesDatabase_General(Tutup)

        KodeDivisi_Angka = KodeDivisi_Angka + 1

        If KodeDivisi_Angka < 10 Then
            KodeDivisi = "00" & KodeDivisi_Angka
        ElseIf KodeDivisi_Angka < 100 Then
            KodeDivisi = "0" & KodeDivisi_Angka
        Else
            KodeDivisi = KodeDivisi_Angka.ToString()
        End If

        txt_KodeDivisi.Text = KodeDivisi
    End Sub


    Sub TampilkanData()

        datatabelUtama.Rows.Clear()

        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset ORDER BY Kode_Divisi ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        Do While dr.Read
            Dim Kode = dr.Item("Kode_Divisi").ToString()
            Dim NamaDivisi = dr.Item("Divisi").ToString()
            datatabelUtama.Rows.Add(Kode, NamaDivisi)
        Loop
        AksesDatabase_General(Tutup)

        datagridUtama.SelectedIndex = -1

    End Sub


    Private Sub txt_Divisi_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Divisi.TextChanged
        If ProsesLoadingForm Then Return
        Divisi = txt_Divisi.Text
        btn_Simpan.IsEnabled = (txt_Divisi.Text <> Kosongan)
    End Sub


    Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

        ' === VALIDASI INPUT ===
        If Divisi = Kosongan Then
            Pesan_Peringatan("Silakan isi kolom 'Divisi'.")
            txt_Divisi.Focus()
            Return
        End If

        KodeDivisi = txt_KodeDivisi.Text

        ' === CEK DUPLIKAT ===
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" SELECT * FROM tbl_DivisiAsset WHERE Divisi = '" & Divisi & "' ", KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            Pesan_Peringatan("Divisi '" & Divisi & "' sudah ada." & Enter2Baris & "Silakan ketik nama yang lain.")
            txt_Divisi.Text = Kosongan
            txt_Divisi.Focus()
            AksesDatabase_General(Tutup)
            Return
        End If
        AksesDatabase_General(Tutup)

        ' === KONFIRMASI ===
        Dim Konfirmasi = TanyaKonfirmasi("Setelah disimpan, Kode Divisi tidak akan bisa dihapus dan diedit." & Enter2Baris & "Yakin akan menyimpan?")
        If Konfirmasi = False Then Return

        ' === SIMPAN KE DATABASE ===
        AksesDatabase_General(Buka)
        If StatusKoneksiDatabase = False Then Return
        cmd = New OdbcCommand(" INSERT INTO tbl_DivisiAsset VALUES ( '" & KodeDivisi & "', '" & Divisi & "' ) ", KoneksiDatabaseGeneral)
        Try
            cmd.ExecuteNonQuery()
            ProsesSuntingDatabase = True
        Catch ex As Exception
            ProsesSuntingDatabase = False
        End Try
        AksesDatabase_General(Tutup)

        ' === FEEDBACK ===
        If ProsesSuntingDatabase = True Then
            Pesan_Sukses("Kode Divisi berhasil disimpan.")
            txt_Divisi.Text = Kosongan
            SistemPenomoranOtomatis_KodeDivisi()
            TampilkanData()
            txt_Divisi.Focus()
        Else
            Pesan_Gagal("Kode Divisi gagal disimpan." & Enter2Baris & teks_SilakanCobaLagi_Database)
        End If

    End Sub


    Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
        Me.Close()
    End Sub

End Class
