// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace CodeOfChaos.Lucide.Tests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(LucideIconsSet))]
public class LucideIconsSetTest {
   [Fact]
   public void IconsExpectedAmount() {
      // Arrange
      const int expectedHardCount = TestData.HardDefinedAmountOfIcons;
      const int expectedLooseCount = LucideData.AmountOfIcons;
      
      // Act
      int actualNumberOfValues = LucideIconsSet.IconAtlas.Count;
      
      // Assert
      Assert.True(expectedHardCount <= actualNumberOfValues);
      Assert.Equal(expectedLooseCount, actualNumberOfValues);
   }
}
