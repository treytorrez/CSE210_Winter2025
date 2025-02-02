using System;
using System.IO;
using System.Text.Json;
using JourMinal;

namespace JourMinal
{
    /// <summary>
    /// Models a digital journal stored as a .journal file.
    /// Responsible for loading and saving entries.
    /// </summary>
    class Journal
    {
        public string _journalName;
        public string _journalFile;
        public List<Entry> _allEntries = new();

        // Constructor for an existing journal file.
        public Journal(string name, string file)
        {
            _journalName = name;
            _journalFile = file;
        }

        /// <summary>
        /// Creates a new journal file if it does not exist and returns a Journal object.
        /// </summary>
        public static Journal CreateNewJournal(string name, string directory)
        {
            string filePath = Path.Combine(directory, $"{name}.journal");
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }
            return new Journal(name, filePath);
        }

        /// <summary>
        /// Retrieves all .journal files in the current directory.
        /// </summary>
        public static string[] GetJournalsInDir()
        {
            // Uses the current directory to search for journal files.
            string[] journalFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.journal");
            return journalFiles;
        }

        /// <summary>
        /// Loads entries from the journal file by deserializing JSON.
        /// </summary>
        public void LoadEntries()
        {
            string json = File.ReadAllText(_journalFile);
            if (json.Length > 0)
            {
                try
                {
                    _allEntries = JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
                }
                catch (Exception e)
                {
                    _allEntries = new List<Entry>();
                    System.Console.WriteLine($"Exception encountered, no entries loaded. Exception code:{e}");
                }
            }
        }

        /// <summary>
        /// Saves an entry by loading existing ones, adding the new entry,
        /// serializing them into JSON, and writing to the file.
        /// </summary>
        public void SaveEntry(Entry entry)
        {
            LoadEntries();
            _allEntries.Add(entry);

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(_allEntries, options);
            File.WriteAllText(_journalFile, updatedJson);
        }
    }
}