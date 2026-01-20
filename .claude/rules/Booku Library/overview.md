# Booku Library

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Library** (folder `BookuID/Booku Library/`)

## Deskripsi

**Booku Library** adalah shared library yang digunakan oleh seluruh project dalam solution BookuID. Library ini dikompilasi menjadi `bcomm.dll`.

## Informasi Project

| Properti | Nilai |
|----------|-------|
| **Nama Project** | Booku Library |
| **Assembly Name** | `bcomm.dll` |
| **Namespace** | `bcomm` |
| **Target Framework** | .NET 8.0 Windows |
| **Arsitektur** | WPF murni (tanpa WinForms) |

## Struktur File

```
Booku Library/
├── Booku Library.vbproj       # Project file
├── mdlPub_ModulUmum.vb        # Modul utama (utilities, warna WPF)
├── mdlPub_Enkripsi.vb         # Enkripsi AES-256
├── mdlPub_KoneksiDatabase.vb  # Koneksi database public
├── mdlPub_VariabelUmum.vb     # Konstanta umum
├── mdlPub_Styling.vb          # Window styling helpers
└── cls_WindowDialogWPF_TanpaX.vb  # Class untuk window tanpa tombol X
```

## Modul dan Fungsinya

### 1. mdlPub_ModulUmum.vb

Modul utama yang berisi berbagai utilities:

| Kategori | Fungsi |
|----------|--------|
| **Variabel Warna WPF** | `WarnaHijauSolid_WPF`, `WarnaMerahSolid_WPF`, `WarnaHitam_XX_WPF`, dll |
| **Region Settings** | `appCulture`, `standardShortDateFormat`, `standardPemisahRibuan` |
| **API Keys** | `APIKey_PHPCode` (terenkripsi) |
| **Folder Paths** | `FolderRootBookuID` |

**Variabel Warna WPF yang Tersedia:**

```vb
' Warna Dasar (sesuai StyleColor.xaml)
WarnaHijauSolid_WPF      ' #388E3C - Primary/Brand Color
WarnaHijauTerang_WPF     ' #E8F5E9 - Primary Surface
WarnaMerahSolid_WPF      ' #F44336 - Error
WarnaMerahTerang_WPF     ' #FFEBEE - Error Surface
WarnaBiruSolid_WPF       ' #2196F3 - Info
WarnaBiruTerang_WPF      ' #E3F2FD - Info Surface
WarnaKuningSolid_WPF     ' #FF9800 - Warning
WarnaKuningTerang_WPF    ' #FFF3E0 - Warning Surface
WarnaPutih_WPF           ' #FFFFFF - White

' Grayscale (Neutral Scale)
WarnaHitam_5_WPF         ' #FAFAFA - Neutral50
WarnaHitam_10_WPF        ' #F5F5F5 - Neutral100
WarnaHitam_15_WPF        ' #EEEEEE - Neutral200
WarnaHitam_20_WPF        ' #E0E0E0 - Neutral300
WarnaHitam_25_WPF        ' #BDBDBD - Neutral400
WarnaHitam_30_WPF        ' #9E9E9E - Neutral500
WarnaHitam_35_WPF        ' #757575 - Neutral600
WarnaHitam_40_WPF        ' #616161 - Neutral700
WarnaHitam_45_WPF        ' #424242 - Neutral800
WarnaHitam_50_WPF        ' #212121 - Neutral900
```

> **Penting:** Warna-warna ini selaras dengan `StyleColor.xaml` di project Booku. Lihat `.claude/rules/Booku/wpf-styling.md` untuk dokumentasi lengkap sistem warna.

### 2. mdlPub_Enkripsi.vb

Modul enkripsi menggunakan AES-256:

| Fungsi | Deskripsi |
|--------|-----------|
| `EnkripsiTeksAES1/2/3()` | Enkripsi teks dengan key berbeda |
| `DekripsiTeksAES1/2/3()` | Dekripsi teks |
| `EnkripsiAngkaAES1/2/3()` | Enkripsi angka |
| `DekripsiAngkaAES1/2/3()` | Dekripsi angka |
| `EncryptString()` | Fungsi dasar enkripsi |
| `DecryptString()` | Fungsi dasar dekripsi |

**Catatan Keamanan:**
- Terdapat 3 pasang Key-IV (AES1, AES2, AES3) untuk berbagai kebutuhan
- Key dan IV **TIDAK BOLEH DIUBAH** karena akan merusak data terenkripsi

### 3. mdlPub_KoneksiDatabase.vb

Koneksi ke database public (remote server):

| Variabel | Deskripsi |
|----------|-----------|
| `LokasiServerDatabasePublic` | IP server MySQL (sqlclusters.com) |
| `PortDatabasePublic` | Port koneksi |
| `KoneksiDatabasePublic` | MySqlConnection object |
| `NamaDatabase_BookuID_Booku_Public` | Nama database public |

**URL Server:**

```vb
' FTP Server
ftpServerBookuID_Support           ' ftp://booku.id/booku/support/
ftpServerBookuID_BackUpDataClient  ' ftp://booku.id/booku/backup/client/

' HTTP URLs
urlFolderServerBookuID_Support     ' https://booku.id/booku/support/
urlFolderPHPCode                   ' https://booku.id/booku/phpcode/
```

**Fungsi Utama:**

```vb
BukaDatabasePublic()       ' Membuka koneksi ke database public
TutupDatabasePublic()      ' Menutup koneksi
cmdPublic_ExecuteNonQuery()  ' Eksekusi query non-select
drPublic_ExecuteReader()   ' Eksekusi query select
```

### 4. mdlPub_VariabelUmum.vb

Konstanta umum yang digunakan di seluruh solution:

```vb
' String kosong dan newline
Kosongan        ' String.Empty
Enter1Baris     ' Environment.NewLine
Enter2Baris     ' Double newline

' Spasi
Spasi1 ... Spasi5

' Header/Footer untuk file konfigurasi
HeaderConfig    ' Warning header untuk file config
FooterConfig    ' Footer dengan branding

' Nama file konfigurasi
NamaFileDataKoneksi            ' "0001.conf"
NamaFileRegistrasiPerangkat    ' "0002.conf"
NamaFileVersiDanApdetAplikasi  ' "0003.conf"
```

### 5. mdlPub_Styling.vb

Helper untuk styling WPF Window:

```vb
' Styling window dialog standar
StyleWindowDialogWPF_Dasar(Jendela)      ' SizeToContent, CenterScreen, NoResize

' Styling window tanpa tombol X (close)
StyleWindowDialogWPF_TanpaTombolX(Jendela)

' Styling window yang bisa di-resize
StyleWindowDialogWPF_Sizable(Jendela)
```

### 6. cls_WindowDialogWPF_TanpaX.vb

Class untuk membuat window tanpa tombol close (X):
- Menggunakan Win32 API (`user32.dll`)
- Menghapus system menu dari window

## Dependencies

| Package | Versi | Kegunaan |
|---------|-------|----------|
| MySql.Data | 9.2.0 | Koneksi MySQL |

## Cara Penggunaan

Library ini otomatis ter-reference oleh project lain dalam solution. Untuk menggunakan:

```vb
' Import namespace
Imports bcomm

' Contoh penggunaan
Dim teksEnkripsi = EnkripsiTeksAES1("teks rahasia")
Dim teksDekripsi = DekripsiTeksAES1(teksEnkripsi)

' Penggunaan warna
label.Foreground = WarnaHijauSolid_WPF
panel.Background = WarnaPutih_WPF

' Styling window
StyleWindowDialogWPF_Dasar(Me)
```

## Aturan Pengembangan

1. **Namespace**: Semua kode harus dalam namespace `bcomm`
2. **Prefix**: Gunakan prefix `mdlPub_` untuk modul publik, `cls_` untuk class
3. **Backward Compatibility**: Perubahan harus mempertahankan kompatibilitas dengan project lain
4. **Keamanan**: Jangan expose key enkripsi atau credential database
5. **Warna**: Sinkronkan variabel warna dengan `StyleColor.xaml` di project Booku
