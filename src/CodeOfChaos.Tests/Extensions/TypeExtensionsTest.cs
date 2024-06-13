// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodeOfChaos.Tests.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(TypeExtensions))]
public class TypeExtensionsTest {
    [Fact]
    public void ExtractByTypeTest_NormalTypes() {
        // Arrange
        var types = new List<Type> { typeof(int), typeof(double), typeof(string), typeof(decimal) };
        var mockedTypes = new Mock<IEnumerable<Type>>();
        mockedTypes.Setup(mt => mt.GetEnumerator()).Returns(types.GetEnumerator());

        // Act
        Type[] result = mockedTypes.Object.ExtractByType<double>().ToArray();

        // Assert
        Assert.Single(result);
        Assert.Equal(typeof(double), result.First());
    }

    [Fact]
    public void ExtractByTypeTest_InterfaceTypes() {
        // Arrange
        var types = new List<Type> { typeof(int), typeof(IComparable), typeof(string), typeof(IConvertible) };
        var mockedTypes = new Mock<IEnumerable<Type>>();
        mockedTypes.Setup(mt => mt.GetEnumerator()).Returns(types.GetEnumerator());

        // Act
        Type[] result = mockedTypes.Object.ExtractByType<IComparable>(allowInterfaces: true).ToArray();

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains(typeof(int), result);
        Assert.Contains(typeof(string), result);
        Assert.Contains(typeof(IComparable), result);
    }

    [Fact]
    public void ExtractByTypeTest_AbstractTypes() {
        // Arrange
        var types = new List<Type> { typeof(int), typeof(AbstractClass), typeof(string), typeof(AbstractClassChild) };
        var mockedTypes = new Mock<IEnumerable<Type>>();
        mockedTypes.Setup(mt => mt.GetEnumerator()).Returns(types.GetEnumerator());

        // Act
        Type[] result = mockedTypes.Object.ExtractByType<AbstractClass>(allowAbstract: true).ToArray();

        // Assert
        Assert.Equal(2, result.Length);
        Assert.Contains(typeof(AbstractClass), result);
        Assert.Contains(typeof(AbstractClassChild), result);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // ExtractExact
    // -----------------------------------------------------------------------------------------------------------------
    [Fact]
    public void ExtractByTypeExactTest_NormalTypes() {
        // Arrange
        var types = new List<Type> { typeof(int), typeof(double), typeof(string), typeof(decimal) };
        var mockedTypes = new Mock<IEnumerable<Type>>();
        mockedTypes.Setup(mt => mt.GetEnumerator()).Returns(types.GetEnumerator());

        // Act
        Type[] result = mockedTypes.Object.ExtractByTypeExact<double>().ToArray();

        // Assert
        Assert.Single(result);
        Assert.Equal(typeof(double), result.First());
    }

    [Fact]
    public void ExtractByTypeExactTest_InterfaceTypes() {
        // Arrange
        var types = new List<Type> { typeof(int), typeof(IComparable), typeof(string), typeof(IConvertible) };
        var mockedTypes = new Mock<IEnumerable<Type>>();
        mockedTypes.Setup(mt => mt.GetEnumerator()).Returns(types.GetEnumerator());

        // Act
        Type[] result = mockedTypes.Object.ExtractByTypeExact<IComparable>().ToArray();

        // Assert
        Assert.Single(result);
        Assert.Equal(typeof(IComparable), result.First());
    }

    [Fact]
    public void ExtractByTypeExactTest_AbstractTypes() {
        // Arrange
        var types = new List<Type> { typeof(int), typeof(AbstractClassExact), typeof(string), typeof(AbstractClassChildExact) };
        var mockedTypes = new Mock<IEnumerable<Type>>();
        mockedTypes.Setup(mt => mt.GetEnumerator()).Returns(types.GetEnumerator());

        // Act
        Type[] result = mockedTypes.Object.ExtractByTypeExact<AbstractClassExact>().ToArray();

        // Assert
        Assert.Single(result);
        Assert.Contains(typeof(AbstractClassExact), result);
    }

    // You need to define these two classes for the above test case
    public abstract class AbstractClass;

    public class AbstractClassChild : AbstractClass;

    // You need to define these two classes for the above test case
    public abstract class AbstractClassExact;

    public class AbstractClassChildExact : AbstractClassExact;
}
