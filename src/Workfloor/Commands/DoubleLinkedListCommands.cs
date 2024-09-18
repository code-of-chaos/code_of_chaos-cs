// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CliArgsParser;
using CodeOfChaos.Extensions;
using JetBrains.Annotations;

namespace Workfloor.Commands;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DoubleLinkedListCommands(DataHolder dataHolder) : ICommandAtlas {

    [UsedImplicitly]
    [Command("start")]  
    public void CommandStart() {
        dataHolder.TestData.TryAddLast(1, "One");
        dataHolder.TestData.TryAddLast(2, "Two");
        dataHolder.TestData.TryAddLast(3, "Three");
        dataHolder.TestData.TryAddLast(4, "Four");
        dataHolder.TestData.TryAddLast(5, "Five");
        dataHolder.TestData.TryAddLast(6, "Six");
        dataHolder.TestData.TryAddLast(7, "Seven");
        dataHolder.TestData.TryAddLast(8, "Eight");
        dataHolder.TestData.TryAddLast(9, "Nine");
        dataHolder.TestData.TryAddLast(10, "Ten");
        
        Console.WriteLine($"Seeded the DoubleLinkedList with {dataHolder.TestData.Count()} values ");
    }
    
    [UsedImplicitly]
    [Command<RemoveParameters>("remove")]  
    public void CommandRemove(RemoveParameters removeParameters) {
        if (removeParameters.Key is not {} key) {
            Console.WriteLine("The parameter `key` was invalid");
            return;
        }

        if (removeParameters.Return) {
            if (!dataHolder.TestData.TryRemove(key, out string? removedValue)) {
                Console.WriteLine("The parameter `key` was not found");
                return;
            }
            Console.WriteLine($"Removed `{removedValue}` at `{key}`");
        }
        else {
            if (!dataHolder.TestData.TryRemove(key)) {
                Console.WriteLine("The parameter `key` was not found");
                return;
            }
        }
        

        Console.WriteLine($"The DoubleLinkedList was correctly updated");
        Console.WriteLine($"The DoubleLinkedList has {dataHolder.TestData.Length} values ");
    }
    
    [UsedImplicitly]
    [Command("loop")]  
    public void CommandLoop() {
        Console.WriteLine($"The DoubleLinkedList has {dataHolder.TestData.Length} values ");
        
        foreach (ReadOnlyDoublyLinkedListNode<int, string> node in dataHolder.TestData) {
            Console.WriteLine($"{node.Key}: {node.Value}");
        }
        
        Console.WriteLine();
    }
    
    [UsedImplicitly]
    [Command<SortParameters>("sort")]  
    public void CommandChooseSort(SortParameters sortParameters) {
        Console.WriteLine("Trying to apply a new sort algorithm");
        dataHolder.TestData.SortingAlg = sortParameters.Name switch {
            "alpha" => DataHolder.SortByValueAlphabetically,
            _ => DoublyLinkedList<int, string>.DefaultSort,
        }; 
        Console.WriteLine("Applied new sort algorithm");
        
        dataHolder.TestData.Sort();
        Console.WriteLine("Applied sort to the data holder");
    }
    
    [UsedImplicitly]
    [Command("reset")]  
    public void CommandReset() {
        dataHolder.TestData.Clear();
    }
    
    
    [UsedImplicitly]
    [Command<SearchParameters>("search")]  
    public void CommandReset(SearchParameters parameters) {
        if (parameters.KeyAsInt is not {} keyAsInt) return;

        Console.WriteLine(dataHolder.TestData.TrySearch(keyAsInt, out string? value)
            ? $"The DoubleLinkedList was correctly searched and found `{value} at `{keyAsInt}`"
            : $"Nothing found at `{keyAsInt}`");
    }
    
}
