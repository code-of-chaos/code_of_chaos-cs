﻿// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Tests.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvWriterTests {
    #region Nameless object
    [Fact]
    public void CsvWriter_WriteToCsv_ShouldGenerateExpectedOutput_NamelessObject() {
        // Arrange
        var data = new[] {
            new { Name = "John", Age = 30 },
            new { Name = "Jane", Age = 25 }
        };

        CsvParser csvWriter = CsvParser.FromConfig(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });

        const string expectedOutput = """
            name;age
            John;30
            Jane;25
            """;

        // Act
        string csvContent = csvWriter.ParseToString(data);

        // Assert
        Assert.Equal(expectedOutput, csvContent.Trim());
    }

    [Fact]
    public async Task CsvWriter_WriteToCsvAsync_ShouldGenerateExpectedOutput_NamelessObject() {
        // Arrange
        var data = new[] {
            new { Name = "John", Age = 30 },
            new { Name = "Jane", Age = 25 }
        };

        CsvParser csvWriter = CsvParser.FromConfig(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });
        
        const string expectedOutput = """
            name;age
            John;30
            Jane;25
            """;

        await using var stringWriter = new StringWriter();

        // Act
        string csvContent = await csvWriter.ParseToStringAsync(data);

        // Assert
        Assert.Equal(expectedOutput, csvContent.Trim());
    }
    #endregion
    
    #region Class Without Attribute
    [Fact]
    public void CsvWriter_WriteToCsv_ShouldGenerateExpectedOutput_ClassWithoutAttributes() {
        // Arrange
        TestModelWithoutAttribute[] data = [
            new() { Name = "John", Age = 30 },
            new() { Name = "Jane", Age = 25 }
        ];

        CsvParser csvWriter = CsvParser.FromConfig(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });

        const string expectedOutput = """
            name;age
            John;30
            Jane;25
            """;

        // Act
        string csvContent = csvWriter.ParseToString(data);

        // Assert
        Assert.Equal(expectedOutput, csvContent.Trim());
    }

    [Fact]
    public async Task CsvWriter_WriteToCsvAsync_ShouldGenerateExpectedOutput_ClassWithoutAttributes() {
        // Arrange
        TestModelWithoutAttribute[] data = [
            new() { Name = "John", Age = 30 },
            new() { Name = "Jane", Age = 25 }
        ];

        var csvWriter = CsvParser.FromConfig(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });
        
        const string expectedOutput = """
            name;age
            John;30
            Jane;25
            """;

        // Act
        string csvContent = await csvWriter.ParseToStringAsync(data);

        // Assert
        Assert.Equal(expectedOutput, csvContent.Trim());
    }
    #endregion
    
    #region Class With Attribute
    [Fact]
    public void CsvWriter_WriteToCsv_ShouldGenerateExpectedOutput_Class() {
        // Arrange
        TestModel[] data = [
            new() { UserName = "John", UserAge = 30 },
            new() { UserName = "Jane", UserAge = 25 }
        ];

        CsvParser csvWriter = CsvParser.FromConfig(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });
        
        const string expectedOutput = """
            name;age
            John;30
            Jane;25
            """;

        // Act
        string csvContent = csvWriter.ParseToString(data);

        // Assert
        Assert.Equal(expectedOutput, csvContent.Trim());
    }

    [Fact]
    public async Task CsvWriter_WriteToCsvAsync_ShouldGenerateExpectedOutput_Class() {
        // Arrange
        TestModel[] data = [
            new() { UserName = "John", UserAge = 30 },
            new() { UserName = "Jane", UserAge = 25 }
        ];

        var csvWriter = CsvParser.FromConfig(config => {
            config.ColumnSplit = ";";
            config.UseLowerCaseHeaders = true;
        });
        
        const string expectedOutput = """
            name;age
            John;30
            Jane;25
            """;

        // Act
        string csvContent = await csvWriter.ParseToStringAsync(data);

        // Assert
        Assert.Equal(expectedOutput, csvContent.Trim());
    }
    #endregion
}
