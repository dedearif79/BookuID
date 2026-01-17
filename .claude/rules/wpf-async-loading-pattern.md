# WPF Async Loading Pattern

> **STATUS: Pattern Baru (Migrasi Bertahap)**
>
> Pattern ini adalah sistem loading data baru yang akan **menggantikan pattern lama secara bertahap**.
>
> **PENTING:** Proses migrasi ke UserControl lain **HARUS dengan persetujuan user**, bukan inisiatif sepihak. Beberapa UserControl mungkin membutuhkan penyesuaian khusus.

## Tujuan Pattern Baru

1. **UI Responsive**: Loading window tetap responsive (animasi berputar) saat memuat data
2. **Mencegah Multiple Loading**: Guard clause mencegah loading dipanggil berulang kali
3. **Error Handling**: Try-Catch-Finally untuk menangani error dengan baik
4. **Backward Compatibility**: Wrapper method untuk kompatibilitas dengan kode lama
5. **Konsistensi Variabel Guard**: Standardisasi nama variabel guard untuk keseragaman

## Perbandingan Pattern Lama vs Baru

### Pattern Lama

```vb
Sub TampilkanData()
    ' Langsung query tanpa async
    datatabelUtama.Clear()
    AksesDatabase_Transaksi(Buka)
    cmd = New OdbcCommand(query, KoneksiDatabaseTransaksi)
    dr = cmd.ExecuteReader
    Do While dr.Read
        ' Proses data
        datatabelUtama.Rows.Add(...)
    Loop
    AksesDatabase_Transaksi(Tutup)
    BersihkanSeleksi()
End Sub

Sub BersihkanSeleksi()
    ' Reset seleksi + enable UI (digabung)
    datagridUtama.SelectedIndex = -1
    btn_Edit.IsEnabled = False
    KetersediaanMenuHalaman(pnl_Halaman, True)  ' Enable UI di sini
End Sub
```

### Pattern Baru (Async)

```vb
' Flag untuk mencegah multiple loading
Private SedangMemuatData As Boolean = False

' Flag untuk mencegah eksekusi saat ComboBox sedang diisi
Dim EksekusiTampilanData As Boolean

Sub RefreshTampilanData()
    EksekusiTampilanData = False
    KontenCombo_Filter1()
    KontenCombo_Filter2()
    EksekusiTampilanData = True
    TampilkanData()
End Sub

Async Sub TampilkanDataAsync()
    ' Guard clause
    If Not EksekusiTampilanData Then Return
    If SedangMemuatData Then Return
    SedangMemuatData = True

    ' Disable UI dan tampilkan loading
    KetersediaanMenuHalaman(pnl_Halaman, False)
    Await Task.Delay(50)  ' Beri waktu UI render

    Try
        datatabelUtama.Clear()
        AksesDatabase_Transaksi(Buka)
        cmd = New OdbcCommand(query, KoneksiDatabaseTransaksi)
        dr = cmd.ExecuteReader

        Do While dr.Read
            ' Proses data
            datatabelUtama.Rows.Add(...)
            Await Task.Yield()  ' Beri kesempatan UI refresh
        Loop

        AksesDatabase_Transaksi(Tutup)

    Catch ex As Exception
        mdl_Logger.WriteException(ex, "TampilkanDataAsync")

    Finally
        BersihkanSeleksi()
        KetersediaanMenuHalaman(pnl_Halaman, True)  ' Enable UI di Finally
        SedangMemuatData = False
    End Try
End Sub

' Wrapper untuk backward compatibility
Public Sub TampilkanData()
    TampilkanDataAsync()
End Sub

' Logika utama reset seleksi (TANPA enable UI)
Sub BersihkanSeleksi()
    JumlahBaris = datatabelUtama.Rows.Count
    BarisTerseleksi = -1
    datagridUtama.SelectedIndex = -1
    datagridUtama.SelectedItem = Nothing
    datagridUtama.SelectedCells.Clear()
    btn_Edit.IsEnabled = False
    btn_Hapus.IsEnabled = False
End Sub

' Wrapper: reset seleksi + enable UI (untuk backward compatibility)
Sub BersihkanSeleksi_SetelahLoading()
    BersihkanSeleksi()
    KetersediaanMenuHalaman(pnl_Halaman, True, False)
End Sub
```

## Struktur Method yang Diperlukan

| Method | Fungsi | Keterangan |
|--------|--------|------------|
| `TampilkanDataAsync()` | Method utama dengan async pattern | Menggantikan `TampilkanData()` lama |
| `TampilkanData()` | Wrapper untuk backward compatibility | Memanggil `TampilkanDataAsync()` |
| `BersihkanSeleksi()` | Logika utama reset seleksi | TANPA enable UI |
| `BersihkanSeleksi_SetelahLoading()` | Wrapper reset seleksi + enable UI | Untuk backward compatibility |

## Komponen yang Diperlukan

### 1. Import Statement

```vb
Imports System.Threading.Tasks
```

### 2. Flag Variables

```vb
' Flag untuk mencegah multiple loading bersamaan
Private SedangMemuatData As Boolean = False

' Flag untuk mencegah eksekusi TampilkanData saat ComboBox sedang diisi
Dim EksekusiTampilanData As Boolean
```

### 3. XAML Requirement

Root DockPanel harus memiliki `x:Name="pnl_Halaman"`:

```xml
<DockPanel x:Name="pnl_Halaman" Style="{StaticResource stylePanelFormHalaman}">
```

## Standarisasi Variabel Guard

### Variabel Lama yang Harus Diganti

Saat migrasi, variabel guard lama dengan nama berbeda-beda harus diganti menjadi `EksekusiTampilanData` untuk konsistensi:

| Variabel Lama | Ganti Menjadi | Keterangan |
|---------------|---------------|------------|
| `EksekusiKode` | `EksekusiTampilanData` | Nama lama yang sering digunakan |
| `ProsesLoadingData` | `EksekusiTampilanData` | Variasi lain |
| Variabel sejenis lainnya | `EksekusiTampilanData` | Standarisasi |

### Tujuan Variabel `EksekusiTampilanData`

Variabel ini mencegah `TampilkanData()` dipanggil saat `RefreshTampilanData()` sedang mengisi ComboBox filter. Tanpa guard ini, setiap perubahan ComboBox akan trigger `TampilkanData()` secara berulang.

### Pattern Penggunaan

```vb
Sub RefreshTampilanData()
    ' MATIKAN eksekusi TampilkanData saat isi ComboBox
    EksekusiTampilanData = False

    ' Isi semua ComboBox filter
    KontenCombo_Filter1()
    KontenCombo_Filter2()
    KontenCombo_Filter3()

    ' AKTIFKAN kembali setelah selesai
    EksekusiTampilanData = True

    ' Baru panggil TampilkanData
    TampilkanData()
End Sub

Async Sub TampilkanDataAsync()
    ' Guard: Jangan eksekusi jika sedang isi ComboBox
    If Not EksekusiTampilanData Then Return

    ' Guard: Jangan eksekusi jika sedang loading
    If SedangMemuatData Then Return

    ' ... sisa kode ...
End Sub
```

## Alur Eksekusi

```
TampilkanDataAsync()
    │
    ├── Guard: If SedangMemuatData Then Return
    ├── SedangMemuatData = True
    ├── KetersediaanMenuHalaman(False)  ← Disable UI + tampilkan loading
    ├── Await Task.Delay(50)            ← Beri waktu UI render
    │
    ├── Try:
    │   ├── Clear datatabel
    │   ├── Query database
    │   └── Loop: Add rows + Await Task.Yield()
    │
    ├── Catch: Log exception
    │
    └── Finally:
        ├── BersihkanSeleksi()
        ├── KetersediaanMenuHalaman(True)  ← Enable UI + tutup loading
        └── SedangMemuatData = False
```

## Checklist Migrasi

Saat migrasi UserControl ke pattern baru, pastikan:

- [ ] Tambah `Imports System.Threading.Tasks`
- [ ] Tambah flag `Private SedangMemuatData As Boolean = False`
- [ ] Tambah/ganti variabel guard menjadi `Dim EksekusiTampilanData As Boolean`
- [ ] Update `RefreshTampilanData()` untuk menggunakan `EksekusiTampilanData`
- [ ] Ubah `TampilkanData()` menjadi `TampilkanDataAsync()` dengan async pattern
- [ ] Buat wrapper `TampilkanData()` yang memanggil `TampilkanDataAsync()`
- [ ] Pisahkan `BersihkanSeleksi()` menjadi logika utama saja (tanpa enable UI)
- [ ] Tambah `BersihkanSeleksi_SetelahLoading()` sebagai wrapper
- [ ] Pastikan XAML memiliki `x:Name="pnl_Halaman"` di root DockPanel
- [ ] Build dan test

---

## Status Migrasi UserControl

### Sudah Dimigrasi

| UserControl | Lokasi | Tanggal |
|-------------|--------|---------|
| `wpfUsc_BukuPengawasanBuktiPengeluaranBankCash` | `Booku/Buku Pengawasan/Penerimaan-Pengeluaran/Bukti Pengeluaran Bank-Cash/` | 2026-01-17 |
| `wpfUsc_DataProject` | `Booku/Data/Data Project/` | 2026-01-17 |
| `wpfUsc_DataCOA` | `Booku/Data/Data COA/` | 2026-01-17 |

### Belum Dimigrasi (Kandidat)

> **Catatan:** Migrasi UserControl di bawah ini memerlukan persetujuan user dan mungkin penyesuaian khusus.

| UserControl | Lokasi | Catatan |
|-------------|--------|---------|
| `wpfUsc_BukuBesar` | `Booku/Buku Besar/` | - |
| `wpfUsc_DataKaryawan` | `Booku/Data/Data Karyawan/` | - |
| `wpfUsc_DataLawanTransaksi` | `Booku/Data/Data Lawan Transaksi/` | - |
| `wpfUsc_DataUser` | `Booku/Data/Data User/` | - |
| *(dan UserControl lainnya)* | - | - |

---

## Catatan Penting

1. **Jangan migrasi tanpa persetujuan** - Beberapa UserControl mungkin membutuhkan penyesuaian khusus
2. **Test setelah migrasi** - Pastikan loading, seleksi, dan tombol berfungsi normal
3. **Backward compatibility** - Wrapper method memastikan kode lama tetap berfungsi
