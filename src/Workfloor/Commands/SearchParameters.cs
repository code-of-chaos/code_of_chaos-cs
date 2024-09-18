// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CliArgsParser;

namespace Workfloor.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class SearchParameters : ICommandParameters {
    [ArgValue("key")] public string? KeyValue { get; set; } 
    public int? KeyAsInt => int.TryParse(KeyValue?.ToLowerInvariant(), out int key) ? key : default;
    public int? KeyAsString => int.TryParse(KeyValue?.ToLowerInvariant(), out int key) ? key : default;
}
