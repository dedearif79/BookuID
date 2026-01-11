# WPF Styling System

Lokasi: `/Booku/WPF/Styles/`

## Arsitektur Style

```
StyleAplikasi.xaml (Master/Induk)
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
| **Runtime** | `mdlWpf_Program.Main()` | Dimuat ke `app.Resources.MergedDictionaries` |
| **Design-time** | `Application.xaml` + setiap file XAML | Untuk Visual Studio designer |

**Catatan Penting:**
- `Application.xaml` diinclude sebagai `Page` (bukan `ApplicationDefinition`) di vbproj untuk menghindari konflik `Sub Main`
- Setiap file XAML (Window/UserControl) memiliki `Resources` sendiri yang me-reference `StyleAplikasi.xaml` untuk design-time support
- Ini adalah **trade-off** karena menggunakan custom entry point (`mdlWpf_Program.Main`)

**Prinsip:**
1. `StyleAplikasi.xaml` adalah **master file** yang merge semua style via `MergedDictionaries`
2. Setiap **jenis elemen** memiliki **file style terpisah** untuk kemudahan organisasi
3. Window/UserControl me-reference `StyleAplikasi.xaml` via `Resources` untuk design-time support

## Cara Membuat Style Baru

1. **Buat file baru** `Style[NamaElemen].xaml` di folder `/Booku/WPF/Styles/`
2. **Daftarkan** di `StyleAplikasi.xaml`:
   ```xml
   <ResourceDictionary Source="/WPF/Styles/Style[NamaElemen].xaml" />
   ```
3. Style otomatis tersedia di semua Window/UserControl yang me-reference `StyleAplikasi.xaml`

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
</Style>

<!-- Varian dengan BasedOn -->
<Style x:Key="styleMenuItemTopLevel" BasedOn="{StaticResource styleMenuItemDasar}" TargetType="MenuItem">
    <Setter Property="Padding" Value="12,8"/>
</Style>
```

## Struktur Internal File Style

```xml
<ResourceDictionary xmlns="..."  xmlns:x="...">

    <!-- 1. WARNA TEMA (sebagai SolidColorBrush) -->
    <SolidColorBrush x:Key="MenuBarBackground" Color="#388E3C"/>
    <SolidColorBrush x:Key="MenuBarForeground" Color="White"/>

    <!-- 2. STYLE DASAR -->
    <Style x:Key="styleMenuDasar" TargetType="Menu">
        <Setter Property="Background" Value="{StaticResource MenuBarBackground}"/>
    </Style>

    <!-- 3. STYLE VARIAN (dengan BasedOn) -->
    <Style x:Key="styleMenuBarAplikasi" BasedOn="{StaticResource styleMenuDasar}" TargetType="Menu">
        <Setter Property="Height" Value="Auto"/>
    </Style>

</ResourceDictionary>
```

## Daftar File Style

| File Style | Komponen |
|------------|----------|
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

## Palet Warna BOOKU (Green Branding)

Warna utama BOOKU adalah **hijau** sesuai logo. Gunakan palet berikut untuk konsistensi UI:

| Kode Warna | Nama | Penggunaan |
|------------|------|------------|
| `#388E3C` | **Primary** | Menu bar, teks utama, aksen, header |
| `#2E7D32` | **Primary Dark** | Hover state pada menu |
| `#1B5E20` | **Primary Darker** | Pressed state, warna paling gelap |
| `#4CAF50` | **Primary Light** | Alternatif warna terang |
| `#E8F5E9` | **Surface Light** | Background status bar, hover sub-menu |
| `#C8E6C9` | **Border Light** | Border tab control |
| `#A5D6A7` | **Border/Separator** | Garis pemisah, border dropdown |

**Aturan Penggunaan:**
- Header/Judul penting: `#388E3C`
- Background highlight: `#E8F5E9`
- Border aktif: `#388E3C`
- Border pasif: `#A5D6A7`
- Teks biasa: `#333333`
- Teks sekunder: `#757575`
