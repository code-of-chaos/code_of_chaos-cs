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
    /// Transforms the elements of an IEnumerable based on a specified condition; if the condition is false, applies a transformation function to each element.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the IEnumerable.</typeparam>
    /// <param name="source">The IEnumerable to transform.</param>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="falseSelector">A transformation function to apply to each element if the condition is false.</param>
    /// <returns>
    /// An IEnumerable where each element is transformed by the falseSelector function if the condition is false,
    /// otherwise, it returns the input IEnumerable unchanged.
    /// </returns>
    [UsedImplicitly]
    public static IEnumerable<TSource> ConditionalSelectOnFalse<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, TSource> falseSelector) =>
        condition
            ? source
            : source.Select(falseSelector)
    ;

    /// <summary>
    /// Projects each element of an IEnumerable into a new form if the specified condition is true.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the IEnumerable.</typeparam>
    /// <param name="source">The IEnumerable to project.</param>
    /// <param name="condition">The condition to apply.</param>
    /// <param name="trueSelector">A transform function to apply to each element if the condition is true.</param>
    /// <returns>
    /// An IEnumerable whose elements are the result of invoking the transform function if the condition is true,
    /// otherwise, it returns the input IEnumerable unchanged.
    /// </returns>
    [UsedImplicitly]
    public static IEnumerable<TSource> ConditionalSelectOnTrue<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, TSource> trueSelector) =>
        condition
            ? source.Select(trueSelector)
            : source
    ;


    /// <summary>
    /// Projects each element of an IEnumerable into a new form based on a specified condition.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in the source IEnumerable.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the selectors.</typeparam>
    /// <param name="source">The IEnumerable to project.</param>
    /// <param name="condition">The condition to apply.</param>
    /// <param name="trueSelector">A transform function to apply to each source element if the condition is true.</param>
    /// <param name="falseSelector">A transform function to apply to each source element if the condition is false.</param>
    /// <returns>
    /// An IEnumerable whose elements are the result of applying the trueSelector if the condition is true,
    /// otherwise the result of applying the falseSelector.
    /// </returns>
    [UsedImplicitly]
    public static IEnumerable<TResult> ConditionalSelect<TSource, TResult>(this IEnumerable<TSource> source, bool condition, Func<TSource, TResult> trueSelector, Func<TSource, TResult> falseSelector) =>
        condition
            ? source.Select(trueSelector)
            : source.Select(falseSelector)
    ;

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
