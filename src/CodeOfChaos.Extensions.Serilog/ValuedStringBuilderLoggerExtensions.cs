// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Serilog;
using Serilog.Core;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.Extensions.Serilog;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Provides extension methods for Serilog ILogger to log messages using a ValuedStringBuilder.
/// </summary>
[UsedImplicitly]
public static class ValuedStringBuilderLoggerExtensions {
    /// <summary>
    /// Contains extension methods for Serilog's ILogger interface that provide a Verbose logging level with the ability to insert values using a ValuedStringBuilder.
    /// </summary>
    /// <param name="logger">The logger to write the log message to.</param>
    /// <param name="builder">The ValuedStringBuilder instance containing the log message and property values.</param>
    [UsedImplicitly] public static void Verbose(this ILogger logger, ValuedStringBuilder builder) {
        logger.Verbose(builder.ToString(), builder.ValuesToArray());
    }
    /// <summary>
    /// Adds a verbose-level log message to the logger using the specified ValuedStringBuilder.
    /// </summary>
    /// <param name="logger">The logger to write the log message to.</param>
    /// <param name="builder">The ValuedStringBuilder instance containing the log message and property values.</param>
    [UsedImplicitly] public static void Debug(this ILogger logger, ValuedStringBuilder builder) {
        logger.Debug(builder.ToString(), builder.ValuesToArray());
    }
    /// <summary>
    /// Provides extension methods for ILogger to log messages using a ValuedStringBuilder.
    /// </summary>
    /// <param name="logger">The logger to write the log message to.</param>
    /// <param name="builder">The ValuedStringBuilder instance containing the log message and property values.</param>
    [UsedImplicitly] public static void Information(this ILogger logger, ValuedStringBuilder builder) {
        logger.Information(builder.ToString(), builder.ValuesToArray());
    }
    /// <summary>
    /// Extension methods for the Serilog ILogger interface that provide additional logging methods using ValuedStringBuilder.
    /// </summary>
    /// <param name="logger">The logger to write the log message to.</param>
    /// <param name="builder">The ValuedStringBuilder instance containing the log message and property values.</param>
    [UsedImplicitly] public static void Warning(this ILogger logger, ValuedStringBuilder builder) {
        logger.Warning(builder.ToString(), builder.ValuesToArray());
    }
    /// <summary>
    /// Contains extension methods for the Serilog ILogger interface to enhance logging with valued string builders.
    /// </summary>
    /// <param name="logger">The logger to write the log message to.</param>
    /// <param name="builder">The ValuedStringBuilder instance containing the log message and property values.</param>
    [UsedImplicitly] public static void Error(this ILogger logger, ValuedStringBuilder builder) {
        logger.Error(builder.ToString(), builder.ValuesToArray());
    }
    /// <summary>
    /// Extension methods for the Serilog ILogger interface that provide additional logging methods using ValuedStringBuilder.
    /// </summary>
    /// <param name="logger">The logger to write the log message to.</param>
    /// <param name="builder">The ValuedStringBuilder instance containing the log message and property values.</param>
    [UsedImplicitly] public static void Fatal(this ILogger logger, ValuedStringBuilder builder) {
        logger.Fatal(builder.ToString(), builder.ValuesToArray());
    }
}
