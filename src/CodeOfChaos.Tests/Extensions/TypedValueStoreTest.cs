// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Types;
using Xunit;

namespace CodeOfChaos.Tests.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TypedValueStoreTests {
    private readonly TypedValueStore _valueStorage = new();

    [Fact]
    public void TryAdd_ShouldAddValue_WhenKeyDoesNotExist() {
        // Arrange
        const string key = "testKey";
        const int value = 42;

        // Act
        bool result = _valueStorage.TryAdd(key, value);

        // Assert
        Assert.True(result);
        Assert.True(_valueStorage.TryGetValue(key, out int retrievedValue));
        Assert.Equal(value, retrievedValue);
    }

    [Fact]
    public void TryAdd_ShouldReturnFalse_WhenKeyAlreadyExists() {
        // Arrange
        const string key = "testKey";
        const int value = 42;
        _valueStorage.TryAdd(key, value);

        // Act
        bool result = _valueStorage.TryAdd(key, 100);

        // Assert
        Assert.False(result);
        Assert.True(_valueStorage.TryGetValue(key, out int retrievedValue));
        Assert.Equal(value, retrievedValue);
    }

    [Fact]
    public void TryGetValue_ShouldReturnTrue_WhenKeyExists() {
        // Arrange
        const string key = "testKey";
        const int value = 42;
        _valueStorage.TryAdd(key, value);

        // Act
        bool result = _valueStorage.TryGetValue(key, out int retrievedValue);

        // Assert
        Assert.True(result);
        Assert.Equal(value, retrievedValue);
    }

    [Fact]
    public void TryGetValue_ShouldReturnFalse_WhenKeyDoesNotExist() {
        // Act
        bool result = _valueStorage.TryGetValue("nonExistentKey", out int retrievedValue);

        // Assert
        Assert.False(result);
        Assert.Equal(default, retrievedValue);
    }

    [Fact]
    public void ContainsKey_ShouldReturnTrue_WhenKeyExists() {
        // Arrange
        const string key = "testKey";
        _valueStorage.TryAdd(key, 42);

        // Act
        bool result = _valueStorage.ContainsKey(key);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ContainsKey_ShouldReturnFalse_WhenKeyDoesNotExist() {
        // Act
        bool result = _valueStorage.ContainsKey("nonExistentKey");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Remove_ShouldReturnTrue_WhenKeyExists() {
        // Arrange
        const string key = "testKey";
        _valueStorage.TryAdd(key, 42);

        // Act
        bool result = _valueStorage.Remove(key);

        // Assert
        Assert.True(result);
        Assert.False(_valueStorage.ContainsKey(key));
    }

    [Fact]
    public void Remove_ShouldReturnFalse_WhenKeyDoesNotExist() {
        // Act
        bool result = _valueStorage.Remove("nonExistentKey");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Clear_ShouldRemoveAllEntries() {
        // Arrange
        _valueStorage.TryAdd("key1", 1);
        _valueStorage.TryAdd("key2", 2);

        // Act
        _valueStorage.Clear();

        // Assert
        Assert.Equal(0, _valueStorage.Count);
    }

    [Fact]
    public void TryUpdate_ShouldUpdateValue_WhenKeyExists() {
        // Arrange
        const string key = "testKey";
        _valueStorage.TryAdd(key, 42);

        // Act
        bool result = _valueStorage.TryUpdate(key, 100);

        // Assert
        Assert.True(result);
        Assert.True(_valueStorage.TryGetValue(key, out int value));
        Assert.Equal(100, value);
    }

    [Fact]
    public void TryUpdate_ShouldReturnFalse_WhenKeyDoesNotExist() {
        // Act
        bool result = _valueStorage.TryUpdate("nonExistentKey", 100);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddOrUpdate_ShouldAddValue_WhenKeyDoesNotExist() {
        // Arrange
        const string key = "testKey";
        const int value = 42;

        // Act
        int result = _valueStorage.AddOrUpdate(key, value, updateValueFactory: (_, v) => v + 1);

        // Assert
        Assert.Equal(value, result);
        Assert.True(_valueStorage.TryGetValue(key, out int retrievedValue));
        Assert.Equal(value, retrievedValue);
    }

    [Fact]
    public void AddOrUpdate_ShouldUpdateValue_WhenKeyExists() {
        // Arrange
        const string key = "testKey";
        _valueStorage.TryAdd(key, 42);

        // Act
        int result = _valueStorage.AddOrUpdate(key, 100, updateValueFactory: (_, v) => v + 1);

        // Assert
        Assert.Equal(43, result);
        Assert.True(_valueStorage.TryGetValue(key, out int updatedValue));
        Assert.Equal(43, updatedValue);
    }

    [Fact]
    public void GetOrAdd_ShouldAddValue_WhenKeyDoesNotExist() {
        // Arrange
        const string key = "testKey";
        const int value = 42;

        // Act
        int result = _valueStorage.GetOrAdd(key, value);

        // Assert
        Assert.Equal(value, result);
        Assert.True(_valueStorage.TryGetValue(key, out int retrievedValue));
        Assert.Equal(value, retrievedValue);
    }

    [Fact]
    public void GetOrAdd_ShouldReturnExistingValue_WhenKeyExists() {
        // Arrange
        const string key = "testKey";
        _valueStorage.TryAdd(key, 42);

        // Act
        int result = _valueStorage.GetOrAdd(key, 100);

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void GetOrAdd_WithFactory_ShouldAddValue_WhenKeyDoesNotExist() {
        // Arrange
        const string key = "testKey";

        // Act
        int result = _valueStorage.GetOrAdd(key, valueFactory: _ => 42);

        // Assert
        Assert.Equal(42, result);
        Assert.True(_valueStorage.TryGetValue(key, out int retrievedValue));
        Assert.Equal(42, retrievedValue);
    }

    [Fact]
    public void GetOrAdd_WithFactory_ShouldReturnExistingValue_WhenKeyExists() {
        // Arrange
        const string key = "testKey";
        _valueStorage.TryAdd(key, 42);

        // Act
        int result = _valueStorage.GetOrAdd(key, valueFactory: _ => 100);

        // Assert
        Assert.Equal(42, result);
    }
}
