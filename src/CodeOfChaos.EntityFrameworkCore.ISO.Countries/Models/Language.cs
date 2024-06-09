// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace CodeOfChaos.EntityFrameworkCore.ISO.Countries.Models;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents a language.
/// </summary>
public class Language {
    /// <summary>
    /// Represents a language.
    /// </summary>
    public string Alpha2 { get; init;} = null!;
    /// <summary>
    /// Represents the Alpha3 property of the Language class.
    /// </summary>
    /// <remarks>
    /// This property is a string that represents the Alpha-3 code of a language.
    /// The Alpha-3 code is a three-letter code assigned by the International Organization for Standardization (ISO) to represent languages and stands for "Alpha-3 Language Code".
    /// </remarks>
    public string Alpha3 { get; init;} = null!;
    /// <summary>
    /// Gets or sets the name of the language.
    /// </summary>
    public string Name { get; init;} = null!;
    /// <summary>
    /// Gets or sets the Name2 property.
    /// </summary>
    /// <remarks>
    /// This property represents the second name of the language.
    /// </remarks>
    /// <value>
    /// The second name of the language.
    /// </value>
    public string Name2 { get; init;} = null!;
    /// <summary>
    /// Gets or sets the native name of the language.
    /// </summary>
    public string NativeName { get; init;} = null!;
    /// <summary>
    /// Represents a language.
    /// </summary>
    public string Family { get; init;} = null!;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Creates an instance of the Language class from an instance of the ISOLib.Language class.
    /// </summary>
    /// <param name="language">The ISOLib.Language instance to create the Language instance from.</param>
    /// <returns>An instance of the Language class.</returns>
    [UsedImplicitly]
    public static Language CreateFromIsoLib(ISOLib.Language language) {
        return new Language {
            Alpha2=language.Alpha2,
            Alpha3=language.Alpha3,
            Name=language.Name,
            Name2=language.Name2,
            NativeName=language.NativeName,
            Family=language.Family
        };
    }

    /// <summary>
    /// Retrieves the ISO language information for the current Language instance, based on the Alpha3 code.
    /// </summary>
    /// <returns>The ISO language information represented by the current Language instance.</returns>
    [UsedImplicitly]
    public ISOLib.Language AsIsoCurrency() {
        return ISOLib.Languages.GetByAlpha3Code(Alpha3);
    } 
    
    
}
