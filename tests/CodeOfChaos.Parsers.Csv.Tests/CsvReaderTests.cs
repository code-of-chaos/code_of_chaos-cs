// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Tests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvReaderTests {
    private static CsvParserConfig BuildConfig(char columnSplit, bool includeHeader) => new() {
        ColumnSplit = columnSplit.ToString(),
        IncludeHeader = includeHeader
    };

    [Fact]
    public void ReadFromCsv_ShouldReadCsvCorrectly() {
        // Arrange
        CsvParserConfig config = BuildConfig(';', true);
        string csv = """
            Name;Age
            John;30
            Jane;25
            """;
        
        var reader = new CsvReader<TestModelWithoutAttribute>(config);
        using var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = reader.ReadFromCsv(stringReader).ToList();

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
        CsvParserConfig config = BuildConfig(';', true);
        string csv = """
            Name;Age
            John;30
            Jane;25
            """;
        
        var reader = new CsvReader<TestModelWithoutAttribute>(config);
        using var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = [];
        await foreach (TestModelWithoutAttribute data in reader.ReadFromCsvAsync(stringReader)) {
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
        CsvParserConfig config = BuildConfig(';', true);
        string csv = """
            Name;Age
            John;30
            Jane;
            """;
        
        var reader = new CsvReader<TestModelWithoutAttribute>(config);
        using var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = reader.ReadFromCsv(stringReader).ToList();

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
        CsvParserConfig config = BuildConfig(',', true);
        string csv = """
            Name,Age
            John,30
            Jane,25
            """;
        
        var reader = new CsvReader<TestModelWithoutAttribute>(config);
        using var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = reader.ReadFromCsv(stringReader).ToList();

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
        CsvParserConfig config = BuildConfig(';', true);
        string csv = """
            Name;Age
            John;30
            Jane;25
            """;
        
        var reader = new CsvReader<TestModelWithoutAttribute>(config);
        using var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = reader.ReadFromCsv(stringReader).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.IsType<int>(result[0].Age);
        Assert.IsType<string>(result[0].Name);
    }
    
    [Fact]
    public void ReadFromCsv_ShouldReadFileCorrectly() {
        // Arrange
        CsvParserConfig config = BuildConfig(';', true);
        var reader = new CsvReader<TestModelWithoutAttribute>(config);
        var path = "Data/TestData.csv";

        // Act
        List<TestModelWithoutAttribute> result = reader.ReadFromCsvFile(path).ToList();

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
        CsvParserConfig config = BuildConfig(';', true);
        var reader = new CsvReader<TestModelWithoutAttribute>(config);
        var path = "Data/TestData.csv";

        // Act
        List<TestModelWithoutAttribute> result = [];
        await foreach (TestModelWithoutAttribute data in reader.ReadFromCsvFileAsync(path)) {
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