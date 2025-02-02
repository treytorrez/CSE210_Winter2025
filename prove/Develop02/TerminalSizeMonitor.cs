/// <summary>
/// Monitors the console window size and cancels the token if the dimensions change.
/// </summary>
public static class TerminalSizeMonitor
{
    private const int MONITOR_SLEEP_MS = 100;

    /// <summary>
    /// Runs a background task that continuously checks the console window size.
    /// Cancels the provided token if the size changes.
    /// </summary>
    public static void Watch(int initialWidth, int initialHeight, CancellationTokenSource cts)
    {
        Task.Run(() =>
        {
            // Loop until cancellation token is triggered.
            while (!cts.Token.IsCancellationRequested)
            {
                if (Console.WindowWidth != initialWidth || Console.WindowHeight != initialHeight)
                {
                    cts.Cancel();
                }
                Thread.Sleep(MONITOR_SLEEP_MS);
            }
        });
    }
}