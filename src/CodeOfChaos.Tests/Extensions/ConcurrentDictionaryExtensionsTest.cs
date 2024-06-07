// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using JetBrains.Annotations;
using System.Collections.Concurrent;
using Xunit;

namespace CodeOfChaos.Tests.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Support Code
// ---------------------------------------------------------------------------------------------------------------------
public enum TestEnum {
    Zero,
    One,
    Two
}

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(ConcurrentDictionaryExtensions))]
public class ConcurrentDictionaryExtensionsTest {
    [Fact]
    public void PopulateWithEmpties_ShouldPopulateCorrectly() {
        // Initialize
        var dict = new ConcurrentDictionary<TestEnum, object>();

        // Act
        dict.PopulateWithEmpties();

        // Assert
        Assert.True(dict.ContainsKey(TestEnum.Zero));
        Assert.True(dict.ContainsKey(TestEnum.One));
        Assert.True(dict.ContainsKey(TestEnum.Two));
    }

    [Fact]
    public void TryAddToBagOrCreateBag_ShouldAddWhenBagNotExists() {
        // Initialize
        var dict = new ConcurrentDictionary<int, ConcurrentBag<string>>();

        // Act
        bool added = dict.TryAddToBagOrCreateBag(1, "test");

        // Assert
        Assert.True(added);
        Assert.Contains("test", dict[1]);
    }

    [Fact]
    public void TryAddToBagOrCreateBag_ShouldAddToExistingBag() {
        // Initialize
        var dict = new ConcurrentDictionary<int, ConcurrentBag<string>>();
        dict.TryAdd(1, new ConcurrentBag<string>());

        // Act
        bool added = dict.TryAddToBagOrCreateBag(1, "test");

        // Assert
        Assert.True(added);
        Assert.Contains("test", dict[1]);
    }

    [Fact]
    public void TryAddToBagOrCreateBag_ShouldNotAddExistingToBag() {
        // Initialize
        var dict = new ConcurrentDictionary<int, ConcurrentBag<string>>();
        dict.TryAddToBagOrCreateBag(1, "test");

        // Act
        var added = dict.TryAddToBagOrCreateBag(1, "test");

        // Assert
        Assert.False(added);
    }

    [Fact]
    public void AddOrUpdate_ShouldAddWhenNotExists() {
        // Initialize
        var dict = new ConcurrentDictionary<int, string>();

        // Act
        dict.AddOrUpdate(1, "test");

        // Assert
        Assert.True(dict.ContainsKey(1));
        Assert.Equal("test", dict[1]);
    }

    [Fact]
    public void AddOrUpdate_ShouldUpdateWhenExists() {
        // Initialize
        var dict = new ConcurrentDictionary<int, string>();
        dict.TryAdd(1, "oldValue");

        // Act
        dict.AddOrUpdate(1, "newValue");

        // Assert
        Assert.Equal("newValue", dict[1]);
    }
}
