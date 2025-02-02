using System.Diagnostics;
using JourMinal;
using static JourMinal.Helpers;
using static System.Console;
using System.ComponentModel.DataAnnotations;

class Program
{
    /// <summary>
    /// Primary entry point: performs opening checks, journal selection, and displays the main menu.
    /// </summary>
    private const int MIN_CONSOLE_WIDTH = 115;

    static void Main(string[] args)
    {

        //=========================================================================================================
        //OPENING SEQUENCE
        //=========================================================================================================
        //Clear console for a clean start
        Clear();
        SlowType("Welcome to...");
        SimpleDelay(2); // Delay for dramatic effect
        
        // Store initial dimensions and check for a wide enough console window.
        int initialWidth = WindowWidth;
        int initialHeight = WindowHeight;
        
        CancellationTokenSource cts = new CancellationTokenSource();
        TerminalSizeMonitor.Watch(initialWidth, initialHeight, cts);
        try
        {
            if (initialWidth < MIN_CONSOLE_WIDTH)
            {
                OpeningScript(cts.Token); // Animates a plea for a larger window.
                WriteLine();
                if (WindowWidth < MIN_CONSOLE_WIDTH)
                {
                    SlowType("That's better.");
                    SimpleDelay(1);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Ignore cancellation exceptions.
        }
        
        // Animate title display using a multi-line string.
        SlowlyLowerString("""
        88                                                                      88                           88
        88                                                                      ""                           88
        88                                                                                                   88
        88   ,adPPYba,   88       88  8b,dPPYba,            88,dPYba,,adPYba,   88  8b,dPPYba,   ,adPPYYba,  88
        88  a8"     "8a  88       88  88P'   "Y8  ========  88P'   "88"    "8a  88  88P'   `"8a  ""     `Y8  88
        88  8b       d8  88       88  88          ========  88      88      88  88  88       88  ,adPPPPP88  88
88,   ,d88  "8a,   ,a8"  "8a,   ,a88  88                    88      88      88  88  88       88  88,    ,88  88
 "Y8888P"    `"YbbdP"'    `"YbbdP'Y8  88                    88      88      88  88  88       88  `"8bbdP"Y8  88

""");
        WriteLine("press enter to continue");
        ReadLine();
        Clear();

        //=========================================================================================================
        //JOURNAL SELECTION
        //=========================================================================================================

        //Check for .journal files in directory
        Journal currJournal;
        string[] detectedJournals = Journal.GetJournalsInDir();
        switch (detectedJournals.Length)
        {
            case 0: // No journals detected; prompt user to create a new one.
                WriteLine("No Journals Detected");
                WriteLine("Would you like to create a new journal? [Y/N]");
                if (ReadLine().ToLower() == "y")
                {
                    WriteLine("Enter Journal Name");
                    string journalName = ReadLine();
                    WriteLine("Enter Journal File Location or leave blank to create in current directory");
                    string journalFileDirectory = ReadLine();
                    if (journalFileDirectory == "")
                    {
                        journalFileDirectory = Directory.GetCurrentDirectory();
                        WriteLine($"Directory Selected: {journalFileDirectory}");
                        SimpleDelay(2);
                    }
                    currJournal = Journal.CreateNewJournal(journalName, journalFileDirectory);
                    break;
                }
                else
                {
                    //TODO: Add a way to set a directory to save journals to
                    WriteLine("Goodbye");
                    return;
                }

            case 1: // One journal detected; select automatically.
                string detectedJournalFullPath = detectedJournals[0];
                string detectedJournalName = Path.GetFileName(detectedJournalFullPath);
                WriteLine($"Detected Journal: {detectedJournalName}");
                currJournal = new Journal(detectedJournalName, detectedJournalFullPath);
                SimpleDelay(2);
                break;

            default: // Multiple journals; prompt user to choose.
                WriteLine("Detected Journals:");
                int i = 0;
                foreach (string journal in detectedJournals)
                {
                    WriteLine($"{i}) {Path.GetFileName(journal)}");
                    i++;
                }
                WriteLine("Enter the number of the journal you would like to load");
                int _ = Int32.Parse(ReadLine());
                currJournal = new Journal(Path.GetFileName(detectedJournals[_]), detectedJournals[_]);
                break;
        }

        //=========================================================================================================
        //MAIN MENU
        //=========================================================================================================
        Clear();
        WriteLine("[1] New Entry");
        WriteLine("[2] Load Entries");
        WriteLine("[3] Options");
        WriteLine("[4] Quit ");

        string input = ReadLine();
        switch (input)
        {
            case "1":
                Clear();
                Entry currEntry = new Entry();
                currEntry.WriteEntry();
                currJournal.SaveEntry(currEntry);
                break;
            case "2":
                Clear();
                WriteLine("Load Entry");
                currJournal.LoadEntries();
                WriteLine("Detected Entries:");
                int index = 0;
                foreach (Entry entry in currJournal._allEntries)
                {
                    WriteLine($"{index}) {entry._name} {entry._date}");
                    index++;
                }
                WriteLine("Enter the number of the entry you would like to load");
                int entryIndex = Int32.Parse(ReadLine());
                try
                {
                    currJournal._allEntries[entryIndex].Display();
                }
                catch { }
                break;
            case "3":
                Clear();
                WriteLine("enter test text");//TODO: Do I need any options?

                break;
            case "4":
                Clear();
                SlowType("Goodbye, see you next time!");
                break;
            default:
                Clear();
                WriteLine("Invalid Input");
                break;
        }
        WriteLine("Press any key to exit");
        ReadLine();
    }
}