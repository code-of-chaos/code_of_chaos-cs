// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.AspNetCore.Environment;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// Enum for predefined environment variable strings.
/// </summary>
[UsedImplicitly]
public enum PreMadeEnvironmentVariablesStrings {
    /// <summary>
    /// Represents the <c>SslCertLocation</c> member of the <see cref="PreMadeEnvironmentVariablesStrings"/> enumeration.
    /// </summary>
    [UsedImplicitly] SslCertLocation,

    /// <summary>
    /// Represents the <c>SslCertPassword</c> member of the <see cref="PreMadeEnvironmentVariablesStrings"/> enumeration.
    /// </summary>
    [UsedImplicitly] SslCertPassword,
}

// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Enum for predefined boolean environment variables.
/// </summary>
[UsedImplicitly]
public enum PremadeEnvironmentVariablesBools {
    /// <summary>
    /// Represents the <c>RunningInDocker</c> member of the <see cref="PremadeEnvironmentVariablesBools"/> enumeration.
    /// </summary>
    [UsedImplicitly] RunningInDocker,

    /// <summary>
    /// Represents the `MadeByAndreasSas` member of the `PremadeEnvironmentVariablesBools` enumeration.
    /// </summary>
    [UsedImplicitly] MadeByAndreasSas
}