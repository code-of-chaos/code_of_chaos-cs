// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.ComponentModel;
using System.Net;

namespace CodeOfChaosAPI.Contracts;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents the result of an API request.
/// </summary>
/// <typeparam name="T">The type of the response data.</typeparam>
public interface IApiResult<T> {
    /// <summary>
    /// Represents the status code of the HTTP response.
    /// </summary>
    /// <remarks>
    /// This property indicates the status code of the HTTP response.
    /// </remarks>
    [Description("Status code of the HTTP Response")] [ReadOnly(true)]
    public HttpStatusCode Status { get; init; }

    /// <summary>
    /// Represents the result of an API operation.
    /// </summary>
    /// <typeparam name="T">The type of the data returned in the API result.</typeparam>
    [Description("Possible Message to explain error code")] [ReadOnly(true)]
    public string? Message { get; init; }

    /// <summary>
    /// Represents the result of an API call.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    [Description("Response data, can be more than one entry.")] [ReadOnly(true)]
    public T[] Data { get; init; }
}

/// <summary>
/// Represents the result of an API operation.
/// </summary>
public interface IApiResult : IApiResult<object>;
