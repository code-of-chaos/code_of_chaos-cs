// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Ansi;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents a three-dimensional vector with byte components.
/// </summary>
public struct ByteVector3(int x, int y, int z) {
    /// <summary>
    /// Represents the X component of a three-dimensional vector with byte components.
    /// </summary>
    /// <value>
    /// The X component of the vector.
    /// </value>
    public byte X { get; } = (byte)x;
    /// <summary>
    /// Represents the Y component of a three-dimensional vector with byte components.
    /// </summary>
    /// <value>
    /// The Y component of a <see cref="ByteVector3"/> object.
    /// </value>
    public byte Y { get; } = (byte)y;
    /// <summary>
    /// Represents the Z component of a three-dimensional vector with byte components.
    /// </summary>
    /// <value>
    /// A byte value that represents the Z component of the vector.
    /// </value>
    public byte Z { get; } = (byte)z;
    // -----------------------------------------------------------------------------------------------------------------
    // Instance creators
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Represents a zero vector in 3D space.
    /// </summary>
    /// <value>
    /// A <see cref="ByteVector3" /> object with all components set to zero.
    /// </value>
    public static ByteVector3 Zero => new(0, 0, 0);

    /// <summary>
    /// Represents the maximum possible value for each component of a ByteVector3 object.
    /// </summary>
    /// <value>
    /// A ByteVector3 object with each component set to 255.
    /// </value>
    public static ByteVector3 Max => new(255, 255, 255);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Converts the current object to an array of bytes.
    /// </summary>
    /// <returns>An array of bytes representing the object.</returns>
    public byte[] ToArray() => [X, Y, Z];

    /// <summary>
    /// Converts the current instance to a string representation using ANSI format.
    /// </summary>
    /// <returns>A string that represents the current instance with ANSI format.</returns>
    public string ToAnsiString() => $"{X};{Y};{Z}";

    /// <summary>
    /// Converts the X, Y, and Z values of the object to an RGB string representation.
    /// </summary>
    /// <returns>
    /// A string representation of the RGB values in the format "rgb(X,Y,Z)".
    /// </returns>
    public string ToRgbString() => $"rgb({X},{Y},{Z})";
}
