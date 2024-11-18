// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Parsers;

namespace CodeOfChaos.Parsers.Csv.Tests.Parsers;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvDictionaryReaderTest {
    private static CsvDictionaryReader CreateReader() {
        return new CsvDictionaryReader(config => config.ColumnSplit = ";");
    }

    [Fact]
    public void FromCsvFile_ShouldReturnCorrectData()
    {
        // Arrange
        var reader = CreateReader();
        var filePath = "FromCsvFile_ShouldReturnCorrectData.csv";
        File.WriteAllText(filePath, """
            id;name
            1;John
            2;Jane
            """);

        // Act
        var result = reader.FromCsvFile(filePath);

        // Assert
        var expected = new List<Dictionary<string, string?>>
        {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };
        
        Assert.Equal(expected, result);
    }

    [Fact]
    public void FromCsvString_ShouldReturnCorrectData()
    {
        // Arrange
        var reader = CreateReader();
        var data = """
            id;name
            1;John
            2;Jane
            """;

        // Act
        var result = reader.FromCsvString(data);

        // Assert
        var expected = new List<Dictionary<string, string?>>
        {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };
        
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task FromCsvFileAsync_ShouldReturnCorrectData()
    {
        // Arrange
        var reader = CreateReader();
        var filePath = "FromCsvFileAsync_ShouldReturnCorrectData.csv";
        await File.WriteAllTextAsync(filePath, """
        id;name
        1;John
        2;Jane
        """);

        // Act
        var result = reader.FromCsvFileAsync(filePath);

        // Assert
        var expected = new List<Dictionary<string, string?>>
        {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        var actual = new List<Dictionary<string, string?>>();
        await foreach (var dict in result)
        {
            actual.Add(dict);
        }

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task FromCsvStringAsync_ShouldReturnCorrectData()
    {
        // Arrange
        var reader = CreateReader();
        var data = """
            id;name
            1;John
            2;Jane
            """;

        // Act
        var result = reader.FromCsvStringAsync(data);

        // Assert
        var expected = new List<Dictionary<string, string?>>
        {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        var actual = new List<Dictionary<string, string?>>();
        await foreach (var dict in result)
        {
            actual.Add(dict);
        }

        Assert.Equal(expected, actual);
    }
}
