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

    /// <summary>
    /// Orders a sequence of values based on a condition if the condition is true, otherwise returns the original sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="condition">The condition to determine whether to order the sequence.</param>
    /// <param name="orderBy">The expression used to order the sequence.</param>
    /// <returns>The ordered sequence if the condition is true, otherwise the original sequence.</returns>
    [UsedImplicitly]
    public static IQueryable<T> ConditionalOrderBy<T>(this IQueryable<T> source, bool condition, Expression<Func<T, object>> orderBy) =>
        condition
            ? source.OrderBy(orderBy)
            : source;

    /// <summary>
    /// Orders a sequence of values based on a condition if the condition is true, otherwise returns the original sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by the function used for ordering.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="condition">The condition to determine whether to order the sequence.</param>
    /// <param name="orderBy">The function to extract a key from an element for ordering.</param>
    /// <param name="comparer">The comparer to compare keys, or null to use the default comparer.</param>
    /// <returns>The ordered sequence if the condition is true, otherwise the original sequence.</returns>
    [UsedImplicitly]
    public static IQueryable<TSource> ConditionalOrderBy<TSource, TKey>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, TKey>> orderBy, IComparer<TKey>? comparer) =>
        condition
            ? source.OrderBy(orderBy, comparer)
            : source;

    /// <summary>
    /// Orders a sequence of values based on a key if the key is not null, otherwise returns the original sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
    /// <typeparam name="TKey">The type of the key used to order the sequence.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="orderBy">The function to extract the key for each element in the sequence.</param>
    /// <returns>The ordered sequence if the key function is not null, otherwise the original sequence.</returns>
    [UsedImplicitly]
    public static IQueryable<TSource> ConditionalOrderByNotNull<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>>? orderBy) =>
        orderBy is not null
            ? source.OrderBy(orderBy)
            : source;
    
    /// <summary>
    /// Orders a sequence of values based on a specified key selector if the selector is not null, otherwise returns the original sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by the key selector.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="orderBy">The key selector function that determines the order of the sequence.</param>
    /// <param name="comparer">The comparer used to compare keys.</param>
    /// <returns>The ordered sequence if the key selector is not null, otherwise the original sequence.</returns>
    [UsedImplicitly]
    public static IQueryable<TSource> ConditionalOrderByNotNull<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>>? orderBy, IComparer<TKey>? comparer) =>
        orderBy is not null
            ? source.OrderBy(orderBy,comparer)
            : source;

    /// <summary>
    /// Applies a specified query transformation to a sequence of values based on a condition.
    /// If the condition is false, the sequence remains unchanged.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
    /// <typeparam name="TKey">The type parameter used in transformations, if applicable.</typeparam>
    /// <param name="source">The sequence to be transformed.</param>
    /// <param name="condition">The condition to determine whether to apply the transformation.</param>
    /// <param name="queryableFunc">A function representing the transformation to apply to the sequence.</param>
    /// <returns>The transformed sequence if the condition is true, otherwise the original sequence.</returns>
    [UsedImplicitly]
    public static IQueryable<TSource> ConditionalQueryable<TSource, TKey>(this IQueryable<TSource> source, bool condition, Func<IQueryable<TSource>, IQueryable<TSource>> queryableFunc) =>
        condition
            ? queryableFunc(source)
            : source;
    
}
