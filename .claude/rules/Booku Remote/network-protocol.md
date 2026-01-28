# Network Protocol

> **Konteks:** Dokumentasi ini khusus untuk **Project Booku Remote** (folder `BookuID/Booku Remote/`)

## Arsitektur Hybrid TCP/UDP

Booku Remote menggunakan arsitektur **hybrid TCP/UDP** untuk performa optimal:

| Layer | Protokol | Kegunaan |
|-------|----------|----------|
| **Control Plane** | TCP | Login, input keyboard/mouse, heartbeat |
| **Data Plane** | UDP | Video frames (high-throughput, low-latency) |

## Port yang Digunakan

| Port | Protokol | Lokasi | Kegunaan |
|------|----------|--------|----------|
| `45678` | UDP | LAN | Discovery perangkat (broadcast) |
| `45679` | TCP | LAN | Koneksi langsung (control plane) |
| `45680` | TCP | VPS | Relay Server (koneksi via internet) |
| `45681` | **UDP** | LAN/Relay | **Video streaming** (data plane) |

> **Catatan:** Semua port di atas adalah nilai **default** dan dapat dikonfigurasi manual melalui UI di window Host atau Tamu. Settings disimpan ke file JSON di `%AppData%\BookuID\Booku Remote\port-settings.json`.

## UDP Packet Structure (Video Streaming)

```
┌────────────────────────────────────────────────────────────┐
│  UDP Packet (max 1216 bytes)                               │
├────────────────────────────────────────────────────────────┤
│  Header (16 bytes)                                         │
│  ├── SessionId     (4 bytes) - Routing key                 │
│  ├── FrameId       (4 bytes) - Frame sequence number       │
│  ├── ChunkIndex    (2 bytes) - Chunk number (0-based)      │
│  ├── ChunkCount    (2 bytes) - Total chunks in frame       │
│  └── TimestampMs   (4 bytes) - Capture timestamp           │
├────────────────────────────────────────────────────────────┤
│  Payload (max 1200 bytes)                                  │
│  ├── CodecType     (1 byte)  - 0x00=JPEG, 0x01=H.264       │
│  └── VideoData     (max 1199 bytes) - JPEG/H.264 chunk     │
└────────────────────────────────────────────────────────────┘
```

## Video Codec Support

| Codec | CodecType | Keterangan |
|-------|-----------|------------|
| **JPEG** | `0x00` | Default, backward compatible |
| **H.264** | `0x01` | Low-latency encoding via FFmpeg |

## Keunggulan UDP Streaming

- **Latency rendah** - Tidak ada TCP acknowledgment overhead
- **Frame dropping** - Timeout 100ms, frame lama diabaikan
- **MTU-safe** - Chunk size 1200 bytes menghindari fragmentasi
- **Multi-codec** - Support JPEG dan H.264
- **Backward compatible** - JPEG fallback jika FFmpeg tidak tersedia

## Tipe Paket (Protokol)

```vb
Public Enum TipePaket
    ' Discovery (1-9)
    BROADCAST_DISCOVERY = 1
    RESPON_DISCOVERY = 2

    ' Koneksi (10-19)
    PERMINTAAN_KONEKSI = 10
    RESPON_KONEKSI = 11
    TUTUP_KONEKSI = 12
    HEARTBEAT = 13

    ' Remote Desktop (20-29)
    FRAME_LAYAR = 20
    INPUT_KEYBOARD = 21
    INPUT_MOUSE = 22
    CLIPBOARD_DATA = 23
    PERMINTAAN_STREAMING = 24
    HENTIKAN_STREAMING = 25

    ' Transfer Berkas (30-39)
    PERMINTAAN_BERKAS = 30
    DATA_BERKAS = 31
    KONFIRMASI_BERKAS = 32
    DAFTAR_FOLDER = 33
    RESPON_TRANSFER = 34          ' Accept/reject transfer request
    KONFIRMASI_CHUNK = 35         ' ACK per chunk (reliability)
    BATAL_TRANSFER = 36           ' Cancel transfer
    RESPON_DAFTAR_FOLDER = 37     ' Folder listing response

    ' Relay Server (40-59)
    RELAY_REGISTER_HOST = 40      ' Host → Relay: register
    RELAY_REGISTER_HOST_OK = 41   ' Relay → Host: confirm + HostCode
    RELAY_UNREGISTER_HOST = 42    ' Host → Relay: unregister
    RELAY_HOST_HEARTBEAT = 43     ' Host → Relay: keep-alive
    RELAY_QUERY_HOST = 45         ' Tamu → Relay: cari Host by code
    RELAY_QUERY_HOST_RESULT = 46  ' Relay → Tamu: info Host
    RELAY_CONNECT_REQUEST = 47    ' Tamu → Relay: minta koneksi
    RELAY_SESSION_STARTED = 52    ' Notify session dimulai
    RELAY_SESSION_ENDED = 53      ' Notify session berakhir
    RELAY_ERROR = 55              ' Generic error
    RELAY_HOST_OFFLINE = 56       ' Host tidak online
    RELAY_INVALID_CODE = 57       ' HostCode tidak valid
End Enum
```

## Konfigurasi Port dan Streaming

Port jaringan dan parameter streaming dapat dikonfigurasi manual melalui UI di window Host/Tamu.

### File dan Lokasi

| Komponen | Lokasi/File |
|----------|-------------|
| **Class Settings** | `Kelas/cls_SetelPort.vb` |
| **File JSON** | `%AppData%\BookuID\Booku Remote\port-settings.json` |

### Parameter yang Dapat Dikonfigurasi

| Parameter | Default | Range | Deskripsi |
|-----------|---------|-------|-----------|
| `PortDiscovery` | 45678 | - | UDP broadcast untuk discovery LAN |
| `PortKoneksi` | 45679 | - | TCP koneksi remote LAN (control plane) |
| `PortRelay` | 45680 | - | TCP koneksi via relay server |
| `PortUdpVideo` | 45681 | - | **UDP video streaming** (data plane) |
| `RelayServerIP` | 155.117.43.209 | - | Alamat IP relay server |
| `TargetFPS` | **15** | **5-30** | **Frame rate streaming (slider)** |

### Cara Kerja

1. **Startup:** `MuatSetelPort()` dipanggil di `InisialisasiVariabelUmum()` untuk load settings dari file JSON
2. **Runtime:** Variabel `PortDiscoveryAktif`, `PortKoneksiAktif`, `PortRelayAktif`, `RelayServerIPAktif`, `TargetFPSAktif` digunakan
3. **UI (Port):** User dapat mengubah port via Expander "Pengaturan Port" di window Host/Tamu
4. **UI (FPS):** User dapat mengubah FPS via slider di Expander "Pengaturan Streaming" di window Host
5. **Simpan:** Perubahan disimpan ke file JSON via `SetelPortAktif.SimpanKeFile()`
6. **Reset:** Tombol "Reset ke Default" mengembalikan ke nilai default

## Konfigurasi Streaming

| Parameter | Nilai Default | Range | Deskripsi |
|-----------|---------------|-------|-----------|
| **Skala Frame** | **0.6 (60%)** | 0.1-1.0 | Skala screenshot (resolusi output) |
| **Target FPS** | **15** | **5-30** | **Frame per second (dapat dikonfigurasi via slider di Host)** |
| JPEG Quality | 30 | - | Kualitas kompresi JPEG |
| Mouse Throttle | 30ms | - | Interval minimum mouse move |
| **Gambar Cursor** | **True** | - | **Menggambar cursor Host pada screenshot** |

> **Catatan:** Target FPS dapat diubah melalui slider di Expander "Pengaturan Streaming" pada window Host. Nilai lebih rendah mengurangi beban bandwidth dan CPU, berguna jika client (terutama Android) mengalami lag atau crash.

## Keamanan

| Aspek | Implementasi |
|-------|--------------|
| **Persetujuan** | Host harus menyetujui setiap koneksi |
| **Izin Kontrol** | Host memilih apakah mengizinkan kontrol input |
| **Kunci Sesi** | Session key untuk validasi paket |
| **Toggle Kontrol** | Tamu harus manually enable kontrol |
