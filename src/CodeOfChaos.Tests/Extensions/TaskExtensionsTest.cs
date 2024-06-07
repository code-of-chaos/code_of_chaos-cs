// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using JetBrains.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using TaskExtensions=CodeOfChaos.Extensions.TaskExtensions;

namespace CodeOfChaos.Tests.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[TestSubject(typeof(TaskExtensions))]
public class TaskExtensionsTest {
    [Fact]
    public async Task WithCancellation_ReturnsOperationCanceledException_WhenCancellationTokenCancels() {
        // Arrange
        var cancellationTokenSource = new CancellationTokenSource();
        Task longRunningTask = Task.Delay(10000, cancellationTokenSource.Token);

        // Act
        cancellationTokenSource.CancelAfter(500);
        Exception? caughtException = null;
        try {
            await longRunningTask.WithCancellation(cancellationTokenSource.Token);
        }
        catch (Exception ex) {
            caughtException = ex;
        }

        // Assert
        Assert.NotNull(caughtException);
        Assert.IsType<OperationCanceledException>(caughtException);
    }

    [Fact]
    public async Task WithCancellation_CompletesTask_WhenNotCancelled() {
        // Arrange
        var cancellationTokenSource = new CancellationTokenSource();
        Task shortRunningTask = Task.Delay(500, cancellationTokenSource.Token);

        // Act
        Exception? caughtException = null;
        try {
            await shortRunningTask.WithCancellation(cancellationTokenSource.Token);
        }
        catch (Exception ex) {
            caughtException = ex;
        }

        // Assert
        Assert.Null(caughtException);
    }
}