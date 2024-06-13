// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections;

namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// Provides extension methods for the Dictionary class.
/// </summary>
public static class DictionaryExtensions {
    /// <summary>
    /// Tries to add or update a value in the ConcurrentDictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="dictionary">The ConcurrentDictionary to add or update the value in.</param>
    /// <param name="key">The key to add or update the value for.</param>
    /// <param name="value">The value to add or update.</param>
    /// <returns>True if the value is added without already existing; otherwise, false.</returns>
    [UsedImplicitly]
    public static IDictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull {
        if (dictionary.TryAdd(key, value)) return dictionary;
        dictionary[key] = value;
        return dictionary;
        // TODO where is the false?
    }

    /// <summary>
    /// Tries to add a value to an existing collection in the dictionary or creates a new collection if the key does not exist.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <typeparam name="TCollection">The type of the collection in the dictionary values.</typeparam>
    /// <param name="dictionary">The dictionary to add or update the value in.</param>
    /// <param name="key">The key to add or update the value for.</param>
    /// <param name="value">The value to add.</param>
    /// <returns>True if the value is added; false if the value already exists in the collection.</returns>
    [UsedImplicitly] 
    public static bool TryAddToOrCreateCollection<TKey, TValue, TCollection>(
        this IDictionary<TKey, TCollection> dictionary,
        TKey key,
        TValue value
    ) where TCollection : ICollection<TValue>, new() {
        if (!dictionary.TryGetValue(key, out TCollection? collection)) return dictionary.TryAdd(key, [value]);
        if (collection.Contains(value)) return false;
        
        collection.Add(value);
        return true;
        
    }
}
