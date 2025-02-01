
public static class TerminalSizeMonitor
{
    public static void Watch(int initialWidth, int initialHeight, CancellationTokenSource cts)
    {
        Task.Run(() =>
        {
            while (!cts.Token.IsCancellationRequested)
            {
                if (Console.WindowWidth != initialWidth || Console.WindowHeight != initialHeight)
                {
                    cts.Cancel();
                }
                Thread.Sleep(100);
            }
        });
    }
}