// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Lucide;
using JetBrains.Annotations;
using System;
using Xunit;

namespace CodeOfChaos.Tests.Lucide;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(Icons))]
public class IconsGeneratedTest {
   [Fact]
   public void IconsExpectedAmount() {
      // Arrange
      const int expectedCount = LucideData.AmountOfIcons;
      
      // Act
      int actualNumberOfValues = Enum.GetNames<Icons>().Length;
      
      // Assert
      Assert.Equal(expectedCount, actualNumberOfValues);
   }
}
