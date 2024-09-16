// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using JetBrains.Annotations;

namespace Workfloor.Commands;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class DataHolder {
    public DoublyLinkedList<int, string> TestData { get; } = new();
    
    public static IEnumerable<DoublyLinkedListNode<int, string>> SortByValueAlphabetically(Dictionary<int, DoublyLinkedListNode<int, string>> nodes)
    {
        // Custom sort alphabetically by the value strings
        var sortedNodes = new List<DoublyLinkedListNode<int, string>>(nodes.Values);
        sortedNodes.Sort((a, b) => string.Compare(a.Value, b.Value, StringComparison.Ordinal));
        return sortedNodes;
    }
}
