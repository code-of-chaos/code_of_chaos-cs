// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CodeOfChaos.EntityFrameworkCore.ISO.Countries.Models;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents a country.
/// </summary>
public class Country {
    /// <summary>
    /// Represents a country.
    /// </summary>
    public string Alpha2 { get; init;} = null!;
    /// <summary>
    /// Represents a country.
    /// </summary>
    [Key] public string Alpha3 { get; init;} = null!;
    /// <summary>
    /// Represents the name of a country.
    /// </summary>
    public string Name { get; init;} = null!;
    /// <summary>
    /// Represents a country with its properties and methods.
    /// </summary>
    public string Name2 { get; init;} = null!;
    /// <summary>
    /// Gets or sets the native name of the country.
    /// </summary>
    public string NativeName { get; init;} = null!;
    /// <summary>
    /// Represents the capital of a country.
    /// </summary>
    public string Capital { get; init;} = null!;
    /// <summary>
    /// Represents the country code of a country.
    /// </summary>
    public string CountryCode { get; init;} = null!;
    /// <summary>
    /// Represents the continent of a country.
    /// </summary>
    public string Continent { get; init;} = null!;
    /// <summary>
    /// Represents the alpha-2 code of the continent where the country belongs to.
    /// </summary>
    public string ContinentAlpha2 { get; init;} = null!;
    /// <summary>
    /// Represents a Wiki property of a Country.
    /// </summary>
    public string Wiki { get; init;} = null!;
    /// <summary>
    /// Represents a country flag.
    /// </summary>
    public string Flag { get; init;} = null!;
    /// <summary>
    /// Represents a country with its details including phone codes. </summary>
    /// /
    public int[] Phones { get; init;} = null!;
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public Currency[] Currencies { get; init;} = null!;
    /// <summary>
    /// Represents a language.
    /// </summary>
    public Language[] Languages { get; init;} = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Create a Country object from an ISOLib.Country object along with an array of currencies and an array of languages.
    /// </summary>
    /// <param name="country">The ISOLib.Country object representing the country.</param>
    /// <param name="currencies">An array of Currency objects representing the currencies used in the country.</param>
    /// <param name="languages">An array of Language objects representing the languages spoken in the country.</param>
    /// <returns>A Country object created from the given ISOLib.Country object along with the provided currencies and languages.</returns>
    public static Country CreateFromIsoLib(ISOLib.Country country, Currency[] currencies, Language[] languages){
        return new Country {
            Alpha2=country.Alpha2,
            Alpha3=country.Alpha3,
            Name=country.Name,
            Name2=country.Name2,
            NativeName=country.NativeName,
            Capital=country.Capital,
            CountryCode=country.CountryCode,
            Continent=country.Continent,
            ContinentAlpha2=country.ContinentAlpha2,
            Phones=country.Phones,
            Currencies=currencies,
            Languages=languages,
            Flag=country.Flag,
            Wiki=country.Wiki
        };
    }

    /// <summary>
    /// Converts the current instance of the <see cref="Country"/> class to an instance of the ISOLib.Country class.
    /// </summary>
    /// <returns>The corresponding instance of the ISOLib.Country class.</returns>
    [UsedImplicitly]
    public ISOLib.Country AsIsoCountry() {
        return ISOLib.Countries.GetByAlpha3Code(Alpha3);
    } 
}
