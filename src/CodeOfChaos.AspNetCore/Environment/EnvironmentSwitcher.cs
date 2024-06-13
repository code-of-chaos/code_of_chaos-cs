// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace CodeOfChaos.AspNetCore.Environment;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// The EnvironmentSwitcher class is responsible for managing environment-specific configurations and settings.
/// </summary>
[UsedImplicitly]
public class EnvironmentSwitcher {
    /// <summary>
    /// Represents a class that provides access to environment variables used in the application.
    /// </summary>
    [UsedImplicitly] public EnvironmentVariables Variables { get; internal init; } = null!;
    /// <summary>
    /// The Builder class is responsible for creating and configuring an instance of EnvironmentSwitcher.
    /// </summary>
    [UsedImplicitly] public IConfiguration Configuration { get; internal init; } = null!;
}
