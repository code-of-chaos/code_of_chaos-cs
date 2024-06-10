// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace CodeOfChaos.AspNetCore.API;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// Represents an abstract base API service.
/// </summary>
/// <typeparam name="TDbContext">The type of the entity framework database context.</typeparam>
public abstract class AbstractBaseApiService<TDbContext>(
    IDbContextFactory<TDbContext> contextFactory
    ) where TDbContext : DbContext {

    /// <summary>
    /// Represents the database context in Entity Framework Core.
    /// </summary>
    [UsedImplicitly]
    protected Task<TDbContext> DbContext => contextFactory.CreateDbContextAsync();
}