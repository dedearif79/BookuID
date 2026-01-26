using Android.Media;
using Android.Graphics;
using System.Collections.Concurrent;

namespace BookuRemoteAndroid.Services;

/// <summary>
/// Service untuk decode H.264 video stream menggunakan Android MediaCodec.
/// Menerima H.264 NAL units dan menghasilkan decoded frames (JPEG bytes).
/// </summary>
public class H264DecoderService : IDisposable
{
    #region Constants

    /// <summary>MIME type untuk H.264</summary>
    private const string MIME_TYPE = "video/avc";

    /// <summary>Timeout untuk dequeue buffer (microseconds)</summary>
    private const int DEQUEUE_TIMEOUT_US = 10000;

    /// <summary>JPEG quality untuk output</summary>
    private const int JPEG_QUALITY = 80;

    #endregion

    #region Fields

    private MediaCodec? _decoder;
    private MediaFormat? _format;
    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _outputTask;

    private int _width;
    private int _height;
    private int _stride;        // Row stride (bisa > width karena padding)
    private int _sliceHeight;   // Slice height (bisa > height karena padding)
    private bool _isRunning;
    private bool _isDisposed;
    private readonly object _lock = new();

    // Queue untuk decoded frames
    private readonly ConcurrentQueue<DecodedFrame> _outputQueue = new();

    // Statistics
    private int _framesDecoded;
    private int _framesDropped;

    #endregion

    #region Events

    /// <summary>
    /// Event ketika frame berhasil di-decode
    /// </summary>
    public event EventHandler<DecodedFrameEventArgs>? FrameDecoded;

    /// <summary>
    /// Event ketika terjadi error
    /// </summary>
    public event EventHandler<DecoderErrorEventArgs>? DecoderError;

    #endregion

    #region Properties

    /// <summary>True jika decoder sedang berjalan</summary>
    public bool IsRunning => _isRunning;

    /// <summary>True jika decoder tersedia dan siap digunakan</summary>
    public bool IsDecoderAvailable => _isRunning && _decoder != null;

    /// <summary>Lebar video output</summary>
    public int Width => _width;

    /// <summary>Tinggi video output</summary>
    public int Height => _height;

    #endregion

    #region Public Methods

    /// <summary>
    /// Memulai decoder dengan resolusi yang ditentukan
    /// </summary>
    /// <param name="width">Lebar video</param>
    /// <param name="height">Tinggi video</param>
    /// <returns>True jika berhasil dimulai</returns>
    public bool Start(int width, int height)
    {
        lock (_lock)
        {
            if (_isRunning) return true;
            if (_isDisposed) return false;

            _width = width;
            _height = height;
            _stride = width;      // Default, akan diupdate dari OutputFormat
            _sliceHeight = height; // Default, akan diupdate dari OutputFormat

            try
            {
                // Buat MediaCodec decoder
                _decoder = MediaCodec.CreateDecoderByType(MIME_TYPE);
                if (_decoder == null)
                {
                    System.Diagnostics.Debug.WriteLine("[H264-ANDROID] Failed to create MediaCodec decoder");
                    return false;
                }

                // Konfigurasi format
                _format = MediaFormat.CreateVideoFormat(MIME_TYPE, width, height);

                // Configure decoder (null surface = ByteBuffer mode)
                _decoder.Configure(_format, null, null, MediaCodecConfigFlags.None);
                _decoder.Start();

                _cancellationTokenSource = new CancellationTokenSource();
                _isRunning = true;

                // Start output thread
                _outputTask = Task.Run(() => OutputLoopAsync(_cancellationTokenSource.Token));

                System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Decoder started: {width}x{height}");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Start error: {ex.Message}");
                Cleanup();
                return false;
            }
        }
    }

    /// <summary>
    /// Menghentikan decoder
    /// </summary>
    public void Stop()
    {
        lock (_lock)
        {
            if (!_isRunning) return;

            System.Diagnostics.Debug.WriteLine("[H264-ANDROID] Stopping decoder...");

            _isRunning = false;
            _cancellationTokenSource?.Cancel();

            Cleanup();

            System.Diagnostics.Debug.WriteLine("[H264-ANDROID] Decoder stopped");
        }
    }

    // Counter untuk input tracking
    private int _inputCount = 0;
    private int _queuedInputs = 0;
    private int _noBufferCount = 0;

    // Throttling untuk stabilitas
    // CATATAN: Throttling terlalu agresif bisa menyebabkan frame drop dan visual artifacts
    // H.264 stream membutuhkan semua NAL units (SPS, PPS, IDR, P-frames) untuk decode yang benar
    private DateTime _lastInputTime = DateTime.MinValue;
    private const int MIN_INPUT_INTERVAL_MS = 5; // Max 200 NAL/detik - lebih longgar untuk H.264
    private int _consecutiveKeyframes = 0;
    private const int MAX_CONSECUTIVE_KEYFRAMES = 30; // Sangat toleran - 30 keyframes berturut (jarang terjadi)

    // Tracking untuk diagnostics
    private int _skippedPFrames = 0;
    private int _skippedKeyframes = 0;

    /// <summary>
    /// Mengirim H.264 data ke decoder
    /// </summary>
    /// <param name="h264Data">H.264 NAL unit data</param>
    /// <returns>True jika berhasil dikirim</returns>
    public bool SendData(byte[] h264Data)
    {
        if (!_isRunning || _decoder == null || _isDisposed) return false;
        if (h264Data == null || h264Data.Length == 0) return false;

        try
        {
            // Double-check state
            if (!_isRunning || _decoder == null) return false;
            _inputCount++;

            // Log NAL info untuk debugging lebih intensif
            var nalType = h264Data.Length > 4 ? GetNalType(h264Data) : -1;

            // === THROTTLING: Batasi input rate ===
            var now = DateTime.UtcNow;
            var elapsed = (now - _lastInputTime).TotalMilliseconds;

            // Skip non-keyframe jika terlalu cepat (kecuali SPS/PPS yang wajib untuk decoder init)
            if (elapsed < MIN_INPUT_INTERVAL_MS && nalType != 7 && nalType != 8)
            {
                // Hanya skip P-frame (type 1) dengan interval sangat pendek, keyframe SELALU diproses
                if (nalType == 1)
                {
                    _skippedPFrames++;
                    if (_skippedPFrames <= 10 || _skippedPFrames % 50 == 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"[H264-SKIP] P-frame skipped (too fast), total skipped={_skippedPFrames}");
                    }
                    return false; // Skip P-frame
                }
            }

            // Skip consecutive keyframes (setelah banyak keyframe berturut, skip untuk beri waktu decoder)
            // Dalam streaming normal, keyframes berturut-turut jarang terjadi
            if (nalType == 5) // IDR keyframe
            {
                _consecutiveKeyframes++;
                if (_consecutiveKeyframes > MAX_CONSECUTIVE_KEYFRAMES)
                {
                    _skippedKeyframes++;
                    if (_skippedKeyframes <= 5 || _skippedKeyframes % 10 == 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"[H264-SKIP] Keyframe skipped (consecutive={_consecutiveKeyframes}), total skipped={_skippedKeyframes}");
                    }
                    return false;
                }
            }
            else if (nalType == 1) // P-frame resets consecutive counter
            {
                _consecutiveKeyframes = 0;
            }

            _lastInputTime = now;

            // Log lebih sering untuk debugging H.264 issues
            if (_inputCount <= 50 || _inputCount % 5 == 0 || nalType == 7 || nalType == 8 || nalType == 5)
            {
                System.Diagnostics.Debug.WriteLine($"[H264-INPUT] #{_inputCount}: {h264Data.Length} bytes, NAL={nalType}, queued={_queuedInputs}, noBuf={_noBufferCount}, consKF={_consecutiveKeyframes}");

                // Log first few bytes untuk debugging
                if (h264Data.Length >= 8 && _inputCount <= 20)
                {
                    var hexBytes = BitConverter.ToString(h264Data, 0, Math.Min(16, h264Data.Length));
                    System.Diagnostics.Debug.WriteLine($"[H264-INPUT] First bytes: {hexBytes}");
                }
            }

            // Get input buffer
            int inputBufferIndex = _decoder.DequeueInputBuffer(DEQUEUE_TIMEOUT_US);
            if (inputBufferIndex < 0)
            {
                _noBufferCount++;
                if (_noBufferCount <= 10 || _noBufferCount % 20 == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"[H264-INPUT] No buffer #{_noBufferCount} (input #{_inputCount})");
                }
                return false;
            }
            _queuedInputs++;

            var inputBuffer = _decoder.GetInputBuffer(inputBufferIndex);
            if (inputBuffer == null)
            {
                return false;
            }

            // Copy data ke buffer
            inputBuffer.Clear();
            inputBuffer.Put(h264Data);

            // Queue input buffer
            _decoder.QueueInputBuffer(
                inputBufferIndex,
                0,
                h264Data.Length,
                GetTimestampUs(),
                MediaCodecBufferFlags.None
            );

            return true;
        }
        catch (ObjectDisposedException)
        {
            System.Diagnostics.Debug.WriteLine("[H264-ANDROID] SendData: decoder disposed");
            return false;
        }
        catch (Java.Lang.IllegalStateException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] SendData: MediaCodec illegal state: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] SendData error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Parse NAL type dari data (untuk debugging)
    /// </summary>
    private int GetNalType(byte[] data)
    {
        // Cari start code dan ambil NAL type
        for (int i = 0; i < data.Length - 4; i++)
        {
            if (data[i] == 0 && data[i + 1] == 0)
            {
                if (data[i + 2] == 0 && data[i + 3] == 1)
                {
                    // 4-byte start code
                    if (i + 4 < data.Length)
                        return data[i + 4] & 0x1F;
                }
                else if (data[i + 2] == 1)
                {
                    // 3-byte start code
                    if (i + 3 < data.Length)
                        return data[i + 3] & 0x1F;
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// Mengirim H.264 data ke decoder secara async
    /// </summary>
    public Task<bool> SendDataAsync(byte[] h264Data)
    {
        return Task.Run(() => SendData(h264Data));
    }

    /// <summary>
    /// Alias untuk SendData - untuk kompatibilitas dengan ViewerPage
    /// </summary>
    /// <param name="h264Data">H.264 NAL unit data</param>
    /// <returns>True jika berhasil dikirim</returns>
    public bool ProcessH264Data(byte[] h264Data)
    {
        return SendData(h264Data);
    }

    #endregion

    #region Private Methods

    // Color format yang terdeteksi
    private int _colorFormat = 0;

    /// <summary>
    /// Loop untuk mengambil output dari decoder
    /// </summary>
    private async Task OutputLoopAsync(CancellationToken token)
    {
        System.Diagnostics.Debug.WriteLine("[H264-ANDROID] Output loop started");

        var bufferInfo = new MediaCodec.BufferInfo();
        int outputCount = 0;
        int errorCount = 0;
        const int MAX_CONSECUTIVE_ERRORS = 10;
        DateTime lastOutputTime = DateTime.UtcNow;

        while (!token.IsCancellationRequested && _isRunning && _decoder != null)
        {
            try
            {
                // Double-check running state before accessing decoder
                if (!_isRunning || _decoder == null)
                {
                    System.Diagnostics.Debug.WriteLine("[H264-ANDROID] Output loop: decoder stopped, exiting");
                    break;
                }

                // Safety check: jika tidak ada output dalam 5 detik, log warning
                if ((DateTime.UtcNow - lastOutputTime).TotalSeconds > 5 && outputCount > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Warning: No output for 5s, outputs so far={outputCount}");
                    lastOutputTime = DateTime.UtcNow;
                }
                // Dequeue output buffer
                int outputBufferIndex = _decoder.DequeueOutputBuffer(bufferInfo, DEQUEUE_TIMEOUT_US);

                if (outputBufferIndex >= 0)
                {
                    outputCount++;
                    errorCount = 0; // Reset error count on success
                    lastOutputTime = DateTime.UtcNow;

                    // Get output buffer
                    var outputBuffer = _decoder.GetOutputBuffer(outputBufferIndex);
                    if (outputBuffer != null && bufferInfo.Size > 0)
                    {
                        // Copy data dari buffer
                        var data = new byte[bufferInfo.Size];
                        outputBuffer.Position(bufferInfo.Offset);
                        outputBuffer.Get(data, 0, bufferInfo.Size);

                        // Log output (lebih sering untuk debugging intensif)
                        int expectedYuvSize = _width * _height * 3 / 2;
                        int expectedWithStride = _stride * _sliceHeight * 3 / 2;

                        // Log setiap frame untuk pertama 30 frame, lalu setiap 5 frame
                        if (outputCount <= 30 || outputCount % 5 == 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"[H264-OUTPUT] #{outputCount}: bufSize={bufferInfo.Size}, expected={expectedYuvSize}, withStride={expectedWithStride}, dim={_width}x{_height}, stride={_stride}, sliceH={_sliceHeight}, colorFmt={_colorFormat}");

                            // Log color format name untuk clarity
                            string colorFormatName = _colorFormat switch
                            {
                                19 => "I420/YUV420Planar",
                                21 => "NV12/YUV420SemiPlanar",
                                17 => "NV21",
                                _ => $"Unknown({_colorFormat})"
                            };
                            System.Diagnostics.Debug.WriteLine($"[H264-OUTPUT] Color format: {colorFormatName}");
                        }

                        // Auto-detect stride dari buffer size jika belum diset atau tidak sesuai
                        AutoDetectStrideFromBufferSize(bufferInfo.Size);

                        // Convert YUV ke JPEG
                        var jpegData = ConvertYuvToJpeg(data, _width, _height);
                        if (jpegData != null)
                        {
                            _framesDecoded++;

                            // Raise event dengan DecodedFrameEventArgs
                            FrameDecoded?.Invoke(this, new DecodedFrameEventArgs
                            {
                                JpegData = jpegData,
                                Frame = new DecodedFrameInfo
                                {
                                    Width = _width,
                                    Height = _height
                                },
                                Timestamp = DateTime.UtcNow
                            });
                        }
                        else
                        {
                            _framesDropped++;
                            if (_framesDropped % 10 == 1)
                            {
                                System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] YUV conversion failed, dropped={_framesDropped}");
                            }
                        }
                    }

                    // Release output buffer
                    _decoder.ReleaseOutputBuffer(outputBufferIndex, false);
                }
                else if (outputBufferIndex == (int)MediaCodecInfoState.OutputFormatChanged)
                {
                    // Format changed, get new format
                    var newFormat = _decoder.OutputFormat;
                    if (newFormat != null)
                    {
                        _width = newFormat.GetInteger(MediaFormat.KeyWidth);
                        _height = newFormat.GetInteger(MediaFormat.KeyHeight);

                        // Dapatkan stride dan slice height (penting untuk konversi YUV!)
                        try
                        {
                            _stride = newFormat.GetInteger(MediaFormat.KeyStride);
                        }
                        catch
                        {
                            _stride = _width; // Default ke width jika tidak tersedia
                        }

                        try
                        {
                            _sliceHeight = newFormat.GetInteger(MediaFormat.KeySliceHeight);
                        }
                        catch
                        {
                            _sliceHeight = _height; // Default ke height jika tidak tersedia
                        }

                        // Dapatkan color format
                        try
                        {
                            _colorFormat = newFormat.GetInteger(MediaFormat.KeyColorFormat);
                        }
                        catch
                        {
                            _colorFormat = 0;
                        }

                        System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Output format changed: {_width}x{_height}, stride={_stride}, sliceHeight={_sliceHeight}, colorFormat={_colorFormat}");
                    }
                }
                else if (outputBufferIndex == (int)MediaCodecInfoState.TryAgainLater)
                {
                    // No output available yet
                    await Task.Delay(1, token);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (ObjectDisposedException)
            {
                // Decoder sudah di-dispose, keluar dari loop
                System.Diagnostics.Debug.WriteLine("[H264-ANDROID] Output loop: ObjectDisposedException, exiting");
                break;
            }
            catch (Java.Lang.IllegalStateException ex)
            {
                // MediaCodec in invalid state, exit
                System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Output loop: MediaCodec illegal state: {ex.Message}");
                break;
            }
            catch (Exception ex)
            {
                errorCount++;
                System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Output loop error #{errorCount}: {ex.Message}");

                // Jika terlalu banyak error berturut-turut, delay lebih lama
                if (errorCount >= MAX_CONSECUTIVE_ERRORS)
                {
                    System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Too many errors ({errorCount}), longer delay");
                    try { await Task.Delay(100, token); } catch { break; }
                    errorCount = 0; // Reset untuk mencoba lagi
                }
                else
                {
                    try { await Task.Delay(10, token); } catch { break; }
                }
            }
        }

        System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Output loop ended, total outputs={outputCount}, decoded={_framesDecoded}, dropped={_framesDropped}, errors={errorCount}");
    }

    /// <summary>
    /// Auto-detect stride dari ukuran buffer output
    /// Dipanggil sebelum konversi YUV untuk memastikan stride benar
    /// </summary>
    private void AutoDetectStrideFromBufferSize(int bufferSize)
    {
        if (_width <= 0 || _height <= 0) return;

        int expectedNoStride = _width * _height * 3 / 2;
        int expectedWithCurrentStride = _stride * _sliceHeight * 3 / 2;

        // Jika buffer size sama dengan expected tanpa stride, stride = width
        if (bufferSize == expectedNoStride)
        {
            if (_stride != _width && _framesDecoded <= 5)
            {
                System.Diagnostics.Debug.WriteLine($"[H264-STRIDE] Auto-detect: buffer matches no-stride, setting stride={_width}");
                _stride = _width;
                _sliceHeight = _height;
            }
            return;
        }

        // Jika buffer size sama dengan expected dengan current stride, stride sudah benar
        if (bufferSize == expectedWithCurrentStride)
        {
            return;
        }

        // Buffer size berbeda - coba hitung stride
        // Untuk YUV420: bufferSize = stride * sliceHeight * 1.5
        // Jika kita assume sliceHeight = height:
        // stride = bufferSize * 2 / (3 * height)

        // Coba berbagai alignment (16, 32, 64, 128)
        int[] alignments = { 16, 32, 64, 128 };

        foreach (int align in alignments)
        {
            int alignedWidth = ((_width + align - 1) / align) * align;
            int alignedHeight = ((_height + align - 1) / align) * align;

            // Coba dengan aligned width saja
            int expectedAlignedW = alignedWidth * _height * 3 / 2;
            if (bufferSize == expectedAlignedW)
            {
                if (_stride != alignedWidth)
                {
                    System.Diagnostics.Debug.WriteLine($"[H264-STRIDE] Auto-detect: align={align}, stride={alignedWidth} (width only)");
                    _stride = alignedWidth;
                    _sliceHeight = _height;
                }
                return;
            }

            // Coba dengan aligned width dan height
            int expectedAlignedBoth = alignedWidth * alignedHeight * 3 / 2;
            if (bufferSize == expectedAlignedBoth)
            {
                if (_stride != alignedWidth || _sliceHeight != alignedHeight)
                {
                    System.Diagnostics.Debug.WriteLine($"[H264-STRIDE] Auto-detect: align={align}, stride={alignedWidth}, sliceHeight={alignedHeight}");
                    _stride = alignedWidth;
                    _sliceHeight = alignedHeight;
                }
                return;
            }
        }

        // Tidak ada alignment yang cocok, coba hitung manual
        // Asumsi sliceHeight == height, hitung stride dari bufferSize
        int calculatedStride = bufferSize * 2 / (3 * _height);
        if (calculatedStride >= _width && calculatedStride <= _width * 2)
        {
            // Pastikan stride adalah kelipatan 2 (untuk UV plane)
            calculatedStride = (calculatedStride / 2) * 2;

            if (_stride != calculatedStride)
            {
                System.Diagnostics.Debug.WriteLine($"[H264-STRIDE] Auto-detect: calculated stride={calculatedStride} from bufferSize={bufferSize}");
                _stride = calculatedStride;
                _sliceHeight = _height;
            }
        }
        else if (_framesDecoded <= 5)
        {
            System.Diagnostics.Debug.WriteLine($"[H264-STRIDE] Warning: cannot determine stride. bufferSize={bufferSize}, width={_width}, height={_height}");
        }
    }

    /// <summary>
    /// Convert YUV data ke JPEG
    /// </summary>
    private byte[]? ConvertYuvToJpeg(byte[] yuvData, int width, int height)
    {
        YuvImage? yuvImage = null;
        System.IO.MemoryStream? outputStream = null;

        try
        {
            // Gunakan stride dan sliceHeight jika tersedia, atau fallback ke width/height
            int stride = _stride > 0 ? _stride : width;
            int sliceHeight = _sliceHeight > 0 ? _sliceHeight : height;

            // Validasi ukuran data dengan mempertimbangkan stride
            // Untuk YUV420: Y plane = stride * sliceHeight, UV plane = stride * sliceHeight / 2
            int expectedSizeWithStride = stride * sliceHeight * 3 / 2;
            int expectedSizeNoStride = width * height * 3 / 2;

            if (yuvData.Length < expectedSizeNoStride)
            {
                System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] YUV data too small: {yuvData.Length} < {expectedSizeNoStride}");
                return null;
            }

            // Log jika stride berbeda dari width (ini sering terjadi di Android!)
            if (stride != width && _framesDecoded <= 5)
            {
                System.Diagnostics.Debug.WriteLine($"[H264-YUV] Stride mismatch: width={width}, stride={stride}, sliceHeight={sliceHeight}");
            }

            // SELALU buat copy dari data untuk menghindari race condition
            byte[] nv21Data;

            // Color format codes:
            // 19 = YUV420Planar (I420) - FFmpeg baseline H.264 default
            // 21 = YUV420SemiPlanar (NV12)
            // 17 = NV21
            // 0 = belum diketahui → default ke I420 untuk baseline H.264

            // Log conversion method yang dipilih (hanya beberapa frame pertama)
            if (_framesDecoded <= 5)
            {
                System.Diagnostics.Debug.WriteLine($"[YUV-SELECT] colorFormat={_colorFormat}, dataLen={yuvData.Length}, w={width}, h={height}, stride={stride}, sliceH={sliceHeight}");
            }

            // === FIX: Default ke I420 untuk baseline H.264 ===
            // FFmpeg dengan -profile:v baseline -pix_fmt yuv420p output I420 (planar)
            // Banyak device Android juga output I420 untuk baseline profile
            // Jika colorFormat belum terdeteksi (0), asumsi I420 bukan NV12
            int effectiveColorFormat = _colorFormat;
            if (effectiveColorFormat == 0)
            {
                // Default ke I420 (19) untuk baseline H.264 dari FFmpeg
                effectiveColorFormat = 19;
                if (_framesDecoded <= 3)
                {
                    System.Diagnostics.Debug.WriteLine($"[YUV-SELECT] colorFormat=0 (unknown), defaulting to I420 (19) for baseline H.264");
                }
            }

            if (effectiveColorFormat == 21) // NV12 - perlu swap UV
            {
                if (_framesDecoded <= 3)
                {
                    System.Diagnostics.Debug.WriteLine($"[YUV-SELECT] Using NV12→NV21 conversion");
                }
                nv21Data = ConvertNv12ToNv21WithStride(yuvData, width, height, stride, sliceHeight);
            }
            else if (effectiveColorFormat == 19 || effectiveColorFormat == 0) // I420/YUV420Planar (atau default)
            {
                if (_framesDecoded <= 3)
                {
                    System.Diagnostics.Debug.WriteLine($"[YUV-SELECT] Using I420→NV21 conversion");
                }
                nv21Data = ConvertI420ToNv21WithStride(yuvData, width, height, stride, sliceHeight);
            }
            else if (_colorFormat == 17) // Sudah NV21, tapi perlu handle stride
            {
                if (stride == width)
                {
                    // No padding, simple copy
                    nv21Data = new byte[width * height * 3 / 2];
                    Buffer.BlockCopy(yuvData, 0, nv21Data, 0, Math.Min(yuvData.Length, nv21Data.Length));
                }
                else
                {
                    // Ada padding, perlu strip stride
                    nv21Data = StripStrideFromNv21(yuvData, width, height, stride, sliceHeight);
                }
            }
            else
            {
                // Format tidak dikenal, coba I420 conversion sebagai default
                // (baseline H.264 dari FFmpeg biasanya output I420/yuv420p)
                if (_framesDecoded <= 3)
                {
                    System.Diagnostics.Debug.WriteLine($"[YUV-SELECT] Unknown format {_colorFormat}, defaulting to I420→NV21");
                }
                nv21Data = ConvertI420ToNv21WithStride(yuvData, width, height, stride, sliceHeight);
            }

            // Validasi hasil konversi
            if (nv21Data == null || nv21Data.Length == 0)
            {
                System.Diagnostics.Debug.WriteLine("[H264-ANDROID] YUV conversion returned empty array");
                return null;
            }

            // Buat YuvImage dengan width (bukan stride!)
            // Karena data sudah di-strip dari padding
            yuvImage = new YuvImage(nv21Data, ImageFormatType.Nv21, width, height, null);
            outputStream = new System.IO.MemoryStream();

            // Compress ke JPEG
            var rect = new Android.Graphics.Rect(0, 0, width, height);
            bool success = yuvImage.CompressToJpeg(rect, JPEG_QUALITY, outputStream);

            if (!success)
            {
                System.Diagnostics.Debug.WriteLine("[H264-ANDROID] CompressToJpeg returned false");
                return null;
            }

            // Copy result ke array baru sebelum dispose
            var result = outputStream.ToArray();

            // Log hasil JPEG compression (beberapa frame pertama)
            if (_framesDecoded <= 5 || _framesDecoded % 30 == 0)
            {
                System.Diagnostics.Debug.WriteLine($"[H264-JPEG] Frame #{_framesDecoded}: NV21 size={nv21Data.Length}, JPEG size={result.Length}, quality={JPEG_QUALITY}");
            }
            return result;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] YUV to JPEG error: {ex.Message}");
            return null;
        }
        finally
        {
            // Dispose secara eksplisit dengan try-catch
            try { outputStream?.Dispose(); } catch { }
            try { yuvImage?.Dispose(); } catch { }
        }
    }

    /// <summary>
    /// Convert NV12 ke NV21 dengan dukungan stride (PENTING untuk Android MediaCodec!)
    /// </summary>
    /// <param name="nv12">Input NV12 data dengan stride</param>
    /// <param name="width">Actual video width</param>
    /// <param name="height">Actual video height</param>
    /// <param name="stride">Row stride (bisa lebih besar dari width)</param>
    /// <param name="sliceHeight">Slice height (bisa lebih besar dari height)</param>
    /// <returns>NV21 data tanpa padding (width * height * 1.5 bytes)</returns>
    private byte[] ConvertNv12ToNv21WithStride(byte[] nv12, int width, int height, int stride, int sliceHeight)
    {
        try
        {
            // Validasi input
            if (nv12 == null || width <= 0 || height <= 0 || stride <= 0 || sliceHeight <= 0)
            {
                System.Diagnostics.Debug.WriteLine($"[YUV-CONV] Invalid params: w={width}, h={height}, stride={stride}, slice={sliceHeight}");
                return new byte[0];
            }

            // Output size tanpa padding
            int outYSize = width * height;
            int outUvSize = width * height / 2;
            var nv21 = new byte[outYSize + outUvSize];

            // Hitung berbagai kemungkinan UV offset
            int srcYPlaneSizeWithStride = stride * sliceHeight;
            int srcYPlaneSizeNoStride = width * height;

            // Log untuk debugging (hanya beberapa frame pertama)
            if (_framesDecoded <= 5)
            {
                System.Diagnostics.Debug.WriteLine($"[YUV-CONV] bufLen={nv12.Length}, w={width}, h={height}, stride={stride}, slice={sliceHeight}");
                System.Diagnostics.Debug.WriteLine($"[YUV-CONV] outYSize={outYSize}, YwithStride={srcYPlaneSizeWithStride}, YnoStride={srcYPlaneSizeNoStride}");
            }

            // === Deteksi apakah buffer memiliki stride atau tidak ===
            bool hasStride = (stride != width) && (nv12.Length >= srcYPlaneSizeWithStride + (stride * sliceHeight / 2));

            // Jika buffer size sama dengan expected tanpa stride, asumsikan tidak ada padding
            if (nv12.Length == outYSize + outUvSize)
            {
                hasStride = false;
                if (_framesDecoded <= 5)
                {
                    System.Diagnostics.Debug.WriteLine($"[YUV-CONV] Buffer matches no-stride size, using simple conversion");
                }
            }

            if (!hasStride)
            {
                // === SIMPLE PATH: Tidak ada stride, langsung swap UV ===
                // Copy Y plane langsung
                Buffer.BlockCopy(nv12, 0, nv21, 0, Math.Min(outYSize, nv12.Length));

                // UV plane dimulai setelah Y
                int uvOffset = outYSize;
                if (uvOffset < nv12.Length)
                {
                    // Swap UV bytes (NV12 UVUV → NV21 VUVU)
                    int uvLen = Math.Min(outUvSize, nv12.Length - uvOffset);
                    for (int i = 0; i < uvLen; i += 2)
                    {
                        if (uvOffset + i + 1 < nv12.Length && outYSize + i + 1 < nv21.Length)
                        {
                            nv21[outYSize + i] = nv12[uvOffset + i + 1];     // V
                            nv21[outYSize + i + 1] = nv12[uvOffset + i];     // U
                        }
                    }
                }

                return nv21;
            }

            // === STRIDE PATH: Ada padding, perlu strip per baris ===

            // Copy Y plane dengan stripping stride padding
            for (int y = 0; y < height; y++)
            {
                int srcOffset = y * stride;
                int dstOffset = y * width;

                if (srcOffset + width <= nv12.Length)
                {
                    Buffer.BlockCopy(nv12, srcOffset, nv21, dstOffset, width);
                }
            }

            // UV plane - coba beberapa kemungkinan offset
            int uvHeight = height / 2;
            int uvOffset1 = srcYPlaneSizeWithStride; // Setelah Y plane dengan stride padding

            // Jika offset pertama invalid, coba offset kedua
            if (uvOffset1 + stride > nv12.Length)
            {
                uvOffset1 = srcYPlaneSizeNoStride; // Langsung setelah Y tanpa padding
            }

            if (_framesDecoded <= 5)
            {
                System.Diagnostics.Debug.WriteLine($"[YUV-CONV] UV offset={uvOffset1}, hasStride={hasStride}");
            }

            // Copy UV plane dengan swap dan stripping stride
            for (int y = 0; y < uvHeight; y++)
            {
                int srcRowOffset = uvOffset1 + (y * stride);
                int dstRowOffset = outYSize + (y * width);

                // Fallback jika stride offset invalid
                if (srcRowOffset + width > nv12.Length)
                {
                    srcRowOffset = uvOffset1 + (y * width);
                }

                for (int x = 0; x < width; x += 2)
                {
                    if (srcRowOffset + x + 1 < nv12.Length && dstRowOffset + x + 1 < nv21.Length)
                    {
                        // NV12: U, V → NV21: V, U (swap)
                        nv21[dstRowOffset + x] = nv12[srcRowOffset + x + 1];     // V
                        nv21[dstRowOffset + x + 1] = nv12[srcRowOffset + x];     // U
                    }
                }
            }

            return nv21;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[YUV-CONV] Exception: {ex.Message}");
            return new byte[0];
        }
    }

    /// <summary>
    /// Convert I420 (YUV420Planar) ke NV21 dengan dukungan stride
    /// I420 layout: Y plane (full), U plane (quarter), V plane (quarter) - all planar/separate
    /// NV21 layout: Y plane (full), VU interleaved (half)
    /// </summary>
    private byte[] ConvertI420ToNv21WithStride(byte[] i420, int width, int height, int stride, int sliceHeight)
    {
        try
        {
            // Output size tanpa padding
            int outYSize = width * height;
            int outUvSize = width * height / 2;
            var nv21 = new byte[outYSize + outUvSize];

            // Log untuk debugging (hanya beberapa frame pertama)
            if (_framesDecoded <= 5)
            {
                System.Diagnostics.Debug.WriteLine($"[I420-CONV] bufLen={i420.Length}, w={width}, h={height}, stride={stride}, slice={sliceHeight}");
            }

            // === Deteksi apakah ada stride atau tidak ===
            bool hasStride = (stride != width);
            int expectedNoStride = width * height * 3 / 2; // 381024 untuk 672x378

            // Jika buffer size cocok dengan tanpa stride, gunakan simple path
            if (i420.Length == expectedNoStride || !hasStride)
            {
                if (_framesDecoded <= 5)
                {
                    System.Diagnostics.Debug.WriteLine($"[I420-CONV] Using simple path (no stride)");
                }

                // === SIMPLE PATH: Tidak ada stride ===
                // Copy Y plane langsung
                Buffer.BlockCopy(i420, 0, nv21, 0, outYSize);

                // I420 layout (dari FFmpeg yuv420p):
                // Y plane: 0 to (width * height)
                // U plane: (width * height) to (width * height * 1.25)
                // V plane: (width * height * 1.25) to (width * height * 1.5)
                int uPlaneOffset = outYSize;                    // U plane pertama (I420 style)
                int vPlaneOffset = outYSize + (outYSize / 4);   // V plane kedua

                int uvWidth = width / 2;
                int uvHeight = height / 2;

                // Log sample UV values untuk diagnosis (hanya frame pertama)
                if (_framesDecoded <= 3)
                {
                    // Sample beberapa nilai UV dari tengah frame
                    int midY = uvHeight / 2;
                    int midX = uvWidth / 2;
                    int sampleUIdx = uPlaneOffset + (midY * uvWidth) + midX;
                    int sampleVIdx = vPlaneOffset + (midY * uvWidth) + midX;

                    byte sampleU = sampleUIdx < i420.Length ? i420[sampleUIdx] : (byte)0;
                    byte sampleV = sampleVIdx < i420.Length ? i420[sampleVIdx] : (byte)0;
                    byte sampleY = (height / 2 * width + width / 2) < i420.Length ? i420[height / 2 * width + width / 2] : (byte)0;

                    System.Diagnostics.Debug.WriteLine($"[I420-SAMPLE] Frame #{_framesDecoded}: Y={sampleY}, U={sampleU} (offset={sampleUIdx}), V={sampleV} (offset={sampleVIdx})");
                    System.Diagnostics.Debug.WriteLine($"[I420-SAMPLE] Offsets: Y=0, U={uPlaneOffset}, V={vPlaneOffset}, bufLen={i420.Length}");
                }

                // Interleave V dan U ke NV21 format (VU VU VU ...)
                int dstIdx = outYSize;
                for (int y = 0; y < uvHeight; y++)
                {
                    for (int x = 0; x < uvWidth; x++)
                    {
                        int srcVIdx = vPlaneOffset + (y * uvWidth) + x;
                        int srcUIdx = uPlaneOffset + (y * uvWidth) + x;

                        if (srcVIdx < i420.Length && srcUIdx < i420.Length && dstIdx + 1 < nv21.Length)
                        {
                            nv21[dstIdx++] = i420[srcVIdx]; // V first in NV21
                            nv21[dstIdx++] = i420[srcUIdx]; // U second
                        }
                    }
                }

                return nv21;
            }

            // === STRIDE PATH: Ada padding ===
            if (_framesDecoded <= 5)
            {
                System.Diagnostics.Debug.WriteLine($"[I420-CONV] Using stride path");
            }

            int srcYPlaneSize = stride * sliceHeight;
            int srcUvPlaneSize = (stride / 2) * (sliceHeight / 2);

            // Copy Y plane dengan stripping stride padding
            for (int y = 0; y < height; y++)
            {
                int srcOffset = y * stride;
                int dstOffset = y * width;
                if (srcOffset + width <= i420.Length)
                {
                    Buffer.BlockCopy(i420, srcOffset, nv21, dstOffset, width);
                }
            }

            // U plane dimulai setelah Y plane (dengan stride)
            int uOffset = srcYPlaneSize;
            // V plane dimulai setelah U plane
            int vOffset = srcYPlaneSize + srcUvPlaneSize;

            int uvH = height / 2;
            int uvW = width / 2;
            int uvStride = stride / 2;

            // Interleave V dan U ke NV21 format
            int dstOffset2 = outYSize;
            for (int y = 0; y < uvH; y++)
            {
                for (int x = 0; x < uvW; x++)
                {
                    int srcUIdx = uOffset + (y * uvStride) + x;
                    int srcVIdx = vOffset + (y * uvStride) + x;

                    if (srcUIdx < i420.Length && srcVIdx < i420.Length && dstOffset2 + 1 < nv21.Length)
                    {
                        nv21[dstOffset2++] = i420[srcVIdx]; // V first in NV21
                        nv21[dstOffset2++] = i420[srcUIdx]; // U second
                    }
                }
            }

            return nv21;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[I420-CONV] Exception: {ex.Message}");
            return new byte[0];
        }
    }

    /// <summary>
    /// Strip stride padding dari NV21 data (untuk format 17)
    /// </summary>
    private byte[] StripStrideFromNv21(byte[] nv21WithStride, int width, int height, int stride, int sliceHeight)
    {
        int outYSize = width * height;
        int outUvSize = width * height / 2;
        var result = new byte[outYSize + outUvSize];

        int srcYPlaneSize = stride * sliceHeight;

        // Strip Y plane
        for (int y = 0; y < height; y++)
        {
            int srcOffset = y * stride;
            int dstOffset = y * width;
            Buffer.BlockCopy(nv21WithStride, srcOffset, result, dstOffset, width);
        }

        // Strip UV plane
        int uvHeight = height / 2;
        for (int y = 0; y < uvHeight; y++)
        {
            int srcOffset = srcYPlaneSize + (y * stride);
            int dstOffset = outYSize + (y * width);
            Buffer.BlockCopy(nv21WithStride, srcOffset, result, dstOffset, width);
        }

        return result;
    }

    /// <summary>
    /// Convert NV12 ke NV21 sederhana (tanpa stride, untuk backward compatibility)
    /// </summary>
    private byte[] ConvertNv12ToNv21(byte[] nv12, int width, int height)
    {
        return ConvertNv12ToNv21WithStride(nv12, width, height, width, height);
    }

    /// <summary>
    /// Convert I420 ke NV21 sederhana (tanpa stride, untuk backward compatibility)
    /// </summary>
    private byte[] ConvertI420ToNv21(byte[] i420, int width, int height)
    {
        return ConvertI420ToNv21WithStride(i420, width, height, width, height);
    }

    /// <summary>
    /// Get timestamp in microseconds
    /// </summary>
    private long GetTimestampUs()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 1000;
    }

    /// <summary>
    /// Cleanup resources
    /// </summary>
    private void Cleanup()
    {
        try
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;

            if (_decoder != null)
            {
                try
                {
                    _decoder.Stop();
                    _decoder.Release();
                }
                catch { }
                _decoder = null;
            }

            _format?.Dispose();
            _format = null;

            while (_outputQueue.TryDequeue(out _)) { }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[H264-ANDROID] Cleanup error: {ex.Message}");
        }
    }

    #endregion

    #region IDisposable

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;

        Stop();
        GC.SuppressFinalize(this);
    }

    ~H264DecoderService()
    {
        Dispose();
    }

    #endregion

    #region Nested Classes

    private class DecodedFrame
    {
        public byte[]? Data { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime Timestamp { get; set; }
    }

    #endregion
}

#region Event Args

/// <summary>
/// Info frame yang sudah di-decode
/// </summary>
public class DecodedFrameInfo
{
    /// <summary>Lebar frame</summary>
    public int Width { get; set; }

    /// <summary>Tinggi frame</summary>
    public int Height { get; set; }
}

/// <summary>
/// Event args untuk frame H.264 yang sudah di-decode
/// </summary>
public class DecodedFrameEventArgs : EventArgs
{
    /// <summary>JPEG data hasil decode</summary>
    public byte[] JpegData { get; set; } = Array.Empty<byte>();

    /// <summary>Info frame (Width, Height)</summary>
    public DecodedFrameInfo Frame { get; set; } = new DecodedFrameInfo();

    /// <summary>Timestamp decode</summary>
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// Event args untuk error decoder
/// </summary>
public class DecoderErrorEventArgs : EventArgs
{
    /// <summary>Pesan error</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>Exception (jika ada)</summary>
    public Exception? Exception { get; set; }

    /// <summary>True jika harus fallback ke JPEG</summary>
    public bool ShouldFallbackToJpeg { get; set; }
}

#endregion
