# Booku Backup

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Backup** (folder `BookuID/Booku Backup/`)

## Deskripsi

**Booku Backup** adalah aplikasi untuk mem-backup folder solution BookuID secara otomatis. Aplikasi ini digunakan oleh developer untuk membuat backup source code dan database.

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Backup |
| **Output** | `Booku Backup.exe` (WinExe) |
| **Namespace** | `Booku_Backup` |
| **Target Framework** | .NET 8.0 Windows |
| **Arsitektur** | WPF |
| **Entry Point** | `Application.xaml` → `wpfWin_StartUp.xaml` |

## Struktur File

```
Booku Backup/
├── Booku Backup.vbproj       # Project file
├── Application.xaml          # Application entry point
├── Application.xaml.vb       # Application code-behind (minimal)
├── AssemblyInfo.vb           # Assembly information
├── wpfWin_StartUp.xaml       # Main window UI (menu utama)
├── wpfWin_StartUp.xaml.vb    # Main window logic
├── wpfWin_Progress.xaml      # Progress window UI
└── wpfWin_Progress.xaml.vb   # Progress window logic (backup file project)
```

## Dependencies

| Dependency | Tipe | Kegunaan |
|------------|------|----------|
| **Booku Library** | Project Reference | Akses ke `bcomm.dll` (kompresi, upload, utilities) |
| **MySql.Data** | NuGet Package | Koneksi database untuk backup DB |

## Fitur Utama

### 1. Backup File Project

Mengkompres seluruh folder solution BookuID dan mengupload ke server.

**Alur Kerja:**

```
┌─────────────────────────────────────────────────────────────────┐
│  1. PERSIAPAN                                                   │
│     ├── Hapus folder .vs (tidak perlu di-backup)                │
│     ├── Hapus file backup lama (2)                              │
│     └── Rename backup lama → backup (2)                         │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  2. KOMPRES PROJECT                                             │
│     ├── Source: D:\VB .Net Project\BookuID\                     │
│     ├── Target: D:\VB .Net Project\BACKUP\BookuID.zip           │
│     └── Tampilkan progress bar                                  │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  3. SALIN KE DOWNLOADS                                          │
│     └── Copy ke C:\Users\[user]\Downloads\BookuID.zip           │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│  4. UPLOAD KE SERVER (Opsional)                                 │
│     ├── Konfirmasi user: "Lanjut ke proses upload?"             │
│     ├── Pindahkan file lama ke folder bak/                      │
│     ├── Upload via chunked method                               │
│     └── Target: booku.id/booku/backup/project/                  │
└─────────────────────────────────────────────────────────────────┘
```

### 2. Backup Database

Fitur untuk backup database MySQL (masih dalam pengembangan sederhana).

**Status:** Implementasi dasar, perlu dikembangkan lebih lanjut.

## Struktur Folder yang Digunakan

```
D:\VB .Net Project\                   # FolderProject_UMUM
├── BookuID\                          # FolderProjectBookuID (source)
│   ├── .vs\                          # Dihapus sebelum backup
│   ├── Booku\
│   ├── Booku Library\
│   └── ... (project lainnya)
│
└── BACKUP\                           # FolderBACKUP_UMUM
    ├── BookuID.zip                   # Backup terbaru
    └── BookuID (2).zip               # Backup sebelumnya

C:\Users\[user]\Downloads\
└── BookuID.zip                       # Copy untuk upload

Server (booku.id):
└── booku/backup/project/
    ├── Sistem Akuntansi Terpadu - YYYY-MM-DD.zip  # Backup dengan tanggal
    └── bak/                          # Folder arsip backup lama
```

## Window dan Komponen UI

### wpfWin_StartUp (Main Window)

| Komponen | Nama | Fungsi |
|----------|------|--------|
| Button | `btn_BackupFileProject` | Mulai backup file project |
| Button | `btn_BackupDatabase` | Mulai backup database |

### wpfWin_Progress (Progress Window)

| Komponen | Nama | Fungsi |
|----------|------|--------|
| ProgressBar | `pgb_Progress` | Progress kompres/upload |
| TextBlock | `lbl_Progress` | Persentase progress |
| TextBlock | `lbl_Report_1` | Status kompres |
| TextBlock | `lbl_Report_2` | Status copy ke Downloads |
| TextBlock | `lbl_Report_3` | Status upload ke server |
| TextBlock | `lbl_Report_4` | (Reserved) |

## Variabel Penting

| Variabel | Nilai Default | Deskripsi |
|----------|---------------|-----------|
| `NamaProject` | `"BookuID"` | Nama folder project |
| `FolderProject_UMUM` | `"D:\VB .Net Project\"` | Folder induk project |
| `FolderBACKUP_UMUM` | `"D:\VB .Net Project\BACKUP"` | Folder backup lokal |
| `FolderDowload` | `"C:\Users\dedea\Downloads\"` | Folder downloads user |

## Fungsi dari Booku Library (bcomm)

| Fungsi | Kegunaan |
|--------|----------|
| `KompressFile()` | Kompres folder ke ZIP dengan progress |
| `UploadFileAsync_MetodeChunked()` | Upload file besar via chunked method |
| `PindahkanFileAntarFolderDiServer_MetodeHTTP()` | Rename/move file di server |
| `Terabas()` | Refresh UI |

## URL Server

| URL | Kegunaan |
|-----|----------|
| `urlFolderServerBookuID_BackUpProject` | `https://booku.id/booku/backup/project/` |
| `urlFileUplaodChunk_PHP` | PHP handler untuk upload chunk |
| `urlFileMergeChunks_PHP` | PHP handler untuk merge chunks |
| `urlFileRenamer_PHP` | PHP handler untuk rename file |

## Naming Convention File Backup

| Format | Contoh | Keterangan |
|--------|--------|------------|
| `BookuID.zip` | `BookuID.zip` | Backup lokal terbaru |
| `BookuID (2).zip` | `BookuID (2).zip` | Backup lokal sebelumnya |
| `Sistem Akuntansi Terpadu - YYYY-MM-DD.zip` | `Sistem Akuntansi Terpadu - 2026-01-20.zip` | Backup di server dengan tanggal |

## Status Pengembangan

| Fitur | Status | Catatan |
|-------|--------|---------|
| Backup File Project | Sudah jalan | Kompres + upload ke server |
| Backup Database | Sederhana | Perlu dikembangkan |
| Rotasi backup lokal | Terbatas | Hanya 2 versi (terbaru + sebelumnya) |
| Rotasi backup server | Ada | File lama dipindah ke folder `bak/` |

## Aturan Pengembangan

1. **Path Hardcoded**: Saat ini path masih hardcoded - perlu dikonfigurasi via settings
2. **Error Handling**: Tambahkan error handling yang lebih baik
3. **Logging**: Pertimbangkan menambah logging untuk tracking backup
4. **Konfigurasi**: Buat file konfigurasi untuk path dan settings
5. **Scheduler**: Pertimbangkan fitur backup otomatis terjadwal
