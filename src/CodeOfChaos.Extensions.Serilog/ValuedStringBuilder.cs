// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace CodeOfChaos.Extensions.Serilog;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// A class for building strings with the ability to insert values.
/// </summary>
public class ValuedStringBuilder {
    /// <summary>
    /// Represents a builder class for creating formatted strings with associated property values.
    /// </summary>
    private readonly List<object?> _propertyValues = [];
    
    /// <summary>
    /// Represents a mutable string of characters with support for appending formatted values.
    /// </summary>
    private readonly StringBuilder _stringBuilder = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Appends a formatted text to the StringBuilder, using the provided objects as values for the placeholders.
    /// </summary>
    /// <param name="text">The text to be appended.</param>
    /// <param name="destruct">Indicates whether to apply destruct formatting to the text.</param>
    /// <param name="objects">The values to be used for the placeholders.</param>
    private void Valued(string text, bool destruct, object?[] objects) {
        string at = destruct ? "@" : "";

        _stringBuilder.Append(text);
        _stringBuilder.AppendJoin(" ", objects.Select((_, i) => $"{{{at}args{_propertyValues.Count + i}}}"));

        _propertyValues.AddRange(objects);
    }

    /// <summary>
    /// Appends a new line to the StringBuilder and formats and appends the specified text along with optional values.
    /// </summary>
    /// <param name="text">The text to append.</param>
    /// <param name="destruct">A boolean indicating whether to automatically destruct the values.</param>
    /// <param name="objects">The optional values to format and append.</param>
    /// <returns>A reference to the current instance of ValuedStringBuilder.</returns>
    public ValuedStringBuilder AppendLineValued(string text, bool destruct, params object?[] objects) {
        Valued(text, destruct, objects);
        _stringBuilder.AppendLine();
        return this;
    }

    /// <summary>
    /// Appends a line of text followed by formatted values to the ValuedStringBuilder object.
    /// </summary>
    /// <param name="text">The text to be appended.</param>
    /// <param name="objects">The values to be formatted and appended.</param>
    /// <returns>The ValuedStringBuilder object.</returns>
    public ValuedStringBuilder AppendLineValued(string text, params object?[] objects) => AppendLineValued(text, false, objects);

    /// <summary>
    /// Appends a formatted string with optional interpolated values to the ValuedStringBuilder.
    /// </summary>
    /// <param name="text">The text to append.</param>
    /// <param name="destruct">A flag indicating if the string should be interpolated with destructuring syntax.</param>
    /// <param name="objects">The optional values to interpolate into the string.</param>
    /// <returns>A reference to the ValuedStringBuilder instance to enable method chaining.</returns>
    public ValuedStringBuilder AppendValued(string text, bool destruct, params object?[] objects) {
        Valued(text, destruct, objects);
        return this;
    }

    /// <summary>
    /// Appends a formatted string to the <see cref="ValuedStringBuilder"/> with an optional array of values.
    /// </summary>
    /// <param name="text">The format string to append.</param>
    /// <para><c>true</c> to use the string interpolation symbol "@" before the format string; otherwise, <c>false</c>.</para>
    /// <para>When <c>true</c>, the format string will be treated as a verbatim string literal, preserving escape sequences.</para>
    /// <param name="objects">An optional array of values to format and append to the <see cref="ValuedStringBuilder"/>.</param>
    /// <returns>Returns the <see cref="ValuedStringBuilder"/> instance.</returns>
    public ValuedStringBuilder AppendValued(string text, params object?[] objects) => AppendValued(text, false, objects);


    /// <summary>
    /// Converts the stored property values to an array.
    /// </summary>
    /// <returns>An array of property values.</returns>
    public object?[] ValuesToArray() => _propertyValues.ToArray();

    // -----------------------------------------------------------------------------------------------------------------
    // String builder quick and dirty methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Appends a value to the ValuedStringBuilder.
    /// </summary>
    /// <param name="value">The value to append.</param>
    /// <returns>The ValuedStringBuilder object.</returns>
    public ValuedStringBuilder Append(object? value) {
        _stringBuilder.Append(value);
        return this;
    }

    /// <summary>
    /// Appends a new line to the current string.
    /// </summary>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    public ValuedStringBuilder AppendLine() {
        _stringBuilder.AppendLine(Environment.NewLine);
        return this;
    }

    /// <summary>
    /// Appends a line to the StringBuilder.
    /// </summary>
    /// <param name="value">The line to append.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    public ValuedStringBuilder AppendLine(string? value) {
        _stringBuilder.AppendLine(value);
        return this;
    }

    /// <summary>
    /// Returns the string representation of the ValuedStringBuilder instance.
    /// </summary>
    /// <returns>The string representation of the ValuedStringBuilder instance.</returns>
    public override string ToString() {
        return _stringBuilder.ToString();
    }
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="trimEnd">A boolean indicating whether to remove the trailing newline</param>
    public string ToString(bool trimEnd) {
        return _stringBuilder.ToString().TrimEnd('\n', '\r');
    }
}