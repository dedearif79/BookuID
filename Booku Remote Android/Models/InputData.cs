using System.Text.Json.Serialization;

namespace BookuRemoteAndroid.Models;

/// <summary>
/// Data input keyboard untuk dikirim ke Host
/// </summary>
public class InputKeyboard
{
    [JsonPropertyName("keyCode")]
    public int KeyCode { get; set; }

    [JsonPropertyName("isKeyDown")]
    public bool IsKeyDown { get; set; }

    [JsonPropertyName("isExtended")]
    public bool IsExtended { get; set; }

    [JsonPropertyName("modifiers")]
    public int Modifiers { get; set; }

    /// <summary>
    /// Membuat input keyboard
    /// </summary>
    public static InputKeyboard Create(int keyCode, bool isKeyDown, bool isExtended = false)
    {
        return new InputKeyboard
        {
            KeyCode = keyCode,
            IsKeyDown = isKeyDown,
            IsExtended = isExtended,
            Modifiers = 0
        };
    }
}

/// <summary>
/// Data input mouse untuk dikirim ke Host
/// </summary>
public class InputMouse
{
    [JsonPropertyName("tipeAksi")]
    public int TipeAksi { get; set; }

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    [JsonPropertyName("button")]
    public int Button { get; set; }

    [JsonPropertyName("isButtonDown")]
    public bool IsButtonDown { get; set; }

    [JsonPropertyName("wheelDelta")]
    public int WheelDelta { get; set; }

    /// <summary>
    /// Membuat input mouse move
    /// </summary>
    public static InputMouse CreateMove(double normalizedX, double normalizedY)
    {
        return new InputMouse
        {
            TipeAksi = (int)TipeAksiMouse.PINDAH,
            X = normalizedX,
            Y = normalizedY,
            Button = 0,
            IsButtonDown = false,
            WheelDelta = 0
        };
    }

    /// <summary>
    /// Membuat input mouse click
    /// </summary>
    public static InputMouse CreateClick(double normalizedX, double normalizedY, TombolMouse button, bool isDown)
    {
        return new InputMouse
        {
            TipeAksi = (int)TipeAksiMouse.KLIK,
            X = normalizedX,
            Y = normalizedY,
            Button = (int)button,
            IsButtonDown = isDown,
            WheelDelta = 0
        };
    }

    /// <summary>
    /// Membuat input mouse wheel
    /// </summary>
    public static InputMouse CreateWheel(double normalizedX, double normalizedY, int delta)
    {
        return new InputMouse
        {
            TipeAksi = (int)TipeAksiMouse.RODA,
            X = normalizedX,
            Y = normalizedY,
            Button = 0,
            IsButtonDown = false,
            WheelDelta = delta
        };
    }
}

/// <summary>
/// Helper untuk mapping Android keycode ke Windows Virtual Key Code
/// </summary>
public static class KeyCodeMapper
{
    // Windows Virtual Key Codes
    public const int VK_BACK = 8;
    public const int VK_TAB = 9;
    public const int VK_RETURN = 13;
    public const int VK_SHIFT = 16;
    public const int VK_CONTROL = 17;
    public const int VK_MENU = 18;      // Alt
    public const int VK_ESCAPE = 27;
    public const int VK_SPACE = 32;
    public const int VK_PRIOR = 33;     // Page Up
    public const int VK_NEXT = 34;      // Page Down
    public const int VK_END = 35;
    public const int VK_HOME = 36;
    public const int VK_LEFT = 37;
    public const int VK_UP = 38;
    public const int VK_RIGHT = 39;
    public const int VK_DOWN = 40;
    public const int VK_INSERT = 45;
    public const int VK_DELETE = 46;

    // Numbers 0-9 = 48-57
    // Letters A-Z = 65-90

    public const int VK_F1 = 112;
    public const int VK_F2 = 113;
    public const int VK_F3 = 114;
    public const int VK_F4 = 115;
    public const int VK_F5 = 116;
    public const int VK_F6 = 117;
    public const int VK_F7 = 118;
    public const int VK_F8 = 119;
    public const int VK_F9 = 120;
    public const int VK_F10 = 121;
    public const int VK_F11 = 122;
    public const int VK_F12 = 123;

    /// <summary>
    /// Mapping karakter ke Windows Virtual Key Code
    /// </summary>
    public static (int keyCode, bool isExtended) MapCharToVK(char c)
    {
        // Letters (case insensitive)
        if (c >= 'a' && c <= 'z')
            return (65 + (c - 'a'), false);
        if (c >= 'A' && c <= 'Z')
            return (65 + (c - 'A'), false);

        // Numbers
        if (c >= '0' && c <= '9')
            return (48 + (c - '0'), false);

        // Special characters
        return c switch
        {
            ' ' => (VK_SPACE, false),
            '\n' or '\r' => (VK_RETURN, false),
            '\t' => (VK_TAB, false),
            _ => (0, false)
        };
    }

    /// <summary>
    /// Extended keys yang memerlukan flag isExtended = true
    /// </summary>
    public static bool IsExtendedKey(int vkCode)
    {
        return vkCode switch
        {
            VK_INSERT or VK_DELETE or VK_HOME or VK_END or
            VK_PRIOR or VK_NEXT or VK_LEFT or VK_UP or
            VK_RIGHT or VK_DOWN => true,
            _ => false
        };
    }
}
