using System;
using System.Diagnostics;
class Program
{

    static void Main()
    {
        // Create a list of Scripture objects; initialize with contents
        Scripture[] DefaultScriptures = new Scripture[]
        {
            // DefaultScriptures[0]
            // Luke 1:37-38
            new Scripture(new ScriptureReference
            {
                _book="Luke", 
                _chapter=1, 
                _hasMultipleVerses=true,
                _startingVerse=37,
                _endVerse=38
            })
            {
                _content= new Dictionary<int, string[]>
                {
                    {37, new string[] {"For", "with", "God", "nothing", "shall", "be", "impossible", "."}},
                    {38, new string[] {"And", "Mary", "said", "Behold", "the", "handmaid", "of", "the", "Lord", ";", "be", "it", "unto", "me", "according", "to", "thy", "word", ".", "And","the", "angel", "departed", "from", "her", "."}},
                }
            },
            new Scripture(new ScriptureReference
            // DefaultScriptures[1]
            // 1 Nephi 3:7
            {
                _book="1 Nephi",
                _chapter=3,
                _hasMultipleVerses = false,
                _startingVerse = 7,
                _endVerse = 7
            })
            {
                _content= new Dictionary<int, string[]>
                {
                    {7, new string[] { "And", "it", "came", "to", "pass", "that", "I,", "Nephi,", "said", "unto",
                    "my", "father",":", "I", "will", "go", "and", "do", "the", "things", "which",
                    "the", "Lord", "hath", "commanded,", "for", "I", "know", "that", "the",
                    "Lord", "giveth", "no", "commandments", "unto", "the", "children", "of",
                    "men,", "save", "he", "shall", "prepare", "a", "way", "for", "them",
                    "that", "they", "may", "accomplish", "the", "thing", "which", "he",
                    "commandeth", "them", "." }}
                }
            },
            new Scripture(new ScriptureReference
            // DefaultScriptures[2]
            // 2 Nephi 2:25
            {
                _book="2 Nephi",
                _chapter=2,
                _hasMultipleVerses = false,
                _startingVerse = 25,
                _endVerse = 25
            })
            {
                _content = new Dictionary<int, string[]>
                {
                    {25, new string[]{"Adam", "fell", "that", "men", "might", "be", ";", "and", "men", "are", 
                    ",", "that", "they", "might", "have", "joy", "."}}
                }
            },
            new Scripture(new ScriptureReference
            // DefaultScriptures[3]
            // Moroni 10:4-5
            {
                _book="Moroni",
                _chapter=10,
                _hasMultipleVerses = true,
                _startingVerse = 4,
                _endVerse = 5
            })
            {
                _content = new Dictionary<int, string[]>
                {
                    {4, new string[] {"And", "when", "ye", "shall", "receive", "these", "things",
                    ",", "I", "would", "exhort", "you", "that", "ye", "would", "ask", "God" , ",", 
                    "the", "Eternal", "Father", ",", "in", "the", "name", "of", "Christ" , ",", 
                    "if", "these", "things", "are", "not", "true", ";", "and", "if", "ye", "shall", 
                    "ask", "with", "a", "sincere", "heart" , ",", "with", "real", "intent" , ",", 
                    "having", "faith", "in", "Christ" , ",", "he", "will", "manifest", "the", 
                    "truth", "of", "it", "unto", "you" , ",", "by", "the", "power", "of", "the", 
                    "Holy", "Ghost", "."}},                
                    {5, new string[] {"And", "by", "the", "power", "of", "the", "Holy", "Ghost", "ye", "may", 
                    "know", "the", "truth", "of", "all", "things", "."}}
                }
            },
            new Scripture(new ScriptureReference
            // DefaultScriptures[4]
            // D&C 88:118
            {
                _book="D&C",
                _chapter=88,
                _hasMultipleVerses = false,
                _startingVerse = 118,
                _endVerse = 118
            })
            {
                _content = new Dictionary<int, string[]>
                {
                    {118, new string[] {"And", "as", "all", "have", "not", "faith", ",", "seek", "ye", "diligently", "and", "teach", "one", "another", "words", "of", "wisdom", ";", "yea", ",", "seek", "yea", "out", "of", "the", "best", "books", "words", "of", "wisdom", ";", "seek", "learning", ",", "even", "by", "study", "and", "also", "by", "faith"}}
                }
            }
        }; 

        // Generate a random number to select and print one of the default verses
        Random random = new Random();
        Scripture currentScripture = DefaultScriptures[random.Next(DefaultScriptures.Length)];
        // Initialize Blank object
        Blank blank = new Blank(currentScripture);
        Dictionary<int, bool[]> blankDictionary = blank.GetBlankDictionary();
        // Welcome message
        Console.WriteLine("Welcome to the Scripture Memorization Program!");
        // Get number of guesses from user
        Console.WriteLine("How many times would you like to guess the verse?");
        int guesses = Convert.ToInt32(Console.ReadLine());
        blank.SetGuesses(guesses);
        // Intialize input variable for later
        string input;
        
        // Main program loop
        do
        {
            //debugging blank class
            Debug.WriteLine($"{blank.GetBlankDictionary()}");
            // print a random scripture
            PrintVerse(currentScripture, blankDictionary);
            // Check to see if user wants to quit or continue
            Console.WriteLine("Press enter to continue or type 'quit' to exit the program.");
            input = Console.ReadLine();
            // Clear the console window
            Console.Clear();

            blank.UpdateBlankDictionary();
        }        
        while (input != "quit");
        

        void PrintVerse(Scripture scripture, Dictionary<int, bool[]> blankDictionary = null)
        {
            //Establish list of punctuation that doesnt't need a space before it
            string[] punctuation = new string[] {",", ".", ";", ":", "!", "?"};

            // Print the verse
            Console.WriteLine($"{scripture._reference._book} {scripture._reference._chapter}:{scripture._reference._startingVerse}- {scripture._reference._endVerse}");
            // Print the content of the verse using the verse numbers to access the dictionary instead  of foreach because dictionary is unordered
            for (int i = scripture._reference._startingVerse; i <= scripture._reference._endVerse; i++)
            {
                // print the verse number
                Console.Write($"{i}: ");
                // print the first word in the verse without a space before it
                Console.Write($"{scripture._content[i][0]}");
                // print the rest of the words in the verse with a space before them
                for (int j = 1; j < scripture._content[i].Length; j++)
                {
                    // check if the word is punctuation
                    if (Array.IndexOf(punctuation, scripture._content[i][j]) != -1)
                    {
                        // if it is, print it without a space before it
                        Console.Write($"{scripture._content[i][j]}");
                    }
                    else if (blankDictionary[i][j])
                    {
                        // if it is blanked out, print an underscore
                        Console.Write($"_");
                    }
                    else
                    {
                        // if it isn't, print it with a space before it 
                        Console.Write($" {scripture._content[i][j]}");
                    }
                }
                // print a new line for the new verse
                Console.WriteLine();

            }
        }// PrintVerse
    }// Main

}// class Program