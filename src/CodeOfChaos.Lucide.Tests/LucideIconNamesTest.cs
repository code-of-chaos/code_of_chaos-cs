// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
namespace CodeOfChaos.Lucide.Tests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(LucideIconNames))]
public class LucideIconNamesTest {
   [Fact]
   public void IconsExpectedAmount() {
      // Arrange
      const int expectedHardCount = TestData.HardDefinedAmountOfIcons;
      const int expectedLooseCount = LucideData.AmountOfIcons;
      
      // Act
      int actualNumberOfValues = typeof(LucideIconNames)
         .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
         .Count(f => 
            f is { IsLiteral: true, IsInitOnly: false } 
            && f.FieldType == typeof(string)
         );
      
      // Assert
      Assert.True(expectedHardCount <= actualNumberOfValues);
      Assert.Equal(expectedLooseCount, actualNumberOfValues);
   }
}
