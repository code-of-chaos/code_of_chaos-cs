// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Ansi;
using JetBrains.Annotations;
using Xunit;

namespace CodeOfChaos.Tests.Ansi;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(ByteVector3))]
public class ByteVector3Test {

    [Fact]
    public void TestZeroConstructor() {
        ByteVector3 zeroVec = ByteVector3.Zero;
        Assert.Equal(0, zeroVec.X);
        Assert.Equal(0, zeroVec.Y);
        Assert.Equal(0, zeroVec.Z);
    }

    [Fact]
    public void TestMaxConstructor() {
        ByteVector3 maxVec = ByteVector3.Max;
        Assert.Equal(255, maxVec.X);
        Assert.Equal(255, maxVec.Y);
        Assert.Equal(255, maxVec.Z);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    public void TestToByteArray(byte x, byte y, byte z) {
        var vec = new ByteVector3(x, y, z);
        byte[] array = vec.ToArray();
        Assert.Equal(new[] { x, y, z }, array);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    public void TestToAnsiString(byte x, byte y, byte z) {
        var vec = new ByteVector3(x, y, z);
        string str = vec.ToAnsiString();
        Assert.Equal($"{x};{y};{z}", str);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    public void TestToRgbString(byte x, byte y, byte z) {
        var vec = new ByteVector3(x, y, z);
        string str = vec.ToRgbString();
        Assert.Equal($"rgb({x},{y},{z})", str);
    }
}