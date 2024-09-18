// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents a node in a doubly linked list. Each node contains a key, a value,
/// a reference to the next node, and a reference to the previous node.
/// </summary>
/// <typeparam name="TKey">The type of the key in the node.</typeparam>
/// <typeparam name="TValue">The type of the value in the node. The value type must be non-nullable.</typeparam>
public class DoublyLinkedListNode<TKey, TValue>(TKey key, TValue value) where TValue : notnull {
    /// <summary>
    /// Gets the key associated with the DoublyLinkedListNode.
    /// </summary>
    public TKey Key { get; } = key;
    /// <summary>
    /// Gets or sets the value associated with the node in a doubly linked list.
    /// This property cannot be null as enforced by the class constraints.
    /// </summary>
    public TValue Value { get; set; } = value;
    /// <summary>
    /// Gets the next node in the doubly linked list.
    /// </summary>
    /// <remarks>
    /// The Next property links to the subsequent node in the doubly linked list.
    /// It returns null if the current node is the last node in the list.
    /// </remarks>
    public DoublyLinkedListNode<TKey, TValue>? Next { get; internal set; }
    /// <summary>
    /// Gets or sets the previous node in the doubly linked list.
    /// </summary>
    /// <remarks>
    /// This property contains a reference to the previous <see cref="DoublyLinkedListNode{TKey, TValue}"/> in the sequence.
    /// It is used to navigate backward through the list from the current node.
    /// If the current node is the first element in the list, this property will be <c>null</c>.
    /// </remarks>
    public DoublyLinkedListNode<TKey, TValue>? Previous { get; internal set; }
}

/// <summary>
/// A read-only node in a doubly linked list.
/// </summary>
/// <typeparam name="TKey">The type of the key of the node.</typeparam>
/// <typeparam name="TValue">The type of the value of the node.</typeparam>
public readonly struct ReadOnlyDoublyLinkedListNode<TKey, TValue>(TKey key, TValue value) where TValue : notnull {
    /// <summary>
    /// Gets the key associated with the node in the doubly linked list.
    /// </summary>
    /// <remarks>
    /// The key is used to identify and access the node within the linked list. It is read-only and assigned
    /// when the node is created.
    /// </remarks>
    public TKey Key { get; } = key;
    /// <summary>
    /// Gets or sets the value associated with the node.
    /// </summary>
    /// <remarks>
    /// This property is of generic type <typeparamref name="TValue"/> and cannot be null.
    /// It represents the data stored in the node and can be modified as required.
    /// </remarks>
    public TValue Value { get; } = value;
}

/// <summary>
/// Delegate for a callback function that sorts a dictionary of nodes in a doubly linked list.
/// </summary>
/// <typeparam name="TKey">The type of the key associated with each node.</typeparam>
/// <typeparam name="TValue">The type of the value associated with each node.</typeparam>
/// <param name="nodes">A dictionary of nodes to be sorted.</param>
/// <returns>An enumerable collection of sorted nodes.</returns>
public delegate IEnumerable<DoublyLinkedListNode<TKey, TValue>> SortCallback<TKey, TValue>(Dictionary<TKey, DoublyLinkedListNode<TKey, TValue>> nodes) where TKey : notnull where TValue : notnull;

/// <summary>
/// Represents a doubly linked list data structure.
/// A doubly linked list contains a sequence of nodes. Each node contains a reference to the previous and next node in the sequence.
/// </summary>
/// <typeparam name="TKey">The type of keys in the list. Keys must be unique and not null.</typeparam>
/// <typeparam name="TValue">The type of values in the list. Values must be not null.</typeparam>
public class DoublyLinkedList<TKey, TValue>(SortCallback<TKey, TValue>? sortCallback = null)
    : IEnumerable<ReadOnlyDoublyLinkedListNode<TKey, TValue>>
    where TKey : notnull 
    where TValue : notnull 
{
    /// <summary>
    /// A dictionary that maps keys to their corresponding nodes in the doubly linked list.
    /// Serves as a lookup table to quickly find nodes based on their keys.
    /// </summary>
    private readonly Dictionary<TKey, DoublyLinkedListNode<TKey, TValue>> _lookup = new();

    /// <summary>
    /// Represents the first node in the doubly linked list.
    /// If the list is empty, the value is <c>null</c>.
    /// </summary>
    public DoublyLinkedListNode<TKey, TValue>? Head { get; private set; }
    /// <summary>
    /// Gets the tail node of the doubly linked list.
    /// The tail node is the last node in the list.
    /// </summary>
    public DoublyLinkedListNode<TKey, TValue>? Tail { get; private set; }

    /// <summary>
    /// The SortingAlg variable is a delegate that defines the sorting algorithm
    /// to be used for reorganizing the nodes in a <see cref="DoublyLinkedList{TKey, TValue}"/>.
    /// It accepts a dictionary of nodes and returns an enumerable of doubly linked list nodes in the desired order.
    /// </summary>
    public SortCallback<TKey, TValue> SortingAlg = sortCallback ?? DefaultSort;
    
    #pragma warning disable CA1829
    /// <summary>
    /// Stores the count of nodes in the <see cref="DoublyLinkedList{TKey, TValue}"/>.
    /// </summary>
    /// <remarks>
    /// This variable is lazy-initialized and provides the current number of elements in the linked list.
    /// </remarks>
    private int? _count;

    /// <summary>
    /// Gets the number of nodes contained in the <see cref="DoublyLinkedList{TKey, TValue}"/>.
    /// </summary>
    /// <remarks>
    /// This property provides the count of the nodes in the doubly linked list by calculating the count if it hasn't been
    /// previously determined or cached. The count is cached for subsequent access to avoid recalculations.
    /// </remarks>
    public int Length => _count ??= this.Count(); // use the underlying enumerator
    #pragma warning restore CA1829

    /// <summary>
    /// Default sorting algorithm that sorts the nodes based on their key in ascending order.
    /// </summary>
    /// <param name="nodes">The dictionary containing the nodes to sort.</param>
    /// <returns>A list of doubly linked list nodes sorted by key in ascending order.</returns>
    public static List<DoublyLinkedListNode<TKey, TValue>> DefaultSort(Dictionary<TKey, DoublyLinkedListNode<TKey, TValue>> nodes) {
        var sortedNodes = new List<DoublyLinkedListNode<TKey, TValue>>(nodes.Values);
        sortedNodes.Sort((a, b) => Comparer<TKey>.Default.Compare(a.Key, b.Key));
        return sortedNodes;
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Tries to add a new node with the specified key and value at the end of the doubly linked list.
    /// </summary>
    /// <param name="key">The key of the new node.</param>
    /// <param name="value">The value of the new node.</param>
    /// <returns>True if the node was successfully added; False if the key already exists.</returns>
    public bool TryAddLast(TKey key, TValue value) {
        var newNode = new DoublyLinkedListNode<TKey, TValue>(key, value);

        if (Head is null && Tail is null) {
            Head = newNode;
        }
        else {
            Tail!.Next = newNode;
            newNode.Previous = Tail;
        }
        Tail = newNode;
        return _lookup.TryAdd(key, newNode);
    }

    /// <summary>
    /// Attempts to remove a node with the specified key from the doubly linked list.
    /// </summary>
    /// <param name="key">The key of the node to remove.</param>
    /// <returns>True if the node was successfully removed; otherwise, false.</returns>
    public bool TryRemove(TKey key) => TryRemove(key, out _);
    /// <summary>
    /// Attempts to remove a node from the doubly linked list by its key.
    /// </summary>
    /// <param name="key">The key of the node to be removed.</param>
    /// <param name="removedValue">When this method returns, contains the value of the removed node, if the key was found; otherwise, the default value for the type of the removedValue parameter. This parameter is passed uninitialized.</param>
    /// <returns>true if the node was successfully removed; otherwise, false.</returns>
    public bool TryRemove(TKey key, [NotNullWhen(true)] out TValue? removedValue) {
        removedValue = default;
        
        if (!_lookup.TryGetValue(key, out DoublyLinkedListNode<TKey, TValue>? node)) return false;
        if (node == Head) {
            Head = node.Next;
        }
        if (node == Tail) {
            Tail = node.Previous;
        }

        switch (node.Previous, node.Next) {
            case (null, null):
                break; // Node was only in the chain
            case ({} previousNode, null):
                previousNode.Next = null;
                break;
            case (null, {} nextNode):
                nextNode.Previous = null;
                break;
            case ({} previousNode, {} nextNode):
                previousNode.Next = nextNode;
                nextNode.Previous = previousNode;
                break;
        }
        _lookup.Remove(key);
        removedValue = node.Value;
        return true;
    }

    /// <summary>
    /// Sorts the DoublyLinkedList using the current sorting algorithm.
    /// </summary>
    /// <remarks>
    /// The Sort method reorders the nodes in the doubly linked list based on the sorting algorithm specified in the SortingAlg property.
    /// The SortingAlg delegate is invoked with the internal dictionary of nodes, and the returned IEnumerable of nodes is used to
    /// reconstruct the linked list in the new order.
    /// </remarks>
    public void Sort() {
        IEnumerable<DoublyLinkedListNode<TKey, TValue>> sortedNodes = SortingAlg(_lookup);
        
        Head = Tail = null;
        foreach (DoublyLinkedListNode<TKey, TValue> node in sortedNodes) {
            TryAddLast(node.Key, node.Value);
        }
    }

    /// Returns an enumerator that iterates through the doubly linked list.
    /// <return>An enumerator for the doubly linked list.</return>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    /// <summary>
    /// Returns an enumerator that iterates through the DoublyLinkedList.
    /// </summary>
    /// <returns>
    /// An enumerator for ReadOnlyDoublyLinkedListNode objects.
    /// </returns>
    public IEnumerator<ReadOnlyDoublyLinkedListNode<TKey, TValue>> GetEnumerator() {
        DoublyLinkedListNode<TKey, TValue>? currentNode = Head;
        while (currentNode is not null) {
            yield return new ReadOnlyDoublyLinkedListNode<TKey, TValue>(currentNode.Key, currentNode.Value);
            currentNode = currentNode.Next;
        }
    }
    /// <summary>
    /// Clears all the nodes from the doubly-linked list, resetting it to an empty state.
    /// </summary>
    /// <remarks>
    /// This method will remove all elements from the list, setting both the head and tail nodes to null.
    /// </remarks>
    public void Clear() {
        _lookup.Clear();
        Head = null;
        Tail = null;
    }
}
