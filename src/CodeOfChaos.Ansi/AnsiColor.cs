// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Ansi;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Provides methods for applying ANSI color codes to text.
/// </summary>
public static class AnsiColor {
    // -----------------------------------------------------------------------------------------------------------------
    // Logic
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Tries to get the color value for a given color name.
    /// </summary>
    /// <param name="colorName">The name of the color.</param>
    /// <returns>
    /// The color value as a <see cref="ByteVector3" /> object.
    /// Returns <see cref="ByteVector3.Max" /> if the color name is not found in the
    /// <see cref="AnsiColors.KnownColorsDictionary" />.
    /// </returns>
    private static ByteVector3 _tryGetColor(string colorName) =>
        !AnsiColors.KnownColorsDictionary.TryGetValue(colorName, out ByteVector3 value)
            ? ByteVector3.Max
            : value;

    // -----------------------------------------------------------------------------------------------------------------
    // String Logic
    // -----------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Wraps the specified text in the specified color, using ANSI color codes.
    /// </summary>
    /// <param name="colorName">The name of the color to use for wrapping the text.</param>
    /// <param name="text">The text to be wrapped.</param>
    /// <returns>
    /// A string that represents the wrapped text in the specified color.
    /// </returns>
    public static string F(string colorName, string? text) => Fore(colorName, text);

    /// <summary>
    /// Sets the foreground color of the console output to the specified color and formats the specified text.
    /// </summary>
    /// <param name="colorName">The name of the color to set (e.g., "red", "blue").</param>
    /// <param name="text">The text to be formatted.</param>
    /// <returns>The formatted text with the specified foreground color applied.</returns>
    public static string Fore(string colorName, string? text) => $"{AnsiCodes.RgbForegroundColor(_tryGetColor(colorName))}{text}{AnsiCodes.ResetGraphicsModes}";

    /// <summary>
    /// Sets the background color and displays the specified text using ANSI escape codes.
    /// </summary>
    /// <param name="colorName">The name of the color to set as the background. This should be a valid color name.</param>
    /// <param name="text">
    /// The text to be displayed with the specified background color. This parameter is optional and can be
    /// null.
    /// </param>
    /// <returns>
    /// A string containing ANSI escape codes for setting the specified background color and displaying the text.
    /// </returns>
    public static string B(string colorName, string? text) => Back(colorName, text);

    /// <summary>
    /// Sets the background color and displays the specified text using ANSI escape codes.
    /// </summary>
    /// <param name="colorName">The name of the color to set as the background. This should be a valid color name.</param>
    /// <param name="text">
    /// The text to be displayed with the specified background color. This parameter is optional and can be
    /// null.
    /// </param>
    /// <returns>
    /// A string containing ANSI escape codes for setting the specified background color and displaying the text.
    /// </returns>
    public static string Back(string colorName, string? text) => $"{AnsiCodes.RgbBackgroundColor(_tryGetColor(colorName))}{text}{AnsiCodes.ResetGraphicsModes}";

    /// <summary>
    /// Applies an underline format to the specified text using the specified color.
    /// </summary>
    /// <param name="colorName">The name of the color to use for the underline.</param>
    /// <param name="text">The text to apply the underline format to.</param>
    /// <returns>The text with the underline format applied.</returns>
    public static string U(string colorName, string? text) => Under(colorName, text);

    /// <summary>
    /// Applies an underline format to the specified text using the specified color.
    /// </summary>
    /// <param name="colorName">The name of the color to use for the underline.</param>
    /// <param name="text">The text to apply the underline format to.</param>
    /// <returns>The text with the underline format applied.</returns>
    public static string Under(string colorName, string? text) => $"{AnsiCodes.RgbUnderlineColor(_tryGetColor(colorName))}{text}{AnsiCodes.ResetGraphicsModes}";

    /// <summary>
    /// Sets the foreground color of the text to the specified color.
    /// </summary>
    /// <param name="colorName">The name of the color to set.</param>
    /// <returns>The ANSI escape code to set the foreground color to the specified color.</returns>
    public static string AsFore(string colorName) => $"{AnsiCodes.RgbForegroundColor(_tryGetColor(colorName))}";
    /// <summary>
    /// Sets the background color of the text to the specified color name.
    /// </summary>
    /// <param name="colorName">The name of the color.</param>
    /// <returns>
    /// A string representing the ANSI escape code for setting the background color to the specified color name.
    /// </returns>
    public static string AsBack(string colorName) => $"{AnsiCodes.RgbBackgroundColor(_tryGetColor(colorName))}";
    /// <summary>
    /// Sets the text color to the provided color name with an underline effect.
    /// </summary>
    /// <param name="colorName">The name of the color.</param>
    /// <returns>
    /// The formatted string with the specified color and underline effect.
    /// </returns>
    public static string AsUnder(string colorName) => $"{AnsiCodes.RgbUnderlineColor(_tryGetColor(colorName))}";
}
