using System.Security.Cryptography;

namespace BookuRemoteRelay.Utils;

/// <summary>
/// Generator untuk HostCode unik.
/// </summary>
public static class HostCodeGenerator
{
    // Karakter yang digunakan (tanpa karakter ambigu: 0/O, 1/I/L)
    private const string Characters = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";

    /// <summary>
    /// Generate HostCode baru (6 karakter).
    /// </summary>
    public static string Generate(int length = 6)
    {
        var bytes = new byte[length];
        RandomNumberGenerator.Fill(bytes);

        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = Characters[bytes[i] % Characters.Length];
        }

        return new string(result);
    }

    /// <summary>
    /// Validasi format HostCode.
    /// </summary>
    public static bool IsValid(string code)
    {
        if (string.IsNullOrEmpty(code)) return false;
        if (code.Length != 6) return false;

        foreach (var c in code.ToUpper())
        {
            if (!Characters.Contains(c)) return false;
        }

        return true;
    }

    /// <summary>
    /// Normalisasi HostCode (uppercase, trim).
    /// </summary>
    public static string Normalize(string code)
    {
        return code?.Trim().ToUpper() ?? "";
    }
}
