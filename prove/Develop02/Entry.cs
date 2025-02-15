namespace JourMinal
{
    using System.Diagnostics;
    using System.Security.Principal;
    using System.Text.Json.Serialization;
    using System.Windows.Forms;

    /// <summary>
    /// Represents a single journal entry with date, content, name, and optional prompt.
    /// </summary>
    public class Entry
    {
        public DateTime _date { get; set; }
        public string _content { get; set; }
        public string _name { get; set; }
        public string _promptString { get; set; }
        [JsonIgnore]
        public string[] _allPrompts { get; } = new string[]
        {
            "What do you want to be when you grow up? Why?",
            "What are five things you would like to do before you are 20 years old?",
            "What is your dream job? Describe a typical work day at this job.",
            "What do you think your life will be like when you are 30 years old?",
            "What are the three most important jobs in the world? Why are these three jobs so important?",
            "Would you want to be the president? Why or why not?",
            "Would you rather be a famous writer, a rock star, or a doctor who finds a cure for cancer? Why?",
            "What are the most important qualities in a friend? Are you a good friend? Why or why not?",
            "Is it ever okay to lie? Why do you think so?",
            "What are three things that every parent should do? Why are these things important?",
            "It has been said that money can't buy happiness. Do you agree or disagree? Why?",
            "Would you rather be beautiful, smart, or athletic? Why?",
            "Describe a real made-up dream or nightmare.",
            "Write about your favorite childhood toy.",
            "Write out the best or the worst day of your life.",
            "Finish this thought: if I could change one thing about myself (if you can't think of anything, you might want to consider telling how you got to be perfect!)",
            "If and when I raise children, I'll never...",
            "I have never been more frightened than when...",
            "Persuade a friend to give up drugs.",
            "Five years from now, I will be...",
            "Write about a day you'd like to forget.",
            "Invent and describe a new food.",
            "Describe an event that changed your life forever, or make up and describe an event that would change your life forever.",
            "Describe someone who is a hero to you and explain why.",
            "Write about a time in your life when you struggled with a choice and made the right one.",
            "Imagine yourself in a different century and describe an average day in your life.",
            "Which character from a book would you most like to meet and why?",
            "Three goals I have set for myself are...",
            "List the top 5 people you admire, and why.",
            "Who is your favorite person in the world and why?",
            "What is your favorite song/musical piece and why?"
        };

        /// <summary>
        /// Default constructor used during interactive entry creation.
        /// Prompts the user for entry name and (optionally) sets a random prompt.
        /// </summary>
        public Entry()
        {
            _date = DateTime.Now;
            _content = "";
            System.Console.WriteLine("Enter Entry name");
            _name = Console.ReadLine();
            System.Console.WriteLine("Would you like a prompt? [Y/N]");
            if (Console.ReadLine().ToLower() == "y")
            {
                _promptString = _allPrompts[new Random().Next(0, _allPrompts.Length)];
            }
            else
            {
                _promptString = "";
            }
        }

        /// <summary>
        /// JSON constructor used for deserialization.
        /// The parameter names match JSON properties exactly.
        /// </summary>
        [JsonConstructor]
        public Entry(string _name, DateTime _date, string _promptString, string _content)
        {
            this._name = _name;
            this._date = _date;
            this._promptString = _promptString;
            this._content = _content;            
        }

        /// <summary>
        /// Clears the console and reads input until EOF (Ctrl+Z+Enter) to set entry content.
        /// </summary>
        public void WriteEntry()
        {
            Console.Clear();
            MessageBox.Show($"Prompt: {_promptString} Press Enter, Ctrl+Z, Enter to save entry", 
                            "Instructions", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
            _content = ReadUntilEndOfInput();
            Debug.WriteLine($"Entry saved '{_content}'");
        }

        /// <summary>
        /// Reads console input until a null value is returned (EOF triggered).
        /// </summary>
        static string ReadUntilEndOfInput()
        {
            string input;
            string result = string.Empty;
            while ((input = Console.ReadLine()) != null)
            {
                result += input + Environment.NewLine;
            }
            return result;
        }

        /// <summary>
        /// Displays entry information in the console.
        /// </summary>
        public void Display(bool clear = true)
        {
            if (clear) { Console.Clear(); }
            System.Console.WriteLine($"""
Title: {_name}
{_date}
Prompt: {_promptString}

{_content}
""");
        }
    }
}