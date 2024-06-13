// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using System.Collections.Generic;
using Xunit;

namespace CodeOfChaos.Tests.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

public class DictionaryExtensionTests {
    [Fact]
    public void TryAddToOrCreateCollection_NewKey_CreatesCollectionAndAddsValue() {
        // Arrange
        var dictionary = new Dictionary<string, HashSet<int>>();
        const string key = "Test";
        const int value = 1;

        // Act
        bool result = dictionary.TryAddToOrCreateCollection(key, value);

        // Assert
        Assert.True(result);
        Assert.True(dictionary.ContainsKey(key));
        Assert.Contains(value, dictionary[key]);
    }

    [Fact]
    public void TryAddToOrCreateCollection_ExistingKeyNewValue_AddsValueToCollection() {
        // Arrange
        var dictionary = new Dictionary<string, HashSet<int>> {
            { "Test", [1] }
        };
        const string key = "Test";
        const int newValue = 2;

        // Act
        bool result = dictionary.TryAddToOrCreateCollection(key, newValue);

        // Assert
        Assert.True(result);
        Assert.True(dictionary.ContainsKey(key));
        Assert.Contains(newValue, dictionary[key]);
    }

    [Fact]
    public void TryAddToOrCreateCollection_ExistingKeyAndValue_ReturnsFalse() {
        // Arrange
        var dictionary = new Dictionary<string, HashSet<int>> {
            { "Test", [1] }
        };
        const string key = "Test";
        const int existingValue = 1;

        // Act
        bool result = dictionary.TryAddToOrCreateCollection(key, existingValue);

        // Assert
        Assert.False(result);
        Assert.True(dictionary.ContainsKey(key));
        Assert.Contains(existingValue, dictionary[key]);
    }
}
