# Booku Encrypter

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Encrypter** (folder `BookuID/Booku Encrypter/`)

## Deskripsi

**Booku Encrypter** adalah aplikasi utilitas untuk enkripsi dan dekripsi teks/data. Aplikasi ini digunakan oleh developer untuk mengenkripsi string yang akan digunakan di kode sumber.

## Status Project

| Status | Keterangan |
|--------|------------|
| **Saat Ini** | Skeleton / Placeholder (belum diimplementasi) |
| **Rencana** | Akan dikembangkan sebagai tool enkripsi untuk developer |

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Encrypter |
| **Output** | `Booku Encrypter.exe` (WinExe) |
| **Namespace** | `Booku_Encrypter` |
| **Target Framework** | .NET 8.0 Windows |
| **Arsitektur** | WPF |
| **Entry Point** | `Application.xaml` → `wpfWin_StartUp.xaml` |

## Struktur File

```
Booku Encrypter/
├── Booku Encrypter.vbproj    # Project file
├── Application.xaml          # Application entry point
├── Application.xaml.vb       # Application code-behind (minimal)
├── AssemblyInfo.vb           # Assembly information
├── wpfWin_StartUp.xaml       # Main window UI (belum diimplementasi)
└── wpfWin_StartUp.xaml.vb    # Main window logic (kosong)
```

## Dependencies

| Dependency | Tipe | Kegunaan |
|------------|------|----------|
| **Booku Library** | Project Reference | Akses ke fungsi enkripsi AES (`EnkripsiTeksAES1/2/3`, `DekripsiTeksAES1/2/3`) |

## Fitur yang Direncanakan

### 1. Enkripsi Teks

Tool untuk mengenkripsi teks menggunakan fungsi AES dari Booku Library:

| Fungsi | Deskripsi |
|--------|-----------|
| `EnkripsiTeksAES1()` | Enkripsi dengan Key-IV set 1 |
| `EnkripsiTeksAES2()` | Enkripsi dengan Key-IV set 2 |
| `EnkripsiTeksAES3()` | Enkripsi dengan Key-IV set 3 |

### 2. Dekripsi Teks

Tool untuk mendekripsi teks terenkripsi:

| Fungsi | Deskripsi |
|--------|-----------|
| `DekripsiTeksAES1()` | Dekripsi dengan Key-IV set 1 |
| `DekripsiTeksAES2()` | Dekripsi dengan Key-IV set 2 |
| `DekripsiTeksAES3()` | Dekripsi dengan Key-IV set 3 |

### 3. Enkripsi/Dekripsi Angka

| Fungsi | Deskripsi |
|--------|-----------|
| `EnkripsiAngkaAES1/2/3()` | Enkripsi angka |
| `DekripsiAngkaAES1/2/3()` | Dekripsi angka |

## Use Case

1. **Developer perlu string terenkripsi untuk kode**
   - Input teks biasa
   - Pilih algoritma (AES1/2/3)
   - Copy hasil enkripsi ke kode sumber

2. **Verifikasi string terenkripsi**
   - Paste string terenkripsi
   - Dekripsi untuk melihat isi asli

## Komponen UI yang Direncanakan

| Komponen | Nama | Fungsi |
|----------|------|--------|
| TextBox | `txt_Input` | Input teks untuk dienkripsi/dekripsi |
| TextBox | `txt_Output` | Output hasil enkripsi/dekripsi |
| ComboBox | `cmb_Metode` | Pilihan metode (AES1, AES2, AES3) |
| Button | `btn_Enkripsi` | Eksekusi enkripsi |
| Button | `btn_Dekripsi` | Eksekusi dekripsi |
| Button | `btn_Copy` | Copy hasil ke clipboard |
| Button | `btn_Clear` | Reset form |

## Aturan Pengembangan

1. **Sederhana**: Fokus pada enkripsi/dekripsi teks saja
2. **Developer-Oriented**: UI tidak perlu fancy, yang penting fungsional
3. **Standalone**: Tidak perlu koneksi database atau network
4. **Copy-Paste Friendly**: Hasil harus mudah di-copy ke clipboard
5. **Multi-Mode**: Support ketiga varian AES (1, 2, 3)
