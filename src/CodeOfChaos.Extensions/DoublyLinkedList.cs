// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DoublyLinkedListNode<TKey, TValue>(TKey key, TValue value) where TValue : notnull {
    public TKey Key { get; } = key;
    public TValue Value { get; set; } = value;
    public DoublyLinkedListNode<TKey, TValue>? Next { get; internal set; }
    public DoublyLinkedListNode<TKey, TValue>? Previous { get; internal set; }
}

public readonly struct ReadOnlyDoublyLinkedListNode<TKey, TValue>(TKey key, TValue value) where TValue : notnull {
    public TKey Key { get; } = key;
    public TValue Value { get; } = value;
}

public delegate IEnumerable<DoublyLinkedListNode<TKey, TValue>> SortCallback<TKey, TValue>(Dictionary<TKey, DoublyLinkedListNode<TKey, TValue>> nodes) where TKey : notnull where TValue : notnull;

public class DoublyLinkedList<TKey, TValue>(SortCallback<TKey, TValue>? sortCallback = null)
    : IEnumerable<ReadOnlyDoublyLinkedListNode<TKey, TValue>>
    where TKey : notnull 
    where TValue : notnull 
{
    private readonly Dictionary<TKey, DoublyLinkedListNode<TKey, TValue>> _lookup = new();

    public DoublyLinkedListNode<TKey, TValue>? Head { get; private set; }
    public DoublyLinkedListNode<TKey, TValue>? Tail { get; private set; }

    public SortCallback<TKey, TValue> SortingAlg = sortCallback ?? DefaultSort;
    
    #pragma warning disable CA1829
    private int? _count;
    public int Length => _count ??= this.Count(); // use the underlying enumerator
    #pragma warning restore CA1829

    public static List<DoublyLinkedListNode<TKey, TValue>> DefaultSort(Dictionary<TKey, DoublyLinkedListNode<TKey, TValue>> nodes) {
        var sortedNodes = new List<DoublyLinkedListNode<TKey, TValue>>(nodes.Values);
        sortedNodes.Sort((a, b) => Comparer<TKey>.Default.Compare(a.Key, b.Key));
        return sortedNodes;
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
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
    
    public bool TryRemove(TKey key) => TryRemove(key, out _);
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
    
    public void Sort() {
        IEnumerable<DoublyLinkedListNode<TKey, TValue>> sortedNodes = SortingAlg(_lookup);
        
        Head = Tail = null;
        foreach (DoublyLinkedListNode<TKey, TValue> node in sortedNodes) {
            TryAddLast(node.Key, node.Value);
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<ReadOnlyDoublyLinkedListNode<TKey, TValue>> GetEnumerator() {
        DoublyLinkedListNode<TKey, TValue>? currentNode = Head;
        while (currentNode is not null) {
            yield return new ReadOnlyDoublyLinkedListNode<TKey, TValue>(currentNode.Key, currentNode.Value);
            currentNode = currentNode.Next;
        }
    }
    public void Clear() {
        _lookup.Clear();
        Head = null;
        Tail = null;
    }
}
