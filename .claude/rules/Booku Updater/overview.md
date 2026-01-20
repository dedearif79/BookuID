# Booku Updater

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Updater** (folder `BookuID/Booku Updater/`)

## Deskripsi

**Booku Updater** adalah aplikasi kecil yang berfungsi untuk mengupdate aplikasi Booku secara otomatis. Updater ini dijalankan terpisah dari aplikasi utama untuk menghindari konflik file saat proses update.

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Updater |
| **Output** | `Booku Updater.exe` (WinExe) |
| **Namespace** | `Booku_Updater` |
| **Target Framework** | .NET 8.0 Windows |
| **Arsitektur** | WPF |
| **Entry Point** | `Application.xaml` → `wpfWin_StartUp.xaml` |

## Struktur File

```
Booku Updater/
├── Booku Updater.vbproj      # Project file
├── Application.xaml          # Application entry point
├── Application.xaml.vb       # Application code-behind (minimal)
├── AssemblyInfo.vb           # Assembly information
├── wpfWin_StartUp.xaml       # Main window UI
└── wpfWin_StartUp.xaml.vb    # Main window logic
```

## Dependencies

| Dependency | Tipe | Kegunaan |
|------------|------|----------|
| **Booku Library** | Project Reference | Akses ke `bcomm.dll` (enkripsi, koneksi DB, utilities) |
| **Newtonsoft.Json** | NuGet Package | Parsing JSON (jika diperlukan) |

## Alur Kerja Update

```
┌─────────────────────────────────────────────────────────────────┐
│  1. STARTUP                                                     │
│     └── User klik "Lanjutkan >>"                                │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  2. AMBIL DATA PUBLIC                                           │
│     ├── Koneksi ke database public (sqlclusters.com)            │
│     ├── Query tbl_infoaplikasi                                  │
│     └── Ambil: Versi_App, Apdet_App, URL_Paket_Booku            │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  3. DOWNLOAD PAKET                                              │
│     ├── Buat folder temp: C:\BookuID\[Folder_Temp_Paket_Booku]  │
│     ├── Download ZIP dari URL_Paket_Booku                       │
│     └── Tampilkan progress bar                                  │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  4. EKSTRAK PAKET                                               │
│     ├── Hapus file lama di folder Booku (kecuali subfolder)     │
│     ├── Hapus folder runtimes                                   │
│     ├── Ekstrak ZIP ke C:\BookuID\Booku\                        │
│     └── Tampilkan progress bar                                  │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  5. SIMPAN VERSI                                                │
│     ├── Enkripsi versi dan update number                        │
│     └── Simpan ke file 0003.conf di folder Attach\Notes         │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  6. FINISH                                                      │
│     ├── Hapus folder temp                                       │
│     ├── Tampilkan pesan sukses/gagal                            │
│     └── User klik tombol → Jalankan Booku.exe                   │
└─────────────────────────────────────────────────────────────────┘
```

## Struktur Folder yang Digunakan

```
C:\BookuID\                           # FolderRootBookuID
├── Booku\                            # FolderRootBooku - folder aplikasi utama
│   ├── Booku.exe                     # Aplikasi utama
│   ├── runtimes\                     # Folder runtime (dihapus saat update)
│   ├── Attach\                       # Folder attachment (dipertahankan)
│   │   └── Notes\
│   │       └── 0003.conf             # File versi aplikasi
│   └── Client\                       # Folder data client (dipertahankan)
│
└── [Folder_Temp_Paket_Booku]\        # Folder temp untuk download
    └── [File_Paket_Booku].zip        # Paket update (ZIP)
```

## Komponen UI (wpfWin_StartUp.xaml)

| Komponen | Nama | Fungsi |
|----------|------|--------|
| TextBlock | `lbl_01` | Pesan status utama |
| TextBlock | `lbl_02` | Pesan status sekunder (tersembunyi) |
| ProgressBar | `pgb_Progress` | Progress download/ekstrak |
| TextBlock | `lbl_Progress` | Persentase progress |
| Button | `btn_Lanjutkan` | Tombol aksi ("Lanjutkan >>" / "Buka Aplikasi Booku >>") |

## Variabel Penting

| Variabel | Tipe | Deskripsi |
|----------|------|-----------|
| `VersiTerbaru` | String | Versi aplikasi terbaru dari database |
| `UpdateTerbaru` | String | Nomor update terbaru |
| `urlPaketBooku` | String | URL download paket ZIP |
| `ProsesUpdate_Aplikasi` | Boolean | Flag status proses update |
| `UpdateBerhasil` | Boolean | Flag hasil update |

## Database Table

Query ke `tbl_infoaplikasi` di database public:

| Kolom | Deskripsi |
|-------|-----------|
| `Versi_App` | Versi aplikasi terbaru |
| `Apdet_App` | Nomor update |
| `URL_Paket_Booku` | URL download paket |
| `Folder_Temp_Paket_Booku` | Nama folder temp |
| `File_Paket_Booku` | Nama file ZIP |

## Fungsi Utama

### Dari Booku Library (bcomm)

| Fungsi | Kegunaan |
|--------|----------|
| `BukaDatabasePublic()` / `TutupDatabasePublic()` | Koneksi database |
| `DownloadFileDariServerAsync_MetodeHTTP()` | Download file dengan progress |
| `EkstrakFile()` | Ekstrak ZIP dengan progress |
| `EnkripsiAngkaAES1()` | Enkripsi versi untuk file config |
| `SimpanDokumen()` | Simpan file teks |
| `HapusFolder()` / `HapusHanyaFileDalamFolder()` | Cleanup |
| `JalankanAplikasi()` | Jalankan Booku.exe |
| `StyleWindowDialogWPF_Dasar()` | Styling window |
| `StyleWindowDialogWPF_TanpaTombolX()` | Disable tombol close |

## Catatan Penting

1. **Window Tanpa Tombol X**: Saat proses update, tombol close (X) dinonaktifkan untuk mencegah user menutup aplikasi di tengah proses.

2. **Folder yang Dipertahankan**: Folder `Attach` dan `Client` tidak dihapus saat update karena berisi data user.

3. **Folder yang Dihapus**:
   - File di root folder Booku (diganti dengan file baru)
   - Folder `runtimes` (diganti dengan yang baru dari paket)

4. **File Konfigurasi Versi**: Versi disimpan terenkripsi di `0003.conf` agar tidak bisa dimanipulasi user.

## Status Pengembangan

| Status | Keterangan |
|--------|------------|
| **Saat Ini** | Sudah jalan (sederhana) |
| **Rencana** | Akan dikembangkan lebih lanjut |

## Aturan Pengembangan

1. **Dependency Minimal**: Updater harus sesederhana mungkin agar ukuran kecil
2. **Error Handling**: Pastikan proses update bisa di-rollback jika gagal
3. **Progress Feedback**: Selalu tampilkan progress ke user
4. **No Auto-Close**: Jangan tutup window otomatis - biarkan user yang memutuskan
5. **Cleanup**: Selalu hapus file temp setelah selesai (sukses maupun gagal)
