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
[TestSubject(typeof(IconSet))]
public class IconSetGeneratedTest {
   [Fact]
   public void IconsExpectedAmount() {
      // Arrange
      const int expectedCount = LucideData.AmountOfIcons;
      
      // Act
      int actualNumberOfValues = IconSet.IconAtlas.Count;
      
      // Assert
      Assert.Equal(expectedCount, actualNumberOfValues);
   }
}
