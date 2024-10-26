// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// Contains extension methods for the System.Type class.
/// </summary>
public static class TypeExtensions {
    /// <summary>
    /// Extracts types from an enumerable of types that are assignable from the specified type.
    /// </summary>
    /// <typeparam name="T">The type to filter for.</typeparam>
    /// <param name="types">The enumerable of types.</param>
    /// <param name="allowInterfaces">True to allow interface types, false otherwise. Default is false.</param>
    /// <param name="allowAbstract">True to allow abstract types, false otherwise. Default is false.</param>
    /// <returns>An enumerable of types that are assignable from <typeparamref name="T"/>.</returns>
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
    public static IEnumerable<Type> ExtractByTypeExact<T>(this IEnumerable<Type> types) => types
        .Where(t =>
            t == typeof(T)
        );


    /// <summary>
    /// Determines whether the specified type derives from a given base type.
    /// </summary>
    /// <typeparam name="T">The base type to check against.</typeparam>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the specified type derives from the base type <typeparamref name="T"/>; otherwise, false.</returns>
    public static bool IsSubclassOf<T>(this Type type) => type.IsSubclassOf(typeof(T));


    /// <summary>
    /// Tries to get a custom attribute of the specified type from the given type.
    /// </summary>
    /// <typeparam name="T">The type of the custom attribute to retrieve.</typeparam>
    /// <param name="type">The type from which to retrieve the custom attribute.</param>
    /// <param name="attribute">When this method returns, contains the custom attribute of type <typeparamref name="T"/> if found; otherwise, null.</param>
    /// <returns>True if a custom attribute of type <typeparamref name="T"/> is found; otherwise, false.</returns>
    public static bool TryGetCustomAttribute<T>(this Type type, [NotNullWhen(true)] out T? attribute) where T : Attribute {
        attribute = type.GetCustomAttribute<T>();
        return attribute != null;
    }

    /// <summary>
    /// Retrieves a custom attribute of type <typeparamref name="T"/> from the given type, or returns a default instance of <typeparamref name="T"/> if the attribute is not found.
    /// </summary>
    /// <typeparam name="T">The type of the attribute to retrieve.</typeparam>
    /// <param name="type">The type to inspect for the custom attribute.</param>
    /// <param name="action">A function that provides an instance of <typeparamref name="T"/> if the attribute is not found. Can be null.</param>
    /// <returns>An instance of the custom attribute of type <typeparamref name="T"/>, or a default instance if the attribute is not found.</returns>
    public static T GetCustomAttributeOrDefault<T>(this Type type, Func<T>? action = null) where T : Attribute, new() {
        if (type.GetCustomAttribute<T>() is {} attribute) return attribute;
        return action is not null 
            ? action() 
            : new T();
    }
}
