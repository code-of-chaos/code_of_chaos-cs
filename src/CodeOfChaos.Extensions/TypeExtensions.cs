// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// Contains extension methods for the System.Type class.
/// </summary>
[UsedImplicitly]
public static class TypeExtensions {
    /// <summary>
    /// Extracts types from an enumerable of types that are assignable from the specified type.
    /// </summary>
    /// <typeparam name="T">The type to filter for.</typeparam>
    /// <param name="types">The enumerable of types.</param>
    /// <param name="allowInterfaces">True to allow interface types, false otherwise. Default is false.</param>
    /// <param name="allowAbstract">True to allow abstract types, false otherwise. Default is false.</param>
    /// <returns>An enumerable of types that are assignable from <typeparamref name="T"/>.</returns>
    [UsedImplicitly]
    public static IEnumerable<Type> ExtractByType<T>(this IEnumerable<Type> types, bool allowInterfaces = false, bool allowAbstract = false) => types
        .Where(t =>
            t == typeof(T)
            || typeof(T).IsAssignableFrom(t)
            // Defaults to return non-interface, non-abstract classes unless specifically allowed.
            && (!t.IsInterface || allowInterfaces)
            && (!t.IsAbstract || allowAbstract)
        );

    /// <summary>
    /// Extracts types from an enumerable of types that are exactly the specified type.
    /// </summary>
    /// <typeparam name="T">The type to filter for.</typeparam>
    /// <param name="types">The enumerable of types.</param>
    /// <returns>An enumerable of types that are exactly the same as <typeparamref name="T"/>.</returns>
    [UsedImplicitly]
    public static IEnumerable<Type> ExtractByTypeExact<T>(this IEnumerable<Type> types) => types
        .Where(t =>
            t == typeof(T)
        );
}
