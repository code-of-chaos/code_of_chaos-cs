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
    /// This method is responsible for creating and configuring the EnvironmentSwitcher. The EnvironmentSwitcher is used for managing environment-specific configurations and settings.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <param name="action">The action that configures the EnvironmentSwitcherOptions.</param>
    /// <typeparam name="TEnvironmentSwitcher">The type of the EnvironmentSwitcher.</typeparam>
    /// <returns>The instance of EnvironmentSwitcher created and configured.</returns>
    [UsedImplicitly]
    public static TEnvironmentSwitcher CreateEnvironmentSwitcher<TEnvironmentSwitcher>(this WebApplicationBuilder builder, Action<EnvironmentSwitcherOptions> action) where TEnvironmentSwitcher : EnvironmentSwitcher, new() {
        builder.Configuration.AddEnvironmentVariables();// Else they won't be loaded

        var options = new EnvironmentSwitcherOptions(builder);
        action(options);

        var switcher = new TEnvironmentSwitcher {
            Variables = options.Variables,
            Configuration = builder.Configuration
        };
        builder.Services.AddSingleton(switcher);

        return switcher;
    }
    
    /// <summary>
    /// This method is responsible for creating and configuring the EnvironmentSwitcher. The EnvironmentSwitcher is used for managing environment-specific configurations and settings.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <param name="action">The action that configures the EnvironmentSwitcherOptions.</param>
    /// <returns>The instance of EnvironmentSwitcher created and configured.</returns>
    [UsedImplicitly]
    public static EnvironmentSwitcher CreateEnvironmentSwitcher(
        this WebApplicationBuilder builder,
        Action<EnvironmentSwitcherOptions> action
    ) => builder.CreateEnvironmentSwitcher<EnvironmentSwitcher>(action);
}
