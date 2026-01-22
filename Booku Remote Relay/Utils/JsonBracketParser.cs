using System.Text;

namespace BookuRemoteRelay.Utils;

/// <summary>
/// Parser untuk JSON tanpa delimiter menggunakan bracket tracking.
/// Protokol Booku Remote tidak menggunakan newline delimiter.
/// </summary>
public static class JsonBracketParser
{
    /// <summary>
    /// Mencari akhir objek JSON pertama dalam buffer.
    /// Menggunakan bracket tracking untuk mendeteksi batas JSON.
    /// </summary>
    /// <param name="buffer">Buffer bytes yang berisi data JSON.</param>
    /// <param name="startIndex">Index mulai pencarian.</param>
    /// <returns>Index karakter '}' terakhir dari objek JSON, atau -1 jika belum lengkap.</returns>
    public static int FindJsonEnd(byte[] buffer, int startIndex = 0)
    {
        if (buffer == null || buffer.Length == 0) return -1;

        int bracketCount = 0;
        bool inString = false;
        bool escapeNext = false;
        bool foundStart = false;

        for (int i = startIndex; i < buffer.Length; i++)
        {
            char ch = (char)buffer[i];

            // Handle escape sequence
            if (escapeNext)
            {
                escapeNext = false;
                continue;
            }

            // Backslash starts escape sequence
            if (ch == '\\' && inString)
            {
                escapeNext = true;
                continue;
            }

            // Toggle string mode on quote
            if (ch == '"')
            {
                inString = !inString;
                continue;
            }

            // Skip if inside string
            if (inString) continue;

            // Track brackets
            if (ch == '{')
            {
                bracketCount++;
                foundStart = true;
            }
            else if (ch == '}')
            {
                bracketCount--;
                if (foundStart && bracketCount == 0)
                {
                    return i; // Found complete JSON object
                }
            }
        }

        return -1; // JSON belum lengkap
    }

    /// <summary>
    /// Extract satu objek JSON dari buffer.
    /// </summary>
    /// <param name="buffer">Buffer yang berisi data.</param>
    /// <param name="jsonString">Output: string JSON yang diekstrak.</param>
    /// <param name="consumedBytes">Output: jumlah bytes yang dikonsumsi.</param>
    /// <returns>True jika berhasil extract JSON lengkap.</returns>
    public static bool TryExtractJson(List<byte> buffer, out string jsonString, out int consumedBytes)
    {
        jsonString = "";
        consumedBytes = 0;

        if (buffer == null || buffer.Count == 0) return false;

        var bufferArray = buffer.ToArray();
        int endIndex = FindJsonEnd(bufferArray, 0);

        if (endIndex == -1) return false;

        // Extract JSON string
        consumedBytes = endIndex + 1;
        jsonString = Encoding.UTF8.GetString(bufferArray, 0, consumedBytes);

        return true;
    }

    /// <summary>
    /// Extract semua objek JSON lengkap dari buffer.
    /// </summary>
    /// <param name="buffer">Buffer yang berisi data.</param>
    /// <returns>List string JSON yang berhasil diekstrak.</returns>
    public static List<string> ExtractAllJson(List<byte> buffer)
    {
        var results = new List<string>();

        while (buffer.Count > 0)
        {
            if (TryExtractJson(buffer, out string json, out int consumed))
            {
                results.Add(json);
                buffer.RemoveRange(0, consumed);
            }
            else
            {
                break; // Tidak ada JSON lengkap lagi
            }
        }

        return results;
    }
}
