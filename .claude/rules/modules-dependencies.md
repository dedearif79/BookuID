# Feature Modules & Dependencies

## Terminologi

| Istilah | Mengacu Kepada |
|---------|----------------|
| **BookuID** | Keseluruhan Solution (berisi 8 project) |
| **Booku** / **Project Booku** | Project utama di folder `BookuID/Booku/` |

## Solution BookuID (8 Projects)

Solution BookuID berisi 8 project yang saling terintegrasi. Jumlah project dapat bertambah seiring waktu sesuai kebutuhan.

| No | Project | Deskripsi | Status |
|----|---------|-----------|--------|
| 1 | **Booku** | **Project Utama.** Aplikasi Sistem Akuntansi Terpadu — memadukan finance, akuntansi, dan perhitungan pajak otomatis sesuai peraturan perpajakan Indonesia. Multicurrency (IDR + 7 mata uang asing). | Aktif, terus dikembangkan |
| 2 | **Booku Assistant** | Aplikasi pendukung untuk project Booku. | Belum dibuat |
| 3 | **Booku Backup** | Sistem backup folder solution BookuID secara otomatis. | Sudah jalan, terus dikembangkan |
| 4 | **Booku Encrypter** | Sistem enkripsi dan dekripsi untuk seluruh project dalam solution BookuID. | Sudah dibuat |
| 5 | **Booku Installer** | Sistem installer aplikasi Booku secara otomatis. | Ada, akan dirombak total |
| 6 | **Booku Library** | Library bersama (bcomm.dll) yang dipakai oleh seluruh project dalam solution BookuID. | Sudah jalan, terus dikembangkan |
| 7 | **Booku Uninstaller** | Sistem untuk mencabut instalasi Booku di PC klien/user. | Belum dibuat |
| 8 | **Booku Updater** | Sistem update aplikasi Booku secara otomatis. | Sudah jalan (sederhana), akan dikembangkan |

### Multicurrency Project Booku

Project Booku mendukung 8 mata uang:
- **IDR** (Rupiah) — mata uang utama
- **7 mata uang asing:** USD, AUD, JPY, CNY, EUR, SGD, GBP

Jumlah mata uang dapat ditambah sesuai kebutuhan klien/user.

### Dependencies Antar Project

Semua project bergantung pada **Booku Library** kecuali Booku Assistant (bergantung pada Booku Uninstaller).

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
