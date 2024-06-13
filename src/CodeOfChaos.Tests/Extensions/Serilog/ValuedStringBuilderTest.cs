// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Serilog;
using JetBrains.Annotations;
using Xunit;

namespace CodeOfChaos.Tests.Extensions.Serilog;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(ValuedStringBuilder))]
public class ValuedStringBuilderTest {
    [Fact]
    public void Test_Append() {
        var vStringBuilder = new ValuedStringBuilder();
        vStringBuilder.Append("Hello").Append(" World!");
        string result = vStringBuilder.ToString();
        Assert.Equal("Hello World!", result);
    }

    [Fact]
    public void Test_AppendLine() {
        var vStringBuilder = new ValuedStringBuilder();
        vStringBuilder.AppendLine("Hello").AppendLine("World!");
        string result = vStringBuilder.ToString();
        Assert.Equal($"Hello{System.Environment.NewLine}World!{System.Environment.NewLine}", result);
    }

    [Fact]
    public void Test_AppendValued() {
        var vStringBuilder = new ValuedStringBuilder();
        vStringBuilder.AppendValued("Hello ", "World!");
        string result = vStringBuilder.ToString();
        Assert.Equal("Hello {args0}", result);
    }

    [Fact]
    public void Test_AppendLineValued() {
        var vStringBuilder = new ValuedStringBuilder();
        vStringBuilder.AppendLineValued("Hello ", "World!");
        string result = vStringBuilder.ToString();
        Assert.Equal($"{System.Environment.NewLine}Hello {{args0}}", result);
    }

    [Fact]
    public void Test_ValuesToArray() {
        var vStringBuilder = new ValuedStringBuilder();
        vStringBuilder.AppendValued("Hello ", "World!");
        object?[] result = vStringBuilder.ValuesToArray();
        Assert.Equal(["World!"], result);
    }
}
