// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.EntityFrameworkCore.ISO.Countries.Contracts;
using CodeOfChaos.EntityFrameworkCore.ISO.Countries.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CodeOfChaos.EntityFrameworkCore.ISO.Countries;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// The PopulateDbIsoLib class is responsible for populating the database with ISO library entities.
/// </summary>
/// <typeparam name="T">The database context type that implements the IHasDbSetsIsoLib interface.</typeparam>
[UsedImplicitly]
public class PopulateDbIsoLib<T>(T dbContext, ILogger logger) where T : DbContext, IHasDbSetsIsoLib {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Populates the database with ISO library entities including currencies, languages, and countries.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [UsedImplicitly] 
    public async Task PopulateAll() {
        await dbContext.Database.BeginTransactionAsync();

        try {
            await PopulateCurrenciesAsync();
            await PopulateLanguagesAsync();
            await PopulateCountriesAsync();

            await dbContext.Database.CommitTransactionAsync();
        }
        
        catch (Exception ex) {
            logger.Error(ex, "Exception occured, rolling back transaction ...");
            await dbContext.Database.RollbackTransactionAsync();
        }
    }

    /// <summary>
    /// Populates the currencies in the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [UsedImplicitly]
    public async Task PopulateCurrenciesAsync() {
         await dbContext.Currencies.AddRangeAsync(
             ISOLib.Currencies.Collection.Select(Currency.CreateFromIsoLib)
         );
    }


    /// <summary>
    /// Populates the languages in the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [UsedImplicitly]
    public async Task PopulateLanguagesAsync() {
        await dbContext.Languages.AddRangeAsync(
            ISOLib.Languages.Collection.Select(Language.CreateFromIsoLib)
        );
    }

    /// <summary>
    /// Populates the countries table in the database with data from the ISOLib.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [UsedImplicitly]
    public async Task PopulateCountriesAsync() {
        IEnumerable<Task<Country>> countryTasks = ISOLib.Countries.Collection
              .Select(async isoCountry => {
                   Task<Currency>[] currencyTasks = isoCountry.Currencies
                        .Select(alpha3 => dbContext.Currencies.FirstAsync(c => c.Alpha3 == alpha3))
                        .ToArray();
                   Task<Language>[] languageTasks = isoCountry.Languages
                        .Select(alpha3 => dbContext.Languages.FirstAsync(l => l.Alpha3 == alpha3))
                        .ToArray();
                  Currency[] currencies = await Task.WhenAll(currencyTasks);
                  Language[] languages = await Task.WhenAll(languageTasks);

                  return Country.CreateFromIsoLib(isoCountry, currencies, languages);
              });

        Country[] countries = await Task.WhenAll(countryTasks);

        dbContext.Countries.AddRange(countries);
    }
}
