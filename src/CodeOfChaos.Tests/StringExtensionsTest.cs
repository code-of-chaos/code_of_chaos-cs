// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using JetBrains.Annotations;
using System.Collections.Generic;
using Xunit;

namespace CodeOfChaos.Tests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(StringExtensions))]
public class StringExtensionsTest {
    // Test cases for IsNotNullOrEmpty method
    [Fact]
    public void IsNotNullOrEmpty_GivenNonEmptyString_ShouldReturnTrue() {
        const string nonEmptyStr = "Test";
        bool result = nonEmptyStr.IsNotNullOrEmpty();
        Assert.True(result);
    }

    [Fact]
    public void IsNotNullOrEmpty_GivenEmptyString_ShouldReturnFalse() {
        const string emptyStr = "";
        bool result = emptyStr.IsNotNullOrEmpty();
        Assert.False(result);
    }

    [Fact]
    public void IsNotNullOrEmpty_GivenNullString_ShouldReturnFalse() {
        string? nullStr = null;
        bool result = nullStr.IsNotNullOrEmpty();
        Assert.False(result);
    }

    // Test cases for IsEmpty method for string array
    [Fact]
    public void IsEmpty_GivenNonEmptyArray_ShouldReturnFalse() {
        string[] arr = ["Cat", "Dog"];
        bool result = arr.IsEmpty();
        Assert.False(result);
    }

    [Fact]
    public void IsEmpty_GivenEmptyArray_ShouldReturnTrue() {
        string[] arr = [];
        bool result = arr.IsEmpty();
        Assert.True(result);
    }

    // Test cases for IsEmpty method for IEnumerable<string>
    [Fact]
    public void IsEmpty_GivenNonEmptyEnumerable_ShouldReturnFalse() {
        IEnumerable<string> arr = new List<string> { "Mouse", "Elephant" };
        bool result = arr.IsEmpty();
        Assert.False(result);
    }

    [Fact]
    public void IsEmpty_GivenEmptyEnumerable_ShouldReturnTrue() {
        IEnumerable<string> arr = new List<string>();
        bool result = arr.IsEmpty();
        Assert.True(result);
    }

}