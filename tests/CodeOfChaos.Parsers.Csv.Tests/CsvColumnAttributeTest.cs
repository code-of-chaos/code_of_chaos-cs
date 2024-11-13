// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Tests;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvColumnAttributeTests {
    [Fact]
    public void CsvColumnAttribute_ConstructorShouldSetNameProperty() {
        // Arrange
        string columnName = "TestColumn";

        // Act
        var attribute = new CsvColumnAttribute(columnName);

        // Assert
        Assert.Equal(columnName, attribute.Name);
    }

    [Fact]
    public void CsvColumnAttribute_NamePropertyShouldBeCaseInsensitive() {
        // Arrange
        string columnName = "TestColumn".ToLowerInvariant();
        var attributeWithUppercase = new CsvColumnAttribute("TESTCOLUMN");
        var attributeWithLowercase = new CsvColumnAttribute("testcolumn");

        // Act & Assert
        Assert.Equal(columnName, attributeWithUppercase.NameLowerInvariant);
        Assert.Equal(columnName, attributeWithLowercase.NameLowerInvariant);
    }

    [Fact]
    public void CsvColumnAttribute_ConstructorShouldThrowNullReferenceException_WhenNameIsNull() {
        // Act & Assert
        #pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<NullReferenceException>(() => new CsvColumnAttribute(null));
        #pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public void CsvColumnAttribute_ConstructorShouldThrowArgumentException_WhenNameIsEmpty() {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new CsvColumnAttribute(string.Empty));
    }
}
