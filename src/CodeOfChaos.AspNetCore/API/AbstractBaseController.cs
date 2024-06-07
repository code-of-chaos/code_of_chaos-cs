// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CodeOfChaos.AspNetCore.API;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public abstract class AbstractBaseController<TDbContext>(
    IDbContextFactory<TDbContext> contextFactory
    ) : Controller where TDbContext : DbContext {

    /// <summary>
    /// Represents the database context in Entity Framework Core.
    /// </summary>
    [UsedImplicitly]
    protected Task<TDbContext> DbContext => contextFactory.CreateDbContextAsync();

    // -----------------------------------------------------------------------------------------------------------------
    // Result Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Returns an <see cref="IActionResult"/> representing a successful API response with data.
    /// </summary>
    /// <typeparam name="T">The type of data in the response.</typeparam>
    /// <param name="objects">The data to include in the response.</param>
    /// <returns>An <see cref="IActionResult"/> representing a successful API response with data.</returns>
    [UsedImplicitly]
    protected static IActionResult Success<T>(params T[] objects) => new JsonResult(ApiResult<T>.Success(objects));
    
    /// <summary>
    /// Method to return a successful API result.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="msg">The message to be included in the result. Default is null.</param>
    /// <param name="objects">The response data objects.</param>
    /// <returns>An IActionResult object representing a successful API result.</returns>
    [UsedImplicitly]
    protected static IActionResult Success<T>(string? msg = null, params T[] objects) => new JsonResult(ApiResult<T>.Success(null, msg, objects));
    
    /// <summary>
    /// Returns an IActionResult object representing a successful API result.
    /// </summary>
    /// <typeparam name="T">The type of the objects to be included in the response data.</typeparam>
    /// <param name="status">The HTTP status code of the response. If null, the default value HttpStatusCode.OK is used.</param>
    /// <param name="msg">An optional message to be included in the response.</param>
    /// <param name="objects">The objects to be included in the response data.</param>
    /// <returns>An IActionResult object representing a successful API result.</returns>
    [UsedImplicitly]
    protected static IActionResult Success<T>(HttpStatusCode? status = null, string? msg = null, params T[] objects) => new JsonResult(ApiResult<T>.Success(status, msg, objects));

    // Only use these if no data has to be sent back to the client
    /// <summary>
    /// Returns an IActionResult object with a successful response.
    /// </summary>
    /// <returns>An IActionResult object representing a successful response.</returns>
    [UsedImplicitly]
    protected static IActionResult Success() => new JsonResult(ApiResult.Success([]));
    
    /// <summary>
    /// Returns a success result with the given objects.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing a success result.</returns>
    [UsedImplicitly]
    protected static IActionResult Success(string msg) => new JsonResult(ApiResult.Success(null, msg, []));
    
    /// <summary>
    /// Returns a successful API result with data.
    /// </summary>
    /// <returns>The successful API result with the specified data.</returns>
    [UsedImplicitly]
    protected static IActionResult Success(HttpStatusCode status, string? msg = null) => new JsonResult(ApiResult.Success(status, msg, []));

    /// <summary>
    /// Returns a failure response indicating a client-side error.
    /// </summary>
    /// <param name="status">The status code of the failure response. If not specified, the default value is HttpStatusCode.BadRequest.</param>
    /// <param name="msg">An optional message to explain the error code.</param>
    /// <returns>A JsonResult representing the failure response.</returns>
    [UsedImplicitly]
    protected static IActionResult FailureClient(HttpStatusCode? status = null, string? msg = null) => new JsonResult(ApiResult.FailureClient(status, msg));
    
    /// <summary>
    /// Method that returns a failure server response.
    /// </summary>
    /// <param name="status">The status code of the response. Defaults to HttpStatusCode.InternalServerError.</param>
    /// <param name="msg">Optional message to explain the error. Defaults to null.</param>
    /// <returns>A JsonResult object representing the failure server response.</returns>
    [UsedImplicitly]
    protected static IActionResult FailureServer(HttpStatusCode? status = null, string? msg = null) => new JsonResult(ApiResult.FailureServer(status, msg));
}
