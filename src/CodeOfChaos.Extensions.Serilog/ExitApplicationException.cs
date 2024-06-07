// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Extensions.Serilog;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <summary>
/// Custom exception class for exiting the application with a specific exit code.
/// </summary>
public class ExitApplicationException(int exitCode, string msg) : Exception(msg) {
    /// <summary>
    /// Represents an exception that is thrown to exit an application with a specific exit code.
    /// </summary>
    [UsedImplicitly]
    public int ExitCode { get; } = exitCode;
}
