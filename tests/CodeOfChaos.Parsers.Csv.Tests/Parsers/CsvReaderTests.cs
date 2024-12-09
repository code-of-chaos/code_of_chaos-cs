// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Parsers;

namespace CodeOfChaos.Parsers.Csv.Tests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvReaderTests {
    [Fact]
    public void ReadFromCsv_ShouldReadCsvCorrectly() {
        // Arrange
        var reader = new CsvFileReader<TestModelWithoutAttribute>(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });
        string csv = """
            Name;Age
            John;30
            Jane;25
            """;

        // Act
        List<TestModelWithoutAttribute> result = reader.FromCsvString(csv).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
        Assert.Equal("Jane", result[1].Name);
        Assert.Equal(25, result[1].Age);
    }

    [Fact]
    public async Task ReadFromCsvAsync_ShouldReadCsvCorrectly() {
        // Arrange
        var reader = new CsvFileReader<TestModelWithoutAttribute>(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });
        string csv = """
            Name;Age
            John;30
            Jane;25
            """;

        // Act
        List<TestModelWithoutAttribute> result = [];
        await foreach (TestModelWithoutAttribute data in reader.FromCsvStringAsync(csv)) {
            result.Add(data);
        }

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
        Assert.Equal("Jane", result[1].Name);
        Assert.Equal(25, result[1].Age);
    }

    [Fact]
    public void ReadFromCsv_ShouldHandleMissingColumnsGracefully() {
        // Arrange
        var reader = new CsvFileReader<TestModelWithoutAttribute>(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });
        string csv = """
            Name;Age
            John;30
            Jane;
            """;

        // Act
        List<TestModelWithoutAttribute> result = reader.FromCsvString(csv).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
        Assert.Equal("Jane", result[1].Name);
        Assert.Equal(0, result[1].Age);  // Age should be correctly parsed to 25
    }

    [Fact]
    public void ReadFromCsv_ShouldRespectConfiguration() {
        // Arrange
        var reader = new CsvFileReader<TestModelWithoutAttribute>(cfg => {
            cfg.ColumnSplit = ",";
            cfg.IncludeHeader = true;
        });
        string csv = """
            Name,Age
            John,30
            Jane,25
            """;

        // Act
        List<TestModelWithoutAttribute> result = reader.FromCsvString(csv).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
        Assert.Equal("Jane", result[1].Name);
        Assert.Equal(25, result[1].Age);
    }

    [Fact]
    public void ReadFromCsv_ShouldConvertToPropertyTypes() {
        // Arrange
        var reader = new CsvFileReader<TestModelWithoutAttribute>(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });
        string csv = """
            Name;Age
            John;30
            Jane;25
            """;

        // Act
        List<TestModelWithoutAttribute> result = reader.FromCsvString(csv).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.IsType<int>(result[0].Age);
        Assert.IsType<string>(result[0].Name);
    }
    
    [Fact]
    public void ReadFromCsv_ShouldReadFileCorrectly() {
        // Arrange
        var reader = new CsvFileReader<TestModelWithoutAttribute>(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });
        var path = "Data/TestData.csv";

        // Act
        List<TestModelWithoutAttribute> result = reader.FromCsvFile(path).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
        Assert.Equal("Jane", result[1].Name);
        Assert.Equal(25, result[1].Age);
    }
    
    [Fact]
    public async Task ReadFromCsv_ShouldReadFileCorrectlyAsync() {
        // Arrange
        var reader = new CsvFileReader<TestModelWithoutAttribute>(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });
        var path = "Data/TestData.csv";

        // Act
        List<TestModelWithoutAttribute> result = [];
        await foreach (TestModelWithoutAttribute data in reader.FromCsvFileAsync(path)) {
            result.Add(data);
        }

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].Name);
        Assert.Equal(30, result[0].Age);
        Assert.Equal("Jane", result[1].Name);
        Assert.Equal(25, result[1].Age);
    }
}