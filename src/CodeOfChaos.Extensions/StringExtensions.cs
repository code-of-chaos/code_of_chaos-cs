// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Provides extension methods for the string class.
/// </summary>
[UsedImplicitly]
public static class StringExtensions {
    /// <summary>
    /// Checks if a string is not null or empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>Returns true if the string is not null or empty, otherwise false.</returns>
    [UsedImplicitly]
    public static bool IsNotNullOrEmpty(this string? str) => !string.IsNullOrEmpty(str);
    
    /// <summary>
    /// Checks if a string is null or empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>Returns true if the string is null or empty, otherwise false.</returns>
    [UsedImplicitly]
    public static bool IsNullOrEmpty(this string? str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// Determines whether the given array of strings is empty.
    /// </summary>
    /// <param name="arr">The array of strings to check.</param>
    /// <returns>Returns true if the array is empty; otherwise, false.</returns>
    [UsedImplicitly]
    public static bool IsEmpty(this string[] arr) => arr.Length == 0;

    /// <summary>
    /// Checks if the given string array is empty.
    /// </summary>
    /// <param name="arr">The string array to check.</param>
    /// <returns>True if the string array is empty, false otherwise.</returns>
    [UsedImplicitly]
    public static bool IsEmpty(this IEnumerable<string> arr) => !arr.Any();
}
