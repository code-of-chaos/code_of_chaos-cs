// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Ansi;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// A utility class containing ANSI escape codes for terminal control.
/// </summary>
public static class AnsiEscapeCodes {
    /// <summary>
    /// A utility class containing ANSI escape codes for terminal control.
    /// </summary>
    /// <remarks>
    /// This class provides constants for common ANSI escape codes used in terminal control.
    /// The <c>EscapeCtrl</c> constant represents the escape control character.
    /// </remarks>
    public const string EscapeCtrl = "^[";

    /// <summary>
    /// A utility class containing ANSI escape codes for terminal control.
    /// </summary>
    public const string EscapeOctal = "\033";

    /// <summary>
    /// A utility class containing ANSI escape codes for terminal control.
    /// </summary>
    public const string EscapeUnicode = "\u001b";
    
    /// <summary>
    /// A utility class containing HEXADECIMAL escape codes for terminal control.
    /// </summary>
    public const string EscapeHexadecimal = "\x1B";
}
