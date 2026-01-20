# Panduan Testing Fase 2b: Kontrol Keyboard dan Mouse

> **Project:** Booku Remote
> **Fase:** 2b - Kontrol Keyboard dan Mouse
> **Tanggal:** 2026-01-20

---

## Persiapan

### Kebutuhan Hardware

| Item | Keterangan |
|------|------------|
| **2 PC** | Terhubung dalam jaringan LAN yang sama |
| **PC Host** | Komputer yang akan dikontrol |
| **PC Tamu** | Komputer yang akan mengontrol |

### Persiapan Software

```
PC Host:
├── Jalankan aplikasi Booku Remote
├── Klik "Mode Host"
└── Pastikan status "Menunggu koneksi..."

PC Tamu:
├── Jalankan aplikasi Booku Remote
├── Klik "Mode Tamu"
├── Scan atau masukkan IP Host
└── Klik "Sambungkan"
```

---

## Prosedur Testing

### Tahap 1: Establish Connection

| Langkah | Aksi | Expected Result |
|---------|------|-----------------|
| 1.1 | Di Host, aktifkan mode Host | Status: "Menunggu koneksi..." |
| 1.2 | Di Tamu, scan perangkat LAN | Host muncul di daftar |
| 1.3 | Klik sambungkan ke Host | Dialog persetujuan muncul di Host |
| 1.4 | Di Host, klik "Terima" dengan izin kontrol ✓ | Koneksi established |
| 1.5 | Window Viewer terbuka di Tamu | Layar Host tampil di Viewer |

### Tahap 2: Aktifkan Kontrol

| Langkah | Aksi | Expected Result |
|---------|------|-----------------|
| 2.1 | Cek toggle kontrol | Toggle "🎮" enabled (tidak disabled) |
| 2.2 | Klik toggle kontrol **ATAU** tekan **Ctrl+K** | Toggle aktif (highlighted) |
| 2.3 | Cek status bar | Text berubah: "Kontrol: ON" |
| 2.4 | Klik di area Image | Image mendapat focus |

---

## Test Cases

### TC-01: Mouse Move

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Gerakkan mouse ke tengah Image | Cursor Host bergerak ke tengah layar |
| 2 | Gerakkan mouse ke pojok kiri atas | Cursor Host ke pojok kiri atas |
| 3 | Gerakkan mouse ke pojok kanan bawah | Cursor Host ke pojok kanan bawah |
| 4 | Gerakkan mouse secara melingkar | Cursor Host mengikuti gerakan |

**Catatan:** Ada throttle 30ms untuk mouse move, jadi gerakan sangat cepat mungkin sedikit tertunda.

---

### TC-02: Mouse Click (Left)

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Buka Notepad di Host (manual) | Notepad terbuka |
| 2 | Arahkan mouse ke area Notepad di Viewer | Cursor Host di atas Notepad |
| 3 | Klik kiri sekali | Notepad mendapat focus |
| 4 | Klik kiri di title bar, tahan, geser | Window Notepad bergerak (drag) |

---

### TC-03: Mouse Click (Right)

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Arahkan mouse ke Desktop di Viewer | Cursor di Desktop Host |
| 2 | Klik kanan | Context menu muncul di Host |
| 3 | Gerakkan mouse ke menu item | Item ter-highlight |
| 4 | Klik kiri untuk memilih | Menu item dieksekusi |

---

### TC-04: Mouse Click (Middle)

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Buka browser di Host, buka halaman dengan link | Browser terbuka |
| 2 | Arahkan mouse ke link di Viewer | Cursor di atas link |
| 3 | Klik tengah (middle click) | Link terbuka di tab baru |

---

### TC-05: Mouse Wheel (Scroll)

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Buka halaman web panjang di browser Host | Halaman dengan scroll |
| 2 | Arahkan mouse ke area konten di Viewer | Cursor di area scrollable |
| 3 | Scroll wheel ke bawah | Halaman scroll ke bawah |
| 4 | Scroll wheel ke atas | Halaman scroll ke atas |
| 5 | Scroll cepat beberapa notch | Scroll smooth mengikuti |

---

### TC-06: Keyboard - Huruf & Angka

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Buka Notepad di Host | Notepad terbuka |
| 2 | Klik di area Notepad via Viewer | Notepad focus, cursor berkedip |
| 3 | Ketik "Hello World 123" | Text muncul di Notepad |
| 4 | Ketik huruf kapital "ABC" (Shift+a,b,c) | "ABC" muncul |

---

### TC-07: Keyboard - Special Keys

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Dengan Notepad terbuka dan ada teks | - |
| 2 | Tekan **Enter** | New line di Notepad |
| 3 | Tekan **Tab** | Tab character |
| 4 | Tekan **Backspace** | Hapus karakter sebelumnya |
| 5 | Tekan **Delete** | Hapus karakter setelah cursor |
| 6 | Tekan **Space** | Spasi |

---

### TC-08: Keyboard - Arrow Keys (Extended)

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Dengan Notepad berisi teks, cursor di tengah | - |
| 2 | Tekan **Arrow Left** (←) | Cursor geser kiri |
| 3 | Tekan **Arrow Right** (→) | Cursor geser kanan |
| 4 | Tekan **Arrow Up** (↑) | Cursor naik satu baris |
| 5 | Tekan **Arrow Down** (↓) | Cursor turun satu baris |
| 6 | Tekan **Home** | Cursor ke awal baris |
| 7 | Tekan **End** | Cursor ke akhir baris |
| 8 | Tekan **Page Up** | Scroll satu halaman ke atas |
| 9 | Tekan **Page Down** | Scroll satu halaman ke bawah |

---

### TC-09: Keyboard - Modifier Keys

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Dengan Notepad berisi "Hello World" | - |
| 2 | **Ctrl+A** | Select all text |
| 3 | **Ctrl+C** | Copy text |
| 4 | Klik di posisi baru | Cursor pindah |
| 5 | **Ctrl+V** | Paste text |
| 6 | **Ctrl+Z** | Undo |
| 7 | **Ctrl+S** | Save dialog muncul |
| 8 | **Alt+F4** | Notepad mencoba close |

---

### TC-10: Keyboard - Shift Selection

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Dengan Notepad berisi teks | - |
| 2 | **Shift + Arrow Right** (beberapa kali) | Text ter-select ke kanan |
| 3 | **Shift + Arrow Left** | Selection berkurang |
| 4 | **Shift + End** | Select sampai akhir baris |
| 5 | **Shift + Home** | Select sampai awal baris |
| 6 | **Ctrl + Shift + End** | Select sampai akhir dokumen |

---

### TC-11: Toggle Kontrol ON/OFF

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Kontrol dalam keadaan ON | - |
| 2 | Tekan **Ctrl+K** | Kontrol OFF, status "Kontrol: OFF" |
| 3 | Gerakkan mouse di Viewer | **Tidak ada gerakan** di Host |
| 4 | Ketik keyboard di Viewer | **Tidak ada input** di Host |
| 5 | Tekan **Ctrl+K** lagi | Kontrol ON kembali |
| 6 | Gerakkan mouse | Mouse Host bergerak lagi |

---

### TC-12: Double Click

| Step | Aksi di Tamu | Expected di Host |
|------|--------------|------------------|
| 1 | Di Desktop Host, ada file/folder | - |
| 2 | Arahkan mouse ke icon file di Viewer | Cursor di atas icon |
| 3 | Double-click cepat | File/folder terbuka di Host |

---

## Troubleshooting

### Problem: Mouse Tidak Bergerak

| Kemungkinan | Solusi |
|-------------|--------|
| Kontrol tidak aktif | Tekan Ctrl+K atau klik toggle |
| Focus tidak di Image | Klik di area Image Viewer |
| Izin kontrol tidak diberikan | Reconnect, pastikan Host centang "Izin Kontrol" |
| Koneksi terputus | Cek status koneksi, reconnect jika perlu |

### Problem: Keyboard Tidak Bekerja

| Kemungkinan | Solusi |
|-------------|--------|
| Focus di control lain | Klik di area Image untuk focus |
| Kontrol tidak aktif | Aktifkan dengan Ctrl+K |
| Extended key tidak terdeteksi | Cek IsExtendedKey() implementation |

### Problem: Gerakan Mouse Patah-patah

| Kemungkinan | Solusi |
|-------------|--------|
| Network latency tinggi | Cek koneksi jaringan |
| Throttle terlalu tinggi | Kurangi MOUSE_MOVE_THROTTLE_MS |
| CPU Host tinggi | Tutup aplikasi berat di Host |

### Problem: Klik Tidak Tepat Posisi

| Kemungkinan | Solusi |
|-------------|--------|
| Scaling issue | Cek normalisasi koordinat di NormalizeX/Y |
| Resolusi berbeda | Koordinat harus normalized (0.0-1.0) |
| Aspect ratio berbeda | Pertimbangkan letterboxing |

---

## Checklist Testing

```
[ ] TC-01: Mouse Move
[ ] TC-02: Mouse Click Left
[ ] TC-03: Mouse Click Right
[ ] TC-04: Mouse Click Middle
[ ] TC-05: Mouse Wheel Scroll
[ ] TC-06: Keyboard Huruf & Angka
[ ] TC-07: Keyboard Special Keys
[ ] TC-08: Keyboard Arrow Keys
[ ] TC-09: Keyboard Modifier Keys
[ ] TC-10: Keyboard Shift Selection
[ ] TC-11: Toggle Kontrol ON/OFF
[ ] TC-12: Double Click
```

---

## Log Testing

| Tanggal | Tester | TC Pass | TC Fail | Notes |
|---------|--------|---------|---------|-------|
| | | | | |
| | | | | |
| | | | | |

---

## Catatan Teknis

### Arsitektur Fase 2b

```
┌─────────────────────────────────────────────────────────────────┐
│  TAMU (Remote Controller)                                       │
│  ┌─────────────────┐     ┌─────────────────┐                    │
│  │ wpfWin_Viewer   │ ──► │ Capture Events  │                    │
│  │ (KeyDown/Mouse) │     │ (WPF Events)    │                    │
│  └─────────────────┘     └────────┬────────┘                    │
│                                   │                              │
│                          KirimPaketAsync()                       │
│                        (INPUT_KEYBOARD/MOUSE)                    │
└───────────────────────────────────┼─────────────────────────────┘
                                    │ TCP Port 45679
                                    ▼
┌───────────────────────────────────┼─────────────────────────────┐
│  HOST (Receives Input)            │                              │
│                          PaketDiterima event                     │
│                                   │                              │
│  ┌─────────────────┐     ┌───────┴─────────┐                    │
│  │ mdl_InjeksiInput│ ◄── │ DeserializeInput│                    │
│  │ (SendInput API) │     │ (JSON→Object)   │                    │
│  └─────────────────┘     └─────────────────┘                    │
└─────────────────────────────────────────────────────────────────┘
```

### File yang Terlibat

| File | Fungsi |
|------|--------|
| `mdl_InjeksiInput.vb` | Windows SendInput API wrapper |
| `mdl_Protokol.vb` | Serialization paket input |
| `cls_PaketData.vb` | Payload classes untuk input |
| `mdl_VariabelUmum.vb` | Enum TipePaket, TipeAksiMouse |
| `wpfWin_Viewer.xaml` | UI toggle kontrol |
| `wpfWin_Viewer.xaml.vb` | Event handlers & send logic |
| `mdl_KoneksiJaringan.vb` | Handle paket di Host |

### Keyboard Shortcut

| Shortcut | Fungsi |
|----------|--------|
| **Ctrl+K** | Toggle kontrol ON/OFF |
| **F11** | Fullscreen (jika diimplementasi) |

---

## Riwayat Dokumen

| Versi | Tanggal | Perubahan |
|-------|---------|-----------|
| 1.0 | 2026-01-20 | Initial version |
