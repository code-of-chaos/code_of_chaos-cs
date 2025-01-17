﻿// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

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
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? str) => !string.IsNullOrEmpty(str);
    
    /// <summary>
    /// Checks if a string is null or empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>Returns true if the string is null or empty, otherwise false.</returns>
    [UsedImplicitly]
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);
    
    
    /// <summary>
    /// Checks if a string is not null or whitespace.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>Returns true if the string is null or empty, otherwise false.</returns>
    [UsedImplicitly]
    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? str) => !string.IsNullOrWhiteSpace(str);
    
    /// <summary>
    /// Checks if a string is null or whitespace.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <returns>Returns true if the string is null or empty, otherwise false.</returns>
    [UsedImplicitly]
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);
    

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

    /// <summary>
    /// Truncates the input string to the specified maximum length.
    /// </summary>
    /// <param name="input">The string to be truncated.</param>
    /// <param name="maxLength">The maximum length of the truncated string.</param>
    /// <returns>Returns the truncated string if the input exceeds the maximum length; otherwise, returns the original string.</returns>
    [UsedImplicitly]
    public static string Truncate(this string input, int maxLength) =>  input.Length <= maxLength ? input : input[..maxLength];
}
