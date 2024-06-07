// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Provides extension methods for the LinkedList class.
/// </summary>
[UsedImplicitly]
public static class LinkedListExtensions {
    /// <summary>
    /// Adds the elements of the specified collection to the end of the linked list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the linked list.</typeparam>
    /// <param name="linkedList">The linked list to add elements to.</param>
    /// <param name="source">The collection whose elements should be added to the end of the linked list.</param>
    [UsedImplicitly]
    public static void AddLastRepeated<T>(this LinkedList<T> linkedList, IEnumerable<T> source) {
        foreach (T t in source) {
            linkedList.AddLast(t);
        }
    }

    /// <summary>
    /// Adds the elements of the specified collection to the beginning of the linked list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the linked list.</typeparam>
    /// <param name="linkedList">The linked list to add elements to.</param>
    /// <param name="source">The collection whose elements should be added to the beginning of the linked list.</param>
    [UsedImplicitly]
    public static void AddFirstRepeated<T>(this LinkedList<T> linkedList, IEnumerable<T> source) {
        foreach (T t in source) {
            linkedList.AddFirst(t);
        }
    }
}
