// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Tests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TestClass {
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}

public class TestClassWithAttribute {
    [CsvColumn("Name")] public string UserName { get; set; } = string.Empty;
    [CsvColumn("Age")] public int UserAge { get; set; }
}