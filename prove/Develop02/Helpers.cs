using System.Runtime.ConstrainedExecution;

namespace JourMinal
{
    /// <summary>
    /// Contains utility methods for delays, text animation, and user list selection.
    /// </summary>
    public static class Helpers
    {
        private const int SCROLL_SPEED_MS = 500;
        private const int DEFAULT_SLOWTYPE_DELAY_MS = 100;

        /// <summary>
        /// Pauses execution for the specified number of seconds.
        /// </summary>
        public static void SimpleDelay(float num)
        {
            System.Threading.Thread.Sleep((int)(num * 1000));
        }

        /// <summary>
        /// Scrolls a multi-line string upward, revealing more lines over time.
        /// </summary>
        public static void SlowlyLowerString(string multiline)
        {
            // Split the string into lines.
            string[] lines = multiline.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            // Gradually display the bottom lines.
            for (int i = 1; i <= lines.Length; i++)
            {
                Console.Clear();
                for (int j = lines.Length - i; j < lines.Length; j++)
                {
                    Console.WriteLine(lines[j]);
                }
                System.Threading.Thread.Sleep(SCROLL_SPEED_MS);
            }
        }

        /// <summary>
        /// Outputs text character-by-character with a default delay for a "typing" effect.
        /// </summary>
        public static void SlowType(string text, int delay = DEFAULT_SLOWTYPE_DELAY_MS)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay);
            }
        }

        /// <summary>
        /// Displays an animated script if the console window is too narrow.
        /// </summary>
        public static void OpeningScript(CancellationToken token)
        {
            System.Console.WriteLine();
            SlowType("...");
            SlowType("could I get a little more space?");
            if (token.IsCancellationRequested)
                return;
            System.Console.WriteLine();
            SlowType("  < this way >  ");
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("  <- this way ->  ");
                SimpleDelay(.5f);
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(" <-- this way --> ");
                SimpleDelay(.5f);
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("<--- this way --->");
                SimpleDelay(.5f);
                if (token.IsCancellationRequested)
                    return;
            }
            SimpleDelay(3);
            System.Console.WriteLine();
            SlowType("...");
            System.Console.WriteLine();
            if (token.IsCancellationRequested)
                return;
            SimpleDelay(2);
            System.Console.WriteLine();
            SlowType("..no?");
            SimpleDelay(2);
            if (token.IsCancellationRequested)
                return;
            System.Console.WriteLine();
            SlowType("well don't complain when it doesn't look right...");
            SimpleDelay(1);
            if (token.IsCancellationRequested)
                return;
        }

    }
}