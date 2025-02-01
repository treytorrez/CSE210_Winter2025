using System.Runtime.ConstrainedExecution;

namespace Helpers
{
    public static class Helpers
    {
        public static void SimpleDelay(float num)
        {
            System.Threading.Thread.Sleep((int)(num * 1000));
        }

        public static void SlowlyLowerString(string multiline)
        {
            // Split into lines
            string[] lines = multiline.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // For each line, show the bottom i lines
            for (int i = 1; i <= lines.Length; i++)
            {

                Console.Clear();
                for (int j = lines.Length - i; j < lines.Length; j++)
                {
                    Console.WriteLine(lines[j]);
                }
                System.Threading.Thread.Sleep(500);
            }
        }

        public static void SlowType(string text, int delay = 100)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay);
            }
        }

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