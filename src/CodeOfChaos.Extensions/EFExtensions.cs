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
public static class EfExtensions {

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
}
