// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Linq.Expressions;

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
    /// Filters a sequence of values based on a condition if the condition is true, otherwise returns the original sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The sequence to filter.</param>
    /// <param name="condition">The condition to determine whether to filter the sequence.</param>
    /// <param name="predicate">The predicate used to filter the sequence.</param>
    /// <returns>The filtered sequence if the condition is true, otherwise the original sequence.</returns>
    [UsedImplicitly]
    public static IQueryable<T> ConditionalWhere<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate) =>
        condition
            ? source.Where(predicate)
            : source;

    /// <summary>
    /// Takes a specified number of elements from the beginning of a sequence, only if a specified condition is true.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to take elements from.</param>
    /// <param name="condition">A bool value indicating whether to apply the take operation.</param>
    /// <param name="count">The number of elements to take from the sequence.</param>
    /// <returns>
    /// Returns a new sequence that contains the specified number of elements from the beginning of the original sequence,
    /// if the specified condition is true and the number of elements to take is greater than 0; otherwise, it returns the original sequence.
    /// </returns>
    [UsedImplicitly]
    public static IQueryable<T> ConditionalTake<T>(this IQueryable<T> source, bool condition, int count) =>
        condition && count > 0
            ? source.Take(count)
            : source;

    /// <summary>
    /// Takes a specified number of elements from a sequence if a condition is met.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The sequence to take elements from.</param>
    /// <param name="condition">The condition to check.</param>
    /// <param name="range">The number of elements to take.</param>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> that contains the specified number of elements from the sequence
    /// if the condition is true; otherwise, the original <paramref name="source"/> sequence.
    /// </returns>
    [UsedImplicitly]
    public static IQueryable<T> ConditionalTake<T>(this IQueryable<T> source, bool condition, Range range) =>
        condition
            ? source.Take(range)
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

    /// <summary>
    /// Iterates over an array and performs the specified action on each element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="linkedList">The array to iterate over.</param>
    /// <param name="action">The action to perform on each element of the array.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="linkedList"/> or <paramref name="action"/> is null.</exception>
    [UsedImplicitly]
    public static void IterateOver<T>(this LinkedList<T> linkedList, Action<T> action) where T : notnull {
        linkedList.ToList().ForEach(action);
    }

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
