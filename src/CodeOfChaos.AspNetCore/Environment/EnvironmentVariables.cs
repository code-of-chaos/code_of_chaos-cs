// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.AspNetCore.Environment;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Represents a class that provides access to environment variables used in the application.
/// </summary>
[UsedImplicitly]
public class EnvironmentVariables(IConfiguration configuration) {
    // private readonly Dictionary<Type, HashSet<string>> _typedValues = new();
    private readonly Dictionary<string, Type> _typedValues = new();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Tries to register the specified environment variable with a type.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to register.</typeparam>
    /// <param name="name">The name of the environment variable to register.</param>
    /// <returns><c>true</c> if the environment variable is successfully registered; otherwise, <c>false</c>.</returns>
    [UsedImplicitly]
    public bool TryRegister<TValue>(string name) {
        return _typedValues.TryAdd(name,typeof(TValue));
    }

    /// <summary>
    /// Tries to register the specified environment variable with a type.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to register.</typeparam>
    /// <typeparam name="TEnum">The Enum</typeparam>
    /// <param name="name">The name of the environment variable to register.</param>
    /// <returns><c>true</c> if the environment variable is successfully registered; otherwise, <c>false</c>.</returns>
    [UsedImplicitly]
    public bool TryRegister<TEnum, TValue>(TEnum name) where TEnum : struct, Enum {
        return Enum.GetName(name) is {} n
               && _typedValues.TryAdd(n,typeof(TValue));
    }

    /// <summary>
    /// Tries to register all environment variable values with a type.
    /// Only registers all enum value names, if they can all be added
    /// </summary>
    /// <typeparam name="TEnum">The enumeration type containing the names of the environment variables to register.</typeparam>
    /// <typeparam name="TValue">The type of the value to register.</typeparam>
    /// <returns><c>true</c> if all the environment variables are successfully registered; otherwise, <c>false</c>.</returns>
    [UsedImplicitly]
    public bool TryRegisterAllValuesAllOrNone<TEnum, TValue>() where TEnum : struct, Enum {
        string[] names = Enum.GetNames<TEnum>();
        return 
            !names.Any(name => _typedValues.ContainsKey(name)) 
            && names.All(name =>  _typedValues.TryAdd(name, typeof(TValue)));
    }

    /// <summary>
    /// Tries to register all the values of an enumeration as environment variables with a specified type.
    /// Only registers those which can be added, and fails silently on those which can't be added
    /// </summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    /// <typeparam name="TValue">The type of the values to register.</typeparam>
    /// <returns><c>true</c> if atleast one environment variable is successfully registered; otherwise, <c>false</c>.</returns>
    [UsedImplicitly]
    public bool TryRegisterAllValuesPartialAllowed<TEnum, TValue>() where TEnum : struct, Enum {
        return Enum.GetNames<TEnum>().Any(name =>  _typedValues.TryAdd(name, typeof(TValue)));
    }

    /// <summary>
    /// Tries to get the value of the specified environment variable.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to retrieve.</typeparam>
    /// <param name="name">The name of the environment variable.</param>
    /// <param name="value">When this method returns, contains the value of the specified environment variable if it is found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the specified environment variable is found and successfully converted to type <typeparamref name="TValue"/>; otherwise, <c>false</c>.</returns>
    public bool TryGetValue<TValue>(string name, [NotNullWhen(true)] out TValue? value) where TValue : notnull {
        value = default;
        string? val = configuration[name];
        if (val is null) return false;
        
        switch (typeof(TValue).Name) {
            case nameof(Boolean):
                if (bool.TryParse(val, out bool boolParseResult)) {
                    value = (TValue)(object)boolParseResult;
                    return true;
                }
                break;

            case nameof(Guid):
                if (Guid.TryParse(val, out Guid guidParseResult)) {
                    value = (TValue)(object)guidParseResult;
                    return true;
                }
                break;

            case nameof(String):
                value = (TValue)(object)val;
                return true;

            // add more cases based on other types you care about
        }

        return false;
    }

    /// <summary>
    /// Gets the required value of the specified environment variable.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to get.</typeparam>
    /// <param name="name">The name of the environment variable.</param>
    /// <returns>The value of the specified environment variable.</returns>
    /// <exception cref="ArgumentException">Thrown when the required environment value could not be found.</exception>
    public TValue GetRequiredValue<TValue>(string name) where TValue : notnull {
        if (!TryGetValue(name, out TValue? value)) {
            throw new ArgumentException($"Required Environment value of name {name} could not be found");
        }
        return value;
    }
}
