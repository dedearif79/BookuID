# Feature Modules & Dependencies

## Terminologi

| Istilah | Mengacu Kepada |
|---------|----------------|
| **BookuID** | Keseluruhan Solution (berisi 12 project) |
| **Booku** / **Project Booku** | Project utama di folder `BookuID/Booku/` |
| **Booku Styles** | Project styling terpusat di folder `BookuID/Booku Styles/` |

## Solution BookuID (12 Projects)

Solution BookuID berisi 12 project yang saling terintegrasi. Jumlah project dapat bertambah seiring waktu sesuai kebutuhan.

| No | Project | Deskripsi | Status |
|----|---------|-----------|--------|
| 1 | **Booku** | **Project Utama.** Aplikasi Sistem Akuntansi Terpadu — memadukan finance, akuntansi, dan perhitungan pajak otomatis sesuai peraturan perpajakan Indonesia. Multicurrency (IDR + 7 mata uang asing). | Aktif, terus dikembangkan |
| 2 | **Booku Assistant** | Aplikasi pendukung untuk project Booku. | Belum dibuat |
| 3 | **Booku Backup** | Sistem backup folder solution BookuID secara otomatis. | Sudah jalan, terus dikembangkan |
| 4 | **Booku Encrypter** | Aplikasi utilitas enkripsi/dekripsi teks untuk developer. | **Skeleton** (belum diimplementasi) |
| 5 | **Booku Installer** | Sistem installer aplikasi Booku secara otomatis. | Ada, akan dirombak total |
| 6 | **Booku Library** | Library bersama (bcomm.dll) yang dipakai oleh seluruh project dalam solution BookuID. | Sudah jalan, terus dikembangkan |
| 7 | **Booku Styles** | **Project Styling Terpusat.** Berisi XAML resources, behaviors, dan styling modules. Dikompilasi menjadi `BookuID.Styles.dll`. | Sudah jalan (post-migrasi) |
| 8 | **Booku Uninstaller** | Sistem untuk mencabut instalasi Booku di PC klien/user. | Belum dibuat |
| 9 | **Booku Updater** | Sistem update aplikasi Booku secara otomatis. | Sudah jalan (sederhana), akan dikembangkan |
| 10 | **Booku Remote** | Aplikasi remote desktop untuk mengontrol PC lain dalam jaringan LAN maupun internet. Mendukung screen sharing, keyboard/mouse control. | Fase 1, 2, 2b, 4 selesai. Fase 3 belum |
| 11 | **Booku Remote Android** | Aplikasi Android (MAUI/C#) untuk remote desktop sebagai Tamu. Menggunakan protokol yang sama dengan Booku Remote WPF. | Sudah jalan (LAN & Internet mode) |
| 12 | **Booku Remote Relay** | Server relay untuk koneksi remote desktop via internet. Berjalan di VPS sebagai perantara Host-Tamu. | Sudah jalan (port 443) |

### Multicurrency Project Booku

Project Booku mendukung 8 mata uang:
- **IDR** (Rupiah) — mata uang utama
- **7 mata uang asing:** USD, AUD, JPY, CNY, EUR, SGD, GBP

Jumlah mata uang dapat ditambah sesuai kebutuhan klien/user.

### Dependencies Antar Project

```
Booku Library (bcomm.dll)
    ↓
Booku Styles (BookuID.Styles.dll)
    ↓
Booku (Main App) + Project lainnya
```

**Catatan Dependencies:**
- **Booku Library**: Dipakai oleh semua project VB.NET (utilities, enkripsi, database)
- **Booku Styles**: Tergantung pada Booku Library, dipakai oleh Booku dan Booku Remote
- **Booku Remote**: Tergantung pada Booku Library dan Booku Styles
- **Booku Remote Android**: Standalone (C#/MAUI), protokol sama dengan Booku Remote WPF
- **Booku Remote Relay**: Standalone (C#/.NET 8), server relay di VPS untuk koneksi internet
- **Booku Assistant**: Bergantung pada Booku Uninstaller (khusus)

## Feature Modules (`/Booku/`)

| Kategori | Modul |
|----------|-------|
| **Akuntansi** | `Buku Besar`, `Jurnal`, `Laporan` |
| **Transaksi** | `Pembelian - Penjualan`, `Transaksi`, `Manajemen Asset` |
| **Data Master** | `Data/` (COA, Karyawan, Mitra, User, Project, Kurs) |
| **Buku Pengawasan** | Deposit Operasional, Gaji, Hutang Bank-Leasing, Hutang Pajak |
| **Admin** | `App Admin`, `App Developer`, `App IT`, `Security`, `Registrasi` |

## Key Dependencies (NuGet)

| Package | Kegunaan |
|---------|----------|
| MySql.Data | MySQL connectivity |
| ClosedXML | Excel handling |
| Microsoft.Web.WebView2 | Web content embedding |
| Newtonsoft.Json | JSON processing |
| HtmlAgilityPack | HTML parsing |
| Obfuscar | Code obfuscation (Release build) |
