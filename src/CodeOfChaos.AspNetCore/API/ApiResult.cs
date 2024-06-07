// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.AspNetCore.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace CodeOfChaos.AspNetCore.API;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents the result of an API request.
/// </summary>
/// <typeparam name="T">The type of the response data.</typeparam>
[UsedImplicitly]
public record ApiResult<T>(
    [property: Description("Status code of the HTTP Response")] [property: ReadOnly(true)]
    HttpStatusCode Status,
    [property: Description("Possible Message to explain error code")] [property: ReadOnly(true)]
    string? Message,
    [property: Description("Response data, can be more than one entry.")] [property: ReadOnly(true)]
    T[] Data
    ) : IApiResult<T> {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Method that returns a failure server response.
    /// </summary>
    /// <param name="status">The status code of the response. Defaults to HttpStatusCode.InternalServerError.</param>
    /// <param name="msg">Optional message to explain the error. Defaults to null.</param>
    /// <returns>A JsonResult object representing the failure server response.</returns>
    /// <remarks>
    /// This method is used to generate a failure server response with the specified status code and message.
    /// It returns a JsonResult object that can be returned by a controller action method.
    /// </remarks>
    public static ApiResult<T> FailureServer(HttpStatusCode? status = null, string? msg = null) => new(status ?? HttpStatusCode.InternalServerError, msg, []);

    /// <summary>
    /// Returns a failure response indicating a client-side error.
    /// </summary>
    /// <param name="status">The status code of the failure response. If not specified, the default value is HttpStatusCode.BadRequest.</param>
    /// <param name="msg">An optional message to explain the error code.</param>
    /// <returns>A JsonResult representing the failure response.</returns>
    public static ApiResult<T> FailureClient(HttpStatusCode? status = null, string? msg = null) => new(status ?? HttpStatusCode.BadRequest, msg, []);

    /// <summary>
    /// Returns an <see cref="IActionResult"/> representing a successful API response with data.
    /// </summary>
    /// <param name="objects">The data to include in the response.</param>
    /// <returns>An <see cref="IActionResult"/> representing a successful API response with data.</returns>
    public static ApiResult<T> Success(params T[] objects) => Success(null, null, objects);
    /// <summary>
    /// Method to return a successful API result.
    /// </summary>
    /// <param name="status">The status code of the failure response. If not specified, the default value is HttpStatusCode.BadRequest.</param>
    /// <param name="msg">The message to be included in the result. Default is null.</param>
    /// <param name="objects">The response data objects.</param>
    /// <returns>An IActionResult object representing a successful API result.</returns>
    public static ApiResult<T> Success(HttpStatusCode? status = null, string? msg = null, params T[] objects) => new(status ?? HttpStatusCode.OK, msg ?? "", objects);
}

/// <summary>
/// Represents the result of an API request.
/// </summary>
/// <remarks>
/// This class is used to encapsulate the result of an empty API result. It contains the status code, message, and response data.
/// </remarks>
[UsedImplicitly]
public record ApiResult(HttpStatusCode Status, string? Message, object[] Data) : ApiResult<object>(Status, Message, Data);
