// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Serilog;
using JetBrains.Annotations;
using Serilog;
using System;
using System.Threading.Tasks;
using Xunit;
using LoggerExtensions=CodeOfChaos.Extensions.Serilog.LoggerExtensions;

namespace CodeOfChaos.Tests.Extensions.Serilog;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

[TestSubject(typeof(LoggerExtensions))]
public class LoggerExtensionsTest {

    [Fact]
    public async Task ThrowFatal_ShouldThrowException() {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.Fatal(It.IsAny<Exception>(), It.IsAny<string>(), Array.Empty<object>()));

        await Assert.ThrowsAsync<Exception>(() => throw mockLogger.Object.ThrowFatal("This is a fatal error"));
    }

    [Fact]
    public async Task ThrowFatal_Generic_ShouldThrowExceptionOfType() {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.Fatal(It.IsAny<Exception>(), It.IsAny<string>(), Array.Empty<object>()));

        await Assert.ThrowsAsync<ArgumentException>(() => throw mockLogger.Object.ThrowFatal<ArgumentException>("This is a fatal error"));
    }

    [Fact]
    public async Task ThrowFatal_WithException_ShouldThrowProvidedException() {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.Fatal(It.IsAny<Exception>(), It.IsAny<string>(), new object[0]));
        var exception = new ArgumentNullException();
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => throw mockLogger.Object.ThrowFatal(exception,"This is a fatal error"));
    }

    [Fact]
    public void ExitFatal_ShouldExitApplicationWithSpecifiedExitCode() {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.Fatal(It.IsAny<string>(), new object[0]));

        Assert.Throws<ExitApplicationException>(() => mockLogger.Object.ExitFatal(1, "Exiting application with exit code {ExitCode}", 1));
    }
}
