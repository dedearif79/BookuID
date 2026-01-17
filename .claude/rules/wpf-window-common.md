# WPF Window Common Pattern

Pattern umum untuk WPF Window: dialog sederhana, deklarasi global, naming convention, dan best practices.

> **Lihat juga:**
> - `wpf-window-input-pattern.md` - Pattern Window Input/Edit
> - `wpf-window-list-pattern.md` - Pattern Window List/Picker

## 1. Pattern untuk Dialog Sederhana

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

## 2. Deklarasi Variabel Global

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
- Main window diinisialisasi di `App.xaml.vb`

## 3. Naming Convention

| Jenis Window | Format | Contoh |
|--------------|--------|--------|
| Input Dialog | `wpfWin_Input[Entity]` | `wpfWin_InputCOA`, `wpfWin_InputJurnal` |
| List/Picker | `wpfWin_List[Entity]` | `wpfWin_ListCOA`, `wpfWin_ListLawanTransaksi` |
| Dialog Utilitas | `wpfWin_[Action]` | `wpfWin_Login`, `wpfWin_PilihBulan` |
| Nested Input | `wpfWin_Input[Parent]_[Child]` | `wpfWin_InputJadwalAngsuran_PihakKetiga` |

## 4. Best Practices

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

## 5. Ringkasan Tipe Window

| Tipe Window | Tujuan | Tombol | Return Value |
|-------------|--------|--------|--------------|
| **Input** | CRUD data | Simpan + Batal | `DataTersimpan` flag |
| **List/Picker** | Pilih dari daftar | Pilih + Tutup | `_Terseleksi` properties |
| **Dialog Sederhana** | Konfirmasi/Input sederhana | OK + Batal | `StatusOK` flag |

## 6. Quick Reference

### Tipe-Tipe Window

| Tipe | Prefix | Tujuan | Pattern File |
|------|--------|--------|--------------|
| **Input/Edit** | `wpfWin_Input` | Form CRUD data | `wpf-window-input-pattern.md` |
| **List/Picker** | `wpfWin_List` | Menampilkan daftar untuk dipilih | `wpf-window-list-pattern.md` |
| **Dialog Sederhana** | `wpfWin_` | Login, konfirmasi, pilihan | File ini (section 1) |

### Properti Window Standar

```xml
<Window x:Class="wpfWin_NamaForm"
        Title="Judul Form"
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
