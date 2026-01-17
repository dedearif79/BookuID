# WPF Window List/Picker Pattern

Pattern standar untuk membuat WPF Window List/Picker (menampilkan daftar data untuk dipilih).

> **Lihat juga:**
> - `wpf-window-input-pattern.md` - Pattern Window Input/Edit
> - `wpf-window-common.md` - Pattern umum, best practices, naming convention

## Karakteristik Window List/Picker

Window List/Picker digunakan untuk menampilkan daftar data dan memilih item dari list. Tidak ada logika penyimpanan/update.

| Aspek | Keterangan |
|-------|------------|
| **Tujuan** | Menampilkan daftar data untuk dipilih |
| **Tombol** | Pilih + Tutup (bukan Simpan + Batal) |
| **Return Value** | Public properties `_Terseleksi` |
| **Double-click** | Langsung memilih item |
| **Filter** | Real-time search saat mengetik |
| **Database** | Hanya SELECT (read-only) |

## 1. Struktur XAML Window List

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

## 2. Deklarasi Variabel Window List

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

## 3. Pattern Constructor Window List

```vb
Sub New()
    InitializeComponent()
    StyleWindowDialogWPF_Dasar(Me)
    Buat_DataTabelUtama()
End Sub
```

## 4. Pattern Event Loaded Window List

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

## 5. Pattern TampilkanData Window List

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

## 6. Pattern ResetForm Window List

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

## 7. Pattern BersihkanSeleksi Window List

```vb
Sub BersihkanSeleksi()
    BarisTerseleksi = -1
    datagridUtama.SelectedIndex = -1
    datagridUtama.SelectedItem = Nothing
    datagridUtama.SelectedCells.Clear()
    btn_Pilih.IsEnabled = False
End Sub
```

## 8. Pattern Event Handler Window List

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

## 9. Pattern Tombol Pilih Window List

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

## 10. Penggunaan Window List dari Parent

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

## 11. Ringkasan Alur Window List

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

## 12. Perbedaan dengan Window Input

| Aspek | Window Input | Window List |
|-------|--------------|-------------|
| **Tujuan** | CRUD data | Pilih dari daftar |
| **Tombol** | Simpan + Batal | Pilih + Tutup |
| **Return Value** | `DataTersimpan` flag | `_Terseleksi` properties |
| **Database** | SELECT, INSERT, UPDATE, DELETE | Hanya SELECT |
| **Double-click** | Tidak ada | Langsung pilih |
| **Filter** | Biasanya di ComboBox | Real-time search |
