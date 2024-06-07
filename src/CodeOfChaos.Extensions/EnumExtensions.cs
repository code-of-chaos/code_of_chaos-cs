// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Provides additional extension methods for working with enums.
/// </summary>
[UsedImplicitly]
public static class EnumExtensions {
    /// <summary>
    /// Get the values of the flagged enum.
    /// </summary>
    /// <typeparam name="T">The type of the enum.</typeparam>
    /// <param name="flagEnum">The enum to retrieve the flagged values from.</param>
    /// <returns T="containing the values of the flagged enum.">Returns an IEnumerable</returns>
    [UsedImplicitly]
    public static IEnumerable<T> GetFlaggedAsValues<T>(this T flagEnum) where T : struct, Enum {
        return Enum.GetValues<T>().Where(f => flagEnum.HasFlag(f));
    }
}
