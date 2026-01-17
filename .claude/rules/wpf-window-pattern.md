# WPF Window Pattern

Pattern standar untuk membuat WPF Window di BookuID.

> **Dokumentasi ini telah dipecah menjadi 3 file terpisah untuk kemudahan navigasi.**

## Daftar File Pattern

| File | Konten | Kapan Digunakan |
|------|--------|-----------------|
| [`wpf-window-input-pattern.md`](wpf-window-input-pattern.md) | Pattern Window Input/Edit (CRUD) | Membuat form input data baru atau edit data |
| [`wpf-window-list-pattern.md`](wpf-window-list-pattern.md) | Pattern Window List/Picker | Membuat picker/selector untuk memilih dari daftar |
| [`wpf-window-common.md`](wpf-window-common.md) | Pattern umum, best practices | Dialog sederhana, naming, deklarasi global |

## Quick Reference

### Tipe-Tipe Window

| Tipe | Prefix | Tujuan | Pattern File |
|------|--------|--------|--------------|
| **Input/Edit** | `wpfWin_Input` | Form CRUD data | `wpf-window-input-pattern.md` |
| **List/Picker** | `wpfWin_List` | Menampilkan daftar untuk dipilih | `wpf-window-list-pattern.md` |
| **Dialog Sederhana** | `wpfWin_` | Login, konfirmasi, pilihan | `wpf-window-common.md` |

### Ringkasan Tipe Window

| Tipe Window | Tujuan | Tombol | Return Value |
|-------------|--------|--------|--------------|
| **Input** | CRUD data | Simpan + Batal | `DataTersimpan` flag |
| **List/Picker** | Pilih dari daftar | Pilih + Tutup | `_Terseleksi` properties |
| **Dialog Sederhana** | Konfirmasi/Input sederhana | OK + Batal | `StatusOK` flag |

### Naming Convention

| Jenis Window | Format | Contoh |
|--------------|--------|--------|
| Input Dialog | `wpfWin_Input[Entity]` | `wpfWin_InputCOA`, `wpfWin_InputJurnal` |
| List/Picker | `wpfWin_List[Entity]` | `wpfWin_ListCOA`, `wpfWin_ListLawanTransaksi` |
| Dialog Utilitas | `wpfWin_[Action]` | `wpfWin_Login`, `wpfWin_PilihBulan` |

## Properti Window Standar

```xml
<Window x:Class="wpfWin_NamaForm"
        Title="Judul Form"
        WindowStartupLocation="CenterScreen"
        WindowStyle="SingleBorderWindow"
        ResizeMode="NoResize"
        SizeToContent="WidthAndHeight">
```

## Best Practice: Pemanggilan Window

**WAJIB:** Selalu buat instance baru sebelum memanggil method apapun pada window.

```vb
' === PATTERN BENAR ===
win_InputEntity = New wpfWin_InputEntity    ' 1. Buat instance baru
win_InputEntity.ResetForm()                  ' 2. Reset form
win_InputEntity.FungsiForm = FungsiForm_TAMBAH
win_InputEntity.ShowDialog()                 ' 3. Tampilkan dialog

If win_InputEntity.DataTersimpan Then
    TampilkanData()
End If
```

---

> Untuk detail lengkap, lihat masing-masing file pattern di atas.
