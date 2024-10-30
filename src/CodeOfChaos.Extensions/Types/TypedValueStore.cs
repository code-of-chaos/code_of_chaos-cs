// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.Extensions.Types;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Provides a thread-safe storage for values of different types identified by keys.
/// </summary>
public class TypedValueStore {
    // -----------------------------------------------------------------------------------------------------------------
    // Nested Interfaces and Classes
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Defines a container interface for storing and retrieving values.
    /// </summary>
    private interface IValueContainer {
        /// <summary>
        /// Attempts to retrieve the stored value as the specified type.
        /// <param name="value">
        /// When this method returns, contains the value associated with the specified type,
        /// if the key is found and the value can be cast to the specified type; otherwise,
        /// the default value for the type of the value parameter. This parameter is passed uninitialized.
        /// </param>
        /// <typeparam name="T">The type of the value to get.</typeparam>
        /// <returns>true if the value was retrieved and is of the specified type; otherwise, false.</returns>
        /// </summary>
        bool TryGetAsValue<T>([NotNullWhen(true)] out T? value) where T : notnull;
    }

    /// <summary>
    /// Represents a container for storing values of a specific type, implementing functionality to retrieve
    /// values while ensuring type safety.
    /// </summary>
    /// <typeparam name="T">The type of the value being stored in the container.</typeparam>
    private class ValueContainer<T>(T value) : IValueContainer {
        /// <inheritdoc cref="IValueContainer.TryGetAsValue{T}"/>
        public bool TryGetAsValue<T1>([NotNullWhen(true)] out T1? value1) where T1 : notnull {
            if (value is T1 castedValue) {
                value1 = castedValue;
                return true;
            }
            value1 = default;
            return false;
        }
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Fields
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Concurrent dictionary used for storing values associated with string keys.
    /// </summary>
    private readonly ConcurrentDictionary<string, IValueContainer> _storage = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Public Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Tries to add a new value to the storage with the specified key.
    /// </summary>
    /// <param name="key">The key under which the value should be stored.</param>
    /// <param name="value">The value to be stored.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>True if the value was successfully added; otherwise, false if the key already exists in the storage.</returns>
    public bool TryAdd<T>(string key, T value) where T : notnull {
        return _storage.TryAdd(key, new ValueContainer<T>(value));
    }

    /// <summary>
    /// Attempts to retrieve the value associated with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="key">The key of the value to retrieve.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns>True if the key was found; otherwise, false.</returns>
    public bool TryGetValue<T>(string key, [NotNullWhen(true)] out T? value) where T : notnull {
        if (_storage.TryGetValue(key, out IValueContainer? container)) {
            return container.TryGetAsValue(out value);
        }
        value = default;
        return false;
    }

    /// <summary>
    /// Checks if the storage contains the specified key.
    /// </summary>
    /// <param name="key">The key to check in the storage.</param>
    /// <returns>
    /// True if the storage contains the specified key; otherwise, false.
    /// </returns>
    public bool ContainsKey(string key) => _storage.ContainsKey(key);

    /// <summary>
    /// Removes an entry from the value storage with the specified key.
    /// </summary>
    /// <param name="key">The key of the entry to be removed.</param>
    /// <returns>True if the entry was successfully removed; otherwise, false.</returns>
    public bool Remove(string key) => _storage.TryRemove(key, out _);

    /// <summary>
    /// Removes all elements from the storage.
    /// </summary>
    public void Clear() => _storage.Clear();

    /// <summary>
    /// Gets the number of elements contained in the ValueStorage.
    /// </summary>
    public int Count => _storage.Count;

    /// <summary>
    /// Attempts to update the value associated with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The key of the value to be updated.</param>
    /// <param name="newValue">The new value to associate with the specified key.</param>
    /// <returns>
    /// True if the value was successfully updated; otherwise, false.
    /// </returns>
    public bool TryUpdate<T>(string key, T newValue) where T : notnull {
        if (!_storage.TryGetValue(key, out IValueContainer? container)) return false;
        return _storage.TryUpdate(key, new ValueContainer<T>(newValue), container);
    }

    /// <summary>
    /// Adds a key/value pair to the storage if the key does not already exist,
    /// or updates a key/value pair in the storage.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The key of the element to add or update.</param>
    /// <param name="addValue">The value to add if the key does not exist.</param>
    /// <param name="updateValueFactory">The function used to generate a new value for an existing key.</param>
    /// <returns>The new value for the key. This will either be <paramref name="addValue"/> or the result of
    /// the <paramref name="updateValueFactory"/>.</returns>
    public T AddOrUpdate<T>(string key, T addValue, Func<string, T, T> updateValueFactory) where T : notnull {
        var newContainer = new ValueContainer<T>(addValue);
        IValueContainer updatedContainer = _storage.AddOrUpdate(
            key,
            newContainer,
            (_, oldContainer) => {
                oldContainer.TryGetAsValue(out T? oldValue);
                return new ValueContainer<T>(updateValueFactory(key, oldValue ?? addValue));
            }
        );

        return updatedContainer.TryGetAsValue(out T? resultValue) 
            ? resultValue 
            : throw new InvalidOperationException("Unexpected failure in AddOrUpdate.");
    }

    /// <summary>
    /// Retrieves the value associated with the specified key, or adds a new value created from the provided one
    /// if the key does not exist in the storage.
    /// </summary>
    /// <typeparam name="T">The type of the value to get or add. Must be a non-nullable type.</typeparam>
    /// <param name="key">The key of the value to get or add.</param>
    /// <param name="value">The value to add if the key does not exist.</param>
    /// <returns>The value associated with the specified key, or the newly added value.</returns>
    public T GetOrAdd<T>(string key, T value) where T : notnull {
        return ((ValueContainer<T>)_storage.GetOrAdd(key, _ => new ValueContainer<T>(value)))
            .TryGetAsValue(out T? resultValue) ? resultValue : throw new InvalidOperationException("Unexpected failure in GetOrAdd.");
    }

    /// <summary>
    /// Gets the value associated with the specified key or adds a new value if the key does not exist.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The key of the value to get or add.</param>
    /// <param name="valueFactory">A factory to create the value in the event of no previously found one.</param>
    /// <returns>The value associated with the specified key or the newly added value.</returns>
    public T GetOrAdd<T>(string key, Func<string, T> valueFactory) where T : notnull {
        return ((ValueContainer<T>)_storage.GetOrAdd(key, k => new ValueContainer<T>(valueFactory(k))))
            .TryGetAsValue(out T? resultValue) ? resultValue : throw new InvalidOperationException("Unexpected failure in GetOrAdd.");
    }
}