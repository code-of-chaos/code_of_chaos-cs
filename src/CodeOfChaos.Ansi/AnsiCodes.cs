// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Ansi;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// The following class is created by using the following resources:
//      - https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797#ansi-escape-sequences
//      - https://github.com/code-of-chaos/AthenaColor (my own package)
/// <summary>
/// Provides ANSI escape codes for cursor control, erase functions, graphics modes, color codes, screen modes, and keyboard strings.
/// </summary>
public static class AnsiCodes {
    /// <summary>
    /// Represents the escape character used in ANSI escape sequences.
    /// </summary>
    private static string _escapeChar = AnsiEscapeCodes.EscapeHexadecimal;

    // -----------------------------------------------------------------------------------------------------------------
    // Cursor Controls
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Represents the ANSI escape code for moving the cursor to the home position.
    /// </summary>
    public static string CursorHome => $"{_escapeChar}[H";
    /// <summary>
    /// Represents the ANSI escape code for querying the cursor position in the terminal.
    /// </summary>
    public static string CursorPosition => $"{_escapeChar}[6n";
    /// <summary>
    /// Represents the escape code for moving the cursor up.
    /// </summary>
    public static string CursorMoveUp => $"{_escapeChar} M";
    /// <summary>
    /// Represents the ANSI escape code for saving the cursor position.
    /// </summary>
    public static string CursorPositionSaveDec => $"{_escapeChar} 7";
    /// <summary>
    /// Represents the ANSI escape sequence for restoring the cursor position.
    /// </summary>
    public static string CursorPositionRestoreDec => $"{_escapeChar} 8";
    /// <summary>
    /// Represents the ANSI escape sequence for saving the current cursor position.
    /// </summary>
    public static string CursorPositionSaveSco => $"{_escapeChar}[s";
    /// <summary>
    /// Represents the ANSI escape code for restoring the cursor position saved with Sco (Save Cursor) code.
    /// </summary>
    public static string CursorPositionRestoreSco => $"{_escapeChar}[u";

    // -----------------------------------------------------------------------------------------------------------------
    // Erase Functions
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Represents ANSI escape codes for erasing parts of the display.
    /// </summary>
    public static string EraseInDisplay => $"{_escapeChar}[J";
    /// <summary>
    /// Represents the ANSI escape code for erasing from the current cursor position to the end of the screen.
    /// </summary>
    public static string EraseFromCursorToScreenEnd => $"{_escapeChar}[0J";
    /// <summary>
    /// Erases all characters from the current cursor position to the beginning of the screen.
    /// </summary>
    public static string EraseFromCursorToScreenBeginning => $"{_escapeChar}[1J";
    /// <summary>
    /// Gets the ANSI escape sequence to erase the entire screen.
    /// </summary>
    public static string EraseEntireScreen => $"{_escapeChar}[2J";
    /// <summary>
    /// The ANSI code for erasing saved lines on the terminal screen.
    /// </summary>
    public static string EraseSavedLines => $"{_escapeChar}[3J";
    /// <summary>
    /// Represents the ANSI escape code for erasing a line.
    /// </summary>
    public static string EraseInLine => $"{_escapeChar}[K";
    /// <summary>
    /// Represents an ANSI escape code for erasing characters from the current cursor position to the end of the line.
    /// </summary>
    public static string EraseFromCursorToLineEnd => $"{_escapeChar}[0K";
    /// <summary>
    /// Erases from the beginning of the line to the cursor position.
    /// </summary>
    public static string EraseFromLineBeginningToCursor => $"{_escapeChar}[1K";
    /// <summary>
    /// Represents the ANSI escape code for erasing the entire line on the console.
    /// </summary>
    public static string EraseEntireLine => $"{_escapeChar}[2K";
    /// <summary>
    /// The ResetGraphicsModes property represents the ANSI escape code that resets all graphics modes to their default values.
    /// </summary>
    public static string ResetGraphicsModes => $"{_escapeChar}[0m";
    /// <summary>
    /// Sets the bold mode in ANSI escape codes.
    /// </summary>
    public static string SetBoldMode => $"{_escapeChar}[1m";
    /// <summary>
    /// Reset the bold mode.
    /// </summary>
    public static string ResetBoldMode => $"{_escapeChar}[22m";
    /// <summary>
    /// Sets the dim mode for the ANSI escape codes.
    /// </summary>
    /// <remarks>
    /// The dim mode reduces the intensity of the text color.
    /// </remarks>
    public static string SetDimMode => $"{_escapeChar}[2m";
    /// <summary>
    /// Represents the ANSI escape sequence to reset the dim mode.
    /// Dim mode is used to reduce the intensity of the text color.
    /// </summary>
    public static string ResetDimMode => $"{_escapeChar}[22m";
    /// <summary>
    /// Sets the italic mode for the text.
    /// </summary>
    /// <remarks>
    /// This method sets the italic mode for the text. When the italic mode is enabled, the text will be displayed in italic font style.
    /// </remarks>
    public static string SetItalicMode => $"{_escapeChar}[3m";
    /// <summary>
    /// Represents the ANSI escape code for resetting the italic mode.
    /// </summary>
    public static string ResetItalicMode => $"{_escapeChar}[23m";
    /// <summary>
    /// Sets the underline mode for text.
    /// </summary>
    /// <remarks>
    /// The underline mode adds a underline effect to the text.
    /// </remarks>
    public static string SetUnderlineMode => $"{_escapeChar}[4m";
    /// <summary>
    /// An ANSI escape code that resets the underline mode for text in a console.
    /// </summary>
    public static string ResetUnderlineMode => $"{_escapeChar}[24m";
    /// <summary>
    /// Set the blinking mode for text representation in ANSI escape codes.
    /// </summary>
    /// <remarks>
    /// This property sets the blinking mode for the text represented using ANSI escape codes.
    /// The blinking mode is used to make the text blink on the console.
    /// </remarks>
    /// <value>
    /// The ANSI escape code for setting the blinking mode.
    /// </value>
    public static string SetBlinkingMode => $"{_escapeChar}[5m";
    /// <summary>
    /// Resets the blinking mode.
    /// </summary>
    public static string ResetBlinkingMode => $"{_escapeChar}[25m";
    /// <summary>
    /// Represents the code for setting the inverse mode in ANSI escape sequences.
    /// </summary>
    public static string SetInverseMode => $"{_escapeChar}[7m";
    /// <summary>
    /// Represents the ANSI escape code for resetting the inverse mode.
    /// The inverse mode swaps the foreground and background colors, making the background become the foreground and vice versa.
    /// Use this code to reset the inverse mode after enabling it.
    /// </summary>
    public static string ResetInverseMode => $"{_escapeChar}[27m";
    /// <summary>
    /// Sets the hidden mode.
    /// </summary>
    /// <returns>The hidden mode ANSI code.</returns>
    public static string SetHiddenMode => $"{_escapeChar}[8m";
    /// <summary>
    /// Resets the hidden mode of ANSI escape codes, which hides the text.
    /// </summary>
    /// <remarks>
    /// This function resets the hidden mode of ANSI escape codes, which hides the text.
    /// It reverts the hidden mode back to the default mode, allowing the text to be visible again.
    /// </remarks>
    /// <returns>A string representing the ANSI escape code to reset the hidden mode.</returns>
    public static string ResetHiddenMode => $"{_escapeChar}[28m";
    /// <summary>
    /// Sets the strikethrough mode for text output.
    /// </summary>
    public static string SetStrikethroughMode => $"{_escapeChar}[9m";
    /// <summary>
    /// Resets the strikethrough mode.
    /// </summary>
    public static string ResetStrikethroughMode => $"{_escapeChar}[29m";
    /// <summary>
    /// Represents the ANSI escape code for setting the foreground color to black.
    /// </summary>
    public static string CommonForegroundBlack => $"{_escapeChar}[30m";
    /// <summary>
    /// Represents the ANSI escape code for setting the foreground color to red.
    /// </summary>
    public static string CommonForegroundRed => $"{_escapeChar}[31m";
    /// <summary>
    /// Represents the ANSI escape code for setting the foreground color to green.
    /// </summary>
    public static string CommonForegroundGreen => $"{_escapeChar}[32m";
    /// <summary>
    /// Represents the ANSI escape code for setting the foreground color to yellow.
    /// </summary>
    public static string CommonForegroundYellow => $"{_escapeChar}[33m";
    /// <summary>
    /// Represents the ANSI escape code for setting the foreground color to blue.
    /// </summary>
    public static string CommonForegroundBlue => $"{_escapeChar}[34m";
    /// <summary>
    /// Represents the ANSI escape code for setting the foreground color to magenta in a console.
    /// </summary>
    public static string CommonForegroundMagenta => $"{_escapeChar}[35m";
    /// <summary>
    /// Represents the ANSI escape code for setting the foreground color to cyan.
    /// </summary>
    public static string CommonForegroundCyan => $"{_escapeChar}[36m";
    /// <summary>
    /// Represents the ANSI escape code for setting the foreground color to white (default).
    /// </summary>
    public static string CommonForegroundWhite => $"{_escapeChar}[37m";
    /// <summary>
    /// Represents the ANSI escape code for the default common foreground color.
    /// </summary>
    public static string CommonForegroundDefault => $"{_escapeChar}[39m";

    /// <summary>
    /// Represents the ANSI escape code for setting the background color to black.
    /// </summary>
    public static string CommonBackgroundBlack => $"{_escapeChar}[40m";
    /// <summary>
    /// Represents the ANSI escape code for setting the background color to red.
    /// </summary>
    public static string CommonBackgroundRed => $"{_escapeChar}[41m";
    /// <summary>
    /// Represents the ANSI escape code for setting the background color to green.
    /// </summary>
    public static string CommonBackgroundGreen => $"{_escapeChar}[42m";
    /// <summary>
    /// Provides the ANSI escape code for setting the background color to yellow.
    /// </summary>
    public static string CommonBackgroundYellow => $"{_escapeChar}[43m";
    /// <summary>
    /// Represents the ANSI escape code for setting the background color to Blue.
    /// </summary>
    public static string CommonBackgroundBlue => $"{_escapeChar}[44m";
    /// <summary>
    /// Represents the ANSI escape code for setting the text background color to Magenta.
    /// </summary>
    public static string CommonBackgroundMagenta => $"{_escapeChar}[45m";
    /// <summary>
    /// Represents the ANSI escape code for setting the background color to cyan.
    /// </summary>
    /// <remarks>
    /// This property is a member of the <see cref="AnsiCodes"/> class.
    /// The ANSI escape code for setting the background color to cyan is <c>\x1B[46m</c>.
    /// </remarks>
    public static string CommonBackgroundCyan => $"{_escapeChar}[46m";
    /// <summary>
    /// Represents the ANSI escape code for setting the common background color to white.
    /// </summary>
    public static string CommonBackgroundWhite => $"{_escapeChar}[47m";
    /// <summary>
    /// Represents the ANSI escape sequence for setting the default background color.
    /// </summary>
    public static string CommonBackgroundDefault => $"{_escapeChar}[49m";

    // -----------------------------------------------------------------------------------------------------------------
    // Common private modes
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Represents the ANSI code for hiding the cursor.
    /// </summary>
    public static string CursorInvisible => $"{_escapeChar}[?25l";
    /// <summary>
    /// Represents the cursor visible property.
    /// </summary>
    public static string CursorVisible => $"{_escapeChar}[?25h";
    /// <summary>
    /// Represents the ANSI escape code for restoring the screen.
    /// </summary>
    public static string RestoreScreen => $"{_escapeChar}[?47l";
    /// <summary>
    /// Represents a property that returns the ANSI escape code for saving the screen.
    /// </summary>
    public static string SaveScreen => $"{_escapeChar}[?47h";
    /// <summary>
    /// Enables the alternative buffer mode.
    /// </summary>
    /// <remarks>
    /// The alternative buffer mode provides a separate screen buffer for displaying information.
    /// This can be useful for displaying temporary or auxiliary information without disrupting the
    /// main screen buffer.
    /// </remarks>
    /// <seealso cref="AnsiCodes.EnableAlternativeBuffer"/>
    public static string EnableAlternativeBuffer => $"{_escapeChar}[?1049h";
    /// <summary>
    /// Represents the ANSI escape code for disabling the alternative buffer mode.
    /// </summary>
    public static string DisableAlternativeBuffer => $"{_escapeChar}[?1049l";
    /// <summary>
    /// Changes the escape character used in the ANSI escape codes.
    /// </summary>
    /// <param name="escape">The new escape character to be used.</param>
    public static void ChangeEscapeChar(string escape) => _escapeChar = escape;
    /// <summary>
    /// Moves the cursor to the specified position in the console output buffer.
    /// </summary>
    /// <param name="line">The line number to move the cursor to. The first line is 1.</param>
    /// <param name="column">The column number to move the cursor to. The first column is 1.</param>
    /// <returns>
    /// The ANSI escape code to move the cursor to the specified position.
    /// </returns>
    public static string CursorMoveH(int line, int column) => $"{_escapeChar}[{line};{column}H";
    /// <summary>
    /// Moves the cursor to the specified line and column position.
    /// </summary>
    /// <param name="line">The line number to move the cursor to.</param>
    /// <param name="column">The column number to move the cursor to.</param>
    /// <returns>
    /// A string representing the ANSI escape sequence to move the cursor to the specified position.
    /// </returns>
    public static string CursorMoveF(int line, int column) => $"{_escapeChar}[{line};{column}f";
    /// <summary>
    /// Moves the cursor up a specified number of lines.
    /// </summary>
    /// <param name="lines">The number of lines to move the cursor up.</param>
    /// <returns></returns>
    public static string CursorMoveUpLines(int lines) => $"{_escapeChar}[{lines}A";
    /// <summary>
    /// Moves the cursor down the specified number of lines.
    /// </summary>
    /// <param name="lines">The number of lines to move the cursor down.</param>
    /// <returns></returns>
    public static string CursorMoveDownLines(int lines) => $"{_escapeChar}[{lines}B";
    /// <summary>
    /// Moves the cursor right by the specified number of columns.
    /// </summary>
    /// <param name="columns">The number of columns to move the cursor right.</param>
    /// <returns>The ANSI escape code to move the cursor right.</returns>
    public static string CursorMoveRightColumns(int columns) => $"{_escapeChar}[{columns}C";
    /// <summary>
    /// Moves the cursor to the left by the specified number of columns.
    /// </summary>
    /// <param name="columns">The number of columns to move the cursor to the left.</param>
    /// <returns>The ANSI escape sequence to move the cursor to the left by the specified number of columns.</returns>
    public static string CursorMoveLeftColumns(int columns) => $"{_escapeChar}[{columns}D";
    /// <summary>
    /// Moves the cursor to the beginning of the next line.
    /// </summary>
    /// <param name="lines">The number of lines to move down. Positive values move down, negative values move up.</param>
    /// <returns>A string that represents the ANSI escape code for moving the cursor to the beginning of the next line.</returns>
    public static string CursorMoveBeginningNextLine(int lines) => $"{_escapeChar}[{lines}E";
    /// <summary>
    /// Moves the cursor to the beginning of the previous line.
    /// </summary>
    /// <param name="lines">The number of lines to move the cursor up.</param>
    /// <returns>The ANSI escape code to move the cursor to the beginning of the previous line.</returns>
    public static string CursorMoveBeginningPreviousLine(int lines) => $"{_escapeChar}[{lines}F";
    /// <summary>
    /// Moves the cursor to the specified column position.
    /// </summary>
    /// <param name="columns">The column position to move the cursor to.</param>
    /// <returns>The ANSI escape sequence to move the cursor to the specified column position.</returns>
    public static string CursorMoveColumn(int columns) => $"{_escapeChar}[{columns}G";

    // -----------------------------------------------------------------------------------------------------------------
    // Colors / Graphics Mode
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Set the graphics modes.
    /// </summary>
    /// <param name="codes">The graphics mode codes.</param>
    /// <returns>The ANSI escape sequence to set the specified graphics modes.</returns>
    public static string SetGraphicsModes(params int[] codes) => $"{_escapeChar}[{string.Join(";", codes)}m";

    // -----------------------------------------------------------------------------------------------------------------
    // Color codes
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Sets the foreground color using a byte value.
    /// </summary>
    /// <param name="color">The byte value representing the color.</param>
    /// <returns>The ANSI escape sequence for setting the foreground color.</returns>
    public static string ByteForegroundColor(byte color) => $"{_escapeChar}[38;5;{(int)color}m";
    /// <summary>
    /// Sets the background color of the console using the given byte color value.
    /// </summary>
    /// <param name="color">The byte color value representing the background color of the console.</param>
    /// <returns>The ANSI escape code for setting the background color.</returns>
    public static string ByteBackgroundColor(byte color) => $"{_escapeChar}[48;5;{(int)color}m";
    /// <summary>
    /// Sets the underline color using the ANSI escape code.
    /// </summary>
    /// <param name="color">The color to set the underline to. Valid values are 0-255.</param>
    /// <returns>The ANSI escape code to set the underline color.</returns>
    public static string ByteUnderlineColor(byte color) => $"{_escapeChar}[58;5;{(int)color}m";

    /// <summary>
    /// Sets the foreground color of the console using RGB values.
    /// </summary>
    /// <param name="color">The RGB color value.</param>
    /// <returns>The ANSI escape sequence for setting the foreground color.</returns>
    public static string RgbForegroundColor(ByteVector3 color) => $"{_escapeChar}[38;2;{color.ToAnsiString()}m";
    /// <summary>
    /// Sets the RGB background color using ANSI escape codes.
    /// </summary>
    /// <param name="color">The RGB color in ByteVector3 format.</param>
    /// <returns>The ANSI escape code string to set the RGB background color.</returns>
    public static string RgbBackgroundColor(ByteVector3 color) => $"{_escapeChar}[48;2;{color.ToAnsiString()}m";
    /// <summary>
    /// Sets the RGB underline color for the text.
    /// </summary>
    /// <param name="color">The RGB color values.</param>
    /// <returns>The ANSI escape sequence to set the RGB underline color for the text.</returns>
    public static string RgbUnderlineColor(ByteVector3 color) => $"{_escapeChar}[58;2;{color.ToAnsiString()}m";

    // -----------------------------------------------------------------------------------------------------------------
    // Screen Modes
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Sets the screen mode.
    /// </summary>
    /// <param name="value">The value of the screen mode.</param>
    /// <returns>A string that represents the screen mode.</returns>
    public static string SetScreenMode(int value) => $"{_escapeChar}[={value}h";
    /// <summary>
    /// Resets the screen mode to the specified value.
    /// </summary>
    /// <param name="value">The value to set the screen mode to.</param>
    /// <returns>A string containing the ANSI escape code to reset the screen mode.</returns>
    public static string ResetScreenMode(int value) => $"{_escapeChar}[={value}l";

    // -----------------------------------------------------------------------------------------------------------------
    // Keyboard Strings
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Redefines a key to a code.
    /// </summary>
    /// <param name="code">The code of the key.</param>
    /// <param name="str">The string representation of the key.</param>
    /// <returns>
    /// A string that represents the redefined key with the provided code and string representation.
    /// </returns>
    public static string RedefineKeyToCode(string code, string str) => $"{_escapeChar}[{code};{str}p";
}
