// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Parsers;

namespace CodeOfChaos.Parsers.Csv.Tests.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvDictionaryWriterTest {
    private CsvDictionaryWriter CreateWriter() {
        return new CsvDictionaryWriter(config => {
            config.ColumnSplit = ";";
            config.IncludeHeader = true;
        });
    }

    [Fact]
    public void WriteToString_ShouldReturnCorrectCsv() {
        // Arrange
        CsvDictionaryWriter writer = CreateWriter();
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        string result = writer.WriteToString(data);

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
        CsvDictionaryWriter writer = CreateWriter();
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        string result = await writer.WriteToStringAsync(data);

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
        CsvDictionaryWriter writer = CreateWriter();
        string filePath = "WriteToFile_ShouldWriteCorrectCsvToFile.csv";
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        try {
            // Act
            writer.WriteToFile(filePath, data);

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
        CsvDictionaryWriter writer = CreateWriter();
        string filePath = "WriteToFileAsync_ShouldWriteCorrectCsvToFileAsync.csv";
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        try {
            // Act
            await writer.WriteToFileAsync(filePath, data);

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
