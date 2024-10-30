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
        ArgumentNullException.ThrowIfNull(linkedList);
        ArgumentNullException.ThrowIfNull(source);

        IEnumerable<T> enumerable = source.ToList();
        for (int i = enumerable.Count() - 1; i >= 0; i--) {
            linkedList.AddFirst(enumerable.ElementAt(i));
        }
    }

    /// <summary>
    /// Finds the first element in the linked list that satisfies a specified condition.
    /// </summary>
    /// <typeparam name="T">The type of elements in the linked list.</typeparam>
    /// <param name="linkedList">The linked list to search.</param>
    /// <param name="action">The condition that the elements must satisfy.</param>
    /// <returns>The first node in the linked list that satisfies the specified condition. If no such element is found, returns null.</returns>
    [UsedImplicitly]
    public static LinkedListNode<T>? Find<T>(this LinkedList<T> linkedList, Func<T, bool> action) {
        return linkedList.FirstOrDefault(action) is {} node ? linkedList.Find(node) : null;
    }

    /// <summary>
    /// Adds the specified node to the end of the linked list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the linked list.</typeparam>
    /// <param name="linkedList">The linked list to which the node will be added.</param>
    /// <param name="node">The node to add to the end of the linked list.</param>
    /// <return>The linked list with the node added.</return>
    public static LinkedList<T> Add<T>(this LinkedList<T> linkedList, LinkedListNode<T> node) {
        linkedList.AddLast(node);
        return linkedList;
    }

    /// <summary>
    /// Adds the specified value to the end of the linked list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the linked list.</typeparam>
    /// <param name="linkedList">The linked list to add the element to.</param>
    /// <param name="value">The value to add to the linked list.</param>
    /// <returns>The linked list with the new value added.</returns>
    public static LinkedList<T> Add<T>(this LinkedList<T> linkedList, T value) {
        linkedList.AddLast(value);
        return linkedList;
    }
}
