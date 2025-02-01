using System.Diagnostics;
using static Helpers.Helpers;
using static System.Console;

class Program
{
    static void Main(string[] args)
    {

        //=========================================================================================================
        //OPENING SEQUENCE
        //=========================================================================================================
        //Clear console for a clean start
        Clear();
        SlowType("Welcome to...");
        //delay for dramatic effect
        SimpleDelay(2);
        //make sure the console is big enough to display the title and journal
        int initialWidth = WindowWidth;
        int initialHeight = WindowHeight;

        //async operation to check if the console is big enough
        CancellationTokenSource cts = new CancellationTokenSource();
        TerminalSizeMonitor.Watch(initialWidth, initialHeight, cts);
        try
        {
            if (initialWidth < 115)
            {
                OpeningScript(cts.Token);
                WriteLine();
                if (WindowWidth < 115)
                {
                    SlowType("That's better.");
                    SimpleDelay(1);
                }
            }

        }

        catch (OperationCanceledException)
        {
        }


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
        string[] detectedJournals = Journal.getJournalsInDir();
        switch (detectedJournals.Length)
        {
            case 0: //no journals detected

                WriteLine("No Journals Detected");
                WriteLine("Would you like to create a new journal? [Y/N]");
                if (ReadLine().ToLower() == "y")
                {
                    //create journal with user entered name and location 
                    //if no file location is entered, create journal in current directory
                    WriteLine("Enter Journal Name");
                    string journalName = ReadLine();
                    WriteLine("Enter Journal File Location or leave blank to create in current directory");
                    string journalFileLocation = ReadLine();
                    if (journalFileLocation == "") { journalFileLocation = Directory.GetCurrentDirectory(); Debug.WriteLine(journalFileLocation); }
                    currJournal = new Journal(journalName, journalFileLocation);
                    break;
                }
                else
                {
                    //TODO: Add a way to set a directory to save journals to
                    WriteLine("Goodbye");
                    return;
                }
                
            case 1: //TODO: automatically select journal if only one is detected
                WriteLine($"Detected Journal: {Path.GetFileName(detectedJournals[0])}");
                currJournal = new Journal(Path.GetFileName(detectedJournals[0]), detectedJournals[0]);
                SimpleDelay(2); 
                break;

            default: //select journal from list of detected journals
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
                break;
            case "2":
                Clear();
                WriteLine("Load Entry");
                break;
            case "3":
                Clear();
                WriteLine("enter test text");
                
                break;
            case "4":
                Clear();
                WriteLine("Quit");
                break;
            default:
                Clear();
                WriteLine("Invalid Input");
                break;
        }
    }
}