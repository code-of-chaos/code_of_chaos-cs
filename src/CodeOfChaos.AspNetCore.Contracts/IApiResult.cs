// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using System.ComponentModel;
using System.Net;

namespace CodeOfChaos.AspNetCore.Contracts;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents the result of an API request.
/// </summary>
/// <typeparam name="T">The type of the response data.</typeparam>
[UsedImplicitly]
public interface IApiResult<T> {
    /// <summary>
    /// Represents the status code of the HTTP response.
    /// </summary>
    /// <remarks>
    /// This property indicates the status code of the HTTP response.
    /// </remarks>
    [Description("Status code of the HTTP Response")] [ReadOnly(true)]
    [UsedImplicitly]
    public HttpStatusCode Status { get; init; }

    /// <summary>
    /// Represents the result of an API operation.
    /// </summary>
    [Description("Possible Message to explain error code")] [ReadOnly(true)]
    [UsedImplicitly]
    public string? Message { get; init; }

    /// <summary>
    /// Represents the result of an API call.
    /// </summary>
    [Description("Response data, can be more than one entry.")] [ReadOnly(true)]
    [UsedImplicitly]
    public T[] Data { get; init; }
}

/// <summary>
/// Represents the result of an API operation.
/// </summary>
[UsedImplicitly]
public interface IApiResult : IApiResult<object>;
