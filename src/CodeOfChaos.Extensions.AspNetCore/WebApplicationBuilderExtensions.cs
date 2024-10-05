// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace CodeOfChaos.Extensions.AspNetCore;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
///     Provides extension methods for the WebApplicationBuilder class.
/// </summary>
[UsedImplicitly]
public static class WebApplicationBuilderExtensions {
    /// <summary>
    /// Overrides the default logging configuration with SeriLog.
    /// Pre-configures the logger to output to the console.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <param name="configure">Extra Configuration setup</param>
    /// <returns>The web application builder with the logging configuration overridden.</returns>
    [UsedImplicitly]
    public static WebApplicationBuilder OverrideLoggingAsSeriLog(this WebApplicationBuilder builder, Action<LoggerConfiguration>? configure = null) {
        LoggerConfiguration loggerConfig = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(new LoggingLevelSwitch())
            .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
            .WriteTo.Async(lsc => lsc.Console());

        // If the user has provided a configuration, add it to the logger configuration.
        configure?.Invoke(loggerConfig);
        
        Log.Logger = loggerConfig.CreateLogger();
        
        builder.Logging.ClearProviders();// Removes the old Microsoft Logging
        builder.Logging.AddSerilog(Log.Logger);
        builder.Services.AddSingleton(Log.Logger);// Else Injecting from Serilog.ILogger won't work

        return builder;
    }
}
