// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CodeOfChaos.AspNetCore.Environment;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// The EnvironmentSwitcher class is responsible for managing environment-specific configurations and settings.
/// </summary>
public class EnvironmentSwitcher(ILogger logger, WebApplicationBuilder builder) {
    /// <summary>
    /// Represents a class that provides access to environment variables used in the application.
    /// </summary>
    private readonly EnvironmentVariables _variables = new(builder.Configuration);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Checks whether the application is running in a Docker container.
    /// </summary>
    /// <returns>
    /// True if the application is running in a Docker container; otherwise, false.
    /// </returns>
    [UsedImplicitly]
    public bool IsRunningInDocker() => _variables.RunningInDocker;
    
    /// <summary>
    /// Retrieves the database connection string based on the running environment.
    /// </summary>
    /// <returns>
    /// The database connection string.
    /// </returns>
    /// <exception cref="ApplicationException">Thrown when no connection string could be determined.</exception>
    [UsedImplicitly]
    public string GetDatabaseConnectionString() {
        if (_variables.RunningInDocker) {
            // Program delivering "builder" is running in a docker container
            return _variables.DockerDb;
        }

        // Program delivering "builder" is NOT running in a docker container
        //      AKA: probably in some local dev's test environment
        if (_variables.TryGetDevelopmentDb(out string? value)) return value;
        if (builder.Configuration.GetConnectionString("DefaultConnection") is {} defaultConnectionString) return defaultConnectionString;

        // All possible routes exhausted
        logger.ThrowFatal<ApplicationException>("No Connection string could be determined");
        return string.Empty;// TODO check why ThrowFatal doesn't NOT RETURN for the IDE
    }

    /// <summary>
    /// Retrieves the SSL certificate location from the environment variables.
    /// </summary>
    /// <returns>The SSL certificate location.</returns>
    [UsedImplicitly]
    public string GetSslCertLocation() => _variables.SslCertLocation;// currently only set through the same ENV variable whether in Docker or dev
    /// <summary>
    /// Retrieves the SSL certificate password from the environment variables.
    /// </summary>
    /// <returns>The SSL certificate password.</returns>
    [UsedImplicitly]
    public string GetSslCertPassword() => _variables.SslCertPassword;

    /// <summary>
    /// Gets the Twitch client ID from the environment variables.
    /// </summary>
    /// <returns>The Twitch client ID.</returns>
    [UsedImplicitly]
    public string GetTwitchClientId() => _variables.TwitchClientId;
    /// <summary>
    /// Gets the Twitch client secret from the environment variables.
    /// </summary>
    /// <returns>The Twitch client secret.</returns>
    [UsedImplicitly]
    public string GetTwitchClientSecret() => _variables.TwitchClientSecret;
}
