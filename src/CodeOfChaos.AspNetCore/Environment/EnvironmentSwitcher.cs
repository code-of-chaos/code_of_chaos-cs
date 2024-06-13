// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
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
    [UsedImplicitly] public EnvironmentVariables Variables { get; internal set; } = null!;
}
