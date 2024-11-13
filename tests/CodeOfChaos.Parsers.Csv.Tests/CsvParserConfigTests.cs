// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Tests;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvParserConfigTests {
    [Fact]
    public void CsvParserConfig_DefaultConstructor_ShouldSetDefaultValues() {
        // Arrange & Act
        var config = new CsvParserConfig();

        // Assert
        Assert.Equal(",", config.ColumnSplit);
        Assert.False(config.UseLowerCaseHeaders);
        Assert.True(config.IncludeHeader);
    }
}
