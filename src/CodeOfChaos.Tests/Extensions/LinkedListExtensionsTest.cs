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
[TestSubject(typeof(LinkedListExtensions))]
public class LinkedListExtensionsTest {

    [Fact]
    public void AddLastRepeated_ShouldAddElementsToTheEndOfLinkedList_WhenSourceIsNotEmpty() {
        // Arrange
        var linkedList = new LinkedList<int>();
        var source = new List<int> { 1, 2, 3 };

        // Act
        linkedList.AddLastRepeated(source);

        // Assert
        Assert.Equal(source, linkedList);
        Assert.NotNull(linkedList.Last);
        Assert.Equal(3, linkedList.Last.Value);
    }

    [Fact]
    public void AddLastRepeated_ShouldNotChangeLinkedList_WhenSourceIsEmpty() {
        // Arrange
        var linkedList = new LinkedList<int>();
        var source = new List<int>();
        if (source == null) throw new ArgumentNullException(nameof(source));

        // Act
        linkedList.AddLastRepeated(source);

        // Assert
        Assert.Equal(source, linkedList);
    }

    [Fact]
    public void AddFirstRepeated_ShouldAddElementsToTheBeginningOfLinkedList_WhenSourceIsNotEmpty() {
        // Arrange
        var linkedList = new LinkedList<int>();
        var source = new List<int> { 1, 2, 3 };

        // Act
        linkedList.AddFirstRepeated(source);

        // Assert
        // Assert
        Assert.Equal(source, linkedList.ToList());
        Assert.Equal(1, linkedList.First?.Value);
    }

    [Fact]
    public void AddFirstRepeated_ShouldNotChangeLinkedList_WhenSourceIsEmpty() {
        // Arrange
        var linkedList = new LinkedList<int>();
        var source = new List<int>();
        if (source == null) throw new ArgumentNullException(nameof(source));

        // Act
        linkedList.AddFirstRepeated(source);

        // Assert
        Assert.Equal(source, linkedList);
    }

    [Fact]
    public void AddFirstRepeated_ShouldThrowArgumentNullException_WhenLinkedListIsNull() {
        // Arrange
        LinkedList<int>? linkedList = null;
        var source = new List<int> { 1, 2, 3 };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => {
            #pragma warning disable CS8604// Possible null reference argument.
            linkedList.AddFirstRepeated(source);
            #pragma warning restore CS8604// Possible null reference argument.
        });
    }
    
    [Fact]
    public void Add_Valid() {
        // Arrange
        LinkedList<int> linkedList = [
            1,2,3
        ];
        var source = new List<int> { 1, 2, 3 };
    
        // Act & Assert
        Assert.Equal(source, linkedList);
    }
}







