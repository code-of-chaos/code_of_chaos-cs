// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.AspNetCore.Environment;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents a class that provides access to environment variables used in the application.
/// </summary>
public class EnvironmentVariables(IConfigurationManager configuration) {
    /// <summary>
    /// Represents a property that indicates whether the program is running inside a Docker container.
    /// </summary>
    public bool RunningInDocker => bool.TryParse(configuration["RunningInDocker"], out bool output) && output;

    /// <summary>
    /// Represents the DockerDb property that provides access to the Docker database information.
    /// </summary>
    public string DockerDb => GetRequiredEnvVar(nameof(DockerDb));

    public string DockerApi => GetRequiredEnvVar(nameof(DockerApi));

    /// <summary>
    /// Represents a class that provides access to development environment variables.
    /// </summary>
    public string DevelopmentDb => GetRequiredEnvVar(nameof(DevelopmentDb));

    /// <summary>
    /// Provides access to development environment variables.
    /// </summary>
    public string DevelopmentApi => GetRequiredEnvVar(nameof(DevelopmentApi));

    /// <summary>
    /// Represents the SSL certificate location.
    /// </summary>
    /// <remarks>
    /// This property retrieves the SSL certificate location from the environment variables.
    /// </remarks>
    public string SslCertLocation => GetRequiredEnvVar(nameof(SslCertLocation));

    /// <summary>
    /// Represents the SSL certificate password retrieved from environment variables.
    /// </summary>
    public string SslCertPassword => GetRequiredEnvVar(nameof(SslCertPassword));

    /// <summary>
    /// Represents the Twitch Client ID retrieved from the environment variables.
    /// </summary>
    public string TwitchClientId => GetRequiredEnvVar(nameof(TwitchClientId));

    /// <summary>
    /// Represents the Twitch Client Secret.
    /// </summary>
    public string TwitchClientSecret => GetRequiredEnvVar(nameof(TwitchClientSecret));
    /// <summary>
    /// Tries to get the value of the DockerDb environment variable.
    /// </summary>
    /// <param name="value">When this method returns, contains the value of the DockerDb environment variable if it is found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the DockerDb environment variable is found; otherwise, <c>false</c>.</returns>
    [UsedImplicitly]
    public bool TryGetDockerDb([NotNullWhen(true)] out string? value) => TryGetEnvVar(nameof(DockerDb), out value);
    /// <summary>
    /// Tries to get the value of the Docker API environment variable.
    /// </summary>
    /// <param name="value">The output parameter that will hold the value of the Docker API environment variable, if found.</param>
    /// <returns><c>true</c> if the Docker API environment variable is found and <paramref name="value"/> is set; otherwise, <c>false</c>.</returns>
    [UsedImplicitly]
    public bool TryGetDockerApi([NotNullWhen(true)] out string? value) => TryGetEnvVar(nameof(DockerApi), out value);
    /// <summary>
    /// Tries to get the value of the DevelopmentDb environment variable.
    /// </summary>
    /// <param name="value">When this method returns, contains the value of the DevelopmentDb environment variable, if it exists; otherwise, null.</param>
    /// <returns>true if the DevelopmentDb environment variable exists; otherwise, false.</returns>
    [UsedImplicitly]
    public bool TryGetDevelopmentDb([NotNullWhen(true)] out string? value) => TryGetEnvVar(nameof(DevelopmentDb), out value);
    /// <summary>
    /// Tries to get the value of the DevelopmentApi environment variable.
    /// </summary>
    /// <param name="value">The value of the DevelopmentApi environment variable, if it exists; otherwise, null.</param>
    /// <returns>True if the DevelopmentApi environment variable exists; otherwise, false.</returns>
    [UsedImplicitly]
    public bool TryGetDevelopmentApi([NotNullWhen(true)] out string? value) => TryGetEnvVar(nameof(DevelopmentApi), out value);
    /// <summary>
    /// Tries to get the value of the SslCertLocation environment variable.
    /// </summary>
    /// <param name="value">When this method returns, contains the value of the SslCertLocation environment variable, if it is found; otherwise, null. This parameter is passed uninitialized.</param>
    /// <returns>true if the SslCertLocation environment variable is found; otherwise, false.</returns>
    [UsedImplicitly]
    public bool TryGetSslCertLocation([NotNullWhen(true)] out string? value) => TryGetEnvVar(nameof(SslCertLocation), out value);
    /// <summary>
    /// Tries to get the value of the SSL certificate password from the environment variables.
    /// </summary>
    /// <param name="value">The output parameter that will contain the password if it is found.</param>
    /// <returns>
    /// True if the SSL certificate password is found and set in the environment variables,
    /// false otherwise.</returns>
    [UsedImplicitly]
    public bool TryGetSslCertPassword([NotNullWhen(true)] out string? value) => TryGetEnvVar(nameof(SslCertPassword), out value);
    /// <summary>
    /// Tries to get the Twitch Client ID environment variable value.
    /// </summary>
    /// <param name="value">When this method returns, contains the value of the Twitch Client ID environment variable, if it is found; otherwise, null.</param>
    /// <returns>true if the Twitch Client ID environment variable is found; otherwise, false.</returns>
    [UsedImplicitly]
    public bool TryGetTwitchClientId([NotNullWhen(true)] out string? value) => TryGetEnvVar(nameof(TwitchClientId), out value);
    /// <summary>
    /// Tries to get the Twitch client secret from the environment variables.
    /// </summary>
    /// <param name="value">The value of the Twitch client secret if found, or null if not found.</param>
    /// <returns>True if the Twitch client secret was found, false otherwise.</returns>
    [UsedImplicitly]
    public bool TryGetTwitchClientSecret([NotNullWhen(true)] out string? value) => TryGetEnvVar(nameof(TwitchClientSecret), out value);


    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the environment variable specified by <paramref name="env"/> is not set.
    /// </summary>
    /// <param name="env">The name of the environment variable to check.</param>
    /// <returns>Returns an <see cref="ArgumentException"/> if the environment variable is not set.</returns>
    private static ArgumentException NotSet(string env) => new($"Environment variable * {env} * was not set");
    /// <summary>
    /// Retrieves the value of the specified environment variable. Throws an exception if the environment variable is not set.
    /// </summary>
    /// <param name="env">The name of the environment variable to retrieve.</param>
    /// <returns>The value of the specified environment variable.</returns>
    private string GetRequiredEnvVar(string env) => configuration[env] ?? throw NotSet(env);
    /// <summary>
    /// Tries to get the value of the specified environment variable.
    /// </summary>
    /// <param name="env">The name of the environment variable.</param>
    /// <param name="value">When this method returns, contains the value of the environment variable, if it is found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the environment variable is found; otherwise, <c>false</c>.</returns>
    private bool TryGetEnvVar(string env, [NotNullWhen(true)] out string? value) {
        if (configuration[env] is not {} v) {
            value = null;
            return false;
        }
        value = v;
        return true;
    }
}
