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
/// Represents a currency.
/// </summary>
public class Currency {
    /// <summary>
    /// Represents a currency.
    /// </summary>
    [Key] public string Alpha3 { get; init; } = null!;
    /// <summary>
    /// Gets or sets the name of the currency.
    /// </summary>
    public string Name { get; init; } = null!;
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public string Number { get; init; } = null!;
    /// <summary>
    /// Gets or sets the number of decimal places in the currency's minor unit.
    /// </summary>
    public int MinorUnit { get; init; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Creates a Currency object from an ISOLib.Currency object.
    /// </summary>
    /// <param name="currency">The ISOLib.Currency object to create from.</param>
    /// <returns>A new Currency object.</returns>
    public static Currency CreateFromIsoLib(ISOLib.Currency currency) {
        return new Currency {
            Alpha3=currency.Alpha3,
            Name=currency.Name,
            Number=currency.Number,
            MinorUnit=currency.MinorUnit
        };
    }

    /// <summary>
    /// Retrieves the ISO currency object associated with the current Currency instance.
    /// </summary>
    /// <returns>The ISO currency object associated with the current Currency instance.</returns>
    [UsedImplicitly]
    public ISOLib.Currency AsIsoCurrency() {
        return ISOLib.Currencies.GetByAlpha3Code(Alpha3);
    }
}
