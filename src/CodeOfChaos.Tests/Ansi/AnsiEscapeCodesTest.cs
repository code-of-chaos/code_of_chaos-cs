// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Ansi;
using JetBrains.Annotations;
using Xunit;

namespace CodeOfChaos.Tests.Ansi;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(AnsiEscapeCodes))]
public class AnsiEscapeCodesTest {

    [Fact]
    public void TestEscapeCtrlValue() {
        // Arrange
        const string expectedValue = "^[";

        // Act
        string actualValue = AnsiEscapeCodes.EscapeCtrl;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void TestEscapeOctalValue() {
        // Arrange
        const string expectedValue = "\033";

        // Act
        string actualValue = AnsiEscapeCodes.EscapeOctal;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void TestEscapeUnicodeValue() {
        // Arrange
        const string expectedValue = "\u001b";

        // Act
        string actualValue = AnsiEscapeCodes.EscapeUnicode;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void TestEscapeHexadecimalValue() {
        // Arrange
        const string expectedValue = "\x1B";

        // Act
        string actualValue = AnsiEscapeCodes.EscapeHexadecimal;

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }
}
