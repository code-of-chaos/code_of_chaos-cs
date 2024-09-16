// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CliArgsParser;

namespace Workfloor.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class RemoveParameters : ICommandParameters {
    [ArgValue("key")] public string? KeyValue { get; set; } 
    public int? Key => int.TryParse(KeyValue?.ToLowerInvariant(), out int key) ? key : default;
    
    [ArgFlag("return")] public bool Return { get; set;}
}
