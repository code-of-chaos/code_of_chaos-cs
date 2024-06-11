// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Ansi;
using Xunit;

namespace CodeOfChaos.Tests.Ansi;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AnsiColorTest {
    [Fact]
    public void Fore_ShouldReturnStringWithAnsiCodes() {
        string actual = AnsiColor.Fore("red", "test");
        string expected = $"{AnsiCodes.RgbForegroundColor(AnsiColors.GetColor("red"))}test{AnsiCodes.ResetGraphicsModes}";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Back_ShouldReturnStringWithAnsiCodes() {
        string actual = AnsiColor.Back("green", "test");
        string expected = $"{AnsiCodes.RgbBackgroundColor(AnsiColors.GetColor("green"))}test{AnsiCodes.ResetGraphicsModes}";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Under_ShouldReturnStringWithAnsiCodes() {
        string actual = AnsiColor.Under("blue", "test");
        string expected = $"{AnsiCodes.RgbUnderlineColor(AnsiColors.GetColor("blue"))}test{AnsiCodes.ResetGraphicsModes}";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AsFore_ShouldReturnStringWithAnsiCode() {
        string actual = AnsiColor.AsFore("yellow");
        string expected = $"{AnsiCodes.RgbForegroundColor(AnsiColors.GetColor("yellow"))}";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AsBack_ShouldReturnStringWithAnsiCode() {
        string actual = AnsiColor.AsBack("cyan");
        string expected = $"{AnsiCodes.RgbBackgroundColor(AnsiColors.GetColor("cyan"))}";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AsUnder_ShouldReturnStringWithAnsiCode() {
        string actual = AnsiColor.AsUnder("magenta");
        string expected = $"{AnsiCodes.RgbUnderlineColor(AnsiColors.GetColor("magenta"))}";
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void F_ShouldCallForeBehaviour() {
        string actual = AnsiColor.F("red", "test");
        string expected = AnsiColor.Fore("red", "test");
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void B_ShouldCallBackBehaviour() {
        string actual = AnsiColor.B("green", "test");
        string expected = AnsiColor.Back("green", "test");
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void U_ShouldCallUnderBehaviour() {
        string actual = AnsiColor.U("blue", "test");
        string expected = AnsiColor.Under("blue", "test");
        Assert.Equal(expected, actual);
    }
}