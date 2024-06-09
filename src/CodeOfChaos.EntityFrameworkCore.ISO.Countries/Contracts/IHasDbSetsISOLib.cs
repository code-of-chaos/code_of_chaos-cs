// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.EntityFrameworkCore.ISO.Countries.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeOfChaos.EntityFrameworkCore.ISO.Countries.Contracts;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents an interface for accessing the database sets of ISO library entities.
/// </summary>
public interface IHasDbSetsIsoLib {
    /// <summary>
    /// Represents a country.
    /// </summary>
    public DbSet<Country> Countries { get; init; }
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public DbSet<Currency> Currencies { get; init; }
    /// <summary>
    /// Represents a set of languages in the ISO library.
    /// </summary>
    public DbSet<Language> Languages { get; init; }
}
