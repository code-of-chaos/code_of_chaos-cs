// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Concurrent;

namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Contains extension methods for ConcurrentDictionary.
/// </summary>
[UsedImplicitly]
public static class ConcurrentDictionaryExtensions {

    /// <summary>
    /// Populates a ConcurrentDictionary with empty values for all keys of a given Enum type.
    /// </summary>
    /// <typeparam name="TKey">The enum type representing the keys of the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="dictionary">The ConcurrentDictionary to populate with empty values.</param>
    /// <returns>
    /// The same ConcurrentDictionary with empty values added for all keys of the Enum type.
    /// </returns>
    [UsedImplicitly]
    public static ConcurrentDictionary<TKey, TValue> PopulateWithEmpties<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary)
        where TValue : new()
        where TKey : Enum {

        foreach (TKey key in Enum.GetValues(typeof(TKey))) {
            dictionary.TryAdd(key, new TValue());
        }

        return dictionary;
    }

    /// <summary>
    /// Tries to add a value to a ConcurrentBag within the ConcurrentDictionary, or creates a new bag if it does not exist.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the bags.</typeparam>
    /// <param name="dictionary">The ConcurrentDictionary containing ConcurrentBags.</param>
    /// <param name="key">The key corresponding to the ConcurrentBag to add the value to.</param>
    /// <param name="value">The value to add to the ConcurrentBag.</param>
    /// <returns>True if the value was added; false if the value already existed in the bag.</returns>
    [UsedImplicitly]
    public static bool TryAddToBagOrCreateBag<TKey, TValue>(this ConcurrentDictionary<TKey, ConcurrentBag<TValue>> dictionary, TKey key, TValue value) where TKey : notnull {
        // return dictionary.TryAddToOrCreateCollection(key, value);
        if (!dictionary.TryGetValue(key, out ConcurrentBag<TValue>? existingBag)) return dictionary.TryAdd(key, [value]);
        if (existingBag.Contains(value)) return false;

        existingBag.Add(value);
        return true;
    }
}
