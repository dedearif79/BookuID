# Feature Modules & Dependencies

## Solution Structure (8 Projects)

| Project | Deskripsi | Namespace |
|---------|-----------|-----------|
| **Booku** | Aplikasi utama | Booku |
| **Booku Library** | Shared library | bcomm |
| **Booku Installer** | Installer + XAMPP setup | Booku_Installer |
| **Booku Updater** | Update utility | Booku_Updater |
| **Booku Backup** | Backup/restore/kloning | Booku_Backup |
| **Booku Encrypter** | Enkripsi utility | Booku_Encrypter |
| **Booku Assistant** | Supporting utility | Booku_Assistant |
| **Booku Uninstaller** | Uninstaller | Booku_Uninstaller |

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
