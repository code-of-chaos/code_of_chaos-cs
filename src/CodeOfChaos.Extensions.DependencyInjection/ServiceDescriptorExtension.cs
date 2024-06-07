// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace CodeOfChaos.Extensions.DependencyInjection;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
///     Extension methods for the <see cref="ServiceDescriptor" /> class.
/// </summary>
[UsedImplicitly]
public static class ServiceDescriptorExtension {
    
    /// <summary>
    ///     Creates a new instance of <see cref="ServiceDescriptor" /> for a service that will be resolved using the specified
    ///     implementation type.
    /// </summary>
    /// <typeparam name="T1">The service type.</typeparam>
    /// <typeparam name="T2">The implementation type.</typeparam>
    /// <param name="lifetime">The lifetime of the service.</param>
    /// <returns>A new instance of <see cref="ServiceDescriptor" />.</returns>
    [UsedImplicitly]
    
    public static ServiceDescriptor NewServiceDescriptor<T1, T2>(ServiceLifetime lifetime) where T2 : T1 => new(typeof(T1), typeof(T2), lifetime);
    /// <summary>
    ///     Creates a new <see cref="ServiceDescriptor" /> with the specified service type, implementation type, and lifetime.
    /// </summary>
    /// <typeparam name="T1">The service type.</typeparam>
    /// <param name="instance">The instance of the service to be stored</param>
    /// <returns>
    ///     A new instance of <see cref="ServiceDescriptor" /> with the specified service type, implementation type, and
    ///     lifetime.
    /// </returns>
    [UsedImplicitly]
    public static ServiceDescriptor NewServiceDescriptor<T1>(object instance) => new(typeof(T1), null, instance);
}
