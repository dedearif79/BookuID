# UI Windows

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## wpfWin_StartUp

Menu utama untuk memilih mode aplikasi.

| Komponen | Fungsi |
|----------|--------|
| `btn_ModeHost` | Aktifkan mode Host (dikontrol) |
| `btn_ModeTamu` | Aktifkan mode Tamu (mengontrol) |

## wpfWin_ModeHost

Window Host yang menunggu koneksi (LAN atau Internet).

| Komponen | Fungsi |
|----------|--------|
| `btn_ModeLAN` / `btn_ModeInternet` | Switch mode koneksi |
| `lbl_StatusKoneksi` | Status koneksi saat ini |
| `lbl_AlamatIP` | Alamat IP Host (mode LAN) |
| `lbl_HostCode` | HostCode untuk mode Internet |
| `txt_Password` | Password opsional untuk koneksi |
| `btn_Hentikan` | Hentikan mode Host |
| **Expander "Pengaturan Port"** | Konfigurasi port Discovery, Koneksi, Relay, UDP Video, dan Relay Server IP |
| **Expander "Pengaturan Streaming"** | **Slider untuk Target FPS (5-30, default 15)** |
| **Local Test Mode** | Checkbox untuk testing di 1 PC — **hanya tampil di Mode Developer** |

### Local Test Mode

- Hanya visible jika `ModeDeveloper = True` (aplikasi dijalankan dari folder Debug)
- Saat diaktifkan, input dari Tamu **tidak di-inject ke Windows** (hanya di-log)
- Berguna untuk testing Host + Tamu di satu PC tanpa input saling interferensi
- Di Release mode, checkbox ini tersembunyi dan `LocalTestMode` selalu `False`

## wpfWin_ModeTamu

Window Tamu untuk scan dan connect ke Host (LAN atau Internet).

| Komponen | Fungsi |
|----------|--------|
| `btn_ModeLAN` / `btn_ModeInternet` | Switch mode koneksi |
| `datagridUtama` | Daftar perangkat Host (mode LAN) |
| `btn_Scan` | Scan ulang perangkat di LAN |
| `txt_HostCode` | Input HostCode (mode Internet) |
| `lbl_HostInfo` | Info Host dari query (mode Internet) |
| `btn_Sambungkan` | Connect ke Host terpilih |
| **Expander "Pengaturan Lanjutan"** | Konfigurasi port Discovery, Relay Server IP, dan port Relay |

## wpfWin_PersetujuanKoneksi

Dialog yang muncul di Host saat ada permintaan koneksi.

| Komponen | Fungsi |
|----------|--------|
| `lbl_NamaTamu` | Nama perangkat Tamu |
| `chk_IzinKontrol` | Izinkan kontrol keyboard/mouse |
| `btn_Terima` | Terima koneksi |
| `btn_Tolak` | Tolak koneksi |

## wpfWin_Viewer

Window Viewer di Tamu untuk melihat dan mengontrol layar Host.

| Komponen | Fungsi |
|----------|--------|
| `img_Layar` | Image control untuk menampilkan frame |
| `tgl_Kontrol` | Toggle aktifkan kontrol keyboard/mouse |
| `btn_Fullscreen` | Mode fullscreen |
| `btn_Putuskan` | Putuskan koneksi |
| `lbl_FPS` | Info FPS streaming |
| `lbl_Latency` | Info latency |

## wpfWin_FileBrowser (Fase 3b)

Dialog file browser untuk transfer berkas.

## wpfWin_TransferProgress (Fase 3b)

Dialog progress untuk transfer berkas.

## Keyboard Shortcut (Viewer)

| Shortcut | Fungsi |
|----------|--------|
| **Ctrl+K** | Toggle kontrol ON/OFF |
| **F11** | Fullscreen (jika diimplementasi) |
