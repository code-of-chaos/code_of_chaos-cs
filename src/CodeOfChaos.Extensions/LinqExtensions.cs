// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Provides extension methods for LINQ queries.
/// </summary>
[UsedImplicitly]
public static class LinqExtensions {
    /// <summary>
    /// Filters the elements of an IEnumerable based on a specified condition, only if the condition is true.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the IEnumerable.</typeparam>
    /// <param name="source">The IEnumerable to filter.</param>
    /// <param name="condition">The condition to apply.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>
    /// An IEnumerable that contains elements from the input sequence that satisfy the condition if the condition is true;
    /// otherwise, it returns the input IEnumerable unchanged.
    /// </returns>
    [UsedImplicitly]
    public static IEnumerable<T> ConditionalWhere<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate) =>
        condition
            ? source.Where(predicate)
            : source;
    
    /// <summary>
    /// Filters the elements of an IEnumerable based on a specified condition if the condition is true, otherwise returns all elements.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the IEnumerable.</typeparam>
    /// <param name="source">The IEnumerable to filter.</param>
    /// <param name="condition">The condition to determine whether to apply the filter or not.</param>
    /// <param name="predicate">The predicate function used to filter the elements.</param>
    /// <returns>
    /// An IEnumerable that contains the elements from the input sequence that satisfy the specified condition, if the condition is true; otherwise, returns all elements.
    /// </returns>
    [UsedImplicitly]
    public static IEnumerable<T> ConditionalWhere<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate) =>
        condition
            ? source.Where(predicate)
            : source;

    /// <summary>
    /// Iterates over the elements of an IEnumerable and performs an action on each element.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the IEnumerable.</typeparam>
    /// <param name="array">The IEnumerable to iterate over.</param>
    /// <param name="action">The action to perform on each element.</param>
    /// <remarks>
    /// This method can be used to iterate over an IEnumerable and perform a specified action on each element.
    /// The action is performed in the order the elements appear in the IEnumerable.
    /// </remarks>
    [UsedImplicitly]
    public static void IterateOver<T>(this IEnumerable<T> array, Action<T> action) where T : notnull {
        array.ToList().ForEach(action);
    }

    /// <summary>
    /// Iterates over an array and performs the specified action on each element.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The array to iterate over.</param>
    /// <param name="action">The action to perform on each element.</param>
    /// <remarks>
    /// This method converts the array to a <see cref="List{T}"/> and then iterates over it,
    /// calling the specified action on each element.
    /// </remarks>
    [UsedImplicitly]
    public static void IterateOver<T>(this IEnumerable<T> array, Func<T, T> action) where T : notnull {
        array.ToList().ForEach(a => action(a));
    }

    // /// <summary>
    // /// Iterates over an array and performs the specified action on each element.
    // /// </summary>
    // /// <typeparam name="T">The type of elements in the array.</typeparam>
    // /// <param name="linkedList">The array to iterate over.</param>
    // /// <param name="action">The action to perform on each element of the array.</param>
    // /// <exception cref="ArgumentNullException">Thrown when the <paramref name="linkedList"/> or <paramref name="action"/> is null.</exception>
    // [UsedImplicitly]
    // public static void IterateOver<T>(this LinkedList<T> linkedList, Action<T> action) where T : notnull {
    //     linkedList.ToList().ForEach(action);
    // }

    /// <summary>
    /// Filters the elements of an <see cref="IEnumerable{T}"/> sequence where the specified condition is false.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="array">The sequence to filter.</param>
    /// <param name="action">The condition to test each element against.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence where the specified condition is false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="array"/> is null.</exception>
    [UsedImplicitly]
    public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> array, Func<T, bool> action) where T : notnull {
        return array.Where(a => !action(a));
    }
}
