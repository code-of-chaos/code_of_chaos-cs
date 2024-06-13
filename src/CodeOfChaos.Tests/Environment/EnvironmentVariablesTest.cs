// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.AspNetCore.Environment;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Xunit;
using System;

namespace CodeOfChaos.Tests.Environment;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(EnvironmentVariables))]
public class EnvironmentVariablesTest {

    private readonly EnvironmentVariables _sut;

    public EnvironmentVariablesTest() {
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(c => c["TestName"]).Returns("TestValue");

        _sut = new EnvironmentVariables(mockConfiguration.Object);
    }

    [Fact]
    public void TryRegister_Success_WhenValueIsNotRegistered() {
        bool result = _sut.TryRegister<string>("Test");
        Assert.True(result);
    }

    [Fact]
    public void TryRegister_Fails_WhenValueIsAlreadyRegistered() {
        _sut.TryRegister<string>("Test");
        bool result = _sut.TryRegister<string>("Test");
        Assert.False(result);
    }

    [Fact]
    public void TryRegisterAllValuesAllOrNone_Success_WhenAllValuesAreNotRegistered() {
        bool result = _sut.TryRegisterAllValuesAllOrNone<SomeEnum, string>();
        Assert.True(result);
    }

    [Fact]
    public void TryRegisterAllValuesAllOrNone_Fails_WhenAnyValueIsAlreadyRegistered() {
        _sut.TryRegister<SomeEnum, string>(SomeEnum.Value1);
        bool result = _sut.TryRegisterAllValuesAllOrNone<SomeEnum, string>();
        Assert.False(result);
    }

    [Fact]
    public void TryRegisterAllValuesPartialAllowed_Success_WhenAllValuesAreNotRegistered() {
        bool result = _sut.TryRegisterAllValuesPartialAllowed<SomeEnum, string>();
        Assert.True(result);
    }

    [Fact]
    public void TryRegisterAllValuesPartialAllowed_Success_WhenAnyValueIsAlreadyRegistered() {
        _sut.TryRegister<SomeEnum, string>(SomeEnum.Value1);
        bool result = _sut.TryRegisterAllValuesPartialAllowed<SomeEnum, string>();
        Assert.True(result);
    }

    [Fact]
    public void TryGetValue_Success_WhenValueIsRegisteredAndCorrectType() {
        _sut.TryGetValue("TestName", out string? value);
        Assert.Equal("TestValue", value);
    }

    [Fact]
    public void TryGetValue_Fails_WhenValueIsNotRegistered() {
        bool result = _sut.TryGetValue("NonExistent", out string? value);
        Assert.False(result);
        Assert.Null(value);
    }

    [Fact]
    public void TryGetValue_Fails_WhenValueIsRegisteredAndIncorrectType() {
        _sut.TryRegister<int>("Test");
        bool result = _sut.TryGetValue("Test", out string? value);
        Assert.False(result);
        Assert.Null(value);
    }

    [Fact]
    public void GetRequiredValue_Success_WhenValueIsRegisteredAndCorrectType() {
        Assert.Equal("TestValue", _sut.GetRequiredValue<string>("TestName"));
    }

    [Fact]
    public void GetRequiredValue_ThrowsException_WhenValueIsNotRegistered() {
        Assert.Throws<ArgumentException>(() => _sut.GetRequiredValue<string>("NonExistent"));
    }

    [Fact]
    public void GetRequiredValue_ThrowsException_WhenValueIsRegisteredAndIncorrectType() {
        _sut.TryRegister<int>("Test");
        Assert.Throws<ArgumentException>(() => _sut.GetRequiredValue<string>("Test"));
    }

    private enum SomeEnum {
        Value1,
        Value2,
        Value3
    }
}