// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

namespace CodeOfChaos.Extensions;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
///     Provides extension methods for working with <see cref="Task" /> objects.
/// </summary>
[UsedImplicitly]
public static class TaskExtensions {

    /// <summary>
    ///     Creates a new task that completes when either the provided task completes or the cancellation token is canceled.
    ///     If the cancellation token is canceled, the returned task will be canceled with an
    ///     <see cref="OperationCanceledException" />.
    /// </summary>
    /// <param name="task">The task to monitor for completion.</param>
    /// <param name="cancellationToken">The cancellation token to observe.</param>
    /// <returns>
    ///     A task that completes when either the provided task completes or the cancellation token is canceled.
    ///     If the cancellation token is canceled, the returned task will be canceled with an
    ///     <see cref="OperationCanceledException" />.
    /// </returns>
    [UsedImplicitly]
    public async static Task WithCancellation(this Task task, CancellationToken cancellationToken) {

        var tcs = new TaskCompletionSource<bool>();

        // s is set when the cancellation happens,
        //      Meaning that WhenAny returns and the tcs.Task has been set, it'll throw the exception
        //      When task returns in WhenAny it is the result
        await using (cancellationToken.Register(callback: s => ((TaskCompletionSource<bool>)s!).TrySetResult(true), tcs)) {
            if (task != await Task.WhenAny(task, tcs.Task))
                throw new OperationCanceledException(cancellationToken);
        }


    }
}
