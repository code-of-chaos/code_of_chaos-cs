// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeOfChaos.AspNetCore.Environment;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// This class is responsible for creating and configuring the EnvironmentSwitcher. The EnvironmentSwitcher is used for managing environment-specific configurations and settings.
/// </summary>
[UsedImplicitly]
public static class EnvironmentSwitcherBuilder {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// This class is responsible for creating and configuring the EnvironmentSwitcher. The EnvironmentSwitcher is used for managing environment-specific configurations and settings.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <param name="action">The action that configures the EnvironmentSwitcherOptions.</param>
    /// <returns>The instance of EnvironmentSwitcher created and configured.</returns>
    [UsedImplicitly] 
    public static EnvironmentSwitcher CreateEnvironmentSwitcher(this WebApplicationBuilder builder, Action<EnvironmentSwitcherOptions> action) {
        builder.Configuration.AddEnvironmentVariables(); // Else they won't be loaded
        
        var options = new EnvironmentSwitcherOptions(builder);
        action(options);
        
        var switcher = new EnvironmentSwitcher(options);
        builder.Services.AddSingleton(switcher);

        return switcher;
    }
}
