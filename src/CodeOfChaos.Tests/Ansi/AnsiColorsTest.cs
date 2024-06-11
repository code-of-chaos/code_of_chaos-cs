// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Ansi;
using System.Collections.ObjectModel;
using Xunit;

namespace CodeOfChaos.Tests.Ansi;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AnsiColorsTest {
    [Fact]
    public void TestKnownColorsDictionary() {
        ReadOnlyDictionary<string, ByteVector3>? ansiColors = AnsiColors.KnownColorsDictionary;
        Assert.NotNull(ansiColors);// check if dictionary is not null

        // Add checks for specific predefined color entries
        ByteVector3 testValue;
        Assert.True(ansiColors.TryGetValue("aliceblue", out testValue));
        Assert.Equal(new ByteVector3(240, 248, 255), testValue);
    }

    [Fact]
    public void TestGetColor() {
        // test for known color, expecting to receive correct RGB object
        Assert.Equal(new ByteVector3(240, 248, 255), AnsiColors.GetColor("aliceblue"));

        // test for unknown color and check if returns zero value
        Assert.Equal(ByteVector3.Zero, AnsiColors.GetColor("unknownColor"));
    }

    [Fact]
    public void TestTryGetColor() {
        ByteVector3 result;

        // test for known color, expecting to receive the correct RGB object and true
        Assert.True(AnsiColors.TryGetColor("aliceblue", out result));
        Assert.Equal(new ByteVector3(240, 248, 255), result);

        // test for unknown color, expecting to receive zero value and false
        Assert.False(AnsiColors.TryGetColor("unknownColor", out result));
        Assert.Equal(ByteVector3.Zero, result);
    }

    [Fact]
    public void TestAddColor() {
        int countBefore = AnsiColors.KnownColorsDictionary.Count;

        // Add a new color
        Assert.True(AnsiColors.AddColor("testColor", new ByteVector3(100, 100, 100)));

        // Check if count of elements in KnownColorsDictionary is increased by 1
        Assert.Equal(countBefore + 1, AnsiColors.KnownColorsDictionary.Count);

        // Check if "testColor" really exists in KnownColorsDictionary
        Assert.True(AnsiColors.KnownColorsDictionary.ContainsKey("testColor"));

        // Try to add the same color again, expecting to return false
        Assert.False(AnsiColors.AddColor("testColor", new ByteVector3(100, 100, 100)));

        // Check if count of elements in KnownColorsDictionary remains the same
        Assert.Equal(countBefore + 1, AnsiColors.KnownColorsDictionary.Count);
    }
}