// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.AspNetCore.API;
using JetBrains.Annotations;
using System.Net;
using Xunit;

namespace CodeOfChaos.Tests.AspNetCore.API;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(ApiResult))]
public class ApiResultTest {
    // Testing method FailureServer
    [Fact]
    public void FailureServer_ShouldReturn_InternalServerError_WhenCalledWithoutParameters() {
        ApiResult<int> result = ApiResult<int>.FailureServer();
        Assert.Equal(HttpStatusCode.InternalServerError, result.Status);
        Assert.Null(result.Message);
        Assert.Empty(result.Data);
    }
    [Theory]
    [InlineData(HttpStatusCode.BadRequest, "Bad Request")]
    [InlineData(HttpStatusCode.Unauthorized, "Unauthorized")]
    private void FailureServer_ShouldReturn_CorrectValues_WhenCalledWithParameters(HttpStatusCode status, string msg) {
        ApiResult<int> result = ApiResult<int>.FailureServer(status, msg);
        Assert.Equal(status, result.Status);
        Assert.Equal(msg, result.Message);
        Assert.Empty(result.Data);
    }

    // Testing method FailureClient
    [Fact]
    public void FailureClient_ShouldReturn_BadRequest_WhenCalledWithoutParameters() {
        ApiResult<int> result = ApiResult<int>.FailureClient();
        Assert.Equal(HttpStatusCode.BadRequest, result.Status);
        Assert.Null(result.Message);
        Assert.Empty(result.Data);
    }
    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Not Found")]
    [InlineData(HttpStatusCode.Unauthorized, "Unauthorized")]
    private void FailureClient_ShouldReturn_CorrectValues_WhenCalledWithParameters(HttpStatusCode status, string msg) {
        ApiResult<int> result = ApiResult<int>.FailureClient(status, msg);
        Assert.Equal(status, result.Status);
        Assert.Equal(msg, result.Message);
        Assert.Empty(result.Data);
    }

    // Testing method Success
    [Fact]
    public void Success_ShouldReturn_OK_WithEmpty_DataObjects_WhenCalledWithoutObjects() {
        ApiResult<int> result = ApiResult<int>.Success();
        Assert.Equal(HttpStatusCode.OK, result.Status);
        Assert.Equal("", result.Message);
        Assert.Empty(result.Data);
    }
    [Theory]
    [InlineData(HttpStatusCode.Created, "Created", new[] { 1, 2, 3 })]
    [InlineData(HttpStatusCode.Accepted, "Accepted", new[] { 4, 5, 6 })]
    public void Success_ShouldReturn_CorrectValues_WhenCalledWithParameters(HttpStatusCode status, string msg, int[] data) {
        ApiResult<int> result = ApiResult<int>.Success(status, msg, data);
        Assert.Equal(status, result.Status);
        Assert.Equal(msg, result.Message);
        Assert.Equal(data, result.Data);
    }
}
