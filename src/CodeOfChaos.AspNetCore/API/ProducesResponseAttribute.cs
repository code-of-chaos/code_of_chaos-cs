// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Net;

namespace CodeOfChaos.AspNetCore.API;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Specifies the type of the HTTP response that a method produces.
/// </summary>
/// <typeparam name="T">The type of the HTTP response message.</typeparam>
/// <remarks>
/// This attribute is used to specify the type of the HTTP response that a method produces.
/// It is typically applied to an action method in a controller class.
/// The type specified by the attribute is used to generate a response for the consumer of the API.
/// The specified HTTP status code will be used in the response message.
/// </remarks>
/// <example>
/// The following example demonstrates the usage of the ProducesResponseTypeAttribute:
/// <code>
/// [HttpGet]
/// [ProducesResponseType(typeof(IEnumerable&lt;Product&gt;), (int)HttpStatusCode.OK)]
/// public IActionResult GetProducts()
/// {
/// var products = _productService.GetProducts();
/// return Ok(products);
/// }
/// </code>
/// </example>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute"/>
[UsedImplicitly]
public class ProducesResponseAttribute<T>(HttpStatusCode httpStatusCode) : Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute<T>((int)httpStatusCode);