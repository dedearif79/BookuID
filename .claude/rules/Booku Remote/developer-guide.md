# Developer Guide

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## Mode Developer

Sistem deteksi otomatis yang membedakan antara Development mode dan Production mode berdasarkan path eksekusi.

### Cara Kerja

```vb
Public ReadOnly Property ModeDeveloper As Boolean
    Get
        Dim baseDir = AppDomain.CurrentDomain.BaseDirectory
        ' Cek apakah path mengandung "\Debug\" (case-insensitive)
        Return baseDir.IndexOf("\Debug\", StringComparison.OrdinalIgnoreCase) >= 0
    End Get
End Property
```

### Perbedaan Mode

| Aspek | Mode Developer (Debug) | Mode Production (Release) |
|-------|------------------------|---------------------------|
| **Path Eksekusi** | Mengandung `\Debug\` | Tidak mengandung `\Debug\` |
| **Single Instance** | Dinonaktifkan (multi-instance OK) | Aktif (Mutex protection) |
| **File Logging** | Aktif (folder `Logs/` dibuat) | Dinonaktifkan (tidak ada file log) |
| **Local Test Mode** | Checkbox tampil di Host | Checkbox tersembunyi |
| **Debug.WriteLine** | Output ke Visual Studio | Tetap aktif tapi tidak terlihat |

### Implementasi

| File | Perubahan |
|------|-----------|
| `mdl_VariabelUmum.vb` | Property `ModeDeveloper`, kondisional di `InitLogFile()` dan `WriteLog()` |
| `Application.xaml.vb` | Mutex hanya aktif jika `Not ModeDeveloper` |
| `wpfWin_ModeHost.xaml.vb` | Local Test Mode checkbox visibility berdasarkan `ModeDeveloper` |

---

## Debugging Notes

> **Prasyarat:** File log hanya tersedia jika aplikasi dijalankan dalam **Mode Developer** (dari folder Debug).

### Cara Debugging dengan File Log

1. **Jalankan aplikasi dari folder Debug** - Log file akan dibuat otomatis di `bin/Debug/net8.0-windows/Logs/`
2. **Setelah test** - Baca file log dengan nama format `BookuRemote_Host_*.log` atau `BookuRemote_Tamu_*.log`
3. **Cari error** - Gunakan `grep` atau search untuk mencari pattern seperti `[ERROR]`, `Exception`, atau komponen spesifik seperti `[H264-DEC]`

### Tag Log yang Digunakan

| Tag | Komponen |
|-----|----------|
| `[UDP-HOST]` | UDP streaming di Host |
| `[UDP-TAMU]` | UDP streaming di Tamu |
| `[H264-ENC]` | H.264 encoder (Host) |
| `[H264-DEC]` | H.264 decoder (Tamu) |
| `[H264-DIAG]` | Diagnostic pixel data |
| `[VIEWER]` | Frame rendering di Viewer |
| `[CURSOR]` | Cursor drawing pada screenshot |
| `[FFMPEG-ENC-ERR]` | FFmpeg encoder stderr |
| `[FFMPEG-DEC-ERR]` | FFmpeg decoder stderr |
| `[LOCAL-TEST]` | Input yang tidak di-inject karena Local Test Mode aktif |
| `[LOG]` | Status sistem logging |

### Contoh Log H.264 yang Berhasil

**Host Log:**
```
[H264-HOST] Encoder started: 1152x648 @ 15fps
[H264-DATA] FIRST IDR frame with SPS+PPS prepended: 281 bytes
[H264-DATA] IDR frame (no SPS+PPS): 248 bytes
[H264-DATA] Non-IDR frame: 12 bytes (type=1)
```

**Tamu Log:**
```
[UDP-TAMU] H.264 detected, enabled ordered delivery mode
[H264-DEC] Decoder STARTED with auto-resolution detection
[FFMPEG-DEC-ERR] Stream #0:0: Video: rawvideo (BGRA), bgra, 1152x648
[H264-DEC] Frame #1 decoded: 1152x648, BGRA size=2985984
[H264] New WriteableBitmap: 1152x648, BackBufferStride=4608
[VIEWER] First frame rendered, loading overlay hidden
```

> **Catatan:** BGRA size = Width × Height × 4 = 1152 × 648 × 4 = 2,985,984 bytes

---

## Aturan Pengembangan

1. **Namespace**: Semua kode harus dalam namespace `Booku_Remote`
2. **Prefix**: Gunakan prefix standar (`wpfWin_`, `mdl_`, `cls_`)
3. **Async/Await**: Gunakan async untuk operasi jaringan
4. **Error Handling**: Tangani exception network dengan baik
5. **UI Thread**: Gunakan `Dispatcher.Invoke` untuk update UI dari async task
6. **Resource Cleanup**: Dispose TCP connections dan streams dengan benar
7. **Logging**: Gunakan `WriteLog()` untuk logging, bukan `Console.WriteLine` atau `Debug.WriteLine` langsung. Gunakan tag yang konsisten (lihat Debugging Notes)

---

## Status Pengembangan

| Komponen | Status | Catatan |
|----------|--------|---------|
| Discovery UDP | Selesai | Broadcast + Response |
| Koneksi TCP (LAN) | Selesai | Handshake + Heartbeat |
| Koneksi Relay (Internet) | Selesai | Via VPS 155.117.43.209:45680 |
| Screen Streaming (TCP) | Selesai | JPEG compression, fallback mode |
| **Screen Streaming (UDP/JPEG) LAN** | **Selesai** | **Port 45681, chunking/reassembly, low-latency** |
| **Screen Streaming (UDP/JPEG) Relay** | **Selesai** | **Via relay server, fire-and-forget receiver pattern** |
| **Screen Streaming (UDP/H.264) Host** | **Selesai** | **FFmpeg encoder, NAL parsing, codec routing** |
| **Screen Streaming (UDP/H.264) Tamu WPF** | **Selesai** | **FFmpeg decoder, BGRA render berfungsi** |
| Keyboard Control | Selesai | SendInput API |
| Mouse Control | Selesai | Move, Click, Wheel |
| **Cursor Drawing** | **Selesai** | **Menggambar cursor Host pada screenshot via Windows API** |
| **Port Settings** | Selesai | Configurable via UI, JSON persistence |
| **FPS Settings** | **Selesai** | **Slider 5-30 FPS di Host, default 15** |
| **File Logging System** | **Selesai** | **WriteLog() ke folder Logs/, auto-cleanup 7 hari, kondisional Mode Developer** |
| **Mode Developer** | **Selesai** | **Deteksi otomatis berdasarkan path eksekusi (Debug vs Release)** |
| **Local Test Mode** | **Selesai** | **Testing di 1 PC tanpa input injection, hanya tampil di Mode Developer** |
| **Single Instance (Kondisional)** | **Selesai** | **Mutex hanya aktif di Release mode** |
| **Clipboard Sync** | **Selesai** | **Bidirectional auto-sync teks/gambar, loop prevention** |
| **File Transfer (LAN)** | **Selesai** | **Bidirectional, chunking, progress, cancel** |
| **File Transfer (Internet)** | **Selesai** | **Via Relay Server, chunking, progress** |
