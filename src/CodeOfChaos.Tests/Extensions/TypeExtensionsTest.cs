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

    [Theory]
    [InlineData(typeof(AbstractClassChild), typeof(AbstractClass))]
    public void IsSubclassOfTest_Positive(Type type, Type baseType) {
        // Act
        bool result = type.IsSubclassOf(baseType);
    
        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(typeof(int), typeof(AbstractClass))]
    public void IsSubclassOfTest_Negative(Type type, Type baseType) {
        // Act
        bool result = type.IsSubclassOf(baseType);
    
        // Assert
        Assert.False(result);
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
    
    #region Attributes
    [AttributeUsage(AttributeTargets.Class)]
    public class TestAttribute(string value) : Attribute {
        public string Value { get; } = value;

        public TestAttribute() : this(string.Empty) {}

    }
    [Test("Test Value")]
    private class ClassWithTestAttribute;
    private class ClassWithoutTestAttribute;
    
    [Fact]
    public void TryGetCustomAttribute_ShouldReturnTrue_WhenAttributeExists() {
        // Arrange
        const string expectedValue = "Test Value";
        Type testClass = typeof(ClassWithTestAttribute);

        // Act
        bool result = testClass.TryGetCustomAttribute(out TestAttribute? attribute);

        // Assert
        Assert.True(result);
        Assert.NotNull(attribute);
        Assert.Equal(expectedValue, attribute.Value);
    }

    [Fact]
    public void TryGetCustomAttribute_ShouldReturnFalse_WhenAttributeDoesNotExist() {
        // Arrange
        Type testClass = typeof(ClassWithoutTestAttribute);

        // Act
        bool result = testClass.TryGetCustomAttribute(out TestAttribute? attribute);

        // Assert
        Assert.False(result);
        Assert.Null(attribute);
    }

    [Fact]
    public void GetCustomAttributeOrDefault_ShouldReturnAttribute_WhenAttributeExists() {
        // Arrange
        string expectedValue = "Test Value";
        Type testClass = typeof(ClassWithTestAttribute);

        // Act
        var attribute = testClass.GetCustomAttributeOrDefault<TestAttribute>();

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal(expectedValue, attribute.Value);
    }

    [Fact]
    public void GetCustomAttributeOrDefault_ShouldReturnDefaultInstance_WhenAttributeDoesNotExist() {
        // Arrange
        Type testClass = typeof(ClassWithoutTestAttribute);

        // Act
        var attribute = testClass.GetCustomAttributeOrDefault<TestAttribute>();

        // Assert
        Assert.NotNull(attribute);
        Assert.IsType<TestAttribute>(attribute);
        Assert.Equal(string.Empty, attribute.Value); // Assuming default is the default constructor.
    }

    [Fact]
    public void GetCustomAttributeOrDefault_ShouldReturnActionResult_WhenAttributeDoesNotExistAndActionIsProvided() {
        // Arrange
        Type testClass = typeof(ClassWithoutTestAttribute);
        const string expectedDefaultValue = "Custom Default Value";

        // Act
        TestAttribute attribute = testClass.GetCustomAttributeOrDefault(() => new TestAttribute(expectedDefaultValue));

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal(expectedDefaultValue, attribute.Value);
    }
    #endregion
    
}
