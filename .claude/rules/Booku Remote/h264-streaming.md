# H.264 Streaming

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## H.264 Encoding Components (Host)

| Komponen | File | Deskripsi |
|----------|------|-----------|
| **FFmpeg Manager** | `mdl_FFmpegManager.vb` | Deteksi FFmpeg, lifecycle management |
| **H.264 Encoder** | `cls_H264Encoder.vb` | Wrapper FFmpeg via stdin/stdout pipe |
| **NAL Parser** | `cls_NalParser.vb` | Parse NAL units dari H.264 stream |

### Alur H.264 Encoding

```
Screen Capture → BitmapKeBgra() → FFmpeg stdin → H.264 stream → NAL Parser → UDP Chunker → Send
```

### Screen Capture (`mdl_TangkapLayar.vb`)

- `TangkapLayarDenganSkala(skala)` - Capture layar dengan skala (default 0.6 = 60%)
- `BitmapKeBgra(bitmap)` - Ekstrak raw BGRA pixel data untuk FFmpeg

### Catatan Penting BitmapKeBgra()

- Gunakan copy langsung dari `bmpData.Scan0` ke byte array
- **JANGAN** tambahkan stride padding handling row-by-row
- **JANGAN** force alpha channel ke 255
- Windows GDI sudah menghasilkan data BGRA yang benar
- Format: `LockBits` → `Marshal.Copy` langsung → `Return`

### FFmpeg Settings (Low-Latency)

```bash
ffmpeg -f rawvideo -pix_fmt bgra -s [W]x[H] -r [FPS] -i pipe:0 \
       -c:v libx264 -preset ultrafast -tune zerolatency \
       -profile:v baseline -level 3.0 -pix_fmt yuv420p \
       -x264-params "bframes=0:keyint=30:min-keyint=30" \
       -f h264 pipe:1
```

### NAL Unit Types

| Type | Deskripsi | Handling |
|------|-----------|----------|
| 7 (SPS) | Sequence Parameter Set | Disimpan saat di-parse, prepend ke keyframe PERTAMA saja |
| 8 (PPS) | Picture Parameter Set | Disimpan saat di-parse, prepend ke keyframe PERTAMA saja |
| 5 (IDR) | Keyframe | Keyframe PERTAMA: SPS+PPS prepended. Keyframe berikutnya: tanpa SPS+PPS |
| 1 (Non-IDR) | P-frame | Dikirim langsung (setelah keyframe pertama terkirim) |

### Catatan Penting SPS+PPS Handling

- SPS+PPS **HANYA** dikirim dengan keyframe **PERTAMA** di awal sesi
- Keyframe berikutnya dikirim **TANPA** SPS+PPS karena decoder sudah tahu parameternya
- Flag `_firstKeyframeSent` digunakan untuk tracking ini
- **JANGAN** kirim SPS+PPS dengan setiap keyframe - ini bisa menyebabkan decode error

> **PENTING:** Host akan skip non-keyframe NAL units sampai keyframe pertama dengan SPS/PPS terkirim. Ini memastikan decoder di client bisa initialize dengan benar.

---

## H.264 Decoding Components (Tamu)

| Komponen | File | Deskripsi |
|----------|------|-----------|
| **H.264 Decoder** | `cls_H264Decoder.vb` | Wrapper FFmpeg decoder via stdin/stdout pipe |

### Alur H.264 Decoding

```
UDP Receive → Frame Assembler → FFmpeg stdin → BGRA output → WriteableBitmap
```

### FFmpeg Decoder Settings

```bash
# Mode dengan resolusi fixed
ffmpeg -f h264 -i pipe:0 -f rawvideo -pix_fmt bgra -s [W]x[H] pipe:1

# Mode auto-resolusi (deteksi dari SPS)
ffmpeg -f h264 -i pipe:0 -f rawvideo -pix_fmt bgra pipe:1
```

### Decoder Modes

| Mode | Method | Deskripsi |
|------|--------|-----------|
| Fixed Resolution | `Start(width, height)` | Resolusi output di-specify saat start |
| Auto Resolution | `StartAutoResolusi()` | Resolusi auto-detect dari SPS dalam H.264 stream |

### BGRA Rendering di Viewer

- **Direct Copy**: BGRA data dari FFmpeg langsung di-copy ke WriteableBitmap tanpa transformasi tambahan.
- **Auto-Resolution**: Resolusi WriteableBitmap dibuat berdasarkan resolusi yang terdeteksi dari FFmpeg stderr output.
- **Kesederhanaan**: Tidak perlu stride handling khusus karena resolusi sudah match.

### Events

| Event | Deskripsi |
|-------|-----------|
| `FrameReady` | BGRA frame tersedia (sudah di-decode) |
| `DecoderError` | Error pada decoder |
| `DecoderStopped` | Decoder berhenti |

---

## File Logging System

Sistem logging ke file untuk debugging, diimplementasikan di `mdl_VariabelUmum.vb`.

> **PENTING:** File logging **HANYA aktif di Mode Developer** (aplikasi dijalankan dari folder Debug). Di Release mode, `WriteLog()` hanya menulis ke `Debug.WriteLine` tanpa membuat file log.

### Lokasi Log Files

```
{AppDomain.CurrentDomain.BaseDirectory}\Logs\
```
Contoh: `Booku Remote\bin\Debug\net8.0-windows\Logs\`

### Format Nama File

```
BookuRemote_{Role}_{timestamp}.log
```
Contoh: `BookuRemote_Host_20260126_214532.log`, `BookuRemote_Tamu_20260126_214535.log`

### Fungsi Logging

| Fungsi | Deskripsi |
|--------|-----------|
| `InitLogFile(role As String)` | Inisialisasi file log dengan role (Host/Tamu) dan timestamp. **Hanya aktif jika `ModeDeveloper = True`** |
| `WriteLog(message As String)` | Menulis pesan ke `Debug.WriteLine` (selalu) + file log (hanya jika Mode Developer) |
| `BersihkanLogLama()` | Menghapus file log yang lebih dari 7 hari |

### Cara Penggunaan

```vb
' Di wpfWin_StartUp saat user pilih mode:
InitLogFile("Host")  ' atau "Tamu"

' Di seluruh kode untuk logging:
WriteLog("[UDP-HOST] Mengirim frame #123")
WriteLog($"[H264-DEC] Frame decoded: {width}x{height}")
```

### Fitur

- Thread-safe dengan `SyncLock`
- Dual output: `Debug.WriteLine` (selalu) + file logging (hanya Mode Developer)
- Auto-cleanup file lama (>7 hari)
- Graceful failure (tidak throw exception jika gagal write)
- **Kondisional**: File logging dinonaktifkan di Release mode untuk performa

> **Catatan:** Di Release mode, folder `Logs/` tidak akan dibuat dan tidak ada file log yang ditulis. Ini menghindari penumpukan file log di PC user.

---

## Catatan Implementasi H.264 Streaming

### Prinsip Dasar - Kesederhanaan adalah Kunci

Implementasi H.264 streaming yang berhasil menggunakan pendekatan **sederhana dan langsung**. Jangan menambahkan "perbaikan" yang tidak diperlukan.

### Yang BENAR (Implementasi Saat Ini)

1. ✅ SPS+PPS hanya di-prepend pada keyframe **PERTAMA** saja
2. ✅ `BitmapKeBgra()` menggunakan copy langsung tanpa stride handling
3. ✅ Skala capture default 0.6 (60% dari resolusi layar)
4. ✅ Cursor drawing via Windows API untuk menampilkan cursor di screenshot
5. ✅ Decoder menggunakan auto-resolution detection dari SPS

### Yang SALAH (Jangan Dilakukan)

- ❌ Jangan kirim SPS+PPS dengan setiap keyframe
- ❌ Jangan tambahkan stride padding handling row-by-row di `BitmapKeBgra()`
- ❌ Jangan force alpha channel ke 255
- ❌ Jangan tambahkan `CompositingMode.SourceCopy` di screen capture

> **Referensi:** Jika ada masalah, bandingkan dengan versi backup yang berhasil. Folder `X_Booku Remote` berisi versi yang gagal untuk referensi perbandingan.
