// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.AspNetCore;
using JetBrains.Annotations;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Builder;
using Serilog.Core;
using ILogger=Serilog.ILogger;

namespace CodeOfChaos.Tests.Extensions.AspNetCore;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(WebApplicationBuilderExtensions))]
public class WebApplicationBuilderExtensionsTests {
    [Fact]
    public void OverrideLoggingAsSeriLog_AddsSerilogToServicesAndClearsOtherProviders() {
        // Arrange
        WebApplicationBuilder webHostBuilder = WebApplication.CreateBuilder([]);

        //Act
        webHostBuilder.OverrideLoggingAsSeriLog();

        // Assert
        object? logger = webHostBuilder.Services.Single(descriptor => descriptor.ServiceType == typeof(ILogger)).ImplementationInstance;
        Assert.NotNull(logger);
        Assert.IsType<Logger>(logger);
    }
}