# WPF Styling System

Lokasi: `/Booku/WPF/Styles/`

## Arsitektur Style

```
StyleAplikasi.xaml (Master/Induk)
    |-- StyleColor.xaml          <- WARNA TERPUSAT (WAJIB PERTAMA)
    |-- StyleWindow.xaml
    |-- StyleUserControl.xaml
    |-- StyleScrollViewer.xaml
    |-- StylePanel.xaml
    |-- StyleGrid.xaml
    |-- StyleGroupBox.xaml
    |-- StyleButton.xaml
    |-- StyleLabel.xaml
    |-- StyleTextBlock.xaml
    |-- StyleTextBox.xaml
    |-- StyleRichTextBox.xaml
    |-- StyleDatePicker.xaml
    |-- StyleCheckBox.xaml
    |-- StyleRadioButton.xaml
    |-- StyleComboBox.xaml
    |-- StyleDataGrid.xaml
    |-- StyleBorder.xaml
    |-- StyleLainnya.xaml
    |-- StyleMenu.xaml
    |-- StyleTabControl.xaml
    +-- StyleStatusBar.xaml
```

## Cara Kerja Resources

| Situasi | Sumber Resources | Keterangan |
|---------|------------------|------------|
| **Runtime** | `App.xaml` + `App.xaml.vb` constructor | Auto-load via XAML + manual load di constructor |
| **Design-time** | `App.xaml` + setiap file XAML | Untuk Visual Studio designer |

**Catatan Penting:**
- `App.xaml` di-include sebagai `ApplicationDefinition` di vbproj (entry point WPF standar)
- Resources di-load dua kali untuk memastikan tersedia: (1) dari XAML definition, (2) dari constructor `App.xaml.vb`
- Setiap file XAML (Window/UserControl) memiliki `Resources` sendiri yang me-reference `StyleAplikasi.xaml` untuk design-time support

**Prinsip:**
1. `StyleAplikasi.xaml` adalah **master file** yang merge semua style via `MergedDictionaries`
2. `StyleColor.xaml` **WAJIB** di-load pertama karena berisi definisi warna yang digunakan file style lain
3. Setiap **jenis elemen** memiliki **file style terpisah** untuk kemudahan organisasi
4. Window/UserControl me-reference `StyleAplikasi.xaml` via `Resources` untuk design-time support

---

## Sistem Manajemen Warna Terpusat

### Lokasi File
- **XAML:** `/Booku/WPF/Styles/StyleColor.xaml`
- **Code-Behind:** `/Booku Library/mdlPub_ModulUmum.vb`

### 2-Layer Color System

```
┌─────────────────────────────────────────────────────────────┐
│  LAYER 1: CORE COLORS (Palet Warna Dasar)                   │
│  ─────────────────────────────────────────                  │
│  Tipe: <Color>                                              │
│  Contoh: <Color x:Key="clrPrimary">#388E3C</Color>          │
│  Tujuan: Definisi warna mentah tanpa konteks penggunaan     │
│  Ubah di sini jika ingin mengganti tema aplikasi            │
└─────────────────────────────────────────────────────────────┘
                           │
                           ▼
┌─────────────────────────────────────────────────────────────┐
│  LAYER 2: SEMANTIC COLORS (Warna Semantik)                  │
│  ─────────────────────────────────────────                  │
│  Tipe: <SolidColorBrush>                                    │
│  Contoh: <SolidColorBrush x:Key="clrTeksPrimer"             │
│           Color="{StaticResource clrNeutral900}"/>          │
│  Tujuan: Warna berdasarkan konteks penggunaan               │
│  Gunakan warna ini di semua style komponen                  │
└─────────────────────────────────────────────────────────────┘
```

### Layer 1: Core Colors (Palet Warna Dasar)

#### Primary (Hijau BOOKU - Brand Color)

| Key | Hex | Penggunaan |
|-----|-----|------------|
| `clrPrimary` | `#388E3C` | Warna utama brand |
| `clrPrimaryLight` | `#4CAF50` | Varian terang |
| `clrPrimaryLighter` | `#81C784` | Varian lebih terang |
| `clrPrimaryDark` | `#2E7D32` | Hover state |
| `clrPrimaryDarker` | `#1B5E20` | Pressed state |
| `clrPrimarySurface` | `#E8F5E9` | Background surface |
| `clrPrimaryBorder` | `#A5D6A7` | Border hijau muda |

#### Neutral (Abu-abu)

| Key | Hex | Setara Material Design |
|-----|-----|------------------------|
| `clrNeutral50` | `#FAFAFA` | Grey 50 |
| `clrNeutral100` | `#F5F5F5` | Grey 100 |
| `clrNeutral200` | `#EEEEEE` | Grey 200 |
| `clrNeutral300` | `#E0E0E0` | Grey 300 |
| `clrNeutral400` | `#BDBDBD` | Grey 400 |
| `clrNeutral500` | `#9E9E9E` | Grey 500 |
| `clrNeutral600` | `#757575` | Grey 600 |
| `clrNeutral700` | `#616161` | Grey 700 |
| `clrNeutral800` | `#424242` | Grey 800 |
| `clrNeutral900` | `#212121` | Grey 900 |

#### Status Colors

| Kategori | Key | Hex |
|----------|-----|-----|
| **Success** | `clrSuccess` | `#4CAF50` |
| | `clrSuccessDark` | `#388E3C` |
| | `clrSuccessSurface` | `#E8F5E9` |
| **Warning** | `clrWarning` | `#FF9800` |
| | `clrWarningDark` | `#F57C00` |
| | `clrWarningSurface` | `#FFF3E0` |
| **Error** | `clrError` | `#F44336` |
| | `clrErrorDark` | `#D32F2F` |
| | `clrErrorSurface` | `#FFEBEE` |
| **Info** | `clrInfo` | `#2196F3` |
| | `clrInfoDark` | `#1976D2` |
| | `clrInfoSurface` | `#E3F2FD` |

#### Base Colors

| Key | Hex |
|-----|-----|
| `clrWhite` | `#FFFFFF` |
| `clrBlack` | `#000000` |

### Layer 2: Semantic Colors (Contoh Penggunaan)

#### Teks

| Key | Referensi | Penggunaan |
|-----|-----------|------------|
| `clrTeksPrimer` | `clrNeutral900` | Teks utama |
| `clrTeksSekunder` | `clrNeutral600` | Teks pendukung |
| `clrTeksDisabled` | `clrNeutral400` | Teks disabled |
| `clrTeksPlaceholder` | `clrNeutral500` | Placeholder |
| `clrTeksLink` | `clrPrimary` | Link/hyperlink |
| `clrTeksAksen` | `clrPrimary` | Teks aksen |
| `clrTeksInvers` | `clrWhite` | Teks di background gelap |

#### DataGrid

| Key | Referensi | Penggunaan |
|-----|-----------|------------|
| `clrDataGridBg` | `clrWhite` | Background grid |
| `clrDataGridBgAlt` | `clrNeutral50` | Alternating row |
| `clrDataGridHeader` | `clrNeutral100` | Header background |
| `clrDataGridHeaderFg` | `clrNeutral800` | Header text |
| `clrDataGridRowSelect` | `clrPrimary` | Row seleksi (hijau solid) |
| `clrDataGridRowSelectFg` | `clrWhite` | Teks row seleksi (putih) |
| `clrDataGridRowHover` | `clrNeutral100` | Row hover |
| `clrDataGridBorder` | `clrNeutral300` | Border grid |
| `clrDataGridGridLine` | `clrNeutral200` | Grid lines |

#### Tombol

| Kategori | Key | Referensi |
|----------|-----|-----------|
| **Sekunder** | `clrBtnBg` | `clrNeutral200` |
| | `clrBtnBgHover` | `clrNeutral300` |
| | `clrBtnBgPressed` | `clrNeutral400` |
| | `clrBtnFg` | `clrNeutral800` |
| **Primer** | `clrBtnPrimerBg` | `clrPrimary` |
| | `clrBtnPrimerBgHover` | `clrPrimaryDark` |
| | `clrBtnPrimerBgPressed` | `clrPrimaryDarker` |
| | `clrBtnPrimerFg` | `clrWhite` |

---

## Variabel Warna di Code-Behind

Lokasi: `/Booku Library/mdlPub_ModulUmum.vb`

### Warna Utama

| Variabel | Hex | Setara XAML |
|----------|-----|-------------|
| `WarnaHijauSolid_WPF` | `#388E3C` | `clrPrimary` |
| `WarnaHijauTerang_WPF` | `#E8F5E9` | `clrPrimarySurface` |
| `WarnaMerahSolid_WPF` | `#F44336` | `clrError` |
| `WarnaMerahTerang_WPF` | `#FFEBEE` | `clrErrorSurface` |
| `WarnaBiruSolid_WPF` | `#2196F3` | `clrInfo` |
| `WarnaBiruTerang_WPF` | `#E3F2FD` | `clrInfoSurface` |
| `WarnaKuningSolid_WPF` | `#FF9800` | `clrWarning` |
| `WarnaKuningTerang_WPF` | `#FFF3E0` | `clrWarningSurface` |
| `WarnaPutih_WPF` | `#FFFFFF` | `clrWhite` |

### Warna Teks

| Variabel | Hex | Setara XAML |
|----------|-----|-------------|
| `WarnaTeksStandar_WPF` | `#212121` | `clrTeksPrimer` |
| `WarnaTeksSekunder_WPF` | `#757575` | `clrTeksSekunder` |
| `WarnaTeksDisabled_WPF` | `#BDBDBD` | `clrTeksDisabled` |
| `WarnaPudar_WPF` | `#9E9E9E` | `clrNeutral500` |

### Warna Neutral (Skala Abu-abu)

| Variabel | Hex | Setara XAML |
|----------|-----|-------------|
| `WarnaHitam_5_WPF` | `#FAFAFA` | `clrNeutral50` |
| `WarnaHitam_10_WPF` | `#F5F5F5` | `clrNeutral100` |
| `WarnaHitam_15_WPF` | `#EEEEEE` | `clrNeutral200` |
| `WarnaHitam_20_WPF` | `#E0E0E0` | `clrNeutral300` |
| `WarnaHitam_25_WPF` | `#BDBDBD` | `clrNeutral400` |
| `WarnaHitam_30_WPF` | `#9E9E9E` | `clrNeutral500` |
| `WarnaHitam_35_WPF` | `#757575` | `clrNeutral600` |
| `WarnaHitam_40_WPF` | `#616161` | `clrNeutral700` |
| `WarnaHitam_45_WPF` | `#424242` | `clrNeutral800` |
| `WarnaHitam_50_WPF` | `#212121` | `clrNeutral900` |

---

## Cara Menggunakan Warna

### Di XAML (Gunakan Semantic Colors)

```xml
<!-- BENAR: Gunakan semantic color -->
<TextBlock Foreground="{StaticResource clrTeksPrimer}" Text="Hello"/>
<Border Background="{StaticResource clrBgPanel}"/>
<Button Background="{StaticResource clrBtnPrimerBg}"/>

<!-- SALAH: Jangan hardcode warna -->
<TextBlock Foreground="#212121" Text="Hello"/>
<Border Background="White"/>
```

### Di Code-Behind (VB.NET)

```vb
' BENAR: Gunakan variabel warna dari mdlPub_ModulUmum
label.Foreground = WarnaTeksStandar_WPF
panel.Background = WarnaPutih_WPF
row.Background = WarnaHijauSolid_WPF

' SALAH: Jangan hardcode warna
label.Foreground = New SolidColorBrush(Color.FromRgb(33, 33, 33))
panel.Background = Brushes.White
```

---

## Cara Membuat Style Baru

1. **Buat file baru** `Style[NamaElemen].xaml` di folder `/Booku/WPF/Styles/`
2. **Gunakan warna dari StyleColor.xaml** (jangan hardcode)
3. **Daftarkan** di `StyleAplikasi.xaml`:
   ```xml
   <ResourceDictionary Source="/WPF/Styles/Style[NamaElemen].xaml" />
   ```
4. Style otomatis tersedia di semua Window/UserControl yang me-reference `StyleAplikasi.xaml`

## Pattern Penamaan Style

Format: `style[NamaElemen][Varian]`

```
styleButtonDasar              <- Base style
styleButtonFormHalaman        <- Varian untuk form halaman (BasedOn Dasar)
styleButtonFormHalamanCrud    <- Varian lebih spesifik (BasedOn FormHalaman)
styleButtonFormHalamanCrudTambah  <- Varian paling spesifik
```

**Contoh inheritance:**
```xml
<!-- Base style -->
<Style x:Key="styleMenuItemDasar" TargetType="MenuItem">
    <Setter Property="FontSize" Value="12"/>
    <Setter Property="Foreground" Value="{StaticResource clrTeksPrimer}"/>
</Style>

<!-- Varian dengan BasedOn -->
<Style x:Key="styleMenuItemTopLevel" BasedOn="{StaticResource styleMenuItemDasar}" TargetType="MenuItem">
    <Setter Property="Padding" Value="12,8"/>
</Style>
```

## Struktur Internal File Style

```xml
<ResourceDictionary xmlns="..."  xmlns:x="...">

    <!-- JANGAN definisikan warna di sini! -->
    <!-- Gunakan referensi dari StyleColor.xaml -->

    <!-- Style dengan referensi ke warna semantic -->
    <Style x:Key="styleMenuDasar" TargetType="Menu">
        <Setter Property="Background" Value="{StaticResource clrMenuBg}"/>
        <Setter Property="Foreground" Value="{StaticResource clrMenuFg}"/>
    </Style>

    <!-- Varian dengan BasedOn -->
    <Style x:Key="styleMenuBarAplikasi" BasedOn="{StaticResource styleMenuDasar}" TargetType="Menu">
        <Setter Property="Height" Value="Auto"/>
    </Style>

</ResourceDictionary>
```

## Daftar File Style

| File Style | Komponen |
|------------|----------|
| `StyleColor.xaml` | **Definisi warna terpusat** |
| `StyleWindow.xaml` | Window |
| `StyleUserControl.xaml` | UserControl |
| `StyleScrollViewer.xaml` | ScrollViewer |
| `StylePanel.xaml` | StackPanel, DockPanel, WrapPanel |
| `StyleGrid.xaml` | Grid |
| `StyleGroupBox.xaml` | GroupBox |
| `StyleButton.xaml` | Button (Form Halaman, Form Dialog, CRUD) |
| `StyleLabel.xaml` | Label |
| `StyleTextBlock.xaml` | TextBlock |
| `StyleTextBox.xaml` | TextBox |
| `StyleRichTextBox.xaml` | RichTextBox |
| `StyleDatePicker.xaml` | DatePicker |
| `StyleCheckBox.xaml` | CheckBox |
| `StyleRadioButton.xaml` | RadioButton |
| `StyleComboBox.xaml` | ComboBox |
| `StyleDataGrid.xaml` | DataGrid |
| `StyleBorder.xaml` | Border |
| `StyleMenu.xaml` | Menu, MenuItem |
| `StyleTabControl.xaml` | TabControl, TabItem |
| `StyleStatusBar.xaml` | StatusBar |
| `StyleLainnya.xaml` | Separator, dll |

---

## Aturan Penting

### DO (Lakukan)

1. **Gunakan semantic colors** dari StyleColor.xaml
2. **Gunakan variabel warna** dari mdlPub_ModulUmum.vb di code-behind
3. **Tambahkan komentar** yang mereferensikan key XAML saat mendefinisikan warna di code-behind
4. **Konsisten** antara warna XAML dan code-behind

### DON'T (Jangan Lakukan)

1. **Jangan hardcode warna** di file style manapun
2. **Jangan definisikan warna baru** di file style selain StyleColor.xaml
3. **Jangan gunakan `Brushes.Red`** atau sejenisnya - gunakan variabel
4. **Jangan buat warna duplikat** - gunakan yang sudah ada
