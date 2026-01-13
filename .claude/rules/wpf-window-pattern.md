# WPF Window Pattern

Pattern standar untuk membuat WPF Window, berdasarkan analisis codebase BookuID.

## Tipe-Tipe Window

| Tipe | Prefix | Tujuan | Section |
|------|--------|--------|---------|
| **Input/Edit** | `wpfWin_Input` | Form CRUD data | 1-9 |
| **List/Picker** | `wpfWin_List` | Menampilkan daftar untuk dipilih | 10 |
| **Dialog Sederhana** | `wpfWin_` | Login, konfirmasi, pilihan | 11 |

## 1. Struktur UI (XAML)

### Layout Hierarki Standar

```
Window (Root)
├── Window.Resources
│   └── ResourceDictionary MergedDictionaries → StyleAplikasi.xaml
└── StackPanel (stylePanelFormDialog) - Root container
    ├── StackPanel (stylePanelFormDialogVertikal) - Header/Body
    │   └── Grid (styleGridFormDialog) - Input fields
    │       ├── ColumnDefinitions (label + input)
    │       └── RowDefinitions (per field)
    │           ├── TextBlock (label)
    │           └── TextBox / ComboBox / DatePicker / CheckBox
    └── StackPanel (stylePanelFormDialogTombol) - Footer
        ├── Button Simpan
        ├── Button Batal / Tutup
        └── Button lainnya (opsional)
```

### Properti Window Standar

```xml
<Window x:Class="wpfWin_NamaForm"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Judul Form"
        Height="Auto"
        Width="Auto"
        WindowStartupLocation="CenterScreen"
        WindowStyle="SingleBorderWindow"
        ResizeMode="NoResize"
        SizeToContent="WidthAndHeight">
```

### Resources untuk Design-Time

```xml
<Window.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="/WPF/Styles/StyleAplikasi.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Window.Resources>
```

### Contoh XAML Skeleton

```xml
<Window x:Class="wpfWin_InputEntity"
        Title="Input Entity"
        WindowStartupLocation="CenterScreen"
        ResizeMode="NoResize"
        SizeToContent="WidthAndHeight">

    <Window.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="/WPF/Styles/StyleAplikasi.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Window.Resources>

    <StackPanel Style="{StaticResource stylePanelFormDialog}">

        <!-- BODY: Input Fields -->
        <StackPanel Style="{StaticResource stylePanelFormDialogVertikal}">
            <Grid Style="{StaticResource styleGridFormDialog}">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="120"/>
                    <ColumnDefinition Width="*"/>
                </Grid.ColumnDefinitions>
                <Grid.RowDefinitions>
                    <RowDefinition Height="Auto"/>
                    <RowDefinition Height="Auto"/>
                    <RowDefinition Height="Auto"/>
                    <RowDefinition Height="Auto"/>
                    <RowDefinition Height="Auto"/>
                </Grid.RowDefinitions>

                <!-- Row 0: Kode (Teks String) -->
                <TextBlock Grid.Row="0" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Kode"/>
                <TextBox Grid.Row="0" Grid.Column="1" Style="{StaticResource styleTextBoxFormDialog}" x:Name="txt_Kode"/>

                <!-- Row 1: Nama (Teks String) -->
                <TextBlock Grid.Row="1" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Nama"/>
                <TextBox Grid.Row="1" Grid.Column="1" Style="{StaticResource styleTextBoxFormDialog}" x:Name="txt_Nama"/>

                <!-- Row 2: Jumlah (Angka dengan Separator Ribuan) -->
                <TextBlock Grid.Row="2" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Jumlah"/>
                <TextBox Grid.Row="2" Grid.Column="1" Style="{StaticResource styleTextBoxFormDialogAngkaSeparatePlus}" x:Name="txt_Jumlah"/>

                <!-- Row 3: Keterangan Label (dengan titik dua) -->
                <TextBlock Grid.Row="3" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Keterangan :"/>

                <!-- Row 4: Keterangan RichTextBox (full width) -->
                <RichTextBox Grid.Row="4" Grid.Column="0" Style="{StaticResource styleRichTextBoxFormDialog}" x:Name="txt_Keterangan" Grid.ColumnSpan="2"/>
            </Grid>
        </StackPanel>

        <!-- FOOTER: Tombol -->
        <StackPanel Style="{StaticResource stylePanelFormDialogTombol}">
            <Button x:Name="btn_Simpan" Content="Simpan"/>
            <Button x:Name="btn_Batal" Content="Batal"/>
        </StackPanel>

    </StackPanel>
</Window>
```

## 1a. Pattern Tipe Input Field

### Ringkasan Tipe Input Field

| Tipe Input | Style XAML | Code-Behind | Contoh |
|------------|------------|-------------|--------|
| **Teks String** | `styleTextBoxFormDialog` | Langsung `.Text` | Kode, Nama, Alamat |
| **Angka Ribuan** | `styleTextBoxFormDialogAngkaSeparatePlus` | `AmbilAngka()` | Jumlah, Harga, Saldo |
| **Keterangan/Uraian** | `styleRichTextBoxFormDialog` | Helper functions | Keterangan, Catatan, Uraian |

### a. Input Teks String

Untuk input teks biasa (kode, nama, alamat, dll).

**XAML:**
```xml
<TextBlock Grid.Row="0" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Nama"/>
<TextBox Grid.Row="0" Grid.Column="1" Style="{StaticResource styleTextBoxFormDialog}" x:Name="txt_Nama"/>
```

**Code-Behind:**
```vb
' Mengisi nilai
txt_Nama.Text = NilaiDariDatabase

' Mengosongkan
txt_Nama.Text = Kosongan

' Mengambil nilai
Nama = txt_Nama.Text
```

### b. Input Angka dengan Separator Ribuan

Untuk input angka yang perlu ditampilkan dengan pemisah ribuan (1.000.000).

> **PENTING:** Gunakan `styleTextBoxFormDialogAngkaSeparatePlus` - style ini **otomatis** menangani formatting separator ribuan. **JANGAN** panggil fungsi formatter manual seperti `PemecahRibuanUntukTextBox_WPF()`.

**XAML:**
```xml
<TextBlock Grid.Row="0" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Jumlah"/>
<TextBox Grid.Row="0" Grid.Column="1" Style="{StaticResource styleTextBoxFormDialogAngkaSeparatePlus}" x:Name="txt_Jumlah"/>
```

**Code-Behind:**
```vb
' Mengisi nilai - cukup .ToString(), style akan auto-format
txt_Jumlah.Text = JumlahDariDatabase.ToString()

' Mengosongkan
txt_Jumlah.Text = Kosongan

' Mengambil nilai - gunakan AmbilAngka() untuk mengekstrak angka dari teks dengan separator
Private Sub txt_Jumlah_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Jumlah.TextChanged
    If ProsesLoadingForm Then Return
    Jumlah = AmbilAngka(txt_Jumlah.Text)
End Sub
```

**Anti-Pattern (JANGAN DILAKUKAN):**
```vb
' SALAH: Memanggil formatter manual saat style sudah auto-format
txt_Jumlah.Text = JumlahDariDatabase.ToString()
PemecahRibuanUntukTextBox_WPF(txt_Jumlah)  ' <-- JANGAN!
```

### c. Input Keterangan/Catatan/Uraian (RichTextBox)

Untuk input teks multiline seperti keterangan, catatan, uraian.

> **PENTING:** Gunakan `RichTextBox` dengan `styleRichTextBoxFormDialog`, bukan `TextBox` dengan `TextWrapping="Wrap"`.

**XAML:**
```xml
<!-- Label di row terpisah dengan titik dua (:) -->
<TextBlock Grid.Row="3" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Keterangan :"/>

<!-- RichTextBox di row berikutnya dengan Grid.ColumnSpan="2" -->
<RichTextBox Grid.Row="4" Grid.Column="0" Style="{StaticResource styleRichTextBoxFormDialog}" x:Name="txt_Keterangan" Grid.ColumnSpan="2"/>
```

**Code-Behind - Helper Functions:**

| Fungsi | Tujuan | Penggunaan |
|--------|--------|------------|
| `IsiValueElemenRichTextBox(rtb, nilai)` | Mengisi nilai ke RichTextBox | Saat Loaded / IsiValueForm |
| `KosongkanValueElemenRichTextBox(rtb)` | Mengosongkan RichTextBox | Saat ResetForm |
| `IsiValueVariabelRichTextBox(rtb)` | Mengambil nilai dari RichTextBox | Saat TextChanged / btn_Simpan |

```vb
' === LOADED / ISIVALUEFORM ===
' Mengisi nilai ke RichTextBox
IsiValueElemenRichTextBox(txt_Keterangan, KeteranganDariDatabase)

' === RESETFORM ===
' Mengosongkan RichTextBox
KosongkanValueElemenRichTextBox(txt_Keterangan)

' === TEXTCHANGED ===
Private Sub txt_Keterangan_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txt_Keterangan.TextChanged
    If ProsesLoadingForm Then Return
    Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
End Sub

' === BTN_SIMPAN_CLICK (sebelum simpan ke database) ===
Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
```

**Anti-Pattern (JANGAN DILAKUKAN):**
```xml
<!-- SALAH: Menggunakan TextBox untuk multiline -->
<TextBox Grid.Row="3" Grid.Column="1" x:Name="txt_Keterangan"
         Height="80" TextWrapping="Wrap" AcceptsReturn="True"/>
```

### d. Contoh Lengkap Form dengan Berbagai Tipe Input

**XAML:**
```xml
<Grid Style="{StaticResource styleGridFormDialog}">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="120"/>
        <ColumnDefinition Width="200"/>
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>

    <!-- Teks String -->
    <TextBlock Grid.Row="0" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Kode"/>
    <TextBox Grid.Row="0" Grid.Column="1" Style="{StaticResource styleTextBoxFormDialog}" x:Name="txt_Kode"/>

    <!-- Teks String -->
    <TextBlock Grid.Row="1" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Nama"/>
    <TextBox Grid.Row="1" Grid.Column="1" Style="{StaticResource styleTextBoxFormDialog}" x:Name="txt_Nama"/>

    <!-- Angka dengan Separator Ribuan -->
    <TextBlock Grid.Row="2" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Jumlah"/>
    <TextBox Grid.Row="2" Grid.Column="1" Style="{StaticResource styleTextBoxFormDialogAngkaSeparatePlus}" x:Name="txt_Jumlah"/>

    <!-- Keterangan Label -->
    <TextBlock Grid.Row="3" Grid.Column="0" Style="{StaticResource styleTextBlockFormDialog}" Text="Keterangan :"/>

    <!-- Keterangan RichTextBox -->
    <RichTextBox Grid.Row="4" Grid.Column="0" Style="{StaticResource styleRichTextBoxFormDialog}" x:Name="txt_Keterangan" Grid.ColumnSpan="2"/>
</Grid>
```

**Code-Behind:**
```vb
' === VARIABEL ===
Dim Kode As String
Dim Nama As String
Dim Jumlah As Int64
Dim Keterangan As String

' === LOADED ===
Private Sub wpfWin_Loaded(...) Handles Me.Loaded
    ProsesLoadingForm = True

    txt_Kode.Text = KodeDariDatabase
    txt_Nama.Text = NamaDariDatabase
    txt_Jumlah.Text = JumlahDariDatabase.ToString()
    IsiValueElemenRichTextBox(txt_Keterangan, KeteranganDariDatabase)

    ProsesLoadingForm = False
End Sub

' === RESETFORM ===
Sub ResetForm()
    txt_Kode.Text = Kosongan
    txt_Nama.Text = Kosongan
    txt_Jumlah.Text = Kosongan
    KosongkanValueElemenRichTextBox(txt_Keterangan)

    Kode = Kosongan
    Nama = Kosongan
    Jumlah = 0
    Keterangan = Kosongan
End Sub

' === TEXTCHANGED HANDLERS ===
Private Sub txt_Kode_TextChanged(...) Handles txt_Kode.TextChanged
    If ProsesLoadingForm Then Return
    Kode = txt_Kode.Text
End Sub

Private Sub txt_Nama_TextChanged(...) Handles txt_Nama.TextChanged
    If ProsesLoadingForm Then Return
    Nama = txt_Nama.Text
End Sub

Private Sub txt_Jumlah_TextChanged(...) Handles txt_Jumlah.TextChanged
    If ProsesLoadingForm Then Return
    Jumlah = AmbilAngka(txt_Jumlah.Text)
End Sub

Private Sub txt_Keterangan_TextChanged(...) Handles txt_Keterangan.TextChanged
    If ProsesLoadingForm Then Return
    Keterangan = IsiValueVariabelRichTextBox(txt_Keterangan)
End Sub
```

## 2. Deklarasi Variabel Standar

### a. Public Properties (Komunikasi dengan Parent)

```vb
' === PUBLIC PROPERTIES ===
Public FungsiForm As String             ' FungsiForm_TAMBAH, FungsiForm_EDIT, FungsiForm_LIHAT
Public JudulForm As String              ' Judul window dinamis
Public JalurMasuk As String             ' Halaman_JURNALUMUM, Halaman_BUKUBESAR, dll
Public ProsesSuntingDatabase As Boolean ' Flag hasil operasi database
Public DataTersimpan As Boolean         ' Flag feedback ke parent
```

| Variabel | Tujuan |
|----------|--------|
| `FungsiForm` | Menentukan mode form (TAMBAH/EDIT/LIHAT) |
| `JudulForm` | Judul window yang dinamis |
| `JalurMasuk` | Identifikasi halaman pemanggil |
| `ProsesSuntingDatabase` | Hasil operasi database (True/False) |
| `DataTersimpan` | Feedback ke parent bahwa data berhasil disimpan |

### b. Guard Flags (Kontrol Eksekusi Event)

```vb
' === GUARD FLAGS ===
Dim ProsesLoadingForm As Boolean        ' Guard untuk event saat Loaded
Dim ProsesResetForm As Boolean          ' Guard untuk ResetForm()
Dim ProsesIsiValueForm As Boolean       ' Guard untuk IsiValueForm()
```

| Flag | Tujuan |
|------|--------|
| `ProsesLoadingForm` | Mencegah event TextChanged dieksekusi saat inisialisasi |
| `ProsesResetForm` | Mencegah event saat reset form |
| `ProsesIsiValueForm` | Mencegah event saat load data untuk edit |

### c. Variabel Nilai Form

```vb
' === NILAI FORM ===
Dim Kode As String
Dim Nama As String
Dim Keterangan As String
Dim Jumlah As Decimal
Dim Tanggal As Date
```

### d. Variabel untuk Return Value (Dialog)

```vb
' === RETURN VALUE ===
Public Property KodeTerseleksi As String
Public Property NamaTerseleksi As String
Public Property TombolPenutup As String = "Cancel"
```

## 3. Pattern Constructor

```vb
Sub New()
    InitializeComponent()

    ' Setup style window
    StyleWindowDialogWPF_Dasar(Me)

    ' Set read-only properties jika ada
    txt_Kode.IsReadOnly = True

    ' Set MaxLength untuk TextBox
    txt_Kode.MaxLength = 10
    txt_Nama.MaxLength = 100

    ' Buat DataTable jika form memiliki grid
    ' Buat_DataTabelUtama()
End Sub
```

## 4. Pattern Event Loaded

```vb
Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    ProsesLoadingForm = True

    ' ===== LOGIC BERDASARKAN FUNGSI FORM =====
    Select Case FungsiForm
        Case FungsiForm_TAMBAH
            JudulForm = "Input [Entity]"
            ResetForm()
            ' Enable field untuk input baru
            txt_Kode.IsEnabled = True

        Case FungsiForm_EDIT
            JudulForm = "Edit [Entity]"
            ResetForm()
            ' Disable field yang tidak boleh diedit
            txt_Kode.IsEnabled = False
            ' Load data dari database
            IsiValueForm()

        Case FungsiForm_LIHAT
            JudulForm = "Lihat [Entity]"
            ResetForm()
            ' Disable semua field
            txt_Kode.IsEnabled = False
            txt_Nama.IsEnabled = False
            btn_Simpan.Visibility = Visibility.Collapsed
            IsiValueForm()
    End Select

    ' ===== LOGIC KHUSUS BERDASARKAN JALUR MASUK =====
    If JalurMasuk = Halaman_JURNALUMUM Then
        ' Konfigurasi khusus
    End If

    ' Set title
    Title = JudulForm

    ' Set focus ke field pertama
    txt_Kode.Focus()

    ' Reset flag status
    DataTersimpan = False

    ProsesLoadingForm = False

End Sub
```

**Catatan Penting:**
- Selalu gunakan guard `ProsesLoadingForm` untuk mencegah event TextChanged
- Set `DataTersimpan = False` di akhir Loaded
- Gunakan `Select Case FungsiForm` untuk branch logic

## 5. Pattern ResetForm

```vb
Sub ResetForm()

    ProsesResetForm = True
    ProsesIsiValueForm = True

    ' ===== ISI COMBOBOX DARI DATABASE =====
    KontenCombo_JenisEntity()
    KontenCombo_Status()

    ' ===== KOSONGKAN SEMUA INPUT =====
    txt_Kode.Text = Kosongan
    txt_Nama.Text = Kosongan
    txt_Keterangan.Text = Kosongan
    KosongkanDatePicker(dtp_Tanggal)
    cmb_Jenis.SelectedIndex = -1

    ' ===== RESET ENABLE/DISABLE KONTROL =====
    txt_Kode.IsEnabled = True
    txt_Nama.IsEnabled = True
    btn_Simpan.IsEnabled = False

    ' ===== RESET TEXT TOMBOL =====
    btn_Batal.Content = teks_Tutup

    ' ===== RESET FLAG STATUS =====
    FungsiForm = Kosongan
    ProsesSuntingDatabase = False

    ' ===== RESET VARIABEL NILAI =====
    Kode = Kosongan
    Nama = Kosongan

    ProsesIsiValueForm = False
    ProsesResetForm = False

End Sub
```

## 6. Pattern IsiValueForm (untuk EDIT)

```vb
Sub IsiValueForm()

    ProsesIsiValueForm = True

    ' ===== QUERY DATABASE =====
    AksesDatabase_General(Buka)
    cmd = New OdbcCommand(" SELECT * FROM tbl_Entity WHERE Kode = '" & Kode & "' ",
                          KoneksiDatabaseGeneral)
    dr = cmd.ExecuteReader

    If dr.Read Then
        ' ===== ISI KONTROL DARI DATABASE =====
        txt_Kode.Text = dr("Kode").ToString
        txt_Nama.Text = dr("Nama").ToString
        txt_Keterangan.Text = dr("Keterangan").ToString

        ' Untuk ComboBox
        cmb_Jenis.SelectedValue = dr("Jenis").ToString

        ' Untuk DatePicker
        If Not IsDBNull(dr("Tanggal")) Then
            dtp_Tanggal.SelectedDate = dr("Tanggal")
        End If

        ' Untuk CheckBox
        chk_Aktif.IsChecked = (dr("Status").ToString = "1")
    End If

    AksesDatabase_General(Tutup)

    ProsesIsiValueForm = False

End Sub
```

## 7. Pattern Validasi Input

```vb
Private Sub btn_Simpan_Click(sender As Object, e As RoutedEventArgs) Handles btn_Simpan.Click

    ' ===== VALIDASI INPUT =====
    If Kode = Kosongan Then
        Pesan_Peringatan("Silakan isi kolom 'Kode'.")
        txt_Kode.Focus()
        Return
    End If

    If Nama = Kosongan Then
        Pesan_Peringatan("Silakan isi kolom 'Nama'.")
        txt_Nama.Focus()
        Return
    End If

    ' ===== VALIDASI DUPLIKAT (untuk TAMBAH) =====
    If FungsiForm = FungsiForm_TAMBAH Then
        AksesDatabase_General(Buka)
        cmd = New OdbcCommand(" SELECT Kode FROM tbl_Entity WHERE Kode = '" & Kode & "' ",
                              KoneksiDatabaseGeneral)
        dr = cmd.ExecuteReader()
        If dr.HasRows Then
            Pesan_Peringatan("Kode sudah ada. Silakan masukkan yang lain.")
            AksesDatabase_General(Tutup)
            Return
        End If
        AksesDatabase_General(Tutup)
    End If

    ' ... lanjutkan dengan proses penyimpanan ...

End Sub
```

**Pattern Penting:**
- Validasi SEBELUM akses database
- Cek duplikat hanya untuk mode TAMBAH

## 8. Pattern Dialog Result / Komunikasi dengan Parent

> **PENTING:** Selalu buat instance baru dengan `win_XXX = New wpfWin_XXX` sebelum memanggil method apapun (`ResetForm()`, `ShowDialog()`, dll). Ini memastikan window selalu dalam kondisi fresh setiap kali dibuka.

### Cara 1: Public Properties

```vb
' === DI FORM INPUT ===
Public Property KodeTerseleksi As String
Public Property NamaTerseleksi As String
Public Property TombolPenutup As String = "Cancel"

Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click
    KodeTerseleksi = txt_Kode.Text
    NamaTerseleksi = txt_Nama.Text
    TombolPenutup = "OK"
    Me.Close()
End Sub

' === DI PARENT ===
win_InputEntity = New wpfWin_InputEntity       ' WAJIB: Buat instance baru
win_InputEntity.ResetForm()
win_InputEntity.ShowDialog()

If win_InputEntity.TombolPenutup = "OK" Then
    Dim Kode = win_InputEntity.KodeTerseleksi
    Dim Nama = win_InputEntity.NamaTerseleksi
    ' Proses data
End If
```

### Cara 2: Public Flag

```vb
' === DI FORM INPUT ===
Public DataTersimpan As Boolean = False

Private Sub btn_Simpan_Click(...)
    ' ... proses simpan
    If ProsesSuntingDatabase = True Then
        DataTersimpan = True
        Me.Close()
    End If
End Sub

' === DI PARENT ===
win_InputEntity = New wpfWin_InputEntity       ' WAJIB: Buat instance baru
win_InputEntity.ResetForm()
win_InputEntity.FungsiForm = FungsiForm_TAMBAH
win_InputEntity.ShowDialog()

If win_InputEntity.DataTersimpan Then
    ' Data berhasil disimpan, refresh parent
    TampilkanData()
End If
```

### Cara 3: Callback ke Parent UserControl

```vb
' === DI FORM INPUT ===
Private Sub btn_Simpan_Click(...)
    ' ... proses simpan
    If ProsesSuntingDatabase = True Then
        ' Langsung refresh parent
        If usc_DataEntity.StatusAktif Then
            usc_DataEntity.TampilkanData()
        End If
    End If
End Sub
```

## 9. Pattern untuk Form dengan DataGrid

Untuk form yang memiliki DataGrid (seperti `wpfWin_InputJurnal`), tambahkan pattern berikut:

### Deklarasi Tambahan

```vb
' === DATATABLE & DATAVIEW ===
Public datatabelUtama As DataTable
Public dataviewUtama As DataView
Public rowviewUtama As DataRowView
Public newRow As DataRow
Public BarisTerseleksi As Integer

' === KOLOM DATAGRID ===
Dim Kolom_NomorUrut As New DataGridTextColumn
Dim Kolom_Kode As New DataGridTextColumn
Dim Kolom_Nama As New DataGridTextColumn
Dim Kolom_Jumlah As New DataGridTextColumn
```

### Setup DataTable

```vb
Sub Buat_DataTabelUtama()
    datatabelUtama = New DataTable
    datatabelUtama.Columns.Add("Nomor_Urut")
    datatabelUtama.Columns.Add("Kode")
    datatabelUtama.Columns.Add("Nama")
    datatabelUtama.Columns.Add("Jumlah", GetType(Int64))

    StyleTabelUtama_WPF(datagridUtama, datatabelUtama, dataviewUtama)

    TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_NomorUrut, "Nomor_Urut", "No.", 45,
                                      FormatAngka, KananTengah, KunciUrut, Terlihat)
    TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_Kode, "Kode", "Kode", 80,
                                      FormatString, TengahTengah, KunciUrut, Terlihat)
    TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_Nama, "Nama", "Nama", 200,
                                      FormatString, KiriTengah, KunciUrut, Terlihat)
    TambahkanKolomTextBoxDataGrid_WPF(datagridUtama, Kolom_Jumlah, "Jumlah", "Jumlah", 100,
                                      FormatAngka, KananTengah, KunciUrut, Terlihat)
End Sub
```

### Event SelectedCellsChanged

```vb
Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) _
    Handles datagridUtama.SelectedCellsChanged

    BarisTerseleksi = datagridUtama.SelectedIndex
    If BarisTerseleksi < 0 Then Return

    rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
    If rowviewUtama Is Nothing Then Return

    ' Ambil nilai terseleksi
    KodeTerseleksi = rowviewUtama("Kode").ToString
    NamaTerseleksi = rowviewUtama("Nama").ToString

    ' Update state tombol
    btn_Edit.IsEnabled = True
    btn_Hapus.IsEnabled = True
End Sub
```

### Handler CRUD DataGrid

```vb
Private Sub btn_TambahBaris_Click(sender As Object, e As RoutedEventArgs) _
    Handles btn_TambahBaris.Click

    ' Buka form input baris
    win_InputBaris = New wpfWin_InputBaris
    win_InputBaris.ResetForm()
    win_InputBaris.ShowDialog()

    If win_InputBaris.TombolPenutup = "OK" Then
        ' Tambah baris ke datatable
        newRow = datatabelUtama.NewRow()
        newRow("Kode") = win_InputBaris.KodeTerseleksi
        newRow("Nama") = win_InputBaris.NamaTerseleksi
        newRow("Jumlah") = win_InputBaris.Jumlah
        datatabelUtama.Rows.Add(newRow)

        ' Refresh tampilan
        RefreshNomorUrut()
    End If
End Sub
```

## 10. Pattern untuk Window List/Picker

Window List/Picker digunakan untuk menampilkan daftar data dan memilih item dari list. Tidak ada logika penyimpanan/update.

### Karakteristik Window List/Picker

| Aspek | Keterangan |
|-------|------------|
| **Tujuan** | Menampilkan daftar data untuk dipilih |
| **Tombol** | Pilih + Tutup (bukan Simpan + Batal) |
| **Return Value** | Public properties `_Terseleksi` |
| **Double-click** | Langsung memilih item |
| **Filter** | Real-time search saat mengetik |
| **Database** | Hanya SELECT (read-only) |

### Struktur XAML Window List

```xml
<Window x:Class="wpfWin_ListEntity"
        Title="Daftar Entity"
        WindowStartupLocation="CenterScreen"
        ResizeMode="NoResize">

    <Window.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="/WPF/Styles/StyleAplikasi.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Window.Resources>

    <StackPanel Style="{StaticResource stylePanelFormDialog}">

        <!-- BODY: Filter + DataGrid -->
        <StackPanel Style="{StaticResource stylePanelFormDialogVertikal}">

            <!-- Filter Pencarian -->
            <StackPanel Orientation="Horizontal" Margin="0 0 0 9">
                <StackPanel Margin="0 0 15 0">
                    <TextBlock Text="Cari Entity :"/>
                    <TextBox x:Name="txt_CariEntity" Width="200"/>
                </StackPanel>
                <!-- ComboBox filter tambahan (opsional) -->
                <StackPanel>
                    <TextBlock Text="Kategori :"/>
                    <ComboBox x:Name="cmb_Kategori" Width="120"/>
                </StackPanel>
            </StackPanel>

            <!-- DataGrid -->
            <DataGrid x:Name="datagridUtama"
                      AutoGenerateColumns="False"
                      MinWidth="400" MinHeight="300" MaxHeight="400"/>

        </StackPanel>

        <!-- FOOTER: Tombol -->
        <StackPanel Style="{StaticResource stylePanelFormDialogTombol}">
            <Button Style="{StaticResource styleButtonFormDialogPilih}" x:Name="btn_Pilih"/>
            <Button Style="{StaticResource styleButtonFormDialogTutup}" x:Name="btn_Tutup"/>
        </StackPanel>

    </StackPanel>
</Window>
```

### Deklarasi Variabel Window List

```vb
' === FILTER & QUERY ===
Dim JudulForm As String
Dim QueryTampilan As String
Dim FilterData As String

' === PUBLIC PROPERTIES (KONFIGURASI) ===
Public JalurMasuk As String
Public PilihKategori As String              ' Filter kategori dari parent

' === PUBLIC PROPERTIES (RETURN VALUE) ===
Public KodeTerseleksi As String
Public NamaTerseleksi As String
Public DataTambahanTerseleksi As String     ' Data tambahan jika diperlukan

' === DATATABLE & DATAGRID ===
Public datatabelUtama As DataTable
Public dataviewUtama As DataView
Public rowviewUtama As DataRowView
Public HeaderKolom As DataGridColumnHeader
Public KolomTerseleksi As DataGridColumn
Public BarisTerseleksi As Integer

' === KOLOM DATAGRID ===
Dim Kode_Entity As New DataGridTextColumn
Dim Nama_Entity As New DataGridTextColumn
```

### Pattern Constructor Window List

```vb
Sub New()
    InitializeComponent()
    StyleWindowDialogWPF_Dasar(Me)
    Buat_DataTabelUtama()
End Sub
```

### Pattern Event Loaded Window List

```vb
Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    ' ===== KONFIGURASI BERDASARKAN PARAMETER =====
    Select Case PilihKategori
        Case Kategori_Supplier
            JudulForm = "Daftar Supplier"
            lbl_CariEntity.Text = "Cari Supplier :"
        Case Kategori_Customer
            JudulForm = "Daftar Customer"
            lbl_CariEntity.Text = "Cari Customer :"
        Case Else
            JudulForm = "Daftar Entity"
            lbl_CariEntity.Text = "Cari Entity :"
    End Select

    Title = JudulForm

    ' ===== TAMPILKAN DATA =====
    RefreshTampilanData()

    ' ===== SET FOCUS =====
    txt_CariEntity.Focus()

End Sub
```

### Pattern TampilkanData Window List

```vb
Sub TampilkanData()

    ' ===== BUILD FILTER =====
    Dim FilterPencarian = " "
    If txt_CariEntity.Text <> Kosongan Then
        Dim Srch = txt_CariEntity.Text
        FilterPencarian = " AND (Kode LIKE '%" & Srch & "%' OR Nama LIKE '%" & Srch & "%') "
    End If

    Dim FilterKategori = " "
    If PilihKategori <> Pilihan_Semua Then
        FilterKategori = " AND Kategori = '" & PilihKategori & "' "
    End If

    ' ===== QUERY DATABASE =====
    FilterData = FilterKategori & FilterPencarian
    QueryTampilan = " SELECT * FROM tbl_Entity WHERE Status_Aktif = 1 " & FilterData

    datatabelUtama.Rows.Clear()
    AksesDatabase_General(Buka)
    cmd = New OdbcCommand(QueryTampilan & " ORDER BY Nama ", KoneksiDatabaseGeneral)
    dr_ExecuteReader()
    If StatusKoneksiDatabase = False Then Return

    Do While dr.Read
        Dim Kode = dr.Item("Kode")
        Dim Nama = dr.Item("Nama")
        datatabelUtama.Rows.Add(Kode, Nama)
    Loop

    AksesDatabase_General(Tutup)

    ' ===== RESET SELEKSI =====
    BersihkanSeleksi()
    txt_CariEntity.Focus()

End Sub

Sub RefreshTampilanData()
    TampilkanData()
End Sub
```

### Pattern ResetForm Window List

```vb
Public Sub ResetForm()

    ProsesResetForm = True

    ' ===== RESET FILTER =====
    txt_CariEntity.Text = Kosongan
    PilihKategori = Pilihan_Semua

    ' ===== RESET RETURN VALUE =====
    KodeTerseleksi = Kosongan
    NamaTerseleksi = Kosongan

    ' ===== RESET DATAGRID =====
    BarisTerseleksi = -1
    datatabelUtama.Rows.Clear()
    btn_Pilih.IsEnabled = False

    ProsesResetForm = False

End Sub
```

### Pattern BersihkanSeleksi Window List

```vb
Sub BersihkanSeleksi()
    BarisTerseleksi = -1
    datagridUtama.SelectedIndex = -1
    datagridUtama.SelectedItem = Nothing
    datagridUtama.SelectedCells.Clear()
    btn_Pilih.IsEnabled = False
End Sub
```

### Pattern Event Handler Window List

```vb
' ===== FILTER REAL-TIME =====
Private Sub txt_CariEntity_TextChanged(sender As Object, e As TextChangedEventArgs) _
    Handles txt_CariEntity.TextChanged
    btn_Pilih.IsEnabled = False
    TampilkanData()
End Sub

' ===== KLIK HEADER KOLOM =====
Private Sub datagridUtama_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) _
    Handles datagridUtama.PreviewMouseLeftButtonUp
    HeaderKolom = TryCast(e.OriginalSource, DataGridColumnHeader)
    If HeaderKolom IsNot Nothing Then
        BersihkanSeleksi()
    End If
End Sub

' ===== SELEKSI BARIS =====
Private Sub datagridUtama_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) _
    Handles datagridUtama.SelectedCellsChanged

    KolomTerseleksi = datagridUtama.CurrentColumn
    BarisTerseleksi = datagridUtama.SelectedIndex
    If BarisTerseleksi < 0 Then Return

    rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
    If rowviewUtama Is Nothing Then Return

    btn_Pilih.IsEnabled = True

End Sub

' ===== DOUBLE-CLICK UNTUK PILIH =====
Private Sub datagridUtama_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) _
    Handles datagridUtama.MouseDoubleClick
    If BarisTerseleksi < 0 Then Return
    btn_Pilih_Click(sender, Nothing)
End Sub
```

### Pattern Tombol Pilih Window List

```vb
Private Sub btn_Pilih_Click(sender As Object, e As RoutedEventArgs) Handles btn_Pilih.Click

    ' ===== VALIDASI SELEKSI =====
    If BarisTerseleksi < 0 Then
        PesanUntukProgrammer("Tidak ada baris terseleksi.")
        Return
    End If

    rowviewUtama = TryCast(datagridUtama.SelectedItem, DataRowView)
    If rowviewUtama Is Nothing Then Return

    ' ===== AMBIL NILAI TERSELEKSI =====
    KodeTerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Kode_Entity")
    NamaTerseleksi = AmbilValueCellTeksBerpotensiDBNull_RowView(rowviewUtama, "Nama_Entity")

    ' ===== VALIDASI TAMBAHAN (OPSIONAL) =====
    ' Contoh: validasi apakah item boleh dipilih
    If KondisiTidakBolehDipilih Then
        Pesan_Peringatan("Item ini tidak dapat dipilih.")
        Return
    End If

    ' ===== RESET FILTER & TUTUP =====
    txt_CariEntity.Text = Kosongan
    Me.Close()

End Sub

Private Sub btn_Tutup_Click(sender As Object, e As RoutedEventArgs) Handles btn_Tutup.Click
    Me.Close()
End Sub
```

### Penggunaan Window List dari Parent

```vb
' === DI PARENT (Form Input) ===
Private Sub btn_PilihEntity_Click(sender As Object, e As RoutedEventArgs) _
    Handles btn_PilihEntity.Click

    ' Buka window list
    win_ListEntity = New wpfWin_ListEntity
    win_ListEntity.ResetForm()
    win_ListEntity.PilihKategori = Kategori_Supplier   ' Set filter jika perlu
    win_ListEntity.ShowDialog()

    ' Cek apakah ada yang dipilih
    If win_ListEntity.KodeTerseleksi <> Kosongan Then
        txt_KodeEntity.Text = win_ListEntity.KodeTerseleksi
        txt_NamaEntity.Text = win_ListEntity.NamaTerseleksi
    End If

End Sub
```

### Ringkasan Alur Window List

```
Constructor: New()
    └── InitializeComponent()
        └── StyleWindowDialogWPF_Dasar()
            └── Buat_DataTabelUtama()

Event Loaded:
    └── Konfigurasi berdasarkan parameter
        └── RefreshTampilanData()
            └── TampilkanData()
                └── BersihkanSeleksi()
        └── txt_CariEntity.Focus()

txt_CariEntity_TextChanged:
    └── TampilkanData() (real-time filter)

datagridUtama_SelectedCellsChanged:
    └── btn_Pilih.IsEnabled = True

datagridUtama_MouseDoubleClick:
    └── btn_Pilih_Click()

btn_Pilih_Click:
    └── Ambil nilai _Terseleksi
        └── Me.Close()

btn_Tutup_Click:
    └── Me.Close() (tanpa return value)
```

## 11. Pattern untuk Dialog Sederhana

Untuk dialog sederhana seperti Login, Pilih Bulan, dll:

```vb
' === DEKLARASI ===
Public Property HasilPilihan As String
Public Property StatusOK As Boolean = False

' === LOADED ===
Private Sub wpfWin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
    ResetForm()
    txt_Input.Focus()
End Sub

' === RESETFORM ===
Sub ResetForm()
    txt_Input.Text = Kosongan
    btn_OK.IsEnabled = False
    StatusOK = False
End Sub

' === LOGIC ENABLE TOMBOL OK ===
Sub LogikaTombolOK()
    btn_OK.IsEnabled = (txt_Input.Text <> Kosongan)
End Sub

' === TEXT CHANGED ===
Private Sub txt_Input_TextChanged(sender As Object, e As TextChangedEventArgs) _
    Handles txt_Input.TextChanged
    LogikaTombolOK()
End Sub

' === TOMBOL OK ===
Private Sub btn_OK_Click(sender As Object, e As RoutedEventArgs) Handles btn_OK.Click
    HasilPilihan = txt_Input.Text
    StatusOK = True
    Me.Close()
End Sub

' === TOMBOL BATAL ===
Private Sub btn_Batal_Click(sender As Object, e As RoutedEventArgs) Handles btn_Batal.Click
    Me.Close()
End Sub
```

## 12. Deklarasi Variabel Global

File: `wpfMdl_ClassWindow.vb`

```vb
Module wpfMdl_ClassWindow

    ' ===== STARTUP & LOGIN =====
    Public win_Login As New wpfWin_Login
    Public win_KunciAkses As New wpfWin_KunciAkses

    ' ===== DATA MASTER =====
    Public win_InputCOA As New wpfWin_InputCOA
    Public win_InputLawanTransaksi As New wpfWin_InputLawanTransaksi
    Public win_InputUser As New wpfWin_InputUser
    Public win_InputKaryawan As New wpfWin_InputDataKaryawan

    ' ===== TRANSAKSI =====
    Public win_InputJurnal As New wpfWin_InputJurnal
    Public win_InputInvoice As New wpfWin_InputInvoicePembelian

    ' ===== DIALOG UTILITAS =====
    Public win_PilihBulan As New wpfWin_PilihBulan
    Public win_Loading As New wpfWin_Loading
    Public win_ListCOA As New wpfWin_ListCOA

    ' ===== MAIN WINDOW (TANPA As New) =====
    ' Inisialisasi di mdlWpf_Program.Main()
    Public win_BOOKU As wpfWin_BOOKU
    Public win_Startup As wpfWin_StartUp

End Module
```

**Aturan Penting:**
- Gunakan `As New` untuk dialog yang siap digunakan
- **JANGAN** gunakan `As New` untuk main window dan startup window
- Main window diinisialisasi di `mdlWpf_Program.Main()`

## 13. Naming Convention

| Jenis Window | Format | Contoh |
|--------------|--------|--------|
| Input Dialog | `wpfWin_Input[Entity]` | `wpfWin_InputCOA`, `wpfWin_InputJurnal` |
| List/Picker | `wpfWin_List[Entity]` | `wpfWin_ListCOA`, `wpfWin_ListLawanTransaksi` |
| Dialog Utilitas | `wpfWin_[Action]` | `wpfWin_Login`, `wpfWin_PilihBulan` |
| Nested Input | `wpfWin_Input[Parent]_[Child]` | `wpfWin_InputJadwalAngsuran_PihakKetiga` |

## 14. Best Practices

### Pemanggilan Window dari Parent

**WAJIB:** Selalu buat instance baru sebelum memanggil method apapun pada window.

```vb
' === PATTERN BENAR ===
Private Sub btn_PilihEntity_Click(...)
    win_ListEntity = New wpfWin_ListEntity    ' 1. Buat instance baru
    win_ListEntity.ResetForm()                 ' 2. Reset form
    win_ListEntity.Parameter = NilaiParameter  ' 3. Set parameter (opsional)
    win_ListEntity.ShowDialog()                ' 4. Tampilkan dialog

    ' 5. Ambil hasil
    If win_ListEntity.KodeTerseleksi <> Kosongan Then
        txt_Kode.Text = win_ListEntity.KodeTerseleksi
    End If
End Sub

' === PATTERN SALAH (JANGAN DILAKUKAN) ===
Private Sub btn_PilihEntity_Click(...)
    win_ListEntity.ResetForm()    ' ERROR: Instance mungkin sudah disposed/stale
    win_ListEntity.ShowDialog()
End Sub
```

**Alasan:**
- Window yang sudah ditutup (`Me.Close()`) tidak bisa dibuka ulang
- State dari pemanggilan sebelumnya bisa tertinggal
- Memastikan window selalu dalam kondisi fresh

### Guard Clauses

```vb
' Gunakan guard untuk mencegah event tidak diinginkan
ProsesLoadingForm = True
' ... inisialisasi ...
ProsesLoadingForm = False

' Di event handler
Private Sub txt_Input_TextChanged(...)
    If ProsesLoadingForm Then Return
    If ProsesResetForm Then Return
    ' ... logic normal ...
End Sub
```

### Validasi Multi-Layer

1. **Client-side** - TextChanged, PreviewTextInput untuk validasi realtime
2. **Form-level** - btn_Simpan_Click untuk validasi lengkap
3. **Database-level** - Cek duplikat sebelum INSERT

### Database Pattern

```vb
' Pattern standar akses database
AksesDatabase_General(Buka)
If StatusKoneksiDatabase = False Then Return

cmd = New OdbcCommand(query, KoneksiDatabaseGeneral)
Try
    cmd.ExecuteNonQuery()
    ProsesSuntingDatabase = True
Catch ex As Exception
    ProsesSuntingDatabase = False
End Try

AksesDatabase_General(Tutup)
```

### Message Feedback

| Fungsi | Kegunaan |
|--------|----------|
| `Pesan_Peringatan(msg)` | Warning/validasi gagal |
| `Pesan_Sukses(msg)` | Operasi berhasil |
| `Pesan_Gagal(msg)` | Error/operasi gagal |
| `TanyaKonfirmasi(msg)` | Konfirmasi sebelum aksi |

### UI State Management

```vb
' Distinct state untuk TAMBAH vs EDIT
Select Case FungsiForm
    Case FungsiForm_TAMBAH
        txt_Kode.IsEnabled = True       ' Bisa input kode baru
    Case FungsiForm_EDIT
        txt_Kode.IsEnabled = False      ' Kode tidak bisa diubah
End Select
```

## 15. Ringkasan Tipe Window

| Tipe Window | Tujuan | Tombol | Return Value |
|-------------|--------|--------|--------------|
| **Input** | CRUD data | Simpan + Batal | `DataTersimpan` flag |
| **List/Picker** | Pilih dari daftar | Pilih + Tutup | `_Terseleksi` properties |
| **Dialog Sederhana** | Konfirmasi/Input sederhana | OK + Batal | `StatusOK` flag |

## 16. Ringkasan Alur

### Window Input (CRUD)

```
Constructor: New()
    └── InitializeComponent()
        └── StyleWindowDialogWPF_Dasar()
            └── Setup awal (MaxLength, IsReadOnly)

Event Loaded:
    └── ProsesLoadingForm = True
        └── Select Case FungsiForm
            ├── TAMBAH → ResetForm()
            └── EDIT → ResetForm() + IsiValueForm()
        └── Title = JudulForm
        └── DataTersimpan = False
        └── ProsesLoadingForm = False

btn_Simpan_Click:
    └── Validasi input
        └── Validasi duplikat (untuk TAMBAH)
            └── ... proses penyimpanan ...
```

### Window List/Picker

```
Constructor: New()
    └── InitializeComponent()
        └── StyleWindowDialogWPF_Dasar()
            └── Buat_DataTabelUtama()

Event Loaded:
    └── Konfigurasi berdasarkan parameter
        └── RefreshTampilanData()
        └── txt_Cari.Focus()

txt_Cari_TextChanged:
    └── TampilkanData() (real-time filter)

datagridUtama_SelectedCellsChanged:
    └── btn_Pilih.IsEnabled = True

btn_Pilih_Click:
    └── Ambil nilai _Terseleksi
        └── Me.Close()
```
