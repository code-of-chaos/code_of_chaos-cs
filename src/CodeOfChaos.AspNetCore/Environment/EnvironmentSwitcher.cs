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
public class EnvironmentSwitcher(EnvironmentSwitcherOptions options) {
    /// <summary>
    /// Represents a class that provides access to environment variables used in the application.
    /// </summary>
    [UsedImplicitly] public readonly EnvironmentVariables Variables = options.Variables;
}
