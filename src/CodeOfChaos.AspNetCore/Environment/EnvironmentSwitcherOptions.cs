// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;

namespace CodeOfChaos.AspNetCore.Environment;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// Represents the options for the EnvironmentSwitcher class.
/// </summary>
public class EnvironmentSwitcherOptions(WebApplicationBuilder builder) {
    /// <summary>
    /// Represents a class that provides access to environment variables used in the application.
    /// </summary>
    public EnvironmentVariables Variables { get; } = new(builder.Configuration);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Defines some pre- and ready made variables for the EnvironmentSwitcherOptions class.
    /// </summary>
    /// <returns>The updated EnvironmentSwitcherOptions instance.</returns>
    [UsedImplicitly] public EnvironmentSwitcherOptions DefinePreMadeVariables() {
        Variables.TryRegisterAllValuesPartialAllowed<PremadeEnvironmentVariablesBools, bool>();
        Variables.TryRegisterAllValuesPartialAllowed<PreMadeEnvironmentVariablesStrings, string>();

        return this;
    }
}
