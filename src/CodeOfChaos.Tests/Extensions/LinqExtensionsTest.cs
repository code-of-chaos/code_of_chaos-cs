// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Range=System.Range;

namespace CodeOfChaos.Tests.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(LinqExtensions))]
public class LinqExtensionsTest {
    [Fact]
    public void TestConditionalWhere() {
        var data = new List<int> { 1, 2, 3, 4, 5 };
        const bool condition = true;

        List<int> result = data.AsQueryable().ConditionalWhere(condition, n => n > 3).ToList();

        Assert.True(result.SequenceEqual(new List<int> { 4, 5 }));
    }

    [Fact]
    public void TestConditionalTakeByCount() {
        var data = new List<int> { 1, 2, 3, 4, 5 };
        const bool condition = true;
        const int count = 3;

        List<int> result = data.AsQueryable().ConditionalTake(condition, count).ToList();

        Assert.True(result.SequenceEqual(new List<int> { 1, 2, 3 }));
    }

    [Fact]
    public void TestConditionalTakeByRange() {
        var data = new List<int> { 1, 2, 3, 4, 5 };
        const bool condition = false;
        var range = new Range(1, 3);

        List<int> result = data.AsQueryable().ConditionalTake(condition, range).ToList();

        Assert.True(result.SequenceEqual(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [Fact]
    public void TestIterateOverWithAction() {
        var data = new List<int> { 1, 2, 3, 4, 5 };

        // ReSharper disable once RedundantAssignment
        data.IterateOver(number => number += 1);

        Assert.True(data.SequenceEqual(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [Fact]
    public void TestIterateOverWithFunc() {
        var data = new List<int> { 1, 2, 3, 4, 5 };

        // ReSharper disable once RedundantAssignment
        data.IterateOver(number => number += 1);

        Assert.True(data.SequenceEqual(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [Fact]
    public void TestIterateOverLinkedList() {
        var linkedList = new LinkedList<int>(new List<int> { 1, 2, 3, 4, 5 });

        // ReSharper disable once RedundantAssignment
        linkedList.IterateOver(number => number += 1);

        Assert.True(linkedList.SequenceEqual(new LinkedList<int>(new List<int> { 1, 2, 3, 4, 5 })));
    }

    [Fact]
    public void TestWhereNot() {
        var data = new List<int> { 1, 2, 3, 4, 5 };
        var action = new Func<int, bool>(number => number > 3);

        List<int> result = data.WhereNot(action).ToList();

        Assert.True(result.SequenceEqual(new List<int> { 1, 2, 3 }));
    }
}