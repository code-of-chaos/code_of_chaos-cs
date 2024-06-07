// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Xunit;

namespace CodeOfChaos.Tests.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Support  Code
// ---------------------------------------------------------------------------------------------------------------------
[Flags]
enum TestFlag {
    Zero =  0,
    First = 1 << 0,
    Second = 1 << 1,
}

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(EnumExtensions))]
public class EnumExtensionsTest {
    [Fact]
    public void GetFlaggedAsValues_FlaggedEnum_ReturnsIncludedFlags() {
        const TestFlag enumValue = TestFlag.First | TestFlag.Second;
    
        var expectedFlags = new List<TestFlag> {TestFlag.Zero, TestFlag.First, TestFlag.Second };
    
        IEnumerable<TestFlag> flagged = enumValue.GetFlaggedAsValues();
    
        Assert.Equal(expectedFlags, flagged);
    }
}