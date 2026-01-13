# WPF Host Pattern

Pattern standar untuk membuat WPF Host yang membungkus UserControl dengan konfigurasi spesifik.

## Konsep Host

Host adalah class yang berfungsi sebagai **wrapper/pembungkus** untuk UserControl dengan konfigurasi tertentu. Satu UserControl dapat memiliki banyak varian Host dengan konfigurasi berbeda.

```
┌─────────────────────────────────────────────────────────────────┐
│ wpfHost_BukuPembelian_Lokal                                     │
│   ├── JudulForm = "Buku Pembelian"                              │
│   ├── Inisialisasi():                                           │
│   │     usc_BukuPembelian_Lokal = New wpfUsc_BukuPembelian      │
│   │     usc_BukuPembelian_Lokal.AsalPembelian = Lokal           │
│   └── Content = usc_BukuPembelian_Lokal                         │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│ wpfHost_BukuPembelian_Impor                                     │
│   ├── JudulForm = "Buku Pembelian - Impor"                      │
│   ├── Inisialisasi():                                           │
│   │     usc_BukuPembelian_Impor = New wpfUsc_BukuPembelian      │
│   │     usc_BukuPembelian_Impor.AsalPembelian = Impor           │
│   └── Content = usc_BukuPembelian_Impor                         │
└─────────────────────────────────────────────────────────────────┘
```

## Aturan Utama

| Aturan | Penjelasan |
|--------|------------|
| **1 file = 1 UserControl** | Setiap file `wpfHost_XXX.vb` hanya untuk 1 `wpfUsc_XXX` |
| **Banyak varian** | 1 file host bisa berisi banyak class varian dengan konfigurasi berbeda |
| **Lokasi file** | File host harus berada di **folder yang sama** dengan UserControl-nya |
| **Inherits ContentControl** | Semua class host harus inherit dari `ContentControl` |

## Struktur File Host

### File dengan Varian Tunggal (Sederhana)

```vb
Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_DataCOA
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Data COA (Default)
' ---------------------------------------------------------------------
Public Class wpfHost_DataCOA
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Data COA"
        Inisialisasi()
        Me.Content = usc_DataCOA
    End Sub

    Sub Inisialisasi()
        usc_DataCOA = New wpfUsc_DataCOA
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_DataCOA.RefreshTampilanData()
    End Sub

End Class
```

### File dengan Banyak Varian

```vb
Imports System.Windows.Controls

' =====================================================================
' WPF Host untuk wpfUsc_BukuPembelian
' 1 file berisi semua varian class yang mengarah ke 1 UserControl
' Menggunakan variabel usc_ yang sudah dideklarasikan di wpfMdl_ClassUserControl
' =====================================================================


' ---------------------------------------------------------------------
' VARIAN 1: Lokal
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPembelian_Lokal
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pembelian"
        Inisialisasi()
        Me.Content = usc_BukuPembelian_Lokal
    End Sub

    Sub Inisialisasi()
        usc_BukuPembelian_Lokal = New wpfUsc_BukuPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Lokal
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPembelian_Lokal.RefreshTampilanData()
    End Sub

End Class


' ---------------------------------------------------------------------
' VARIAN 2: Impor
' ---------------------------------------------------------------------
Public Class wpfHost_BukuPembelian_Impor
    Inherits ContentControl

    Public Property JudulForm As String

    Sub New()
        JudulForm = "Buku Pembelian - Impor"
        Inisialisasi()
        Me.Content = usc_BukuPembelian_Impor
    End Sub

    Sub Inisialisasi()
        usc_BukuPembelian_Impor = New wpfUsc_BukuPembelian With {
            .JudulForm = JudulForm,
            .AsalPembelian = AsalPembelian_Impor
        }
    End Sub

    Sub CekKesesuaianData()
        Inisialisasi()
        usc_BukuPembelian_Impor.RefreshTampilanData()
    End Sub

End Class
```

## Struktur Class Host

Setiap class Host memiliki 3 komponen utama:

### 1. Property

```vb
Public Property JudulForm As String
Public Property KodeMataUang As String = KodeMataUang_IDR    ' Default value jika perlu
```

### 2. Constructor (Sub New)

```vb
Sub New()
    ' 1. Set judul form
    JudulForm = "Nama Form"

    ' 2. Panggil inisialisasi
    Inisialisasi()

    ' 3. Set content ke UserControl
    Me.Content = usc_NamaUserControl
End Sub
```

### 3. Sub Inisialisasi

```vb
Sub Inisialisasi()
    ' Buat instance UserControl dengan konfigurasi spesifik
    usc_NamaUserControl = New wpfUsc_NamaUserControl With {
        .Property1 = Value1,
        .Property2 = Value2,
        .Property3 = Value3
    }

    ' Atau dengan assignment terpisah
    usc_NamaUserControl = New wpfUsc_NamaUserControl
    usc_NamaUserControl.Property1 = Value1
    usc_NamaUserControl.Property2 = Value2

    ' Panggil method konfigurasi jika perlu
    usc_NamaUserControl.VisibilitasFilterJenisRelasi(True)
End Sub
```

### 4. Sub CekKesesuaianData

```vb
Sub CekKesesuaianData()
    ' Re-inisialisasi untuk memastikan data terbaru
    Inisialisasi()

    ' Refresh tampilan data
    usc_NamaUserControl.RefreshTampilanData()
End Sub
```

## Naming Convention

### File Host

| Komponen | Format | Contoh |
|----------|--------|--------|
| **File** | `wpfHost_[NamaUserControl].vb` | `wpfHost_BukuPembelian.vb` |
| **Class default** | `wpfHost_[NamaUserControl]` | `wpfHost_BukuPembelian` |
| **Class varian** | `wpfHost_[NamaUserControl]_[Varian]` | `wpfHost_BukuPembelian_Lokal` |

### Variabel Host (di wpfMdl_ClassHost.vb)

| Komponen | Format | Contoh |
|----------|--------|--------|
| **Variabel** | `host_[NamaVarian]` | `host_BukuPembelian_Lokal` |

### Variabel UserControl (di wpfMdl_ClassUserControl.vb)

| Komponen | Format | Contoh |
|----------|--------|--------|
| **Variabel** | `usc_[NamaVarian]` | `usc_BukuPembelian_Lokal` |

## Contoh Varian yang Umum

### Berdasarkan Asal Transaksi

```vb
' Lokal vs Impor/Ekspor
wpfHost_BukuPembelian_Lokal
wpfHost_BukuPembelian_Impor

wpfHost_BukuPenjualan_Lokal
wpfHost_BukuPenjualan_Ekspor
```

### Berdasarkan Mata Uang

```vb
' Multi-currency
wpfHost_BukuPengawasanHutangUsaha              ' Default IDR
wpfHost_BukuPengawasanHutangUsaha_Impor_USD
wpfHost_BukuPengawasanHutangUsaha_Impor_EUR
wpfHost_BukuPengawasanHutangUsaha_Impor_JPY
wpfHost_BukuPengawasanHutangUsaha_Impor_CNY
wpfHost_BukuPengawasanHutangUsaha_Impor_SGD
wpfHost_BukuPengawasanHutangUsaha_Impor_AUD
wpfHost_BukuPengawasanHutangUsaha_Impor_GBP
```

### Berdasarkan Jenis Relasi

```vb
' Afiliasi vs Non-Afiliasi
wpfHost_BukuPengawasanHutangUsaha              ' Semua
wpfHost_BukuPengawasanHutangUsaha_Afiliasi
wpfHost_BukuPengawasanHutangUsaha_NonAfiliasi
```

### Berdasarkan Jenis Produk

```vb
' Jenis produk/transaksi
wpfHost_POPembelian_Lokal_Barang
wpfHost_POPembelian_Lokal_Jasa
wpfHost_POPembelian_Lokal_BarangDanJasa
wpfHost_POPembelian_Lokal_JasaKonstruksi
wpfHost_POPembelian_Lokal_Semua
```

### Berdasarkan Metode

```vb
' Dengan/Tanpa PO
wpfHost_InvoicePembelian_DenganPO_Lokal_Rutin
wpfHost_InvoicePembelian_DenganPO_Lokal_Termin
wpfHost_InvoicePembelian_TanpaPO_Lokal_Barang
wpfHost_InvoicePembelian_TanpaPO_Lokal_Jasa
```

## Deklarasi Variabel Global

### File: wpfMdl_ClassHost.vb

```vb
' =====================================================================
' Modul Deklarasi Variabel Host WPF
' Semua variabel host dideklarasikan secara Public di modul ini
' =====================================================================

Module wpfMdl_ClassHost

    ' =================================================================
    ' DATA MASTER
    ' =================================================================
    Public host_DataCOA As wpfHost_DataCOA
    Public host_DataLawanTransaksi As wpfHost_DataLawanTransaksi
    Public host_DataKaryawan As wpfHost_DataKaryawan

    ' =================================================================
    ' PEMBELIAN - BUKU
    ' =================================================================
    Public host_BukuPembelian_Lokal As wpfHost_BukuPembelian_Lokal
    Public host_BukuPembelian_Impor As wpfHost_BukuPembelian_Impor

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG USAHA
    ' =================================================================
    Public host_BukuPengawasanHutangUsaha As wpfHost_BukuPengawasanHutangUsaha
    Public host_BukuPengawasanHutangUsaha_Afiliasi As wpfHost_BukuPengawasanHutangUsaha_Afiliasi
    Public host_BukuPengawasanHutangUsaha_NonAfiliasi As wpfHost_BukuPengawasanHutangUsaha_NonAfiliasi
    Public host_BukuPengawasanHutangUsaha_Impor_USD As wpfHost_BukuPengawasanHutangUsaha_Impor_USD
    ' ... dst

End Module
```

### File: wpfMdl_ClassUserControl.vb

```vb
Module wpfMdl_ClassUserControl

    ' =================================================================
    ' DATA MASTER
    ' =================================================================
    Public usc_DataCOA As New wpfUsc_DataCOA

    ' =================================================================
    ' PEMBELIAN - BUKU
    ' =================================================================
    Public usc_BukuPembelian_Lokal As New wpfUsc_BukuPembelian
    Public usc_BukuPembelian_Impor As New wpfUsc_BukuPembelian

    ' =================================================================
    ' BUKU PENGAWASAN - HUTANG USAHA
    ' =================================================================
    Public usc_BukuPengawasanHutangUsaha As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_Afiliasi As New wpfUsc_BukuPengawasanHutangUsaha
    Public usc_BukuPengawasanHutangUsaha_NonAfiliasi As New wpfUsc_BukuPengawasanHutangUsaha
    ' ... dst (semua variabel usc_ yang berbeda mengarah ke UserControl yang sama)

End Module
```

**Catatan Penting:**
- Variabel `host_` dideklarasikan **tanpa** `As New` (akan diinstansiasi saat dipanggil)
- Variabel `usc_` dideklarasikan **dengan** `As New` untuk instance default

## Penggunaan Host di Menu Handler

### Pattern Standar

```vb
Private Sub mnu_BukuPembelian_Lokal_Click(sender As Object, e As RoutedEventArgs) _
    Handles mnu_BukuPembelian_Lokal.Click

    ' 1. Buat instance host baru
    host_BukuPembelian_Lokal = New wpfHost_BukuPembelian_Lokal

    ' 2. Buka dalam tab menggunakan helper function
    BukaUserControlDalamTab(usc_BukuPembelian_Lokal, host_BukuPembelian_Lokal.JudulForm)

End Sub
```

### Pattern dengan Pengecekan Tab Aktif

```vb
Private Sub mnu_BukuPembelian_Lokal_Click(sender As Object, e As RoutedEventArgs) _
    Handles mnu_BukuPembelian_Lokal.Click

    ' Cek apakah tab sudah terbuka
    If TabSudahTerbuka(host_BukuPembelian_Lokal.JudulForm) Then
        ' Aktifkan tab yang sudah ada
        AktifkanTab(host_BukuPembelian_Lokal.JudulForm)
    Else
        ' Buat instance baru dan buka tab
        host_BukuPembelian_Lokal = New wpfHost_BukuPembelian_Lokal
        BukaUserControlDalamTab(usc_BukuPembelian_Lokal, host_BukuPembelian_Lokal.JudulForm)
    End If

End Sub
```

## Anti-Pattern (JANGAN DILAKUKAN)

### SALAH: 1 file untuk banyak UserControl berbeda

```
❌ SALAH:
wpfHost_Pembelian.vb
├── wpfHost_POPembelian         → wpfUsc_POPembelian
├── wpfHost_SuratJalanPembelian → wpfUsc_SuratJalanPembelian
├── wpfHost_InvoicePembelian    → wpfUsc_InvoicePembelian
└── wpfHost_BukuPembelian       → wpfUsc_BukuPembelian
```

### BENAR: 1 file untuk 1 UserControl dengan banyak varian

```
✓ BENAR:
wpfHost_POPembelian.vb
├── wpfHost_POPembelian_Lokal_Barang    → wpfUsc_POPembelian
├── wpfHost_POPembelian_Lokal_Jasa      → wpfUsc_POPembelian
├── wpfHost_POPembelian_Impor_Barang    → wpfUsc_POPembelian
└── wpfHost_POPembelian_Impor_Jasa      → wpfUsc_POPembelian
```

## Lokasi File

| Komponen | Lokasi |
|----------|--------|
| File Host | Folder yang sama dengan UserControl |
| wpfMdl_ClassHost.vb | `/Booku/WPF/Modul Umum/` |
| wpfMdl_ClassUserControl.vb | `/Booku/WPF/Modul Umum/` |

### Contoh Struktur Folder

```
/Booku/Pembelian - Penjualan/Pembelian/1 - Buku Pembelian/
├── wpfUsc_BukuPembelian.xaml
├── wpfUsc_BukuPembelian.xaml.vb
└── wpfHost_BukuPembelian.vb          <- Host di folder yang sama
```

## Ringkasan Alur

```
Menu Click:
    └── host_XXX = New wpfHost_XXX()
        └── Sub New()
            ├── JudulForm = "..."
            ├── Inisialisasi()
            │   └── usc_XXX = New wpfUsc_XXX With { .Config = Value }
            └── Me.Content = usc_XXX
        └── BukaUserControlDalamTab(usc_XXX, host_XXX.JudulForm)

CekKesesuaianData (refresh):
    └── Inisialisasi()
        └── usc_XXX.RefreshTampilanData()
```

## Kapan Membuat Varian Host Baru

| Kondisi | Buat Varian Baru? | Contoh |
|---------|-------------------|--------|
| Filter data berbeda | Ya | Lokal vs Impor |
| Mata uang berbeda | Ya | IDR vs USD vs EUR |
| Jenis relasi berbeda | Ya | Afiliasi vs Non-Afiliasi |
| Jenis produk berbeda | Ya | Barang vs Jasa |
| Hanya tampilan berbeda | Tidak | Cukup property di UserControl |
| Hanya judul berbeda | Tidak | Cukup set JudulForm di Host |

## Best Practices

1. **Konsistensi Naming** - Gunakan pattern naming yang konsisten untuk semua varian
2. **Satu Tanggung Jawab** - Setiap Host hanya mengkonfigurasi satu kombinasi setting
3. **Deklarasi di Module** - Selalu deklarasikan variabel host di `wpfMdl_ClassHost.vb`
4. **Lokasi File** - Letakkan file host di folder yang sama dengan UserControl
5. **Dokumentasi** - Gunakan komentar section untuk memisahkan varian di file host
