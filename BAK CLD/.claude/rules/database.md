# Database Architecture

## Remote Server

- **Server**: MySQL di sqlclusters.com

## Database Naming Convention

Prefix: `bookuid_booku_`

| Suffix | Deskripsi |
|--------|-----------|
| `_public` | Konfigurasi remote (read-only) |
| `_dasar` | Base configuration |
| `_general` | Data transaksional |
| `_transaksi` | Record transaksi |

## Connection Modules

- **mdl_KoneksiDatabase.vb** (`/Booku/Modul Umum/`) - Koneksi database (XAMPP/MySQL init, ODBC, OleDb, transaksi)
- **mdlPub_KoneksiDatabase.vb** (`/Booku Library/`) - Database utilities publik
