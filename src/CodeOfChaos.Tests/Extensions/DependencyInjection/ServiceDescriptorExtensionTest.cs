// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace CodeOfChaos.Tests.Extensions.DependencyInjection;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

[TestSubject(typeof(ServiceDescriptorExtension))]
public class ServiceDescriptorExtensionTest {
    [Fact]
    public void NewServiceDescriptor_TypeParameters_ReturnsExpected() {
        // Arrange
        const ServiceLifetime lifetime = ServiceLifetime.Scoped;

        // Act
        ServiceDescriptor result = ServiceDescriptorExtension.NewServiceDescriptor<IDisposable, DateTimeProvider>(lifetime);

        // Assert
        Assert.Equal(typeof(IDisposable), result.ServiceType);
        Assert.Equal(typeof(DateTimeProvider), result.ImplementationType);
        Assert.Equal(lifetime, result.Lifetime);
    }

    [Fact]
    public void NewServiceDescriptor_InstanceParameter_ReturnsExpected() {
        // Arrange
        var instance = new DateTimeProvider();

        // Act
        ServiceDescriptor result = ServiceDescriptorExtension.NewServiceDescriptor<IDisposable>(instance);

        // Assert
        Assert.Equal(typeof(IDisposable), result.ServiceType);
        Assert.Null(result.ImplementationType);
        Assert.Equal(instance, result.ImplementationInstance);
    }

    private class DateTimeProvider : IDisposable {
        public void Dispose() {}
    }
}