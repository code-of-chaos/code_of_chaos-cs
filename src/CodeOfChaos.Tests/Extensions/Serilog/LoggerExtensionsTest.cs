// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Serilog;
using JetBrains.Annotations;
using Serilog;
using System;
using Xunit;
using LoggerExtensions=CodeOfChaos.Extensions.Serilog.LoggerExtensions;

namespace CodeOfChaos.Tests.Extensions.Serilog;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

[TestSubject(typeof(LoggerExtensions))]
public class LoggerExtensionsTest {

    [Fact]
    public void ThrowFatal_ShouldThrowException() {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.Fatal(It.IsAny<Exception>(), It.IsAny<string>(), new object[0]));

        Assert.Throws<Exception>(() => mockLogger.Object.ThrowFatal("This is a fatal error"));
    }

    [Fact]
    public void ThrowFatal_Generic_ShouldThrowExceptionOfType() {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.Fatal(It.IsAny<Exception>(), It.IsAny<string>(), new object[0]));

        Assert.Throws<ArgumentException>(() => mockLogger.Object.ThrowFatal<ArgumentException>("This is a fatal error"));
    }

    [Fact]
    public void ThrowFatal_WithException_ShouldThrowProvidedException() {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.Fatal(It.IsAny<Exception>(), It.IsAny<string>(), new object[0]));
        var exception = new ArgumentNullException();

        Assert.Throws<ArgumentNullException>(() => mockLogger.Object.ThrowFatal(exception, "This is a fatal error"));
    }

    [Fact]
    public void ExitFatal_ShouldExitApplicationWithSpecifiedExitCode() {
        var mockLogger = new Mock<ILogger>();
        mockLogger.Setup(l => l.Fatal(It.IsAny<string>(), new object[0]));

        Assert.Throws<ExitApplicationException>(() => mockLogger.Object.ExitFatal(1, "Exiting application with exit code {ExitCode}", 1));
    }
}
