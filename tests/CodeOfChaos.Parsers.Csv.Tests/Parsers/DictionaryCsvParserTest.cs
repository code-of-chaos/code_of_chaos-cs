// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Tests.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DictionaryCsvParserTest {
    private static CsvParser CreateParser() {
        return CsvParser.FromConfig(config => config.ColumnSplit = ";");
    }

    [Fact]
    public void FromCsvFile_ShouldReturnCorrectData() {
        // Arrange
        CsvParser parser = CreateParser();
        const string filePath = "FromCsvFile_ShouldReturnCorrectData.csv";
        File.WriteAllText(filePath, """
        id;name
        1;John
        2;Jane
        """);

        // Act
        List<Dictionary<string, string?>> result = parser.ToDictionaryList(filePath);

        // Assert
        var expected = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        Assert.Equal(expected, result);
    }

    [Fact]
    public void FromCsvString_ShouldReturnCorrectData() {
        // Arrange
        CsvParser parser = CreateParser();
        string data = """
            id;name
            1;John
            2;Jane
            """;
        var stringReader = new StringReader(data);

        // Act
        List<Dictionary<string, string?>> result = parser.ToDictionaryList(stringReader);

        // Assert
        var expected = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task FromCsvFileAsync_ShouldReturnCorrectData() {
        // Arrange
        CsvParser parser = CreateParser();
        string filePath = "FromCsvFileAsync_ShouldReturnCorrectData.csv";
        await File.WriteAllTextAsync(filePath, """
        id;name
        1;John
        2;Jane
        """);

        // Act
        IAsyncEnumerable<Dictionary<string, string?>> result = parser.ToDictionaryEnumerableAsync(filePath);

        // Assert
        var expected = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        var actual = new List<Dictionary<string, string?>>();
        await foreach (Dictionary<string, string?> dict in result) {
            actual.Add(dict);
        }

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task FromCsvStringAsync_ShouldReturnCorrectData() {
        // Arrange
        CsvParser parser = CreateParser();
        string data = """
            id;name
            1;John
            2;Jane
            """;
        var reader = new StringReader(data);

        // Act
        IAsyncEnumerable<Dictionary<string, string?>> result = parser.ToDictionaryEnumerableAsync(reader);

        // Assert
        var expected = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        var actual = new List<Dictionary<string, string?>>();
        await foreach (Dictionary<string, string?> dict in result) {
            actual.Add(dict);
        }

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void WriteToString_ShouldReturnCorrectCsv() {
        // Arrange
        CsvParser parser = CreateParser();
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        string result = parser.ParseToString(data);

        // Assert
        string expected = """
            id;name
            1;John
            2;Jane
            """;
        Assert.Equal(expected, result.Trim(), ignoreLineEndingDifferences: true);
    }

    [Fact]
    public async Task WriteToStringAsync_ShouldReturnCorrectCsv() {
        // Arrange
        CsvParser parser = CreateParser();
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        string result = await parser.ParseToStringAsync(data);

        // Assert
        string expected = """
            id;name
            1;John
            2;Jane
            """;
        Assert.Equal(expected, result.Trim(), ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void WriteToFile_ShouldWriteCorrectCsvToFile() {
        // Arrange
        CsvParser parser = CreateParser();
        string filePath = "WriteToFile_ShouldWriteCorrectCsvToFile.csv";
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        try {
            // Act
            parser.ParseToFile(filePath, data);

            // Assert
            string result = File.ReadAllText(filePath);
            string expected = """
                id;name
                1;John
                2;Jane
                """;
            Assert.Equal(expected, result.Trim(), ignoreLineEndingDifferences: true);
        }
        finally {
            // Clean up
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public async Task WriteToFileAsync_ShouldWriteCorrectCsvToFileAsync() {
        // Arrange
        CsvParser parser = CreateParser();
        string filePath = "WriteToFileAsync_ShouldWriteCorrectCsvToFileAsync.csv";
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        try {
            // Act
            await parser.ParseToFileAsync(filePath, data);

            // Assert
            string result = await File.ReadAllTextAsync(filePath);
            string expected = """
                id;name
                1;John
                2;Jane
                """;
            Assert.Equal(expected, result.Trim(), ignoreLineEndingDifferences: true);
        }
        finally {
            // Clean up
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }
}
